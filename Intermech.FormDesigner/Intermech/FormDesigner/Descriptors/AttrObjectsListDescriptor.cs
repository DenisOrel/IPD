// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Descriptors.AttrObjectsListDescriptor
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

/// <summary>Конструктор.</summary>
/// <param name="ctrl">Контрол</param>
internal class AttrObjectsListDescriptor(Control ctrl) : FormDesignerControlDescriptor(ctrl)
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
      "TabIndex",
      "TabStop"
    };
    propertiesNames["Data"] = new List<string>()
    {
      "AttributeInfo",
      "CanAddAttribute",
      "ParentPoint",
      "SelectFromImbase",
      "Tag",
      "MaxCountValue"
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
    string category = LocalizationHolder.rm.GetString("FormDesigner_PropGroupName_Data");
    Attribute[] attributes1 = new Attribute[6]
    {
      (Attribute) new CategoryAttribute(category),
      (Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_Columns_Name")),
      (Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_Columns_Description")),
      (Attribute) new TypeConverterAttribute(typeof (ColumnCollectionConverter)),
      (Attribute) new EditorAttribute(typeof (ColumnCollectionEditor), typeof (UITypeEditor)),
      (Attribute) new RefreshPropertiesAttribute(RefreshProperties.All)
    };
    FormDesignerControlsPropertyDescriptor propertyDescriptor1 = new FormDesignerControlsPropertyDescriptor((object) this, originalPdc["ColumnCollection"], attributes1);
    propertyDescriptor1.SetCanReset(true);
    propertyDescriptor1.AfterSetValue += new PropertySetValue(this.OnAfterSetValue);
    propertyDescriptor1.AfterResetValue += new EventHandler(this.OnColumnCollection_AfterResetValue);
    this._pdc.Add((PropertyDescriptor) propertyDescriptor1);
    Attribute[] attributes2 = new Attribute[5]
    {
      (Attribute) new CategoryAttribute(category),
      (Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropName_ColumnsAliases")),
      (Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_ColumnsAliases")),
      (Attribute) new TypeConverterAttribute(typeof (ColumnCollectionConverter)),
      (Attribute) new EditorAttribute(typeof (ColumnsNamesEditor), typeof (UITypeEditor))
    };
    FormDesignerControlsPropertyDescriptor propertyDescriptor2 = new FormDesignerControlsPropertyDescriptor((object) this, originalPdc["ColumnsAliases"], attributes2);
    propertyDescriptor2.SetCanReset(true);
    this._pdc.Add((PropertyDescriptor) propertyDescriptor2);
    Attribute[] attributes3 = new Attribute[5]
    {
      (Attribute) new CategoryAttribute(category),
      (Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropName_UseColumnsAliases")),
      (Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_UseColumnsAliases")),
      (Attribute) new TypeConverterAttribute(typeof (Intermech.FormDesigner.Wrappers.BooleanConverter)),
      (Attribute) new EditorAttribute(typeof (BooleanEditor), typeof (UITypeEditor))
    };
    this._pdc.Add((PropertyDescriptor) new FormDesignerControlsPropertyDescriptor((object) this, originalPdc["UseColumnsAliases"], attributes3));
    Attribute[] attributes4 = new Attribute[5]
    {
      (Attribute) new CategoryAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropGroupName_Appearance")),
      (Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_ShowContextMenu_Name")),
      (Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_ShowContextMenu_Description")),
      (Attribute) new TypeConverterAttribute(typeof (Intermech.FormDesigner.Wrappers.BooleanConverter)),
      (Attribute) new EditorAttribute(typeof (BooleanEditor), typeof (UITypeEditor))
    };
    this._pdc.Add((PropertyDescriptor) new FormDesignerControlsPropertyDescriptor((object) this, originalPdc["ShowContextMenu"], attributes4));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="component"></param>
  /// <param name="e"></param>
  private void OnAfterSetValue(object component, SetValueEventArgs e)
  {
    if (e == null || e.PropertyDescriptor == null)
      return;
    int num = e.PropertyDescriptor.Name == "ColumnCollection" ? 1 : 0;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnColumnCollection_AfterResetValue(object sender, EventArgs e)
  {
    (this._pdc["ColumnsAliases"] as FormDesignerControlsPropertyDescriptor).ResetValue((object) this._ctrl);
  }
}
