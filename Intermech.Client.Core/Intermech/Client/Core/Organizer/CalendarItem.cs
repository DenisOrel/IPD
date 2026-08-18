
// Type: Intermech.Client.Core.Organizer.CalendarItem
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


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// Represents an item of the calendar with a date and timespan
/// </summary>
/// <remarks>
/// <para>CalendarItem provides a graphical representation of tasks within a date range.</para>
/// </remarks>
public class CalendarItem : SchedulerSelectableElement
{
  private long _objID;
  private string _caption = string.Empty;
  private Rectangle[] _additionalBounds;
  private Color _borderColor = Color.Empty;
  private Color _backColor = Color.Empty;
  private Color _backColorLighter = Color.Empty;
  private Color _foreColor = Color.Empty;
  private Color _patternColor = Color.Empty;
  private DateTime _startDate;
  private DateTime _finishDate;
  private TimeSpan _duration;
  private Image _img;
  /// <summary>
  /// Флаг введен для того. чтобы различать элементы, которые создавались в соотвествие с объектами IPS,
  /// от элементов, которые создавались как повторения базового элемента.
  /// У таких элементов нет соответствующих им объектов IPS.
  /// </summary>
  private bool _baseItem = true;
  private bool _readOnly;
  private bool _locked;
  private bool _isDragging;
  private bool _isEditing;
  private bool _isResizingStartDate;
  private bool _isResizingFinishDate;
  private bool _isOnView;
  private int _minuteStartTop;
  private int _minuteEndTop;
  private HatchStyle _pattern;
  private List<SchedulerTimeScaleUnit> _unitsPassing = new List<SchedulerTimeScaleUnit>();
  private List<CalendarDayTop> _topsPassing = new List<CalendarDayTop>();
  private object _tag;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="r1"></param>
  /// <param name="r2"></param>
  /// <returns></returns>
  private static int CompareBounds(Rectangle r1, Rectangle r2) => r1.Top.CompareTo(r2.Top);

  /// <summary>
  /// Gets or sets an array of rectangles containing bounds additional to property.
  /// </summary>
  /// <remarks>
  /// Items may contain additional bounds because of several graphical occourences, mostly when Calendar in
  /// <see cref="F:Intermech.Client.Core.Organizer.CalendarDaysMode.Short" /> mode, due to the duration of the item; e.g. when an all day item lasts several weeks,
  /// one rectangle for week must be drawn to indicate the presence of the item.
  /// </remarks>
  public virtual Rectangle[] AditionalBounds
  {
    get => this._additionalBounds;
    set => this._additionalBounds = value;
  }

  /// <summary>Возможность пользователя изменять данные элемента.</summary>
  [DefaultValue(false)]
  [CustomDescription("Attribute.Client.Core_240")]
  public bool ReadOnly
  {
    get => this._readOnly;
    set => this._readOnly = !this._baseItem || value;
  }

  /// <summary>
  /// Gets or sets the a background color for the object. If Color.Empty, renderer default's will be used.
  /// </summary>
  public Color BackgroundColor
  {
    get => this._backColor;
    set => this._backColor = value;
  }

  /// <summary>
  /// Gets or sets the lighter background color of the item.
  /// </summary>
  public Color BackgroundColorLighter
  {
    get => this._backColorLighter;
    set => this._backColorLighter = value;
  }

  /// <summary>Базовый элемент.</summary>
  public bool BaseItem => this._baseItem;

  /// <summary>
  /// Gets or sets the bordercolor of the item. If Color.Empty, renderer default's will be used.
  /// </summary>
  public Color BorderColor
  {
    get => this._borderColor;
    set => this._borderColor = value;
  }

  /// <summary>Наименование элемента планировщика.</summary>
  public virtual string Caption
  {
    get => this._caption;
    set => this._caption = value;
  }

  /// <summary>Gets the StartDate of the item. Implemented.</summary>
  public override DateTime Date => this.StartDate;

