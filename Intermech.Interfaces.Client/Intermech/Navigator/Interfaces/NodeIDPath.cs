// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.NodeIDPath
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Путь к элементу пространства навигации. Путь основан на дескрипторе корневого узла и
/// массиве унифицированных идентификаторов родительских элементов.
/// </summary>
public class NodeIDPath : IEnumerable, ICloneable
{
  /// <summary>Описание корневого элемента пространства навигации</summary>
  private IDescriptor _rootDescriptor;
  /// <summary>
  /// Список унифицированных идентификаторов родительских элементов пространства навигации
  /// </summary>
  private ArrayList _items;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="rootDescriptor">Описание корневого элемента пространства навигации</param>
  public NodeIDPath(IDescriptor rootDescriptor)
  {
    this._rootDescriptor = rootDescriptor;
    this._items = new ArrayList();
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="path">Путь к элементу пространства навигации</param>
  public NodeIDPath(NodeIDPath path)
  {
    this._rootDescriptor = path._rootDescriptor;
    this._items = path._items.Clone() as ArrayList;
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="path">Путь к элементу пространства навигации</param>
  /// <param name="nodeID">Идентификатор элемента пространства навигации, который будет добавлен в путь</param>
  public NodeIDPath(NodeIDPath path, INodeID nodeID)
    : this(path)
  {
    this._items.Add((object) nodeID);
  }

  /// <summary>Описание корневого элемента пространства навигации</summary>
  public IDescriptor RootDescriptor
  {
    get => this._rootDescriptor;
    set => this._rootDescriptor = value;
  }

  /// <summary>
  /// Количество идентификаторов элементов пространства навигации, которое содержится в пути
  /// </summary>
  public int Length => this._items.Count;

  /// <summary>
  /// Получить унифицированный идентификатор элемента пространства навигации с указанным индексом
  /// </summary>
  /// <param name="Index">Индекс унифицированного идентификатора элемента пространства навигации</param>
  /// <returns>Унифицированный идентификатор элемента пространства навигации с указанным индексом</returns>
  public INodeID this[int Index] => this._items[Index] as INodeID;

  /// <summary>
  /// Получить унифицированный идентификатор первого элемента пространства навигации из пути
  /// </summary>
  public INodeID FirstID => this._items.Count <= 0 ? (INodeID) null : this._items[0] as INodeID;

  /// <summary>
  /// Получить последний унифицированный идентификатор элемента пространства навигации из пути
  /// </summary>
  public INodeID LastID
  {
    get => this._items.Count <= 0 ? (INodeID) null : this._items[this._items.Count - 1] as INodeID;
  }

  /// <summary>Очистить элементы в пути, сохранив дескриптор</summary>
  public void Clear() => this._items.Clear();

  /// <summary>
  /// Добавить в путь очередной идентификатор элемента пространства навигации
  /// </summary>
  /// <param name="NodeID">Добавляемый идентификатор элемента пространства навигации</param>
  public void Add(INodeID NodeID) => this._items.Add((object) NodeID);

  /// <summary>
  /// Добавить описание идентификатора элемента пространства навигации в начало или конец пути
  /// </summary>
  /// <param name="NodeID">Добавляемый идентификатор элемента пространства навигации</param>
  /// <param name="ToEnd">true - добавить описание узла в конец пути, иначе вставить его в начало пути</param>
  public void Add(INodeID NodeID, bool ToEnd)
  {
    if (ToEnd)
      this._items.Add((object) NodeID);
    else
      this._items.Insert(0, (object) NodeID);
  }

  /// <summary>
  /// Удалить последний идентификатор элемента пространства навигации из пути
  /// </summary>
  public void RemoveLast() => this._items.RemoveAt(this._items.Count - 1);

  /// <summary>Вернуть перечислитель для элементов пути</summary>
  /// <returns>Перечислитель для элементов пути</returns>
  public IEnumerator GetEnumerator() => this._items.GetEnumerator();

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public object Clone() => (object) new NodeIDPath(this);
}
