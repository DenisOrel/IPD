
// Type: Intermech.Interfaces.EveryDayExecution
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech.Interfaces
{
    /// <summary>Выполнение каждый день</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_532")]
    [Category("Misc")]
    [Serializable]
    public enum EveryDayExecution
    {
      None,
      /// <summary>Каждый день</summary>
      [CustomDescription("Attribute.Interfaces_531")] EveryDay,
      /// <summary>По рабочим дням</summary>
      [CustomDescription("Attribute.Interfaces_533")] OnWorkdays,
      /// <summary>По выходным и праздничным дням</summary>
      [CustomDescription("Attribute.Interfaces_534")] OnHolidays,
    }
}
