// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.CheckUncheckContextMenu
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Bars;
using Intermech.Interfaces.Workflow;
using System;
using System.Collections;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class CheckUncheckContextMenu : ContextMenu
{
  private Intermech.Bars.ToolBar _toolBar;
  private EventHandler _refreshHandler;

  public CheckUncheckContextMenu(Intermech.Bars.ToolBar toolBar, EventHandler refreshHandler)
  {
    this._toolBar = toolBar;
    this._refreshHandler = refreshHandler;
    MenuItem menuItem1 = new MenuItem(LocalizationHolder.GetString("CheckAll"), new EventHandler(this.CheckUncheckAll));
    menuItem1.Tag = (object) true;
    this.MenuItems.Add(menuItem1);
    MenuItem menuItem2 = new MenuItem(LocalizationHolder.GetString("UncheckAll"), new EventHandler(this.CheckUncheckAll));
    menuItem2.Tag = (object) false;
    this.MenuItems.Add(menuItem2);
    toolBar.ContextMenu = (ContextMenu) this;
  }

  public static void Attach(Intermech.Bars.ToolBar toolBar, EventHandler refreshHandler)
  {
    CheckUncheckContextMenu uncheckContextMenu = new CheckUncheckContextMenu(toolBar, refreshHandler);
  }

  private void CheckUncheckAll(object sender, EventArgs e)
  {
    foreach (ToolbarItemBase toolbarItemBase in (CollectionBase) this._toolBar.Items)
    {
      ButtonItem buttonItem = toolbarItemBase as ButtonItem;
      MenuItem menuItem = sender as MenuItem;
      if (buttonItem != null && menuItem != null && buttonItem.AutoToggle == AutoToggleType.Single)
        buttonItem.Checked = (bool) menuItem.Tag;
    }
    if (this._refreshHandler == null)
      return;
    this._refreshHandler((object) null, (EventArgs) null);
  }
}
