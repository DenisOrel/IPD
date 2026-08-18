// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNode.AutoSelectionNodeTreeComparer
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using System.Collections;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNode;

public class AutoSelectionNodeTreeComparer : IComparer
{
  public int Compare(object x, object y)
  {
    TreeNode treeNode1 = x as TreeNode;
    TreeNode treeNode2 = y as TreeNode;
    AutoSelectionNodeBase selectionNodeBase1;
    AutoSelectionNodeBase selectionNodeBase2;
    if (treeNode1 != null && treeNode2 != null)
    {
      selectionNodeBase1 = treeNode1.Tag as AutoSelectionNodeBase;
      selectionNodeBase2 = treeNode2.Tag as AutoSelectionNodeBase;
    }
    else
    {
      selectionNodeBase1 = x as AutoSelectionNodeBase;
      selectionNodeBase2 = y as AutoSelectionNodeBase;
    }
    return selectionNodeBase1 == null ? (selectionNodeBase2 != null ? -1 : 0) : (selectionNodeBase2 == null ? 1 : selectionNodeBase1.Order - selectionNodeBase2.Order);
  }
}
