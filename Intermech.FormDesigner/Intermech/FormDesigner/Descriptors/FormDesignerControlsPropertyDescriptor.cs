// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Descriptors.FormDesignerControlsPropertyDescriptor
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.FormDesigner.Wrappers;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Descriptors;

/// <summary>
/// Класс является тестовым. Далее сделать общим для всех контролов.
/// </summary>
internal class FormDesignerControlsPropertyDescriptor : PropertyDescriptor
{
  private object _component;
  private PropertyDescriptor _pd;
  private TypeConverter _converter;
  private UITypeEditor _editor;
  private bool _isReadOnly;
  private bool _canReset;

  /// <summary>Дочерние свойства.</summary>
  public PropertyDescriptorCollection ChildProperties { get; set; }

  /// <summary>Конструктор.</summary>
  /// <param name="component">Компонент</param>
  /// <param name="pd">PropertyDescriptor</param>
  /// <param name="attributes">Список атрибутов</param>
  public FormDesignerControlsPropertyDescriptor(
    object component,
    PropertyDescriptor pd,
    Attribute[] attributes)
    : base((MemberDescriptor) pd, attributes)
  {
    this._component = component;
    this._pd = pd;
  }

  /// <summary>
  /// 
  /// </summary>
  public event PropertySetValue AfterSetValue;

  /// <summary>
  /// 
  /// </summary>
  public event EventHandler AfterResetValue;

  /// <summary>Converter свойства.</summary>
  public override TypeConverter Converter => this._converter ?? base.Converter;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="component"></param>
  /// <returns></returns>
  public override bool CanResetValue(object component) => this._canReset;

  /// <summary>
  /// 
  /// </summary>
  public override System.Type ComponentType => this._component.GetType();

  /// <summary>Editor свойства.</summary>
  /// <param name="editorBaseType">Тип editor'а</param>
  /// <returns>Editor</returns>
  public override object GetEditor(System.Type editorBaseType)
  {
    return (object) this._editor ?? base.GetEditor(editorBaseType);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="component"></param>
  /// <returns></returns>
  public override object GetValue(object component)
  {
    object obj = (object) null;
    if (component is Control)
      obj = this._pd.GetValue(component);
    else if (this._component is Control)
      obj = this._pd.GetValue(this._component);
    return obj;
  }

  /// <summary>
  /// 
  /// </summary>
  public override bool IsReadOnly => this._pd.IsReadOnly || this._isReadOnly;

  /// <summary>
  /// 
  /// </summary>
  public override System.Type PropertyType => this._pd.PropertyType;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="component"></param>
  public override void ResetValue(object component)
  {
    this._pd.ResetValue(component);
    if (this.AfterResetValue == null)
      return;
    this.AfterResetValue(component, EventArgs.Empty);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="component"></param>
  /// <param name="value"></param>
  public override void SetValue(object component, object value)
  {
    if (component is Control)
      this._pd.SetValue(component, value);
    else if (this._component is Control)
      this._pd.SetValue(this._component, value);
    if (this.AfterSetValue == null)
      return;
    this.AfterSetValue(component, new SetValueEventArgs((PropertyDescriptor) this, value));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="component"></param>
  /// <returns></returns>
  public override bool ShouldSerializeValue(object component)
  {
    bool flag = false;
    if (component is Control)
      flag = this._pd.ShouldSerializeValue(component);
    else if (this._component is Control)
      flag = this._pd.ShouldSerializeValue(this._component);
    return flag;
  }

  /// <summary>
  /// Возможность сбрасывать значение в значение по умолчанию.
  /// </summary>
  /// <param name="canReset"></param>
  public void SetCanReset(bool canReset) => this._canReset = canReset;

  /// <summary>Установить converter свойства.</summary>
  /// <param name="converter">Converter свойства</param>
  public void SetConverter(TypeConverter converter) => this._converter = converter;

  /// <summary>Установить editor свойства.</summary>
  /// <param name="editor">Editor</param>
  public void SetEditor(UITypeEditor editor) => this._editor = editor;

  /// <summary>Только для чтения.</summary>
  /// <param name="isReadOnly"></param>
  public void SetReadOnly(bool isReadOnly) => this._isReadOnly = isReadOnly;

  /// <summary>Задать видимость свойства.</summary>
  /// <param name="isVisible">Значение</param>
  public void SetVisible(bool isVisible)
  {
    for (int index = 0; index < this.AttributeArray.Length; ++index)
    {
      if (this.AttributeArray[index] is BrowsableAttribute)
      {
        this.AttributeArray[index] = (Attribute) new BrowsableAttribute(isVisible);
        break;
      }
    }
  }
}
