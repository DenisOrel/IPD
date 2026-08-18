// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TransactionKeeper
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

#nullable disable
namespace Intermech.Interfaces;

internal sealed class TransactionKeeper : ITransactionKeeper, IDisposable
{
  [NotNull]
  private static readonly ConcurrentDictionary<long, TransactionStatus> _statuses = new ConcurrentDictionary<long, TransactionStatus>();
  [NotNull]
  private static readonly ConcurrentDictionary<long, ExactRollbackCause> _rollbackCauses = new ConcurrentDictionary<long, ExactRollbackCause>();
  [NotNull]
  private static readonly ConcurrentDictionary<long, ITransactionKeeper> _activeTractionKeepers = new ConcurrentDictionary<long, ITransactionKeeper>();
  [NotNull]
  private readonly object _syncObject = new object();
  private static long _idGen;
  [NotEmpty]
  private readonly long _id = Interlocked.Increment(ref TransactionKeeper._idGen);
  [NotNull]
  [NotWhitespace]
  private readonly string _name;
  [NotNull]
  [NotWhitespace]
  private readonly string _callerFilePath;
  private volatile TransactionStatus _status;
  [NotNull]
  private volatile IDBTransactions _dbTransactions;
  private readonly bool _isNestedTransaction;
  private readonly ExternalTransactionRelationship _externalTransactionRelationship;
  private volatile bool _transactionStartedByKeeper;
  [CanBeNull]
  private volatile SynchronizationContext _synchronizationContext;
  [CanBeNull]
  private CancellationTokenRegistration? _cancellationTokenRegistration;
  private CancellationToken _cancellationToken;
  private TransactionKeeperDisposeAction _disposeAction;
  private bool _canCommit;
  [CanBeNull]
  private Func<bool> _canCommitMethod;
  private volatile bool _beforeCommitFired;
  private volatile bool _afterCommitFired;
  private volatile bool _beforeRollbackFired;
  private volatile bool _afterRollbackFired;

  [NotNull]
  [ItemNotNull]
  public static ITransactionKeeper[] GetActiveKeepers()
  {
    return TransactionKeeper._activeTractionKeepers.Values.ToArray<ITransactionKeeper>(TransactionKeeper._activeTractionKeepers.Count);
  }

  internal static TransactionStatus GetStatus([NotEmpty] long transactionID)
  {
    TransactionStatus status;
    if (!TransactionKeeper._statuses.TryGetValue(transactionID, out status))
      throw new InvalidOperationException($"Информация о транзакции с ID={status} не обнаружена");
    return status;
  }

  internal static bool TryGetStatus([NotEmpty] long transactionID, out TransactionStatus status)
  {
    if (TransactionKeeper._statuses.TryGetValue(transactionID, out status))
      return true;
    status = TransactionStatus.Unknown;
    return false;
  }

  internal static ExactRollbackCause GetRollbackCause([NotEmpty] long transactionID)
  {
    ExactRollbackCause exactRollbackCause;
    return TransactionKeeper._rollbackCauses.TryGetValue(transactionID, out exactRollbackCause) ? exactRollbackCause : ExactRollbackCause.None;
  }

  internal static bool TryGetRollbackCause([NotEmpty] long transactionID, out ExactRollbackCause rollbackCause)
  {
    if (TransactionKeeper._rollbackCauses.TryGetValue(transactionID, out rollbackCause))
      return true;
    rollbackCause = ExactRollbackCause.None;
    return false;
  }

  [CanBeNull]
  private event Action _beforeCommit;

  [CanBeNull]
  private event Action _afterCommit;

  [CanBeNull]
  private event Action<ExactRollbackCause> _beforeRollback;

  [CanBeNull]
  private event Action<ExactRollbackCause> _afterRollback;

