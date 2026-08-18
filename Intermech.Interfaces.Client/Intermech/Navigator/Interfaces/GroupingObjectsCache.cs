// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.GroupingObjectsCache
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Кэш для хранения списков идентификаторов найденных группирующих объектов для
/// указанных идентификаторов элементов пространства навигации
/// </summary>
public class GroupingObjectsCache : IGroupingObjectsCache
{
  /// <summary>Объект для синхронизации доступа к кэшу</summary>
  public object SyncRoot = new object();
  /// <summary>Кэш найденных группирующих объектов</summary>
  protected Dictionary<GroupingObjectsCacheKey, SearchGroupingObjects> _cache = new Dictionary<GroupingObjectsCacheKey, SearchGroupingObjects>();

  /// <summary>
  /// Получить из кэша список идентификаторов ранее найденных группирующих объектов
  /// для указанного идентификатора элемента пространства навигации с указанным режимом поиска
  /// </summary>
  /// <param name="nodeID">Идентификатор элемента пространства навигации</param>
  /// <param name="searchMode">Режим поиска</param>
  /// <returns>Список идентификаторов или null, если значение не было найдено или данные устарели</returns>
  public virtual SearchGroupingObjects GetGroupingObjects(INodeID nodeID, string analyzerName)
  {
    if (nodeID == null)
      return (SearchGroupingObjects) null;
    lock (this.SyncRoot)
    {
      GroupingObjectsCacheKey key = new GroupingObjectsCacheKey(nodeID, analyzerName);
      return !this._cache.ContainsKey(key) ? (SearchGroupingObjects) null : this._cache[key].Clone() as SearchGroupingObjects;
    }
  }

  /// <summary>
  /// Сохранить в кэше новые данные по группирующим объектам
  /// для указанного идентификатора элемента пространства навигации с указанным режимом поиска
  /// </summary>
  /// <param name="nodeID">Идентификатор элемента пространства навигации</param>
  /// <param name="searchMode">Режим поиска</param>
  /// <param name="groupingObjects">Список идентификаторов версий найденных группирующих объектов</param>
  public virtual void SetGroupingObjects(
    INodeID nodeID,
    string analyzerName,
    SearchGroupingObjects groupingObjects)
  {
    if (nodeID == null || groupingObjects == null)
      return;
    lock (this.SyncRoot)
      this._cache[new GroupingObjectsCacheKey(nodeID, analyzerName)] = groupingObjects.Clone() as SearchGroupingObjects;
  }

  /// <summary>
  /// Удалить из кэша данные по группирующим объектам
  /// для указанного идентификатора элемента пространства навигации с указанным режимом поиска
  /// </summary>
  /// <param name="nodeID">Идентификатор элемента пространства навигации</param>
  /// <param name="searchMode">Режим поиска</param>
  public virtual void RemoveGroupingObjects(INodeID nodeID, string analyzerName)
  {
    if (nodeID == null)
      return;
    lock (this.SyncRoot)
    {
      GroupingObjectsCacheKey key = new GroupingObjectsCacheKey(nodeID, analyzerName);
      if (!this._cache.ContainsKey(key))
        return;
      this._cache.Remove(key);
    }
  }

  /// <summary>Полностью очистить кэш</summary>
  public virtual void Reset()
  {
    lock (this.SyncRoot)
      this._cache.Clear();
  }
}
