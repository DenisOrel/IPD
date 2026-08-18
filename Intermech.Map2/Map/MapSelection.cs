// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapSelection
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections;
using System.Drawing;


namespace Intermech.Map
{
    [Serializable]
    public class MapSelection : MapCollection
    {
      [NonSerialized]
      private Pen myBoundingHandlePen;
      [NonSerialized]
      private bool myFocused;
      [NonSerialized]
      private Hashtable myHandles;
      private SizeF myHotSpot;
      private Hashtable myObjTable;
      [NonSerialized]
      private SolidBrush myResizeHandleBrush;
      [NonSerialized]
      private Pen myResizeHandlePen;
      private Color myResizeHandlePenColor;
      [NonSerialized]
      private MapView myView;

      public MapSelection(MapView view)
      {
        this.myView = (MapView) null;
        this.myObjTable = new Hashtable();
        this.myHotSpot = new SizeF();
        this.myHandles = (Hashtable) null;
        this.myBoundingHandlePen = (Pen) null;
        this.myResizeHandlePen = (Pen) null;
        this.myResizeHandlePenColor = Color.Black;
        this.myResizeHandleBrush = (SolidBrush) null;
        this.myFocused = true;
        this.myView = view;
      }

      public override void Add(MapObject obj)
      {
        if (obj == null)
          return;
        MapView view = this.View;
        if (view != null && view.Selection == this && this.Count >= view.MaximumSelectionCount || this.Contains(obj))
          return;
        if (view != null && obj.Document != view.Document && obj.View != view)
          throw new ArgumentException("Selected objects must belong to the view or its document");
        this.addToSelection(obj);
      }

      public void AddAllSelectionHandles()
      {
        foreach (MapObject selectedObj in (MapCollection) this)
        {
          MapObject selectionObject = selectedObj.SelectionObject;
          if (selectionObject != null)
          {
            if (selectedObj.CanView())
              selectionObject.AddSelectionHandles(this, selectedObj);
            else
              selectionObject.RemoveSelectionHandles(this);
          }
        }
      }

      public virtual void AddHandle(MapObject obj, IMapHandle handle)
      {
        if (this.myHandles == null)
          this.myHandles = new Hashtable();
        object handle1 = this.myHandles[(object) obj];
        if (handle1 == null)
          this.myHandles[(object) obj] = (object) handle;
        else if (handle1 is ArrayList)
          ((ArrayList) handle1).Add((object) handle);
        else
          this.myHandles[(object) obj] = (object) new ArrayList()
          {
            handle1,
            (object) handle
          };
        if (this.View == null)
          return;
        this.View.Layers.Default.Add(handle.MapObject);
      }

      private void addToSelection(MapObject obj)
      {
        base.Add(obj);
        this.myObjTable[(object) obj] = (object) null;
        MapView view = this.View;
        if (view == null)
          return;
        if (obj.IsInDocument)
          obj.OnGotSelection(this);
        view.RaiseObjectGotSelection(obj);
      }

      public override bool Contains(MapObject obj)
      {
        return obj != null && this.myObjTable.ContainsKey((object) obj);
      }

      public virtual IMapHandle CreateBoundingHandle(MapObject obj, MapObject selectedObj)
      {
        IMapHandle boundingHandle = obj.CreateBoundingHandle();
        if (boundingHandle == null)
          return (IMapHandle) null;
        boundingHandle.SelectedObject = selectedObj;
        MapObject mapObject = boundingHandle.MapObject;
        if (mapObject == null)
          return (IMapHandle) null;
        mapObject.Selectable = false;
        if (mapObject is MapShape mapShape)
        {
          Color color = Color.LightGray;
          MapView view = this.View;
          if (view != null)
            color = !this.Focused ? view.NoFocusSelectionColor : (this.Primary == null || this.Primary.SelectionObject != obj ? view.SecondarySelectionColor : view.PrimarySelectionColor);
          float boundingHandlePenWidth = view.BoundingHandlePenWidth;
          float width = (double) boundingHandlePenWidth == 0.0 ? 0.0f : boundingHandlePenWidth;
          if (this.myBoundingHandlePen == null || this.myBoundingHandlePen.Color != color || (double) this.myBoundingHandlePen.Width != (double) width)
            this.myBoundingHandlePen = new Pen(color, width);
          mapShape.Pen = this.myBoundingHandlePen;
          mapShape.Brush = (Brush) null;
        }
        this.AddHandle(obj, boundingHandle);
        return boundingHandle;
      }

