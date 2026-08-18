// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.TreeViewExtensions
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Controls;
using Intermech.Diagnostics;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Extensions;

public static class TreeViewExtensions
{
  [NotNull]
  [ItemNotNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IList<TreeNode> GetNodesList([NotNull] this TreeView treeView)
  {
    return (IList<TreeNode>) new TreeNodeList(treeView.Nodes);
  }
}
