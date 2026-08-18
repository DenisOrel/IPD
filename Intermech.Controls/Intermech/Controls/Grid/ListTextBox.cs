
// Type: Intermech.Controls.Grid.ListTextBox
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Windows.Forms;


namespace Intermech.Controls.Grid;

/// <summary>Summary description for GLTextBox.</summary>
internal class ListTextBox : TextBox, IEmbeddedControl
{
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  protected ListItem _item;
  protected ListSubItem _subItem;
  protected ListGrid _parent;

  public ListTextBox() => this.InitializeComponent();

  /// <summary>Clean up any resources being used.</summary>
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
    this.KeyPress += new KeyPressEventHandler(this.TextBox_KeyPress);
  }

  protected override void OnPaint(PaintEventArgs pe) => base.OnPaint(pe);

  protected override void OnGotFocus(EventArgs e) => base.OnGotFocus(e);

  protected override void OnLostFocus(EventArgs e) => base.OnLostFocus(e);

  public ListItem Item
  {
    get => this._item;
    set => this._item = value;
  }

  public ListSubItem SubItem
  {
    get => this._subItem;
    set => this._subItem = value;
  }

  public ListGrid ListControl
  {
    get => this._parent;
    set => this._parent = value;
  }

  public string ReturnText() => this.Text;

  public bool Load(ListItem item, ListSubItem subItem, ListGrid listctrl)
  {
    this.BorderStyle = BorderStyle.None;
    this.AutoSize = false;
    this._item = item;
    this._subItem = subItem;
    this._parent = listctrl;
    this.Text = subItem.Text;
    return true;
  }

  public void Unload() => this._subItem.Text = this.Text;

  private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
  {
  }
}
