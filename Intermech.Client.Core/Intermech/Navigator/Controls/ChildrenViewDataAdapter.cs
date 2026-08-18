
// Type: Intermech.Navigator.Controls.ChildrenViewDataAdapter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Search;
using Intermech.Search.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TenTec.Windows.iGridLib;


namespace Intermech.Navigator.Controls;

/// <summary>Вспомогательный класс для работы с данными для грида</summary>
public sealed class ChildrenViewDataAdapter : IEnumerable<INodeID>, IEnumerable
{
  /// <summary>Класс-владелец</summary>
  private ChildrenView _childrenView;
  private List<INodeID> _nodeIds = new List<INodeID>();
  private long _readRecordCount;
  /// <summary>Количество всех записей</summary>
  private long _totalRecordCount;
  /// <summary>Коллекция колонок</summary>
  private NodeColumnCollection _nodeColumnCollection;
  /// <summary>Закладка</summary>
  private object _bookmark;
  /// <summary>Данные зачитаны полностью или нет</summary>
  private bool _eof;
  /// <summary>Изменился ли порядок сортировки</summary>
  private bool _sortOrderChanged;
  private volatile INodeQuery _nodeQuery;
  private LazyService<ICategoryTypeIconService> _categoryTypeIconService = new LazyService<ICategoryTypeIconService>();
  private LazyService<IColumnSchemes> _columnSchemes = new LazyService<IColumnSchemes>();
  private LazyService<ICurrentUserAndRole> _currentUserAndRole = new LazyService<ICurrentUserAndRole>();

  public ChildrenViewDataAdapter(ChildrenView childrenView)
  {
    this._childrenView = childrenView != null ? childrenView : throw new ArgumentNullException(nameof (childrenView));
  }

  /// <summary>Количество считанных записей</summary>
  public long ReadedRecordCount => (long) this._nodeIds.Count;

  /// <summary>Количество всех записей</summary>
  public long TotalRecordCount => this._totalRecordCount;

  public INodeID this[int index] => this._nodeIds[index];

  /// <summary>Данные зачитаны полностью или нет</summary>
  public bool Eof => this._eof;

  public object Bookmark => this._bookmark;

  public bool HasPreloadedData { get; private set; }

  /// <summary>Очистить коллекции строк</summary>
  public void ClearRows()
  {
    try
    {
      this._childrenView.Grid.Rows.Clear();
      this._bookmark = (object) null;
      this._eof = true;
      this._totalRecordCount = 0L;
      if (this._childrenView._gridSelectedItems == null)
        return;
      this._childrenView._gridSelectedItems.Invalidate();
    }
    finally
    {
      this.GetNodeIds();
      this.RaiseDataTableChanged();
    }
  }

  public int IndexOf(INodeID nodeID)
  {
    if (nodeID == null)
      throw new ArgumentNullException(nameof (nodeID));
    int index = 0;
    for (int count = this._nodeIds.Count; index < count; ++index)
    {
      if (this[index] == nodeID)
        return index;
    }
    return -1;
  }

  public void Refresh() => throw new NotImplementedException();

  public void ReadNext() => throw new NotImplementedException();

  public void ReadAll() => throw new NotImplementedException();

  public void Preload(int? count = null)
  {
    this._nodeQuery = this.GetNodeQuery();
    if (this._nodeQuery != null)
    {
      NodeColumnCollection columns = new NodeColumnCollection((IEnumerable<NodeColumn>) this._childrenView.GetNodeColumns());
      columns.AddRange((IEnumerable<NodeColumn>) this._childrenView.GetSpecialNodeColumns());
      this.SetQueryColumns(this._nodeQuery, columns);
      this._nodeQuery.Execute((object) null, count.HasValue ? count.Value : 2147483646);
    }
    this.HasPreloadedData = true;
  }

  public void ClearPreloadedData()
  {
    this._nodeQuery = (INodeQuery) null;
    this.HasPreloadedData = false;
  }

