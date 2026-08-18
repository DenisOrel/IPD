
// Type: Intermech.ObjectsClassifyType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech
{
    /// <summary>Типы классификации объектов</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_126")]
    [Category("Misc")]
    public enum ObjectsClassifyType
    {
      /// <summary>Не классифицировать</summary>
      [CustomDescription("Attribute.Interfaces_127")] None,
      /// <summary>Выборочная классификация</summary>
      [CustomDescription("Attribute.Interfaces_128")] Selective,
      /// <summary>Обязательная классификация</summary>
      [CustomDescription("Attribute.Interfaces_129")] Obligatory,
    }
}
