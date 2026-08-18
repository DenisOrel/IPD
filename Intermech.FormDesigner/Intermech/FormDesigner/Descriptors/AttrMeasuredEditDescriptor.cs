// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Descriptors.AttrMeasuredEditDescriptor
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.FormDesigner.Attributes;
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
internal class AttrMeasuredEditDescriptor(Control ctrl) : FormDesignerControlDescriptor(ctrl)
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
      "DefaultMeasured",
      "DefaultValue",
      "ParentPoint",
      "Tag",
      "UseInExpertSystem"
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
      case "TextAlign":
        attributes.Add((Attribute) new TypeConverterAttribute(typeof (HorizontalAlignmentConverter)));
        attributes.Add((Attribute) new EditorAttribute(typeof (HorizontalAlignmentEditor), typeof (UITypeEditor)));
        break;
      case "AttributeInfo":
        attributes.Add((Attribute) new FieldTypesAttribute(new FieldTypes[1]
        {
          FieldTypes.ftMeasured
        }));
        attributes.Add((Attribute) new MultiValueModesAttribute(new MultiValueModes[1]));
        break;
      case "DefaultMeasured":
        attributes.Add((Attribute) new DefaultValueAttribute(""));
        attributes.Add((Attribute) new TypeConverterAttribute(typeof (UnitsConverter)));
        attributes.Add((Attribute) new EditorAttribute(typeof (UnitsEditor), typeof (UITypeEditor)));
        break;
      case "DefaultValue":
        attributes.Add((Attribute) new DefaultValueAttribute((string) null));
        attributes.Add((Attribute) new ResetValueAttribute(true));
        attributes.Add((Attribute) new EditorAttribute(typeof (MeasuredValueEditor), typeof (UITypeEditor)));
        break;
      case "Margin":
        attributes.Add((Attribute) new DefaultValueAttribute(3));
        break;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void AfterCreatePropertiesCollection()
  {
    base.AfterCreatePropertiesCollection();
    object obj = (this._pdc["AttributeInfo"] as FormDesignerPropertyDescriptor).GetValue((object) this._ctrl);
    bool isReadOnly = obj == null || obj == DBNull.Value;
    (this._pdc["DefaultMeasured"] as FormDesignerPropertyDescriptor).SetReadOnly(isReadOnly);
    (this._pdc["DefaultValue"] as FormDesignerPropertyDescriptor).SetReadOnly(isReadOnly);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected override void AttributeInfo_AfterSetValue(object sender, SetValueEventArgs e)
  {
    base.AttributeInfo_AfterSetValue(sender, e);
    object obj = e?.Value;
    this.AttributeInfo_PropertyValueChanged(sender, obj);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected override void AttributeInfo_AfterResetValue(object sender, EventArgs e)
  {
    base.AttributeInfo_AfterResetValue(sender, e);
    this.AttributeInfo_PropertyValueChanged(sender, (object) null);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="component"></param>
  /// <param name="value"></param>
  private void AttributeInfo_PropertyValueChanged(object component, object value)
  {
    bool isReadOnly = value == null;
    FormDesignerPropertyDescriptor propertyDescriptor1 = this._pdc["DefaultMeasured"] as FormDesignerPropertyDescriptor;
    propertyDescriptor1.ResetValue(component);
    propertyDescriptor1.SetReadOnly(isReadOnly);
    FormDesignerPropertyDescriptor propertyDescriptor2 = this._pdc["DefaultValue"] as FormDesignerPropertyDescriptor;
    propertyDescriptor2.ResetValue(component);
    propertyDescriptor2.SetReadOnly(isReadOnly);
  }
}
