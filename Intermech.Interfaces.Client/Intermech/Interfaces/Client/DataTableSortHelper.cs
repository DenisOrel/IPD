// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.DataTableSortHelper
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Набор статических методов для подготовки к сортировке таблицы с данными DataTable
/// </summary>
public static class DataTableSortHelper
{
  private static readonly string _asc = " ASC";
  private static readonly string _desc = " DESC";

  public static void GetMeasuredColumnFilter(
    DataTable resultTable,
    StringBuilder sortString,
    string sourceColumn,
    List<string> needDeleted,
    NodeColumnSortOrder sortOrder)
  {
    DataTableSortHelper.GetMeasuredColumnFilter(resultTable, sortString, sourceColumn, needDeleted, (SortOrders) sortOrder);
  }

  public static void GetMeasuredColumnFilter(
    DataTable resultTable,
    StringBuilder sortString,
    string sourceColumn,
    List<string> needDeleted,
    SortOrders sortOrder)
  {
    string columnName = Convert.ToString(resultTable.Columns.Count);
    needDeleted.Add(columnName);
    DataColumn column = new DataColumn(columnName, typeof (double));
    resultTable.Columns.Add(column);
    for (int index = 0; index < resultTable.Rows.Count; ++index)
    {
      double num = 0.0;
      try
      {
        string mValue = Convert.ToString(resultTable.Rows[index][sourceColumn]);
        if (mValue != string.Empty)
        {
          MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(mValue);
          if (measuredValue != null)
            num = MeasureHelper.ConvertToBaseMeasure(measuredValue).Value;
        }
      }
      catch
      {
      }
      resultTable.Rows[index][columnName] = (object) num;
    }
    resultTable.AcceptChanges();
    sortString.AppendFormat("[{0}] {1},", (object) columnName, sortOrder == SortOrders.ASC ? (object) DataTableSortHelper._asc : (object) DataTableSortHelper._desc);
  }

  public static void GetPositionColumnFilter(
    DataTable resultTable,
    StringBuilder sortString,
    string sourceColumn,
    List<string> needDeleted,
    NodeColumnSortOrder sortOrder)
  {
    DataTableSortHelper.GetPositionColumnFilter(resultTable, sortString, sourceColumn, needDeleted, (SortOrders) sortOrder);
  }

  public static void GetPositionColumnFilter(
    DataTable resultTable,
    StringBuilder sortString,
    string sourceColumn,
    List<string> needDeleted,
    SortOrders sortOrder)
  {
    string columnName1 = Convert.ToString(resultTable.Columns.Count);
    needDeleted.Add(columnName1);
    resultTable.Columns.Add(new DataColumn(columnName1, typeof (long)));
    string columnName2 = Convert.ToString(resultTable.Columns.Count);
    needDeleted.Add(columnName2);
    resultTable.Columns.Add(new DataColumn(columnName2, typeof (string)));
    Regex regex = new Regex("^(?<digit>\\d+)(?<word>\\w+)$");
    for (int index = 0; index < resultTable.Rows.Count; ++index)
    {
      string str1 = Convert.ToString(resultTable.Rows[index][sourceColumn]);
      long result = long.MinValue;
      string str2 = string.Empty;
      if (!long.TryParse(str1, out result))
      {
        Match match = regex.Match(str1);
        if (match.Groups.Count > 1)
        {
          result = Convert.ToInt64(match.Groups["digit"].Value);
          str2 = match.Groups["word"].Value;
        }
        else
          str2 = str1;
      }
      resultTable.Rows[index][columnName1] = (object) result;
      resultTable.Rows[index][columnName2] = (object) str2;
    }
    resultTable.AcceptChanges();
    sortString.Append($"[{columnName1}] {(sortOrder == SortOrders.ASC ? (object) DataTableSortHelper._asc : (object) DataTableSortHelper._desc)},");
    sortString.Append($"[{columnName2}] {(sortOrder == SortOrders.ASC ? (object) DataTableSortHelper._asc : (object) DataTableSortHelper._desc)},");
  }
}
