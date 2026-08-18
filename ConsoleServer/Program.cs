using Intermech;
using Intermech.ApplicationModel;
using Intermech.ApplicationModel.NinjectIntegration;
using Intermech.Collections;
using Intermech.Configuration;
using Intermech.Diagnostics;
using Intermech.Globalization;
using Intermech.Interfaces;
using Intermech.Interfaces.Data.Metadata;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.GlobalIndex;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services;
using Intermech.Localization;
using Intermech.Protection;
using Intermech.Security;
using Intermech.Server.Data;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading;


namespace ConsoleServer;

internal sealed class Program : ConsoleApplicationBase
{
  private Program.SavedLoginInfo savedAdminLoginInfo;
  private IConsoleService consoleService;
  private StandardKernel iocContainer;
  private ApplicationStateService applicationStateService;
  private EventLogWriterSyncWrapper fileEventLogWriter;
  private EventLogWriterSyncWrapper systemEventLogWriter;
  private ApplicationEventLogService eventLogService;
  private AlertMessageService alertService;
  private ExceptionDisplayService exceptionService;
  private IProtectionKey protectionKey;
  private IConsoleCommandRegistry commandsRegistryService;
  private IPSFatalExceptionLogger fatalExceptionHandler;
  private CustomServices customServices;
  private RemotingInfoService remotingInfoService;
  private DbManagerLogger dbManagerLogger;
  private IntermechServer imserver;
  private string imserverUri;
  private OutputViewService outputViewService;

  [STAThread]
  private static void Main(string[] args) => new Program(args).Run();

  public Program(string[] arguments)
    : base(arguments)
  {
    this.savedAdminLoginInfo = new Program.SavedLoginInfo();
    this.consoleService = (IConsoleService) new ConsoleService();
  }

  protected override void DoRun()
  {
    base.DoRun();
    this.CreateIOCContainer();
    this.RegisterConsoleService();
    this.CreateCustomServicesContainer();
    this.CreateEventLogService();
    this.LogApplicationRunning();
    this.CreateApplicationStateEventsService();
    this.CreateOutputViewService();
    this.CreateAlertMessageService();
    this.CreateExceptionService();
    UICultureHelper.ApplySettingsFromConfigurationFile();
    this.DisplayBanner();
    this.InitializeExceptionHandlers();
    this.InitializeRoleBasedSecurity();
    this.InitializeApplicationMode();
    if (!this.TryStartRemoting() || !this.TryCreateProtectionKey())
      return;
    this.CreateRemotingInfoService();
    this.CreateDBManagerLogger();
    this.CreateIMServer();
    this.CreateConsoleCommandsRegistry();
    if (!this.TryStartIMServer())
      return;
    this.RunCommandLoop();
  }

  protected override void DoCleanup(bool errorMode)
  {
    if (this.applicationStateService != null)
      this.applicationStateService.RaiseExit();
    this.RemoveIMServer();
    this.RemoveConsoleCommandsRegistry();
    this.RemoveDBManagerLogger();
    this.RemoveRemotingInfoService();
    this.InvokeSilently((Action) (() => this.RemoveProtectionKey(false)), "RemoveProtectionKey(false)");
    this.RemoveExceptionHandlers();
    this.RemoveExceptionService();
    this.RemoveAlertMessageService();
    this.RemoveOutputViewService();
    this.RemoveApplicationStateEventsService();
    this.LogApplicationStopped();
    this.RemoveEventLogService();
    this.RemoveCustomServicesContainer();
    this.RemoveConsoleService();
    this.RemoveIOCContainer();
    base.DoCleanup(errorMode);
  }

  protected override void DoEmergencyExit()
  {
    base.DoEmergencyExit();
    if (this.applicationStateService != null)
      this.applicationStateService.RaiseEmergencyExit();
    this.InvokeSilently((Action) (() => this.RemoveProtectionKey(true)), "RemoveProtectionKey(true)");
  }

  protected override void DoReportUnexpectedExit()
  {
    base.DoReportUnexpectedExit();
    this.consoleService.WriteLine(string.Empty);
    this.consoleService.WriteLine("Нажмите enter для завершения работы приложения.");
    this.consoleService.ReadLine();
  }

  protected override IExceptionDisplayService TryGetExceptionDisplayService()
  {
    return (IExceptionDisplayService) this.exceptionService;
  }

  private void CreateIOCContainer()
  {
    this.iocContainer = new StandardKernel(Array.Empty<INinjectModule>());
    this.iocContainer.Load((INinjectModule) new MainApplicationNinjectModule());
    ApplicationServices.Container.ServiceResolver = this.iocContainer.Get<IApplicationServiceResolver>();
    this.iocContainer.Load((INinjectModule) new AssemblyNinjectModule());
    this.iocContainer.Bind<StackTraceBuilder>().To<IPSStackTraceBuilder>();
  }

  private void RemoveIOCContainer()
  {
    if (this.iocContainer == null)
      return;
    this.iocContainer.Dispose();
    this.iocContainer = (StandardKernel) null;
  }

  private void CreateApplicationStateEventsService()
  {
    this.applicationStateService = new ApplicationStateService();
    ApplicationServices.Container.AddService(typeof (IApplicationStateEventsService), (object) this.applicationStateService);
  }

  private void RemoveApplicationStateEventsService()
  {
    if (this.applicationStateService == null)
      return;
    ApplicationServices.Container.RemoveService(typeof (IApplicationStateEventsService));
    this.applicationStateService = (ApplicationStateService) null;
  }

  private void RegisterConsoleService()
  {
    ServerServices.AddService(typeof (IConsoleService), (object) this.consoleService);
  }

  private void RemoveConsoleService() => ServerServices.RemoveService(typeof (IConsoleService));

  private void CreateEventLogService()
  {
    try
    {
      this.fileEventLogWriter = EventLogWriters.Synchronized((IEventLogWriter) ApplicationEventLogWriters.CreateTextFileWriter("consoleserver.log"));
    }
    catch (Exception ex)
    {
      this.consoleService.WriteLine("Ошибка записи в файл consoleserver.log : " + ex.Message, ConsoleColor.Red);
      this.consoleService.WriteLine("Проверьте значение параметра LogPath в файле конфигурации сервера приложений " + Path.Combine(Directory.GetCurrentDirectory(), "ConsoleServer.exe.config"), ConsoleColor.Red);
      throw;
    }
    this.systemEventLogWriter = EventLogWriters.Synchronized(EventLogWriters.CreateSystemLogWriter(SystemEventLogType.Application, ServerDiagnosticsConsts.EventLogSourceName));
    this.eventLogService = new ApplicationEventLogService((IEventLogWriter) this.fileEventLogWriter, (IEventLogWriter) this.systemEventLogWriter, (IEventLogWriter) this.fileEventLogWriter);
    ServerServices.AddService(typeof (IApplicationEventLogService), (object) this.eventLogService);
  }

