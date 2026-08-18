// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.StructureEditorPropertyDescriptor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Imbase;

internal class StructureEditorPropertyDescriptor : PropertyDescriptor
{
  private Type _propType;
  private bool _canReset = true;
  private object _value;

  internal StructureEditorPropertyDescriptor(
    Attribute[] attributes,
    Type propType,
    string name,
    object value)
    : base(name, attributes)
  {
    this._propType = propType;
    this._value = value;
  }

  internal event PropertySetValue AfterSetValue;

  public override bool CanResetValue(object component) => this._canReset;

  public override Type ComponentType => this.GetType();

  public override object GetValue(object component) => this._value;

  public override bool IsReadOnly => false;

  public override Type PropertyType => this._propType;

  public override void ResetValue(object component)
  {
    Attribute[] attributeArray = this.AttributeArray;
    if (attributeArray == null || attributeArray.Length == 0)
      return;
    for (int index = 0; index < attributeArray.Length; ++index)
    {
      Attribute attribute = attributeArray[index];
      if (attribute is DefaultValueAttribute)
        this.SetValue(component, (attribute as DefaultValueAttribute).Value);
    }
  }

  public override void SetValue(object component, object value)
  {
    this._value = value;
    PropertySetValue afterSetValue = this.AfterSetValue;
    if (afterSetValue == null)
      return;
    afterSetValue(component, new SetValueEventArgs((PropertyDescriptor) this, this._value == null || this._value == DBNull.Value || string.IsNullOrEmpty(this._value.ToString()) ? (object) null : this._value));
  }

  public override bool ShouldSerializeValue(object component) => false;

  public void SetCanReset(bool canReset) => this._canReset = canReset;
}
