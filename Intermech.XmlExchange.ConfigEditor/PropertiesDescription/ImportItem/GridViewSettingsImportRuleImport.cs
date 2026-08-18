// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ImportItem.GridViewSettingsImportRuleImport
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

internal class GridViewSettingsImportRuleImport(
  XmlExchangeImportRuleImport ruleImport,
  bool readOnly,
  bool inBase) : GridViewSettingsImportObjectType((XmlExchangeImportObjectType) ruleImport, readOnly, inBase)
{
  private string _category = "Правило импорта";

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
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Правило импорта"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("rule - Правило импорта"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc2 = properties["SkipExists"];
    if (propDesc2 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._importItem, propDesc2);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._category));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Импорт объекта, если найден существующий объект в базе"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("skipExists - Импорт объекта, если найден существующий объект в базе IPS. Настройка игнорируется для правила импорта: Импорт объектов данного типа не выполняется"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (SkipExistsConverter)));
      propertyDescriptor.AddAttribute((Attribute) new EditorAttribute(typeof (SkipExistsSelected), typeof (UITypeEditor)));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc3 = properties["Dictionary"];
    if (propDesc3 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._importItem, propDesc3);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._category));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Справочник Imbase, по которому требуется создавать новый объект"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("dictionary - Cправочник Imbase, по которому требуется создавать новый объект. Используется только  для правила: Создание объекта на основе справочников НСИ (Imbase)"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (GuidToCaptionConverter)));
      propertyDescriptor.AddAttribute((Attribute) new EditorAttribute(typeof (CatalogSelected), typeof (UITypeEditor)));
      propertyDescriptor.AddAttribute((Attribute) new DefaultValueAttribute(typeof (Guid), Guid.Empty.ToString()));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    this.CreateCollectionProperty(listProperty);
  }
}
