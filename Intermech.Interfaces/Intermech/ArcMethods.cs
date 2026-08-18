
// Type: Intermech.ArcMethods
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech
{
    /// <summary>Метод упаковки информации в BLOB-поле</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_84")]
    [Category("Misc")]
    public enum ArcMethods
    {
      /// <summary>Без сжатия</summary>
      [CustomDescription("Attribute.Interfaces_85")] NotPacked,
      /// <summary>Информация упакована с помощью библиотеки ZLib</summary>
      [Description("ZLib")] ZLibPacked,
    }
}
