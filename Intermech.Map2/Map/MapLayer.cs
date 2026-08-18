// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapLayer
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;


namespace Intermech.Map
{
    [DebuggerDisplay("'{Identifier}' Is:{(IsInView) ? \"InView\" : \"InDoc\"}")]
    [Serializable]
    public sealed class MapLayer : IMapCollection, ICollection, IEnumerable, IMapLayerAbilities
    {
      private static readonly RectangleF NullRect = RectangleF.Empty;
      public const int ChangedObject = 901;
      public const int InsertedObject = 902;
      public const int RemovedObject = 903;
      public const int ChangedObjectLayer = 904;
      public const int ChangedAllowView = 910;
      public const int ChangedAllowSelect = 911;
      public const int ChangedAllowMove = 912;
      public const int ChangedAllowCopy = 913;
      public const int ChangedAllowResize = 914;
      public const int ChangedAllowReshape = 915;
      public const int ChangedAllowDelete = 916;
      public const int ChangedAllowInsert = 917;
      public const int ChangedAllowLink = 918;
      public const int ChangedAllowEdit = 919;
      public const int ChangedAllowPrint = 920;
      public const int ChangedIdentifier = 930;
      private bool myAllowCopy;
      private bool myAllowDelete;
      private bool myAllowEdit;
      private bool myAllowInsert;
      private bool myAllowLink;
      private bool myAllowMove;
      private bool myAllowPrint;
      private bool myAllowReshape;
      private bool myAllowResize;
      private bool myAllowSelect;
      private bool myAllowView;
      [NonSerialized]
      private ArrayList myCaches;
      private object myIdentifier;
      private bool myIsInDocument;
      private IMapLayerCollectionContainer myLayerCollectionContainer;
      private ArrayList myObjects;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="hint"></param>
      /// <param name="subhint"></param>
      /// <param name="obj"></param>
      /// <param name="oldI"></param>
      /// <param name="oldVal"></param>
      /// <param name="oldRect"></param>
      /// <param name="newI"></param>
      /// <param name="newVal"></param>
      /// <param name="newRect"></param>
      private void RaiseChanged(
        int hint,
        int subhint,
        object obj,
        int oldI,
        object oldVal,
        RectangleF oldRect,
        int newI,
        object newVal,
        RectangleF newRect)
      {
        if (this.LayerCollectionContainer == null)
          return;
        this.LayerCollectionContainer.RaiseChanged(hint, subhint, obj, oldI, oldVal, oldRect, newI, newVal, newRect);
      }

      internal MapLayer()
      {
        this.myLayerCollectionContainer = (IMapLayerCollectionContainer) null;
        this.myIsInDocument = false;
        this.myObjects = new ArrayList();
        this.myAllowView = true;
        this.myAllowPrint = true;
        this.myAllowSelect = true;
        this.myAllowMove = true;
        this.myAllowCopy = true;
        this.myAllowResize = true;
        this.myAllowReshape = true;
        this.myAllowDelete = true;
        this.myAllowInsert = true;
        this.myAllowLink = true;
        this.myAllowEdit = true;
        this.myIdentifier = (object) null;
        this.myCaches = (ArrayList) null;
      }

      public void Add(MapObject obj)
      {
        if (obj == null)
          return;
        if (obj.Layer != null)
        {
          if (obj.Layer.LayerCollectionContainer != this.LayerCollectionContainer)
            throw new ArgumentException("Cannot add an object to a layer when it is already part of a different document's or view's layer.");
          MapLayer oldLayer = obj.Parent == null ? obj.Layer : throw new ArgumentException("Cannot add an object to a layer when it is part of a group.");
          if (oldLayer == this)
            return;
          this.changeLayer(obj, oldLayer, false);
        }
        else
        {
          if (obj.Parent != null)
            obj.Parent.Remove(obj);
          this.addToLayer(obj, false);
        }
      }

