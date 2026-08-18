
// Type: Intermech.Navigator.Conditions.FormulaConditionController
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using System;


namespace Intermech.Navigator.Conditions;

internal sealed class FormulaConditionController : ConditionController<FormulaConditionForm>
{
  public override string VisibleName => "Использовать сравнение значений атрибутов объекта";

  public override bool IsHandleConditionStructure(ConditionStructure conditionStructure)
  {
    return conditionStructure.Value is ConditionFormula;
  }

  public override bool HandleConditionCaption(
    ConditionStructure conditionStructure,
    out string conditionText,
    out string valueText)
  {
    int attributeId = this.dataProvider.GetAttributeID(conditionStructure.Attribute);
    conditionText = attributeId == 0 ? "Сравнение по формуле" : $"\"{this.dataProvider.GetAttributeName((object) attributeId)}\" {EnumDescConverter.GetEnumDescription((Enum) conditionStructure.RelationalOperator)}";
    valueText = ((ConditionFormula) conditionStructure.Value).Formula;
    return true;
  }

  public override bool IsInnerSupported => false;
}
