
// Type: Intermech.Navigator.Conditions.ObjectValueToStringConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.SelectionService;
using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.Conditions;

internal sealed class ObjectValueToStringConverter : ValueToStringConverter
{
  public ObjectValueToStringConverter()
    : base((object) SelectionParameterTypes.sptObject)
  {
  }

  public override string ConvertValue(
    IConditionDataProvider dataProvider,
    object value,
    object typeID)
  {
    switch (value)
    {
      case -1:
      case -1L:
        return "Входной объект";
      case string _:
        return (string) value;
      default:
        return dataProvider.GetObjectCaption(value);
    }
  }
}
