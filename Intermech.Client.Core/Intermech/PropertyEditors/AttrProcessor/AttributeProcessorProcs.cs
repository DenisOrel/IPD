
// Type: Intermech.PropertyEditors.AttrProcessor.AttributeProcessorProcs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;


namespace Intermech.PropertyEditors.AttrProcessor;

public class AttributeProcessorProcs
{
  public static MultiValueModes GetMultiValueModes(int attributeId)
  {
    MultiValueModes multiValueModes = MultiValueModes.SingleValue;
    IDBAttributeTypeInfo attributeType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(attributeId, false);
    if (attributeType != null)
      multiValueModes = attributeType.MultipleValued;
    return multiValueModes;
  }

  public static bool IsValuedFromList(int attributeId)
  {
    return MultiValueModesHelper.IsValuedFromList(AttributeProcessorProcs.GetMultiValueModes(attributeId));
  }

  public static bool IsMultipleValued(int attributeId)
  {
    return MultiValueModesHelper.IsMultipleValued(AttributeProcessorProcs.GetMultiValueModes(attributeId));
  }
}
