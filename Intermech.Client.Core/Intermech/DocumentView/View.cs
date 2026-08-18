
// Type: Intermech.DocumentView.View
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.DocumentView;

public class View : Control, IView
{
  private IDocument _document;
  [NonSerialized]
  private Size _borderSize = SystemInformation.Border3DSize;
  private BorderStyle _borderStyle;
  private PointF _origin;
  private SizeF _shadowOffset;
  private bool _showsNegativeCoordinates;
  [NonSerialized]
  private bool _cancelMouseDown;
  private ITool _tool;
  private ITool _defaultTool;
  private InputEventArgs _firstInput = new InputEventArgs();
  private InputEventArgs _lastInput = new InputEventArgs();
  private bool _allowMouse = true;
  private ArrayList _mouseDownTools;
  private ArrayList _mouseMoveTools;
  private ArrayList _mouseUpTools;
  private bool mySelectsByFirstChar;
  private int _autoScrollDelay = 1000;
  private int _autoScrollTime = 100;
  private ViewGridStyle _gridStyle;
  private Point _hoverPoint;
  [NonSerialized]
  private System.Threading.Timer _hoverTimer;
  [NonSerialized]
  private bool _hoverTimerEnabled;
  private int _hoverDelay = 1000;
  [NonSerialized]
  private bool _panning;
  [NonSerialized]
  private Point _panningOrigin;
  [NonSerialized]
  private Point _autoScrollPoint;
  [NonSerialized]
  private Cursor _defaultCursor;
  [NonSerialized]
  private System.Threading.Timer _autoScrollTimer;
  [NonSerialized]
  private bool _autoScrollTimerEnabled;
  private ISelection _selection = new ISelection();
  private Size myAutoPanRegion;
  private Rectangle _prevXorRect;
  private bool _prevXorRectValid;
  private Size _autoScrollRegion = new Size(SystemInformation.VerticalScrollBarWidth, SystemInformation.HorizontalScrollBarHeight);
  [NonSerialized]
  private ToolTip _toolTip;
  [NonSerialized]
  internal Bitmap _cacheImage;
  [NonSerialized]
  internal bool _cacheValid;
  private int _suppressPaint;
  [NonSerialized]
  internal PaintEventArgs _paintEventArgs;
  private Border3DStyle _border3DStyle;
  private InterpolationMode _interpolationMode;
  private TextRenderingHint _textRenderingHint;
  private SmoothingMode _smoothingMode;
  [NonSerialized]
  private SolidBrush _backgroundBrush;
  private bool _allowSelect = true;
  private bool _updatingScrollBars;
  private int myScrollBarHeight = SystemInformation.HorizontalScrollBarHeight;
  private int myScrollBarWidth = SystemInformation.VerticalScrollBarWidth;
  private Size myScrollSmallChange;
  private float _horizScale;
  private HScrollBar _horizScroll = new HScrollBar();
  private ScrollEventHandler _horizScrollHandler;
  private ViewScrollBarVisibility _showHorizScroll;
  private float _vertScale;
  private VScrollBar _vertScroll = new VScrollBar();
  private ScrollEventHandler _vertScrollHandler;
  private ViewScrollBarVisibility _showVertScroll;
  private Control _corner = new Control();

  /// <summary>
  /// The user performed a double click on the background, not over any document object.
  /// </summary>
  public event InputEventHandler BackgroundDoubleClicked;

  /// <summary>
  /// The user hovered over the background, not over any document object.  [Not in GoDiagram Pocket]
  /// </summary>
  public event InputEventHandler BackgroundHover;

  public event EventHandler ViewChanged;

  public event EventHandler ViewChanging;

  /// <summary>
  /// A document object was hovered over by the user.  [Not in GoDiagram Pocket]
  /// </summary>
  public event ObjectEventHandler ObjectHover;

  internal void SafeOnDocumentChanged(object sender, DocumentChangedEventArgs e)
  {
    this.OnDocumentChanged(sender, e);
  }

  private void OnDocumentChanged(object sender, DocumentChangedEventArgs e) => this.UpdateView();

  /// <summary>
  /// Add all eligible document objects that are within a given rectangle to this view's selection.
  /// </summary>
  /// <param name="rect">A <c>RectangleF</c> in document coordinates.</param>
  /// <remarks>
  /// This method only selects document objects.
  /// It heeds <see cref="M:Intermech.Map.MapView.CanSelectObjects" />,
  /// <see cref="M:Intermech.Map.MapLayer.CanViewObjects" />, <see cref="M:Intermech.Map.MapLayer.CanSelectObjects" />,
  /// <see cref="M:Intermech.Map.MapObject.CanView" />, and <see cref="M:Intermech.Map.MapObject.CanSelect" />.
  /// This actually checks to see if the whole <see cref="P:Intermech.Map.MapObject.SelectionObject" />
  /// is within the <paramref name="rect" /> bounds.  Such a policy allows a
  /// <see cref="T:Intermech.Map.MapGroup" /> to be selected even though only one part of the group
  /// is in the rectangle, the <see cref="P:Intermech.Map.MapObject.SelectionObject" /> that appears
  /// to the user to be selected.
  /// This will consider the children of <see cref="T:Intermech.Map.MapGroup" />s.
  /// Once it finds a selectable object within the rectangle,
  /// it does not recurse further into that object.
  /// </remarks>
  public virtual void SelectInRectangle(RectangleF rect)
  {
  }

  /// <summary>
  /// Draw a rectangle on the screen in XOR mode.  [Not in GoDiagram Pocket]
  /// </summary>
  /// <param name="rect"></param>
  /// <remarks>
  /// <para>
  /// This is not supported in GoDiagram Pocket, because the .NET Compact
  /// Framework does not support drawing in XOR mode.
  /// </para>
  /// The parameter is in view coordinates.
  /// You should call this method twice for each set of rectangular coordinates--
  /// once to draw the rectangle and once to restore the original screen image.
  /// </remarks>
  public void DrawXorRectangle(Rectangle rect)
  {
    Rectangle screen = this.RectangleToScreen(rect);
    Color color = Color.Black;
    if (color == Color.Empty)
      color = this.BackColor;
    Color backColor = color;
    ControlPaint.DrawReversibleFrame(screen, backColor, FrameStyle.Dashed);
  }

  /// <summary>
  /// This convenience method erases any previous XOR-drawn rectangle and then
  /// may draw a new one with the given dimensions.
  /// </summary>
  /// <param name="rect">The size and location of the rectangle to draw, in view coordinates.</param>
  /// <param name="drawnew">Whether to draw the new XOR rectangle.</param>
  /// <remarks>
  /// This always erases any earlier XOR rectangle drawn by this method.
  /// It only draws a new rectangle if <paramref name="drawnew" /> is true.
  /// <para>
  /// In GoDiagram Pocket, this draws a gray rectangle, because the .NET Compact
  /// Framework does not support drawing in XOR mode.
  /// </para>
  /// </remarks>
  public virtual void DrawXorBox(Rectangle rect, bool drawnew)
  {
    if (this._prevXorRectValid)
    {
      try
      {
        this.DrawXorRectangle(this._prevXorRect);
      }
      catch (SecurityException ex)
      {
        this.Refresh();
      }
      this._prevXorRectValid = false;
    }
    if (!drawnew)
      return;
    try
    {
      this.DrawXorRectangle(rect);
    }
    catch (SecurityException ex)
    {
      Graphics graphics = this.CreateGraphics();
      graphics.DrawRectangle(SystemPens.ControlDark, rect.X, rect.Y, rect.Width, rect.Height);
      graphics.Dispose();
    }
    this._prevXorRect = rect;
    this._prevXorRectValid = true;
  }

  /// <summary>
  /// Gets or sets whether the user can use the mouse in this view.
  /// </summary>
  /// <remarks>
  /// A false value prevents the user from using the mouse in this view
  /// by the normal mechanisms.
  /// Your code can always handle mouse events programmatically by adding
  /// mouse event handlers to this control or by overriding
  /// <see cref="M:Intermech.Map.MapView.OnMouseDown(System.Windows.Forms.MouseEventArgs)" />, <see cref="M:Intermech.Map.MapView.OnMouseMove(System.Windows.Forms.MouseEventArgs)" />,
  /// <see cref="M:Intermech.Map.MapView.OnMouseUp(System.Windows.Forms.MouseEventArgs)" />, or <see cref="M:Intermech.Map.MapView.OnDoubleClick(System.EventArgs)" />.
  /// </remarks>
  [DefaultValue(true)]
  [Description("Whether the user can use the mouse in this view.")]
  [Category("Behavior")]
  public bool AllowMouse
  {
    get => this._allowMouse;
    set
    {
      if (this._allowMouse == value)
        return;
      this._allowMouse = value;
      this.RaisePropertyChangedEvent(nameof (AllowMouse));
    }
  }

  /// <summary>
  /// Gets the canonical event args information for the last mouse down.
  /// </summary>
  [Browsable(false)]
  public InputEventArgs FirstInput => this._firstInput;

  /// <summary>
  /// Gets the canonical event args information for the last mouse or keyboard input.
  /// </summary>
  /// <remarks>
  /// The last input event args information is used by the tools, the view, and objects
  /// to decide how to behave.  Typically you will use <see cref="P:Intermech.Map.MapInputEventArgs.DocPoint" />
  /// to see where an event occurred, or <see cref="P:Intermech.Map.MapInputEventArgs.Control" /> to see
  /// if the Ctrl key was held down at the time of the event.
  /// </remarks>
  [Browsable(false)]
  public InputEventArgs LastInput => this._lastInput;

  /// <summary>This method is the view's mouse down event handler.</summary>
  /// <param name="evt"></param>
  /// <remarks>
  /// This method makes sure the <see cref="P:Intermech.Map.MapView.LastInput" /> and
  /// <see cref="P:Intermech.Map.MapView.FirstInput" /> canonicalized input property values
  /// have up-to-date information describing this mouse input event.
  /// It then calls <see cref="M:Intermech.Map.MapView.DoMouseDown" /> to allow tools
  /// to handle the input event, and then finally calls
  /// the base method to invoke all of the <c>MouseDown</c> event handlers.
  /// </remarks>
  protected override void OnMouseDown(MouseEventArgs evt)
  {
    InputEventArgs lastInput = this.LastInput;
    if (this.AllowMouse)
    {
      lastInput.ViewPoint = new Point(evt.X, evt.Y);
      lastInput.DocPoint = this.ConvertViewToDoc(lastInput.ViewPoint);
      lastInput.Buttons = evt.Button;
      lastInput.Modifiers = Control.ModifierKeys;
      lastInput.Delta = evt.Delta;
      lastInput.Key = Keys.None;
      lastInput.MouseEventArgs = evt;
      this.FirstInput.ViewPoint = lastInput.ViewPoint;
      this.FirstInput.DocPoint = lastInput.DocPoint;
      this.FirstInput.Buttons = lastInput.Buttons;
      this.FirstInput.Modifiers = lastInput.Modifiers;
      this.FirstInput.Delta = lastInput.Delta;
      this.FirstInput.Key = lastInput.Key;
      this.FirstInput.MouseEventArgs = evt;
      this.DoMouseDown();
    }
    base.OnMouseDown(evt);
    lastInput.MouseEventArgs = (MouseEventArgs) null;
    this.FirstInput.MouseEventArgs = (MouseEventArgs) null;
  }

  /// <summary>This method is the view's mouse up event handler.</summary>
  /// <param name="evt"></param>
  /// <remarks>
  /// This method makes sure the <see cref="P:Intermech.Map.MapView.LastInput" /> property value
  /// has up-to-date information describing this mouse input event.
  /// It then calls <see cref="M:Intermech.Map.MapView.DoMouseUp" /> to allow tools
  /// to handle the input event, and then finally calls
  /// the base method to invoke all of the <c>MouseUp</c> event handlers.
  /// </remarks>
  protected override void OnMouseUp(MouseEventArgs evt)
  {
    InputEventArgs lastInput = this.LastInput;
    if (this.AllowMouse)
    {
      lastInput.ViewPoint = new Point(evt.X, evt.Y);
      lastInput.DocPoint = this.ConvertViewToDoc(lastInput.ViewPoint);
      lastInput.Buttons = evt.Button;
      lastInput.Modifiers = Control.ModifierKeys;
      lastInput.Delta = evt.Delta;
      lastInput.Key = Keys.None;
      lastInput.MouseEventArgs = evt;
      this.DoMouseUp();
    }
    base.OnMouseUp(evt);
    lastInput.MouseEventArgs = (MouseEventArgs) null;
  }

  /// <summary>
  /// This is the mouse wheel event handler, that handles scrolling and zooming.
  /// </summary>
  /// <param name="evt"></param>
  /// <remarks>
  /// <para>
  /// This is not supported in GoDiagram Pocket, due to differences in .NET
  /// Compact Framework controls.
  /// </para>
  /// This method makes sure the <see cref="P:Intermech.Map.MapView.LastInput" /> property value
  /// has up-to-date information describing this mouse input event.
  /// It then calls <see cref="M:Intermech.Map.MapView.DoMouseWheel" /> to allow tools
  /// to handle the input event, and then finally calls
  /// the base method to invoke all of the <c>MouseWheel</c> event handlers.
  /// </remarks>
  protected override void OnMouseWheel(MouseEventArgs evt)
  {
    InputEventArgs lastInput = this.LastInput;
    if (this.AllowMouse)
    {
      lastInput.ViewPoint = new Point(evt.X, evt.Y);
      lastInput.DocPoint = this.ConvertViewToDoc(lastInput.ViewPoint);
      lastInput.Buttons = evt.Button;
      lastInput.Modifiers = Control.ModifierKeys;
      lastInput.Delta = evt.Delta;
      lastInput.Key = Keys.None;
      lastInput.MouseEventArgs = evt;
      this.DoMouseWheel();
    }
    base.OnMouseWheel(evt);
    lastInput.MouseEventArgs = (MouseEventArgs) null;
  }

  /// <summary>This method is the view's mouse move event handler.</summary>
  /// <param name="evt"></param>
  /// <remarks>
  /// This method makes sure the <see cref="P:Intermech.Map.MapView.LastInput" /> property value
  /// has up-to-date information describing this mouse input event.
  /// It then calls <see cref="M:Intermech.Map.MapView.DoMouseMove" /> to allow tools
  /// to handle the input event, and then finally calls
  /// the base method to invoke all of the <c>MouseMove</c> event handlers.
  /// </remarks>
  protected override void OnMouseMove(MouseEventArgs evt)
  {
    InputEventArgs lastInput = this.LastInput;
    Point point = new Point(evt.X, evt.Y);
    if (this.AllowMouse)
    {
      lastInput.ViewPoint = point;
      lastInput.DocPoint = this.ConvertViewToDoc(lastInput.ViewPoint);
      lastInput.Buttons = evt.Button;
      lastInput.Modifiers = Control.ModifierKeys;
      lastInput.Delta = evt.Delta;
      lastInput.Key = Keys.None;
      lastInput.MouseEventArgs = evt;
      this.DoMouseMove();
    }
    base.OnMouseMove(evt);
    lastInput.MouseEventArgs = (MouseEventArgs) null;
  }

  /// <summary>Handle a canonicalized mouse down input event.</summary>
  /// <remarks>
  /// This method assumes <see cref="P:Intermech.Map.MapView.LastInput" /> has information
  /// representing a mouse down input event.
  /// We also assume that <see cref="P:Intermech.Map.MapView.FirstInput" /> has a copy
  /// of the canonicalized input event.
  /// By default this just gets focus and calls
  /// on the current
  /// This is normally called by <see cref="M:Intermech.Map.MapView.OnMouseDown(System.Windows.Forms.MouseEventArgs)" /> and any
  /// other code that wishes to simulate a canonicalized mouse down event.
  /// This is not called when  is false.
  /// </remarks>
  public virtual void DoMouseDown()
  {
    int num = this.Focused ? 1 : 0;
    this.InitFocus();
    if ((num != 0 || !this._cancelMouseDown) && this.Tool != null)
      this.Tool.DoMouseDown();
    this._cancelMouseDown = false;
  }

