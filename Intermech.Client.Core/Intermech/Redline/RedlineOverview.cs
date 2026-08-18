
// Type: Intermech.Redline.RedlineOverview
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Map;
using System;
using System.ComponentModel;
using System.Drawing;


namespace Intermech.Redline;

/// <summary>окно	 предпросмотра</summary>
public class RedlineOverview : MapOverview
{
  private MapView _observed;

  public RedlineOverview()
  {
    this.ShowsNegativeCoordinates = true;
    this.ShadowOffset = new SizeF(0.0f, 0.0f);
    this.ShowHorizontalScrollBar = MapViewScrollBarVisibility.Hide;
    this.ShowVerticalScrollBar = MapViewScrollBarVisibility.Hide;
    this.Leave += new EventHandler(this.RedlineOverview_Leave);
  }

  private void RedlineOverview_Leave(object sender, EventArgs e) => this.CheckObserved();

  /// <summary>проекция начала окна на Документ </summary>
  [Category("Appearance")]
  [TypeConverter(typeof (MapPointFConverter))]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Description("The position in the document that this view is displaying.")]
  public override PointF DocPosition
  {
    get => this.myOrigin;
    set
    {
      RectangleF documentBounds = this.ComputeDocumentBounds();
      PointF origin = this.myOrigin;
      PointF location = documentBounds.Location;
      PointF pointF = location;
      if (!(origin != pointF))
        return;
      this.myOrigin = location;
      this.RaisePropertyChangedEvent(nameof (DocPosition));
    }
  }

  protected override void OverviewUpdate()
  {
    this.DocScale = this.LimitDocScale(1f);
    this.DocPosition = PointF.Empty;
  }

  public override PointF LimitDocPosition(PointF p)
  {
    RectangleF documentBounds = this.ComputeDocumentBounds();
    if (documentBounds.Contains(p))
      return p;
    PointF pointF = p;
    if ((double) pointF.X < (double) documentBounds.Left)
      pointF.X = documentBounds.Left;
    if ((double) pointF.X > (double) documentBounds.Right)
      pointF.X = documentBounds.Right;
    if ((double) pointF.Y < (double) documentBounds.Top)
      pointF.Y = documentBounds.Top;
    if ((double) pointF.Y > (double) documentBounds.Bottom)
      pointF.Y = documentBounds.Bottom;
    return pointF;
  }

  public override float LimitDocScale(float scale)
  {
    RectangleF documentBounds = this.ComputeDocumentBounds();
    float num = 1f;
    if ((double) documentBounds.Width > 0.0 && (double) documentBounds.Height > 0.0)
    {
      Size size = this.DisplayRectangle.Size;
      num = Math.Min((float) size.Width / documentBounds.Width, (float) size.Height / documentBounds.Height);
    }
    return num;
  }

  protected override void Dispose(bool disposing)
  {
    base.Dispose(disposing);
    if (this._observed != null)
      this._observed.VisibleChanged -= new EventHandler(this.MapView_VisibleChanged);
    this._observed = (MapView) null;
  }

  /// <summary>проверка видимости окна Observed и переключение base.Observed </summary>
  private void CheckObserved()
  {
    if (this._observed == null)
      return;
    bool flag = false;
    if (!this._observed.Visible)
    {
      base.Observed = (MapView) null;
      flag = true;
    }
    if (this._observed.Visible)
    {
      base.Observed = this._observed;
      flag = true;
    }
    if (!flag)
      return;
    this.UpdateView();
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public override MapView Observed
  {
    get
    {
      if (this._observed == null)
        return (MapView) null;
      if (base.Observed == null)
      {
        if (this._observed.Visible)
          base.Observed = this._observed;
        return base.Observed;
      }
      if (!this._observed.Visible)
        base.Observed = (MapView) null;
      return base.Observed;
    }
    set
    {
      if (this._observed != null)
        this._observed.VisibleChanged -= new EventHandler(this.MapView_VisibleChanged);
      this._observed = value;
      if (this._observed != null)
        this._observed.VisibleChanged += new EventHandler(this.MapView_VisibleChanged);
      base.Observed = this._observed;
      this.CheckObserved();
    }
  }

  private void MapView_VisibleChanged(object sender, EventArgs e)
  {
    if (this.Disposing)
      return;
    this.CheckObserved();
  }

  protected override void OnVisibleChanged(EventArgs evt)
  {
    base.OnVisibleChanged(evt);
    this.CheckObserved();
    if (!this.Visible)
      return;
    this.LayoutScrollBars(false);
    this.ZoomToFit();
    this.UpdateView();
  }

  protected override void OnSizeChanged(EventArgs evt)
  {
    base.OnSizeChanged(evt);
    this.CheckObserved();
    this.ZoomToFit();
  }

  public override RectangleF ComputeDocumentBounds()
  {
    if (this.Observed == null)
      return new RectangleF();
    if (this.OverviewRect != null)
      this.OverviewRect.UpdateRectFromView();
    return RectangleF.Union(this.Observed.ComputeDocumentBounds(), this.Observed.DocExtent);
  }

  protected override void OnBackgroundSingleClicked(MapInputEventArgs evt)
  {
    base.OnBackgroundSingleClicked(evt);
    if (this.OverviewRect == null)
      return;
    RectangleF bounds = this.OverviewRect.Bounds;
    this.OverviewRect.Location = this.OverviewRect.ComputeMove(this.OverviewRect.Location, new PointF(evt.DocPoint.X - bounds.Width / 2f, evt.DocPoint.Y - bounds.Height / 2f));
    this.ZoomToFit();
  }
}
