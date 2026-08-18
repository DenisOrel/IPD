// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ImportItem.GridViewSettingsImportAttrType
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.XmlExchange.ConfigEditor.ImportConfig;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ImportItem;

internal class GridViewSettingsImportAttrType : GridViewSettingsImportItem
{
  private string _categoryAttribute = "Атрибут";
  private string _categorySetting = "Настройки";
  private bool _inBase;

  public GridViewSettingsImportAttrType(
    XmlExchangeImportAttrTypeBase attrType,
    bool readOnly,
    bool inBase)
    : base((XmlExchangeImportItem) attrType, readOnly)
  {
    this._inBase = inBase;
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

  private new void CreatePdc(Attribute[] attributes)
  {
    if (this._importItem == null)
      return;
    List<PropertyDescriptor> listProperty = new List<PropertyDescriptor>();
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) this._importItem, attributes, true);
    PropertyDescriptor propDesc1 = properties["Name"];
    if (propDesc1 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._importItem, propDesc1);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._categoryAttribute));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Наименование типа"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("name - Наименование типа в базе"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(this._inBase));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc2 = properties["Guid"];
    if (propDesc2 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._importItem, propDesc2);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._categoryAttribute));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Глобальный идентификатор типа"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("guid - Глобальный идентификатор типа в базе"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(this._inBase));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc3 = properties["Operation"];
    if (propDesc3 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._importItem, propDesc3);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._categorySetting));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Тип операции «склеивания»  условий на текущий атрибут при поиске объектов"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("operation - Тип операции «склеивания»  условий на текущий атрибут при поиске объектов"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc4 = properties["CaseSensitive"];
    if (propDesc4 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._importItem, propDesc4);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._categorySetting));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Режим «чувствительности» к регистру при поиске по атрибуту"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("casesensitive - Режим «чувствительности» к регистру при поиске по атрибуту"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc5 = properties["UserID"];
    if (propDesc5 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._importItem, propDesc5);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._categorySetting));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Идентификатор типа атрибута в XML"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("user_id - Идентификатор типа атрибута в XML, по значению которого производиться поиск в базе IPS"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc6 = properties["Value"];
    if (propDesc6 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._importItem, propDesc6);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._categorySetting));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Фиксированное значение (константа)"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("value - Фиксированное значение (константа), которое будет использоваться вместо значения атрибута при поиске в базе"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc7 = properties["Order"];
    if (propDesc7 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._importItem, propDesc7);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._categorySetting));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Приоритет (последовательность) обработки атрибута при поиске объекта"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("order - Приоритет (последовательность) обработки атрибута при поиске объекта"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    this.CreateCollectionProperty(listProperty);
  }
}
