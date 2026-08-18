// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapParallelogram
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
    public class MapParallelogram : MapShape
    {
      public const int ChangedDirection = 1468;
      public const int ChangedReshapableSkew = 1467;
      public const int ChangedSkew = 1466;
      private const int flagDirection = 2097152 /*0x200000*/;
      private const int flagReshapableSkew = 1048576 /*0x100000*/;
      private PointF[] myPoints;
      private SizeF mySkew;
      public const int SkewHandleID = 1038;

      public MapParallelogram()
      {
        this.mySkew = new SizeF(10f, 0.0f);
        this.myPoints = new PointF[4];
        this.InternalFlags |= 512 /*0x0200*/;
        this.InternalFlags |= 3145728 /*0x300000*/;
      }

      public override void AddSelectionHandles(MapSelection sel, MapObject selectedObj)
      {
        base.AddSelectionHandles(sel, selectedObj);
        if (!this.CanReshape() || !this.ReshapableSkew)
          return;
        RectangleF bounds1 = this.Bounds;
        SizeF skew = this.Skew;
        PointF loc = new PointF();
        loc = !this.Direction ? new PointF(bounds1.X + bounds1.Width - skew.Width, bounds1.Y + skew.Height) : new PointF(bounds1.X + skew.Width, bounds1.Y + skew.Height);
        if (!(sel.CreateResizeHandle((MapObject) this, selectedObj, loc, 1038, true).MapObject is MapHandle mapObject))
          return;
        mapObject.Style = MapHandleStyle.Diamond;
        mapObject.Brush = MapShape.Brushes_Yellow;
        RectangleF bounds2 = mapObject.Bounds;
        MapObject.InflateRect(ref bounds2, 1f, 1f);
        mapObject.Bounds = bounds2;
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 1466:
            this.Skew = e.GetSize(undo);
            break;
          case 1467:
            this.ReshapableSkew = (bool) e.GetValue(undo);
            break;
          case 1468:
            this.Direction = (bool) e.GetValue(undo);
            break;
          default:
            base.ChangeValue(e, undo);
            this.ResetPath();
            break;
        }
      }

      public override bool ContainsPoint(PointF p)
      {
        return base.ContainsPoint(p) && this.GetPath().IsVisible(p);
      }

      public override MapObject CopyObject(MapCopyDictionary env)
      {
        MapParallelogram mapParallelogram = (MapParallelogram) base.CopyObject(env);
        if (mapParallelogram != null)
          mapParallelogram.myPoints = (PointF[]) this.myPoints.Clone();
        return (MapObject) mapParallelogram;
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
        if (whichHandle == 1038 && (this.ResizesRealtime || evttype == MapInputState.Finish || evttype == MapInputState.Cancel))
        {
          RectangleF bounds = this.Bounds;
          double num = (double) bounds.Height / (double) bounds.Width;
          double left = (double) bounds.Left;
          double top = (double) bounds.Top;
          this.Skew = this.Skew with
          {
            Width = !this.Direction ? ((double) newPoint.X < (double) bounds.X + (double) bounds.Width ? ((double) newPoint.X >= (double) bounds.X ? bounds.X + bounds.Width - newPoint.X : bounds.Width) : 0.0f) : ((double) newPoint.X >= (double) bounds.X ? ((double) newPoint.X < (double) bounds.X + (double) bounds.Width ? newPoint.X - bounds.X : bounds.Width) : 0.0f),
            Height = (double) newPoint.Y >= (double) bounds.Y ? ((double) newPoint.Y < (double) bounds.Y + (double) bounds.Height ? newPoint.Y - bounds.Y : bounds.Height) : 0.0f
          };
          this.ResetPath();
        }
        else
        {
          base.DoResize(view, origRect, newPoint, whichHandle, evttype, min, max);
          this.ResetPath();
        }
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
        float x = bounds.X;
        float y = bounds.Y;
        float num1 = bounds.X + bounds.Width;
        float num2 = bounds.Y + bounds.Height;
        SizeF skew = this.Skew;
        bool direction = this.Direction;
        float num3 = Math.Min(skew.Width, bounds.Width);
        float num4 = Math.Min(skew.Height, bounds.Height);
        this.myPoints[0].X = x + (direction ? num3 : 0.0f);
        this.myPoints[0].Y = y + (direction ? num4 : 0.0f);
        this.myPoints[1].X = num1 - (direction ? 0.0f : num3);
        this.myPoints[1].Y = y + (direction ? 0.0f : num4);
        this.myPoints[2].X = num1 - (direction ? num3 : 0.0f);
        this.myPoints[2].Y = num2 - (direction ? num4 : 0.0f);
        this.myPoints[3].X = x + (direction ? 0.0f : num3);
        this.myPoints[3].Y = num2 - (direction ? 0.0f : num4);
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
        PointF[] points = this.getPoints();
        if (this.Shadowed)
        {
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
          for (int index = 0; index < length; ++index)
          {
            points[index].X -= shadowOffset.Width;
            points[index].Y -= shadowOffset.Height;
          }
        }
        MapShape.DrawPolygon(g, view, this.Pen, this.Brush, points);
      }

      [DefaultValue(true)]
      [Description("Determines the direction of the fixed diagonal.")]
      [Category("Appearance")]
      public virtual bool Direction
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
          this.Changed(1468, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("Whether users can reshape the skew of this resizable object.")]
      [DefaultValue(true)]
      [Category("Behavior")]
      public virtual bool ReshapableSkew
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
          this.Changed(1467, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("The tiltedness of the parallelogram")]
      [Category("Appearance")]
      [TypeConverter(typeof (MapSizeFConverter))]
      public SizeF Skew
      {
        get => this.mySkew;
        set
        {
          SizeF skew = this.mySkew;
          if ((double) value.Width < 0.0)
            value.Width = 0.0f;
          if ((double) value.Height < 0.0)
            value.Height = 0.0f;
          if (!(skew != value))
            return;
          this.mySkew = value;
          this.ResetPath();
          this.Changed(1466, 0, (object) null, MapObject.MakeRect(skew), 0, (object) null, MapObject.MakeRect(value));
        }
      }
    }
}
