
// Type: Intermech.Search.BooleanComboBox
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search;

public sealed class BooleanComboBox : SingleValueEditor<bool>
{
  private const string Yes = "Да";
  private const string No = "Нет";
  private static readonly string[] YesNo = new string[2]
  {
    "Да",
    "Нет"
  };
  private static readonly string[] EmptyYesNo = new string[3]
  {
    string.Empty,
    "Да",
    "Нет"
  };
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ComboBox _comboBox;

  public BooleanComboBox()
  {
    this.InitializeComponent();
    this.InitializeComboBox();
  }

  protected override void DoSetAllowEmpty() => this.InitializeComboBox();

  protected override void DoSetValue()
  {
    this._comboBox.SelectedIndexChanged -= new EventHandler(this.ComboBox_SelectedIndexChanged);
    try
    {
      if (!this.IsEmpty)
      {
        if (this.TypedValue)
          this._comboBox.SelectedIndex = this._comboBox.Items.IndexOf((object) "Да");
        else
          this._comboBox.SelectedIndex = this._comboBox.Items.IndexOf((object) "Нет");
      }
      else if (this.AllowEmpty)
        this._comboBox.SelectedIndex = this._comboBox.Items.IndexOf((object) string.Empty);
      else
        this._comboBox.SelectedIndex = this._comboBox.Items.IndexOf((object) "Нет");
    }
    finally
    {
      this._comboBox.SelectedIndexChanged += new EventHandler(this.ComboBox_SelectedIndexChanged);
    }
  }

  private void ComboBox_KeyUp(object sender, KeyEventArgs e) => this.HandleKeyUp(e.KeyCode);

  private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    string selectedItem = this._comboBox.SelectedItem as string;
    switch (selectedItem)
    {
      case "Да":
        this.SetValue((object) true, false);
        break;
      case "Нет":
        this.SetValue((object) false, false);
        break;
      default:
        if (string.IsNullOrEmpty(selectedItem))
        {
          this.SetValue((object) null, false);
          break;
        }
        this.SetValue((object) false, false);
        break;
    }
  }

  private void InitializeComboBox()
  {
    this._comboBox.BeginUpdate();
    try
    {
      this._comboBox.Items.Clear();
      this._comboBox.Items.AddRange(this.AllowEmpty ? (object[]) BooleanComboBox.EmptyYesNo : (object[]) BooleanComboBox.YesNo);
    }
    finally
    {
      this._comboBox.EndUpdate();
    }
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
    this._comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._comboBox.FormattingEnabled = true;
    this._comboBox.Location = new Point(0, 0);
    this._comboBox.Margin = new Padding(0);
    this._comboBox.Name = "_comboBox";
    this._comboBox.Size = new Size(200, 21);
    this._comboBox.TabIndex = 0;
    this._comboBox.SelectedIndexChanged += new EventHandler(this.ComboBox_SelectedIndexChanged);
    this._comboBox.KeyUp += new KeyEventHandler(this.ComboBox_KeyUp);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._comboBox);
    this.Name = nameof (BooleanComboBox);
    this.Size = new Size(200, 21);
    this.ResumeLayout(false);
  }
}
