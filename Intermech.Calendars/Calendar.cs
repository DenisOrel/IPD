// Decompiled with JetBrains decompiler
// Type: Intermech.Calendars.Calendar
// Assembly: Intermech.Calendars, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8CDCC38D-5D33-4955-8468-4C0264D69139
// Assembly location: D:\IPS\Client\Intermech.Calendars.dll
// XML documentation location: D:\IPS\Client\Intermech.Calendars.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Calendars;
using Intermech.Metadata;
using System;

#nullable disable
namespace Intermech.Calendars;

/// <summary>Класс хранящийся в объекте типа "Календарь"</summary>
[Serializable]
public class Calendar : CalendarInDB, IXmlObjectIPS, ICalendar, ICalendarInDB, ICalendarBase
{
  public override CalendarOwnerType Owner => CalendarOwnerType.CalendarObject;

  /// <summary>Идентификатор календаря</summary>
  [NotEmpty]
  public long CalendarID { get; }

  public Calendar([NotEmpty] long calendarID) => this.CalendarID = calendarID;

  [ContractAnnotation("throwIfNotFound: true => NotNull; throwIfNotFound: false => CanBeNull")]
  public override IBlobWriter GetCalendarWriter([NotNull] IUserSession iUserSession, bool throwIfNotFound = true)
  {
    IDBObject dbObject = iUserSession.GetObject(this.CalendarID, false) ?? iUserSession.GetObjectByID(this.CalendarID, false);
    if (dbObject != null)
    {
      int data = (int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.Data;
      return Intermech.Diagnostics.Check.Is<IBlobWriter>((object) (dbObject.GetAttributeByID(data) ?? dbObject.Attributes.AddAttribute(data, false)), "dataAttribute");
    }
    if (throwIfNotFound)
      throw new CalendarNotFoundException(this.CalendarID);
    return (IBlobWriter) null;
  }
}
