
// Type: Intermech.Files.WorkAreaKVSIndex
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Data;
using Intermech.Data.KeyValueStores;
using System;
using System.Collections.Generic;


namespace Intermech.Files;

internal sealed class WorkAreaKVSIndex : IWorkAreaIndex
{
  private DBObjectStateByObjectIdIndex objectIdIndex;
  private InMemoryKeyValueStoreParameters<long, WorkAreaIndexDBObjectRecord> storeParams;
  private InMemoryKeyValueStore<long, WorkAreaIndexDBObjectRecord> store;
  private BackupReplica<long, WorkAreaIndexDBObjectRecord> persistentReplica;

  public WorkAreaKVSIndex(
    BackupReplica<long, WorkAreaIndexDBObjectRecord> persistentReplica)
  {
    if (persistentReplica == null)
      throw new ArgumentNullException(nameof (persistentReplica));
    this.objectIdIndex = new DBObjectStateByObjectIdIndex();
    this.storeParams = new InMemoryKeyValueStoreParameters<long, WorkAreaIndexDBObjectRecord>();
    this.storeParams.Views.Add((InMemoryKeyValueStoreView<long, WorkAreaIndexDBObjectRecord>) this.objectIdIndex);
    this.store = new InMemoryKeyValueStore<long, WorkAreaIndexDBObjectRecord>(this.storeParams);
    this.persistentReplica = persistentReplica;
    this.store.LoadData((IKeyValueDataCursor<long, WorkAreaIndexDBObjectRecord>) this.persistentReplica);
    this.store.RegisterReplica((IKeyValueStoreReplica<long, WorkAreaIndexDBObjectRecord>) this.persistentReplica);
  }

  public void Append(DBObjectState objectState)
  {
    if (objectState == null)
      throw new ArgumentNullException(nameof (objectState));
    this.store.Add(objectState.Id, new WorkAreaIndexDBObjectRecord(objectState, DateTime.UtcNow));
  }

  public void Remove(DBObjectState objectState)
  {
    if (objectState == null)
      throw new ArgumentNullException(nameof (objectState));
    this.store.Remove(objectState.Id);
  }

  public void Update(DBObjectState objectState)
  {
    if (objectState == null)
      throw new ArgumentNullException(nameof (objectState));
    this.store.Update(objectState.Id, new WorkAreaIndexDBObjectRecord(objectState, DateTime.UtcNow));
  }

  public void BatchAppend(ICollection<DBObjectState> list)
  {
    if (list == null)
      throw new ArgumentNullException(nameof (list));
    if (list.Count == 0)
      return;
    DateTime utcNow = DateTime.UtcNow;
    using (CommitableObjectScope commitableObjectScope = this.store.BeginTransactionScope(true))
    {
      foreach (DBObjectState objectState in (IEnumerable<DBObjectState>) list)
        this.store.Add(objectState.Id, new WorkAreaIndexDBObjectRecord(objectState, utcNow));
      commitableObjectScope.Complete();
    }
  }

  public void BatchRemove(ICollection<DBObjectState> list)
  {
    if (list == null)
      throw new ArgumentNullException(nameof (list));
    if (list.Count == 0)
      return;
    using (CommitableObjectScope commitableObjectScope = this.store.BeginTransactionScope(true))
    {
      foreach (DBObjectState dbObjectState in (IEnumerable<DBObjectState>) list)
        this.store.Remove(dbObjectState.Id);
      commitableObjectScope.Complete();
    }
  }

  public void BatchUpdate(
    ICollection<DBObjectState> updateList,
    ICollection<DBObjectState> appendList)
  {
    if (updateList == null)
      throw new ArgumentNullException(nameof (updateList));
    if (appendList == null)
      throw new ArgumentNullException(nameof (appendList));
    if (updateList.Count == 0 && appendList.Count == 0)
      return;
    DateTime utcNow = DateTime.UtcNow;
    using (CommitableObjectScope commitableObjectScope = this.store.BeginTransactionScope(true))
    {
      foreach (DBObjectState update in (IEnumerable<DBObjectState>) updateList)
        this.store.Update(update.Id, new WorkAreaIndexDBObjectRecord(update, utcNow));
      foreach (DBObjectState append in (IEnumerable<DBObjectState>) appendList)
        this.store.Add(append.Id, new WorkAreaIndexDBObjectRecord(append, utcNow));
      commitableObjectScope.Complete();
    }
  }

  public bool Contains(long objectId) => this.objectIdIndex.ContainsKey(objectId);

  public DBObjectState Find(long id) => this.store.TryGetByKey(id)?.ObjectState;

  public DBObjectState FindByVersionId(long objectId)
  {
    return this.objectIdIndex.TryGetByKey(objectId)?.ObjectState;
  }

  public DateTime? GetPublishTime(long objectId)
  {
    return this.objectIdIndex.TryGetByKey(objectId)?.LastUsedTime;
  }

  public List<DBObjectState> Query()
  {
    return this.store.GetAll().ConvertAll<DBObjectState>((Converter<WorkAreaIndexDBObjectRecord, DBObjectState>) (record => record.ObjectState));
  }

  public List<DBObjectState> QueryNotUsed(DateTime noUseSinceDate)
  {
    List<WorkAreaIndexDBObjectRecord> all = this.store.GetAll();
    List<DBObjectState> dbObjectStateList = new List<DBObjectState>(all.Count);
    foreach (WorkAreaIndexDBObjectRecord indexDbObjectRecord in all)
    {
      if (indexDbObjectRecord.LastUsedTime < noUseSinceDate)
        dbObjectStateList.Add(indexDbObjectRecord.ObjectState);
    }
    return dbObjectStateList;
  }

  public void Flush() => this.persistentReplica.Flush();
}
