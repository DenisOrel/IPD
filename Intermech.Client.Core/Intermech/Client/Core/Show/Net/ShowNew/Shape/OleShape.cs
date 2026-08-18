
// Type: Intermech.Client.Core.Show.Net.ShowNew.Shape.OleShape
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Show.Net.Stylus;
using Intermech.Controls.OleContainer;
using Intermech.Interfaces.Show;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;


namespace Intermech.Client.Core.Show.Net.ShowNew.Shape;

/// <summary> Описание всавки OLE </summary>
[DebuggerDisplay("{Stylus.ColorDwg.UInt,h} {LineWeight} {Layer.Name}")]
internal sealed class OleShape : BaseShape, IDisposable
{
  private ImOleContainer _oleContainer;
  private Metafile _metafile;
  private List<PointD> _listPnt;
  private PointD _basePnt;
  private PointD _vectorX;
  private PointD _vectorY;

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  private static extern IntPtr GetDC(IntPtr a0);

  [DllImport("user32.dll")]
  private static extern int ReleaseDC(IntPtr p1, IntPtr p2);

  /// <summary>удалось ли создать рисунок из OLE-объекта</summary>
  internal bool IsMetafile => this._metafile != null;

  public void Dispose()
  {
    this._oleContainer?.Dispose();
    this._oleContainer = (ImOleContainer) null;
    this._metafile?.Dispose();
    this._metafile = (Metafile) null;
  }

  internal OleShape(ILayer layer, IStylus stylus, double lineWeight)
    : base(layer, stylus, lineWeight)
  {
  }

