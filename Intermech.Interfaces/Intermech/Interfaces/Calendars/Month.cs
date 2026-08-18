
// Type: Intermech.Interfaces.Calendars.Month
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech.Interfaces.Calendars
{
    /// <summary>Месяц года.</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [Serializable]
    public enum Month
    {
      /// <summary>Январь</summary>
      [CustomDescription("Attribute.Interfaces_457")] January = 1,
      /// <summary>Февраль</summary>
      [CustomDescription("Attribute.Interfaces_458")] February = 2,
      /// <summary>Март</summary>
      [CustomDescription("Attribute.Interfaces_459")] March = 3,
      /// <summary>Апрель</summary>
      [CustomDescription("Attribute.Interfaces_460")] April = 4,
      /// <summary>Май</summary>
      [CustomDescription("Attribute.Interfaces_461")] May = 5,
      /// <summary>Июнь</summary>
      [CustomDescription("Attribute.Interfaces_462")] June = 6,
      /// <summary>Июль</summary>
      [CustomDescription("Attribute.Interfaces_463")] July = 7,
      /// <summary>Август</summary>
      [CustomDescription("Attribute.Interfaces_464")] August = 8,
      /// <summary>Сентябрь</summary>
      [CustomDescription("Attribute.Interfaces_465")] September = 9,
      /// <summary>Октябрь</summary>
      [CustomDescription("Attribute.Interfaces_466")] October = 10, // 0x0000000A
      /// <summary>Ноябрь</summary>
      [CustomDescription("Attribute.Interfaces_467")] November = 11, // 0x0000000B
      /// <summary>Декабрь</summary>
      [CustomDescription("Attribute.Interfaces_468")] December = 12, // 0x0000000C
    }
}
