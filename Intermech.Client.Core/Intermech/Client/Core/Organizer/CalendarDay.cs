
// Type: Intermech.Client.Core.Organizer.CalendarDay
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// Represents a day present on the <see cref="T:Intermech.Client.Core.Organizer.Scheduler" /> control's view.
/// </summary>
public class CalendarDay : SchedulerSelectableElement
{
  private Size overflowSize = new Size(16 /*0x10*/, 16 /*0x10*/);
  private Padding overflowPadding = new Padding(5);
  private List<CalendarItem> _containedItems;
  private SchedulerTimeScaleUnit[] _timeUnits;
  private Scheduler _calendar;
  private DateTime _date;
  private CalendarDayTop _dayTop;
  private int _index;
  private bool _overflowStart;
  private bool _overflowEnd;
  private bool _overflowStartSelected;
  private bool _overlowEndSelected;

  /// <summary>Creates a new Day.</summary>
  /// <param name="calendar">Calendar this day belongs to</param>
  /// <param name="date">Date of the day</param>
  /// <param name="index">Index of the day on the current calendar's view</param>
  internal CalendarDay(Scheduler calendar, DateTime date, int index)
    : base(calendar)
  {
    this._containedItems = new List<CalendarItem>();
    this._calendar = calendar;
    this._dayTop = new CalendarDayTop(this);
    this._date = date;
    this._index = index;
    this.UpdateUnits();
  }

  /// <summary>
  /// Gets the bounds of the body of the day (where time-based CalendarItems are placed).
  /// </summary>
  public Rectangle BodyBounds
  {
    get
    {
      Rectangle bounds = this.Bounds;
      int left = bounds.Left;
      bounds = this.DayTop.Bounds;
      int bottom1 = bounds.Bottom;
      bounds = this.Bounds;
      int right = bounds.Right;
      bounds = this.Bounds;
      int bottom2 = bounds.Bottom;
      return Rectangle.FromLTRB(left, bottom1, right, bottom2);
    }
  }

  /// <summary>Gets a list of items contained on the day.</summary>
  internal List<CalendarItem> ContainedItems => this._containedItems;

  /// <summary>Gets the date this day represents.</summary>
  public override DateTime Date => this._date;

  /// <summary>
  /// Gets the DayTop of the day, the place where multi-day and all-day items are placed.
  /// </summary>
  public CalendarDayTop DayTop => this._dayTop;

  /// <summary>Gets the bounds of the header of the day.</summary>
  public Rectangle HeaderBounds
  {
    get
    {
      Rectangle bounds = this.Bounds;
      int left = bounds.Left;
      bounds = this.Bounds;
      int top = bounds.Top;
      bounds = this.Bounds;
      int width = bounds.Width;
      int dayHeaderHeight = this.Scheduler.Renderer.DayHeaderHeight;
      return new Rectangle(left, top, width, dayHeaderHeight);
    }
  }

  /// <summary>Gets the index of this day on the calendar.</summary>
  public int Index => this._index;

  /// <summary>
  /// Gets a value indicating if the day contains items not shown through the end of the day.
  /// </summary>
  public bool OverflowEnd => this._overflowEnd;

  /// <summary>
  /// Gets the bounds of the <see cref="P:Intermech.Client.Core.Organizer.CalendarDay.OverflowEnd" /> indicator.
  /// </summary>
  public virtual Rectangle OverflowEndBounds
  {
    get
    {
      Rectangle bounds = this.Bounds;
      int x = bounds.Right - this.overflowPadding.Right - this.overflowSize.Width;
      bounds = this.Bounds;
      int y = bounds.Bottom - this.overflowPadding.Bottom - this.overflowSize.Height;
      return new Rectangle(new Point(x, y), this.overflowSize);
    }
  }

  /// <summary>
  /// Gets a value indicating if the <see cref="P:Intermech.Client.Core.Organizer.CalendarDay.OverflowEnd" /> indicator is currently selected.
  /// </summary>
  /// <remarks>
  /// This value set to <c>true</c> when user hovers the mouse on the <see cref="P:Intermech.Client.Core.Organizer.CalendarDay.OverflowStartBounds" /> area.
  /// </remarks>
  public bool OverflowEndSelected => this._overlowEndSelected;

  /// <summary>
  /// Gets a value indicating if the day contains items not shown through the start of the day.
  /// </summary>
  public bool OverflowStart => this._overflowStart;

  /// <summary>
  /// Gets the bounds of the <see cref="P:Intermech.Client.Core.Organizer.CalendarDay.OverflowStart" /> indicator.
  /// </summary>
  public virtual Rectangle OverflowStartBounds
  {
    get
    {
      Rectangle bounds = this.Bounds;
      int x = bounds.Right - this.overflowPadding.Right - this.overflowSize.Width;
      bounds = this.Bounds;
      int y = bounds.Top + this.overflowPadding.Top;
      return new Rectangle(new Point(x, y), this.overflowSize);
    }
  }

