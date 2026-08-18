
// Type: Intermech.Navigator.Controls.NavigatorTreeViewSelectedItem
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Diagnostics;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Реализует интерфейс ISelectedItems для одного узла дерева "Навигатора"
/// </summary>
public class NavigatorTreeViewSelectedItem : ISelectedItems, ISimpleSelectedItems
{
  /// <summary>Дерево "Навигатора"</summary>
  private NavigatorTreeView _treeView;

  /// <summary>Создать экземпляр коллекции</summary>
  /// <param name="treeView">Дерево "Навигатора"</param>
  /// <param name="node">Узел, на основании которого создаётся коллекция</param>
  public NavigatorTreeViewSelectedItem(NavigatorTreeView treeView, NavigatorTreeNode node)
  {
    this._treeView = treeView;
    this.Node = node;
  }

  /// <summary>Узел дерева "Навигатора"</summary>
  public NavigatorTreeNode Node { get; set; }

  /// <summary>Является ли коллекция разнородной</summary>
  public bool IsCollage
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Количество узлов в коллекции</summary>
  public int Count
  {
    [DebuggerStepThrough] get => this.Node == null ? 0 : 1;
  }

  /// <summary>Получить данные у узла коллекции</summary>
  /// <param name="index">Индекс узла в коллекции</param>
  /// <param name="dataFormat">Формат запрашиваемых данных</param>
  /// <returns>Данные запрашиваемого формата</returns>
  public object GetItemData(int index, Type dataFormat)
  {
    INode nodeHandler = this._treeView.GetNodeHandler(this.Node);
    if (dataFormat == typeof (INode))
      return (object) nodeHandler;
    INodeID nodeId = this.Node.NodeID;
    if (dataFormat == typeof (INodeID))
      return (object) nodeId;
    return dataFormat == typeof (NavigatorTreeNode) ? (object) this.Node : nodeHandler.GetData(nodeId, dataFormat);
  }

  /// <summary>Получить описание узла у указнного элемента коллекции</summary>
  /// <param name="index">Индекс элемента коллекции</param>
  /// <returns>Описание узла указанного элемента коллекции</returns>
  public INodeID GetItemID(int index) => this.Node.NodeID;

  /// <summary>Получить данные у родительского элемента</summary>
  /// <param name="index">Индекс дочернего элемента коллекции</param>
  /// <param name="dataFormat">Формат запрашиваемых данных</param>
  /// <returns>Данные указанного формата у родительского элемента</returns>
  public object GetParentData(int index, Type dataFormat)
  {
    NavigatorTreeNode parent = this.Node.Parent;
    return parent == null ? (object) null : this._treeView.GetNodeHandler(parent).GetData(parent.NodeID, dataFormat);
  }

  /// <summary>Получить путь к родительскому узлу</summary>
  /// <param name="index">Индекс дочернего элемента</param>
  /// <returns>Путь к родительскому узлу</returns>
  public NodeIDPath GetParentPath(int index) => this._treeView.GetNodeIDPath(this.Node.Parent);
}
