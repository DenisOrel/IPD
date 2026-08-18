// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ImportItem.GridViewSettingsImportRuleCreate
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

internal class GridViewSettingsImportRuleCreate(
  XmlExchangeImportRuleCreate ruleCreate,
  bool readOnly,
  bool inBase) : GridViewSettingsImportObjectType((XmlExchangeImportObjectType) ruleCreate, readOnly, inBase)
{
  private string _category = "Правило создания";

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
    PropertyDescriptor propDesc1 = properties["Rule"];
    if (propDesc1 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._importItem, propDesc1);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._category));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Правило создания"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("rule - Правило создания"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc2 = properties["LcStep"];
    if (propDesc2 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._importItem, propDesc2);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._category));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Глобальный идентификатор шага ЖЦ"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("lcStep - Глобальный идентификатор шага ЖЦ для схемы, назначенной указанному типу объекта IPS"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (LcStepConverter)));
      propertyDescriptor.AddAttribute((Attribute) new EditorAttribute(typeof (LcStepSelected), typeof (UITypeEditor)));
      propertyDescriptor.AddAttribute((Attribute) new DefaultValueAttribute(typeof (Guid), Guid.Empty.ToString()));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc3 = properties["VersionOwner"];
    if (propDesc3 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._importItem, propDesc3);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._category));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Правило назначения родительской версии объекта"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("version_owner - Правило назначения родительской версии объекта"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc4 = properties["VersionNo"];
    if (propDesc4 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._importItem, propDesc4);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._category));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Правило назначения версии объекта"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("version_no - Правило назначения версии объекта"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc5 = properties["VersionNoAttrId"];
    if (propDesc5 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._importItem, propDesc5, typeof (int));
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._category));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute(" Идентификатор типа атрибута в XML, по значению которого производиться поиск версии объекта в базе IPS"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("version_no_attr_id - Идентификатор типа атрибута в XML, по значению которого производиться поиск / назначение версии объекта в базе IPS. Если параметр не задан – номер версии определяется из поля объекта F_VERSION_ID"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    this.CreateCollectionProperty(listProperty);
  }
}
