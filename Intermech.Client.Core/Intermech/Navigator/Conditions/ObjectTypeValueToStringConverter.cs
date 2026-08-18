
// Type: Intermech.Navigator.Conditions.ObjectTypeValueToStringConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.SelectionService;
using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.Conditions;

internal sealed class ObjectTypeValueToStringConverter : ValueToStringConverter
{
  public ObjectTypeValueToStringConverter()
    : base((object) SelectionParameterTypes.sptObjectType)
  {
  }

  public override string ConvertValue(
    IConditionDataProvider dataProvider,
    object value,
    object typeID)
  {
    return dataProvider.GetObjectTypeCaption(value);
  }
}
