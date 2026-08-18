// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.NodeContextMenu
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Bars;
using Intermech.Document.UI;
using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Вспомогательный класс для генерации контекстного меню</summary>
[Serializable]
public class NodeContextMenu
{
  /// <summary>Команда относится к контекстному меню</summary>
  public static bool ContextMenuCommand = false;
  private static DocumentTreeNode[] contextForContextMenu = (DocumentTreeNode[]) null;
  private static Dictionary<string, MenuButtonItem> contextMenuDictionary = new Dictionary<string, MenuButtonItem>();

  /// <summary>Контекст контекстного меню. Т.е. элементы для которых было вызвано контекстное меню</summary>
  public static DocumentTreeNode[] ContextForContextMenu
  {
    [DebuggerStepThrough] get => NodeContextMenu.contextForContextMenu;
    set => NodeContextMenu.contextForContextMenu = value;
  }

  /// <summary>Получить пункт контекстного меню</summary>
  /// <param name="commandName">Имя команды</param>
  /// <returns>Пункт контекстного меню</returns>
  public static MenuButtonItem GetContextMenuItem(string commandName)
  {
    if (commandName == null)
      throw new ArgumentNullException(nameof (commandName));
    MenuButtonItem contextMenuItem;
    NodeContextMenu.contextMenuDictionary.TryGetValue(commandName, out contextMenuItem);
    return contextMenuItem;
  }

  /// <summary>Получить позицию пункта контекстного меню</summary>
  /// <param name="commandName">Имя команды</param>
  /// <param name="menuItemList">список пунктов меню в котором искать</param>
  /// <returns>Позиция</returns>
  public static int GetContextMenuItemIndex(string commandName, List<MenuButtonItem> menuItemList)
  {
    if (commandName == null)
      throw new ArgumentNullException(nameof (commandName));
    for (int index = 0; index < menuItemList.Count; ++index)
    {
      if (menuItemList[index].CommandName == commandName)
        return index;
    }
    return -1;
  }

  /// <summary>Установить пункт контекстного меню для команды</summary>
  /// <param name="commandName">Имя команды</param>
  /// <param name="menuItem">Пункт контекстного меню</param>
  public static void SetContextMenuItem(string commandName, MenuButtonItem menuItem)
  {
    if (commandName == null)
      throw new ArgumentNullException(nameof (commandName));
    if (NodeContextMenu.contextMenuDictionary.ContainsKey(commandName))
      NodeContextMenu.contextMenuDictionary[commandName] = menuItem;
    else
      NodeContextMenu.contextMenuDictionary.Add(commandName, menuItem);
  }

  private static MenuButtonItem AddIfEnabledContextMenuItem(
    string commandName,
    List<MenuButtonItem> defContextMenu,
    ICommandManager commandManager)
  {
    MenuButtonItem contextMenuItem = NodeContextMenu.GetContextMenuItem(commandName);
    if (contextMenuItem != null)
    {
      if (commandManager != null)
      {
        ICommandState command = commandManager.FindCommand(contextMenuItem.CommandName);
        if (command != null)
        {
          commandManager.Add((ButtonItemBase) contextMenuItem);
          commandManager.QueryStatus(command);
          if (contextMenuItem.Enabled != command.Enabled)
            contextMenuItem.Enabled = command.Enabled;
        }
      }
      if (contextMenuItem.Enabled)
      {
        contextMenuItem.Visible = contextMenuItem.Enabled;
        defContextMenu.Add(contextMenuItem);
        return contextMenuItem;
      }
    }
    return (MenuButtonItem) null;
  }

  private static MenuButtonItem AddContextMenuItem(
    string commandName,
    List<MenuButtonItem> defContextMenu,
    ICommandManager commandManager)
  {
    MenuButtonItem contextMenuItem = NodeContextMenu.GetContextMenuItem(commandName);
    if (contextMenuItem == null)
      return (MenuButtonItem) null;
    if (commandManager != null)
    {
      ICommandState command = commandManager.FindCommand(contextMenuItem.CommandName);
      if (command != null)
      {
        commandManager.Add((ButtonItemBase) contextMenuItem);
        commandManager.QueryStatus(command);
        if (contextMenuItem.Enabled != command.Enabled)
          contextMenuItem.Enabled = command.Enabled;
      }
    }
    defContextMenu.Add(contextMenuItem);
    return contextMenuItem;
  }

