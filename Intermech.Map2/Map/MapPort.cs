// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapPort
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Intermech.Map
{
    [Serializable]
    public class MapPort : MapShape, IMapPort, IMapGraphPart, IMapIdentifiablePart
    {
      public const int ChangedPortUserObject = 1701;
      public const int ChangedStyle = 1702;
      public const int ChangedObject = 1703;
      public const int ChangedValidFrom = 1704;
      public const int ChangedValidTo = 1705;
      public const int ChangedValidSelfNode = 1706;
      public const int ChangedFromSpot = 1707;
      public const int ChangedToSpot = 1708;
      public const int ChangedAddedLink = 1709;
      public const int ChangedValidDuplicateLinks = 1711;
      public const int ChangedEndSegmentLength = 1712;
      public const int ChangedPartID = 1713;
      public const int ChangedClearsLinksWhenRemoved = 1714;
      public const int ChangedPortUserFlags = 1700;
      public const int ChangedRemovedLink = 1710;
      private const int flagClearsLinksWhenRemoved = 33554432 /*0x02000000*/;
      private const int flagNoClearLinks = 67108864 /*0x04000000*/;
      private const int flagRecursive = 16777216 /*0x01000000*/;
      private const int flagRedirectToSubGraphPort = 134217728 /*0x08000000*/;
      private const int flagValidDuplicateLinks = 8388608 /*0x800000*/;
      private const int flagValidFrom = 1048576 /*0x100000*/;
      private const int flagValidSelfNode = 4194304 /*0x400000*/;
      private const int flagValidTo = 2097152 /*0x200000*/;
      private float myEndSegmentLength;
      private int myFromLinkSpot;
      protected ArrayList myLinks;
      private int myPartID;
      private MapObject myPortObject;
      private MapPortStyle myStyle;
      private int myToLinkSpot;
      private int myUserFlags;
      private object myUserObject;

      public MapPort()
      {
        this.myStyle = MapPortStyle.Ellipse;
        this.myPortObject = (MapObject) null;
        this.myFromLinkSpot = 64 /*0x40*/;
        this.myToLinkSpot = 256 /*0x0100*/;
        this.myLinks = new ArrayList();
        this.myEndSegmentLength = 10f;
        this.myUserFlags = 0;
        this.myUserObject = (object) null;
        this.myPartID = -1;
        this.InternalFlags &= -17;
        this.InternalFlags &= -3;
        this.InternalFlags |= 36700160 /*0x02300000*/;
        this.Brush = MapShape.Brushes_Black;
      }

      public virtual void AddDestinationLink(IMapLink link)
      {
        link.FromPort = (IMapPort) this;
        if (link.FromPort != this)
          return;
        this.addLink(link);
      }

      private void addLink(IMapLink link)
      {
        if (this.myLinks.Contains((object) link))
          return;
        this.myLinks.Add((object) link);
        this.Changed(1709, 0, (object) link, MapObject.NullRect, 0, (object) link, MapObject.NullRect);
        this.OnLinkChanged(link, 1709, 0, (object) link, MapObject.NullRect, 0, (object) link, MapObject.NullRect);
      }

      public virtual void AddSourceLink(IMapLink link)
      {
        link.ToPort = (IMapPort) this;
        if (link.ToPort != this)
          return;
        this.addLink(link);
      }

      public virtual bool CanLinkFrom()
      {
        return this.IsValidFrom && this.CanView() && (this.Layer == null || this.Layer.CanLinkObjects());
      }

      public virtual bool CanLinkTo()
      {
        return this.IsValidTo && this.CanView() && (this.Layer == null || this.Layer.CanLinkObjects());
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 1700:
            this.UserFlags = e.GetInt(undo);
            break;
          case 1701:
            this.UserObject = e.GetValue(undo);
            break;
          case 1702:
            this.Style = (MapPortStyle) e.GetInt(undo);
            break;
          case 1703:
            this.PortObject = (MapObject) e.GetValue(undo);
            break;
          case 1704:
            this.IsValidFrom = (bool) e.GetValue(undo);
            break;
          case 1705:
            this.IsValidTo = (bool) e.GetValue(undo);
            break;
          case 1706:
            this.IsValidSelfNode = (bool) e.GetValue(undo);
            break;
          case 1707:
            this.FromSpot = e.GetInt(undo);
            break;
          case 1708:
            this.ToSpot = e.GetInt(undo);
            break;
          case 1709:
            IMapLink oldValue1 = (IMapLink) e.OldValue;
            if (!undo)
            {
              this.addLink(oldValue1);
              break;
            }
            this.RemoveLink(oldValue1);
            break;
          case 1710:
            IMapLink oldValue2 = (IMapLink) e.OldValue;
            if (!undo)
            {
              this.RemoveLink(oldValue2);
              break;
            }
            this.addLink(oldValue2);
            break;
          case 1711:
            this.IsValidDuplicateLinks = (bool) e.GetValue(undo);
            break;
          case 1712:
            this.EndSegmentLength = e.GetFloat(undo);
            break;
          case 1713:
            this.PartID = e.GetInt(undo);
            break;
          case 1714:
            this.ClearsLinksWhenRemoved = (bool) e.GetValue(undo);
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      public virtual void ClearLinks() => this.ClearLinks((MapObject) null);

      private void ClearLinks(MapObject mainObj)
      {
        IMapLayerCollectionContainer collectionContainer = this.Layer != null ? this.Layer.LayerCollectionContainer : (IMapLayerCollectionContainer) null;
        int val1;
        for (int index = this.myLinks.Count; index > 0; index = Math.Min(val1, this.myLinks.Count))
        {
          IMapLink link = (IMapLink) this.myLinks[val1 = index - 1];
          MapObject mapObject = link.MapObject;
          if (mapObject == null || mapObject.Layer == null || mapObject.Layer.LayerCollectionContainer == collectionContainer && (mainObj == null || !mapObject.IsChildOf(mainObj) && !mapObject.Movable))
            link.Unlink();
        }
      }

      private void ComputeTrianglePoints(PointF[] v)
      {
        RectangleF bounds = this.Bounds;
        int num1;
        switch (this.Style)
        {
          case MapPortStyle.Triangle:
            num1 = this.ToSpot;
            break;
          case MapPortStyle.TriangleTopLeft:
            num1 = 8;
            break;
          case MapPortStyle.TriangleTopRight:
            num1 = 16 /*0x10*/;
            break;
          case MapPortStyle.TriangleBottomRight:
            num1 = 2;
            break;
          case MapPortStyle.TriangleBottomLeft:
            num1 = 4;
            break;
          case MapPortStyle.TriangleMiddleTop:
            num1 = 128 /*0x80*/;
            break;
          case MapPortStyle.TriangleMiddleRight:
            num1 = 256 /*0x0100*/;
            break;
          case MapPortStyle.TriangleMiddleBottom:
            num1 = 32 /*0x20*/;
            break;
          case MapPortStyle.TriangleMiddleLeft:
            num1 = 64 /*0x40*/;
            break;
          default:
            num1 = this.ToSpot;
            break;
        }
        int num2 = num1;
        if (num2 <= 16 /*0x10*/)
        {
          switch (num2 - 1)
          {
            case 0:
            case 2:
              v[0].X = bounds.X;
              v[0].Y = bounds.Y;
              v[1].X = bounds.X + bounds.Width;
              v[1].Y = bounds.Y + bounds.Height / 2f;
              v[2].X = bounds.X;
              v[2].Y = bounds.Y + bounds.Height;
              return;
            case 1:
              v[0].X = bounds.X + bounds.Width / 2f;
              v[0].Y = bounds.Y;
              v[1].X = bounds.X + bounds.Width;
              v[1].Y = bounds.Y + bounds.Height;
              v[2].X = bounds.X;
              v[2].Y = bounds.Y + bounds.Height / 2f;
              return;
            case 3:
              v[0].X = bounds.X + bounds.Width;
              v[0].Y = bounds.Y + bounds.Height / 2f;
              v[1].X = bounds.X;
              v[1].Y = bounds.Y + bounds.Height;
              v[2].X = bounds.X + bounds.Width / 2f;
              v[2].Y = bounds.Y;
              return;
            case 4:
            case 5:
            case 6:
              break;
            case 7:
              v[0].X = bounds.X + bounds.Width / 2f;
              v[0].Y = bounds.Y + bounds.Height;
              v[1].X = bounds.X;
              v[1].Y = bounds.Y;
              v[2].X = bounds.X + bounds.Width;
              v[2].Y = bounds.Y + bounds.Height / 2f;
              return;
            default:
              if (num2 == 16 /*0x10*/)
              {
                v[0].X = bounds.X;
                v[0].Y = bounds.Y + bounds.Height / 2f;
                v[1].X = bounds.X + bounds.Width;
                v[1].Y = bounds.Y;
                v[2].X = bounds.X + bounds.Width / 2f;
                v[2].Y = bounds.Y + bounds.Height;
                return;
              }
              break;
          }
        }
        else if (num2 <= 64 /*0x40*/)
        {
          if (num2 == 32 /*0x20*/)
          {
            v[0].X = bounds.X + bounds.Width;
            v[0].Y = bounds.Y;
            v[1].X = bounds.X + bounds.Width / 2f;
            v[1].Y = bounds.Y + bounds.Height;
            v[2].X = bounds.X;
            v[2].Y = bounds.Y;
            return;
          }
          if (num2 == 64 /*0x40*/)
          {
            v[0].X = bounds.X + bounds.Width;
            v[0].Y = bounds.Y + bounds.Height;
            v[1].X = bounds.X;
            v[1].Y = bounds.Y + bounds.Height / 2f;
            v[2].X = bounds.X + bounds.Width;
            v[2].Y = bounds.Y;
            return;
          }
        }
        else if (num2 == 128 /*0x80*/)
        {
          v[0].X = bounds.X;
          v[0].Y = bounds.Y + bounds.Height;
          v[1].X = bounds.X + bounds.Width / 2f;
          v[1].Y = bounds.Y;
          v[2].X = bounds.X + bounds.Width;
          v[2].Y = bounds.Y + bounds.Height;
          return;
        }
        v[0].X = bounds.X;
        v[0].Y = bounds.Y;
        v[1].X = bounds.X + bounds.Width;
        v[1].Y = bounds.Y + bounds.Height / 2f;
        v[2].X = bounds.X;
        v[2].Y = bounds.Y + bounds.Height;
      }

      public virtual bool ContainsLink(IMapLink l) => this.myLinks.Contains((object) l);

      [Description("A array copy of all of the links connected at this port.")]
      public virtual IMapLink[] CopyLinksArray()
      {
        IMapLink[] mapLinkArray = new IMapLink[this.LinksCount];
        this.myLinks.CopyTo((Array) mapLinkArray, 0);
        return mapLinkArray;
      }

      public override MapObject CopyObject(MapCopyDictionary env)
      {
        MapPort mapPort = (MapPort) base.CopyObject(env);
        if (mapPort != null)
        {
          mapPort.myLinks = new ArrayList();
          mapPort.myPartID = -1;
          if (this.myPortObject != null)
            env.Delayeds.Add((object) this);
        }
        return (MapObject) mapPort;
      }

      public override void CopyObjectDelayed(MapCopyDictionary env, MapObject newobj)
      {
        base.CopyObjectDelayed(env, newobj);
        MapPort mapPort = (MapPort) newobj;
        if (!(env[(object) this.myPortObject] is MapObject mapObject))
          return;
        mapPort.myPortObject = mapObject;
      }

      private bool CycleOK(IMapPort toPort)
      {
        MapDocument document = this.Document;
        if (document != null)
        {
          switch (document.ValidCycle)
          {
            case MapDocumentValidCycle.NotDirected:
              return !MapDocument.MakesDirectedCycle(this.Node, toPort.Node);
            case MapDocumentValidCycle.NotDirectedFast:
              return !MapDocument.MakesDirectedCycleFast(this.Node, toPort.Node);
            case MapDocumentValidCycle.NotUndirected:
              return !MapDocument.MakesUndirectedCycle(this.Node, toPort.Node);
            case MapDocumentValidCycle.DestinationTree:
              return toPort.SourceLinksCount == 0 && !MapDocument.MakesDirectedCycleFast(this.Node, toPort.Node);
            case MapDocumentValidCycle.SourceTree:
              return this.DestinationLinksCount == 0 && !MapDocument.MakesDirectedCycleFast(this.Node, toPort.Node);
          }
        }
        return true;
      }

      public override RectangleF ExpandPaintBounds(RectangleF rect, MapView view)
      {
        MapObject portObject = this.PortObject;
        if (portObject == null || portObject == this || this.Style != MapPortStyle.Object || portObject.Layer != null || (this.InternalFlags & 16777216 /*0x01000000*/) != 0)
          return base.ExpandPaintBounds(rect, view);
        this.InternalFlags |= 16777216 /*0x01000000*/;
        RectangleF rectangleF = portObject.ExpandPaintBounds(rect, view);
        this.InternalFlags &= -16777217;
        return rectangleF;
      }

      internal MapSubGraph FindCollapsedSubGraph(MapObject obj)
      {
        if (obj == null)
          return (MapSubGraph) null;
        if (obj.Parent is MapSubGraph)
        {
          obj = (MapObject) obj.Parent;
          if (obj.CanView())
            return (MapSubGraph) null;
        }
        MapSubGraph parentSubGraph = MapSubGraph.FindParentSubGraph(obj);
        MapSubGraph collapsedSubGraph = (MapSubGraph) null;
        for (; parentSubGraph != null && !parentSubGraph.IsExpanded; parentSubGraph = MapSubGraph.FindParentSubGraph((MapObject) parentSubGraph))
          collapsedSubGraph = parentSubGraph;
        return collapsedSubGraph;
      }

      public static IMapNode FindParentNode(MapObject x)
      {
        if (x == null)
          return (IMapNode) null;
        return x is IMapNode mapNode ? mapNode : MapPort.FindParentNode((MapObject) x.Parent);
      }

      public static IMapNode FindTopNode(MapObject x)
      {
        if (x == null)
          return (IMapNode) null;
        return x.IsTopLevel ? x as IMapNode : MapPort.FindTopNode((MapObject) x.Parent) ?? x as IMapNode;
      }

      public virtual float GetFromLinkDir(IMapLink link)
      {
        if ((this.InternalFlags & 134217728 /*0x08000000*/) != 0 && !this.CanView())
        {
          MapSubGraph collapsedSubGraph = this.FindCollapsedSubGraph((MapObject) this);
          if (collapsedSubGraph != null && collapsedSubGraph.Port != null)
            return collapsedSubGraph.Port.GetFromLinkDir(link);
        }
        int fromSpot = this.FromSpot;
        switch (fromSpot)
        {
          case 0:
          case 1:
            if (link == null || link.ToPort == null || link.ToPort.MapObject == null)
              return 0.0f;
            PointF center1 = link.ToPort.MapObject.Center;
            PointF center2 = this.Center;
            return (double) Math.Abs(center1.X - center2.X) > (double) Math.Abs(center1.Y - center2.Y) ? ((double) center1.X >= (double) center2.X ? 0.0f : 180f) : ((double) center1.Y >= (double) center2.Y ? 90f : 270f);
          default:
            return this.GetLinkDir(fromSpot);
        }
      }

      public virtual PointF GetFromLinkPoint(IMapLink link)
      {
        if ((this.InternalFlags & 134217728 /*0x08000000*/) != 0 && !this.CanView())
        {
          MapSubGraph collapsedSubGraph = this.FindCollapsedSubGraph((MapObject) this);
          if (collapsedSubGraph != null && collapsedSubGraph.Port != null)
            return collapsedSubGraph.Port.GetFromLinkPoint(link);
        }
        if (this.FromSpot != 0)
          return this.GetSpotLocation(this.FromSpot);
        if (link == null || link.ToPort == null || link.ToPort.MapObject == null)
          return this.Center;
        if (!(link is MapLink mapLink) && link is MapLabeledLink)
          mapLink = (link as MapLabeledLink).RealLink;
        PointF p;
        if (mapLink != null && mapLink.PointsCount > (mapLink.Orthogonal ? 6 : 2))
        {
          p = mapLink.GetPoint(1);
          if (mapLink.Orthogonal)
            p = this.OrthoPointToward(p);
        }
        else
        {
          p = link.ToPort.MapObject.Center;
          if (mapLink != null && mapLink.Orthogonal)
            p = this.OrthoPointToward(p);
        }
        return this.GetLinkPointFromPoint(p);
      }

      public virtual float GetLinkDir(int spot)
      {
        int num = spot;
        if (num <= 16 /*0x10*/)
        {
          switch (num - 1)
          {
            case 0:
            case 2:
              return 0.0f;
            case 1:
              return 225f;
            case 3:
              return 315f;
            case 4:
            case 5:
            case 6:
              break;
            case 7:
              return 45f;
            default:
              if (num == 16 /*0x10*/)
                return 135f;
              break;
          }
        }
        else
        {
          if (num <= 64 /*0x40*/)
          {
            if (num == 32 /*0x20*/)
              return 270f;
            return 0.0f;
          }
          if (num == 128 /*0x80*/)
            return 90f;
          if (num == 256 /*0x0100*/)
            return 180f;
        }
        return 0.0f;
      }

      public virtual PointF GetLinkPointFromPoint(PointF p)
      {
        if ((this.InternalFlags & 134217728 /*0x08000000*/) != 0 && !this.CanView())
        {
          MapSubGraph collapsedSubGraph = this.FindCollapsedSubGraph((MapObject) this);
          if (collapsedSubGraph != null && collapsedSubGraph.Port != null)
            return collapsedSubGraph.Port.GetLinkPointFromPoint(p);
        }
        MapObject mapObject = this.PortObject;
        if (mapObject == null || mapObject.Layer == null)
          mapObject = (MapObject) this;
        PointF result;
        return !mapObject.ContainsPoint(p) && this.GetNearestIntersectionPoint(p, this.Center, out result) ? result : mapObject.Center;
      }

      public override bool GetNearestIntersectionPoint(PointF p1, PointF p2, out PointF result)
      {
        MapObject portObject = this.PortObject;
        if (portObject == null || portObject == this || this.Style == MapPortStyle.Object || portObject.Layer == null || (this.InternalFlags & 16777216 /*0x01000000*/) != 0)
          return base.GetNearestIntersectionPoint(p1, p2, out result);
        this.InternalFlags |= 16777216 /*0x01000000*/;
        int num = portObject.GetNearestIntersectionPoint(p1, p2, out result) ? 1 : 0;
        this.InternalFlags &= -16777217;
        return num != 0;
      }

      public virtual float GetToLinkDir(IMapLink link)
      {
        if ((this.InternalFlags & 134217728 /*0x08000000*/) != 0 && !this.CanView())
        {
          MapSubGraph collapsedSubGraph = this.FindCollapsedSubGraph((MapObject) this);
          if (collapsedSubGraph != null && collapsedSubGraph.Port != null)
            return collapsedSubGraph.Port.GetToLinkDir(link);
        }
        int toSpot = this.ToSpot;
        switch (toSpot)
        {
          case 0:
          case 1:
            if (link == null || link.FromPort == null || link.FromPort.MapObject == null)
              return 0.0f;
            PointF center1 = link.FromPort.MapObject.Center;
            PointF center2 = this.Center;
            return (double) Math.Abs(center1.X - center2.X) > (double) Math.Abs(center1.Y - center2.Y) ? ((double) center1.X >= (double) center2.X ? 0.0f : 180f) : ((double) center1.Y >= (double) center2.Y ? 90f : 270f);
          default:
            return this.GetLinkDir(toSpot);
        }
      }

      public virtual PointF GetToLinkPoint(IMapLink link)
      {
        if ((this.InternalFlags & 134217728 /*0x08000000*/) != 0 && !this.CanView())
        {
          MapSubGraph collapsedSubGraph = this.FindCollapsedSubGraph((MapObject) this);
          if (collapsedSubGraph != null && collapsedSubGraph.Port != null)
            return collapsedSubGraph.Port.GetToLinkPoint(link);
        }
        if (this.ToSpot != 0)
          return this.GetSpotLocation(this.ToSpot);
        if (link == null || link.FromPort == null || link.FromPort.MapObject == null)
          return this.Center;
        if (!(link is MapLink mapLink) && link is MapLabeledLink)
          mapLink = (link as MapLabeledLink).RealLink;
        PointF p;
        if (mapLink != null && mapLink.PointsCount > (mapLink.Orthogonal ? 6 : 2))
        {
          p = mapLink.GetPoint(mapLink.PointsCount - 2);
          if (mapLink.Orthogonal)
            p = this.OrthoPointToward(p);
        }
        else
        {
          p = link.FromPort.MapObject.Center;
          if (mapLink != null && mapLink.Orthogonal)
            p = this.OrthoPointToward(p);
        }
        return this.GetLinkPointFromPoint(p);
      }

      public virtual bool IsInSameNode(IMapPort p) => MapPort.IsInSameNode((IMapPort) this, p);

      public static bool IsInSameNode(IMapPort a, IMapPort b)
      {
        if (a != null && b != null)
        {
          if (a == b)
            return true;
          object obj1 = (object) a.Node;
          if (obj1 == null && a.MapObject != null)
            obj1 = (object) a.MapObject.TopLevelObject;
          object obj2 = (object) b.Node;
          if (obj2 == null && b.MapObject != null)
            obj2 = (object) b.MapObject.TopLevelObject;
          if (obj1 != null)
            return obj1 == obj2;
        }
        return false;
      }

      public virtual bool IsLinked(IMapPort p) => MapPort.IsLinked((IMapPort) this, p);

      public static bool IsLinked(IMapPort a, IMapPort b)
      {
        if (a != null && b != null)
        {
          if (b is MapPort mapPort)
          {
            foreach (IMapLink link in mapPort.Links)
            {
              IMapPort fromPort = link.FromPort;
              IMapPort toPort = link.ToPort;
              if (fromPort == a && toPort == b)
                return true;
            }
          }
          else
          {
            foreach (IMapLink link in b.Links)
            {
              IMapPort fromPort = link.FromPort;
              IMapPort toPort = link.ToPort;
              if (fromPort == a && toPort == b)
                return true;
            }
          }
        }
        return false;
      }

      public virtual bool IsValidLink(IMapPort toPort)
      {
        return this.CanLinkFrom() && toPort != null && toPort.CanLinkTo() && (this.IsValidSelfNode && toPort.MapObject is MapPort && ((MapPort) toPort.MapObject).IsValidSelfNode || !this.IsInSameNode(toPort)) && (this.IsValidDuplicateLinks && toPort.MapObject is MapPort && ((MapPort) toPort.MapObject).IsValidDuplicateLinks || !this.IsLinked(toPort)) && this.CycleOK(toPort);
      }

      public virtual void LinksOnPortChanged(
        int subhint,
        int oldI,
        object oldVal,
        RectangleF oldRect,
        int newI,
        object newVal,
        RectangleF newRect)
      {
        foreach (IMapLink link in this.Links)
          link?.OnPortChanged((IMapPort) this, subhint, oldI, oldVal, oldRect, newI, newVal, newRect);
      }

      public override GraphicsPath MakePath()
      {
        GraphicsPath graphicsPath = new GraphicsPath(FillMode.Winding);
        RectangleF bounds = this.Bounds;
        switch (this.Style)
        {
          case MapPortStyle.None:
            graphicsPath.AddLine(bounds.X, bounds.Y, bounds.X, bounds.Y);
            return graphicsPath;
          case MapPortStyle.Object:
            graphicsPath.AddLine(bounds.X, bounds.Y, bounds.X, bounds.Y);
            return graphicsPath;
          case MapPortStyle.Triangle:
          case MapPortStyle.TriangleTopLeft:
          case MapPortStyle.TriangleTopRight:
          case MapPortStyle.TriangleBottomRight:
          case MapPortStyle.TriangleBottomLeft:
          case MapPortStyle.TriangleMiddleTop:
          case MapPortStyle.TriangleMiddleRight:
          case MapPortStyle.TriangleMiddleBottom:
          case MapPortStyle.TriangleMiddleLeft:
            PointF[] pointFArray = new PointF[3];
            this.ComputeTrianglePoints(pointFArray);
            graphicsPath.AddPolygon(pointFArray);
            return graphicsPath;
          case MapPortStyle.Rectangle:
            graphicsPath.AddRectangle(bounds);
            return graphicsPath;
          case MapPortStyle.Diamond:
            PointF[] points = new PointF[4];
            points[0].X = bounds.X + bounds.Width / 2f;
            points[0].Y = bounds.Y;
            points[1].X = bounds.X + bounds.Width;
            points[1].Y = bounds.Y + bounds.Height / 2f;
            points[2].X = points[0].X;
            points[2].Y = bounds.Y + bounds.Height;
            points[3].X = bounds.X;
            points[3].Y = points[1].Y;
            graphicsPath.AddPolygon(points);
            return graphicsPath;
          case MapPortStyle.Plus:
            graphicsPath.AddLine(bounds.X + bounds.Width / 2f, bounds.Y, bounds.X + bounds.Width / 2f, bounds.Y + bounds.Height);
            graphicsPath.StartFigure();
            graphicsPath.AddLine(bounds.X, bounds.Y + bounds.Height / 2f, bounds.X + bounds.Width, bounds.Y + bounds.Height / 2f);
            return graphicsPath;
          case MapPortStyle.Times:
            graphicsPath.AddLine(bounds.X, bounds.Y, bounds.X + bounds.Width, bounds.Y + bounds.Height);
            graphicsPath.StartFigure();
            graphicsPath.AddLine(bounds.X + bounds.Width, bounds.Y, bounds.X, bounds.Y + bounds.Height);
            return graphicsPath;
          case MapPortStyle.PlusTimes:
            graphicsPath.AddLine(bounds.X + bounds.Width / 2f, bounds.Y, bounds.X + bounds.Width / 2f, bounds.Y + bounds.Height);
            graphicsPath.StartFigure();
            graphicsPath.AddLine(bounds.X, bounds.Y + bounds.Height / 2f, bounds.X + bounds.Width, bounds.Y + bounds.Height / 2f);
            graphicsPath.StartFigure();
            graphicsPath.AddLine(bounds.X, bounds.Y, bounds.X + bounds.Width, bounds.Y + bounds.Height);
            graphicsPath.StartFigure();
            graphicsPath.AddLine(bounds.X + bounds.Width, bounds.Y, bounds.X, bounds.Y + bounds.Height);
            return graphicsPath;
          default:
            graphicsPath.AddEllipse(bounds.X, bounds.Y, bounds.Width, bounds.Height);
            return graphicsPath;
        }
      }

      IEnumerable IMapPort.DestinationLinks
      {
        get => (IEnumerable) new MapPortFilteredLinkEnumerator((IMapPort) this, this.myLinks, true);
      }

      IEnumerable IMapPort.Links => (IEnumerable) new MapPortLinkEnumerator(this.myLinks);

      IEnumerable IMapPort.SourceLinks
      {
        get => (IEnumerable) new MapPortFilteredLinkEnumerator((IMapPort) this, this.myLinks, false);
      }

      protected override void OnBoundsChanged(RectangleF old)
      {
        base.OnBoundsChanged(old);
        this.LinksOnPortChanged(1001, 0, (object) null, old, 0, (object) null, this.Bounds);
      }

      protected override void OnLayerChanged(MapLayer oldlayer, MapLayer newlayer, MapObject mainObj)
      {
        base.OnLayerChanged(oldlayer, newlayer, mainObj);
        if (newlayer != null || !this.ClearsLinksWhenRemoved || this.NoClearLinks)
          return;
        this.ClearLinks(mainObj);
      }

      public virtual void OnLinkChanged(
        IMapLink l,
        int subhint,
        int oldI,
        object oldVal,
        RectangleF oldRect,
        int newI,
        object newVal,
        RectangleF newRect)
      {
      }

      public override bool OnMouseOver(MapInputEventArgs evt, MapView view)
      {
        if (!view.CanLinkObjects() || !this.CanLinkFrom() && !this.CanLinkTo())
          return false;
        view.Cursor = Cursors.Hand;
        return true;
      }

      private PointF OrthoPointToward(PointF p)
      {
        PointF center = this.Center;
        if ((double) Math.Abs(p.X - center.X) >= (double) Math.Abs(p.Y - center.Y))
        {
          p.X = (double) p.X < (double) center.X ? -99999f : 99999f;
          p.Y = center.Y;
          return p;
        }
        p.Y = (double) p.Y < (double) center.Y ? -99999f : 99999f;
        p.X = center.X;
        return p;
      }

      public override void Paint(Graphics g, MapView view)
      {
        if (this.PaintGreek(g, view))
          return;
        RectangleF bounds = this.Bounds;
        switch (this.Style)
        {
          case MapPortStyle.None:
            break;
          case MapPortStyle.Object:
            MapObject portObject = this.PortObject;
            if (portObject == null || portObject.Layer != null)
              break;
            portObject.Bounds = bounds;
            portObject.Paint(g, view);
            break;
          case MapPortStyle.Triangle:
          case MapPortStyle.TriangleTopLeft:
          case MapPortStyle.TriangleTopRight:
          case MapPortStyle.TriangleBottomRight:
          case MapPortStyle.TriangleBottomLeft:
          case MapPortStyle.TriangleMiddleTop:
          case MapPortStyle.TriangleMiddleRight:
          case MapPortStyle.TriangleMiddleBottom:
          case MapPortStyle.TriangleMiddleLeft:
            PointF[] pointFArray1 = view.AllocTempPointArray(3);
            this.ComputeTrianglePoints(pointFArray1);
            MapShape.DrawPolygon(g, view, this.Pen, this.Brush, pointFArray1);
            view.FreeTempPointArray(pointFArray1);
            break;
          case MapPortStyle.Rectangle:
            MapShape.DrawRectangle(g, view, this.Pen, this.Brush, bounds.X, bounds.Y, bounds.Width, bounds.Height);
            break;
          case MapPortStyle.Diamond:
            PointF[] pointFArray2 = view.AllocTempPointArray(4);
            pointFArray2[0].X = bounds.X + bounds.Width / 2f;
            pointFArray2[0].Y = bounds.Y;
            pointFArray2[1].X = bounds.X + bounds.Width;
            pointFArray2[1].Y = bounds.Y + bounds.Height / 2f;
            pointFArray2[2].X = pointFArray2[0].X;
            pointFArray2[2].Y = bounds.Y + bounds.Height;
            pointFArray2[3].X = bounds.X;
            pointFArray2[3].Y = pointFArray2[1].Y;
            MapShape.DrawPolygon(g, view, this.Pen, this.Brush, pointFArray2);
            view.FreeTempPointArray(pointFArray2);
            break;
          case MapPortStyle.Plus:
            MapShape.DrawLine(g, view, this.Pen, bounds.X + bounds.Width / 2f, bounds.Y, bounds.X + bounds.Width / 2f, bounds.Y + bounds.Height);
            MapShape.DrawLine(g, view, this.Pen, bounds.X, bounds.Y + bounds.Height / 2f, bounds.X + bounds.Width, bounds.Y + bounds.Height / 2f);
            break;
          case MapPortStyle.Times:
            MapShape.DrawLine(g, view, this.Pen, bounds.X, bounds.Y, bounds.X + bounds.Width, bounds.Y + bounds.Height);
            MapShape.DrawLine(g, view, this.Pen, bounds.X + bounds.Width, bounds.Y, bounds.X, bounds.Y + bounds.Height);
            break;
          case MapPortStyle.PlusTimes:
            MapShape.DrawLine(g, view, this.Pen, bounds.X + bounds.Width / 2f, bounds.Y, bounds.X + bounds.Width / 2f, bounds.Y + bounds.Height);
            MapShape.DrawLine(g, view, this.Pen, bounds.X, bounds.Y + bounds.Height / 2f, bounds.X + bounds.Width, bounds.Y + bounds.Height / 2f);
            MapShape.DrawLine(g, view, this.Pen, bounds.X, bounds.Y, bounds.X + bounds.Width, bounds.Y + bounds.Height);
            MapShape.DrawLine(g, view, this.Pen, bounds.X + bounds.Width, bounds.Y, bounds.X, bounds.Y + bounds.Height);
            break;
          default:
            MapShape.DrawEllipse(g, view, this.Pen, this.Brush, bounds.X, bounds.Y, bounds.Width, bounds.Height);
            break;
        }
      }

      public virtual bool PaintGreek(Graphics g, MapView view)
      {
        float docScale = view.DocScale;
        float paintNothingScale = view.PaintNothingScale;
        float paintGreekScale = view.PaintGreekScale;
        if (view.IsPrinting)
        {
          paintNothingScale /= 4f;
          paintGreekScale /= 4f;
        }
        if ((double) docScale > (double) paintNothingScale)
        {
          if ((double) docScale > (double) paintGreekScale)
            return false;
          if (this.Style != MapPortStyle.None)
          {
            RectangleF bounds = this.Bounds;
            MapShape.DrawRectangle(g, view, this.Pen, this.Brush, bounds.X, bounds.Y, bounds.Width, bounds.Height);
          }
        }
        return true;
      }

      public virtual void RemoveLink(IMapLink link)
      {
        int index = this.myLinks.IndexOf((object) link);
        if (index < 0)
          return;
        this.myLinks.RemoveAt(index);
        this.Changed(1710, 0, (object) link, MapObject.NullRect, 0, (object) link, MapObject.NullRect);
        this.OnLinkChanged(link, 1710, 0, (object) link, MapObject.NullRect, 0, (object) link, MapObject.NullRect);
      }

      [Category("Behavior")]
      [DefaultValue(true)]
      [Description("Whether removing a port from its document causes its attached links to be removed too.")]
      public virtual bool ClearsLinksWhenRemoved
      {
        get => (this.InternalFlags & 33554432 /*0x02000000*/) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 33554432 /*0x02000000*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 33554432 /*0x02000000*/;
          else
            this.InternalFlags &= -33554433;
          this.Changed(1714, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("Gets an enumerator over the links going out of this port.")]
      public virtual MapPortFilteredLinkEnumerator DestinationLinks
      {
        get => new MapPortFilteredLinkEnumerator((IMapPort) this, this.myLinks, true);
      }

      [Description("The number of links going out of this port.")]
      public virtual int DestinationLinksCount
      {
        get
        {
          int destinationLinksCount = 0;
          foreach (IMapLink destinationLink in this.DestinationLinks)
          {
            if (destinationLink != null)
              ++destinationLinksCount;
          }
          return destinationLinksCount;
        }
      }

      [Description("The length of the link segment closest to this port.")]
      public virtual float EndSegmentLength
      {
        get => this.myEndSegmentLength;
        set
        {
          float endSegmentLength = this.myEndSegmentLength;
          if ((double) endSegmentLength == (double) value)
            return;
          this.myEndSegmentLength = value;
          this.Changed(1712, 0, (object) null, MapObject.MakeRect(endSegmentLength), 0, (object) null, MapObject.MakeRect(value));
          this.LinksOnPortChanged(1712, 0, (object) null, MapObject.MakeRect(endSegmentLength), 0, (object) null, MapObject.MakeRect(value));
        }
      }

      [Category("Appearance")]
      [Description("The spot for attaching links coming out from this port.")]
      [DefaultValue(64 /*0x40*/)]
      public virtual int FromSpot
      {
        get => this.myFromLinkSpot;
        set
        {
          int fromLinkSpot = this.myFromLinkSpot;
          if (fromLinkSpot == value)
            return;
          this.myFromLinkSpot = value;
          this.Changed(1707, fromLinkSpot, (object) null, MapObject.NullRect, value, (object) null, MapObject.NullRect);
          this.LinksOnPortChanged(1707, fromLinkSpot, (object) null, MapObject.NullRect, value, (object) null, MapObject.NullRect);
        }
      }

      [Description("Returns itself as a MapObject.")]
      public MapObject MapObject
      {
        get => (MapObject) this;
        set
        {
        }
      }

      [Description("Whether a valid link can be made between two ports already connected by a link.")]
      [DefaultValue(false)]
      [Category("Behavior")]
      public virtual bool IsValidDuplicateLinks
      {
        get => (this.InternalFlags & 8388608 /*0x800000*/) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 8388608 /*0x800000*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 8388608 /*0x800000*/;
          else
            this.InternalFlags &= -8388609;
          this.Changed(1711, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("A flag for whether a valid link can have this port as its FromPort.")]
      [Category("Behavior")]
      [DefaultValue(true)]
      public virtual bool IsValidFrom
      {
        get => (this.InternalFlags & 1048576 /*0x100000*/) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 1048576 /*0x100000*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 1048576 /*0x100000*/;
          else
            this.InternalFlags &= -1048577;
          this.Changed(1704, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("Whether a valid link can be made between two ports belonging to the same node.")]
      [Category("Behavior")]
      [DefaultValue(false)]
      public virtual bool IsValidSelfNode
      {
        get => (this.InternalFlags & 4194304 /*0x400000*/) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 4194304 /*0x400000*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 4194304 /*0x400000*/;
          else
            this.InternalFlags &= -4194305;
          this.Changed(1706, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Category("Behavior")]
      [Description("A flag for whether a valid link can have this port as its ToPort.")]
      [DefaultValue(true)]
      public virtual bool IsValidTo
      {
        get => (this.InternalFlags & 2097152 /*0x200000*/) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 2097152 /*0x200000*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 2097152 /*0x200000*/;
          else
            this.InternalFlags &= -2097153;
          this.Changed(1705, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("Gets an enumerator over all of the links connected at this port.")]
      public virtual MapPortLinkEnumerator Links => new MapPortLinkEnumerator(this.myLinks);

      [Description("The total number of links connected at this port.")]
      public virtual int LinksCount => this.myLinks.Count;

      internal bool NoClearLinks
      {
        get => (this.InternalFlags & 67108864 /*0x04000000*/) != 0;
        set
        {
          if (value)
            this.InternalFlags |= 67108864 /*0x04000000*/;
          else
            this.InternalFlags &= -67108865;
        }
      }

      [Description("The node that this port is part of.")]
      public virtual IMapNode Node => MapPort.FindParentNode((MapObject) this);

      [Description("The unique ID of this part in its document.")]
      [Category("Ownership")]
      public int PartID
      {
        get => this.myPartID;
        set
        {
          int partId = this.myPartID;
          if (partId == value)
            return;
          this.myPartID = value;
          this.Changed(1713, partId, (object) null, MapObject.NullRect, value, (object) null, MapObject.NullRect);
        }
      }

      [Description("The MapObject that may take this port's place and appearance or shape.")]
      public virtual MapObject PortObject
      {
        get => this.myPortObject;
        set
        {
          MapObject portObject = this.myPortObject;
          if (portObject == value)
            return;
          this.myPortObject = value;
          this.Changed(1703, 0, (object) portObject, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
          this.LinksOnPortChanged(1703, 0, (object) portObject, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("Gets an enumerator over the links coming into this port.")]
      public virtual MapPortFilteredLinkEnumerator SourceLinks
      {
        get => new MapPortFilteredLinkEnumerator((IMapPort) this, this.myLinks, false);
      }

      [Description("The number of links coming into this port.")]
      public virtual int SourceLinksCount
      {
        get
        {
          int sourceLinksCount = 0;
          foreach (IMapLink sourceLink in this.SourceLinks)
          {
            if (sourceLink != null)
              ++sourceLinksCount;
          }
          return sourceLinksCount;
        }
      }

      [Description("The appearance style.")]
      [Category("Appearance")]
      [DefaultValue(2)]
      public virtual MapPortStyle Style
      {
        get => this.myStyle;
        set
        {
          MapPortStyle style = this.myStyle;
          if (style == value)
            return;
          this.myStyle = value;
          this.Changed(1702, (int) style, (object) null, MapObject.NullRect, (int) value, (object) null, MapObject.NullRect);
          this.LinksOnPortChanged(1702, (int) style, (object) null, MapObject.NullRect, (int) value, (object) null, MapObject.NullRect);
        }
      }

      [Category("Appearance")]
      [DefaultValue(256 /*0x0100*/)]
      [Description("The spot for attaching links going into this port.")]
      public virtual int ToSpot
      {
        get => this.myToLinkSpot;
        set
        {
          switch (value)
          {
            case -24:
              this.InternalFlags &= -134217729;
              break;
            case -23:
              this.InternalFlags |= 134217728 /*0x08000000*/;
              break;
          }
          int toLinkSpot = this.myToLinkSpot;
          if (toLinkSpot == value)
            return;
          this.myToLinkSpot = value;
          this.Changed(1708, toLinkSpot, (object) null, MapObject.NullRect, value, (object) null, MapObject.NullRect);
          this.LinksOnPortChanged(1708, toLinkSpot, (object) null, MapObject.NullRect, value, (object) null, MapObject.NullRect);
        }
      }

      [Description("An integer value associated with this port.")]
      [DefaultValue(0)]
      public virtual int UserFlags
      {
        get => this.myUserFlags;
        set
        {
          int userFlags = this.myUserFlags;
          if (userFlags == value)
            return;
          this.myUserFlags = value;
          this.Changed(1700, userFlags, (object) null, MapObject.NullRect, value, (object) null, MapObject.NullRect);
        }
      }

      [Description("An object associated with this port.")]
      [DefaultValue(null)]
      public virtual object UserObject
      {
        get => this.myUserObject;
        set
        {
          object userObject = this.myUserObject;
          if (userObject == value)
            return;
          this.myUserObject = value;
          this.Changed(1701, 0, userObject, MapObject.NullRect, 0, value, MapObject.NullRect);
        }
      }
    }
}
