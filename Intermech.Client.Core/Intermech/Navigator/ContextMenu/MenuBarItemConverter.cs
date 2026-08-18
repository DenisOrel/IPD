
// Type: Intermech.Navigator.ContextMenu.MenuBarItemConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Controls;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Search;
using Intermech.Search.Configuration;
using Intermech.Search.UI.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;


namespace Intermech.Navigator.ContextMenu;

/// <summary>
/// Реализует алгоритм преобразования таблицы команд в
/// контекстное меню на основе Intermech.Bars.
/// </summary>
internal class MenuBarItemConverter : IConverter
{
  private MenuBar _menuBar;
  private static IFactory _factory;
  private static ICategoryTypeIconService _categoryTypeIconService;
  private LazyService<ICommandStatisticsRepository> _commandStatisticsRepository = new LazyService<ICommandStatisticsRepository>();

  public MenuBarItemConverter()
  {
    this._menuBar = new MenuBar();
    this._menuBar.FullMenus = false;
    if (Holder.BarManager == null || Holder.BarManager.MenuBar == null)
      return;
    this._menuBar.ImageList = Holder.BarManager.MenuBar.ImageList;
    this._menuBar.ShortcutListener = Holder.BarManager.MenuBar.ShortcutListener;
  }

  /// <summary>Возвращает построенное контекстное меню</summary>
  /// <returns>Контекстное меню</returns>
  public Component ToContextMenu(CommandsTable commandsTable, IServiceProvider viewServices)
  {
    Services.Check(commandsTable);
    Services.Check(viewServices);
    if (MenuBarItemConverter._factory == null)
    {
      MenuBarItemConverter._factory = ServicesManager.GetService(typeof (IFactory)) as IFactory;
      MenuBarItemConverter._categoryTypeIconService = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    }
    lock (Holder.Factory.ContextMenuTemplate)
      return (Component) this.CreatePopupMenuBarItem(commandsTable, Holder.Factory.ConfiguredContextMenuTemplate, viewServices);
  }

  /// <summary>
  /// Обработчик нажатия на пункт контекстного меню. Запускает команду на выполнение.
  /// </summary>
  private static void ClickHandler(object sender, EventArgs e)
  {
    if (!(sender is MenuButtonItem menuButtonItem) || menuButtonItem.Tag == null)
      return;
    ((CommandHelper) menuButtonItem.Tag).Execute(menuButtonItem.Text);
    menuButtonItem.Tag = (object) null;
  }

  private PopupMenuBarItem CreatePopupMenuBarItem(
    CommandsTable commandsTable,
    MenuTemplate menuTemplate,
    IServiceProvider serviceProvider)
  {
    PopupMenuBarItem parentMenuItem = new PopupMenuBarItem();
    parentMenuItem.PopupHost = (IPopupMenuHost) this._menuBar;
    this.CreateChildrenMenuButtonItems((MenuItemBase) parentMenuItem, commandsTable, serviceProvider, menuTemplate.Nodes);
    return parentMenuItem;
  }

  private void CreateChildrenMenuButtonItems(
    MenuItemBase parentMenuItem,
    CommandsTable commandsTable,
    IServiceProvider serviceProvider,
    MenuTemplateNodeCollection menuTemplateNodeCollection)
  {
    List<MenuButtonItem> expandedMenu = this.CreateExpandedMenu(commandsTable, serviceProvider, menuTemplateNodeCollection);
    if (CoreConfigurationOptions.UI_MinimizeContextMenu && CoreConfigurationOptions.UI_MinimizedContextMenuCommandsCount < (long) expandedMenu.Count)
    {
      List<MenuButtonItem> collapsedMenu = this.CreateCollapsedMenu(expandedMenu);
      foreach (MenuButtonItem menuButtonItem in expandedMenu)
      {
        MenuButtonItem menuItem = menuButtonItem;
        if (collapsedMenu.Where<MenuButtonItem>((Func<MenuButtonItem, bool>) (o => o == menuItem)).Count<MenuButtonItem>() > 0)
          menuItem.Importance = ToolBarItemImportance.Medium;
        else
          menuItem.Importance = ToolBarItemImportance.Low;
      }
    }
    parentMenuItem.Items.AddRange((ToolbarItemBase[]) expandedMenu.ToArray());
  }

  private List<MenuButtonItem> CreateCollapsedMenu(List<MenuButtonItem> menuItems)
  {
    Dictionary<string, CommandStatistics> source = new Dictionary<string, CommandStatistics>();
    foreach (MenuButtonItem menuItem in menuItems)
    {
      CommandStatistics statisticsForCommand = this.FindStatisticsForCommand((MenuItemBase) menuItem);
      if (statisticsForCommand != null)
        source[menuItem.CommandName] = statisticsForCommand;
    }
    List<string> filtered = source.OrderByDescending<KeyValuePair<string, CommandStatistics>, int>((Func<KeyValuePair<string, CommandStatistics>, int>) (o => o.Value.CurrentSessionUsesCount)).ThenByDescending<KeyValuePair<string, CommandStatistics>, long>((Func<KeyValuePair<string, CommandStatistics>, long>) (o => o.Value.TotalUsesCount)).Select<KeyValuePair<string, CommandStatistics>, string>((Func<KeyValuePair<string, CommandStatistics>, string>) (o => o.Key)).Take<string>((int) CoreConfigurationOptions.UI_MinimizedContextMenuCommandsCount).ToList<string>();
    return menuItems.Where<MenuButtonItem>((Func<MenuButtonItem, bool>) (o => filtered.Contains(o.CommandName))).ToList<MenuButtonItem>();
  }

