// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.TreeNodeCollectionExtensions
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Extensions;

public static class TreeNodeCollectionExtensions
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void AddMany(
    [NotNull] this TreeNodeCollection treeNodeCollection,
    [NotNull] IEnumerable<TreeNode> nodes)
  {
    foreach (TreeNode node in nodes)
      treeNodeCollection.Add(node);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TreeNode FindNode(
    [NotNull] this TreeNodeCollection treeNodeCollection,
    [NotNull] Predicate<TreeNode> condition)
  {
    foreach (TreeNode treeNode in treeNodeCollection)
    {
      if (condition(treeNode))
        return treeNode;
      TreeNode node = treeNode.Nodes.FindNode(condition);
      if (node != null)
        return node;
    }
    return (TreeNode) null;
  }
}
