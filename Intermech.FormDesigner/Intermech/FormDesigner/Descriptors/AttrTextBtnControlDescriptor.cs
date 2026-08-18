// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Descriptors.AttrTextBtnControlDescriptor
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.FormDesigner.Attributes;
using Intermech.FormDesigner.Wrappers;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Descriptors;

/// <summary>Конструктор.</summary>
/// <param name="ctrl"></param>
internal class AttrTextBtnControlDescriptor(Control ctrl) : FormDesignerControlDescriptor(ctrl)
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
      "Hint",
      "Text",
      "TextAlign"
    };
    propertiesNames["Behavior"] = new List<string>()
    {
      "DisabledInDesign",
      "TabIndex",
      "TabStop"
    };
    propertiesNames["Data"] = new List<string>()
    {
      "AttributeInfo",
      "CanAddAttribute",
      "ParentPoint",
      "Tag",
      "DataSourceName",
      "SelectionGuid"
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
      case "AttributeInfo":
        attributes.Add((Attribute) new FieldTypesAttribute(new FieldTypes[3]
        {
          FieldTypes.ftObjectLink,
          FieldTypes.ftObjectLinkByID,
          FieldTypes.ftSystem
        }));
        attributes.Add((Attribute) new MultiValueModesAttribute(new MultiValueModes[1]));
        break;
      case "DataSourceName":
        attributes.Add((Attribute) new DefaultValueAttribute(""));
        attributes.Add((Attribute) new CategoryAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropGroupName_Data")));
        attributes.Add((Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropName_DataSourceName")));
        attributes.Add((Attribute) new EditorAttribute(typeof (AttrTextBtnDataSourceNameEditor), typeof (UITypeEditor)));
        break;
      case "SelectionGuid":
        attributes.Add((Attribute) new DefaultValueAttribute(typeof (Guid), Guid.Empty.ToString()));
        attributes.Add((Attribute) new ResetValueAttribute(true));
        attributes.Add((Attribute) new CategoryAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropGroupName_Data")));
        attributes.Add((Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("FormDesigner_ContextSelection")));
        attributes.Add((Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("FormDesigner_ContextSelection")));
        attributes.Add((Attribute) new TypeConverterAttribute(typeof (SelectionsConverter)));
        attributes.Add((Attribute) new EditorAttribute(typeof (SelectionsEditor), typeof (UITypeEditor)));
        break;
    }
  }
}
