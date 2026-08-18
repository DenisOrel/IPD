// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.Compositions.CompositionTaskBooster
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;


namespace Intermech.Kernel.Services.Compositions;

public class CompositionTaskBooster(
  IUserSession session,
  ICompositionLoadService compositionService) : CompositionLoadTask(session, compositionService)
{
  private IList<ColumnDescriptor> _orgColumns;
  private IList<ColumnDescriptor> _objColumns;
  private IList<int> _orgColAttrIDs;
  private IList<int> _objColAttrIDs;
  private IList<int> _fxColAttrIDs;
  private IList<DataRow> _taskRows;
  private DataTable _resultTable;
  private bool _boostStarted;
  private IDictionary<long, DataRow> _obj2AttrRowCache;
  private IList<int> _boosterColumn2Remove;

  private bool Boost_Allowed()
  {
    IEnumerable<ConditionStructure> conditions = this._loadingParams.Conditions;
    Dictionary<AttributeSourceTypes, bool> type2CustomAttr;
    SqlHelper.HasCustomAttributes(new DBRecordSetParams(conditions != null ? conditions.ToArray<ConditionStructure>() : (ConditionStructure[]) null, (object[]) null), AttributeSourceTypes.Relation, out type2CustomAttr);
    return (type2CustomAttr == null || !type2CustomAttr.ContainsKey(AttributeSourceTypes.Object)) && (!this._session.EnabledVisibilityFiltration || this._loadingParams.DbParams == null || !this._loadingParams.DbParams.Values.Any<HybridDictionary>((System.Func<HybridDictionary, bool>) (item => item[(object) "{7FB30639-2F65-4407-B78E-523547B1B133}"] != null && item[(object) "{7FB30639-2F65-4407-B78E-523547B1B133}"].Equals((object) true))));
  }

  private void Boost_Start()
  {
    this._boostStarted = false;
    if (!this.Boost_Allowed())
      return;
    this._orgColumns = (IList<ColumnDescriptor>) this._columns;
    this._objColumns = (IList<ColumnDescriptor>) new List<ColumnDescriptor>();
    List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>(this._columns.Count);
    this._fxColAttrIDs = (IList<int>) new List<int>(this._columns.Count);
    this._orgColAttrIDs = (IList<int>) new List<int>(this._columns.Count);
    this._objColAttrIDs = (IList<int>) new List<int>(this._columns.Count);
    for (int index = 0; index < this._columns.Count; ++index)
    {
      bool flag1 = false;
      ColumnDescriptor column = this._columns[index];
      int num = !(column.AttributeID is int attributeId) ? MetaDataHelper.GetAttributeID(column.AttributeID) : attributeId;
      try
      {
        if (num != -10000)
        {
          if (num != 0)
          {
            bool flag2 = ObligatoryObjectAttributesHelper.IsObligatoryAttribute(num);
            if (flag2)
            {
              if (flag2)
              {
                AttributeSourceTypes attributeSourceType = ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) num);
                if (column.AttributeSource != attributeSourceType && column.AttributeSource != AttributeSourceTypes.Auto)
                {
                  column.AttributeSource = attributeSourceType;
                  this._columns[index] = column;
                }
                if (attributeSourceType == AttributeSourceTypes.Object)
                  flag1 = num == -50 || num == -12;
              }
            }
            else if (column.AttributeSource == AttributeSourceTypes.Object)
              flag1 = true;
          }
        }
      }
      finally
      {
        if (flag1)
        {
          this._objColumns.Add(column);
          this._objColAttrIDs.Add(num);
        }
        else
        {
          columnDescriptorList.Add(column);
          this._fxColAttrIDs.Add(num);
        }
        this._orgColAttrIDs.Add(num);
      }
    }
    this._columns = columnDescriptorList;
    this._boostStarted = this._objColumns.Count != 0;
    if (!this._boostStarted)
      return;
    this._boosterColumn2Remove = (IList<int>) new List<int>();
    int attributeID1 = -2;
    if (this._orgColAttrIDs.IndexOf(attributeID1) == -1)
    {
      this._boosterColumn2Remove.Add(this._orgColumns.Count);
      ColumnDescriptor columnDescriptor = new ColumnDescriptor((object) attributeID1, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0);
      this._columns.Add(columnDescriptor);
      this._orgColumns.Add(columnDescriptor);
      this._fxColAttrIDs.Add(attributeID1);
      this._orgColAttrIDs.Add(attributeID1);
    }
    int attributeID2 = -7;
    if (this._orgColAttrIDs.IndexOf(attributeID2) != -1)
      return;
    this._boosterColumn2Remove.Add(this._orgColumns.Count);
    ColumnDescriptor columnDescriptor1 = new ColumnDescriptor((object) attributeID2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0);
    this._columns.Add(columnDescriptor1);
    this._orgColumns.Add(columnDescriptor1);
    this._fxColAttrIDs.Add(attributeID2);
    this._orgColAttrIDs.Add(attributeID2);
  }

  private void Boost_Stop()
  {
    if (!this._boostStarted || this._resultTable == null)
      return;
    for (int index1 = this._boosterColumn2Remove.Count - 1; index1 >= 0; --index1)
    {
      int index2 = this._boosterColumn2Remove[index1];
      if (index2 >= 0 && index1 < this._resultTable.Columns.Count)
        this._resultTable.Columns.RemoveAt(index2);
    }
  }

  private void Boost_PostProceedTask()
  {
    if (this._taskRows == null || this._taskRows.Count == 0)
      return;
    if (!this._boostStarted)
    {
      this._resultTable = this._taskRows[0].Table.Clone();
      DataSetProcessor.AssignRows(this._resultTable, (IEnumerable<DataRow>) this._taskRows, true, true);
      this.RemoveServiceColumns(this._resultTable);
    }
    else
    {
      this.ResultTable_CreateStruct();
      this.ResultTable_LoadObjectData();
      this.ResultTable_AppendAllData();
    }
  }

  private void ResultTable_CreateStruct()
  {
    this._resultTable = DBRecordSet.CreateEmptyDataTable(string.Empty, this._orgColumns.ToArray<ColumnDescriptor>());
    if (this._resultTable == null)
      return;
    DataTable resultTable = this._taskRows[0].Table.Clone();
    this.RemoveServiceColumns(resultTable);
    this._resultTable.TableName = resultTable.TableName;
    this._resultTable.MinimumCapacity = this._taskRows.Count;
    int count1 = resultTable.Columns.Count;
    for (int count2 = this._fxColAttrIDs.Count; count2 < count1; ++count2)
    {
      string columnName = resultTable.Columns[count2].ColumnName;
      if (this._resultTable.Columns.IndexOf(columnName) == -1)
        this._resultTable.Columns.Add(new DataColumn(columnName, resultTable.Columns[count2].DataType, resultTable.Columns[count2].Expression, resultTable.Columns[count2].ColumnMapping));
    }
  }

  private void ResultTable_AppendAllData()
  {
    this._resultTable.BeginLoadData();
    try
    {
      DataTable dataTable = this._taskRows[0].Table;
      int count1 = dataTable.Columns.Count;
      for (int index = 1; index < this._taskRows.Count; ++index)
      {
        DataTable table = this._taskRows[index].Table;
        if (count1 < table.Columns.Count)
        {
          dataTable = table;
          count1 = table.Columns.Count;
        }
      }
      int count2 = this._resultTable.Columns.Count;
      int[] numArray = new int[count2];
      for (int index = 0; index < count2; ++index)
      {
        string columnName = this._resultTable.Columns[index].ColumnName;
        int num = !(columnName == "F_PART_OBJ_ID") ? (index >= this._orgColumns.Count ? dataTable.Columns.IndexOf(columnName) : this._columns.IndexOf(this._orgColumns[index])) : this._col_idx_PartObjectID;
        numArray[index] = num;
      }
      for (int index1 = 0; index1 < numArray.Length; ++index1)
      {
        int index2 = numArray[index1];
        if (index2 != -1 && this._resultTable.Columns[index1].DataType != dataTable.Columns[index2].DataType)
          this._resultTable.Columns[index1].DataType = dataTable.Columns[index2].DataType;
      }
      object[] itemArray = this._resultTable.NewRow().ItemArray;
      foreach (DataRow taskRow in (IEnumerable<DataRow>) this._taskRows)
      {
        for (int index = 0; index < count2; ++index)
        {
          int columnIndex = numArray[index];
          if (columnIndex != -1)
          {
            if (taskRow.Table.Columns.Count > columnIndex)
              itemArray[index] = taskRow[columnIndex];
          }
          else
            itemArray[index] = (object) DBNull.Value;
        }
        DataRow dataRow;
        if (this._obj2AttrRowCache != null && this._obj2AttrRowCache.TryGetValue(Convert.ToInt64(taskRow[this._col_idx_ObjID]), out dataRow))
        {
          if (dataRow.Table.ExtendedProperties[(object) "colCache"] is DataColumn[] extendedProperty)
          {
            DataColumnCollection columns = dataRow.Table.Columns;
            DataColumn dataColumn = columns.Count > 0 ? columns[0] : (DataColumn) null;
            int count3 = this._orgColumns.Count;
            for (int index = 0; index < count3; ++index)
            {
              DataColumn column = extendedProperty[index];
              if (column != null && column != dataColumn)
                itemArray[index] = dataRow[column];
            }
          }
          else
            continue;
        }
        this._resultTable.Rows.Add(itemArray);
      }
    }
    finally
    {
      this._resultTable.EndLoadData();
    }
    this._resultTable.AcceptChanges();
  }

  private void ResultTable_LoadObjectData()
  {
    if (this._resultTable == null)
      return;
    this._obj2AttrRowCache = (IDictionary<long, DataRow>) new Dictionary<long, DataRow>(this._resultTable.Rows.Count);
    Dictionary<int, List<long>> dictionary = new Dictionary<int, List<long>>();
    foreach (DataRow taskRow in (IEnumerable<DataRow>) this._taskRows)
    {
      int int32 = Convert.ToInt32(taskRow[this._col_idx_ObjectType]);
      long int64 = Convert.ToInt64(taskRow[this._col_idx_ObjID]);
      List<long> longList;
      if (!dictionary.TryGetValue(int32, out longList))
      {
        longList = new List<long>();
        dictionary.Add(int32, longList);
      }
      longList.Add(int64);
    }
    List<int> intList = new List<int>();
    List<long> objectIDs = new List<long>();
    foreach (KeyValuePair<int, List<long>> keyValuePair in dictionary)
    {
      if (MetaDataHelper.IsLocalObjectType(keyValuePair.Key))
      {
        this.ResultTable_LoadObjectData(keyValuePair.Key, keyValuePair.Value);
      }
      else
      {
        bool flag = false;
        foreach (int objColAttrId in (IEnumerable<int>) this._objColAttrIDs)
        {
          if (objColAttrId >= 0)
          {
            IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(keyValuePair.Key, objColAttrId);
            if (attribute4ObjectType != null && (attribute4ObjectType.OptimizationMode == OptimizationModes.Read || attribute4ObjectType.OptimizationMode == OptimizationModes.Seek))
            {
              flag = true;
              break;
            }
          }
        }
        if (flag)
        {
          this.ResultTable_LoadObjectData(keyValuePair.Key, keyValuePair.Value);
        }
        else
        {
          intList.Add(keyValuePair.Key);
          objectIDs.AddRange((IEnumerable<long>) keyValuePair.Value);
        }
      }
    }
    this.ResultTable_LoadObjectData(intList.Count == 1 ? intList[0] : -1, objectIDs);
  }

  private void ResultTable_LoadObjectData(int objTypeId, List<long> objectIDs)
  {
    if (objectIDs == null || objectIDs.Count == 0)
      return;
    GenericListHelper.MakeUnique<long>(objectIDs);
    IDBObjectCollection objectCollection = this._session.GetObjectCollection(objTypeId);
    objectCollection.ShowAllModifications = true;
    List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>(this._objColumns.Count)
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
    };
    if (objTypeId == -1)
    {
      columnDescriptorList.AddRange((IEnumerable<ColumnDescriptor>) this._objColumns);
    }
    else
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType(objTypeId);
      for (int index = 0; index < this._objColumns.Count; ++index)
      {
        ColumnDescriptor objColumn = this._objColumns[index];
        if (!objectType.AnyAttributes)
        {
          int objColAttrId = this._objColAttrIDs[index];
          if (objColAttrId > 0 && MetaDataHelper.GetAttribute4ObjectType(objTypeId, objColAttrId) == null)
            continue;
        }
        objColumn.Sort = SortOrders.NONE;
        columnDescriptorList.Add(objColumn);
      }
    }
    bool enForceSave = false;
    if (((UserSession) this._session).DataManager.DataProvider.Name == "Sql")
      enForceSave = objectIDs.Count > ((UserSession) this._session).DataManager.DataProvider.MaximumINOperands / 2;
    INConditionValue inConditionValue = ((UserSession) this._session).QueryBuilder.StartINCondition((object) -2, (Array) objectIDs.ToArray(), enForceSave);
    DataTable dataTable;
    try
    {
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(-2, RelationalOperators.In, (object) inConditionValue, (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object)
      }, columnDescriptorList.ToArray());
      dataTable = objectCollection.Select(paramSet);
      if (dataTable == null)
        return;
    }
    finally
    {
      ((UserSession) this._session).QueryBuilder.StopINCondition(inConditionValue);
    }
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      this._obj2AttrRowCache[Convert.ToInt64(row[0])] = row;
    DataColumn[] dataColumnArray = new DataColumn[this._orgColumns.Count];
    for (int index1 = 0; index1 < this._orgColumns.Count; ++index1)
    {
      int index2 = columnDescriptorList.IndexOf(this._orgColumns[index1]);
      dataColumnArray[index1] = index2 != -1 ? dataTable.Columns[index2] : (DataColumn) null;
    }
    dataTable.ExtendedProperties[(object) "colCache"] = (object) dataColumnArray;
  }

  protected override DataTable DoExecute(
    CompositionLoadTask.CompositionCustomMethods method)
  {
    this.Boost_Start();
    try
    {
      this._taskRows = this.ProceedTaskMethod(method);
      this.Boost_PostProceedTask();
    }
    finally
    {
      this.Boost_Stop();
    }
    this._taskRows = (IList<DataRow>) null;
    return this._resultTable;
  }

  protected override void RemoveServiceColumns(DataTable taskTable)
  {
    base.RemoveServiceColumns(taskTable);
  }
}
