
// Type: Intermech.Client.Core.Organizer.CalendarDayTop
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// Represents the top area of a day, where multiday and all day items are stored
/// </summary>
public class CalendarDayTop : SchedulerSelectableElement
{
  private CalendarDay _day;
  private List<CalendarItem> _passingItems;

  /// <summary>Creates a new DayTop for the specified day</summary>
  /// <param name="day"></param>
  public CalendarDayTop(CalendarDay day)
    : base(day.Scheduler)
  {
    this._day = day;
    this._passingItems = new List<CalendarItem>();
  }

  public override DateTime Date
  {
    get
    {
      DateTime date = this.Day.Date;
      int year = date.Year;
      date = this.Day.Date;
      int month = date.Month;
      date = this.Day.Date;
      int day = date.Day;
      return new DateTime(year, month, day);
    }
  }

  /// <summary>Gets the Day of this DayTop</summary>
  public CalendarDay Day => this._day;

  /// <summary>Gets the list of items passing on this daytop</summary>
  public List<CalendarItem> PassingItems => this._passingItems;

  internal void AddPassingItem(CalendarItem item)
  {
    if (this.PassingItems.Contains(item))
      return;
    this.PassingItems.Add(item);
  }
}
