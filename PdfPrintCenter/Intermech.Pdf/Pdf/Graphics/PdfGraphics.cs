// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfGraphics
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.ColorSpace;
using Syncfusion.Pdf.Graphics.Fonts;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Primitives;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

#nullable disable
namespace Syncfusion.Pdf.Graphics;

public sealed class PdfGraphics
{
  private PdfAutomaticFieldInfoCollection m_automaticFields;
  private bool m_bCSInitialized;
  internal bool m_bStateSaved;
  private SizeF m_canvasSize;
  private bool m_CIEColors;
  internal RectangleF m_clipBounds;
  private PdfBrush m_currentBrush;
  private PdfColorSpace m_currentColorSpace;
  private PdfFont m_currentFont;
  private PdfPen m_currentPen;
  private PdfStringFormat m_currentStringFormat;
  internal float m_DpiY;
  private PdfGraphics.GetResources m_getResources;
  private Stack<PdfGraphicsState> m_graphicsState;
  internal bool m_isBaselineFormat;
  internal bool m_isEMF;
  internal bool m_isEMFPlus;
  internal bool m_isUseFontSize;
  private PdfPageLayer m_layer;
  private PdfTransformationMatrix m_matrix;
  private float m_previousCharacterSpacing;
  private TextRenderingMode m_previousTextRenderingMode;
  private float m_previousTextScaling;
  private float m_previousWordSpacing;
  private float m_split;
  private PdfStreamWriter m_streamWriter;
  private PdfStringLayoutResult m_stringLayoutResult;
  private static bool m_transparencyObject = false;
  private Dictionary<PdfGraphics.TransparencyData, PdfTransparency> m_trasparencies;
  private const int PathTypesValuesMask = 15;
  private static object s_transparencyLock = new object();

  internal PdfGraphics(SizeF size, PdfGraphics.GetResources resources, PdfStreamWriter writer)
  {
    this.m_isBaselineFormat = true;
    this.m_previousTextScaling = 100f;
    if (writer == null)
      throw new ArgumentNullException(nameof (writer));
    if (resources == null)
      throw new ArgumentNullException(nameof (resources));
    this.m_streamWriter = writer;
    this.m_getResources = resources;
    this.m_canvasSize = size;
    this.Initialize();
  }

  internal PdfGraphics(SizeF size, PdfGraphics.GetResources resources, PdfStream stream)
    : this(size, resources, new PdfStreamWriter(stream))
  {
  }

  private void ApplyStringSettings(
    PdfFont font,
    PdfPen pen,
    PdfBrush brush,
    PdfStringFormat format,
    RectangleF bounds)
  {
    if (font == null)
      throw new ArgumentNullException(nameof (font));
    switch (brush)
    {
      case PdfTilingBrush _:
        this.m_bCSInitialized = false;
        (brush as PdfTilingBrush).Graphics.ColorSpace = this.ColorSpace;
        break;
      case PdfGradientBrush _:
        this.m_bCSInitialized = false;
        (brush as PdfGradientBrush).ColorSpace = this.ColorSpace;
        break;
    }
    bool flag = false;
    TextRenderingMode renderingMode = this.GetTextRenderingMode(pen, brush, format);
    if (font.Name.Equals("Arial Unicode MS") && font.Bold && (font as PdfTrueTypeFont).Unicode)
    {
      if (pen == null && brush != null)
        pen = new PdfPen(brush);
      renderingMode = TextRenderingMode.FillStroke;
      flag = true;
    }
    this.StateControl(pen, brush, font, format);
    this.m_streamWriter.BeginText();
    if (this.Layer != null && this.Page != null && this.Page is PdfPage)
    {
      PdfSection section = (this.Page as PdfPage).Section;
      if (section.ParentDocument is PdfDocument && section.ParentDocument.FileStructure.TaggedPdf && PdfCrossTable.Dereference(section.ParentDocument.Catalog["StructTreeRoot"]) is PdfStructTreeRoot pdfStructTreeRoot)
        this.StreamWriter.WriteTag($"/{"P"} <</MCID {pdfStructTreeRoot.Add("P", "", this.Page, bounds)} >>BDC");
    }
    else
    {
      PdfStructTreeRoot structTreeRoot = PdfCatalog.StructTreeRoot;
      if (structTreeRoot != null)
        this.StreamWriter.WriteTag($"/{"P"} <</MCID {structTreeRoot.Add("P", "", bounds)} >>BDC");
    }
    if (flag)
      this.m_streamWriter.SetLineWidth(font.Size / 30f);
    if (renderingMode != this.m_previousTextRenderingMode)
    {
      this.m_streamWriter.SetTextRenderingMode(renderingMode);
      this.m_previousTextRenderingMode = renderingMode;
    }
    float charSpacing = format != null ? format.CharacterSpacing : 0.0f;
    if ((double) charSpacing != (double) this.m_previousCharacterSpacing)
    {
      this.m_streamWriter.SetCharacterSpacing(charSpacing);
      this.m_previousCharacterSpacing = charSpacing;
    }
    float wordSpacing = format != null ? format.WordSpacing : 0.0f;
    if ((double) wordSpacing == (double) this.m_previousWordSpacing)
      return;
    this.m_streamWriter.SetWordSpacing(wordSpacing);
    this.m_previousWordSpacing = wordSpacing;
  }

  private string[] BreakUnicodeLine(string line, PdfTrueTypeFont ttfFont, out string[] words)
  {
    if (line == null)
      throw new ArgumentNullException(nameof (line));
    if (ttfFont == null)
      throw new ArgumentNullException(nameof (ttfFont));
    words = line.Split((char[]) null);
    string[] strArray = new string[words.Length];
    int index = 0;
    for (int length = words.Length; index < length; ++index)
    {
      string text = words[index];
      strArray[index] = this.ConvertToUnicode(text, ttfFont);
    }
    return strArray;
  }

  private void BrushControl(PdfBrush brush, bool saveState)
  {
    if (brush == null)
      return;
    bool flag1 = false;
    bool flag2 = false;
    PdfBrush pdfBrush = brush.Clone();
    if (pdfBrush is PdfGradientBrush pdfGradientBrush)
    {
      PdfTransformationMatrix matrix1 = pdfGradientBrush.Matrix;
      PdfTransformationMatrix matrix2 = this.Matrix.Clone();
      if (matrix1 != null)
      {
        matrix1.Multiply(matrix2);
        matrix2 = matrix1;
      }
      pdfGradientBrush.Matrix = matrix2;
    }
    if (!(brush is PdfSolidBrush pdfSolidBrush))
    {
      if (pdfBrush.MonitorChanges(this.m_currentBrush, this.m_streamWriter, this.m_getResources, saveState, this.ColorSpace))
        this.m_currentBrush = pdfBrush;
    }
    else if (pdfSolidBrush.Colorspaces == null)
    {
      if (pdfBrush.MonitorChanges(this.m_currentBrush, this.m_streamWriter, this.m_getResources, saveState, this.ColorSpace))
        this.m_currentBrush = pdfBrush;
    }
    else
    {
      if (pdfSolidBrush.Colorspaces is PdfCalRGBColor)
        this.ColorSpace = PdfColorSpace.RGB;
      else if (pdfSolidBrush.Colorspaces is PdfCalGrayColor)
        this.ColorSpace = PdfColorSpace.GrayScale;
      else if (pdfSolidBrush.Colorspaces is PdfICCColor)
      {
        flag1 = true;
        PdfICCColor colorspaces = pdfSolidBrush.Colorspaces as PdfICCColor;
        if (colorspaces.ColorSpaces.AlternateColorSpace != null)
        {
          if (colorspaces.ColorSpaces.AlternateColorSpace is PdfCalGrayColorSpace)
            this.ColorSpace = PdfColorSpace.GrayScale;
          else if (colorspaces.ColorSpaces.AlternateColorSpace is PdfCalRGBColorSpace)
            this.ColorSpace = PdfColorSpace.RGB;
          else if (colorspaces.ColorSpaces.AlternateColorSpace is PdfLabColorSpace)
            this.ColorSpace = PdfColorSpace.RGB;
          else if (colorspaces.ColorSpaces.AlternateColorSpace is PdfDeviceColorSpace)
          {
            switch ((colorspaces.ColorSpaces.AlternateColorSpace as PdfDeviceColorSpace).DeviceColorSpaceType.ToString())
            {
              case "RGB":
                this.ColorSpace = PdfColorSpace.RGB;
                break;
              case "GrayScale":
                this.ColorSpace = PdfColorSpace.GrayScale;
                break;
              case "CMYK":
                this.ColorSpace = PdfColorSpace.CMYK;
                break;
            }
          }
        }
        else
          this.ColorSpace = PdfColorSpace.RGB;
      }
      else if (pdfSolidBrush.Colorspaces is PdfSeparationColor)
      {
        flag1 = true;
        this.ColorSpace = PdfColorSpace.GrayScale;
      }
      else if (pdfSolidBrush.Colorspaces is PdfIndexedColor)
      {
        flag2 = true;
        this.ColorSpace = PdfColorSpace.GrayScale;
      }
      if (!flag1 ? (!flag2 ? pdfBrush.MonitorChanges(this.m_currentBrush, this.m_streamWriter, this.m_getResources, saveState, this.ColorSpace, true) : pdfBrush.MonitorChanges(this.m_currentBrush, this.m_streamWriter, this.m_getResources, saveState, this.ColorSpace, true, true, true)) : pdfBrush.MonitorChanges(this.m_currentBrush, this.m_streamWriter, this.m_getResources, saveState, this.ColorSpace, true, true))
        this.m_currentBrush = pdfBrush;
    }
    brush = (PdfBrush) null;
  }

  private void BuildUpPath(PdfPath path) => this.BuildUpPath(path.PathPoints, path.PathTypes);

  private void BuildUpPath(PointF[] points, byte[] types)
  {
    int i = 0;
    for (int length = points.Length; i < length; ++i)
    {
      byte type = types[i];
      PointF point = points[i];
      switch ((int) type & 15)
      {
        case 0:
          this.m_streamWriter.BeginPath(point);
          break;
        case 1:
          this.m_streamWriter.AppendLineSegment(point);
          break;
        case 3:
          PointF p2;
          PointF p3;
          this.GetBezierPoints(points, types, ref i, out p2, out p3);
          this.m_streamWriter.AppendBezierSegment(point, p2, p3);
          break;
        default:
          throw new ArithmeticException("Incorrect path formation.");
      }
      this.CheckFlags(types[i]);
    }
  }

  private void CapControl(PdfPen pen, float x2, float y2, float x1, float y1)
  {
  }

  private bool CheckCorrectLayoutRectangle(ref RectangleF layoutRectangle) => true;

  internal RectangleF CheckCorrectLayoutRectangle(
    SizeF textSize,
    float x,
    float y,
    PdfStringFormat format)
  {
    RectangleF rectangleF = new RectangleF(x, y, textSize.Width, textSize.Width);
    if (format != null)
    {
      switch (format.Alignment)
      {
        case PdfTextAlignment.Center:
          rectangleF.X -= rectangleF.Width / 2f;
          break;
        case PdfTextAlignment.Right:
          rectangleF.X -= rectangleF.Width;
          break;
      }
      switch (format.LineAlignment)
      {
        case PdfVerticalAlignment.Middle:
          rectangleF.Y -= rectangleF.Height / 2f;
          return rectangleF;
        case PdfVerticalAlignment.Bottom:
          rectangleF.Y -= rectangleF.Height;
          return rectangleF;
      }
    }
    return rectangleF;
  }

  private void CheckFlags(byte type)
  {
    if (((int) type & 128 /*0x80*/) != 128 /*0x80*/)
      return;
    this.m_streamWriter.ClosePath();
  }

