// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.CustomTechClassList`1
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route;

/// <summary>Базовый список для технологических объектов</summary>
public class CustomTechClassList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable where T : CustomTechClass
{
  /// <summary>Owner item</summary>
  private readonly CustomTechClass _owner;
  /// <summary>
  /// 
  /// </summary>
  private readonly List<T> _items = new List<T>();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="value"></param>
  protected void SetOwnerModified(bool value)
  {
    if (this._owner == null)
      return;
    this._owner.Modified = value;
  }

  /// <summary>Конструктор</summary>
  /// <param name="owner">Владелец / родительский объект</param>
  public CustomTechClassList(CustomTechClass owner) => this._owner = owner;

  /// <summary>Добавить объект</summary>
  /// <param name="item">Добавляемый элемент</param>
  public void Add(T item)
  {
    this._items.Add(item);
    this.SetOwnerModified(true);
  }

  /// <summary>Вставить объект</summary>
  /// <param name="index">Индекс в списке для вставки</param>
  /// <param name="item">Добавляемый элемент</param>
  public void Insert(int index, T item)
  {
    this._items.Insert(index, item);
    this.SetOwnerModified(true);
  }

  /// <summary>Удалить объект</summary>
  /// <param name="item">Удаляемый элемент</param>
  public void Remove(T item)
  {
    this._items.Remove(item);
    this.SetOwnerModified(true);
  }

  /// <summary>Очистить список</summary>
  public void Clear()
  {
    this._items.Clear();
    this.SetOwnerModified(true);
  }

  /// <summary>Удалить объекты из базы</summary>
  /// <param name="item">Удаляемый элемент из базы</param>
  public void RemoveFromBase(T item)
  {
    if ((object) item == null || item.ObjectId == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.GetObject(item.ObjectId, false);
  }

  /// <summary>Индекс элемента в списке</summary>
  /// <param name="item"></param>
  /// <returns></returns>
  public int IndexOf(T item) => this._items.IndexOf(item);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="comparer"></param>
  public void Sort(IComparer<T> comparer) => this._items.Sort(comparer);

  public void Sort(Comparison<T> comparison) => this._items.Sort(comparison);

  /// <summary>
  /// 
  /// </summary>
  public int Count => this._items.Count;

  public bool IsReadOnly { get; }

  /// <summary>Индексатор</summary>
  /// <param name="index">Индекс</param>
  /// <returns></returns>
  public T this[int index]
  {
    get => this._items[index];
    set => this._items[index] = value;
  }

  public void RemoveAt(int index)
  {
    this._items.RemoveAt(index);
    this.SetOwnerModified(true);
  }

  public void CopyTo(T[] array, int arrayIndex) => throw new NotImplementedException();

  bool ICollection<T>.Remove(T item)
  {
    int num = this._items.Remove(item) ? 1 : 0;
    this.SetOwnerModified(true);
    return num != 0;
  }

  public bool Contains(T item) => this._items.Contains(item);

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public IEnumerator<T> GetEnumerator() => (IEnumerator<T>) this._items.GetEnumerator();

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
}
