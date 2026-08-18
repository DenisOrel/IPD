// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ExportItem.GridViewSettingsExportExtension
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;
using Intermech.Interfaces.XmlExchange.Settings.Export.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ExportItem;

internal class GridViewSettingsExportExtension : GridViewSettingsExportItem
{
  private protected string category = "Расширение задачи экспорта данных";
  private XmlExchangeExportExtension _exportExtension;

  public GridViewSettingsExportExtension(XmlExchangeExportExtension exportExtension, bool readOnly)
    : base((XmlExchangeExportItem) exportExtension, readOnly)
  {
    this._exportExtension = exportExtension;
  }

  private void CreatePdc(Attribute[] attributes)
  {
    if (this._exportExtension == null)
      return;
    List<PropertyDescriptor> listProperty = new List<PropertyDescriptor>();
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) this._exportExtension, attributes, true);
    PropertyDescriptor propDesc1 = properties["Guid"];
    if (propDesc1 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._exportExtension, propDesc1);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this.category));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Глобальный идентификатор расширения экспорта"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("extention guid - Глобальный идентификатор расширения экспорта"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc2 = properties["Name"];
    if (propDesc2 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._exportExtension, propDesc2);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this.category));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Наименование расширения экспорта"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("name - Наименование расширения экспорта"));
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
