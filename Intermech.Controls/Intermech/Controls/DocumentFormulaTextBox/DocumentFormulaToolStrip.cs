
// Type: Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaToolStrip
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Interfaces.Client;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Controls.DocumentFormulaTextBox;

internal class DocumentFormulaToolStrip : ContextMenuStrip
{
  private TextBoxBase _txt;
  /// <summary>
  /// Признак, что был клик правой клавишей перед открытием контекстного меню
  /// </summary>
  private bool _rightButtonClick;
  /// <summary>Позиция клика правой клавишей мыши</summary>
  private int _position = -1;
  private string _currText = string.Empty;
  private ToolStripMenuItem _cmiUndo;
  private ToolStripSeparator toolStripSeparator3;
  private ToolStripMenuItem _cmiCut;
  private ToolStripMenuItem _cmiCopy;
  private ToolStripMenuItem _cmiPaste;
  private ToolStripMenuItem _cmiDelete;
  private ToolStripSeparator toolStripSeparator1;
  private ToolStripMenuItem _cmiSelectAll;
  private ToolStripSeparator toolStripSeparator2;
  private ToolStripMenuItem _cmiPasteSymbol;
  private ToolStripMenuItem _cmiEditSymbol;
  private ToolStripMenuItem _cmiDelSymbol;

  public DocumentFormulaToolStrip(TextBoxBase textBox)
  {
    this._cmiUndo = new ToolStripMenuItem();
    this.toolStripSeparator3 = new ToolStripSeparator();
    this._cmiCut = new ToolStripMenuItem();
    this._cmiCopy = new ToolStripMenuItem();
    this._cmiPaste = new ToolStripMenuItem();
    this._cmiDelete = new ToolStripMenuItem();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this._cmiSelectAll = new ToolStripMenuItem();
    this.toolStripSeparator2 = new ToolStripSeparator();
    this._cmiPasteSymbol = new ToolStripMenuItem();
    this._cmiEditSymbol = new ToolStripMenuItem();
    this._cmiDelSymbol = new ToolStripMenuItem();
    this.Items.AddRange(new ToolStripItem[10]
    {
      (ToolStripItem) this._cmiCut,
      (ToolStripItem) this._cmiCopy,
      (ToolStripItem) this._cmiPaste,
      (ToolStripItem) this._cmiDelete,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this._cmiSelectAll,
      (ToolStripItem) this.toolStripSeparator2,
      (ToolStripItem) this._cmiPasteSymbol,
      (ToolStripItem) this._cmiEditSymbol,
      (ToolStripItem) this._cmiDelSymbol
    });
    this._cmiCut.Text = "Вырезать";
    this._cmiCut.Tag = (object) "1";
    this._cmiCopy.Text = "Копировать";
    this._cmiCopy.Tag = (object) "2";
    this._cmiPaste.Text = "Вставить";
    this._cmiPaste.Tag = (object) "3";
    this._cmiDelete.Text = "Удалить";
    this._cmiDelete.Tag = (object) "4";
    this._cmiSelectAll.Text = "Выделить все";
    this._cmiSelectAll.Tag = (object) "5";
    this._cmiPasteSymbol.Text = "Вставить специальный символ";
    this._cmiPasteSymbol.Tag = (object) "6";
    this._cmiEditSymbol.Text = "Редактировать специальный символ";
    this._cmiEditSymbol.Tag = (object) "7";
    this._cmiDelSymbol.Text = "Удалить специальный символ";
    this._cmiDelSymbol.Tag = (object) "8";
    this._cmiUndo.Click += new EventHandler(this.On_cm_MenuItem_Click);
    this._cmiCut.Click += new EventHandler(this.On_cm_MenuItem_Click);
    this._cmiCopy.Click += new EventHandler(this.On_cm_MenuItem_Click);
    this._cmiPaste.Click += new EventHandler(this.On_cm_MenuItem_Click);
    this._cmiDelete.Click += new EventHandler(this.On_cm_MenuItem_Click);
    this._cmiSelectAll.Click += new EventHandler(this.On_cm_MenuItem_Click);
    this._cmiPasteSymbol.Click += new EventHandler(this.On_cm_MenuItem_Click);
    this._cmiEditSymbol.Click += new EventHandler(this.On_cm_MenuItem_Click);
    this._cmiDelSymbol.Click += new EventHandler(this.On_cm_MenuItem_Click);
    this._txt = textBox;
    this._txt.MouseDown += new MouseEventHandler(this._txt_MouseDown);
  }

  private void _txt_MouseDown(object sender, MouseEventArgs e)
  {
    if (e.Button != MouseButtons.Right)
      return;
    this._rightButtonClick = true;
    Point location = e.Location;
    int indexFromPosition = this._txt.GetCharIndexFromPosition(location);
    Point positionFromCharIndex = this._txt.GetPositionFromCharIndex(indexFromPosition);
    int num1;
    if (location.X <= positionFromCharIndex.X)
    {
      num1 = indexFromPosition;
    }
    else
    {
      int num2 = num1 = indexFromPosition + 1;
    }
    this._position = num1;
  }

