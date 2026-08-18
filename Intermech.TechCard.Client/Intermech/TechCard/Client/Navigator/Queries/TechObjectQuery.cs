// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Navigator.Queries.TechObjectQuery
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using Intermech.TechCard.Client.Navigator.Descriptors;
using Intermech.TechCard.Client.Navigator.Nodes;
using Intermech.TechCard.Client.Navigator.Parts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.TechCard.Client.Navigator.Queries;

/// <summary>Techcard objects query</summary>
/// <summary>Constructor</summary>
/// <param name="support"></param>
/// <param name="objTypeID"></param>
/// <param name="conditions"></param>
/// <param name="services"></param>
public class TechObjectQuery(
  INodeQuerySupport support,
  int objTypeID,
  ConditionStructure[] conditions,
  IServiceProvider services) : ObjectsQuery(support, objTypeID, conditions, services)
{
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
    if (recMapping != null && recMapping.SortFields != null)
    {
      int capacity = Math.Min(recMapping.SortFields.Length, recMapping.SortOrders.Length);
      List<object> objectList = new List<object>(capacity);
      List<NodeColumnSortOrder> nodeColumnSortOrderList = new List<NodeColumnSortOrder>(capacity);
      for (int index = 0; index < capacity; ++index)
      {
        object sortField = recMapping.SortFields[index];
        if (!objectList.Contains(sortField))
        {
          objectList.Add(sortField);
          nodeColumnSortOrderList.Add(recMapping.SortOrders[0]);
        }
      }
      recMapping.SortFields = objectList.ToArray();
      recMapping.SortOrders = nodeColumnSortOrderList.ToArray();
    }
    return base.GetQueryParams(bookmark, count, recMapping);
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
    DataTable dataTable1 = base.GetDataTable(queryParams);
    if (dataTable1 == null || dataTable1.Rows.Count == 0)
      return dataTable1;
    TechObjectListNode owner = this.support is ObjectsPartBase support ? support.Owner as TechObjectListNode : (TechObjectListNode) null;
    TechObjectListDescriptor descriptor = owner != null ? owner.Descriptor as TechObjectListDescriptor : (TechObjectListDescriptor) null;
    if (descriptor == null || descriptor.Mode != TechObjectListMode.MultiValue)
      return dataTable1;
    string empty = string.Empty;
    for (int index = 0; index < queryParams.Columns.Length; ++index)
    {
      object column = queryParams.Columns[index];
      if (column.GetType() == typeof (ObligatoryObjectAttributes) && (int) column == -2)
        empty = index.ToString();
    }
    if (empty == string.Empty)
      return dataTable1;
    this.mapping.RegisterSpecialField((object) TechObjectListPart.ncF_PRJLINK_ID);
    DataTable dataTable2 = new DataTable(dataTable1.TableName);
    List<DataColumn> dataColumnList = new List<DataColumn>(dataTable1.Columns.Count);
    foreach (DataColumn column in (InternalDataCollectionBase) dataTable1.Columns)
      dataColumnList.Add(new DataColumn(column.ColumnName, column.DataType, column.Expression, column.ColumnMapping));
    DataColumn dataColumn = new DataColumn(dataColumnList.Count.ToString(), typeof (long));
    dataColumnList.Add(dataColumn);
    dataTable2.Columns.AddRange(dataColumnList.ToArray());
    Hashtable hashtable = new Hashtable(dataTable1.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
      hashtable.Add((object) Convert.ToInt64(row[empty]), (object) row);
    int num = 1;
    foreach (long objectId in (IEnumerable) owner.ObjectIDs)
    {
      if (hashtable.ContainsKey((object) objectId))
      {
        List<object> objectList = new List<object>((IEnumerable<object>) ((DataRow) hashtable[(object) objectId]).ItemArray)
        {
          (object) num++
        };
        dataTable2.Rows.Add(objectList.ToArray());
      }
    }
    return dataTable2;
  }
}
