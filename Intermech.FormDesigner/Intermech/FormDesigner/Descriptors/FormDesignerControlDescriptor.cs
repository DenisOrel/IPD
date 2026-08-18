// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Descriptors.FormDesignerControlDescriptor
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
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Descriptors;

internal abstract class FormDesignerControlDescriptor : ICustomTypeDescriptor, IWrapper
{
  protected PropertyDescriptorCollection _originalPdc;
  protected PropertyDescriptorCollection _pdc = new PropertyDescriptorCollection((PropertyDescriptor[]) null);
  protected Control _ctrl;

  /// <summary>Конструктор.</summary>
  /// <param name="ctrl">Контрол</param>
  public FormDesignerControlDescriptor(Control ctrl)
  {
    this._ctrl = ctrl;
    this._originalPdc = TypeDescriptor.GetProperties((object) ctrl, true);
    this.CreatePropertiesCollection();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public AttributeCollection GetAttributes()
  {
    return TypeDescriptor.GetAttributes((object) this._ctrl, true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public string GetClassName() => TypeDescriptor.GetClassName((object) this._ctrl, true);

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public string GetComponentName() => TypeDescriptor.GetComponentName((object) this._ctrl, true);

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public TypeConverter GetConverter() => TypeDescriptor.GetConverter((object) this._ctrl, true);

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public EventDescriptor GetDefaultEvent()
  {
    return TypeDescriptor.GetDefaultEvent((object) this._ctrl, true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public PropertyDescriptor GetDefaultProperty()
  {
    return TypeDescriptor.GetDefaultProperty((object) this._ctrl, true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="editorBaseType"></param>
  /// <returns></returns>
  public object GetEditor(System.Type editorBaseType)
  {
    return TypeDescriptor.GetEditor((object) this._ctrl, editorBaseType, true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  public EventDescriptorCollection GetEvents(Attribute[] attributes)
  {
    return TypeDescriptor.GetEvents((object) this._ctrl, attributes, true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public EventDescriptorCollection GetEvents()
  {
    return TypeDescriptor.GetEvents((object) this._ctrl, true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  public PropertyDescriptorCollection GetProperties(Attribute[] attributes) => this.GetProperties();

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public PropertyDescriptorCollection GetProperties()
  {
    return this._pdc ?? new PropertyDescriptorCollection((PropertyDescriptor[]) null);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="pd"></param>
  /// <returns></returns>
  public object GetPropertyOwner(PropertyDescriptor pd) => (object) this._ctrl;

  /// <summary>
  /// 
  /// </summary>
  public object BaseClass => (object) this._ctrl;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnAttributeInfo_AfterSetValue(object sender, SetValueEventArgs e)
  {
    this.AttributeInfo_AfterSetValue(sender, e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnAttributeInfo_AfterResetValue(object sender, EventArgs e)
  {
    this.AttributeInfo_AfterResetValue(sender, e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnNameProperty_AfterSetValue(object sender, SetValueEventArgs e)
  {
    this.NameProperty_AfterSetValue(sender, e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="propertiesNames"></param>
  protected virtual void CreatePropertiesCollection()
  {
    Dictionary<string, List<string>> propertiesNames = new Dictionary<string, List<string>>();
    this.FillPropertiesNames(propertiesNames);
    string empty = string.Empty;
    foreach (KeyValuePair<string, List<string>> keyValuePair in propertiesNames)
    {
      string categoryName = $"Attribute_Category_{keyValuePair.Key}";
      foreach (string propertyName in keyValuePair.Value)
        this._pdc.Add((PropertyDescriptor) this.GetDescriptor(categoryName, propertyName));
    }
    this.AfterCreatePropertiesCollection();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="categoryName"></param>
  /// <param name="propertyName"></param>
  /// <returns></returns>
  private FormDesignerPropertyDescriptor GetDescriptor(string categoryName, string propertyName)
  {
    List<Attribute> attributes = new List<Attribute>()
    {
      (Attribute) new CustomCategory(categoryName),
      (Attribute) new CustomDisplayName($"Attribute_{propertyName}_Name"),
      (Attribute) new CustomDescription($"Attribute_{propertyName}_Description")
    };
    this.FillPropertyAttributes(propertyName, attributes);
    return new FormDesignerPropertyDescriptor((object) this, this._originalPdc[propertyName], attributes.ToArray());
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="propertiesNames"></param>
  protected virtual void FillPropertiesNames(Dictionary<string, List<string>> propertiesNames)
  {
    propertiesNames["Design"] = new List<string>()
    {
      "Name"
    };
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="propertyName"></param>
  /// <param name="attributes"></param>
  protected virtual void FillPropertyAttributes(string propertyName, List<Attribute> attributes)
  {
    // ISSUE: reference to a compiler-generated method
    switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(propertyName))
    {
      case 21126864:
        if (!(propertyName == "AutoSizeMode"))
          return;
        attributes.Add((Attribute) new DefaultValueAttribute((object) AutoSizeMode.GrowOnly));
        attributes.Add((Attribute) new TypeConverterAttribute(typeof (AutoSizeModeConverter)));
        attributes.Add((Attribute) new EditorAttribute(typeof (AutoSizeModeEditor), typeof (UITypeEditor)));
        return;
      case 121036118:
        if (!(propertyName == "Padding"))
          return;
        break;
      case 266367750:
        int num = propertyName == "Name" ? 1 : 0;
        return;
      case 599956904:
        if (!(propertyName == "TabIndex"))
          return;
        attributes.Add((Attribute) new DefaultValueAttribute(-1));
        return;
      case 777198197:
        if (!(propertyName == "BackColor"))
          return;
        attributes.Add((Attribute) new DefaultValueAttribute((object) SystemColors.Control));
        return;
      case 825767886:
        if (!(propertyName == "MinimumSize"))
          return;
        goto label_85;
      case 829894807:
        if (!(propertyName == "AutoSize"))
          return;
        attributes.Add((Attribute) new DefaultValueAttribute(false));
        attributes.Add((Attribute) new TypeConverterAttribute(typeof (Intermech.FormDesigner.Wrappers.BooleanConverter)));
        attributes.Add((Attribute) new EditorAttribute(typeof (BooleanEditor), typeof (UITypeEditor)));
        return;
      case 957039752:
        if (!(propertyName == "BorderStyle"))
          return;
        attributes.Add((Attribute) new DefaultValueAttribute((object) BorderStyle.Fixed3D));
        attributes.Add((Attribute) new TypeConverterAttribute(typeof (BorderStyleConverter)));
        attributes.Add((Attribute) new EditorAttribute(typeof (BorderStyleEditor), typeof (UITypeEditor)));
        return;
      case 971403444:
        if (!(propertyName == "CanAddAttribute"))
          return;
        attributes.Add((Attribute) new DefaultValueAttribute(true));
        attributes.Add((Attribute) new TypeConverterAttribute(typeof (Intermech.FormDesigner.Wrappers.BooleanConverter)));
        attributes.Add((Attribute) new EditorAttribute(typeof (BooleanEditor), typeof (UITypeEditor)));
        return;
      case 1041509726:
        if (!(propertyName == "Text"))
          return;
        attributes.Add((Attribute) new DefaultValueAttribute(""));
        return;
      case 1079093535:
        if (!(propertyName == "TextAlign"))
          return;
        attributes.Add((Attribute) new DefaultValueAttribute((object) ContentAlignment.MiddleCenter));
        return;
      case 1307034372:
        if (!(propertyName == "TabStop"))
          return;
        attributes.Add((Attribute) new DefaultValueAttribute(true));
        attributes.Add((Attribute) new TypeConverterAttribute(typeof (Intermech.FormDesigner.Wrappers.BooleanConverter)));
        attributes.Add((Attribute) new EditorAttribute(typeof (BooleanEditor), typeof (UITypeEditor)));
        return;
      case 1494001562:
        if (!(propertyName == "Image"))
          return;
        attributes.Add((Attribute) new ResetValueAttribute(true));
        attributes.Add((Attribute) new TypeConverterAttribute(typeof (Intermech.FormDesigner.Wrappers.ImageConverter)));
        attributes.Add((Attribute) new RefreshPropertiesAttribute(RefreshProperties.All));
        return;
      case 1539345862:
        if (!(propertyName == "Location"))
          return;
        attributes.Add((Attribute) new TypeConverterAttribute(typeof (Intermech.FormDesigner.Wrappers.PointConverter)));
        return;
      case 1613000528:
        if (!(propertyName == "MaximumSize"))
          return;
        goto label_85;
      case 2260826701:
        if (!(propertyName == "HorizontalScrollbar"))
          return;
        attributes.Add((Attribute) new DefaultValueAttribute(false));
        attributes.Add((Attribute) new TypeConverterAttribute(typeof (Intermech.FormDesigner.Wrappers.BooleanConverter)));
        attributes.Add((Attribute) new EditorAttribute(typeof (BooleanEditor), typeof (UITypeEditor)));
        return;
      case 2789707388:
        if (!(propertyName == "Size"))
          return;
        attributes.Add((Attribute) new TypeConverterAttribute(typeof (Intermech.FormDesigner.Wrappers.SizeConverter)));
        return;
      case 2809814704:
        if (!(propertyName == "Font"))
          return;
        attributes.Add((Attribute) new TypeConverterAttribute(typeof (Intermech.FormDesigner.Wrappers.FontConverter)));
        attributes.Add((Attribute) new EditorAttribute(typeof (FontEditor), typeof (UITypeEditor)));
        return;
      case 2815095226:
        if (!(propertyName == "Dock"))
          return;
        attributes.Add((Attribute) new DefaultValueAttribute((object) DockStyle.None));
        attributes.Add((Attribute) new TypeConverterAttribute(typeof (DockStyleConverter)));
        return;
      case 2936102910:
        if (!(propertyName == "ForeColor"))
          return;
        attributes.Add((Attribute) new DefaultValueAttribute((object) SystemColors.ControlText));
        return;
      case 3040892372:
        if (!(propertyName == "Anchor"))
          return;
        attributes.Add((Attribute) new DefaultValueAttribute((object) (AnchorStyles.Top | AnchorStyles.Left)));
        attributes.Add((Attribute) new TypeConverterAttribute(typeof (AnchorStylesConverter)));
        return;
      case 3064987242:
        if (!(propertyName == "UseInExpertSystem"))
          return;
        attributes.Add((Attribute) new DefaultValueAttribute(false));
        attributes.Add((Attribute) new TypeConverterAttribute(typeof (Intermech.FormDesigner.Wrappers.BooleanConverter)));
        attributes.Add((Attribute) new EditorAttribute(typeof (BooleanEditor), typeof (UITypeEditor)));
        return;
      case 3082893376:
        if (!(propertyName == "DisabledInDesign"))
          return;
        attributes.Add((Attribute) new DefaultValueAttribute(false));
        attributes.Add((Attribute) new TypeConverterAttribute(typeof (YesNoConverter)));
        attributes.Add((Attribute) new EditorAttribute(typeof (YesNoEditor), typeof (UITypeEditor)));
        return;
      case 3301734811:
        if (!(propertyName == "Margin"))
          return;
        break;
      case 3368697499:
        if (!(propertyName == "AttributeInfo"))
          return;
        attributes.Add((Attribute) new DefaultValueAttribute((string) null));
        attributes.Add((Attribute) new ResetValueAttribute(true));
        attributes.Add((Attribute) new TypeConverterAttribute(typeof (AttributeInfo2TypeNamesConverter)));
        attributes.Add((Attribute) new EditorAttribute(typeof (AttributeInfoEditor), typeof (UITypeEditor)));
        attributes.Add((Attribute) new RefreshPropertiesAttribute(RefreshProperties.All));
        return;
      case 3421787608:
        if (!(propertyName == "Hint"))
          return;
        attributes.Add((Attribute) new DefaultValueAttribute(""));
        return;
      case 3855197768:
        if (!(propertyName == "Sorted"))
          return;
        attributes.Add((Attribute) new DefaultValueAttribute(false));
        attributes.Add((Attribute) new TypeConverterAttribute(typeof (Intermech.FormDesigner.Wrappers.BooleanConverter)));
        attributes.Add((Attribute) new EditorAttribute(typeof (BooleanEditor), typeof (UITypeEditor)));
        return;
      case 3859239051:
        if (!(propertyName == "ImageAlign"))
          return;
        attributes.Add((Attribute) new DefaultValueAttribute((object) ContentAlignment.MiddleCenter));
        attributes.Add((Attribute) new TypeConverterAttribute(typeof (ContentAlignmentConverter)));
        return;
      case 4169356339:
        if (!(propertyName == "Tag"))
          return;
        attributes.Add((Attribute) new DefaultValueAttribute(""));
        attributes.Add((Attribute) new TypeConverterAttribute(typeof (StringConverter)));
        return;
      case 4292470505:
        if (!(propertyName == "ParentPoint"))
          return;
        attributes.Add((Attribute) new DefaultValueAttribute((object) AttributeDestinationPoint.Default));
        attributes.Add((Attribute) new TypeConverterAttribute(typeof (ParentPointConverter)));
        attributes.Add((Attribute) new EditorAttribute(typeof (ParentPointEditor), typeof (UITypeEditor)));
        return;
      default:
        return;
    }
    attributes.Add((Attribute) new TypeConverterAttribute(typeof (MarginPaddingConverter)));
    return;
label_85:
    attributes.Add((Attribute) new DefaultValueAttribute((object) new Size(0, 0)));
    attributes.Add((Attribute) new TypeConverterAttribute(typeof (Intermech.FormDesigner.Wrappers.SizeConverter)));
    attributes.Add((Attribute) new RefreshPropertiesAttribute(RefreshProperties.All));
  }

  /// <summary>
  /// 
  /// </summary>
  protected virtual void AfterCreatePropertiesCollection()
  {
    if (this._pdc["AttributeInfo"] is FormDesignerPropertyDescriptor propertyDescriptor)
    {
      propertyDescriptor.AfterSetValue += new EventHandler<SetValueEventArgs>(this.OnAttributeInfo_AfterSetValue);
      propertyDescriptor.AfterResetValue += new EventHandler(this.OnAttributeInfo_AfterResetValue);
    }
    (this._pdc["Name"] as FormDesignerPropertyDescriptor).AfterSetValue += new EventHandler<SetValueEventArgs>(this.OnNameProperty_AfterSetValue);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected virtual void AttributeInfo_AfterSetValue(object sender, SetValueEventArgs e)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected virtual void AttributeInfo_AfterResetValue(object sender, EventArgs e)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected virtual void NameProperty_AfterSetValue(object sender, SetValueEventArgs e)
  {
    if (this._ctrl.Site == null)
      return;
    this._ctrl.Site.Name = Convert.ToString(e.Value);
  }
}
