
// Type: Intermech.Search.Box`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search;

public class Box<T> : SingleValueEditor<T>
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TextBoxWithCheckBoxAndButtons _textBoxWithCheckBoxAndButtons;

  public Box()
  {
    this.InitializeComponent();
    this.SuspendLayout();
    this._textBoxWithCheckBoxAndButtons.SuspendAllLayout();
    this._textBoxWithCheckBoxAndButtons.CheckBox.Visible = false;
    this._textBoxWithCheckBoxAndButtons.TextBox.BorderStyle = BorderStyle.None;
    this._textBoxWithCheckBoxAndButtons.TextBox.Multiline = true;
    this._textBoxWithCheckBoxAndButtons.TextBox.ReadOnly = !this.SupportedTextInput;
    this._textBoxWithCheckBoxAndButtons.TextBox.WordWrap = false;
    this._textBoxWithCheckBoxAndButtons.EditButton.FlatAppearance.BorderSize = 0;
    this._textBoxWithCheckBoxAndButtons.EditButton.FlatStyle = FlatStyle.Flat;
    this._textBoxWithCheckBoxAndButtons.EditButton.Visible = this.SupportedEditing;
    this._textBoxWithCheckBoxAndButtons.ClearButton.FlatAppearance.BorderSize = 0;
    this._textBoxWithCheckBoxAndButtons.ClearButton.FlatStyle = FlatStyle.Flat;
    this._textBoxWithCheckBoxAndButtons.ClearButton.Visible = this.SupportedClearing && this.AllowEmpty;
    this._textBoxWithCheckBoxAndButtons.TextBox.KeyDown += new KeyEventHandler(this.TextBox_KeyDown);
    this._textBoxWithCheckBoxAndButtons.TextBox.KeyUp += new KeyEventHandler(this.TextBox_KeyUp);
    this._textBoxWithCheckBoxAndButtons.TextBox.TextChanged += new EventHandler(this.TextBox_TextChanged);
    this._textBoxWithCheckBoxAndButtons.EditButton.Click += new EventHandler(this.EditButton_Click);
    this._textBoxWithCheckBoxAndButtons.ClearButton.Click += new EventHandler(this.ClearButton_Click);
    this._textBoxWithCheckBoxAndButtons.ResumeAllLayout(false);
    this.ResumeLayout(false);
    if (this.SupportedDeniedIncorrectInputTextBoxValidator)
    {
      DeniedIncorrectInputTextBoxValidator textBoxValidator = new DeniedIncorrectInputTextBoxValidator(this._textBoxWithCheckBoxAndButtons.TextBox, new Predicate<string>(this.IsValidPrefix), new Predicate<string>(this._IsValid));
    }
    this.UpdateControls();
  }

  protected virtual bool SupportedTextInput => true;

  protected virtual bool SupportedDeniedIncorrectInputTextBoxValidator => false;

  protected virtual bool SupportedEditing => true;

  protected virtual bool SupportedClearing => true;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int TextBoxMaxLength
  {
    get => this._textBoxWithCheckBoxAndButtons.TextBox.MaxLength;
    set => this._textBoxWithCheckBoxAndButtons.TextBox.MaxLength = value;
  }

  protected virtual void Edit()
  {
  }

  protected virtual bool IsValidPrefix(string text) => throw new NotImplementedException();

  protected virtual string GetTextBoxText()
  {
    return this.Value == null ? (string) null : this.Value.ToString();
  }

  protected virtual bool TryParse(string text, out T result) => throw new NotImplementedException();

  private void TextBox_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyData != Keys.Return)
      return;
    e.SuppressKeyPress = true;
  }

  private void TextBox_KeyUp(object sender, KeyEventArgs e) => this.HandleKeyUp(e.KeyCode);

  private void TextBox_TextChanged(object sender, EventArgs e)
  {
    if (this.AllowEmpty && string.IsNullOrEmpty(this._textBoxWithCheckBoxAndButtons.TextBox.Text))
    {
      this.SetValue((object) null, false);
    }
    else
    {
      T result = this.DefaultValue;
      this.TryParse(this._textBoxWithCheckBoxAndButtons.TextBox.Text, out result);
      this.SetValue((object) result, false);
    }
  }

  private void EditButton_Click(object sender, EventArgs e)
  {
    this.Edit();
    this._textBoxWithCheckBoxAndButtons.ActiveControl = (Control) this._textBoxWithCheckBoxAndButtons.TextBox;
  }

  private void ClearButton_Click(object sender, EventArgs e)
  {
    this.SetValue((object) null, true);
    this._textBoxWithCheckBoxAndButtons.ActiveControl = (Control) this._textBoxWithCheckBoxAndButtons.TextBox;
  }

  protected override void DoSetAllowEmpty()
  {
    this._textBoxWithCheckBoxAndButtons.ClearButton.Visible = this.SupportedClearing && this.AllowEmpty;
  }

  protected override void DoSetValue()
  {
    this._textBoxWithCheckBoxAndButtons.TextBox.TextChanged -= new EventHandler(this.TextBox_TextChanged);
    try
    {
      this._textBoxWithCheckBoxAndButtons.TextBox.Text = this.GetTextBoxText();
    }
    finally
    {
      this._textBoxWithCheckBoxAndButtons.TextBox.TextChanged += new EventHandler(this.TextBox_TextChanged);
    }
    this.UpdateControls();
  }

  protected override bool DoValidate()
  {
    return !this.SupportedTextInput || this._IsValid(this._textBoxWithCheckBoxAndButtons.TextBox.Text);
  }

  public override void SetFocus()
  {
    this._textBoxWithCheckBoxAndButtons.ActiveControl = (Control) this._textBoxWithCheckBoxAndButtons.TextBox;
  }

  private bool _IsValid(string text)
  {
    if (this.AllowEmpty && string.IsNullOrEmpty(text))
      return true;
    T result = this.DefaultValue;
    return this.TryParse(text, out result);
  }

  private void UpdateControls()
  {
    this._textBoxWithCheckBoxAndButtons.ClearButton.Enabled = !this.IsEmpty;
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
    this._textBoxWithCheckBoxAndButtons = new TextBoxWithCheckBoxAndButtons();
    this.SuspendLayout();
    this._textBoxWithCheckBoxAndButtons.Dock = DockStyle.Fill;
    this._textBoxWithCheckBoxAndButtons.Location = new Point(0, 0);
    this._textBoxWithCheckBoxAndButtons.Name = "_textBoxWithCheckBoxAndButtons";
    this._textBoxWithCheckBoxAndButtons.Size = new Size(200, 20);
    this._textBoxWithCheckBoxAndButtons.TabIndex = 0;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._textBoxWithCheckBoxAndButtons);
    this.Name = nameof (Box<T>);
    this.Size = new Size(200, 20);
    this.ResumeLayout(false);
  }
}
