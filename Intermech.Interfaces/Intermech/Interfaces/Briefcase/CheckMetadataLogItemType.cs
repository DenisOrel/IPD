
// Type: Intermech.Interfaces.Briefcase.CheckMetadataLogItemType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech.Interfaces.Briefcase
{
    /// <summary>
    /// Тип записи
    /// (ошибка, предупреждение,
    /// предупреждение об возможной потере данных, предупреждение об возможном изменении системного атрибута)
    /// </summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces.Briefcase_1")]
    [Category("Misc")]
    public enum CheckMetadataLogItemType
    {
      [CustomDescription("Attribute.Interfaces.Briefcase_2")] Error = 0,
      [CustomDescription("Attribute.Interfaces.Briefcase_3")] WarningSystem = 10, // 0x0000000A
      [CustomDescription("Attribute.Interfaces.Briefcase_4")] WarningLostData = 20, // 0x00000014
      [CustomDescription("Attribute.Interfaces.Briefcase_5")] Warning = 30, // 0x0000001E
      [CustomDescription("Attribute.Interfaces.Briefcase_6")] Information = 40, // 0x00000028
    }
}
