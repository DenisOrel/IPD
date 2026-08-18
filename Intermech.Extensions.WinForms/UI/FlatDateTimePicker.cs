// Decompiled with JetBrains decompiler
// Type: Intermech.UI.FlatDateTimePicker
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.WindowsDll;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.UI;

public class FlatDateTimePicker : DateTimePicker
{
  [CanBeNull]
  private User32.ComboBoxInfo _info;
  private int _dropDownButtonWidth = -1;
  private bool _droppedDown;
  private int _invalidateSince;
  private int _lastDropDownButtonWidth;
  private int _lastDropDownButtonHeight;
  private readonly Brush _brushWhiteSmoke = (Brush) new SolidBrush(Color.WhiteSmoke);
  private Brush _brushBackColor;
  private bool _hoover;
  private Rectangle _upButtonRect = Rectangle.Empty;
  private Rectangle _downButtonRect = Rectangle.Empty;
  private readonly GraphicsPath _arrowUpPath = new GraphicsPath();
  private readonly Brush _arrowBrush = (Brush) new SolidBrush(Color.Black);
  private Bitmap _arrowBitmap;
  private Graphics _arrowGraphics;

  [NotNull]
  protected User32.ComboBoxInfo Info
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._info ?? (this._info = this.GetInfo());
    }
  }

  private int DropDownButtonWidth
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._dropDownButtonWidth < 0 ? (this._dropDownButtonWidth = this.Info.rcButton.Width) : this._dropDownButtonWidth;
    }
  }

  public FlatDateTimePicker()
  {
    this.SetStyle(ControlStyles.DoubleBuffer, true);
    this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
  }

  protected override void OnValueChanged([NotNull] EventArgs eventArgs)
  {
    base.OnValueChanged(eventArgs);
    this.Invalidate();
  }

  protected override void WndProc(ref Message m)
  {
    switch (m.Msg)
    {
      case 15:
        base.WndProc(ref m);
        IntPtr dcThrowWinErrors1 = User32.GetWindowDC_ThrowWinErrors(m.HWnd);
        Graphics g1 = Graphics.FromHdc(dcThrowWinErrors1);
        this.OverrideDropDown(g1);
        this.OverrideControlBorder(g1);
        User32.ReleaseDC_ThrowWinErrors(m.HWnd, dcThrowWinErrors1);
        g1.Dispose();
        break;
      case 32 /*0x20*/:
        base.WndProc(ref m);
        if (!this._droppedDown || this._invalidateSince >= 3)
          break;
        this.Invalidate();
        ++this._invalidateSince;
        break;
      case 133:
        IntPtr dcThrowWinErrors2 = User32.GetWindowDC_ThrowWinErrors(m.HWnd);
        Graphics g2 = Graphics.FromHdc(dcThrowWinErrors2);
        User32.SendMessage(this.Handle, 792, dcThrowWinErrors2, IntPtr.Zero);
        User32.SendMessage(this.Handle, 15, IntPtr.Zero, IntPtr.Zero);
        this.OverrideControlBorder(g2);
        m.Result = (IntPtr) 1;
        User32.ReleaseDC_ThrowWinErrors(m.HWnd, dcThrowWinErrors2);
        g2.Dispose();
        break;
      default:
        base.WndProc(ref m);
        break;
    }
  }

  [NotNull]
  private Brush BrushBackColor
  {
    get => this._brushBackColor ?? (this._brushBackColor = (Brush) new SolidBrush(this.BackColor));
  }

  private bool Hoover
  {
    get => this._hoover;
    set
    {
      if (this._hoover == value)
        return;
      this._hoover = value;
      this.Invalidate();
    }
  }

  private void UpdateButtonImage(Rectangle rect)
  {
    if (this._arrowBitmap != null)
    {
      this._arrowBitmap.Dispose();
      if (this._arrowGraphics != null)
        this._arrowGraphics.Dispose();
    }
    if (rect.Width <= 0)
      rect.Width = 10;
    if (rect.Height <= 0)
      rect.Height = 10;
    this._arrowBitmap = new Bitmap(rect.Width, rect.Height, PixelFormat.Format24bppRgb);
    this._arrowBitmap.MakeTransparent();
    this._arrowGraphics = Graphics.FromImage((Image) this._arrowBitmap);
    PointF[] points = new PointF[3]
    {
      PointF.Empty,
      PointF.Empty,
      PointF.Empty
    };
    float num = (float) rect.Height / 2f;
    points[0].X = (float) ((double) rect.Width * 5.0 / 16.0);
    points[0].Y = num - 2f;
    points[1].X = (float) ((double) rect.Width * 8.0 / 16.0);
    points[1].Y = num + 2f;
    points[2].X = (float) ((double) rect.Width * 12.0 / 16.0);
    points[2].Y = num - 2f;
    this._arrowUpPath.Reset();
    this._arrowUpPath.AddLines(points);
    this._arrowGraphics.SmoothingMode = SmoothingMode.None;
    this._arrowGraphics.FillPath(this._arrowBrush, this._arrowUpPath);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this._brushWhiteSmoke != null)
        this._brushWhiteSmoke.Dispose();
      if (this._brushBackColor != null)
        this._brushBackColor.Dispose();
      if (this._arrowUpPath != null)
        this._arrowUpPath.Dispose();
      if (this._arrowBrush != null)
        this._arrowBrush.Dispose();
      if (this._arrowGraphics != null)
        this._arrowGraphics.Dispose();
      if (this._arrowBitmap != null)
        this._arrowBitmap.Dispose();
    }
    base.Dispose(disposing);
  }

  protected override void OnMouseEnter([NotNull] EventArgs e)
  {
    base.OnMouseEnter(e);
    this.Hoover = true;
  }

  protected override void OnMouseLeave([NotNull] EventArgs e)
  {
    base.OnMouseLeave(e);
    this.Hoover = false;
  }

  private void OverrideDropDown([NotNull] Graphics g)
  {
    if (this.ShowUpDown)
      return;
    Rectangle rect = new Rectangle(this.ClientRectangle.Width - this._dropDownButtonWidth, 0, this._dropDownButtonWidth, this.ClientRectangle.Height);
    if (this._lastDropDownButtonWidth != rect.Width || this._lastDropDownButtonHeight != rect.Height)
    {
      this.UpdateButtonImage(rect);
      this._lastDropDownButtonWidth = this._dropDownButtonWidth;
      this._lastDropDownButtonHeight = this.ClientRectangle.Height;
    }
    g.FillRectangle(this._hoover ? Intermech.Diagnostics.Check.NotNull<Brush>(this._brushWhiteSmoke, "_brushWhiteSmoke") : this.BrushBackColor, rect);
    g.DrawImage((Image) this._arrowBitmap, rect.Left, rect.Top);
  }

  private void OverrideControlBorder([NotNull] Graphics g)
  {
    ControlPaint.DrawBorder(g, new Rectangle(0, 0, this.Width, this.Height), SystemColors.ControlDark, ButtonBorderStyle.Solid);
  }

  protected override void OnDropDown([NotNull] EventArgs eventArgs)
  {
    this._invalidateSince = 0;
    this._droppedDown = true;
    base.OnDropDown(eventArgs);
  }

  protected override void OnCloseUp([NotNull] EventArgs eventArgs)
  {
    this._droppedDown = false;
    base.OnCloseUp(eventArgs);
  }

  protected override void OnResize([NotNull] EventArgs e)
  {
    base.OnResize(e);
    this.Invalidate();
  }
}
