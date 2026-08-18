// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.PropDescriptor
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.PropertyEditors;

/// <summary>
/// Дескриптор поля, назначаемого в PropertyDescriptorCollection PropDescriptorHolder'a для назначения в PropertyGrid
/// </summary>
public class PropDescriptor : PropertyDescriptor
{
  private int propID = -1;
  private object component;
  private string name;
  private object value;
  private Type propertyType;
  private TypeConverter converter;
  private object editor;
  private string category;
  private string description;
  private bool readOnly;
  private bool browsable = true;
  private bool resetvalue;
  private bool disableManualEdit;
  private string mask = string.Empty;
  private object tag;
  private bool valueChanged;
  private bool changedValueApplied;

  public virtual bool ValueChanged
  {
    get => this.valueChanged;
    set => this.valueChanged = value;
  }

  public bool DisableManualEdit => this.disableManualEdit;

  public string Mask => this.mask;

  public bool ChangedValueApplied
  {
    get => this.changedValueApplied;
    set => this.changedValueApplied = value;
  }

  public PropDescriptor(
    int propID,
    object component,
    string name,
    object value,
    Type type,
    TypeConverter converter,
    object editor,
    string category,
    string description,
    bool readOnly,
    bool browsable,
    bool reset)
    : this(propID, component, name, value, type, converter, editor, category, description, readOnly, browsable, reset, string.Empty)
  {
  }

  public PropDescriptor(
    int propID,
    object component,
    string name,
    object value,
    Type type,
    TypeConverter converter,
    object editor,
    string category,
    string description,
    bool readOnly,
    bool browsable,
    bool reset,
    string mask)
    : this(propID, component, name, value, type, converter, editor, category, description, readOnly, browsable, reset, mask, false)
  {
  }

  public PropDescriptor(
    int propID,
    object component,
    string name,
    object value,
    Type type,
    TypeConverter converter,
    object editor,
    string category,
    string description,
    bool readOnly,
    bool browsable,
    bool reset,
    string mask,
    bool disableManualEdit)
    : base(name, (Attribute[]) null)
  {
    this.propID = propID;
    this.component = component;
    this.name = name;
    this.value = value;
    this.propertyType = type;
    this.converter = converter;
    if (this.converter == null)
      this.converter = TypeDescriptor.GetConverter(type);
    if (disableManualEdit)
      this.converter = (TypeConverter) new TypeConvertorWrapper(this.converter, disableManualEdit);
    this.editor = editor;
    this.category = category;
    this.description = description;
    this.readOnly = readOnly;
    this.browsable = browsable;
    this.resetvalue = reset;
    this.disableManualEdit = disableManualEdit;
    this.mask = mask;
    this.valueChanged = false;
  }

  public int PropID => this.propID;

  public object Component
  {
    get => this.component;
    set => this.component = value;
  }

  public override object GetEditor(Type editorBaseType)
  {
    return this.editor != null ? this.editor : base.GetEditor(editorBaseType);
  }

  public object Editor
  {
    [DebuggerStepThrough] get => this.editor;
    [DebuggerStepThrough] set => this.editor = value;
  }

  public override string Category => this.category;

  public override string Description => this.description;

  public override string DisplayName => this.name;

  public override Type ComponentType => this.component.GetType();

  public override bool IsReadOnly => this.readOnly;

  public override bool IsBrowsable => this.browsable;

  public override Type PropertyType => this.propertyType;

  public override TypeConverter Converter => this.converter;

  public override bool CanResetValue(object component) => this.resetvalue;

  public override object GetValue(object component) => this.value;

  public override void ResetValue(object component)
  {
    this.value = (object) null;
    this.valueChanged = true;
  }

  public override void SetValue(object component, object value)
  {
    this.value = value;
    if (this.value == value)
      return;
    this.valueChanged = true;
  }

  public override bool ShouldSerializeValue(object component) => false;

  public void SetReadOnly(bool aReadOnly) => this.readOnly = aReadOnly;

  public void SetEditor(object aEditor) => this.editor = aEditor;

  public void SetBrowsable(bool aBrowsable) => this.browsable = aBrowsable;

  public void SetName(string aName) => this.name = aName;

  public void SetDescription(string aDescription) => this.description = aDescription;

  public void SetPropID(int aPropID) => this.propID = aPropID;

  public void SetPropertyType(Type aType) => this.propertyType = aType;

  public void SetResetValue(bool aResetvalue) => this.resetvalue = aResetvalue;

  public void SetConverter(TypeConverter aTypeConverter) => this.converter = aTypeConverter;

  public virtual void ResetValueChanged(object component) => this.valueChanged = false;

  public object Tag
  {
    get => this.tag;
    set => this.tag = value;
  }
}
