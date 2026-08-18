// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.ContextMenu.AdjustableMenusHelper
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces.Client;

#nullable disable
namespace Intermech.Navigator.ContextMenu;

/// <summary>
/// Вспомогательный статический класс для работы с коллекциями настраиваемых команд
/// </summary>
public static class AdjustableMenusHelper
{
  /// <summary>
  /// Построить коллекцию настраиваемых команд на основании указанного шаблона меню
  /// </summary>
  /// <param name="menuTemplate">Шаблон меню</param>
  /// <returns>Коллекция настраиваемых команд</returns>
  public static AdjustableMenuCommands BuildFromMenuTemplate(MenuTemplate menuTemplate)
  {
    AdjustableMenuCommands parent = new AdjustableMenuCommands((AdjustableMenuCommands) null);
    if (menuTemplate == null || menuTemplate.Nodes.Count == 0)
      return parent;
    for (int index = 0; index < menuTemplate.Nodes.Count; ++index)
    {
      MenuTemplateNode node = menuTemplate.Nodes[index];
      AdjustableMenusHelper.BuildFromMenuTemplateNode(parent, node);
    }
    parent.Sort();
    return parent;
  }

  /// <summary>
  /// Построить коллекцию настраиваемых команд на основании указанного шаблона узла меню
  /// </summary>
  /// <param name="parent">Родительская коллекция настраиваемых команд</param>
  /// <param name="menuTemplateNode">Шаблон узла меню</param>
  /// <returns>Коллекция настраиваемых команд или null</returns>
  private static void BuildFromMenuTemplateNode(
    AdjustableMenuCommands parent,
    MenuTemplateNode menuTemplateNode)
  {
    if (parent == null || menuTemplateNode == null)
      return;
    AdjustableMenuCommand adjustableMenuCommand = parent.Add(menuTemplateNode.Name, menuTemplateNode.Text, string.Empty, menuTemplateNode.ImageIndex, menuTemplateNode.Visible, menuTemplateNode.GroupID, menuTemplateNode.OrderID, menuTemplateNode.Shortcut, menuTemplateNode.ImageListSource);
    for (int index = 0; index < menuTemplateNode.Nodes.Count; ++index)
    {
      MenuTemplateNode node = menuTemplateNode.Nodes[index];
      AdjustableMenusHelper.BuildFromMenuTemplateNode(adjustableMenuCommand.Items, node);
    }
  }

  /// <summary>
  /// Построить шаблон контекстного меню на основании указанной коллекции настраиваемых команд
  /// </summary>
  /// <param name="menuCommands">Коллекция настраиваемых команд</param>
  /// <returns>Шаблон контекстного меню</returns>
  public static MenuTemplate BuildMenuTemplate(AdjustableMenuCommands menuCommands)
  {
    (ServicesManager.GetService(typeof (IHotKeysManager)) as IHotKeysManager).UnregisterHotKeys();
    MenuTemplate parent = new MenuTemplate();
    if (menuCommands == null || menuCommands.Count == 0)
      return parent;
    for (int index = 0; index < menuCommands.Count; ++index)
    {
      AdjustableMenuCommand menuCommand = menuCommands[index];
      AdjustableMenusHelper.BuildMenuTemplateNodeTop(parent, menuCommand);
    }
    return parent;
  }

  /// <summary>
  /// Построить узел шаблона контекстного меню на основе настраиваемой команды
  /// </summary>
  /// <param name="parent">Шаблон узла контекстного меню</param>
  /// <param name="command">Настраиваемая команда контекстного меню</param>
  private static void BuildMenuTemplateNode(MenuTemplateNode parent, AdjustableMenuCommand command)
  {
    if (parent == null || command == null)
      return;
    MenuTemplateNode menuTemplateNode = new MenuTemplateNode(command.Command, command.Caption, command.ImageIndex, command.Group, command.OrderBy, command.Shortcut, command.Visible, command.ImageListSource);
    parent.Nodes.Add(menuTemplateNode);
    for (int index = 0; index < command.Items.Count; ++index)
    {
      AdjustableMenuCommand command1 = command.Items[index];
      AdjustableMenusHelper.BuildMenuTemplateNode(menuTemplateNode, command1);
    }
  }

  /// <summary>
  /// Построить узел верхнего уровня в шаблоне контекстного меню на основании настраиваемой команды
  /// </summary>
  /// <param name="parent">Шаблон контекстного меню</param>
  /// <param name="command">Настраиваемая команда контекстного меню</param>
  private static void BuildMenuTemplateNodeTop(MenuTemplate parent, AdjustableMenuCommand command)
  {
    if (parent == null || command == null)
      return;
    MenuTemplateNode menuTemplateNode = new MenuTemplateNode(command.Command, command.Caption, command.ImageIndex, command.Group, command.OrderBy, command.Shortcut, command.Visible, command.ImageListSource);
    parent.Nodes.Add(menuTemplateNode);
    for (int index = 0; index < command.Items.Count; ++index)
      AdjustableMenusHelper.BuildMenuTemplateNode(menuTemplateNode, command.Items[index]);
  }
}