  /// <summary>
  /// Gets a value indicating if the <see cref="P:Intermech.Client.Core.Organizer.CalendarDay.OverflowStart" /> indicator is currently selected.
  /// </summary>
  /// <remarks>
  /// This value set to <c>true</c> when user hovers the mouse on the <see cref="P:Intermech.Client.Core.Organizer.CalendarDay.OverflowStartBounds" /> area.
  /// </remarks>
  public bool OverflowStartSelected => this._overflowStartSelected;

  /// <summary>
  /// Gets a value indicating if the day is specified on the view (See remarks).
  /// </summary>
  /// <remarks>
  /// A day may not be specified on the view, but still present to make up a square calendar.
  /// This days should be drawn in a way that indicates it's necessary but unrequested presence.
  /// </remarks>
  public bool SpecifiedOnView
  {
    get
    {
      return this.Date.CompareTo(this.Scheduler.ViewStart) >= 0 && this.Date.CompareTo(this.Scheduler.ViewEnd) <= 0;
    }
  }

  /// <summary>Gets the time units contained on the day.</summary>
  public SchedulerTimeScaleUnit[] TimeUnits => this._timeUnits;

  /// <summary>
  /// Adds an item to the <see cref="P:Intermech.Client.Core.Organizer.CalendarDay.ContainedItems" /> list if not in yet.
  /// </summary>
  /// <param name="item"></param>
  internal void AddContainedItem(CalendarItem item)
  {
    if (this.ContainedItems.Contains(item))
      return;
    this.ContainedItems.Add(item);
  }

  /// <summary>
  /// 
  /// </summary>
  internal void ClearTimeUnits()
  {
    foreach (SchedulerTimeScaleUnit timeUnit in this._timeUnits)
      timeUnit.PassingItems.Clear();
  }

  /// <summary>
  /// Sets the value of he <see cref="P:Intermech.Client.Core.Organizer.CalendarDay.OverflowEnd" /> property.
  /// </summary>
  /// <param name="overflow">Value of the property</param>
  internal void SetOverflowEnd(bool overflow) => this._overflowEnd = overflow;

  /// <summary>
  /// Sets the value of the <see cref="P:Intermech.Client.Core.Organizer.CalendarDay.OverflowEndSelected" /> property.
  /// </summary>
  /// <param name="selected">Value to pass to the property</param>
  internal void SetOverflowEndSelected(bool selected) => this._overlowEndSelected = selected;

  /// <summary>
  /// Sets the value of he <see cref="P:Intermech.Client.Core.Organizer.CalendarDay.OverflowStart" /> property.
  /// </summary>
  /// <param name="overflow">Value of the property</param>
  internal void SetOverflowStart(bool overflow) => this._overflowStart = overflow;

  /// <summary>
  /// Sets the value of the <see cref="P:Intermech.Client.Core.Organizer.CalendarDay.OverflowStartSelected" /> property.
  /// </summary>
  /// <param name="selected">Value to pass to the property</param>
  internal void SetOverflowStartSelected(bool selected) => this._overflowStartSelected = selected;

  /// <summary>Updates the highlights of the units.</summary>
  internal void UpdateHighlights()
  {
    if (this.TimeUnits == null)
      return;
    for (int index = 0; index < this.TimeUnits.Length; ++index)
      this.TimeUnits[index].Highlighted = this.TimeUnits[index].CheckHighlighted();
  }

  /// <summary>
  /// Updates the value of <see cref="P:Intermech.Client.Core.Organizer.CalendarDay.TimeUnits" /> property.
  /// </summary>
  internal void UpdateUnits()
  {
    int num;
    switch (this.Scheduler.TimeScale)
    {
      case CalendarTimeScale.FiveMinutes:
        num = 12;
        break;
      case CalendarTimeScale.SixMinutes:
        num = 10;
        break;
      case CalendarTimeScale.TenMinutes:
        num = 6;
        break;
      case CalendarTimeScale.FifteenMinutes:
        num = 4;
        break;
      case CalendarTimeScale.ThirtyMinutes:
        num = 2;
        break;
      case CalendarTimeScale.SixtyMinutes:
        num = 1;
        break;
      default:
        throw new NotImplementedException("TimeScale not supported");
    }
    this._timeUnits = new SchedulerTimeScaleUnit[24 * num];
    int hours = 0;
    int minutes = 0;
    for (int index = 0; index < this._timeUnits.Length; ++index)
    {
      this._timeUnits[index] = new SchedulerTimeScaleUnit(this, index, hours, minutes);
      minutes += 60 / num;
      if (minutes >= 60)
      {
        minutes = 0;
        ++hours;
      }
    }
    this.UpdateHighlights();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override string ToString() => this.Date.ToShortDateString();
}
