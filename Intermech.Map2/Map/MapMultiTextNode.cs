// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapMultiTextNode
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;


namespace Intermech.Map
{
    [Serializable]
    public class MapMultiTextNode : MapNode
    {
      public const int InsertedLeftPort = 3001;
      public const int InsertedRightPort = 3002;
      public const int RemovedLeftPort = 3003;
      public const int RemovedRightPort = 3004;
      public const int ReplacedPort = 3005;
      public const int ChangedTopPort = 3006;
      public const int ChangedBottomPort = 3007;
      public const int ChangedItemWidth = 3008;
      private float _itemWidth;
      private MapObject _topPort;
      private MapObject _bottomPort;
      private List<MapObject> _leftPorts;
      private List<MapObject> _rightPorts;
      private MapListGroup _listGroup;

      public MapMultiTextNode()
      {
        this._listGroup = (MapListGroup) null;
        this._topPort = (MapObject) null;
        this._bottomPort = (MapObject) null;
        this._leftPorts = new List<MapObject>();
        this._rightPorts = new List<MapObject>();
        this._itemWidth = -1f;
        this.Initializing = true;
        this._listGroup = (MapListGroup) new MapMultiTextNodeListGroup();
        this._listGroup.Selectable = false;
        this._listGroup.LinePen = MapShape.Pens_Black;
        this._listGroup.BorderPen = MapShape.Pens_Black;
        this._listGroup.Alignment = 1;
        this.Add((MapObject) this._listGroup);
        this._topPort = this.CreateEndPort(true);
        this.Add(this._topPort);
        this._bottomPort = this.CreateEndPort(false);
        this.Add(this._bottomPort);
        this.InternalFlags &= -17;
        this.Initializing = false;
        this.LayoutChildren((MapObject) null);
      }

      public void AddItem(MapObject item, MapObject leftport, MapObject rightport)
      {
        this.InsertItem(this.ItemCount, item, leftport, rightport);
      }

