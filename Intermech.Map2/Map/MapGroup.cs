// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapGroup
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;


namespace Intermech.Map
{
    [Serializable]
    public class MapGroup : MapObject, IMapCollection, ICollection, IEnumerable, IList
    {
      public const int InsertedObject = 1051;
      public const int RemovedObject = 1052;
      public const int ChangedZOrder = 1053;
      public const int ReplacedObject = 1054;
      private const int flagInvalidPaintBounds = 1048576 /*0x100000*/;
      [NonSerialized]
      private float myBottom;
      [NonSerialized]
      private float myLeft;
      private ArrayList _objects;
      [NonSerialized]
      private SizeF myPaintBoundsShadowOffset;
      [NonSerialized]
      private float myRight;
      [NonSerialized]
      private float myTop;

      public MapGroup()
      {
        this._objects = new ArrayList();
        this.myPaintBoundsShadowOffset = new SizeF();
        this.myLeft = 0.0f;
        this.myTop = 0.0f;
        this.myRight = 0.0f;
        this.myBottom = 0.0f;
      }

      public virtual void Add(MapObject obj)
      {
        if (obj == null)
          return;
        MapGroup parent = obj.Parent;
        if (parent == null)
        {
          if (obj.Layer != null)
            throw new ArgumentException("Cannot add an object to a group when it is already part of a document or view.");
          this.insertAt(this._objects.Count, obj, false);
        }
        else if (parent != this)
          throw new ArgumentException("Cannot move an object from one group to another without first removing it from its parent.");
      }

      public int Add(object obj)
      {
        this.Add((MapObject) obj);
        return this.Count - 1;
      }