  /// <summary>Получить контекстное меню для заданного контекста</summary>
  /// <param name="docControl">Контрол документа</param>
  /// <param name="commandManager">CommandManager</param>
  /// <param name="context">Узлы дерева для которых было вызвано контекстное меню</param>
  /// <returns>Список пунктов контекстного меню</returns>
  public static List<MenuButtonItem> GetContextMenu(
    DocumentControl docControl,
    ICommandManager commandManager,
    DocumentTreeNode[] context)
  {
    List<MenuButtonItem> defContextMenu = new List<MenuButtonItem>(3);
    NodeContextMenu.ContextForContextMenu = context;
    NodeContextMenu.ContextMenuCommand = true;
    if (context == null || context.Length == 0)
      return defContextMenu;
    NodeContextMenu.AddContextMenuItem("Cut", defContextMenu, commandManager);
    NodeContextMenu.AddContextMenuItem("Copy", defContextMenu, commandManager);
    NodeContextMenu.AddContextMenuItem("Paste", defContextMenu, commandManager);
    NodeContextMenu.AddContextMenuItem("Delete", defContextMenu, commandManager);
    NodeContextMenu.AddIfEnabledContextMenuItem("CallEditor", defContextMenu, commandManager);
    bool flag = false;
    NodeContextMenu.AddIfEnabledContextMenuItem("AddToUserDictionary", defContextMenu, commandManager);
    MenuButtonItem menuButtonItem1 = NodeContextMenu.AddIfEnabledContextMenuItem("MoveToBegin", defContextMenu, commandManager);
    if (menuButtonItem1 != null)
    {
      menuButtonItem1.BeginGroup = !flag;
      flag = true;
    }
    MenuButtonItem menuButtonItem2 = NodeContextMenu.AddIfEnabledContextMenuItem("MoveUp", defContextMenu, commandManager);
    if (menuButtonItem2 != null)
    {
      menuButtonItem2.BeginGroup = !flag;
      flag = true;
    }
    MenuButtonItem menuButtonItem3 = NodeContextMenu.AddIfEnabledContextMenuItem("MoveDown", defContextMenu, commandManager);
    if (menuButtonItem3 != null)
    {
      menuButtonItem3.BeginGroup = !flag;
      flag = true;
    }
    MenuButtonItem menuButtonItem4 = NodeContextMenu.AddIfEnabledContextMenuItem("MoveToEnd", defContextMenu, commandManager);
    if (menuButtonItem4 != null)
      menuButtonItem4.BeginGroup = !flag;
    NodeContextMenu.AddIfEnabledContextMenuItem("BlockGeometryChanging", defContextMenu, commandManager);
    NodeContextMenu.AddIfEnabledContextMenuItem("UnblockGeometryChanging", defContextMenu, commandManager);
    MenuButtonItem menuButtonItem5 = NodeContextMenu.AddIfEnabledContextMenuItem("ConvertToLabel", defContextMenu, commandManager);
    MenuButtonItem menuButtonItem6 = NodeContextMenu.AddIfEnabledContextMenuItem("ConvertToTextBox", defContextMenu, commandManager);
    if (menuButtonItem6 != null)
      menuButtonItem6.BeginGroup = menuButtonItem5 == null;
    MenuButtonItem menuButtonItem7 = NodeContextMenu.AddIfEnabledContextMenuItem("ConvertToContainer", defContextMenu, commandManager);
    if (menuButtonItem7 != null)
      menuButtonItem7.BeginGroup = menuButtonItem5 == null && menuButtonItem6 == null;
    MenuButtonItem menuButtonItem8 = NodeContextMenu.AddIfEnabledContextMenuItem("ConvertToArea", defContextMenu, commandManager);
    if (menuButtonItem8 != null)
      menuButtonItem8.BeginGroup = menuButtonItem5 == null && menuButtonItem6 == null && menuButtonItem7 == null;
    MenuButtonItem menuButtonItem9 = NodeContextMenu.AddIfEnabledContextMenuItem("ApplyPreviousTable", defContextMenu, commandManager);
    if (menuButtonItem9 != null)
      menuButtonItem9.BeginGroup = menuButtonItem5 == null && menuButtonItem6 == null && menuButtonItem7 == null && menuButtonItem8 == null;
    NodeContextMenu.AddIfEnabledContextMenuItem("RemoveRow", defContextMenu, commandManager);
    NodeContextMenu.AddIfEnabledContextMenuItem("RemoveColumn", defContextMenu, commandManager);
    NodeContextMenu.AddIfEnabledContextMenuItem("RemoveCell", defContextMenu, commandManager);
    NodeContextMenu.AddIfEnabledContextMenuItem("AddTableRowAbove", defContextMenu, commandManager);
    NodeContextMenu.AddIfEnabledContextMenuItem("AddTableRowBelow", defContextMenu, commandManager);
    NodeContextMenu.AddIfEnabledContextMenuItem("AddRowFromTemplateAbove", defContextMenu, commandManager);
    NodeContextMenu.AddIfEnabledContextMenuItem("AddRowFromTemplateBelow", defContextMenu, commandManager);
    NodeContextMenu.AddIfEnabledContextMenuItem("AddTableColumnLeft", defContextMenu, commandManager);
    NodeContextMenu.AddIfEnabledContextMenuItem("AddTableColumnRight", defContextMenu, commandManager);
    NodeContextMenu.AddIfEnabledContextMenuItem("AddTableCell", defContextMenu, commandManager);
    NodeContextMenu.AddIfEnabledContextMenuItem("AddTableSection", defContextMenu, commandManager);
    NodeContextMenu.AddIfEnabledContextMenuItem("SplitCell", defContextMenu, commandManager);
    NodeContextMenu.AddIfEnabledContextMenuItem("MergeCells", defContextMenu, commandManager);
    NodeContextMenu.AddIfEnabledContextMenuItem("ConvertToHeader", defContextMenu, commandManager);
    NodeContextMenu.AddIfEnabledContextMenuItem("UpdateTable", defContextMenu, commandManager);
    NodeContextMenu.AddIfEnabledContextMenuItem("ApplyPreviousTable", defContextMenu, commandManager);
    NodeContextMenu.AddIfEnabledContextMenuItem("SelectContinuationTable", defContextMenu, commandManager);
    NodeContextMenu.AddIfEnabledContextMenuItem("LoadOleFile", defContextMenu, commandManager);
    NodeContextMenu.AddIfEnabledContextMenuItem("CreateOleObject", defContextMenu, commandManager);
    NodeContextMenu.AddIfEnabledContextMenuItem("SaveImageToFile", defContextMenu, commandManager);
    NodeContextMenu.AddIfEnabledContextMenuItem("DocEditor.InsertAdditionalPages", defContextMenu, commandManager);
    NodeContextMenu.AddIfEnabledContextMenuItem("DocEditor.RemoveAdditionalPages", defContextMenu, commandManager);
    NodeContextMenu.AddIfEnabledContextMenuItem("DocEditor.ChangePageNumberingStyle", defContextMenu, commandManager);
    NodeContextMenu.AddIfEnabledContextMenuItem("ChangeVisibility", defContextMenu, commandManager);
    NodeContextMenu.AddIfEnabledContextMenuItem("Tree.Update", defContextMenu, commandManager);
    if (context.Length == 1 && context[0] is Page)
    {
      NodeContextMenu.AddIfEnabledContextMenuItem("CreateNextPageTemplate", defContextMenu, commandManager);
      NodeContextMenu.AddIfEnabledContextMenuItem("SelectAll", defContextMenu, commandManager);
    }
    return defContextMenu;
  }

  /// <summary>Добавить пункт к контекстному меню</summary>
  /// <param name="contextMenu">Контекстное меню</param>
  /// <param name="menuItemList">Список пунктов контекстного меню</param>
  public static void AddToContextMenu(
    ContextMenuBarItem contextMenu,
    List<MenuButtonItem> menuItemList)
  {
    if (menuItemList == null || menuItemList.Count <= 0)
      return;
    MenuButtonItem[] menuButtonItemArray = new MenuButtonItem[menuItemList.Count];
    menuItemList.CopyTo(menuButtonItemArray);
    contextMenu.Items.AddRange((ToolbarItemBase[]) menuButtonItemArray);
  }

  /// <summary>Добавить пункт к контекстному меню</summary>
  /// <param name="contextMenu">Контекстное меню</param>
  /// <param name="menuItemList">Список пунктов контекстного меню</param>
  public static void AddToContextMenu(
    ContextMenuBarItem contextMenu,
    List<ToolbarItemBase> menuItemList)
  {
    if (menuItemList == null || menuItemList.Count <= 0)
      return;
    contextMenu.Items.AddRange(menuItemList.ToArray());
  }
}
