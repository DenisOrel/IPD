// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.UserSessionLoginService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Простейшая реализация сервиса, предоставляющего информацию о параметрах входа пользователя на сервер приложений.
/// Для получения необходимы сведений используется информация из клиентского пула сессий сервера приложений.
/// Класс является thread safe.
/// </summary>
public sealed class UserSessionLoginService : IUserSessionLoginService
{
  private IUserSessionPool userSessionPool;
  private static readonly IUserSessionLoginInfo emptyLoginInfo = (IUserSessionLoginInfo) new UserSessionLoginInfo();

  /// <summary>Создает объект.</summary>
  /// <param name="userSessionPool">Клиентский пул сессий сервера приложений</param>
  /// <exception cref="T:ArgumentNullException">Параметр <paramref name="userSessionPool" /> не должен быть равен null</exception>
  public UserSessionLoginService(IUserSessionPool userSessionPool)
  {
    this.userSessionPool = userSessionPool != null ? userSessionPool : throw new ArgumentNullException(nameof (userSessionPool));
  }

  /// <summary>
  /// Возвращает основные параметры входа пользователя на сервер приложений.
  /// </summary>
  /// <returns>Объект с основными параметрами входа пользователя на сервер приложений</returns>
  public IUserSessionLoginInfo GetLoginInfo()
  {
    return this.userSessionPool.TryGetMainSessionLoginInfo() ?? UserSessionLoginService.emptyLoginInfo;
  }
}
