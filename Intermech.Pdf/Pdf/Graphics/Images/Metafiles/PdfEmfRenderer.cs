// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.Images.Metafiles.PdfEmfRenderer
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Microsoft.Win32;
using Syncfusion.Pdf.Native;
using Syncfusion.Pdf.Primitives;
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;


namespace Syncfusion.Pdf.Graphics.Images.Metafiles
{
    internal class PdfEmfRenderer : IDisposable
    {
      private float m_alphaBrush;
      private float m_alphaPen;
      private bool m_bFirstCall;
      private bool m_bFirstTransform;
      private bool m_bIsTransparency;
      private PdfBlendMode m_blendMode;
      private PdfTransformationMatrix m_bounds;
      private bool m_bPageTransformed;
      private bool m_CloseShape;
      private object m_context;
      private PdfUnitConvertor m_convertX;
      private PdfUnitConvertor m_convertY;
      internal CustomLineCapArrowData m_customLineCapArrowData;
      private bool m_embedFonts;
      internal bool m_EMFState;
      private PdfGraphics m_graphics;
      private Hashtable m_graphicsStates;
      private System.Drawing.Graphics m_grCache;
      private int m_imageResolution;
      private bool m_isIntersectClipRect;
      internal EmfPlusRecordType m_previousRecordtype;
      private long m_quality;
      private RectangleF m_realClip;
      internal EmfPlusRecordType m_recordType;
      private PdfGraphicsState m_startState;
      private bool m_stateChanged;
      private bool m_stateRestored;
      private bool m_taggedPDF;
      private RectangleF m_textClip;
      private static Image s_bmp = (Image) new Bitmap(1, 1);
      private float TextAngleLocal;

      public PdfEmfRenderer(PdfGraphics graphics)
      {
        this.m_bFirstCall = true;
        this.m_graphicsStates = new Hashtable();
        this.m_bFirstTransform = true;
        this.m_quality = 100L;
        this.m_alphaPen = 1f;
        this.m_alphaBrush = 1f;
        this.m_graphics = graphics != null ? graphics : throw new ArgumentNullException(nameof (graphics));
      }

      internal PdfEmfRenderer(PdfGraphics graphics, PointF location, bool tagged)
        : this(graphics)
      {
        this.m_taggedPDF = tagged;
        this.Context = (object) new TextRegionManager();
      }

      public PdfEmfRenderer(PdfGraphics graphics, int imageResolution, bool embedFonts)
        : this(graphics)
      {
        this.m_imageResolution = imageResolution;
        this.m_embedFonts = embedFonts;
      }

      public PdfEmfRenderer(PdfGraphics graphics, long quality, bool embedFonts)
        : this(graphics)
      {
        this.m_quality = quality;
        this.m_embedFonts = embedFonts;
      }

      public void BeforeEnd()
      {
        if (this.m_startState != null)
        {
          this.Graphics.PutComment(nameof (BeforeEnd));
          this.Graphics.Restore(this.m_startState);
        }
        if (this.m_grCache == null)
          return;
        this.m_grCache.Dispose();
      }

      public void BeforeStart()
      {
        lock (PdfEmfRenderer.s_bmp)
          this.m_grCache = System.Drawing.Graphics.FromImage(PdfEmfRenderer.s_bmp);
        this.Graphics.PutComment(nameof (BeforeStart));
        this.m_startState = this.Graphics.Save();
        if (this.m_bounds == null)
          return;
        this.Graphics.MultiplyTransform(this.m_bounds);
      }

      public GraphicsContainer BeginContainer()
      {
        this.InternalResetClip();
        this.InternalResetTransformation();
        this.Graphics.PutComment("BegingContainer");
        PdfGraphicsState pdfGraphicsState = this.Graphics.Save();
        GraphicsContainer key = this.NativeGraphics.BeginContainer();
        this.m_graphicsStates[(object) key] = (object) pdfGraphicsState;
        this.m_bFirstTransform = true;
        return key;
      }

      public GraphicsContainer BeginContainer(
        RectangleF destRect,
        RectangleF srcRect,
        GraphicsUnit unit)
      {
        this.InternalResetClip();
        this.InternalResetTransformation();
        this.Graphics.PutComment("BegingContainer");
        PdfGraphicsState pdfGraphicsState = this.Graphics.Save();
        GraphicsContainer key = this.NativeGraphics.BeginContainer(destRect, srcRect, unit);
        this.m_graphicsStates[(object) key] = (object) pdfGraphicsState;
        return key;
      }

      private Image ChangeResolution(int value, Image image)
      {
        Bitmap bitmap = image as Bitmap;
        bitmap.SetResolution((float) value, (float) value);
        Stream stream = (Stream) new MemoryStream();
        ImageFormat rawFormat = image.RawFormat;
        if (rawFormat.Equals((object) ImageFormat.Jpeg) || rawFormat.Equals((object) ImageFormat.Gif))
          bitmap.Save(stream, ImageFormat.Jpeg);
        else if (rawFormat.Equals((object) ImageFormat.Png))
          bitmap.Save(stream, ImageFormat.Png);
        else if (rawFormat.Equals((object) ImageFormat.Bmp))
          bitmap.Save(stream, ImageFormat.Bmp);
        else if (rawFormat.Equals((object) ImageFormat.MemoryBmp))
          bitmap.Save(stream, ImageFormat.MemoryBmp);
        return stream.Length > 0L ? Image.FromStream(stream) : (Image) bitmap;
      }

      private PdfMask CheckAlpha(Bitmap bitmap)
      {
        PdfMask pdfMask = (PdfMask) null;
        switch (bitmap.PixelFormat)
        {
          case PixelFormat.Format1bppIndexed:
          case PixelFormat.Format4bppIndexed:
          case PixelFormat.Format8bppIndexed:
            System.Drawing.Color[] entries = bitmap.Palette.Entries;
            return this.CheckAlpha(bitmap.Palette.Flags, (Image) bitmap, entries);
          case PixelFormat.Format32bppPArgb:
          case PixelFormat.Format32bppArgb:
            return (PdfMask) new PdfImageMask(new PdfBitmap((Image) PdfBitmap.CreateMaskFromARGBImage((Image) bitmap)));
          default:
            return pdfMask;
        }
      }

      private PdfMask CheckAlpha(int flags, Image bitmap, System.Drawing.Color[] array)
      {
        PdfMask pdfMask = (PdfMask) null;
        bool flag = false;
        int index = 0;
        for (int length = array.Length; index < length; ++index)
        {
          if (flags == 1 && array[index].A < byte.MaxValue)
            flag = true;
        }
        if (flag)
          pdfMask = (PdfMask) new PdfImageMask(new PdfBitmap(PdfBitmap.CreateMaskFromIndexedImage(bitmap)));
        return pdfMask;
      }

      private bool CheckPdfPage(RectangleF rect, bool image)
      {
        if (this.m_taggedPDF && this.Graphics.Page is PdfPage)
        {
          float height = (this.Graphics.Page as PdfPage).GetClientSize().Height;
          float width = (this.Graphics.Page as PdfPage).GetClientSize().Width;
          PdfUnitConvertor pdfUnitConvertor = new PdfUnitConvertor();
          float pixels = pdfUnitConvertor.ConvertToPixels(height, PdfGraphicsUnit.Point);
          pdfUnitConvertor.ConvertToPixels(width, PdfGraphicsUnit.Point);
          if (!image)
            this.TextRegions.Add(new TextRegion(rect.Location.Y, rect.Height));
          if ((double) pixels < (double) rect.Bottom && (!image || (double) rect.Height <= (double) pixels))
          {
            this.m_graphics.Split = (double) this.m_graphics.Split <= 0.0 ? pdfUnitConvertor.ConvertFromPixels(rect.Y, PdfGraphicsUnit.Point) : Math.Min(this.m_graphics.Split, pdfUnitConvertor.ConvertFromPixels(rect.Y, PdfGraphicsUnit.Point));
            return false;
          }
        }
        return true;
      }

      public void Clear(System.Drawing.Color color)
      {
        this.NativeGraphics.Clear(color);
        using (Brush brush = (Brush) new SolidBrush(color))
        {
          RectangleF[] rects = new RectangleF[1]
          {
            this.NativeGraphics.ClipBounds
          };
          this.FillRectangles(brush, rects);
        }
      }

      internal static PdfTextAlignment ConvertAlingnmet(StringAlignment stringAlignment)
      {
        if (stringAlignment == StringAlignment.Center)
          return PdfTextAlignment.Center;
        return stringAlignment == StringAlignment.Far ? PdfTextAlignment.Right : PdfTextAlignment.Left;
      }

      private PdfBrush ConvertBrush(Brush brush)
      {
        PdfBlendMode blendMode = PdfBlendMode.Normal;
        float alpha;
        PdfBrush pdfBrush = this.ConvertBrush(brush, out alpha);
        if (this.IsTranparency)
        {
          this.Graphics.SetTransparency(this.AlphaPen, this.AlphaBrush, this.BlendMode);
          return pdfBrush;
        }
        this.Graphics.SetTransparency(alpha, alpha, blendMode);
        return pdfBrush;
      }

