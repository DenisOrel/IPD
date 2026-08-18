
// Type: Intermech.DocumentView.ToolRubberBanding
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Intermech.DocumentView;

/// <summary>
/// The tool used to handle a user's background drag to do a multiple selection.
/// </summary>
/// <remarks>
/// No transaction is performed by this tool, although it is possible
/// (but unconventional) that <see cref="M:Intermech.Map.MapToolRubberBanding.DoRubberBand(System.Drawing.Rectangle)" /> might be
/// overridden to perform one.
/// This tool is normally used as a modeless tool, one of the view's mouse tools,
/// that can be started upon a mouse move (<see cref="P:Intermech.Map.MapView.MouseMoveTools" />).
/// When the <see cref="P:Intermech.Map.MapToolRubberBanding.Modal" /> property is set to true,
/// this tool waits for a mouse down at which <see cref="M:Intermech.Map.MapToolRubberBanding.CanStart" />
/// returns true before drawing the rubber-band box.
/// </remarks>
[Serializable]
public class ToolRubberBanding(IView view) : AbstractTool(view)
{
  private static Cursor _zoomCursor;
  [NonSerialized]
  private bool _active;
  private bool _modal;
  [NonSerialized]
  private Rectangle _box;

  private static Cursor ZoomCursor
  {
    get
    {
      if (ToolRubberBanding._zoomCursor == (Cursor) null)
      {
        Stream manifestResourceStream = typeof (ToolRubberBanding).Assembly.GetManifestResourceStream("Intermech.Client.Core.DocumentView.ZoomWindow.cur");
        if (manifestResourceStream != null)
        {
          ToolRubberBanding._zoomCursor = new Cursor(manifestResourceStream);
          manifestResourceStream.Close();
        }
        else
          ToolRubberBanding._zoomCursor = Cursors.Cross;
      }
      return ToolRubberBanding._zoomCursor;
    }
  }

  private void Activate()
  {
    this._active = true;
    Point viewPoint = this.FirstInput.ViewPoint;
    int x = viewPoint.X;
    viewPoint = this.FirstInput.ViewPoint;
    int y = viewPoint.Y;
    this.Box = new Rectangle(x, y, 0, 0);
    if (this.FirstInput.Shift || this.Selection.IsEmpty)
      return;
    this.Selection.Clear();
    this.View.Refresh();
  }

  /// <summary>
  /// This tool can start if the user can select objects in this view and the
  /// input event point is not over a selectable document object.
  /// </summary>
  /// <returns></returns>
  public override bool CanStart()
  {
    if (!this.View.CanSelectObjects() || this.LastInput.Buttons != MouseButtons.Left)
      return false;
    Size dragSize = this.DragSize;
    Point viewPoint1 = this.FirstInput.ViewPoint;
    Point viewPoint2 = this.LastInput.ViewPoint;
    return (Math.Abs(viewPoint1.X - viewPoint2.X) > dragSize.Width / 2 || Math.Abs(viewPoint1.Y - viewPoint2.Y) > dragSize.Height / 2) && this.View.PickObject(true, false, this.FirstInput.DocPoint, true) == null;
  }

  /// <summary>
  /// This method is called to compute the latest bounds of the <see cref="P:Intermech.Map.MapToolRubberBanding.Box" />.
  /// </summary>
  /// <returns>a <c>Rectangle</c> in view coordinates</returns>
  public virtual Rectangle ComputeRubberBandBox()
  {
    Point viewPoint1 = this.FirstInput.ViewPoint;
    Point viewPoint2 = this.LastInput.ViewPoint;
    return new Rectangle(Math.Min(viewPoint2.X, viewPoint1.X), Math.Min(viewPoint2.Y, viewPoint1.Y), Math.Abs(viewPoint2.X - viewPoint1.X), Math.Abs(viewPoint2.Y - viewPoint1.Y));
  }

  /// <summary>
  /// This starts keeping track of the <see cref="P:Intermech.Map.MapToolRubberBanding.Box" />'s bounds.
  /// </summary>
  /// <remarks>
  /// Unless the <see cref="P:Intermech.Map.MapInputEventArgs.Shift" /> modifier is true,
  /// we also clear the view's selection.
  /// </remarks>
  public override void DoMouseDown()
  {
    if (!this.CanStart())
      return;
    this.Activate();
  }

