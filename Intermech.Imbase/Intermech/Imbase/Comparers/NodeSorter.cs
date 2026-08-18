// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Comparers.NodeSorter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System.Collections;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Comparers;

public class NodeSorter : IComparer
{
  public int Compare(object x, object y)
  {
    TreeNode treeNode1 = x as TreeNode;
    TreeNode treeNode2 = y as TreeNode;
    return treeNode1 != null ? (treeNode2 != null ? string.Compare(treeNode1.Name, treeNode2.Name) : 1) : (treeNode2 != null ? -1 : 0);
  }
}
