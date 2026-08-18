
// Type: IMClient.Program




using IMClient.AutoUpdater;
using IMClient.PropertyPages;
using IMClient.Remoting;
using IMClient.UserSessions;
using Intermech;
using Intermech.ApplicationModel;
using Intermech.ApplicationModel.NinjectIntegration;
using Intermech.Bars;
using Intermech.Calendars.Editor;
using Intermech.Client;
using Intermech.Client.Core;
using Intermech.Client.Scripting;
using Intermech.Controls;
using Intermech.Diagnostics;
using Intermech.Docking;
using Intermech.Globalization;
using Intermech.Interfaces;
using Intermech.Interfaces.Caches.Metadata;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Data.Metadata;
using Intermech.Interfaces.Plugins;
using Intermech.IO;
using Intermech.Protection;
using Intermech.Runtime.ComInterop.LocalServer;
using Intermech.Security;
using Intermech.Services;
using Intermech.UI;
using Intermech.UI.ExceptionHandling;
using Intermech.UI.Winforms;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace IMClient
{
    internal sealed class Program(string[] arguments) : ApplicationBase((IList<string>) arguments)
    {
      private StandardKernel iocContainer;
      private IApplicationEventLogService eventLogService;
      private ApplicationStateService applicationStateService;
      private IAlertMessageService alertService;
      private IExceptionHandlerService exceptionService;
      private IPSFatalExceptionLogger fatalExceptionHandler;
      private UIExceptionHandler uiExceptionHandler;
      private IProtectionKey protectionKey;
      private ComServer comServer;
      private ProgramAutoExitHandler comServerAutoExitHandler;
      private LazyInitializerModuleGroup beforeDBStageInitializers;
      private LazyInitializerModuleGroup afterDBStageInitializers;

      [STAThread]
      private static void Main(string[] args) => new Program(args).Run();

      protected override void DoRun()
      {
        base.DoRun();
        UICultureHelper.ApplySettingsFromConfigurationFile();
        this.InitializeWinforms();
        this.CreateIOCContainer();
        this.InitializeProgramServices();
        this.InitializeExceptionHandlers();
        this.InitializeRoleBasedSecurity();
        this.LogApplicationRunning();
        if (!this.TryInitializeHyperlinkHandler() || !this.TryInitializeComServer() || !this.TryStartLocalConsoleServerIfNeeded() || !this.TryStartRemoting() || !this.TryCreateProtectionKey())
          return;
        this.RunBeforeDBStageInitializers();
        this.RunAfterDBStageInitializers();
        this.RunMainFormLoop();
      }

      protected override void DoCleanup(bool errorMode)
      {
        if (this.applicationStateService != null)
          this.applicationStateService.RaiseExit();
        this.ShutdownAfterDBStageInitializers();
        this.ShutdownBeforeDBStageInitializers();
        this.InvokeSilently(new Action(this.RemoveProtectionKey));
        this.LogApplicationStopped();
        this.RemoveExceptionHandlers();
        this.ReleaseProgramServices();
        this.RemoveIOCContainer();
        base.DoCleanup(errorMode);
      }

      protected override IExceptionDisplayService TryGetExceptionDisplayService()
      {
        return this.iocContainer.Get<IExceptionDisplayService>();
      }

      private void InitializeWinforms()
      {
        HighDPIServices.EnableHighDPIMode();
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
      }

      private void CreateIOCContainer()
      {
        this.iocContainer = new StandardKernel(Array.Empty<INinjectModule>());
        this.iocContainer.Load((INinjectModule) new MainApplicationNinjectModule());
        ApplicationServices.Container.ServiceResolver = this.iocContainer.Get<IApplicationServiceResolver>();
        this.iocContainer.Load((INinjectModule) new AssemblyNinjectModule());
        this.iocContainer.Bind<StackTraceBuilder>().To<IPSStackTraceBuilder>();
        this.iocContainer.Bind<IApplicationStateEventsService, ApplicationStateService>().To<ApplicationStateService>().InSingletonScope();
        this.iocContainer.Bind<IEventLogWriter>().ToMethod<EventLogWriterSyncWrapper>((Func<IContext, EventLogWriterSyncWrapper>) (context => EventLogWriters.Synchronized((IEventLogWriter) ApplicationEventLogWriters.CreateTextFileWriter("imclient.log")))).InSingletonScope().Named("FileEventLog").OnDeactivation((Action<EventLogWriterSyncWrapper>) (instance => DisposeUtils.TryDispose((object) instance.Unwrap())));
        this.iocContainer.Bind<IEventLogWriter>().ToMethod<EventLogWriterSyncWrapper>((Func<IContext, EventLogWriterSyncWrapper>) (context => EventLogWriters.Synchronized(EventLogWriters.CreateSystemLogWriter(SystemEventLogType.Application, ClientDiagnosticsConsts.EventLogSourceName)))).InSingletonScope().Named("SystemEventLog");
        this.iocContainer.Bind<IApplicationEventLogService>().ToMethod(new Func<IContext, IApplicationEventLogService>(this.CreateEventLogService)).InSingletonScope();
        this.iocContainer.Bind<IAlertMessageService>().To<AlertMessageService>().InSingletonScope();
        this.iocContainer.Bind<IUIDispatcherService>().ToConstant<UIDispatcherService>(UIDispatcherService.FromCurrentUIThread()).InSingletonScope();
        this.iocContainer.Bind<IExceptionDisplayService, IExceptionHandlerService>().To<ExceptionHandlerService>().InSingletonScope().WithConstructorArgument<Func<Exception, DialogResult>>(new Func<Exception, DialogResult>(this.ShowUnhandledExceptionDialog)).WithPropertyValue("EventLogService", (Func<IContext, object>) (context => (object) context.Kernel.Get<IOptionalService<IApplicationEventLogService>>())).WithPropertyValue("SplashService", (Func<IContext, object>) (context => (object) context.Kernel.Get<IOptionalService<ISplashService>>())).WithPropertyValue("UINotificationService", (Func<IContext, object>) (context => (object) context.Kernel.Get<IOptionalService<IUINotificationService>>()));
        this.iocContainer.Bind<IMServerService>().ToSelf().InSingletonScope().WithPropertyValue("ConnectionErrorStrategy", (Func<IContext, object>) (context => (object) context.Kernel.Get<IMServerConnectionErrorStrategy>()));
        this.iocContainer.Bind<IMServerConnectionErrorStrategy>().To<IMServerInteractiveConnectionErrorStrategy>();
        this.iocContainer.Bind<MainFormInitParams>().ToSelf();
        this.iocContainer.Bind<Action<PluginManager>>().ToConstant<Action<PluginManager>>(new Action<PluginManager>(this.ConfigurePluginManager)).WhenInjectedInto<MainFormInitParams>();
        this.iocContainer.Bind<MainForm>().ToSelf().InSingletonScope();
        this.iocContainer.Bind<ILocalConfigurationManager>().To<LocalConfigurationManager>().InSingletonScope().WithConstructorArgument<string>("IMClient");
        this.iocContainer.Bind<IConfigurationManager>().To<Intermech.Interfaces.Configuration.ConfigurationManager>().InSingletonScope().WithConstructorArgument<string>("IMClient");
        this.iocContainer.Bind<IStartupService, StartupService>().To<StartupService>().InSingletonScope();
        this.iocContainer.Bind<IInvokeService>().To<WinformsInvokeService>().InSingletonScope().WithConstructorArgument(typeof (Form), (Func<IContext, object>) (context => (object) context.Kernel.Get<MainForm>()));
        this.iocContainer.Bind<IPropertyPagesService, PropertyPagesService>().To<PropertyPagesService>().InSingletonScope().WithConstructorArgument(typeof (System.IServiceProvider), (Func<IContext, object>) (context => (object) ApplicationServices.Container));
        this.iocContainer.Bind<ComServer>().ToSelf().InSingletonScope();
        this.iocContainer.Bind<INotificationService>().To<NotificationService>().InSingletonScope().WithPropertyValue("MainFormServiceProvider", (Func<IContext, object>) (context => (object) context.Kernel.Get<IOptionalService<IMainFormUpdate>>()));
        this.iocContainer.Bind<UserConfigurationFromFileModule>().ToSelf();
        this.iocContainer.Bind<Module>().ToSelf();
        this.iocContainer.Bind<IMClientSessionPool, IUserSessionPool>().To<IMClientSessionPool>().InSingletonScope().WithPropertyValue("OptionalServices", (Func<IContext, object>) (context => (object) context.Kernel.Get<SessionPoolOptionalServices>())).WithPropertyValue("SpeedupServices", (Func<IContext, object>) (context => (object) context.Kernel.Get<UserSessionSpeedupServices>()));
        this.iocContainer.Bind<IUserSessionLoginService>().To<UserSessionLoginService>().InSingletonScope();
        this.iocContainer.Bind<IMetadataChangeMonitor>().To<MetadataChangeMonitor>().InSingletonScope();
        this.iocContainer.Bind<MetadataResolverFactory>().ToSelf().InSingletonScope();
        this.iocContainer.Load((INinjectModule) new MetaDataHelperNinjectModule());
        this.iocContainer.Load((INinjectModule) new ClientCacheNinjectModule());
        this.iocContainer.Load((INinjectModule) new ClientMetadataCacheNinjectModule());
        this.iocContainer.Load((INinjectModule) new ClientSessionSpeedupServicesNinjectModule());
        this.iocContainer.Bind<ICustomServicesSpeedupService>().To<CustomServicesSpeedupService>().InSingletonScope();
        this.iocContainer.Bind<UserSessionPoolModule>().ToSelf();
        this.iocContainer.Bind<UserSessionValidatorsModule>().ToSelf();
        this.iocContainer.Bind<UserSessionExceptionsReporter>().ToSelf();
        this.iocContainer.Bind<UserSessionExceptionsModule>().ToSelf();
        this.iocContainer.Bind<UserConfigurationFromDBModule>().ToSelf();
        this.iocContainer.Bind<DBHelpersInitializationModule>().ToSelf();
        this.iocContainer.Bind<BarManager>().ToMethod((Func<IContext, BarManager>) (context => context.Kernel.Get<MainForm>().GetBarManagerService())).InSingletonScope();
        this.iocContainer.Bind<DockManager>().ToMethod((Func<IContext, DockManager>) (context => context.Kernel.Get<MainForm>().GetDockManagerService())).InSingletonScope();
        this.iocContainer.Bind<IServerEventLogService>().To<ServerEventLogService>().InSingletonScope();
        this.iocContainer.Load((INinjectModule) new CSharpScriptsNinjectModule());
        this.iocContainer.Load((INinjectModule) new RemotingServicesNinjectModule());
        this.iocContainer.Bind<ICalendarsEditorLoader>().To<CalendarsEditorLoader>().InSingletonScope();
      }

      private void RemoveIOCContainer()
      {
        if (this.iocContainer == null)
          return;
        this.iocContainer.Dispose();
        this.iocContainer = (StandardKernel) null;
      }

      private IApplicationEventLogService CreateEventLogService(IContext context)
      {
        IEventLogWriter eventLogWriter = context.Kernel.Get<IEventLogWriter>("FileEventLog");
        IEventLogWriter systemLog = context.Kernel.Get<IEventLogWriter>("SystemEventLog");
        return (IApplicationEventLogService) new ApplicationEventLogService(eventLogWriter, systemLog, eventLogWriter);
      }

      private void InitializeProgramServices()
      {
        this.eventLogService = this.iocContainer.Get<IApplicationEventLogService>();
        this.applicationStateService = this.iocContainer.Get<ApplicationStateService>();
        this.exceptionService = this.iocContainer.Get<IExceptionHandlerService>();
        this.alertService = this.iocContainer.Get<IAlertMessageService>();
      }

      private void ReleaseProgramServices()
      {
        this.alertService = (IAlertMessageService) null;
        this.exceptionService = (IExceptionHandlerService) null;
        this.applicationStateService = (ApplicationStateService) null;
        this.eventLogService = (IApplicationEventLogService) null;
      }

      private void InitializeExceptionHandlers()
      {
        ExceptionServices.StackTraceBuilderFactory = this.iocContainer.Get<Func<StackTraceBuilder>>();
        this.fatalExceptionHandler = new IPSFatalExceptionLogger(this.eventLogService.AllLogs);
        this.fatalExceptionHandler.Activate();
        this.uiExceptionHandler = new UIExceptionHandler((Action<Exception>) (uiException => this.exceptionService.ShowException(uiException)));
        this.uiExceptionHandler.Activate();
      }

      private void RemoveExceptionHandlers()
      {
        if (this.uiExceptionHandler != null)
        {
          this.uiExceptionHandler.Deactivate();
          this.uiExceptionHandler = (UIExceptionHandler) null;
        }
        if (this.fatalExceptionHandler == null)
          return;
        this.fatalExceptionHandler.Deactivate();
        this.fatalExceptionHandler = (IPSFatalExceptionLogger) null;
      }

      private DialogResult ShowUnhandledExceptionDialog(Exception exception)
      {
        using (ExceptionForm exceptionForm = new ExceptionForm())
        {
          exceptionForm.ViewModel.RecoveryHandler = (ErrorRecoveryHandler) new ExceptionRecoveryHandler();
          exceptionForm.ViewModel.Exception = exception;
          return exceptionForm.ShowDialogWithOwner();
        }
      }

      private void InitializeRoleBasedSecurity() => RBSClient.InitializeSecurityContext();

      private void LogApplicationRunning()
      {
        string location = this.GetType().Assembly.Location;
        StringBuilder stringBuilder = new StringBuilder(512 /*0x0200*/);
        stringBuilder.AppendLine($"{ClientDiagnosticsConsts.EventLogSourceName} запущен.");
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
        this.eventLogService.DefaultLog.Write($"{ClientDiagnosticsConsts.EventLogSourceName} завершил выполнение.");
      }

      private bool TryInitializeHyperlinkHandler()
      {
        HyperlinkHandler.ExceptionService = this.exceptionService;
        HyperlinkHandler.StartupService = this.iocContainer.Get<IStartupService>();
        HyperlinkHandler.MainFormService = this.iocContainer.Get<Lazy<IMainFormUpdate>>();
        return !HyperlinkHandler.Process((IList<string>) this.Arguments);
      }

      private bool TryStartRemoting()
      {
        new ClientRemotingConfigurator().Configure();
        return true;
      }

      private void RunBeforeDBStageInitializers()
      {
        this.beforeDBStageInitializers = this.iocContainer.Get<LazyInitializerModuleGroup>();
        this.beforeDBStageInitializers.Add<UserConfigurationFromFileModule>();
        this.beforeDBStageInitializers.Add<Module>();
        this.beforeDBStageInitializers.Add<UserSessionPoolModule>();
        this.beforeDBStageInitializers.Add<UserSessionValidatorsModule>();
        this.beforeDBStageInitializers.Add<UserSessionExceptionsModule>();
        this.beforeDBStageInitializers.Initialize();
      }

      private void ShutdownBeforeDBStageInitializers()
      {
        if (this.beforeDBStageInitializers == null)
          return;
        this.beforeDBStageInitializers.Shutdown();
        this.beforeDBStageInitializers = (LazyInitializerModuleGroup) null;
      }

      private void RunAfterDBStageInitializers()
      {
        this.afterDBStageInitializers = this.iocContainer.Get<LazyInitializerModuleGroup>();
        this.afterDBStageInitializers.Add<DBHelpersInitializationModule>();
        this.afterDBStageInitializers.Add<UserConfigurationFromDBModule>();
        this.afterDBStageInitializers.Add<RemotingServicesModule>();
        this.afterDBStageInitializers.Initialize();
      }

      private void ShutdownAfterDBStageInitializers()
      {
        if (this.afterDBStageInitializers == null)
          return;
        this.afterDBStageInitializers.Shutdown();
        this.afterDBStageInitializers = (LazyInitializerModuleGroup) null;
      }

      private bool TryInitializeComServer()
      {
        this.comServer = this.iocContainer.Get<ComServer>();
        this.comServer.HostApplication = (IHostApplication) new DefaultHostApplication();
        this.comServer.ComPluginManager = (ComPluginManager) new ComXmlFilesPluginManager();
        this.comServer.ComObjectFactory = (ComObjectFactory) new ProgramComObjectFactory(this.iocContainer);
        ComHost.Instance = this.comServer;
        ComServerInitializationResult initializationResult = this.comServer.Initialize();
        if (!initializationResult.IsSuccessful)
        {
          if (initializationResult.Exception is ComServerRegistrationException)
            this.ShowComServerRegistrationException((ComServerRegistrationException) initializationResult.Exception);
          else if (initializationResult.Exception is ComServerException)
            this.ShowComServerInternalStartException((ComServerException) initializationResult.Exception);
          else
            this.ShowComServerUnhandledStartException(initializationResult.Exception);
        }
        if (initializationResult.ExitRequested)
          return false;
        if (this.comServer.IsActive)
        {
          if (this.comServer.RunMode == ComServerRunMode.Embedding)
            this.comServerAutoExitHandler = new ProgramAutoExitHandler(this.comServer);
          this.iocContainer.Get<IStartupService>().StartupComplete += (EventHandler) ((s, e) => this.comServer.ActivateMissingComClasses());
        }
        return true;
      }

      private void ShowComServerRegistrationException(ComServerRegistrationException x)
      {
        StringBuilder stringBuilder1 = new StringBuilder(512 /*0x0200*/);
        stringBuilder1.Append(x.Message);
        if (x.Problems.Count != 0)
        {
          if (x.Problems.Count == 1)
          {
            stringBuilder1.Append(' ');
            stringBuilder1.Append(x.Problems[0]);
          }
          else
          {
            stringBuilder1.AppendLine();
            stringBuilder1.AppendLine("Список ошибок:");
            foreach (string problem in x.Problems)
              stringBuilder1.AppendLine("  - " + problem);
          }
        }
        this.eventLogService.DefaultLog.Write(stringBuilder1.ToString(), EventLogItemType.Error);
        if (this.IsInstallerMode())
          return;
        StringBuilder stringBuilder2 = new StringBuilder(512 /*0x0200*/);
        stringBuilder2.Append(x.Message);
        stringBuilder2.Append(' ');
        stringBuilder2.Append(LocalizationHolder.rm.GetString("IMClient_95"));
        int num = (int) MessageBox.Show(stringBuilder2.ToString(), LocalizationHolder.rm.GetString("IMClient_69"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }

      private void ShowComServerInternalStartException(ComServerException x)
      {
        this.eventLogService.DefaultLog.Write(x.Message);
        if (this.IsInstallerMode())
          return;
        int num = (int) MessageBox.Show(x.Message, LocalizationHolder.rm.GetString("IMClient_69"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }

      private void ShowComServerUnhandledStartException(Exception x)
      {
        string preamble = "В процессе конфигурирования поддержки COM-объектов произошла ошибка.";
        this.eventLogService.DefaultLog.Write(ExceptionServices.GetExtendedExceptionText(x, preamble), EventLogItemType.Error);
        if (this.IsInstallerMode())
          return;
        int num = (int) MessageBox.Show($"{preamble} {x.Message}", LocalizationHolder.rm.GetString("IMClient_69"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }

      private bool IsInstallerMode()
      {
        return string.Equals(Environment.GetEnvironmentVariable("IPS_INSTALLER"), "1");
      }

      private void RunMainFormLoop()
      {
        MainFormInitParams initParams = this.iocContainer.Get<MainFormInitParams>();
        MainForm mainForm = this.iocContainer.Get<MainForm>();
        mainForm.Initialize(initParams);
        Application.Run((Form) mainForm);
      }

      private void ConfigurePluginManager(PluginManager pluginManager)
      {
        pluginManager.PackageActivator = this.iocContainer.Get<IPackageActivator>();
        pluginManager.AssemblyResolveFilter = (IAssemblyResolveFilter) new ProgramAssemblyResolveFilter();
      }

      private bool TryCreateProtectionKey()
      {
        int num1 = 348;
        byte[][] numArray = new byte[32 /*0x20*/][]
        {
          new byte[16 /*0x10*/]
          {
            (byte) 59,
            (byte) 66,
            (byte) 27,
            (byte) 48 /*0x30*/,
            (byte) 83,
            (byte) 76,
            (byte) 171,
            byte.MaxValue,
            (byte) 179,
            (byte) 222,
            (byte) 44,
            (byte) 34,
            (byte) 156,
            (byte) 163,
            (byte) 136,
            (byte) 209
          },
          new byte[16 /*0x10*/]
          {
            (byte) 67,
            (byte) 80 /*0x50*/,
            (byte) 85,
            (byte) 254,
            (byte) 176 /*0xB0*/,
            (byte) 191,
            (byte) 172,
            (byte) 127 /*0x7F*/,
            (byte) 90,
            (byte) 98,
            (byte) 240 /*0xF0*/,
            (byte) 200,
            (byte) 200,
            (byte) 222,
            (byte) 93,
            (byte) 59
          },
          new byte[16 /*0x10*/]
          {
            (byte) 114,
            (byte) 80 /*0x50*/,
            (byte) 249,
            (byte) 185,
            (byte) 173,
            (byte) 172,
            (byte) 5,
            (byte) 91,
            (byte) 36,
            (byte) 156,
            (byte) 90,
            (byte) 29,
            (byte) 60,
            (byte) 60,
            (byte) 176 /*0xB0*/,
            (byte) 149
          },
          new byte[16 /*0x10*/]
          {
            (byte) 247,
            (byte) 61,
            (byte) 80 /*0x50*/,
            (byte) 41,
            (byte) 212,
            (byte) 73,
            (byte) 216,
            (byte) 123,
            (byte) 214,
            (byte) 221,
            (byte) 195,
            (byte) 160 /*0xA0*/,
            (byte) 191,
            (byte) 11,
            (byte) 108,
            (byte) 174
          },
          new byte[16 /*0x10*/]
          {
            (byte) 231,
            (byte) 93,
            (byte) 25,
            (byte) 95,
            byte.MaxValue,
            (byte) 202,
            (byte) 21,
            (byte) 151,
            (byte) 133,
            (byte) 67,
            (byte) 49,
            (byte) 156,
            (byte) 224 /*0xE0*/,
            (byte) 89,
            (byte) 185,
            (byte) 211
          },
          new byte[16 /*0x10*/]
          {
            (byte) 198,
            (byte) 211,
            (byte) 148,
            (byte) 94,
            (byte) 154,
            (byte) 151,
            (byte) 124,
            (byte) 9,
            (byte) 9,
            (byte) 234,
            (byte) 226,
            (byte) 79,
            (byte) 29,
            (byte) 247,
            (byte) 42,
            (byte) 204
          },
          new byte[16 /*0x10*/]
          {
            (byte) 36,
            byte.MaxValue,
            (byte) 125,
            (byte) 2,
            (byte) 229,
            (byte) 0,
            (byte) 4,
            (byte) 142,
            (byte) 60,
            (byte) 191,
            (byte) 11,
            (byte) 182,
            (byte) 241,
            (byte) 141,
            (byte) 195,
            (byte) 119
          },
          new byte[16 /*0x10*/]
          {
            (byte) 84,
            (byte) 81,
            (byte) 141,
            (byte) 167,
            (byte) 102,
            (byte) 127 /*0x7F*/,
            (byte) 146,
            (byte) 184,
            byte.MaxValue,
            (byte) 215,
            (byte) 247,
            (byte) 115,
            (byte) 67,
            (byte) 189,
            (byte) 164,
            (byte) 67
          },
          new byte[16 /*0x10*/]
          {
            (byte) 46,
            (byte) 150,
            (byte) 58,
            (byte) 62,
            (byte) 138,
            (byte) 41,
            (byte) 209,
            (byte) 167,
            (byte) 59,
            (byte) 98,
            (byte) 52,
            (byte) 105,
            (byte) 122,
            (byte) 105,
            (byte) 123,
            (byte) 203
          },
          new byte[16 /*0x10*/]
          {
            (byte) 155,
            (byte) 229,
            (byte) 102,
            (byte) 232,
            (byte) 16 /*0x10*/,
            (byte) 196,
            (byte) 185,
            (byte) 96 /*0x60*/,
            (byte) 6,
            (byte) 221,
            (byte) 107,
            (byte) 156,
            (byte) 138,
            (byte) 177,
            (byte) 143,
            (byte) 73
          },
          new byte[16 /*0x10*/]
          {
            (byte) 227,
            (byte) 134,
            (byte) 248,
            (byte) 60,
            (byte) 190,
            (byte) 173,
            (byte) 175,
            (byte) 231,
            (byte) 88,
            (byte) 138,
            (byte) 35,
            (byte) 64 /*0x40*/,
            (byte) 190,
            (byte) 94,
            (byte) 165,
            (byte) 247
          },
          new byte[16 /*0x10*/]
          {
            (byte) 100,
            (byte) 169,
            (byte) 86,
            (byte) 22,
            (byte) 65,
            (byte) 201,
            (byte) 162,
            (byte) 154,
            (byte) 67,
            (byte) 235,
            (byte) 242,
            (byte) 194,
            (byte) 232,
            (byte) 75,
            (byte) 35,
            (byte) 192 /*0xC0*/
          },
          new byte[16 /*0x10*/]
          {
            (byte) 128 /*0x80*/,
            (byte) 186,
            (byte) 109,
            (byte) 73,
            (byte) 31 /*0x1F*/,
            (byte) 122,
            (byte) 73,
            (byte) 251,
            (byte) 46,
            (byte) 214,
            (byte) 199,
            (byte) 130,
            (byte) 204,
            (byte) 155,
            (byte) 19,
            (byte) 210
          },
          new byte[16 /*0x10*/]
          {
            (byte) 7,
            (byte) 0,
            (byte) 135,
            (byte) 70,
            (byte) 90,
            (byte) 250,
            (byte) 81,
            (byte) 190,
            (byte) 195,
            (byte) 189,
            (byte) 220,
            (byte) 51,
            (byte) 33,
            (byte) 202,
            (byte) 31 /*0x1F*/,
            (byte) 92
          },
          new byte[16 /*0x10*/]
          {
            (byte) 68,
            (byte) 64 /*0x40*/,
            (byte) 211,
            (byte) 107,
            (byte) 149,
            (byte) 135,
            (byte) 185,
            (byte) 128 /*0x80*/,
            (byte) 230,
            (byte) 82,
            (byte) 230,
            byte.MaxValue,
            (byte) 5,
            (byte) 145,
            (byte) 232,
            (byte) 220
          },
          new byte[16 /*0x10*/]
          {
            (byte) 222,
            (byte) 56,
            (byte) 140,
            (byte) 27,
            (byte) 110,
            (byte) 84,
            (byte) 167,
            (byte) 66,
            (byte) 33,
            (byte) 185,
            (byte) 73,
            (byte) 247,
            (byte) 132,
            (byte) 151,
            (byte) 161,
            (byte) 127 /*0x7F*/
          },
          new byte[16 /*0x10*/]
          {
            (byte) 189,
            (byte) 60,
            (byte) 233,
            (byte) 192 /*0xC0*/,
            (byte) 252,
            (byte) 49,
            (byte) 171,
            (byte) 104,
            (byte) 60,
            (byte) 252,
            (byte) 14,
            (byte) 231,
            (byte) 230,
            (byte) 45,
            (byte) 26,
            (byte) 189
          },
          new byte[16 /*0x10*/]
          {
            (byte) 218,
            (byte) 126,
            (byte) 98,
            (byte) 217,
            (byte) 38,
            (byte) 72,
            (byte) 155,
            (byte) 115,
            (byte) 137,
            (byte) 222,
            (byte) 107,
            (byte) 85,
            (byte) 234,
            (byte) 99,
            (byte) 96 /*0x60*/,
            (byte) 130
          },
          new byte[16 /*0x10*/]
          {
            (byte) 119,
            (byte) 81,
            (byte) 251,
            (byte) 126,
            (byte) 138,
            (byte) 58,
            (byte) 98,
            (byte) 249,
            (byte) 0,
            (byte) 237,
            (byte) 98,
            (byte) 204,
            (byte) 147,
            (byte) 74,
            (byte) 245,
            (byte) 156
          },
          new byte[16 /*0x10*/]
          {
            (byte) 220,
            (byte) 172,
            (byte) 33,
            (byte) 188,
            (byte) 106,
            (byte) 88,
            (byte) 91,
            (byte) 12,
            (byte) 40,
            (byte) 162,
            (byte) 177,
            (byte) 244,
            (byte) 90,
            (byte) 248,
            (byte) 22,
            (byte) 135
          },
          new byte[16 /*0x10*/]
          {
            (byte) 237,
            (byte) 234,
            (byte) 197,
            (byte) 144 /*0x90*/,
            (byte) 177,
            (byte) 82,
            (byte) 213,
            (byte) 19,
            (byte) 148,
            (byte) 107,
            (byte) 46,
            (byte) 152,
            (byte) 121,
            (byte) 209,
            (byte) 153,
            (byte) 185
          },
          new byte[16 /*0x10*/]
          {
            (byte) 236,
            (byte) 184,
            (byte) 12,
            (byte) 8,
            (byte) 126,
            (byte) 250,
            (byte) 91,
            (byte) 100,
            (byte) 173,
            (byte) 76,
            (byte) 74,
            (byte) 123,
            (byte) 45,
            (byte) 110,
            (byte) 216,
            (byte) 124
          },
          new byte[16 /*0x10*/]
          {
            (byte) 56,
            (byte) 41,
            (byte) 136,
            (byte) 180,
            (byte) 150,
            (byte) 106,
            (byte) 81,
            (byte) 131,
            (byte) 217,
            (byte) 240 /*0xF0*/,
            (byte) 192 /*0xC0*/,
            (byte) 15,
            (byte) 206,
            (byte) 223,
            (byte) 213,
            (byte) 242
          },
          new byte[16 /*0x10*/]
          {
            (byte) 88,
            (byte) 72,
            (byte) 234,
            (byte) 222,
            (byte) 49,
            (byte) 2,
            (byte) 121,
            (byte) 152,
            (byte) 80 /*0x50*/,
            (byte) 102,
            (byte) 219,
            (byte) 195,
            (byte) 136,
            (byte) 169,
            (byte) 212,
            (byte) 1
          },
          new byte[16 /*0x10*/]
          {
            (byte) 7,
            (byte) 113,
            (byte) 249,
            (byte) 251,
            (byte) 103,
            (byte) 138,
            (byte) 11,
            (byte) 36,
            (byte) 123,
            (byte) 232,
            (byte) 233,
            (byte) 31 /*0x1F*/,
            (byte) 11,
            (byte) 202,
            (byte) 127 /*0x7F*/,
            (byte) 46
          },
          new byte[16 /*0x10*/]
          {
            (byte) 16 /*0x10*/,
            (byte) 186,
            (byte) 166,
            (byte) 226,
            (byte) 214,
            (byte) 202,
            (byte) 79,
            (byte) 1,
            (byte) 214,
            (byte) 163,
            (byte) 221,
            (byte) 167,
            (byte) 28,
            (byte) 6,
            (byte) 131,
            (byte) 148
          },
          new byte[16 /*0x10*/]
          {
            (byte) 83,
            (byte) 169,
            (byte) 148,
            (byte) 106,
            (byte) 194,
            (byte) 64 /*0x40*/,
            (byte) 6,
            (byte) 131,
            (byte) 152,
            (byte) 66,
            (byte) 182,
            (byte) 193,
            (byte) 146,
            (byte) 198,
            (byte) 69,
            (byte) 181
          },
          new byte[16 /*0x10*/]
          {
            (byte) 155,
            (byte) 121,
            (byte) 72,
            (byte) 10,
            (byte) 209,
            (byte) 152,
            (byte) 136,
            (byte) 141,
            (byte) 193,
            (byte) 125,
            (byte) 223,
            (byte) 111,
            (byte) 7,
            (byte) 166,
            (byte) 194,
            (byte) 26
          },
          new byte[16 /*0x10*/]
          {
            (byte) 150,
            (byte) 59,
            (byte) 145,
            (byte) 7,
            (byte) 216,
            (byte) 157,
            (byte) 81,
            (byte) 173,
            (byte) 30,
            (byte) 99,
            (byte) 57,
            (byte) 174,
            (byte) 129,
            (byte) 50,
            (byte) 119,
            (byte) 15
          },
          new byte[16 /*0x10*/]
          {
            (byte) 142,
            (byte) 245,
            (byte) 126,
            (byte) 123,
            (byte) 188,
            (byte) 85,
            (byte) 6,
            (byte) 97,
            (byte) 218,
            (byte) 144 /*0x90*/,
            (byte) 186,
            (byte) 166,
            (byte) 103,
            (byte) 165,
            (byte) 11,
            (byte) 42
          },
          new byte[16 /*0x10*/]
          {
            (byte) 112 /*0x70*/,
            (byte) 145,
            (byte) 187,
            (byte) 37,
            (byte) 75,
            (byte) 107,
            (byte) 48 /*0x30*/,
            (byte) 198,
            (byte) 106,
            (byte) 118,
            (byte) 98,
            (byte) 37,
            (byte) 212,
            (byte) 73,
            (byte) 146,
            (byte) 180
          },
          new byte[16 /*0x10*/]
          {
            (byte) 157,
            (byte) 250,
            (byte) 166,
            (byte) 133,
            (byte) 54,
            (byte) 166,
            (byte) 80 /*0x50*/,
            (byte) 248,
            (byte) 26,
            (byte) 177,
            (byte) 115,
            (byte) 29,
            (byte) 95,
            (byte) 201,
            (byte) 74,
            (byte) 76
          }
        };
        int index = (Environment.TickCount & 15) * 2;
        byte[] query = numArray[index];
        byte[] reply = numArray[index + 1];
        ProtectionService.Provider = (System.IServiceProvider) ServicesManager.ServiceContainer;
        ProtectionService.HasUI = true;
        ProtectionService.sw += new je(this.ProtectionService_AskUser);
        ProtectionService.f6 += new nq(this.ProtectionService_Authorize);
        try
        {
          try
          {
            if (ProtectionKeyBase.IsTerminal())
            {
              this.protectionKey = (IProtectionKey) null;
              this.eventLogService.DefaultLog.Write("Невозможно использовать локальный ключ в терминальной сессии.");
            }
            else
            {
              this.protectionKey = (IProtectionKey) new LocalKey(num1, query, reply);
              this.eventLogService.DefaultLog.Write("Используется локальный ключ.");
            }
          }
          catch (Exception ex)
          {
            if (ex is LocalKeyOutOfDateException)
            {
              int num2 = (int) MessageBox.Show(ex.Message, LocalizationHolder.rm.GetString("IMClient_96"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (ex is CriticalProtectionException)
            {
              int num3 = (int) MessageBox.Show(ex.Message, LocalizationHolder.rm.GetString("IMClient_96"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
          }
          if (this.protectionKey == null)
          {
            IMServerService service = ServicesManager.GetService(typeof (IMServerService)) as IMServerService;
            NetworkKey.SetSpareServers(service.GetAppConfigurationService().GetConfigurationOption("Protection.SpareServers"));
            NetworkKey.SetInformAdmins(service.GetAppConfigurationService().GetConfigurationOption("Protection.InformAdmins"));
            this.protectionKey = (IProtectionKey) new NetworkKey(num1, query, reply);
            this.eventLogService.DefaultLog.Write("Используется сетевой ключ.");
          }
          ServicesManager.AddService(typeof (IProtectionKey), (object) this.protectionKey);
          ServicesManager.AddService(typeof (ILicenser), (object) this.protectionKey);
          return true;
        }
        catch (KeyException ex)
        {
          string message = ex.Message;
          string defferedExceptionsText = NetworkKey.DefferedExceptionsText;
          if (!string.IsNullOrEmpty(defferedExceptionsText))
          {
            string str = message + Environment.NewLine + defferedExceptionsText;
          }
          int num4 = (int) MessageBox.Show(defferedExceptionsText, LocalizationHolder.rm.GetString("IMClient_69"));
          return false;
        }
        catch (ProtectionException ex)
        {
          string text = ex.Message;
          string defferedExceptionsText = NetworkKey.DefferedExceptionsText;
          if (!string.IsNullOrEmpty(defferedExceptionsText))
            text = text + Environment.NewLine + defferedExceptionsText;
          int num5 = (int) MessageBox.Show(text, LocalizationHolder.rm.GetString("IMClient_69"));
          return false;
        }
      }

      private void RemoveProtectionKey()
      {
        if (this.protectionKey == null)
          return;
        ProtectionService.sw -= new je(this.ProtectionService_AskUser);
        ServicesManager.RemoveService(typeof (IProtectionKey));
        ServicesManager.RemoveService(typeof (ILicenser));
        this.InvokeSilently((Action) (() => this.protectionKey.Dispose()), "this.protectionKey.Dispose()");
        this.protectionKey = (IProtectionKey) null;
      }

      private string ProtectionService_Authorize(int daysLeft, string licenseText, ref bool cancel)
      {
        return AuthorizeForm.ShowDialog(daysLeft, licenseText, ref cancel);
      }

      private bool ProtectionService_AskUser(string question, string caption)
      {
        return MessageBox.Show(question, caption, MessageBoxButtons.YesNo) == DialogResult.Yes;
      }

      private bool TryStartLocalConsoleServerIfNeeded()
      {
        try
        {
          string str1 = System.Configuration.ConfigurationManager.AppSettings["ConsoleServerExePath"]?.Trim();
          if (string.IsNullOrEmpty(str1))
            return true;
          Process[] processesByName = Process.GetProcessesByName("ConsoleServer");
          using (Process process1 = new Process())
          {
            string str2 = str1;
            if (!Path.IsPathRooted(str1))
              str2 = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, str1));
            if (processesByName.Length != 0)
            {
              foreach (Process process2 in processesByName)
              {
                if (process2.MainModule != null && PathUtils.IsSamePath(process2.MainModule.FileName, str2))
                  return true;
              }
            }
            process1.StartInfo = File.Exists(str2) ? new ProcessStartInfo(str2) : throw new Exception($"Не удалось запустить локальный сервер приложений IPS. Заданный в настройках файл \"{str2}\" не найден, исправьте ошибку и повторите запуск.");
            return process1.Start();
          }
        }
        catch (Exception ex)
        {
          this.exceptionService.ShowException(ex);
          return false;
        }
      }
    }
}
