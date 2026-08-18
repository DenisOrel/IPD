
// Type: Intermech.Navigator.Conditions.InGlobalIndexController
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;


namespace Intermech.Navigator.Conditions;

internal sealed class InGlobalIndexController : ConditionController<InGlobalIndexForm>
{
  public override string VisibleName => "Поиск в общем индексе";

  public override bool IsHandleConditionStructure(ConditionStructure conditionStructure)
  {
    return (object) conditionStructure.RelationalOperator is RelationalOperators.InGlobalIndex;
  }

  public override bool HandleConditionCaption(
    ConditionStructure conditionStructure,
    out string condition,
    out string value)
  {
    condition = "Поиск в общем индексе";
    value = conditionStructure.Value is GlobalIndexSearchValue ? ((GlobalIndexSearchValue) conditionStructure.Value).Value : "<Строка не задана>";
    return true;
  }
}
