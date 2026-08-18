// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.AVSCommandsBuilder
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Bars;
using Intermech.Interfaces.AVS;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.PDMTree;

internal class AVSCommandsBuilder : IDisposable
{
  private List<ExternalAVSCommand> commands;
  private BarManager barManager;
  private MenuBarItem cachedMenu;
  private bool disposed;

  public AVSCommandsBuilder(BarManager barManager)
  {
    this.barManager = barManager;
    this.commands = new List<ExternalAVSCommand>();
  }

  public void Dispose()
  {
    if (this.disposed)
      return;
    this.DoDispose();
    this.disposed = true;
  }

  private void DoDispose()
  {
    if (this.cachedMenu == null)
      return;
    this.cachedMenu.Visible = false;
    foreach (ToolbarItemBase toolbarItemBase in (CollectionBase) this.cachedMenu.Items)
      toolbarItemBase.Visible = false;
  }

  private void RequireNotDisposed()
  {
    if (this.disposed)
      throw new ObjectDisposedException(this.GetType().FullName);
  }

  public void AddCommand(string commandName, string caption, string hint, EventHandler handler)
  {
    MenuBarItem menu = this.GetMenu();
    MenuItemBase menuItem = menu.FindItem(commandName);
    if (menuItem == null)
    {
      menuItem = (MenuItemBase) menu.Items[menu.Items.Add(caption)];
      menuItem.CommandName = commandName;
      menuItem.ToolTipText = hint;
    }
    if (!menu.Visible)
      menu.Visible = true;
    menuItem.Visible = true;
    menuItem.Enabled = false;
    this.commands.Add(new ExternalAVSCommand(menuItem, handler));
  }

  public ExternalAVSCommand[] Build() => this.commands.ToArray();

  private MenuBarItem GetMenu()
  {
    if (this.cachedMenu == null)
    {
      this.cachedMenu = this.barManager.MenuBar.FindMenuBar("CADLinkMenu");
      if (this.cachedMenu == null)
      {
        this.cachedMenu = this.barManager.MenuBar.AddMenuBar("Интеграция с CAD");
        this.cachedMenu.CommandName = "CADLinkMenu";
      }
    }
    return this.cachedMenu;
  }
}
