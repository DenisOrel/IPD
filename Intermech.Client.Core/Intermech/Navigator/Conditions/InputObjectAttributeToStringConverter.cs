
// Type: Intermech.Navigator.Conditions.InputObjectAttributeToStringConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.SelectionService;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.Conditions;

internal class InputObjectAttributeToStringConverter : ValueToStringConverter
{
  public InputObjectAttributeToStringConverter()
    : base((object) typeof (InputObjectAttribute))
  {
  }

  public override string ConvertValue(
    IConditionDataProvider dataProvider,
    object value,
    object typeID)
  {
    string str = string.Empty;
    if (value != null)
    {
      InputObjectAttribute inputObjectAttribute = (InputObjectAttribute) value;
      if (!inputObjectAttribute.ObjectGUID.Equals(Guid.Empty))
        str = dataProvider.GetObjectTypeCaption((object) inputObjectAttribute.ObjectGUID);
      str = str == string.Empty ? str : str + ".";
      if (!inputObjectAttribute.AttributeGUID.Equals(Guid.Empty))
        str += dataProvider.GetAttributeName((object) inputObjectAttribute.AttributeGUID);
    }
    return str;
  }
}
