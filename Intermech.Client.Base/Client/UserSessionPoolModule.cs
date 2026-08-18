using Intermech.ApplicationModel;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data.Metadata;
using Intermech.Remoting.Optimized;
using Intermech.Security;
using System;


namespace Intermech.Client
{
    /// <summary>
    /// Модуль инициализации пула сессий сервера приложений и распределителя клиентских сессий.
    /// </summary>
    public sealed class UserSessionPoolModule : InitializerModule
    {
      private IUserSessionPool userSessionPool;
      private IMetadataChangeMonitor metadataChangeMonitor;
      private MetadataResolverFactory metadataResolversFactory;
      private IServerEventLogService serverEventLogService;

      public UserSessionPoolModule(
        IUserSessionPool userSessionPool,
        IMetadataChangeMonitor metadataChangeMonitor,
        MetadataResolverFactory metadataResolversFactory,
        IServerEventLogService serverEventLogService)
      {
        if (userSessionPool == null)
          throw new ArgumentNullException(nameof (userSessionPool));
        if (metadataChangeMonitor == null)
          throw new ArgumentNullException(nameof (metadataChangeMonitor));
        if (metadataResolversFactory == null)
          throw new ArgumentNullException(nameof (metadataResolversFactory));
        if (serverEventLogService == null)
          throw new ArgumentNullException(nameof (serverEventLogService));
        this.userSessionPool = userSessionPool;
        this.metadataChangeMonitor = metadataChangeMonitor;
        this.metadataResolversFactory = metadataResolversFactory;
        this.serverEventLogService = serverEventLogService;
      }

      /// <summary>
      /// Выполняет инициализацию объектов и сервисов, предоставляемых модулем.
      /// </summary>
      protected override void DoInitialize()
      {
        base.DoInitialize();
        this.userSessionPool.MainSessionCreated += new EventHandler<UserSessionCreatedEventArgs>(this.OnUpdateRoleBasedSecurityContext);
        SessionKeeper.InitializeAllocator((IUserSessionAllocator) this.userSessionPool);
        ClientRemotingDynamicSettings.Instance.FormatterSinkInterceptorFactory = new Func<IClientFormatterSinkInterceptor>(this.CreateUserSessionLostInterceptor);
        if (UserSessionGuardServices.IsEnabled)
          SessionKeeper.EnableSessionGuard();
        MetadataResolvers.ChangeMonitor = this.metadataChangeMonitor;
        MetadataResolvers.Factory = this.metadataResolversFactory;
      }

      /// <summary>
      /// Завершает работу объектов и сервисов, предоставленных модулем.
      /// Если свойство модуля IsInitialized возвращает false, то DoShutdown вызван как реакция на необработанное исключение при инициализации модуля.
      /// </summary>
      protected override void DoShutdown()
      {
        MetadataResolvers.ChangeMonitor = (IMetadataChangeMonitor) null;
        MetadataResolvers.Factory = (MetadataResolverFactory) null;
        if (SessionKeeper.CurrentAllocator != null)
        {
          SessionKeeper.CurrentAllocator.Shutdown();
          ClientRemotingDynamicSettings.Instance.FormatterSinkInterceptorFactory = (Func<IClientFormatterSinkInterceptor>) null;
        }
        base.DoShutdown();
      }

      private void OnUpdateRoleBasedSecurityContext(object sender, UserSessionCreatedEventArgs e)
      {
        RBSClient.UpdateSecurityContext(e.Session);
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
        if (string.IsNullOrEmpty(text))
          return;
        this.serverEventLogService.AddToTrace(text, Consts.traceAlways, "network_errors.log");
      }
    }
}