      public virtual IMapHandle CreateResizeHandle(
        MapObject obj,
        MapObject selectedObj,
        PointF loc,
        int handleid,
        bool filled)
      {
        IMapHandle resizeHandle = obj.CreateResizeHandle(handleid);
        if (resizeHandle == null)
          return (IMapHandle) null;
        resizeHandle.HandleID = handleid;
        resizeHandle.SelectedObject = selectedObj;
        MapObject mapObject = resizeHandle.MapObject;
        if (mapObject == null)
          return (IMapHandle) null;
        MapView view = this.View;
        SizeF sizeF = mapObject.Size;
        if ((double) sizeF.Width <= 0.0 || (double) sizeF.Height <= 0.0)
          sizeF = view != null ? view.ResizeHandleSize : new SizeF(6f, 6f);
        mapObject.Bounds = new RectangleF(loc.X - sizeF.Width / 2f, loc.Y - sizeF.Height / 2f, sizeF.Width, sizeF.Height);
        mapObject.Selectable = handleid != 0;
        if (mapObject is MapShape mapShape)
        {
          Color color = Color.LightGray;
          if (view != null)
            color = !this.Focused ? view.NoFocusSelectionColor : (this.Primary == null || this.Primary.SelectionObject != obj ? view.SecondarySelectionColor : view.PrimarySelectionColor);
          if (filled)
          {
            float resizeHandlePenWidth = view.ResizeHandlePenWidth;
            float width = (double) resizeHandlePenWidth == 0.0 ? 0.0f : resizeHandlePenWidth;
            if (this.myResizeHandlePen == null || this.myResizeHandlePen.Color != this.myResizeHandlePenColor || (double) this.myResizeHandlePen.Width != (double) width)
              this.myResizeHandlePen = new Pen(this.myResizeHandlePenColor, width);
            mapShape.Pen = this.myResizeHandlePen;
            if (this.myResizeHandleBrush == null || this.myResizeHandleBrush.Color != color)
              this.myResizeHandleBrush = new SolidBrush(color);
            mapShape.Brush = (Brush) this.myResizeHandleBrush;
          }
          else
          {
            float resizeHandlePenWidth = view.ResizeHandlePenWidth;
            float width = (double) resizeHandlePenWidth == 0.0 ? 0.0f : resizeHandlePenWidth + 1f;
            if (this.myResizeHandlePen == null || this.myResizeHandlePen.Color != color || (double) this.myResizeHandlePen.Width != (double) width)
              this.myResizeHandlePen = new Pen(color, width);
            mapShape.Pen = this.myResizeHandlePen;
            mapShape.Brush = (Brush) null;
          }
        }
        this.AddHandle(obj, resizeHandle);
        return resizeHandle;
      }

      public virtual IMapHandle GetAnExistingHandle(MapObject obj)
      {
        if (this.myHandles != null)
        {
          object handle = this.myHandles[(object) obj];
          if (handle == null)
            return (IMapHandle) null;
          if (!(handle is ArrayList))
            return (IMapHandle) handle;
          ArrayList arrayList = (ArrayList) handle;
          if (arrayList.Count > 0)
            return (IMapHandle) arrayList[0];
        }
        return (IMapHandle) null;
      }

