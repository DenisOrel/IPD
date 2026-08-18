
// Type: Intermech.Interfaces.LanguageCodes
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Коды языков.
    /// В буквенном обозначении код языка по ISO 639-3, в значении: цифровое обозначение языка по ГОСТ 7.75-97
    /// </summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Interfaces_Languages")]
    [Category("Misc")]
    public enum LanguageCodes
    {
      /// <summary>Английский</summary>
      [CustomDescription("Interfaces_Languages_17")] ENG = 45, // 0x0000002D
      /// <summary>Белорусский</summary>
      [CustomDescription("Interfaces_Languages_41")] BEL = 90, // 0x0000005A
      /// <summary>Испанский</summary>
      [CustomDescription("Interfaces_Languages_117")] SPA = 230, // 0x000000E6
      /// <summary>Итальянский</summary>
      [CustomDescription("Interfaces_Languages_118")] ITA = 235, // 0x000000EB
      /// <summary>Казахский</summary>
      [CustomDescription("Interfaces_Languages_125")] KAZ = 255, // 0x000000FF
      /// <summary>Китайский</summary>
      [CustomDescription("Interfaces_Languages_142")] ZHO = 315, // 0x0000013B
      /// <summary>Латышский</summary>
      [CustomDescription("Interfaces_Languages_168")] LAV = 385, // 0x00000181
      /// <summary>Немецкий</summary>
      [CustomDescription("Interfaces_Languages_215")] DEU = 481, // 0x000001E1
      /// <summary>Польский</summary>
      [CustomDescription("Interfaces_Languages_244")] POL = 540, // 0x0000021C
      /// <summary>Русский</summary>
      [CustomDescription("Interfaces_Languages_255")] RUS = 570, // 0x0000023A
      /// <summary>Украинский</summary>
      [CustomDescription("Interfaces_Languages_328")] UKR = 720, // 0x000002D0
      /// <summary>Французский</summary>
      [CustomDescription("Interfaces_Languages_339")] FRA = 745, // 0x000002E9
      /// <summary>Эстонский</summary>
      [CustomDescription("Interfaces_Languages_382")] EST = 850, // 0x00000352
    }
}