  private void RemoveEventLogService()
  {
    if (this.eventLogService != null)
    {
      ServerServices.RemoveService(typeof (IApplicationEventLogService));
      this.eventLogService = (ApplicationEventLogService) null;
    }
    if (this.systemEventLogWriter != null)
      this.systemEventLogWriter = (EventLogWriterSyncWrapper) null;
    if (this.fileEventLogWriter == null)
      return;
    ((TextFileEventLogWriter) this.fileEventLogWriter.Unwrap()).Dispose();
    this.fileEventLogWriter = (EventLogWriterSyncWrapper) null;
  }

  private void CreateAlertMessageService()
  {
    this.alertService = new AlertMessageService((IApplicationEventLogService) this.eventLogService, this.consoleService);
    ServerServices.AddService(typeof (IAlertMessageService), (object) this.alertService);
  }

  private void RemoveAlertMessageService()
  {
    if (this.alertService == null)
      return;
    ServerServices.RemoveService(typeof (IAlertMessageService));
    this.alertService = (AlertMessageService) null;
  }

  private void CreateExceptionService()
  {
    this.exceptionService = new ExceptionDisplayService((IAlertMessageService) this.alertService);
    ServerServices.AddService(typeof (IExceptionDisplayService), (object) this.exceptionService);
  }

  private void RemoveExceptionService()
  {
    if (this.exceptionService == null)
      return;
    ServerServices.RemoveService(typeof (IExceptionDisplayService));
    this.exceptionService = (ExceptionDisplayService) null;
  }

  private void InitializeRoleBasedSecurity() => RBSServer.InitializeSecurityContext();

  private void InitializeApplicationMode()
  {
    AdminUtilsService.ServerRunMode = ServerRunModes.Console;
  }

  private void CreateCustomServicesContainer()
  {
    this.customServices = new CustomServices();
    ServerServices.AddService(typeof (ICustomServices), (object) this.customServices);
  }

  private void RemoveCustomServicesContainer()
  {
    if (this.customServices == null)
      return;
    ServerServices.RemoveService(typeof (ICustomServices));
    this.customServices = (CustomServices) null;
  }

  private void CreateRemotingInfoService()
  {
    this.remotingInfoService = new RemotingInfoService();
    TrackingServices.RegisterTrackingHandler((ITrackingHandler) this.remotingInfoService);
    ServerServices.AddService(typeof (IRemotingInfoService), (object) this.remotingInfoService);
    this.customServices.AddService(typeof (IRemotingInfoService), (object) this.remotingInfoService);
  }

  private void RemoveRemotingInfoService()
  {
    if (this.remotingInfoService == null)
      return;
    TrackingServices.UnregisterTrackingHandler((ITrackingHandler) this.remotingInfoService);
    ServerServices.RemoveService(typeof (IRemotingInfoService));
    this.customServices.RemoveService(typeof (IRemotingInfoService));
    this.remotingInfoService = (RemotingInfoService) null;
  }

  private void CreateDBManagerLogger()
  {
    this.dbManagerLogger = new DbManagerLogger();
    DbManagerConfiguration.Loggers.Add((IDbManagerLogger) this.dbManagerLogger);
    DbManagerConfiguration.Loggers.Enabled = false;
  }

  private void RemoveDBManagerLogger()
  {
    if (this.dbManagerLogger == null)
      return;
    DbManagerConfiguration.Loggers.Remove((IDbManagerLogger) this.dbManagerLogger);
    this.dbManagerLogger = (DbManagerLogger) null;
  }

  private void CreateIMServer() => this.imserver = new IntermechServer();

  private void RemoveIMServer()
  {
    if (this.imserver == null)
      return;
    RemotingServices.Disconnect((MarshalByRefObject) this.imserver);
    this.imserver.CloseServer();
    this.imserver = (IntermechServer) null;
  }

  private void CreateConsoleCommandsRegistry()
  {
    this.commandsRegistryService = (IConsoleCommandRegistry) new ConsoleCommandRegistry();
    ServerServices.AddService(typeof (IConsoleCommandRegistry), (object) this.commandsRegistryService);
    this.RegisterGeneralConsoleCommands();
    this.RegisterSqlLogConsoleCommands();
    this.RegisterAdminConsoleCommands();
  }

  private void RemoveConsoleCommandsRegistry()
  {
    if (this.commandsRegistryService == null)
      return;
    ServerServices.RemoveService(typeof (IConsoleCommandRegistry));
    this.commandsRegistryService = (IConsoleCommandRegistry) null;
  }

  private bool TryStartRemoting()
  {
    try
    {
      ServerRemotingConfigurator remotingConfigurator = new ServerRemotingConfigurator();
      remotingConfigurator.Configure();
      this.imserverUri = remotingConfigurator.IMServerUri;
      return true;
    }
    catch (Exception ex)
    {
      string text = ex.Message;
      int length = text.IndexOf("\r\n");
      if (length > 0)
        text = text.Substring(0, length);
      this.consoleService.WriteLine(LocalizationHolder.rm.GetString("ConsoleServer_11"), ConsoleColor.Yellow);
      this.consoleService.WriteLine(LocalizationHolder.rm.GetString("ConsoleServer_12"), ConsoleColor.Yellow);
      this.consoleService.WriteLine(string.Empty);
      this.consoleService.WriteLine(LocalizationHolder.rm.GetString("ConsoleServer_13"), ConsoleColor.Red);
      this.consoleService.WriteLine(text, ConsoleColor.Red);
      this.consoleService.WriteLine(string.Empty);
      this.consoleService.WriteLine("Нажмите enter для завершения работы приложения.");
      this.consoleService.ReadLine();
      return false;
    }
  }

  private void CreateOutputViewService()
  {
    this.outputViewService = new OutputViewService();
    ServerServices.AddService(typeof (IOutputView), (object) this.outputViewService);
    this.customServices.AddService(typeof (IOutputViewHistory), (object) this.outputViewService);
  }

  private void RemoveOutputViewService()
  {
    if (this.outputViewService == null)
      return;
    ServerServices.RemoveService(typeof (IOutputView));
    this.customServices.RemoveService(typeof (IOutputViewHistory));
    this.outputViewService = (OutputViewService) null;
  }

