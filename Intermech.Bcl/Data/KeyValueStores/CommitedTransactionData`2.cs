
// Type: Intermech.Data.KeyValueStores.CommitedTransactionData`2
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Data.KeyValueStores
{
    public struct CommitedTransactionData<TKey, TValue>(
      int contentVersion,
      IList<KeyValueStoreOperation<TKey, TValue>> operations)
      where TKey : IEquatable<TKey>
    {
      private int contentVersion = contentVersion;
      private IList<KeyValueStoreOperation<TKey, TValue>> operations = operations;

      /// <summary>Возвращает новую версию содержимого хранилища.</summary>
      public int ContentVersion
      {
        [DebuggerStepThrough] get => this.contentVersion;
      }

      /// <summary>
      /// Возвращает список операций, выполненных в рамках транзакции.
      /// </summary>
      public IList<KeyValueStoreOperation<TKey, TValue>> Operations
      {
        [DebuggerStepThrough] get => this.operations;
      }
    }
}