      private PdfBrush ConvertBrush(Brush brush, out float alpha)
      {
        SolidBrush solidBrush = brush as SolidBrush;
        TextureBrush textureBrush = brush as TextureBrush;
        LinearGradientBrush linearGradientBrush1 = brush as LinearGradientBrush;
        HatchBrush hatchBrush = brush as HatchBrush;
        PathGradientBrush pathGradientBrush = brush as PathGradientBrush;
        alpha = 1f;
        if (solidBrush != null)
        {
          PdfSolidBrush pdfSolidBrush = new PdfSolidBrush((PdfColor) solidBrush.Color);
          alpha = (float) solidBrush.Color.A / (float) byte.MaxValue;
          return (PdfBrush) pdfSolidBrush;
        }
        if (textureBrush != null)
        {
          Image image1 = textureBrush.Image;
          PdfImage image2 = PdfImage.FromImage(image1);
          PdfTilingBrush pdfTilingBrush = new PdfTilingBrush((SizeF) image1.Size);
          if (image2 is PdfBitmap)
          {
            Bitmap bitmap = image1 as Bitmap;
            PdfBitmap image3 = image2 as PdfBitmap;
            PdfMask pdfMask = this.CheckAlpha(bitmap);
            if (pdfMask != null)
              image3.Mask = pdfMask;
            pdfTilingBrush.Graphics.DrawImage((PdfImage) image3, PointF.Empty, (SizeF) image1.Size);
            return (PdfBrush) pdfTilingBrush;
          }
          pdfTilingBrush.Graphics.DrawImage(image2, PointF.Empty, (SizeF) image1.Size);
          return (PdfBrush) pdfTilingBrush;
        }
        if (linearGradientBrush1 != null)
        {
          RectangleF rectangle = linearGradientBrush1.Rectangle;
          System.Drawing.Color color1 = System.Drawing.Color.Empty;
          System.Drawing.Color color2 = System.Drawing.Color.Empty;
          System.Drawing.Color[] linearColors = linearGradientBrush1.LinearColors;
          ColorBlend colorBlend = (ColorBlend) null;
          if (linearColors != null)
          {
            color1 = linearColors[0];
            color2 = linearColors[1];
          }
          if (color1.R == (byte) 0 && color1.G == (byte) 0 && color1.B == (byte) 0 && linearGradientBrush1.WrapMode == WrapMode.TileFlipX)
          {
            color1 = linearColors[1];
            color2 = linearColors[0];
          }
          try
          {
            colorBlend = linearGradientBrush1.InterpolationColors;
            if (colorBlend != null)
            {
              color1 = colorBlend.Colors[0];
              color2 = color1;
            }
          }
          catch
          {
          }
          PdfLinearGradientBrush linearGradientBrush2;
          if ((double) linearGradientBrush1.Transform.OffsetX > 0.0)
          {
            linearGradientBrush2 = new PdfLinearGradientBrush(rectangle, (PdfColor) color1, (PdfColor) color2, PdfLinearGradientMode.Vertical);
          }
          else
          {
            linearGradientBrush2 = new PdfLinearGradientBrush(rectangle, (PdfColor) color1, (PdfColor) color2, PdfLinearGradientMode.Horizontal);
            linearGradientBrush2.Matrix = new PdfTransformationMatrix()
            {
              Matrix = linearGradientBrush1.Transform
            };
          }
          if (colorBlend != null)
          {
            System.Drawing.Color[] colors = colorBlend.Colors;
            PdfColorBlend pdfColorBlend = new PdfColorBlend(colors.Length);
            pdfColorBlend.Colors = PdfEmfRenderer.ConvertColors(colors);
            pdfColorBlend.Positions = colorBlend.Positions;
            linearGradientBrush2.InterpolationColors = pdfColorBlend;
          }
          else
          {
            Blend blend = linearGradientBrush1.Blend;
            if (blend != null)
            {
              PdfBlend pdfBlend = new PdfBlend();
              pdfBlend.Factors = blend.Factors;
              pdfBlend.Positions = blend.Positions;
              linearGradientBrush2.Blend = pdfBlend;
            }
          }
          if (linearGradientBrush1.WrapMode == WrapMode.Tile || linearGradientBrush1.WrapMode == WrapMode.TileFlipX)
            linearGradientBrush2.Extend = PdfExtend.Both;
          alpha = (float) color1.A / (float) byte.MaxValue;
          return (PdfBrush) linearGradientBrush2;
        }
        if (hatchBrush != null)
          return this.ConvertHatchBrush(hatchBrush, out alpha);
        if (pathGradientBrush == null)
          throw new ArgumentException("Unsupported brush type: " + (object) brush, nameof (brush));
        PdfSolidBrush pdfSolidBrush1 = new PdfSolidBrush((PdfColor) System.Drawing.Color.Black);
        alpha = (float) pathGradientBrush.CenterColor.A / (float) byte.MaxValue;
        return (PdfBrush) pdfSolidBrush1;
      }

      internal static PdfLineCap ConvertCaps(LineCap cap)
      {
        if (cap == LineCap.Square)
          return PdfLineCap.Square;
        return cap == LineCap.Round ? PdfLineCap.Round : PdfLineCap.Flat;
      }

      internal static PdfColor[] ConvertColors(System.Drawing.Color[] colors)
      {
        int length = colors.Length;
        PdfColor[] pdfColorArray = new PdfColor[length];
        for (int index = 0; index < length; ++index)
          pdfColorArray[index] = (PdfColor) colors[index];
        return pdfColorArray;
      }

      internal static PdfDashStyle ConvertDashStyle(DashStyle dashStyle)
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

      private PdfStringFormat ConvertFormat(StringFormat format)
      {
        PdfStringFormat pdfStringFormat = (PdfStringFormat) null;
        if (format != null)
        {
          this.Graphics.PutComment($"String Format Flags: {(object) format.FormatFlags}({(object) (int) format.FormatFlags})");
          this.Graphics.PutComment("Alignment: " + (object) format.Alignment);
          this.Graphics.PutComment("Line Alignment: " + (object) format.LineAlignment);
          pdfStringFormat = new PdfStringFormat();
          pdfStringFormat.LineLimit = false;
          pdfStringFormat.Alignment = PdfEmfRenderer.ConvertAlingnmet(format.Alignment);
          pdfStringFormat.LineAlignment = PdfEmfRenderer.CovertLineAlignment(format.LineAlignment);
          format.GetTabStops(out float _);
          pdfStringFormat.NoClip = true;
          pdfStringFormat.RightToLeft = (format.FormatFlags & StringFormatFlags.DirectionRightToLeft) != 0;
          if (pdfStringFormat.NoClip)
            pdfStringFormat.LineLimit = false;
          pdfStringFormat.WordWrap = this.GetWrapType(format.FormatFlags);
        }
        return pdfStringFormat;
      }

      private PdfBrush ConvertHatchBrush(HatchBrush hatchBrush, out float alpha)
      {
        System.Drawing.Color foregroundColor = hatchBrush.ForegroundColor;
        System.Drawing.Color backgroundColor = hatchBrush.BackgroundColor;
        SizeF sizeF = new SizeF(8f, 8f);
        PdfTilingBrush pdfTilingBrush = new PdfTilingBrush(sizeF);
        PdfGraphics graphics = pdfTilingBrush.Graphics;
        PdfPen pen = new PdfPen((PdfColor) foregroundColor, 1f);
        alpha = (float) foregroundColor.A / (float) byte.MaxValue;
        if (!backgroundColor.IsEmpty && backgroundColor.A != (byte) 0)
          graphics.DrawRectangle((PdfBrush) new PdfSolidBrush((PdfColor) backgroundColor), new RectangleF(PointF.Empty, sizeF));
        switch (hatchBrush.HatchStyle)
        {
          case HatchStyle.Horizontal:
            PdfEmfRenderer.DrawHorizontal(graphics, pen, sizeF);
            goto case HatchStyle.LargeConfetti;
          case HatchStyle.ForwardDiagonal:
            PdfEmfRenderer.DrawForwardDiagonal(graphics, pen, sizeF);
            goto case HatchStyle.LargeConfetti;
          case HatchStyle.BackwardDiagonal:
            PdfEmfRenderer.DrawBackwardDiagonal(graphics, pen, sizeF);
            goto case HatchStyle.LargeConfetti;
          case HatchStyle.Cross:
            PdfEmfRenderer.DrawCross(graphics, pen, sizeF);
            goto case HatchStyle.LargeConfetti;
          case HatchStyle.DiagonalCross:
            PdfEmfRenderer.DrawForwardDiagonal(graphics, pen, sizeF);
            PdfEmfRenderer.DrawBackwardDiagonal(graphics, pen, sizeF);
            goto case HatchStyle.LargeConfetti;
          case HatchStyle.LightDownwardDiagonal:
            PdfEmfRenderer.DrawDownwardDiagonal(graphics, pen, sizeF);
            goto case HatchStyle.LargeConfetti;
          case HatchStyle.LightUpwardDiagonal:
            PdfEmfRenderer.DrawUpwardDiagonal(graphics, pen, sizeF);
            goto case HatchStyle.LargeConfetti;
          case HatchStyle.DarkDownwardDiagonal:
            pen.Width = 2f;
            PdfEmfRenderer.DrawDownwardDiagonal(graphics, pen, sizeF);
            goto case HatchStyle.LargeConfetti;
          case HatchStyle.DarkUpwardDiagonal:
            pen.Width = 2f;
            PdfEmfRenderer.DrawUpwardDiagonal(graphics, pen, sizeF);
            goto case HatchStyle.LargeConfetti;
          case HatchStyle.LightVertical:
            PdfEmfRenderer.DrawVertical(graphics, pen, sizeF);
            goto case HatchStyle.LargeConfetti;
          case HatchStyle.LightHorizontal:
            PdfEmfRenderer.DrawHorizontal(graphics, pen, sizeF);
            goto case HatchStyle.LargeConfetti;
          case HatchStyle.DarkVertical:
            pen.Width = 2f;
            PdfEmfRenderer.DrawVertical(graphics, pen, sizeF);
            goto case HatchStyle.LargeConfetti;
          case HatchStyle.DarkHorizontal:
            pen.Width = 2f;
            PdfEmfRenderer.DrawHorizontal(graphics, pen, sizeF);
            goto case HatchStyle.LargeConfetti;
          case HatchStyle.DashedDownwardDiagonal:
            pen.DashStyle = PdfDashStyle.Dash;
            PdfEmfRenderer.DrawDownwardDiagonal(graphics, pen, sizeF);
            goto case HatchStyle.LargeConfetti;
          case HatchStyle.DashedUpwardDiagonal:
            pen.DashStyle = PdfDashStyle.Dash;
            PdfEmfRenderer.DrawUpwardDiagonal(graphics, pen, sizeF);
            goto case HatchStyle.LargeConfetti;
          case HatchStyle.DashedHorizontal:
            pen.DashStyle = PdfDashStyle.Dash;
            PdfEmfRenderer.DrawHorizontal(graphics, pen, sizeF);
            goto case HatchStyle.LargeConfetti;
          case HatchStyle.DashedVertical:
            pen.DashStyle = PdfDashStyle.Dash;
            PdfEmfRenderer.DrawVertical(graphics, pen, sizeF);
            goto case HatchStyle.LargeConfetti;
          case HatchStyle.LargeConfetti:
          case HatchStyle.Divot:
            return (PdfBrush) pdfTilingBrush ?? (PdfBrush) new PdfSolidBrush((PdfColor) foregroundColor);
          case HatchStyle.DiagonalBrick:
            PdfEmfRenderer.DrawForwardDiagonal(graphics, pen, sizeF);
            PdfEmfRenderer.DrawBrickTails(graphics, pen, sizeF);
            goto case HatchStyle.LargeConfetti;
          case HatchStyle.HorizontalBrick:
            PdfEmfRenderer.DrawHorizontalBrick(graphics, pen, sizeF);
            goto case HatchStyle.LargeConfetti;
          case HatchStyle.Weave:
            this.DrawWeave(graphics, pen, sizeF);
            goto case HatchStyle.LargeConfetti;
          case HatchStyle.DottedGrid:
            pen.DashStyle = PdfDashStyle.Dot;
            PdfEmfRenderer.DrawCross(graphics, pen, sizeF);
            goto case HatchStyle.LargeConfetti;
          case HatchStyle.DottedDiamond:
            pen.DashStyle = PdfDashStyle.Dot;
            PdfEmfRenderer.DrawForwardDiagonal(graphics, pen, sizeF);
            PdfEmfRenderer.DrawBackwardDiagonal(graphics, pen, sizeF);
            goto case HatchStyle.LargeConfetti;
          case HatchStyle.LargeCheckerBoard:
            PdfEmfRenderer.DrawCheckerBoard(graphics, pen, sizeF, 4);
            goto case HatchStyle.LargeConfetti;
          default:
            alpha = 0.5f;
            pdfTilingBrush = (PdfTilingBrush) null;
            goto case HatchStyle.LargeConfetti;
        }
      }

