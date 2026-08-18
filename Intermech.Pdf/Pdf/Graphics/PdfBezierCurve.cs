// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfBezierCurve
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Drawing;


namespace Syncfusion.Pdf.Graphics
{
    public class PdfBezierCurve : PdfDrawElement
    {
      private PointF m_endPoint;
      private PointF m_firstControlPoint;
      private PointF m_secondControlPoint;
      private PointF m_startPoint;

      protected PdfBezierCurve()
      {
        this.m_startPoint = PointF.Empty;
        this.m_firstControlPoint = PointF.Empty;
        this.m_secondControlPoint = PointF.Empty;
        this.m_endPoint = PointF.Empty;
      }

      public PdfBezierCurve(
        PointF startPoint,
        PointF firstControlPoint,
        PointF secondControlPoint,
        PointF endPoint)
      {
        this.m_startPoint = PointF.Empty;
        this.m_firstControlPoint = PointF.Empty;
        this.m_secondControlPoint = PointF.Empty;
        this.m_endPoint = PointF.Empty;
        this.m_startPoint = startPoint;
        this.m_firstControlPoint = firstControlPoint;
        this.m_secondControlPoint = secondControlPoint;
        this.m_endPoint = endPoint;
      }

      public PdfBezierCurve(
        PdfPen pen,
        PointF startPoint,
        PointF firstControlPoint,
        PointF secondControlPoint,
        PointF endPoint)
        : base(pen)
      {
        this.m_startPoint = PointF.Empty;
        this.m_firstControlPoint = PointF.Empty;
        this.m_secondControlPoint = PointF.Empty;
        this.m_endPoint = PointF.Empty;
        this.m_startPoint = startPoint;
        this.m_firstControlPoint = firstControlPoint;
        this.m_secondControlPoint = secondControlPoint;
        this.m_endPoint = endPoint;
      }

      public PdfBezierCurve(
        float startPointX,
        float startPointY,
        float firstControlPointX,
        float firstControlPointY,
        float secondControlPointX,
        float secondControlPointY,
        float endPointX,
        float endPointY)
      {
        this.m_startPoint = PointF.Empty;
        this.m_firstControlPoint = PointF.Empty;
        this.m_secondControlPoint = PointF.Empty;
        this.m_endPoint = PointF.Empty;
        this.m_startPoint.X = startPointX;
        this.m_startPoint.Y = startPointY;
        this.m_firstControlPoint.X = firstControlPointX;
        this.m_firstControlPoint.Y = firstControlPointY;
        this.m_secondControlPoint.X = secondControlPointX;
        this.m_secondControlPoint.Y = secondControlPointY;
        this.m_endPoint.X = endPointX;
        this.m_endPoint.Y = endPointY;
      }

      public PdfBezierCurve(
        PdfPen pen,
        float startPointX,
        float startPointY,
        float firstControlPointX,
        float firstControlPointY,
        float secondControlPointX,
        float secondControlPointY,
        float endPointX,
        float endPointY)
        : base(pen)
      {
        this.m_startPoint = PointF.Empty;
        this.m_firstControlPoint = PointF.Empty;
        this.m_secondControlPoint = PointF.Empty;
        this.m_endPoint = PointF.Empty;
        this.m_startPoint.X = startPointX;
        this.m_startPoint.Y = startPointY;
        this.m_firstControlPoint.X = firstControlPointX;
        this.m_firstControlPoint.Y = firstControlPointY;
        this.m_secondControlPoint.X = secondControlPointX;
        this.m_secondControlPoint.Y = secondControlPointY;
        this.m_endPoint.X = endPointX;
        this.m_endPoint.Y = endPointY;
      }

      protected override void DrawInternal(PdfGraphics graphics)
      {
        if (graphics == null)
          throw new ArgumentNullException(nameof (graphics));
        graphics.DrawBezier(this.GetPen(), this.StartPoint, this.FirstControlPoint, this.SecondControlPoint, this.EndPoint);
      }

      protected override RectangleF GetBoundsInternal() => throw new NotImplementedException();

      public PointF EndPoint
      {
        get => this.m_endPoint;
        set => this.m_endPoint = value;
      }

      public PointF FirstControlPoint
      {
        get => this.m_firstControlPoint;
        set => this.m_firstControlPoint = value;
      }

      public PointF SecondControlPoint
      {
        get => this.m_secondControlPoint;
        set => this.m_secondControlPoint = value;
      }

      public PointF StartPoint
      {
        get => this.m_startPoint;
        set => this.m_startPoint = value;
      }
    }
}
