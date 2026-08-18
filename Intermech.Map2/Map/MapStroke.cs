// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapStroke
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace Intermech.Map
{
    [Serializable]
    public class MapStroke : MapShape
    {
      public const int ChangedAddPoint = 1201;
      public const int ChangedRemovePoint = 1202;
      public const int ChangedModifiedPoint = 1203;
      public const int ChangedAllPoints = 1204;
      public const int ChangedStyle = 1205;
      public const int ChangedCurviness = 1206;
      public const int ChangedHighlightPen = 1236;
      public const int ChangedHighlight = 1237;
      public const int ChangedHighlightWhenSelected = 1238;
      public const int ChangedToArrowHead = 1250;
      public const int ChangedToArrowLength = 1251;
      public const int ChangedToArrowShaftLength = 1252;
      public const int ChangedToArrowWidth = 1253;
      public const int ChangedToArrowFilled = 1254;
      public const int ChangedToArrowStyle = 1255;
      public const int ChangedFromArrowHead = 1260;
      public const int ChangedFromArrowLength = 1261;
      public const int ChangedFromArrowShaftLength = 1262;
      public const int ChangedFromArrowWidth = 1263;
      public const int ChangedFromArrowFilled = 1264;
      public const int ChangedFromArrowStyle = 1265;
      public const int ChangedPenWidth = 1266;
      public const int ChangedHighlightPenWidth = 1267;
      private const float DEFAULT_ARROW_WIDTH = 8f;
      private const bool DEFAULT_ARROW_FILLED = true;
      private const float DEFAULT_ARROW_LENGTH = 10f;
      private const int DEFAULT_ARROW_POLYGON_SIDES = 4;
      private const float DEFAULT_ARROW_SHAFT_LENGTH = 8f;
      private const MapStrokeArrowheadStyle DEFAULT_ARROW_STYLE = MapStrokeArrowheadStyle.Polygon;
      private const int flagHighlightWhenSelected = 8388608 /*0x800000*/;
      private const int flagStrokeArrowEnd = 2097152 /*0x200000*/;
      private const int flagStrokeArrowStart = 1048576 /*0x100000*/;
      private const int flagStrokeHighlight = 4194304 /*0x400000*/;
      private const int LINE_FUZZ = 3;
      private float _curviness;
      private MapStroke.ArrowInfo _fromArrowInfo;
      private MapShape.MapPenInfo _highlightPenInfo;
      private PointF[] _points;
      private int _pointsCount;
      private MapStrokeStyle _style;
      private MapStroke.ArrowInfo _toArrowInfo;
      private static float[] _intersections = new float[50];

      public MapStroke()
      {
        this._style = MapStrokeStyle.Line;
        this._pointsCount = 0;
        this._points = new PointF[6];
        this._toArrowInfo = (MapStroke.ArrowInfo) null;
        this._fromArrowInfo = (MapStroke.ArrowInfo) null;
        this._curviness = 10f;
        this._highlightPenInfo = MapShape.GetPenInfo((Pen) null);
        this.InternalFlags |= 512 /*0x0200*/;
        this.Brush = MapShape.Brushes_Black;
      }

      private void addLine(GraphicsPath path, float offx, float offy, PointF from, PointF to)
      {
        if (this.Style != MapStrokeStyle.RoundedLineWithJumpOvers)
        {
          path.AddLine(from.X + offx, from.Y + offy, to.X + offx, to.Y + offy);
        }
        else
        {
          float num1 = 10f;
          float num2 = num1 / 2f;
          int pointsCount = this.PointsCount;
          lock (MapStroke._intersections)
          {
            float[] intersections1 = MapStroke._intersections;
            int intersections2 = this.getIntersections(from, to, intersections1);
            PointF pointF1 = from;
            if (intersections2 > 0)
            {
              if ((double) from.Y == (double) to.Y)
              {
                if ((double) from.X < (double) to.X)
                {
                  int index = 0;
                  while (index < intersections2)
                  {
                    float num3 = Math.Max(from.X, Math.Min(intersections1[index++] - num2, to.X - num1));
                    path.AddLine(pointF1.X + offx, pointF1.Y + offy, num3 + offx, to.Y + offy);
                    pointF1 = new PointF(num3 + offx, to.Y + offy);
                    float x = Math.Min(num3 + num1, to.X);
                    while (index < intersections2)
                    {
                      float num4 = intersections1[index];
                      if ((double) num4 < (double) x + (double) num1)
                      {
                        ++index;
                        x = Math.Min(num4 + num2, to.X);
                      }
                      else
                        break;
                    }
                    PointF pointF2 = new PointF((float) (((double) num3 + (double) x) / 2.0), to.Y - num1);
                    PointF pointF3 = new PointF(x, to.Y);
                    path.AddBezier(pointF1.X, pointF1.Y, pointF1.X, pointF2.Y, pointF3.X, pointF2.Y, pointF3.X, pointF3.Y);
                    pointF1 = pointF3;
                  }
                }
                else
                {
                  int index = intersections2 - 1;
                  while (index >= 0)
                  {
                    float num5 = Math.Min(from.X, Math.Max(intersections1[index--] + num2, to.X + num1));
                    path.AddLine(pointF1.X + offx, pointF1.Y + offy, num5 + offx, to.Y + offy);
                    pointF1 = new PointF(num5 + offx, to.Y + offy);
                    float x = Math.Max(num5 - num1, to.X);
                    while (index >= 0)
                    {
                      float num6 = intersections1[index];
                      if ((double) num6 > (double) x - (double) num1)
                      {
                        --index;
                        x = Math.Max(num6 - num2, to.X);
                      }
                      else
                        break;
                    }
                    PointF pointF4 = new PointF((float) (((double) num5 + (double) x) / 2.0), to.Y - num1);
                    PointF pointF5 = new PointF(x, to.Y);
                    path.AddBezier(pointF1.X, pointF1.Y, pointF1.X, pointF4.Y, pointF5.X, pointF4.Y, pointF5.X, pointF5.Y);
                    pointF1 = pointF5;
                  }
                }
              }
              else if ((double) from.X == (double) to.X)
              {
                if ((double) from.Y < (double) to.Y)
                {
                  int index = 0;
                  while (index < intersections2)
                  {
                    float num7 = Math.Max(from.Y, Math.Min(intersections1[index++] - num2, to.Y - num1));
                    path.AddLine(pointF1.X + offx, pointF1.Y + offy, to.X + offx, num7 + offy);
                    pointF1 = new PointF(to.X + offx, num7 + offy);
                    float y = Math.Min(num7 + num1, to.Y);
                    while (index < intersections2)
                    {
                      float num8 = intersections1[index];
                      if ((double) num8 < (double) y + (double) num1)
                      {
                        ++index;
                        y = Math.Min(num8 + num2, to.Y);
                      }
                      else
                        break;
                    }
                    PointF pointF6 = new PointF(to.X - num1, (float) (((double) num7 + (double) y) / 2.0));
                    PointF pointF7 = new PointF(to.X, y);
                    path.AddBezier(pointF1.X, pointF1.Y, pointF6.X, pointF1.Y, pointF6.X, pointF7.Y, pointF7.X, pointF7.Y);
                    pointF1 = pointF7;
                  }
                }
                else
                {
                  int index = intersections2 - 1;
                  while (index >= 0)
                  {
                    float num9 = Math.Min(from.Y, Math.Max(intersections1[index--] + num2, to.Y + num1));
                    path.AddLine(pointF1.X + offx, pointF1.Y + offy, to.X + offx, num9 + offy);
                    pointF1 = new PointF(to.X + offx, num9 + offy);
                    float y = Math.Max(num9 - num1, to.Y);
                    while (index >= 0)
                    {
                      float num10 = intersections1[index];
                      if ((double) num10 > (double) y - (double) num1)
                      {
                        --index;
                        y = Math.Max(num10 - num2, to.Y);
                      }
                      else
                        break;
                    }
                    PointF pointF8 = new PointF(to.X - num1, (float) (((double) num9 + (double) y) / 2.0));
                    PointF pointF9 = new PointF(to.X, y);
                    path.AddBezier(pointF1.X, pointF1.Y, pointF8.X, pointF1.Y, pointF8.X, pointF9.Y, pointF9.X, pointF9.Y);
                    pointF1 = pointF9;
                  }
                }
              }
            }
            path.AddLine(pointF1.X + offx, pointF1.Y + offy, to.X + offx, to.Y + offy);
          }
        }
      }

      private PointF addLineAndCorner(
        GraphicsPath path,
        float offx,
        float offy,
        PointF a,
        PointF b,
        PointF c)
      {
        if ((double) a.Y == (double) b.Y && (double) b.X == (double) c.X)
        {
          float num1 = Math.Min(Math.Min(Math.Abs(this.Curviness), Math.Abs(b.X - a.X) / 2f), Math.Abs(c.Y - b.Y) / 2f);
          float num2 = num1;
          if ((double) num2 < 1.0 || (double) num1 < 1.0)
          {
            this.addLine(path, offx, offy, a, b);
            return b;
          }
          PointF to = b;
          PointF pointF = b;
          float sweepAngle = 90f;
          RectangleF rect = new RectangleF(0.0f, 0.0f, num2 * 2f, num1 * 2f);
          float startAngle;
          if ((double) b.X > (double) a.X)
          {
            to.X = b.X - num2;
            if ((double) c.Y > (double) b.Y)
            {
              pointF.Y = b.Y + num1;
              startAngle = 270f;
              rect.X = b.X - num2 * 2f;
              rect.Y = b.Y;
            }
            else
            {
              pointF.Y = b.Y - num1;
              startAngle = 90f;
              sweepAngle = -90f;
              rect.X = b.X - num2 * 2f;
              rect.Y = b.Y - num1 * 2f;
            }
          }
          else
          {
            to.X = b.X + num2;
            if ((double) c.Y > (double) b.Y)
            {
              pointF.Y = b.Y + num1;
              startAngle = 270f;
              sweepAngle = -90f;
              rect.X = b.X;
              rect.Y = b.Y;
            }
            else
            {
              pointF.Y = b.Y - num1;
              startAngle = 90f;
              rect.X = b.X;
              rect.Y = b.Y - num1 * 2f;
            }
          }
          this.addLine(path, offx, offy, a, to);
          rect.X += offx;
          rect.Y += offy;
          path.AddArc(rect, startAngle, sweepAngle);
          return pointF;
        }
        if ((double) a.X == (double) b.X && (double) b.Y == (double) c.Y)
        {
          float num3 = Math.Min(Math.Min(Math.Abs(this.Curviness), Math.Abs(b.Y - a.Y) / 2f), Math.Abs(c.X - b.X) / 2f);
          float num4 = num3;
          if ((double) num3 < 1.0 || (double) num4 < 1.0)
          {
            this.addLine(path, offx, offy, a, b);
            return b;
          }
          PointF to = b;
          PointF pointF = b;
          float sweepAngle = 90f;
          RectangleF rect = new RectangleF(0.0f, 0.0f, num3 * 2f, num4 * 2f);
          float startAngle;
          if ((double) b.Y > (double) a.Y)
          {
            to.Y = b.Y - num4;
            if ((double) c.X > (double) b.X)
            {
              pointF.X = b.X + num3;
              startAngle = 180f;
              sweepAngle = -90f;
              rect.Y = b.Y - num4 * 2f;
              rect.X = b.X;
            }
            else
            {
              pointF.X = b.X - num3;
              startAngle = 0.0f;
              rect.Y = b.Y - num4 * 2f;
              rect.X = b.X - num3 * 2f;
            }
          }
          else
          {
            to.Y = b.Y + num4;
            if ((double) c.X > (double) b.X)
            {
              pointF.X = b.X + num3;
              startAngle = 180f;
              rect.Y = b.Y;
              rect.X = b.X;
            }
            else
            {
              pointF.X = b.X - num3;
              startAngle = 0.0f;
              sweepAngle = -90f;
              rect.Y = b.Y;
              rect.X = b.X - num3 * 2f;
            }
          }
          this.addLine(path, offx, offy, a, to);
          rect.X += offx;
          rect.Y += offy;
          path.AddArc(rect, startAngle, sweepAngle);
          return pointF;
        }
        this.addLine(path, offx, offy, a, b);
        return b;
      }

      public virtual int AddPoint(PointF p)
      {
        this.ResetPath();
        int length = this._points.Length;
        if (this._pointsCount >= length)
        {
          PointF[] destinationArray = new PointF[Math.Max(length * 2, this._pointsCount + 1)];
          Array.Copy((Array) this._points, 0, (Array) destinationArray, 0, length);
          this._points = destinationArray;
        }
        int index = this._pointsCount++;
        this._points[index] = p;
        this.InvalidBounds = true;
        this.Changed(1201, index, (object) null, MapObject.MakeRect(p), index, (object) null, MapObject.MakeRect(p));
        return index;
      }

      public int AddPoint(float x, float y) => this.AddPoint(new PointF(x, y));

      public override void AddSelectionHandles(MapSelection sel, MapObject selectedObj)
      {
        sel.RemoveHandles((MapObject) this);
        if (this.HighlightWhenSelected)
        {
          bool skipsUndoManager = this.SkipsUndoManager;
          this.SkipsUndoManager = true;
          this.Highlight = true;
          this.SkipsUndoManager = skipsUndoManager;
        }
        else
        {
          int lastPickIndex = this.LastPickIndex;
          if (this.CanResize())
          {
            if (this.CanReshape())
            {
              for (int firstPickIndex = this.FirstPickIndex; firstPickIndex <= lastPickIndex; ++firstPickIndex)
              {
                PointF point = this.GetPoint(firstPickIndex);
                sel.CreateResizeHandle((MapObject) this, selectedObj, point, 8192 /*0x2000*/ + firstPickIndex, true);
              }
            }
            else
              base.AddSelectionHandles(sel, selectedObj);
          }
          else
          {
            for (int firstPickIndex = this.FirstPickIndex; firstPickIndex <= lastPickIndex; ++firstPickIndex)
            {
              PointF point = this.GetPoint(firstPickIndex);
              sel.CreateResizeHandle((MapObject) this, selectedObj, point, 0, false);
            }
          }
        }
      }

      private void addStroke(
        GraphicsPath path,
        float offx,
        float offy,
        PointF[] fromPoly,
        PointF[] toPoly)
      {
        int pointsCount = this.PointsCount;
        if (this.Style == MapStrokeStyle.Bezier && pointsCount >= 4)
        {
          for (int i = 3; i < pointsCount; i += 3)
          {
            PointF pointF1 = fromPoly == null || i - 3 != 0 || (double) this.FromArrowShaftLength <= 0.0 || !(fromPoly[2] == this.GetPoint(0)) ? this.GetPoint(i - 3) : fromPoly[0];
            PointF point1 = this.GetPoint(i - 2);
            if (i + 3 >= pointsCount)
              i = pointsCount - 1;
            PointF point2 = this.GetPoint(i - 1);
            PointF pointF2 = toPoly == null || i != pointsCount - 1 || (double) this.ToArrowShaftLength <= 0.0 || !(toPoly[2] == this.GetPoint(i)) ? this.GetPoint(i) : toPoly[0];
            path.AddBezier(pointF1.X + offx, pointF1.Y + offy, point1.X + offx, point1.Y + offy, point2.X + offx, point2.Y + offy, pointF2.X + offx, pointF2.Y + offy);
          }
        }
        else
        {
          if (pointsCount < 2)
            return;
          if (pointsCount == 2 || this.Style == MapStrokeStyle.Line || this.Style == MapStrokeStyle.Bezier || (double) Math.Abs(this.Curviness) < 1.0 && this.Style != MapStrokeStyle.RoundedLineWithJumpOvers)
          {
            for (int i = 1; i < pointsCount; ++i)
            {
              PointF pointF3 = fromPoly == null || i - 1 != 0 || (double) this.FromArrowShaftLength <= 0.0 || !(fromPoly[2] == this.GetPoint(0)) ? this.GetPoint(i - 1) : fromPoly[0];
              PointF pointF4 = toPoly == null || i != pointsCount - 1 || (double) this.ToArrowShaftLength <= 0.0 || !(toPoly[2] == this.GetPoint(i)) ? this.GetPoint(i) : toPoly[0];
              path.AddLine(pointF3.X + offx, pointF3.Y + offy, pointF4.X + offx, pointF4.Y + offy);
            }
          }
          else
          {
            PointF pointF = this.GetPoint(0);
            if (fromPoly != null && (double) this.FromArrowShaftLength > 0.0 && fromPoly[2] == pointF)
              pointF = fromPoly[0];
            int i1;
            for (int i2 = 1; i2 < pointsCount; i2 = i1)
            {
              int i3 = this.furthestPoint(pointF, i2, i2 > 1);
              PointF point3 = this.GetPoint(i3);
              if (i3 >= pointsCount - 1)
              {
                if (toPoly != null && (double) this.ToArrowShaftLength > 0.0 && toPoly[2] == point3)
                  point3 = toPoly[0];
                if (!(pointF != point3))
                  break;
                this.addLine(path, offx, offy, pointF, point3);
                break;
              }
              i1 = this.furthestPoint(point3, i3 + 1, i3 < pointsCount - 3);
              PointF point4 = this.GetPoint(i1);
              if (toPoly != null && i1 == pointsCount - 1 && (double) this.ToArrowShaftLength > 0.0 && toPoly[2] == point4)
                point4 = toPoly[0];
              pointF = this.addLineAndCorner(path, offx, offy, pointF, point3, point4);
            }
          }
        }
      }

      internal static RectangleF BezierBounds(PointF b0, PointF b1, PointF b2, PointF b3)
      {
        PointF pointF1 = b0;
        PointF pointF2 = new PointF((float) (((double) b0.X + (double) b1.X) / 2.0), (float) (((double) b0.Y + (double) b1.Y) / 2.0));
        PointF pointF3 = new PointF((float) (((double) b1.X + (double) b2.X) / 2.0), (float) (((double) b1.Y + (double) b2.Y) / 2.0));
        PointF pointF4 = new PointF((float) (((double) b2.X + (double) b3.X) / 2.0), (float) (((double) b2.Y + (double) b3.Y) / 2.0));
        PointF pointF5 = b3;
        PointF pointF6 = pointF1;
        PointF pointF7 = new PointF((float) (((double) pointF1.X + (double) pointF2.X) / 2.0), (float) (((double) pointF1.Y + (double) pointF2.Y) / 2.0));
        PointF pointF8 = new PointF((float) (((double) pointF2.X + (double) pointF3.X) / 2.0), (float) (((double) pointF2.Y + (double) pointF3.Y) / 2.0));
        PointF pointF9 = new PointF((float) (((double) pointF3.X + (double) pointF4.X) / 2.0), (float) (((double) pointF3.Y + (double) pointF4.Y) / 2.0));
        PointF pointF10 = new PointF((float) (((double) pointF4.X + (double) pointF5.X) / 2.0), (float) (((double) pointF4.Y + (double) pointF5.Y) / 2.0));
        PointF pointF11 = pointF5;
        float x1 = pointF6.X;
        float x2 = pointF6.X;
        if ((double) pointF7.X < (double) x1)
          x1 = pointF7.X;
        else if ((double) pointF7.X > (double) x2)
          x2 = pointF7.X;
        if ((double) pointF8.X < (double) x1)
          x1 = pointF8.X;
        else if ((double) pointF8.X > (double) x2)
          x2 = pointF8.X;
        if ((double) pointF9.X < (double) x1)
          x1 = pointF9.X;
        else if ((double) pointF9.X > (double) x2)
          x2 = pointF9.X;
        if ((double) pointF10.X < (double) x1)
          x1 = pointF10.X;
        else if ((double) pointF10.X > (double) x2)
          x2 = pointF10.X;
        if ((double) pointF11.X < (double) x1)
          x1 = pointF11.X;
        else if ((double) pointF11.X > (double) x2)
          x2 = pointF11.X;
        float y1 = pointF6.Y;
        float y2 = pointF6.Y;
        if ((double) pointF7.Y < (double) y1)
          y1 = pointF7.Y;
        else if ((double) pointF7.Y > (double) y2)
          y2 = pointF7.Y;
        if ((double) pointF8.Y < (double) y1)
          y1 = pointF8.Y;
        else if ((double) pointF8.Y > (double) y2)
          y2 = pointF8.Y;
        if ((double) pointF9.Y < (double) y1)
          y1 = pointF9.Y;
        else if ((double) pointF9.Y > (double) y2)
          y2 = pointF9.Y;
        if ((double) pointF10.Y < (double) y1)
          y1 = pointF10.Y;
        else if ((double) pointF10.Y > (double) y2)
          y2 = pointF10.Y;
        if ((double) pointF11.Y < (double) y1)
          y1 = pointF11.Y;
        else if ((double) pointF11.Y > (double) y2)
          y2 = pointF11.Y;
        return new RectangleF(x1 - 10f, y1 - 10f, (float) ((double) x2 - (double) x1 + 20.0), (float) ((double) y2 - (double) y1 + 20.0));
      }

      internal static bool BezierContainsPoint(
        PointF b0,
        PointF b1,
        PointF b2,
        PointF b3,
        float fuzz,
        PointF p)
      {
        PointF pointF1 = b0;
        PointF pointF2 = new PointF((float) (((double) b0.X + (double) b1.X) / 2.0), (float) (((double) b0.Y + (double) b1.Y) / 2.0));
        PointF pointF3 = new PointF((float) (((double) b1.X + (double) b2.X) / 2.0), (float) (((double) b1.Y + (double) b2.Y) / 2.0));
        PointF pointF4 = new PointF((float) (((double) b2.X + (double) b3.X) / 2.0), (float) (((double) b2.Y + (double) b3.Y) / 2.0));
        PointF pointF5 = b3;
        PointF a1 = pointF1;
        PointF a2 = new PointF((float) (((double) pointF1.X + (double) pointF2.X) / 2.0), (float) (((double) pointF1.Y + (double) pointF2.Y) / 2.0));
        PointF pointF6 = new PointF((float) (((double) pointF2.X + (double) pointF3.X) / 2.0), (float) (((double) pointF2.Y + (double) pointF3.Y) / 2.0));
        PointF pointF7 = new PointF((float) (((double) pointF3.X + (double) pointF4.X) / 2.0), (float) (((double) pointF3.Y + (double) pointF4.Y) / 2.0));
        PointF pointF8 = new PointF((float) (((double) pointF4.X + (double) pointF5.X) / 2.0), (float) (((double) pointF4.Y + (double) pointF5.Y) / 2.0));
        PointF b4 = pointF5;
        PointF b5 = a2;
        double fuzz1 = (double) fuzz;
        PointF p1 = p;
        return MapStroke.LineContainsPoint(a1, b5, (float) fuzz1, p1) || MapStroke.LineContainsPoint(a2, pointF6, fuzz, p) || MapStroke.LineContainsPoint(pointF6, pointF7, fuzz, p) || MapStroke.LineContainsPoint(pointF7, pointF8, fuzz, p) || MapStroke.LineContainsPoint(pointF8, b4, fuzz, p);
      }

      internal static void BezierMidPoint(
        PointF b0,
        PointF b1,
        PointF b2,
        PointF b3,
        out PointF v,
        out PointF w)
      {
        PointF pointF1 = new PointF((float) (((double) b0.X + (double) b1.X) / 2.0), (float) (((double) b0.Y + (double) b1.Y) / 2.0));
        PointF pointF2 = new PointF((float) (((double) b1.X + (double) b2.X) / 2.0), (float) (((double) b1.Y + (double) b2.Y) / 2.0));
        PointF pointF3 = new PointF((float) (((double) b2.X + (double) b3.X) / 2.0), (float) (((double) b2.Y + (double) b3.Y) / 2.0));
        v = new PointF((float) (((double) pointF1.X + (double) pointF2.X) / 2.0), (float) (((double) pointF1.Y + (double) pointF2.Y) / 2.0));
        w = new PointF((float) (((double) pointF2.X + (double) pointF3.X) / 2.0), (float) (((double) pointF2.Y + (double) pointF3.Y) / 2.0));
      }

      internal static bool BezierNearestIntersectionOnLine(
        PointF b0,
        PointF b1,
        PointF b2,
        PointF b3,
        PointF p1,
        PointF p2,
        out PointF result)
      {
        PointF pointF1 = b0;
        PointF pointF2 = new PointF((float) (((double) b0.X + (double) b1.X) / 2.0), (float) (((double) b0.Y + (double) b1.Y) / 2.0));
        PointF pointF3 = new PointF((float) (((double) b1.X + (double) b2.X) / 2.0), (float) (((double) b1.Y + (double) b2.Y) / 2.0));
        PointF pointF4 = new PointF((float) (((double) b2.X + (double) b3.X) / 2.0), (float) (((double) b2.Y + (double) b3.Y) / 2.0));
        PointF pointF5 = b3;
        PointF a1 = pointF1;
        PointF a2 = new PointF((float) (((double) pointF1.X + (double) pointF2.X) / 2.0), (float) (((double) pointF1.Y + (double) pointF2.Y) / 2.0));
        PointF pointF6 = new PointF((float) (((double) pointF2.X + (double) pointF3.X) / 2.0), (float) (((double) pointF2.Y + (double) pointF3.Y) / 2.0));
        PointF pointF7 = new PointF((float) (((double) pointF3.X + (double) pointF4.X) / 2.0), (float) (((double) pointF3.Y + (double) pointF4.Y) / 2.0));
        PointF pointF8 = new PointF((float) (((double) pointF4.X + (double) pointF5.X) / 2.0), (float) (((double) pointF4.Y + (double) pointF5.Y) / 2.0));
        PointF b4 = pointF5;
        float num1 = 1E+21f;
        PointF pointF9 = new PointF();
        PointF b5 = a2;
        PointF p = p1;
        PointF q = p2;
        PointF result1;
        ref PointF local = ref result1;
        if (MapStroke.NearestIntersectionOnLine(a1, b5, p, q, out local))
        {
          float num2 = (float) (((double) result1.X - (double) p1.X) * ((double) result1.X - (double) p1.X) + ((double) result1.Y - (double) p1.Y) * ((double) result1.Y - (double) p1.Y));
          if ((double) num2 < (double) num1)
          {
            num1 = num2;
            pointF9 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(a2, pointF6, p1, p2, out result1))
        {
          float num3 = (float) (((double) result1.X - (double) p1.X) * ((double) result1.X - (double) p1.X) + ((double) result1.Y - (double) p1.Y) * ((double) result1.Y - (double) p1.Y));
          if ((double) num3 < (double) num1)
          {
            num1 = num3;
            pointF9 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(pointF6, pointF7, p1, p2, out result1))
        {
          float num4 = (float) (((double) result1.X - (double) p1.X) * ((double) result1.X - (double) p1.X) + ((double) result1.Y - (double) p1.Y) * ((double) result1.Y - (double) p1.Y));
          if ((double) num4 < (double) num1)
          {
            num1 = num4;
            pointF9 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(pointF7, pointF8, p1, p2, out result1))
        {
          float num5 = (float) (((double) result1.X - (double) p1.X) * ((double) result1.X - (double) p1.X) + ((double) result1.Y - (double) p1.Y) * ((double) result1.Y - (double) p1.Y));
          if ((double) num5 < (double) num1)
          {
            num1 = num5;
            pointF9 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(pointF8, b4, p1, p2, out result1))
        {
          float num6 = (float) (((double) result1.X - (double) p1.X) * ((double) result1.X - (double) p1.X) + ((double) result1.Y - (double) p1.Y) * ((double) result1.Y - (double) p1.Y));
          if ((double) num6 < (double) num1)
          {
            num1 = num6;
            pointF9 = result1;
          }
        }
        result = pointF9;
        return (double) num1 < 1.0000000200408773E+21;
      }

      public virtual void CalculateArrowhead(
        PointF anchor,
        PointF endPoint,
        bool atEnd,
        PointF[] poly)
      {
        float x = endPoint.X;
        float y = endPoint.Y;
        double num1 = (double) x - (double) anchor.X;
        float num2 = y - anchor.Y;
        float num3 = (float) Math.Sqrt(num1 * num1 + (double) num2 * (double) num2);
        if ((double) num3 <= 1.0)
          num3 = 1f;
        float num4 = (float) num1 / num3;
        float num5 = num2 / num3;
        float val1;
        float arrowShaftLength;
        float num6;
        if (atEnd)
        {
          val1 = this.ToArrowLength;
          arrowShaftLength = this.ToArrowShaftLength;
          num6 = this.ToArrowWidth;
        }
        else
        {
          val1 = this.FromArrowLength;
          arrowShaftLength = this.FromArrowShaftLength;
          num6 = this.FromArrowWidth;
        }
        float num7 = num6 / 2f;
        float num8 = Math.Max(val1, arrowShaftLength);
        if ((double) num8 > 0.0 && (double) num3 < (double) num8 && this.Style != MapStrokeStyle.Bezier)
        {
          float num9 = num3 / num8;
          val1 *= num9;
          arrowShaftLength *= num9;
          num7 *= num9;
        }
        float num10 = -arrowShaftLength;
        float num11 = 0.0f;
        float num12 = -val1;
        float num13 = -num7;
        float num14 = -val1;
        float num15 = num7;
        poly[0].X = x + (float) ((double) num4 * (double) num10 - (double) num5 * (double) num11);
        poly[0].Y = y + (float) ((double) num5 * (double) num10 + (double) num4 * (double) num11);
        poly[1].X = x + (float) ((double) num4 * (double) num12 - (double) num5 * (double) num13);
        poly[1].Y = y + (float) ((double) num5 * (double) num12 + (double) num4 * (double) num13);
        poly[2].X = x;
        poly[2].Y = y;
        poly[3].X = x + (float) ((double) num4 * (double) num14 - (double) num5 * (double) num15);
        poly[3].Y = y + (float) ((double) num5 * (double) num14 + (double) num4 * (double) num15);
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 1201:
            if (!undo)
            {
              int oldInt = e.OldInt;
              RectangleF newRect = e.NewRect;
              double x = (double) newRect.X;
              newRect = e.NewRect;
              double y = (double) newRect.Y;
              PointF p = new PointF((float) x, (float) y);
              this.InsertPoint(oldInt, p);
              break;
            }
            this.RemovePoint(e.OldInt);
            break;
          case 1202:
            if (!undo)
            {
              this.RemovePoint(e.OldInt);
              break;
            }
            int oldInt1 = e.OldInt;
            RectangleF oldRect1 = e.OldRect;
            double x1 = (double) oldRect1.X;
            oldRect1 = e.OldRect;
            double y1 = (double) oldRect1.Y;
            PointF p1 = new PointF((float) x1, (float) y1);
            this.InsertPoint(oldInt1, p1);
            break;
          case 1203:
            if (!undo)
            {
              int oldInt2 = e.OldInt;
              RectangleF newRect = e.NewRect;
              double x2 = (double) newRect.X;
              newRect = e.NewRect;
              double y2 = (double) newRect.Y;
              PointF p2 = new PointF((float) x2, (float) y2);
              this.SetPoint(oldInt2, p2);
              break;
            }
            int oldInt3 = e.OldInt;
            RectangleF oldRect2 = e.OldRect;
            double x3 = (double) oldRect2.X;
            oldRect2 = e.OldRect;
            double y3 = (double) oldRect2.Y;
            PointF p3 = new PointF((float) x3, (float) y3);
            this.SetPoint(oldInt3, p3);
            break;
          case 1204:
            this.SetPoints((PointF[]) e.GetValue(undo));
            break;
          case 1205:
            this.Style = (MapStrokeStyle) e.GetValue(undo);
            break;
          case 1206:
            this.Curviness = e.GetFloat(undo);
            break;
          case 1236:
            object obj = e.GetValue(undo);
            switch (obj)
            {
              case Pen _:
                this.HighlightPen = (Pen) obj;
                return;
              case MapShape.MapPenInfo _:
                this.HighlightPen = ((MapShape.MapPenInfo) obj).GetPen();
                return;
              default:
                return;
            }
          case 1237:
            this.Highlight = (bool) e.GetValue(undo);
            break;
          case 1238:
            this.HighlightWhenSelected = (bool) e.GetValue(undo);
            break;
          case 1250:
            this.ToArrow = (bool) e.GetValue(undo);
            break;
          case 1251:
            this.ToArrowLength = e.GetFloat(undo);
            break;
          case 1252:
            this.ToArrowShaftLength = e.GetFloat(undo);
            break;
          case 1253:
            this.ToArrowWidth = e.GetFloat(undo);
            break;
          case 1254:
            this.ToArrowFilled = (bool) e.GetValue(undo);
            break;
          case 1255:
            this.ToArrowStyle = (MapStrokeArrowheadStyle) e.GetValue(undo);
            break;
          case 1260:
            this.FromArrow = (bool) e.GetValue(undo);
            break;
          case 1261:
            this.FromArrowLength = e.GetFloat(undo);
            break;
          case 1262:
            this.FromArrowShaftLength = e.GetFloat(undo);
            break;
          case 1263:
            this.FromArrowWidth = e.GetFloat(undo);
            break;
          case 1264:
            this.FromArrowFilled = (bool) e.GetValue(undo);
            break;
          case 1265:
            this.FromArrowStyle = (MapStrokeArrowheadStyle) e.GetValue(undo);
            break;
          case 1267:
            this.HighlightPenWidth = e.GetFloat(undo);
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      public virtual void ClearPoints()
      {
        this.Changing(1204);
        this.ResetPath();
        this._pointsCount = 0;
        this.InvalidBounds = true;
        this.Changed(1204, 0, (object) null, MapObject.NullRect, 0, (object) null, MapObject.NullRect);
      }

      protected override RectangleF ComputeBounds()
      {
        int pointsCount = this.PointsCount;
        if (pointsCount <= 0)
        {
          PointF position = this.Position;
          return new RectangleF(position.X, position.Y, 0.0f, 0.0f);
        }
        PointF point1 = this.GetPoint(0);
        float num1 = point1.X;
        float num2 = point1.Y;
        float val1_1 = point1.X;
        float val1_2 = point1.Y;
        if (this.Style == MapStrokeStyle.Bezier && pointsCount >= 4)
        {
          for (int i = 3; i < pointsCount; i += 3)
          {
            PointF point2 = this.GetPoint(i - 3);
            PointF point3 = this.GetPoint(i - 2);
            if (i + 3 >= pointsCount)
              i = pointsCount - 1;
            PointF point4 = this.GetPoint(i - 1);
            PointF point5 = this.GetPoint(i);
            PointF b1 = point3;
            PointF b2 = point4;
            PointF b3 = point5;
            RectangleF rectangleF = MapStroke.BezierBounds(point2, b1, b2, b3);
            num1 = Math.Min(num1, rectangleF.X);
            num2 = Math.Min(num2, rectangleF.Y);
            val1_1 = Math.Max(val1_1, rectangleF.X + rectangleF.Width);
            val1_2 = Math.Max(val1_2, rectangleF.Y + rectangleF.Height);
          }
        }
        else
        {
          for (int i = 1; i < pointsCount; ++i)
          {
            PointF point6 = this.GetPoint(i);
            num1 = Math.Min(num1, point6.X);
            num2 = Math.Min(num2, point6.Y);
            val1_1 = Math.Max(val1_1, point6.X);
            val1_2 = Math.Max(val1_2, point6.Y);
          }
        }
        return new RectangleF(num1, num2, val1_1 - num1, val1_2 - num2);
      }

      public override bool ContainsPoint(PointF p) => this.GetSegmentNearPoint(p) >= 0;

      public override void CopyNewValueForRedo(MapChangedEventArgs e)
      {
        if (e.SubHint == 1204)
        {
          PointF[] pointFArray = this.CopyPointsArray();
          e.NewValue = (object) pointFArray;
        }
        else
          base.CopyNewValueForRedo(e);
      }

      public override MapObject CopyObject(MapCopyDictionary env)
      {
        MapStroke mapStroke = (MapStroke) base.CopyObject(env);
        if (mapStroke != null)
        {
          mapStroke._points = (PointF[]) this._points.Clone();
          if (this._toArrowInfo != null)
            mapStroke._toArrowInfo = (MapStroke.ArrowInfo) this._toArrowInfo.Clone();
          if (this._fromArrowInfo != null)
            mapStroke._fromArrowInfo = (MapStroke.ArrowInfo) this._fromArrowInfo.Clone();
        }
        return (MapObject) mapStroke;
      }

      public override void CopyOldValueForUndo(MapChangedEventArgs e)
      {
        if (e.SubHint == 1204)
        {
          if (e.IsBeforeChanging)
            return;
          MapChangedEventArgs beforeChangingEdit = e.FindBeforeChangingEdit();
          if (beforeChangingEdit == null)
            return;
          e.OldValue = beforeChangingEdit.NewValue;
        }
        else
          base.CopyOldValueForUndo(e);
      }

      [Category("Appearance")]
      [Description("A copy of the array of points in this stroke.")]
      public virtual PointF[] CopyPointsArray()
      {
        PointF[] destinationArray = new PointF[this._pointsCount];
        Array.Copy((Array) this._points, 0, (Array) destinationArray, 0, this._pointsCount);
        return destinationArray;
      }

      public override void DoResize(
        MapView view,
        RectangleF origRect,
        PointF newPoint,
        int whichHandle,
        MapInputState evttype,
        SizeF min,
        SizeF max)
      {
        if (whichHandle >= 8192 /*0x2000*/ && (this.ResizesRealtime || evttype == MapInputState.Finish || evttype == MapInputState.Cancel))
          this.SetPoint(whichHandle - 8192 /*0x2000*/, newPoint);
        else
          base.DoResize(view, origRect, newPoint, whichHandle, evttype, min, max);
      }

      protected virtual void DrawArrowhead(
        Graphics g,
        MapView view,
        Pen pen,
        Brush brush,
        bool atEnd,
        float offsetw,
        float offseth,
        PointF[] poly)
      {
        Brush brush1 = (Brush) null;
        if (poly[0] != poly[2] && (atEnd ? (this.ToArrowFilled ? 1 : 0) : (this.FromArrowFilled ? 1 : 0)) != 0)
          brush1 = brush;
        switch (atEnd ? (int) this.ToArrowStyle : (int) this.FromArrowStyle)
        {
          case 0:
            if ((double) offsetw == 0.0 && (double) offseth == 0.0)
            {
              MapShape.DrawPolygon(g, view, pen, brush1, poly);
              break;
            }
            int length = poly.Length;
            for (int index = 0; index < length; ++index)
            {
              poly[index].X += offsetw;
              poly[index].Y += offseth;
            }
            MapShape.DrawPolygon(g, view, pen, brush1, poly);
            for (int index = 0; index < length; ++index)
            {
              poly[index].X -= offsetw;
              poly[index].Y -= offseth;
            }
            break;
          case 1:
            float x1 = poly[0].X;
            float y1 = poly[0].Y;
            float x2 = poly[2].X;
            float y2 = poly[2].Y;
            float num1 = (float) (((double) x1 + (double) x2) / 2.0) + offsetw;
            float num2 = (float) (((double) y1 + (double) y2) / 2.0) + offseth;
            float num3 = (float) Math.Sqrt(((double) x1 - (double) x2) * ((double) x1 - (double) x2) + ((double) y1 - (double) y2) * ((double) y1 - (double) y2));
            MapShape.DrawEllipse(g, view, pen, brush1, num1 - num3 / 2f, num2 - num3 / 2f, num3, num3);
            break;
          case 2:
            float x1_1 = poly[1].X + offsetw;
            float y1_1 = poly[1].Y + offseth;
            float x2_1 = poly[3].X + offsetw;
            float y2_1 = poly[3].Y + offseth;
            MapShape.DrawLine(g, view, pen, x1_1, y1_1, x2_1, y2_1);
            break;
        }
      }

      public override RectangleF ExpandPaintBounds(RectangleF rect, MapView view)
      {
        if (this.Pen != null)
        {
          float num = (float) ((double) Math.Max(this.PenInfo.Width, 1f) / 2.0 * (double) this.PenInfo.MiterLimit + 1.0);
          if (this.HighlightPen != null)
          {
            float val2 = (float) ((double) Math.Max(this.HighlightPenInfo.Width, 1f) / 2.0 * (double) this.HighlightPenInfo.MiterLimit + 1.0);
            num = Math.Max(num, val2);
          }
          if (this.ToArrow)
            num = Math.Max(Math.Max(num, this.ToArrowLength), this.ToArrowWidth);
          if (this.FromArrow)
            num = Math.Max(Math.Max(num, this.FromArrowLength), this.FromArrowWidth);
          MapObject.InflateRect(ref rect, num, num);
          if (!this.Shadowed)
            return rect;
          SizeF shadowOffset = this.GetShadowOffset(view);
          if ((double) shadowOffset.Width < 0.0)
          {
            rect.X += shadowOffset.Width;
            rect.Width -= shadowOffset.Width;
          }
          else
            rect.Width += shadowOffset.Width;
          if ((double) shadowOffset.Height < 0.0)
          {
            rect.Y += shadowOffset.Height;
            rect.Height -= shadowOffset.Height;
            return rect;
          }
          rect.Height += shadowOffset.Height;
        }
        return rect;
      }

      private int furthestPoint(PointF a, int i, bool oneway)
      {
        int pointsCount = this.PointsCount;
        PointF pointF1;
        for (pointF1 = a; a == pointF1; pointF1 = this.GetPoint(i++))
        {
          if (i >= pointsCount)
            return pointsCount - 1;
        }
        if ((double) a.X != (double) pointF1.X && (double) a.Y != (double) pointF1.Y)
          return i - 1;
        for (PointF pointF2 = pointF1; (double) a.X == (double) pointF1.X && (double) pointF1.X == (double) pointF2.X && (!oneway || ((double) a.Y >= (double) pointF1.Y ? ((double) pointF1.Y >= (double) pointF2.Y ? 1 : 0) : ((double) pointF1.Y <= (double) pointF2.Y ? 1 : 0)) != 0) || (double) a.Y == (double) pointF1.Y && (double) pointF1.Y == (double) pointF2.Y && (!oneway || ((double) a.X >= (double) pointF1.X ? ((double) pointF1.X >= (double) pointF2.X ? 1 : 0) : ((double) pointF1.X <= (double) pointF2.X ? 1 : 0)) != 0); pointF2 = this.GetPoint(i++))
        {
          if (i >= pointsCount)
            return pointsCount - 1;
        }
        return i - 2;
      }

      public static float GetAngle(float x, float y)
      {
        if ((double) Math.Abs(x) <= 1.0)
          return (double) y > 0.0 ? 90f : 270f;
        float angle = (float) (Math.Atan((double) Math.Abs(y / x)) * 180.0 / Math.PI);
        if ((double) x < 0.0)
          return (double) y < 0.0 ? angle + 180f : 180f - angle;
        if ((double) y < 0.0)
          angle = 360f - angle;
        return angle;
      }

      public virtual int GetArrowheadPointsCount(bool atEnd) => 4;

      private int getIntersections(PointF A, PointF B, float[] v)
      {
        MapDocument document = this.Document;
        if (document == null)
          return 0;
        float x = Math.Min(A.X, B.X);
        float y = Math.Min(A.Y, B.Y);
        float num1 = Math.Max(A.X, B.X);
        float num2 = Math.Max(A.Y, B.Y);
        RectangleF r = new RectangleF(x, y, num1 - x, num2 - y);
        int intersections = 0;
        foreach (MapLayer layer in document.Layers)
        {
          if (layer.CanViewObjects())
          {
            MapLayer.MapLayerCache cache = layer.FindCache(r);
            if (cache != null)
            {
              ArrayList arrayList = (ArrayList) null;
              foreach (MapStroke stroke in cache.Strokes)
              {
                if (stroke.Layer == null)
                {
                  if (arrayList == null)
                    arrayList = new ArrayList();
                  arrayList.Add((object) stroke);
                }
                else
                {
                  if (stroke == this)
                  {
                    if (arrayList != null)
                    {
                      foreach (MapObject o in arrayList)
                        MapCollection.fastRemove(cache.Strokes, (object) o);
                    }
                    Array.Sort((Array) v, 0, intersections, (IComparer) Comparer.Default);
                    return intersections;
                  }
                  intersections = this.getIntersections2(A, B, v, intersections, stroke);
                }
              }
              if (arrayList != null)
              {
                foreach (MapObject o in arrayList)
                  MapCollection.fastRemove(cache.Strokes, (object) o);
              }
            }
            else
            {
              foreach (MapObject mapObject in layer)
              {
                if (!(mapObject is MapStroke link))
                {
                  if (mapObject is MapLabeledLink mapLabeledLink)
                    link = (MapStroke) mapLabeledLink.RealLink;
                  else
                    continue;
                }
                if (link.Style == MapStrokeStyle.RoundedLineWithJumpOvers && link.CanView())
                {
                  if (link == this)
                  {
                    Array.Sort((Array) v, 0, intersections, (IComparer) Comparer.Default);
                    return intersections;
                  }
                  intersections = this.getIntersections2(A, B, v, intersections, link);
                }
              }
            }
          }
        }
        Array.Sort((Array) v, 0, intersections, (IComparer) Comparer.Default);
        return intersections;
      }

      private int getIntersections2(PointF A, PointF B, float[] v, int numints, MapStroke link)
      {
        if (link.CanView())
        {
          int pointsCount = link.PointsCount;
          for (int i = 1; i < pointsCount; ++i)
          {
            PointF point1 = link.GetPoint(i - 1);
            PointF point2 = link.GetPoint(i);
            PointF result = new PointF();
            if (this.getOrthoSegmentIntersection(A, B, point1, point2, ref result) && numints < v.Length)
              v[numints++] = (double) A.Y != (double) B.Y ? result.Y : result.X;
          }
        }
        return numints;
      }

      public override bool GetNearestIntersectionPoint(PointF p1, PointF p2, out PointF result)
      {
        int pointsCount = this.PointsCount;
        float num1 = 1E+21f;
        PointF pointF1 = new PointF();
        if (this.Style == MapStrokeStyle.Bezier && pointsCount >= 4)
        {
          for (int i = 3; i < pointsCount; i += 3)
          {
            PointF point1 = this.GetPoint(i - 3);
            PointF point2 = this.GetPoint(i - 2);
            if (i + 3 >= pointsCount)
              i = pointsCount - 1;
            PointF point3 = this.GetPoint(i - 1);
            PointF point4 = this.GetPoint(i);
            PointF b1 = point2;
            PointF b2 = point3;
            PointF b3 = point4;
            PointF p1_1 = p1;
            PointF p2_1 = p2;
            PointF pointF2;
            ref PointF local = ref pointF2;
            if (MapStroke.BezierNearestIntersectionOnLine(point1, b1, b2, b3, p1_1, p2_1, out local))
            {
              float num2 = (float) (((double) pointF2.X - (double) p1.X) * ((double) pointF2.X - (double) p1.X) + ((double) pointF2.Y - (double) p1.Y) * ((double) pointF2.Y - (double) p1.Y));
              if ((double) num2 < (double) num1)
              {
                num1 = num2;
                pointF1 = pointF2;
              }
            }
          }
        }
        else
        {
          for (int i = 0; i < pointsCount - 1; ++i)
          {
            PointF result1;
            if (MapStroke.NearestIntersectionOnLine(this.GetPoint(i), this.GetPoint(i + 1), p1, p2, out result1))
            {
              float num3 = (float) (((double) result1.X - (double) p1.X) * ((double) result1.X - (double) p1.X) + ((double) result1.Y - (double) p1.Y) * ((double) result1.Y - (double) p1.Y));
              if ((double) num3 < (double) num1)
              {
                num1 = num3;
                pointF1 = result1;
              }
            }
          }
        }
        result = pointF1;
        return (double) num1 < 1.0000000200408773E+21;
      }

      private bool getOrthoSegmentIntersection(
        PointF A,
        PointF B,
        PointF C,
        PointF D,
        ref PointF result)
      {
        if ((double) A.X != (double) B.X)
        {
          if ((double) C.X != (double) D.X || (double) Math.Min(A.X, B.X) >= (double) C.X || (double) Math.Max(A.X, B.X) <= (double) C.X || (double) Math.Min(C.Y, D.Y) >= (double) A.Y || (double) Math.Max(C.Y, D.Y) <= (double) A.Y)
          {
            result.X = 0.0f;
            result.Y = 0.0f;
            return false;
          }
          result.X = C.X;
          result.Y = A.Y;
          return true;
        }
        if ((double) C.Y == (double) D.Y && (double) Math.Min(A.Y, B.Y) < (double) C.Y && (double) Math.Max(A.Y, B.Y) > (double) C.Y && (double) Math.Min(C.X, D.X) < (double) A.X && (double) Math.Max(C.X, D.X) > (double) A.X)
        {
          result.X = A.X;
          result.Y = C.Y;
          return true;
        }
        result.X = 0.0f;
        result.Y = 0.0f;
        return false;
      }

      private GraphicsPath GetPath(float offx, float offy, PointF[] fromPoly, PointF[] toPoly)
      {
        if ((double) offx != 0.0 || (double) offy != 0.0 || this.Style == MapStrokeStyle.RoundedLineWithJumpOvers)
        {
          GraphicsPath path = new GraphicsPath();
          this.addStroke(path, offx, offy, fromPoly, toPoly);
          return path;
        }
        if (this.myPath == null)
        {
          this.myPath = new GraphicsPath();
          this.addStroke(this.myPath, 0.0f, 0.0f, fromPoly, toPoly);
        }
        return this.myPath;
      }

      public virtual PointF GetPoint(int i)
      {
        return i >= 0 && i < this._pointsCount ? this._points[i] : throw new ArgumentOutOfRangeException("MapStroke.GetPoint given an invalid index");
      }

      public int GetSegmentNearPoint(PointF pnt)
      {
        RectangleF bounds = this.Bounds;
        float num1 = Math.Max(this.InternalPenWidth, 1f) + this.PickMargin;
        if ((double) pnt.X >= (double) bounds.X - (double) num1 && (double) pnt.X <= (double) bounds.X + (double) bounds.Width + (double) num1 && (double) pnt.Y >= (double) bounds.Y - (double) num1 && (double) pnt.Y <= (double) bounds.Y + (double) bounds.Height + (double) num1)
        {
          int pointsCount = this.PointsCount;
          if (pointsCount <= 1)
            return -1;
          float fuzz1 = num1 - this.PickMargin / 2f;
          if (this.Style == MapStrokeStyle.Bezier && pointsCount >= 4)
          {
            float num2 = fuzz1 * Math.Max(1f, Math.Max(bounds.Width, bounds.Height) / 100f);
            for (int i = 3; i < pointsCount; i += 3)
            {
              int segmentNearPoint = i;
              PointF point1 = this.GetPoint(i - 3);
              PointF point2 = this.GetPoint(i - 2);
              if (i + 3 >= pointsCount)
                i = pointsCount - 1;
              PointF point3 = this.GetPoint(i - 1);
              PointF point4 = this.GetPoint(i);
              PointF b1 = point2;
              PointF b2 = point3;
              PointF b3 = point4;
              double fuzz2 = (double) num2;
              PointF p = pnt;
              if (MapStroke.BezierContainsPoint(point1, b1, b2, b3, (float) fuzz2, p))
                return segmentNearPoint;
            }
          }
          else
          {
            for (int i = 0; i < pointsCount - 1; ++i)
            {
              if (MapStroke.LineContainsPoint(this.GetPoint(i), this.GetPoint(i + 1), fuzz1, pnt))
                return i;
            }
          }
        }
        return -1;
      }

      public virtual void InsertPoint(int i, PointF p)
      {
        if (i < 0)
          throw new ArgumentOutOfRangeException("MapStroke.InsertPoint given an invalid index, less than zero");
        if (i > this._pointsCount)
          i = this._pointsCount;
        this.ResetPath();
        int length = this._points.Length;
        if (this._pointsCount >= length)
        {
          PointF[] destinationArray = new PointF[Math.Max(length * 2, this._pointsCount + 1)];
          Array.Copy((Array) this._points, 0, (Array) destinationArray, 0, length);
          this._points = destinationArray;
        }
        if (this._pointsCount > i)
          Array.Copy((Array) this._points, i, (Array) this._points, i + 1, this._pointsCount - i);
        ++this._pointsCount;
        this._points[i] = p;
        this.InvalidBounds = true;
        this.Changed(1201, i, (object) null, MapObject.MakeRect(p), i, (object) null, MapObject.MakeRect(p));
      }

      internal static bool LineContainsPoint(PointF a, PointF b, float fuzz, PointF p)
      {
        float x1;
        float x2;
        if ((double) a.X < (double) b.X)
        {
          x1 = b.X;
          x2 = a.X;
        }
        else
        {
          x1 = a.X;
          x2 = b.X;
        }
        float y1;
        float y2;
        if ((double) a.Y < (double) b.Y)
        {
          y1 = a.Y;
          y2 = b.Y;
        }
        else
        {
          y1 = b.Y;
          y2 = a.Y;
        }
        if ((double) a.X == (double) b.X && (double) a.X - (double) fuzz <= (double) p.X && (double) p.X <= (double) a.X + (double) fuzz && (double) y1 <= (double) p.Y && (double) p.Y <= (double) y2 || (double) a.Y == (double) b.Y && (double) a.Y - (double) fuzz <= (double) p.Y && (double) p.Y <= (double) a.Y + (double) fuzz && (double) x2 <= (double) p.X && (double) p.X <= (double) x1)
          return true;
        float num1 = x1 + fuzz;
        float num2 = x2 - fuzz;
        if ((double) num2 <= (double) p.X && (double) p.X <= (double) num1)
        {
          float num3 = y2 + fuzz;
          float num4 = y1 - fuzz;
          if ((double) num4 <= (double) p.Y && (double) p.Y <= (double) num3)
          {
            if ((double) num1 - (double) num2 > (double) num3 - (double) num4)
            {
              if ((double) Math.Abs(a.X - b.X) <= (double) fuzz)
                return true;
              float num5 = (float) (((double) b.Y - (double) a.Y) / ((double) b.X - (double) a.X) * ((double) p.X - (double) a.X)) + a.Y;
              return (double) num5 - (double) fuzz <= (double) p.Y && (double) p.Y <= (double) num5 + (double) fuzz;
            }
            if ((double) Math.Abs(a.Y - b.Y) <= (double) fuzz)
              return true;
            float num6 = (float) (((double) b.X - (double) a.X) / ((double) b.Y - (double) a.Y) * ((double) p.Y - (double) a.Y)) + a.X;
            if ((double) num6 - (double) fuzz <= (double) p.X && (double) p.X <= (double) num6 + (double) fuzz)
              return true;
          }
        }
        return false;
      }

      public override GraphicsPath MakePath()
      {
        return (GraphicsPath) this.GetPath(0.0f, 0.0f, (PointF[]) null, (PointF[]) null).Clone();
      }

      public static bool NearestIntersectionOnLine(
        PointF a,
        PointF b,
        PointF p,
        PointF q,
        out PointF result)
      {
        float x1 = a.X;
        float y1 = a.Y;
        float x2 = b.X;
        float y2 = b.Y;
        float x3 = p.X;
        float y3 = p.Y;
        float x4 = q.X;
        float y4 = q.Y;
        if ((double) x3 == (double) x4)
        {
          if ((double) x1 == (double) x2)
          {
            MapStroke.NearestPointOnLine(a, b, p, out result);
            return false;
          }
          float y5 = (float) (((double) y2 - (double) y1) / ((double) x2 - (double) x1) * ((double) x3 - (double) x1)) + y1;
          return MapStroke.NearestPointOnLine(a, b, new PointF(x3, y5), out result);
        }
        float num1 = (float) (((double) y4 - (double) y3) / ((double) x4 - (double) x3));
        if ((double) x1 == (double) x2)
        {
          float y6 = num1 * (x1 - x3) + y3;
          if ((double) y6 < (double) Math.Min(y1, y2))
          {
            result = new PointF(x1, Math.Min(y1, y2));
            return false;
          }
          if ((double) y6 > (double) Math.Max(y1, y2))
          {
            result = new PointF(x1, Math.Max(y1, y2));
            return false;
          }
          result = new PointF(x1, y6);
          return true;
        }
        float num2 = (float) (((double) y2 - (double) y1) / ((double) x2 - (double) x1));
        if ((double) num1 == (double) num2)
        {
          MapStroke.NearestPointOnLine(a, b, p, out result);
          return false;
        }
        float x5 = (float) (((double) num2 * (double) x1 - (double) num1 * (double) x3 + (double) y3 - (double) y1) / ((double) num2 - (double) num1));
        if ((double) num2 == 0.0)
        {
          if ((double) x5 < (double) Math.Min(x1, x2))
          {
            result = new PointF(Math.Min(x1, x2), y1);
            return false;
          }
          if ((double) x5 > (double) Math.Max(x1, x2))
          {
            result = new PointF(Math.Max(x1, x2), y1);
            return false;
          }
          result = new PointF(x5, y1);
          return true;
        }
        float y7 = num2 * (x5 - x1) + y1;
        return MapStroke.NearestPointOnLine(a, b, new PointF(x5, y7), out result);
      }

      public static bool NearestPointOnLine(PointF a, PointF b, PointF p, out PointF result)
      {
        float x1 = a.X;
        float y1 = a.Y;
        float x2 = b.X;
        float y2 = b.Y;
        float x3 = p.X;
        float y3 = p.Y;
        if ((double) x1 == (double) x2)
        {
          float y4;
          float y5;
          if ((double) y1 < (double) y2)
          {
            y4 = y1;
            y5 = y2;
          }
          else
          {
            y4 = y2;
            y5 = y1;
          }
          float y6 = y3;
          if ((double) y6 < (double) y4)
          {
            result = new PointF(x1, y4);
            return false;
          }
          if ((double) y6 > (double) y5)
          {
            result = new PointF(x1, y5);
            return false;
          }
          result = new PointF(x1, y6);
          return true;
        }
        if ((double) y1 == (double) y2)
        {
          float x4;
          float x5;
          if ((double) x1 < (double) x2)
          {
            x4 = x1;
            x5 = x2;
          }
          else
          {
            x4 = x2;
            x5 = x1;
          }
          float x6 = x3;
          if ((double) x6 < (double) x4)
          {
            result = new PointF(x4, y1);
            return false;
          }
          if ((double) x6 > (double) x5)
          {
            result = new PointF(x5, y1);
            return false;
          }
          result = new PointF(x6, y1);
          return true;
        }
        float num1 = (float) (((double) x2 - (double) x1) * ((double) x2 - (double) x1) + ((double) y2 - (double) y1) * ((double) y2 - (double) y1));
        float num2 = (float) (((double) x1 - (double) x3) * ((double) x1 - (double) x2) + ((double) y1 - (double) y3) * ((double) y1 - (double) y2)) / num1;
        if ((double) num2 < 0.0)
        {
          result = a;
          return false;
        }
        if ((double) num2 > 1.0)
        {
          result = b;
          return false;
        }
        float x7 = x1 + num2 * (x2 - x1);
        float y7 = y1 + num2 * (y2 - y1);
        result = new PointF(x7, y7);
        return true;
      }

      protected override void OnBoundsChanged(RectangleF old)
      {
        base.OnBoundsChanged(old);
        int pointsCount = this.PointsCount;
        SizeF size = this.Size;
        if ((double) old.Width == (double) size.Width && (double) old.Height == (double) size.Height)
        {
          RectangleF bounds = this.Bounds;
          float num1 = bounds.X - old.X;
          float num2 = bounds.Y - old.Y;
          if ((double) num1 == 0.0 && (double) num2 == 0.0)
            return;
          bool suspendsUpdates = this.SuspendsUpdates;
          if (!suspendsUpdates)
            this.Changing(1204);
          this.SuspendsUpdates = true;
          for (int i = 0; i < pointsCount; ++i)
          {
            PointF point = this.GetPoint(i);
            float x = point.X + num1;
            float y = point.Y + num2;
            this.SetPoint(i, new PointF(x, y));
          }
          this.InvalidBounds = false;
          this.SuspendsUpdates = suspendsUpdates;
          if (suspendsUpdates)
            return;
          this.Changed(1204, 0, (object) null, old, 0, (object) null, bounds);
        }
        else
        {
          RectangleF bounds = this.Bounds;
          float num3 = 1f;
          if ((double) old.Width != 0.0)
            num3 = bounds.Width / old.Width;
          float num4 = 1f;
          if ((double) old.Height != 0.0)
            num4 = bounds.Height / old.Height;
          bool suspendsUpdates = this.SuspendsUpdates;
          if (!suspendsUpdates)
            this.Changing(1204);
          this.SuspendsUpdates = true;
          for (int i = 0; i < pointsCount; ++i)
          {
            PointF point = this.GetPoint(i);
            float x = bounds.X + (point.X - old.X) * num3;
            float y = bounds.Y + (point.Y - old.Y) * num4;
            this.SetPoint(i, new PointF(x, y));
          }
          this.InvalidBounds = false;
          this.SuspendsUpdates = suspendsUpdates;
          if (suspendsUpdates)
            return;
          this.Changed(1204, 0, (object) null, old, 0, (object) null, bounds);
        }
      }

      public override void Paint(Graphics g, MapView view)
      {
        Pen pen1 = this.Pen;
        if (pen1 == null)
          return;
        Pen pen2 = pen1;
        Brush brush = this.Brush;
        int pointsCount = this.PointsCount;
        PointF[] pointFArray1 = (PointF[]) null;
        PointF[] pointFArray2 = (PointF[]) null;
        if (this.FromArrow && pointsCount >= 2)
        {
          if (this._fromArrowInfo == null)
            this._fromArrowInfo = new MapStroke.ArrowInfo();
          pointFArray1 = this._fromArrowInfo.GetPoly(this.GetArrowheadPointsCount(false));
          this.CalculateArrowhead(this.FromArrowAnchorPoint, this.FromArrowEndPoint, false, pointFArray1);
        }
        if (this.ToArrow && pointsCount >= 2)
        {
          if (this._toArrowInfo == null)
            this._toArrowInfo = new MapStroke.ArrowInfo();
          pointFArray2 = this._toArrowInfo.GetPoly(this.GetArrowheadPointsCount(true));
          this.CalculateArrowhead(this.ToArrowAnchorPoint, this.ToArrowEndPoint, true, pointFArray2);
        }
        if (this.Shadowed && this.Pen != null)
        {
          SizeF shadowOffset = this.GetShadowOffset(view);
          Pen shadowPen = this.GetShadowPen(view, this.InternalPenWidth);
          if (shadowPen != null)
          {
            GraphicsPath path = this.GetPath(shadowOffset.Width, shadowOffset.Height, pointFArray1, pointFArray2);
            MapShape.DrawPath(g, view, shadowPen, (Brush) null, path);
            this.DisposePath(path);
          }
          Brush shadowBrush = this.GetShadowBrush(view);
          if (pointFArray1 != null)
            this.DrawArrowhead(g, view, shadowPen, shadowBrush, false, shadowOffset.Width, shadowOffset.Height, pointFArray1);
          if (pointFArray2 != null)
            this.DrawArrowhead(g, view, shadowPen, shadowBrush, true, shadowOffset.Width, shadowOffset.Height, pointFArray2);
        }
        GraphicsPath path1 = this.GetPath(0.0f, 0.0f, pointFArray1, pointFArray2);
        Pen highlightPen = this.HighlightPen;
        if (highlightPen != null && this.Highlight)
          MapShape.DrawPath(g, view, highlightPen, (Brush) null, path1);
        MapShape.DrawPath(g, view, pen1, (Brush) null, path1);
        this.DisposePath(path1);
        if (pointFArray1 != null || pointFArray2 != null)
        {
          if (pen2.DashStyle != DashStyle.Solid || (double) pen2.Width > 1.0)
          {
            Pen pen3 = new Pen(pen2.Color);
            if (pointFArray1 != null)
              this.DrawArrowhead(g, view, pen3, brush, false, 0.0f, 0.0f, pointFArray1);
            if (pointFArray2 != null)
              this.DrawArrowhead(g, view, pen3, brush, true, 0.0f, 0.0f, pointFArray2);
            pen3.Dispose();
          }
          else
          {
            if (pointFArray1 != null)
              this.DrawArrowhead(g, view, pen2, brush, false, 0.0f, 0.0f, pointFArray1);
            if (pointFArray2 != null)
              this.DrawArrowhead(g, view, pen2, brush, true, 0.0f, 0.0f, pointFArray2);
          }
        }
        if (this.Layer == null || view == null || this.Style != MapStrokeStyle.RoundedLineWithJumpOvers)
          return;
        MapLayer.MapLayerCache cache = this.Layer.FindCache(view);
        if (cache == null || cache.Strokes.Contains((object) this))
          return;
        cache.Strokes.Add((object) this);
      }

      public virtual void RemovePoint(int i)
      {
        if (i < 0 || i >= this._pointsCount)
          return;
        this.ResetPath();
        PointF point = this._points[i];
        if (this._pointsCount > i + 1)
          Array.Copy((Array) this._points, i + 1, (Array) this._points, i, this._pointsCount - i - 1);
        --this._pointsCount;
        this.InvalidBounds = true;
        this.Changed(1202, i, (object) null, MapObject.MakeRect(point), i, (object) null, MapObject.MakeRect(point));
      }

      public override void RemoveSelectionHandles(MapSelection sel)
      {
        if (this.HighlightWhenSelected)
        {
          bool skipsUndoManager = this.SkipsUndoManager;
          this.SkipsUndoManager = true;
          this.Highlight = false;
          this.SkipsUndoManager = skipsUndoManager;
        }
        base.RemoveSelectionHandles(sel);
      }

      public virtual void SetPoint(int i, PointF p)
      {
        PointF p1 = i >= 0 && i < this._pointsCount ? this._points[i] : throw new ArgumentOutOfRangeException("MapStroke.SetPoint given an invalid index");
        if (!(p1 != p))
          return;
        this.ResetPath();
        this._points[i] = p;
        this.InvalidBounds = true;
        this.Changed(1203, i, (object) null, MapObject.MakeRect(p1), i, (object) null, MapObject.MakeRect(p));
      }

      public virtual void SetPoints(PointF[] points)
      {
        this.Changing(1204);
        this.ResetPath();
        int length = points.Length;
        if (length > this._points.Length)
          this._points = new PointF[length];
        Array.Copy((Array) points, 0, (Array) this._points, 0, length);
        this._pointsCount = length;
        this.InvalidBounds = true;
        this.Changed(1204, 0, (object) null, MapObject.NullRect, 0, (object) null, MapObject.NullRect);
      }

      [DefaultValue(10f)]
      [Category("Appearance")]
      [Description("How rounded corners are for strokes of style RoundedLine and how curved Bezier links are.")]
      public virtual float Curviness
      {
        get => this._curviness;
        set
        {
          float curviness = this._curviness;
          if ((double) curviness == (double) value)
            return;
          this._curviness = value;
          this.ResetPath();
          this.Changed(1206, 0, (object) null, MapObject.MakeRect(curviness), 0, (object) null, MapObject.MakeRect(value));
        }
      }

      [Category("Behavior")]
      [Description("The index of the first point getting a selection handle.")]
      public virtual int FirstPickIndex => 0;

      [Category("Appearance")]
      [DefaultValue(false)]
      [Description("Whether an arrow is drawn at the start of this stroke.")]
      public virtual bool FromArrow
      {
        get => (this.InternalFlags & 1048576 /*0x100000*/) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 1048576 /*0x100000*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 1048576 /*0x100000*/;
          else
            this.InternalFlags &= -1048577;
          this.InvalidBounds = true;
          this.ResetPath();
          this.Changed(1260, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Category("Appearance")]
      [Description("A point specifying the direction from which comes the arrow at the start of this stroke.")]
      [TypeConverter(typeof (MapPointFConverter))]
      public virtual PointF FromArrowAnchorPoint => this.GetPoint(1);

      [TypeConverter(typeof (MapPointFConverter))]
      [Description("The point at the tip of the arrow at the start of this stroke.")]
      [Category("Appearance")]
      public virtual PointF FromArrowEndPoint => this.GetPoint(0);

      [Description("Whether the arrowhead is filled with the stroke's brush")]
      [DefaultValue(true)]
      [Category("Appearance")]
      public virtual bool FromArrowFilled
      {
        get => this._fromArrowInfo == null || this._fromArrowInfo.Filled;
        set
        {
          bool oldVal = this._fromArrowInfo == null || this._fromArrowInfo.Filled;
          if (oldVal == value)
            return;
          if (this._fromArrowInfo == null)
            this._fromArrowInfo = new MapStroke.ArrowInfo();
          this._fromArrowInfo.Filled = value;
          this.Changed(1264, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Category("Appearance")]
      [DefaultValue(10f)]
      [Description("The length of the arrowhead at the start of this stroke, along the shaft from the end point to the widest point.")]
      public virtual float FromArrowLength
      {
        get => this._fromArrowInfo != null ? this._fromArrowInfo.ArrowLength : 10f;
        set
        {
          float x = this._fromArrowInfo == null ? 10f : this._fromArrowInfo.ArrowLength;
          if ((double) x == (double) value)
            return;
          if (this._fromArrowInfo == null)
            this._fromArrowInfo = new MapStroke.ArrowInfo();
          this._fromArrowInfo.ArrowLength = value;
          this.InvalidBounds = true;
          this.ResetPath();
          this.Changed(1261, 0, (object) null, MapObject.MakeRect(x), 0, (object) null, MapObject.MakeRect(value));
        }
      }

      [Description("The length of the arrow along the shaft at the start of this stroke.")]
      [DefaultValue(8f)]
      [Category("Appearance")]
      public virtual float FromArrowShaftLength
      {
        get => this._fromArrowInfo != null ? this._fromArrowInfo.ShaftLength : 8f;
        set
        {
          float x = this._fromArrowInfo == null ? 8f : this._fromArrowInfo.ShaftLength;
          if ((double) x == (double) value)
            return;
          if (this._fromArrowInfo == null)
            this._fromArrowInfo = new MapStroke.ArrowInfo();
          this._fromArrowInfo.ShaftLength = value;
          this.InvalidBounds = true;
          this.ResetPath();
          this.Changed(1262, 0, (object) null, MapObject.MakeRect(x), 0, (object) null, MapObject.MakeRect(value));
        }
      }

      [Category("Appearance")]
      [Description("Specifies the general shape of the arrowhead")]
      [DefaultValue(0)]
      public virtual MapStrokeArrowheadStyle FromArrowStyle
      {
        get
        {
          return this._fromArrowInfo != null ? this._fromArrowInfo.Style : MapStrokeArrowheadStyle.Polygon;
        }
        set
        {
          MapStrokeArrowheadStyle oldVal = this._fromArrowInfo == null ? MapStrokeArrowheadStyle.Polygon : this._fromArrowInfo.Style;
          if (oldVal == value)
            return;
          if (this._fromArrowInfo == null)
            this._fromArrowInfo = new MapStroke.ArrowInfo();
          this._fromArrowInfo.Style = value;
          this.ResetPath();
          this.Changed(1265, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("The width at its widest point of the arrowhead at the start of this stroke.")]
      [DefaultValue(8f)]
      [Category("Appearance")]
      public virtual float FromArrowWidth
      {
        get => this._fromArrowInfo != null ? this._fromArrowInfo.Width : 8f;
        set
        {
          float x = this._fromArrowInfo == null ? 8f : this._fromArrowInfo.Width;
          if ((double) x == (double) value || (double) value < 0.0)
            return;
          if (this._fromArrowInfo == null)
            this._fromArrowInfo = new MapStroke.ArrowInfo();
          this._fromArrowInfo.Width = value;
          this.InvalidBounds = true;
          this.Changed(1263, 0, (object) null, MapObject.MakeRect(x), 0, (object) null, MapObject.MakeRect(value));
        }
      }

      [Description("Whether a highlight is shown along the path of this stroke.")]
      [DefaultValue(false)]
      [Category("Appearance")]
      public virtual bool Highlight
      {
        get => (this.InternalFlags & 4194304 /*0x400000*/) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 4194304 /*0x400000*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 4194304 /*0x400000*/;
          else
            this.InternalFlags &= -4194305;
          this.Changed(1237, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("The pen used to draw the highlight.")]
      [DefaultValue(null)]
      [Category("Appearance")]
      public virtual Pen HighlightPen
      {
        get => this._highlightPenInfo != null ? this._highlightPenInfo.GetPen() : (Pen) null;
        set
        {
          MapShape.MapPenInfo highlightPenInfo = this._highlightPenInfo;
          MapShape.MapPenInfo penInfo = MapShape.GetPenInfo(value);
          if (highlightPenInfo == penInfo)
            return;
          this._highlightPenInfo = penInfo;
          this.Changed(1236, 0, (object) highlightPenInfo, MapObject.NullRect, 0, (object) penInfo, MapObject.NullRect);
          if (this.Parent == null)
            return;
          this.Parent.InvalidatePaintBounds();
        }
      }

      internal MapShape.MapPenInfo HighlightPenInfo => this._highlightPenInfo;

      [Description("[Only supported in MapDiagram Pocket]")]
      public virtual float HighlightPenWidth
      {
        get => this.HighlightPenInfo != null ? this.HighlightPenInfo.Width : 0.0f;
        set
        {
          float num = 0.0f;
          if (this.HighlightPenInfo != null)
            num = this.HighlightPenInfo.Width;
          if ((double) num == (double) value)
            return;
          Pen highlightPen = this.HighlightPen;
          if (highlightPen == null)
            return;
          Pen pen = (Pen) highlightPen.Clone();
          pen.Width = value;
          this.HighlightPen = pen;
        }
      }

      [DefaultValue(false)]
      [Description("Whether the highlight is shown when this stroke becomes selected.")]
      [Category("Behavior")]
      public virtual bool HighlightWhenSelected
      {
        get => (this.InternalFlags & 8388608 /*0x800000*/) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 8388608 /*0x800000*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 8388608 /*0x800000*/;
          else
            this.InternalFlags &= -8388609;
          this.Changed(1238, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Category("Behavior")]
      [Description("The index of the last point getting a selection handle.")]
      public virtual int LastPickIndex => this.PointsCount - 1;

      [Description("[Only supported in MapDiagram Pocket]")]
      public virtual float PenWidth
      {
        get => this.InternalPenWidth;
        set
        {
          if ((double) this.InternalPenWidth == (double) value)
            return;
          Pen pen1 = this.Pen;
          if (pen1 == null)
            return;
          Pen pen2 = (Pen) pen1.Clone();
          pen2.Width = value;
          this.Pen = pen2;
        }
      }

      [Category("Behavior")]
      [Description("About how close users need to be to the stroke to pick it")]
      public virtual float PickMargin => 3f;

      [Description("The number of points in this stroke.")]
      [Category("Appearance")]
      public virtual int PointsCount => this._pointsCount;

      [Description("The kind of curve drawn using this stroke's points.")]
      [Category("Appearance")]
      [DefaultValue(0)]
      public virtual MapStrokeStyle Style
      {
        get => this._style;
        set
        {
          MapStrokeStyle style = this._style;
          if (style == value)
            return;
          this._style = value;
          this.ResetPath();
          this.Changed(1205, 0, (object) style, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("Whether an arrow is drawn at the end of this stroke.")]
      [Category("Appearance")]
      [DefaultValue(false)]
      public virtual bool ToArrow
      {
        get => (this.InternalFlags & 2097152 /*0x200000*/) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 2097152 /*0x200000*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 2097152 /*0x200000*/;
          else
            this.InternalFlags &= -2097153;
          this.InvalidBounds = true;
          this.ResetPath();
          this.Changed(1250, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Category("Appearance")]
      [TypeConverter(typeof (MapPointFConverter))]
      [Description("A point which specifies the direction the arrow is coming from.")]
      public virtual PointF ToArrowAnchorPoint => this.GetPoint(this.PointsCount - 2);

      [TypeConverter(typeof (MapPointFConverter))]
      [Description("The point at the tip of the arrowhead at the end of this stroke.")]
      [Category("Appearance")]
      public virtual PointF ToArrowEndPoint => this.GetPoint(this.PointsCount - 1);

      [DefaultValue(true)]
      [Description("Whether the arrowhead is filled with the stroke's brush")]
      [Category("Appearance")]
      public virtual bool ToArrowFilled
      {
        get => this._toArrowInfo == null || this._toArrowInfo.Filled;
        set
        {
          bool oldVal = this._toArrowInfo == null || this._toArrowInfo.Filled;
          if (oldVal == value)
            return;
          if (this._toArrowInfo == null)
            this._toArrowInfo = new MapStroke.ArrowInfo();
          this._toArrowInfo.Filled = value;
          this.Changed(1254, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [DefaultValue(10f)]
      [Description("The length of the arrow at the end of this stroke, along the shaft from the end point to the widest point.")]
      [Category("Appearance")]
      public virtual float ToArrowLength
      {
        get => this._toArrowInfo != null ? this._toArrowInfo.ArrowLength : 10f;
        set
        {
          float x = this._toArrowInfo == null ? 10f : this._toArrowInfo.ArrowLength;
          if ((double) x == (double) value)
            return;
          if (this._toArrowInfo == null)
            this._toArrowInfo = new MapStroke.ArrowInfo();
          this._toArrowInfo.ArrowLength = value;
          this.InvalidBounds = true;
          this.ResetPath();
          this.Changed(1251, 0, (object) null, MapObject.MakeRect(x), 0, (object) null, MapObject.MakeRect(value));
        }
      }

      [DefaultValue(8f)]
      [Description("The length of the arrow along the shaft at the end of this stroke.")]
      [Category("Appearance")]
      public virtual float ToArrowShaftLength
      {
        get => this._toArrowInfo != null ? this._toArrowInfo.ShaftLength : 8f;
        set
        {
          float x = this._toArrowInfo == null ? 8f : this._toArrowInfo.ShaftLength;
          if ((double) x == (double) value)
            return;
          if (this._toArrowInfo == null)
            this._toArrowInfo = new MapStroke.ArrowInfo();
          this._toArrowInfo.ShaftLength = value;
          this.InvalidBounds = true;
          this.ResetPath();
          this.Changed(1252, 0, (object) null, MapObject.MakeRect(x), 0, (object) null, MapObject.MakeRect(value));
        }
      }

      [Description("Specifies the general shape of the arrowhead")]
      [Category("Appearance")]
      [DefaultValue(0)]
      public virtual MapStrokeArrowheadStyle ToArrowStyle
      {
        get => this._toArrowInfo != null ? this._toArrowInfo.Style : MapStrokeArrowheadStyle.Polygon;
        set
        {
          MapStrokeArrowheadStyle oldVal = this._toArrowInfo == null ? MapStrokeArrowheadStyle.Polygon : this._toArrowInfo.Style;
          if (oldVal == value)
            return;
          if (this._toArrowInfo == null)
            this._toArrowInfo = new MapStroke.ArrowInfo();
          this._toArrowInfo.Style = value;
          this.ResetPath();
          this.Changed(1255, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("The width of the arrowhead at the widest point.")]
      [Category("Appearance")]
      [DefaultValue(8f)]
      public virtual float ToArrowWidth
      {
        get => this._toArrowInfo != null ? this._toArrowInfo.Width : 8f;
        set
        {
          float x = this._toArrowInfo == null ? 8f : this._toArrowInfo.Width;
          if ((double) x == (double) value || (double) value < 0.0)
            return;
          if (this._toArrowInfo == null)
            this._toArrowInfo = new MapStroke.ArrowInfo();
          this._toArrowInfo.Width = value;
          this.InvalidBounds = true;
          this.Changed(1253, 0, (object) null, MapObject.MakeRect(x), 0, (object) null, MapObject.MakeRect(value));
        }
      }

      [Serializable]
      internal sealed class ArrowInfo : ICloneable
      {
        private const int flagFilled = 65536 /*0x010000*/;
        private const int flagStyleMask = 65535 /*0xFFFF*/;
        internal float ArrowLength;
        private int myFlags;
        internal PointF[] myPolyPoints;
        internal float ShaftLength;
        internal float Width;

        internal ArrowInfo()
        {
          this.ArrowLength = 10f;
          this.ShaftLength = 8f;
          this.Width = 8f;
          this.myFlags = 65536 /*0x010000*/;
          this.myPolyPoints = (PointF[]) null;
        }

        public object Clone()
        {
          MapStroke.ArrowInfo arrowInfo = (MapStroke.ArrowInfo) this.MemberwiseClone();
          if (this.myPolyPoints != null)
            arrowInfo.myPolyPoints = (PointF[]) this.myPolyPoints.Clone();
          return (object) arrowInfo;
        }

        internal PointF[] GetPoly(int n)
        {
          if (this.myPolyPoints == null || this.myPolyPoints.Length < n)
            this.myPolyPoints = new PointF[n];
          return this.myPolyPoints;
        }

        internal bool Filled
        {
          get => (this.myFlags & 65536 /*0x010000*/) != 0;
          set
          {
            if (value)
              this.myFlags |= 65536 /*0x010000*/;
            else
              this.myFlags &= -65537;
          }
        }

        internal MapStrokeArrowheadStyle Style
        {
          get => (MapStrokeArrowheadStyle) (this.myFlags & (int) ushort.MaxValue);
          set
          {
            this.myFlags = (int) ((MapStrokeArrowheadStyle) (this.myFlags & -65536) | value & (MapStrokeArrowheadStyle) 65535 /*0xFFFF*/);
          }
        }
      }
    }
}