  /// <summary>Handle a canonicalized mouse move input event.</summary>
  /// <remarks>
  /// This method assumes <see cref="P:Intermech.Map.MapView.LastInput" /> has information
  /// representing a mouse move input event.
  /// By default this just calls
  /// on the current <see cref="P:Intermech.Map.MapView.Tool" />.
  /// This is normally called by <see cref="M:Intermech.Map.MapView.OnMouseMove(System.Windows.Forms.MouseEventArgs)" /> and any
  /// other code that wishes to simulate a canonicalized mouse move event.
  /// This is not called when <see cref="P:Intermech.Map.MapView.AllowMouse" /> is false.
  /// </remarks>
  public virtual void DoMouseMove()
  {
    if (this.Tool == null)
      return;
    this.Tool.DoMouseMove();
  }

  /// <summary>Handle a canonicalized mouse up input event.</summary>
  /// <remarks>
  /// This method assumes <see cref="P:Intermech.Map.MapView.LastInput" /> has information
  /// representing a mouse up input event.
  /// By default this just calls
  /// on the current
  /// This is normally called by  and any
  /// other code that wishes to simulate a canonicalized mouse up event.
  /// This is not called when  is false.
  /// </remarks>
  public virtual void DoMouseUp()
  {
    if (this.Tool == null)
      return;
    this.Tool.DoMouseUp();
  }

  /// <summary>
  /// Handle a canonicalized mouse wheel input event.  [Not in GoDiagram Pocket]
  /// </summary>
  /// <remarks>
  /// This method assume <see cref="P:Intermech.Map.MapView.LastInput" /> has information
  /// representing an input event describing the rotation of the
  /// mouse wheel.
  /// By default this just calls
  /// on the current
  /// This is normally called by  and any
  /// other code that wishes to simulate a canonicalized mouse wheel event.
  /// This is not called when  is false.
  /// </remarks>
  public virtual void DoMouseWheel()
  {
    if (this.Tool == null)
      return;
    this.Tool.DoMouseWheel();
  }

  internal void InitFocus()
  {
    try
    {
      this.InitFocus2();
    }
    catch (SecurityException ex)
    {
      Trace.WriteLine("Focus: " + ex.ToString());
    }
  }

  private void InitFocus2() => this.Focus();

  /// <summary>Perform the standard mouse wheel behavior for views.</summary>
  /// <param name="evt"></param>
  /// <remarks>
  /// When the Control key is held down, rotating the mouse wheel changes the
  /// <see cref="P:Intermech.Map.MapView.DocScale" /> to "zoom" the view in or out.
  /// Otherwise rotating the mouse wheel scrolls the view by calling <see cref="M:Intermech.Map.MapView.ScrollLine(System.Single,System.Single)" />.
  /// If the Shift key is held down, the scrolling is horizontal instead of vertical.
  /// </remarks>
  public virtual void DoWheel(InputEventArgs evt)
  {
    if (evt.Delta == 0)
      return;
    if (evt.Control)
    {
      this.ZoomToScale(evt.DocPoint, (float) (1.0 - (double) evt.Delta / 1200.0));
    }
    else
    {
      float num = (float) -evt.Delta / 60f;
      if (evt.Shift)
        this.ScrollLine(num, 0.0f);
      else
        this.ScrollLine(0.0f, num);
    }
  }

  public View()
  {
    this._vertScrollHandler = (ScrollEventHandler) null;
    this._horizScrollHandler = (ScrollEventHandler) null;
    this._showVertScroll = ViewScrollBarVisibility.IfNeeded;
    this._showHorizScroll = ViewScrollBarVisibility.IfNeeded;
    this._paintEventArgs = (PaintEventArgs) null;
    this._suppressPaint = 0;
    this._updatingScrollBars = true;
    this._autoScrollRegion = new Size(SystemInformation.VerticalScrollBarWidth, SystemInformation.HorizontalScrollBarHeight);
    this._autoScrollTime = 100;
    this._autoScrollDelay = 1000;
    this._autoScrollTimer = (System.Threading.Timer) null;
    this._autoScrollTimerEnabled = false;
    this._autoScrollPoint = new Point();
    this._panning = false;
    this._panningOrigin = new Point();
    this._defaultCursor = (Cursor) null;
    this._prevXorRect = new Rectangle();
    this._prevXorRectValid = false;
    this._cancelMouseDown = false;
    this._borderStyle = BorderStyle.Fixed3D;
    this._border3DStyle = Border3DStyle.Etched;
    this._borderSize = SystemInformation.Border3DSize;
    this._document = (IDocument) null;
    this.mySelectsByFirstChar = true;
    this.myScrollSmallChange = new Size(16 /*0x10*/, 16 /*0x10*/);
    this.myAutoPanRegion = new Size(16 /*0x10*/, 16 /*0x10*/);
    this._showsNegativeCoordinates = true;
    this._origin = new PointF();
    this._horizScale = 1f;
    this._vertScale = 1f;
    this._smoothingMode = SmoothingMode.HighQuality;
    this._textRenderingHint = TextRenderingHint.ClearTypeGridFit;
    this._interpolationMode = InterpolationMode.High;
    this._cacheImage = (Bitmap) null;
    this._firstInput = new InputEventArgs();
    this._lastInput = new InputEventArgs();
    this._tool = (ITool) null;
    this._defaultTool = (ITool) null;
    this._mouseDownTools = (ArrayList) null;
    this._mouseMoveTools = (ArrayList) null;
    this._mouseUpTools = (ArrayList) null;
    this._backgroundBrush = (SolidBrush) null;
    this._shadowOffset = new SizeF(0.0f, 0.0f);
    this.init((Intermech.DocumentView.Document) null);
  }

