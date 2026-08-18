
// Type: Intermech.Search.MainMenuService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search;

public sealed class MainMenuService : IMainMenuService
{
  private MenuBar _menuBar;
  private List<MainMenuService.MenuItemsGroup> _menuItemsGroups = new List<MainMenuService.MenuItemsGroup>();
  private bool _suppressRebuildMainMenu;

  public MainMenuService(MenuBar menuBar)
  {
    this._menuBar = menuBar != null ? menuBar : throw new ArgumentNullException(nameof (menuBar));
  }

  public MenuBar MenuBar => this._menuBar;

  public event EventHandler AfterMainMenuChanged;

  public void RegisterMenuItems(
    MainMenuItemSite mainMenuItemSite,
    MainMenuItemPosition mainMenuItemPosition,
    params MenuButtonItem[] menuItems)
  {
    if (menuItems == null || menuItems.Length == 0)
      throw new ArgumentException();
    lock (this._menuItemsGroups)
    {
      foreach (MenuButtonItem menuItem in menuItems)
        this.RegisterMenuItemsGroupInternal(mainMenuItemSite, mainMenuItemPosition, true, menuItem);
    }
  }

  public void RegisterMenuItemsGroup(
    MainMenuItemSite mainMenuItemSite,
    MainMenuItemPosition mainMenuItemPosition,
    bool disableAutoBeginGroup,
    params MenuButtonItem[] menuItems)
  {
    if (menuItems == null || menuItems.Length == 0)
      throw new ArgumentException();
    lock (this._menuItemsGroups)
      this.RegisterMenuItemsGroupInternal(mainMenuItemSite, mainMenuItemPosition, disableAutoBeginGroup, menuItems);
  }

  public void UnregiterMenuItems(params MenuButtonItem[] menuItems)
  {
    if (menuItems == null || menuItems.Length == 0)
      throw new ArgumentException();
    lock (this._menuItemsGroups)
    {
      foreach (MainMenuService.MenuItemsGroup menuItemsGroup in this._menuItemsGroups.ToArray())
      {
        foreach (MenuButtonItem menuItem in menuItems)
        {
          if (menuItemsGroup.Items.Contains(menuItem))
            menuItemsGroup.Items.Remove(menuItem);
        }
        if (menuItemsGroup.Items.Count == 0)
          this._menuItemsGroups.Remove(menuItemsGroup);
      }
      foreach (MenuButtonItem menuItem in menuItems)
      {
        if (menuItem.Parent != null)
          menuItem.Parent.Items.Remove((ToolbarItemBase) menuItem);
      }
      this.RebuildMenu();
    }
  }

  public void SuppressRebuildMainMenu() => this._suppressRebuildMainMenu = true;

  public void ResumeRebuildMainMenu()
  {
    this._suppressRebuildMainMenu = false;
    this.RebuildMenu();
  }

  private void RegisterMenuItemsGroupInternal(
    MainMenuItemSite mainMenuItemSite,
    MainMenuItemPosition mainMenuItemPosition,
    bool disableAutoBeginGroup,
    params MenuButtonItem[] menuItems)
  {
    this._menuItemsGroups.Add(new MainMenuService.MenuItemsGroup(mainMenuItemSite, mainMenuItemPosition, menuItems)
    {
      DisableAutoBeginGroup = disableAutoBeginGroup
    });
    this.RebuildMenu();
  }

