
// Type: Intermech.Navigator.Conditions.CheckOutByValueToStringConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.SelectionService;
using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.Conditions;

internal class CheckOutByValueToStringConverter : UserValueToStringConverter
{
  public CheckOutByValueToStringConverter()
    : base(SelectionParameterTypes.sptCheckOutBy)
  {
  }

  public override string ConvertValue(
    IConditionDataProvider dataProvider,
    object value,
    object typeID)
  {
    return value is 0L ? "Никем не взят на изменение" : base.ConvertValue(dataProvider, value, typeID);
  }
}
