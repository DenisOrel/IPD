
// Type: Intermech.Client.Specialized.ClientApplicationLifecycleHandler
// Assembly: Intermech.Client.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C6CEDFE2-45F7-4A85-9CFB-4D0105C0197F
:\IPS\Client\Intermech.Client.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Base.xml

using Intermech.ApplicationModel;
using Intermech.ApplicationModel.NinjectIntegration;
using Intermech.Client.Scripting;
using Intermech.Diagnostics;
using Intermech.Globalization;
using Intermech.Interfaces;
using Intermech.Interfaces.Caches.Metadata;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data.Metadata;
using Intermech.Runtime;
using Intermech.Security;
using Intermech.Services;
using Ninject;
using Ninject.Activation;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using System;
using System.Diagnostics;


namespace Intermech.Client.Specialized
{
    /// <summary>
    /// Обработчик основных этапов в жизненном цикле специализированного клиента IPS.
    /// </summary>
    public class ClientApplicationLifecycleHandler
    {
      private IClientApplicationHost host;
      private bool isInitialized;
      /// <summary>Признак инициализации remoting</summary>
      private static bool isRemotingConfigured;
      private StandardKernel iocContainer;
      private LazyInitializerModuleGroup initializerModules;
      private ApplicationStateService applicationStateService;

      /// <summary>Создает объект.</summary>
      /// <param name="host">Объект интеграции с хост-приложением специализированного клиента IPS</param>
      public ClientApplicationLifecycleHandler(IClientApplicationHost host)
      {
        if (host == null)
          throw new ArgumentNullException(nameof (host));
        ClientApplicationLifecycleHandler.CheckHost(host);
        this.host = host;
      }

      private static void CheckHost(IClientApplicationHost host)
      {
        if (host.LoginInfoProvider == null)
          throw PropertyExceptions.PropertyNotSetException((object) host, "LoginInfoProvider");
      }

      /// <summary>
      /// Возвращает объект интеграции с хост-приложением специализированного клиента IPS.
      /// </summary>
      public IClientApplicationHost Host
      {
        [DebuggerStepThrough] get => this.host;
      }

      /// <summary>
      /// Выполняет начальную инициализацию клиентского приложения.
      /// </summary>
      /// <exception cref="T:InvalidOperationException">Инициализация уже была выполнена</exception>
      public void Initialize()
      {
        if (this.isInitialized)
          throw new InvalidOperationException("Приложение уже было инициализировано.");
        try
        {
          this.DoInitialize();
        }
        catch
        {
          this.DoShutdown(true);
          throw;
        }
        this.isInitialized = true;
      }

      /// <summary>
      /// Выполняет начальную инициализацию клиентского приложения.
      /// </summary>
      protected virtual void DoInitialize()
      {
        UICultureHelper.ApplySettingsFromConfigurationFile();
        this.CreateIOCContainer();
        this.InitializeProgramServices();
        this.InitializeExceptionHandlers();
        this.InitializeRoleBasedSecurity();
        this.InitializeRemoting();
        this.initializerModules = this.iocContainer.Get<LazyInitializerModuleGroup>();
        this.initializerModules.Add<UserSessionPoolModule>();
        this.initializerModules.Add<UserSessionValidatorsModule>();
        this.initializerModules.Add<UserSessionExceptionsModule>();
        this.initializerModules.Add<DBHelpersInitializationModule>();
        this.initializerModules.Initialize();
      }

