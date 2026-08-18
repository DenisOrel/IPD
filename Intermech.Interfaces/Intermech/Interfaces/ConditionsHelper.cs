
// Type: Intermech.Interfaces.ConditionsHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Вспомогательный статический класс, позволяющий формировать
    /// условия запросов к ядру (ConditionStructure) по основным
    /// критериям правила подбора версий объектов
    /// </summary>
    public static class ConditionsHelper
    {
      /// <summary>Вспомогательный класс</summary>
      private static CompareFunctionsHelper cfHelper = new CompareFunctionsHelper();

      /// <summary>
      /// Подготовить параметры запроса к ядру по основным критериям
      /// указанного правила подбора версий
      /// </summary>
      /// <param name="rule">Правило подбора версий</param>
      /// <param name="modificationID">Номер взаимосвязанного контекста редактирования</param>
      /// <returns>Параметры запроса к ядру или null, если один из критериев нельзя преобразовать в параметры запроса</returns>
      public static List<ConditionStructure> CreateStructures(VersionsRule rule, long modificationID)
      {
        List<ConditionStructure> structures = new List<ConditionStructure>();
        if (rule == null || rule.Criterions.Count == 0)
          return (List<ConditionStructure>) null;
        if (!string.IsNullOrEmpty(rule.RuleObjectGuid) && (rule.RuleObjectGuid == "cad00601-306c-11d8-b4e9-00304f19f545" || rule.RuleObjectGuid == "cad001e2-306c-11d8-b4e9-00304f19f545"))
        {
          int groupID = 0;
          if (modificationID != 0L)
          {
            structures.Add(new ConditionStructure(-15, RelationalOperators.Equal, (object) modificationID, (object) null, LogicalOperators.OR, 1, true, AttributeSourceTypes.Object, ColumnContents.Text));
            groupID = -1;
          }
          structures.Add(new ConditionStructure(-16, RelationalOperators.Equal, (object) 1L, (object) null, LogicalOperators.NONE, groupID, true, AttributeSourceTypes.Object, ColumnContents.Text));
          return structures;
        }
        List<VersionsRuleCriterion> mainCriterions = rule.GetMainCriterions();
        VersionsRuleCriterion advancedCriterion = rule.GetAdvancedCriterion();
        if (advancedCriterion.CompareFunction == "BASEVERSION")
          mainCriterions.Add(advancedCriterion);
        VersionsRuleCriterion versionsRuleCriterion = (VersionsRuleCriterion) null;
        for (int index = 0; index < mainCriterions.Count; ++index)
        {
          int num = 0;
          ConditionStructure conditionStructure = ConditionsHelper.cfHelper.CriterionToConditionStructure(mainCriterions[index]);
          if (conditionStructure.Attribute == null)
            return (List<ConditionStructure>) null;
          if (mainCriterions[index].BoolFunction == "AND" && (versionsRuleCriterion == null || versionsRuleCriterion.BoolFunction != "AND") && index < mainCriterions.Count - 1)
            num = 1;
          if (mainCriterions[index].BoolFunction != "AND" && versionsRuleCriterion != null && versionsRuleCriterion.BoolFunction == "AND")
            num = -1;
          conditionStructure.GroupID = num;
          structures.Add(conditionStructure);
          versionsRuleCriterion = mainCriterions[index];
        }
        if (structures.Count > 1)
        {
          ConditionStructure conditionStructure1 = structures[0];
          ++conditionStructure1.GroupID;
          structures[0] = conditionStructure1;
          ConditionStructure conditionStructure2 = structures[structures.Count - 1];
          --conditionStructure2.GroupID;
          structures[structures.Count - 1] = conditionStructure2;
        }
        return structures;
      }
    }
}
