
// Type: Intermech.Client.Core.Organizer.SchedulerTimeScaleUnit
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// Represents a selectable timescale unit on a <see cref="T:Intermech.Client.Core.Organizer.CalendarDay" />
/// </summary>
public class SchedulerTimeScaleUnit : SchedulerSelectableElement
{
  private DateTime _date = DateTime.MinValue;
  private CalendarDay _day;
  private List<CalendarItem> _passingItems;
  private int _hours;
  private int _minutes;
  private int _index;
  private bool _highlighted;
  private bool _visible;

  /// <summary>Gets the exact date when the unit starts</summary>
  public override DateTime Date
  {
    get
    {
      if (this._date.Equals(DateTime.MinValue))
      {
        DateTime date = this.Day.Date;
        int year = date.Year;
        date = this.Day.Date;
        int month = date.Month;
        date = this.Day.Date;
        int day = date.Day;
        int hours = this.Hours;
        int minutes = this.Minutes;
        this._date = new DateTime(year, month, day, hours, minutes, 0);
      }
      return this._date;
    }
  }

  /// <summary>
  /// Gets the <see cref="T:Intermech.Client.Core.Organizer.CalendarDay" /> this unit belongs to
  /// </summary>
  public CalendarDay Day => this._day;

  /// <summary>Gets the duration of the unit.</summary>
  public TimeSpan Duration => new TimeSpan(0, (int) this.Scheduler.TimeScale, 0);

  /// <summary>
  /// Gets if the unit is highlighted because it fits in some of the calendar's highlight ranges
  /// </summary>
  public bool Highlighted
  {
    get => this._highlighted;
    set => this._highlighted = value;
  }

  /// <summary>Gets the hour when this unit starts</summary>
  public int Hours => this._hours;

  /// <summary>Gets the index of the unit relative to the day</summary>
  public int Index => this._index;

  /// <summary>Gets the minute when this unit starts</summary>
  public int Minutes
  {
    get => this._minutes;
    set => this._minutes = value;
  }

  /// <summary>
  /// Gets or sets the amount of items that pass over the unit
  /// </summary>
  internal List<CalendarItem> PassingItems
  {
    get => this._passingItems;
    set => this._passingItems = value;
  }

  /// <summary>
  /// Gets a value indicating if the unit is currently visible on viewport
  /// </summary>
  public bool Visible
  {
    get => this._visible;
    set => this._visible = value;
  }

  /// <summary>Конструктор.</summary>
  /// <param name="day"><see cref="T:Intermech.Client.Core.Organizer.CalendarDay" /> this unit belongs to</param>
  /// <param name="index">Index of the unit relative to the container day</param>
  /// <param name="hours">Hour of the unit</param>
  /// <param name="minutes">Minutes of the unit</param>
  internal SchedulerTimeScaleUnit(CalendarDay day, int index, int hours, int minutes)
    : base(day.Scheduler)
  {
    this._day = day;
    this._index = index;
    this._hours = hours;
    this._minutes = minutes;
    this._passingItems = new List<CalendarItem>();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="item"></param>
  internal void AddPassingItem(CalendarItem item)
  {
    if (this.PassingItems.Contains(item))
      return;
    this.PassingItems.Add(item);
    this.Day.AddContainedItem(item);
  }

  /// <summary>
  /// Gets a value indicating if the unit should be higlighted
  /// </summary>
  /// <returns></returns>
  internal bool CheckHighlighted()
  {
    for (int index = 0; index < this.Day.Scheduler.HighlightRanges.Length; ++index)
    {
      CalendarHighlightRange highlightRange = this.Day.Scheduler.HighlightRanges[index];
      if (highlightRange.DayOfWeek == this.Date.DayOfWeek && this.Date.TimeOfDay.CompareTo(highlightRange.StartTime) >= 0 && this.Date.TimeOfDay.CompareTo(highlightRange.EndTime) < 0)
        return true;
    }
    return false;
  }

  /// <summary>
  /// Clears existance of item from this unit and it's corresponding day.
  /// </summary>
  /// <param name="item"></param>
  internal void ClearItemExistance(CalendarItem item)
  {
    if (this.PassingItems.Contains(item))
      this.PassingItems.Remove(item);
    if (!this.Day.ContainedItems.Contains(item))
      return;
    this.Day.ContainedItems.Remove(item);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override string ToString() => $"[{this.Index}] - {this.Date.ToShortTimeString()}";
}
