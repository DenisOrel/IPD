// Decompiled with JetBrains decompiler
// Type: Intermech.Calendars.CalendarBase
// Assembly: Intermech.Calendars, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8CDCC38D-5D33-4955-8468-4C0264D69139
// Assembly location: D:\IPS\Client\Intermech.Calendars.dll
// XML documentation location: D:\IPS\Client\Intermech.Calendars.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Calendars;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Calendars;

/// <summary>Прототип календаря, который используется для наполнения параметрами перед сохранением в БД</summary>
[Serializable]
public class CalendarBase : IXmlObjectIPS, ICalendarBase, IXmlReaderSupport
{
  [CanBeNull]
  private static CultureInfo _ruCultureInfo;
  private bool _needRecalcHoursInDay;
  private double _hoursInDay = 8.0;
  private double _hoursInWeek = 40.0;
  private WeekDay _weekStartDay = WeekDay.Monday;
  private Month _yearStartMonth = Month.January;

  [NotNull]
  internal static CultureInfo RuCultureInfo
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return CalendarBase._ruCultureInfo ?? (CalendarBase._ruCultureInfo = CultureInfo.GetCultureInfo("ru-RU"));
    }
  }

  /// <summary>Конструктор по-умолчанию</summary>
  public CalendarBase()
  {
    this.StandardWorkPeriods.Add(new WorkTime(9, 13));
    this.StandardWorkPeriods.Add(new WorkTime(14, 18));
    this.StandardWeek = new StandardWeek(this);
    this.SpecialCalendarDays = new SpecialPeriodsList(this);
  }

  /// <summary>Наименование календаря</summary>
  [NotNull]
  public string Name { get; private set; } = string.Empty;

  /// <summary>Стандартная неделя данного календаря</summary>
  [NotNull]
  public StandardWeek StandardWeek { get; }

  /// <summary>Список всех специальных периодов календарных дней</summary>
  [NotNull]
  [ItemNotNull]
  public SpecialPeriodsList SpecialCalendarDays { get; }

  /// <summary>День начала рабочей недели</summary>
  public WeekDay WeekStartDay
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._weekStartDay;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._weekStartDay = value;
    }
  }

  /// <summary>Месяц начала финансового года</summary>
  public Month YearStartMonth
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._yearStartMonth;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._yearStartMonth = value;
    }
  }

  /// <summary>Часы начала рабочего времени</summary>
  public int DefaultStartHour { get; set; } = 9;

  /// <summary>Минуты начала рабочего времени</summary>
  public int DefaultStartMinute { get; set; }

  /// <summary>Часы окончания рабочего времени</summary>
  public int DefaultFinishHour { get; set; } = 18;

  /// <summary>Минуты начала рабочего времени</summary>
  public int DefaultFinishMinute { get; set; }

  /// <summary>Рабочих часов в дне</summary>
  public double HoursInDay
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      if (this._needRecalcHoursInDay)
        this.RecalcHoursInDay();
      return this._hoursInDay;
    }
    set => this._hoursInDay = value;
  }

  /// <summary>Рабочих часов в неделе</summary>
  public double HoursInWeek
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      if (this._needRecalcHoursInDay)
        this.RecalcHoursInDay();
      return this._hoursInWeek;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._hoursInWeek = value;
    }
  }

  /// <summary>Рабочих дней в месяце</summary>
  public int DaysInMonth { get; set; } = 20;

  /// <summary>Рабочие периоды стандартного рабочего дня</summary>
  [NotNull]
  [ItemNotNull]
  public WorkPeriodsList StandardWorkPeriods { get; } = new WorkPeriodsList();

  /// <summary>Записать поля в XML</summary>
  /// <param name="elementName">Имя элемента XML, под которым нужно сохранить данные</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public void WriteToXml([NotNull] string elementName, [NotNull] XmlWriter xw, [CanBeNull] ObjectIDGenerator objectRefId)
  {
    xw.WriteStartElement(elementName);
    try
    {
      xw.WriteAttributeString("Name", this.Name);
      xw.WriteAttributeString("WeekStartDay", ((int) this.WeekStartDay).ToString());
      xw.WriteAttributeString("YearStartMonth", ((int) this.YearStartMonth).ToString());
      xw.WriteAttributeString("DefaultStartHour", this.DefaultStartHour.ToString());
      xw.WriteAttributeString("DefaultStartMinute", this.DefaultStartMinute.ToString());
      xw.WriteAttributeString("DefaultFinishHour", this.DefaultFinishHour.ToString());
      xw.WriteAttributeString("DefaultFinishMinute", this.DefaultFinishMinute.ToString());
      xw.WriteAttributeString("HoursInDay", this._hoursInDay.ToString((IFormatProvider) CultureInfo.InvariantCulture));
      xw.WriteAttributeString("HoursInWeek", this._hoursInWeek.ToString((IFormatProvider) CultureInfo.InvariantCulture));
      xw.WriteAttributeString("DaysInMonth", this.DaysInMonth.ToString());
      XmlHelperIPS.WriteListToXml("StandardDayWorkPeriods", (IList) this.StandardWorkPeriods, "WorkTime", xw, objectRefId);
      this.StandardWeek.WriteToXml("StandardWeek", xw, objectRefId);
      try
      {
        XmlHelperIPS.WriteListToXml("SpecialPeriods", (IList) this.SpecialCalendarDays, "SpecialPeriod", xw, objectRefId);
      }
      catch
      {
      }
    }
    finally
    {
      xw.WriteEndElement();
    }
  }

  /// <summary>Загрузить из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public void ReadFromXml(XmlReadArgsIPS readArgs)
  {
    XmlHelperIPS.ReadFromXml((IXmlObjectIPS) this, readArgs);
  }

  /// <summary>Прочитать одно поле из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле прочитано</returns>
  public bool ReadFieldFromXml(XmlReadArgsIPS readArgs)
  {
    switch (readArgs.Reader.LocalName)
    {
      case "DaysInMonth":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.DaysInMonth = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "DefaultFinishHour":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.DefaultFinishHour = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "DefaultFinishMinute":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.DefaultFinishMinute = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "DefaultStartHour":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.DefaultStartHour = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "DefaultStartMinute":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.DefaultStartMinute = Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "HoursInDay":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        try
        {
          this._hoursInDay = Convert.ToDouble(readArgs.Reader.Value, (IFormatProvider) CalendarBase.RuCultureInfo);
        }
        catch (FormatException ex1)
        {
          try
          {
            this._hoursInDay = Convert.ToDouble(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
          }
          catch (FormatException ex2)
          {
            try
            {
              this._hoursInDay = Convert.ToDouble(readArgs.Reader.Value);
            }
            catch (FormatException ex3)
            {
              this._needRecalcHoursInDay = true;
            }
          }
        }
        return true;
      case "HoursInWeek":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        try
        {
          this._hoursInWeek = Convert.ToDouble(readArgs.Reader.Value, (IFormatProvider) CalendarBase.RuCultureInfo);
        }
        catch (FormatException ex4)
        {
          try
          {
            this._hoursInWeek = Convert.ToDouble(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
          }
          catch (FormatException ex5)
          {
            try
            {
              this._hoursInWeek = Convert.ToDouble(readArgs.Reader.Value);
            }
            catch (FormatException ex6)
            {
              this._needRecalcHoursInDay = true;
            }
          }
        }
        return true;
      case "Name":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.Name = readArgs.Reader.Value;
        return true;
      case "SpecialDays":
      case "SpecialPeriods":
        this.SpecialCalendarDays.Clear();
        XmlHelperIPS.ReadListFromXml((IList) this.SpecialCalendarDays, typeof (SpecialDay), readArgs);
        for (int index = this.SpecialCalendarDays.Count - 1; index >= 0; --index)
        {
          SpecialDay specialCalendarDay = this.SpecialCalendarDays[index];
          if (specialCalendarDay.DateRepeatRate == DateRepeatRate.EveryWeek || specialCalendarDay.DateRepeatRate == DateRepeatRate.Once)
          {
            WeekDay weekDay = CalendarsService.DayOfWeekToWeekDay(specialCalendarDay.PeriodStartDate.DayOfWeek);
            if ((specialCalendarDay.DayType == this.StandardWeek[weekDay].DayType || this.StandardWeek[weekDay].DayType == DayType.StandardWork && specialCalendarDay.DayType == DayType.NonStandardWork) && specialCalendarDay.WorkTimePeriods.Equals((object) this.StandardWeek[weekDay].WorkTimePeriods))
              this.SpecialCalendarDays.RemoveAt(index);
          }
        }
        foreach (DayBase specialCalendarDay in (List<SpecialDay>) this.SpecialCalendarDays)
          specialCalendarDay.Calendar = this;
        return true;
      case "StandardDayWorkPeriods":
      case "StandartDayWorkPeriods":
        this.StandardWorkPeriods.Clear();
        XmlHelperIPS.ReadListFromXml((IList) this.StandardWorkPeriods, typeof (WorkTime), readArgs);
        return true;
      case "StandardWeek":
      case "StandartWeek":
        this.StandardWeek.ReadFromXml(readArgs);
        return true;
      case "WeekStartDay":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.WeekStartDay = (WeekDay) Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "YearStartMonth":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this.YearStartMonth = (Month) Convert.ToInt32(readArgs.Reader.Value);
        return true;
      default:
        return false;
    }
  }

  /// <summary>Сохранение в XML</summary>
  public void SaveToXmlDocument([NotNull, NotEmpty] string fileName)
  {
    XmlHelperIPS.WriteXmlDocument(fileName, (IXmlObjectIPS) this, "Calendar");
  }

  /// <summary>Сохранение в XML</summary>
  public void SaveToXmlDocument([NotNull] Stream stream)
  {
    XmlHelperIPS.WriteXmlDocument(stream, (IXmlObjectIPS) this, "Calendar");
  }

  /// <summary>
  /// Преобразование внутреннего тип WeekDay (1-понедельник ... 7 - воскресенье)
  /// в стандартный .NET тип DayOfWeek (0-воскресенье ... 6 - суббота)
  /// </summary>
  public static DayOfWeek WeekDayToDayOfWeek(WeekDay dayOfWeek)
  {
    return dayOfWeek != WeekDay.Sunday ? (DayOfWeek) dayOfWeek : DayOfWeek.Sunday;
  }

  public bool CheckDayForDate([NotNull] SpecialDay specialDay, DateTime day)
  {
    switch (specialDay.DateRepeatRate)
    {
      case DateRepeatRate.Once:
        return specialDay.PeriodStartDate <= day && specialDay.PeriodFinishDate >= day;
      case DateRepeatRate.EveryWeek:
        return specialDay.PeriodFinishDate.DayOfWeek >= specialDay.PeriodStartDate.DayOfWeek ? specialDay.PeriodStartDate.DayOfWeek <= day.DayOfWeek && specialDay.PeriodFinishDate.DayOfWeek >= day.DayOfWeek : specialDay.PeriodStartDate.DayOfWeek <= day.DayOfWeek || specialDay.PeriodFinishDate.DayOfWeek >= day.DayOfWeek;
      case DateRepeatRate.EveryMonth:
        return specialDay.PeriodFinishDate.Day >= specialDay.PeriodStartDate.Day ? specialDay.PeriodStartDate.Day <= day.Day && specialDay.PeriodFinishDate.Day >= day.Day : specialDay.PeriodStartDate.Day <= day.Day || specialDay.PeriodFinishDate.Day >= day.Day;
      case DateRepeatRate.EveryYear:
        return specialDay.PeriodFinishDate.DayOfYear >= specialDay.PeriodStartDate.DayOfYear ? specialDay.PeriodStartDate.DayOfYear <= day.DayOfYear && specialDay.PeriodFinishDate.DayOfYear >= day.DayOfYear : specialDay.PeriodStartDate.Day <= day.DayOfYear || specialDay.PeriodFinishDate.Day >= day.DayOfYear;
      default:
        throw new NotSupportedEnumException((Enum) specialDay.DateRepeatRate, $"Unsupported {"DateRepeatRate"} value {specialDay.DateRepeatRate}");
    }
  }

  /// <summary>Получить описание настроек дня по календарной дате</summary>
  /// <param name="day">Дата, на которую искать день</param>
  /// <param name="exceptDay">День, который надо игнорировать </param>
  [NotNull]
  public DayBase GetDayByDate(DateTime day, [CanBeNull] DayBase exceptDay = null)
  {
    SpecialDay specialDay = (SpecialDay) null;
    foreach (SpecialDay specialCalendarDay in (List<SpecialDay>) this.SpecialCalendarDays)
    {
      if (this.CheckDayForDate(specialCalendarDay, day) && (specialDay == null || specialDay == exceptDay || specialDay.DateRepeatRate != DateRepeatRate.Once && specialCalendarDay.DateRepeatRate == DateRepeatRate.Once || specialCalendarDay.PeriodSize < specialDay.PeriodSize))
        specialDay = specialCalendarDay;
    }
    return specialDay != null && specialDay != exceptDay ? (DayBase) specialDay : (DayBase) this.StandardWeek.GetDayOfWeek(day.DayOfWeek);
  }

  /// <summary>Получить описание настроек дня пересекающихся с календарным периодом</summary>
  [NotNull]
  public List<SpecialDay> GetSpecialDaysInPeriod(DateTime periodStart, DateTime periodFinish)
  {
    List<SpecialDay> specialDaysInPeriod = new List<SpecialDay>();
    DateTime dateTime;
    foreach (SpecialDay specialCalendarDay in (List<SpecialDay>) this.SpecialCalendarDays)
    {
      switch (specialCalendarDay.DateRepeatRate)
      {
        case DateRepeatRate.Once:
          if (specialCalendarDay.PeriodStartDate <= periodStart && specialCalendarDay.PeriodFinishDate >= periodStart || specialCalendarDay.PeriodStartDate <= periodFinish && specialCalendarDay.PeriodFinishDate >= periodFinish || specialCalendarDay.PeriodStartDate > periodStart && specialCalendarDay.PeriodFinishDate < periodFinish)
          {
            specialDaysInPeriod.Add(specialCalendarDay);
            continue;
          }
          continue;
        case DateRepeatRate.EveryWeek:
          specialDaysInPeriod.Add(specialCalendarDay);
          continue;
        case DateRepeatRate.EveryMonth:
          dateTime = specialCalendarDay.PeriodFinishDate;
          int day1 = dateTime.Day;
          dateTime = specialCalendarDay.PeriodStartDate;
          int day2 = dateTime.Day;
          if (day1 >= day2)
          {
            dateTime = specialCalendarDay.PeriodStartDate;
            if (dateTime.Day <= periodStart.Day)
            {
              dateTime = specialCalendarDay.PeriodFinishDate;
              if (dateTime.Day >= periodStart.Day)
                goto label_15;
            }
            dateTime = specialCalendarDay.PeriodStartDate;
            if (dateTime.Day <= periodFinish.Day)
            {
              dateTime = specialCalendarDay.PeriodFinishDate;
              if (dateTime.Day >= periodFinish.Day)
                goto label_15;
            }
            dateTime = specialCalendarDay.PeriodStartDate;
            if (dateTime.Day > periodFinish.Day)
            {
              dateTime = specialCalendarDay.PeriodFinishDate;
              if (dateTime.Day < periodFinish.Day)
                goto label_15;
            }
            dateTime = specialCalendarDay.PeriodStartDate;
            if (dateTime.Day >= periodStart.Day)
            {
              dateTime = specialCalendarDay.PeriodFinishDate;
              if (dateTime.Day > periodFinish.Day)
                continue;
            }
            else
              continue;
label_15:
            specialDaysInPeriod.Add(specialCalendarDay);
            continue;
          }
          if (periodFinish.Day < periodStart.Day)
          {
            specialDaysInPeriod.Add(specialCalendarDay);
            continue;
          }
          int day3 = periodStart.Day;
          dateTime = specialCalendarDay.PeriodFinishDate;
          int day4 = dateTime.Day;
          if (day3 > day4)
          {
            int day5 = periodFinish.Day;
            dateTime = specialCalendarDay.PeriodStartDate;
            int day6 = dateTime.Day;
            if (day5 < day6)
              continue;
          }
          specialDaysInPeriod.Add(specialCalendarDay);
          continue;
        case DateRepeatRate.EveryYear:
          dateTime = specialCalendarDay.PeriodFinishDate;
          int dayOfYear1 = dateTime.DayOfYear;
          dateTime = specialCalendarDay.PeriodStartDate;
          int dayOfYear2 = dateTime.DayOfYear;
          if (dayOfYear1 >= dayOfYear2)
          {
            dateTime = specialCalendarDay.PeriodStartDate;
            if (dateTime.DayOfYear <= periodStart.DayOfYear)
            {
              dateTime = specialCalendarDay.PeriodFinishDate;
              if (dateTime.DayOfYear >= periodStart.DayOfYear)
                goto label_30;
            }
            dateTime = specialCalendarDay.PeriodStartDate;
            if (dateTime.DayOfYear <= periodFinish.DayOfYear)
            {
              dateTime = specialCalendarDay.PeriodFinishDate;
              if (dateTime.DayOfYear >= periodFinish.DayOfYear)
                goto label_30;
            }
            dateTime = specialCalendarDay.PeriodStartDate;
            if (dateTime.DayOfYear > periodFinish.DayOfYear)
            {
              dateTime = specialCalendarDay.PeriodFinishDate;
              if (dateTime.DayOfYear < periodFinish.DayOfYear)
                goto label_30;
            }
            dateTime = specialCalendarDay.PeriodStartDate;
            if (dateTime.DayOfYear >= periodStart.DayOfYear)
            {
              dateTime = specialCalendarDay.PeriodFinishDate;
              if (dateTime.DayOfYear > periodFinish.DayOfYear)
                continue;
            }
            else
              continue;
label_30:
            specialDaysInPeriod.Add(specialCalendarDay);
            continue;
          }
          if (periodFinish.DayOfYear < periodStart.DayOfYear)
          {
            specialDaysInPeriod.Add(specialCalendarDay);
            continue;
          }
          int dayOfYear3 = periodStart.DayOfYear;
          dateTime = specialCalendarDay.PeriodFinishDate;
          int dayOfYear4 = dateTime.DayOfYear;
          if (dayOfYear3 > dayOfYear4)
          {
            int dayOfYear5 = periodFinish.DayOfYear;
            dateTime = specialCalendarDay.PeriodStartDate;
            int dayOfYear6 = dateTime.DayOfYear;
            if (dayOfYear5 < dayOfYear6)
              continue;
          }
          specialDaysInPeriod.Add(specialCalendarDay);
          continue;
        default:
          throw new NotSupportedEnumException((Enum) specialCalendarDay.DateRepeatRate, $"Unsupported {"DateRepeatRate"} value {specialCalendarDay.DateRepeatRate}");
      }
    }
    return specialDaysInPeriod;
  }

  /// <summary>Получить описание настроек ВСЕХ дней пересекающихся с календарным периодом</summary>
  [NotNull]
  public List<DayBase> GetDaysInPeriod(DateTime periodStart, DateTime periodFinish)
  {
    List<DayBase> daysInPeriod = new List<DayBase>((int) (periodFinish.Date - periodStart.Date).TotalDays + 1);
    for (DateTime day = periodStart.Date; day <= periodFinish.Date; day = day.AddDays(1.0))
      daysInPeriod.Add(this.GetDayByDate(day, (DayBase) null));
    return daysInPeriod;
  }

  /// <summary>Добавить в календарь новый специальный день</summary>
  public void AddSpecialDay([NotNull] SpecialDay newDay) => this.SpecialCalendarDays.Add(newDay);

  /// <summary>Обновление данных _hoursInDay и _hoursInWeek. Внутренняя служебная функция</summary>
  private void RecalcHoursInDay()
  {
    TimeSpan timeSpan1 = new TimeSpan(0L);
    foreach (WorkTime standardWorkPeriod in (List<WorkTime>) this.StandardWorkPeriods)
      timeSpan1 += standardWorkPeriod.Duration;
    this._hoursInDay = timeSpan1.TotalHours;
    TimeSpan timeSpan2 = new TimeSpan(0L);
    foreach (DayBase weekDay in (IEnumerable<CalendarWeekDay>) this.StandardWeek.WeekDays)
    {
      if (weekDay.DayType == DayType.StandardWork)
        timeSpan2 += timeSpan1;
    }
    this._hoursInWeek = timeSpan2.TotalHours;
    this._needRecalcHoursInDay = false;
  }

  /// <summary>Стандартная неделя данного календаря</summary>
  IWeekBase ICalendarBase.StandardWeek => (IWeekBase) this.StandardWeek;

  /// <summary>Список всех специальных периодов календарных дней</summary>
  IReadOnlyList<ISpecialCalendarDay> ICalendarBase.SpecialCalendarDays
  {
    get => (IReadOnlyList<ISpecialCalendarDay>) this.SpecialCalendarDays;
  }

  /// <summary>День начала рабочей недели</summary>
  DayOfWeek ICalendarBase.WeekStartDay
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return CalendarBase.WeekDayToDayOfWeek(this.WeekStartDay);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this.WeekStartDay = CalendarsService.DayOfWeekToWeekDay(value);
    }
  }

  /// <summary>Рабочие периоды стандартного рабочего дня (интерфейсов IWorkPeriod)</summary>
  IReadOnlyList<IWorkTimePeriod> ICalendarBase.StandardWorkPeriods
  {
    get
    {
      return (IReadOnlyList<IWorkTimePeriod>) this.StandardWorkPeriods.ConvertAll<IWorkTimePeriod>(new Converter<WorkTime, IWorkTimePeriod>(WorkTime.ConvertToIWorkTimePeriod));
    }
  }

  /// <summary>Получить интерфейс описания настроек дня по календарной дате</summary>
  ICalendarDay ICalendarBase.GetDayByDate(DateTime day)
  {
    return (ICalendarDay) this.GetDayByDate(day, (DayBase) null);
  }

  /// <summary>Получить описание настроек дня пересекающихся с календарным периодом</summary>
  IReadOnlyList<ISpecialCalendarDay> ICalendarBase.GetSpecialDaysInPeriod(
    DateTime periodStart,
    DateTime periodFinish)
  {
    return (IReadOnlyList<ISpecialCalendarDay>) this.GetSpecialDaysInPeriod(periodStart, periodFinish);
  }

  /// <summary>Получить описание настроек ВСЕХ дней пересекающихся с календарным периодом</summary>
  IReadOnlyList<ICalendarDay> ICalendarBase.GetDaysInPeriod(
    DateTime periodStart,
    DateTime periodFinish)
  {
    return (IReadOnlyList<ICalendarDay>) this.GetDaysInPeriod(periodStart, periodFinish);
  }

  /// <summary>Сохранение параметров в атрибут = OwnerGuid</summary>
  public void SaveParams([NotNull] IBlobWriter blobWriter)
  {
    using (MemoryStream inStream = new MemoryStream())
    {
      this.SaveToXmlDocument((Stream) inStream);
      long position = inStream.Position;
      inStream.Position = 0L;
      using (MemoryStream outStream = new MemoryStream())
      {
        ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).PackStream((Stream) outStream, (Stream) inStream, Convert.ToInt32((object) ZLibCompressLevels.LevelMax));
        outStream.Position = 0L;
        byte[] buffer = outStream.GetBuffer();
        byte[] data = new byte[outStream.Length];
        byte[] dst = data;
        int length = (int) outStream.Length;
        Buffer.BlockCopy((Array) buffer, 0, (Array) dst, 0, length);
        BlobInformation blobInfo = new BlobInformation(position, outStream.Length, DateTime.Now, string.Empty, ArcMethods.ZLibPacked, string.Empty);
        if (!blobWriter.OpenBlob(blobInfo, false))
          return;
        blobWriter.WriteDataBlock(data);
      }
    }
  }

  public string XmlNodeName => "Calendar";

  public void ReadFromXml([NotNull] XmlReader reader)
  {
    reader.ReadObject(this.XmlNodeName, new XmlReaderExtensions.LoadObjectPropertiesFromReaderMethod(this.LoadObjectPropertiesMethod));
  }

  private bool LoadObjectPropertiesMethod([NotNull] XmlReader reader, [NotNull, NotWhitespace] string name, [CanBeNull] string value)
  {
    switch (name)
    {
      case "DaysInMonth":
        this.DaysInMonth = Convert.ToInt32(value);
        return true;
      case "DefaultFinishHour":
        this.DefaultFinishHour = Convert.ToInt32(value);
        return true;
      case "DefaultFinishMinute":
        this.DefaultFinishMinute = Convert.ToInt32(value);
        return true;
      case "DefaultStartHour":
        this.DefaultStartHour = Convert.ToInt32(value);
        return true;
      case "DefaultStartMinute":
        this.DefaultStartMinute = Convert.ToInt32(value);
        return true;
      case "HoursInDay":
        if (!CalendarBase.TryParseDouble(value, out this._hoursInDay))
          this._needRecalcHoursInDay = true;
        return true;
      case "HoursInWeek":
        if (!CalendarBase.TryParseDouble(value, out this._hoursInWeek))
          this._needRecalcHoursInDay = true;
        return true;
      case "Name":
        this.Name = value;
        return true;
      case "SpecialDays":
      case "SpecialPeriods":
        this.SpecialCalendarDays.ReadFromXml(reader);
        return true;
      case "StandardDayWorkPeriods":
      case "StandartDayWorkPeriods":
        this.StandardWorkPeriods.ReadFromXml(reader);
        return true;
      case "StandardWeek":
      case "StandartWeek":
        this.StandardWeek.ReadFromXml(reader);
        return true;
      case "WeekStartDay":
        this.WeekStartDay = (WeekDay) int.Parse(value);
        return true;
      case "YearStartMonth":
        this.YearStartMonth = (Month) int.Parse(value);
        return true;
      default:
        return false;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static bool TryParseDouble([NotNull, NotWhitespace] string str, out double result)
  {
    try
    {
      result = Convert.ToDouble(str, (IFormatProvider) CalendarBase.RuCultureInfo);
    }
    catch (FormatException ex1)
    {
      try
      {
        result = Convert.ToDouble(str, (IFormatProvider) CultureInfo.InvariantCulture);
      }
      catch (FormatException ex2)
      {
        try
        {
          result = Convert.ToDouble(str);
        }
        catch (FormatException ex3)
        {
          result = 0.0;
          return false;
        }
      }
    }
    return true;
  }
}
