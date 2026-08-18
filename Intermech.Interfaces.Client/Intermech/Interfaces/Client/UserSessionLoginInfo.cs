// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.UserSessionLoginInfo
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Контейнер с параметрами входа пользователя на сервер приложений.
/// </summary>
public class UserSessionLoginInfo : IUserSessionLoginInfo
{
  private string _loginName;
  private string _userName;
  private long _roleId;
  private int _accessLevel;
  private string _roleName;
  private string _actingUserName;

  /// <summary>Создает объект.</summary>
  public UserSessionLoginInfo()
  {
    this._loginName = string.Empty;
    this._userName = string.Empty;
    this._roleName = string.Empty;
    this._roleId = 0L;
    this._accessLevel = -1;
    this._actingUserName = string.Empty;
  }

  /// <summary>
  /// Заполняет свойства текущего объекта, копируя значения из указанного объекта.
  /// </summary>
  /// <param name="anotherLoginInfo">Другой объект</param>
  /// <exception cref="T:ArgumentNullException">Параметр <paramref name="anotherLoginInfo" /> не должен быть равен null</exception>
  public virtual void Assign(UserSessionLoginInfo anotherLoginInfo)
  {
    this.LoginName = anotherLoginInfo != null ? anotherLoginInfo.LoginName : throw new ArgumentNullException(nameof (anotherLoginInfo));
    this.UserName = anotherLoginInfo.UserName;
    this.RoleName = anotherLoginInfo.RoleName;
    this.RoleId = anotherLoginInfo.RoleId;
    this.AccessLevel = anotherLoginInfo.AccessLevel;
    this.ActingUserName = anotherLoginInfo.ActingUserName;
  }

  /// <summary>Возвращает или задает логин пользователя.</summary>
  public string LoginName
  {
    get => this._loginName;
    set => this._loginName = value;
  }

  /// <summary>
  /// Возвращае или задает полное имя пользователя.
  /// Для входа пользователя это свойство заполнять не требуется. Оно будет заполнено автоматически после успешного входа.
  /// </summary>
  public string UserName
  {
    get => this._userName;
    set => this._userName = value;
  }

  /// <summary>
  /// Возвращает или задает название роли пользователя.
  /// Для входа пользователя требуется, чтобы было заполнено либо это свойство, либо RoleId.
  /// </summary>
  public string RoleName
  {
    get => this._roleName;
    set => this._roleName = value;
  }

  /// <summary>
  /// Возвращает или задает идентификатор роли пользователя.
  /// Для входа пользователя требуется, чтобы было заполнено либо это свойство, либо RoleName.
  /// </summary>
  public long RoleId
  {
    get => this._roleId;
    set => this._roleId = value;
  }

  /// <summary>Возвращает или задает уровень доступа пользователя.</summary>
  public int AccessLevel
  {
    get => this._accessLevel;
    set => this._accessLevel = value;
  }

  /// <summary>
  /// Возвращает или задает полное имя пользователя, который исполняет обязанности текущего пользователя.
  /// Для входа пользователя это свойство заполнять не требуется. Оно будет заполнено автоматически после успешного входа.
  /// </summary>
  public string ActingUserName
  {
    get => this._actingUserName;
    set => this._actingUserName = value;
  }
}
