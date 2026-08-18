
// Type: Intermech.Search.SingleValueFromListAttributeEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Search;

public sealed class SingleValueFromListAttributeEditor : AttributeEditor
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ComboBox _comboBox;

  public SingleValueFromListAttributeEditor()
  {
    this.InitializeComponent();
    this._comboBox.DisplayMember = "Item2";
    this._comboBox.ValueMember = "Item1";
  }

  protected override void DoSetValue()
  {
    this._comboBox.SelectedIndexChanged -= new EventHandler(this.ComboBox_SelectedIndexChanged);
    try
    {
      Tuple<object, string> tuple = this._comboBox.Items.Cast<Tuple<object, string>>().FirstOrDefault<Tuple<object, string>>((Func<Tuple<object, string>, bool>) (o => object.Equals(o.Item1, this.Value)));
      if (tuple == null)
        return;
      this._comboBox.SelectedIndex = this._comboBox.Items.IndexOf((object) tuple);
    }
    finally
    {
      this._comboBox.SelectedIndexChanged += new EventHandler(this.ComboBox_SelectedIndexChanged);
    }
  }

  protected override void DoInitializeEditor()
  {
    this._comboBox.BeginUpdate();
    try
    {
      this._comboBox.Items.Clear();
      if (this.AllowEmpty)
        this._comboBox.Items.Add((object) new Tuple<object, string>((object) null, string.Empty));
      if (this.AttributeType == null || this.AttributeType.PossibleValues == null)
        return;
      for (int index = 0; index < this.AttributeType.PossibleValues.Count; ++index)
      {
        string str = this.AttributeType.PossibleValuesDescriptions[index] == null || !(this.AttributeType.PossibleValuesDescriptions[index].ToString() != string.Empty) ? this.AttributeType.PossibleValues[index].ToString() : this.AttributeType.PossibleValuesDescriptions[index].ToString();
        this._comboBox.Items.Add((object) new Tuple<object, string>(this.AttributeType.PossibleValues[index], str));
      }
    }
    finally
    {
      this._comboBox.EndUpdate();
    }
  }

  private void ComboBox_KeyUp(object sender, KeyEventArgs e) => this.HandleKeyUp(e.KeyCode);

  private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._comboBox.SelectedIndex < 0)
      return;
    this.SetValue(((Tuple<object, string>) this._comboBox.Items[this._comboBox.SelectedIndex]).Item1, false);
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
    this._comboBox = new ComboBox();
    this.SuspendLayout();
    this._comboBox.Dock = DockStyle.Fill;
    this._comboBox.FormattingEnabled = true;
    this._comboBox.Location = new Point(0, 0);
    this._comboBox.Name = "_comboBox";
    this._comboBox.Size = new Size(240 /*0xF0*/, 21);
    this._comboBox.TabIndex = 0;
    this._comboBox.SelectedIndexChanged += new EventHandler(this.ComboBox_SelectedIndexChanged);
    this._comboBox.KeyUp += new KeyEventHandler(this.ComboBox_KeyUp);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._comboBox);
    this.Name = nameof (SingleValueFromListAttributeEditor);
    this.Size = new Size(240 /*0xF0*/, 23);
    this.ResumeLayout(false);
  }
}
