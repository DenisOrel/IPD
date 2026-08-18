
// Type: Intermech.Interfaces.Calendars.DateRepeatRate
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech.Interfaces.Calendars
{
    /// <summary>Повторяемость специального рабочего дня.</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [Serializable]
    public enum DateRepeatRate
    {
      /// <summary>Однократно</summary>
      [CustomDescription("Attribute.Interfaces_476")] Once,
      /// <summary>Еженедельно</summary>
      [CustomDescription("Attribute.Interfaces_477")] EveryWeek,
      /// <summary>Ежемесячно</summary>
      [CustomDescription("Attribute.Interfaces_478")] EveryMonth,
      /// <summary>Ежегодно</summary>
      [CustomDescription("Attribute.Interfaces_479")] EveryYear,
    }
}
