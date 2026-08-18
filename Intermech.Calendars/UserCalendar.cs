
// Type: Intermech.Calendars.UserCalendar
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


namespace Intermech.Calendars
{
    /// <summary>Класс хранящийся в объекте типа "Пользователь"</summary>
    [Serializable]
    public class UserCalendar : 
      CalendarInDB,
      IXmlObjectIPS,
      IUserCalendar,
      ICalendarInDB,
      ICalendarBase,
      IXmlReaderSupport
    {
      public override CalendarOwnerType Owner => CalendarOwnerType.User;

      /// <summary>Идентификатор пользователя</summary>
      [NotEmpty]
      public long UserID { get; }

      /// <summary>Дата принятия на работу</summary>
      public DateTime? HireDate { get; set; }

      /// <summary>Дата Увольнения</summary>
      public DateTime? FireDate { get; set; }

      public UserCalendar([NotEmpty] long userID) => this.UserID = userID;

      public override void ReadAdditionalParamsFromOwnerObject([NotNull] IDBObject calendarOwnerObj)
      {
        this.HireDate = calendarOwnerObj.GetAttrDateTimeValueOrNull((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.UserHireDate);
        this.FireDate = calendarOwnerObj.GetAttrDateTimeValueOrNull((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.UserFireDate);
      }

      [ContractAnnotation("throwIfNotFound: true => NotNull; throwIfNotFound: false => CanBeNull")]
      public override IBlobWriter GetCalendarWriter([NotNull] IUserSession iUserSession, bool throwIfNotFound = true)
      {
        IDBObject iDbAttributable = iUserSession.GetObject(this.UserID, false) ?? iUserSession.GetObjectByID(this.UserID, false);
        if (iDbAttributable != null)
        {
          iDbAttributable.SetAttrNullableDateTimeValue((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.UserHireDate, this.HireDate);
          iDbAttributable.SetAttrNullableDateTimeValue((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.UserFireDate, this.FireDate);
          return Intermech.Diagnostics.Check.Is<IBlobWriter>((object) (iDbAttributable.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.UserCalendar) ?? iDbAttributable.Attributes.AddAttribute((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.UserCalendar, false)), "dataAttribute");
        }
        if (throwIfNotFound)
          throw new ObjectNotFoundException(this.UserID);
        return (IBlobWriter) null;
      }
    }
}
