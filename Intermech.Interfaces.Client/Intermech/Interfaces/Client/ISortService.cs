// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ISortService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Data;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Сервис сортировки данных</summary>
public interface ISortService
{
  /// <summary>
  /// Сортировка таблицы. Параметры сортировки задаются в расширенных свойствах столбцов:
  /// DataColumn.ExtendedProperties[typeof(ColumnAttributeData)] = new ColumnAttributeData(..)
  /// </summary>
  /// <param name="table"></param>
  DataTable SortTable(DataTable table);
}
