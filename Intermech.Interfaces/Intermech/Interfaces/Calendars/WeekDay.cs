
// Type: Intermech.Interfaces.Calendars.WeekDay
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech.Interfaces.Calendars
{
    /// <summary>День недели.</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [Serializable]
    public enum WeekDay
    {
      /// <summary>Понедельник</summary>
      [CustomDescription("Attribute.Interfaces_469")] Monday = 1,
      /// <summary>Вторник</summary>
      [CustomDescription("Attribute.Interfaces_470")] Tuesday = 2,
      /// <summary>Среда</summary>
      [CustomDescription("Attribute.Interfaces_471")] Wednesday = 3,
      /// <summary>Четверг</summary>
      [CustomDescription("Attribute.Interfaces_472")] Thursday = 4,
      /// <summary>Пятница</summary>
      [CustomDescription("Attribute.Interfaces_473")] Friday = 5,
      /// <summary>Суббота</summary>
      [CustomDescription("Attribute.Interfaces_474")] Saturday = 6,
      /// <summary>Воскресенье</summary>
      [CustomDescription("Attribute.Interfaces_475")] Sunday = 7,
    }
}
