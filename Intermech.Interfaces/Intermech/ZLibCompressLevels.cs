
// Type: Intermech.ZLibCompressLevels
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech
{
    /// <summary>Уровни упаковки информации с помощью библиотеки ZLib</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_86")]
    [Category("Misc")]
    public enum ZLibCompressLevels
    {
      /// <summary>Без компрессии</summary>
      [CustomDescription("Attribute.Interfaces_87")] NoCompress,
      /// <summary>1</summary>
      [Description("1")] Level1,
      /// <summary>2</summary>
      [Description("2")] Level2,
      /// <summary>3</summary>
      [Description("3")] Level3,
      /// <summary>4</summary>
      [Description("4")] Level4,
      /// <summary>5 - средняя компрессия</summary>
      [CustomDescription("Attribute.Interfaces_88")] LevelNormal,
      /// <summary>6</summary>
      [Description("6")] Level6,
      /// <summary>7</summary>
      [Description("7")] Level7,
      /// <summary>8</summary>
      [Description("8")] Level8,
      /// <summary>9 - максимальная компрессия</summary>
      [CustomDescription("Attribute.Interfaces_89")] LevelMax,
    }
}
