
// Type: Intermech.Redline.FullImageMapView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Map;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Redline;

/// <summary>окно для закладки ПРОСМОТР </summary>
public class FullImageMapView : RedlineView
{
  public override float LimitDocScale(float scale)
  {
    RectangleF documentBounds = this.ComputeDocumentBounds();
    float num1 = 1f;
    if ((double) documentBounds.Width > 0.0 && (double) documentBounds.Height > 0.0)
    {
      Size size = this.DisplayRectangle.Size;
      num1 = Math.Min((float) size.Width / documentBounds.Width, (float) size.Height / documentBounds.Height);
    }
    float num2 = Math.Min(0.1f * num1, this.PixelsPerMM);
    float num3 = Math.Max(10000f * num1, this.PixelsPerMM);
    if ((double) scale < (double) num2)
      return num2;
    return (double) scale > (double) num3 ? num3 : scale;
  }

  public override void Zoom1to1()
  {
    this.DocPosition = this.ComputeDocumentBounds().Location;
    this.DocScale = this.PixelsPerMM;
    this.OnViewChanging();
    this.UpdateView();
  }

  public override void RescaleToFit()
  {
    RectangleF documentBounds = this.ComputeDocumentBounds();
    if (this.VerticalScrollBar != null)
      this.VerticalScrollBar.Visible = false;
    if (this.HorizontalScrollBar != null)
      this.HorizontalScrollBar.Visible = false;
    float num = this.DocScale;
    if ((double) documentBounds.Width > 0.0 && (double) documentBounds.Height > 0.0)
    {
      Size size = this.DisplayRectangle.Size;
      num = Math.Min((float) size.Width / documentBounds.Width, (float) size.Height / documentBounds.Height);
    }
    this.DocPosition = new PointF(documentBounds.X, documentBounds.Y);
    this.DocScale = num;
  }

  public override void ZoomToScale(PointF ptdoc, float scale)
  {
    float docScale = this.DocScale;
    float num = this.LimitDocScale(docScale * scale);
    if ((double) docScale == (double) num)
      return;
    PointF docPosition = this.DocPosition;
    Point view = this.ConvertDocToView(ptdoc);
    this.OnViewChanging();
    this.DocScale = num;
    SizeF doc = this.ConvertViewToDoc(new Size(this.ConvertDocToView(ptdoc)) - new Size(view));
    this.DocPosition = docPosition + doc;
    this.OnViewChanging();
    this.UpdateView();
  }

  public override void ZoomToBox(RectangleF docBox)
  {
    this.OnViewChanging();
    if (this.VerticalScrollBar != null)
      this.VerticalScrollBar.Visible = false;
    if (this.HorizontalScrollBar != null)
      this.HorizontalScrollBar.Visible = false;
    float num = this.DocScale;
    if ((double) docBox.Width > 0.0 && (double) docBox.Height > 0.0)
    {
      Size size = this.DisplayRectangle.Size;
      if ((double) size.Width > 4.0 && (double) size.Height > 4.0)
      {
        size.Width -= 2;
        size.Height -= 2;
      }
      num = Math.Min((float) size.Width / docBox.Width, (float) size.Height / docBox.Height);
    }
    this.DocPosition = new PointF(docBox.X, docBox.Y);
    this.DocScale = num;
    this.OnViewChanging();
    this.UpdateView();
  }

