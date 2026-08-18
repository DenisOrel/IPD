
// Type: Intermech.Data.KeyValueStores.BackupReplica`2
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Data.KeyValueStores
{
    /// <summary>
    /// Базовый класс для реплики, предназначенной только для создания резервной копии основного хранилища. Одновременно реплика
    /// может находиться либо в режиме чтения данных из реплики, либо в режиме обновления данных. Если реплика не подключена к
    /// хранилищу, то она находится в режиме чтения и может быть использована для восстановления содержимого хранилища. Если
    /// реплика подключена к хранилищу, то она находится в режиме записи и может быть использована только для обновления резервной копии.
    /// </summary>
    /// <typeparam name="TKey">Тип ключей в хранилище</typeparam>
    /// <typeparam name="TValue">Тип значений в хранилище</typeparam>
    public abstract class BackupReplica<TKey, TValue> : 
      IKeyValueStoreReplica<TKey, TValue>,
      IKeyValueContentVersion,
      IKeyValueDataCursor<TKey, TValue>
      where TKey : IEquatable<TKey>
    {
      private const int FlushTimeout = 60000;
      private bool isInitialized;
      private bool isAttached;
      private BackupReplicaBackgroundTxProcessor<CommitedTransactionData<TKey, TValue>> bgOperations;

      protected BackupReplica()
      {
        this.bgOperations = new BackupReplicaBackgroundTxProcessor<CommitedTransactionData<TKey, TValue>>(25, new Action<IList<CommitedTransactionData<TKey, TValue>>>(this.DoUpdateData), new Action<IList<CommitedTransactionData<TKey, TValue>>, Exception>(this.DoHandleUpdateError));
      }

      public BackupReplicaMode Mode
      {
        [DebuggerStepThrough] get
        {
          return !this.IsAttached ? BackupReplicaMode.ReadOnly : BackupReplicaMode.WriteOnly;
        }
      }

      protected void LazyInitialize()
      {
        if (this.isInitialized || this.isInitialized)
          return;
        this.DoInitialize();
        this.isInitialized = true;
      }

      protected virtual void DoInitialize()
      {
      }

      protected bool IsInitialized
      {
        [DebuggerStepThrough] get => this.isInitialized;
      }

      public void Flush()
      {
        this.LazyInitialize();
        this.bgOperations.WaitHandle.Wait(60000);
      }

      /// <summary>Возвращает версию содержимого хранилища.</summary>
      public int ContentVersion
      {
        [DebuggerStepThrough] get
        {
          this.LazyInitialize();
          return this.GetContentVersion();
        }
      }

      protected abstract int GetContentVersion();

      public void ScanData(Action<KeyValuePair<TKey, TValue>> action)
      {
        if (action == null)
          throw new ArgumentNullException(nameof (action));
        this.LazyInitialize();
        this.CheckNotAttached();
        this.DoScanData(action);
      }

      protected abstract void DoScanData(Action<KeyValuePair<TKey, TValue>> action);

      public void Attach(IKeyValueStore<TKey, TValue> store)
      {
        if (store == null)
          throw new ArgumentNullException(nameof (store));
        if (this.isAttached)
          throw new InvalidOperationException("Реплика уже была подключена к хранилищу.");
        this.LazyInitialize();
        this.DoAttach(store);
        this.isAttached = true;
      }

      public void Detach()
      {
        if (!this.isAttached)
          return;
        this.DoDetach();
        this.isAttached = false;
        this.Flush();
      }

      public bool IsAttached
      {
        [DebuggerStepThrough] get => this.isAttached;
      }

      protected void CheckNotAttached()
      {
        if (this.IsAttached)
          throw new InvalidOperationException("Реплика не должна быть подключена к хранилищу.");
      }

      protected void CheckAttached()
      {
        if (!this.IsAttached)
          throw new InvalidOperationException("Реплика должна быть подключена к хранилищу.");
      }

      protected virtual void DoAttach(IKeyValueStore<TKey, TValue> store)
      {
      }

      protected virtual void DoDetach()
      {
      }

      /// <summary>
      /// Выполняет обновление содержимого реплики в конце транзакции.
      /// Метод вызывается из процесса фиксации транзакции и не должен бросать исключений.
      /// </summary>
      /// <param name="transactionData">Зафиксированная транзакция</param>
      public void UpdateData(
        CommitedTransactionData<TKey, TValue> transactionData)
      {
        this.bgOperations.Add(transactionData);
      }

      /// <summary>
      /// Выполняет обновление содержимого реплики.
      /// Метод вызывается асинхронно из фонового потока после того, как транзакция была успешно зафиксирована.
      /// </summary>
      /// <param name="transactions">Список транзакций, примененных к хранилищу</param>
      protected abstract void DoUpdateData(
        IList<CommitedTransactionData<TKey, TValue>> transactions);

      /// <summary>
      /// Обрабатывает ошибку обновления реплики.
      /// Метод вызывается асинхронно из фонового потока после того, как транзакция была успешно зафиксирована.
      /// Метод не должен бросать исключений.
      /// </summary>
      /// <param name="transactions">Список журналов транзакций, примененных к хранилищу</param>
      /// <param name="exception">Необработанное исключение при обновлении содержимого реплики</param>
      protected virtual void DoHandleUpdateError(
        IList<CommitedTransactionData<TKey, TValue>> transactions,
        Exception exception)
      {
      }
    }
}
