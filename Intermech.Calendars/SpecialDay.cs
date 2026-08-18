// Decompiled with JetBrains decompiler
// Type: Intermech.Calendars.SpecialDay
// Assembly: Intermech.Calendars, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8CDCC38D-5D33-4955-8468-4C0264D69139
// Assembly location: D:\IPS\Client\Intermech.Calendars.dll
// XML documentation location: D:\IPS\Client\Intermech.Calendars.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Calendars;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Calendars;

/// <summary>Класс, описывающий специальный период календарных дней</summary>
[Serializable]
public class SpecialDay : DayBase, ISpecialCalendarDay, ICalendarDay, IXmlReaderSupport
{
  private DateTime _periodStartDate = new DateTime(0L);
  private DateTime _periodFinishDate = new DateTime(0L);
  private DateRepeatRate _dateRepeatRate;
  private int _lockCorrectionCounter;

  /// <summary>Дата начала специального периода календарных дней</summary>
  public DateTime PeriodStartDate
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._periodStartDate;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._periodStartDate = value;
      this.AfterChange();
    }
  }

  /// <summary>Дата окончания специального периода календарных дней</summary>
  public DateTime PeriodFinishDate
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._periodFinishDate;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._periodFinishDate = value;
      this.AfterChange();
    }
  }

  [NotNull]
  [NotEmpty]
  public DayOfWeek[] DaysOfWeekArray
  {
    get
    {
      DayOfWeek[] array = new DayOfWeek[Math.Min(this.PeriodSize + 1, 7)];
      int num = 0;
      DateTime date = this.PeriodFinishDate.Date;
      for (DateTime dateTime = this.PeriodStartDate.Date; dateTime <= date; dateTime = dateTime.AddDays(1.0))
      {
        array[num++] = dateTime.DayOfWeek;
        if (num >= 7)
          break;
      }
      Array.Sort<DayOfWeek>(array);
      return array;
    }
  }

  [NotNull]
  [NotEmpty]
  public WeekDay[] WeekDaysArray
  {
    get
    {
      WeekDay[] array = new WeekDay[Math.Min(this.PeriodSize + 1, 7)];
      int num = 0;
      DateTime date = this.PeriodFinishDate.Date;
      for (DateTime dateTime = this.PeriodStartDate.Date; dateTime <= date; dateTime = dateTime.AddDays(1.0))
      {
        array[num++] = CalendarsService.DayOfWeekToWeekDay(dateTime.DayOfWeek);
        if (num >= 7)
          break;
      }
      Array.Sort<WeekDay>(array);
      return array;
    }
  }

  [NotNull]
  [NotEmpty]
  public DateTime[] DaysInPeriodArray
  {
    get
    {
      DateTime[] daysInPeriodArray = new DateTime[this.PeriodSize + 1];
      int num = 0;
      DateTime date = this.PeriodFinishDate.Date;
      for (DateTime dateTime = this.PeriodStartDate.Date; dateTime <= date; dateTime = dateTime.AddDays(1.0))
        daysInPeriodArray[num++] = dateTime;
      return daysInPeriodArray;
    }
  }

  [NotNull]
  [NotEmpty]
  public IEnumerable<DateTime> DaysInPeriodEnumeration
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.GetDaysInPeriodEnumeration();
  }

  [NotNull]
  [NotEmpty]
  private IEnumerable<DateTime> GetDaysInPeriodEnumeration()
  {
    DateTime dateTime = this.PeriodFinishDate;
    DateTime periodFinishDate = dateTime.Date;
    dateTime = this.PeriodStartDate;
    for (DateTime date = dateTime.Date; date <= periodFinishDate; date = date.AddDays(1.0))
      yield return date;
  }

  /// <summary>Продолжительности специального периода дней</summary>
  public int PeriodSize
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (this._periodFinishDate - this._periodStartDate).Days;
    }
  }

  /// <summary>Периодичность повторения специального дня</summary>
  public DateRepeatRate DateRepeatRate
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._dateRepeatRate;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._dateRepeatRate = value;
      this.AfterChange();
    }
  }

  /// <summary>Конструктор</summary>
  public SpecialDay([NotNull] CalendarBase calendar)
    : base(calendar)
  {
  }

  /// <summary>Конструктор</summary>
  public SpecialDay([NotNull] CalendarBase calendar, [NotNull] XmlReader reader)
    : this(calendar)
  {
    this.ReadFromXml(reader);
  }

  /// <summary>Конструктор</summary>
  public SpecialDay([NotNull] CalendarBase calendar, DateTime date, DayType dayType)
    : this(calendar, date, date, dayType)
  {
  }

  /// <summary>Конструктор</summary>
  public SpecialDay(
    [NotNull] CalendarBase calendar,
    DateTime startDate,
    DateTime finishDate,
    DayType dayType)
    : this(calendar, startDate, finishDate, dayType, calendar.StandardWorkPeriods, DateRepeatRate.Once)
  {
  }

  /// <summary>Конструктор</summary>
  public SpecialDay(
    [NotNull] CalendarBase calendar,
    DateTime startDate,
    DateTime finishDate,
    DayType dayType,
    DateRepeatRate dateRepeatRate)
    : this(calendar, startDate, finishDate, dayType, calendar.StandardWorkPeriods, dateRepeatRate)
  {
  }

  /// <summary>Конструктор</summary>
  public SpecialDay(
    [NotNull] Intermech.Calendars.Calendar calendar,
    DateTime startDate,
    DateTime finishDate,
    DayType dayType,
    DateRepeatRate dateRepeatRate)
    : this((CalendarBase) calendar, startDate, finishDate, dayType, calendar.StandardWorkPeriods, dateRepeatRate)
  {
  }

  /// <summary>Конструктор</summary>
  public SpecialDay(
    [NotNull] Intermech.Calendars.Calendar calendar,
    DateTime startDate,
    DateTime finishDate,
    DayType dayType,
    [NotNull, ItemNotNull] WorkPeriodsList workTimePeriods)
    : this((CalendarBase) calendar, startDate, finishDate, dayType, workTimePeriods, DateRepeatRate.Once)
  {
  }

  /// <summary>Конструктор</summary>
  public SpecialDay(
    [NotNull] CalendarBase calendar,
    DateTime startDate,
    DateTime finishDate,
    DayType dayType,
    [NotNull, ItemNotNull] WorkPeriodsList workTimePeriods,
    DateRepeatRate dateRepeatRate)
    : this(calendar)
  {
    this.WorkTimePeriods = (IReadOnlyList<WorkTime>) workTimePeriods;
    this.DayType = dayType;
    this._periodStartDate = startDate;
    this._periodFinishDate = finishDate;
    this._dateRepeatRate = dateRepeatRate;
  }

  /// <summary>Создать копию</summary>
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static SpecialDay CreateCopy([NotNull] SpecialDay aBase)
  {
    return SpecialDay.CreateCopy(aBase, aBase.Calendar);
  }

  /// <summary>Создать копию</summary>
  [NotNull]
  public static SpecialDay CreateCopy([NotNull] SpecialDay aBase, [CanBeNull] CalendarBase targetCalendar)
  {
    SpecialDay copy = new SpecialDay(targetCalendar);
    copy.CopyParamsFrom(aBase);
    return copy;
  }

  [CanBeNull]
  public static ISpecialCalendarDay ConvertToISpecialCalendarDay([CanBeNull] SpecialDay specialCalendarDayInfo)
  {
    return (ISpecialCalendarDay) specialCalendarDayInfo;
  }

  /// <summary>Даты начала и окончания специального периода календарных дней</summary>
  /// <param name="periodStartDate">Дата начала специального периода календарных дней</param>
  /// <param name="periodFinishDate">Дата окончания специального периода календарных дней</param>
  public void SetStartFinishDates(DateTime periodStartDate, DateTime periodFinishDate)
  {
    this._periodStartDate = periodStartDate;
    this._periodFinishDate = periodFinishDate;
    this.AfterChange();
  }

  /// <summary>Автоматически вызывается после изменения данных</summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private void AfterChange() => this.CorrectPeriods();

  /// <summary>Заблокировать устранения конфликтов в периоде</summary>
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void LockCorrection() => ++this._lockCorrectionCounter;

  /// <summary>Разблокировать устранения конфликтов в периоде</summary>
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void UnlockCorrection()
  {
    if (this._lockCorrectionCounter <= 0)
      return;
    --this._lockCorrectionCounter;
    if (this._lockCorrectionCounter != 0)
      return;
    this.CorrectPeriods();
  }

  /// <summary>Функция устранения конфликтов в периоде</summary>
  private void CorrectPeriods()
  {
    if (this._lockCorrectionCounter != 0 || !(this._periodStartDate > this._periodFinishDate))
      return;
    this._periodFinishDate = this._periodStartDate;
  }

  /// <summary>Скопировать параметры из другого объекта</summary>
  public void CopyParamsFrom([NotNull] SpecialDay specialCalendarDayInfo)
  {
    this.CopyParamsFrom((DayBase) specialCalendarDayInfo);
    this._periodStartDate = specialCalendarDayInfo.PeriodStartDate;
    this._periodFinishDate = specialCalendarDayInfo._periodFinishDate;
    this._dateRepeatRate = specialCalendarDayInfo.DateRepeatRate;
  }

  /// <summary>Записать поля в XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public override void WriteDataToXml(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    base.WriteDataToXml(xw, objectRefId);
    if (this._periodStartDate.Year == this._periodFinishDate.Year && this._periodStartDate.DayOfYear == this._periodFinishDate.DayOfYear)
    {
      xw.WriteAttributeString("Date", this._periodStartDate.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    }
    else
    {
      xw.WriteAttributeString("StartDate", this._periodStartDate.ToString((IFormatProvider) CultureInfo.InvariantCulture));
      xw.WriteAttributeString("FinishDate", this._periodFinishDate.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    }
    xw.WriteAttributeString("RepeatRate", ((int) this._dateRepeatRate).ToString());
  }

  /// <summary>Прочитать одно поле из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле прочитано</returns>
  public override bool ReadFieldFromXml(XmlReadArgsIPS readArgs)
  {
    switch (readArgs.Reader.LocalName)
    {
      case "Date":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        if (!DateTime.TryParse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out this._periodStartDate) && !DateTime.TryParse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.CurrentCulture, DateTimeStyles.None, out this._periodStartDate) && !DateTime.TryParse(readArgs.Reader.Value, out this._periodStartDate) && !DateTime.TryParse(readArgs.Reader.Value, (IFormatProvider) CalendarBase.RuCultureInfo, DateTimeStyles.None, out this._periodStartDate))
          this._periodStartDate = Convert.ToDateTime(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
        this._periodFinishDate = this._periodStartDate;
        return true;
      case "StartDate":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        if (!DateTime.TryParse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out this._periodStartDate) && !DateTime.TryParse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.CurrentCulture, DateTimeStyles.None, out this._periodStartDate) && !DateTime.TryParse(readArgs.Reader.Value, out this._periodStartDate) && !DateTime.TryParse(readArgs.Reader.Value, (IFormatProvider) CalendarBase.RuCultureInfo, DateTimeStyles.None, out this._periodStartDate))
          this._periodStartDate = Convert.ToDateTime(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
        return true;
      case "FinishDate":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        if (!DateTime.TryParse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out this._periodFinishDate) && !DateTime.TryParse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.CurrentCulture, DateTimeStyles.None, out this._periodFinishDate) && !DateTime.TryParse(readArgs.Reader.Value, out this._periodFinishDate) && !DateTime.TryParse(readArgs.Reader.Value, (IFormatProvider) CalendarBase.RuCultureInfo, DateTimeStyles.None, out this._periodFinishDate))
          this._periodFinishDate = Convert.ToDateTime(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
        return true;
      case "RepeatRate":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this._dateRepeatRate = (DateRepeatRate) Convert.ToInt32(readArgs.Reader.Value);
        return true;
      default:
        return base.ReadFieldFromXml(readArgs);
    }
  }

  [NotNull]
  [NotWhitespace]
  public override string XmlNodeName => "SpecialPeriod";

  /// <summary>Загрузить из XML</summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public override void ReadFromXml([NotNull, NotEmpty] XmlReader reader)
  {
    reader.ReadObject(new XmlReaderExtensions.LoadObjectPropertiesFromReaderMethod(((DayBase) this).LoadPropsFromXml));
  }

  protected override bool LoadPropsFromXml([NotNull] XmlReader reader, [NotNull] string name, [CanBeNull] string value)
  {
    switch (name)
    {
      case "Date":
        if (!DateTime.TryParse(value, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out this._periodStartDate) && !DateTime.TryParse(value, (IFormatProvider) CultureInfo.CurrentCulture, DateTimeStyles.None, out this._periodStartDate) && !DateTime.TryParse(value, out this._periodStartDate) && !DateTime.TryParse(value, (IFormatProvider) CalendarBase.RuCultureInfo, DateTimeStyles.None, out this._periodStartDate))
          this._periodStartDate = Convert.ToDateTime(value, (IFormatProvider) CultureInfo.InvariantCulture);
        this._periodFinishDate = this._periodStartDate;
        return true;
      case "FinishDate":
        if (!DateTime.TryParse(value, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out this._periodFinishDate) && !DateTime.TryParse(value, (IFormatProvider) CultureInfo.CurrentCulture, DateTimeStyles.None, out this._periodFinishDate) && !DateTime.TryParse(value, out this._periodFinishDate) && !DateTime.TryParse(value, (IFormatProvider) CalendarBase.RuCultureInfo, DateTimeStyles.None, out this._periodFinishDate))
          this._periodFinishDate = Convert.ToDateTime(value, (IFormatProvider) CultureInfo.InvariantCulture);
        return true;
      case "RepeatRate":
        this._dateRepeatRate = (DateRepeatRate) Convert.ToInt32(value);
        return true;
      default:
        return base.LoadPropsFromXml(reader, name, value);
    }
  }
}