  private void RegisterGeneralConsoleCommands()
  {
    this.commandsRegistryService.Add(new ConsoleCommandInfo("gc", string.Empty, string.Empty, new ConsoleCommandMethod(this.GCCommand)));
    this.commandsRegistryService.Add(new ConsoleCommandInfo("dumpdm", string.Empty, string.Empty, new ConsoleCommandMethod(this.DumpDataManagersCommand)));
    this.commandsRegistryService.Add(new ConsoleCommandInfo("info", string.Empty, "show connection string", new ConsoleCommandMethod(this.ShowInfoCommand)));
    this.commandsRegistryService.Add(new ConsoleCommandInfo("sessions", string.Empty, "show sessions information", new ConsoleCommandMethod(this.PrintSessionsListCommand)));
    this.commandsRegistryService.Add(new ConsoleCommandInfo("converters", string.Empty, LocalizationHolder.rm.GetString("ShowConverters"), new ConsoleCommandMethod(this.PrintIndexConvertersCommand)));
  }

  private void GCCommand(IConsoleService consoleService, List<string> commandArgs)
  {
    GC.Collect();
    consoleService.WriteLine("Garbage collection is done.");
  }

  private void RegisterSqlLogConsoleCommands()
  {
    this.commandsRegistryService.Add(new ConsoleCommandInfo("sqllogon", string.Empty, "enable SQL logging", new ConsoleCommandMethod(this.SqlLogOnCommand)));
    this.commandsRegistryService.Add(new ConsoleCommandInfo("sqllogoff", string.Empty, "disable SQL logging", new ConsoleCommandMethod(this.SqlLogOffCommand)));
    this.commandsRegistryService.Add(new ConsoleCommandInfo("sqllogfile", string.Empty, "filename for SQL log", new ConsoleCommandMethod(this.SqlLogFileCommand)));
  }

  private void SqlLogOnCommand(IConsoleService consoleService, List<string> commandArgs)
  {
    DbManagerConfiguration.Loggers.Enabled = true;
    consoleService.WriteLine("SQL logging is enabled");
  }

  private void SqlLogOffCommand(IConsoleService consoleService, List<string> commandArgs)
  {
    DbManagerConfiguration.Loggers.Enabled = false;
    consoleService.WriteLine("SQL logging is disabled");
  }

  private void SqlLogFileCommand(IConsoleService consoleService, List<string> commandArgs)
  {
    consoleService.WriteLine("Enter SQL log file name(if empty, log to console)");
    this.dbManagerLogger.FileName = consoleService.ReadLine();
  }

  private void RegisterAdminConsoleCommands()
  {
    this.commandsRegistryService.Add(new ConsoleCommandInfo("admin", string.Empty, string.Empty, new ConsoleCommandMethod(this.ShowAdminMemu)));
    this.commandsRegistryService.Add(new ConsoleCommandInfo("list", string.Empty, LocalizationHolder.rm.GetString("ConsoleServer_5"), (IConsoleCommand) new ShowRemoteObjectsCommand(this.remotingInfoService)));
    this.commandsRegistryService.Add(new ConsoleCommandInfo("verbose", string.Empty, LocalizationHolder.rm.GetString("ConsoleServer_6"), (IConsoleCommand) new VerboseRemoteObjectsCommand(this.remotingInfoService)));
  }

  private string ProtectionService_Authorize(int daysLeft, string licenseText, ref bool cancel)
  {
    cancel = false;
    if (daysLeft > 7)
      return string.Empty;
    string str = LocalizationHolder.rm.GetString("ConsoleServer_21");
    if (daysLeft < 5)
      str = LocalizationHolder.rm.GetString("ConsoleServer_22");
    if (daysLeft == 1)
      str = LocalizationHolder.rm.GetString("ConsoleServer_23");
    if (daysLeft == 0)
      this.consoleService.WriteLine(LocalizationHolder.rm.GetString("ConsoleServer_24"), ConsoleColor.Yellow);
    else
      this.consoleService.WriteLine(string.Format(LocalizationHolder.rm.GetString("ConsoleServer_25"), (object) daysLeft, (object) str), ConsoleColor.Yellow);
    this.consoleService.WriteLine(string.Format(LocalizationHolder.rm.GetString("ConsoleServer_26"), (object) licenseText), ConsoleColor.Yellow);
    this.consoleService.Write(LocalizationHolder.rm.GetString("ConsoleServer_27"), ConsoleColor.Yellow);
    return this.consoleService.ReadLine();
  }

  private void InitializeExceptionHandlers()
  {
    ExceptionServices.StackTraceBuilderFactory = this.iocContainer.Get<Func<StackTraceBuilder>>();
    this.fatalExceptionHandler = new IPSFatalExceptionLogger(this.eventLogService.AllLogs);
    this.fatalExceptionHandler.Activate();
  }

  private void RemoveExceptionHandlers()
  {
    if (this.fatalExceptionHandler == null)
      return;
    this.fatalExceptionHandler.Deactivate();
    this.fatalExceptionHandler = (IPSFatalExceptionLogger) null;
  }

  private void PrintSessionsListCommand(IConsoleService consoleService, List<string> commandArgs)
  {
    (ServerServices.GetService(typeof (IUserSessionCollection)) as IUserSessionCollection).PrintSessions(string.Empty, true);
  }

  private void PrepareBase()
  {
    Console.Write(LocalizationHolder.rm.GetString("ConsoleServer_8"));
    if (!(Console.ReadLine().ToLower() == "да"))
      return;
    (ServerServices.GetService(typeof (IAdminUtilsService)) as AdminUtilsService).PrepareSourceDatabase(0, UtilsOutputMode.Console);
  }

  private void RetrieveDBStatistics(IUserSession session)
  {
    Console.WriteLine("Начат сбор статистики в базе данных...");
    (ServerServices.GetService(typeof (IAdminUtilsService)) as AdminUtilsService).RetrieveDBStatistics(session.SessionGUID);
    Console.WriteLine("Сбор статистики завершен.");
  }

  private void PurgeObjectTypes()
  {
    Console.Write("Введите имя файла со списком типов удаляемых объектов:");
    string settingsFileName = Console.ReadLine();
    (ServerServices.GetService(typeof (IAdminUtilsService)) as AdminUtilsService).PurgeObjectTypes(settingsFileName);
  }

