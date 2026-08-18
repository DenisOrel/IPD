
// Type: Intermech.Navigator.Conditions.InLCHistoryController
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using System;


namespace Intermech.Navigator.Conditions;

internal sealed class InLCHistoryController : ConditionController<InLCHistoryConditionForm>
{
  public override string VisibleName => "Поиск с использованием атрибутов истории ЖЦ объекта";

  public override bool IsHandleConditionStructure(ConditionStructure conditionStructure)
  {
    return conditionStructure.Value is LC_ConditionParams;
  }

  public override bool HandleConditionCaption(
    ConditionStructure conditionStructure,
    out string conditionText,
    out string valueText)
  {
    LC_ConditionParams lcConditionParams = conditionStructure.Value as LC_ConditionParams;
    conditionText = $"Дата перевода на \"{(lcConditionParams.LevelID.HasValue ? (object) this.dataProvider.GetLifecycleLevelCaption((object) lcConditionParams.LevelID) : (object) this.dataProvider.GetLifecycleStepCaption((object) lcConditionParams.LCStepID))}\" ";
    conditionText += EnumDescConverter.GetEnumDescription((Enum) lcConditionParams.DateOperator);
    if (lcConditionParams.DateOperator == RelationalOperators.LastNDays)
      valueText = Convert.ToString(lcConditionParams.LastNDays);
    else if (lcConditionParams.DateOperator == RelationalOperators.Between)
    {
      ref string local1 = ref valueText;
      string str1 = lcConditionParams.BeginDate.ToString("d");
      ref DateTime? local2 = ref lcConditionParams.EndDate;
      string str2 = local2.HasValue ? local2.GetValueOrDefault().ToString("d") : (string) null;
      string str3 = $"от {str1} до {str2}";
      local1 = str3;
    }
    else
      valueText = lcConditionParams.BeginDate.ToString("d");
    return true;
  }
}
