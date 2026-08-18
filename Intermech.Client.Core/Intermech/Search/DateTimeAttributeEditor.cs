
// Type: Intermech.Search.DateTimeAttributeEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search;

public sealed class DateTimeAttributeEditor : AttributeEditor
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private DateTimePicker _dateTimePicker;

  public DateTimeAttributeEditor() => this.InitializeComponent();

  protected override void DoSetValue()
  {
    if (this.Value != null)
    {
      if (!(this.Value is DateTime))
        throw new InvalidOperationException();
      if (this._dateTimePicker.ShowCheckBox)
        this._dateTimePicker.Checked = true;
      this._dateTimePicker.Value = (DateTime) this.Value;
    }
    else
      this._dateTimePicker.Checked = false;
  }

  protected override void DoInitializeEditor()
  {
    this._dateTimePicker.ShowCheckBox = this.AllowEmpty;
  }

  private void DateTimePicker_KeyUp(object sender, KeyEventArgs e) => this.HandleKeyUp(e.KeyCode);

  private void DateTimePicker_ValueChanged(object sender, EventArgs e)
  {
    if (!this._dateTimePicker.ShowCheckBox || this._dateTimePicker.Checked)
      this.SetValue((object) this._dateTimePicker.Value, false);
    else
      this.SetValue((object) null, false);
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
    this._dateTimePicker = new DateTimePicker();
    this.SuspendLayout();
    this._dateTimePicker.CustomFormat = "dd.MM.yyyy hh:mm:ss";
    this._dateTimePicker.Dock = DockStyle.Fill;
    this._dateTimePicker.Format = DateTimePickerFormat.Custom;
    this._dateTimePicker.Location = new Point(0, 0);
    this._dateTimePicker.Name = "_dateTimePicker";
    this._dateTimePicker.Size = new Size(287, 20);
    this._dateTimePicker.TabIndex = 0;
    this._dateTimePicker.ValueChanged += new EventHandler(this.DateTimePicker_ValueChanged);
    this._dateTimePicker.KeyUp += new KeyEventHandler(this.DateTimePicker_KeyUp);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._dateTimePicker);
    this.Name = nameof (DateTimeAttributeEditor);
    this.Size = new Size(287, 22);
    this.ResumeLayout(false);
  }
}
