// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfPath
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace Syncfusion.Pdf.Graphics
{
    public class PdfPath : PdfFillElement
    {
      private bool m_bStartFigure;
      private PdfFillMode m_fillMode;
      private List<byte> m_pathTypes;
      private List<PointF> m_points;

      public PdfPath() => this.m_bStartFigure = true;

      public PdfPath(PdfBrush brush)
        : base(brush)
      {
        this.m_bStartFigure = true;
      }

      public PdfPath(PdfPen pen)
        : base(pen)
      {
        this.m_bStartFigure = true;
      }

      public PdfPath(PointF[] points, byte[] pathTypes)
      {
        this.m_bStartFigure = true;
        this.AddPath(points, pathTypes);
      }

      public PdfPath(PdfBrush brush, PdfFillMode fillMode)
        : this(brush)
      {
        this.FillMode = fillMode;
      }

      public PdfPath(PdfPen pen, PointF[] points, byte[] pathTypes)
        : base(pen)
      {
        this.m_bStartFigure = true;
        this.AddPath(points, pathTypes);
      }

      public PdfPath(PdfPen pen, PdfBrush brush, PdfFillMode fillMode)
        : base(pen, brush)
      {
        this.m_bStartFigure = true;
        this.FillMode = fillMode;
      }

      public PdfPath(PdfBrush brush, PdfFillMode fillMode, PointF[] points, byte[] pathTypes)
        : base(brush)
      {
        this.m_bStartFigure = true;
        this.AddPath(points, pathTypes);
        this.FillMode = fillMode;
      }

      public void AddArc(RectangleF rectangle, float startAngle, float sweepAngle)
      {
        this.AddArc(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, startAngle, sweepAngle);
      }

      public void AddArc(
        float x,
        float y,
        float width,
        float height,
        float startAngle,
        float sweepAngle)
      {
        List<float[]> bezierArcPoints = PdfGraphics.GetBezierArcPoints(x, y, x + width, y + height, startAngle, sweepAngle);
        List<float> points = new List<float>(8);
        for (int index = 0; index < bezierArcPoints.Count; ++index)
        {
          float[] collection = bezierArcPoints[index];
          points.Clear();
          points.AddRange((IEnumerable<float>) collection);
          this.AddPoints(points, PathPointType.Bezier);
        }
      }

      public void AddBezier(
        PointF startPoint,
        PointF firstControlPoint,
        PointF secondControlPoint,
        PointF endPoint)
      {
        this.AddBezier(startPoint.X, startPoint.Y, firstControlPoint.X, firstControlPoint.Y, secondControlPoint.X, secondControlPoint.Y, endPoint.X, endPoint.Y);
      }

      public void AddBezier(
        float startPointX,
        float startPointY,
        float firstControlPointX,
        float firstControlPointY,
        float secondControlPointX,
        float secondControlPointY,
        float endPointX,
        float endPointY)
      {
        this.AddPoints(new List<float>(8)
        {
          startPointX,
          startPointY,
          firstControlPointX,
          firstControlPointY,
          secondControlPointX,
          secondControlPointY,
          endPointX,
          endPointY
        }, PathPointType.Bezier);
      }

      public void AddEllipse(RectangleF rectangle)
      {
        this.AddEllipse(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
      }

      public void AddEllipse(float x, float y, float width, float height)
      {
        this.StartFigure();
        this.AddArc(x, y, width, height, 0.0f, 360f);
        this.CloseFigure();
      }

      public void AddLine(PointF point1, PointF point2)
      {
        this.AddLine(point1.X, point1.Y, point2.X, point2.Y);
      }

      public void AddLine(float x1, float y1, float x2, float y2)
      {
        this.AddPoints(new List<float>(4) { x1, y1, x2, y2 }, PathPointType.Line);
      }

      public void AddPath(PdfPath path) => this.AddPath(path.PathPoints, path.PathTypes);

      public void AddPath(PointF[] pathPoints, byte[] pathTypes)
      {
        if (pathPoints == null)
          throw new ArgumentNullException(nameof (pathPoints));
        if (pathTypes == null)
          throw new ArgumentNullException(nameof (pathTypes));
        if (pathPoints.Length != pathTypes.Length)
          throw new ArgumentException("The argument arrays should be of equal length.");
        this.Points.AddRange((IEnumerable<PointF>) pathPoints);
        this.Types.AddRange((IEnumerable<byte>) pathTypes);
      }

      public void AddPie(RectangleF rectangle, float startAngle, float sweepAngle)
      {
        this.AddPie(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, startAngle, sweepAngle);
      }

      public void AddPie(
        float x,
        float y,
        float width,
        float height,
        float startAngle,
        float sweepAngle)
      {
        this.StartFigure();
        this.AddArc(x, y, width, height, startAngle, sweepAngle);
        this.AddPoint(new PointF(x + width / 2f, y + height / 2f), PathPointType.Line);
        this.CloseFigure();
      }

      private void AddPoint(PointF point, PathPointType pointType)
      {
        this.Points.Add(point);
        this.Types.Add((byte) pointType);
      }

      private void AddPoints(List<float> points, PathPointType pointType)
      {
        this.AddPoints(points, pointType, 0, points.Count);
      }

      private void AddPoints(
        List<float> points,
        PathPointType pointType,
        int startIndex,
        int endIndex)
      {
        for (int index = startIndex; index < endIndex; index = index + 1 + 1)
        {
          PointF point = new PointF(points[index], points[index + 1]);
          if (index == startIndex)
          {
            if (this.PointCount <= 0 || this.m_bStartFigure)
            {
              this.AddPoint(point, PathPointType.Start);
              this.m_bStartFigure = false;
            }
            else if (point != this.LastPoint)
              this.AddPoint(point, PathPointType.Line);
          }
          else
            this.AddPoint(point, pointType);
        }
      }

      public void AddPolygon(PointF[] points)
      {
        List<float> points1 = new List<float>(points.Length * 2);
        this.StartFigure();
        foreach (PointF point in points)
        {
          points1.Add(point.X);
          points1.Add(point.Y);
        }
        this.AddPoints(points1, PathPointType.Line);
        this.CloseFigure();
      }

      public void AddRectangle(RectangleF rectangle)
      {
        this.AddRectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
      }

      public void AddRectangle(float x, float y, float width, float height)
      {
        List<float> points = new List<float>();
        this.StartFigure();
        points.Add(x);
        points.Add(y);
        points.Add(x + width);
        points.Add(y);
        points.Add(x + width);
        points.Add(y + height);
        points.Add(x);
        points.Add(y + height);
        this.AddPoints(points, PathPointType.Line);
        this.CloseFigure();
      }

      public void CloseAllFigures()
      {
        int index = 0;
        for (int count = this.m_pathTypes.Count; index < count; ++index)
        {
          PathPointType type = (PathPointType) this.Types[index];
          if (index != 0 && type == PathPointType.Start)
            this.CloseFigure(index - 1);
        }
      }

      public void CloseFigure()
      {
        if (this.PointCount > 0)
          this.CloseFigure(this.PointCount - 1);
        this.StartFigure();
      }

      private void CloseFigure(int index)
      {
        if (index < 0)
          throw new IndexOutOfRangeException();
        PathPointType pathPointType = (PathPointType) ((int) this.Types[index] | 128 /*0x80*/);
        this.Types[index] = (byte) pathPointType;
      }

      protected override void DrawInternal(PdfGraphics graphics)
      {
        if (graphics == null)
          throw new ArgumentNullException(nameof (graphics));
        graphics.DrawPath(this.GetPen(), this.Brush, this);
      }

      protected override RectangleF GetBoundsInternal()
      {
        PointF empty1 = PointF.Empty;
        PointF empty2 = PointF.Empty;
        PointF[] pathPoints = this.PathPoints;
        int index = 0;
        for (int length = pathPoints.Length; index < length; ++index)
        {
          PointF pointF = pathPoints[index];
          empty1.X = Math.Min(pointF.X, empty1.X);
          empty1.Y = Math.Min(pointF.Y, empty1.Y);
          empty2.X = Math.Max(pointF.X, empty1.X);
          empty2.Y = Math.Max(pointF.Y, empty1.Y);
        }
        return new RectangleF(empty1.X, empty1.Y, empty2.X - empty1.X, empty2.Y - empty1.Y);
      }

      public PointF GetLastPoint()
      {
        PointF lastPoint = PointF.Empty;
        int pointCount = this.PointCount;
        if (pointCount > 0 && this.m_points != null)
          lastPoint = this.m_points[pointCount - 1];
        return lastPoint;
      }

      public void StartFigure() => this.m_bStartFigure = true;

      public PdfFillMode FillMode
      {
        get => this.m_fillMode;
        set => this.m_fillMode = value;
      }

      public PointF LastPoint => this.GetLastPoint();

      public PointF[] PathPoints => this.Points.ToArray();

      public byte[] PathTypes => this.Types.ToArray();

      public int PointCount
      {
        get
        {
          int pointCount = 0;
          if (this.m_points != null)
            pointCount = this.m_points.Count;
          return pointCount;
        }
      }

      internal List<PointF> Points
      {
        get
        {
          if (this.m_points == null)
            this.m_points = new List<PointF>();
          return this.m_points;
        }
      }

      internal List<byte> Types
      {
        get
        {
          if (this.m_pathTypes == null)
            this.m_pathTypes = new List<byte>();
          return this.m_pathTypes;
        }
      }
    }
}
