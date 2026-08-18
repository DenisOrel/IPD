
// Type: Intermech.LCAccessTypes
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech
{
    /// <summary>
    /// Типы контроля прав доступа к объектам на соответствующих этапах ЖЦ
    /// 0 - контроль прав не производится,
    /// 1 - контроль только по правам ЖЦ (без возможности индивидуального назначения
    /// прав).
    /// 2 - контроль по ЖЦ и персонально объекту (но без возможности передачи прав
    /// доступа по наследству),
    /// 3 - то же, но с возможностью публиковать права подтипам.
    /// </summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_250")]
    [Category("Access")]
    public enum LCAccessTypes
    {
      [CustomDescription("Attribute.Interfaces_251")] NoCheck,
      [CustomDescription("Attribute.Interfaces_252")] CheckLCOnly,
      [CustomDescription("Attribute.Interfaces_253")] CheckAll,
    }
}
