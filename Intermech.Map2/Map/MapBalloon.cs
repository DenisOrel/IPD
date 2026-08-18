// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapBalloon
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.ComponentModel;
using System.Drawing;


namespace Intermech.Map
{
    [Serializable]
    public class MapBalloon : MapComment
    {
      public const int ChangedAnchor = 2310;
      public const int ChangedCorner = 2311;
      public const int ChangedBaseWidth = 2312;
      private MapObject _anchor;
      private float _baseWidth;
      private SizeF _corner;

      public MapBalloon()
      {
        this._anchor = (MapObject) null;
        this._corner = new SizeF(4f, 4f);
        this._baseWidth = 30f;
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 2310:
            this.Initializing = true;
            this.Anchor = (MapObject) e.GetValue(undo);
            this.Initializing = false;
            break;
          case 2311:
            this.Initializing = true;
            this.Corner = e.GetSize(undo);
            this.Initializing = false;
            break;
          case 2312:
            this.Initializing = true;
            this.BaseWidth = e.GetFloat(undo);
            this.Initializing = false;
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      protected override RectangleF ComputeBounds()
      {
        MapText label = this.Label;
        if (label == null)
          return base.ComputeBounds();
        SizeF topLeftMargin = this.TopLeftMargin;
        SizeF bottomRightMargin = this.BottomRightMargin;
        return new RectangleF(label.Left - topLeftMargin.Width, label.Top - topLeftMargin.Height, label.Width + topLeftMargin.Width + bottomRightMargin.Width, label.Height + topLeftMargin.Height + bottomRightMargin.Height);
      }

      public override MapObject CopyObject(MapCopyDictionary env)
      {
        MapBalloon mapBalloon = (MapBalloon) base.CopyObject(env);
        if (mapBalloon == null)
          return (MapObject) mapBalloon;
        env.Delayeds.Add((object) this);
        return (MapObject) mapBalloon;
      }

      public override void CopyObjectDelayed(MapCopyDictionary env, MapObject newobj)
      {
        base.CopyObjectDelayed(env, newobj);
        MapBalloon mapBalloon = (MapBalloon) newobj;
        mapBalloon._anchor = env[(object) this._anchor] as MapObject;
        mapBalloon.LayoutChildren((MapObject) null);
      }

      protected override MapObject CreateBackground()
      {
        MapPolygon background = new MapPolygon();
        background.Shadowed = true;
        background.Selectable = false;
        background.Pen = MapShape.Pens_LightGray;
        background.Brush = MapShape.Brushes_LemonChiffon;
        return (MapObject) background;
      }

      public override RectangleF ExpandPaintBounds(RectangleF rect, MapView view)
      {
        MapObject background = this.Background;
        if (background != null)
        {
          rect = MapObject.UnionRect(rect, background.Bounds);
          rect = background.ExpandPaintBounds(rect, view);
        }
        return rect;
      }

      public override void LayoutChildren(MapObject childchanged)
      {
        if (this.Initializing)
          return;
        MapText label = this.Label;
        if (label == null || !(this.Background is MapPolygon background) || childchanged == background)
          return;
        SizeF topLeftMargin = this.TopLeftMargin;
        SizeF bottomRightMargin = this.BottomRightMargin;
        RectangleF rectangleF = new RectangleF(label.Left - topLeftMargin.Width, label.Top - topLeftMargin.Height, label.Width + topLeftMargin.Width + bottomRightMargin.Width, label.Height + topLeftMargin.Height + bottomRightMargin.Height);
        SizeF corner = this.Corner;
        float num1 = corner.Width;
        if ((double) num1 > (double) rectangleF.Width / 2.0)
          num1 = rectangleF.Width / 2f;
        float num2 = corner.Height;
        if ((double) num2 > (double) rectangleF.Height / 2.0)
          num2 = rectangleF.Height / 2f;
        float x1 = rectangleF.X;
        float y1 = rectangleF.Y;
        float x2 = x1 + num1;
        float y2 = y1 + num2;
        float num3 = x1 + rectangleF.Width / 2f;
        float num4 = y1 + rectangleF.Height / 2f;
        float x3 = x1 + rectangleF.Width - num1;
        float y3 = y1 + rectangleF.Height - num2;
        float x4 = x1 + rectangleF.Width;
        float y4 = y1 + rectangleF.Height;
        RectangleF bounds = background.Bounds;
        bool suspendsUpdates = this.SuspendsUpdates;
        if (!suspendsUpdates)
          background.Changing(1412);
        background.SuspendsUpdates = true;
        background.ClearPoints();
        if (this.Anchor != null)
        {
          float num5 = Math.Min(rectangleF.Width - num1, this.BaseWidth);
          float num6 = Math.Min(rectangleF.Height - num2, this.BaseWidth);
          float left = label.Left;
          float top = label.Top;
          float right = label.Right;
          float bottom = label.Bottom;
          PointF center1 = label.Center;
          PointF center2 = this.Anchor.Center;
          PointF result1;
          label.GetNearestIntersectionPoint(center2, center1, out result1);
          PointF result2;
          this.Anchor.GetNearestIntersectionPoint(center1, center2, out result2);
          if ((double) result1.Y <= (double) top && (double) result1.X < (double) num3)
          {
            background.AddPoint(x1, y1);
            background.AddPoint(result2);
            background.AddPoint(x1 + num5, y1);
          }
          else
            background.AddPoint(x2, y1);
          if ((double) result1.Y <= (double) top && (double) result1.X >= (double) num3)
          {
            background.AddPoint(x4 - num5, y1);
            background.AddPoint(result2);
            background.AddPoint(x4, y1);
          }
          else
            background.AddPoint(x3, y1);
          if ((double) result1.X >= (double) right & (double) result1.Y < (double) num4)
          {
            background.AddPoint(x4, y1);
            background.AddPoint(result2);
            background.AddPoint(x4, y1 + num6);
          }
          else
            background.AddPoint(x4, y2);
          if ((double) result1.X >= (double) right & (double) result1.Y >= (double) num4)
          {
            background.AddPoint(x4, y4 - num6);
            background.AddPoint(result2);
            background.AddPoint(x4, y4);
          }
          else
            background.AddPoint(x4, y3);
          if ((double) result1.Y >= (double) bottom && (double) result1.X >= (double) num3)
          {
            background.AddPoint(x4, y4);
            background.AddPoint(result2);
            background.AddPoint(x4 - num5, y4);
          }
          else
            background.AddPoint(x3, y4);
          if ((double) result1.Y >= (double) bottom && (double) result1.X < (double) num3)
          {
            background.AddPoint(x1 + num5, y4);
            background.AddPoint(result2);
            background.AddPoint(x1, y4);
          }
          else
            background.AddPoint(x2, y4);
          if ((double) result1.X <= (double) left && (double) result1.Y >= (double) num4)
          {
            background.AddPoint(x1, y4);
            background.AddPoint(result2);
            background.AddPoint(x1, y4 - num6);
          }
          else
            background.AddPoint(x1, y3);
          if ((double) result1.X <= (double) left && (double) result1.Y < (double) num4)
          {
            background.AddPoint(x1, y1 + num6);
            background.AddPoint(result2);
            background.AddPoint(x1, y1);
          }
          else
            background.AddPoint(x1, y2);
        }
        else
        {
          background.AddPoint(x2, y1);
          background.AddPoint(x3, y1);
          background.AddPoint(x4, y2);
          background.AddPoint(x4, y3);
          background.AddPoint(x3, y4);
          background.AddPoint(x2, y4);
          background.AddPoint(x1, y3);
          background.AddPoint(x1, y2);
        }
        background.SuspendsUpdates = suspendsUpdates;
        if (suspendsUpdates)
          return;
        background.Changed(1412, 0, (object) null, bounds, 0, (object) null, background.Bounds);
      }

      protected override void MoveChildren(RectangleF old)
      {
        base.MoveChildren(old);
        this.LayoutChildren((MapObject) null);
      }

      protected override void OnLayerChanged(MapLayer oldlayer, MapLayer newlayer, MapObject mainObj)
      {
        base.OnLayerChanged(oldlayer, newlayer, mainObj);
        if (oldlayer == null || newlayer != null || this.Anchor == null)
          return;
        this.Anchor.RemoveObserver((MapObject) this);
      }

      protected override void OnObservedChanged(
        MapObject observed,
        int subhint,
        int oldI,
        object oldVal,
        RectangleF oldRect,
        int newI,
        object newVal,
        RectangleF newRect)
      {
        base.OnObservedChanged(observed, subhint, oldI, oldVal, oldRect, newI, newVal, newRect);
        if (subhint != 1001 || observed != this.Anchor)
          return;
        this.LayoutChildren((MapObject) null);
      }

      [Description("The object that the balloon comment is pointing at")]
      public virtual MapObject Anchor
      {
        get => this._anchor;
        set
        {
          MapObject anchor = this._anchor;
          if (anchor == value)
            return;
          anchor?.RemoveObserver((MapObject) this);
          this._anchor = value;
          value?.AddObserver((MapObject) this);
          this.Changed(2310, 0, (object) anchor, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
          this.LayoutChildren((MapObject) null);
        }
      }

      [Category("Appearance")]
      [DefaultValue(30)]
      [Description("The width of the base of the balloon's pointer")]
      public virtual float BaseWidth
      {
        get => this._baseWidth;
        set
        {
          float baseWidth = this._baseWidth;
          if ((double) baseWidth == (double) value || (double) value <= 0.0)
            return;
          this._baseWidth = value;
          this.Changed(2312, 0, (object) null, MapObject.MakeRect(baseWidth), 0, (object) null, MapObject.MakeRect(value));
          this.LayoutChildren((MapObject) null);
        }
      }

      private SizeF Corner
      {
        get => this._corner;
        set
        {
          SizeF corner = this._corner;
          if (!(corner != value) || (double) value.Width < 0.0 || (double) value.Height < 0.0)
            return;
          this._corner = value;
          this.Changed(2311, 0, (object) null, MapObject.MakeRect(corner), 0, (object) null, MapObject.MakeRect(value));
          this.LayoutChildren((MapObject) null);
        }
      }
    }
}
