// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IValueToStringConverter
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

public interface IValueToStringConverter
{
  object ConverterID { get; }

  string ConvertValue(IConditionDataProvider dataProvider, object conditionValue, object typeID);

  string ConvertValue(
    IConditionDataProvider dataProvider,
    object attributeID,
    object conditionValue,
    object typeID);
}
