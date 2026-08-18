// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapSimpleNode
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
    public class MapSimpleNode : MapNode, IMapNodeIconConstraint
    {
      public const int ChangedIcon = 2602;
      public const int ChangedInPort = 2604;
      public const int ChangedLabel = 2603;
      public const int ChangedOrientation = 2606;
      public const int ChangedOutPort = 2605;
      public const int ChangedText = 2601;
      private MapObject myIcon;
      private MapPort myInPort;
      private MapText myLabel;
      private Orientation myOrientation;
      private MapPort myOutPort;
      private string myText;

      public MapSimpleNode()
      {
        this.myText = "";
        this.myIcon = (MapObject) null;
        this.myLabel = (MapText) null;
        this.myInPort = (MapPort) null;
        this.myOutPort = (MapPort) null;
        this.myOrientation = Orientation.Horizontal;
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 2601:
            this.Text = (string) e.GetValue(undo);
            break;
          case 2602:
            this.Icon = (MapObject) e.GetValue(undo);
            break;
          case 2603:
            this.Label = (MapText) e.GetValue(undo);
            break;
          case 2604:
            this.InPort = (MapPort) e.GetValue(undo);
            break;
          case 2605:
            this.OutPort = (MapPort) e.GetValue(undo);
            break;
          case 2606:
            this.setOrientation((Orientation) e.GetInt(undo), true);
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      protected override void CopyChildren(MapGroup newgroup, MapCopyDictionary env)
      {
        base.CopyChildren(newgroup, env);
        MapSimpleNode mapSimpleNode = (MapSimpleNode) newgroup;
        mapSimpleNode.myIcon = (MapObject) env[(object) this.myIcon];
        mapSimpleNode.myLabel = (MapText) env[(object) this.myLabel];
        mapSimpleNode.myInPort = (MapPort) env[(object) this.myInPort];
        mapSimpleNode.myOutPort = (MapPort) env[(object) this.myOutPort];
      }

      protected virtual MapObject CreateIcon(ResourceManager res, string iconname)
      {
        if (iconname != null)
        {
          MapNodeIcon icon = new MapNodeIcon();
          if (res != null)
            icon.ResourceManager = res;
          icon.Name = iconname;
          icon.MinimumIconSize = new SizeF(20f, 20f);
          icon.MaximumIconSize = new SizeF(100f, 200f);
          icon.Size = icon.MinimumIconSize;
          return (MapObject) icon;
        }
        MapRectangle icon1 = new MapRectangle();
        icon1.Selectable = false;
        icon1.Size = new SizeF(20f, 20f);
        return (MapObject) icon1;
      }

      protected virtual MapObject CreateIcon(ImageList imglist, int imgindex)
      {
        MapNodeIcon icon = new MapNodeIcon();
        icon.ImageList = imglist;
        icon.Index = imgindex;
        icon.MinimumIconSize = new SizeF(20f, 20f);
        icon.MaximumIconSize = new SizeF(100f, 200f);
        icon.Size = icon.MinimumIconSize;
        return (MapObject) icon;
      }

      protected virtual MapText CreateLabel(string name)
      {
        MapText label = (MapText) null;
        if (name != null)
        {
          label = new MapText();
          label.Text = name;
          label.Selectable = false;
          label.Alignment = 1;
        }
        return label;
      }

      protected virtual MapPort CreatePort(bool input)
      {
        MapPort port = new MapPort();
        port.Size = new SizeF(6f, 6f);
        port.IsValidFrom = !input;
        port.IsValidTo = input;
        return port;
      }

      public virtual void Initialize(ResourceManager res, string iconname, string name)
      {
        this.Initializing = true;
        this.myIcon = this.CreateIcon(res, iconname);
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
        this.myText = name;
        this.myLabel = this.CreateLabel(name);
        this.Add((MapObject) this.myLabel);
        if (this.myLabel != null)
          this.myLabel.AddObserver((MapObject) this);
        this.myInPort = this.CreatePort(true);
        this.Add((MapObject) this.myInPort);
        this.myOutPort = this.CreatePort(false);
        this.Add((MapObject) this.myOutPort);
        this.PropertiesDelegatedToSelectionObject = true;
        this.Initializing = false;
        this.LayoutChildren((MapObject) null);
      }

      public override void LayoutChildren(MapObject childchanged)
      {
        if (this.Initializing)
          return;
        MapObject icon = this.Icon;
        if (icon == null)
          return;
        if (this.Orientation == Orientation.Horizontal)
        {
          if (this.Label != null)
            this.Label.SetSpotLocation(32 /*0x20*/, icon, 128 /*0x80*/);
          if (this.InPort != null)
            this.InPort.SetSpotLocation(64 /*0x40*/, icon, 256 /*0x0100*/);
          if (this.OutPort == null)
            return;
          this.OutPort.SetSpotLocation(256 /*0x0100*/, icon, 64 /*0x40*/);
        }
        else
        {
          if (this.Label != null)
            this.Label.SetSpotLocation(256 /*0x0100*/, icon, 64 /*0x40*/);
          if (this.InPort != null)
            this.InPort.SetSpotLocation(128 /*0x80*/, icon, 32 /*0x20*/);
          if (this.OutPort == null)
            return;
          this.OutPort.SetSpotLocation(32 /*0x20*/, icon, 128 /*0x80*/);
        }
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
        if (subhint != 1501 || observed != this.Label)
          return;
        this.Text = (string) newVal;
      }

      public virtual void OnOrientationChanged(Orientation old)
      {
        if (this.Orientation == Orientation.Vertical)
        {
          if (this.InPort != null)
          {
            this.InPort.ToSpot = 32 /*0x20*/;
            this.InPort.FromSpot = 32 /*0x20*/;
          }
          if (this.OutPort != null)
          {
            this.OutPort.ToSpot = 128 /*0x80*/;
            this.OutPort.FromSpot = 128 /*0x80*/;
          }
        }
        else
        {
          if (this.InPort != null)
          {
            this.InPort.ToSpot = 256 /*0x0100*/;
            this.InPort.FromSpot = 256 /*0x0100*/;
          }
          if (this.OutPort != null)
          {
            this.OutPort.ToSpot = 64 /*0x40*/;
            this.OutPort.FromSpot = 64 /*0x40*/;
          }
        }
        this.LayoutChildren((MapObject) null);
      }

      public override void Remove(MapObject obj)
      {
        base.Remove(obj);
        if (obj == this.myIcon)
          this.myIcon = (MapObject) null;
        else if (obj == this.myLabel)
        {
          this.myLabel.RemoveObserver((MapObject) this);
          this.myLabel = (MapText) null;
        }
        else if (obj == this.myInPort)
        {
          this.myInPort = (MapPort) null;
        }
        else
        {
          if (obj != this.myOutPort)
            return;
          this.myOutPort = (MapPort) null;
        }
      }

      private void setOrientation(Orientation o, bool undoing)
      {
        Orientation orientation = this.myOrientation;
        if (orientation == o)
          return;
        this.myOrientation = o;
        this.Changed(2606, (int) orientation, (object) null, MapObject.NullRect, (int) o, (object) null, MapObject.NullRect);
        if (undoing)
          return;
        this.OnOrientationChanged(orientation);
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
          this.Changed(2602, 0, (object) icon, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      public virtual MapImage Image => this.myIcon as MapImage;

      public MapPort InPort
      {
        get => this.myInPort;
        set
        {
          MapPort inPort = this.myInPort;
          if (inPort == value)
            return;
          if (inPort != null)
            this.Remove((MapObject) inPort);
          this.myInPort = value;
          if (value != null)
            this.Add((MapObject) value);
          this.Changed(2604, 0, (object) inPort, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      public override MapText Label
      {
        get => this.myLabel;
        set
        {
          MapText label = this.myLabel;
          if (label == value)
            return;
          if (label != null)
          {
            this.Remove((MapObject) label);
            label.RemoveObserver((MapObject) this);
          }
          this.myLabel = value;
          if (value != null)
          {
            this.Add((MapObject) value);
            value.AddObserver((MapObject) this);
          }
          this.Changed(2603, 0, (object) label, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [TypeConverter(typeof (MapSizeFConverter))]
      [Description("The maximum size for the icon")]
      [Category("Appearance")]
      public virtual SizeF MaximumIconSize
      {
        get => this.Icon is MapNodeIcon icon ? icon.MaximumIconSize : new SizeF(100f, 200f);
        set
        {
          if (!(this.Icon is MapNodeIcon icon))
            return;
          icon.MaximumIconSize = value;
        }
      }

      [TypeConverter(typeof (MapSizeFConverter))]
      [Description("The minimum size for the icon")]
      [Category("Appearance")]
      public virtual SizeF MinimumIconSize
      {
        get
        {
          if (this.Orientation == Orientation.Horizontal)
          {
            float width = 20f;
            float num = 20f;
            if (this.Icon is MapNodeIcon icon)
            {
              width = icon.MinimumIconSize.Width;
              num = icon.MinimumIconSize.Height;
            }
            if (this.InPort != null)
              num = Math.Max(num, this.InPort.Height);
            if (this.OutPort != null)
              num = Math.Max(num, this.OutPort.Height);
            return new SizeF(width, num);
          }
          float num1 = 20f;
          float height = 20f;
          if (this.Icon is MapNodeIcon icon1)
          {
            num1 = icon1.MinimumIconSize.Width;
            height = icon1.MinimumIconSize.Height;
          }
          if (this.InPort != null)
            num1 = Math.Max(num1, this.InPort.Width);
          if (this.OutPort != null)
            num1 = Math.Max(num1, this.OutPort.Width);
          return new SizeF(num1, height);
        }
        set
        {
          if (!(this.Icon is MapNodeIcon icon))
            return;
          icon.MinimumIconSize = value;
        }
      }

      [DefaultValue(0)]
      [Category("Appearance")]
      [Description("The general orientation of the node and how links connect to it")]
      public Orientation Orientation
      {
        get => this.myOrientation;
        set => this.setOrientation(value, false);
      }

      public MapPort OutPort
      {
        get => this.myOutPort;
        set
        {
          MapPort outPort = this.myOutPort;
          if (outPort == value)
            return;
          if (outPort != null)
            this.Remove((MapObject) outPort);
          this.myOutPort = value;
          if (value != null)
            this.Add((MapObject) value);
          this.Changed(2605, 0, (object) outPort, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      public override MapObject SelectionObject => this.Icon != null ? this.Icon : (MapObject) this;

      public override string Text
      {
        get => this.myText;
        set
        {
          string text = this.myText;
          if (!(text != value))
            return;
          this.myText = value;
          this.Changed(2601, 0, (object) text, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
          if (this.Label == null)
            return;
          this.Label.Text = value;
        }
      }
    }
}
