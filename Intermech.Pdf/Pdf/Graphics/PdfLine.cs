// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfLine
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Drawing;


namespace Syncfusion.Pdf.Graphics
{
    public class PdfLine : PdfDrawElement
    {
      private float m_x1;
      private float m_x2;
      private float m_y1;
      private float m_y2;

      private PdfLine()
      {
      }

      public PdfLine(PointF point1, PointF point2)
        : this(point1.X, point1.Y, point2.X, point2.Y)
      {
      }

      public PdfLine(PdfPen pen, PointF point1, PointF point2)
        : base(pen)
      {
        this.m_x1 = point1.X;
        this.m_y1 = point1.Y;
        this.m_x2 = point2.X;
        this.m_y2 = point2.Y;
      }

      public PdfLine(float x1, float y1, float x2, float y2)
      {
        this.m_x1 = x1;
        this.m_y1 = y1;
        this.m_x2 = x2;
        this.m_y2 = y2;
      }

      public PdfLine(PdfPen pen, float x1, float y1, float x2, float y2)
        : base(pen)
      {
        this.m_x1 = x1;
        this.m_y1 = y1;
        this.m_x2 = x2;
        this.m_y2 = y2;
      }

      protected override void DrawInternal(PdfGraphics graphics)
      {
        if (graphics == null)
          throw new ArgumentNullException(nameof (graphics));
        graphics.DrawLine(this.GetPen(), this.X1, this.Y1, this.X2, this.Y2);
      }

      protected override RectangleF GetBoundsInternal()
      {
        float x = Math.Min(this.X1, this.X2);
        float num1 = Math.Max(this.X1, this.X2);
        float y = Math.Min(this.Y1, this.Y2);
        float num2 = Math.Max(this.Y1, this.Y2);
        return new RectangleF(x, y, num1 - x, num2 - y);
      }

      public float X1
      {
        get => this.m_x1;
        set => this.m_x1 = value;
      }

      public float X2
      {
        get => this.m_x2;
        set => this.m_x2 = value;
      }

      public float Y1
      {
        get => this.m_y1;
        set => this.m_y1 = value;
      }

      public float Y2
      {
        get => this.m_y2;
        set => this.m_y2 = value;
      }
    }
}
