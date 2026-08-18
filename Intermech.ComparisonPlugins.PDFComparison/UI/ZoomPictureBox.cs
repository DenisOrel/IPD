// Decompiled with JetBrains decompiler
// Type: Intermech.ComparisonPlugins.PDFComparison.UI.ZoomPictureBox
// Assembly: Intermech.ComparisonPlugins.PDFComparison, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A8B4ECC9-43EB-48A8-B8E5-C6978FF09846
// Assembly location: D:\IPS\Client\Intermech.ComparisonPlugins.PDFComparison.dll

using Intermech.ComparisonPlugins.PDFComparison.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ComparisonPlugins.PDFComparison.UI;

public class ZoomPictureBox : UserControl
{
  private Image _image;
  private PointF _visibleCenter;
  private float _zoom = 1f;
  private ZoomPictureBox.MouseState _mouseState;
  private Point _startDragged;
  private PointF _startDraggedVisibleCenter;
  private int _sourceImageWidth;
  private int _sourceImageHeight;
  private IContainer components;
  private Button buttonZoomReset;
  private Button buttonZoomOut;
  private Button buttonZoomIn;
  private ToolTip toolTip1;

  [DefaultValue(0.2f)]
  public float ZoomDelta { get; set; }

  [DefaultValue(true)]
  public bool AllowUserDrag { get; set; }

  [DefaultValue(true)]
  public bool AllowUserZoom { get; set; }

  public InterpolationMode InterpolationMode { get; set; }

  public InterpolationMode InterpolationModeZoomOut { get; set; }

  public PixelOffsetMode PixelOffsetMode { get; set; }

  public ZoomPictureBox()
  {
    this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    this.ZoomDelta = 0.2f;
    this.AllowUserDrag = true;
    this.AllowUserZoom = true;
    this.InterpolationMode = InterpolationMode.Bicubic;
    this.InterpolationModeZoomOut = InterpolationMode.Bilinear;
    this.PixelOffsetMode = PixelOffsetMode.HighQuality;
    this.InitializeComponent();
  }

  [DefaultValue(null)]
  public Image Image
  {
    get => this._image;
    set
    {
      this._image = value;
      if (value == null)
      {
        this._sourceImageWidth = 0;
        this._sourceImageHeight = 0;
        this.VisibleCenter = new PointF(0.0f, 0.0f);
      }
      else
      {
        this._sourceImageWidth = value.Width;
        this._sourceImageHeight = value.Height;
        this.ResetView();
      }
      this.Invalidate();
    }
  }

  public void UpdateImage(Image image)
  {
    this._image = image;
    this._sourceImageWidth = image.Width;
    this._sourceImageHeight = image.Height;
    this.Invalidate();
  }

  public void ResetView()
  {
    float num1 = (float) this.ClientSize.Width / (float) this._sourceImageWidth;
    float num2 = (float) this.ClientSize.Height / (float) this._sourceImageHeight;
    this.Zoom = (double) num1 < (double) num2 ? num1 : num2;
    this.VisibleCenter = new PointF((float) this._sourceImageWidth / 2f, (float) this._sourceImageHeight / 2f);
  }

  [DefaultValue(1f)]
  public float Zoom
  {
    get => this._zoom;
    set
    {
      this._zoom = (double) Math.Abs(value) > 1.4012984643248171E-45 ? value : throw new Exception("Zoom must be more then 0");
    }
  }

  public PointF VisibleCenter
  {
    get => this._visibleCenter;
    set
    {
      this._visibleCenter = value;
      this.Invalidate();
    }
  }

  protected override void OnMouseWheel(MouseEventArgs e)
  {
    base.OnMouseWheel(e);
    if (!this.AllowUserZoom)
      return;
    Point imagePoint1 = this.ClientToImagePoint(e.Location);
    if (e.Delta > 0)
      this.increaseZoom();
    if (e.Delta < 0)
      this.decreaseZoom();
    Point imagePoint2 = this.ClientToImagePoint(e.Location);
    int num1 = imagePoint2.X - imagePoint1.X;
    int num2 = imagePoint2.Y - imagePoint1.Y;
    PointF visibleCenter = this.VisibleCenter;
    double x = (double) visibleCenter.X - (double) num1;
    visibleCenter = this.VisibleCenter;
    double y = (double) visibleCenter.Y - (double) num2;
    this.VisibleCenter = new PointF((float) x, (float) y);
  }

  protected override void OnMouseDown(MouseEventArgs e)
  {
    base.OnMouseDown(e);
    if (this.AllowUserDrag && e.Button == MouseButtons.Left)
    {
      this.Cursor = Cursors.SizeAll;
      this._mouseState = ZoomPictureBox.MouseState.Drag;
    }
    this._startDragged = e.Location;
    this._startDraggedVisibleCenter = this.VisibleCenter;
  }

  protected override void OnMouseUp(MouseEventArgs e)
  {
    base.OnMouseUp(e);
    this.Cursor = Cursors.Default;
    this._mouseState = ZoomPictureBox.MouseState.None;
  }

  protected override void OnMouseMove(MouseEventArgs e)
  {
    base.OnMouseMove(e);
    if (this._mouseState != ZoomPictureBox.MouseState.Drag)
      return;
    this.VisibleCenter = new PointF(this._startDraggedVisibleCenter.X - (float) (e.Location.X - this._startDragged.X) / this._zoom, this._startDraggedVisibleCenter.Y - (float) (e.Location.Y - this._startDragged.Y) / this._zoom);
  }