      internal static PdfLineJoin ConvertJoin(LineJoin join)
      {
        if (join == LineJoin.Bevel)
          return PdfLineJoin.Bevel;
        return join == LineJoin.Round ? PdfLineJoin.Round : PdfLineJoin.Miter;
      }

      private PdfPen ConvertPen(Pen pen) => this.ConvertPen(pen, false);

      private PdfPen ConvertPen(Pen pen, bool rotate)
      {
        float alpha;
        PdfPen pdfPen = this.ConvertPen(pen, out alpha, rotate);
        if ((double) alpha == 0.0)
          return new PdfPen(PdfBrushes.White);
        if (this.IsTranparency)
        {
          this.Graphics.SetTransparency(this.AlphaPen, this.AlphaBrush, this.BlendMode);
          return pdfPen;
        }
        this.Graphics.SetTransparency(alpha, alpha, PdfBlendMode.Normal);
        return pdfPen;
      }

      private PdfPen ConvertPen(Pen pen, out float alpha, bool rotate)
      {
        PdfPen pdfPen;
        try
        {
          pdfPen = new PdfPen((PdfColor) pen.Color);
        }
        catch (ArgumentException ex)
        {
          pdfPen = new PdfPen((PdfColor) System.Drawing.Color.Empty);
        }
        pdfPen.DashStyle = PdfEmfRenderer.ConvertDashStyle(pen.DashStyle);
        if (pdfPen.DashStyle != PdfDashStyle.Solid)
        {
          pdfPen.DashOffset = pen.DashOffset;
          pdfPen.DashPattern = pen.DashPattern;
        }
        pdfPen.LineCap = PdfEmfRenderer.ConvertCaps(pen.StartCap);
        pdfPen.LineCap = PdfEmfRenderer.ConvertCaps(pen.EndCap);
        pdfPen.LineJoin = PdfEmfRenderer.ConvertJoin(pen.LineJoin);
        pdfPen.MiterLimit = pen.MiterLimit;
        pdfPen.Width = pen.Width;
        try
        {
          alpha = (float) pen.Color.A / (float) byte.MaxValue;
        }
        catch (ArgumentException ex)
        {
          alpha = 0.0f;
        }
        if (pen.Brush != null)
        {
          float alpha1;
          PdfBrush brush = this.ConvertBrush(pen.Brush, out alpha1);
          if (brush is PdfSolidBrush pdfSolidBrush && pdfSolidBrush.Color.A != (byte) 0 || pdfSolidBrush == null)
          {
            pdfPen.Brush = brush;
            alpha = alpha1;
          }
          if (pen.CompoundArray.Length != 0)
          {
            double width1 = (double) pen.Width;
            float width2 = pen.Width;
            float num = (float) Math.Pow(2.0, Math.Round(Math.Log((double) pen.Width, 2.0)) + 1.0);
            PdfTilingBrush pdfTilingBrush = new PdfTilingBrush(new SizeF(num, num));
            PdfPen pen1 = new PdfPen(brush);
            pen1.Width = (pen.CompoundArray[1] - pen.CompoundArray[0]) * width2;
            if (!rotate)
              pdfTilingBrush.Graphics.DrawLine(pen1, 0.0f, pen1.Width, num, pen1.Width);
            else
              pdfTilingBrush.Graphics.DrawLine(pen1, 0.0f, pen1.Width, 0.0f, num);
            pen1.Width = (pen.CompoundArray[1] - pen.CompoundArray[0]) * width2;
            if (!rotate)
            {
              pdfTilingBrush.Graphics.TranslateTransform(0.0f, pen.Width);
              pdfTilingBrush.Graphics.DrawLine(pen1, 0.0f, pen1.Width / 2f, num, pen1.Width / 2f);
            }
            else
            {
              pdfTilingBrush.Graphics.TranslateTransform(pen.Width, 0.0f);
              pdfTilingBrush.Graphics.DrawLine(pen1, pen1.Width / 2f, pen1.Width, pen1.Width / 2f, num);
            }
            pdfPen.Brush = (PdfBrush) pdfTilingBrush;
          }
        }
        return pdfPen;
      }

      private PdfPen ConvertToPen(Pen pen, out float alpha)
      {
        PdfPen pen1;
        try
        {
          pen1 = new PdfPen((PdfColor) pen.Color);
        }
        catch (ArgumentException ex)
        {
          pen1 = new PdfPen((PdfColor) System.Drawing.Color.Empty);
        }
        pen1.DashStyle = PdfEmfRenderer.ConvertDashStyle(pen.DashStyle);
        if (pen1.DashStyle != PdfDashStyle.Solid)
        {
          pen1.DashOffset = pen.DashOffset;
          pen1.DashPattern = pen.DashPattern;
        }
        pen1.LineCap = PdfEmfRenderer.ConvertCaps(pen.StartCap);
        pen1.LineCap = PdfEmfRenderer.ConvertCaps(pen.EndCap);
        pen1.LineJoin = PdfEmfRenderer.ConvertJoin(pen.LineJoin);
        pen1.MiterLimit = pen.MiterLimit;
        pen1.Width = pen.Width;
        try
        {
          alpha = (float) pen.Color.A / (float) byte.MaxValue;
        }
        catch (ArgumentException ex)
        {
          alpha = 0.0f;
        }
        if (pen.Brush != null)
        {
          float alpha1;
          PdfBrush pdfBrush = this.ConvertBrush(pen.Brush, out alpha1);
          if ((!(pdfBrush is PdfSolidBrush pdfSolidBrush) || pdfSolidBrush.Color.A == (byte) 0) && pdfSolidBrush != null)
            return pen1;
          pen1.Brush = pdfBrush;
          alpha = alpha1;
        }
        return pen1;
      }

      private PointF CorrectLocation(
        PointF location,
        SizeF size,
        SizeF realSize,
        PdfStringFormat format)
      {
        PointF pointF = location;
        if ((double) this.TextAngleLocal == 0.0)
        {
          switch (format.Alignment)
          {
            case PdfTextAlignment.Center:
              pointF.X += size.Width / 2f;
              break;
            case PdfTextAlignment.Right:
              if ((double) size.Width > (double) realSize.Width)
              {
                pointF.X += size.Width - realSize.Width;
                break;
              }
              break;
          }
          switch (format.LineAlignment)
          {
            case PdfVerticalAlignment.Top:
              return pointF;
            case PdfVerticalAlignment.Middle:
              pointF.Y += size.Height / 2f;
              return pointF;
            case PdfVerticalAlignment.Bottom:
              if ((double) size.Height > (double) realSize.Height)
                pointF.Y += size.Height - realSize.Height;
              return pointF;
          }
        }
        return pointF;
      }

      internal static PdfVerticalAlignment CovertLineAlignment(StringAlignment stringAlignment)
      {
        if (stringAlignment == StringAlignment.Near)
          return PdfVerticalAlignment.Top;
        return stringAlignment == StringAlignment.Far ? PdfVerticalAlignment.Bottom : PdfVerticalAlignment.Middle;
      }

      public void Dispose()
      {
        this.m_graphics = (PdfGraphics) null;
        this.m_bFirstCall = true;
        this.m_grCache = (System.Drawing.Graphics) null;
      }

      public void DrawArc(Pen pen, RectangleF rect, float startAngle, float sweepAngle)
      {
        if (pen == null)
          throw new ArgumentNullException(nameof (pen));
        this.OnDrawPrimitive();
        this.Graphics.DrawArc(this.ConvertPen(pen), rect, startAngle, sweepAngle);
      }

      private static void DrawBackwardDiagonal(PdfGraphics graphics, PdfPen pen, SizeF brushSize)
      {
        graphics.DrawLine(pen, brushSize.Width, 0.0f, 0.0f, brushSize.Height);
        graphics.DrawLine(pen, -1f, 1f, 1f, -1f);
        graphics.DrawLine(pen, brushSize.Width - 1f, brushSize.Height + 1f, brushSize.Width + 1f, brushSize.Height - 1f);
      }

      public void DrawBeziers(Pen pen, PointF[] points)
      {
        if (pen == null)
          throw new ArgumentNullException(nameof (pen));
        if (points == null)
          throw new ArgumentNullException(nameof (points));
        if (points.Length < 4)
          throw new ArgumentException("Incorrect size of array", nameof (points));
        this.OnDrawPrimitive();
        PdfPen pen1 = this.ConvertPen(pen);
        int num = 3;
        int index1 = 0;
        PointF startPoint = points[index1];
        int index2 = index1 + 1;
        while (index2 + num <= points.Length)
        {
          PointF point1 = points[index2];
          int index3 = index2 + 1;
          PointF point2 = points[index3];
          int index4 = index3 + 1;
          PointF point3 = points[index4];
          index2 = index4 + 1;
          this.Graphics.DrawBezier(pen1, startPoint, point1, point2, point3);
          startPoint = point3;
        }
      }

      private static void DrawBrickTails(PdfGraphics graphics, PdfPen pen, SizeF brushSize)
      {
        float x1 = brushSize.Width / 2f;
        float y1 = brushSize.Height / 2f;
        graphics.DrawLine(pen, x1, y1, brushSize.Width, brushSize.Height);
      }