  /// <summary>Gets the day on the Calendar where this item ends.</summary>
  /// <remarks>
  /// This day is not necesarily the day corresponding to the day on <see cref="P:Intermech.Client.Core.Organizer.CalendarItem.EndDate" />,
  /// since this date can be out of the range of the current view.
  /// <para>If Item is not on view date range this property will return null.</para>
  /// </remarks>
  public CalendarDay DayEnd
  {
    get
    {
      if (!this.IsOnViewDateRange)
        return (CalendarDay) null;
      return !this.IsOpenEnd ? this.Scheduler.FindDay(this.EndDate) : this.Scheduler.Days[this.Scheduler.Days.Length - 1];
    }
  }

  /// <summary>
  /// Gets the day on the <see cref="T:Intermech.Client.Core.Organizer.Scheduler" /> where this item starts.
  /// </summary>
  /// <remarks>
  /// This day is not necesarily the day corresponding to the day on <see cref="P:Intermech.Client.Core.Organizer.CalendarItem.StartDate" />,
  /// since start date can be out of the range of the current view.
  /// <para>If Item is not on view date range this property will return null.</para>
  /// </remarks>
  public CalendarDay DayStart
  {
    get
    {
      if (!this.IsOnViewDateRange)
        return (CalendarDay) null;
      return !this.IsOpenStart ? this.Scheduler.FindDay(this.StartDate) : this.Scheduler.Days[0];
    }
  }

  /// <summary>Gets the duration of the item.</summary>
  public TimeSpan Duration
  {
    get
    {
      if (this._duration.TotalMinutes == 0.0)
        this._duration = this.EndDate.Subtract(this.StartDate);
      return this._duration;
    }
  }

  /// <summary>Gets or sets the end time of the item.</summary>
  public DateTime EndDate
  {
    get => this._finishDate;
    set
    {
      this._finishDate = value;
      this._duration = new TimeSpan(0, 0, 0);
      this.ClearPassings();
    }
  }