  internal void ClipTranslateMargins(RectangleF clipBounds)
  {
    this.m_clipBounds = clipBounds;
    this.m_streamWriter.WriteComment("Clip margins.");
    this.m_streamWriter.AppendRectangle(clipBounds);
    this.m_streamWriter.ClosePath();
    this.m_streamWriter.ClipPath(false);
    this.m_streamWriter.WriteComment("Translate co-ordinate system.");
    this.TranslateTransform(clipBounds.X, clipBounds.Y);
  }

  internal void ClipTranslateMargins(
    float x,
    float y,
    float left,
    float top,
    float right,
    float bottom)
  {
    RectangleF rect;
    ref RectangleF local = ref rect;
    double x1 = (double) left;
    double y1 = (double) top;
    SizeF size = this.Size;
    double width = (double) size.Width - (double) left - (double) right;
    size = this.Size;
    double height = (double) size.Height - (double) top - (double) bottom;
    local = new RectangleF((float) x1, (float) y1, (float) width, (float) height);
    this.m_clipBounds = rect;
    this.m_streamWriter.WriteComment("Clip margins.");
    this.m_streamWriter.AppendRectangle(rect);
    this.m_streamWriter.ClosePath();
    this.m_streamWriter.ClipPath(false);
    this.m_streamWriter.WriteComment("Translate co-ordinate system.");
    this.TranslateTransform(x, y);
  }

  private void ColorSpaceControl(PdfColorSpaces colorspace)
  {
    if (colorspace == null)
      return;
    PdfName name = this.m_getResources().GetName((IPdfWrapper) colorspace);
    this.m_streamWriter.SetColorSpace(colorspace, name);
  }

  private void ConstructArcPath(
    float x1,
    float y1,
    float x2,
    float y2,
    float startAng,
    float sweepAngle)
  {
    List<float[]> bezierArcPoints = PdfGraphics.GetBezierArcPoints(x1, y1, x2, y2, startAng, sweepAngle);
    if (bezierArcPoints.Count == 0)
      return;
    float[] numArray1 = bezierArcPoints[0];
    this.m_streamWriter.BeginPath(numArray1[0], numArray1[1]);
    for (int index = 0; index < bezierArcPoints.Count; ++index)
    {
      float[] numArray2 = bezierArcPoints[index];
      this.m_streamWriter.AppendBezierSegment(numArray2[2], numArray2[3], numArray2[4], numArray2[5], numArray2[6], numArray2[7]);
    }
  }

  private string ConvertToUnicode(string text, PdfTrueTypeFont ttfFont)
  {
    string unicode = (string) null;
    if (text == null)
      throw new ArgumentNullException(nameof (text));
    if (ttfFont == null)
      throw new ArgumentNullException(nameof (ttfFont));
    if (ttfFont.InternalFont is UnicodeTrueTypeFont)
    {
      TtfReader ttfReader = (ttfFont.InternalFont as UnicodeTrueTypeFont).TtfReader;
      ttfFont.SetSymbols(text);
      string text1 = text;
      return PdfString.ByteToString(PdfString.ToUnicodeArray(ttfReader.ConvertString(text1), false));
    }
    if (ttfFont.InternalFont is TrueTypeFont)
    {
      TtfReader ttfReader = (ttfFont.InternalFont as TrueTypeFont).TtfReader;
      ttfFont.SetSymbols(text);
      string text2 = text;
      unicode = PdfString.ByteToString(PdfString.ToUnicodeArray(ttfReader.ConvertString(text2), false));
    }
    return unicode;
  }

  private PdfPen CreateUnderlineStikeoutPen(
    PdfPen pen,
    PdfBrush brush,
    PdfFont font,
    PdfStringFormat format)
  {
    if (font == null)
      throw new ArgumentNullException(nameof (font));
    float width = font.Metrics.GetSize(format) / 20f;
    PdfPen underlineStikeoutPen = (PdfPen) null;
    if (pen != null)
      return new PdfPen(pen.Color, width);
    if (brush != null)
      underlineStikeoutPen = new PdfPen(brush, width);
    return underlineStikeoutPen;
  }

  private PdfGraphicsState DoRestoreState()
  {
    PdfGraphicsState pdfGraphicsState = this.m_graphicsState.Pop();
    this.m_matrix = pdfGraphicsState.Matrix;
    this.m_currentBrush = pdfGraphicsState.Brush;
    this.m_currentPen = pdfGraphicsState.Pen;
    this.m_currentFont = pdfGraphicsState.Font;
    this.m_currentColorSpace = pdfGraphicsState.ColorSpace;
    this.m_previousCharacterSpacing = pdfGraphicsState.CharacterSpacing;
    this.m_previousWordSpacing = pdfGraphicsState.WordSpacing;
    this.m_previousTextScaling = pdfGraphicsState.TextScaling;
    this.m_previousTextRenderingMode = pdfGraphicsState.TextRenderingMode;
    this.m_streamWriter.RestoreGraphicsState();
    return pdfGraphicsState;
  }

  public void DrawArc(PdfPen pen, RectangleF rectangle, float startAngle, float sweepAngle)
  {
    GraphicsPath graphicsPath = new GraphicsPath();
    graphicsPath.AddArc(rectangle, startAngle, sweepAngle);
    PointF[] pathPoints = graphicsPath.PathPoints;
    byte[] pathTypes = graphicsPath.PathTypes;
    PdfPath path = new PdfPath(pen, pathPoints, pathTypes);
    this.DrawPath(pen, path);
    graphicsPath.Dispose();
  }

  public void DrawArc(
    PdfPen pen,
    float x,
    float y,
    float width,
    float height,
    float startAngle,
    float sweepAngle)
  {
    if ((double) sweepAngle == 0.0)
      return;
    this.StateControl(pen, (PdfBrush) null, (PdfFont) null);
    this.ConstructArcPath(x, y, x + width, y + height, startAngle, sweepAngle);
    this.DrawPath(pen, (PdfBrush) null, false);
  }

  private void DrawAsciiLine(
    LineInfo lineInfo,
    RectangleF layoutRectangle,
    PdfFont font,
    PdfStringFormat format)
  {
    if (font == null)
      throw new ArgumentNullException(nameof (font));
    double num = (double) this.JustifyLine(lineInfo, layoutRectangle.Width, format);
    this.m_streamWriter.ShowNextLineText(this.GetAsciiString(lineInfo.Text));
  }

  private void DrawAsciiLine(
    LineInfo lineInfo,
    RectangleF layoutRectangle,
    PdfFont font,
    PdfStringFormat format,
    bool embed)
  {
    if (font == null)
      throw new ArgumentNullException(nameof (font));
    double num = (double) this.JustifyLine(lineInfo, layoutRectangle.Width, format);
    string text = lineInfo.Text;
    this.m_streamWriter.ShowNextLineText(this.GetAsciiString(text));
    PdfTrueTypeFont ttfFont = font as PdfTrueTypeFont;
    this.ConvertToUnicode(text, ttfFont);
  }

  public void DrawBezier(
    PdfPen pen,
    PointF startPoint,
    PointF firstControlPoint,
    PointF secondControlPoint,
    PointF endPoint)
  {
    this.DrawBezier(pen, startPoint.X, startPoint.Y, firstControlPoint.X, firstControlPoint.Y, secondControlPoint.X, secondControlPoint.Y, endPoint.X, endPoint.Y);
  }

  public void DrawBezier(
    PdfPen pen,
    float startPointX,
    float startPointY,
    float firstControlPointX,
    float firstControlPointY,
    float secondControlPointX,
    float secondControlPointY,
    float endPointX,
    float endPointY)
  {
    this.StateControl(pen, (PdfBrush) null, (PdfFont) null);
    this.CapControl(pen, secondControlPointX, secondControlPointY, endPointX, endPointY);
    this.CapControl(pen, firstControlPointX, firstControlPointY, secondControlPointX, startPointY);
    PdfStreamWriter streamWriter = this.StreamWriter;
    streamWriter.BeginPath(startPointX, startPointY);
    streamWriter.AppendBezierSegment(firstControlPointX, firstControlPointY, secondControlPointX, secondControlPointY, endPointX, endPointY);
    streamWriter.StrokePath();
  }

  private void DrawCjkString(
    LineInfo lineInfo,
    RectangleF layoutRectangle,
    PdfFont font,
    PdfStringFormat format)
  {
    if (font == null)
      throw new ArgumentNullException(nameof (font));
    double num = (double) this.JustifyLine(lineInfo, layoutRectangle.Width, format);
    this.m_streamWriter.ShowNextLineText(this.GetCjkString(lineInfo.Text), false);
  }

