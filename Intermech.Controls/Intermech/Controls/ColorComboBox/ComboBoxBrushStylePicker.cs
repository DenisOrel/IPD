
// Type: Intermech.Controls.ColorComboBox.ComboBoxBrushStylePicker
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Controls.ColorComboBox;

/// <summary>
/// Represents  an office ComboBox control that holds Color Picker control to select color from.
/// </summary>
[DefaultEvent("SelectedBrushStyleChanged")]
[ToolboxItem(true)]
[Description("Displays a list of brush styles in a drop down menu to select brush style from")]
public class ComboBoxBrushStylePicker : ComboBox
{
  /// <summary>
  /// The OfficeColorPicker control that the combobox should hold
  /// </summary>
  private BrushStylePicker _brushStylePicker = new BrushStylePicker();
  private const int WM_USER = 1024 /*0x0400*/;
  private const int WM_REFLECT = 8192 /*0x2000*/;
  private const int WM_COMMAND = 273;
  private const int CBN_DROPDOWN = 7;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Occurs when the value of the Color property changes.</summary>
  [Category("Behavior")]
  [Description("Occurs when the value of the brush style property changes.")]
  public event EventHandler SelectedBrushStyleChanged;

  /// <summary>
  /// Gets or sets the selected color from the OfficeColorPicker
  /// </summary>
  public Color Color
  {
    get => this._brushStylePicker != null ? this._brushStylePicker.Color : Color.Empty;
    set
    {
      if (this._brushStylePicker != null)
        this._brushStylePicker.Color = value;
      this.Refresh();
    }
  }

  /// <summary>
  /// Gets or sets the selected color from the OfficeColorPicker
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public BrushStyle SelectedBrushStyle
  {
    get
    {
      return this._brushStylePicker != null ? this._brushStylePicker.SelectedBrushStyle : (BrushStyle) null;
    }
    set
    {
      if (this._brushStylePicker != null)
        this._brushStylePicker.SelectedBrushStyle = value;
      this.Refresh();
    }
  }

  /// <summary>
  /// Initialize a new instance of the
  /// ComboBoxColorPicker representing an office ComboBox control
  /// that holds color picker control to select color from.
  /// </summary>
  public ComboBoxBrushStylePicker()
  {
    this.InitializeComponent();
    this.Items.Add((object) nameof (Color));
    this.SelectedIndex = 0;
    this.DrawMode = DrawMode.OwnerDrawFixed;
    this.DropDownStyle = ComboBoxStyle.DropDownList;
    this._brushStylePicker.SelectedBrushStyleChanged += new EventHandler(this.HandleSelectedBrushStyleChanged);
    this._brushStylePicker.BorderStyle = BorderStyle.None;
    this.SetStyle(ControlStyles.ResizeRedraw, true);
    this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
  }

  /// <summary>
  /// Initialize a new instance of the
  /// ComboBoxColorPicker representing an office ComboBox control
  /// that holds color picker control to select color from.
  /// </summary>
  /// <param name="startingColor">Starting color to the OfficeColorPicker control</param>
  public ComboBoxBrushStylePicker(BrushStyle brushStyle)
    : this()
  {
    this.SelectedBrushStyle = brushStyle;
  }

  /// <summary>Fires the SelectedColorChanged event</summary>
  /// <param name="e"></param>
  public void OnSelectedColorChanged(EventArgs e)
  {
    if (this.SelectedBrushStyleChanged != null)
      this.SelectedBrushStyleChanged((object) this, e);
    this.Refresh();
  }

  /// <summary>
  /// Handles color changed - fires the SelectedColorChanged event.
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void HandleSelectedBrushStyleChanged(object sender, EventArgs e)
  {
    this.OnSelectedColorChanged(EventArgs.Empty);
  }

  /// <summary>Opens the drop down box with the OfficeColorPicker</summary>
  private void ShowDropDown()
  {
    if (this._brushStylePicker == null)
      return;
    this._brushStylePicker.Show((Control) this, 0, this.Height);
  }

  /// <summary>
  /// Overrides, paint rectangle in the item regions instead of text
  /// </summary>
  /// <param name="e"></param>
  protected override void OnDrawItem(DrawItemEventArgs e)
  {
  }

  public static int HIWORD(int n) => n >> 16 /*0x10*/ & (int) ushort.MaxValue;

  protected override void WndProc(ref Message m)
  {
    if (m.Msg == 8465 && ComboBoxBrushStylePicker.HIWORD((int) m.WParam) == 7)
      this.ShowDropDown();
    else
      base.WndProc(ref m);
  }

  protected override void OnDropDown(EventArgs e)
  {
    base.OnDropDown(e);
    this._brushStylePicker.Focus();
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
    this.components = (IContainer) new System.ComponentModel.Container();
    this.SuspendLayout();
    this.DropDownHeight = 1;
    this.DropDownWidth = 1;
    this.IntegralHeight = false;
    this.ItemHeight = 16 /*0x10*/;
    this.Size = new Size(90, 21);
    this.ResumeLayout(false);
  }
}
