
// Type: Intermech.Client.Core.Organizer.Scheduler
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// 
/// </summary>
[DefaultEvent("LoadItems")]
public class Scheduler : ScrollableControl
{
  private DayOfWeek _firstDayOfWeek = DayOfWeek.Monday;
  private SchedulerHeader _header;
  private string _caption = string.Empty;
  private CalendarRenderer _renderer;
  private CalendarState _state;
  private CalendarTimeScale _timeScale = CalendarTimeScale.ThirtyMinutes;
  private DateTime _selEnd;
  private DateTime _selStart;
  private DateTime _viewEnd;
  private DateTime _viewStart;
  private CalendarWeek[] _weeks;
  private CalendarDay[] _days;
  private CalendarDaysMode _daysMode;
  private Dictionary<int, List<int>> _excludedDays = new Dictionary<int, List<int>>();
  private int _maxFullDays = 7;
  private int _maxViewDays = 35;
  private List<SchedulerSelectableElement> _selectedElements = new List<SchedulerSelectableElement>();
  private ICalendarSelectableElement _selectedElementEnd;
  private ICalendarSelectableElement _selectedElementStart;
  private Rectangle _selectedElementSquare;
  private Rectangle _currOverflowBounds = Rectangle.Empty;
  private SchedulerItemCollection _items;
  private CalendarItem _editModeItem;
  private CalendarItem itemOnState;
  private bool itemOnStateChanged;
  private string _itemsDateFormat = "dd/MMM";
  private string _itemsTimeFormat = "HH:mm tt";
  private CalendarHighlightRange[] _highlightRanges;
  private int _timeUnitsOffset;
  private bool _allowItemEdit = true;
  private bool _allowItemResize = true;
  private bool _selectionChanged;
  private bool _creatingItem;
  private bool _finalizingEdition;
  private Icon _repetitionIco;
  private VScrollBar _scroll = new VScrollBar();
  private TextBox _textBox;

  /// <summary>
  /// Returns a value indicating if two date ranges intersect.
  /// </summary>
  /// <param name="startA"></param>
  /// <param name="endA"></param>
  /// <param name="startB"></param>
  /// <param name="endB"></param>
  /// <returns></returns>
  public static bool DateIntersects(
    DateTime startA,
    DateTime endA,
    DateTime startB,
    DateTime endB)
  {
    return startB < endA && startA < endB;
  }

  /// <summary>Возможность редактирования элемента планировщика.</summary>
  [DefaultValue(true)]
  [CustomDescription("Attribute.Client.Core_241")]
  public bool AllowItemEdit
  {
    get => this._allowItemEdit;
    set => this._allowItemEdit = value;
  }

  /// <summary>
  /// Возможность пользователя изменять размеры элемента планировщика.
  /// </summary>
  [DefaultValue(true)]
  [CustomDescription("Attribute.Client.Core_242")]
  public bool AllowItemResize
  {
    get => this._allowItemResize;
    set => this._allowItemResize = value;
  }

  /// <summary>Строка, отображающая выбраные день, месяц и год.</summary>
  [Browsable(false)]
  public string Caption => this._caption;

  /// <summary>Gets the days visible on the ccurrent view.</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public CalendarDay[] Days => this._days;

  /// <summary>Gets the union of day body rectangles.</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Rectangle DaysBodyRectangle
  {
    get => Rectangle.Union(this.Days[0].BodyBounds, this.Days[this.Days.Length - 1].BodyBounds);
  }

  /// <summary>Gets the mode in which days are drawn.</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public CalendarDaysMode DaysMode => this._daysMode;

  /// <summary>
  /// Gets if the calendar is currently in edit mode of some item.
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool EditMode => this.TextBox != null;

  /// <summary>Gets the item being edited (if any).</summary>
  public CalendarItem EditModeItem => this._editModeItem;

  /// <summary>
  /// 
  /// </summary>
  public Dictionary<int, List<int>> ExcludedDays
  {
    get => this._excludedDays;
    set => this._excludedDays = value != null ? value : new Dictionary<int, List<int>>(0);
  }

  /// <summary>Первый день недели.</summary>
  [DefaultValue(DayOfWeek.Monday)]
  [CustomDescription("Attribute.Client.Core_243")]
  public DayOfWeek FirstDayOfWeek
  {
    set => this._firstDayOfWeek = value;
    get => this._firstDayOfWeek;
  }

  /// <summary>
  /// Верхняя панель размещения кнопок переключения режима выделения.
  /// </summary>
  [Browsable(false)]
  internal SchedulerHeader Header => this._header;

  /// <summary>
  /// Gets or sets the time ranges that should be highlighted as work-time.
  /// This ranges are week based.
  /// </summary>
  public CalendarHighlightRange[] HighlightRanges
  {
    get => this._highlightRanges;
    set
    {
      this._highlightRanges = value;
      this.UpdateHighlights();
    }
  }

  /// <summary>Gets the collection of items currently on the view.</summary>
  /// <remarks>
  /// This collection changes every time the view is changed.
  /// </remarks>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public SchedulerItemCollection Items => this._items;

  /// <summary>
  /// Gets or sets the format in which time is shown in the items, when applicable.
  /// </summary>
  [DefaultValue("dd/MMM")]
  public string ItemsDateFormat
  {
    get => this._itemsDateFormat;
    set => this._itemsDateFormat = value;
  }

  /// <summary>
  /// Gets or sets the format in which time is shown in the items, when applicable.
  /// </summary>
  [DefaultValue("HH:mm tt")]
  public string ItemsTimeFormat
  {
    get => this._itemsTimeFormat;
    set => this._itemsTimeFormat = value;
  }

  /// <summary>
  /// Максимальное количество полных дней отображаемых в планировщике.
  /// </summary>
  [DefaultValue(7)]
  public int MaxFullDays
  {
    get => this._maxFullDays;
    set => this._maxFullDays = value;
  }

  /// <summary>
  /// Максимальное количество дней отображаемых в планировщике.
  /// Количество дней должно быть кратно 7.
  /// </summary>
  [DefaultValue(35)]
  public int MaxViewDays
  {
    get => this._maxViewDays;
    set => this._maxViewDays = value / 7 * 7;
  }

