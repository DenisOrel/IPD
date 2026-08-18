// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfPen
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.ColorSpace;
using Syncfusion.Pdf.IO;
using System;


namespace Syncfusion.Pdf.Graphics
{
    public class PdfPen : ICloneable
    {
      private bool m_bImmutable;
      private PdfBrush m_brush;
      private PdfColor m_color;
      private PdfColorSpace m_colorSpace;
      private PdfExtendedColor m_colorspaces;
      private float m_dashOffset;
      private float[] m_dashPattern;
      private PdfDashStyle m_dashStyle;
      private PdfLineCap m_lineCap;
      private PdfLineJoin m_lineJoin;
      private float m_miterLimit;
      private float m_width;

      private PdfPen()
      {
        this.m_color = PdfColor.Empty;
        this.m_dashPattern = new float[0];
        this.m_width = 1f;
      }

      public PdfPen(PdfExtendedColor color)
      {
        this.m_color = PdfColor.Empty;
        this.m_dashPattern = new float[0];
        this.m_width = 1f;
        PdfColorSpaces colorSpace1 = color.ColorSpace;
        this.m_colorspaces = color;
        PdfColorSpaces colorSpace2 = color.ColorSpace;
        this.m_colorspaces = color;
        switch (color)
        {
          case PdfCalRGBColor _:
            PdfCalRGBColor pdfCalRgbColor = color as PdfCalRGBColor;
            this.m_color = new PdfColor((byte) pdfCalRgbColor.Red, (byte) pdfCalRgbColor.Green, (byte) pdfCalRgbColor.Blue);
            break;
          case PdfCalGrayColor _:
            PdfCalGrayColor pdfCalGrayColor = color as PdfCalGrayColor;
            this.m_color = new PdfColor((float) (byte) pdfCalGrayColor.Gray);
            this.m_color.Gray = Convert.ToSingle(pdfCalGrayColor.Gray);
            break;
          case PdfLabColor _:
            PdfLabColor pdfLabColor = color as PdfLabColor;
            this.m_color = new PdfColor((byte) pdfLabColor.L, (byte) pdfLabColor.A, (byte) pdfLabColor.B);
            break;
          case PdfICCColor _:
            PdfICCColor pdfIccColor = color as PdfICCColor;
            if (pdfIccColor.ColorSpaces.AlternateColorSpace is PdfCalGrayColorSpace)
            {
              this.m_color = new PdfColor((float) (byte) pdfIccColor.ColorComponents[0]);
              this.m_color.Gray = Convert.ToSingle(pdfIccColor.ColorComponents[0]);
              break;
            }
            if (pdfIccColor.ColorSpaces.AlternateColorSpace is PdfCalRGBColorSpace)
            {
              this.m_color = new PdfColor((byte) pdfIccColor.ColorComponents[0], (byte) pdfIccColor.ColorComponents[1], (byte) pdfIccColor.ColorComponents[2]);
              break;
            }
            if (pdfIccColor.ColorSpaces.AlternateColorSpace is PdfLabColorSpace)
            {
              this.m_color = new PdfColor((byte) pdfIccColor.ColorComponents[0], (byte) pdfIccColor.ColorComponents[1], (byte) pdfIccColor.ColorComponents[2]);
              break;
            }
            if (!(pdfIccColor.ColorSpaces.AlternateColorSpace is PdfDeviceColorSpace))
            {
              this.m_color = new PdfColor((byte) pdfIccColor.ColorComponents[0], (byte) pdfIccColor.ColorComponents[1], (byte) pdfIccColor.ColorComponents[2]);
              break;
            }
            switch ((pdfIccColor.ColorSpaces.AlternateColorSpace as PdfDeviceColorSpace).DeviceColorSpaceType.ToString())
            {
              case "RGB":
                this.m_color = new PdfColor((byte) pdfIccColor.ColorComponents[0], (byte) pdfIccColor.ColorComponents[1], (byte) pdfIccColor.ColorComponents[2]);
                return;
              case "GrayScale":
                this.m_color = new PdfColor((float) (byte) pdfIccColor.ColorComponents[0]);
                this.m_color.Gray = Convert.ToSingle(pdfIccColor.ColorComponents[0]);
                return;
              case "CMYK":
                this.m_color = new PdfColor((float) pdfIccColor.ColorComponents[0], (float) pdfIccColor.ColorComponents[1], (float) pdfIccColor.ColorComponents[2], (float) pdfIccColor.ColorComponents[3]);
                return;
              default:
                return;
            }
          case PdfSeparationColor _:
            this.m_color.Gray = (float) (color as PdfSeparationColor).Tint;
            break;
          case PdfIndexedColor _:
            this.m_color.G = (byte) (color as PdfIndexedColor).SelectColorIndex;
            break;
        }
      }

