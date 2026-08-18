
// Type: Intermech.Extensions.NavigatorTreeViewExtensions
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using Intermech.Diagnostics;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;


namespace Intermech.Extensions;

/// <summary>Расширения класса NavigatorTreeNode</summary>
public static class NavigatorTreeViewExtensions
{
  /// <summary>Сфокусированная в данный момент нода дерева</summary>
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static NavigatorTreeNode GetFocusedTreeNode([NotNull] this NavigatorTreeView tree)
  {
    return tree.FocusedNode;
  }

  /// <summary>Интерфейс идентификатора сфокусированной в дереве ноды</summary>
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static INodeID GetFocusedNodeID([NotNull] this NavigatorTreeView tree)
  {
    return tree.FocusedNode?.NodeID;
  }

  /// <summary>Идентификатор категории сфокусированной в данной момент в дереве сущности</summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int? GetFocusedCategoryID([NotNull] this NavigatorTreeView tree)
  {
    return tree.GetFocusedNodeID()?.CategoryID;
  }

  /// <summary>Идентификатор типа сфокусированной в данной момент в дереве сущности</summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int? GetFocusedTypeID([NotNull] this NavigatorTreeView tree)
  {
    return tree.GetFocusedNodeID()?.TypeID;
  }

  /// <summary>Перечисление всех выбранных нод дерева без какой-либо фильтрации</summary>
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<NavigatorTreeNode> GetSelectedNodes([NotNull] this NavigatorTreeView tree)
  {
    RowSelectionList selectedRows = tree.SelectedRows;
    return (selectedRows != null ? selectedRows.CastList<NavigatorTreeNode>() : (IReadOnlyList<NavigatorTreeNode>) null) ?? (IReadOnlyList<NavigatorTreeNode>) Array.Empty<NavigatorTreeNode>();
  }

  /// <summary>Перечисление интерфейсов идентификаторов выбранных нод без какой-либо фильтрации</summary>
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<INodeID> GetSelectedNodeIDs([NotNull] this NavigatorTreeView tree)
  {
    RowSelectionList selectedRows = tree.SelectedRows;
    return (selectedRows != null ? selectedRows.MapList<INodeID>((Func<object, INodeID>) (rowObj => !(rowObj is NavigatorTreeNode navigatorTreeNode) ? (INodeID) null : navigatorTreeNode.NodeID)) : (IReadOnlyList<INodeID>) null) ?? (IReadOnlyList<INodeID>) Array.Empty<INodeID>();
  }

  /// <summary>Перечисление идентификаторов категорий выбранных сущностей без какой-либо фильтрации</summary>
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<int> GetSelectedCategoryIDs([NotNull] this NavigatorTreeView tree)
  {
    IReadOnlyList<NavigatorTreeNode> selectedNodes = tree.GetSelectedNodes();
    return selectedNodes.Count == 0 ? (IReadOnlyList<int>) Array.Empty<int>() : (IReadOnlyList<int>) selectedNodes.SelectNotNull<NavigatorTreeNode, INodeID>((Func<NavigatorTreeNode, INodeID>) (treeNode => treeNode.NodeID)).Select<INodeID, int>((Func<INodeID, int>) (nodeID => nodeID.CategoryID)).Distinct<int>().ToList<int>(selectedNodes.Count);
  }

  /// <summary>Перечисление всех выбранных и видимых в дереве нод
  /// При этом те ноды, у которых выбрана какая-нибудь из вышестоящих нод в перечисление не попадает</summary>
  [NotNull]
  [ItemNotNull]
  public static IReadOnlyList<NavigatorTreeNode> GetSelectedNodesClosestToRoot(
    [NotNull] this NavigatorTreeView tree)
  {
    List<NavigatorTreeNode> nodesClosestToRoot = new List<NavigatorTreeNode>();
    NavigatorTreeNode prevFailedNode = (NavigatorTreeNode) null;
    NavigatorTreeNode prevOkNode = (NavigatorTreeNode) null;
    foreach (NavigatorTreeNode selectedNode in (IEnumerable<NavigatorTreeNode>) tree.GetSelectedNodes())
    {
      if ((prevFailedNode == null || selectedNode.Parent != prevFailedNode.Parent && selectedNode.Parent != prevFailedNode) && (prevOkNode == null || selectedNode.Parent != prevOkNode) && (prevOkNode != null && prevOkNode.Parent == selectedNode.Parent || selectedNode.Parents().All<NavigatorTreeNode>((Func<NavigatorTreeNode, bool>) (treeNode =>
      {
        if (treeNode == prevOkNode || treeNode == prevFailedNode)
          return false;
        Row handle = treeNode.Handle;
        return handle != null && !handle.Selected;
      }))))
      {
        nodesClosestToRoot.Add(selectedNode);
        prevFailedNode = (NavigatorTreeNode) null;
        prevOkNode = selectedNode;
      }
      else
        prevFailedNode = selectedNode;
    }
    return (IReadOnlyList<NavigatorTreeNode>) nodesClosestToRoot;
  }

