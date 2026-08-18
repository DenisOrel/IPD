// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapToolLinking
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Map
{
    [Serializable]
    public abstract class MapToolLinking : MapTool
    {
      public static readonly object Invalid;
      [NonSerialized]
      private bool myForwards;
      private bool myForwardsOnly;
      [NonSerialized]
      private bool myLinkingNew;
      [NonSerialized]
      private IMapPort myOrigEndPort;
      [NonSerialized]
      private IMapPort myOrigStartPort;
      private bool myOrthogonal;
      private bool myOrthogonalSet;
      [NonSerialized]
      private IMapPort myTempEndPort;
      [NonSerialized]
      private IMapLink myTempLink;
      [NonSerialized]
      private IMapPort myTempStartPort;
      [NonSerialized]
      private Hashtable myValidPortsCache;
      public static readonly object Valid = (object) nameof (Valid);

      static MapToolLinking() => MapToolLinking.Invalid = (object) nameof (Invalid);

      protected MapToolLinking(MapView v)
        : base(v)
      {
        this.myForwardsOnly = false;
        this.myOrthogonal = false;
        this.myOrthogonalSet = false;
        this.myLinkingNew = true;
        this.myForwards = true;
        this.myOrigStartPort = (IMapPort) null;
        this.myOrigEndPort = (IMapPort) null;
        this.myTempStartPort = (IMapPort) null;
        this.myTempEndPort = (IMapPort) null;
        this.myTempLink = (IMapLink) null;
        this.myValidPortsCache = new Hashtable();
      }

      protected virtual IMapLink CreateTemporaryLink(IMapPort fromPort, IMapPort toPort)
      {
        IMapLink instance = (IMapLink) Activator.CreateInstance(this.View.NewLinkClass);
        if (instance == null || instance.MapObject == null)
          return (IMapLink) null;
        instance.FromPort = fromPort;
        instance.ToPort = toPort;
        MapObject mapObject = instance.MapObject;
        switch (mapObject)
        {
          case MapLink _:
            MapLink mapLink = (MapLink) mapObject;
            if (this.myOrthogonalSet)
              mapLink.Orthogonal = this.Orthogonal;
            mapLink.AdjustingStyle = MapLinkAdjustingStyle.Calculate;
            break;
          case MapLabeledLink _:
            MapLabeledLink mapLabeledLink = (MapLabeledLink) mapObject;
            if (this.myOrthogonalSet)
              mapLabeledLink.Orthogonal = this.Orthogonal;
            mapLabeledLink.AdjustingStyle = MapLinkAdjustingStyle.Calculate;
            break;
        }
        this.View.Layers.Default.Add(mapObject);
        return instance;
      }

      protected virtual IMapPort CreateTemporaryPort(
        IMapPort port,
        PointF pnt,
        bool forToPort,
        bool atEnd)
      {
        int num = port == null ? 0 : (port.MapObject != null ? 1 : 0);
        MapToolLinking.MapTemporaryPort temporaryPort = new MapToolLinking.MapTemporaryPort();
        temporaryPort.Target = port as MapPort;
        if (num != 0)
          temporaryPort.Size = port.MapObject.Size;
        temporaryPort.Center = pnt;
        temporaryPort.Style = MapPortStyle.None;
        this.View.Layers.Default.Add((MapObject) temporaryPort);
        return (IMapPort) temporaryPort;
      }

      public override void DoCancelMouse()
      {
        if (this.myLinkingNew)
        {
          if (this.Forwards)
            this.DoNoNewLink(this.StartPort, (IMapPort) null);
          else
            this.DoNoNewLink((IMapPort) null, this.StartPort);
        }
        else if (this.OriginalEndPort == null)
        {
          if (this.Forwards)
            this.DoNoRelink(this.Link, this.StartPort, (IMapPort) null);
          else
            this.DoNoRelink(this.Link, (IMapPort) null, this.StartPort);
        }
        else if (this.Forwards)
          this.DoCancelRelink(this.Link, this.OriginalStartPort, this.OriginalEndPort);
        else
          this.DoCancelRelink(this.Link, this.OriginalEndPort, this.OriginalStartPort);
        this.View.Cursor = this.View.DefaultCursor;
        this.StopTool();
      }

      public virtual void DoCancelRelink(IMapLink oldlink, IMapPort fromPort, IMapPort toPort)
      {
        oldlink.FromPort = fromPort;
        oldlink.ToPort = toPort;
        this.TransactionResult = (string) null;
      }

      public virtual void DoLinking(PointF dc)
      {
        if (this.EndPort == null)
          return;
        MapObject mapObject = this.EndPort.MapObject;
        if (mapObject == null)
          return;
        IMapPort mapPort = this.PickNearestPort(dc);
        if (mapObject is MapToolLinking.MapTemporaryPort mapTemporaryPort)
          mapTemporaryPort.Target = mapPort as MapPort;
        RectangleF rectangleF = mapPort == null || mapPort.MapObject == null ? new RectangleF(dc.X, dc.Y, 0.0f, 0.0f) : mapPort.MapObject.Bounds;
        mapObject.Bounds = rectangleF;
      }

      /// <summary>действия когда мышь двигают</summary>
      public override void DoMouseMove()
      {
        this.DoLinking(this.LastInput.DocPoint);
        this.View.DoAutoScroll(this.LastInput.ViewPoint);
      }

      /// <summary>действия когда клавиша мыши отпущена</summary>
      public override void DoMouseUp()
      {
        IMapPort mapPort1 = this.PickNearestPort(this.LastInput.DocPoint);
        if (mapPort1 != null)
        {
          if (this.myLinkingNew)
          {
            if (this.Forwards)
              this.DoNewLink(this.OriginalStartPort, mapPort1);
            else
              this.DoNewLink(mapPort1, this.OriginalStartPort);
          }
          else if (this.Forwards)
            this.DoRelink(this.Link, this.OriginalStartPort, mapPort1);
          else
            this.DoRelink(this.Link, mapPort1, this.OriginalStartPort);
        }
        else
        {
          IMapPort mapPort2 = this.PickPort(this.LastInput.DocPoint);
          if (this.myLinkingNew)
          {
            if (this.Forwards)
              this.DoNoNewLink(this.OriginalStartPort, mapPort2);
            else
              this.DoNoNewLink(mapPort2, this.OriginalStartPort);
          }
          else if (this.Forwards)
            this.DoNoRelink(this.Link, this.OriginalStartPort, mapPort2);
          else
            this.DoNoRelink(this.Link, mapPort2, this.OriginalStartPort);
        }
        this.StopTool();
      }

      public virtual void DoNewLink(IMapPort fromPort, IMapPort toPort)
      {
        IMapLink link = this.View.CreateLink(fromPort, toPort);
        if (link != null)
        {
          this.TransactionResult = "New Link";
          this.View.RaiseLinkCreated(link.MapObject);
        }
        else
          this.TransactionResult = (string) null;
      }

      public virtual void DoNoNewLink(IMapPort fromPort, IMapPort toPort)
      {
        this.TransactionResult = (string) null;
      }

      public virtual void DoNoRelink(IMapLink oldlink, IMapPort fromPort, IMapPort toPort)
      {
        MapObject mapObject = oldlink.MapObject;
        if (mapObject != null && mapObject.Layer != null)
        {
          if (mapObject.Movable)
          {
            oldlink.FromPort = fromPort;
            oldlink.ToPort = toPort;
            this.TransactionResult = "Relink";
            this.View.RaiseLinkRelinked(oldlink.MapObject);
            return;
          }
          if (mapObject.CanDelete())
          {
            CancelEventArgs evt = new CancelEventArgs();
            this.View.RaiseSelectionDeleting(evt);
            if (!evt.Cancel)
            {
              mapObject.Remove();
              this.View.RaiseSelectionDeleted();
              this.TransactionResult = "Relink";
              return;
            }
            this.DoCancelMouse();
          }
          else
            this.DoCancelMouse();
        }
        this.TransactionResult = (string) null;
      }

      public virtual void DoRelink(IMapLink oldlink, IMapPort fromPort, IMapPort toPort)
      {
        oldlink.FromPort = fromPort;
        oldlink.ToPort = toPort;
        MapSubGraph.ReparentToCommonSubGraph(oldlink.MapObject, fromPort?.MapObject, toPort?.MapObject, true, this.View.Document.LinksLayer);
        this.TransactionResult = "Relink";
        this.View.RaiseLinkRelinked(oldlink.MapObject);
      }

      public virtual bool IsValidFromPort(IMapPort fromPort) => fromPort.CanLinkFrom();

      public virtual bool IsValidLink(IMapPort fromPort, IMapPort toPort)
      {
        return fromPort == null || toPort == null || fromPort.IsValidLink(toPort);
      }

      public virtual bool IsValidToPort(IMapPort toPort) => !this.ForwardsOnly && toPort.CanLinkTo();

      public virtual IMapPort PickNearestPort(PointF dc)
      {
        IMapPort bestPort = (IMapPort) null;
        float portGravity = this.View.PortGravity;
        float bestDist = portGravity * portGravity;
        foreach (MapLayer backward1 in this.View.Layers.Backwards)
        {
          if (backward1.IsInDocument && backward1.CanViewObjects())
          {
            foreach (MapObject backward2 in backward1.Backwards)
              bestPort = this.pickNearestPort1(backward2, dc, bestPort, ref bestDist);
          }
        }
        return bestPort;
      }

      private IMapPort pickNearestPort1(
        MapObject obj,
        PointF dc,
        IMapPort bestPort,
        ref float bestDist)
      {
        if (obj is IMapPort mapPort)
        {
          PointF pointF = this.PortPoint(mapPort, dc);
          double num1 = (double) dc.X - (double) pointF.X;
          float num2 = dc.Y - pointF.Y;
          float num3 = (float) (num1 * num1 + (double) num2 * (double) num2);
          if ((double) num3 <= (double) bestDist)
          {
            object obj1 = (object) null;
            if (this.ValidPortsCache != null)
              obj1 = this.ValidPortsCache[(object) mapPort];
            if (obj1 == MapToolLinking.Valid)
            {
              bestPort = mapPort;
              bestDist = num3;
            }
            else if (obj1 != MapToolLinking.Invalid)
            {
              if (this.Forwards && this.IsValidLink(this.OriginalStartPort, mapPort) || !this.Forwards && this.IsValidLink(mapPort, this.OriginalStartPort))
              {
                if (this.ValidPortsCache != null)
                  this.ValidPortsCache[(object) mapPort] = MapToolLinking.Valid;
                bestPort = mapPort;
                bestDist = num3;
              }
              else if (this.ValidPortsCache != null)
                this.ValidPortsCache[(object) mapPort] = MapToolLinking.Invalid;
            }
          }
        }
        if (obj is MapGroup mapGroup)
        {
          foreach (MapObject mapObject in mapGroup.GetEnumerator())
            bestPort = this.pickNearestPort1(mapObject, dc, bestPort, ref bestDist);
        }
        return bestPort;
      }

      public virtual IMapPort PickPort(PointF dc)
      {
        return this.View.PickObject(true, false, dc, false) as IMapPort;
      }

      public virtual PointF PortPoint(IMapPort port, PointF dc)
      {
        if (!(port.MapObject is MapPort mapObject1))
          return port.MapObject.Center;
        MapObject mapObject2 = mapObject1.PortObject;
        if (mapObject2 == null || mapObject2.Layer == null)
          mapObject2 = (MapObject) mapObject1;
        SizeF size = mapObject2.Size;
        return (double) size.Width < 10.0 && (double) size.Height < 10.0 ? mapObject2.Center : mapObject1.GetLinkPointFromPoint(dc);
      }

      public virtual void StartNewLink(IMapPort port, PointF dc)
      {
        if (port == null)
          return;
        this.StartTransaction();
        this.myLinkingNew = true;
        if (this.IsValidFromPort(port))
        {
          this.Forwards = true;
          this.StartPort = this.CreateTemporaryPort(port, port.MapObject.Center, false, false);
          this.EndPort = this.CreateTemporaryPort(port, dc, true, true);
          this.Link = this.CreateTemporaryLink(this.StartPort, this.EndPort);
        }
        else
        {
          this.Forwards = false;
          this.StartPort = this.CreateTemporaryPort(port, port.MapObject.Center, true, false);
          this.EndPort = this.CreateTemporaryPort(port, dc, false, true);
          this.Link = this.CreateTemporaryLink(this.EndPort, this.StartPort);
        }
        this.View.Cursor = Cursors.Hand;
      }

      public virtual void StartRelink(IMapLink oldlink, IMapPort oldport, PointF dc)
      {
        if (oldlink == null)
          return;
        MapObject mapObject = oldlink.MapObject;
        if (mapObject == null || mapObject.Layer == null)
          return;
        this.StartTransaction();
        this.myLinkingNew = false;
        this.OriginalEndPort = oldport;
        this.Link = oldlink;
        if (oldlink.ToPort == oldport)
        {
          this.Forwards = true;
          this.OriginalStartPort = oldlink.FromPort;
          PointF pnt = dc;
          if (this.OriginalStartPort != null)
          {
            pnt = this.OriginalStartPort.MapObject.Center;
          }
          else
          {
            switch (oldlink)
            {
              case MapLink _:
                MapLink mapLink = (MapLink) oldlink;
                if (mapLink.PointsCount > 0)
                {
                  pnt = mapLink.GetPoint(0);
                  break;
                }
                break;
              case MapLabeledLink _:
                MapLabeledLink mapLabeledLink = (MapLabeledLink) oldlink;
                if (mapLabeledLink.RealLink.PointsCount > 0)
                {
                  pnt = mapLabeledLink.RealLink.GetPoint(0);
                  break;
                }
                break;
            }
          }
          this.StartPort = this.CreateTemporaryPort(this.OriginalStartPort, pnt, false, false);
          oldlink.FromPort = this.StartPort;
          this.EndPort = this.CreateTemporaryPort(this.OriginalEndPort, dc, true, true);
          oldlink.ToPort = this.EndPort;
        }
        else if (oldlink.FromPort == oldport)
        {
          this.Forwards = false;
          this.OriginalStartPort = oldlink.ToPort;
          PointF pnt = dc;
          if (this.OriginalStartPort != null)
          {
            pnt = this.OriginalStartPort.MapObject.Center;
          }
          else
          {
            switch (oldlink)
            {
              case MapLink _:
                MapLink mapLink = (MapLink) oldlink;
                if (mapLink.PointsCount > 0)
                {
                  pnt = mapLink.GetPoint(mapLink.PointsCount - 1);
                  break;
                }
                break;
              case MapLabeledLink _:
                MapLabeledLink mapLabeledLink = (MapLabeledLink) oldlink;
                if (mapLabeledLink.RealLink.PointsCount > 0)
                {
                  pnt = mapLabeledLink.RealLink.GetPoint(mapLabeledLink.RealLink.PointsCount - 1);
                  break;
                }
                break;
            }
          }
          this.StartPort = this.CreateTemporaryPort(this.OriginalStartPort, pnt, true, false);
          oldlink.ToPort = this.StartPort;
          this.EndPort = this.CreateTemporaryPort(this.OriginalEndPort, dc, false, true);
          oldlink.FromPort = this.EndPort;
        }
        this.View.Cursor = Cursors.Hand;
      }

      public override void Stop()
      {
        this.View.StopAutoScroll();
        this.Forwards = true;
        this.OriginalStartPort = (IMapPort) null;
        this.OriginalEndPort = (IMapPort) null;
        if (this.Link != null)
        {
          MapObject mapObject = this.Link.MapObject;
          if (mapObject != null && mapObject.IsInView)
            mapObject.Remove();
        }
        this.Link = (IMapLink) null;
        if (this.StartPort != null)
        {
          MapObject mapObject = this.StartPort.MapObject;
          if (mapObject != null && mapObject.IsInView)
            mapObject.Remove();
        }
        this.StartPort = (IMapPort) null;
        if (this.EndPort != null)
        {
          MapObject mapObject = this.EndPort.MapObject;
          if (mapObject != null && mapObject.IsInView)
            mapObject.Remove();
        }
        this.EndPort = (IMapPort) null;
        if (this.ValidPortsCache != null)
          this.ValidPortsCache.Clear();
        this.StopTransaction();
      }

      public IMapPort EndPort
      {
        get => this.myTempEndPort;
        set => this.myTempEndPort = value;
      }

      public bool Forwards
      {
        get => this.myForwards;
        set => this.myForwards = value;
      }

      public virtual bool ForwardsOnly
      {
        get => this.myForwardsOnly;
        set => this.myForwardsOnly = value;
      }

      public IMapLink Link
      {
        get => this.myTempLink;
        set => this.myTempLink = value;
      }

      public IMapPort OriginalEndPort
      {
        get => this.myOrigEndPort;
        set => this.myOrigEndPort = value;
      }

      public IMapPort OriginalStartPort
      {
        get => this.myOrigStartPort;
        set => this.myOrigStartPort = value;
      }

      public virtual bool Orthogonal
      {
        get => this.myOrthogonal;
        set
        {
          this.myOrthogonal = value;
          this.myOrthogonalSet = true;
        }
      }

      public IMapPort StartPort
      {
        get => this.myTempStartPort;
        set => this.myTempStartPort = value;
      }

      public Hashtable ValidPortsCache
      {
        get => this.myValidPortsCache;
        set => this.myValidPortsCache = value;
      }

      internal class MapTemporaryPort : MapPort
      {
        private MapPort myTargetPort;

        internal MapTemporaryPort()
        {
          this.myTargetPort = (MapPort) null;
          this.PortObject = (MapObject) null;
          this.FromSpot = 0;
          this.ToSpot = 0;
          this.Size = new SizeF();
        }

        public override float GetFromLinkDir(IMapLink link)
        {
          return this.Target != null ? this.Target.GetFromLinkDir(link) : base.GetFromLinkDir(link);
        }

        public override PointF GetFromLinkPoint(IMapLink link)
        {
          return this.Target != null ? this.Target.GetFromLinkPoint(link) : base.GetFromLinkPoint(link);
        }

        public override PointF GetLinkPointFromPoint(PointF p)
        {
          return this.Target != null ? this.Target.GetLinkPointFromPoint(p) : base.GetLinkPointFromPoint(p);
        }

        public override float GetToLinkDir(IMapLink link)
        {
          return this.Target != null ? this.Target.GetToLinkDir(link) : base.GetToLinkDir(link);
        }

        public override PointF GetToLinkPoint(IMapLink link)
        {
          return this.Target != null ? this.Target.GetToLinkPoint(link) : base.GetToLinkPoint(link);
        }

        public override float EndSegmentLength
        {
          get => this.Target != null ? this.Target.EndSegmentLength : base.EndSegmentLength;
        }

        public override int FromSpot => this.Target != null ? this.Target.FromSpot : base.FromSpot;

        public override MapObject PortObject
        {
          get => this.Target != null ? this.Target.PortObject : base.PortObject;
        }

        internal MapPort Target
        {
          get => this.myTargetPort;
          set => this.myTargetPort = value;
        }

        public override int ToSpot => this.Target != null ? this.Target.ToSpot : base.ToSpot;
      }
    }
}
