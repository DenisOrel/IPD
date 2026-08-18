
// Type: Intermech.Interfaces.Objects.SampleFunctions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech.Interfaces.Objects
{
    /// <summary>
    /// Функциональные номера выборок по их назначению. Применяется для создания специальных выборок
    /// на рабочих столах юзеров
    /// </summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_54")]
    [Category("Samples")]
    public enum SampleFunctions
    {
      [Description("")] Common,
      [CustomDescription("Attribute.Interfaces_55")] CheckedOut,
      [CustomDescription("Attribute.Interfaces_56")] MyTrash,
      [CustomDescription("InBoxDocsSample")] InBoxDocs,
    }
}
