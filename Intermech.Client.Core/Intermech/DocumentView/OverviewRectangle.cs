
// Type: Intermech.DocumentView.OverviewRectangle
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing;


namespace Intermech.DocumentView;

/// <summary>
/// This class implements the rectangle shown and dragged around in the overview window.
/// It is also responsible for keeping track of changes to the view.
/// </summary>
[Serializable]
public class OverviewRectangle : IObject
{
  [NonSerialized]
  private bool myChanging;

  /// <summary>
  /// Create a <see cref="T:Intermech.Map.MapRectangle" /> that knows about the view that it represents.
  /// </summary>
  /// <remarks>The overview rectangle is not selectable.</remarks>
  public OverviewRectangle()
  {
    this.myChanging = false;
    this.Selectable = false;
    this.Resizable = false;
  }

  /// <summary>Paint a possibly shadowed rectangle.</summary>
  /// <param name="g"></param>
  /// <param name="iview"></param>
  public override void Paint(Graphics g, IView iview)
  {
    RectangleF bounds = this.Bounds;
    g.DrawRectangle(SystemPens.ControlText, bounds.X, bounds.Y, bounds.Width, bounds.Height);
  }

  /// <summary>
  /// Limit where this rectangle can be dragged, to avoid misleading the user
  /// into believing they could scroll even futher.
  /// </summary>
  /// <param name="origLoc"></param>
  /// <param name="newLoc"></param>
  /// <returns></returns>
  public override PointF ComputeMove(PointF origLoc, PointF newLoc)
  {
    if (this.ObservedView != null)
    {
      PointF documentTopLeft = this.ObservedView.DocumentTopLeft;
      SizeF documentSize = this.ObservedView.DocumentSize;
      if ((double) newLoc.X + (double) this.Width > (double) documentTopLeft.X + (double) documentSize.Width)
        newLoc.X = documentTopLeft.X + documentSize.Width - this.Width;
      if ((double) newLoc.X < (double) documentTopLeft.X)
        newLoc.X = documentTopLeft.X;
      if ((double) newLoc.Y + (double) this.Height > (double) documentTopLeft.Y + (double) documentSize.Height)
        newLoc.Y = documentTopLeft.Y + documentSize.Height - this.Height;
      if ((double) newLoc.Y < (double) documentTopLeft.Y)
        newLoc.Y = documentTopLeft.Y;
      if (this.ObservedView.ShowsNegativeCoordinates)
        return newLoc;
      if ((double) newLoc.X < 0.0)
        newLoc.X = 0.0f;
      if ((double) newLoc.Y < 0.0)
        newLoc.Y = 0.0f;
    }
    return newLoc;
  }

  /// <summary>
  /// Treat this rectangle as being hollow--the user can only pick the rectangle when close to the edge.
  /// </summary>
  /// <param name="p"></param>
  /// <returns></returns>
  public override bool ContainsPoint(PointF p)
  {
    RectangleF bounds = this.Bounds;
    float num = 4f / this.View.DocScale;
    IObject.InflateRect(ref bounds, num, num);
    if (!IObject.ContainsRect(bounds, p))
      return false;
    IObject.InflateRect(ref bounds, -2f * num, -2f * num);
    return !IObject.ContainsRect(bounds, p);
  }

  /// <summary>
  /// As the user drags this rectangle around, change the observed view's
  /// DocPosition property.
  /// </summary>
  /// <param name="old"></param>
  /// <remarks>
  /// This basically just does <c>ObservedView.DocPosition = Position</c>,
  /// although it ignores changes caused by a change in the observed view.
  /// </remarks>
  protected override void OnBoundsChanged(RectangleF old)
  {
    base.OnBoundsChanged(old);
    if (this.ObservedView == null || this.myChanging)
      return;
    this.myChanging = true;
    this.AsyncUpdateObserved();
    this.myChanging = false;
  }

  protected void AsyncUpdateObserved() => this.ObservedView.DocPosition = this.Position;

  /// <summary>The overview rectangle should not get selected.</summary>
  /// <param name="sel"></param>
  public override void OnGotSelection(ISelection sel)
  {
  }

  /// <summary>
  /// Make this GoRectangle's position and size correspond to the
  /// observed view's position and size in the document
  /// </summary>
  /// <remarks>
  /// This method also scrolls this overview window, if needed,
  /// to make the rectangle visible.
  /// </remarks>
  public void UpdateRectFromView()
  {
    if (this.ObservedView == null || this.myChanging)
      return;
    this.myChanging = true;
    this.Bounds = this.ObservedView.DocExtent;
    if (this.View != null)
      this.View.ScrollRectangleToVisible(this.Bounds);
    this.myChanging = false;
  }

  /// <summary>
  /// Gets the view whose bounds this rectangle is representing in the overview.
  /// </summary>
  public IView ObservedView => this.View is Overview view ? view.Observed : (IView) null;

  protected delegate void AsyncUpdate();
}
