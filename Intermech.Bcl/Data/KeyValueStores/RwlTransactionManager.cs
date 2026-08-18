
// Type: Intermech.Data.KeyValueStores.RwlTransactionManager
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Diagnostics;
using System;
using System.Runtime.CompilerServices;
using System.Threading;


namespace Intermech.Data.KeyValueStores
{
    internal class RwlTransactionManager : IRwlCommitRollbackManager, IRwlQuerySynchronizer
    {
      private static readonly RwlReadScope nestedQueryScope;
      private static readonly RwlWriteScope nestedUpdateScope;
      private readonly IRwlTransactionTarget txTarget;
      private readonly int timeout;
      private readonly ReaderWriterLockSlim rwl;
      private readonly ThreadLocal<RwlTransactionThreadState> currentTx;
      private volatile bool isStopped;
      private volatile int newTxThreads;
      private bool disableNewTx;

      public RwlTransactionManager(IRwlTransactionTarget target, int timeout)
      {
        if (target == null)
          throw new ArgumentNullException(nameof (target));
        if (timeout <= 0)
          throw new ArgumentOutOfRangeException(nameof (timeout));
        this.txTarget = target;
        this.timeout = timeout;
        this.rwl = new ReaderWriterLockSlim();
        this.currentTx = new ThreadLocal<RwlTransactionThreadState>();
      }

      public bool TryStop()
      {
        if (this.isStopped)
          return true;
        this.CheckNotInTransaction();
        if (!this.rwl.TryEnterWriteLock(this.timeout))
          return false;
        this.disableNewTx = true;
        this.rwl.ExitWriteLock();
        int num = Math.Max(this.timeout / 25, 10);
        while (this.newTxThreads != 0 && num != 0)
        {
          --num;
          Thread.Sleep(25);
        }
        if (num <= 0)
          return false;
        try
        {
          this.rwl.Dispose();
          this.currentTx.Dispose();
          this.isStopped = true;
          return true;
        }
        catch (Exception ex)
        {
          SuppressedExceptions.TraceException(ex, "RwlTransactionManager.TryStop()");
          return false;
        }
      }

      private void EnterLock(bool writeMode)
      {
        Interlocked.Increment(ref this.newTxThreads);
        try
        {
          if ((writeMode ? (this.rwl.TryEnterWriteLock(this.timeout) ? 1 : 0) : (this.rwl.TryEnterReadLock(this.timeout) ? 1 : 0)) == 0)
            throw new TimeoutException(RwlTransactionResources.SR_BeginTransactionTimeout);
          if (this.disableNewTx)
          {
            if (writeMode)
              this.rwl.ExitWriteLock();
            else
              this.rwl.ExitReadLock();
            throw new InvalidOperationException(RwlTransactionResources.SR_TransactionManagerWasTurnedOff);
          }
        }
        finally
        {
          Interlocked.Decrement(ref this.newTxThreads);
        }
      }

      public RwlReadScope BeginQueryScope()
      {
        this.txTarget.CheckTransactionScopeIsAllowed(false);
        if (this.currentTx.Value != null)
          return RwlTransactionManager.nestedQueryScope;
        this.EnterLock(false);
        return new RwlReadScope(this.rwl);
      }

      public RwlWriteScope BeginUpdateScope()
      {
        this.txTarget.CheckTransactionScopeIsAllowed(true);
        if (this.currentTx.Value != null)
          return RwlTransactionManager.nestedUpdateScope;
        this.EnterLock(true);
        return new RwlWriteScope(this.rwl);
      }

      public CommitableObjectScope BeginTransactionScope(bool canWrite = true)
      {
        RwlTransactionThreadState threadState = this.currentTx.Value;
        if (threadState == null)
          return new CommitableObjectScope((ICommitableObjectThreadState) this.BeginTransactionInternal(canWrite), true);
        this.txTarget.CheckTransactionScopeIsAllowed(canWrite);
        if (canWrite && !threadState.Transaction.CanWrite)
          throw new InvalidOperationException(RwlTransactionResources.SR_WriteTransactionRequired);
        return new CommitableObjectScope((ICommitableObjectThreadState) threadState, false);
      }

      public RwlTransaction BeginTransaction(bool canWrite)
      {
        if (this.currentTx.Value == null)
          return this.BeginTransactionInternal(canWrite).Transaction;
        throw new InvalidOperationException(RwlTransactionResources.SR_CantBeginAnotherTransaction);
      }

      private RwlTransactionThreadState BeginTransactionInternal(bool canWrite)
      {
        this.EnterLock(canWrite);
        RwlTransaction rwlTransaction = new RwlTransaction((IRwlCommitRollbackManager) this, canWrite);
        try
        {
          this.txTarget.BeginTransaction(rwlTransaction);
        }
        catch
        {
          this.EndTransaction(rwlTransaction);
          throw;
        }
        RwlTransactionThreadState transactionThreadState = new RwlTransactionThreadState(rwlTransaction);
        this.currentTx.Value = transactionThreadState;
        return transactionThreadState;
      }

      void IRwlCommitRollbackManager.CommitTransaction(RwlTransaction transaction)
      {
        try
        {
          this.txTarget.CommitTransaction(transaction);
        }
        finally
        {
          this.EndTransaction(transaction);
        }
      }

      void IRwlCommitRollbackManager.RollbackTransaction(RwlTransaction transaction)
      {
        try
        {
          this.txTarget.RollbackTransaction(transaction);
        }
        finally
        {
          this.EndTransaction(transaction);
        }
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      private void EndTransaction(RwlTransaction tx)
      {
        if (tx.CanWrite)
          this.rwl.ExitWriteLock();
        else
          this.rwl.ExitReadLock();
        this.currentTx.Value = (RwlTransactionThreadState) null;
      }

      public bool InTransaction() => this.currentTx.Value != null;

      public RwlTransaction TryGetCurrentTransaction() => this.currentTx.Value?.Transaction;

      public void CheckNotInTransaction()
      {
        if (this.currentTx.Value != null || this.rwl.IsReadLockHeld || this.rwl.IsWriteLockHeld)
          throw new InvalidOperationException(RwlTransactionResources.SR_TransactionIsNotAllowed);
      }
    }
}
