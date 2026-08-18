// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapBoxPort
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
    public class MapBoxPort : MapPort
    {
      public const int ChangedLinkPointsSpread = 2211;
      private static IComparer myComparer = (IComparer) new MapBoxPort.EndPositionComparer();
      private bool myLinkPointsSpread;
      [NonSerialized]
      private bool myRespreading;
      [NonSerialized]
      private MapBoxPort.LinkInfo[] mySortedLinks;

      public MapBoxPort()
      {
        this.myLinkPointsSpread = false;
        this.mySortedLinks = (MapBoxPort.LinkInfo[]) null;
        this.myRespreading = false;
        this.Style = MapPortStyle.Rectangle;
        this.Pen = (Pen) null;
        this.Brush = MapShape.Brushes_Gray;
        this.FromSpot = 1;
        this.ToSpot = 1;
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        if (e.SubHint == 2211)
          this.LinkPointsSpread = (bool) e.GetValue(undo);
        else
          base.ChangeValue(e, undo);
      }

      public override MapObject CopyObject(MapCopyDictionary env)
      {
        MapBoxPort mapBoxPort = (MapBoxPort) base.CopyObject(env);
        if (mapBoxPort != null)
        {
          mapBoxPort.mySortedLinks = (MapBoxPort.LinkInfo[]) null;
          mapBoxPort.myRespreading = false;
        }
        return (MapObject) mapBoxPort;
      }

      public override RectangleF ExpandPaintBounds(RectangleF rect, MapView view)
      {
        rect = base.ExpandPaintBounds(rect, view);
        if (this.Style != MapPortStyle.None && this.Parent != null && this.Parent.Shadowed)
        {
          SizeF shadowOffset = this.GetShadowOffset(view);
          if ((double) shadowOffset.Width < 0.0)
          {
            rect.X += shadowOffset.Width;
            rect.Width -= shadowOffset.Width;
          }
          else
            rect.Width += shadowOffset.Width;
          if ((double) shadowOffset.Height < 0.0)
          {
            rect.Y += shadowOffset.Height;
            rect.Height -= shadowOffset.Height;
            return rect;
          }
          rect.Height += shadowOffset.Height;
        }
        return rect;
      }

      public virtual float GetAngle(IMapLink link)
      {
        if (link == null)
          return 0.0f;
        IMapPort mapPort = link.GetOtherPort((IMapPort) this);
        if (mapPort == null)
        {
          if (link.FromPort != null && link.FromPort.MapObject != null && link.FromPort.MapObject.Bounds == this.Bounds)
            mapPort = link.ToPort;
          else if (link.ToPort != null && link.ToPort.MapObject != null && link.ToPort.MapObject.Bounds == this.Bounds)
            mapPort = link.FromPort;
        }
        if (mapPort == null)
          return 0.0f;
        MapObject mapObject = mapPort.MapObject;
        if (mapObject == null)
          return 0.0f;
        PointF pointF = mapObject.Center;
        PointF center = this.Center;
        if (!(link is MapLink mapLink) && link is MapLabeledLink)
          mapLink = ((MapLabeledLink) link).RealLink;
        if (mapLink != null && mapLink.PointsCount > 0)
          pointF = mapLink.FromPort != mapPort ? mapLink.GetPoint(mapLink.PointsCount - 1) : mapLink.GetPoint(0);
        return MapStroke.GetAngle(pointF.X - center.X, pointF.Y - center.Y);
      }

      public virtual float GetDirection(IMapLink link)
      {
        if (link == null)
          return 0.0f;
        return link.FromPort == this ? this.GetFromLinkDir(link) : this.GetToLinkDir(link);
      }

      public override float GetFromLinkDir(IMapLink link)
      {
        if (this.FromSpot != 0 && this.FromSpot != 1)
          return this.GetLinkDir(this.FromSpot);
        float angle = this.GetAngle(link);
        if (this.IsOrthogonal(link))
        {
          if ((double) angle >= 60.0 && (double) angle < 150.0)
            return 90f;
          if ((double) angle >= 150.0 && (double) angle < 240.0)
            return 180f;
          return (double) angle >= 240.0 && (double) angle < 330.0 ? 270f : 0.0f;
        }
        if ((double) angle > 45.0 && (double) angle < 135.0)
          return 90f;
        if ((double) angle >= 135.0 && (double) angle <= 225.0)
          return 180f;
        return (double) angle > 225.0 && (double) angle < 315.0 ? 270f : 0.0f;
      }

      public override PointF GetFromLinkPoint(IMapLink link)
      {
        MapObject mapObject = this.PortObject;
        if (mapObject == null || mapObject.Layer == null)
          mapObject = (MapObject) this;
        if (this.FromSpot != 0 && this.FromSpot != 1)
          return mapObject.GetSpotLocation(this.FromSpot);
        if (link == null || link.MapObject == null)
          return mapObject.Center;
        if (this.LinkPointsSpread)
        {
          MapBoxPort.LinkInfo[] linkInfoArray = this.SortLinks();
          int length = linkInfoArray.Length;
          for (int index = 0; index < length; ++index)
          {
            MapBoxPort.LinkInfo linkInfo = linkInfoArray[index];
            if (linkInfo.Link == link)
              return linkInfo.LinkPoint;
          }
        }
        float angle = this.GetAngle(link);
        int spot = !this.IsOrthogonal(link) ? ((double) angle <= 45.0 || (double) angle >= 135.0 ? ((double) angle < 135.0 || (double) angle > 225.0 ? ((double) angle <= 225.0 || (double) angle >= 315.0 ? 64 /*0x40*/ : 32 /*0x20*/) : 256 /*0x0100*/) : 128 /*0x80*/) : ((double) angle < 60.0 || (double) angle >= 150.0 ? ((double) angle < 150.0 || (double) angle >= 240.0 ? ((double) angle < 240.0 || (double) angle >= 330.0 ? 64 /*0x40*/ : 32 /*0x20*/) : 256 /*0x0100*/) : 128 /*0x80*/);
        return mapObject.GetSpotLocation(spot);
      }

      internal PointF GetSideLinkPoint(MapBoxPort.LinkInfo info)
      {
        MapObject mapObject = this.PortObject;
        if (mapObject == null || mapObject.Layer == null)
          mapObject = (MapObject) this;
        switch (info.Side)
        {
          case 32 /*0x20*/:
            PointF spotLocation1 = mapObject.GetSpotLocation(2);
            PointF spotLocation2 = mapObject.GetSpotLocation(4);
            float num1 = spotLocation2.X - spotLocation1.X;
            float num2 = spotLocation2.Y - spotLocation1.Y;
            float num3 = (float) (((double) info.IndexOnSide + 1.0) / ((double) info.NumOnSide + 1.0));
            return new PointF(spotLocation1.X + num1 * num3, spotLocation1.Y + num2 * num3);
          case 128 /*0x80*/:
            PointF spotLocation3 = mapObject.GetSpotLocation(8);
            PointF spotLocation4 = mapObject.GetSpotLocation(16 /*0x10*/);
            float num4 = spotLocation4.X - spotLocation3.X;
            float num5 = spotLocation4.Y - spotLocation3.Y;
            float num6 = (float) (((double) info.IndexOnSide + 1.0) / ((double) info.NumOnSide + 1.0));
            return new PointF(spotLocation3.X + num4 * num6, spotLocation3.Y + num5 * num6);
          case 256 /*0x0100*/:
            PointF spotLocation5 = mapObject.GetSpotLocation(16 /*0x10*/);
            PointF spotLocation6 = mapObject.GetSpotLocation(2);
            float num7 = spotLocation6.X - spotLocation5.X;
            float num8 = spotLocation6.Y - spotLocation5.Y;
            float num9 = (float) (((double) info.IndexOnSide + 1.0) / ((double) info.NumOnSide + 1.0));
            return new PointF(spotLocation5.X + num7 * num9, spotLocation5.Y + num8 * num9);
          default:
            PointF spotLocation7 = mapObject.GetSpotLocation(4);
            PointF spotLocation8 = mapObject.GetSpotLocation(8);
            float num10 = spotLocation8.X - spotLocation7.X;
            float num11 = spotLocation8.Y - spotLocation7.Y;
            float num12 = (float) (((double) info.IndexOnSide + 1.0) / ((double) info.NumOnSide + 1.0));
            return new PointF(spotLocation7.X + num10 * num12, spotLocation7.Y + num11 * num12);
        }
      }

      public override float GetToLinkDir(IMapLink link)
      {
        if (this.ToSpot != 0 && this.ToSpot != 1)
          return this.GetLinkDir(this.ToSpot);
        float angle = this.GetAngle(link);
        if (this.IsOrthogonal(link))
        {
          if ((double) angle >= 30.0 && (double) angle < 120.0)
            return 90f;
          if ((double) angle >= 120.0 && (double) angle < 210.0)
            return 180f;
          return (double) angle >= 210.0 && (double) angle < 300.0 ? 270f : 0.0f;
        }
        if ((double) angle > 45.0 && (double) angle < 135.0)
          return 90f;
        if ((double) angle >= 135.0 && (double) angle <= 225.0)
          return 180f;
        return (double) angle > 225.0 && (double) angle < 315.0 ? 270f : 0.0f;
      }

      public override PointF GetToLinkPoint(IMapLink link)
      {
        MapObject mapObject = this.PortObject;
        if (mapObject == null || mapObject.Layer == null)
          mapObject = (MapObject) this;
        if (this.ToSpot != 0 && this.ToSpot != 1)
          return mapObject.GetSpotLocation(this.ToSpot);
        if (link == null || link.MapObject == null)
          return mapObject.Center;
        if (this.LinkPointsSpread)
        {
          MapBoxPort.LinkInfo[] linkInfoArray = this.SortLinks();
          int length = linkInfoArray.Length;
          for (int index = 0; index < length; ++index)
          {
            MapBoxPort.LinkInfo linkInfo = linkInfoArray[index];
            if (linkInfo.Link == link)
              return linkInfo.LinkPoint;
          }
        }
        float angle = this.GetAngle(link);
        int spot = !this.IsOrthogonal(link) ? ((double) angle <= 45.0 || (double) angle >= 135.0 ? ((double) angle < 135.0 || (double) angle > 225.0 ? ((double) angle <= 225.0 || (double) angle >= 315.0 ? 64 /*0x40*/ : 32 /*0x20*/) : 256 /*0x0100*/) : 128 /*0x80*/) : ((double) angle < 30.0 || (double) angle >= 120.0 ? ((double) angle < 120.0 || (double) angle >= 210.0 ? ((double) angle < 210.0 || (double) angle >= 300.0 ? 64 /*0x40*/ : 32 /*0x20*/) : 256 /*0x0100*/) : 128 /*0x80*/);
        return mapObject.GetSpotLocation(spot);
      }

      public virtual bool IsOrthogonal(IMapLink link)
      {
        switch (link)
        {
          case MapLink mapLink:
            return mapLink.Orthogonal;
          case MapLabeledLink mapLabeledLink:
            return mapLabeledLink.Orthogonal;
          default:
            return false;
        }
      }

      public override void Paint(Graphics g, MapView view)
      {
        if (this.Style != MapPortStyle.None && this.Parent != null && this.Parent.Shadowed)
        {
          RectangleF bounds = this.Bounds;
          SizeF shadowOffset = this.Parent.GetShadowOffset(view);
          if (this.Brush != null)
          {
            Brush shadowBrush = this.Parent.GetShadowBrush(view);
            MapShape.DrawRectangle(g, view, (Pen) null, shadowBrush, bounds.X + shadowOffset.Width, bounds.Y + shadowOffset.Height, bounds.Width, bounds.Height);
          }
          else if (this.Pen != null)
          {
            Pen shadowPen = this.Parent.GetShadowPen(view, MapShape.GetPenWidth(this.Pen));
            MapShape.DrawRectangle(g, view, shadowPen, (Brush) null, bounds.X + shadowOffset.Width, bounds.Y + shadowOffset.Height, bounds.Width, bounds.Height);
          }
        }
        base.Paint(g, view);
      }

      internal MapBoxPort.LinkInfo[] SortLinks()
      {
        if (this.mySortedLinks == null || this.mySortedLinks.Length != this.LinksCount)
          this.mySortedLinks = new MapBoxPort.LinkInfo[this.LinksCount];
        if (!this.myRespreading)
        {
          bool respreading = this.myRespreading;
          this.myRespreading = true;
          int num1 = 0;
          foreach (IMapLink link in this.Links)
          {
            float direction = this.GetDirection(link);
            float angle = this.GetAngle(link);
            int s;
            if ((double) direction == 0.0)
            {
              s = 64 /*0x40*/;
              if ((double) angle > 180.0)
                angle -= 360f;
            }
            else
              s = (double) direction != 90.0 ? ((double) direction != 180.0 ? 32 /*0x20*/ : 256 /*0x0100*/) : 128 /*0x80*/;
            this.mySortedLinks[num1++] = new MapBoxPort.LinkInfo(link, angle, s, direction);
          }
          Array.Sort((Array) this.mySortedLinks, 0, this.mySortedLinks.Length, MapBoxPort.myComparer);
          int length = this.mySortedLinks.Length;
          int num2 = -1;
          int num3 = 0;
          for (int index = 0; index < length; ++index)
          {
            MapBoxPort.LinkInfo sortedLink = this.mySortedLinks[index];
            if (sortedLink.Side != num2)
            {
              num2 = sortedLink.Side;
              num3 = 0;
            }
            sortedLink.IndexOnSide = num3;
            ++num3;
          }
          int num4 = -1;
          int num5 = 0;
          for (int index = length - 1; index >= 0; --index)
          {
            MapBoxPort.LinkInfo sortedLink = this.mySortedLinks[index];
            if (sortedLink.Side != num4)
            {
              num4 = sortedLink.Side;
              num5 = sortedLink.IndexOnSide + 1;
            }
            sortedLink.NumOnSide = num5;
            sortedLink.LinkPoint = this.GetSideLinkPoint(sortedLink);
          }
          this.myRespreading = respreading;
        }
        return this.mySortedLinks;
      }

      [Description("Whether the link points are distributed evenly along each side")]
      [Category("Appearance")]
      [DefaultValue(false)]
      public virtual bool LinkPointsSpread
      {
        get => this.myLinkPointsSpread;
        set
        {
          bool linkPointsSpread = this.myLinkPointsSpread;
          if (linkPointsSpread == value)
            return;
          this.myLinkPointsSpread = value;
          this.Changed(2211, 0, (object) linkPointsSpread, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
          this.LinksOnPortChanged(2211, 0, (object) linkPointsSpread, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Serializable]
      internal sealed class EndPositionComparer : IComparer
      {
        internal EndPositionComparer()
        {
        }

        public int Compare(object x, object y)
        {
          MapBoxPort.LinkInfo linkInfo1 = x as MapBoxPort.LinkInfo;
          MapBoxPort.LinkInfo linkInfo2 = y as MapBoxPort.LinkInfo;
          if (linkInfo1 != null && linkInfo2 != null)
          {
            if (linkInfo1.Side < linkInfo2.Side)
              return -1;
            if (linkInfo1.Side > linkInfo2.Side)
              return 1;
            if ((double) linkInfo1.Angle < (double) linkInfo2.Angle)
              return -1;
            if ((double) linkInfo1.Angle > (double) linkInfo2.Angle)
              return 1;
          }
          return 0;
        }
      }

      [Serializable]
      internal sealed class LinkInfo
      {
        internal float Angle;
        internal float Direction;
        internal int IndexOnSide;
        internal IMapLink Link;
        internal PointF LinkPoint;
        internal int NumOnSide;
        internal int Side;

        internal LinkInfo(IMapLink l, float a, int s, float d)
        {
          this.Link = l;
          this.Angle = a;
          this.Side = s;
          this.Direction = d;
        }
      }
    }
}
