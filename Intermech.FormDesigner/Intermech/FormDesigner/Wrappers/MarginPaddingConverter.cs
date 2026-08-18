// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.MarginPaddingConverter
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>MarginPadding конвертер для русификации.</summary>
public class MarginPaddingConverter : PaddingConverter
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
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof (Padding), attributes);
    PropertyDescriptorCollection descriptorCollection = new PropertyDescriptorCollection((PropertyDescriptor[]) null);
    ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor propertyDescriptor1 = new ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor(properties["All"], (object) null);
    propertyDescriptor1.AddAttribute((Attribute) new CustomDisplayName("Attribute.FormDesigner_83"));
    descriptorCollection.Add((PropertyDescriptor) propertyDescriptor1);
    ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor propertyDescriptor2 = new ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor(properties["Left"], (object) null);
    propertyDescriptor2.AddAttribute((Attribute) new CustomDisplayName("Attribute.FormDesigner_2"));
    descriptorCollection.Add((PropertyDescriptor) propertyDescriptor2);
    ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor propertyDescriptor3 = new ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor(properties["Top"], (object) null);
    propertyDescriptor3.AddAttribute((Attribute) new CustomDisplayName("Attribute.FormDesigner_3"));
    descriptorCollection.Add((PropertyDescriptor) propertyDescriptor3);
    ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor propertyDescriptor4 = new ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor(properties["Right"], (object) null);
    propertyDescriptor4.AddAttribute((Attribute) new CustomDisplayName("Attribute.FormDesigner_87"));
    descriptorCollection.Add((PropertyDescriptor) propertyDescriptor4);
    ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor propertyDescriptor5 = new ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor(properties["Bottom"], (object) null);
    propertyDescriptor5.AddAttribute((Attribute) new CustomDisplayName("Attribute.FormDesigner_89"));
    descriptorCollection.Add((PropertyDescriptor) propertyDescriptor5);
    return descriptorCollection.Sort(new string[5]
    {
      "All",
      "Left",
      "Top",
      "Right",
      "Bottom"
    });
  }
}
