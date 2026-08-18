
// Type: Intermech.Navigator.Conditions.RelationTypeValueToStringConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.SelectionService;
using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.Conditions;

internal sealed class RelationTypeValueToStringConverter : ValueToStringConverter
{
  public RelationTypeValueToStringConverter()
    : base((object) SelectionParameterTypes.sptLinkType)
  {
  }

  public override string ConvertValue(
    IConditionDataProvider dataProvider,
    object value,
    object typeID)
  {
    return dataProvider.GetRelationTypeCaption(value);
  }
}
