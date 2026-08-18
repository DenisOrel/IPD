
// Type: Intermech.PropertyEditors.AttributePropertyDescriberService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;


namespace Intermech.PropertyEditors;

/// <summary>
/// Служба регистрации обработчиков для редактирования атрибутов в ObjectPropertyGrid
/// </summary>
public class AttributePropertyDescriberService : IAttributePropertyDescriberService
{
  public void RegisterDescriber(
    int attributeId,
    IAttributePropertyDescriber iAttributePropertyDescriber)
  {
    AttributePropertyDescriberHolder.AddDescriber(attributeId, iAttributePropertyDescriber);
  }

  public void UnregisterDescriber(int attributeId)
  {
    AttributePropertyDescriberHolder.RemoveDescriber(attributeId);
  }

  public IAttributePropertyDescriber GetDescriber(int attributeId)
  {
    IAttributePropertyDescriber describer = AttributePropertyDescriberHolder.GetDescriber(attributeId);
    if (describer == null && AttributeValuesEditor.IsTableRecordRefFlagSet(attributeId) && ServicesManager.GetService(typeof (IImbaseSelector)) is IImbaseSelector service)
      describer = service.GetDescriberForTableRecordRefFlag();
    return describer;
  }
}
