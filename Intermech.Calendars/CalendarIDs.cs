
// Type: Intermech.Calendars.CalendarIDs
// Assembly: Intermech.Calendars, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8CDCC38D-5D33-4955-8468-4C0264D69139
// Assembly location: D:\IPS\Client\Intermech.Calendars.dll
// XML documentation location: D:\IPS\Client\Intermech.Calendars.xml

using Intermech.Extensions;
using Intermech.Interfaces;
using System;
using System.Runtime.CompilerServices;


namespace Intermech.Calendars
{
    /// <summary>Кэш идентификаторов типов и прочей мета-информации</summary>
    public static class CalendarIDs
    {
      private static int? _calendarsTypeID;
      private static int? _dataAttributeID;

      /// <summary>Идентификатор типа объекта "календари"</summary>
      public static int CalendarsTypeID
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return Helper.GetOrInit<int>(ref CalendarIDs._calendarsTypeID, (Func<int>) (() => MetaDataHelperService.Instance.GetObjectTypeID("cad00d87-306c-11d8-b4e9-00304f19f545")));
        }
      }

      internal static int DataAttributeID
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return Helper.GetOrInit<int>(ref CalendarIDs._dataAttributeID, (Func<int>) (() => MetaDataHelperService.Instance.GetAttributeTypeID("cad001b2-306c-11d8-b4e9-00304f19f545")));
        }
      }
    }
}
