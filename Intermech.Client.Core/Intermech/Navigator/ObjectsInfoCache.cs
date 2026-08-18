
// Type: Intermech.Navigator.ObjectsInfoCache
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator;

/// <summary>
/// Кэш заголовков и кратких описаний объектов по их идентификаторам в базе данных.
/// При отсутствии в нем информации для указанного идентификатора кэш лезет в базу.
/// Если информация является устаревшей (срок действия кэшированной записи - 10 минут),
/// либо в сервис приходит уведомление об изменении кэшированного объекта, она будет
/// исключена из кэша
/// </summary>
internal class ObjectsInfoCache : IClientObjectsInfoCache, ICache, IObjectsInfoCache
{
  /// <summary>Время устаревания записей в кэше - 10 минут</summary>
  private TimeSpan syncDelta = new TimeSpan(0, 10, 0);
  /// <summary>
  /// Коллекция пар значений [(Int64)Идентификатор версии объекта] = [Краткое описание версии объекта]
  /// </summary>
  private Dictionary<long, QuickObjectInfoHolder> _items = new Dictionary<long, QuickObjectInfoHolder>();
  /// <summary>
  /// Коллекция пар значений [(Guid)Идентификатор версии объекта] = [Краткое описание версии объекта]
  /// </summary>
  private Dictionary<Guid, QuickObjectInfoHolder> _itemsByGuid = new Dictionary<Guid, QuickObjectInfoHolder>();
  /// <summary>
  /// Коллекция пар значений [(Int64)Идентификатор (ID) объекта] = [(Int64)Идентификатор базовой версии объекта]
  /// </summary>
  private Dictionary<long, long> _itemsBaseVersionByID = new Dictionary<long, long>();

  /// <summary>Загрузить краткое описание версии объекта в кэш</summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <returns>Краткое описание версии объекта или null, если объект не найден</returns>
  private QuickObjectInfoHolder LoadItem(long objectID)
  {
    if (objectID == 0L)
      return (QuickObjectInfoHolder) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return this.CheckValid(new QuickObjectInfoHolder(sessionKeeper.Session.GetObjectInfo(objectID)), false);
  }

  /// <summary>Загрузить краткое описание версии объекта в кэш</summary>
  /// <param name="objectGuid">Guid версии объекта</param>
  /// <returns>Краткое описание версии объекта или null, если объект не найден</returns>
  private QuickObjectInfoHolder LoadItem(Guid objectGuid)
  {
    if (Guid.Empty.Equals(objectGuid))
      return (QuickObjectInfoHolder) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return this.CheckValid(new QuickObjectInfoHolder(sessionKeeper.Session.GetObjectInfo(objectGuid)), false);
  }

