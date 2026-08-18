// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Converters_Editors.AutoSelectionNodeTestConverter
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.AutoSelectionNode;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Intermech.AutoSelection.Client.Converters_Editors;

internal class AutoSelectionNodeTestConverter : TypeConverter
{
  public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;

  public override PropertyDescriptorCollection GetProperties(
    ITypeDescriptorContext context,
    object value,
    Attribute[] attributes)
  {
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value, attributes, true);
    PropertyDescriptor objB = properties.Find("DefObjAttrList", true);
    if (objB != null && value is AutoSelectionNodeTest selectionNodeTest)
    {
      List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>(properties.Count);
      for (int index = 0; index < properties.Count; ++index)
      {
        PropertyDescriptor objA = properties[index];
        if (!object.Equals((object) objA, (object) objB) || selectionNodeTest.ObjectMode != AutoSelectionTestObjectMode.UseCurrent)
          propertyDescriptorList.Add(objA);
      }
      properties = new PropertyDescriptorCollection(propertyDescriptorList.ToArray(), ((IList) properties).IsReadOnly);
    }
    return properties;
  }
}