  private CommandStatistics FindStatisticsForCommand(MenuItemBase menuItem)
  {
    if (string.IsNullOrEmpty(menuItem.CommandName))
      return (CommandStatistics) null;
    return menuItem.Items.Count == 0 ? this._commandStatisticsRepository.Value.Find(menuItem.CommandName) ?? new CommandStatistics() : menuItem.Items.Cast<MenuItemBase>().Select<MenuItemBase, CommandStatistics>((Func<MenuItemBase, CommandStatistics>) (o => this.FindStatisticsForCommand(o))).Where<CommandStatistics>((Func<CommandStatistics, bool>) (o => o != null)).OrderByDescending<CommandStatistics, int>((Func<CommandStatistics, int>) (o => o.CurrentSessionUsesCount)).ThenByDescending<CommandStatistics, long>((Func<CommandStatistics, long>) (o => o.TotalUsesCount)).FirstOrDefault<CommandStatistics>() ?? new CommandStatistics();
  }

  private List<MenuButtonItem> CreateExpandedMenu(
    CommandsTable commandsTable,
    IServiceProvider serviceProvider,
    MenuTemplateNodeCollection menuTemplateNodeCollection)
  {
    List<MenuButtonItem> expandedMenu = new List<MenuButtonItem>();
    MenuTemplateNode menuTemplateNode1 = (MenuTemplateNode) null;
    foreach (MenuTemplateNode menuTemplateNode2 in menuTemplateNodeCollection)
    {
      if (((IEnumerable<string>) commandsTable.CommandNames).Contains<string>(menuTemplateNode2.Name) || this.IsAllowableGroupNode(menuTemplateNode2, commandsTable))
      {
        MenuButtonItem menuButtonItem = this.CreateMenuButtonItem(commandsTable, serviceProvider, menuTemplateNode2);
        if (menuTemplateNode1 != null && menuTemplateNode1.GroupID != menuTemplateNode2.GroupID)
          menuButtonItem.BeginGroup = true;
        menuTemplateNode1 = menuTemplateNode2;
        expandedMenu.Add(menuButtonItem);
      }
    }
    return expandedMenu;
  }

  private bool IsAllowableGroupNode(MenuTemplateNode node, CommandsTable commandsTable)
  {
    foreach (MenuTemplateNode node1 in node.Nodes)
    {
      if (((IEnumerable<string>) commandsTable.CommandNames).Contains<string>(node1.Name))
        return true;
    }
    return false;
  }

  private MenuButtonItem CreateMenuButtonItem(
    CommandsTable commandsTable,
    IServiceProvider serviceProvider,
    MenuTemplateNode menuTemplateNode)
  {
    CommandHelper commandHelper = (CommandHelper) null;
    if (!string.IsNullOrEmpty(menuTemplateNode.Name) && ((IEnumerable<string>) commandsTable.CommandNames).Contains<string>(menuTemplateNode.Name))
      commandHelper = new CommandHelper(menuTemplateNode.Name, commandsTable[menuTemplateNode.Name], serviceProvider);
    MenuButtonItem menuButtonItem = this.CreateMenuButtonItem(menuTemplateNode, commandHelper, serviceProvider);
    this.CreateChildrenMenuButtonItems((MenuItemBase) menuButtonItem, commandsTable, serviceProvider, menuTemplateNode.Nodes);
    return menuButtonItem;
  }

  private MenuButtonItem CreateMenuButtonItem(
    MenuTemplateNode menuTemplateNode,
    CommandHelper commandHelper,
    IServiceProvider serviceProvider)
  {
    ISelectedItems items = commandHelper == null || commandHelper.CommandLink == null || commandHelper.CommandLink.ItemsLink == null ? (ISelectedItems) null : commandHelper.CommandLink.ItemsLink.Items;
    MenuButtonItem menuButtonItem = new MenuButtonItem();
    string text = menuTemplateNode.Text;
    int imageIndex = menuTemplateNode.ImageIndex;
    Image image = menuTemplateNode.Image;
    try
    {
      MenuBarItemConverter._factory.MenuTemplateNodeTransform(menuTemplateNode, items, serviceProvider);
      menuButtonItem.Text = menuTemplateNode.Text;
      menuButtonItem.ImageIndex = menuTemplateNode.ImageIndex;
      menuButtonItem.Image = menuTemplateNode.ImageListSource == ImageListSource.CategoryImageList ? MenuBarItemConverter._categoryTypeIconService.ImageList.Images[menuTemplateNode.ImageIndex] : menuTemplateNode.Image;
    }
    finally
    {
      menuTemplateNode.Text = text;
      menuTemplateNode.ImageIndex = imageIndex;
      menuTemplateNode.Image = image;
    }
    menuButtonItem.CommandName = menuTemplateNode.Name;
    menuButtonItem.PrimaryShortcut = menuTemplateNode.Shortcut;
    menuButtonItem.ShortcutActive = false;
    menuButtonItem.Click += new EventHandler(MenuBarItemConverter.ClickHandler);
    menuButtonItem.Tag = (object) commandHelper;
    menuButtonItem.AutoToggle = AutoToggleType.None;
    if (commandHelper != null)
    {
      switch (commandHelper.CommandLink.CommandInfo.State.State)
      {
        case ContextMenuCheckState.Unchecked:
          menuButtonItem.AutoToggle = AutoToggleType.Single;
          menuButtonItem.Checked = false;
          break;
        case ContextMenuCheckState.Checked:
          menuButtonItem.AutoToggle = AutoToggleType.Single;
          menuButtonItem.Checked = true;
          break;
      }
    }
    return menuButtonItem;
  }
}
