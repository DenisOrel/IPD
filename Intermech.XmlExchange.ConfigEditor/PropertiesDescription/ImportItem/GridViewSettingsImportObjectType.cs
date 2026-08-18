// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ImportItem.GridViewSettingsImportObjectType
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.XmlExchange.ConfigEditor.ImportConfig;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ImportItem;

internal abstract class GridViewSettingsImportObjectType : GridViewSettingsImportItem
{
  private string _category = "Тип объекта";
  private bool _inBase;

  protected GridViewSettingsImportObjectType(
    XmlExchangeImportObjectType importObjType,
    bool readOnly,
    bool inBase)
    : base((XmlExchangeImportItem) importObjType, readOnly)
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
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._category));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Наименование типа"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("name - Наименование типа в базе"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(this._inBase));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc2 = properties["Guid"];
    if (propDesc2 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._importItem, propDesc2);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._category));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Глобальный идентификатор типа"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("guid - Глобальный идентификатор типа в базе"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(this._inBase));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    this.CreateCollectionProperty(listProperty);
  }
}
