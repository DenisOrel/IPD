
// Type: Intermech.Client.Core.Show.Net.ShowNew.Shape.ImageShape
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Show.Net.ShowNew.ExternFile;
using Intermech.Client.Core.Show.Net.Stylus;
using Intermech.Interfaces.Show;
using Syncfusion.Pdf.Graphics;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace Intermech.Client.Core.Show.Net.ShowNew.Shape;

/// <summary> Описание вставки рисунка </summary>
[DebuggerDisplay("{Stylus.ColorDwg.UInt,h} {LineWeight} {Layer.Name}")]
internal sealed class ImageShape : BaseShape, IDisposable
{
  private string _fileNameImage;
  private Image _image;
  private PdfImage _imagePdf;
  private PointD[] _boundsPnt;
  private PointD _basePnt;
  private PointD _vectorX;
  private PointD _vectorY;

  internal ImageShape(ILayer layer, IStylus stylus, double lineWeight)
    : base(layer, stylus, lineWeight)
  {
  }

  public void Dispose()
  {
    this._image?.Dispose();
    this._image = (Image) null;
    if (this._imagePdf is IDisposable imagePdf)
      imagePdf.Dispose();
    this._imagePdf = (PdfImage) null;
  }

  internal BaseShape Init(ImageTable images, ConvertStream stream)
  {
    this._fileNameImage = images[(int) stream.ReadInt16()];
    this._basePnt = stream.ReadPointD();
    this._vectorX = stream.ReadPointD();
    this._vectorY = stream.ReadPointD();
    this._boundsPnt = stream.ReadPointD((int) stream.ReadInt16());
    this.ExtendBounds(this._boundsPnt);
    return (BaseShape) this;
  }

  internal BaseShape InitShort(ImageTable images, ConvertStream stream, FormatterShort formatter)
  {
    this._fileNameImage = images[(int) stream.ReadInt16()];
    this._basePnt = formatter.ReCover(stream.ReadPointF32());
    this._vectorX = stream.ReadPointD();
    this._vectorY = stream.ReadPointD();
    this._boundsPnt = formatter.ReCover(stream.ReadPointF16((int) stream.ReadInt16()));
    this.ExtendBounds(this._boundsPnt);
    return (BaseShape) this;
  }

  private PointF[] ConvertToPointF()
  {
    PointF[] pointF = new PointF[this._boundsPnt.Length];
    for (int index = 0; index < pointF.Length; ++index)
    {
      pointF[index].X = (float) this._boundsPnt[index].X;
      pointF[index].Y = (float) this._boundsPnt[index].Y;
    }
    return pointF;
  }

  internal override void Draw(System.Drawing.Graphics graphics)
  {
    if (!this.Layer.Visible || !graphics.ClipBounds.IntersectsWith(RectangleD.ToRectangleF(this.BoundWeight)))
      return;
    Pen pen = this.Stylus.Pen;
    pen.Alignment = PenAlignment.Center;
    pen.Width = (float) this.Weight;
    GraphicsState gstate = graphics.Save();
    try
    {
      using (Region region = new Region())
      {
        GraphicsPath path = new GraphicsPath();
        path.AddPolygon(this.ConvertToPointF());
        region.Intersect(path);
        graphics.SetClip(region, CombineMode.Replace);
      }
      if (this._image == null)
        this._image = Image.FromFile(this._fileNameImage);
      if (this._image == null)
        return;
      PointF pointF1 = new PointF((float) (this._basePnt.X + this._vectorY.X), (float) (this._basePnt.Y + this._vectorY.Y));
      PointF pointF2 = new PointF((float) (this._basePnt.X + this._vectorY.X + this._vectorX.X), (float) (this._basePnt.Y + this._vectorY.Y + this._vectorX.Y));
      PointF pointF3 = new PointF((float) this._basePnt.X, (float) this._basePnt.Y);
      graphics.DrawImage(this._image, new PointF[3]
      {
        pointF1,
        pointF2,
        pointF3
      });
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
    this.Stylus.PdfPen.Width = (float) this.Weight;
    PdfGraphicsState state = graphics.Save();
    try
    {
      using (GraphicsPath graphicsPath = new GraphicsPath())
      {
        graphicsPath.AddPolygon(this.ConvertToPointF());
        PdfPath path = new PdfPath((PdfBrush) new PdfSolidBrush((PdfColor) Color.Empty), PdfFillMode.Alternate, graphicsPath.PathPoints, graphicsPath.PathTypes);
        graphics.SetClip(path, PdfFillMode.Alternate);
      }
      if (this._imagePdf == null)
        this._imagePdf = PdfImage.FromFile(this._fileNameImage);
      if (this._imagePdf == null)
        return;
      float angle = Atan2(this._vectorX);
      float num = Atan2(this._vectorY);
      graphics.TranslateTransform((float) (this._basePnt.X + this._vectorY.X), (float) (this._basePnt.Y + this._vectorY.Y));
      graphics.RotateTransform(angle);
      graphics.SkewTransform(0.0f, (float) (90.0 - ((double) num - (double) angle)));
      graphics.DrawImage(this._imagePdf, PointF.Empty, new SizeF((float) Math.Sqrt(this._vectorX.X * this._vectorX.X + this._vectorX.Y * this._vectorX.Y), (float) Math.Sqrt(this._vectorY.X * this._vectorY.X + this._vectorY.Y * this._vectorY.Y)));
    }
    catch (OverflowException ex)
    {
    }
    finally
    {
      graphics.Restore(state);
    }

    static float Atan2(PointD v)
    {
      return (float) ((v.Y < 0.0 ? 360.0 : 0.0) + (v.IsEmpty ? 0.0 : Math.Atan2(v.Y, v.X) * 180.0 / Math.PI));
    }
  }
}
