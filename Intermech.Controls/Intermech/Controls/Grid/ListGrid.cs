
// Type: Intermech.Controls.Grid.ListGrid
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;


namespace Intermech.Controls.Grid;

public class ListGrid : Control
{
  private static Bitmap _checkedImage;
  private static Bitmap _uncheckedImage;
  private const int WM_KEYDOWN = 256 /*0x0100*/;
  private const int VK_LEFT = 37;
  private const int VK_UP = 38;
  private const int VK_RIGHT = 39;
  private const int VK_DOWN = 40;
  private const int CHECKBOX_SIZE = 13;
  private const int RESIZE_ARROW_PADDING = 3;
  private const int MINIMUM_COLUMN_SIZE = 0;
  private int _lastSelectionIndex;
  private int _lastSubSelectionIndex;
  private ListState _listState;
  private Point _columnResizeAnchor;
  private int _resizeColumnNumber;
  private ArrayList _liveControls = new ArrayList();
  private ArrayList _newLiveControls = new ArrayList();
  private IContainer components;
  private ManagedVScrollBar _vScrollBar;
  private ManagedHScrollBar _hScrollBar;
  private Rectangle _cornerBox;
  private Control _activatedEmbeddedControl;
  private ColumnCollection _columns;
  private ListItemCollection _items;
  private bool _showBorder = true;
  private GridLineStyle _gridLineStyle = GridLineStyle.Solid;
  private GridLines _gridLines;
  private GridType _gridType = GridType.Exists;
  private int _itemHeight = 18;
  private int _headerHeight = 22;
  private int _borderWidth = 2;
  private Color _gridColor = Color.LightGray;
  private bool _multiSelect;
  private Color _selectionColor = Color.DarkBlue;
  private bool _headerVisible = true;
  private ImageList _imageList;
  private Color _selectedTextColor = Color.White;
  private int _maxHeight;
  private bool _autoHeight = true;
  private bool _allowColumnResize = true;
  private bool _fullRowSelect = true;
  private SortType _sortType = SortType.InsertionSort;
  private ListItem _focusedItem;
  private bool _showFocusRect;
  private bool _hotColumnTracking;
  private bool _hotItemTracking;
  private int _hotColumnIndex = -1;
  private int _hotItemIndex = -1;
  private Color _hotTrackingColor = Color.LightGray;
  private bool _updating;
  private bool _alternatingColors;
  private Color _alternateBackgroundColor = Color.DarkGreen;
  private Color _superFlatHeaderColor = Color.White;
  private HeaderStyle _headerStyle;
  private bool _itemWordWrap;
  private bool _headerWordWrap;
  private bool _selectable = true;
  private bool _hoverEvents;
  private int _hoverTime = 1;
  private Point _lastHoverSpot = new Point(0, 0);
  private bool _hoverLive;
  private Timer _hoverTimer;
  private bool _backgroundStretchToFit = true;
  private bool _themesAvailable;
  private IntPtr _theme = IntPtr.Zero;

  internal static void DW(string strout)
  {
  }

  internal static void DI(string strout)
  {
  }

  static ListGrid()
  {
    Assembly assembly = typeof (ListGrid).Assembly;
    ListGrid._checkedImage = new Bitmap(assembly.GetManifestResourceStream("Intermech.Controls.ListGrid.Resources.checked.bmp"));
    ListGrid._uncheckedImage = new Bitmap(assembly.GetManifestResourceStream("Intermech.Controls.ListGrid.Resources.unchecked.bmp"));
  }

  private void InitializeComponent()
  {
  }

  /// <summary>
  /// Click happened inside control.  Use ClickEventArgs to find out origination area.
  /// </summary>
  public event ListGrid.ClickedEventHandler SelectedIndexChanged;

  /// <summary>
  /// Click happened inside control.  Use ClickEventArgs to find out origination area.
  /// </summary>
  public event ListGrid.ClickedEventHandler ColumnClick;

  /// <summary>Item Changed Event</summary>
  public event ChangedEventHandler ItemChanged;

  /// <summary>Column Changed Event</summary>
  public event ChangedEventHandler ColumnChanged;

  /// <summary>A hover event has occured.</summary>
  /// <remarks>
  /// Use HoverType member of HoverEventArgs to find out if this is a hover origination
  /// or termination event.
  /// </remarks>
  public event ListGrid.HoverEventDelegate Hover;

  [Description("Enabling hover events slows the control some but allows you to be informed when a user has hovered over an item.")]
  [Category("Behavior")]
  [Browsable(true)]
  [DefaultValue(false)]
  public bool HoverEvents
  {
    get => this._hoverEvents;
    set
    {
      this._hoverEvents = value;
      if (this.DesignMode)
        return;
      if (this._hoverEvents)
      {
        this._hoverTimer = new Timer();
        this._hoverTimer.Interval = this._hoverTime * 1000;
        this._hoverTimer.Tick += new EventHandler(this.OnHoverTimerTick);
        this._hoverTimer.Start();
      }
      else
      {
        if (this._hoverTimer == null)
          return;
        this._hoverTimer.Stop();
        this._hoverTimer = (Timer) null;
      }
    }
  }

