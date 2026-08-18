// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.TaskDataCache
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Expert;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Expert.Server;

public class TaskDataCache
{
  protected Dictionary<long, TaskDataCache.ObjDataItem> _objDataCache;
  protected Dictionary<long, TaskDataCache.RelDataItem> _relDataCache;
  protected Dictionary<TaskDataCache.ColumnsMode, List<ColumnDescriptor>> _mode2DataObjColumns;
  protected Dictionary<TaskDataCache.ColumnsMode, List<ColumnDescriptor>> _mode2DataRelColumns;

  protected void FillObjCacheData(HybridTableExp ht)
  {
    lock (this._objDataCache)
    {
      this._objDataCache.Clear();
      if (ht == null || ht.RowsCount == 0)
        return;
      int indexByName1 = ht.Columns.GetIndexByName("F_OBJECT_ID");
      if (indexByName1 == -1)
      {
        Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(-2);
        indexByName1 = ht.Columns.GetIndexByName(attributeTypeGuid.ToString());
      }
      int indexByName2 = ht.Columns.GetIndexByName("F_OBJECT_TYPE");
      if (indexByName2 == -1)
      {
        Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(-7);
        indexByName2 = ht.Columns.GetIndexByName(attributeTypeGuid.ToString());
      }
      int indexByName3 = ht.Columns.GetIndexByName("F_ID");
      if (indexByName3 == -1)
      {
        Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(-3);
        indexByName3 = ht.Columns.GetIndexByName(attributeTypeGuid.ToString());
      }
      if (indexByName1 == -1 || indexByName2 == -1 || indexByName3 == -1)
        return;
      int indexByName4 = ht.Columns.GetIndexByName("F_GUID");
      if (indexByName4 == -1)
      {
        Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(-12);
        indexByName4 = ht.Columns.GetIndexByName(attributeTypeGuid.ToString());
      }
      int indexByName5 = ht.Columns.GetIndexByName("CAPTION");
      if (indexByName5 == -1)
      {
        Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(-50);
        indexByName5 = ht.Columns.GetIndexByName(attributeTypeGuid.ToString());
      }
      for (int index = 0; index < ht.RowsCount; ++index)
      {
        HybridRowExp hybridRowExp = ht[index];
        if (hybridRowExp != null)
        {
          long int64 = Convert.ToInt64(hybridRowExp[indexByName1]);
          if (!this._objDataCache.ContainsKey(int64))
          {
            Guid result = Guid.Empty;
            if (indexByName4 != -1)
              Guid.TryParse(Convert.ToString(hybridRowExp[indexByName4]), out result);
            TaskDataCache.ObjDataItem objDataItem = new TaskDataCache.ObjDataItem(int64, Convert.ToInt32(hybridRowExp[indexByName2]), Convert.ToInt64(hybridRowExp[indexByName3]), result, indexByName5 != -1 ? Convert.ToString(hybridRowExp[indexByName5]) : (string) null);
            this._objDataCache.Add(int64, objDataItem);
          }
        }
      }
    }
  }

