// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Startup
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Bars;
using Intermech.Client.Core.FormDesigner;
using Intermech.Client.Core.FormDesigner.Navigator;
using Intermech.Commands;
using Intermech.FormDesigner.Links;
using Intermech.FormDesigner.Navigator;
using Intermech.FormDesigner.Wrappers;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Plugins;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner;

/// <summary>Модуль расширения "Редактор форм".</summary>
public class Startup : IPackage, IConfigurable
{
  /// <summary>Выгрузка плагина.</summary>
  public void Unload()
  {
  }

  /// <summary>Заголовок плагина.</summary>
  public string Name => LocalizationHolder.rm.GetString("FormDesigner_114");

  /// <summary>Загрузка плагина.</summary>
  /// <param name="serviceProvider">Провайдер сервисов</param>
  public void Load(System.IServiceProvider serviceProvider)
  {
    ProviderHolder.ServiceProvider = serviceProvider;
    FormDesignerFormLinksManager.Register();
    ProviderHolder.EditorService = (IFormDesignerEditorService) new FormDesignerEditorService();
    ServicesManager.AddService(typeof (IFormDesignerEditorService), (object) ProviderHolder.EditorService);
    (serviceProvider.GetService(typeof (IPluginManager)) as IPluginManager).LoadComplete += new EventHandler(this.OnPlugins_LoadComplete);
    ObjectCommandEvents.SaveChanges.Before += new EventHandler<BeforeObjectCommandArgs>(this.OnCommands_Before);
    FormDesignerPropertiesPage designerPropertiesPage = new FormDesignerPropertiesPage((System.IServiceProvider) ServicesManager.ServiceContainer);
  }

  /// <summary>Загрузка конфигурации.</summary>
  /// <param name="configurationManager">Менеджер конфигурации</param>
  public void LoadConfiguration(IConfigurationManager configurationManager)
  {
    IConfiguration configuration = configurationManager.Open("FormDesigner.Editor.Docking");
    if (configuration == null || !configuration.HasProperty("Docking"))
      return;
    ProviderHolder.DockString = configuration.GetProperty("Docking");
  }

  /// <summary>Сохранение конфигурации.</summary>
  /// <param name="configurationManager">Менеджер конфигурации</param>
  public void SaveConfiguration(IConfigurationManager configurationManager)
  {
    (configurationManager.Open("FormDesigner.Editor.Docking") ?? configurationManager.Create("FormDesigner.Editor.Docking")).SetProperty("Docking", ProviderHolder.DockString);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnPlugins_LoadComplete(object sender, EventArgs e)
  {
    ProviderHolder.BarManager = ProviderHolder.ServiceProvider.GetService(typeof (BarManager)) as BarManager;
    ProviderHolder.Factory = ProviderHolder.ServiceProvider.GetService(typeof (IFactory)) as IFactory;
    if (ProviderHolder.Factory == null || ProviderHolder.BarManager == null)
      return;
    ProviderHolder.iList.TransparentColor = Color.Lime;
    Bitmap bitmap = (Bitmap) null;
    ProviderHolder.iList.Images.Add((Image) (bitmap = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.Align.Bottoms.bmp"))));
    ProviderHolder.iList.Images.Add((Image) (bitmap = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.Align.Center.bmp"))));
    ProviderHolder.iList.Images.Add((Image) (bitmap = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.Align.Lefts.bmp"))));
    ProviderHolder.iList.Images.Add((Image) (bitmap = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.Align.Middles.bmp"))));
    ProviderHolder.iList.Images.Add((Image) (bitmap = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.Align.Rights.bmp"))));
    ProviderHolder.iList.Images.Add((Image) (bitmap = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.Align.ToGrid.bmp"))));
    ProviderHolder.iList.Images.Add((Image) (bitmap = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.Align.Tops.bmp"))));
    ProviderHolder.iList.Images.Add((Image) (bitmap = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.CenterInForm.Horizontally.bmp"))));
    ProviderHolder.iList.Images.Add((Image) (bitmap = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.CenterInForm.Vertically.bmp"))));
    ProviderHolder.iList.Images.Add((Image) (bitmap = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.HorizontalSpacing.Decrease.bmp"))));
    ProviderHolder.iList.Images.Add((Image) (bitmap = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.HorizontalSpacing.Increase.bmp"))));
    ProviderHolder.iList.Images.Add((Image) (bitmap = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.HorizontalSpacing.MakeEqual.bmp"))));
    ProviderHolder.iList.Images.Add((Image) (bitmap = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.HorizontalSpacing.Remove.bmp"))));
    ProviderHolder.iList.Images.Add((Image) (bitmap = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.MakeSameSize.Both.bmp"))));
    ProviderHolder.iList.Images.Add((Image) (bitmap = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.MakeSameSize.Height.bmp"))));
    ProviderHolder.iList.Images.Add((Image) (bitmap = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.MakeSameSize.SizeToGrid.bmp"))));
    ProviderHolder.iList.Images.Add((Image) (bitmap = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.MakeSameSize.Width.bmp"))));
    ProviderHolder.iList.Images.Add((Image) (bitmap = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.Order.BringToFront.bmp"))));
    ProviderHolder.iList.Images.Add((Image) (bitmap = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.Order.SendToBack.bmp"))));
    ProviderHolder.iList.Images.Add((Image) (bitmap = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.Order.TabOrder.bmp"))));
    ProviderHolder.iList.Images.Add((Image) (bitmap = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.VerticalSpacing.Decrease.bmp"))));
    ProviderHolder.iList.Images.Add((Image) (bitmap = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.VerticalSpacing.Increase.bmp"))));
    ProviderHolder.iList.Images.Add((Image) (bitmap = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.VerticalSpacing.MakeEqual.bmp"))));
    ProviderHolder.iList.Images.Add((Image) (bitmap = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.FormDesigner.Resources.VerticalSpacing.Remove.bmp"))));
    int count = ProviderHolder.BarManager.MenuBar.ImageList.Images.Count;
    for (int index = 0; index < ProviderHolder.iList.Images.Count; ++index)
    {
      ProviderHolder.BarManager.MenuBar.ImageList.Images.AddStrip(ProviderHolder.iList.Images[index]);
      ProviderHolder.MenuIndex.Add((object) index, (object) (count + index));
    }
    IDBObjectTypeInfo objectType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectType(GuidHolder.FormsTypeGuid, true);
    ProviderHolder.Factory.AddViewsProvider(1, objectType.ObjectType, (IViewsProvider) new FormDesignerEditorViewProvider());
    ProviderHolder.Factory.AddCommandsProvider(1, objectType.ObjectType, (ICommandsProvider) new FormDesignerCommands());
    ServicesManager.AddService(typeof (IFormDesignerStateHolder), (object) new FormDesignerStateHolder());
    new SplitterWrapper().Prepare();
    ClientFormsCache.Initialize();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnCommands_Before(object sender, BeforeObjectCommandArgs e)
  {
    if (!(ProviderHolder.EditorService is FormDesignerEditorService editorService))
      return;
    Control editorControl = editorService.GetEditorControl((sender as ObjectCommand).ObjectId);
    if (editorControl == null)
      return;
    (editorControl as FormDesignerControl).BeforeCheckIn();
  }
}
