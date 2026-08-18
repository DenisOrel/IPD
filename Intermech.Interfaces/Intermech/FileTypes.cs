
// Type: Intermech.FileTypes
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech
{
    /// <summary>Тип файла в файловом шкафу</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.FileType")]
    [Category("Misc")]
    public enum FileTypes
    {
      /// <summary>Файл объекта</summary>
      [ColorFileTypes("Black"), CustomDescription("FileTypes.ftNormal")] ftNormal,
      /// <summary>Файл, не влияющий на подписи объекта</summary>
      [ColorFileTypes("Blue"), CustomDescription("FileTypes.ftNotContent")] ftNotContent,
      /// <summary>Файл ОТД</summary>
      [ColorFileTypes("Blue"), CustomDescription("FileTypes.ftOTD")] ftOTD,
      /// <summary>Файл замечаний</summary>
      [ColorFileTypes("Red"), CustomDescription("FileTypes.ftRedlining")] ftRedlining,
      /// <summary>Аутентичный файл</summary>
      [ColorFileTypes("Green", "SlateBlue"), CustomDescription("FileTypes.ftAuthentical")] ftAuthentical,
      /// <summary>Неизвестный тип файла</summary>
      [ColorFileTypes("Purple", "SlateBlue"), CustomDescription("FileTypes.ftUnknown")] ftUnknown,
    }
}
