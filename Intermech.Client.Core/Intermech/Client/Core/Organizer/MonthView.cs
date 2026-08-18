
// Type: Intermech.Client.Core.Organizer.MonthView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Calendars;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;


namespace Intermech.Client.Core.Organizer;

/// <summary>Класс хранит данные месяца.</summary>
public class MonthView
{
  /// <summary>Площадь месяца</summary>
  private Rectangle _bounds = Rectangle.Empty;

  /// <summary>Площадь месяца.</summary>
  public Rectangle Bounds
  {
    get
    {
      return !this._bounds.IsEmpty ? this._bounds : new Rectangle(0, 0, this.Size.Width, this.Size.Height);
    }
  }

  /// <summary>Календарь.</summary>
  public CalendarView Calendar { get; private set; }

  /// <summary>Наименование месяца.</summary>
  public string Caption
  {
    get
    {
      return this.FirstDateOfMonth.ToString(this.Calendar.MonthTitleFormat, (IFormatProvider) CultureInfo.CurrentUICulture);
    }
  }

  /// <summary>Наименования дней недели.</summary>
  public List<string> DayNames { get; set; }

  /// <summary>Площади наименований дней недели.</summary>
  public List<Rectangle> DayNamesBounds { get; private set; }

  /// <summary>Коллекция дней месяца.</summary>
  public List<DayView> Days { get; private set; }

  /// <summary>Список чисел выходных дней данного месяца.</summary>
  public List<int> ExcludedDays { get; private set; }

  /// <summary>
  /// 
  /// </summary>
  public DayView FirstDay => this.Days[0];

  /// <summary>Первое число данного месяца.</summary>
  public DateTime FirstDateOfMonth { get; private set; }

  /// <summary>Площадь заголовка.</summary>
  public Rectangle HeaderBounds { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public DayView LastDay => this.Days[this.Days.Count - 1];

  /// <summary>Координаты верхнего левого угла месяца.</summary>
  public Point Location => !this._bounds.IsEmpty ? this._bounds.Location : new Point(0, 0);

  /// <summary>Размер месяца.</summary>
  public Size Size => this.Calendar.MonthSize;

  /// <summary>Конструктор.</summary>
  /// <param name="calendarView">Календарь</param>
  /// <param name="date">Текущий день месяца</param>
  internal MonthView(CalendarView calendarView, DateTime date)
  {
    this.Calendar = calendarView;
    this.FirstDateOfMonth = new DateTime(date.Year, date.Month, 1);
    int num = this.FirstDateOfMonth.DayOfWeek - this.Calendar.FirstDayOfWeek;
    DateTime date1 = this.FirstDateOfMonth.AddDays((double) (-1 * (num < 0 ? 7 + num : num)));
    this.DayNames = new List<string>(7);
    this.DayNamesBounds = new List<Rectangle>(7);
    this.Days = new List<DayView>(42);
    this.ExcludedDays = new List<int>();
    this.HeaderBounds = Rectangle.Empty;
    string empty = string.Empty;
    for (int index = 0; index < this.Days.Capacity; ++index)
    {
      this.Days.Add(new DayView(this, date1));
      if (index < 7)
        this.DayNames.Add(date1.ToString(this.Calendar.DayNamesFormat, (IFormatProvider) CultureInfo.CurrentUICulture).Substring(0, this.Calendar.DayNamesLength));
      date1 = date1.AddDays(1.0);
    }
    this.ReadCalendarSettings();
  }

  /// <summary>
  /// Рассчитать область вывода для заголовка, наименований дней и дней недели.
  /// </summary>
  /// <param name="location">Координаты верхнего левого угла месяца</param>
  internal void SetLocation(Point location)
  {
    this._bounds = new Rectangle(location, this.Calendar.MonthSize);
    int num1 = location.X;
    int y1 = location.Y;
    int height1 = this.Calendar.DaySize.Height;
    if (this.Calendar.DaySize.Height < 16 /*0x10*/)
      height1 = 16 /*0x10*/;
    else if (height1 % 2 != 0)
      ++height1;
    this.HeaderBounds = new Rectangle(location, new Size(this.Size.Width, height1));
    Size daySize;
    int y2;
    if (this.Calendar.DayNamesVisible)
    {
      int num2 = location.Y + height1;
      for (int index = 0; index < 7; ++index)
      {
        List<Rectangle> dayNamesBounds = this.DayNamesBounds;
        int x = num1;
        int y3 = num2;
        daySize = this.Calendar.DaySize;
        int width1 = daySize.Width;
        daySize = this.Calendar.DaySize;
        int height2 = daySize.Height;
        Rectangle rectangle = new Rectangle(x, y3, width1, height2);
        dayNamesBounds.Add(rectangle);
        int num3 = num1;
        daySize = this.Calendar.DaySize;
        int width2 = daySize.Width;
        num1 = num3 + width2;
      }
      int num4 = num2;
      daySize = this.Calendar.DaySize;
      int num5 = daySize.Height + this.Calendar.ItemPadding.Top;
      y2 = num4 + num5;
    }
    else
    {
      this.DayNamesBounds = new List<Rectangle>(0);
      y2 = location.Y + height1 + this.Calendar.ItemPadding.Top;
    }
    int x1 = location.X;
    for (int index = 0; index < this.Days.Capacity; ++index)
    {
      this.Days[index].Bounds = new Rectangle(new Point(x1, y2), this.Calendar.DaySize);
      int num6 = x1;
      daySize = this.Calendar.DaySize;
      int width = daySize.Width;
      x1 = num6 + width;
      if ((index + 1) % 7 == 0)
      {
        x1 = location.X;
        int num7 = y2;
        daySize = this.Calendar.DaySize;
        int height3 = daySize.Height;
        y2 = num7 + height3;
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public void ReadCalendarSettings()
  {
    if (this.Calendar.CalendarSettings == null)
      return;
    foreach (DayView day1 in this.Days)
    {
      ICalendarDay dayByDate = this.Calendar.CalendarSettings.GetDayByDate(day1.Date);
      if (dayByDate != null)
      {
        DateTime dateTime = day1.Date;
        int month1 = dateTime.Month;
        dateTime = this.FirstDateOfMonth;
        int month2 = dateTime.Month;
        if (month1 == month2 && dayByDate.DayType == DayType.Holiday)
        {
          List<int> excludedDays = this.ExcludedDays;
          dateTime = day1.Date;
          int day2 = dateTime.Day;
          excludedDays.Add(day2);
        }
      }
    }
  }
}
