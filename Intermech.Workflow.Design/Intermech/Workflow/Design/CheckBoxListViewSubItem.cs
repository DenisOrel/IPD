// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.CheckBoxListViewSubItem
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

#nullable disable
namespace Intermech.Workflow.Design;

public class CheckBoxListViewSubItem : OwnerdrawListViewSubitem, IClickTarget
{
  private bool _checked;

  public bool Checked
  {
    get => this._checked;
    set
    {
      if (this._checked == value)
        return;
      this._checked = value;
      if (this._listView != null)
        this._listView.Invalidate(this.Bounds);
      EventHandler onClick = this.OnClick;
      if (onClick == null)
        return;
      onClick((object) this, (EventArgs) null);
    }
  }

  public override void Draw(DrawInfo di, DrawListViewSubItemEventArgs e)
  {
    base.Draw(di, e);
    CheckBoxState state = this.Checked ? CheckBoxState.CheckedNormal : CheckBoxState.UncheckedNormal;
    if (!this._listView.Enabled)
      state += CheckBoxState.UncheckedPressed;
    Size glyphSize = CheckBoxRenderer.GetGlyphSize(e.Graphics, state);
    Point glyphLocation = new Point(e.Bounds.Width / 2 - glyphSize.Width / 2, e.Bounds.Height / 2 - glyphSize.Height / 2);
    glyphLocation.Offset(e.Bounds.Left, e.Bounds.Top);
    CheckBoxRenderer.DrawCheckBox(e.Graphics, glyphLocation, state);
  }

  public void MouseClick(MouseEventArgs e)
  {
    int num1 = this.Bounds.Height / 2;
    Rectangle bounds = this.Bounds;
    int left = bounds.Left;
    bounds = this.Bounds;
    int num2 = bounds.Width / 2;
    int num3 = left + num2;
    if (e.X <= num3 - num1 || e.X >= num3 + num1)
      return;
    this.Checked = !this.Checked;
  }

  public event EventHandler OnClick;
}
