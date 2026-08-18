// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.GridFilters.DateGridFilterControl
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace GridViewExtensions.GridFilters;

public class DateGridFilterControl : UserControl
{
  private DateTimePicker _picker1;
  private DateTimePicker _picker2;
  private ComboBox _comboBox;
  private System.ComponentModel.Container components;

  public event EventHandler Changed;

  public DateGridFilterControl()
  {
    this.InitializeComponent();
    this._picker1.Format = DateTimePickerFormat.Short;
    this._picker2.Format = DateTimePickerFormat.Short;
    this._comboBox.SelectedIndex = 0;
    this.RefreshPickerWidth();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  protected override void OnResize(EventArgs e)
  {
    base.OnResize(e);
    this.RefreshPickerWidth();
  }

  private void InitializeComponent()
  {
    this._picker1 = new DateTimePicker();
    this._picker2 = new DateTimePicker();
    this._comboBox = new ComboBox();
    this.SuspendLayout();
    this._picker1.Checked = false;
    this._picker1.Dock = DockStyle.Fill;
    this._picker1.Location = new Point(40, 0);
    this._picker1.Name = "_picker1";
    this._picker1.Size = new Size(64 /*0x40*/, 20);
    this._picker1.TabIndex = 1;
    this._picker1.TextChanged += new EventHandler(this.OnChanged);
    this._picker1.KeyDown += new KeyEventHandler(this.OnKeyDown);
    this._picker1.KeyPress += new KeyPressEventHandler(this.OnKeyPress);
    this._picker1.KeyUp += new KeyEventHandler(this.OnKeyUp);
    this._picker2.Checked = false;
    this._picker2.Dock = DockStyle.Right;
    this._picker2.Location = new Point(104, 0);
    this._picker2.Name = "_picker2";
    this._picker2.Size = new Size(40, 20);
    this._picker2.TabIndex = 2;
    this._picker2.TextChanged += new EventHandler(this.OnChanged);
    this._picker2.KeyDown += new KeyEventHandler(this.OnKeyDown);
    this._picker2.KeyPress += new KeyPressEventHandler(this.OnKeyPress);
    this._picker2.KeyUp += new KeyEventHandler(this.OnKeyUp);
    this._comboBox.Dock = DockStyle.Left;
    this._comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._comboBox.Items.AddRange(new object[7]
    {
      (object) "",
      (object) "=",
      (object) "<>",
      (object) ">",
      (object) "<",
      (object) ">=",
      (object) "<="
    });
    this._comboBox.Location = new Point(0, 0);
    this._comboBox.Name = "_comboBox";
    this._comboBox.Size = new Size(40, 21);
    this._comboBox.TabIndex = 0;
    this._comboBox.SelectedIndexChanged += new EventHandler(this.OnChanged);
    this._comboBox.KeyDown += new KeyEventHandler(this.OnKeyDown);
    this._comboBox.KeyPress += new KeyPressEventHandler(this.OnKeyPress);
    this._comboBox.KeyUp += new KeyEventHandler(this.OnKeyUp);
    this.Controls.Add((Control) this._picker1);
    this.Controls.Add((Control) this._picker2);
    this.Controls.Add((Control) this._comboBox);
    this.Name = nameof (DateGridFilterControl);
    this.Size = new Size(144 /*0x90*/, 21);
    this.ResumeLayout(false);
  }

  public DateTimePicker DateTimePicker1 => this._picker1;

  public DateTimePicker DateTimePicker2 => this._picker2;

  public ComboBox ComboBox => this._comboBox;

  private void RefreshPickerWidth()
  {
    this._picker2.Width = (this.Width - this._comboBox.Width) / 2;
  }

  private void OnChanged(object sender, EventArgs e)
  {
    this._picker2.Visible = this._comboBox.Text == "<x<";
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, e);
  }

  private void OnKeyPress(object sender, KeyPressEventArgs e) => this.OnKeyPress(e);

  private void OnKeyUp(object sender, KeyEventArgs e) => this.OnKeyUp(e);

  private void OnKeyDown(object sender, KeyEventArgs e) => this.OnKeyDown(e);
}
