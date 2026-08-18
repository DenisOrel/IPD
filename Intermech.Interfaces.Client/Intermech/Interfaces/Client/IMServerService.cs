// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IMServerService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.AutoUpdater;
using Intermech.Configuration;
using Intermech.Protection;
using Intermech.Remoting.Sponsors;
using Intermech.Text;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Threading;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Клиентский сервис, предоставляющий доступ к главному объекту сервера приложений (IMServer).
/// Подключение к серверу приложений выполняется лениво при первом обращении к любому методу или свойству сервиса.
/// Реализация сервиса является thread safe.
/// </summary>
public class IMServerService : IServiceProvider
{
  private static readonly ApplicationServiceRef<IMServerService> defaultInstance = new ApplicationServiceRef<IMServerService>();
  private object syncRoot;
  private IMServerConnectionErrorReporter connectionErrorReporter;
  private IMServerConnectionStrategy connectionStrategy;
  private IMServerConnectionErrorStrategy connectionErrorStrategy;
  private bool isConfigured;
  private IMServerConnectInfo mainServerInfo;
  private List<IMServerConnectInfo> allServersInfos;
  private bool isConnected;
  private IMServerConnectInfo activeServerInfo;
  private IMServer activeServerObject;
  private IMServerLiveStatus activeServerLiveStatusService;
  private IMServerAppConfigurationProxy activeServerAppConfigurationProxy;
  private ILeaseRenewalService activeServerLeaseRenewalService;
  private readonly ConcurrentDictionary<Type, object> activeServerCustomServiceCache;
  private readonly Func<Type, object> getCustomServiceFunc;

  /// <summary>Создает объект.</summary>
  public IMServerService()
  {
    this.syncRoot = new object();
    this.connectionErrorStrategy = new IMServerConnectionErrorStrategy();
    this.connectionErrorReporter = new IMServerConnectionErrorReporter();
    this.connectionStrategy = IMServerService.CreateConnectionStrategy();
    this.activeServerCustomServiceCache = new ConcurrentDictionary<Type, object>();
    this.getCustomServiceFunc = new Func<Type, object>(this.GetCustomServiceSlow);
  }

  private static IMServerConnectionStrategy CreateConnectionStrategy()
  {
    if (AppSettingsHelper.GetBoolean("Broker", false))
    {
      string serverName = AppSettingsHelper.GetString("BrokerName", (string) null);
      if (!string.IsNullOrEmpty(serverName))
      {
        int int32 = AppSettingsHelper.GetInt32("BrokerPort", 9901);
        return (IMServerConnectionStrategy) new ConnectionBrokerStrategy(serverName, int32);
      }
    }
    return (IMServerConnectionStrategy) new MainServerOnlyConnectionStrategy();
  }

  /// <summary>
  /// Возвращает признак, что подключение к серверу приложений уже установлено.
  /// </summary>
  public bool IsConnected
  {
    [DebuggerStepThrough] get
    {
      lock (this.syncRoot)
        return this.isConnected;
    }
  }

  /// <summary>
  /// Возвращает Url объекта подключенного сервера приложений.
  /// </summary>
  public string ServerUrl
  {
    [DebuggerStepThrough] get
    {
      lock (this.syncRoot)
      {
        this.ConnectLazily();
        return this.activeServerInfo.Url;
      }
    }
  }

  /// <summary>Возвращает объект подключенного сервера приложений.</summary>
  public IMServer ServerObject
  {
    [DebuggerStepThrough] get
    {
      lock (this.syncRoot)
      {
        this.ConnectLazily();
        return this.activeServerObject;
      }
    }
  }

  object IServiceProvider.GetService(Type serviceType) => this.GetCustomService(serviceType);

  /// <summary>
  /// Получает интерфейс, зарегистрированный на сервере службой ICustomServices и доступный на стороне клиента
  /// </summary>
  /// <param name="serviceType">Тип зарегистрированного интерфейса</param>
  /// <returns>Требуемый интерфейс или null</returns>
  public object GetCustomService(Type serviceType)
  {
    if (serviceType == (Type) null)
      throw new ArgumentNullException(nameof (serviceType));
    return this.activeServerCustomServiceCache.GetOrAdd(serviceType, this.getCustomServiceFunc);
  }

