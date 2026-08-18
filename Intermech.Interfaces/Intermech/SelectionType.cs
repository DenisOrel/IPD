
// Type: Intermech.SelectionType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech
{
    /// <summary>Принадлежность выборки</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_130")]
    [Category("Misc")]
    public enum SelectionType
    {
      /// <summary>Не задана</summary>
      [CustomDescription("Attribute.Interfaces_131")] None,
      /// <summary>Архивы</summary>
      [CustomDescription("Attribute.Interfaces_132")] Archiv,
      /// <summary>Все архивы</summary>
      [CustomDescription("Attribute.Interfaces_133")] Archives,
      /// <summary>Типы объектов</summary>
      [CustomDescription("Attribute.Interfaces_134")] ObjectType,
      /// <summary>Все типы объектов</summary>
      [CustomDescription("Attribute.Interfaces_135")] ObjectTypes,
      /// <summary>Контекст</summary>
      [CustomDescription("Attribute.Interfaces_136")] Context,
      /// <summary>Списки объектов</summary>
      [CustomDescription("Attribute.Interfaces_137")] ListObjects,
      /// <summary>Почта</summary>
      [CustomDescription("Attribute.Interfaces_138")] Mail,
      /// <summary>Органайзер</summary>
      [CustomDescription("Intermech_Interfaces_SelectionType_Organizer")] Organizer,
    }
}