  /// <summary>
  /// As the mouse is dragged, we display the rubber band box.
  /// </summary>
  /// <remarks>
  /// If <see cref="P:Intermech.Map.MapToolRubberBanding.Modal" /> is true, we skip all mouse moves and mouse ups
  /// until a mouse down occurs when <see cref="M:Intermech.Map.MapToolRubberBanding.CanStart" /> returns true.
  /// If <see cref="P:Intermech.Map.MapToolRubberBanding.Modal" /> is false, a mouse move starts keeping track
  /// of the rubber-band <see cref="P:Intermech.Map.MapToolRubberBanding.Box" />.
  /// </remarks>
  public override void DoMouseMove()
  {
    if (!this._active)
    {
      if (this.Modal)
        return;
      this.Activate();
    }
    else
    {
      this.View.Cursor = ToolRubberBanding.ZoomCursor;
      this.Box = this.ComputeRubberBandBox();
      this.View.DrawXorBox(this.Box, true);
    }
  }

  /// <summary>
  /// When the mouse is released, we remove the rubber band box, call <see cref="M:Intermech.Map.MapToolRubberBanding.DoRubberBand(System.Drawing.Rectangle)" />,
  /// and stop this tool if <see cref="P:Intermech.Map.MapToolRubberBanding.Modal" /> is false.
  /// </summary>
  public override void DoMouseUp()
  {
    if (this._active)
    {
      this.Box = this.ComputeRubberBandBox();
      this.DoRubberBand(this.Box);
    }
    this.StopTool();
  }

  /// <summary>
  /// This method is called as part of the mouse up event, normally to select
  /// the objects within the <paramref name="box" />.
  /// </summary>
  /// <param name="box">a <c>Rectangle</c> describing what the user outlined, in view coordinates</param>
  /// <remarks>
  /// By default this will call <see cref="M:Intermech.Map.MapView.SelectInRectangle(System.Drawing.RectangleF)" />, after converting
  /// the <paramref name="box" /> into document coordinates.
  /// If the box is too small in width and height, this acts like a normal mouse click instead.
  /// </remarks>
  public virtual void DoRubberBand(Rectangle box)
  {
    Size dragSize = this.DragSize;
    if (box.Width <= dragSize.Width / 2 && box.Height <= dragSize.Height / 2)
    {
      this.DoSelect(this.LastInput);
      this.DoClick(this.LastInput);
    }
    else
    {
      RectangleF doc = this.View.ConvertViewToDoc(box);
      this.View.ZoomToBox(doc);
      this.View.SelectInRectangle(doc);
    }
  }

  /// <summary>Remove the rubber band box from the view.</summary>
  public override void Stop()
  {
    this.View.DrawXorBox(this.Box, false);
    this.View.Cursor = this.View.GetDefaultCursor();
    this._active = false;
  }

  /// <summary>
  /// Gets or sets the rectangle that the user has drawn so far.
  /// </summary>
  /// <value>
  /// This <c>Rectangle</c> is in view coordinates.
  /// You should call <see cref="M:Intermech.Map.MapView.ConvertViewToDoc(System.Drawing.Rectangle)" /> to convert
  /// these view coordinates into document coordinates so that you can
  /// select any objects within the rectangle corresponding to this box.
  /// It is initially a zero size rectangle at the mouse down point.
  /// </value>
  /// <remarks>
  /// This is normally set to the value last computed by <see cref="M:Intermech.Map.MapToolRubberBanding.ComputeRubberBandBox" />.
  /// </remarks>
  /// <seealso cref="M:Intermech.Map.MapToolRubberBanding.DoRubberBand(System.Drawing.Rectangle)" />
  public Rectangle Box
  {
    get => this._box;
    set => this._box = value;
  }

  /// <summary>
  /// Gets or sets whether this tool should wait for a mouse-down before
  /// drawing a rubber-band box.
  /// </summary>
  /// <value>The default value is false.</value>
  public virtual bool Modal
  {
    get => this._modal;
    set => this._modal = value;
  }
}
