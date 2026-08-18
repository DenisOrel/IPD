
// Type: Intermech.Search.EventLogFilters.EventLogFiltersHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.EventLog;
using Intermech.Kernel.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace Intermech.Search.EventLogFilters
{
    public static class EventLogFiltersHelper
    {
      public static Tuple<ActionType, string>[] GetAllEventLogActions()
      {
        List<Tuple<ActionType, string>> tupleList = new List<Tuple<ActionType, string>>();
        tupleList.AddRange(((IEnumerable<Enum>) EventLogFiltersHelper.GetAllEnumValues(typeof (ActionType))).Select<Enum, Tuple<ActionType, string>>((Func<Enum, Tuple<ActionType, string>>) (o => new Tuple<ActionType, string>((ActionType) o, ((ActionType) o).GetDescription<ActionType>()))));
        foreach (DictionaryEntry actionName in Helper.ActionNames)
          tupleList.Add(new Tuple<ActionType, string>(((CategoryValue) actionName.Key).ActionID, (string) actionName.Value));
        return tupleList.ToArray();
      }

      public static Enum[] GetAllEnumValues(Type type)
      {
        return ((IEnumerable<FieldInfo>) type.GetFields(BindingFlags.Static | BindingFlags.Public)).Select<FieldInfo, Enum>((Func<FieldInfo, Enum>) (o => o.GetValue((object) null) as Enum)).ToArray<Enum>();
      }

      public static ConditionStructure[] CreateConditionsFromFilter(EventLogFilter filter)
      {
        List<ConditionStructure> conditionStructureList1 = new List<ConditionStructure>();
        ConditionStructure conditionStructure1;
        if (filter.HasActionCondition && filter.Action.Length != 0)
        {
          Tuple<ActionType, string>[] allEventLogActions = EventLogFiltersHelper.GetAllEventLogActions();
          List<ConditionStructure> conditionStructureList2 = conditionStructureList1;
          conditionStructure1 = new ConditionStructure();
          conditionStructure1.Attribute = (object) ObligatoryObjectAttributes.F_EVENT_TYPE;
          conditionStructure1.RelationalOperator = filter.ActionRelop;
          conditionStructure1.Value = (object) ((IEnumerable<Tuple<ActionType, string>>) allEventLogActions).Where<Tuple<ActionType, string>>((Func<Tuple<ActionType, string>, bool>) (o => ((IEnumerable<string>) filter.Action).Contains<string>(o.Item2))).Select<Tuple<ActionType, string>, ActionType>((Func<Tuple<ActionType, string>, ActionType>) (o => o.Item1)).Distinct<ActionType>().ToArray<ActionType>();
          conditionStructure1.SQL = string.Empty;
          ConditionStructure conditionStructure2 = conditionStructure1;
          conditionStructureList2.Add(conditionStructure2);
        }
        if (filter.HasCategoryCondition)
        {
          List<ConditionStructure> conditionStructureList3 = conditionStructureList1;
          conditionStructure1 = new ConditionStructure();
          conditionStructure1.Attribute = (object) ObligatoryObjectAttributes.F_CATEGORY_TYPE;
          conditionStructure1.RelationalOperator = filter.CategoryRelop;
          conditionStructure1.Value = (object) filter.Category;
          conditionStructure1.SQL = string.Empty;
          ConditionStructure conditionStructure3 = conditionStructure1;
          conditionStructureList3.Add(conditionStructure3);
        }
        if (filter.HasCategoryIDCondition)
        {
          List<ConditionStructure> conditionStructureList4 = conditionStructureList1;
          conditionStructure1 = new ConditionStructure();
          conditionStructure1.Attribute = (object) ObligatoryObjectAttributes.F_CATEGORY_ID;
          conditionStructure1.RelationalOperator = filter.CategoryIDRelop;
          conditionStructure1.Value = (object) filter.CategoryID;
          conditionStructure1.SQL = string.Empty;
          ConditionStructure conditionStructure4 = conditionStructure1;
          conditionStructureList4.Add(conditionStructure4);
        }
        if (filter.HasCommentCondition)
        {
          List<ConditionStructure> conditionStructureList5 = conditionStructureList1;
          conditionStructure1 = new ConditionStructure();
          conditionStructure1.Attribute = (object) ObligatoryObjectAttributes.F_NOTE;
          conditionStructure1.RelationalOperator = filter.CommentRelop;
          conditionStructure1.Value = (object) filter.Comment;
          conditionStructure1.SQL = string.Empty;
          ConditionStructure conditionStructure5 = conditionStructure1;
          conditionStructureList5.Add(conditionStructure5);
        }
        if (filter.HasEventEndCondition)
        {
          List<ConditionStructure> conditionStructureList6 = conditionStructureList1;
          conditionStructure1 = new ConditionStructure();
          conditionStructure1.Attribute = (object) ObligatoryObjectAttributes.F_END_DATE;
          conditionStructure1.RelationalOperator = filter.EventEndRelop;
          conditionStructure1.Value = (object) filter.EventEnd;
          conditionStructure1.SQL = string.Empty;
          ConditionStructure conditionStructure6 = conditionStructure1;
          conditionStructureList6.Add(conditionStructure6);
        }
        if (filter.HasEventIDCondition)
        {
          List<ConditionStructure> conditionStructureList7 = conditionStructureList1;
          conditionStructure1 = new ConditionStructure();
          conditionStructure1.Attribute = (object) ObligatoryObjectAttributes.F_EVENT_ID;
          conditionStructure1.RelationalOperator = filter.EventIDRelop;
          conditionStructure1.Value = (object) filter.EventID;
          conditionStructure1.SQL = string.Empty;
          ConditionStructure conditionStructure7 = conditionStructure1;
          conditionStructureList7.Add(conditionStructure7);
        }
        if (filter.HasEventStartCondition)
        {
          List<ConditionStructure> conditionStructureList8 = conditionStructureList1;
          conditionStructure1 = new ConditionStructure();
          conditionStructure1.Attribute = (object) ObligatoryObjectAttributes.F_BEGIN_DATE;
          conditionStructure1.RelationalOperator = filter.EventStartRelop;
          conditionStructure1.Value = (object) filter.EventStart;
          conditionStructure1.SQL = string.Empty;
          ConditionStructure conditionStructure8 = conditionStructure1;
          conditionStructureList8.Add(conditionStructure8);
        }
        if (filter.HasMachineNameCondition)
        {
          List<ConditionStructure> conditionStructureList9 = conditionStructureList1;
          conditionStructure1 = new ConditionStructure();
          conditionStructure1.Attribute = (object) ObligatoryObjectAttributes.F_COMPUTER_NAME;
          conditionStructure1.RelationalOperator = filter.MachineNameRelop;
          conditionStructure1.Value = (object) filter.MachineName;
          conditionStructure1.SQL = string.Empty;
          ConditionStructure conditionStructure9 = conditionStructure1;
          conditionStructureList9.Add(conditionStructure9);
        }
        if (filter.HasObjectNameCondition)
        {
          List<ConditionStructure> conditionStructureList10 = conditionStructureList1;
          conditionStructure1 = new ConditionStructure();
          conditionStructure1.Attribute = (object) ObligatoryObjectAttributes.F_OBJECT_NAME;
          conditionStructure1.RelationalOperator = filter.ObjectNameRelop;
          conditionStructure1.Value = (object) filter.ObjectName;
          conditionStructure1.SQL = string.Empty;
          ConditionStructure conditionStructure10 = conditionStructure1;
          conditionStructureList10.Add(conditionStructure10);
        }
        if (filter.HasObjectVersionIDCondition)
        {
          List<ConditionStructure> conditionStructureList11 = conditionStructureList1;
          conditionStructure1 = new ConditionStructure();
          conditionStructure1.Attribute = (object) ObligatoryObjectAttributes.F_OBJECT_ID;
          conditionStructure1.RelationalOperator = filter.ObjectVersionIDRelop;
          conditionStructure1.Value = (object) filter.ObjectVersionID;
          conditionStructure1.SQL = string.Empty;
          ConditionStructure conditionStructure11 = conditionStructure1;
          conditionStructureList11.Add(conditionStructure11);
        }
        if (filter.HasRelationIDCondition)
        {
          List<ConditionStructure> conditionStructureList12 = conditionStructureList1;
          conditionStructure1 = new ConditionStructure();
          conditionStructure1.Attribute = (object) ObligatoryObjectAttributes.F_RELATION_ID;
          conditionStructure1.RelationalOperator = filter.RelationIDRelop;
          conditionStructure1.Value = (object) filter.RelationID;
          conditionStructure1.SQL = string.Empty;
          ConditionStructure conditionStructure12 = conditionStructure1;
          conditionStructureList12.Add(conditionStructure12);
        }
        if (filter.HasTypeCondition)
        {
          List<ConditionStructure> conditionStructureList13 = conditionStructureList1;
          conditionStructure1 = new ConditionStructure();
          conditionStructure1.Attribute = (object) ObligatoryObjectAttributes.F_AUDIT_TYPE;
          conditionStructure1.RelationalOperator = filter.TypeRelop;
          conditionStructure1.Value = (object) filter.Type;
          conditionStructure1.SQL = string.Empty;
          ConditionStructure conditionStructure13 = conditionStructure1;
          conditionStructureList13.Add(conditionStructure13);
        }
        if (filter.HasUserCondition)
        {
          List<ConditionStructure> conditionStructureList14 = conditionStructureList1;
          conditionStructure1 = new ConditionStructure();
          conditionStructure1.Attribute = (object) ObligatoryObjectAttributes.F_USER_ID;
          conditionStructure1.RelationalOperator = filter.UserRelop;
          conditionStructure1.Value = (object) filter.UserVersionID;
          conditionStructure1.SQL = string.Empty;
          ConditionStructure conditionStructure14 = conditionStructure1;
          conditionStructureList14.Add(conditionStructure14);
        }
        return conditionStructureList1.ToArray();
      }
    }
}
