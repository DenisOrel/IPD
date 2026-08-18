
// Type: Intermech.UniqueValueModesHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech
{
    public class UniqueValueModesHelper
    {
      public static string GetCaption(UniqueValueModes mode) => EnumTypeHelper.GetCaption((Enum) mode);

      public static UniqueValueModes GetUniqueValueMode(string s)
      {
        return (UniqueValueModes) EnumTypeHelper.GetEnumValue(typeof (UniqueValueModes), s);
      }
    }
}
