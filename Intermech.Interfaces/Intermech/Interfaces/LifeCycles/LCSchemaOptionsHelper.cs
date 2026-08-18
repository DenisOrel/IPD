
// Type: Intermech.Interfaces.LifeCycles.LCSchemaOptionsHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Text;


namespace Intermech.Interfaces.LifeCycles
{
    public class LCSchemaOptionsHelper
    {
      public static string GetCaption(LCSchemaOptions option)
      {
        return EnumTypeHelper.GetCaption((Enum) option);
      }

      public static LCSchemaOptions GetLCSchemaOption(string s)
      {
        return (LCSchemaOptions) EnumTypeHelper.GetEnumValue(typeof (LCSchemaOptions), s);
      }

      public static string GetCaptions(LCSchemaOptions options)
      {
        StringBuilder stringBuilder = new StringBuilder();
        if (stringBuilder.Length > 0)
          stringBuilder.Length -= 2;
        return stringBuilder.ToString();
      }
    }
}
