// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Descriptors.AttrButtonDescriptor
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.FormDesigner.Actions;
using Intermech.FormDesigner.Wrappers;
using Intermech.Interfaces;
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
internal class AttrButtonDescriptor(Control ctrl) : FormDesignerControlDescriptor(ctrl)
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="component"></param>
  /// <param name="e"></param>
  private void OnFormDesignerAction_AfterSetValue(object component, SetValueEventArgs e)
  {
    this.FormDesignerAction_PropertyValueChanged((object) this._ctrl, e.Value as FormDesignerAction);
  }

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
      "FlatStyle",
      "Font",
      "ForeColor",
      "Hint",
      "Image",
      "ImageAlign",
      "Text",
      "TextAlign"
    };
    propertiesNames["Behavior"] = new List<string>()
    {
      "AlwaysEnabled",
      "TabIndex",
      "TabStop"
    };
    propertiesNames["Data"] = new List<string>()
    {
      "FormDesignerAction",
      "FormDesignerActionParams",
      "Tag"
    };
    propertiesNames["Layout"] = new List<string>()
    {
      "Anchor",
      "AutoSize",
      "AutoSizeMode",
      "Dock",
      "Location",
      "Margin",
      "MaximumSize",
      "MinimumSize",
      "Padding",
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
    // ISSUE: reference to a compiler-generated method
    switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(propertyName))
    {
      case 121036118:
        if (!(propertyName == "Padding"))
          break;
        attributes.Add((Attribute) new DefaultValueAttribute(0));
        break;
      case 725424655:
        if (!(propertyName == "FlatStyle"))
          break;
        attributes.Add((Attribute) new DefaultValueAttribute((object) FlatStyle.Standard));
        attributes.Add((Attribute) new TypeConverterAttribute(typeof (FlatStyleConverter)));
        attributes.Add((Attribute) new EditorAttribute(typeof (FlatStyleEditor), typeof (UITypeEditor)));
        break;
      case 1079093535:
        if (!(propertyName == "TextAlign"))
          break;
        attributes.Add((Attribute) new TypeConverterAttribute(typeof (ContentAlignmentConverter)));
        break;
      case 1808719973:
        if (!(propertyName == "AlwaysEnabled"))
          break;
        attributes.Add((Attribute) new TypeConverterAttribute(typeof (YesNoConverter)));
        attributes.Add((Attribute) new EditorAttribute(typeof (BooleanEditor), typeof (UITypeEditor)));
        break;
      case 2804299692:
        if (!(propertyName == "FormDesignerAction"))
          break;
        attributes.Add((Attribute) new TypeConverterAttribute(typeof (FormDesignerActionTypeConverter)));
        attributes.Add((Attribute) new EditorAttribute(typeof (FormDesignerActionUITypeEditor), typeof (UITypeEditor)));
        attributes.Add((Attribute) new RefreshPropertiesAttribute(RefreshProperties.All));
        break;
      case 3301734811:
        if (!(propertyName == "Margin"))
          break;
        attributes.Add((Attribute) new DefaultValueAttribute(3));
        break;
      case 3405419578:
        if (!(propertyName == "FormDesignerActionParams"))
          break;
        attributes.Add((Attribute) new DefaultValueAttribute((string) null));
        break;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void AfterCreatePropertiesCollection()
  {
    base.AfterCreatePropertiesCollection();
    FormDesignerPropertyDescriptor propertyDescriptor = this._pdc["FormDesignerAction"] as FormDesignerPropertyDescriptor;
    propertyDescriptor.AfterSetValue += new EventHandler<SetValueEventArgs>(this.OnFormDesignerAction_AfterSetValue);
    this.AnalizeAction((object) this._ctrl, propertyDescriptor.GetValue((object) this._ctrl) as FormDesignerAction);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="component"></param>
  /// <param name="action"></param>
  private void FormDesignerAction_PropertyValueChanged(object component, FormDesignerAction action)
  {
    if (this._pdc["Text"] != null)
      this._pdc["Text"].SetValue(component, (object) action.ActionName);
    this.AnalizeAction(component, action);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="component"></param>
  /// <param name="action"></param>
  private void AnalizeAction(object component, FormDesignerAction action)
  {
    (this._pdc["AlwaysEnabled"] as FormDesignerPropertyDescriptor).SetVisible(action.ActionGuid == new Guid("9c9a974e-5067-4425-bb92-eab0b07e170d"));
    FormDesignerPropertyDescriptor propertyDescriptor = this._pdc["FormDesignerActionParams"] as FormDesignerPropertyDescriptor;
    IFormDesignerActionParams component1 = propertyDescriptor.GetValue(component) as IFormDesignerActionParams;
    System.Type converterType = (System.Type) null;
    System.Type editorType = (System.Type) null;
    if (component1 != null)
    {
      TypeConverter converter = TypeDescriptor.GetConverter((object) component1);
      if (converter != null)
        converterType = converter.GetType();
      if (TypeDescriptor.GetEditor((object) component1, typeof (UITypeEditor)) is UITypeEditor editor)
        editorType = editor.GetType();
    }
    propertyDescriptor.SetConverter(converterType);
    propertyDescriptor.SetEditor(editorType);
  }
}