      public IMapCollection AddCollection(IMapCollection coll, bool reparentLinks)
      {
        MapCollection coll1 = new MapCollection();
        foreach (MapObject mapObject in (IEnumerable) coll)
          coll1.Add(mapObject);
        foreach (MapObject mapObject in coll1)
        {
          int num = mapObject.Layer != null ? 1 : 0;
          if (num != 0)
          {
            MapGroup.setAllNoClear(mapObject, true);
            mapObject.Remove();
          }
          this.Add(mapObject);
          if (num != 0)
            MapGroup.setAllNoClear(mapObject, false);
        }
        if (reparentLinks && this.IsInDocument)
          MapSubGraph.ReparentAllLinksToSubGraphs((IMapCollection) coll1, true, this.Document.LinksLayer);
        return (IMapCollection) coll1;
      }

      internal void addToLayer(MapObject obj, bool undoing)
      {
        try
        {
          this.myObjects.Add((object) obj);
          obj.SetLayer(this, obj, undoing);
          this.InsertIntoCache(obj);
          RectangleF bounds = obj.Bounds;
          this.RaiseChanged(902, 0, (object) obj, 0, (object) null, MapLayer.NullRect, 0, (object) this, bounds);
        }
        catch (Exception ex)
        {
          obj.SetBeingRemoved(true);
          this.RemoveFromCache(obj);
          this.myObjects.Remove((object) obj);
          throw ex;
        }
      }

      private bool CacheWanted(MapView view)
      {
        return this.IsInDocument && MapDocument.myCaching && !view.IsPrinting;
      }

      public bool CanCopyObjects()
      {
        if (!this.AllowCopy)
          return false;
        return !this.IsInDocument || this.LayerCollectionContainer.CanCopyObjects();
      }

      public bool CanDeleteObjects()
      {
        if (!this.AllowDelete)
          return false;
        return !this.IsInDocument || this.LayerCollectionContainer.CanDeleteObjects();
      }

      public bool CanEditObjects()
      {
        if (!this.AllowEdit)
          return false;
        return !this.IsInDocument || this.LayerCollectionContainer.CanEditObjects();
      }

      public bool CanInsertObjects()
      {
        if (!this.AllowInsert)
          return false;
        return !this.IsInDocument || this.LayerCollectionContainer.CanInsertObjects();
      }

      public bool CanLinkObjects()
      {
        if (!this.AllowLink)
          return false;
        return !this.IsInDocument || this.LayerCollectionContainer.CanLinkObjects();
      }

      public bool CanMoveObjects()
      {
        if (!this.AllowMove)
          return false;
        return !this.IsInDocument || this.LayerCollectionContainer.CanMoveObjects();
      }

      public bool CanPrintObjects() => this.AllowPrint;

      public bool CanReshapeObjects()
      {
        if (!this.AllowReshape)
          return false;
        return !this.IsInDocument || this.LayerCollectionContainer.CanReshapeObjects();
      }

      public bool CanResizeObjects()
      {
        if (!this.AllowResize)
          return false;
        return !this.IsInDocument || this.LayerCollectionContainer.CanResizeObjects();
      }

      public bool CanSelectObjects()
      {
        if (!this.AllowSelect)
          return false;
        return !this.IsInDocument || this.LayerCollectionContainer.CanSelectObjects();
      }

      public bool CanViewObjects() => this.AllowView;

      internal void changeLayer(MapObject obj, MapLayer oldLayer, bool undoing)
      {
        oldLayer.RemoveFromCache(obj);
        int oldI = MapCollection.fastRemove(oldLayer.myObjects, (object) obj);
        this.myObjects.Add((object) obj);
        obj.SetLayer(this, obj, undoing);
        this.InsertIntoCache(obj);
        RectangleF bounds = obj.Bounds;
        this.RaiseChanged(904, 0, (object) obj, oldI, (object) oldLayer, bounds, -1, (object) this, bounds);
      }

      public void Clear()
      {
        int val1;
        for (int index = this.myObjects.Count; index > 0; index = Math.Min(val1, this.myObjects.Count))
          this.Remove((MapObject) this.myObjects[val1 = index - 1]);
      }

      public bool Contains(MapObject obj) => obj != null && obj.Layer == this;

      public MapObject FirstObject
      {
        get
        {
          return this.myObjects != null && this.myObjects.Count > 0 ? this.myObjects[0] as MapObject : (MapObject) null;
        }
      }

      public MapObject[] CopyArray()
      {
        MapObject[] array = new MapObject[this.Count];
        this.CopyTo(array, 0);
        return array;
      }

      public void CopyTo(Array array, int index) => this.myObjects.CopyTo(array, index);

