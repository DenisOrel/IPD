
// Type: Intermech.Interfaces.CompositionTracking.CompositionTrackingMode
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech.Interfaces.CompositionTracking
{
    /// <summary>Composition track modes</summary>
    /// <remarks>Оставил для совместитмости / конвертации старых настроек</remarks>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_1")]
    [Category("Misc")]
    public enum CompositionTrackingMode
    {
      [CustomDescription("Attribute.Interfaces_2")] ctmNone,
      [CustomDescription("Attribute.Interfaces_3")] ctmTrackig,
    }
}
