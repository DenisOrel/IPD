// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ExportItem.GridViewSettingsApplType
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.XmlExchange.ConfigEditor.ExportApplSetting;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ExportItem;

internal class GridViewSettingsApplType : ICustomTypeDescriptor, IConfigItemProperties
{
  private protected string сategory;
  private protected PropertyDescriptorCollection _pdc;
  private protected IExportApplType _exportApplType;

  public GridViewSettingsApplType(IExportApplType exportApplType, bool readOnly)
  {
    this._exportApplType = exportApplType;
    this.ReadOnlyProperties(readOnly);
  }

  protected virtual void CreatePdc(Attribute[] attributes)
  {
    if (this._exportApplType == null)
      return;
    this.сategory = this._exportApplType.ApplType;
    List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>();
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) this._exportApplType, attributes, true);
    PropertyDescriptor propDesc1 = properties["TypeName"];
    if (propDesc1 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._exportApplType, propDesc1);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this.сategory));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Имя типа"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("name - Имя типа"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(true));
      propertyDescriptorList.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc2 = properties["TypeGuid"];
    if (propDesc2 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._exportApplType, propDesc2);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this.сategory));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Глобальный идентификатор типа"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("guid - Глобальный идентификатор типа"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(this._exportApplType.ExistInBase));
      propertyDescriptorList.Add((PropertyDescriptor) propertyDescriptor);
    }
    PropertyDescriptor propDesc3 = properties["TypeID"];
    if (propDesc3 != null)
    {
      XmlConfigPropertyDescriptor propertyDescriptor = new XmlConfigPropertyDescriptor((object) this._exportApplType, propDesc3);
      propertyDescriptor.AddAttribute((Attribute) new CategoryAttribute(this.сategory));
      propertyDescriptor.AddAttribute((Attribute) new DisplayNameAttribute("Идентификатор типа"));
      propertyDescriptor.AddAttribute((Attribute) new DescriptionAttribute("id - Идентификатор типа"));
      propertyDescriptor.AddAttribute((Attribute) new ReadOnlyAttribute(this._exportApplType.ExistInBase));
      propertyDescriptorList.Add((PropertyDescriptor) propertyDescriptor);
    }
    this._pdc = new PropertyDescriptorCollection(propertyDescriptorList.ToArray());
  }

  public AttributeCollection GetAttributes()
  {
    return TypeDescriptor.GetAttributes((object) this._exportApplType, true);
  }

  public string GetClassName() => TypeDescriptor.GetClassName((object) this._exportApplType, true);

  public string GetComponentName()
  {
    return TypeDescriptor.GetComponentName((object) this._exportApplType, true);
  }

  public TypeConverter GetConverter()
  {
    return TypeDescriptor.GetConverter((object) this._exportApplType, true);
  }

  public EventDescriptor GetDefaultEvent()
  {
    return TypeDescriptor.GetDefaultEvent((object) this._exportApplType, true);
  }

  public PropertyDescriptor GetDefaultProperty()
  {
    return TypeDescriptor.GetDefaultProperty((object) this._exportApplType, true);
  }

  public object GetEditor(Type editorBaseType)
  {
    return TypeDescriptor.GetEditor((object) this._exportApplType, editorBaseType, true);
  }

  public EventDescriptorCollection GetEvents(Attribute[] attributes)
  {
    return TypeDescriptor.GetEvents((object) this._exportApplType, attributes, true);
  }

  public EventDescriptorCollection GetEvents()
  {
    return TypeDescriptor.GetEvents((object) this._exportApplType, true);
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
    return pd is XmlConfigPropertyDescriptor propertyDescriptor ? propertyDescriptor.Owner : (object) this._exportApplType;
  }

  public void ResetOldValues()
  {
    if (this._pdc == null)
      return;
    foreach (PropertyDescriptor propertyDescriptor1 in this._pdc)
    {
      if (propertyDescriptor1 is XmlConfigPropertyDescriptor propertyDescriptor2)
        propertyDescriptor2.ResetOldValue((object) this._exportApplType);
    }
  }

  public void ResetValues()
  {
    if (this._pdc == null)
      return;
    foreach (PropertyDescriptor propertyDescriptor1 in this._pdc)
    {
      if (propertyDescriptor1 is XmlConfigPropertyDescriptor propertyDescriptor2)
        propertyDescriptor2.ResetValue((object) this._exportApplType);
    }
  }

  private void SetOldValues()
  {
    if (this._pdc == null)
      return;
    foreach (PropertyDescriptor propertyDescriptor1 in this._pdc)
    {
      if (propertyDescriptor1 is XmlConfigPropertyDescriptor propertyDescriptor2)
        propertyDescriptor2.SetOldValue((object) this._exportApplType);
    }
  }

  public void SaveSettings()
  {
    this.ResetOldValues();
    this._exportApplType.UpdateExportAppl();
  }

  public void ResetSettings() => this.SetOldValues();

  public void ReadOnlyProperties(bool readOnly)
  {
    if (!readOnly)
      return;
    TypeDescriptor.AddAttributes((object) this._exportApplType, (Attribute) new ReadOnlyAttribute(true));
  }
}
