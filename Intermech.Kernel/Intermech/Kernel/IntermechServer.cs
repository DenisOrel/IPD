// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.IntermechServer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.ApplicationModel;
using Intermech.Checksums;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.CustomServices;
using Intermech.Interfaces.Data.Metadata;
using Intermech.Interfaces.LifeCycles;
using Intermech.Interfaces.MetadataUpdates;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.GlobalIndex;
using Intermech.Interfaces.Server.Security;
using Intermech.Interfaces.Services;
using Intermech.Interfaces.Snapshots;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Briefcase;
using Intermech.Kernel.Cache;
using Intermech.Kernel.CompositionView;
using Intermech.Kernel.Dictionary;
using Intermech.Kernel.FileStorages;
using Intermech.Kernel.GlobalIndex;
using Intermech.Kernel.LifeCycles;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services;
using Intermech.Kernel.Services.MetadataUpdates;
using Intermech.Kernel.Services.PortalServices;
using Intermech.Kernel.Services.ScheduledTasks;
using Intermech.Kernel.Snapshots;
using Intermech.Localization;
using Intermech.Protection;
using Intermech.Remoting.Optimized;
using Intermech.Remoting.Sponsors;
using Intermech.Search;
using Intermech.Search.AttributeChangeHistory;
using Intermech.Search.AutoConcretization;
using Intermech.Search.ButtonBars;
using Intermech.Search.CompositionByObjectTypesFilters;
using Intermech.Search.Concretization;
using Intermech.Search.ContextMenus;
using Intermech.Search.Data.Repositories;
using Intermech.Search.Discussions;
using Intermech.Search.EditingContexts;
using Intermech.Search.EventLogFilters;
using Intermech.Search.GroupAttributesChanging;
using Intermech.Search.ObjectListFilters;
using Intermech.Search.ObjectsVisiblity;
using Intermech.Search.PasswordChange;
using Intermech.Search.RecentObjects;
using Intermech.Search.UI;
using Intermech.Security;
using Intermech.Server.Data;
using Intermech.Tools.Kernel;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Reflection;
using System.Threading;


namespace Intermech.Kernel;

public class IntermechServer : LongLifeObject, IMServer
{
  private volatile bool _initialized;
  private IntermechServerInitParams _initParams;
  private ICustomServices _customServices;
  private Intermech.Interfaces.Configuration.ConfigurationManager _configManager;
  private string _configFileName;
  private PluginManager _pluginManager;
  private ConnectionStringService _connectionStringService;
  private DbManagerService _dbManagerService;
  private EventLogHelper _eventLogHelper;
  private CacheDataset _cacheDatasetService;
  private DBTimedEvents _dbTimedEventsService;
  private KernelUpdate _kernelUpdateHelper;
  private UpdateService _metadataUpdateService;
  private BlobStoragesPool _blobStoragesPoolService;
  private AdminUtilsService _adminUtilsService;
  private GlobalIndexService _globalIndexService;
  private ISelectionsService _selectionsService;
  private RemoteExceptionDataProvider _remoteExceptionDataProvider;
  private LazyInitializerModuleGroup _kernelRootServicesModules;
  private LazyInitializerModuleGroup _normalServicesModules;
  private Version _assemblyVersion;
  private IntermechServerAppConfiguration _appConfigService;
  private ReflectionHackLeaseRenewalService _leaseRenewalService;
  private IntermechServerLiveStatus _liveStatusService;
  private Lazy<byte[]> _userBanner;
  private static readonly Type updatableType = typeof (IUpdatable);
  private ElementStatusesPluginDescription _versionsSelectionPlugin = new ElementStatusesPluginDescription(8, "cad005f2-306c-11d8-b4e9-00304f19f545", "cad005f7-306c-11d8-b4e9-00304f19f545", LocalizationHolder.rm.GetString("Server_2"), LocalizationHolder.rm.GetString("Server_3"));
  private ElementStatusesPluginDescription _objectLevelStatuses;

  public IntermechServer()
  {
    this._initialized = false;
    this._assemblyVersion = this.GetType().Assembly.GetName().Version;
    this._appConfigService = new IntermechServerAppConfiguration(this);
    this._leaseRenewalService = new ReflectionHackLeaseRenewalService();
    this._liveStatusService = new IntermechServerLiveStatus(this);
    this._userBanner = new Lazy<byte[]>(new Func<byte[]>(this.ReadUserBannerFromFile), LazyThreadSafetyMode.PublicationOnly);
  }

  public void Initialize(IntermechServerInitParams initParams)
  {
    if (initParams == null)
      throw new ArgumentNullException(nameof (initParams));
    initParams.Validate();
    lock (this)
    {
      if (this._initialized)
        return;
      this._initParams = initParams;
      this.InitializeCore();
      if (!this._initParams.OnlyPatchBase)
        this.InitializePluginsAndExtensions();
      this._pluginManager.FinishAutoLoad();
      this._initialized = true;
      this.ReportInitializationComplete();
      if (this._initParams.OnlyPatchBase)
        return;
      this.PostInitialize();
    }
  }

  private void ReportInitializationComplete()
  {
    this._eventLogHelper.AddToTrace(LocalizationHolder.rm.GetString("Server_116"), Intermech.Consts.traceAlways, string.Empty);
  }

