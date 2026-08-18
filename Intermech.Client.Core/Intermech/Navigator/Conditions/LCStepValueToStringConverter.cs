
// Type: Intermech.Navigator.Conditions.LCStepValueToStringConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.SelectionService;
using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.Conditions;

internal sealed class LCStepValueToStringConverter : ValueToStringConverter
{
  public LCStepValueToStringConverter()
    : base((object) SelectionParameterTypes.sptLifecycleStep)
  {
  }

  public override string ConvertValue(
    IConditionDataProvider dataProvider,
    object value,
    object typeID)
  {
    return dataProvider.GetLifecycleStepCaption(value);
  }
}