  /// <summary>преобразовать OLE-объект в рисунок</summary>
  /// <returns>рисунок иначе null</returns>
  internal Metafile OleTometafile()
  {
    Metafile metafile = (Metafile) null;
    IntPtr dc = OleShape.GetDC(IntPtr.Zero);
    try
    {
      metafile = new Metafile((Stream) new MemoryStream(), dc, EmfType.EmfPlusDual);
      using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage((Image) metafile))
      {
        graphics.PageUnit = GraphicsUnit.Pixel;
        graphics.SetClip(new Rectangle(0, 0, this._oleContainer.Width, this._oleContainer.Height), CombineMode.Replace);
        this._oleContainer.PaintOn(graphics, new Rectangle(0, 0, this._oleContainer.Width, this._oleContainer.Height));
      }
    }
    catch
    {
      metafile?.Dispose();
      metafile = (Metafile) null;
    }
    finally
    {
      OleShape.ReleaseDC(IntPtr.Zero, dc);
    }
    return metafile;
  }

  internal void Init(ConvertStream stream)
  {
    this._oleContainer = new ImOleContainer();
    this._oleContainer.CreateControl();
    byte[] buffer = stream.ReadBytes(stream.ReadInt32());
    int index = 22;
    using (MemoryStream memoryStream = new MemoryStream(buffer, index, buffer.Length - index))
    {
      memoryStream.Position = 0L;
      this._oleContainer.SourceData = (Stream) memoryStream;
      this._oleContainer.Update();
      this._metafile = this.OleTometafile();
    }
    this._basePnt = stream.ReadPointD();
    this._vectorX = stream.ReadPointD();
    this._vectorY = stream.ReadPointD();
    this._listPnt = new List<PointD>((IEnumerable<PointD>) stream.ReadPointD((int) stream.ReadInt16()));
    this.ExtendBounds(this._listPnt.ToArray());
  }

  internal void InitShort(ConvertStream stream, FormatterShort formatter)
  {
    this._oleContainer = new ImOleContainer();
    this._oleContainer.CreateControl();
    using (MemoryStream memoryStream = new MemoryStream(stream.ReadBytes(stream.ReadInt32())))
    {
      memoryStream.Position = 12L;
      this._oleContainer.SourceData = (Stream) memoryStream;
      this._metafile = this.OleTometafile();
    }
    this._basePnt = formatter.ReCover(stream.ReadPointF32());
    this._vectorX = stream.ReadPointD();
    this._vectorY = stream.ReadPointD();
    this._listPnt = new List<PointD>((IEnumerable<PointD>) formatter.ReCover(stream.ReadPointF16((int) stream.ReadInt16())));
    this.ExtendBounds(this._listPnt.ToArray());
  }

  private bool MetafileCallback(
    EmfPlusRecordType recordType,
    int flags,
    int dataSize,
    IntPtr data,
    PlayRecordCallback callbackData)
  {
    byte[] numArray = (byte[]) null;
    if (data != IntPtr.Zero)
    {
      numArray = new byte[dataSize];
      Marshal.Copy(data, numArray, 0, dataSize);
    }
    this._metafile.PlayRecord(recordType, flags, dataSize, numArray);
    return true;
  }

  /// <summary>создать рамку как отдельную полилинию</summary>
  /// <returns></returns>
  internal PolyLineShape CreatePolyLine()
  {
    return new PolyLineShape(this._listPnt, this.Layer, this.Stylus, this.LineWeight);
  }

  private PointF[] ConvertToPointF()
  {
    PointF[] pointF = new PointF[this._listPnt.Count];
    for (int index = 0; index < pointF.Length; ++index)
    {
      ref PointF local1 = ref pointF[index];
      PointD pointD = this._listPnt[index];
      double x = pointD.X;
      local1.X = (float) x;
      ref PointF local2 = ref pointF[index];
      pointD = this._listPnt[index];
      double y = pointD.Y;
      local2.Y = (float) y;
    }
    return pointF;
  }

  internal override void Draw(System.Drawing.Graphics graphics)
  {
    if (this._metafile == null || !this.Layer.Visible || !graphics.ClipBounds.IntersectsWith(RectangleD.ToRectangleF(this.BoundWeight)))
      return;
    Pen pen = this.Stylus.Pen;
    pen.Alignment = PenAlignment.Center;
    pen.Width = (float) this.Weight;
    try
    {
      RectangleF rectangleF = RectangleD.ToRectangleF(this.Bound);
      RectangleF srcRect = new RectangleF(new PointF(0.0f, 0.0f), (SizeF) this._metafile.Size);
      graphics.EnumerateMetafile(this._metafile, rectangleF, srcRect, GraphicsUnit.Pixel, new System.Drawing.Graphics.EnumerateMetafileProc(this.MetafileCallback));
    }
    catch (OverflowException ex)
    {
    }
    catch
    {
    }
  }

  /// <summary>прорисовка в Pdf</summary>
  /// <param name="graphics">Graphics для рисования PDF</param>
  /// <param name="clipBox">Границы для рисования</param>
  internal override void Draw(PdfGraphics graphics, RectangleD clipBox)
  {
    if (this._metafile == null || !this.Layer.Visible)
      return;
    PdfGraphicsState state = graphics.Save();
    try
    {
      using (PdfBitmap image = new PdfBitmap((Image) this.GetBitmap((SizeF) this._metafile.Size)))
      {
        RectangleF rectangleF = RectangleD.ToRectangleF(this.Bound);
        graphics.DrawImage((PdfImage) image, rectangleF);
      }
    }
    catch (OverflowException ex)
    {
    }
    catch
    {
    }
    finally
    {
      graphics.Restore(state);
    }
  }

  private Bitmap GetBitmap(SizeF size, int koef = 30)
  {
    size = new SizeF(size.Width * (float) koef, size.Height * (float) koef);
    using (Bitmap bitmap = new Bitmap((int) size.Width, (int) size.Height))
    {
      double num1 = (double) size.Width / (double) this._metafile.Width;
      double num2 = (double) size.Height / (double) this._metafile.Height;
      using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage((Image) bitmap))
      {
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.CompositingQuality = CompositingQuality.HighQuality;
        graphics.Clear(Color.Transparent);
        graphics.EnumerateMetafile(this._metafile, new RectangleF(0.0f, 0.0f, size.Width, size.Height), new System.Drawing.Graphics.EnumerateMetafileProc(this.MetafileCallback));
      }
      using (MemoryStream memoryStream = new MemoryStream())
      {
        bitmap.Save((Stream) memoryStream, ImageFormat.Png);
        return new Bitmap((Stream) memoryStream);
      }
    }
  }
}
