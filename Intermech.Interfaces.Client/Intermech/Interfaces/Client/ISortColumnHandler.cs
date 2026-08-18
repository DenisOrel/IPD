// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ISortColumnHandler
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Data;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Обработчик сортируемой колонки</summary>
public interface ISortColumnHandler
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="table"></param>
  /// <param name="columnIndex"></param>
  /// <param name="attrData"></param>
  /// <param name="sortSQL"></param>
  /// <returns></returns>
  bool Handle(DataTable table, int columnIndex, ColumnAttributeData attrData, out string sortSQL);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="table"></param>
  void AfterSorting(DataTable table);
}