  internal TransactionKeeper(
    [NotNull] IUserSession session,
    [CanBeNull] string name,
    TransactionKeeperDisposeAction disposeAction,
    ExternalTransactionRelationship externalTransactionRelationship,
    [CanBeNull] Func<bool> getCanCommit,
    [CanBeNull] Action beforeCommit,
    [CanBeNull] Action afterCommit,
    [CanBeNull] Action<ExactRollbackCause> beforeRollback,
    [CanBeNull] Action<ExactRollbackCause> afterRollback,
    [CanBeNull] SynchronizationContext synchronizationContext,
    CancellationToken? cancellationToken,
    [CanBeNull] string callerFilePath)
  {
    this.SetStatus(TransactionStatus.Unknown);
    TransactionKeeper._activeTractionKeepers.TryAdd(this._id, (ITransactionKeeper) this);
    this._name = !string.IsNullOrWhiteSpace(name) ? name : "No name";
    this._callerFilePath = !string.IsNullOrWhiteSpace(callerFilePath) ? callerFilePath : "Unknown";
    this._synchronizationContext = synchronizationContext ?? SynchronizationContext.Current;
    this._cancellationToken = !cancellationToken.HasValue || !(cancellationToken.Value != new CancellationToken()) ? CancellationToken.None : cancellationToken.Value;
    if (this._cancellationToken != CancellationToken.None)
      this._cancellationTokenRegistration = new CancellationTokenRegistration?(this._cancellationToken.Register(new Action(this.TransactionCancelled), false));
    this._disposeAction = disposeAction;
    this._externalTransactionRelationship = externalTransactionRelationship;
    this._canCommitMethod = getCanCommit;
    if (afterCommit != null)
      this._afterCommit += afterCommit;
    if (beforeCommit != null)
      this._beforeCommit += beforeCommit;
    if (beforeRollback != null)
      this._beforeRollback += beforeRollback;
    if (afterRollback != null)
      this._afterRollback += afterRollback;
    this._canCommit = true;
    this._dbTransactions = session.GetCustomService<IDBTransactions>();
    this._isNestedTransaction = !this._dbTransactions.InTransaction;
    if (this._externalTransactionRelationship == ExternalTransactionRelationship.CreateNestedTransaction || !this._isNestedTransaction)
    {
      this._dbTransactions.StartTransaction();
      this._transactionStartedByKeeper = true;
    }
    this.SetStatus(TransactionStatus.NotEnded);
    this.ThrowIfCancellationRequested();
  }

  private bool SetStatus(TransactionStatus status)
  {
    switch (status)
    {
      case TransactionStatus.Unknown:
        if (!TransactionKeeper._statuses.TryAdd(this._id, TransactionStatus.Unknown))
          return false;
        this._status = status;
        return true;
      case TransactionStatus.NotEnded:
        if (this._status != TransactionStatus.Unknown || !TransactionKeeper._statuses.TryUpdate(this._id, status, TransactionStatus.Unknown))
          return false;
        this._status = status;
        return true;
      case TransactionStatus.CommitStarted:
        if (this._status != TransactionStatus.NotEnded || !TransactionKeeper._statuses.TryUpdate(this._id, status, TransactionStatus.NotEnded))
          return false;
        this._status = status;
        return true;
      case TransactionStatus.Committed:
        if (this._status != TransactionStatus.CommitStarted || !TransactionKeeper._statuses.TryUpdate(this._id, status, TransactionStatus.CommitStarted))
          return false;
        this._status = status;
        return true;
      case TransactionStatus.RollbackStarted:
        if ((this._status != TransactionStatus.NotEnded || !TransactionKeeper._statuses.TryUpdate(this._id, status, TransactionStatus.NotEnded)) && (this._status != TransactionStatus.CommitStarted || !TransactionKeeper._statuses.TryUpdate(this._id, status, TransactionStatus.CommitStarted)))
          return false;
        this._status = status;
        return true;
      case TransactionStatus.Rollbacked:
        if (this._status != TransactionStatus.RollbackStarted || !TransactionKeeper._statuses.TryUpdate(this._id, status, TransactionStatus.RollbackStarted))
          return false;
        this._status = status;
        return true;
      default:
        throw new ArgumentOutOfRangeException(nameof (status), (object) status, (string) null);
    }
  }

