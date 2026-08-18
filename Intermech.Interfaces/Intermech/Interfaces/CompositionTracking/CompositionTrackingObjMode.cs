
// Type: Intermech.Interfaces.CompositionTracking.CompositionTrackingObjMode
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech.Interfaces.CompositionTracking
{
    /// <summary>Режим обработки "дочерних" / "вложенных" объектов</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_450")]
    [Category("Misc")]
    [Flags]
    public enum CompositionTrackingObjMode
    {
      [CustomDescription("Attribute.Interfaces_2")] ctcNone = 0,
      [CustomDescription("Attribute.Interfaces_451")] ctomProceed = 1,
      [CustomDescription("Attribute.Interfaces_452")] ctomContext = 2,
      [CustomDescription("Attribute.Interfaces_453")] ctomAll = 4,
    }
}