  private void InitializeCore()
  {
    this.InitializeDelayedTraceLogger();
    this.InitializeRemotingDynamicExtensions();
    this.InitializeCustomServices();
    this.InitializePackedStreamService();
    this.InitializeConfigurationManagerService();
    this.InitializePluginManagerService();
    this.InitializeConnectionStringService();
    this.InitializeDbManagerService();
    this.InitializeEventLogHelper();
    this.InitializeKernelUpdateHelper();
    Intermech.Interfaces.EventLog.Helper.Init();
    if (AdminUtilsService.ServerRunMode == ServerRunModes.Console)
      this.ShowDbConnectionString();
    using (IDbManager dbManager = this._dbManagerService.CreateDbManager())
    {
      if (this._initParams.ClearPatchFlag)
      {
        this.ClearPatchFlag(dbManager);
      }
      else
      {
        this._eventLogHelper.AddToTrace($"Сервер приложений стартован на устройстве {EnvironmentConsts.MachineName}...", Intermech.Consts.traceAlways, "");
        string rdbmsVersionMessage = dbManager.DataProvider.GetValidateRDBMSVersionMessage();
        if (rdbmsVersionMessage != string.Empty)
        {
          this._eventLogHelper.AddToTrace(rdbmsVersionMessage, Intermech.Consts.traceAlways, string.Empty);
          if (AdminUtilsService.ServerRunMode == ServerRunModes.Console)
            this.ShowValidateRDBMSMessage(rdbmsVersionMessage);
        }
        try
        {
          AdminUtilsService.RebuildOracleIndexes(dbManager, (IEventLogHelper) this._eventLogHelper);
          this.RunDatabaseStructurePatches(dbManager);
          this.InitializeKernelRootServices(dbManager);
          if (this._initParams.RebuildViewsMode)
          {
            this._initParams.OnlyPatchBase = true;
          }
          else
          {
            if (!this._initParams.SkipMetadataScripts)
            {
              this.RunMetadataUpdateScripts(dbManager);
              this.RunMetadataPatches();
            }
            this._cacheDatasetService.FillSyncParentObjectTypes(dbManager);
            (ServerServices.GetService(typeof (IIDHelper)) as IDHelper).LoadData(dbManager);
            KernelRoot.Init();
            this.InitializeNormalServices();
          }
        }
        finally
        {
          if (CacheDataset.PatchMode)
            this.ClearPatchFlag(dbManager);
        }
      }
    }
  }

  private void InitializeRemotingDynamicExtensions()
  {
    this.InstallRemoteExceptionDataProvider();
    ServerRemotingDynamicSettings.Instance.FormatterSinkInterceptorFactory = new Func<IServerFormatterSinkInterceptor>(this.CreateUserSessionLostInterceptor);
  }

  private void InitializeDelayedTraceLogger()
  {
    this.AddToServerServices(typeof (ITraceLoggerService), (object) new TraceLoggerService());
  }

  private void InitializeCustomServices() => this._customServices = this._initParams.CustomServices;

  private void InitializePackedStreamService()
  {
    this.AddToServerServices(typeof (IPackedStream), (object) new PackedStreamService());
  }

  private void InitializeConfigurationManagerService()
  {
    this._configManager = new Intermech.Interfaces.Configuration.ConfigurationManager("Intermech.Server");
    this.AddToServerServices(typeof (IConfigurationManager), (object) this._configManager);
    this.LoadConfiguration();
  }

  private void InitializePluginManagerService()
  {
    this._pluginManager = new PluginManager((IServiceProvider) ApplicationServices.Container, (IConfigurationManager) this._configManager, ServiceUtils.GetService<IOutputView>((object) ApplicationServices.Container, true), ServiceUtils.GetService<IAlertMessageService>((object) ApplicationServices.Container, true));
    if (this._initParams.PluginManagerConfigureAction != null)
      this._initParams.PluginManagerConfigureAction(this._pluginManager);
    this.AddToServerServices(typeof (IPluginManager), (object) this._pluginManager);
  }

  private void InitializeConnectionStringService()
  {
    this._connectionStringService = new ConnectionStringService();
    this.AddToServerServices(typeof (IConnectionStringService), (object) this._connectionStringService);
  }

  private void InitializeDbManagerService()
  {
    string s = System.Configuration.ConfigurationManager.AppSettings.Get("CommandTimeout");
    int result;
    if (s != null && int.TryParse(s, out result))
      DbManagerConfiguration.NormalCommandTimeout = result;
    this._dbManagerService = new DbManagerService((IConnectionStringService) this._connectionStringService, "Sql", (IEnumerable<string>) new string[2]
    {
      "Intermech.DataProvider.Oracle.dll",
      "Intermech.DataProvider.PostgreSQL.dll"
    });
    using (IDbManager dbManager = this._dbManagerService.CreateDbManager())
    {
      if (dbManager.DataProvider.CanUseIndexTablespace)
      {
        object obj = dbManager.ExecuteScalar("SELECT F_VALUE FROM IMS_CONFIGS WHERE F_MODULE_NAME = :moduleName AND F_USER_ID = 0 AND F_SECTION_ID = :sectID AND F_PARAM_NAME = :parName", dbManager.Parameter("moduleName", (object) "KERNEL"), dbManager.Parameter("sectID", (object) "COMMON"), dbManager.Parameter("parName", (object) "INDEX_TABLESPACE"));
        if (obj != null)
        {
          if (obj != DBNull.Value)
            dbManager.DataProvider.TrySetIndexTablespaceName(Convert.ToString(obj));
        }
      }
    }
    ServerConsts.ShortenedConnectionString = this.GetShortenedConnectionString(this._dbManagerService.ConnectionString);
    this.AddToServerServices(typeof (IDbManagerService), (object) this._dbManagerService);
  }

  private void InitializeEventLogHelper()
  {
    using (IDbManager dbManager = this._dbManagerService.CreateDbManager())
    {
      this._eventLogHelper = new EventLogHelper();
      this._eventLogHelper.LoadSettings(dbManager);
      this.AddToServerServices(typeof (IEventLogHelper), (object) this._eventLogHelper);
      this.AddToServerServices(typeof (IServerEventLogService), (object) this._eventLogHelper);
      DbManagerConfiguration.EventLogHelper = (IEventLogHelper) this._eventLogHelper;
    }
  }

  private void InitializeKernelUpdateHelper()
  {
    this._kernelUpdateHelper = new KernelUpdate((IEventLogHelper) this._eventLogHelper);
    this.AddToServerServices(typeof (IDBVersionUpdater), (object) this._kernelUpdateHelper);
  }

  private void ShowDbConnectionString()
  {
    ConsoleColor foregroundColor = Console.ForegroundColor;
    try
    {
      Console.WriteLine(string.Empty);
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.Write("Строка подключения:");
      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine($"{this._dbManagerService.ConnectionName}={ServerConsts.ShortenedConnectionString}");
      Console.WriteLine(string.Empty);
    }
    finally
    {
      Console.ForegroundColor = foregroundColor;
    }
  }

