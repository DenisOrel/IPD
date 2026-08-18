// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapDiamond
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace Intermech.Map
{
    [Serializable]
    public class MapDiamond : MapShape
    {
      private PointF[] myPoints;

      public MapDiamond() => this.myPoints = new PointF[4];

      public override bool ContainsPoint(PointF p)
      {
        return base.ContainsPoint(p) && this.GetPath().IsVisible(p);
      }

      public override MapObject CopyObject(MapCopyDictionary env)
      {
        MapDiamond mapDiamond = (MapDiamond) base.CopyObject(env);
        if (mapDiamond != null)
          mapDiamond.myPoints = (PointF[]) this.myPoints.Clone();
        return (MapObject) mapDiamond;
      }

      public override bool GetNearestIntersectionPoint(PointF p1, PointF p2, out PointF result)
      {
        RectangleF bounds = this.Bounds;
        float shift = this.InternalPenWidth / 2f;
        PointF[] points = this.getPoints();
        PointF pointF1 = MapShape.ExpandPointOnEdge(points[0], bounds, shift);
        PointF pointF2 = MapShape.ExpandPointOnEdge(points[1], bounds, shift);
        PointF pointF3 = MapShape.ExpandPointOnEdge(points[2], bounds, shift);
        PointF pointF4 = MapShape.ExpandPointOnEdge(points[3], bounds, shift);
        float x = p1.X;
        float y = p1.Y;
        float num1 = 1E+21f;
        PointF pointF5 = new PointF();
        PointF result1;
        if (MapStroke.NearestIntersectionOnLine(pointF1, pointF2, p1, p2, out result1))
        {
          float num2 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num2 < (double) num1)
          {
            num1 = num2;
            pointF5 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(pointF2, pointF3, p1, p2, out result1))
        {
          float num3 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num3 < (double) num1)
          {
            num1 = num3;
            pointF5 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(pointF3, pointF4, p1, p2, out result1))
        {
          float num4 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num4 < (double) num1)
          {
            num1 = num4;
            pointF5 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(pointF4, pointF1, p1, p2, out result1))
        {
          float num5 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num5 < (double) num1)
          {
            num1 = num5;
            pointF5 = result1;
          }
        }
        result = pointF5;
        return (double) num1 < 1.0000000200408773E+21;
      }

      private PointF[] getPoints()
      {
        RectangleF bounds = this.Bounds;
        this.myPoints[0].X = bounds.X + bounds.Width / 2f;
        this.myPoints[0].Y = bounds.Y;
        this.myPoints[1].X = bounds.X + bounds.Width;
        this.myPoints[1].Y = bounds.Y + bounds.Height / 2f;
        this.myPoints[2].X = this.myPoints[0].X;
        this.myPoints[2].Y = bounds.Y + bounds.Height;
        this.myPoints[3].X = bounds.X;
        this.myPoints[3].Y = this.myPoints[1].Y;
        return this.myPoints;
      }

      public override GraphicsPath MakePath()
      {
        GraphicsPath graphicsPath = new GraphicsPath(FillMode.Winding);
        graphicsPath.AddLines(this.getPoints());
        graphicsPath.CloseAllFigures();
        return graphicsPath;
      }

      public override void Paint(Graphics g, MapView view)
      {
        if (this.Shadowed)
        {
          PointF[] points = this.getPoints();
          int length = points.Length;
          SizeF shadowOffset = this.GetShadowOffset(view);
          for (int index = 0; index < length; ++index)
          {
            points[index].X += shadowOffset.Width;
            points[index].Y += shadowOffset.Height;
          }
          if (this.Brush != null)
          {
            Brush shadowBrush = this.GetShadowBrush(view);
            MapShape.DrawPolygon(g, view, (Pen) null, shadowBrush, points);
          }
          else if (this.Pen != null)
          {
            Pen shadowPen = this.GetShadowPen(view, this.InternalPenWidth);
            MapShape.DrawPolygon(g, view, shadowPen, (Brush) null, points);
          }
        }
        MapShape.DrawPolygon(g, view, this.Pen, this.Brush, this.getPoints());
      }
    }
}
