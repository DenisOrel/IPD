
// Type: Intermech.Interfaces.WebPortal.PublishAttributeCategory
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Тип атрибута опубликованного объекта</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Interfaces_Portal_1")]
    [Serializable]
    public enum PublishAttributeCategory
    {
      [CustomDescription("Interfaces_Portal_2")] Auto,
      /// <summary>Aтрибуты опубликованного объекта</summary>
      [CustomDescription("Interfaces_Portal_3")] PublishObject,
      /// <summary>Aтрибуты объекта</summary>
      [CustomDescription("Interfaces_Portal_4")] Object,
      /// <summary>Aтрибуты опубликованной связи</summary>
      [CustomDescription("Interfaces_Portal_5")] PublishRelation,
      /// <summary>Aтрибуты связи</summary>
      [CustomDescription("Interfaces_Portal_6")] Relation,
    }
}