  private bool CheckCanCommit()
  {
    if (!this._canCommit)
      return false;
    if (this._canCommitMethod == null)
      return true;
    return this._synchronizationContext != null && this._synchronizationContext != SynchronizationContext.Current ? this._synchronizationContext.Send<bool>(this._canCommitMethod) : this._canCommitMethod();
  }

  private void FireBeforeCommit([NotNull] IDBTransactions dbTransactions)
  {
    Intermech.Diagnostics.Check.Assert(!this._beforeCommitFired, (Func<string>) (() => "BeforeCommit уже было вызвано"));
    Intermech.Diagnostics.Check.Assert(!this._beforeRollbackFired, (Func<string>) (() => "BeforeRollback уже было вызвано"));
    Intermech.Diagnostics.Check.Assert(!this._afterRollbackFired, (Func<string>) (() => "AfterRollback уже было вызвано"));
    lock (this._syncObject)
    {
      Intermech.Diagnostics.Check.Assert(!this._beforeCommitFired, (Func<string>) (() => "BeforeCommit уже было вызвано"));
      Intermech.Diagnostics.Check.Assert(!this._beforeRollbackFired, (Func<string>) (() => "BeforeRollback уже было вызвано"));
      Intermech.Diagnostics.Check.Assert(!this._afterRollbackFired, (Func<string>) (() => "AfterRollback уже было вызвано"));
      Thread.MemoryBarrier();
      this._beforeCommitFired = true;
      if (this._beforeCommit == null)
        return;
      try
      {
        if (this._synchronizationContext != null && this._synchronizationContext != SynchronizationContext.Current)
          this._synchronizationContext.Send((Action) (() => this._beforeCommit()));
        else
          this._beforeCommit();
      }
      catch
      {
        this.RollbackInternal(dbTransactions, ExactRollbackCause.Exception);
        throw;
      }
    }
  }

  private void FireAfterCommit()
  {
    Intermech.Diagnostics.Check.Assert(!this._afterCommitFired, (Func<string>) (() => "AfterCommit уже было вызвано"));
    Intermech.Diagnostics.Check.Assert(!this._beforeRollbackFired, (Func<string>) (() => "BeforeRollback уже было вызвано"));
    Intermech.Diagnostics.Check.Assert(!this._afterRollbackFired, (Func<string>) (() => "AfterRollback уже было вызвано"));
    lock (this._syncObject)
    {
      Intermech.Diagnostics.Check.Assert(!this._afterCommitFired, (Func<string>) (() => "AfterCommit уже было вызвано"));
      Intermech.Diagnostics.Check.Assert(!this._beforeRollbackFired, (Func<string>) (() => "BeforeRollback уже было вызвано"));
      Intermech.Diagnostics.Check.Assert(!this._afterRollbackFired, (Func<string>) (() => "AfterRollback уже было вызвано"));
      Thread.MemoryBarrier();
      this._afterCommitFired = true;
      if (this._afterCommit == null)
        return;
      if (this._synchronizationContext != null && this._synchronizationContext != SynchronizationContext.Current)
        this._synchronizationContext.Post((Action) (() => this._afterCommit()));
      else
        this._afterCommit();
    }
  }

