
// Type: Intermech.Redline.MapToolZooming
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Map;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Redline;

public class MapToolZooming : MapToolRubberBanding
{
  [NonSerialized]
  private MapView _zoomedView;

  public MapToolZooming(MapView view)
    : base(view)
  {
    this._zoomedView = view;
  }

  public override bool CanStart()
  {
    return this.LastInput.Buttons == MouseButtons.Left && this.View.PickObject(true, false, this.FirstInput.DocPoint, true) == null;
  }

  public override Rectangle ComputeRubberBandBox()
  {
    Point viewPoint1 = this.FirstInput.ViewPoint;
    Point viewPoint2 = this.LastInput.ViewPoint;
    if (viewPoint2.X < 0)
      viewPoint2.X = 0;
    if (viewPoint2.X >= this._zoomedView.Width)
      viewPoint2.X = this._zoomedView.Width - 1;
    if (viewPoint2.Y < 0)
      viewPoint2.Y = 0;
    if (viewPoint2.Y >= this._zoomedView.Height)
      viewPoint2.Y = this._zoomedView.Height - 1;
    int num1 = viewPoint2.X - viewPoint1.X;
    int num2 = viewPoint2.Y - viewPoint1.Y;
    return new Rectangle(Math.Min(viewPoint2.X, viewPoint1.X), Math.Min(viewPoint2.Y, viewPoint1.Y), Math.Abs(num1), Math.Abs(num2));
  }

  public override void DoRubberBand(Rectangle box)
  {
    if (box.Width < 4 || box.Height < 4)
      return;
    MapView zoomedView = this.ZoomedView;
    if (zoomedView == null)
      return;
    RectangleF doc1 = this.View.ConvertViewToDoc(box);
    Size size = zoomedView.DisplayRectangle.Size;
    PointF pointF = new PointF((float) (((double) doc1.Left + (double) doc1.Right) / 2.0), (float) (((double) doc1.Top + (double) doc1.Bottom) / 2.0));
    float num = Math.Min((float) size.Width / (float) box.Width, (float) size.Height / (float) box.Height);
    zoomedView.DocScale *= num;
    SizeF doc2 = zoomedView.ConvertViewToDoc(size);
    zoomedView.DocPosition = new PointF(pointF.X - doc2.Width / 2f, pointF.Y - doc2.Height / 2f);
    zoomedView.OnViewChanging();
  }

  public MapView ZoomedView
  {
    get => this._zoomedView;
    set => this._zoomedView = value;
  }
}
