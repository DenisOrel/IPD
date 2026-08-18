// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.ColoredDateTimePicker
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

#nullable disable
namespace Intermech.Project.Controls;

public class ColoredDateTimePicker : DateTimePicker
{
  [CanBeNull]
  private Brush _foreBrush;
  [NotNull]
  private readonly Brush _backBrush = (Brush) new SolidBrush(SystemColors.Window);
  [NotNull]
  private readonly Brush _disabledBrush = (Brush) new SolidBrush(SystemColors.ButtonFace);
  private Color _foreColor = SystemColors.ControlText;

  public ColoredDateTimePicker() => this.SetStyle(ControlStyles.UserPaint, true);

  [NotNull]
  private Brush ForeBrush
  {
    get => this._foreBrush ?? (this._foreBrush = (Brush) new SolidBrush(this.ForeColor));
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    Rectangle bounds = new Rectangle(this.ClientRectangle.Width - 17, 0, 17, 16 /*0x10*/);
    Brush brush1;
    ComboBoxState state;
    if (this.Enabled)
    {
      brush1 = this._backBrush;
      state = ComboBoxState.Normal;
    }
    else
    {
      brush1 = this._disabledBrush;
      state = ComboBoxState.Disabled;
    }
    Graphics graphics = e.Graphics;
    Brush brush2 = brush1;
    Rectangle clientRectangle = this.ClientRectangle;
    int width = clientRectangle.Width;
    clientRectangle = this.ClientRectangle;
    int height = clientRectangle.Height;
    graphics.FillRectangle(brush2, 0, 0, width, height);
    e.Graphics.DrawString(this.Text, this.Font, this.ForeBrush, 0.0f, 2f);
    ComboBoxRenderer.DrawDropDownButton(e.Graphics, bounds, state);
  }

  protected override void OnEnabledChanged([NotNull] EventArgs e)
  {
    base.OnEnabledChanged(e);
    this.ForeColor = this.Enabled ? SystemColors.ControlText : SystemColors.GrayText;
  }

  public override Color ForeColor
  {
    get => this._foreColor;
    set
    {
      if (!(this._foreColor != value))
        return;
      this._foreColor = value;
      this._foreBrush = (Brush) null;
      this.Refresh();
    }
  }

  protected override void Dispose(bool disposing)
  {
    base.Dispose(disposing);
    this._backBrush.Dispose();
    this._disabledBrush.Dispose();
    if (this._foreBrush == null)
      return;
    this._foreBrush.Dispose();
    this._foreBrush = (Brush) null;
  }
}
