
// Type: Intermech.Extensions.NavigatorTreeNodeExtensions
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Search.ObjectGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;


namespace Intermech.Extensions;

/// <summary>Расширения класса NavigatorTreeNode</summary>
public static class NavigatorTreeNodeExtensions
{
  /// <summary>Перечисление всех нод, ведущих к корню иерархии (данную ноду не включаем)</summary>
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static List<NavigatorTreeNode> Parents([NotNull] this NavigatorTreeNode node)
  {
    List<NavigatorTreeNode> navigatorTreeNodeList = new List<NavigatorTreeNode>(node.Level + 1);
    for (; node.Parent != null; node = node.Parent)
      navigatorTreeNodeList.Add(node.Parent);
    return navigatorTreeNodeList;
  }

  /// <summary>Перечисление всех нод, ведущих к корню иерархии (включая данную)</summary>
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static List<NavigatorTreeNode> ThisAndParents(
    [NotNull] this NavigatorTreeNode node,
    [CanBeNull] Func<NavigatorTreeNode, bool> condition = null)
  {
    List<NavigatorTreeNode> navigatorTreeNodeList = new List<NavigatorTreeNode>(node.Level + 1);
    for (; node != null; node = node.Parent)
    {
      if ((condition != null ? (condition(node) ? 1 : 0) : 1) != 0)
        navigatorTreeNodeList.Add(node);
    }
    return navigatorTreeNodeList;
  }

  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static List<NavigatorTreeNode> ExpandedChildNodes(
    [NotNull] this NavigatorTreeNode node,
    bool recursive)
  {
    List<NavigatorTreeNode> result = new List<NavigatorTreeNode>();
    if (node.HasChildren)
      RecursiveGetExpandedChildNodes(node);
    return result;

    void RecursiveGetExpandedChildNodes(NavigatorTreeNode targetNode)
    {
      foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) targetNode.Children)
      {
        result.Add(child);
        if (recursive && child.HasChildren)
          RecursiveGetExpandedChildNodes(targetNode);
      }
    }
  }

  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<NavigatorTreeNode> MinusSelfContains(
    [NotNull] this IEnumerable<NavigatorTreeNode> nodes)
  {
    IReadOnlyCollection<NavigatorTreeNode> nodesCollection = (IReadOnlyCollection<NavigatorTreeNode>) nodes.Distinct<NavigatorTreeNode>().ToList<NavigatorTreeNode>();
    return (IReadOnlyList<NavigatorTreeNode>) nodesCollection.Where<NavigatorTreeNode>((Func<NavigatorTreeNode, bool>) (node => !node.Parents().Any<NavigatorTreeNode>(new Func<NavigatorTreeNode, bool>(((Enumerable) nodesCollection).Contains<NavigatorTreeNode>)))).ToList<NavigatorTreeNode>(nodesCollection.Count);
  }

  /// <summary>Последовательность всех дочерних нод, рекурсивная или нет, с автоподгрузкой недогруженных узлов или нет</summary>
  /// <param name="recursive">Если true, то работает рекурсивно</param>
  /// <param name="autoPopulateNodes">Если true, то автоматом подгружает состав дочерних нод</param>
  /// <returns>Последовательность</returns>
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<NavigatorTreeNode> EnumerationWithChilds(
    [NotNull] this IEnumerable<NavigatorTreeNode> nodes,
    bool recursive = true,
    bool autoPopulateNodes = false)
  {
    return nodes.Aggregate<NavigatorTreeNode, IEnumerable<NavigatorTreeNode>>((IEnumerable<NavigatorTreeNode>) null, (Func<IEnumerable<NavigatorTreeNode>, NavigatorTreeNode, IEnumerable<NavigatorTreeNode>>) ((result, rootNode) => result.ConcatIgnoreNull<NavigatorTreeNode>(rootNode.EnumerationWithChilds(recursive, autoPopulateNodes))));
  }

  /// <summary>Последовательность всех дочерних нод, удовлетворяющих некому условию (которое необязательно указывать),
  /// рекурсивная или нет, с автоподгрузкой недогруженных узлов или нет</summary>
  /// <param name="nodes">The nodes to act on. This cannot be null</param>
  /// <param name="condition">Условие, которому должны соответствовать ноды</param>
  /// <param name="recursive">(Optional) Если true, то работает рекурсивно</param>
  /// <param name="autoPopulateNodes">(Optional) Если true, то автоматом подгружает состав дочерних нод</param>
  /// <returns>Последовательность</returns>
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<NavigatorTreeNode> EnumerationWithChilds(
    [NotNull] this IEnumerable<NavigatorTreeNode> nodes,
    [NotNull] Func<NavigatorTreeNode, bool> condition,
    bool recursive = true,
    bool autoPopulateNodes = false)
  {
    return nodes.Aggregate<NavigatorTreeNode, IEnumerable<NavigatorTreeNode>>((IEnumerable<NavigatorTreeNode>) null, (Func<IEnumerable<NavigatorTreeNode>, NavigatorTreeNode, IEnumerable<NavigatorTreeNode>>) ((result, rootNode) => result.ConcatIgnoreNull<NavigatorTreeNode>(rootNode.EnumerationWithChilds(condition, recursive, autoPopulateNodes))));
  }

  /// <summary>Последовательность всех дочерних нод, удовлетворяющих некому условию (которое необязательно указывать), рекурсивная или нет,
  /// с автоподгрузкой недогруженных узлов или нет</summary>
  /// <param name="condition">Условие, которому должны соответствовать ноды</param>
  /// <param name="invokeForChilds">Перебирать ли дочерние ноды данной</param>
  /// <param name="autoPopulateNodes">Если true, то автоматом подгружает состав дочерних нод</param>
  /// <returns>Последовательность</returns>
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<NavigatorTreeNode> EnumerationWithChilds(
    [NotNull] this IEnumerable<NavigatorTreeNode> nodes,
    [NotNull] Func<NavigatorTreeNode, bool> condition,
    [NotNull] Func<NavigatorTreeNode, bool> invokeForChilds,
    bool autoPopulateNodes = false)
  {
    return nodes.Aggregate<NavigatorTreeNode, IEnumerable<NavigatorTreeNode>>((IEnumerable<NavigatorTreeNode>) null, (Func<IEnumerable<NavigatorTreeNode>, NavigatorTreeNode, IEnumerable<NavigatorTreeNode>>) ((result, rootNode) => result.ConcatIgnoreNull<NavigatorTreeNode>(rootNode.EnumerationWithChilds(condition, invokeForChilds, autoPopulateNodes))));
  }

  /// <summary>Последовательность всех дочерних нод, рекурсивная или нет, с автоподгрузкой недогруженных узлов или нет</summary>
  /// <param name="recursive">Если true, то работает рекурсивно</param>
  /// <param name="autoPopulateNodes">Если true, то автоматом подгружает состав дочерних нод</param>
  /// <returns>Последовательность</returns>
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<NavigatorTreeNode> ChildsEnumeration(
    [NotNull] this IEnumerable<NavigatorTreeNode> nodes,
    bool recursive = true,
    bool autoPopulateNodes = false)
  {
    return nodes.Aggregate<NavigatorTreeNode, IEnumerable<NavigatorTreeNode>>((IEnumerable<NavigatorTreeNode>) null, (Func<IEnumerable<NavigatorTreeNode>, NavigatorTreeNode, IEnumerable<NavigatorTreeNode>>) ((result, rootNode) => result.ConcatIgnoreNull<NavigatorTreeNode>(rootNode.ChildsEnumeration(recursive, autoPopulateNodes))));
  }

  /// <summary>Последовательность всех дочерних нод, удовлетворяющих некому условию (которое необязательно указывать),
  /// рекурсивная или нет, с автоподгрузкой недогруженных узлов или нет</summary>
  /// <param name="nodes">The nodes to act on. This cannot be null</param>
  /// <param name="condition">Условие, которому должны соответствовать ноды</param>
  /// <param name="recursive">(Optional) Если true, то работает рекурсивно</param>
  /// <param name="autoPopulateNodes">(Optional) Если true, то автоматом подгружает состав дочерних нод</param>
  /// <returns>Последовательность</returns>
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<NavigatorTreeNode> ChildsEnumeration(
    [NotNull] this IEnumerable<NavigatorTreeNode> nodes,
    [NotNull] Func<NavigatorTreeNode, bool> condition,
    bool recursive = true,
    bool autoPopulateNodes = false)
  {
    return nodes.Aggregate<NavigatorTreeNode, IEnumerable<NavigatorTreeNode>>((IEnumerable<NavigatorTreeNode>) null, (Func<IEnumerable<NavigatorTreeNode>, NavigatorTreeNode, IEnumerable<NavigatorTreeNode>>) ((result, rootNode) => result.ConcatIgnoreNull<NavigatorTreeNode>(rootNode.ChildsEnumeration(condition, recursive, autoPopulateNodes))));
  }

  /// <summary>Последовательность всех дочерних нод, удовлетворяющих некому условию (которое необязательно указывать),
  /// рекурсивная или нет, с автоподгрузкой недогруженных узлов или нет</summary>
  /// <param name="nodes">The nodes to act on. This cannot be null</param>
  /// <param name="condition">Условие, которому должны соответствовать ноды</param>
  /// <param name="invokeForChilds">Перебирать ли дочерние ноды данной</param>
  /// <param name="autoPopulateNodes">(Optional) Если true, то автоматом подгружает состав дочерних нод</param>
  /// <returns>Последовательность</returns>
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<NavigatorTreeNode> ChildsEnumeration(
    [NotNull] this IEnumerable<NavigatorTreeNode> nodes,
    [NotNull] Func<NavigatorTreeNode, bool> condition,
    [NotNull] Func<NavigatorTreeNode, bool> invokeForChilds,
    bool autoPopulateNodes = false)
  {
    return nodes.Aggregate<NavigatorTreeNode, IEnumerable<NavigatorTreeNode>>((IEnumerable<NavigatorTreeNode>) null, (Func<IEnumerable<NavigatorTreeNode>, NavigatorTreeNode, IEnumerable<NavigatorTreeNode>>) ((result, rootNode) => result.ConcatIgnoreNull<NavigatorTreeNode>(rootNode.ChildsEnumeration(condition, invokeForChilds, autoPopulateNodes))));
  }

  [NotNull]
  [ItemNotNull]
  public static IReadOnlyList<NavigatorTreeNode> GetObjectChilds([NotNull] this NavigatorTreeNode node)
  {
    List<NavigatorTreeNode> objectChilds = new List<NavigatorTreeNode>(node.Children.Count);
    foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) node.Children)
    {
      if (child.NodeID is ObjectGroupNodeID)
      {
        if (!child.Full)
          child.PopulateAndWaitForFull();
        if (child.Children.Count > 0)
        {
          objectChilds.Capacity = objectChilds.Capacity + child.Children.Count - 1;
          objectChilds.AddRange((IEnumerable<NavigatorTreeNode>) child.Children);
        }
      }
      else
        objectChilds.Add(child);
    }
    return (IReadOnlyList<NavigatorTreeNode>) objectChilds;
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T GetData<T>([NotNull] this NavigatorTreeNode node, [CanBeNull] INodeID nodeID)
  {
    return nodeID == null ? default (T) : (T) node?.Tree?.GetNodeHandler(node)?.GetData(nodeID, typeof (T));
  }

  [NotNull]
  [ItemCanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyCollection<T> GetData<T>(
    [NotNull] this NavigatorTreeNode node,
    [NotNull, ItemNotNull] NodeIDCollection nodeIDs)
  {
    IReadOnlyCollection<T> objs;
    if (node == null)
    {
      objs = (IReadOnlyCollection<T>) null;
    }
    else
    {
      NavigatorTreeView tree = node.Tree;
      if (tree == null)
      {
        objs = (IReadOnlyCollection<T>) null;
      }
      else
      {
        INode nodeHandler = tree.GetNodeHandler(node);
        objs = nodeHandler != null ? ((IReadOnlyCollection<object>) nodeHandler.GetData(nodeIDs, typeof (T))).MapReadOnlyCollection<object, T>((Func<object, T>) (obj => (T) obj), false) : (IReadOnlyCollection<T>) null;
      }
    }
    return objs ?? (IReadOnlyCollection<T>) Array.Empty<T>();
  }

  [ContractAnnotation("throwExceptionIfNotFound:true => NotNull; => CanBeNull")]
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T GetService<T>([NotNull] this NavigatorTreeNode node, bool throwExceptionIfNotFound = true)
  {
    object service = node?.Tree?.GetNodeHandler(node)?.GetService(typeof (T));
    return !(service == null & throwExceptionIfNotFound) ? (T) service : throw new Exception($"Service {typeof (T)} not found in navigator tree node");
  }
}
