
// Type: Intermech.Interfaces.ClearingMode
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech.Interfaces
{
    /// <summary>Режимы очистки (типы временныех интервалов)</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_58")]
    [Category("Misc")]
    public enum ClearingMode
    {
      [CustomDescription("Attribute.Interfaces_59")] SeveralPerYear,
      [CustomDescription("Attribute.Interfaces_60")] SeveralPerMonth,
      [CustomDescription("Attribute.Interfaces_61")] SeveralPerWeek,
      [CustomDescription("Attribute.Interfaces_62")] SeveralPerDay,
    }
}
