// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapCube
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
    public class MapCube : MapShape
    {
      public const int ChangedDepth = 1491;
      public const int ChangedPerspective = 1492;
      public const int ChangedReshapableDepth = 1493;
      public const int DepthHandleID = 1033;
      private const int flagReshapableDepth = 1048576 /*0x100000*/;
      private SizeF _depth;
      private MapPerspective _perspective;
      private PointF[] _points;

      public MapCube()
      {
        this._points = new PointF[7];
        this._depth = new SizeF(10f, 10f);
        this._perspective = MapPerspective.TopLeft;
        this.InternalFlags |= 1048576 /*0x100000*/;
        this.InternalFlags |= 512 /*0x0200*/;
      }

      public override void AddSelectionHandles(MapSelection sel, MapObject selectedObj)
      {
        base.AddSelectionHandles(sel, selectedObj);
        if (!this.CanReshape() || !this.ReshapableDepth)
          return;
        RectangleF bounds1 = this.Bounds;
        PointF pointF = new PointF();
        SizeF depth = this.Depth;
        PointF point = this.getPoints(0.0f, 0.0f)[1];
        if (!(sel.CreateResizeHandle((MapObject) this, selectedObj, point, 1033, true).MapObject is MapHandle mapObject))
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
          case 1491:
            this.Depth = e.GetSize(undo);
            break;
          case 1492:
            this.Perspective = (MapPerspective) e.GetValue(undo);
            break;
          case 1493:
            this.ReshapableDepth = (bool) e.GetValue(undo);
            break;
          default:
            base.ChangeValue(e, undo);
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
        RectangleF bounds = this.Bounds;
        SizeF depth = this.Depth;
        if (whichHandle == 1033 && (this.ResizesRealtime || evttype == MapInputState.Finish || evttype == MapInputState.Cancel))
        {
          if (this.Perspective == MapPerspective.TopRight)
          {
            depth.Height = (double) newPoint.Y <= (double) bounds.Y + (double) bounds.Height ? ((double) newPoint.Y >= (double) bounds.Y ? newPoint.Y - bounds.Y : 0.0f) : bounds.Height;
            depth.Width = (double) newPoint.X <= (double) bounds.X + (double) bounds.Width ? ((double) newPoint.X >= (double) bounds.X ? bounds.X + bounds.Width - newPoint.X : bounds.Width) : 0.0f;
          }
          else if (this.Perspective == MapPerspective.BottomRight)
          {
            depth.Height = (double) newPoint.Y >= (double) bounds.Y ? ((double) newPoint.Y <= (double) bounds.Y + (double) bounds.Height ? bounds.Y + bounds.Height - newPoint.Y : 0.0f) : bounds.Height;
            depth.Width = (double) newPoint.X >= (double) bounds.X ? ((double) newPoint.X <= (double) bounds.X + (double) bounds.Width ? bounds.X + bounds.Width - newPoint.X : 0.0f) : bounds.Width;
          }
          else if (this.Perspective == MapPerspective.TopLeft)
          {
            depth.Height = (double) newPoint.Y <= (double) bounds.Y + (double) bounds.Height ? ((double) newPoint.Y >= (double) bounds.Y ? newPoint.Y - bounds.Y : 0.0f) : bounds.Height;
            depth.Width = (double) newPoint.X <= (double) bounds.X + (double) bounds.Width ? ((double) newPoint.X >= (double) bounds.X ? newPoint.X - bounds.X : 0.0f) : bounds.Width;
          }
          else if (this.Perspective == MapPerspective.BottomLeft)
          {
            depth.Height = (double) newPoint.Y >= (double) bounds.Y ? ((double) newPoint.Y <= (double) bounds.Y + (double) bounds.Height ? bounds.Y + bounds.Height - newPoint.Y : 0.0f) : bounds.Height;
            depth.Width = (double) newPoint.X <= (double) bounds.X + (double) bounds.Width ? ((double) newPoint.X >= (double) bounds.X ? newPoint.X - bounds.X : 0.0f) : bounds.Width;
          }
          this.Depth = depth;
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
        PointF[] points = this.getPoints(0.0f, 0.0f);
        PointF pointF1 = MapShape.ExpandPointOnEdge(points[0], bounds, shift);
        PointF pointF2 = MapShape.ExpandPointOnEdge(points[4], bounds, shift);
        PointF pointF3 = MapShape.ExpandPointOnEdge(points[5], bounds, shift);
        PointF pointF4 = MapShape.ExpandPointOnEdge(points[6], bounds, shift);
        PointF pointF5 = MapShape.ExpandPointOnEdge(points[2], bounds, shift);
        PointF pointF6 = MapShape.ExpandPointOnEdge(points[3], bounds, shift);
        float x = p1.X;
        float y = p1.Y;
        float num1 = 1E+21f;
        PointF pointF7 = new PointF();
        PointF result1;
        if (MapStroke.NearestIntersectionOnLine(pointF1, pointF2, p1, p2, out result1))
        {
          float num2 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num2 < (double) num1)
          {
            num1 = num2;
            pointF7 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(pointF2, pointF3, p1, p2, out result1))
        {
          float num3 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num3 < (double) num1)
          {
            num1 = num3;
            pointF7 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(pointF3, pointF4, p1, p2, out result1))
        {
          float num4 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num4 < (double) num1)
          {
            num1 = num4;
            pointF7 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(pointF4, pointF5, p1, p2, out result1))
        {
          float num5 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num5 < (double) num1)
          {
            num1 = num5;
            pointF7 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(pointF5, pointF6, p1, p2, out result1))
        {
          float num6 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num6 < (double) num1)
          {
            num1 = num6;
            pointF7 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(pointF6, pointF1, p1, p2, out result1))
        {
          float num7 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num7 < (double) num1)
          {
            num1 = num7;
            pointF7 = result1;
          }
        }
        result = pointF7;
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
        RectangleF bounds = this.Bounds;
        PointF[] points = this.getPoints(offx, offy);
        path.AddLine(points[0], points[1]);
        path.AddLine(points[1], points[2]);
        path.AddLine(points[2], points[3]);
        path.AddLine(points[3], points[0]);
        path.AddLine(points[0], points[4]);
        path.AddLine(points[4], points[5]);
        path.AddLine(points[5], points[1]);
        path.AddLine(points[2], points[6]);
        path.AddLine(points[6], points[5]);
        path.AddLine(points[5], points[1]);
        return path;
      }

      private PointF[] getPoints(float offx, float offy)
      {
        RectangleF bounds = this.Bounds;
        SizeF depth = this.Depth;
        bounds.X += offx;
        bounds.Y += offy;
        if ((double) depth.Width > (double) bounds.Width)
          depth.Width = bounds.Width;
        if ((double) depth.Height > (double) bounds.Height)
          depth.Height = bounds.Height;
        if (this.Perspective == MapPerspective.TopRight)
        {
          this._points[0] = new PointF(bounds.X, bounds.Y + depth.Height);
          this._points[1] = new PointF(bounds.X + bounds.Width - depth.Width, bounds.Y + depth.Height);
          this._points[2] = new PointF(bounds.X + bounds.Width - depth.Width, bounds.Y + bounds.Height);
          this._points[3] = new PointF(bounds.X, bounds.Y + bounds.Height);
          this._points[4] = new PointF(bounds.X + depth.Width, bounds.Y);
          this._points[5] = new PointF(bounds.X + bounds.Width, bounds.Y);
          this._points[6] = new PointF(bounds.X + bounds.Width, bounds.Y + bounds.Height - depth.Height);
        }
        else if (this.Perspective == MapPerspective.BottomRight)
        {
          this._points[0] = new PointF(bounds.X + bounds.Width - depth.Width, bounds.Y);
          this._points[1] = new PointF(bounds.X + bounds.Width - depth.Width, bounds.Y + bounds.Height - depth.Height);
          this._points[2] = new PointF(bounds.X, bounds.Y + bounds.Height - depth.Height);
          this._points[3] = new PointF(bounds.X, bounds.Y);
          this._points[4] = new PointF(bounds.X + bounds.Width, bounds.Y + depth.Height);
          this._points[5] = new PointF(bounds.X + bounds.Width, bounds.Y + bounds.Height);
          this._points[6] = new PointF(bounds.X + depth.Width, bounds.Y + bounds.Height);
        }
        else if (this.Perspective == MapPerspective.TopLeft)
        {
          this._points[0] = new PointF(bounds.X + depth.Width, bounds.Y + bounds.Height);
          this._points[1] = new PointF(bounds.X + depth.Width, bounds.Y + depth.Height);
          this._points[2] = new PointF(bounds.X + bounds.Width, bounds.Y + depth.Height);
          this._points[3] = new PointF(bounds.X + bounds.Width, bounds.Y + bounds.Height);
          this._points[4] = new PointF(bounds.X, bounds.Y + bounds.Height - depth.Height);
          this._points[5] = new PointF(bounds.X, bounds.Y);
          this._points[6] = new PointF(bounds.X + bounds.Width - depth.Width, bounds.Y);
        }
        else if (this.Perspective == MapPerspective.BottomLeft)
        {
          this._points[0] = new PointF(bounds.X + bounds.Width, bounds.Y + bounds.Height - depth.Height);
          this._points[1] = new PointF(bounds.X + depth.Width, bounds.Y + bounds.Height - depth.Height);
          this._points[2] = new PointF(bounds.X + depth.Width, bounds.Y);
          this._points[3] = new PointF(bounds.X + bounds.Width, bounds.Y);
          this._points[4] = new PointF(bounds.X + bounds.Width - depth.Width, bounds.Y + bounds.Height);
          this._points[5] = new PointF(bounds.X, bounds.Y + bounds.Height);
          this._points[6] = new PointF(bounds.X, bounds.Y + depth.Height);
        }
        return this._points;
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

      [Description("The offset of the back square from the forward square giving the impression of depth.")]
      [TypeConverter(typeof (MapSizeFConverter))]
      [Category("Appearance")]
      public virtual SizeF Depth
      {
        get => this._depth;
        set
        {
          SizeF depth = this._depth;
          if ((double) value.Width < 0.0)
            value.Width = 0.0f;
          if ((double) value.Height < 0.0)
            value.Height = 0.0f;
          if (!(depth != value))
            return;
          this._depth = value;
          this.ResetPath();
          this.Changed(1491, 0, (object) null, MapObject.MakeRect(depth), 0, (object) null, MapObject.MakeRect(value));
        }
      }

      [DefaultValue(0)]
      [Category("Appearance")]
      [Description("The direction the back square is offset from the front square.")]
      public virtual MapPerspective Perspective
      {
        get => this._perspective;
        set
        {
          MapPerspective perspective = this._perspective;
          if (perspective == value)
            return;
          this._perspective = value;
          this.ResetPath();
          this.Changed(1492, 0, (object) perspective, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Category("Behavior")]
      [DefaultValue(true)]
      [Description("Whether to add the depth control handle.")]
      public virtual bool ReshapableDepth
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
          this.Changed(1493, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }
    }
}
