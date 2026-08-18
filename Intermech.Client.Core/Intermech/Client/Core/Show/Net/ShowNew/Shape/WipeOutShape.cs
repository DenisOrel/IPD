
// Type: Intermech.Client.Core.Show.Net.ShowNew.Shape.WipeOutShape
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

/// <summary>вырезать область графики</summary>
[DebuggerDisplay("[{_points.Length,d}] {Layer.Name}")]
internal sealed class WipeOutShape : BaseShape
{
  private readonly PointD[] _pointDs;

  /// <summary>объект DWG</summary>
  internal IShowDwgWork Work { get; }

  internal WipeOutShape(
    PointD[] pointDs,
    ILayer layer,
    IStylus stylus,
    double lineWeight,
    IShowDwgWork work)
    : base(layer, stylus, lineWeight)
  {
    this.Work = work;
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
      using (GraphicsPath path = new GraphicsPath())
      {
        path.AddPolygon(pointF);
        graphics.SetClip(path, CombineMode.Replace);
        graphics.Clear(this.Work.PaperColor);
      }
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
      PdfPath path = new PdfPath();
      path.AddPolygon(pointF);
      path.FillMode = PdfFillMode.Winding;
      graphics.SetClip(path);
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
