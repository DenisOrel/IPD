
// Type: Intermech.Client.Core.Navigator.Controls.Windows.AutoCompleteTextBox
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Controls.Windows;

/// <summary>текст с массивом для поиска</summary>
public class AutoCompleteTextBox : TextBox
{
  private ListBox _listBox;
  private bool _isAdded;
  private string _formerValue = string.Empty;

  /// <summary>разделитель церочек поиска в строке</summary>
  public char Separator { get; set; } = ';';

  /// <summary>массив строк поиска</summary>
  public string[] Values { get; set; }

  public AutoCompleteTextBox()
  {
    this.InitializeAutoComplete();
    this.HideListBox();
  }

  private void InitializeAutoComplete()
  {
    this.SuspendLayout();
    ListBox listBox = new ListBox();
    listBox.Visible = false;
    listBox.Location = new Point(0, 0);
    listBox.Name = "_listBox";
    listBox.Size = new Size(120, 96 /*0x60*/);
    listBox.TabIndex = 0;
    this._listBox = listBox;
    this._listBox.MouseDoubleClick += new MouseEventHandler(this._listBox_MouseDoubleClick);
    this.KeyDown += new KeyEventHandler(this.TextBox_KeyDown);
    this.KeyUp += new KeyEventHandler(this.TextBox_KeyUp);
    this.PreviewKeyDown += new PreviewKeyDownEventHandler(this.TextBox_PreviewKeyDown);
  }

  private void TextBox_KeyUp(object sender, KeyEventArgs e) => this.UpdateListBox();

  private void TextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
  {
    if (e.KeyCode != Keys.Tab || !this._listBox.Visible)
      return;
    this.InsertWord((string) this._listBox.SelectedItem);
    this.HideListBox();
    this._formerValue = this.Text;
  }

  private void TextBox_KeyDown(object sender, KeyEventArgs e)
  {
    switch (e.KeyCode)
    {
      case Keys.Return:
        if (this._listBox.Visible)
        {
          this.HideListBox();
          this._formerValue = this.Text;
        }
        e.Handled = true;
        break;
      case Keys.Up:
        if (this._listBox.Visible && this._listBox.SelectedIndex > 0)
          --this._listBox.SelectedIndex;
        e.Handled = true;
        break;
      case Keys.Down:
        if (this._listBox.Visible && this._listBox.SelectedIndex < this._listBox.Items.Count - 1)
          ++this._listBox.SelectedIndex;
        e.Handled = true;
        break;
    }
  }

  private void _listBox_MouseDoubleClick(object sender, MouseEventArgs e)
  {
    if (!this._listBox.Visible)
      return;
    this.HideListBox();
    this.Text = (string) this._listBox.SelectedItem;
    this._formerValue = this.Text;
  }

  private void ShowListBox()
  {
    if (!this._isAdded)
    {
      this.Parent.Controls.Add((Control) this._listBox);
      this._listBox.Left = this.Left;
      this._listBox.Top = this.Top + this.Height;
      this._isAdded = true;
    }
    this._listBox.Visible = true;
    this._listBox.BringToFront();
  }

  private void HideListBox() => this._listBox.Visible = false;

  private void UpdateListBox()
  {
    if (this.Text == this._formerValue)
      return;
    this._formerValue = this.Text;
    string word = this.GetWord();
    if (this.Values != null && word.Length > 0)
    {
      object[] array = ((IEnumerable<string>) this.Values).Where<string>((Func<string, bool>) (x => x.ToUpper().Contains(word.ToUpper()) && !this.SelectedValues.Contains(x))).Select<string, object>((Func<string, object>) (x => (object) x)).ToArray<object>();
      if (array.Length != 0)
      {
        this.ShowListBox();
        this._listBox.Items.Clear();
        this._listBox.Items.AddRange(array);
        this._listBox.SelectedIndex = 0;
        this._listBox.Height = 0;
        this._listBox.Width = this.Width;
        this.Focus();
        using (Graphics graphics = this._listBox.CreateGraphics())
        {
          for (int index = 0; index < Math.Min(6, this._listBox.Items.Count); ++index)
            this._listBox.Height += this._listBox.GetItemHeight(index);
          for (int index = 0; index < this._listBox.Items.Count; ++index)
          {
            int width = (int) graphics.MeasureString((string) this._listBox.Items[index] + "_", this._listBox.Font).Width;
            this._listBox.Width = this._listBox.Width < width ? width : this._listBox.Width;
          }
        }
      }
      else
        this.HideListBox();
    }
    else
      this.HideListBox();
  }

  private string GetWord()
  {
    string text = this.Text;
    int selectionStart = this.SelectionStart;
    int num1 = text.LastIndexOf(this.Separator, selectionStart < 1 ? 0 : selectionStart - 1);
    int startIndex = num1 == -1 ? 0 : num1 + 1;
    int num2 = text.IndexOf(this.Separator, selectionStart);
    int num3 = num2 == -1 ? text.Length : num2;
    int length = num3 - startIndex < 0 ? 0 : num3 - startIndex;
    return text.Substring(startIndex, length);
  }

  private void InsertWord(string newTag)
  {
    string text = this.Text;
    int selectionStart = this.SelectionStart;
    int num = text.LastIndexOf(this.Separator, selectionStart < 1 ? 0 : selectionStart - 1);
    int length = num == -1 ? 0 : num + 1;
    int startIndex = text.IndexOf(this.Separator, selectionStart);
    string str = text.Substring(0, length) + newTag;
    this.Text = str + (startIndex == -1 ? "" : text.Substring(startIndex, text.Length - startIndex));
    this.SelectionStart = str.Length;
  }

  public List<string> SelectedValues
  {
    get
    {
      return new List<string>((IEnumerable<string>) this.Text.Split(new char[1]
      {
        this.Separator
      }, StringSplitOptions.RemoveEmptyEntries));
    }
  }
}
