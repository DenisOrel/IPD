// Decompiled with JetBrains decompiler
// Type: Intermech.Controls.LoadingCircle
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Controls;

public class LoadingCircle : Control
{
  private const double NumberOfDegreesInCircle = 360.0;
  private const double NumberOfDegreesInHalfCircle = 180.0;
  private const int DefaultInnerCircleRadius = 8;
  private const int DefaultOuterCircleRadius = 10;
  private const int DefaultNumberOfSpoke = 10;
  private const int DefaultSpokeThickness = 4;
  private const int MacOSXInnerCircleRadius = 5;
  private const int MacOSXOuterCircleRadius = 11;
  private const int MacOSXNumberOfSpoke = 12;
  private const int MacOSXSpokeThickness = 2;
  private const int FireFoxInnerCircleRadius = 6;
  private const int FireFoxOuterCircleRadius = 7;
  private const int FireFoxNumberOfSpoke = 9;
  private const int FireFoxSpokeThickness = 4;
  private const int IE7InnerCircleRadius = 8;
  private const int IE7OuterCircleRadius = 9;
  private const int IE7NumberOfSpoke = 24;
  private const int IE7SpokeThickness = 4;
  private readonly Color _defaultColor = Color.DarkGray;
  [NotNull]
  private readonly Timer _timer;
  private bool _timerActive;
  private int _numberOfSpoke;
  private int _spokeThickness;
  private int _progressValue;
  private int? _outerCircleRadius;
  private int _innerCircleRadius;
  private PointF _centerPoint;
  private Color _color;
  [CanBeNull]
  private Color[] _colors;
  [CanBeNull]
  private double[] _angles;
  private LoadingCircle.StylePresets _stylePreset;
  private IContainer components;

  [TypeConverter("System.Drawing.ColorConverter")]
  [Category("LoadingCircle")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Description("Sets the color of spoke.")]
  public Color Color
  {
    get => this._color;
    set
    {
      this._color = value;
      this.GenerateColorsPallet();
      this.Invalidate();
    }
  }

  [Description("Gets or sets the radius of outer circle.")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Category("LoadingCircle")]
  public int OuterCircleRadius
  {
    get => (this._outerCircleRadius ?? (this._outerCircleRadius = new int?(10))).Value;
    set
    {
      this._outerCircleRadius = new int?(value);
      this._stylePreset = LoadingCircle.StylePresets.Custom;
      this.Invalidate();
    }
  }

  [Description("Gets or sets the radius of inner circle.")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Category("LoadingCircle")]
  public int InnerCircleRadius
  {
    get
    {
      if (this._innerCircleRadius == 0)
        this._innerCircleRadius = 8;
      return this._innerCircleRadius;
    }
    set
    {
      this._innerCircleRadius = value;
      this._stylePreset = LoadingCircle.StylePresets.Custom;
      this.Invalidate();
    }
  }

  [Description("Gets or sets the number of spoke.")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Category("LoadingCircle")]
  public int NumberSpoke
  {
    get
    {
      if (this._numberOfSpoke == 0)
        this._numberOfSpoke = 10;
      return this._numberOfSpoke;
    }
    set
    {
      if (this._numberOfSpoke == value || this._numberOfSpoke <= 0)
        return;
      this._numberOfSpoke = value;
      this._stylePreset = LoadingCircle.StylePresets.Custom;
      this.GenerateColorsPallet();
      this.GetSpokesAngles();
      this.Invalidate();
    }
  }

  [Description("Gets or sets the number of spoke.")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Category("LoadingCircle")]
  public bool Active
  {
    get => this._timerActive;
    set
    {
      this._timerActive = value;
      this.ActiveTimer();
    }
  }

  [Description("Gets or sets the thickness of a spoke.")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Category("LoadingCircle")]
  public int SpokeThickness
  {
    get
    {
      if (this._spokeThickness <= 0)
        this._spokeThickness = 4;
      return this._spokeThickness;
    }
    set
    {
      this._spokeThickness = value;
      this._stylePreset = LoadingCircle.StylePresets.Custom;
      this.Invalidate();
    }
  }

  [Description("Gets or sets the rotation speed. Higher the slower.")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Category("LoadingCircle")]
  public int RotationSpeed
  {
    get => this._timer.Interval;
    set
    {
      if (value <= 0)
        return;
      this._timer.Interval = value;
    }
  }

  [Category("LoadingCircle")]
  [Description("Quickly sets the style to one of these presets, or a custom style if desired")]
  [DefaultValue(typeof (LoadingCircle.StylePresets), "Custom")]
  public LoadingCircle.StylePresets StylePreset
  {
    get => this._stylePreset;
    set
    {
      this._stylePreset = value;
      switch (this._stylePreset)
      {
        case LoadingCircle.StylePresets.Custom:
          this.SetCircleAppearance(10, 4, 8, 10);
          break;
        case LoadingCircle.StylePresets.MacOSX:
          this.SetCircleAppearance(12, 2, 5, 11);
          break;
        case LoadingCircle.StylePresets.Firefox:
          this.SetCircleAppearance(9, 4, 6, 7);
          break;
        case LoadingCircle.StylePresets.IE7:
          this.SetCircleAppearance(24, 4, 8, 9);
          break;
      }
    }
  }

