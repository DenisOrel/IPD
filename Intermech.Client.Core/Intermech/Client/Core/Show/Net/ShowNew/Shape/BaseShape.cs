
// Type: Intermech.Client.Core.Show.Net.ShowNew.Shape.BaseShape
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Show.Net.DwgLayer;
using Intermech.Client.Core.Show.Net.Stylus;
using Intermech.Interfaces.Show;
using Syncfusion.Pdf.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace Intermech.Client.Core.Show.Net.ShowNew.Shape;

[DebuggerDisplay("{Stylus.ColorDwg.UInt,h} {LineWeight} {Layer.Name}")]
[DebuggerTypeProxy(typeof (ArcShape))]
[DebuggerTypeProxy(typeof (RectangleShape))]
[DebuggerTypeProxy(typeof (PolyLineShape))]
[DebuggerTypeProxy(typeof (TextShape))]
internal abstract class BaseShape : IShape
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal void ExtendBounds(params PointD[] pos)
  {
    if (pos == null)
      return;
    this.ExtendBounds(RectangleD.Empty, (IList<PointD>) pos);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal void ExtendBounds(RectangleD box, IList<PointD> pos)
  {
    if (pos == null)
      return;
    double num1;
    double num2;
    double y1;
    double x1;
    if (box.IsEmpty)
    {
      num2 = num1 = double.MinValue;
      x1 = y1 = double.MaxValue;
    }
    else
    {
      num2 = box.Right;
      num1 = box.Bottom;
      x1 = box.X;
      y1 = box.Y;
    }
    int count = pos.Count;
    for (int index = 0; index < count; ++index)
    {
      PointD po = pos[index];
      double x2 = po.X;
      double y2 = po.Y;
      if (num2 < x2)
        num2 = x2;
      if (num1 < y2)
        num1 = y2;
      if (x1 > x2)
        x1 = x2;
      if (y1 > y2)
        y1 = y2;
    }
    box = new RectangleD(x1, y1, num2 - x1, num1 - y1);
    this.SetBound(box);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal void SetBound(RectangleD box)
  {
    RectangleD bound = this.Bound;
    this.Bound = bound.Equals(RectangleD.Empty) ? box : RectangleD.Union(bound, box);
  }

  /// <summary>Слой в котором лежит примитив</summary>
  public ILayer Layer { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; [MethodImpl(MethodImplOptions.AggressiveInlining)] private set; }

  /// <summary>Acad-цвет примитива</summary>
  public IStylus Stylus { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; [MethodImpl(MethodImplOptions.AggressiveInlining)] private set; }

  /// <summary>толщина рисуемой линии в единицах  примитива</summary>
  public double LineWeight { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; [MethodImpl(MethodImplOptions.AggressiveInlining)] private set; }

  /// <summary>габариты примитива</summary>
  public RectangleD Bound { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; [MethodImpl(MethodImplOptions.AggressiveInlining)] private set; }

  /// <summary> полная толщина рисуемой линии</summary>
  public virtual double Weight
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.LineWeight + this.Stylus.Weight;
  }

  /// <summary>габариты примитива с учетом толщины пера</summary>
  public RectangleD BoundWeight
  {
    get
    {
      RectangleD bound = this.Bound;
      bound.Inflate(this.Weight, this.Weight);
      return bound;
    }
  }

  protected BaseShape(ILayer layer, IStylus stylus, double lineWeight)
  {
    this.Layer = layer;
    this.Stylus = stylus;
    this.LineWeight = lineWeight;
  }

  /// <summary>прорисовка в GDI+</summary>
  internal abstract void Draw(System.Drawing.Graphics graphics);

  /// <summary>прорисовка в Pdf</summary>
  /// <param name="graphics">Graphics для рисования PDF</param>
  /// <param name="clipBox">Границы для рисования</param>
  internal abstract void Draw(PdfGraphics graphics, RectangleD clipBox);

  internal virtual bool CheckBlank(RectangleD clipBox, double epsilon) => false;

  /// <summary>пересчёт габарита для слоя</summary>
  internal virtual void ReCalculationBound()
  {
    RectangleD boundWeight = this.BoundWeight;
    if (boundWeight == RectangleD.Empty || !(this.Layer is DwgLayerObject layer))
      return;
    RectangleD box = this.Layer.Bound == RectangleD.Empty ? boundWeight : RectangleD.Union(this.Layer.Bound, boundWeight);
    layer.SetBound(box);
  }
}
