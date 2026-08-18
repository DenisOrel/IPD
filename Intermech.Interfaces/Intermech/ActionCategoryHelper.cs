
// Type: Intermech.ActionCategoryHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech
{
    public class ActionCategoryHelper
    {
      public static string GetCaption(ActionCategory category)
      {
        return EnumTypeHelper.GetCaption((Enum) category);
      }

      public static ActionCategory GetActionCategory(string s)
      {
        return (ActionCategory) EnumTypeHelper.GetEnumValue(typeof (ActionCategory), s);
      }
    }
}