      private void DrawCap(
        LineCap cap,
        PointF[] points,
        int startPointIndex,
        int endPointIndex,
        float width,
        PdfBrush brush)
      {
        switch (cap)
        {
          case LineCap.Round:
            SizeF size = new SizeF(width, width);
            RectangleF rectangle = new RectangleF(points[endPointIndex], size);
            this.Graphics.DrawEllipse(brush, rectangle);
            break;
          case LineCap.Triangle:
            PointF point1 = points[endPointIndex];
            PointF point2 = points[startPointIndex];
            double x1 = (double) point1.X;
            float y1 = point1.Y;
            double num1 = x1 - (double) point2.X;
            float num2 = y1 - point2.Y;
            float num3 = (float) Math.Sqrt(num1 * num1 + (double) num2 * (double) num2);
            double num4 = num1 / (double) num3;
            float num5 = num2 / num3 * width;
            double num6 = (double) width;
            float num7 = (float) (num4 * num6);
            float x2 = (float) x1 - num5;
            float y2 = y1 + num7;
            float x3 = (float) x1 + num5;
            float y3 = y1 - num7;
            float x4 = (float) x1 + num7;
            float y4 = y1 + num5;
            PointF pointF1 = new PointF(x3, y3);
            PointF pointF2 = new PointF(x2, y2);
            PointF pointF3 = new PointF(x4, y4);
            PointF[] points1 = new PointF[3]
            {
              pointF1,
              pointF3,
              pointF2
            };
            this.Graphics.DrawPolygon(brush, points1);
            break;
          case LineCap.Custom:
            if ((double) this.m_customLineCapArrowData.width == 0.0)
              break;
            this.DrawCustomLineCapArrow(cap, points, startPointIndex, endPointIndex, width, brush);
            this.m_customLineCapArrowData.Reset();
            break;
        }
      }

      private static void DrawCheckerBoard(
        PdfGraphics graphics,
        PdfPen pen,
        SizeF brushSize,
        int cellSize)
      {
        int num1 = (int) ((double) brushSize.Width / (double) cellSize);
        int num2 = (int) ((double) brushSize.Height / (double) cellSize);
        PdfSolidBrush brush = new PdfSolidBrush(pen.Color);
        for (int index1 = 0; index1 < num2; ++index1)
        {
          float y = (float) (index1 * cellSize);
          for (int index2 = 0; index2 < num1; ++index2)
          {
            float x = (float) (index2 * cellSize);
            graphics.DrawRectangle((PdfBrush) brush, x, y, (float) cellSize, (float) cellSize);
          }
        }
      }

      public void DrawClosedCurve(Pen pen, PointF[] points, float tension, PdfFillMode fillMode)
      {
        if (pen == null)
          throw new ArgumentNullException(nameof (pen));
        this.Graphics.DrawPolygon(this.ConvertPen(pen), points);
      }

      private void DrawCompoundLine(Pen pen, PointF[] points, bool rotate, PdfPen pdfPen)
      {
        float num = 0.0f;
        for (int index = 0; index < pen.CompoundArray.Length; index += 2)
        {
          float width = pen.Width;
          pdfPen.Width = (pen.CompoundArray[index + 1] - pen.CompoundArray[index]) * width;
          if (!rotate)
            this.Graphics.DrawLine(pdfPen, points[0].X, points[0].Y + num, points[1].X, points[1].Y + num);
          else
            this.Graphics.DrawLine(pdfPen, points[0].X + num, points[0].Y, points[1].X + num, points[1].Y);
          if (index + 1 < pen.CompoundArray.Length - 1)
            num += (pen.CompoundArray[index + 2] - pen.CompoundArray[index + 1]) * width + pdfPen.Width;
        }
      }

      private static void DrawCross(PdfGraphics graphics, PdfPen pen, SizeF brushSize)
      {
        float num1 = brushSize.Width / 2f;
        float num2 = brushSize.Height / 2f;
        graphics.DrawLine(pen, num1, 0.0f, num1, brushSize.Height);
        graphics.DrawLine(pen, 0.0f, num2, brushSize.Width, num2);
      }

      public void DrawCurve(
        Pen pen,
        PointF[] points,
        PointF[] penPoints,
        int offset,
        int numSegments,
        float tension)
      {
        GraphicsPath path = new GraphicsPath();
        if (!this.IsLine(points))
          path.AddCurve(points, tension);
        else
          path.AddLines(points);
        this.DrawPath(pen, path);
        this.DrawCustomCap(pen, points, penPoints, false);
      }

      private void DrawCustomCap(Pen pen, PointF[] points, PointF[] penPoints, bool isStartCap)
      {
        if (penPoints == null)
          return;
        PdfGraphicsState state = this.Graphics.Save();
        PointF empty1 = PointF.Empty;
        PointF empty2 = PointF.Empty;
        PointF point1;
        PointF point2;
        if (isStartCap)
        {
          point1 = points[1];
          point2 = points[0];
        }
        else
        {
          point1 = points[points.Length - 2];
          point2 = points[points.Length - 1];
        }
        float num1 = point1.X - point2.X;
        float num2 = point1.Y - point2.Y;
        float num3 = (float) Math.Sqrt((double) num1 * (double) num1 + (double) num2 * (double) num2);
        double num4 = (double) num1 / (double) num2;
        float num5 = (float) Math.Atan((double) num2 / (double) num1);
        if ((double) num1 / (double) num3 < 0.0)
          num5 += 3.141593f;
        this.Graphics.TranslateTransform(point2.X, point2.Y);
        this.Graphics.RotateTransform((float) ((double) num5 * 180.0 / Math.PI) + 90f);
        PointF[] pointFArray = new PointF[penPoints.Length];
        GraphicsPath path = new GraphicsPath();
        float num6 = pen.Width / 2f;
        pointFArray[0].X = penPoints[0].X * (pen.Width - num6);
        pointFArray[0].Y = penPoints[0].Y * pen.Width;
        for (int index = 1; index < penPoints.Length; ++index)
        {
          pointFArray[index].X = penPoints[index].X * (pen.Width - num6);
          pointFArray[index].Y = penPoints[index].Y * pen.Width;
          path.AddLine(pointFArray[index - 1], pointFArray[index]);
        }
        pen.Width = num6;
        this.DrawPath(pen, path);
        this.Graphics.Restore(state);
      }

      private void DrawCustomLineCapArrow(
        LineCap cap,
        PointF[] points,
        int startPointIndex,
        int endPointIndex,
        float width,
        PdfBrush brush)
      {
        PointF point1 = points[endPointIndex];
        PointF point2 = points[startPointIndex];
        double num1 = Math.Cos(Math.PI / 6.0);
        double num2 = Math.Sin(Math.PI / 6.0);
        float x = point1.X;
        float y = point1.Y;
        double num3 = (double) x - (double) point2.X;
        float num4 = y - point2.Y;
        float num5 = (float) Math.Sqrt(num3 * num3 + (double) num4 * (double) num4);
        double num6 = num3 / (double) num5;
        float num7 = num4 / num5 * this.m_customLineCapArrowData.width;
        double width1 = (double) this.m_customLineCapArrowData.width;
        float num8 = (float) (num6 * width1);
        PointF point2_1 = new PointF(x - (float) ((double) num8 * num1 + (double) num7 * -num2), y - (float) ((double) num8 * num2 + (double) num7 * num1));
        PointF point2_2 = new PointF(x - (float) ((double) num8 * num1 + (double) num7 * num2), y - (float) ((double) num8 * -num2 + (double) num7 * num1));
        if (this.m_customLineCapArrowData.fillState == 0)
        {
          PdfPen pen = new PdfPen(brush);
          this.Graphics.DrawLine(pen, new PointF(x, y), point2_1);
          this.Graphics.DrawLine(pen, new PointF(x, y), point2_2);
        }
        else
        {
          if (this.m_customLineCapArrowData.fillState != 1)
            return;
          PointF[] points1 = new PointF[3]
          {
            new PointF(x, y),
            point2_1,
            point2_2
          };
          this.Graphics.DrawPolygon(brush, points1);
        }
      }

      private static void DrawDownwardDiagonal(PdfGraphics graphics, PdfPen pen, SizeF brushSize)
      {
        float num1 = brushSize.Height / 2f;
        float num2 = brushSize.Width / 2f;
        graphics.DrawLine(pen, 0.0f, 0.0f, brushSize.Width, brushSize.Height);
        graphics.DrawLine(pen, 0.0f, num1, num2, brushSize.Height);
        graphics.DrawLine(pen, num2, 0.0f, brushSize.Width, num1);
        graphics.DrawLine(pen, -1f, -1f, 1f, 1f);
        graphics.DrawLine(pen, brushSize.Width - 1f, brushSize.Height - 1f, brushSize.Width + 1f, brushSize.Height + 1f);
      }

      public void DrawEllipse(Pen pen, RectangleF rect)
      {
        if (pen == null)
          throw new ArgumentNullException(nameof (pen));
        this.OnDrawPrimitive();
        this.Graphics.DrawEllipse(this.ConvertPen(pen), rect);
      }

      private static void DrawForwardDiagonal(PdfGraphics graphics, PdfPen pen, SizeF brushSize)
      {
        graphics.DrawLine(pen, 0.0f, 0.0f, brushSize.Width, brushSize.Height);
        graphics.DrawLine(pen, -1f, -1f, 1f, 1f);
        graphics.DrawLine(pen, brushSize.Width - 1f, brushSize.Height - 1f, brushSize.Width + 1f, brushSize.Height + 1f);
      }

      private static void DrawHorizontal(PdfGraphics graphics, PdfPen pen, SizeF brushSize)
      {
        float num1 = 0.0f;
        float num2 = brushSize.Height / 2f;
        float height = brushSize.Height;
        graphics.DrawLine(pen, 0.0f, num1, brushSize.Width, num1);
        graphics.DrawLine(pen, 0.0f, num2, brushSize.Width, num2);
        graphics.DrawLine(pen, 0.0f, height, brushSize.Width, height);
      }

      private static void DrawHorizontalBrick(PdfGraphics graphics, PdfPen pen, SizeF brushSize)
      {
        float num1 = brushSize.Width / 2f;
        float num2 = brushSize.Height / 2f;
        graphics.DrawLine(pen, 0.0f, 0.0f, brushSize.Width, 0.0f);
        graphics.DrawLine(pen, 0.0f, brushSize.Height, brushSize.Width, brushSize.Height);
        graphics.DrawLine(pen, 0.0f, num2, brushSize.Width, num2);
        graphics.DrawLine(pen, num1, 0.0f, num1, num2);
        graphics.DrawLine(pen, 0.0f, num2, 0.0f, brushSize.Height);
        graphics.DrawLine(pen, brushSize.Width, num2, brushSize.Width, brushSize.Height);
      }

