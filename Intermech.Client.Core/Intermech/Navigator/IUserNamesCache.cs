
// Type: Intermech.Navigator.IUserNamesCache
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Navigator;

/// <summary>
/// Интерфейс для извлечения из кэша имен пользователей по их идентификаторам в базе данных.
/// </summary>
public interface IUserNamesCache : ICache
{
  /// <summary>
  /// По идентификатору версии объекта получить имя пользователя
  /// </summary>
  /// <param name="userObjectID">Идентификатор версии объекта пользователя</param>
  /// <returns>Имя пользователя</returns>
  string GetUserName(long userObjectID);

  /// <summary>По Guid версии объекта получить имя пользователя</summary>
  /// <param name="userObjectGuid">Guid версии объекта пользователя</param>
  /// <returns>Имя пользователя</returns>
  string GetUserName(Guid userObjectGuid);
}
