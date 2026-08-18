// Decompiled with JetBrains decompiler
// Type: Intermech.Controls.FlatComboBox
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.WindowsDll;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Controls;

[ToolboxBitmap(typeof (ComboBox))]
public class FlatComboBox : ComboBox
{
  [CanBeNull]
  private User32.ComboBoxInfo _info;
  private int _dropDownButtonWidth = -1;
  private static readonly IntPtr _nonZeroResult = new IntPtr(1);
  [CanBeNull]
  private Pen _enabledPen;
  private bool _graySelection;
  [NotNull]
  private readonly Pen _disabledPen = new Pen(SystemColors.Control, 2f);
  private int _lastDropDownButtonWidth;
  private int _lastDropDownButtonHeight;
  [NotNull]
  private readonly Brush _brushWhiteSmoke = (Brush) new SolidBrush(Color.WhiteSmoke);
  [CanBeNull]
  private Brush _brushBackColor;
  private bool _hoover;
  private Rectangle _upButtonRect = Rectangle.Empty;
  private Rectangle _downButtonRect = Rectangle.Empty;
  [NotNull]
  private readonly GraphicsPath _arrowUpPath = new GraphicsPath();
  [NotNull]
  private readonly Brush _arrowBrush = (Brush) new SolidBrush(Color.Black);
  [CanBeNull]
  private Bitmap _arrowBitmap;
  [CanBeNull]
  private Graphics _arrowGraphics;

  [NotNull]
  protected User32.ComboBoxInfo Info
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._info ?? (this._info = this.GetInfo());
    }
  }

  protected int DropDownButtonWidth
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._dropDownButtonWidth < 0 ? (this._dropDownButtonWidth = this.Info.rcButton.Width) : this._dropDownButtonWidth;
    }
  }

  public FlatComboBox()
  {
    this.SetStyle(ControlStyles.DoubleBuffer, true);
    this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    this.SetStyle(ControlStyles.UserPaint, true);
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    FlatComboBox.PaintFlatControlBorder((Control) this, e.Graphics);
    Rectangle clientRectangle = this.ClientRectangle;
    if (!this._graySelection)
    {
      clientRectangle.Inflate(-2, -2);
      clientRectangle.Width -= this.DropDownButtonWidth;
    }
    else
      clientRectangle.Inflate(-1, -1);
    this.OnDrawItem(this.Focused || this.Parent is ContainerControl parent && parent.ActiveControl == this ? new DrawItemEventArgs(e.Graphics, this.Font, clientRectangle, this.SelectedIndex, DrawItemState.ComboBoxEdit, SystemColors.HighlightText, SystemColors.Highlight) : new DrawItemEventArgs(e.Graphics, this.Font, clientRectangle, this.SelectedIndex, DrawItemState.ComboBoxEdit, this.ForeColor, this.BackColor));
    this.PaintFlatDropDown((Control) this, e.Graphics);
  }

  [NotNull]
  private Pen EnabledPen => this._enabledPen ?? (this._enabledPen = new Pen(this.BackColor, 2f));

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(false)]
  public bool GraySelection
  {
    get => this._graySelection;
    set
    {
      if (this._graySelection == value)
        return;
      this._graySelection = value;
      this.Invalidate();
    }
  }

  private static void PaintFlatControlBorder([NotNull] Control ctrl, [NotNull] Graphics g)
  {
    Rectangle bounds = new Rectangle(0, 0, ctrl.Width, ctrl.Height);
    ControlPaint.DrawBorder(g, bounds, SystemColors.ControlDark, ButtonBorderStyle.Solid);
  }

  [NotNull]
  private Brush BrushBackColor
  {
    get
    {
      return this._brushBackColor ?? (this._brushBackColor = (Brush) new SolidBrush(SystemColors.Window));
    }
  }

  protected bool Hoover
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

  protected override void OnDropDownClosed([NotNull] EventArgs e)
  {
    base.OnDropDownClosed(e);
    this.Hoover = this.Bounds.Contains(this.PointToClient(Cursor.Position));
  }

  public void PaintFlatDropDown([NotNull] Control ctrl, [NotNull] Graphics g)
  {
    Rectangle rect = new Rectangle(ctrl.Width - this.DropDownButtonWidth, 0, this.DropDownButtonWidth, ctrl.Height);
    if (this._lastDropDownButtonWidth != rect.Width || this._lastDropDownButtonHeight != rect.Height)
    {
      this.UpdateButtonImage(rect);
      this._lastDropDownButtonWidth = this.DropDownButtonWidth;
      this._lastDropDownButtonHeight = ctrl.Height;
    }
    if (this._arrowBitmap == null)
      return;
    g.DrawImageUnscaled((Image) this._arrowBitmap, rect.Left, rect.Top);
  }

  private void UpdateButtonImage(Rectangle rect)
  {
    if (this._arrowBitmap != null)
    {
      this._arrowBitmap.Dispose();
      this._arrowGraphics?.Dispose();
    }
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
      this._brushWhiteSmoke.Dispose();
      if (this._brushBackColor != null)
        this._brushBackColor.Dispose();
      if (this._enabledPen != null)
        this._enabledPen.Dispose();
      this._disabledPen.Dispose();
      this._arrowUpPath.Dispose();
      this._arrowBrush.Dispose();
      if (this._arrowGraphics != null)
        this._arrowGraphics.Dispose();
      if (this._arrowBitmap != null)
        this._arrowBitmap.Dispose();
    }
    base.Dispose(disposing);
  }

  protected override void OnResize([NotNull] EventArgs e)
  {
    base.OnResize(e);
    this.Invalidate();
  }
}
