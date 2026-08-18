// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapSubGraph
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace Intermech.Map
{
    [Serializable]
    public class MapSubGraph : MapNode
    {
      public const int ChangedCorner = 2710;
      public const int ChangedBackgroundColor = 2704;
      public const int ChangedBorderPen = 2708;
      public const int ChangedBottomRightMargin = 2712;
      public const int ChangedCollapsedBottomRightMargin = 2714;
      public const int ChangedCollapsedCorner = 2715;
      public const int ChangedCollapsedLabelSpot = 2716;
      public const int ChangedCollapsedObject = 2717;
      public const int ChangedCollapsedTopLeftMargin = 2713;
      public const int ChangedCollapsible = 2703;
      public const int ChangedLabel = 2702;
      public const int ChangedLabelSpot = 2706;
      public const int ChangedOpacity = 2705;
      public const int ChangedPickableBackground = 2709;
      public const int ChangedPort = 2711;
      public const int ChangedState = 2718;
      public const int ChangedTopLeftMargin = 2707;
      private const int flagCollapsible = 33554432 /*0x02000000*/;
      private const int flagExpandedResizable = 134217728 /*0x08000000*/;
      private const int flagIgnoreLabel = 16777216 /*0x01000000*/;
      private const int flagPickableBackground = 67108864 /*0x04000000*/;
      private Color myBackgroundColor;
      private MapShape.MapPenInfo myBorderPenInfo;
      private SizeF myBottomRightMargin;
      private Hashtable myBoundsHashtable;
      private SizeF myCollapsedBottomRightMargin;
      private SizeF myCollapsedCorner;
      private int myCollapsedLabelSpot;
      private MapObject myCollapsedObject;
      private SizeF myCollapsedTopLeftMargin;
      private SizeF myCorner;
      private MapSubGraphHandle myHandle;
      private MapText myLabel;
      private int myLabelSpot;
      private float myOpacity;
      private Hashtable myPathsHashtable;
      private MapPort myPort;
      private MapSubGraphState myState;
      private SizeF myTopLeftMargin;

      public MapSubGraph()
      {
        this.myState = MapSubGraphState.Expanded;
        this.myHandle = (MapSubGraphHandle) null;
        this.myLabel = (MapText) null;
        this.myPort = (MapPort) null;
        this.myCollapsedObject = (MapObject) null;
        this.myBackgroundColor = Color.LightBlue;
        this.myOpacity = 25f;
        this.myLabelSpot = 32 /*0x20*/;
        this.myCollapsedLabelSpot = 1;
        this.myCorner = new SizeF(0.0f, 0.0f);
        this.myCollapsedCorner = new SizeF(0.0f, 0.0f);
        this.myTopLeftMargin = new SizeF(4f, 4f);
        this.myBottomRightMargin = new SizeF(4f, 4f);
        this.myCollapsedTopLeftMargin = new SizeF(0.0f, 0.0f);
        this.myCollapsedBottomRightMargin = new SizeF(0.0f, 0.0f);
        this.myBorderPenInfo = MapShape.GetPenInfo((Pen) null);
        this.myBoundsHashtable = new Hashtable();
        this.myPathsHashtable = new Hashtable();
        this.InternalFlags |= 131072 /*0x020000*/;
        this.InternalFlags |= 33554432 /*0x02000000*/;
        this.InternalFlags &= -17;
        this.myHandle = this.CreateHandle();
        this.Add((MapObject) this.myHandle);
        this.myCollapsedObject = this.CreateCollapsedObject();
        this.Add(this.myCollapsedObject);
        this.myLabel = this.CreateLabel();
        this.Add((MapObject) this.myLabel);
        this.myPort = this.CreatePort();
        this.InsertBefore((MapObject) null, (MapObject) this.myPort);
        this.Initializing = false;
        this.LayoutChildren((MapObject) null);
      }

      public override void Add(MapObject obj)
      {
        if (this.Handle != null && this.Count >= 1)
          this.InsertBefore((MapObject) this.Handle, obj);
        else
          base.Add(obj);
      }

      public override void ChangeValue(MapChangedEventArgs e, bool undo)
      {
        switch (e.SubHint)
        {
          case 2702:
            this.Label = (MapText) e.GetValue(undo);
            break;
          case 2703:
            this.Collapsible = (bool) e.GetValue(undo);
            break;
          case 2704:
            this.BackgroundColor = (Color) e.GetValue(undo);
            break;
          case 2705:
            this.Opacity = e.GetFloat(undo);
            break;
          case 2706:
            this.setLabelSpot(e.GetInt(undo), true);
            break;
          case 2707:
            this.setTopLeftMargin(e.GetSize(undo), true);
            break;
          case 2708:
            object obj = e.GetValue(undo);
            switch (obj)
            {
              case Pen _:
                this.BorderPen = (Pen) obj;
                return;
              case MapShape.MapPenInfo _:
                this.BorderPen = ((MapShape.MapPenInfo) obj).GetPen();
                return;
              default:
                return;
            }
          case 2709:
            this.PickableBackground = (bool) e.GetValue(undo);
            break;
          case 2710:
            this.Corner = e.GetSize(undo);
            break;
          case 2711:
            this.Port = (MapPort) e.GetValue(undo);
            break;
          case 2712:
            this.setBottomRightMargin(e.GetSize(undo), true);
            break;
          case 2713:
            this.setCollapsedTopLeftMargin(e.GetSize(undo), true);
            break;
          case 2714:
            this.setCollapsedBottomRightMargin(e.GetSize(undo), true);
            break;
          case 2715:
            this.CollapsedCorner = e.GetSize(undo);
            break;
          case 2716:
            this.setCollapsedLabelSpot(e.GetInt(undo), true);
            break;
          case 2717:
            this.CollapsedObject = (MapObject) e.GetValue(undo);
            break;
          case 2718:
            MapSubGraphState mapSubGraphState = (MapSubGraphState) e.GetInt(undo);
            this.State = mapSubGraphState;
            this.Initializing = mapSubGraphState == MapSubGraphState.Collapsing || mapSubGraphState == MapSubGraphState.Expanding;
            if (!this.Initializing)
              break;
            base.ChangeValue(new MapChangedEventArgs(e)
            {
              SubHint = 1001
            }, undo);
            break;
          default:
            base.ChangeValue(e, undo);
            break;
        }
      }

      public virtual void Collapse()
      {
        if (this.State != MapSubGraphState.Expanded || !this.Collapsible)
          return;
        this.State = MapSubGraphState.Collapsing;
        this.Initializing = true;
        this.PrepareCollapse();
        RectangleF collapsedRectangle = this.ComputeCollapsedRectangle(this.ComputeCollapsedSize(true));
        foreach (MapObject child in (MapGroup) this)
          this.SaveChildBounds(child, collapsedRectangle);
        foreach (MapObject child in (MapGroup) this)
          this.CollapseChild(child, collapsedRectangle);
        this.FinishCollapse(collapsedRectangle);
        this.Initializing = false;
        this.InvalidBounds = true;
        this.State = MapSubGraphState.Collapsed;
        this.LayoutChildren((MapObject) null);
      }

      protected virtual void CollapseChild(MapObject child, RectangleF sgrect)
      {
        if (child == this.Handle || child == this.Label || child == this.Port || child == this.CollapsedObject)
          return;
        if (!(child is IMapLink))
        {
          PointF a = new PointF(sgrect.X + sgrect.Width / 2f, sgrect.Y + sgrect.Height / 2f);
          if (child is MapSubGraph mapSubGraph)
          {
            SizeF collapsedSize = mapSubGraph.ComputeCollapsedSize(false);
            RectangleF collapsedRectangle = mapSubGraph.ComputeCollapsedRectangle(collapsedSize);
            PointF b = new PointF(collapsedRectangle.X + collapsedRectangle.Width / 2f, collapsedRectangle.Y + collapsedRectangle.Height / 2f);
            SizeF sizeF = MapTool.SubtractPoints(a, b);
            mapSubGraph.Position = new PointF(mapSubGraph.Left + sizeF.Width, mapSubGraph.Top + sizeF.Height);
          }
          else
            child.Center = a;
        }
        child.Visible = false;
      }

      protected override RectangleF ComputeBounds()
      {
        RectangleF a = this.Bounds;
        if (!this.Initializing)
        {
          bool flag = false;
          foreach (MapObject child in (MapGroup) this)
          {
            if (!this.ComputeBoundsSkip(child))
            {
              RectangleF b;
              if (child is MapSubGraph mapSubGraph && !mapSubGraph.CanView())
              {
                SizeF collapsedSize = mapSubGraph.ComputeCollapsedSize(false);
                b = mapSubGraph.ComputeCollapsedRectangle(collapsedSize);
              }
              else
                b = child.Bounds;
              if (!flag)
              {
                a = b;
                flag = true;
              }
              else
                a = MapObject.UnionRect(a, b);
            }
          }
          if (!flag)
            return a;
          SizeF sizeF;
          SizeF bottomRightMargin;
          if (this.IsExpanded)
          {
            sizeF = this.TopLeftMargin;
            bottomRightMargin = this.BottomRightMargin;
          }
          else
          {
            sizeF = this.CollapsedTopLeftMargin;
            bottomRightMargin = this.CollapsedBottomRightMargin;
          }
          a.X -= sizeF.Width;
          a.Y -= sizeF.Height;
          a.Width += sizeF.Width + bottomRightMargin.Width;
          a.Height += sizeF.Height + bottomRightMargin.Height;
        }
        return a;
      }

      protected virtual bool ComputeBoundsSkip(MapObject child)
      {
        if (child == this.Handle)
          return true;
        if (child == this.Label)
          return (this.InternalFlags & 16777216 /*0x01000000*/) != 0 || !child.CanView();
        if (child == this.Port)
          return true;
        if (child == this.CollapsedObject)
          return !child.CanView();
        return child is IMapLink mapLink && (!child.CanView() || this.Port != null && (mapLink.FromPort == this.Port || mapLink.ToPort == this.Port));
      }

      protected virtual RectangleF ComputeCollapsedRectangle(SizeF s)
      {
        PointF referencePoint = this.ComputeReferencePoint();
        return new RectangleF(referencePoint.X, referencePoint.Y, s.Width, s.Height);
      }

      public virtual SizeF ComputeCollapsedSize(bool visible)
      {
        SizeF collapsedSize = new SizeF(0.0f, 0.0f);
        if (visible && this.CollapsedObject != null)
          collapsedSize = this.CollapsedObject.Size;
        foreach (MapObject child in (MapGroup) this)
        {
          if (!this.ComputeCollapsedSizeSkip(child))
          {
            SizeF sizeF = child.Size;
            if (child is MapSubGraph mapSubGraph)
              sizeF = mapSubGraph.ComputeCollapsedSize(false);
            collapsedSize.Width = Math.Max(collapsedSize.Width, sizeF.Width);
            collapsedSize.Height = Math.Max(collapsedSize.Height, sizeF.Height);
          }
        }
        return collapsedSize;
      }

      protected virtual bool ComputeCollapsedSizeSkip(MapObject child)
      {
        return child == this.Handle || child == this.Label || child == this.Port || child == this.CollapsedObject || child is IMapLink;
      }

      protected virtual PointF ComputeReferencePoint()
      {
        return this.Handle != null ? this.Handle.Position : this.Position;
      }

      protected override void CopyChildren(MapGroup newgroup, MapCopyDictionary env)
      {
        MapSubGraph mapSubGraph = (MapSubGraph) newgroup;
        mapSubGraph.myHandle = (MapSubGraphHandle) null;
        mapSubGraph.myLabel = (MapText) null;
        mapSubGraph.myPort = (MapPort) null;
        mapSubGraph.myCollapsedObject = (MapObject) null;
        foreach (MapObject mapObject in (MapGroup) this)
          env.Copy(mapObject);
        foreach (MapObject key in (MapGroup) this)
        {
          MapObject mapObject = (MapObject) env[(object) key];
          mapSubGraph.Add(mapObject);
          if (key == this.myHandle)
            mapSubGraph.myHandle = (MapSubGraphHandle) mapObject;
          else if (key == this.myLabel)
            mapSubGraph.myLabel = (MapText) mapObject;
          else if (key == this.myPort)
            mapSubGraph.myPort = (MapPort) mapObject;
          else if (key == this.myCollapsedObject)
            mapSubGraph.myCollapsedObject = mapObject;
        }
        mapSubGraph.myBoundsHashtable = new Hashtable();
        IDictionaryEnumerator enumerator1 = this.myBoundsHashtable.GetEnumerator();
        while (enumerator1.MoveNext())
        {
          MapObject key1 = (MapObject) enumerator1.Key;
          MapObject key2 = (MapObject) env[(object) key1];
          if (key2 != null)
          {
            RectangleF rectangleF = (RectangleF) enumerator1.Value;
            mapSubGraph.myBoundsHashtable[(object) key2] = (object) rectangleF;
          }
        }
        mapSubGraph.myPathsHashtable = new Hashtable();
        IDictionaryEnumerator enumerator2 = this.myPathsHashtable.GetEnumerator();
        while (enumerator2.MoveNext())
        {
          MapStroke key3 = (MapStroke) enumerator2.Key;
          MapStroke key4 = (MapStroke) env[(object) key3];
          if (key4 != null)
          {
            PointF[] pointFArray = (PointF[]) enumerator2.Value;
            mapSubGraph.myPathsHashtable[(object) key4] = pointFArray.Clone();
          }
        }
      }

      protected virtual MapObject CreateCollapsedObject() => (MapObject) null;

      protected virtual MapSubGraphHandle CreateHandle() => new MapSubGraphHandle();

      protected virtual MapText CreateLabel()
      {
        MapText label = new MapText();
        label.Selectable = false;
        label.Alignment = 128 /*0x80*/;
        label.Wrapping = true;
        label.Bold = true;
        label.Editable = true;
        return label;
      }

      protected virtual MapPort CreatePort() => (MapPort) null;

      public override void DoResize(
        MapView view,
        RectangleF origRect,
        PointF newPoint,
        int whichHandle,
        MapInputState evttype,
        SizeF min,
        SizeF max)
      {
        RectangleF bounds = this.ComputeBounds();
        SizeF sizeF1;
        SizeF bottomRightMargin;
        if (this.IsExpanded)
        {
          sizeF1 = this.TopLeftMargin;
          bottomRightMargin = this.BottomRightMargin;
        }
        else
        {
          sizeF1 = this.CollapsedTopLeftMargin;
          bottomRightMargin = this.CollapsedBottomRightMargin;
        }
        bounds.X += sizeF1.Width;
        bounds.Y += sizeF1.Height;
        bounds.Width -= sizeF1.Width + bottomRightMargin.Width;
        bounds.Height -= sizeF1.Height + bottomRightMargin.Height;
        RectangleF r = evttype != MapInputState.Cancel ? this.ComputeResize(origRect, newPoint, whichHandle, new SizeF(bounds.Width, bounds.Height), max, true) : origRect;
        if (this.ResizesRealtime || evttype == MapInputState.Cancel)
        {
          SizeF sizeF2 = new SizeF(Math.Max(0.0f, r.Right - bounds.Right), Math.Max(0.0f, r.Bottom - bounds.Bottom));
          SizeF sizeF3 = new SizeF(Math.Max(0.0f, bounds.X - r.X), Math.Max(0.0f, bounds.Y - r.Y));
          if (this.IsExpanded)
          {
            this.BottomRightMargin = sizeF2;
            this.TopLeftMargin = sizeF3;
          }
          else
          {
            this.CollapsedBottomRightMargin = sizeF2;
            this.CollapsedTopLeftMargin = sizeF3;
          }
        }
        else
        {
          Rectangle view1 = view.ConvertDocToView(r);
          if (evttype != MapInputState.Finish)
            view.DrawXorBox(view1);
          if (evttype != MapInputState.Finish)
            return;
          SizeF sizeF4 = new SizeF(Math.Max(0.0f, r.Right - bounds.Right), Math.Max(0.0f, r.Bottom - bounds.Bottom));
          SizeF sizeF5 = new SizeF(Math.Max(0.0f, bounds.X - r.X), Math.Max(0.0f, bounds.Y - r.Y));
          if (this.IsExpanded)
          {
            this.BottomRightMargin = sizeF4;
            this.TopLeftMargin = sizeF5;
          }
          else
          {
            this.CollapsedBottomRightMargin = sizeF4;
            this.CollapsedTopLeftMargin = sizeF5;
          }
        }
      }

      public virtual void Expand()
      {
        if (this.State != MapSubGraphState.Collapsed || !this.Collapsible)
          return;
        this.State = MapSubGraphState.Expanding;
        this.Initializing = true;
        this.PrepareExpand();
        PointF referencePoint = this.ComputeReferencePoint();
        foreach (MapObject child in (MapGroup) this)
          this.ExpandChild(child, referencePoint);
        this.FinishExpand(referencePoint);
        this.Initializing = false;
        this.InvalidBounds = true;
        this.State = MapSubGraphState.Expanded;
        this.LayoutChildren((MapObject) null);
      }

      public virtual void ExpandAll()
      {
        this.Expand();
        foreach (MapObject mapObject in (MapGroup) this)
        {
          if (mapObject is MapSubGraph mapSubGraph)
            mapSubGraph.ExpandAll();
        }
      }

      protected virtual void ExpandChild(MapObject child, PointF hpos)
      {
        if (child == this.CollapsedObject)
          return;
        if (child is MapLink || child is MapLabeledLink)
        {
          MapStroke key = !(child is MapLink) ? (MapStroke) ((MapLabeledLink) child).RealLink : (MapStroke) child;
          if (this.SavedPaths.ContainsKey((object) key))
          {
            PointF[] savedPath = (PointF[]) this.SavedPaths[(object) key];
            for (int index = 0; index < savedPath.Length; ++index)
            {
              PointF pointF = savedPath[index];
              pointF.X += hpos.X;
              pointF.Y += hpos.Y;
              savedPath[index] = pointF;
            }
            key.SetPoints(savedPath);
          }
        }
        else if (this.SavedBounds.ContainsKey((object) child))
        {
          RectangleF savedBound = (RectangleF) this.SavedBounds[(object) child];
          child.Bounds = new RectangleF(hpos.X + savedBound.X, hpos.Y + savedBound.Y, savedBound.Width, savedBound.Height);
        }
        child.Visible = true;
      }

      public override RectangleF ExpandPaintBounds(RectangleF rect, MapView view)
      {
        if (this.Shadowed)
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

      public static MapSubGraph FindParentSubGraph(MapObject obj)
      {
        if (obj != null)
        {
          for (MapObject parent = (MapObject) obj.Parent; parent != null; parent = (MapObject) parent.Parent)
          {
            if (parent is MapSubGraph)
              return (MapSubGraph) parent;
          }
        }
        return (MapSubGraph) null;
      }

      protected virtual void FinishCollapse(RectangleF sgrect)
      {
        if (this.CollapsedObject != null)
        {
          this.CollapsedObject.Visible = true;
          this.CollapsedObject.Printable = true;
        }
        if (this.Label != null)
          this.Label.Position = new PointF(sgrect.X, sgrect.Y);
        if (!this.Resizable)
          return;
        this.InternalFlags |= 134217728 /*0x08000000*/;
        this.Resizable = false;
      }

      protected virtual void FinishExpand(PointF hpos)
      {
        if (this.CollapsedObject != null)
        {
          this.CollapsedObject.Visible = false;
          this.CollapsedObject.Printable = false;
        }
        if ((this.InternalFlags & 134217728 /*0x08000000*/) != 0)
        {
          this.InternalFlags &= -134217729;
          this.Resizable = true;
        }
        this.SavedBounds.Clear();
        this.SavedPaths.Clear();
      }

      private MapNodeLinkEnumerator GetLinkEnumerator(MapNode.Search s)
      {
        return new MapNodeLinkEnumerator((MapNode) this, s);
      }

      private MapNodeNodeEnumerator GetNodeEnumerator(MapNode.Search s)
      {
        return new MapNodeNodeEnumerator((MapNode) this, s);
      }

      public override void LayoutChildren(MapObject childchanged)
      {
        if (this.Initializing || childchanged == this.Handle && childchanged != null || childchanged == this.Port && childchanged != null)
          return;
        this.LayoutLabel();
        this.LayoutHandle();
        this.LayoutPort();
      }

      public virtual void LayoutHandle()
      {
        if (!this.IsExpanded)
          return;
        MapSubGraphHandle handle = this.Handle;
        if (handle == null || !handle.CanView())
          return;
        RectangleF bounds = this.ComputeBounds();
        SizeF sizeF = !this.IsExpanded ? this.CollapsedTopLeftMargin : this.TopLeftMargin;
        handle.Position = new PointF(bounds.X + sizeF.Width, bounds.Y + sizeF.Height);
      }

      public virtual void LayoutLabel()
      {
        MapText label = this.Label;
        if (label == null || !label.CanView())
          return;
        this.InternalFlags |= 16777216 /*0x01000000*/;
        RectangleF r = this.ComputeBounds();
        this.InternalFlags &= -16777217;
        SizeF sizeF;
        SizeF bottomRightMargin;
        if (this.IsExpanded)
        {
          sizeF = this.TopLeftMargin;
          bottomRightMargin = this.BottomRightMargin;
        }
        else
        {
          sizeF = this.CollapsedTopLeftMargin;
          bottomRightMargin = this.CollapsedBottomRightMargin;
        }
        r.X += sizeF.Width;
        r.Y += sizeF.Height;
        r.Width -= sizeF.Width + bottomRightMargin.Width;
        r.Height -= sizeF.Height + bottomRightMargin.Height;
        int spot;
        if (!this.IsExpanded)
        {
          spot = this.CollapsedLabelSpot;
          r = this.ComputeCollapsedRectangle(this.ComputeCollapsedSize(true));
        }
        else
          spot = this.LabelSpot;
        MapObject collapsedObject = this.CollapsedObject;
        if (collapsedObject != null)
        {
          PointF pointF = new PointF(r.X + r.Width / 2f, r.Y + r.Height / 2f);
          collapsedObject.Center = pointF;
          if (!this.IsExpanded)
            r = collapsedObject.Bounds;
        }
        PointF rectangleSpotLocation = this.GetRectangleSpotLocation(r, spot);
        this.PositionLabel(label, spot, rectangleSpotLocation);
      }

      public virtual void LayoutPort()
      {
        MapPort port = this.Port;
        if (port == null || !port.CanView())
          return;
        if (this.Handle != null)
        {
          RectangleF bounds = this.Handle.Bounds;
          port.Bounds = bounds;
        }
        else if (this.Label != null)
        {
          port.Bounds = this.Label.Bounds;
        }
        else
        {
          RectangleF bounds = this.ComputeBounds();
          SizeF sizeF = !this.IsExpanded ? this.CollapsedTopLeftMargin : this.TopLeftMargin;
          port.Position = new PointF(bounds.X + sizeF.Width, bounds.Y + sizeF.Height);
        }
      }

      protected override void MoveChildren(RectangleF prevRect)
      {
        float num1 = this.Left - prevRect.X;
        float num2 = this.Top - prevRect.Y;
        foreach (MapObject mapObject in (MapGroup) this)
        {
          if (mapObject is IMapLink)
          {
            RectangleF bounds = mapObject.Bounds;
            mapObject.Bounds = new RectangleF(bounds.X + num1, bounds.Y + num2, bounds.Width, bounds.Height);
          }
        }
        foreach (MapObject mapObject in (MapGroup) this)
        {
          if (!(mapObject is IMapLink))
          {
            RectangleF bounds = mapObject.Bounds;
            mapObject.Bounds = new RectangleF(bounds.X + num1, bounds.Y + num2, bounds.Width, bounds.Height);
          }
        }
      }

      public override void Paint(Graphics g, MapView view)
      {
        if (this.CollapsedObject == null || (view.IsPrinting ? (!this.CollapsedObject.CanPrint() ? 1 : 0) : (!this.CollapsedObject.CanView() ? 1 : 0)) != 0)
          this.PaintDecoration(g, view);
        base.Paint(g, view);
      }

      protected virtual void PaintDecoration(Graphics g, MapView view)
      {
        SizeF corner = !this.IsExpanded ? this.CollapsedCorner : this.Corner;
        GraphicsPath path1 = new GraphicsPath(FillMode.Winding);
        if ((double) this.Opacity > 0.0)
        {
          MapRoundedRectangle.MakeRoundedRectangularPath(path1, 0.0f, 0.0f, this.Bounds, corner);
          if (this.Shadowed)
          {
            SizeF shadowOffset = this.GetShadowOffset(view);
            GraphicsPath path2 = new GraphicsPath(FillMode.Winding);
            MapRoundedRectangle.MakeRoundedRectangularPath(path2, shadowOffset.Width, shadowOffset.Height, this.Bounds, corner);
            Region region = new Region(path2);
            region.Exclude(path1);
            Brush shadowBrush = this.GetShadowBrush(view);
            g.FillRegion(shadowBrush, region);
            region.Dispose();
            path2.Dispose();
          }
          Brush brush = (Brush) new SolidBrush(Color.FromArgb((int) Math.Round((double) this.Opacity / 100.0 * (double) byte.MaxValue), this.BackgroundColor));
          MapShape.DrawPath(g, view, (Pen) null, brush, path1);
          brush.Dispose();
          path1.Reset();
        }
        if (this.BorderPen != null)
        {
          RectangleF bounds = this.Bounds;
          float num = this.BorderPenInfo != null ? this.BorderPenInfo.Width : this.BorderPen.Width;
          MapObject.InflateRect(ref bounds, (float) (-(double) num / 2.0), (float) (-(double) num / 2.0));
          MapRoundedRectangle.MakeRoundedRectangularPath(path1, 0.0f, 0.0f, bounds, corner);
          MapShape.DrawPath(g, view, this.BorderPen, (Brush) null, path1);
        }
        path1.Dispose();
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
          if (this.PickableBackground)
          {
            if (!selectableOnly)
              return (MapObject) this;
            if (this.CanSelect())
              return (MapObject) this;
          }
        }
        return (MapObject) null;
      }

      public override IMapCollection PickObjects(
        PointF p,
        bool selectableOnly,
        IMapCollection coll,
        int max)
      {
        if (coll == null)
          coll = (IMapCollection) new MapCollection();
        if (coll.Count < max && this.CanView())
        {
          foreach (MapObject backward in this.Backwards)
          {
            MapObject mapObject = backward.Pick(p, selectableOnly);
            if (mapObject != null)
            {
              coll.Add(mapObject);
              if (coll.Count >= max)
                return coll;
            }
          }
          MapObject mapObject1 = this.Pick(p, selectableOnly);
          if (mapObject1 != null)
            coll.Add(mapObject1);
        }
        return coll;
      }

      private void PositionLabel(MapText lab, int spot, PointF pt)
      {
        switch (spot)
        {
          case 2:
            lab.Alignment = spot;
            lab.SetSpotLocation(16 /*0x10*/, pt);
            if (this.Handle == null || !MapObject.IntersectsRect(this.Handle.Bounds, lab.Bounds))
              break;
            pt.X = this.Handle.Right + 2f;
            lab.SetSpotLocation(16 /*0x10*/, pt);
            break;
          case 4:
            lab.Alignment = spot;
            lab.SetSpotLocation(8, pt);
            break;
          case 8:
            lab.Alignment = spot;
            lab.SetSpotLocation(4, pt);
            break;
          case 16 /*0x10*/:
            lab.Alignment = spot;
            lab.SetSpotLocation(2, pt);
            break;
          default:
            lab.Alignment = this.SpotOpposite(spot);
            lab.SetSpotLocation(this.SpotOpposite(spot), pt);
            break;
        }
      }

      protected virtual void PrepareCollapse()
      {
        foreach (MapObject mapObject in (MapGroup) this)
        {
          if (mapObject is MapSubGraph mapSubGraph)
            mapSubGraph.Collapse();
        }
      }

      protected virtual void PrepareExpand()
      {
      }

      public override void Remove(MapObject obj)
      {
        base.Remove(obj);
        if (obj == this.myHandle)
          this.myHandle = (MapSubGraphHandle) null;
        else if (obj == this.myLabel)
          this.myLabel = (MapText) null;
        else if (obj == this.myPort)
          this.myPort = (MapPort) null;
        else if (obj == this.myCollapsedObject)
          this.myCollapsedObject = (MapObject) null;
        if (this.SavedBounds.ContainsKey((object) obj))
          this.SavedBounds.Remove((object) obj);
        if (!this.SavedPaths.ContainsKey((object) obj))
          return;
        this.SavedPaths.Remove((object) obj);
      }

      public static void ReparentAllLinksToSubGraphs(IMapCollection coll, bool behind, MapLayer layer)
      {
        foreach (MapObject mapObject in (IEnumerable) coll)
        {
          switch (mapObject)
          {
            case IMapNode mapNode:
              IEnumerator enumerator1 = mapNode.Links.GetEnumerator();
              try
              {
                while (enumerator1.MoveNext())
                {
                  IMapLink current = (IMapLink) enumerator1.Current;
                  if (current != null && current.FromPort != null && current.ToPort != null)
                    MapSubGraph.ReparentToCommonSubGraph(current.MapObject, current.FromPort.MapObject, current.ToPort.MapObject, behind, layer);
                }
                continue;
              }
              finally
              {
                if (enumerator1 is IDisposable disposable)
                  disposable.Dispose();
              }
            case IMapPort mapPort:
              IEnumerator enumerator2 = mapPort.Links.GetEnumerator();
              try
              {
                while (enumerator2.MoveNext())
                {
                  IMapLink current = (IMapLink) enumerator2.Current;
                  if (current != null && current.FromPort != null && current.ToPort != null)
                    MapSubGraph.ReparentToCommonSubGraph(current.MapObject, current.FromPort.MapObject, current.ToPort.MapObject, behind, layer);
                }
                continue;
              }
              finally
              {
                if (enumerator2 is IDisposable disposable)
                  disposable.Dispose();
              }
            case IMapLink mapLink:
              if (mapLink.FromPort != null && mapLink.ToPort != null)
              {
                MapSubGraph.ReparentToCommonSubGraph(mapLink.MapObject, mapLink.FromPort.MapObject, mapLink.ToPort.MapObject, behind, layer);
                continue;
              }
              continue;
            default:
              continue;
          }
        }
      }

      public static void ReparentToCommonSubGraph(
        MapObject obj,
        MapObject child1,
        MapObject child2,
        bool behind,
        MapLayer layer)
      {
        MapObject mapObject = MapObject.FindCommonParent((MapObject) MapSubGraph.FindParentSubGraph(child1), (MapObject) MapSubGraph.FindParentSubGraph(child2));
        while (true)
        {
          switch (mapObject)
          {
            case null:
            case MapSubGraph _:
              goto label_3;
            default:
              mapObject = (MapObject) mapObject.Parent;
              continue;
          }
        }
    label_3:
        MapSubGraph mapSubGraph = mapObject as MapSubGraph;
        if (obj.Parent == mapSubGraph && obj.Layer != null)
          return;
        if (obj.Parent == null && obj.Layer == null)
        {
          if (mapSubGraph != null)
          {
            if (behind)
              mapSubGraph.InsertBefore((MapObject) null, obj);
            else
              mapSubGraph.InsertAfter((MapObject) null, obj);
          }
          else
            layer.Add(obj);
        }
        else
        {
          MapCollection coll = new MapCollection();
          coll.Add(obj);
          if (mapSubGraph != null)
            mapSubGraph.AddCollection((IMapCollection) coll, false);
          else
            layer.AddCollection((IMapCollection) coll, false);
        }
      }

      protected override void RescaleChildren(RectangleF prevRect)
      {
        if ((double) prevRect.Width <= 0.0 || (double) prevRect.Height <= 0.0)
          return;
        RectangleF bounds1 = this.Bounds;
        float num1 = bounds1.Width / prevRect.Width;
        float num2 = bounds1.Height / prevRect.Height;
        foreach (MapObject mapObject in (MapGroup) this)
        {
          if (mapObject is IMapLink && mapObject.AutoRescales)
          {
            RectangleF bounds2 = mapObject.Bounds;
            float x = bounds1.X + (bounds2.X - prevRect.X) * num1;
            float y = bounds1.Y + (bounds2.Y - prevRect.Y) * num2;
            float width = bounds2.Width * num1;
            float height = bounds2.Height * num2;
            mapObject.Bounds = new RectangleF(x, y, width, height);
          }
        }
        foreach (MapObject mapObject in (MapGroup) this)
        {
          if (!(mapObject is IMapLink) && mapObject.AutoRescales)
          {
            RectangleF bounds3 = mapObject.Bounds;
            float x = bounds1.X + (bounds3.X - prevRect.X) * num1;
            float y = bounds1.Y + (bounds3.Y - prevRect.Y) * num2;
            float width = bounds3.Width * num1;
            float height = bounds3.Height * num2;
            mapObject.Bounds = new RectangleF(x, y, width, height);
          }
        }
      }

      protected virtual void SaveChildBounds(MapObject child, RectangleF sgrect)
      {
        if (child == this.Handle || child == this.Label || child == this.Port || child == this.CollapsedObject)
          return;
        switch (child)
        {
          case MapLink _:
          case MapLabeledLink _:
            PointF[] pointFArray = (!(child is MapLink) ? (MapStroke) ((MapLabeledLink) child).RealLink : (MapStroke) child).CopyPointsArray();
            for (int index = 0; index < pointFArray.Length; ++index)
            {
              PointF pointF = pointFArray[index];
              pointF.X -= sgrect.X;
              pointF.Y -= sgrect.Y;
              pointFArray[index] = pointF;
            }
            this.myPathsHashtable[(object) child] = (object) pointFArray;
            break;
          default:
            SizeF size = child.Size;
            SizeF sizeF = MapTool.SubtractPoints(child.Position, new PointF(sgrect.X, sgrect.Y));
            this.myBoundsHashtable[(object) child] = (object) new RectangleF(sizeF.Width, sizeF.Height, size.Width, size.Height);
            break;
        }
      }

      private void setBottomRightMargin(SizeF margin, bool undoing)
      {
        SizeF bottomRightMargin = this.myBottomRightMargin;
        if (!(bottomRightMargin != margin) || (double) margin.Width < 0.0 || (double) margin.Height < 0.0)
          return;
        this.myBottomRightMargin = margin;
        this.Changed(2712, 0, (object) null, MapObject.MakeRect(bottomRightMargin), 0, (object) null, MapObject.MakeRect(margin));
        if (undoing)
          return;
        this.InvalidBounds = true;
      }

      private void setCollapsedBottomRightMargin(SizeF margin, bool undoing)
      {
        SizeF bottomRightMargin = this.myCollapsedBottomRightMargin;
        if (!(bottomRightMargin != margin) || (double) margin.Width < 0.0 || (double) margin.Height < 0.0)
          return;
        this.myCollapsedBottomRightMargin = margin;
        this.Changed(2714, 0, (object) null, MapObject.MakeRect(bottomRightMargin), 0, (object) null, MapObject.MakeRect(margin));
        if (undoing)
          return;
        this.InvalidBounds = true;
      }

      private void setCollapsedLabelSpot(int spot, bool undoing)
      {
        int collapsedLabelSpot = this.myCollapsedLabelSpot;
        if (collapsedLabelSpot == spot)
          return;
        this.myCollapsedLabelSpot = spot;
        this.Changed(2716, collapsedLabelSpot, (object) null, MapObject.NullRect, spot, (object) null, MapObject.NullRect);
        if (undoing)
          return;
        this.LayoutChildren((MapObject) null);
      }

      private void setCollapsedTopLeftMargin(SizeF margin, bool undoing)
      {
        SizeF collapsedTopLeftMargin = this.myCollapsedTopLeftMargin;
        if (!(collapsedTopLeftMargin != margin) || (double) margin.Width < 0.0 || (double) margin.Height < 0.0)
          return;
        this.myCollapsedTopLeftMargin = margin;
        this.Changed(2713, 0, (object) null, MapObject.MakeRect(collapsedTopLeftMargin), 0, (object) null, MapObject.MakeRect(margin));
        if (undoing)
          return;
        this.InvalidBounds = true;
      }

      private void setLabelSpot(int spot, bool undoing)
      {
        int labelSpot = this.myLabelSpot;
        if (labelSpot == spot)
          return;
        this.myLabelSpot = spot;
        this.Changed(2706, labelSpot, (object) null, MapObject.NullRect, spot, (object) null, MapObject.NullRect);
        if (undoing)
          return;
        this.LayoutChildren((MapObject) null);
      }

      private void setTopLeftMargin(SizeF margin, bool undoing)
      {
        SizeF topLeftMargin = this.myTopLeftMargin;
        if (!(topLeftMargin != margin) || (double) margin.Width < 0.0 || (double) margin.Height < 0.0)
          return;
        this.myTopLeftMargin = margin;
        this.Changed(2707, 0, (object) null, MapObject.MakeRect(topLeftMargin), 0, (object) null, MapObject.MakeRect(margin));
        if (undoing)
          return;
        this.InvalidBounds = true;
      }

      public void Toggle()
      {
        if (this.State == MapSubGraphState.Expanded)
        {
          this.Collapse();
        }
        else
        {
          if (this.State != MapSubGraphState.Collapsed)
            return;
          this.Expand();
        }
      }

      [Category("Appearance")]
      [Description("The background color for the group; the opacity is specified separately")]
      public virtual Color BackgroundColor
      {
        get => this.myBackgroundColor;
        set
        {
          Color backgroundColor = this.myBackgroundColor;
          if (!(backgroundColor != value))
            return;
          this.myBackgroundColor = value;
          this.Changed(2704, 0, (object) backgroundColor, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Category("Appearance")]
      [Description("The pen used to draw an outline for this node.")]
      public virtual Pen BorderPen
      {
        get => this.myBorderPenInfo != null ? this.myBorderPenInfo.GetPen() : (Pen) null;
        set
        {
          MapShape.MapPenInfo borderPenInfo = this.myBorderPenInfo;
          MapShape.MapPenInfo penInfo = MapShape.GetPenInfo(value);
          if (borderPenInfo == penInfo)
            return;
          this.myBorderPenInfo = penInfo;
          this.Changed(2708, 0, (object) borderPenInfo, MapObject.NullRect, 0, (object) penInfo, MapObject.NullRect);
        }
      }

      internal MapShape.MapPenInfo BorderPenInfo => this.myBorderPenInfo;

      [Description("The margin around the text inside the background at the right side and the bottom")]
      [TypeConverter(typeof (MapSizeFConverter))]
      [Category("Appearance")]
      public virtual SizeF BottomRightMargin
      {
        get => this.myBottomRightMargin;
        set => this.setBottomRightMargin(value, false);
      }

      [TypeConverter(typeof (MapSizeFConverter))]
      [Description("The margin around the text inside the background at the right side and the bottom of a collapsed subgraph")]
      [Category("Appearance")]
      public virtual SizeF CollapsedBottomRightMargin
      {
        get => this.myCollapsedBottomRightMargin;
        set => this.setCollapsedBottomRightMargin(value, false);
      }

      [Category("Appearance")]
      [Description("The maximum radial width and height of each corner of a collapsed node")]
      [TypeConverter(typeof (MapSizeFConverter))]
      public virtual SizeF CollapsedCorner
      {
        get => this.myCollapsedCorner;
        set
        {
          SizeF collapsedCorner = this.myCollapsedCorner;
          if (!(collapsedCorner != value) || (double) value.Width < 0.0 || (double) value.Height < 0.0)
            return;
          this.myCollapsedCorner = value;
          this.Changed(2715, 0, (object) null, MapObject.MakeRect(collapsedCorner), 0, (object) null, MapObject.MakeRect(value));
        }
      }

      [Description("The spot where the label should be positioned when the node is collapsed")]
      [DefaultValue(1)]
      [Category("Appearance")]
      public virtual int CollapsedLabelSpot
      {
        get => this.myCollapsedLabelSpot;
        set => this.setCollapsedLabelSpot(value, false);
      }

      public MapObject CollapsedObject
      {
        get => this.myCollapsedObject;
        set
        {
          MapObject collapsedObject = this.myCollapsedObject;
          if (collapsedObject == value)
            return;
          if (collapsedObject != null)
            this.Remove(collapsedObject);
          this.myCollapsedObject = value;
          if (value != null)
            this.InsertBefore((MapObject) null, value);
          this.Changed(2717, 0, (object) collapsedObject, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [TypeConverter(typeof (MapSizeFConverter))]
      [Category("Appearance")]
      [Description("The margin around the text inside the background at the left side and the top of a collapsed subgraph")]
      public virtual SizeF CollapsedTopLeftMargin
      {
        get => this.myCollapsedTopLeftMargin;
        set => this.setCollapsedTopLeftMargin(value, false);
      }

      [Description("Whether the user is allowed to expand and collapse this subgraph")]
      [DefaultValue(true)]
      [Category("Behavior")]
      public virtual bool Collapsible
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
          this.Changed(2703, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Description("The maximum radial width and height of each corner")]
      [Category("Appearance")]
      [TypeConverter(typeof (MapSizeFConverter))]
      public virtual SizeF Corner
      {
        get => this.myCorner;
        set
        {
          SizeF corner = this.myCorner;
          if (!(corner != value) || (double) value.Width < 0.0 || (double) value.Height < 0.0)
            return;
          this.myCorner = value;
          this.Changed(2710, 0, (object) null, MapObject.MakeRect(corner), 0, (object) null, MapObject.MakeRect(value));
        }
      }

      [Description("Gets an enumerator over all of the links going out of this node.")]
      public virtual MapNodeLinkEnumerator ExternalDestinationLinks
      {
        get => this.GetLinkEnumerator(MapNode.Search.LinksOut | MapNode.Search.NotSelf);
      }

      [Description("Gets an enumerator over all of the nodes that have links going out of this node.")]
      public virtual MapNodeNodeEnumerator ExternalDestinations
      {
        get => this.GetNodeEnumerator(MapNode.Search.NodesOut | MapNode.Search.NotSelf);
      }

      [Description("Gets an enumerator over all of the links connected to this node.")]
      public virtual MapNodeLinkEnumerator ExternalLinks
      {
        get
        {
          return this.GetLinkEnumerator(MapNode.Search.LinksIn | MapNode.Search.LinksOut | MapNode.Search.NotSelf);
        }
      }

      [Description("Gets an enumerator over all of the nodes that are connected to this node.")]
      public virtual MapNodeNodeEnumerator ExternalNodes
      {
        get
        {
          return this.GetNodeEnumerator(MapNode.Search.NodesIn | MapNode.Search.NodesOut | MapNode.Search.NotSelf);
        }
      }

      [Description("Gets an enumerator over all of the links coming into this node.")]
      public virtual MapNodeLinkEnumerator ExternalSourceLinks
      {
        get => this.GetLinkEnumerator(MapNode.Search.LinksIn | MapNode.Search.NotSelf);
      }

      [Description("Gets an enumerator over all of the nodes that have links coming into this node.")]
      public virtual MapNodeNodeEnumerator ExternalSources
      {
        get => this.GetNodeEnumerator(MapNode.Search.NodesIn | MapNode.Search.NotSelf);
      }

      public MapSubGraphHandle Handle => this.myHandle;

      [Description("Whether this subgraph is in an expanded state")]
      [Category("Appearance")]
      [DefaultValue(true)]
      public bool IsExpanded => this.State == MapSubGraphState.Expanded;

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
          this.Changed(2702, 0, (object) label, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      [Category("Appearance")]
      [DefaultValue(32 /*0x20*/)]
      [Description("The spot where the label should be positioned")]
      public virtual int LabelSpot
      {
        get => this.myLabelSpot;
        set => this.setLabelSpot(value, false);
      }

      [Browsable(false)]
      public virtual SizeF Margin
      {
        get => this.myTopLeftMargin;
        set
        {
          this.TopLeftMargin = value;
          this.BottomRightMargin = value;
        }
      }

      [Category("Appearance")]
      [Description("The opaqueness of the background; the background color is specified separately")]
      [DefaultValue(20f)]
      public virtual float Opacity
      {
        get => this.myOpacity;
        set
        {
          float opacity = this.myOpacity;
          if ((double) opacity == (double) value || (double) value < 0.0 || (double) value > 100.0)
            return;
          this.myOpacity = value;
          this.Changed(2705, 0, (object) null, MapObject.MakeRect(opacity), 0, (object) null, MapObject.MakeRect(value));
        }
      }

      [Description("Whether picking in the background of this node selects the node.")]
      [Category("Behavior")]
      [DefaultValue(false)]
      public virtual bool PickableBackground
      {
        get => (this.InternalFlags & 67108864 /*0x04000000*/) != 0;
        set
        {
          bool oldVal = (this.InternalFlags & 67108864 /*0x04000000*/) != 0;
          if (oldVal == value)
            return;
          if (value)
            this.InternalFlags |= 67108864 /*0x04000000*/;
          else
            this.InternalFlags &= -67108865;
          this.Changed(2709, 0, (object) oldVal, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
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
            this.InsertBefore((MapObject) null, (MapObject) value);
          this.Changed(2711, 0, (object) port, MapObject.NullRect, 0, (object) value, MapObject.NullRect);
        }
      }

      public Hashtable SavedBounds => this.myBoundsHashtable;

      public Hashtable SavedPaths => this.myPathsHashtable;

      protected MapSubGraphState State
      {
        get => this.myState;
        set
        {
          MapSubGraphState state = this.myState;
          if (state == value)
            return;
          this.myState = value;
          this.Changed(2718, (int) state, (object) null, this.Bounds, (int) value, (object) null, this.Bounds);
        }
      }

      [Category("Appearance")]
      [TypeConverter(typeof (MapSizeFConverter))]
      [Description("The margin around the text inside the background at the left side and the top")]
      public virtual SizeF TopLeftMargin
      {
        get => this.myTopLeftMargin;
        set => this.setTopLeftMargin(value, false);
      }
    }
}
