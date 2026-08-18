
// Type: Intermech.Interfaces.Calendars.DayType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech.Interfaces.Calendars
{
    /// <summary>Тип дня.</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [Flags]
    [Serializable]
    public enum DayType
    {
      /// <summary>Стандартный рабочий день</summary>
      [CustomDescription("Attribute.Interfaces_454")] StandardWork = 0,
      /// <summary>Выходной день</summary>
      [CustomDescription("Attribute.Interfaces_455")] Holiday = 1,
      /// <summary>Рабочий день с нестандартным рабочим графиком</summary>
      [CustomDescription("Attribute.Interfaces_456")] NonStandardWork = 2,
      /// <summary>Оставлено для обратной совместимости, используйте DayType.StandardWork</summary>
      [Obsolete, Description("Obsolete")] StandartWork = 0,
      /// <summary>Оставлено для обратной совместимости, используйте DayType.Holiday</summary>
      [Obsolete, Description("Obsolete")] Holyday = Holiday, // 0x00000001
      /// <summary>Оставлено для обратной совместимости, используйте DayType.NonStandardWork</summary>
      [Obsolete, Description("Obsolete")] NonStandartWork = NonStandardWork, // 0x00000002
    }
}
