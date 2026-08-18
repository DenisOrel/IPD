// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfLinearGradientBrush
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;
using System.Drawing;


namespace Syncfusion.Pdf.Graphics
{
    public class PdfLinearGradientBrush : PdfGradientBrush
    {
      private PdfBlend m_blend;
      private RectangleF m_boundaries;
      private PdfColorBlend m_colourBlend;
      private PdfColor[] m_colours;
      private PointF m_pointEnd;
      private PointF m_pointStart;

      private PdfLinearGradientBrush(PdfColor color1, PdfColor color2)
        : base(new PdfDictionary())
      {
        this.m_colours = new PdfColor[2]{ color1, color2 };
        this.m_colourBlend = new PdfColorBlend(2);
        this.m_colourBlend.Positions = new float[2]{ 0.0f, 1f };
        this.m_colourBlend.Colors = this.m_colours;
        this.InitShading();
      }

      public PdfLinearGradientBrush(PointF point1, PointF point2, PdfColor color1, PdfColor color2)
        : this(color1, color2)
      {
        this.m_pointStart = point1;
        this.m_pointEnd = point2;
        this.SetPoints(this.m_pointStart, this.m_pointEnd);
      }

      public PdfLinearGradientBrush(
        RectangleF rect,
        PdfColor color1,
        PdfColor color2,
        PdfLinearGradientMode mode)
        : this(color1, color2)
      {
        this.m_boundaries = rect;
        switch (mode)
        {
          case PdfLinearGradientMode.BackwardDiagonal:
            this.m_pointStart = new PointF(rect.Right, rect.Top);
            this.m_pointEnd = new PointF(rect.Left, rect.Bottom);
            break;
          case PdfLinearGradientMode.ForwardDiagonal:
            this.m_pointStart = new PointF(rect.Left, rect.Top);
            this.m_pointEnd = new PointF(rect.Right, rect.Bottom);
            break;
          case PdfLinearGradientMode.Horizontal:
            this.m_pointStart = new PointF(rect.Left, rect.Top);
            this.m_pointEnd = new PointF(rect.Right, rect.Top);
            break;
          case PdfLinearGradientMode.Vertical:
            this.m_pointStart = new PointF(rect.Left, rect.Top);
            this.m_pointEnd = new PointF(rect.Left, rect.Bottom);
            break;
          default:
            throw new ArgumentException("Unsupported linear gradient mode: " + (object) mode, nameof (mode));
        }
        this.SetPoints(this.m_pointStart, this.m_pointEnd);
      }

      public PdfLinearGradientBrush(RectangleF rect, PdfColor color1, PdfColor color2, float angle)
        : this(color1, color2)
      {
        this.m_boundaries = rect;
        angle %= 360f;
        if ((double) angle == 0.0)
        {
          this.m_pointStart = new PointF(rect.Left, rect.Top);
          this.m_pointEnd = new PointF(rect.Right, rect.Top);
        }
        else if ((double) angle == 90.0)
        {
          this.m_pointStart = new PointF(rect.Left, rect.Top);
          this.m_pointEnd = new PointF(rect.Left, rect.Bottom);
        }
        else if ((double) angle == 180.0)
        {
          this.m_pointEnd = new PointF(rect.Left, rect.Top);
          this.m_pointStart = new PointF(rect.Right, rect.Top);
        }
        else if ((double) angle == 270.0)
        {
          this.m_pointEnd = new PointF(rect.Left, rect.Top);
          this.m_pointStart = new PointF(rect.Left, rect.Bottom);
        }
        else
        {
          double num1 = Math.PI / 180.0;
          double num2 = (double) angle * num1;
          double num3 = Math.Tan(num2);
          PointF pointF1 = new PointF(this.m_boundaries.Left + (float) (((double) this.m_boundaries.Right - (double) this.m_boundaries.Left) / 2.0), this.m_boundaries.Top + (float) (((double) this.m_boundaries.Bottom - (double) this.m_boundaries.Top) / 2.0));
          float num4 = this.m_boundaries.Width / 2f * (float) Math.Cos(num2);
          double num5 = (double) num4;
          float num6 = (float) (num3 * num5);
          PointF pointF2 = PdfLinearGradientBrush.SubPoints(new PointF(num4 + pointF1.X, num6 + pointF1.Y), pointF1);
          float num7 = PdfLinearGradientBrush.MulPoints(PdfLinearGradientBrush.SubPoints(this.ChoosePoint(angle), pointF1), pointF2) / PdfLinearGradientBrush.MulPoints(pointF2, pointF2);
          this.m_pointStart = PdfLinearGradientBrush.AddPoints(pointF1, PdfLinearGradientBrush.MulPoint(pointF2, num7));
          this.m_pointEnd = PdfLinearGradientBrush.AddPoints(pointF1, PdfLinearGradientBrush.MulPoint(pointF2, -num7));
        }
        this.SetPoints(this.m_pointStart, this.m_pointEnd);
      }

      private static PointF AddPoints(PointF point1, PointF point2)
      {
        return new PointF(point1.X + point2.X, point1.Y + point2.Y);
      }

      private PointF ChoosePoint(float angle)
      {
        if ((double) angle < 90.0 && (double) angle > 0.0)
          return new PointF(this.m_boundaries.Right, this.m_boundaries.Bottom);
        if ((double) angle < 180.0 && (double) angle > 90.0)
          return new PointF(this.m_boundaries.Left, this.m_boundaries.Bottom);
        if ((double) angle < 270.0 && (double) angle > 180.0)
          return new PointF(this.m_boundaries.Left, this.m_boundaries.Top);
        if ((double) angle <= 270.0)
          throw new PdfException("Internal error.");
        return new PointF(this.m_boundaries.Right, this.m_boundaries.Top);
      }

