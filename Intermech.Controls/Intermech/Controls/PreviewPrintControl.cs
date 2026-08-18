
// Type: Intermech.Controls.PreviewPrintControl
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Windows.Forms;


namespace Intermech.Controls;

/// <summary>
/// Контрол был перенесен Осипенко А. из сборки Intermech.Document.Model 13.05.10
/// </summary>
public class PreviewPrintControl : ScrollableControl
{
  private const int _border = 10;
  private const double _defaultZoom = 0.3;
  private static readonly object EVENT_STARTPAGECHANGED = new object();
  private PrintDocument _document;
  private ExtendedPreviewPageInfo[] _pageInfo;
  private VScrollBar _vScrollBar1;
  private Point _lastOffset;
  private Point _position = new Point(0, 0);
  private Point _screendpi = Point.Empty;
  private Size _imageSize = Size.Empty;
  private Size _virtualSize = new Size(1, 1);
  private int _columns = 1;
  private int _rows = 1;
  private int _startPage;
  private double _zoom = 0.3;
  private bool _antiAlias;
  private bool _autoZoom = true;
  private bool _exceptionPrinting;
  private bool _layoutOk;
  private bool _pageInfoCalcPending;
  private bool _showOnlyPrintableArea = true;
  public Rectangle[] PageRectangles;

  /// <summary>Конструктор.</summary>
  public PreviewPrintControl()
  {
    this.ResetBackColor();
    this.ResetForeColor();
    this.Size = new Size(100, 100);
    this.SetStyle(ControlStyles.ResizeRedraw, false);
    this.SetStyle(ControlStyles.Opaque | ControlStyles.OptimizedDoubleBuffer, true);
    this.AutoScroll = true;
  }

