// Decompiled with JetBrains decompiler
// Type: Intermech.Calendars.DayBase
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

/// <summary>Класс, описывающий календарный день</summary>
[Serializable]
public abstract class DayBase : IXmlObjectIPS, ICalendarDay, IXmlReaderSupport
{
  private DayType _dayType;
  [NotNull]
  private WorkPeriodsList _workTimePeriods;
  [NotNull]
  private CalendarBase _calendar;

  /// <summary>Ссылка на календарь, которому принадлежит данный день</summary>
  [NotNull]
  public CalendarBase Calendar
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._calendar;
    set
    {
      if (this._calendar == value)
        return;
      if (this._workTimePeriods.Count == 0)
        this._workTimePeriods = value.StandardWorkPeriods;
      this._calendar = value;
    }
  }

  internal bool IsStandardWorkTime
  {
    get
    {
      return this._workTimePeriods == this._calendar.StandardWorkPeriods || this._workTimePeriods.Equals((ICollection<WorkTime>) this._calendar.StandardWorkPeriods);
    }
  }

  /// <summary>Тип дня (стандартный рабочий, выходной, нестандартный рабочий)</summary>
  public DayType DayType
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._dayType;
    set
    {
      if (this._dayType == value)
        return;
      if (value == DayType.StandardWork)
        this._workTimePeriods = this._calendar.StandardWorkPeriods;
      else if (this.IsStandardWorkTime)
        this.CreateStandardWorkPeriods();
      this._dayType = value;
    }
  }

  public void AddWorkPeriod([NotNull] WorkTime workTimePeriod)
  {
    Intermech.Diagnostics.Check.Assert(this._workTimePeriods != this.Calendar.StandardWorkPeriods, "StandardWorkPeriods not allowed!");
    this._workTimePeriods.Add(workTimePeriod);
  }

  public void RemoveWorkPeriod(int periodNum)
  {
    Intermech.Diagnostics.Check.Assert(this._workTimePeriods != this.Calendar.StandardWorkPeriods, "StandardWorkPeriods not allowed!");
    this._workTimePeriods.RemoveAt(periodNum);
  }

  /// <summary>Список рабочих периодов</summary>
  [NotNull]
  [ItemNotNull]
  public IReadOnlyList<WorkTime> WorkTimePeriods
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (IReadOnlyList<WorkTime>) this._workTimePeriods;
    }
    set
    {
      if (this._workTimePeriods == this._calendar.StandardWorkPeriods)
        return;
      this.CopyWorkTimePeriodsFrom((IEnumerable<WorkTime>) value);
    }
  }

  /// <summary>Список рабочих периодов</summary>
  [NotNull]
  [ItemNotNull]
  IReadOnlyList<IWorkTimePeriod> ICalendarDay.WorkTimePeriods
  {
    get
    {
      return this._dayType == DayType.Holiday ? (IReadOnlyList<IWorkTimePeriod>) Array.Empty<IWorkTimePeriod>() : (IReadOnlyList<IWorkTimePeriod>) this._workTimePeriods;
    }
  }

  /// <summary>Создание стандартного рабочего дня</summary>
  protected DayBase([NotNull] CalendarBase calendar)
  {
    this._calendar = calendar;
    this._dayType = DayType.StandardWork;
    this._workTimePeriods = calendar.StandardWorkPeriods;
  }

  /// <summary>Создать стандартные рабочие периоды</summary>
  private void CreateStandardWorkPeriods()
  {
    this._workTimePeriods = new WorkPeriodsList();
    this.CopyWorkTimePeriodsFrom((IEnumerable<WorkTime>) this._calendar.StandardWorkPeriods);
  }

  /// <summary>Скопировать рабочие периоды из другого источника</summary>
  private void CopyWorkTimePeriodsFrom([NotNull, ItemNotNull] IEnumerable<WorkTime> listOfWorkTimePeriods)
  {
    if (this._workTimePeriods == this._calendar.StandardWorkPeriods)
      return;
    this._workTimePeriods.Clear();
    foreach (WorkTime ofWorkTimePeriod in listOfWorkTimePeriods)
      this._workTimePeriods.Add(WorkTime.CreateCopy(ofWorkTimePeriod));
  }

  /// <summary>Скопировать параметры из другого объекта</summary>
  public void CopyParamsFrom([NotNull] DayBase calendarDayInfo)
  {
    if (this.Calendar != null)
      return;
    this.WorkTimePeriods = calendarDayInfo.WorkTimePeriods;
  }

  [CanBeNull]
  public static ICalendarDay ConvertToICalendarDay([CanBeNull] DayBase calendarDayInfo)
  {
    return (ICalendarDay) calendarDayInfo;
  }

  /// <summary>Записать поля в XML</summary>
  /// <param name="elementName">Имя элемента XML, под которым нужно сохранить данные</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public void WriteToXml([NotNull, NotEmpty] string elementName, [NotNull] XmlWriter xw, [NotNull] ObjectIDGenerator objectRefId)
  {
    xw.WriteStartElement(elementName);
    try
    {
      this.WriteDataToXml(xw, objectRefId);
      XmlHelperIPS.WriteListToXml("WorkTimePeriods", (IList) this._workTimePeriods, "WorkTimePeriod", xw, objectRefId);
    }
    finally
    {
      xw.WriteEndElement();
    }
  }

  /// <summary>Записать поля в XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public virtual void WriteDataToXml([NotNull] XmlWriter xw, [NotNull] ObjectIDGenerator objectRefId)
  {
    xw.WriteAttributeString("DayType", ((int) this._dayType).ToString());
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
  public virtual bool ReadFieldFromXml(XmlReadArgsIPS readArgs)
  {
    switch (readArgs.Reader.LocalName)
    {
      case "DayType":
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        this._dayType = (DayType) Convert.ToInt32(readArgs.Reader.Value);
        return true;
      case "WorkTimePeriods":
        this._workTimePeriods.Clear();
        XmlHelperIPS.ReadListFromXml((IList) this._workTimePeriods, typeof (WorkTime), readArgs);
        return true;
      default:
        return false;
    }
  }

  [NotNull]
  [NotWhitespace]
  public abstract string XmlNodeName { get; }

  /// <summary>Загрузить из XML</summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public virtual void ReadFromXml([NotNull, NotEmpty] XmlReader reader)
  {
    reader.ReadObject(this.XmlNodeName, new XmlReaderExtensions.LoadObjectPropertiesFromReaderMethod(this.LoadPropsFromXml));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected virtual bool LoadPropsFromXml([NotNull] XmlReader reader, [NotNull, NotWhitespace] string name, [CanBeNull] string value)
  {
    switch (name)
    {
      case "DayType":
        this.DayType = (DayType) int.Parse(value);
        return true;
      case "WorkTimePeriods":
        if (this._workTimePeriods == this._calendar.StandardWorkPeriods)
          return false;
        this._workTimePeriods.ReadFromXml(reader);
        return true;
      default:
        return false;
    }
  }
}
