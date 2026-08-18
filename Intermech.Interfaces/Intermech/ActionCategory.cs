
// Type: Intermech.ActionCategory
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech
{
    /// <summary>
    /// Категория действия (чтение, модификация, администрирование) -
    /// предназначена для группировки действий в диалогах назначения прав доступа
    /// </summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_245")]
    [Category("Access")]
    public enum ActionCategory
    {
      /// <summary>Не определена</summary>
      [CustomDescription("Attribute.Interfaces_246")] NotDefined,
      /// <summary>Читать</summary>
      [CustomDescription("Attribute.Interfaces_247")] Read,
      /// <summary>Изменять</summary>
      [CustomDescription("Attribute.Interfaces_248")] Write,
      /// <summary>Администрировать</summary>
      [CustomDescription("Attribute.Interfaces_249")] Admin,
    }
}
