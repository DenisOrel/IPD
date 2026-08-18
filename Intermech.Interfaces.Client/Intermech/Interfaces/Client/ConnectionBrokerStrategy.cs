// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ConnectionBrokerStrategy
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.ApplicationModel;
using Intermech.Interfaces.ConnectionBroker;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Реализует стратегию подключения к серверу приложений через брокер подключений. Если через брокера подключение получить не удаётся - пытается подключиться по обычным настройкам.
/// </summary>
internal sealed class ConnectionBrokerStrategy : MainServerOnlyConnectionStrategy
{
  /// <summary>Имя сервера, на котором работает брокер</summary>
  private string _ServerName;
  /// <summary>Порт, на котором работает брокер</summary>
  private int _ServerPort;

  public ConnectionBrokerStrategy(string serverName, int serverPort)
  {
    this._ServerName = serverName;
    this._ServerPort = serverPort;
  }

  /// <summary>
  /// Выбирает подходящих сервер приложений и подключается к нему. Об ошибках подключения к серверу метод должен сообщать
  /// с помощью исключения типа <see cref="T:IMServerConnectionException" />.
  /// </summary>
  /// <param name="mainServer">Адрес и параметры подключения для основного сервера приложений</param>
  /// <param name="knownServers">Адреса и параметры подключаения для всех доступных серверов приложений</param>
  /// <param name="lastConnectedServer">Адрес и параметры подключения для последнего успешно подключенного сервера. Может быть null</param>
  /// <returns>Кортеж с информацией о подключенном сервере приложений: (адрес сервера, объект сервера)</returns>
  /// <exception cref="T:IMServerConnectionException">Ошибка подключения к серверу приложений</exception>
  protected override Tuple<IMServerConnectInfo, IMServer> DoConnect(
    IMServerConnectInfo mainServer,
    IList<IMServerConnectInfo> knownServers,
    IMServerConnectInfo lastConnectedServer)
  {
    string str = string.Empty;
    IApplicationEventLogService service = ServicesManager.GetService(typeof (IApplicationEventLogService)) as IApplicationEventLogService;
    IMServerConnectInfo serverConnectInfo1 = new IMServerConnectInfo($"tcp://{this._ServerName}:{this._ServerPort}/Broker.rem");
    IConnectionBroker connectionBroker = (IConnectionBroker) null;
    try
    {
      connectionBroker = (IConnectionBroker) Activator.GetObject(typeof (IConnectionBroker), serverConnectInfo1.Url);
      str = connectionBroker.GetAppServerURL(this._DBConnectionString, false);
      if (str == string.Empty)
        service.DefaultLog.Write($"Брокер {this._ServerName}:{this._ServerPort} не вернул адрес сервера приложений.");
    }
    catch (Exception ex)
    {
      service.DefaultLog.Write($"Не удалось подключиться к брокеру {this._ServerName}:{this._ServerPort}: {ex.Message}");
    }
    if (str == string.Empty)
      return base.DoConnect(mainServer, knownServers, lastConnectedServer);
    IMServerConnectInfo serverConnectInfo2 = new IMServerConnectInfo(str);
    IMServer mserver = (IMServer) Activator.GetObject(typeof (IMServer), str);
    if (!this.TestConnection(mserver).Item1)
    {
      string appServerUrl = connectionBroker.GetAppServerURL(this._DBConnectionString, true);
      if (appServerUrl == string.Empty)
      {
        service.DefaultLog.Write($"Брокер {this._ServerName}:{this._ServerPort} не вернул адрес сервера приложений в режиме принудительного опроса серверов.");
        return base.DoConnect(mainServer, knownServers, lastConnectedServer);
      }
      serverConnectInfo2 = new IMServerConnectInfo(appServerUrl);
      mserver = (IMServer) Activator.GetObject(typeof (IMServer), appServerUrl);
      Tuple<bool, Exception> tuple = this.TestConnection(mserver);
      if (!tuple.Item1)
        throw new IMServerConnectionException($"Не удалось подключиться к основному серверу приложений по адресу {serverConnectInfo2.Url}.", true, tuple.Item2);
    }
    MainServerOnlyConnectionStrategy.ValidateServerVersion(mserver);
    return Tuple.Create<IMServerConnectInfo, IMServer>(serverConnectInfo2, mserver);
  }
}