  /// <summary>Перечисление интерфейсов идентификаторов выбранных сущностей
  /// При этом те ноды, у которых выбрана какая-нибудь из вышестоящих нод в перечисление не попадает</summary>
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<INodeID> GetSelectedClosestToRootNodeIDs([NotNull] this NavigatorTreeView tree)
  {
    IReadOnlyList<NavigatorTreeNode> nodesClosestToRoot = tree.GetSelectedNodesClosestToRoot();
    return nodesClosestToRoot.Count == 0 ? (IReadOnlyList<INodeID>) Array.Empty<INodeID>() : nodesClosestToRoot.MapListReadOnly<NavigatorTreeNode, INodeID>((Func<NavigatorTreeNode, INodeID>) (node => node.NodeID));
  }

  /// <summary>Перечисление идентификаторов категорий выбранных сущностей
  /// При этом те ноды, у которых выбрана какая-нибудь из вышестоящих нод в перечисление не попадает</summary>
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<int> GetSelectedClosestToRootCategoryIDs([NotNull] this NavigatorTreeView tree)
  {
    IReadOnlyList<NavigatorTreeNode> nodesClosestToRoot = tree.GetSelectedNodesClosestToRoot();
    return nodesClosestToRoot.Count == 0 ? (IReadOnlyList<int>) Array.Empty<int>() : (IReadOnlyList<int>) nodesClosestToRoot.SelectNotNull<NavigatorTreeNode, INodeID>((Func<NavigatorTreeNode, INodeID>) (treeNode => treeNode.NodeID)).Select<INodeID, int>((Func<INodeID, int>) (nodeID => nodeID.CategoryID)).Distinct<int>().ToList<int>(nodesClosestToRoot.Count);
  }

  /// <summary>Перечисление отмеченных нод без какой-либо фильтрации
  /// Метод рекурсивно перебирает только загруженные ноды</summary>
  [NotNull]
  [ItemNotNull]
  public static IReadOnlyList<NavigatorTreeNode> GetCheckedNodes(
    [NotNull] this NavigatorTreeView tree,
    [CanBeNull, ItemNotNull] IReadOnlyCollection<NavigatorTreeNode> items = null)
  {
    if (items == null && tree.RootNode != null)
      items = (IReadOnlyCollection<NavigatorTreeNode>) new NavigatorTreeNode[1]
      {
        tree.RootNode
      };
    if (items == null)
      return (IReadOnlyList<NavigatorTreeNode>) Array.Empty<NavigatorTreeNode>();
    List<NavigatorTreeNode> result = new List<NavigatorTreeNode>();
    AddCheckedNodes(items);
    return (IReadOnlyList<NavigatorTreeNode>) result;

    void AddCheckedNodes(IReadOnlyCollection<NavigatorTreeNode> nodes)
    {
      foreach (NavigatorTreeNode node in (IEnumerable<NavigatorTreeNode>) nodes)
      {
        if (node.CheckState != CheckState.Unchecked)
          result.Add(node);
        if (node.HasChildren && node.Full && node.Children != null && node.Children.Count > 0)
          AddCheckedNodes((IReadOnlyCollection<NavigatorTreeNode>) node.Children);
      }
    }
  }

  /// <summary>Перечисление интерфейсов идентификаторов отмеченных нод без какой-либо фильтрации
  /// Метод рекурсивно перебирает только загруженные ноды</summary>
  [NotNull]
  [ItemNotNull]
  public static IReadOnlyList<INodeID> GetCheckedNodeIDs([NotNull] this NavigatorTreeView tree)
  {
    return tree.GetCheckedNodes().MapListReadOnly<NavigatorTreeNode, INodeID>((Func<NavigatorTreeNode, INodeID>) (node => node.NodeID));
  }
}
