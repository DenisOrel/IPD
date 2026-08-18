
// Type: Intermech.Navigator.Conditions.AttributeConditionControls.InputObjectAttributeMasterForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.Conditions.AttributeConditionControls;

internal sealed class InputObjectAttributeMasterForm : MasterForm
{
  protected override IEditValueStepControl GetEditValueStepControl(
    IConditionDataProvider dataProvider,
    int[] objectTypeIDs)
  {
    return (IEditValueStepControl) new EditInputObjectAttributeStepControl(dataProvider, objectTypeIDs);
  }
}
