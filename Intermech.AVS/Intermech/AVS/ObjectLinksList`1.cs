// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.ObjectLinksList`1
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace Intermech.AVS;

/// <summary> Список объектов, представляющих обекты-пары идентификатор_объекта и набор_сущностей_ссылающихся_на_него </summary>
public class ObjectLinksList<T>
{
  private System.Collections.Generic.List<ObjectLinks<T>> _list;
  private ReadOnlyCollection<long> _cachedReadOnlyKeysList;
  private System.Collections.Generic.List<long> _keysList;
  private ReadOnlyCollection<ObjectLinks<T>> _cachedReadOnlyList;
  private Dictionary<long, ObjectLinks<T>> _objectIdToObjectLinkHash;

  /// <summary> Конструктор </summary>
  public ObjectLinksList()
  {
    this._keysList = new System.Collections.Generic.List<long>();
    this._list = new System.Collections.Generic.List<ObjectLinks<T>>();
    this._objectIdToObjectLinkHash = new Dictionary<long, ObjectLinks<T>>();
  }

  /// <summary> Конструктор </summary>
  public ObjectLinksList(int capacity)
  {
    this._keysList = new System.Collections.Generic.List<long>(capacity);
    this._list = new System.Collections.Generic.List<ObjectLinks<T>>(capacity);
    this._objectIdToObjectLinkHash = new Dictionary<long, ObjectLinks<T>>(capacity);
  }

  /// <summary> Получение списка, для доступа "снаружи" (для редактирования недоступен) </summary>
  public ReadOnlyCollection<ObjectLinks<T>> List
  {
    get
    {
      if (this._cachedReadOnlyList == null)
        this._cachedReadOnlyList = this._list.AsReadOnly();
      return this._cachedReadOnlyList;
    }
  }

  /// <summary> Получение списка зарегистрированых идентификаторов объектов, для доступа "снаружи" (для редактирования недоступен) </summary>
  public ReadOnlyCollection<long> RegisteredObjectIDs
  {
    get
    {
      if (this._cachedReadOnlyKeysList == null)
        this._cachedReadOnlyKeysList = this._keysList.AsReadOnly();
      return this._cachedReadOnlyKeysList;
    }
  }

  /// <summary> Получение списка сущностей, ссылающихся на объект с данным идентификатором (Вернёт null если объект не зарегистрирован в списке) </summary>
  /// <param name="objectID"> Идентификатор объекта </param>
  /// <returns> Список сущностей, ссылающихся на данный объект </returns>
  public ReadOnlyCollection<T> this[long objectID]
  {
    get => this.GetObjectLinksByObjectID(objectID)?.ObjectLinksCollection;
    set => this.RegisterObjectAndLinks(objectID, (IList<T>) value);
  }

  /// <summary> Получение объекта-пары идентификатор_объекта и набор_сущностей_ссылающихся_на_него по идентификатору объекта </summary>
  public ObjectLinks<T> GetObjectLinksByObjectID(long objectID)
  {
    ObjectLinks<T> objectLinksByObjectId = (ObjectLinks<T>) null;
    this._objectIdToObjectLinkHash.TryGetValue(objectID, out objectLinksByObjectId);
    return objectLinksByObjectId;
  }

  /// <summary> Регистрация идентификатора объекта </summary>
  public ObjectLinks<T> RegisterObject(long objectID)
  {
    ObjectLinks<T> objectLinks = this.GetObjectLinksByObjectID(objectID);
    if (objectLinks == null)
    {
      objectLinks = new ObjectLinks<T>(objectID);
      this._objectIdToObjectLinkHash[objectID] = objectLinks;
      this._list.Add(objectLinks);
      this._keysList.Add(objectID);
    }
    return objectLinks;
  }

  /// <summary> Регистрация идентификатора объекта и сущности, ссылающейся на него </summary>
  public ObjectLinks<T> RegisterObjectAndLink(long objectID, T objectLink)
  {
    ObjectLinks<T> objectLinks = this.RegisterObject(objectID);
    objectLinks.ConnectToObjectLink(objectLink);
    return objectLinks;
  }

  /// <summary> Регистрация идентификатора объекта и массива сущностей, ссылающихся на него </summary>
  public ObjectLinks<T> RegisterObjectAndLinks(long objectID, T[] objectLinks)
  {
    ObjectLinks<T> objectLinks1 = this.RegisterObject(objectID);
    objectLinks1.ConnectToObjectLinks(objectLinks);
    return objectLinks1;
  }

  /// <summary> Регистрация идентификатора объекта и списка сущностей, ссылающихся на него </summary>
  public ObjectLinks<T> RegisterObjectAndLinks(long objectID, IList<T> objectLinks)
  {
    ObjectLinks<T> objectLinks1 = this.RegisterObject(objectID);
    objectLinks1.ConnectToObjectLinks(objectLinks);
    return objectLinks1;
  }

  /// <summary> Дерегистрация идентификатора объекта и сущности, ссылающейся на него </summary>
  public void UnregisterObjectLink(long objectID, T objectLink)
  {
    this.GetObjectLinksByObjectID(objectID)?.DisconnectFromObjectLink(objectLink);
  }

  /// <summary> Дерегистрация идентификатора объекта и массива сущностей, ссылающихся на него </summary>
  public void UnregisterObjectLinks(long objectID, T[] objectLinks)
  {
    this.GetObjectLinksByObjectID(objectID)?.DisconnectFromObjectLinks(objectLinks);
  }

  /// <summary> Дерегистрация идентификатора объекта и списка сущностей, ссылающихся на него </summary>
  public void UnregisterObjectLinks(long objectID, IList<T> objectLinks)
  {
    this.GetObjectLinksByObjectID(objectID)?.DisconnectFromObjectLinks(objectLinks);
  }
}
