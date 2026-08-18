
// Type: Intermech.Client.Core.Organizer.CalendarView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Calendars;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Client.Core.Organizer;

/// <summary>Календарь. Элемент пользовательского интерфейса.</summary>
[Designer(typeof (CalendarViewDesigner))]
public class CalendarView : ContainerControl
{
  private ICalendar _calendarSettings;
  /// <summary>Дата, с которой начинается отображаемый месяц</summary>
  private DateTime _viewStart = DateTime.Today;
  /// <summary>
  /// Первый день недели (в зависимости от локальных настрое)
  /// </summary>
  private DayOfWeek _firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
  /// <summary>Набор календарей, которые помещаются на контрол</summary>
  private List<MonthView> _monthes;
  /// <summary>Формат отображения заголовка месяца</summary>
  private string _monthTitleFormat = "MMMM yyyy";
  /// <summary>Цвет заголовка месяца</summary>
  private Color _monthTitleColor = SystemColors.ActiveCaption;
  /// <summary>Цвет текста заголовка месяца</summary>
  private Color _monthTitleTextColor = SystemColors.ActiveCaptionText;
  /// <summary>Формат отображения наименований дней</summary>
  private string _dayNamesFormat = "ddd";
  /// <summary>Отображение наименования дней недели</summary>
  private bool _dayNamesVisible = true;
  /// <summary>Количество букв в сокращенном наименовании дня</summary>
  private int _dayNamesLength = 2;
  /// <summary>Цвет фона дня</summary>
  private Color _dayBackColor = SystemColors.Window;
  /// <summary>Цвет фона выделенного дня</summary>
  private Color _daySelectedBackColor = Color.Orange;
  /// <summary>Цвет текста выделенного дня</summary>
  private Color _daySelectedTextColor = SystemColors.ControlText;
  /// <summary>Цвет дней не принадлежащих данному месяцу</summary>
  private Color _dayGrayedText = SystemColors.GrayText;
  /// <summary>Цвет границы текущего дня</summary>
  private Color _todayBorderColor = Color.Maroon;
  /// <summary>Отступы</summary>
  private Padding _itemPadding = new Padding(2);
  /// <summary>Флаг нажатия кнопки</summary>
  private bool _isMouseDown;
  /// <summary>Способ выделения</summary>
  private DateSelectionMode _selectionMode;
  /// <summary>День, с которого началось выделение</summary>
  private DayView _startSelectionDay;
  /// <summary>Последний день под курсором (при передвижении мыши)</summary>
  private DayView _lastDayUnderCursor;
  /// <summary>Площадь левой кнопки навигации месяца</summary>
  private Rectangle _backwardButtonBounds = Rectangle.Empty;
  /// <summary>Площадь правой кнопки навигации месяца</summary>
  private Rectangle _forwardButtonBounds = Rectangle.Empty;
  private bool _backwardButtonSelected;
  private bool _forwardButtonSelected;
  /// <summary>Идентификатор атрибута "Начато"</summary>
  private int _attrIDStartDate;
  /// <summary>Идентификатор типа объектов "Задачи органайзера"</summary>
  private int _taskTypeID = -1;
  /// <summary>
  /// Дата, которую выбирает пользователь руками, щелкая по датам в календаре
  /// или переключая на конкретную дату выбором дня на вкладках Неделя и Месяц
  /// </summary>
  public DateTime DateChoosedByUser = DateTime.UtcNow;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool BackwardButtonSelected
  {
    get => this._backwardButtonSelected;
    set
    {
      this._backwardButtonSelected = value;
      this.Invalidate(this._backwardButtonBounds);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public ICalendar CalendarSettings
  {
    get => this._calendarSettings;
    set
    {
      this._calendarSettings = value;
      this.SetCalendarSettings();
      switch (this._selectionMode)
      {
        case DateSelectionMode.Week:
          this.SelectWeek(this.SelectionBegin, true);
          break;
        case DateSelectionMode.WorkWeek:
          this.SelectWeek(this.SelectionBegin, true);
          break;
      }
    }
  }

  /// <summary>Способ выделения.</summary>
  [DefaultValue(DateSelectionMode.Days)]
  [Category("Appearance")]
  public DateSelectionMode DateSelectionMode
  {
    get => this._selectionMode;
    set
    {
      if (this._selectionMode == value)
        return;
      this._selectionMode = value;
      switch (value)
      {
        case DateSelectionMode.Days:
          this.SelectDay(this.DateChoosedByUser, true);
          break;
        case DateSelectionMode.Month:
          this.SelectMonth(this.SelectionBegin);
          break;
        case DateSelectionMode.Week:
          this.SelectWeek(this.SelectionBegin, true);
          break;
        case DateSelectionMode.WorkWeek:
          this.SelectWeek(this.SelectionBegin, true);
          break;
      }
      this.Invalidate();
    }
  }

  /// <summary>Формат отображения наименований дней недели.</summary>
  [DefaultValue("ddd")]
  [Category("Appearance")]
  public string DayNamesFormat
  {
    get => this._dayNamesFormat;
    set
    {
      this._dayNamesFormat = value;
      this.Invalidate();
    }
  }

  /// <summary>Количество букв в сокращенном наименовании дня.</summary>
  [DefaultValue(2)]
  [Category("Appearance")]
  public int DayNamesLength
  {
    get => this._dayNamesLength;
    set
    {
      this._dayNamesLength = value;
      this.CalcMonthSize();
      this.UpdateMonths();
      this.Invalidate();
    }
  }

  /// <summary>Отображение наименований дней недели.</summary>
  [DefaultValue(true)]
  [Category("Appearance")]
  public bool DayNamesVisible
  {
    get => this._dayNamesVisible;
    set
    {
      this._dayNamesVisible = value;
      this.CalcMonthSize();
      this.UpdateMonths();
      this.Invalidate();
    }
  }

  /// <summary>Размер одного "дня".</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Size DaySize { get; private set; }

  /// <summary>
  /// Первый день недели (в зависимости от локальных настрое).
  /// </summary>
  [DefaultValue(DayOfWeek.Monday)]
  [Category("Appearance")]
  public DayOfWeek FirstDayOfWeek
  {
    get => this._firstDayOfWeek;
    set
    {
      this._firstDayOfWeek = value;
      this.UpdateMonths();
      this.Invalidate();
    }
  }

  /// <summary>Шрифт элемента управления.</summary>
  public override Font Font
  {
    get => base.Font;
    set
    {
      base.Font = value;
      this.CalcMonthSize();
      this.UpdateMonths();
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool ForwardButtonSelected
  {
    get => this._forwardButtonSelected;
    set
    {
      this._forwardButtonSelected = value;
      this.Invalidate(this._forwardButtonBounds);
    }
  }

  /// <summary>Отступы между элементами.</summary>
  [DefaultValue(2)]
  [Category("Appearance")]
  public Padding ItemPadding
  {
    get => this._itemPadding;
    set
    {
      this._itemPadding = value;
      this.CalcMonthSize();
      this.UpdateMonths();
      this.Invalidate();
    }
  }

  /// <summary>
  /// Свойство сообщает, что процесс выделения интервала дат окончен.
  /// Введено для случая, когда выделение интервала осуществляется при помощи мыши.
  /// false - когда левая кнопка мыши нажата и происходит перемещение по календарю,
  /// true - когда левая кнопка мыши отпущена, т.е. выделение интервала дат окончено.
  /// </summary>
  [Browsable(false)]
  public bool IsEndSelection => !this._isMouseDown;

  /// <summary>Максимально возможное количество выделенных дней.</summary>
  [DefaultValue(42)]
  [Category("Appearance")]
  public int MaxSelectionCount { get; set; }

  /// <summary>Размер одного "месяца".</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Size MonthSize { get; private set; }

  /// <summary>Формат отображения заголовка месяца.</summary>
  [DefaultValue("MMMM yyyy")]
  [Category("Appearance")]
  public string MonthTitleFormat
  {
    get => this._monthTitleFormat;
    set
    {
      this._monthTitleFormat = value;
      this.Invalidate();
    }
  }

  /// <summary>Первый выделенный день.</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public DateTime SelectionBegin { get; private set; }

  /// <summary>Последний выделенный день.</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public DateTime SelectionEnd { get; private set; }

  /// <summary>Дата, которой заканчиваются отображаеме месяцы.</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public DateTime ViewEnd
  {
    get
    {
      DateTime firstDateOfMonth = this._monthes[this._monthes.Count - 1].FirstDateOfMonth;
      return firstDateOfMonth.Date.AddDays((double) DateTime.DaysInMonth(firstDateOfMonth.Year, firstDateOfMonth.Month));
    }
  }

  /// <summary>Дата, с которой начинается отображаеме месяцы.</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public DateTime ViewStart
  {
    get => this._viewStart;
    set
    {
      this._viewStart = value;
      this.UpdateMonths();
      this.Invalidate();
    }
  }

  /// <summary>Конечный день рабочей недели.</summary>
  [DefaultValue(DayOfWeek.Sunday)]
  public DayOfWeek WorkWeekEnd { get; set; }

  /// <summary>Начальный день рабочей недели.</summary>
  [DefaultValue(DayOfWeek.Monday)]
  public DayOfWeek WorkWeekStart { get; set; }

  /// <summary>Цвет фона дня.</summary>
  [DefaultValue(typeof (Color), "Window")]
  [Category("Color scheme")]
  public Color DayBackColor
  {
    get => this._dayBackColor;
    set
    {
      this._dayBackColor = value;
      this.Invalidate();
    }
  }

  /// <summary>Цвет дней не принадлежащих данному месяцу.</summary>
  [DefaultValue(typeof (Color), "GrayText")]
  [Category("Color scheme")]
  public Color DayGrayedText
  {
    get => this._dayGrayedText;
    set
    {
      this._dayGrayedText = value;
      this.Invalidate();
    }
  }

  /// <summary>Цвет фона выделенного дня.</summary>
  [DefaultValue(typeof (Color), "Orange")]
  [Category("Color scheme")]
  public Color DaySelectedBackColor
  {
    get => this._daySelectedBackColor;
    set
    {
      this._daySelectedBackColor = value;
      this.Invalidate();
    }
  }

  /// <summary>Цвет текста выделенного дня.</summary>
  [DefaultValue(typeof (Color), "ControlText")]
  [Category("Color scheme")]
  public Color DaySelectedTextColor
  {
    get => this._daySelectedTextColor;
    set
    {
      this._daySelectedTextColor = value;
      this.Invalidate();
    }
  }

  /// <summary>Цвет текста дня.</summary>
  [DefaultValue(typeof (Color), "ControlText")]
  [Category("Color scheme")]
  public Color DayTextColor
  {
    get => this.ForeColor;
    set
    {
      this.ForeColor = value;
      this.Invalidate();
    }
  }

  /// <summary>Цвет заголовка месяца.</summary>
  [DefaultValue(typeof (Color), "ActiveCaption")]
  [Category("Color scheme")]
  public Color MonthTitleColor
  {
    get => this._monthTitleColor;
    set
    {
      this._monthTitleColor = value;
      this.Invalidate();
    }
  }

  /// <summary>Цвет текста заголовка месяца.</summary>
  [DefaultValue(typeof (Color), "ActiveCaptionText")]
  [Category("Color scheme")]
  public Color MonthTitleTextColor
  {
    get => this._monthTitleTextColor;
    set
    {
      this._monthTitleTextColor = value;
      this.Invalidate();
    }
  }

  /// <summary>Цвет границы текущего дня.</summary>
  [DefaultValue(typeof (Color), "Maroon")]
  [Category("Color scheme")]
  public Color TodayBorderColor
  {
    get => this._todayBorderColor;
    set
    {
      this._todayBorderColor = value;
      this.Invalidate();
    }
  }

  /// <summary>Конструктор.</summary>
  public CalendarView()
  {
    this.SetStyle(ControlStyles.DoubleBuffer, true);
    this.SetStyle(ControlStyles.UserPaint, true);
    this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    this.InitializeComponent();
    this.MaxSelectionCount = 42;
    this.WorkWeekStart = DayOfWeek.Monday;
    this.WorkWeekEnd = DayOfWeek.Sunday;
    this.SelectionBegin = DateTime.Today;
    this.SelectionEnd = this.SelectionBegin.Add(new TimeSpan(23, 59, 59));
    DateTime today = DateTime.Today;
    int year = today.Year;
    today = DateTime.Today;
    int month = today.Month;
    this._viewStart = new DateTime(year, month, 1);
    this.CalcMonthSize();
    this._attrIDStartDate = MetaDataHelper.GetAttributeTypeID(SystemGUIDs.attributeStart);
    this._taskTypeID = MetaDataHelper.GetObjectTypeID("cad015bc-306c-11d8-b4e9-00304f19f545");
  }

  /// <summary>Изменение выделенного дня/дней.</summary>
  public event EventHandler SelectionChanged;

  /// <summary>Навигация по месяцам.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnArrow_Click(object sender, EventArgs e)
  {
    this.Focus();
    if (Convert.ToInt16((sender as ImageButton).Tag) == (short) 0)
      this.ViewStart = this.ViewStart.AddMonths(-1);
    else
      this.ViewStart = this.ViewStart.AddMonths(1);
  }

  /// <summary>Нажатие кнопки мыши.</summary>
  /// <param name="e"></param>
  protected override void OnMouseDown(MouseEventArgs e)
  {
    base.OnMouseDown(e);
    this.Focus();
    this._isMouseDown = true;
    if (this.ForwardButtonSelected)
      this.ArrowClick(true);
    else if (this.BackwardButtonSelected)
    {
      this.ArrowClick(false);
    }
    else
    {
      DayView dayView = this.SearchDayUnderCursor(e.Location);
      if (dayView == null)
        return;
      this._startSelectionDay = this._lastDayUnderCursor = dayView;
      switch (this.DateSelectionMode)
      {
        case DateSelectionMode.Days:
          this.SelectDay(dayView.Date, true);
          this.DateChoosedByUser = dayView.Date;
          break;
        case DateSelectionMode.Month:
          this.SelectMonth(dayView.Date);
          break;
        case DateSelectionMode.Week:
          this.SelectWeek(dayView.Date, true);
          break;
        case DateSelectionMode.WorkWeek:
          this.SelectWeek(dayView.Date, true);
          break;
      }
    }
  }

  /// <summary>Движение мыши.</summary>
  /// <param name="e"></param>
  protected override void OnMouseMove(MouseEventArgs e)
  {
    base.OnMouseMove(e);
    if (this._forwardButtonBounds.Contains(e.Location))
      this.ForwardButtonSelected = true;
    else if (this.ForwardButtonSelected)
      this.ForwardButtonSelected = false;
    if (this._backwardButtonBounds.Contains(e.Location))
      this.BackwardButtonSelected = true;
    else if (this.BackwardButtonSelected)
      this.BackwardButtonSelected = false;
    if (!this._isMouseDown || this.DateSelectionMode == DateSelectionMode.WorkWeek)
      return;
    DayView dayView = this.SearchDayUnderCursor(e.Location);
    if (dayView == null || dayView == this._lastDayUnderCursor)
      return;
    switch (this.DateSelectionMode)
    {
      case DateSelectionMode.Days:
        this.SelectDay(dayView.Date, false);
        break;
      case DateSelectionMode.Week:
        this.SelectWeek(dayView.Date, false);
        break;
      case DateSelectionMode.WorkWeek:
        this.SelectWeek(dayView.Date, false);
        break;
    }
    this._lastDayUnderCursor = dayView;
  }

  /// <summary>Освобождение кнопки мыши.</summary>
  /// <param name="e"></param>
  protected override void OnMouseUp(MouseEventArgs e)
  {
    base.OnMouseUp(e);
    this._isMouseDown = false;
    this._startSelectionDay = this._lastDayUnderCursor = (DayView) null;
    this.OnSelectionChanged(EventArgs.Empty);
  }

  /// <summary>Скролирование календаря.</summary>
  /// <param name="e"></param>
  protected override void OnMouseWheel(MouseEventArgs e)
  {
    base.OnMouseWheel(e);
    this.ArrowClick(e.Delta < 0);
  }

  /// <summary>Отрисовка контрола.</summary>
  /// <param name="e"></param>
  protected override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    e.Graphics.Clear(SystemColors.Window);
    int num = 0;
    foreach (MonthView monthe in this._monthes)
    {
      Rectangle rectangle = monthe.Bounds;
      if (rectangle.IntersectsWith(e.ClipRectangle))
      {
        this.DrawBox(new CalendarBoxEventArgs(e.Graphics, monthe.HeaderBounds, monthe.Caption, this.MonthTitleTextColor, this.MonthTitleColor));
        if (monthe.DayNamesBounds != null && monthe.DayNamesBounds.Count > 0)
        {
          for (int index = 0; index < monthe.DayNamesBounds.Count; ++index)
            this.DrawBox(new CalendarBoxEventArgs(e.Graphics, monthe.DayNamesBounds[index], monthe.DayNames[index], StringAlignment.Far, this.ForeColor, this._dayBackColor));
          using (Pen pen1 = new Pen(this.MonthTitleColor))
          {
            rectangle = monthe.DayNamesBounds[0];
            int bottom = rectangle.Bottom;
            Graphics graphics = e.Graphics;
            Pen pen2 = pen1;
            rectangle = monthe.Bounds;
            Point pt1 = new Point(rectangle.X, bottom);
            rectangle = monthe.Bounds;
            Point pt2 = new Point(rectangle.Right, bottom);
            graphics.DrawLine(pen2, pt1, pt2);
          }
        }
        foreach (DayView day in monthe.Days)
        {
          if (day.Visible)
          {
            CalendarBoxEventArgs e1 = new CalendarBoxEventArgs(e.Graphics, day.Bounds, day.Date.Day.ToString(), StringAlignment.Far, day.Grayed ? this.DayGrayedText : (day.Selected ? this.DaySelectedTextColor : this.ForeColor), day.Selected ? this.DaySelectedBackColor : this.DayBackColor)
            {
              IsMarked = day.IsMarked && !day.Selected
            };
            if (day.Date.Equals(DateTime.Now.Date))
              e1.BorderColor = this.TodayBorderColor;
            this.DrawBox(e1);
          }
        }
        if (num == 0)
        {
          Rectangle backwardButtonBounds = this._backwardButtonBounds;
          using (Brush brush = (Brush) new SolidBrush(this._backwardButtonSelected ? Color.Gold : Color.White))
          {
            Point[] points = new Point[3]
            {
              new Point(backwardButtonBounds.Right, backwardButtonBounds.Top),
              new Point(backwardButtonBounds.Right, backwardButtonBounds.Bottom - 1),
              new Point(backwardButtonBounds.Left + backwardButtonBounds.Width / 2, backwardButtonBounds.Top + backwardButtonBounds.Height / 2)
            };
            e.Graphics.FillPolygon(brush, points);
          }
          Rectangle forwardButtonBounds = this._forwardButtonBounds;
          using (Brush brush = (Brush) new SolidBrush(this._forwardButtonSelected ? Color.Gold : Color.White))
          {
            Point[] points = new Point[3]
            {
              new Point(forwardButtonBounds.Left, forwardButtonBounds.Top),
              new Point(forwardButtonBounds.Left, forwardButtonBounds.Bottom - 1),
              new Point(forwardButtonBounds.Left + forwardButtonBounds.Width / 2, forwardButtonBounds.Top + forwardButtonBounds.Height / 2)
            };
            e.Graphics.FillPolygon(brush, points);
          }
          ++num;
        }
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public override void Refresh()
  {
    base.Refresh();
    this.UpdateMonths();
    this.Invalidate();
  }

  /// <summary>Изменение размеров контрола.</summary>
  /// <param name="e"></param>
  protected override void OnResize(EventArgs e) => this.Refresh();

  /// <summary>
  /// 
  /// </summary>
  public void ArrowClick(bool forward)
  {
    this.ViewStart = this.ViewStart.AddMonths(forward ? 1 : -1);
  }

  /// <summary>Расчитывает размер дня и календаря.</summary>
  private void CalcMonthSize()
  {
    List<string> stringList1 = new List<string>(38);
    int val2_1 = 0;
    int val2_2 = 0;
    if (this.DayNamesVisible)
    {
      for (int index = 0; index < 7; ++index)
      {
        List<string> stringList2 = stringList1;
        DateTime dateTime = this.ViewStart;
        dateTime = dateTime.AddDays((double) index);
        string str = dateTime.ToString(this.DayNamesFormat).Substring(0, this.DayNamesLength);
        stringList2.Add(str);
      }
    }
    for (int index = 1; index < 32 /*0x20*/; ++index)
      stringList1.Add(index.ToString());
    using (Font font = new Font(this.Font, FontStyle.Bold))
    {
      foreach (string text in stringList1)
      {
        Size size = TextRenderer.MeasureText(text, font);
        val2_1 = Math.Max(size.Width, val2_1);
        val2_2 = Math.Max(size.Height, val2_2);
      }
    }
    int num1 = val2_1;
    Padding itemPadding = this.ItemPadding;
    int horizontal = itemPadding.Horizontal;
    int width = num1 + horizontal;
    int num2 = val2_2;
    itemPadding = this.ItemPadding;
    int vertical = itemPadding.Vertical;
    int height = num2 + vertical;
    this.DaySize = new Size(width, height);
    int num3 = height;
    if (height < 16 /*0x10*/)
      num3 = 16 /*0x10*/;
    else if (height % 2 != 0)
      ++num3;
    this.MonthSize = new Size(width * 7, height * 6 + (this.DayNamesVisible ? height : 0) + num3);
  }

  /// <summary>Отрисовка области календаря.</summary>
  /// <param name="e">Данные области отрисовки</param>
  private void DrawBox(CalendarBoxEventArgs e)
  {
    if (!e.BackgroundColor.IsEmpty)
    {
      using (SolidBrush solidBrush = new SolidBrush(e.BackgroundColor))
        e.Graphics.FillRectangle((Brush) solidBrush, e.Bounds);
    }
    if (!e.TextColor.IsEmpty && !string.IsNullOrEmpty(e.Text))
    {
      if (e.IsMarked)
      {
        using (Brush brush = (Brush) new SolidBrush(Color.FromArgb(173, 209, (int) byte.MaxValue)))
        {
          Rectangle rect;
          ref Rectangle local = ref rect;
          int x1 = e.Bounds.X;
          Rectangle bounds = e.Bounds;
          int width1 = bounds.Width;
          bounds = e.Bounds;
          int height1 = bounds.Height;
          int num = width1 - height1 - 1;
          int x2 = x1 + num;
          bounds = e.Bounds;
          int y = bounds.Y + 1;
          bounds = e.Bounds;
          int width2 = bounds.Height - 1;
          bounds = e.Bounds;
          int height2 = bounds.Height - 1;
          local = new Rectangle(x2, y, width2, height2);
          e.Graphics.FillEllipse(brush, rect);
        }
      }
      TextRenderer.DrawText((IDeviceContext) e.Graphics, e.Text, e.Font != null ? e.Font : this.Font, e.Bounds, e.TextColor, e.TextFlags);
    }
    if (e.BorderColor.IsEmpty)
      return;
    using (Pen pen = new Pen(e.BorderColor))
    {
      Rectangle bounds = e.Bounds;
      --bounds.Width;
      --bounds.Height;
      e.Graphics.DrawRectangle(pen, bounds);
    }
  }

  /// <summary>Изменение выделения.</summary>
  /// <param name="e"></param>
  private void OnSelectionChanged(EventArgs e)
  {
    if (this.SelectionChanged == null)
      return;
    this.SelectionChanged((object) this, e);
  }

  /// <summary>Поиск дня недели под курсором мыши.</summary>
  /// <param name="p">Координаты курсора</param>
  /// <returns></returns>
  private DayView SearchDayUnderCursor(Point p)
  {
    DayView dayView = (DayView) null;
    MonthView monthView = this._monthes.FirstOrDefault<MonthView>((System.Func<MonthView, bool>) (x => x.Bounds.Contains(p)));
    if (monthView != null)
      dayView = monthView.Days.FirstOrDefault<DayView>((System.Func<DayView, bool>) (x => x.Bounds.Contains(p)));
    return dayView;
  }

  /// <summary>Выделение дня.</summary>
  /// <param name="day">Текущий день недели</param>
  /// <param name="oneAction">Однократное действие (например клик).
  /// В таком случае начальный и конечный дни совпадают</param>
  private void SelectDay(DateTime day, bool oneAction)
  {
    if (oneAction)
    {
      DateTime dateTime = this.DesignMode ? DateTime.Today : day;
      this.SetSelection(dateTime, dateTime, false);
    }
    else
    {
      if (this._startSelectionDay == null)
        return;
      if (day < this._startSelectionDay.Date)
        this.SetSelection(day, this._startSelectionDay.Date, true);
      else
        this.SetSelection(this._startSelectionDay.Date, day, true);
    }
  }

  /// <summary>Выделение недели.</summary>
  /// <param name="day">Текущий день недели</param>
  /// <param name="oneAction">Однократное действие (например клик)</param>
  private void SelectWeek(DateTime day, bool oneAction)
  {
    day = this.DesignMode ? DateTime.Today : day;
    int num1 = day.DayOfWeek - this._firstDayOfWeek;
    DateTime dateBeg1 = day.AddDays((double) (-1 * (num1 < 0 ? 7 + num1 : num1)));
    if (oneAction)
    {
      if (this.DateSelectionMode == DateSelectionMode.Week)
      {
        this.SetSelection(dateBeg1, dateBeg1.AddDays(6.0), false);
      }
      else
      {
        if (this.DateSelectionMode != DateSelectionMode.WorkWeek)
          return;
        this.SetSelection(dateBeg1, dateBeg1.AddDays(6.0), false);
      }
    }
    else
    {
      if (this._startSelectionDay == null)
        return;
      DateTime date = this._startSelectionDay.Date;
      int num2 = date.DayOfWeek - this._firstDayOfWeek;
      date = this._startSelectionDay.Date;
      DateTime dateBeg2 = date.AddDays((double) (-1 * (num2 < 0 ? 7 + num2 : num2)));
      if (day < this._startSelectionDay.Date)
        this.SetSelection(dateBeg1, dateBeg2.AddDays(6.0), true);
      else
        this.SetSelection(dateBeg2, dateBeg1.AddDays(6.0), true);
    }
  }

  /// <summary>Выделение месяца.</summary>
  /// <param name="day">Текущий день недели</param>
  private void SelectMonth(DateTime day)
  {
    DateTime dateTime = new DateTime(day.Year, day.Month, 1);
    int num = dateTime.DayOfWeek - this._firstDayOfWeek;
    DateTime dateBeg = dateTime.AddDays((double) (-1 * (num < 0 ? 7 + num : num)));
    this.SetSelection(dateBeg, dateBeg.AddDays(34.0), false);
  }

  /// <summary>
  /// 
  /// </summary>
  private void SetCalendarSettings()
  {
    if (this._monthes == null)
      return;
    this._monthes.ForEach((Action<MonthView>) (x => x.ReadCalendarSettings()));
    this.FirstDayOfWeek = this._calendarSettings.WeekStartDay;
  }

  /// <summary>Обновить календари в контроле.</summary>
  private void UpdateMonths()
  {
    int num1 = 2;
    Size size1 = this.ClientSize;
    double width1 = (double) size1.Width;
    size1 = this.MonthSize;
    double num2 = (double) (size1.Width + num1);
    int int32_1 = Convert.ToInt32(Math.Max(Math.Floor(width1 / num2), 1.0));
    Size size2 = this.ClientSize;
    double height1 = (double) size2.Height;
    size2 = this.MonthSize;
    double num3 = (double) (size2.Height + num1);
    int int32_2 = Convert.ToInt32(Math.Max(Math.Floor(height1 / num3), 1.0));
    int capacity = int32_1 * int32_2;
    int num4 = int32_1 * this.MonthSize.Width + (int32_1 - 1) * num1;
    int num5 = int32_2 * this.MonthSize.Height + (int32_2 - 1) * num1;
    int num6 = (this.ClientSize.Width - num4) / 2;
    int num7 = (this.ClientSize.Height - num5) / 2;
    int x1 = num6;
    int y1 = num7;
    this._monthes = new List<MonthView>(capacity);
    Size size3;
    for (int months = 0; months < capacity; ++months)
    {
      MonthView monthView = new MonthView(this, this.ViewStart.AddMonths(months));
      monthView.SetLocation(new Point(x1, y1));
      this._monthes.Add(monthView);
      int num8 = x1;
      int num9 = num1;
      size3 = this.MonthSize;
      int width2 = size3.Width;
      int num10 = num9 + width2;
      x1 = num8 + num10;
      if ((months + 1) % int32_1 == 0)
      {
        x1 = num6;
        int num11 = y1;
        int num12 = num1;
        size3 = this.MonthSize;
        int height2 = size3.Height;
        int num13 = num12 + height2;
        y1 = num11 + num13;
      }
    }
    MonthView monthe1 = this._monthes[0];
    MonthView monthe2 = this._monthes[this._monthes.Count - 1];
    int x2 = monthe1.Bounds.Left + this.ItemPadding.Left;
    int y2 = monthe1.Bounds.Top + this.ItemPadding.Top;
    size3 = this.DaySize;
    int height3 = size3.Height;
    Padding itemPadding = this.ItemPadding;
    int horizontal = itemPadding.Horizontal;
    int width3 = height3 - horizontal;
    size3 = this.DaySize;
    int height4 = size3.Height;
    itemPadding = this.ItemPadding;
    int vertical = itemPadding.Vertical;
    int height5 = height4 - vertical;
    this._backwardButtonBounds = new Rectangle(x2, y2, width3, height5);
    int right1 = monthe1.Bounds.Right;
    itemPadding = this.ItemPadding;
    int right2 = itemPadding.Right;
    int x3 = right1 - right2 - this._backwardButtonBounds.Width;
    int top1 = monthe1.Bounds.Top;
    itemPadding = this.ItemPadding;
    int top2 = itemPadding.Top;
    int y3 = top1 + top2;
    int width4 = this._backwardButtonBounds.Width;
    int height6 = this._backwardButtonBounds.Height;
    this._forwardButtonBounds = new Rectangle(x3, y3, width4, height6);
    if (this.Site != null && this.Site.DesignMode)
      return;
    this.GetItems(monthe1.FirstDay.Date, monthe2.LastDay.Date);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="startDate"></param>
  /// <param name="finishDate"></param>
  private void GetItems(DateTime startDate, DateTime finishDate)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ConditionStructure[] joinedConditions = new ConditionStructure[1]
      {
        new ConditionStructure(this._attrIDStartDate, RelationalOperators.Between, (object) startDate, (object) finishDate, LogicalOperators.NONE, 0, true)
        {
          AttributeSource = AttributeSourceTypes.Object
        }
      };
      ColumnDescriptor[] columns = new ColumnDescriptor[1]
      {
        new ColumnDescriptor((object) this._attrIDStartDate, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.ASC, 0)
      };
      IDBRecords objectCollection = (IDBRecords) sessionKeeper.Session.GetObjectCollection(this._taskTypeID);
      if (objectCollection != null)
      {
        DBRecordSetParams pars = new DBRecordSetParams(ConditionStructure.Join(joinedConditions, OrganizerTaskNode.DefaultConditions), columns);
        this.MakrDays(objectCollection, pars);
      }
      DescriptorCollection descriptors = (ServicesManager.GetService(typeof (IOrganizerService)) as OrganizerService).Descriptors;
      for (int index = 0; index < descriptors.Count; ++index)
      {
        OrganizerChildNodeDescriptor childNodeDescriptor = descriptors[index] as OrganizerChildNodeDescriptor;
        INodeID recordNodeId = childNodeDescriptor.GetRecordNodeID();
        if (recordNodeId != null && recordNodeId.TypeID != -1)
        {
          IDBRecords collection = childNodeDescriptor.GetCollection(sessionKeeper.Session);
          if (collection != null)
          {
            DBRecordSetParams pars = new DBRecordSetParams(ConditionStructure.Join(joinedConditions, childNodeDescriptor.Conditions), columns);
            this.MakrDays(collection, pars);
          }
        }
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="coll"></param>
  /// <param name="pars"></param>
  private void MakrDays(IDBRecords coll, DBRecordSetParams pars)
  {
    DataTable dataTable = coll.Select(pars);
    if (dataTable == null)
      return;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      DateTime result = DateTime.MinValue;
      if (DateTime.TryParse(Convert.ToString(row[0]), out result))
      {
        DateTime onlyDate = result.Date;
        foreach (MonthView monthe in this._monthes)
        {
          DayView dayView = monthe.Days.FirstOrDefault<DayView>((System.Func<DayView, bool>) (x => x.Date.Date == onlyDate));
          if (dayView != null)
            dayView.IsMarked = true;
        }
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public List<int> GetExcludedDaysForMonth(DateTime date)
  {
    int monthNum = date.Month;
    return this._monthes.FirstOrDefault<MonthView>((System.Func<MonthView, bool>) (x => x.FirstDateOfMonth.Month == monthNum))?.ExcludedDays;
  }

  /// <summary>Выделение интервала времени.</summary>
  /// <param name="dateBeg">Начальная дата</param>
  /// <param name="dateEnd">Конечная дата</param>
  /// <param name="checkCount"></param>
  public void SetSelection(DateTime dateBeg, DateTime dateEnd, bool checkCount)
  {
    bool flag = false;
    if (checkCount)
    {
      if (this.MaxSelectionCount < 1 || Math.Abs(dateBeg.Subtract(this.SelectionEnd).TotalDays) < (double) this.MaxSelectionCount)
      {
        this.SelectionBegin = new DateTime(dateBeg.Year, dateBeg.Month, dateBeg.Day);
        flag = true;
      }
      if (this.MaxSelectionCount < 1 || Math.Abs(dateEnd.Subtract(this.SelectionBegin).TotalDays) < (double) this.MaxSelectionCount)
      {
        this.SelectionEnd = dateEnd.Add(new TimeSpan(23, 59, 59));
        flag = true;
      }
    }
    else
    {
      this.SelectionBegin = new DateTime(dateBeg.Year, dateBeg.Month, dateBeg.Day);
      this.SelectionEnd = dateEnd.Add(new TimeSpan(23, 59, 59));
      flag = true;
    }
    if (!flag)
      return;
    this.Invalidate();
    this.OnSelectionChanged(EventArgs.Empty);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
    this.SuspendLayout();
    this.ResumeLayout(false);
  }
}
