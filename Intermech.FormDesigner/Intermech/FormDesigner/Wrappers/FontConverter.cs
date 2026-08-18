// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.FontConverter
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>Font конвертер для русификации.</summary>
public class FontConverter : System.Drawing.FontConverter
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
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof (Font), attributes);
    PropertyDescriptorCollection descriptorCollection = new PropertyDescriptorCollection((PropertyDescriptor[]) null);
    ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor propertyDescriptor1 = new ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor(properties["Name"], (object) null);
    propertyDescriptor1.AddAttribute((Attribute) new CustomDisplayName("Attribute.FormDesigner_74"));
    descriptorCollection.Add((PropertyDescriptor) propertyDescriptor1);
    ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor propertyDescriptor2 = new ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor(properties["Size"], (object) null);
    propertyDescriptor2.AddAttribute((Attribute) new CustomDisplayName("Attribute.FormDesigner_75"));
    descriptorCollection.Add((PropertyDescriptor) propertyDescriptor2);
    ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor propertyDescriptor3 = new ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor(properties["Bold"], (object) null);
    propertyDescriptor3.AddAttribute((Attribute) new CustomDisplayName("Attribute.FormDesigner_77"));
    propertyDescriptor3.AddAttribute((Attribute) new TypeConverterAttribute(typeof (BooleanConverter)));
    propertyDescriptor3.AddAttribute((Attribute) new EditorAttribute(typeof (BooleanEditor), typeof (UITypeEditor)));
    descriptorCollection.Add((PropertyDescriptor) propertyDescriptor3);
    ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor propertyDescriptor4 = new ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor(properties["Italic"], (object) null);
    propertyDescriptor4.AddAttribute((Attribute) new CustomDisplayName("Attribute.FormDesigner_80"));
    propertyDescriptor4.AddAttribute((Attribute) new TypeConverterAttribute(typeof (BooleanConverter)));
    propertyDescriptor4.AddAttribute((Attribute) new EditorAttribute(typeof (BooleanEditor), typeof (UITypeEditor)));
    descriptorCollection.Add((PropertyDescriptor) propertyDescriptor4);
    ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor propertyDescriptor5 = new ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor(properties["Strikeout"], (object) null);
    propertyDescriptor5.AddAttribute((Attribute) new CustomDisplayName("Attribute.FormDesigner_81"));
    propertyDescriptor5.AddAttribute((Attribute) new TypeConverterAttribute(typeof (BooleanConverter)));
    propertyDescriptor5.AddAttribute((Attribute) new EditorAttribute(typeof (BooleanEditor), typeof (UITypeEditor)));
    descriptorCollection.Add((PropertyDescriptor) propertyDescriptor5);
    ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor propertyDescriptor6 = new ClassWrapperForPropertyGrid.LocalizedPropertyDescriptor(properties["Underline"], (object) null);
    propertyDescriptor6.AddAttribute((Attribute) new CustomDisplayName("Attribute.FormDesigner_82"));
    propertyDescriptor6.AddAttribute((Attribute) new TypeConverterAttribute(typeof (BooleanConverter)));
    propertyDescriptor6.AddAttribute((Attribute) new EditorAttribute(typeof (BooleanEditor), typeof (UITypeEditor)));
    descriptorCollection.Add((PropertyDescriptor) propertyDescriptor6);
    return descriptorCollection.Sort(new string[6]
    {
      "Name",
      "Size",
      "Unit",
      "Italic",
      "Strikeout",
      "Underline"
    });
  }
}
