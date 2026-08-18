// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.Images.Metafiles.EmfPlusParser
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace Syncfusion.Pdf.Graphics.Images.Metafiles;

internal class EmfPlusParser : EmfParser
{
  private const int ColorFlag = 32768 /*0x8000*/;
  private const int IndexFlag = 255 /*0xFF*/;
  private int LineCloseFlag;
  private bool m_bProcess;
  private PdfUnitConvertor m_convertX;
  private PdfUnitConvertor m_convertY;
  private GraphicsPath m_currentPenCap;
  private ObjectData m_objects;
  private MetafileType m_type;
  private const int ObjectFlag = 65280;
  private const int PathFillWinding = 8192 /*0x2000*/;
  private const int RegionFlag = 268435456 /*0x10000000*/;
  private const int UseShorts = 16384 /*0x4000*/;

  public EmfPlusParser(PdfEmfRenderer renderer, SizeF dpi)
    : base(renderer)
  {
    this.m_objects = new ObjectData();
    this.LineCloseFlag = 8192 /*0x2000*/;
    this.m_convertX = new PdfUnitConvertor(dpi.Width);
    this.m_convertY = new PdfUnitConvertor(dpi.Height);
  }

  public EmfPlusParser(MetafileType type, SizeF dpi)
    : base(dpi)
  {
    this.m_objects = new ObjectData();
    this.LineCloseFlag = 8192 /*0x2000*/;
    this.m_type = type;
    this.m_convertX = new PdfUnitConvertor(dpi.Width);
    this.m_convertY = new PdfUnitConvertor(dpi.Height);
  }

  private void BeginContainer(byte[] data, int flags)
  {
    if (this.Renderer.EmbedFonts)
      return;
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    int index = 0;
    int floatSize = MetafileParser.FloatSize;
    GraphicsContainer state = this.Renderer.BeginContainer(this.ReadRectangle(data, ref index, floatSize), this.ReadRectangle(data, ref index, floatSize), (GraphicsUnit) flags);
    this.ReadInteger(data, ref index);
    this.Objects.SetState(index, (object) state);
  }

