// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IGroupingObjectsCache
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс кэша для хранения списков идентификаторов найденных группирующих объектов для
/// указанных идентификаторов элементов пространства навигации
/// </summary>
public interface IGroupingObjectsCache
{
  /// <summary>
  /// Получить из кэша список идентификаторов ранее найденных группирующих объектов
  /// для указанного идентификатора элемента пространства навигации с указанным режимом поиска
  /// </summary>
  /// <param name="nodeID">Идентификатор элемента пространства навигации</param>
  /// <param name="searchMode">Режим поиска</param>
  /// <returns>Список группирующих объектов или null, если значение не было найдено или данные устарели</returns>
  SearchGroupingObjects GetGroupingObjects(INodeID nodeID, string analyzerName);

  /// <summary>
  /// Сохранить в кэше новые данные по группирующим объектам
  /// для указанного идентификатора элемента пространства навигации с указанным режимом поиска
  /// </summary>
  /// <param name="nodeID">Идентификатор элемента пространства навигации</param>
  /// <param name="searchMode">Режим поиска</param>
  /// <param name="groupingObjects">Список найденных группирующих объектов</param>
  void SetGroupingObjects(
    INodeID nodeID,
    string analyzerName,
    SearchGroupingObjects groupingObjects);

  /// <summary>
  /// Удалить из кэша данные по группирующим объектам
  /// для указанного идентификатора элемента пространства навигации с указанным режимом поиска
  /// </summary>
  /// <param name="nodeID">Идентификатор элемента пространства навигации</param>
  /// <param name="searchMode">Режим поиска</param>
  void RemoveGroupingObjects(INodeID nodeID, string analyzerName);

  /// <summary>Полностью очистить кэш</summary>
  void Reset();
}
