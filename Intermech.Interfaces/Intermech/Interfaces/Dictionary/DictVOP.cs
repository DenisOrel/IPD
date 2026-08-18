
// Type: Intermech.Interfaces.Dictionary.DictVOP
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech.Interfaces.Dictionary
{
    [TypeConverter(typeof (EnumDescConverter))]
    public enum DictVOP
    {
      [CustomDescription("Attribute.Interfaces_7")] Value,
      [CustomDescription("Attribute.Interfaces_8")] Div,
      [CustomDescription("Attribute.Interfaces_9")] Mod,
    }
}