      public virtual MapText AddString(string s)
      {
        int itemCount = this.ItemCount;
        MapText text = this.CreateText(s, itemCount);
        this.AddItem((MapObject) text, this.CreatePort(true, itemCount), this.CreatePort(false, itemCount));
        return text;
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 3001:
            int newInt1 = e.NewInt;
            MapObject mapObject1 = (MapObject) e.GetValue(undo);
            if (undo)
              break;
            this._leftPorts.Insert(newInt1, mapObject1);
            this.Add(mapObject1);
            break;
          case 3002:
            int newInt2 = e.NewInt;
            MapObject mapObject2 = (MapObject) e.GetValue(undo);
            if (undo)
              break;
            this._rightPorts.Insert(newInt2, mapObject2);
            this.Add(mapObject2);
            break;
          case 3003:
            int oldInt1 = e.OldInt;
            MapObject mapObject3 = (MapObject) e.GetValue(undo);
            if (!undo)
              break;
            this._leftPorts.Insert(oldInt1, mapObject3);
            this.Add(mapObject3);
            break;
          case 3004:
            int oldInt2 = e.OldInt;
            MapObject mapObject4 = (MapObject) e.GetValue(undo);
            if (!undo)
              break;
            this._rightPorts.Insert(oldInt2, mapObject4);
            this.Add(mapObject4);
            break;
          case 3005:
            int oldInt3 = e.OldInt;
            if (oldInt3 >= 0)
            {
              this.SetRightPort(oldInt3, (MapObject) e.GetValue(undo));
              break;
            }
            this.SetLeftPort(-oldInt3 - 1, (MapObject) e.GetValue(undo));
            break;
          case 3006:
            this.TopPort = (MapObject) e.GetValue(undo);
            break;
          case 3007:
            this.BottomPort = (MapObject) e.GetValue(undo);
            break;
          case 3008:
            this.setItemWidth(e.GetFloat(undo), true);
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      protected override void CopyChildren(MapGroup newgroup, MapCopyDictionary env)
      {
        MapMultiTextNode mapMultiTextNode = (MapMultiTextNode) newgroup;
        base.CopyChildren(newgroup, env);
        mapMultiTextNode._leftPorts = new List<MapObject>();
        mapMultiTextNode._rightPorts = new List<MapObject>();
        mapMultiTextNode._listGroup = (MapListGroup) env[(object) this._listGroup];
        mapMultiTextNode._topPort = (MapObject) env[(object) this._topPort];
        mapMultiTextNode._bottomPort = (MapObject) env[(object) this._bottomPort];
        for (int index = 0; index < this._leftPorts.Count; ++index)
        {
          MapObject leftPort = this._leftPorts[index];
          MapObject mapObject = (MapObject) env[(object) leftPort];
          mapMultiTextNode._leftPorts.Add(mapObject);
        }
        for (int index = 0; index < this._rightPorts.Count; ++index)
        {
          MapObject rightPort = this._rightPorts[index];
          MapObject mapObject = (MapObject) env[(object) rightPort];
          mapMultiTextNode._rightPorts.Add(mapObject);
        }
      }

      public virtual MapObject CreateEndPort(bool top)
      {
        MapPort endPort = new MapPort();
        endPort.Size = new SizeF(5f, 3f);
        endPort.Style = MapPortStyle.None;
        if (top)
        {
          endPort.FromSpot = 32 /*0x20*/;
          endPort.ToSpot = 32 /*0x20*/;
          return (MapObject) endPort;
        }
        endPort.FromSpot = 128 /*0x80*/;
        endPort.ToSpot = 128 /*0x80*/;
        return (MapObject) endPort;
      }

      public virtual MapObject CreatePort(bool left, int idx)
      {
        MapPort port = new MapPort();
        port.Size = new SizeF(3f, 5f);
        port.Style = MapPortStyle.None;
        if (left)
        {
          port.FromSpot = 256 /*0x0100*/;
          port.ToSpot = 256 /*0x0100*/;
          return (MapObject) port;
        }
        port.FromSpot = 64 /*0x40*/;
        port.ToSpot = 64 /*0x40*/;
        return (MapObject) port;
      }

      public virtual MapText CreateText(string s, int idx)
      {
        MapText text = new MapText();
        text.Selectable = false;
        text.Alignment = 1;
        text.Multiline = true;
        text.BackgroundOpaqueWhenSelected = true;
        text.BackgroundColor = Color.LightBlue;
        text.DragsNode = true;
        text.Text = s;
        text.Wrapping = true;
        if ((double) this.ItemWidth > 0.0)
        {
          text.WrappingWidth = this.ItemWidth;
          text.Width = this.ItemWidth;
        }
        return text;
      }

      public MapObject GetItem(int i) => this._listGroup[i];

      public virtual MapObject GetLeftPort(int i)
      {
        return i >= 0 && i < this._leftPorts.Count ? this._leftPorts[i] : (MapObject) null;
      }

      public virtual MapObject GetRightPort(int i)
      {
        return i >= 0 && i < this._rightPorts.Count ? this._rightPorts[i] : (MapObject) null;
      }

      public virtual string GetString(int i)
      {
        return this._listGroup[i] is MapText mapText ? mapText.Text : "";
      }

      public virtual void InsertItem(int i, MapObject item, MapObject leftport, MapObject rightport)
      {
        if (i < 0 || i > this._listGroup.Count)
          return;
        this._listGroup.Insert(i, item);
        if (i >= 0 && i <= this._leftPorts.Count)
        {
          this._leftPorts.Insert(i, leftport);
          this.Add(leftport);
          this.Changed(3001, i, (object) null, MapObject.NullRect, i, (object) leftport, MapObject.NullRect);
        }
        if (i < 0 || i > this._rightPorts.Count)
          return;
        this._rightPorts.Insert(i, rightport);
        this.Add(rightport);
        this.Changed(3002, i, (object) null, MapObject.NullRect, i, (object) rightport, MapObject.NullRect);
      }

      public virtual MapText InsertString(int i, string s)
      {
        if (i < 0 || i >= this.ItemCount)
          return (MapText) null;
        MapText text = this.CreateText(s, i);
        this.InsertItem(i, (MapObject) text, this.CreatePort(true, i), this.CreatePort(false, i));
        return text;
      }

      public override void LayoutChildren(MapObject childchanged)
      {
        if (this.Initializing || this._listGroup == null)
          return;
        this.Initializing = true;
        if (this._topPort != null)
          this._topPort.SetSpotLocation(128 /*0x80*/, (MapObject) this._listGroup, 32 /*0x20*/);
        if (this._bottomPort != null)
          this._bottomPort.SetSpotLocation(32 /*0x20*/, (MapObject) this._listGroup, 128 /*0x80*/);
        int index = 0;
        foreach (MapObject mapObject in (MapGroup) this._listGroup)
        {
          if (mapObject != null && index < this._leftPorts.Count)
          {
            MapObject leftPort = this._leftPorts[index];
            if (leftPort != null)
            {
              PointF spotLocation = mapObject.GetSpotLocation(256 /*0x0100*/) with
              {
                X = this._listGroup.Left
              };
              leftPort.SetSpotLocation(64 /*0x40*/, spotLocation);
            }
          }
          if (mapObject != null && index < this._rightPorts.Count)
          {
            MapObject rightPort = this._rightPorts[index];
            if (rightPort != null)
            {
              PointF spotLocation = mapObject.GetSpotLocation(64 /*0x40*/) with
              {
                X = this._listGroup.Right
              };
              rightPort.SetSpotLocation(256 /*0x0100*/, spotLocation);
            }
          }
          ++index;
        }
        this.Initializing = false;
      }

      public virtual void OnItemWidthChanged(float old)
      {
        float itemWidth = this.ItemWidth;
        foreach (MapObject mapObject in (MapGroup) this.ListGroup)
        {
          if (mapObject is MapText mapText && (double) itemWidth > 0.0)
            mapText.WrappingWidth = itemWidth;
          if (mapObject != null && (double) itemWidth > 0.0)
            mapObject.Width = itemWidth;
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
          if (!selectableOnly)
            return (MapObject) this;
          if (this.CanSelect())
            return (MapObject) this;
        }
        return (MapObject) null;
      }

      public override void Remove(MapObject obj)
      {
        int index1 = this._leftPorts.IndexOf(obj);
        if (index1 >= 0)
        {
          this._leftPorts[index1] = (MapObject) null;
        }
        else
        {
          int index2 = this._rightPorts.IndexOf(obj);
          if (index2 >= 0)
            this._rightPorts[index2] = (MapObject) null;
        }
        base.Remove(obj);
      }

      public virtual void RemoveItem(int i) => this._listGroup.RemoveAt(i);

      internal void RemoveOnlyPorts(int i)
      {
        if (i >= 0 && i < this._leftPorts.Count)
        {
          MapObject leftPort = this._leftPorts[i];
          this._leftPorts.RemoveAt(i);
          if (leftPort != null)
            base.Remove(leftPort);
          this.Changed(3003, i, (object) leftPort, MapObject.NullRect, i, (object) null, MapObject.NullRect);
        }
        if (i < 0 || i >= this._rightPorts.Count)
          return;
        MapObject rightPort = this._rightPorts[i];
        this._rightPorts.RemoveAt(i);
        if (rightPort != null)
          base.Remove(rightPort);
        this.Changed(3004, i, (object) rightPort, MapObject.NullRect, i, (object) null, MapObject.NullRect);
      }

      public void SetItem(int i, MapObject obj) => this._listGroup[i] = obj;

      private void setItemWidth(float w, bool undoing)
      {
        float itemWidth = this._itemWidth;
        if ((double) itemWidth == (double) w)
          return;
        this._itemWidth = w;
        this.Changed(3008, 0, (object) null, MapObject.MakeRect(itemWidth), 0, (object) null, MapObject.MakeRect(w));
        if (undoing)
          return;
        this.OnItemWidthChanged(itemWidth);
      }

      public virtual void SetLeftPort(int i, MapObject p)
      {
        MapObject leftPort = this.GetLeftPort(i);
        if (leftPort == p)
          return;
        if (leftPort != null)
        {
          if (p != null)
            p.Bounds = leftPort.Bounds;
          base.Remove(leftPort);
        }
        this._leftPorts[i] = p;
        this.Add(p);
        this.Changed(3005, -(i + 1), (object) leftPort, MapObject.NullRect, -(i + 1), (object) p, MapObject.NullRect);
      }

      public virtual void SetRightPort(int i, MapObject p)
      {
        MapObject rightPort = this.GetRightPort(i);
        if (rightPort == p)
          return;
        if (rightPort != null)
        {
          if (p != null)
            p.Bounds = rightPort.Bounds;
          base.Remove(rightPort);
        }
        this._rightPorts[i] = p;
        this.Add(p);
        this.Changed(3005, i, (object) rightPort, MapObject.NullRect, i, (object) p, MapObject.NullRect);
      }

      public virtual void SetString(int i, string s)
      {
        if (!(this._listGroup[i] is MapText mapText))
          return;
        mapText.Text = s;
      }

      [Description("How each item is positioned along the X axis.")]
      [Category("Appearance")]
      [DefaultValue(256 /*0x0100*/)]
      public int Alignment
      {
        get => this.ListGroup.Alignment;
        set => this.ListGroup.Alignment = value;
      }

      [Description("The pen used to draw an outline for this node.")]
      [Category("Appearance")]
      public Pen BorderPen
      {
        get => this.ListGroup.BorderPen;
        set => this.ListGroup.BorderPen = value;
      }

      public virtual MapObject BottomPort
      {
        get => this._bottomPort;
        set
        {
          MapObject bottomPort = this._bottomPort;
          if (bottomPort == value)
            return;
          if (bottomPort != null)
            base.Remove(bottomPort);
          this._bottomPort = value;
          if (value != null)
            this.Add(value);
          this.Changed(3007, 0, (object) bottomPort, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [TypeConverter(typeof (MapSizeFConverter))]
      [Description("The margin around the text inside the background at the right side and the bottom")]
      [Category("Appearance")]
      public SizeF BottomRightMargin
      {
        get => this.ListGroup.BottomRightMargin;
        set => this.ListGroup.BottomRightMargin = value;
      }

      [Description("The brush used to fill the outline of this shape.")]
      [DefaultValue(null)]
      [Category("Appearance")]
      public Brush Brush
      {
        get => this.ListGroup.Brush;
        set => this.ListGroup.Brush = value;
      }

      [Category("Appearance")]
      [TypeConverter(typeof (MapSizeFConverter))]
      [Description("The maximum radial width and height of each corner")]
      public SizeF Corner
      {
        get => this.ListGroup.Corner;
        set => this.ListGroup.Corner = value;
      }

      public int ItemCount => this._listGroup.Count;

      [Description("The width for all items, and the wrapping width for all text items")]
      [DefaultValue(-1)]
      [Category("Appearance")]
      public virtual float ItemWidth
      {
        get => this._itemWidth;
        set => this.setItemWidth(value, false);
      }

      [Description("The pen used to draw lines separating the items.")]
      [Category("Appearance")]
      public Pen LinePen
      {
        get => this.ListGroup.LinePen;
        set => this.ListGroup.LinePen = value;
      }

      public MapListGroup ListGroup => this._listGroup;

      public override bool Shadowed
      {
        get => this.ListGroup.Shadowed;
        set => this.ListGroup.Shadowed = value;
      }

      [Description("The additional vertical distance between items.")]
      [Category("Appearance")]
      [DefaultValue(0)]
      public float Spacing
      {
        get => this.ListGroup.Spacing;
        set => this.ListGroup.Spacing = value;
      }

      [Description("The margin around the text inside the background at the left side and the top")]
      [Category("Appearance")]
      [TypeConverter(typeof (MapSizeFConverter))]
      public SizeF TopLeftMargin
      {
        get => this.ListGroup.TopLeftMargin;
        set => this.ListGroup.TopLeftMargin = value;
      }

      public virtual MapObject TopPort
      {
        get => this._topPort;
        set
        {
          MapObject topPort = this._topPort;
          if (topPort == value)
            return;
          if (topPort != null)
            base.Remove(topPort);
          this._topPort = value;
          if (value != null)
            this.Add(value);
          this.Changed(3006, 0, (object) topPort, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }
    }
}
