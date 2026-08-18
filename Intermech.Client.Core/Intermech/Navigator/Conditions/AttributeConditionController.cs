
// Type: Intermech.Navigator.Conditions.AttributeConditionController
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.Conditions.AttributeConditionControls;


namespace Intermech.Navigator.Conditions;

/// <summary>Контроллер для условия с атрибутом для выборки</summary>
public class AttributeConditionController : ConditionController<MasterForm>
{
  public override string VisibleName => "Использовать атрибут для сравнения";

  public override bool IsHandleConditionStructure(ConditionStructure conditionStructure)
  {
    return conditionStructure.Attribute != null;
  }

  public override bool AttributesCondition => true;
}
