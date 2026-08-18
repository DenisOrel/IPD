// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.lTypeDescriptor
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core.FormDesigner.Controls;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

internal class lTypeDescriptor : ICustomTypeDescriptor, IWrapper
{
  private object _component;
  private bool _noCustomTypeDescriptor;
  private bool _onlyTranslate = true;

  public lTypeDescriptor(object component) => this._component = component;

  public lTypeDescriptor(object component, bool onlyTranslate)
    : this(component)
  {
    this._onlyTranslate = onlyTranslate;
  }

  public lTypeDescriptor(object component, bool onlyTranslate, bool noCustomTypeDescriptor)
    : this(component, onlyTranslate)
  {
    this._noCustomTypeDescriptor = noCustomTypeDescriptor;
  }

  public AttributeCollection GetAttributes()
  {
    return TypeDescriptor.GetAttributes(this._component, this._noCustomTypeDescriptor);
  }

  public string GetClassName()
  {
    return TypeDescriptor.GetClassName(this._component, this._noCustomTypeDescriptor);
  }

  public string GetComponentName()
  {
    return TypeDescriptor.GetComponentName(this._component, this._noCustomTypeDescriptor);
  }

  public TypeConverter GetConverter()
  {
    return TypeDescriptor.GetConverter(this._component, this._noCustomTypeDescriptor);
  }

  public EventDescriptor GetDefaultEvent()
  {
    return TypeDescriptor.GetDefaultEvent(this._component, this._noCustomTypeDescriptor);
  }

  public PropertyDescriptor GetDefaultProperty()
  {
    return TypeDescriptor.GetDefaultProperty(this._component, this._noCustomTypeDescriptor);
  }

  public object GetEditor(Type editorBaseType)
  {
    return TypeDescriptor.GetEditor(this._component, editorBaseType, this._noCustomTypeDescriptor);
  }

  public EventDescriptorCollection GetEvents(Attribute[] attributes)
  {
    return TypeDescriptor.GetEvents(this._component, attributes, this._noCustomTypeDescriptor);
  }

  public EventDescriptorCollection GetEvents()
  {
    return TypeDescriptor.GetEvents(this._component, this._noCustomTypeDescriptor);
  }

  public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
  {
    PropertyDescriptorCollection properties1 = TypeDescriptor.GetProperties(this._component, attributes, this._noCustomTypeDescriptor);
    PropertyDescriptorCollection properties2 = new PropertyDescriptorCollection((PropertyDescriptor[]) null);
    foreach (PropertyDescriptor opd in properties1)
    {
      PropertyDescriptor propertyDescriptor = lPropertyTranslate.PropertyTranslate.Translate(this._component, opd, this._onlyTranslate);
      if (propertyDescriptor != null)
        properties2.Add(propertyDescriptor);
    }
    return properties2;
  }

  public PropertyDescriptorCollection GetProperties()
  {
    AttributeCollection attributes1 = this.GetAttributes();
    Attribute[] attributes2 = new Attribute[attributes1.Count];
    attributes1.CopyTo((Array) attributes2, 0);
    return this.GetProperties(attributes2);
  }

  public object GetPropertyOwner(PropertyDescriptor pd) => this._component;

  public object BaseClass => this._component;
}
