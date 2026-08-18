// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.PdmConfiguratorContextsCache
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Interfaces.Compositions;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Кэш контекстов конфигуратора составов IPS. Кэш может быть общим, а может
/// быть создан для конкретной учётной записи пользователя.
/// </summary>
[Serializable]
public sealed class PdmConfiguratorContextsCache
{
  /// <summary>Генератор уникальных номеров</summary>
  private static long _index;
  /// <summary>Уникальный номер кэша</summary>
  public long Handle;
  /// <summary>
  /// Объект для потокобезопасного доступа к полям кэша извне
  /// </summary>
  public object SyncRoot = new object();
  /// <summary>
  /// Промежуток времени, в течение которого содержимое контекста конфигуратора составов считается актуальным
  /// </summary>
  public TimeSpan Timeout = new TimeSpan(23, 59, 59);
  /// <summary>Идентификатор пользователя, для которого создан кэш</summary>
  private long _userID;
  /// <summary>Кэш контекстов конфигуратора составов IPS</summary>
  private Dictionary<RelationPair, PdmConfiguratorContext> _items = new Dictionary<RelationPair, PdmConfiguratorContext>();

  /// <summary>Идентификатор пользователя, для которого создан кэш</summary>
  public long UserID
  {
    [DebuggerStepThrough] get => this._userID;
  }

  /// <summary>Кэш контекстов конфигуратора составов IPS</summary>
  public Dictionary<RelationPair, PdmConfiguratorContext> Items
  {
    [DebuggerStepThrough] get => this._items;
  }

  /// <summary>
  /// Создать экземпляр кэша контекстов конфигуратора составов IPS
  /// </summary>
  public PdmConfiguratorContextsCache()
  {
    ++PdmConfiguratorContextsCache._index;
    this.Handle = PdmConfiguratorContextsCache._index;
  }

  /// <summary>
  /// Создать экземпляр кэша контекстов конфигуратора составов IPS для конкретной учётной записи
  /// </summary>
  /// <param name="userID">Идентификатор пользователя</param>
  public PdmConfiguratorContextsCache(long userID)
    : this()
  {
    this._userID = userID;
  }

  /// <summary>Найти в кэше контекст с указанным ключом</summary>
  /// <param name="key">Ключ</param>
  /// <returns>Контекст или null</returns>
  private PdmConfiguratorContext GetValue(RelationPair key)
  {
    lock (this._items)
    {
      if (key != null)
      {
        if (this._items.ContainsKey(key))
          return this._items[key];
      }
    }
    return (PdmConfiguratorContext) null;
  }

  /// <summary>Кэшировать (либо удалить из кэша) указанное значение</summary>
  /// <param name="key">Ключ</param>
  /// <param name="value">Контекст или null для удаления контекста из кэша</param>
  private void SetValue(RelationPair key, PdmConfiguratorContext value)
  {
    if (key == null || key.Empty)
      return;
    if (value == null)
    {
      lock (this._items)
      {
        if (!this._items.ContainsKey(key))
          return;
        this._items.Remove(key);
      }
    }
    else
    {
      lock (this._items)
      {
        PdmConfiguratorContext configuratorContext = value;
        configuratorContext.ContextsCache = this;
        this._items[key] = configuratorContext;
      }
    }
  }

  /// <summary>Удалить из кэша всю информацию</summary>
  public void Reset()
  {
    lock (this._items)
      this._items.Clear();
  }

  /// <summary>Удалить из кэша записи с истёкшём сроком действия</summary>
  public void ResetExpired()
  {
    lock (this._items)
    {
      if (this._items.Count == 0)
        return;
      List<RelationPair> relationPairList = new List<RelationPair>();
      foreach (KeyValuePair<RelationPair, PdmConfiguratorContext> keyValuePair in this._items)
      {
        if (DateTime.UtcNow - keyValuePair.Value.ModifiedAt > this.Timeout)
          relationPairList.Add(keyValuePair.Key);
      }
      for (int index = 0; index < relationPairList.Count; ++index)
        this._items.Remove(relationPairList[index]);
    }
  }

  /// <summary>
  /// Удалить из кэша всю информацию, которая касается указанной опции
  /// </summary>
  /// <param name="option">Идентификатор версии объекта опции</param>
  /// <param name="userID">Идентификатор пользователя</param>
  public void ResetOption(long option, long userID)
  {
    Guid optionGuid = PdmConfiguratorCache.CacheFindOptionGuid(option);
    if (optionGuid == Guid.Empty)
      return;
    lock (this)
    {
      if (this._items.Count == 0)
        return;
      List<RelationPair> relationPairList = new List<RelationPair>();
      foreach (KeyValuePair<RelationPair, PdmConfiguratorContext> keyValuePair in this._items)
      {
        if (keyValuePair.Value.OptionsValues.ContainsKey(optionGuid) && (keyValuePair.Key.USER_ID == userID || userID == 0L))
          relationPairList.Add(keyValuePair.Key);
      }
      for (int index = 0; index < relationPairList.Count; ++index)
        this._items.Remove(relationPairList[index]);
    }
  }

  /// <summary>Работа с контекстом для указанной связи</summary>
  /// <param name="handle"></param>
  /// <param name="topObjectID"></param>
  /// <param name="topObjectType"></param>
  /// <param name="prjLinkID">Идентификатор связи</param>
  /// <param name="userID">Идентификатор пользователя</param>
  /// <returns>Контекст, назначенный указанной связи, или null</returns>
  public PdmConfiguratorContext this[
    long handle,
    long topObjectID,
    int topObjectType,
    long prjLinkID,
    long userID]
  {
    get => this.GetValue(new RelationPair(handle, topObjectID, topObjectType, prjLinkID, userID));
    set
    {
      this.SetValue(new RelationPair(handle, topObjectID, topObjectType, prjLinkID, userID), value);
    }
  }

  /// <summary>
  /// Работа с контекстом для указанного родительского объекта
  /// </summary>
  /// <param name="handle"></param>
  /// <param name="topObjectID"></param>
  /// <param name="topObjectType"></param>
  /// <param name="userID">Идентификатор пользователя</param>
  /// <param name="projID">Идентификатор версии родительского объекта</param>
  /// <param name="relType">Идентификатор типа связи</param>
  /// <returns>Контекст, назначенный указанному родительскому объекту, или null</returns>
  public PdmConfiguratorContext this[
    long handle,
    long topObjectID,
    int topObjectType,
    long userID,
    long projID,
    int relType]
  {
    get
    {
      return this.GetValue(new RelationPair(handle, topObjectID, topObjectType, 0L, userID, projID, relType, -1));
    }
    set
    {
      this.SetValue(new RelationPair(handle, topObjectID, topObjectType, 0L, userID, projID, relType, -1), value);
    }
  }

  /// <summary>Работа с контекстом для указанного ключа</summary>
  /// <param name="key">Ключ</param>
  /// <returns>Контекст, назначенный указанного ключа, или null</returns>
  public PdmConfiguratorContext this[RelationPair key]
  {
    get => this.GetValue(key);
    set => this.SetValue(key, value);
  }
}
