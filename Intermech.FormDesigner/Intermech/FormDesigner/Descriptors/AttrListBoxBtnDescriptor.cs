// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Descriptors.AttrListBoxBtnDescriptor
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.FormDesigner.Wrappers;
using Intermech.Localization;
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
internal class AttrListBoxBtnDescriptor(Control ctrl) : FormDesignerControlDescriptor(ctrl)
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
      "DisabledInDesign",
      "HorizontalScrollbar",
      "Sorted",
      "TabIndex",
      "TabStop"
    };
    propertiesNames["Data"] = new List<string>()
    {
      "AttributeInfo",
      "CanAddAttribute",
      "ParentPoint",
      "SelectFromImbase",
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
      case "AttributeInfo":
        attributes.Add((Attribute) new FieldTypesAttribute(new FieldTypes[2]
        {
          FieldTypes.ftObjectLink,
          FieldTypes.ftObjectLinkByID
        }));
        attributes.Add((Attribute) new MultiValueModesAttribute(new MultiValueModes[1]
        {
          MultiValueModes.MultiValues
        }));
        break;
      case "SelectFromImbase":
        attributes.Add((Attribute) new TypeConverterAttribute(typeof (YesNoConverter)));
        attributes.Add((Attribute) new EditorAttribute(typeof (YesNoEditor), typeof (UITypeEditor)));
        break;
    }
  }

  protected override void CreatePropertiesCollection()
  {
    base.CreatePropertiesCollection();
    PropertyDescriptorCollection originalPdc = this._originalPdc;
    string empty = string.Empty;
    Attribute[] attributes = new Attribute[5]
    {
      (Attribute) new CategoryAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropGroupName_Appearance")),
      (Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_ShowContextMenu_Name")),
      (Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_ShowContextMenu_Description")),
      (Attribute) new TypeConverterAttribute(typeof (Intermech.FormDesigner.Wrappers.BooleanConverter)),
      (Attribute) new EditorAttribute(typeof (BooleanEditor), typeof (UITypeEditor))
    };
    this._pdc.Add((PropertyDescriptor) new FormDesignerControlsPropertyDescriptor((object) this, originalPdc["ShowContextMenu"], attributes));
  }
}
