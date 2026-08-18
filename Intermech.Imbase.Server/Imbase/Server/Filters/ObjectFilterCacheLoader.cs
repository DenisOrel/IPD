// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Filters.ObjectFilterCacheLoader
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Collections;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase.Filters;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Server.Filters;

internal class ObjectFilterCacheLoader
{
  protected IUserSession _session;
  protected readonly ObjFilterCache _objCache;
  protected bool _loadFilterData;

  private IEnumerable<ObjFilterCacheItem> GetFilterData()
  {
    List<ObjFilterCacheItem> filterData1 = (List<ObjFilterCacheItem>) new HashedList<ObjFilterCacheItem>();
    if (this._session == null)
      return (IEnumerable<ObjFilterCacheItem>) filterData1;
    if (Intermech.Imbase.Consts.ImbaseObjFilterTypeID == -1)
      return (IEnumerable<ObjFilterCacheItem>) filterData1;
    DataTable dataTable = (DataTable) null;
    DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new List<ColumnDescriptor>()
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0),
      new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad001a0-306c-11d8-b4e9-00304f19f545"), ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0),
      new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad0004b-306c-11d8-b4e9-00304f19f545"), ColumnContents.Value, ColumnNameMapping.Guid, SortOrders.NONE, 0),
      new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseFilterOwnerAttrGuid), ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0)
    }.ToArray());
    IDBObjectCollection objectCollection = this._session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseObjFilterTypeID);
    if (objectCollection != null)
      dataTable = objectCollection.Select(paramSet);
    if (dataTable == null)
      return (IEnumerable<ObjFilterCacheItem>) filterData1;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      if (row != null)
      {
        long int64 = Convert.ToInt64(row[0]);
        string caption = row[1].ToString();
        string str = row[2] != DBNull.Value ? row[2].ToString() : string.Empty;
        string owner = row[4] != DBNull.Value ? row[4].ToString() : string.Empty;
        Guid objTypeGuid = Guid.Empty;
        if (str != string.Empty && GuidHelper.IsGuid(str))
          objTypeGuid = new Guid(str);
        ImbaseObjFilterInfo info = new ImbaseObjFilterInfo(int64, MetaDataHelper.GetObjectTypeID(objTypeGuid), caption, owner);
        ImbaseObjFilterData filterData2 = (ImbaseObjFilterData) null;
        if (this._loadFilterData)
          ImbaseObjFilterDataHelper.LoadFilterData(int64, this._session, out filterData2);
        ImbaseObjFilterData data = filterData2;
        ObjFilterCacheItem objFilterCacheItem = new ObjFilterCacheItem(info, data);
        filterData1.Add(objFilterCacheItem);
      }
    }
    return (IEnumerable<ObjFilterCacheItem>) filterData1;
  }

  protected virtual void LoadCacheData()
  {
    if (this._session == null)
      return;
    try
    {
      this._objCache.Load(this.GetFilterData());
    }
    finally
    {
      this._session.Logout(nameof (ObjectFilterCacheLoader));
      this._session = (IUserSession) null;
    }
  }

  public ObjectFilterCacheLoader(ObjFilterCache objCache) => this._objCache = objCache;

  public void Execute(bool loadFilterData)
  {
    this._loadFilterData = loadFilterData;
    this._session = ServiceUtils.GetService<IDBTimedEvents>((object) ServerServices.ServiceContainer, true).GetSystemSessionTemporaryClone(nameof (ObjectFilterCacheLoader));
    this.LoadCacheData();
  }
}
