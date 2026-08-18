// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Comparers.NodeComparer
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Controls;
using System.Collections;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Comparers;

public class NodeComparer : IComparer
{
  public int Compare(object x, object y)
  {
    TreeNode treeNode1 = x as TreeNode;
    TreeNode treeNode2 = y as TreeNode;
    NodeInfo tag1 = treeNode1.Tag as NodeInfo;
    NodeInfo tag2 = treeNode2.Tag as NodeInfo;
    int num1 = (tag1.TypeId == Intermech.Imbase.Consts.ImbaseFavoritesTypeID ? -1 : 0) - (tag2.TypeId == Intermech.Imbase.Consts.ImbaseFavoritesTypeID ? -1 : 0);
    if (num1 != 0)
      return num1;
    int num2 = (treeNode1.Tag != null ? (treeNode1.Tag as NodeInfo)._order : (int) short.MaxValue) - (treeNode2.Tag != null ? (treeNode2.Tag as NodeInfo)._order : (int) short.MaxValue);
    return num2 == 0 ? treeNode1.Text.CompareTo(treeNode2.Text) : num2;
  }
}