  private void RebuildMenu()
  {
    if (this._suppressRebuildMainMenu)
      return;
    foreach (MainMenuService.MenuItemsGroup menuItemsGroup in this._menuItemsGroups)
    {
      foreach (MenuButtonItem menuButtonItem in menuItemsGroup.Items)
      {
        if (menuButtonItem.Parent != null)
          menuButtonItem.Parent.Items.Remove((ToolbarItemBase) menuButtonItem);
      }
    }
    this.OrderAndAddToMenuAsGroup(this._menuItemsGroups.Where<MainMenuService.MenuItemsGroup>((Func<MainMenuService.MenuItemsGroup, bool>) (o => o.Site == MainMenuItemSite.TuningTop)));
    this.OrderAndAddToMenuAsGroup(this._menuItemsGroups.Where<MainMenuService.MenuItemsGroup>((Func<MainMenuService.MenuItemsGroup, bool>) (o => o.Site == MainMenuItemSite.TuningMiddle)));
    this.OrderAndAddToMenuAsGroup(this._menuItemsGroups.Where<MainMenuService.MenuItemsGroup>((Func<MainMenuService.MenuItemsGroup, bool>) (o => o.Site == MainMenuItemSite.TuningBottom)));
    this.OrderAndAddToMenuAsGroup(this._menuItemsGroups.Where<MainMenuService.MenuItemsGroup>((Func<MainMenuService.MenuItemsGroup, bool>) (o => o.Site == MainMenuItemSite.ViewTop)));
    this.OrderAndAddToMenuAsGroup(this._menuItemsGroups.Where<MainMenuService.MenuItemsGroup>((Func<MainMenuService.MenuItemsGroup, bool>) (o => o.Site == MainMenuItemSite.ViewMiddle)));
    this.OrderAndAddToMenuAsGroup(this._menuItemsGroups.Where<MainMenuService.MenuItemsGroup>((Func<MainMenuService.MenuItemsGroup, bool>) (o => o.Site == MainMenuItemSite.ViewBottom)));
    foreach (IEnumerable<MainMenuService.MenuItemsGroup> menuItemsGroups in this._menuItemsGroups.Where<MainMenuService.MenuItemsGroup>((Func<MainMenuService.MenuItemsGroup, bool>) (o => o.Site != MainMenuItemSite.TuningBottom && o.Site != MainMenuItemSite.TuningMiddle && o.Site != MainMenuItemSite.TuningTop && o.Site != MainMenuItemSite.ViewBottom && o.Site != MainMenuItemSite.ViewMiddle && o.Site != MainMenuItemSite.ViewTop)).GroupBy<MainMenuService.MenuItemsGroup, MainMenuItemSite>((Func<MainMenuService.MenuItemsGroup, MainMenuItemSite>) (o => o.Site)))
      this.OrderAndAddToMenu(menuItemsGroups);
    EventHandler afterMainMenuChanged = this.AfterMainMenuChanged;
    if (afterMainMenuChanged == null)
      return;
    afterMainMenuChanged((object) this, EventArgs.Empty);
  }

  private void OrderAndAddToMenuAsGroup(
    IEnumerable<MainMenuService.MenuItemsGroup> menuItemsGroups)
  {
    this.AddToMenu(this.OrderMenuItemsGroups(menuItemsGroups), true);
  }

  private void OrderAndAddToMenu(
    IEnumerable<MainMenuService.MenuItemsGroup> menuItemsGroups)
  {
    this.AddToMenu(this.OrderMenuItemsGroups(menuItemsGroups));
  }

  private List<MainMenuService.MenuItemsGroup> OrderMenuItemsGroups(
    IEnumerable<MainMenuService.MenuItemsGroup> menuItemsGroups)
  {
    List<MainMenuService.MenuItemsGroup> menuItemsGroupList = new List<MainMenuService.MenuItemsGroup>();
    menuItemsGroupList.AddRange(this.OrderMenuItemsGroupsByFirstItemText(menuItemsGroups.Where<MainMenuService.MenuItemsGroup>((Func<MainMenuService.MenuItemsGroup, bool>) (o => o.Position == MainMenuItemPosition.First))));
    menuItemsGroupList.AddRange(this.OrderMenuItemsGroupsByFirstItemText(menuItemsGroups.Where<MainMenuService.MenuItemsGroup>((Func<MainMenuService.MenuItemsGroup, bool>) (o => o.Position == MainMenuItemPosition.Second))));
    menuItemsGroupList.AddRange(this.OrderMenuItemsGroupsByFirstItemText(menuItemsGroups.Where<MainMenuService.MenuItemsGroup>((Func<MainMenuService.MenuItemsGroup, bool>) (o => o.Position == MainMenuItemPosition.Third))));
    menuItemsGroupList.AddRange(this.OrderMenuItemsGroupsByFirstItemText(menuItemsGroups.Where<MainMenuService.MenuItemsGroup>((Func<MainMenuService.MenuItemsGroup, bool>) (o => o.Position == MainMenuItemPosition.Default))));
    menuItemsGroupList.AddRange(this.OrderMenuItemsGroupsByFirstItemText(menuItemsGroups.Where<MainMenuService.MenuItemsGroup>((Func<MainMenuService.MenuItemsGroup, bool>) (o => o.Position == MainMenuItemPosition.Penultimate))));
    menuItemsGroupList.AddRange(this.OrderMenuItemsGroupsByFirstItemText(menuItemsGroups.Where<MainMenuService.MenuItemsGroup>((Func<MainMenuService.MenuItemsGroup, bool>) (o => o.Position == MainMenuItemPosition.Last))));
    return menuItemsGroupList;
  }

