// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.GridFilters.TextGridFilterControl
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace GridViewExtensions.GridFilters;

public class TextGridFilterControl : UserControl
{
  private ComboBox _comboBoxValue;
  private ComboBox _comboBoxCondition;
  private System.ComponentModel.Container components;

  public event EventHandler Changed;

  public TextGridFilterControl()
  {
    this.InitializeComponent();
    this._comboBoxCondition.SelectedIndex = 0;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this._comboBoxValue = new ComboBox();
    this._comboBoxCondition = new ComboBox();
    this.SuspendLayout();
    this._comboBoxValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._comboBoxValue.Location = new Point(31 /*0x1F*/, 0);
    this._comboBoxValue.Name = "_comboBoxValue";
    this._comboBoxValue.Size = new Size(93, 21);
    this._comboBoxValue.TabIndex = 2;
    this._comboBoxValue.TextChanged += new EventHandler(this.OnChanged);
    this._comboBoxValue.KeyDown += new KeyEventHandler(this.OnKeyDown);
    this._comboBoxValue.KeyPress += new KeyPressEventHandler(this.OnKeyPress);
    this._comboBoxValue.KeyUp += new KeyEventHandler(this.OnKeyUp);
    this._comboBoxCondition.Dock = DockStyle.Left;
    this._comboBoxCondition.DropDownStyle = ComboBoxStyle.DropDownList;
    this._comboBoxCondition.Items.AddRange(new object[2]
    {
      (object) "*",
      (object) "!*"
    });
    this._comboBoxCondition.Location = new Point(0, 0);
    this._comboBoxCondition.Name = "_comboBoxCondition";
    this._comboBoxCondition.Size = new Size(32 /*0x20*/, 21);
    this._comboBoxCondition.TabIndex = 0;
    this._comboBoxCondition.SelectedIndexChanged += new EventHandler(this.OnChanged);
    this._comboBoxCondition.KeyDown += new KeyEventHandler(this.OnKeyDown);
    this._comboBoxCondition.KeyPress += new KeyPressEventHandler(this.OnKeyPress);
    this._comboBoxCondition.KeyUp += new KeyEventHandler(this.OnKeyUp);
    this.Controls.Add((Control) this._comboBoxCondition);
    this.Controls.Add((Control) this._comboBoxValue);
    this.Name = nameof (TextGridFilterControl);
    this.Size = new Size(125, 21);
    this.ResumeLayout(false);
  }

  public ComboBox ComboBoxValue => this._comboBoxValue;

  public ComboBox ComboBoxCondition => this._comboBoxCondition;

  private void OnChanged(object sender, EventArgs e)
  {
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, e);
  }

  private void OnKeyPress(object sender, KeyPressEventArgs e) => this.OnKeyPress(e);

  private void OnKeyUp(object sender, KeyEventArgs e) => this.OnKeyUp(e);

  private void OnKeyDown(object sender, KeyEventArgs e) => this.OnKeyDown(e);
}
