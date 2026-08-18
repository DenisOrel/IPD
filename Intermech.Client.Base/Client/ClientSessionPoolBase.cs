
// Type: Intermech.Client.ClientSessionPoolBase
// Assembly: Intermech.Client.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C6CEDFE2-45F7-4A85-9CFB-4D0105C0197F
:\IPS\Client\Intermech.Client.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Base.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Runtime;
using System;
using System.Diagnostics;
using System.Threading;


namespace Intermech.Client
{
    public abstract class ClientSessionPoolBase : 
      SessionPoolBase,
      IUserSessionPool,
      IUserSessionAllocator
    {
      private IMServerService imserverService;
      private IUserSession cachedMainSession;
      private IUserSessionLoginInfo cachedReadonlyLoginInfo;

      /// <summary>Создает объект.</summary>
      /// <param name="imserverService">Сервис доступа к главному объекту сервера приложений IPS</param>
      /// <param name="clientCacheService">Сервис клиентского кэша метаданных для сессий сервера приложений</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="imserverService" /> не должен быть равен null; параметр <paramref name="clientCacheService" /> не должен быть равен null</exception>
      public ClientSessionPoolBase(IMServerService imserverService, IClientCache clientCacheService)
        : base(clientCacheService)
      {
        this.imserverService = imserverService != null ? imserverService : throw new ArgumentNullException(nameof (imserverService));
      }

      /// <summary>
      /// Возвращает сервис доступа к главному объекту сервера приложений IPS.
      /// </summary>
      protected IMServerService IMServerService
      {
        [DebuggerStepThrough] get => this.imserverService;
      }

      /// <summary>
      /// Создает и возвращает основную сессию сервера приложений.
      /// </summary>
      /// <returns>Объект сессии</returns>
      protected sealed override IUserSession CreateMainSession()
      {
        Tuple<IUserSession, UserSessionLoginInfo> loginMainSession = this.CreateAndLoginMainSession();
        IUserSession mainSession = loginMainSession.Item1;
        UserSessionLoginInfo sessionLoginInfo = loginMainSession.Item2;
        try
        {
          this.RaiseMainSessionCreated(mainSession, sessionLoginInfo);
        }
        catch
        {
          SilentActionInvoker.Default.Invoke((Action) (() => mainSession.Logout("DefaultMainClientSession")));
          throw;
        }
        this.cachedMainSession = mainSession;
        this.cachedReadonlyLoginInfo = (IUserSessionLoginInfo) this.CopyLoginInfo(sessionLoginInfo);
        return mainSession;
      }

      protected UserSessionLoginInfo CopyLoginInfo(UserSessionLoginInfo loginInfo)
      {
        UserSessionLoginInfo sessionLoginInfo = new UserSessionLoginInfo();
        sessionLoginInfo.Assign(loginInfo);
        return sessionLoginInfo;
      }

      /// <summary>
      /// Создает основную сессию сервера приложений и выполняет вход пользователя.
      /// </summary>
      /// <returns>Объект сессии и информация о пользователе</returns>
      protected abstract Tuple<IUserSession, UserSessionLoginInfo> CreateAndLoginMainSession();

      /// <summary>
      /// Создает дополнительную сессию сервера приложений путем клонирования основной сессии.
      /// </summary>
      /// <returns>Объект сессии</returns>
      protected sealed override IUserSession CloneMainSession()
      {
        return this.cachedMainSession.Clone("DefaultMainClientSession");
      }

      /// <summary>
      /// Создает ключ для текущего потока (thread). Ключ будет использоваться пулом сессий для ассоциирования текущего потока и выделенной ему сессии.
      /// </summary>
      /// <returns>Ключ текущего потока</returns>
      protected sealed override SessionPoolThreadKey CreateCurrentThreadKey()
      {
        return new SessionPoolThreadKey(Thread.CurrentThread.ManagedThreadId, SessionPoolVars.ControlFlowId.Value);
      }

      /// <summary>
      /// Проверяет работоспособность подключения к серверу приложений.
      /// Метод должен работать максимально быстро, так как вызывается очень часто.
      /// </summary>
      /// <returns>Результат проверки</returns>
      protected override bool TestConnection()
      {
        return this.cachedMainSession != null ? this.IMServerService.TestConnection((object) this.cachedMainSession) : this.IMServerService.TestConnection();
      }

      /// <summary>
      /// Очищает все кэшированные сервисом данные, полученные от сервера приложений.
      /// Метод используется при обрыве подключения к серверу приложений.
      /// </summary>
      protected override void ResetConnectionData()
      {
        base.ResetConnectionData();
        this.IMServerService.ResetConnection();
        this.ResetMainSessionData();
        this.RaiseMainSessionLost();
      }

      /// <summary>
      /// Очищает все кэшированные ресурсы, связанные с основной сессией сервера приложений.
      /// Метод используется при обрыве подключения к серверу приложений.
      /// </summary>
      protected virtual void ResetMainSessionData()
      {
        this.cachedMainSession = (IUserSession) null;
        this.cachedReadonlyLoginInfo = (IUserSessionLoginInfo) null;
      }

      private void RaiseMainSessionCreated(
        IUserSession mainSession,
        UserSessionLoginInfo mainSessionLoginInfo)
      {
        EventHandler<UserSessionCreatedEventArgs> mainSessionCreated = this.MainSessionCreated;
        if (mainSessionCreated == null)
          return;
        UserSessionCreatedEventArgs e = new UserSessionCreatedEventArgs(this.IMServerService.ServerObject, mainSession, (IUserSessionLoginInfo) mainSessionLoginInfo);
        mainSessionCreated((object) this, e);
      }

      private void RaiseMainSessionLost()
      {
        EventHandler mainSessionLost = this.MainSessionLost;
        if (mainSessionLost == null)
          return;
        mainSessionLost((object) this, EventArgs.Empty);
      }

      /// <summary>
      /// Возвращает параметры входа пользователя на сервер приложений, использованные для создания основной сессии.
      /// </summary>
      /// <returns>Параметры входа пользователя или null, если основая сессия еще не создна или была закрыта</returns>
      public IUserSessionLoginInfo TryGetMainSessionLoginInfo()
      {
        lock (this.SyncRoot)
          return this.cachedReadonlyLoginInfo;
      }

      /// <summary>
      /// Событие, появляющееся после успешного создания основной сессии.
      /// </summary>
      public event EventHandler<UserSessionCreatedEventArgs> MainSessionCreated;

      /// <summary>
      /// Событие, появляющееся после обрыва подключения к серверу приложений и потери основной сессии.
      /// </summary>
      public event EventHandler MainSessionLost;
    }
}
