// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Params.CustomPropertyDescriptor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.Imbase.Params;

public class CustomPropertyDescriptor : PropertyDescriptor
{
  private string _category;
  private string _displayName;
  private bool? _readOnly;
  private readonly ArrayList _attributeList = new ArrayList();
  private readonly PropertyDescriptor _propDesc;
  private PropertyDescriptorCollection _children = new PropertyDescriptorCollection((PropertyDescriptor[]) null);

  internal void ResetOldValue(object component)
  {
    this.OldValue = this._propDesc.GetValue(this.Owner ?? component);
  }

  public CustomPropertyDescriptor(object owner, PropertyDescriptor propDesc)
    : base((MemberDescriptor) propDesc)
  {
    this._propDesc = propDesc;
    this.Owner = owner;
    this.OldValue = propDesc.GetValue(owner);
  }

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

  public object OldValue { get; set; }

  public void SetDisplayName(string value) => this._displayName = value;

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

  public override Type PropertyType => this._propDesc.PropertyType;

  public override bool CanResetValue(object component) => this._propDesc.CanResetValue(component);

  public override object GetValue(object component)
  {
    return this.Owner == null ? this._propDesc.GetValue(component) : this._propDesc.GetValue(this.Owner);
  }

  public override void ResetValue(object component) => this._propDesc.ResetValue(component);

  public override void SetValue(object component, object value)
  {
    if (this.Converter != null && value is string && this.Converter.CanConvertFrom(value.GetType()))
      this._propDesc.SetValue(this.Owner ?? component, this.Converter.ConvertFrom(value));
    else
      this._propDesc.SetValue(this.Owner ?? component, value);
  }

  public override bool ShouldSerializeValue(object component)
  {
    return this._propDesc.Attributes[typeof (DefaultValueAttribute)] is DefaultValueAttribute attribute ? !object.Equals(this.GetValue(component), attribute.Value) : !object.Equals(this.GetValue(component), this.OldValue);
  }

  internal bool PropertiesSupported => this._children.Count > 0;

  internal PropertyDescriptorCollection ChildProperties
  {
    get => this._children;
    set
    {
      this._children = value;
      this._children = this._children ?? new PropertyDescriptorCollection((PropertyDescriptor[]) null);
    }
  }

  public void AddAttribute(Attribute attr) => this._attributeList.Add((object) attr);

  public object Owner { [DebuggerStepThrough] get; }
}