  private void ShowValidateRDBMSMessage(string validateMessage)
  {
    ConsoleColor foregroundColor = Console.ForegroundColor;
    try
    {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine(validateMessage);
      Console.WriteLine(string.Empty);
    }
    finally
    {
      Console.ForegroundColor = foregroundColor;
    }
  }

  private void ClearPatchFlag(IDbManager db)
  {
    db.ExecuteNonQuery("DELETE FROM IMS_CONFIGS WHERE F_MODULE_NAME = 'KERNEL' AND F_SECTION_ID = 'DB_PATCH'");
  }

  private void LoadMeasuresList(IUserSession session)
  {
    try
    {
      DBMeasureObject.LoadMeasuresList(session);
    }
    catch (Exception ex)
    {
      this._eventLogHelper.AddToTrace(LocalizationHolder.rm.GetString("Server_110") + ex.Message, Intermech.Consts.traceAlways, string.Empty);
    }
  }

  private void InitializeKernelRootServices(IDbManager db)
  {
    this.AddToServerServices(typeof (IUserSessionCollection), (object) UserSession.Sessions);
    this.AddToServerServices(typeof (IUserSessionClientConnections), (object) UserSession.ClientConnections);
    ServiceLocator.Initialize((IServiceProvider) ServerServices.ServiceContainer);
    Intermech.Search.Module.Initialize();
    this.InitCodeSecurity(db);
    SessionKeeper.InitializeAllocator((IUserSessionAllocator) new UserSessionAllocator());
    MetadataResolvers.ChangeMonitor = this._initParams.MetadataChangeMonitor;
    MetadataResolvers.Factory = this._initParams.MetadataResolversFactory;
    this.AddToServerServices(typeof (ICaptionsHelper), (object) new CaptionsHelper());
    this._cacheDatasetService = new CacheDataset(db);
    this.AddToServerServices(typeof (ICacheDataset), (object) this._cacheDatasetService);
    this.AddToServerServices(typeof (IObjectsInfoCache), (object) this._cacheDatasetService);
    MetaDataHelper.SyncMetadata(this._cacheDatasetService._DBSet, true);
    MetaDataHelperUpdateService.RegisterService();
    this.AddToServerServices(typeof (IIDHelper), (object) new IDHelper(db));
    this.AddToServerServices(typeof (IDBConfigurationService), (object) new DBConfigurationService(db));
    this.AddToServerServices(typeof (IDBAttributeTypeService), (object) new DBAttributeTypeService());
    this.AddToServerServices(typeof (IDBAttributeService), (object) new DBAttributeService());
    this.AddToServerServices(typeof (IUsersGroupsListCache), (object) new UsersGroupsListCache());
    this._kernelRootServicesModules = this._initParams.SharedLibraryInitializerService.InitializerModuleFactory.Create<LazyInitializerModuleGroup>();
    this._kernelRootServicesModules.Add<PairedObjectsCreatorModule>();
    this._kernelRootServicesModules.Initialize();
    DBObjectService service = new DBObjectService();
    IDBObjectCreator creatorInstance = (IDBObjectCreator) new DBKernelObjectCreator();
    service.AddCreator((object) new Guid("cad0000b-306c-11d8-b4e9-00304f19f545"), (object) creatorInstance);
    service.AddCreator((object) new Guid("cad00002-306c-11d8-b4e9-00304f19f545"), (object) creatorInstance);
    service.AddCreator((object) new Guid("cad00007-306c-11d8-b4e9-00304f19f545"), (object) creatorInstance);
    service.AddCreator((object) new Guid("cad00014-306c-11d8-b4e9-00304f19f545"), (object) creatorInstance);
    service.AddCreator((object) new Guid("cad0004a-306c-11d8-b4e9-00304f19f545"), (object) creatorInstance);
    service.AddCreator((object) new Guid("cad001b3-306c-11d8-b4e9-00304f19f545"), (object) creatorInstance);
    service.AddCreator((object) new Guid("cad00342-306c-11d8-b4e9-00304f19f545"), (object) creatorInstance);
    service.AddCreator((object) new Guid("cad00812-306c-11d8-b4e9-00304f19f545"), (object) creatorInstance);
    service.AddCreator((object) new Guid("cad00003-306c-11d8-b4e9-00304f19f545"), (object) creatorInstance);
    service.AddCreator((object) new Guid("cad00822-306c-11d8-b4e9-00304f19f545"), (object) creatorInstance);
    service.AddCreator((object) new Guid("cad0088f-306c-11d8-b4e9-00304f19f545"), (object) creatorInstance);
    service.AddCreator((object) new Guid("cad0088e-306c-11d8-b4e9-00304f19f545"), (object) creatorInstance);
    service.AddCreator((object) new Guid("cad00156-306c-11d8-b4e9-00304f19f545"), (object) creatorInstance);
    service.AddCreator((object) new Guid("cad0013b-306c-11d8-b4e9-00304f19f545"), (object) creatorInstance);
    service.AddCreator((object) PortalConsts.objtypeSites, (object) creatorInstance);
    this.AddToServerServices(typeof (IDBObjectService), (object) service);
    this.AddToServerServices(typeof (IDBObjectCollectionService), (object) new DBObjectCollectionService());
    this._blobStoragesPoolService = new BlobStoragesPool();
    this.AddToServerServices(typeof (IBlobStoragesPool), (object) this._blobStoragesPoolService);
    this.AddToCustomServices(typeof (IBlobStoragesService), (object) this._blobStoragesPoolService);
    this.AddToServerServices(typeof (IDBRelationCollectionService), (object) new DBRelationCollectionService());
    this.AddToServerServices(typeof (IDBRelationService), (object) new DBRelationService());
    this.AddToServerServices(typeof (IStringNormalizer), (object) new StringNormalizerService());
    this.AddToServerAndCustomServices(typeof (IFileNamesService), (object) new FileNamesService());
    ContainerService containerService = new ContainerService();
    this.AddToServerAndCustomServices(typeof (IContainerService), (object) containerService);
    this.AddToServerAndCustomServices(typeof (IDatabaseLocker), (object) new DatabaseLocker());
    DocumentTypeSettingsService serviceInstance1 = new DocumentTypeSettingsService();
    this.AddToServerAndCustomServices(typeof (IDocumentTypeSettingsService), (object) serviceInstance1);
    this.AddToServerAndCustomServices(typeof (IIDLinkTranslate), (object) new IDLinkTranslateService());
    this._adminUtilsService = new AdminUtilsService();
    this.AddToServerAndCustomServices(typeof (IAdminUtilsService), (object) this._adminUtilsService);
    this._selectionsService = (ISelectionsService) new SelectionSrvService();
    this.AddToServerAndCustomServices(typeof (ISelectionsService), (object) this._selectionsService);
    CompositionLoadService serviceInstance2 = new CompositionLoadService();
    this.AddToServerAndCustomServices(typeof (ICompositionLoadService), (object) serviceInstance2);
    this.AddToCustomServices(typeof (ITypedInfoService), (object) serviceInstance2);
    this.AddToServerAndCustomServices(typeof (IVersionRulesCacheService), (object) new VersionRulesCacheService());
    IDBEditingContextsServerService contextsServerService = (IDBEditingContextsServerService) new DBEditingContextsService();
    this.AddToServerServices(typeof (IDBEditingContextsServerService), (object) contextsServerService);
    this.AddToServerAndCustomServices(typeof (IDBEditingContextsService), (object) contextsServerService);
    IElementStatusesService serviceInstance3 = (IElementStatusesService) new ElementStatusesService();
    this.AddToServerAndCustomServices(typeof (IElementStatusesService), (object) serviceInstance3);
    IPluginStatusesTable pluginStatusesTable = (IPluginStatusesTable) new PluginStatusesTable();
    this.AddToServerAndCustomServices(typeof (IPluginStatusesTable), (object) pluginStatusesTable);
    this._dbTimedEventsService = new DBTimedEvents((IDbManagerService) this._dbManagerService, (IEventLogHelper) this._eventLogHelper, ServiceUtils.GetService<IApplicationStateEventsService>((object) ApplicationServices.Container, true));
    this.AddToServerServices(typeof (IDBTimedEvents), (object) this._dbTimedEventsService);
    this.AddToCustomServices(typeof (ITimedEventsSheduler), (object) this._dbTimedEventsService);
    if (this._initParams.RebuildViewsMode)
    {
      this._adminUtilsService.RebuildAllViews();
    }
    else
    {
      this.AddToServerServices(typeof (ICustomImport), (object) new CustomImportService());
      ObligatoryObjectsService obligatoryObjectsService = new ObligatoryObjectsService();
      this.AddToServerServices(typeof (IObligatoryObjectsService), (object) obligatoryObjectsService);
      this._metadataUpdateService = new UpdateService((IEventLogHelper) this._eventLogHelper, (IObligatoryObjectsRegistryService) obligatoryObjectsService);
      this.AddToServerServices(typeof (IUpdateService), (object) this._metadataUpdateService);
      this.AddToCustomServices(typeof (IUpdateLogService), (object) this._metadataUpdateService);
      try
      {
        this._globalIndexService = new GlobalIndexService((IDbManagerService) this._dbManagerService, (IDBTimedEvents) this._dbTimedEventsService);
        this.AddToServerServices(typeof (IGlobalIndexService), (object) this._globalIndexService);
        this.AddToCustomServices(typeof (IGlobalIndexSettings), (object) this._globalIndexService);
        this.AddToCustomServices(typeof (IGlobalIndexHelper), (object) this._globalIndexService);
      }
      catch (Exception ex)
      {
        this.ShowServiceInitializationException(typeof (GlobalIndexService), ex);
      }
      this.AddToServerServices(typeof (IDelayedUpdaterService), (object) new DelayedUpdaterService());
      this.AddToServerServices(typeof (IImportingEntityCustomCheckService), (object) new ImportingEntityCustomCheckService());
      this.AddToServerServices(typeof (IFiltrationTableService), (object) new FiltrationTableService());
      DictionaryServiceHolder.RegisterService();
      IUserSession sessionTemporaryClone = this._dbTimedEventsService.GetSystemSessionTemporaryClone(nameof (InitializeKernelRootServices));
      try
      {
        this._blobStoragesPoolService.ValidateStorages(sessionTemporaryClone);
        this._cacheDatasetService.InitPossibleValuesCache(sessionTemporaryClone);
        KernelRoot.InitAdminList(sessionTemporaryClone);
        this.LoadMeasuresList(sessionTemporaryClone);
        try
        {
          containerService.InitCache(sessionTemporaryClone);
          serviceInstance1.InitCache(sessionTemporaryClone, containerService);
        }
        catch (Exception ex)
        {
          this._eventLogHelper.AddToTrace("Ошибка инициализации кэшей контейнеров атрибутов и настроек типов документов: " + ex.Message, Intermech.Consts.traceAlways, string.Empty);
          this._eventLogHelper.AddToTrace(ex.StackTrace, Intermech.Consts.traceAlways, string.Empty);
        }
        try
        {
          byte AnElementStatesBits;
          try
          {
            List<IMSLifeCycleLevel> lcLevelsList = MetaDataHelper.GetLCLevelsList();
            if (lcLevelsList.Count == 0)
            {
              if (sessionTemporaryClone is IUserSessionCacheDataSet sessionCacheDataSet)
                MetaDataHelper.SyncMetadata(sessionCacheDataSet.CacheDataSet, true);
              lcLevelsList = MetaDataHelper.GetLCLevelsList();
            }
            AnElementStatesBits = Convert.ToByte(Math.Log((double) (lcLevelsList.Count * 2), 2.0));
          }
          catch
          {
            AnElementStatesBits = (byte) 8;
          }
          this._objectLevelStatuses = new ElementStatusesPluginDescription((int) AnElementStatesBits, "{7074E0E4-B3AB-4B3E-AD56-050CD256AF10}", "{76FCDEFA-59AF-4468-8BA6-AEF9ACB20795}", LocalizationHolder.rm.GetString("Server_104"), LocalizationHolder.rm.GetString("Server_105"));
          serviceInstance3.RegisterServerPlugin(this._objectLevelStatuses);
          serviceInstance3.RegisterServerPlugin(this._versionsSelectionPlugin);
          ElementStatusesPluginDescription serverPlugin = new ElementStatusesPluginDescription(32 /*0x20*/, ObjectsVisibilityConstants.ObjectsVisiblityModuleGuid, (string) null, "Видимость объектов", "Статусы видимости объектов");
          serviceInstance3.RegisterServerPlugin(serverPlugin);
          VersionSelectionStatuses.AddVersionSelectionStatuses(sessionTemporaryClone, pluginStatusesTable);
        }
        catch (Exception ex)
        {
          this._eventLogHelper.AddToTrace(LocalizationHolder.rm.GetString("Server_106") + ex.Message, Intermech.Consts.traceAlways, string.Empty);
          this._eventLogHelper.AddToTrace(ex.StackTrace, Intermech.Consts.traceAlways, string.Empty);
        }
        try
        {
          AttachedSelectionsService selectionsService = new AttachedSelectionsService(sessionTemporaryClone);
          this.AddToServerServices(typeof (IAttachedSelectionsServerService), (object) selectionsService);
          this.AddToCustomServices(typeof (IAttachedSelectionsService), (object) selectionsService);
        }
        catch (Exception ex)
        {
          this.ShowServiceInitializationException(typeof (AttachedSelectionsService), ex);
        }
        try
        {
          this.AddToServerAndCustomServices(typeof (IRedliningService), (object) new RedliningService(sessionTemporaryClone));
        }
        catch (Exception ex)
        {
          this.ShowServiceInitializationException(typeof (RedliningService), ex);
        }
      }
      finally
      {
        sessionTemporaryClone.Logout(nameof (InitializeKernelRootServices));
      }
    }
  }

