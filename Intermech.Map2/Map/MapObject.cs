using System;
using System.ComponentModel;
using System.Drawing;


namespace Intermech.Map
{
    [Serializable]
    public abstract class MapObject : IDisposable
    {
      public static readonly RectangleF NullRect = RectangleF.Empty;
      public const int NoHandle = 0;
      public const int NoSpot = 0;
      public const int ChangedBounds = 1001;
      public const int ChangedVisible = 1003;
      public const int ChangedSelectable = 1004;
      public const int ChangedMovable = 1005;
      public const int ChangedCopyable = 1006;
      public const int ChangedResizable = 1007;
      public const int ChangedReshapable = 1008;
      public const int ChangedDeletable = 1009;
      public const int ChangedEditable = 1010;
      public const int ChangedAutoRescales = 1011;
      public const int ChangedResizeRealtime = 1012;
      public const int ChangedShadowed = 1013;
      public const int ChangedAddedObserver = 1014;
      public const int ChangedRemovedObserver = 1015;
      public const int ChangedDragsNode = 1016;
      public const int ChangedPrintable = 1017;
      public const int MiddleCenter = 1;
      public const int MiddleTop = 32 /*0x20*/;
      public const int MiddleRight = 64 /*0x40*/;
      public const int MiddleBottom = 128 /*0x80*/;
      public const int MiddleLeft = 256 /*0x0100*/;
      public const int BottomRight = 8;
      public const int BottomLeft = 16 /*0x10*/;
      public const int BottomCenter = 128 /*0x80*/;
      public const int TopLeft = 2;
      public const int TopRight = 4;
      public const int TopCenter = 32 /*0x20*/;
      public const int LastChangedHint = 10000;
      public const int LastHandle = 8192 /*0x2000*/;
      public const int LastSpot = 8192 /*0x2000*/;
      public const int RepaintAll = 1000;
      public const int Middle = 1;
      internal const int flagVisible = 1;
      internal const int flagSelectable = 2;
      internal const int flagMovable = 4;
      internal const int flagCopyable = 8;
      internal const int flagResizable = 16 /*0x10*/;
      internal const int flagReshapable = 32 /*0x20*/;
      internal const int flagDeletable = 64 /*0x40*/;
      internal const int flagEditable = 128 /*0x80*/;
      internal const int flagAutoRescales = 256 /*0x0100*/;
      internal const int flagResizesRealtime = 512 /*0x0200*/;
      internal const int flagShadowed = 1024 /*0x0400*/;
      internal const int flagDragsNode = 2048 /*0x0800*/;
      internal const int flagSuspendsUpdates = 4096 /*0x1000*/;
      internal const int flagSkipsUndoManager = 8192 /*0x2000*/;
      internal const int flagSkipsBoundsChanged = 16384 /*0x4000*/;
      internal const int flagInvalidBounds = 32768 /*0x8000*/;
      internal const int flagBeingRemoved = 65536 /*0x010000*/;
      internal const int flagInitializing = 131072 /*0x020000*/;
      internal const int flagReserved1 = 262144 /*0x040000*/;
      internal const int flagPrintable = 524288 /*0x080000*/;
      internal const int flagObject1 = 1048576 /*0x100000*/;
      internal const int flagObject2 = 2097152 /*0x200000*/;
      internal const int flagObject3 = 4194304 /*0x400000*/;
      internal const int flagObject4 = 8388608 /*0x800000*/;
      internal const int flagObject5 = 16777216 /*0x01000000*/;
      internal const int flagObject6 = 33554432 /*0x02000000*/;
      internal const int flagObject7 = 67108864 /*0x04000000*/;
      internal const int flagObject8 = 134217728 /*0x08000000*/;
      internal const int flagObject9 = 268435456 /*0x10000000*/;
      internal const int flagObject10 = 536870912 /*0x20000000*/;
      internal const int flagObject11 = 1073741824 /*0x40000000*/;
      private RectangleF _bounds;
      private int _internalFlags;
      private MapLayer _layer;
      private MapCollection _observers;
      private MapGroup _parent;
      private long _ZIndex;
      private static long _ZIndexCounter;

      protected MapObject()
      {
        this._layer = (MapLayer) null;
        this._parent = (MapGroup) null;
        this._bounds = new RectangleF(0.0f, 0.0f, 10f, 10f);
        this.InternalFlags = 524671;
        this._observers = (MapCollection) null;
      }

      public virtual void AddObserver(MapObject obj)
      {
        if (obj == null)
          return;
        if (this._observers == null)
          this._observers = new MapCollection();
        if (this._observers.Contains(obj))
          return;
        this._observers.Add(obj);
        this.Changed(1014, 0, (object) null, MapObject.NullRect, 0, (object) obj, MapObject.NullRect);
      }

      public virtual void AddSelectionHandles(MapSelection sel, MapObject selectedObj)
      {
        this.RemoveSelectionHandles(sel);
        if (!this.CanResize())
        {
          sel.CreateBoundingHandle(this, selectedObj);
        }
        else
        {
          RectangleF bounds = this.Bounds;
          float x1 = bounds.X;
          float x2 = bounds.X + bounds.Width / 2f;
          float x3 = bounds.X + bounds.Width;
          float y1 = bounds.Y;
          float y2 = bounds.Y + bounds.Height / 2f;
          float y3 = bounds.Y + bounds.Height;
          sel.CreateResizeHandle(this, selectedObj, new PointF(x1, y1), 2, true);
          sel.CreateResizeHandle(this, selectedObj, new PointF(x3, y1), 4, true);
          sel.CreateResizeHandle(this, selectedObj, new PointF(x3, y3), 8, true);
          sel.CreateResizeHandle(this, selectedObj, new PointF(x1, y3), 16 /*0x10*/, true);
          if (!this.CanReshape())
            return;
          sel.CreateResizeHandle(this, selectedObj, new PointF(x2, y1), 32 /*0x20*/, true);
          sel.CreateResizeHandle(this, selectedObj, new PointF(x3, y2), 64 /*0x40*/, true);
          sel.CreateResizeHandle(this, selectedObj, new PointF(x2, y3), 128 /*0x80*/, true);
          sel.CreateResizeHandle(this, selectedObj, new PointF(x1, y2), 256 /*0x0100*/, true);
        }
      }

