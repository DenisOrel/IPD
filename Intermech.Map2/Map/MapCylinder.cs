// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapCylinder
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
    public class MapCylinder : MapShape
    {
      public const int ChangedMinorRadius = 1481;
      public const int ChangedOrientation = 1482;
      public const int ChangedPerspective = 1483;
      public const int ChangedResizableRadius = 1484;
      private const int flagResizableRadius = 1048576 /*0x100000*/;
      private float myMinorRadius;
      private Orientation myOrientation;
      private MapPerspective myPerspective;
      private PointF[] myPoints;
      public const int RadiusHandleID = 1032;

      public MapCylinder()
      {
        this.myPoints = new PointF[4];
        this.myMinorRadius = 10f;
        this.myOrientation = Orientation.Vertical;
        this.myPerspective = MapPerspective.TopLeft;
        this.InternalFlags |= 512 /*0x0200*/;
        this.InternalFlags |= 1048576 /*0x100000*/;
      }

      public override void AddSelectionHandles(MapSelection sel, MapObject selectedObj)
      {
        base.AddSelectionHandles(sel, selectedObj);
        if (!this.CanReshape() || !this.ResizableRadius)
          return;
        RectangleF bounds1 = this.Bounds;
        PointF loc = new PointF();
        float minorRadius = this.MinorRadius;
        loc = this.Orientation != Orientation.Vertical ? (this.Perspective == MapPerspective.TopLeft || this.Perspective == MapPerspective.BottomLeft ? new PointF(bounds1.X + 2f * minorRadius, bounds1.Y + bounds1.Height / 2f) : new PointF((float) ((double) bounds1.X + (double) bounds1.Width - 2.0 * (double) minorRadius), bounds1.Y + bounds1.Height / 2f)) : (this.Perspective == MapPerspective.TopLeft || this.Perspective == MapPerspective.TopRight ? new PointF(bounds1.X + bounds1.Width / 2f, bounds1.Y + 2f * minorRadius) : new PointF(bounds1.X + bounds1.Width / 2f, (float) ((double) bounds1.Y + (double) bounds1.Height - 2.0 * (double) minorRadius)));
        if (!(sel.CreateResizeHandle((MapObject) this, selectedObj, loc, 1032, true).MapObject is MapHandle mapObject))
          return;
        mapObject.Style = MapHandleStyle.Diamond;
        mapObject.Brush = MapShape.Brushes_Yellow;
        RectangleF bounds2 = mapObject.Bounds;
        MapObject.InflateRect(ref bounds2, 1f, 1f);
        mapObject.Bounds = bounds2;
        if (this.Orientation == Orientation.Horizontal)
          mapObject.Cursor = Cursors.SizeWE;
        else
          mapObject.Cursor = Cursors.SizeNS;
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 1481:
            this.MinorRadius = e.GetFloat(undo);
            break;
          case 1482:
            this.Orientation = (Orientation) e.GetValue(undo);
            break;
          case 1483:
            this.Perspective = (MapPerspective) e.GetValue(undo);
            break;
          case 1484:
            this.ResizableRadius = (bool) e.GetValue(undo);
            break;
          default:
            base.ChangeValue(e, undo);
            this.ResetPath();
            break;
        }
      }

      public override bool ContainsPoint(PointF p)
      {
        if (!base.ContainsPoint(p))
          return false;
        GraphicsPath path = this.GetPath(0.0f, 0.0f);
        int num = path.IsVisible(p) ? 1 : 0;
        this.DisposePath(path);
        return num != 0;
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
        if (whichHandle == 1032 && (this.ResizesRealtime || evttype == MapInputState.Finish || evttype == MapInputState.Cancel))
        {
          RectangleF bounds = this.Bounds;
          float minorRadius = this.MinorRadius;
          this.MinorRadius = this.myOrientation != Orientation.Vertical ? (this.Perspective == MapPerspective.TopLeft || this.Perspective == MapPerspective.BottomLeft ? ((double) newPoint.X <= (double) bounds.X + (double) bounds.Width ? ((double) newPoint.X >= (double) bounds.X ? (float) (((double) newPoint.X - (double) bounds.X) / 2.0) : 0.0f) : bounds.Width / 2f) : ((double) newPoint.X <= (double) bounds.X + (double) bounds.Width ? ((double) newPoint.X >= (double) bounds.X ? (float) (((double) bounds.X + (double) bounds.Width - (double) newPoint.X) / 2.0) : bounds.Width / 2f) : 0.0f)) : (this.Perspective == MapPerspective.TopLeft || this.Perspective == MapPerspective.TopRight ? ((double) newPoint.Y <= (double) bounds.Y + (double) bounds.Height ? ((double) newPoint.Y >= (double) bounds.Y ? (float) (((double) newPoint.Y - (double) bounds.Y) / 2.0) : 0.0f) : bounds.Height / 2f) : ((double) newPoint.Y <= (double) bounds.Y + (double) bounds.Height ? ((double) newPoint.Y >= (double) bounds.Y ? (float) (((double) bounds.Y + (double) bounds.Height - (double) newPoint.Y) / 2.0) : bounds.Height / 2f) : 0.0f));
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
        PointF[] points = this.getPoints(0.0f, 0.0f);
        PointF a1 = MapShape.ExpandPointOnEdge(points[0], bounds, shift);
        PointF b1 = MapShape.ExpandPointOnEdge(points[1], bounds, shift);
        PointF a2 = MapShape.ExpandPointOnEdge(points[2], bounds, shift);
        PointF b2 = MapShape.ExpandPointOnEdge(points[3], bounds, shift);
        float num1 = 1E+21f;
        PointF pointF = new PointF();
        RectangleF rect1;
        RectangleF rect2;
        float startAngle1;
        float startAngle2;
        if (this.Orientation == Orientation.Vertical)
        {
          rect1 = new RectangleF(bounds.X, bounds.Y, bounds.Width, this.MinorRadius * 2f);
          rect2 = new RectangleF(bounds.X, (float) ((double) bounds.Y + (double) bounds.Height - (double) this.MinorRadius * 2.0), bounds.Width, this.MinorRadius * 2f);
          startAngle1 = 180f;
          startAngle2 = 0.0f;
        }
        else
        {
          rect1 = new RectangleF(bounds.X, bounds.Y, this.MinorRadius * 2f, bounds.Height);
          rect2 = new RectangleF((float) ((double) bounds.X + (double) bounds.Width - (double) this.MinorRadius * 2.0), bounds.Y, this.MinorRadius * 2f, bounds.Height);
          startAngle1 = 90f;
          startAngle2 = 270f;
        }
        PointF result1;
        if (MapEllipse.NearestIntersectionOnArc(rect1, p1, p2, out result1, startAngle1, 180f))
        {
          float num2 = (float) (((double) result1.X - (double) p1.X) * ((double) result1.X - (double) p1.X) + ((double) result1.Y - (double) p1.Y) * ((double) result1.Y - (double) p1.Y));
          if ((double) num2 < (double) num1)
          {
            num1 = num2;
            pointF = result1;
          }
        }
        if (this.Orientation == Orientation.Horizontal)
        {
          if (MapEllipse.NearestIntersectionOnArc(rect2, p1, p2, out result1, 270f, 90f))
          {
            float num3 = (float) (((double) result1.X - (double) p1.X) * ((double) result1.X - (double) p1.X) + ((double) result1.Y - (double) p1.Y) * ((double) result1.Y - (double) p1.Y));
            if ((double) num3 < (double) num1)
            {
              num1 = num3;
              pointF = result1;
            }
          }
          if (MapEllipse.NearestIntersectionOnArc(rect2, p1, p2, out result1, 0.0f, 90f))
          {
            float num4 = (float) (((double) result1.X - (double) p1.X) * ((double) result1.X - (double) p1.X) + ((double) result1.Y - (double) p1.Y) * ((double) result1.Y - (double) p1.Y));
            if ((double) num4 < (double) num1)
            {
              num1 = num4;
              pointF = result1;
            }
          }
        }
        else if (MapEllipse.NearestIntersectionOnArc(rect2, p1, p2, out result1, startAngle2, 180f))
        {
          float num5 = (float) (((double) result1.X - (double) p1.X) * ((double) result1.X - (double) p1.X) + ((double) result1.Y - (double) p1.Y) * ((double) result1.Y - (double) p1.Y));
          if ((double) num5 < (double) num1)
          {
            num1 = num5;
            pointF = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(a1, b1, p1, p2, out result1))
        {
          float num6 = (float) (((double) result1.X - (double) p1.X) * ((double) result1.X - (double) p1.X) + ((double) result1.Y - (double) p1.Y) * ((double) result1.Y - (double) p1.Y));
          if ((double) num6 < (double) num1)
          {
            num1 = num6;
            pointF = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(a2, b2, p1, p2, out result1))
        {
          float num7 = (float) (((double) result1.X - (double) p1.X) * ((double) result1.X - (double) p1.X) + ((double) result1.Y - (double) p1.Y) * ((double) result1.Y - (double) p1.Y));
          if ((double) num7 < (double) num1)
          {
            num1 = num7;
            pointF = result1;
          }
        }
        result = pointF;
        return (double) num1 < 1.0000000200408773E+21;
      }

      private GraphicsPath GetPath(float offx, float offy)
      {
        GraphicsPath path;
        if ((double) offx != 0.0 || (double) offy != 0.0)
        {
          path = new GraphicsPath(FillMode.Winding);
        }
        else
        {
          if (this.myPath != null)
            return this.myPath;
          path = new GraphicsPath(FillMode.Winding);
          this.myPath = path;
        }
        RectangleF bounds1 = this.Bounds;
        PointF[] points = this.getPoints(offx, offy);
        float num = this.MinorRadius;
        if ((double) num == 0.0)
        {
          RectangleF bounds2 = this.Bounds;
          bounds2.X += offx;
          bounds2.Y += offy;
          path.AddRectangle(bounds2);
          path.CloseAllFigures();
          return path;
        }
        if (this.Orientation == Orientation.Vertical)
        {
          if ((double) num > (double) bounds1.Height / 2.0)
            num = bounds1.Height / 2f;
          if (this.Perspective == MapPerspective.TopLeft || this.Perspective == MapPerspective.TopRight)
          {
            path.AddEllipse(points[0].X, points[0].Y - num, bounds1.Width, num * 2f);
            path.AddLine(points[3], points[2]);
            path.AddArc(points[1].X, points[1].Y - num, bounds1.Width, num * 2f, 0.0f, 180f);
            path.AddLine(points[1], points[0]);
            return path;
          }
          path.AddArc(points[0].X, points[0].Y - num, bounds1.Width, num * 2f, 180f, 180f);
          path.AddLine(points[3], points[2]);
          path.AddEllipse(points[1].X, points[1].Y - num, bounds1.Width, num * 2f);
          path.AddArc(points[1].X, points[1].Y - num, bounds1.Width, num * 2f, 0.0f, 180f);
          path.AddLine(points[1], points[0]);
          return path;
        }
        if ((double) num > (double) bounds1.Width / 2.0)
          num = bounds1.Width / 2f;
        if (this.Perspective == MapPerspective.TopLeft || this.Perspective == MapPerspective.BottomLeft)
        {
          path.AddEllipse(points[0].X - num, points[0].Y, num * 2f, bounds1.Height);
          path.AddLine(points[0], points[1]);
          path.AddArc(points[1].X - num, points[1].Y, num * 2f, bounds1.Height, 270f, 180f);
          path.AddLine(points[2], points[3]);
          return path;
        }
        path.AddArc(points[0].X - num, points[0].Y, num * 2f, bounds1.Height, 90f, 180f);
        path.AddLine(points[0], points[1]);
        path.AddEllipse(points[1].X - num, points[1].Y, num * 2f, bounds1.Height);
        path.AddArc(points[1].X - num, points[1].Y, num * 2f, bounds1.Height, 270f, 180f);
        path.AddLine(points[2], points[3]);
        return path;
      }

      private PointF[] getPoints(float offx, float offy)
      {
        RectangleF bounds = this.Bounds;
        float num = this.MinorRadius;
        bounds.X += offx;
        bounds.Y += offy;
        if (this.Orientation == Orientation.Vertical)
        {
          if ((double) num > (double) bounds.Height / 2.0)
            num = bounds.Height / 2f;
          this.myPoints[0] = new PointF(bounds.X, bounds.Y + num);
          this.myPoints[1] = new PointF(bounds.X, bounds.Y + bounds.Height - num);
          this.myPoints[2] = new PointF(bounds.X + bounds.Width, bounds.Y + bounds.Height - num);
          this.myPoints[3] = new PointF(bounds.X + bounds.Width, bounds.Y + num);
        }
        else
        {
          if ((double) num > (double) bounds.Width / 2.0)
            num = bounds.Width / 2f;
          this.myPoints[0] = new PointF(bounds.X + num, bounds.Y);
          this.myPoints[1] = new PointF(bounds.X + bounds.Width - num, bounds.Y);
          this.myPoints[2] = new PointF(bounds.X + bounds.Width - num, bounds.Y + bounds.Height);
          this.myPoints[3] = new PointF(bounds.X + num, bounds.Y + bounds.Height);
        }
        return this.myPoints;
      }

      public override GraphicsPath MakePath() => (GraphicsPath) this.GetPath(0.0f, 0.0f).Clone();

      public override void Paint(Graphics g, MapView view)
      {
        if (this.Shadowed)
        {
          SizeF shadowOffset = this.GetShadowOffset(view);
          GraphicsPath path = this.GetPath(shadowOffset.Width, shadowOffset.Height);
          if (this.Brush != null)
          {
            Brush shadowBrush = this.GetShadowBrush(view);
            MapShape.DrawPath(g, view, (Pen) null, shadowBrush, path);
          }
          else if (this.Pen != null)
          {
            Pen shadowPen = this.GetShadowPen(view, this.InternalPenWidth);
            MapShape.DrawPath(g, view, shadowPen, (Brush) null, path);
          }
          this.DisposePath(path);
        }
        GraphicsPath path1 = this.GetPath(0.0f, 0.0f);
        MapShape.DrawPath(g, view, this.Pen, this.Brush, path1);
        this.DisposePath(path1);
      }

      [DefaultValue(10f)]
      [Description("The length of cylinder's ellipse's minor radius.")]
      [Category("Appearance")]
      public virtual float MinorRadius
      {
        get => this.myMinorRadius;
        set
        {
          float minorRadius = this.myMinorRadius;
          if ((double) value < 0.0)
            value = 0.0f;
          if ((double) minorRadius == (double) value)
            return;
          this.myMinorRadius = value;
          this.ResetPath();
          this.Changed(1481, 0, (object) null, MapObject.MakeRect(minorRadius), 0, (object) null, MapObject.MakeRect(value));
        }
      }

      [Description("Whether the parallel lines of the cylinder are drawn horizontally or vertically.")]
      [DefaultValue(1)]
      [Category("Appearance")]
      public virtual Orientation Orientation
      {
        get => this.myOrientation;
        set
        {
          Orientation orientation = this.myOrientation;
          if (orientation == value)
            return;
          this.myOrientation = value;
          this.ResetPath();
          this.Changed(1482, 0, (object) orientation, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("Whether the cylinder's full ellipse is drawn on the top or bottom of the cylinder.")]
      [Category("Appearance")]
      [DefaultValue(0)]
      public virtual MapPerspective Perspective
      {
        get => this.myPerspective;
        set
        {
          MapPerspective perspective = this.myPerspective;
          if (perspective == value)
            return;
          this.myPerspective = value;
          this.ResetPath();
          this.Changed(1483, 0, (object) perspective, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("Whether to add the radius control handle.")]
      [Category("Behavior")]
      [DefaultValue(true)]
      public virtual bool ResizableRadius
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
          this.Changed(1484, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }
    }
}
