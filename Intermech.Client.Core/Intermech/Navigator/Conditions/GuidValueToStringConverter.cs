
// Type: Intermech.Navigator.Conditions.GuidValueToStringConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.SelectionService;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.Conditions;

internal sealed class GuidValueToStringConverter : ValueToStringConverter
{
  public GuidValueToStringConverter()
    : base((object) SelectionParameterTypes.sptGlobalID)
  {
  }

  public override string ConvertValue(
    IConditionDataProvider dataProvider,
    object value,
    object typeID)
  {
    if (value is Guid guid)
      return guid.ToString();
    string text = Convert.ToString(value);
    return GuidHelper.IsGuid(text) ? text : string.Empty;
  }
}
