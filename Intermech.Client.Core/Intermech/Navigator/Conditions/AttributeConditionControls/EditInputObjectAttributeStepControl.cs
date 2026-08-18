
// Type: Intermech.Navigator.Conditions.AttributeConditionControls.EditInputObjectAttributeStepControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.SelectionService;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.SelectionView;
using System.Collections.Generic;


namespace Intermech.Navigator.Conditions.AttributeConditionControls;

internal class EditInputObjectAttributeStepControl : EditValueStepControl
{
  public EditInputObjectAttributeStepControl(
    IConditionDataProvider dataProvider,
    int[] objectTypeIDs)
    : base(dataProvider, objectTypeIDs)
  {
    this.SetValueGroupBoxText("Атрибут входного объекта");
  }

  protected override void GetControlData(
    out RelationalOperators[] enabledOperators,
    out SelectionParameterTypes paramType)
  {
    RelationalOperators[] enabledOperators1;
    base.GetControlData(out enabledOperators1, out paramType);
    List<RelationalOperators> relationalOperatorsList = new List<RelationalOperators>();
    foreach (RelationalOperators RelationalOperator in enabledOperators1)
    {
      if (!SelectionParameter.IsInRelationOpr(RelationalOperator) && !SelectionParameter.IsNoneValueOpr(RelationalOperator))
        relationalOperatorsList.Add(RelationalOperator);
    }
    enabledOperators = relationalOperatorsList.ToArray();
  }

  protected override ShowValueMode GetValueMode(
    RelationOperatorValueMode rovm,
    SelectionParameterTypes paramType,
    RelationalOperators currentOperator,
    bool possibleValuesPresent)
  {
    return ShowValueMode.svmInputObjectAttribute;
  }

  public override void OnActivate(
    ConditionAttributeInfo attribute,
    ConditionStructure conditionStructure)
  {
    ConditionStructure conditionStructure1 = conditionStructure.Clone();
    if (attribute != null && conditionStructure.Value == null)
    {
      conditionStructure1.Value = (object) new InputObjectAttribute();
      ((InputObjectAttribute) conditionStructure1.Value).AttributeGUID = this.dataProvider.GetAttributeGuid(attribute.Id);
    }
    base.OnActivate(attribute, conditionStructure1);
  }
}
