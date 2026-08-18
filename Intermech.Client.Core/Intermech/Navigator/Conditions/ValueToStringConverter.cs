
// Type: Intermech.Navigator.Conditions.ValueToStringConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.Conditions;

internal abstract class ValueToStringConverter : IValueToStringConverter
{
  public ValueToStringConverter(object converterID) => this.ConverterID = converterID;

  public object ConverterID { get; private set; }

  public virtual string ConvertValue(
    IConditionDataProvider dataProvider,
    object value,
    object typeID)
  {
    return Convert.ToString(value);
  }

  public virtual string ConvertValue(
    IConditionDataProvider dataProvider,
    object attributeID,
    object conditionValue,
    object typeID)
  {
    return this.ConvertValue(dataProvider, conditionValue, typeID);
  }
}