      public void CopyTo(MapObject[] array, int index) => this.myObjects.CopyTo((Array) array, index);

      internal MapLayer.MapLayerCache FindCache(MapView view)
      {
        foreach (MapLayer.MapLayerCache cach in this.Caches)
        {
          if (cach.View == view)
            return cach;
        }
        return (MapLayer.MapLayerCache) null;
      }

      internal MapLayer.MapLayerCache FindCache(PointF p)
      {
        MapLayer.MapLayerCache cache = (MapLayer.MapLayerCache) null;
        foreach (MapLayer.MapLayerCache cach in this.Caches)
        {
          if (MapObject.ContainsRect(cach.Rect, p) && (cache == null || cach.Objects.Count < cache.Objects.Count))
            cache = cach;
        }
        return cache;
      }

      internal MapLayer.MapLayerCache FindCache(RectangleF r)
      {
        MapLayer.MapLayerCache cache = (MapLayer.MapLayerCache) null;
        foreach (MapLayer.MapLayerCache cach in this.Caches)
        {
          if (MapObject.ContainsRect(cach.Rect, r) && (cache == null || cach.Objects.Count < cache.Objects.Count))
            cache = cach;
        }
        return cache;
      }

      public MapLayerEnumerator GetEnumerator() => new MapLayerEnumerator(this.myObjects, true);

      internal void init(IMapLayerCollectionContainer lcc)
      {
        this.myLayerCollectionContainer = lcc;
        this.myIsInDocument = lcc is MapDocument;
        this.myAllowPrint = this.myIsInDocument;
      }

      internal void InsertIntoCache(MapObject obj)
      {
        RectangleF bounds = obj.Bounds;
        foreach (MapLayer.MapLayerCache cach in this.Caches)
        {
          RectangleF b = obj.ExpandPaintBounds(bounds, cach.View);
          if (MapObject.IntersectsRect(cach.Rect, b))
            cach.Objects.Add((object) obj);
        }
      }

      IEnumerable IMapCollection.Backwards
      {
        get => (IEnumerable) new MapLayerEnumerator(this.myObjects, false);
      }

      public void Paint(Graphics g, MapView view, RectangleF clipRect)
      {
        bool isPrinting = view.IsPrinting;
        ArrayList arrayList = (ArrayList) null;
        if ((isPrinting ? (this.CanPrintObjects() ? 1 : 0) : (this.CanViewObjects() ? 1 : 0)) == 0)
          return;
        RectangleF docExtent = view.DocExtent;
        MapLayer.MapLayerCache mapLayerCache = this.FindCache(view);
        if (mapLayerCache != null && mapLayerCache.Rect == docExtent)
          arrayList = mapLayerCache.Objects;
        if (arrayList == null)
          arrayList = this.myObjects;
        bool flag = mapLayerCache == null && this.CacheWanted(view);
        if (flag)
        {
          if (mapLayerCache == null)
          {
            mapLayerCache = new MapLayer.MapLayerCache(view);
            this.Caches.Add((object) mapLayerCache);
          }
          else
            mapLayerCache.Reset();
          mapLayerCache.Rect = docExtent;
        }
        foreach (MapObject mapObject in arrayList)
        {
          if (mapObject != null)
          {
            RectangleF bounds = mapObject.Bounds;
            RectangleF a = mapObject.ExpandPaintBounds(bounds, view);
            if ((isPrinting ? (mapObject.CanPrint() ? 1 : 0) : (mapObject.CanView() ? 1 : 0)) != 0 && MapObject.IntersectsRect(a, clipRect))
              mapObject.Paint(g, view);
            if (flag && MapObject.IntersectsRect(a, docExtent))
              mapLayerCache.Objects.Add((object) mapObject);
          }
        }
      }

      public MapObject PickObject(PointF p, bool selectableOnly)
      {
        if (this.CanViewObjects())
        {
          if (selectableOnly && !this.CanSelectObjects())
            return (MapObject) null;
          MapLayer.MapLayerCache cache = this.FindCache(p);
          if (cache != null)
          {
            ArrayList objects = cache.Objects;
            for (int index = objects.Count - 1; index >= 0; --index)
            {
              MapObject mapObject1 = (MapObject) objects[index];
              if (mapObject1 != null)
              {
                MapObject mapObject2 = mapObject1.Pick(p, selectableOnly);
                if (mapObject2 != null)
                  return mapObject2;
              }
            }
          }
          else
          {
            foreach (MapObject backward in this.Backwards)
            {
              MapObject mapObject = backward.Pick(p, selectableOnly);
              if (mapObject != null)
                return mapObject;
            }
          }
        }
        return (MapObject) null;
      }

