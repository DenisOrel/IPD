
// Type: Intermech.Navigator.Conditions.DateTimeValueToStringConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.SelectionService;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.Conditions;

internal sealed class DateTimeValueToStringConverter : ValueToStringConverter
{
  public DateTimeValueToStringConverter()
    : base((object) SelectionParameterTypes.sptDate)
  {
  }

  public override string ConvertValue(
    IConditionDataProvider dataProvider,
    object conditionValue,
    object typeID)
  {
    if (conditionValue is DateTime)
      return Convert.ToDateTime(conditionValue).ToString(DateTimeHelper.GenerateDisplayFormat(Convert.ToString(conditionValue)));
    return Convert.ToString(conditionValue).Equals(Intermech.Consts.CurrentDateFunction) ? Intermech.Consts.CurrentDateFunction : Convert.ToString(conditionValue);
  }
}
