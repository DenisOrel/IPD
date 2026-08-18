// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.ContextMenu.MenuTemplateNodeCollection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Navigator.ContextMenu;

/// <summary>Коллекция узлов для шаблона меню</summary>
public class MenuTemplateNodeCollection : 
  IAssignable,
  ICloneable,
  IEnumerable<MenuTemplateNode>,
  IEnumerable
{
  /// <summary>Шаблон меню</summary>
  internal MenuTemplate _template;
  /// <summary>Шаблон элемента меню</summary>
  internal MenuTemplateNode _owner;
  /// <summary>Дочерние узлы</summary>
  private List<MenuTemplateNode> _children;
  /// <summary>Разброс при сортировке</summary>
  private const int _sortThreshold = 10;

  /// <summary>Создать экземпляр коллекции</summary>
  /// <param name="template">Шаблон меню</param>
  /// <param name="owner">Шаблон элемента меню</param>
  public MenuTemplateNodeCollection(MenuTemplate template, MenuTemplateNode owner)
  {
    this._template = template;
    this._owner = owner;
    this._children = new List<MenuTemplateNode>();
  }

  /// <summary>
  /// Создать экземпляр коллекции, заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public MenuTemplateNodeCollection(object source) => this.Assign(source);

  /// <summary>Количество дочерних элементов в коллекции</summary>
  public int Count => this._children.Count;

  /// <summary>Дочерний элемент коллекции с указанным индексом</summary>
  /// <param name="index">Индекс элемента в коллекции</param>
  /// <returns>Дочерний элемент коллекции с указанным индексом</returns>
  public MenuTemplateNode this[int index] => this._children[index];

  /// <summary>Добавить элемент в коллекцию</summary>
  /// <param name="node">Добавляемый шаблон элемента меню</param>
  /// <returns></returns>
  public void Add(MenuTemplateNode node)
  {
    if (node.Parent != null)
      node.Parent.Nodes.Remove(node);
    node._parent = this._owner;
    node._template = this._template;
    if (this._template != null)
    {
      node.Nodes.MenuTemplate = this._template;
      this.InsertIntoNamedNodes(node);
      if (this._template._updateCount == 0)
      {
        node.Nodes.Sort();
        IComparer<MenuTemplateNode> comparer = (IComparer<MenuTemplateNode>) new MenuTemplateNodeComparer();
        for (int index = 0; index < this._children.Count; ++index)
        {
          if (comparer.Compare(node, this._children[index]) < 0)
          {
            this._children.Insert(index, node);
            return;
          }
        }
      }
    }
    this._children.Add(node);
  }

  /// <summary>Добавить узлы из коллекции</summary>
  /// <param name="nodes">Коллекция узлов</param>
  public void AddRange(ICollection nodes)
  {
    if (nodes.Count < 10 && this._template != null)
      this._template.BeginUpdate();
    try
    {
      foreach (MenuTemplateNode node in (IEnumerable) nodes)
        this.Add(node);
    }
    finally
    {
      if (nodes.Count < 10 && this._template != null)
        this._template.EndUpdate();
    }
  }

  /// <summary>Удалить узел из коллекции</summary>
  /// <param name="node">Удаляемый узел</param>
  public void Remove(MenuTemplateNode node)
  {
    int index = this._children.IndexOf(node);
    if (index < 0)
      return;
    this._children.RemoveAt(index);
    node._parent = (MenuTemplateNode) null;
    node.Nodes.MenuTemplate = (MenuTemplate) null;
    if (this._template == null)
      return;
    this.RemoveFromNamedNodes(node);
  }

  /// <summary>Удалит узел с указанным индексом из коллекции</summary>
  /// <param name="index">Индекс удаляемого из коллекции узла</param>
  public void RemoveAt(int index)
  {
    if (index >= this._children.Count)
      return;
    MenuTemplateNode child = this._children[index];
    this._children.RemoveAt(index);
    child._parent = (MenuTemplateNode) null;
    child.Nodes.MenuTemplate = (MenuTemplate) null;
    if (this._template == null)
      return;
    this.RemoveFromNamedNodes(child);
  }

  /// <summary>Отыскать индекс указанного узла в коллекции</summary>
  /// <param name="node">Указанный узел</param>
  /// <returns>Индекс узла</returns>
  public int IndexOf(MenuTemplateNode node) => this._children.IndexOf(node);

  /// <summary>Выполнить сортировку элементов коллекции</summary>
  public void Sort()
  {
    this._children.Sort((IComparer<MenuTemplateNode>) new MenuTemplateNodeComparer());
    foreach (MenuTemplateNode child in this._children)
      child.Nodes.Sort();
  }

  /// <summary>Выполнить перемещение узла внутри коллекции</summary>
  /// <param name="node">Перемещаемый узел</param>
  internal void RelocateNode(MenuTemplateNode node)
  {
    IComparer<MenuTemplateNode> comparer = (IComparer<MenuTemplateNode>) new MenuTemplateNodeComparer();
    int index1 = this._children.IndexOf(node);
    bool flag = false;
    if (index1 > 0)
      flag |= comparer.Compare(this._children[index1 - 1], this._children[index1]) > 0;
    if (index1 < this._children.Count - 1)
      flag |= comparer.Compare(this._children[index1], this._children[index1 + 1]) > 0;
    if (!flag)
      return;
    this._children.RemoveAt(index1);
    for (int index2 = 0; index2 < this._children.Count; ++index2)
    {
      if (comparer.Compare(node, this._children[index2]) < 0)
      {
        this._children.Insert(index2, node);
        return;
      }
    }
    this._children.Add(node);
  }

  /// <summary>Шаблон меню</summary>
  internal MenuTemplate MenuTemplate
  {
    get => this._template;
    set
    {
      if (this._template == value)
        return;
      this._template = value;
      foreach (MenuTemplateNode child in this._children)
      {
        child.Nodes.MenuTemplate = value;
        child._template = value;
      }
    }
  }

  /// <summary>Удалить из именованных узлов</summary>
  /// <param name="node">Удаляемый узел</param>
  private void RemoveFromNamedNodes(MenuTemplateNode node)
  {
    this._template.UpdateNameHash(node, node.Name, "");
    foreach (MenuTemplateNode child in node.Nodes._children)
      this._template.UpdateNameHash(child, child.Name, "");
  }

  /// <summary>Добавить в именованные узлы</summary>
  /// <param name="node">Добавляемый узел</param>
  private void InsertIntoNamedNodes(MenuTemplateNode node)
  {
    this._template.UpdateNameHash(node, "", node.Name);
    foreach (MenuTemplateNode child in node.Nodes._children)
      this._template.UpdateNameHash(child, "", child.Name);
  }

  /// <summary>Очистить поля класса</summary>
  public void Clear()
  {
    this._template = (MenuTemplate) null;
    this._owner = (MenuTemplateNode) null;
    if (this._children == null)
      return;
    this._children.Clear();
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    if (!(source is MenuTemplateNodeCollection templateNodeCollection))
      return;
    this._template = templateNodeCollection._template;
    this._owner = templateNodeCollection._owner;
    this._children = new List<MenuTemplateNode>(templateNodeCollection._children.Count);
    for (int index = 0; index < templateNodeCollection._children.Count; ++index)
    {
      MenuTemplateNode menuTemplateNode = templateNodeCollection._children[index].Clone() as MenuTemplateNode;
      menuTemplateNode._template = this._template;
      menuTemplateNode._parent = (MenuTemplateNode) null;
      this._children.Add(menuTemplateNode);
    }
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public object Clone() => (object) new MenuTemplateNodeCollection((object) this);

  public IEnumerator<MenuTemplateNode> GetEnumerator()
  {
    return (IEnumerator<MenuTemplateNode>) this._children.GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
}