      public IMapCollection PickObjects(PointF p, bool selectableOnly, IMapCollection coll, int max)
      {
        if (coll == null)
          coll = (IMapCollection) new MapCollection();
        if (coll.Count < max && this.CanViewObjects() && (!selectableOnly || this.CanSelectObjects()))
        {
          MapLayer.MapLayerCache cache = this.FindCache(p);
          if (cache != null)
          {
            ArrayList objects = cache.Objects;
            for (int index = objects.Count - 1; index >= 0; --index)
            {
              MapObject mapObject1 = (MapObject) objects[index];
              if (mapObject1 is MapGroup mapGroup)
              {
                mapGroup.PickObjects(p, selectableOnly, coll, max);
              }
              else
              {
                MapObject mapObject2 = mapObject1.Pick(p, selectableOnly);
                if (mapObject2 != null)
                {
                  coll.Add(mapObject2);
                  if (coll.Count >= max)
                    return coll;
                }
              }
            }
            return coll;
          }
          foreach (MapObject backward in this.Backwards)
          {
            if (backward is MapGroup mapGroup)
            {
              mapGroup.PickObjects(p, selectableOnly, coll, max);
            }
            else
            {
              MapObject mapObject = backward.Pick(p, selectableOnly);
              if (mapObject != null)
              {
                coll.Add(mapObject);
                if (coll.Count >= max)
                  return coll;
              }
            }
          }
        }
        return coll;
      }

      public void Remove(MapObject obj)
      {
        if (obj == null)
          return;
        MapLayer layer = obj.Layer;
        if (layer == null)
          return;
        if (layer != this)
          throw new ArgumentException("Cannot remove an object from a layer if it does not belong to that layer.");
        MapGroup parent = obj.Parent;
        if (parent != null)
          parent.Remove(obj);
        else
          this.removeFromLayer(obj, false);
      }

      internal void RemoveFromCache(MapObject obj)
      {
        RectangleF bounds = obj.Bounds;
        foreach (MapLayer.MapLayerCache cach in this.Caches)
        {
          RectangleF b = obj.ExpandPaintBounds(bounds, cach.View);
          if (MapObject.IntersectsRect(cach.Rect, b))
          {
            MapCollection.fastRemove(cach.Objects, (object) obj);
            MapCollection.fastRemove(cach.Strokes, (object) obj);
          }
        }
      }

      internal void removeFromLayer(MapObject obj, bool undoing)
      {
        try
        {
          obj.SetBeingRemoved(true);
          this.RemoveFromCache(obj);
          int oldI = MapCollection.fastRemove(this.myObjects, (object) obj);
          RectangleF bounds = obj.Bounds;
          this.RaiseChanged(903, 0, (object) obj, oldI, (object) this, bounds, 0, (object) null, MapLayer.NullRect);
        }
        finally
        {
          obj.SetLayer((MapLayer) null, obj, undoing);
          obj.SetBeingRemoved(false);
        }
      }

      internal void ResetCache() => this.myCaches = new ArrayList();

      public void SetModifiable(bool b)
      {
        this.AllowMove = b;
        this.AllowResize = b;
        this.AllowReshape = b;
        this.AllowDelete = b;
        this.AllowInsert = b;
        this.AllowLink = b;
        this.AllowEdit = b;
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        return (IEnumerator) new MapLayerEnumerator(this.myObjects, true);
      }

      internal void UpdateCache(MapObject obj, MapChangedEventArgs evt)
      {
        foreach (MapLayer.MapLayerCache cach in this.Caches)
        {
          RectangleF oldRect = evt.OldRect;
          RectangleF b1 = obj.ExpandPaintBounds(oldRect, cach.View);
          RectangleF newRect = evt.NewRect;
          RectangleF b2 = obj.ExpandPaintBounds(newRect, cach.View);
          if ((MapObject.IntersectsRect(cach.Rect, b1) ? 1 : 0) == 0 & MapObject.IntersectsRect(cach.Rect, b2) && !cach.Objects.Contains((object) obj))
            cach.Objects.Add((object) obj);
        }
      }

