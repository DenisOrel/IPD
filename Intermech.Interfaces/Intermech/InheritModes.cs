
// Type: Intermech.InheritModes
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech
{
    /// <summary>
    /// Режимы наследования атрибутов от типов объектов:
    /// 0 - не передается дочерним типам (собственный атрибут);
    /// 1 - передается дочерним типам;
    /// 2 - унаследован от родительского типа;
    /// </summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_90")]
    [Category("Misc")]
    public enum InheritModes
    {
      /// <summary>Собственный</summary>
      [CustomDescription("Attribute.Interfaces_91")] Private,
      /// <summary>Общий</summary>
      [CustomDescription("Attribute.Interfaces_92")] Public,
      /// <summary>Унаследован</summary>
      [CustomDescription("Attribute.Interfaces_93")] Inherited,
    }
}
