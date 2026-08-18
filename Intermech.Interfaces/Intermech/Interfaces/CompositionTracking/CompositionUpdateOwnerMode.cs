
// Type: Intermech.Interfaces.CompositionTracking.CompositionUpdateOwnerMode
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech.Interfaces.CompositionTracking
{
    /// <summary>Режим "обновления" родительского объекта</summary>
    /// <remarks>Измнение даты модификации родительского объекта при изменении дочернего объекта</remarks>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_535")]
    [Category("Misc")]
    [Flags]
    public enum CompositionUpdateOwnerMode
    {
      [CustomDescription("Attribute.Interfaces_2")] None = 0,
      [CustomDescription("Attribute.Interfaces_536")] BaseVersion = 1,
      [CustomDescription("Attribute.Interfaces_537")] Context = 2,
    }
}
