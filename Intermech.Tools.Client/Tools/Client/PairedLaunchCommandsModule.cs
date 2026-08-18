// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.PairedLaunchCommandsModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Bars;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Tools.Integrators;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client;

internal sealed class PairedLaunchCommandsModule : InitializerModule
{
  private static readonly object DynamicAppCommandTag = new object();
  private readonly BarManager barManager;
  private readonly MenuBarItem appMenu;

  public PairedLaunchCommandsModule(BarManager barManager)
  {
    this.barManager = barManager;
    this.appMenu = this.barManager.MenuBar.FindMenuBar("Applications");
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    if (this.appMenu == null)
      return;
    this.barManager.MenuBar.EnterMenuLoop += new EventHandler(this.OnEnterMenuLoop);
    this.barManager.MenuBar.ExitMenuLoop += new EventHandler(this.OnExitMenuLoop);
  }

  protected override void DoShutdown()
  {
    if (this.appMenu != null)
    {
      this.barManager.MenuBar.EnterMenuLoop -= new EventHandler(this.OnEnterMenuLoop);
      this.barManager.MenuBar.ExitMenuLoop -= new EventHandler(this.OnExitMenuLoop);
    }
    base.DoShutdown();
  }

  private void OnEnterMenuLoop(object sender, EventArgs e)
  {
    this.barManager.MenuBar.SuspendLayout();
    try
    {
      this.InsertDynamicAppCommands();
    }
    finally
    {
      this.barManager.MenuBar.ResumeLayout();
    }
  }

  private void OnExitMenuLoop(object sender, EventArgs e)
  {
    this.barManager.MenuBar.SuspendLayout();
    try
    {
      this.RemoveDynamicAppCommands();
    }
    finally
    {
      this.barManager.MenuBar.ResumeLayout();
    }
  }

  private void InsertDynamicAppCommands()
  {
    List<ToolbarItemBase> toolbarItemBaseList = new List<ToolbarItemBase>();
    foreach (object integrator in ClientContext.Integrators.GetIntegrators())
    {
      IApplicationLauncherService service = ServiceUtils.GetService<IApplicationLauncherService>(integrator, false);
      if (service != null)
      {
        foreach (MenuCommand command in service.GetCommands())
        {
          MenuCommand cmd = command;
          MenuButtonItem menuButtonItem = new MenuButtonItem();
          menuButtonItem.Text = cmd.Text;
          menuButtonItem.ToolTipText = cmd.Tooltip;
          menuButtonItem.Image = cmd.Image;
          menuButtonItem.Click += (EventHandler) ((cmdSender, cmdArgs) => cmd.CommandHandler());
          menuButtonItem.Tag = PairedLaunchCommandsModule.DynamicAppCommandTag;
          toolbarItemBaseList.Add((ToolbarItemBase) menuButtonItem);
        }
      }
    }
    if (toolbarItemBaseList.Count <= 0)
      return;
    toolbarItemBaseList.Sort((Comparison<ToolbarItemBase>) ((x, y) => StringComparer.CurrentCultureIgnoreCase.Compare(x.Text, y.Text)));
    toolbarItemBaseList[0].BeginGroup = true;
    this.appMenu.Items.AddRange(toolbarItemBaseList.ToArray());
  }

  private void RemoveDynamicAppCommands()
  {
    List<MenuButtonItem> menuButtonItemList = new List<MenuButtonItem>(this.appMenu.Items.Count);
    for (int index = this.appMenu.Items.Count - 1; index >= 0; --index)
    {
      MenuButtonItem menuButtonItem = this.appMenu.Items[index];
      if (menuButtonItem.Tag == PairedLaunchCommandsModule.DynamicAppCommandTag)
      {
        this.appMenu.Items.RemoveAt(index);
        menuButtonItemList.Add(menuButtonItem);
      }
    }
    for (int index = 0; index < menuButtonItemList.Count; ++index)
      menuButtonItemList[index].Dispose();
  }
}
