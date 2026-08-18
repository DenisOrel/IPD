
// Type: Intermech.UniqueValueModes
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech
{
    /// <summary>
    /// Контроль уникальности атрибута в пределах базы:
    /// 0 - не контролировать уникальность,
    /// 1 - контролировать среди объектов данного типа,
    /// 2 - контролировать среди объектов данного типа и версий данного объекта,
    /// 3 - контролировать среди всех объектов и их версий,
    /// </summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_192")]
    [Category("Misc")]
    public enum UniqueValueModes
    {
      /// <summary>Не контролировать уникальность</summary>
      [CustomDescription("Attribute.Interfaces_193")] NotUnique,
      /// <summary>
      /// Контролировать уникальность среди объектов данного типа
      /// </summary>
      [CustomDescription("Attribute.Interfaces_194")] TypeOnly,
      /// <summary>
      /// Контролировать уникальность среди объектов данного типа и версий данного объекта
      /// </summary>
      [CustomDescription("Attribute.Interfaces_195")] VerTypeOnly,
      /// <summary>
      /// Контролировать уникальность среди всех объектов и их версий
      /// </summary>
      [CustomDescription("Attribute.Interfaces_196")] AllVerTypes,
    }
}