  private void init(Intermech.DocumentView.Document doc)
  {
    this._document = (IDocument) doc;
    this._defaultTool = this.CreateDefaultTool();
    this._tool = this.DefaultTool;
    this._tool.Start();
    this.SetStyle(ControlStyles.UserPaint | ControlStyles.Opaque | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
    this._corner.BackColor = SystemColors.Control;
    this.Controls.Add((Control) this._vertScroll);
    this.Controls.Add((Control) this._horizScroll);
    this.Controls.Add(this._corner);
    this._vertScroll.SmallChange = this.ScrollSmallChange.Height;
    this._horizScroll.SmallChange = this.ScrollSmallChange.Width;
    this._toolTip = new ToolTip();
    this._vertScrollHandler = new ScrollEventHandler(this.HandleScroll);
    this._vertScroll.Scroll += this._vertScrollHandler;
    this._horizScrollHandler = new ScrollEventHandler(this.HandleScroll);
    this._horizScroll.Scroll += this._horizScrollHandler;
    this._vertScroll.RightToLeft = RightToLeft.No;
    this._horizScroll.RightToLeft = RightToLeft.No;
    this.BackColor = Color.White;
  }

  /// <summary>
  /// Rather than having separate events whenever any view property changed,
  /// all such notifications occur through this single event.
  /// </summary>
  public event PropertyChangedEventHandler PropertyChanged;

  /// <summary>This is the event handler for both scroll bars.</summary>
  /// <param name="sender"></param>
  /// <param name="e">
  /// This is a <c>ScrollEventArgs</c>, except in GoDiagram Pocket where it is an <c>EventArgs</c>,
  /// due to the differences in scroll bar controls between the standard framework and the
  /// compact framework.
  /// </param>
  /// <remarks>
  /// This method sets the <see cref="P:Intermech.Map.MapView.DocPosition" /> property according to
  /// the new value.
  /// </remarks>
  public virtual void HandleScroll(object sender, ScrollEventArgs e)
  {
    if (e.Type == ScrollEventType.EndScroll)
      return;
    int newValue = e.NewValue;
    this.InitFocus();
    PointF docPosition = this.DocPosition;
    if (sender == this.VerticalScrollBar)
    {
      docPosition.Y = (float) newValue / this._vertScale;
      this.DocPosition = docPosition;
    }
    else
    {
      if (sender != this.HorizontalScrollBar)
        return;
      docPosition.X = (float) newValue / this._horizScale;
      this.DocPosition = docPosition;
    }
  }

  /// <summary>
  /// Gets the extent of the view in its document, both position and size.
  /// </summary>
  /// <value>
  /// The <c>RectangleF</c> value is in document coordinates.
  /// </value>
  /// <remarks>
  /// This convenience method returns <c>new RectangleF(this.DocPosition, this.DocExtentSize)</c>.
  /// </remarks>
  /// <seealso cref="P:Intermech.Map.MapView.DocPosition" />
  /// <seealso cref="P:Intermech.Map.MapView.DocExtentSize" />
  [Browsable(false)]
  public RectangleF DocExtent
  {
    get
    {
      PointF docPosition = this.DocPosition;
      SizeF docExtentSize = this.DocExtentSize;
      return new RectangleF(docPosition.X, docPosition.Y, docExtentSize.Width, docExtentSize.Height);
    }
  }

  /// <summary>
  /// Given a point in this document, calculate the corresponding point in this view.
  /// </summary>
  /// <param name="p">
  /// A <c>PointF</c> in document coordinates.
  /// </param>
  /// <returns>
  /// The corresponding <c>Point</c> in view coordinates.
  /// </returns>
  /// <remarks>
  /// This method takes this view's current position in the document, and the current
  /// view scale, into account when computing the transformation from document coordinates
  /// to view coordinates.
  /// Note that because documents are often larger than the views,
  /// many object positions will often have corresponding view positions outside the
  /// view's client area.
  /// </remarks>
  /// <seealso cref="M:Intermech.Map.MapView.ConvertViewToDoc(System.Drawing.Point)" />
  public virtual Point ConvertDocToView(PointF p)
  {
    PointF docPosition = this.DocPosition;
    return new Point((int) Math.Floor(((double) p.X - (double) docPosition.X) * (double) this._horizScale) + this._borderSize.Width, (int) Math.Floor(((double) p.Y - (double) docPosition.Y) * (double) this._vertScale) + this._borderSize.Height);
  }

  /// <summary>
  /// Given a rectangle in this document, calculate the corresponding rectangle in this view.
  /// </summary>
  /// <param name="r">
  /// A <c>RectangleF</c> in document coordinates.
  /// </param>
  /// <returns>
  /// The corresponding <c>Rectangle</c> in view coordinates.
  /// </returns>
  /// <remarks>
  /// This method takes this view's current position in the document, and the current
  /// view scale, into account when computing the transformation from document coordinates
  /// to view coordinates.
  /// Note that because documents are often larger than the views,
  /// many object positions will often have corresponding view positions outside the
  /// view's client area.
  /// </remarks>
  public virtual Rectangle ConvertDocToView(RectangleF r)
  {
    PointF docPosition = this.DocPosition;
    return new Rectangle((int) Math.Floor(((double) r.X - (double) docPosition.X) * (double) this._horizScale) + this._borderSize.Width, (int) Math.Floor(((double) r.Y - (double) docPosition.Y) * (double) this._vertScale) + this._borderSize.Height, (int) Math.Ceiling((double) r.Width * (double) this._horizScale), (int) Math.Ceiling((double) r.Height * (double) this._vertScale));
  }

  /// <summary>
  /// Given a size in this document, calculate the corresponding size in this view.
  /// </summary>
  /// <param name="s">
  /// A <c>SizeF</c> in document coordinates.
  /// </param>
  /// <returns>
  /// The corresponding <c>Size</c> in view coordinates.
  /// </returns>
  /// <remarks>
  /// This method takes this view's current view scale into account when computing the
  /// transformation from document coordinates to view coordinates.
  /// </remarks>
  public virtual Size ConvertDocToView(SizeF s)
  {
    return new Size((int) Math.Ceiling((double) s.Width * (double) this._horizScale), (int) Math.Ceiling((double) s.Height * (double) this._vertScale));
  }

  /// <summary>
  /// Given a point in this view, calculate the corresponding point in the view's document.
  /// </summary>
  /// <param name="p">
  /// A <c>Point</c> in view coordinates.
  /// </param>
  /// <returns>
  /// The corresponding <c>PointF</c> in document coordinates.
  /// </returns>
  /// <remarks>
  /// This method takes this view's current position in the document, and the current
  /// view scale, into account when computing the transformation from view coordinates
  /// to document coordinates.
  /// </remarks>
  public virtual PointF ConvertViewToDoc(Point p)
  {
    PointF docPosition = this.DocPosition;
    return new PointF((float) (p.X - this._borderSize.Width) / this._horizScale + docPosition.X, (float) (p.Y - this._borderSize.Height) / this._vertScale + docPosition.Y);
  }

  /// <summary>
  /// Given a rectangle in this view, calculate the corresponding rectangle in the view's document.
  /// </summary>
  /// <param name="r">
  /// A <c>Rectangle</c> in view coordinates.
  /// </param>
  /// <returns>
  /// The corresponding <c>RectangleF</c> in document coordinates.
  /// </returns>
  /// <remarks>
  /// This method takes this view's current position in the document, and the current
  /// view scale, into account when computing the transformation from view coordinates
  /// to document coordinates.
  /// </remarks>
  public virtual RectangleF ConvertViewToDoc(Rectangle r)
  {
    PointF docPosition = this.DocPosition;
    return new RectangleF((float) (r.X - this._borderSize.Width) / this._horizScale + docPosition.X, (float) (r.Y - this._borderSize.Height) / this._vertScale + docPosition.Y, (float) r.Width / this._horizScale, (float) r.Height / this._vertScale);
  }

  /// <summary>
  /// Given a size in this view, calculate the corresponding size in the view's document.
  /// </summary>
  /// <param name="s">
  /// A <c>Size</c> in view coordinates.
  /// </param>
  /// <returns>
  /// The corresponding <c>SizeF</c> in document coordinates.
  /// </returns>
  /// <remarks>
  /// This method takes this view's current view scale into account when computing the
  /// transformation from view coordinates to document coordinates.
  /// </remarks>
  public virtual SizeF ConvertViewToDoc(Size s)
  {
    return new SizeF((float) s.Width / this._horizScale, (float) s.Height / this._vertScale);
  }

  /// <summary>
  /// Gets or sets the position in the document that this view is displaying.
  /// </summary>
  /// <value>
  /// The <c>PointF</c> value is in document coordinates and corresponds to
  /// this view's top-left corner's position in the document.
  /// Initially the value is (0, 0).
  /// </value>
  /// <remarks>
  /// When setting this property, it first adjusts the value by calling
  /// <see cref="M:Intermech.Map.MapView.LimitDocPosition(System.Drawing.PointF)" />.
  /// </remarks>
  [Description("The position in the document that this view is displaying.")]
  [Browsable(false)]
  [Category("Appearance")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual PointF DocPosition
  {
    get => this._origin;
    set
    {
      PointF origin = this._origin;
      PointF pointF1 = this.LimitDocPosition(value);
      PointF pointF2 = pointF1;
      if (!(origin != pointF2))
        return;
      this._origin = pointF1;
      this.RaisePropertyChangedEvent(nameof (DocPosition));
    }
  }

  public virtual PointF OriginDocPosition
  {
    get => this._origin;
    set
    {
      if (!(this._origin != value))
        return;
      this._origin = value;
      this.RaisePropertyChangedEvent("DocPosition");
    }
  }

  /// <summary>
  /// This method is called when setting the DocPosition property to make
  /// sure the view only takes reasonable, desired positions.
  /// </summary>
  /// <param name="p"></param>
  /// <returns></returns>
  /// <remarks>
  /// By default this method tries to keep the view within the document.
  /// </remarks>
  public virtual PointF LimitDocPosition(PointF p)
  {
    PointF documentTopLeft = this.DocumentTopLeft;
    SizeF documentSize = this.DocumentSize;
    SizeF docExtentSize = this.DocExtentSize;
    float num1 = documentTopLeft.X + documentSize.Width - docExtentSize.Width;
    if ((double) num1 < (double) documentTopLeft.X)
      p.X = documentTopLeft.X;
    else if ((double) p.X > (double) num1 && (double) num1 > (double) documentTopLeft.X)
      p.X = num1;
    else if ((double) p.X < (double) documentTopLeft.X)
      p.X = documentTopLeft.X;
    float num2 = documentTopLeft.Y + documentSize.Height - docExtentSize.Height;
    if ((double) num2 < (double) documentTopLeft.Y)
    {
      p.Y = documentTopLeft.Y;
      return p;
    }
    if ((double) p.Y > (double) num2 && (double) num2 > (double) documentTopLeft.Y)
    {
      p.Y = num2;
      return p;
    }
    if ((double) p.Y < (double) documentTopLeft.Y)
      p.Y = documentTopLeft.Y;
    return p;
  }

  /// <summary>
  /// Gets or sets the scale at which this view displays its document.
  /// </summary>
  /// <value>
  /// <para>
  /// A value of <c>1.0f</c> specifies that one unit in document coordinates corresponds
  /// to one pixel in view coordinates.  Values smaller than one make objects appear
  /// smaller on the screen.  Larger values make it appear that you have zoomed into
  /// the diagram.
  /// </para>
  /// <para>
  /// The <c>float</c> value must be greater than zero.  The default value is <c>1.0f</c>.
  /// </para>
  /// </value>
  /// <remarks>
  /// When setting this property, it first limits the value by calling
  /// <see cref="M:Intermech.Map.MapView.LimitDocScale(System.Single)" />.
  /// A different value is used when printing, <c>PrintScale</c>.
  /// </remarks>
  [Description("The scale at which this view displays its document.")]
  [DefaultValue(1f)]
  [Category("Appearance")]
  public virtual float DocScale
  {
    get => this._horizScale;
    set
    {
      float num = this.LimitDocScale(value);
      if ((double) this._horizScale == (double) num && (double) this._vertScale == (double) num)
        return;
      this._horizScale = num;
      this._vertScale = num;
      this.RaisePropertyChangedEvent(nameof (DocScale));
    }
  }

  /// <summary>Gets or sets the offset distance for drop shadows.</summary>
  /// <value>
  /// This <c>SizeF</c> value specifies the offset, where positive values move
  /// the shadow to the right and to the bottom.
  /// The default value is (5, 5).
  /// </value>
  /// <remarks>
  /// The shadow only appears for those objects that have the <see cref="P:Intermech.Map.MapObject.Shadowed" />
  /// property set to true.
  /// </remarks>
  /// <seealso cref="P:Intermech.Map.MapView.ShadowColor" />
  [Description("The offset distance for drop shadows.")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Category("Shadows")]
  [Browsable(false)]
  public virtual SizeF ShadowOffset
  {
    get => this._shadowOffset;
    set
    {
      if (!(this._shadowOffset != value))
        return;
      this._shadowOffset = value;
      this.RaisePropertyChangedEvent(nameof (ShadowOffset));
    }
  }

  /// <summary>
  /// This method is called when setting the DocScale property to make
  /// sure that the view only displays objects at a reasonable scale.
  /// </summary>
  /// <param name="s"></param>
  /// <returns></returns>
  /// <remarks>
  /// By default this limits the value to between 0.01f and 10.0f.
  /// </remarks>
  public virtual float LimitDocScale(float s)
  {
    if ((double) s < 0.00050000002374872565)
      s = 0.0005f;
    if ((double) s > 200.0)
      s = 200f;
    return s;
  }

  /// <summary>Gets the size of this view in its document.</summary>
  /// <value>
  /// The <c>SizeF</c> value is in document coordinates.
  /// </value>
  /// <remarks>
  /// The value depends on the actual size of the client area and the scale
  /// at which the document is being shown.
  /// </remarks>
  /// <seealso cref="P:Intermech.Map.MapView.DocPosition" />
  [Browsable(false)]
  public virtual SizeF DocExtentSize
  {
    get
    {
      Size size = this.DisplayRectangle.Size;
      return new SizeF((float) size.Width / this._horizScale, (float) size.Height / this._vertScale);
    }
  }

  /// <summary>
  /// Raise a <see cref="E:Intermech.DocumentView.IView.PropertyChanged" /> event for the given property name.
  /// </summary>
  /// <param name="propname"></param>
  /// <remarks>
  /// This just calls IView.OnPropertyChanged(System.ComponentModel.PropertyChangedEventArgs).
  /// </remarks>
  public void RaisePropertyChangedEvent(string propname)
  {
    this.OnPropertyChanged(new PropertyChangedEventArgs(propname));
  }

  /// <summary>
  /// Call all <see cref="E:Intermech.Map.MapView.PropertyChanged" /> event handlers.
  /// </summary>
  /// <param name="evt"></param>
  /// <remarks>
  /// This also calls <see cref="M:Intermech.DocumentView.IView.UpdateView" />, unless the property is
  /// known to be a minor one.
  /// If you override this method, be sure to call the base method too.
  /// This is called by <see cref="M:Intermech.Map.MapView.RaisePropertyChangedEvent(System.String)" />
  /// </remarks>
  protected virtual void OnPropertyChanged(PropertyChangedEventArgs evt)
  {
    if (this.PropertyChanged != null)
      this.PropertyChanged((object) this, evt);
    if (!(evt.PropertyName != "Tool"))
      return;
    this.UpdateView();
  }

  /// <summary>
  /// Cause the whole view, including scroll bars, to be redrawn.
  /// </summary>
  /// <remarks>
  /// This calls <c>Invalidate()</c> after updating the scroll bars.
  /// </remarks>
  public virtual void UpdateView()
  {
    this._cacheValid = false;
    this.UpdateBorderWidths();
    this.UpdateScrollBars();
    this.Invalidate();
  }

  /// <summary>
  /// Update the scroll bars for this view, changing the minimum/maximum/value
  /// and visibility as appropriate.
  /// </summary>
  /// <seealso cref="M:Intermech.Map.MapView.LayoutScrollBars(System.Boolean)" />
  public virtual void UpdateScrollBars()
  {
    if (this._updatingScrollBars)
      return;
    HScrollBar horizontalScrollBar = this.HorizontalScrollBar;
    VScrollBar verticalScrollBar = this.VerticalScrollBar;
    if (verticalScrollBar == null && horizontalScrollBar == null)
      return;
    PointF documentTopLeft = this.DocumentTopLeft;
    SizeF documentSize = this.DocumentSize;
    int num1 = (int) Math.Floor((double) documentTopLeft.X * (double) this._horizScale);
    int num2 = (int) Math.Floor((double) documentTopLeft.Y * (double) this._vertScale);
    int val1_1 = (int) Math.Ceiling(((double) documentTopLeft.X + (double) documentSize.Width) * (double) this._horizScale);
    int val1_2 = (int) Math.Ceiling(((double) documentTopLeft.Y + (double) documentSize.Height) * (double) this._vertScale);
    PointF docPosition = this.DocPosition;
    int val2_1 = (int) Math.Floor((double) docPosition.X * (double) this._horizScale);
    int val2_2 = (int) Math.Floor((double) docPosition.Y * (double) this._vertScale);
    Size size = this.Size;
    size.Width -= 2 * this._borderSize.Width;
    if (size.Width < 0)
      size.Width = 0;
    size.Height -= 2 * this._borderSize.Height;
    if (size.Height < 0)
      size.Height = 0;
    bool flag1 = val1_2 - num2 > size.Height || val2_2 > num2 || val2_2 < val1_2 - size.Height;
    bool flag2 = verticalScrollBar != null && (this.ShowVerticalScrollBar == ViewScrollBarVisibility.Show || this.ShowVerticalScrollBar == ViewScrollBarVisibility.IfNeeded & flag1);
    bool flag3 = val1_1 - num1 > size.Width || val2_1 > num1 || val2_1 < val1_1 - size.Width;
    bool flag4 = horizontalScrollBar != null && (this.ShowHorizontalScrollBar == ViewScrollBarVisibility.Show || this.ShowHorizontalScrollBar == ViewScrollBarVisibility.IfNeeded & flag3);
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
    bool flag5 = val1_2 - num2 > size.Height || val2_2 > num2 || val2_2 < val1_2 - size.Height;
    bool flag6 = verticalScrollBar != null && (this.ShowVerticalScrollBar == ViewScrollBarVisibility.Show || this.ShowVerticalScrollBar == ViewScrollBarVisibility.IfNeeded & flag5);
    bool flag7 = val1_1 - num1 > size.Width || val2_1 > num1 || val2_1 < val1_1 - size.Width;
    bool flag8 = horizontalScrollBar != null && (this.ShowHorizontalScrollBar == ViewScrollBarVisibility.Show || this.ShowHorizontalScrollBar == ViewScrollBarVisibility.IfNeeded & flag7);
    this._updatingScrollBars = true;
    bool flag9 = false;
    if (verticalScrollBar != null)
    {
      int num3 = val1_2 - size.Height;
      if (val2_2 > num3 && num3 > num2)
        val2_2 = num3;
      else if (val2_2 < num2)
        val2_2 = num2;
      int num4 = Math.Max(Math.Max(val1_2, val2_2 + size.Height) - 12, val2_2);
      if (verticalScrollBar.Minimum != num2)
        verticalScrollBar.Minimum = num2;
      if (verticalScrollBar.Maximum != num4)
        verticalScrollBar.Maximum = num4;
      if (verticalScrollBar.Value != val2_2)
        verticalScrollBar.Value = val2_2;
      if (verticalScrollBar.Visible != flag6)
        flag9 = true;
      verticalScrollBar.Visible = flag6;
      verticalScrollBar.Enabled = flag5;
    }
    if (horizontalScrollBar != null)
    {
      int num5 = val1_1 - size.Width;
      if (val2_1 > num5 && num5 > num1)
        val2_1 = num5;
      else if (val2_1 < num1)
        val2_1 = num1;
      int num6 = Math.Max(Math.Max(val1_1, val2_1 + size.Width) - 12, val2_1);
      if (horizontalScrollBar.Minimum != num1)
        horizontalScrollBar.Minimum = num1;
      if (horizontalScrollBar.Maximum != num6)
        horizontalScrollBar.Maximum = num6;
      if (horizontalScrollBar.Value != val2_1)
        horizontalScrollBar.Value = val2_1;
      if (horizontalScrollBar.Visible != flag8)
        flag9 = true;
      horizontalScrollBar.Visible = flag8;
      horizontalScrollBar.Enabled = flag7;
    }
    this._updatingScrollBars = false;
    if (!flag9)
      return;
    this.LayoutScrollBars(false);
  }

  /// <summary>
  /// Gets or sets the horizontal scroll bar used by the view when not all objects can be
  /// displayed at once in the given client area.
  /// </summary>
  /// <value>
  /// The <c>HScrollBar</c> control may be invisible and/or disabled, or null.
  /// </value>
  /// <remarks>
  /// The scroll bar has the LargeChange and SmallChange properties, which affect how
  /// much is scrolled at a time.  The <c>LargeChange</c> property is computed given the width of
  /// the view's client area minus the <c>SmallChange</c> value.  The <c>SmallChange</c> property
  /// value is taken from the <see cref="P:Intermech.Map.MapView.ScrollSmallChange" /> property width.
  /// </remarks>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public virtual HScrollBar HorizontalScrollBar
  {
    get => this._horizScroll;
    set
    {
      HScrollBar horizScroll = this._horizScroll;
      if (horizScroll == value)
        return;
      if (horizScroll != null)
      {
        horizScroll.Scroll -= this._horizScrollHandler;
        this.Controls.Remove((Control) horizScroll);
      }
      this._horizScroll = value;
      if (value != null)
      {
        value.SmallChange = this.ScrollSmallChange.Width;
        this.Controls.Add((Control) value);
        value.Scroll += this._horizScrollHandler;
      }
      this.LayoutScrollBars(true);
      this.RaisePropertyChangedEvent(nameof (HorizontalScrollBar));
    }
  }

  /// <summary>
  /// Gets or sets the vertical scroll bar used by the view when not all objects can be
  /// displayed at once in the given client area.
  /// </summary>
  /// <value>
  /// The <c>VScrollBar</c> control may be invisible and/or disabled, or null.  Setting this
  /// property will set up <see cref="M:Intermech.Map.MapView.HandleScroll(System.Object,System.Windows.Forms.ScrollEventArgs)" /> as a scroll event handler for the scroll bar.
  /// </value>
  /// <remarks>
  /// The scroll bar has the <c>LargeChange</c> and <c>SmallChange</c> properties, which affect how
  /// much is scrolled at a time.  The <c>LargeChange</c> property is computed given the height of
  /// the view's client area minus the <c>SmallChange</c> value.  The <c>SmallChange</c> property
  /// value is taken from the <see cref="P:Intermech.Map.MapView.ScrollSmallChange" /> property height.
  /// </remarks>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public virtual VScrollBar VerticalScrollBar
  {
    get => this._vertScroll;
    set
    {
      VScrollBar vertScroll = this._vertScroll;
      if (vertScroll == value)
        return;
      if (vertScroll != null)
      {
        vertScroll.Scroll -= this._vertScrollHandler;
        this.Controls.Remove((Control) vertScroll);
      }
      this._vertScroll = value;
      if (value != null)
      {
        value.SmallChange = this.ScrollSmallChange.Height;
        this.Controls.Add((Control) value);
        value.Scroll += this._vertScrollHandler;
      }
      this.LayoutScrollBars(true);
      this.RaisePropertyChangedEvent(nameof (VerticalScrollBar));
    }
  }

  /// <summary>
  /// Gets or sets the visibility policy for the vertical scroll bar.
  /// </summary>
  /// <value>
  /// The default value is <see cref="F:Intermech.Map.MapViewScrollBarVisibility.IfNeeded" />.
  /// </value>
  [DefaultValue(2)]
  [Description("The visibility policy for the vertical scroll bar.")]
  [Category("Appearance")]
  public virtual ViewScrollBarVisibility ShowVerticalScrollBar
  {
    get => this._showVertScroll;
    set
    {
      if (this._showVertScroll == value)
        return;
      this._showVertScroll = value;
      this.LayoutScrollBars(true);
      this.RaisePropertyChangedEvent(nameof (ShowVerticalScrollBar));
    }
  }

  /// <summary>
  /// Gets or sets the visibility policy for the horizontal scroll bar.
  /// </summary>
  /// <value>
  /// The default value is <see cref="F:Intermech.Map.MapViewScrollBarVisibility.IfNeeded" />.
  /// </value>
  [Category("Appearance")]
  [Description("The visibility policy for the horizontal scroll bar.")]
  [DefaultValue(2)]
  public virtual ViewScrollBarVisibility ShowHorizontalScrollBar
  {
    get => this._showHorizScroll;
    set
    {
      if (this._showHorizScroll == value)
        return;
      this._showHorizScroll = value;
      this.LayoutScrollBars(true);
      this.RaisePropertyChangedEvent(nameof (ShowHorizontalScrollBar));
    }
  }

  /// <summary>Position and size the scrollbars and corner.</summary>
  /// <param name="update">
  /// Whether to call <see cref="M:Intermech.Map.MapView.UpdateScrollBars" /> afterwards.
  /// </param>
  /// <remarks>
  /// By default this places the vertical scroll bar at the right edge of the view,
  /// the horizontal scroll bar at the bottom edge, and the corner in the bottom right.
  /// All of these controls are inside the view's border.
  /// </remarks>
  public virtual void LayoutScrollBars(bool update)
  {
    if (this._updatingScrollBars)
      return;
    Rectangle clientRectangle = this.ClientRectangle;
    int x = clientRectangle.Width - this._borderSize.Width;
    VScrollBar verticalScrollBar = this.VerticalScrollBar;
    if (verticalScrollBar != null && verticalScrollBar.Visible)
      x -= this.myScrollBarWidth;
    int y = clientRectangle.Height - this._borderSize.Height;
    HScrollBar horizontalScrollBar = this.HorizontalScrollBar;
    if (horizontalScrollBar != null && horizontalScrollBar.Visible)
      y -= this.myScrollBarHeight;
    Control cornerControl = this.CornerControl;
    if (cornerControl != null)
    {
      if (verticalScrollBar != null && verticalScrollBar.Visible && horizontalScrollBar != null && horizontalScrollBar.Visible)
      {
        cornerControl.Bounds = new Rectangle(x, y, this.myScrollBarWidth, this.myScrollBarHeight);
        cornerControl.Visible = true;
      }
      else
        cornerControl.Visible = false;
    }
    Size scrollSmallChange;
    if (verticalScrollBar != null && verticalScrollBar.Visible)
    {
      verticalScrollBar.Bounds = new Rectangle(x, this._borderSize.Height, this.myScrollBarWidth, y - this._borderSize.Height);
      VScrollBar vscrollBar = verticalScrollBar;
      int height1 = this.ScrollSmallChange.Height;
      int height2 = verticalScrollBar.Height;
      scrollSmallChange = this.ScrollSmallChange;
      int height3 = scrollSmallChange.Height;
      int val2 = height2 - height3;
      int num = Math.Max(height1, val2);
      vscrollBar.LargeChange = num;
    }
    if (horizontalScrollBar != null && horizontalScrollBar.Visible)
    {
      horizontalScrollBar.Bounds = new Rectangle(this._borderSize.Width, y, x - this._borderSize.Width, this.myScrollBarHeight);
      HScrollBar hscrollBar = horizontalScrollBar;
      scrollSmallChange = this.ScrollSmallChange;
      int width1 = scrollSmallChange.Width;
      int width2 = horizontalScrollBar.Width;
      scrollSmallChange = this.ScrollSmallChange;
      int width3 = scrollSmallChange.Width;
      int val2 = width2 - width3;
      int num = Math.Max(width1, val2);
      hscrollBar.LargeChange = num;
    }
    if (!update)
      return;
    this.UpdateScrollBars();
  }

  /// <summary>
  /// Gets or sets the distance to scroll when scrolling a small amount.
  /// </summary>
  /// <value>
  /// The <c>Size</c> value must have positive <c>Width</c> and <c>Height</c>, indicating the
  /// amount in view coordinates to scroll horizontally or vertically in either direction.
  /// </value>
  /// <remarks>
  /// Setting this property also modifies the <c>SmallChange</c> properties of the scroll bars,
  /// if there are any.
  /// </remarks>
  [Description("The distance to scroll when scrolling a small amount.")]
  [Category("Behavior")]
  public virtual Size ScrollSmallChange
  {
    get => this.myScrollSmallChange;
    set
    {
      if (!(this.myScrollSmallChange != value))
        return;
      if (value.Width <= 0 || value.Height <= 0)
        throw new ArgumentOutOfRangeException("New Size value for GoView.ScrollSmallChange must have positive dimensions");
      this.myScrollSmallChange = value;
      HScrollBar horizontalScrollBar = this.HorizontalScrollBar;
      if (horizontalScrollBar != null && horizontalScrollBar.SmallChange != this.myScrollSmallChange.Width)
        horizontalScrollBar.SmallChange = this.myScrollSmallChange.Width;
      VScrollBar verticalScrollBar = this.VerticalScrollBar;
      if (verticalScrollBar != null && verticalScrollBar.SmallChange != this.myScrollSmallChange.Height)
        verticalScrollBar.SmallChange = this.myScrollSmallChange.Height;
      this.RaisePropertyChangedEvent(nameof (ScrollSmallChange));
    }
  }

  /// <summary>
  /// Programmatically scroll the view by a "line" (a small change).
  /// </summary>
  /// <param name="dx">the number of lines to change the X coordinate of the <see cref="P:Intermech.Map.MapView.DocPosition" />; positive increases, negative decreases</param>
  /// <param name="dy">the number of lines to change the Y coordinate of the <see cref="P:Intermech.Map.MapView.DocPosition" />; positive increases, negative decreases</param>
  /// <remarks>
  /// This method does not depend on the existence of any scrollbars,
  /// but does depend on the value of <see cref="P:Intermech.Map.MapView.ScrollSmallChange" /> to determine
  /// a new value for <see cref="P:Intermech.Map.MapView.DocPosition" />.
  /// </remarks>
  public virtual void ScrollLine(float dx, float dy)
  {
    PointF docPosition = this.DocPosition;
    SizeF docExtentSize = this.DocExtentSize;
    PointF documentTopLeft = this.DocumentTopLeft;
    SizeF documentSize = this.DocumentSize;
    Size scrollSmallChange = this.ScrollSmallChange;
    float num1 = dx * (float) scrollSmallChange.Width / this._horizScale;
    docPosition.X += num1;
    docPosition.X = (double) num1 < 0.0 ? Math.Max(docPosition.X, documentTopLeft.X) : Math.Min(docPosition.X, Math.Max(documentTopLeft.X, documentTopLeft.X + documentSize.Width - docExtentSize.Width));
    float num2 = dy * (float) scrollSmallChange.Height / this._vertScale;
    docPosition.Y += num2;
    docPosition.Y = (double) num2 < 0.0 ? Math.Max(docPosition.Y, documentTopLeft.Y) : Math.Min(docPosition.Y, Math.Max(documentTopLeft.Y, documentTopLeft.Y + documentSize.Height - docExtentSize.Height));
    this.DocPosition = docPosition;
  }

  [Description("The border style for this view.")]
  [DefaultValue(2)]
  [Category("Appearance")]
  public virtual BorderStyle BorderStyle
  {
    get => this._borderStyle;
    set
    {
      if (this._borderStyle == value)
        return;
      this._borderStyle = value;
      this.UpdateBorderWidths();
      this.RaisePropertyChangedEvent(nameof (BorderStyle));
    }
  }

  private void UpdateBorderWidths()
  {
    Size borderSize = this._borderSize;
    Size size;
    switch (this.BorderStyle)
    {
      case BorderStyle.None:
        size = new Size();
        break;
      case BorderStyle.FixedSingle:
        size = SystemInformation.BorderSize;
        break;
      default:
        size = SystemInformation.Border3DSize;
        break;
    }
    if (!(size != this._borderSize))
      return;
    this._borderSize = size;
    this.LayoutScrollBars(false);
  }

  /// <summary>
  /// Gets or sets the <c>Control</c> that fits in the corner adjacent to both vertical
  /// and horizontal scroll bars, when both are visible.
  /// </summary>
  /// <value>
  /// Any <c>Control</c> may be used here; the initial value is a blank, default <c>Control</c>.
  /// </value>
  /// <remarks>
  /// The position and size of the control are set automatically to fit the scroll
  /// bars' width and height.
  /// </remarks>
  /// <seealso cref="P:Intermech.Map.MapView.VerticalScrollBar" />
  /// <seealso cref="P:Intermech.Map.MapView.HorizontalScrollBar" />
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual Control CornerControl
  {
    get => this._corner;
    set
    {
      Control corner = this._corner;
      if (corner == value)
        return;
      if (corner != null)
        this.Controls.Remove(corner);
      this._corner = value;
      if (value != null)
        this.Controls.Add(value);
      this.LayoutScrollBars(true);
      this.RaisePropertyChangedEvent(nameof (CornerControl));
    }
  }

  /// <summary>
  /// Gets or sets the document that this view is displaying.
  /// </summary>
  /// <value>
  /// The initial value is created by a call to <see cref="M:Intermech.Map.MapView.CreateDocument" />.
  /// The value must not be null.
  /// </value>
  /// <remarks>
  /// <para>
  /// The document serves as the container of graphical objects that you want
  /// to display.
  /// Normally you should create graphical objects (instances of subclasses of
  /// <see cref="T:Intermech.Map.MapObject" />) and add them to the document, in order to make them
  /// visible to the user.
  /// Although often there will be one view for each document, there are can be
  /// more than one view displaying the same document, or sometimes no views at all
  /// for a document.
  /// Each view will have its own state, such as scroll position and selection.
  /// The document holds all of the state that should be shared by all views.
  /// </para>
  /// <para>
  /// Setting this property to a different document will stop any ongoing editing
  /// in this view, clear out the selection, make this view's OnDocumentChanged method
  /// the event handler for the new document, and call
  /// <see cref="M:Intermech.Map.MapView.InitializeLayersFromDocument" /> to set up the document layers in
  /// this view.
  /// </para>
  /// </remarks>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual IDocument Document
  {
    get => this._document;
    set
    {
      if (value == null)
        throw new ArgumentOutOfRangeException("New value for GoView.Document must not be null");
      IDocument document = this.Document;
      if (value == document)
        return;
      if (document != null)
        document.Changed -= new DocumentChangedEventHandler(this.OnDocumentChanged);
      if (this.Tool != null)
        this.DoCancelMouse();
      this.DoEndEdit();
      this._document = value;
      value.Changed += new DocumentChangedEventHandler(this.OnDocumentChanged);
      this.RaisePropertyChangedEvent(nameof (Document));
      this.InitializeLayersFromDocument();
    }
  }

  public virtual void InitializeLayersFromDocument()
  {
  }

  /// <summary>
  /// Invoke all <see cref="E:Intermech.Map.MapView.BackgroundSingleClicked" /> event handlers.
  /// </summary>
  /// <param name="evt"></param>
  protected virtual void OnBackgroundSingleClicked(InputEventArgs evt)
  {
  }

  /// <summary>
  /// Replace one of the "mode-less" tools used by this view.
  /// </summary>
  /// <param name="tooltype">the <c>Type</c> of the tool to be replaced;
  /// this should not be a base class of the actual tool instance type</param>
  /// <param name="newtool">the tool to use instead of the existing one of
  /// <c>Type</c> <paramref name="tooltype" />;
  /// if null, the old tool is only removed</param>
  /// <returns>the tool that was replaced, or null if no such instance was found</returns>
  /// <remarks>
  /// When you want to customize an existing "mode-less" tool, and when setting one of its properties
  /// is insufficient, you may need to define your own subclass of that tool or define
  /// your own tool inheriting from <see cref="T:Intermech.Map.MapTool" />.
  /// In order for the view to use your tool, you'll need to create an instance of
  /// your tool class for the view, and then you can either set <see cref="P:Intermech.Map.MapView.Tool" />
  /// explicitly, or let <see cref="T:Intermech.Map.MapToolManager" /> find your tool in one of the mouse tool
  /// lists, such as <see cref="P:Intermech.Map.MapView.MouseDownTools" />.
  /// For the latter case, you could just add an instance of your tool to one of those lists.
  /// But often you will not want to allow the instance of the original tool class to be used.
  /// This method makes it easy to replace an existing tool with a different one.
  /// This method searches all of the lists of mode-less tools:
  /// <seealso cref="P:Intermech.Map.MapView.MouseDownTools" />, <seealso cref="P:Intermech.Map.MapView.MouseMoveTools" />, <seealso cref="P:Intermech.Map.MapView.MouseUpTools" />.
  /// </remarks>
  /// <seealso cref="M:Intermech.Map.MapView.FindMouseTool(System.Type)" />
  /// <example>
  /// You have defined a new subclass of <see cref="T:Intermech.Map.MapToolLinkingNew" />, called <c>CustomLinkTool</c>.
  /// For each view that you want to use of this new tool instead of the standard way
  /// for users to draw new links, call
  /// <c>aView.ReplaceMouseTool(typeof(GoToolLinkingNew), new CustomLinkTool(aView))</c>
  /// </example>
  public virtual ITool ReplaceMouseTool(System.Type tooltype, ITool newtool)
  {
    IList mouseDownTools = this.MouseDownTools;
    for (int index = 0; index < mouseDownTools.Count; ++index)
    {
      if (mouseDownTools[index].GetType() == tooltype)
      {
        ITool tool = (ITool) mouseDownTools[index];
        if (newtool == null)
        {
          mouseDownTools.RemoveAt(index);
          return tool;
        }
        mouseDownTools[index] = (object) newtool;
        return tool;
      }
    }
    IList mouseMoveTools = this.MouseMoveTools;
    for (int index = 0; index < mouseMoveTools.Count; ++index)
    {
      if (mouseMoveTools[index].GetType() == tooltype)
      {
        ITool tool = (ITool) mouseMoveTools[index];
        if (newtool == null)
        {
          mouseMoveTools.RemoveAt(index);
          return tool;
        }
        mouseMoveTools[index] = (object) newtool;
        return tool;
      }
    }
    IList mouseUpTools = this.MouseUpTools;
    for (int index = 0; index < mouseUpTools.Count; ++index)
    {
      if (mouseUpTools[index].GetType() == tooltype)
      {
        ITool tool = (ITool) mouseUpTools[index];
        if (newtool == null)
        {
          mouseUpTools.RemoveAt(index);
          return tool;
        }
        mouseUpTools[index] = (object) newtool;
        return tool;
      }
    }
    return (ITool) null;
  }

  /// <summary>Gets the top-left position of the document.</summary>
  /// <value>
  /// The <c>PointF</c> value specifies the top-left corner of the document in
  /// document coordinates.
  /// </value>
  /// <remarks>
  /// This value is normally the same as <c>Document.TopLeft</c>.
  /// However, a view may decide to change the extent of the document that
  /// the view displays.  For example, the <see cref="P:Intermech.Map.MapView.ShowsNegativeCoordinates" />
  /// property, when false, restricts the view to only showing non-negative
  /// positions in the document by always returning the (0, 0) point.
  /// This property also leaves room
  /// for any shadows, as specified by <see cref="P:Intermech.Map.MapView.ShadowOffset" />.
  /// A different document top-left position is used when printing,
  /// <c>PrintDocumentTopLeft</c>.
  /// </remarks>
  [Browsable(false)]
  public virtual PointF DocumentTopLeft
  {
    get
    {
      if (!this.ShowsNegativeCoordinates || this.Document == null)
        return new PointF();
      PointF topLeft = this.Document.TopLeft;
      SizeF shadowOffset = this.ShadowOffset;
      if ((double) shadowOffset.Width < 0.0)
        topLeft.X += shadowOffset.Width;
      if ((double) shadowOffset.Height < 0.0)
        topLeft.Y += shadowOffset.Height;
      return topLeft;
    }
  }

  /// <summary>Gets the dimensions of the document.</summary>
  /// <value>
  /// The <c>SizeF</c> value measures the document in document coordinates.
  /// </value>
  /// <remarks>
  /// This value is normally the same as <c>Document.Size</c>.
  /// However, a view may decide to change the extent of the document that
  /// the view displays.  For example, the <see cref="P:Intermech.Map.MapView.ShowsNegativeCoordinates" />
  /// property, when false, restricts the view to only showing non-negative
  /// positions in the document.  In addition to restricting the
  /// <see cref="P:Intermech.Map.MapView.DocumentTopLeft" /> property to non-negative positions,
  /// it adjusts this property accordingly.  This property also leaves room
  /// for any shadows, as specified by <see cref="P:Intermech.Map.MapView.ShadowOffset" />.
  /// This property is different from the result of <see cref="M:Intermech.Map.MapView.ComputeDocumentBounds" />
  /// because the latter method only takes into account what objects there actually are
  /// in the document, whereas this property will have the same value even if the
  /// document is empty.
  /// A different document size is used when printing, <c>PrintDocumentSize</c>.
  /// </remarks>
  [Browsable(false)]
  public virtual SizeF DocumentSize
  {
    get
    {
      IDocument document = this.Document;
      if (document == null)
        return new SizeF();
      SizeF size = document.Size;
      size.Width += Math.Abs(this.ShadowOffset.Width);
      size.Height += Math.Abs(this.ShadowOffset.Height);
      if (!this.ShowsNegativeCoordinates)
      {
        PointF topLeft = document.TopLeft;
        if ((double) topLeft.X < 0.0)
          size.Width += topLeft.X;
        if ((double) topLeft.Y < 0.0)
          size.Height += topLeft.Y;
      }
      return size;
    }
  }

  /// <summary>
  /// Controls whether any parts of the document at negative coordinates can be seen
  /// or scrolled to by the user.
  /// </summary>
  /// <value>
  /// If this value is true, the user will be able to scroll to negative coordinate
  /// positions in the document.
  /// If this value is false, the user cannot see objects located at negative coordinates.
  /// The default value is true.
  /// </value>
  /// <remarks>
  /// <para>
  /// When this value is false, it limits the values of the <see cref="P:Intermech.Map.MapView.DocumentSize" />
  /// and <see cref="P:Intermech.Map.MapView.DocumentTopLeft" /> properties.
  /// </para>
  /// <para>
  /// For <see cref="T:Intermech.Map.MapPalette" /> the default value is false.
  /// </para>
  /// </remarks>
  [DefaultValue(true)]
  [Category("Behavior")]
  [Description("Whether any parts of the document at negative coordinates can be seen or scrolled to.")]
  public virtual bool ShowsNegativeCoordinates
  {
    get => this._showsNegativeCoordinates;
    set
    {
      if (this._showsNegativeCoordinates == value)
        return;
      this._showsNegativeCoordinates = value;
      this.RaisePropertyChangedEvent(nameof (ShowsNegativeCoordinates));
    }
  }

  /// <summary>Handle a canonicalized cancel input event.</summary>
  /// <remarks>
  /// This method assumes <see cref="P:Intermech.Map.MapView.LastInput" /> has information
  /// representing a mouse down input event.
  /// By default this just calls
  /// on the current
  /// This is normally called by <c>OnQueryContinueDrag</c>
  /// and most tools when the user types an <c>Escape</c>, and by any
  /// other code that wishes to simulate cancelling a mouse operation.
  /// For example, this is called when the
  /// is changed, to try to clean up any input operation that might
  /// be in progress.
  /// </remarks>
  public virtual void DoCancelMouse()
  {
    this._cancelMouseDown = true;
    this.Tool.DoCancelMouse();
  }

  /// <summary>
  /// Stop the user's editing an object using the <see cref="P:Intermech.Map.MapView.EditControl" />.
  /// </summary>
  /// <remarks>
  /// If <see cref="P:Intermech.Map.MapView.EditControl" /> is non-null, we call <see cref="M:Intermech.Map.MapControl.DoEndEdit(Intermech.Map.MapView)" /> on it,
  /// which presumably will call <see cref="M:Intermech.Map.MapObject.DoEndEdit(Intermech.Map.MapView)" /> on the
  /// <see cref="P:Intermech.Map.MapControl.EditedObject" />.
  /// The responsibility for calling <see cref="M:Intermech.Map.MapView.RaiseObjectEdited(Intermech.Map.MapObject)" />
  /// and for finishing any transaction rests with the individual implementations
  /// of <see cref="M:Intermech.Map.MapObject.DoEndEdit(Intermech.Map.MapView)" />.
  /// </remarks>
  public virtual void DoEndEdit()
  {
  }

  /// <summary>
  /// Gets or sets the current tool being used by this view.
  /// </summary>
  /// <remarks>
  /// <para>
  /// As standard input events occur, the event args information is canonicalized
  /// into an instance of <see cref="T:Intermech.Map.MapInputEventArgs" /> and then the current Tool's
  /// appropriate method is called.
  /// </para>
  /// <para>
  /// Setting this property to null results in setting it to the value of
  /// <see cref="P:Intermech.Map.MapView.DefaultTool" />.
  /// A tool that has finished will probably need to reset this property to
  /// the <see cref="P:Intermech.Map.MapView.DefaultTool" />,
  /// typically by calling the <see cref="M:Intermech.Map.MapTool.StopTool" /> method.
  /// </para>
  /// <para>
  /// If the tool is explicitly set as a result of some user-interface command,
  /// the tool is being used in a "modal" fashion.
  /// </para>
  /// <para>
  /// If the tool is set as a result of the <see cref="T:Intermech.Map.MapToolManager" /> searching
  /// through the lists of tools to be started as a result of a mouse down, a mouse
  /// move, or a mouse up, then the tool is being used in a "mode-less" fashion.
  /// </para>
  /// </remarks>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual ITool Tool
  {
    get => this._tool;
    set
    {
      if (this._tool == value)
        return;
      if (this._tool != null)
        this._tool.Stop();
      this._tool = value != null ? value : this.DefaultTool;
      if (this._tool != null)
        this._tool.Start();
      this.RaisePropertyChangedEvent(nameof (Tool));
    }
  }

  /// <summary>Gets or sets the default tool.</summary>
  /// <value>
  /// The value must not be null.  Initially this is assigned the value of <see cref="M:Intermech.Map.MapView.CreateDefaultTool" />.
  /// </value>
  /// <remarks>
  /// When the <see cref="P:Intermech.Map.MapView.Tool" /> property is set to null, we actually reset
  /// <see cref="P:Intermech.Map.MapView.Tool" /> to be the value of this <see cref="P:Intermech.Map.MapView.DefaultTool" /> property.
  /// By default this value is an instance of <see cref="T:Intermech.Map.MapToolManager" />, which handles
  /// standard keyboard commands and invokes the appropriate tool upon mouse down/move/up
  /// events.
  /// </remarks>
  /// <seealso cref="P:Intermech.Map.MapView.Tool" />
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual ITool DefaultTool
  {
    get => this._defaultTool;
    set
    {
      if (this._defaultTool == value)
        return;
      this._defaultTool = value != null ? value : throw new ArgumentOutOfRangeException("New value for GoView.DefaultTool must not be null");
      this.RaisePropertyChangedEvent(nameof (DefaultTool));
    }
  }

  /// <summary>
  /// Programmatically scroll the view by a "page" (a large change).
  /// </summary>
  /// <param name="dx">the number of pages to change the X coordinate of the <see cref="P:Intermech.Map.MapView.DocPosition" />; positive increases, negative decreases</param>
  /// <param name="dy">the number of pages to change the Y coordinate of the <see cref="P:Intermech.Map.MapView.DocPosition" />; positive increases, negative decreases</param>
  /// <remarks>
  /// This method does not depend on the existence of any scrollbars,
  /// but does depend on the values of <see cref="P:Intermech.Map.MapView.DocExtentSize" /> and <see cref="P:Intermech.Map.MapView.ScrollSmallChange" /> to determine
  /// a new value for <see cref="P:Intermech.Map.MapView.DocPosition" /> that is a "line" less than one full "page" away from the old position
  /// times the factor provided by the parameters for each direction.
  /// </remarks>
  /// <seealso cref="M:Intermech.Map.MapView.ScrollLine(System.Single,System.Single)" />
  /// <seealso cref="M:Intermech.Map.MapView.ScrollRectangleToVisible(System.Drawing.RectangleF)" />
  public virtual void ScrollPage(float dx, float dy)
  {
    PointF docPosition = this.DocPosition;
    SizeF docExtentSize = this.DocExtentSize;
    PointF documentTopLeft = this.DocumentTopLeft;
    SizeF documentSize = this.DocumentSize;
    Size scrollSmallChange = this.ScrollSmallChange;
    float num1 = dx * Math.Max((float) scrollSmallChange.Width, docExtentSize.Width - (float) scrollSmallChange.Width) / this._horizScale;
    docPosition.X += num1;
    docPosition.X = (double) num1 < 0.0 ? Math.Max(docPosition.X, documentTopLeft.X) : Math.Min(docPosition.X, Math.Max(documentTopLeft.X, documentTopLeft.X + documentSize.Width - docExtentSize.Width));
    float num2 = dy * Math.Max((float) scrollSmallChange.Height, docExtentSize.Height - (float) scrollSmallChange.Height) / this._vertScale;
    docPosition.Y += num2;
    docPosition.Y = (double) num2 < 0.0 ? Math.Max(docPosition.Y, documentTopLeft.Y) : Math.Min(docPosition.Y, Math.Max(documentTopLeft.Y, documentTopLeft.Y + documentSize.Height - docExtentSize.Height));
    this.DocPosition = docPosition;
  }

  /// <summary>
  /// Change this view's DocPosition so that the given rectangle is visible.
  /// </summary>
  /// <param name="contentRect">the area, in document coordinates, to try to scroll into view</param>
  /// <remarks>
  /// Usually you call this method with the bounds of an object, to make
  /// that object visible to the user and not scrolled off somewhere.
  /// </remarks>
  /// <seealso cref="M:Intermech.Map.MapView.ScrollPage(System.Single,System.Single)" />
  /// <seealso cref="M:Intermech.Map.MapView.ScrollLine(System.Single,System.Single)" />
  public virtual void ScrollRectangleToVisible(RectangleF contentRect)
  {
    RectangleF docExtent = this.DocExtent;
    if (IObject.ContainsRect(docExtent, contentRect))
      return;
    this.DocPosition = new PointF((double) contentRect.Width >= (double) docExtent.Width ? contentRect.X : (float) ((double) contentRect.X + (double) contentRect.Width / 2.0 - (double) docExtent.Width / 2.0), (double) contentRect.Height >= (double) docExtent.Height ? contentRect.Y : (float) ((double) contentRect.Y + (double) contentRect.Height / 2.0 - (double) docExtent.Height / 2.0));
  }

  /// <summary>
  /// Perform the behavior that normally occurs upon a double click.
  /// </summary>
  /// <param name="evt"></param>
  /// <returns></returns>
  /// <remarks>
  /// By default this picks the document object at the event's
  /// <see cref="P:Intermech.Map.MapInputEventArgs.DocPoint" />.
  /// If an object is found, it raises the <c>ObjectDoubleClicked</c> event and
  /// calls <see cref="M:Intermech.Map.MapObject.OnDoubleClick(Intermech.Map.MapInputEventArgs,Intermech.Map.MapView)" /> on the object and
  /// on its <see cref="P:Intermech.Map.MapObject.Parent" />s (if any) until it returns
  /// true.
  /// If no object is found at the event's point, it raises the
  /// <see cref="E:Intermech.Map.MapView.BackgroundDoubleClicked" /> event.
  /// This is normally called from the <see cref="M:Intermech.Map.MapTool.DoClick(Intermech.Map.MapInputEventArgs)" />
  /// method, which is called by those tools that treat clicks in the
  /// standard fashion.
  /// </remarks>
  public virtual bool DoDoubleClick(InputEventArgs evt)
  {
    IObject iobject = this.PickObject(true, false, evt.DocPoint, false);
    if (iobject != null)
    {
      this.RaiseObjectDoubleClicked(iobject, evt);
      for (; iobject != null; iobject = iobject.Parent)
      {
        if (iobject.OnDoubleClick(evt, this))
          return true;
      }
    }
    else
      this.RaiseBackgroundDoubleClicked(evt);
    return false;
  }

  /// <summary>Find a visible object at a given point.</summary>
  /// <param name="doc">If true, consider objects in document layers.</param>
  /// <param name="view">If true, consider objects in view layers.</param>
  /// <param name="p">The <c>PointF</c> in document coordinates at which to search.</param>
  /// <param name="selectableOnly">
  /// If true, skip over any objects whose <see cref="M:Intermech.Map.MapObject.CanSelect" /> property is false.
  /// </param>
  /// <returns>
  /// A <see cref="T:Intermech.Map.MapObject" /> that contains the <paramref name="p" />, or null if
  /// no such object exists.
  /// </returns>
  /// <remarks>
  /// This method never actually selects any object--use <see cref="T:Intermech.Map.MapSelection" />
  /// instead.
  /// Please note that if an object is found, it might not be a top-level object.
  /// In fact, when <paramref name="selectableOnly" /> is false, it is very likely
  /// that if any object is found at the given point, it will be a child of some
  /// group.
  /// </remarks>
  /// <seealso cref="M:Intermech.Map.MapLayer.PickObject(System.Drawing.PointF,System.Boolean)" />
  public virtual IObject PickObject(bool doc, bool view, PointF p, bool selectableOnly)
  {
    return (IObject) null;
  }

  /// <summary>
  /// Raise an <see cref="E:Intermech.Map.MapView.ObjectDoubleClicked" /> event for a given object and canonicalized input event.
  /// </summary>
  /// <param name="obj"></param>
  /// <param name="evt"></param>
  public void RaiseObjectDoubleClicked(IObject obj, InputEventArgs evt)
  {
    this.OnObjectDoubleClicked(new ObjectEventArgs(obj, evt));
  }

  private void OnObjectDoubleClicked(ObjectEventArgs objectEventArgs)
  {
    throw new Exception("The method or operation is not implemented.");
  }

  /// <summary>This lays out the scroll bars, too, if needed.</summary>
  /// <param name="evt"></param>
  /// <remarks>
  /// This is not supported in GoDiagram Pocket, due to differences in .NET
  /// Compact Framework controls.
  /// </remarks>
  protected override void OnSizeChanged(EventArgs evt)
  {
    base.OnSizeChanged(evt);
    this.LayoutScrollBars(false);
    this.UpdateView();
  }

  /// <summary>This lays out the scroll bars, too, if needed.</summary>
  /// <remarks>
  /// This is not supported in GoDiagram Pocket, due to differences in .NET
  /// Compact Framework controls.
  /// </remarks>
  protected override void OnCreateControl()
  {
    base.OnCreateControl();
    this._updatingScrollBars = false;
    this.LayoutScrollBars(true);
  }

  /// <summary>
  /// In case the size changed while it was not visible, make sure we update the scroll bars.
  /// </summary>
  /// <remarks>
  /// This is not supported in GoDiagram Pocket, due to differences in .NET
  /// Compact Framework controls.
  /// </remarks>
  protected override void OnVisibleChanged(EventArgs evt)
  {
    base.OnVisibleChanged(evt);
    if (!this.Visible)
      return;
    this.LayoutScrollBars(false);
    this.UpdateView();
  }

  /// <summary>
  /// Gets a list of "mode-less" tools to be considered for becoming the current Tool upon a mouse up event.
  /// </summary>
  /// <value>
  /// The <c>IList</c> may be modified.
  /// </value>
  /// <remarks>
  /// <para>
  /// , an instance of which is normally the <see cref="P:Intermech.Map.MapView.DefaultTool" />,
  /// iterates through this list when a mouse up event occurs.  The first tool that it finds
  /// whose  method returns true becomes this view's current
  /// .  If no such tool is found, the <see cref="T:Intermech.Map.MapToolManager" /> continues
  /// its normal behavior.
  /// </para>
  /// <para>
  /// By default this returns a list containing only an instance of the
  ///  tool.
  /// </para>
  /// </remarks>
  [Browsable(false)]
  public virtual IList MouseUpTools
  {
    get
    {
      if (this._mouseUpTools == null)
        this._mouseUpTools = new ArrayList();
      return (IList) this._mouseUpTools;
    }
  }

  /// <summary>
  /// Perform the immediate behavior normally associated with the mouse moving without
  /// a mouse button being pressed.  [Not in GoDiagram Pocket]
  /// </summary>
  /// <param name="evt"></param>
  /// <returns></returns>
  /// <remarks>
  /// This is called to handle mouse moves immediately.  If you don't need the
  /// immediate response, but would prefer getting an event after the mouse has
  /// rested at one spot for a while, use the <see cref="M:Intermech.Map.MapView.DoHover(Intermech.Map.MapInputEventArgs)" /> method,
  /// the <see cref="M:Intermech.Map.MapObject.OnHover(Intermech.Map.MapInputEventArgs,Intermech.Map.MapView)" /> method, or the <see cref="E:Intermech.Map.MapView.ObjectHover" />
  /// or <see cref="E:Intermech.Map.MapView.BackgroundHover" /> events.
  /// By default this picks the topmost/frontmost view or document object
  /// at the event's <see cref="P:Intermech.Map.MapInputEventArgs.DocPoint" />.
  /// It calls <see cref="M:Intermech.Map.MapView.DoToolTipObject(Intermech.Map.MapObject)" /> on the result, even if it is null.
  /// If an object is found, it calls <see cref="M:Intermech.Map.MapObject.OnMouseOver(Intermech.Map.MapInputEventArgs,Intermech.Map.MapView)" />
  /// on the object and on its <see cref="P:Intermech.Map.MapObject.Parent" />s (if any)
  /// until it returns true.
  /// If no object is found at the event's point, or if no object's
  /// <see cref="M:Intermech.Map.MapObject.OnMouseOver(Intermech.Map.MapInputEventArgs,Intermech.Map.MapView)" /> returns true, it calls
  /// <see cref="M:Intermech.Map.MapView.DoBackgroundMouseOver(Intermech.Map.MapInputEventArgs)" />.  The assumption is that any object
  /// that changes the <c>Cursor</c> will return true from the
  /// <see cref="M:Intermech.Map.MapObject.OnMouseOver(Intermech.Map.MapInputEventArgs,Intermech.Map.MapView)" /> method.
  /// This is normally called from <see cref="T:Intermech.Map.MapToolManager" />, when no
  /// other more specific tools are in effect.
  /// </remarks>
  public virtual bool DoMouseOver(InputEventArgs evt)
  {
    IObject iobject = this.PickObject(true, true, evt.DocPoint, false);
    this.DoToolTipObject(iobject);
    bool flag = false;
    for (; iobject != null; iobject = iobject.Parent)
    {
      if (iobject.OnMouseOver(evt, this))
      {
        flag = true;
        break;
      }
    }
    if (!flag)
      this.DoBackgroundMouseOver(evt);
    this.DetectHover(evt.ViewPoint);
    return flag;
  }

  /// <summary>
  /// Start or restart a timer to see if the mouse has moved; if at the end
  /// of the timer the mouse has not moved, <see cref="M:Intermech.Map.MapView.DoMouseHover" />
  /// is called.  [Not in GoDiagram Pocket]
  /// </summary>
  /// <param name="viewPnt">a <c>Point</c> in view coordinates</param>
  /// <remarks>
  /// This is called whenever a tool wants to deliver hover events.
  /// The time the mouse must rest motionless is determined by
  /// <see cref="P:Intermech.Map.MapView.HoverDelay" />.
  /// A mouse leave event will stop the hover timer.
  /// </remarks>
  public virtual void DetectHover(Point viewPnt)
  {
    if (this._hoverTimer == null)
    {
      this._hoverTimer = new System.Threading.Timer(new TimerCallback(this.hoverCallback), (object) new EventHandler(this.hoverTick), -1, -1);
      this._hoverTimerEnabled = false;
    }
    if (this._hoverPoint != viewPnt)
      this.StopHoverTimer();
    if (!this._hoverTimerEnabled)
    {
      this._hoverTimer.Change(this.HoverDelay, -1);
      this._hoverTimerEnabled = true;
    }
    this._hoverPoint = viewPnt;
  }

  private void StopHoverTimer()
  {
    if (this._hoverTimer == null)
      return;
    this._hoverTimer.Change(-1, -1);
    this._hoverTimerEnabled = false;
  }

  private void hoverCallback(object obj)
  {
    if (!this.IsHandleCreated)
      return;
    this.Invoke((Delegate) obj);
  }

  private void hoverTick(object sender, EventArgs e)
  {
    if (!this._hoverTimerEnabled)
      return;
    InputEventArgs lastInput = this.LastInput;
    lastInput.ViewPoint = this._hoverPoint;
    lastInput.DocPoint = this.ConvertViewToDoc(lastInput.ViewPoint);
    lastInput.Buttons = Control.MouseButtons;
    lastInput.Modifiers = Control.ModifierKeys;
    lastInput.Delta = 0;
    lastInput.Key = Keys.None;
    this.DoMouseHover();
  }

  /// <summary>
  /// Handle a canonicalized mouse hover input event.  [Not in GoDiagram Pocket]
  /// </summary>
  /// <remarks>
  /// This method assumes  has information
  /// representing a mouse-hovering-somewhere event.
  /// By default this just calls
  /// on the current
  /// This is normally called by  and any
  /// other code that wishes to simulate a canonicalized mouse hover event.
  /// This is not called when  is false.
  /// </remarks>
  public virtual void DoMouseHover() => this.Tool.DoMouseHover();

  /// <summary>
  /// Invoke all <see cref="E:Intermech.Map.MapView.BackgroundHover" /> event handlers.  [Not in GoDiagram Pocket]
  /// </summary>
  /// <param name="evt"></param>
  /// <remarks>
  /// If you want to get notification of mouse moves immediately, rather than
  /// after a delay, you'll need to override <see cref="M:Intermech.Map.MapView.DoMouseOver(Intermech.Map.MapInputEventArgs)" /> or
  /// one of the methods that it calls, such as <see cref="M:Intermech.Map.MapObject.OnMouseOver(Intermech.Map.MapInputEventArgs,Intermech.Map.MapView)" />.
  /// </remarks>
  protected virtual void OnBackgroundHover(InputEventArgs evt)
  {
    if (this.BackgroundHover == null)
      return;
    this.BackgroundHover((object) this, evt);
  }

  /// <summary>
  /// Gets or sets how long a mouse should stay at one spot before a
  /// hover event occurs.  [Not in GoDiagram Pocket]
  /// </summary>
  /// <value>
  /// The time is in milliseconds.  The default is 1000 (one second).
  /// </value>
  [Description("How long a mouse should stay at one spot before a hover event occurs.")]
  [DefaultValue(1000)]
  [Category("Behavior")]
  public int HoverDelay
  {
    get => this._hoverDelay;
    set
    {
      if (this._hoverDelay == value)
        return;
      this._hoverDelay = value;
      this.RaisePropertyChangedEvent(nameof (HoverDelay));
    }
  }

  public virtual void DoBackgroundMouseOver(InputEventArgs evt)
  {
    Cursor defaultCursor = this.DefaultCursor;
    if (!(this.Cursor != defaultCursor))
      return;
    this.Cursor = defaultCursor;
  }

  /// <summary>
  /// Gets a list of "mode-less" tools to be considered for becoming the current Tool upon a mouse move event.
  /// </summary>
  [Browsable(false)]
  public virtual IList MouseMoveTools
  {
    get
    {
      if (this._mouseMoveTools == null)
      {
        this._mouseMoveTools = new ArrayList();
        this._mouseMoveTools.Add((object) new ToolDragging((IView) this));
        this._mouseMoveTools.Add((object) new ToolRubberBanding((IView) this));
      }
      return (IList) this._mouseMoveTools;
    }
  }

  /// <summary>
  /// Start or continue automatically scrolling the view during a mouse drag.
  /// </summary>
  /// <param name="viewPnt">the current mouse point, in view coordinates</param>
  /// <remarks>
  /// As soon <see cref="M:Intermech.Map.MapView.ComputeAutoScrollDocPosition(System.Drawing.Point)" /> returns a new
  /// <see cref="P:Intermech.Map.MapView.DocPosition" /> value, this method starts a <c>Timer</c>
  /// that waits for <see cref="P:Intermech.Map.MapView.AutoScrollDelay" /> milliseconds.
  /// After waiting, it repeatedly sets <see cref="P:Intermech.Map.MapView.DocPosition" />
  /// to the latest <see cref="M:Intermech.Map.MapView.ComputeAutoScrollDocPosition(System.Drawing.Point)" /> value,
  /// until the position does not change (presumably because the
  /// <see cref="P:Intermech.Map.MapView.LastInput" />'s view point is no longer in the autoscroll
  /// margin).
  /// Setting this view's <see cref="P:Intermech.Map.MapView.DocPosition" /> occurs each
  /// <see cref="P:Intermech.Map.MapView.AutoScrollTime" /> milliseconds.
  /// This method is normally called by those tools that want to support
  /// auto-scrolling during a mouse move.
  /// The timer is stopped when the mouse leaves this view.
  /// </remarks>
  /// <seealso cref="M:Intermech.Map.MapView.DoAutoPan(System.Drawing.Point,System.Drawing.Point)" />
  /// <seealso cref="M:Intermech.Map.MapView.StopAutoScroll" />
  public virtual void DoAutoScroll(Point viewPnt)
  {
    this._panning = false;
    this._autoScrollPoint = viewPnt;
    this.DoInternalAutoScroll();
  }

  /// <summary>
  /// Perform the behavior that normally occurs upon a mouse hover.  [Not in GoDiagram Pocket]
  /// </summary>
  /// <param name="evt"></param>
  /// <returns></returns>
  /// <remarks>
  /// By default this picks the document object at the event's
  /// <see cref="P:Intermech.Map.MapInputEventArgs.DocPoint" />.
  /// If an object is found, it raises the ObjectHover event and
  /// calls <see cref="M:Intermech.Map.MapObject.OnHover(Intermech.Map.MapInputEventArgs,Intermech.Map.MapView)" /> on the object and on
  /// its <see cref="P:Intermech.Map.MapObject.Parent" />s (if any) until it returns true.
  /// If no object is found at the event's point, it raises the
  /// BackgroundHover event.
  /// This is normally called from the <see cref="M:Intermech.Map.MapToolManager.DoMouseHover" />
  /// method, which is called by those tools that treat clicks in the
  /// standard fashion.
  /// </remarks>
  public virtual bool DoHover(InputEventArgs evt)
  {
    IObject iobject = this.PickObject(true, false, evt.DocPoint, false);
    if (iobject != null)
    {
      this.RaiseObjectHover(iobject, evt);
      for (; iobject != null; iobject = iobject.Parent)
      {
        if (iobject.OnHover(evt, this))
          return true;
      }
    }
    else
      this.RaiseBackgroundHover(evt);
    return false;
  }

  /// <summary>
  /// Raise a <see cref="E:Intermech.Map.MapView.BackgroundHover" /> event for a given canonicalized input event.  [Not in GoDiagram Pocket]
  /// </summary>
  /// <param name="evt"></param>
  public void RaiseBackgroundHover(InputEventArgs evt) => this.OnBackgroundHover(evt);

  /// <summary>
  /// Raise an <see cref="E:Intermech.Map.MapView.ObjectHover" /> event for a given object and canonicalized input event.  [Not in GoDiagram Pocket]
  /// </summary>
  /// <param name="obj"></param>
  /// <param name="evt"></param>
  public void RaiseObjectHover(IObject obj, InputEventArgs evt)
  {
    this.OnObjectHover(new ObjectEventArgs(obj, evt));
  }

  /// <summary>
  /// Invoke all <see cref="E:Intermech.Map.MapView.ObjectHover" /> event handlers.  [Not in GoDiagram Pocket]
  /// </summary>
  /// <param name="evt"></param>
  protected virtual void OnObjectHover(ObjectEventArgs evt)
  {
    if (this.ObjectHover == null)
      return;
    this.ObjectHover((object) this, evt);
  }

  /// <summary>
  /// Gets or sets whether the user typing a letter or digit will cause
  /// the next node whose Text starts with that character to become the
  /// primary selection.
  /// </summary>
  /// <remarks>This property is initially true.</remarks>
  [Description("Whether the user typing a letter or digit will select the next node starting with that character.")]
  [DefaultValue(true)]
  [Category("Selection")]
  public bool SelectsByFirstChar
  {
    get => this.mySelectsByFirstChar;
    set
    {
      if (this.mySelectsByFirstChar == value)
        return;
      this.mySelectsByFirstChar = value;
      this.RaisePropertyChangedEvent(nameof (SelectsByFirstChar));
    }
  }

  /// <summary>
  /// Gets a list of "mode-less" tools to be considered for becoming the current Tool upon a mouse down event.
  /// </summary>
  /// <value>
  /// The <c>IList</c> may be modified.
  /// </value>
  /// <remarks>
  /// <para>
  /// , an instance of which is normally the <see cref="P:Intermech.Map.MapView.DefaultTool" />,
  /// iterates through this list when a mouse down event occurs.  The first tool that it finds
  /// whose  method returns true becomes this view's current
  /// .  If no such tool is found, the  continues
  /// its normal behavior.
  /// </para>
  /// <para>
  /// By default this returns a list containing instances of the ,
  /// , ,
  /// , ,
  /// and  tools, in that order.
  /// The order of the tools matters, because even if several tools can start, only the first one
  /// actually is started.
  /// </para>
  /// </remarks>
  /// <seealso cref="P:Intermech.Map.MapView.MouseMoveTools" />
  /// <seealso cref="P:Intermech.Map.MapView.MouseUpTools" />
  [Browsable(false)]
  public virtual IList MouseDownTools
  {
    get
    {
      if (this._mouseDownTools == null)
      {
        this._mouseDownTools = new ArrayList();
        this._mouseDownTools.Add((object) new ToolPanningAcad((IView) this));
      }
      return (IList) this._mouseDownTools;
    }
  }

  public ISelection Selection => this._selection;

  /// <summary>
  /// Raise a <see cref="E:Intermech.Map.MapView.BackgroundDoubleClicked" /> event for a given canonicalized input event.
  /// </summary>
  /// <param name="evt"></param>
  public void RaiseBackgroundDoubleClicked(InputEventArgs evt)
  {
    this.OnBackgroundDoubleClicked(evt);
  }

  /// <summary>
  /// Invoke all <see cref="E:Intermech.Map.MapView.BackgroundDoubleClicked" /> event handlers.
  /// </summary>
  /// <param name="evt"></param>
  protected virtual void OnBackgroundDoubleClicked(InputEventArgs evt)
  {
    if (this.BackgroundDoubleClicked == null)
      return;
    this.BackgroundDoubleClicked((object) this, evt);
  }

  /// <summary>
  /// Perform the behavior that normally occurs upon a single click.
  /// </summary>
  /// <param name="evt"></param>
  /// <returns></returns>
  /// <remarks>
  /// By default this picks the document object at the event's
  /// <see cref="P:Intermech.Map.MapInputEventArgs.DocPoint" />.
  /// If an object is found, it raises the <c>ObjectSingleClicked</c> event and
  /// calls <see cref="M:Intermech.Map.MapObject.OnSingleClick(Intermech.Map.MapInputEventArgs,Intermech.Map.MapView)" /> on the object and
  /// on its <see cref="P:Intermech.Map.MapObject.Parent" />s (if any) until it returns
  /// true.
  /// If no object is found at the event's point, it raises the
  /// <see cref="E:Intermech.Map.MapView.BackgroundSingleClicked" /> event.
  /// This is normally called from the <see cref="M:Intermech.Map.MapTool.DoClick(Intermech.Map.MapInputEventArgs)" />
  /// method, which is called by those tools that treat clicks in the
  /// standard fashion.
  /// <para>
  /// In GoDiagram Pocket, this method also calls <c>DoToolTipObject</c>,
  /// because there is no mouse-over event.
  /// </para>
  /// </remarks>
  public virtual bool DoSingleClick(InputEventArgs evt)
  {
    IObject iobject = this.PickObject(true, false, evt.DocPoint, false);
    if (iobject != null)
    {
      this.RaiseObjectSingleClicked(iobject, evt);
      for (; iobject != null; iobject = iobject.Parent)
      {
        if (iobject.OnSingleClick(evt, this))
          return true;
      }
    }
    else
      this.RaiseBackgroundSingleClicked(evt);
    return false;
  }

  private void RaiseBackgroundSingleClicked(InputEventArgs evt)
  {
    this.OnBackgroundSingleClicked(evt);
  }

  private void RaiseObjectSingleClicked(IObject obj1, InputEventArgs evt)
  {
    throw new Exception("The method or operation is not implemented.");
  }

  public bool StartTransaction()
  {
    throw new Exception("The method or operation is not implemented.");
  }

  public bool AbortTransaction()
  {
    throw new Exception("The method or operation is not implemented.");
  }

  public bool FinishTransaction(string p)
  {
    throw new Exception("The method or operation is not implemented.");
  }

  public void EditDelete() => throw new Exception("The method or operation is not implemented.");

  public void SelectAll() => throw new Exception("The method or operation is not implemented.");

  public void EditCopy() => throw new Exception("The method or operation is not implemented.");

  public void EditCut() => throw new Exception("The method or operation is not implemented.");

  public void EditPaste() => throw new Exception("The method or operation is not implemented.");

  public void EditEdit() => throw new Exception("The method or operation is not implemented.");

  /// <summary>
  /// Determine the actual extent of all of the objects in the document
  /// as seen by this view.
  /// </summary>
  /// <returns>A <c>RectangleF</c> in document coordinates</returns>
  /// <remarks>
  /// This is called by methods such as <see cref="M:Intermech.Map.MapView.RescaleToFit" />, that
  /// want to know how much area is taken up by visible document objects.
  /// <see cref="P:Intermech.Map.MapView.DocumentSize" /> is different in that that property is
  /// likely to be less changeable as objects are moved or deleted.
  /// </remarks>
  public virtual RectangleF ComputeDocumentBounds()
  {
    return this.Document.ComputeBounds(this.Document.Layers, (IView) this);
  }

  public void Undo() => throw new Exception("The method or operation is not implemented.");

  public void Redo() => throw new Exception("The method or operation is not implemented.");

  /// <summary>
  /// Called to see if the user can select objects in this view for this document.
  /// </summary>
  /// <remarks>
  /// This just returns <c>AllowSelect &amp;&amp; Document.CanSelectObjects</c>.
  /// This predicate is used by methods such as <see cref="M:Intermech.Map.MapView.SelectAll" /> and
  /// <see cref="M:Intermech.Map.MapView.SelectInRectangle(System.Drawing.RectangleF)" />.
  /// </remarks>
  /// <seealso cref="P:Intermech.Map.MapView.AllowSelect" />
  /// <seealso cref="M:Intermech.Map.MapDocument.CanSelectObjects" />
  public virtual bool CanSelectObjects() => this.AllowSelect && this.Document.CanSelectObjects();

  public bool SelectNextNode(char ch1)
  {
    throw new Exception("The method or operation is not implemented.");
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public override Cursor Cursor
  {
    get => base.Cursor;
    set
    {
      if (this._defaultCursor == (Cursor) null)
        this._defaultCursor = this.Cursor;
      if (!(this.Cursor != value))
        return;
      base.Cursor = value;
    }
  }

  /// <summary>
  /// Start or continue scrolling the view according to the relative position of
  /// the <paramref name="viewPnt" /> compared to the <paramref name="originPnt" />.
  /// </summary>
  /// <param name="originPnt">the original panning point, in view coordinates</param>
  /// <param name="viewPnt">the current mouse point, in view coordinates</param>
  /// <remarks>
  /// This uses the same mechanisms as <see cref="M:Intermech.Map.MapView.DoAutoScroll(System.Drawing.Point)" /> -- do not
  /// try to auto-scroll and auto-pan at the same time.
  /// Automatic panning occurs in the area outside of the region specified by
  /// <see cref="P:Intermech.Map.MapView.AutoPanRegion" /> surrounding the <paramref name="originPnt" />.
  /// </remarks>
  /// <seealso cref="M:Intermech.Map.MapView.StopAutoScroll" />
  /// <seealso cref="P:Intermech.Map.MapView.AutoScrollDelay" />
  /// <seealso cref="P:Intermech.Map.MapView.AutoScrollTime" />
  public virtual void DoAutoPan(Point originPnt, Point viewPnt)
  {
    this._panning = true;
    this._panningOrigin = originPnt;
    this._autoScrollPoint = viewPnt;
    this.DoInternalAutoScroll();
  }

  /// <summary>
  /// This method is called to determine the next position in the document for this view,
  /// given a point at which the user is holding the mouse during a pan operation.
  /// </summary>
  /// <param name="originPnt">
  /// The original panning point, in view coordinates.
  /// </param>
  /// <param name="viewPnt">The mouse point, in view coordinates.</param>
  /// <remarks>
  /// This uses the <see cref="P:Intermech.Map.MapView.ScrollSmallChange" /> property to calculate a new <see cref="P:Intermech.Map.MapView.DocPosition" />.
  /// When the current mouse point is within the <see cref="P:Intermech.Map.MapView.AutoPanRegion" /> width or height
  /// distance from the <paramref name="originPnt" />, no scrolling occurs.
  /// When the current mouse point is outside of this region, between the <see cref="P:Intermech.Map.MapView.AutoPanRegion" />
  /// distance (width or height) and three times that distance, automatic scrolling proceeds
  /// at the smallest scrolling increment, <see cref="P:Intermech.Map.MapView.ScrollSmallChange" />.
  /// The farther away the <paramref name="viewPnt" /> is from the <paramref name="originPnt" />, the larger a multiple of
  /// the <see cref="P:Intermech.Map.MapView.ScrollSmallChange" /> is used as a scroll step in that direction.
  /// </remarks>
  public virtual PointF ComputeAutoPanDocPosition(Point originPnt, Point viewPnt)
  {
    Point view = this.ConvertDocToView(this.DocPosition);
    Size autoPanRegion = this.AutoPanRegion;
    int width1 = this.ScrollSmallChange.Width;
    int height1 = this.ScrollSmallChange.Height;
    Rectangle displayRectangle = this.DisplayRectangle;
    int num1 = viewPnt.X - originPnt.X;
    int num2 = viewPnt.Y - originPnt.Y;
    int width2 = autoPanRegion.Width;
    int height2 = autoPanRegion.Height;
    int num3 = 2 * width2;
    int num4 = 2 * height2;
    if (num1 < -num3)
    {
      int num5 = (num1 + width2) * (num1 + width2) / 100;
      view.X -= Math.Min(displayRectangle.Width, width1 * num5);
    }
    else if (num1 < -width2)
      view.X -= width1;
    else if (num1 > num3)
    {
      int num6 = (num1 - width2) * (num1 - width2) / 100;
      view.X += Math.Min(displayRectangle.Width, width1 * num6);
    }
    else if (num1 > width2)
      view.X += width1;
    if (num2 < -num4)
    {
      int num7 = (num2 + height2) * (num2 + height2) / 100;
      view.Y -= Math.Min(displayRectangle.Height, height1 * num7);
    }
    else if (num2 < -height2)
      view.Y -= height1;
    else if (num2 > num4)
    {
      int num8 = (num2 - height2) * (num2 - height2) / 100;
      view.Y += Math.Min(displayRectangle.Height, height1 * num8);
    }
    else if (num2 > height2)
      view.Y += height1;
    return this.ConvertViewToDoc(view);
  }

  /// <summary>
  /// Gets or sets the region around the original pan point where automatic panning does not occur.
  /// </summary>
  /// <value>
  /// The value defaults to 16x16; any new values must not be negative.
  /// </value>
  /// <remarks>
  /// This is used by <see cref="M:Intermech.Map.MapView.ComputeAutoPanDocPosition(System.Drawing.Point,System.Drawing.Point)" /> to decide whether the
  /// current mouse point is close enough to the original pan point that no scrolling
  /// should occur.
  /// </remarks>
  [Category("Behavior")]
  [Description("The area around the original pan point outside of which the mouse will automatically cause the view to scroll.")]
  public virtual Size AutoPanRegion
  {
    get => this.myAutoPanRegion;
    set
    {
      if (!(this.myAutoPanRegion != value))
        return;
      if (value.Width < 0 || value.Height < 0)
        throw new ArgumentOutOfRangeException("New Size value for GoView.AutoPanRegion must have non-negative dimensions");
      this.myAutoPanRegion = value;
      this.RaisePropertyChangedEvent(nameof (AutoPanRegion));
    }
  }

  [Description("The margin in the view where a mouse drag will automatically cause the view to scroll.")]
  [Category("Behavior")]
  public virtual Size AutoScrollRegion
  {
    get => this._autoScrollRegion;
    set
    {
      if (!(this._autoScrollRegion != value))
        return;
      if (value.Width < 0 || value.Height < 0)
        throw new ArgumentOutOfRangeException("New Size value for GoView.AutoScrollRegion must have non-negative dimensions");
      this._autoScrollRegion = value;
      this.RaisePropertyChangedEvent(nameof (AutoScrollRegion));
    }
  }

  /// <summary>
  /// This method is called to determine the next position in the document for this view,
  /// given a point at which the user is dragging the mouse.
  /// </summary>
  /// <param name="viewPnt">The mouse point, in view coordinates.</param>
  /// <remarks>
  /// This uses the <see cref="P:Intermech.Map.MapView.AutoScrollRegion" /> and <see cref="P:Intermech.Map.MapView.ScrollSmallChange" />
  /// properties to calculate a new <see cref="P:Intermech.Map.MapView.DocPosition" />.
  /// The closer the point is to the edge of the view, the larger a multiple of
  /// the <see cref="P:Intermech.Map.MapView.ScrollSmallChange" /> is used as a scroll step in that direction.
  /// </remarks>
  public virtual PointF ComputeAutoScrollDocPosition(Point viewPnt)
  {
    Point view = this.ConvertDocToView(this.DocPosition);
    Size autoScrollRegion = this.AutoScrollRegion;
    int width = this.ScrollSmallChange.Width;
    int height = this.ScrollSmallChange.Height;
    Rectangle displayRectangle = this.DisplayRectangle;
    if (viewPnt.X >= displayRectangle.X && viewPnt.X < displayRectangle.X + autoScrollRegion.Width)
    {
      view.X -= width;
      if (viewPnt.X < displayRectangle.X + autoScrollRegion.Width / 2)
        view.X -= width;
      if (viewPnt.X < displayRectangle.X + autoScrollRegion.Width / 4)
        view.X -= 2 * width;
    }
    else if (viewPnt.X <= displayRectangle.X + displayRectangle.Width && viewPnt.X > displayRectangle.X + displayRectangle.Width - autoScrollRegion.Width)
    {
      view.X += width;
      if (viewPnt.X > displayRectangle.X + displayRectangle.Width - autoScrollRegion.Width / 2)
        view.X += width;
      if (viewPnt.X > displayRectangle.X + displayRectangle.Width - autoScrollRegion.Width / 4)
        view.X += 2 * width;
    }
    if (viewPnt.Y >= displayRectangle.Y && viewPnt.Y < displayRectangle.Y + autoScrollRegion.Height)
    {
      view.Y -= height;
      if (viewPnt.Y < displayRectangle.Y + autoScrollRegion.Height / 2)
        view.Y -= height;
      if (viewPnt.Y < displayRectangle.Y + autoScrollRegion.Height / 4)
        view.Y -= 2 * height;
    }
    else if (viewPnt.Y <= displayRectangle.Y + displayRectangle.Height && viewPnt.Y > displayRectangle.Y + displayRectangle.Height - autoScrollRegion.Height)
    {
      view.Y += height;
      if (viewPnt.Y > displayRectangle.Y + displayRectangle.Height - autoScrollRegion.Height / 2)
        view.Y += height;
      if (viewPnt.Y > displayRectangle.Y + displayRectangle.Height - autoScrollRegion.Height / 4)
        view.Y += 2 * height;
    }
    return this.ConvertViewToDoc(view);
  }

  private void DoInternalAutoScroll()
  {
    if (this._autoScrollTimer == null)
    {
      this._autoScrollTimer = new System.Threading.Timer(new TimerCallback(this.autoScrollCallback), (object) new EventHandler(this.autoScrollTick), -1, -1);
      this._autoScrollTimerEnabled = false;
    }
    if ((this._panning ? this.ComputeAutoPanDocPosition(this._panningOrigin, this._autoScrollPoint) : this.ComputeAutoScrollDocPosition(this._autoScrollPoint)) != this.DocPosition)
    {
      if (this._autoScrollTimerEnabled)
        return;
      if (!this.Focused)
        this._autoScrollTimer.Change(this.AutoScrollDelay, -1);
      else
        this._autoScrollTimer.Change(this.AutoScrollTime, -1);
      this._autoScrollTimerEnabled = true;
    }
    else
    {
      if (this._panning)
        return;
      this.StopAutoScroll();
    }
  }

  [DefaultValue(100)]
  [Category("Behavior")]
  [Description("How long to wait before changing the DocPosition during autoscrolling.")]
  public int AutoScrollTime
  {
    get => this._autoScrollTime;
    set
    {
      if (this._autoScrollTime == value || value < 0)
        return;
      this._autoScrollTime = value;
      this.RaisePropertyChangedEvent(nameof (AutoScrollTime));
    }
  }

  /// <summary>Stop any ongoing auto-scroll or auto-pan action.</summary>
  /// <remarks>
  /// This stops the Timer used to get repeating events to consider scrolling.
  /// </remarks>
  /// <seealso cref="M:Intermech.Map.MapView.DoAutoScroll(System.Drawing.Point)" />
  /// <seealso cref="M:Intermech.Map.MapView.DoAutoPan(System.Drawing.Point,System.Drawing.Point)" />
  public void StopAutoScroll()
  {
    if (this._autoScrollTimer == null)
      return;
    this._autoScrollTimer.Change(-1, -1);
    this._autoScrollTimerEnabled = false;
  }

  private void autoScrollCallback(object obj)
  {
    if (!this.IsHandleCreated)
      return;
    this.Invoke((Delegate) obj);
  }

  private void autoScrollTick(object sender, EventArgs evt)
  {
    if (!this._autoScrollTimerEnabled)
      return;
    PointF pointF = this._panning ? this.ComputeAutoPanDocPosition(this._panningOrigin, this._autoScrollPoint) : this.ComputeAutoScrollDocPosition(this._autoScrollPoint);
    if (pointF == this.DocPosition)
    {
      this._autoScrollTimer.Change(this.AutoScrollDelay, -1);
    }
    else
    {
      this.DocPosition = pointF;
      this._autoScrollTimer.Change(this.AutoScrollTime, -1);
    }
  }

  /// <summary>Gets or sets how long to wait before autoscrolling.</summary>
  /// <value>
  /// The time is in milliseconds.
  /// The default is 1000 (one second).
  /// The value must not be negative.
  /// </value>
  /// <remarks>
  /// This is helpful in avoiding autoscrolling when the user is dragging something
  /// into the view and doesn't yet intend to autoscroll.
  /// </remarks>
  [Description("How long to wait in the autoscroll margin before performing any autoscrolling.")]
  [DefaultValue(1000)]
  [Category("Behavior")]
  public int AutoScrollDelay
  {
    get => this._autoScrollDelay;
    set
    {
      if (this._autoScrollDelay == value || value < 0)
        return;
      this._autoScrollDelay = value;
      this.RaisePropertyChangedEvent(nameof (AutoScrollDelay));
    }
  }

  public Cursor GetDefaultCursor() => this.DefaultCursor;

  /// <summary>
  /// Called by the system when needing to fix up invalidated parts of this view.
  /// </summary>
  /// <param name="evt"></param>
  protected override void OnPaint(PaintEventArgs evt)
  {
    base.OnPaint(evt);
    this.onPaintCanvas(evt);
  }

  private void onPaintCanvas(PaintEventArgs evt)
  {
    if (this._suppressPaint > 0)
      return;
    this._paintEventArgs = evt;
    Graphics graphics = evt.Graphics;
    graphics.PageUnit = GraphicsUnit.Pixel;
    Rectangle clipRectangle = evt.ClipRectangle;
    if (!this._cacheValid && clipRectangle.Width > 0 && clipRectangle.Height > 0)
    {
      Rectangle clientRectangle = this.ClientRectangle;
      if (this._cacheImage == null || this._cacheImage.Width < clientRectangle.Width || this._cacheImage.Height < clientRectangle.Height)
      {
        if (this._cacheImage != null)
          this._cacheImage.Dispose();
        this._cacheImage = new Bitmap(clientRectangle.Width, clientRectangle.Height, graphics);
      }
      using (Graphics g = Graphics.FromImage((Image) this._cacheImage))
      {
        g.PageUnit = GraphicsUnit.Pixel;
        g.FillRectangle(SystemBrushes.Control, 0, 0, this._cacheImage.Width, this._cacheImage.Height);
        this.PaintBorder(g, clientRectangle, clipRectangle);
        Rectangle rectangle = Rectangle.Intersect(clipRectangle, this.DisplayRectangle);
        g.IntersectClip(rectangle);
        RectangleF doc = this.ConvertViewToDoc(rectangle);
        g.TranslateTransform((float) this._borderSize.Width, (float) this._borderSize.Height);
        g.ScaleTransform(this._horizScale, this._vertScale);
        PointF docPosition = this.DocPosition;
        g.TranslateTransform(-docPosition.X, -docPosition.Y);
        this.PaintView(g, doc);
        this._cacheValid = true;
      }
    }
    if (this._cacheImage == null)
      return;
    graphics.DrawImage((Image) this._cacheImage, clipRectangle, clipRectangle, GraphicsUnit.Pixel);
  }

  /// <summary>
  /// Paint all of the objects of this view or its document that are visible in
  /// the given rectangle.
  /// </summary>
  /// <param name="g"></param>
  /// <param name="clipRect">A <c>RectangleF</c> in document coordinates.</param>
  /// <remarks>
  /// This calls <see cref="M:Intermech.Map.MapView.PaintPaperColor(System.Drawing.Graphics,System.Drawing.RectangleF)" />,
  /// <see cref="M:Intermech.Map.MapView.PaintBackgroundDecoration(System.Drawing.Graphics,System.Drawing.RectangleF)" />, and
  /// <see cref="M:Intermech.Map.MapView.PaintObjects(System.Boolean,System.Boolean,System.Drawing.Graphics,System.Drawing.RectangleF)" /> for both document and view objects.
  /// </remarks>
  protected virtual void PaintView(Graphics g, RectangleF clipRect)
  {
    this.PaintPaperColor(g, clipRect);
    this.PaintBackgroundDecoration(g, clipRect);
    g.SmoothingMode = this.SmoothingMode;
    g.TextRenderingHint = this.TextRenderingHint;
    g.InterpolationMode = this.InterpolationMode;
    this.PaintObjects(true, true, g, clipRect);
  }

  /// <summary>
  /// Draw any decoration that should appear behind all of the objects.
  /// </summary>
  /// <param name="g"></param>
  /// <param name="clipRect"></param>
  /// <remarks>
  /// By default this method draws this <c>Control</c>'s <c>BackgroundImage</c>,
  /// if any, and then a grid, according to the <see cref="P:Intermech.Map.MapView.GridStyle" />.
  /// </remarks>
  protected virtual void PaintBackgroundDecoration(Graphics g, RectangleF clipRect)
  {
  }

  /// <summary>Draw small crosses at the grid points.</summary>
  /// <param name="g"></param>
  /// <param name="cross"></param>
  /// <param name="clipRect"></param>
  protected virtual void DrawGridCrosses(Graphics g, SizeF cross, RectangleF clipRect)
  {
  }

  /// <summary>Draw continuous lines for the grids.</summary>
  /// <param name="g"></param>
  /// <param name="clipRect"></param>
  protected virtual void DrawGridLines(Graphics g, RectangleF clipRect)
  {
  }

  /// <summary>Gets or sets the style of the grid.</summary>
  /// <value>
  /// This <see cref="T:Intermech.Map.MapViewGridStyle" /> value defaults to <see cref="F:Intermech.Map.MapViewGridStyle.None" />.
  /// </value>
  /// <seealso cref="P:Intermech.Map.MapView.GridOrigin" />
  /// <seealso cref="P:Intermech.Map.MapView.GridCellSize" />
  /// <seealso cref="P:Intermech.Map.MapView.GridColor" />
  [Description("The appearance style of the grid.")]
  [Category("Grid")]
  [DefaultValue(0)]
  public virtual ViewGridStyle GridStyle
  {
    get => this._gridStyle;
    set
    {
      if (this._gridStyle == value)
        return;
      this._gridStyle = value;
      this.RaisePropertyChangedEvent(nameof (GridStyle));
    }
  }

  /// <summary>
  /// Fill in the document paper color or view background color.
  /// </summary>
  /// <param name="g"></param>
  /// <param name="clipRect"></param>
  /// <remarks>
  /// If this view's document's <see cref="P:Intermech.Map.MapDocument.PaperColor" /> is
  /// <c>Color.Empty</c> we use this view's <c>BackColor</c> instead.
  /// </remarks>
  protected virtual void PaintPaperColor(Graphics g, RectangleF clipRect)
  {
    Color color = Color.Empty;
    if (this.Document != null)
      color = this.Document.PaperColor;
    if (color == Color.Empty)
      color = this.BackColor;
    if (this._backgroundBrush == null || this._backgroundBrush.Color != color)
    {
      if (this._backgroundBrush != null)
        this._backgroundBrush.Dispose();
      this._backgroundBrush = new SolidBrush(color);
    }
    g.FillRectangle((Brush) this._backgroundBrush, clipRect);
  }

  /// <summary>Paint all the document and/or view objects.</summary>
  /// <param name="doc">If true, paint document objects.</param>
  /// <param name="view">If true, paint view objects.</param>
  /// <param name="g"></param>
  /// <param name="clipRect"></param>
  /// <seealso cref="M:Intermech.Map.MapLayer.Paint(System.Drawing.Graphics,Intermech.Map.MapView,System.Drawing.RectangleF)" />
  protected virtual void PaintObjects(bool doc, bool view, Graphics g, RectangleF clipRect)
  {
    if (this.Document == null)
      return;
    foreach (Layer layer in this.Document.Layers)
      layer.Paint(g, (IView) this, clipRect);
  }

  [Category("Appearance")]
  [DefaultValue(2)]
  [Description("How nicely lines are drawn")]
  public SmoothingMode SmoothingMode
  {
    get => this._smoothingMode;
    set
    {
      if (this._smoothingMode == value)
        return;
      this._smoothingMode = value;
      this.RaisePropertyChangedEvent(nameof (SmoothingMode));
    }
  }

  [Category("Appearance")]
  [Description("How nicely text is rendered")]
  [DefaultValue(5)]
  public TextRenderingHint TextRenderingHint
  {
    get => this._textRenderingHint;
    set
    {
      if (this._textRenderingHint == value)
        return;
      this._textRenderingHint = value;
      this.RaisePropertyChangedEvent(nameof (TextRenderingHint));
    }
  }

  [DefaultValue(2)]
  [Description("How images are rendered when scaled or stretched")]
  [Category("Appearance")]
  public InterpolationMode InterpolationMode
  {
    get => this._interpolationMode;
    set
    {
      if (this._interpolationMode == value)
        return;
      this._interpolationMode = value;
      this.RaisePropertyChangedEvent(nameof (InterpolationMode));
    }
  }

  internal void PaintBorder(Graphics g, Rectangle rect, Rectangle clipRect)
  {
    switch (this.BorderStyle)
    {
      case BorderStyle.None:
        break;
      case BorderStyle.FixedSingle:
        if (clipRect.X > rect.X + this._borderSize.Width && clipRect.Y > rect.Y + this._borderSize.Height && clipRect.X + clipRect.Width < rect.X + rect.Width - this._borderSize.Width && clipRect.Y + clipRect.Height < rect.Y + rect.Height - this._borderSize.Height)
          break;
        g.DrawRectangle(SystemPens.WindowFrame, rect);
        break;
      default:
        if (clipRect.X > rect.X + this._borderSize.Width && clipRect.Y > rect.Y + this._borderSize.Height && clipRect.X + clipRect.Width < rect.X + rect.Width - this._borderSize.Width && clipRect.Y + clipRect.Height < rect.Y + rect.Height - this._borderSize.Height)
          break;
        ControlPaint.DrawBorder3D(g, rect, this.Border3DStyle);
        break;
    }
  }

  [Description("The 3D border style for this view, when BorderStyle is Fixed3D.")]
  [DefaultValue(6)]
  [Category("Appearance")]
  public virtual Border3DStyle Border3DStyle
  {
    get => this._border3DStyle;
    set
    {
      if (this._border3DStyle == value)
        return;
      this._border3DStyle = value;
      this.RaisePropertyChangedEvent(nameof (Border3DStyle));
    }
  }

  public virtual void ZoomIn() => this.ZoomToScale(0.85f);

  public virtual void ZoomOut() => this.ZoomToScale(1.15f);

  public virtual void ZoomToScale(float scale)
  {
    Size size = this.DisplayRectangle.Size;
    PointF docPosition = this.DocPosition;
    Rectangle displayRectangle = this.DisplayRectangle;
    int left = displayRectangle.Left;
    displayRectangle = this.DisplayRectangle;
    int right = displayRectangle.Right;
    int x = (left + right) / 2;
    displayRectangle = this.DisplayRectangle;
    int top = displayRectangle.Top;
    displayRectangle = this.DisplayRectangle;
    int bottom = displayRectangle.Bottom;
    int y = (top + bottom) / 2;
    PointF doc = this.ConvertViewToDoc(new Point(x, y));
    RectangleF docBox = new RectangleF(0.0f, 0.0f, (float) (((double) doc.X - (double) docPosition.X) * 2.0) * scale, (float) (((double) doc.Y - (double) docPosition.Y) * 2.0) * scale);
    docBox.X = doc.X - docBox.Width / 2f;
    docBox.Y = doc.Y - docBox.Height / 2f;
    this.ZoomToBox(docBox);
  }

  public virtual void ZoomToScale(PointF viewpt, float scale)
  {
    Size size = this.DisplayRectangle.Size;
    PointF docPosition = this.DocPosition;
    Rectangle displayRectangle = this.DisplayRectangle;
    PointF doc = this.ConvertViewToDoc(new Point((displayRectangle.Left + displayRectangle.Right) / 2, (displayRectangle.Top + displayRectangle.Bottom) / 2));
    RectangleF docBox = new RectangleF(0.0f, 0.0f, (float) (((double) doc.X - (double) docPosition.X) * 2.0), (float) (((double) doc.Y - (double) docPosition.Y) * 2.0));
    docBox.Width *= scale;
    docBox.Height *= scale;
    docBox.X = (float) ((1.0 - (double) scale) * (double) viewpt.X + (double) doc.X - (1.0 - (double) scale) * (double) doc.X - (double) docBox.Width / 2.0);
    docBox.Y = (float) ((1.0 - (double) scale) * (double) viewpt.Y + (double) doc.Y - (1.0 - (double) scale) * (double) doc.Y - (double) docBox.Height / 2.0);
    this.ZoomToBox(docBox);
  }

  public virtual void ZoomToBox(RectangleF docBox)
  {
    this.OnViewChanging();
    this.OriginDocPosition = new PointF(docBox.X, docBox.Y);
    float num = this.DocScale;
    if ((double) docBox.Width > 0.0 && (double) docBox.Height > 0.0)
    {
      Size size = this.DisplayRectangle.Size;
      num = Math.Min((float) size.Width / docBox.Width, (float) size.Height / docBox.Height);
    }
    this.DocScale = num;
    this.OnViewChanged();
    this.UpdateView();
  }

  public void SetPosAndScale(PointF newPos, float newScale)
  {
    this.DocScale = newScale;
    this.OriginDocPosition = newPos;
    this.UpdateView();
  }

  private void OnViewChanged()
  {
    if (this.ViewChanged == null)
      return;
    this.ViewChanged((object) this, EventArgs.Empty);
  }

  private void OnViewChanging()
  {
    if (this.ViewChanging == null)
      return;
    this.ViewChanging((object) this, EventArgs.Empty);
  }

  /// <summary>
  /// Change the DocScale property so that all document objects are visible.
  /// </summary>
  /// <remarks>
  /// By default this will include the (0, 0) origin.
  /// If the document is very large, the <see cref="M:Intermech.Map.MapView.LimitDocScale(System.Single)" />
  /// method might prevent the whole document from fitting.
  /// Calling this method will not necessarily cause the scroll bars to
  /// disappear, because the scroll bars normally show the extent of the
  /// document, which is normally greater than the extent of the actual
  /// objects in the document.
  /// </remarks>
  public virtual void ZoomToFit() => this.ZoomToBox(this.ComputeDocumentBounds());

  /// <summary>Gets the area where the view displays its document.</summary>
  /// <value>
  /// The <c>Rectangle</c> value specifies an area in control coordinates
  /// relative to the top left corner of this control.
  /// </value>
  /// <remarks>
  /// The display rectangle is normally smaller than the <c>Control.Size</c>,
  /// because of the scroll bars and the border along the edges.
  /// Note the difference with <see cref="P:Intermech.Map.MapView.DocExtent" />, which gets an area
  /// in a document in document coordinates depending on the <see cref="P:Intermech.Map.MapView.DocPosition" />
  /// and <see cref="P:Intermech.Map.MapView.DocScale" />.
  /// </remarks>
  /// <seealso cref="M:Intermech.Map.MapView.ConvertViewToDoc(System.Drawing.Rectangle)" />
  [Browsable(false)]
  public override Rectangle DisplayRectangle
  {
    get
    {
      Size size = this.Size;
      int val2_1 = size.Width - 2 * this._borderSize.Width;
      if (this.VerticalScrollBar != null && this.VerticalScrollBar.Visible)
        val2_1 -= this.myScrollBarWidth;
      int val2_2 = size.Height - 2 * this._borderSize.Height;
      if (this.HorizontalScrollBar != null && this.HorizontalScrollBar.Visible)
        val2_2 -= this.myScrollBarHeight;
      return new Rectangle(this._borderSize.Width, this._borderSize.Height, Math.Max(1, val2_1), Math.Max(1, val2_2));
    }
  }

  /// <summary>
  /// Redraw the whole view if the Control style has changed.
  /// </summary>
  /// <param name="evt"></param>
  /// <remarks>
  /// This is not supported in GoDiagram Pocket, due to differences in .NET
  /// Compact Framework controls.
  /// </remarks>
  protected override void OnStyleChanged(EventArgs evt)
  {
    base.OnStyleChanged(evt);
    this.UpdateView();
  }

  /// <summary>
  /// Redraw the whole view if the system colors have changed.
  /// </summary>
  /// <param name="evt"></param>
  /// <remarks>
  /// This is not supported in GoDiagram Pocket, due to differences in .NET
  /// Compact Framework controls.
  /// </remarks>
  protected override void OnSystemColorsChanged(EventArgs evt)
  {
    base.OnSystemColorsChanged(evt);
    this.UpdateView();
  }

  /// <summary>
  /// Create an instance of the default <see cref="P:Intermech.Map.MapView.DefaultTool" /> for this view.
  /// </summary>
  /// <returns></returns>
  /// <remarks>
  /// By default this creates an instance of <see cref="T:Intermech.Map.MapToolManager" />.
  /// </remarks>
  /// <seealso cref="P:Intermech.Map.MapView.Tool" />
  public virtual ITool CreateDefaultTool() => (ITool) new ToolManager((IView) this);

  /// <summary>
  /// Gets or sets whether the user can select objects in this view.
  /// </summary>
  /// <remarks>
  /// A false value prevents the user from selecting objects in this view
  /// by the normal mechanisms.
  /// Even when this property value is true, some objects might not be
  /// selectable by the user because the document or the object disallows it
  /// or because the object is not visible.
  /// Your code can always select objects programmatically by calling
  /// <c>Selection.Select(obj)</c> or <c>Selection.Add(obj)</c>.
  /// </remarks>
  /// <seealso cref="M:Intermech.Map.MapView.CanSelectObjects" />
  /// <seealso cref="P:Intermech.Map.MapDocument.AllowSelect" />
  [DefaultValue(true)]
  [Category("Behavior")]
  [Description("Whether the user can select objects, if visible.")]
  public bool AllowSelect
  {
    get => this._allowSelect;
    set
    {
      if (this._allowSelect == value)
        return;
      this._allowSelect = value;
      this.RaisePropertyChangedEvent(nameof (AllowSelect));
    }
  }

  /// <summary>
  /// This method is responsible for finding a tooltip string for an object and then
  /// displaying it in a ToolTip.
  /// </summary>
  /// <param name="obj"></param>
  /// <remarks>
  /// This calls <see cref="M:Intermech.Map.MapObject.GetToolTip(Intermech.Map.MapView)" /> on the given object
  /// and its <see cref="P:Intermech.Map.MapObject.Parent" />'s until it gets a non-null
  /// <c>String</c> return value.
  /// By default this method does nothing if this view has no <see cref="P:Intermech.Map.MapView.ToolTip" />
  /// <c>Control</c>.
  /// This method is normally called by <see cref="M:Intermech.Map.MapView.DoMouseOver(Intermech.Map.MapInputEventArgs)" />.
  /// </remarks>
  public virtual void DoToolTipObject(IObject obj)
  {
    if (this.ToolTip == null)
      return;
    string toolTip = this.ToolTip.GetToolTip((Control) this);
    string caption = (string) null;
    for (; obj != null; obj = obj.Parent)
    {
      caption = obj.GetToolTip((IView) this);
      if (caption != null)
        break;
    }
    if (caption == null)
      caption = "";
    if (!(caption != toolTip))
      return;
    this.ToolTip.SetToolTip((Control) this, caption);
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual ToolTip ToolTip
  {
    get => this._toolTip;
    set
    {
      if (this._toolTip == value)
        return;
      this._toolTip = value;
      this.RaisePropertyChangedEvent(nameof (ToolTip));
    }
  }

  public void SetDefaults()
  {
  }

  public virtual void MoveSelection(IObject iObject, SizeF ef2)
  {
  }

  [SpecialName]
  void IView.add_Paint(PaintEventHandler value) => this.Paint += value;

  [SpecialName]
  void IView.remove_Paint(PaintEventHandler value) => this.Paint -= value;

  [SpecialName]
  void IView.add_Resize(EventHandler value) => this.Resize += value;

  [SpecialName]
  void IView.remove_Resize(EventHandler value) => this.Resize -= value;

  void IView.Invalidate(Rectangle rectangle) => this.Invalidate(rectangle);

  [SpecialName]
  bool IView.get_Visible() => this.Visible;

  [SpecialName]
  void IView.set_Visible(bool value) => this.Visible = value;
}
