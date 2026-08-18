// Decompiled with JetBrains decompiler
// Type: Intermech.Calendars.OrganizationUnitCalendar
// Assembly: Intermech.Calendars, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8CDCC38D-5D33-4955-8468-4C0264D69139
// Assembly location: D:\IPS\Client\Intermech.Calendars.dll
// XML documentation location: D:\IPS\Client\Intermech.Calendars.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Calendars;
using Intermech.Metadata;
using System;

#nullable disable
namespace Intermech.Calendars;

/// <summary>Класс хранящийся в объекте типа "Организационная единица"</summary>
[Serializable]
public class OrganizationUnitCalendar : 
  Calendar,
  IXmlObjectIPS,
  IOrganizationUnitCalendar,
  ICalendarInDB,
  ICalendarBase,
  IXmlReaderSupport
{
  public override CalendarOwnerType Owner => CalendarOwnerType.OrganizationUnit;

  /// <summary>Идентификатор организационной единицы</summary>
  [NotEmpty]
  public long UnitID { get; }

  public OrganizationUnitCalendar([NotEmpty] long unitID, [NotEmpty] long calendarID)
    : base(calendarID)
  {
    this.UnitID = unitID;
  }

  [ContractAnnotation("throwIfNotFound: true => NotNull; throwIfNotFound: false => CanBeNull")]
  public override IBlobWriter GetCalendarWriter([NotNull] IUserSession iUserSession, bool throwIfNotFound = true)
  {
    IDBObject dbObject = iUserSession.GetObject(this.UnitID, false) ?? iUserSession.GetObjectByID(this.UnitID, false);
    if (dbObject != null)
      return Intermech.Diagnostics.Check.Is<IBlobWriter>((object) (dbObject.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.Calendar) ?? dbObject.Attributes.AddAttribute((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.Calendar, false)), "dataAttribute");
    if (throwIfNotFound)
      throw new ObjectNotFoundException(this.UnitID);
    return (IBlobWriter) null;
  }
}
