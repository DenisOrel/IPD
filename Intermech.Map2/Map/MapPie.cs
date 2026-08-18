// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapPie
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace Intermech.Map
{
    [Serializable]
    public class MapPie : MapShape
    {
      public const int ChangedResizableEndAngle = 1454;
      public const int ChangedResizableStartAngle = 1453;
      public const int ChangedStartAngle = 1451;
      public const int ChangedSweepAngle = 1452;
      public const int EndAngleHandleID = 1040;
      private const int flagResizableEndAngle = 2097152 /*0x200000*/;
      private const int flagResizableStartAngle = 1048576 /*0x100000*/;
      private float myStartAngle;
      private float mySweepAngle;
      public const int StartAngleHandleID = 1039;

      public MapPie()
      {
        this.myStartAngle = 0.0f;
        this.mySweepAngle = 60f;
        this.InternalFlags |= 512 /*0x0200*/;
        this.InternalFlags |= 3145728 /*0x300000*/;
      }

      public override void AddSelectionHandles(MapSelection sel, MapObject selectedObj)
      {
        base.AddSelectionHandles(sel, selectedObj);
        if (!this.CanReshape())
          return;
        if (this.ResizableStartAngle)
        {
          RectangleF bounds1 = this.Bounds;
          PointF pointAtAngle = this.GetPointAtAngle(this.StartAngle);
          if (sel.CreateResizeHandle((MapObject) this, selectedObj, pointAtAngle, 1039, true).MapObject is MapHandle mapObject)
          {
            mapObject.Style = MapHandleStyle.Diamond;
            mapObject.Brush = MapShape.Brushes_Yellow;
            RectangleF bounds2 = mapObject.Bounds;
            MapObject.InflateRect(ref bounds2, 1f, 1f);
            mapObject.Bounds = bounds2;
          }
        }
        if (!this.ResizableEndAngle)
          return;
        RectangleF bounds3 = this.Bounds;
        PointF pointAtAngle1 = this.GetPointAtAngle(this.StartAngle + this.SweepAngle);
        if (!(sel.CreateResizeHandle((MapObject) this, selectedObj, pointAtAngle1, 1040, true).MapObject is MapHandle mapObject1))
          return;
        mapObject1.Style = MapHandleStyle.Diamond;
        mapObject1.Brush = MapShape.Brushes_Yellow;
        RectangleF bounds4 = mapObject1.Bounds;
        MapObject.InflateRect(ref bounds4, 1f, 1f);
        mapObject1.Bounds = bounds4;
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 1451:
            this.StartAngle = e.GetFloat(undo);
            break;
          case 1452:
            this.SweepAngle = e.GetFloat(undo);
            break;
          case 1453:
            this.ResizableStartAngle = (bool) e.GetValue(undo);
            break;
          case 1454:
            this.ResizableEndAngle = (bool) e.GetValue(undo);
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      public override bool ContainsPoint(PointF p)
      {
        if (base.ContainsPoint(p))
        {
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
          if (num8 * num8 / ((double) num6 * (double) num6) + (double) num9 * (double) num9 / ((double) num7 * (double) num7) > 1.0)
            return false;
          float angle = MapStroke.GetAngle(p.X - num4, p.Y - num5);
          float num10;
          float num11;
          if ((double) this.SweepAngle < 0.0)
          {
            num10 = this.StartAngle + this.SweepAngle;
            num11 = -this.SweepAngle;
          }
          else
          {
            num10 = this.StartAngle;
            num11 = this.SweepAngle;
          }
          if ((double) num11 > 360.0)
            return true;
          if ((double) num10 + (double) num11 > 360.0)
            return (double) angle >= (double) num10 || (double) angle <= (double) num10 + (double) num11 - 360.0;
          if ((double) angle >= (double) num10)
            return (double) angle <= (double) num10 + (double) num11;
        }
        return false;
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
        if ((whichHandle == 1039 || whichHandle == 1040) && (this.ResizesRealtime || evttype == MapInputState.Finish || evttype == MapInputState.Cancel))
        {
          if (whichHandle == 1039)
          {
            RectangleF bounds = this.Bounds;
            float num1 = bounds.Width / 2f;
            float num2 = bounds.Height / 2f;
            float num3 = bounds.X + num1;
            float num4 = bounds.Y + num2;
            float angle = MapStroke.GetAngle(newPoint.X - num3, newPoint.Y - num4);
            float num5 = this.SweepAngle - (angle - this.StartAngle);
            if ((double) this.SweepAngle >= 0.0)
            {
              if ((double) num5 < 0.0)
                num5 += 360f;
            }
            else if ((double) num5 >= 0.0)
              num5 -= 360f;
            this.SweepAngle = num5;
            this.StartAngle = angle;
          }
          else
          {
            RectangleF bounds = this.Bounds;
            float num6 = bounds.Width / 2f;
            float num7 = bounds.Height / 2f;
            float num8 = bounds.X + num6;
            float num9 = bounds.Y + num7;
            float num10 = MapStroke.GetAngle(newPoint.X - num8, newPoint.Y - num9) - this.StartAngle;
            if ((double) this.SweepAngle >= 0.0)
            {
              if ((double) num10 < 0.0)
                num10 += 360f;
            }
            else if ((double) num10 >= 0.0)
              num10 -= 360f;
            this.SweepAngle = num10;
          }
        }
        else
          base.DoResize(view, origRect, newPoint, whichHandle, evttype, min, max);
      }

      public override bool GetNearestIntersectionPoint(PointF p1, PointF p2, out PointF result)
      {
        RectangleF bounds = this.Bounds;
        float num1 = this.InternalPenWidth / 2f;
        MapObject.InflateRect(ref bounds, num1, num1);
        float num2 = bounds.Width / 2f;
        float num3 = bounds.Height / 2f;
        float x = bounds.X + num2;
        float y = bounds.Y + num3;
        float num4 = p1.X - x;
        float num5 = p1.Y - y;
        float startAngle = this.StartAngle;
        float sweepAngle = this.SweepAngle;
        float ang = startAngle + sweepAngle;
        if ((double) ang > 360.0)
          ang -= 360f;
        bool intersectionPoint = false;
        float num6 = 1E+21f;
        result = new PointF();
        if (-0.0099999997764825821 < (double) num4 && (double) num4 < 0.0099999997764825821)
          ;
        if (-0.0099999997764825821 < (double) num5 && (double) num5 < 0.0099999997764825821)
          ;
        PointF result1;
        if ((double) sweepAngle >= 360.0)
        {
          if (MapEllipse.NearestIntersectionOnEllipse(bounds, p1, p2, out result1))
          {
            float num7 = (float) (((double) p1.X - (double) result1.X) * ((double) p1.X - (double) result1.X) + ((double) p1.Y - (double) result1.Y) * ((double) p1.Y - (double) result1.Y));
            if ((double) num7 < (double) num6)
            {
              intersectionPoint = true;
              result = result1;
              num6 = num7;
            }
          }
        }
        else if ((double) sweepAngle + (double) startAngle > 360.0)
        {
          if (MapEllipse.NearestIntersectionOnArc(bounds, p1, p2, out result1, startAngle, 360f - startAngle))
          {
            float num8 = (float) (((double) p1.X - (double) result1.X) * ((double) p1.X - (double) result1.X) + ((double) p1.Y - (double) result1.Y) * ((double) p1.Y - (double) result1.Y));
            if ((double) num8 < (double) num6)
            {
              intersectionPoint = true;
              result = result1;
              num6 = num8;
            }
          }
          if (MapEllipse.NearestIntersectionOnArc(bounds, p1, p2, out result1, 0.0f, sweepAngle - (360f - startAngle)))
          {
            float num9 = (float) (((double) p1.X - (double) result1.X) * ((double) p1.X - (double) result1.X) + ((double) p1.Y - (double) result1.Y) * ((double) p1.Y - (double) result1.Y));
            if ((double) num9 < (double) num6)
            {
              intersectionPoint = true;
              result = result1;
              num6 = num9;
            }
          }
        }
        else if (MapEllipse.NearestIntersectionOnArc(bounds, p1, p2, out result1, startAngle, sweepAngle))
        {
          float num10 = (float) (((double) p1.X - (double) result1.X) * ((double) p1.X - (double) result1.X) + ((double) p1.Y - (double) result1.Y) * ((double) p1.Y - (double) result1.Y));
          if ((double) num10 < (double) num6)
          {
            intersectionPoint = true;
            result = result1;
            num6 = num10;
          }
        }
        PointF pointAtAngle1 = this.GetPointAtAngle(startAngle);
        if (MapStroke.NearestIntersectionOnLine(new PointF(x, y), pointAtAngle1, p1, p2, out result1))
        {
          float num11 = (float) (((double) p1.X - (double) result1.X) * ((double) p1.X - (double) result1.X) + ((double) p1.Y - (double) result1.Y) * ((double) p1.Y - (double) result1.Y));
          if ((double) num11 < (double) num6)
          {
            intersectionPoint = true;
            result = result1;
            num6 = num11;
          }
        }
        PointF pointAtAngle2 = this.GetPointAtAngle(ang);
        if (MapStroke.NearestIntersectionOnLine(new PointF(x, y), pointAtAngle2, p1, p2, out result1))
        {
          if (((double) p1.X - (double) result1.X) * ((double) p1.X - (double) result1.X) + ((double) p1.Y - (double) result1.Y) * ((double) p1.Y - (double) result1.Y) < (double) num6)
          {
            intersectionPoint = true;
            result = result1;
          }
        }
        return intersectionPoint;
      }

      internal PointF GetPointAtAngle(float ang)
      {
        RectangleF bounds = this.Bounds;
        float num1 = bounds.Width / 2f;
        float num2 = bounds.Height / 2f;
        float x = bounds.X + num1;
        float y = bounds.Y + num2;
        if ((double) num1 == 0.0)
          return new PointF(x, y);
        float num3 = (float) Math.Cos((double) ang / 180.0 * Math.PI);
        float num4 = (float) (1.0 - (double) num2 * (double) num2 / ((double) num1 * (double) num1));
        float num5 = num1 * (float) Math.Sqrt((1.0 - (double) num4) / (1.0 - (double) num4 * (double) num3 * (double) num3)) * num3;
        return new PointF(x + num5, y + (float) Math.Tan((double) ang / 180.0 * Math.PI) * num5);
      }

      public override GraphicsPath MakePath()
      {
        GraphicsPath graphicsPath = new GraphicsPath(FillMode.Winding);
        RectangleF bounds = this.Bounds;
        if ((double) bounds.Width > 0.0 && (double) bounds.Height > 0.0)
          graphicsPath.AddPie(bounds.X, bounds.Y, bounds.Width, bounds.Height, this.StartAngle, this.SweepAngle);
        return graphicsPath;
      }

      public override void Paint(Graphics g, MapView view)
      {
        float startAngle = this.StartAngle;
        float sweepAngle = this.SweepAngle;
        RectangleF bounds = this.Bounds;
        if (this.Shadowed)
        {
          SizeF shadowOffset = this.GetShadowOffset(view);
          if (this.Brush != null)
          {
            Brush shadowBrush = this.GetShadowBrush(view);
            MapShape.DrawPie(g, view, (Pen) null, shadowBrush, bounds.X + shadowOffset.Width, bounds.Y + shadowOffset.Height, bounds.Width, bounds.Height, startAngle, sweepAngle);
          }
          else if (this.Pen != null)
          {
            Pen shadowPen = this.GetShadowPen(view, this.InternalPenWidth);
            MapShape.DrawPie(g, view, shadowPen, (Brush) null, bounds.X + shadowOffset.Width, bounds.Y + shadowOffset.Height, bounds.Width, bounds.Height, startAngle, sweepAngle);
          }
        }
        MapShape.DrawPie(g, view, this.Pen, this.Brush, bounds.X, bounds.Y, bounds.Width, bounds.Height, startAngle, sweepAngle);
      }

      [Description("Whether users can resize the end angle of this resizable object.")]
      [Category("Behavior")]
      [DefaultValue(true)]
      public virtual bool ResizableEndAngle
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
          this.Changed(1454, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Category("Behavior")]
      [DefaultValue(true)]
      [Description("Whether users can resize the start angle of this resizable object.")]
      public virtual bool ResizableStartAngle
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
          this.Changed(1453, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Category("Appearance")]
      [DefaultValue(0)]
      [Description("The start angle for the side of the pie slice")]
      public float StartAngle
      {
        get => this.myStartAngle;
        set
        {
          float startAngle = this.myStartAngle;
          if ((double) value < 0.0)
            value = (float) (360.0 - -(double) value % 360.0);
          else if ((double) value >= 360.0)
            value %= 360f;
          if ((double) startAngle == (double) value)
            return;
          this.myStartAngle = value;
          this.ResetPath();
          this.Changed(1451, 0, (object) null, MapObject.MakeRect(startAngle), 0, (object) null, MapObject.MakeRect(value));
        }
      }

      [Category("Appearance")]
      [Description("The sweep angle for the body of the pie slice")]
      [DefaultValue(60)]
      public float SweepAngle
      {
        get => this.mySweepAngle;
        set
        {
          float sweepAngle = this.mySweepAngle;
          if ((double) value > 360.0 || (double) value < -360.0)
            value %= 360f;
          if ((double) sweepAngle == (double) value)
            return;
          this.mySweepAngle = value;
          this.ResetPath();
          this.Changed(1452, 0, (object) null, MapObject.MakeRect(sweepAngle), 0, (object) null, MapObject.MakeRect(value));
        }
      }
    }
}
