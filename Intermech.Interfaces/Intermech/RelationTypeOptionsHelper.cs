
// Type: Intermech.RelationTypeOptionsHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Text;


namespace Intermech
{
    public class RelationTypeOptionsHelper
    {
      public static string GetCaption(RelationTypeOptions option)
      {
        return EnumTypeHelper.GetCaption((Enum) option);
      }

      public static RelationTypeOptions GetRelationTypeOption(string s)
      {
        return (RelationTypeOptions) EnumTypeHelper.GetEnumValue(typeof (RelationTypeOptions), s);
      }

      public static string GetCaptions(RelationTypeOptions options)
      {
        StringBuilder stringBuilder = new StringBuilder();
        if ((RelationTypeOptions.EnableCycleRelations & options) == RelationTypeOptions.EnableCycleRelations)
          stringBuilder.Append(RelationTypeOptionsHelper.GetCaption(RelationTypeOptions.EnableCycleRelations) + ", ");
        if (stringBuilder.Length > 0)
          stringBuilder.Length -= 2;
        return stringBuilder.ToString();
      }
    }
}
