// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Converters_Editors.AutoSelectionNodeItemObjectConverter
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.AutoSelectionNode;
using Intermech.Interfaces.Document;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace Intermech.AutoSelection.Client.Converters_Editors;

internal class AutoSelectionNodeItemObjectConverter : AutoSelectionItemCommonConverter
{
  public override PropertyDescriptorCollection GetProperties(
    ITypeDescriptorContext context,
    object value,
    Attribute[] attributes)
  {
    PropertyDescriptorCollection properties = base.GetProperties(context, value, attributes);
    if (properties == null)
      return (PropertyDescriptorCollection) null;
    List<PropertyDescriptor> source = new List<PropertyDescriptor>(properties.Count);
    foreach (PropertyDescriptor PropDesc in properties)
      source.Add(PropDesc is CustomPropertyDescriptor ? PropDesc : (PropertyDescriptor) new CustomPropertyDescriptor(PropDesc));
    AutoSelectionNodeItemObject selectionNodeItemObject = (AutoSelectionNodeItemObject) value;
    PropertyDescriptor objB = source.FirstOrDefault<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (item => item.Name == "ItemObjectID"));
    if (objB != null)
    {
      List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>(properties.Count);
      foreach (PropertyDescriptor objA in source)
      {
        if (!object.Equals((object) objA, (object) objB) || selectionNodeItemObject.ObjTypeGuid.Value != Guid.Empty)
          propertyDescriptorList.Add(objA);
      }
      source = propertyDescriptorList;
    }
    if (source.FirstOrDefault<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (item => item.Name == "CalcObjectAttrList")) is CustomPropertyDescriptor propertyDescriptor1)
      propertyDescriptor1.SetIsReadOnly(selectionNodeItemObject.ItemObjectMode == AutoSelectonItemObjectMode.LinkToObjectOnly);
    if (source.FirstOrDefault<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (item => item.Name == "DefObjAttrList")) is CustomPropertyDescriptor propertyDescriptor2)
      propertyDescriptor2.SetIsReadOnly(selectionNodeItemObject.ItemObjectMode == AutoSelectonItemObjectMode.LinkToObjectOnly);
    if (source.FirstOrDefault<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (item => item.Name == "CalcRelAttrList")) is CustomPropertyDescriptor propertyDescriptor3)
      propertyDescriptor3.SetIsReadOnly(false);
    if (source.FirstOrDefault<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (item => item.Name == "DefRelAttrList")) is CustomPropertyDescriptor propertyDescriptor4)
      propertyDescriptor4.SetIsReadOnly(false);
    return new PropertyDescriptorCollection(source.ToArray(), ((IList) properties).IsReadOnly);
  }
}
