
// Type: Intermech.Navigator.Conditions.BoolValueToStringConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.SelectionService;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.Conditions;

internal sealed class BoolValueToStringConverter : ValueToStringConverter
{
  public BoolValueToStringConverter()
    : base((object) SelectionParameterTypes.sptBool)
  {
  }

  public override string ConvertValue(
    IConditionDataProvider dataProvider,
    object value,
    object typeID)
  {
    return !Convert.ToBoolean(value) ? Intermech.Consts.FalseValue : Intermech.Consts.TrueValue;
  }
}
