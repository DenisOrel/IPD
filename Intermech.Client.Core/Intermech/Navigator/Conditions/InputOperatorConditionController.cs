
// Type: Intermech.Navigator.Conditions.InputOperatorConditionController
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.SelectionService;
using Intermech.Kernel.Search;


namespace Intermech.Navigator.Conditions;

internal sealed class InputOperatorConditionController : ConditionController<InputOperatorForm>
{
  public override string VisibleName => "Использовать операторы отношений для состава и входимости";

  public override bool IsHandleConditionStructure(ConditionStructure conditionStructure)
  {
    return SelectionParameter.IsInRelationOpr(conditionStructure.RelationalOperator);
  }
}
