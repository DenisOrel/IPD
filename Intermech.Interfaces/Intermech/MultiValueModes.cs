
// Type: Intermech.MultiValueModes
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech
{
    /// <summary>
    /// Режимы работы со списковыми параметрами
    /// 0 - параметр может иметь только одно значение.
    /// 1 - параметр может содержать список значений.
    /// 2 - параметр может содержать только одно значение из списка предустановленных значений.
    /// 3 - параметр может содержать подмножество значений из списка допустимых значений.
    /// </summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_121")]
    [Category("Misc")]
    public enum MultiValueModes
    {
      /// <summary>Атрибут может содержать одно значение</summary>
      [CustomDescription("Attribute.Interfaces_122")] SingleValue,
      /// <summary>Атрибут может содержать множество значений</summary>
      [CustomDescription("Attribute.Interfaces_123")] MultiValues,
      /// <summary>
      /// Атрибут может содержать одно значение из списка разрешенных значений
      /// </summary>
      [CustomDescription("Attribute.Interfaces_124")] SingleValueFromList,
      /// <summary>
      /// Атрибут может содержать множество значений из списка разрешенных значений
      /// </summary>
      [CustomDescription("Attribute.Interfaces_125")] MultiValuesFromList,
    }
}
