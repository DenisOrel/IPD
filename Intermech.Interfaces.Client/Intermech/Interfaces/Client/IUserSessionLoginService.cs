// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IUserSessionLoginService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс сервиса, предоставляющего информацию о параметрах входа пользователя на сервер приложений.
/// </summary>
public interface IUserSessionLoginService
{
  /// <summary>
  /// Возвращает основные параметры входа пользователя на сервер приложений.
  /// </summary>
  /// <returns>Объект с основными параметрами входа пользователя на сервер приложений</returns>
  IUserSessionLoginInfo GetLoginInfo();
}