      public virtual bool CanCopy()
      {
        if (!this.Copyable)
          return false;
        return this.Layer == null || this.Layer.CanCopyObjects();
      }

      public virtual bool CanDelete()
      {
        if (!this.Deletable)
          return false;
        return this.Layer == null || this.Layer.CanDeleteObjects();
      }

      public virtual bool CanEdit()
      {
        if (!this.Editable)
          return false;
        return this.Layer == null || this.Layer.CanEditObjects();
      }

      public virtual bool CanMove()
      {
        if (!this.Movable)
          return false;
        return this.Layer == null || this.Layer.CanMoveObjects();
      }

      public virtual bool CanPrint()
      {
        if (!this.Printable)
          return false;
        if (this.Parent != null)
          return this.Parent.CanPrint();
        return this.Layer == null || this.Layer.CanPrintObjects();
      }

      public virtual bool CanReshape()
      {
        if (!this.Reshapable)
          return false;
        return this.Layer == null || this.Layer.CanReshapeObjects();
      }

      public virtual bool CanResize()
      {
        if (!this.Resizable)
          return false;
        return this.Layer == null || this.Layer.CanResizeObjects();
      }

      public virtual bool CanSelect()
      {
        if (!this.Selectable)
          return false;
        return this.Layer == null || this.Layer.CanSelectObjects();
      }

      public virtual bool CanView()
      {
        if (!this.Visible)
          return false;
        if (this.Parent != null)
          return this.Parent.CanView();
        return this.Layer == null || this.Layer.CanViewObjects();
      }

      public virtual void Changed(
        int subhint,
        int oldI,
        object oldVal,
        RectangleF oldRect,
        int newI,
        object newVal,
        RectangleF newRect)
      {
        if (this.SuspendsUpdates)
          return;
        MapLayer layer = this.Layer;
        if (layer != null && layer.LayerCollectionContainer != null)
        {
          RectangleF bounds = this.Bounds;
          layer.LayerCollectionContainer.RaiseChanged(901, subhint, (object) this, oldI, oldVal, oldRect, newI, newVal, newRect);
        }
        if (this._observers == null)
          return;
        foreach (MapObject observer in this._observers)
          observer.OnObservedChanged(this, subhint, oldI, oldVal, oldRect, newI, newVal, newRect);
      }

