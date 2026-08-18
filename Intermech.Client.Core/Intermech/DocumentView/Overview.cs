
// Type: Intermech.DocumentView.Overview
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.DocumentView;

/// <summary>
/// Provide a reduced-scale view of a document, showing the size and position of
/// another view's viewport onto that same document, and support panning and
/// zooming of that observed view.
/// </summary>
/// <remarks>
/// The user can drag around the rectangle representing the observed view's
/// viewport in order to scroll it.  Clicking will move the observed view's
/// viewport to that location.  Doing a rubber-band drag will change the
/// observed view's position and scale to match the box that was drawn.
/// </remarks>
[ToolboxBitmap(typeof (Overview), "Intermech.Map.MapOverview.bmp")]
public class Overview : View
{
  [NonSerialized]
  private DocumentChangedEventHandler _documentChangedEventHandler;
  private View _observedView;
  private IDocument _observedDocument;
  private OverviewRectangle _overviewRect;
  [NonSerialized]
  private PropertyChangedEventHandler _viewPropertyChangedEventHandler;
  [NonSerialized]
  private EventHandler _viewResizedEventHandler;
  private ToolZooming _zoomTool;
  private StringFormat _sf;

  /// <summary>
  /// Create a <see cref="T:Intermech.Map.MapOverview" /> window capable of displaying the position
  /// of a different <see cref="T:Intermech.Map.MapView" /> in its <see cref="T:Intermech.Map.MapDocument" />.
  /// </summary>
  /// <remarks>
  /// You need to set the <see cref="P:Intermech.Map.MapOverview.Observed" /> property to make this
  /// overview <c>Control</c> useful.
  /// </remarks>
  public Overview()
  {
    this._observedView = (View) null;
    this._observedDocument = (IDocument) null;
    this._overviewRect = (OverviewRectangle) null;
    this._zoomTool = (ToolZooming) null;
    this._documentChangedEventHandler = (DocumentChangedEventHandler) null;
    this._viewResizedEventHandler = (EventHandler) null;
    this._viewPropertyChangedEventHandler = (PropertyChangedEventHandler) null;
    this._zoomTool = new ToolZooming((IView) this);
    this.ReplaceMouseTool(typeof (ToolRubberBanding), (ITool) this._zoomTool);
    this._sf = new StringFormat();
    this._sf.Alignment = StringAlignment.Center;
    this._sf.LineAlignment = StringAlignment.Center;
    this.DocScale = 1f;
    this.DoubleBuffered = true;
  }

  private void AddListeners()
  {
    if (this._documentChangedEventHandler == null)
    {
      this._documentChangedEventHandler = new DocumentChangedEventHandler(((View) this).SafeOnDocumentChanged);
      this._viewResizedEventHandler = new EventHandler(this.ComponentResized);
      this._viewPropertyChangedEventHandler = new PropertyChangedEventHandler(this.OnViewChanged);
    }
    if (this._observedDocument != null)
      this._observedDocument.Changed += this._documentChangedEventHandler;
    if (this._observedView == null)
      return;
    this._observedView.Resize += this._viewResizedEventHandler;
    this._observedView.PropertyChanged += this._viewPropertyChangedEventHandler;
  }

  /// <summary>
  /// Handle changes in the observed view's (window) shape by changing the bounds
  /// of the OverviewRect.
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected void ComponentResized(object sender, EventArgs e)
  {
    if (this.OverviewRect == null)
      return;
    this.OverviewRect.UpdateRectFromView();
  }

  /// <summary>
  /// Create an instance of <see cref="T:Intermech.Map.MapOverviewRectangle" /> for the
  /// given view.
  /// </summary>
  /// <param name="observed"></param>
  /// <returns>An <see cref="T:Intermech.Map.MapOverviewRectangle" /> that knows which view's extent it represents</returns>
  public virtual OverviewRectangle CreateOverviewRectangle(IView observed)
  {
    return new OverviewRectangle();
  }

  /// <summary>
  /// Remove any event handlers from the <see cref="P:Intermech.Map.MapOverview.Observed" /> view and document.
  /// </summary>
  /// <param name="disposing"></param>
  protected override void Dispose(bool disposing)
  {
    base.Dispose(disposing);
    this.RemoveListeners();
    this._observedView = (View) null;
    this._sf.Dispose();
  }

