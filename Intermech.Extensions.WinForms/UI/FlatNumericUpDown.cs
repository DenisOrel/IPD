// Decompiled with JetBrains decompiler
// Type: Intermech.UI.FlatNumericUpDown
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.UI;

[CLSCompliant(false)]
public class FlatNumericUpDown : NumericUpDown, ISupportInitialize
{
  [NotNull]
  private readonly FieldInfo _buttonState;
  [NotNull]
  private Control _upDownButtons;
  [NotNull]
  private Control _upDownEdit;
  private Brush _arrowBkBrush;
  private readonly Brush _arrowHotTrackBrush = (Brush) new SolidBrush(SystemColors.ControlLight);
  private readonly Brush _arrowHotTrackDownBrush = (Brush) new SolidBrush(SystemColors.Control);
  protected Brush _ArrowBrush = (Brush) new SolidBrush(SystemColors.ControlDarkDark);
  private readonly Pen _arrowPen = new Pen(SystemColors.ControlDark);
  private Rectangle _upButtonRect = Rectangle.Empty;
  private Rectangle _downButtonRect = Rectangle.Empty;
  private Bitmap _arrowUpBitmap;
  private Bitmap _arrowDownBitmap;
  private bool _mouseCaptured;
  private FlatNumericUpDown.MouseHoverState _hoverState;
  private int _lastUpDownHeight;
  private bool _position;
  [NotNull]
  private readonly GlobalMouseHandler _mouseHandler = new GlobalMouseHandler();
  private int _lockTextCounter;
  private bool _currentValueChanged;

