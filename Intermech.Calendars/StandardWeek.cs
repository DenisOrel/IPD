// Decompiled with JetBrains decompiler
// Type: Intermech.Calendars.StandardWeek
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
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Calendars;

/// <summary>Класс "Стандартная рабочая неделя"</summary>
[Serializable]
public class StandardWeek : 
  IXmlObjectIPS,
  IWeekBase,
  IEnumerable<CalendarWeekDay>,
  IEnumerable,
  IXmlReaderSupport
{
  [NotNull]
  private CalendarBase _calendar;
  [NotNull]
  private readonly CalendarWeekDay _monday;
  [NotNull]
  private readonly CalendarWeekDay _tuesday;
  [NotNull]
  private readonly CalendarWeekDay _wednesday;
  [NotNull]
  private readonly CalendarWeekDay _thursday;
  [NotNull]
  private readonly CalendarWeekDay _friday;
  [NotNull]
  private readonly CalendarWeekDay _saturday;
  [NotNull]
  private readonly CalendarWeekDay _sunday;

  /// <summary>Ссылка на календарь, которому принадлежит неделя</summary>
  [NotNull]
  public CalendarBase Calendar
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._calendar;
    set
    {
      if (this._calendar == value)
        return;
      this._calendar = value;
      foreach (DayBase weekDay in (IEnumerable<CalendarWeekDay>) this.WeekDays)
        weekDay.Calendar = value;
    }
  }

  /// <summary>Список дней недели</summary>
  [NotNull]
  [ItemNotNull]
  public IReadOnlyList<CalendarWeekDay> WeekDays { get; }

  /// <summary>Список дней недели</summary>
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public CalendarWeekDay GetDayOfWeek(DayOfWeek dayOfWeek)
  {
    switch (dayOfWeek)
    {
      case DayOfWeek.Sunday:
        return this._sunday;
      case DayOfWeek.Monday:
        return this._monday;
      case DayOfWeek.Tuesday:
        return this._tuesday;
      case DayOfWeek.Wednesday:
        return this._wednesday;
      case DayOfWeek.Thursday:
        return this._thursday;
      case DayOfWeek.Friday:
        return this._friday;
      case DayOfWeek.Saturday:
        return this._saturday;
      default:
        throw new NotSupportedEnumException((Enum) dayOfWeek, $"Unsupported {"DayOfWeek"} value {dayOfWeek}");
    }
  }

  /// <summary>Получить день недели</summary>
  [NotNull]
  public CalendarWeekDay this[WeekDay param]
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      switch (param)
      {
        case WeekDay.Monday:
          return this._monday;
        case WeekDay.Tuesday:
          return this._tuesday;
        case WeekDay.Wednesday:
          return this._wednesday;
        case WeekDay.Thursday:
          return this._thursday;
        case WeekDay.Friday:
          return this._friday;
        case WeekDay.Saturday:
          return this._saturday;
        case WeekDay.Sunday:
          return this._sunday;
        default:
          throw new NotSupportedEnumException((Enum) param, $"Unsupported {"WeekDay"} value {param}");
      }
    }
  }

  /// <summary>Получить день недели по стандартному системному типу DayOfWeek</summary>
  IWeekDayInfo IWeekBase.GetDayOfWeek(DayOfWeek dayOfWeek)
  {
    return (IWeekDayInfo) this.GetDayOfWeek(dayOfWeek);
  }

  IReadOnlyList<IWeekDayInfo> IWeekBase.WeekDays => (IReadOnlyList<IWeekDayInfo>) this.WeekDays;

  /// <summary>Получить день недели</summary>
  IWeekDayInfo IWeekBase.this[WeekDay param] => (IWeekDayInfo) this[param];

  /// <summary>Понедельник</summary>
  IWeekDayInfo IWeekBase.Monday => (IWeekDayInfo) this._monday;

  /// <summary>Вторник</summary>
  IWeekDayInfo IWeekBase.Tuesday => (IWeekDayInfo) this._tuesday;

  /// <summary>Среда</summary>
  IWeekDayInfo IWeekBase.Wednesday => (IWeekDayInfo) this._wednesday;

  /// <summary>Четверг</summary>
  IWeekDayInfo IWeekBase.Thursday => (IWeekDayInfo) this._thursday;

  /// <summary>Пятница</summary>
  IWeekDayInfo IWeekBase.Friday => (IWeekDayInfo) this._friday;

  /// <summary>Суббота</summary>
  IWeekDayInfo IWeekBase.Saturday => (IWeekDayInfo) this._saturday;

  /// <summary>Воскресенье</summary>
  IWeekDayInfo IWeekBase.Sunday => (IWeekDayInfo) this._sunday;

  /// <summary>Базовый конструктор</summary>
  public StandardWeek([NotNull] CalendarBase calendar)
  {
    this._calendar = calendar;
    this.WeekDays = (IReadOnlyList<CalendarWeekDay>) new CalendarWeekDay[7]
    {
      this._monday = new CalendarWeekDay(this, WeekDay.Monday, DayType.StandardWork),
      this._tuesday = new CalendarWeekDay(this, WeekDay.Tuesday, DayType.StandardWork),
      this._wednesday = new CalendarWeekDay(this, WeekDay.Wednesday, DayType.StandardWork),
      this._thursday = new CalendarWeekDay(this, WeekDay.Thursday, DayType.StandardWork),
      this._friday = new CalendarWeekDay(this, WeekDay.Friday, DayType.StandardWork),
      this._saturday = new CalendarWeekDay(this, WeekDay.Saturday, DayType.Holiday),
      this._sunday = new CalendarWeekDay(this, WeekDay.Sunday, DayType.Holiday)
    };
  }

  /// <summary>Записать поля в XML</summary>
  /// <param name="elementName">Имя элемента XML, под которым нужно сохранить данные</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public void WriteToXml([NotNull] string elementName, [NotNull] XmlWriter xw, [NotNull] ObjectIDGenerator objectRefId)
  {
    xw.WriteStartElement(elementName);
    try
    {
      this._monday.WriteToXml("Monday", xw, objectRefId);
      this._tuesday.WriteToXml("Tuesday", xw, objectRefId);
      this._wednesday.WriteToXml("Wednesday", xw, objectRefId);
      this._thursday.WriteToXml("Thursday", xw, objectRefId);
      this._friday.WriteToXml("Friday", xw, objectRefId);
      this._saturday.WriteToXml("Saturday", xw, objectRefId);
      this._sunday.WriteToXml("Sunday", xw, objectRefId);
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
    foreach (DayBase weekDay in (IEnumerable<CalendarWeekDay>) this.WeekDays)
      weekDay.Calendar = this._calendar;
  }

  /// <summary>Прочитать одно поле из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле прочитано</returns>
  public bool ReadFieldFromXml(XmlReadArgsIPS readArgs)
  {
    switch (readArgs.Reader.LocalName)
    {
      case "Friday":
        this._friday.ReadFromXml(readArgs);
        return true;
      case "Monday":
        this._monday.ReadFromXml(readArgs);
        return true;
      case "Saturday":
        this._saturday.ReadFromXml(readArgs);
        return true;
      case "Sunday":
        this._sunday.ReadFromXml(readArgs);
        return true;
      case "Thursday":
        this._thursday.ReadFromXml(readArgs);
        return true;
      case "Tuesday":
        this._tuesday.ReadFromXml(readArgs);
        return true;
      case "Wednesday":
        this._wednesday.ReadFromXml(readArgs);
        return true;
      default:
        return false;
    }
  }

  public IEnumerator<CalendarWeekDay> GetEnumerator() => this.WeekDays.GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.WeekDays.GetEnumerator();

  public string XmlNodeName => nameof (StandardWeek);

  public void ReadFromXml([NotNull] XmlReader reader)
  {
    reader.ReadObject(new XmlReaderExtensions.LoadObjectPropertiesFromReaderMethod(this.LoadPropsFromXml));
  }

  private bool LoadPropsFromXml([NotNull] XmlReader reader, [NotNull, NotWhitespace] string name, [CanBeNull] string value)
  {
    switch (name)
    {
      case "Friday":
        this._friday.ReadFromXml(reader);
        return true;
      case "Monday":
        this._monday.ReadFromXml(reader);
        return true;
      case "Saturday":
        this._saturday.ReadFromXml(reader);
        return true;
      case "Sunday":
        this._sunday.ReadFromXml(reader);
        return true;
      case "Thursday":
        this._thursday.ReadFromXml(reader);
        return true;
      case "Tuesday":
        this._tuesday.ReadFromXml(reader);
        return true;
      case "Wednesday":
        this._wednesday.ReadFromXml(reader);
        return true;
      default:
        return false;
    }
  }
}
