// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapTextNode
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
    public class MapTextNode : MapNode
    {
      public const int ChangedAutoResizes = 2809;
      public const int ChangedBackground = 2802;
      public const int ChangedBottomPort = 2805;
      public const int ChangedBottomRightMargin = 2808;
      public const int ChangedLabel = 2801;
      public const int ChangedLeftPort = 2806;
      public const int ChangedRightPort = 2804;
      public const int ChangedTopLeftMargin = 2807;
      public const int ChangedTopPort = 2803;
      private const int flagAutoResizes = 16777216 /*0x01000000*/;
      private MapObject myBack;
      private MapPort myBottomPort;
      private SizeF myBottomRightMargin;
      private MapText myLabel;
      private MapPort myLeftPort;
      private MapPort myRightPort;
      private SizeF myTopLeftMargin;
      private MapPort myTopPort;

      public MapTextNode()
      {
        this.myLabel = (MapText) null;
        this.myBack = (MapObject) null;
        this.myTopPort = (MapPort) null;
        this.myRightPort = (MapPort) null;
        this.myBottomPort = (MapPort) null;
        this.myLeftPort = (MapPort) null;
        this.myTopLeftMargin = new SizeF(4f, 2f);
        this.myBottomRightMargin = new SizeF(4f, 2f);
        this.InternalFlags &= -17;
        this.InternalFlags |= 16908288 /*0x01020000*/;
        this.myBack = this.CreateBackground();
        this.Add(this.myBack);
        this.myLabel = this.CreateLabel();
        this.Add((MapObject) this.myLabel);
        this.myTopPort = this.CreatePort(32 /*0x20*/);
        this.Add((MapObject) this.myTopPort);
        this.myRightPort = this.CreatePort(64 /*0x40*/);
        this.Add((MapObject) this.myRightPort);
        this.myBottomPort = this.CreatePort(128 /*0x80*/);
        this.Add((MapObject) this.myBottomPort);
        this.myLeftPort = this.CreatePort(256 /*0x0100*/);
        this.Add((MapObject) this.myLeftPort);
        this.Initializing = false;
        this.LayoutChildren((MapObject) null);
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 2801:
            this.Label = (MapText) e.GetValue(undo);
            break;
          case 2802:
            this.Background = (MapObject) e.GetValue(undo);
            break;
          case 2803:
            this.TopPort = (MapPort) e.GetValue(undo);
            break;
          case 2804:
            this.RightPort = (MapPort) e.GetValue(undo);
            break;
          case 2805:
            this.BottomPort = (MapPort) e.GetValue(undo);
            break;
          case 2806:
            this.LeftPort = (MapPort) e.GetValue(undo);
            break;
          case 2807:
            this.Initializing = true;
            this.TopLeftMargin = e.GetSize(undo);
            this.Initializing = false;
            break;
          case 2808:
            this.Initializing = true;
            this.BottomRightMargin = e.GetSize(undo);
            this.Initializing = false;
            break;
          case 2809:
            this.setAutoResizes((bool) e.GetValue(undo), true);
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      protected override void CopyChildren(MapGroup newgroup, MapCopyDictionary env)
      {
        base.CopyChildren(newgroup, env);
        MapTextNode mapTextNode = (MapTextNode) newgroup;
        mapTextNode.myBack = (MapObject) env[(object) this.myBack];
        mapTextNode.myLabel = (MapText) env[(object) this.myLabel];
        mapTextNode.myTopPort = (MapPort) env[(object) this.myTopPort];
        mapTextNode.myRightPort = (MapPort) env[(object) this.myRightPort];
        mapTextNode.myBottomPort = (MapPort) env[(object) this.myBottomPort];
        mapTextNode.myLeftPort = (MapPort) env[(object) this.myLeftPort];
      }

      protected virtual MapObject CreateBackground()
      {
        MapRectangle background = new MapRectangle();
        background.Selectable = false;
        background.Resizable = false;
        background.Reshapable = false;
        background.Brush = MapShape.Brushes_LightGray;
        return (MapObject) background;
      }

      protected virtual MapText CreateLabel()
      {
        MapText label = new MapText();
        label.Selectable = false;
        label.Multiline = true;
        return label;
      }

      protected virtual MapPort CreatePort(int spot)
      {
        MapPort port = new MapPort();
        port.Style = MapPortStyle.None;
        port.Size = new SizeF(4f, 4f);
        port.FromSpot = spot;
        port.ToSpot = spot;
        return port;
      }

      public override void LayoutChildren(MapObject childchanged)
      {
        if (this.Initializing)
          return;
        MapText label = this.Label;
        if (label == null)
          return;
        MapObject mapObject = this.Background;
        if (mapObject != null)
        {
          SizeF topLeftMargin = this.TopLeftMargin;
          SizeF bottomRightMargin = this.BottomRightMargin;
          if (this.AutoResizes)
          {
            mapObject.Bounds = new RectangleF(label.Left - topLeftMargin.Width, label.Top - topLeftMargin.Height, label.Width + topLeftMargin.Width + bottomRightMargin.Width, label.Height + topLeftMargin.Height + bottomRightMargin.Height);
          }
          else
          {
            float width = Math.Max(mapObject.Width - (topLeftMargin.Width + bottomRightMargin.Width), 0.0f);
            float val2 = Math.Max(mapObject.Height - (topLeftMargin.Height + bottomRightMargin.Height), 0.0f);
            label.Width = width;
            label.WrappingWidth = width;
            label.UpdateSize();
            float height = Math.Min(label.Height, val2);
            float x = mapObject.Left + topLeftMargin.Width;
            float y = (float) ((double) mapObject.Top + (double) topLeftMargin.Height + ((double) val2 - (double) height) / 2.0);
            label.Bounds = new RectangleF(x, y, width, height);
          }
        }
        if (mapObject == null && this.AutoResizes)
          mapObject = (MapObject) label;
        if (mapObject == null)
          return;
        if (this.TopPort != null)
          this.TopPort.SetSpotLocation(32 /*0x20*/, mapObject, 32 /*0x20*/);
        if (this.RightPort != null)
          this.RightPort.SetSpotLocation(64 /*0x40*/, mapObject, 64 /*0x40*/);
        if (this.BottomPort != null)
          this.BottomPort.SetSpotLocation(128 /*0x80*/, mapObject, 128 /*0x80*/);
        if (this.LeftPort == null)
          return;
        this.LeftPort.SetSpotLocation(256 /*0x0100*/, mapObject, 256 /*0x0100*/);
      }

      public virtual void OnAutoResizesChanged(bool old)
      {
        MapText label = this.Label;
        if (label == null)
          return;
        label.Wrapping = old;
        label.Clipping = old;
        this.PropertiesDelegatedToSelectionObject = old;
      }

      public override void Remove(MapObject obj)
      {
        base.Remove(obj);
        if (obj == this.myBack)
          this.myBack = (MapObject) null;
        else if (obj == this.myLabel)
          this.myLabel = (MapText) null;
        else if (obj == this.myTopPort)
          this.myTopPort = (MapPort) null;
        else if (obj == this.myRightPort)
          this.myRightPort = (MapPort) null;
        else if (obj == this.myBottomPort)
        {
          this.myBottomPort = (MapPort) null;
        }
        else
        {
          if (obj != this.myLeftPort)
            return;
          this.myLeftPort = (MapPort) null;
        }
      }

      private void setAutoResizes(bool b, bool undoing)
      {
        bool flag = (this.InternalFlags & 16777216 /*0x01000000*/) != 0;
        if (flag == b)
          return;
        if (b)
          this.InternalFlags |= 16777216 /*0x01000000*/;
        else
          this.InternalFlags &= -16777217;
        this.Changed(2809, 0, (object) flag, MapObject.NullRect, 0, (object) b, MapObject.NullRect);
        if (undoing)
          return;
        this.OnAutoResizesChanged(flag);
      }

      [Description("Whether the background changes size as the text changes")]
      [Category("Behavior")]
      [DefaultValue(true)]
      public virtual bool AutoResizes
      {
        get => (this.InternalFlags & 16777216 /*0x01000000*/) != 0;
        set => this.setAutoResizes(value, false);
      }

      public virtual MapObject Background
      {
        get => this.myBack;
        set
        {
          MapObject back = this.myBack;
          if (back == value)
            return;
          if (back != null)
          {
            if (value != null)
            {
              value.Selectable = back.Selectable;
              value.Shadowed = back.Shadowed;
            }
            this.Remove(back);
          }
          this.myBack = value;
          if (value != null)
            this.InsertBefore((MapObject) null, value);
          this.Changed(2802, 0, (object) back, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      public virtual MapPort BottomPort
      {
        get => this.myBottomPort;
        set
        {
          MapPort bottomPort = this.myBottomPort;
          if (bottomPort == value)
            return;
          if (bottomPort != null)
            this.Remove((MapObject) bottomPort);
          this.myBottomPort = value;
          if (value != null)
            this.Add((MapObject) value);
          this.Changed(2805, 0, (object) bottomPort, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [TypeConverter(typeof (MapSizeFConverter))]
      [Description("The margin around the text inside the background at the right side and the bottom")]
      [Category("Appearance")]
      public virtual SizeF BottomRightMargin
      {
        get => this.myBottomRightMargin;
        set
        {
          SizeF bottomRightMargin = this.myBottomRightMargin;
          if (!(bottomRightMargin != value))
            return;
          this.myBottomRightMargin = value;
          this.Changed(2808, 0, (object) null, MapObject.MakeRect(bottomRightMargin), 0, (object) null, MapObject.MakeRect(value));
          this.LayoutChildren((MapObject) null);
        }
      }

      [Description("The Brush used by the background")]
      [Category("Appearance")]
      public Brush Brush
      {
        get
        {
          return this.Background != null && this.Background is MapShape ? ((MapShape) this.Background).Brush : (Brush) null;
        }
        set
        {
          if (this.Background == null || !(this.Background is MapShape))
            return;
          ((MapShape) this.Background).Brush = value;
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
            this.Remove((MapObject) label);
          this.myLabel = value;
          if (value != null)
            this.Add((MapObject) value);
          this.Changed(2801, 0, (object) label, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      public virtual MapPort LeftPort
      {
        get => this.myLeftPort;
        set
        {
          MapPort leftPort = this.myLeftPort;
          if (leftPort == value)
            return;
          if (leftPort != null)
            this.Remove((MapObject) leftPort);
          this.myLeftPort = value;
          if (value != null)
            this.Add((MapObject) value);
          this.Changed(2806, 0, (object) leftPort, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Category("Appearance")]
      [Description("The Pen used by the background")]
      public Pen Pen
      {
        get
        {
          return this.Background != null && this.Background is MapShape ? ((MapShape) this.Background).Pen : (Pen) null;
        }
        set
        {
          if (this.Background == null || !(this.Background is MapShape))
            return;
          ((MapShape) this.Background).Pen = value;
        }
      }

      public virtual MapPort RightPort
      {
        get => this.myRightPort;
        set
        {
          MapPort rightPort = this.myRightPort;
          if (rightPort == value)
            return;
          if (rightPort != null)
            this.Remove((MapObject) rightPort);
          this.myRightPort = value;
          if (value != null)
            this.Add((MapObject) value);
          this.Changed(2804, 0, (object) rightPort, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      public override MapObject SelectionObject
      {
        get => this.Background != null && !this.AutoResizes ? this.Background : (MapObject) this;
      }

      public override bool Shadowed
      {
        get => this.Background != null ? this.Background.Shadowed : base.Shadowed;
        set
        {
          if (this.Background != null)
            this.Background.Shadowed = value;
          else
            base.Shadowed = value;
        }
      }

      [Category("Appearance")]
      [TypeConverter(typeof (MapSizeFConverter))]
      [Description("The margin around the text inside the background at the left side and the top")]
      public virtual SizeF TopLeftMargin
      {
        get => this.myTopLeftMargin;
        set
        {
          SizeF topLeftMargin = this.myTopLeftMargin;
          if (!(topLeftMargin != value))
            return;
          this.myTopLeftMargin = value;
          this.Changed(2807, 0, (object) null, MapObject.MakeRect(topLeftMargin), 0, (object) null, MapObject.MakeRect(value));
          this.LayoutChildren((MapObject) null);
        }
      }

      public virtual MapPort TopPort
      {
        get => this.myTopPort;
        set
        {
          MapPort topPort = this.myTopPort;
          if (topPort == value)
            return;
          if (topPort != null)
            this.Remove((MapObject) topPort);
          this.myTopPort = value;
          if (value != null)
            this.Add((MapObject) value);
          this.Changed(2803, 0, (object) topPort, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }
    }
}
