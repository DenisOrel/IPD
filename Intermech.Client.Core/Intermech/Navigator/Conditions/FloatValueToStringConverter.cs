
// Type: Intermech.Navigator.Conditions.FloatValueToStringConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.SelectionService;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.Conditions;

internal sealed class FloatValueToStringConverter : ValueToStringConverter
{
  public FloatValueToStringConverter()
    : base((object) SelectionParameterTypes.sptFloat)
  {
  }

  public override string ConvertValue(
    IConditionDataProvider dataProvider,
    object value,
    object typeID)
  {
    return Convert.ToString(Convert.ToDecimal(value));
  }
}
