
// Type: Intermech.Client.Core.TreeNodeCollectionHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Intermech.Client.Core;

public static class TreeNodeCollectionHelper
{
  public static IEnumerable<TreeNode> Collect(this TreeNodeCollection nodes)
  {
    foreach (TreeNode node in nodes)
    {
      yield return node;
      foreach (TreeNode treeNode in node.Nodes.Collect())
        yield return treeNode;
    }
  }

  public static TreeNode SearchTree(
    this TreeNodeCollection nodes,
    object tag,
    Func<object, object, bool> comparer)
  {
    if (tag == null)
      return (TreeNode) null;
    foreach (TreeNode node in nodes)
    {
      if (comparer(tag, node.Tag))
        return node;
      TreeNode treeNode = node.Nodes.SearchTree(tag, comparer);
      if (treeNode != null)
        return treeNode;
    }
    return (TreeNode) null;
  }
}