  protected void FillObjCacheData(DataTable dataTable)
  {
    lock (this._objDataCache)
    {
      this._objDataCache.Clear();
      if (dataTable == null || dataTable.Rows.Count == 0)
        return;
      int columnIndex1 = dataTable.Columns.IndexOf("F_OBJECT_ID");
      if (columnIndex1 == -1)
      {
        Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(-2);
        columnIndex1 = dataTable.Columns.IndexOf(attributeTypeGuid.ToString());
      }
      int columnIndex2 = dataTable.Columns.IndexOf("F_OBJECT_TYPE");
      if (columnIndex2 == -1)
      {
        Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(-7);
        columnIndex2 = dataTable.Columns.IndexOf(attributeTypeGuid.ToString());
      }
      int columnIndex3 = dataTable.Columns.IndexOf("F_ID");
      if (columnIndex3 == -1)
      {
        Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(-3);
        columnIndex3 = dataTable.Columns.IndexOf(attributeTypeGuid.ToString());
      }
      if (columnIndex1 == -1 || columnIndex2 == -1 || columnIndex3 == -1)
        return;
      int columnIndex4 = dataTable.Columns.IndexOf("F_GUID");
      if (columnIndex4 == -1)
      {
        Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(-12);
        columnIndex4 = dataTable.Columns.IndexOf(attributeTypeGuid.ToString());
      }
      int columnIndex5 = dataTable.Columns.IndexOf("CAPTION");
      if (columnIndex5 == -1)
      {
        Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(-50);
        columnIndex5 = dataTable.Columns.IndexOf(attributeTypeGuid.ToString());
      }
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (row != null)
        {
          long int64 = Convert.ToInt64(row[columnIndex1]);
          if (!this._objDataCache.ContainsKey(int64))
          {
            Guid result = Guid.Empty;
            if (columnIndex4 != -1)
              Guid.TryParse(Convert.ToString(row[columnIndex4]), out result);
            TaskDataCache.ObjDataItem objDataItem = new TaskDataCache.ObjDataItem(int64, Convert.ToInt32(row[columnIndex2]), Convert.ToInt64(row[columnIndex3]), result, columnIndex5 != -1 ? Convert.ToString(row[columnIndex5]) : (string) null);
            this._objDataCache.Add(int64, objDataItem);
          }
        }
      }
    }
  }

  protected void FillRelCacheData(HybridTableExp ht)
  {
    lock (this._relDataCache)
    {
      this._relDataCache.Clear();
      if (ht == null || ht.RowsCount == 0)
        return;
      int indexByName1 = ht.Columns.GetIndexByName("F_PRJLINK_ID");
      if (indexByName1 == -1)
      {
        Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(-20);
        indexByName1 = ht.Columns.GetIndexByName(attributeTypeGuid.ToString());
      }
      int indexByName2 = ht.Columns.GetIndexByName("F_RELATION_TYPE");
      if (indexByName2 == -1)
      {
        Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(-23);
        indexByName2 = ht.Columns.GetIndexByName(attributeTypeGuid.ToString());
      }
      int indexByName3 = ht.Columns.GetIndexByName("F_PRJ_GUID");
      if (indexByName3 == -1)
      {
        Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(-26);
        indexByName3 = ht.Columns.GetIndexByName(attributeTypeGuid.ToString());
      }
      if (indexByName1 == -1 || indexByName2 == -1 || indexByName3 == -1)
        return;
      for (int index = 0; index < ht.RowsCount; ++index)
      {
        HybridRowExp hybridRowExp = ht[index];
        if (hybridRowExp != null)
        {
          long int64 = Convert.ToInt64(hybridRowExp[indexByName1]);
          if (!this._relDataCache.ContainsKey(int64))
          {
            TaskDataCache.RelDataItem relDataItem = new TaskDataCache.RelDataItem(int64, Convert.ToInt32(hybridRowExp[indexByName2]), new Guid(Convert.ToString(hybridRowExp[indexByName3])));
            this._relDataCache.Add(int64, relDataItem);
          }
        }
      }
    }
  }

  protected void FillRelCacheData(DataTable dataTable)
  {
    lock (this._relDataCache)
    {
      this._relDataCache.Clear();
      if (dataTable == null || dataTable.Rows.Count == 0)
        return;
      int columnIndex1 = dataTable.Columns.IndexOf("F_PRJLINK_ID");
      if (columnIndex1 == -1)
      {
        Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(-20);
        columnIndex1 = dataTable.Columns.IndexOf(attributeTypeGuid.ToString());
      }
      int columnIndex2 = dataTable.Columns.IndexOf("F_RELATION_TYPE");
      if (columnIndex2 == -1)
      {
        Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(-23);
        columnIndex2 = dataTable.Columns.IndexOf(attributeTypeGuid.ToString());
      }
      int columnIndex3 = dataTable.Columns.IndexOf("F_PRJ_GUID");
      if (columnIndex3 == -1)
      {
        Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(-26);
        columnIndex3 = dataTable.Columns.IndexOf(attributeTypeGuid.ToString());
      }
      if (columnIndex1 == -1 || columnIndex2 == -1 || columnIndex3 == -1)
        return;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (row != null)
        {
          long int64 = Convert.ToInt64(row[columnIndex1]);
          if (!this._relDataCache.ContainsKey(int64))
          {
            TaskDataCache.RelDataItem relDataItem = new TaskDataCache.RelDataItem(int64, Convert.ToInt32(row[columnIndex2]), new Guid(Convert.ToString(row[columnIndex3])));
            this._relDataCache.Add(int64, relDataItem);
          }
        }
      }
    }
  }

  public TaskDataCache()
  {
    this._objDataCache = new Dictionary<long, TaskDataCache.ObjDataItem>();
    this._relDataCache = new Dictionary<long, TaskDataCache.RelDataItem>();
    this._mode2DataObjColumns = new Dictionary<TaskDataCache.ColumnsMode, List<ColumnDescriptor>>();
    this._mode2DataRelColumns = new Dictionary<TaskDataCache.ColumnsMode, List<ColumnDescriptor>>();
  }

  public TaskDataCache.ObjDataItem GetObjData(long objectId, IUserSession session)
  {
    if (objectId == 0L || objectId == -1L)
      return (TaskDataCache.ObjDataItem) null;
    lock (this._objDataCache)
    {
      TaskDataCache.ObjDataItem objData;
      if (this._objDataCache.TryGetValue(objectId, out objData))
      {
        if (session != null && (TypedInfoItem) objData != (TypedInfoItem) null)
          objData.UpdateDataInfo(session);
        return objData;
      }
      if (session == null)
        return (TaskDataCache.ObjDataItem) null;
      QuickObjectInfo objectInfo = session.GetObjectInfo(objectId);
      if (!objectInfo.Empty)
        objData = new TaskDataCache.ObjDataItem(objectId, objectInfo.ObjectTypeID, objectInfo.ID, objectInfo.VersionGuid, objectInfo.Caption);
      this._objDataCache.Add(objectId, objData);
      return objData;
    }
  }

  public TaskDataCache.RelDataItem GetRelData(long relationId, IUserSession session)
  {
    if (relationId == 0L)
      return (TaskDataCache.RelDataItem) null;
    lock (this._relDataCache)
    {
      TaskDataCache.RelDataItem relData;
      if (this._relDataCache.TryGetValue(relationId, out relData))
        return relData;
      if (session == null)
        return (TaskDataCache.RelDataItem) null;
      IDBRelation relation = session.GetRelation(relationId, false);
      if (relation != null)
        relData = new TaskDataCache.RelDataItem(relationId, relation.RelationType, relation.GUID);
      this._relDataCache.Add(relationId, relData);
      return relData;
    }
  }

  public TypedInfoItem GetItemData(long itemId, IUserSession session)
  {
    if (itemId == 0L || itemId == 0L)
      return (TypedInfoItem) null;
    lock (this._objDataCache)
    {
      lock (this._relDataCache)
      {
        TaskDataCache.ObjDataItem itemData1;
        bool flag1 = this._objDataCache.TryGetValue(itemId, out itemData1);
        if (flag1 && (TypedInfoItem) itemData1 != (TypedInfoItem) null)
          return (TypedInfoItem) itemData1;
        TaskDataCache.RelDataItem itemData2;
        bool flag2 = this._relDataCache.TryGetValue(itemId, out itemData2);
        if (flag2 && (TypedInfoItem) itemData2 != (TypedInfoItem) null)
          return (TypedInfoItem) itemData2;
        if (session == null)
          return (TypedInfoItem) null;
        if (!flag1)
        {
          QuickObjectInfo objectInfo = session.GetObjectInfo(itemId);
          if (!objectInfo.Empty)
            itemData1 = new TaskDataCache.ObjDataItem(itemId, objectInfo.ObjectTypeID, objectInfo.ID, objectInfo.VersionGuid, objectInfo.Caption);
          this._objDataCache.Add(itemId, itemData1);
          if ((TypedInfoItem) itemData1 != (TypedInfoItem) null)
            return (TypedInfoItem) itemData1;
        }
        if (flag2)
          return (TypedInfoItem) null;
        IDBRelation relation = session.GetRelation(itemId, false);
        if (relation != null)
          itemData2 = new TaskDataCache.RelDataItem(itemId, relation.RelationType, relation.GUID);
        this._relDataCache.Add(itemId, itemData2);
        return (TypedInfoItem) itemData2;
      }
    }
  }

  public List<ColumnDescriptor> GetCacheObjOnlyColumnList(TaskDataCache.ColumnsMode mode)
  {
    List<ColumnDescriptor> objOnlyColumnList1;
    if (this._mode2DataObjColumns.TryGetValue(mode, out objOnlyColumnList1))
      return objOnlyColumnList1;
    List<ColumnDescriptor> objOnlyColumnList2 = new List<ColumnDescriptor>(5);
    objOnlyColumnList2.Add(new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    objOnlyColumnList2.Add(new ColumnDescriptor((object) -7, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    objOnlyColumnList2.Add(new ColumnDescriptor((object) -3, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    if (mode == TaskDataCache.ColumnsMode.All)
    {
      objOnlyColumnList2.Add(new ColumnDescriptor((object) -12, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
      objOnlyColumnList2.Add(new ColumnDescriptor((object) new Guid("cad00047-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    }
    this._mode2DataObjColumns[mode] = objOnlyColumnList2;
    return objOnlyColumnList2;
  }

  public List<ColumnDescriptor> GetCacheColumnList(TaskDataCache.ColumnsMode mode)
  {
    List<ColumnDescriptor> cacheColumnList1;
    if (this._mode2DataRelColumns.TryGetValue(mode, out cacheColumnList1))
      return cacheColumnList1;
    List<ColumnDescriptor> cacheColumnList2 = new List<ColumnDescriptor>(8);
    cacheColumnList2.AddRange((IEnumerable<ColumnDescriptor>) this.GetCacheObjOnlyColumnList(mode));
    cacheColumnList2.Add(new ColumnDescriptor((object) new Guid("cad00033-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    cacheColumnList2.Add(new ColumnDescriptor((object) new Guid("cad00036-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    cacheColumnList2.Add(new ColumnDescriptor((object) new Guid("cad00344-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    this._mode2DataRelColumns[mode] = cacheColumnList2;
    return cacheColumnList2;
  }

  public ColumnDescriptor[] GetCacheColumns(TaskDataCache.ColumnsMode mode)
  {
    return this.GetCacheColumnList(mode).ToArray();
  }

  public void FillCacheData(DataTable dataTable)
  {
    this.FillObjCacheData(dataTable);
    this.FillRelCacheData(dataTable);
  }

  public void FillCacheData(HybridTableExp dataTable)
  {
    this.FillObjCacheData(dataTable);
    this.FillRelCacheData(dataTable);
  }

  public Dictionary<long, TaskDataCache.ObjDataItem> ObjDataCache
  {
    [DebuggerStepThrough] get => this._objDataCache;
  }

  public Dictionary<long, TaskDataCache.RelDataItem> RelDataCache
  {
    [DebuggerStepThrough] get => this._relDataCache;
  }

  public static bool IsEmpty(TypedInfoItem infoItem)
  {
    return infoItem == (TypedInfoItem) null || infoItem.ItemTypeID == -1 || infoItem.ItemTypeID == -1;
  }

  public enum ColumnsMode
  {
    All,
    SystemOnly,
  }

  public class ObjDataItem : ObjInfoCaptionItem
  {
    protected long _Id;
    protected Guid _objGuid = Guid.Empty;

    public ObjDataItem(long objectId, int objTypeId)
      : this(objectId, objTypeId, 0L)
    {
    }

    public ObjDataItem(long objectId, int objTypeId, long aId)
      : this(objectId, objTypeId, aId, Guid.Empty)
    {
    }

    public ObjDataItem(long objectId, int objTypeId, long aId, Guid objGuid)
      : base(objectId, objTypeId)
    {
      this._Id = aId;
      this._objGuid = objGuid;
    }

    public ObjDataItem(long objectId, int objTypeId, long aId, Guid objGuid, string caption)
      : base(objectId, objTypeId)
    {
      this._Id = aId;
      this._objGuid = objGuid;
      this._caption = caption;
    }

    public long Id
    {
      [DebuggerStepThrough] get => this._Id;
      [DebuggerStepThrough] set => this._Id = value;
    }

    public Guid ObjGuid
    {
      [DebuggerStepThrough] get => this._objGuid;
      [DebuggerStepThrough] set => this._objGuid = value;
    }

    public void UpdateDataInfo(IUserSession ius)
    {
      if (this._caption != null)
        return;
      QuickObjectInfo objectInfo = ius.GetObjectInfo(this.ObjectID);
      this._caption = objectInfo.Caption;
      this._objGuid = objectInfo.VersionGuid;
    }
  }

  public class RelDataItem : RelInfoItem
  {
    protected Guid _relGuid = Guid.Empty;

    public RelDataItem(long relationId, int relTypeId)
      : this(relationId, relTypeId, Guid.Empty)
    {
    }

    public RelDataItem(long relationId, int relTypeId, Guid relGuid)
      : base(relationId, relTypeId)
    {
      this._relGuid = relGuid;
    }

    public Guid RelGuid
    {
      [DebuggerStepThrough] get => this._relGuid;
      [DebuggerStepThrough] set => this._relGuid = value;
    }
  }
}
