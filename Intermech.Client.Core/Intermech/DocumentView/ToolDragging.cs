
// Type: Intermech.DocumentView.ToolDragging
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.DocumentView;

/// <summary>
/// The tool used to implement dragging behavior, for moving and copying objects.
/// </summary>
/// <remarks>
/// This tool is expected to be invoked upon a mouse move.
/// </remarks>
[Serializable]
public class ToolDragging : AbstractTool
{
  [NonSerialized]
  private SizeF myMoveOffset;
  [NonSerialized]
  internal bool mySelectionSet;

  /// <summary>The standard tool constructor.</summary>
  /// <param name="v"></param>
  public ToolDragging(IView v)
    : base(v)
  {
    this.myMoveOffset = new SizeF();
    this.mySelectionSet = false;
  }

  private bool alreadyDragged(Hashtable draggeds, IObject o)
  {
    for (IObject key = o; key != null; key = key.Parent)
    {
      if (draggeds.Contains((object) key))
        return true;
    }
    return false;
  }

  /// <summary>
  /// The dragging tool is applicable when the user can move or copy one or more objects.
  /// </summary>
  /// <returns>
  /// This predicate returns true when:
  /// <list type="bullet">
  /// <item>the user has started moving the mouse with a mouse button down</item>
  /// <item>the view allows objects to be moved or copied or dragged out of the window</item>
  /// <item>the mouse button is not the context menu button</item>
  /// <item>there is a selectable object under the mouse</item>
  /// <item>and that object can be moved or copied</item>
  /// </list>
  /// </returns>
  public override bool CanStart()
  {
    if (this.LastInput.Buttons != MouseButtons.Left)
      return false;
    Size size = new Size(4, 4);
    Point viewPoint1 = this.FirstInput.ViewPoint;
    Point viewPoint2 = this.LastInput.ViewPoint;
    return (Math.Abs(viewPoint1.X - viewPoint2.X) > size.Width / 2 || Math.Abs(viewPoint1.Y - viewPoint2.Y) > size.Height / 2) && this.View.PickObject(true, false, this.FirstInput.DocPoint, true) != null;
  }

  /// <summary>
  /// Mouse drags just call <see cref="M:Intermech.Map.MapToolDragging.DoDragging(Intermech.Map.MapInputState)" /> and <see cref="M:Intermech.Map.MapView.DoAutoScroll(System.Drawing.Point)" />.
  /// </summary>
  /// <remarks>
  /// By default this sets the <c>Effect</c> according to the
  /// values of <see cref="M:Intermech.Map.MapToolDragging.MayBeCopying" />, <see cref="M:Intermech.Map.MapToolDragging.MayBeMoving" />,
  /// <see cref="M:Intermech.Map.MapToolDragging.MustBeCopying" />, <see cref="M:Intermech.Map.MapToolDragging.MustBeMoving" />
  /// and whether all of the objects in the selection can be copied or moved.
  /// </remarks>
  public override void DoMouseMove()
  {
    this.DoDragging();
    this.View.DoAutoScroll(this.LastInput.ViewPoint);
  }

  /// <summary>
  /// Perform the drag, for both moving and copying, including the final move or copy
  /// on a mouse up event.
  /// </summary>
  public virtual void DoDragging()
  {
    if (this.CurrentObject == null)
      return;
    SizeF sizeF = AbstractTool.SubtractPoints(this.LastInput.DocPoint, this.CurrentObject.Position);
    this.View.MoveSelection(this.CurrentObject, new SizeF(sizeF.Width - this.MoveOffset.Width, sizeF.Height - this.MoveOffset.Height));
  }

  /// <summary>
  /// The release of the mouse makes a final call to <see cref="M:Intermech.Map.MapToolDragging.DoDragging(Intermech.Map.MapInputState)" /> before
  /// finishing the transaction.
  /// </summary>
  public override void DoMouseUp() => this.StopTool();

  /// <summary>Start a drag-and-drop operation.</summary>
  /// <remarks>
  /// This first remembers the <see cref="P:Intermech.Map.MapToolDragging.MoveOffset" /> between the <see cref="P:Intermech.Map.MapTool.CurrentObject" />'s
  /// position and the mouse point (the first input event point).
  /// It removes any selection handles, so those do not need to be dragged along.
  /// It also starts a transaction.
  /// If the view's <see cref="P:Intermech.Map.MapView.AllowDragOut" /> property is true, we call
  /// <c>Control.DoDragDrop</c> to start the standard modal drag-and-drop process.
  /// This depends on the cooperation of <see cref="M:Intermech.Map.MapView.OnDragOver(System.Windows.Forms.DragEventArgs)" />, <see cref="M:Intermech.Map.MapView.OnDragDrop(System.Windows.Forms.DragEventArgs)" />,
  /// and <see cref="M:Intermech.Map.MapView.OnQueryContinueDrag(System.Windows.Forms.QueryContinueDragEventArgs)" /> to call <see cref="M:Intermech.Map.MapToolDragging.DoMouseMove" />,
  /// <see cref="M:Intermech.Map.MapToolDragging.DoMouseUp" />, and <see cref="M:Intermech.Map.MapToolDragging.DoCancelMouse" /> appropriately when the
  /// drop target is the same view as the drag source.
  /// If the view's <see cref="P:Intermech.Map.MapView.AllowDragOut" /> property is false, the
  /// normal calls to <see cref="M:Intermech.Map.MapToolDragging.DoMouseMove" />, <see cref="M:Intermech.Map.MapToolDragging.DoMouseUp" />, and
  /// <see cref="M:Intermech.Map.MapToolDragging.DoCancelMouse" /> occur.
  /// </remarks>
  public override void Start()
  {
    if (this.mySelectionSet)
      return;
    this.CurrentObject = this.View.PickObject(true, false, this.FirstInput.DocPoint, true);
    if (this.CurrentObject == null)
      return;
    this.MoveOffset = AbstractTool.SubtractPoints(this.FirstInput.DocPoint, this.CurrentObject.Position);
  }

  /// <summary>Clean up after any drag.</summary>
  /// <remarks>
  /// This restores any hidden selection handles, removes any
  /// drag selection objects, and stops the current transaction.
  /// </remarks>
  public override void Stop()
  {
    this.View.StopAutoScroll();
    this.CurrentObject = (IObject) null;
    this.mySelectionSet = false;
  }

  /// <summary>
  /// Gets or sets the offset of the mouse point within the current object.
  /// </summary>
  /// <value>
  /// This <c>SizeF</c> value is in document coordinates.
  /// </value>
  /// <remarks>
  /// The mouse is normally inside the current object, which is just one
  /// of the selected objects being dragged.
  /// </remarks>
  public SizeF MoveOffset
  {
    get => this.myMoveOffset;
    set => this.myMoveOffset = value;
  }
}