      public PdfPen(PdfBrush brush)
      {
        this.m_color = PdfColor.Empty;
        this.m_dashPattern = new float[0];
        this.m_width = 1f;
        if (brush == null)
          throw new ArgumentNullException(nameof (brush));
        this.SetBrush(brush);
      }

      public PdfPen(PdfColor color)
      {
        this.m_color = PdfColor.Empty;
        this.m_dashPattern = new float[0];
        this.m_width = 1f;
        this.Color = color;
      }

      public PdfPen(PdfBrush brush, float width)
        : this(brush)
      {
        this.Width = width;
      }

      internal PdfPen(PdfColor color, bool immutable)
        : this(color)
      {
        this.m_bImmutable = immutable;
      }

      public PdfPen(PdfColor color, float width)
        : this(color)
      {
        this.Width = width;
      }

      private void CheckImmutability(string propertyName)
      {
        if (this.m_bImmutable)
          throw new ArgumentException("The immutable object can't be changed", propertyName);
      }

      public PdfPen Clone() => this.MemberwiseClone() as PdfPen;

      private bool DashControl(PdfPen pen, bool saveState, PdfStreamWriter streamWriter)
      {
        if (pen != null)
          saveState |= (double) this.DashOffset != (double) pen.DashOffset | this.DashPattern != pen.DashPattern | this.DashStyle != pen.DashStyle | (double) this.Width != (double) pen.Width;
        else
          saveState = true;
        if (saveState)
        {
          float width = this.Width;
          float[] pattern = this.GetPattern();
          streamWriter.SetLineDashPattern(pattern, this.DashOffset * width);
        }
        return saveState;
      }

      internal float[] GetPattern()
      {
        float[] pattern = this.DashPattern.Clone() as float[];
        for (int index = 0; index < pattern.Length; ++index)
          pattern[index] *= this.Width;
        return pattern;
      }

      internal bool MonitorChanges(
        PdfPen currentPen,
        PdfStreamWriter streamWriter,
        PdfGraphics.GetResources getResources,
        bool saveState,
        PdfColorSpace currentColorSpace,
        PdfTransformationMatrix matrix)
      {
        bool flag1 = false;
        saveState = true;
        if (currentPen == null)
          flag1 = true;
        bool flag2 = this.DashControl(currentPen, saveState, streamWriter);
        if (saveState || (double) this.Width != (double) currentPen.Width)
        {
          streamWriter.SetLineWidth(this.Width);
          flag2 = true;
        }
        if (saveState || this.LineJoin != currentPen.LineJoin)
        {
          streamWriter.SetLineJoin(this.LineJoin);
          flag2 = true;
        }
        if (saveState || this.LineCap != currentPen.LineCap)
        {
          streamWriter.SetLineCap(this.LineCap);
          flag2 = true;
        }
        if (saveState || (double) this.MiterLimit != (double) currentPen.MiterLimit)
        {
          float miterLimit = this.MiterLimit;
          if ((double) miterLimit > 0.0)
          {
            streamWriter.SetMiterLimit(miterLimit);
            flag2 = true;
          }
        }
        if (!saveState && !(this.Color != currentPen.Color) && this.Brush == currentPen.Brush && this.m_colorSpace == currentColorSpace)
          return flag2;
        PdfBrush brush1 = this.m_brush;
        if (brush1 != null)
        {
          PdfBrush brush2 = brush1.Clone();
          this.SetStrokingToBrush(brush2);
          if (brush2 is PdfGradientBrush pdfGradientBrush)
          {
            PdfTransformationMatrix matrix1 = pdfGradientBrush.Matrix;
            if (matrix1 != null)
              matrix.Multiply(matrix1);
            pdfGradientBrush.Matrix = matrix;
          }
          PdfBrush brush3 = currentPen?.Brush;
          return flag2 | brush2.MonitorChanges(brush3, streamWriter, getResources, saveState, currentColorSpace);
        }
        if (this.Colorspaces == null)
        {
          streamWriter.SetColorAndSpace(this.Color, currentColorSpace, true);
          return true;
        }
        streamWriter.SetColorAndSpace(this.Color, currentColorSpace, true, true);
        return true;
      }