      [DefaultValue(true)]
      [Description("Whether the user can copy the selected objects in this layer.")]
      [Category("Behavior")]
      public bool AllowCopy
      {
        get => this.myAllowCopy;
        set
        {
          bool allowCopy = this.myAllowCopy;
          if (allowCopy == value)
            return;
          this.myAllowCopy = value;
          this.RaiseChanged(913, 0, (object) this, 0, (object) allowCopy, MapLayer.NullRect, 0, (object) value, MapLayer.NullRect);
        }
      }

      [DefaultValue(true)]
      [Category("Behavior")]
      [Description("Whether the user can delete the selected objects in this layer.")]
      public bool AllowDelete
      {
        get => this.myAllowDelete;
        set
        {
          bool allowDelete = this.myAllowDelete;
          if (allowDelete == value)
            return;
          this.myAllowDelete = value;
          this.RaiseChanged(916, 0, (object) this, 0, (object) allowDelete, MapLayer.NullRect, 0, (object) value, MapLayer.NullRect);
        }
      }

      [Category("Behavior")]
      [Description("Whether the user can edit objects in this layer.")]
      [DefaultValue(true)]
      public bool AllowEdit
      {
        get => this.myAllowEdit;
        set
        {
          bool allowEdit = this.myAllowEdit;
          if (allowEdit == value)
            return;
          this.myAllowEdit = value;
          this.RaiseChanged(919, 0, (object) this, 0, (object) allowEdit, MapLayer.NullRect, 0, (object) value, MapLayer.NullRect);
        }
      }

      [Description("Whether the user can insert objects in this layer.")]
      [DefaultValue(true)]
      [Category("Behavior")]
      public bool AllowInsert
      {
        get => this.myAllowInsert;
        set
        {
          bool allowInsert = this.myAllowInsert;
          if (allowInsert == value)
            return;
          this.myAllowInsert = value;
          this.RaiseChanged(917, 0, (object) this, 0, (object) allowInsert, MapLayer.NullRect, 0, (object) value, MapLayer.NullRect);
        }
      }

      [Description("Whether the user can link ports in this layer.")]
      [Category("Behavior")]
      [DefaultValue(true)]
      public bool AllowLink
      {
        get => this.myAllowLink;
        set
        {
          bool allowLink = this.myAllowLink;
          if (allowLink == value)
            return;
          this.myAllowLink = value;
          this.RaiseChanged(918, 0, (object) this, 0, (object) allowLink, MapLayer.NullRect, 0, (object) value, MapLayer.NullRect);
        }
      }

      [Description("Whether the user can move the selected objects in this layer.")]
      [DefaultValue(true)]
      [Category("Behavior")]
      public bool AllowMove
      {
        get => this.myAllowMove;
        set
        {
          bool allowMove = this.myAllowMove;
          if (allowMove == value)
            return;
          this.myAllowMove = value;
          this.RaiseChanged(912, 0, (object) this, 0, (object) allowMove, MapLayer.NullRect, 0, (object) value, MapLayer.NullRect);
        }
      }

      [Description("Whether the view can print the objects in this layer.")]
      [Category("Behavior")]
      [DefaultValue(true)]
      public bool AllowPrint
      {
        get => this.myAllowPrint;
        set
        {
          bool allowPrint = this.myAllowPrint;
          if (allowPrint == value)
            return;
          this.myAllowPrint = value;
          this.RaiseChanged(920, 0, (object) this, 0, (object) allowPrint, MapLayer.NullRect, 0, (object) value, MapLayer.NullRect);
        }
      }

      [Description("Whether the user can reshape the resizable objects in this layer.")]
      [Category("Behavior")]
      [DefaultValue(true)]
      public bool AllowReshape
      {
        get => this.myAllowReshape;
        set
        {
          bool allowReshape = this.myAllowReshape;
          if (allowReshape == value)
            return;
          this.myAllowReshape = value;
          this.RaiseChanged(915, 0, (object) this, 0, (object) allowReshape, MapLayer.NullRect, 0, (object) value, MapLayer.NullRect);
        }
      }

