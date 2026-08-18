
// Type: Intermech.Interfaces.LCStepLinkOptions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech.Interfaces
{
    /// <summary>Опции связи между шагами ЖЦ</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("LCStepLinkOptions")]
    [Category("Misc")]
    [Flags]
    public enum LCStepLinkOptions
    {
      /// <summary>Нет опций</summary>
      [CustomDescription("Attribute.Interfaces_180")] None = 0,
      /// <summary>Автоматически переводить предыдущую версию на шаг</summary>
      [CustomDescription("AutoTransfer")] AutoTransfer = 1,
    }
}