  public void LoadRows(int count, bool selectFirstRow)
  {
    INodeQuery nodeQuery = this._nodeQuery;
    this._nodeQuery = (INodeQuery) null;
    NodeColumnCollection nodeColumns = this._childrenView.GetNodeColumns();
    if (nodeQuery == null)
    {
      nodeQuery = this.GetNodeQuery();
      if (nodeQuery == null)
        return;
      NodeColumnCollection columns = new NodeColumnCollection((IEnumerable<NodeColumn>) nodeColumns);
      columns.AddRange((IEnumerable<NodeColumn>) this._childrenView.GetSpecialNodeColumns());
      this.SetQueryColumns(nodeQuery, columns);
      nodeQuery.Execute(this._bookmark, count);
    }
    this._bookmark = nodeQuery.Bookmark;
    this._eof = this._bookmark == null;
    if (nodeQuery.TotalRecordCount != 0L)
      this._totalRecordCount = nodeQuery.TotalRecordCount;
    List<int> items = (this._childrenView == null || this._childrenView.Services == null ? (IToSelectItemsAnalyzers) null : this._childrenView.Services.GetService(typeof (IToSelectItemsAnalyzers)) as IToSelectItemsAnalyzers) != null ? new List<int>() : (List<int>) null;
    try
    {
      ChildrenViewRowData[] dataForNodeQuery = this.CreateRowDataForNodeQuery(nodeQuery, nodeColumns);
      foreach (ChildrenViewRowData childrenViewRowData in dataForNodeQuery)
        this._childrenView.Grid.Rows.Add().Tag = (object) childrenViewRowData;
      this.UpdateGrid(nodeColumns);
      if (items != null && items.Count > 0)
      {
        this._childrenView.SelectItems(items, false);
      }
      else
      {
        if (!selectFirstRow)
          return;
        ChildrenViewRowData firstRowData = ((IEnumerable<ChildrenViewRowData>) dataForNodeQuery).FirstOrDefault<ChildrenViewRowData>();
        if (firstRowData == null)
          return;
        iGRow row = this._childrenView.Grid.Rows.Cast<iGRow>().FirstOrDefault<iGRow>((Func<iGRow, bool>) (o => o.Tag == firstRowData));
        if (this._childrenView.Grid.GroupObject.Count != 0 || this._childrenView.DisableAutoselectFirstRow)
          return;
        this._childrenView.Grid.CurRow = row;
        this._childrenView.SetSelectedForRow(row, true);
      }
    }
    finally
    {
      this.GetNodeIds();
      this.RaiseDataTableChanged();
      this._childrenView.Group();
    }
  }

  public void Append(NodeIDCollection partialNodeIds, bool selectFirstRecord)
  {
    if (partialNodeIds == null)
      throw new ArgumentNullException(nameof (partialNodeIds));
    INodeQuery nodeQuery = this.GetNodeQuery();
    if (nodeQuery == null)
      return;
    NodeColumnCollection nodeColumns = this._childrenView.GetNodeColumns();
    NodeColumnCollection columns = new NodeColumnCollection((IEnumerable<NodeColumn>) nodeColumns);
    columns.AddRange((IEnumerable<NodeColumn>) this._childrenView.GetSpecialNodeColumns());
    this.SetQueryColumns(nodeQuery, columns);
    nodeQuery.Execute(partialNodeIds);
    if (nodeQuery.RecordCount <= 0)
      return;
    try
    {
      ChildrenViewRowData[] dataForNodeQuery = this.CreateRowDataForNodeQuery(nodeQuery, nodeColumns);
      foreach (ChildrenViewRowData childrenViewRowData in dataForNodeQuery)
        this._childrenView.Grid.Rows.Add().Tag = (object) childrenViewRowData;
      this.GetNodeIds();
      this.UpdateGrid(nodeColumns);
      List<int> items = new List<int>();
      foreach (ChildrenViewRowData childrenViewRowData in dataForNodeQuery)
      {
        iGRow rowWithNodeId = this._childrenView.GetRowWithNodeID(childrenViewRowData.NodeID);
        if (rowWithNodeId != null)
          items.Add(rowWithNodeId.Index);
      }
      if (items.Count <= 0)
        return;
      this._childrenView.SelectItems(items, true);
    }
    finally
    {
      this.RaiseDataTableChanged();
      if (this._childrenView.Grid.GroupObject.Count > 0)
      {
        this._childrenView._groupRowsCount = 0;
        this._childrenView._collapsedRowsCount = 0;
        this._childrenView.Grid.Group();
      }
    }
  }

