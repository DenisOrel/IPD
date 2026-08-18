
// Type: Intermech.Client.Core.Show.Net.ShowNew.Shape.PointShape
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

/// <summary> Описание точки </summary>
[DebuggerTypeProxy(typeof (BaseShape))]
[DebuggerDisplay("{Stylus.ColorDwg.UInt,h} {LineWeight} {Layer.Name}")]
internal sealed class PointShape : BaseShape
{
  private PointD _pos;

  internal PointShape(PointD pos, ILayer layer, IStylus stylus, double lineWeight)
    : base(layer, stylus, lineWeight)
  {
    this._pos = pos;
    this.SetBound(new RectangleD(this._pos.X, this._pos.Y, 0.0, 0.0));
  }

  internal override void Draw(System.Drawing.Graphics graphics)
  {
    if (!this.Layer.Visible || !graphics.ClipBounds.IntersectsWith(RectangleD.ToRectangleF(this.BoundWeight)))
      return;
    Pen pen = this.Stylus.Pen;
    pen.Alignment = PenAlignment.Center;
    pen.Width = (float) this.Weight;
    PointF pointF = new PointF((float) this._pos.X, (float) this._pos.Y);
    try
    {
      graphics.DrawLine(pen, pointF, pointF);
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
  }
}