  private void InitializeNormalServices()
  {
    Initializer.InitializeAll();
    SimplePtpService.Install();
    try
    {
      this.AddToCustomServices(typeof (IEmailService), (object) new EmailService());
    }
    catch (Exception ex)
    {
      this.ShowServiceInitializationException(typeof (EmailService), ex);
    }
    this.AddToServerAndCustomServices(typeof (ILCScriptService), (object) new LCStepScriptService((IDBTimedEvents) this._dbTimedEventsService));
    if (GuidService.IsServiceEnabled())
    {
      if (AdminUtilsService.ServerRunMode == ServerRunModes.Console)
        Console.Write("Загрузка плагина Guids...");
      this.AddToCustomServices(typeof (IGuidService), (object) new GuidService((IDbManagerService) this._dbManagerService, (ICacheDataset) this._cacheDatasetService));
      if (AdminUtilsService.ServerRunMode == ServerRunModes.Console)
        Console.WriteLine("ОК");
    }
    ImportUsersProfileHolder.RegisterService();
    CompositionViewServer.RegisterService();
    DataVaultServiceHolder.RegisterService();
    try
    {
      this.AddToCustomServices(typeof (IFixAttributeService), (object) new FixAttributeService());
    }
    catch (Exception ex)
    {
      this.ShowServiceInitializationException(typeof (FixAttributeService), ex);
    }
    this.AddToCustomServices(typeof (IFileComparisonService), (object) new FileComparisonService());
    this.AddToCustomServices(typeof (IUserSubstituteService), (object) new UserSubstituteService());
    this.AddToCustomServices(typeof (IUserFavouritesService), (object) new UserFavouritesService());
    this.AddToCustomServices(typeof (ILifecycleService), (object) new LifecycleService());
    ScheduledScriptService.RegisterService();
    this.AddToServerServices(typeof (IBrowseFolder), (object) new BrowseFolderService());
    object obj = (object) new ServerBriefcase();
    this.AddToServerAndCustomServices(typeof (IServerBriefcase), obj);
    this.AddToServerServices(typeof (ICategoryExportManager), obj);
    this.AddToServerServices(typeof (IBriefcaseProcesses), (object) new BriefcaseProcesses());
    ISpecHandleAttributes service = (ISpecHandleAttributes) new SpecHandleAttributesService();
    this.AddToServerServices(typeof (ISpecHandleAttributes), (object) service);
    service.RegisterNotUpdatingAttribute(new Guid("cad0014d-306c-11d8-b4e9-00304f19f545"));
    service.RegisterNotUpdatingAttribute(new Guid("cad00019-306c-11d8-b4e9-00304f19f545"));
    service.RegisterNotUpdatingAttribute(new Guid("cad014ff-306c-11d8-b4e9-00304f19f545"));
    SelectionExport.Init();
    this.AddToServerAndCustomServices(typeof (IChecksumsService), (object) new ChecksumsService());
    this.AddToServerAndCustomServices(typeof (IRelationAttributesPackageWriter), (object) new RelationAttributesPackageWriter());
    this.AddToServerAndCustomServices(typeof (ICompositionsAutomaticSortingService), (object) new CompositionsAutomaticSortingService());
    this.AddToServerAndCustomServices(typeof (IBlobImporter), (object) new BlobImporter(this._blobStoragesPoolService));
    this.AddToServerAndCustomServices(typeof (IObjectsCheckOutServerService), (object) new ObjectsCheckOutServerService());
    IObjectsDeleteAnalyzerService serviceInstance1 = (IObjectsDeleteAnalyzerService) new ObjectsDeleteAnalyzerService();
    this.AddToServerAndCustomServices(typeof (IObjectsDeleteAnalyzerService), (object) serviceInstance1);
    serviceInstance1.RegisterAnalyzer((IObjectsDeleteAnalyzer) new KernelObjectsDeleteAnalyzer());
    this.AddToServerAndCustomServices(typeof (IObjectsChangingAnalyzerService), (object) new ObjectsChangingAnalyzerService());
    ISearchGroupingObjectsService serviceInstance2 = (ISearchGroupingObjectsService) new SearchGroupingObjectAnalyzerService();
    this.AddToServerAndCustomServices(typeof (ISearchGroupingObjectsService), (object) serviceInstance2);
    EditingContextsForObjectsAnalyzer forObjectsAnalyzer = new EditingContextsForObjectsAnalyzer();
    EditingContextsForObjectsWithCompositionsAnalyzer compositionsAnalyzer = new EditingContextsForObjectsWithCompositionsAnalyzer(forObjectsAnalyzer);
    EditingContextsForObjectsWithComplexCompositionsAnalyzer analyzer1 = new EditingContextsForObjectsWithComplexCompositionsAnalyzer(compositionsAnalyzer);
    EditingContextsForAllObjectVersionsAnalyzer versionsAnalyzer = new EditingContextsForAllObjectVersionsAnalyzer(forObjectsAnalyzer);
    EditingContextsForAllObjectVersionsWithCompositionsAnalyzer analyzer2 = new EditingContextsForAllObjectVersionsWithCompositionsAnalyzer(versionsAnalyzer, compositionsAnalyzer);
    serviceInstance2.RegisterAnalyzer((ISearchGroupingObjectAnalyzer) forObjectsAnalyzer);
    serviceInstance2.RegisterAnalyzer((ISearchGroupingObjectAnalyzer) compositionsAnalyzer);
    serviceInstance2.RegisterAnalyzer((ISearchGroupingObjectAnalyzer) analyzer1);
    serviceInstance2.RegisterAnalyzer((ISearchGroupingObjectAnalyzer) versionsAnalyzer);
    serviceInstance2.RegisterAnalyzer((ISearchGroupingObjectAnalyzer) analyzer2);
    this.AddToServerAndCustomServices(typeof (IObjectsDeleteService), (object) new ObjectsDeleteService());
    this.AddToCustomServices(typeof (IObjectRepositoryServerHandler), (object) new ObjectRepositoryServerHandler());
    this.AddToCustomServices(typeof (IRelationRepositoryServerService), (object) new RelationRepositoryServerService());
    this.AddToCustomServices(typeof (ICompositionRepositoryServerService), (object) new CompositionRepositoryServerService());
    this.AddToCustomServices(typeof (IEditingContextServerService), (object) new EditingContextServerService());
    this.AddToCustomServices(typeof (IDefaultCommandsSettingsServerService), (object) new DefaultCommandsSettingsServerService());
    this.AddToCustomServices(typeof (IButtonBarServerService), (object) new ButtonBarServerService());
    new GroupAttributesChangingServerModule(this._customServices).Load();
    new CompositionByObjectTypesFiltersServerModule(this._customServices).Load();
    new ObjectListFiltersServerModule(this._customServices).Load();
    new ContextMenuServerModule().Load();
    new RecentObjectsServerModule().Load();
    new EventLogFiltersServerModule().Load();
    new AttributeChangeHistoryServerModule().Load();
    new ConcretizationServerModule().Load();
    new AutoConcretizationServerModule(this._customServices, ServiceLocator.Get<IEventLogHelper>(), (IConcretizationServerService) this._customServices.GetService(typeof (IConcretizationServerService))).Load();
    if (ServerServices.GetService(typeof (IMServer)) == null)
      ServerServices.AddService(typeof (IMServer), (object) this);
    new PasswordChangeServerModule((IServiceProvider) ServerServices.ServiceContainer).Load();
    new DiscussionsServerModule((IServiceProvider) ServerServices.ServiceContainer).Load();
    this.AddToServerAndCustomServices(typeof (IRecentObjectsSharingService), (object) new RecentObjectsSharingService());
    this.AddToServerServices(typeof (IDBSecurityService), (object) new DBSecurityService());
    TemporaryAccessService temporaryAccessService = new TemporaryAccessService();
    this.AddToServerServices(typeof (ITemporaryAccessService), (object) temporaryAccessService);
    this.AddToCustomServices(typeof (IInternalUserSessions), (object) temporaryAccessService);
    IUserSession sessionTemporaryClone = this._dbTimedEventsService.GetSystemSessionTemporaryClone(nameof (InitializeNormalServices));
    try
    {
      this.AddToServerAndCustomServices(typeof (ISnapshotService), (object) new SnapshotService(sessionTemporaryClone));
      try
      {
        SiteServerService serviceInstance3 = new SiteServerService();
        this.AddToCustomServices(typeof (ISiteServerService), (object) serviceInstance3);
        ConnectionSettings settings = serviceInstance3.Settings;
        SitesCacheService serviceInstance4 = new SitesCacheService((IEventLogHelper) this._eventLogHelper);
        serviceInstance4.Reload((object) sessionTemporaryClone);
        this.AddToCustomServices(typeof (ISitesCacheService), (object) serviceInstance4);
        serviceInstance3.InitServices((object) sessionTemporaryClone, settings);
      }
      catch (Exception ex)
      {
        this.ShowServiceInitializationException(typeof (SiteServerService), ex);
      }
      try
      {
        this.AddToCustomServices(typeof (IServerInformationCollector), (object) new ServerInformationCollector());
      }
      catch (Exception ex)
      {
        this.ShowServiceInitializationException(typeof (ServerInformationCollector), ex);
      }
    }
    finally
    {
      sessionTemporaryClone.Logout(nameof (InitializeNormalServices));
    }
    this._normalServicesModules = (LazyInitializerModuleGroup) this._initParams.SharedLibraryInitializerService.InitializerModuleFactory.Create<AssemblyInitializerModule>();
    this._normalServicesModules.Initialize();
  }

