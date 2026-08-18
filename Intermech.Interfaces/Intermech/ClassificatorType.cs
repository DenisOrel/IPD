
// Type: Intermech.ClassificatorType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech
{
    /// <summary>Принадлежность классификатора</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_139")]
    [Category("Misc")]
    public enum ClassificatorType
    {
      /// <summary>Не задана</summary>
      [CustomDescription("Attribute.Interfaces_140")] None,
      /// <summary>Архивы</summary>
      [CustomDescription("Attribute.Interfaces_141")] Archiv,
      /// <summary>Все архивы</summary>
      [CustomDescription("Attribute.Interfaces_142")] Archives,
      /// <summary>Типы объектов</summary>
      [CustomDescription("Attribute.Interfaces_143")] ObjectType,
      /// <summary>Все типы объектов</summary>
      [CustomDescription("Attribute.Interfaces_144")] ObjectTypes,
    }
}
