// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.UserSessionCreatedEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Аргументы события создания пользовательской сессии.</summary>
public sealed class UserSessionCreatedEventArgs : EventArgs
{
  private IMServer server;
  private IUserSession session;
  private IUserSessionLoginInfo loginInfo;

  /// <summary>Создает объект.</summary>
  /// <param name="server">Основной объект сервера приложений</param>
  /// <param name="session">Объект созданной пользовательской сессии</param>
  /// <param name="loginInfo">Контейнер с параметрами входа пользователя на сервер приложений</param>
  /// <exception cref="T:ArgumentNullException">Параметры <paramref name="server" />, <paramref name="session" />, <paramref name="loginInfo" /> не должны быть равны null</exception>
  public UserSessionCreatedEventArgs(
    IMServer server,
    IUserSession session,
    IUserSessionLoginInfo loginInfo)
  {
    if (server == null)
      throw new ArgumentNullException(nameof (server));
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (loginInfo == null)
      throw new ArgumentNullException(nameof (loginInfo));
    this.server = server;
    this.session = session;
    this.loginInfo = loginInfo;
  }

  /// <summary>Возвращает основной объект сервера приложений.</summary>
  public IMServer Server
  {
    [DebuggerStepThrough] get => this.server;
  }

  /// <summary>Возвращает объект пользовательской сессии.</summary>
  public IUserSession Session
  {
    [DebuggerStepThrough] get => this.session;
  }

  /// <summary>
  /// Возвращает контейнер с параметрами входа пользователя на сервер приложений.
  /// </summary>
  public IUserSessionLoginInfo LoginInfo
  {
    [DebuggerStepThrough] get => this.loginInfo;
  }
}
