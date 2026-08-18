// Decompiled with JetBrains decompiler
// Type: Intermech.Update.UpdatePlugin
// Assembly: Intermech.Update, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 825FBF29-0112-4B23-8140-950E091D8F10
// Assembly location: D:\IPS\Client\Intermech.Update.dll

using Intermech.Bars;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Plugins;
using Intermech.Search;
using System;

#nullable disable
namespace Intermech.Update;

public class UpdatePlugin : IPackage
{
  private ScriptsEditorControl _scriptsEditor;
  private IServiceProvider _serviceProvider;
  private const string _commandName = "AutoUpdateScripts";
  private const string _editorName = "Редактор скриптов автообновления";

  public void Load(IServiceProvider serviceProvider)
  {
    this._serviceProvider = serviceProvider;
    INamedImageList service1 = (INamedImageList) this._serviceProvider.GetService(typeof (INamedImageList));
    int num = service1 == null ? -1 : service1.ImageIndex("imgAutoupgradeScripts");
    if (!(this._serviceProvider.GetService(typeof (IMainMenuService)) is IMainMenuService service2))
      return;
    MenuButtonItem menuButtonItem = new MenuButtonItem("Редактор скриптов автообновления");
    menuButtonItem.CommandName = "AutoUpdateScripts";
    menuButtonItem.Click += new EventHandler(this.AutoUpdateScriptsMenuClick);
    menuButtonItem.ImageIndex = num;
    MenuButtonItem[] menuButtonItemArray = new MenuButtonItem[1]
    {
      menuButtonItem
    };
    service2.RegisterMenuItems(MainMenuItemSite.TuningTop, MainMenuItemPosition.Default, menuButtonItemArray);
  }

  public void Unload()
  {
    BarManager service = (BarManager) this._serviceProvider.GetService(typeof (BarManager));
    if (service == null)
      return;
    MenuBar menuBar1 = service.MenuBar;
    if (menuBar1.FindMenuItem("View.AutoUpdateScripts") != null)
      return;
    MenuBarItem menuBar2 = menuBar1.FindMenuBar("mnService");
    if (menuBar2 == null)
      return;
    MenuItemBase menuItemBase = menuBar2.FindItem("AutoUpdateScripts");
    menuBar2.Items.Remove((ToolbarItemBase) menuItemBase);
  }

  public string Name => "Intermech.Update";

  private void AutoUpdateScriptsMenuClick(object sender, EventArgs e)
  {
    DockManager service1 = (DockManager) this._serviceProvider.GetService(typeof (DockManager));
    this._scriptsEditor = new ScriptsEditorControl();
    this._scriptsEditor.Name = "Редактор скриптов автообновления";
    INamedImageList service2 = (INamedImageList) this._serviceProvider.GetService(typeof (INamedImageList));
    if (service1 == null)
      return;
    this._scriptsEditor.Show(service1);
    this._scriptsEditor.Activate();
  }
}