      public override PdfBrush Clone()
      {
        PdfLinearGradientBrush brush = this.MemberwiseClone() as PdfLinearGradientBrush;
        brush.ResetPatternDictionary(new PdfDictionary(this.PatternDictionary));
        brush.Shading = new PdfDictionary();
        brush.InitShading();
        brush.SetPoints(brush.m_pointStart, brush.m_pointEnd);
        if (this.Matrix != null)
          brush.Matrix = this.Matrix.Clone();
        if (this.m_colours != null)
          brush.m_colours = this.m_colours.Clone() as PdfColor[];
        if (this.Blend != null)
          brush.Blend = this.Blend.Clone();
        else if (this.InterpolationColors != null)
          brush.InterpolationColors = this.InterpolationColors.Clone();
        brush.Extend = this.Extend;
        this.CloneBackgroundValue((PdfGradientBrush) brush);
        this.CloneAntiAliasingValue((PdfGradientBrush) brush);
        return (PdfBrush) brush;
      }

      private void InitShading()
      {
        this.ColorSpace = this.ColorSpace;
        this.Function = this.m_colourBlend.GetFunction(this.ColorSpace);
        this.Shading["ShadingType"] = (IPdfPrimitive) new PdfNumber(2);
      }

      private static PointF MulPoint(PointF point, float value)
      {
        point.X *= value;
        point.Y *= value;
        return point;
      }

      private static float MulPoints(PointF point1, PointF point2)
      {
        return (float) ((double) point1.X * (double) point2.X + (double) point1.Y * (double) point2.Y);
      }

      internal override void ResetFunction()
      {
        this.Function = this.m_colourBlend.GetFunction(this.ColorSpace);
      }

      private void SetPoints(PointF point1, PointF point2)
      {
        this.Shading["Coords"] = (IPdfPrimitive) new PdfArray()
        {
          (IPdfPrimitive) new PdfNumber(point1.X),
          (IPdfPrimitive) new PdfNumber(PdfGraphics.UpdateY(point1.Y)),
          (IPdfPrimitive) new PdfNumber(point2.X),
          (IPdfPrimitive) new PdfNumber(PdfGraphics.UpdateY(point2.Y))
        };
      }

      private static PointF SubPoints(PointF point1, PointF point2)
      {
        return new PointF(point1.X - point2.X, point1.Y - point2.Y);
      }

      public PdfBlend Blend
      {
        get => this.m_blend;
        set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (Blend));
          if (this.m_colours == null)
            throw new NotSupportedException("There is no starting and ending colours specified.");
          this.m_blend = value;
          this.m_colourBlend = this.m_blend.GenerateColorBlend(this.m_colours, this.ColorSpace);
          this.ResetFunction();
        }
      }

      public PdfExtend Extend
      {
        get
        {
          PdfExtend extend = PdfExtend.None;
          if (this.Shading[nameof (Extend)] is PdfArray pdfArray)
          {
            PdfBoolean pdfBoolean1 = pdfArray[0] as PdfBoolean;
            PdfBoolean pdfBoolean2 = pdfArray[1] as PdfBoolean;
            if (pdfBoolean1.Value)
              extend |= PdfExtend.Start;
            if (pdfBoolean2.Value)
              extend |= PdfExtend.End;
          }
          return extend;
        }
        set
        {
          PdfBoolean pdfBoolean1;
          PdfBoolean pdfBoolean2;
          if (!(this.Shading[nameof (Extend)] is PdfArray pdfArray))
          {
            pdfBoolean1 = new PdfBoolean(false);
            pdfBoolean2 = new PdfBoolean(false);
            this.Shading[nameof (Extend)] = (IPdfPrimitive) new PdfArray()
            {
              (IPdfPrimitive) pdfBoolean1,
              (IPdfPrimitive) pdfBoolean2
            };
          }
          else
          {
            pdfBoolean1 = pdfArray[0] as PdfBoolean;
            pdfBoolean2 = pdfArray[1] as PdfBoolean;
          }
          pdfBoolean1.Value = (value & PdfExtend.Start) > PdfExtend.None;
          pdfBoolean2.Value = (value & PdfExtend.End) > PdfExtend.None;
        }
      }

      public PdfColorBlend InterpolationColors
      {
        get => this.m_colourBlend;
        set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (InterpolationColors));
          this.m_blend = (PdfBlend) null;
          this.m_colours = (PdfColor[]) null;
          this.m_colourBlend = value;
          this.ResetFunction();
        }
      }

      public PdfColor[] LinearColors
      {
        get => this.m_colours;
        set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (LinearColors));
          if (value.Length < 2)
            throw new ArgumentException("The array is too small", nameof (LinearColors));
          if (this.m_colours == null)
          {
            this.m_colours = new PdfColor[2]
            {
              value[0],
              value[1]
            };
          }
          else
          {
            this.m_colours[0] = value[0];
            this.m_colours[1] = value[1];
          }
          if (this.m_blend == null)
          {
            this.m_colourBlend = new PdfColorBlend(2);
            this.m_colourBlend.Colors = this.m_colours;
            this.m_colourBlend.Positions = new float[2]
            {
              0.0f,
              1f
            };
          }
          else
            this.m_colourBlend = this.m_blend.GenerateColorBlend(this.m_colours, this.ColorSpace);
          this.ResetFunction();
        }
      }

      public RectangleF Rectangle => this.m_boundaries;
    }
}