  private void AddToServerServices(Type serviceType, object service)
  {
    ServerServices.AddService(serviceType, service);
  }

  private void AddToCustomServices(Type serviceType, object serviceInstance)
  {
    this._customServices.AddService(serviceType, serviceInstance);
  }

  private void AddToServerAndCustomServices(Type serviceType, object serviceInstance)
  {
    ServerServices.AddService(serviceType, serviceInstance);
    this._customServices.AddService(serviceType, serviceInstance);
  }

  private void ShowServiceInitializationException(Type serviceType, Exception exception)
  {
    if (this._eventLogHelper == null)
      return;
    this._eventLogHelper.AddToTrace($"{serviceType.Name} initialization error: {exception.Message}", Intermech.Consts.traceAlways, string.Empty);
    this._eventLogHelper.AddToTrace(exception.StackTrace, Intermech.Consts.traceAlways, string.Empty);
  }

  private void RunDatabaseStructurePatches(IDbManager db)
  {
    try
    {
      this._kernelUpdateHelper.PatchDatabase(db, (IEventLogHelper) this._eventLogHelper);
    }
    catch (Exception ex)
    {
      this._eventLogHelper.AddToTrace("Error while updating the database: " + ex.Message, Intermech.Consts.traceAlways, string.Empty);
      throw;
    }
  }

