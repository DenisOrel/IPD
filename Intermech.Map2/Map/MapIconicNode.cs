// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapIconicNode
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;


namespace Intermech.Map
{
    [Serializable]
    public class MapIconicNode : MapNode
    {
      protected bool _doMapIconicLayoutChildren = true;
      public const int ChangedDraggableLabel = 2651;
      public const int ChangedIcon = 2652;
      public const int ChangedLabel = 2653;
      public const int ChangedLabelOffset = 2655;
      public const int ChangedPort = 2654;
      private bool myDraggableLabel;
      protected MapObject myIcon;
      private MapText myLabel;
      protected SizeF myLabelOffset;
      private MapPort myPort;

      public MapIconicNode()
      {
        this.myIcon = (MapObject) null;
        this.myLabel = (MapText) null;
        this.myPort = (MapPort) null;
        this.myDraggableLabel = false;
        this.myLabelOffset = new SizeF(-99999f, -99999f);
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 2651:
            this.setDraggableLabel((bool) e.GetValue(undo), true);
            break;
          case 2652:
            this.Icon = (MapObject) e.GetValue(undo);
            break;
          case 2653:
            this.Label = (MapText) e.GetValue(undo);
            break;
          case 2654:
            this.Port = (MapPort) e.GetValue(undo);
            break;
          case 2655:
            this.setLabelOffset(e.GetSize(undo), true);
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      protected override void CopyChildren(MapGroup newgroup, MapCopyDictionary env)
      {
        base.CopyChildren(newgroup, env);
        MapIconicNode mapIconicNode = (MapIconicNode) newgroup;
        mapIconicNode.myIcon = (MapObject) env[(object) this.myIcon];
        mapIconicNode.myLabel = (MapText) env[(object) this.myLabel];
        mapIconicNode.myPort = (MapPort) env[(object) this.myPort];
      }

      protected virtual MapObject CreateIcon(ResourceManager res, string iconname)
      {
        MapImage icon = new MapImage();
        if (res != null)
          icon.ResourceManager = res;
        icon.Name = iconname;
        icon.Selectable = false;
        icon.Resizable = false;
        return (MapObject) icon;
      }

      protected virtual MapObject CreateIcon(ImageList imglist, int imgindex)
      {
        MapImage icon = new MapImage();
        icon.ImageList = imglist;
        icon.Index = imgindex;
        icon.Selectable = false;
        icon.Resizable = false;
        return (MapObject) icon;
      }

      protected virtual MapText CreateLabel(string name)
      {
        MapText label = (MapText) null;
        if (name != null)
        {
          label = new MapText();
          label.Text = name;
          label.Selectable = this.DraggableLabel;
          label.Alignment = 32 /*0x20*/;
        }
        return label;
      }

      protected virtual MapPort CreatePort()
      {
        MapPort port = new MapPort();
        port.Style = MapPortStyle.None;
        port.Size = new SizeF(6f, 6f);
        port.FromSpot = 0;
        port.ToSpot = 0;
        port.PortObject = (MapObject) this;
        return port;
      }

      public virtual void Initialize(ResourceManager res, string iconname, string name)
      {
        this.Initializing = true;
        this.myIcon = this.CreateIcon(res, iconname);
        this.Add(this.myIcon);
        this.initializeCommon(name);
      }

      public virtual void Initialize(string name)
      {
        this.Initializing = true;
        this.myIcon = (MapObject) new MapImage();
        this.Add(this.myIcon);
        this.initializeCommon(name);
      }

      public virtual void Initialize(ImageList imglist, int imgindex, string name)
      {
        this.Initializing = true;
        this.myIcon = this.CreateIcon(imglist, imgindex);
        this.Add(this.myIcon);
        this.initializeCommon(name);
      }

      private void initializeCommon(string name)
      {
        this.myLabel = this.CreateLabel(name);
        this.Add((MapObject) this.myLabel);
        this.myPort = this.CreatePort();
        this.Add((MapObject) this.myPort);
        this.PropertiesDelegatedToSelectionObject = true;
        this.Initializing = false;
        this.LayoutChildren((MapObject) null);
      }

      public override void LayoutChildren(MapObject childchanged)
      {
        if (this.Initializing || !this._doMapIconicLayoutChildren)
          return;
        MapObject icon = this.Icon;
        if (icon == null)
          return;
        MapText label = this.Label;
        if (label != null)
        {
          if (this.DraggableLabel && childchanged == label)
          {
            this.myLabelOffset = new SizeF(label.Left - icon.Left, label.Top - icon.Top);
            return;
          }
          if ((double) this.myLabelOffset.Width > -99999.0)
            label.Position = new PointF(icon.Left + this.myLabelOffset.Width, icon.Top + this.myLabelOffset.Height);
          else
            label.SetSpotLocation(32 /*0x20*/, icon, 128 /*0x80*/);
        }
        if (this.Port == null)
          return;
        this.Port.SetSpotLocation(1, icon, 1);
      }

      public override void Remove(MapObject obj)
      {
        base.Remove(obj);
        if (obj == this.myIcon)
          this.myIcon = (MapObject) null;
        else if (obj == this.myLabel)
        {
          this.myLabel = (MapText) null;
        }
        else
        {
          if (obj != this.myPort)
            return;
          this.myPort = (MapPort) null;
        }
      }

      private void setDraggableLabel(bool d, bool undoing)
      {
        bool draggableLabel = this.myDraggableLabel;
        if (draggableLabel == d)
          return;
        this.myDraggableLabel = d;
        this.Changed(2651, 0, (object) draggableLabel, MapObject.NullRect, 0, (object) d, MapObject.NullRect);
        if (undoing || this.Label == null)
          return;
        this.Label.Selectable = d;
      }

      private void setLabelOffset(SizeF v, bool undoing)
      {
        SizeF labelOffset = this.myLabelOffset;
        if (!(labelOffset != v))
          return;
        this.myLabelOffset = v;
        this.Changed(2655, 0, (object) null, MapObject.MakeRect(labelOffset), 0, (object) null, MapObject.MakeRect(v));
        if (undoing)
          return;
        this.LayoutChildren((MapObject) null);
      }

      [Description("Whether users can drag the label independently of the node")]
      [Category("Behavior")]
      [DefaultValue(false)]
      public virtual bool DraggableLabel
      {
        get => this.myDraggableLabel;
        set => this.setDraggableLabel(value, false);
      }

      public virtual MapObject Icon
      {
        get => this.myIcon;
        set
        {
          MapObject icon = this.myIcon;
          if (icon == value)
            return;
          this.CopyPropertiesFromSelectionObject(icon, value);
          if (icon != null)
            this.Remove(icon);
          this.myIcon = value;
          if (value != null)
            this.InsertBefore((MapObject) null, value);
          this.Changed(2652, 0, (object) icon, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
          if (this.Port == null || this.Port.PortObject != icon)
            return;
          this.Port.PortObject = value;
        }
      }

      public virtual MapImage Image => this.myIcon as MapImage;

      public override MapText Label
      {
        get => this.myLabel;
        set
        {
          MapText label = this.myLabel;
          if (label == value)
            return;
          if (label != null)
            this.Remove((MapObject) label);
          this.myLabel = value;
          if (value != null)
            this.Add((MapObject) value);
          this.Changed(2653, 0, (object) label, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Category("Appearance")]
      [Description("The offset of the Label relative to the Icon")]
      [TypeConverter(typeof (MapSizeFConverter))]
      public SizeF LabelOffset
      {
        get => this.myLabelOffset;
        set => this.setLabelOffset(value, false);
      }

      public MapPort Port
      {
        get => this.myPort;
        set
        {
          MapPort port = this.myPort;
          if (port == value)
            return;
          if (port != null)
            this.Remove((MapObject) port);
          this.myPort = value;
          if (value != null)
            this.Add((MapObject) value);
          this.Changed(2654, 0, (object) port, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
          if (value == null || value.PortObject != null)
            return;
          value.PortObject = this.Icon;
        }
      }

      public override MapObject SelectionObject => this.Icon != null ? this.Icon : (MapObject) this;
    }
}
