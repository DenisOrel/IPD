// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.SessionPoolBase
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Threading;
using System;
using System.Diagnostics;
using System.Threading;

#nullable disable
namespace Intermech.Interfaces.Client;

public abstract class SessionPoolBase : IUserSessionAllocator
{
  private object _syncRoot;
  private IClientCache _clientCache;
  private AtomicRef<SessionPoolOptionalServices> _optionalServices;
  private AtomicRef<UserSessionSpeedupServices> _speedupServices;
  private IMServerConnectionErrorReporter _connectionErrorReporter;
  private ActiveSessionsCollection _activeSessionsPool;
  private InactiveSessionsCollection _inactiveSessionsPool;
  private bool _poolInitialized;
  private IUserSession _mainSession;
  private Timer _lazyCleanupTimer;
  private DateTime _lastCheckConnectionTime;
  /// <summary>
  /// Интервал времени между периодическими чистками пула сессий.
  /// </summary>
  private static readonly TimeSpan CleanupPeriod = TimeSpan.FromHours(1.0);
  /// <summary>
  /// Интервал периодической проверки обрыва подключения к серверу приложений.
  /// </summary>
  private static readonly TimeSpan CheckConnectionPeriod = TimeSpan.FromSeconds(1.0);

  /// <summary>Создает объект.</summary>
  /// <param name="clientCacheService">Сервис клиентского кэша метаданных для сессий сервера приложений</param>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="clientCacheService" /> не должен быть равен null</exception>
  protected SessionPoolBase(IClientCache clientCacheService)
  {
    if (clientCacheService == null)
      throw new ArgumentNullException(nameof (clientCacheService));
    this._syncRoot = new object();
    this._clientCache = clientCacheService;
    this._optionalServices = new AtomicRef<SessionPoolOptionalServices>(new SessionPoolOptionalServices());
    this._speedupServices = new AtomicRef<UserSessionSpeedupServices>(new UserSessionSpeedupServices());
    this._connectionErrorReporter = new IMServerConnectionErrorReporter();
    this._activeSessionsPool = new ActiveSessionsCollection(16 /*0x10*/);
    this._inactiveSessionsPool = new InactiveSessionsCollection(16 /*0x10*/);
    this._lastCheckConnectionTime = DateTime.MinValue;
  }

