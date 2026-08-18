// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.PointConverter
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>Point конвертер для русификации.</summary>
public class PointConverter : System.Drawing.PointConverter
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="value"></param>
  /// <param name="attributes"></param>
  /// <returns></returns>
  public override PropertyDescriptorCollection GetProperties(
    ITypeDescriptorContext context,
    object value,
    Attribute[] attributes)
  {
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof (Point), attributes);
    PropertyDescriptorCollection descriptorCollection = new PropertyDescriptorCollection((PropertyDescriptor[]) null);
    ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor propertyDescriptor1 = new ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor(properties["X"], (object) null);
    propertyDescriptor1.AddAttribute((Attribute) new CustomDisplayName("Attribute.FormDesigner_72"));
    descriptorCollection.Add((PropertyDescriptor) propertyDescriptor1);
    ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor propertyDescriptor2 = new ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor(properties["Y"], (object) null);
    propertyDescriptor2.AddAttribute((Attribute) new CustomDisplayName("Attribute.FormDesigner_73"));
    descriptorCollection.Add((PropertyDescriptor) propertyDescriptor2);
    return descriptorCollection.Sort(new string[2]
    {
      "X",
      "Y"
    });
  }
}
