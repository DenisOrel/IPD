
// Type: Intermech.Navigator.Conditions.HandlerValueToStringConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Interfaces.SelectionService;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using System;


namespace Intermech.Navigator.Conditions;

internal sealed class HandlerValueToStringConverter : ValueToStringConverter
{
  public HandlerValueToStringConverter()
    : base((object) SelectionParameterTypes.sptHandler)
  {
  }

  public override string ConvertValue(
    IConditionDataProvider dataProvider,
    object attributeID,
    object value,
    object typeID)
  {
    int attributeId = dataProvider.GetAttributeID(attributeID);
    IAttributePropertyDescriber describer = ServicesManager.GetService<IAttributePropertyDescriberService>().GetDescriber(attributeId);
    return describer != null ? Convert.ToString(describer.GetPropDescriptorValue((IElementInfo) null, attributeId, value)) : this.ConvertValue(dataProvider, attributeID, value);
  }
}
