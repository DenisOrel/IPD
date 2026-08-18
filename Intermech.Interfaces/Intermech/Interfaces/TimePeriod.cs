
// Type: Intermech.Interfaces.TimePeriod
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech.Interfaces
{
    /// <summary>Периодичность</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_530")]
    [Category("Misc")]
    [Serializable]
    public enum TimePeriod
    {
      /// <summary>Однократно</summary>
      [CustomDescription("Attribute.Interfaces_476")] OneTime,
      /// <summary>Ежедневно</summary>
      [CustomDescription("Attribute.Interfaces_531")] EveryDay,
      /// <summary>Еженедельно</summary>
      [CustomDescription("Attribute.Interfaces_477")] EveryWeek,
      /// <summary>Ежемесячно</summary>
      [CustomDescription("Attribute.Interfaces_478")] EveryMonth,
    }
}
