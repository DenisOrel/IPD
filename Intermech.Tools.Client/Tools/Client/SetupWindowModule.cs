// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.SetupWindowModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Bars;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.NavBars;
using Intermech.Search;
using Intermech.Tools.Setup;
using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Client;

internal sealed class SetupWindowModule : InitializerModule
{
  private const string AppPaneName = "adminPane";
  private const string MenuName = "mnService";
  private IAppItem appPaneButton;
  private MenuButtonItem mainMenuButton;

  protected override void DoInitialize()
  {
    base.DoInitialize();
    INamedImageList service1 = ServiceUtils.GetService<INamedImageList>((object) ServicesManager.ServiceContainer, false);
    int imageIndex = service1 == null ? -1 : service1.ImageIndex("imgAppPane2");
    IAppPane appPane = this.FindAppPane("adminPane");
    if (appPane != null)
      this.appPaneButton = appPane.Add(LocalizationHolder.rm.GetString("Tools.Client_88"), new EventHandler(this.ShowToolSetupWindow), imageIndex);
    if (!(ServicesManager.GetService(typeof (IMainMenuService)) is IMainMenuService service2))
      return;
    this.mainMenuButton = new MenuButtonItem(LocalizationHolder.rm.GetString("Tools.Client_88"), new EventHandler(this.ShowToolSetupWindow), imageIndex);
    MenuButtonItem[] menuButtonItemArray = new MenuButtonItem[1]
    {
      this.mainMenuButton
    };
    service2.RegisterMenuItems(MainMenuItemSite.TuningTop, MainMenuItemPosition.Default, menuButtonItemArray);
  }

  protected override void DoShutdown()
  {
    base.DoShutdown();
    IAppItem appPaneButton = this.appPaneButton;
    if (this.mainMenuButton == null)
      return;
    this.mainMenuButton.Dispose();
    this.mainMenuButton = (MenuButtonItem) null;
  }

  private IAppPane FindAppPane(string name)
  {
    INavigationBar service = ServiceUtils.GetService<INavigationBar>((object) ServicesManager.ServiceContainer, false);
    return service == null ? (IAppPane) null : service.FindPane(name) as IAppPane;
  }

  private MenuBarItem FindMenu(string menuName)
  {
    return ServiceUtils.GetService<BarManager>((object) ServicesManager.ServiceContainer, false)?.MenuBar.FindMenuBar(menuName);
  }

  private void ShowToolSetupWindow(object sender, EventArgs e)
  {
    Size size = Screen.PrimaryScreen.WorkingArea.Size;
    size.Width = (int) Math.Round((double) size.Width * 0.75);
    size.Height = (int) Math.Round((double) size.Height * 0.75);
    ToolSetupWindow toolSetupWindow = new ToolSetupWindow();
    toolSetupWindow.Size = size;
    int num = (int) toolSetupWindow.ShowDialog();
  }
}
