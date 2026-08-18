// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Converters_Editors.AutoSelectionReadOnlyPropDescriptor
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.AutoSelection.Client.Converters_Editors;

internal class AutoSelectionReadOnlyPropDescriptor : PropertyDescriptor
{
  private readonly PropertyDescriptor _propertyDescriptor;

  public AutoSelectionReadOnlyPropDescriptor(
    PropertyDescriptor propertyDescriptor,
    Attribute[] attrs)
    : base(propertyDescriptor.Name, attrs)
  {
    this._propertyDescriptor = propertyDescriptor;
  }

  public override Type ComponentType => this._propertyDescriptor.ComponentType;

  public override TypeConverter Converter => this._propertyDescriptor.Converter;

  public override bool IsLocalizable => this._propertyDescriptor.IsLocalizable;

  public override bool IsReadOnly => true;

  public override Type PropertyType => this._propertyDescriptor.PropertyType;

  public new DesignerSerializationVisibility SerializationVisibility
  {
    get => this._propertyDescriptor.SerializationVisibility;
  }

  public override bool SupportsChangeEvents => this._propertyDescriptor.SupportsChangeEvents;

  public override void AddValueChanged(object component, EventHandler handler)
  {
    this._propertyDescriptor.AddValueChanged(component, handler);
  }

  public override bool CanResetValue(object component)
  {
    return this._propertyDescriptor.CanResetValue(component);
  }

  public override bool Equals(object obj) => this._propertyDescriptor.Equals(obj);

  public new PropertyDescriptorCollection GetChildProperties()
  {
    return this._propertyDescriptor.GetChildProperties();
  }

  public new PropertyDescriptorCollection GetChildProperties(Attribute[] filter)
  {
    return this._propertyDescriptor.GetChildProperties(filter);
  }

  public new PropertyDescriptorCollection GetChildProperties(object instance)
  {
    return this._propertyDescriptor.GetChildProperties(instance);
  }

  public override int GetHashCode() => this._propertyDescriptor.GetHashCode();

  protected new Type GetTypeFromName(string typeName) => base.GetTypeFromName(typeName);

  public override object GetValue(object component) => this._propertyDescriptor.GetValue(component);

  public override void RemoveValueChanged(object component, EventHandler handler)
  {
    this._propertyDescriptor.RemoveValueChanged(component, handler);
  }

  public override void ResetValue(object component)
  {
    this._propertyDescriptor.ResetValue(component);
  }

  public override void SetValue(object component, object value)
  {
    this._propertyDescriptor.SetValue(component, value);
  }

  public override bool ShouldSerializeValue(object component)
  {
    return this._propertyDescriptor.ShouldSerializeValue(component);
  }
}