  private void BeginContainerNoParams(byte[] data)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    GraphicsContainer state = this.Renderer.BeginContainer();
    int index = 0;
    this.Objects.SetState(this.ReadInteger(data, ref index), (object) state);
  }

  private bool CheckForComplexScripts(string text)
  {
    foreach (char ch in text)
    {
      if (ch >= 'ऀ' && ch <= 'ॿ')
        return true;
    }
    return false;
  }

  private void Clear(byte[] data)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    int index = 0;
    this.Renderer.Clear(this.ReadColor(data, ref index));
  }

  private Region CombineRegion(Region srcRegion, Region dstRegion, CombineMode mode)
  {
    if (srcRegion == null)
      throw new ArgumentNullException(nameof (srcRegion));
    if (dstRegion == null)
      throw new ArgumentNullException(nameof (dstRegion));
    switch (mode)
    {
      case CombineMode.Replace:
        srcRegion = dstRegion.Clone();
        return srcRegion;
      case CombineMode.Intersect:
        srcRegion.Intersect(dstRegion);
        return srcRegion;
      case CombineMode.Union:
        srcRegion.Union(dstRegion);
        return srcRegion;
      case CombineMode.Xor:
        srcRegion.Xor(dstRegion);
        return srcRegion;
      case CombineMode.Exclude:
        srcRegion.Exclude(dstRegion);
        return srcRegion;
      case CombineMode.Complement:
        srcRegion.Complement(dstRegion);
        return srcRegion;
      default:
        return srcRegion;
    }
  }

  private bool ContainsColor(int flags) => (flags & 32768 /*0x8000*/) > 0;

  private void ConvertToPng(Image img, out MemoryStream stream)
  {
    try
    {
      EncoderParameter encoderParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
      EncoderParameters encoderParams = new EncoderParameters(1);
      encoderParams.Param[0] = encoderParameter;
      ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
      ImageCodecInfo encoder = (ImageCodecInfo) null;
      stream = new MemoryStream();
      foreach (ImageCodecInfo imageCodecInfo in imageEncoders)
      {
        if (imageCodecInfo.MimeType == "image/png")
        {
          encoder = imageCodecInfo;
          break;
        }
      }
      if (encoder == null)
        return;
      img.Save((Stream) stream, encoder, encoderParams);
    }
    catch (Exception ex)
    {
      stream = (MemoryStream) null;
    }
  }

  protected override System.Drawing.Graphics.EnumerateMetafileProc CreateParsingHandler()
  {
    return new System.Drawing.Graphics.EnumerateMetafileProc(this.EnumerateMetafile);
  }

  private GraphicsPath CreatePath(byte[] data, ref int index, int flags)
  {
    index += MetafileParser.IntSize;
    int int32 = BitConverter.ToInt32(data, index);
    index += MetafileParser.IntSize;
    BitConverter.ToInt32(data, index);
    PointF[] pointFArray = new PointF[int32];
    index += MetafileParser.IntSize;
    PointF[] pts = this.ReadPoints(data, ref index, int32, this.GetDataStep(flags));
    byte[] destinationArray = new byte[int32];
    Array.Copy((Array) data, index, (Array) destinationArray, 0, int32);
    byte[] types = destinationArray;
    GraphicsPath path = new GraphicsPath(pts, types);
    index += int32;
    return path;
  }

  private void DrawArc(byte[] data, int flags)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    if (!(this.Objects.GetObject(this.GetIndex(flags)) is Pen pen))
      return;
    int floatSize = MetafileParser.FloatSize;
    int index1 = 0;
    float startAngle = this.ReadNumber(data, index1, floatSize);
    int index2 = index1 + floatSize;
    float sweepAngle = this.ReadNumber(data, index2, floatSize);
    int index3 = index2 + floatSize;
    int dataStep = this.GetDataStep(flags);
    RectangleF rect = this.ReadRectangle(data, ref index3, dataStep);
    this.Renderer.DrawArc(pen, rect, startAngle, sweepAngle);
  }

  private void DrawBeziers(byte[] data, int flags)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    if (!(this.Objects.GetObject(this.GetIndex(flags)) is Pen pen))
      return;
    int startIndex = 0;
    int intSize = MetafileParser.IntSize;
    int int32 = BitConverter.ToInt32(data, startIndex);
    int index = startIndex + intSize;
    int dataStep = this.GetDataStep(flags);
    PointF[] points = this.ReadPoints(data, ref index, int32, dataStep);
    this.Renderer.DrawBeziers(pen, points);
  }

  private void DrawClosedCurve(byte[] data, int flags)
  {
    Pen pen = this.Objects.GetObject(this.GetIndex(flags)) as Pen;
    int index1 = 0;
    float tension = this.ReadNumber(data, index1, MetafileParser.IntSize);
    int startIndex = index1 + MetafileParser.IntSize;
    int int32 = BitConverter.ToInt32(data, startIndex);
    int index2 = startIndex + MetafileParser.IntSize;
    PointF[] points = this.ReadPoints(data, ref index2, int32, this.GetDataStep(flags));
    PdfFillMode fillMode = PdfFillMode.Winding;
    this.Renderer.DrawClosedCurve(pen, points, tension, fillMode);
  }

  private void DrawCurve(byte[] data, int flags)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    Pen pen = this.Objects.GetObject(this.GetIndex(flags)) as Pen;
    int index1 = 0;
    float tension = this.ReadNumber(data, index1, MetafileParser.IntSize);
    int index2 = index1 + MetafileParser.IntSize;
    float offset = this.ReadNumber(data, index2, MetafileParser.IntSize);
    int index3 = index2 + MetafileParser.IntSize;
    double num = (double) this.ReadNumber(data, index3, MetafileParser.IntSize);
    int startIndex = index3 + MetafileParser.IntSize;
    int int32 = BitConverter.ToInt32(data, startIndex);
    int index4 = startIndex + MetafileParser.IntSize;
    PointF[] points = this.ReadPoints(data, ref index4, int32, this.GetDataStep(flags));
    PointF[] penPoints = (PointF[]) null;
    if (this.m_currentPenCap != null)
      penPoints = this.m_currentPenCap.PathPoints;
    this.Renderer.DrawCurve(pen, points, penPoints, (int) offset, 1, tension);
  }

  private void DrawDriverString(byte[] recordData, int flags)
  {
    Font font = this.Objects.GetFont(this.GetIndex(flags));
    int index1 = 0;
    int floatSize = MetafileParser.FloatSize;
    Brush brush = this.GetBrush(recordData, ref index1, flags);
    BitConverter.ToInt32(recordData, index1);
    int startIndex1 = index1 + MetafileParser.IntSize;
    BitConverter.ToInt32(recordData, startIndex1);
    int startIndex2 = startIndex1 + MetafileParser.IntSize;
    int int32 = BitConverter.ToInt32(recordData, startIndex2);
    int index2 = startIndex2 + MetafileParser.IntSize;
    char[] chars = Encoding.Unicode.GetChars(recordData, index2, int32 * 2);
    string str = new string(chars);
    int index3 = index2 + int32 * 2;
    PointF pointF1 = new PointF();
    foreach (char ch in chars)
    {
      PointF pointF2 = this.ReadPoint(recordData, ref index3, floatSize);
      RectangleF rect = new RectangleF(pointF2.X, pointF2.Y - font.SizeInPoints, 0.0f, 0.0f);
      this.Renderer.DrawString(ch.ToString(), font, brush, rect);
    }
  }

  private void DrawEllipse(byte[] data, int flags)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    if (!(this.Objects.GetObject(this.GetIndex(flags)) is Pen pen))
      return;
    int index = 0;
    int dataStep = this.GetDataStep(flags);
    RectangleF rect = this.ReadRectangle(data, ref index, dataStep);
    this.Renderer.DrawEllipse(pen, rect);
  }

  private void DrawImage(byte[] data, int flags)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    if (!(this.Objects.GetObject(this.GetIndex(flags)) is Image image))
      return;
    int startIndex1 = 0;
    int intSize = MetafileParser.IntSize;
    int int32_1 = BitConverter.ToInt32(data, startIndex1);
    if (int32_1 >= 0)
      this.Objects.GetObject(int32_1);
    int startIndex2 = startIndex1 + intSize;
    GraphicsUnit int32_2 = (GraphicsUnit) BitConverter.ToInt32(data, startIndex2);
    int index = startIndex2 + intSize;
    int floatSize = MetafileParser.FloatSize;
    RectangleF srcRect = this.ReadRectangle(data, ref index, floatSize);
    int dataStep = this.GetDataStep(flags);
    RectangleF destRect = this.ReadRectangle(data, ref index, dataStep);
    MemoryStream stream = (MemoryStream) null;
    if (image is Bitmap)
      this.ConvertToPng(image, out stream);
    if (stream != null)
    {
      this.Renderer.DrawImage(Image.FromStream((Stream) stream), destRect, srcRect, int32_2);
      stream.Dispose();
    }
    else
      this.Renderer.DrawImage(image, destRect, srcRect, int32_2);
  }

  private void DrawImagePoints(byte[] data, int flags)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    Image image1 = this.Objects.GetObject(this.GetIndex(flags)) as Image;
    if (image1.RawFormat.Equals((object) ImageFormat.Gif))
    {
      MemoryStream memoryStream = new MemoryStream();
      image1.Save((Stream) memoryStream, ImageFormat.Png);
      image1 = Image.FromStream((Stream) memoryStream);
    }
    if (image1.PixelFormat == (PixelFormat) 8207)
    {
      Image image2 = image1;
      image1 = (Image) new Bitmap(image1.Width, image1.Height, PixelFormat.Format32bppArgb);
      System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(image1);
      graphics.DrawImage(image2, 0, 0, image1.Width, image1.Height);
      graphics.Dispose();
      image2.Dispose();
    }
    if (image1 == null)
      return;
    int startIndex1 = 0;
    int intSize = MetafileParser.IntSize;
    int int32_1 = BitConverter.ToInt32(data, startIndex1);
    int startIndex2 = startIndex1 + intSize;
    if (int32_1 >= 0)
      this.Objects.GetObject(int32_1);
    int int32_2 = BitConverter.ToInt32(data, startIndex2);
    int index1 = startIndex2 + intSize;
    GraphicsUnit units = (GraphicsUnit) int32_2;
    int floatSize = MetafileParser.FloatSize;
    RectangleF srcRect = this.ReadRectangle(data, ref index1, floatSize);
    int int32_3 = BitConverter.ToInt32(data, index1);
    index1 += floatSize;
    int dataStep = this.GetDataStep(flags);
    PointF[] points = this.ReadPoints(data, ref index1, int32_3, dataStep);
    bool flag = false;
    int num = 0;
    for (int index2 = 0; index2 < points.Length; ++index2)
    {
      if (points[index2] == PointF.Empty)
        ++num;
    }
    if (num == points.Length)
      flag = true;
    if (flag)
      return;
    this.Renderer.DrawImage(image1, points, srcRect, units);
  }

  private void DrawLines(byte[] data, int flags)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    if (!(this.Objects.GetObject(this.GetIndex(flags)) is Pen pen))
      return;
    int startIndex = 0;
    int intSize = MetafileParser.IntSize;
    int int32 = BitConverter.ToInt32(data, startIndex);
    int index = startIndex + intSize;
    int dataStep = this.GetDataStep(flags);
    PointF[] points = this.ReadPoints(data, ref index, int32, dataStep);
    bool closeShape = (flags & this.LineCloseFlag) == this.LineCloseFlag;
    this.Renderer.DrawLines(pen, points, closeShape);
  }

  private void DrawPath(byte[] data, int flags)
  {
    Pen pen = data != null ? this.Objects.GetObject(BitConverter.ToInt32(data, 0)) as Pen : throw new ArgumentNullException(nameof (data));
    GraphicsPath path = this.Objects.GetObject(this.GetIndex(flags)) as GraphicsPath;
    if (pen == null || path == null)
      return;
    this.Renderer.DrawPath(pen, path);
  }

  private void DrawPie(byte[] data, int flags)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    if (!(this.Objects.GetObject(this.GetIndex(flags)) is Pen pen))
      return;
    int index1 = 0;
    int floatSize = MetafileParser.FloatSize;
    float startAngle = this.ReadNumber(data, index1, floatSize);
    int index2 = index1 + floatSize;
    float sweepAngle = this.ReadNumber(data, index2, floatSize);
    int index3 = index2 + floatSize;
    int dataStep = this.GetDataStep(flags);
    RectangleF rect = this.ReadRectangle(data, ref index3, dataStep);
    this.Renderer.DrawPie(pen, rect, startAngle, sweepAngle);
  }

  private void DrawRects(byte[] data, int flags)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    if (!(this.Objects.GetObject(this.GetIndex(flags)) is Pen pen))
      return;
    int startIndex = 0;
    int intSize = MetafileParser.IntSize;
    int int32 = BitConverter.ToInt32(data, startIndex);
    int index1 = startIndex + intSize;
    int dataStep = this.GetDataStep(flags);
    RectangleF[] rects = new RectangleF[int32];
    for (int index2 = 0; index2 < int32; ++index2)
      rects[index2] = this.ReadRectangle(data, ref index1, dataStep);
    this.Renderer.DrawRectangles(pen, rects);
  }

  private void DrawString(byte[] data, int flags)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    int index1 = 0;
    int floatSize = MetafileParser.FloatSize;
    bool flag = false;
    Font font = this.Objects.GetFont(this.GetIndex(flags));
    Brush brush = this.GetBrush(data, ref index1, flags);
    this.Renderer.Graphics.m_isEMFPlus = true;
    int int32_1 = BitConverter.ToInt32(data, index1);
    int startIndex = index1 + MetafileParser.IntSize;
    StringFormat format = int32_1 >= 0 ? this.Objects.GetObject(int32_1) as StringFormat : (StringFormat) null;
    int int32_2 = BitConverter.ToInt32(data, startIndex);
    int index2 = startIndex + MetafileParser.IntSize;
    if (font.Name.ToLower().Equals("calibri"))
      this.Renderer.Graphics.m_isUseFontSize = true;
    RectangleF rectangleF = this.ReadRectangle(data, ref index2, floatSize);
    string str = new string(Encoding.Unicode.GetChars(data, index2, int32_2 * 2));
    if (PdfString.IsUnicode(str))
    {
      PdfFont pdfFont = (PdfFont) new PdfTrueTypeFont(font);
      try
      {
        pdfFont.MeasureString(str);
      }
      catch
      {
        if ((format.FormatFlags & StringFormatFlags.DirectionRightToLeft) != (StringFormatFlags) 0)
          font = new Font("Times New Roman", font.Size, font.Style);
        if (font.Name.ToLower().Equals("microsoft sans serif"))
          font = new Font("Arial Unicode MS", font.Size, font.Style);
        else if (font.Name.ToLower().Equals("arial"))
          font = new Font("Arial", font.Size, font.Style);
      }
      flag = this.CheckForComplexScripts(str);
    }
    if (!flag)
    {
      if (format != null && font != null && this.IsValidRect(rectangleF))
      {
        this.Renderer.DrawString(str, font, brush, rectangleF, format);
      }
      else
      {
        if (font == null || !this.IsValidRect(rectangleF))
          return;
        if ((double) rectangleF.Width == 0.0 && (double) rectangleF.Height == 0.0 && font.Name.ToLower().Equals("microsoft sans serif"))
        {
          using (Bitmap bitmap = new Bitmap(1, 1))
          {
            SizeF sizeF1 = System.Drawing.Graphics.FromImage((Image) bitmap).MeasureString(str, font);
            SizeF sizeF2 = new PdfTrueTypeFont(font).MeasureString(str);
            for (float num = sizeF1.Height - (float) font.Height; (double) sizeF1.Height - (double) num > (double) sizeF2.Height; sizeF2 = new PdfTrueTypeFont(font).MeasureString(str))
            {
              float emSize = font.Size + 0.5f;
              font = new Font(font.Name, emSize, font.Style);
            }
          }
        }
        this.Renderer.DrawString(str, font, brush, rectangleF);
      }
    }
    else
    {
      GraphicsPath path = new GraphicsPath();
      path.AddString(str, font.FontFamily, (int) font.Style, font.Size, rectangleF, format);
      this.Renderer.FillPath(brush, path);
      path.Dispose();
    }
  }

  private void DumpData(byte[] data, EmfPlusRecordType type)
  {
    int num = 0;
    while (num < data.Length)
      ++num;
  }

  private void EmfHeader(byte[] data)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    int intSize = MetafileParser.IntSize;
    int index = 0;
    Rectangle rectangle = this.ReadRectL(data, ref index);
    this.ReadRectL(data, ref index);
    BitConverter.ToInt32(data, index);
    index += intSize;
    BitConverter.ToInt32(data, index);
    index += intSize;
    BitConverter.ToInt32(data, index);
    index += intSize;
    BitConverter.ToInt32(data, index);
    index += intSize;
    BitConverter.ToInt32(data, index);
    index += intSize;
    BitConverter.ToInt32(data, index);
    index += intSize;
    BitConverter.ToInt32(data, index);
    index += intSize;
    BitConverter.ToInt32(data, index);
    index += intSize;
    BitConverter.ToInt32(data, index);
    index += intSize;
    BitConverter.ToInt32(data, index);
    index += intSize;
    PointF location1 = (PointF) rectangle.Location;
    if (location1 != PointF.Empty)
    {
      ++rectangle.Width;
      ++rectangle.Height;
      rectangle.Location = Point.Empty;
    }
    GraphicsPath graphicsPath = new GraphicsPath();
    SizeF sizeF = this.Renderer.EmbedFonts || location1 == PointF.Empty ? (SizeF) rectangle.Size : this.Renderer.Graphics.Size;
    float width = (double) sizeF.Width == (double) rectangle.Width ? 1f : sizeF.Width / (float) rectangle.Width;
    float height = (double) sizeF.Height + (double) location1.Y == (double) rectangle.Height ? 1f : sizeF.Height / (float) rectangle.Height;
    PointF location2 = PointF.Empty;
    if (location1 != PointF.Empty)
    {
      if ((double) location1.X < 0.0 || (double) location1.X > 0.0)
        width = (double) sizeF.Width + (double) location1.X == (double) rectangle.Width ? 1f : (sizeF.Width + location1.X) / (float) rectangle.Width;
      if ((double) location1.Y < 0.0 || (double) location1.Y > 0.0)
        height = (double) sizeF.Width + (double) location1.Y == (double) rectangle.Height ? 1f : (sizeF.Height + location1.Y) / (float) rectangle.Height;
      location2 = new PointF((float) (-(double) location1.X + 2.0), (float) (-(double) location1.Y + 2.0));
    }
    if ((double) location1.X >= 0.0 && (double) location1.Y >= 0.0)
      this.Renderer.SetBounds(location2, new SizeF(width, height));
    this.Header();
  }

  private void EmfProcess(
    EmfPlusRecordType recordType,
    int flags,
    int dataSize,
    IntPtr data,
    PlayRecordCallback callbackData)
  {
    if (recordType == EmfPlusRecordType.GetDC && this.Type != MetafileType.EmfPlusDual)
    {
      this.m_bProcess = true;
    }
    else
    {
      if (!this.m_bProcess)
        return;
      if (recordType < EmfPlusRecordType.EmfHeader || recordType > EmfPlusRecordType.EmfCreateColorSpaceW)
        this.m_bProcess = false;
      else
        base.EnumerateMetafile(recordType, flags, dataSize, data, callbackData);
    }
  }

  private void EndContainer(byte[] data)
  {
    if (this.Renderer.EmbedFonts)
      return;
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    this.Renderer.EndContainer(this.Objects.GetState(BitConverter.ToInt32(data, 0)) as GraphicsContainer);
  }

  private void EndOfFile() => this.Renderer.BeforeEnd();

  private new bool EnumerateMetafile(
    EmfPlusRecordType recordType,
    int flags,
    int dataSize,
    IntPtr data,
    PlayRecordCallback callbackData)
  {
    byte[] numArray = new byte[dataSize];
    if (data != IntPtr.Zero)
      Marshal.Copy(data, numArray, 0, dataSize);
    this.EmfProcess(recordType, flags, dataSize, data, callbackData);
    try
    {
      switch (recordType)
      {
        case EmfPlusRecordType.EmfHeader:
          this.EmfHeader(numArray);
          break;
        case EmfPlusRecordType.EmfEof:
        case EmfPlusRecordType.EndOfFile:
          this.EndOfFile();
          break;
        case EmfPlusRecordType.Header:
          this.Header(numArray, flags);
          break;
        case EmfPlusRecordType.Object:
          this.Object(numArray, flags);
          break;
        case EmfPlusRecordType.Clear:
          this.Clear(numArray);
          break;
        case EmfPlusRecordType.FillRects:
          this.FillRects(numArray, flags);
          break;
        case EmfPlusRecordType.DrawRects:
          this.DrawRects(numArray, flags);
          break;
        case EmfPlusRecordType.FillPolygon:
          this.FillPolygon(numArray, flags);
          break;
        case EmfPlusRecordType.DrawLines:
          this.DrawLines(numArray, flags);
          break;
        case EmfPlusRecordType.FillEllipse:
          this.FillEllipse(numArray, flags);
          break;
        case EmfPlusRecordType.DrawEllipse:
          this.DrawEllipse(numArray, flags);
          break;
        case EmfPlusRecordType.FillPie:
          this.FillPie(numArray, flags);
          break;
        case EmfPlusRecordType.DrawPie:
          this.DrawPie(numArray, flags);
          break;
        case EmfPlusRecordType.DrawArc:
          this.DrawArc(numArray, flags);
          break;
        case EmfPlusRecordType.FillRegion:
          this.FillRegion(numArray, flags);
          break;
        case EmfPlusRecordType.FillPath:
          this.FillPath(numArray, flags);
          break;
        case EmfPlusRecordType.DrawPath:
          this.DrawPath(numArray, flags);
          break;
        case EmfPlusRecordType.FillClosedCurve:
          this.FillClosedCurve(numArray, flags);
          break;
        case EmfPlusRecordType.DrawClosedCurve:
          this.DrawClosedCurve(numArray, flags);
          break;
        case EmfPlusRecordType.DrawCurve:
          this.DrawCurve(numArray, flags);
          break;
        case EmfPlusRecordType.DrawBeziers:
          this.DrawBeziers(numArray, flags);
          break;
        case EmfPlusRecordType.DrawImage:
          this.DrawImage(numArray, flags);
          break;
        case EmfPlusRecordType.DrawImagePoints:
          this.DrawImagePoints(numArray, flags);
          break;
        case EmfPlusRecordType.DrawString:
          this.DrawString(numArray, flags);
          break;
        case EmfPlusRecordType.SetRenderingOrigin:
          this.SetRenderingOrigin(numArray);
          break;
        case EmfPlusRecordType.SetAntiAliasMode:
          this.SetAntiAliasMode(numArray, flags);
          break;
        case EmfPlusRecordType.SetTextRenderingHint:
          this.SetTextRenderingHint(flags);
          break;
        case EmfPlusRecordType.SetTextContrast:
          this.SetTextContrast(flags);
          break;
        case EmfPlusRecordType.SetInterpolationMode:
          this.SetInterpolationMode(numArray, flags);
          break;
        case EmfPlusRecordType.SetPixelOffsetMode:
          this.SetPixelOffsetMode(numArray, flags);
          break;
        case EmfPlusRecordType.SetCompositingMode:
          this.SetComposingMode(numArray, flags);
          break;
        case EmfPlusRecordType.SetCompositingQuality:
          this.SetCompositingQuality(numArray, flags);
          break;
        case EmfPlusRecordType.Save:
          this.Save(numArray);
          break;
        case EmfPlusRecordType.Restore:
          this.Restore(numArray);
          break;
        case EmfPlusRecordType.BeginContainer:
          this.BeginContainer(numArray, flags);
          break;
        case EmfPlusRecordType.BeginContainerNoParams:
          this.BeginContainerNoParams(numArray);
          break;
        case EmfPlusRecordType.EndContainer:
          this.EndContainer(numArray);
          break;
        case EmfPlusRecordType.SetWorldTransform:
          this.SetWorldTransform(numArray);
          break;
        case EmfPlusRecordType.ResetWorldTransform:
          this.ResetWorldTransform(numArray);
          break;
        case EmfPlusRecordType.MultiplyWorldTransform:
          this.MultiplyWorldTransform(numArray, flags);
          break;
        case EmfPlusRecordType.TranslateWorldTransform:
          this.TranslateWorldTransform(numArray, flags);
          break;
        case EmfPlusRecordType.ScaleWorldTransform:
          this.ScaleWorldTransform(numArray, flags);
          break;
        case EmfPlusRecordType.RotateWorldTransform:
          this.RotateWorldTransform(numArray, flags);
          break;
        case EmfPlusRecordType.SetPageTransform:
          if (!this.Renderer.PageTransformed)
          {
            this.Renderer.PageTransformed = true;
            this.SetPageTransform(numArray, flags);
            break;
          }
          break;
        case EmfPlusRecordType.ResetClip:
          this.ResetClip(numArray);
          break;
        case EmfPlusRecordType.SetClipRect:
          this.SetClipRect(numArray, flags);
          break;
        case EmfPlusRecordType.SetClipPath:
          this.SetClipPath(numArray, flags);
          break;
        case EmfPlusRecordType.SetClipRegion:
          this.SetClipRegion(numArray, flags);
          break;
        case EmfPlusRecordType.OffsetClip:
          this.OffsetClip(numArray);
          break;
        case EmfPlusRecordType.DrawDriverString:
          this.DrawDriverString(numArray, flags);
          break;
        default:
          this.Renderer.m_EMFState = true;
          if (!base.EnumerateMetafile(recordType, flags, dataSize, data, callbackData))
            throw new Exception("Record not properly implemented");
          break;
      }
    }
    catch
    {
    }
    return true;
  }

  private void FillClosedCurve(byte[] data, int flags)
  {
    FillMode fillMode = this.GetFillMode(flags);
    int index1 = 0;
    Brush brush = this.GetBrush(data, ref index1, flags);
    float tension = this.ReadNumber(data, index1, MetafileParser.FloatSize);
    int int32 = BitConverter.ToInt32(data, index1);
    int index2 = index1 + MetafileParser.IntSize;
    int dataStep = this.GetDataStep(flags);
    PointF[] points = this.ReadPoints(data, ref index2, int32, dataStep);
    this.Renderer.FillClosedCurve(brush, points, fillMode, tension);
  }

  private void FillEllipse(byte[] data, int flags)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    int index = 0;
    Brush brush = this.GetBrush(data, ref index, flags);
    int dataStep = this.GetDataStep(flags);
    RectangleF rect = this.ReadRectangle(data, ref index, dataStep);
    this.Renderer.FillEllipse(brush, rect);
  }

  private void FillPath(byte[] data, int flags)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    GraphicsPath path = this.Objects.GetObject(this.GetIndex(flags)) as GraphicsPath;
    int index = 0;
    Brush brush = this.GetBrush(data, ref index, flags);
    if (path == null)
      return;
    this.Renderer.FillPath(brush, path);
  }

  private void FillPie(byte[] data, int flags)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    int index = 0;
    Brush brush = this.GetBrush(data, ref index, flags);
    int floatSize = MetafileParser.FloatSize;
    float startAngle = this.ReadNumber(data, index, floatSize);
    index += floatSize;
    float sweepAngle = this.ReadNumber(data, index, floatSize);
    index += floatSize;
    int dataStep = this.GetDataStep(flags);
    RectangleF rectangleF = this.ReadRectangle(data, ref index, dataStep);
    this.Renderer.FillPie(brush, rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height, startAngle, sweepAngle);
  }

  private void FillPolygon(byte[] data, int flags)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    int index = 0;
    Brush brush = this.GetBrush(data, ref index, flags);
    int intSize = MetafileParser.IntSize;
    int int32 = BitConverter.ToInt32(data, index);
    index += intSize;
    int dataStep = this.GetDataStep(flags);
    int fillMode = (int) this.GetFillMode(flags);
    PointF[] points = this.ReadPoints(data, ref index, int32, dataStep);
    this.Renderer.FillPolygon(brush, points);
  }

  private void FillRects(byte[] data, int flags)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    int index1 = 0;
    Brush brush = this.GetBrush(data, ref index1, flags);
    int intSize = MetafileParser.IntSize;
    int int32 = BitConverter.ToInt32(data, index1);
    index1 += intSize;
    int dataStep = this.GetDataStep(flags);
    RectangleF[] rects = new RectangleF[int32];
    for (int index2 = 0; index2 < int32; ++index2)
      rects[index2] = this.ReadRectangle(data, ref index1, dataStep);
    this.Renderer.FillRectangles(brush, rects);
  }

  private void FillRegion(byte[] data, int flags)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    int index = 0;
    Brush brush = this.GetBrush(data, ref index, flags);
    if (!(this.Objects.GetObject(this.GetIndex(flags)) is Region region))
      return;
    this.Renderer.FillRegion(brush, region);
  }

  private Brush GetBrush(byte[] data, ref int index, int flags)
  {
    if (this.ContainsColor(flags))
      return (Brush) new SolidBrush(this.ReadColor(data, ref index));
    int int32 = BitConverter.ToInt32(data, index);
    index += MetafileParser.IntSize;
    return this.Objects.GetBrush(int32);
  }

  private CombineMode GetCombineMode(int flags) => (CombineMode) (flags >> 8 & (int) byte.MaxValue);

  private int GetDataStep(int flags)
  {
    return (flags & 16384 /*0x4000*/) != 0 ? MetafileParser.ShortSize : MetafileParser.FloatSize;
  }

  private FillMode GetFillMode(int flags)
  {
    return (flags & 8192 /*0x2000*/) != 0 ? FillMode.Winding : FillMode.Alternate;
  }

  private int GetIndex(int flags) => flags & (int) byte.MaxValue;

  private MatrixOrder GetMatrixOrder(int flags)
  {
    return (flags & 8192 /*0x2000*/) != 0 ? MatrixOrder.Append : MatrixOrder.Prepend;
  }

  private void Header() => this.Renderer.BeforeStart();

  private void Header(byte[] data, int flags)
  {
    if (this.PageUnit != GraphicsUnit.Display)
      this.Renderer.PageUnit = this.PageUnit;
    if ((double) this.PageScale == 1.0)
      return;
    this.Renderer.PageScale = this.PageScale;
  }

  private bool IsValidRect(RectangleF rect)
  {
    return !float.IsNaN(rect.X) && !float.IsNaN(rect.Y) && !float.IsNaN(rect.Width) && !float.IsNaN(rect.Height);
  }

  private void MultiplyWorldTransform(byte[] data, int flags)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    int index = 0;
    int floatSize = MetafileParser.FloatSize;
    this.Renderer.MultiplyTransform(this.ReadMatrix(data, ref index, floatSize), this.GetMatrixOrder(flags));
  }

  private void Object(byte[] data, int flags)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    ObjectType objectType = (ObjectType) (flags & 65280);
    int index1 = this.GetIndex(flags);
    int index2 = 0;
    object obj = (object) null;
    switch (objectType)
    {
      case ObjectType.Brush:
        obj = (object) this.ReadBrush(data, ref index2);
        break;
      case ObjectType.Pen:
        obj = (object) this.ReadPen(data, ref index2);
        break;
      case ObjectType.Path:
        obj = (object) this.ReadPath(data, ref index2);
        break;
      case ObjectType.Region:
        obj = (object) this.ReadRegion(data, ref index2);
        break;
      case ObjectType.Image:
        obj = (object) this.ReadImage(data, ref index2);
        break;
      case ObjectType.Font:
        obj = (object) this.ReadFont(data, ref index2);
        break;
      case ObjectType.StringFormat:
        obj = (object) this.ReadStringFormat(data, ref index2);
        break;
    }
    if (obj == null)
      return;
    this.Objects.SetObject(index1, obj);
  }

  private void OffsetClip(byte[] data)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    int index1 = 0;
    int floatSize = MetafileParser.FloatSize;
    float dx = this.ReadNumber(data, index1, floatSize);
    int index2 = index1 + floatSize;
    float dy = this.ReadNumber(data, index2, floatSize);
    int num = index2 + floatSize;
    this.Renderer.TranslateClip(dx, dy);
  }

  private int ReadBlend(
    byte[] data,
    int start,
    int step,
    out float[] positions,
    out float[] factors)
  {
    int startIndex = start;
    int int32 = BitConverter.ToInt32(data, startIndex);
    int index1 = startIndex + MetafileParser.IntSize;
    positions = new float[int32];
    factors = new float[int32];
    for (int index2 = 0; index2 < int32; ++index2)
    {
      positions[index2] = this.ReadNumber(data, index1, MetafileParser.FloatSize);
      index1 += MetafileParser.FloatSize;
    }
    for (int index3 = 0; index3 < int32; ++index3)
    {
      factors[index3] = this.ReadNumber(data, index1, MetafileParser.FloatSize);
      index1 += MetafileParser.FloatSize;
    }
    Array.Sort<float, float>(positions, factors);
    return index1;
  }

  private Brush ReadBrush(byte[] data, ref int index)
  {
    index += MetafileParser.IntSize;
    BrushType int32 = (BrushType) BitConverter.ToInt32(data, index);
    index += MetafileParser.IntSize;
    switch (int32)
    {
      case BrushType.SolidBrush:
        return (Brush) new SolidBrush(this.ReadColor(data, ref index));
      case BrushType.HatchBrush:
        return this.ReadHatchBrush(data, ref index);
      case BrushType.TextureBrush:
        return this.ReadTextureBrush(data, ref index);
      case BrushType.PathGradientBrush:
        return this.ReadPathGradientBrush(data, ref index);
      case BrushType.LienarGradientBrush:
        return this.ReadGradientBrush(data, ref index);
      default:
        return (Brush) null;
    }
  }

  private Color ReadColor(byte[] data, ref int index)
  {
    byte num1 = data != null ? data[index] : throw new ArgumentNullException(nameof (data));
    ++index;
    byte num2 = data[index];
    ++index;
    byte num3 = data[index];
    ++index;
    int alpha = (int) data[index];
    ++index;
    int red = (int) num3;
    int green = (int) num2;
    int blue = (int) num1;
    return Color.FromArgb(alpha, red, green, blue);
  }

  private ColorBlend ReadColorBlend(byte[] data, ref int index, int step)
  {
    int int32 = BitConverter.ToInt32(data, index);
    index += MetafileParser.IntSize;
    float[] keys = new float[int32];
    for (int index1 = 0; index1 < int32; ++index1)
    {
      float num = this.ReadNumber(data, index, step);
      index += step;
      keys[index1] = num;
    }
    Color[] items = new Color[int32];
    for (int index2 = 0; index2 < int32; ++index2)
      items[index2] = this.ReadColor(data, ref index);
    ColorBlend colorBlend = new ColorBlend(int32);
    Array.Sort<float, Color>(keys, items);
    colorBlend.Positions = keys;
    colorBlend.Colors = items;
    return colorBlend;
  }

  private Font ReadFont(byte[] data, ref int index)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    index += MetafileParser.IntSize;
    int floatSize = MetafileParser.FloatSize;
    float num = this.ReadNumber(data, index, floatSize);
    index += floatSize;
    float emSize = num / this.Renderer.PageScale;
    int int32_1 = BitConverter.ToInt32(data, index);
    index += MetafileParser.IntSize;
    GraphicsUnit unit = (GraphicsUnit) int32_1;
    int int32_2 = BitConverter.ToInt32(data, index);
    index += MetafileParser.IntSize;
    FontStyle style = (FontStyle) int32_2;
    index += MetafileParser.IntSize;
    int int32_3 = BitConverter.ToInt32(data, index);
    index += MetafileParser.IntSize;
    int count = int32_3 << 1;
    char[] chars = Encoding.Unicode.GetChars(data, index, count);
    index += count;
    return new Font(new string(chars), emSize, style, unit);
  }

  private Brush ReadGradientBrush(byte[] data, ref int index)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    int length = 4;
    int intSize = MetafileParser.IntSize;
    GradientBrushFlags gradientBrushFlags = this.ReadGradientBrushFlags(data, ref index);
    WrapMode wrapMode = this.ReadWrapMode(data, ref index);
    int floatSize = MetafileParser.FloatSize;
    RectangleF rect = this.ReadRectangle(data, ref index, floatSize);
    Color[] colorArray = new Color[length];
    for (int index1 = 0; index1 < length; ++index1)
      colorArray[index1] = this.ReadColor(data, ref index);
    LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, colorArray[0], colorArray[1], 0.0f);
    linearGradientBrush.Blend = new Blend();
    linearGradientBrush.WrapMode = wrapMode;
    if ((gradientBrushFlags & GradientBrushFlags.Matrix) > GradientBrushFlags.Default)
    {
      Matrix matrix = this.ReadMatrix(data, ref index, floatSize);
      linearGradientBrush.Transform = matrix;
    }
    if ((gradientBrushFlags & GradientBrushFlags.Blend) > GradientBrushFlags.Default)
    {
      float[] positions;
      float[] factors;
      index = this.ReadBlend(data, index, MetafileParser.FloatSize, out positions, out factors);
      linearGradientBrush.Blend = new Blend()
      {
        Factors = factors,
        Positions = positions
      };
    }
    if ((gradientBrushFlags & GradientBrushFlags.ColorBlend) > GradientBrushFlags.Default && index < data.Length)
    {
      ColorBlend colorBlend = this.ReadColorBlend(data, ref index, MetafileParser.FloatSize);
      linearGradientBrush.InterpolationColors = colorBlend;
    }
    linearGradientBrush.GammaCorrection = (gradientBrushFlags & GradientBrushFlags.GammaCorrection) > GradientBrushFlags.Default;
    return (Brush) linearGradientBrush;
  }

  private GradientBrushFlags ReadGradientBrushFlags(byte[] data, ref int index)
  {
    int int32 = BitConverter.ToInt32(data, index);
    index += MetafileParser.IntSize;
    return (GradientBrushFlags) int32;
  }

  private Brush ReadHatchBrush(byte[] data, ref int index)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    int intSize = MetafileParser.IntSize;
    int int32 = BitConverter.ToInt32(data, index);
    index += intSize;
    Color foreColor = this.ReadColor(data, ref index);
    Color backColor = this.ReadColor(data, ref index);
    return (Brush) new HatchBrush((HatchStyle) int32, foreColor, backColor);
  }

  private Image ReadImage(byte[] data, ref int index)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    index += MetafileParser.IntSize;
    int intSize = MetafileParser.IntSize;
    Image image = (Image) null;
    int int32_1 = BitConverter.ToInt32(data, index);
    index += intSize;
    ObjectImageFormat objectImageFormat = (ObjectImageFormat) int32_1;
    if (objectImageFormat != ObjectImageFormat.Unknown)
    {
      bool flag = false;
      int length = 0;
      if (objectImageFormat != ObjectImageFormat.Bitmap)
      {
        if (objectImageFormat == ObjectImageFormat.Metafile)
        {
          int int32_2 = BitConverter.ToInt32(data, index);
          index += MetafileParser.IntSize;
          length = BitConverter.ToInt32(data, index);
          index += MetafileParser.IntSize;
          if (int32_2 == 2 && BitConverter.ToInt32(data, index) == -1698247209)
            index += MetafileParser.IntSize + 20;
        }
      }
      else
      {
        int int32_3 = BitConverter.ToInt32(data, index);
        index += intSize;
        int int32_4 = BitConverter.ToInt32(data, index);
        index += intSize;
        int int32_5 = BitConverter.ToInt32(data, index);
        index += intSize;
        PixelFormat int32_6 = (PixelFormat) BitConverter.ToInt32(data, index);
        index += intSize;
        flag = int32_3 != 0 && int32_4 != 0 && int32_5 != 0;
        index += MetafileParser.IntSize;
        if (flag)
        {
          length = int32_5 * int32_4;
          byte[] numArray = new byte[length];
          Array.Copy((Array) data, index, (Array) numArray, 0, length);
          IntPtr num = Marshal.AllocHGlobal(length);
          Marshal.Copy(numArray, 0, num, length);
          image = (Image) new Bitmap(int32_3, int32_4, int32_5, int32_6, num);
        }
        else
          length = data.Length - index;
      }
      if (!flag)
        image = Image.FromStream((Stream) new MemoryStream(data, index, length));
    }
    return image;
  }

  private int ReadInteger(byte[] data, ref int index) => BitConverter.ToInt32(data, index);

  private Matrix ReadMatrix(byte[] data, ref int index, int step)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    float[] numArray = new float[6];
    for (int index1 = 0; index1 < numArray.Length; ++index1)
    {
      float num = this.ReadNumber(data, index, step);
      index += step;
      numArray[index1] = num;
    }
    return new Matrix(numArray[0], numArray[1], numArray[2], numArray[3], numArray[4], numArray[5]);
  }

  private GraphicsPath ReadPath(byte[] data, ref int index)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    index += MetafileParser.IntSize;
    int floatSize = MetafileParser.FloatSize;
    int int32_1 = BitConverter.ToInt32(data, index);
    index += MetafileParser.IntSize;
    int int32_2 = BitConverter.ToInt32(data, index);
    index += MetafileParser.IntSize;
    FillMode fillMode = this.GetFillMode(int32_2);
    GraphicsPath graphicsPath;
    if (int32_1 > 0)
    {
      int dataStep = this.GetDataStep(int32_2);
      PointF[] pts = this.ReadPoints(data, ref index, int32_1, dataStep);
      byte[] destinationArray = new byte[int32_1];
      Array.Copy((Array) data, index, (Array) destinationArray, 0, int32_1);
      byte[] types = destinationArray;
      graphicsPath = new GraphicsPath(pts, types);
      index += int32_1;
      int num = int32_1 % 4;
      if (num > 0)
        index += 4 - num;
    }
    else
      graphicsPath = new GraphicsPath();
    graphicsPath.FillMode = fillMode;
    return graphicsPath;
  }

  private Brush ReadPathGradientBrush(byte[] data, ref int index)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    int intSize = MetafileParser.IntSize;
    GradientBrushFlags gradientBrushFlags = this.ReadGradientBrushFlags(data, ref index);
    WrapMode wrapMode = this.ReadWrapMode(data, ref index);
    Color color = this.ReadColor(data, ref index);
    PointF pointF1 = this.ReadPoint(data, ref index, MetafileParser.FloatSize);
    int int32 = BitConverter.ToInt32(data, index);
    index += MetafileParser.IntSize;
    Color[] colorArray = new Color[int32];
    for (int index1 = 0; index1 < int32; ++index1)
      colorArray[index1] = this.ReadColor(data, ref index);
    BitConverter.ToInt32(data, index);
    index += MetafileParser.IntSize;
    PathGradientBrush pathGradientBrush = new PathGradientBrush(this.ReadPath(data, ref index).PathPoints, wrapMode);
    pathGradientBrush.CenterColor = color;
    pathGradientBrush.CenterPoint = pointF1;
    pathGradientBrush.SurroundColors = colorArray;
    Blend blend = new Blend();
    pathGradientBrush.Blend = blend;
    if ((gradientBrushFlags & GradientBrushFlags.Matrix) != GradientBrushFlags.Default)
    {
      Matrix matrix = this.ReadMatrix(data, ref index, MetafileParser.FloatSize);
      pathGradientBrush.Transform = matrix;
    }
    if ((gradientBrushFlags & GradientBrushFlags.Blend) != GradientBrushFlags.Default)
    {
      float[] positions;
      float[] factors;
      index = this.ReadBlend(data, index, MetafileParser.FloatSize, out positions, out factors);
      pathGradientBrush.Blend = new Blend()
      {
        Factors = factors,
        Positions = positions
      };
    }
    if ((gradientBrushFlags & GradientBrushFlags.ColorBlend) != GradientBrushFlags.Default)
    {
      ColorBlend colorBlend = this.ReadColorBlend(data, ref index, MetafileParser.FloatSize);
      pathGradientBrush.InterpolationColors = colorBlend;
    }
    if ((gradientBrushFlags & GradientBrushFlags.FocusScales) != GradientBrushFlags.Default)
    {
      index += MetafileParser.IntSize;
      PointF pointF2 = this.ReadPoint(data, ref index, MetafileParser.FloatSize);
      pathGradientBrush.FocusScales = pointF2;
    }
    return (Brush) pathGradientBrush;
  }

  private Pen ReadPen(byte[] data, ref int index)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    this.m_currentPenCap = (GraphicsPath) null;
    BitConverter.ToInt32(data, index);
    index += MetafileParser.IntSize;
    index += MetafileParser.IntSize;
    int floatSize = MetafileParser.FloatSize;
    int int32_1 = BitConverter.ToInt32(data, index);
    index += MetafileParser.IntSize;
    PenFlags flags = (PenFlags) int32_1;
    index += MetafileParser.IntSize;
    float width = this.ReadNumber(data, index, floatSize);
    index += floatSize;
    Pen pen = new Pen(Color.Empty, width);
    if ((flags & PenFlags.Transform) != PenFlags.Default)
    {
      Matrix matrix = this.ReadMatrix(data, ref index, MetafileParser.FloatSize);
      pen.Transform = matrix;
    }
    if ((flags & PenFlags.StartCap) != PenFlags.Default)
    {
      int int32_2 = BitConverter.ToInt32(data, index);
      index += MetafileParser.IntSize;
      LineCap lineCap = (LineCap) int32_2;
      pen.StartCap = lineCap;
    }
    if ((flags & PenFlags.EndCap) != PenFlags.Default)
    {
      int int32_3 = BitConverter.ToInt32(data, index);
      index += MetafileParser.IntSize;
      LineCap lineCap = (LineCap) int32_3;
      pen.EndCap = lineCap;
    }
    if ((flags & PenFlags.LineJoin) != PenFlags.Default)
    {
      int int32_4 = BitConverter.ToInt32(data, index);
      index += MetafileParser.IntSize;
      LineJoin lineJoin = (LineJoin) int32_4;
      pen.LineJoin = lineJoin;
    }
    if ((flags & PenFlags.MiterLimit) != PenFlags.Default)
    {
      float single = BitConverter.ToSingle(data, index);
      index += MetafileParser.FloatSize;
      pen.MiterLimit = single;
    }
    if ((flags & PenFlags.DashStyle) != PenFlags.Default)
    {
      int int32_5 = BitConverter.ToInt32(data, index);
      index += MetafileParser.IntSize;
      DashStyle dashStyle = (DashStyle) int32_5;
      pen.DashStyle = dashStyle;
    }
    if ((flags & PenFlags.DashCap) != PenFlags.Default)
    {
      DashCap int32_6 = (DashCap) BitConverter.ToInt32(data, index);
      index += MetafileParser.IntSize;
      pen.DashCap = int32_6;
    }
    if ((flags & PenFlags.DashOffset) != PenFlags.Default)
    {
      float single = BitConverter.ToSingle(data, index);
      index += MetafileParser.FloatSize;
      pen.DashOffset = single;
    }
    if ((flags & PenFlags.DashPattern) != PenFlags.Default)
    {
      float[] numArray = this.ReadSingleArray(data, ref index, MetafileParser.FloatSize);
      pen.DashPattern = numArray;
    }
    if ((flags & PenFlags.Alignment) != PenFlags.Default)
    {
      PenAlignment int32_7 = (PenAlignment) BitConverter.ToInt32(data, index);
      index += MetafileParser.IntSize;
      pen.Alignment = int32_7;
    }
    if ((flags & PenFlags.CompoundArray) != PenFlags.Default)
    {
      float[] numArray = this.ReadSingleArray(data, ref index, MetafileParser.FloatSize);
      pen.CompoundArray = numArray;
    }
    if ((flags & PenFlags.CustomStartCap) != PenFlags.Default)
    {
      int int32_8 = BitConverter.ToInt32(data, index);
      index += MetafileParser.IntSize;
      index += int32_8;
    }
    if ((flags & PenFlags.CustomEndCap) != PenFlags.Default)
    {
      BitConverter.ToInt32(data, index);
      index += MetafileParser.IntSize;
      index += MetafileParser.IntSize;
      int int32_9 = BitConverter.ToInt32(data, index);
      index += MetafileParser.IntSize;
      switch (int32_9)
      {
        case 0:
          int int32_10 = BitConverter.ToInt32(data, index);
          index += MetafileParser.IntSize;
          index += 44;
          switch (int32_10)
          {
            case 1:
              BitConverter.ToInt32(data, index);
              index += MetafileParser.IntSize;
              BitConverter.ToInt32(data, index);
              GraphicsPath path = this.CreatePath(data, ref index, (int) flags);
              this.m_currentPenCap = new GraphicsPath(path.PathPoints, path.PathTypes);
              break;
            case 2:
              int int32_11 = BitConverter.ToInt32(data, index);
              index += MetafileParser.IntSize;
              index += int32_11;
              break;
          }
          break;
        case 1:
          this.Renderer.m_customLineCapArrowData = new CustomLineCapArrowData();
          this.Renderer.m_customLineCapArrowData.width = BitConverter.ToSingle(data, index);
          index += MetafileParser.IntSize;
          this.Renderer.m_customLineCapArrowData.height = BitConverter.ToSingle(data, index);
          index += MetafileParser.IntSize;
          this.Renderer.m_customLineCapArrowData.middleInset = BitConverter.ToSingle(data, index);
          index += MetafileParser.IntSize;
          this.Renderer.m_customLineCapArrowData.fillState = BitConverter.ToInt32(data, index);
          index += MetafileParser.IntSize;
          this.Renderer.m_customLineCapArrowData.lineStartCap = BitConverter.ToInt32(data, index);
          index += MetafileParser.IntSize;
          this.Renderer.m_customLineCapArrowData.lineEndCap = BitConverter.ToInt32(data, index);
          index += MetafileParser.IntSize;
          this.Renderer.m_customLineCapArrowData.lineJoin = BitConverter.ToInt32(data, index);
          index += MetafileParser.IntSize;
          this.Renderer.m_customLineCapArrowData.lineMitterLimit = BitConverter.ToSingle(data, index);
          index += MetafileParser.IntSize;
          this.Renderer.m_customLineCapArrowData.widthScale = BitConverter.ToSingle(data, index);
          index += MetafileParser.IntSize;
          index += 16 /*0x10*/;
          break;
      }
      pen.Width = width;
    }
    Brush brush = this.ReadBrush(data, ref index);
    pen.Brush = brush;
    return pen;
  }

  private PointF ReadPoint(byte[] data, ref int index, int step)
  {
    double x = (double) this.ReadNumber(data, index, step);
    index += step;
    float num = this.ReadNumber(data, index, step);
    index += step;
    double y = (double) num;
    return new PointF((float) x, (float) y);
  }

  private PointF[] ReadPoints(byte[] data, ref int index, int number, int step)
  {
    PointF[] pointFArray = new PointF[number];
    for (int index1 = 0; index1 < number; ++index1)
      pointFArray[index1] = this.ReadPoint(data, ref index, step);
    return pointFArray;
  }

  private RectangleF ReadRectangle(byte[] data, ref int index, int step)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    double x = (double) this.ReadNumber(data, index, step);
    index += step;
    float num1 = this.ReadNumber(data, index, step);
    index += step;
    float num2 = this.ReadNumber(data, index, step);
    index += step;
    float num3 = this.ReadNumber(data, index, step);
    index += step;
    double y = (double) num1;
    double width = (double) num2;
    double height = (double) num3;
    return new RectangleF((float) x, (float) y, (float) width, (float) height);
  }

  private Rectangle ReadRectL(byte[] data, ref int index)
  {
    int intSize = MetafileParser.IntSize;
    int int32_1 = BitConverter.ToInt32(data, index);
    index += intSize;
    int int32_2 = BitConverter.ToInt32(data, index);
    index += intSize;
    int int32_3 = BitConverter.ToInt32(data, index);
    index += intSize;
    int int32_4 = BitConverter.ToInt32(data, index);
    index += intSize;
    int y = int32_2;
    int width = int32_3;
    int height = int32_4;
    return new Rectangle(int32_1, y, width, height);
  }

  private Region ReadRegion(byte[] data, ref int index)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    index += MetafileParser.IntSize;
    int intSize = MetafileParser.IntSize;
    int int32_1 = BitConverter.ToInt32(data, index);
    index += intSize;
    int length = int32_1 / 2;
    CombineMode[] combineModeArray = new CombineMode[length];
    for (int index1 = 0; index1 < length; ++index1)
    {
      int int32_2 = BitConverter.ToInt32(data, index);
      index += intSize;
      CombineMode combineMode = (CombineMode) int32_2;
      combineModeArray[combineModeArray.Length - 1 - index1] = combineMode;
    }
    Region srcRegion = this.ReadRegion(data, ref index, intSize);
    if (index < data.Length && combineModeArray.Length != 0)
    {
      for (int index2 = 0; index2 < combineModeArray.Length && index < data.Length; ++index2)
      {
        CombineMode mode = combineModeArray[index2];
        Region dstRegion = this.ReadRegion(data, ref index, intSize);
        srcRegion = this.CombineRegion(srcRegion, dstRegion, mode);
      }
    }
    return srcRegion;
  }

  private Region ReadRegion(byte[] data, ref int index, int step)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    Region region1 = (Region) null;
    int int32 = BitConverter.ToInt32(data, index);
    index += MetafileParser.IntSize;
    switch (int32)
    {
      case 268435456 /*0x10000000*/:
        return new Region(this.ReadRectangle(data, ref index, step));
      case 268435457 /*0x10000001*/:
        GraphicsPath path = this.ReadRegionPath(data, ref index, step);
        if (path != null)
          region1 = new Region(path);
        return region1;
      case 268435458 /*0x10000002*/:
        Region region2 = new Region(RectangleF.Empty);
        region2.MakeEmpty();
        return region2;
      case 268435459 /*0x10000003*/:
        Region region3 = new Region();
        region3.MakeInfinite();
        return region3;
      default:
        return region1;
    }
  }

  private GraphicsPath ReadRegionPath(byte[] data, ref int index, int step)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    BitConverter.ToInt32(data, index);
    index += MetafileParser.IntSize;
    return this.ReadPath(data, ref index);
  }

  private float[] ReadSingleArray(byte[] data, ref int index, int step)
  {
    int length = data != null ? BitConverter.ToInt32(data, index) : throw new ArgumentNullException(nameof (data));
    index += MetafileParser.IntSize;
    float[] numArray = new float[length];
    for (int index1 = 0; index1 < length; ++index1)
    {
      float num = this.ReadNumber(data, index, step);
      index += step;
      numArray[index1] = num;
    }
    return numArray;
  }

  private StringFormat ReadStringFormat(byte[] data, ref int index)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    index += MetafileParser.IntSize;
    int intSize = MetafileParser.IntSize;
    int int32_1 = BitConverter.ToInt32(data, index);
    index += intSize;
    StringFormat stringFormat = new StringFormat((StringFormatFlags) int32_1);
    index += intSize;
    int int32_2 = BitConverter.ToInt32(data, index);
    index += intSize;
    StringAlignment stringAlignment1 = (StringAlignment) int32_2;
    stringFormat.Alignment = stringAlignment1;
    int int32_3 = BitConverter.ToInt32(data, index);
    index += intSize;
    StringAlignment stringAlignment2 = (StringAlignment) int32_3;
    stringFormat.LineAlignment = stringAlignment2;
    int int32_4 = BitConverter.ToInt32(data, index);
    index += intSize;
    StringDigitSubstitute substitute = (StringDigitSubstitute) int32_4;
    int language = BitConverter.ToInt32(data, index) & (int) ushort.MaxValue;
    index += intSize;
    stringFormat.SetDigitSubstitution(language, substitute);
    float single = BitConverter.ToSingle(data, index);
    index += MetafileParser.FloatSize;
    int int32_5 = BitConverter.ToInt32(data, index);
    index += intSize;
    HotkeyPrefix hotkeyPrefix = (HotkeyPrefix) int32_5;
    stringFormat.HotkeyPrefix = hotkeyPrefix;
    index += 12;
    int int32_6 = BitConverter.ToInt32(data, index);
    index += intSize;
    StringTrimming stringTrimming = (StringTrimming) int32_6;
    stringFormat.Trimming = stringTrimming;
    int int32_7 = BitConverter.ToInt32(data, index);
    index += intSize;
    int int32_8 = BitConverter.ToInt32(data, index);
    index += intSize;
    if (int32_7 > 0)
    {
      float[] tabStops = new float[int32_7];
      for (int index1 = 0; index1 < int32_7; ++index1)
      {
        tabStops[index1] = BitConverter.ToSingle(data, index);
        index += MetafileParser.FloatSize;
      }
      stringFormat.SetTabStops(single, tabStops);
    }
    if (int32_8 > 0)
    {
      CharacterRange[] ranges = new CharacterRange[int32_8];
      for (int index2 = 0; index2 < int32_8; ++index2)
      {
        int int32_9 = BitConverter.ToInt32(data, index);
        index += MetafileParser.IntSize;
        int int32_10 = BitConverter.ToInt32(data, index);
        index += MetafileParser.IntSize;
        ranges[index2] = new CharacterRange(int32_9, int32_10 - int32_9);
      }
      stringFormat.SetMeasurableCharacterRanges(ranges);
    }
    return stringFormat;
  }

  private Brush ReadTextureBrush(byte[] data, ref int index)
  {
    int int32 = BitConverter.ToInt32(data, index);
    index += MetafileParser.IntSize;
    int num = (int32 & 2) != 0 ? 1 : 0;
    WrapMode wrapMode = this.ReadWrapMode(data, ref index);
    Matrix matrix = (Matrix) null;
    if (num != 0)
      matrix = this.ReadMatrix(data, ref index, MetafileParser.FloatSize);
    TextureBrush textureBrush = new TextureBrush(this.ReadImage(data, ref index), wrapMode);
    if (num != 0)
      textureBrush.Transform = matrix;
    return (Brush) textureBrush;
  }

  private WrapMode ReadWrapMode(byte[] data, ref int index)
  {
    int int32 = BitConverter.ToInt32(data, index);
    index += MetafileParser.IntSize;
    return (WrapMode) int32;
  }

  private void ResetClip(byte[] data)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    this.Renderer.ResetClip();
  }

  private void ResetWorldTransform(byte[] data)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    this.Renderer.ResetTransform();
  }

  private void Restore(byte[] data)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    this.Renderer.Restore(this.Objects.GetState(BitConverter.ToInt32(data, 0)) as GraphicsState);
  }

  private void RotateWorldTransform(byte[] data, int flags)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    int index = 0;
    int floatSize = MetafileParser.FloatSize;
    this.Renderer.RotateTransform(this.ReadNumber(data, index, floatSize), this.GetMatrixOrder(flags));
  }

  private void Save(byte[] data)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    this.Objects.SetState(BitConverter.ToInt32(data, 0), (object) this.Renderer.Save());
  }

  private void ScaleWorldTransform(byte[] data, int flags)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    int index1 = 0;
    int floatSize = MetafileParser.FloatSize;
    float sx = this.ReadNumber(data, index1, floatSize);
    int index2 = index1 + floatSize;
    float sy = this.ReadNumber(data, index2, floatSize);
    int num = index2 + floatSize;
    MatrixOrder matrixOrder = this.GetMatrixOrder(flags);
    this.Renderer.ScaleTransform(sx, sy, matrixOrder);
  }

  private void SetAntiAliasMode(byte[] data, int flags)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
  }

  private void SetClipPath(byte[] data, int flags)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    CombineMode combineMode = this.GetCombineMode(flags);
    if (!(this.Objects.GetObject(this.GetIndex(flags)) is GraphicsPath path))
      return;
    this.Renderer.SetClip(path, combineMode);
  }

  private void SetClipRect(byte[] data, int flags)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    int index = 0;
    int floatSize = MetafileParser.FloatSize;
    this.Renderer.SetClip(this.ReadRectangle(data, ref index, floatSize), this.GetCombineMode(flags));
  }

  private void SetClipRegion(byte[] data, int flags)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    CombineMode combineMode = this.GetCombineMode(flags);
    if (!(this.Objects.GetObject(this.GetIndex(flags)) is Region region))
      return;
    this.Renderer.SetClip(region, combineMode);
  }

  private void SetComposingMode(byte[] data, int flags)
  {
  }

  private void SetCompositingQuality(byte[] data, int flags)
  {
  }

  private void SetInterpolationMode(byte[] data, int flags)
  {
  }

  private void SetPageTransform(byte[] data, int flags)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    GraphicsUnit graphicsUnit = (GraphicsUnit) flags;
    float single = BitConverter.ToSingle(data, 0);
    if (graphicsUnit == GraphicsUnit.World)
      return;
    this.Renderer.PageUnit = graphicsUnit == GraphicsUnit.Point ? graphicsUnit : GraphicsUnit.Pixel;
    this.Renderer.PageScale = single;
  }

  private void SetPixelOffsetMode(byte[] data, int flags)
  {
  }

  private void SetRenderingOrigin(byte[] data)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    int startIndex1 = 0;
    int intSize = MetafileParser.IntSize;
    int int32_1 = BitConverter.ToInt32(data, startIndex1);
    int startIndex2 = startIndex1 + intSize;
    int int32_2 = BitConverter.ToInt32(data, startIndex2);
    this.Renderer.SetRenderingOrigin(new Point(int32_1, int32_2));
  }

  private void SetTextContrast(int flags)
  {
  }

  private void SetTextRenderingHint(int flags)
  {
    this.Renderer.NativeGraphics.TextRenderingHint = (TextRenderingHint) flags;
  }

  private void SetWorldTransform(byte[] data)
  {
    int index = 0;
    int floatSize = MetafileParser.FloatSize;
    this.Renderer.Transform = this.ReadMatrix(data, ref index, floatSize);
  }

  private void TranslateWorldTransform(byte[] data, int flags)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    int index1 = 0;
    int floatSize = MetafileParser.FloatSize;
    float dx = this.ReadNumber(data, index1, floatSize);
    int index2 = index1 + floatSize;
    float dy = this.ReadNumber(data, index2, floatSize);
    int num = index2 + floatSize;
    MatrixOrder matrixOrder = this.GetMatrixOrder(flags);
    this.Renderer.TranslateTransform(dx, dy, matrixOrder);
  }

  private ObjectData Objects => this.m_objects;

  public override MetafileType Type => this.m_type;
}