  protected override void OnOpening(CancelEventArgs e)
  {
    if (this._txt.Enabled)
    {
      this._cmiUndo.Enabled = this._txt.Text != this._currText;
      this._cmiCut.Enabled = this._cmiCopy.Enabled = this._cmiDelete.Enabled = this._txt.SelectionLength > 0;
      this._cmiPaste.Enabled = Clipboard.ContainsText();
      int startIndex = -1;
      int finishIndex = -1;
      int nPos = this._txt.SelectionStart;
      if (this._rightButtonClick)
      {
        this._rightButtonClick = false;
        nPos = this._position;
      }
      else
        this._position = nPos;
      this._cmiEditSymbol.Enabled = this._cmiDelSymbol.Enabled = !string.IsNullOrEmpty(this.GetSymbol(nPos, ref startIndex, ref finishIndex));
    }
    else
    {
      this._cmiUndo.Enabled = false;
      e.Cancel = true;
    }
  }

  private void On_cm_MenuItem_Click(object sender, EventArgs e)
  {
    string str1;
    switch (Convert.ToInt16((sender as ToolStripMenuItem).Tag))
    {
      case 0:
        this._txt.Text = this._currText;
        this._txt.SelectionStart = this._txt.Text.Length;
        break;
      case 1:
        this._currText = this._txt.Text;
        Clipboard.SetText(this._txt.SelectedText);
        int selectionStart1 = this._txt.SelectionStart;
        this._txt.Text = this._txt.Text.Remove(selectionStart1, this._txt.SelectionLength);
        this._txt.SelectionStart = selectionStart1;
        break;
      case 2:
        Clipboard.SetText(this._txt.SelectedText);
        break;
      case 3:
        this._currText = this._txt.Text;
        string text1 = Clipboard.GetText();
        int selectionStart2 = this._txt.SelectionStart;
        this._txt.Text = this._txt.SelectionLength > 0 ? this._txt.Text.Replace(this._txt.SelectedText, text1) : this._txt.Text.Insert(selectionStart2, text1);
        this._txt.SelectionStart = selectionStart2 + text1.Length;
        break;
      case 4:
        this._currText = this._txt.Text;
        int selectionStart3 = this._txt.SelectionStart;
        this._txt.Text = this._txt.Text.Remove(selectionStart3, this._txt.SelectionLength);
        this._txt.SelectionStart = selectionStart3;
        break;
      case 5:
        this._txt.SelectionStart = 0;
        this._txt.SelectionLength = this._txt.Text.Length;
        break;
      case 6:
      case 7:
        this._currText = this._txt.Text;
        int startIndex1 = -1;
        int finishIndex1 = -1;
        str1 = string.Empty;
        if (!(ServicesManager.GetService(typeof (IIMDocumentEditorService)) is IIMDocumentEditorService service))
          break;
        string symbol = this.GetSymbol(this._position, ref startIndex1, ref finishIndex1);
        if (!service.CallDocumentFormulaEditor(ref symbol) || string.IsNullOrEmpty(symbol))
          break;
        string text2 = this._txt.Text;
        int startIndex2;
        string str2;
        if (startIndex1 > -1 && startIndex1 < finishIndex1)
        {
          string str3 = text2.Remove(startIndex1, finishIndex1 - startIndex1);
          startIndex2 = startIndex1 < this._txt.Text.Length ? startIndex1 : (this._txt.Text.Length > 0 ? this._txt.Text.Length - 1 : 0);
          str2 = str3.Insert(startIndex2, symbol);
        }
        else
        {
          startIndex2 = this._txt.SelectionStart;
          str2 = !string.IsNullOrEmpty(this._txt.SelectedText) ? text2.Replace(this._txt.SelectedText, symbol) : text2.Insert(startIndex2, symbol);
        }
        this._txt.Text = str2;
        this._txt.SelectionStart = startIndex2 + symbol.Length;
        break;
      case 8:
        int startIndex3 = -1;
        int finishIndex2 = -1;
        str1 = this.GetSymbol(this._position, ref startIndex3, ref finishIndex2);
        if (startIndex3 <= -1 || startIndex3 >= finishIndex2)
          break;
        this._txt.Text = this._txt.Text.Remove(startIndex3, finishIndex2 - startIndex3);
        this._txt.SelectionStart = startIndex3 < this._txt.Text.Length ? startIndex3 : (this._txt.Text.Length > 0 ? this._txt.Text.Length - 1 : 0);
        break;
    }
  }

  private string GetSymbol(int nPos, ref int startIndex, ref int finishIndex)
  {
    string symbol = string.Empty;
    if (!string.IsNullOrEmpty(this._txt.Text))
    {
      startIndex = finishIndex = -1;
      string text = this._txt.Text;
      int startIndex1 = text.LastIndexOf("<<", nPos, nPos + 1);
      if (startIndex1 > -1 && text.LastIndexOf(">>", nPos, nPos) < startIndex1)
      {
        int num1 = text.IndexOf(">>", nPos);
        if (num1 > -1)
        {
          int num2 = text.IndexOf("<<", nPos);
          if (num2 == -1 || num2 > num1)
          {
            int num3 = num1 + 2;
            symbol = text.Substring(startIndex1, num3 - startIndex1);
            startIndex = startIndex1;
            finishIndex = num3;
          }
        }
      }
    }
    return symbol;
  }
}
