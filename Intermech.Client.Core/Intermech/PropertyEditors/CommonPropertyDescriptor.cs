
// Type: Intermech.PropertyEditors.CommonPropertyDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;


namespace Intermech.PropertyEditors;

/// <summary>
/// 
/// </summary>
public class CommonPropertyDescriptor : PropertyDescriptor
{
  private PropertyDescriptor _descr;
  private object _oldValue;
  private string _displayName;
  private bool _isReadOnly;

  public CommonPropertyDescriptor(PropertyDescriptor descr, object oldValue)
    : base((MemberDescriptor) descr)
  {
    this._descr = descr;
    this._oldValue = oldValue;
  }

  public CommonPropertyDescriptor(PropertyDescriptor descr)
    : this(descr, (object) null)
  {
  }

  public override Type ComponentType => this._descr.ComponentType;

  public override bool IsReadOnly => this._descr.IsReadOnly || this._isReadOnly;

  public void SetReadOnly(bool isReadOnly) => this._isReadOnly = isReadOnly;

  public override Type PropertyType => this._descr.PropertyType;

  public override bool CanResetValue(object component) => this._descr.CanResetValue(component);

  public override void ResetValue(object component) => this._descr.ResetValue(component);

  public override object GetValue(object component) => this._descr.GetValue(component);

  public override void SetValue(object component, object value)
  {
    this._descr.SetValue(component, value);
  }

  public override bool ShouldSerializeValue(object component)
  {
    return !object.Equals(this._oldValue, this.GetValue(component));
  }

  public override string DisplayName
  {
    get => this._displayName != null ? this._displayName : base.DisplayName;
  }

  public void SetDisplayName(string displayName) => this._displayName = displayName;

  public void SetEditor(Type editorBaseType)
  {
    this.AttributeArray = new List<Attribute>((IEnumerable<Attribute>) this.AttributeArray)
    {
      (Attribute) new EditorAttribute(editorBaseType, typeof (UITypeEditor))
    }.ToArray();
  }

  public void SetConverter(Type converterType)
  {
    this.AttributeArray = new List<Attribute>((IEnumerable<Attribute>) this.AttributeArray)
    {
      (Attribute) new TypeConverterAttribute(converterType)
    }.ToArray();
  }
}
