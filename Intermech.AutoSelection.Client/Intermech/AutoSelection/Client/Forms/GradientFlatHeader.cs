// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Forms.GradientFlatHeader
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using DevAge.Drawing;
using SourceGrid3;
using SourceGrid3.Cells.Views;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoSelection.Client.Forms;

public class GradientFlatHeader : Header
{
  private static readonly GradientFlatHeader Default = new GradientFlatHeader();
  private static readonly GradientFlatHeader GradientColumnHeader = new GradientFlatHeader();
  private static readonly GradientFlatHeader GradientRowHeader;

  static GradientFlatHeader()
  {
    GradientFlatHeader gradientFlatHeader = new GradientFlatHeader();
    gradientFlatHeader.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
    GradientFlatHeader.GradientRowHeader = gradientFlatHeader;
  }

  public GradientFlatHeader()
  {
    this.UseTheme = false;
    this.BackColor = Color.FromKnownColor(KnownColor.Control);
    this.Border = new RectangleBorder(new DevAge.Drawing.Border(SystemColors.ControlDark), new DevAge.Drawing.Border(SystemColors.ControlDark));
    this.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleLeft;
  }

  public GradientFlatHeader(GradientFlatHeader pSource)
    : base((Header) pSource)
  {
  }

  public override object Clone() => (object) new GradientFlatHeader(this);

  protected override void DrawCell_Background(
    CellContext cellContext,
    PaintEventArgs e,
    Rectangle pClientRectangle)
  {
    using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(pClientRectangle, ControlPaint.Light(this.BackColor, 0.5f), this.BackColor, LinearGradientMode.Vertical))
      e.Graphics.FillRectangle((Brush) linearGradientBrush, pClientRectangle);
  }
}
