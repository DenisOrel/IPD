
// Type: Intermech.DocumentView.ToolZooming
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.DocumentView;

/// <summary>
/// This tool handles a user's drag in the background to draw a rubber-band box
/// to specify a new document position and scale for a view.
/// </summary>
[Serializable]
public class ToolZooming : ToolRubberBanding
{
  [NonSerialized]
  private IView _zoomedView;

  /// <summary>The standard tool constructor.</summary>
  /// <param name="view"></param>
  public ToolZooming(IView view)
    : base(view)
  {
    this._zoomedView = view;
  }

  /// <summary>
  /// Allow this tool to start if the user isn't using the context button
  /// and if the mouse isn't over an object in the document.
  /// </summary>
  /// <returns></returns>
  public override bool CanStart()
  {
    return this.LastInput.Buttons == MouseButtons.Left && this.View.PickObject(true, false, this.FirstInput.DocPoint, true) == null;
  }

  /// <summary>
  /// Make the box keep the aspect ratio of the observed view.
  /// </summary>
  /// <returns></returns>
  public override Rectangle ComputeRubberBandBox()
  {
    Point viewPoint1 = this.FirstInput.ViewPoint;
    Point viewPoint2 = this.LastInput.ViewPoint;
    int num1 = viewPoint2.X - viewPoint1.X;
    int num2 = viewPoint2.Y - viewPoint1.Y;
    IView zoomedView = this.ZoomedView;
    if (zoomedView == null || zoomedView.DisplayRectangle.Height == 0 || num2 == 0)
      return new Rectangle(Math.Min(viewPoint2.X, viewPoint1.X), Math.Min(viewPoint2.Y, viewPoint1.Y), Math.Abs(viewPoint2.X - viewPoint1.X), Math.Abs(viewPoint2.Y - viewPoint1.Y));
    Rectangle displayRectangle = zoomedView.DisplayRectangle;
    float num3 = (float) displayRectangle.Width / (float) displayRectangle.Height;
    int val1_1;
    int val1_2;
    if ((double) Math.Abs((float) num1 / (float) num2) < (double) num3)
    {
      val1_1 = viewPoint1.X + num1;
      val1_2 = viewPoint1.Y + (int) Math.Ceiling((double) Math.Abs(num1) / (double) num3) * (num2 < 0 ? -1 : 1);
    }
    else
    {
      val1_1 = viewPoint1.X + (int) Math.Ceiling((double) Math.Abs(num2) * (double) num3) * (num1 < 0 ? -1 : 1);
      val1_2 = viewPoint1.Y + num2;
    }
    return new Rectangle(Math.Min(val1_1, viewPoint1.X), Math.Min(val1_2, viewPoint1.Y), Math.Abs(val1_1 - viewPoint1.X), Math.Abs(val1_2 - viewPoint1.Y));
  }

  /// <summary>
  /// Instead of selecting objects within a rectangle, change the <see cref="P:Intermech.Map.MapToolZooming.ZoomedView" />'s
  /// <see cref="P:Intermech.Map.MapView.DocPosition" /> and <see cref="P:Intermech.Map.MapView.DocScale" /> to match the
  /// given <paramref name="box" /> within this view.
  /// </summary>
  /// <param name="box">a rectangle whose aspect ratio matches the
  /// <see cref="P:Intermech.Map.MapToolZooming.ZoomedView" />'s, and whose width and height are at least 4.</param>
  public override void DoRubberBand(Rectangle box)
  {
    if (box.Width < 4 || box.Height < 4)
      return;
    IView zoomedView = this.ZoomedView;
    if (zoomedView == null)
      return;
    RectangleF doc = this.View.ConvertViewToDoc(box);
    Rectangle displayRectangle = zoomedView.DisplayRectangle;
    zoomedView.DocScale = (float) displayRectangle.Width / doc.Width;
    zoomedView.DocPosition = new PointF(doc.X, doc.Y);
  }

  /// <summary>
  /// Gets the view whose aspect ratio we want to maintain when drawing a zoom region,
  /// and whose document position and scale will be adjusted on a mouse up.
  /// </summary>
  /// <value>
  /// The initial value is the same as <see cref="P:Intermech.Map.MapTool.View" />.
  /// </value>
  public IView ZoomedView
  {
    get => this._zoomedView;
    set => this._zoomedView = value;
  }
}
