// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IUserSessionLoginInfo
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс для чтения параметров входа пользователя на сервер приложений.
/// </summary>
public interface IUserSessionLoginInfo
{
  /// <summary>Возвращает логин пользователя.</summary>
  string LoginName { get; }

  /// <summary>Возвращае полное имя пользователя.</summary>
  string UserName { get; }

  /// <summary>Возвращает название роли пользователя.</summary>
  string RoleName { get; }

  /// <summary>Возвращает идентификатор роли пользователя.</summary>
  long RoleId { get; }

  /// <summary>Возвращает уровень доступа пользователя.</summary>
  int AccessLevel { get; }

  /// <summary>
  /// Возвращает полное имя пользователя, который исполняет обязанности текущего пользователя.
  /// </summary>
  string ActingUserName { get; }
}