  public void Update(IList indexes)
  {
    if (indexes == null)
      throw new ArgumentNullException(nameof (indexes));
    INodeQuery nodeQuery = this.GetNodeQuery();
    if (nodeQuery == null)
      return;
    bool flag1 = false;
    List<INodeID> selectedNodeIds = this._childrenView.SelectedNodeIDs;
    NodeIDCollection nodeIDs = new NodeIDCollection();
    for (int index = 0; index < indexes.Count; ++index)
    {
      INodeID nodeId = this[(int) indexes[index]];
      nodeIDs.Add(nodeId);
      if (!flag1 && selectedNodeIds.IndexOf(nodeId) >= 0)
        flag1 = true;
    }
    NodeColumnCollection nodeColumns = this._childrenView.GetNodeColumns();
    NodeColumnCollection columns = new NodeColumnCollection((IEnumerable<NodeColumn>) nodeColumns);
    columns.AddRange((IEnumerable<NodeColumn>) this._childrenView.GetSpecialNodeColumns());
    this.SetQueryColumns(nodeQuery, columns);
    nodeQuery.Execute(nodeIDs);
    bool flag2 = flag1;
    try
    {
      foreach (ChildrenViewRowData childrenViewRowData in this.CreateRowDataForNodeQuery(nodeQuery, nodeColumns))
      {
        ChildrenViewRowData rowData = childrenViewRowData;
        iGRow iGrow = this._childrenView.Grid.Rows.Cast<iGRow>().FirstOrDefault<iGRow>((Func<iGRow, bool>) (o => o.Tag is ChildrenViewRowData && object.Equals((object) ((ChildrenViewRowData) o.Tag).NodeID, (object) rowData.NodeID)));
        if (iGrow != null)
        {
          if (this._childrenView.EditingMode)
          {
            foreach (KeyValuePair<string, ChildrenViewCellData> cellData in rowData.CellDataDictionary)
            {
              if (iGrow.Tag is ChildrenViewRowData)
              {
                ChildrenViewCellData childrenViewCellData = (ChildrenViewCellData) null;
                if (((ChildrenViewRowData) iGrow.Tag).CellDataDictionary.TryGetValue(cellData.Key, out childrenViewCellData))
                  cellData.Value.ReadOnly = childrenViewCellData.ReadOnly;
              }
            }
          }
          iGrow.Tag = (object) rowData;
        }
      }
      this.UpdateGrid(nodeColumns);
    }
    finally
    {
      this.GetNodeIds();
      this.RaiseDataTableChanged();
      if (flag2)
        this._childrenView.SelectionChanged();
    }
  }

  public void Replace(IList indexes, NodeIDCollection replacementNodeIds)
  {
    if (indexes == null)
      throw new ArgumentNullException(nameof (indexes));
    if (replacementNodeIds == null)
      throw new ArgumentNullException(nameof (replacementNodeIds));
    for (int index1 = 0; index1 < indexes.Count; ++index1)
    {
      int index2 = (int) indexes[index1];
      iGRow rowWithNodeId = this._childrenView.GetRowWithNodeID(index2);
      if (rowWithNodeId != null)
        rowWithNodeId.Tag = (object) new ChildrenViewRowData(replacementNodeIds[index1]);
      if (index2 <= this._nodeIds.Count - 1)
        this._nodeIds[index2] = replacementNodeIds[index1];
    }
    this.Update(indexes);
  }

  public void Remove(IList indexes)
  {
    if (indexes == null)
      throw new ArgumentNullException(nameof (indexes));
    try
    {
      for (int index = indexes.Count - 1; index >= 0; --index)
      {
        iGRow rowWithNodeId = this._childrenView.GetRowWithNodeID((int) indexes[index]);
        if (rowWithNodeId != null)
          this._childrenView.Grid.Rows.RemoveAt(rowWithNodeId.Index);
      }
    }
    finally
    {
      this.GetNodeIds();
      this.RaiseDataTableChanged();
      this._totalRecordCount -= (long) indexes.Count;
    }
  }

