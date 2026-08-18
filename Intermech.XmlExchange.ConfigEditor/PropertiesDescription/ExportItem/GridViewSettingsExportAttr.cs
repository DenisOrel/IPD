// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ExportItem.GridViewSettingsExportAttr
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ExportItem;

internal class GridViewSettingsExportAttr : GridViewSettingsExportTypedItem
{
  private XmlExchangeExportAttr _exportAttr;

  public GridViewSettingsExportAttr(XmlExchangeExportAttr exportAttr, bool readOnly, bool inBase)
    : base((XmlExchangeExportTypedItem) exportAttr, readOnly, inBase)
  {
    this._exportAttr = exportAttr;
  }

  private void CreatePdc(Attribute[] attributes)
  {
    if (this._exportAttr == null)
      return;
    List<PropertyDescriptor> listProperty = new List<PropertyDescriptor>();
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) this._exportAttr, attributes, true);
    if (this._modeView != null && this._modeView.ThisExportConfig)
    {
      PropertyDescriptor propDesc1 = properties["Mode"];
      if (propDesc1 != null)
      {
        XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._exportAttr, propDesc1);
        propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this.category));
        propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Режимы экспорта/обработки атрибута"));
        propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("mode - Режимы экспорта/обработки атрибута"));
        propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
        listProperty.Add((PropertyDescriptor) propertyDescriptor);
      }
      PropertyDescriptor propDesc2 = properties["UserMeasureCode"];
      if (propDesc2 != null)
      {
        XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._exportAttr, propDesc2, typeof (string));
        propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this.category));
        propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Пользовательская ед. изменения"));
        propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("userMeasureCode - Пользовательская ед. изменения"));
        propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
        listProperty.Add((PropertyDescriptor) propertyDescriptor);
      }
      PropertyDescriptor propDesc3 = properties["UserFldType"];
      if (propDesc3 != null)
      {
        XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._exportAttr, propDesc3, typeof (string));
        propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this.category));
        propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Пользовательский тип данных"));
        propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("userFldType - Пользовательский тип данных"));
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
