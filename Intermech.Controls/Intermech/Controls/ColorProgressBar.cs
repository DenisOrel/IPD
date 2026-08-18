
// Type: Intermech.Controls.ColorProgressBar
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Intermech.Controls;

[Description("Color Progress Bar")]
[ToolboxBitmap(typeof (ProgressBar))]
[Designer(typeof (ColorProgressBarDesigner))]
public class ColorProgressBar : Control
{
  private int _value;
  private int _minValue;
  private int _maxValue = 100;
  private int _step = 10;
  private bool _showPercent;
  private float _darkPercent = 0.25f;
  private ColorProgressBar.FillStyles _fillStyle;
  private ColorProgressBar.GradientModes _gradientMode = ColorProgressBar.GradientModes.Center;
  private Color _barColor = Color.SkyBlue;
  private Color _borderColor = SystemColors.ControlDark;

  public event EventHandler Changed;

  public ColorProgressBar()
  {
    this.Size = new Size(150, 15);
    this.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
  }

  [DefaultValue(false)]
  [Category("ColorProgressBar")]
  public bool ShowPercent
  {
    get => this._showPercent;
    set
    {
      if (this._showPercent == value)
        return;
      this._showPercent = value;
      this.Invalidate();
    }
  }

  [Description("ColorProgressBar color")]
  [Category("ColorProgressBar")]
  public Color BarColor
  {
    get => this._barColor;
    set
    {
      if (!(this._barColor != value))
        return;
      this._barColor = value;
      this.Invalidate();
    }
  }

  protected bool ShouldSerializeBarColor() => this._barColor != Color.SkyBlue;

  [Description("ColorProgressBar color dark percent")]
  [Category("ColorProgressBar")]
  [DefaultValue(0.25f)]
  public float DarkPercent
  {
    get => this._darkPercent;
    set
    {
      if ((double) this._darkPercent == (double) value)
        return;
      this._darkPercent = value;
      this.Invalidate();
    }
  }

  [Description("ColorProgressBar fill style")]
  [Category("ColorProgressBar")]
  [DefaultValue(ColorProgressBar.FillStyles.Solid)]
  public ColorProgressBar.FillStyles FillStyle
  {
    get => this._fillStyle;
    set
    {
      if (this._fillStyle == value)
        return;
      this._fillStyle = value;
      this.Invalidate();
    }
  }

  [Description("ColorProgressBar gradient fill style")]
  [Category("ColorProgressBar")]
  [DefaultValue(ColorProgressBar.GradientModes.Center)]
  public ColorProgressBar.GradientModes GradientMode
  {
    get => this._gradientMode;
    set
    {
      if (this._gradientMode == value)
        return;
      this._gradientMode = value;
      this.Invalidate();
    }
  }

  [Description("The current value for the ColorProgressBar, in the range specified by the Minimum and Maximum properties.")]
  [Category("ColorProgressBar")]
  [RefreshProperties(RefreshProperties.All)]
  public int Value
  {
    get => this._value;
    set
    {
      if (this._value == value)
        return;
      if (value < this._minValue)
        throw new ArgumentException($"'{(object) value}' is not a valid value for 'Value'.\n'Value' must be between 'Minimum' and 'Maximum'.");
      this._value = value <= this._maxValue ? value : throw new ArgumentException($"'{(object) value}' is not a valid value for 'Value'.\n'Value' must be between 'Minimum' and 'Maximum'.");
      this.Invalidate();
      this.OnChanged();
    }
  }

  [Description("The lower bound of the range this ColorProgressbar is working with.")]
  [Category("ColorProgressBar")]
  [RefreshProperties(RefreshProperties.All)]
  [DefaultValue(0)]
  public int Minimum
  {
    get => this._minValue;
    set
    {
      if (this._minValue == value)
        return;
      this._minValue = value;
      if (this._minValue > this._maxValue)
        this._maxValue = this._minValue;
      if (this._minValue > this._value)
        this._value = this._minValue;
      this.Invalidate();
      this.OnChanged();
    }
  }

  [Description("The uppper bound of the range this ColorProgressbar is working with.")]
  [Category("ColorProgressBar")]
  [RefreshProperties(RefreshProperties.All)]
  [DefaultValue(100)]
  public int Maximum
  {
    get => this._maxValue;
    set
    {
      if (this._maxValue == value)
        return;
      this._maxValue = value;
      if (this._maxValue < this._value)
        this._value = this._maxValue;
      if (this._maxValue < this._minValue)
        this._minValue = this._maxValue;
      this.Invalidate();
    }
  }

  [Description("The amount to jump the current value of the control by when the Step() method is called.")]
  [Category("ColorProgressBar")]
  [DefaultValue(10)]
  public int Step
  {
    get => this._step;
    set
    {
      if (this._step == value)
        return;
      this._step = value;
      this.Invalidate();
    }
  }

  [Description("The border color of ColorProgressBar")]
  [Category("ColorProgressBar")]
  public Color BorderColor
  {
    get => this._borderColor;
    set
    {
      if (!(this._borderColor != value))
        return;
      this._borderColor = value;
      this.Invalidate();
    }
  }

  protected bool ShouldSerializeBorderColor() => this._borderColor != SystemColors.ControlDark;

