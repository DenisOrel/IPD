// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ExportItem.GridViewSettingsExportObj
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ExportItem;

internal class GridViewSettingsExportObj : GridViewSettingsExportAttributable
{
  private XmlExchangeExportObj _exportObj;

  public GridViewSettingsExportObj(XmlExchangeExportObj exportObj, bool readOnly, bool inBase)
    : base((XmlExchangeExportAttributable) exportObj, readOnly, inBase)
  {
    this._exportObj = exportObj;
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

  private void CreatePdc(Attribute[] attributes)
  {
    if (this._exportObj == null)
      return;
    List<PropertyDescriptor> listProperty = new List<PropertyDescriptor>();
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) this._exportObj, attributes, true);
    if (this._modeView != null && this._modeView.ThisExportConfig)
    {
      PropertyDescriptor propDesc = properties["ObjModes"];
      if (propDesc != null)
      {
        XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._exportObj, propDesc);
        propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this.category));
        propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Режим выгрузки объекта"));
        propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("objmodes - Режим выгрузки объекта"));
        propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
        listProperty.Add((PropertyDescriptor) propertyDescriptor);
      }
    }
    this.CreateCollectionProperty(listProperty);
  }
}
