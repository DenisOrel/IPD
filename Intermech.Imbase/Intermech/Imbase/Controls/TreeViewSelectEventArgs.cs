// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Controls.TreeViewSelectEventArgs
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Controls;

public class TreeViewSelectEventArgs : TreeViewEventArgs
{
  public TreeViewSelectEventArgs(TreeViewEventArgs args, NodeInfo nodeInfo)
    : base(args.Node, args.Action)
  {
    this.NodeInfo = nodeInfo;
  }

  public NodeInfo NodeInfo { get; }
}