  public IEnumerator<INodeID> GetEnumerator()
  {
    return (IEnumerator<INodeID>) this._nodeIds.GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

  private void GetNodeIds()
  {
    this._nodeIds = this._childrenView.Grid.Rows.Cast<iGRow>().Where<iGRow>((Func<iGRow, bool>) (o => o.Tag is ChildrenViewRowData)).Select<iGRow, INodeID>((Func<iGRow, INodeID>) (o => ((ChildrenViewRowData) o.Tag).NodeID)).ToList<INodeID>();
  }

  private INodeQuery GetNodeQuery()
  {
    return this._childrenView.Node.GetQuery(this._childrenView.ViewContentType);
  }

  /// <summary>Задать колонки для запроса</summary>
  /// <param name="query">Запрос к источнику данных</param>
  /// <param name="columns">Коллекция колонок</param>
  private void SetQueryColumns(INodeQuery query, NodeColumnCollection columns)
  {
    for (int index = 0; index < columns.Count; ++index)
    {
      INodeColumnTransform defaultTransform = this._columnSchemes.Value.GetDefaultTransform(columns[index].SchemeGuid, columns[index].ID);
      query.AddColumn(columns[index], defaultTransform);
    }
  }

  /// <summary>
  /// Вызываем событие, уведомляющее об изменении таблицы в источнике данных
  /// </summary>
  private void RaiseDataTableChanged() => this._childrenView.RaiseDataTableChanged();

  /// <summary>Вернуть индекс значка для указанных категории и типа</summary>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="handler">Родительский узел (обработчик)</param>
  /// <param name="state">Состояние</param>
  /// <returns>Индекс значка для указанных категории и типа</returns>
  private int GetCategoryTypeIconIndex(INodeID nodeID, INode handler, object state)
  {
    INavigatorIconInformation data = nodeID == null || handler == null ? (INavigatorIconInformation) null : handler.GetData(nodeID, typeof (INavigatorIconInformation)) as INavigatorIconInformation;
    return Images32x16_Cache.GetImage32x16Index(nodeID.CategoryID, nodeID.TypeID, (object) data);
  }

  private ChildrenViewRowData[] CreateRowDataForNodeQuery(
    INodeQuery nodeQuery,
    NodeColumnCollection nodeColumns)
  {
    List<ChildrenViewRowData> childrenViewRowDataList = new List<ChildrenViewRowData>();
    for (int index1 = 0; index1 < nodeQuery.RecordCount; ++index1)
    {
      ChildrenViewRowData rowData = new ChildrenViewRowData(nodeQuery.GetRecordNodeID(index1));
      object[] rawRecordValues = nodeQuery.GetRawRecordValues(index1);
      object[] recordValues = nodeQuery.GetRecordValues(index1);
      for (int index2 = 0; index2 < nodeColumns.Count; ++index2)
      {
        NodeColumn nodeColumn = nodeColumns[index2];
        object obj1 = rawRecordValues[index2];
        object obj2 = recordValues[index2];
        if (obj1 is DBNull)
          obj1 = (object) null;
        else if (nodeColumn.Attribute != null && nodeColumn.Attribute.RealFieldType == FieldTypes.ftBoolean)
          obj1 = (object) Convert.ToBoolean(obj1);
        ChildrenViewCellData childrenViewCellData = new ChildrenViewCellData(rowData, nodeColumn)
        {
          RawValue = obj1,
          Value = obj2
        };
        rowData.CellDataDictionary[nodeColumn.Key] = childrenViewCellData;
      }
      childrenViewRowDataList.Add(rowData);
    }
    return childrenViewRowDataList.ToArray();
  }

  public void UpdateGrid(NodeColumnCollection nodeColumns)
  {
    Dictionary<object, string> dictionary1 = new Dictionary<object, string>(nodeColumns.Count);
    for (int index = 0; index < nodeColumns.Count; ++index)
    {
      if (!dictionary1.ContainsKey(nodeColumns[index].ID))
        dictionary1.Add(nodeColumns[index].ID, nodeColumns[index].ID.ToString() + ".images");
    }
    iGCol col1 = this._childrenView.Grid.Cols["Special_StateImage"];
    iGCol col2 = this._childrenView.Grid.Cols["Special_CheckedOut"];
    Dictionary<string, iGCol> dictionary2 = new Dictionary<string, iGCol>();
    foreach (iGCol col3 in (IEnumerable) this._childrenView.Grid.Cols)
      dictionary2[col3.Key] = col3;
    foreach (iGRow row in (IEnumerable) this._childrenView.Grid.Rows)
    {
      if (row.Tag is ChildrenViewRowData)
      {
        ChildrenViewRowData tag = (ChildrenViewRowData) row.Tag;
        IDBTypedObjectID data1 = (IDBTypedObjectID) this._childrenView.Node.GetData(tag.NodeID, typeof (IDBTypedObjectID));
        IDBRelationID data2 = (IDBRelationID) this._childrenView.Node.GetData(tag.NodeID, typeof (IDBRelationID));
        if (col1 != null)
        {
          iGCell cell = row.Cells[col1.Index];
          IImageState data3 = (IImageState) this._childrenView.Node.GetData(tag.NodeID, typeof (IImageState));
          int categoryTypeIconIndex = this.GetCategoryTypeIconIndex(tag.NodeID, this._childrenView.Node, data3?.State);
          cell.ImageList = this._categoryTypeIconService.Value.ImageList;
          cell.ImageIndex = categoryTypeIconIndex;
          if (data1 == null)
            cell.Value = (object) string.Format(LocalizationHolder.rm.GetString("ChildrenView_ObjectTypeHint"), (object) MetaDataHelper.GetObjectTypeName(tag.NodeID.TypeID));
          else
            cell.Value = (object) string.Format(LocalizationHolder.rm.GetString("ChildrenView_ObjectTypeHintFull"), (object) MetaDataHelper.GetObjectTypeName(data1.ObjectType), (object) data1.Caption, (object) data1.Version, (data1.BaseVersion & 1L) == 1L ? (object) LocalizationHolder.rm.GetString("Client.Core_1322") : (object) LocalizationHolder.rm.GetString("Client.Core_1321"), (object) data1.ObjectID, (object) data1.ID);
        }
        if (col2 != null)
        {
          iGCell cell = row.Cells[col2.Index];
          IDBCheckedOutByID data4 = (IDBCheckedOutByID) this._childrenView.Node.GetData(tag.NodeID, typeof (IDBCheckedOutByID));
          cell.Value = data4 == null ? (object) string.Empty : (object) string.Format(LocalizationHolder.rm.GetString("ChildrenView_CheckedOutHint"), (object) ChildrenView._userNamesCache.GetUserName(data4.CheckedOutBy));
          int num = -1;
          if (data4 != null && !ObjectHelper.IsUnknownObjectID(data4.CheckedOutBy))
            num = data4.CheckedOutBy != this._currentUserAndRole.Value.UserID ? ChildrenView._namedImageList.ImageIndex("imgUserOther") : ChildrenView._namedImageList.ImageIndex("imgUserCurrent");
          cell.ImageList = ChildrenView._namedImageList.ImageList;
          cell.ImageIndex = num;
        }
        foreach (NodeColumn nodeColumn in (List<NodeColumn>) nodeColumns)
        {
          iGCol iGcol = (iGCol) null;
          if (dictionary2.TryGetValue(nodeColumn.Key, out iGcol))
          {
            iGCell cell = row.Cells[iGcol.Index];
            IMSAttributeType attribute = nodeColumn.Attribute;
            if (tag.CellDataDictionary.ContainsKey(nodeColumn.Key))
            {
              ChildrenViewCellData cellData = tag.CellDataDictionary[nodeColumn.Key];
              cell.Value = cellData.Value;
              if (this._childrenView._painterDictionary[(object) dictionary1[nodeColumn.ID]] is IGridColumnImageList painter)
              {
                cell.ImageList = painter.ImageList;
                cell.ImageIndex = painter.ImageIndex(tag.NodeID, cell, nodeColumns, this._childrenView.Grid);
              }
            }
          }
        }
      }
    }
  }
}