  /// <summary>
  /// Возвращает или задает объект для вывода диагностических сообщений.
  /// Значение свойства не может быть null.
  /// </summary>
  public IMServerConnectionErrorReporter ConnectionErrorReporter
  {
    [DebuggerStepThrough] get => this._connectionErrorReporter;
    [DebuggerStepThrough] set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      Interlocked.Exchange<IMServerConnectionErrorReporter>(ref this._connectionErrorReporter, value);
    }
  }

  /// <summary>
  /// Возвращает контейнер с необязательными сервисами, используемыми пулом сессий.
  /// </summary>
  public SessionPoolOptionalServices OptionalServices
  {
    [DebuggerStepThrough] get => this._optionalServices.Value;
    [DebuggerStepThrough] set
    {
      this._optionalServices.Value = value != null ? value : throw new ArgumentNullException(nameof (value));
    }
  }

  /// <summary>
  /// Возвращает контейнер с клиентскими прокси-сервисами для ускорения работы сессий сервера приложений.
  /// </summary>
  public UserSessionSpeedupServices SpeedupServices
  {
    [DebuggerStepThrough] get => this._speedupServices.Value;
    [DebuggerStepThrough] set
    {
      this._speedupServices.Value = value != null ? value : throw new ArgumentNullException(nameof (value));
    }
  }

  /// <summary>
  /// Выделяет сессию для текущего потока. Если для текущего потока уже имеется выделенная сессия, то метод должен вернуть эту же сессию.
  /// </summary>
  /// <returns>Дескриптор выделенной сессии</returns>
  public IUserSessionDescriptor Allocate()
  {
    lock (this.SyncRoot)
    {
      if (this.PoolInitialized)
        this.CheckConnectionPeriodically();
      if (!this.PoolInitialized)
        this.InitializeSessionPool();
      return (IUserSessionDescriptor) this.AssignSession();
    }
  }

  /// <summary>Освобождает выделенную ранее сессию.</summary>
  /// <param name="descriptor">Дескриптор выделенной сессии</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на дескриптор сессии не может быть null</exception>
  /// <exception cref="T:System.InvalidOperationException">Попытка освободить сессию, выделенную для другого потока приложения</exception>
  public void Release(IUserSessionDescriptor descriptor)
  {
    SessionPoolDescriptor descriptor1 = descriptor != null ? (SessionPoolDescriptor) descriptor : throw new ArgumentNullException(nameof (descriptor));
    if (!object.Equals((object) descriptor1.ThreadKey, (object) this.CreateCurrentThreadKey()))
      throw new InvalidOperationException("Невозможно освободить сессию, так как она была выделена для другого потока приложения.");
    lock (this.SyncRoot)
    {
      if (this.ActiveSessionsPool.TryGet(descriptor1.ThreadKey) != descriptor1)
        return;
      descriptor1.EndUsage();
      if (descriptor1.UsageCount != 0)
        return;
      switch (descriptor1.ReleaseMode)
      {
        case UserSessionReleaseMode.Drop:
          int threadId = descriptor1.ThreadKey.ThreadId;
          this.ActiveSessionsPool.Remove(descriptor1.ThreadKey);
          this.ConnectionErrorReporter.ReportEvent(false, $"Пользовательская сессия для потока с ThreadID={threadId} была отключена от сервера приложений. Она будет отброшена и заменена новой сессией.");
          break;
        default:
          this.ActiveSessionsPool.Remove(descriptor1.ThreadKey);
          this.InactiveSessionsPool.Add(descriptor1);
          break;
      }
    }
  }

  /// <summary>
  /// Завершает использование механизма выделения пользовательских сессий. Метод вызывается в конце работы приложения для
  /// корректного завершения приложения. Обычно, реализация этого метода используется для очистки пула сессий, если таковой имеется.
  /// </summary>
  public void Shutdown()
  {
    lock (this.SyncRoot)
    {
      if (!this.PoolInitialized)
        return;
      this.ClientCache.SaveCache();
      this.ClearSessionPool(true);
    }
  }

  /// <summary>
  /// Закрывает неиспользуемые в течение долгого времени пользовательские сессии.
  /// </summary>
  /// <param name="state">Не используется</param>
  private void FreeInactiveSessions(object state)
  {
    lock (this.SyncRoot)
    {
      if (this.InactiveSessionsPool.IsEmpty)
        return;
      try
      {
        DateTime utcNow = DateTime.UtcNow;
        foreach (SessionPoolDescriptor descriptor in this.InactiveSessionsPool.GetAll((Predicate<SessionPoolDescriptor>) (descriptor => utcNow >= descriptor.LastAccessTimeUtc + SessionPoolBase.CleanupPeriod)))
        {
          this.LogoutSessionSilently(descriptor.Session);
          this.InactiveSessionsPool.Remove(descriptor);
        }
      }
      catch (Exception ex)
      {
        this.ConnectionErrorReporter.ReportException(ex, "Произошел сбой в процессе фонового освобождения неиспользуемых пользовательских сессий сервера приложений.");
      }
    }
  }

  private SessionPoolDescriptor AssignSession()
  {
    SessionPoolThreadKey currentThreadKey = this.CreateCurrentThreadKey();
    SessionPoolDescriptor descriptor = this.ActiveSessionsPool.TryGet(currentThreadKey);
    if (descriptor == null)
    {
      bool isSessionPinningRequired = this.IsSessionPinningRequired(currentThreadKey);
      descriptor = this.InactiveSessionsPool.TryGet(currentThreadKey, isSessionPinningRequired);
      if (descriptor != null)
      {
        this.InactiveSessionsPool.Remove(descriptor);
      }
      else
      {
        descriptor = new SessionPoolDescriptor((IUserSession) this.CloneMainSessionInternal());
        if (isSessionPinningRequired)
          descriptor.OwnerThreadKey = currentThreadKey;
      }
      this.ActiveSessionsPool.Add(currentThreadKey, descriptor);
    }
    descriptor.BeginUsage();
    return descriptor;
  }

  private ClientSession CloneMainSessionInternal()
  {
    return this.CreateClientSession(this.CloneMainSession());
  }

  private ClientSession CreateClientSession(IUserSession rawSession)
  {
    ClientSessionContext clientSessionContext = new ClientSessionContext(this.ClientCache, this.SpeedupServices.CustomServices, this.SpeedupServices.DBConfigurations);
    return new ClientSession(rawSession, clientSessionContext);
  }

  /// <summary>
  /// Периодически проверяет подключение к серверу приложений. Если подключение разорвано, то пул открытых сессий будет очищен.
  /// Метод используется для автоматического переподключения к серверу приложений при обрыве подключения незаметно для пользователя.
  /// </summary>
  private void CheckConnectionPeriodically()
  {
    DateTime utcNow = DateTime.UtcNow;
    if (!(utcNow - this.LastCheckConnectionTime > SessionPoolBase.CheckConnectionPeriod))
      return;
    this.CheckConnection();
    this.LastCheckConnectionTime = utcNow;
  }

  /// <summary>
  /// Проверяет подключение к серверу приложений. Если подключение разорвано, то пул открытых сессий будет очищен.
  /// Метод используется для автоматического переподключения к серверу приложений при обрыве подключения незаметно для пользователя.
  /// </summary>
  private void CheckConnection()
  {
    if (this.TestConnection())
      return;
    this.ConnectionErrorReporter.ReportEvent(true, "Обнаружен обрыв подключения к серверу приложений. Пул сессий был очищен.");
    this.ClearSessionPool(false);
    this.ResetConnectionData();
  }

  private void InitializeSessionPool()
  {
    Tuple<IUserSession, ClientSession> mainSessionInternal = this.CreateMainSessionInternal();
    IUserSession userSession = mainSessionInternal.Item1;
    ClientSession clientSession = mainSessionInternal.Item2;
    this.PoolInitialized = true;
    this.MainSession = (IUserSession) clientSession;
    this.LastCheckConnectionTime = DateTime.UtcNow;
    this.ConnectionErrorReporter.ReportEvent(false, "Пул сессий был успешно инициализирован.");
    this.StartBackgroundJobs();
  }

  private Tuple<IUserSession, ClientSession> CreateMainSessionInternal()
  {
    try
    {
      IUserSession mainSession = this.CreateMainSession();
      this.ClientCache.LoadCache(mainSession);
      ClientSession clientSession = this.CreateClientSession(mainSession);
      return Tuple.Create<IUserSession, ClientSession>(mainSession, clientSession);
    }
    catch
    {
      if (!this.TestConnection())
      {
        this.ConnectionErrorReporter.ReportEvent(true, "Не удалось создать основную пользовательскую сессию из-за обрыва подключения к серверу приложений.");
        this.ResetConnectionData();
        this.ClientCache.ClearCache();
      }
      throw;
    }
  }

  private void LogoutSessionSilently(IUserSession session)
  {
    try
    {
      session.Logout("DefaultMainClientSession");
    }
    catch (Exception ex)
    {
      this.ConnectionErrorReporter.ReportException(ex, "Произошел сбой в при закрытии пользовательской сессии сервера приложений.");
    }
  }

  /// <summary>Очищает пул открытых пользовательских сессий.</summary>
  private void ClearSessionPool(bool doLogout)
  {
    this.StopBackgroundJobs();
    if (doLogout)
    {
      foreach (SessionPoolDescriptor descriptor in this.InactiveSessionsPool.GetAll())
      {
        this.LogoutSessionSilently(descriptor.Session);
        this.InactiveSessionsPool.Remove(descriptor);
      }
      this.LogoutSessionSilently(this.MainSession);
    }
    this.ActiveSessionsPool.EmergencyClear();
    this.InactiveSessionsPool.EmergencyClear();
    this.PoolInitialized = false;
    this.MainSession = (IUserSession) null;
  }

  private void StartBackgroundJobs()
  {
    this.LazyCleanupTimer = new Timer(new TimerCallback(this.FreeInactiveSessions), (object) null, SessionPoolBase.CleanupPeriod, SessionPoolBase.CleanupPeriod);
  }

  private void StopBackgroundJobs()
  {
    if (this.LazyCleanupTimer == null)
      return;
    this.LazyCleanupTimer.Change(-1, -1);
    this.LazyCleanupTimer.Dispose();
    this.LazyCleanupTimer = (Timer) null;
  }

  /// <summary>
  /// Проверяет работоспособность подключения к серверу приложений.
  /// Метод должен работать максимально быстро, так как вызывается очень часто.
  /// </summary>
  /// <returns>Результат проверки</returns>
  protected virtual bool TestConnection() => true;

  /// <summary>
  /// Очищает все кэшированные сервисом данные, полученные от сервера приложений.
  /// Метод используется при обрыве подключения к серверу приложений.
  /// </summary>
  protected virtual void ResetConnectionData()
  {
  }

  protected abstract SessionPoolThreadKey CreateCurrentThreadKey();

  protected abstract IUserSession CreateMainSession();

  protected abstract IUserSession CloneMainSession();

  /// <summary>
  /// Проверяет, требует ли текущий поток закрепления сессий, выделяемых для него.
  /// В реализации по умолчанию ни один поток не требует закрепления сессий.
  /// </summary>
  /// <param name="threadKey">Ключ текущего потока</param>
  /// <returns>true, если требуется закрепление сессии за текущим потоком</returns>
  protected virtual bool IsSessionPinningRequired(SessionPoolThreadKey threadKey) => false;

  /// <summary>Объект для синхронизации многопоточного доступа.</summary>
  protected object SyncRoot
  {
    [DebuggerStepThrough] get => this._syncRoot;
  }

  /// <summary>Экземпляр клиентского кэша.</summary>
  protected IClientCache ClientCache
  {
    [DebuggerStepThrough] get => this._clientCache;
  }

  /// <summary>
  /// Пул открытых и распределенных пользовательских сессий. Ключем является идентификатор потока, который использует сессию.
  /// </summary>
  private ActiveSessionsCollection ActiveSessionsPool
  {
    [DebuggerStepThrough] get => this._activeSessionsPool;
  }

  /// <summary>
  /// Пул открытых и нераспределенных пользовательских сессий.
  /// </summary>
  private InactiveSessionsCollection InactiveSessionsPool
  {
    [DebuggerStepThrough] get => this._inactiveSessionsPool;
  }

  /// <summary>
  /// Возвращает признак, что пул сессий уже был инициализирован.
  /// </summary>
  protected bool PoolInitialized
  {
    get => this._poolInitialized;
    private set => this._poolInitialized = value;
  }

  /// <summary>Основная сессия приложения.</summary>
  private IUserSession MainSession
  {
    [DebuggerStepThrough] get => this._mainSession;
    [DebuggerStepThrough] set => this._mainSession = value;
  }

  /// <summary>
  /// Таймер для запуска процесса освобождения давно не используемых, но открытых сессий.
  /// </summary>
  private Timer LazyCleanupTimer
  {
    [DebuggerStepThrough] get => this._lazyCleanupTimer;
    [DebuggerStepThrough] set => this._lazyCleanupTimer = value;
  }

  /// <summary>
  /// Время последней проверки подключения к серверу приложений.
  /// </summary>
  protected DateTime LastCheckConnectionTime
  {
    [DebuggerStepThrough] get => this._lastCheckConnectionTime;
    [DebuggerStepThrough] private set => this._lastCheckConnectionTime = value;
  }
}
