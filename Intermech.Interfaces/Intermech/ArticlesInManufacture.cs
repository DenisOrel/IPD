
// Type: Intermech.ArticlesInManufacture
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech
{
    /// <summary>Учет изделий в производстве</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_275")]
    [Category("Misc")]
    public enum ArticlesInManufacture
    {
      [CustomDescription("Attribute.Interfaces_276")] Parties,
      [CustomDescription("Attribute.Interfaces_277")] Instances,
    }
}
