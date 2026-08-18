
// Type: Intermech.Client.Core.SortService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Client.Core;

internal class SortService : ISortService
{
  private List<ISortColumnHandler> _sortColumnHandlers;

  public SortService()
  {
    this._sortColumnHandlers = new List<ISortColumnHandler>();
    this._sortColumnHandlers.Add((ISortColumnHandler) new MeasureSortColumnHandler());
    this._sortColumnHandlers.Add((ISortColumnHandler) new PositionSortColumnHandler());
  }

  public DataTable SortTable(DataTable table)
  {
    Dictionary<Type, ISortColumnHandler> dictionary1 = new Dictionary<Type, ISortColumnHandler>();
    SortedDictionary<int, string> sortedDictionary = new SortedDictionary<int, string>();
    Dictionary<int, ColumnAttributeData> dictionary2 = new Dictionary<int, ColumnAttributeData>();
    for (int index = 0; index < table.Columns.Count; ++index)
    {
      ColumnAttributeData extendedProperty = table.Columns[index].ExtendedProperties[(object) typeof (ColumnAttributeData)] as ColumnAttributeData;
      if (extendedProperty.Sort != SortOrders.NONE)
        dictionary2.Add(index, extendedProperty);
    }
    foreach (KeyValuePair<int, ColumnAttributeData> keyValuePair in dictionary2)
    {
      string sortSQL = string.Empty;
      for (int index = 0; index < this._sortColumnHandlers.Count; ++index)
      {
        ISortColumnHandler sortColumnHandler = this._sortColumnHandlers[index];
        if (sortColumnHandler.Handle(table, keyValuePair.Key, keyValuePair.Value, out sortSQL))
        {
          Type type = sortColumnHandler.GetType();
          if (!dictionary1.ContainsKey(type))
          {
            dictionary1.Add(type, sortColumnHandler);
            break;
          }
          break;
        }
      }
      if (sortSQL == string.Empty)
        sortSQL = $"[{table.Columns[keyValuePair.Key].ColumnName}] {(keyValuePair.Value.Sort == SortOrders.ASC ? (object) "ASC" : (object) "DESC")}";
      sortedDictionary.Add(keyValuePair.Value.OrderByID, sortSQL);
    }
    if (sortedDictionary.Count == 0)
      return table;
    string str1 = string.Empty;
    foreach (string str2 in sortedDictionary.Values)
      str1 = str1 + (str1.Length > 0 ? ", " : string.Empty) + str2;
    DataView dataView = new DataView(table);
    dataView.Sort = str1;
    foreach (ISortColumnHandler sortColumnHandler in dictionary1.Values)
      sortColumnHandler.AfterSorting(table);
    return dataView.ToTable();
  }
}
