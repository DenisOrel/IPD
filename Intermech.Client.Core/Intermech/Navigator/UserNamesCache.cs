
// Type: Intermech.Navigator.UserNamesCache
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator;

/// <summary>
/// Кэш имен пользователей. При отсутствии в нем имени для указанного идентификатора кэш лезет в базу.
/// </summary>
public class UserNamesCache : IUserNamesCache, ICache
{
  /// <summary>
  /// Коллекция пар значений [(Int64)Идентификатор пользователя] = [(string)Имя пользователя]
  /// </summary>
  private Dictionary<long, string> _names;
  /// <summary>
  /// Коллекция пар значений [(Guid)Идентификатор пользователя] = [(string)Имя пользователя]
  /// </summary>
  private Dictionary<Guid, string> _namesGuid;

  /// <summary>Сбросить содержимое кэша</summary>
  public void Reset()
  {
    this._names = (Dictionary<long, string>) null;
    this._namesGuid = (Dictionary<Guid, string>) null;
  }

  /// <summary>
  /// Метод зачитывает с сервера список всех пользователей, чтобы не дёргать их по одному
  /// </summary>
  private void LoadUsersCache()
  {
    Tuple<long, Guid, string>[] usersCache;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      usersCache = sessionKeeper.Session.ServerCache.GetUsersCache();
    Dictionary<long, string> dictionary1 = new Dictionary<long, string>(usersCache.Length);
    Dictionary<Guid, string> dictionary2 = new Dictionary<Guid, string>(usersCache.Length);
    foreach (Tuple<long, Guid, string> tuple in usersCache)
    {
      dictionary1.Add(tuple.Item1, tuple.Item3);
      dictionary2.Add(tuple.Item2, tuple.Item3);
    }
    this._namesGuid = dictionary2;
    this._names = dictionary1;
  }

  /// <summary>Проверяет зачитан ли кэш и если нет - зачитывает его</summary>
  private void CheckLoaded()
  {
    if (this._names != null)
      return;
    this.LoadUsersCache();
  }

  /// <summary>Вернуть имя пользователя по его идентификатору</summary>
  /// <param name="userObjectID">Идентификатор пользователя</param>
  /// <returns>Имя пользователя</returns>
  public string GetUserName(long userObjectID)
  {
    if (userObjectID == 0L)
      return string.Empty;
    this.CheckLoaded();
    if (!this._names.ContainsKey(userObjectID))
    {
      try
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(userObjectID);
          this._names[userObjectID] = objectInfo.Caption;
        }
      }
      catch
      {
        this._names[userObjectID] = LocalizationHolder.rm.GetString("Client.Core_268") + userObjectID.ToString();
      }
    }
    return this._names[userObjectID];
  }

  /// <summary>Вернуть имя пользователя по его Guid</summary>
  /// <param name="userObjectGuid">Guid пользователя</param>
  /// <returns>Имя пользователя</returns>
  public string GetUserName(Guid userObjectGuid)
  {
    if (userObjectGuid == Guid.Empty)
      return string.Empty;
    this.CheckLoaded();
    if (!this._namesGuid.ContainsKey(userObjectGuid))
    {
      try
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(userObjectGuid);
          this._namesGuid[userObjectGuid] = dbObject.Caption;
        }
      }
      catch
      {
        this._namesGuid[userObjectGuid] = string.Empty;
      }
    }
    return this._namesGuid[userObjectGuid];
  }
}
