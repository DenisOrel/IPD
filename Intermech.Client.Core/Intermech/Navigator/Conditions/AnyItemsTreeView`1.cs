
// Type: Intermech.Navigator.Conditions.AnyItemsTreeView`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.Conditions;

public abstract class AnyItemsTreeView<T> : TreeView
{
  private TreeNode _previousSeletedNode;

  public AnyItemsTreeView()
  {
    this.FullRowSelect = true;
    this.ShowLines = false;
    this.ShowRootLines = false;
  }

  public void Initialize(T[] items, T selectedItem)
  {
    this.Nodes.Clear();
    int index1 = 0;
    for (int index2 = 0; index2 < items.Length; ++index2)
    {
      T obj = items[index2];
      if (obj.Equals((object) selectedItem))
        index1 = index2;
      this.Nodes.Add(this.ItemCaption(obj)).Tag = (object) obj;
    }
    if (this.Nodes.Count <= 0)
      return;
    this.SelectedNode = this.Nodes[index1];
  }

  protected abstract string ItemCaption(T item);

  protected override void OnAfterSelect(TreeViewEventArgs e)
  {
    base.OnAfterSelect(e);
    if (this._previousSeletedNode != null)
    {
      this._previousSeletedNode.BackColor = this.BackColor;
      this._previousSeletedNode.ForeColor = this.ForeColor;
    }
    e.Node.BackColor = SystemColors.Highlight;
    e.Node.ForeColor = SystemColors.HighlightText;
    this._previousSeletedNode = this.SelectedNode;
  }

  protected override void OnValidating(CancelEventArgs e)
  {
    base.OnValidating(e);
    this.SelectedNode.BackColor = SystemColors.Highlight;
    this.SelectedNode.ForeColor = SystemColors.HighlightText;
    this._previousSeletedNode = this.SelectedNode;
  }
}
