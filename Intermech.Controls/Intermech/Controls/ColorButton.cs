
// Type: Intermech.Controls.ColorButton
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Controls;

[Designer(typeof (ColorButtonDesigner))]
public class ColorButton : Button, IArrowKeysNavigationSupported
{
  private HatchStyle _hatchStyle = HatchStyle.Cross;
  private bool _underlineOnMouseHover;
  private ColorButton.BrushTypeEnum _brushType;
  private bool _forceDown;
  private bool _mouseOver;
  private Color _mouseOverColorOuter = SystemColors.Control;
  private Pen _mouseOverOuterPen;
  private Color _mouseOverColorInnerLight = SystemColors.ButtonHighlight;
  private Pen _mouseOverInnerPenLight;
  private Color _mouseOverColorInnerDark = SystemColors.ButtonShadow;
  private Pen _mouseOverInnerPenDark;
  private Color _borderColor = Color.DarkGray;
  public const AnchorStyles AllBorders = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
  private AnchorStyles _borders = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
  private Color _color = Color.Red;
  private Color _hatchBackgroundColor = Color.Empty;
  private object _lockObject = new object();
  private StringFormat _stringFormat = new StringFormat();
  private Font _underlinedFont;
  private Brush _brush;
  private Pen _borderPen;
  private SolidBrush _textBrush;
  private bool _lMouseButtonDown;
  private Control _upControl;
  private Control _downControl;
  private Control _leftControl;
  private Control _rightControl;

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(HatchStyle.Cross)]
  public HatchStyle HatchStyle
  {
    [DebuggerStepThrough] get => this._hatchStyle;
    set
    {
      if (this._hatchStyle == value)
        return;
      this._hatchStyle = value;
      if (this._brushType != ColorButton.BrushTypeEnum.Hatch)
        return;
      this.DisposeObj<Brush>(ref this._brush);
      this.Invalidate();
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(false)]
  public bool UnderlineOnMouseHover
  {
    [DebuggerStepThrough] get => this._underlineOnMouseHover;
    set => this._underlineOnMouseHover = value;
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(ColorButton.BrushTypeEnum.Solid)]
  public ColorButton.BrushTypeEnum BrushType
  {
    [DebuggerStepThrough] get => this._brushType;
    set
    {
      if (this._brushType == value)
        return;
      this._brushType = value;
      this.DisposeObj<Brush>(ref this._brush);
      this.Invalidate();
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(false)]
  public bool ForceDown
  {
    [DebuggerStepThrough] get => this._forceDown;
    set
    {
      if (this._forceDown == value)
        return;
      this._forceDown = value;
      this.Invalidate();
    }
  }

  private bool MouseOver
  {
    [DebuggerStepThrough] get => this._mouseOver;
    set
    {
      if (this._mouseOver == value)
        return;
      this._mouseOver = value;
      this.Invalidate();
    }
  }

  private bool DrawActiveBorder => this._mouseOver || this._forceDown;

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (Color), "Control")]
  public Color MouseOverColorOuter
  {
    [DebuggerStepThrough] get => this._mouseOverColorOuter;
    set
    {
      if (!(value != this._mouseOverColorOuter))
        return;
      this._mouseOverColorOuter = value;
      this.DisposeObj<Pen>(ref this._mouseOverOuterPen);
      if (!this.DrawActiveBorder)
        return;
      this.Invalidate();
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (Color), "ButtonHighlight")]
  public Color MouseOverColorInnerLight
  {
    [DebuggerStepThrough] get => this._mouseOverColorInnerLight;
    set
    {
      if (!(value != this._mouseOverColorInnerLight))
        return;
      this._mouseOverColorInnerLight = value;
      this.DisposeObj<Pen>(ref this._mouseOverInnerPenLight);
      if (!this.DrawActiveBorder)
        return;
      this.Invalidate();
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (Color), "ButtonShadow")]
  public Color MouseOverColorInnerDark
  {
    [DebuggerStepThrough] get => this._mouseOverColorInnerDark;
    set
    {
      if (!(value != this._mouseOverColorInnerDark))
        return;
      this._mouseOverColorInnerDark = value;
      this.DisposeObj<Pen>(ref this._mouseOverInnerPenDark);
      if (!this.DrawActiveBorder)
        return;
      this.Invalidate();
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (Color), "DarkGray")]
  public Color BorderColor
  {
    [DebuggerStepThrough] get => this._borderColor;
    set
    {
      if (!(value != this._borderColor))
        return;
      this._borderColor = value;
      this.DisposeObj<Pen>(ref this._borderPen);
      if (this._borders == AnchorStyles.None)
        return;
      this.Invalidate();
    }
  }

  [DefaultValue(AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right)]
  public AnchorStyles Borders
  {
    [DebuggerStepThrough] get => this._borders;
    set
    {
      if (value == this._borders)
        return;
      this._borders = value;
      this.Invalidate();
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (Color), "Red")]
  public Color Color
  {
    [DebuggerStepThrough] get => this._color;
    set
    {
      if (!(value != this._color))
        return;
      this._color = value;
      this.DisposeObj<Brush>(ref this._brush);
      this.Invalidate();
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (Color), "")]
  public Color HatchBackgroundColor
  {
    [DebuggerStepThrough] get => this._hatchBackgroundColor;
    set
    {
      if (!(value != this._hatchBackgroundColor))
        return;
      this._hatchBackgroundColor = value;
      if (this._brushType != ColorButton.BrushTypeEnum.Hatch)
        return;
      this.DisposeObj<Brush>(ref this._brush);
      this.Invalidate();
    }
  }

  public ColorButton()
  {
    this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    this.SetStyle(ControlStyles.Selectable, true);
    this.SetStyle(ControlStyles.UserPaint, true);
    this.SetStyle(ControlStyles.ResizeRedraw, true);
    this._stringFormat.Alignment = StringAlignment.Center;
    this._stringFormat.LineAlignment = StringAlignment.Center;
    this._stringFormat.HotkeyPrefix = HotkeyPrefix.Show;
    this._stringFormat.Trimming = StringTrimming.EllipsisCharacter;
  }

  protected override CreateParams CreateParams
  {
    get
    {
      CreateParams createParams = base.CreateParams;
      createParams.ExStyle |= 32 /*0x20*/;
      return createParams;
    }
  }

  protected override void OnPaintBackground(PaintEventArgs e)
  {
  }

  public void UpdateAllGraphics()
  {
    this.DisposeAllGraphics();
    this.Invalidate();
  }

  private void DisposeAllGraphics()
  {
    lock (this._lockObject)
    {
      this.DisposeObj<Brush>(ref this._brush);
      this.DisposeObj<Pen>(ref this._borderPen);
      this.DisposeObj<Pen>(ref this._mouseOverOuterPen);
      this.DisposeObj<Pen>(ref this._mouseOverInnerPenLight);
      this.DisposeObj<Pen>(ref this._mouseOverInnerPenDark);
      this.DisposeObj<SolidBrush>(ref this._textBrush);
      this.DisposeObj<Font>(ref this._underlinedFont);
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.DisposeAllGraphics();
      this.UpControl = (Control) null;
      this.DownControl = (Control) null;
      this.LeftControl = (Control) null;
      this.RightControl = (Control) null;
    }
    base.Dispose(disposing);
  }

  protected void DisposeObj<T>(ref T obj) where T : class, IDisposable
  {
    CommonHelper.SafeDisposeAndNull<T>(this._lockObject, ref obj);
  }

  protected override void OnFontChanged(EventArgs e)
  {
    this.DisposeObj<Font>(ref this._underlinedFont);
    this.DisposeObj<SolidBrush>(ref this._textBrush);
    base.OnFontChanged(e);
  }

  protected override void OnPaint(PaintEventArgs pevent)
  {
    lock (this._lockObject)
    {
      Rectangle clientRectangle1 = this.ClientRectangle;
      if (this._borders != AnchorStyles.None && !this.DrawActiveBorder)
      {
        LazyInitializer.EnsureInitialized<Pen>(ref this._borderPen, (Func<Pen>) (() => new Pen(this._borderColor)));
        if (this._borders == (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right))
        {
          pevent.Graphics.DrawRectangle(this._borderPen, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
          clientRectangle1.Inflate(-1, -1);
        }
        else
        {
          if ((this._borders & AnchorStyles.Left) != AnchorStyles.None)
          {
            pevent.Graphics.DrawLine(this._borderPen, 0, 0, 0, this.Height - 1);
            ++clientRectangle1.X;
            --clientRectangle1.Width;
          }
          if ((this._borders & AnchorStyles.Top) != AnchorStyles.None)
          {
            pevent.Graphics.DrawLine(this._borderPen, 0, 0, this.Width - 1, 0);
            ++clientRectangle1.Y;
            --clientRectangle1.Height;
          }
          if ((this._borders & AnchorStyles.Right) != AnchorStyles.None)
          {
            pevent.Graphics.DrawLine(this._borderPen, this.Width - 1, 0, this.Width - 1, this.Height - 1);
            --clientRectangle1.Width;
          }
          if ((this._borders & AnchorStyles.Bottom) != AnchorStyles.None)
          {
            pevent.Graphics.DrawLine(this._borderPen, 0, this.Height - 1, this.Width - 1, this.Height - 1);
            --clientRectangle1.Height;
          }
        }
      }
      if (this.DrawActiveBorder)
      {
        pevent.Graphics.DrawRectangle(LazyInitializer.EnsureInitialized<Pen>(ref this._mouseOverOuterPen, (Func<Pen>) (() => new Pen(this._mouseOverColorOuter))), new Rectangle(0, 0, clientRectangle1.Width - 1, clientRectangle1.Height - 1));
        clientRectangle1.Inflate(-1, -1);
        LazyInitializer.EnsureInitialized<Pen>(ref this._mouseOverInnerPenLight, (Func<Pen>) (() => new Pen(this._mouseOverColorInnerLight)));
        LazyInitializer.EnsureInitialized<Pen>(ref this._mouseOverInnerPenDark, (Func<Pen>) (() => new Pen(this._mouseOverColorInnerDark)));
        Pen pen1 = this._lMouseButtonDown || this._forceDown ? this._mouseOverInnerPenDark : this._mouseOverInnerPenLight;
        Pen pen2 = this._lMouseButtonDown || this._forceDown ? this._mouseOverInnerPenLight : this._mouseOverInnerPenDark;
        pevent.Graphics.DrawLine(pen1, clientRectangle1.Left, clientRectangle1.Top, clientRectangle1.Left, clientRectangle1.Bottom - 1);
        pevent.Graphics.DrawLine(pen1, clientRectangle1.Left + 1, clientRectangle1.Top, clientRectangle1.Right - 2, clientRectangle1.Top);
        pevent.Graphics.DrawLine(pen2, clientRectangle1.Left + 1, clientRectangle1.Bottom - 1, clientRectangle1.Right - 1, clientRectangle1.Bottom - 1);
        pevent.Graphics.DrawLine(pen2, clientRectangle1.Right - 1, clientRectangle1.Top, clientRectangle1.Right - 1, clientRectangle1.Bottom - 2);
        clientRectangle1.Inflate(-1, -1);
        if (this._lMouseButtonDown || this._forceDown)
        {
          pevent.Graphics.DrawLine(pen1, clientRectangle1.Left, clientRectangle1.Top, clientRectangle1.Left, clientRectangle1.Bottom - 1);
          pevent.Graphics.DrawLine(pen1, clientRectangle1.Left + 1, clientRectangle1.Top, clientRectangle1.Right - 2, clientRectangle1.Top);
          pevent.Graphics.DrawLine(pen2, clientRectangle1.Left + 1, clientRectangle1.Bottom - 1, clientRectangle1.Right - 1, clientRectangle1.Bottom - 1);
          pevent.Graphics.DrawLine(pen2, clientRectangle1.Right - 1, clientRectangle1.Top, clientRectangle1.Right - 1, clientRectangle1.Bottom - 2);
          clientRectangle1.Inflate(-1, -1);
        }
      }
      pevent.Graphics.FillRectangle(LazyInitializer.EnsureInitialized<Brush>(ref this._brush, new Func<Brush>(this.CreateBrush)), clientRectangle1);
      if (!string.IsNullOrEmpty(this.Text) || this.Image != null)
      {
        SizeF sizeF1;
        if (this.Image == null)
        {
          sizeF1 = SizeF.Empty;
        }
        else
        {
          Size size = this.Image.Size;
          double width = (double) (size.Width + 4);
          size = this.Image.Size;
          double height = (double) (size.Height + 4);
          sizeF1 = new SizeF((float) width, (float) height);
        }
        SizeF sizeF2 = sizeF1;
        float x = 0.0f;
        if (!string.IsNullOrEmpty(this.Text))
        {
          RectangleF layoutRectangle = new RectangleF((float) clientRectangle1.X, (float) clientRectangle1.Y, (float) clientRectangle1.Width, (float) clientRectangle1.Height);
          layoutRectangle.Inflate(-3f, -3f);
          if (this._lMouseButtonDown || this._forceDown)
            layoutRectangle.Offset(1f, 1f);
          LazyInitializer.EnsureInitialized<SolidBrush>(ref this._textBrush, (Func<SolidBrush>) (() => new SolidBrush(this.ForeColor)));
          SizeF sizeF3 = !string.IsNullOrEmpty(this.Text) ? pevent.Graphics.MeasureString(this.Text, this.Font, new SizeF(layoutRectangle.Width - sizeF2.Width, layoutRectangle.Height), this._stringFormat) : SizeF.Empty;
          if (this.Image != null)
          {
            switch (base.TextAlign)
            {
              case ContentAlignment.TopLeft:
              case ContentAlignment.MiddleLeft:
              case ContentAlignment.BottomLeft:
                x = layoutRectangle.X;
                layoutRectangle.X += sizeF2.Width;
                layoutRectangle.Width -= sizeF2.Width;
                break;
              case ContentAlignment.TopCenter:
              case ContentAlignment.MiddleCenter:
              case ContentAlignment.BottomCenter:
                float num1 = layoutRectangle.X + layoutRectangle.Width / 2f;
                float num2 = sizeF3.Width + sizeF2.Width;
                x = (float) ((double) layoutRectangle.X + (double) num1 - (double) num2 / 2.0);
                layoutRectangle.X = x + sizeF2.Width;
                layoutRectangle.Width = sizeF3.Width;
                break;
              case ContentAlignment.TopRight:
              case ContentAlignment.MiddleRight:
              case ContentAlignment.BottomRight:
                layoutRectangle.X = layoutRectangle.X + layoutRectangle.Width - sizeF3.Width;
                x = layoutRectangle.X - sizeF2.Width;
                layoutRectangle.Width = sizeF3.Width;
                break;
            }
          }
          Font font = !this._mouseOver || !this._underlineOnMouseHover ? this.Font : LazyInitializer.EnsureInitialized<Font>(ref this._underlinedFont, (Func<Font>) (() => !this.Font.Underline ? new Font(this.Font, this.Font.Style | FontStyle.Underline) : this.Font));
          pevent.Graphics.DrawString(this.Text, font, (Brush) this._textBrush, layoutRectangle, this._stringFormat);
        }
        else if (this.Image != null)
        {
          switch (this.ImageAlign)
          {
            case ContentAlignment.TopLeft:
            case ContentAlignment.MiddleLeft:
            case ContentAlignment.BottomLeft:
              x = (float) clientRectangle1.X;
              break;
            case ContentAlignment.TopCenter:
            case ContentAlignment.MiddleCenter:
            case ContentAlignment.BottomCenter:
              x = (float) (clientRectangle1.X + clientRectangle1.Width / 2) - sizeF2.Width / 2f;
              break;
            case ContentAlignment.TopRight:
            case ContentAlignment.MiddleRight:
            case ContentAlignment.BottomRight:
              x = (float) (clientRectangle1.Right - clientRectangle1.Width);
              break;
          }
          if (this._lMouseButtonDown || this._forceDown)
            ++x;
        }
        if (this.Image != null)
        {
          float num = 0.0f;
          switch (this.ImageAlign)
          {
            case ContentAlignment.TopLeft:
            case ContentAlignment.TopCenter:
            case ContentAlignment.TopRight:
              num = (float) clientRectangle1.Y;
              break;
            case ContentAlignment.MiddleLeft:
            case ContentAlignment.MiddleCenter:
            case ContentAlignment.MiddleRight:
              num = (float) (clientRectangle1.Y + clientRectangle1.Height / 2) - sizeF2.Height / 2f;
              break;
            case ContentAlignment.BottomLeft:
            case ContentAlignment.BottomCenter:
            case ContentAlignment.BottomRight:
              num = (float) (clientRectangle1.Bottom - clientRectangle1.Height);
              break;
          }
          if (this._lMouseButtonDown || this._forceDown)
            ++num;
          pevent.Graphics.DrawImageUnscaled(this.Image, (int) x, (int) num + 1);
        }
      }
      if (!this.Focused)
        return;
      Rectangle clientRectangle2 = this.ClientRectangle;
      clientRectangle2.Inflate(-4, -4);
      if (this._lMouseButtonDown || this._forceDown)
        clientRectangle2.Offset(1, 1);
      ControlPaint.DrawFocusRectangle(pevent.Graphics, clientRectangle2, this.Color.InvertAsBlackWhite(), this.Color.AsBlackWhite());
    }
  }

  public override ContentAlignment TextAlign
  {
    [DebuggerStepThrough] get => base.TextAlign;
    set
    {
      if (base.TextAlign == value)
        return;
      switch (value)
      {
        case ContentAlignment.TopLeft:
          this._stringFormat.Alignment = StringAlignment.Near;
          this._stringFormat.LineAlignment = StringAlignment.Near;
          break;
        case ContentAlignment.TopCenter:
          this._stringFormat.Alignment = StringAlignment.Center;
          this._stringFormat.LineAlignment = StringAlignment.Near;
          break;
        case ContentAlignment.TopRight:
          this._stringFormat.Alignment = StringAlignment.Far;
          this._stringFormat.LineAlignment = StringAlignment.Near;
          break;
        case ContentAlignment.MiddleLeft:
          this._stringFormat.Alignment = StringAlignment.Near;
          this._stringFormat.LineAlignment = StringAlignment.Center;
          break;
        case ContentAlignment.MiddleCenter:
          this._stringFormat.Alignment = StringAlignment.Center;
          this._stringFormat.LineAlignment = StringAlignment.Center;
          break;
        case ContentAlignment.MiddleRight:
          this._stringFormat.Alignment = StringAlignment.Far;
          this._stringFormat.LineAlignment = StringAlignment.Center;
          break;
        case ContentAlignment.BottomLeft:
          this._stringFormat.Alignment = StringAlignment.Near;
          this._stringFormat.LineAlignment = StringAlignment.Far;
          break;
        case ContentAlignment.BottomCenter:
          this._stringFormat.Alignment = StringAlignment.Center;
          this._stringFormat.LineAlignment = StringAlignment.Far;
          break;
        case ContentAlignment.BottomRight:
          this._stringFormat.Alignment = StringAlignment.Far;
          this._stringFormat.LineAlignment = StringAlignment.Far;
          break;
      }
      base.TextAlign = value;
    }
  }

  public override Color ForeColor
  {
    [DebuggerStepThrough] get => base.ForeColor;
    set
    {
      if (!(base.ForeColor != value))
        return;
      base.ForeColor = value;
      this.DisposeObj<SolidBrush>(ref this._textBrush);
      this.Invalidate();
    }
  }

  private Brush CreateBrush()
  {
    switch (this._brushType)
    {
      case ColorButton.BrushTypeEnum.Solid:
        return (Brush) new SolidBrush(this.Color);
      case ColorButton.BrushTypeEnum.Hatch:
        return (Brush) new HatchBrush(this._hatchStyle, this._color, !(this._hatchBackgroundColor != Color.Transparent) || !(this._hatchBackgroundColor != Color.Empty) ? base.BackColor : this._hatchBackgroundColor);
      default:
        throw new Exception($"Unknown BrushType value {this._brushType}");
    }
  }

  protected override bool ShowFocusCues => false;

  protected override void OnMouseEnter(EventArgs e)
  {
    this.MouseOver = true;
    base.OnMouseEnter(e);
  }

  protected override void OnMouseLeave(EventArgs e)
  {
    this.MouseOver = false;
    base.OnMouseLeave(e);
  }

  private bool LMouseButtonDown
  {
    [DebuggerStepThrough] get => this._lMouseButtonDown;
    set
    {
      if (value == this._lMouseButtonDown)
        return;
      this._lMouseButtonDown = value;
      this.Invalidate();
    }
  }

  protected override void OnMouseDown(MouseEventArgs mevent)
  {
    if (mevent.Button == MouseButtons.Left)
      this.LMouseButtonDown = true;
    base.OnMouseDown(mevent);
  }

  protected override void OnMouseUp(MouseEventArgs mevent)
  {
    if (mevent.Button == MouseButtons.Left)
      this.LMouseButtonDown = false;
    base.OnMouseUp(mevent);
  }

  protected override void OnLeave(EventArgs e)
  {
    base.OnLeave(e);
    this.Invalidate();
  }

  protected override void OnEnter(EventArgs e)
  {
    base.OnEnter(e);
    this.Invalidate();
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Category("Navigation")]
  [DefaultValue(null)]
  public Control UpControl
  {
    [DebuggerStepThrough] get => this._upControl;
    set
    {
      if (this._upControl == value)
        return;
      Control upControl = this._upControl;
      if (value != null)
        value.Disposed += new EventHandler(this.UpControl_Disposed);
      if (this._upControl != null)
        this._upControl.Disposed -= new EventHandler(this.UpControl_Disposed);
      this._upControl = value;
      if (value != null && value is IArrowKeysNavigationSupported)
      {
        IArrowKeysNavigationSupported navigationSupported = (IArrowKeysNavigationSupported) value;
        if (navigationSupported.DownControl == null)
          navigationSupported.DownControl = (Control) this;
      }
      if (upControl == null || upControl.IsDisposed || !(upControl is IArrowKeysNavigationSupported))
        return;
      IArrowKeysNavigationSupported navigationSupported1 = (IArrowKeysNavigationSupported) upControl;
      if (navigationSupported1.DownControl != this)
        return;
      navigationSupported1.DownControl = (Control) null;
    }
  }

  private void UpControl_Disposed(object sender, EventArgs e)
  {
    this._upControl.Disposed -= new EventHandler(this.UpControl_Disposed);
    this._upControl = (Control) null;
  }

  private bool NavigateFromDirrection(Control control, IEnumerable<Control> borderControls)
  {
    bool flag = false;
    if (control is ILastFocusedControlTracker)
    {
      ILastFocusedControlTracker focusedControlTracker = (ILastFocusedControlTracker) control;
      if (focusedControlTracker.TrackLastFocusedChildControl && focusedControlTracker.LastFocusedChildControl != null)
      {
        Control lastControl = focusedControlTracker.LastFocusedChildControl;
        if (lastControl.CanFocus && borderControls.Contains<Control>((Predicate<Control>) (ctrl => ctrl == lastControl)))
        {
          lastControl.Focus();
          flag = true;
        }
      }
    }
    if (!flag)
    {
      Control control1 = borderControls.FirstOrDefault<Control>((Func<Control, bool>) (ctrl => ctrl.CanFocus));
      if (control1 != null)
      {
        control1.Focus();
        flag = true;
      }
    }
    return flag;
  }

  [Category("Navigation")]
  public event OnNavigateDelegate OnNavigateToUp;

  public virtual void NavigateToUp()
  {
    bool blockDefaultNavigation = false;
    if (this.OnNavigateToUp != null)
      this.OnNavigateToUp((IArrowKeysNavigationSupported) this, ref blockDefaultNavigation);
    if (this._upControl == null || blockDefaultNavigation || !this._upControl.CanFocus)
      return;
    bool flag = false;
    if (this._upControl is IFocusFromDirection)
    {
      IFocusFromDirection upControl = (IFocusFromDirection) this._upControl;
      if (!upControl.BottomMostControls.IsEmpty<Control>())
        flag = this.NavigateFromDirrection(this._upControl, upControl.BottomMostControls);
    }
    if (flag)
      return;
    this._upControl.Focus();
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Category("Navigation")]
  [DefaultValue(null)]
  public Control DownControl
  {
    [DebuggerStepThrough] get => this._downControl;
    set
    {
      if (this._downControl == value)
        return;
      Control downControl = this._downControl;
      if (value != null)
        value.Disposed += new EventHandler(this.DownControl_Disposed);
      if (this._downControl != null)
        this._downControl.Disposed -= new EventHandler(this.DownControl_Disposed);
      this._downControl = value;
      if (value != null && value is IArrowKeysNavigationSupported)
      {
        IArrowKeysNavigationSupported navigationSupported = (IArrowKeysNavigationSupported) value;
        if (navigationSupported.UpControl == null)
          navigationSupported.UpControl = (Control) this;
      }
      if (downControl == null || downControl.IsDisposed || !(downControl is IArrowKeysNavigationSupported))
        return;
      IArrowKeysNavigationSupported navigationSupported1 = (IArrowKeysNavigationSupported) downControl;
      if (navigationSupported1.UpControl != this)
        return;
      navigationSupported1.UpControl = (Control) null;
    }
  }

  private void DownControl_Disposed(object sender, EventArgs e)
  {
    this._downControl.Disposed -= new EventHandler(this.DownControl_Disposed);
    this._downControl = (Control) null;
  }

  [Category("Navigation")]
  public event OnNavigateDelegate OnNavigateToDown;

  public virtual void NavigateToDown()
  {
    bool blockDefaultNavigation = false;
    if (this.OnNavigateToDown != null)
      this.OnNavigateToDown((IArrowKeysNavigationSupported) this, ref blockDefaultNavigation);
    if (this._downControl == null || blockDefaultNavigation || !this._downControl.CanFocus)
      return;
    bool flag = false;
    if (this._downControl is IFocusFromDirection)
    {
      IFocusFromDirection downControl = (IFocusFromDirection) this._downControl;
      if (!downControl.TopMostControls.IsEmpty<Control>())
        flag = this.NavigateFromDirrection(this._downControl, downControl.TopMostControls);
    }
    if (flag)
      return;
    this._downControl.Focus();
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Category("Navigation")]
  [DefaultValue(null)]
  public Control LeftControl
  {
    [DebuggerStepThrough] get => this._leftControl;
    set
    {
      if (this._leftControl == value)
        return;
      Control leftControl = this._leftControl;
      if (value != null)
        value.Disposed += new EventHandler(this.LeftControl_Disposed);
      if (this._leftControl != null)
        this._leftControl.Disposed -= new EventHandler(this.LeftControl_Disposed);
      this._leftControl = value;
      if (value != null && value is IArrowKeysNavigationSupported)
      {
        IArrowKeysNavigationSupported navigationSupported = (IArrowKeysNavigationSupported) value;
        if (navigationSupported.RightControl == null)
          navigationSupported.RightControl = (Control) this;
      }
      if (leftControl == null || leftControl.IsDisposed || !(leftControl is IArrowKeysNavigationSupported))
        return;
      IArrowKeysNavigationSupported navigationSupported1 = (IArrowKeysNavigationSupported) leftControl;
      if (navigationSupported1.RightControl != this)
        return;
      navigationSupported1.RightControl = (Control) null;
    }
  }

  private void LeftControl_Disposed(object sender, EventArgs e)
  {
    this._leftControl.Disposed -= new EventHandler(this.LeftControl_Disposed);
    this._leftControl = (Control) null;
  }

  [Category("Navigation")]
  public event OnNavigateDelegate OnNavigateToLeft;

  public virtual void NavigateToLeft()
  {
    bool blockDefaultNavigation = false;
    if (this.OnNavigateToLeft != null)
      this.OnNavigateToLeft((IArrowKeysNavigationSupported) this, ref blockDefaultNavigation);
    if (this._leftControl == null || blockDefaultNavigation || !this._leftControl.CanFocus)
      return;
    bool flag = false;
    if (this._leftControl is IFocusFromDirection)
    {
      IFocusFromDirection leftControl = (IFocusFromDirection) this._leftControl;
      if (!leftControl.RightMostControls.IsEmpty<Control>())
        flag = this.NavigateFromDirrection(this._leftControl, leftControl.RightMostControls);
    }
    if (flag)
      return;
    this._leftControl.Focus();
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Category("Navigation")]
  [DefaultValue(null)]
  public Control RightControl
  {
    [DebuggerStepThrough] get => this._rightControl;
    set
    {
      if (this._rightControl == value)
        return;
      Control rightControl = this._rightControl;
      if (value != null)
        value.Disposed += new EventHandler(this.RightControl_Disposed);
      if (this._rightControl != null)
        this._rightControl.Disposed -= new EventHandler(this.RightControl_Disposed);
      this._rightControl = value;
      if (value != null && value is IArrowKeysNavigationSupported)
      {
        IArrowKeysNavigationSupported navigationSupported = (IArrowKeysNavigationSupported) value;
        if (navigationSupported.LeftControl == null)
          navigationSupported.LeftControl = (Control) this;
      }
      if (rightControl == null || rightControl.IsDisposed || !(rightControl is IArrowKeysNavigationSupported))
        return;
      IArrowKeysNavigationSupported navigationSupported1 = (IArrowKeysNavigationSupported) rightControl;
      if (navigationSupported1.LeftControl != this)
        return;
      navigationSupported1.LeftControl = (Control) null;
    }
  }

  private void RightControl_Disposed(object sender, EventArgs e)
  {
    this._rightControl.Disposed -= new EventHandler(this.RightControl_Disposed);
    this._rightControl = (Control) null;
  }

  [Category("Navigation")]
  public event OnNavigateDelegate OnNavigateToRight;

  public virtual void NavigateToRight()
  {
    bool blockDefaultNavigation = false;
    if (this.OnNavigateToRight != null)
      this.OnNavigateToRight((IArrowKeysNavigationSupported) this, ref blockDefaultNavigation);
    if (this._rightControl == null || blockDefaultNavigation || !this._rightControl.CanFocus)
      return;
    bool flag = false;
    if (this._rightControl is IFocusFromDirection)
    {
      IFocusFromDirection rightControl = (IFocusFromDirection) this._rightControl;
      if (!rightControl.LeftMostControls.IsEmpty<Control>())
        flag = this.NavigateFromDirrection(this._leftControl, rightControl.LeftMostControls);
    }
    if (flag)
      return;
    this._rightControl.Focus();
  }

  protected override bool IsInputKey(Keys keyData)
  {
    switch (keyData)
    {
      case Keys.Left:
      case Keys.Left | Keys.Shift:
      case Keys.Left | Keys.Control:
        return this._leftControl != null || this.OnNavigateToLeft != null;
      case Keys.Up:
      case Keys.Up | Keys.Shift:
      case Keys.Up | Keys.Control:
        return this._upControl != null || this.OnNavigateToUp != null;
      case Keys.Right:
      case Keys.Right | Keys.Shift:
      case Keys.Right | Keys.Control:
        return this._rightControl != null || this.OnNavigateToRight != null;
      case Keys.Down:
      case Keys.Down | Keys.Shift:
      case Keys.Down | Keys.Control:
        return this._downControl != null || this.OnNavigateToDown != null;
      default:
        return base.IsInputKey(keyData);
    }
  }

  protected override void OnKeyDown(KeyEventArgs e)
  {
    switch (e.KeyCode)
    {
      case Keys.Left:
        if (this._leftControl != null || this.OnNavigateToLeft != null)
        {
          this.NavigateToLeft();
          return;
        }
        break;
      case Keys.Up:
        if (this._upControl != null || this.OnNavigateToUp != null)
        {
          this.NavigateToUp();
          return;
        }
        break;
      case Keys.Right:
        if (this._rightControl != null || this.OnNavigateToRight != null)
        {
          this.NavigateToRight();
          return;
        }
        break;
      case Keys.Down:
        if (this._downControl != null || this.OnNavigateToDown != null)
        {
          this.NavigateToDown();
          return;
        }
        break;
    }
    base.OnKeyDown(e);
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [ReadOnly(true)]
  public new Color BackColor
  {
    [DebuggerStepThrough] get => this._color;
    set => throw new NotImplementedException("BackColor is dennied");
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [ReadOnly(true)]
  public new FlatStyle FlatStyle
  {
    [DebuggerStepThrough] get => base.FlatStyle;
    set => throw new NotImplementedException("FlatStyle is dennied");
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [ReadOnly(true)]
  public new FlatButtonAppearance FlatAppearance
  {
    [DebuggerStepThrough] get => base.FlatAppearance;
    set => throw new NotImplementedException("FlatAppearance is dennied");
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [ReadOnly(true)]
  public new Image BackgroundImage
  {
    get => base.BackgroundImage;
    set => throw new NotImplementedException("BackgroundImage is dennied");
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [ReadOnly(true)]
  public new ImageLayout BackgroundImageLayout
  {
    get => base.BackgroundImageLayout;
    set => throw new NotImplementedException("BackgroundImageLayout is dennied");
  }

  public enum BrushTypeEnum
  {
    Solid,
    Hatch,
  }
}
