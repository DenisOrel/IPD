// Decompiled with JetBrains decompiler
// Type: Intermech.Controls.TreeNodeExtendedCollection`1
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Controls;

public class TreeNodeExtendedCollection<TTreeNode> : TreeNodeCollection<TTreeNode> where TTreeNode : TreeNodeExtendedBase
{
  protected TreeNodeExtendedCollection([NotNull] TreeNodeCollection treeNodeCollection)
    : base(treeNodeCollection)
  {
  }

  internal TreeNodeExtendedCollection([NotNull] TreeViewExtended<TTreeNode> treeView)
    : base(treeView.OriginalNodes)
  {
  }

  internal TreeNodeExtendedCollection([NotNull] TreeNodeExtended<TTreeNode> treeNode)
    : base(treeNode.OriginalNodes)
  {
  }
}
