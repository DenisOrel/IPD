// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.PdfGenerator.PdfImGraphics
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Native;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace Intermech.Document.Model.PdfGenerator;

internal class PdfImGraphics : ImGraphics
{
  private Metafile metafile;
  private string SkipRecord;
  private System.Drawing.Graphics.EnumerateMetafileProc metafileDelegate;
  private PdfGraphics pdfGr;
  private Bitmap bmp;
  private Dictionary<GraphicsState, PdfGraphicsState> stateDict = new Dictionary<GraphicsState, PdfGraphicsState>();

  public PdfTransformationMatrix GetMatrix(Matrix m)
  {
    return new PdfTransformationMatrix()
    {
      Matrix = m.Clone()
    };
  }

  public PdfBrush GetBrush(Brush br)
  {
    PdfBrush brush = (PdfBrush) null;
    if (br is SolidBrush)
      brush = (PdfBrush) new PdfSolidBrush(this.GetColor((br as SolidBrush).Color));
    return brush;
  }

  public PdfColor GetColor(Color color) => new PdfColor(color);

  public PdfStringFormat ConvertFormat(StringFormat format)
  {
    PdfStringFormat pdfStringFormat = (PdfStringFormat) null;
    if (format != null)
    {
      pdfStringFormat = new PdfStringFormat();
      pdfStringFormat.LineLimit = false;
      pdfStringFormat.Alignment = this.ConvertAlingnmet(format.Alignment);
      pdfStringFormat.LineAlignment = this.CovertLineAlignment(format.LineAlignment);
      format.GetTabStops(out float _);
      pdfStringFormat.NoClip = true;
      pdfStringFormat.RightToLeft = (format.FormatFlags & StringFormatFlags.DirectionRightToLeft) != 0;
      if (pdfStringFormat.NoClip)
        pdfStringFormat.LineLimit = false;
      pdfStringFormat.WordWrap = this.GetWrapType(format.FormatFlags);
    }
    return pdfStringFormat;
  }

  internal PdfVerticalAlignment CovertLineAlignment(StringAlignment stringAlignment)
  {
    if (stringAlignment == StringAlignment.Near)
      return PdfVerticalAlignment.Top;
    return stringAlignment == StringAlignment.Far ? PdfVerticalAlignment.Bottom : PdfVerticalAlignment.Middle;
  }

  private PdfWordWrapType GetWrapType(StringFormatFlags stringFormatFlags)
  {
    PdfWordWrapType wrapType = PdfWordWrapType.Word;
    if ((stringFormatFlags & StringFormatFlags.NoWrap) != (StringFormatFlags) 0)
      wrapType = PdfWordWrapType.None;
    return wrapType;
  }

  internal PdfTextAlignment ConvertAlingnmet(StringAlignment stringAlignment)
  {
    if (stringAlignment == StringAlignment.Center)
      return PdfTextAlignment.Center;
    return stringAlignment == StringAlignment.Far ? PdfTextAlignment.Right : PdfTextAlignment.Left;
  }

  public PdfPen GetPen(Pen pen)
  {
    PdfPen pen1 = new PdfPen((PdfColor) pen.Color, pen.Width);
    if (pen.DashStyle == DashStyle.Solid)
    {
      pen1.LineCap = this.ConvertCaps(pen.StartCap);
      pen1.LineCap = this.ConvertCaps(pen.EndCap);
    }
    pen1.DashStyle = this.ConvertDashStyle(pen.DashStyle);
    if (pen1.DashStyle != PdfDashStyle.Solid)
    {
      pen1.DashOffset = pen.DashOffset;
      pen1.DashPattern = pen.DashPattern;
    }
    pen1.LineJoin = this.ConvertJoin(pen.LineJoin);
    pen1.MiterLimit = pen.MiterLimit;
    pen1.Width = pen.Width * 2f;
    return pen1;
  }

  public Bitmap GetBitmap(Image image, SizeF size, Color backColor)
  {
    try
    {
      return this.GetBitmap(image, size, backColor, 30);
    }
    catch
    {
      try
      {
        return this.GetBitmap(image, size, backColor, 15);
      }
      catch
      {
        return this.GetBitmap(image, size, backColor, 10);
      }
    }
  }

