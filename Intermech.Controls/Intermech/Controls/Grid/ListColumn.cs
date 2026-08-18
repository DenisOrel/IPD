
// Type: Intermech.Controls.Grid.ListColumn
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Controls.Grid;

[DesignTimeVisible(true)]
[TypeConverter("Intermech.Controls.Grid.ColumnConverter")]
public class ListColumn
{
  private int _width = 100;
  private string _name = nameof (Name);
  private string _text = "Column";
  private ColumnState _state;
  private SortDirection _lastSortDirection = SortDirection.Descending;
  private ArrayList _activeControlItems = new ArrayList();
  private ContentAlignment _textAlignment = ContentAlignment.MiddleLeft;
  private int _imageIndex = -1;
  private bool _checkBoxes;
  private ListGrid _parent;
  private Control _activatedEmbeddedControlTemplate;
  private ActivatedEmbeddedType _activatedEmbeddedType;
  private bool _numericSort;

  public ListColumn()
  {
  }

  public ListColumn(string name)
  {
    this.Name = name;
    this.Text = name;
  }

  /// <summary>Column has changed event</summary>
  public event ChangedEventHandler Changed;

  [Description("Activated embedded control types available.")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public Control ActivatedEmbeddedControlTemplate
  {
    get => this._activatedEmbeddedControlTemplate;
    set => this._activatedEmbeddedControlTemplate = value;
  }

  [Description("Type of system embedded control you would like activated in place here.")]
  [Category("Behavior")]
  [Browsable(true)]
  [DefaultValue(ActivatedEmbeddedType.None)]
  public ActivatedEmbeddedType ActivatedEmbeddedType
  {
    get => this._activatedEmbeddedType;
    set
    {
      this._activatedEmbeddedType = value;
      switch (value)
      {
        case ActivatedEmbeddedType.None:
          this.ActivatedEmbeddedControlTemplate = (Control) null;
          break;
        case ActivatedEmbeddedType.TextBox:
          this.ActivatedEmbeddedControlTemplate = (Control) new ListTextBox();
          break;
        case ActivatedEmbeddedType.ComboBox:
          this.ActivatedEmbeddedControlTemplate = (Control) new ListComboBox();
          break;
        case ActivatedEmbeddedType.DateTimePicker:
          this.ActivatedEmbeddedControlTemplate = (Control) new ListDateTimePicker();
          break;
      }
    }
  }

  /// <summary>Whether or not NumericSort are visible in this column</summary>
  [Description("When sort turned on, only compare numeric values in cells.")]
  [Category("Behavior")]
  [Browsable(true)]
  [DefaultValue(false)]
  public bool NumericSort
  {
    get => this._numericSort;
    set => this._numericSort = value;
  }

  [Description("Parent")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public ListGrid Parent
  {
    get => this._parent;
    set => this._parent = value;
  }

  [Description("Whether or not checkboxes are visible in this column.")]
  [Category("Behavior")]
  [Browsable(true)]
  [DefaultValue(false)]
  public bool CheckBoxes
  {
    get => this._checkBoxes;
    set => this._checkBoxes = value;
  }

  [TypeConverter(typeof (ImageIndexConverter))]
  [DefaultValue(-1)]
  public int ImageIndex
  {
    get => this._imageIndex;
    set => this._imageIndex = value;
  }

  /// <summary>Alignment of text in the header and in the cells</summary>
  [Description("Text alignment inside column header.")]
  [Browsable(true)]
  [DefaultValue(ContentAlignment.MiddleLeft)]
  public ContentAlignment TextAlignment
  {
    get => this._textAlignment;
    set => this._textAlignment = value;
  }

  [Description("Array of items that have live controls.")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public ArrayList ActiveControlItems
  {
    get => this._activeControlItems;
    set => this._activeControlItems = value;
  }

  [Description("Last time sort was done, which direction.")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public SortDirection LastSortState
  {
    get => this._lastSortDirection;
    set => this._lastSortDirection = value;
  }

  [Category("Design")]
  [Browsable(true)]
  [DefaultValue(100)]
  public int Width
  {
    get => this._width;
    set
    {
      if (this._width == value)
        return;
      this._width = value;
      if (this.Changed == null)
        return;
      this.Changed((object) this, new ChangedEventArgs(ChangedType.ColumnChanged, this, (ListItem) null, (ListSubItem) null));
    }
  }

  /// <summary>Text</summary>
  [Category("Misc")]
  [Description("Text to be displayed in header.")]
  [Browsable(true)]
  [DefaultValue("Column")]
  public string Text
  {
    get => this._text;
    set
    {
      if (!(this._text != value))
        return;
      this._text = value;
      if (this.Changed == null)
        return;
      this.Changed((object) this, new ChangedEventArgs(ChangedType.ColumnChanged, this, (ListItem) null, (ListSubItem) null));
    }
  }

  [Category("Design")]
  [Browsable(true)]
  [DefaultValue("Name")]
  public string Name
  {
    get => this._name;
    set
    {
      if (!(this._name != value))
        return;
      this._name = value;
      if (this.Changed == null)
        return;
      this.Changed((object) this, new ChangedEventArgs(ChangedType.ColumnChanged, this, (ListItem) null, (ListSubItem) null));
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ColumnState State
  {
    get => this._state;
    set
    {
      if (this._state == value)
        return;
      this._state = value;
      if (this.Changed == null)
        return;
      this.Changed((object) this, new ChangedEventArgs(ChangedType.ColumnStateChanged, this, (ListItem) null, (ListSubItem) null));
    }
  }
}
