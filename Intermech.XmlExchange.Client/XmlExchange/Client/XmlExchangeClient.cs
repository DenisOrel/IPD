// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.Client.XmlExchangeClient
// Assembly: Intermech.XmlExchange.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 60313882-D426-47E0-8CD2-E15037D75FF2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.XmlExchange.Client.xml

using Intermech.Bars;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.XmlExchange;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Protection;
using Intermech.Search;
using Intermech.XmlExchange.Client.Navigator;
using Intermech.XmlExchange.Client.Navigator.Commands;
using System;
using System.Drawing;
using System.IO;

#nullable disable
namespace Intermech.XmlExchange.Client;

/// <summary>Класс идентификации плагина</summary>
public class XmlExchangeClient : IPackage
{
  /// <summary>Имя плагина</summary>
  /// <remarks>Что бы каждый раз не дергать локализатор сохраню здесь значение</remarks>
  private string _name = string.Empty;
  /// <summary>Менеджер плагинов</summary>
  private IPluginManager _manager;
  /// <summary>Является ли текущий пользователь администратором</summary>
  private static bool _isUserAdmin;
  /// <summary>
  /// Если данное свойство равно true, все механизмы плагина должны быть заблокированы
  /// </summary>
  private static bool _pluginLocked;

  /// <summary>Инициализация данных класса</summary>
  private void InitData() => this._name = LocalizationHolder.rm.GetString("XmlExchange.Client_1");

  /// <summary>Конструктор</summary>
  public XmlExchangeClient() => this.InitData();

  /// <summary>Заголовок плагина</summary>
  public string Name => this._name;

  /// <summary>Загрузка плагина</summary>
  /// <param name="serviceProvider">Провайдер сервисов</param>
  public void Load(IServiceProvider serviceProvider)
  {
    if (!(serviceProvider.GetService(typeof (ILicenser)) is ILicenser service1))
      throw new ProtectionException(LocalizationHolder.rm.GetString("XmlExchange.Client_34"));
    service1.AllocateLicense(XmlExchangeProtectionKey.appId);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      XmlExchangeClient._isUserAdmin = sessionKeeper.Session.IsAdmin;
      IXmlServerPlugin xmlServerPlugin;
      try
      {
        xmlServerPlugin = sessionKeeper.Session.GetCustomService(typeof (IXmlServerPlugin)) as IXmlServerPlugin;
      }
      catch
      {
        xmlServerPlugin = (IXmlServerPlugin) null;
      }
      XmlExchangeClient._pluginLocked = xmlServerPlugin == null;
      if (XmlExchangeClient._pluginLocked)
        return;
      this._manager = serviceProvider.GetService(typeof (IPluginManager)) as IPluginManager;
      if (this._manager != null)
        this._manager.LoadComplete += new EventHandler(this._manager_LoadComplete);
      XmlExchangeClientCache.Services.Factory = serviceProvider.GetService(typeof (IFactory)) as IFactory;
      XmlExchangeClientCache.Services.BackgroundTaskView = serviceProvider.GetService(typeof (IBackgroundTaskView)) as IBackgroundTaskView;
      XmlExchangeClientCache.Services.NamedImageList = ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
      this.LoadPluginResources(serviceProvider);
      ICommandsProvider provider1 = (ICommandsProvider) new XmlExchangeCommandProvider(XmlExchangeClientCache.Services.Factory);
      IViewsProvider provider2 = (IViewsProvider) new XmlViewsProvider();
      if (XmlExchangeClientCache.Services.Factory != null)
      {
        XmlExchangeClientCache.Services.Factory.AddCommandsProvider(1, provider1);
        XmlExchangeClientCache.Services.Factory.AddViewsProvider(1, provider2);
      }
      IMainMenuService service2 = ServiceUtils.GetService<IMainMenuService>((object) ApplicationServices.Container, false);
      if (service2 == null)
        return;
      this.RegisterExportCommands(service2);
      this.RegisterImportCommands(service2);
    }
  }

  /// <summary>Выгрузка плагинов</summary>
  public void Unload()
  {
    ServiceUtils.GetService<ILicenser>((object) ApplicationServices.Container, false)?.ReleaseLicense(XmlExchangeProtectionKey.appId);
    if (this._manager == null)
      return;
    this._manager.LoadComplete -= new EventHandler(this._manager_LoadComplete);
    this._manager = (IPluginManager) null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _manager_LoadComplete(object sender, EventArgs e)
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = XmlExchangeProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = XmlExchangeProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(XmlExchangeProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("XmlExchange.Client_2"), (object) LocalizationHolder.rm.GetString("XmlExchange.Client_1"), (object) num));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="mainMenuService"></param>
  private void RegisterExportCommands(IMainMenuService mainMenuService)
  {
    INamedImageList service = ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
    MenuButtonItem menuButtonItem1 = new MenuButtonItem(LocalizationHolder.rm.GetString("XmlExchange.Client_8"));
    menuButtonItem1.ImageIndex = service != null ? service.ImageIndex("XML.imgBriefcaseExport") : -1;
    menuButtonItem1.CommandName = "ExportData";
    MenuButtonItem menuButtonItem2 = menuButtonItem1;
    menuButtonItem2.Click += new EventHandler(this.menuExport_Click);
    mainMenuService.RegisterMenuItems(MainMenuItemSite.ExportImport, MainMenuItemPosition.Third, menuButtonItem2);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="mainMenuService"></param>
  private void RegisterImportCommands(IMainMenuService mainMenuService)
  {
    INamedImageList service = ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
    MenuButtonItem menuButtonItem1 = new MenuButtonItem(LocalizationHolder.rm.GetString("XmlExchange.Client_15"));
    menuButtonItem1.ImageIndex = service != null ? service.ImageIndex("XML.imgBriefcaseImport") : -1;
    menuButtonItem1.CommandName = "ImportData";
    MenuButtonItem menuButtonItem2 = menuButtonItem1;
    menuButtonItem2.Click += new EventHandler(this.menuImport_Click);
    mainMenuService.RegisterMenuItems(MainMenuItemSite.ExportImport, MainMenuItemPosition.Third, menuButtonItem2);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void menuExport_Click(object sender, EventArgs e)
  {
    XmlExportCommand.Execute(SelectedItemsHelper.GetNavigatorSelection(), (IServiceProvider) ApplicationServices.Container, (object) null);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void menuImport_Click(object sender, EventArgs e)
  {
    XmlImportCommand.Execute(SelectedItemsHelper.GetNavigatorSelection(), (IServiceProvider) ApplicationServices.Container, (object) null);
  }

  /// <summary>Загрузить ресурсы плагина</summary>
  /// <param name="serviceProvider">Коллекция сервисов</param>
  private void LoadPluginResources(IServiceProvider serviceProvider)
  {
    if (XmlExchangeClient._pluginLocked || XmlExchangeClientCache.Services.NamedImageList == null)
      return;
    Stream manifestResourceStream = this.GetType().Assembly.GetManifestResourceStream("Intermech.XmlExchange.Client.Resources.XmlExchangeBitmaps.bmp");
    if (manifestResourceStream == null)
      return;
    using (Bitmap images = new Bitmap(manifestResourceStream))
    {
      images.MakeTransparent();
      XmlExchangeClientCache.Services.NamedImageList.AddStrip((Image) images, new string[4]
      {
        "XML.imgBriefcase",
        "XML.imgBriefcaseDocument",
        "XML.imgBriefcaseExport",
        "XML.imgBriefcaseImport"
      });
    }
    manifestResourceStream.Close();
  }
}