      public virtual int GetHandleCount(MapObject obj)
      {
        if (this.myHandles == null)
          return 0;
        object handle = this.myHandles[(object) obj];
        if (handle == null)
          return 0;
        return handle is ArrayList ? ((ArrayList) handle).Count : 1;
      }

      public virtual void OnGotFocus()
      {
        this.myFocused = true;
        if (this.View == null)
          return;
        if (this.View.HidesSelection)
        {
          this.AddAllSelectionHandles();
        }
        else
        {
          if (!(this.View.NoFocusSelectionColor != this.View.PrimarySelectionColor))
            return;
          this.RemoveAllSelectionHandles();
          this.AddAllSelectionHandles();
        }
      }

      public virtual void OnLostFocus()
      {
        this.myFocused = false;
        if (this.View == null)
          return;
        if (this.View.HidesSelection)
        {
          this.RemoveAllSelectionHandles();
        }
        else
        {
          if (!(this.View.NoFocusSelectionColor != this.View.PrimarySelectionColor))
            return;
          this.RemoveAllSelectionHandles();
          this.AddAllSelectionHandles();
        }
      }

      public override void Remove(MapObject obj)
      {
        if (obj == null)
          return;
        if (this.Contains(obj))
          this.removeFromSelection(obj);
        else
          this.RemoveHandles(obj);
      }

      public void RemoveAllSelectionHandles()
      {
        foreach (MapObject backward in this.Backwards)
          backward.SelectionObject?.RemoveSelectionHandles(this);
      }

      private void removeFromSelection(MapObject obj)
      {
        MapObject primary1 = this.Primary;
        this.myObjTable.Remove((object) obj);
        base.Remove(obj);
        MapView view = this.View;
        if (view == null)
          return;
        if (obj.IsInDocument)
          obj.OnLostSelection(this);
        view.RaiseObjectLostSelection(obj);
        if (primary1 != obj || !primary1.IsInDocument)
          return;
        MapObject primary2 = this.Primary;
        if (primary2 == null)
          return;
        primary2.OnLostSelection(this);
        view.RaiseObjectLostSelection(primary2);
        primary2.OnGotSelection(this);
        view.RaiseObjectGotSelection(primary2);
      }

      public virtual void RemoveHandles(MapObject obj)
      {
        if (this.myHandles == null)
          return;
        object handle = this.myHandles[(object) obj];
        if (handle == null)
          return;
        if (this.View != null)
        {
          if (handle is ArrayList arrayList)
          {
            for (int index = 0; index < arrayList.Count; ++index)
            {
              IMapHandle mapHandle = (IMapHandle) arrayList[index];
              MapObject mapObject = mapHandle.MapObject;
              mapHandle.SelectedObject = (MapObject) null;
              mapObject?.Layer?.Remove(mapObject);
            }
          }
          else
          {
            IMapHandle mapHandle = (IMapHandle) handle;
            mapHandle.SelectedObject = (MapObject) null;
            MapObject mapObject = mapHandle.MapObject;
            mapObject?.Layer?.Remove(mapObject);
          }
        }
        this.myHandles.Remove((object) obj);
      }

      public virtual MapObject Select(MapObject obj)
      {
        if (obj == null)
          return (MapObject) null;
        if (this.Primary != obj || this.Count != 1)
        {
          this.Clear();
          this.Add(obj);
        }
        return obj;
      }

      public virtual void Toggle(MapObject obj)
      {
        if (obj == null)
          return;
        if (this.Contains(obj))
          this.Remove(obj);
        else
          this.Add(obj);
      }

      public virtual bool Focused
      {
        get => this.myFocused;
        set => this.myFocused = value;
      }

      public virtual SizeF HotSpot
      {
        get => this.myHotSpot;
        set => this.myHotSpot = value;
      }

      public virtual MapObject Primary => this.First;

      public override object SyncRoot => this.View != null ? (object) this.View : (object) this;

      public MapView View
      {
        get => this.myView;
        set
        {
          if (value == null)
            return;
          this.myView = value;
        }
      }
    }
}