      public void DrawImage(Image image, RectangleF destRect, RectangleF srcRect, GraphicsUnit units)
      {
        if (image == null)
          throw new ArgumentNullException(nameof (image));
        if (this.m_taggedPDF && !this.CheckPdfPage(destRect, true))
          return;
        this.OnDrawPrimitive();
        PdfImage image1 = PdfImage.FromImage(image);
        if (image is Bitmap)
        {
          if (this.m_imageResolution > 0)
          {
            Image image2 = this.ChangeResolution(this.m_imageResolution, image);
            if (image2 != null)
              this.Graphics.DrawImage((PdfImage) new PdfBitmap(image2), destRect);
            else
              this.Graphics.DrawImage((PdfImage) new PdfBitmap(image), destRect);
          }
          else
          {
            (image1 as PdfBitmap).Quality = this.m_quality;
            this.Graphics.DrawImage(image1, destRect);
          }
        }
        this.Graphics.DrawImage(image1, destRect);
      }

      public void DrawImage(Image image, PointF[] points, RectangleF srcRect, GraphicsUnit units)
      {
        if (image == null)
          throw new ArgumentNullException(nameof (image));
        if (points.Length < 0 || points.Length > 3)
          throw new ArgumentOutOfRangeException(nameof (points), (object) points, "Value can not be less 0 and greater 3");
        RectangleF rectangleF = new RectangleF(Math.Min(points[0].X, points[2].X), points[0].Y, points[1].X - points[0].X, points[2].Y - points[0].Y);
        if (this.m_taggedPDF && !this.CheckPdfPage(rectangleF, true))
          return;
        this.OnDrawPrimitive();
        PdfImage image1 = PdfImage.FromImage(image);
        if (image is Bitmap)
        {
          if (this.m_imageResolution > 0)
          {
            Image image2 = this.ChangeResolution(this.m_imageResolution, image);
            if (image2 != null)
              this.Graphics.DrawImage((PdfImage) new PdfBitmap(image2), rectangleF);
            else
              this.Graphics.DrawImage((PdfImage) new PdfBitmap(image), rectangleF);
          }
          else
          {
            (image1 as PdfBitmap).Quality = this.m_quality;
            this.Graphics.DrawImage(image1, rectangleF);
          }
        }
        else
          this.Graphics.DrawImage(image1, rectangleF);
      }

      public void DrawImage(
        Image image,
        Brush brush,
        RectangleF destRect,
        RectangleF srcRect,
        uint dwRop)
      {
        this.Graphics.PutComment(nameof (DrawImage));
        this.OnDrawPrimitive();
        this.ConvertBrush(brush);
        PdfBitmap image1 = (PdfBitmap) null;
        if (image != null)
        {
          image1 = (PdfBitmap) PdfImage.FromImage(image);
          image1.Quality = this.m_quality;
        }
        switch ((RASTER_CODE) dwRop)
        {
          case RASTER_CODE.SRCINVERT:
            if (image == null)
              break;
            this.Graphics.Save();
            this.Graphics.SetTransparency(0.1f);
            this.Graphics.DrawImage((PdfImage) image1, destRect);
            this.Graphics.Restore();
            break;
          case RASTER_CODE.SRCAND:
            if (image == null)
              break;
            this.Graphics.Save();
            this.Graphics.SetTransparency(1.1f, 1.1f, PdfBlendMode.Multiply);
            this.Graphics.DrawImage((PdfImage) image1, destRect);
            this.Graphics.Restore();
            break;
          case RASTER_CODE.SRCANDDST:
            PdfBrush brush1 = this.ConvertBrush(brush);
            this.Graphics.Save();
            this.Graphics.SetTransparency(0.1f);
            this.Graphics.DrawRectangle(brush1, destRect);
            this.Graphics.Restore();
            break;
          case RASTER_CODE.SRCCOPY:
            if (image == null)
              break;
            this.DrawImage(image, destRect, srcRect, GraphicsUnit.Pixel);
            break;
          case RASTER_CODE.SRCPAINT:
            if (image == null)
              break;
            this.Graphics.Save();
            Bitmap bitmap = new Bitmap(image);
            bitmap.MakeTransparent(System.Drawing.Color.Black);
            MemoryStream memoryStream = new MemoryStream();
            bitmap.Save((Stream) memoryStream, ImageFormat.Png);
            PdfImage image2 = PdfImage.FromStream((Stream) memoryStream);
            memoryStream.Dispose();
            this.Graphics.DrawImage(image2, destRect);
            this.Graphics.Restore();
            break;
          case RASTER_CODE.PATCOPY:
            RectangleF[] rects = new RectangleF[1]{ destRect };
            this.FillRectangles(brush, rects);
            break;
          default:
            if (image != null)
            {
              this.Graphics.Save();
              this.Graphics.SetTransparency(1f);
              this.Graphics.DrawImage((PdfImage) image1, destRect);
              this.Graphics.Restore();
              break;
            }
            PdfBrush brush2 = this.ConvertBrush(brush);
            this.Graphics.Save();
            this.Graphics.SetTransparency(0.5f);
            this.Graphics.DrawRectangle(brush2, destRect);
            this.Graphics.Restore();
            break;
        }
      }

      public void DrawLines(Pen pen, PointF[] points)
      {
        if (pen == null)
          throw new ArgumentNullException(nameof (pen));
        int num = points != null ? points.Length : throw new ArgumentNullException(nameof (points));
        if (num < 2)
          throw new ArgumentException("Incorrect size of array", nameof (points));
        this.OnDrawPrimitive();
        bool rotate = false;
        if (points.Length > 1 && pen.CompoundArray.Length != 0 && (double) points[0].X == (double) points[1].X)
          rotate = true;
        float alpha;
        PdfPen pdfPen = this.ConvertToPen(pen, out alpha);
        if ((double) alpha == 0.0)
          pdfPen = new PdfPen(PdfBrushes.White);
        else if (this.IsTranparency)
          this.Graphics.SetTransparency(this.AlphaPen, this.AlphaBrush, this.BlendMode);
        else
          this.Graphics.SetTransparency(alpha, alpha, PdfBlendMode.Normal);
        PointF point1 = points[0];
        if (pen.CompoundArray.Length != 0)
        {
          this.DrawCompoundLine(pen, points, rotate, pdfPen);
        }
        else
        {
          for (int index = 1; index < num; ++index)
          {
            PointF point = points[index];
            this.Graphics.DrawLine(pdfPen, point1, point);
            point1 = point;
          }
        }
        if (this.m_CloseShape)
          this.Graphics.DrawLine(pdfPen, points[num - 1], points[0]);
        PdfBrush brushFromPen = this.GetBrushFromPen(pdfPen);
        float width = pen.Width / 2f;
        LineCap endCap = pen.EndCap;
        if (num > 1 || endCap == LineCap.Round)
          this.DrawCap(endCap, points, num - 2, num - 1, width, brushFromPen);
        LineCap startCap = pen.StartCap;
        if (num <= 1 && startCap != LineCap.Round)
          return;
        this.DrawCap(startCap, points, 0, 1, width, brushFromPen);
      }

      internal void DrawLines(Pen pen, PointF[] points, bool closeShape)
      {
        this.m_CloseShape = closeShape;
        this.DrawLines(pen, points);
        this.m_CloseShape = false;
      }

      public void DrawPath(Pen pen, GraphicsPath path)
      {
        if (pen == null)
          throw new ArgumentNullException(nameof (pen));
        if (path == null)
          throw new ArgumentNullException(nameof (path));
        this.OnDrawPrimitive();
        this.Graphics.DrawPath(this.ConvertPen(pen), new PdfPath(path.PathPoints, path.PathTypes));
      }

      public void DrawPie(Pen pen, RectangleF rect, float startAngle, float sweepAngle)
      {
        if (pen == null)
          throw new ArgumentNullException(nameof (pen));
        this.OnDrawPrimitive();
        this.Graphics.DrawPie(this.ConvertPen(pen), rect, startAngle, sweepAngle);
      }

      public void DrawPolygon(Pen pen, PointF[] points)
      {
        if (pen == null)
          throw new ArgumentNullException(nameof (pen));
        if (points == null)
          throw new ArgumentNullException(nameof (points));
        this.OnDrawPrimitive();
        this.Graphics.DrawPolygon(this.ConvertPen(pen), points);
      }

      public void DrawRectangles(Pen pen, RectangleF[] rects)
      {
        if (pen == null)
          throw new ArgumentNullException(nameof (pen));
        if (rects == null)
          throw new ArgumentNullException(nameof (rects));
        this.OnDrawPrimitive();
        PdfPen pen1 = this.ConvertPen(pen);
        for (int index = 0; index < rects.Length; ++index)
        {
          RectangleF rect = rects[index];
          this.Graphics.DrawRectangle(pen1, rect);
        }
      }

      public void DrawString(string text, Font font, Brush brush, RectangleF rect)
      {
        if (text == null)
          throw new ArgumentNullException(nameof (text));
        if (font == null)
          throw new ArgumentNullException(nameof (font));
        if (brush == null)
          throw new ArgumentNullException(nameof (brush));
        this.OnDrawPrimitive();
        PdfBrush brush1 = this.ConvertBrush(brush);
        PdfFont pdfFont;
        try
        {
          pdfFont = this.GetPdfFont(text, font);
        }
        catch (Exception ex)
        {
          pdfFont = (PdfFont) new PdfTrueTypeFont(this.GetInstalledFontLocation(font), font.Size, (PdfFontStyle) font.Style);
        }
        SizeF textSize;
        float num = this.ScaleText(text, pdfFont, rect, out textSize, (PdfStringFormat) null);
        if (this.m_taggedPDF && !this.CheckPdfPage(new RectangleF(rect.Location, new SizeF(rect.Width, Math.Max(rect.Height, pdfFont.Height))), false))
          return;
        if ((double) num != 1.0)
        {
          PdfStringFormat format = new PdfStringFormat();
          format.HorizontalScalingFactor = num * 100f;
          rect.Width /= num;
          if ((double) rect.Width == 0.0 && (double) rect.Height == 0.0)
          {
            PointF point = this.CorrectLocation(rect.Location, rect.Size, textSize, format);
            this.Graphics.DrawString(text, pdfFont, brush1, point, format);
          }
          else
          {
            if ((double) rect.Height == 0.0)
              rect.Height = (float) font.Height;
            this.Graphics.DrawString(text, pdfFont, brush1, rect, format);
          }
        }
        else if ((double) rect.Width == 0.0 && (double) rect.Height == 0.0)
        {
          PointF point = this.CorrectLocation(rect.Location, rect.Size, textSize, new PdfStringFormat());
          this.Graphics.DrawString(text, pdfFont, brush1, point);
        }
        else
        {
          if ((double) rect.Height == 0.0)
            rect.Height = (float) font.Height;
          this.Graphics.DrawString(text, pdfFont, brush1, rect);
        }
      }

