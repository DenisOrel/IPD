
// Type: Intermech.Controls.Grid.ListSubItem
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Controls.Grid;

/// <summary>SubItems which make up a GLItem</summary>
[DesignTimeVisible(true)]
[TypeConverter("Intermech.Controls.Grid.ListSubItemConverter")]
public class ListSubItem
{
  private string _text = "";
  private Color _foreColor = Color.Black;
  private int _imageIndex = -1;
  private HorizontalAlignment _imageAlignment;
  private Color _backColor;
  private bool _selected;
  private object _tag;
  private bool _forceText;
  private Control _control;
  private ListGrid _parent;
  private bool _checked;
  private Hashtable _embeddedControlProperties;
  private Rectangle _lastCellRect = new Rectangle(0, 0, 0, 0);
  private double _value;

  /// <summary>Sub Item has changed.</summary>
  public event ChangedEventHandler Changed;

  /// <summary>last rectangle that text was drawn into</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Rectangle LastCellRect
  {
    get => this._lastCellRect;
    set => this._lastCellRect = value;
  }

  /// <summary>is the checkbox checked or not</summary>
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Description("Item Check State")]
  [DefaultValue(false)]
  public bool Checked
  {
    get => this._checked;
    set
    {
      if (this._checked == value)
        return;
      this._checked = value;
      if (this.Changed == null)
        return;
      this.Changed((object) this, new ChangedEventArgs(ChangedType.SubItemChanged, (ListColumn) null, (ListItem) null, this));
    }
  }

  /// <summary>pointer to the primary Parent on top</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ListGrid Parent
  {
    get => this._parent;
    set => this._parent = value;
  }

  /// <summary>
  /// Properties of the embedded controls in the listview
  /// 
  /// this is brilliant because it also allows people to set properties of controls that I don't know about
  /// 
  /// the reason I'm even doing this is so many standard control types don't have to be shown
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Hashtable EmbeddedControlProperties
  {
    get
    {
      if (this._embeddedControlProperties == null)
        this._embeddedControlProperties = new Hashtable();
      return this._embeddedControlProperties;
    }
  }

  /// <summary>
  /// Force the sub item display to default to text only
  /// 
  /// This will override everything.
  /// </summary>
  [Description("We can choose to NOT display the control override coming from the column.")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Browsable(true)]
  [Category("Appearance")]
  [DefaultValue(false)]
  public bool ForceText
  {
    get => this._forceText;
    set
    {
      if (this._forceText == value)
        return;
      this._forceText = value;
      this.Changed((object) this, new ChangedEventArgs(ChangedType.SubItemChanged, (ListColumn) null, (ListItem) null, (ListSubItem) null));
    }
  }

  /// <summary>Embedded Control</summary>
  [Description("Embeded control.")]
  [Browsable(false)]
  [Category("Control")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Control Control
  {
    get => this._control;
    set
    {
      if (this._control == value)
        return;
      this._control = value;
      this._control.Visible = false;
    }
  }

  /// <summary>Index of image</summary>
  [Description("Index of image to display from imagelist.  This assumes that an imagelist exists.  If it does not, this will do nothing.")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Browsable(true)]
  [Category("ImageIndex")]
  [DefaultValue(-1)]
  public int ImageIndex
  {
    get => this._imageIndex;
    set
    {
      if (this._imageIndex == value)
        return;
      this._imageIndex = value;
      if (this.Changed == null)
        return;
      this.Changed((object) this, new ChangedEventArgs(ChangedType.SubItemChanged, (ListColumn) null, (ListItem) null, this));
    }
  }

  /// <summary>Alignment of the image within the subitem</summary>
  [Description("Image info for the sub item.")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Browsable(true)]
  [Category("Image")]
  [DefaultValue(HorizontalAlignment.Left)]
  public HorizontalAlignment ImageAlignment
  {
    get => this._imageAlignment;
    set
    {
      if (this._imageAlignment == value)
        return;
      this._imageAlignment = value;
      if (this.Changed == null)
        return;
      this.Changed((object) this, new ChangedEventArgs(ChangedType.SubItemChanged, (ListColumn) null, (ListItem) null, this));
    }
  }

  /// <summary>Extra user information</summary>
  [Description("Extra user information")]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Category("Data")]
  [DefaultValue(null)]
  public object Tag
  {
    get => this._tag;
    set => this._tag = value;
  }

  [Browsable(false)]
  [DefaultValue(0.0)]
  public double Value
  {
    get => this._value;
    set
    {
      if (this._value == value)
        return;
      this._value = value;
      if (this.Changed == null)
        return;
      this.Changed((object) this, new ChangedEventArgs(ChangedType.SubItemChanged, (ListColumn) null, (ListItem) null, this));
    }
  }

  /// <summary>Text</summary>
  [Description("Text")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Browsable(true)]
  [Category("Data")]
  [DefaultValue("")]
  public string Text
  {
    get
    {
      if (this.Parent != null && this.Parent.ActivatedEmbeddedControl != null)
      {
        IEmbeddedControl activatedEmbeddedControl = (IEmbeddedControl) this.Parent.ActivatedEmbeddedControl;
        if (activatedEmbeddedControl != null && activatedEmbeddedControl.SubItem == this)
          return activatedEmbeddedControl.ReturnText();
      }
      return this._text;
    }
    set
    {
      if (!(this._text != value))
        return;
      this._text = value;
      if (this.Changed == null)
        return;
      this.Changed((object) this, new ChangedEventArgs(ChangedType.SubItemChanged, (ListColumn) null, (ListItem) null, this));
    }
  }

  /// <summary>Color of text in item</summary>
  [Description("Color of the text")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Browsable(true)]
  [Category("Appearance")]
  public Color ForeColor
  {
    get => this._foreColor;
    set
    {
      if (!(this._foreColor != value))
        return;
      this._foreColor = value;
      if (this.Changed == null)
        return;
      this.Changed((object) this, new ChangedEventArgs(ChangedType.SubItemChanged, (ListColumn) null, (ListItem) null, this));
    }
  }

  /// <summary>Background color</summary>
  [Description("Color of background")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Browsable(true)]
  [Category("Appearance")]
  public Color BackColor
  {
    get => this._backColor;
    set
    {
      if (!(this._backColor != value))
        return;
      this._backColor = value;
      if (this.Changed == null)
        return;
      this.Changed((object) this, new ChangedEventArgs(ChangedType.SubItemChanged, (ListColumn) null, (ListItem) null, this));
    }
  }

  /// <summary>Indicates when the item is selected</summary>
  [Description("Sub item selection state.")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  [Category("Appearance")]
  public bool Selected
  {
    get => this._selected;
    set
    {
      if (this._selected == value)
        return;
      this._selected = value;
      if (this.Changed == null)
        return;
      this.Changed((object) this, new ChangedEventArgs(ChangedType.ItemChanged, (ListColumn) null, (ListItem) null, this));
    }
  }
}
