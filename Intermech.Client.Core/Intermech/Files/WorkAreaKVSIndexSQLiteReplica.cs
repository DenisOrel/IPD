
// Type: Intermech.Files.WorkAreaKVSIndexSQLiteReplica
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ControlFlow;
using Intermech.Data;
using Intermech.Data.KeyValueStores;
using Intermech.Diagnostics;
using System;
using System.Collections.Generic;


namespace Intermech.Files;

internal sealed class WorkAreaKVSIndexSQLiteReplica : 
  BackupReplica<long, WorkAreaIndexDBObjectRecord>
{
  private WorkAreaSQLiteIndexFile sqliteIndexFile;
  private IEventLogWriter eventLogWriter;
  private int contentVersion;

  public WorkAreaKVSIndexSQLiteReplica(
    WorkAreaSQLiteIndexFile sqliteIndexFile,
    IEventLogWriter eventLogWriter)
  {
    if (sqliteIndexFile == null)
      throw new ArgumentNullException(nameof (sqliteIndexFile));
    if (eventLogWriter == null)
      throw new ArgumentNullException(nameof (eventLogWriter));
    this.sqliteIndexFile = sqliteIndexFile;
    this.eventLogWriter = eventLogWriter;
    this.contentVersion = 0;
  }

  protected override int GetContentVersion() => this.contentVersion;

  protected override void DoScanData(
    Action<KeyValuePair<long, WorkAreaIndexDBObjectRecord>> action)
  {
    this.sqliteIndexFile.CreateDbContext().ObjectStates.ScanRecords((Action<WorkAreaIndexDBObjectRecord>) (record => action(new KeyValuePair<long, WorkAreaIndexDBObjectRecord>(record.ObjectState.Id, record))));
  }

  /// <summary>
  /// Выполняет обновление содержимого реплики.
  /// Метод вызывается асинхронно из фонового потока после того, как транзакция была успешно зафиксирована.
  /// </summary>
  /// <param name="transactions">Список транзакций, примененных к хранилищу</param>
  protected override void DoUpdateData(
    IList<CommitedTransactionData<long, WorkAreaIndexDBObjectRecord>> transactions)
  {
    WorkAreaSQLiteIndexDaoContext dbContext = this.sqliteIndexFile.CreateDbContext();
    using (new DynamicScope())
    {
      DataScope.OpenConnection(dbContext.ConnectionPool);
      DataScope.BeginTransaction();
      foreach (CommitedTransactionData<long, WorkAreaIndexDBObjectRecord> transaction in (IEnumerable<CommitedTransactionData<long, WorkAreaIndexDBObjectRecord>>) transactions)
      {
        foreach (KeyValueStoreOperation<long, WorkAreaIndexDBObjectRecord> operation in (IEnumerable<KeyValueStoreOperation<long, WorkAreaIndexDBObjectRecord>>) transaction.Operations)
        {
          switch (operation.OpCode)
          {
            case KeyValueStoreOpCode.AppendItem:
              dbContext.ObjectStates.Append(operation.Value);
              continue;
            case KeyValueStoreOpCode.ReplaceItem:
              dbContext.ObjectStates.Update(operation.Value);
              continue;
            case KeyValueStoreOpCode.RemoveItem:
              dbContext.ObjectStates.Remove(operation.Value);
              continue;
            default:
              throw new NotSupportedEnumException((Enum) operation.OpCode);
          }
        }
      }
      DataScope.Commit();
    }
    this.contentVersion = transactions[transactions.Count - 1].ContentVersion;
  }

  /// <summary>
  /// Обрабатывает ошибку обновления реплики.
  /// Метод вызывается асинхронно из фонового потока после того, как транзакция была успешно зафиксирована.
  /// Метод не должен бросать исключений.
  /// </summary>
  /// <param name="transactions">Список журналов транзакций, примененных к хранилищу</param>
  /// <param name="exception">Необработанное исключение при обновлении содержимого реплики</param>
  protected override void DoHandleUpdateError(
    IList<CommitedTransactionData<long, WorkAreaIndexDBObjectRecord>> transactions,
    Exception exception)
  {
    base.DoHandleUpdateError(transactions, exception);
    this.eventLogWriter.Write(ExceptionServices.GetExtendedExceptionText(exception, "При записи индекса рабочей области файлового хранилища произошла ошибка."), EventLogItemType.Error);
  }
}
