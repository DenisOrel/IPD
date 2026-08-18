// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CyclicServerConnectionStrategy
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Реализует простейший алгоритм выбора сервера приложений из нескольких доступных - при обрыве подключения к текущему серверу
/// приложений выполняется подключение к следующему доступному.
/// </summary>
internal sealed class CyclicServerConnectionStrategy : IMServerConnectionStrategy
{
  private int activeServerIndex;

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
    if (lastConnectedServer != null)
      this.MoveToNextServerIndex(knownServers.Count);
    for (int index = 0; index < knownServers.Count; ++index)
    {
      IMServerConnectInfo knownServer = knownServers[this.activeServerIndex];
      IMServer serverObject = (IMServer) Activator.GetObject(typeof (IMServer), knownServer.Url);
      if (this.TestConnection(serverObject).Item1)
        return Tuple.Create<IMServerConnectInfo, IMServer>(knownServer, serverObject);
      this.MoveToNextServerIndex(knownServers.Count);
    }
    throw new IMServerConnectionException("Не удалось подключиться к серверу приложений. Ни один из известных серверов не отвечает.", true);
  }

  private void MoveToNextServerIndex(int serverCount)
  {
    ++this.activeServerIndex;
    if (this.activeServerIndex < serverCount)
      return;
    this.activeServerIndex = 0;
  }
}