  /// <summary>
  /// Limit mouse over behavior to document objects--ignore view objects
  /// </summary>
  /// <param name="evt"></param>
  /// <returns></returns>
  /// <remarks>
  /// This is basically to support tooltips, which are more valuable when
  /// the objects are so small.  Other mouse-over behavior, including
  /// changing the <c>Cursor</c>, is explicitly avoided.
  /// </remarks>
  public override bool DoMouseOver(InputEventArgs evt)
  {
    if (this.OverviewRect != null && this.OverviewRect.ContainsPoint(evt.DocPoint))
      this.Cursor = Cursors.SizeAll;
    else
      this.Cursor = this.DefaultCursor;
    return true;
  }

  protected override void OnPaint(PaintEventArgs evt)
  {
    base.OnPaint(evt);
    if (this._overviewRect != null && this._observedDocument != null)
    {
      Rectangle view = this.ConvertDocToView(this._overviewRect.Bounds);
      Color blue = Color.Blue;
      int r = (int) blue.R;
      blue = Color.Blue;
      int g = (int) blue.G;
      int b = (int) Color.Blue.B;
      using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(100, r, g, b)))
        evt.Graphics.FillRectangle((Brush) solidBrush, view);
    }
    else
      evt.Graphics.DrawString(LocalizationHolder.rm.GetString("Client.Core_156"), this.Font, SystemBrushes.ControlText, (RectangleF) this.DisplayRectangle, this._sf);
  }

  /// <summary>Initialize the layers of this view.</summary>
  /// <remarks>
  /// This method makes sure this overview's layers are the same as that of its document.
  /// It also adds the result of <see cref="M:Intermech.Map.MapOverview.CreateOverviewRectangle(Intermech.Map.MapView)" /> to this
  /// view's default layer.
  /// </remarks>
  public override void InitializeLayersFromDocument()
  {
    base.InitializeLayersFromDocument();
    if (this.Observed == null)
      return;
    if (this._overviewRect != null)
      this._overviewRect.BoundsChanged -= new EventHandler(this.OverviewRect_BoundsChanged);
    this._overviewRect = this.CreateOverviewRectangle(this.Observed);
    this._overviewRect.BoundsChanged += new EventHandler(this.OverviewRect_BoundsChanged);
    this._overviewRect.View = (IView) this;
    this._overviewRect.Bounds = this.Observed.DocExtent;
  }

  private void OverviewRect_BoundsChanged(object sender, EventArgs e)
  {
    RectangleF bounds = this._overviewRect.Bounds;
    RectangleF oldBounds = this._overviewRect.OldBounds;
    this.Invalidate();
  }

  public override void MoveSelection(IObject obj1, SizeF offset)
  {
    if (obj1 == null)
      return;
    PointF location = obj1.Location;
    PointF newLoc = new PointF(location.X + offset.Width, location.Y + offset.Height);
    obj1.DoMove((IView) this, location, newLoc);
  }

  /// <summary>
  /// Allow mouse clicks not on the OverviewRect, but elsewhere in the
  /// Overview, to cause the OverviewRect to be centered there, or as
  /// near as allowed.
  /// </summary>
  /// <param name="evt"></param>
  protected override void OnBackgroundSingleClicked(InputEventArgs evt)
  {
    base.OnBackgroundSingleClicked(evt);
    if (this.OverviewRect == null)
      return;
    RectangleF bounds = this.OverviewRect.Bounds;
    this.OverviewRect.Location = this.OverviewRect.ComputeMove(this.OverviewRect.Location, new PointF(evt.DocPoint.X - bounds.Width / 2f, evt.DocPoint.Y - bounds.Height / 2f));
  }

  /// <summary>
  /// Don't allow the user to select any objects except the OverviewRect,
  /// even though that rectangle is a view object, not a document object.
  /// </summary>
  /// <param name="doc"></param>
  /// <param name="view"></param>
  /// <param name="p"></param>
  /// <param name="selectableOnly"></param>
  /// <returns></returns>
  /// <remarks>
  /// The <see cref="P:Intermech.Map.MapOverview.OverviewRect" /> gets picked when the point <paramref name="p" />
  /// is in the rectangle's bounds, even though the the <see cref="P:Intermech.Map.MapOverview.OverviewRect" />
  /// is not selectable and is not even a document object.
  /// </remarks>
  public override IObject PickObject(bool doc, bool view, PointF p, bool selectableOnly)
  {
    return this.OverviewRect != null && this.OverviewRect.ContainsPoint(p) ? (IObject) this.OverviewRect : (IObject) null;
  }

  private void RemoveListeners()
  {
    if (this._observedDocument != null)
      this._observedDocument.Changed -= this._documentChangedEventHandler;
    if (this._observedView == null)
      return;
    this._observedView.Resize -= this._viewResizedEventHandler;
    this._observedView.PropertyChanged -= this._viewPropertyChangedEventHandler;
  }

  /// <summary>
  /// Handle basic changes to the observed view's DocPosition or DocScale,
  /// or when the observed view's Document got swapped for a different document.
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected void OnViewChanged(object sender, PropertyChangedEventArgs e)
  {
    if (e.PropertyName == "DocPosition" || e.PropertyName == "DocScale")
    {
      if (this.OverviewRect == null)
        return;
      this.OverviewRect.UpdateRectFromView();
    }
    else
    {
      if (!(e.PropertyName == "Document") || !(sender is IView))
        return;
      if (this._observedDocument != null)
        this._observedDocument.Changed -= this._documentChangedEventHandler;
      this._observedDocument = ((IView) sender).Document;
      if (this._observedDocument != null)
        this._observedDocument.Changed += this._documentChangedEventHandler;
      this.InitializeLayersFromDocument();
      if (this.OverviewRect == null)
        return;
      this.OverviewRect.UpdateRectFromView();
    }
  }

  /// <summary>
  /// Make this view think the observed view's document is actually its own.
  /// </summary>
  /// <remarks>
  /// Setting this property is not useful except for changing the document
  /// that is shown when there is no <see cref="P:Intermech.Map.MapOverview.Observed" /> view.
  /// </remarks>
  public override IDocument Document
  {
    get => this._observedDocument != null ? this._observedDocument : base.Document;
    set => base.Document = value;
  }

  /// <summary>Gets or sets the view that this overview is watching.</summary>
  /// <value>
  /// This property should not be set to itself or another GoOverview.
  /// </value>
  /// <remarks>
  /// This overview is useless until it has a <see cref="T:Intermech.Map.MapView" /> to observe.
  /// When this property is set, this overview becomes a document <c>Changed</c>
  /// event handler for the observed view's document so that it can display
  /// that document.
  /// It also becomes a <c>PropertyChanged</c> event handler and a <c>Resize</c>
  /// event handler for the observed view so that it can track the observed
  /// view's extent (position and size) in its document, as well as any
  /// replacement of the observed view's document.
  /// </remarks>
  /// <seealso cref="T:Intermech.Map.MapOverviewRectangle" />
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public virtual IView Observed
  {
    get => (IView) this._observedView;
    set
    {
      if (value == this || value is Overview || this._observedView == value)
        return;
      this.RemoveListeners();
      this._observedView = value as View;
      if (this._observedView != null)
      {
        this._zoomTool.ZoomedView = (IView) this._observedView;
        this._observedDocument = this._observedView.Document;
        this.AddListeners();
      }
      else
      {
        this._zoomTool.ZoomedView = (IView) this;
        this._observedDocument = (IDocument) null;
        this._overviewRect = (OverviewRectangle) null;
      }
      this.InitializeLayersFromDocument();
      this.UpdateView();
      this.RaisePropertyChangedEvent(nameof (Observed));
    }
  }

  /// <summary>
  /// Gets the <see cref="T:Intermech.Map.MapOverviewRectangle" /> representing the observed
  /// view's extent in its document.
  /// </summary>
  /// <remarks>
  /// This is the rectangle in this view that the user drags to
  /// change the <see cref="P:Intermech.Map.MapView.DocPosition" /> of the observed view.
  /// </remarks>
  /// <seealso cref="P:Intermech.Map.MapOverview.Observed" />
  /// <seealso cref="M:Intermech.Map.MapOverview.CreateOverviewRectangle(Intermech.Map.MapView)" />
  [Browsable(false)]
  public OverviewRectangle OverviewRect => this._overviewRect;

  /// <summary>This should just track what the observed view shows.</summary>
  public override bool ShowsNegativeCoordinates
  {
    get => this.Observed != null && this.Observed.ShowsNegativeCoordinates;
    set => base.ShowsNegativeCoordinates = value;
  }
}
