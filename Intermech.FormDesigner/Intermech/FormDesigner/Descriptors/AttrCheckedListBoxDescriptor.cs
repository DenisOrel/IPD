// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Descriptors.AttrCheckedListBoxDescriptor
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.FormDesigner.Wrappers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Descriptors;

/// <summary>
/// 
/// </summary>
/// <summary>Конструктор.</summary>
/// <param name="ctrl">Контрол</param>
internal class AttrCheckedListBoxDescriptor(Control ctrl) : FormDesignerControlDescriptor(ctrl)
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="propertiesNames"></param>
  protected override void FillPropertiesNames(Dictionary<string, List<string>> propertiesNames)
  {
    base.FillPropertiesNames(propertiesNames);
    propertiesNames["Appearance"] = new List<string>()
    {
      "BackColor",
      "BorderStyle",
      "Font",
      "ForeColor",
      "Hint"
    };
    propertiesNames["Behavior"] = new List<string>()
    {
      "CheckOnClick",
      "DisabledInDesign",
      "ScrollAlwaysVisible",
      "Sorted",
      "TabIndex",
      "TabStop"
    };
    propertiesNames["Data"] = new List<string>()
    {
      "AttributeInfo",
      "CanAddAttribute",
      "ParentPoint",
      "Tag"
    };
    propertiesNames["Layout"] = new List<string>()
    {
      "Anchor",
      "Dock",
      "Location",
      "Margin",
      "MaximumSize",
      "MinimumSize",
      "Size"
    };
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="propertyName"></param>
  /// <param name="attributes"></param>
  protected override void FillPropertyAttributes(string propertyName, List<Attribute> attributes)
  {
    base.FillPropertyAttributes(propertyName, attributes);
    switch (propertyName)
    {
      case "CheckOnClick":
        attributes.Add((Attribute) new DefaultValueAttribute(true));
        attributes.Add((Attribute) new TypeConverterAttribute(typeof (System.ComponentModel.BooleanConverter)));
        attributes.Add((Attribute) new EditorAttribute(typeof (BooleanEditor), typeof (UITypeEditor)));
        break;
      case "ScrollAlwaysVisible":
        attributes.Add((Attribute) new DefaultValueAttribute(false));
        attributes.Add((Attribute) new TypeConverterAttribute(typeof (System.ComponentModel.BooleanConverter)));
        attributes.Add((Attribute) new EditorAttribute(typeof (BooleanEditor), typeof (UITypeEditor)));
        break;
      case "AttributeInfo":
        attributes.Add((Attribute) new FieldTypesAttribute(new FieldTypes[3]
        {
          FieldTypes.ftString,
          FieldTypes.ftInteger,
          FieldTypes.ftDouble
        }));
        attributes.Add((Attribute) new MultiValueModesAttribute(new MultiValueModes[1]
        {
          MultiValueModes.MultiValuesFromList
        }));
        break;
    }
  }
}
