
// Type: Intermech.OptimizationModes
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech
{
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_98")]
    [Category("Misc")]
    public enum OptimizationModes
    {
      /// <summary>Запись атрибута</summary>
      [CustomDescription("Attribute.Interfaces_99")] Write,
      /// <summary>Чтение атрибута</summary>
      [CustomDescription("Attribute.Interfaces_100")] Read,
      /// <summary>Сортировка и поиск по атрибуту</summary>
      [CustomDescription("Attribute.Interfaces_101")] Seek,
      /// <summary>Атрибут отсутствует</summary>
      [CustomDescription("Attribute.Interfaces_102")] NotFound,
    }
}
