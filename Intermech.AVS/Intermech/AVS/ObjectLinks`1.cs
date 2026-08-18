// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.ObjectLinks`1
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace Intermech.AVS;

/// <summary> Пара идентификатор_объекта и набор_сущностей_ссылающихся_на_него </summary>
public class ObjectLinks<T>
{
  private long _objectID;
  private List<T> _objectLinks = new List<T>();
  private ReadOnlyCollection<T> _cachedReadOnlyObjectLinks;

  /// <summary> Конструктор </summary>
  /// <param name="objectID"> Идентификатор объекта </param>
  public ObjectLinks(long objectID) => this._objectID = objectID;

  /// <summary> Конструктор </summary>
  /// <param name="objectID"> Идентификатор объекта </param>
  /// <param name="objectLink"> Сущность, ссылающаяся на объект </param>
  public ObjectLinks(long objectID, T objectLink)
    : this(objectID)
  {
    this.ConnectToObjectLink(objectLink);
  }

  /// <summary> Конструктор </summary>
  /// <param name="objectID"> Идентификатор объекта </param>
  /// <param name="objectLinks"> Набор сущностей, ссылающихся на объект </param>
  public ObjectLinks(long objectID, T[] objectLinks)
    : this(objectID)
  {
    this.ConnectToObjectLinks(objectLinks);
  }

  /// <summary> Конструктор </summary>
  /// <param name="objectID"> Идентификатор объекта </param>
  /// <param name="objectLinks"> Набор сущностей, ссылающихся на объект </param>
  public ObjectLinks(long objectID, IList<T> objectLinks)
    : this(objectID)
  {
    this.ConnectToObjectLinks(objectLinks);
  }

  /// <summary> Идентификатор объекта </summary>
  public long ObjectID => this._objectID;

  /// <summary> Получение списка сущностей, ссылающихся на объект, для доступа "снаружи" (недоступен для редактирования) </summary>
  /// <returns> Список сущностей, ссылающихся на объект, доступный "снаружи" (недоступен для редактирования) </returns>
  public ReadOnlyCollection<T> ObjectLinksCollection
  {
    get
    {
      if (this._cachedReadOnlyObjectLinks == null)
        this._cachedReadOnlyObjectLinks = this._objectLinks.AsReadOnly();
      return this._cachedReadOnlyObjectLinks;
    }
  }

  /// <summary> Регистрация связи, ссылающейся на объект </summary>
  /// <param name="objectLink"> Сущность, ссылающаяся на объект </param>
  public void ConnectToObjectLink(T objectLink)
  {
    if (this._objectLinks.Contains(objectLink))
      return;
    this._objectLinks.Add(objectLink);
  }

  /// <summary> Регистрация связей, ссылающихся на объект </summary>
  /// <param name="objectLinks"> Набор сущностей, ссылающихся на объект </param>
  public void ConnectToObjectLinks(T[] objectLinks)
  {
    if (objectLinks == null)
      return;
    foreach (T objectLink in objectLinks)
      this.ConnectToObjectLink(objectLink);
  }

  /// <summary> Регистрация связей, ссылающихся на объект </summary>
  /// <param name="objectLinks"> Набор сущностей, ссылающихся на объект </param>
  public void ConnectToObjectLinks(IList<T> objectLinks)
  {
    if (objectLinks == null)
      return;
    foreach (T objectLink in (IEnumerable<T>) objectLinks)
      this.ConnectToObjectLink(objectLink);
  }

  /// <summary> Дерегистрация связи, ссылающейся на объект </summary>
  /// <param name="objectLink"> Сущность, ссылающаяся на объект </param>
  public void DisconnectFromObjectLink(T objectLink)
  {
    int index = this._objectLinks.IndexOf(objectLink);
    if (index == -1)
      return;
    this._objectLinks.RemoveAt(index);
  }

  /// <summary> Дерегистрация связей, ссылающихся на объект </summary>
  /// <param name="objectLinks"> Набор сущностей, ссылающихся на объект </param>
  public void DisconnectFromObjectLinks(T[] objectLinks)
  {
    if (objectLinks == null)
      return;
    foreach (T objectLink in objectLinks)
      this.DisconnectFromObjectLink(objectLink);
  }

  /// <summary> Дерегистрация связей, ссылающихся на объект </summary>
  /// <param name="objectLinks"> Набор сущностей, ссылающихся на объект </param>
  public void DisconnectFromObjectLinks(IList<T> objectLinks)
  {
    if (objectLinks == null)
      return;
    foreach (T objectLink in (IEnumerable<T>) objectLinks)
      this.DisconnectFromObjectLink(objectLink);
  }
}
