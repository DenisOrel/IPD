
// Type: Intermech.Navigator.Controls.NavigatorTreeViewSelectedItems
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Diagnostics;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Реализует интерфейс ISelectedItems для коллекции узлов дерева "Навигатора"
/// </summary>
public class NavigatorTreeViewSelectedItems : ISelectedItems, ISimpleSelectedItems
{
  private NavigatorTreeView _treeView;
  private NavigatorTreeNode[] _nodes;
  /// <summary>Требуется ли проверка коллекции на разнородность</summary>
  private bool _checkIsCollage;
  /// <summary>Является ли коллекция разнородной</summary>
  private bool _isCollage;

  public NavigatorTreeViewSelectedItems(NavigatorTreeView treeView, NavigatorTreeNode[] nodes)
  {
    this._treeView = treeView;
    this._nodes = nodes;
    this.InvalidateCollageCheck();
  }

  /// <summary>Коллекция узлов дерева</summary>
  public NavigatorTreeNode[] Nodes
  {
    [DebuggerStepThrough] get => this._nodes;
    set
    {
      this._nodes = value;
      this.InvalidateCollageCheck();
    }
  }

  /// <summary>
  /// Выделены ли разнородные узлы в коллекции (принадлежащие разным родительским узлам)
  /// </summary>
  public bool IsCollage
  {
    get
    {
      if (this._checkIsCollage)
      {
        if (this._nodes.Length != 0)
        {
          NavigatorTreeNode parent = this._nodes[0].Parent;
          for (int index = 1; index < this._nodes.Length; ++index)
          {
            if (this._nodes[index].Parent != parent)
            {
              this._isCollage = true;
              break;
            }
          }
        }
        this._checkIsCollage = false;
      }
      return this._isCollage;
    }
  }

  /// <summary>Количество узлов в коллекции</summary>
  public int Count
  {
    [DebuggerStepThrough] get => this._nodes.Length;
  }

  /// <summary>Получить данные у элемента коллекции</summary>
  /// <param name="index">Индекс элемента коллекции</param>
  /// <param name="dataFormat">Формат запрашиваемых данных</param>
  /// <returns>Данные запрошенного формата или null</returns>
  public object GetItemData(int index, Type dataFormat)
  {
    if (this._treeView.IsDisposed)
      return (object) null;
    NavigatorTreeNode node = this._nodes[index];
    INode nodeHandler = this._treeView.GetNodeHandler(node);
    if (dataFormat == typeof (INode))
      return (object) nodeHandler;
    return dataFormat == typeof (NavigatorTreeNode) ? (object) node : nodeHandler.GetData(node.NodeID, dataFormat);
  }

  /// <summary>Получить элемент с указанным индексом</summary>
  /// <param name="index">Индекс элемента</param>
  /// <returns>Элемент с указанным индексом</returns>
  public INodeID GetItemID(int index) => this._nodes[index].NodeID;

  /// <summary>Получить данные у родительского элемента</summary>
  /// <param name="index">Индекс родительского элемента</param>
  /// <param name="dataFormat">Формат запрашиваемых данных</param>
  /// <returns>Данные запрошенного формата или null</returns>
  public object GetParentData(int index, Type dataFormat)
  {
    NavigatorTreeNode parent = this._nodes[index].Parent;
    if (parent == null)
      return (object) null;
    return this._treeView.GetNodeHandler(parent)?.GetData(parent.NodeID, dataFormat);
  }

  /// <summary>Получить путь у родительского узла</summary>
  /// <param name="index">Индекс дочернего узла в коллекции</param>
  /// <returns>Путь у родительского узла</returns>
  public NodeIDPath GetParentPath(int index)
  {
    return this._treeView.GetNodeIDPath(this._nodes[index].Parent);
  }

  /// <summary>
  /// Указать коллекции, что требуется проверка на её разнородность
  /// </summary>
  internal void InvalidateCollageCheck()
  {
    this._checkIsCollage = true;
    this._isCollage = false;
  }

  /// <summary>Изменились узлы в коллекции</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void NodesChanged(object sender, EventArgs e) => this.InvalidateCollageCheck();
}
