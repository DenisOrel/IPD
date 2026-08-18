using Intermech.ApplicationModel;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Diagnostics;


namespace Intermech.Client
{
    /// <summary>
    /// Модуль включения/отключения валидации состояния пользовательских сессий сервера приложений.
    /// Валидация позволяет защититься от наиболее распространенных программистских ошибок - незакрытых транзакций, невыключенных кэшей,
    /// невыключенных специальных режимов работы и т.д.
    /// </summary>
    public sealed class UserSessionValidatorsModule : InitializerModule
    {
      private IUserSessionPool userSessionPool;
      private SessionValidationManager validationManager;
      private IMServerService imserverService;
      private IServerEventLogService serverEventLog;

      /// <summary>Создает объект.</summary>
      /// <param name="imserverService">Сервис доступа к главному объекту сервера приложений IPS</param>
      /// <param name="userSessionPool">Пул сессий сервера приложений IPS</param>
      /// <param name="serverEventLog">Сервис для записи сообщений в лог-файлы сервера приложений</param>
      public UserSessionValidatorsModule(
        IMServerService imserverService,
        IUserSessionPool userSessionPool,
        IServerEventLogService serverEventLog)
      {
        if (imserverService == null)
          throw new ArgumentNullException(nameof (imserverService));
        if (userSessionPool == null)
          throw new ArgumentNullException(nameof (userSessionPool));
        if (serverEventLog == null)
          throw new ArgumentNullException(nameof (serverEventLog));
        this.userSessionPool = userSessionPool;
        this.validationManager = SessionKeeper.Validators;
        this.imserverService = imserverService;
        this.serverEventLog = serverEventLog;
      }

      /// <summary>
      /// Выполняет инициализацию объектов и сервисов, предоставляемых модулем.
      /// </summary>
      protected override void DoInitialize()
      {
        base.DoInitialize();
        this.userSessionPool.MainSessionCreated += new EventHandler<UserSessionCreatedEventArgs>(this.OnMainSessionCreated);
        this.imserverService.ConnectionLost += new EventHandler(this.OnIMServerLost);
      }

      /// <summary>
      /// Завершает работу объектов и сервисов, предоставленных модулем.
      /// Если свойство модуля IsInitialized возвращает false, то DoShutdown вызван как реакция на необработанное исключение при инициализации модуля.
      /// </summary>
      protected override void DoShutdown()
      {
        base.DoShutdown();
        this.userSessionPool.MainSessionCreated -= new EventHandler<UserSessionCreatedEventArgs>(this.OnMainSessionCreated);
        this.imserverService.ConnectionLost -= new EventHandler(this.OnIMServerLost);
      }

      private void OnMainSessionCreated(object sender, UserSessionCreatedEventArgs e)
      {
        if (this.imserverService.GetAppConfigurationService().GetTraceSwitch("UserSession.CheckForForgottenTransactions") == TraceLevel.Off)
          return;
        this.validationManager.BeforeReleaseSessionToPool.Add(new Func<SessionValidator>(this.ForgottenTransactionsSessionValidatorFactory));
      }

      private void OnIMServerLost(object sender, EventArgs e)
      {
        this.validationManager.BeforeReleaseSessionToPool.Remove(new Func<SessionValidator>(this.ForgottenTransactionsSessionValidatorFactory));
      }

      private ForgottenTransactionsSessionValidator ForgottenTransactionsSessionValidatorFactory()
      {
        return new ForgottenTransactionsSessionValidator(this.serverEventLog);
      }
    }
}
