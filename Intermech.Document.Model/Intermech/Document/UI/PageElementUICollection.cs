// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.PageElementUICollection
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Collections;
using System.Diagnostics;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Коллекция интерфейсных элементов</summary>
public class PageElementUICollection : IList, ICollection, IEnumerable
{
  private ArrayList arrayList = new ArrayList();
  private PageElementUI owner;

  /// <summary>Конструктор</summary>
  public PageElementUICollection()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="owner">Владелец</param>
  public PageElementUICollection(PageElementUI owner) => this.owner = owner;

  /// <summary>Типизированный индексатор</summary>
  public PageElementUI this[int index]
  {
    [DebuggerStepThrough] get => (PageElementUI) this.arrayList[index];
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (this[index] == value)
        return;
      this.RemoveAt(index);
      this.Insert(index, value);
    }
  }

  /// <summary>Добавить подузел</summary>
  public virtual int Add(PageElementUI value)
  {
    if (value == null)
      return -1;
    if (value.Parent != null && value.Parent != this.owner)
      value.Parent.PageElementUIs.Remove(value);
    int num = this.arrayList.IndexOf((object) value);
    if (num == -1)
    {
      num = this.arrayList.Add((object) value);
      value.AssignParent(this.owner);
      if (this.owner != null)
        this.owner.OnChildElementAdded(new PageElementUI_EventArgs(value));
    }
    return num;
  }

  /// <summary>Поменять местами элементы</summary>
  /// <param name="i1">Индекс элемента 1</param>
  /// <param name="i2">Индекс элемента 2</param>
  public virtual void Exchange(int i1, int i2)
  {
    if (i1 == i2)
      return;
    object array = this.arrayList[i1];
    this.arrayList[i1] = this.arrayList[i2];
    this.arrayList[i2] = array;
  }

  /// <summary>Вставить узел в определенную позицию</summary>
  /// <param name="index">Индекс с которым должен быть вставлен узел</param>
  /// <param name="value">Узел</param>
  public virtual void Insert(int index, PageElementUI value)
  {
    if (value == null)
      return;
    int i2 = this.IndexOf(value);
    if (i2 == -1)
    {
      if (value.Parent != null)
        value.Parent.PageElementUIs.Remove(value);
      this.arrayList.Insert(index, (object) value);
      value.AssignParent(this.owner);
      if (this.owner == null)
        return;
      this.owner.OnChildElementAdded(new PageElementUI_EventArgs(value));
    }
    else
      this.Exchange(index, i2);
  }

  /// <summary>Удалить узел из коллекции</summary>
  /// <param name="value">Удаляемый узел</param>
  public virtual void Remove(PageElementUI value)
  {
    if (value == null)
      return;
    int index = this.arrayList.IndexOf((object) value);
    if (index == -1)
      return;
    this.RemoveAt(index);
  }

  /// <summary>Удалить узел с заданным индексом</summary>
  /// <param name="index">Индекс удаляемого узла</param>
  public virtual void RemoveAt(int index)
  {
    PageElementUI element = this[index];
    this.arrayList.RemoveAt(index);
    element.AssignParent((PageElementUI) null);
    if (this.owner == null)
      return;
    this.owner.OnChildElementRemoved(new PageElementUI_EventArgs(element));
  }

  /// <summary>Очистить коллекцию</summary>
  [DebuggerStepThrough]
  public virtual void Clear()
  {
    for (int index = this.arrayList.Count - 1; index >= 0; --index)
    {
      if (index < this.arrayList.Count)
        this.RemoveAt(index);
    }
  }

  /// <summary>Определяет находится ли объект в коллекции</summary>
  /// <param name="value">Искомый объект</param>
  [DebuggerStepThrough]
  public bool Contains(PageElementUI value) => this.arrayList.Contains((object) value);

  /// <summary>Индекс узла в коллекции</summary>
  /// <param name="value">Узел</param>
  /// <returns>Индекс узла</returns>
  [DebuggerStepThrough]
  public int IndexOf(PageElementUI value) => this.arrayList.IndexOf((object) value);

  /// <summary>Индексатор интерфейса</summary>
  object IList.this[int index]
  {
    [DebuggerStepThrough] get => (object) this[index];
    set => this[index] = (PageElementUI) value;
  }

  /// <summary>Добавить подузел</summary>
  [DebuggerStepThrough]
  int IList.Add(object value) => this.Add((PageElementUI) value);

  /// <summary>Определяет находится ли объект в коллекции</summary>
  /// <param name="value">Искомый объект</param>
  [DebuggerStepThrough]
  bool IList.Contains(object value) => this.Contains(value as PageElementUI);

  /// <summary>Индекс узла в коллекции</summary>
  /// <param name="value">Узел</param>
  /// <returns>Индекс узла</returns>
  [DebuggerStepThrough]
  int IList.IndexOf(object value) => this.IndexOf(value as PageElementUI);

  /// <summary>Вставить узел в определенную позицию</summary>
  /// <param name="index">Индекс с которым должен быть вставлен узел</param>
  /// <param name="value">Узел</param>
  [DebuggerStepThrough]
  void IList.Insert(int index, object value) => this.Insert(index, (PageElementUI) value);

  /// <summary>Удалить узел из коллекции</summary>
  /// <param name="value">Удаляемый узел</param>
  [DebuggerStepThrough]
  void IList.Remove(object value) => this.Remove(value as PageElementUI);

  /// <summary>Размер зафиксирован</summary>
  public bool IsFixedSize
  {
    [DebuggerStepThrough] get => this.arrayList.IsFixedSize;
  }

  /// <summary>Только чтение</summary>
  public bool IsReadOnly
  {
    [DebuggerStepThrough] get => this.arrayList.IsReadOnly;
  }

  /// <summary>Количество элементов</summary>
  public int Count
  {
    [DebuggerStepThrough] get => this.arrayList.Count;
  }

  /// <summary>Синхронизирован</summary>
  public bool IsSynchronized
  {
    [DebuggerStepThrough] get => this.arrayList.IsSynchronized;
  }

  /// <summary>When implemented by a class, gets an object that can be used to synchronize access to the ICollection</summary>
  public object SyncRoot
  {
    [DebuggerStepThrough] get => this.arrayList.SyncRoot;
  }

  /// <summary>Копирует элементы коллекции в массив</summary>
  /// <param name="array">Массив, место записи</param>
  /// <param name="index">Индекс элемента, с которого начинается копирование</param>
  [DebuggerStepThrough]
  public void CopyTo(Array array, int index) => this.arrayList.CopyTo(array, index);

  /// <summary>Возвращает перечислитель IEnumerator</summary>
  /// <returns>Перечислитель IEnumerator</returns>
  [DebuggerStepThrough]
  public IEnumerator GetEnumerator() => this.arrayList.GetEnumerator();
}
