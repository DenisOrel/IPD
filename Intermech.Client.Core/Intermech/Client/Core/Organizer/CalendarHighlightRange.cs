
// Type: Intermech.Client.Core.Organizer.CalendarHighlightRange
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// Represents a range of time that is highlighted as work-time
/// </summary>
public class CalendarHighlightRange
{
  private Scheduler _calendar;
  private DayOfWeek _dayOfWeek;
  private TimeSpan _startTime;
  private TimeSpan _endTime;

  /// <summary>Creates a new empty range</summary>
  public CalendarHighlightRange()
  {
  }

  /// <summary>Creates a new range with the specified information</summary>
  /// <param name="day"></param>
  /// <param name="startTime"></param>
  /// <param name="endTime"></param>
  public CalendarHighlightRange(DayOfWeek day, TimeSpan startTime, TimeSpan endTime)
    : this()
  {
    this._dayOfWeek = day;
    this._startTime = startTime;
    this._endTime = endTime;
  }

  /// <summary>
  /// Gets the calendar that this range is assigned to. (If any)
  /// </summary>
  public Scheduler Calendar => this._calendar;

  /// <summary>Gets or sets the day of the week for this range</summary>
  public DayOfWeek DayOfWeek
  {
    get => this._dayOfWeek;
    set
    {
      this._dayOfWeek = value;
      this.Update();
    }
  }

  /// <summary>Gets or sets the start time of the range</summary>
  public TimeSpan StartTime
  {
    get => this._startTime;
    set
    {
      this._startTime = value;
      this.Update();
    }
  }

  /// <summary>Gets or sets the end time of the range</summary>
  public TimeSpan EndTime
  {
    get => this._endTime;
    set
    {
      this._endTime = value;
      this.Update();
    }
  }

  /// <summary>Tells the calendar to update the highligts</summary>
  private void Update()
  {
    if (this.Calendar == null)
      return;
    this.Calendar.UpdateHighlights();
  }

  /// <summary>
  /// Sets the value of the <see cref="P:Intermech.Client.Core.Organizer.CalendarHighlightRange.Calendar" /> property
  /// </summary>
  /// <param name="calendar">Calendar that this range belongs to</param>
  internal void SetCalendar(Scheduler calendar) => this._calendar = calendar;
}