  private void ShowAdminMemu(IConsoleService consoleService, List<string> commandArgs)
  {
    UserSession session = this.imserver.CreateSession() as UserSession;
    if (this.savedAdminLoginInfo.IsEmpty)
    {
      Console.Write("login:");
      string str = Console.ReadLine();
      Console.Write("password:");
      StringBuilder stringBuilder = new StringBuilder();
      while (true)
      {
        ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
        if (consoleKeyInfo.Key != ConsoleKey.Enter)
        {
          stringBuilder.Append(consoleKeyInfo.KeyChar);
          Console.Write("*");
        }
        else
          break;
      }
      Console.WriteLine();
      Console.Write("Большинство команд данного меню требуют эксклюзивный доступ к базе данных. Работа пользователей перед выполнением данных команд должна быть остановлена. Продолжить (да/нет)?");
      if (Console.ReadLine().ToLower().Trim() != "да")
        return;
      DateTime now = DateTime.Now;
      TimeSpan aTimeZoneOffset = now - now.ToUniversalTime();
      try
      {
        session.Login(str, new PswPackage(stringBuilder.ToString(), CryptHelper.NoneCrypt), EnvironmentConsts.MachineName, aTimeZoneOffset, session.IdentHelper.AdminRoleID, "AdminMenuSession");
        (ServerServices.GetService(typeof (IAdminUtilsService)) as IAdminUtilsService).CheckAdminProcedureAccess(session.SessionGUID, "Вход в административное меню сервера приложений");
        this.savedAdminLoginInfo = new Program.SavedLoginInfo(str, stringBuilder.ToString());
      }
      catch (Exception ex)
      {
        this.PrintError(ex.Message);
        return;
      }
    }
    else
    {
      DateTime now = DateTime.Now;
      TimeSpan aTimeZoneOffset = now - now.ToUniversalTime();
      session.Login(this.savedAdminLoginInfo.UserName, new PswPackage(this.savedAdminLoginInfo.Password, CryptHelper.NoneCrypt), EnvironmentConsts.MachineName, aTimeZoneOffset, session.IdentHelper.AdminRoleID, "AdminMenuSession");
    }
    Console.WriteLine(" ================  ADMIN MENU  ======================");
    Console.WriteLine();
    Console.WriteLine("1. Rebuild IMS_OBJECTS_VIEW");
    Console.WriteLine("2. Rebuild views for objects");
    Console.WriteLine("3. Rebuild views for relations");
    Console.WriteLine("4. Delete objects");
    Console.WriteLine("5. Correct classifier keys for Imbase objects");
    Console.WriteLine("6. Correct classifier keys for");
    Console.WriteLine("7. Prepare empty database");
    Console.WriteLine("8. Retrieve database statistics");
    Console.WriteLine("9. Repair ECO contexts");
    Console.WriteLine("10. Rebuild Global Index");
    Console.WriteLine("11. Articles attributes synchronization");
    Console.WriteLine("12. Delete disabled relations");
    Console.WriteLine("13. Delete signs with empty graph");
    Console.WriteLine("14. Find storage errors");
    Console.WriteLine("15. Fix storage errors");
    Console.WriteLine("16. Generate attributes report");
    Console.WriteLine("17. Repair database");
    Console.WriteLine("18. Unlock admin commands");
    Console.WriteLine();
    Console.WriteLine("100. Exit");
    Console.Write(">");
    string str1;
    do
    {
      Console.Write(">");
      str1 = Console.ReadLine();
      if (str1 == "1")
      {
        try
        {
          (ServerServices.GetService(typeof (IAdminUtilsService)) as IAdminUtilsService).RebuildObjectsView(session.SessionGUID);
          Console.WriteLine("Completed.");
        }
        catch (Exception ex)
        {
          this.PrintError(ex.Message);
        }
      }
      else if (str1 == "2")
      {
        DataTable dataTable = session.GetObjectTypeCollection(-2).Select(string.Empty);
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          try
          {
            IDBObjectType objectType = session.GetObjectType(Convert.ToInt32(dataTable.Rows[index]["F_OBJECT_TYPE"]));
            Console.Write($"{index + 1} of {dataTable.Rows.Count}: Rebuild for \"{objectType.ObjectTypeName}\" ... ");
            objectType.RebuildView();
            Console.WriteLine("OK");
          }
          catch (Exception ex)
          {
            this.PrintError(ex.Message);
          }
        }
        Console.WriteLine("Completed.");
      }
      else if (str1 == "3")
      {
        DataTable dataTable = session.GetRelationTypeCollection().Select(string.Empty);
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          try
          {
            IDBRelationType relationType = session.GetRelationType(Convert.ToInt32(dataTable.Rows[index]["F_RELATION_TYPE"]));
            Console.Write($"{index + 1} of {dataTable.Rows.Count}: Rebuild for \"{relationType.Description}\" ... ");
            relationType.RebuildView();
            Console.WriteLine("OK");
          }
          catch (Exception ex)
          {
            this.PrintError(ex.Message);
          }
        }
        Console.WriteLine("Completed.");
      }
      else if (str1 == "4")
        this.PurgeObjectTypes();
      else if (str1 == "5")
      {
        Console.WriteLine("Start correct classifier keys for Imbase objects...");
        DataTable dataTable = session.GetObjectCollection(MetaDataHelper.GetObjectTypeID(new Guid("cad00221-306c-11d8-b4e9-00304f19f545"))).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID
        }));
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          DBClassifier.RebuildKeys((IUserSession) session, new long[1]
          {
            Convert.ToInt64(dataTable.Rows[index][0])
          });
          if (index == dataTable.Rows.Count - 1)
            Console.WriteLine(".");
          else
            Console.Write(".");
        }
        Console.WriteLine("Completed.");
      }
      else if (str1 == "6")
      {
        Console.Write("Enter root object id:");
        long result;
        if (!long.TryParse(Console.ReadLine(), out result))
        {
          this.PrintError("Wrong id!");
        }
        else
        {
          Console.WriteLine($"Start correct classifier keys for {result}...");
          DBClassifier.RebuildKeys((IUserSession) session, new long[1]
          {
            result
          });
          Console.WriteLine("Completed.");
        }
      }
      else if (str1 == "7")
        this.PrepareBase();
      else if (str1 == "8")
        this.RetrieveDBStatistics((IUserSession) session);
      else if (str1 == "9")
        (ServerServices.GetService(typeof (IAdminUtilsService)) as AdminUtilsService).FixECO_Context((IUserSession) session);
      else if (str1 == "10")
        (ServerServices.GetService(typeof (IAdminUtilsService)) as AdminUtilsService).RebuidGlobalIndex((IUserSession) session);
      else if (str1 == "11")
        (ServerServices.GetService(typeof (IAdminUtilsService)) as AdminUtilsService).ArtAttrsSync((IUserSession) session);
      else if (str1 == "12")
        (ServerServices.GetService(typeof (IAdminUtilsService)) as AdminUtilsService).DeleteInvalidRelations((IUserSession) session);
      else if (str1 == "13")
        (ServerServices.GetService(typeof (IAdminUtilsService)) as AdminUtilsService).DeleteEmptyGraphSigns((IUserSession) session);
      else if (str1 == "14")
        (ServerServices.GetService(typeof (IAdminUtilsService)) as AdminUtilsService).DeleteDublicateFiles((IUserSession) session, false);
      else if (str1 == "15")
        (ServerServices.GetService(typeof (IAdminUtilsService)) as AdminUtilsService).DeleteDublicateFiles((IUserSession) session, true);
      else if (str1 == "16")
      {
        AdminUtilsService service = ServerServices.GetService(typeof (IAdminUtilsService)) as AdminUtilsService;
        Console.Write("Введите имя файла отчета:");
        using (StreamWriter text = File.CreateText(Console.ReadLine()))
        {
          text.WriteLine("Проверка обоснованности назначения атрибутов типам объектов и связей");
          Console.Write("Введите минимальное количество объектов для анализа:");
          string str2 = Console.ReadLine();
          int int32 = Convert.ToInt32(str2);
          text.WriteLine("Минимальное количество объектов для анализа: " + str2);
          text.WriteLine();
          foreach (string nullAttribute in service.GetNULLAttributes(session.SessionGUID, int32, true))
            text.WriteLine(nullAttribute);
          Console.Write("Анализ атрибутов закончен.");
        }
      }
      else if (str1 == "17")
      {
        AdminUtilsService service = ServerServices.GetService(typeof (IAdminUtilsService)) as AdminUtilsService;
        Console.WriteLine("Начата проверка целостности данных...");
        Guid sessionGuid = session.SessionGUID;
        string[] strArray = service.RepairData(sessionGuid);
        if (strArray != null && strArray.Length != 0)
        {
          foreach (string str3 in strArray)
            Console.WriteLine(str3);
        }
        else
          Console.WriteLine("Проверка базы данных успешно завершена.");
      }
      else if (str1 == "18")
      {
        (ServerServices.GetService(typeof (IDatabaseLocker)) as IDatabaseLocker).UnLockAll((IUserSession) session);
        Console.WriteLine("Административные команды успешно разблокированы.");
      }
    }
    while (!(str1 == "100"));
    session.Logout("AdminMenuSession");
    this.savedAdminLoginInfo = new Program.SavedLoginInfo();
  }

  private void PrintError(string errorString)
  {
    this.consoleService.WriteLine(errorString, ConsoleColor.Red);
  }

  private void PrintIndexConvertersCommand(IConsoleService consoleService, List<string> commandArgs)
  {
    IGlobalIndexSettings service = ServerServices.GetService(typeof (IGlobalIndexService)) as IGlobalIndexSettings;
    foreach (string converters in service.ConvertersList)
      consoleService.WriteLine(converters);
    string indexingExtensions = service.NotIndexingExtensions;
    if (indexingExtensions != string.Empty)
      consoleService.WriteLine("Запрещённые для индексации типы файлов: " + indexingExtensions);
    consoleService.WriteLine("---------------------------------");
    consoleService.WriteLine($"Размер очереди на индексацию: {service.QueueLength}");
  }

  private void ShowInfoCommand(IConsoleService consoleService, List<string> commandArgs)
  {
    string connectionString = ServerConsts.ShortenedConnectionString;
    consoleService.WriteLine(connectionString);
  }

  private void DumpDataManagersCommand(IConsoleService consoleService, List<string> commandArgs)
  {
    ICollection<IDbManagerStatus> activeDbManagers = ((IDbManagerService) ApplicationServices.Container.GetService(typeof (IDbManagerService))).GetActiveDbManagers();
    consoleService.WriteLine(string.Format(LocalizationHolder.rm.GetString("ConsoleServer_9"), (object) activeDbManagers.Count), ConsoleColor.Green);
    foreach (IDbManagerStatus dbManagerStatus in (IEnumerable<IDbManagerStatus>) activeDbManagers)
    {
      Thread.MemoryBarrier();
      IDbManagerConnectionInfo connectionInfo = dbManagerStatus.GetConnectionInfo();
      ConsoleColor color = connectionInfo.InTransaction ? ConsoleColor.Red : ConsoleColor.Green;
      if (connectionInfo.ConnectionState == ConnectionState.Open)
        color = ConsoleColor.DarkRed;
      consoleService.WriteLine($"{connectionInfo.ID:X8}:\t{connectionInfo.InTransaction}\t{connectionInfo.TransactionDepth}\t{connectionInfo.ConnectionState}", color);
    }
    consoleService.WriteLine(LocalizationHolder.rm.GetString("ConsoleServer_10"), ConsoleColor.Green);
  }

  private void LogApplicationRunning()
  {
    string location = this.GetType().Assembly.Location;
    StringBuilder stringBuilder = new StringBuilder(512 /*0x0200*/);
    stringBuilder.AppendLine($"{ServerDiagnosticsConsts.EventLogSourceName} запущен.");
    stringBuilder.AppendLine($"Исполняемый файл: {location}");
    if (File.Exists(location))
    {
      FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(location);
      stringBuilder.AppendLine($"Версия: {versionInfo.FileVersion}");
    }
    this.eventLogService.DefaultLog.Write(stringBuilder.ToString());
  }

  private void LogApplicationStopped()
  {
    if (this.eventLogService == null)
      return;
    this.eventLogService.DefaultLog.Write($"{ServerDiagnosticsConsts.EventLogSourceName} завершил выполнение.");
  }

  private void DisplayBanner()
  {
    try
    {
      object[] customAttributes1 = typeof (XMLSettingsStorage).Assembly.GetCustomAttributes(typeof (AssemblyVersionString), true);
      object[] customAttributes2 = typeof (XMLSettingsStorage).Assembly.GetCustomAttributes(typeof (AssemblyBuildDate), true);
      AssemblyVersionString assemblyVersionString = customAttributes1 == null || customAttributes1.Length == 0 ? (AssemblyVersionString) null : customAttributes1[0] as AssemblyVersionString;
      AssemblyBuildDate assemblyBuildDate = customAttributes2 == null || customAttributes2.Length == 0 ? (AssemblyBuildDate) null : customAttributes2[0] as AssemblyBuildDate;
      if (assemblyVersionString == null || assemblyBuildDate == null)
        return;
      string[] strArray = assemblyVersionString.Description.Split('.');
      string str = strArray.Length != 4 || DataSetProcessor.GetInt64Value((object) strArray[2], 0L) <= 0L ? string.Empty : $"Service Pack {strArray[2]} ";
      this.consoleService.WriteLine(string.Format("IPS Console Server v{0} {3}({1}){4}Copyright (c) 2003-{2} Intermech\n", (object) assemblyVersionString.Description, (object) assemblyBuildDate.Description, (object) assemblyBuildDate.AssemblyBuildYear, (object) str, !string.IsNullOrEmpty(str) ? (object) "\n" : (object) " "), ConsoleColor.White);
    }
    catch
    {
    }
  }

  private bool TryCreateProtectionKey()
  {
    int num = 335;
    byte[][] numArray = new byte[32 /*0x20*/][]
    {
      new byte[16 /*0x10*/]
      {
        (byte) 81,
        (byte) 176 /*0xB0*/,
        (byte) 223,
        (byte) 95,
        (byte) 29,
        (byte) 115,
        (byte) 218,
        (byte) 16 /*0x10*/,
        (byte) 92,
        (byte) 83,
        (byte) 181,
        (byte) 185,
        (byte) 76,
        (byte) 215,
        (byte) 200,
        (byte) 230
      },
      new byte[16 /*0x10*/]
      {
        (byte) 14,
        (byte) 31 /*0x1F*/,
        (byte) 172,
        (byte) 58,
        (byte) 46,
        (byte) 180,
        (byte) 109,
        (byte) 115,
        (byte) 243,
        (byte) 5,
        (byte) 133,
        (byte) 191,
        (byte) 51,
        (byte) 102,
        (byte) 74,
        (byte) 55
      },
      new byte[16 /*0x10*/]
      {
        (byte) 124,
        (byte) 77,
        (byte) 53,
        (byte) 150,
        (byte) 121,
        (byte) 19,
        (byte) 40,
        (byte) 161,
        (byte) 118,
        (byte) 102,
        (byte) 112 /*0x70*/,
        (byte) 54,
        (byte) 81,
        (byte) 87,
        (byte) 51,
        (byte) 80 /*0x50*/
      },
      new byte[16 /*0x10*/]
      {
        (byte) 250,
        (byte) 172,
        (byte) 245,
        (byte) 177,
        (byte) 46,
        byte.MaxValue,
        (byte) 213,
        (byte) 243,
        (byte) 139,
        (byte) 14,
        (byte) 40,
        (byte) 11,
        (byte) 111,
        (byte) 113,
        (byte) 90,
        (byte) 118
      },
      new byte[16 /*0x10*/]
      {
        (byte) 119,
        (byte) 166,
        (byte) 202,
        (byte) 220,
        (byte) 237,
        (byte) 177,
        (byte) 231,
        (byte) 223,
        (byte) 42,
        (byte) 235,
        (byte) 43,
        (byte) 14,
        (byte) 235,
        (byte) 182,
        (byte) 69,
        (byte) 152
      },
      new byte[16 /*0x10*/]
      {
        (byte) 100,
        (byte) 184,
        (byte) 202,
        (byte) 33,
        (byte) 153,
        (byte) 4,
        (byte) 234,
        (byte) 221,
        (byte) 33,
        (byte) 186,
        (byte) 57,
        (byte) 147,
        (byte) 50,
        (byte) 128 /*0x80*/,
        (byte) 172,
        (byte) 145
      },
      new byte[16 /*0x10*/]
      {
        (byte) 226,
        (byte) 95,
        (byte) 93,
        (byte) 40,
        (byte) 9,
        (byte) 183,
        (byte) 204,
        (byte) 61,
        (byte) 136,
        (byte) 61,
        (byte) 233,
        (byte) 183,
        (byte) 3,
        (byte) 163,
        (byte) 190,
        (byte) 5
      },
      new byte[16 /*0x10*/]
      {
        (byte) 66,
        (byte) 227,
        (byte) 245,
        (byte) 118,
        (byte) 14,
        (byte) 220,
        (byte) 228,
        (byte) 173,
        (byte) 208 /*0xD0*/,
        (byte) 56,
        (byte) 46,
        (byte) 223,
        (byte) 150,
        (byte) 130,
        (byte) 49,
        (byte) 218
      },
      new byte[16 /*0x10*/]
      {
        (byte) 31 /*0x1F*/,
        (byte) 101,
        (byte) 66,
        (byte) 166,
        (byte) 12,
        (byte) 236,
        (byte) 249,
        (byte) 203,
        (byte) 102,
        (byte) 86,
        (byte) 107,
        (byte) 141,
        (byte) 232,
        (byte) 25,
        (byte) 181,
        (byte) 192 /*0xC0*/
      },
      new byte[16 /*0x10*/]
      {
        (byte) 155,
        (byte) 86,
        (byte) 127 /*0x7F*/,
        (byte) 1,
        (byte) 181,
        (byte) 70,
        (byte) 129,
        (byte) 172,
        (byte) 18,
        (byte) 172,
        (byte) 142,
        (byte) 124,
        (byte) 2,
        (byte) 47,
        (byte) 151,
        (byte) 50
      },
      new byte[16 /*0x10*/]
      {
        (byte) 32 /*0x20*/,
        (byte) 215,
        (byte) 83,
        (byte) 241,
        (byte) 249,
        (byte) 10,
        (byte) 71,
        (byte) 191,
        (byte) 217,
        (byte) 141,
        (byte) 84,
        (byte) 175,
        (byte) 199,
        (byte) 47,
        (byte) 219,
        (byte) 134
      },
      new byte[16 /*0x10*/]
      {
        (byte) 193,
        (byte) 228,
        (byte) 240 /*0xF0*/,
        (byte) 158,
        (byte) 50,
        (byte) 137,
        (byte) 130,
        (byte) 137,
        (byte) 1,
        (byte) 26,
        (byte) 172,
        (byte) 158,
        (byte) 37,
        (byte) 6,
        (byte) 96 /*0x60*/,
        (byte) 245
      },
      new byte[16 /*0x10*/]
      {
        (byte) 44,
        (byte) 38,
        (byte) 238,
        (byte) 134,
        (byte) 115,
        (byte) 159,
        (byte) 139,
        (byte) 245,
        (byte) 102,
        (byte) 146,
        (byte) 193,
        (byte) 178,
        (byte) 75,
        (byte) 63 /*0x3F*/,
        (byte) 85,
        (byte) 110
      },
      new byte[16 /*0x10*/]
      {
        (byte) 115,
        (byte) 132,
        (byte) 62,
        (byte) 106,
        (byte) 254,
        (byte) 180,
        (byte) 0,
        (byte) 93,
        (byte) 251,
        (byte) 141,
        (byte) 224 /*0xE0*/,
        (byte) 76,
        (byte) 125,
        (byte) 170,
        (byte) 71,
        (byte) 120
      },
      new byte[16 /*0x10*/]
      {
        (byte) 135,
        (byte) 40,
        (byte) 150,
        (byte) 43,
        (byte) 80 /*0x50*/,
        (byte) 205,
        (byte) 11,
        (byte) 20,
        (byte) 29,
        (byte) 130,
        (byte) 204,
        (byte) 127 /*0x7F*/,
        (byte) 152,
        (byte) 74,
        (byte) 3,
        (byte) 55
      },
      new byte[16 /*0x10*/]
      {
        (byte) 252,
        (byte) 67,
        (byte) 81,
        (byte) 193,
        (byte) 18,
        (byte) 138,
        (byte) 39,
        (byte) 208 /*0xD0*/,
        (byte) 61,
        (byte) 88,
        (byte) 211,
        byte.MaxValue,
        (byte) 44,
        (byte) 88,
        (byte) 105,
        (byte) 132
      },
      new byte[16 /*0x10*/]
      {
        (byte) 122,
        (byte) 228,
        (byte) 96 /*0x60*/,
        (byte) 194,
        (byte) 43,
        (byte) 47,
        (byte) 76,
        (byte) 128 /*0x80*/,
        (byte) 75,
        (byte) 93,
        (byte) 139,
        (byte) 103,
        (byte) 72,
        (byte) 148,
        (byte) 115,
        (byte) 154
      },
      new byte[16 /*0x10*/]
      {
        (byte) 224 /*0xE0*/,
        (byte) 48 /*0x30*/,
        (byte) 179,
        (byte) 134,
        (byte) 14,
        (byte) 43,
        (byte) 94,
        (byte) 22,
        (byte) 130,
        (byte) 215,
        (byte) 148,
        (byte) 233,
        (byte) 173,
        (byte) 29,
        (byte) 26,
        (byte) 113
      },
      new byte[16 /*0x10*/]
      {
        (byte) 55,
        (byte) 229,
        (byte) 39,
        (byte) 158,
        (byte) 152,
        (byte) 175,
        (byte) 54,
        (byte) 95,
        (byte) 26,
        (byte) 217,
        (byte) 105,
        (byte) 240 /*0xF0*/,
        (byte) 211,
        (byte) 12,
        (byte) 93,
        (byte) 27
      },
      new byte[16 /*0x10*/]
      {
        (byte) 226,
        (byte) 182,
        (byte) 246,
        (byte) 84,
        (byte) 212,
        (byte) 52,
        (byte) 165,
        (byte) 187,
        (byte) 223,
        (byte) 76,
        (byte) 71,
        (byte) 207,
        (byte) 80 /*0x50*/,
        (byte) 21,
        (byte) 208 /*0xD0*/,
        (byte) 138
      },
      new byte[16 /*0x10*/]
      {
        (byte) 143,
        (byte) 138,
        (byte) 55,
        (byte) 103,
        (byte) 222,
        (byte) 146,
        (byte) 66,
        (byte) 88,
        (byte) 220,
        (byte) 21,
        (byte) 223,
        (byte) 242,
        (byte) 65,
        (byte) 163,
        (byte) 203,
        (byte) 136
      },
      new byte[16 /*0x10*/]
      {
        (byte) 98,
        (byte) 252,
        (byte) 81,
        (byte) 214,
        (byte) 225,
        (byte) 137,
        (byte) 177,
        (byte) 246,
        (byte) 16 /*0x10*/,
        (byte) 72,
        (byte) 83,
        (byte) 211,
        (byte) 102,
        (byte) 248,
        (byte) 69,
        (byte) 210
      },
      new byte[16 /*0x10*/]
      {
        (byte) 15,
        (byte) 49,
        (byte) 71,
        (byte) 178,
        (byte) 35,
        (byte) 100,
        (byte) 158,
        (byte) 202,
        (byte) 173,
        (byte) 1,
        (byte) 167,
        (byte) 81,
        (byte) 198,
        (byte) 91,
        (byte) 173,
        (byte) 63 /*0x3F*/
      },
      new byte[16 /*0x10*/]
      {
        (byte) 200,
        (byte) 119,
        (byte) 64 /*0x40*/,
        (byte) 126,
        (byte) 214,
        (byte) 146,
        (byte) 210,
        (byte) 147,
        (byte) 215,
        (byte) 95,
        (byte) 14,
        (byte) 68,
        (byte) 153,
        (byte) 217,
        (byte) 217,
        (byte) 218
      },
      new byte[16 /*0x10*/]
      {
        (byte) 0,
        (byte) 111,
        (byte) 216,
        (byte) 190,
        (byte) 92,
        (byte) 12,
        (byte) 188,
        (byte) 165,
        (byte) 163,
        (byte) 206,
        (byte) 194,
        (byte) 131,
        (byte) 208 /*0xD0*/,
        (byte) 68,
        (byte) 30,
        (byte) 118
      },
      new byte[16 /*0x10*/]
      {
        (byte) 245,
        (byte) 253,
        (byte) 86,
        (byte) 243,
        (byte) 103,
        (byte) 236,
        (byte) 129,
        (byte) 12,
        (byte) 199,
        (byte) 132,
        (byte) 54,
        (byte) 158,
        (byte) 204,
        (byte) 202,
        (byte) 37,
        (byte) 125
      },
      new byte[16 /*0x10*/]
      {
        (byte) 13,
        (byte) 224 /*0xE0*/,
        (byte) 225,
        (byte) 161,
        (byte) 196,
        (byte) 171,
        (byte) 248,
        (byte) 42,
        (byte) 236,
        (byte) 108,
        (byte) 185,
        (byte) 221,
        (byte) 234,
        (byte) 241,
        (byte) 146,
        (byte) 128 /*0x80*/
      },
      new byte[16 /*0x10*/]
      {
        (byte) 219,
        (byte) 119,
        (byte) 50,
        byte.MaxValue,
        (byte) 40,
        (byte) 80 /*0x50*/,
        (byte) 205,
        (byte) 213,
        (byte) 44,
        (byte) 193,
        (byte) 59,
        (byte) 119,
        (byte) 106,
        (byte) 83,
        (byte) 187,
        (byte) 51
      },
      new byte[16 /*0x10*/]
      {
        (byte) 103,
        (byte) 159,
        (byte) 241,
        (byte) 209,
        (byte) 203,
        (byte) 13,
        (byte) 44,
        (byte) 2,
        (byte) 117,
        (byte) 162,
        (byte) 15,
        (byte) 84,
        (byte) 161,
        (byte) 27,
        (byte) 250,
        (byte) 104
      },
      new byte[16 /*0x10*/]
      {
        (byte) 147,
        (byte) 217,
        (byte) 183,
        (byte) 105,
        (byte) 243,
        (byte) 217,
        (byte) 51,
        (byte) 63 /*0x3F*/,
        (byte) 51,
        (byte) 91,
        (byte) 136,
        (byte) 153,
        (byte) 244,
        (byte) 212,
        (byte) 140,
        (byte) 214
      },
      new byte[16 /*0x10*/]
      {
        (byte) 227,
        (byte) 49,
        (byte) 67,
        (byte) 229,
        (byte) 122,
        (byte) 12,
        (byte) 123,
        (byte) 85,
        (byte) 119,
        (byte) 173,
        (byte) 209,
        (byte) 239,
        (byte) 82,
        (byte) 223,
        (byte) 186,
        (byte) 178
      },
      new byte[16 /*0x10*/]
      {
        (byte) 26,
        (byte) 100,
        (byte) 192 /*0xC0*/,
        (byte) 17,
        (byte) 198,
        (byte) 51,
        (byte) 201,
        (byte) 16 /*0x10*/,
        (byte) 74,
        (byte) 174,
        (byte) 3,
        (byte) 173,
        (byte) 184,
        (byte) 237,
        (byte) 169,
        (byte) 149
      }
    };
    int index1 = (Environment.TickCount & 15) * 2;
    byte[] query = numArray[index1];
    byte[] reply = numArray[index1 + 1];
    ProtectionService.Provider = (IServiceProvider) ServerServices.ServiceContainer;
    ProtectionService.HasUI = false;
    ProtectionService.f6 += new nq(this.ProtectionService_Authorize);
    try
    {
      try
      {
        if (ProtectionKeyBase.IsTerminal())
        {
          this.protectionKey = (IProtectionKey) null;
          string str = "Невозможно использовать локальный ключ в терминальной сессии.";
          this.eventLogService.DefaultLog.Write(str);
          this.consoleService.WriteLine(str, ConsoleColor.Yellow);
        }
        else
        {
          this.protectionKey = (IProtectionKey) new LocalKey(num, query, reply);
          string str = LocalizationHolder.rm.GetString("ConsoleServer_14");
          this.eventLogService.DefaultLog.Write(str);
          this.consoleService.WriteLine(str);
        }
      }
      catch (Exception ex)
      {
        int foregroundColor = (int) Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(LocalizationHolder.rm.GetString("ConsoleServer_1") + ex.Message);
        Console.WriteLine(LocalizationHolder.rm.GetString("ConsoleServer_2"));
        Console.ForegroundColor = (ConsoleColor) foregroundColor;
        this.protectionKey = (IProtectionKey) null;
      }
      NameValueCollection appSettings = ConfigurationManager.AppSettings;
      NetworkKey.SetSpareServers(appSettings["Protection.SpareServers"]);
      NetworkKey.SetInformAdmins(appSettings["Protection.InformAdmins"]);
      for (int index2 = 0; index2 < 10; ++index2)
      {
        try
        {
          if (this.protectionKey == null)
          {
            this.protectionKey = (IProtectionKey) new NetworkKey(num, query, reply);
            string str = LocalizationHolder.rm.GetString("ConsoleServer_15");
            this.eventLogService.DefaultLog.Write(str);
            this.consoleService.WriteLine(str);
          }
          else
            break;
        }
        catch (ProtectionException ex)
        {
          if (index2 < 8)
          {
            int foregroundColor = (int) Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(LocalizationHolder.rm.GetString("ConsoleServer_16") + ex.Message);
            Console.WriteLine(LocalizationHolder.rm.GetString("ConsoleServer_17"));
            Console.ForegroundColor = (ConsoleColor) foregroundColor;
            Thread.Sleep(15000);
            this.protectionKey = (IProtectionKey) null;
          }
        }
        catch
        {
          throw;
        }
      }
      if (this.protectionKey == null)
        throw new Exception(LocalizationHolder.rm.GetString("ConsoleServer_16") + "Превышено количество попыток подключения.");
      ServerServices.AddService(typeof (IProtectionKey), (object) this.protectionKey);
      ServerServices.AddService(typeof (ILicenser), (object) this.protectionKey);
    }
    catch (Exception ex)
    {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine(ex.Message);
      Console.WriteLine(ex.StackTrace);
      Console.ReadLine();
      return false;
    }
    return true;
  }

  private void RemoveProtectionKey(bool emergencyMode)
  {
    if (this.protectionKey == null)
      return;
    if (!emergencyMode)
    {
      ProtectionService.f6 -= new nq(this.ProtectionService_Authorize);
      ServerServices.RemoveService(typeof (IProtectionKey));
      ServerServices.RemoveService(typeof (ILicenser));
    }
    this.InvokeSilently((Action) (() => this.protectionKey.Dispose()), "this.protectionKey.Dispose()");
    this.protectionKey = (IProtectionKey) null;
  }

  private bool TryStartIMServer()
  {
    RemotingServices.Marshal((MarshalByRefObject) this.imserver, this.imserverUri);
    IntermechServerInitParams initParams = new IntermechServerInitParams();
    initParams.SharedLibraryInitializerService = this.iocContainer.Get<ISharedLibraryInitializerService>();
    initParams.MetadataChangeMonitor = this.iocContainer.Get<IMetadataChangeMonitor>();
    initParams.MetadataResolversFactory = this.iocContainer.Get<MetadataResolverFactory>();
    initParams.CustomServices = (ICustomServices) this.customServices;
    initParams.PluginManagerConfigureAction = new Action<PluginManager>(this.ConfigurePluginManager);
    initParams.OnlyPatchBase = this.HasCommandLineFlag("/Q");
    initParams.ClearPatchFlag = this.HasCommandLineFlag("/C");
    initParams.RebuildViewsMode = this.HasCommandLineFlag("/RV");
    initParams.SkipMetadataScripts = this.HasCommandLineFlag("/SkipMetadataScripts") || AppSettingsHelper.GetBoolean("SkipMetadataScripts", false);
    initParams.SkipPlugins = this.HasCommandLineFlag("/SkipPlugins") || AppSettingsHelper.GetBoolean("SkipPlugins", false);
    this.consoleService.WriteLine(LocalizationHolder.rm.GetString("ConsoleServer_18"));
    if (initParams.OnlyPatchBase)
      this.consoleService.WriteLine(LocalizationHolder.rm.GetString("ConsoleServer_19"));
    if (initParams.RebuildViewsMode)
      this.consoleService.WriteLine("Сервер запущен в режиме пересоздания представлений данных.");
    this.imserver.Initialize(initParams);
    return !initParams.OnlyPatchBase && !initParams.ClearPatchFlag && !initParams.RebuildViewsMode;
  }

  private void ConfigurePluginManager(PluginManager pluginManager)
  {
    pluginManager.PackageActivator = this.iocContainer.Get<IPackageActivator>();
  }

  private void RunCommandLoop()
  {
    new ConsoleCommandDispatcher(this.consoleService, this.commandsRegistryService).Run();
  }

  private bool HasCommandLineFlag(string flagName)
  {
    return CollectionUtils.Exists<string>((IEnumerable<string>) this.Arguments, (Predicate<string>) (x => string.Equals(x, flagName, StringComparison.OrdinalIgnoreCase)));
  }

  private class SavedLoginInfo
  {
    public bool IsEmpty;
    public string UserName;
    public string Password;

    public SavedLoginInfo(string userName, string password)
    {
      this.Password = password;
      this.UserName = userName;
      this.IsEmpty = false;
    }

    public SavedLoginInfo()
    {
      this.Password = string.Empty;
      this.UserName = string.Empty;
      this.IsEmpty = true;
    }
  }
}
