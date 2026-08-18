// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ImportItem.GridViewSettingsImportRuleSearch
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.XmlExchange.ConfigEditor.ImportConfig;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ImportItem;

internal class GridViewSettingsImportRuleSearch(
  XmlExchangeImportRuleSearch ruleSearch,
  bool readOnly,
  bool inBase) : GridViewSettingsImportObjectType((XmlExchangeImportObjectType) ruleSearch, readOnly, inBase)
{
  private string _category = "Правило поиска";

  public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
  {
    if (this._pdc == null)
    {
      base.GetProperties(attributes);
      this.CreatePdc(attributes);
    }
    return this._pdc ?? new PropertyDescriptorCollection((PropertyDescriptor[]) null);
  }

  private void CreatePdc(Attribute[] attributes)
  {
    if (this._importItem == null)
      return;
    List<PropertyDescriptor> listProperty = new List<PropertyDescriptor>();
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) this._importItem, attributes, true);
    PropertyDescriptor propDesc1 = properties["Operation"];
    if (propDesc1 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._importItem, propDesc1);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._category));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Тип операции «склеивания»  условий на атрибуты при поиске объектов по умолчанию"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("operation - Тип операции «склеивания»  условий на атрибуты при поиске объектов по умолчанию"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc2 = properties["SearchType"];
    if (propDesc2 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._importItem, propDesc2);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._category));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Тип объекта, по которому производить поиск объектов в базе IPS"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("search_type - Глобальный идентификатор типа объекта, по которому производиться поиск объектов в базе IPS, если он не задан – тип определяем по значению, заданному в параметре guid"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (TypeObjectConverterBase)));
      propertyDescriptor.AddAttribute((Attribute) new EditorAttribute(typeof (TypeObjectSelected), typeof (UITypeEditor)));
      propertyDescriptor.AddAttribute((Attribute) new DefaultValueAttribute(typeof (Guid), Guid.Empty.ToString()));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    this.CreateCollectionProperty(listProperty);
  }
}
