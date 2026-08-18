// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapOverview
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Map
{
    [ToolboxBitmap(typeof (MapOverview), "Intermech.Map.MapOverview.bmp")]
    public class MapOverview : MapView
    {
      [NonSerialized]
      private MapChangedEventHandler myDocChangedEventHandler;
      private MapView myObserved;
      private MapDocument myObservedDocument;
      private MapOverviewRectangle myOverviewRect;
      [NonSerialized]
      private PropertyChangedEventHandler myViewPropertyChangedEventHandler;
      [NonSerialized]
      private EventHandler myViewResizedEventHandler;
      private MapToolZooming myZoomTool;

      public MapOverview()
      {
        this.myObserved = (MapView) null;
        this.myObservedDocument = (MapDocument) null;
        this.myOverviewRect = (MapOverviewRectangle) null;
        this.myZoomTool = (MapToolZooming) null;
        this.myDocChangedEventHandler = (MapChangedEventHandler) null;
        this.myViewResizedEventHandler = (EventHandler) null;
        this.myViewPropertyChangedEventHandler = (PropertyChangedEventHandler) null;
        this.myZoomTool = new MapToolZooming((MapView) this);
        this.ReplaceMouseTool(typeof (MapToolRubberBanding), (IMapTool) this.myZoomTool);
        this.SetModifiable(false);
        this.AllowSelect = false;
        this.AllowCopy = false;
        this.AllowMove = true;
        this.AllowDragOut = false;
        this.InitAllowDrop(false);
        this.DragsRealtime = true;
        this.DocScale = 0.125f;
      }

      private void AddListeners()
      {
        if (this.myDocChangedEventHandler == null)
        {
          this.myDocChangedEventHandler = new MapChangedEventHandler(((MapView) this).SafeOnDocumentChanged);
          this.myViewResizedEventHandler = new EventHandler(this.ComponentResized);
          this.myViewPropertyChangedEventHandler = new PropertyChangedEventHandler(this.ViewChanged);
        }
        if (this.myObservedDocument != null)
          this.myObservedDocument.Changed += this.myDocChangedEventHandler;
        if (this.myObserved == null)
          return;
        this.myObserved.Resize += this.myViewResizedEventHandler;
        this.myObserved.PropertyChanged += this.myViewPropertyChangedEventHandler;
      }

      protected void ComponentResized(object sender, EventArgs e)
      {
        if (this.OverviewRect == null)
          return;
        this.OverviewRect.UpdateRectFromView();
      }

      public virtual MapOverviewRectangle CreateOverviewRectangle(MapView observed)
      {
        return new MapOverviewRectangle();
      }

      protected override void Dispose(bool disposing)
      {
        base.Dispose(disposing);
        this.RemoveListeners();
        this.myObserved = (MapView) null;
      }

      public override bool DoMouseOver(MapInputEventArgs evt)
      {
        if (this.OverviewRect != null && this.OverviewRect.ContainsPoint(evt.DocPoint))
          this.Cursor = Cursors.SizeAll;
        else
          this.Cursor = this.DefaultCursor;
        this.DoToolTipObject(this.Document.PickObject(evt.DocPoint, false));
        return true;
      }

      public override void InitializeLayersFromDocument()
      {
        base.InitializeLayersFromDocument();
        if (this.Observed == null)
          return;
        this.myOverviewRect = this.CreateOverviewRectangle(this.Observed);
        this.myOverviewRect.Bounds = this.Observed.DocExtent;
        this.Layers.Default.Add((MapObject) this.myOverviewRect);
      }

      protected override void OnBackgroundSingleClicked(MapInputEventArgs evt)
      {
        base.OnBackgroundSingleClicked(evt);
        if (this.OverviewRect == null)
          return;
        RectangleF bounds = this.OverviewRect.Bounds;
        this.OverviewRect.Location = this.OverviewRect.ComputeMove(this.OverviewRect.Location, new PointF(evt.DocPoint.X - bounds.Width / 2f, evt.DocPoint.Y - bounds.Height / 2f));
      }

      public override MapObject PickObject(bool doc, bool view, PointF p, bool selectableOnly)
      {
        return this.OverviewRect != null && this.OverviewRect.ContainsPoint(p) ? (MapObject) this.OverviewRect : (MapObject) null;
      }

      private void RemoveListeners()
      {
        if (this.myObservedDocument != null)
          this.myObservedDocument.Changed -= this.myDocChangedEventHandler;
        if (this.myObserved == null)
          return;
        this.myObserved.Resize -= this.myViewResizedEventHandler;
        this.myObserved.PropertyChanged -= this.myViewPropertyChangedEventHandler;
      }

      protected virtual void OverviewUpdate()
      {
      }

      /// <summary>
      /// Handle basic changes to the observed view's DocPosition or DocScale,
      /// or when the observed view's Document got swapped for a different document.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      protected void ViewChanged(object sender, PropertyChangedEventArgs e)
      {
        if (e.PropertyName == "DocPosition" || e.PropertyName == "DocScale")
        {
          if (this.OverviewRect != null)
            this.OverviewRect.UpdateRectFromView();
          this.OverviewUpdate();
        }
        else
        {
          if (!(e.PropertyName == "Document") || !(sender is MapView))
            return;
          if (this.myObservedDocument != null)
            this.myObservedDocument.Changed -= this.myDocChangedEventHandler;
          this.myObservedDocument = ((MapView) sender).Document;
          if (this.myObservedDocument != null)
            this.myObservedDocument.Changed += this.myDocChangedEventHandler;
          this.InitializeLayersFromDocument();
          if (this.OverviewRect == null)
            return;
          this.OverviewRect.UpdateRectFromView();
        }
      }

      public override MapDocument Document
      {
        get => this.myObservedDocument != null ? this.myObservedDocument : base.Document;
        set => base.Document = value;
      }

      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      [Browsable(false)]
      public virtual MapView Observed
      {
        get => this.myObserved;
        set
        {
          if (value == this || value is MapOverview || this.myObserved == value)
            return;
          this.RemoveListeners();
          this.myObserved = value;
          if (this.myObserved != null)
          {
            this.myZoomTool.ZoomedView = this.myObserved;
            this.myObservedDocument = this.myObserved.Document;
            this.AddListeners();
          }
          else
          {
            this.myZoomTool.ZoomedView = (MapView) this;
            this.myObservedDocument = (MapDocument) null;
            this.myOverviewRect = (MapOverviewRectangle) null;
          }
          this.InitializeLayersFromDocument();
          this.UpdateView();
          this.RaisePropertyChangedEvent(nameof (Observed));
        }
      }

      [Browsable(false)]
      public MapOverviewRectangle OverviewRect => this.myOverviewRect;

      public override bool ShowsNegativeCoordinates
      {
        get => this.Observed != null && this.Observed.ShowsNegativeCoordinates;
        set => base.ShowsNegativeCoordinates = value;
      }
    }
}
