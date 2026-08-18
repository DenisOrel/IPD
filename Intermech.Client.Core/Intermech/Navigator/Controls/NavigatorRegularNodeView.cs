
// Type: Intermech.Navigator.Controls.NavigatorRegularNodeView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Diagnostics;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Класс позволяет получать доступ к коллекции узлов INodeID у элемента управления
/// </summary>
internal class NavigatorRegularNodeView : INodeView
{
  /// <summary>Дерево "Навигатора"</summary>
  private NavigatorTreeView _treeView;
  /// <summary>Обновляемый узел дерева</summary>
  private NavigatorTreeNode _node;
  /// <summary>
  /// Сведения о возможностях вида, который для получения выводимой информации использует элементы навигации
  /// </summary>
  private NodeViewCapabilities _caps;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="treeView">Дерево "Навигатора"</param>
  public NavigatorRegularNodeView(NavigatorTreeView treeView)
  {
    this._treeView = treeView;
    this._node = (NavigatorTreeNode) null;
    this._caps = (NodeViewCapabilities) null;
  }

  /// <summary>Выполнить привязку к указанному узлу дерева</summary>
  /// <param name="node">Узел дерева "Навигатора"</param>
  public void Bind(NavigatorTreeNode node)
  {
    this._node = node;
    this._caps = new NodeViewCapabilities(ContentType.Folders, this._treeView._treeColumns, node.Full);
  }

  /// <summary>
  /// Сведения о возможностях вида, который для получения выводимой информации использует элементы навигации
  /// </summary>
  public NodeViewCapabilities Capabilities
  {
    [DebuggerStepThrough] get => this._caps;
  }

  /// <summary>Количество узлов</summary>
  int INodeView.Count
  {
    [DebuggerStepThrough] get => this._node.Children.Count;
  }

  /// <summary>Получить узел с указанным индексом</summary>
  /// <param name="index">Индекс узла</param>
  /// <returns></returns>
  INodeID INodeView.this[int index]
  {
    [DebuggerStepThrough] get => this._node.Children[index].NodeID;
  }

  /// <summary>Добавить в коллекцию дополнительные узлы</summary>
  /// <param name="nodeIDs">Коллекция дополнительных узлов</param>
  public void Append(NodeIDCollection nodeIDs)
  {
    this._treeView.ProcessAddChildren(this._node, nodeIDs);
  }

  /// <summary>Обновить коллекцию узлов с указанными индексами</summary>
  /// <param name="indexes">Коллекция индексов узлов, которые требуется обновить</param>
  public void Update(IList indexes)
  {
    this._treeView.ProcessUpdateChildren(this._node, (NodeColumnCollection) null, indexes);
  }

  /// <summary>
  /// Выполнить замену узлов с указанными индексами данными из дополнительной коллекции
  /// </summary>
  /// <param name="indexes">Коллекция индексов узлов, которые требуется заменить</param>
  /// <param name="replacementNodeIDs">Коллекция новых узлов взамен старых</param>
  public void Replace(IList indexes, NodeIDCollection replacementNodeIDs)
  {
    for (int index = 0; index < indexes.Count; ++index)
    {
      NavigatorTreeNode child = this._node.Children[(int) indexes[index]];
      INodeQuery query = this._treeView.GetNodeHandler(child).GetQuery(ContentType.Folders);
      query.Execute(new NodeIDCollection()
      {
        replacementNodeIDs[index]
      });
      NavigatorTreeNode navigatorTreeNode = child;
      navigatorTreeNode.Bookmark = (object) null;
      navigatorTreeNode.Full = false;
      navigatorTreeNode.NodeID = query.GetRecordNodeID(0);
      if (navigatorTreeNode.Handler != null && navigatorTreeNode.Handler is IDisposable)
        ((IDisposable) navigatorTreeNode.Handler).Dispose();
      navigatorTreeNode.Handler = this._treeView.CreateChildHandler(child);
      child.ClearChildren();
      this._treeView.PopulateNode(child);
    }
  }

  /// <summary>Удалить узлы с указанными индексами</summary>
  /// <param name="indexes">Коллекция индексов узлов, которые требуется удалить</param>
  public void Remove(IList indexes) => this._treeView.ProcessRemoveChildren(this._node, indexes);
}
