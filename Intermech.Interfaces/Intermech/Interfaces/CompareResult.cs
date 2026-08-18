
// Type: Intermech.Interfaces.CompareResult
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech.Interfaces
{
    /// <summary>Результат сравнения двух величиин.</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_305")]
    [Category("Misc")]
    public enum CompareResult
    {
      [CustomDescription("Attribute.Interfaces_306")] Equal,
      [CustomDescription("Attribute.Interfaces_307")] More,
      [CustomDescription("Attribute.Interfaces_308")] Less,
      [CustomDescription("Attribute.Interfaces_309")] NotCompatible,
    }
}