  [Description("Amount of time in seconds a user hovers before hover event is fired.  Can NOT be zero.")]
  [Category("Behavior")]
  [Browsable(true)]
  [DefaultValue(1)]
  public int HoverTime
  {
    get => this._hoverTime;
    set
    {
      if (this._hoverTime < 1)
        this._hoverTime = 1;
      else
        this._hoverTime = value;
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public Control ActivatedEmbeddedControl
  {
    get => this._activatedEmbeddedControl;
    set => this._activatedEmbeddedControl = value;
  }

  [RefreshProperties(RefreshProperties.Repaint)]
  [Description("Whether or not to stretch background to fit inner list area.")]
  [Category("Behavior")]
  [Browsable(true)]
  [DefaultValue(true)]
  public bool BackgroundStretchToFit
  {
    get => this._backgroundStretchToFit;
    set => this._backgroundStretchToFit = value;
  }

  [RefreshProperties(RefreshProperties.Repaint)]
  [Description("Items selectable.")]
  [Category("Behavior")]
  [Browsable(true)]
  [DefaultValue(true)]
  public bool Selectable
  {
    get => this._selectable;
    set => this._selectable = value;
  }

  [RefreshProperties(RefreshProperties.Repaint)]
  [Description("Word wrap in header")]
  [Category("Header")]
  [Browsable(true)]
  [DefaultValue(false)]
  public bool HeaderWordWrap
  {
    get => this._headerWordWrap;
    set
    {
      this._headerWordWrap = value;
      if (!this.DesignMode || this.Parent == null)
        return;
      this.Parent.Invalidate(true);
    }
  }

  [RefreshProperties(RefreshProperties.Repaint)]
  [Description("Word wrap in cells")]
  [Category("Item")]
  [Browsable(true)]
  [DefaultValue(false)]
  public bool ItemWordWrap
  {
    get => this._itemWordWrap;
    set
    {
      this._itemWordWrap = value;
      if (!this.DesignMode || this.Parent == null)
        return;
      this.Parent.Invalidate(true);
    }
  }

  [RefreshProperties(RefreshProperties.Repaint)]
  [Description("Color for text in boxes that are selected.")]
  [Category("Header")]
  [Browsable(true)]
  public Color SuperFlatHeaderColor
  {
    get => this._superFlatHeaderColor;
    set
    {
      this._superFlatHeaderColor = value;
      if (!this.DesignMode || this.Parent == null)
        return;
      this.Parent.Invalidate(true);
    }
  }

  [RefreshProperties(RefreshProperties.Repaint)]
  [Description("Overall look of control")]
  [Category("Behavior")]
  [Browsable(true)]
  [DefaultValue(HeaderStyle.Normal)]
  public HeaderStyle HeaderStyle
  {
    get => this._headerStyle;
    set
    {
      this._headerStyle = value;
      if (!this.DesignMode || this.Parent == null)
        return;
      ListGrid.DI("Calling Invalidate from ControlStyle Property");
      this.Parent.Invalidate(true);
    }
  }

  [RefreshProperties(RefreshProperties.Repaint)]
  [Description("turn xp themes on or not")]
  [Category("Item Alternating Colors")]
  [Browsable(true)]
  [DefaultValue(false)]
  public bool AlternatingColors
  {
    get => this._alternatingColors;
    set
    {
      this._alternatingColors = value;
      if (!this.DesignMode || this.Parent == null)
        return;
      this.Parent.Invalidate(true);
    }
  }

  [RefreshProperties(RefreshProperties.Repaint)]
  [Description("Color for text in boxes that are selected.")]
  [Category("Item Alternating Colors")]
  [Browsable(true)]
  public Color AlternateBackground
  {
    get => this._alternateBackgroundColor;
    set
    {
      this._alternateBackgroundColor = value;
      if (!this.DesignMode || this.Parent == null)
        return;
      this.Parent.Invalidate(true);
    }
  }

  /// <summary>Whether or not to show a border.</summary>
  [Description("Whether or not to show a border.")]
  [Category("Appearance")]
  [Browsable(true)]
  [DefaultValue(true)]
  public bool ShowBorder
  {
    get => this._showBorder;
    set
    {
      this._showBorder = value;
      if (!this.DesignMode || this.Parent == null)
        return;
      this.Parent.Invalidate(true);
    }
  }

  /// <summary>Color for text in boxes that are selected</summary>
  [RefreshProperties(RefreshProperties.Repaint)]
  [Description("Color for text in boxes that are selected.")]
  [Category("Item")]
  [Browsable(true)]
  public Color SelectedTextColor
  {
    get => this._selectedTextColor;
    set => this._selectedTextColor = value;
  }

  /// <summary>hot tracking</summary>
  [Description("Color for hot tracking.")]
  [Category("Appearance")]
  [Browsable(true)]
  public Color HotTrackingColor
  {
    get => this._hotTrackingColor;
    set => this._hotTrackingColor = value;
  }

  /// <summary>Hot Tracking of columns and items</summary>
  [Description("Show hot tracking.")]
  [Category("Behavior")]
  [Browsable(true)]
  [DefaultValue(false)]
  public bool HotItemTracking
  {
    get => this._hotItemTracking;
    set => this._hotItemTracking = value;
  }

  /// <summary>Hot Tracking of columns and items</summary>
  [Description("Show hot tracking.")]
  [Category("Behavior")]
  [Browsable(true)]
  [DefaultValue(false)]
  public bool HotColumnTracking
  {
    get => this._hotColumnTracking;
    set => this._hotColumnTracking = value;
  }

  /// <summary>Show the focus rect or not</summary>
  [Description("Show Focus Rect on items.")]
  [Category("Item")]
  [Browsable(true)]
  [DefaultValue(false)]
  public bool ShowFocusRect
  {
    get => this._showFocusRect;
    set => this._showFocusRect = value;
  }

  /// <summary>auto sorting</summary>
  [RefreshProperties(RefreshProperties.Repaint)]
  [Description("Type of sorting algorithm used.")]
  [Category("Behavior")]
  [Browsable(true)]
  [DefaultValue(SortType.InsertionSort)]
  public SortType SortType
  {
    get => this._sortType;
    set => this._sortType = value;
  }

  /// <summary>
  /// 
  /// </summary>
  [RefreshProperties(RefreshProperties.Repaint)]
  [Description("ImageList to be used in listview.")]
  [Category("Behavior")]
  [Browsable(true)]
  public ImageList ImageList
  {
    get => this._imageList;
    set => this._imageList = value;
  }

  /// <summary>Allow columns to be resized</summary>
  [RefreshProperties(RefreshProperties.Repaint)]
  [Description("Allow resizing of columns")]
  [Category("Header")]
  [Browsable(true)]
  [DefaultValue(true)]
  public bool AllowColumnResize
  {
    get => this._allowColumnResize;
    set => this._allowColumnResize = value;
  }

  /// <summary>Control resizes height of row based on size.</summary>
  [RefreshProperties(RefreshProperties.Repaint)]
  [Description("Do we want rows to automatically adjust height")]
  [Category("Item")]
  [Browsable(true)]
  [DefaultValue(true)]
  public bool AutoHeight
  {
    get => this._autoHeight;
    set
    {
      this._autoHeight = value;
      if (!this.DesignMode || this.Parent == null)
        return;
      this.Parent.Invalidate(true);
    }
  }

  /// <summary>you want the header to be visible or not</summary>
  [RefreshProperties(RefreshProperties.Repaint)]
  [Description("Column Headers Visible")]
  [Category("Header")]
  [Browsable(true)]
  [DefaultValue(true)]
  public bool HeaderVisible
  {
    get => this._headerVisible;
    set
    {
      this._headerVisible = value;
      if (!this.DesignMode || this.Parent == null)
        return;
      this.Parent.Invalidate(true);
    }
  }

  /// <summary>Collection of columns</summary>
  [Category("Header")]
  [Description("Column Collection")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [Editor(typeof (CustomCollectionEditor), typeof (UITypeEditor))]
  [Browsable(true)]
  public ColumnCollection Columns => this._columns;

  /// <summary>Collection of items</summary>
  [Category("Item")]
  [Description("Items collection")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [Editor(typeof (CollectionEditor), typeof (UITypeEditor))]
  [Browsable(true)]
  public ListItemCollection Items => this._items;

  /// <summary>selection bar color</summary>
  [RefreshProperties(RefreshProperties.Repaint)]
  [Description("Background color to mark selection.")]
  [Category("Item")]
  [Browsable(true)]
  public Color SelectionColor
  {
    get => this._selectionColor;
    set => this._selectionColor = value;
  }

  /// <summary>Selection Full Row</summary>
  [RefreshProperties(RefreshProperties.Repaint)]
  [Description("Allow full row select.")]
  [Category("Item")]
  [Browsable(true)]
  [DefaultValue(true)]
  public bool FullRowSelect
  {
    get => this._fullRowSelect;
    set => this._fullRowSelect = value;
  }

  /// <summary>Allow multiple row selection</summary>
  [RefreshProperties(RefreshProperties.Repaint)]
  [Description("Allow multiple selections.")]
  [Category("Item")]
  [Browsable(true)]
  [DefaultValue(false)]
  public bool AllowMultiselect
  {
    get => this._multiSelect;
    set => this._multiSelect = value;
  }

  [RefreshProperties(RefreshProperties.Repaint)]
  [Description("Border Padding")]
  [Category("Appearance")]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  private int BorderPadding
  {
    get => this.ShowBorder ? this._borderWidth : 0;
    set
    {
      this._borderWidth = value;
      this.Invalidate();
    }
  }

  [RefreshProperties(RefreshProperties.Repaint)]
  [Description("Border Width")]
  [Category("Appearance")]
  [Browsable(true)]
  [DefaultValue(2)]
  public int BorderWidth
  {
    get => this._borderWidth;
    set
    {
      this._borderWidth = value;
      this.Invalidate();
    }
  }

  /// <summary>Grid Line Styles</summary>
  [RefreshProperties(RefreshProperties.Repaint)]
  [Description("Whether or not to draw gridlines")]
  [Category("Grid")]
  [Browsable(true)]
  [DefaultValue(GridLineStyle.Solid)]
  public GridLineStyle GridLineStyle
  {
    get => this._gridLineStyle;
    set
    {
      this._gridLineStyle = value;
      if (!this.DesignMode || this.Parent == null)
        return;
      this.Parent.Invalidate(true);
    }
  }

  [RefreshProperties(RefreshProperties.Repaint)]
  [Description("Whether or not to draw gridlines")]
  [Category("Grid")]
  [Browsable(true)]
  [DefaultValue(GridType.Exists)]
  public GridType GridTypes
  {
    get => this._gridType;
    set
    {
      this._gridType = value;
      if (!this.DesignMode || this.Parent == null)
        return;
      ListGrid.DI("Calling Invalidate From GLGridTypes");
      this.Parent.Invalidate(true);
    }
  }

  /// <summary>Grid Lines Type</summary>
  [RefreshProperties(RefreshProperties.Repaint)]
  [Description("Whether or not to draw gridlines")]
  [Category("Grid")]
  [Browsable(true)]
  [DefaultValue(GridLines.Both)]
  public GridLines GridLines
  {
    get => this._gridLines;
    set
    {
      this._gridLines = value;
      if (!this.DesignMode || this.Parent == null)
        return;
      ListGrid.DI("Calling Invalidate From GLGridLines");
      this.Parent.Invalidate(true);
    }
  }

  [RefreshProperties(RefreshProperties.Repaint)]
  [Description("Color of the grid if we draw it.")]
  [Category("Grid")]
  [Browsable(true)]
  public Color GridColor
  {
    get => this._gridColor;
    set
    {
      this._gridColor = value;
      if (!this.DesignMode || this.Parent == null)
        return;
      ListGrid.DI("Calling Invalidate From GridColor");
      this.Parent.Invalidate(true);
    }
  }

  /// <summary>how big do we want the individual items to be</summary>
  [RefreshProperties(RefreshProperties.Repaint)]
  [Description("How high each row is.")]
  [Category("Item")]
  [Browsable(true)]
  [DefaultValue(20)]
  public int ItemHeight
  {
    get => this._itemHeight;
    set
    {
      this._itemHeight = value;
      if (!this.DesignMode || this.Parent == null)
        return;
      ListGrid.DI("Calling Invalidate From ItemHeight");
      this.Parent.Invalidate(true);
    }
  }

  /// <summary>Force header height.</summary>
  [RefreshProperties(RefreshProperties.Repaint)]
  [Description("How high the Header are.")]
  [Category("Header")]
  [Browsable(true)]
  [DefaultValue(20)]
  public int HeaderHeight
  {
    get => this.HeaderVisible ? this._headerHeight : 0;
    set
    {
      this._headerHeight = value;
      if (!this.DesignMode || this.Parent == null)
        return;
      ListGrid.DI("Calling Invalidate From HeaderHeight");
      this.Parent.Invalidate(true);
    }
  }

  /// <summary>amount of space inside any given cell to borders</summary>
  [Description("Cell padding area")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public int CellPaddingSize => 2;

  /// <summary>Are themes available for this control?</summary>
  [Description("Are Themes Available")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  protected bool ThemesAvailable => this._themesAvailable;

  /// <summary>returns a list of only the selected items</summary>
  [Description("Selected Items Array")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public ArrayList SelectedItems => this.Items.SelectedItems;

  /// <summary>returns a list of only the selected items indexes</summary>
  [Description("Selected Items Array Of Indicies")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public ArrayList SelectedIndicies => this.Items.SelectedIndicies;

  /// <summary>currently Hot Column</summary>
  [Description("Currently Focused Column")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public int HotColumnIndex
  {
    get => this._hotColumnIndex;
    set
    {
      if (!this._hotColumnTracking || this._hotColumnIndex == value)
        return;
      this._hotItemIndex = -1;
      this._hotColumnIndex = value;
      if (this.DesignMode)
        return;
      ListGrid.DI("Calling Invalidate From HotColumnIndex");
      this.Invalidate(true);
    }
  }

  /// <summary>Current Hot Item</summary>
  [Description("Currently Focused Item")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public int HotItemIndex
  {
    get => this._hotItemIndex;
    set
    {
      if (!this._hotItemTracking || this._hotItemIndex == value)
        return;
      this._hotColumnIndex = -1;
      this._hotItemIndex = value;
      ListGrid.DI("Calling Invalidate From HotItemIndex");
      this.Invalidate(true);
    }
  }

  /// <summary>Currently focused item</summary>
  [Description("Currently Focused Item")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public ListItem FocusedItem
  {
    get
    {
      if (this._focusedItem != null && this.Items.FindItemIndex(this._focusedItem) < 0)
        this._focusedItem = (ListItem) null;
      return this._focusedItem;
    }
    set
    {
      if (this._focusedItem == value)
        return;
      this._focusedItem = value;
      if (!this.DesignMode)
      {
        ListGrid.DI("Calling Invalidate From FocusedItem");
        this.Invalidate(true);
      }
      if (this.SelectedIndexChanged == null)
        return;
      this.SelectedIndexChanged((object) this, new ClickEventArgs(this.Items.FindItemIndex(value), -1));
    }
  }

  /// <summary>Current count of items in collection.</summary>
  [Description("Number of items/rows in the list.")]
  [Category("Behavior")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  [DefaultValue(0)]
  public int Count => this.Items.Count;

  /// <summary>Calculates total height of all rows combined.</summary>
  [Description("All items together height.")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  protected int TotalRowHeight => this.ItemHeight * this.Items.Count;

  /// <summary>Number of rows currently visible</summary>
  [Description("Number of rows currently visible in inner rect.")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  protected int VisibleRowsCount => this.RowsInnerClientRect.Height / this.ItemHeight;

  /// <summary>
  /// Max Height of any given row at any given time.  Used with AutoHeight exclusively.
  /// </summary>
  [Description("this will always reflect the most height any item line has needed")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  protected int MaxHeight
  {
    get => this._maxHeight;
    set
    {
      if (value <= this._maxHeight)
        return;
      this._maxHeight = value;
      if (!this.AutoHeight)
        return;
      this.ItemHeight = this.MaxHeight;
      if (!this.DesignMode)
      {
        ListGrid.DI("Calling Invalidate From MaxHeight");
        this.Invalidate(true);
      }
      ListGrid.DW("Item height set bigger");
    }
  }

  /// <summary>Rect of header area</summary>
  [Description("The rectangle of the header inside parent control")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  protected Rectangle HeaderRect
  {
    get
    {
      return new Rectangle(this.BorderPadding, this.BorderPadding, this.Width - this.BorderPadding * 2, this.HeaderHeight);
    }
  }

  /// <summary>Row Client Rectangle</summary>
  [Description("The rectangle of the client inside parent control")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  protected Rectangle RowsClientRect
  {
    get
    {
      return new Rectangle(this.BorderPadding, this.HeaderHeight + this.BorderPadding, this.Width - this.BorderPadding * 2, this.Height - this.HeaderHeight - this.BorderPadding * 2);
    }
  }

  /// <summary>Full Sized rectangle of all columns total width.</summary>
  [Description("Full Sized rectangle of all columns total width.")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public Rectangle RowsRect
  {
    get
    {
      return new Rectangle()
      {
        X = -this._hScrollBar.Value + this.BorderPadding,
        Y = this.HeaderHeight + this.BorderPadding,
        Width = this.Columns.Width,
        Height = this.VisibleRowsCount * this.ItemHeight
      };
    }
  }

  /// <summary>
  /// The inner rectangle of the client inside parent control taking scroll bars into account.
  /// </summary>
  [Description("The inner rectangle of the client inside parent control taking scroll bars into account.")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public Rectangle RowsInnerClientRect
  {
    get
    {
      Rectangle rowsClientRect = this.RowsClientRect;
      rowsClientRect.Width -= this._vScrollBar.mWidth;
      rowsClientRect.Height -= this._hScrollBar.mHeight;
      if (rowsClientRect.Width < 0)
        rowsClientRect.Width = 0;
      if (rowsClientRect.Height < 0)
        rowsClientRect.Height = 0;
      return rowsClientRect;
    }
  }

  /// <summary>constructor</summary>
  public ListGrid()
  {
    ListGrid.DW("Constructor");
    this._columns = new ColumnCollection(this);
    this._columns.Changed += new ChangedEventHandler(this.Columns_Changed);
    this._items = new ListItemCollection(this);
    this._items.Changed += new ChangedEventHandler(this.Items_Changed);
    this.InitializeComponent();
    if (!this.DesignMode)
      this._themesAvailable = this.AreThemesAvailable();
    this.TabStop = true;
    this.SetStyle(ControlStyles.UserPaint | ControlStyles.Opaque | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.UserMouse | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
    this.BackColor = SystemColors.ControlLightLight;
    this._hScrollBar = new ManagedHScrollBar();
    this._vScrollBar = new ManagedVScrollBar();
    this._hScrollBar.Scroll += new ScrollEventHandler(this.OnScroll);
    this._vScrollBar.Scroll += new ScrollEventHandler(this.OnScroll);
    this._cornerBox = new Rectangle();
    this.SuspendLayout();
    this._hScrollBar.Anchor = AnchorStyles.None;
    this._hScrollBar.CausesValidation = false;
    this._hScrollBar.Location = new Point(24, 0);
    this._hScrollBar.mHeight = 16 /*0x10*/;
    this._hScrollBar.mWidth = 120;
    this._hScrollBar.Name = "hPanelScrollBar";
    this._hScrollBar.Size = new Size(120, 16 /*0x10*/);
    this._hScrollBar.Scroll += new ScrollEventHandler(this.hPanelScrollBar_Scroll);
    this._hScrollBar.Parent = (Control) this;
    this.Controls.Add((Control) this._hScrollBar);
    this._vScrollBar.Anchor = AnchorStyles.None;
    this._vScrollBar.CausesValidation = false;
    this._vScrollBar.Location = new Point(0, 12);
    this._vScrollBar.mHeight = 120;
    this._vScrollBar.mWidth = 16 /*0x10*/;
    this._vScrollBar.Name = "vPanelScrollBar";
    this._vScrollBar.Size = new Size(16 /*0x10*/, 120);
    this._vScrollBar.Scroll += new ScrollEventHandler(this.vPanelScrollBar_Scroll);
    this._vScrollBar.Parent = (Control) this;
    this.Controls.Add((Control) this._vScrollBar);
    this.Name = "GlacialList";
    this.ResumeLayout(false);
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (this._theme != IntPtr.Zero)
      ThemeRoutines.CloseThemeData(this._theme);
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// If an activated embedded control exists, remove and unload it
  /// </summary>
  private void DestroyActivatedEmbedded()
  {
    if (this._activatedEmbeddedControl == null)
      return;
    ((IEmbeddedControl) this._activatedEmbeddedControl).Unload();
    if (this._activatedEmbeddedControl == null)
      return;
    this._activatedEmbeddedControl.Dispose();
    this._activatedEmbeddedControl = (Control) null;
  }

  /// <summary>
  /// Instance the activated embeddec control for this item/column
  /// </summary>
  /// <param name="nColumn"></param>
  /// <param name="item"></param>
  /// <param name="subItem"></param>
  protected void ActivateEmbeddedControl(int nColumn, ListItem item, ListSubItem subItem)
  {
    if (this._activatedEmbeddedControl != null)
    {
      this._activatedEmbeddedControl.Dispose();
      this._activatedEmbeddedControl = (Control) null;
    }
    if (this.Columns[nColumn].ActivatedEmbeddedControlTemplate == null)
      return;
    Control instance = (Control) Activator.CreateInstance(this.Columns[nColumn].ActivatedEmbeddedControlTemplate.GetType());
    ((IEmbeddedControl) instance ?? throw new Exception("Control does not implement the GLEmbeddedControl interface, can't start")).Load(item, subItem, this);
    instance.KeyPress += new KeyPressEventHandler(this.tb_KeyPress);
    instance.Parent = (Control) this;
    this.ActivatedEmbeddedControl = instance;
    Rectangle rectangle1 = subItem.LastCellRect;
    int height1 = rectangle1.Height;
    rectangle1 = this._activatedEmbeddedControl.Bounds;
    int height2 = rectangle1.Height;
    int num = (height1 - height2) / 2;
    Rectangle rectangle2;
    if (this.GridLineStyle == GridLineStyle.None)
    {
      ref Rectangle local = ref rectangle2;
      rectangle1 = subItem.LastCellRect;
      int x = rectangle1.X + 1;
      rectangle1 = subItem.LastCellRect;
      int y = rectangle1.Y + 1;
      rectangle1 = subItem.LastCellRect;
      int width = rectangle1.Width - 3;
      rectangle1 = subItem.LastCellRect;
      int height3 = rectangle1.Height - 2;
      local = new Rectangle(x, y, width, height3);
    }
    else
    {
      ref Rectangle local = ref rectangle2;
      rectangle1 = subItem.LastCellRect;
      int x = rectangle1.X + 1;
      rectangle1 = subItem.LastCellRect;
      int y = rectangle1.Y + 2;
      rectangle1 = subItem.LastCellRect;
      int width = rectangle1.Width - 3;
      rectangle1 = subItem.LastCellRect;
      int height4 = rectangle1.Height - 3;
      local = new Rectangle(x, y, width, height4);
    }
    instance.Bounds = rectangle2;
    instance.Show();
    instance.Focus();
  }

  /// <summary>check for return (if we get it, deactivate)</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tb_KeyPress(object sender, KeyPressEventArgs e)
  {
    if (e.KeyChar != '\r' && e.KeyChar != '\u001B')
      return;
    this.DestroyActivatedEmbedded();
  }

  /// <summary>keep certain keys here</summary>
  /// <param name="msg"></param>
  /// <returns></returns>
  public override bool PreProcessMessage(ref Message msg)
  {
    ListGrid.DW("PreProcessMessage " + msg.ToString());
    if (msg.Msg != 256 /*0x0100*/)
      return base.PreProcessMessage(ref msg);
    Keys wparam = (Keys) (int) msg.WParam;
    if (wparam == Keys.Return)
    {
      this.DestroyActivatedEmbedded();
      return true;
    }
    if (this.FocusedItem != null && this.Count > 0 && this.Selectable)
    {
      int itemIndex = this.Items.FindItemIndex(this.FocusedItem);
      int num = itemIndex;
      if (itemIndex < 0)
        return true;
      if (wparam == Keys.A && (Control.ModifierKeys & Keys.Control) == Keys.Control)
      {
        for (int nItemIndex = 0; nItemIndex < this.Items.Count; ++nItemIndex)
          this.Items[nItemIndex].Selected = true;
        return base.PreProcessMessage(ref msg);
      }
      int nItemIndex1;
      switch (wparam)
      {
        case Keys.Escape:
          this.Items.ClearSelection();
          this.FocusedItem = (ListItem) null;
          return base.PreProcessMessage(ref msg);
        case Keys.Space:
          if (!this.AllowMultiselect)
            this.Items.ClearSelection(this.Items[itemIndex]);
          this.Items[itemIndex].Selected = !this.Items[itemIndex].Selected;
          return base.PreProcessMessage(ref msg);
        case Keys.Prior:
          nItemIndex1 = itemIndex - this.VisibleRowsCount;
          break;
        case Keys.Next:
          nItemIndex1 = itemIndex + this.VisibleRowsCount;
          break;
        case Keys.End:
          nItemIndex1 = this.Count - 1;
          break;
        case Keys.Home:
          nItemIndex1 = 0;
          break;
        case Keys.Up:
          nItemIndex1 = itemIndex - 1;
          break;
        case Keys.Down:
          nItemIndex1 = itemIndex + 1;
          break;
        default:
          return base.PreProcessMessage(ref msg);
      }
      if (nItemIndex1 > this.Count - 1)
        nItemIndex1 = this.Count - 1;
      if (nItemIndex1 < 0)
        nItemIndex1 = 0;
      if (nItemIndex1 < this._vScrollBar.Value)
        this._vScrollBar.Value = nItemIndex1;
      if (nItemIndex1 > this._vScrollBar.Value + (this.VisibleRowsCount - 1))
        this._vScrollBar.Value = nItemIndex1 - (this.VisibleRowsCount - 1);
      if (num != nItemIndex1)
      {
        if ((Control.ModifierKeys & Keys.Control) != Keys.Control && (Control.ModifierKeys & Keys.Shift) != Keys.Shift)
        {
          this._lastSelectionIndex = nItemIndex1;
          this.Items[nItemIndex1].Selected = true;
          this.Items.ClearSelection(this.Items[nItemIndex1]);
        }
        else if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
        {
          this.Items.ClearSelection();
          if (!this.AllowMultiselect)
            this.Items[nItemIndex1].Selected = !this.Items[nItemIndex1].Selected;
          else if (this._lastSelectionIndex >= 0)
          {
            int lastSelectionIndex = this._lastSelectionIndex;
            do
            {
              this.Items[lastSelectionIndex].Selected = true;
              if (lastSelectionIndex > nItemIndex1)
                --lastSelectionIndex;
              if (lastSelectionIndex < nItemIndex1)
                ++lastSelectionIndex;
            }
            while (lastSelectionIndex != nItemIndex1);
            this.Items[lastSelectionIndex].Selected = true;
          }
        }
        else
          this._lastSelectionIndex = nItemIndex1;
        this.FocusedItem = this.Items[nItemIndex1];
      }
    }
    else
    {
      int num1 = this._vScrollBar.Value;
      int num2;
      switch (wparam)
      {
        case Keys.Prior:
          num2 = num1 - this.VisibleRowsCount;
          break;
        case Keys.Next:
          num2 = num1 + this.VisibleRowsCount;
          break;
        case Keys.End:
          num2 = this.Count - this.VisibleRowsCount;
          break;
        case Keys.Home:
          num2 = 0;
          break;
        case Keys.Up:
          num2 = num1 - 1;
          break;
        case Keys.Down:
          num2 = num1 + 1;
          break;
        default:
          return base.PreProcessMessage(ref msg);
      }
      if (num2 > this.Count - this.VisibleRowsCount)
        num2 = this.Count - this.VisibleRowsCount;
      if (num2 < 0)
        num2 = 0;
      if (this._vScrollBar.Value != num2)
      {
        this._vScrollBar.Value = num2;
        ListGrid.DI("Calling Invalidate From PreProcessMessage");
        this.Invalidate();
      }
    }
    return true;
  }

  /// <summary>
  /// Timer handler.  This mostly deals with the hover technology with events firing.
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnHoverTimerTick(object sender, EventArgs e)
  {
    Point point = !(this.Cursor != (Cursor) null) ? new Point(9999, 9999) : this.PointToClient(Cursor.Position);
    int nItem = 0;
    int nColumn = 0;
    int nCellX = 0;
    int nCellY = 0;
    ListRegion listRegion;
    this.InterpretCoords(point.X, point.Y, out listRegion, out nCellX, out nCellY, out nItem, out nColumn, out ListState _);
    if (point == this._lastHoverSpot && !this._hoverLive && listRegion != ListRegion.NonClient)
    {
      if (this.Hover != null)
        this.Hover((object) this, new HoverEventArgs(HoverType.HoverStart, nItem, nColumn, listRegion));
      this._hoverLive = true;
    }
    else if (this._hoverLive && point != this._lastHoverSpot)
    {
      if (this.Hover != null)
        this.Hover((object) this, new HoverEventArgs(HoverType.HoverEnd, -1, -1, ListRegion.NonClient));
      this._hoverLive = false;
    }
    this._lastHoverSpot = point;
  }

  /// <summary>Item has changed, fire event</summary>
  /// <param name="source"></param>
  /// <param name="e"></param>
  protected void Items_Changed(object source, ChangedEventArgs e)
  {
    ListGrid.DW("GlacialList::Items_Changed");
    this.DestroyActivatedEmbedded();
    if (this.ItemChanged != null)
      this.ItemChanged((object) this, e);
    if (e.Item == null || !this.IsItemVisible(e.Item))
      return;
    ListGrid.DI("Calling Invalidate From Items_Changed");
    this.Invalidate();
  }

  public void Columns_Changed(object source, ChangedEventArgs e)
  {
    ListGrid.DW(nameof (Columns_Changed));
    if (e.ChangedType != ChangedType.ColumnStateChanged)
      this.DestroyActivatedEmbedded();
    if (this.ColumnChanged != null)
      this.ColumnChanged((object) this, e);
    ListGrid.DI("Calling Invalidate From Columns_Changed");
    this.Invalidate();
  }

  /// <summary>
  /// When the control receives focus
  /// 
  /// this routine is the one that makes absolute certain if the embedded control loses focus then
  /// the embedded control is destroyed
  /// </summary>
  /// <param name="e"></param>
  protected override void OnGotFocus(EventArgs e)
  {
    this.DestroyActivatedEmbedded();
    base.OnGotFocus(e);
  }

  /// <summary>
  /// This is an OPTIMIZED routine to see if an item is visible.
  /// 
  /// The other method of just checking against the item index was slow becuase it had to walk the entire list, which would massively
  /// slow down the control when large numbers of items were added.
  /// </summary>
  /// <param name="item"></param>
  /// <returns></returns>
  public bool IsItemVisible(ListItem item)
  {
    int itemIndex = this.Items.FindItemIndex(item);
    return itemIndex >= this._vScrollBar.Value && itemIndex < this._vScrollBar.Value + this.VisibleRowsCount;
  }

  /// <summary>Tell paint to stop worry about updates</summary>
  public void BeginUpdate() => this._updating = true;

  /// <summary>
  /// Tell paint to start worrying about updates again and repaint while your at it
  /// </summary>
  public void EndUpdate()
  {
    this._updating = false;
    this.Invalidate();
  }

  /// <summary>
  /// interpret mouse coordinates
  /// 
  /// ok, I've violated the spirit of this routine a couple times (but no more!).  Do NOT put anything
  /// functional in this routine.  It is ONLY for analyzing the mouse coordinates.  Do not break this again!
  /// </summary>
  /// <param name="nScreenX"></param>
  /// <param name="nScreenY"></param>
  /// <param name="listRegion"></param>
  /// <param name="nCellX"></param>
  /// <param name="nCellY"></param>
  /// <param name="nItem"></param>
  /// <param name="nColumn"></param>
  /// <param name="nState"></param>
  public void InterpretCoords(
    int nScreenX,
    int nScreenY,
    out ListRegion listRegion,
    out int nCellX,
    out int nCellY,
    out int nItem,
    out int nColumn,
    out ListState nState)
  {
    ListGrid.DW("Interpret Coords");
    nState = ListState.Normal;
    nColumn = 0;
    nItem = 0;
    nCellX = 0;
    nCellY = 0;
    listRegion = ListRegion.NonClient;
    int num1 = -this._hScrollBar.Value + this.BorderPadding;
    nColumn = 0;
    while (nColumn < this.Columns.Count)
    {
      ListColumn column = this.Columns[nColumn];
      nCellX = nScreenX - num1;
      if (nScreenX > num1 && nScreenX < num1 + column.Width - 3)
      {
        nState = ListState.ColumnSelect;
        break;
      }
      if (nScreenX >= num1 + column.Width - 3 && nScreenX <= num1 + column.Width + 3 && (nColumn + 1 == this.Columns.Count || this.Columns[nColumn + 1].Width != 0))
      {
        if (!this.AllowColumnResize)
          return;
        nState = ListState.ColumnResizing;
        return;
      }
      num1 += column.Width;
      ++nColumn;
    }
    int num2 = nScreenY;
    Rectangle rectangle = this.RowsInnerClientRect;
    int y1 = rectangle.Y;
    if (num2 >= y1)
    {
      int num3 = nScreenY;
      rectangle = this.RowsInnerClientRect;
      int bottom = rectangle.Bottom;
      if (num3 < bottom)
      {
        listRegion = ListRegion.Client;
        this.Columns.ClearHotStates();
        this.HotColumnIndex = -1;
        ref int local1 = ref nItem;
        int num4 = nScreenY;
        rectangle = this.RowsInnerClientRect;
        int y2 = rectangle.Y;
        int num5 = (num4 - y2) / this.ItemHeight + this._vScrollBar.Value;
        local1 = num5;
        ref int local2 = ref nCellY;
        int num6 = nScreenY;
        rectangle = this.RowsInnerClientRect;
        int y3 = rectangle.Y;
        int num7 = (num6 - y3) % this.ItemHeight;
        local2 = num7;
        this.HotItemIndex = nItem;
        if (nItem >= this.Items.Count || nItem > this._vScrollBar.Value + this.VisibleRowsCount)
        {
          nState = ListState.Normal;
          listRegion = ListRegion.NonClient;
          return;
        }
        nState = ListState.Selecting;
        for (int index = 0; index < this.Columns.Count; ++index)
        {
          if (index >= nColumn)
          {
            nColumn = index;
            break;
          }
        }
        return;
      }
    }
    int num8 = nScreenY;
    rectangle = this.HeaderRect;
    int y4 = rectangle.Y;
    if (num8 < y4)
      return;
    int num9 = nScreenY;
    rectangle = this.HeaderRect;
    int bottom1 = rectangle.Bottom;
    if (num9 >= bottom1)
      return;
    listRegion = ListRegion.Header;
    this.HotItemIndex = -1;
    this.HotColumnIndex = nColumn;
    if (nColumn <= -1 || nColumn >= this.Columns.Count || this.Columns.AnyPressed() || this.Columns[nColumn].State != ColumnState.Normal)
      return;
    this.Columns.ClearHotStates();
    this.Columns[nColumn].State = ColumnState.Hot;
  }

  /// <summary>return the X starting point of a particular column</summary>
  /// <param name="nColumn"></param>
  /// <returns></returns>
  public int GetColumnScreenX(int nColumn)
  {
    ListGrid.DW("Get Column Screen X");
    if (nColumn >= this.Columns.Count)
      return 0;
    int columnScreenX = -this._hScrollBar.Value;
    int num = 0;
    foreach (ListColumn column in (CollectionBase) this.Columns)
    {
      if (num >= nColumn)
        return columnScreenX;
      ++num;
      columnScreenX += column.Width;
    }
    return 0;
  }

  /// <summary>
  /// Sort a column.
  /// 
  /// Set to virtual so you can write your own sorting
  /// </summary>
  /// <param name="nColumn"></param>
  public virtual void SortColumn(int nColumn, bool toggle)
  {
    if (this.Count < 2)
      return;
    if (this.SortType == SortType.InsertionSort)
      new ListQuickSort()
      {
        NumericCompare = this.Columns[nColumn].NumericSort,
        SortDirection = this.Columns[nColumn].LastSortState,
        SortColumn = nColumn
      }.InsertionSort(this.Items, 0, this.Items.Count - 1);
    else if (this.SortType == SortType.MergeSort)
      new MergeSort()
      {
        NumericCompare = this.Columns[nColumn].NumericSort,
        SortDirection = this.Columns[nColumn].LastSortState,
        SortColumn = nColumn
      }.sort(this.Items, 0, this.Items.Count - 1);
    else if (this.SortType == SortType.QuickSort)
      new ListQuickSort()
      {
        NumericCompare = this.Columns[nColumn].NumericSort,
        SortDirection = this.Columns[nColumn].LastSortState,
        SortColumn = nColumn
      }.sort(this.Items);
    if (!toggle)
      return;
    if (this.Columns[nColumn].LastSortState == SortDirection.Descending)
      this.Columns[nColumn].LastSortState = SortDirection.Ascending;
    else
      this.Columns[nColumn].LastSortState = SortDirection.Descending;
  }

  internal static bool RunningOnXP()
  {
    bool flag = false;
    if (Environment.OSVersion.Platform == PlatformID.Win32NT)
      flag = Environment.OSVersion.Version >= new Version(5, 1, 0, 0);
    return flag;
  }

  protected bool AreThemesAvailable()
  {
    ListGrid.DW(nameof (AreThemesAvailable));
    try
    {
      if (ListGrid.RunningOnXP())
      {
        if (ThemeRoutines.IsThemeActive() == 1)
        {
          if (this._theme == IntPtr.Zero)
          {
            this._theme = ThemeRoutines.OpenThemeData(this._theme, "HEADER");
            return true;
          }
        }
      }
    }
    catch (Exception ex)
    {
    }
    return false;
  }

  /// <summary>Control is resizing, handle invalidations</summary>
  /// <param name="e"></param>
  protected override void OnResize(EventArgs e)
  {
    ListGrid.DW("GlacialList_Resize");
    ListGrid.DI("Calling Invalidate From OnResize");
    this.Invalidate();
  }

  /// <summary>Entry point to paint routines</summary>
  /// <param name="e"></param>
  protected override void OnPaint(PaintEventArgs e)
  {
    ListGrid.DW("Paint");
    if (!this.DesignMode && this._updating)
      return;
    using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
    {
      Graphics graphics = e.Graphics;
      Rectangle clientRectangle = this.ClientRectangle;
      graphics.FillRectangle((Brush) solidBrush, clientRectangle);
      if (this._showBorder)
        graphics.DrawRectangle(SystemPens.ControlDark, clientRectangle.Left, clientRectangle.Top, clientRectangle.Width - 1, clientRectangle.Height - 1);
      this.RecalcScroll();
      if (!this._cornerBox.IsEmpty)
        e.Graphics.FillRectangle((Brush) solidBrush, this._cornerBox);
      if (this.Columns.Width > this.HeaderRect.Width)
      {
        int width1 = this.Columns.Width;
      }
      else
      {
        int width2 = this.HeaderRect.Width;
      }
      if (this.HeaderVisible)
      {
        graphics.SetClip(this.HeaderRect);
        this.DrawHeader(graphics, new Size(this.HeaderRect.Width, this.HeaderRect.Height));
      }
      graphics.SetClip(this.RowsInnerClientRect);
      this.DrawRows(graphics, (Brush) solidBrush);
      foreach (Control liveControl in this._liveControls)
        liveControl.Visible = false;
      this._liveControls = this._newLiveControls;
      this._newLiveControls = new ArrayList();
      graphics.SetClip(this.ClientRectangle);
    }
  }

  public virtual void DrawHeader(Graphics g, Size sizeHeader)
  {
    ListGrid.DW(nameof (DrawHeader));
    if (this.HeaderStyle == HeaderStyle.SuperFlat)
    {
      using (SolidBrush solidBrush = new SolidBrush(this.SuperFlatHeaderColor))
        g.FillRectangle((Brush) solidBrush, this.HeaderRect);
    }
    else if (this.HeaderStyle == HeaderStyle.Flat)
    {
      using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(this.HeaderRect, ControlPaint.Light(SystemColors.Control, 0.5f), SystemColors.Control, LinearGradientMode.Vertical))
        g.FillRectangle((Brush) linearGradientBrush, this.HeaderRect);
    }
    else
      g.FillRectangle(SystemBrushes.Control, this.HeaderRect);
    if (this.Columns.Count <= 0)
      return;
    int x = -this._hScrollBar.Value + this.HeaderRect.X;
    foreach (ListColumn column in (CollectionBase) this.Columns)
    {
      if (x + column.Width < 0)
      {
        x += column.Width;
      }
      else
      {
        if (x > this.HeaderRect.Right)
          break;
        if (column.Width > 0)
          this.DrawColumnHeader(g, new Rectangle(x, this.HeaderRect.Y, column.Width, this.HeaderHeight), column);
        x += column.Width;
      }
    }
  }

  public virtual void DrawColumnHeader(Graphics g, Rectangle bounds, ListColumn column)
  {
    switch (this._headerStyle)
    {
      case HeaderStyle.Normal:
        if (column.State != ColumnState.Pressed)
        {
          ControlPaint.DrawButton(g, bounds, ButtonState.Normal);
          break;
        }
        ControlPaint.DrawButton(g, bounds, ButtonState.Pushed);
        break;
      case HeaderStyle.Flat:
        Rectangle rect1 = new Rectangle(bounds.Location, bounds.Size);
        --rect1.Height;
        g.DrawRectangle(SystemPens.ControlDark, rect1);
        bounds.Inflate(-1, -1);
        if (column.State != ColumnState.Pressed)
        {
          g.DrawLine(SystemPens.ControlLightLight, rect1.Left + 1, rect1.Top + 1, rect1.Left + 1, rect1.Bottom - 1);
          g.DrawLine(SystemPens.ControlLightLight, rect1.Left + 1, rect1.Top + 1, rect1.Right - 1, rect1.Top + 1);
          break;
        }
        ++bounds.X;
        --bounds.Width;
        break;
      case HeaderStyle.SuperFlat:
        SolidBrush solidBrush = new SolidBrush(this.SuperFlatHeaderColor);
        g.FillRectangle((Brush) solidBrush, bounds);
        solidBrush.Dispose();
        break;
      case HeaderStyle.XP:
        if (this.ThemesAvailable)
        {
          IntPtr hdc = g.GetHdc();
          RECT rect2 = new RECT(bounds.X, bounds.Y, bounds.Right, bounds.Bottom);
          RECT clipRect = new RECT(bounds.X, bounds.Y, bounds.Right, bounds.Bottom);
          if (column.State == ColumnState.Normal)
            ThemeRoutines.DrawThemeBackground(this._theme, hdc, 1, 1, ref rect2, ref clipRect);
          else if (column.State == ColumnState.Pressed)
            ThemeRoutines.DrawThemeBackground(this._theme, hdc, 1, 3, ref rect2, ref clipRect);
          else if (column.State == ColumnState.Hot)
            ThemeRoutines.DrawThemeBackground(this._theme, hdc, 1, 2, ref rect2, ref clipRect);
          g.ReleaseHdc(hdc);
          break;
        }
        goto case HeaderStyle.Normal;
    }
    if (column.ImageIndex > -1 && this.ImageList != null && column.ImageIndex < this.ImageList.Images.Count)
    {
      bounds = this.DrawCellGraphic(g, bounds, this.ImageList.Images[column.ImageIndex], HorizontalAlignment.Left);
    }
    else
    {
      bounds.X += this.CellPaddingSize;
      bounds.Width -= this.CellPaddingSize;
    }
    this.DrawCellText(g, bounds, column.Text, column.TextAlignment, this.ForeColor, false, this.HeaderWordWrap);
  }

  public virtual void DrawRows(Graphics g, Brush brush)
  {
    ListGrid.DW(nameof (DrawRows));
    g.FillRectangle(brush, this.RowsClientRect);
    Rectangle rowsInnerClientRect;
    if (this.BackgroundImage != null)
    {
      if (this.BackgroundStretchToFit)
      {
        Graphics graphics = g;
        Image backgroundImage = this.BackgroundImage;
        rowsInnerClientRect = this.RowsInnerClientRect;
        int x = rowsInnerClientRect.X;
        rowsInnerClientRect = this.RowsInnerClientRect;
        int y = rowsInnerClientRect.Y;
        rowsInnerClientRect = this.RowsInnerClientRect;
        int width = rowsInnerClientRect.Width;
        rowsInnerClientRect = this.RowsInnerClientRect;
        int height = rowsInnerClientRect.Height;
        graphics.DrawImage(backgroundImage, x, y, width, height);
      }
      else
      {
        Graphics graphics = g;
        Image backgroundImage = this.BackgroundImage;
        rowsInnerClientRect = this.RowsInnerClientRect;
        int x = rowsInnerClientRect.X;
        rowsInnerClientRect = this.RowsInnerClientRect;
        int y = rowsInnerClientRect.Y;
        graphics.DrawImage(backgroundImage, x, y);
      }
    }
    int num1 = !this._vScrollBar.Visible ? 0 : this._vScrollBar.Value;
    Rectangle rowsRect = this.RowsRect with
    {
      Height = this.ItemHeight
    };
    for (int index = 0; index < this.VisibleRowsCount + 1 && index + num1 < this.Items.Count; ++index)
    {
      this.DrawRow(g, rowsRect, this.Items[index + num1], index + num1);
      rowsRect.Y += this.ItemHeight;
    }
    if (this.GridLineStyle != GridLineStyle.None)
      this.DrawGridLines(g, this.RowsInnerClientRect);
    if (!this.HotColumnTracking || this.HotColumnIndex == -1 || this.HotColumnIndex >= this.Columns.Count)
      return;
    int num2 = -this._hScrollBar.Value;
    for (int nColumnIndex = 0; nColumnIndex < this.HotColumnIndex; ++nColumnIndex)
      num2 += this.Columns[nColumnIndex].Width;
    int r = (int) this.HotTrackingColor.R;
    Color hotTrackingColor = this.HotTrackingColor;
    int g1 = (int) hotTrackingColor.G;
    hotTrackingColor = this.HotTrackingColor;
    int b = (int) hotTrackingColor.B;
    using (Brush brush1 = (Brush) new SolidBrush(Color.FromArgb(75, r, g1, b)))
    {
      Graphics graphics = g;
      Brush brush2 = brush1;
      rowsInnerClientRect = this.RowsInnerClientRect;
      int x = rowsInnerClientRect.X + num2;
      rowsInnerClientRect = this.RowsInnerClientRect;
      int y = rowsInnerClientRect.Y;
      int width = this.Columns[this.HotColumnIndex].Width + 1;
      rowsInnerClientRect = this.RowsInnerClientRect;
      int height = rowsInnerClientRect.Height - 1;
      graphics.FillRectangle(brush2, x, y, width, height);
    }
  }

  public virtual void DrawRow(Graphics g, Rectangle bounds, ListItem item, int itemIndex)
  {
    ListGrid.DW(nameof (DrawRow));
    Color color;
    Rectangle rowsInnerClientRect;
    if (item.Selected && this.Selectable)
    {
      color = this.SelectionColor;
      int r = (int) color.R;
      color = this.SelectionColor;
      int g1 = (int) color.G;
      color = this.SelectionColor;
      int b = (int) color.B;
      SolidBrush solidBrush1 = new SolidBrush(Color.FromArgb((int) byte.MaxValue, r, g1, b));
      if (!this.FullRowSelect)
      {
        int width = -this._hScrollBar.Value + this.Columns.Width;
        g.FillRectangle((Brush) solidBrush1, this.RowsInnerClientRect.X, bounds.Y, width, bounds.Height);
      }
      else
      {
        Graphics graphics = g;
        SolidBrush solidBrush2 = solidBrush1;
        int x = this.RowsInnerClientRect.X;
        int y = bounds.Y;
        rowsInnerClientRect = this.RowsInnerClientRect;
        int width = rowsInnerClientRect.Width;
        int height = bounds.Height;
        graphics.FillRectangle((Brush) solidBrush2, x, y, width, height);
      }
      solidBrush1.Dispose();
    }
    else
    {
      int argb1 = item.BackColor.ToArgb();
      color = this.BackColor;
      int argb2 = color.ToArgb();
      if (argb1 != argb2 && item.BackColor != Color.White)
      {
        SolidBrush solidBrush3 = new SolidBrush(item.BackColor);
        Graphics graphics = g;
        SolidBrush solidBrush4 = solidBrush3;
        int x = this.RowsInnerClientRect.X;
        int y = bounds.Y;
        rowsInnerClientRect = this.RowsInnerClientRect;
        int width = rowsInnerClientRect.Width;
        int height = bounds.Height;
        graphics.FillRectangle((Brush) solidBrush4, x, y, width, height);
        solidBrush3.Dispose();
      }
      else if (this.AlternatingColors && this.Items.FindItemIndex(item) % 2 > 0)
      {
        SolidBrush solidBrush5 = new SolidBrush(this.AlternateBackground);
        if (!this.FullRowSelect)
        {
          int width = -this._hScrollBar.Value + this.Columns.Width;
          g.FillRectangle((Brush) solidBrush5, this.RowsInnerClientRect.X, bounds.Y, width, bounds.Height);
        }
        else
        {
          Graphics graphics = g;
          SolidBrush solidBrush6 = solidBrush5;
          int x = this.RowsInnerClientRect.X;
          int y = bounds.Y;
          rowsInnerClientRect = this.RowsInnerClientRect;
          int width = rowsInnerClientRect.Width;
          int height = bounds.Height;
          graphics.FillRectangle((Brush) solidBrush6, x, y, width, height);
        }
        solidBrush5.Dispose();
      }
    }
    int x1 = -this._hScrollBar.Value + this.BorderPadding;
    for (int index = 0; index < this.Columns.Count; ++index)
    {
      Rectangle rectSubItem = new Rectangle(x1, bounds.Y, this.Columns[index].Width, bounds.Height);
      if (rectSubItem.Right >= 0)
      {
        int left = rectSubItem.Left;
        rowsInnerClientRect = this.RowsInnerClientRect;
        int right = rowsInnerClientRect.Right;
        if (left <= right)
          this.DrawSubItem(g, rectSubItem, item, item.SubItems[index], index);
      }
      x1 += this.Columns[index].Width;
    }
    if (itemIndex == this.HotItemIndex && this.HotItemTracking)
    {
      color = this.HotTrackingColor;
      int r = (int) color.R;
      color = this.HotTrackingColor;
      int g2 = (int) color.G;
      color = this.HotTrackingColor;
      int b = (int) color.B;
      Brush brush1 = (Brush) new SolidBrush(Color.FromArgb(75, r, g2, b));
      Graphics graphics = g;
      Brush brush2 = brush1;
      rowsInnerClientRect = this.RowsInnerClientRect;
      int x2 = rowsInnerClientRect.X;
      int y = bounds.Y;
      rowsInnerClientRect = this.RowsInnerClientRect;
      int width = rowsInnerClientRect.Width;
      int height = bounds.Height;
      graphics.FillRectangle(brush2, x2, y, width, height);
      brush1.Dispose();
    }
    if (item.RowBorderSize > 0)
    {
      Pen pen = new Pen(item.RowBorderColor, (float) item.RowBorderSize);
      pen.Alignment = PenAlignment.Inset;
      g.DrawRectangle(pen, bounds);
      pen.Dispose();
    }
    if (!this.Selectable || !this.ShowFocusRect || this.FocusedItem != item)
      return;
    Graphics graphics1 = g;
    rowsInnerClientRect = this.RowsInnerClientRect;
    int x3 = rowsInnerClientRect.X + 1;
    int y1 = bounds.Y;
    rowsInnerClientRect = this.RowsInnerClientRect;
    int width1 = rowsInnerClientRect.Width - 1;
    int height1 = bounds.Height;
    Rectangle rectangle = new Rectangle(x3, y1, width1, height1);
    ControlPaint.DrawFocusRectangle(graphics1, rectangle);
  }

  /// <summary>Draw Sub Item (Cell) at location specified</summary>
  /// <param name="graphicsSubItem"></param>
  /// <param name="rectSubItem"></param>
  /// <param name="item"></param>
  /// <param name="subItem"></param>
  /// <param name="nColumn"></param>
  public virtual void DrawSubItem(
    Graphics graphicsSubItem,
    Rectangle rectSubItem,
    ListItem item,
    ListSubItem subItem,
    int nColumn)
  {
    ListGrid.DW(nameof (DrawSubItem));
    Rectangle rectangle1 = new Rectangle(rectSubItem.X, rectSubItem.Y, rectSubItem.Width, rectSubItem.Height);
    if (subItem.Control != null && !subItem.ForceText)
    {
      Control control = subItem.Control;
      if (control.Parent != this)
      {
        control.Parent = (Control) this;
        control.BringToFront();
      }
      Rectangle rectangle2 = new Rectangle(rectangle1.Location, rectangle1.Size);
      rectangle2.Inflate(-this.CellPaddingSize, -this.CellPaddingSize);
      control.GetType();
      PropertyInfo property = control.GetType().GetProperty("PreferredHeight");
      if (property != (PropertyInfo) null)
      {
        int num = (int) property.GetValue((object) control, (object[]) null);
        if (num + this.CellPaddingSize * 2 > this.ItemHeight && this.AutoHeight)
          this.ItemHeight = num + this.CellPaddingSize * 2;
        rectangle2.Y = rectangle1.Y + (rectangle1.Height - num) / 2;
      }
      this._newLiveControls.Add((object) control);
      if (this._liveControls.Contains((object) control))
        this._liveControls.Remove((object) control);
      if (control.Bounds.ToString() != rectangle2.ToString())
        control.Bounds = rectangle2;
      if (control.Visible)
        return;
      control.Visible = true;
    }
    else
    {
      Color color = subItem.BackColor;
      int argb1 = color.ToArgb();
      color = this.BackColor;
      int argb2 = color.ToArgb();
      if (argb1 != argb2 && !item.Selected && subItem.BackColor != Color.White)
      {
        SolidBrush solidBrush = new SolidBrush(subItem.BackColor);
        graphicsSubItem.FillRectangle((Brush) solidBrush, rectSubItem);
        solidBrush.Dispose();
      }
      if (this.Columns[nColumn].CheckBoxes)
        rectSubItem = this.DrawCheckBox(graphicsSubItem, rectSubItem, subItem.Checked);
      if (subItem.ImageIndex > -1 && this.ImageList != null && subItem.ImageIndex < this.ImageList.Images.Count)
        rectSubItem = this.DrawCellGraphic(graphicsSubItem, rectSubItem, this.ImageList.Images[subItem.ImageIndex], subItem.ImageAlignment);
      Color textColor;
      if (item.Selected && this.Selectable)
      {
        textColor = this.SelectedTextColor;
      }
      else
      {
        textColor = this.ForeColor;
        color = item.ForeColor;
        int argb3 = color.ToArgb();
        color = this.ForeColor;
        int argb4 = color.ToArgb();
        if (argb3 != argb4)
        {
          textColor = item.ForeColor;
        }
        else
        {
          color = subItem.ForeColor;
          int argb5 = color.ToArgb();
          color = this.ForeColor;
          int argb6 = color.ToArgb();
          if (argb5 != argb6)
            textColor = subItem.ForeColor;
        }
      }
      this.DrawCellText(graphicsSubItem, rectSubItem, subItem.Text, this.Columns[nColumn].TextAlignment, textColor, item.Selected, this.ItemWordWrap);
      subItem.LastCellRect = rectSubItem;
    }
  }

  public virtual Rectangle DrawCheckBox(Graphics graphicsCell, Rectangle rectCell, bool bChecked)
  {
    int num1 = 13 + this.CellPaddingSize * 2;
    int num2 = 13 + this.CellPaddingSize * 2;
    this.MaxHeight = num1;
    if (num2 > rectCell.Width || num1 > rectCell.Height)
      return rectCell;
    int y = rectCell.Y + this.CellPaddingSize + (rectCell.Height - num1) / 2;
    int x = rectCell.X + this.CellPaddingSize;
    if (bChecked)
      graphicsCell.DrawImage((Image) ListGrid._checkedImage, x, y);
    else
      graphicsCell.DrawImage((Image) ListGrid._uncheckedImage, x, y);
    rectCell.Width -= 13 + this.CellPaddingSize * 2;
    rectCell.X += num2;
    return rectCell;
  }

  public virtual Rectangle DrawCellGraphic(
    Graphics graphicsCell,
    Rectangle rectCell,
    Image img,
    HorizontalAlignment alignment)
  {
    int num1 = img.Height + this.CellPaddingSize;
    int num2 = img.Width + this.CellPaddingSize;
    this.MaxHeight = num1;
    if (num2 > rectCell.Width || num1 > rectCell.Height)
      return rectCell;
    switch (alignment)
    {
      case HorizontalAlignment.Left:
        int y1 = rectCell.Y + this.CellPaddingSize + (rectCell.Height - num1) / 2;
        int x1 = rectCell.X + this.CellPaddingSize;
        graphicsCell.DrawImage(img, x1, y1);
        rectCell.Width -= img.Width + this.CellPaddingSize * 2;
        rectCell.X += num2;
        break;
      case HorizontalAlignment.Right:
        int y2 = rectCell.Y + this.CellPaddingSize + (rectCell.Height - num1) / 2;
        int x2 = rectCell.Right - num2;
        graphicsCell.DrawImage(img, x2, y2);
        rectCell.Width -= num2;
        break;
      case HorizontalAlignment.Center:
        int y3 = rectCell.Y + this.CellPaddingSize + (rectCell.Height - num1) / 2;
        int x3 = rectCell.X + this.CellPaddingSize + (rectCell.Width - num2) / 2;
        graphicsCell.DrawImage(img, x3, y3);
        rectCell.Width = 0;
        break;
    }
    return rectCell;
  }

  public virtual void DrawCellText(
    Graphics graphicsCell,
    Rectangle rectCell,
    string strCellText,
    ContentAlignment alignment,
    Color textColor,
    bool bSelected,
    bool bWordWrap)
  {
    int nWidth = rectCell.Width - this.CellPaddingSize * 2;
    int height = rectCell.Height;
    int cellPaddingSize = this.CellPaddingSize;
    using (SolidBrush solidBrush = new SolidBrush(textColor))
    {
      StringFormat stringFormat = new StringFormat();
      stringFormat.Alignment = StringHelpers.ConvertContentAlignmentToHorizontalStringAlignment(alignment);
      stringFormat.LineAlignment = StringHelpers.ConvertContentAlignmentToVerticalStringAlignment(alignment);
      SizeF sizeF;
      if (bWordWrap)
      {
        stringFormat.FormatFlags = (StringFormatFlags) 0;
        sizeF = graphicsCell.MeasureString(strCellText, this.Font, (PointF) new Point(this.CellPaddingSize, this.CellPaddingSize), stringFormat);
      }
      else
      {
        stringFormat.FormatFlags = StringFormatFlags.NoWrap;
        sizeF = graphicsCell.MeasureString(strCellText, this.Font, (PointF) new Point(this.CellPaddingSize, this.CellPaddingSize), stringFormat);
        if ((double) sizeF.Width > (double) nWidth)
          strCellText = StringHelpers.TruncateString(strCellText, nWidth, graphicsCell, this.Font);
      }
      this.MaxHeight = (int) sizeF.Height + this.CellPaddingSize * 2;
      rectCell.Inflate(-this.CellPaddingSize, 0);
      graphicsCell.DrawString(strCellText, this.Font, (Brush) solidBrush, (RectangleF) rectCell, stringFormat);
    }
  }

  public virtual void DrawGridLines(Graphics g, Rectangle rect)
  {
    int num1 = this._vScrollBar.Value;
    int y = rect.Y;
    using (Pen pen = new Pen(this.GridColor))
    {
      pen.DashStyle = this.GridLineStyle != GridLineStyle.Dashed ? (this.GridLineStyle != GridLineStyle.Solid ? DashStyle.Solid : DashStyle.Solid) : DashStyle.Dash;
      if (this.GridLines == GridLines.Both || this.GridLines == GridLines.Horizontal)
      {
        int num2 = this.VisibleRowsCount + 1;
        if (this.GridTypes == GridType.Exists && this.VisibleRowsCount > this.Count)
          num2 = this.Count;
        for (int index = 0; index < num2; ++index)
        {
          y += this.ItemHeight;
          g.DrawLine(pen, rect.X, y, this.Columns.Width + rect.X, y);
        }
      }
      if (this.GridLines != GridLines.Both && this.GridLines != GridLines.Vertical)
        return;
      int num3 = -this._hScrollBar.Value;
      int y2 = rect.Bottom;
      if (this.GridTypes == GridType.Exists)
        y2 = rect.Y + this.Items.Count * this.ItemHeight;
      g.DrawLine(pen, rect.X + num3, rect.Y, rect.X + num3, y2);
      for (int nColumnIndex = 0; nColumnIndex < this.Columns.Count; ++nColumnIndex)
      {
        num3 += this.Columns[nColumnIndex].Width;
        g.DrawLine(pen, rect.X + num3, rect.Y, rect.X + num3, y2);
      }
    }
  }

  private void OnScroll(object sender, ScrollEventArgs e)
  {
    this.DestroyActivatedEmbedded();
    this.Invalidate();
  }

  /// <summary>Recalculate scroll bars and control size</summary>
  private void RecalcScroll()
  {
    int num = 0;
    bool flag;
    do
    {
      ListGrid.DW("Begin scrolbar updates loop");
      flag = false;
      int width1 = this.Columns.Width;
      Rectangle rowsInnerClientRect = this.RowsInnerClientRect;
      int width2 = rowsInnerClientRect.Width;
      if (width1 > width2 && !this._hScrollBar.Visible)
      {
        this._hScrollBar.mVisible = true;
        this._hScrollBar.Value = 0;
        flag = true;
        ListGrid.DI("Calling Invalidate From RecalcScroll");
        this.Invalidate();
        ListGrid.DW("showing hscrollbar");
      }
      int width3 = this.Columns.Width;
      rowsInnerClientRect = this.RowsInnerClientRect;
      int width4 = rowsInnerClientRect.Width;
      if (width3 <= width4 && this._hScrollBar.Visible)
      {
        this._hScrollBar.mVisible = false;
        this._hScrollBar.Value = 0;
        flag = true;
        ListGrid.DI("Calling Invalidate From RecalcScroll");
        this.Invalidate();
        ListGrid.DW("hiding hscrollbar");
      }
      int totalRowHeight1 = this.TotalRowHeight;
      rowsInnerClientRect = this.RowsInnerClientRect;
      int height1 = rowsInnerClientRect.Height;
      if (totalRowHeight1 > height1 && !this._vScrollBar.Visible)
      {
        this._vScrollBar.mVisible = true;
        this._hScrollBar.Value = 0;
        flag = true;
        ListGrid.DI("Calling Invalidate From RecalcScroll");
        this.Invalidate();
        ListGrid.DW("showing vscrollbar");
      }
      int totalRowHeight2 = this.TotalRowHeight;
      rowsInnerClientRect = this.RowsInnerClientRect;
      int height2 = rowsInnerClientRect.Height;
      if (totalRowHeight2 <= height2 && this._vScrollBar.Visible)
      {
        this._vScrollBar.mVisible = false;
        this._vScrollBar.Value = 0;
        flag = true;
        ListGrid.DI("Calling Invalidate From RecalcScroll");
        this.Invalidate();
        ListGrid.DW("hiding vscrollbar");
      }
      ListGrid.DW("End scrolbar updates loop");
    }
    while (++num <= 4 && flag);
    Rectangle rowsInnerClientRect1 = this.RowsInnerClientRect;
    if (this._vScrollBar.Visible)
    {
      this._vScrollBar.mTop = rowsInnerClientRect1.Y;
      this._vScrollBar.mLeft = rowsInnerClientRect1.Right;
      this._vScrollBar.mHeight = rowsInnerClientRect1.Height;
      this._vScrollBar.mLargeChange = this.VisibleRowsCount;
      this._vScrollBar.mMaximum = this.Count - 1;
      if (this._vScrollBar.Value + this.VisibleRowsCount > this.Count)
      {
        ListGrid.DW("Changing vpanel value");
        this._vScrollBar.Value = this.Count - this.VisibleRowsCount;
      }
    }
    if (this._hScrollBar.Visible)
    {
      this._hScrollBar.mLeft = rowsInnerClientRect1.Left;
      this._hScrollBar.mTop = rowsInnerClientRect1.Bottom;
      this._hScrollBar.mWidth = rowsInnerClientRect1.Width;
      this._hScrollBar.mLargeChange = rowsInnerClientRect1.Width;
      this._hScrollBar.mMaximum = this.Columns.Width;
      if (this._hScrollBar.Value + this._hScrollBar.LargeChange > this._hScrollBar.Maximum)
      {
        ListGrid.DW("Changing vpanel value");
        this._hScrollBar.Value = this._hScrollBar.Maximum - this._hScrollBar.LargeChange;
      }
    }
    int borderPadding = this.BorderPadding;
    if (this._hScrollBar.Visible && this._vScrollBar.Visible)
      this._cornerBox = new Rectangle(this._hScrollBar.Right, this._vScrollBar.Bottom, this._vScrollBar.Width, this._hScrollBar.Height);
    else
      this._cornerBox = Rectangle.Empty;
  }

  /// <summary>Handle vertical scroll bar movement</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void vPanelScrollBar_Scroll(object sender, ScrollEventArgs e)
  {
    ListGrid.DW(nameof (vPanelScrollBar_Scroll));
    ListGrid.DI("Calling Invalidate From vPanelScrollBar_Scroll");
    this.Invalidate();
  }

  /// <summary>Handle horizontal scroll bar movement</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void hPanelScrollBar_Scroll(object sender, ScrollEventArgs e)
  {
    ListGrid.DW(nameof (hPanelScrollBar_Scroll));
    ListGrid.DI("Calling Invalidate From hPanelScrollBar_Scroll");
    this.Invalidate();
  }

  /// <summary>
  /// OnDoubleclick
  /// 
  /// if someone double clicks on an area, we need to start a control potentially
  /// </summary>
  /// <param name="e"></param>
  protected override void OnDoubleClick(EventArgs e)
  {
    ListGrid.DW("GlacialList.OnDoubleClick");
    Point client = this.PointToClient(Cursor.Position);
    int nItem = 0;
    int nColumn = 0;
    int nCellX = 0;
    int nCellY = 0;
    ListRegion listRegion;
    this.InterpretCoords(client.X, client.Y, out listRegion, out nCellX, out nCellY, out nItem, out nColumn, out ListState _);
    if (listRegion == ListRegion.Client && nColumn < this.Columns.Count)
      this.ActivateEmbeddedControl(nColumn, this.Items[nItem], this.Items[nItem].SubItems[nColumn]);
    base.OnDoubleClick(e);
  }

  /// <summary>
  /// had to put this routine in because of overriden protection level being unchangable
  /// </summary>
  /// <param name="Sender"></param>
  /// <param name="e"></param>
  protected void OnMouseDownFromSubItem(object Sender, MouseEventArgs e)
  {
    ListGrid.DW(nameof (OnMouseDownFromSubItem));
    Point mousePosition = Control.MousePosition;
    int x = mousePosition.X;
    mousePosition = Control.MousePosition;
    int y = mousePosition.Y;
    Point client = this.PointToClient(new Point(x, y));
    e = new MouseEventArgs(e.Button, e.Clicks, client.X, client.Y, e.Delta);
    this.OnMouseDown(e);
  }

  /// <summary>Mouse has left the control area</summary>
  /// <param name="e"></param>
  protected override void OnMouseLeave(EventArgs e)
  {
    this.Columns.ClearHotStates();
    this.HotItemIndex = -1;
    this.HotColumnIndex = -1;
    base.OnMouseLeave(e);
  }

  /// <summary>mouse button pressed</summary>
  /// <param name="e"></param>
  protected override void OnMouseDown(MouseEventArgs e)
  {
    ListGrid.DW("GlacialList_MouseDown");
    int nItem = 0;
    int nColumn = 0;
    int nCellX = 0;
    int nCellY = 0;
    ListState nState;
    this.InterpretCoords(e.X, e.Y, out ListRegion _, out nCellX, out nCellY, out nItem, out nColumn, out nState);
    if (e.Button == MouseButtons.Right)
    {
      base.OnMouseDown(e);
    }
    else
    {
      switch (nState)
      {
        case ListState.Selecting:
          if (nColumn < this.Columns.Count && this.Columns[nColumn].CheckBoxes && nCellX > this.CellPaddingSize && nCellX < this.CellPaddingSize + 13 && nCellY > this.CellPaddingSize && nCellY < this.CellPaddingSize + 13)
            this.Items[nItem].SubItems[nColumn].Checked = !this.Items[nItem].SubItems[nColumn].Checked;
          this._listState = ListState.Selecting;
          this.FocusedItem = this.Items[nItem];
          if ((Control.ModifierKeys & Keys.Control) == Keys.Control && this.AllowMultiselect)
          {
            this._lastSelectionIndex = nItem;
            this.Items[nItem].Selected = !this.Items[nItem].Selected;
            base.OnMouseDown(e);
            return;
          }
          if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift && this.AllowMultiselect)
          {
            this.Items.ClearSelection();
            if (this._lastSelectionIndex >= 0)
            {
              int lastSelectionIndex = this._lastSelectionIndex;
              do
              {
                this.Items[lastSelectionIndex].Selected = true;
                if (lastSelectionIndex > nItem)
                  --lastSelectionIndex;
                if (lastSelectionIndex < nItem)
                  ++lastSelectionIndex;
              }
              while (lastSelectionIndex != nItem);
              this.Items[lastSelectionIndex].Selected = true;
            }
            base.OnMouseDown(e);
            return;
          }
          this.Items.ClearSelection(this.Items[nItem]);
          if (this._lastSelectionIndex < this.Count && this._lastSubSelectionIndex < this.Columns.Count)
            this.Items[this._lastSelectionIndex].SubItems[this._lastSubSelectionIndex].Selected = false;
          if (!this.FullRowSelect && nItem < this.Count && nColumn < this.Columns.Count)
            this.Items[nItem].SubItems[nColumn].Selected = true;
          this._lastSelectionIndex = nItem;
          this._lastSubSelectionIndex = nColumn;
          this.Items[nItem].Selected = true;
          break;
        case ListState.ColumnSelect:
          this._listState = ListState.Normal;
          if (this.SortType != SortType.None)
          {
            this.Columns[nColumn].State = ColumnState.Pressed;
            this.SortColumn(nColumn, true);
          }
          if (this.ColumnClick != null)
            this.ColumnClick((object) this, new ClickEventArgs(nItem, nColumn));
          base.OnMouseDown(e);
          return;
        case ListState.ColumnResizing:
          Cursor.Current = Cursors.VSplit;
          this._listState = ListState.ColumnResizing;
          this._columnResizeAnchor = new Point(this.GetColumnScreenX(nColumn), e.Y);
          this._resizeColumnNumber = nColumn;
          base.OnMouseDown(e);
          return;
      }
      base.OnMouseDown(e);
    }
  }

  /// <summary>when mouse moves</summary>
  /// <param name="e"></param>
  protected override void OnMouseMove(MouseEventArgs e)
  {
    ListGrid.DW("GlacialList_MouseMove");
    try
    {
      if (this._listState == ListState.ColumnResizing)
      {
        Cursor.Current = Cursors.VSplit;
        int num = e.X - this._columnResizeAnchor.X - this.BorderPadding;
        if (num <= 0)
          num = 0;
        this.Columns[this._resizeColumnNumber].Width = num;
        this.OnMove((EventArgs) e);
        return;
      }
      int nItem = 0;
      int nColumn = 0;
      int nCellX = 0;
      int nCellY = 0;
      ListState nState;
      this.InterpretCoords(e.X, e.Y, out ListRegion _, out nCellX, out nCellY, out nItem, out nColumn, out nState);
      if (nState == ListState.ColumnResizing)
      {
        Cursor.Current = Cursors.VSplit;
        this.OnMove((EventArgs) e);
        return;
      }
      Cursor.Current = Cursors.Arrow;
    }
    catch (Exception ex)
    {
    }
    this.OnMove((EventArgs) e);
  }

  /// <summary>mouse up</summary>
  /// <param name="e"></param>
  protected override void OnMouseUp(MouseEventArgs e)
  {
    ListGrid.DW("MouseUp");
    Cursor.Current = Cursors.Arrow;
    this.Columns.ClearStates();
    int nItem = 0;
    int nColumn = 0;
    int nCellX = 0;
    int nCellY = 0;
    this.InterpretCoords(e.X, e.Y, out ListRegion _, out nCellX, out nCellY, out nItem, out nColumn, out ListState _);
    this._listState = ListState.Normal;
    base.OnMouseUp(e);
  }

  /// <summary>Clicked Event Handler delegate definition</summary>
  public delegate void ClickedEventHandler(object source, ClickEventArgs e);

  /// <summary>Hover Event delegate definition</summary>
  public delegate void HoverEventDelegate(object source, HoverEventArgs e);

  private enum WIN32Codes
  {
    WM_SETREDRAW = 11, // 0x0000000B
    WM_CANCELMODE = 31, // 0x0000001F
    WM_NOTIFY = 78, // 0x0000004E
    WM_GETDLGCODE = 135, // 0x00000087
    WM_KEYDOWN = 256, // 0x00000100
    WM_KEYUP = 257, // 0x00000101
    WM_CHAR = 258, // 0x00000102
    WM_SYSKEYDOWN = 260, // 0x00000104
    WM_SYSKEYUP = 261, // 0x00000105
    WM_COMMAND = 273, // 0x00000111
    WM_MENUCHAR = 288, // 0x00000120
    WM_MOUSEMOVE = 512, // 0x00000200
    WM_LBUTTONDOWN = 513, // 0x00000201
    WM_MOUSELAST = 522, // 0x0000020A
    WM_USER = 1024, // 0x00000400
    WM_REFLECT = 8192, // 0x00002000
  }

  private enum DialogCodes
  {
    DLGC_WANTARROWS = 1,
    DLGC_WANTTAB = 2,
    DLGC_WANTALLKEYS = 4,
    DLGC_WANTMESSAGE = 4,
    DLGC_HASSETSEL = 8,
    DLGC_DEFPUSHBUTTON = 16, // 0x00000010
    DLGC_UNDEFPUSHBUTTON = 32, // 0x00000020
    DLGC_RADIOBUTTON = 64, // 0x00000040
    DLGC_WANTCHARS = 128, // 0x00000080
    DLGC_STATIC = 256, // 0x00000100
    DLGC_BUTTON = 8192, // 0x00002000
  }
}
