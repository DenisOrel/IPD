
// Type: Intermech.DocumentView.InputEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.DocumentView;

/// <summary>
/// Holds information for unified input events for views, for both
/// keyboard input and mouse input, including mouse button and mouse wheel
/// and drag-and-drop mouse actions, where no <see cref="T:Intermech.Map.MapObject" />
/// is involved.
/// </summary>
/// <remarks>
/// For input events that occur in the "background", there is of course
/// no particular <see cref="T:Intermech.Map.MapObject" />.  For input events that do
/// involve an object, event handlers use the <see cref="T:Intermech.Map.MapObjectEventArgs" />
/// class.  When no particular input information is associated with an
/// event, <see cref="T:Intermech.Map.MapSelectionEventArgs" /> is used when there is a
/// particular object, and <see cref="T:System.EventArgs" /> is used otherwise.
/// </remarks>
/// <seealso cref="E:Intermech.Map.MapView.BackgroundDoubleClicked" />
/// <seealso cref="E:Intermech.Map.MapView.BackgroundSingleClicked" />
[Serializable]
public class InputEventArgs : EventArgs
{
  private MouseButtons myButtons;
  private int myDelta;
  private PointF myDocPoint;
  private bool myDoubleClick;
  private DragEventArgs myDragEventArgs;
  private Keys myKey;
  private KeyEventArgs myKeyEventArgs;
  private Keys myModifiers;
  private MouseEventArgs myMouseEventArgs;
  private Point myViewPoint;

  /// <summary>
  /// The constructor produces an empty object, describing no event.
  /// </summary>
  public InputEventArgs()
  {
    this.myButtons = MouseButtons.None;
    this.myModifiers = Keys.None;
    this.myKey = Keys.None;
    this.myMouseEventArgs = (MouseEventArgs) null;
    this.myDragEventArgs = (DragEventArgs) null;
    this.myKeyEventArgs = (KeyEventArgs) null;
    this.myDoubleClick = false;
    this.myDelta = 0;
  }

  /// <summary>
  /// This copy constructor makes a copy of the argument object.
  /// </summary>
  /// <param name="evt"></param>
  public InputEventArgs(InputEventArgs evt)
  {
    this.myButtons = MouseButtons.None;
    this.myModifiers = Keys.None;
    this.myKey = Keys.None;
    this.myMouseEventArgs = (MouseEventArgs) null;
    this.myDragEventArgs = (DragEventArgs) null;
    this.myKeyEventArgs = (KeyEventArgs) null;
    this.myDoubleClick = false;
    this.myDelta = 0;
    this.ViewPoint = evt.ViewPoint;
    this.DocPoint = evt.DocPoint;
    this.Buttons = evt.Buttons;
    this.Modifiers = evt.Modifiers;
    this.Key = evt.Key;
    this.MouseEventArgs = evt.MouseEventArgs;
    this.DragEventArgs = evt.DragEventArgs;
    this.KeyEventArgs = evt.KeyEventArgs;
    this.DoubleClick = evt.DoubleClick;
    this.Delta = evt.Delta;
  }

  /// <summary>
  /// Gets whether <see cref="P:Intermech.Map.MapInputEventArgs.Modifiers" /> has <c>Keys.Alt</c> set.
  /// </summary>
  public virtual bool Alt => (this.Modifiers & Keys.Alt) == Keys.Alt;

  /// <summary>
  /// Gets or sets the MouseButtons used with this input event.
  /// </summary>
  /// <value>
  /// The <c>MouseButtons</c> value will be some combination of
  /// <c>MouseButtons.Left</c>, <c>MouseButtons.Middle</c>, and <c>MouseButtons.Right</c>.
  /// </value>
  /// <remarks>
  /// This value may not be meaningful for keyboard input, but should be valid
  /// for mouse and drag-and-drop events.
  /// </remarks>
  public MouseButtons Buttons
  {
    get => this.myButtons;
    set => this.myButtons = value;
  }

  /// <summary>
  /// Gets whether <see cref="P:Intermech.Map.MapInputEventArgs.Modifiers" /> has <c>Keys.Control</c> set.
  /// </summary>
  public virtual bool Control => (this.Modifiers & Keys.Control) == Keys.Control;

  /// <summary>
  /// Gets or sets the amount of change associated with a mouse-wheel rotation.
  /// </summary>
  public int Delta
  {
    get => this.myDelta;
    set => this.myDelta = value;
  }

  /// <summary>
  /// Gets or sets the point at which this input event occurred.
  /// </summary>
  /// <value>
  /// The <c>PointF</c> is in document coordinates.
  /// </value>
  /// <remarks>
  /// This should be valid for mouse and drag-and-drop events.
  /// For keyboard input, this is the last available mouse point.
  /// </remarks>
  public PointF DocPoint
  {
    get => this.myDocPoint;
    set => this.myDocPoint = value;
  }

  /// <summary>Gets or sets whether this is a double-click event.</summary>
  public bool DoubleClick
  {
    get => this.myDoubleClick;
    set => this.myDoubleClick = value;
  }

  public DragEventArgs DragEventArgs
  {
    get => this.myDragEventArgs;
    set => this.myDragEventArgs = value;
  }

  /// <summary>
  /// Gets whether <see cref="P:Intermech.Map.MapInputEventArgs.Buttons" /> equals <c>MouseButtons.Right</c>.
  /// </summary>
  public virtual bool IsContextButton => this.Buttons == MouseButtons.Right;

  /// <summary>Gets or sets the key pressed as this input event.</summary>
  /// <remarks>
  /// The <c>Keys</c> value will be something like <c>Keys.C</c>.
  /// </remarks>
  public Keys Key
  {
    get => this.myKey;
    set => this.myKey = value;
  }

  public KeyEventArgs KeyEventArgs
  {
    get => this.myKeyEventArgs;
    set => this.myKeyEventArgs = value;
  }

  /// <summary>
  /// Gets or sets the modifier keys used with this input event.
  /// </summary>
  /// <value>
  /// The <c>Keys</c> value will be some combination of
  /// <c>Keys.Control</c>, <c>Keys.Shift</c>, and <c>Keys.Alt</c>.
  /// </value>
  public Keys Modifiers
  {
    get => this.myModifiers;
    set => this.myModifiers = value;
  }

  public MouseEventArgs MouseEventArgs
  {
    get => this.myMouseEventArgs;
    set => this.myMouseEventArgs = value;
  }

  /// <summary>
  /// Gets whether <see cref="P:Intermech.Map.MapInputEventArgs.Modifiers" /> has <c>Keys.Shift</c> set.
  /// </summary>
  public virtual bool Shift => (this.Modifiers & Keys.Shift) == Keys.Shift;

  /// <summary>
  /// Gets or sets the point at which this input event occurred.
  /// </summary>
  /// <value>
  /// The <c>Point</c> is in view coordinates.
  /// </value>
  /// <remarks>
  /// This should be valid for mouse and drag-and-drop events.
  /// For keyboard input, this is the last available mouse point.
  /// </remarks>
  /// <seealso cref="P:Intermech.Map.MapInputEventArgs.DocPoint" />
  public Point ViewPoint
  {
    get => this.myViewPoint;
    set => this.myViewPoint = value;
  }
}
