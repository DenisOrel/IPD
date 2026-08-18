
// Type: Intermech.PropertyEditors.IObjectPropDescriptorHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

/// <summary>
/// для взаимодействий с ObjectPropDescriptorHolder ( ObjectPropertyGrid )
/// </summary>
public interface IObjectPropDescriptorHolder
{
  PropertyDescriptorCollection ExtendPropDescriptorCollectionbyMode(
    object component,
    GetAttributeValuesModes avm,
    bool hideIfNotInMode);

  GetAttributeValuesModes AttributeValuesModes { get; }

  PropertyDescriptorCollection PropDescriptorCollection { get; }

  ObjectPropertyGrid ObjectPropertyGrid { get; }
}