  public override void UpdateScrollBars()
  {
    if (this.myUpdatingScrollBars)
      return;
    HScrollBar horizontalScrollBar = this.HorizontalScrollBar;
    VScrollBar verticalScrollBar = this.VerticalScrollBar;
    if (verticalScrollBar == null && horizontalScrollBar == null)
      return;
    Size size = this.Size;
    size.Width = Math.Max(0, size.Width - 2 * this.myBorderSize.Width);
    size.Height = Math.Max(0, size.Height - 2 * this.myBorderSize.Height);
    RectangleF rectangleF = new RectangleF(this.DocumentTopLeft, this.Document.Size);
    Rectangle rectangle = new Rectangle()
    {
      X = (int) Math.Floor((double) rectangleF.Left * (double) this.myHorizScale),
      Y = (int) Math.Floor((double) rectangleF.Top * (double) this.myVertScale)
    };
    rectangle.Width = (int) Math.Ceiling((double) rectangleF.Right * (double) this.myHorizScale) - rectangle.Left;
    rectangle.Height = (int) Math.Ceiling((double) rectangleF.Bottom * (double) this.myVertScale) - rectangle.Top;
    PointF docPosition = this.DocPosition;
    Point point = new Point();
    point.X = (int) Math.Floor((double) docPosition.X * (double) this.myHorizScale);
    point.Y = (int) Math.Floor((double) docPosition.Y * (double) this.myVertScale);
    bool flag1 = rectangle.Height > size.Height + 1 || point.Y > rectangle.Top || point.Y + size.Height + 1 < rectangle.Bottom;
    bool flag2 = verticalScrollBar != null && (this.ShowVerticalScrollBar == MapViewScrollBarVisibility.Show || this.ShowVerticalScrollBar == MapViewScrollBarVisibility.IfNeeded & flag1);
    bool flag3 = rectangle.Width > size.Width + 1 || point.X > rectangle.Left || point.X + (size.Width + 1) < rectangle.Right;
    bool flag4 = horizontalScrollBar != null && (this.ShowHorizontalScrollBar == MapViewScrollBarVisibility.Show || this.ShowHorizontalScrollBar == MapViewScrollBarVisibility.IfNeeded & flag3);
    if (flag2)
      size.Width = Math.Max(0, size.Width - this.myScrollBarWidth);
    if (flag4)
      size.Height = Math.Max(0, size.Height - this.myScrollBarHeight);
    bool flag5 = rectangle.Height > size.Height + 1 || point.Y > rectangle.Top || point.Y + size.Height + 1 < rectangle.Bottom;
    bool flag6 = verticalScrollBar != null && (this.ShowVerticalScrollBar == MapViewScrollBarVisibility.Show || this.ShowVerticalScrollBar == MapViewScrollBarVisibility.IfNeeded & flag5);
    bool flag7 = rectangle.Width > size.Width + 1 || point.X > rectangle.Left || point.X + size.Width + 1 < rectangle.Right;
    bool flag8 = horizontalScrollBar != null && (this.ShowHorizontalScrollBar == MapViewScrollBarVisibility.Show || this.ShowHorizontalScrollBar == MapViewScrollBarVisibility.IfNeeded & flag7);
    this.myUpdatingScrollBars = true;
    bool flag9 = false;
    if (verticalScrollBar != null)
    {
      int num1 = rectangle.Bottom - size.Height;
      if (point.Y > num1 && num1 > rectangle.Top)
        point.Y = num1;
      else if (point.Y < rectangle.Top)
        point.Y = rectangle.Top;
      int num2 = Math.Max(Math.Max(rectangle.Bottom, point.Y + size.Height) - 12, point.Y);
      if (verticalScrollBar.Minimum != rectangle.Top)
        verticalScrollBar.Minimum = rectangle.Top;
      if (verticalScrollBar.Maximum != num2)
        verticalScrollBar.Maximum = num2;
      if (verticalScrollBar.Value != point.Y)
        verticalScrollBar.Value = point.Y;
      if (verticalScrollBar.Visible != flag6)
        flag9 = true;
      verticalScrollBar.Visible = flag6;
      verticalScrollBar.Enabled = flag5;
    }
    if (horizontalScrollBar != null)
    {
      int num3 = rectangle.Right - size.Width;
      if (point.X > num3 && num3 > rectangle.Left)
        point.X = num3;
      else if (point.X < rectangle.Left)
        point.X = rectangle.Left;
      int num4 = Math.Max(Math.Max(rectangle.Right, point.X + size.Width) - 12, point.X);
      if (horizontalScrollBar.Minimum != rectangle.Left)
        horizontalScrollBar.Minimum = rectangle.Left;
      if (horizontalScrollBar.Maximum != num4)
        horizontalScrollBar.Maximum = num4;
      if (horizontalScrollBar.Value != point.X)
        horizontalScrollBar.Value = point.X;
      if (horizontalScrollBar.Visible != flag8)
        flag9 = true;
      horizontalScrollBar.Visible = flag8;
      horizontalScrollBar.Enabled = flag7;
    }
    this.myUpdatingScrollBars = false;
    if (!flag9)
      return;
    this.LayoutScrollBars(false);
  }
}
