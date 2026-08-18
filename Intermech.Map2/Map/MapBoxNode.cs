// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapBoxNode
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
    public class MapBoxNode : MapNode
    {
      public const int ChangedBody = 2201;
      public const int ChangedPortBorderMargin = 2202;
      private MapObject myBody;
      private MapPort myPort;
      private SizeF myPortBorderMargin;

      public MapBoxNode()
      {
        this.myBody = (MapObject) null;
        this.myPortBorderMargin = new SizeF(4f, 4f);
        this.myPort = (MapPort) null;
        this.InternalFlags |= 131072 /*0x020000*/;
        this.InternalFlags &= -17;
        this.myPort = this.CreatePort();
        this.Add((MapObject) this.myPort);
        this.myBody = this.CreateBody();
        this.Add(this.myBody);
        this.Initializing = false;
        this.LayoutChildren((MapObject) null);
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 2201:
            this.Body = (MapObject) e.GetValue(undo);
            break;
          case 2202:
            this.Initializing = true;
            this.PortBorderMargin = e.GetSize(undo);
            this.Initializing = false;
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      protected override void CopyChildren(MapGroup newgroup, MapCopyDictionary env)
      {
        base.CopyChildren(newgroup, env);
        MapBoxNode mapBoxNode = (MapBoxNode) newgroup;
        mapBoxNode.myPort = (MapPort) env[(object) this.myPort];
        mapBoxNode.myBody = (MapObject) env[(object) this.myBody];
      }

      protected virtual MapObject CreateBody()
      {
        MapText body = new MapText();
        body.TransparentBackground = false;
        body.BackgroundColor = Color.White;
        body.Multiline = true;
        body.Selectable = false;
        return (MapObject) body;
      }

      protected virtual MapPort CreatePort() => (MapPort) new MapBoxPort();

      public override void LayoutChildren(MapObject childchanged)
      {
        if (this.Initializing)
          return;
        MapObject body = this.Body;
        if (body == null)
          return;
        MapObject port = (MapObject) this.Port;
        if (port == null)
          return;
        RectangleF bounds = body.Bounds;
        SizeF portBorderMargin = this.PortBorderMargin;
        MapObject.InflateRect(ref bounds, portBorderMargin.Width, portBorderMargin.Height);
        port.Bounds = bounds;
      }

      public override void Remove(MapObject obj)
      {
        base.Remove(obj);
        if (obj == this.myBody)
        {
          this.myBody = (MapObject) null;
        }
        else
        {
          if (obj != this.myPort)
            return;
          this.myPort = (MapPort) null;
        }
      }

      public virtual MapObject Body
      {
        get => this.myBody;
        set
        {
          MapObject body = this.myBody;
          if (body == value)
            return;
          if (body != null)
            this.Remove(body);
          this.myBody = value;
          if (this.myBody != null)
          {
            if (body != null)
              this.myBody.Center = body.Center;
            this.Add(this.myBody);
          }
          this.Changed(2201, 0, (object) body, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [DefaultValue(false)]
      [Description("Whether the link points are distributed evenly along each side")]
      [Category("Appearance")]
      public virtual bool LinkPointsSpread
      {
        get => this.Port is MapBoxPort && ((MapBoxPort) this.Port).LinkPointsSpread;
        set
        {
          if (!(this.Port is MapBoxPort))
            return;
          ((MapBoxPort) this.Port).LinkPointsSpread = value;
        }
      }

      public MapPort Port => this.myPort;

      [Description("The margin that is always visible for the port on each side of the body")]
      [TypeConverter(typeof (MapSizeFConverter))]
      [Category("Appearance")]
      public virtual SizeF PortBorderMargin
      {
        get => this.myPortBorderMargin;
        set
        {
          SizeF portBorderMargin = this.myPortBorderMargin;
          if (!(portBorderMargin != value) || (double) value.Width < 0.0 || (double) value.Height < 0.0)
            return;
          this.myPortBorderMargin = value;
          this.Changed(2202, 0, (object) null, MapObject.MakeRect(portBorderMargin), 0, (object) null, MapObject.MakeRect(value));
          this.LayoutChildren((MapObject) null);
        }
      }
    }
}
