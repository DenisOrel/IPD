// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ControlListViewSubItem
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class ControlListViewSubItem : ListViewItem.ListViewSubItem
{
  private Control _control;
  private EnhListView _view;
  private ListViewItem _listItem;
  private int _pos;
  private Rectangle _bounds;
  private bool _looseFocus;

  public ControlListViewSubItem(ListViewItem li, Control c)
    : this(li, c, false)
  {
  }

  public ControlListViewSubItem(ListViewItem li, Control c, bool looseFocus)
  {
    this._listItem = li;
    this._pos = li.SubItems.Count;
    this._view = li.ListView as EnhListView;
    this._control = c;
    c.Parent = (Control) this._view;
    this._looseFocus = looseFocus;
    if (!looseFocus)
      return;
    c.Click += new EventHandler(this.Control_Click);
  }

  private void Control_Click(object sender, EventArgs e)
  {
    this._listItem.Selected = true;
    this._view.Focus();
  }

  public Control Control => this._control;

  internal void UpdateSubControlPos(bool recalcPos)
  {
    if (this._bounds.IsEmpty | recalcPos)
    {
      ColumnHeader column = this._view.Columns[this._pos - 1];
      int x = 0;
      for (int index = 0; index < this._pos; ++index)
        x += this._view.Columns[index].Width + 2;
      Rectangle bounds = this._listItem.GetBounds(ItemBoundsPortion.Entire);
      this._bounds = new Rectangle(x, bounds.Top, column.Width, bounds.Height);
    }
    this._control.Bounds = this._bounds;
  }

  internal void UpdateSubControlColor(Color back, Color fore)
  {
    this._control.BackColor = back;
    this._control.ForeColor = fore;
  }
}
