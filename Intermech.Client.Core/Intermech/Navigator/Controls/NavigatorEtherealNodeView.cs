
// Type: Intermech.Navigator.Controls.NavigatorEtherealNodeView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Класс позволяет получать доступ к коллекции узлов INodeID у элемента управления
/// </summary>
internal class NavigatorEtherealNodeView : INodeView
{
  /// <summary>Дерево "Навигатора"</summary>
  private NavigatorTreeView _treeView;
  /// <summary>Корневой узел дерева</summary>
  private NavigatorTreeNode _rootNode;
  /// <summary>
  /// Сведения о возможностях вида, который для получения выводимой информации использует элементы навигации
  /// </summary>
  private NodeViewCapabilities _caps;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="treeView">Дерево "Навигатора"</param>
  /// <param name="rootNode">Корневой узел дерева</param>
  public NavigatorEtherealNodeView(NavigatorTreeView treeView, NavigatorTreeNode rootNode)
  {
    this._treeView = treeView;
    this._rootNode = rootNode;
    this._caps = new NodeViewCapabilities(ContentType.Folders, this._treeView._treeColumns, false);
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
    [DebuggerStepThrough] get => 1;
  }

  /// <summary>Получить узел с указанным индексом</summary>
  /// <param name="index">Индекс узла</param>
  /// <returns>Узел с указанным индексом</returns>
  INodeID INodeView.this[int index]
  {
    [DebuggerStepThrough] get => this._treeView.RootNodeID;
  }

  /// <summary>Добавить в коллекцию дополнительные узлы</summary>
  /// <param name="nodeIDs">Коллекция дополнительных узлов</param>
  public void Append(NodeIDCollection nodeIDs)
  {
  }

  /// <summary>Обновить коллекцию узлов с указанными индексами</summary>
  /// <param name="indexes">Коллекция индексов узлов, которые требуется обновить</param>
  public void Update(IList indexes)
  {
    INodeQuery query = this._treeView.RootHandler.GetQuery(ContentType.Folders);
    if (query == null)
      return;
    NodeColumnCollection treeColumns = this._treeView._treeColumns;
    bool flag = false;
    if (!treeColumns.Any<NodeColumn>((Func<NodeColumn, bool>) (o => o.ID != null && o.ID.Equals((object) ObligatoryObjectAttributes.F_VERSION_ID))))
    {
      NodeColumn nodeColumn = new NodeColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_VERSION_ID, typeof (long), FieldTypes.ftSystem, string.Empty);
      treeColumns.Add(nodeColumn);
      flag = true;
    }
    this._treeView.SetQueryColumns(query, treeColumns);
    query.Execute((object) null, 1);
    List<object> list1 = ((IEnumerable<object>) query.GetRecordValues(0)).ToList<object>();
    List<object> list2 = ((IEnumerable<object>) query.GetRawRecordValues(0)).ToList<object>();
    if (flag)
    {
      list1.RemoveAt(list1.Count - 1);
      list2.RemoveAt(list2.Count - 1);
      treeColumns.RemoveAt(treeColumns.Count - 1);
    }
    this._treeView.UpdateNodeFields(this._rootNode, list1.ToArray(), list2.ToArray(), treeColumns);
    this._rootNode.NodeID = query.GetRecordNodeID(0);
    this._treeView.RaiseRootNodeModified();
    this._treeView.InvalidateSelectedItems();
  }

  /// <summary>
  /// Выполнить замену узлов с указанными индексами данными из дополнительной коллекции
  /// </summary>
  /// <param name="indexes">Коллекция индексов узлов, которые требуется заменить</param>
  /// <param name="replacementNodeIDs">Коллекция новых узлов взамен старых</param>
  public void Replace(IList indexes, NodeIDCollection replacementNodeIDs)
  {
    INodeQuery query1 = this._treeView.GetNodeHandler(this._rootNode).GetQuery(ContentType.Folders);
    query1.Execute(new NodeIDCollection()
    {
      replacementNodeIDs[0]
    });
    NavigatorTreeNode rootNode = this._rootNode;
    rootNode.NodeID = query1.GetRecordNodeID(0);
    rootNode.Handler = this._treeView.RootHandler.GetChild(rootNode.NodeID);
    rootNode.Bookmark = (object) null;
    rootNode.Full = false;
    if (rootNode.Handler is IContextAware handler)
      handler.Services = this._treeView.Services;
    this._rootNode.ClearChildren();
    if (rootNode.Handle != null)
      this._treeView.UpdateRow(rootNode.Handle);
    this._treeView.PopulateNode(this._rootNode);
    INodeQuery query2 = this._treeView.RootHandler.GetQuery(ContentType.Folders);
    if (query2 != null)
    {
      NodeColumnCollection treeColumns = this._treeView._treeColumns;
      this._treeView.SetQueryColumns(query2, treeColumns);
      query2.Execute((object) null, 1);
      this._treeView.UpdateNodeFields(this._rootNode, query2.GetRecordValues(0), query2.GetRawRecordValues(0), treeColumns);
    }
    this._treeView.RaiseRootNodeReplaced();
    this._treeView.RaiseSelectedItemsChanged();
    this._treeView.RaiseAfterFocusNode(this._rootNode);
  }

  /// <summary>Удалить узлы с указанными индексами</summary>
  /// <param name="indexes">Коллекция индексов узлов, которые требуется удалить</param>
  public void Remove(IList indexes) => this._treeView.ClearCore(false);
}
