
// Type: Intermech.Navigator.Selections.BindingHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System.Collections.Generic;


namespace Intermech.Navigator.Selections;

public static class BindingHelper
{
  public static List<ConditionStructure> GetBindingConditions4SelectionNode(
    BindingType bindingType,
    int selectionType)
  {
    List<ConditionStructure> conditions4SelectionNode = new List<ConditionStructure>();
    conditions4SelectionNode.AddRange((IEnumerable<ConditionStructure>) new ConditionStructure[2]
    {
      new ConditionStructure(Consts.KindSelectionAttrID, RelationalOperators.AttributeExists, (object) null, LogicalOperators.AND, 1, false),
      new ConditionStructure(Consts.KindSelectionAttrID, RelationalOperators.Equal, (object) selectionType, LogicalOperators.AND, -1, false)
    });
    switch (bindingType)
    {
      case BindingType.CommonSelections:
        conditions4SelectionNode.Add(new ConditionStructure(-7, RelationalOperators.Equal, (object) MetaDataHelper.GetObjectTypeID("cad00122-306c-11d8-b4e9-00304f19f545"), LogicalOperators.AND, 0, false));
        break;
      case BindingType.PersonalSelections:
        conditions4SelectionNode.Add(new ConditionStructure(-7, RelationalOperators.Equal, (object) MetaDataHelper.GetObjectTypeID("cad00123-306c-11d8-b4e9-00304f19f545"), LogicalOperators.AND, 0, false));
        break;
    }
    return conditions4SelectionNode;
  }

  public static List<ConditionStructure> GetBindingConditions4ClassifierNode(int classifierType)
  {
    List<ConditionStructure> conditions4ClassifierNode = new List<ConditionStructure>();
    conditions4ClassifierNode.AddRange((IEnumerable<ConditionStructure>) new ConditionStructure[2]
    {
      new ConditionStructure(Consts.KindClassifierAttrID, RelationalOperators.AttributeExists, (object) null, LogicalOperators.AND, 1, false),
      new ConditionStructure(Consts.KindClassifierAttrID, RelationalOperators.Equal, (object) classifierType, LogicalOperators.AND, -1, false)
    });
    return conditions4ClassifierNode;
  }
}
