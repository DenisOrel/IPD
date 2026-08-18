
// Type: Intermech.Data.DataScope
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.ControlFlow;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Data
{
    public static class DataScope
    {
      private static readonly DynamicVariable<ConnectionData> Connection = new DynamicVariable<ConnectionData>("DbContext.Connection", (ConnectionData) null);
      private static readonly DynamicVariable<TransactionData> Tx = new DynamicVariable<TransactionData>("DbContext.Tx", (TransactionData) null);

      public static void RequireNew()
      {
        DataScope.Connection.Declare((ConnectionData) null);
        DataScope.Tx.Declare((TransactionData) null);
      }

      public static void OpenConnection(IDbConnectionPool pool)
      {
        if (pool == null)
          throw new ArgumentNullException(nameof (pool));
        if (DataScope.Connection.Value != null)
          return;
        IDbConnection connection = pool.AllocateConnection();
        DataScope.Connection.Value = new ConnectionData(pool, connection);
      }

      public static IDbCommand CreateCommand()
      {
        IDbCommand command = DataScope.GetActiveConnection().Connection.CreateCommand();
            TransactionData transactionData = DataScope.Tx.Value;
        if (transactionData != null)
          command.Transaction = transactionData.Transaction;
        return command;
      }

      public static void RequireNoTransaction()
      {
        if (DataScope.InTransaction)
          throw new InvalidOperationException("Для корректной работы метода недопустимо наличие открытой транзакции.");
      }

      public static bool InTransaction => DataScope.Tx.Value != null;

      public static void BeginTransaction(IsolationLevel level = IsolationLevel.ReadCommitted)
      {
            TransactionData transactionData = DataScope.Tx.Value;
        if (transactionData == null)
        {
          transactionData = new TransactionData(DataScope.GetActiveConnection().Connection.BeginTransaction(level));
          DataScope.Tx.Value = transactionData;
        }
        ++transactionData.Depth;
      }

      public static void Commit()
      {
            TransactionData activeTransaction = DataScope.GetActiveTransaction();
        --activeTransaction.Depth;
        if (activeTransaction.Depth != 0)
          return;
        DataScope.Tx.Value = (TransactionData) null;
        activeTransaction.Transaction.Commit();
        if (activeTransaction.CommitActions == null)
          return;
        foreach (Action commitAction in (IEnumerable<Action>) activeTransaction.CommitActions)
          commitAction();
        activeTransaction.CommitActions = (ICollection<Action>) null;
      }

      public static void Rollback()
      {
            TransactionData activeTransaction = DataScope.GetActiveTransaction();
        DataScope.Tx.Value = (TransactionData) null;
        activeTransaction.Transaction.Rollback();
      }

      public static void RegisterCommitAction(Action action)
      {
        if (action == null)
          throw new ArgumentNullException(nameof (action));
            TransactionData activeTransaction = DataScope.GetActiveTransaction();
        if (activeTransaction.CommitActions == null)
          activeTransaction.CommitActions = (ICollection<Action>) new LinkedList<Action>();
        activeTransaction.CommitActions.Add(action);
      }

      private static TransactionData GetActiveTransaction()
      {
        return DataScope.Tx.Value ?? throw new InvalidOperationException(LocalizationHolder.rm.GetString("SR_1673"));
      }

      private static ConnectionData GetActiveConnection()
      {
        return DataScope.Connection.Value ?? throw new InvalidOperationException(LocalizationHolder.rm.GetString("SR_1674"));
      }

      private sealed class ConnectionData : IDisposable
      {
        public readonly IDbConnectionPool Pool;
        public IDbConnection Connection;

        public ConnectionData(IDbConnectionPool pool, IDbConnection connection)
        {
          this.Pool = pool;
          this.Connection = connection;
        }

        public void Dispose()
        {
          if (this.Connection == null)
            return;
          IDbConnection connection = this.Connection;
          this.Connection = (IDbConnection) null;
          this.Pool.ReleaseConnection(connection);
        }
      }

      private sealed class TransactionData
      {
        public readonly IDbTransaction Transaction;
        public int Depth;
        public ICollection<Action> CommitActions;

        public TransactionData(IDbTransaction tx)
        {
          this.Transaction = tx;
          this.Depth = 0;
        }
      }
    }
}
