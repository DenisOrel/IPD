// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ImportItem.GridViewSettingsImportItem
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.XmlExchange.ConfigEditor.ImportConfig;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ImportItem;

internal abstract class GridViewSettingsImportItem : ICustomTypeDescriptor, IConfigItemProperties
{
  private protected PropertyDescriptorCollection _pdc;
  private protected XmlExchangeImportItem _importItem;

  protected GridViewSettingsImportItem(XmlExchangeImportItem importItem, bool readOnly)
  {
    this._importItem = importItem;
    this.ReadOnlyProperties(readOnly);
  }

  private protected virtual void CreatePdc(Attribute[] attributes)
  {
    if (this._importItem == null)
      return;
    List<PropertyDescriptor> listProperty = new List<PropertyDescriptor>();
    PropertyDescriptor property = TypeDescriptor.GetProperties((object) this._importItem, attributes, true)["Comments"];
    if (property != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._importItem, property);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute("Комментарий"));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Комментарий"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("comment - Комментарий"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(false));
      listProperty.Add((PropertyDescriptor) propertyDescriptor);
    }
    this.CreateCollectionProperty(listProperty);
  }

  public AttributeCollection GetAttributes()
  {
    return TypeDescriptor.GetAttributes((object) this._importItem, true);
  }

  public string GetClassName() => TypeDescriptor.GetClassName((object) this._importItem, true);

  public string GetComponentName()
  {
    return TypeDescriptor.GetComponentName((object) this._importItem, true);
  }

  public TypeConverter GetConverter()
  {
    return TypeDescriptor.GetConverter((object) this._importItem, true);
  }

  public EventDescriptor GetDefaultEvent()
  {
    return TypeDescriptor.GetDefaultEvent((object) this._importItem, true);
  }

  public PropertyDescriptor GetDefaultProperty()
  {
    return TypeDescriptor.GetDefaultProperty((object) this._importItem, true);
  }

  public object GetEditor(Type editorBaseType)
  {
    return TypeDescriptor.GetEditor((object) this._importItem, editorBaseType, true);
  }

  public EventDescriptorCollection GetEvents(Attribute[] attributes)
  {
    return TypeDescriptor.GetEvents((object) this._importItem, attributes, true);
  }

  public EventDescriptorCollection GetEvents()
  {
    return TypeDescriptor.GetEvents((object) this._importItem, true);
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
    return pd is XmlConfigPropertyDescriptor propertyDescriptor ? propertyDescriptor.Owner : (object) this._importItem;
  }

  public void ResetOldValues()
  {
    if (this._pdc == null)
      return;
    foreach (PropertyDescriptor propertyDescriptor1 in this._pdc)
    {
      if (propertyDescriptor1 is XmlConfigPropertyDescriptor propertyDescriptor2)
        propertyDescriptor2.ResetOldValue((object) this._importItem);
    }
  }

  public void ResetValues()
  {
    if (this._pdc == null)
      return;
    foreach (PropertyDescriptor propertyDescriptor1 in this._pdc)
    {
      if (propertyDescriptor1 is XmlConfigPropertyDescriptor propertyDescriptor2)
        propertyDescriptor2.ResetValue((object) this._importItem);
    }
  }

  private void SetOldValues()
  {
    if (this._pdc == null)
      return;
    foreach (PropertyDescriptor propertyDescriptor1 in this._pdc)
    {
      if (propertyDescriptor1 is XmlConfigPropertyDescriptor propertyDescriptor2)
        propertyDescriptor2.SetOldValue((object) this._importItem);
    }
  }

  public void SaveSettings() => this.ResetOldValues();

  public void ResetSettings() => this.SetOldValues();

  public void ReadOnlyProperties(bool readOnly)
  {
    if (!readOnly)
      return;
    TypeDescriptor.AddAttributes((object) this._importItem, (Attribute) new ReadOnlyAttribute(true));
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