      [DefaultValue(true)]
      [Description("Whether the user can resize the selected objects in this layer.")]
      [Category("Behavior")]
      public bool AllowResize
      {
        get => this.myAllowResize;
        set
        {
          bool allowResize = this.myAllowResize;
          if (allowResize == value)
            return;
          this.myAllowResize = value;
          this.RaiseChanged(914, 0, (object) this, 0, (object) allowResize, MapLayer.NullRect, 0, (object) value, MapLayer.NullRect);
        }
      }

      [DefaultValue(true)]
      [Category("Behavior")]
      [Description("Whether the user can select objects in this layer.")]
      public bool AllowSelect
      {
        get => this.myAllowSelect;
        set
        {
          bool allowSelect = this.myAllowSelect;
          if (allowSelect == value)
            return;
          this.myAllowSelect = value;
          this.RaiseChanged(911, 0, (object) this, 0, (object) allowSelect, MapLayer.NullRect, 0, (object) value, MapLayer.NullRect);
        }
      }

      [DefaultValue(true)]
      [Category("Behavior")]
      [Description("Whether the user can see the objects in this layer.")]
      public bool AllowView
      {
        get => this.myAllowView;
        set
        {
          bool allowView = this.myAllowView;
          if (allowView == value)
            return;
          this.myAllowView = value;
          this.RaiseChanged(910, 0, (object) this, 0, (object) allowView, MapLayer.NullRect, 0, (object) value, MapLayer.NullRect);
        }
      }

      [Browsable(false)]
      public MapLayerEnumerator Backwards => new MapLayerEnumerator(this.myObjects, false);

      internal ArrayList Caches
      {
        get
        {
          if (this.myCaches == null)
            this.myCaches = new ArrayList();
          return this.myCaches;
        }
      }

      [Description("The number of objects in this layer.")]
      public int Count => this.myObjects.Count;

      [Description("The document in which this layer belongs.")]
      [Category("Ownership")]
      public MapDocument Document
      {
        get => this.myIsInDocument ? (MapDocument) this.myLayerCollectionContainer : (MapDocument) null;
      }

      [Description("An identifier for this layer.")]
      [DefaultValue(null)]
      public object Identifier
      {
        get => this.myIdentifier;
        set
        {
          object identifier = this.myIdentifier;
          if (identifier == value)
            return;
          this.myIdentifier = value;
          this.RaiseChanged(930, 0, (object) this, 0, identifier, MapLayer.NullRect, 0, value, MapLayer.NullRect);
        }
      }

      [Browsable(false)]
      public bool IsEmpty => this.myObjects.Count == 0;

      [Browsable(false)]
      public bool IsInDocument => this.myIsInDocument;

      [Browsable(false)]
      public bool IsInView => !this.myIsInDocument;

      [Browsable(false)]
      public bool IsSynchronized => false;

      [Browsable(false)]
      public IMapLayerCollectionContainer LayerCollectionContainer => this.myLayerCollectionContainer;

      [Browsable(false)]
      public object SyncRoot => (object) this.LayerCollectionContainer;

      [Description("The view in which this layer belongs.")]
      [Category("Ownership")]
      public MapView View
      {
        get => this.myIsInDocument ? (MapView) null : (MapView) this.myLayerCollectionContainer;
      }

      internal sealed class MapLayerCache
      {
        private ArrayList myObjects;
        private RectangleF myRect;
        private ArrayList myStrokes;
        private MapView myView;

        internal MapLayerCache(MapView view)
        {
          this.myView = (MapView) null;
          this.myObjects = (ArrayList) null;
          this.myRect = new RectangleF(0.0f, 0.0f, 0.0f, 0.0f);
          this.myStrokes = (ArrayList) null;
          this.myView = view;
          this.myObjects = new ArrayList();
          this.myStrokes = new ArrayList();
        }

        internal void Reset()
        {
          this.myObjects.Clear();
          this.myStrokes.Clear();
          this.myRect = new RectangleF(0.0f, 0.0f, 0.0f, 0.0f);
        }

        internal ArrayList Objects => this.myObjects;

        internal RectangleF Rect
        {
          get => this.myRect;
          set => this.myRect = value;
        }

        internal ArrayList Strokes => this.myStrokes;

        internal MapView View => this.myView;
      }
    }
}
