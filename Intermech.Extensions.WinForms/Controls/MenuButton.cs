// Decompiled with JetBrains decompiler
// Type: Intermech.Controls.MenuButton
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Controls;

public class MenuButton : Button
{
  private readonly bool _showMenuOnClick;

  protected override void OnMouseUp([NotNull] MouseEventArgs e)
  {
    if (this.Menu != null && e.Button == MouseButtons.Left && e.X >= this.ClientRectangle.Right - this.Height)
    {
      using (this.SetTempValue<bool>("_showMenuOnClick", true))
        base.OnMouseUp(e);
    }
    else
      base.OnMouseUp(e);
  }

  protected override void OnClick([NotNull] EventArgs e)
  {
    if (this._showMenuOnClick)
      this.ShowMenu();
    else
      base.OnClick(e);
  }

  private void ShowMenu(Point? p = null)
  {
    if (this.Menu == null)
      return;
    p = new Point?(p ?? new Point(1, this.Height - 1));
    this.Menu.Show((Control) this, p.Value);
  }

  protected override void OnPaint([NotNull] PaintEventArgs pEvent)
  {
    Rectangle clipRect;
    ref Rectangle local = ref clipRect;
    Rectangle clipRectangle = pEvent.ClipRectangle;
    int x1 = clipRectangle.X;
    clipRectangle = pEvent.ClipRectangle;
    int y1 = clipRectangle.Y;
    clipRectangle = pEvent.ClipRectangle;
    int width = Math.Max(clipRectangle.Width - this.Height, 0);
    clipRectangle = pEvent.ClipRectangle;
    int height = clipRectangle.Height;
    local = new Rectangle(x1, y1, width, height);
    base.OnPaint(new PaintEventArgs(pEvent.Graphics, clipRect));
    base.OnPaint(pEvent);
    if (this.Menu == null)
      return;
    Rectangle clientRectangle = this.ClientRectangle;
    int x2 = clientRectangle.Width - 14;
    clientRectangle = this.ClientRectangle;
    int y2 = clientRectangle.Height / 2 - 1;
    Brush brush = this.Enabled ? SystemBrushes.ControlText : SystemBrushes.ButtonShadow;
    Point[] points = new Point[3]
    {
      new Point(x2, y2),
      new Point(x2 + 7, y2),
      new Point(x2 + 3, y2 + 4)
    };
    pEvent.Graphics.FillPolygon(brush, points);
    clientRectangle = this.ClientRectangle;
    int num = clientRectangle.Width - this.Height;
    int y1_1 = y2 - 4;
    int y2_1 = y2 + 8;
    using (Pen pen = new Pen(Brushes.DarkGray)
    {
      DashStyle = DashStyle.Dot
    })
      pEvent.Graphics.DrawLine(pen, num, y1_1, num, y2_1);
  }

  protected override bool ProcessDialogKey(Keys keyData)
  {
    if (keyData == (Keys.Down | Keys.Alt))
      this.ShowMenu();
    return base.ProcessDialogKey(keyData);
  }

  [DefaultValue(null)]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [CanBeNull]
  public ContextMenuStrip Menu { get; set; }

  [DefaultValue(null)]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public int? SplitWidth { get; set; }
}
