
// Type: Intermech.DocumentView.ToolPanning
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.DocumentView;

/// <summary>
/// This tool supports both automatic and manual panning in a view.
/// </summary>
/// <remarks>
/// When autopanning, this remembers an initial panning point and
/// then autoscrolls the view in the direction of the
/// current mouse point relative to the original panning point.
/// This tool can be used in either a modal or a mode-less manner.
/// To use modally, where the first mouse click will establish the
/// panning origin, mouse moves determine autopanning direction
/// and speed, and the second mouse up will stop the tool:
/// <code>
/// aView.Tool = new GoToolPanning(aView);
/// </code>
/// If you set the <see cref="P:Intermech.Map.MapToolPanning.Origin" /> before the tool starts,
/// the first mouse click is not needed.
/// <code>
/// GoToolPanning tool = new GoToolPanning(aView);
/// tool.Origin = aView.LastInput.ViewPoint;  // or another point in the view
/// aView.Tool = tool;
/// </code>
/// <para>
/// It is started mode-lessly when the user presses the middle
/// mouse button, which is normally the mouse wheel.  An instance
/// of this tool is in the <see cref="P:Intermech.Map.MapView.MouseDownTools" /> list.
/// </para>
/// <para>
/// However, in ASP.NET WebForms, the panning gesture consists of
/// only a single mouse-down, drag, mouse-up.  Since mouse moves
/// are only simulated on WebForms, and auto-panning is not
/// possible with no mouse time information,
/// a simpler gesture is easier to use.  This results in just a
/// single scroll, according to the distance and direction
/// between the <c>FirstInput.ViewPoint</c> and <c>LastInput.ViewPoint</c>.
/// </para>
/// <para>
/// For manual panning, you will need to create a separate instance
/// of this class and set <see cref="P:Intermech.Map.MapToolPanning.AutoPan" /> to false.
/// Then the user's left-mouse down and drag and up will pan the view.
/// When you set the <see cref="P:Intermech.Map.MapToolPanning.Modal" /> property, this tool will
/// remain in this mode even after the user does a mouse up.
/// They can cancel this mode by pressing the Cancel key.
/// </para>
/// <para>
/// By default no manual-panning tool is installed in a <see cref="T:Intermech.Map.MapView" />.
/// To implement a "Manual Pan" command:
/// <code>
/// GoToolPanning panningtool = new GoToolPanning(myView);
/// panningtool.AutoPan = false;
/// panningtool.Modal = true;
/// myView.Tool = panningtool;
/// </code>
/// However, if you do not need the user to do multiple selections by
/// using the <see cref="T:Intermech.Map.MapToolRubberBanding" /> tool,
/// you may find it nicer to use the manual panning tool
/// in a mode-less manner, so that the user can use all the other standard tools
/// in a natural fashion.
/// <code>
/// GoToolPanning panningtool = new GoToolPanning(myView);
/// panningtool.AutoPan = false;
/// myView.MouseDownTools.Add(panningtool);
/// </code>
/// Both <see cref="T:Intermech.Map.MapToolRubberBanding" /> and this <see cref="T:Intermech.Map.MapToolPanning" />
/// (when <see cref="P:Intermech.Map.MapToolPanning.AutoPan" /> is false) are started when the user does a
/// mouse-down and drag in the background, so those two tools would conflict.
/// But the user can still do multiple selections by using Shift- or Control-click.
/// </para>
/// </remarks>
[Serializable]
/// <summary>The standard tool constructor.</summary>
/// <param name="v"></param>
public class ToolPanning(IView v) : AbstractTool(v)
{
  [NonSerialized]
  private bool myActive;
  private bool myModal;
  private bool myAutoPan = true;
  [NonSerialized]
  private Point myLastViewPoint;
  [NonSerialized]
  private Point myOrigin;
  [NonSerialized]
  private bool myOriginSet;
  [NonSerialized]
  private PaintEventHandler myPaintHandler;

  /// <summary>
  /// This tool can start when the middle mouse button is pressed.
  /// </summary>
  /// <returns></returns>
  public override bool CanStart()
  {
    InputEventArgs lastInput = this.LastInput;
    if (lastInput.Alt || lastInput.Control || lastInput.Shift)
      return false;
    if (this.AutoPan)
      return lastInput.Buttons == MouseButtons.Middle;
    return lastInput.Buttons == MouseButtons.Left && this.View.PickObject(true, false, lastInput.DocPoint, true) == null;
  }

  /// <summary>Stop panning whenever any key is pressed.</summary>
  public override void DoKeyDown() => this.StopTool();

  private void DoManualPan()
  {
    PointF docPosition = this.View.DocPosition;
    Size s;
    ref Size local = ref s;
    Point viewPoint = this.LastInput.ViewPoint;
    int width = viewPoint.X - this.myLastViewPoint.X;
    viewPoint = this.LastInput.ViewPoint;
    int height = viewPoint.Y - this.myLastViewPoint.Y;
    local = new Size(width, height);
    SizeF doc = this.View.ConvertViewToDoc(s);
    this.myLastViewPoint = this.LastInput.ViewPoint;
    this.View.DocPosition = new PointF(docPosition.X + doc.Width, docPosition.Y + doc.Height);
  }

