// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.MainServerOnlyConnectionStrategy
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Реализует алгоритм выбора сервера приложений, который всегда подключается только к основному серверу приложений.
/// Этот алгоритм используется для обратной совместимости с версиями IPS до 5.0, в которой реализован выбор сервера приложений из списка доступных.
/// </summary>
internal class MainServerOnlyConnectionStrategy : IMServerConnectionStrategy
{
  private Version clientVersion;

  /// <summary>Создает объект.</summary>
  public MainServerOnlyConnectionStrategy()
  {
    this.clientVersion = Assembly.GetExecutingAssembly().GetName().Version;
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
    IMServerConnectInfo serverConnectInfo = mainServer;
    IMServer mserver = (IMServer) Activator.GetObject(typeof (IMServer), serverConnectInfo.Url);
    Tuple<bool, Exception> tuple = this.TestConnection(mserver);
    if (!tuple.Item1)
      throw new IMServerConnectionException($"Не удалось подключиться к основному серверу приложений по адресу {serverConnectInfo.Url}.", true, tuple.Item2);
    MainServerOnlyConnectionStrategy.ValidateServerVersion(mserver);
    return Tuple.Create<IMServerConnectInfo, IMServer>(serverConnectInfo, mserver);
  }

  [Conditional("RELEASE")]
  protected static void ValidateServerVersion(IMServer server)
  {
    Version version1 = server.Version;
    Version version2 = Assembly.GetExecutingAssembly().GetName().Version;
    if (!version2.Equals(version1))
      throw new IMServerConnectionException(string.Format(LocalizationHolder.rm.GetString("Interfaces.Client_154"), (object) Environment.NewLine, (object) version1, (object) version2), false);
  }
}
