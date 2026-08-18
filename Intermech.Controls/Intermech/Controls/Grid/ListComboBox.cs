
// Type: Intermech.Controls.Grid.ListComboBox
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Windows.Forms;


namespace Intermech.Controls.Grid;

internal class ListComboBox : ComboBox, IEmbeddedControl
{
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  protected ListItem _item;
  protected ListSubItem _subItem;
  protected ListGrid _parent;

  public ListComboBox() => this.InitializeComponent();

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent() => this.components = new System.ComponentModel.Container();

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
    this._item = item;
    this._subItem = subItem;
    this._parent = listctrl;
    this.Text = subItem.Text;
    return true;
  }

  public void Unload() => this._subItem.Text = this.Text;
}
