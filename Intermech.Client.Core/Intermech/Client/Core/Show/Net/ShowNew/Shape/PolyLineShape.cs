
// Type: Intermech.Client.Core.Show.Net.ShowNew.Shape.PolyLineShape
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Show.Net.Extensions;
using Intermech.Client.Core.Show.Net.Stylus;
using Intermech.Interfaces.Show;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;


namespace Intermech.Client.Core.Show.Net.ShowNew.Shape;

/// <summary> Summary description for DPolyLine.</summary>
[DebuggerDisplay("[{_list.Count,d}] W={Weight} {Stylus.ColorDwg.UInt,h} {LineWeight} {Layer.Name}")]
internal sealed class PolyLineShape : BaseShape
{
  private readonly List<PointD> _list;

  internal PolyLineShape(List<PointD> lstPnt, ILayer layer, IStylus stylus, double lineWeight)
    : base(layer, stylus, lineWeight)
  {
    this._list = lstPnt;
    this.ExtendBounds(this._list.ToArray());
  }

  private PointF[] ConvertToPointF()
  {
    PointF[] pointF = new PointF[this._list.Count];
    for (int index = 0; index < pointF.Length; ++index)
    {
      ref PointF local1 = ref pointF[index];
      PointD pointD = this._list[index];
      double x = pointD.X;
      local1.X = (float) x;
      ref PointF local2 = ref pointF[index];
      pointD = this._list[index];
      double y = pointD.Y;
      local2.Y = (float) y;
    }
    return pointF;
  }

  /// <summary>проверка является ли замкнутый контур прямоугольником</summary>
  /// <returns>прямоугольник, иначе null</returns>
  internal BaseShape CheckCreateBox()
  {
    int count = this._list.Count;
    if (count != 5)
      return (BaseShape) null;
    if (this._list[0] != this._list[count - 1])
      return (BaseShape) null;
    double num1 = this._list.Min<PointD>((Func<PointD, double>) (p => p.X));
    double num2 = this._list.Min<PointD>((Func<PointD, double>) (p => p.Y));
    SizeD min = new SizeD(num1, num2);
    PointD[] array = this._list.Select<PointD, PointD>((Func<PointD, PointD>) (p => p - min)).Take<PointD>(4).ToArray<PointD>();
    int[] numArray = new int[4]{ -1, -1, -1, -1 };
    for (int index1 = 0; index1 < array.Length; ++index1)
    {
      int index2 = (Math.Abs(array[index1].Y) < 1E-05 ? 0 : 1) + (Math.Abs(array[index1].X) < 1E-05 ? 0 : 2);
      if (numArray[index2] != -1)
        return (BaseShape) null;
      numArray[index2] = index1;
    }
    if ((numArray[3] - numArray[0] + 4) % 4 != 2)
      return (BaseShape) null;
    if ((numArray[1] - numArray[2] + 4) % 4 != 2)
      return (BaseShape) null;
    double width = ((IEnumerable<PointD>) array).Max<PointD>((Func<PointD, double>) (p => p.X));
    if (Math.Abs(array[numArray[2]].X - width) >= 1E-05)
      return (BaseShape) null;
    if (Math.Abs(array[numArray[3]].X - width) >= 1E-05)
      return (BaseShape) null;
    double height = ((IEnumerable<PointD>) array).Max<PointD>((Func<PointD, double>) (p => p.Y));
    if (Math.Abs(array[numArray[1]].Y - height) >= 1E-05)
      return (BaseShape) null;
    return Math.Abs(array[numArray[3]].Y - height) >= 1E-05 ? (BaseShape) null : (BaseShape) new RectangleShape(new RectangleD(num1, num2, width, height), this.Layer, this.Stylus, this.LineWeight);
  }

  internal override void Draw(System.Drawing.Graphics graphics)
  {
    if (!this.Layer.Visible || !graphics.ClipBounds.IntersectsWith(RectangleD.ToRectangleF(this.BoundWeight)))
      return;
    Pen pen = this.Stylus.Pen;
    pen.Alignment = PenAlignment.Center;
    pen.Width = (float) this.Weight;
    PointF[] pointF = this.ConvertToPointF();
    try
    {
      graphics.DrawLines(pen, pointF);
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
    PointF[] pointF = this.ConvertToPointF();
    try
    {
      for (int index = 1; index < pointF.Length; ++index)
        graphics.DrawLine(pdfPen, pointF[index - 1], pointF[index]);
    }
    catch (OverflowException ex)
    {
    }
  }

  /// <summary>проверка можно ли объединить пару цепочек</summary>
  /// <param name="pline">новая цепочка</param>
  /// <returns>true - добавить цепочку</returns>
  internal bool CheckChainAdd(PolyLineShape pline)
  {
    if (this.Layer != pline.Layer || this.Stylus != pline.Stylus || this.LineWeight != pline.LineWeight || !this._list.AddChain<PointD>(pline._list))
      return false;
    if (!pline.Bound.IsEmpty)
      this.SetBound(pline.Bound);
    else
      this.ExtendBounds(this.Bound, (IList<PointD>) pline._list);
    return true;
  }
}