  public LoadingCircle()
  {
    this.SetStyle(ControlStyles.UserPaint, true);
    this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    this.SetStyle(ControlStyles.ResizeRedraw, true);
    this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
    this._color = this._defaultColor;
    this.GenerateColorsPallet();
    this.GetSpokesAngles();
    this.GetControlCenterPoint();
    this._timer = new Timer();
    this._timer.Tick += new EventHandler(this.aTimer_Tick);
    this.ActiveTimer();
    this.Resize += new EventHandler(this.LoadingCircle_Resize);
  }

  private void LoadingCircle_Resize([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.GetControlCenterPoint();
  }

  private void aTimer_Tick([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this._progressValue = ++this._progressValue % this._numberOfSpoke;
    this.Invalidate();
  }

  protected override void OnPaint([NotNull] PaintEventArgs e)
  {
    if (this._numberOfSpoke > 0)
    {
      e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
      int num = this._progressValue;
      for (int index1 = 0; index1 < this._numberOfSpoke; ++index1)
      {
        int index2 = num % this._numberOfSpoke;
        LoadingCircle.DrawLine(e.Graphics, LoadingCircle.GetCoordinate(this._centerPoint, this._innerCircleRadius, this._angles[index2]), LoadingCircle.GetCoordinate(this._centerPoint, this.OuterCircleRadius, this._angles[index2]), this._colors[index1], this._spokeThickness);
        num = index2 + 1;
      }
    }
    base.OnPaint(e);
  }

  public override Size GetPreferredSize(Size proposedSize)
  {
    proposedSize.Width = (this.OuterCircleRadius + this._spokeThickness) * 2;
    return proposedSize;
  }

  private static Color Darken(Color objColor, int intPercent)
  {
    int r = (int) objColor.R;
    int g = (int) objColor.G;
    int b = (int) objColor.B;
    return Color.FromArgb(intPercent, Math.Min(r, (int) byte.MaxValue), Math.Min(g, (int) byte.MaxValue), Math.Min(b, (int) byte.MaxValue));
  }

  private void GenerateColorsPallet()
  {
    this._colors = this.GenerateColorsPallet(this._color, this.Active, this._numberOfSpoke);
  }

  [NotNull]
  private Color[] GenerateColorsPallet(Color objColor, bool blnShadeColor, int intNbSpoke)
  {
    Color[] colorsPallet = new Color[this.NumberSpoke];
    byte num = (byte) ((int) byte.MaxValue / this.NumberSpoke);
    byte intPercent = 0;
    for (int index = 0; index < this.NumberSpoke; ++index)
    {
      if (blnShadeColor)
      {
        if (index == 0 || index < this.NumberSpoke - intNbSpoke)
        {
          colorsPallet[index] = objColor;
        }
        else
        {
          intPercent += num;
          if (intPercent > byte.MaxValue)
            intPercent = byte.MaxValue;
          colorsPallet[index] = LoadingCircle.Darken(objColor, (int) intPercent);
        }
      }
      else
        colorsPallet[index] = objColor;
    }
    return colorsPallet;
  }

  private void GetControlCenterPoint()
  {
    this._centerPoint = LoadingCircle.GetControlCenterPoint((Control) this);
  }

  private static PointF GetControlCenterPoint([NotNull] Control objControl)
  {
    return new PointF((float) (objControl.Width / 2), (float) (objControl.Height / 2 - 1));
  }

  private static void DrawLine(
    [NotNull] Graphics objGraphics,
    PointF objPointOne,
    PointF objPointTwo,
    Color objColor,
    [NonNegativeValue] int intLineThickness)
  {
    using (Pen pen = new Pen((Brush) new SolidBrush(objColor), (float) intLineThickness))
    {
      pen.StartCap = LineCap.Round;
      pen.EndCap = LineCap.Round;
      objGraphics.DrawLine(pen, objPointOne, objPointTwo);
    }
  }

  private static PointF GetCoordinate(PointF objCircleCenter, int intRadius, double dblAngle)
  {
    dblAngle = Math.PI * dblAngle / 180.0;
    return new PointF(objCircleCenter.X + (float) intRadius * (float) Math.Cos(dblAngle), objCircleCenter.Y + (float) intRadius * (float) Math.Sin(dblAngle));
  }

  private void GetSpokesAngles() => this._angles = LoadingCircle.GetSpokesAngles(this.NumberSpoke);

  [NotNull]
  private static double[] GetSpokesAngles(int intNumberSpoke)
  {
    double[] spokesAngles = new double[intNumberSpoke];
    double num = 360.0 / (double) intNumberSpoke;
    for (int index = 0; index < intNumberSpoke; ++index)
      spokesAngles[index] = index == 0 ? num : spokesAngles[index - 1] + num;
    return spokesAngles;
  }

  private void ActiveTimer()
  {
    if (this._timerActive)
    {
      this._timer.Start();
    }
    else
    {
      this._timer.Stop();
      this._progressValue = 0;
    }
    this.GenerateColorsPallet();
    this.Invalidate();
  }

  public void SetCircleAppearance(
    int numberSpoke,
    int spokeThickness,
    int innerCircleRadius,
    int outerCircleRadius)
  {
    this.NumberSpoke = numberSpoke;
    this.SpokeThickness = spokeThickness;
    this.InnerCircleRadius = innerCircleRadius;
    this.OuterCircleRadius = outerCircleRadius;
    this.Invalidate();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  public enum StylePresets
  {
    Custom,
    MacOSX,
    Firefox,
    IE7,
  }
}
