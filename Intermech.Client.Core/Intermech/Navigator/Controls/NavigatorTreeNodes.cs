
// Type: Intermech.Navigator.Controls.NavigatorTreeNodes
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace Intermech.Navigator.Controls;

/// <summary>Коллекция узлов в дереве "Навигатора"</summary>
public class NavigatorTreeNodes : List<NavigatorTreeNode>, ICloneable
{
  /// <summary>
  /// Узел дерева "Навигатора", которому принадлежит коллекция узлов
  /// </summary>
  protected NavigatorTreeNode _owner;
  /// <summary>Дерево-владелец</summary>
  protected NavigatorTreeView _tree;

  /// <summary>Создать пустой экземпляр класса</summary>
  public NavigatorTreeNodes()
  {
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="tree">Дерево, которому принадлежит коллекция</param>
  /// <param name="owner">Узел дерева "Навигатора", которому принадлежит коллекция узлов</param>
  public NavigatorTreeNodes(NavigatorTreeView tree, NavigatorTreeNode owner)
  {
    this._tree = tree;
    this._owner = owner;
  }

  /// <summary>
  /// Узел дерева "Навигатора", которому принадлежит коллекция узлов
  /// </summary>
  public NavigatorTreeNode Owner
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._owner;
  }

  public NavigatorTreeView Tree
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._tree;
  }

  /// <summary>
  /// Отыскать в коллекции (начиная с её корневой записи) описание узла дерева "Навигатора".
  /// Поиск также будет проходить в дочерних коллекциях.
  /// </summary>
  /// <param name="nodeID">Описание узла дерева "Навигатора"</param>
  /// <returns>null, если описание узла не найдено</returns>
  public virtual NavigatorTreeNode FindNodeIDFromRoot(INodeID nodeID)
  {
    if (nodeID == null)
      return (NavigatorTreeNode) null;
    NavigatorTreeNode navigatorTreeNode = this._owner;
    while (navigatorTreeNode.Parent != null)
      navigatorTreeNode = navigatorTreeNode.Parent;
    return navigatorTreeNode.FindNodeID(nodeID);
  }

  /// <summary>
  /// Отыскать в коллекции описание узла дерева "Навигатора".
  /// Поиск также будет проходить в дочерних коллекциях.
  /// </summary>
  /// <param name="nodeID">Уникальный в пределах коллекции идентификатор версии объекта</param>
  /// <returns>null, если описание удаляемого объекта не найдено</returns>
  public virtual NavigatorTreeNode FindNodeID(INodeID nodeID)
  {
    if (nodeID == null)
      return (NavigatorTreeNode) null;
    for (int index = 0; index < this.Count; ++index)
    {
      NavigatorTreeNode nodeId = this[index].FindNodeID(nodeID);
      if (nodeId != null)
        return nodeId;
    }
    return (NavigatorTreeNode) null;
  }

  /// <summary>Добавить новый узел в коллекцию</summary>
  /// <param name="nodeID">Описание узла</param>
  /// <returns>Вновь добавленный узел</returns>
  public virtual NavigatorTreeNode Add(INodeID nodeID)
  {
    NavigatorTreeNode navigatorTreeNode = new NavigatorTreeNode(this._tree, this._owner, nodeID);
    base.Add(navigatorTreeNode);
    return navigatorTreeNode;
  }

  /// <summary>Добавить новый узел в коллекцию</summary>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="values">Значения ячеек узла (для отображения на экране)</param>
  /// <param name="rawValues">Значения ячеек узла (исходные)</param>
  /// <returns>Вновь добавленный узел</returns>
  public virtual NavigatorTreeNode Add(INodeID nodeID, object[] values, object[] rawValues)
  {
    NavigatorTreeNode navigatorTreeNode = new NavigatorTreeNode(this._tree, this._owner, nodeID, values, rawValues);
    base.Add(navigatorTreeNode);
    return navigatorTreeNode;
  }

  public new void Add(NavigatorTreeNode navigatorTreeNode)
  {
    int count = this.Count;
    base.Add(navigatorTreeNode);
  }

  /// <summary>Добавить новый узел в коллекцию</summary>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="values">Значения ячеек узла</param>
  /// <param name="rawValues">Значения ячеек узла (исходные)</param>
  /// <param name="handler">Узел</param>
  public virtual NavigatorTreeNode Add(
    INodeID nodeID,
    object[] values,
    object[] rawValues,
    INode handler)
  {
    NavigatorTreeNode navigatorTreeNode = new NavigatorTreeNode(this._tree, this._owner, nodeID, values, rawValues, handler);
    base.Add(navigatorTreeNode);
    return navigatorTreeNode;
  }

  /// <summary>Добавить новый узел в коллекцию</summary>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="values">Значения ячеек узла</param>
  /// <param name="rawValues">Значения ячеек узла (исходные)</param>
  /// <param name="handler">Узел</param>
  /// <param name="flags">Флажки узла</param>
  public virtual NavigatorTreeNode Add(
    INodeID nodeID,
    object[] values,
    object[] rawValues,
    INode handler,
    TreeNodeFlags flags)
  {
    NavigatorTreeNode navigatorTreeNode = new NavigatorTreeNode(this._tree, this._owner, nodeID, values, rawValues, handler, flags);
    base.Add(navigatorTreeNode);
    return navigatorTreeNode;
  }

  /// <summary>Добавить новый узел в коллекцию</summary>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="values">Значения ячеек узла</param>
  /// <param name="rawValues">Значения ячеек узла (исходные)</param>
  /// <param name="handler">Узел</param>
  /// <param name="flags">Флажки узла</param>
  /// <param name="bookmark">Закладка</param>
  public virtual NavigatorTreeNode Add(
    INodeID nodeID,
    object[] values,
    object[] rawValues,
    INode handler,
    TreeNodeFlags flags,
    object bookmark)
  {
    NavigatorTreeNode navigatorTreeNode = new NavigatorTreeNode(this._tree, this._owner, nodeID, values, rawValues, handler, flags, bookmark);
    base.Add(navigatorTreeNode);
    return navigatorTreeNode;
  }

  /// <summary>Добавить новый узел в коллекцию</summary>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="values">Значения ячеек узла</param>
  /// <param name="rawValues">Значения ячеек узла (исходные)</param>
  /// <param name="handler">Узел</param>
  /// <param name="flags">Флажки узла</param>
  /// <param name="bookmark">Закладка</param>
  /// <param name="full">В узел прочитаны все данные, пакетное чтение не должно трогать этот узел</param>
  public virtual NavigatorTreeNode Add(
    INodeID nodeID,
    object[] values,
    object[] rawValues,
    INode handler,
    TreeNodeFlags flags,
    object bookmark,
    bool full)
  {
    NavigatorTreeNode navigatorTreeNode = new NavigatorTreeNode(this._tree, this._owner, nodeID, values, rawValues, handler, flags, bookmark, full);
    base.Add(navigatorTreeNode);
    return navigatorTreeNode;
  }

  /// <summary>Добавить новый узел в коллекцию</summary>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="values">Значения ячеек узла</param>
  /// <param name="rawValues">Значения ячеек узла (исходные)</param>
  /// <param name="handler">Узел</param>
  /// <param name="flags">Флажки узла</param>
  /// <param name="bookmark">Закладка</param>
  /// <param name="full">В узел прочитаны все данные, пакетное чтение не должно трогать этот узел</param>
  /// <param name="validColumns">Набор состояний колонок (валидны ли значения в колонках или нет - маска колонок)</param>
  public virtual NavigatorTreeNode Add(
    INodeID nodeID,
    object[] values,
    object[] rawValues,
    INode handler,
    TreeNodeFlags flags,
    object bookmark,
    bool full,
    StatesRecord validColumns)
  {
    NavigatorTreeNode navigatorTreeNode = new NavigatorTreeNode(this._tree, this._owner, nodeID, values, rawValues, handler, flags, bookmark, full, validColumns);
    base.Add(navigatorTreeNode);
    return navigatorTreeNode;
  }

  /// <summary>
  /// Полное присваивание другого списка узлов дерева "Навигатора"
  /// </summary>
  /// <param name="source">Источник</param>
  public virtual void Assign(NavigatorTreeNodes source)
  {
    this.Clear();
    if (source == null)
      return;
    this._owner = source.Owner;
    for (int index = 0; index < source.Count; ++index)
      this.Add(source[index].Clone() as NavigatorTreeNode);
  }

  /// <summary>Создать копию экземпляра класса</summary>
  /// <returns>Копия экземпляра класса</returns>
  public virtual object Clone()
  {
    NavigatorTreeNodes navigatorTreeNodes = new NavigatorTreeNodes(this._tree, this._owner);
    navigatorTreeNodes.Assign(this);
    return (object) navigatorTreeNodes;
  }

  /// <summary>
  /// Перестроим ссылки на строки у коллекции дочерних узлов
  /// </summary>
  /// <param name="parentNode"></param>
  public void RebuildHandles()
  {
    NavigatorTreeNode owner = this.Owner;
    if (owner == null || owner.Handle == null)
      return;
    for (int index = 0; index < owner.Children.Count; ++index)
      owner.Children[index].Handle = owner.Handle.ChildRowByIndex(index);
  }

  /// <summary>Удалить элемент с указанным индексом</summary>
  /// <param name="index">Индекс удаляемого элемента</param>
  public new void RemoveAt(int index)
  {
    base.RemoveAt(index);
    this.RebuildHandles();
  }

  public void Remove(NavigatorTreeNode node)
  {
    if (node == null)
      throw new ArgumentNullException(nameof (node));
    base.Remove(node);
    this.RebuildHandles();
  }
}
