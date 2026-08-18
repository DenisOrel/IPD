// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IMServerConnectionStrategy
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Базовый класс для алгоритмов выбора сервера приложений из нескольких доступных.
/// </summary>
internal abstract class IMServerConnectionStrategy
{
  /// <summary>
  /// Строка подключения к базе данных, полученная у сервера приложений.
  /// </summary>
  protected string _DBConnectionString = string.Empty;
  private IMServerConnectInfo lastConnectedServer;

  /// <summary>
  /// Выбирает подходящих сервер приложений и подключается к нему. Об ошибках подключения к серверу приложений метод сообщает
  /// с помощью исключения типа <see cref="T:IMServerConnectionException" />.
  /// </summary>
  /// <param name="mainServer">Адрес и параметры подключения для основного сервера приложений</param>
  /// <param name="knownServers">Адреса и параметры подключаения для всех доступных серверов приложений</param>
  /// <returns>Кортеж с информацией о подключенном сервере приложений: (адрес сервера, объект сервера)</returns>
  /// <exception cref="T:IMServerConnectionException">Ошибка подключения к серверу приложений</exception>
  /// <exception cref="T:ArgumentNullException">mainServer || knownServers</exception>
  public Tuple<IMServerConnectInfo, IMServer> Connect(
    IMServerConnectInfo mainServer,
    IList<IMServerConnectInfo> knownServers)
  {
    if (mainServer == null)
      throw new ArgumentNullException(nameof (mainServer));
    if (knownServers == null)
      throw new ArgumentNullException(nameof (knownServers));
    Tuple<IMServerConnectInfo, IMServer> tuple = this.DoConnect(mainServer, knownServers, this.lastConnectedServer);
    this.lastConnectedServer = tuple.Item1;
    return tuple;
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
  protected abstract Tuple<IMServerConnectInfo, IMServer> DoConnect(
    IMServerConnectInfo mainServer,
    IList<IMServerConnectInfo> knownServers,
    IMServerConnectInfo lastConnectedServer);

  /// <summary>Позволяет убедиться, что сервер приложений отвечает.</summary>
  /// <param name="serverObject">Объект сервера приложений</param>
  /// <returns>Результат проверки</returns>
  protected Tuple<bool, Exception> TestConnection(IMServer serverObject)
  {
    if (serverObject == null)
      throw new ArgumentNullException(nameof (serverObject));
    try
    {
      serverObject.LiveStatus.KnockKnock();
      string connectionString = serverObject.LiveStatus.ConnectionString;
      if (this._DBConnectionString == string.Empty)
      {
        this._DBConnectionString = connectionString;
      }
      else
      {
        int num = this._DBConnectionString != connectionString ? 1 : 0;
      }
      return new Tuple<bool, Exception>(true, (Exception) null);
    }
    catch (Exception ex)
    {
      return new Tuple<bool, Exception>(false, ex);
    }
  }

  /// <summary>
  /// Проверяет, является ли указанный объект исключением Remoting.
  /// </summary>
  /// <param name="obj">Проверяемый объект</param>
  /// <returns>Признак, что это исключение Remoting</returns>
  protected bool IsRemotingException(Exception obj)
  {
    switch (obj)
    {
      case WebException _:
      case SocketException _:
      case RemotingException _:
        return true;
      default:
        return false;
    }
  }
}
