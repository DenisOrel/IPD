
// Type: Intermech.Navigator.Controls.NavigatorTreeViewVisibleNodesContainer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections;
using System.Diagnostics;


namespace Intermech.Navigator.Controls;

/// <summary>Контейнер видимых узлов дерева "Навигатора"</summary>
internal class NavigatorTreeViewVisibleNodesContainer
{
  /// <summary>Список узлов</summary>
  private IList _nodes;

  /// <summary>Конструктор</summary>
  /// <param name="nodes">Список видимых узлов</param>
  public NavigatorTreeViewVisibleNodesContainer(IList nodes) => this._nodes = nodes;

  /// <summary>Возвращает количество видимых узлов.</summary>
  public int Count
  {
    [DebuggerStepThrough] get => this._nodes.Count;
  }

  /// <summary>
  /// Возвращает видимый узел, находящихся в указанной позиции.
  /// </summary>
  public NavigatorTreeNode this[int index]
  {
    [DebuggerStepThrough] get => this._nodes[index] as NavigatorTreeNode;
  }
}
