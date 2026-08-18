// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ExportItem.GridViewSettingsDefAttrValue
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ExportItem;

internal class GridViewSettingsDefAttrValue : GridViewSettingsExportItem
{
  private XmlExchangeExportDefAttrValue _defAttrValue;

  public GridViewSettingsDefAttrValue(XmlExchangeExportDefAttrValue defAttrValue, bool readOnly)
    : base((XmlExchangeExportItem) defAttrValue, readOnly)
  {
    this._defAttrValue = defAttrValue;
  }

  private void CreatePdc(Attribute[] attributes)
  {
    if (this._defAttrValue == null)
      return;
    string category1 = "Настройки атрибута";
    string category2 = "Значение атрибута";
    List<PropertyDescriptor> listProperty = new List<PropertyDescriptor>();
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) this._defAttrValue, attributes, true);
    PropertyDescriptor propDesc1 = properties["UserID"];
    if (propDesc1 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._defAttrValue, propDesc1, typeof (int));
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(category1));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Идентификатор типа атрибута"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("user_id - Идентификатор типа атрибута"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc2 = properties["UserName"];
    if (propDesc2 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._defAttrValue, propDesc2, typeof (string));
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(category1));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Наименование типа атрибута"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("user_name - Наименование типа атрибута"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc3 = properties["UserAlias"];
    if (propDesc3 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._defAttrValue, propDesc3, typeof (string));
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(category1));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Псевдоним типа атрибута"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("user_alias - Псевдоним типа атрибута"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc4 = properties["UserFldType"];
    if (propDesc4 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._defAttrValue, propDesc4, typeof (FieldTypes));
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(category1));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Тип хранимых данных"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("user_type - Тип хранимых данных"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      propertyDescriptor.AddAttribute((Attribute) new EditorAttribute(typeof (FieldTypeSelected), typeof (UITypeEditor)));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (FieldTypeConverter)));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc5 = properties["Guid"];
    if (propDesc5 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._defAttrValue, propDesc5, typeof (Guid));
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(category1));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Глобальный идентификатор типа атрибута"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("Глобальный идентификатор типа атрибута"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc6 = properties["Value"];
    if (propDesc6 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._defAttrValue, propDesc6, typeof (string));
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(category2));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Значение атрибута"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("f_value - Значение атрибута"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc7 = properties["IntegerValue"];
    if (propDesc7 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._defAttrValue, propDesc7, typeof (long));
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(category2));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Целочисленное значение атрибута"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("Целочисленное значение атрибута"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc8 = properties["DoubleValue"];
    if (propDesc8 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._defAttrValue, propDesc8, typeof (double));
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(category2));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Вещественное значение атрибута"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("Вещественное значение атрибута"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc9 = properties["StringValue"];
    if (propDesc9 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._defAttrValue, propDesc9, typeof (string));
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(category2));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Строковое значение атрибута"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("f_string_value - Строковое значение атрибута"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    this.CreateCollectionProperty(listProperty);
  }

  public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
  {
    if (this._pdc == null)
    {
      base.GetProperties(attributes);
      this.CreatePdc(attributes);
    }
    return this._pdc ?? new PropertyDescriptorCollection((PropertyDescriptor[]) null);
  }
}