  /// <summary>
  /// When manually panning (i.e. <see cref="P:Intermech.Map.MapToolPanning.AutoPan" /> is false),
  /// a mouse down causes future mouse moves to change the view's
  /// <see cref="P:Intermech.Map.MapView.DocPosition" /> to move along with the mouse.
  /// </summary>
  public override void DoMouseDown()
  {
    if (this.AutoPan)
      base.DoMouseDown();
    else
      this.Active = true;
  }

  /// <summary>
  /// Call <see cref="M:Intermech.Map.MapView.DoAutoPan(System.Drawing.Point,System.Drawing.Point)" /> to pan the view according to
  /// the current mouse point relative to the <see cref="P:Intermech.Map.MapToolPanning.Origin" />.
  /// </summary>
  /// <remarks>
  /// When autopanning, until the <see cref="P:Intermech.Map.MapToolPanning.Origin" /> panning point is set, this method
  /// does nothing.
  /// When manually panning, this changes the view's <see cref="P:Intermech.Map.MapView.DocPosition" />
  /// along with changes of the mouse position in the view, thereby panning the view.
  /// However, when <see cref="P:Intermech.Map.MapToolPanning.AutoPan" /> is false and <see cref="P:Intermech.Map.MapToolPanning.Modal" /> is true,
  /// mouse moves have no effect unless a drag is in progress--i.e. after a mouse down.
  /// </remarks>
  public override void DoMouseMove()
  {
    if (this.AutoPan)
    {
      if (!this.myOriginSet)
        return;
      Size size = new Size(16 /*0x10*/, 16 /*0x10*/);
      int width = size.Width;
      int height = size.Height;
      int x1 = this.LastInput.ViewPoint.X;
      Point point = this.Origin;
      int x2 = point.X;
      int num1 = x1 - x2;
      point = this.LastInput.ViewPoint;
      int y1 = point.Y;
      point = this.Origin;
      int y2 = point.Y;
      int num2 = y1 - y2;
      if (num1 < -width)
      {
        if (num2 < -height)
          this.View.Cursor = Cursors.PanNW;
        else if (num2 > height)
          this.View.Cursor = Cursors.PanSW;
        else
          this.View.Cursor = Cursors.PanWest;
      }
      else if (num1 > width)
      {
        if (num2 < -height)
          this.View.Cursor = Cursors.PanNE;
        else if (num2 > height)
          this.View.Cursor = Cursors.PanSE;
        else
          this.View.Cursor = Cursors.PanEast;
      }
      else if (num2 < -height)
        this.View.Cursor = Cursors.PanNorth;
      else if (num2 > height)
        this.View.Cursor = Cursors.PanSouth;
      else
        this.View.Cursor = Cursors.NoMove2D;
      this.View.DoAutoPan(this.Origin, this.LastInput.ViewPoint);
    }
    else if (!this.Active)
    {
      if (this.Modal)
        return;
      this.Active = true;
    }
    else
      this.DoManualPan();
  }

  /// <summary>
  /// On the first mouse up, set the <see cref="P:Intermech.Map.MapToolPanning.Origin" /> point and display
  /// the panning origin marker; on the second second mouse up, stop this tool.
  /// </summary>
  /// <remarks>
  /// When autopanning, if the <see cref="P:Intermech.Map.MapToolPanning.Origin" /> has already been set, a mouse up just
  /// stops this tool.
  /// For WebForms, the behavior is different--this method always sets
  /// the <see cref="P:Intermech.Map.MapToolPanning.Origin" /> to the <c>FirstInput.ViewPoint</c>,
  /// scrolls the view according to <see cref="M:Intermech.Map.MapView.ComputeAutoPanDocPosition(System.Drawing.Point,System.Drawing.Point)" />,
  /// and then stops this tool.
  /// When manually panning, a mouse up just stops this tool, unless
  /// <see cref="P:Intermech.Map.MapToolPanning.Modal" /> is true, in which case this tool waits for a mouse
  /// down again to start panning during mouse moves.
  /// </remarks>
  public override void DoMouseUp()
  {
    if (this.AutoPan)
    {
      if (!this.myOriginSet)
      {
        this.Origin = this.LastInput.ViewPoint;
        this.SetPaintingOriginMarker(true);
      }
      else
        this.StopTool();
    }
    else if (this.Modal)
      this.Active = false;
    else
      this.StopTool();
  }

  /// <summary>Stop panning whenever the mouse wheel turns.</summary>
  public override void DoMouseWheel() => this.StopTool();

  private void HandlePaint(object sender, PaintEventArgs evt)
  {
    Cursor noMove2D = Cursors.NoMove2D;
    int width = noMove2D.Size.Width;
    int height = noMove2D.Size.Height;
    noMove2D.Draw(evt.Graphics, this.OriginRect);
  }

