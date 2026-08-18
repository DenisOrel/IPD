
// Type: Intermech.Navigator.Controls.INavigatorTreeViewContextMenuHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.Controls;

/// <summary>
/// Интерфейс позволяет определить, для какого узла дерева "Навигатора" был выполнен вызов контекстного меню
/// </summary>
public interface INavigatorTreeViewContextMenuHelper
{
  /// <summary>Узел, в котором было вызвано контекстное меню</summary>
  NavigatorTreeNode MenuNode { get; }

  /// <summary>
  /// Дерево "Навигатора", в котором выполнен вызов контекстного меню
  /// </summary>
  NavigatorTreeView Tree { get; }

  /// <summary>
  /// Можно ли обработчику команды контекстного меню восстанавливать ранее сфокусированный узел в дереве
  /// </summary>
  bool CanRestoreFocusedNode { get; set; }
}
