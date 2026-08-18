// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.TreeNodeExtensions
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Extensions;

public static class TreeNodeExtensions
{
  public static void SetNodeFont([NotNull] this TreeNode node, [CanBeNull] Font newFont, bool recursive = false)
  {
    if (node.TreeView == null)
      return;
    node.TreeView.BeginUpdate();
    try
    {
      node.NodeFont = newFont;
      GC.Collect();
      Application.DoEvents();
      if (!recursive)
        return;
      foreach (TreeNode node1 in node.Nodes)
        node1.SetNodeFont(newFont, true);
    }
    finally
    {
      node.TreeView.EndUpdate();
    }
  }
}
