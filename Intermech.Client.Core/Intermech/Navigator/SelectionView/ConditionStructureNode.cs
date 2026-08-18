
// Type: Intermech.Navigator.SelectionView.ConditionStructureNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using System.Collections.Generic;


namespace Intermech.Navigator.SelectionView;

/// <summary>
/// Информация по условию выборки, храниться в Tag нода дерева
/// </summary>
internal class ConditionStructureNode
{
  /// <summary>Условие выборки</summary>
  public ConditionStructure ConditionStruct;
  /// <summary>Включена на данный момент</summary>
  public bool Enabled;
  /// <summary>Допустимые значения</summary>
  public Dictionary<object, string> PossibleValues;

  public ConditionStructureNode(ConditionStructure conditionStructure)
  {
    this.ConditionStruct = conditionStructure;
    this.Enabled = true;
  }
}
