// Decompiled with JetBrains decompiler
// Type: Intermech.Calendars.CalendarLoader
// Assembly: Intermech.Calendars, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8CDCC38D-5D33-4955-8468-4C0264D69139
// Assembly location: D:\IPS\Client\Intermech.Calendars.dll
// XML documentation location: D:\IPS\Client\Intermech.Calendars.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Calendars;
using Intermech.Metadata;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;

#nullable disable
namespace Intermech.Calendars;

/// <summary>Фабрика календарей</summary>
public static class CalendarLoader
{
  [ContractAnnotation("throwIfNotFound:false => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Calendar GetCalendarByID(
    [NotNull] IUserSession userSession,
    [NotEmpty] long calendarID,
    bool throwIfNotFound = true)
  {
    return CalendarLoader.GetCustomCalendarByID<Calendar>(userSession, calendarID, (Func<Calendar>) (() => new Calendar(calendarID)), throwIfNotFound);
  }

  [ContractAnnotation("throwIfNotFound:false => CanBeNull; => NotNull")]
  private static TCalendar GetCustomCalendarByID<TCalendar>(
    [NotNull] IUserSession userSession,
    [NotEmpty] long calendarID,
    [NotNull] Func<TCalendar> customCalendarConstructor,
    bool throwIfNotFound)
    where TCalendar : Calendar
  {
    IDBObject calendarOwnerObj = userSession.GetObject(calendarID, false) ?? userSession.GetObjectByID(calendarID, false);
    if (calendarOwnerObj == null)
    {
      if (throwIfNotFound)
        throw new CalendarNotFoundException(calendarID);
      return default (TCalendar);
    }
    if (!(calendarOwnerObj.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.Data) is IBlobReader attributeById))
      throw new AttributeNotFoundException(Intermech.Metadata.Attributes.Data.Name, string.Empty, calendarID);
    return CalendarLoader.ReadCalendarFromBlob<TCalendar>(calendarOwnerObj, attributeById, customCalendarConstructor, false);
  }

  [ContractAnnotation("throwIfNotFound:false => CanBeNull; => NotNull")]
  public static UserCalendar GetUserCalendar(
    [NotNull] IUserSession userSession,
    [NotEmpty] long userID,
    bool throwIfNotFound = true)
  {
    IDBObject calendarOwnerObj = userSession.GetObject(userID, false) ?? userSession.GetObjectByID(userID, false);
    if (calendarOwnerObj == null)
    {
      if (throwIfNotFound)
        throw new ObjectNotFoundException(userID);
      return (UserCalendar) null;
    }
    if (calendarOwnerObj.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.UserCalendar) is IBlobReader attributeById)
      return CalendarLoader.ReadCalendarFromBlob<UserCalendar>(calendarOwnerObj, attributeById, (Func<UserCalendar>) (() => new UserCalendar(userID)), throwIfNotFound);
    if (throwIfNotFound)
      throw new AttributeNotFoundException(Intermech.Metadata.Attributes.UserCalendar.Name, string.Empty, userID);
    return (UserCalendar) null;
  }

  [ContractAnnotation("throwIfNotFound:false => CanBeNull; => NotNull")]
  public static OrganizationUnitCalendar GetOrganizationUnitCalendar(
    [NotNull] IUserSession userSession,
    [NotEmpty] long unitID,
    bool throwIfNotFound = true)
  {
    IDBObject iDbAttributable = userSession.GetObject(unitID, false) ?? userSession.GetObjectByID(unitID, false);
    if (iDbAttributable == null)
    {
      if (throwIfNotFound)
        throw new ObjectNotFoundException(unitID);
      return (OrganizationUnitCalendar) null;
    }
    long calendarID;
    return !iDbAttributable.TryGetAttrObjLinkId((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.Calendar, out calendarID) ? (OrganizationUnitCalendar) null : CalendarLoader.GetCustomCalendarByID<OrganizationUnitCalendar>(userSession, calendarID, (Func<OrganizationUnitCalendar>) (() => new OrganizationUnitCalendar(unitID, calendarID)), throwIfNotFound);
  }

  [NotNull]
  public static TCalendar ReadCalendarFromBlob<TCalendar>(
    [NotNull] IDBObject calendarOwnerObj,
    [NotNull] IBlobReader blobReader,
    [NotNull] Func<TCalendar> emptyCalendarConstructor,
    bool throwIfNotFound = true)
    where TCalendar : CalendarInDB
  {
    BlobInformation blobInformation = blobReader.OpenBlob(0);
    try
    {
      if (blobInformation.RealFileSize > 0L)
      {
        MemoryStream inStream = (MemoryStream) null;
        try
        {
          byte[] buffer = blobReader.ReadDataBlock((int) blobInformation.RealFileSize);
          if (buffer != null)
          {
            if (buffer.Length != 0)
            {
              inStream = new MemoryStream(buffer);
              if (inStream.Length > 0L)
              {
                inStream.Seek(0L, SeekOrigin.Begin);
                inStream.Write(buffer, 0, buffer.Length);
                inStream.Seek(0L, SeekOrigin.Begin);
                if (blobInformation.ArcMethod == ArcMethods.ZLibPacked)
                {
                  MemoryStream outStream = new MemoryStream();
                  ZLibStreamHelper.UnpackStream((Stream) inStream, (Stream) outStream);
                  inStream.Close();
                  inStream = outStream;
                  inStream.Seek(0L, SeekOrigin.Begin);
                }
                MemoryStream input = inStream;
                using (XmlReader reader = XmlReader.Create((Stream) input, new XmlReaderSettings()
                {
                  IgnoreWhitespace = true
                }))
                {
                  TCalendar calendar = emptyCalendarConstructor();
                  calendar.ReadAdditionalParamsFromOwnerObject(calendarOwnerObj);
                  int content = (int) reader.MoveToContent();
                  calendar.ReadFromXml(reader);
                  return calendar;
                }
              }
            }
          }
        }
        finally
        {
          inStream?.Close();
        }
      }
      if (throwIfNotFound)
        throw new CalendarNotFoundException();
      return emptyCalendarConstructor();
    }
    finally
    {
      blobReader.CloseBlob();
    }
  }
}