      public void DrawString(
        string text,
        Font font,
        Brush brush,
        RectangleF rect,
        StringFormat format)
      {
        if (text == null)
          throw new ArgumentNullException(nameof (text));
        if (font == null)
          throw new ArgumentNullException(nameof (font));
        if (brush == null)
          throw new ArgumentNullException(nameof (brush));
        if (format == null)
          throw new ArgumentNullException(nameof (format));
        if (this.m_recordType == EmfPlusRecordType.EmfExtTextOutW && this.m_EMFState)
        {
          if ((double) this.Transform.Elements[3] == -1.0)
            this.Graphics.TranslateTransform(0.0f, 0.0f, this.m_EMFState);
          this.m_EMFState = false;
        }
        this.OnDrawPrimitive();
        PdfBrush brush1 = this.ConvertBrush(brush);
        PdfStringFormat format1 = this.ConvertFormat(format);
        PdfFont pdfFont;
        try
        {
          pdfFont = this.GetPdfFont(text, font);
        }
        catch (Exception ex)
        {
          pdfFont = (PdfFont) new PdfTrueTypeFont(this.GetInstalledFontLocation(font), font.Size, (PdfFontStyle) font.Style);
        }
        SizeF textSize = rect.Size;
        float num1 = this.ScaleText(text, pdfFont, rect, out textSize, format1);
        if ((double) num1 != 1.0)
        {
          if (format1 == null)
            format1 = new PdfStringFormat();
          if (this.m_isIntersectClipRect)
          {
            if (this.m_previousRecordtype != EmfPlusRecordType.EmfIntersectClipRect)
              format1.HorizontalScalingFactor = num1 * 100f;
            this.m_isIntersectClipRect = false;
          }
          else
            format1.HorizontalScalingFactor = num1 * 100f;
        }
        StringFormatFlags stringFormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
        bool flag = (format.FormatFlags & stringFormatFlags) == stringFormatFlags && format.Trimming == StringTrimming.None && format.Alignment == StringAlignment.Near && format.LineAlignment == StringAlignment.Near;
        if ((double) rect.Width > (double) pdfFont.MeasureString(text, format1).Width && text.Split((char[]) null).Length > 1)
        {
          char[] spaces = StringTokenizer.Spaces;
          StringTokenizer.GetCharsCount(text, spaces);
          float num2 = 0.0f;
          if (((format1.Alignment == PdfTextAlignment.Justify ? 1 : (format1.LineAlignment == PdfVerticalAlignment.Middle ? 1 : 0)) | (flag ? 1 : 0)) != 0)
          {
            float width = pdfFont.MeasureString(text, format1).Width;
            num2 = (rect.Width - width) / (float) text.Length;
          }
          format1.CharacterSpacing = num2;
        }
        if (this.m_taggedPDF && !this.CheckPdfPage(new RectangleF(rect.Location, new SizeF(rect.Width, Math.Max(rect.Height, pdfFont.Height))), false))
          return;
        if ((((double) rect.Width != 0.0 ? 0 : ((double) rect.Height == 0.0 ? 1 : 0)) | (flag ? 1 : 0)) != 0)
        {
          PointF point = this.CorrectLocation(rect.Location, rect.Size, textSize, format1);
          this.Graphics.DrawString(text, pdfFont, brush1, point, format1);
        }
        else
        {
          rect.Width /= num1;
          if ((double) rect.Width == 0.0)
            rect.Width = textSize.Width;
          else if ((double) rect.Height == 0.0)
            rect.Height = textSize.Height;
          if ((double) rect.Width <= 0.0)
            return;
          if ((double) this.m_textClip.X != 0.0 && (double) this.m_textClip.Y != 0.0 && (double) this.m_textClip.Width != 0.0 && (double) this.m_textClip.Height != 0.0)
          {
            if ((double) this.m_textClip.Width < (double) rect.Width / 2.0)
              return;
            this.Graphics.DrawString(text, pdfFont, brush1, rect, format1);
            this.m_textClip = RectangleF.Empty;
          }
          else
            this.Graphics.DrawString(text, pdfFont, brush1, rect, format1);
        }
      }

      public void DrawString(
        string text,
        Font font,
        Brush brush,
        RectangleF rect,
        StringFormat format,
        float textAngle)
      {
        this.TextAngleLocal = textAngle;
        if (text == null)
          throw new ArgumentNullException(nameof (text));
        if (font == null)
          throw new ArgumentNullException(nameof (font));
        if (brush == null)
          throw new ArgumentNullException(nameof (brush));
        if (format == null)
          throw new ArgumentNullException(nameof (format));
        this.OnDrawPrimitive();
        PdfBrush brush1 = this.ConvertBrush(brush);
        PdfStringFormat format1 = this.ConvertFormat(format);
        PdfFont pdfFont;
        try
        {
          pdfFont = this.GetPdfFont(text, font);
        }
        catch (Exception ex)
        {
          pdfFont = (PdfFont) new PdfTrueTypeFont(this.GetInstalledFontLocation(font), font.Size, (PdfFontStyle) font.Style);
        }
        SizeF textSize = rect.Size;
        float num1 = this.ScaleText(text, pdfFont, rect, out textSize, format1);
        if ((double) num1 != 1.0)
        {
          if (format1 == null)
            format1 = new PdfStringFormat();
          format1.HorizontalScalingFactor = num1 * 100f;
        }
        StringFormatFlags stringFormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
        bool flag = (format.FormatFlags & stringFormatFlags) == stringFormatFlags && format.Trimming == StringTrimming.None && format.Alignment == StringAlignment.Near && format.LineAlignment == StringAlignment.Near;
        if ((double) rect.Width > (double) pdfFont.MeasureString(text, format1).Width && text.Split((char[]) null).Length > 1)
        {
          char[] spaces = StringTokenizer.Spaces;
          StringTokenizer.GetCharsCount(text, spaces);
          float width = pdfFont.MeasureString(text, format1).Width;
          float num2 = (rect.Width - width) / (float) text.Length;
          format1.CharacterSpacing = num2;
        }
        if (this.m_taggedPDF && !this.CheckPdfPage(new RectangleF(rect.Location, new SizeF(rect.Width, Math.Max(rect.Height, pdfFont.Height))), false))
          return;
        if ((((double) rect.Width != 0.0 ? 0 : ((double) rect.Height == 0.0 ? 1 : 0)) | (flag ? 1 : 0)) != 0)
        {
          PointF pointF = this.CorrectLocation(rect.Location, rect.Size, textSize, format1);
          if (this.EmbedFonts)
            format1.RightToLeft = false;
          this.Graphics.Save();
          this.Graphics.TranslateTransform(pointF.X, pointF.Y);
          this.Graphics.RotateTransform(textAngle);
          this.Graphics.DrawString(text, pdfFont, brush1, PointF.Empty);
          this.Graphics.Restore();
        }
        else
        {
          rect.Width /= num1;
          if ((double) rect.Width == 0.0)
            rect.Width = textSize.Width;
          else if ((double) rect.Height == 0.0)
            rect.Height = textSize.Height;
          PointF pointF = this.CorrectLocation(rect.Location, rect.Size, textSize, format1);
          if ((double) rect.Width <= 0.0)
            return;
          this.Graphics.Save();
          this.Graphics.TranslateTransform(pointF.X, pointF.Y);
          this.Graphics.RotateTransform(textAngle);
          this.Graphics.DrawString(text, pdfFont, brush1, PointF.Empty);
          this.Graphics.Restore();
        }
      }

      private static void DrawUpwardDiagonal(PdfGraphics graphics, PdfPen pen, SizeF brushSize)
      {
        float num1 = brushSize.Height / 2f;
        float num2 = brushSize.Width / 2f;
        graphics.DrawLine(pen, brushSize.Width, 0.0f, 0.0f, brushSize.Height);
        graphics.DrawLine(pen, 0.0f, num1, num2, 0.0f);
        graphics.DrawLine(pen, num2, brushSize.Height, brushSize.Width, num1);
        graphics.DrawLine(pen, -1f, 1f, 1f, -1f);
        graphics.DrawLine(pen, brushSize.Width - 1f, brushSize.Height + 1f, brushSize.Width + 1f, brushSize.Height - 1f);
      }

      private static void DrawVertical(PdfGraphics graphics, PdfPen pen, SizeF brushSize)
      {
        float num1 = 0.0f;
        float num2 = brushSize.Height / 2f;
        float height = brushSize.Height;
        graphics.DrawLine(pen, num1, 0.0f, num1, brushSize.Height);
        graphics.DrawLine(pen, num2, 0.0f, num2, brushSize.Height);
        graphics.DrawLine(pen, height, 0.0f, height, brushSize.Height);
      }

      private void DrawWeave(PdfGraphics g, PdfPen pen, SizeF brushSize)
      {
        g.TranslateTransform(-0.5f, -0.5f);
        g.DrawLine(pen, new PointF(0.0f, 0.0f), new PointF(0.5f, 0.5f));
        g.DrawLine(pen, new PointF(0.0f, 1f), new PointF(1f, 0.0f));
        g.DrawLine(pen, new PointF(0.0f, 5f), new PointF(5f, 0.0f));
        g.DrawLine(pen, new PointF(0.0f, 4f), new PointF(5f, 9f));
        g.DrawLine(pen, new PointF(2.5f, 2.5f), new PointF(9f, 9f));
        g.DrawLine(pen, new PointF(4f, 0.0f), new PointF(6.5f, 2.5f));
        g.DrawLine(pen, new PointF((float) (6.5 - Math.Sqrt(0.125)), (float) (2.5 + Math.Sqrt(0.125))), new PointF(9f, 0.0f));
        g.DrawLine(pen, new PointF(6.5f, 6.5f), new PointF(9f, 4f));
        g.DrawLine(pen, new PointF(2.5f, 6.5f), new PointF(0.5f, 8.5f));
      }

