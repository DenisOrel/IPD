
// Type: Intermech.Files.DBObjectStateByObjectIdIndex
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Data.KeyValueStores;
using System.Collections.Generic;


namespace Intermech.Files;

internal sealed class DBObjectStateByObjectIdIndex : 
  InMemoryKeyValueStoreView<long, WorkAreaIndexDBObjectRecord>
{
  private Dictionary<long, WorkAreaIndexDBObjectRecord> searchTable;

  public DBObjectStateByObjectIdIndex()
  {
    this.searchTable = new Dictionary<long, WorkAreaIndexDBObjectRecord>();
  }

  /// <summary>Удаляет все данные представления.</summary>
  protected override void DoClearData() => this.searchTable.Clear();

  /// <summary>
  /// Обновляет представление синхронно с основным хранилищем.
  /// Метод вызывается из процесса модификации содержимого основного хранилища и не должен бросать исключений.
  /// </summary>
  /// <param name="operation">Выполненная операция модификации содержимого основного хранилища</param>
  protected override void DoUpdateData(
    KeyValueStoreOperation<long, WorkAreaIndexDBObjectRecord> operation)
  {
    switch (operation.OpCode)
    {
      case KeyValueStoreOpCode.AppendItem:
        this.searchTable.Add(operation.Value.ObjectState.ObjectId, operation.Value);
        break;
      case KeyValueStoreOpCode.ReplaceItem:
        if (operation.PreviousValue.ObjectState.ObjectId != operation.Value.ObjectState.ObjectId)
          this.searchTable.Remove(operation.PreviousValue.ObjectState.ObjectId);
        this.searchTable[operation.Value.ObjectState.ObjectId] = operation.Value;
        break;
      case KeyValueStoreOpCode.RemoveItem:
        this.searchTable.Remove(operation.Value.ObjectState.ObjectId);
        break;
    }
  }

  public bool ContainsKey(long objectId)
  {
    this.CheckInitialized();
    using (this.QuerySynchronizer.BeginQueryScope())
      return this.searchTable.ContainsKey(objectId);
  }

  public WorkAreaIndexDBObjectRecord TryGetByKey(long objectId)
  {
    this.CheckInitialized();
    using (this.QuerySynchronizer.BeginQueryScope())
    {
      WorkAreaIndexDBObjectRecord indexDbObjectRecord;
      return this.searchTable.TryGetValue(objectId, out indexDbObjectRecord) ? indexDbObjectRecord : (WorkAreaIndexDBObjectRecord) null;
    }
  }
}
