// Decompiled with JetBrains decompiler
// Type: Intermech.Calendars.CalendarInDB
// Assembly: Intermech.Calendars, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8CDCC38D-5D33-4955-8468-4C0264D69139
// Assembly location: D:\IPS\Client\Intermech.Calendars.dll
// XML documentation location: D:\IPS\Client\Intermech.Calendars.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Calendars;
using System;

#nullable disable
namespace Intermech.Calendars;

/// <summary>Класс-описатель календаря в БД</summary>
[Serializable]
public abstract class CalendarInDB : 
  CalendarBase,
  IXmlObjectIPS,
  ICalendarInDB,
  ICalendarBase,
  IXmlReaderSupport
{
  /// <summary>Тип контейнера, содержащего календарь</summary>
  public abstract CalendarOwnerType Owner { get; }

  /// <summary>Сохранение параметров в объект с guid-ом = OwnerGuid</summary>
  public void SaveParams([NotNull] IUserSession iUserSession, bool throwIfNotFound = true)
  {
    IBlobWriter calendarWriter = this.GetCalendarWriter(iUserSession, throwIfNotFound);
    if (calendarWriter == null)
      return;
    this.SaveParams(calendarWriter);
  }

  [ContractAnnotation("throwIfNotFound: true => NotNull; throwIfNotFound: false => CanBeNull")]
  public abstract IBlobWriter GetCalendarWriter([NotNull] IUserSession iUserSession, bool throwIfNotFound = true);

  public virtual void ReadAdditionalParamsFromOwnerObject([NotNull] IDBObject calendarOwnerObj)
  {
  }
}
