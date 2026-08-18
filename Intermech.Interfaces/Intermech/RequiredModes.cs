
// Type: Intermech.RequiredModes
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech
{
    /// <summary>
    /// 0 - атрибут может быть добавлен для объектов (связей) данного типа вручную
    /// (и может быть затем удален);
    /// 1 - атрибут добавляется автоматически, но может быть удален.
    /// 2 - атрибут добавляется автоматически и не может быть удален.
    /// </summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_94")]
    [Category("Misc")]
    public enum RequiredModes
    {
      /// <summary>Атрибут может быть добавлен вручную</summary>
      [CustomDescription("Attribute.Interfaces_95")] Manual,
      /// <summary>Необязательный атрибут, добавляемый автоматически</summary>
      [CustomDescription("Attribute.Interfaces_96")] Auto,
      /// <summary>Обязательный атрибут, добавляемый автоматически</summary>
      [CustomDescription("Attribute.Interfaces_97")] AutoRequired,
    }
}
