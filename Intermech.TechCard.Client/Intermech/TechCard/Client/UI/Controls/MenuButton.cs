// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.UI.Controls.MenuButton
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.UI.Controls;

/// <summary>DropDown menu button</summary>
public class MenuButton : Button
{
  /// <summary>
  /// 
  /// </summary>
  public MenuButton() => this.SplitWidth = 20;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="args"></param>
  protected override void OnMouseDown(MouseEventArgs args)
  {
    Rectangle rectangle = new Rectangle(this.Width - this.SplitWidth, 0, this.Width, this.Height);
    if (this.Menu != null && args.Button == MouseButtons.Left && rectangle.Contains(args.Location))
      this.Menu.Show((Control) this, 0, this.Height);
    else
      base.OnMouseDown(args);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="args"></param>
  protected override void OnPaint(PaintEventArgs args)
  {
    Rectangle clipRect;
    ref Rectangle local = ref clipRect;
    Rectangle clipRectangle = args.ClipRectangle;
    int x1 = clipRectangle.X;
    clipRectangle = args.ClipRectangle;
    int y1 = clipRectangle.Y;
    clipRectangle = args.ClipRectangle;
    int width = Math.Max(clipRectangle.Width - this.SplitWidth, 0);
    clipRectangle = args.ClipRectangle;
    int height = clipRectangle.Height;
    local = new Rectangle(x1, y1, width, height);
    base.OnPaint(new PaintEventArgs(args.Graphics, clipRect));
    base.OnPaint(args);
    if (this.Menu == null || this.SplitWidth <= 0)
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
    args.Graphics.FillPolygon(brush, points);
    clientRectangle = this.ClientRectangle;
    int num = clientRectangle.Width - this.SplitWidth;
    int y1_1 = y2 - 4;
    int y2_1 = y2 + 8;
    using (Pen pen = new Pen(Brushes.DarkGray)
    {
      DashStyle = DashStyle.Dot
    })
      args.Graphics.DrawLine(pen, num, y1_1, num, y2_1);
  }

  /// <summary>DropDown Menu</summary>
  [DefaultValue(null)]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public ContextMenuStrip Menu { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue(20)]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public int SplitWidth { get; set; }
}
