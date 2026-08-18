
// Type: Intermech.Kernel.Search.LogicalOperators
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech.Kernel.Search
{
    /// <summary>Логические операторы</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_367")]
    [Category("SQL")]
    public enum LogicalOperators
    {
      /// <summary>Нет оператора</summary>
      [Description("")] NONE,
      /// <summary>Оператор "ИЛИ"</summary>
      [CustomDescription("Attribute.Interfaces_368")] OR,
      /// <summary>Оператор "И"</summary>
      [CustomDescription("Attribute.Interfaces_369")] AND,
      /// <summary>Оператор "НЕ"</summary>
      [CustomDescription("Attribute.Interfaces_370")] NOT,
    }
}