      public void EndContainer(GraphicsContainer container)
      {
        if (container == null)
          throw new ArgumentNullException(nameof (container));
        this.Graphics.PutComment(nameof (EndContainer));
        if (this.m_graphicsStates[(object) container] is PdfGraphicsState graphicsState)
          this.Graphics.Restore(graphicsState);
        this.NativeGraphics.EndContainer(container);
        this.m_stateChanged = true;
        this.m_bFirstCall = true;
        this.m_bFirstTransform = true;
        this.Transform = this.Transform;
      }

      public void ExcludeClip(Rectangle rect)
      {
        this.NativeGraphics.ExcludeClip(rect);
        this.SetClip();
      }

      public void ExcludeClip(Region region)
      {
        this.NativeGraphics.ExcludeClip(region);
        this.SetClip();
      }

      public void FillClosedCurve(Brush brush, PointF[] points, FillMode fillMode, float tension)
      {
        if (brush == null)
          throw new ArgumentNullException(nameof (brush));
        this.Graphics.DrawPolygon(this.ConvertBrush(brush), points);
      }

      public void FillEllipse(Brush brush, RectangleF rect)
      {
        if (brush == null)
          throw new ArgumentNullException(nameof (brush));
        this.OnDrawPrimitive();
        this.Graphics.DrawEllipse(this.ConvertBrush(brush), rect);
      }

      public void FillPath(Brush brush, GraphicsPath path)
      {
        if (brush == null)
          throw new ArgumentNullException(nameof (brush));
        if (path == null)
          throw new ArgumentNullException(nameof (path));
        this.OnDrawPrimitive();
        this.Graphics.DrawPath(this.ConvertBrush(brush), new PdfPath(path.PathPoints, path.PathTypes));
      }

      public void FillPie(
        Brush brush,
        float x,
        float y,
        float width,
        float height,
        float startAngle,
        float sweepAngle)
      {
        if (brush == null)
          throw new ArgumentNullException(nameof (brush));
        this.OnDrawPrimitive();
        RectangleF rectangle = new RectangleF(x, y, width, height);
        this.Graphics.DrawPie(this.ConvertBrush(brush), rectangle, startAngle, sweepAngle);
      }

      public void FillPolygon(Brush brush, PointF[] points)
      {
        if (brush == null)
          throw new ArgumentNullException(nameof (brush));
        if (points == null)
          throw new ArgumentNullException(nameof (points));
        this.OnDrawPrimitive();
        this.Graphics.DrawPolygon(this.ConvertBrush(brush), points);
      }

      public void FillRectangles(Brush brush, RectangleF[] rects)
      {
        if (brush == null)
          throw new ArgumentNullException(nameof (brush));
        if (rects == null)
          throw new ArgumentNullException(nameof (rects));
        this.OnDrawPrimitive();
        PdfBrush brush1 = this.ConvertBrush(brush);
        for (int index = 0; index < rects.Length; ++index)
        {
          RectangleF rect = rects[index];
          this.Graphics.DrawRectangle(brush1, rect);
        }
      }

      public void FillRegion(Brush brush, Region region)
      {
        if (brush == null)
          throw new ArgumentNullException(nameof (brush));
        if (region == null)
          throw new ArgumentNullException(nameof (region));
        this.OnDrawPrimitive();
        Matrix transform = this.NativeGraphics.Transform;
        RectangleF[] regionScans = region.GetRegionScans(transform);
        this.FillRectangles(brush, regionScans);
      }

      private PdfBrush GetBrushFromPen(PdfPen pdfPen)
      {
        return pdfPen.Brush ?? (PdfBrush) new PdfSolidBrush(pdfPen.Color);
      }

      private GraphicsPath GetClipPath()
      {
        GraphicsPath clipPath = (GraphicsPath) null;
        Region clip = this.NativeGraphics.Clip;
        if (!clip.IsEmpty(this.NativeGraphics) && !clip.IsInfinite(this.NativeGraphics))
        {
          clipPath = new GraphicsPath(FillMode.Winding);
          RectangleF realClip = this.RealClip;
          if ((double) realClip.X == 0.0)
          {
            realClip = this.RealClip;
            if ((double) realClip.Y == 0.0)
            {
              realClip = this.RealClip;
              if ((double) realClip.Width == 0.0)
              {
                realClip = this.RealClip;
                if ((double) realClip.Height == 0.0)
                {
                  RectangleF[] regionScans = clip.GetRegionScans(new Matrix());
                  clipPath.AddRectangles(regionScans);
                  goto label_7;
                }
              }
            }
          }
          clipPath.AddRectangle(this.RealClip);
          this.RealClip = RectangleF.Empty;
          return clipPath;
        }
    label_7:
        return clipPath;
      }

      private string GetFontSuffix(FontStyle fs)
      {
        string fontSuffix = "";
        switch (fs)
        {
          case FontStyle.Bold:
            return "Bold";
          case FontStyle.Italic:
            return "Italic";
          case FontStyle.Bold | FontStyle.Italic:
            fontSuffix = "Bold Italic";
            break;
        }
        return fontSuffix;
      }

      private string GetInstalledFontLocation(Font font)
      {
        string name = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
        RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name, false);
        string[] valueNames = registryKey.GetValueNames();
        string environmentVariable = Environment.GetEnvironmentVariable("SystemRoot");
        string str1 = $"{font.Name} {this.GetFontSuffix(font.Style)}";
        int index = 0;
        for (int length = valueNames.Length; index < length; ++index)
        {
          if (valueNames[index].Contains(str1))
          {
            string str2 = registryKey.GetValue(valueNames[index]).ToString();
            return $"{environmentVariable}\\Fonts\\{str2}";
          }
        }
        return string.Empty;
      }

      private PdfFillMode GetPathFillMode(GraphicsPath path)
      {
        if (path == null)
          throw new ArgumentNullException(nameof (path));
        return path.FillMode != FillMode.Winding ? PdfFillMode.Alternate : PdfFillMode.Winding;
      }

      private PdfFont GetPdfFont(string text, Font font)
      {
        if (text == null)
          throw new ArgumentNullException(nameof (text));
        if (font == null)
          throw new ArgumentNullException(nameof (font));
        bool unicode = PdfDocument.ConformanceLevel == PdfConformanceLevel.Pdf_A1B || PdfString.IsUnicode(font.Name) || PdfString.IsUnicode(text);
        float size = font.Size;
        if (font.Name == "Wingdings" || font.Name.ToLower() == "latha" || font.Name.ToLower() == "shruti" || font.Name.ToLower() == "mangal" || font.Name.ToLower() == "tunga" || font.Name.ToLower() == "vrinda" || font.Name.ToLower() == "swis721 th bt")
          unicode = true;
        return (PdfFont) new PdfTrueTypeFont(font, size, unicode);
      }

      private PdfWordWrapType GetWrapType(StringFormatFlags stringFormatFlags)
      {
        PdfWordWrapType wrapType = PdfWordWrapType.Word;
        if ((stringFormatFlags & StringFormatFlags.NoWrap) != (StringFormatFlags) 0)
          wrapType = PdfWordWrapType.None;
        return wrapType;
      }

      private PdfGraphicsUnit GraphicsToPrintUnits(GraphicsUnit gUnits)
      {
        switch (gUnits)
        {
          case GraphicsUnit.Display:
            return PdfGraphicsUnit.Pixel;
          case GraphicsUnit.Pixel:
            return PdfGraphicsUnit.Pixel;
          case GraphicsUnit.Point:
            return PdfGraphicsUnit.Point;
          case GraphicsUnit.Inch:
            return PdfGraphicsUnit.Inch;
          case GraphicsUnit.Document:
            return PdfGraphicsUnit.Document;
          case GraphicsUnit.Millimeter:
            return PdfGraphicsUnit.Millimeter;
          default:
            return PdfGraphicsUnit.Point;
        }
      }

      private void InternalResetClip()
      {
        if (this.m_bFirstCall)
          return;
        this.Graphics.PutComment(nameof (InternalResetClip));
        this.m_bFirstCall = true;
        this.Graphics.Restore();
        this.m_stateChanged = true;
      }

      private void InternalResetTransformation()
      {
        if (!this.m_bFirstTransform)
        {
          this.Graphics.PutComment(nameof (InternalResetTransformation));
          this.Graphics.Restore();
        }
        this.m_bFirstTransform = true;
      }

      public void IntersectClip(RectangleF rect)
      {
        this.NativeGraphics.IntersectClip(rect);
        this.m_isIntersectClipRect = true;
        this.SetClip();
      }

      public void IntersectClip(Region region)
      {
        this.NativeGraphics.IntersectClip(region);
        this.SetClip();
      }

      private bool IsLine(PointF[] points)
      {
        int length = points.Length;
        float x = points[0].X;
        bool flag1 = false;
        for (int index = 1; index < length; ++index)
        {
          flag1 = false;
          if ((double) x == (double) points[index].X)
            flag1 = true;
          else
            break;
        }
        if (!flag1)
        {
          float y = points[0].Y;
          for (int index = 1; index < length; ++index)
          {
            flag1 = false;
            if ((double) y == (double) points[index].Y)
              flag1 = true;
            else
              break;
          }
        }
        if (!flag1)
        {
          float num1 = points[1].X - points[0].X;
          float num2 = (points[1].Y - points[0].Y) / num1;
          for (int index = 2; index < length; ++index)
          {
            float num3 = points[index].X - points[index - 1].X;
            float num4 = (points[index].Y - points[index - 1].Y) / num3;
            bool flag2 = false;
            if ((double) Math.Abs(num2 - num4) > 1.4012984643248171E-45)
              return flag2;
            flag1 = true;
          }
        }
        return flag1;
      }

      public void MultiplyTransform(Matrix matrix, MatrixOrder order)
      {
        if (matrix == null)
          throw new ArgumentNullException(nameof (matrix));
        this.NativeGraphics.MultiplyTransform(matrix, order);
        this.Graphics.PutComment("MuliplyTransform");
        if (order == MatrixOrder.Append)
          this.Graphics.MultiplyTransform(PdfEmfRenderer.PrepareMatrix(matrix));
        else
          this.Transform = this.NativeGraphics.Transform;
      }

      private void OnChangeState() => this.m_stateChanged = true;

