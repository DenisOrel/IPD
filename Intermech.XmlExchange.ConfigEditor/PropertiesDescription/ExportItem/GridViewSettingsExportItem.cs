// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ExportItem.GridViewSettingsExportItem
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.FormDesigner.Wrappers;
using Intermech.Interfaces.XmlExchange;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ExportItem;

internal abstract class GridViewSettingsExportItem : ICustomTypeDescriptor, IConfigItemProperties
{
  private readonly string _сategory = "Прочие свойства";
  private protected PropertyDescriptorCollection _pdc;
  private protected XmlExchangeExportItem _exportItem;
  private protected ConfigEditorModeView _modeView;

  public GridViewSettingsExportItem(XmlExchangeExportItem exportBase, bool readOnly)
  {
    this._exportItem = exportBase;
    this.ReadOnlyProperties(readOnly);
    this._modeView = ConfigEditorModeView.GetModeView();
  }

  private void CreatePdc(Attribute[] attributes)
  {
    if (this._exportItem == null)
      return;
    List<PropertyDescriptor> listProperty = new List<PropertyDescriptor>();
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) this._exportItem, attributes, true);
    PropertyDescriptor propDesc1 = properties["Comments"];
    if (propDesc1 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._exportItem, propDesc1);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._сategory));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Комментарий"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("сomments - Комментарий"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    if (this._modeView != null && this._modeView.ThisExportConfig)
    {
      PropertyDescriptor propDesc2 = properties["Enabled"];
      if (propDesc2 != null)
      {
        XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._exportItem, propDesc2);
        propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this._сategory));
        propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Выполнять"));
        propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("enabled - Выполнять"));
        propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
        propertyDescriptor.AddAttribute((Attribute) new EditorAttribute(typeof (BooleanEditor), typeof (UITypeEditor)));
        propertyDescriptor.AddAttribute((Attribute) new TypeConverterAttribute(typeof (ConfigEditorBoolConverter)));
        listProperty.Add((PropertyDescriptor) propertyDescriptor);
      }
    }
    this.CreateCollectionProperty(listProperty);
  }

  public AttributeCollection GetAttributes()
  {
    return TypeDescriptor.GetAttributes((object) this._exportItem, true);
  }

  public string GetClassName() => TypeDescriptor.GetClassName((object) this._exportItem, true);

  public string GetComponentName()
  {
    return TypeDescriptor.GetComponentName((object) this._exportItem, true);
  }

  public TypeConverter GetConverter()
  {
    return TypeDescriptor.GetConverter((object) this._exportItem, true);
  }

  public EventDescriptor GetDefaultEvent()
  {
    return TypeDescriptor.GetDefaultEvent((object) this._exportItem, true);
  }

  public PropertyDescriptor GetDefaultProperty()
  {
    return TypeDescriptor.GetDefaultProperty((object) this._exportItem, true);
  }

  public object GetEditor(Type editorBaseType)
  {
    return TypeDescriptor.GetEditor((object) this._exportItem, editorBaseType, true);
  }

  public EventDescriptorCollection GetEvents(Attribute[] attributes)
  {
    return TypeDescriptor.GetEvents((object) this._exportItem, attributes, true);
  }

  public EventDescriptorCollection GetEvents()
  {
    return TypeDescriptor.GetEvents((object) this._exportItem, true);
  }

  public virtual PropertyDescriptorCollection GetProperties(Attribute[] attributes)
  {
    if (this._pdc == null)
      this.CreatePdc(attributes);
    return this._pdc ?? new PropertyDescriptorCollection((PropertyDescriptor[]) null);
  }

  public virtual PropertyDescriptorCollection GetProperties()
  {
    return this.GetProperties(new Attribute[0]);
  }

  public object GetPropertyOwner(PropertyDescriptor pd)
  {
    return pd is XmlConfigPropertyDescriptor propertyDescriptor ? propertyDescriptor.Owner : (object) this._exportItem;
  }

  private void ResetOldValues()
  {
    if (this._pdc == null)
      return;
    foreach (PropertyDescriptor propertyDescriptor1 in this._pdc)
    {
      if (propertyDescriptor1 is XmlConfigPropertyDescriptor propertyDescriptor2)
        propertyDescriptor2.ResetOldValue((object) this._exportItem);
    }
  }

  private void SetOldValues()
  {
    if (this._pdc == null)
      return;
    foreach (PropertyDescriptor propertyDescriptor1 in this._pdc)
    {
      if (propertyDescriptor1 is XmlConfigPropertyDescriptor propertyDescriptor2)
        propertyDescriptor2.SetOldValue((object) this._exportItem);
    }
  }

  public void SaveSettings() => this.ResetOldValues();

  public void ResetSettings() => this.SetOldValues();

  public void ReadOnlyProperties(bool readOnly)
  {
    if (!readOnly)
      return;
    TypeDescriptor.AddAttributes((object) this._exportItem, (Attribute) new ReadOnlyAttribute(true));
  }

  protected void CreateCollectionProperty(List<PropertyDescriptor> listProperty)
  {
    if (this._pdc != null)
    {
      foreach (PropertyDescriptor propertyDescriptor in listProperty)
        this._pdc.Add(propertyDescriptor);
    }
    else
      this._pdc = new PropertyDescriptorCollection(listProperty.ToArray());
  }
}
