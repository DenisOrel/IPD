
// Type: Intermech.Calendars.Attributes
// Assembly: Intermech.Calendars, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8CDCC38D-5D33-4955-8468-4C0264D69139
// Assembly location: D:\IPS\Client\Intermech.Calendars.dll
// XML documentation location: D:\IPS\Client\Intermech.Calendars.xml

using Intermech.Diagnostics;
using Intermech.Metadata;
using System.Runtime.CompilerServices;


namespace Intermech.Calendars
{
    /// <summary>Системные типы атрибутов IPS.Calendars</summary>
    public abstract class Attributes : Intermech.Metadata.Attributes
    {
      [NotNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      private static SystemAttribute Create([NotNull, NotWhitespace] string guid, [CallerMemberName, NotNull, NotWhitespace] string idName = "")
      {
        return Intermech.Metadata.Attributes.Create<Attributes>(guid, true, idName);
      }

      /// <summary>Guid-ы и идентификаторы системных атрибутов IPS.Calendars (строковое представление Guid-ов)</summary>
      public new abstract class Consts : Intermech.Metadata.Attributes.Consts
      {
      }
    }
}
