
// Type: Intermech.CheckAccessFlags
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech
{
    /// <summary>Флаги, управляющие проверкой прав доступа</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_239")]
    [Category("Access")]
    [Flags]
    public enum CheckAccessFlags
    {
      /// <summary>Нет проверки</summary>
      None = 0,
      /// <summary>
      /// Генерировать исключение в случае отсутствия прав доступа
      /// </summary>
      [CustomDescription("Attribute.Interfaces_240")] ThrowACException = 1,
      /// <summary>Режим пакетной проверки</summary>
      [CustomDescription("Attribute.Interfaces_241")] BatchCheck = 2,
    }
}
