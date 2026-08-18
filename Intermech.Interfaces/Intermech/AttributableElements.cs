
// Type: Intermech.AttributableElements
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech
{
    /// <summary>Вид элемента</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_271")]
    [Category("Misc")]
    public enum AttributableElements
    {
      /// <summary>Информация о виде элемента недоступна</summary>
      [CustomDescription("Attribute.Interfaces_272")] None,
      /// <summary>Объект</summary>
      [CustomDescription("Attribute.Interfaces_273")] Object,
      /// <summary>Связь</summary>
      [CustomDescription("Attribute.Interfaces_274")] Relation,
      /// <summary>Итерация</summary>
      [CustomDescription("Attribute.Interfaces_505")] Snapshot,
    }
}
