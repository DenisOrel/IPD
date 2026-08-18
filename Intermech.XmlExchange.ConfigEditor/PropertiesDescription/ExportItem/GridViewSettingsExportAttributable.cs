// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ExportItem.GridViewSettingsExportAttributable
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ExportItem;

internal class GridViewSettingsExportAttributable : GridViewSettingsExportTypedItem
{
  private XmlExchangeExportAttributable _exportAttributable;

  public GridViewSettingsExportAttributable(
    XmlExchangeExportAttributable exportAttributable,
    bool readOnly,
    bool inBase)
    : base((XmlExchangeExportTypedItem) exportAttributable, readOnly, inBase)
  {
    this._exportAttributable = exportAttributable;
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
    if (this._exportAttributable == null)
      return;
    List<PropertyDescriptor> listProperty = new List<PropertyDescriptor>();
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) this._exportAttributable, attributes, true);
    if (this._modeView != null && this._modeView.ThisExportConfig)
    {
      PropertyDescriptor propDesc = properties["AttrMode"];
      if (propDesc != null)
      {
        XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._exportAttributable, propDesc);
        propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this.category));
        propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Режим выгрузки атрибутов"));
        propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("attrmode - Режим выгрузки атрибутов"));
        propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
        listProperty.Add((PropertyDescriptor) propertyDescriptor);
      }
    }
    this.CreateCollectionProperty(listProperty);
  }
}