  /// <summary>
  /// Gets or sets the <see cref="T:Intermech.Client.Core.Organizer.CalendarRenderer" /> of the <see cref="T:Intermech.Client.Core.Organizer.Scheduler" />.
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public CalendarRenderer Renderer
  {
    get => this._renderer;
    set
    {
      this._renderer = value;
      if (value == null || !this.Created)
        return;
      value.OnInitialize(new CalendarRendererEventArgs((Scheduler) null, (Graphics) null, Rectangle.Empty));
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public Icon RepetitionIco => this._repetitionIco;

  /// <summary>
  /// 
  /// </summary>
  public VScrollBar ScrollBar => this._scroll;

  /// <summary>Gets the last selected element.</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ICalendarSelectableElement SelectedElementEnd
  {
    get => this._selectedElementEnd;
    set
    {
      this._selectedElementEnd = value;
      this.UpdateSelectionElements();
    }
  }

  /// <summary>Gets the first selected element.</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ICalendarSelectableElement SelectedElementStart
  {
    get => this._selectedElementStart;
    set
    {
      this._selectedElementStart = value;
      this.UpdateSelectionElements();
    }
  }

  /// <summary>Коллекция выделенных элементов.</summary>
  /// <returns></returns>
  [Browsable(false)]
  public List<CalendarItem> SelectedItems
  {
    get
    {
      List<CalendarItem> selectedItems = new List<CalendarItem>(this.Items.Count);
      foreach (CalendarItem calendarItem in (List<CalendarItem>) this.Items)
      {
        if (calendarItem.Selected)
          selectedItems.Add(calendarItem);
      }
      return selectedItems;
    }
  }

  /// <summary>
  /// Gets or sets the end date-time of the view's selection.
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public DateTime SelectionEnd
  {
    get => this._selEnd;
    set => this._selEnd = value;
  }

  /// <summary>
  /// Gets or sets the start date-time of the view's selection.
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public DateTime SelectionStart
  {
    get => this._selStart;
    set => this._selStart = value;
  }

  /// <summary>Gets or sets the state of the calendar.</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public CalendarState State => this._state;

  /// <summary>Gets the TextBox of the edit mode.</summary>
  internal TextBox TextBox
  {
    get => this._textBox;
    set => this._textBox = value;
  }

  /// <summary>
  /// Gets or sets the <see cref="T:Intermech.Client.Core.Organizer.CalendarTimeScale" /> for visualization.
  /// </summary>
  [DefaultValue(CalendarTimeScale.ThirtyMinutes)]
  public CalendarTimeScale TimeScale
  {
    get => this._timeScale;
    set
    {
      if (this._timeScale == value)
        return;
      this._timeScale = value;
      if (this.Days != null)
      {
        for (int index = 0; index < this.Days.Length; ++index)
          this.Days[index].UpdateUnits();
      }
      this.Renderer.PerformLayout();
      this.Refresh();
    }
  }

  /// <summary>Gets or sets the offset of scrolled units.</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int TimeUnitsOffset
  {
    get => this._timeUnitsOffset;
    set
    {
      if (this._timeUnitsOffset == value)
        return;
      this._timeUnitsOffset = value;
      this.Renderer.PerformLayout();
      this.Invalidate();
    }
  }

  /// <summary>Gets or sets the end date-time of the current view.</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public DateTime ViewEnd
  {
    get => this._viewEnd;
    set
    {
      this._viewEnd = value.Date.Add(new TimeSpan(23, 59, 59));
      this.UpdateDaysAndWeeks();
      this.Renderer.PerformLayout();
      this.Invalidate();
      this.ReloadItems();
      this.CreateCaption();
    }
  }

  /// <summary>Gets or sets the start date-time of the current view.</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public DateTime ViewStart
  {
    get => this._viewStart;
    set
    {
      if (this._viewStart == value.Date)
        return;
      this._viewStart = value.Date;
      this.ClearItems();
      this.UpdateDaysAndWeeks();
      this.Renderer.PerformLayout();
      this.Invalidate();
      this.ReloadItems();
    }
  }

  /// <summary>
  /// Gets the weeks currently visible on the calendar, if <see cref="P:Intermech.Client.Core.Organizer.Scheduler.DaysMode" /> is <see cref="F:Intermech.Client.Core.Organizer.CalendarDaysMode.Short" />.
  /// </summary>
  public CalendarWeek[] Weeks => this._weeks;

  /// <summary>
  /// Creates a new <see cref="T:Intermech.Client.Core.Organizer.Scheduler" /> control.
  /// </summary>
  public Scheduler()
  {
    this.SetStyle(ControlStyles.ResizeRedraw, true);
    this.SetStyle(ControlStyles.Selectable, true);
    this.DoubleBuffered = true;
    this._items = new SchedulerItemCollection(this);
    this._renderer = (CalendarRenderer) new CalendarProfessionalRenderer(this);
    this.HighlightRanges = new CalendarHighlightRange[0];
    this._header = new SchedulerHeader(this.Font);
    this._header.Parent = (Control) this;
    this._header.ButtonClick += new SchedulerHeader.ClickEventHandler(this.On_header_ButtonClick);
    this._header.RadioButtonClick += new SchedulerHeader.ClickEventHandler(this.On_header_RadioButtonClick);
    this._scroll.Width = 20;
    this._scroll.LargeChange = this._scroll.SmallChange = 1;
    this.Controls.Add((Control) this._scroll);
    this._scroll.Scroll += new ScrollEventHandler(this.On_scroll_Scroll);
    this.SetViewRange(DateTime.Now, DateTime.Now.AddDays(0.0));
    this._repetitionIco = ResourceHelper.GetResourceData<Icon>(typeof (Scheduler).Assembly, "Intermech.Client.Core.Resources.RepetitionIco.ico");
  }

  /// <summary>Возникает при клике на заголовке дня в планировщике.</summary>
  [CustomDescription("Attribute.Client.Core_244")]
  public event SchedulerDayEventHandler DayHeaderClick;

  /// <summary>
  /// 
  /// </summary>
  [CustomDescription("Attribute.Client.Core_245")]
  public event Scheduler.CalendarHeaderButtonClickEventHandler HeaderButtonClick;

  /// <summary>
  /// 
  /// </summary>
  [CustomDescription("Attribute.Client.Core_246")]
  public event Scheduler.CalendarHeaderButtonClickEventHandler HeaderRadioButtonClick;

  /// <summary>
  /// Возникает после редактирования наименования элемента планировщика.
  /// </summary>
  [CustomDescription("Attribute.Client.Core_247")]
  public event SchedulerItemCancelEventHandler ItemCaptionEdited;

  /// <summary>
  /// Возникает во время редактирования наименования элемента планировщика.
  /// </summary>
  [CustomDescription("Attribute.Client.Core_248")]
  public event SchedulerItemCancelEventHandler ItemCaptionEditing;

  /// <summary>Возникает при клике элемента планировщика.</summary>
  [CustomDescription("Attribute.Client.Core_249")]
  public event SchedulerItemEventHandler ItemClick;

  /// <summary>Возникает после создания элемента планировщика.</summary>
  [CustomDescription("Attribute.Client.Core_250")]
  public event SchedulerItemCancelEventHandler ItemCreated;

  /// <summary>Возникает во время создания элемента планировщика.</summary>
  /// <remarks>Событие может быть прервано.</remarks>
  [CustomDescription("Attribute.Client.Core_251")]
  public event SchedulerItemCancelEventHandler ItemCreating;

  /// <summary>
  /// Возникает после изменения интервала времени элемента планировщика.
  /// </summary>
  [CustomDescription("Attribute.Client.Core_252")]
  public event SchedulerItemEventHandler ItemDatesChanged;

  /// <summary>Возникает при двойном клике элемента планировщика.</summary>
  [CustomDescription("Attribute.Client.Core_253")]
  public event SchedulerItemEventHandler ItemDoubleClick;

  /// <summary>
  /// Возникает после наведение курсора мыши на элемент планировщика.
  /// </summary>
  [CustomDescription("Attribute.Client.Core_254")]
  public event SchedulerItemEventHandler ItemMouseHover;

  /// <summary>Возникает после удаления элемента планировщика.</summary>
  [CustomDescription("Attribute.Client.Core_255")]
  public event SchedulerItemsEventHandler ItemsDeleted;

  /// <summary>Возникает перед удалением элемента планировщика.</summary>
  [CustomDescription("Attribute.Client.Core_256")]
  public event SchedulerItemsCancelEventHandler ItemsDeleting;

  /// <summary>Возникает после выделения элемента планировщика.</summary>
  [CustomDescription("Attribute.Client.Core_257")]
  public event SchedulerItemEventHandler ItemSelected;

  /// <summary>
  /// 
  /// </summary>
  [CustomDescription("Attribute.Client.Core_258")]
  public event SchedulerItemsEventHandler ItemsSelectionChanged;

  /// <summary>
  /// Возникает после позиционирования элемента планировщика.
  /// </summary>
  [CustomDescription("Attribute.Client.Core_259")]
  public event EventHandler ItemsPositioned;

  /// <summary>Возникает когда элемент создается в планировщике.</summary>
  [CustomDescription("Attribute.Client.Core_260")]
  public event SchedulerDatesEventHandler LoadItems;

  /// <summary>
  /// Возникает при двойном клике на свободном поле планировшика.
  /// </summary>
  [CustomDescription("Attribute.Client.Core_261")]
  public event EventHandler SchedulerDoubleClick;

  /// <summary>Возникает при скроллинге месяца.</summary>
  [CustomDescription("Attribute.Client.Core_262")]
  public event SchedulerDatesEventHandler ScrollMonth;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="index"></param>
  private void On_header_ButtonClick(object sender, int index)
  {
    if (this.HeaderButtonClick == null)
      return;
    this.HeaderButtonClick((object) this, index);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="index"></param>
  private void On_header_RadioButtonClick(object sender, int index)
  {
    if (this.HeaderRadioButtonClick == null)
      return;
    this.HeaderRadioButtonClick((object) this, index);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_scroll_Scroll(object sender, ScrollEventArgs e)
  {
    if (e.NewValue == e.OldValue)
      return;
    if (this.DaysMode == CalendarDaysMode.Expanded)
    {
      this.ScrollTimeUnits((e.OldValue - e.NewValue) * 120);
    }
    else
    {
      if (this.DaysMode != CalendarDaysMode.Short)
        return;
      this.ScrollCalendar((e.OldValue - e.NewValue) * 120);
    }
  }

  /// <summary>
  /// Handles the Keydown event of the TextBox that edit items.
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_textBox_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode == Keys.Escape)
    {
      this.FinalizeEditMode(true);
    }
    else
    {
      if (e.KeyCode != Keys.Return)
        return;
      this.FinalizeEditMode(false);
    }
  }

  /// <summary>
  /// Handles the LostFocus event of the TextBox that edit items.
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_textBox_LostFocus(object sender, EventArgs e) => this.FinalizeEditMode(false);

  /// <summary>Изменение выделенного элемента.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnCalendarItem_SelectionChanged(object sender, EventArgs e)
  {
    this._selectionChanged = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="disposing"></param>
  protected override void Dispose(bool disposing)
  {
    base.Dispose(disposing);
    if (this._repetitionIco == null)
      return;
    this._repetitionIco.Dispose();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="keyData"></param>
  /// <returns></returns>
  protected override bool IsInputKey(Keys keyData)
  {
    return keyData == Keys.Down || keyData == Keys.Up || keyData == Keys.Right || keyData == Keys.Left || base.IsInputKey(keyData);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnClick(EventArgs e)
  {
    base.OnClick(e);
    this.Select();
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void OnCreateControl()
  {
    base.OnCreateControl();
    this.Renderer.OnInitialize(new CalendarRendererEventArgs(new CalendarRendererEventArgs(this, (Graphics) null, Rectangle.Empty)));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnKeyDown(KeyEventArgs e)
  {
    base.OnKeyDown(e);
    if (this.SelectedElementEnd == null)
      return;
    int modifierKeys = (int) Control.ModifierKeys;
    int timeScale = (int) this.TimeScale;
    ICalendarSelectableElement selectionStart = (ICalendarSelectableElement) null;
    ICalendarSelectableElement selectableElement = (ICalendarSelectableElement) null;
    if (e.KeyCode == Keys.F2)
      this.ActivateEditMode();
    else if (e.KeyCode == Keys.Delete)
      this.DeleteSelectedItems();
    else if (e.KeyCode == Keys.Down)
    {
      if (e.Shift)
        selectionStart = this.SelectedElementStart;
      selectableElement = (ICalendarSelectableElement) this.GetTimeUnit(this.SelectedElementEnd.Date.Add(new TimeSpan(0, (int) this.TimeScale, 0)));
    }
    else if (e.KeyCode == Keys.Up)
    {
      if (e.Shift)
        selectionStart = this.SelectedElementStart;
      selectableElement = (ICalendarSelectableElement) this.GetTimeUnit(this.SelectedElementEnd.Date.Add(new TimeSpan(0, -(int) this.TimeScale, 0)));
    }
    else if (e.KeyCode == Keys.Right)
      selectableElement = (ICalendarSelectableElement) this.GetTimeUnit(this.SelectedElementEnd.Date.Add(new TimeSpan(24, 0, 0)));
    else if (e.KeyCode == Keys.Left)
      selectableElement = (ICalendarSelectableElement) this.GetTimeUnit(this.SelectedElementEnd.Date.Add(new TimeSpan(-24, 0, 0)));
    else if (e.KeyCode == Keys.Escape)
      this.ClearSelectedItems();
    if (selectionStart != null)
    {
      this.SetSelectionRange(selectionStart, selectableElement);
    }
    else
    {
      if (selectableElement == null)
        return;
      this.SetSelectionRange(selectableElement, selectableElement);
      if (!(selectableElement is SchedulerTimeScaleUnit))
        return;
      this.EnsureVisible(selectableElement as SchedulerTimeScaleUnit);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseDoubleClick(MouseEventArgs e)
  {
    base.OnMouseDoubleClick(e);
    if (this._header.Bounds.Contains(e.Location))
      return;
    CalendarItem calendarItem = this.ItemAt(e.Location);
    if (calendarItem != null)
    {
      if (!calendarItem.BaseItem)
      {
        string caption = LocalizationHolder.rm.GetString("Organizer_Name");
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Organizer_RepetitionItem_Msg"), caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      this.OnItemDoubleClick(new SchedulerItemEventArgs(calendarItem));
    }
    else
      this.OnSchedulerDoubleClick(new EventArgs());
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseDown(MouseEventArgs e)
  {
    base.OnMouseDown(e);
    if (this._header.Bounds.Contains(e.Location))
    {
      this._header.OnMouseClick(e);
      this.Invalidate();
    }
    else
    {
      ICalendarSelectableElement selectableElement = this.HitTest(e.Location);
      CalendarItem calendarItem = selectableElement as CalendarItem;
      bool flag = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
      if (!this.Focused)
        this.Focus();
      switch (this.State)
      {
        case CalendarState.Idle:
          if (calendarItem != null)
          {
            if (!flag)
              this.ClearSelectedItems();
            calendarItem.Selected = true;
            this.Invalidate(calendarItem);
            this.OnItemSelected(new SchedulerItemEventArgs(calendarItem));
            this.itemOnState = calendarItem;
            this.itemOnStateChanged = false;
            if (this.AllowItemEdit && !calendarItem.ReadOnly)
            {
              if (this.itemOnState.ResizeStartDateZone(e.Location) && this.AllowItemResize)
              {
                this.SetState(CalendarState.ResizingItem);
                this.itemOnState.IsResizingStartDate = true;
              }
              else if (this.itemOnState.ResizeEndDateZone(e.Location) && this.AllowItemResize)
              {
                this.SetState(CalendarState.ResizingItem);
                this.itemOnState.IsResizingEndDate = true;
              }
              else
                this.SetState(CalendarState.DraggingItem);
            }
            this.SetSelectionRange((ICalendarSelectableElement) null, (ICalendarSelectableElement) null);
            break;
          }
          this.ClearSelectedItems();
          if (flag)
          {
            if (selectableElement != null && this.SelectedElementEnd == null && !this.SelectedElementEnd.Equals((object) selectableElement))
              this.SelectedElementEnd = selectableElement;
          }
          else if (this.SelectedElementStart == null || selectableElement != null && !this.SelectedElementStart.Equals((object) selectableElement))
            this.SetSelectionRange(selectableElement, selectableElement);
          this.SetState(CalendarState.DraggingTimeSelection);
          break;
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseMove(MouseEventArgs e)
  {
    base.OnMouseMove(e);
    if (this._header.Bounds.Contains(e.Location))
    {
      this._header.Focused = true;
      this._header.OnMouseMove(e);
      this.Invalidate(this._header.Bounds);
    }
    else
    {
      if (this._header.Focused)
      {
        this._header.Focused = false;
        this.Cursor = Cursors.Default;
        this._header.OnMouseMove(e);
        this.Invalidate(this._header.Bounds);
      }
      CalendarDay calendarDay = (CalendarDay) null;
      foreach (CalendarDay day in this.Days)
      {
        day.SetOverflowEndSelected(false);
        if (day.Bounds.Contains(e.Location))
          calendarDay = day;
      }
      if (calendarDay != null && this.DaysMode == CalendarDaysMode.Short)
      {
        this.Invalidate(this._currOverflowBounds);
        using (GraphicsPath graphicsPath1 = new GraphicsPath())
        {
          int y = calendarDay.OverflowEndBounds.Top + calendarDay.OverflowEndBounds.Height / 2;
          GraphicsPath graphicsPath2 = graphicsPath1;
          Point[] points = new Point[3];
          points[0] = new Point(calendarDay.OverflowEndBounds.Left, y);
          points[1] = new Point(calendarDay.OverflowEndBounds.Right, y);
          int left = calendarDay.OverflowEndBounds.Left;
          Rectangle overflowEndBounds = calendarDay.OverflowEndBounds;
          int num = overflowEndBounds.Width / 2;
          int x = left + num;
          overflowEndBounds = calendarDay.OverflowEndBounds;
          int bottom = overflowEndBounds.Bottom;
          points[2] = new Point(x, bottom);
          graphicsPath2.AddPolygon(points);
          this._currOverflowBounds = Rectangle.Truncate(graphicsPath1.GetBounds());
        }
        calendarDay.SetOverflowEndSelected(this._currOverflowBounds.Contains(e.Location));
        this.Invalidate(this._currOverflowBounds);
      }
      ICalendarSelectableElement selectableElement = this.HitTest(e.Location, this.State != 0);
      CalendarItem calendarItem = selectableElement as CalendarItem;
      CalendarDayTop calendarDayTop = selectableElement as CalendarDayTop;
      int modifierKeys = (int) Control.ModifierKeys;
      if (selectableElement == null)
        return;
      switch (this.State)
      {
        case CalendarState.Idle:
          Cursor cursor = Cursors.Default;
          if (calendarItem != null)
          {
            if ((calendarItem.ResizeEndDateZone(e.Location) || calendarItem.ResizeStartDateZone(e.Location)) && this.AllowItemResize && !calendarItem.ReadOnly)
              cursor = calendarItem.IsOnDayTop || this.DaysMode == CalendarDaysMode.Short ? Cursors.SizeWE : Cursors.SizeNS;
            this.OnItemMouseHover(new SchedulerItemEventArgs(calendarItem));
          }
          if (this.Cursor.Equals((object) cursor))
            break;
          this.Cursor = cursor;
          break;
        case CalendarState.DraggingTimeSelection:
          if (this.SelectedElementStart == null || this.SelectedElementEnd.Equals((object) selectableElement))
            break;
          this.SelectedElementEnd = selectableElement;
          break;
        case CalendarState.DraggingItem:
          TimeSpan duration = this.itemOnState.Duration;
          this.itemOnState.IsDragging = true;
          this.itemOnState.StartDate = selectableElement.Date;
          this.itemOnState.EndDate = this.itemOnState.StartDate.Add(duration);
          this.Renderer.PerformItemsLayout();
          this.Invalidate();
          this.itemOnStateChanged = true;
          break;
        case CalendarState.ResizingItem:
          if (this.itemOnState.IsResizingEndDate && selectableElement.Date.CompareTo(this.itemOnState.StartDate) >= 0)
            this.itemOnState.EndDate = selectableElement.Date.Add(calendarDayTop != null || this.DaysMode == CalendarDaysMode.Short ? new TimeSpan(23, 59, 59) : this.Days[0].TimeUnits[0].Duration);
          else if (this.itemOnState.IsResizingStartDate && selectableElement.Date.CompareTo(this.itemOnState.EndDate) <= 0)
            this.itemOnState.StartDate = selectableElement.Date;
          this.Renderer.PerformItemsLayout();
          this.Invalidate();
          this.itemOnStateChanged = true;
          break;
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseUp(MouseEventArgs e)
  {
    bool flag1 = true;
    bool flag2 = false;
    if (this.DaysMode == CalendarDaysMode.Short)
    {
      foreach (CalendarDay day in this.Days)
      {
        if (day.Bounds.Contains(e.Location))
        {
          using (GraphicsPath graphicsPath1 = new GraphicsPath())
          {
            Rectangle overflowEndBounds = day.OverflowEndBounds;
            int top = overflowEndBounds.Top;
            overflowEndBounds = day.OverflowEndBounds;
            int num1 = overflowEndBounds.Height / 2;
            int y = top + num1;
            GraphicsPath graphicsPath2 = graphicsPath1;
            Point[] points = new Point[3];
            overflowEndBounds = day.OverflowEndBounds;
            points[0] = new Point(overflowEndBounds.Left, y);
            overflowEndBounds = day.OverflowEndBounds;
            points[1] = new Point(overflowEndBounds.Right, y);
            overflowEndBounds = day.OverflowEndBounds;
            int left = overflowEndBounds.Left;
            overflowEndBounds = day.OverflowEndBounds;
            int num2 = overflowEndBounds.Width / 2;
            int x = left + num2;
            overflowEndBounds = day.OverflowEndBounds;
            int bottom = overflowEndBounds.Bottom;
            points[2] = new Point(x, bottom);
            graphicsPath2.AddPolygon(points);
            if (Rectangle.Truncate(graphicsPath1.GetBounds()).Contains(e.Location))
            {
              flag2 = true;
              this.OnDayHeaderClick(new SchedulerDayEventArgs(day));
            }
            else
              break;
          }
        }
      }
    }
    if (!flag2)
    {
      ICalendarSelectableElement selectableElement = this.HitTest(e.Location, this.State == CalendarState.DraggingTimeSelection);
      CalendarDay day = selectableElement as CalendarDay;
      int modifierKeys = (int) Control.ModifierKeys;
      switch (this.State)
      {
        case CalendarState.Idle:
          List<CalendarItem> selectedItems = this.SelectedItems;
          break;
        case CalendarState.DraggingTimeSelection:
          if (this.SelectedElementStart == null || selectableElement != null && !this.SelectedElementEnd.Equals((object) selectableElement))
            this.SelectedElementEnd = selectableElement;
          if (day != null && day.HeaderBounds.Contains(e.Location))
          {
            this.OnDayHeaderClick(new SchedulerDayEventArgs(day));
            break;
          }
          break;
        case CalendarState.DraggingItem:
          if (this.itemOnStateChanged)
          {
            this.OnItemDatesChanged(new SchedulerItemEventArgs(this.itemOnState));
            break;
          }
          break;
        case CalendarState.ResizingItem:
          if (this.itemOnStateChanged)
          {
            this.OnItemDatesChanged(new SchedulerItemEventArgs(this.itemOnState));
            break;
          }
          break;
      }
      if (this.itemOnState != null)
      {
        this.itemOnState.IsDragging = false;
        this.itemOnState.IsResizingEndDate = false;
        this.itemOnState.IsResizingStartDate = false;
        this.Invalidate(this.itemOnState);
        this.OnItemClick(new SchedulerItemEventArgs(this.itemOnState));
        this.itemOnState = (CalendarItem) null;
      }
    }
    this.SetState(CalendarState.Idle);
    if (!flag1)
      return;
    this.OnItemsSelectionChanged(new SchedulerItemsEventArgs(this.SelectedItems));
    base.OnMouseUp(e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseWheel(MouseEventArgs e)
  {
    base.OnMouseWheel(e);
    if (this.DaysMode == CalendarDaysMode.Expanded)
    {
      this.ScrollTimeUnits(e.Delta);
    }
    else
    {
      if (this.DaysMode != CalendarDaysMode.Short)
        return;
      this.ScrollCalendar(e.Delta);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    if (e.ClipRectangle == this._header.Bounds)
    {
      this._header.OnDraw(e);
    }
    else
    {
      CalendarRendererEventArgs e1 = new CalendarRendererEventArgs(this, e.Graphics, e.ClipRectangle);
      this.Renderer.OnDrawBackground(e1);
      this._header.OnDraw(e);
      this.Renderer.OnDrawCaption(e1);
      switch (this.DaysMode)
      {
        case CalendarDaysMode.Short:
          this.Renderer.OnDrawDayNameHeaders(e1);
          this.Renderer.OnDrawWeekHeaders(e1);
          break;
        case CalendarDaysMode.Expanded:
          this.Renderer.OnDrawTimeScale(e1);
          break;
        default:
          throw new NotImplementedException("Current DaysMode not implemented");
      }
      this.Renderer.OnDrawDays(e1);
      this.Renderer.OnDrawItems(e1);
      this.Renderer.OnDrawOverflows(e1);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnResize(EventArgs e)
  {
    base.OnResize(e);
    this.TimeUnitsOffset = this.TimeUnitsOffset;
    this.Renderer.PerformLayout();
    this._header.Width = this.Width - 2;
  }

  /// <summary>Removes all the items currently on the calendar.</summary>
  private void ClearItems()
  {
    this.Items.Clear();
    this.Renderer.DayTopHeight = this.Renderer.DayTopMinHeight;
  }

  /// <summary>
  /// Clears selection of currently selected components (As quick as possible).
  /// </summary>
  private void ClearSelectedComponents()
  {
    foreach (SchedulerSelectableElement selectedElement in this._selectedElements)
      selectedElement.Selected = false;
    this._selectedElements.Clear();
    this.Invalidate(this._selectedElementSquare);
    this._selectedElementSquare = Rectangle.Empty;
  }

  /// <summary>Unselects the selected items.</summary>
  private void ClearSelectedItems()
  {
    Rectangle rectangle = Rectangle.Empty;
    foreach (CalendarItem calendarItem in (List<CalendarItem>) this.Items)
    {
      if (calendarItem.Selected)
        rectangle = rectangle.IsEmpty ? calendarItem.Bounds : Rectangle.Union(rectangle, calendarItem.Bounds);
      calendarItem.Selected = false;
    }
    this.Invalidate(rectangle);
  }

  /// <summary>Формирование надписи выбранного интервала.</summary>
  private void CreateCaption()
  {
    if (this._days == null || this._days.Length == 0)
      return;
    CultureInfo currentUiCulture = CultureInfo.CurrentUICulture;
    if (this._header.Buttons[2].Active)
      this._caption = this._days[this._days.Length / 2].Date.ToString("MMMM yyyy", (IFormatProvider) currentUiCulture);
    else if (this._days.Length == 1)
    {
      this._caption = this._days[0].Date.ToString(currentUiCulture.IetfLanguageTag == "ru-RU" ? "d MMMM yyyy 'г.'" : "MMMM dd, yyyy", (IFormatProvider) currentUiCulture);
    }
    else
    {
      CalendarDay day1 = this._days[0];
      CalendarDay day2 = this._days[this._days.Length - 1];
      DateTime date = day1.Date;
      int month1 = date.Month;
      date = day2.Date;
      int month2 = date.Month;
      if (month1 == month2)
      {
        if (currentUiCulture.IetfLanguageTag == "ru-RU")
        {
          date = day1.Date;
          // ISSUE: variable of a boxed type
          __Boxed<int> day3 = (ValueType) date.Day;
          date = day2.Date;
          string longDateString = date.ToLongDateString();
          this._caption = $"{day3} - {longDateString}";
          this._caption = this._caption.Substring(0, this._caption.Length - 3);
        }
        else
        {
          date = day1.Date;
          string str1 = date.ToString("MMMM dd", (IFormatProvider) currentUiCulture);
          date = day2.Date;
          string str2 = date.ToString("dd, yyyy", (IFormatProvider) currentUiCulture);
          this._caption = $"{str1} - {str2}";
        }
      }
      else
      {
        date = day1.Date;
        int year1 = date.Year;
        date = day2.Date;
        int year2 = date.Year;
        if (year1 != year2)
        {
          if (currentUiCulture.IetfLanguageTag == "ru-RU")
          {
            date = day1.Date;
            string str3 = date.ToString("dd MMMM yyyy", (IFormatProvider) currentUiCulture);
            date = day2.Date;
            string str4 = date.ToString("dd MMMM yyyy", (IFormatProvider) currentUiCulture);
            this._caption = $"{str3} - {str4}";
          }
          else
          {
            date = day1.Date;
            string str5 = date.ToString("MMMM dd, yyyy", (IFormatProvider) currentUiCulture);
            date = day2.Date;
            string str6 = date.ToString("MMMM dd, yyyy", (IFormatProvider) currentUiCulture);
            this._caption = $"{str5} - {str6}";
          }
        }
        else if (currentUiCulture.IetfLanguageTag == "ru-RU")
        {
          date = day1.Date;
          string str7 = date.ToString("dd MMMM", (IFormatProvider) currentUiCulture);
          date = day2.Date;
          string str8 = date.ToString("dd MMMM yyyy", (IFormatProvider) currentUiCulture);
          this._caption = $"{str7} - {str8}";
        }
        else
        {
          date = day1.Date;
          string str9 = date.ToString("MMMM dd", (IFormatProvider) currentUiCulture);
          date = day2.Date;
          string str10 = date.ToString("MMMM dd, yyyy", (IFormatProvider) currentUiCulture);
          this._caption = $"{str9} - {str10}";
        }
      }
    }
  }

  /// <summary>
  /// Grows the rectangle to repaint currently selected elements.
  /// </summary>
  /// <param name="rect"></param>
  private void GrowSquare(Rectangle rect)
  {
    this._selectedElementSquare = this._selectedElementSquare.IsEmpty ? rect : Rectangle.Union(this._selectedElementSquare, rect);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="oldValue"></param>
  private void OnScrollMonth(DateTime oldValue)
  {
    if (this.ScrollMonth == null)
      return;
    this.ScrollMonth((object) this, new SchedulerDatesEventArgs(this, oldValue, this.ViewStart));
  }

  /// <summary>
  /// Raises the <see cref="E:Intermech.Client.Core.Organizer.Scheduler.ItemsPositioned" /> event.
  /// </summary>
  internal void RaiseItemsPositioned() => this.OnItemsPositioned(EventArgs.Empty);

  /// <summary>Перегрузка элементов.</summary>
  private void ReloadItems()
  {
    this.OnLoadItems(new SchedulerDatesEventArgs(this, this.ViewStart, this.ViewEnd));
  }

  /// <summary>Scrolls the calendar using the specified delta.</summary>
  /// <param name="delta"></param>
  private void ScrollCalendar(int delta)
  {
    DateTime viewStart = this.ViewStart;
    int num = delta < 0 ? 7 : -7;
    DateTime dateTime = this.ViewStart;
    DateTime dateStart = dateTime.AddDays((double) num);
    dateTime = this.ViewEnd;
    DateTime dateEnd = dateTime.AddDays((double) num);
    this.SetViewRange(dateStart, dateEnd);
    this.OnScrollMonth(viewStart);
  }

  /// <summary>Scrolls the time units using the specified delta.</summary>
  /// <param name="delta"></param>
  private void ScrollTimeUnits(int delta)
  {
    int timeUnitsOffset = this.TimeUnitsOffset;
    int visibleTimeUnits = this.Renderer.GetVisibleTimeUnits();
    int num1 = delta >= 0 ? timeUnitsOffset + 1 : timeUnitsOffset - 1;
    if (num1 > 0)
      num1 = 0;
    else if (this.Days != null && this.Days.Length != 0 && this.Days[0].TimeUnits != null && num1 * -1 >= this.Days[0].TimeUnits.Length)
      num1 = -1 * (this.Days[0].TimeUnits.Length - 1);
    else if (this.Days != null && this.Days.Length != 0 && this.Days[0].TimeUnits != null)
    {
      int num2 = -1 * (this.Days[0].TimeUnits.Length - visibleTimeUnits);
      if (num1 < num2)
        num1 = num2;
    }
    if (num1 == this.TimeUnitsOffset)
      return;
    this.TimeUnitsOffset = num1;
    this._scroll.Value = Math.Abs(num1);
  }

  /// <summary>
  /// Sets the value of the <see cref="P:Intermech.Client.Core.Organizer.Scheduler.DaysMode" /> property.
  /// </summary>
  /// <param name="mode">Mode in which days will be rendered</param>
  private void SetDaysMode(CalendarDaysMode mode) => this._daysMode = mode;

  /// <summary>
  /// Sets the value of the <see cref="P:Intermech.Client.Core.Organizer.Scheduler.State" /> property.
  /// </summary>
  /// <param name="state">Current state of the calendar</param>
  private void SetState(CalendarState state) => this._state = state;

  /// <summary>Updates the.</summary>
  private void UpdateDaysAndWeeks()
  {
    DateTime dateTime1 = this.ViewEnd;
    int year = dateTime1.Year;
    dateTime1 = this.ViewEnd;
    int month = dateTime1.Month;
    dateTime1 = this.ViewEnd;
    int day = dateTime1.Day;
    dateTime1 = new DateTime(year, month, day, 23, 59, 59);
    TimeSpan timeSpan = dateTime1.Subtract(this.ViewStart.Date);
    timeSpan = timeSpan.Add(new TimeSpan(0, 0, 0, 1, 0));
    int days = 0;
    if (timeSpan.Days < 1 || timeSpan.Days > this.MaxViewDays)
      throw new Exception("Days between ViewStart and ViewEnd should be between 1 and MaximumViewDays");
    if (timeSpan.Days > this.MaxFullDays)
    {
      this.SetDaysMode(CalendarDaysMode.Short);
      days = (int) (new int[7]{ 0, 1, 2, 3, 4, 5, 6 }[(int) this.ViewStart.DayOfWeek] - this.FirstDayOfWeek);
      timeSpan = timeSpan.Add(new TimeSpan(days, 0, 0, 0));
      while (timeSpan.Days % 7 != 0)
        timeSpan = timeSpan.Add(new TimeSpan(1, 0, 0, 0));
    }
    else
      this.SetDaysMode(CalendarDaysMode.Expanded);
    this.Renderer.DayTopHeight = 0;
    List<CalendarDay> calendarDayList = new List<CalendarDay>(timeSpan.Days);
    DateTime dateTime2;
    for (int index = 0; index < timeSpan.Days; ++index)
    {
      dateTime2 = this.ViewStart;
      DateTime date = dateTime2.AddDays((double) (-days + index));
      if (!this._excludedDays.ContainsKey(date.Month) || !this._excludedDays[date.Month].Contains(date.Day))
        calendarDayList.Add(new CalendarDay(this, date, index));
    }
    this._days = new CalendarDay[calendarDayList.Count];
    this._days = calendarDayList.ToArray();
    if (this.DaysMode == CalendarDaysMode.Short)
    {
      List<CalendarWeek> calendarWeekList = new List<CalendarWeek>();
      for (int index = 0; index < this.Days.Length; ++index)
      {
        dateTime2 = this.Days[index].Date;
        if (dateTime2.DayOfWeek == this.FirstDayOfWeek)
          calendarWeekList.Add(new CalendarWeek(this, this.Days[index].Date));
      }
      this._weeks = calendarWeekList.ToArray();
    }
    else
      this._weeks = new CalendarWeek[0];
    this.UpdateHighlights();
  }

  /// <summary>
  /// Updates the value of the <see cref="P:Intermech.Client.Core.Organizer.SchedulerTimeScaleUnit.Highlighted" /> property on the time units of days.
  /// </summary>
  internal void UpdateHighlights()
  {
    if (this.Days == null)
      return;
    for (int index = 0; index < this.Days.Length; ++index)
      this.Days[index].UpdateHighlights();
  }

  /// <summary>
  /// Informs elements who's selected and who's not, and repaints <see cref="F:Intermech.Client.Core.Organizer.Scheduler._selectedElementSquare" />.
  /// </summary>
  private void UpdateSelectionElements()
  {
    this.ClearSelectedComponents();
    if (this._selectedElementEnd == null || this._selectedElementStart == null)
      return;
    SchedulerTimeScaleUnit schedulerTimeScaleUnit1 = this._selectedElementStart as SchedulerTimeScaleUnit;
    CalendarDayTop calendarDayTop1 = this._selectedElementStart as CalendarDayTop;
    CalendarDay calendarDay1 = this._selectedElementStart as CalendarDay;
    SchedulerTimeScaleUnit schedulerTimeScaleUnit2 = this._selectedElementEnd as SchedulerTimeScaleUnit;
    CalendarDayTop calendarDayTop2 = this._selectedElementEnd as CalendarDayTop;
    CalendarDay calendarDay2 = this._selectedElementEnd as CalendarDay;
    if (this._selectedElementEnd.CompareTo(this.SelectedElementStart) < 0)
    {
      schedulerTimeScaleUnit1 = this._selectedElementEnd as SchedulerTimeScaleUnit;
      calendarDayTop1 = this._selectedElementEnd as CalendarDayTop;
      calendarDay1 = this._selectedElementEnd as CalendarDay;
      schedulerTimeScaleUnit2 = this.SelectedElementStart as SchedulerTimeScaleUnit;
      calendarDayTop2 = this.SelectedElementStart as CalendarDayTop;
      calendarDay2 = this._selectedElementStart as CalendarDay;
    }
    if (schedulerTimeScaleUnit1 != null && schedulerTimeScaleUnit2 != null)
    {
      bool flag = false;
      int index1 = schedulerTimeScaleUnit1.Day.Index;
      while (!flag)
      {
        for (int index2 = index1 == schedulerTimeScaleUnit1.Day.Index ? schedulerTimeScaleUnit1.Index : 0; index1 < this.Days.Length && index2 < this.Days[index1].TimeUnits.Length; ++index2)
        {
          SchedulerTimeScaleUnit timeUnit = this.Days[index1].TimeUnits[index2];
          timeUnit.Selected = true;
          this.GrowSquare(timeUnit.Bounds);
          this._selectedElements.Add((SchedulerSelectableElement) timeUnit);
          if (timeUnit.Equals((object) schedulerTimeScaleUnit2))
          {
            flag = true;
            break;
          }
        }
        ++index1;
      }
    }
    else if (calendarDayTop1 != null && calendarDayTop2 != null)
    {
      for (int index = calendarDayTop1.Day.Index; index <= calendarDayTop2.Day.Index; ++index)
      {
        CalendarDayTop dayTop = this.Days[index].DayTop;
        dayTop.Selected = true;
        this.GrowSquare(dayTop.Bounds);
        this._selectedElements.Add((SchedulerSelectableElement) dayTop);
      }
    }
    else if (calendarDay1 != null && calendarDay2 != null)
    {
      for (int index = calendarDay1.Index; index <= calendarDay2.Index; ++index)
      {
        CalendarDay day = this.Days[index];
        day.Selected = true;
        this.GrowSquare(day.Bounds);
        this._selectedElements.Add((SchedulerSelectableElement) day);
      }
    }
    this.Invalidate(this._selectedElementSquare);
  }

  /// <summary>Нажатие заголовка дня планировщика.</summary>
  /// <param name="e"></param>
  protected virtual void OnDayHeaderClick(SchedulerDayEventArgs e)
  {
    if (this.DayHeaderClick == null)
      return;
    this._header.SetActiveButton(0);
    this.DayHeaderClick((object) this, e);
  }

  /// <summary>Нажатие элемента планировщика.</summary>
  /// <param name="e"></param>
  protected virtual void OnItemClick(SchedulerItemEventArgs e)
  {
    if (this.ItemClick == null)
      return;
    this.ItemClick((object) this, e);
  }

  /// <summary>Завершение создания элемента планировщика.</summary>
  /// <param name="e"></param>
  protected virtual void OnItemCreated(SchedulerItemCancelEventArgs e)
  {
    if (this.ItemCreated == null)
      return;
    this.ItemCreated((object) this, e);
  }

  /// <summary>Создание элемента планировщика.</summary>
  /// <param name="e"></param>
  protected virtual void OnItemCreating(SchedulerItemCancelEventArgs e)
  {
    if (this.ItemCreating == null)
      return;
    this.ItemCreating((object) this, e);
  }

  /// <summary>Изменение интервала времени элемента планировщика.</summary>
  /// <param name="e"></param>
  protected virtual void OnItemDatesChanged(SchedulerItemEventArgs e)
  {
    if (this.ItemDatesChanged == null)
      return;
    this.ItemDatesChanged((object) this, e);
  }

  /// <summary>Двойное нажание элемента планировщика.</summary>
  /// <param name="e"></param>
  protected virtual void OnItemDoubleClick(SchedulerItemEventArgs e)
  {
    if (this.ItemDoubleClick == null)
      return;
    this.ItemDoubleClick((object) this, e);
  }

  /// <summary>
  /// Завершение редактирования текста элемента планировщика.
  /// </summary>
  /// <param name="e"></param>
  protected virtual void OnItemEdited(SchedulerItemCancelEventArgs e)
  {
    if (this.ItemCaptionEdited == null)
      return;
    this.ItemCaptionEdited((object) this, e);
  }

  /// <summary>Редактирование элемента планировщика.</summary>
  /// <param name="e"></param>
  protected virtual void OnItemEditing(SchedulerItemCancelEventArgs e)
  {
    if (this.ItemCaptionEditing == null)
      return;
    this.ItemCaptionEditing((object) this, e);
  }

  /// <summary>Наведение курсора мыши на элемент планировщика.</summary>
  /// <param name="e"></param>
  protected virtual void OnItemMouseHover(SchedulerItemEventArgs e)
  {
    if (this.ItemMouseHover == null)
      return;
    this.ItemMouseHover((object) this, e);
  }

  /// <summary>Завершение удаления элемента планировщика.</summary>
  /// <param name="e"></param>
  protected virtual void OnItemsDeleted(SchedulerItemsEventArgs e)
  {
    if (this.ItemsDeleted == null)
      return;
    this.ItemsDeleted((object) this, e);
  }

  /// <summary>Удаление элемента планировщика.</summary>
  /// <param name="e"></param>
  protected virtual void OnItemsDeleting(SchedulerItemsCancelEventArgs e)
  {
    if (this.ItemsDeleting == null)
      return;
    this.ItemsDeleting((object) this, e);
  }

  /// <summary>Выделение элемента планировщика.</summary>
  /// <param name="e"></param>
  protected virtual void OnItemSelected(SchedulerItemEventArgs e)
  {
    if (this.ItemSelected == null)
      return;
    this.ItemSelected((object) this, e);
  }

  /// <summary>
  /// Изменение коллекции выделенных элементов планировщика.
  /// </summary>
  /// <param name="e"></param>
  protected virtual void OnItemsSelectionChanged(SchedulerItemsEventArgs e)
  {
    if (!this._selectionChanged)
      return;
    this._selectionChanged = false;
    if (this.ItemsSelectionChanged == null)
      return;
    this.ItemsSelectionChanged((object) this, e);
  }

  /// <summary>Позиционирование элемента планировщика.</summary>
  /// <param name="e"></param>
  protected virtual void OnItemsPositioned(EventArgs e)
  {
    if (this.ItemsPositioned == null)
      return;
    this.ItemsPositioned((object) this, e);
  }

  /// <summary>Загрузка элементов планировщика.</summary>
  /// <param name="e"></param>
  protected virtual void OnLoadItems(SchedulerDatesEventArgs e)
  {
    if (this.LoadItems == null)
      return;
    this.LoadItems((object) this, e);
  }

  /// <summary>Двойной клик на свободном поле планировщика.</summary>
  /// <param name="e"></param>
  protected virtual void OnSchedulerDoubleClick(EventArgs e)
  {
    if (this.SchedulerDoubleClick == null)
      return;
    this.SchedulerDoubleClick((object) this, e);
  }

  /// <summary>Activates the edit mode on the first selected item.</summary>
  public void ActivateEditMode()
  {
    foreach (CalendarItem calendarItem in (List<CalendarItem>) this.Items)
    {
      if (calendarItem.Selected)
      {
        this.ActivateEditMode(calendarItem);
        break;
      }
    }
  }

  /// <summary>Activates the edit mode on the specified item.</summary>
  /// <param name="item"></param>
  public void ActivateEditMode(CalendarItem item)
  {
    SchedulerItemCancelEventArgs e = new SchedulerItemCancelEventArgs(item);
    if (!this._creatingItem)
      this.OnItemEditing(e);
    if (e.Cancel)
      return;
    this._editModeItem = item;
    this.TextBox = new TextBox();
    this.TextBox.KeyDown += new KeyEventHandler(this.On_textBox_KeyDown);
    this.TextBox.LostFocus += new EventHandler(this.On_textBox_LostFocus);
    Rectangle bounds = item.Bounds;
    bounds.Inflate(-2, -2);
    this.TextBox.Bounds = bounds;
    this.TextBox.BorderStyle = BorderStyle.None;
    this.TextBox.Text = item.Caption;
    this.TextBox.Multiline = true;
    this.Controls.Add((Control) this.TextBox);
    this.TextBox.Visible = true;
    this.TextBox.Focus();
    this.TextBox.SelectionStart = this.TextBox.Text.Length;
    this.SetState(CalendarState.EditingItemText);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objID"></param>
  /// <param name="startDate"></param>
  /// <param name="finishDate"></param>
  /// <param name="caption"></param>
  /// <param name="img"></param>
  /// <param name="baseItem"></param>
  /// <returns></returns>
  public CalendarItem CreateItem(
    long objID,
    DateTime startDate,
    DateTime finishDate,
    string caption,
    Image img,
    bool baseItem)
  {
    if (objID == 0L)
      return (CalendarItem) null;
    if (finishDate.CompareTo(startDate) < 0)
    {
      DateTime dateTime = finishDate;
      finishDate = startDate;
      startDate = dateTime;
    }
    CalendarItem calendarItem = new CalendarItem(this, objID, startDate, finishDate, caption, baseItem);
    calendarItem.Image = img;
    calendarItem.SelectionChanged += new EventHandler(this.OnCalendarItem_SelectionChanged);
    SchedulerItemCancelEventArgs e = new SchedulerItemCancelEventArgs(calendarItem);
    this.OnItemCreating(e);
    if (e.Cancel)
      return (CalendarItem) null;
    this.Items.Add(calendarItem);
    return calendarItem;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objID"></param>
  /// <param name="startDate"></param>
  /// <param name="finishDate"></param>
  /// <param name="caption"></param>
  /// <param name="img"></param>
  /// <param name="repetition"></param>
  /// <returns></returns>
  public CalendarItem CreateItem(
    long objID,
    DateTime startDate,
    DateTime finishDate,
    string caption,
    Image img,
    Repetition repetition)
  {
    if (objID == 0L)
      return (CalendarItem) null;
    if (finishDate.CompareTo(startDate) < 0)
    {
      DateTime dateTime = finishDate;
      finishDate = startDate;
      startDate = dateTime;
    }
    bool baseItem = true;
    CalendarItem calendarItem1 = (CalendarItem) null;
    CalendarItem calendarItem2 = (CalendarItem) null;
    switch (repetition)
    {
      case Repetition.Daily:
        if (startDate < this._viewStart)
        {
          TimeSpan timeSpan = this._viewStart.Date.Subtract(startDate.Date);
          startDate = startDate.AddDays((double) timeSpan.Days);
          finishDate = finishDate.AddDays((double) timeSpan.Days);
          baseItem = false;
        }
        while (startDate < this._viewEnd)
        {
          CalendarItem calendarItem3 = this.CreateItem(objID, startDate, finishDate, caption, img, baseItem);
          if (baseItem)
            calendarItem2 = calendarItem3;
          startDate = startDate.AddDays(1.0);
          finishDate = finishDate.AddDays(1.0);
          baseItem = false;
        }
        break;
      case Repetition.Weekly:
        if (startDate < this._viewStart)
        {
          int num = this._viewStart.Date.Subtract(startDate.Date).Days / 7;
          startDate = startDate.AddDays((double) (num * 7));
          finishDate = finishDate.AddDays((double) (num * 7));
          baseItem = false;
        }
        while (startDate < this._viewEnd)
        {
          CalendarItem calendarItem4 = this.CreateItem(objID, startDate, finishDate, caption, img, baseItem);
          if (baseItem)
            calendarItem2 = calendarItem4;
          startDate = startDate.AddDays(7.0);
          finishDate = finishDate.AddDays(7.0);
          baseItem = false;
        }
        break;
      case Repetition.Monthly:
        int day1 = DateTime.DaysInMonth(this._viewStart.Year, this._viewStart.Month);
        int day2 = startDate.Day;
        int day3 = finishDate.Day;
        DateTime dateTime1 = new DateTime(this._viewStart.Year, this._viewStart.Month, 1, 0, 0, 0);
        bool flag = startDate.Month == finishDate.Month;
        if (finishDate < this._viewStart)
        {
          if (flag)
          {
            if (day3 <= day1)
            {
              startDate = new DateTime(dateTime1.Year, dateTime1.Month, day2, startDate.Hour, startDate.Minute, 0);
              finishDate = new DateTime(dateTime1.Year, dateTime1.Month, day3, finishDate.Hour, finishDate.Minute, 0);
            }
            else if (day2 <= day1)
            {
              startDate = new DateTime(dateTime1.Year, dateTime1.Month, day2, startDate.Hour, startDate.Minute, 0);
              finishDate = new DateTime(dateTime1.Year, dateTime1.Month, day1, finishDate.Hour, finishDate.Minute, 0);
            }
            else
            {
              DateTime dateTime2 = dateTime1.AddMonths(1);
              startDate = new DateTime(dateTime2.Year, dateTime2.Month, day2, startDate.Hour, startDate.Minute, 0);
              finishDate = new DateTime(dateTime2.Year, dateTime2.Month, day3, finishDate.Hour, finishDate.Minute, 0);
            }
          }
          else
          {
            if (this._viewStart.Day == 1)
              dateTime1 = dateTime1.AddMonths(-1);
            if (day2 <= day1)
            {
              startDate = new DateTime(dateTime1.Year, dateTime1.Month, day2, startDate.Hour, startDate.Minute, 0);
              DateTime dateTime3 = dateTime1.AddMonths(1);
              int num = DateTime.DaysInMonth(dateTime3.Year, dateTime3.Month);
              finishDate = new DateTime(dateTime3.Year, dateTime3.Month, day3 <= num ? day3 : num, finishDate.Hour, finishDate.Minute, 0);
            }
            else
            {
              DateTime dateTime4 = dateTime1.AddMonths(1);
              startDate = new DateTime(dateTime4.Year, dateTime4.Month, 1, startDate.Hour, startDate.Minute, 0);
              finishDate = new DateTime(dateTime4.Year, dateTime4.Month, day3, finishDate.Hour, finishDate.Minute, 0);
            }
          }
          baseItem = false;
        }
        while (startDate < this._viewEnd)
        {
          if (finishDate >= this._viewStart)
            calendarItem1 = this.CreateItem(objID, startDate, finishDate, caption, img, baseItem);
          if (baseItem)
            calendarItem2 = calendarItem1;
          dateTime1 = dateTime1.AddMonths(1);
          int day4 = DateTime.DaysInMonth(dateTime1.Year, dateTime1.Month);
          if (flag)
          {
            if (day3 <= day4)
            {
              startDate = new DateTime(dateTime1.Year, dateTime1.Month, day2, startDate.Hour, startDate.Minute, 0);
              finishDate = new DateTime(dateTime1.Year, dateTime1.Month, day3, finishDate.Hour, finishDate.Minute, 0);
            }
            else if (day2 <= day4)
            {
              startDate = new DateTime(dateTime1.Year, dateTime1.Month, day2, startDate.Hour, startDate.Minute, 0);
              finishDate = new DateTime(dateTime1.Year, dateTime1.Month, day4, finishDate.Hour, finishDate.Minute, 0);
            }
            else
            {
              DateTime dateTime5 = dateTime1.AddMonths(1);
              startDate = new DateTime(dateTime5.Year, dateTime5.Month, day2, startDate.Hour, startDate.Minute, 0);
              finishDate = new DateTime(dateTime5.Year, dateTime5.Month, day3, finishDate.Hour, finishDate.Minute, 0);
            }
          }
          else if (day2 <= day4)
          {
            startDate = new DateTime(dateTime1.Year, dateTime1.Month, day2, startDate.Hour, startDate.Minute, 0);
            DateTime dateTime6 = dateTime1.AddMonths(1);
            int num = DateTime.DaysInMonth(dateTime6.Year, dateTime6.Month);
            finishDate = new DateTime(dateTime6.Year, dateTime6.Month, day3 <= num ? day3 : num, finishDate.Hour, finishDate.Minute, 0);
          }
          else
          {
            DateTime dateTime7 = dateTime1.AddMonths(1);
            startDate = new DateTime(dateTime7.Year, dateTime7.Month, 1, startDate.Hour, startDate.Minute, 0);
            finishDate = new DateTime(dateTime7.Year, dateTime7.Month, day3, finishDate.Hour, finishDate.Minute, 0);
          }
          baseItem = false;
        }
        break;
      case Repetition.Yearly:
        if (startDate < this._viewStart)
        {
          startDate = new DateTime(this._viewStart.Year, startDate.Month, startDate.Day, startDate.Hour, startDate.Minute, 0);
          finishDate = new DateTime(this._viewStart.Year, finishDate.Month, finishDate.Day, finishDate.Hour, finishDate.Minute, 0);
          baseItem = false;
        }
        while (startDate < this._viewEnd)
        {
          CalendarItem calendarItem5 = this.CreateItem(objID, startDate, finishDate, caption, img, baseItem);
          if (baseItem)
            calendarItem2 = calendarItem5;
          startDate = startDate.AddYears(1);
          finishDate = finishDate.AddYears(1);
          baseItem = false;
        }
        break;
    }
    return calendarItem2;
  }

  /// <summary>
  /// Creates a new item on the current selection.
  /// If there's no selection, this will be ignored.
  /// </summary>
  /// <param name="objID"></param>
  /// <param name="caption">Text of the item</param>
  /// <param name="editMode">If <c>true</c> activates the edit mode so user can edit the text of the item.</param>
  /// <returns></returns>
  public CalendarItem CreateItemOnSelection(long objID, string caption, bool editMode)
  {
    if (this.SelectedElementEnd == null || this.SelectedElementStart == null)
      return (CalendarItem) null;
    TimeSpan timeSpan = this.SelectedElementEnd is SchedulerTimeScaleUnit selectedElementEnd ? selectedElementEnd.Duration : new TimeSpan(23, 59, 59);
    DateTime startDate = this.SelectedElementStart.Date;
    DateTime dateTime1 = this.SelectedElementEnd.Date;
    if (dateTime1.CompareTo(startDate) < 0)
    {
      DateTime dateTime2 = dateTime1;
      dateTime1 = startDate;
      startDate = dateTime2;
    }
    CalendarItem itemOnSelection = new CalendarItem(this, objID, startDate, dateTime1.Add(timeSpan), caption, true);
    SchedulerItemCancelEventArgs e = new SchedulerItemCancelEventArgs(itemOnSelection);
    this.OnItemCreating(e);
    if (e.Cancel)
      return (CalendarItem) null;
    this.Items.Add(itemOnSelection);
    if (!editMode)
      return itemOnSelection;
    this._creatingItem = true;
    this.ActivateEditMode(itemOnSelection);
    return itemOnSelection;
  }

  /// <summary>Удаление выделенных элементов планировщика.</summary>
  public void DeleteSelectedItems()
  {
    Stack<CalendarItem> calendarItemStack = new Stack<CalendarItem>();
    List<CalendarItem> selectedItems = this.SelectedItems;
    if (selectedItems.Count == 0)
      return;
    SchedulerItemsCancelEventArgs e = new SchedulerItemsCancelEventArgs(selectedItems);
    this.OnItemsDeleting(e);
    if (e.Cancel)
      return;
    foreach (CalendarItem calendarItem in selectedItems)
      this.Items.Remove(calendarItem);
    this.OnItemsDeleted(new SchedulerItemsEventArgs(selectedItems));
    this.Renderer.PerformItemsLayout();
  }

  /// <summary>
  /// Ensures the scrolling shows the specified time unit. It doesn't affect View date ranges.
  /// </summary>
  /// <param name="unit">Unit to ensure visibility</param>
  public void EnsureVisible(SchedulerTimeScaleUnit unit)
  {
    if (this.Days == null || this.Days.Length == 0)
      return;
    Rectangle bodyBounds = this.Days[0].BodyBounds;
    if (unit.Bounds.Bottom > bodyBounds.Bottom)
    {
      this.TimeUnitsOffset = -Convert.ToInt32(Math.Ceiling(unit.Date.TimeOfDay.TotalMinutes / (double) this.TimeScale)) + this.Renderer.GetVisibleTimeUnits();
    }
    else
    {
      if (unit.Bounds.Top >= bodyBounds.Top)
        return;
      this.TimeUnitsOffset = -Convert.ToInt32(Math.Ceiling(unit.Date.TimeOfDay.TotalMinutes / (double) this.TimeScale));
    }
  }

  /// <summary>
  /// Finalizes editing the <see cref="P:Intermech.Client.Core.Organizer.Scheduler.EditModeItem" />.
  /// </summary>
  /// <param name="cancel">Value indicating if edition of item should be canceled.</param>
  public void FinalizeEditMode(bool cancel)
  {
    if (!this.EditMode || this.EditModeItem == null || this._finalizingEdition)
      return;
    this._finalizingEdition = true;
    string caption = this._editModeItem.Caption;
    CalendarItem editModeItem = this._editModeItem;
    this._editModeItem = (CalendarItem) null;
    SchedulerItemCancelEventArgs e = new SchedulerItemCancelEventArgs(editModeItem);
    if (!cancel)
      editModeItem.Caption = this.TextBox.Text.Trim();
    if (this.TextBox != null)
    {
      this.TextBox.Visible = false;
      this.Controls.Remove((Control) this.TextBox);
      this.TextBox.Dispose();
    }
    if (this._editModeItem != null)
      this.Invalidate(editModeItem);
    this._textBox = (TextBox) null;
    if (this._creatingItem)
      this.OnItemCreated(e);
    else
      this.OnItemEdited(e);
    if (e.Cancel)
      editModeItem.Caption = caption;
    this._creatingItem = false;
    this._finalizingEdition = false;
    if (this.State != CalendarState.EditingItemText)
      return;
    this.SetState(CalendarState.Idle);
  }

  /// <summary>
  /// Finds the <see cref="T:Intermech.Client.Core.Organizer.CalendarDay" /> for the specified date, if in the view.
  /// </summary>
  /// <param name="d">Date to find day</param>
  /// <returns><see cref="T:Intermech.Client.Core.Organizer.CalendarDay" /> object that matches the date, <c>null</c> if day was not found.</returns>
  public CalendarDay FindDay(DateTime d)
  {
    if (this.Days == null)
      return (CalendarDay) null;
    for (int index = 0; index < this.Days.Length; ++index)
    {
      DateTime date = this.Days[index].Date;
      date = date.Date;
      if (date.Equals(d.Date.Date))
        return this.Days[index];
    }
    return (CalendarDay) null;
  }

  /// <summary>
  /// Gets the time unit that starts with the specified date.
  /// </summary>
  /// <param name="d">Date and time of the unit you want to extract</param>
  /// <returns>Matching time unit. <c>null</c> If out of range.</returns>
  public SchedulerTimeScaleUnit GetTimeUnit(DateTime d)
  {
    if (this.Days == null)
      return (SchedulerTimeScaleUnit) null;
    foreach (CalendarDay day in this.Days)
    {
      if (day.Date.Equals(d.Date))
      {
        double num = Convert.ToDouble((int) this.TimeScale);
        int int32 = Convert.ToInt32(Math.Floor(d.TimeOfDay.TotalMinutes / num));
        return day.TimeUnits[int32];
      }
    }
    return (SchedulerTimeScaleUnit) null;
  }

  /// <summary>
  /// Searches for the first hitted <see cref="T:Intermech.Client.Core.Organizer.ICalendarSelectableElement" />.
  /// </summary>
  /// <param name="p">Point to check for hit test</param>
  /// <returns></returns>
  public ICalendarSelectableElement HitTest(Point p) => this.HitTest(p, false);

  /// <summary>
  /// Searches for the first hitted <see cref="T:Intermech.Client.Core.Organizer.ICalendarSelectableElement" />.
  /// </summary>
  /// <param name="p">Point to check for hit test</param>
  /// <param name="ignoreItems"></param>
  /// <returns></returns>
  public ICalendarSelectableElement HitTest(Point p, bool ignoreItems)
  {
    if (!ignoreItems)
    {
      foreach (CalendarItem calendarItem in (List<CalendarItem>) this.Items)
      {
        foreach (Rectangle allBound in calendarItem.GetAllBounds())
        {
          if (allBound.Contains(p))
            return (ICalendarSelectableElement) calendarItem;
        }
      }
    }
    for (int index1 = 0; index1 < this.Days.Length; ++index1)
    {
      Rectangle bounds = this.Days[index1].Bounds;
      if (bounds.Contains(p))
      {
        if (this.DaysMode == CalendarDaysMode.Expanded)
        {
          bounds = this.Days[index1].DayTop.Bounds;
          if (bounds.Contains(p))
            return (ICalendarSelectableElement) this.Days[index1].DayTop;
          for (int index2 = 0; index2 < this.Days[index1].TimeUnits.Length; ++index2)
          {
            if (this.Days[index1].TimeUnits[index2].Visible)
            {
              bounds = this.Days[index1].TimeUnits[index2].Bounds;
              if (bounds.Contains(p))
                return (ICalendarSelectableElement) this.Days[index1].TimeUnits[index2];
            }
          }
          return (ICalendarSelectableElement) this.Days[index1];
        }
        if (this.DaysMode == CalendarDaysMode.Short)
          return (ICalendarSelectableElement) this.Days[index1];
      }
    }
    return (ICalendarSelectableElement) null;
  }

  /// <summary>Invalidates the bounds of the specified day.</summary>
  /// <param name="day"></param>
  public void Invalidate(CalendarDay day) => this.Invalidate(day.Bounds);

  /// <summary>Ivalidates the bounds of the specified unit.</summary>
  /// <param name="unit"></param>
  public void Invalidate(SchedulerTimeScaleUnit unit) => this.Invalidate(unit.Bounds);

  /// <summary>Invalidates the area of the specified item.</summary>
  /// <param name="item"></param>
  public void Invalidate(CalendarItem item)
  {
    Rectangle rectangle = item.Bounds;
    foreach (Rectangle allBound in item.GetAllBounds())
      rectangle = Rectangle.Union(rectangle, allBound);
    rectangle.Inflate(this.Renderer.ItemShadowPadding + this.Renderer.ItemInvalidateMargin, this.Renderer.ItemShadowPadding + this.Renderer.ItemInvalidateMargin);
    this.Invalidate(rectangle);
  }

  /// <summary>
  /// Returns the item hitted at the specified location. Null if no item hitted.
  /// </summary>
  /// <param name="p">Location to serach for items</param>
  /// <returns>Hitted item at the location. Null if no item hitted.</returns>
  public CalendarItem ItemAt(Point p) => this.HitTest(p) as CalendarItem;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="ts"></param>
  public void SetTimeUnit(TimeSpan ts)
  {
    if (this.Days == null || ts == TimeSpan.Zero || ts == TimeSpan.MinValue)
      return;
    int num1 = this.Days[0].TimeUnits.Length / 24;
    int num2 = 60 / num1;
    if (ts.Hours == 24)
      ts = new TimeSpan(0, ts.Minutes, 0);
    int num3 = 0;
    switch (this._timeScale)
    {
      case CalendarTimeScale.ThirtyMinutes:
        num3 = 2;
        break;
    }
    int num4 = ts.Hours * num1 + ts.Minutes / num2;
    if (num4 > num3)
      num4 -= num3;
    int num5 = this.Days[0].TimeUnits.Length - this.Renderer.GetVisibleTimeUnits();
    while (num4 > num5)
      --num4;
    this.TimeUnitsOffset = -1 * num4;
  }

  /// <summary>
  /// Establishes the selection range with only one graphical update.
  /// </summary>
  /// <param name="selectionStart">Fisrt selected element</param>
  /// <param name="selectionEnd">Last selection element</param>
  public void SetSelectionRange(
    ICalendarSelectableElement selectionStart,
    ICalendarSelectableElement selectionEnd)
  {
    this._selectedElementStart = selectionStart;
    this.SelectedElementEnd = selectionEnd;
  }

  /// <summary>
  /// Sets the value of <see cref="P:Intermech.Client.Core.Organizer.Scheduler.ViewStart" /> and <see cref="P:Intermech.Client.Core.Organizer.Scheduler.ViewEnd" /> properties triggering only one repaint process.
  /// </summary>
  /// <param name="dateStart">Start date of view</param>
  /// <param name="dateEnd">End date of view</param>
  public void SetViewRange(DateTime dateStart, DateTime dateEnd)
  {
    this._viewStart = dateStart.Date;
    this.ViewEnd = dateEnd;
  }

  /// <summary>
  /// Returns a value indicating if the view range intersects the specified date range.
  /// </summary>
  /// <param name="dateStart"></param>
  /// <param name="dateEnd"></param>
  public bool ViewIntersects(DateTime dateStart, DateTime dateEnd)
  {
    return Scheduler.DateIntersects(this.ViewStart, this.ViewEnd, dateStart, dateEnd);
  }

  /// <summary>
  /// Returns a value indicating if the view range intersect the date range of the specified item.
  /// </summary>
  /// <param name="item"></param>
  public bool ViewIntersects(CalendarItem item)
  {
    return this.ViewIntersects(item.StartDate, item.EndDate);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="index"></param>
  public delegate void CalendarHeaderButtonClickEventHandler(object sender, int index);
}
