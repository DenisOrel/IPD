
// Type: Intermech.Navigator.Controls.NavigatorTreeViewContextMenuHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Класс позволяет определить, для какого узла дерева "Навигатора" был выполнен вызов контекстного меню
/// </summary>
public sealed class NavigatorTreeViewContextMenuHelper : INavigatorTreeViewContextMenuHelper
{
  /// <summary>Создать экземпляр класса</summary>
  /// <param name="tree">Дерево "Навигатора", в котором выполнен вызов контекстного меню</param>
  /// <param name="menuNode">Узел, в котором было вызвано контекстное меню</param>
  /// <param name="focusedNode">Текущий узел в дереве "Навигатора"</param>
  /// <param name="canRestoreFocusedNode">Можно ли обработчику команды контекстного меню восстанавливать ранее сфокусированный узел в дереве</param>
  public NavigatorTreeViewContextMenuHelper(NavigatorTreeView tree)
  {
    this.Tree = tree != null ? tree : throw new ArgumentNullException(nameof (tree));
  }

  public NavigatorTreeView Tree { get; private set; }

  public NavigatorTreeNode MenuNode { get; internal set; }

  public bool CanRestoreFocusedNode { get; set; }
}
