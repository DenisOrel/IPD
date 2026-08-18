// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.PropDescriptorHolder
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.PropertyEditors;

/// <summary>Базовый тип для Holder'ов, назначаемых в PropertyGrid</summary>
public class PropDescriptorHolder : ICustomTypeDescriptor
{
  protected PropertyDescriptorCollection _pdc;

  public void DropPropertyDescriptorCollection() => this._pdc = (PropertyDescriptorCollection) null;

  public virtual void CreateProperties(PropertyDescriptorCollection pdc)
  {
  }

  public PropertyDescriptorCollection PropDescriptorCollection => this._pdc;

  public void RemovePDCItem(PropertyDescriptor pd)
  {
    this._pdc = PropDescriptorHolder.RemovePDCItem(this._pdc, pd);
  }

  public static PropertyDescriptorCollection RemovePDCItem(
    PropertyDescriptorCollection aPdc,
    PropertyDescriptor aPd)
  {
    PropertyDescriptorCollection descriptorCollection1 = (PropertyDescriptorCollection) null;
    if (aPdc == null)
      return descriptorCollection1;
    PropertyDescriptorCollection descriptorCollection2 = new PropertyDescriptorCollection((PropertyDescriptor[]) null);
    for (int index = 0; index < aPdc.Count; ++index)
    {
      if (aPdc[index] != aPd)
        descriptorCollection2.Add(aPdc[index]);
    }
    return descriptorCollection2;
  }

  public static PropertyDescriptorCollection RemovePDCItem(
    PropertyDescriptorCollection aPdc,
    int index)
  {
    PropertyDescriptorCollection descriptorCollection1 = (PropertyDescriptorCollection) null;
    if (aPdc == null)
      return descriptorCollection1;
    PropertyDescriptorCollection descriptorCollection2 = new PropertyDescriptorCollection((PropertyDescriptor[]) null);
    for (int index1 = 0; index1 < aPdc.Count; ++index1)
    {
      if (index1 != index)
        descriptorCollection2.Add(aPdc[index1]);
    }
    return descriptorCollection2;
  }

  public static int IndexOfPDCItem(PropertyDescriptorCollection aPdc, PropertyDescriptor aPd)
  {
    int num = -1;
    for (int index = 0; index < aPdc.Count; ++index)
    {
      if (aPdc[index] == aPd)
      {
        num = index;
        break;
      }
    }
    return num;
  }

  protected virtual AttributeCollection ExtendAttributes(AttributeCollection attributes)
  {
    return attributes;
  }

  public TypeConverter GetConverter() => TypeDescriptor.GetConverter((object) this, true);

  public EventDescriptorCollection GetEvents(Attribute[] attributes)
  {
    return TypeDescriptor.GetEvents((object) this, attributes, true);
  }

  EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
  {
    return TypeDescriptor.GetEvents((object) this, true);
  }

  public string GetComponentName() => TypeDescriptor.GetComponentName((object) this, true);

  public object GetPropertyOwner(PropertyDescriptor pd) => (object) this;

  public AttributeCollection GetAttributes()
  {
    return this.ExtendAttributes(TypeDescriptor.GetAttributes((object) this, true));
  }

  public PropertyDescriptorCollection GetProperties(Attribute[] attributes) => this.GetProperties();

  public PropertyDescriptorCollection GetProperties()
  {
    if (this._pdc == null)
    {
      this._pdc = new PropertyDescriptorCollection((PropertyDescriptor[]) null);
      this.CreateProperties(this._pdc);
    }
    PropertyDescriptorCollection properties = new PropertyDescriptorCollection((PropertyDescriptor[]) null);
    foreach (PropertyDescriptor propertyDescriptor in this._pdc)
    {
      if (propertyDescriptor.IsBrowsable)
        properties.Add(propertyDescriptor);
    }
    return properties;
  }

  public object GetEditor(Type editorBaseType)
  {
    return TypeDescriptor.GetEditor((object) this, editorBaseType, true);
  }

  public PropertyDescriptor GetDefaultProperty()
  {
    return TypeDescriptor.GetDefaultProperty((object) this, true);
  }

  public EventDescriptor GetDefaultEvent() => TypeDescriptor.GetDefaultEvent((object) this, true);

  public string GetClassName() => TypeDescriptor.GetClassName((object) this, true);
}
