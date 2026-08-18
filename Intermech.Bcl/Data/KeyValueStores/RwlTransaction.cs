
// Type: Intermech.Data.KeyValueStores.RwlTransaction
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;


namespace Intermech.Data.KeyValueStores
{
    internal sealed class RwlTransaction : ICommitableObject
    {
      private IRwlCommitRollbackManager manager;
      private bool canWrite;
      private bool isEnded;

      public RwlTransaction(IRwlCommitRollbackManager manager, bool canWrite)
      {
        this.manager = manager;
        this.canWrite = canWrite;
      }

      /// <summary>
      /// Возвращает признак, что это транзакция поддерживает модификацию данных.
      /// </summary>
      public bool CanWrite
      {
        [DebuggerStepThrough] get => this.canWrite;
      }

      public void Commit()
      {
        if (this.isEnded)
          throw this.TransactionIsAlreadyEndedException();
        this.manager.CommitTransaction(this);
        this.isEnded = true;
      }

      public void Rollback()
      {
        if (this.isEnded)
          throw this.TransactionIsAlreadyEndedException();
        this.manager.RollbackTransaction(this);
        this.isEnded = true;
      }

      private InvalidOperationException TransactionIsAlreadyEndedException()
      {
        return new InvalidOperationException(RwlTransactionResources.SR_TransactionIsAlreadyEnded);
      }
    }
}
