
// Type: Intermech.Client.Core.SortColumnHandler
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

/// <summary>Базовый класс для обработчиков</summary>
internal abstract class SortColumnHandler : ISortColumnHandler
{
  protected string additionalColumnNamePrefix;

  public SortColumnHandler(string additionalColumnNamePrefix)
  {
    this.additionalColumnNamePrefix = additionalColumnNamePrefix;
  }

  protected DataColumn NewAdditionalColumn(DataTable table, Type dataType)
  {
    return table.Columns.Add($"{this.additionalColumnNamePrefix}{Guid.NewGuid()}", dataType);
  }

  protected string GetSortOrdersString(SortOrders sortOrder)
  {
    if (sortOrder == SortOrders.ASC)
      return "ASC";
    if (sortOrder == SortOrders.DESC)
      return "DESC";
    throw new ArgumentOutOfRangeException();
  }

  protected string ColumnNameInSQL(string columnName) => $"[{columnName}]";

  public abstract bool Handle(
    DataTable table,
    int columnIndex,
    ColumnAttributeData attrData,
    out string sortSQL);

  public virtual void AfterSorting(DataTable table)
  {
    List<string> stringList = new List<string>();
    for (int index = 0; index < table.Columns.Count; ++index)
    {
      if (table.Columns[index].ColumnName.StartsWith(this.additionalColumnNamePrefix))
        stringList.Add(table.Columns[index].ColumnName);
    }
    for (int index = 0; index < stringList.Count; ++index)
      table.Columns.Remove(stringList[index]);
  }
}
