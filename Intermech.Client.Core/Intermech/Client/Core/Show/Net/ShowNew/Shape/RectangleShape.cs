
// Type: Intermech.Client.Core.Show.Net.ShowNew.Shape.RectangleShape
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Show.Net.Stylus;
using Intermech.Interfaces.Show;
using Syncfusion.Pdf.Graphics;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace Intermech.Client.Core.Show.Net.ShowNew.Shape;

/// <summary>прямоугольник</summary>
[DebuggerDisplay("W={Weight} {Stylus.ColorDwg.UInt,h} {LineWeight} {Layer.Name}")]
internal sealed class RectangleShape : BaseShape
{
  private readonly RectangleD _rect;

  internal RectangleShape(RectangleD rect, ILayer layer, IStylus stylus, double lineWeight)
    : base(layer, stylus, lineWeight)
  {
    this._rect = rect;
    this.SetBound(this._rect);
  }

  internal override bool CheckBlank(RectangleD clipBox, double epsilon)
  {
    return Math.Abs(clipBox.Left - this._rect.Left) < epsilon && Math.Abs(clipBox.Right - this._rect.Right) < epsilon && Math.Abs(clipBox.Top - this._rect.Top) < epsilon && Math.Abs(clipBox.Bottom - this._rect.Bottom) < epsilon;
  }

  internal override void Draw(System.Drawing.Graphics graphics)
  {
    if (!this.Layer.Visible || !graphics.ClipBounds.IntersectsWith(RectangleD.ToRectangleF(this.BoundWeight)))
      return;
    Pen pen = this.Stylus.Pen;
    pen.Alignment = PenAlignment.Center;
    pen.Width = (float) this.Weight;
    RectangleF[] rects = new RectangleF[1]
    {
      RectangleD.ToRectangleF(this._rect)
    };
    try
    {
      graphics.DrawRectangles(pen, rects);
    }
    catch (OverflowException ex)
    {
    }
  }

  /// <summary>прорисовка в Pdf</summary>
  /// <param name="graphics">Graphics для рисования PDF</param>
  /// <param name="clipBox">Границы для рисования</param>
  internal override void Draw(PdfGraphics graphics, RectangleD clipBox)
  {
    if (!this.Layer.Visible)
      return;
    PdfPen pdfPen = this.Stylus.PdfPen;
    pdfPen.Width = (float) this.Weight;
    RectangleF[] rectangleFArray = new RectangleF[1]
    {
      RectangleD.ToRectangleF(this._rect)
    };
    try
    {
      foreach (RectangleF rectangle in rectangleFArray)
        graphics.DrawRectangle(pdfPen, rectangle);
    }
    catch (OverflowException ex)
    {
    }
  }
}