      public virtual IMapCollection AddCollection(IMapCollection coll, bool reparentLinks)
      {
        foreach (MapObject mapObject in (IEnumerable) coll)
        {
          if (this.IsChildOf(mapObject) || this == mapObject)
            throw new ArgumentException("Cannot add a group to itself or to one of its own children.");
        }
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

      private void CalculatePaintBounds(MapView view)
      {
        this.InternalFlags &= -1048577;
        RectangleF bounds = this.Bounds;
        float val1_1 = bounds.X;
        float val1_2 = bounds.Y;
        float val1_3 = val1_1 + bounds.Width;
        float val1_4 = val1_2 + bounds.Height;
        foreach (MapObject mapObject in this.GetEnumerator())
        {
          RectangleF rect = mapObject.Bounds;
          rect = mapObject.ExpandPaintBounds(rect, view);
          val1_1 = Math.Min(val1_1, rect.X);
          val1_2 = Math.Min(val1_2, rect.Y);
          val1_3 = Math.Max(val1_3, rect.X + rect.Width);
          val1_4 = Math.Max(val1_4, rect.Y + rect.Height);
        }
        if (view != null)
          this.myPaintBoundsShadowOffset = this.GetShadowOffset(view);
        this.myLeft = bounds.X - val1_1;
        this.myTop = bounds.Y - val1_2;
        this.myRight = val1_3 - (bounds.X + bounds.Width);
        this.myBottom = val1_4 - (bounds.Y + bounds.Height);
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 1051:
            int num1 = e.NewInt;
            MapObject newValue1 = e.NewValue as MapObject;
            if (!undo)
            {
              if (num1 < 0)
                num1 = this._objects.Count;
              if (this._objects.IndexOf((object) newValue1) >= 0)
                break;
              this.insertAt(num1, newValue1, true);
              break;
            }
            if (num1 < 0)
              num1 = this._objects.IndexOf((object) newValue1);
            if (num1 < 0)
              break;
            this.removeAt(num1, newValue1, true);
            break;
          case 1052:
            int num2 = e.OldInt;
            MapObject oldValue1 = e.OldValue as MapObject;
            if (!undo)
            {
              if (num2 < 0)
                num2 = this._objects.IndexOf((object) oldValue1);
              if (num2 < 0)
                break;
              this.removeAt(num2, oldValue1, true);
              break;
            }
            if (num2 < 0)
              num2 = this._objects.Count;
            if (this._objects.IndexOf((object) oldValue1) >= 0)
              break;
            this.insertAt(num2, oldValue1, true);
            break;
          case 1053:
            MapObject oldValue2 = (MapObject) e.OldValue;
            int oldInt1 = e.OldInt;
            int newInt = e.NewInt;
            this._objects.Remove((object) oldValue2);
            if (!undo)
            {
              this.moveTo(newInt, oldValue2, oldInt1);
              break;
            }
            this.moveTo(oldInt1, oldValue2, newInt);
            break;
          case 1054:
            MapObject oldValue3 = (MapObject) e.OldValue;
            MapObject newValue2 = (MapObject) e.NewValue;
            int oldInt2 = e.OldInt;
            if (!undo)
            {
              this.replaceAt(oldInt2, newValue2, true);
              break;
            }
            this.replaceAt(oldInt2, oldValue3, true);
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      public virtual void Clear()
      {
        int val1;
        for (int index = this._objects.Count; index > 0; index = Math.Min(val1, this._objects.Count))
          this.Remove((MapObject) this._objects[val1 = index - 1]);
      }

      protected override RectangleF ComputeBounds()
      {
        RectangleF a = this.Bounds;
        bool flag = false;
        foreach (MapObject mapObject in this.GetEnumerator())
        {
          if (!flag)
          {
            a = mapObject.Bounds;
            flag = true;
          }
          else
            a = MapObject.UnionRect(a, mapObject.Bounds);
        }
        return a;
      }

      public virtual bool Contains(MapObject obj) => obj != null && obj.Parent == this;

      public bool Contains(object obj) => this.Contains((MapObject) obj);

      public override bool ContainsPoint(PointF p)
      {
        if (MapObject.ContainsRect(this.Bounds, p))
        {
          foreach (MapObject mapObject in this.GetEnumerator())
          {
            if (mapObject.CanView() && mapObject.ContainsPoint(p))
              return true;
          }
        }
        return false;
      }

      public virtual MapObject[] CopyArray()
      {
        MapObject[] array = new MapObject[this.Count];
        this.CopyTo(array, 0);
        return array;
      }

      protected virtual void CopyChildren(MapGroup newgroup, MapCopyDictionary env)
      {
        foreach (MapObject mapObject1 in this.GetEnumerator())
        {
          MapObject mapObject2 = env.Copy(mapObject1);
          newgroup.Add(mapObject2);
        }
      }

      public override MapObject CopyObject(MapCopyDictionary env)
      {
        MapGroup newgroup = (MapGroup) base.CopyObject(env);
        if (newgroup != null)
        {
          newgroup._objects = new ArrayList();
          bool initializing = newgroup.Initializing;
          newgroup.Initializing = true;
          this.CopyChildren(newgroup, env);
          newgroup.Initializing = initializing;
        }
        return (MapObject) newgroup;
      }

      public void CopyTo(MapObject[] array, int index) => this.CopyTo((Array) array, index);

      public virtual void CopyTo(Array array, int index) => this._objects.CopyTo(array, index);

      public override RectangleF ExpandPaintBounds(RectangleF rect, MapView view)
      {
        if ((this.InternalFlags & 1048576 /*0x100000*/) != 0 || view == null || this.myPaintBoundsShadowOffset != this.GetShadowOffset(view))
          this.CalculatePaintBounds(view);
        return new RectangleF(rect.X - this.myLeft, rect.Y - this.myTop, rect.Width + this.myLeft + this.myRight, rect.Height + this.myTop + this.myBottom);
      }

      public MapGroupEnumerator GetEnumerator() => new MapGroupEnumerator(this._objects, true);

      public override bool GetNearestIntersectionPoint(PointF p1, PointF p2, out PointF result)
      {
        float num1 = 1E+21f;
        PointF pointF = new PointF();
        foreach (MapObject mapObject in this.GetEnumerator())
        {
          PointF result1;
          if (mapObject.CanView() && mapObject.GetNearestIntersectionPoint(p1, p2, out result1))
          {
            float num2 = (float) (((double) result1.X - (double) p1.X) * ((double) result1.X - (double) p1.X) + ((double) result1.Y - (double) p1.Y) * ((double) result1.Y - (double) p1.Y));
            if ((double) num2 < (double) num1)
            {
              num1 = num2;
              pointF = result1;
            }
          }
        }
        result = pointF;
        return (double) num1 < 1.0000000200408773E+21;
      }

      public virtual int IndexOf(MapObject obj) => this._objects.IndexOf((object) obj);

      public int IndexOf(object obj) => this.IndexOf((MapObject) obj);

      public virtual void Insert(int index, MapObject obj)
      {
        if (index == this.Count)
          this.Add(obj);
        else
          this.InsertBefore((MapObject) this._objects[index], obj);
      }

      public void Insert(int index, object obj) => this.Insert(index, (MapObject) obj);

      public virtual void InsertAfter(MapObject child, MapObject newobj)
      {
        if (newobj == null)
          return;
        MapGroup mapGroup = child == null || child.Parent == this ? newobj.Parent : throw new ArgumentException("Cannot insert an object into a group after a child that is not a member of the group.");
        if (mapGroup == null)
        {
          if (newobj.Layer != null)
            throw new ArgumentException("Cannot add an object to a group when it is already part of a document or view.");
          this.insertAt((child == null ? this._objects.Count - 1 : this._objects.IndexOf((object) child)) + 1, newobj, false);
        }
        else
        {
          if (mapGroup != this)
            throw new ArgumentException("Cannot move an object from one group to another without first removing it from its parent.");
          int num1 = newobj != child ? this._objects.IndexOf((object) newobj) : throw new ArgumentException("Cannot insert an object into a group after itself.");
          int num2 = child == null ? this._objects.Count - 1 : this._objects.IndexOf((object) child);
          if (num2 > num1)
            --num2;
          if (num2 == num1 || num2 + 1 == num1)
            return;
          this._objects.RemoveAt(num1);
          this.moveTo(num2 + 1, newobj, num1);
        }
      }

      private void insertAt(int idx, MapObject obj, bool undoing)
      {
        RectangleF bounds1 = obj.Bounds;
        if (!undoing || this._objects.IndexOf((object) obj) < 0)
        {
          if (idx < 0 || idx > this._objects.Count)
            idx = this._objects.Count;
          this._objects.Insert(idx, (object) obj);
        }
        obj.SetParent(this, undoing);
        this.Changed(1051, 0, (object) null, MapObject.NullRect, idx, (object) obj, bounds1);
        if (undoing)
          return;
        this.LayoutChildren(obj);
        this.InvalidBounds = true;
        RectangleF bounds2 = this.Bounds;
      }

      public virtual void InsertBefore(MapObject child, MapObject newobj)
      {
        if (newobj == null)
          return;
        MapGroup mapGroup = child == null || child.Parent == this ? newobj.Parent : throw new ArgumentException("Cannot insert an object into a group before (behind) a child that is not a member of the group.");
        if (mapGroup == null)
        {
          if (newobj.Layer != null)
            throw new ArgumentException("Cannot add an object to a group when it is already part of a document or view.");
          this.insertAt(child == null ? 0 : this._objects.IndexOf((object) child), newobj, false);
        }
        else
        {
          if (mapGroup != this)
            throw new ArgumentException("Cannot move an object from one group to another without first removing it from its parent.");
          int num = newobj != child ? this._objects.IndexOf((object) newobj) : throw new ArgumentException("Cannot insert an object into a group before (behind) itself.");
          int newidx = child == null ? 0 : this._objects.IndexOf((object) child);
          if (newidx > num)
            --newidx;
          if (newidx == num)
            return;
          this._objects.RemoveAt(num);
          this.moveTo(newidx, newobj, num);
        }
      }

      internal void InvalidatePaintBounds()
      {
        this.InternalFlags |= 1048576 /*0x100000*/;
        if (this.Parent == null)
          return;
        this.Parent.InvalidatePaintBounds();
      }

      public virtual void LayoutChildren(MapObject childchanged)
      {
      }

      protected virtual void MoveChildren(RectangleF old)
      {
        float num1 = this.Left - old.X;
        float num2 = this.Top - old.Y;
        foreach (MapObject mapObject in this.GetEnumerator())
        {
          RectangleF bounds = mapObject.Bounds;
          mapObject.Bounds = new RectangleF(bounds.X + num1, bounds.Y + num2, bounds.Width, bounds.Height);
        }
      }

      private void moveTo(int newidx, MapObject obj, int oldidx)
      {
        RectangleF bounds = obj.Bounds;
        this._objects.Insert(newidx, (object) obj);
        this.Changed(1053, oldidx, (object) obj, bounds, newidx, (object) obj, bounds);
      }

      protected override void OnBoundsChanged(RectangleF old)
      {
        base.OnBoundsChanged(old);
        SizeF size = this.Size;
        if ((double) old.Width == (double) size.Width && (double) old.Height == (double) size.Height)
        {
          this.MoveChildren(old);
        }
        else
        {
          this.RescaleChildren(old);
          this.LayoutChildren((MapObject) null);
          this.InvalidBounds = true;
        }
      }

      protected internal virtual void OnChildBoundsChanged(MapObject child, RectangleF old)
      {
        this.LayoutChildren(child);
        this.InvalidBounds = true;
      }

      public override void Paint(Graphics g, MapView view)
      {
        bool isPrinting = view.IsPrinting;
        RectangleF clipBounds = g.ClipBounds;
        bool flag1 = MapObject.ContainsRect(clipBounds, this.Bounds);
        foreach (MapObject mapObject in this.GetEnumerator())
        {
          if ((isPrinting ? (mapObject.CanPrint() ? 1 : 0) : (mapObject.CanView() ? 1 : 0)) != 0)
          {
            bool flag2 = flag1;
            if (!flag2)
            {
              RectangleF bounds = mapObject.Bounds;
              flag2 = MapObject.IntersectsRect(mapObject.ExpandPaintBounds(bounds, view), clipBounds);
            }
            if (flag2)
              mapObject.Paint(g, view);
          }
        }
      }

      public override MapObject Pick(PointF p, bool selectableOnly)
      {
        if (this.CanView())
        {
          if (!MapObject.ContainsRect(this.Bounds, p))
            return (MapObject) null;
          foreach (MapObject backward in this.Backwards)
          {
            MapObject mapObject = backward.Pick(p, selectableOnly);
            if (mapObject != null)
              return mapObject;
          }
        }
        return (MapObject) null;
      }

      public virtual IMapCollection PickObjects(
        PointF p,
        bool selectableOnly,
        IMapCollection coll,
        int max)
      {
        if (coll == null)
          coll = (IMapCollection) new MapCollection();
        if (coll.Count < max && this.CanView())
        {
          MapObject mapObject = this.Pick(p, selectableOnly);
          if (mapObject != null)
            coll.Add(mapObject);
        }
        return coll;
      }

      public virtual void Remove(MapObject obj)
      {
        if (obj == null)
          return;
        MapGroup parent = obj.Parent;
        if (parent == null)
          return;
        if (parent != this)
          throw new ArgumentException("Cannot remove an object from a group if it doesn't belong to that group.");
        int index = this._objects.IndexOf((object) obj);
        if (index < 0)
          return;
        this.removeAt(index, obj, false);
      }

      public void Remove(object obj) => this.Remove((MapObject) obj);

      private void removeAt(int index, MapObject obj, bool undoing)
      {
        try
        {
          obj.SetBeingRemoved(true);
          if (undoing)
          {
            int num = this._objects.IndexOf((object) obj);
            if (num >= 0)
            {
              if (index < 0 || index >= this._objects.Count)
                index = num;
              this._objects.RemoveAt(index);
            }
          }
          else
            this._objects.RemoveAt(index);
          RectangleF bounds1 = obj.Bounds;
          this.Changed(1052, index, (object) obj, bounds1, 0, (object) null, MapObject.NullRect);
          if (undoing)
            return;
          this.LayoutChildren(obj);
          this.InvalidBounds = true;
          RectangleF bounds2 = this.Bounds;
        }
        catch (Exception ex)
        {
          MapObject.Trace("MapGroup.Remove: " + ex.ToString());
          throw ex;
        }
        finally
        {
          obj.SetParent((MapGroup) null, undoing);
          obj.SetBeingRemoved(false);
        }
      }

      public void RemoveAt(int index) => this.Remove((MapObject) this._objects[index]);

      private void replaceAt(int index, MapObject newobj, bool undoing)
      {
        MapObject oldVal = (MapObject) this._objects[index];
        oldVal.SetBeingRemoved(true);
        oldVal.SetParent((MapGroup) null, undoing);
        oldVal.SetBeingRemoved(false);
        this._objects[index] = (object) newobj;
        RectangleF bounds1 = newobj.Bounds;
        newobj.SetParent(this, undoing);
        this.Changed(1054, index, (object) oldVal, MapObject.NullRect, index, (object) newobj, MapObject.NullRect);
        if (undoing)
          return;
        this.LayoutChildren(newobj);
        this.InvalidBounds = true;
        RectangleF bounds2 = this.Bounds;
      }

      protected virtual void RescaleChildren(RectangleF old)
      {
        if ((double) old.Width <= 0.0 || (double) old.Height <= 0.0)
          return;
        RectangleF bounds1 = this.Bounds;
        float num1 = bounds1.Width / old.Width;
        float num2 = bounds1.Height / old.Height;
        foreach (MapObject mapObject in this.GetEnumerator())
        {
          if (mapObject.AutoRescales)
          {
            RectangleF bounds2 = mapObject.Bounds;
            float x = bounds1.X + (bounds2.X - old.X) * num1;
            float y = bounds1.Y + (bounds2.Y - old.Y) * num2;
            float width = bounds2.Width * num1;
            float height = bounds2.Height * num2;
            mapObject.Bounds = new RectangleF(x, y, width, height);
          }
        }
      }

      internal static void setAllNoClear(MapObject obj, bool b)
      {
        switch (obj)
        {
          case MapPort mapPort:
            mapPort.NoClearLinks = b;
            break;
          case MapLink mapLink:
            mapLink.NoClearPorts = b;
            break;
          case MapGroup mapGroup:
            foreach (MapObject mapObject in mapGroup)
              MapGroup.setAllNoClear(mapObject, b);
            break;
        }
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        return (IEnumerator) new MapGroupEnumerator(this._objects, true);
      }

      object IList.this[int index]
      {
        get => (object) this[index];
        set => this[index] = (MapObject) value;
      }

      [Browsable(false)]
      public IEnumerable Backwards => (IEnumerable) new MapGroupEnumerator(this._objects, false);

      [Description("The number of child objects in this group.")]
      public virtual int Count => this._objects.Count;

      [Description("The first child object of this group.")]
      public MapObject First
      {
        get => this._objects.Count == 0 ? (MapObject) null : (MapObject) this._objects[0];
      }

      [Browsable(false)]
      public virtual bool IsEmpty => this.Count == 0;

      [Browsable(false)]
      public virtual bool IsFixedSize => false;

      [Browsable(false)]
      public virtual bool IsReadOnly => false;

      [Browsable(false)]
      public virtual bool IsSynchronized => false;

      public virtual MapObject this[int index]
      {
        get => (MapObject) this._objects[index];
        set
        {
          if (this._objects[index] == value || value == null)
            return;
          MapObject newobj = value;
          if (newobj.Parent == this)
            return;
          this.replaceAt(index, newobj, false);
        }
      }

      [Description("The last child object of this group.")]
      public MapObject Last
      {
        get
        {
          int count = this._objects.Count;
          return count == 0 ? (MapObject) null : (MapObject) this._objects[count - 1];
        }
      }

      [Browsable(false)]
      public virtual object SyncRoot => (object) this.Document;
    }
}
