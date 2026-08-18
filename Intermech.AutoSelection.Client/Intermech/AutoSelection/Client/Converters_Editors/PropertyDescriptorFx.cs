// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Converters_Editors.PropertyDescriptorFx
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.AutoSelection.Client.Converters_Editors;

public class PropertyDescriptorFx : PropertyDescriptor
{
  private readonly PropertyDescriptor _propertyDescriptor;
  private TypeConverter _converter;
  private object _editor;
  private readonly object _oldValue;
  private string _displayName;
  private bool _isReadOnly;

  public PropertyDescriptorFx(PropertyDescriptor propertyDescriptor, object oldValue = null)
    : base((MemberDescriptor) propertyDescriptor)
  {
    this._propertyDescriptor = propertyDescriptor;
    this._oldValue = oldValue;
  }

  public override Type ComponentType => this._propertyDescriptor.ComponentType;

  public override bool IsReadOnly => this._propertyDescriptor.IsReadOnly || this._isReadOnly;

  public void SetReadOnly(bool isReadOnly) => this._isReadOnly = isReadOnly;

  public override Type PropertyType => this._propertyDescriptor.PropertyType;

  public override bool CanResetValue(object component)
  {
    return this._propertyDescriptor.CanResetValue(component);
  }

  public override void ResetValue(object component)
  {
    this._propertyDescriptor.ResetValue(component);
  }

  public override object GetValue(object component) => this._propertyDescriptor.GetValue(component);

  public override void SetValue(object component, object value)
  {
    this._propertyDescriptor.SetValue(component, value);
  }

  public override bool ShouldSerializeValue(object component)
  {
    return !object.Equals(this._oldValue, this.GetValue(component));
  }

  public override string DisplayName => this._displayName ?? base.DisplayName;

  public void SetDisplayName(string displayName) => this._displayName = displayName;

  public void SetConverter(Type converterType)
  {
    this.AttributeArray = new List<Attribute>((IEnumerable<Attribute>) this.AttributeArray)
    {
      (Attribute) new TypeConverterAttribute(converterType)
    }.ToArray();
  }

  public void SetConverter(TypeConverter converter) => this._converter = converter;

  public override TypeConverter Converter => this._converter ?? this._propertyDescriptor.Converter;

  public void SetEditor(Type editorBaseType)
  {
    this.AttributeArray = new List<Attribute>((IEnumerable<Attribute>) this.AttributeArray)
    {
      (Attribute) new EditorAttribute(editorBaseType, typeof (UITypeEditor))
    }.ToArray();
  }

  public void SetEditor(object editor) => this._editor = editor;

  public override object GetEditor(Type editorBaseType)
  {
    return this._editor ?? this._propertyDescriptor.GetEditor(editorBaseType);
  }
}
