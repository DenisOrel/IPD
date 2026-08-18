// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Converters_Editors.AutoSelectionNodeItemImbaseConverter
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

internal class AutoSelectionNodeItemImbaseConverter : AutoSelectionItemCommonConverter
{
  public override PropertyDescriptorCollection GetProperties(
    ITypeDescriptorContext context,
    object value,
    Attribute[] attributes)
  {
    PropertyDescriptorCollection properties = base.GetProperties(context, value, attributes);
    PropertyDescriptor objB = properties?.Find("ImbaseObjectId", true);
    if (objB != null)
    {
      AutoSelectionNodeItemImbase selectionNodeItemImbase = (AutoSelectionNodeItemImbase) value;
      List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>(properties.Count);
      for (int index = 0; index < properties.Count; ++index)
      {
        PropertyDescriptor objA = properties[index];
        if (!object.Equals((object) objA, (object) objB) || selectionNodeItemImbase.ObjTypeGuid.Value != Guid.Empty)
          propertyDescriptorList.Add(objA);
      }
      properties = new PropertyDescriptorCollection(propertyDescriptorList.ToArray(), ((IList) properties).IsReadOnly);
    }
    return properties;
  }
}
