
// Type: Intermech.DocumentView.AbstractTool
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.DocumentView;

/// <summary>
/// This abstract class provides the base for all of the predefined tools.
/// </summary>
[Serializable]
public abstract class AbstractTool : ITool
{
  private Size _dragSize;
  [NonSerialized]
  private IObject _currentObject;
  [NonSerialized]
  private string _stopTransactionName;
  [NonSerialized]
  private IView _view;

  /// <summary>The constructor associates a view with the tool.</summary>
  /// <param name="v">
  /// This <see cref="T:Intermech.Map.MapView" /> must not be null.
  /// </param>
  protected AbstractTool(IView v)
  {
    this._view = (IView) null;
    this._dragSize = SystemInformation.DragSize;
    this._stopTransactionName = (string) null;
    this._currentObject = (IObject) null;
    this._view = v != null ? v : throw new ArgumentNullException(sc_2504.ssp_imclient_2505());
  }

  /// <summary>
  /// This predicate should be true if this tool can be activated to be the view's current tool.
  /// </summary>
  /// <returns></returns>
  /// <remarks>
  /// By default, this returns true.
  /// This is normally only called by the <see cref="T:Intermech.Map.MapToolManager" /> to decide whether this tool should be started as a
  /// mode-less mouse tool.
  /// </remarks>
  public virtual bool CanStart() => true;

  /// <summary>
  /// The view calls this method when the user cancels the gesture with the mouse;
  /// all of the event information is provided by the <see cref="P:Intermech.Map.MapTool.LastInput" /> property.
  /// </summary>
  /// <remarks>
  /// By default, this just calls <see cref="M:Intermech.Map.MapTool.StopTool" />.
  /// </remarks>
  public virtual void DoCancelMouse() => this.StopTool();

  /// <summary>
  /// Any tool can call this method in order to implement the standard click behavior.
  /// </summary>
  /// <param name="evt">a <see cref="T:Intermech.Map.MapInputEventArgs" /> describing the input event</param>
  /// <returns></returns>
  /// <remarks>
  /// By default, this just calls either <see cref="M:Intermech.Map.MapView.DoDoubleClick(Intermech.Map.MapInputEventArgs)" />
  /// or <see cref="M:Intermech.Map.MapView.DoSingleClick(Intermech.Map.MapInputEventArgs)" />, depending on whether
  /// <see cref="P:Intermech.Map.MapInputEventArgs.DoubleClick" /> is true.
  /// </remarks>
  public virtual bool DoClick(InputEventArgs evt)
  {
    return evt.DoubleClick ? this.View.DoDoubleClick(evt) : this.View.DoSingleClick(evt);
  }

  /// <summary>
  /// The view calls this method when the user presses a key on the keyboard;
  /// all of the event information is provided by the <see cref="P:Intermech.Map.MapTool.LastInput" /> property.
  /// </summary>
  /// <remarks>
  /// By default, this just calls <see cref="M:Intermech.Map.MapTool.DoCancelMouse" /> if the user pressed
  /// the <c>Escape</c> key.
  /// </remarks>
  public virtual void DoKeyDown()
  {
    if (this.LastInput.Key != Keys.Escape)
      return;
    this.DoCancelMouse();
  }

  /// <summary>
  /// The view calls this method upon a mouse down event; all of the event
  /// information is provided by the <see cref="P:Intermech.Map.MapTool.LastInput" /> property.
  /// </summary>
  /// <remarks>By default, this does nothing.</remarks>
  public virtual void DoMouseDown()
  {
  }

  /// <summary>
  /// The view calls this method after the mouse rests for a while at a point;
  /// all of the event information is provided by the <see cref="P:Intermech.Map.MapTool.LastInput" /> property.
  /// </summary>
  /// <remarks>By default, this does nothing.</remarks>
  public virtual void DoMouseHover()
  {
  }

  /// <summary>
  /// The view calls this method upon a mouse move event; all of the event
  /// information is provided by the <see cref="P:Intermech.Map.MapTool.LastInput" /> property.
  /// </summary>
  /// <remarks>By default, this does nothing.</remarks>
  public virtual void DoMouseMove()
  {
  }

