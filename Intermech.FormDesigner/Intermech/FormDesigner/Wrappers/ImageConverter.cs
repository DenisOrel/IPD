// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.ImageConverter
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

/// <summary>Image конвертер для русификации.</summary>
public class ImageConverter : System.Drawing.ImageConverter
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
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof (Image), attributes);
    PropertyDescriptorCollection descriptorCollection = new PropertyDescriptorCollection((PropertyDescriptor[]) null);
    ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor propertyDescriptor1 = new ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor(properties["RawFormat"], (object) null);
    propertyDescriptor1.AddAttribute((Attribute) new CustomDisplayName("Attribute.FormDesigner.ImageConverter.RawFormat"));
    descriptorCollection.Add((PropertyDescriptor) propertyDescriptor1);
    ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor propertyDescriptor2 = new ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor(properties["Size"], (object) null);
    propertyDescriptor2.AddAttribute((Attribute) new CustomDisplayName("Attribute.FormDesigner.ImageConverter.Size"));
    propertyDescriptor2.AddAttribute((Attribute) new TypeConverterAttribute(typeof (SizeConverter)));
    descriptorCollection.Add((PropertyDescriptor) propertyDescriptor2);
    return descriptorCollection.Sort(new string[2]
    {
      "RawFormat",
      "Size"
    });
  }
}