      internal bool MonitorChanges(
        PdfPen currentPen,
        PdfStreamWriter streamWriter,
        PdfGraphics.GetResources getResources,
        bool saveState,
        PdfColorSpace currentColorSpace,
        PdfTransformationMatrix matrix,
        bool iccBased)
      {
        bool flag1 = false;
        if (currentPen == null)
        {
          saveState = true;
          flag1 = true;
        }
        bool flag2 = this.DashControl(currentPen, saveState, streamWriter);
        if (saveState || (double) this.Width != (double) currentPen.Width)
        {
          streamWriter.SetLineWidth(this.Width);
          flag2 = true;
        }
        if (saveState || this.LineJoin != currentPen.LineJoin)
        {
          streamWriter.SetLineJoin(this.LineJoin);
          flag2 = true;
        }
        if (saveState || this.LineCap != currentPen.LineCap)
        {
          streamWriter.SetLineCap(this.LineCap);
          flag2 = true;
        }
        if (saveState || (double) this.MiterLimit != (double) currentPen.MiterLimit)
        {
          float miterLimit = this.MiterLimit;
          if ((double) miterLimit > 0.0)
          {
            streamWriter.SetMiterLimit(miterLimit);
            flag2 = true;
          }
        }
        if (!saveState && !(this.Color != currentPen.Color) && this.Brush == currentPen.Brush && this.m_colorSpace == currentColorSpace)
          return flag2;
        PdfBrush brush1 = this.m_brush;
        if (brush1 != null)
        {
          PdfBrush brush2 = brush1.Clone();
          this.SetStrokingToBrush(brush2);
          if (brush2 is PdfGradientBrush pdfGradientBrush)
          {
            PdfTransformationMatrix matrix1 = pdfGradientBrush.Matrix;
            if (matrix1 != null)
              matrix.Multiply(matrix1);
            pdfGradientBrush.Matrix = matrix;
          }
          PdfBrush brush3 = currentPen?.Brush;
          return flag2 | brush2.MonitorChanges(brush3, streamWriter, getResources, saveState, currentColorSpace);
        }
        if (this.Colorspaces == null)
        {
          streamWriter.SetColorAndSpace(this.Color, currentColorSpace, true);
          return true;
        }
        if (this.Colorspaces is PdfIndexedColor)
        {
          streamWriter.SetColorAndSpace(this.Color, currentColorSpace, true, true, true, true);
          return true;
        }
        streamWriter.SetColorAndSpace(this.Color, currentColorSpace, true, true, true);
        return true;
      }

      private void ResetStroking(PdfBrush brush)
      {
        PdfTilingBrush brush1 = this.m_brush as PdfTilingBrush;
        PdfGradientBrush brush2 = this.m_brush as PdfGradientBrush;
        if (brush1 != null)
        {
          brush1.Stroking = false;
        }
        else
        {
          if (brush2 == null)
            throw new ArgumentException("Unsupported brush.", nameof (brush));
          brush2.Stroking = false;
        }
      }

