// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.DatabaseConfiguratorStartup
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using ImSSP;
using Intermech.ApplicationModel;
using Intermech.Bars;
using Intermech.Client.Core.Tools.MetadataUpdates;
using Intermech.Controls;
using Intermech.DatabaseConfigurator.Dictionary;
using Intermech.DatabaseConfigurator.FileStorage;
using Intermech.DatabaseConfigurator.PropertyPages;
using Intermech.DatabaseConfigurator.Scripting.CSharp;
using Intermech.DatabaseConfigurator.Scripts;
using Intermech.DatabaseConfigurator.Security;
using Intermech.DatabaseConfigurator.TimeTable;
using Intermech.DatabaseConfigurator.Utils;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.Plugins;
using Intermech.Kernel.Search;
using Intermech.Ldap;
using Intermech.Localization;
using Intermech.NavBars;
using Intermech.Navigator;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.PropertyEditors;
using Intermech.Search;
using Intermech.Security;
using Intermech.Security.EventLog;
using Intermech.Statistics;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.DatabaseConfigurator;

public class DatabaseConfiguratorStartup(IOCBasedPackageParameters createParameters) : 
  IOCBasedPackage(createParameters, LocalizationHolder.rm.GetString("DatabaseConfigurator_159")),
  IConfigurable,
  IPackage
{
  private System.IServiceProvider _serviceProvider;
  private DockControl _dbCfgWindow;
  private DockControl _dbStatsWindow;
  private DockControl securityWindow;
  private Guid SecurityWindowGUID = new Guid("{CA7224C4-51F4-5fad-83B7-CCE417ECB318}");
  private FixInvalidBlobsControl fixControl;

  protected override void DoUnload()
  {
    if (this._serviceProvider.GetService(typeof (IDatabaseConfiguratorService)) is IDatabaseConfiguratorService service)
      service.UnregisterCategoryProps(DatabaseConfiguratorConsts.ObjectTypesCategoryID);
    base.DoUnload();
  }

  protected override void DoLoad()
  {
    base.DoLoad();
    this._serviceProvider = (System.IServiceProvider) ApplicationServices.Container;
    IWellKnownWindowsOpenService service1 = ServicesManager.GetService(typeof (IWellKnownWindowsOpenService)) as IWellKnownWindowsOpenService;
    int imageIndex1 = 0;
    int imageIndex2 = 0;
    int imageIndex3 = 0;
    using (Stream manifestResourceStream = this.GetType().Assembly.GetManifestResourceStream("Intermech.DatabaseConfigurator.Resources.dc.ico"))
    {
      using (Icon icon = new Icon(manifestResourceStream))
      {
        INamedImageList service2 = (INamedImageList) this._serviceProvider.GetService(typeof (INamedImageList));
        imageIndex1 = service2.Add(icon, "imgDatabaseConfigurator");
        imageIndex2 = service2.ImageIndex("imgKeys");
        imageIndex3 = service2.ImageIndex("imgPerformance");
      }
    }
    NormalizedIndexesPage normalizedIndexesPage = new NormalizedIndexesPage(this._serviceProvider);
    NormalizedIndexesReplacesPage indexesReplacesPage = new NormalizedIndexesReplacesPage(this._serviceProvider);
    if (this._serviceProvider.GetService(typeof (IMainMenuService)) is IMainMenuService service3)
    {
      MenuButtonItem menuButtonItem1 = new MenuButtonItem(LocalizationHolder.rm.GetString("DatabaseConfigurator_162"));
      menuButtonItem1.CommandName = "database_statistics";
      menuButtonItem1.Click += new EventHandler(this.DatabaseStatisticsMenuClick);
      menuButtonItem1.ImageIndex = imageIndex3;
      service3.RegisterMenuItems(MainMenuItemSite.TuningMiddle, MainMenuItemPosition.Default, menuButtonItem1);
      service1?.RegisterWindowOpeningHandler("databaseStatistics", new EventHandler(this.DatabaseStatisticsMenuClick));
      MenuButtonItem menuButtonItem2 = new MenuButtonItem(LocalizationHolder.rm.GetString("DatabaseConfigurator_161"));
      menuButtonItem2.CommandName = "database_security";
      menuButtonItem2.Click += new EventHandler(this.DatabaseSecurityMenuClick);
      menuButtonItem2.ImageIndex = imageIndex2;
      service3.RegisterMenuItems(MainMenuItemSite.TuningTop, MainMenuItemPosition.First, menuButtonItem2);
      service1?.RegisterWindowOpeningHandler("SecurityWindow", new EventHandler(this.DatabaseSecurityMenuClick));
      MenuButtonItem menuButtonItem3 = new MenuButtonItem(LocalizationHolder.rm.GetString("DatabaseConfigurator_160"));
      menuButtonItem3.CommandName = "database_configurator";
      menuButtonItem3.BeginGroup = false;
      menuButtonItem3.Click += new EventHandler(this.DatabaseConfiguratorMenuClick);
      menuButtonItem3.ImageIndex = imageIndex1;
      service3.RegisterMenuItems(MainMenuItemSite.TuningTop, MainMenuItemPosition.Second, menuButtonItem3);
      service1?.RegisterWindowOpeningHandler("databaseConfigurator", new EventHandler(this.DatabaseConfiguratorMenuClick));
      List<MenuButtonItem> menuButtonItemList = new List<MenuButtonItem>();
      MenuButtonItem menuButtonItem4 = new MenuButtonItem(LocalizationHolder.rm.GetString("DatabaseConfigurator_164"), new EventHandler(this.RebuildViewsMenuClick));
      menuButtonItem4.CommandName = "AdminUtils.RebuildViews";
      menuButtonItemList.Add(menuButtonItem4);
      MenuButtonItem menuButtonItem5 = new MenuButtonItem("Обновить представления для указанных объектов", new EventHandler(this.UpdateViews4Objects));
      menuButtonItem5.CommandName = "AdminUtils.UpdateViews4Objects";
      menuButtonItemList.Add(menuButtonItem5);
      MenuButtonItem menuButtonItem6 = new MenuButtonItem(LocalizationHolder.rm.GetString("DatabaseConfigurator_165"), new EventHandler(this.ReloadCacheMenuClick));
      menuButtonItem6.CommandName = "AdminUtils.ReloadCache";
      menuButtonItemList.Add(menuButtonItem6);
      MenuButtonItem menuButtonItem7 = new MenuButtonItem(LocalizationHolder.rm.GetString("DatabaseConfigurator_166"), new EventHandler(this.RepairDataMenuClick));
      menuButtonItem7.CommandName = "AdminUtils.RepairData";
      menuButtonItemList.Add(menuButtonItem7);
      MenuButtonItem menuButtonItem8 = new MenuButtonItem(LocalizationHolder.rm.GetString("DatabaseConfigurator_167"), new EventHandler(this.ClearTrashMenuClick));
      menuButtonItem8.CommandName = "AdminUtils.ClearTrash";
      menuButtonItemList.Add(menuButtonItem8);
      MenuButtonItem menuButtonItem9 = new MenuButtonItem(LocalizationHolder.rm.GetString("DatabaseConfigurator_168"), new EventHandler(this.RebuildIndexMenuClick));
      menuButtonItem9.CommandName = "AdminUtils.RebuildIndex";
      menuButtonItemList.Add(menuButtonItem9);
      MenuButtonItem menuButtonItem10 = new MenuButtonItem(LocalizationHolder.rm.GetString("DatabaseConfigurator_217"), new EventHandler(this.FindInvaildAttributes));
      menuButtonItem10.CommandName = "AdminUtils.FindInvaildAttributes";
      menuButtonItemList.Add(menuButtonItem10);
      MenuButtonItem menuButtonItem11 = new MenuButtonItem("Очистить атрибут 'Узел информационной сети'", new EventHandler(this.ClearSiteIDMenuClick));
      menuButtonItem11.CommandName = "AdminUtils.ClearSiteID";
      menuButtonItemList.Add(menuButtonItem11);
      MenuButtonItem menuButtonItem12 = new MenuButtonItem("Проверить очередь индексации объектов", new EventHandler(this.GetIndexQueue));
      menuButtonItem12.CommandName = "AdminUtils.GetIndexQueue";
      menuButtonItemList.Add(menuButtonItem12);
      MenuButtonItem menuButtonItem13 = new MenuButtonItem("Добавить объекты в очередь на индексацию", new EventHandler(this.AddToIndexQueue));
      menuButtonItem13.CommandName = "AdminUtils.AddToIndexQueue";
      menuButtonItemList.Add(menuButtonItem13);
      MenuButtonItem menuButtonItem14 = new MenuButtonItem("Вывести список открытых сессий", new EventHandler(this.GetSessionsList));
      menuButtonItem14.CommandName = "AdminUtils.GetSessionsList";
      menuButtonItemList.Add(menuButtonItem14);
      MenuButtonItem menuButtonItem15 = new MenuButtonItem(LocalizationHolder.rm.GetString("DatabaseConfigurator_250"), new EventHandler(this.EditingContextsClearModificationGroup));
      menuButtonItem15.CommandName = "AdminUtils.EditingContextsClearModificationGroup";
      menuButtonItemList.Add(menuButtonItem15);
      MenuButtonItem menuButtonItem16 = new MenuButtonItem("Проверить наличие петель в составе", new EventHandler(this.CheckCycleRelations));
      menuButtonItem16.CommandName = "AdminUtils.CheckCycleRelations";
      menuButtonItemList.Add(menuButtonItem16);
      MenuButtonItem menuButtonItem17 = new MenuButtonItem(LocalizationHolder.rm.GetString("FixLCStepsCommand"), new EventHandler(this.FixLCStepsMenuClick));
      menuButtonItem17.CommandName = "AdminUtils.FixLCSteps";
      menuButtonItemList.Add(menuButtonItem17);
      MenuButtonItem menuButtonItem18 = new MenuButtonItem("Диагностика ошибок патча БД", new EventHandler(this.ShowMetadataUpdatesLog));
      menuButtonItem18.CommandName = "AdminUtils.ShowMetadataUpdatesLog";
      menuButtonItemList.Add(menuButtonItem18);
      MenuButtonItem menuButtonItem19 = new MenuButtonItem(LocalizationHolder.rm.GetString("DatabaseConfigurator_251"), new EventHandler(this.ParseImportedEco));
      menuButtonItem19.CommandName = "AdminUtils.ParseImportedEco";
      menuButtonItem19.BeginGroup = true;
      menuButtonItemList.Add(menuButtonItem19);
      MenuButtonItem menuButtonItem20 = new MenuButtonItem(LocalizationHolder.rm.GetString("DatabaseConfigurator_252"), new EventHandler(this.DeleteObjectRelation));
      menuButtonItem20.CommandName = "AdminUtils.DeleteObjectRelation";
      menuButtonItemList.Add(menuButtonItem20);
      MenuButtonItem menuButtonItem21 = new MenuButtonItem(LocalizationHolder.rm.GetString("DatabaseConfigurator_236"), new EventHandler(this.FixBlobs));
      menuButtonItem21.CommandName = "AdminUtils.FixBlobs";
      menuButtonItemList.Add(menuButtonItem21);
      MenuButtonItem menuButtonItem22 = new MenuButtonItem("Поиск неиспользуемых атрибутов", new EventHandler(this.FindIdleAttributes));
      menuButtonItem22.CommandName = "AdminUtils.IdleAttributes";
      menuButtonItemList.Add(menuButtonItem22);
      service3.RegisterMenuItemsGroup(MainMenuItemSite.AdministratorUtilities, MainMenuItemPosition.Default, false, menuButtonItemList.ToArray());
      MenuButtonItem menuButtonItem23 = new MenuButtonItem(LocalizationHolder.rm.GetString("DatabaseConfigurator_263"), new EventHandler(this.TimeTable));
      menuButtonItem23.CommandName = "TimeTableCommand";
      service3.RegisterMenuItems(MainMenuItemSite.TuningMiddle, MainMenuItemPosition.Default, menuButtonItem23);
    }
    IContentProvider service4 = (IContentProvider) this._serviceProvider.GetService(typeof (IContentProvider));
    if (service4 != null)
      service4.ContentCallback += new GetContentCallback(this.ContentProvider_ContentCallback);
    if (this._serviceProvider is IServiceContainer serviceProvider)
    {
      serviceProvider.AddService(typeof (ILCSchema4ObjTypeFormProvider), (object) new LCSchema4ObjTypeFormProvider());
      serviceProvider.AddService(typeof (IDatabaseConfiguratorService), (object) new DatabaseConfiguratorService());
    }
    INavigationBar service5 = (INavigationBar) this._serviceProvider.GetService(typeof (INavigationBar));
    if (service5 != null && service5.FindPane("adminPane") is IAppPane pane)
    {
      pane.Add(LocalizationHolder.rm.GetString("DatabaseConfigurator_169"), new EventHandler(this.DatabaseConfiguratorMenuClick), imageIndex1);
      pane.Add(LocalizationHolder.rm.GetString("DatabaseConfigurator_170"), new EventHandler(this.DatabaseSecurityMenuClick), imageIndex2);
      pane.Add(LocalizationHolder.rm.GetString("DatabaseConfigurator_171"), new EventHandler(this.DatabaseStatisticsMenuClick), imageIndex3);
    }
    AccountsPolicyPage accountsPolicyPage = new AccountsPolicyPage(this._serviceProvider);
    DocTypesPage docTypesPage = new DocTypesPage(this._serviceProvider);
    DatabasePropertiesPage databasePropertiesPage = new DatabasePropertiesPage(this._serviceProvider);
    SnapshotSettingsPage snapshotSettingsPage = new SnapshotSettingsPage(this._serviceProvider);
    SystemDiagnosticsSettingsPage diagnosticsSettingsPage = new SystemDiagnosticsSettingsPage(this._serviceProvider);
    QuantityPhysListSettingsPage listSettingsPage = new QuantityPhysListSettingsPage(this._serviceProvider);
    Holder.GuidMapper = (IGuidMapper) this._serviceProvider.GetService(typeof (IGuidMapper));
    Holder.Factory = (IFactory) this._serviceProvider.GetService(typeof (IFactory));
    Holder.IconService = (ICategoryTypeIconService) this._serviceProvider.GetService(typeof (ICategoryTypeIconService));
    Holder.NotificationService = (INotificationService) this._serviceProvider.GetService(typeof (INotificationService));
    Holder.NamedImageList = this._serviceProvider.GetService(typeof (INamedImageList)) as INamedImageList;
    if (Holder.NamedImageList == null)
      Holder.NamedImageList = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
    DatabaseConfiguratorConsts.SecurityCategoryID = Holder.GuidMapper.Register(DatabaseConfiguratorConsts.CategorySecurityGuid);
    Holder.Factory.AddNodeType(DatabaseConfiguratorConsts.SecurityCategoryID, typeof (SecurityNode));
    ICommandsProvider provider = (ICommandsProvider) new CommandsProvider();
    DatabaseConfiguratorConsts.EventLogCategoryID = Holder.GuidMapper.Register(DatabaseConfiguratorConsts.CategoryEventLogGuid);
    Holder.Factory.AddNodeType(DatabaseConfiguratorConsts.EventLogCategoryID, typeof (Intermech.Security.EventLog.Node));
    Holder.Factory.AddCommandsProvider(DatabaseConfiguratorConsts.EventLogCategoryID, provider);
    Holder.Factory.AddViewsProvider(DatabaseConfiguratorConsts.EventLogCategoryID, (IViewsProvider) new Intermech.Security.EventLog.ViewsProvider());
    DatabaseConfiguratorConsts.EventFilterCategoryID = Holder.GuidMapper.Register(DatabaseConfiguratorConsts.CategoryEventFilterGuid);
    Holder.Factory.AddNodeType(DatabaseConfiguratorConsts.EventFilterCategoryID, typeof (FilterNode));
    Holder.Factory.AddCommandsProvider(DatabaseConfiguratorConsts.EventFilterCategoryID, provider);
    Holder.Factory.AddViewsProvider(DatabaseConfiguratorConsts.EventFilterCategoryID, (IViewsProvider) new FilterViewsProvider());
    Holder.Factory.AddCommandsProvider(10, provider);
    Holder.Factory.AddViewsProvider(1, (IViewsProvider) new SecurityViewProvider());
    Holder.Factory.AddViewsProvider(DatabaseConfiguratorConsts.SecurityCategoryID, (IViewsProvider) new SecurityRootViewProvider());
    IClientMetadataCache service6 = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    Holder.Factory.AddViewsProvider(1, service6.GetObjectType(new Guid("cad00014-306c-11d8-b4e9-00304f19f545"), true).ObjectType, (IViewsProvider) new FileStorageProvider());
    using (Stream manifestResourceStream = this.GetType().Assembly.GetManifestResourceStream("Intermech.DatabaseConfigurator.Resources.SecurityRoot.ico"))
    {
      using (Icon icon = new Icon(manifestResourceStream))
        Holder.IconService.AddIcon(icon, DatabaseConfiguratorConsts.SecurityCategoryID);
    }
    using (Stream manifestResourceStream = this.GetType().Assembly.GetManifestResourceStream("Intermech.DatabaseConfigurator.Resources.user_gr.ico"))
    {
      using (Icon icon = new Icon(manifestResourceStream))
        Holder.IconService.AddIcon(icon, ClientConsts.UsersGroupsCategoryID);
    }
    using (Stream manifestResourceStream = this.GetType().Assembly.GetManifestResourceStream("Intermech.DatabaseConfigurator.Resources.Roles.ico"))
    {
      using (Icon icon = new Icon(manifestResourceStream))
        Holder.IconService.AddIcon(icon, ClientConsts.UsersRolesCategoryID);
    }
    using (Stream manifestResourceStream = this.GetType().Assembly.GetManifestResourceStream("Intermech.DatabaseConfigurator.Resources.MeasuredItems.ico"))
    {
      using (Icon icon = new Icon(manifestResourceStream))
        Holder.IconService.AddIcon(icon, ClientConsts.MeasuresCategoryID);
    }
    using (Stream manifestResourceStream = this.GetType().Assembly.GetManifestResourceStream("Intermech.DatabaseConfigurator.Resources.EventLog.ico"))
    {
      using (Icon icon = new Icon(manifestResourceStream))
        Holder.IconService.AddIcon(icon, DatabaseConfiguratorConsts.EventLogCategoryID);
    }
    using (Stream manifestResourceStream = this.GetType().Assembly.GetManifestResourceStream("Intermech.DatabaseConfigurator.Resources.EventLog.ico"))
    {
      using (Icon icon = new Icon(manifestResourceStream))
        Holder.IconService.AddIcon(icon, 10);
    }
    using (Stream manifestResourceStream = this.GetType().Assembly.GetManifestResourceStream("Intermech.DatabaseConfigurator.Resources.EventLogFilter.ico"))
    {
      using (Icon icon = new Icon(manifestResourceStream))
        Holder.IconService.AddIcon(icon, DatabaseConfiguratorConsts.EventFilterCategoryID);
    }
    using (MemoryStream memoryStream = DBCResourcesAccess.LoadResurce(DBCResourcesAccess.nameSpace + "EventLogFilter.ico"))
    {
      using (Icon icon = new Icon((Stream) memoryStream))
        Holder.NamedImageList.Add(icon, "imgEventLogFilterIcon");
    }
    if (this._serviceProvider.GetService(typeof (IDatabaseConfiguratorService)) is IDatabaseConfiguratorService service7)
    {
      DatabaseConfiguratorConsts.ObjectTypesCategoryID = service7.RegisterCategoryProps(4, (ICategoryProps) new ClassifiedObjectType());
      service7.RegisterCategoryProps(7, (ICategoryProps) new LCStepScriptProperty());
    }
    if (this._serviceProvider.GetService(typeof (IPropertyPagesService)) is IPropertyPagesService service8)
      service8.AddPage(LocalizationHolder.rm.GetString("DatabaseConfigurator_254"), (IPropertyPage) new MailSettingsControl());
    MailProxySettings mailProxySettings = new MailProxySettings(this._serviceProvider);
    LdapSettingsPage ldapSettingsPage = new LdapSettingsPage(this._serviceProvider);
    if ((ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).IsAdmin)
    {
      if (service3 == null)
        service3 = ServicesManager.GetService(typeof (IMainMenuService)) as IMainMenuService;
      if (service3 != null)
      {
        MenuButtonItem menuButtonItem = new MenuButtonItem("Настройки синхронизации со службами каталогов", new EventHandler(this.LdapConfigClick));
        menuButtonItem.CommandName = "AdminUtils.LdapConfig";
        service3.RegisterMenuItems(MainMenuItemSite.AdministratorUtilities, MainMenuItemPosition.Default, menuButtonItem);
      }
    }
    MenuTemplate contextMenuTemplate = Holder.Factory.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("ImportFromNTDomain", LocalizationHolder.rm.GetString("DatabaseConfigurator_172"), -1, 20, 10));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("LdapSync", LocalizationHolder.rm.GetString("LdapSync"), -1, 20, 11));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("ImportUsersProfile", LocalizationHolder.rm.GetString("DatabaseConfigurator_173"), -1, 20, 12));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("FileStorageInfo", LocalizationHolder.rm.GetString("DatabaseConfigurator_174"), -1, 17, 10));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("GetAccessReport", LocalizationHolder.rm.GetString("GetAccessReport"), -1, 17, 11));
      contextMenuTemplate.Nodes.Add(new MenuTemplateNode("GetAccessReportForObjects", LocalizationHolder.rm.GetString("GetAccessReportForObjects"), -1, 17, 12));
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
    Holder.Factory.AddCommandsProvider(1, service6.GroupsTypeID, (ICommandsProvider) new ImportFromNTMenuProvider());
    Holder.Factory.AddCommandsProvider(1, service6.GroupsTypeID, (ICommandsProvider) new LdapSyncMenuProvider());
    Holder.Factory.AddCommandsProvider(1, service6.UsersTypeID, (ICommandsProvider) new ImportUsersProfile());
    Holder.Factory.AddCommandsProvider(1, service6.GetObjectType(new Guid("cad00014-306c-11d8-b4e9-00304f19f545"), true).ObjectType, (ICommandsProvider) new FileStorageInfoProvider());
    Holder.Factory.AddCommandsProvider(15, (ICommandsProvider) new FilesProvider());
    Holder.Factory.AddCommandsProvider(1, (ICommandsProvider) new GetAccessReportProvider());
    Holder.Factory.AddNodeType(1, service6.GetObjectType(new Guid("cad00014-306c-11d8-b4e9-00304f19f545"), true).ObjectType, typeof (FileStorageNode));
    DictHolder.DictStartup = new DictStartup(this._serviceProvider);
  }

  private void ShowMetadataUpdatesLog(object sender, EventArgs e)
  {
    using (ServerLogForm serverLogForm = new ServerLogForm())
    {
      int num = (int) serverLogForm.ShowDialog();
    }
  }

  private void LdapConfigClick(object sender, EventArgs e)
  {
    using (LdapConfigsPurgeForm configsPurgeForm = new LdapConfigsPurgeForm())
    {
      int num = (int) configsPurgeForm.ShowDialog();
    }
  }

  protected override void DoInitializeIOCContainer()
  {
    base.DoInitializeIOCContainer();
    this.IOCContainer.Load((INinjectModule) new ScriptCheckerNinjectModule());
  }

  protected override void CreateSubModules(LazyInitializerModuleGroup subModules)
  {
    base.CreateSubModules(subModules);
    subModules.Add<ScriptCheckerInitializerModule>();
  }

  internal DockControl ContentProvider_ContentCallback(Guid guid, string persistString)
  {
    if (guid == DatabaseConfiguratorControl._databaseConfiguratorControlGuid)
    {
      if (this._dbCfgWindow == null)
        this._dbCfgWindow = (DockControl) new DatabaseConfiguratorControl();
      return this._dbCfgWindow;
    }
    if (guid == StatisticsControl._statisticsControlGuid)
    {
      if (this._dbStatsWindow == null)
        this._dbStatsWindow = (DockControl) new StatisticsControl();
      return this._dbStatsWindow;
    }
    if (guid == FixInvalidBlobsControl.controlGuid)
    {
      if (this.fixControl == null)
        this.fixControl = new FixInvalidBlobsControl();
      return (DockControl) this.fixControl;
    }
    if (!(guid == this.SecurityWindowGUID))
      return (DockControl) null;
    if (this.SecurityWindowIsInvalid())
      this.CreateSecurityWindow(persistString);
    return this.securityWindow;
  }

  internal void DatabaseConfiguratorMenuClick(object sender, EventArgs e)
  {
    DockManager service = (DockManager) ServicesManager.GetService(typeof (DockManager));
    if (service == null)
      return;
    if (this._dbCfgWindow == null)
    {
      DockControl dockControl = service.FindDockControl(DatabaseConfiguratorControl._databaseConfiguratorControlGuid);
      if (dockControl != null)
      {
        dockControl.Activate();
        this._dbCfgWindow = service.FindDockControl(DatabaseConfiguratorControl._databaseConfiguratorControlGuid);
      }
      if (this._dbCfgWindow == null)
        this._dbCfgWindow = (DockControl) new DatabaseConfiguratorControl();
    }
    this._dbCfgWindow.Show(service);
    this._dbCfgWindow.Activate();
  }

  internal void DatabaseSecurityMenuClick(object sender, EventArgs e)
  {
    DockManager service = (DockManager) ServicesManager.GetService(typeof (DockManager));
    if (service != null && this.SecurityWindowIsInvalid())
    {
      DockControl dockControl = service.FindDockControl(this.SecurityWindowGUID);
      if (dockControl != null)
      {
        dockControl.Activate();
        this.securityWindow = service.FindDockControl(this.SecurityWindowGUID);
      }
    }
    if (this.SecurityWindowIsInvalid())
      this.CreateSecurityWindow(string.Empty);
    this.securityWindow.Show(service);
    this.securityWindow.Activate();
  }

  private bool SecurityWindowIsInvalid()
  {
    return this.securityWindow == null || this.securityWindow.IsDisposed;
  }

  private void CreateSecurityWindow(string persistString)
  {
    INamedImageList service = (INamedImageList) this._serviceProvider.GetService(typeof (INamedImageList));
    int num = service == null ? -1 : service.ImageIndex("imgKeys");
    this.securityWindow = (DockControl) new WellKnownNavWindow();
    (this.securityWindow as WellKnownNavWindow).WellKnownName = "SecurityWindow";
    this.securityWindow.Guid = this.SecurityWindowGUID;
    this.securityWindow.Text = LocalizationHolder.rm.GetString("DatabaseConfigurator_175");
    this.securityWindow.TabImageIndex = num;
    (this.securityWindow as WellKnownNavWindow).TreeView.SetColumns(Intermech.Navigator.Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending));
    (this.securityWindow as WellKnownNavWindow).TreeView.Build((IDescriptor) new SecurityNodeDescriptor());
    if (string.IsNullOrEmpty(persistString))
      return;
    try
    {
      XmlDocument xmlDoc = new XmlDocument();
      xmlDoc.LoadXml(persistString);
      (this.securityWindow as WellKnownNavWindow).RestoreState(xmlDoc);
    }
    catch
    {
      (this.securityWindow as WellKnownNavWindow).WellKnownName = string.Empty;
      this.securityWindow.HideOnClose = false;
      this.securityWindow.Close();
      this.securityWindow.Dispose();
    }
  }

  internal void DatabaseStatisticsMenuClick(object sender, EventArgs e)
  {
    if (this._dbStatsWindow == null)
      this._dbStatsWindow = (DockControl) new StatisticsControl();
    DockManager service = (DockManager) this._serviceProvider.GetService(typeof (DockManager));
    if (service == null)
      return;
    this._dbStatsWindow.Show(service);
    this._dbStatsWindow.Activate();
  }

  internal void RebuildViewsMenuClick(object sender, EventArgs e)
  {
    if (MessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_176"), LocalizationHolder.rm.GetString("DatabaseConfigurator_177"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes || MessageBox.Show(AdminConsts.AdminProcWarningMessage, LocalizationHolder.rm.GetString("DatabaseConfigurator_177"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      (sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) as IAdminUtilsService).CheckAdminProcedureAccess(sessionKeeper.Session.SessionGUID, "Перегенерация представлений данных");
    ViewsBuilder task = new ViewsBuilder();
    ((IBackgroundTaskView) this._serviceProvider.GetService(typeof (IBackgroundTaskView)))?.AddTask((IBackgroundTask) task);
    new Thread(new ThreadStart(task.Rebuild))
    {
      IsBackground = true
    }.Start();
  }

  private ISimpleSelectedItems GetNavigatorSelection()
  {
    return (ISimpleSelectedItems) SelectedItemsHelper.GetNavigatorSelection();
  }

  internal void ClearSiteIDMenuClick(object sender, EventArgs e)
  {
    ISimpleSelectedItems navigatorSelection = this.GetNavigatorSelection();
    if (navigatorSelection != null && navigatorSelection.Count > 0)
    {
      if (MessageBox.Show("Данная команда очистит атрибут 'Узел информационной сети' у выбранных объектов. Это может привести к ошибкам при передаче данных объектов через IPS WebPortal. Продолжить операцию?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
        return;
      List<long> longList = new List<long>(navigatorSelection.Count);
      for (int index = 0; index < navigatorSelection.Count; ++index)
      {
        if (navigatorSelection.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData && itemData.ObjectID != 0L)
          longList.Add(itemData.ObjectID);
      }
      if (longList.Count <= 0)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (!(sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) is IAdminUtilsService customService))
          return;
        customService.ClearSiteIDs(sessionKeeper.Session.SessionGUID, longList.ToArray());
      }
    }
    else
    {
      int num = (int) MessageBox.Show("Не выбрано ни одного объекта.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
  }

  internal void ClearTrashMenuClick(object sender, EventArgs e)
  {
    if (MessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_178"), LocalizationHolder.rm.GetString("DatabaseConfigurator_179"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
      return;
    DBTrashCleaner task = new DBTrashCleaner();
    ((IBackgroundTaskView) this._serviceProvider.GetService(typeof (IBackgroundTaskView)))?.AddTask((IBackgroundTask) task);
    new Thread(new ThreadStart(task.ClearTrash))
    {
      IsBackground = true
    }.Start();
  }

  internal void RebuildIndexMenuClick(object sender, EventArgs e)
  {
    if (MessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_180"), LocalizationHolder.rm.GetString("DatabaseConfigurator_181"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes || MessageBox.Show(AdminConsts.AdminProcWarningMessage, LocalizationHolder.rm.GetString("DatabaseConfigurator_177"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      (sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) as IAdminUtilsService).CheckAdminProcedureAccess(sessionKeeper.Session.SessionGUID, "Перегенерация представлений данных");
    IndexRebuilder task = new IndexRebuilder();
    ((IBackgroundTaskView) this._serviceProvider.GetService(typeof (IBackgroundTaskView)))?.AddTask((IBackgroundTask) task);
    new Thread(new ThreadStart(task.RebuildIndex)).Start();
  }

  internal void FindInvaildAttributes(object sender, EventArgs e)
  {
    InvalidAttributes task = new InvalidAttributes();
    ((IBackgroundTaskView) this._serviceProvider.GetService(typeof (IBackgroundTaskView)))?.AddTask((IBackgroundTask) task);
    Thread thread = new Thread(new ThreadStart(task.FindInvaildAttributes));
    thread.IsBackground = true;
    thread.TrySetApartmentState(ApartmentState.STA);
    thread.Start();
  }

  internal void GetIndexQueue(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IGlobalIndexSettings)) is IGlobalIndexSettings customService))
        return;
      customService.GetIndexQueue(sessionKeeper.Session.SessionGUID);
      int num = (int) MessageBox.Show("Очередь на индексацию пуста.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
  }

  internal void AddToIndexQueue(object sender, EventArgs e)
  {
    List<long> objectsList = this.GetObjectsList();
    if (objectsList.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IGlobalIndexHelper customService = (IGlobalIndexHelper) sessionKeeper.Session.GetCustomService(typeof (IGlobalIndexHelper));
      if (customService == null)
        return;
      int queue = customService.AddToQueue(sessionKeeper.Session.SessionGUID, objectsList.ToArray());
      if (queue > 0)
      {
        int num1 = (int) MessageBox.Show($"В очередь для переиндексации добавлено {queue} атрибута(ов) объектов.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else
      {
        int num2 = (int) MessageBox.Show("Среди отмеченных объектов не найдено атрибутов для переиндексации.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
    }
  }

  internal void GetSessionsList(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      string[] sessionsList = (sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) as IAdminUtilsService).GetSessionsList(sessionKeeper.Session.SessionGUID);
      if (sessionsList == null)
        return;
      string category = "Список пользовательских сессий на сервере приложений";
      IOutputView service = ServicesManager.GetService(typeof (IOutputView)) as IOutputView;
      service.ClearText(category);
      for (int index = 0; index < sessionsList.Length; ++index)
        service.WriteString(category, sessionsList[index]);
      service.Activate(category);
      service.ShowView();
    }
  }

  internal void UpdateViews4Objects(object sender, EventArgs e)
  {
    List<long> objectsList = this.GetObjectsList();
    if (objectsList.Count <= 0 || MessageBox.Show("Данная команда обновит представления данных для указанных объектов/связей. Продолжить?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      int count = objectsList.Count;
      IAdminUtilsService customService = sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) as IAdminUtilsService;
      customService.RepairViews4Objects(sessionKeeper.Session.SessionGUID, objectsList.ToArray());
      ISimpleSelectedItems service = ServicesManager.GetService(typeof (ISimpleSelectedItems)) as ISimpleSelectedItems;
      objectsList.Clear();
      for (int index = 0; index < service.Count; ++index)
      {
        if (service.GetItemData(index, typeof (IDBRelationID)) is IDBRelationID itemData && itemData.Value != -1L && objectsList.IndexOf(itemData.Value) < 0)
          objectsList.Add(itemData.Value);
      }
      string str = string.Empty;
      if (objectsList.Count > 0)
      {
        customService.RepairViews4Relations(sessionKeeper.Session.SessionGUID, objectsList.ToArray());
        str = string.Format(" и связи(ей)", (object) objectsList.Count);
      }
      int num = (int) MessageBox.Show($"Обновлены представления для {count} объекта(ов){str}.", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
  }

  private List<long> GetObjectsList()
  {
    if (!(ServicesManager.GetService(typeof (ISimpleSelectedItems)) is ISimpleSelectedItems service) || service.Count == 0)
    {
      int num = (int) MessageBox.Show("Не отмечен ни один объект.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      return new List<long>(0);
    }
    List<long> objectsList = new List<long>(service.Count);
    for (int index = 0; index < service.Count; ++index)
    {
      if (service.GetItemData(index, typeof (IDBObjectID)) is IDBObjectID itemData && objectsList.IndexOf(itemData.Value) < 0)
        objectsList.Add(itemData.Value);
    }
    return objectsList;
  }

  internal void CheckCycleRelations(object sender, EventArgs e)
  {
    if (!(ServicesManager.GetService(typeof (ISimpleSelectedItems)) is ISimpleSelectedItems service1) || service1.Count == 0)
    {
      int num1 = (int) MessageBox.Show("Не отмечен ни один объект.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
    else
    {
      List<long> longList = new List<long>(service1.Count);
      for (int index = 0; index < service1.Count; ++index)
      {
        if (service1.GetItemData(index, typeof (IDBObjectID)) is IDBObjectID itemData && longList.IndexOf(itemData.ID) < 0)
          longList.Add(itemData.ID);
      }
      if (longList.Count <= 0 || MessageBox.Show("Данная команда проверит наличие циклических связей в составе всех версий указанных объектов. Продолжить?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        string[] cycleRelations = (sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) as IAdminUtilsService).FindCycleRelations(sessionKeeper.Session.SessionGUID, longList.ToArray());
        if (cycleRelations.Length == 0)
        {
          int num2 = (int) MessageBox.Show("Петель в составах указанных объектов не найдено.", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        else
        {
          string category = "Поиск петель в составе объектов";
          IOutputView service = ServicesManager.GetService(typeof (IOutputView)) as IOutputView;
          service.ClearText(category);
          for (int index = 0; index < cycleRelations.Length; ++index)
            service.WriteString(category, cycleRelations[index]);
          service.Activate(category);
          service.ShowView();
        }
      }
    }
  }

  internal void EditingContextsClearModificationGroup(object sender, EventArgs e)
  {
    if (!(ServicesManager.GetService(typeof (ISimpleSelectedItems)) is ISimpleSelectedItems service) || service.Count == 0)
      return;
    List<long> versionIDs = new List<long>(service.Count);
    for (int index = 0; index < service.Count; ++index)
    {
      if (service.GetItemData(index, typeof (IDBObjectID)) is IDBObjectID itemData && versionIDs.IndexOf(itemData.Value) < 0)
        versionIDs.Add(itemData.Value);
    }
    List<long> objectIDs = (List<long>) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IDBEditingContextsService)) is IDBEditingContextsService customService))
        return;
      objectIDs = customService.ClearModificationGroupID((object) sessionKeeper.Session.SessionGUID, versionIDs, true);
    }
    if (objectIDs == null || objectIDs.Count == 0)
    {
      int num1 = (int) IMMessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_255"), LocalizationHolder.rm.GetString("DatabaseConfigurator_256"), MessageBoxButtons.OK, IMMessageBoxImage.Information);
    }
    else
    {
      (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", (IList<long>) objectIDs));
      int num2 = (int) IMMessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_255"), LocalizationHolder.rm.GetString("DatabaseConfigurator_257"), MessageBoxButtons.OK, IMMessageBoxImage.Information);
    }
  }

  internal void ParseImportedEco(object sender, EventArgs e)
  {
    bool flag = (ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IEcoImportService)) is IEcoImportService customService1 ? customService1.IsRunning : throw new Exception(LocalizationHolder.rm.GetString("DatabaseConfigurator_258"));
    long progress = flag ? customService1.Progress : 0L;
    if (flag && IMMessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_255"), string.Format(LocalizationHolder.rm.GetString("DatabaseConfigurator_259") + LocalizationHolder.rm.GetString("DatabaseConfigurator_260"), (object) progress), MessageBoxButtons.YesNo, IMMessageBoxImage.Information) != DialogResult.Yes)
      return;
    IEcoImportService customService2 = (ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IEcoImportService)) as IEcoImportService;
    if (!customService2.IsRunning ? customService2.Start() : !customService2.Stop())
    {
      int num1 = (int) IMMessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_255"), LocalizationHolder.rm.GetString("DatabaseConfigurator_261"), MessageBoxButtons.OK, IMMessageBoxImage.Information);
    }
    else
    {
      int num2 = (int) IMMessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_255"), LocalizationHolder.rm.GetString("DatabaseConfigurator_262"), MessageBoxButtons.OK, IMMessageBoxImage.Information);
    }
  }

  internal void DeleteObjectRelation(object sender, EventArgs e)
  {
    if (!(ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service) || !service.IsAdmin)
      return;
    int num = (int) DeleteObjectRelationForm.Execute();
  }

  internal void FixBlobs(object sender, EventArgs e)
  {
    FixBlobsClass task = new FixBlobsClass();
    ((IBackgroundTaskView) this._serviceProvider.GetService(typeof (IBackgroundTaskView)))?.AddTask((IBackgroundTask) task);
    new Thread(new ThreadStart(task.FindInvalidBlobs))
    {
      IsBackground = true
    }.Start();
    while (task.State != BackgroundTaskState.Terminated && task.State != BackgroundTaskState.Stopped && task.State != BackgroundTaskState.Error)
      Application.DoEvents();
    if (task.State != BackgroundTaskState.Terminated && task.State != BackgroundTaskState.Stopped)
      return;
    DockManager service = ServicesManager.GetService(typeof (DockManager)) as DockManager;
    if (this.fixControl == null)
      this.fixControl = new FixInvalidBlobsControl();
    this.fixControl.LoadInformation(task.blobInfos);
    this.fixControl.Show(service);
    this.fixControl.Activate();
  }

  internal void FindIdleAttributes(object sender, EventArgs e)
  {
    DockManager service = (DockManager) ServicesManager.GetService(typeof (DockManager));
    IdleAttributesDockControl control = new IdleAttributesDockControl();
    service.DocumentContainer.AddDocument((DockControl) control);
    control.Activate();
  }

  private void TimeTable(object sender, EventArgs e)
  {
    using (TimedEventsShedulerForm eventsShedulerForm = new TimedEventsShedulerForm())
    {
      int num = (int) eventsShedulerForm.ShowDialog();
    }
  }

  internal void ReloadCacheMenuClick(object sender, EventArgs e)
  {
    if (MessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_184"), LocalizationHolder.rm.GetString("DatabaseConfigurator_185"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
      return;
    Cursor.Current = Cursors.WaitCursor;
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (!(sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) is IAdminUtilsService customService))
          throw new Exception(LocalizationHolder.rm.GetString(sc_5721.ssp_imclient_5722()));
        customService.ReloadCache(sessionKeeper.Session.SessionGUID);
        ((IClientSession) sessionKeeper.Session).ClientCache.ReloadCache(sessionKeeper.Session);
        MeasureHelper.Init(sessionKeeper.Session.GetMeasuresList());
        ((INotificationService) ServicesManager.GetService(typeof (INotificationService)))?.FireEvent((object) null, new NotificationEventArgs("MetadataCacheReloaded"));
      }
    }
    finally
    {
      Cursor.Current = Cursors.Default;
    }
  }

  internal void RepairDataMenuClick(object sender, EventArgs e)
  {
    if (MessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_187"), LocalizationHolder.rm.GetString("DatabaseConfigurator_188"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
      return;
    if (MessageBox.Show(AdminConsts.AdminProcWarningMessage, LocalizationHolder.rm.GetString("DatabaseConfigurator_177"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
      return;
    try
    {
      Cursor.Current = Cursors.WaitCursor;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (!(sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) is IAdminUtilsService customService))
          throw new Exception(LocalizationHolder.rm.GetString(sc_5721.ssp_imclient_5723()));
        customService.CheckAdminProcedureAccess(sessionKeeper.Session.SessionGUID, "Проверка целостности базы данных");
        string[] strArray = customService.RepairData(sessionKeeper.Session.SessionGUID);
        if (strArray == null)
          strArray = new string[1]
          {
            "Проверка целостности базы данных успешно завершена. Отчет о проверке находится в файле RepairData.log сервера приложениий IPS."
          };
        string category = "Проверка целостности базы данных";
        IOutputView service = ServicesManager.GetService(typeof (IOutputView)) as IOutputView;
        service.ClearText(category);
        for (int index = 0; index < strArray.Length; ++index)
          service.WriteString(category, strArray[index]);
        service.Activate(category);
        service.ShowView();
      }
    }
    finally
    {
      Cursor.Current = Cursors.Default;
    }
  }

  internal void FixLCStepsMenuClick(object sender, EventArgs e)
  {
    using (SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), "Укажите типы объектов", typeof (ObjectTypeFolder), true))
    {
      if (selectorForm.ShowDialog() != DialogResult.OK)
        return;
      IOutputView service = ServicesManager.GetService(typeof (IOutputView)) as IOutputView;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (!(sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) is IAdminUtilsService customService))
          throw new Exception(LocalizationHolder.rm.GetString(sc_5721.ssp_imclient_5724()));
        for (int index = 0; index < selectorForm.IDList.Count; ++index)
        {
          foreach (string fixLcStep in customService.FixLCSteps(sessionKeeper.Session.SessionGUID, Convert.ToInt32(selectorForm.IDList[index])))
            service.WriteString("Вывод", fixLcStep);
          service.ShowView();
        }
      }
    }
  }

  public void LoadConfiguration(IConfigurationManager configurationManager)
  {
    if (ConfigCache.Loaded)
      return;
    ConfigCache.LoadConfig();
  }

  public void SaveConfiguration(IConfigurationManager configurationManager)
  {
    ConfigCache.SaveConfig();
  }
}
