// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Subsystems.Import_from_Excel.SettingItemPropertyDescriptor
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Client.Subsystems.Import_from_Excel;

public class SettingItemPropertyDescriptor : PropertyDescriptor
{
  private string _category;
  private string _displayName;
  private object _oldValue;
  private readonly object _owner;
  private bool? _readOnly;
  private readonly ArrayList _attributeList = new ArrayList();
  private readonly PropertyDescriptor _propDesc;

  internal void ResetOldValue(object component)
  {
    this._oldValue = this._propDesc.GetValue(this._owner ?? component);
  }

  public SettingItemPropertyDescriptor(object owner, PropertyDescriptor propDesc)
    : base((MemberDescriptor) propDesc)
  {
    this._propDesc = propDesc;
    this._owner = owner;
    this._oldValue = propDesc.GetValue(owner);
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

  public object OldValue
  {
    get => this._oldValue;
    set => this._oldValue = value;
  }

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
    return this._owner != null ? this._propDesc.GetValue(this._owner) : this._propDesc.GetValue(component);
  }

  public override void ResetValue(object component) => this._propDesc.ResetValue(component);

  public override void SetValue(object component, object value)
  {
    if (this.Converter != null && value is string && this.Converter.CanConvertFrom(value.GetType()))
      this._propDesc.SetValue(this._owner ?? component, this.Converter.ConvertFrom(value));
    else
      this._propDesc.SetValue(this._owner ?? component, value);
  }

  public override bool ShouldSerializeValue(object component)
  {
    return this._propDesc.Attributes[typeof (DefaultValueAttribute)] is DefaultValueAttribute attribute ? !object.Equals(this.GetValue(component), attribute.Value) : !object.Equals(this.GetValue(component), this._oldValue);
  }

  public void AddAttribute(Attribute attr) => this._attributeList.Add((object) attr);

  public object Owner
  {
    [DebuggerStepThrough] get => this._owner;
  }
}
