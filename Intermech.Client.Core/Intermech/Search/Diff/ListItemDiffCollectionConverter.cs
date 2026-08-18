
// Type: Intermech.Search.Diff.ListItemDiffCollectionConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace Intermech.Search.Diff;

public sealed class ListItemDiffCollectionConverter : DiffCollectionConverterBase<ListItemDiff>
{
  protected override PropertyDescriptorCollection CreatePropertyDescriptorCollection(
    IDiffCollection<ListItemDiff> diffCollection)
  {
    List<System.ComponentModel.PropertyDescriptor> propertyDescriptorList = new List<System.ComponentModel.PropertyDescriptor>();
    foreach (ListItemDiff diff in (IEnumerable<ListItemDiff>) diffCollection)
    {
      Type propertyType = diff.FirstOperand == null || diff.FirstOperand.Value == null ? typeof (object) : diff.FirstOperand.Value.GetType();
      ListItemDiffPropertyDescriptor propertyDescriptor = new ListItemDiffPropertyDescriptor(typeof (ListItemDiffCollection), diff.Index, propertyType);
      propertyDescriptorList.Add((System.ComponentModel.PropertyDescriptor) propertyDescriptor);
    }
    return new PropertyDescriptorCollection(propertyDescriptorList.ToArray());
  }
}