      private void OnDrawPrimitive()
      {
        if (!this.m_stateChanged)
          return;
        this.InternalResetClip();
        this.m_bFirstCall = false;
        this.Graphics.PutComment(nameof (OnDrawPrimitive));
        this.Graphics.Save();
        this.SetPdfClipPath();
        this.m_stateChanged = false;
      }

      public void OnError(Exception ex) => this.BeforeEnd();

      private static PdfTransformationMatrix PrepareMatrix(Matrix matrix)
      {
        PdfTransformationMatrix transformationMatrix = new PdfTransformationMatrix();
        PdfTransformationMatrix matrix1 = new PdfTransformationMatrix();
        matrix1.Matrix = matrix;
        transformationMatrix.Scale(1f, -1f);
        transformationMatrix.Multiply(matrix1);
        transformationMatrix.Scale(1f, -1f);
        return transformationMatrix;
      }

      private static PdfTransformationMatrix PrepareMatrix(Matrix matrix, float pageScale)
      {
        PdfTransformationMatrix transformationMatrix = new PdfTransformationMatrix();
        PdfTransformationMatrix matrix1 = new PdfTransformationMatrix();
        matrix1.Matrix = matrix;
        transformationMatrix.Scale(pageScale, -pageScale);
        transformationMatrix.Multiply(matrix1);
        transformationMatrix.Scale(1f, -1f);
        return transformationMatrix;
      }

      public void ResetClip()
      {
        this.NativeGraphics.ResetClip();
        this.m_stateChanged = true;
        this.m_textClip = RectangleF.Empty;
      }

      public void ResetTransform()
      {
        this.Graphics.PutComment(nameof (ResetTransform));
        this.InternalResetClip();
        this.SetTransform();
        this.NativeGraphics.ResetTransform();
      }

      public void Restore(GraphicsState gState)
      {
        this.Graphics.PutComment(nameof (Restore));
        if (this.m_graphicsStates[(object) gState] is PdfGraphicsState graphicsState)
          this.Graphics.Restore(graphicsState);
        this.NativeGraphics.Restore(gState);
        this.m_stateChanged = true;
        this.m_bFirstCall = true;
        this.m_bFirstTransform = false;
        this.m_stateRestored = true;
      }

      public void RotateTransform(float angle, MatrixOrder order)
      {
        this.Graphics.PutComment(nameof (RotateTransform));
        this.NativeGraphics.RotateTransform(angle, order);
        if (order == MatrixOrder.Append)
          this.Graphics.RotateTransform(angle);
        else
          this.Transform = this.Transform;
      }

      public GraphicsState Save()
      {
        this.Graphics.PutComment(nameof (Save));
        this.InternalResetClip();
        this.InternalResetTransformation();
        PdfGraphicsState pdfGraphicsState = this.Graphics.Save();
        GraphicsState key = this.NativeGraphics.Save();
        this.m_graphicsStates[(object) key] = (object) pdfGraphicsState;
        return key;
      }

      private float ScaleText(
        string text,
        PdfFont pdfFont,
        RectangleF rect,
        out SizeF textSize,
        PdfStringFormat format)
      {
        if (text == null)
          throw new ArgumentNullException(nameof (text));
        if (pdfFont == null)
          throw new ArgumentNullException(nameof (pdfFont));
        float num = 1f;
        textSize = rect.Size;
        if (text.Length > 0)
        {
          if (format == null)
            format = new PdfStringFormat();
          if (text.EndsWith(" "))
            format.MeasureTrailingSpaces = true;
          textSize = pdfFont.MeasureString(text, format);
          if ((double) rect.Width > 0.0 && (double) rect.Width < 2147483904.0 && (double) textSize.Width > (double) rect.Width && (double) textSize.Width > 0.0)
            num = rect.Width / textSize.Width;
        }
        return num;
      }

      public void ScaleTransform(float sx, float sy, MatrixOrder order)
      {
        this.Graphics.PutComment(nameof (ScaleTransform));
        this.NativeGraphics.ScaleTransform(sx, sy, order);
        if (order == MatrixOrder.Append)
          this.Graphics.ScaleTransform(sx, sy);
        else
          this.Transform = this.Transform;
      }

      internal void SetBBox(RectangleF bounds) => this.m_graphics.SetBBox(bounds);

      internal void SetBounds(PointF location, SizeF size)
      {
        this.m_bounds = new PdfTransformationMatrix();
        if (!size.IsEmpty)
          this.m_bounds.Scale(size);
        if (location.IsEmpty)
          return;
        this.m_bounds.Translate(location.X, -location.Y);
      }

      private void SetClip() => this.m_stateChanged = true;

      public void SetClip(GraphicsPath path, CombineMode mode)
      {
        this.NativeGraphics.SetClip(path, mode);
        this.SetClip();
      }

      public void SetClip(RectangleF rect, CombineMode mode)
      {
        this.NativeGraphics.SetClip(rect, mode);
        this.RealClip = rect;
        this.m_textClip = rect;
        this.SetClip();
      }

      public void SetClip(Region region, CombineMode mode)
      {
        this.RealClip = RectangleF.Empty;
        this.NativeGraphics.SetClip(region, mode);
        this.SetClip();
      }

      private void SetPdfClipPath()
      {
        GraphicsPath clipPath = this.GetClipPath();
        if (clipPath == null)
          return;
        PointF[] pathPoints = clipPath.PathPoints;
        byte[] pathTypes1 = clipPath.PathTypes;
        PdfFillMode pathFillMode = this.GetPathFillMode(clipPath);
        byte[] pathTypes2 = pathTypes1;
        this.Graphics.SetClip(new PdfPath(pathPoints, pathTypes2), pathFillMode);
      }

      public void SetRenderingOrigin(Point origin)
      {
        this.Graphics.PutComment(nameof (SetRenderingOrigin));
        this.NativeGraphics.RenderingOrigin = origin;
        this.Graphics.TranslateTransform((float) -origin.X, (float) -origin.Y);
        this.SetTransform();
      }

      private void SetTransform()
      {
        this.InternalResetClip();
        this.Graphics.PutComment(nameof (SetTransform));
        if (!this.m_bFirstTransform && !this.m_stateRestored)
        {
          this.Graphics.Restore();
        }
        else
        {
          this.m_bFirstTransform = false;
          this.m_stateRestored = false;
        }
        this.Graphics.Save();
      }

      public void SetTransform(Matrix matrix)
      {
        if (matrix == null)
          throw new ArgumentNullException(nameof (matrix));
        this.Graphics.PutComment("SetTransform( matrix )");
        this.InternalResetClip();
        this.Transform = matrix;
      }

      public void TransformPoints(CoordinateSpace destSpace, CoordinateSpace srcSpace, PointF[] pts)
      {
        this.NativeGraphics.TransformPoints(destSpace, srcSpace, pts);
      }

      public void TranslateClip(float dx, float dy)
      {
        this.NativeGraphics.TranslateClip(dx, dy);
        this.m_stateChanged = true;
      }

      public void TranslateTransform(float dx, float dy, MatrixOrder order)
      {
        this.Graphics.PutComment(nameof (TranslateTransform));
        if (this.NativeGraphics != null)
          this.NativeGraphics.TranslateTransform(dx, dy, order);
        if (order == MatrixOrder.Append)
          this.Graphics.TranslateTransform(dx, dy);
        else
          this.Transform = this.Transform;
      }

      internal float AlphaBrush
      {
        get => this.m_alphaBrush;
        set => this.m_alphaBrush = value;
      }

      internal float AlphaPen
      {
        get => this.m_alphaPen;
        set => this.m_alphaPen = value;
      }

      internal PdfBlendMode BlendMode
      {
        get => this.m_blendMode;
        set => this.m_blendMode = value;
      }

      private RectangleF ClipBounds => RectangleF.Empty;

      internal object Context
      {
        get => this.m_context;
        set
        {
          if (this.m_context == value)
            return;
          this.m_context = value;
        }
      }

      private PdfUnitConvertor ConvertX
      {
        get
        {
          if (this.m_convertX == null)
            this.m_convertX = new PdfUnitConvertor(this.NativeGraphics);
          return this.m_convertX;
        }
      }

      private PdfUnitConvertor ConvertY
      {
        get
        {
          if (this.m_convertY == null)
            this.m_convertY = new PdfUnitConvertor(this.NativeGraphics);
          return this.m_convertY;
        }
      }

      internal bool EmbedFonts => this.m_embedFonts;

      public PdfGraphics Graphics => this.m_graphics;

      internal bool IsTranparency
      {
        get => this.m_bIsTransparency;
        set => this.m_bIsTransparency = value;
      }

      public System.Drawing.Graphics NativeGraphics => this.m_grCache;

      public float PageScale
      {
        get => this.NativeGraphics.PageScale;
        set
        {
          this.NativeGraphics.PageScale = value;
          this.Graphics.PutComment("PageScale property");
          this.SetTransform();
          this.Graphics.ScaleTransform(value, value);
        }
      }

      public bool PageTransformed
      {
        get => this.m_bPageTransformed;
        set => this.m_bPageTransformed = value;
      }

      public GraphicsUnit PageUnit
      {
        get => this.NativeGraphics.PageUnit;
        set
        {
          this.NativeGraphics.PageUnit = value;
          float num1 = 1f;
          float num2 = 1f;
          if (value != GraphicsUnit.Display)
          {
            num1 = this.ConvertX.ConvertUnits(num1, this.GraphicsToPrintUnits(value), PdfGraphicsUnit.Pixel);
            num2 = this.ConvertY.ConvertUnits(num2, this.GraphicsToPrintUnits(value), PdfGraphicsUnit.Pixel);
          }
          this.Graphics.PutComment("PageUnit property");
          this.NativeGraphics.ScaleTransform(num1, num2, MatrixOrder.Prepend);
          this.Graphics.ScaleTransform(num1, num2);
        }
      }

      private RectangleF RealClip
      {
        get => this.m_realClip;
        set => this.m_realClip = value;
      }

      private TextRegionManager TextRegions => this.Context as TextRegionManager;

      public Matrix Transform
      {
        get => this.NativeGraphics.Transform;
        set
        {
          this.InternalResetClip();
          this.NativeGraphics.Transform = value;
          PdfTransformationMatrix matrix = PdfEmfRenderer.PrepareMatrix(value, this.PageScale);
          this.Graphics.PutComment("Transform property");
          this.SetTransform();
          this.Graphics.MultiplyTransform(matrix);
        }
      }
    }
}
