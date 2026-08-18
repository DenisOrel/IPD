// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Converters_Editors.AutoSelectionTestNodeExpConverter
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.AutoSelection.Client.Converters_Editors;

internal class AutoSelectionTestNodeExpConverter : ExpandableObjectConverter
{
  public override PropertyDescriptorCollection GetProperties(
    ITypeDescriptorContext context,
    object value,
    Attribute[] attributes)
  {
    PropertyDescriptorCollection properties = base.GetProperties(context, value, attributes);
    List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>(properties.Count);
    for (int index = 0; index < properties.Count; ++index)
    {
      PropertyDescriptor propertyDescriptor1 = properties[index];
      AttributeCollection attributes1 = propertyDescriptor1.Attributes;
      List<Attribute> attributeList = new List<Attribute>();
      foreach (Attribute objA in attributes1)
      {
        if (!object.Equals((object) objA, (object) ReadOnlyAttribute.No) && !object.Equals((object) objA, (object) ReadOnlyAttribute.Default) && !(objA is EditorAttribute))
          attributeList.Add(objA);
      }
      attributeList.Add((Attribute) ReadOnlyAttribute.Yes);
      attributeList.Add((Attribute) new EditorAttribute(typeof (SelectionEmptyEditor), typeof (UITypeEditor)));
      PropertyDescriptor propertyDescriptor2 = (PropertyDescriptor) new AutoSelectionReadOnlyPropDescriptor(propertyDescriptor1, attributeList.ToArray());
      propertyDescriptorList.Add(propertyDescriptor2);
    }
    return new PropertyDescriptorCollection(propertyDescriptorList.ToArray(), ((IList) properties).IsReadOnly);
  }
}
