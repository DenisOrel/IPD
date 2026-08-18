
// Type: Intermech.Redline.RedlineView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Map;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Redline;

/// <summary>окно для закладки ПРОСМОТР </summary>
public class RedlineView : MapView
{
  public RedlineView()
  {
    this.DragsRealtime = true;
    this.ShowsNegativeCoordinates = true;
    this.ShadowOffset = new SizeF(0.0f, 0.0f);
  }

  public override PointF LimitDocPosition(PointF p)
  {
    RectangleF documentBounds = this.ComputeDocumentBounds();
    if (documentBounds.Contains(p) || (double) documentBounds.Width <= 0.0 || (double) documentBounds.Height <= 0.0)
      return p;
    Size size = this.DisplayRectangle.Size;
    double num = (double) Math.Min((float) size.Width / documentBounds.Width, (float) size.Height / documentBounds.Height);
    return p;
  }

  public virtual PointF LimitDocPosition_(PointF point)
  {
    PointF documentTopLeft = this.DocumentTopLeft;
    SizeF documentSize = this.DocumentSize;
    SizeF docExtentSize = this.DocExtentSize;
    float num1 = documentTopLeft.X + (documentSize.Width - docExtentSize.Width);
    float num2 = documentTopLeft.Y + (documentSize.Height - docExtentSize.Height);
    if ((double) num1 < (double) documentTopLeft.X)
      point.X = documentTopLeft.X;
    else if ((double) point.X > (double) num1 && (double) num1 > (double) documentTopLeft.X)
      point.X = num1;
    else if ((double) point.X < (double) documentTopLeft.X)
      point.X = documentTopLeft.X;
    if ((double) num2 < (double) documentTopLeft.Y)
      point.Y = documentTopLeft.Y;
    else if ((double) point.Y > (double) num2 && (double) num2 > (double) documentTopLeft.Y)
      point.Y = num2;
    else if ((double) point.Y < (double) documentTopLeft.Y)
      point.Y = documentTopLeft.Y;
    return point;
  }

  public override float LimitDocScale(float scale)
  {
    RectangleF documentBounds = this.ComputeDocumentBounds();
    float num1 = 1f;
    if ((double) documentBounds.Width > 0.0 && (double) documentBounds.Height > 0.0)
    {
      Size size = this.DisplayRectangle.Size;
      num1 = Math.Min((float) size.Width / documentBounds.Width, (float) size.Height / documentBounds.Height);
    }
    float num2 = Math.Min(0.1f * num1, this.PixelsPerMM);
    float num3 = Math.Max(10000f * num1, this.PixelsPerMM);
    if ((double) scale < (double) num2)
      return num2;
    return (double) scale > (double) num3 ? num3 : scale;
  }

  public override void Zoom1to1()
  {
    RectangleF documentBounds = this.ComputeDocumentBounds();
    this.DocScale = this.PixelsPerMM;
    this.DocPosition = documentBounds.Location;
  }

  public override void RescaleToFit()
  {
    RectangleF documentBounds = this.ComputeDocumentBounds();
    if (this.VerticalScrollBar != null)
      this.VerticalScrollBar.Visible = false;
    if (this.HorizontalScrollBar != null)
      this.HorizontalScrollBar.Visible = false;
    float num = this.DocScale;
    if ((double) documentBounds.Width > 0.0 && (double) documentBounds.Height > 0.0)
    {
      if (this.VerticalScrollBar != null)
        this.VerticalScrollBar.Visible = false;
      if (this.HorizontalScrollBar != null)
        this.HorizontalScrollBar.Visible = false;
      Size size = this.DisplayRectangle.Size;
      num = Math.Min((float) size.Width / documentBounds.Width, (float) size.Height / documentBounds.Height);
    }
    this.DocScale = num;
    this.DocPosition = new PointF(documentBounds.X, documentBounds.Y);
  }

  public override void ZoomToScale(PointF ptdoc, float scale)
  {
    float docScale = this.DocScale;
    float num = this.LimitDocScale(docScale * scale);
    if ((double) docScale == (double) num)
      return;
    PointF docPosition = this.DocPosition;
    Point view = this.ConvertDocToView(ptdoc);
    this.OnViewChanging();
    this.DocScale = num;
    SizeF doc = this.ConvertViewToDoc(new Size(this.ConvertDocToView(ptdoc)) - new Size(view));
    this.DocPosition = docPosition + doc;
    this.UpdateView();
  }

  public override void ZoomToBox(RectangleF docBox)
  {
    this.OnViewChanging();
    if (this.VerticalScrollBar != null)
      this.VerticalScrollBar.Visible = false;
    if (this.HorizontalScrollBar != null)
      this.HorizontalScrollBar.Visible = false;
    float num = this.DocScale;
    if ((double) docBox.Width > 0.0 && (double) docBox.Height > 0.0)
    {
      if (this.VerticalScrollBar != null)
        this.VerticalScrollBar.Visible = false;
      if (this.HorizontalScrollBar != null)
        this.HorizontalScrollBar.Visible = false;
      Size size = this.DisplayRectangle.Size;
      num = Math.Min((float) size.Width / docBox.Width, (float) size.Height / docBox.Height);
    }
    this.DocScale = num;
    this.DocPosition = new PointF(docBox.X, docBox.Y);
    this.OnViewChanging();
    this.UpdateView();
  }