  /// <summary>
  /// 
  /// </summary>
  public bool AutoZoom
  {
    get => this._autoZoom;
    set
    {
      if (this._autoZoom == value)
        return;
      this._autoZoom = value;
      this.InvalidateLayout();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public int Columns
  {
    get => this._columns;
    set
    {
      if (value < 1)
      {
        object[] objArray = new object[3]
        {
          (object) nameof (Columns),
          (object) value.ToString((IFormatProvider) CultureInfo.CurrentCulture),
          (object) 1.ToString((IFormatProvider) CultureInfo.CurrentCulture)
        };
      }
      this._columns = value;
      this.InvalidateLayout();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public PrintDocument Document
  {
    get => this._document;
    set => this._document = value;
  }

  /// <summary>
  /// 
  /// </summary>
  private Point Position
  {
    get => this._position;
    set => this.SetPositionNoInvalidate(value);
  }

  /// <summary>
  /// 
  /// </summary>
  public override RightToLeft RightToLeft
  {
    get => base.RightToLeft;
    set
    {
      base.RightToLeft = value;
      this.InvalidatePreview();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public int Rows
  {
    get => this._rows;
    set
    {
      if (value < 1)
      {
        object[] objArray = new object[3]
        {
          (object) nameof (Rows),
          (object) value.ToString((IFormatProvider) CultureInfo.CurrentCulture),
          (object) 1.ToString((IFormatProvider) CultureInfo.CurrentCulture)
        };
      }
      this._rows = value;
      this.InvalidateLayout();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public int StartPage
  {
    get
    {
      int val1 = this._startPage;
      if (this._pageInfo != null)
        val1 = Math.Min(val1, this._pageInfo.Length - this._rows * this._columns);
      return Math.Max(val1, 0);
    }
    set
    {
      if (value < 0)
      {
        object[] objArray = new object[3]
        {
          (object) nameof (StartPage),
          (object) value.ToString((IFormatProvider) CultureInfo.CurrentCulture),
          (object) 0.ToString((IFormatProvider) CultureInfo.CurrentCulture)
        };
      }
      int startPage1 = this.StartPage;
      this._startPage = value;
      int startPage2 = this._startPage;
      if (startPage1 == startPage2)
        return;
      this.InvalidateLayout();
      this.OnStartPageChanged(EventArgs.Empty);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Bindable(false)]
  public override string Text
  {
    get => base.Text;
    set => base.Text = value;
  }

  /// <summary>
  /// 
  /// </summary>
  public bool UseAntiAlias
  {
    get => this._antiAlias;
    set => this._antiAlias = value;
  }

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  [EditorBrowsable(EditorBrowsableState.Never)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  private Size VirtualSize
  {
    get => this._virtualSize;
    set
    {
      this.SetVirtualSizeNoInvalidate(value);
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue(0.3)]
  public double Zoom
  {
    get => this._zoom;
    set
    {
      if (value <= 0.0)
        throw new ArgumentException("PrintPreviewControlZoomNegative");
      this._autoZoom = false;
      this._zoom = value;
      this.InvalidateLayout();
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(true)]
  public bool ShowOnlyPrintableArea
  {
    get => this._showOnlyPrintableArea;
    set
    {
      if (this._showOnlyPrintableArea == value)
        return;
      this._showOnlyPrintableArea = value;
      if (this.Document == null)
        return;
      this.InvalidatePreview();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public event EventHandler StartPageChanged
  {
    add => this.Events.AddHandler(PreviewPrintControl.EVENT_STARTPAGECHANGED, (Delegate) value);
    remove
    {
      this.Events.RemoveHandler(PreviewPrintControl.EVENT_STARTPAGECHANGED, (Delegate) value);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  [Browsable(false)]
  public new event EventHandler TextChanged
  {
    add => base.TextChanged += value;
    remove => base.TextChanged -= value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="value"></param>
  internal void SetVirtualSizeNoInvalidate(Size value)
  {
    this._virtualSize = value;
    this.AutoScrollMinSize = value;
    if (this._virtualSize.Height > this.Size.Height)
      this._virtualSize.Height += 17;
    if (this._virtualSize.Width > this.Size.Width)
      this._virtualSize.Width += 17;
    this.SetPositionNoInvalidate(this._position);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="pevent"></param>
  protected override void OnPaint(PaintEventArgs pevent)
  {
    using (Brush brush1 = (Brush) new SolidBrush(this.BackColor))
    {
      if (this._pageInfo == null || this._pageInfo.Length == 0)
      {
        pevent.Graphics.FillRectangle(brush1, this.ClientRectangle);
        if (this._pageInfo != null || this._exceptionPrinting)
        {
          StringFormat stringFormat = new StringFormat();
          SolidBrush solidBrush = new SolidBrush(this.ForeColor);
          try
          {
          }
          finally
          {
            solidBrush.Dispose();
            stringFormat.Dispose();
          }
        }
        else
          this.BeginInvoke((Delegate) new MethodInvoker(this.CalculatePageInfo));
      }
      else
      {
        if (!this._layoutOk)
          this.ComputeLayout();
        Size size = new Size(PreviewPrintControl.PixelsToPhysical(new Point(this.ClientRectangle.Size), this._screendpi));
        Point point1 = new Point(this.VirtualSize);
        Point point2 = new Point(Math.Max(0, (this.ClientRectangle.Width - point1.X) / 2), Math.Max(0, (this.ClientRectangle.Height - point1.Y) / 2));
        ref Point local1 = ref point2;
        int x1 = local1.X;
        Point position = this.Position;
        int x2 = position.X;
        local1.X = x1 - x2;
        ref Point local2 = ref point2;
        int y1 = local2.Y;
        position = this.Position;
        int y2 = position.Y;
        local2.Y = y1 - y2;
        this._lastOffset = point2;
        int pixels1 = PreviewPrintControl.PhysicalToPixels(10, this._screendpi.X);
        int pixels2 = PreviewPrintControl.PhysicalToPixels(10, this._screendpi.Y);
        Region clip = pevent.Graphics.Clip;
        this.PageRectangles = new Rectangle[this._rows * this._columns];
        Point empty = Point.Empty;
        int val1 = 0;
        try
        {
          for (int index1 = 0; index1 < this._rows; ++index1)
          {
            empty.X = 0;
            empty.Y = val1 * index1;
            for (int index2 = 0; index2 < this._columns; ++index2)
            {
              int index3 = this.StartPage + index2 + index1 * this._columns;
              if (index3 < this._pageInfo.Length)
              {
                Size physicalSize = this._pageInfo[index3].PhysicalSize;
                if (this._autoZoom)
                  this._zoom = Math.Min((double) (size.Width - 10 * (this._columns + 1)) / (double) (this._columns * physicalSize.Width), (double) (size.Height - 10 * (this._rows + 1)) / (double) (this._rows * physicalSize.Height));
                this._imageSize = new Size((int) (this._zoom * (double) physicalSize.Width), (int) (this._zoom * (double) physicalSize.Height));
                Point pixels3 = PreviewPrintControl.PhysicalToPixels(new Point(this._imageSize), this._screendpi);
                int x3 = point2.X + pixels1 * (index2 + 1) + empty.X;
                int y3 = point2.Y + pixels2 * (index1 + 1) + empty.Y;
                empty.X += pixels3.X;
                val1 = Math.Max(val1, pixels3.Y);
                this.PageRectangles[index3 - this.StartPage] = new Rectangle(x3, y3, pixels3.X, pixels3.Y);
                pevent.Graphics.ExcludeClip(this.PageRectangles[index3 - this.StartPage]);
              }
            }
          }
          pevent.Graphics.FillRectangle(brush1, this.ClientRectangle);
        }
        finally
        {
          pevent.Graphics.Clip = clip;
        }
        for (int index = 0; index < this.PageRectangles.Length; ++index)
        {
          if (index + this.StartPage < this._pageInfo.Length)
          {
            Rectangle pageRectangle = this.PageRectangles[index];
            using (Brush brush2 = (Brush) new SolidBrush(this.ForeColor))
              pevent.Graphics.FillRectangle(brush2, pageRectangle);
            this.DrawPageBorder(pevent.Graphics, pageRectangle);
            if (!this.ShowOnlyPrintableArea)
            {
              pageRectangle.X += (int) ((double) this._pageInfo[index + this.StartPage].PrintableRect.Location.X * this.Zoom);
              pageRectangle.Y += (int) ((double) this._pageInfo[index + this.StartPage].PrintableRect.Location.Y * this.Zoom);
            }
            if (this._pageInfo[index + this.StartPage].Image != null)
              pevent.Graphics.DrawImage(this._pageInfo[index + this.StartPage].Image, pageRectangle);
            this.DrawPageBorder2(pevent.Graphics, pageRectangle);
          }
        }
      }
    }
    base.OnPaint(pevent);
  }

  protected virtual void DrawPageBorder(Graphics g, Rectangle rect)
  {
    g.DrawRectangle(Pens.Black, rect);
  }

  protected virtual void DrawPageBorder2(Graphics g, Rectangle rect)
  {
    --rect.Width;
    --rect.Height;
    g.DrawRectangle(Pens.Black, rect);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="eventargs"></param>
  protected override void OnResize(EventArgs eventargs)
  {
    this.InvalidateLayout();
    base.OnResize(eventargs);
  }

  protected override void OnMouseWheel(MouseEventArgs e)
  {
    if (!(e is HandledMouseEventArgs))
      return;
    ((HandledMouseEventArgs) e).Handled = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="se"></param>
  protected override void OnScroll(ScrollEventArgs se)
  {
    Point position = this.Position;
    if (se.ScrollOrientation == ScrollOrientation.VerticalScroll)
      position.Y = se.NewValue;
    if (se.ScrollOrientation == ScrollOrientation.HorizontalScroll)
      position.X = se.NewValue;
    this.SetPositionNoInvalidate(position);
    this.Refresh();
    base.OnScroll(se);
  }

  /// <summary>
  /// 
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public override void ResetBackColor() => this.BackColor = SystemColors.AppWorkspace;

  /// <summary>
  /// 
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public override void ResetForeColor() => this.ForeColor = Color.White;

  /// <summary>
  /// 
  /// </summary>
  private void CalculatePageInfo() => this.CalculatePageInfo(false);

  /// <summary>
  /// 
  /// </summary>
  private void ComputeLayout()
  {
    this._layoutOk = true;
    if (this._pageInfo.Length == 0)
    {
      this.ClientSize = this.Size;
    }
    else
    {
      Graphics graphics = this.CreateGraphics();
      this._screendpi = new Point((int) graphics.DpiX, (int) graphics.DpiY);
      IntPtr hdc = graphics.GetHdc();
      graphics.ReleaseHdcInternal(hdc);
      graphics.Dispose();
      Size physicalSize = this._pageInfo[this.StartPage].PhysicalSize;
      Size size = new Size(PreviewPrintControl.PixelsToPhysical(new Point(this.ClientRectangle.Size), this._screendpi));
      if (this._autoZoom)
        this._zoom = Math.Min((double) (size.Width - 10 * (this._columns + 1)) / (double) (this._columns * physicalSize.Width), (double) (size.Height - 10 * (this._rows + 1)) / (double) (this._rows * physicalSize.Height));
      this._imageSize = new Size((int) (this._zoom * (double) physicalSize.Width), (int) (this._zoom * (double) physicalSize.Height));
      this.SetVirtualSizeNoInvalidate(new Size(PreviewPrintControl.PhysicalToPixels(new Point(this._imageSize.Width * this._columns + 10 * (this._columns + 1), this._imageSize.Height * this._rows + 10 * (this._rows + 1)), this._screendpi)));
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void ComputePreview()
  {
    int startPage1 = this.StartPage;
    if (this._document == null)
    {
      this._pageInfo = new ExtendedPreviewPageInfo[0];
    }
    else
    {
      PrintController printController = this._document.PrintController;
      PreviewPrintController underlyingController = new PreviewPrintController(this);
      underlyingController.UseAntiAlias = this.UseAntiAlias;
      this._document.PrintController = (PrintController) new PrintControllerWithStatusDialog((PrintController) underlyingController, "Предварительный просмотр");
      this._document.Print();
      this._pageInfo = underlyingController.GetPreviewPageInfo();
      this._document.PrintController = printController;
    }
    int startPage2 = this.StartPage;
    if (startPage1 == startPage2)
      return;
    this.OnStartPageChanged(EventArgs.Empty);
  }

  /// <summary>
  /// 
  /// </summary>
  private void InvalidateLayout()
  {
    this._layoutOk = false;
    this.Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  public void InvalidatePreview()
  {
    this._pageInfo = (ExtendedPreviewPageInfo[]) null;
    this.InvalidateLayout();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="value"></param>
  private void SetPositionNoInvalidate(Point value)
  {
    Point position = this._position;
    this._position = value;
    this._position.X = Math.Min(this._position.X, this._virtualSize.Width - this.Width);
    this._position.Y = Math.Min(this._position.Y, this._virtualSize.Height - this.Height);
    if (this._position.X < 0)
      this._position.X = 0;
    if (this._position.Y < 0)
      this._position.Y = 0;
    Rectangle clientRectangle = this.ClientRectangle;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected virtual void OnStartPageChanged(EventArgs e)
  {
    if (!(this.Events[PreviewPrintControl.EVENT_STARTPAGECHANGED] is EventHandler eventHandler))
      return;
    eventHandler((object) this, e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="force"></param>
  public void CalculatePageInfo(bool force)
  {
    if (!(!this._pageInfoCalcPending | force))
      return;
    this._pageInfoCalcPending = true;
    try
    {
      if (this._pageInfo != null & force)
        return;
      try
      {
        this.ComputePreview();
      }
      catch
      {
        this._exceptionPrinting = true;
        throw;
      }
      finally
      {
        this.Invalidate();
      }
    }
    finally
    {
      this._pageInfoCalcPending = false;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="physical"></param>
  /// <param name="dpi"></param>
  /// <returns></returns>
  private static Point PhysicalToPixels(Point physical, Point dpi)
  {
    return new Point(PreviewPrintControl.PhysicalToPixels(physical.X, dpi.X), PreviewPrintControl.PhysicalToPixels(physical.Y, dpi.Y));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="physicalSize"></param>
  /// <param name="dpi"></param>
  /// <returns></returns>
  private static Size PhysicalToPixels(Size physicalSize, Point dpi)
  {
    return new Size(PreviewPrintControl.PhysicalToPixels(physicalSize.Width, dpi.X), PreviewPrintControl.PhysicalToPixels(physicalSize.Height, dpi.Y));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="physicalSize"></param>
  /// <param name="dpi"></param>
  /// <returns></returns>
  private static int PhysicalToPixels(int physicalSize, int dpi)
  {
    return (int) ((double) (physicalSize * dpi) / 100.0);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="pixels"></param>
  /// <param name="dpi"></param>
  /// <returns></returns>
  private static Point PixelsToPhysical(Point pixels, Point dpi)
  {
    return new Point(PreviewPrintControl.PixelsToPhysical(pixels.X, dpi.X), PreviewPrintControl.PixelsToPhysical(pixels.Y, dpi.Y));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="pixels"></param>
  /// <param name="dpi"></param>
  /// <returns></returns>
  private static Size PixelsToPhysical(Size pixels, Point dpi)
  {
    return new Size(PreviewPrintControl.PixelsToPhysical(pixels.Width, dpi.X), PreviewPrintControl.PixelsToPhysical(pixels.Height, dpi.Y));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="pixels"></param>
  /// <param name="dpi"></param>
  /// <returns></returns>
  private static int PixelsToPhysical(int pixels, int dpi)
  {
    return (int) ((double) pixels * 100.0 / (double) dpi);
  }

  private void InitializeComponent()
  {
    this._vScrollBar1 = new VScrollBar();
    this.SuspendLayout();
    this._vScrollBar1.Location = new Point(44, 47);
    this._vScrollBar1.Name = "vScrollBar1";
    this._vScrollBar1.Size = new Size(17, 80 /*0x50*/);
    this._vScrollBar1.TabIndex = 0;
    this._vScrollBar1.Visible = true;
    this.Controls.Add((Control) this._vScrollBar1);
    this.Name = nameof (PreviewPrintControl);
    this.ResumeLayout(false);
  }
}
