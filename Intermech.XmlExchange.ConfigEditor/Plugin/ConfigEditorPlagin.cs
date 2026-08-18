// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.Plugin.ConfigEditorPlagin
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Plugins;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.XmlExchange.ConfigEditor.Editor;
using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.Plugin;

public class ConfigEditorPlagin : IPackage
{
  private System.IServiceProvider _serviceProvider;

  public string Name => "Редактор конфигураций экспорта/импорта XML";

  public void Load(System.IServiceProvider serviceProvider)
  {
    this._serviceProvider = serviceProvider;
    if (!(this._serviceProvider.GetService(typeof (IPluginManager)) is IPluginManager service))
      return;
    if (service.IsLoadComplete)
      this.PluginManager_LoadComplete((object) null, (EventArgs) null);
    else
      service.LoadComplete += new EventHandler(this.PluginManager_LoadComplete);
  }

  public void Unload()
  {
    this.NavigatorContextMenu(false);
    this.MainMenu(false);
  }

  private void PluginManager_LoadComplete(object sender, EventArgs e)
  {
    this.AddIcons();
    this.NavigatorContextMenu(true);
    this.MainMenu(true);
  }

  internal void NavigatorContextMenu(bool addMenu)
  {
    IFactory type = ApplicationServices.Container.GetService(typeof (IFactory)).CastToType<IFactory>();
    if (addMenu)
      type.AddCommandsProvider(1, MetaDataHelper.GetObjectTypeID("cadd9457-306c-11d8-b4e9-00304f19f545"), (ICommandsProvider) new ConfigEditorCommandProvider());
    else
      type.RemoveCommandsProvider(1, MetaDataHelper.GetObjectTypeID("cadd9457-306c-11d8-b4e9-00304f19f545"), (ICommandsProvider) new ConfigEditorCommandProvider());
  }

  internal void MainMenu(bool addMenu)
  {
    MenuItemBase menuBar = (MenuItemBase) this._serviceProvider.GetService(typeof (BarManager)).CastToType<BarManager>().MenuBar.FindMenuBar("ExportImport");
    if (addMenu)
    {
      MenuButtonItem menuButtonItem = new MenuButtonItem("Редактор конфигураций экспорта/импорта XML", new EventHandler(this.EditorConfigExportClick));
      menuButtonItem.Icon = ImageResources.configEditor;
      menuBar.Items.Add((ToolbarItemBase) menuButtonItem);
    }
    else
    {
      MenuItemBase menuItemBase = menuBar.FindItem("Редактор конфигураций экспорта/импорта XML");
      if (menuItemBase == null)
        return;
      menuBar.Items.Remove((ToolbarItemBase) menuItemBase);
    }
  }

  private void EditorConfigExportClick(object sender, EventArgs e)
  {
    ConfigEditorWindow.ShowEditorWindow();
  }

  private void AddIcons()
  {
    if (!(this._serviceProvider.GetService(typeof (ICategoryTypeIconService)) is ICategoryTypeIconService service))
      return;
    ImageList imageList = service.ImageList;
    if (imageList == null)
      return;
    this.AddIcon(imageList, ImageResources.exportApplSettings, "exportApplSettings");
    this.AddIcon(imageList, ImageResources.importImbaseSettings, "importImbaseSettings");
    this.AddIcon(imageList, ImageResources.importMatchingTypes, "importMatchingTypes");
  }

  private void AddIcon(ImageList imageList, Icon icon, string iconKey)
  {
    if (imageList.Images.ContainsKey(iconKey))
      return;
    using (Icon icon1 = ImagesResizeHelper.ResizeIconTo32x16(icon, imageList.TransparentColor))
    {
      imageList.Images.Add(iconKey, icon1);
      icon1.Dispose();
    }
  }
}
