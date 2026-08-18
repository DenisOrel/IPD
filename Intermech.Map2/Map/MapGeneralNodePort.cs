// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapGeneralNodePort
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Map
{
    [Serializable]
    public class MapGeneralNodePort : MapPort
    {
      public const int ChangedLabel = 2431;
      public const int ChangedLeftSide = 2433;
      public const int ChangedName = 2430;
      public const int ChangedSideIndex = 2432;
      private bool myLeftSide;
      private string myName;
      private MapGeneralNodePortLabel myPortLabel;
      private int mySideIndex;

      public MapGeneralNodePort()
      {
        this.myLeftSide = true;
        this.mySideIndex = -1;
        this.myName = "";
        this.myPortLabel = (MapGeneralNodePortLabel) null;
        this.Style = MapPortStyle.Triangle;
        this.Pen = MapShape.Pens_Gray;
        this.Brush = MapShape.Brushes_LightGray;
        this.Size = new SizeF(8f, 8f);
        this.LeftSide = true;
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 2430:
            this.Name = (string) e.GetValue(undo);
            break;
          case 2431:
            this.Label = (MapGeneralNodePortLabel) e.GetValue(undo);
            break;
          case 2432:
            this.SideIndex = e.GetInt(undo);
            break;
          case 2433:
            this.LeftSide = (bool) e.GetValue(undo);
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      public override MapObject CopyObject(MapCopyDictionary env)
      {
        MapGeneralNodePort mapGeneralNodePort = (MapGeneralNodePort) base.CopyObject(env);
        if (mapGeneralNodePort != null && this.myPortLabel != null)
        {
          mapGeneralNodePort.myPortLabel = (MapGeneralNodePortLabel) env.Copy((MapObject) this.myPortLabel);
          if (mapGeneralNodePort.myPortLabel != null)
            mapGeneralNodePort.myPortLabel.Port = mapGeneralNodePort;
        }
        return (MapObject) mapGeneralNodePort;
      }

      public override PointF GetFromLinkPoint(IMapLink link) => this.GetLinkPoint(this.FromSpot);

      public virtual PointF GetLinkPoint(int spot)
      {
        switch (spot)
        {
          case 32 /*0x20*/:
            RectangleF bounds1 = this.Bounds;
            PointF linkPoint1 = new PointF(bounds1.X + bounds1.Width / 2f, bounds1.Y);
            MapGeneralNodePortLabel label1 = this.Label;
            if (label1 != null && label1.Visible)
              linkPoint1.Y -= label1.Height + this.LabelSpacing;
            return linkPoint1;
          case 64 /*0x40*/:
            RectangleF bounds2 = this.Bounds;
            PointF linkPoint2 = new PointF(bounds2.X + bounds2.Width, bounds2.Y + bounds2.Height / 2f);
            MapGeneralNodePortLabel label2 = this.Label;
            if (label2 != null && label2.Visible)
              linkPoint2.X += label2.Width + this.LabelSpacing;
            return linkPoint2;
          case 128 /*0x80*/:
            RectangleF bounds3 = this.Bounds;
            PointF linkPoint3 = new PointF(bounds3.X + bounds3.Width / 2f, bounds3.Y + bounds3.Height);
            MapGeneralNodePortLabel label3 = this.Label;
            if (label3 != null && label3.Visible)
              linkPoint3.Y += label3.Height + this.LabelSpacing;
            return linkPoint3;
          case 256 /*0x0100*/:
            RectangleF bounds4 = this.Bounds;
            PointF linkPoint4 = new PointF(bounds4.X, bounds4.Y + bounds4.Height / 2f);
            MapGeneralNodePortLabel label4 = this.Label;
            if (label4 != null && label4.Visible)
              linkPoint4.X -= label4.Width + this.LabelSpacing;
            return linkPoint4;
          default:
            return this.GetSpotLocation(spot);
        }
      }

      public override PointF GetToLinkPoint(IMapLink link) => this.GetLinkPoint(this.ToSpot);

      public override string GetToolTip(MapView view) => this.Name;

      public virtual void LayoutLabel()
      {
        MapText label = (MapText) this.Label;
        if (label == null)
          return;
        if (this.Parent is MapGeneralNode parent && parent.Orientation == Orientation.Vertical)
        {
          if (this.LeftSide)
          {
            label.Alignment = 1;
            PointF spotLocation = this.GetSpotLocation(32 /*0x20*/);
            spotLocation.Y -= this.LabelSpacing;
            label.SetSpotLocation(128 /*0x80*/, spotLocation);
          }
          else
          {
            label.Alignment = 1;
            PointF spotLocation = this.GetSpotLocation(128 /*0x80*/);
            spotLocation.Y += this.LabelSpacing;
            label.SetSpotLocation(32 /*0x20*/, spotLocation);
          }
        }
        else if (this.LeftSide)
        {
          label.Alignment = 64 /*0x40*/;
          PointF spotLocation = this.GetSpotLocation(256 /*0x0100*/);
          spotLocation.X -= this.LabelSpacing;
          label.SetSpotLocation(64 /*0x40*/, spotLocation);
        }
        else
        {
          label.Alignment = 256 /*0x0100*/;
          PointF spotLocation = this.GetSpotLocation(64 /*0x40*/);
          spotLocation.X += this.LabelSpacing;
          label.SetSpotLocation(256 /*0x0100*/, spotLocation);
        }
      }

      public virtual MapGeneralNodePortLabel Label
      {
        get => this.myPortLabel;
        set
        {
          MapGeneralNodePortLabel portLabel = this.myPortLabel;
          if (portLabel == value)
            return;
          if (portLabel != null)
          {
            portLabel.Port = (MapGeneralNodePort) null;
            if (portLabel.Parent != null)
              portLabel.Parent.Remove((MapObject) portLabel);
          }
          this.myPortLabel = value;
          if (value != null)
          {
            value.Port = this;
            if (this.LeftSide)
              value.Alignment = 64 /*0x40*/;
            else
              value.Alignment = 256 /*0x0100*/;
            if (this.Parent != null)
              this.Parent.Add((MapObject) value);
          }
          this.LayoutLabel();
          this.Changed(2431, 0, (object) portLabel, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      public virtual float LabelSpacing => 2f;

      public bool LeftSide
      {
        get => this.myLeftSide;
        set
        {
          bool leftSide = this.myLeftSide;
          if (leftSide == value)
            return;
          this.myLeftSide = value;
          this.IsValidFrom = !value;
          this.IsValidTo = value;
          this.Changed(2433, 0, (object) leftSide, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      public string Name
      {
        get => this.myName;
        set
        {
          string name = this.myName;
          if (!(name != value))
            return;
          this.myName = value;
          this.Changed(2430, 0, (object) name, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
          if (this.Label != null)
            this.Label.Text = value;
          this.LinksOnPortChanged(2430, 0, (object) name, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      public virtual float PortAndLabelHeight
      {
        get
        {
          if (!this.Visible)
            return 0.0f;
          MapGeneralNodePortLabel label = this.Label;
          return this.Parent is MapGeneralNode parent && parent.Orientation == Orientation.Vertical ? (label != null && label.Visible ? this.Height + this.LabelSpacing + label.Height : this.Height) : (label != null && label.Visible ? Math.Max(this.Height, label.Height) : this.Height);
        }
      }

      public virtual float PortAndLabelWidth
      {
        get
        {
          if (!this.Visible)
            return 0.0f;
          MapGeneralNodePortLabel label = this.Label;
          return this.Parent is MapGeneralNode parent && parent.Orientation == Orientation.Vertical ? (label != null && label.Visible ? Math.Max(this.Width, label.Width) : this.Width) : (label != null && label.Visible ? this.Width + this.LabelSpacing + label.Width : this.Width);
        }
      }

      public int SideIndex
      {
        get => this.mySideIndex;
        set
        {
          int sideIndex = this.mySideIndex;
          if (sideIndex == value)
            return;
          this.mySideIndex = value;
          this.Changed(2432, sideIndex, (object) null, MapObject.NullRect, value, (object) null, MapObject.NullRect);
        }
      }
    }
}