  private void RunMetadataUpdateScripts(IDbManager db)
  {
    this._metadataUpdateService.AddModule(Assembly.GetExecutingAssembly().FullName, (IUpdatable) this._kernelUpdateHelper);
    this._pluginManager.ScanAssembliesForAutoLoad(new Func<string, bool>(this.CollectIUpdatables));
    this._metadataUpdateService.WriteStartToLog();
    bool flag = false;
    try
    {
      string updateFolderPath = KernelUpdate.GetUpdateFolderPath((IConfigurationManager) this._configManager);
      this._kernelUpdateHelper.PatchStoredProcs(new string[1]
      {
        "import_object"
      }, db, (IEventLogHelper) this._eventLogHelper, 409);
      if (!this._metadataUpdateService.StartUpdate(updateFolderPath))
      {
        flag = true;
        throw new Exception(LocalizationHolder.rm.GetString("Server_114"));
      }
    }
    catch (Exception ex)
    {
      if (!flag)
      {
        this._eventLogHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Server_100"), (object) ex.Message), Intermech.Consts.traceAlways, string.Empty);
        if (ex.StackTrace != string.Empty)
          this._eventLogHelper.AddToTrace(ex.StackTrace, Intermech.Consts.traceAlways, string.Empty);
      }
      this.ShowAlertMessage(ex.Message, true);
    }
    finally
    {
      this._metadataUpdateService.WriteEndToLog();
    }
  }