      private void CreateIOCContainer()
      {
        this.iocContainer = new StandardKernel(Array.Empty<INinjectModule>());
        this.iocContainer.Load((INinjectModule) new MainApplicationNinjectModule());
        if (!this.iocContainer.HasModule(typeof (FuncModule).FullName))
          this.iocContainer.Load((INinjectModule) new FuncModule());
        ApplicationServices.Container.ServiceResolver = this.iocContainer.Get<IApplicationServiceResolver>();
        this.iocContainer.Bind<StackTraceBuilder>().To<IPSStackTraceBuilder>();
        this.iocContainer.Bind<IApplicationStateEventsService, ApplicationStateService>().To<ApplicationStateService>().InSingletonScope();
        this.iocContainer.Bind<IMetadataChangeMonitor>().To<EmptyMetadataChangeMonitor>().InSingletonScope();
        this.iocContainer.Bind<MetadataResolverFactory>().ToSelf().InSingletonScope();
        this.iocContainer.Bind<IMServerService>().ToSelf().InSingletonScope();
        this.iocContainer.Load((INinjectModule) new MetaDataHelperNinjectModule());
        this.iocContainer.Load((INinjectModule) new ClientCacheNinjectModule());
        this.iocContainer.Load((INinjectModule) new ClientMetadataCacheNinjectModule());
        this.iocContainer.Load((INinjectModule) new ClientSessionSpeedupServicesNinjectModule());
        this.iocContainer.Bind<ICustomServicesSpeedupService>().To<CustomServicesSpeedupService>().InSingletonScope();
        this.iocContainer.Bind<SimpleSessionPool, IUserSessionPool>().To<SimpleSessionPool>().InSingletonScope().WithConstructorArgument<Func<SimpleSessionPoolLoginInfo>>(this.Host.LoginInfoProvider).WithPropertyValue("OptionalServices", (Func<IContext, object>) (context => (object) context.Kernel.Get<SessionPoolOptionalServices>())).WithPropertyValue("SpeedupServices", (Func<IContext, object>) (context => (object) context.Kernel.Get<UserSessionSpeedupServices>()));
        this.iocContainer.Bind<IUserSessionLoginService>().To<UserSessionLoginService>().InSingletonScope();
        this.iocContainer.Bind<IServerEventLogService>().To<ServerEventLogService>().InSingletonScope();
        this.iocContainer.Load((INinjectModule) new CSharpScriptsNinjectModule());
        this.iocContainer.Bind<UserSessionPoolModule>().ToSelf();
        this.iocContainer.Bind<UserSessionValidatorsModule>().ToSelf();
        this.iocContainer.Bind<UserSessionExceptionsReporter>().ToSelf();
        this.iocContainer.Bind<UserSessionExceptionsModule>().ToSelf();
        this.iocContainer.Bind<DBHelpersInitializationModule>().ToSelf();
      }

      private void InitializeProgramServices()
      {
        this.applicationStateService = this.iocContainer.Get<ApplicationStateService>();
      }

      private void InitializeExceptionHandlers()
      {
        ExceptionServices.StackTraceBuilderFactory = this.iocContainer.Get<Func<StackTraceBuilder>>();
      }

      private void InitializeRoleBasedSecurity() => RBSClient.InitializeSecurityContext();

      private void InitializeRemoting()
      {
        if (ClientApplicationLifecycleHandler.isRemotingConfigured)
          return;
        new ClientRemotingConfigurator().Configure();
        ClientApplicationLifecycleHandler.isRemotingConfigured = true;
      }

      /// <summary>
      /// Освобождает ресурсы и завершает жизненный цикл клиентского приложения.
      /// </summary>
      public void Shutdown()
      {
        if (!this.isInitialized)
          throw new InvalidOperationException("Приложение не было инициализировано.");
        this.DoShutdown(false);
        this.isInitialized = false;
      }

      /// <summary>
      /// Освобождает ресурсы и завершает жизненный цикл клиентского приложения.
      /// </summary>
      /// <param name="errorMode">Признак, что метод был вызван после ошибки инициализации текущего объекта</param>
      protected virtual void DoShutdown(bool errorMode)
      {
        if (this.applicationStateService != null)
        {
          this.applicationStateService.RaiseExit();
          this.applicationStateService = (ApplicationStateService) null;
        }
        if (this.initializerModules != null)
        {
          this.initializerModules.Shutdown();
          this.initializerModules = (LazyInitializerModuleGroup) null;
        }
        if (this.iocContainer == null)
          return;
        this.iocContainer.Dispose();
        this.iocContainer = (StandardKernel) null;
      }

      private void OnUpdateRoleBasedSecurityContext(object sender, UserSessionCreatedEventArgs e)
      {
        RBSClient.UpdateSecurityContext(e.Session);
      }
    }
}
