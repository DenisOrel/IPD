
// Type: Intermech.Controls.Grid.ListItem
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;


namespace Intermech.Controls.Grid;

/// <summary>GLItem which corresponds to rows in the list view</summary>
[DesignTimeVisible(true)]
[TypeConverter("Intermech.Controls.Grid.ListItemConverter")]
public class ListItem
{
  private ListSubItemCollection _subItems;
  private bool _selected;
  private object _tag;
  private Color _foreColor = Color.Black;
  private ListGrid _parent;
  private Color _rowBorderColor = Color.Black;
  private Color _backColor = Color.White;
  private int _rowBorderSize;

  public event ChangedEventHandler Changed;

  public void SubItemCollection_Changed(object source, ChangedEventArgs e)
  {
    if (this.Changed == null)
      return;
    e.Item = this;
    this.Changed((object) this, e);
  }

  /// <summary>Constructor</summary>
  public ListItem()
  {
    this._subItems = this.Parent == null ? new ListSubItemCollection() : new ListSubItemCollection(this.Parent);
    this.SubItems.Changed += new ChangedEventHandler(this.SubItemCollection_Changed);
  }

  /// <summary>Constructor</summary>
  /// <param name="parent"></param>
  public ListItem(ListGrid parent)
  {
    this._subItems = new ListSubItemCollection(parent);
    this._subItems.Parent = parent;
    this.Parent = parent;
    this.SubItems.Changed += new ChangedEventHandler(this.SubItemCollection_Changed);
  }

  /// <summary>row border size</summary>
  [Description("Size of a border on each row.")]
  [Category("Behavior")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Browsable(true)]
  [DefaultValue(0)]
  public int RowBorderSize
  {
    get => this._rowBorderSize;
    set => this._rowBorderSize = value;
  }

  /// <summary>Text color for item</summary>
  [Category("Behavior")]
  [Description("Sub Items")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [Editor(typeof (CollectionEditor), typeof (UITypeEditor))]
  [Browsable(true)]
  public ListSubItemCollection SubItems => this._subItems;

  /// <summary>Row border color</summary>
  [Description("Set the back color for an entire row.")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Browsable(true)]
  public Color BackColor
  {
    get => this._backColor;
    set => this._backColor = value;
  }

  /// <summary>Row border color</summary>
  [Description("If you have row border size set to something other than 0 then it will take on this color.")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Browsable(true)]
  public Color RowBorderColor
  {
    get => this._rowBorderColor;
    set => this._rowBorderColor = value;
  }

  /// <summary>Text for cell 0 (added by popular request)</summary>
  public string Text
  {
    get => this.SubItems[0].Text;
    set => this.SubItems[0].Text = value;
  }

  /// <summary>User defineable object</summary>
  [Description("Extra user information.")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public object Tag
  {
    get => this._tag;
    set => this._tag = value;
  }

  /// <summary>Text color for item</summary>
  [Description("Text Color override for item.")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Browsable(true)]
  public Color ForeColor
  {
    get => this._foreColor;
    set => this._foreColor = value;
  }

  /// <summary>pointer to parent</summary>
  [Description("Parent")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public ListGrid Parent
  {
    get => this._parent;
    set
    {
      this._parent = value;
      this.SubItems.Parent = value;
    }
  }

  /// <summary>Selected</summary>
  [Description("")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DefaultValue(false)]
  [Browsable(false)]
  public bool Selected
  {
    get => this._selected;
    set
    {
      if (this._selected == value)
        return;
      if (this._parent != null && !this._parent.AllowMultiselect && !this._parent.Items.Updating)
      {
        this._parent.Items.Updating = true;
        this._parent.Items.ClearSelection();
        this._parent.Items.Updating = false;
      }
      this._selected = value;
      this.Changed((object) this, new ChangedEventArgs(ChangedType.SelectionChanged, (ListColumn) null, this, (ListSubItem) null));
    }
  }
}
