
// Type: Intermech.InheritModesHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech
{
    public class InheritModesHelper
    {
      public static string GetCaption(InheritModes mode) => EnumTypeHelper.GetCaption((Enum) mode);

      public static InheritModes GetInheritMode(string s)
      {
        return (InheritModes) EnumTypeHelper.GetEnumValue(typeof (InheritModes), s);
      }
    }
}
