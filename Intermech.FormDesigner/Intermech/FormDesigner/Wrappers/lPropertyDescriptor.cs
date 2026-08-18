// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.lPropertyDescriptor
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

internal class lPropertyDescriptor : PropertyDescriptor
{
  private PropertyDescriptor _descr;
  private object _oldValue;
  private bool _isReadOnly;
  private string _displayName;
  private lPropertyTemplate _template;

  public lPropertyDescriptor(PropertyDescriptor descr, object oldValue)
    : base((MemberDescriptor) descr)
  {
    this._descr = descr;
    this._oldValue = oldValue;
  }

  public lPropertyDescriptor(PropertyDescriptor descr)
    : this(descr, (object) null)
  {
  }

  public override Type ComponentType => this._descr.ComponentType;

  public override bool IsReadOnly => this._descr.IsReadOnly || this._isReadOnly;

  public override Type PropertyType => this._descr.PropertyType;

  public override bool CanResetValue(object component) => this._descr.CanResetValue(component);

  public override void ResetValue(object component) => this._descr.ResetValue(component);

  public override object GetValue(object component) => this._descr.GetValue(component);

  public override void SetValue(object component, object value)
  {
    if (this.BeforeSetValue != null)
      this.BeforeSetValue(component, new SetValueEventArgs((PropertyDescriptor) this, value));
    this._descr.SetValue(component, value);
    if (this.AfterSetValue == null)
      return;
    this.AfterSetValue(component, new SetValueEventArgs((PropertyDescriptor) this, value));
  }

  public override bool ShouldSerializeValue(object component)
  {
    return !object.Equals(this._oldValue, this.GetValue(component));
  }

  public override string DisplayName
  {
    get => this._displayName != null ? this._displayName : base.DisplayName;
  }

  public event PropertySetValue BeforeSetValue;

  public event PropertySetValue AfterSetValue;

  public event Intermech.FormDesigner.Wrappers.AddCustomAttribute AddCustomAttribute;

  public void SetDisplayName(string displayName) => this._displayName = displayName;

  public void SetReadOnly(bool isReadOnly) => this._isReadOnly = isReadOnly;

  public void SetAttributes(Attribute[] attributes) => this.AttributeArray = attributes;

  public void AddAttribute(Attribute attribute)
  {
    this.AttributeArray = new List<Attribute>((IEnumerable<Attribute>) this.AttributeArray)
    {
      attribute
    }.ToArray();
  }

  public lPropertyTemplate Template
  {
    get => this._template;
    set => this._template = value;
  }
}
