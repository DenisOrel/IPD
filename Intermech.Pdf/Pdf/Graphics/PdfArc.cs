// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfArc
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Drawing;


namespace Syncfusion.Pdf.Graphics
{
    public class PdfArc : PdfEllipsePart
    {
      protected PdfArc()
      {
      }

      public PdfArc(RectangleF rectangle, float startAngle, float sweepAngle)
        : base(rectangle, startAngle, sweepAngle)
      {
      }

      public PdfArc(PdfPen pen, RectangleF rectangle, float startAngle, float sweepAngle)
        : base(pen, (PdfBrush) null, rectangle, startAngle, sweepAngle)
      {
      }

      public PdfArc(float width, float height, float startAngle, float sweepAngle)
        : this(0.0f, 0.0f, width, height, startAngle, sweepAngle)
      {
      }

      public PdfArc(PdfPen pen, float width, float height, float startAngle, float sweepAngle)
        : this(pen, 0.0f, 0.0f, width, height, startAngle, sweepAngle)
      {
      }

      public PdfArc(float x, float y, float width, float height, float startAngle, float sweepAngle)
        : base(x, y, width, height, startAngle, sweepAngle)
      {
      }

      public PdfArc(
        PdfPen pen,
        float x,
        float y,
        float width,
        float height,
        float startAngle,
        float sweepAngle)
        : base(pen, (PdfBrush) null, x, y, width, height, startAngle, sweepAngle)
      {
      }

      protected override void DrawInternal(PdfGraphics graphics)
      {
        if (graphics == null)
          throw new ArgumentNullException(nameof (graphics));
        graphics.DrawArc(this.GetPen(), this.Bounds, this.StartAngle, this.SweepAngle);
      }
    }
}