  private void FireBeforeRollback([NotNull] IDBTransactions dbTransactions, ExactRollbackCause rollbackCause)
  {
    Intermech.Diagnostics.Check.Assert(!this._beforeRollbackFired, (Func<string>) (() => "BeforeRollback уже было вызвано"));
    Intermech.Diagnostics.Check.Assert(!this._afterCommitFired, (Func<string>) (() => "AfterCommit уже было вызвано"));
    lock (this._syncObject)
    {
      Intermech.Diagnostics.Check.Assert(!this._beforeRollbackFired, (Func<string>) (() => "BeforeRollback уже было вызвано"));
      Intermech.Diagnostics.Check.Assert(!this._afterCommitFired, (Func<string>) (() => "AfterCommit уже было вызвано"));
      Thread.MemoryBarrier();
      this._beforeRollbackFired = true;
      if (this._beforeRollback == null)
        return;
      try
      {
        if (this._synchronizationContext != null && this._synchronizationContext != SynchronizationContext.Current)
          this._synchronizationContext.Send((Action) (() => this._beforeRollback(rollbackCause)));
        else
          this._beforeRollback(rollbackCause);
      }
      catch
      {
        this.RollbackInternal(dbTransactions, ExactRollbackCause.Exception);
        throw;
      }
    }
  }

  private void FireAfterRollback(ExactRollbackCause rollbackCause)
  {
    Intermech.Diagnostics.Check.Assert(!this._afterRollbackFired, (Func<string>) (() => "AfterRollback уже было вызвано"));
    Intermech.Diagnostics.Check.Assert(!this._afterCommitFired, (Func<string>) (() => "AfterCommit уже было вызвано"));
    lock (this._syncObject)
    {
      Intermech.Diagnostics.Check.Assert(!this._afterRollbackFired, (Func<string>) (() => "AfterRollback уже было вызвано"));
      Intermech.Diagnostics.Check.Assert(!this._afterCommitFired, (Func<string>) (() => "AfterCommit уже было вызвано"));
      Thread.MemoryBarrier();
      this._afterRollbackFired = true;
      if (this._afterRollback == null)
        return;
      if (this._synchronizationContext != null && this._synchronizationContext != SynchronizationContext.Current)
        this._synchronizationContext.Post((Action) (() => this._afterRollback(rollbackCause)));
      else
        this._afterRollback(rollbackCause);
    }
  }

  private void TransactionCancelled()
  {
    if (this._synchronizationContext != null && this._synchronizationContext != SynchronizationContext.Current)
      this._synchronizationContext.Send(new Action(this.TransactionCancelledInternal));
    else
      this.TransactionCancelledInternal();
  }

  private void TransactionCancelledInternal()
  {
    if (this._dbTransactions == null)
      return;
    lock (this._syncObject)
    {
      if (this._dbTransactions == null)
        return;
      this.RollbackInternal(this._dbTransactions, ExactRollbackCause.Cancellation);
    }
  }