  private bool CollectIUpdatables(string assemblyLocation)
  {
    try
    {
      Assembly assembly = Assembly.LoadFrom(this._pluginManager.AssemblyPathNormalizer.Normalize(assemblyLocation));
      foreach (Type type in assembly.GetTypes())
      {
        if (type.IsClass && !type.IsAbstract)
        {
          if (IntermechServer.updatableType.IsAssignableFrom(type))
          {
            try
            {
              this._metadataUpdateService.AddModule(assembly.FullName, (IUpdatable) Activator.CreateInstance(type));
            }
            catch (Exception ex)
            {
              this._eventLogHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Server_101"), (object) assemblyLocation, (object) ex.Message), Intermech.Consts.traceAlways, string.Empty);
              if (ex.StackTrace != string.Empty)
                this._eventLogHelper.AddToTrace(ex.StackTrace, Intermech.Consts.traceAlways, string.Empty);
            }
          }
        }
      }
    }
    catch (Exception ex)
    {
      this._eventLogHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Server_101"), (object) assemblyLocation, (object) ex.Message), Intermech.Consts.traceAlways, string.Empty);
      if (ex.StackTrace != string.Empty)
        this._eventLogHelper.AddToTrace(ex.StackTrace, Intermech.Consts.traceAlways, string.Empty);
    }
    return true;
  }

  private void RunMetadataPatches()
  {
    IUserSession sessionTemporaryClone = this._dbTimedEventsService.GetSystemSessionTemporaryClone(nameof (RunMetadataPatches));
    try
    {
      this._kernelUpdateHelper.PatchKernelMetadata((UserSession) sessionTemporaryClone);
    }
    finally
    {
      sessionTemporaryClone.Logout(nameof (RunMetadataPatches));
    }
  }

  private UserSessionLostInterceptor CreateUserSessionLostInterceptor()
  {
    return new UserSessionLostInterceptor()
    {
      LogAction = new Action<string>(this.LogUserSessionLostInterceptorEvent)
    };
  }

  private void LogUserSessionLostInterceptorEvent(string text)
  {
    if (string.IsNullOrEmpty(text) || this._eventLogHelper == null)
      return;
    this._eventLogHelper.AddToTrace(text, Intermech.Consts.traceAlways, "network_errors.log");
  }

