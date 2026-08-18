
// Type: Intermech.Search.Diff.AttributeDiffCollectionConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Search.Data.Repositories;
using System.Collections.Generic;
using System.ComponentModel;


namespace Intermech.Search.Diff;

public sealed class AttributeDiffCollectionConverter : DiffCollectionConverterBase<AttributeDiff>
{
  protected override PropertyDescriptorCollection CreatePropertyDescriptorCollection(
    IDiffCollection<AttributeDiff> diffCollection)
  {
    List<System.ComponentModel.PropertyDescriptor> propertyDescriptorList = new List<System.ComponentModel.PropertyDescriptor>();
    foreach (AttributeDiff diff in (IEnumerable<AttributeDiff>) diffCollection)
    {
      IMSAttributeType attributeType = ServiceLocator.Get<IAttributeTypeRepository>().Find(diff.AttributeTypeID);
      AttributeDiffPropertyDescriptor propertyDescriptor = new AttributeDiffPropertyDescriptor(diffCollection.GetType(), attributeType);
      propertyDescriptorList.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor);
    }
    return new PropertyDescriptorCollection(propertyDescriptorList.ToArray());
  }
}
