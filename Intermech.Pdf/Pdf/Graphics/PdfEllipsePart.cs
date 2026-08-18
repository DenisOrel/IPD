// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfEllipsePart
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Drawing;


namespace Syncfusion.Pdf.Graphics
{
    public abstract class PdfEllipsePart : PdfRectangleArea
    {
      private float m_startAngle;
      private float m_sweepAngle;

      protected PdfEllipsePart()
      {
      }

      protected PdfEllipsePart(RectangleF rectangle, float startAngle, float sweepAngle)
        : base(rectangle)
      {
        this.m_startAngle = startAngle;
        this.m_sweepAngle = sweepAngle;
      }

      protected PdfEllipsePart(
        PdfPen pen,
        PdfBrush brush,
        RectangleF rectangle,
        float startAngle,
        float sweepAngle)
        : base(pen, brush, rectangle)
      {
        this.m_startAngle = startAngle;
        this.m_sweepAngle = sweepAngle;
      }

      protected PdfEllipsePart(
        float x,
        float y,
        float width,
        float height,
        float startAngle,
        float sweepAngle)
        : base(x, y, width, height)
      {
        this.m_startAngle = startAngle;
        this.m_sweepAngle = sweepAngle;
      }

      protected PdfEllipsePart(
        PdfPen pen,
        PdfBrush brush,
        float x,
        float y,
        float width,
        float height,
        float startAngle,
        float sweepAngle)
        : base(pen, brush, x, y, width, height)
      {
        this.m_startAngle = startAngle;
        this.m_sweepAngle = sweepAngle;
      }

      public float StartAngle
      {
        get => this.m_startAngle;
        set => this.m_startAngle = value;
      }

      public float SweepAngle
      {
        get => this.m_sweepAngle;
        set => this.m_sweepAngle = value;
      }
    }
}
