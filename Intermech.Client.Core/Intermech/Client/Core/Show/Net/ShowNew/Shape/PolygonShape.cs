
// Type: Intermech.Client.Core.Show.Net.ShowNew.Shape.PolygonShape
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

/// <summary>Полигон</summary>
[DebuggerDisplay("[{_points.Length,d}] {PaperColor.ToArgb(),h} {Stylus.ColorDwg.UInt,h} {LineWeight} {Layer.Name}")]
internal sealed class PolygonShape : BaseShape
{
  private readonly PointD[] _pointDs;

  internal PolygonShape(PointD[] pointDs, ILayer layer, IStylus stylus, double lineWeight)
    : base(layer, stylus, lineWeight)
  {
    this._pointDs = pointDs;
    this.ExtendBounds(this._pointDs);
  }

  private PointF[] ConvertToPointF()
  {
    PointF[] pointF = new PointF[this._pointDs.Length];
    for (int index = 0; index < pointF.Length; ++index)
    {
      pointF[index].X = (float) this._pointDs[index].X;
      pointF[index].Y = (float) this._pointDs[index].Y;
    }
    return pointF;
  }

  internal override void Draw(System.Drawing.Graphics graphics)
  {
    if (!this.Layer.Visible || !graphics.ClipBounds.IntersectsWith(RectangleD.ToRectangleF(this.BoundWeight)))
      return;
    PointF[] pointF = this.ConvertToPointF();
    GraphicsState gstate = graphics.Save();
    try
    {
      SolidBrush solidBrush = this.Stylus.SolidBrush;
      graphics.FillPolygon((Brush) solidBrush, pointF);
    }
    catch (OverflowException ex)
    {
    }
    finally
    {
      graphics.Restore(gstate);
    }
  }

  /// <summary>прорисовка в Pdf</summary>
  /// <param name="graphics">Graphics для рисования PDF</param>
  /// <param name="clipBox">Границы для рисования</param>
  internal override void Draw(PdfGraphics graphics, RectangleD clipBox)
  {
    if (!this.Layer.Visible)
      return;
    PointF[] pointF = this.ConvertToPointF();
    PdfGraphicsState state = graphics.Save();
    try
    {
      PdfBrush pdfBrush = this.Stylus.PdfBrush;
      PdfPen pdfPen = this.Stylus.PdfPen;
      pdfPen.Width = (float) this.Weight;
      graphics.DrawPolygon(pdfPen, pdfBrush, pointF);
    }
    catch (OverflowException ex)
    {
    }
    finally
    {
      graphics.Restore(state);
    }
  }
}