  /// <summary>
  /// The view calls this method upon a mouse up event; all of the event
  /// information is provided by the <see cref="P:Intermech.Map.MapTool.LastInput" /> property.
  /// </summary>
  /// <remarks>
  /// By default, this just calls <see cref="M:Intermech.Map.MapTool.StopTool" />.
  /// </remarks>
  public virtual void DoMouseUp() => this.StopTool();

  /// <summary>
  /// The view calls this method as the mouse wheel rotates; all of the event
  /// information is provided by the <see cref="P:Intermech.Map.MapTool.LastInput" /> property.
  /// </summary>
  /// <remarks>By default this does nothing.</remarks>
  public virtual void DoMouseWheel()
  {
  }

  /// <summary>
  /// Any tool can call this method in order to implement the standard selection behavior
  /// for a user click.
  /// </summary>
  /// <param name="evt">a <see cref="T:Intermech.Map.MapInputEventArgs" /> describing the input event</param>
  /// <remarks>
  /// This sets the <see cref="P:Intermech.Map.MapTool.CurrentObject" /> to be the result of a call
  /// to the view's <see cref="M:Intermech.Map.MapView.PickObject(System.Boolean,System.Boolean,System.Drawing.PointF,System.Boolean)" /> to pick the selectable
  /// document object at the current point.
  /// If an object is found, what happens to the selection depends on any
  /// modifiers to the event:
  /// if <see cref="P:Intermech.Map.MapInputEventArgs.Control" /> is true,
  /// we toggle the selectedness of the current object;
  /// if <see cref="P:Intermech.Map.MapInputEventArgs.Shift" /> is true,
  /// we add the current object to the selection;
  /// otherwise we just make the current object the only selection.
  /// If no object is found and neither <see cref="P:Intermech.Map.MapInputEventArgs.Control" />
  /// nor <see cref="P:Intermech.Map.MapInputEventArgs.Shift" /> are true, we empty the selection.
  /// </remarks>
  public virtual void DoSelect(InputEventArgs evt)
  {
    this.CurrentObject = this.View.PickObject(true, false, evt.DocPoint, true);
    if (this.CurrentObject != null)
    {
      if (evt.Control)
        this.Selection.Toggle(this.CurrentObject);
      else if (evt.Shift)
        this.Selection.Add(this.CurrentObject);
      else
        this.Selection.Select(this.CurrentObject);
    }
    else
    {
      if (evt.Control || evt.Shift)
        return;
      this.Selection.Clear();
    }
  }

  /// <summary>
  /// This method is called when this tool becomes the view's current tool.
  /// </summary>
  /// <remarks>
  /// Typically you will want to put initialization code here for each time the tool is started.
  /// By default, this does nothing.
  /// You should not normally be calling this method directly--only the view should.
  /// </remarks>
  public virtual void Start()
  {
  }

  /// <summary>Start a transaction on the view.</summary>
  /// <returns></returns>
  /// <remarks>
  /// This is typically called in overrides of <see cref="M:Intermech.Map.MapTool.Start" />.
  /// This method also sets the <see cref="P:Intermech.Map.MapTool.TransactionResult" /> to null,
  /// so that a call to <see cref="M:Intermech.Map.MapTool.StopTransaction" /> will abort the
  /// transaction rather than finishing it normally.
  /// Not all tools involve changes to the view's document, and thus not
  /// all tools need to start and stop transactions.
  /// </remarks>
  public bool StartTransaction()
  {
    this.TransactionResult = (string) null;
    return this.View.StartTransaction();
  }

  /// <summary>
  /// This method is called when this tool is about to be replaced as the view's current tool.
  /// </summary>
  /// <remarks>
  /// Typically you will want to put termination code here for each time the tool is stopped.
  /// By default, this does nothing.
  /// You should not normally be calling this method directly--only the view should.
  /// If you want to cause this tool to stop, call <see cref="M:Intermech.Map.MapTool.StopTool" /> instead,
  /// which will eventually call this method.
  /// </remarks>
  public virtual void Stop()
  {
  }

