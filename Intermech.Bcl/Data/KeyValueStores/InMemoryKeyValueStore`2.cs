
// Type: Intermech.Data.KeyValueStores.InMemoryKeyValueStore`2
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace Intermech.Data.KeyValueStores
{
    public class InMemoryKeyValueStore<TKey, TValue> : 
      IKeyValueStore<TKey, TValue>,
      IDisposable,
      IRwlTransactionTarget
      where TKey : IEquatable<TKey>
    {
      private int contentVersion;
      private Dictionary<TKey, TValue> valueTable;
      private bool valueTableCorrupted;
      private List<IKeyValueStoreReplica<TKey, TValue>> replicas;
      private List<InMemoryKeyValueStoreView<TKey, TValue>> views;
      private RwlTransactionManager txManager;
      private List<KeyValueStoreOperation<TKey, TValue>> historyLog;
      private bool isDisposed;

      public InMemoryKeyValueStore(
        InMemoryKeyValueStoreParameters<TKey, TValue> parameters)
      {
        if (parameters == null)
          throw new ArgumentNullException(nameof (parameters));
        this.contentVersion = 0;
        this.valueTable = new Dictionary<TKey, TValue>();
        this.replicas = new List<IKeyValueStoreReplica<TKey, TValue>>();
        this.views = parameters.Views;
        this.txManager = new RwlTransactionManager((IRwlTransactionTarget) this, parameters.TransactionTimeout);
        this.historyLog = new List<KeyValueStoreOperation<TKey, TValue>>();
        if (this.views.Count == 0)
          return;
        this.InitializeViews();
      }

      public void Dispose() => this.TryDispose();

      public bool TryDispose()
      {
        if (this.isDisposed)
          return true;
        if (!this.txManager.TryStop())
          return false;
        this.ClearDataSilently(0);
        this.views.Clear();
        this.isDisposed = true;
        return true;
      }

      public bool IsDisposed
      {
        [DebuggerStepThrough] get => this.isDisposed;
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      private void CheckNotDisposed()
      {
        if (this.isDisposed)
          throw this.CreateObjectDisposedException();
      }

      private ObjectDisposedException CreateObjectDisposedException()
      {
        return new ObjectDisposedException(this.GetType().FullName);
      }

      public void LoadData(IKeyValueDataReader<TKey, TValue> dataReader)
      {
        if (dataReader == null)
          throw new ArgumentNullException(nameof (dataReader));
        this.CheckNotDisposed();
        this.txManager.CheckNotInTransaction();
        using (this.txManager.BeginUpdateScope())
        {
          this.CheckLoadDataAllowed();
          try
          {
            this.ClearData(dataReader.ContentVersion);
            for (KeyValuePair<TKey, TValue>? nullable = dataReader.TryRead(); nullable.HasValue; nullable = dataReader.TryRead())
              this.LoadData(nullable.Value);
          }
          catch
          {
            this.ClearDataSilently(0);
            throw;
          }
        }
      }

      public void LoadData(IKeyValueDataCursor<TKey, TValue> dataCursor)
      {
        if (dataCursor == null)
          throw new ArgumentNullException(nameof (dataCursor));
        this.CheckNotDisposed();
        this.txManager.CheckNotInTransaction();
        using (this.txManager.BeginUpdateScope())
        {
          this.CheckLoadDataAllowed();
          try
          {
            this.ClearData(dataCursor.ContentVersion);
            dataCursor.ScanData(new Action<KeyValuePair<TKey, TValue>>(this.LoadData));
          }
          catch
          {
            this.ClearDataSilently(0);
            throw;
          }
        }
      }

      private void CheckLoadDataAllowed()
      {
        if (this.replicas.Count != 0)
          throw new InvalidOperationException("Загрузка данных в хранилище невозможна, так как к хранилищу уже подключены реплики.");
      }

      private void ClearDataSilently(int newContentVersion)
      {
        SilentActionInvoker.Default.Invoke((Action) (() => this.ClearData(newContentVersion)), "InMemoryKeyValueStore.ClearDataSilently()");
      }

      private void ClearData(int newContentVersion)
      {
        this.contentVersion = newContentVersion;
        this.valueTable.Clear();
        if (this.views.Count == 0)
          return;
        this.ClearViews();
      }

      private void LoadData(KeyValuePair<TKey, TValue> keyValuePair)
      {
        this.valueTable.Add(keyValuePair.Key, keyValuePair.Value);
        if (this.views.Count == 0)
          return;
        this.UpdateViews(new KeyValueStoreOperation<TKey, TValue>(KeyValueStoreOpCode.AppendItem, keyValuePair.Key, keyValuePair.Value));
      }

      public void RegisterReplica(IKeyValueStoreReplica<TKey, TValue> replica)
      {
        if (replica == null)
          throw new ArgumentNullException(nameof (replica));
        this.CheckNotDisposed();
        using (this.txManager.BeginUpdateScope())
        {
          if (this.replicas.Contains(replica))
            return;
          if (replica.ContentVersion != this.contentVersion)
            throw new InvalidOperationException("Невозможно зарегистрировать реплику в  хранилище, так как идентификатор версии содержимого реплики не совпадает с идентификатором версии содержимого хранилища.");
          replica.Attach((IKeyValueStore<TKey, TValue>) this);
          this.replicas.Add(replica);
        }
      }

      public void UnregisterReplica(IKeyValueStoreReplica<TKey, TValue> replica)
      {
        if (replica == null)
          throw new ArgumentNullException(nameof (replica));
        this.CheckNotDisposed();
        using (this.txManager.BeginUpdateScope())
        {
          if (this.replicas.Count == 0)
            return;
          int index = this.replicas.IndexOf(replica);
          if (index < 0)
            return;
          replica.Detach();
          this.replicas.RemoveAt(index);
        }
      }

      public int ContentVersion
      {
        [DebuggerStepThrough] get
        {
          this.CheckNotDisposed();
          using (this.txManager.BeginQueryScope())
            return this.contentVersion;
        }
      }

      public int Count
      {
        [DebuggerStepThrough] get
        {
          this.CheckNotDisposed();
          using (this.txManager.BeginQueryScope())
            return this.valueTable.Count;
        }
      }

      public TValue TryGetByKey(TKey key)
      {
        this.CheckNotDisposed();
        using (this.txManager.BeginQueryScope())
        {
          TValue obj;
          return this.valueTable.TryGetValue(key, out obj) ? obj : default (TValue);
        }
      }

      public List<TKey> GetKeys()
      {
        this.CheckNotDisposed();
        using (this.txManager.BeginQueryScope())
        {
          List<TKey> keys = new List<TKey>(this.valueTable.Count);
          foreach (KeyValuePair<TKey, TValue> keyValuePair in this.valueTable)
            keys.Add(keyValuePair.Key);
          return keys;
        }
      }

      public List<TValue> GetAll()
      {
        this.CheckNotDisposed();
        using (this.txManager.BeginQueryScope())
        {
          List<TValue> all = new List<TValue>(this.valueTable.Count);
          foreach (KeyValuePair<TKey, TValue> keyValuePair in this.valueTable)
            all.Add(keyValuePair.Value);
          return all;
        }
      }

      public void Add(TKey key, TValue value)
      {
        this.CheckNotDisposed();
        using (CommitableObjectScope commitableObjectScope = this.txManager.BeginTransactionScope())
        {
          if (this.valueTable.ContainsKey(key))
            throw new InvalidOperationException($"Невозможно добавить в хранилище новое значение с ключем '{key}', так как значение ключа не уникально. В хранилище уже есть другое значение с таким ключем.");
          this.valueTable.Add(key, value);
          KeyValueStoreOperation<TKey, TValue> operation = new KeyValueStoreOperation<TKey, TValue>(KeyValueStoreOpCode.AppendItem, key, value);
          this.historyLog.Add(operation);
          if (this.views.Count != 0)
            this.UpdateViews(operation);
          commitableObjectScope.Complete();
        }
      }

      public void Update(TKey key, TValue value)
      {
        this.CheckNotDisposed();
        using (CommitableObjectScope commitableObjectScope = this.txManager.BeginTransactionScope())
        {
          TValue previousValue = this.valueTable.ContainsKey(key) ? this.valueTable[key] : throw new InvalidOperationException($"Невозможно обновить в хранилище значение с ключем '{key}', так как значение с таким ключем отсутствует.");
          this.valueTable[key] = value;
          KeyValueStoreOperation<TKey, TValue> operation = new KeyValueStoreOperation<TKey, TValue>(KeyValueStoreOpCode.ReplaceItem, key, value, previousValue);
          this.historyLog.Add(operation);
          if (this.views.Count != 0)
            this.UpdateViews(operation);
          commitableObjectScope.Complete();
        }
      }

      public void Remove(TKey key)
      {
        this.CheckNotDisposed();
        using (CommitableObjectScope commitableObjectScope = this.txManager.BeginTransactionScope())
        {
          if (this.valueTable.ContainsKey(key))
          {
            TValue obj = this.valueTable[key];
            this.valueTable.Remove(key);
            KeyValueStoreOperation<TKey, TValue> operation = new KeyValueStoreOperation<TKey, TValue>(KeyValueStoreOpCode.RemoveItem, key, obj);
            this.historyLog.Add(operation);
            if (this.views.Count != 0)
              this.UpdateViews(operation);
          }
          commitableObjectScope.Complete();
        }
      }

      public ICommitableObject BeginTransaction(bool canWrite = true)
      {
        this.CheckNotDisposed();
        return (ICommitableObject) this.txManager.BeginTransaction(canWrite);
      }

      public CommitableObjectScope BeginTransactionScope(bool canWrite = true)
      {
        this.CheckNotDisposed();
        return this.txManager.BeginTransactionScope(canWrite);
      }

      void IRwlTransactionTarget.CheckTransactionScopeIsAllowed(bool canWrite)
      {
        this.CheckDataIsNotCorrupted();
      }

      void IRwlTransactionTarget.BeginTransaction(RwlTransaction transaction)
      {
        this.CheckDataIsNotCorrupted();
      }

      void IRwlTransactionTarget.CommitTransaction(RwlTransaction transaction)
      {
        if (!transaction.CanWrite)
          return;
        if (this.valueTableCorrupted)
        {
          this.ResetTransactionResourcesOnly();
        }
        else
        {
          ++this.contentVersion;
          if (this.historyLog.Count == 0)
            return;
          try
          {
            if (this.replicas.Count == 0)
              return;
            this.UpdateReplicas();
          }
          finally
          {
            this.historyLog.Clear();
          }
        }
      }

      void IRwlTransactionTarget.RollbackTransaction(RwlTransaction transaction)
      {
        if (!transaction.CanWrite)
          return;
        if (this.valueTableCorrupted)
        {
          this.ResetTransactionResourcesOnly();
        }
        else
        {
          if (this.historyLog.Count == 0)
            return;
          try
          {
            this.PlayRollbackLog();
          }
          finally
          {
            this.historyLog.Clear();
          }
        }
      }

      private void PlayRollbackLog()
      {
        for (int index = this.historyLog.Count - 1; index >= 0; --index)
        {
          KeyValueStoreOperation<TKey, TValue> valueStoreOperation = this.historyLog[index];
          switch (valueStoreOperation.OpCode)
          {
            case KeyValueStoreOpCode.AppendItem:
              this.valueTable.Remove(valueStoreOperation.Key);
              if (this.views.Count != 0)
              {
                this.UpdateViews(new KeyValueStoreOperation<TKey, TValue>(KeyValueStoreOpCode.RemoveItem, valueStoreOperation.Key, valueStoreOperation.Value));
                break;
              }
              break;
            case KeyValueStoreOpCode.ReplaceItem:
              this.valueTable[valueStoreOperation.Key] = valueStoreOperation.PreviousValue;
              if (this.views.Count != 0)
              {
                this.UpdateViews(new KeyValueStoreOperation<TKey, TValue>(KeyValueStoreOpCode.ReplaceItem, valueStoreOperation.Key, valueStoreOperation.PreviousValue, valueStoreOperation.Value));
                break;
              }
              break;
            case KeyValueStoreOpCode.RemoveItem:
              this.valueTable.Add(valueStoreOperation.Key, valueStoreOperation.Value);
              if (this.views.Count != 0)
              {
                this.UpdateViews(new KeyValueStoreOperation<TKey, TValue>(KeyValueStoreOpCode.AppendItem, valueStoreOperation.Key, valueStoreOperation.Value));
                break;
              }
              break;
          }
        }
      }

      private void ResetTransactionResourcesOnly()
      {
        if (this.historyLog.Count == 0)
          return;
        this.historyLog.Clear();
      }

      private void UpdateReplicas()
      {
        KeyValueStoreOperation<TKey, TValue>[] valueStoreOperationArray = new KeyValueStoreOperation<TKey, TValue>[this.historyLog.Count];
        this.historyLog.CopyTo(valueStoreOperationArray, 0);
        CommitedTransactionData<TKey, TValue> transactionData = new CommitedTransactionData<TKey, TValue>(this.contentVersion, (IList<KeyValueStoreOperation<TKey, TValue>>) valueStoreOperationArray);
        try
        {
          List<IKeyValueStoreReplica<TKey, TValue>> replicas = this.replicas;
          if (replicas.Count == 1)
          {
            replicas[0].UpdateData(transactionData);
          }
          else
          {
            for (int index = 0; index < replicas.Count; ++index)
              replicas[index].UpdateData(transactionData);
          }
        }
        catch
        {
          this.SetDataIsCorrupted();
          throw;
        }
      }

      private void InitializeViews()
      {
        for (int index = 0; index < this.views.Count; ++index)
          this.views[index].Initialize((IRwlQuerySynchronizer) this.txManager);
      }

      private void ClearViews()
      {
        try
        {
          List<InMemoryKeyValueStoreView<TKey, TValue>> views = this.views;
          if (views.Count == 1)
          {
            views[0].ClearData();
          }
          else
          {
            for (int index = 0; index < views.Count; ++index)
              views[index].ClearData();
          }
        }
        catch
        {
          this.SetDataIsCorrupted();
          throw;
        }
      }

      private void UpdateViews(KeyValueStoreOperation<TKey, TValue> operation)
      {
        try
        {
          List<InMemoryKeyValueStoreView<TKey, TValue>> views = this.views;
          if (views.Count == 1)
          {
            views[0].UpdateData(operation);
          }
          else
          {
            for (int index = 0; index < views.Count; ++index)
              views[index].UpdateData(operation);
          }
        }
        catch
        {
          this.SetDataIsCorrupted();
          throw;
        }
      }

      public bool InTransaction
      {
        [DebuggerStepThrough] get
        {
          this.CheckNotDisposed();
          return this.txManager.InTransaction();
        }
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      private void CheckDataIsNotCorrupted()
      {
        if (this.valueTableCorrupted)
          throw this.CreateDataCorruptedException();
      }

      private InvalidOperationException CreateDataCorruptedException()
      {
        return new InvalidOperationException("Хранилище повреждено и не может больше использоваться.");
      }

      private void SetDataIsCorrupted()
      {
        if (this.valueTableCorrupted)
          return;
        this.valueTableCorrupted = true;
      }
    }
}
