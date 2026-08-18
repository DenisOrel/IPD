
// Type: Intermech.Search.Diff.PropertyDiffCollectionConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections.Generic;
using System.ComponentModel;


namespace Intermech.Search.Diff;

public sealed class PropertyDiffCollectionConverter : DiffCollectionConverterBase<PropertyDiff>
{
  protected override PropertyDescriptorCollection CreatePropertyDescriptorCollection(
    IDiffCollection<PropertyDiff> diffCollection)
  {
    List<System.ComponentModel.PropertyDescriptor> propertyDescriptorList = new List<System.ComponentModel.PropertyDescriptor>();
    foreach (PropertyDiff diff in (IEnumerable<PropertyDiff>) diffCollection)
    {
      TypeDiffPropertyDescriptor propertyDescriptor = new TypeDiffPropertyDescriptor(typeof (PropertyDiffCollection), diff.PropertyInfo);
      propertyDescriptorList.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor);
    }
    return new PropertyDescriptorCollection(propertyDescriptorList.ToArray());
  }
}