  private void SetPaintingOriginMarker(bool b)
  {
    if (b)
    {
      this.myPaintHandler = new PaintEventHandler(this.HandlePaint);
      this.View.Paint += this.myPaintHandler;
      this.View.Invalidate(this.OriginRect);
    }
    else
    {
      if (this.myPaintHandler == null)
        return;
      this.View.Paint -= this.myPaintHandler;
      this.myPaintHandler = (PaintEventHandler) null;
      this.View.Invalidate(this.OriginRect);
    }
  }

  /// <summary>Initialize this tool.</summary>
  public override void Start()
  {
    if (this.AutoPan)
    {
      this.View.Cursor = Cursors.NoMove2D;
      if (!this.myOriginSet)
        return;
      this.SetPaintingOriginMarker(true);
    }
    else
      this.View.Cursor = Cursors.SizeAll;
  }

  /// <summary>
  /// Stop any auto-panning in the view and remove the original panning point marker.
  /// </summary>
  public override void Stop()
  {
    if (this.AutoPan)
    {
      this.myOriginSet = false;
      this.View.StopAutoScroll();
      this.View.Cursor = this.View.GetDefaultCursor();
      this.SetPaintingOriginMarker(false);
    }
    else
    {
      this.Active = false;
      this.View.Cursor = this.View.GetDefaultCursor();
    }
  }

  private bool Active
  {
    get => this.myActive;
    set
    {
      if (this.myActive == value)
        return;
      this.myActive = value;
      if (!value)
        return;
      this.myLastViewPoint = this.LastInput.ViewPoint;
    }
  }

  /// <summary>
  /// Gets or sets whether this tool is used to implement autopanning or manual panning.
  /// </summary>
  /// <value>the initial value is true</value>
  /// <remarks>
  /// When this value is true, this tool implements the standard
  /// auto-scrolling panning, initiated by a middle-mouse button click and
  /// terminated by a second click or a key press.
  /// When this value is false, this tool implements the standard
  /// manual-scrolling panning, initiated by a left-mouse drag in the background
  /// and terminated by a mouse up (when <see cref="P:Intermech.Map.MapToolPanning.Modal" /> is false).
  /// </remarks>
  public virtual bool AutoPan
  {
    get => this.myAutoPan;
    set => this.myAutoPan = value;
  }

  /// <summary>
  /// Gets or sets whether this tool is used in a modal fashion when <see cref="P:Intermech.Map.MapToolPanning.AutoPan" /> is false.
  /// </summary>
  /// <value>the initial value is false</value>
  /// <remarks>
  /// This property is ignored when <see cref="P:Intermech.Map.MapToolPanning.AutoPan" /> is true.
  /// <para>
  /// When you add an instance of this non-autopanning tool to the <see cref="P:Intermech.Map.MapView.MouseDownTools" />,
  /// you are using it in a non-Modal fashion.
  /// <code>
  /// GoToolPanning panningtool = new GoToolPanning(myView);
  /// panningtool.AutoPan = false;
  /// myView.MouseDownTools.Add(panningtool);
  /// </code>
  /// This allows the user to use the other mode-less mouse tools,
  /// such as selecting, dragging, resizing, and linking as well as this manual panning tool
  /// when the user does a mouse down in the background.
  /// </para>
  /// <para>
  /// If you use this manual panning tool in a modal fashion, the user
  /// will remain in this tool, able to pan the view whenever the user drags
  /// the mouse anywhere in the view.
  /// <code>
  /// GoToolPanning panningtool = new GoToolPanning(myView);
  /// panningtool.AutoPan = false;
  /// panningtool.Modal = true;
  /// myView.Tool = panningtool;
  /// </code>
  /// The user can leave this mode by pressing the Cancel key or any other key.
  /// </para>
  /// </remarks>
  public virtual bool Modal
  {
    get => this.myModal;
    set => this.myModal = value;
  }

  /// <summary>Gets or sets the original panning point.</summary>
  /// <value>
  /// This is a point in view coordinates.
  /// It is set on the first mouse up.
  /// Once this value is set, mouse moves cause
  /// </value>
  /// <remarks>
  /// This is only relevant when <see cref="P:Intermech.Map.MapToolPanning.AutoPan" /> is true.
  /// </remarks>
  public Point Origin
  {
    get => this.myOrigin;
    set
    {
      if (!(this.myOrigin != value))
        return;
      this.myOrigin = value;
      this.myOriginSet = true;
    }
  }

  private Rectangle OriginRect
  {
    get
    {
      Cursor noMove2D = Cursors.NoMove2D;
      int width1 = noMove2D.Size.Width;
      int height1 = noMove2D.Size.Height;
      Point origin = this.Origin;
      int x = origin.X - width1 / 2;
      origin = this.Origin;
      int y = origin.Y - height1 / 2;
      int width2 = width1;
      int height2 = height1;
      return new Rectangle(x, y, width2, height2);
    }
  }
}
