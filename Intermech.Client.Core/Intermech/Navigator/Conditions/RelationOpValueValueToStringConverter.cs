
// Type: Intermech.Navigator.Conditions.RelationOpValueValueToStringConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.SelectionService;
using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.Conditions;

internal sealed class RelationOpValueValueToStringConverter : ValueToStringConverter
{
  public RelationOpValueValueToStringConverter()
    : base((object) SelectionParameterTypes.sptRelationOpValue)
  {
  }

  public override string ConvertValue(
    IConditionDataProvider dataProvider,
    object value,
    object typeID)
  {
    string empty = string.Empty;
    string str = !(value is -1) ? dataProvider.GetObjectTypeCaption(value) : "Тип входного объекта";
    if (typeID != null)
      str += $" (тип связи: \"{dataProvider.GetRelationTypeCaption(typeID)}\")";
    return str;
  }
}
