
// Type: Intermech.EnumTypeHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech
{
    public class EnumTypeHelper
    {
      public static string GetCaption(Enum value) => EnumDescConverter.GetEnumDescription(value);

      public static Enum GetEnumValue(Type type, string s)
      {
        return EnumTypeHelper.GetEnumValue(type, s, (object) null);
      }

      public static Enum GetEnumValue(Type type, string s, object defaultValue)
      {
        return (Enum) EnumDescConverter.GetEnumValue(type, s, defaultValue);
      }

      public static string GetDescription(Type type) => EnumDescConverter.GetTypeDescription(type);
    }
}
