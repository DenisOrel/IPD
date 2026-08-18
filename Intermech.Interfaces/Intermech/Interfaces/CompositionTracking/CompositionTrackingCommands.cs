
// Type: Intermech.Interfaces.CompositionTracking.CompositionTrackingCommands
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech.Interfaces.CompositionTracking
{
    /// <summary>Composition tracking commands (settings)</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_1")]
    [Category("Misc")]
    [Flags]
    public enum CompositionTrackingCommands
    {
      [CustomDescription("Attribute.Interfaces_2")] ctcNone = 0,
      [CustomDescription("Attribute.Interfaces_446")] ctcCheckOut = 1,
      [CustomDescription("Attribute.Interfaces_447")] ctcUndoCheckOut = 2,
      [CustomDescription("Attribute.Interfaces_448")] ctcCheckin = 4,
      [CustomDescription("Attribute.Interfaces_449")] ctcNextLCStep = 8,
      [CustomDescription("Attribute.Interfaces_558")] ctcCreateVersion = 16, // 0x00000010
    }
}
