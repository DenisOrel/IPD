// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IUserSessionPool
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс пула сессий клиента IPS. При получении сервиса следует учитывать, что он не является является необязательным,
/// так как не все клиенты IPS используют пул сессий.
/// </summary>
public interface IUserSessionPool : IUserSessionAllocator
{
  /// <summary>
  /// Возвращает параметры входа пользователя на сервер приложений, использованные для создания основной сессии.
  /// </summary>
  /// <returns>Параметры входа пользователя или null, если основая сессия еще не создна или была закрыта</returns>
  IUserSessionLoginInfo TryGetMainSessionLoginInfo();

  /// <summary>
  /// Событие, появляющееся после успешного создания основной сессии.
  /// </summary>
  event EventHandler<UserSessionCreatedEventArgs> MainSessionCreated;
}