  /// <summary>Gets the text of the end date.</summary>
  public virtual string EndDateText
  {
    get
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      DateTime endDate;
      if (this.IsOpenEnd)
      {
        endDate = this.EndDate;
        empty1 = endDate.ToString(this.Scheduler.ItemsDateFormat);
      }
      if (this.ShowEndTime)
      {
        endDate = this.EndDate;
        if (!endDate.TimeOfDay.Equals(new TimeSpan(23, 59, 59)))
        {
          endDate = this.EndDate;
          empty2 = endDate.ToString(this.Scheduler.ItemsTimeFormat);
        }
      }
      return $"{empty1} {empty2}".Trim();
    }
  }

  /// <summary>
  /// Gets or sets the forecolor of the item. If Color.Empty, renderer default's will be used.
  /// </summary>
  public Color ForeColor
  {
    get => this._foreColor;
    set => this._foreColor = value;
  }

  /// <summary>Gets or sets an image for the item.</summary>
  public Image Image
  {
    get => this._img;
    set => this._img = value;
  }

  /// <summary>Gets a value indicating if the item is being dragged.</summary>
  public bool IsDragging
  {
    get => this._isDragging;
    internal set => this._isDragging = value;
  }

  /// <summary>
  /// Gets a value indicating if the item is currently being edited by the user.
  /// </summary>
  public bool IsEditing
  {
    get => this._isEditing;
    internal set => this._isEditing = value;
  }

  /// <summary>
  /// Gets a value indicating if the item goes on the DayTop area of the <see cref="T:Intermech.Client.Core.Organizer.CalendarDay" />.
  /// </summary>
  public bool IsOnDayTop
  {
    get
    {
      DateTime dateTime = this.StartDate;
      int day1 = dateTime.Day;
      dateTime = this.EndDate;
      dateTime = dateTime.AddSeconds(1.0);
      int day2 = dateTime.Day;
      return day1 != day2;
    }
  }

  /// <summary>
  /// Gets a value indicating if the item is currently on view.
  /// </summary>
  /// <remarks>The item may not be on view because of scrolling</remarks>
  public bool IsOnView
  {
    get => this._isOnView;
    internal set => this._isOnView = value;
  }

  /// <summary>
  /// Gets a value indicating if the item is on the range specified by <see cref="P:Intermech.Client.Core.Organizer.Scheduler.ViewStart" /> and <see cref="P:Intermech.Client.Core.Organizer.Scheduler.ViewEnd" />.
  /// </summary>
  public bool IsOnViewDateRange
  {
    get
    {
      DateTime date = this.Scheduler.Days[0].Date;
      DateTime dateTime1 = this.Scheduler.Days[this.Scheduler.Days.Length - 1].Date.Add(new TimeSpan(23, 59, 59));
      DateTime startDate = this.StartDate;
      DateTime endDate = this.EndDate;
      DateTime dateTime2 = dateTime1;
      return startDate < dateTime2 && date < endDate;
    }
  }

  /// <summary>
  /// Gets a value indicating if the item's <see cref="P:Intermech.Client.Core.Organizer.CalendarItem.StartDate" /> is before the <see cref="P:Intermech.Client.Core.Organizer.Scheduler.ViewStart" /> date.
  /// </summary>
  public bool IsOpenStart => this.StartDate.CompareTo(this.Scheduler.Days[0].Date) < 0;

  /// <summary>
  /// Gets a value indicating if the item's <see cref="P:Intermech.Client.Core.Organizer.CalendarItem.EndDate" /> is aftter the <see cref="P:Intermech.Client.Core.Organizer.Scheduler.ViewEnd" /> date.
  /// </summary>
  public bool IsOpenEnd
  {
    get
    {
      return this.EndDate.CompareTo(this.Scheduler.Days[this.Scheduler.Days.Length - 1].Date.Add(new TimeSpan(23, 59, 59))) > 0;
    }
  }

  /// <summary>
  /// Gets a value indicating if item is being resized by the <see cref="P:Intermech.Client.Core.Organizer.CalendarItem.EndDate" />.
  /// </summary>
  public bool IsResizingEndDate
  {
    get => this._isResizingFinishDate;
    internal set => this._isResizingFinishDate = value;
  }

  /// <summary>
  /// Gets a value indicating if item is being resized by the <see cref="P:Intermech.Client.Core.Organizer.CalendarItem.StartDate" />.
  /// </summary>
  public bool IsResizingStartDate
  {
    get => this._isResizingStartDate;
    internal set => this._isResizingStartDate = value;
  }

  /// <summary>Gets a value indicating if this item is locked.</summary>
  /// <remarks>
  /// When an item is locked, the user can't drag it or change it's text
  /// </remarks>
  public bool Locked
  {
    get => this._locked;
    set => this._locked = value;
  }

  /// <summary>Gets the top correspoinding to the ending minute.</summary>
  public int MinuteEndTop
  {
    get => this._minuteEndTop;
    internal set => this._minuteEndTop = value;
  }

  /// <summary>Gets the top corresponding to the starting minute.</summary>
  public int MinuteStartTop
  {
    get => this._minuteStartTop;
    internal set => this._minuteStartTop = value;
  }

  /// <summary>
  /// Идентификатор объекта, по данным которого создается элемент планировщика.
  /// </summary>
  public long ObjectID => this._objID;

  /// <summary>
  /// Gets or sets the pattern style to use in the background of item.
  /// </summary>
  public HatchStyle Pattern
  {
    get => this._pattern;
    set => this._pattern = value;
  }

  /// <summary>Gets or sets the pattern's color.</summary>
  public Color PatternColor
  {
    get => this._patternColor;
    set => this._patternColor = value;
  }

  /// <summary>
  /// Gets a value indicating if the item should show the time of the <see cref="P:Intermech.Client.Core.Organizer.CalendarItem.StartDate" />.
  /// </summary>
  public bool ShowStartTime
  {
    get
    {
      if (this.IsOpenStart)
        return true;
      return (this.IsOnDayTop || this.Scheduler.DaysMode == CalendarDaysMode.Short) && !this.StartDate.TimeOfDay.Equals(new TimeSpan(0, 0, 0));
    }
  }

  /// <summary>
  /// Gets a value indicating if the item should show the time of the <see cref="P:Intermech.Client.Core.Organizer.CalendarItem.EndDate" />.
  /// </summary>
  public virtual bool ShowEndTime
  {
    get
    {
      if (!this.IsOpenEnd && (!this.IsOnDayTop && this.Scheduler.DaysMode != CalendarDaysMode.Short || this.EndDate.TimeOfDay.Equals(new TimeSpan(23, 59, 59))))
        return false;
      return this.Scheduler.DaysMode != CalendarDaysMode.Short || !(this.StartDate.Date == this.EndDate.Date);
    }
  }

  /// <summary>Gets the text of the start date.</summary>
  public virtual string StartDateText
  {
    get
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      DateTime startDate;
      if (this.IsOpenStart)
      {
        startDate = this.StartDate;
        empty1 = startDate.ToString(this.Scheduler.ItemsDateFormat);
      }
      if (this.ShowStartTime)
      {
        startDate = this.StartDate;
        if (!startDate.TimeOfDay.Equals(new TimeSpan(0, 0, 0)))
        {
          startDate = this.StartDate;
          empty2 = startDate.ToString(this.Scheduler.ItemsTimeFormat);
        }
      }
      return $"{empty1} {empty2}".Trim();
    }
  }

  /// <summary>Gets or sets the start time of the item.</summary>
  public virtual DateTime StartDate
  {
    get => this._startDate;
    set
    {
      this._startDate = value;
      this._duration = new TimeSpan(0, 0, 0);
      this.ClearPassings();
    }
  }

  /// <summary>Gets or sets a tag object for the item.</summary>
  public object Tag
  {
    get => this._tag;
    set => this._tag = value;
  }

  /// <summary>Gets the list of DayTops that this item passes thru.</summary>
  internal List<CalendarDayTop> TopsPassing => this._topsPassing;

  /// <summary>Gets or sets the units that this item passes by.</summary>
  internal List<SchedulerTimeScaleUnit> UnitsPassing
  {
    get => this._unitsPassing;
    set => this._unitsPassing = value;
  }

  /// <summary>Конструктор.</summary>
  /// <param name="scheduler">Планировщик заданий</param>
  /// <param name="objID">Идентификатор объекта, по данным которого создается элемент планировщика</param>
  /// <param name="baseItem">Признак базового элемента</param>
  public CalendarItem(Scheduler scheduler, long objID, bool baseItem)
    : base(scheduler)
  {
    this._objID = objID;
    this._baseItem = baseItem;
    this.ReadOnly = !baseItem;
  }

  /// <summary>Конструктор.</summary>
  /// <param name="scheduler">Планировщик заданий</param>
  /// <param name="objID">Идентификатор объекта, по данным которого создается элемент планировщика</param>
  /// <param name="startDate">Дата начала</param>
  /// <param name="finishDate">Дата окончания</param>
  /// <param name="caption">Наименование</param>
  /// <param name="baseItem">Признак базового элемента</param>
  public CalendarItem(
    Scheduler scheduler,
    long objID,
    DateTime startDate,
    DateTime finishDate,
    string caption,
    bool baseItem)
    : this(scheduler, objID, baseItem)
  {
    this.StartDate = startDate;
    this.EndDate = finishDate;
    this.Caption = caption;
  }

  /// <summary>Конструктор.</summary>
  /// <param name="scheduler">Планировщик заданий</param>
  /// <param name="objID">Идентификатор объекта, по данным которого создается элемент планировщика</param>
  /// <param name="startDate">Дата начала</param>
  /// <param name="duration">Интервал временни</param>
  /// <param name="caption">Наименование</param>
  /// <param name="baseItem">Признак базового элемента</param>
  public CalendarItem(
    Scheduler scheduler,
    long objID,
    DateTime startDate,
    TimeSpan duration,
    string caption,
    bool baseItem)
    : this(scheduler, objID, startDate, startDate.Add(duration), caption, baseItem)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override string ToString()
  {
    DateTime dateTime = this.StartDate;
    string shortTimeString1 = dateTime.ToShortTimeString();
    dateTime = this.EndDate;
    string shortTimeString2 = dateTime.ToShortTimeString();
    return $"{shortTimeString1} - {shortTimeString2}";
  }

  /// <summary>Adds bounds for the item.</summary>
  /// <param name="r"></param>
  internal void AddBounds(Rectangle r)
  {
    if (r.IsEmpty)
      throw new ArgumentException("r can't be empty");
    if (this.Bounds.IsEmpty)
      this.Bounds = r;
    else
      this.AditionalBounds = new List<Rectangle>(this.AditionalBounds == null ? (IEnumerable<Rectangle>) new Rectangle[0] : (IEnumerable<Rectangle>) this.AditionalBounds)
      {
        r
      }.ToArray();
  }

  /// <summary>
  /// Adds the specified <see cref="T:Intermech.Client.Core.Organizer.CalendarDayTop" /> as a passing one.
  /// </summary>
  /// <param name="top"></param>
  internal void AddTopPassing(CalendarDayTop top)
  {
    if (this.TopsPassing.Contains(top))
      return;
    this.TopsPassing.Add(top);
  }

  /// <summary>Adds the specified unit as a passing unit.</summary>
  /// <param name="calendarTimeScaleUnit"></param>
  internal void AddUnitPassing(SchedulerTimeScaleUnit calendarTimeScaleUnit)
  {
    if (this.UnitsPassing.Contains(calendarTimeScaleUnit))
      return;
    this.UnitsPassing.Add(calendarTimeScaleUnit);
  }

  /// <summary>Clears all bounds of the item.</summary>
  internal void ClearBounds()
  {
    this.Bounds = Rectangle.Empty;
    this.AditionalBounds = new Rectangle[0];
    this._minuteStartTop = 0;
    this._minuteEndTop = 0;
  }

  /// <summary>
  /// Clears the item's existance off passing units and tops.
  /// </summary>
  internal void ClearPassings()
  {
    foreach (SchedulerTimeScaleUnit schedulerTimeScaleUnit in this.UnitsPassing)
      schedulerTimeScaleUnit.ClearItemExistance(this);
    this.UnitsPassing.Clear();
    this.TopsPassing.Clear();
  }

  /// <summary>
  /// It pushes the left and the right to the center of the item to visually indicate start and end time.
  /// </summary>
  internal void FirstAndLastRectangleGapping()
  {
    if (!this.IsOpenStart)
      this.Bounds = Rectangle.FromLTRB(this.Bounds.Left + this.Scheduler.Renderer.ItemsPadding, this.Bounds.Top, this.Bounds.Right, this.Bounds.Bottom);
    if (this.IsOpenEnd)
      return;
    if (this.AditionalBounds != null && this.AditionalBounds.Length != 0)
    {
      Rectangle aditionalBound = this.AditionalBounds[this.AditionalBounds.Length - 1];
      this.AditionalBounds[this.AditionalBounds.Length - 1] = Rectangle.FromLTRB(aditionalBound.Left, aditionalBound.Top, aditionalBound.Right - this.Scheduler.Renderer.ItemsPadding, aditionalBound.Bottom);
    }
    else
    {
      Rectangle bounds = this.Bounds;
      this.Bounds = Rectangle.FromLTRB(bounds.Left, bounds.Top, bounds.Right - this.Scheduler.Renderer.ItemsPadding, bounds.Bottom);
    }
  }

  /// <summary>
  /// Applies color to background, border, and forecolor, from the specified color.
  /// </summary>
  /// <param name="color"></param>
  public void ApplyColor(Color color)
  {
    this.BackgroundColor = color;
    this.BackgroundColorLighter = Color.FromArgb((int) color.R + ((int) byte.MaxValue - (int) color.R) / 2 + ((int) byte.MaxValue - (int) color.R) / 3, (int) color.G + ((int) byte.MaxValue - (int) color.G) / 2 + ((int) byte.MaxValue - (int) color.G) / 3, (int) color.B + ((int) byte.MaxValue - (int) color.B) / 2 + ((int) byte.MaxValue - (int) color.B) / 3);
    this.BorderColor = Color.FromArgb(Convert.ToInt32(Convert.ToSingle(color.R) * 0.8f), Convert.ToInt32(Convert.ToSingle(color.G) * 0.8f), Convert.ToInt32(Convert.ToSingle(color.B) * 0.8f));
    this.ForeColor = ((int) color.R + (int) color.G + (int) color.B) / 3 > (int) sbyte.MaxValue ? Color.Black : Color.White;
  }

  /// <summary>Gets all the bounds related to the item.</summary>
  /// <remarks>
  ///  Items that are broken on two or more weeks may have more than one rectangle bounds.
  /// </remarks>
  /// <returns></returns>
  public IEnumerable<Rectangle> GetAllBounds()
  {
    List<Rectangle> allBounds = new List<Rectangle>(this.AditionalBounds == null ? (IEnumerable<Rectangle>) new Rectangle[0] : (IEnumerable<Rectangle>) this.AditionalBounds);
    allBounds.Add(this.Bounds);
    allBounds.Sort(new Comparison<Rectangle>(CalendarItem.CompareBounds));
    return (IEnumerable<Rectangle>) allBounds;
  }

  /// <summary>
  /// Indicates if the time of the item intersects with the provided time.
  /// </summary>
  /// <param name="timeStart"></param>
  /// <param name="timeEnd"></param>
  /// <returns></returns>
  public bool IntersectsWith(TimeSpan timeStart, TimeSpan timeEnd)
  {
    TimeSpan timeOfDay = this.StartDate.TimeOfDay;
    int int32_1 = Convert.ToInt32(timeOfDay.TotalMinutes);
    timeOfDay = this.EndDate.TimeOfDay;
    int int32_2 = Convert.ToInt32(timeOfDay.TotalMinutes);
    return Rectangle.FromLTRB(0, int32_1, 5, int32_2).IntersectsWith(Rectangle.FromLTRB(0, Convert.ToInt32(timeStart.TotalMinutes), 5, Convert.ToInt32(timeEnd.TotalMinutes - 1.0)));
  }

  /// <summary>Removes all specific coloring for the item.</summary>
  public void RemoveColors()
  {
    this.BackgroundColor = Color.Empty;
    this.ForeColor = Color.Empty;
    this.BorderColor = Color.Empty;
  }

  /// <summary>
  /// Gets a value indicating if the specified point is in a resize zone of <see cref="P:Intermech.Client.Core.Organizer.CalendarItem.EndDate" />.
  /// </summary>
  /// <param name="p"></param>
  /// <returns></returns>
  public bool ResizeEndDateZone(Point p)
  {
    int num = 4;
    List<Rectangle> rectangleList = new List<Rectangle>(this.GetAllBounds());
    Rectangle rectangle1 = rectangleList[0];
    Rectangle rectangle2 = rectangleList[rectangleList.Count - 1];
    return this.IsOnDayTop || this.Scheduler.DaysMode == CalendarDaysMode.Short ? Rectangle.FromLTRB(rectangle2.Right - num, rectangle2.Top, rectangle2.Right, rectangle2.Bottom).Contains(p) : Rectangle.FromLTRB(rectangle2.Left, rectangle2.Bottom - num, rectangle2.Right, rectangle2.Bottom).Contains(p);
  }

  /// <summary>
  /// Gets a value indicating if the specified point is in a resize zone of <see cref="P:Intermech.Client.Core.Organizer.CalendarItem.StartDate" />.
  /// </summary>
  /// <param name="p"></param>
  /// <returns></returns>
  public bool ResizeStartDateZone(Point p)
  {
    int num = 4;
    List<Rectangle> rectangleList = new List<Rectangle>(this.GetAllBounds());
    Rectangle rectangle1 = rectangleList[0];
    Rectangle rectangle2 = rectangleList[rectangleList.Count - 1];
    return this.IsOnDayTop || this.Scheduler.DaysMode == CalendarDaysMode.Short ? Rectangle.FromLTRB(rectangle1.Left, rectangle1.Top, rectangle1.Left + num, rectangle1.Bottom).Contains(p) : Rectangle.FromLTRB(rectangle1.Left, rectangle1.Top, rectangle1.Right, rectangle1.Top + num).Contains(p);
  }
}
