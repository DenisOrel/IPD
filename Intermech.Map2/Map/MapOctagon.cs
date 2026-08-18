// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapOctagon
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Intermech.Map
{
    [Serializable]
    public class MapOctagon : MapShape
    {
      public const int ChangedCorner = 1469;
      public const int ChangedReshapableCorner = 1470;
      public const int CornerHeightHandleID = 1031;
      public const int CornerWidthHandleID = 1030;
      private const int flagReshapableCorner = 1048576 /*0x100000*/;
      private SizeF myCorner;
      private PointF[] myPoints;

      public MapOctagon()
      {
        this.myCorner = new SizeF(10f, 10f);
        this.myPoints = new PointF[8];
        this.InternalFlags |= 1048576 /*0x100000*/;
        this.InternalFlags |= 512 /*0x0200*/;
      }

      public override void AddSelectionHandles(MapSelection sel, MapObject selectedObj)
      {
        base.AddSelectionHandles(sel, selectedObj);
        if (!this.CanReshape() || !this.ReshapableCorner)
          return;
        RectangleF bounds1 = this.Bounds;
        PointF loc;
        ref PointF local1 = ref loc;
        double x1 = (double) bounds1.X;
        SizeF corner = this.Corner;
        double width = (double) corner.Width;
        double x2 = x1 + width;
        double y1 = (double) bounds1.Y;
        local1 = new PointF((float) x2, (float) y1);
        if (sel.CreateResizeHandle((MapObject) this, selectedObj, loc, 1030, true).MapObject is MapHandle mapObject1)
        {
          mapObject1.Style = MapHandleStyle.Diamond;
          mapObject1.Brush = MapShape.Brushes_Yellow;
          RectangleF bounds2 = mapObject1.Bounds;
          MapObject.InflateRect(ref bounds2, 1f, 1f);
          mapObject1.Bounds = bounds2;
          mapObject1.Cursor = Cursors.SizeWE;
        }
        ref PointF local2 = ref loc;
        double x3 = (double) bounds1.X;
        double y2 = (double) bounds1.Y;
        corner = this.Corner;
        double height = (double) corner.Height;
        double y3 = y2 + height;
        local2 = new PointF((float) x3, (float) y3);
        if (!(sel.CreateResizeHandle((MapObject) this, selectedObj, loc, 1031, true).MapObject is MapHandle mapObject2))
          return;
        mapObject2.Style = MapHandleStyle.Diamond;
        mapObject2.Brush = MapShape.Brushes_Yellow;
        RectangleF bounds3 = mapObject2.Bounds;
        MapObject.InflateRect(ref bounds3, 1f, 1f);
        mapObject2.Bounds = bounds3;
        mapObject2.Cursor = Cursors.SizeNS;
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 1469:
            this.Corner = e.GetSize(undo);
            break;
          case 1470:
            this.ReshapableCorner = (bool) e.GetValue(undo);
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      public override bool ContainsPoint(PointF p)
      {
        return base.ContainsPoint(p) && this.GetPath().IsVisible(p);
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
        if (whichHandle < 1030 || !this.ResizesRealtime && evttype != MapInputState.Finish && evttype != MapInputState.Cancel)
        {
          base.DoResize(view, origRect, newPoint, whichHandle, evttype, min, max);
        }
        else
        {
          RectangleF bounds = this.Bounds;
          SizeF corner = this.Corner;
          switch (whichHandle)
          {
            case 1030:
              corner.Width = (double) newPoint.X < (double) bounds.X ? 0.0f : ((double) newPoint.X < (double) bounds.X + (double) bounds.Width / 2.0 ? newPoint.X - bounds.X : bounds.Width / 2f);
              break;
            case 1031:
              corner.Height = (double) newPoint.Y < (double) bounds.Y ? 0.0f : ((double) newPoint.Y < (double) bounds.Y + (double) bounds.Height / 2.0 ? newPoint.Y - bounds.Y : bounds.Height / 2f);
              break;
          }
          this.Corner = corner;
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
        PointF pointF5 = MapShape.ExpandPointOnEdge(points[4], bounds, shift);
        PointF pointF6 = MapShape.ExpandPointOnEdge(points[5], bounds, shift);
        PointF pointF7 = MapShape.ExpandPointOnEdge(points[6], bounds, shift);
        PointF pointF8 = MapShape.ExpandPointOnEdge(points[7], bounds, shift);
        float x = p1.X;
        float y = p1.Y;
        float num1 = 1E+21f;
        PointF pointF9 = new PointF();
        PointF result1;
        if (MapStroke.NearestIntersectionOnLine(pointF1, pointF2, p1, p2, out result1))
        {
          float num2 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num2 < (double) num1)
          {
            num1 = num2;
            pointF9 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(pointF2, pointF3, p1, p2, out result1))
        {
          float num3 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num3 < (double) num1)
          {
            num1 = num3;
            pointF9 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(pointF3, pointF4, p1, p2, out result1))
        {
          float num4 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num4 < (double) num1)
          {
            num1 = num4;
            pointF9 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(pointF4, pointF5, p1, p2, out result1))
        {
          float num5 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num5 < (double) num1)
          {
            num1 = num5;
            pointF9 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(pointF5, pointF6, p1, p2, out result1))
        {
          float num6 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num6 < (double) num1)
          {
            num1 = num6;
            pointF9 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(pointF6, pointF7, p1, p2, out result1))
        {
          float num7 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num7 < (double) num1)
          {
            num1 = num7;
            pointF9 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(pointF7, pointF8, p1, p2, out result1))
        {
          float num8 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num8 < (double) num1)
          {
            num1 = num8;
            pointF9 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(pointF8, pointF1, p1, p2, out result1))
        {
          float num9 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num9 < (double) num1)
          {
            num1 = num9;
            pointF9 = result1;
          }
        }
        result = pointF9;
        return (double) num1 < 1.0000000200408773E+21;
      }

      private PointF[] getPoints()
      {
        RectangleF bounds = this.Bounds;
        SizeF corner = this.Corner;
        if ((double) corner.Width > (double) bounds.Width / 2.0)
          corner.Width = bounds.Width / 2f;
        if ((double) corner.Height > (double) bounds.Height / 2.0)
          corner.Height = bounds.Height / 2f;
        this.myPoints[0] = new PointF(bounds.X + corner.Width, bounds.Y);
        this.myPoints[1] = new PointF(bounds.X, bounds.Y + corner.Height);
        this.myPoints[2] = new PointF(bounds.X, bounds.Y + bounds.Height - corner.Height);
        this.myPoints[3] = new PointF(bounds.X + corner.Width, bounds.Y + bounds.Height);
        this.myPoints[4] = new PointF(bounds.X + bounds.Width - corner.Width, bounds.Y + bounds.Height);
        this.myPoints[5] = new PointF(bounds.X + bounds.Width, bounds.Y + bounds.Height - corner.Height);
        this.myPoints[6] = new PointF(bounds.X + bounds.Width, bounds.Y + corner.Height);
        this.myPoints[7] = new PointF(bounds.X + bounds.Width - corner.Width, bounds.Y);
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
        SizeF shadowOffset = this.GetShadowOffset(view);
        PointF[] points = this.getPoints();
        if (this.Shadowed)
        {
          int length = points.Length;
          for (int index = 0; index < length; ++index)
          {
            this.myPoints[index].X += shadowOffset.Width;
            this.myPoints[index].Y += shadowOffset.Height;
          }
          if (this.Brush != null)
          {
            Brush shadowBrush = this.GetShadowBrush(view);
            MapShape.DrawPolygon(g, view, (Pen) null, shadowBrush, this.myPoints);
          }
          else if (this.Pen != null)
          {
            Pen shadowPen = this.GetShadowPen(view, this.InternalPenWidth);
            MapShape.DrawPolygon(g, view, shadowPen, (Brush) null, this.myPoints);
          }
          for (int index = 0; index < length; ++index)
          {
            this.myPoints[index].X -= shadowOffset.Width;
            this.myPoints[index].Y -= shadowOffset.Height;
          }
        }
        MapShape.DrawPolygon(g, view, this.Pen, this.Brush, this.myPoints);
      }

      [Category("Appearance")]
      [TypeConverter(typeof (MapSizeFConverter))]
      [Description("The maximum width and height of each corner")]
      public virtual SizeF Corner
      {
        get => this.myCorner;
        set
        {
          SizeF corner = this.myCorner;
          if ((double) value.Width < 0.0)
            value.Width = 0.0f;
          if ((double) value.Height < 0.0)
            value.Height = 0.0f;
          if (!(corner != value))
            return;
          this.myCorner = value;
          this.ResetPath();
          this.Changed(1469, 0, (object) null, MapObject.MakeRect(corner), 0, (object) null, MapObject.MakeRect(value));
        }
      }

      [Category("Behavior")]
      [DefaultValue(true)]
      [Description("Whether users can reshape the corner of this resizable object.")]
      public virtual bool ReshapableCorner
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
          this.Changed(1470, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }
    }
}
