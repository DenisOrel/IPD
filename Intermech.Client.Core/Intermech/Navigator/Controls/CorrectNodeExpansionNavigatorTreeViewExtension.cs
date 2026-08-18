
// Type: Intermech.Navigator.Controls.CorrectNodeExpansionNavigatorTreeViewExtension
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Navigator.Controls;

public sealed class CorrectNodeExpansionNavigatorTreeViewExtension
{
  private NavigatorTreeView _tree;
  private DateTime _lastMouseDoubleClickTime = DateTime.MinValue;

  public CorrectNodeExpansionNavigatorTreeViewExtension(NavigatorTreeView tree)
  {
    this._tree = tree != null ? tree : throw new ArgumentNullException(nameof (tree));
    this._tree.CellDoubleClick += new EventHandler(this.Tree_CellDoubleClick);
  }

  public bool TryCorrentIncorrectNodeExpansion(NavigatorTreeNode node)
  {
    NavigatorTreeNode focusedNode = this._tree.FocusedNode;
    if (node == focusedNode || (DateTime.Now - this._lastMouseDoubleClickTime).Seconds >= 3)
      return false;
    focusedNode?.Expand();
    node.Expanded = false;
    return true;
  }

  private void Tree_CellDoubleClick(object sender, EventArgs e)
  {
    this._lastMouseDoubleClickTime = DateTime.Now;
  }
}
