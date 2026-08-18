// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapEllipse
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
    public class MapEllipse : MapShape
    {
      public override bool ContainsPoint(PointF p)
      {
        if (!base.ContainsPoint(p))
          return false;
        RectangleF bounds = this.Bounds;
        float num1 = this.InternalPenWidth / 2f;
        float num2 = bounds.Width / 2f;
        float num3 = bounds.Height / 2f;
        float num4 = bounds.X + num2;
        float num5 = bounds.Y + num3;
        float num6 = num2 + num1;
        float num7 = num3 + num1;
        if ((double) num6 == 0.0 || (double) num7 == 0.0)
          return false;
        double num8 = (double) p.X - (double) num4;
        float num9 = p.Y - num5;
        return num8 * num8 / ((double) num6 * (double) num6) + (double) num9 * (double) num9 / ((double) num7 * (double) num7) <= 1.0;
      }

      public override bool GetNearestIntersectionPoint(PointF p1, PointF p2, out PointF result)
      {
        RectangleF bounds = this.Bounds;
        float num = this.InternalPenWidth / 2f;
        MapObject.InflateRect(ref bounds, num, num);
        return MapEllipse.NearestIntersectionOnEllipse(bounds, p1, p2, out result);
      }

      public override GraphicsPath MakePath()
      {
        GraphicsPath graphicsPath = new GraphicsPath(FillMode.Winding);
        RectangleF bounds = this.Bounds;
        graphicsPath.AddEllipse(bounds.X, bounds.Y, bounds.Width, bounds.Height);
        return graphicsPath;
      }

      public static bool NearestIntersectionOnArc(
        RectangleF rect,
        PointF p1,
        PointF p2,
        out PointF result,
        float startAngle,
        float sweepAngle)
      {
        float num1 = rect.Width / 2f;
        float num2 = rect.Height / 2f;
        float num3 = rect.X + num1;
        float num4 = rect.Y + num2;
        float num5;
        float num6;
        if ((double) sweepAngle < 0.0)
        {
          num5 = startAngle + sweepAngle;
          num6 = -sweepAngle;
        }
        else
        {
          num5 = startAngle;
          num6 = sweepAngle;
        }
        if ((double) p1.X != (double) p2.X)
        {
          float num7 = (double) p1.X <= (double) p2.X ? (float) (((double) p2.Y - (double) p1.Y) / ((double) p2.X - (double) p1.X)) : (float) (((double) p1.Y - (double) p2.Y) / ((double) p1.X - (double) p2.X));
          float num8 = (float) ((double) p1.Y - (double) num4 - (double) num7 * ((double) p1.X - (double) num3));
          float num9 = (float) Math.Sqrt((double) num1 * (double) num1 * ((double) num7 * (double) num7) + (double) num2 * (double) num2 - (double) num8 * (double) num8);
          float x1 = (float) ((-((double) num1 * (double) num1 * (double) num7 * (double) num8) + (double) num1 * (double) num2 * (double) num9) / ((double) num2 * (double) num2 + (double) num1 * (double) num1 * ((double) num7 * (double) num7))) + num3;
          float x2 = (float) ((-((double) num1 * (double) num1 * (double) num7 * (double) num8) - (double) num1 * (double) num2 * (double) num9) / ((double) num2 * (double) num2 + (double) num1 * (double) num1 * ((double) num7 * (double) num7))) + num3;
          float y1 = num7 * (x1 - num3) + num8 + num4;
          float y2 = num7 * (x2 - num3) + num8 + num4;
          float angle1 = MapStroke.GetAngle(x1 - num3, y1 - num4);
          float angle2 = MapStroke.GetAngle(x2 - num3, y2 - num4);
          if ((double) angle1 < (double) num5)
            angle1 += 360f;
          if ((double) angle2 < (double) num5)
            angle2 += 360f;
          if ((double) angle1 > (double) num5 + (double) num6)
            angle1 -= 360f;
          if ((double) angle2 > (double) num5 + (double) num6)
            angle2 -= 360f;
          bool flag1 = (double) angle1 >= (double) num5 && (double) angle1 <= (double) num5 + (double) num6;
          bool flag2 = (double) angle2 >= (double) num5 && (double) angle2 <= (double) num5 + (double) num6;
          if (flag1 & flag2)
          {
            result = (double) Math.Abs((float) (((double) p1.X - (double) x1) * ((double) p1.X - (double) x1))) + (double) Math.Abs((float) (((double) p1.Y - (double) y1) * ((double) p1.Y - (double) y1))) >= (double) (Math.Abs((float) (((double) p1.X - (double) x2) * ((double) p1.X - (double) x2))) + Math.Abs((float) (((double) p1.Y - (double) y2) * ((double) p1.Y - (double) y2)))) ? new PointF(x2, y2) : new PointF(x1, y1);
            return true;
          }
          if (flag1 && !flag2)
          {
            result = new PointF(x1, y1);
            return true;
          }
          if (!flag1 & flag2)
          {
            result = new PointF(x2, y2);
            return true;
          }
          result = new PointF();
          return false;
        }
        float num10 = (float) Math.Sqrt((double) num2 * (double) num2 - (double) num2 * (double) num2 / ((double) num1 * (double) num1) * (((double) p1.X - (double) num3) * ((double) p1.X - (double) num3)));
        float y3 = num4 + num10;
        float y4 = num4 - num10;
        float angle3 = MapStroke.GetAngle(p1.X - num3, y3 - num4);
        float angle4 = MapStroke.GetAngle(p1.X - num3, y4 - num4);
        if ((double) angle3 < (double) num5)
          angle3 += 360f;
        if ((double) angle4 < (double) num5)
          angle4 += 360f;
        if ((double) angle3 > (double) num5 + (double) num6)
          angle3 -= 360f;
        if ((double) angle4 > (double) num5 + (double) num6)
          angle4 -= 360f;
        bool flag3 = (double) angle3 >= (double) num5 && (double) angle3 <= (double) num5 + (double) num6;
        bool flag4 = (double) angle4 >= (double) num5 && (double) angle4 <= (double) num5 + (double) num6;
        if (flag3 & flag4)
        {
          result = (double) Math.Abs(y3 - p1.Y) >= (double) Math.Abs(y4 - p1.Y) ? new PointF(p1.X, y4) : new PointF(p1.X, y3);
          return true;
        }
        if (flag3 && !flag4)
        {
          result = new PointF(p1.X, y3);
          return true;
        }
        if (!flag3 & flag4)
        {
          result = new PointF(p1.X, y4);
          return true;
        }
        result = new PointF();
        return false;
      }

      public static bool NearestIntersectionOnEllipse(
        RectangleF rect,
        PointF p1,
        PointF p2,
        out PointF result)
      {
        if ((double) rect.Width == 0.0)
          return MapStroke.NearestIntersectionOnLine(new PointF(rect.X, rect.Y), new PointF(rect.X, rect.Y + rect.Height), p1, p2, out result);
        if ((double) rect.Height == 0.0)
          return MapStroke.NearestIntersectionOnLine(new PointF(rect.X, rect.Y), new PointF(rect.X + rect.Width, rect.Y), p1, p2, out result);
        float num1 = rect.Width / 2f;
        float num2 = rect.Height / 2f;
        float num3 = rect.X + num1;
        float num4 = rect.Y + num2;
        if ((double) p1.X != (double) p2.X)
        {
          float num5 = (double) p1.X <= (double) p2.X ? (float) (((double) p2.Y - (double) p1.Y) / ((double) p2.X - (double) p1.X)) : (float) (((double) p1.Y - (double) p2.Y) / ((double) p1.X - (double) p2.X));
          float num6 = (float) ((double) p1.Y - (double) num4 - (double) num5 * ((double) p1.X - (double) num3));
          if ((double) num1 * (double) num1 * ((double) num5 * (double) num5) + (double) num2 * (double) num2 - (double) num6 * (double) num6 < 0.0)
          {
            result = new PointF();
            return false;
          }
          float num7 = (float) Math.Sqrt((double) num1 * (double) num1 * ((double) num5 * (double) num5) + (double) num2 * (double) num2 - (double) num6 * (double) num6);
          float x1 = (float) ((-((double) num1 * (double) num1 * (double) num5 * (double) num6) + (double) num1 * (double) num2 * (double) num7) / ((double) num2 * (double) num2 + (double) num1 * (double) num1 * ((double) num5 * (double) num5))) + num3;
          float x2 = (float) ((-((double) num1 * (double) num1 * (double) num5 * (double) num6) - (double) num1 * (double) num2 * (double) num7) / ((double) num2 * (double) num2 + (double) num1 * (double) num1 * ((double) num5 * (double) num5))) + num3;
          float y1 = num5 * (x1 - num3) + num6 + num4;
          float y2 = num5 * (x2 - num3) + num6 + num4;
          result = (double) Math.Abs((float) (((double) p1.X - (double) x1) * ((double) p1.X - (double) x1))) + (double) Math.Abs((float) (((double) p1.Y - (double) y1) * ((double) p1.Y - (double) y1))) >= (double) (Math.Abs((float) (((double) p1.X - (double) x2) * ((double) p1.X - (double) x2))) + Math.Abs((float) (((double) p1.Y - (double) y2) * ((double) p1.Y - (double) y2)))) ? new PointF(x2, y2) : new PointF(x1, y1);
        }
        else
        {
          double num8 = (double) num2 * (double) num2;
          float num9 = num1 * num1;
          float num10 = p1.X - num3;
          float d = (float) (num8 - num8 / (double) num9 * ((double) num10 * (double) num10));
          if ((double) d < 0.0)
          {
            result = new PointF();
            return false;
          }
          float num11 = (float) Math.Sqrt((double) d);
          float y3 = num4 + num11;
          float y4 = num4 - num11;
          result = (double) Math.Abs(y3 - p1.Y) >= (double) Math.Abs(y4 - p1.Y) ? new PointF(p1.X, y4) : new PointF(p1.X, y3);
        }
        return true;
      }

      public override void Paint(Graphics g, MapView view)
      {
        RectangleF bounds = this.Bounds;
        if (this.Shadowed)
        {
          SizeF shadowOffset = this.GetShadowOffset(view);
          if (this.Brush != null)
          {
            Brush shadowBrush = this.GetShadowBrush(view);
            MapShape.DrawEllipse(g, view, (Pen) null, shadowBrush, bounds.X + shadowOffset.Width, bounds.Y + shadowOffset.Height, bounds.Width, bounds.Height);
          }
          else if (this.Pen != null)
          {
            Pen shadowPen = this.GetShadowPen(view, this.InternalPenWidth);
            MapShape.DrawEllipse(g, view, shadowPen, (Brush) null, bounds.X + shadowOffset.Width, bounds.Y + shadowOffset.Height, bounds.Width, bounds.Height);
          }
        }
        MapShape.DrawEllipse(g, view, this.Pen, this.Brush, bounds.X, bounds.Y, bounds.Width, bounds.Height);
      }
    }
}
