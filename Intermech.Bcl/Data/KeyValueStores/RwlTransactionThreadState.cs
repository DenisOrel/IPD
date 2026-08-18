
// Type: Intermech.Data.KeyValueStores.RwlTransactionThreadState
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Diagnostics;


namespace Intermech.Data.KeyValueStores
{
    internal sealed class RwlTransactionThreadState : ICommitableObjectThreadState
    {
      private RwlTransaction transaction;
      private bool canCommit;

      public RwlTransactionThreadState(RwlTransaction transaction)
      {
        this.transaction = transaction;
        this.canCommit = true;
      }

      public RwlTransaction Transaction
      {
        [DebuggerStepThrough] get => this.transaction;
      }

      public ICommitableObject CommitableObject
      {
        [DebuggerStepThrough] get => (ICommitableObject) this.transaction;
      }

      /// <summary>
      /// Возвращает или задает признак, что фиксация транзакции разрешена.
      /// </summary>
      /// <remarks>
      /// Свойство используется вложенными областями видимости транзакции для запрета фиксации всей транзакции в случае,
      /// если вложенная область видимости не смогла подтвердить свое успешное завершение.
      /// </remarks>
      public bool CanCommit
      {
        [DebuggerStepThrough] get => this.canCommit;
        [DebuggerStepThrough] set => this.canCommit = value;
      }
    }
}
