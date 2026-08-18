
// Type: Intermech.Kernel.Search.AttributeSourceTypes
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech.Kernel.Search
{
    /// <summary>Принадлежность атрибута</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_427")]
    [Category("SQL")]
    public enum AttributeSourceTypes
    {
      /// <summary>Источник не указан</summary>
      [CustomDescription("Attribute.Interfaces_428")] Auto,
      /// <summary>Атрибут объекта</summary>
      [CustomDescription("Attribute.Interfaces_429")] Object,
      /// <summary>Атрибут связи</summary>
      [CustomDescription("Attribute.Interfaces_430")] Relation,
      /// <summary>Атрибут событий</summary>
      [CustomDescription("Attribute.Interfaces_431")] Events,
      /// <summary>Атрибут истории значений</summary>
      [CustomDescription("Attribute.Interfaces_432")] History,
      /// <summary>Атрибут файлового шкафа</summary>
      [CustomDescription("Attribute.Interfaces_433")] FileStorage,
      /// <summary>Атрибут итерации</summary>
      [CustomDescription("Attribute.Interfaces_559")] Snapshot,
      /// <summary>Прочие атрибуты</summary>
      [CustomDescription("Attribute.Interfaces_434")] Other,
    }
}
