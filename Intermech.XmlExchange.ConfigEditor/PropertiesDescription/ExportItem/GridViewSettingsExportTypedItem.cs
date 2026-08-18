// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ExportItem.GridViewSettingsExportTypedItem
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ExportItem;

internal abstract class GridViewSettingsExportTypedItem : GridViewSettingsExportUserItem
{
  protected string category = "Системные свойства";
  private XmlExchangeExportTypedItem _exportTypedBase;
  private bool _inBase;

  protected GridViewSettingsExportTypedItem(
    XmlExchangeExportTypedItem exportTypedItem,
    bool readOnly,
    bool inBase)
    : base((XmlExchangeExportUserItem) exportTypedItem, readOnly)
  {
    this._exportTypedBase = exportTypedItem;
    this._inBase = inBase;
  }

  private void CreatePdc(Attribute[] attributes)
  {
    if (this._exportTypedBase == null)
      return;
    List<PropertyDescriptor> listProperty = new List<PropertyDescriptor>();
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) this._exportTypedBase, attributes, true);
    PropertyDescriptor propDesc1 = properties["TypeID"];
    if (propDesc1 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._exportTypedBase, propDesc1);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this.category));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Идентификатор типа"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("id - Идентификатор типа в базе"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(this._inBase));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc2 = properties["TypeGuid"];
    if (propDesc2 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._exportTypedBase, propDesc2);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this.category));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Глобальный идентификатор типа"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("guid - Глобальный идентификатор типа в базе"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(this._inBase));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc3 = properties["TypeName"];
    if (propDesc3 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._exportTypedBase, propDesc3);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this.category));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Наименование типа"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("name - Наименование типа в базе"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(this._inBase));
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
