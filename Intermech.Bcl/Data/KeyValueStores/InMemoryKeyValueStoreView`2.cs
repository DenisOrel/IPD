
// Type: Intermech.Data.KeyValueStores.InMemoryKeyValueStoreView`2
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace Intermech.Data.KeyValueStores
{
    public abstract class InMemoryKeyValueStoreView<TKey, TValue> where TKey : IEquatable<TKey>
    {
      private IRwlQuerySynchronizer querySynchronizer;
      private bool isInitialized;

      public void Initialize(IRwlQuerySynchronizer querySynchronizer)
      {
        if (querySynchronizer == null)
          throw new ArgumentNullException(nameof (querySynchronizer));
        if (this.isInitialized)
          throw new InvalidOperationException($"Объект '{this.GetType().FullName}' уже был инициализирован.");
        this.querySynchronizer = querySynchronizer;
        this.isInitialized = true;
      }

      public bool IsInitialized
      {
        [DebuggerStepThrough] get => this.isInitialized;
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      protected void CheckInitialized()
      {
        if (!this.isInitialized)
          throw this.CreateNotInitializedException();
      }

      private InvalidOperationException CreateNotInitializedException()
      {
        return new InvalidOperationException($"Объект '{this.GetType().FullName}' должен быть предварительно инициализирован.");
      }

      protected IRwlQuerySynchronizer QuerySynchronizer
      {
        [DebuggerStepThrough] get => this.querySynchronizer;
      }

      /// <summary>Удаляет все данные представления.</summary>
      internal void ClearData() => this.DoClearData();

      /// <summary>Удаляет все данные представления.</summary>
      protected abstract void DoClearData();

      /// <summary>
      /// Обновляет представление синхронно с основным хранилищем.
      /// Метод вызывается из процесса модификации содержимого основного хранилища и не должен бросать исключений.
      /// </summary>
      /// <param name="operation">Выполненная операция модификации содержимого основного хранилища</param>
      internal void UpdateData(KeyValueStoreOperation<TKey, TValue> operation)
      {
        this.DoUpdateData(operation);
      }

      /// <summary>
      /// Обновляет представление синхронно с основным хранилищем.
      /// Метод вызывается из процесса модификации содержимого основного хранилища и не должен бросать исключений.
      /// </summary>
      /// <param name="operation">Выполненная операция модификации содержимого основного хранилища</param>
      protected abstract void DoUpdateData(KeyValueStoreOperation<TKey, TValue> operation);
    }
}