  public void DrawEllipse(PdfBrush brush, RectangleF rectangle)
  {
    this.DrawEllipse((PdfPen) null, brush, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
  }

  public void DrawEllipse(PdfPen pen, RectangleF rectangle)
  {
    this.DrawEllipse(pen, (PdfBrush) null, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
  }

  public void DrawEllipse(PdfPen pen, PdfBrush brush, RectangleF rectangle)
  {
    this.DrawEllipse(pen, brush, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
  }

  public void DrawEllipse(PdfBrush brush, float x, float y, float width, float height)
  {
    this.DrawEllipse((PdfPen) null, brush, x, y, width, height);
  }

  public void DrawEllipse(PdfPen pen, float x, float y, float width, float height)
  {
    this.DrawEllipse(pen, (PdfBrush) null, x, y, width, height);
  }

  public void DrawEllipse(
    PdfPen pen,
    PdfBrush brush,
    float x,
    float y,
    float width,
    float height)
  {
    if (brush is PdfTilingBrush)
    {
      this.m_bCSInitialized = true;
      float x1 = this.m_matrix.OffsetX + x;
      float y1 = this.Layer == null || this.Layer.Page == null ? this.ClientSize.Height - this.m_matrix.OffsetY + y : this.Layer.Page.Size.Height - this.m_matrix.OffsetY + y;
      (brush as PdfTilingBrush).Location = new PointF(x1, y1);
      (brush as PdfTilingBrush).Graphics.ColorSpace = this.ColorSpace;
    }
    this.StateControl(pen, brush, (PdfFont) null);
    this.ConstructArcPath(x, y, x + width, y + height, 0.0f, 360f);
    this.DrawPath(pen, brush, true);
  }

  public void DrawImage(PdfImage image, PointF point)
  {
    if (image == null)
      throw new ArgumentNullException(nameof (image));
    this.DrawImage(image, point.X, point.Y);
  }

  public void DrawImage(PdfImage image, RectangleF rectangle)
  {
    if (image == null)
      throw new ArgumentNullException(nameof (image));
    this.DrawImage(image, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
  }

  public void DrawImage(PdfImage image, PointF point, SizeF size)
  {
    if (image == null)
      throw new ArgumentNullException(nameof (image));
    this.DrawImage(image, point.X, point.Y, size.Width, size.Height);
  }

  public void DrawImage(PdfImage image, float x, float y)
  {
    SizeF sizeF = image != null ? image.PhysicalDimension : throw new ArgumentNullException(nameof (image));
    this.DrawImage(image, x, y, sizeF.Width, sizeF.Height);
  }

  public void DrawImage(PdfImage image, float x, float y, float width, float height)
  {
    bool flag = false;
    if (image == null)
      throw new ArgumentNullException(nameof (image));
    SizeF clientSize = this.ClientSize;
    if ((double) clientSize.Height < 0.0)
    {
      double num = (double) y;
      clientSize = this.ClientSize;
      double height1 = (double) clientSize.Height;
      y = (float) (num + height1);
    }
    image.Save();
    if (this.Layer != null && this.Page != null && this.Page is PdfPage)
    {
      PdfSection section = (this.Page as PdfPage).Section;
      if (section.ParentDocument is PdfDocument && section.ParentDocument.FileStructure.TaggedPdf)
      {
        flag = true;
        if (PdfCrossTable.Dereference(section.ParentDocument.Catalog["StructTreeRoot"]) is PdfStructTreeRoot pdfStructTreeRoot)
          this.m_streamWriter.WriteTag($"/{"Figure"} <</MCID {pdfStructTreeRoot.Add("Figure", "Image", this.Page, RectangleF.Empty)} >>BDC");
      }
    }
    else
    {
      PdfStructTreeRoot structTreeRoot = PdfCatalog.StructTreeRoot;
      if (structTreeRoot != null)
      {
        flag = true;
        this.StreamWriter.WriteTag($"/{"Figure"} <</MCID {structTreeRoot.Add("Figure", "Image", RectangleF.Empty)} >>BDC");
      }
    }
    PdfGraphicsState state = this.Save();
    PdfTransformationMatrix transformationMatrix = new PdfTransformationMatrix();
    this.GetTranslateTransform(x, y + height, transformationMatrix);
    if (image.InternalImage is Metafile)
      this.GetScaleTransform(width / (float) image.Width, height / (float) image.Height, transformationMatrix);
    else
      this.GetScaleTransform(width, height, transformationMatrix);
    this.m_streamWriter.ModifyCTM(transformationMatrix);
    this.m_streamWriter.ExecuteObject(this.m_getResources().GetName((IPdfWrapper) image));
    lock (PdfGraphics.s_transparencyLock)
    {
      if (image.SoftMask)
        PdfGraphics.m_transparencyObject = true;
      if (PdfGraphics.m_transparencyObject)
      {
        if (this.Layer != null)
        {
          if (!this.Page.Dictionary.ContainsKey("Group"))
            this.SetTransparencyGroup(this.Page);
        }
      }
    }
    this.Restore(state);
    if (flag)
      this.m_streamWriter.WriteTag("EMC");
    this.m_getResources().RequireProcSet("ImageB");
    this.m_getResources().RequireProcSet("ImageC");
    this.m_getResources().RequireProcSet("ImageI");
    this.m_getResources().RequireProcSet("Text");
  }

  private void DrawLayoutResult(
    PdfStringLayoutResult result,
    PdfFont font,
    PdfStringFormat format,
    RectangleF layoutRectangle)
  {
    if (result == null)
      throw new ArgumentNullException(nameof (result));
    if (font == null)
      throw new ArgumentNullException(nameof (font));
    LineInfo[] lines = result.Lines;
    bool flag = font is PdfTrueTypeFont pdfTrueTypeFont && pdfTrueTypeFont.Unicode;
    bool embed = pdfTrueTypeFont != null && pdfTrueTypeFont.Embed;
    int index = 0;
    for (int length = lines.Length; index < length; ++index)
    {
      LineInfo lineInfo = lines[index];
      string text = lineInfo.Text;
      float width = lineInfo.Width;
      if (text == null || text.Length == 0)
      {
        this.m_streamWriter.StartNextLine();
      }
      else
      {
        float horizontalAlignShift = this.GetHorizontalAlignShift(width, layoutRectangle.Width, format);
        float lineIndent = this.GetLineIndent(lineInfo, format, layoutRectangle, index == 0);
        float x = horizontalAlignShift + (!this.RightToLeft(format) ? lineIndent : 0.0f);
        if ((double) x != 0.0)
          this.m_streamWriter.StartNextLine(x, 0.0f);
        if (font is PdfCjkStandardFont)
          this.DrawCjkString(lineInfo, layoutRectangle, font, format);
        else if (flag)
          this.DrawUnicodeLine(lineInfo, layoutRectangle, font, format);
        else if (embed)
          this.DrawAsciiLine(lineInfo, layoutRectangle, font, format, embed);
        else
          this.DrawAsciiLine(lineInfo, layoutRectangle, font, format);
        if ((double) x != 0.0)
          this.m_streamWriter.StartNextLine(-x, 0.0f);
      }
    }
    this.m_getResources().RequireProcSet("Text");
  }

  public void DrawLine(PdfPen pen, PointF point1, PointF point2)
  {
    this.DrawLine(pen, point1.X, point1.Y, point2.X, point2.Y);
  }

  public void DrawLine(PdfPen pen, float x1, float y1, float x2, float y2)
  {
    this.StateControl(pen, (PdfBrush) null, (PdfFont) null);
    this.CapControl(pen, x1, y1, x2, y2);
    this.CapControl(pen, x2, y2, x1, y1);
    PdfStreamWriter streamWriter = this.StreamWriter;
    streamWriter.BeginPath(x1, y1);
    streamWriter.AppendLineSegment(x2, y2);
    streamWriter.StrokePath();
    this.m_getResources().RequireProcSet("PDF");
  }

  public void DrawPath(PdfBrush brush, PdfPath path) => this.DrawPath((PdfPen) null, brush, path);

  public void DrawPath(PdfPen pen, PdfPath path) => this.DrawPath(pen, (PdfBrush) null, path);

  public void DrawPath(PdfPen pen, PdfBrush brush, PdfPath path)
  {
    switch (brush)
    {
      case PdfTilingBrush _:
        this.m_bCSInitialized = false;
        (brush as PdfTilingBrush).Graphics.ColorSpace = this.ColorSpace;
        break;
      case PdfGradientBrush _:
        this.m_bCSInitialized = false;
        (brush as PdfGradientBrush).ColorSpace = this.ColorSpace;
        break;
    }
    this.StateControl(pen, brush, (PdfFont) null);
    this.BuildUpPath(path);
    this.DrawPath(pen, brush, path.FillMode, false);
  }

  private void DrawPath(PdfPen pen, PdfBrush brush, bool needClosing)
  {
    this.DrawPath(pen, brush, PdfFillMode.Winding, needClosing);
  }

  private void DrawPath(PdfPen pen, PdfBrush brush, PdfFillMode fillMode, bool needClosing)
  {
    bool flag1 = pen != null;
    bool flag2 = brush != null;
    bool useEvenOddRule = fillMode == PdfFillMode.Alternate;
    if (flag1 & flag2)
    {
      if (needClosing)
        this.StreamWriter.CloseFillStrokePath(useEvenOddRule);
      else
        this.StreamWriter.FillStrokePath(useEvenOddRule);
    }
    else if (!flag1 && !flag2)
      this.StreamWriter.EndPath();
    else if (flag1)
    {
      if (needClosing)
        this.StreamWriter.CloseStrokePath();
      else
        this.StreamWriter.StrokePath();
    }
    else
    {
      if (!flag2)
        throw new PdfException("Internal CLR error.");
      if (needClosing)
        this.StreamWriter.CloseFillPath(useEvenOddRule);
      else
        this.StreamWriter.FillPath(useEvenOddRule);
    }
  }

  public void DrawPdfTemplate(PdfTemplate template, PointF location)
  {
    if (template == null)
      throw new ArgumentNullException(nameof (template));
    this.DrawPdfTemplate(template, location, template.Size);
  }

  public void DrawPdfTemplate(PdfTemplate template, PointF location, SizeF size)
  {
    PdfCrossTable crossTable = (PdfCrossTable) null;
    if (this.m_layer != null)
    {
      bool flag = false;
      if (this.Page is PdfLoadedPage)
      {
        crossTable = (this.Page as PdfLoadedPage).Document.CrossTable;
        flag = (this.Page as PdfLoadedPage).Document.EnableMemoryOptimization;
      }
      else if (this.Page is PdfPage)
      {
        crossTable = (this.Page as PdfPage).Section.ParentDocument.CrossTable;
        flag = (this.Page as PdfPage).Section.ParentDocument.EnableMemoryOptimization;
      }
      if (template.ReadOnly & flag)
        template.CloneResources(crossTable);
    }
    if (template == null)
      throw new ArgumentNullException(nameof (template));
    float x1 = (double) template.Width > 0.0 ? size.Width / template.Width : 1f;
    float y1 = (double) template.Height > 0.0 ? size.Height / template.Height : 1f;
    int num1 = (double) x1 != 1.0 ? 1 : ((double) y1 != 1.0 ? 1 : 0);
    if (this.m_layer != null && this.Page.Dictionary.ContainsKey("CropBox") && this.Page.Dictionary.ContainsKey("MediaBox"))
    {
      PdfArray pdfArray1 = (object) (this.Page.Dictionary["CropBox"] as PdfReferenceHolder) == null ? this.Page.Dictionary["CropBox"] as PdfArray : (this.Page.Dictionary["CropBox"] as PdfReferenceHolder).Object as PdfArray;
      PdfArray pdfArray2 = (object) (this.Page.Dictionary["MediaBox"] as PdfReferenceHolder) == null ? this.Page.Dictionary["MediaBox"] as PdfArray : (this.Page.Dictionary["MediaBox"] as PdfReferenceHolder).Object as PdfArray;
      float floatValue1 = (pdfArray2[0] as PdfNumber).FloatValue;
      float floatValue2 = (pdfArray2[1] as PdfNumber).FloatValue;
      float floatValue3 = (pdfArray1[0] as PdfNumber).FloatValue;
      float floatValue4 = (pdfArray1[3] as PdfNumber).FloatValue;
      if ((double) floatValue3 > 0.0 && (double) floatValue4 > 0.0 && (double) floatValue1 < 0.0 && (double) floatValue2 < 0.0)
      {
        this.TranslateTransform(floatValue3, -floatValue4);
        location.X = -floatValue3;
        location.Y = floatValue4;
      }
    }
    PdfGraphicsState state = this.Save();
    PdfTransformationMatrix transformationMatrix = new PdfTransformationMatrix();
    PointF pointF;
    if (this.m_layer != null)
    {
      bool flag = false;
      if (this.Page.Dictionary.ContainsKey("CropBox") && this.Page.Dictionary.ContainsKey("MediaBox"))
      {
        PdfArray pdfArray3 = (object) (this.Page.Dictionary["CropBox"] as PdfReferenceHolder) == null ? this.Page.Dictionary["CropBox"] as PdfArray : (this.Page.Dictionary["CropBox"] as PdfReferenceHolder).Object as PdfArray;
        PdfArray pdfArray4 = (object) (this.Page.Dictionary["MediaBox"] as PdfReferenceHolder) == null ? this.Page.Dictionary["MediaBox"] as PdfArray : (this.Page.Dictionary["MediaBox"] as PdfReferenceHolder).Object as PdfArray;
        if (pdfArray3 != null && pdfArray4 != null && pdfArray3.ToRectangle() == pdfArray4.ToRectangle())
          flag = true;
      }
      if (this.Page.Dictionary.ContainsKey("MediaBox"))
      {
        PdfArray pdfArray = (object) (this.Page.Dictionary["MediaBox"] as PdfReferenceHolder) == null ? this.Page.Dictionary["MediaBox"] as PdfArray : (this.Page.Dictionary["MediaBox"] as PdfReferenceHolder).Object as PdfArray;
        if (pdfArray != null && (double) (pdfArray[3] as PdfNumber).FloatValue == 0.0)
          flag = true;
      }
      pointF = this.Page.Origin;
      int num2;
      if ((double) pointF.X >= 0.0)
      {
        pointF = this.Page.Origin;
        num2 = (double) pointF.Y >= 0.0 ? 1 : 0;
      }
      else
        num2 = 0;
      int num3 = flag ? 1 : 0;
      if ((num2 | num3) != 0)
        this.GetTranslateTransform(location.X, location.Y + size.Height, transformationMatrix);
      else
        this.GetTranslateTransform(location.X, location.Y + 0.0f, transformationMatrix);
    }
    else
      this.GetTranslateTransform(location.X, location.Y + size.Height, transformationMatrix);
    if (num1 != 0)
      this.GetScaleTransform(x1, y1, transformationMatrix);
    this.m_streamWriter.ModifyCTM(transformationMatrix);
    this.m_streamWriter.ExecuteObject(this.m_getResources().GetName((IPdfWrapper) template));
    this.Restore(state);
    PdfGraphics graphics = template.Graphics;
    if (graphics != null)
    {
      foreach (PdfAutomaticFieldInfo automaticField in (PdfCollection) graphics.AutomaticFields)
      {
        PointF location1;
        ref PointF local = ref location1;
        pointF = automaticField.Location;
        double x2 = (double) pointF.X + (double) location.X;
        pointF = automaticField.Location;
        double y2 = (double) pointF.Y + (double) location.Y;
        local = new PointF((float) x2, (float) y2);
        SizeF size1 = template.Size;
        double num4;
        if ((double) size1.Width != 0.0)
        {
          double width1 = (double) size.Width;
          size1 = template.Size;
          double width2 = (double) size1.Width;
          num4 = width1 / width2;
        }
        else
          num4 = 0.0;
        float scalingX = (float) num4;
        size1 = template.Size;
        double num5;
        if ((double) size1.Height != 0.0)
        {
          double height1 = (double) size.Height;
          size1 = template.Size;
          double height2 = (double) size1.Height;
          num5 = height1 / height2;
        }
        else
          num5 = 0.0;
        float scalingY = (float) num5;
        this.AutomaticFields.Add(new PdfAutomaticFieldInfo(automaticField.Field, location1, scalingX, scalingY));
        this.Page.Dictionary.Modify();
      }
    }
    this.m_getResources().RequireProcSet("ImageB");
    this.m_getResources().RequireProcSet("ImageC");
    this.m_getResources().RequireProcSet("ImageI");
    this.m_getResources().RequireProcSet("Text");
  }

  public void DrawPie(PdfBrush brush, RectangleF rectangle, float startAngle, float sweepAngle)
  {
    GraphicsPath graphicsPath = new GraphicsPath();
    graphicsPath.AddPie(new Rectangle((int) rectangle.X, (int) rectangle.Y, (int) rectangle.Width, (int) rectangle.Height), startAngle, sweepAngle);
    PdfPath path = new PdfPath(graphicsPath.PathPoints, graphicsPath.PathTypes);
    this.DrawPath(brush, path);
    graphicsPath.Dispose();
  }

  public void DrawPie(PdfPen pen, RectangleF rectangle, float startAngle, float sweepAngle)
  {
    this.DrawPie(pen, (PdfBrush) null, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, startAngle, sweepAngle);
  }

  public void DrawPie(
    PdfPen pen,
    PdfBrush brush,
    RectangleF rectangle,
    float startAngle,
    float sweepAngle)
  {
    this.DrawPie(pen, brush, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, startAngle, sweepAngle);
  }

  public void DrawPie(
    PdfBrush brush,
    float x,
    float y,
    float width,
    float height,
    float startAngle,
    float sweepAngle)
  {
    this.DrawPie((PdfPen) null, brush, x, y, width, height, startAngle, sweepAngle);
  }

  public void DrawPie(
    PdfPen pen,
    float x,
    float y,
    float width,
    float height,
    float startAngle,
    float sweepAngle)
  {
    this.DrawPie(pen, (PdfBrush) null, x, y, width, height, startAngle, sweepAngle);
  }

  public void DrawPie(
    PdfPen pen,
    PdfBrush brush,
    float x,
    float y,
    float width,
    float height,
    float startAngle,
    float sweepAngle)
  {
    if ((double) sweepAngle == 0.0)
      return;
    switch (brush)
    {
      case PdfTilingBrush _:
        this.m_bCSInitialized = false;
        float x1 = this.m_matrix.OffsetX + x;
        float y1 = this.Layer == null || this.Layer.Page == null ? this.ClientSize.Height - this.m_matrix.OffsetY + y : this.Layer.Page.Size.Height - this.m_matrix.OffsetY + y;
        (brush as PdfTilingBrush).Location = new PointF(x1, y1);
        (brush as PdfTilingBrush).Graphics.ColorSpace = this.ColorSpace;
        break;
      case PdfGradientBrush _:
        this.m_bCSInitialized = false;
        (brush as PdfGradientBrush).ColorSpace = this.ColorSpace;
        break;
    }
    this.StateControl(pen, brush, (PdfFont) null);
    this.ConstructArcPath(x, y, x + width, y + height, startAngle, sweepAngle);
    this.m_streamWriter.AppendLineSegment(x + width / 2f, y + height / 2f);
    this.DrawPath(pen, brush, true);
  }

  public void DrawPolygon(PdfBrush brush, PointF[] points)
  {
    this.DrawPolygon((PdfPen) null, brush, points);
  }

  public void DrawPolygon(PdfPen pen, PointF[] points)
  {
    this.DrawPolygon(pen, (PdfBrush) null, points);
  }

  public void DrawPolygon(PdfPen pen, PdfBrush brush, PointF[] points)
  {
    switch (brush)
    {
      case PdfTilingBrush _:
        this.m_bCSInitialized = false;
        (brush as PdfTilingBrush).Graphics.ColorSpace = this.ColorSpace;
        break;
      case PdfGradientBrush _:
        this.m_bCSInitialized = false;
        (brush as PdfGradientBrush).ColorSpace = this.ColorSpace;
        break;
    }
    int length = points.Length;
    if (length <= 0)
      return;
    this.StateControl(pen, brush, (PdfFont) null);
    this.m_streamWriter.BeginPath(points[0]);
    for (int index = 1; index < length; ++index)
      this.m_streamWriter.AppendLineSegment(points[index]);
    this.DrawPath(pen, brush, true);
  }

  public void DrawRectangle(PdfBrush brush, RectangleF rectangle)
  {
    this.DrawRectangle((PdfPen) null, brush, rectangle);
  }

  public void DrawRectangle(PdfPen pen, RectangleF rectangle)
  {
    this.DrawRectangle(pen, (PdfBrush) null, rectangle);
  }

  public void DrawRectangle(PdfPen pen, PdfBrush brush, RectangleF rectangle)
  {
    this.DrawRectangle(pen, brush, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
  }

  public void DrawRectangle(PdfBrush brush, float x, float y, float width, float height)
  {
    this.DrawRectangle((PdfPen) null, brush, x, y, width, height);
  }

  public void DrawRectangle(PdfPen pen, float x, float y, float width, float height)
  {
    this.DrawRectangle(pen, (PdfBrush) null, x, y, width, height);
  }

  public void DrawRectangle(
    PdfPen pen,
    PdfBrush brush,
    float x,
    float y,
    float width,
    float height)
  {
    if (brush is PdfSolidBrush && (brush as PdfSolidBrush).Color.A == (byte) 0)
    {
      lock (PdfGraphics.s_transparencyLock)
        PdfGraphics.m_transparencyObject = true;
    }
    if (brush is PdfTilingBrush)
    {
      this.m_bCSInitialized = false;
      float x1 = this.m_matrix.OffsetX + x;
      float y1 = this.Layer == null || this.Layer.Page == null ? this.ClientSize.Height - this.m_matrix.OffsetY + y : this.Layer.Page.Size.Height - this.m_matrix.OffsetY + y;
      (brush as PdfTilingBrush).Location = new PointF(x1, y1);
      (brush as PdfTilingBrush).Graphics.ColorSpace = this.ColorSpace;
    }
    else if (brush is PdfGradientBrush)
    {
      this.m_bCSInitialized = false;
      (brush as PdfGradientBrush).ColorSpace = this.ColorSpace;
    }
    if (brush is PdfSolidBrush && (brush as PdfSolidBrush).Color.IsEmpty)
      brush = (PdfBrush) null;
    this.StateControl(pen, brush, (PdfFont) null);
    this.StreamWriter.AppendRectangle(x, y, width, height);
    this.DrawPath(pen, brush, false);
  }

  public void DrawString(string s, PdfFont font, PdfBrush brush, PointF point)
  {
    this.DrawString(s, font, brush, point, (PdfStringFormat) null);
  }

  public void DrawString(string s, PdfFont font, PdfBrush brush, RectangleF layoutRectangle)
  {
    this.DrawString(s, font, brush, layoutRectangle, (PdfStringFormat) null);
  }

  public void DrawString(string s, PdfFont font, PdfPen pen, PointF point)
  {
    this.DrawString(s, font, pen, point, (PdfStringFormat) null);
  }

  public void DrawString(string s, PdfFont font, PdfPen pen, RectangleF layoutRectangle)
  {
    this.DrawString(s, font, pen, layoutRectangle, (PdfStringFormat) null);
  }

  public void DrawString(
    string s,
    PdfFont font,
    PdfBrush brush,
    PointF point,
    PdfStringFormat format)
  {
    this.DrawString(s, font, brush, point.X, point.Y, format);
  }

  public void DrawString(
    string s,
    PdfFont font,
    PdfBrush brush,
    RectangleF layoutRectangle,
    PdfStringFormat format)
  {
    this.DrawString(s, font, (PdfPen) null, brush, layoutRectangle, format);
  }

  public void DrawString(string s, PdfFont font, PdfBrush brush, float x, float y)
  {
    this.DrawString(s, font, brush, x, y, (PdfStringFormat) null);
  }

  public void DrawString(string s, PdfFont font, PdfPen pen, PdfBrush brush, PointF point)
  {
    this.DrawString(s, font, pen, brush, point, (PdfStringFormat) null);
  }

  public void DrawString(
    string s,
    PdfFont font,
    PdfPen pen,
    PointF point,
    PdfStringFormat format)
  {
    this.DrawString(s, font, pen, point.X, point.Y, format);
  }

  public void DrawString(
    string s,
    PdfFont font,
    PdfPen pen,
    RectangleF layoutRectangle,
    PdfStringFormat format)
  {
    this.DrawString(s, font, pen, (PdfBrush) null, layoutRectangle, format);
  }

  public void DrawString(string s, PdfFont font, PdfPen pen, float x, float y)
  {
    this.DrawString(s, font, pen, x, y, (PdfStringFormat) null);
  }

  public void DrawString(
    string s,
    PdfFont font,
    PdfBrush brush,
    float x,
    float y,
    PdfStringFormat format)
  {
    this.DrawString(s, font, (PdfPen) null, brush, x, y, format);
  }

  public void DrawString(
    string s,
    PdfFont font,
    PdfPen pen,
    PdfBrush brush,
    PointF point,
    PdfStringFormat format)
  {
    this.DrawString(s, font, pen, brush, point.X, point.Y, format);
  }

  public void DrawString(
    string s,
    PdfFont font,
    PdfPen pen,
    PdfBrush brush,
    RectangleF layoutRectangle,
    PdfStringFormat format)
  {
    if (s == null)
      throw new ArgumentNullException(nameof (s));
    s = font != null ? PdfGraphics.NormalizeText(font, s) : throw new ArgumentNullException(nameof (font));
    PdfStringLayoutResult result = new PdfStringLayouter().Layout(s, font, format, layoutRectangle.Size);
    if (!result.Empty)
    {
      RectangleF rectangleF = this.CheckCorrectLayoutRectangle(result.ActualSize, layoutRectangle.X, layoutRectangle.Y, format);
      if ((double) layoutRectangle.Width <= 0.0)
      {
        layoutRectangle.X = rectangleF.X;
        layoutRectangle.Width = rectangleF.Width;
      }
      if ((double) layoutRectangle.Height <= 0.0)
      {
        layoutRectangle.Y = rectangleF.Y;
        layoutRectangle.Height = rectangleF.Height;
      }
      if ((double) this.ClientSize.Height < 0.0)
        layoutRectangle.Y += this.ClientSize.Height;
      this.DrawStringLayoutResult(result, font, pen, brush, layoutRectangle, format);
    }
    this.m_getResources().RequireProcSet("Text");
    this.m_stringLayoutResult = result;
  }

  public void DrawString(string s, PdfFont font, PdfPen pen, PdfBrush brush, float x, float y)
  {
    this.DrawString(s, font, pen, brush, x, y, (PdfStringFormat) null);
  }

  public void DrawString(
    string s,
    PdfFont font,
    PdfPen pen,
    float x,
    float y,
    PdfStringFormat format)
  {
    this.DrawString(s, font, pen, (PdfBrush) null, x, y, format);
  }

  public void DrawString(
    string s,
    PdfFont font,
    PdfPen pen,
    PdfBrush brush,
    float x,
    float y,
    PdfStringFormat format)
  {
    RectangleF layoutRectangle = new RectangleF(x, y, 0.0f, 0.0f);
    this.DrawString(s, font, pen, brush, layoutRectangle, format);
  }

  internal void DrawStringLayoutResult(
    PdfStringLayoutResult result,
    PdfFont font,
    PdfPen pen,
    PdfBrush brush,
    RectangleF layoutRectangle,
    PdfStringFormat format)
  {
    if (result == null)
      throw new ArgumentNullException(nameof (result));
    if (font == null)
      throw new ArgumentNullException(nameof (font));
    if (result.Empty)
      return;
    int num1 = (format == null ? 0 : (!format.LineLimit ? 1 : 0)) & (format == null ? (true ? 1 : 0) : (!format.NoClip ? 1 : 0));
    PdfGraphicsState state = (PdfGraphicsState) null;
    if (num1 != 0)
    {
      state = this.Save();
      RectangleF rectangle = new RectangleF(layoutRectangle.Location, result.ActualSize);
      if ((double) layoutRectangle.Width > 0.0)
        rectangle.Width = layoutRectangle.Width;
      if (format.LineAlignment == PdfVerticalAlignment.Middle)
        rectangle.Y += (float) (((double) layoutRectangle.Height - (double) rectangle.Height) / 2.0);
      else if (format.LineAlignment == PdfVerticalAlignment.Bottom)
        rectangle.Y += layoutRectangle.Height - rectangle.Height;
      this.SetClip(rectangle);
    }
    this.ApplyStringSettings(font, pen, brush, format, layoutRectangle);
    float textScaling = format != null ? format.HorizontalScalingFactor : 100f;
    if ((double) textScaling != (double) this.m_previousTextScaling)
    {
      this.m_streamWriter.SetTextScaling(textScaling);
      this.m_previousTextScaling = textScaling;
    }
    float leading = format == null || (double) format.LineSpacing == 0.0 ? font.Height : format.LineSpacing;
    bool flag = format != null && format.SubSuperScript == PdfSubSuperScript.SubScript;
    float num2 = !this.m_isEMFPlus ? (flag ? leading - (font.Height + font.Metrics.GetDescent(format)) : leading - font.Metrics.GetAscent(format)) : (!this.m_isUseFontSize ? (flag ? leading - (font.Height + font.Metrics.GetDescent(format)) : leading - font.Metrics.GetAscent(format)) : (flag ? leading - (font.Height + font.Metrics.GetDescent(format)) : leading - font.Size));
    if (this.m_isEMF && this.m_isBaselineFormat && format != null && format.Alignment != PdfTextAlignment.Right)
      num2 = 0.0f;
    this.m_streamWriter.StartNextLine(layoutRectangle.X, layoutRectangle.Y - num2);
    if (this.m_isEMF && this.m_isBaselineFormat && format != null && format.Alignment == PdfTextAlignment.Right)
    {
      if ((double) leading > (double) font.Size)
        this.m_streamWriter.SetLeading(leading);
      else if ((double) num2 != 0.0)
        this.m_streamWriter.SetLeading(font.Size + font.Size / font.Height);
      else
        this.m_streamWriter.SetLeading(leading);
    }
    else if (!this.m_isEMF || !this.m_isBaselineFormat)
    {
      if ((double) leading > (double) font.Size)
        this.m_streamWriter.SetLeading(leading);
      else if ((double) num2 != 0.0)
        this.m_streamWriter.SetLeading(font.Size + font.Size / font.Height);
      else
        this.m_streamWriter.SetLeading(leading);
    }
    else
      this.m_streamWriter.SetLeading(0.0f);
    SizeF actualSize = result.ActualSize;
    float verticalAlignShift = this.GetTextVerticalAlignShift(actualSize.Height, layoutRectangle.Height, format);
    if (this.m_isEMF)
    {
      actualSize = result.ActualSize;
      if ((double) actualSize.Height - (double) layoutRectangle.Height > (double) font.Size / 2.0 - 1.0)
      {
        actualSize = result.ActualSize;
        verticalAlignShift = this.GetTextVerticalAlignShift(actualSize.Height, font.Height, format);
      }
    }
    if ((double) verticalAlignShift != 0.0)
      this.m_streamWriter.StartNextLine(0.0f, verticalAlignShift);
    this.DrawLayoutResult(result, font, format, layoutRectangle);
    if ((double) verticalAlignShift != 0.0)
      this.m_streamWriter.StartNextLine(0.0f, (float) -((double) verticalAlignShift - (double) result.LineHeight));
    if (this.Layer != null && this.Page != null && this.Page is PdfPage && (this.Page as PdfPage).Section.ParentDocument is PdfDocument && (this.Page as PdfPage).Section.ParentDocument.FileStructure.TaggedPdf || PdfCatalog.StructTreeRoot != null)
      this.m_streamWriter.WriteTag("EMC");
    this.m_streamWriter.EndText();
    this.UnderlineStrikeoutText(pen, brush, result, font, layoutRectangle, format);
    this.m_isEMFPlus = false;
    this.m_isUseFontSize = false;
    if (num1 == 0)
      return;
    this.Restore(state);
  }

  private void DrawUnicodeBlocks(
    string[] blocks,
    string[] words,
    PdfFont font,
    PdfStringFormat format,
    float wordSpacing)
  {
    if (blocks == null)
      throw new ArgumentNullException(nameof (blocks));
    if (words == null)
      throw new ArgumentNullException(nameof (words));
    if (font == null)
      throw new ArgumentNullException(nameof (font));
    this.m_streamWriter.StartNextLine();
    float x = 0.0f;
    float num1 = 0.0f;
    float num2 = 0.0f;
    float num3 = 0.0f;
    try
    {
      if (format != null)
      {
        num2 = format.FirstLineIndent;
        num3 = format.ParagraphIndent;
        format.FirstLineIndent = 0.0f;
        format.ParagraphIndent = 0.0f;
      }
      float num4 = font.GetCharWidth(' ', format) + wordSpacing;
      float num5 = format != null ? format.CharacterSpacing : 0.0f;
      float num6 = format == null || (double) wordSpacing != 0.0 ? 0.0f : format.WordSpacing;
      float num7 = num4 + (num5 + num6);
      int index = 0;
      for (int length = blocks.Length; index < length; ++index)
      {
        string block = blocks[index];
        string word = words[index];
        float num8 = 0.0f;
        if ((double) x != 0.0)
          this.m_streamWriter.StartNextLine(x, 0.0f);
        if (word.Length > 0)
        {
          num8 = num8 + font.MeasureString(word, format).Width + num5;
          this.m_streamWriter.ShowText(this.GetUnicodeString(block));
        }
        if (index != length - 1)
        {
          x = num8 + num7;
          num1 += x;
        }
      }
      if ((double) num1 <= 0.0)
        return;
      this.m_streamWriter.StartNextLine(-num1, 0.0f);
    }
    finally
    {
      if (format != null)
      {
        format.FirstLineIndent = num2;
        format.ParagraphIndent = num3;
      }
    }
  }

  private void DrawUnicodeLine(
    LineInfo lineInfo,
    RectangleF layoutRectangle,
    PdfFont font,
    PdfStringFormat format)
  {
    if (font == null)
      throw new ArgumentNullException(nameof (font));
    string text = lineInfo.Text;
    double width = (double) lineInfo.Width;
    int num = format == null ? 0 : (format.RightToLeft ? 1 : 0);
    bool wordSpace = format != null && ((double) format.WordSpacing != 0.0 || format.Alignment == PdfTextAlignment.Justify);
    PdfTrueTypeFont pdfTrueTypeFont = font as PdfTrueTypeFont;
    float wordSpacing = this.JustifyLine(lineInfo, layoutRectangle.Width, format);
    if (num != 0)
    {
      bool rtl = format != null && format.Alignment == PdfTextAlignment.Right;
      string[] blocks = RtlRenderer.Layout(text, pdfTrueTypeFont, rtl, wordSpace);
      string[] words;
      if (blocks.Length > 1)
        words = RtlRenderer.SplitLayout(text, pdfTrueTypeFont, rtl, wordSpace);
      else
        words = new string[1]{ text };
      this.DrawUnicodeBlocks(blocks, words, font, format, wordSpacing);
    }
    else if (wordSpace)
    {
      string[] words = (string[]) null;
      this.DrawUnicodeBlocks(this.BreakUnicodeLine(text, pdfTrueTypeFont, out words), words, font, format, wordSpacing);
    }
    else
      this.m_streamWriter.ShowNextLineText(this.GetUnicodeString(this.ConvertToUnicode(text, pdfTrueTypeFont)));
  }

  private void FlipHorizontal()
  {
    PdfTransformationMatrix matrix = new PdfTransformationMatrix();
    matrix.Translate(0.0f, this.Size.Height);
    matrix.Scale(1f, -1f);
    this.m_streamWriter.ModifyCTM(matrix);
  }

  private void FlipVertical()
  {
    PdfTransformationMatrix matrix = new PdfTransformationMatrix();
    matrix.Translate(this.Size.Width, 0.0f);
    matrix.Scale(-1f, 1f);
    this.m_streamWriter.ModifyCTM(matrix);
  }

  public void Flush()
  {
    if (!this.m_bStateSaved)
      return;
    this.m_streamWriter.RestoreGraphicsState();
    this.m_bStateSaved = false;
  }

  private void FontControl(PdfFont font, PdfStringFormat format, bool saveState)
  {
    if (font == null)
      return;
    PdfSubSuperScript pdfSubSuperScript1 = format != null ? format.SubSuperScript : PdfSubSuperScript.None;
    PdfSubSuperScript pdfSubSuperScript2 = this.m_currentStringFormat != null ? this.m_currentStringFormat.SubSuperScript : PdfSubSuperScript.None;
    if (!saveState && font == this.m_currentFont && pdfSubSuperScript1 == pdfSubSuperScript2)
      return;
    PdfName name = this.m_getResources().GetName((IPdfWrapper) font);
    this.m_currentFont = font;
    this.m_currentStringFormat = format;
    float size = font.Metrics.GetSize(format);
    this.m_streamWriter.SetFont(font, name, size);
  }

  private PdfString GetAsciiString(string token)
  {
    return token != null ? new PdfString(token)
    {
      Encode = PdfString.ForceEncoding.ASCII
    } : throw new ArgumentNullException(nameof (token));
  }

  internal static List<float[]> GetBezierArcPoints(
    float x1,
    float y1,
    float x2,
    float y2,
    float startAng,
    float extent)
  {
    if ((double) x1 > (double) x2)
    {
      double num = (double) x1;
      x1 = x2;
      x2 = (float) num;
    }
    if ((double) y2 > (double) y1)
    {
      double num = (double) y1;
      y1 = y2;
      y2 = (float) num;
    }
    float num1;
    int num2;
    if ((double) Math.Abs(extent) <= 90.0)
    {
      num1 = extent;
      num2 = 1;
    }
    else
    {
      num2 = (int) Math.Ceiling((double) Math.Abs(extent) / 90.0);
      num1 = extent / (float) num2;
    }
    float num3 = (float) (((double) x1 + (double) x2) / 2.0);
    float num4 = (float) (((double) y1 + (double) y2) / 2.0);
    float num5 = (float) (((double) x2 - (double) x1) / 2.0);
    float num6 = (float) (((double) y2 - (double) y1) / 2.0);
    float num7 = (float) ((double) num1 * Math.PI / 360.0);
    float num8 = (float) Math.Abs(4.0 / 3.0 * (1.0 - Math.Cos((double) num7)) / Math.Sin((double) num7));
    List<float[]> bezierArcPoints = new List<float[]>();
    for (int index = 0; index < num2; ++index)
    {
      float num9 = (float) (((double) startAng + (double) index * (double) num1) * Math.PI / 180.0);
      double num10 = ((double) startAng + (double) (index + 1) * (double) num1) * Math.PI / 180.0;
      float num11 = (float) Math.Cos((double) num9);
      float num12 = (float) Math.Cos(num10);
      float num13 = (float) Math.Sin((double) num9);
      float num14 = (float) Math.Sin(num10);
      if ((double) num1 > 0.0)
        bezierArcPoints.Add(new float[8]
        {
          num3 + num5 * num11,
          num4 - num6 * num13,
          num3 + num5 * (num11 - num8 * num13),
          num4 - num6 * (num13 + num8 * num11),
          num3 + num5 * (num12 + num8 * num14),
          num4 - num6 * (num14 - num8 * num12),
          num3 + num5 * num12,
          num4 - num6 * num14
        });
      else
        bezierArcPoints.Add(new float[8]
        {
          num3 + num5 * num11,
          num4 - num6 * num13,
          num3 + num5 * (num11 + num8 * num13),
          num4 - num6 * (num13 - num8 * num11),
          num3 + num5 * (num12 - num8 * num14),
          num4 - num6 * (num14 + num8 * num12),
          num3 + num5 * num12,
          num4 - num6 * num14
        });
    }
    return bezierArcPoints;
  }

  private void GetBezierPoints(
    PointF[] points,
    byte[] types,
    ref int i,
    out PointF p2,
    out PointF p3)
  {
    ++i;
    p2 = ((int) types[i] & 15) == 3 ? points[i] : throw new ArgumentException("Malforming path.");
    ++i;
    p3 = ((int) types[i] & 15) == 3 ? points[i] : throw new ArgumentException("Malforming path.");
  }

  private byte[] GetCjkString(string line)
  {
    return line != null ? PdfString.EscapeSymbols(PdfString.ToUnicodeArray(line, false)) : throw new ArgumentNullException(nameof (line));
  }

  private float GetHorizontalAlignShift(float lineWidth, float boundsWidth, PdfStringFormat format)
  {
    float horizontalAlignShift = 0.0f;
    if ((double) boundsWidth >= 0.0 && format != null)
    {
      switch (format.Alignment)
      {
        case PdfTextAlignment.Left:
          return horizontalAlignShift;
        case PdfTextAlignment.Center:
          return (float) (((double) boundsWidth - (double) lineWidth) / 2.0);
        case PdfTextAlignment.Right:
          return boundsWidth - lineWidth;
      }
    }
    return horizontalAlignShift;
  }

  internal RectangleF GetLineBounds(
    int lineIndex,
    PdfStringLayoutResult result,
    PdfFont font,
    RectangleF layoutRectangle,
    PdfStringFormat format)
  {
    if (result == null)
      throw new ArgumentNullException(nameof (result));
    if (font == null)
      throw new ArgumentNullException(nameof (font));
    RectangleF lineBounds = RectangleF.Empty;
    if (!result.Empty && lineIndex < result.LineCount && lineIndex >= 0)
    {
      LineInfo line = result.Lines[lineIndex];
      float y = (float) ((double) this.GetTextVerticalAlignShift(result.ActualSize.Height, layoutRectangle.Height, format) + (double) layoutRectangle.Y + (double) result.LineHeight * (double) lineIndex);
      float width1 = line.Width;
      float horizontalAlignShift = this.GetHorizontalAlignShift(width1, layoutRectangle.Width, format);
      float lineIndent = this.GetLineIndent(line, format, layoutRectangle, lineIndex == 0);
      float num = horizontalAlignShift + (!this.RightToLeft(format) ? lineIndent : 0.0f);
      float x = layoutRectangle.X + num;
      float width2 = !this.ShouldJustify(line, layoutRectangle.Width, format) ? width1 - lineIndent : layoutRectangle.Width - lineIndent;
      float lineHeight = result.LineHeight;
      lineBounds = new RectangleF(x, y, width2, lineHeight);
    }
    return lineBounds;
  }

  private float GetLineIndent(
    LineInfo lineInfo,
    PdfStringFormat format,
    RectangleF layoutBounds,
    bool firstLine)
  {
    float lineIndent = 0.0f;
    bool flag = (lineInfo.LineType & LineType.FirstParagraphLine) > LineType.None;
    if (format != null & flag)
    {
      float val2 = firstLine ? format.FirstLineIndent : format.ParagraphIndent;
      lineIndent = (double) layoutBounds.Width > 0.0 ? Math.Min(layoutBounds.Width, val2) : val2;
    }
    return lineIndent;
  }

  private PdfTransformationMatrix GetRotateTransform(float angle, PdfTransformationMatrix input)
  {
    if (input == null)
      input = new PdfTransformationMatrix();
    input.Rotate(PdfGraphics.UpdateY(angle));
    return input;
  }

  private PdfTransformationMatrix GetScaleTransform(
    float x,
    float y,
    PdfTransformationMatrix input)
  {
    if (input == null)
      input = new PdfTransformationMatrix();
    input.Scale(x, y);
    return input;
  }

  private PdfTransformationMatrix GetSkewTransform(
    float angleX,
    float angleY,
    PdfTransformationMatrix input)
  {
    if (input == null)
      input = new PdfTransformationMatrix();
    input.Skew(PdfGraphics.UpdateY(angleX), PdfGraphics.UpdateY(angleY));
    return input;
  }

  private string[] GetTextLines(string text)
  {
    MatchCollection matchCollection = new Regex("[^\r\n]*").Matches(text);
    int count = matchCollection.Count;
    List<string> stringList = new List<string>();
    bool flag = true;
    for (int i = 0; i < count; ++i)
    {
      string str = matchCollection[i].Value;
      if (str == string.Empty && !flag)
      {
        flag = true;
      }
      else
      {
        if (str != string.Empty)
          flag = false;
        stringList.Add(str);
      }
    }
    return stringList.ToArray();
  }

  private TextRenderingMode GetTextRenderingMode(
    PdfPen pen,
    PdfBrush brush,
    PdfStringFormat format)
  {
    TextRenderingMode textRenderingMode = TextRenderingMode.None;
    if (pen != null && brush != null)
      textRenderingMode = TextRenderingMode.FillStroke;
    else if (pen != null)
      textRenderingMode = TextRenderingMode.Stroke;
    else if (brush != null)
      textRenderingMode = TextRenderingMode.Fill;
    if (format != null && format.ClipPath)
      textRenderingMode |= TextRenderingMode.ClipFill;
    return textRenderingMode;
  }

  internal float GetTextVerticalAlignShift(
    float textHeight,
    float boundsHeight,
    PdfStringFormat format)
  {
    float verticalAlignShift = 0.0f;
    if ((double) boundsHeight >= 0.0 && format != null)
    {
      switch (format.LineAlignment)
      {
        case PdfVerticalAlignment.Top:
          return verticalAlignShift;
        case PdfVerticalAlignment.Middle:
          return (float) (((double) boundsHeight - (double) textHeight) / 2.0);
        case PdfVerticalAlignment.Bottom:
          return boundsHeight - textHeight;
      }
    }
    return verticalAlignShift;
  }

  private PdfTransformationMatrix GetTranslateTransform(
    float x,
    float y,
    PdfTransformationMatrix input)
  {
    if (input == null)
      input = new PdfTransformationMatrix();
    input.Translate(x, PdfGraphics.UpdateY(y));
    return input;
  }

  private PdfString GetUnicodeString(string token)
  {
    return token != null ? new PdfString(token)
    {
      Converted = true,
      Encode = PdfString.ForceEncoding.ASCII
    } : throw new ArgumentNullException(nameof (token));
  }

  private void InitCurrentColorSpace()
  {
    if (this.m_bCSInitialized)
      return;
    this.m_streamWriter.SetColorSpace("DeviceRGB", true);
    this.m_streamWriter.SetColorSpace("DeviceRGB", false);
    this.m_bCSInitialized = true;
  }

  private void InitCurrentColorSpace(PdfColorSpace colorspace)
  {
    PdfResources pdfResources = this.m_getResources();
    if (this.m_bCSInitialized)
      return;
    if (this.m_currentColorSpace != PdfColorSpace.GrayScale)
    {
      this.m_streamWriter.SetColorSpace("Device" + this.m_currentColorSpace.ToString(), true);
      this.m_streamWriter.SetColorSpace("Device" + this.m_currentColorSpace.ToString(), false);
      this.m_bCSInitialized = true;
    }
    else
    {
      this.m_streamWriter.SetColorSpace("DeviceGray", true);
      this.m_streamWriter.SetColorSpace("DeviceGray", false);
      this.m_bCSInitialized = true;
    }
  }

  private void Initialize()
  {
    this.m_bStateSaved = false;
    this.m_currentPen = (PdfPen) null;
    this.m_currentBrush = (PdfBrush) null;
    this.m_currentFont = (PdfFont) null;
    this.m_currentColorSpace = PdfColorSpace.RGB;
    this.m_bCSInitialized = false;
    this.m_matrix = (PdfTransformationMatrix) null;
    this.m_previousTextRenderingMode = ~TextRenderingMode.Fill;
    this.m_previousCharacterSpacing = -1f;
    this.m_previousWordSpacing = -1f;
    this.m_previousTextScaling = -100f;
    this.m_trasparencies = (Dictionary<PdfGraphics.TransparencyData, PdfTransparency>) null;
    this.m_currentStringFormat = (PdfStringFormat) null;
    this.m_clipBounds = new RectangleF(PointF.Empty, this.Size);
    this.m_graphicsState = new Stack<PdfGraphicsState>();
    this.m_getResources().RequireProcSet("PDF");
  }

  internal void InitializeCoordinates()
  {
    this.m_streamWriter.WriteComment("Change co-ordinate system to left/top.");
    this.TranslateTransform(0.0f, PdfGraphics.UpdateY(this.Size.Height));
  }

  internal void InitializeCoordinates(PdfPageBase page)
  {
    PointF empty = PointF.Empty;
    PdfDictionary dictionary = page.Dictionary;
    bool flag = false;
    if (page.Dictionary.ContainsKey("CropBox") && page.Dictionary.ContainsKey("MediaBox"))
    {
      PdfArray pdfArray1 = page.Dictionary["CropBox"] as PdfArray;
      PdfArray pdfArray2 = page.Dictionary["MediaBox"] as PdfArray;
      if (pdfArray1.ToRectangle() == pdfArray2.ToRectangle())
        flag = true;
      if ((double) (pdfArray1[0] as PdfNumber).FloatValue > 0.0 && (double) (pdfArray1[3] as PdfNumber).FloatValue > 0.0 && (double) (pdfArray2[0] as PdfNumber).FloatValue < 0.0 && (double) (pdfArray2[1] as PdfNumber).FloatValue < 0.0)
      {
        this.TranslateTransform((pdfArray1[0] as PdfNumber).FloatValue, -(pdfArray1[3] as PdfNumber).FloatValue);
        empty.X = -(pdfArray1[0] as PdfNumber).FloatValue;
        empty.Y = (pdfArray1[3] as PdfNumber).FloatValue;
      }
    }
    else if (!page.Dictionary.ContainsKey("CropBox"))
      flag = true;
    if (flag)
    {
      this.m_streamWriter.WriteComment("Change co-ordinate system to left/top.");
      this.TranslateTransform(0.0f, PdfGraphics.UpdateY(this.Size.Height));
    }
    else
    {
      PdfTransformationMatrix input = new PdfTransformationMatrix();
      this.GetTranslateTransform(empty.X, empty.Y + 0.0f, input);
    }
  }

  private float JustifyLine(LineInfo lineInfo, float boundsWidth, PdfStringFormat format)
  {
    string text = lineInfo.Text;
    float width = lineInfo.Width;
    int num1 = this.ShouldJustify(lineInfo, boundsWidth, format) ? 1 : 0;
    bool flag = format != null && (double) format.WordSpacing != 0.0;
    char[] spaces = StringTokenizer.Spaces;
    int charsCount = StringTokenizer.GetCharsCount(text, spaces);
    float num2 = 0.0f;
    if (num1 != 0)
    {
      if (flag)
        width -= (float) charsCount * format.WordSpacing;
      float wordSpacing = (boundsWidth - width) / (float) charsCount;
      this.m_streamWriter.SetWordSpacing(wordSpacing);
      return wordSpacing;
    }
    if (format != null && format.Alignment == PdfTextAlignment.Justify)
      this.m_streamWriter.SetWordSpacing(0.0f);
    return num2;
  }

  internal void MultiplyTransform(PdfTransformationMatrix matrix)
  {
    this.m_streamWriter.ModifyCTM(matrix);
  }

  public void MultiplyTransform1(PdfTransformationMatrix matrix)
  {
    this.m_streamWriter.ModifyCTM(matrix);
    this.Matrix.Multiply(matrix);
  }

  internal void ResetTransform()
  {
    System.Drawing.Drawing2D.Matrix matrix = this.Matrix.Matrix.Clone();
    matrix.Invert();
    this.m_streamWriter.ModifyCTM(new PdfTransformationMatrix()
    {
      Matrix = matrix
    });
    this.Matrix.Matrix.Multiply(matrix);
  }

  internal static string NormalizeText(PdfFont font, string text)
  {
    PdfTrueTypeFont pdfTrueTypeFont = font as PdfTrueTypeFont;
    if (font is PdfStandardFont || pdfTrueTypeFont != null && !pdfTrueTypeFont.Unicode)
      text = PdfStandardFont.Convert(text);
    return text;
  }

  private void PageSave(object sender, EventArgs e)
  {
    if (this.m_automaticFields == null)
      return;
    foreach (PdfAutomaticFieldInfo automaticField in (PdfCollection) this.m_automaticFields)
      automaticField.Field.PerformDraw(this, automaticField.Location, automaticField.ScalingX, automaticField.ScalingY);
  }

  private void PenControl(PdfPen pen, bool saveState)
  {
    if (pen == null)
      return;
    bool flag1 = false;
    bool flag2 = false;
    PdfPen pdfPen = pen;
    if (pdfPen != null && pdfPen.Colorspaces != null)
    {
      if (pdfPen.Colorspaces is PdfCalRGBColor)
        this.ColorSpace = PdfColorSpace.RGB;
      else if (pdfPen.Colorspaces is PdfCalGrayColor)
        this.ColorSpace = PdfColorSpace.GrayScale;
      else if (pdfPen.Colorspaces is PdfICCColor)
      {
        flag1 = true;
        PdfICCColor colorspaces = pdfPen.Colorspaces as PdfICCColor;
        if (colorspaces.ColorSpaces.AlternateColorSpace != null)
        {
          if (colorspaces.ColorSpaces.AlternateColorSpace is PdfCalGrayColorSpace)
            this.ColorSpace = PdfColorSpace.GrayScale;
          else if (colorspaces.ColorSpaces.AlternateColorSpace is PdfCalRGBColorSpace)
            this.ColorSpace = PdfColorSpace.RGB;
          else if (colorspaces.ColorSpaces.AlternateColorSpace is PdfLabColorSpace)
            this.ColorSpace = PdfColorSpace.RGB;
          else if (colorspaces.ColorSpaces.AlternateColorSpace is PdfDeviceColorSpace)
          {
            switch ((colorspaces.ColorSpaces.AlternateColorSpace as PdfDeviceColorSpace).DeviceColorSpaceType.ToString())
            {
              case "RGB":
                this.ColorSpace = PdfColorSpace.RGB;
                break;
              case "GrayScale":
                this.ColorSpace = PdfColorSpace.GrayScale;
                break;
              case "CMYK":
                this.ColorSpace = PdfColorSpace.CMYK;
                break;
            }
          }
        }
        else
          this.ColorSpace = PdfColorSpace.RGB;
      }
      else if (pdfPen.Colorspaces is PdfSeparationColor)
      {
        flag1 = true;
        this.ColorSpace = PdfColorSpace.GrayScale;
      }
      else if (pdfPen.Colorspaces is PdfIndexedColor)
      {
        flag2 = true;
        this.ColorSpace = PdfColorSpace.GrayScale;
      }
    }
    if (!(flag1 || flag2 ? (!flag2 ? pen.MonitorChanges(this.m_currentPen, this.m_streamWriter, this.m_getResources, saveState, this.ColorSpace, this.Matrix.Clone(), true) : pen.MonitorChanges(this.m_currentPen, this.m_streamWriter, this.m_getResources, saveState, this.ColorSpace, this.Matrix.Clone(), true)) : pen.MonitorChanges(this.m_currentPen, this.m_streamWriter, this.m_getResources, saveState, this.ColorSpace, this.Matrix.Clone())))
      return;
    this.m_currentPen = pen.Clone();
  }

  internal void PutComment(string comment) => this.m_streamWriter.WriteComment(comment);

  internal void Reset(SizeF size)
  {
    this.m_canvasSize = size;
    this.m_streamWriter.Clear();
    this.Initialize();
    this.InitializeCoordinates();
  }

  public void Restore()
  {
    if (this.m_graphicsState.Count <= 0)
      return;
    this.DoRestoreState();
  }

  public void Restore(PdfGraphicsState state)
  {
    if (state == null)
      throw new ArgumentNullException(nameof (state));
    if (state.Graphics != this)
      throw new ArgumentException("The GraphicsState belongs to another Graphics object.", nameof (state));
    if (!this.m_graphicsState.Contains(state))
      return;
    do
      ;
    while (this.m_graphicsState.Count != 0 && this.DoRestoreState() != state);
  }

  private bool RightToLeft(PdfStringFormat format) => format != null && format.RightToLeft;

  public void RotateTransform(float angle)
  {
    PdfTransformationMatrix transformationMatrix = new PdfTransformationMatrix();
    this.GetRotateTransform(angle, transformationMatrix);
    this.m_streamWriter.ModifyCTM(transformationMatrix);
    this.Matrix.Multiply(transformationMatrix);
  }

  public PdfGraphicsState Save()
  {
    PdfGraphicsState pdfGraphicsState = new PdfGraphicsState(this, this.Matrix.Clone());
    pdfGraphicsState.Brush = this.m_currentBrush;
    pdfGraphicsState.Pen = this.m_currentPen;
    pdfGraphicsState.Font = this.m_currentFont;
    pdfGraphicsState.ColorSpace = this.m_currentColorSpace;
    pdfGraphicsState.CharacterSpacing = this.m_previousCharacterSpacing;
    pdfGraphicsState.WordSpacing = this.m_previousWordSpacing;
    pdfGraphicsState.TextScaling = this.m_previousTextScaling;
    pdfGraphicsState.TextRenderingMode = this.m_previousTextRenderingMode;
    this.m_graphicsState.Push(pdfGraphicsState);
    if (this.m_bStateSaved)
    {
      this.m_streamWriter.RestoreGraphicsState();
      this.m_bStateSaved = false;
    }
    this.m_streamWriter.SaveGraphicsState();
    return pdfGraphicsState;
  }

  public void ScaleTransform(float scaleX, float scaleY)
  {
    PdfTransformationMatrix transformationMatrix = new PdfTransformationMatrix();
    this.GetScaleTransform(scaleX, scaleY, transformationMatrix);
    this.m_streamWriter.ModifyCTM(transformationMatrix);
    this.Matrix.Multiply(transformationMatrix);
  }

  internal void SetBBox(RectangleF bounds)
  {
    this.m_streamWriter.GetStream()["BBox"] = (IPdfPrimitive) PdfArray.FromRectangle(bounds);
  }

  public void SetClip(PdfPath path)
  {
    if (path == null)
      throw new ArgumentNullException(nameof (path));
    this.SetClip(path, path.FillMode);
  }

  public void SetClip(RectangleF rectangle) => this.SetClip(rectangle, PdfFillMode.Winding);

  public void SetClip(PdfPath path, PdfFillMode mode)
  {
    if (path == null)
      throw new ArgumentNullException(nameof (path));
    this.BuildUpPath(path);
    this.m_streamWriter.ClipPath(mode == PdfFillMode.Alternate);
  }

  public void SetClip(RectangleF rectangle, PdfFillMode mode)
  {
    this.m_streamWriter.AppendRectangle(rectangle);
    this.m_streamWriter.ClipPath(mode == PdfFillMode.Alternate);
  }

  internal void SetLayer(PdfPageLayer layer)
  {
    this.m_layer = layer;
    if (layer.Page is PdfPage page)
      page.BeginSave += new EventHandler(this.PageSave);
    else
      (layer.Page as PdfLoadedPage).BeginSave += new EventHandler(this.PageSave);
  }

  public void SetTransparency(float alpha)
  {
    this.SetTransparency(alpha, alpha, PdfBlendMode.Normal);
  }

  public void SetTransparency(float alphaPen, float alphaBrush)
  {
    this.SetTransparency(alphaPen, alphaBrush, PdfBlendMode.Normal);
  }

  public void SetTransparency(float alphaPen, float alphaBrush, PdfBlendMode blendMode)
  {
    if (this.m_trasparencies == null)
      this.m_trasparencies = new Dictionary<PdfGraphics.TransparencyData, PdfTransparency>();
    PdfTransparency pdfTransparency = (PdfTransparency) null;
    PdfGraphics.TransparencyData key = new PdfGraphics.TransparencyData(alphaPen, alphaBrush, blendMode);
    if (this.m_trasparencies.ContainsKey(key))
      pdfTransparency = this.m_trasparencies[key];
    if (pdfTransparency == null)
    {
      pdfTransparency = new PdfTransparency(alphaPen, alphaBrush, blendMode);
      this.m_trasparencies[key] = pdfTransparency;
    }
    this.StreamWriter.SetGraphicsState(this.m_getResources().GetName((IPdfWrapper) pdfTransparency));
  }

  internal void SetTransparencyGroup(PdfPageBase page)
  {
    PdfDictionary pdfDictionary = new PdfDictionary();
    pdfDictionary.SetName("CS", "DeviceRGB");
    pdfDictionary.SetBoolean("K", false);
    pdfDictionary.SetName("S", "Transparency");
    pdfDictionary.SetBoolean("I", false);
    page.Dictionary["Group"] = (IPdfPrimitive) pdfDictionary;
  }

  private bool ShouldJustify(LineInfo lineInfo, float boundsWidth, PdfStringFormat format)
  {
    string text = lineInfo.Text;
    float width = lineInfo.Width;
    int num1 = format == null ? 0 : (format.Alignment == PdfTextAlignment.Justify ? 1 : 0);
    bool flag1 = (double) boundsWidth >= 0.0 && (double) width < (double) boundsWidth;
    char[] spaces = StringTokenizer.Spaces;
    bool flag2 = StringTokenizer.GetCharsCount(text, spaces) > 0 && text[0] != ' ';
    bool flag3 = (lineInfo.LineType & LineType.LayoutBreak) > LineType.None;
    int num2 = flag1 ? 1 : 0;
    return (num1 & num2 & (flag2 ? 1 : 0) & (flag3 ? 1 : 0)) != 0;
  }

  public void SkewTransform(float angleX, float angleY)
  {
    PdfTransformationMatrix transformationMatrix = new PdfTransformationMatrix();
    this.GetSkewTransform(angleX, angleY, transformationMatrix);
    this.m_streamWriter.ModifyCTM(transformationMatrix);
    this.Matrix.Multiply(transformationMatrix);
  }

  private void StateControl(PdfPen pen, PdfBrush brush, PdfFont font)
  {
    this.StateControl(pen, brush, font, (PdfStringFormat) null);
  }

  private void StateControl(PdfPen pen, PdfBrush brush, PdfFont font, PdfStringFormat format)
  {
    if ((pen != null && pen.Color.A == (byte) 0 || brush != null && brush is PdfSolidBrush && (brush as PdfSolidBrush).Color.A == (byte) 0) && this.Layer != null && !this.Layer.Page.Dictionary.ContainsKey("Group"))
      this.SetTransparencyGroup(this.Layer.Page);
    if (brush is PdfGradientBrush)
    {
      this.m_bCSInitialized = false;
      (brush as PdfGradientBrush).ColorSpace = this.ColorSpace;
    }
    if (brush is PdfTilingBrush)
    {
      this.m_bCSInitialized = false;
      (brush as PdfTilingBrush).Graphics.ColorSpace = this.ColorSpace;
    }
    bool saveState = false;
    if (brush != null)
    {
      if (brush is PdfSolidBrush pdfSolidBrush)
      {
        if (pdfSolidBrush.Colorspaces != null)
        {
          this.ColorSpaceControl(pdfSolidBrush.Colorspaces.ColorSpace);
        }
        else
        {
          if (this.m_layer != null)
          {
            if (this.m_layer.Page is PdfPage && (this.m_layer.Page as PdfPage).Section.ParentDocument.GetType().Name != "PdfLoadedDocument")
            {
              this.ColorSpace = (this.m_layer.Page as PdfPage).Document.ColorSpace;
              this.m_currentColorSpace = (this.m_layer.Page as PdfPage).Document.ColorSpace;
            }
            else if (this.m_layer.Page is PdfLoadedPage)
            {
              this.ColorSpace = ((this.m_layer.Page as PdfLoadedPage).Document as PdfLoadedDocument).ColorSpace;
              this.m_currentColorSpace = ((this.m_layer.Page as PdfLoadedPage).Document as PdfLoadedDocument).ColorSpace;
            }
          }
          this.InitCurrentColorSpace(this.m_currentColorSpace);
        }
      }
      else
      {
        if (this.m_layer != null)
        {
          if (this.m_layer.Page is PdfPage && (this.m_layer.Page as PdfPage).Section.ParentDocument.GetType().Name != "PdfLoadedDocument")
          {
            this.ColorSpace = (this.m_layer.Page as PdfPage).Document.ColorSpace;
            this.m_currentColorSpace = (this.m_layer.Page as PdfPage).Document.ColorSpace;
          }
          else if (this.m_layer.Page is PdfLoadedPage)
          {
            this.ColorSpace = ((this.m_layer.Page as PdfLoadedPage).Document as PdfLoadedDocument).ColorSpace;
            this.m_currentColorSpace = ((this.m_layer.Page as PdfLoadedPage).Document as PdfLoadedDocument).ColorSpace;
          }
        }
        this.InitCurrentColorSpace(this.m_currentColorSpace);
      }
    }
    else if (pen != null)
    {
      PdfPen pdfPen = pen;
      if (pdfPen != null)
      {
        if (pdfPen.Colorspaces != null)
        {
          this.ColorSpaceControl(pdfPen.Colorspaces.ColorSpace);
        }
        else
        {
          if (this.m_layer != null)
          {
            if (this.m_layer.Page is PdfPage && (this.m_layer.Page as PdfPage).Section.ParentDocument.GetType().Name != "PdfLoadedDocument")
            {
              this.ColorSpace = (this.m_layer.Page as PdfPage).Document.ColorSpace;
              this.m_currentColorSpace = (this.m_layer.Page as PdfPage).Document.ColorSpace;
            }
            else if (this.m_layer.Page is PdfLoadedPage)
            {
              this.ColorSpace = ((this.m_layer.Page as PdfLoadedPage).Document as PdfLoadedDocument).ColorSpace;
              this.m_currentColorSpace = ((this.m_layer.Page as PdfLoadedPage).Document as PdfLoadedDocument).ColorSpace;
            }
          }
          this.InitCurrentColorSpace(this.m_currentColorSpace);
        }
      }
      else
      {
        if (this.m_layer != null)
        {
          if (this.m_layer.Page is PdfPage && (this.m_layer.Page as PdfPage).Section.ParentDocument.GetType().Name != "PdfLoadedDocument")
          {
            this.ColorSpace = (this.m_layer.Page as PdfPage).Document.ColorSpace;
            this.m_currentColorSpace = (this.m_layer.Page as PdfPage).Document.ColorSpace;
          }
          else if (this.m_layer.Page is PdfLoadedPage)
          {
            this.ColorSpace = ((this.m_layer.Page as PdfLoadedPage).Document as PdfLoadedDocument).ColorSpace;
            this.m_currentColorSpace = ((this.m_layer.Page as PdfLoadedPage).Document as PdfLoadedDocument).ColorSpace;
          }
        }
        this.InitCurrentColorSpace(this.m_currentColorSpace);
      }
    }
    if (saveState)
    {
      if (this.m_bStateSaved)
        this.m_streamWriter.RestoreGraphicsState();
      this.m_streamWriter.SaveGraphicsState();
      this.m_bStateSaved = true;
    }
    this.PenControl(pen, saveState);
    this.BrushControl(brush, saveState);
    this.FontControl(font, format, saveState);
  }

  public void TranslateTransform(float offsetX, float offsetY)
  {
    PdfTransformationMatrix transformationMatrix = new PdfTransformationMatrix();
    this.GetTranslateTransform(offsetX, offsetY, transformationMatrix);
    this.m_streamWriter.ModifyCTM(transformationMatrix);
    this.Matrix.Multiply(transformationMatrix);
  }

  internal void TranslateTransform(float offsetX, float offsetY, bool value)
  {
    PdfTransformationMatrix transformationMatrix = new PdfTransformationMatrix(value);
    this.GetTranslateTransform(offsetX, offsetY, transformationMatrix);
    this.m_streamWriter.ModifyCTM(transformationMatrix);
    this.Matrix.Multiply(transformationMatrix);
  }

  private void UnderlineStrikeoutText(
    PdfPen pen,
    PdfBrush brush,
    PdfStringLayoutResult result,
    PdfFont font,
    RectangleF layoutRectangle,
    PdfStringFormat format)
  {
    if (result == null)
      throw new ArgumentNullException(nameof (result));
    if (font == null)
      throw new ArgumentNullException(nameof (font));
    if (!font.Underline && !font.Strikeout)
      return;
    PdfPen underlineStikeoutPen = this.CreateUnderlineStikeoutPen(pen, brush, font, format);
    if (underlineStikeoutPen == null)
      return;
    float verticalAlignShift = this.GetTextVerticalAlignShift(result.ActualSize.Height, layoutRectangle.Height, format);
    if (format != null && format.SubSuperScript == PdfSubSuperScript.SubScript)
      verticalAlignShift += font.Height - font.Metrics.GetHeight(format);
    float num1 = !this.m_isEMFPlus ? (float) ((double) layoutRectangle.Y + (double) verticalAlignShift + (double) font.Metrics.GetAscent(format) + 1.5 * (double) underlineStikeoutPen.Width) : (!this.m_isUseFontSize ? (float) ((double) layoutRectangle.Y + (double) verticalAlignShift + (double) font.Metrics.GetAscent(format) + 1.5 * (double) underlineStikeoutPen.Width) : (float) ((double) layoutRectangle.Y + (double) verticalAlignShift + (double) font.Size + 1.5 * (double) underlineStikeoutPen.Width));
    float num2 = (float) ((double) layoutRectangle.Y + (double) verticalAlignShift + (double) font.Metrics.GetHeight(format) / 2.0 + 1.5 * (double) underlineStikeoutPen.Width);
    LineInfo[] lines = result.Lines;
    int index = 0;
    for (int lineCount = result.LineCount; index < lineCount; ++index)
    {
      LineInfo lineInfo = lines[index];
      string text = lineInfo.Text;
      float width = lineInfo.Width;
      float horizontalAlignShift = this.GetHorizontalAlignShift(width, layoutRectangle.Width, format);
      float lineIndent = this.GetLineIndent(lineInfo, format, layoutRectangle, index == 0);
      float num3 = horizontalAlignShift + (!this.RightToLeft(format) ? lineIndent : 0.0f);
      float x1 = layoutRectangle.X + num3;
      float x2 = !this.ShouldJustify(lineInfo, layoutRectangle.Width, format) ? x1 + width - lineIndent : x1 + layoutRectangle.Width - lineIndent;
      if (font.Underline)
      {
        float num4 = num1;
        this.DrawLine(underlineStikeoutPen, x1, num4, x2, num4);
        num1 += result.LineHeight;
      }
      if (font.Strikeout)
      {
        float num5 = num2;
        this.DrawLine(underlineStikeoutPen, x1, num5, x2, num5);
        num2 += result.LineHeight;
      }
    }
  }

  internal static float UpdateY(float y) => -y;

  internal PdfAutomaticFieldInfoCollection AutomaticFields
  {
    get
    {
      if (this.m_automaticFields == null)
        this.m_automaticFields = new PdfAutomaticFieldInfoCollection();
      return this.m_automaticFields;
    }
  }

  public SizeF ClientSize => this.m_clipBounds.Size;

  public PdfColorSpace ColorSpace
  {
    get => this.m_currentColorSpace;
    set => this.m_currentColorSpace = value;
  }

  internal PdfPageLayer Layer => this.m_layer;

  public PdfTransformationMatrix Matrix
  {
    get
    {
      if (this.m_matrix == null)
        this.m_matrix = new PdfTransformationMatrix();
      return this.m_matrix;
    }
  }

  internal PdfPageBase Page => this.m_layer.Page;

  public SizeF Size => this.m_canvasSize;

  internal float Split
  {
    get => this.m_split;
    set => this.m_split = value;
  }

  internal PdfStreamWriter StreamWriter => this.m_streamWriter;

  internal PdfStringLayoutResult StringLayoutResult => this.m_stringLayoutResult;

  internal static bool TransparencyObject => PdfGraphics.m_transparencyObject;

  internal delegate PdfResources GetResources();

  private struct TransparencyData
  {
    internal float AlphaPen;
    internal float AlphaBrush;
    internal PdfBlendMode BlendMode;

    internal TransparencyData(float alphaPen, float alphaBrush, PdfBlendMode blendMode)
    {
      this.AlphaPen = alphaPen;
      this.AlphaBrush = alphaBrush;
      this.BlendMode = blendMode;
    }

    public override bool Equals(object obj)
    {
      bool flag = false;
      if (obj != null && obj is PdfGraphics.TransparencyData transparencyData)
        flag = true & (double) this.AlphaBrush == (double) transparencyData.AlphaBrush & (double) this.AlphaPen == (double) transparencyData.AlphaPen & this.BlendMode == transparencyData.BlendMode;
      return flag;
    }

    public override int GetHashCode() => base.GetHashCode();
  }
}
