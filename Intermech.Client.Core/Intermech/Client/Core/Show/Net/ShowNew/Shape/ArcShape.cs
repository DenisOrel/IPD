
// Type: Intermech.Client.Core.Show.Net.ShowNew.Shape.ArcShape
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

/// <summary> Описание дуги. </summary>
[DebuggerDisplay("{_startAngle}°+({_sweepAngle})° R={_radius} W={Weight} {Stylus.ColorDwg.UInt,h} {LineWeight} {Layer.Name}")]
internal sealed class ArcShape : BaseShape
{
  private RectangleD _boxArc;
  private double _startAngle;
  private double _sweepAngle;
  private double _radius;

  internal ArcShape(ILayer layer, IStylus stylus, double lineWeight)
    : base(layer, stylus, lineWeight)
  {
  }

  /// <summary>Перевести из полярных координат</summary>
  /// <param name="angle">полярный угол(радианы против часовой стрелки)</param>
  /// <param name="radius">длинна до точки</param>
  /// <returns>точка на дуге</returns>
  private PointD Potated(double angle, double radius)
  {
    return new PointD(radius * Math.Cos(angle), radius * Math.Sin(angle));
  }

  /// <summary>получить полярный угол(радианы против часовой стрелки)</summary>
  /// <param name="x"> </param>
  /// <param name="y"> </param>
  /// <returns>полярный угол(радианы против часовой стрелки)</returns>
  private double Angle(double x, double y)
  {
    return (y < 0.0 ? 2.0 * Math.PI : 0.0) + (Math.Abs(y) > 1E-16 || Math.Abs(x) > 1E-16 ? Math.Atan2(y, x) : 0.0);
  }

  internal BaseShape InitShort(PointD pnt1, PointD pnt2, PointD pntS, PointD pntE)
  {
    SizeD sizeD = new SizeD((pnt1.X + pnt2.X) / 2.0, (pnt1.Y + pnt2.Y) / 2.0);
    PointD pointD1 = pntS - sizeD;
    PointD pointD2 = pntE - sizeD;
    double radius = Math.Sqrt(pointD1.X * (pntS.X - sizeD.Width) + pointD1.Y * pointD1.Y);
    this._radius = radius;
    this._startAngle = this.Angle(pointD1.X, pointD1.Y);
    this._sweepAngle = this.Angle(pointD2.X, pointD2.Y);
    if (0.0 < (this._sweepAngle -= this._startAngle))
      this._sweepAngle -= 2.0 * Math.PI;
    this._boxArc.X = sizeD.Width - radius;
    this._boxArc.Y = sizeD.Height - radius;
    this._boxArc.Width = radius * 2.0;
    this._boxArc.Height = radius * 2.0;
    this.ExtendBounds(this.Potated(this._startAngle, radius) + sizeD);
    this.ExtendBounds(this.Potated(this._startAngle + this._sweepAngle, radius) + sizeD);
    double startAngle = this._startAngle;
    double num = startAngle + (startAngle < 0.0 ? 2.0 * Math.PI : 0.0);
    for (double angle = 0.0; angle < num + this._sweepAngle; angle += Math.PI / 2.0)
    {
      if (angle >= num)
        this.ExtendBounds(this.Potated(angle, radius) + sizeD);
    }
    this._startAngle *= 180.0 / Math.PI;
    this._sweepAngle *= 180.0 / Math.PI;
    return (BaseShape) this;
  }

  internal BaseShape Init(ConvertStream stream)
  {
    SizeD sizeD = new SizeD(stream.ReadPointD());
    PointD pointD1 = stream.ReadPointD();
    this._sweepAngle = stream.ReadDouble() * 2.0;
    PointD pointD2 = pointD1 - sizeD;
    double radius = Math.Sqrt(pointD2.X * pointD2.X + pointD2.Y * pointD2.Y);
    this._radius = radius;
    this._startAngle = this.Angle(pointD2.X, pointD2.Y) - this._sweepAngle / 2.0;
    this._boxArc.X = sizeD.Width - radius;
    this._boxArc.Y = sizeD.Height - radius;
    this._boxArc.Width = radius * 2.0;
    this._boxArc.Height = radius * 2.0;
    this.ExtendBounds(pointD1);
    this.ExtendBounds(this.Potated(this._startAngle, radius) + sizeD);
    this.ExtendBounds(this.Potated(this._startAngle + this._sweepAngle, radius) + sizeD);
    double startAngle = this._startAngle;
    double num = startAngle + (startAngle < 0.0 ? 2.0 * Math.PI : 0.0);
    for (double angle = 0.0; angle < num + this._sweepAngle; angle += Math.PI / 2.0)
    {
      if (angle >= num)
        this.ExtendBounds(this.Potated(angle, radius) + sizeD);
    }
    this._startAngle *= 180.0 / Math.PI;
    this._sweepAngle *= 180.0 / Math.PI;
    return (BaseShape) this;
  }

  internal override void Draw(System.Drawing.Graphics graphics)
  {
    if (!this.Layer.Visible || !graphics.ClipBounds.IntersectsWith(RectangleD.ToRectangleF(this.BoundWeight)))
      return;
    Pen pen = this.Stylus.Pen;
    pen.Alignment = PenAlignment.Center;
    pen.Width = (float) this.Weight;
    try
    {
      graphics.DrawArc(pen, RectangleD.ToRectangleF(this._boxArc), (float) this._startAngle, (float) this._sweepAngle);
    }
    catch (OverflowException ex)
    {
    }
  }

  private bool IsLine(RectangleF box, System.Drawing.Graphics graphics)
  {
    PointF[] pts = new PointF[2]
    {
      new PointF(box.Left, box.Top),
      new PointF(box.Left, box.Bottom)
    };
    graphics.Transform.TransformPoints(pts);
    pts[0].X -= pts[1].X;
    pts[0].Y -= pts[1].Y;
    return Math.Sqrt((double) pts[0].X * (double) pts[0].X + (double) pts[0].Y * (double) pts[0].Y) <= 9.9999997473787516E-05;
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
    try
    {
      graphics.DrawArc(pdfPen, RectangleD.ToRectangleF(this._boxArc), (float) this._startAngle, (float) this._sweepAngle);
    }
    catch (OverflowException ex)
    {
    }
  }
}
