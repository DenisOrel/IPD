
// Type: Intermech.Interfaces.BlobProcessorMode
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech.Interfaces
{
    /// <summary>Состояние, в котором находится обработчик BLOB-поля</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Client.Core_195")]
    [Category("Misc")]
    public enum BlobProcessorMode
    {
      /// <summary>Состояние неизвестно</summary>
      [Description("")] Unknown,
      /// <summary>Чтение</summary>
      [CustomDescription("Attribute.Client.Core_196")] Read,
      /// <summary>Запись</summary>
      [CustomDescription("Attribute.Client.Core_197")] Write,
      /// <summary>Упаковка</summary>
      [CustomDescription("Attribute.Client.Core_198")] Pack,
      /// <summary>Распаковка</summary>
      [CustomDescription("Attribute.Client.Core_199")] Unpack,
    }
}
