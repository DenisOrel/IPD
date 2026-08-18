// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.AttrTreeViewItemComparer
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System.Collections;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

internal class AttrTreeViewItemComparer : IComparer
{
  public int Compare(object x, object y)
  {
    int num1 = 0;
    int num2 = 0;
    if (x is AttributeNode)
      num1 = 1;
    if (y is AttributeNode)
      num2 = 1;
    TreeNode treeNode1 = (TreeNode) x;
    if (treeNode1.TreeView != null && treeNode1.Level == 0)
      return 0;
    TreeNode treeNode2 = (TreeNode) y;
    if (treeNode2.TreeView != null && treeNode2.Level == 0)
      return 0;
    return num1 != num2 ? num1 - num2 : string.Compare(x.ToString(), y.ToString());
  }
}
