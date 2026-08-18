
// Type: Intermech.ApplicabilityModes
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech
{
    /// <summary>
    /// Обязательность связи.
    /// -1 - данной связи существовать не может;
    /// 0 - дочерний объект может быть создан без данной связи;
    /// 1 - дочерний объект должен быть создан в контексте данной связи
    /// (например, объект строительства может быть создан только в контексте стройки);
    /// 2 - дочерний объект должен быть создан в контексте одной из таких связей
    /// (например, папка классификатора должна входить или в сам классификатор, или в
    /// другую папку классификатора).
    /// </summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_79")]
    [Category("Misc")]
    public enum ApplicabilityModes
    {
      /// <summary>Запрещенная связь</summary>
      [CustomDescription("Attribute.Interfaces_80")] Disabled = -1, // 0xFFFFFFFF
      /// <summary>Разрешенная связь</summary>
      [CustomDescription("Attribute.Interfaces_81")] Enabled = 0,
      /// <summary>Обязательная связь</summary>
      [CustomDescription("Attribute.Interfaces_82")] Required = 1,
      /// <summary>Одна из обязательных связей</summary>
      [CustomDescription("Attribute.Interfaces_83")] AnyRequired = 2,
    }
}