  private string GetShortenedConnectionString(string connectionString)
  {
    HashSet<string> stringSet = new HashSet<string>((IEnumerable<string>) new string[4]
    {
      "Server",
      "Port",
      "Database",
      "Data Source"
    }, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    DbConnectionStringBuilder connectionStringBuilder = new DbConnectionStringBuilder();
    connectionStringBuilder.ConnectionString = connectionString;
    string[] strArray = new string[connectionStringBuilder.Keys.Count];
    connectionStringBuilder.Keys.CopyTo((Array) strArray, 0);
    foreach (string keyword in strArray)
    {
      if (!stringSet.Contains(keyword))
        connectionStringBuilder.Remove(keyword);
    }
    return connectionStringBuilder.ToString().ToUpper();
  }

  private void InitializePluginsAndExtensions()
  {
    if (this._initParams.SkipPlugins)
      return;
    this._pluginManager.LoadConfiguration();
  }

  private void PostInitialize()
  {
    this.ClearTemporaryStorage();
    this.InitializeSelectionsServiceClassifierCache();
    (ServerServices.GetService(typeof (ITraceLoggerService)) as ITraceLoggerService).CheckTruncateLogFiles();
    this.StartTimedEventsService();
    this.ScheduleClearCacheBlobsTask();
    this.ScheduleAdminUtilsTask();
    this.RegisterGlobalIndexPlugins();
    if (!(ServerServices.GetService(typeof (IProtectionKey)) is IProtectionKey service))
      return;
    service.PostLoad();
  }

  private void StartTimedEventsService() => this._dbTimedEventsService.Start();

  private void InitializeSelectionsServiceClassifierCache()
  {
    try
    {
      this._selectionsService.LoadClassifierToObjTypeCache();
    }
    catch (Exception ex)
    {
      this._eventLogHelper.AddToTrace(LocalizationHolder.rm.GetString("Server_95") + ex.Message, Intermech.Consts.traceAlways, string.Empty);
    }
  }

  private void ClearTemporaryStorage() => new TemporaryStorage().Clear();

  private void ScheduleClearCacheBlobsTask()
  {
    this._blobStoragesPoolService.RegisterClearCacheBlobs();
  }

  private void ScheduleAdminUtilsTask() => this._adminUtilsService.PrepareScheduledTasks();

  private void RegisterGlobalIndexPlugins()
  {
    this._globalIndexService.RegisterFileConverter((IIndexerFileConverter) new TextFileConverter());
    TextMiningFileConverter converter = new TextMiningFileConverter();
    if (File.Exists(converter.MinetextFilename))
      this._globalIndexService.RegisterFileConverter((IIndexerFileConverter) converter);
    this._globalIndexService.RegisterFileConverter((IIndexerFileConverter) new DwgDxfFileConverter());
  }

  private void InstallRemoteExceptionDataProvider()
  {
    this._remoteExceptionDataProvider = new RemoteExceptionDataProvider();
    this._remoteExceptionDataProvider.CanSaveExceptionData += new Func<Exception, bool>(this.IsRemotingClientCallException);
    this._remoteExceptionDataProvider.Enabled = true;
  }

  private bool IsRemotingClientCallException(Exception exception)
  {
    return !IPSPrincipal.CurrentPrincipal.IsInRole(IPSBuiltInRole.Server);
  }

  private void ShowAlertMessage(string message, bool isError)
  {
    if (string.IsNullOrEmpty(message))
      return;
    IAlertMessageService service = ServiceUtils.GetService<IAlertMessageService>((object) ServerServices.ServiceContainer, false);
    if (service == null)
      return;
    string caption = isError ? "Ошибка" : "Сообщение";
    AlertMessageType messageType = isError ? AlertMessageType.Error : AlertMessageType.Information;
    service.ShowMessage(caption, message, messageType);
  }

  private void InitCodeSecurity(IDbManager db)
  {
    RBSServer.UpdateSecurityContext(Convert.ToInt64(db.ExecuteScalar($"select {"F_OBJECT_ID"} from {"IMS_GUID"} where {"F_GUID"} = :guidPar", db.Parameter("guidPar", (object) "cad0000d-306c-11d8-b4e9-00304f19f545"))));
  }

  private void LoadConfiguration()
  {
    this._configFileName = Path.ChangeExtension(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".vshost", string.Empty), ".cfg");
    if (!File.Exists(this._configFileName))
      return;
    using (Stream stream = (Stream) File.OpenRead(this._configFileName))
      this._configManager.Load(stream);
  }

  public void CloseServer()
  {
    try
    {
      if (!(ServerServices.GetService(typeof (IAppServerFilesCache)) is IAppServerFilesCache service) || !(System.Configuration.ConfigurationManager.AppSettings.Get("DisableClearFilesCache") != "1"))
        return;
      service.ClearServerCache();
    }
    catch (Exception ex)
    {
      if (!(ServerServices.GetService(typeof (IEventLogHelper)) is IEventLogHelper service))
        return;
      service.AddToTrace("Ошибка закрытия сервера приложений: " + ex.Message);
      service.AddToTrace(ex.StackTrace);
    }
  }

  public Version Version => this._assemblyVersion;

  public IMServerLoginMode LoginMode
  {
    get
    {
      this.WaitInitialized();
      return UserSession.LoginMode;
    }
  }

  public IUserSession CreateSession()
  {
    this.WaitInitialized();
    return (IUserSession) new UserSession();
  }

  public IMServerAppConfiguration AppConfiguration
  {
    get => (IMServerAppConfiguration) this._appConfigService;
  }

  public ILeaseRenewalService LeaseRenewalService
  {
    get => (ILeaseRenewalService) this._leaseRenewalService;
  }

  public IMServerLiveStatus LiveStatus => (IMServerLiveStatus) this._liveStatusService;

  private void WaitInitialized()
  {
    if (this._initialized)
      return;
    int num = 0;
    while (!this._initialized)
    {
      Thread.Sleep(1000);
      if (++num >= 120)
        throw new Exception(string.Join(" ", LocalizationHolder.rm.GetString("Server_75"), LocalizationHolder.rm.GetString("Server_74")));
    }
  }

  public byte[] UsersBanner
  {
    get
    {
      this.WaitInitialized();
      return this._userBanner.Value;
    }
  }

  private byte[] ReadUserBannerFromFile()
  {
    try
    {
      string path = Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), "Banner.img");
      if (File.Exists(path))
        return File.ReadAllBytes(path);
    }
    catch
    {
    }
    return (byte[]) null;
  }

  public void AddToTrace(
    string text,
    int traceLevel,
    string traceFileName = null,
    string computerName = null,
    string userName = null)
  {
    this.WaitInitialized();
    this._eventLogHelper.AddToTrace(text, traceLevel, traceFileName, computerName, userName);
  }

  public void AddToTrace(ICollection<AddToTraceRecord> eventRecords)
  {
    if (eventRecords == null)
      throw new ArgumentNullException(nameof (eventRecords));
    this.WaitInitialized();
    foreach (AddToTraceRecord eventRecord in (IEnumerable<AddToTraceRecord>) eventRecords)
      this._eventLogHelper.AddToTrace(eventRecord.Text, eventRecord.TraceLevel, eventRecord.TraceFileName, eventRecord.ComputerName, eventRecord.UserName);
  }

  public char CryptMethod => ServerConsts.CryptMethod;

  public object GetCustomService(Type serviceType)
  {
    if (serviceType == (Type) null)
      throw new ArgumentNullException(nameof (serviceType));
    if (serviceType == typeof (IDBTransactions))
      throw new KernelException("Данная служба может быть получена только через IUserSession");
    this.WaitInitialized();
    return this._customServices.GetService(serviceType);
  }
}
