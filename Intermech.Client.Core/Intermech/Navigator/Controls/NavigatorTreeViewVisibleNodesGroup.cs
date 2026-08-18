
// Type: Intermech.Navigator.Controls.NavigatorTreeViewVisibleNodesGroup
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections;
using System.Diagnostics;


namespace Intermech.Navigator.Controls;

/// <summary>Группа видимых узлов дерева "Навигатора"</summary>
internal class NavigatorTreeViewVisibleNodesGroup : NavigatorTreeViewVisibleNodesContainer
{
  /// <summary>Родительский узел</summary>
  private NavigatorTreeNode _parent;

  /// <summary>Конструктор</summary>
  /// <param name="node">Узел</param>
  /// <param name="parent">Родительский узел</param>
  public NavigatorTreeViewVisibleNodesGroup(NavigatorTreeNode node, NavigatorTreeNode parent)
    : this((IList) new NavigatorTreeNode[1]{ node }, parent)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="nodes">Узлы</param>
  /// <param name="parent">Родительский узел</param>
  public NavigatorTreeViewVisibleNodesGroup(IList nodes, NavigatorTreeNode parent)
    : base(nodes)
  {
    this._parent = parent;
  }

  /// <summary>
  /// Возвращает узел, являющийся непосредственным родителем для группы
  /// видимых узлов. Этот узел может быть невидимым.
  /// </summary>
  public NavigatorTreeNode Parent
  {
    [DebuggerStepThrough] get => this._parent;
  }
}
