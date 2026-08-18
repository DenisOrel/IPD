// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Server.ArchiveStorageIDService
// Assembly: Intermech.Archives.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2799C6CB-9B1D-4DB5-A12D-8C5FBFCAD6E5
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Archives.Server.dll

using Intermech.Archives.Common;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using System;
using System.Collections.Concurrent;
using System.Data;

#nullable disable
namespace Intermech.Archives.Server;

internal class ArchiveStorageIDService
{
  private IBlobStoragesPool _StoragesPool;
  private int _StorageAttrID;
  private ConcurrentDictionary<long, long> _StoragesCache;

  public ArchiveStorageIDService(IUserSession session)
  {
    this._StoragesPool = ServerServices.GetService(typeof (IBlobStoragesPool)) as IBlobStoragesPool;
    this._StoragesPool.GetStorageIDEvent += new GetStorageIDHandler(this.getStorageIDEvent);
    this._StorageAttrID = session.IdentHelper.GetAttributeID("cad0005c-306c-11d8-b4e9-00304f19f545");
    this.ReloadCache(session);
    if (!(ServerServices.GetService(typeof (IEventLogHelper)) is IEventLogHelper service))
      return;
    service.AfterCacheReload += new Intermech.Interfaces.Server.CacheReloadHandler(this.CacheReloadHandler);
  }

  public int StorageAttrID => this._StorageAttrID;

  private void CacheReloadHandler(IDbManager db)
  {
    IUserSession sessionTemporaryClone = (ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone("ArchivesServer.CacheReloadHandler");
    try
    {
      this.ReloadCache(sessionTemporaryClone);
    }
    finally
    {
      sessionTemporaryClone.Logout("ArchivesServer.CacheReloadHandler");
    }
  }

  private void ReloadCache(IUserSession session)
  {
    ConcurrentDictionary<long, long> concurrentDictionary = new ConcurrentDictionary<long, long>();
    DataTable dataTable = session.GetObjectCollection(ConstsHolder.ArcTypeID).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(this._StorageAttrID, RelationalOperators.Greater, (object) 0, LogicalOperators.NONE, 0, false)
      {
        Content = ColumnContents.ID
      }
    }, new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) -2),
      new ColumnDescriptor((object) this._StorageAttrID, ColumnContents.ID, ColumnNameMapping.Index, SortOrders.NONE, 0)
    }, lastOrderValue: (object) 0));
    for (int index = 0; index < dataTable.Rows.Count; ++index)
      concurrentDictionary.TryAdd(Convert.ToInt64(dataTable.Rows[index][0]), Convert.ToInt64(dataTable.Rows[index][1]));
    this._StoragesCache = concurrentDictionary;
  }

  private void getStorageIDEvent(GetStorageIDEventArgs args)
  {
    if (args.ParentObject == null || !MetaDataHelper.IsObjectTypeChildOf(args.ParentObject.ObjectType, ConstsHolder.DocTypeID))
      return;
    IDBAttribute attributeById = args.ParentObject.GetAttributeByID(ConstsHolder.ArchiveAttrID);
    long num;
    if (attributeById == null || attributeById.IsNull || !this._StoragesCache.TryGetValue(attributeById.AsInteger, out num))
      return;
    args.StorageID = num;
  }

  public long GetStorageID(long archiveID)
  {
    long num;
    return this._StoragesCache.TryGetValue(archiveID, out num) ? num : 0L;
  }

  internal void SetStorageID(long archiveID, long storageID)
  {
    this._StoragesCache[archiveID] = storageID;
  }

  internal void ClearStorageID(long archiveID)
  {
    this._StoragesCache.TryRemove(archiveID, out long _);
  }
}
