// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Views.iFocusAndSelection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Navigator.Interfaces;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Navigator.Views;

/// <summary>
/// Хранит идентификаторы сфокусированной и выделенных в гриде/дереве записей.
/// Используется для сохранения/восстановления состояния грида/дерева.
/// </summary>
public sealed class iFocusAndSelection
{
  /// <summary>Идентификатор сфокусированной строки</summary>
  public INodeID FocusedRow;
  /// <summary>Список идентификаторов выделенных строк</summary>
  public List<INodeID> SelectedRows;
  /// <summary>Индекс сфокусированной строки</summary>
  public int FocusedIndex;
  /// <summary>Список индексов выделенных строк</summary>
  public List<int> SelectedIndexes;
  /// <summary>
  /// Название активной вложенной странички (String.Empty - вложенные закладки были закрыты)
  /// </summary>
  public string ActivePage;
  /// <summary>
  /// Настройки сфокусированной и выделенной записей во вложенных закладках
  /// </summary>
  public iFocusAndSelection SubviewSelection;

  /// <summary>Высота дочернего вида</summary>
  public int ChildrenViewHeight { get; set; }

  public HashSet<string> CollapsedGroups { get; set; }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="focusedRow">Идентификатор сфокусированной строки</param>
  /// <param name="selectedRows">Список идентификаторов выделенных строк</param>
  /// <param name="focusedIndex">Индекс сфокусированной строки</param>
  /// <param name="selectedIndexes">Список индексов выделенных строк</param>
  /// <param name="activePage">Название активной вложенной странички (String.Empty - вложенные закладки были закрыты)</param>
  /// <param name="subviewSelection">Настройки сфокусированной и выделенной записей во вложенных закладках</param>
  public iFocusAndSelection(
    INodeID focusedRow,
    List<INodeID> selectedRows,
    int focusedIndex,
    List<int> selectedIndexes,
    string activePage,
    iFocusAndSelection subviewSelection)
  {
    this.FocusedRow = focusedRow;
    this.SelectedRows = selectedRows;
    this.FocusedIndex = focusedIndex;
    this.SelectedIndexes = selectedIndexes;
    this.ActivePage = activePage;
    this.SubviewSelection = subviewSelection;
  }
}
