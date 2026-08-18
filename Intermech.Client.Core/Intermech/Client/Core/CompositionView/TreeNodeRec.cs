
// Type: Intermech.Client.Core.CompositionView.TreeNodeRec
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using System.Collections.Generic;


namespace Intermech.Client.Core.CompositionView;

/// <summary>
/// Структура для корректной вставки объекта в дерево навигатора
/// </summary>
internal class TreeNodeRec
{
  /// <summary>Узел навигатора</summary>
  protected NavigatorTreeNode _treeNode;
  /// <summary>Список ид. связей</summary>
  protected List<long> _relIDList;
  /// <summary>
  /// 
  /// </summary>
  protected NodesInsertPosition _position = NodesInsertPosition.After;

  /// <summary>Конструктор</summary>
  /// <param name="treeNode"></param>
  public TreeNodeRec(NavigatorTreeNode treeNode, NodesInsertPosition position = NodesInsertPosition.After)
  {
    this._treeNode = treeNode;
    this._position = position;
    this._relIDList = new List<long>();
  }

  /// <summary>Узел навигатора</summary>
  public NavigatorTreeNode TreeNode => this._treeNode;

  /// <summary>Список ид. связей</summary>
  public List<long> RelIDList => this._relIDList;

  /// <summary>Позиция для вставки</summary>
  public NodesInsertPosition Position => this._position;
}
