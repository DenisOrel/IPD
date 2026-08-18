
// Type: Intermech.Search.DeniedIncorrectInputTextBoxValidator
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Windows.Forms;


namespace Intermech.Search;

public sealed class DeniedIncorrectInputTextBoxValidator : TextBoxValidator
{
  private Predicate<string> _isValidPrefix;
  private Predicate<string> _isValid;
  private string _text;
  private int _selectionStart;
  private int _selectionLength;

  public DeniedIncorrectInputTextBoxValidator(
    TextBox textBox,
    Predicate<string> isValidPrefix,
    Predicate<string> isValid)
    : base(textBox)
  {
    if (isValidPrefix == null)
      throw new ArgumentNullException(nameof (isValidPrefix));
    if (isValid == null)
      throw new ArgumentNullException(nameof (isValid));
    this._isValidPrefix = isValidPrefix;
    this._isValid = isValid;
    this.TextBox.KeyPress += new KeyPressEventHandler(this.TextBox_KeyPress);
    this.TextBox.MouseClick += new MouseEventHandler(this.TextBox_MouseClick);
    this.TextBox.TextChanged += new EventHandler(this.TextBox_TextChanged);
  }

  private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
  {
    if (!char.IsControl(e.KeyChar))
    {
      string str1 = this.TextBox.Text ?? string.Empty;
      if (this.TextBox.SelectionLength != 0)
        str1 = str1.Remove(this.TextBox.SelectionStart, this.TextBox.SelectionLength);
      string str2 = str1.Insert(this.TextBox.SelectionStart, e.KeyChar.ToString());
      if (!string.IsNullOrEmpty(str2) && !this._isValidPrefix(str2) && !this._isValid(str2))
        e.Handled = true;
    }
    this.SaveTextBoxState();
  }

  private void TextBox_MouseClick(object sender, MouseEventArgs e) => this.SaveTextBoxState();

  private void TextBox_TextChanged(object sender, EventArgs e)
  {
    if (!string.IsNullOrEmpty(this.TextBox.Text) && !this._isValidPrefix(this.TextBox.Text) && !this._isValid(this.TextBox.Text))
      this.RestoreTextBoxState();
    this.SaveTextBoxState();
  }

  private void SaveTextBoxState()
  {
    this._text = this.TextBox.Text;
    this._selectionStart = this.TextBox.SelectionStart;
    this._selectionLength = this.TextBox.SelectionLength;
  }

  private void RestoreTextBoxState()
  {
    this.TextBox.TextChanged -= new EventHandler(this.TextBox_TextChanged);
    try
    {
      this.TextBox.Text = this._text;
      this.TextBox.SelectionStart = this._selectionStart;
      this.TextBox.SelectionLength = this._selectionLength;
    }
    finally
    {
      this.TextBox.TextChanged += new EventHandler(this.TextBox_TextChanged);
    }
  }
}