  private object GetCustomServiceSlow(Type serviceType)
  {
    return this.ServerObject.GetCustomService(serviceType);
  }

  /// <summary>
  /// Возвращает или задает стратегию обработки ошибок подключения к серверу приложений.
  /// Значение свойства не может быть null.
  /// </summary>
  public IMServerConnectionErrorStrategy ConnectionErrorStrategy
  {
    [DebuggerStepThrough] get => this.connectionErrorStrategy;
    [DebuggerStepThrough] set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      Interlocked.Exchange<IMServerConnectionErrorStrategy>(ref this.connectionErrorStrategy, value);
    }
  }

  /// <summary>
  /// Возвращает или задает объект для вывода диагностических сообщений.
  /// Значение свойства не может быть null.
  /// </summary>
  public IMServerConnectionErrorReporter ConnectionErrorReporter
  {
    [DebuggerStepThrough] get => this.connectionErrorReporter;
    [DebuggerStepThrough] set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      Interlocked.Exchange<IMServerConnectionErrorReporter>(ref this.connectionErrorReporter, value);
    }
  }

  /// <summary>
  /// Проверяет работоспособность подключения к серверу приложений.
  /// </summary>
  /// <returns>true - если подключение к серверу приложений было установлено, а сервер приложений отвечает; иначе - false</returns>
  public bool TestConnection()
  {
    lock (this.syncRoot)
    {
      if (!this.isConnected)
        return false;
      try
      {
        this.GetLiveStatusService().KnockKnock();
        return true;
      }
      catch
      {
        return false;
      }
    }
  }

  /// <summary>
  /// Проверяет работоспособность подключения к серверу приложений.
  /// Подключение считается работоспособным только в том случае, если через remoting
  /// доступен не только сам сервер приложений, но и указанный серверный объект.
  /// </summary>
  /// <param name="serverObject">Дополнительный серверный объект для проверки подключения</param>
  /// <returns>true - если подключение к серверу приложений было установлено, а сервер приложений отвечает; иначе - false</returns>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="serverObject" /> содержит null</exception>
  public bool TestConnection(object serverObject)
  {
    if (serverObject == null)
      throw new ArgumentNullException(nameof (serverObject));
    lock (this.syncRoot)
    {
      if (!this.isConnected)
        return false;
      try
      {
        this.GetLiveStatusService().KnockKnock(serverObject);
        return true;
      }
      catch
      {
        return false;
      }
    }
  }

  /// <summary>
  /// Проверяет работоспособность подключения к серверу приложений.
  /// Подключение считается работоспособным только в том случае, если через remoting
  /// доступен не только сам сервер приложений, но и указанные серверные объекты.
  /// </summary>
  /// <param name="serverObjects">Дополнительные серверные объекты для проверки подключения</param>
  /// <returns>true - если подключение к серверу приложений было установлено, а сервер приложений отвечает; иначе - false</returns>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="serverObjects" /> содержит null</exception>
  public bool TestConnection(params object[] serverObjects)
  {
    if (serverObjects == null)
      throw new ArgumentNullException(nameof (serverObjects));
    lock (this.syncRoot)
    {
      if (!this.isConnected)
        return false;
      try
      {
        this.GetLiveStatusService().KnockKnock(serverObjects);
        return true;
      }
      catch
      {
        return false;
      }
    }
  }

  /// <summary>
  /// Разрывает подключение к серверу приложений и очищает все кэшированные сервисом данные, полученные от сервера приложений.
  /// Метод используется при обрыве подключения к серверу приложений, он обеспечивает возможность восстановления подключения.
  /// </summary>
  public void ResetConnection()
  {
    lock (this.syncRoot)
    {
      if (!this.isConnected)
        return;
      this.RaiseConnectionLost();
      this.ClearConnectionData();
      this.isConnected = false;
    }
  }

  private void RaiseConnected()
  {
    EventHandler connected = this.Connected;
    if (connected == null)
      return;
    connected((object) this, EventArgs.Empty);
  }

  private void RaiseConnectionLost()
  {
    EventHandler connectionLost = this.ConnectionLost;
    if (connectionLost == null)
      return;
    connectionLost((object) this, EventArgs.Empty);
  }

  /// <summary>
  /// Событие, срабатывающее после успешного подключения к серверу приложений.
  /// </summary>
  public event EventHandler Connected;

  /// <summary>
  /// Событие, срабатывающиее после обрыва подключения к серверу приложений.
  /// </summary>
  public event EventHandler ConnectionLost;

  /// <summary>
  /// Возвращает сервис для получения состояния сервера приложений.
  /// </summary>
  /// <returns>Объект сервиса</returns>
  public IMServerLiveStatus GetLiveStatusService()
  {
    lock (this.syncRoot)
    {
      if (this.activeServerLiveStatusService == null)
        this.activeServerLiveStatusService = this.ServerObject.LiveStatus;
      return this.activeServerLiveStatusService;
    }
  }

  /// <summary>
  /// Возвращает сервис для получения конфигурации сервера приложений из файла app.config.
  /// </summary>
  /// <returns>Объект сервиса</returns>
  public IMServerAppConfiguration GetAppConfigurationService()
  {
    lock (this.syncRoot)
    {
      if (this.activeServerAppConfigurationProxy == null)
        this.activeServerAppConfigurationProxy = new IMServerAppConfigurationProxy(this.ServerObject.AppConfiguration);
      return (IMServerAppConfiguration) this.activeServerAppConfigurationProxy;
    }
  }

  /// <summary>
  /// Возвращает сервис сервера приложений для управления временем жизни серверных объектов.
  /// </summary>
  /// <returns>Объект сервиса</returns>
  public ILeaseRenewalService GetLeaseRenewalService()
  {
    lock (this.syncRoot)
    {
      if (this.activeServerLeaseRenewalService == null)
        this.activeServerLeaseRenewalService = this.ServerObject.LeaseRenewalService;
      return this.activeServerLeaseRenewalService;
    }
  }

  private void ConfigureLazily()
  {
    if (this.isConfigured)
      return;
    this.ConfigureCore();
    this.isConfigured = true;
  }

  private void ConfigureCore()
  {
    try
    {
      this.mainServerInfo = this.GetMainServerInfo();
      this.allServersInfos = new List<IMServerConnectInfo>();
      this.allServersInfos.Add(this.mainServerInfo);
      this.allServersInfos.AddRange((IEnumerable<IMServerConnectInfo>) this.GetAuxServersInfos());
    }
    catch
    {
      this.mainServerInfo = (IMServerConnectInfo) null;
      this.allServersInfos = (List<IMServerConnectInfo>) null;
      throw;
    }
  }

  private IMServerConnectInfo GetMainServerInfo()
  {
    foreach (WellKnownClientTypeEntry wellKnownClientType in RemotingConfiguration.GetRegisteredWellKnownClientTypes())
    {
      if (wellKnownClientType.ObjectType == typeof (IMServer))
        return new IMServerConnectInfo(wellKnownClientType.ObjectUrl);
    }
    throw new InvalidOperationException("Bad remoting configuration. No application server url found.");
  }

  private List<IMServerConnectInfo> GetAuxServersInfos()
  {
    List<IMServerConnectInfo> auxServersInfos = new List<IMServerConnectInfo>();
    for (int index = 1; index < 32 /*0x20*/; ++index)
    {
      string serverUrl = TextServices.Trim(ConfigurationManager.AppSettings["AuxServer" + index.ToString()]);
      if (!string.IsNullOrEmpty(serverUrl))
        auxServersInfos.Add(new IMServerConnectInfo(serverUrl));
    }
    return auxServersInfos;
  }

  private void ConnectLazily()
  {
    this.ConfigureLazily();
    if (this.isConnected)
      return;
    this.ConnectCore();
    this.isConnected = true;
    this.RaiseConnected();
  }

  private void ConnectCore()
  {
    IMServerService.AbortApplicationEventArgs abortArgs = (IMServerService.AbortApplicationEventArgs) null;
    bool flag = true;
    do
    {
      try
      {
        this.TryToConnect();
        this.ConnectionErrorReporter.ReportEvent(false, $"Установлено подключение к серверу приложений {this.activeServerInfo.Url}");
        return;
      }
      catch (IMServerConnectionException ex)
      {
        IMServerConnectionErrorInfo connectionErrorInfo = this.ConnectionErrorStrategy.FormatConnectionException(ex);
        this.ConnectionErrorReporter.ReportEvent(true, $"Произошел сбой при подключению к серверу приложений.{Environment.NewLine}{$"Причина: исключительная ситуация типа '{connectionErrorInfo.ExceptionType}'"}{Environment.NewLine}{connectionErrorInfo.ExceptionText}");
        switch (this.ConnectionErrorStrategy.HandleConnectionException(ex))
        {
          case IMServerReconnectType.TryConnectAgain:
            this.ConnectionErrorReporter.ReportEvent(false, "Выполняется попытка переподключения к сереверу приложений.");
            break;
          case IMServerReconnectType.AbortConnection:
            throw;
          case IMServerReconnectType.AbortApplication:
            flag = false;
            abortArgs = new IMServerService.AbortApplicationEventArgs(ex.Message);
            break;
          case IMServerReconnectType.AbortApplicationSilently:
            flag = false;
            abortArgs = new IMServerService.AbortApplicationEventArgs((string) null);
            break;
        }
      }
    }
    while (flag);
    if (abortArgs == null)
      return;
    this.AbortApplication(abortArgs);
  }

  private void AbortApplication(
    IMServerService.AbortApplicationEventArgs abortArgs)
  {
    if (!string.IsNullOrEmpty(abortArgs.Message))
    {
      using (AutoUpdaterErrorDialog updaterErrorDialog = new AutoUpdaterErrorDialog())
      {
        updaterErrorDialog.Text = "Фатальная ошибка";
        updaterErrorDialog.MessageText = abortArgs.Message;
        if (new AutoUpdaterClientSettings().AllowAutoUpdate)
          updaterErrorDialog.AutoCloseMode = true;
        int num = (int) updaterErrorDialog.ShowDialog();
      }
    }
    ProtectionService.Stop();
    Process.GetCurrentProcess().Kill();
  }

  private void TryToConnect()
  {
    try
    {
      Tuple<IMServerConnectInfo, IMServer> tuple = this.connectionStrategy.Connect(this.mainServerInfo, (IList<IMServerConnectInfo>) this.allServersInfos);
      this.activeServerInfo = tuple.Item1;
      this.activeServerObject = tuple.Item2;
    }
    catch
    {
      this.ClearConnectionData();
      throw;
    }
  }

  private void ClearConnectionData()
  {
    this.activeServerInfo = (IMServerConnectInfo) null;
    this.activeServerObject = (IMServer) null;
    this.activeServerLiveStatusService = (IMServerLiveStatus) null;
    this.activeServerAppConfigurationProxy = (IMServerAppConfigurationProxy) null;
    this.activeServerLeaseRenewalService = (ILeaseRenewalService) null;
    this.activeServerCustomServiceCache.Clear();
  }

  /// <summary>
  /// Возвращает экземпляр сервиса, используемый по умолчанию.
  /// </summary>
  [Obsolete("Use the dependency injection instead of this.", true)]
  public static IMServerService Default => IMServerService.defaultInstance.Value;

  /// <summary>
  /// Содержит параметры для события аварийного завершения текущего приложения,
  /// когда подключение к серверу приложений не представляется возможным.
  /// </summary>
  private class AbortApplicationEventArgs : EventArgs
  {
    /// <summary>Создает объект.</summary>
    /// <param name="message">Сообщение, отображаемое перед аварийным завершением приложения. Значение параметра может быть не задано</param>
    public AbortApplicationEventArgs(string message) => this.Message = message;

    /// <summary>
    /// Возвращает или задает сообщение, отображаемое перед аварийным завершением приложения.
    /// Значение свойства может быть не задано.
    /// </summary>
    public string Message { get; private set; }
  }
}
