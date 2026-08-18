// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.GridFilters.NumericGridFilterControl
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace GridViewExtensions.GridFilters;

public class NumericGridFilterControl : UserControl
{
  private TextBox _textBox1;
  private TextBox _textBox2;
  private ComboBox _comboBox;
  private ComboBox _comboBox1;
  private System.ComponentModel.Container components;

  public event EventHandler Changed;

  public NumericGridFilterControl()
  {
    this.InitializeComponent();
    this._comboBox.SelectedIndex = 1;
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
    this.RefreshTextBoxWidth();
  }

  private void InitializeComponent()
  {
    this._textBox1 = new TextBox();
    this._textBox2 = new TextBox();
    this._comboBox = new ComboBox();
    this._comboBox1 = new ComboBox();
    this.SuspendLayout();
    this._textBox1.Dock = DockStyle.Fill;
    this._textBox1.Location = new Point(40, 0);
    this._textBox1.Name = "_textBox1";
    this._textBox1.Size = new Size(0, 20);
    this._textBox1.TabIndex = 1;
    this._textBox1.TextChanged += new EventHandler(this.OnChanged);
    this._textBox1.KeyDown += new KeyEventHandler(this.OnKeyDown);
    this._textBox1.KeyPress += new KeyPressEventHandler(this.OnKeyPress);
    this._textBox1.KeyUp += new KeyEventHandler(this.OnKeyUp);
    this._textBox2.Dock = DockStyle.Right;
    this._textBox2.Location = new Point(40, 0);
    this._textBox2.Name = "_textBox2";
    this._textBox2.Size = new Size(104, 20);
    this._textBox2.TabIndex = 2;
    this._textBox2.TextChanged += new EventHandler(this.OnChanged);
    this._textBox2.KeyDown += new KeyEventHandler(this.OnKeyDown);
    this._textBox2.KeyPress += new KeyPressEventHandler(this.OnKeyPress);
    this._textBox2.KeyUp += new KeyEventHandler(this.OnKeyUp);
    this._comboBox.Dock = DockStyle.Left;
    this._comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._comboBox.Items.AddRange(new object[7]
    {
      (object) "*",
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
    this._comboBox1.Dock = DockStyle.Fill;
    this._comboBox1.FormattingEnabled = true;
    this._comboBox1.Location = new Point(40, 0);
    this._comboBox1.Name = "_comboBox1";
    this._comboBox1.Size = new Size(0, 21);
    this._comboBox1.TabIndex = 3;
    this._comboBox1.Visible = false;
    this._comboBox1.TextChanged += new EventHandler(this.OnChanged);
    this.Controls.Add((Control) this._comboBox1);
    this.Controls.Add((Control) this._textBox1);
    this.Controls.Add((Control) this._textBox2);
    this.Controls.Add((Control) this._comboBox);
    this.Name = nameof (NumericGridFilterControl);
    this.Size = new Size(144 /*0x90*/, 21);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  public TextBox TextBox1 => this._textBox1;

  public ComboBox ComboBox1 => this._comboBox1;

  public TextBox TextBox2 => this._textBox2;

  public ComboBox ComboBox => this._comboBox;

  private void RefreshTextBoxWidth()
  {
    this._textBox2.Width = (this.Width - this._comboBox.Width) / 2;
  }

  private void OnChanged(object sender, EventArgs e)
  {
    this._textBox2.Visible = this._comboBox.Text == "<x<";
    this._textBox1.Visible = this._textBox2.Visible;
    this._comboBox1.Visible = !this._textBox1.Visible;
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, e);
  }

  private void OnKeyPress(object sender, KeyPressEventArgs e) => this.OnKeyPress(e);

  private void OnKeyUp(object sender, KeyEventArgs e) => this.OnKeyUp(e);

  private void OnKeyDown(object sender, KeyEventArgs e) => this.OnKeyDown(e);
}
