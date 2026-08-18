
// Type: OfficePickers.ColorPicker.ComboBoxColorPicker
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Diagnostics;
using OfficePickers.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace OfficePickers.ColorPicker;

/// <summary>
/// Represents  an office ComboBox control that holds Color Picker control to select color from.
/// </summary>
[ToolboxBitmap(typeof (ComboBoxColorPicker), "ComboBoxColorPicker")]
[DefaultEvent("SelectedColorChanged")]
[DefaultProperty("Color")]
[ToolboxItem(true)]
[Description("Displays a list of colors in a drop down menu to select color from")]
public class ComboBoxColorPicker : ComboBox
{
  private HatchStyle? _hatch;
  /// <summary>
  /// The OfficeColorPicker control that the combobox should hold
  /// </summary>
  private OfficeColorPicker _colorPicker = new OfficeColorPicker();
  private const int WM_USER = 1024 /*0x0400*/;
  private const int WM_REFLECT = 8192 /*0x2000*/;
  private const int WM_COMMAND = 273;
  private const int CBN_DROPDOWN = 7;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Occurs when the value of the Color property changes.</summary>
  [Category("Behavior")]
  [Description("Occurs when the value of the Color property changes.")]
  public event EventHandler SelectedColorChanged;

  /// <summary>
  /// Gets or sets the selected color from the OfficeColorPicker
  /// </summary>
  public Color Color
  {
    get => this._colorPicker != null ? this._colorPicker.Color : Color.Empty;
    set
    {
      if (this._colorPicker != null)
        this._colorPicker.Color = value;
      this.Refresh();
    }
  }

  [CanBeNull]
  public HatchStyle? Hatch
  {
    get => this._hatch;
    set
    {
      HatchStyle? hatch = this._hatch;
      HatchStyle? nullable = value;
      if (hatch.GetValueOrDefault() == nullable.GetValueOrDefault() & hatch.HasValue == nullable.HasValue)
        return;
      this._hatch = value;
      this.Invalidate();
    }
  }

  /// <summary>
  /// Initialize a new instance of the
  /// ComboBoxColorPicker representing an office ComboBox control
  /// that holds color picker control to select color from.
  /// </summary>
  public ComboBoxColorPicker()
  {
    this.InitializeComponent();
    this.Items.Add((object) nameof (Color));
    this.SelectedIndex = 0;
    this.DrawMode = DrawMode.OwnerDrawFixed;
    this.DropDownStyle = ComboBoxStyle.DropDownList;
    this._colorPicker.SelectedColorChanged += new EventHandler(this.HandleSelectedColorChanged);
    this._colorPicker.BorderStyle = BorderStyle.None;
    this.SetStyle(ControlStyles.ResizeRedraw, true);
    this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
  }

  /// <summary>
  /// Initialize a new instance of the
  /// ComboBoxColorPicker representing an office ComboBox control
  /// that holds color picker control to select color from.
  /// </summary>
  /// <param name="startingColor">Starting color to the OfficeColorPicker control</param>
  public ComboBoxColorPicker(Color startingColor)
    : this()
  {
    this.Color = startingColor;
  }

  /// <summary>Fires the SelectedColorChanged event</summary>
  /// <param name="e"></param>
  public void OnSelectedColorChanged(EventArgs e)
  {
    if (this.SelectedColorChanged != null)
      this.SelectedColorChanged((object) this, e);
    this.Refresh();
  }

  /// <summary>
  /// Handles color changed - fires the SelectedColorChanged event.
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void HandleSelectedColorChanged(object sender, EventArgs e)
  {
    this.OnSelectedColorChanged(EventArgs.Empty);
  }

  /// <summary>Opens the drop down box with the OfficeColorPicker</summary>
  private void ShowDropDown()
  {
    if (this._colorPicker == null)
      return;
    this._colorPicker.Show((Control) this, 0, this.Height);
    this.BeginInvoke((Delegate) (() => Intermech.WindowsDll.User32.ReleaseCapture()));
  }

  /// <summary>
  /// Overrides, paint rectangle in the item regions instead of text
  /// </summary>
  /// <param name="e"></param>
  protected override void OnDrawItem(DrawItemEventArgs e)
  {
    if (e.Index <= -1 || !this.Enabled)
      return;
    if ((e.State & DrawItemState.Focus) == DrawItemState.None)
    {
      if (this._hatch.HasValue)
      {
        using (HatchBrush hatchBrush = new HatchBrush(this._hatch.Value, this.Enabled ? this.Color : Color.White, SystemColors.Window))
          e.Graphics.FillRectangle((Brush) hatchBrush, e.Bounds);
      }
      else
      {
        using (SolidBrush solidBrush = new SolidBrush(this.Enabled ? this.Color : Color.White))
          e.Graphics.FillRectangle((Brush) solidBrush, e.Bounds);
      }
      Rectangle rect = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);
      using (Pen pen = new Pen(CustomColors.ButtonBorder))
        e.Graphics.DrawRectangle(pen, rect);
    }
    else
    {
      Rectangle rect;
      ref Rectangle local = ref rect;
      int x = e.Bounds.X + 1;
      Rectangle bounds = e.Bounds;
      int y = bounds.Y + 1;
      bounds = e.Bounds;
      int width = bounds.Width - 3;
      bounds = e.Bounds;
      int height = bounds.Height - 3;
      local = new Rectangle(x, y, width, height);
      if (this._hatch.HasValue)
      {
        using (HatchBrush hatchBrush = new HatchBrush(this._hatch.Value, this.Enabled ? this.Color : Color.White, SystemColors.Window))
          e.Graphics.FillRectangle((Brush) hatchBrush, rect);
      }
      else
      {
        using (SolidBrush solidBrush = new SolidBrush(this.Enabled ? this.Color : Color.White))
          e.Graphics.FillRectangle((Brush) solidBrush, rect);
      }
      using (Pen pen = new Pen(CustomColors.ButtonBorder))
        e.Graphics.DrawRectangle(pen, rect);
      ControlPaint.DrawFocusRectangle(e.Graphics, e.Bounds);
    }
  }

  public static int HIWORD(int n) => n >> 16 /*0x10*/ & (int) ushort.MaxValue;

  protected override void WndProc(ref Message m)
  {
    if (m.Msg == 8465 && ComboBoxColorPicker.HIWORD((int) m.WParam) == 7)
      this.ShowDropDown();
    else
      base.WndProc(ref m);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.SuspendLayout();
    this.DropDownHeight = 1;
    this.DropDownWidth = 1;
    this.IntegralHeight = false;
    this.ItemHeight = 16 /*0x10*/;
    this.Size = new Size(90, 21);
    this.ResumeLayout(false);
  }
}
