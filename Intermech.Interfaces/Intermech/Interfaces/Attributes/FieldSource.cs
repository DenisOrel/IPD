
// Type: Intermech.Interfaces.Attributes.FieldSource
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.ComponentModel;


namespace Intermech.Interfaces.Attributes
{
    /// <summary>Источник данных поля записи AVS</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [Serializable]
    public enum FieldSource
    {
      /// <summary>Атрибут связи</summary>
      [Description("Атрибут связи")] Relation,
      /// <summary>Атрибут объекта</summary>
      [Description("Атрибут объекта")] Object,
      /// <summary>Поле записи в документе</summary>
      [Description("Поле записи в документе")] DocumentRowField,
    }
}
