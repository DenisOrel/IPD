
// Type: Intermech.ObjectRecordKind
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech
{
    /// <summary>
    /// Информация о записи в списке объектов (содержимое F_OBJECT_VER_TYPE)
    /// </summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_63")]
    [Category("Misc")]
    public enum ObjectRecordKind
    {
      /// <summary>Заготовка</summary>
      [CustomDescription("Attribute.Interfaces_64")] Blank = -1, // 0xFFFFFFFF
      /// <summary>Объект</summary>
      [CustomDescription("Attribute.Interfaces_65")] Object = 0,
      /// <summary>Импортируемый объект</summary>
      [CustomDescription("Attribute.Interfaces_66")] Import = 1,
    }
}