  private void decreaseZoom()
  {
    this.Zoom = (float) Math.Exp(Math.Log((double) this._zoom) - (double) this.ZoomDelta);
  }

  private void increaseZoom()
  {
    this.Zoom = (float) Math.Exp(Math.Log((double) this._zoom) + (double) this.ZoomDelta);
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    if (this._image == null)
      return;
    e.Graphics.ResetTransform();
    e.Graphics.InterpolationMode = (double) this.Zoom < 1.0 ? this.InterpolationModeZoomOut : this.InterpolationMode;
    e.Graphics.PixelOffsetMode = this.PixelOffsetMode;
    if (this._mouseState == ZoomPictureBox.MouseState.Drag)
      e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
    Point client = this.ImagePointToClient(Point.Empty);
    e.Graphics.Clear(Color.Gray);
    int width = this._image.Width;
    int height = this._image.Height;
    e.Graphics.DrawImage(this._image, (float) client.X, (float) client.Y, (float) width * this.Zoom, (float) height * this.Zoom);
    base.OnPaint(e);
  }

  public Point ClientToImagePoint(Point point)
  {
    return Point.Round(this.ClientToImagePoint((PointF) point));
  }

  public Point ImagePointToClient(Point point)
  {
    return Point.Round(this.ImagePointToClient((PointF) point));
  }

  public PointF ClientToImagePoint(PointF point)
  {
    return new PointF((point.X - (float) this.ClientSize.Width / 2f) / this.Zoom + this._visibleCenter.X, (point.Y - (float) this.ClientSize.Height / 2f) / this.Zoom + this._visibleCenter.Y);
  }

  public PointF ImagePointToClient(PointF point)
  {
    return new PointF((float) (((double) point.X - (double) this._visibleCenter.X) * (double) this.Zoom + (double) this.ClientSize.Width / 2.0), (float) (((double) point.Y - (double) this._visibleCenter.Y) * (double) this.Zoom + (double) this.ClientSize.Height / 2.0));
  }

  public Image GetScreenshot()
  {
    Size clientSize = this.ClientSize;
    int width = clientSize.Width;
    clientSize = this.ClientSize;
    int height = clientSize.Height;
    Image image = (Image) new Bitmap(width, height);
    using (Graphics graphics = Graphics.FromImage(image))
      this.OnPaint(new PaintEventArgs(graphics, this.ClientRectangle));
    return image;
  }

  private void buttonZoomIn_Click(object sender, EventArgs e)
  {
    this.increaseZoom();
    this.Invalidate();
  }

  private void buttonZoomOut_Click(object sender, EventArgs e)
  {
    this.decreaseZoom();
    this.Invalidate();
  }

  private void buttonZoomReset_Click(object sender, EventArgs e) => this.ResetView();

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.buttonZoomReset = new Button();
    this.buttonZoomOut = new Button();
    this.buttonZoomIn = new Button();
    this.toolTip1 = new ToolTip(this.components);
    this.SuspendLayout();
    this.buttonZoomReset.BackColor = SystemColors.Control;
    this.buttonZoomReset.BackgroundImage = (Image) Resources.reset;
    this.buttonZoomReset.BackgroundImageLayout = ImageLayout.Zoom;
    this.buttonZoomReset.Location = new Point(82, 4);
    this.buttonZoomReset.Name = "buttonZoomReset";
    this.buttonZoomReset.Size = new Size(33, 33);
    this.buttonZoomReset.TabIndex = 6;
    this.toolTip1.SetToolTip((Control) this.buttonZoomReset, "Сбросить масштаб и положение");
    this.buttonZoomReset.UseVisualStyleBackColor = false;
    this.buttonZoomReset.Click += new EventHandler(this.buttonZoomReset_Click);
    this.buttonZoomOut.BackColor = SystemColors.Control;
    this.buttonZoomOut.BackgroundImage = (Image) Resources.negative;
    this.buttonZoomOut.BackgroundImageLayout = ImageLayout.Zoom;
    this.buttonZoomOut.Location = new Point(43, 4);
    this.buttonZoomOut.Name = "buttonZoomOut";
    this.buttonZoomOut.Size = new Size(33, 33);
    this.buttonZoomOut.TabIndex = 7;
    this.toolTip1.SetToolTip((Control) this.buttonZoomOut, "Уменьшить масштаб");
    this.buttonZoomOut.UseVisualStyleBackColor = false;
    this.buttonZoomOut.Click += new EventHandler(this.buttonZoomOut_Click);
    this.buttonZoomIn.BackColor = SystemColors.Control;
    this.buttonZoomIn.BackgroundImage = (Image) Resources.positive;
    this.buttonZoomIn.BackgroundImageLayout = ImageLayout.Zoom;
    this.buttonZoomIn.Location = new Point(4, 4);
    this.buttonZoomIn.Name = "buttonZoomIn";
    this.buttonZoomIn.Size = new Size(33, 33);
    this.buttonZoomIn.TabIndex = 8;
    this.toolTip1.SetToolTip((Control) this.buttonZoomIn, "Увеличить масштаб");
    this.buttonZoomIn.UseVisualStyleBackColor = false;
    this.buttonZoomIn.Click += new EventHandler(this.buttonZoomIn_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = Color.Gray;
    this.Controls.Add((Control) this.buttonZoomReset);
    this.Controls.Add((Control) this.buttonZoomOut);
    this.Controls.Add((Control) this.buttonZoomIn);
    this.Name = nameof (ZoomPictureBox);
    this.ResumeLayout(false);
  }

  private enum MouseState
  {
    None,
    Drag,
  }
}
