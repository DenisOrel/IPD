
// Type: Intermech.Navigator.Conditions.AttributeConditionControls.IEditValueStepControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using System.Windows.Forms;


namespace Intermech.Navigator.Conditions.AttributeConditionControls;

public interface IEditValueStepControl
{
  event StepControlStateChangedHandler StepControlStateChanged;

  void OnActivate(ConditionAttributeInfo attribute, ConditionStructure conditionStructure);

  ConditionStructure ConditionStructure { get; }

  UserControl Control { get; }
}