  public void Dispose()
  {
    try
    {
      Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
      this.ThrowIfCancellationRequested();
      lock (this._syncObject)
      {
        IDBTransactions dbTransactions = Interlocked.Exchange<IDBTransactions>(ref this._dbTransactions, (IDBTransactions) null);
        Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) dbTransactions);
        Thread.MemoryBarrier();
        ref CancellationTokenRegistration? local = ref this._cancellationTokenRegistration;
        if (local.HasValue)
          local.GetValueOrDefault().Dispose();
        if (dbTransactions.InTransaction)
        {
          if (Marshal.GetExceptionPointers() != IntPtr.Zero || Marshal.GetExceptionCode() != 0)
            this.RollbackInternal(dbTransactions, ExactRollbackCause.Exception);
          else if (this._cancellationToken != CancellationToken.None && this._cancellationToken.IsCancellationRequested)
          {
            this.RollbackInternal(dbTransactions, ExactRollbackCause.Cancellation);
          }
          else
          {
            if (!this._transactionStartedByKeeper && this._status == TransactionStatus.Rollbacked)
              throw new TransactionRollbackException(this._id, this._name, this._callerFilePath, $"Транзакция \"{this._name}\" была автоматически отменена");
            if (this._status != TransactionStatus.Committed)
            {
              if (this._status != TransactionStatus.Rollbacked)
              {
                switch (this._disposeAction)
                {
                  case TransactionKeeperDisposeAction.AutoCommit:
                    this.CommitInternal(dbTransactions, false);
                    break;
                  case TransactionKeeperDisposeAction.AutoRollback:
                    this.RollbackInternal(dbTransactions, ExactRollbackCause.AutoRollbackOnDisposeKeeper);
                    break;
                  default:
                    throw new ArgumentOutOfRangeException();
                }
              }
            }
          }
        }
      }
      TransactionKeeper._activeTractionKeepers.TryRemove(this._id, out ITransactionKeeper _);
    }
    finally
    {
      Intermech.Diagnostics.Check.Assert(!this._transactionStartedByKeeper, $"Транзакция \"{this._name}\" стартована, но не закрыта!");
      if (this._status != TransactionStatus.Committed && this._status != TransactionStatus.Rollbacked)
        throw new InvalidOperationException($"Транзакция \"{this._name}\": После Dispose ITransactionKeeper транзакция должна иметь статус или закоммичена, или откачена");
      if (this._afterCommitFired && !this._beforeCommitFired)
        throw new InvalidOperationException($"Транзакция \"{this._name}\": AfterCommit был вызван, однако BeforeCommit - нет");
      if (this._afterRollbackFired && !this._beforeRollbackFired)
        throw new InvalidOperationException($"Транзакция \"{this._name}\": AfterRollback был вызван, однако BeforeRollback - нет");
      if (!this._afterCommitFired && !this._afterRollbackFired)
        throw new InvalidOperationException($"Транзакция \"{this._name}\": До Dispose обязательно должен быть вызван AfterCommit или AfterRollback");
      if (TransactionKeeper._activeTractionKeepers.ContainsKey(this._id))
        throw new InvalidOperationException($"После Dispose транзакция \"{this._name}\" всё ещё считается активной");
    }
  }

  private void CommitInternal([NotNull] IDBTransactions dbTransactions, bool manual)
  {
    if (!dbTransactions.InTransaction || this._status == TransactionStatus.Committed || this._status == TransactionStatus.Rollbacked)
      return;
    this.ThrowIfCancellationRequested();
    bool flag;
    try
    {
      flag = this.CheckCanCommit();
    }
    catch
    {
      this.RollbackInternal(dbTransactions, ExactRollbackCause.Exception);
      throw;
    }
    if (!flag)
    {
      this.ForceCommitInternal(dbTransactions, manual);
    }
    else
    {
      if (!this._transactionStartedByKeeper)
        throw new InvalidCommitException(this._id, this._name, this._callerFilePath, $"Транзакцию \"{this._name}\" нельзя закоммитить, не пройдена проверка СanCommit");
      this.RollbackInternal(dbTransactions, ExactRollbackCause.NegativeCommitValidation);
    }
  }

  private void ForceCommitInternal([NotNull] IDBTransactions dbTransactions, bool manual)
  {
    if (!dbTransactions.InTransaction || this._status == TransactionStatus.Committed || this._status == TransactionStatus.Rollbacked)
      return;
    this.ThrowIfCancellationRequested();
    if (!this.SetStatus(TransactionStatus.CommitStarted))
      return;
    this.FireBeforeCommit(dbTransactions);
    this.ThrowIfCancellationRequested();
    try
    {
      if (this._transactionStartedByKeeper)
      {
        this.ThrowIfCancellationRequested();
        dbTransactions.Commit();
        this._transactionStartedByKeeper = false;
      }
      if (this.SetStatus(TransactionStatus.Committed))
        TransactionKeeper._rollbackCauses.TryAdd(this._id, ExactRollbackCause.NotRollbacked);
      TransactionKeeper._activeTractionKeepers.TryRemove(this._id, out ITransactionKeeper _);
    }
    catch
    {
      this.RollbackInternal(dbTransactions, ExactRollbackCause.Exception);
      throw;
    }
    this.FireAfterCommit();
  }

  private void RollbackInternal([NotNull] IDBTransactions dbTransactions, ExactRollbackCause cause)
  {
    if (!dbTransactions.InTransaction || this._status == TransactionStatus.Committed || this._status == TransactionStatus.Rollbacked)
      return;
    bool flag = this._status == TransactionStatus.RollbackStarted && cause == ExactRollbackCause.Exception;
    if (!flag && !this.SetStatus(TransactionStatus.RollbackStarted))
      return;
    if (!flag)
    {
      this.FireBeforeRollback(dbTransactions, cause);
      this.ThrowIfCancellationRequested();
    }
    try
    {
      int num = this._transactionStartedByKeeper ? 1 : 0;
      if (this._transactionStartedByKeeper)
      {
        dbTransactions.Rollback();
        this._transactionStartedByKeeper = false;
      }
      if (this.SetStatus(TransactionStatus.Rollbacked))
        TransactionKeeper._rollbackCauses.TryAdd(this._id, cause);
      TransactionKeeper._activeTractionKeepers.TryRemove(this._id, out ITransactionKeeper _);
      if (num == 0 && cause != ExactRollbackCause.Exception)
        throw new TransactionRollbackException(this._id, this._name, this._callerFilePath, $"Транзакция \"{this._name}\" была отменена по причине {cause.GetName<ExactRollbackCause>()}");
    }
    finally
    {
      this.FireAfterRollback(cause);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private void ThrowIfCancellationRequested()
  {
    if (this._status == TransactionStatus.RollbackStarted || !(this._cancellationToken != CancellationToken.None) || !this._cancellationToken.IsCancellationRequested || this._dbTransactions == null)
      return;
    lock (this._syncObject)
    {
      if (this._dbTransactions != null)
      {
        if (this._transactionStartedByKeeper)
          this.RollbackInternal(this._dbTransactions, ExactRollbackCause.Cancellation);
      }
    }
    this._cancellationToken.ThrowIfCancellationRequested();
  }

  long ITransactionKeeper.ID
  {
    get
    {
      Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
      this.ThrowIfCancellationRequested();
      return this._id;
    }
  }

  [NotNull]
  [NotWhitespace]
  string ITransactionKeeper.Name
  {
    get
    {
      Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
      this.ThrowIfCancellationRequested();
      return this._name;
    }
  }

  [NotNull]
  [NotWhitespace]
  string ITransactionKeeper.CallerFilePath
  {
    get
    {
      Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
      this.ThrowIfCancellationRequested();
      return this._callerFilePath;
    }
  }

  TransactionStatus ITransactionKeeper.Status
  {
    get
    {
      Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
      this.ThrowIfCancellationRequested();
      return this._status;
    }
  }

  [CanBeNull]
  SynchronizationContext ITransactionKeeper.SynchronizationContext
  {
    get
    {
      Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
      this.ThrowIfCancellationRequested();
      return this._synchronizationContext;
    }
    set
    {
      Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
      this.ThrowIfCancellationRequested();
      if (this._synchronizationContext == value)
        return;
      lock (this._syncObject)
      {
        Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
        this.ThrowIfCancellationRequested();
        if (this._synchronizationContext == value)
          return;
        Thread.MemoryBarrier();
        this._synchronizationContext = value;
      }
    }
  }

  CancellationToken? ITransactionKeeper.CancellationToken
  {
    get
    {
      Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
      this.ThrowIfCancellationRequested();
      return !(this._cancellationToken != CancellationToken.None) ? new CancellationToken?() : new CancellationToken?(this._cancellationToken);
    }
    set
    {
      Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
      this.ThrowIfCancellationRequested();
      lock (this._syncObject)
      {
        Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
        this.ThrowIfCancellationRequested();
        Thread.MemoryBarrier();
        CancellationToken cancellationToken = !value.HasValue || !(value.Value != new CancellationToken()) ? CancellationToken.None : value.Value;
        if (!(this._cancellationToken != cancellationToken))
          return;
        ref CancellationTokenRegistration? local = ref this._cancellationTokenRegistration;
        if (local.HasValue)
          local.GetValueOrDefault().Dispose();
        this._cancellationToken = cancellationToken;
        if (this._cancellationToken != CancellationToken.None)
          this._cancellationTokenRegistration = new CancellationTokenRegistration?(this._cancellationToken.Register(new Action(this.TransactionCancelled), false));
        else
          this._cancellationTokenRegistration = new CancellationTokenRegistration?();
      }
    }
  }

  TransactionKeeperDisposeAction ITransactionKeeper.DisposeAction
  {
    get
    {
      Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
      this.ThrowIfCancellationRequested();
      return this._disposeAction;
    }
    set
    {
      Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
      this.ThrowIfCancellationRequested();
      if (this._disposeAction == value)
        return;
      lock (this._syncObject)
      {
        Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
        this.ThrowIfCancellationRequested();
        if (this._disposeAction == value)
          return;
        Thread.MemoryBarrier();
        this._disposeAction = value;
      }
    }
  }

  ExternalTransactionRelationship ITransactionKeeper.ExternalTransactionRelationship
  {
    get
    {
      Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
      this.ThrowIfCancellationRequested();
      return this._externalTransactionRelationship;
    }
  }

  bool ITransactionKeeper.CanCommit
  {
    get
    {
      Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
      this.ThrowIfCancellationRequested();
      return this.CheckCanCommit();
    }
    set
    {
      Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
      this.ThrowIfCancellationRequested();
      if (this._canCommit == value)
        return;
      lock (this._syncObject)
      {
        Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
        this.ThrowIfCancellationRequested();
        Thread.MemoryBarrier();
        this._canCommit = value;
      }
    }
  }

  [CanBeNull]
  Func<bool> ITransactionKeeper.CanCommitMethod
  {
    get
    {
      Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
      this.ThrowIfCancellationRequested();
      return this._canCommitMethod;
    }
    set
    {
      Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
      this.ThrowIfCancellationRequested();
      if (this._canCommitMethod == value)
        return;
      lock (this._syncObject)
      {
        Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
        this.ThrowIfCancellationRequested();
        Thread.MemoryBarrier();
        this._canCommitMethod = value;
      }
    }
  }

  [CanBeNull]
  event Action ITransactionKeeper.BeforeCommit
  {
    add
    {
      Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
      this.ThrowIfCancellationRequested();
      if (value == null)
        return;
      lock (this._syncObject)
      {
        Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
        this.ThrowIfCancellationRequested();
        Thread.MemoryBarrier();
        this._beforeCommit += value;
      }
    }
    remove
    {
      Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
      this.ThrowIfCancellationRequested();
      if (value == null)
        return;
      lock (this._syncObject)
      {
        Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
        this.ThrowIfCancellationRequested();
        Thread.MemoryBarrier();
        this._beforeCommit -= value;
      }
    }
  }

  [CanBeNull]
  event Action ITransactionKeeper.AfterCommit
  {
    add
    {
      Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
      this.ThrowIfCancellationRequested();
      if (value == null)
        return;
      lock (this._syncObject)
      {
        Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
        this.ThrowIfCancellationRequested();
        Thread.MemoryBarrier();
        this._afterCommit += value;
      }
    }
    remove
    {
      Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
      this.ThrowIfCancellationRequested();
      if (value == null)
        return;
      lock (this._syncObject)
      {
        Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
        this.ThrowIfCancellationRequested();
        Thread.MemoryBarrier();
        this._afterCommit -= value;
      }
    }
  }

  [CanBeNull]
  event Action<ExactRollbackCause> ITransactionKeeper.BeforeRollback
  {
    add
    {
      Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
      this.ThrowIfCancellationRequested();
      if (value == null)
        return;
      lock (this._syncObject)
      {
        Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
        this.ThrowIfCancellationRequested();
        Thread.MemoryBarrier();
        this._beforeRollback += value;
      }
    }
    remove
    {
      Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
      this.ThrowIfCancellationRequested();
      if (value == null)
        return;
      lock (this._syncObject)
      {
        Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
        this.ThrowIfCancellationRequested();
        Thread.MemoryBarrier();
        this._beforeRollback -= value;
      }
    }
  }

  [CanBeNull]
  event Action<ExactRollbackCause> ITransactionKeeper.AfterRollback
  {
    add
    {
      Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
      this.ThrowIfCancellationRequested();
      if (value == null)
        return;
      lock (this._syncObject)
      {
        Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
        this.ThrowIfCancellationRequested();
        Thread.MemoryBarrier();
        this._afterRollback += value;
      }
    }
    remove
    {
      Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
      this.ThrowIfCancellationRequested();
      if (value == null)
        return;
      lock (this._syncObject)
      {
        Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
        this.ThrowIfCancellationRequested();
        Thread.MemoryBarrier();
        this._afterRollback -= value;
      }
    }
  }

  bool ITransactionKeeper.IsNestedTransaction
  {
    get
    {
      Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
      this.ThrowIfCancellationRequested();
      return this._isNestedTransaction;
    }
  }

  bool ITransactionKeeper.InTransaction
  {
    get
    {
      Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
      this.ThrowIfCancellationRequested();
      lock (this._syncObject)
      {
        Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
        this.ThrowIfCancellationRequested();
        Thread.MemoryBarrier();
        return this._dbTransactions.InTransaction;
      }
    }
  }

  bool ITransactionKeeper.TransactionStartedByKeeper
  {
    get
    {
      Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
      this.ThrowIfCancellationRequested();
      return this._transactionStartedByKeeper;
    }
  }

  void ITransactionKeeper.Commit()
  {
    Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
    this.ThrowIfCancellationRequested();
    lock (this._syncObject)
    {
      Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
      this.ThrowIfCancellationRequested();
      Thread.MemoryBarrier();
      this.CommitInternal(this._dbTransactions, true);
    }
  }

  private void CheckTransactionNotClosed()
  {
    if (!this._dbTransactions.InTransaction)
      throw new InvalidOperationException($"Транзакция \"{this._name}\" уже не активна!");
    switch (this._status)
    {
      case TransactionStatus.CommitStarted:
        throw new InvalidOperationException($"Коммит транзакции \"{this._name}\" уже начался!");
      case TransactionStatus.Committed:
        throw new InvalidOperationException($"Транзакция \"{this._name}\" уже закоммичена!");
      case TransactionStatus.RollbackStarted:
        throw new InvalidOperationException($"Откат транзакции \"{this._name}\" уже начался!");
      case TransactionStatus.Rollbacked:
        throw new InvalidOperationException($"Транзакция \"{this._name}\" уже откачена!");
    }
  }

  bool ITransactionKeeper.TryCommit()
  {
    Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
    this.ThrowIfCancellationRequested();
    lock (this._syncObject)
    {
      Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
      this.ThrowIfCancellationRequested();
      Thread.MemoryBarrier();
      try
      {
        this.ThrowIfCancellationRequested();
        if (!this.CheckCanCommit())
          return false;
      }
      catch
      {
        this.RollbackInternal(this._dbTransactions, ExactRollbackCause.Exception);
        throw;
      }
      this.ThrowIfCancellationRequested();
      this.ForceCommitInternal(this._dbTransactions, true);
      return true;
    }
  }

  void ITransactionKeeper.Rollback()
  {
    Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
    this.ThrowIfCancellationRequested();
    lock (this._syncObject)
    {
      Intermech.Diagnostics.Check.NotDisposed<TransactionKeeper>((object) this._dbTransactions);
      this.ThrowIfCancellationRequested();
      Thread.MemoryBarrier();
      this.RollbackInternal(this._dbTransactions, ExactRollbackCause.Manual);
    }
  }
}
