// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ClientSessionObjectProxy
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Remoting;
using System;
using System.Diagnostics;
using System.Runtime.Remoting;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Базовый класс для клиентских оберток для серверных объектов, принадлежащих одной конкретной пользовательской сессии сервера приложений.
/// </summary>
internal abstract class ClientSessionObjectProxy : MarshalByRefObject, IServerObjectWrapper
{
  private ClientSession _clientSession;
  private MarshalByRefObject _serverObjectMbr;

  /// <summary>Создает объект.</summary>
  /// <param name="clientSession">Обернутая пользовательская сессия сервера приложений, которой принадлежит текущий объект</param>
  /// <param name="serverObjectMbr">Серверный объект</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="clientSession" /> равен null; параметр <paramref name="serverObjectMbr" /> равен null</exception>
  protected ClientSessionObjectProxy(
    ClientSession clientSession,
    MarshalByRefObject serverObjectMbr)
  {
    if (clientSession == null)
      throw new ArgumentNullException(nameof (clientSession));
    if (serverObjectMbr == null)
      throw new ArgumentNullException(nameof (serverObjectMbr));
    this._clientSession = clientSession;
    this._serverObjectMbr = serverObjectMbr;
  }

  /// <summary>
  /// Возвращает обернутую пользовательскую сессию сервера приложений, которой принадлежит текущий объект.
  /// </summary>
  protected ClientSession ClientSession
  {
    [DebuggerStepThrough] get => this._clientSession;
  }

  /// <summary>Возвращает ссылку на серверный объект.</summary>
  /// <returns>Ссылка на серверный объект</returns>
  MarshalByRefObject IServerObjectWrapper.GetServerObject() => this._serverObjectMbr;

  /// <summary>Возвращает remoting-ссылку на текущий объект.</summary>
  /// <param name="requestedType">Тип ссылки</param>
  /// <returns>remoting-ссылка на текущий объект</returns>
  public override ObjRef CreateObjRef(Type requestedType)
  {
    return this._serverObjectMbr.CreateObjRef(requestedType);
  }
}
