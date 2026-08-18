// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.DocumentTreeNodeCollection
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Коллекция узлов дерева документа. Только для внутреннего использования для хранения дочерних узлов.
/// Должна принадлежать узлу дерева документа</summary>
[Serializable]
public class DocumentTreeNodeCollection : 
  IList<DocumentTreeNode>,
  ICollection<DocumentTreeNode>,
  IEnumerable<DocumentTreeNode>,
  IEnumerable,
  ICloneable
{
  /// <summary>Внутренний список узлов коллекции</summary>
  [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
  private List<DocumentTreeNode> arrayList;
  /// <summary>Владелец коллекции</summary>
  [NonSerialized]
  private DocumentTreeNode owner;

  /// <summary>Конструктор</summary>
  /// <param name="owner">Владелец</param>
  public DocumentTreeNodeCollection(DocumentTreeNode owner)
  {
    this.owner = owner;
    this.arrayList = new List<DocumentTreeNode>();
  }

  /// <summary>Конструктор</summary>
  /// <param name="owner">Владелец</param>
  /// <param name="capacity">Первоначальный размер коллекции</param>
  public DocumentTreeNodeCollection(DocumentTreeNode owner, int capacity)
  {
    this.owner = owner;
    this.arrayList = new List<DocumentTreeNode>(capacity);
  }

  /// <summary>Владелец коллекции</summary>
  public DocumentTreeNode Owner
  {
    [DebuggerStepThrough] get => this.owner;
  }

  /// <summary>Размер зафиксирован</summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public bool IsFixedSize
  {
    [DebuggerStepThrough] get => ((IList) this.arrayList).IsFixedSize;
  }

  /// <summary>Только чтение</summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public bool IsReadOnly
  {
    [DebuggerStepThrough] get => ((IList) this.arrayList).IsReadOnly;
  }

  /// <summary>Количество элементов</summary>
  public int Count
  {
    [DebuggerStepThrough] get => this.arrayList.Count;
  }

  /// <summary>Синхронизирован</summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public bool IsSynchronized
  {
    [DebuggerStepThrough] get => ((ICollection) this.arrayList).IsSynchronized;
  }

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public object SyncRoot
  {
    [DebuggerStepThrough] get => ((ICollection) this.arrayList).SyncRoot;
  }

  internal void SetOwner(DocumentTreeNode value, bool updateUI, bool updateLayout, bool isLoading)
  {
    if (this.owner == value)
      return;
    this.owner = value;
    if (this.owner != null && this.owner.IsVirtualNode)
      return;
    for (int index = 0; index < this.Count; ++index)
      this.arrayList[index].AssignParent(this.owner, updateUI, updateLayout, isLoading);
  }

  /// <summary>Клонировать коллекцию вместе с узлами</summary>
  /// <returns>Копия коллекции узлов</returns>
  [Obsolete("Нужно пользоваться методами DocumentTreeNode")]
  public DocumentTreeNodeCollection Clone() => this._Clone();

  private DocumentTreeNodeCollection _Clone()
  {
    DocumentTreeNodeCollection treeNodeCollection = new DocumentTreeNodeCollection((DocumentTreeNode) null, this.arrayList.Count);
    if (this.owner == null || !this.owner.IsVirtualNode)
    {
      int index = 0;
      for (int count = this.arrayList.Count; index < count; ++index)
        treeNodeCollection._Add(this.arrayList[index].Clone(true, true));
    }
    else
    {
      int index = 0;
      for (int count = this.arrayList.Count; index < count; ++index)
        treeNodeCollection._Add(this.arrayList[index]);
    }
    return treeNodeCollection;
  }

  /// <summary>Типизированный индексатор</summary>
  public DocumentTreeNode this[int index]
  {
    [DebuggerStepThrough] get => this.arrayList[index];
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (this[index] == value)
        return;
      this._RemoveAt(index);
      this._Insert(index, value);
    }
  }

  /// <summary>Внутренний метод добавления узла в коллекцию</summary>
  /// <param name="value">Узел</param>
  /// <returns>Индекс узла в коллекции</returns>
  internal int AddInternal(DocumentTreeNode value)
  {
    this.arrayList.Add(value);
    return this.arrayList.Count - 1;
  }

  /// <summary>Добавить подузел</summary>
  [Obsolete("Нужно пользоваться методами DocumentTreeNode")]
  public int Add(DocumentTreeNode value) => this._Add(value);

  /// <summary>Добавить подузел</summary>
  private int _Add(DocumentTreeNode value)
  {
    if (this.owner != null)
      return this.owner.AddChildNode(value, false, true, false, false);
    this.arrayList.Add(value);
    return this.arrayList.Count - 1;
  }

  /// <summary>Поменять узлы местами</summary>
  /// <param name="i1">Индекс первого узла</param>
  /// <param name="i2">Индекс второго узла</param>
  public void Exchange(int i1, int i2)
  {
    if (i1 == i2)
      return;
    object array = (object) this.arrayList[i1];
    this.arrayList[i1] = this.arrayList[i2];
    this.arrayList[i2] = array as DocumentTreeNode;
    if (this.owner == null)
      return;
    this.owner.OnChildNodesPositionExchanged(new ChildNodesPositionExchanged_EventArgs(i1, i2));
  }

  /// <summary>Внутренний метод вставки узла</summary>
  /// <param name="index">Индекс с которым нужно вставить узел</param>
  /// <param name="value">Узел</param>
  internal void InsertInternal(int index, DocumentTreeNode value)
  {
    this.arrayList.Insert(index, value);
  }

  /// <summary>Вставить узел в определенную позицию</summary>
  /// <param name="index">Индекс с которым должен быть вставлен узел</param>
  /// <param name="value">Узел</param>
  [Obsolete("Нужно пользоваться методами DocumentTreeNode")]
  public void Insert(int index, DocumentTreeNode value) => this._Insert(index, value);

  /// <summary>Вставить узел в определенную позицию</summary>
  /// <param name="index">Индекс с которым должен быть вставлен узел</param>
  /// <param name="value">Узел</param>
  private void _Insert(int index, DocumentTreeNode value)
  {
    if (this.owner != null)
      this.owner.InsertChildNode(index, value, false, true, true, true);
    else
      this.arrayList.Insert(index, value);
  }

  /// <summary>Удалить узел из коллекции</summary>
  /// <param name="value">Удаляемый узел</param>
  [Obsolete("Нужно пользоваться методами DocumentTreeNode")]
  public void Remove(DocumentTreeNode value) => this._Remove(value);

  /// <summary>Удалить узел из коллекции</summary>
  /// <param name="value">Удаляемый узел</param>
  private bool _Remove(DocumentTreeNode value)
  {
    if (value != null)
    {
      int index = this.arrayList.IndexOf(value);
      if (index != -1)
      {
        this._RemoveAt(index);
        return true;
      }
    }
    return false;
  }

  /// <summary>Внутренний метод удаления узла</summary>
  /// <param name="index">Индекс удаляемого узла</param>
  internal void RemoveAtInternal(int index) => this.arrayList.RemoveAt(index);

  /// <summary>Удалить узел с заданным индексом</summary>
  /// <param name="index">Индекс удаляемого узла</param>
  [Obsolete("Нужно пользоваться методами DocumentTreeNode")]
  public void RemoveAt(int index) => this._RemoveAt(index);

  private void _RemoveAt(int index)
  {
    if (this.owner != null)
      this.owner.RemoveChildNodeAt(index, true, true);
    else
      this.arrayList.RemoveAt(index);
  }

  /// <summary>Очистить коллекцию. Выполняет полную процедуру удаление узлов</summary>
  [DebuggerStepThrough]
  [Obsolete("Нужно пользоваться методами DocumentTreeNode")]
  public void Clear()
  {
    for (int index = this.arrayList.Count - 1; index >= 0; --index)
      this.RemoveAt(index);
  }

  internal void ClearInternal()
  {
    if (this.arrayList == null)
      return;
    this.arrayList.Clear();
  }

  /// <summary>Определяет находится ли объект в коллекции</summary>
  /// <param name="value">Искомый объект</param>
  [DebuggerStepThrough]
  public bool Contains(DocumentTreeNode value) => this.arrayList.Contains(value);

  /// <summary>Индекс узла в коллекции</summary>
  /// <param name="value">Узел</param>
  /// <returns>Индекс узла</returns>
  [DebuggerStepThrough]
  public int IndexOf(DocumentTreeNode value) => this.arrayList.IndexOf(value);

  /// <summary>
  /// Найти первый элемент согласно условию и вернуть его индекс в коллекции
  /// </summary>
  /// <param name="match">Делегат задающий условие поиска</param>
  /// <returns>Индекс найденного элемента, или -1, если элемент не найден</returns>
  public int FindIndex(Predicate<DocumentTreeNode> match) => this.arrayList.FindIndex(match);

  /// <summary>
  /// Найти первый элемент согласно условию и вернуть его индекс в коллекции
  /// </summary>
  /// <param name="startIndex">Индекс элемента с которого нужно начинать поиск</param>
  /// <param name="match">Делегат задающий условие поиска</param>
  /// <returns>Индекс найденного элемента, или -1, если элемент не найден</returns>
  public int FindIndex(int startIndex, Predicate<DocumentTreeNode> match)
  {
    return this.arrayList.FindIndex(startIndex, match);
  }

  /// <summary>
  /// Найти первый элемент согласно условию и вернуть его индекс в коллекции
  /// </summary>
  /// <param name="startIndex">Индекс элемента с которого нужно начинать поиск</param>
  /// <param name="count">Количество элементов от старта, среди которых нужно искать</param>
  /// <param name="match">Делегат задающий условие поиска</param>
  /// <returns>Индекс найденного элемента, или -1, если элемент не найден</returns>
  public int FindIndex(int startIndex, int count, Predicate<DocumentTreeNode> match)
  {
    return this.arrayList.FindIndex(startIndex, count, match);
  }

  [Obsolete("Нужно пользоваться методами DocumentTreeNode")]
  public void CopyTo(DocumentTreeNode[] array, int arrayIndex)
  {
    this.arrayList.CopyTo(array, arrayIndex);
  }

  void ICollection<DocumentTreeNode>.Add(DocumentTreeNode item)
  {
    throw new NotImplementedException();
  }

  bool ICollection<DocumentTreeNode>.Remove(DocumentTreeNode item) => this._Remove(item);

  [DebuggerStepThrough]
  IEnumerator<DocumentTreeNode> IEnumerable<DocumentTreeNode>.GetEnumerator()
  {
    return (IEnumerator<DocumentTreeNode>) this.arrayList.GetEnumerator();
  }

  [DebuggerStepThrough]
  public IEnumerator GetEnumerator() => (IEnumerator) this.arrayList.GetEnumerator();

  /// <summary>Реализация интерфейса ICloneable</summary>
  /// <returns>Полную копию коллекции</returns>
  [DebuggerStepThrough]
  object ICloneable.Clone() => (object) this._Clone();

  /// <summary>Сравнить содержимое списков</summary>
  /// <param name="list1">Первый список</param>
  /// <param name="list2">Второй список</param>
  /// <returns>true, если содержимое списков идентично</returns>
  public static bool ContentEquals(IList<DocumentTreeNode> list1, IList<DocumentTreeNode> list2)
  {
    if (list1 == list2)
      return true;
    if (list1 == null || list2 == null || list1.Count != list2.Count)
      return false;
    int index = 0;
    for (int count = list1.Count; index < count; ++index)
    {
      if (list1[index] != list2[index])
        return false;
    }
    return true;
  }

  /// <summary> Сравнение одного уровня дерева элементов </summary>
  /// <returns></returns>
  public static int CompareLevel(IList<DocumentTreeNode> list1, IList<DocumentTreeNode> list2)
  {
    if (list2 == null)
      return 1;
    if (list1.Count != list2.Count)
      return list1.Count <= list2.Count ? -1 : 1;
    for (int index = 0; index < list1.Count; ++index)
    {
      if (list1[index] != list2[index])
        return list1[index] == null ? -1 : 1;
    }
    return 0;
  }
}
