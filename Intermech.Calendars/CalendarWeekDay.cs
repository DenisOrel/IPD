// Decompiled with JetBrains decompiler
// Type: Intermech.Calendars.CalendarWeekDay
// Assembly: Intermech.Calendars, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8CDCC38D-5D33-4955-8468-4C0264D69139
// Assembly location: D:\IPS\Client\Intermech.Calendars.dll
// XML documentation location: D:\IPS\Client\Intermech.Calendars.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Calendars;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Calendars;

/// <summary>Класс, Описывающий день недели</summary>
[Serializable]
public class CalendarWeekDay : DayBase, IWeekDayInfo, ICalendarDay, IXmlReaderSupport
{
  private WeekDay _weekDay;

  /// <summary>Ссылка на стандартную неделю</summary>
  [CanBeNull]
  public StandardWeek WeekBase { get; }

  /// <summary>Номер дня недели</summary>
  public WeekDay WeekDay
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._weekDay;
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] private set
    {
      this._weekDay = value;
    }
  }

  /// <summary>Номер дня недели</summary>
  public DayOfWeek DayOfWeek
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return CalendarBase.WeekDayToDayOfWeek(this.WeekDay);
    }
  }

  /// <summary>Базовый конструктор</summary>
  public CalendarWeekDay([NotNull] StandardWeek weekBase, WeekDay weekDay, DayType dayType)
    : base(weekBase.Calendar)
  {
    this.WeekBase = weekBase;
    this.WeekDay = weekDay;
    this.DayType = dayType;
  }

  [CanBeNull]
  public static IWeekDayInfo ConvertToIWeekDayInfo([CanBeNull] CalendarWeekDay weekDayInfo)
  {
    return (IWeekDayInfo) weekDayInfo;
  }

  /// <summary>Записать поля в XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public override void WriteDataToXml(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    base.WriteDataToXml(xw, objectRefId);
    xw.WriteAttributeString("WeekDay", ((int) this.WeekDay).ToString());
  }

  /// <summary>Прочитать одно поле из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле прочитано</returns>
  public override bool ReadFieldFromXml(XmlReadArgsIPS readArgs)
  {
    if (!(readArgs.Reader.LocalName == "WeekDay"))
      return base.ReadFieldFromXml(readArgs);
    if (!readArgs.Reader.HasValue)
      readArgs.Reader.Read();
    this.WeekDay = (WeekDay) Convert.ToInt32(readArgs.Reader.Value);
    return true;
  }

  [NotNull]
  [NotWhitespace]
  public override string XmlNodeName
  {
    get
    {
      switch (this.WeekDay)
      {
        case WeekDay.Monday:
          return "Monday";
        case WeekDay.Tuesday:
          return "Tuesday";
        case WeekDay.Wednesday:
          return "Wednesday";
        case WeekDay.Thursday:
          return "Thursday";
        case WeekDay.Friday:
          return "Friday";
        case WeekDay.Saturday:
          return "Saturday";
        case WeekDay.Sunday:
          return "Sunday";
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }

  protected override bool LoadPropsFromXml(XmlReader reader, string name, string value)
  {
    if (!(name == "WeekDay"))
      return base.LoadPropsFromXml(reader, name, value);
    this.WeekDay = (WeekDay) int.Parse(value);
    return true;
  }
}