      private void SetBrush(PdfBrush brush)
      {
        if (brush is PdfSolidBrush pdfSolidBrush)
        {
          this.Color = pdfSolidBrush.Color;
        }
        else
        {
          this.m_brush = brush.Clone();
          this.SetStrokingToBrush(this.m_brush);
        }
      }

      private void SetStrokingToBrush(PdfBrush brush)
      {
        PdfTilingBrush pdfTilingBrush = brush as PdfTilingBrush;
        PdfGradientBrush pdfGradientBrush = brush as PdfGradientBrush;
        if (pdfTilingBrush != null)
          pdfTilingBrush.Stroking = true;
        else if (pdfGradientBrush != null)
          pdfGradientBrush.Stroking = true;
        else if (!(brush is PdfSolidBrush))
          throw new ArgumentException("Unsupported brush.", nameof (brush));
      }

      object ICloneable.Clone() => (object) this.Clone();

      public PdfBrush Brush
      {
        get
        {
          PdfBrush brush = this.m_brush != null ? this.m_brush.Clone() : (PdfBrush) null;
          if (this.m_brush != null)
            this.ResetStroking(brush);
          return this.m_brush;
        }
        set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (Brush));
          this.CheckImmutability(nameof (Brush));
          this.SetBrush(value);
        }
      }

      public PdfColor Color
      {
        get => this.m_color;
        set
        {
          this.CheckImmutability(nameof (Color));
          this.m_color = value;
        }
      }

      internal PdfExtendedColor Colorspaces
      {
        get => this.m_colorspaces;
        set => this.m_colorspaces = value;
      }

      public float DashOffset
      {
        get => this.m_dashOffset;
        set
        {
          this.CheckImmutability(nameof (DashOffset));
          this.m_dashOffset = value;
        }
      }

      public float[] DashPattern
      {
        get => this.m_dashPattern;
        set
        {
          if (this.DashStyle == PdfDashStyle.Solid)
            throw new ArgumentException("This operation is not allowed. Set Custom dash style to change the pattern.");
          this.CheckImmutability(nameof (DashPattern));
          this.m_dashPattern = value;
        }
      }

      public PdfDashStyle DashStyle
      {
        get => this.m_dashStyle;
        set
        {
          this.CheckImmutability(nameof (DashStyle));
          if (this.m_dashStyle == value)
            return;
          this.m_dashStyle = value;
          switch (this.m_dashStyle)
          {
            case PdfDashStyle.Dash:
              this.m_dashPattern = new float[2]{ 3f, 1f };
              break;
            case PdfDashStyle.Dot:
              this.m_dashPattern = new float[2]{ 1f, 1f };
              break;
            case PdfDashStyle.DashDot:
              this.m_dashPattern = new float[4]
              {
                3f,
                1f,
                1f,
                1f
              };
              break;
            case PdfDashStyle.DashDotDot:
              this.m_dashPattern = new float[6]
              {
                3f,
                1f,
                1f,
                1f,
                1f,
                1f
              };
              break;
            case PdfDashStyle.Custom:
              break;
            default:
              this.m_dashStyle = PdfDashStyle.Solid;
              this.m_dashPattern = new float[0];
              break;
          }
        }
      }

      internal bool IsImmutable => this.m_bImmutable;

      public PdfLineCap LineCap
      {
        get => this.m_lineCap;
        set
        {
          this.CheckImmutability(nameof (LineCap));
          this.m_lineCap = value;
        }
      }

      public PdfLineJoin LineJoin
      {
        get => this.m_lineJoin;
        set
        {
          this.CheckImmutability(nameof (LineJoin));
          this.m_lineJoin = value;
        }
      }

      public float MiterLimit
      {
        get => this.m_miterLimit;
        set
        {
          this.CheckImmutability(nameof (MiterLimit));
          this.m_miterLimit = value;
        }
      }

      public float Width
      {
        get => this.m_width;
        set
        {
          this.CheckImmutability(nameof (Width));
          this.m_width = value;
        }
      }
    }
}
