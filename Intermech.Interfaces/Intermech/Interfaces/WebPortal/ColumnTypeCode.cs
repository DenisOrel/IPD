
// Type: Intermech.Interfaces.WebPortal.ColumnTypeCode
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Типы данных в колонках</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Interfaces_Portal_7")]
    [Serializable]
    public enum ColumnTypeCode
    {
      tcEmpty = 0,
      tcObject = 1,
      tcDBNull = 2,
      tcBoolean = 3,
      tcChar = 4,
      tcSByte = 5,
      tcByte = 6,
      tcInt16 = 7,
      tcUInt16 = 8,
      tcInt32 = 9,
      tcUInt32 = 10, // 0x0000000A
      tcInt64 = 11, // 0x0000000B
      tcUInt64 = 12, // 0x0000000C
      tcSingle = 13, // 0x0000000D
      tcDouble = 14, // 0x0000000E
      tcDecimal = 15, // 0x0000000F
      tcDateTime = 16, // 0x00000010
      tcString = 18, // 0x00000012
    }
}
