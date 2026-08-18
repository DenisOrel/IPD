
// Type: Intermech.Search.ButtonBars.MainMenuCommands.MainMenuCommandsClientModule
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Plugins;
using Intermech.Navigator;
using Intermech.Navigator.ContextMenu;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


namespace Intermech.Search.ButtonBars.MainMenuCommands;

public sealed class MainMenuCommandsClientModule
{
  public const string MainMenuCommandName = "MainMenu";
  private MenuTemplateNode _mainMenuMenuTemplateNode;
  private ICommandTarget _mainMenuCommandsCommandTarget;

  public void Load()
  {
    ServiceLocator.Get<IStartupService>().StartupComplete += new EventHandler(this.StartupService_StartupComplete);
  }

  public void Unload() => this.RemoveMainMenuTemplateNodeAndCommandsProvider();

  private void StartupService_StartupComplete(object sender, EventArgs e)
  {
    this.RemoveMainMenuTemplateNodeAndCommandsProvider();
    this.CreateMainMenuMenuTemplateNodeAndCommandsTarget();
    ServiceLocator.Get<IPluginManager>().PluginAdded += new PluginEventHandler(this.PluginManager_PluginAdded);
  }

  private void PluginManager_PluginAdded(object sender, PluginEventArgs e)
  {
    this.RemoveMainMenuTemplateNodeAndCommandsProvider();
    this.CreateMainMenuMenuTemplateNodeAndCommandsTarget();
  }

  private void CreateMainMenuMenuTemplateNodeAndCommandsTarget()
  {
    try
    {
      CommandsInfo commandsInfo = new CommandsInfo();
      this._mainMenuMenuTemplateNode = this.CreateMenuTemplateNodeForMainMenu(ref commandsInfo);
      MenuTemplate contextMenuTemplate = Holder.Factory.ContextMenuTemplate;
      contextMenuTemplate.BeginUpdate();
      try
      {
        contextMenuTemplate.Nodes.Add(this._mainMenuMenuTemplateNode);
      }
      finally
      {
        contextMenuTemplate.EndUpdate();
      }
      contextMenuTemplate.RebuildNameHash();
      this._mainMenuCommandsCommandTarget = (ICommandTarget) new MainMenuCommandsCommandTarget();
      ServiceLocator.Get<ICommandManager>().AddTarget(this._mainMenuCommandsCommandTarget);
    }
    catch (Exception ex)
    {
    }
  }

  private void RemoveMainMenuTemplateNodeAndCommandsProvider()
  {
    if (this._mainMenuCommandsCommandTarget == null)
      return;
    ServiceLocator.Get<ICommandManager>().RemoveTarget(this._mainMenuCommandsCommandTarget);
  }

  private MenuTemplateNode CreateMenuTemplateNodeForMainMenu(ref CommandsInfo commandsInfo)
  {
    MenuTemplateNode templateNodeForMainMenu = new MenuTemplateNode()
    {
      Name = "MainMenu",
      Text = "Главное меню"
    };
    if (ServiceLocator.Get<IMainMenuService>().MenuBar.Items.Cast<MenuItemBase>().FirstOrDefault<MenuItemBase>((Func<MenuItemBase, bool>) (o => o.CommandName == "Applications")) is MenuBarItem menuItem)
      templateNodeForMainMenu.Nodes.Add(this.CreateMenuTemplateNodeForMenuItem((MenuItemBase) menuItem, ref commandsInfo));
    return templateNodeForMainMenu;
  }

  private MenuTemplateNode CreateMenuTemplateNodeForMenuItem(
    MenuItemBase menuItem,
    ref CommandsInfo commandsInfo)
  {
    MenuTemplateNode templateNodeForMenuItem = new MenuTemplateNode();
    string commandNameForMenuItem = this.CreateCommandNameForMenuItem(menuItem);
    templateNodeForMenuItem.Name = commandNameForMenuItem;
    templateNodeForMenuItem.Text = this.RemoveAmpersand(menuItem.Text);
    INamedImageList namedImageList = ServiceLocator.Get<INamedImageList>();
    if (namedImageList.ImageIndex(commandNameForMenuItem) < 0)
    {
      Image forMenuButtonItem = this.GetImageForMenuButtonItem(menuItem);
      if (forMenuButtonItem != null)
        namedImageList.Add(forMenuButtonItem, commandNameForMenuItem);
    }
    templateNodeForMenuItem.ImageIndex = namedImageList.ImageIndex(commandNameForMenuItem);
    templateNodeForMenuItem.ImageListSource = ImageListSource.NamedImageList;
    if (menuItem.Items.Count == 0)
    {
      CommandInfo commandInfo = new CommandInfo(-1, (ClickEventHandler) ((items, viewServices, additinalInfo) => menuItem.PerformClick()));
      commandsInfo.Add(commandNameForMenuItem, commandInfo);
    }
    foreach (MenuItemBase menuItem1 in (CollectionBase) menuItem.Items)
      templateNodeForMenuItem.Nodes.Add(this.CreateMenuTemplateNodeForMenuItem(menuItem1, ref commandsInfo));
    return templateNodeForMenuItem;
  }

  private string CreateCommandNameForMenuItem(MenuItemBase menuButtonItem)
  {
    List<string> source = new List<string>();
    for (MenuItemBase menuItemBase = menuButtonItem; menuItemBase != null; menuItemBase = menuItemBase.Parent)
      source.Add(!string.IsNullOrEmpty(menuItemBase.CommandName) ? menuItemBase.CommandName : menuItemBase.Text);
    source.Add("MainMenu");
    return string.Join("/", source.Reverse<string>());
  }

  private Image GetImageForMenuButtonItem(MenuItemBase menuButtonItem)
  {
    try
    {
      if (menuButtonItem.Image != null)
        return menuButtonItem.Image;
      if (menuButtonItem.ImageList != null)
      {
        if (menuButtonItem.ImageIndex >= 0)
          return menuButtonItem.ImageList.Images[menuButtonItem.ImageIndex];
      }
    }
    catch (ObjectDisposedException ex)
    {
    }
    return (Image) null;
  }

  private string RemoveAmpersand(string text) => text.Replace("&", "");
}
