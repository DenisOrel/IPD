
// Type: Intermech.Client.Core.Organizer.CalendarWeek
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing;
using System.Globalization;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// Represents a week displayed on the <see cref="P:Intermech.Client.Core.Organizer.CalendarWeek.Calendar" />
/// </summary>
public class CalendarWeek
{
  private Rectangle _bounds;
  private Scheduler _calendar;
  private DateTime _firstDay;

  /// <summary>Creates a new week for the specified calendar</summary>
  /// <param name="calendar">Calendar this week belongs to</param>
  /// <param name="firstDay">Start day of the week</param>
  internal CalendarWeek(Scheduler calendar, DateTime firstDay)
  {
    this._calendar = calendar;
    this._firstDay = firstDay;
  }

  /// <summary>Gets the bounds of the week</summary>
  public Rectangle Bounds => this._bounds;

  /// <summary>Gets the calendar this week belongs to</summary>
  public Scheduler Calendar => this._calendar;

  /// <summary>Gets the bounds of the week header</summary>
  public Rectangle HeaderBounds
  {
    get
    {
      Rectangle bounds = this.Bounds;
      int left = bounds.Left;
      bounds = this.Bounds;
      int y = bounds.Top + this.Calendar.Renderer.DayHeaderHeight;
      int weekHeaderWidth = this.Calendar.Renderer.WeekHeaderWidth;
      bounds = this.Bounds;
      int height = bounds.Height - this.Calendar.Renderer.DayHeaderHeight;
      return new Rectangle(left, y, weekHeaderWidth, height);
    }
  }

  /// <summary>Gets the sunday that starts the week</summary>
  public DateTime StartDate => this._firstDay;

  /// <summary>
  /// Gets the short version of week's string representation
  /// </summary>
  /// <returns></returns>
  public string ToStringShort()
  {
    DateTime dateTime = this.StartDate.AddDays(6.0);
    return dateTime.Month != this.StartDate.Month ? $"{this.StartDate.ToString("d/M")} - {dateTime.ToString("d/M")}" : $"{this.StartDate.Day} - {dateTime.ToString("d/M")}";
  }

  /// <summary>Gets the large version of string representation</summary>
  /// <returns>The week in a string format</returns>
  public string ToStringLarge()
  {
    DateTime dateTime = this.StartDate.AddDays(6.0);
    return dateTime.Month != this.StartDate.Month ? $"{this.StartDate.ToString("d MMM", (IFormatProvider) CultureInfo.CurrentUICulture)} - {dateTime.ToString("d MMM", (IFormatProvider) CultureInfo.CurrentUICulture)}" : $"{this.StartDate.Day} - {dateTime.ToString("d MMM", (IFormatProvider) CultureInfo.CurrentUICulture)}";
  }

  /// <summary>Returns a string representation of the week</summary>
  /// <returns></returns>
  public override string ToString() => this.ToStringLarge();

  /// <summary>
  /// Sets the value of the <see cref="P:Intermech.Client.Core.Organizer.CalendarWeek.Bounds" /> property
  /// </summary>
  /// <param name="r"></param>
  internal void SetBounds(Rectangle r) => this._bounds = r;
}