  public static void SetStyleHack([NotNull] Control control, ControlStyles flag)
  {
    control.GetType().GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic)?.Invoke((object) control, new object[2]
    {
      (object) flag,
      (object) true
    });
  }

  public FlatNumericUpDown()
  {
    this.CheckControls();
    this._upDownButtons.Paint += new PaintEventHandler(this.upDownButtons_Paint);
    this._buttonState = this._upDownButtons.GetType().GetField("pushed", BindingFlags.Instance | BindingFlags.NonPublic);
    FlatNumericUpDown.SetStyleHack(this._upDownButtons, ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer);
    this._upDownButtons.MouseEnter += new EventHandler(this._upDownButtons_MouseEnter);
    this._upDownButtons.MouseLeave += new EventHandler(this._upDownButtons_MouseLeave);
    this._upDownButtons.MouseHover += new EventHandler(this._upDownButtons_MouseHover);
    this.SetStyle(ControlStyles.Opaque, false);
    this._mouseHandler.TheMouseMoved += new MouseMovedEvent(this._mouseHandler_TheMouseMoved);
  }

  private void _mouseHandler_TheMouseMoved() => this.UpdateMouseHoverState();

  private void _upDownButtons_MouseHover([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.UpdateMouseHoverState();
  }

  private void _upDownButtons_MouseLeave([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.UpdateMouseHoverState();
  }

  private void _upDownButtons_MouseEnter([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.UpdateMouseHoverState();
  }

  private void CheckControls()
  {
    if (this._upDownButtons != null)
      return;
    this._upDownButtons = this.Controls[0];
    this._upDownEdit = this.Controls[1];
  }

  protected bool DownButtonPushed()
  {
    return Convert.ToInt32(this._buttonState.GetValue((object) this._upDownButtons)) == 2;
  }

  protected bool UpButtonPushed()
  {
    return Convert.ToInt32(this._buttonState.GetValue((object) this._upDownButtons)) == 1;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this._mouseCaptured)
        Application.RemoveMessageFilter((IMessageFilter) this._mouseHandler);
      if (this._arrowBkBrush != null)
        this._arrowBkBrush.Dispose();
      this._ArrowBrush?.Dispose();
      this._arrowPen?.Dispose();
      this._arrowHotTrackBrush?.Dispose();
      this._arrowHotTrackDownBrush?.Dispose();
      if (this._arrowUpBitmap != null)
        this._arrowUpBitmap.Dispose();
      if (this._arrowDownBitmap != null)
        this._arrowDownBitmap.Dispose();
    }
    base.Dispose(disposing);
  }

  private void UpdateButtonImage()
  {
    int width1 = this._upDownButtons.ClientRectangle.Width;
    if (this._arrowUpBitmap != null)
      this._arrowUpBitmap.Dispose();
    int width2 = this._upDownButtons.ClientRectangle.Width;
    Rectangle clientRectangle1 = this._upDownButtons.ClientRectangle;
    int height1 = (clientRectangle1.Height >> 1) - 1;
    this._upButtonRect = new Rectangle(0, 0, width2, height1);
    int y = this._upButtonRect.Height + 1;
    clientRectangle1 = this._upDownButtons.ClientRectangle;
    int width3 = clientRectangle1.Width;
    Rectangle clientRectangle2 = this._upDownButtons.ClientRectangle;
    int height2 = clientRectangle2.Height;
    clientRectangle2 = this._upDownButtons.ClientRectangle;
    int num = clientRectangle2.Height >> 1;
    int height3 = height2 - num;
    this._downButtonRect = new Rectangle(0, y, width3, height3);
    this._arrowUpBitmap = new Bitmap(width1 - 1, this._upButtonRect.Height, PixelFormat.Format24bppRgb);
    this._arrowUpBitmap.MakeTransparent();
    this._arrowDownBitmap = new Bitmap(width1 - 1, this._downButtonRect.Height, PixelFormat.Format24bppRgb);
    this._arrowDownBitmap.MakeTransparent();
    using (Graphics graphics = Graphics.FromImage((Image) this._arrowUpBitmap))
      this.PaintButtonUpImage(graphics, this._upButtonRect);
    using (Graphics graphics = Graphics.FromImage((Image) this._arrowDownBitmap))
      this.PaintButtonDownImage(graphics, this._downButtonRect);
  }

  protected virtual void PaintButtonUpImage([NotNull] Graphics graphics, Rectangle rect)
  {
    int width = rect.Width;
    PointF[] points = new PointF[3]
    {
      new PointF((float) (width * 3 / 16 /*0x10*/), (float) (width * 7 / 16 /*0x10*/)),
      new PointF((float) (width * 7 / 16 /*0x10*/), (float) (width * 2 / 16 /*0x10*/)),
      new PointF((float) (width * 12 / 16 /*0x10*/), (float) (width * 7 / 16 /*0x10*/))
    };
    graphics.SmoothingMode = SmoothingMode.None;
    graphics.FillPolygon(this._ArrowBrush, points);
  }

  protected virtual void PaintButtonDownImage([NotNull] Graphics graphics, Rectangle rect)
  {
    graphics.DrawImage((Image) this._arrowUpBitmap, rect.Left, rect.Top, this._arrowUpBitmap.Width, -this._arrowUpBitmap.Height);
  }

  private FlatNumericUpDown.MouseHoverState HoverState
  {
    [DebuggerStepThrough] get => this._hoverState;
    set
    {
      if (this._hoverState == value)
        return;
      this._hoverState = value;
      if (this._hoverState != FlatNumericUpDown.MouseHoverState.None && !this._mouseCaptured)
      {
        Application.AddMessageFilter((IMessageFilter) this._mouseHandler);
        this._mouseCaptured = true;
      }
      else if (this._hoverState == FlatNumericUpDown.MouseHoverState.None && this._mouseCaptured)
      {
        Application.RemoveMessageFilter((IMessageFilter) this._mouseHandler);
        this._mouseCaptured = false;
      }
      this._upDownButtons.Invalidate();
    }
  }

  private void UpdateMouseHoverState()
  {
    this.CheckControls();
    Rectangle upButtonRect = this._upButtonRect;
    Rectangle downButtonRect = this._downButtonRect;
    upButtonRect.Inflate(1, 1);
    downButtonRect.Inflate(1, 1);
    Point client = this._upDownButtons.PointToClient(Cursor.Position);
    if (upButtonRect.Contains(client))
      this.HoverState = FlatNumericUpDown.MouseHoverState.Up;
    else if (downButtonRect.Contains(client))
      this.HoverState = FlatNumericUpDown.MouseHoverState.Down;
    else
      this.HoverState = FlatNumericUpDown.MouseHoverState.None;
  }

  private void upDownButtons_Paint([CanBeNull] object sender, [NotNull] PaintEventArgs e)
  {
    this.CheckControls();
    if (this._lastUpDownHeight != this._upDownButtons.ClientRectangle.Height)
    {
      this.UpdateButtonImage();
      this._lastUpDownHeight = this._upDownButtons.ClientRectangle.Height;
    }
    if (this._arrowBkBrush == null)
      this._arrowBkBrush = (Brush) new SolidBrush(this.BackColor);
    e.Graphics.FillRectangle(this._arrowBkBrush, this._upDownButtons.ClientRectangle);
    switch (this._hoverState)
    {
      case FlatNumericUpDown.MouseHoverState.Up:
        Brush brush1 = this.UpButtonPushed() ? this._arrowHotTrackBrush : this._arrowHotTrackDownBrush;
        e.Graphics.FillRectangle(brush1, this._upButtonRect);
        e.Graphics.DrawLine(this._arrowPen, 0, 0, 0, this._upButtonRect.Height);
        e.Graphics.DrawLine(this._arrowPen, 0, this._upButtonRect.Height, this._upButtonRect.Width, this._upButtonRect.Height);
        break;
      case FlatNumericUpDown.MouseHoverState.Down:
        Brush brush2 = this.DownButtonPushed() ? this._arrowHotTrackBrush : this._arrowHotTrackDownBrush;
        e.Graphics.FillRectangle(brush2, this._downButtonRect);
        e.Graphics.DrawLine(this._arrowPen, 0, this._downButtonRect.Top, this._downButtonRect.Right, this._downButtonRect.Top);
        e.Graphics.DrawLine(this._arrowPen, 0, this._downButtonRect.Top, 0, this._downButtonRect.Bottom);
        break;
    }
    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
    e.Graphics.DrawImage((Image) this._arrowUpBitmap, 1, 0);
    e.Graphics.DrawImage((Image) this._arrowDownBitmap, 1, this._downButtonRect.Top);
  }

  private void PositionControls()
  {
    if (this._position)
      return;
    try
    {
      this._position = true;
      this.CheckControls();
      Rectangle rectangle1 = Rectangle.Empty;
      Rectangle rectangle2 = Rectangle.Empty;
      Rectangle rectangle3 = new Rectangle(Point.Empty, this.ClientSize);
      int width = rectangle3.Width;
      bool withVisualStyles = Application.RenderWithVisualStyles;
      int borderStyle = (int) this.BorderStyle;
      int num1 = borderStyle == 0 ? 0 : 2;
      rectangle3.Inflate(-num1, -num1);
      int num2 = rectangle3.Height * 16 /*0x10*/ / 20;
      int num3 = borderStyle != 0 ? (withVisualStyles ? 1 : 2) : 0;
      if (this._upDownEdit != null)
        rectangle1 = rectangle3 with
        {
          Size = new Size(rectangle3.Width - num2 + num3 + 3, rectangle3.Height)
        };
      if (this._upDownButtons != null)
        rectangle2 = new Rectangle(rectangle3.Right - num2 + num3 - 1, rectangle3.Top - num3, num2 + 1, rectangle3.Height + num3 * 2);
      if (this.RtlTranslateLeftRight(this.UpDownAlign) == LeftRightAlignment.Left)
      {
        rectangle2.X = width - rectangle2.Right;
        rectangle1.X = width - rectangle1.Right;
      }
      if (this._upDownEdit != null)
        this._upDownEdit.Bounds = rectangle1;
      if (this._upDownButtons == null)
        return;
      this._upDownButtons.Bounds = rectangle2;
      this._upDownButtons.Invalidate();
    }
    finally
    {
      this._position = false;
    }
  }

  protected override void OnHandleCreated([NotNull] EventArgs e)
  {
    base.OnHandleCreated(e);
    this.PositionControls();
  }

  protected override void OnTextBoxResize([CanBeNull] object source, [NotNull] EventArgs e)
  {
    base.OnTextBoxResize(source, e);
    this.PositionControls();
  }

  protected override void OnLayout(LayoutEventArgs e)
  {
    base.OnLayout(e);
    this.PositionControls();
  }

  protected override void OnFontChanged([NotNull] EventArgs e)
  {
    base.OnFontChanged(e);
    this.PositionControls();
  }

  protected override void UpdateEditText()
  {
    ++this._lockTextCounter;
    try
    {
      base.UpdateEditText();
    }
    finally
    {
      --this._lockTextCounter;
    }
    if (!this._currentValueChanged && (string.IsNullOrEmpty(this.Text) || this.Text.Length == 1 && this.Text == "-"))
      return;
    this._currentValueChanged = false;
    this.ChangingText = true;
    this.Text = this.GetNumberText(base.Value);
  }

  [NotNull]
  public override string Text
  {
    get => base.Text ?? string.Empty;
    set
    {
      if (this._lockTextCounter != 0)
        return;
      base.Text = value;
    }
  }

  [NotNull]
  private string GetNumberText(Decimal num)
  {
    string numberText;
    if (this.Hexadecimal)
    {
      numberText = ((long) num).ToString("X", (IFormatProvider) CultureInfo.InvariantCulture);
    }
    else
    {
      StringBuilder stringBuilder = new StringBuilder(20);
      bool flag = false;
      if (this.ThousandsSeparator)
      {
        stringBuilder.Append("#0,#");
        flag = true;
      }
      if (!flag)
        stringBuilder.Append("#0");
      if (this.DecimalPlaces > 0)
        stringBuilder.Append('.').Append('#', this.DecimalPlaces);
      numberText = num.ToString(stringBuilder.ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
    }
    return numberText;
  }

  public new Decimal Value
  {
    get => base.Value;
    set
    {
      if (!(value != base.Value))
        return;
      if (value < this.Minimum || value > this.Maximum)
        throw new ArgumentOutOfRangeException("Value min max");
      this._currentValueChanged = true;
      base.Value = value;
    }
  }

  protected override bool IsInputKey(Keys keyData)
  {
    if (keyData <= Keys.Down)
    {
      if (keyData != Keys.Up && keyData != Keys.Down)
        goto label_4;
    }
    else if (keyData != (Keys.Up | Keys.Control) && keyData != (Keys.Down | Keys.Control))
      goto label_4;
    return !this.InterceptArrowKeys;
label_4:
    return base.IsInputKey(keyData);
  }

  protected override void OnTextBoxKeyDown([CanBeNull] object source, [NotNull] KeyEventArgs e)
  {
    if (!this.InterceptArrowKeys && e.Control)
    {
      if (e.KeyCode == Keys.Up)
      {
        this.UpButton();
        e.Handled = true;
        this.InterceptArrowKeys = true;
        try
        {
          this.OnKeyDown(e);
          return;
        }
        finally
        {
          this.InterceptArrowKeys = false;
        }
      }
      else if (e.KeyCode == Keys.Down)
      {
        this.DownButton();
        e.Handled = true;
        this.InterceptArrowKeys = true;
        try
        {
          this.OnKeyDown(e);
          return;
        }
        finally
        {
          this.InterceptArrowKeys = false;
        }
      }
    }
    this.OnKeyDown(e);
  }

  private enum MouseHoverState
  {
    None,
    Up,
    Down,
  }
}
