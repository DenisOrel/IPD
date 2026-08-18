// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Navigator.Queries.TechObjectVirtualQuery
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using Intermech.TechCard.Client.Navigator.Descriptors;
using Intermech.TechCard.Client.Navigator.Nodes;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.TechCard.Client.Navigator.Queries;

/// <summary>Techcard objects virtual query</summary>
/// <summary>Constructor</summary>
/// <param name="support"></param>
/// <param name="objTypeID"></param>
/// <param name="conditions"></param>
/// <param name="services"></param>
public class TechObjectVirtualQuery(
  INodeQuerySupport support,
  int objTypeID,
  ConditionStructure[] conditions,
  IServiceProvider services) : TechObjectQuery(support, objTypeID, conditions, services)
{
  /// <summary>
  /// Модифицированная схема поиска (оригинальная у нас уже есть)
  /// </summary>
  protected RecordMapping _mappingFix;

  /// <summary>
  /// </summary>
  /// <param name="recMapping"></param>
  /// <param name="bookmark"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  protected override DBRecordSetParams GetQueryParams(
    object bookmark,
    int count,
    RecordMapping recMapping)
  {
    this._mappingFix = (RecordMapping) null;
    RecordMapping recMapping1 = recMapping;
    if (recMapping != null && recMapping.Count != 0)
    {
      TechObjectListNode owner = this.Support is ObjectsPartBase support ? support.Owner as TechObjectListNode : (TechObjectListNode) null;
      TechObjectListVirtualDescriptor descriptor = owner != null ? owner.Descriptor as TechObjectListVirtualDescriptor : (TechObjectListVirtualDescriptor) null;
      if (descriptor != null && descriptor.VirtualData != null && descriptor.VirtualData.Count != 0)
      {
        recMapping1 = new RecordMapping();
        this._mappingFix = recMapping1;
        List<int> intList = new List<int>((IEnumerable<int>) descriptor.VirtualData.GetFieldAttrIds());
        for (int index = 0; index < recMapping.Count; ++index)
        {
          NodeColumn column = recMapping[index].Column;
          int result = 0;
          if (column.ID != null)
            int.TryParse(column.ID.ToString(), out result);
          if (result != 0 && intList.Contains(result))
            recMapping1.RegisterColumn(column, (object) null, recMapping[index].Transform);
          else
            recMapping1.RegisterColumn(column, recMapping[index].Field, recMapping[index].Transform);
        }
        foreach (object specialField in recMapping.SpecialFields)
          recMapping1.RegisterSpecialField(specialField);
      }
    }
    return base.GetQueryParams(bookmark, count, recMapping1);
  }

  /// <summary>
  /// Возвращает таблицу, содержащую результаты запроса. Базовый класс
  /// вызывает этот метод, чтобы получить результаты запроса в формате
  /// источника данных, а затем транслирует их в унифицированный формат,
  /// понятный навигатору.
  /// </summary>
  /// <param name="queryParams">Параметры запроса к базе данных</param>
  /// <returns>Таблица с значениями атрибутов объектов</returns>
  protected override DataTable GetDataTable(DBRecordSetParams queryParams)
  {
    int length = this.mapping.Fields.Length;
    DataTable dataTable1 = base.GetDataTable(queryParams);
    if (dataTable1 == null || dataTable1.Rows.Count == 0 || this._mappingFix == null || this._mappingFix.Fields.Length == this.mapping.Fields.Length)
      return dataTable1;
    TechObjectListNode owner = this.Support is ObjectsPartBase support ? support.Owner as TechObjectListNode : (TechObjectListNode) null;
    TechObjectListVirtualDescriptor descriptor = owner != null ? owner.Descriptor as TechObjectListVirtualDescriptor : (TechObjectListVirtualDescriptor) null;
    if (descriptor == null || descriptor.VirtualData == null || descriptor.VirtualData.Count == 0)
      return dataTable1;
    List<object> objectList1 = new List<object>((IEnumerable<object>) this.mapping.Fields);
    List<object> objectList2 = new List<object>((IEnumerable<object>) this._mappingFix.Fields);
    for (int index = length; index < objectList1.Count; ++index)
      objectList2.Add(objectList1[index]);
    Dictionary<int, int> dictionary1 = new Dictionary<int, int>();
    Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
    DataTable dataTable2 = new DataTable(dataTable1.TableName);
    List<DataColumn> dataColumnList = new List<DataColumn>(objectList1.Count);
    foreach (object obj in objectList1)
    {
      int index = objectList2.IndexOf(obj);
      if (index != -1)
      {
        DataColumn column = dataTable1.Columns[index];
        int count = dataColumnList.Count;
        dataColumnList.Add(new DataColumn(count.ToString(), column.DataType, column.Expression, column.ColumnMapping));
        dictionary1.Add(count, index);
      }
      else
      {
        int count = dataColumnList.Count;
        DataColumn dataColumn = new DataColumn(count.ToString(), typeof (string));
        dataColumnList.Add(dataColumn);
        int result;
        if (obj is NodeColumnID nodeColumnId && nodeColumnId.ID != null && int.TryParse(nodeColumnId.ID.ToString(), out result))
          dictionary2.Add(count, result);
      }
    }
    dataTable2.Columns.AddRange(dataColumnList.ToArray());
    string empty = string.Empty;
    for (int index = 0; index < queryParams.Columns.Length; ++index)
    {
      object column = queryParams.Columns[index];
      if (column.GetType() == typeof (ObligatoryObjectAttributes) && (int) column == -2)
      {
        empty = index.ToString();
        break;
      }
    }
    TechObjectVirtualQuery.RowsDataList rowsDataList = new TechObjectVirtualQuery.RowsDataList();
    List<TechObjectListVirtualDescriptor.ObjVirtualField> objVirtualFieldList = new List<TechObjectListVirtualDescriptor.ObjVirtualField>(dataTable1.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
    {
      TechObjectVirtualQuery.RowsData rowsData = new TechObjectVirtualQuery.RowsData(dataTable2.Columns.Count);
      TechObjectListVirtualDescriptor.ObjVirtualField objVirtualField1 = (TechObjectListVirtualDescriptor.ObjVirtualField) null;
      if (empty != string.Empty)
      {
        long int64 = Convert.ToInt64(row[empty]);
        List<TechObjectListVirtualDescriptor.ObjVirtualField> fields4Object = descriptor.VirtualData.GetFields4Object(int64);
        if (fields4Object.Count == 1)
          objVirtualField1 = fields4Object[0];
        else if (fields4Object.Count > 1)
        {
          foreach (TechObjectListVirtualDescriptor.ObjVirtualField objVirtualField2 in fields4Object)
          {
            if (!objVirtualFieldList.Contains(objVirtualField2))
            {
              objVirtualField1 = objVirtualField2;
              break;
            }
          }
          if (objVirtualField1 != null)
            objVirtualFieldList.Add(objVirtualField1);
        }
      }
      for (int key = 0; key < dataTable2.Columns.Count; ++key)
      {
        int num;
        if (dictionary1.TryGetValue(key, out num))
          rowsData.Add(row[num]);
        else if (objVirtualField1 != null && dictionary2.TryGetValue(key, out num))
        {
          object fieldData = (object) DBNull.Value;
          int fieldIndex = objVirtualField1.GetFieldIndex(num);
          if (fieldIndex != -1 && objVirtualField1[fieldIndex].FieldData != null)
            fieldData = objVirtualField1[fieldIndex].FieldData;
          rowsData.Add(fieldData);
        }
        else
          rowsData.Add((object) DBNull.Value);
      }
      rowsDataList.Add(rowsData);
    }
    Dictionary<int, NodeColumnSortOrder> sortColList = new Dictionary<int, NodeColumnSortOrder>();
    for (int index = 0; index < this.mapping.SortFields.Length; ++index)
    {
      object sortField = this.mapping.SortFields[index];
      int key = objectList1.IndexOf(sortField);
      if (key != -1)
        sortColList.Add(key, this.mapping.SortOrders[index]);
    }
    if (sortColList.Count != 0)
    {
      TechObjectVirtualQuery.RowsDataComparer rowsDataComparer = new TechObjectVirtualQuery.RowsDataComparer(sortColList);
      rowsDataList.Sort((IComparer<TechObjectVirtualQuery.RowsData>) rowsDataComparer);
    }
    foreach (TechObjectVirtualQuery.RowsData rowsData in (List<TechObjectVirtualQuery.RowsData>) rowsDataList)
      dataTable2.Rows.Add(rowsData.ToArray());
    return dataTable2;
  }

  /// <summary>Объект - перечень значений строки таблицы</summary>
  protected class RowsData : List<object>
  {
    /// <summary>Конструктор</summary>
    public RowsData()
    {
    }

    /// <summary>Конструктор</summary>
    /// <param name="collection">The collection whose elements are copied to the new list.</param>
    public RowsData(IEnumerable<object> collection)
      : base(collection)
    {
    }

    /// <summary>Конструктор</summary>
    /// <param name="capacity">The number of elements that the new list can initially store.</param>
    public RowsData(int capacity)
      : base(capacity)
    {
    }
  }

  /// <summary>Перечень строк со значениями</summary>
  protected class RowsDataList : List<TechObjectVirtualQuery.RowsData>
  {
    /// <summary>Конструктор</summary>
    public RowsDataList()
    {
    }

    /// <summary>Конструктор</summary>
    /// <param name="collection">The collection whose elements are copied to the new list.</param>
    public RowsDataList(
      IEnumerable<TechObjectVirtualQuery.RowsData> collection)
      : base(collection)
    {
    }

    /// <summary>Конструктор</summary>
    /// <param name="capacity">The number of elements that the new list can initially store.</param>
    public RowsDataList(int capacity)
      : base(capacity)
    {
    }
  }

  /// <summary>Компарер строк</summary>
  protected class RowsDataComparer : IComparer<TechObjectVirtualQuery.RowsData>
  {
    /// <summary>Перечень столбцов с сортировкой</summary>
    private readonly Dictionary<int, NodeColumnSortOrder> _sortColList;

    /// <summary>Сравнение строк данных</summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="colIdx">Индекс столбца</param>
    /// <param name="sortOrder">Направление сортировки</param>
    /// <returns></returns>
    protected int Compare(
      TechObjectVirtualQuery.RowsData x,
      TechObjectVirtualQuery.RowsData y,
      int colIdx,
      NodeColumnSortOrder sortOrder)
    {
      if (colIdx == -1)
        return 0;
      if (x.Count <= colIdx)
        return y.Count > colIdx ? -1 : 0;
      if (y.Count <= colIdx)
        return x.Count > colIdx ? 1 : 0;
      object val1 = x[colIdx];
      object val2 = y[colIdx];
      int num = 0;
      if (val1 is IComparable)
        num = (val1 as IComparable).CompareTo(val2);
      else if (val2 is IComparable)
        num = (val2 as IComparable).CompareTo(val1);
      else if (val1 is MeasuredValue && val2 is MeasuredValue)
      {
        switch (MeasureHelper.Compare(val1 as MeasuredValue, val2 as MeasuredValue))
        {
          case CompareResult.Equal:
          case CompareResult.NotCompatible:
            num = 0;
            break;
          case CompareResult.More:
            num = 1;
            break;
          case CompareResult.Less:
            num = -1;
            break;
        }
      }
      if (num == 0 || sortOrder != NodeColumnSortOrder.Descending)
        return num;
      num = -num;
      return num;
    }

    /// <summary>Конструктор</summary>
    /// <param name="sortColList">Перечень столбцов с сортировкой</param>
    public RowsDataComparer(Dictionary<int, NodeColumnSortOrder> sortColList)
    {
      this._sortColList = sortColList;
    }

    /// <summary>Сравнение строк данных</summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public int Compare(TechObjectVirtualQuery.RowsData x, TechObjectVirtualQuery.RowsData y)
    {
      if (this._sortColList == null || this._sortColList.Count == 0 || x == y)
        return 0;
      if (y == null)
        return 1;
      if (x == null)
        return -1;
      int num = 0;
      foreach (KeyValuePair<int, NodeColumnSortOrder> sortCol in this._sortColList)
      {
        num = this.Compare(x, y, sortCol.Key, sortCol.Value);
        if (num != 0)
          break;
      }
      return num;
    }
  }
}
