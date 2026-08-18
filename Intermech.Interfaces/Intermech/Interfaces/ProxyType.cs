
// Type: Intermech.Interfaces.ProxyType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech.Interfaces
{
    /// <summary>Тип прокси-сервера.</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [Serializable]
    public enum ProxyType
    {
      /// <summary> Нет </summary>
      [CustomDescription("Attribute.Interfaces_549")] None,
      /// <summary> HTTP </summary>
      [CustomDescription("Attribute.Interfaces_550")] HTTP,
      /// <summary> SOCKS 4 </summary>
      [CustomDescription("Attribute.Interfaces_551")] SOCKS4,
      /// <summary> SOCKS 5 </summary>
      [CustomDescription("Attribute.Interfaces_552")] SOCKS5,
    }
}
