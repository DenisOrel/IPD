// Decompiled with JetBrains decompiler
// Type: Intermech.UI.PanelAdv
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.UI;

public class PanelAdv : Panel
{
  private bool _selectable;

  protected override void Dispose(bool disposing)
  {
    int num = disposing ? 1 : 0;
    base.Dispose(disposing);
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (bool), "false")]
  public new bool DoubleBuffered
  {
    get => base.DoubleBuffered;
    set => base.DoubleBuffered = value;
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (bool), "false")]
  public bool Selectable
  {
    get => this._selectable;
    set
    {
      if (this._selectable == value)
        return;
      this._selectable = value;
      if (this._selectable)
      {
        if (!this.TabStop)
          this.TabStop = true;
        if (!this.GetStyle(ControlStyles.UserPaint))
          this.SetStyle(ControlStyles.UserPaint, true);
        this.SetStyle(ControlStyles.Selectable, this._selectable);
      }
      this.Invalidate();
    }
  }

  protected override bool ShowFocusCues => this._selectable;

  protected override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    if (!this._selectable || !this.Focused)
      return;
    ControlPaint.DrawFocusRectangle(e.Graphics, this.ClientRectangle);
  }

  protected override void OnEnter([NotNull] EventArgs e)
  {
    base.OnEnter(e);
    if (!this._selectable)
      return;
    this.Invalidate();
  }

  protected override void OnLeave([NotNull] EventArgs e)
  {
    base.OnLeave(e);
    if (!this._selectable)
      return;
    this.Invalidate();
  }
}