  [Browsable(false)]
  public double Percent
  {
    get
    {
      double num = (double) (this._maxValue - this._minValue);
      return num != 0.0 ? (double) this._value / num : 0.0;
    }
  }

  private void OnChanged()
  {
    if (this.Changed == null)
      return;
    this.Changed((object) this, EventArgs.Empty);
  }

  public void PerformStep()
  {
    if (this._value < this._maxValue)
      this._value += this._step;
    else
      this._value = this._maxValue;
    this.Invalidate();
    this.OnChanged();
  }

  public void PerformStepBack()
  {
    if (this._value > this._minValue)
      this._value -= this._step;
    else
      this._value = this._minValue;
    this.Invalidate();
    this.OnChanged();
  }

  public void Increment(int value)
  {
    if (this._value < this._maxValue)
      this._value += value;
    else
      this._value = this._maxValue;
    this.Invalidate();
    this.OnChanged();
  }

  public void Decrement(int value)
  {
    if (this._value > this._minValue)
      this._value -= value;
    else
      this._value = this._minValue;
    this.Invalidate();
    this.OnChanged();
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    Color color1 = ControlPaint.Dark(this._barColor, this._darkPercent);
    if (!this.BackColor.Equals((object) Color.Transparent))
    {
      using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
        e.Graphics.FillRectangle((Brush) solidBrush, this.ClientRectangle);
    }
    if (this._maxValue == this._minValue || this._value == 0)
    {
      this.drawBorder(e.Graphics);
    }
    else
    {
      double num1 = (double) this._value / (double) (this._maxValue - this._minValue);
      int width = (int) ((double) this.Width * num1);
      if (width == 0)
      {
        this.drawBorder(e.Graphics);
      }
      else
      {
        RectangleF rect1 = new RectangleF(0.0f, 0.0f, (float) width, (float) this.Height);
        switch (this._gradientMode)
        {
          case ColorProgressBar.GradientModes.None:
            using (SolidBrush solidBrush = new SolidBrush(this._barColor))
            {
              e.Graphics.FillRectangle((Brush) solidBrush, new Rectangle(0, 0, width, this.Height));
              break;
            }
          case ColorProgressBar.GradientModes.Center:
            Rectangle rect2 = new Rectangle(0, 0, width, this.Height / 2);
            Rectangle rect3 = new Rectangle(0, this.Height / 2, width, this.Height / 2);
            LinearGradientBrush linearGradientBrush1 = new LinearGradientBrush(new Point(0, 0), new Point(0, this.Height / 2), color1, this._barColor);
            e.Graphics.FillRectangle((Brush) linearGradientBrush1, rect2);
            linearGradientBrush1.Dispose();
            LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(new Point(0, this.Height / 2 - 1), new Point(0, this.Height), this._barColor, color1);
            e.Graphics.FillRectangle((Brush) linearGradientBrush2, rect3);
            linearGradientBrush2.Dispose();
            break;
          case ColorProgressBar.GradientModes.Vertical:
            LinearGradientBrush linearGradientBrush3 = new LinearGradientBrush(rect1, this._barColor, color1, LinearGradientMode.Vertical);
            e.Graphics.FillRectangle((Brush) linearGradientBrush3, rect1);
            linearGradientBrush3.Dispose();
            break;
          case ColorProgressBar.GradientModes.Horizontal:
            LinearGradientBrush linearGradientBrush4 = new LinearGradientBrush(rect1, this._barColor, color1, LinearGradientMode.Horizontal);
            e.Graphics.FillRectangle((Brush) linearGradientBrush4, rect1);
            linearGradientBrush4.Dispose();
            break;
        }
        int num2 = (int) ((double) this.Height * 0.67);
        int num3 = width / num2;
        Color color2 = ControlPaint.LightLight(this._barColor);
        switch (this._fillStyle)
        {
          case ColorProgressBar.FillStyles.Dashed:
            using (Pen pen = new Pen(color2, 1f))
            {
              for (int index = 1; index <= num3; ++index)
                e.Graphics.DrawLine(pen, num2 * index, 0, num2 * index, this.Height);
              break;
            }
        }
        if (this._showPercent)
        {
          string s = $"{(int) (num1 * 100.0)} %";
          StringFormat format = new StringFormat();
          format.Alignment = StringAlignment.Center;
          format.LineAlignment = StringAlignment.Center;
          using (Brush brush = (Brush) new SolidBrush(this.ForeColor))
            e.Graphics.DrawString(s, this.Font, brush, (RectangleF) this.ClientRectangle, format);
        }
        this.drawBorder(e.Graphics);
      }
    }
  }

  protected void drawBorder(Graphics g)
  {
    Rectangle rect;
    ref Rectangle local = ref rect;
    Rectangle clientRectangle = this.ClientRectangle;
    int width = clientRectangle.Width - 1;
    clientRectangle = this.ClientRectangle;
    int height = clientRectangle.Height - 1;
    local = new Rectangle(0, 0, width, height);
    using (Pen pen = new Pen(this._borderColor, 1f))
      g.DrawRectangle(pen, rect);
  }

  public enum FillStyles
  {
    Solid,
    Dashed,
  }

  public enum GradientModes
  {
    None,
    Center,
    Vertical,
    Horizontal,
  }
}