      public virtual void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 1000:
            break;
          case 1001:
            this._bounds = e.GetRect(undo);
            this.InvalidateViews();
            break;
          case 1003:
            this.Visible = (bool) e.GetValue(undo);
            break;
          case 1004:
            this.Selectable = (bool) e.GetValue(undo);
            break;
          case 1005:
            this.Movable = (bool) e.GetValue(undo);
            break;
          case 1006:
            this.Copyable = (bool) e.GetValue(undo);
            break;
          case 1007:
            this.Resizable = (bool) e.GetValue(undo);
            break;
          case 1008:
            this.Reshapable = (bool) e.GetValue(undo);
            break;
          case 1009:
            this.Deletable = (bool) e.GetValue(undo);
            break;
          case 1010:
            this.Editable = (bool) e.GetValue(undo);
            break;
          case 1011:
            this.AutoRescales = (bool) e.GetValue(undo);
            break;
          case 1012:
            this.ResizesRealtime = (bool) e.GetValue(undo);
            break;
          case 1013:
            this.Shadowed = (bool) e.GetValue(undo);
            break;
          case 1014:
            MapObject newValue = e.NewValue as MapObject;
            if (!undo)
            {
              this.AddObserver(newValue);
              break;
            }
            this.RemoveObserver(newValue);
            break;
          case 1015:
            MapObject oldValue = e.OldValue as MapObject;
            if (!undo)
            {
              this.RemoveObserver(oldValue);
              break;
            }
            this.AddObserver(oldValue);
            break;
          case 1016:
            this.DragsNode = (bool) e.GetValue(undo);
            break;
          case 1017:
            this.Printable = (bool) e.GetValue(undo);
            break;
          default:
            throw new ArgumentOutOfRangeException("Unknown ChangedObject subhint");
        }
      }

      public virtual void Changing(int subhint)
      {
        if (this.SuspendsUpdates)
          return;
        this.Document?.RaiseChanging(901, subhint, (object) this);
      }

      protected virtual RectangleF ComputeBounds() => this.Bounds;

      public virtual PointF ComputeMove(PointF origLoc, PointF newLoc) => newLoc;

      public virtual RectangleF ComputeResize(
        RectangleF origRect,
        PointF newPoint,
        int handle,
        SizeF min,
        SizeF max,
        bool reshape)
      {
        float x = origRect.X;
        float y = origRect.Y;
        float num1 = origRect.X + origRect.Width;
        float num2 = origRect.Y + origRect.Height;
        float num3 = 1f;
        if (!reshape)
        {
          float num4 = origRect.Width;
          float num5 = origRect.Height;
          if ((double) num4 <= 0.0)
            num4 = 1f;
          if ((double) num5 <= 0.0)
            num5 = 1f;
          num3 = num5 / num4;
        }
        RectangleF resize = origRect;
        switch (handle)
        {
          case 2:
            resize.X = Math.Max(newPoint.X, num1 - max.Width);
            resize.X = Math.Min(resize.X, num1 - min.Width);
            resize.Width = num1 - resize.X;
            if ((double) resize.Width <= 0.0)
              resize.Width = 1f;
            resize.Y = Math.Max(newPoint.Y, num2 - max.Height);
            resize.Y = Math.Min(resize.Y, num2 - min.Height);
            resize.Height = num2 - resize.Y;
            if ((double) resize.Height <= 0.0)
              resize.Height = 1f;
            if (!reshape)
            {
              float num6 = resize.Height / resize.Width;
              if ((double) num3 < (double) num6)
              {
                resize.Height = num3 * resize.Width;
                resize.Y = num2 - resize.Height;
                return resize;
              }
              resize.Width = resize.Height / num3;
              resize.X = num1 - resize.Width;
            }
            return resize;
          case 3:
            return resize;
          case 4:
            resize.Width = Math.Min(newPoint.X - x, max.Width);
            resize.Width = Math.Max(resize.Width, min.Width);
            resize.Y = Math.Max(newPoint.Y, num2 - max.Height);
            resize.Y = Math.Min(resize.Y, num2 - min.Height);
            resize.Height = num2 - resize.Y;
            if ((double) resize.Height <= 0.0)
              resize.Height = 1f;
            if (!reshape)
            {
              float num7 = resize.Height / resize.Width;
              if ((double) num3 < (double) num7)
              {
                resize.Height = num3 * resize.Width;
                resize.Y = num2 - resize.Height;
                return resize;
              }
              resize.Width = resize.Height / num3;
            }
            return resize;
          case 8:
            resize.Width = Math.Min(newPoint.X - x, max.Width);
            resize.Width = Math.Max(resize.Width, min.Width);
            resize.Height = Math.Min(newPoint.Y - y, max.Height);
            resize.Height = Math.Max(resize.Height, min.Height);
            if (!reshape)
            {
              float num8 = resize.Height / resize.Width;
              if ((double) num3 < (double) num8)
              {
                resize.Height = num3 * resize.Width;
                return resize;
              }
              resize.Width = resize.Height / num3;
            }
            return resize;
          case 16 /*0x10*/:
            resize.X = Math.Max(newPoint.X, num1 - max.Width);
            resize.X = Math.Min(resize.X, num1 - min.Width);
            resize.Width = num1 - resize.X;
            if ((double) resize.Width <= 0.0)
              resize.Width = 1f;
            resize.Height = Math.Min(newPoint.Y - y, max.Height);
            resize.Height = Math.Max(resize.Height, min.Height);
            if (!reshape)
            {
              float num9 = resize.Height / resize.Width;
              if ((double) num3 < (double) num9)
              {
                resize.Height = num3 * resize.Width;
                return resize;
              }
              resize.Width = resize.Height / num3;
              resize.X = num1 - resize.Width;
            }
            return resize;
          case 32 /*0x20*/:
            resize.Y = Math.Max(newPoint.Y, num2 - max.Height);
            resize.Y = Math.Min(resize.Y, num2 - min.Height);
            resize.Height = num2 - resize.Y;
            if ((double) resize.Height <= 0.0)
              resize.Height = 1f;
            return resize;
          case 64 /*0x40*/:
            resize.Width = Math.Min(newPoint.X - x, max.Width);
            resize.Width = Math.Max(resize.Width, min.Width);
            return resize;
          case 128 /*0x80*/:
            resize.Height = Math.Min(newPoint.Y - y, max.Height);
            resize.Height = Math.Max(resize.Height, min.Height);
            return resize;
          case 256 /*0x0100*/:
            resize.X = Math.Max(newPoint.X, num1 - max.Width);
            resize.X = Math.Min(resize.X, num1 - min.Width);
            resize.Width = num1 - resize.X;
            if ((double) resize.Width <= 0.0)
              resize.Width = 1f;
            return resize;
          default:
            return resize;
        }
      }

      public virtual bool ContainedByRectangle(RectangleF r)
      {
        RectangleF bounds = this.Bounds;
        return (double) r.Width > 0.0 && (double) r.Height > 0.0 && (double) bounds.Width >= 0.0 && (double) bounds.Height >= 0.0 && (double) bounds.X >= (double) r.X && (double) bounds.Y >= (double) r.Y && (double) bounds.X + (double) bounds.Width <= (double) r.X + (double) r.Width && (double) bounds.Y + (double) bounds.Height <= (double) r.Y + (double) r.Height;
      }

      public virtual bool ContainsPoint(PointF p) => MapObject.ContainsRect(this.Bounds, p);

      internal static bool ContainsRect(RectangleF a, PointF b)
      {
        return (double) a.X <= (double) b.X && (double) b.X <= (double) a.X + (double) a.Width && (double) a.Y <= (double) b.Y && (double) b.Y <= (double) a.Y + (double) a.Height;
      }

      internal static bool ContainsRect(RectangleF a, RectangleF b)
      {
        return (double) a.X <= (double) b.X && (double) b.X + (double) b.Width <= (double) a.X + (double) a.Width && (double) a.Y <= (double) b.Y && (double) b.Y + (double) b.Height <= (double) a.Y + (double) a.Height;
      }

      public virtual void CopyNewValueForRedo(MapChangedEventArgs e)
      {
      }

      public virtual MapObject CopyObject(MapCopyDictionary env)
      {
        if ((MapObject) env[(object) this] != null)
          return (MapObject) null;
        MapObject mapObject = (MapObject) this.MemberwiseClone();
        env[(object) this] = (object) mapObject;
        mapObject._layer = (MapLayer) null;
        mapObject._parent = (MapGroup) null;
        if (this._observers != null && this._observers.Count > 0)
          env.Delayeds.Add((object) this);
        mapObject._observers = (MapCollection) null;
        return mapObject;
      }

      public virtual void CopyObjectDelayed(MapCopyDictionary env, MapObject newobj)
      {
        foreach (MapObject observer in this.Observers)
        {
          MapObject mapObject = env[(object) observer] as MapObject;
          newobj.AddObserver(mapObject);
        }
      }

      public virtual void CopyOldValueForUndo(MapChangedEventArgs e)
      {
      }

      public virtual IMapHandle CreateBoundingHandle()
      {
        MapHandle boundingHandle = new MapHandle();
        RectangleF bounds = this.Bounds;
        MapObject.InflateRect(ref bounds, 1f, 1f);
        boundingHandle.Bounds = bounds;
        return (IMapHandle) boundingHandle;
      }

      public virtual MapControl CreateEditor(MapView view) => (MapControl) null;

      public virtual IMapHandle CreateResizeHandle(int handleid) => (IMapHandle) new MapHandle();

      public virtual void DoBeginEdit(MapView view)
      {
      }

      public virtual void DoEndEdit(MapView view)
      {
      }

      public virtual void DoMove(MapView view, PointF origLoc, PointF newLoc)
      {
        this.Location = this.ComputeMove(origLoc, newLoc);
      }

      public virtual void DoResize(
        MapView view,
        RectangleF origRect,
        PointF newPoint,
        int whichHandle,
        MapInputState evttype,
        SizeF min,
        SizeF max)
      {
        if (evttype == MapInputState.Cancel)
        {
          this.Bounds = origRect;
        }
        else
        {
          RectangleF resize = this.ComputeResize(origRect, newPoint, whichHandle, min, max, this.CanReshape() && !view.LastInput.Shift);
          if (this.ResizesRealtime)
          {
            this.Bounds = resize;
          }
          else
          {
            Rectangle view1 = view.ConvertDocToView(resize);
            if (evttype != MapInputState.Finish)
              view.DrawXorBox(view1);
            if (evttype != MapInputState.Finish)
              return;
            this.Bounds = resize;
          }
        }
      }

      public virtual RectangleF ExpandPaintBounds(RectangleF rect, MapView view) => rect;

      public static MapObject FindCommonParent(MapObject a, MapObject b)
      {
        if (a == b)
          return a;
        if (b != null)
        {
          for (MapObject mapObject = a; mapObject != null; mapObject = (MapObject) mapObject.Parent)
          {
            for (MapObject commonParent = b; commonParent != null; commonParent = (MapObject) commonParent.Parent)
            {
              if (commonParent == mapObject)
                return commonParent;
            }
          }
        }
        return (MapObject) null;
      }

      public virtual bool GetNearestIntersectionPoint(PointF p1, PointF p2, out PointF result)
      {
        return MapObject.GetNearestIntersectionPoint(this.Bounds, p1, p2, out result);
      }

      public static bool GetNearestIntersectionPoint(
        RectangleF rect,
        PointF p1,
        PointF p2,
        out PointF result)
      {
        PointF pointF1 = new PointF(rect.X, rect.Y);
        PointF pointF2 = new PointF(rect.X + rect.Width, rect.Y);
        PointF pointF3 = new PointF(rect.X, rect.Y + rect.Height);
        PointF pointF4 = new PointF(rect.X + rect.Width, rect.Y + rect.Height);
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
        if (MapStroke.NearestIntersectionOnLine(pointF2, pointF4, p1, p2, out result1))
        {
          float num3 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num3 < (double) num1)
          {
            num1 = num3;
            pointF5 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(pointF4, pointF3, p1, p2, out result1))
        {
          float num4 = (float) (((double) result1.X - (double) x) * ((double) result1.X - (double) x) + ((double) result1.Y - (double) y) * ((double) result1.Y - (double) y));
          if ((double) num4 < (double) num1)
          {
            num1 = num4;
            pointF5 = result1;
          }
        }
        if (MapStroke.NearestIntersectionOnLine(pointF3, pointF1, p1, p2, out result1))
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

      public virtual PointF GetRectangleSpotLocation(RectangleF r, int spot)
      {
        PointF rectangleSpotLocation = new PointF(r.X, r.Y);
        switch (spot)
        {
          case 1:
            rectangleSpotLocation.X += r.Width / 2f;
            rectangleSpotLocation.Y += r.Height / 2f;
            return rectangleSpotLocation;
          case 2:
          case 3:
            return rectangleSpotLocation;
          case 4:
            rectangleSpotLocation.X += r.Width;
            return rectangleSpotLocation;
          case 8:
            rectangleSpotLocation.X += r.Width;
            rectangleSpotLocation.Y += r.Height;
            return rectangleSpotLocation;
          case 16 /*0x10*/:
            rectangleSpotLocation.Y += r.Height;
            return rectangleSpotLocation;
          case 32 /*0x20*/:
            rectangleSpotLocation.X += r.Width / 2f;
            return rectangleSpotLocation;
          case 64 /*0x40*/:
            rectangleSpotLocation.X += r.Width;
            rectangleSpotLocation.Y += r.Height / 2f;
            return rectangleSpotLocation;
          case 128 /*0x80*/:
            rectangleSpotLocation.X += r.Width / 2f;
            rectangleSpotLocation.Y += r.Height;
            return rectangleSpotLocation;
          case 256 /*0x0100*/:
            rectangleSpotLocation.Y += r.Height / 2f;
            return rectangleSpotLocation;
          default:
            return rectangleSpotLocation;
        }
      }

      public virtual Brush GetShadowBrush(MapView view)
      {
        return view != null ? (Brush) view.GetShadowBrush() : (Brush) null;
      }

      public virtual SizeF GetShadowOffset(MapView view)
      {
        return view != null ? view.ShadowOffset : new SizeF();
      }

      public virtual Pen GetShadowPen(MapView view, float width) => view?.GetShadowPen(width);

      public virtual PointF GetSpotLocation(int spot)
      {
        return this.GetRectangleSpotLocation(this.Bounds, spot);
      }

      public virtual string GetToolTip(MapView view) => (string) null;

      public static void InflateRect(ref RectangleF a, float w, float h)
      {
        a.X -= w;
        a.Width += w * 2f;
        a.Y -= h;
        a.Height += h * 2f;
      }

      public static bool IntersectsRect(RectangleF a, RectangleF b)
      {
        float width1 = a.Width;
        float height1 = a.Height;
        float width2 = b.Width;
        float height2 = b.Height;
        if ((double) width2 >= 0.0 && (double) height2 >= 0.0 && (double) width1 >= 0.0 && (double) height1 >= 0.0)
        {
          float x1 = a.X;
          float y1 = a.Y;
          float x2 = b.X;
          float y2 = b.Y;
          float num1 = width2 + x2;
          float num2 = height2 + y2;
          float num3 = width1 + x1;
          float num4 = height1 + y1;
          if (((double) num1 <= (double) x2 || (double) num1 >= (double) x1) && ((double) num2 <= (double) y2 || (double) num2 >= (double) y1) && ((double) num3 <= (double) x1 || (double) num3 >= (double) x2))
            return (double) num4 <= (double) y1 || (double) num4 >= (double) y2;
        }
        return false;
      }

      public void InvalidateViews()
      {
        if (this.Parent != null)
          this.Parent.InvalidatePaintBounds();
        this.Changed(1000, 0, (object) null, MapObject.NullRect, 0, (object) null, MapObject.NullRect);
      }

      public bool IsChildOf(MapObject obj)
      {
        if (obj is MapGroup)
        {
          for (MapGroup parent = this.Parent; parent != null; parent = parent.Parent)
          {
            if (parent == obj)
              return true;
          }
        }
        return false;
      }

      public static RectangleF MakeRect(PointF p) => new RectangleF(p.X, p.Y, 0.0f, 0.0f);

      public static RectangleF MakeRect(SizeF s) => new RectangleF(0.0f, 0.0f, s.Width, s.Height);

      public static RectangleF MakeRect(float x) => new RectangleF(x, 0.0f, 0.0f, 0.0f);

      protected virtual void OnBoundsChanged(RectangleF old)
      {
      }

      public virtual bool OnContextClick(MapInputEventArgs evt, MapView view) => false;

      public virtual bool OnDoubleClick(MapInputEventArgs evt, MapView view) => false;

      internal long ZIndex => this._ZIndex;

      public virtual void OnGotSelection(MapSelection sel)
      {
        if (!this.IsInDocument || !this.CanView())
          return;
        ++MapObject._ZIndexCounter;
        this._ZIndex = MapObject._ZIndexCounter;
        this.SelectionObject?.AddSelectionHandles(sel, this);
      }

      public virtual bool OnHover(MapInputEventArgs evt, MapView view) => false;

      protected virtual void OnLayerChanged(MapLayer oldlayer, MapLayer newlayer, MapObject mainObj)
      {
      }

      public virtual void OnLostSelection(MapSelection sel)
      {
        this.SelectionObject?.RemoveSelectionHandles(sel);
      }

      public virtual bool OnMouseOver(MapInputEventArgs evt, MapView view) => false;

      protected virtual void OnObservedChanged(
        MapObject observed,
        int subhint,
        int oldI,
        object oldVal,
        RectangleF oldRect,
        int newI,
        object newVal,
        RectangleF newRect)
      {
      }

      protected virtual void OnParentChanged(MapGroup oldgroup, MapGroup newgroup)
      {
      }

      public virtual bool OnSingleClick(MapInputEventArgs evt, MapView view) => false;

      public virtual void Paint(Graphics g, MapView view)
      {
      }

      public virtual MapObject Pick(PointF p, bool selectableOnly)
      {
        if (this.CanView())
        {
          if (!this.ContainsPoint(p))
            return (MapObject) null;
          if (!selectableOnly || this.CanSelect())
            return this;
          MapObject mapObject = this;
          while (mapObject.Parent != null)
          {
            mapObject = (MapObject) mapObject.Parent;
            if (mapObject.CanSelect())
              return mapObject;
          }
        }
        return (MapObject) null;
      }

      public void Remove()
      {
        MapLayer layer = this.Layer;
        if (layer != null)
          layer.Remove(this);
        else
          this.Parent?.Remove(this);
      }

      public virtual void RemoveObserver(MapObject obj)
      {
        if (obj == null || this._observers == null || !this._observers.Contains(obj))
          return;
        this._observers.Remove(obj);
        this.Changed(1015, 0, (object) obj, MapObject.NullRect, 0, (object) null, MapObject.NullRect);
      }

      public virtual void RemoveSelectionHandles(MapSelection sel) => sel.RemoveHandles(this);

      internal void SetBeingRemoved(bool value)
      {
        if (value)
          this.InternalFlags |= 65536 /*0x010000*/;
        else
          this.InternalFlags &= -65537;
      }

      internal void SetLayer(MapLayer value, MapObject mainObj, bool undoing)
      {
        if (this is MapGroup mapGroup)
        {
          foreach (MapObject copy in mapGroup.CopyArray())
            copy.SetLayer(value, mainObj, undoing);
        }
        MapLayer layer = this._layer;
        MapLayer newlayer = value;
        if (layer == newlayer)
          return;
        if (newlayer == null)
        {
          if (!undoing)
            this.OnLayerChanged(layer, (MapLayer) null, mainObj);
          this._layer = (MapLayer) null;
        }
        else
        {
          this._layer = newlayer;
          if (undoing)
            return;
          this.OnLayerChanged(layer, newlayer, mainObj);
        }
      }

      internal void SetParent(MapGroup value, bool undoing)
      {
        MapGroup parent = this._parent;
        MapGroup newgroup = value;
        if (parent == newgroup)
          return;
        if (newgroup == null)
        {
          if (!undoing)
            this.OnParentChanged(parent, (MapGroup) null);
          this.SetLayer((MapLayer) null, this, undoing);
          this._parent = (MapGroup) null;
        }
        else
        {
          this._parent = newgroup;
          this.SetLayer(newgroup.Layer, this, undoing);
          if (undoing)
            return;
          this.OnParentChanged(parent, newgroup);
        }
      }

      /// <summary>
      /// Сдвинуть прямоугольник так, чтобы точка, соответствующая spot, была в указанной точке
      /// </summary>
      /// <param name="r">Исходный прямоугольник</param>
      /// <param name="spot">Идентификатор места (левый верх, центр и т.п.)</param>
      /// <param name="p">Точка, в которой должно быть указанное место прямоугольника</param>
      /// <returns>Сдвинутый прямоугольник</returns>
      public virtual RectangleF SetRectangleSpotLocation(RectangleF r, int spot, PointF p)
      {
        switch (spot)
        {
          case 1:
            r.X = p.X - r.Width / 2f;
            r.Y = p.Y - r.Height / 2f;
            return r;
          case 2:
          case 3:
            r.X = p.X;
            r.Y = p.Y;
            return r;
          case 4:
            r.X = p.X - r.Width;
            r.Y = p.Y;
            return r;
          case 8:
            r.X = p.X - r.Width;
            r.Y = p.Y - r.Height;
            return r;
          case 16 /*0x10*/:
            r.X = p.X;
            r.Y = p.Y - r.Height;
            return r;
          case 32 /*0x20*/:
            r.X = p.X - r.Width / 2f;
            r.Y = p.Y;
            return r;
          case 64 /*0x40*/:
            r.X = p.X - r.Width;
            r.Y = p.Y - r.Height / 2f;
            return r;
          case 128 /*0x80*/:
            r.X = p.X - r.Width / 2f;
            r.Y = p.Y - r.Height;
            return r;
          case 256 /*0x0100*/:
            r.X = p.X;
            r.Y = p.Y - r.Height / 2f;
            return r;
          default:
            r.X = p.X;
            r.Y = p.Y;
            return r;
        }
      }

      public virtual void SetSizeKeepingLocation(SizeF s) => this.Size = s;

      public virtual void SetSpotLocation(int spot, PointF newp)
      {
        this.Bounds = this.SetRectangleSpotLocation(this.Bounds, spot, newp);
      }

      public void SetSpotLocation(int spot, MapObject obj, int otherSpot)
      {
        PointF spotLocation = obj.GetSpotLocation(otherSpot);
        this.SetSpotLocation(spot, spotLocation);
      }

      /// <summary>Вернуть код противоположной точки</summary>
      /// <param name="spot">Код точки</param>
      /// <returns>Код противоположной точки</returns>
      public virtual int SpotOpposite(int spot)
      {
        switch (spot)
        {
          case 1:
            return 1;
          case 2:
            return 8;
          case 3:
            return spot;
          case 4:
            return 16 /*0x10*/;
          case 8:
            return 2;
          case 16 /*0x10*/:
            return 4;
          case 32 /*0x20*/:
            return 128 /*0x80*/;
          case 64 /*0x40*/:
            return 256 /*0x0100*/;
          case 128 /*0x80*/:
            return 32 /*0x20*/;
          case 256 /*0x0100*/:
            return 64 /*0x40*/;
          default:
            return spot;
        }
      }

      protected internal static void Trace(string msg) => System.Diagnostics.Trace.WriteLine(msg);

      internal static RectangleF UnionRect(RectangleF r, PointF p)
      {
        if ((double) p.X < (double) r.X)
        {
          r.Width = r.X + r.Width - p.X;
          r.X = p.X;
        }
        else if ((double) p.X > (double) r.X + (double) r.Width)
          r.Width = p.X - r.X;
        if ((double) p.Y < (double) r.Y)
        {
          r.Height = r.Y + r.Height - p.Y;
          r.Y = p.Y;
          return r;
        }
        if ((double) p.Y > (double) r.Y + (double) r.Height)
          r.Height = p.Y - r.Y;
        return r;
      }

      internal static RectangleF UnionRect(RectangleF a, RectangleF b)
      {
        float x = Math.Min(a.X, b.X);
        float y = Math.Min(a.Y, b.Y);
        float num1 = Math.Max(a.X + a.Width, b.X + b.Width);
        float num2 = Math.Max(a.Y + a.Height, b.Y + b.Height);
        return new RectangleF(x, y, num1 - x, num2 - y);
      }

      [Category("Behavior")]
      [DefaultValue(true)]
      [Description("Whether this object automatically rescales its appearance when its size changes.")]
      public virtual bool AutoRescales
      {
        get => (this.InternalFlags & 256 /*0x0100*/) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 256 /*0x0100*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 256 /*0x0100*/;
          else
            this.InternalFlags &= -257;
          this.Changed(1011, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Browsable(false)]
      public bool BeingRemoved => (this.InternalFlags & 65536 /*0x010000*/) != 0;

      [Description("The coordinate of the bottom side of the Bounds.")]
      [Category("Bounds")]
      public float Bottom
      {
        get
        {
          RectangleF bounds = this.Bounds;
          return bounds.Y + bounds.Height;
        }
        set
        {
          RectangleF bounds = this.Bounds;
          bounds.Y = value - (bounds.Y + bounds.Height);
          this.Bounds = bounds;
        }
      }

      [Category("Bounds")]
      [Browsable(false)]
      public virtual RectangleF Bounds
      {
        get
        {
          if (this.InvalidBounds && !this.SkipsBoundsChanged)
          {
            this.InvalidBounds = false;
            this.SkipsBoundsChanged = true;
            this.Bounds = this.ComputeBounds();
            this.SkipsBoundsChanged = false;
          }
          return this._bounds;
        }
        set
        {
          RectangleF bounds = this._bounds;
          if ((double) value.Width < 0.0 || (double) value.Height < 0.0 || !(bounds != value))
            return;
          this._bounds = value;
          this.Changed(1001, 0, (object) null, bounds, 0, (object) null, value);
          if (!this.SkipsBoundsChanged)
          {
            this.SkipsBoundsChanged = true;
            this.OnBoundsChanged(bounds);
            if (this.InvalidBounds)
            {
              this.InvalidBounds = false;
              this.Bounds = this.ComputeBounds();
            }
          }
          this.SkipsBoundsChanged = false;
          MapGroup parent = this.Parent;
          if (parent == null)
            return;
          parent.InvalidatePaintBounds();
          if (parent.SkipsBoundsChanged)
            return;
          parent.SkipsBoundsChanged = true;
          parent.OnChildBoundsChanged(this, bounds);
          if (parent.InvalidBounds)
          {
            parent.InvalidBounds = false;
            parent.Bounds = parent.ComputeBounds();
          }
          parent.SkipsBoundsChanged = false;
        }
      }

      [Browsable(false)]
      [Category("Bounds")]
      [TypeConverter(typeof (MapPointFConverter))]
      public PointF Center
      {
        get
        {
          RectangleF bounds = this.Bounds;
          return new PointF(bounds.X + bounds.Width / 2f, bounds.Y + bounds.Height / 2f);
        }
        set
        {
          RectangleF bounds = this.Bounds;
          bounds.X = value.X - bounds.Width / 2f;
          bounds.Y = value.Y - bounds.Height / 2f;
          this.Bounds = bounds;
        }
      }

      [Category("Behavior")]
      [DefaultValue(true)]
      [Description("Whether users can copy this object.")]
      public virtual bool Copyable
      {
        get => (this.InternalFlags & 8) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 8) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 8;
          else
            this.InternalFlags &= -9;
          this.Changed(1006, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Category("Behavior")]
      [DefaultValue(true)]
      [Description("Whether users can delete this object.")]
      public virtual bool Deletable
      {
        get => (this.InternalFlags & 64 /*0x40*/) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 64 /*0x40*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 64 /*0x40*/;
          else
            this.InternalFlags &= -65;
          this.Changed(1009, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("The MapDocument to which this object belongs.")]
      [Category("Ownership")]
      public MapDocument Document => this.Layer?.Document;

      [Category("Behavior")]
      [Description("The object that will get dragged when this selected object is dragged.")]
      public virtual MapObject DraggingObject
      {
        get
        {
          if (this.DragsNode)
          {
            for (MapObject parent = (MapObject) this.Parent; parent != null; parent = (MapObject) parent.Parent)
            {
              if (parent is IMapNode || parent.Parent == null)
                return parent;
            }
          }
          return this;
        }
      }

      [DefaultValue(false)]
      [Category("Behavior")]
      [Description("Whether this selected child object, when dragged, drags the node instead.")]
      public virtual bool DragsNode
      {
        get => (this.InternalFlags & 2048 /*0x0800*/) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 2048 /*0x0800*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 2048 /*0x0800*/;
          else
            this.InternalFlags &= -2049;
          this.Changed(1016, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("Whether users can edit this object.")]
      [Category("Behavior")]
      [DefaultValue(false)]
      public virtual bool Editable
      {
        get => (this.InternalFlags & 128 /*0x80*/) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 128 /*0x80*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 128 /*0x80*/;
          else
            this.InternalFlags &= -129;
          this.Changed(1010, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Browsable(false)]
      public virtual MapControl Editor => (MapControl) null;

      [Description("The height of the Bounds.")]
      [Category("Bounds")]
      public float Height
      {
        get => this.Bounds.Height;
        set => this.Bounds = this.Bounds with { Height = value };
      }

      [Browsable(false)]
      public bool Initializing
      {
        get => (this.InternalFlags & 131072 /*0x020000*/) != 0;
        set
        {
          if (value)
            this.InternalFlags |= 131072 /*0x020000*/;
          else
            this.InternalFlags &= -131073;
        }
      }

      internal int InternalFlags
      {
        get => this._internalFlags;
        set => this._internalFlags = value;
      }

      [Browsable(false)]
      protected bool InvalidBounds
      {
        get => (this.InternalFlags & 32768 /*0x8000*/) != 0;
        set
        {
          if (value)
            this.InternalFlags |= 32768 /*0x8000*/;
          else
            this.InternalFlags &= -32769;
        }
      }

      [Browsable(false)]
      public bool IsInDocument
      {
        get
        {
          MapLayer layer = this.Layer;
          return layer != null && layer.IsInDocument;
        }
      }

      [Browsable(false)]
      public bool IsInView
      {
        get
        {
          MapLayer layer = this.Layer;
          return layer != null && layer.IsInView;
        }
      }

      [Browsable(false)]
      public bool IsTopLevel => this._parent == null;

      [Category("Ownership")]
      [Description("The MapLayer to which this object belongs.")]
      public MapLayer Layer => this._layer;

      [Description("The coordinate of the left side of the Bounds.")]
      [Category("Bounds")]
      public float Left
      {
        get => this.Bounds.X;
        set => this.Bounds = this.Bounds with { X = value };
      }

      [Category("Bounds")]
      [TypeConverter(typeof (MapPointFConverter))]
      [Description("The natural location for this object, perhaps different from Position.")]
      public virtual PointF Location
      {
        get => this.Position;
        set => this.Position = value;
      }

      [DefaultValue(true)]
      [Description("Whether users can move this object.")]
      [Category("Behavior")]
      public virtual bool Movable
      {
        get => (this.InternalFlags & 4) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 4) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 4;
          else
            this.InternalFlags &= -5;
          this.Changed(1005, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      public MapCollectionEnumerator Observers
      {
        get
        {
          return this._observers != null ? this._observers.GetEnumerator() : MapCollectionEnumerator.Empty;
        }
      }

      [Description("The parent MapGroup for this object, or null if top-level.")]
      [Category("Ownership")]
      public MapGroup Parent => this._parent;

      [Browsable(false)]
      public MapObject ParentNode
      {
        get
        {
          MapObject parentNode = this;
          while (parentNode.Parent != null && !(parentNode.Parent is MapSubGraph))
            parentNode = (MapObject) parentNode.Parent;
          return parentNode;
        }
      }

      [Browsable(false)]
      [Category("Bounds")]
      [TypeConverter(typeof (MapPointFConverter))]
      public PointF Position
      {
        get
        {
          RectangleF bounds = this.Bounds;
          return new PointF(bounds.X, bounds.Y);
        }
        set
        {
          this.Bounds = this.Bounds with
          {
            X = value.X,
            Y = value.Y
          };
        }
      }

      [DefaultValue(true)]
      [Description("Whether a view can print this object.")]
      [Category("Behavior")]
      public virtual bool Printable
      {
        get => (this.InternalFlags & 524288 /*0x080000*/) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 524288 /*0x080000*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 524288 /*0x080000*/;
          else
            this.InternalFlags &= -524289;
          this.Changed(1017, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [DefaultValue(true)]
      [Category("Behavior")]
      [Description("Whether users can reshape this resizable object.")]
      public virtual bool Reshapable
      {
        get => (this.InternalFlags & 32 /*0x20*/) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 32 /*0x20*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 32 /*0x20*/;
          else
            this.InternalFlags &= -33;
          this.Changed(1008, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [DefaultValue(true)]
      [Description("Whether users can resize this object.")]
      [Category("Behavior")]
      public virtual bool Resizable
      {
        get => (this.InternalFlags & 16 /*0x10*/) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 16 /*0x10*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 16 /*0x10*/;
          else
            this.InternalFlags &= -17;
          this.Changed(1007, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("Whether this object's size continuously changes during a user resizing operation.")]
      [Category("Behavior")]
      [DefaultValue(false)]
      public virtual bool ResizesRealtime
      {
        get => (this.InternalFlags & 512 /*0x0200*/) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 512 /*0x0200*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 512 /*0x0200*/;
          else
            this.InternalFlags &= -513;
          this.Changed(1012, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("The coordinate of the right side of the Bounds.")]
      [Category("Bounds")]
      public float Right
      {
        get
        {
          RectangleF bounds = this.Bounds;
          return bounds.X + bounds.Width;
        }
        set
        {
          RectangleF bounds = this.Bounds;
          bounds.X = value - (bounds.X + bounds.Width);
          this.Bounds = bounds;
        }
      }

      [DefaultValue(true)]
      [Description("Whether users can select this object.")]
      [Category("Behavior")]
      public virtual bool Selectable
      {
        get => (this.InternalFlags & 2) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 2) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 2;
          else
            this.InternalFlags &= -3;
          this.Changed(1004, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("The object that will get the selection handles when this object is selected.")]
      [Category("Appearance")]
      public virtual MapObject SelectionObject => this;

      [DefaultValue(false)]
      [Description("Whether this object is painted with a drop shadow.")]
      [Category("Appearance")]
      public virtual bool Shadowed
      {
        get => (this.InternalFlags & 1024 /*0x0400*/) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 1024 /*0x0400*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 1024 /*0x0400*/;
          else
            this.InternalFlags &= -1025;
          this.Changed(1013, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [TypeConverter(typeof (MapSizeFConverter))]
      [Browsable(false)]
      [Category("Bounds")]
      public SizeF Size
      {
        get
        {
          RectangleF bounds = this.Bounds;
          return new SizeF(bounds.Width, bounds.Height);
        }
        set
        {
          this.Bounds = this.Bounds with
          {
            Width = value.Width,
            Height = value.Height
          };
        }
      }

      private bool SkipsBoundsChanged
      {
        get => (this.InternalFlags & 16384 /*0x4000*/) != 0;
        set
        {
          if (value)
            this.InternalFlags |= 16384 /*0x4000*/;
          else
            this.InternalFlags &= -16385;
        }
      }

      /// <summary>  пропускать ли запись в UndoManager? true- пропускать </summary>
      [Browsable(false)]
      public bool SkipsUndoManager
      {
        get => (this.InternalFlags & 8192 /*0x2000*/) != 0;
        set
        {
          if (value)
            this.InternalFlags |= 8192 /*0x2000*/;
          else
            this.InternalFlags &= -8193;
        }
      }

      [Browsable(false)]
      public bool SuspendsUpdates
      {
        get => (this.InternalFlags & 4096 /*0x1000*/) != 0;
        set
        {
          if (value)
            this.InternalFlags |= 4096 /*0x1000*/;
          else
            this.InternalFlags &= -4097;
        }
      }

      [Category("Bounds")]
      [Description("The coordinate of the top side of the Bounds.")]
      public float Top
      {
        get => this.Bounds.Y;
        set => this.Bounds = this.Bounds with { Y = value };
      }

      [Browsable(false)]
      public MapObject TopLevelObject
      {
        get
        {
          MapObject topLevelObject = this;
          while (topLevelObject.Parent != null)
            topLevelObject = (MapObject) topLevelObject.Parent;
          return topLevelObject;
        }
      }

      [Description("The MapView to which this object belongs.")]
      [Category("Ownership")]
      public MapView View => this.Layer?.View;

      [Description("Whether users can see this object.")]
      [Category("Behavior")]
      [DefaultValue(true)]
      public virtual bool Visible
      {
        get => (this.InternalFlags & 1) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 1) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 1;
          else
            this.InternalFlags &= -2;
          this.Changed(1003, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Category("Bounds")]
      [Description("The width of the Bounds.")]
      public float Width
      {
        get => this.Bounds.Width;
        set => this.Bounds = this.Bounds with { Width = value };
      }

      public virtual void Dispose()
      {
      }
    }
}