  /// <summary>
  /// This method just causes the view's current tool to be stopped
  /// and to start the view's default tool instead as the current tool.
  /// </summary>
  /// <remarks>
  /// Call this method when this tool is finished its task.
  /// When the view replaces this tool with the default one, it will
  /// call the <see cref="M:Intermech.Map.MapTool.Stop" /> method on this tool.
  /// </remarks>
  public void StopTool()
  {
    if (this.View.Tool != this)
      return;
    this.View.Tool = (ITool) null;
  }

  /// <summary>
  /// Stop the current transaction, aborting it if <see cref="P:Intermech.Map.MapTool.TransactionResult" /> is null.
  /// </summary>
  /// <returns></returns>
  /// <remarks>
  /// This is typically called in overrides of <see cref="M:Intermech.Map.MapTool.Stop" />.
  /// </remarks>
  public bool StopTransaction()
  {
    return this.TransactionResult == null ? this.View.AbortTransaction() : this.View.FinishTransaction(this.TransactionResult);
  }

  /// <summary>Gets or sets this tool's CurrentObject property.</summary>
  /// <remarks>
  /// Often different methods of a tool will need to deal with the "current"
  /// <see cref="T:Intermech.Map.MapObject" /> that the user is working with.  This property
  /// is provided so each tool doesn't need to define it.
  /// </remarks>
  public IObject CurrentObject
  {
    get => this._currentObject;
    set => this._currentObject = value;
  }

  /// <summary>
  /// Gets or sets the dimensions, in pixels, of the rectangle that a drag operation must extend
  /// to be considered a drag operation.
  /// </summary>
  /// <value>
  /// This <c>Size</c> is in view coordinates, not in document coordinates.
  /// The default value is 4x4.
  /// </value>
  /// <remarks>The rectangle is centered on the mouse-down point.</remarks>
  internal Size DragSize
  {
    get => this._dragSize;
    set => this._dragSize = value;
  }

  /// <summary>Gets this view's FirstInput property.</summary>
  public InputEventArgs FirstInput => this.View.FirstInput;

  /// <summary>Gets this view's LastInput property.</summary>
  public InputEventArgs LastInput => this.View.LastInput;

  /// <summary>Gets this view's Selection property.</summary>
  public ISelection Selection => this.View.Selection;

  /// <summary>
  /// Gets or sets whether to abort the current transaction if this tool is stopped;
  /// if set to a string, the string specifies the name of the transaction that will
  /// be finished when the tool stops.
  /// </summary>
  /// <remarks>
  /// This determines whether <see cref="M:Intermech.Map.MapTool.StopTransaction" /> calls
  /// <see cref="M:Intermech.DocumentView.IView.AbortTransaction" /> or <see cref="M:Intermech.DocumentView.IView.FinishTransaction(System.String)" />,
  /// depending on whether the value is null or a <c>String</c>.
  /// </remarks>
  public string TransactionResult
  {
    get => this._stopTransactionName;
    set => this._stopTransactionName = value;
  }

  /// <summary>
  /// Gets the view for which this tool is handling canonicalized input events.
  /// </summary>
  public IView View
  {
    get => this._view;
    set
    {
      if (value == null)
        return;
      this._view = value;
    }
  }

  /// <summary>
  /// This shared method helps do subtraction of <c>PointF</c> values.
  /// </summary>
  /// <param name="a"></param>
  /// <param name="b"></param>
  /// <returns></returns>
  public static SizeF SubtractPoints(PointF a, PointF b) => new SizeF(a.X - b.X, a.Y - b.Y);

  /// <summary>
  /// This shared method helps do subtraction of <c>PointF</c> and <c>SizeF</c> values.
  /// </summary>
  /// <param name="a"></param>
  /// <param name="b"></param>
  /// <returns></returns>
  public static SizeF SubtractPoints(PointF a, SizeF b) => new SizeF(a.X - b.Width, a.Y - b.Height);

  /// <summary>
  /// This shared method helps do subtraction of <c>PointF</c> and <c>SizeF</c> values.
  /// </summary>
  /// <param name="a"></param>
  /// <param name="b"></param>
  /// <returns></returns>
  public static SizeF SubtractPoints(SizeF a, PointF b) => new SizeF(a.Width - b.X, a.Height - b.Y);
}
