
// Type: Intermech.ComputeValueModes
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech
{
    /// <summary>
    /// Вычисляемы параметр или нет:
    /// 0 - обычный;
    /// 1 - вычисляемый с хранением в базе;
    /// 2 - вычисляемый "на лету".
    /// </summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_183")]
    [Category("Misc")]
    public enum ComputeValueModes
    {
      /// <summary>Невычисляемый атрибут</summary>
      [CustomDescription("Attribute.Interfaces_184")] NotComputableValue,
      /// <summary>Атрибут вычисляется в момент изменения данных</summary>
      [CustomDescription("Attribute.Interfaces_185")] StoredValue,
      /// <summary>Атрибут вычисляется в момент чтения данных</summary>
      [CustomDescription("Attribute.Interfaces_186")] JITValue,
      /// <summary>Нормализованный индекс</summary>
      [CustomDescription("Attribute.Interfaces_187")] IndexValue,
    }
}
