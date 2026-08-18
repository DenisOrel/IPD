// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Controls.CustomTreeListHandler
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Handler;
using DevExpress.IM.XtraTreeList.Nodes;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Controls;

internal class CustomTreeListHandler(TreeList tree) : TreeListHandler(tree)
{
  private AVSWindow AVSWindow
  {
    get
    {
      for (Control parent = this.TreeList.Parent; parent != null; parent = parent.Parent)
      {
        if (parent is AVSWindow)
          return parent as AVSWindow;
      }
      return (AVSWindow) null;
    }
  }

  public TreeListNode NextNode(TreeListNode node)
  {
    TreeListNode parentNode = node.ParentNode;
    if (parentNode == null)
      return (TreeListNode) null;
    TreeListNode treeListNode = (TreeListNode) null;
    int index = parentNode.Nodes.IndexOf(node) + 1;
    if (index < parentNode.Nodes.Count)
      treeListNode = parentNode.Nodes[index];
    return treeListNode;
  }

  public TreeListNode PrevNode(TreeListNode node)
  {
    TreeListNode parentNode = node.ParentNode;
    if (parentNode == null)
      return (TreeListNode) null;
    TreeListNode treeListNode = (TreeListNode) null;
    int index = parentNode.Nodes.IndexOf(node) - 1;
    if (index >= 0)
      treeListNode = parentNode.Nodes[index];
    return treeListNode;
  }

  public override void OnDragDrop(DragEventArgs e)
  {
  }

  public override void OnDragOver(DragEventArgs e)
  {
  }
}
