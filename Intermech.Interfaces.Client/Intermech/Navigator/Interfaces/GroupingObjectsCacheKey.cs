// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.GroupingObjectsCacheKey
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>Ключ в кэше группирующих объектов</summary>
public class GroupingObjectsCacheKey
{
  /// <summary>Идентификатор элемента пространства навигации</summary>
  public INodeID NodeID;
  /// <summary>
  /// Режим поиска, по которому были найдены группирующие объекты
  /// </summary>
  public string SearchMode;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="nodeID">Идентификатор элемента пространства навигации</param>
  /// <param name="searchMode">Режим поиска, по которому были найдены группирующие объекты</param>
  public GroupingObjectsCacheKey(INodeID nodeID, string analyzerName)
  {
    this.NodeID = nodeID;
    this.SearchMode = analyzerName;
  }

  /// <summary>Выполнить сравнение с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj)
  {
    if (!(obj is GroupingObjectsCacheKey groupingObjectsCacheKey))
      return base.Equals(obj);
    return this.SearchMode == groupingObjectsCacheKey.SearchMode && this.NodeID != null && this.NodeID.Equals((object) groupingObjectsCacheKey.NodeID);
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode()
  {
    return (this.SearchMode != null ? this.SearchMode.GetHashCode() : 0) ^ (this.NodeID != null ? this.NodeID.GetHashCode() : 0);
  }
}
