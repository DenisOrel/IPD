
// Type: Intermech.Navigator.Conditions.InputObjectAttributeController
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.SelectionService;
using Intermech.Kernel.Search;
using Intermech.Navigator.Conditions.AttributeConditionControls;
using System.Collections.Generic;


namespace Intermech.Navigator.Conditions;

internal sealed class InputObjectAttributeController : 
  ConditionController<InputObjectAttributeMasterForm>
{
  private SelectionType[] _supportedTypes = new SelectionType[1]
  {
    SelectionType.Context
  };

  public override string VisibleName => "Использовать атрибут входного объекта для сравнения";

  public override bool IsHandleConditionStructure(ConditionStructure conditionStructure)
  {
    if (conditionStructure.Attribute != null)
    {
      if (!(conditionStructure.Value is IList<object>))
        return conditionStructure.Value is InputObjectAttribute;
      if (((ICollection<object>) conditionStructure.Value).Count > 0)
        return ((IList<object>) conditionStructure.Value)[0] is InputObjectAttribute;
    }
    return false;
  }

  public override SelectionType[] SupportedTypes => this._supportedTypes;
}
