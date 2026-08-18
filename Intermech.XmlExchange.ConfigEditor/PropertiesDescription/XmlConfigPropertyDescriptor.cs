// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.PropertiesDescription.XmlConfigPropertyDescriptor
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.PropertiesDescription;

internal class XmlConfigPropertyDescriptor : PropertyDescriptor
{
  private string _category;
  private string _displayName;
  private object _oldValue;
  private bool? _readOnly;
  private readonly ArrayList _attributeList = new ArrayList();
  private readonly PropertyDescriptor _propDesc;
  private Type _propTypeDefault;
  private TypeConverter _converter;

  public override AttributeCollection Attributes
  {
    get
    {
      Attribute[] attributeArray = new Attribute[this._attributeList.Count + this.AttributeArray.Length];
      this._attributeList.CopyTo((Array) attributeArray);
      for (int count = this._attributeList.Count; count < attributeArray.Length; ++count)
        attributeArray[count] = this.AttributeArray[count - this._attributeList.Count];
      return new AttributeCollection(attributeArray);
    }
  }

  public override string Category
  {
    get
    {
      if (this._category != null)
        return this._category;
      this._category = this.Attributes[typeof (CategoryAttribute)] is CategoryAttribute attribute ? attribute.Category : this._propDesc.Category;
      return this._category;
    }
  }

  public override string DisplayName
  {
    get
    {
      if (this._displayName != null)
        return this._displayName;
      this._displayName = this.Attributes[typeof (DisplayNameAttribute)] is DisplayNameAttribute attribute ? attribute.DisplayName : this._propDesc.Name;
      return this._displayName;
    }
  }

  public override object GetEditor(Type editorBaseType)
  {
    if (this.Editor != null)
      return this.Editor;
    if (this.Attributes[typeof (EditorAttribute)] is EditorAttribute attribute)
    {
      Type type = Type.GetType(attribute.EditorTypeName);
      if (type != (Type) null)
      {
        this.Editor = Activator.CreateInstance(type);
        return this.Editor;
      }
    }
    return base.GetEditor(editorBaseType);
  }

  public object Editor { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

  public override Type ComponentType => this._propDesc.ComponentType;

  public override bool IsReadOnly
  {
    get
    {
      if (this._readOnly.HasValue)
        return this._readOnly.Value;
      this._readOnly = new bool?(this.Attributes[typeof (ReadOnlyAttribute)] is ReadOnlyAttribute attribute ? attribute.IsReadOnly : this._propDesc.IsReadOnly);
      return this._readOnly.Value;
    }
  }

  public override Type PropertyType
  {
    get
    {
      if (this._oldValue == null && this._propTypeDefault != (Type) null)
        return this._propTypeDefault;
      return this._oldValue != null ? this._oldValue.GetType() : this._propDesc.PropertyType;
    }
  }

  public object Owner { [DebuggerStepThrough] get; }

  internal void ResetOldValue(object component)
  {
    this._oldValue = this._propDesc.GetValue(this.Owner ?? component);
  }

  public XmlConfigPropertyDescriptor(object owner, PropertyDescriptor propDesc, Type typeDefault = null)
    : base((MemberDescriptor) propDesc)
  {
    this._propDesc = propDesc;
    this.Owner = owner;
    this._oldValue = propDesc.GetValue(owner);
    if (!(typeDefault != (Type) null))
      return;
    this._propTypeDefault = typeDefault;
  }

  public void SetDisplayName(string value) => this._displayName = value;

  public override bool CanResetValue(object component)
  {
    return this.Attributes[typeof (DefaultValueAttribute)] is DefaultValueAttribute;
  }

  public override object GetValue(object component)
  {
    return this.Owner != null ? this._propDesc.GetValue(this.Owner) ?? (object) string.Empty : this._propDesc.GetValue(component);
  }

  public override void ResetValue(object component)
  {
    if (this.Attributes[typeof (DefaultValueAttribute)] is DefaultValueAttribute attribute)
      this.SetValue(component, attribute.Value);
    else
      this.SetValue(component, this._oldValue);
  }

  public override void SetValue(object component, object value)
  {
    if (this.Converter != null && value is string && this.Converter.CanConvertFrom(value.GetType()))
      this._propDesc.SetValue(this.Owner ?? component, this.Converter.ConvertFrom(value));
    else
      this._propDesc.SetValue(this.Owner ?? component, value);
  }

  public void SetOldValue(object component) => this.SetValue(component, this._oldValue);

  public override bool ShouldSerializeValue(object component)
  {
    return this._propDesc.Attributes[typeof (DefaultValueAttribute)] is DefaultValueAttribute attribute ? !object.Equals(this.GetValue(component), attribute.Value) : !object.Equals(this.GetValue(component), this._oldValue);
  }

  public void AddAttribute(Attribute attr) => this._attributeList.Add((object) attr);

  public override TypeConverter Converter
  {
    get
    {
      if (this._converter != null)
        return this._converter;
      if (this.Attributes[typeof (TypeConverterAttribute)] is TypeConverterAttribute attribute)
      {
        Type type = Type.GetType(attribute.ConverterTypeName);
        if (type != (Type) null)
        {
          this._converter = Activator.CreateInstance(type, (object) this.PropertyType) as TypeConverter;
          return this._converter;
        }
      }
      this._converter = TypeDescriptor.GetConverter(this.PropertyType);
      return this._converter;
    }
  }
}
