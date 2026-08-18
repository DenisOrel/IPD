
// Type: Intermech.Interfaces.Dictionary.DictROP
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech.Interfaces.Dictionary
{
    [TypeConverter(typeof (EnumDescConverter))]
    public enum DictROP
    {
      [CustomDescription("Attribute.Interfaces_11")] Equal,
      [CustomDescription("Attribute.Interfaces_12")] NotEqual,
      [CustomDescription("Attribute.Interfaces_13")] More,
      [CustomDescription("Attribute.Interfaces_14")] NotMore,
      [CustomDescription("Attribute.Interfaces_15")] MoreOrEqual,
      [CustomDescription("Attribute.Interfaces_16")] Less,
      [CustomDescription("Attribute.Interfaces_17")] NotLess,
      [CustomDescription("Attribute.Interfaces_18")] LessOrEqual,
      [CustomDescription("Attribute.Interfaces_19")] In,
      [CustomDescription("Attribute.Interfaces_20")] NotIn,
    }
}