  /// <summary>
  /// Проверить запись в кэше на устаревание
  /// При необходимости запись будет перечитана (удалена)
  /// </summary>
  /// <param name="forceReload">true означает принудительное чтение записи из базы данных</param>
  /// <param name="item">Проверяемая запись</param>
  /// <returns>Обновлённая запись или null, если объект не найден</returns>
  private QuickObjectInfoHolder CheckValid(QuickObjectInfoHolder item, bool forceReload)
  {
    if (item == null)
      return (QuickObjectInfoHolder) null;
    if (!forceReload && DateTime.UtcNow - item.Value.LoadTime < this.syncDelta)
      return item;
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(item.Value.ObjectID);
        this._items.Remove(item.Value.ObjectID);
        this._items.Remove(-item.Value.ObjectID);
        this._itemsByGuid.Remove(item.Value.VersionGuid);
        if (objectInfo.Empty)
          return (QuickObjectInfoHolder) null;
        item = new QuickObjectInfoHolder(objectInfo);
        this._items[item.Value.ObjectID] = item;
        this._itemsByGuid[item.Value.VersionGuid] = item;
        return item;
      }
    }
    catch
    {
      return (QuickObjectInfoHolder) null;
    }
  }

  /// <summary>Сбросить содержимое кэша</summary>
  public void Reset()
  {
    lock (this)
    {
      this._items.Clear();
      this._itemsByGuid.Clear();
      this._itemsBaseVersionByID.Clear();
    }
  }

  /// <summary>
  /// По идентификатору версии объекта получить заголовок объекта
  /// </summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <returns>Заголовок объекта</returns>
  public string GetObjectCaption(long objectID)
  {
    if (objectID == 0L)
      return string.Empty;
    lock (this)
    {
      if (!this._items.ContainsKey(objectID))
        this.LoadItem(objectID);
      else
        this.CheckValid(this._items[objectID], false);
      return this._items.ContainsKey(objectID) ? this._items[objectID].Value.Caption : string.Empty;
    }
  }

  /// <summary>По Guid версии объекта получить его заголовок</summary>
  /// <param name="objectGuid">Guid версии объекта</param>
  /// <returns>Заголовок объекта</returns>
  public string GetObjectCaption(Guid objectGuid)
  {
    if (Guid.Empty.Equals(objectGuid))
      return string.Empty;
    lock (this)
    {
      if (!this._itemsByGuid.ContainsKey(objectGuid))
        this.LoadItem(objectGuid);
      else
        this.CheckValid(this._itemsByGuid[objectGuid], false);
      return this._itemsByGuid.ContainsKey(objectGuid) ? this._itemsByGuid[objectGuid].Value.Caption : string.Empty;
    }
  }

  /// <summary>
  /// По идентификатору версии объекта получить его краткое описание
  /// </summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <returns>Краткое описание объекта</returns>
  public QuickObjectInfo GetObjectInfo(long objectID)
  {
    if (objectID == 0L)
      return new QuickObjectInfo(-1L, "", -1, Guid.Empty, -1L);
    lock (this)
    {
      if (!this._items.ContainsKey(objectID))
        this.LoadItem(objectID);
      else
        this.CheckValid(this._items[objectID], false);
      return this._items.ContainsKey(objectID) ? this._items[objectID].Value : new QuickObjectInfo(-1L, "", -1, Guid.Empty, -1L);
    }
  }

  /// <summary>По Guid версии объекта получить его краткое описание</summary>
  /// <param name="objectID">Guid версии объекта</param>
  /// <returns>Краткое описание объекта</returns>
  public QuickObjectInfo GetObjectInfo(Guid objectGuid)
  {
    if (Guid.Empty.Equals(objectGuid))
      return new QuickObjectInfo(-1L, "", -1, Guid.Empty, -1L);
    lock (this)
    {
      this.LoadItem(objectGuid);
      return this._itemsByGuid.ContainsKey(objectGuid) ? this._itemsByGuid[objectGuid].Value : new QuickObjectInfo(-1L, "", -1, Guid.Empty, -1L);
    }
  }

  /// <summary>
  /// По идентификатору объекта (не версии) получить заголовок базовой версии объекта
  /// </summary>
  /// <param name="ID">Идентификатор объекта (не версии)</param>
  /// <returns></returns>
  public string GetObjectCaptionByID(long ID)
  {
    if (ID == 0L)
      return string.Empty;
    lock (this)
    {
      long objectID = 0;
      if (!this._itemsBaseVersionByID.TryGetValue(ID, out objectID))
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject objectBaseVersionById = sessionKeeper.Session.GetObjectBaseVersionByID(ID, false);
          if (objectBaseVersionById == null)
            return string.Empty;
          objectID = objectBaseVersionById.ObjectID;
          this._itemsBaseVersionByID.Add(ID, objectID);
        }
      }
      return this.GetObjectCaption(objectID);
    }
  }

  /// <summary>
  /// По идентификатору объекта получить краткое описание базовой версии объекта
  /// </summary>
  /// <param name="ID">Идентификатор объекта (не версии)</param>
  /// <returns>Краткое описание базовой версии объекта</returns>
  public QuickObjectInfo GetObjectInfoByID(long ID)
  {
    if (ID == 0L)
      return new QuickObjectInfo(-1L, "", -1, Guid.Empty, -1L);
    lock (this)
    {
      long objectID = 0;
      if (!this._itemsBaseVersionByID.TryGetValue(ID, out objectID))
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject objectBaseVersionById = sessionKeeper.Session.GetObjectBaseVersionByID(ID, false);
          if (objectBaseVersionById == null)
            return new QuickObjectInfo(-1L, "", -1, Guid.Empty, -1L);
          objectID = objectBaseVersionById.ObjectID;
          this._itemsBaseVersionByID.Add(ID, objectID);
        }
      }
      return this.GetObjectInfo(objectID);
    }
  }

  /// <summary>Удалает из кэша описание для объекта</summary>
  /// <param name="objectId">Идентификатор объекта для удаления описания</param>
  public bool ResetInfo(long objectId)
  {
    if (!this._items.ContainsKey(objectId))
      return false;
    lock (this)
    {
      QuickObjectInfoHolder objectInfoHolder = this._items[objectId];
      this._items.Remove(objectId);
      this._items.Remove(-objectId);
      this._itemsByGuid.Remove(objectInfoHolder.Value.VersionGuid);
    }
    return true;
  }
}
