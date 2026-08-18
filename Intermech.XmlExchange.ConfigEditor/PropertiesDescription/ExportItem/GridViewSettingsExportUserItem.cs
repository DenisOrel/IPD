// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ExportItem.GridViewSettingsExportUserItem
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ExportItem;

internal abstract class GridViewSettingsExportUserItem : GridViewSettingsExportItem
{
  private string _category = "Пользовательские свойства";
  private XmlExchangeExportUserItem _exportUserItem;

  protected GridViewSettingsExportUserItem(XmlExchangeExportUserItem exportUserItem, bool readOnly)
    : base((XmlExchangeExportItem) exportUserItem, readOnly)
  {
    this._exportUserItem = exportUserItem;
  }

  private void CreatePdc(Attribute[] attributes)
  {
    if (this._exportUserItem == null)
      return;
    List<PropertyDescriptor> listProperty = new List<PropertyDescriptor>();
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) this._exportUserItem, attributes, true);
    if (this._modeView != null && this._modeView.UserDataOnly)
    {
      PropertyDescriptor propDesc1 = properties["UserID"];
      if (propDesc1 != null)
      {
        XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._exportUserItem, propDesc1);
        propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._category));
        propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Пользовательский идентификатор типа"));
        propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("user_id - Пользовательский идентификатор типа"));
        propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
        listProperty.Add((PropertyDescriptor) propertyDescriptor);
      }
      PropertyDescriptor propDesc2 = properties["UserName"];
      if (propDesc2 != null)
      {
        XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._exportUserItem, propDesc2);
        propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._category));
        propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Пользовательское наименование типа"));
        propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("user_name - Пользовательское наименование типа"));
        propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
        listProperty.Add((PropertyDescriptor) propertyDescriptor);
      }
      PropertyDescriptor propDesc3 = properties["UserAlias"];
      if (propDesc3 != null)
      {
        XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._exportUserItem, propDesc3);
        propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._category));
        propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Пользовательский псевдоним типа"));
        propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("user_alias - Пользовательский псевдоним типа"));
        propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
        listProperty.Add((PropertyDescriptor) propertyDescriptor);
      }
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
