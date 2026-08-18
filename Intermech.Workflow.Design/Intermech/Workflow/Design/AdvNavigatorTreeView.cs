// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.AdvNavigatorTreeView
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Navigator.Controls;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

internal class AdvNavigatorTreeView : NavigatorTreeView
{
  protected override bool BackgroundTreeTasks => true;

  protected override void TreeMouseDown(object sender, MouseEventArgs e)
  {
    base.TreeMouseDown(sender, e);
    NavigatorTreeNode nodeAt = this.GetNodeAt(e.X, e.Y);
    if (nodeAt == null)
      return;
    this.FocusedNode = nodeAt;
  }

  protected internal bool AllInState(NavigatorTreeNodes nodes, CheckState state)
  {
    foreach (NavigatorTreeNode node in (List<NavigatorTreeNode>) nodes)
    {
      if (node.CheckState != state)
        return false;
    }
    return true;
  }
}
