
// Type: Intermech.Data.KeyValueStores.IKeyValueStoreReplica`2
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Data.KeyValueStores
{
    public interface IKeyValueStoreReplica<TKey, TValue> : IKeyValueContentVersion where TKey : IEquatable<TKey>
    {
      void Attach(IKeyValueStore<TKey, TValue> store);

      void Detach();

      bool IsAttached { get; }

      /// <summary>
      /// Выполняет обновление содержимого реплики в конце транзакции.
      /// Метод вызывается из процесса фиксации транзакции и не должен бросать исключений.
      /// </summary>
      /// <param name="transactionData">Зафиксированная транзакция</param>
      void UpdateData(
        CommitedTransactionData<TKey, TValue> transactionData);
    }
}
