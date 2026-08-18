
// Type: Intermech.Navigator.Controls.GroupingAndSortingColumns
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Вспомогательный класс, позволяющий сохранять набор группирующих и сортируемых колонок грида
/// </summary>
public sealed class GroupingAndSortingColumns
{
  /// <summary>Список колонок, по которым выполнено группирование</summary>
  public NodeColumnCollection GroupingColums;
  /// <summary>Список колонок, по которым выполнена сортировка</summary>
  public NodeColumnCollection SortedColums;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="groupingColums">Список колонок, по которым выполнено группирование</param>
  /// <param name="sortedColums">Список колонок, по которым выполнена сортировка</param>
  public GroupingAndSortingColumns(
    NodeColumnCollection groupingColums,
    NodeColumnCollection sortedColums)
  {
    this.GroupingColums = groupingColums;
    this.SortedColums = sortedColums;
  }
}