  public Bitmap GetBitmap(Image image, SizeF size, Color backColor, int koef)
  {
    Bitmap bitmap1 = (Bitmap) null;
    if (image is Metafile)
    {
      size = new SizeF(size.Width * (float) koef, size.Height * (float) koef);
      double num1 = (double) size.Width / (double) image.Width;
      double num2 = (double) size.Height / (double) image.Height;
      Bitmap bitmap2 = new Bitmap((int) size.Width, (int) size.Height);
      using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage((Image) bitmap2))
      {
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.CompositingQuality = CompositingQuality.HighQuality;
        graphics.Clear(Color.Transparent);
        this.metafileDelegate = new System.Drawing.Graphics.EnumerateMetafileProc(this.MetafileCallback);
        this.metafile = image as Metafile;
        graphics.EnumerateMetafile(image as Metafile, new RectangleF(0.0f, 0.0f, size.Width, size.Height), this.metafileDelegate);
        this.metafile = (Metafile) null;
      }
      MemoryStream memoryStream = new MemoryStream();
      bitmap2.Save((Stream) memoryStream, ImageFormat.Png);
      bitmap1 = new Bitmap((Stream) memoryStream);
    }
    if (image is Bitmap)
      bitmap1 = image as Bitmap;
    return bitmap1;
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
      if (recordType == EmfPlusRecordType.EmfPolygon16 && this.SkipRecord == "EmfPolygon16")
        return true;
      if (recordType == EmfPlusRecordType.EmfExtTextOutW)
      {
        Type structureType = typeof (PdfImGraphics.EMR_EXTTEXTOUTA);
        if (((PdfImGraphics.EMR_EXTTEXTOUTA) Marshal.PtrToStructure(data, structureType)).emrtext.nChars == 0)
          return true;
      }
      if (recordType == EmfPlusRecordType.EmfGdiComment)
      {
        EMR_GDICOMMENT structure = new EMR_GDICOMMENT();
        EMR_GDICOMMENT structureEx = (EMR_GDICOMMENT) this.GetStructureEx(data, (ValueType) structure);
        try
        {
          string str = Encoding.Unicode.GetString(structureEx.data1);
          if (str.StartsWith("#Skip#"))
            this.SkipRecord = str.Replace("#Skip#", "");
        }
        catch
        {
        }
        return true;
      }
      numArray = new byte[dataSize];
      Marshal.Copy(data, numArray, 0, dataSize);
    }
    this.metafile.PlayRecord(recordType, flags, dataSize, numArray);
    return true;
  }

  private ValueType GetStructureEx(IntPtr ptr, ValueType structure)
  {
    object obj = (object) structure;
    Type type = structure.GetType();
    IntPtr ptr1 = new IntPtr(ptr.ToInt64());
    FieldInfo[] fields = type.GetFields();
    uint num1 = 0;
    int index1 = 0;
    for (int length = fields.Length; index1 < length; ++index1)
    {
      FieldInfo fieldInfo = fields[index1];
      Type fieldType = fieldInfo.FieldType;
      if (!fieldType.IsArray)
      {
        object structure1 = Marshal.PtrToStructure(ptr1, fieldType);
        fieldInfo.SetValue(obj, structure1, BindingFlags.Public, (Binder) null, CultureInfo.InvariantCulture);
        if (fieldType == typeof (uint))
          num1 = (uint) structure1;
        int num2 = Marshal.SizeOf(fieldType);
        ptr1 = new IntPtr(ptr1.ToInt64() + (long) num2);
      }
      else
      {
        Type elementType = fieldType.GetElementType();
        int num3 = Marshal.SizeOf(elementType);
        ArrayList arrayList = new ArrayList();
        for (int index2 = 0; (long) index2 < (long) num1; ++index2)
        {
          object structure2 = Marshal.PtrToStructure(ptr1, elementType);
          arrayList.Add(structure2);
          ptr1 = new IntPtr(ptr1.ToInt64() + (long) num3);
        }
        Array array = arrayList.ToArray(elementType);
        fieldInfo.SetValue(obj, (object) array, BindingFlags.Instance, (Binder) null, CultureInfo.InvariantCulture);
      }
    }
    structure = (ValueType) obj;
    return structure;
  }

  public PdfImage GetImage(Image image)
  {
    PdfImage image1 = (PdfImage) null;
    if (image is Bitmap)
      image1 = (PdfImage) new PdfBitmap(image);
    if (image is Metafile)
      image1 = (PdfImage) new PdfMetafile(image as Metafile);
    return image1;
  }

  public PdfPath GetPath(GraphicsPath path)
  {
    PointF[] points = (PointF[]) path.PathPoints.Clone();
    for (int index = 0; index < points.Length; ++index)
      points[index] = this.To(points[index]);
    byte[] pathTypes = path.PathTypes;
    return new PdfPath(points, pathTypes);
  }

  internal PdfDashStyle ConvertDashStyle(DashStyle dashStyle)
  {
    switch (dashStyle)
    {
      case DashStyle.Dash:
        return PdfDashStyle.Dash;
      case DashStyle.Dot:
        return PdfDashStyle.Dot;
      case DashStyle.DashDot:
        return PdfDashStyle.DashDot;
      case DashStyle.DashDotDot:
        return PdfDashStyle.DashDotDot;
      case DashStyle.Custom:
        return PdfDashStyle.Custom;
      default:
        return PdfDashStyle.Solid;
    }
  }

  internal PdfLineCap ConvertCaps(LineCap cap)
  {
    if (cap == LineCap.Square)
      return PdfLineCap.Square;
    return cap == LineCap.Round ? PdfLineCap.Round : PdfLineCap.Flat;
  }

  internal PdfLineJoin ConvertJoin(LineJoin join)
  {
    if (join == LineJoin.Bevel)
      return PdfLineJoin.Bevel;
    return join == LineJoin.Round ? PdfLineJoin.Round : PdfLineJoin.Miter;
  }

  public PdfImGraphics(PdfGraphics pdfGr)
  {
    this.pdfGr = pdfGr;
    this.bmp = new Bitmap(1, 1);
    this.bmp.SetResolution(1440f, 1440f);
    this.g = System.Drawing.Graphics.FromImage((Image) this.bmp);
  }

  private Rectangle To(Rectangle rect)
  {
    return new Rectangle(UnitsConverter.MmToPoints((float) rect.X), UnitsConverter.MmToPoints((float) rect.Y), UnitsConverter.MmToPoints((float) rect.Width), UnitsConverter.MmToPoints((float) rect.Height));
  }

  private RectangleF To(RectangleF rect)
  {
    return new RectangleF(UnitsConverter.MmToPointsF(rect.X), UnitsConverter.MmToPointsF(rect.Y), UnitsConverter.MmToPointsF(rect.Width), UnitsConverter.MmToPointsF(rect.Height));
  }

  private Point To(Point point)
  {
    return new Point(UnitsConverter.MmToPoints((float) point.X), UnitsConverter.MmToPoints((float) point.Y));
  }

  private PointF To(PointF point)
  {
    return new PointF(UnitsConverter.MmToPointsF(point.X), UnitsConverter.MmToPointsF(point.Y));
  }

  private int To(int x) => UnitsConverter.MmToPoints((float) x);

  private float To(float x) => UnitsConverter.MmToPointsF(x);

  public override void SetClip(Rectangle rect)
  {
    this.pdfGr.SetClip((RectangleF) this.To(rect));
    this.g.SetClip(rect);
  }

  public override void SetClip(RectangleF rect)
  {
    this.pdfGr.SetClip(this.To(rect));
    this.g.SetClip(rect);
  }

  public override void SetClip(Rectangle rect, CombineMode combineMode)
  {
    this.pdfGr.SetClip((RectangleF) this.To(rect));
    this.g.SetClip(rect, combineMode);
  }

  public override void SetClip(Region region, CombineMode combineMode)
  {
    this.g.SetClip(region, combineMode);
  }

  public override GraphicsUnit PageUnit
  {
    get => this.g.PageUnit;
    set => this.g.PageUnit = value;
  }

  public override GraphicsState Save()
  {
    PdfGraphicsState pdfGraphicsState = this.pdfGr.Save();
    GraphicsState key = this.g.Save();
    this.stateDict[key] = pdfGraphicsState;
    return key;
  }

  public override void Restore(GraphicsState gstate)
  {
    if (this.stateDict.ContainsKey(gstate))
      this.pdfGr.Restore(this.stateDict[gstate]);
    this.g.Restore(gstate);
  }

  public override void MultiplyTransform(Matrix matrix)
  {
    this.pdfGr.MultiplyTransform1(this.GetMatrix(matrix));
    this.g.MultiplyTransform(matrix);
  }

  public override void MultiplyTransform(Matrix matrix, System.Drawing.Drawing2D.MatrixOrder order)
  {
    this.pdfGr.MultiplyTransform1(this.GetMatrix(matrix));
    this.g.MultiplyTransform(matrix, order);
  }

  public override Matrix Transform
  {
    get => this.g.Transform;
    set => this.g.Transform = value;
  }

  public override Matrix Transform1 => this.pdfGr.Matrix.Matrix;

  public override void ResetTransform() => base.ResetTransform();

  public override void RotateTransform(float angle)
  {
    this.pdfGr.RotateTransform(angle);
    this.g.RotateTransform(angle);
  }

  public override void TranslateTransform(float dx, float dy)
  {
    this.pdfGr.TranslateTransform(this.To(dx), this.To(dy));
    this.g.TranslateTransform(dx, dy);
  }

  public override void ScaleTransform(float sx, float sy)
  {
    this.pdfGr.ScaleTransform(sx, sy);
    this.g.ScaleTransform(sx, sy);
  }

  public override CompositingQuality CompositingQuality
  {
    get => this.g.CompositingQuality;
    set => this.g.CompositingQuality = value;
  }

  public override void FillRectangle(Brush brush, Rectangle rect)
  {
    this.pdfGr.DrawRectangle(this.GetBrush(brush), (RectangleF) this.To(rect));
    this.g.FillRectangle(brush, rect);
  }

  public override void FillRectangle(Brush brush, RectangleF rect)
  {
    this.pdfGr.DrawRectangle(this.GetBrush(brush), this.To(rect));
    this.g.FillRectangle(brush, rect);
  }

  public override float DpiX => this.g.DpiX;

  public override float DpiY => this.g.DpiY;

  public override void DrawLine(Pen pen, Point pt1, Point pt2)
  {
    if (pen.Color != Color.Transparent)
      this.pdfGr.DrawLine(this.GetPen(pen), (PointF) this.To(pt1), (PointF) this.To(pt2));
    this.g.DrawLine(pen, pt1, pt2);
  }

  public override void DrawLine(Pen pen, PointF pt1, PointF pt2)
  {
    if (pen.Color != Color.Transparent)
      this.pdfGr.DrawLine(this.GetPen(pen), this.To(pt1), this.To(pt2));
    this.g.DrawLine(pen, pt1, pt2);
  }

  public override void DrawImage(Image image, Point point)
  {
    this.pdfGr.DrawImage(this.GetImage(image), (PointF) this.To(point));
    this.g.DrawImage(image, point);
  }

  public override void DrawImage(Image image, PointF point)
  {
    this.pdfGr.DrawImage(this.GetImage(image), this.To(point));
    this.g.DrawImage(image, point);
  }

  public override void DrawRectangle(Pen pen, float x, float y, float width, float height)
  {
    this.pdfGr.DrawRectangle(this.GetPen(pen), this.To(new RectangleF(x, y, width, height)));
    this.g.DrawRectangle(pen, x, y, width, height);
  }

  public override RectangleF ClipBounds => this.g.ClipBounds;

  public override void DrawImage(
    Image image,
    PointF[] destPoints,
    RectangleF srcRect,
    GraphicsUnit srcUnit,
    ImageAttributes imageAttr)
  {
    this.g.DrawImage(image, destPoints, srcRect, srcUnit, imageAttr);
  }

  public override void DrawImage(
    Image image,
    PointF[] destPoints,
    RectangleF srcRect,
    GraphicsUnit srcUnit)
  {
    this.g.DrawImage(image, destPoints, srcRect, srcUnit);
  }

  public override void DrawPath(Pen pen, GraphicsPath path)
  {
    this.pdfGr.DrawPath(this.GetPen(pen), this.GetPath(path));
    this.g.DrawPath(pen, path);
  }

  public override void DrawArc(Pen pen, RectangleF rect, float startAngle, float sweepAngle)
  {
    this.pdfGr.DrawArc(this.GetPen(pen), this.To(rect), startAngle, sweepAngle);
    this.g.DrawArc(pen, rect, startAngle, sweepAngle);
  }

  public override void DrawImage(Image image, RectangleF rect)
  {
    this.pdfGr.DrawImage(this.GetImage(image), this.To(rect));
    this.g.DrawImage(image, rect);
  }

  public override void DrawImageUnscaled(Image image, int x, int y)
  {
    this.pdfGr.DrawImage(this.GetImage(image), this.To(new PointF((float) x, (float) y)));
    this.g.DrawImageUnscaled(image, x, y);
  }

  public override void DrawImage(
    Image image,
    Rectangle destRect,
    int srcX,
    int srcY,
    int srcWidth,
    int srcHeight,
    GraphicsUnit srcUnit,
    ImageAttributes imageAttr)
  {
    this.g.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttr);
  }

  public override SizeF MeasureString(
    string text,
    Font font,
    SizeF layoutArea,
    StringFormat stringFormat,
    out int charactersFitted,
    out int linesFilled)
  {
    return this.g.MeasureString(text, font, layoutArea, stringFormat, out charactersFitted, out linesFilled);
  }

  public override void DrawString(
    string s,
    Font font,
    Brush brush,
    RectangleF layoutRectangle,
    StringFormat format)
  {
    this.pdfGr.DrawString(s, (PdfFont) new PdfTrueTypeFont(font, true), this.GetBrush(brush), this.To(layoutRectangle), this.ConvertFormat(format));
    this.g.DrawString(s, font, brush, layoutRectangle, format);
  }

  public override Region Clip
  {
    get => this.g.Clip;
    set => this.g.Clip = value;
  }

  internal struct EMR_EXTTEXTOUTA
  {
    public PdfImGraphics.RECT rclBounds;
    public int iGraphicsMode;
    public float exScale;
    public float eyScale;
    public PdfImGraphics.EMR_TEXT emrtext;
  }

  internal struct POINT
  {
    public int x;
    public int y;

    public POINT(int X, int Y)
    {
      this.x = X;
      this.y = Y;
    }

    public POINT(int lParam)
    {
      this.x = lParam & (int) ushort.MaxValue;
      this.y = lParam >> 16 /*0x10*/;
    }

    public static implicit operator Point(PdfImGraphics.POINT p) => new Point(p.x, p.y);

    public static implicit operator PointF(PdfImGraphics.POINT p)
    {
      return new PointF((float) p.x, (float) p.y);
    }

    public static implicit operator PdfImGraphics.POINT(Point p)
    {
      return new PdfImGraphics.POINT(p.X, p.Y);
    }
  }

  internal struct EMR_TEXT
  {
    public PdfImGraphics.POINT ptlReference;
    public int nChars;
    public int offString;
    public int fOptions;
    public PdfImGraphics.RECT rcl;
    public int offDx;
  }

  internal struct RECT(int x1, int y1, int x2, int y2)
  {
    public int left = x1;
    public int top = y1;
    public int right = x2;
    public int bottom = y2;

    public int Width => this.right - this.left;

    public int Height => this.bottom - this.top;

    public Point TopLeft => new Point(this.left, this.top);

    public Size Size => new Size(this.Width, this.Height);

    public override string ToString() => $"{this.TopLeft}x{this.Size}";

    public static implicit operator Rectangle(PdfImGraphics.RECT rect)
    {
      return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
    }

    public static implicit operator RectangleF(PdfImGraphics.RECT rect)
    {
      return RectangleF.FromLTRB((float) rect.left, (float) rect.top, (float) rect.right, (float) rect.bottom);
    }

    public static implicit operator Size(PdfImGraphics.RECT rect)
    {
      return new Size(rect.right - rect.left, rect.bottom - rect.top);
    }

    public static explicit operator PdfImGraphics.RECT(Rectangle rect)
    {
      return new PdfImGraphics.RECT()
      {
        left = rect.Left,
        right = rect.Right,
        top = rect.Top,
        bottom = rect.Bottom
      };
    }
  }
}