  [Category("Selection")]
  [DefaultValue(1f)]
  [Description("The width of the pen used to draw the standard resize handle")]
  public override float ResizeHandlePenWidth
  {
    get
    {
      float resizeHandlePenWidth = this.myResizeHandlePenWidth;
      if (this.Document != null)
        resizeHandlePenWidth /= this.myHorizScale;
      return resizeHandlePenWidth;
    }
    set
    {
      if ((double) this.myResizeHandlePenWidth == (double) value)
        return;
      this.myResizeHandlePenWidth = value;
      this.RaisePropertyChangedEvent(nameof (ResizeHandlePenWidth));
    }
  }

  [TypeConverter(typeof (MapSizeFConverter))]
  [Category("Selection")]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Description("The default size for new resize handles.")]
  public override SizeF ResizeHandleSize
  {
    get
    {
      SizeF resizeHandleSize = this.myResizeHandleSize;
      if (this.Document != null)
        resizeHandleSize = new SizeF(resizeHandleSize.Width / this.myHorizScale, resizeHandleSize.Height / this.myVertScale);
      return resizeHandleSize;
    }
    set
    {
      if (!(this.myResizeHandleSize != value))
        return;
      this.myResizeHandleSize = value;
      this.RaisePropertyChangedEvent(nameof (ResizeHandleSize));
    }
  }

  public override IList MouseDownTools
  {
    get
    {
      if (this._mouseDownTools == null)
      {
        this._mouseDownTools = new ArrayList();
        this._mouseDownTools.Add((object) new MapToolAction((MapView) this));
        this._mouseDownTools.Add((object) new MapToolContext((MapView) this));
        this._mouseDownTools.Add((object) new MapToolPanningAcad((MapView) this));
        this._mouseDownTools.Add((object) new MapToolRelinking((MapView) this));
        this._mouseDownTools.Add((object) new MapToolResizing((MapView) this));
        this._mouseDownTools.Add((object) new MapToolLinkingNew((MapView) this));
      }
      return (IList) this._mouseDownTools;
    }
  }

  public override IList MouseMoveTools
  {
    get
    {
      if (this._mouseMoveTools == null)
      {
        this._mouseMoveTools = new ArrayList();
        this._mouseMoveTools.Add((object) new MapToolDragging((MapView) this));
        this._mouseMoveTools.Add((object) new MapToolZooming((MapView) this));
      }
      return (IList) this._mouseMoveTools;
    }
  }

  public override IList MouseUpTools
  {
    get
    {
      if (this._mouseUpTools == null)
      {
        this._mouseUpTools = new ArrayList();
        this._mouseUpTools.Add((object) new MapToolSelecting((MapView) this));
      }
      return (IList) this._mouseUpTools;
    }
  }

  public override void DoWheel(MapInputEventArgs evt)
  {
    if (evt.Delta == 0)
      return;
    if (evt.Shift)
      this.ScrollLine((float) -evt.Delta / 120f, 0.0f);
    else if (evt.Control)
    {
      this.ScrollLine(0.0f, (float) -evt.Delta / 120f);
    }
    else
    {
      float num = (float) evt.Delta / 1200f;
      this.ZoomToScale(evt.DocPoint, 1f + num);
    }
  }

  public override void ScrollLine(float dx, float dy)
  {
    PointF docPosition = this.DocPosition;
    SizeF docExtentSize = this.DocExtentSize;
    PointF documentTopLeft = this.DocumentTopLeft;
    SizeF documentSize = this.DocumentSize;
    Size scrollSmallChange = this.ScrollSmallChange;
    float num1 = dx * (float) scrollSmallChange.Width / this.myHorizScale;
    float num2 = dy * (float) scrollSmallChange.Height / this.myVertScale;
    docPosition.X += num1;
    docPosition.Y += num2;
    docPosition.X = (double) num1 <= 0.0 ? Math.Max(docPosition.X, documentTopLeft.X) : Math.Min(docPosition.X, Math.Max(documentTopLeft.X, documentTopLeft.X + documentSize.Width - docExtentSize.Width));
    docPosition.Y = (double) num2 <= 0.0 ? Math.Max(docPosition.Y, documentTopLeft.Y) : Math.Min(docPosition.Y, Math.Max(documentTopLeft.Y, documentTopLeft.Y + documentSize.Height - docExtentSize.Height));
    this.DocPosition = docPosition;
  }

  public override void UpdateScrollBars()
  {
    if (this.myUpdatingScrollBars)
      return;
    HScrollBar horizontalScrollBar = this.HorizontalScrollBar;
    VScrollBar verticalScrollBar = this.VerticalScrollBar;
    if (verticalScrollBar == null && horizontalScrollBar == null)
      return;
    Size size = this.Size;
    size.Width -= 2 * this.myBorderSize.Width;
    if (size.Width < 0)
      size.Width = 0;
    size.Height -= 2 * this.myBorderSize.Height;
    if (size.Height < 0)
      size.Height = 0;
    RectangleF rectangleF = new RectangleF(this.DocumentTopLeft, this.Document.Size);
    Rectangle rectangle = new Rectangle()
    {
      X = (int) Math.Floor((double) rectangleF.Left * (double) this.myHorizScale),
      Y = (int) Math.Floor((double) rectangleF.Top * (double) this.myVertScale)
    };
    rectangle.Width = (int) Math.Ceiling((double) rectangleF.Right * (double) this.myHorizScale) - rectangle.Left;
    rectangle.Height = (int) Math.Ceiling((double) rectangleF.Bottom * (double) this.myVertScale) - rectangle.Top;
    PointF docPosition = this.DocPosition;
    Point point = new Point();
    point.X = (int) Math.Floor((double) docPosition.X * (double) this.myHorizScale);
    point.Y = (int) Math.Floor((double) docPosition.Y * (double) this.myVertScale);
    bool flag1 = rectangle.Height > size.Height + 1 || point.Y > rectangle.Top || point.Y + size.Height + 1 < rectangle.Bottom;
    bool flag2 = verticalScrollBar != null && (this.ShowVerticalScrollBar == MapViewScrollBarVisibility.Show || this.ShowVerticalScrollBar == MapViewScrollBarVisibility.IfNeeded & flag1);
    bool flag3 = rectangle.Width > size.Width + 1 || point.X > rectangle.Left || point.X + (size.Width + 1) < rectangle.Right;
    bool flag4 = horizontalScrollBar != null && (this.ShowHorizontalScrollBar == MapViewScrollBarVisibility.Show || this.ShowHorizontalScrollBar == MapViewScrollBarVisibility.IfNeeded & flag3);
    if (flag2)
    {
      size.Width -= this.myScrollBarWidth;
      size.Width = Math.Max(0, size.Width);
    }
    if (flag4)
    {
      size.Height -= this.myScrollBarHeight;
      size.Height = Math.Max(0, size.Height);
    }
    bool flag5 = rectangle.Height > size.Height + 1 || point.Y > rectangle.Top || point.Y + size.Height + 1 < rectangle.Bottom;
    bool flag6 = verticalScrollBar != null && (this.ShowVerticalScrollBar == MapViewScrollBarVisibility.Show || this.ShowVerticalScrollBar == MapViewScrollBarVisibility.IfNeeded & flag5);
    bool flag7 = rectangle.Width > size.Width + 1 || point.X > rectangle.Left || point.X + size.Width + 1 < rectangle.Right;
    bool flag8 = horizontalScrollBar != null && (this.ShowHorizontalScrollBar == MapViewScrollBarVisibility.Show || this.ShowHorizontalScrollBar == MapViewScrollBarVisibility.IfNeeded & flag7);
    this.myUpdatingScrollBars = true;
    bool flag9 = false;
    if (verticalScrollBar != null)
    {
      int num1 = rectangle.Bottom - size.Height;
      if (point.Y > num1 && num1 > rectangle.Top)
        point.Y = num1;
      else if (point.Y < rectangle.Top)
        point.Y = rectangle.Top;
      int num2 = Math.Max(Math.Max(rectangle.Bottom, point.Y + size.Height) - 12, point.Y);
      if (verticalScrollBar.Minimum != rectangle.Top)
        verticalScrollBar.Minimum = rectangle.Top;
      if (verticalScrollBar.Maximum != num2)
        verticalScrollBar.Maximum = num2;
      if (verticalScrollBar.Value != point.Y)
        verticalScrollBar.Value = point.Y;
      if (verticalScrollBar.Visible != flag6)
        flag9 = true;
      verticalScrollBar.Visible = flag6;
      verticalScrollBar.Enabled = flag5;
    }
    if (horizontalScrollBar != null)
    {
      int num3 = rectangle.Right - size.Width;
      if (point.X > num3 && num3 > rectangle.Left)
        point.X = num3;
      else if (point.X < rectangle.Left)
        point.X = rectangle.Left;
      int num4 = Math.Max(Math.Max(rectangle.Right, point.X + size.Width) - 12, point.X);
      if (horizontalScrollBar.Minimum != rectangle.Left)
        horizontalScrollBar.Minimum = rectangle.Left;
      if (horizontalScrollBar.Maximum != num4)
        horizontalScrollBar.Maximum = num4;
      if (horizontalScrollBar.Value != point.X)
        horizontalScrollBar.Value = point.X;
      if (horizontalScrollBar.Visible != flag8)
        flag9 = true;
      horizontalScrollBar.Visible = flag8;
      horizontalScrollBar.Enabled = flag7;
    }
    this.myUpdatingScrollBars = false;
    if (!flag9)
      return;
    this.LayoutScrollBars(false);
  }
}
