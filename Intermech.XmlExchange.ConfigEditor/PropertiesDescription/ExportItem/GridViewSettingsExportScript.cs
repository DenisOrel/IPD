// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ExportItem.GridViewSettingsExportScript
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ExportItem;

internal class GridViewSettingsExportScript : GridViewSettingsExportItem
{
  private string _scriptCategory = "Скрипт задачи экспорта данных";
  private XmlExchangeExportScript _exportScript;

  public GridViewSettingsExportScript(XmlExchangeExportScript exportScript, bool readOnly)
    : base((XmlExchangeExportItem) exportScript, readOnly)
  {
    this._exportScript = exportScript;
  }

  private void CreatePdc(Attribute[] attributes)
  {
    if (this._exportScript == null)
      return;
    List<PropertyDescriptor> listProperty = new List<PropertyDescriptor>();
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) this._exportScript, attributes, true);
    PropertyDescriptor propDesc1 = properties["ScriptName"];
    if (propDesc1 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._exportScript, propDesc1);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._scriptCategory));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Наименование скрипта"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("Script name - Наименование скрипта"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc2 = properties["ScriptCode"];
    if (propDesc2 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._exportScript, propDesc2);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._scriptCategory));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Текст скрипта"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("Script code - Текст скрипта"));
      propertyDescriptor.AddAttribute((Attribute) new EditorAttribute(typeof (ScriptEditor), typeof (UITypeEditor)));
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
