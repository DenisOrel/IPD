using System;
using System.Drawing;


namespace Intermech.Map
{
    [Serializable]
    public class MapToolZooming : MapToolRubberBanding
    {
      [NonSerialized]
      private MapView myZoomedView;

      public MapToolZooming(MapView v)
        : base(v)
      {
        this.myZoomedView = v;
      }

      public override bool CanStart()
      {
        return !this.FirstInput.IsContextButton && this.View.PickObject(true, false, this.FirstInput.DocPoint, true) == null;
      }

      public override Rectangle ComputeRubberBandBox()
      {
        Point viewPoint1 = this.FirstInput.ViewPoint;
        Point viewPoint2 = this.LastInput.ViewPoint;
        int num1 = viewPoint2.X - viewPoint1.X;
        int num2 = viewPoint2.Y - viewPoint1.Y;
        MapView zoomedView = this.ZoomedView;
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

      public override void DoRubberBand(Rectangle box)
      {
        if (box.Width < 4 || box.Height < 4)
          return;
        MapView zoomedView = this.ZoomedView;
        if (zoomedView == null)
          return;
        RectangleF doc = this.View.ConvertViewToDoc(box);
        Size size = zoomedView.DisplayRectangle.Size;
        zoomedView.DocScale = (float) size.Width / doc.Width;
        zoomedView.DocPosition = new PointF(doc.X, doc.Y);
      }

      public MapView ZoomedView
      {
        get => this.myZoomedView;
        set => this.myZoomedView = value;
      }
    }
}
