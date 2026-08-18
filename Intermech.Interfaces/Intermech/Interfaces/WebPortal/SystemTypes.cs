
// Type: Intermech.Interfaces.WebPortal.SystemTypes
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.ComponentModel;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Система узла</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [Description("Система узла")]
    [Category("Misc")]
    public enum SystemTypes
    {
      [Description("Неизвестно")] Unknown = -1, // 0xFFFFFFFF
      [Description("SEARCH")] Search = 0,
      [Description("IPS")] IPS = 1,
    }
}
