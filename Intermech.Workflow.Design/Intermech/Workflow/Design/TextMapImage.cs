// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.TextMapImage
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Map;
using System.Drawing;

#nullable disable
namespace Intermech.Workflow.Design;

internal class TextMapImage : MapImageEx
{
  private string _text = string.Empty;
  public Brush BackgroundBrush;
  public Font Font;
  public Brush Brush;

  public TextMapImage(string text)
  {
    this._text = text;
    this.Font = new Font("Arial", 9f, GraphicsUnit.Pixel);
  }

  public override void Paint(Graphics g, MapView view)
  {
    if (this.BackgroundBrush == null)
      this.BackgroundBrush = (Brush) new SolidBrush(Color.FromArgb(150, Color.White));
    g.FillRectangle(this.BackgroundBrush, this.Bounds);
    StringFormat format = new StringFormat(StringFormat.GenericTypographic);
    format.Alignment = StringAlignment.Center;
    format.FormatFlags &= ~StringFormatFlags.LineLimit;
    if (this.Brush == null)
      this.Brush = (Brush) new SolidBrush(Color.FromArgb(0, 100, 0));
    g.DrawString(this._text, this.Font, this.Brush, this.Bounds, format);
  }
}
