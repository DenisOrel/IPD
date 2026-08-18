
// Type: Intermech.Kernel.Search.ConditionStructureHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;


namespace Intermech.Kernel.Search
{
    /// <summary>
    /// Хелпер для удобного формирования массивов условий ConditionStructure[]
    /// </summary>
    public class ConditionStructureHelper
    {
      public static ConditionStructure[] GetConditions(string conditionsStr, ICollection<object> values)
      {
        return new List<ConditionStructure>().ToArray();
      }

      public static ConditionStructure[] GetConditions(string conditionsStr)
      {
        return ConditionStructureHelper.GetConditions(conditionsStr, (ICollection<object>) null);
      }
    }
}
