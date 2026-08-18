
// Type: Intermech.Controls.ZoomAndPan
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;


namespace Intermech.Controls;

/// <summary>
/// This class is here to add zooming and panning support for user controls.
/// </summary>
/// <remarks>
/// Well, that's what it's here for, but at present it only supports the zoom functionality.
/// </remarks>
public abstract class ZoomAndPan : UserControl
{
  /// <summary>Store the zoom factor</summary>
  private float _zoom = 1f;
  /// <summary>Store the update flag</summary>
  private bool _updating = true;
  /// <summary>Store the size of the page</summary>
  private Size _pageSize = new Size(100, 100);

  /// <summary>Setup defaults for the control</summary>
  public ZoomAndPan()
  {
    this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
    this.SetStyle(ControlStyles.Selectable, false);
    this.AutoScroll = true;
    this.AutoScrollMinSize = new Size(0, 0);
    this._pageSize = new Size(100, 100);
  }

  /// <summary>Get/Set the zoom factor</summary>
  /// <remarks>
  /// A zoom factor of 1.0 is 100%, 2.0 is 200%, 0.5 is 50% and so on
  /// </remarks>
  [Description("The zoom level. 1.0 is no zoom, 2.0 is double size, 0.5 is half size")]
  [DefaultValue(1f)]
  [Category("Appearance")]
  public float Zoom
  {
    get => this._zoom;
    set
    {
      this._zoom = value;
      if (!this._updating)
        return;
      this.RecalculateDisplay(true);
    }
  }

  /// <summary>Get/Set the page size</summary>
  /// <remarks>
  /// The page size is used to compute the scroll bar positions, and should be set to the limits of the scroll area
  /// </remarks>
  [Description("Get/Set the size of the page - i.e. the bounds of the view")]
  [Category("Appearance")]
  [DefaultValue(typeof (Size), "100,100")]
  public Size PageSize
  {
    get => this._pageSize;
    set
    {
      this._pageSize = value;
      if (!this._updating)
        return;
      this.RecalculateDisplay(true);
    }
  }

  /// <summary>Lock screen updates so that no repainting ocurrs</summary>
  public void BeginUpdate() => this._updating = false;

  /// <summary>Unlock screen updates and redraw the screen</summary>
  public void EndUpdate()
  {
    this._updating = true;
    this.RecalculateDisplay(true);
  }

  /// <summary>
  /// Get the Updating flag which is false if screen updates are off
  /// </summary>
  public bool Updating => this._updating;

  /// <summary>
  /// Method which is called from the OnPaint method to render the contents that need to
  /// be displayed, based on the zoom factor and scroll position of the control
  /// </summary>
  /// <remarks>
  /// When using this control as a base class, you no longer code in the OnPaint method. Instead
  /// you should override RenderGraphics and do your painting there. This ensures that the appropriate
  /// modifications have already been made to the passed Graphics object (scale and transformation), so
  /// all you need to worry about is the placement of your objects - this class handles the rest.
  /// </remarks>
  /// <param name="renderRect">The rectangle being rendered</param>
  /// <param name="g">The graphics object to use for the rendering</param>
  public abstract void RenderGraphics(Rectangle renderRect, Graphics g);

  /// <summary>
  /// Change the zoom factor. Override to provide your own zoom increments
  /// </summary>
  /// <remarks>
  /// This method is designed to be overridden to control the zoom factor of the display. The
  /// default implementation doubles or halves the size of the display depending on whether the
  /// user is zooming in (double) our out (half).
  /// </remarks>
  /// <param name="zoomIn"></param>
  public virtual void ChangeZoom(bool zoomIn)
  {
    if (zoomIn)
    {
      if ((double) this.Zoom <= 0.0099999997764825821)
        return;
      this.Zoom *= 0.5f;
    }
    else
      this.Zoom *= 2f;
  }

  /// <summary>Paint the contents of the control</summary>
  /// <remarks>
  /// This method alters the graphics transformation mode to take account of scaling and scroll positions.
  /// It then calls the RenderGraphics method to actually do the rendering.
  /// </remarks>
  /// <param name="e"></param>
  protected override void OnPaint(PaintEventArgs e)
  {
    if (!this._updating)
      return;
    base.OnPaint(e);
    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
    e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
    Matrix matrix = new Matrix();
    matrix.Translate((float) this.AutoScrollPosition.X, (float) this.AutoScrollPosition.Y);
    matrix.Scale(this.Zoom, this.Zoom);
    e.Graphics.Transform = matrix;
    Point[] pts = new Point[2]
    {
      e.ClipRectangle.Location,
      new Point(e.ClipRectangle.Size)
    };
    e.Graphics.TransformPoints(CoordinateSpace.World, CoordinateSpace.Page, pts);
    Rectangle renderRect = new Rectangle(pts[0], new Size(pts[1]));
    renderRect.Inflate(2, 2);
    this.RenderGraphics(renderRect, e.Graphics);
  }

  /// <summary>
  /// Process the mouse wheel message. This will zoom in/out
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseWheel(MouseEventArgs e)
  {
    if (!this._updating)
      return;
    this.ChangeZoom(e.Delta < 0);
  }

  /// <summary>
  /// Method to update the display based on the size of the page and the zooming factor
  /// </summary>
  /// <param name="invalidate">If true, invalidates the control to redraw all of the content</param>
  protected void RecalculateDisplay(bool invalidate)
  {
    SizeF sizeF;
    ref SizeF local = ref sizeF;
    Size pageSize = this.PageSize;
    double width = (double) (pageSize.Width + 2) * (double) this.Zoom;
    pageSize = this.PageSize;
    double height = (double) (pageSize.Height + 2) * (double) this.Zoom;
    local = new SizeF((float) width, (float) height);
    this.AutoScrollMinSize = sizeF.ToSize();
    if (!invalidate)
      return;
    this.Invalidate();
  }
}
