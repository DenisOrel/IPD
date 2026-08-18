// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.NodeListViewItem
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class NodeListViewItem : ListViewItem
{
  private bool _expanded;
  private NodeListViewItem _parent;
  private List<ListViewItem> _children;

  public event OnGetChildrenEventHandler OnGetChildren;

  public NodeListViewItem(NodeListViewItem parent, string text)
    : base(text)
  {
    this._parent = parent;
  }

  public NodeListViewItem Parent => this._parent;

  public int Level => this._parent == null ? 0 : this._parent.Level + 1;

  public bool HasChildren
  {
    get => this.StateImageIndex != -1;
    set
    {
      if (value)
        this.StateImageIndex = Convert.ToInt32(this._expanded);
      else
        this.StateImageIndex = -1;
    }
  }

  public bool Expanded => this._expanded;

  public bool Toggle()
  {
    if (this.HasChildren)
    {
      if (this.Expanded)
        this.Collapse();
      else
        this.Expand();
    }
    return this.HasChildren;
  }

  public List<ListViewItem> Children
  {
    get => this._children;
    set => this._children = value;
  }

  protected void AddChildren()
  {
    if (this._children == null)
      return;
    int num = this.Level + 1;
    ListView listView = this.ListView;
    if (listView == null)
      return;
    for (int index = this._children.Count - 1; index >= 0; --index)
    {
      this._children[index].IndentCount = num;
      listView.Items.Insert(this.Index + 1, this._children[index]);
      if (this._children[index] is NodeListViewItem child && child.Expanded)
        child.AddChildren();
    }
  }

  public void Expand()
  {
    if (!this.HasChildren || this.Expanded)
      return;
    this._expanded = !this._expanded;
    this.StateImageIndex = Convert.ToInt32(this._expanded);
    if (this._children == null)
    {
      OnGetChildrenEventHandler onGetChildren = this.OnGetChildren;
      if (onGetChildren != null)
        onGetChildren(this);
    }
    this.AddChildren();
  }

  protected void RemoveChildren()
  {
    if (this._children == null)
      return;
    ListView listView = this.ListView;
    if (listView == null)
      return;
    foreach (ListViewItem child in this._children)
    {
      if (child is NodeListViewItem)
        ((NodeListViewItem) child).RemoveChildren();
      listView.Items.Remove(child);
    }
    this._children = (List<ListViewItem>) null;
  }

  public void Collapse()
  {
    if (!this.HasChildren || !this.Expanded)
      return;
    this._expanded = !this._expanded;
    this.StateImageIndex = Convert.ToInt32(this._expanded);
    this.RemoveChildren();
  }

  public bool InsideStateImage(Point p)
  {
    Rectangle bounds = this.GetBounds(ItemBoundsPortion.Entire);
    int num = this.ListView.SmallImageList != null ? this.IndentCount * this.ListView.SmallImageList.ImageSize.Width : 0;
    return p.X >= bounds.Left + num && p.X < bounds.Left + num + 16 /*0x10*/ && p.Y > bounds.Top && p.Y < bounds.Bottom;
  }
}