  private IEnumerable<MainMenuService.MenuItemsGroup> OrderMenuItemsGroupsByFirstItemText(
    IEnumerable<MainMenuService.MenuItemsGroup> menuItemsGroups)
  {
    return (IEnumerable<MainMenuService.MenuItemsGroup>) menuItemsGroups.OrderBy<MainMenuService.MenuItemsGroup, string>((Func<MainMenuService.MenuItemsGroup, string>) (o => o.Items.Count <= 0 ? string.Empty : o.Items[0].Text));
  }

  private void AddToMenu(
    List<MainMenuService.MenuItemsGroup> menuItemsGroups,
    bool beginGroup = false)
  {
    MainMenuService.MenuItemsGroup menuItemsGroup1 = menuItemsGroups.FirstOrDefault<MainMenuService.MenuItemsGroup>();
    MenuButtonItem menuButtonItem1 = menuItemsGroup1 != null ? menuItemsGroup1.Items.FirstOrDefault<MenuButtonItem>() : (MenuButtonItem) null;
    if (beginGroup && menuButtonItem1 != null)
      menuButtonItem1.BeginGroup = true;
    foreach (MainMenuService.MenuItemsGroup menuItemsGroup2 in menuItemsGroups)
    {
      if (menuItemsGroup2.Items.Count > 0)
      {
        string pathForSite = this.GetPathForSite(menuItemsGroup2.Site);
        MenuItemBase menuItemBase = !pathForSite.Contains(".") ? (MenuItemBase) this._menuBar.FindMenuBar(pathForSite) : this._menuBar.FindMenuItem(pathForSite);
        MenuButtonItem menuButtonItem2 = menuItemsGroup2.Items.First<MenuButtonItem>();
        if (!beginGroup || beginGroup && menuButtonItem2 != menuButtonItem1)
          menuButtonItem2.BeginGroup = !menuItemsGroup2.DisableAutoBeginGroup;
        menuItemBase.Items.AddRange((ToolbarItemBase[]) menuItemsGroup2.Items.ToArray());
      }
    }
  }

  private string GetPathForSite(MainMenuItemSite mainMenuSite)
  {
    switch (mainMenuSite)
    {
      case MainMenuItemSite.Applications:
        return "Applications";
      case MainMenuItemSite.Composition:
        return "Composition";
      case MainMenuItemSite.TuningTop:
      case MainMenuItemSite.TuningMiddle:
      case MainMenuItemSite.TuningBottom:
        return "mnService";
      case MainMenuItemSite.AdministratorUtilities:
        return "mnService.AdminUtils";
      case MainMenuItemSite.ExportImport:
        return "ExportImport";
      case MainMenuItemSite.ViewTop:
      case MainMenuItemSite.ViewMiddle:
      case MainMenuItemSite.ViewBottom:
        return "View";
      default:
        throw new NotSupportedException();
    }
  }

  private sealed class MenuItemsGroup
  {
    public MenuItemsGroup(
      MainMenuItemSite mainMenuItemsSite,
      MainMenuItemPosition menuItemsPosition,
      MenuButtonItem[] menuItems)
    {
      if (menuItems == null || menuItems.Length == 0)
        throw new ArgumentException();
      this.Site = mainMenuItemsSite;
      this.Position = menuItemsPosition;
      this.Items = ((IEnumerable<MenuButtonItem>) menuItems).ToList<MenuButtonItem>();
    }

    public MainMenuItemSite Site { get; private set; }

    public MainMenuItemPosition Position { get; private set; }

    public List<MenuButtonItem> Items { get; private set; }

    public bool DisableAutoBeginGroup { get; set; }
  }
}
