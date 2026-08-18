
// Type: Intermech.Client.Core.Organizer.DayView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Calendars;
using System;
using System.Collections.Generic;
using System.Drawing;


namespace Intermech.Client.Core.Organizer;

/// <summary>Класс хранит данные дня.</summary>
public class DayView
{
  /// <summary>Календарь</summary>
  private CalendarView _calendar;

  /// <summary>Площадь дня.</summary>
  public Rectangle Bounds { get; set; }

  /// <summary>Дата месяца.</summary>
  public DateTime Date { get; private set; }

  /// <summary>Принадлежность дня текущему месяцу.</summary>
  public bool Grayed
  {
    get
    {
      DateTime dateTime = this.Month.FirstDateOfMonth;
      int month1 = dateTime.Month;
      dateTime = this.Date;
      int month2 = dateTime.Month;
      return month1 != month2;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public bool IsMarked { get; set; }

  /// <summary>Месяц.</summary>
  public MonthView Month { get; private set; }

  /// <summary>Выделен ли день в календаре.</summary>
  public bool Selected
  {
    get
    {
      bool selected;
      if (this._calendar.DateSelectionMode != DateSelectionMode.WorkWeek)
      {
        selected = this._calendar.SelectionBegin <= this.Date && this.Date <= this._calendar.SelectionEnd;
      }
      else
      {
        int month1 = this.Month.FirstDateOfMonth.Month;
        DateTime date = this.Date;
        int month2 = date.Month;
        if (month1 == month2)
        {
          List<int> excludedDays = this.Month.ExcludedDays;
          date = this.Date;
          int day = date.Day;
          if (excludedDays.Contains(day))
          {
            selected = false;
            goto label_6;
          }
        }
        selected = this._calendar.CalendarSettings.GetDayByDate(this.Date).DayType != DayType.Holiday && this._calendar.SelectionBegin <= this.Date && this.Date <= this._calendar.SelectionEnd;
      }
label_6:
      return selected;
    }
  }

  /// <summary>Видимость дня в месяце.</summary>
  public bool Visible
  {
    get
    {
      return !this.Grayed || !(this.Date > this._calendar.ViewStart) || !(this.Date < this._calendar.ViewEnd);
    }
  }

  /// <summary>Конструктор.</summary>
  /// <param name="month">Месяц</param>
  /// <param name="date">Дата месяца</param>
  internal DayView(MonthView month, DateTime date)
  {
    this._calendar = month.Calendar;
    this.Month = month;
    this.Date = date;
  }
}
