// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Descriptors.FormDesignerPropertyDescriptor
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.FormDesigner.Attributes;
using Intermech.FormDesigner.Wrappers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;

#nullable disable
namespace Intermech.FormDesigner.Descriptors;

/// <summary>
/// 
/// </summary>
internal class FormDesignerPropertyDescriptor : PropertyDescriptor
{
  private object _component;
  private PropertyDescriptor _pd;
  private bool _canReset;

  /// <summary>Конструктор.</summary>
  /// <param name="component">Компонент</param>
  /// <param name="pd">PropertyDescriptor</param>
  /// <param name="attributes">Список атрибутов</param>
  public FormDesignerPropertyDescriptor(
    object component,
    PropertyDescriptor pd,
    Attribute[] attributes)
    : base(pd.Name, attributes)
  {
    this._component = component;
    this._pd = pd;
    if (attributes == null || !(((IEnumerable<Attribute>) attributes).FirstOrDefault<Attribute>((Func<Attribute, bool>) (x => x is ResetValueAttribute)) is ResetValueAttribute resetValueAttribute))
      return;
    this._canReset = resetValueAttribute.CanResetValue;
  }

  /// <summary>
  /// 
  /// </summary>
  public event EventHandler<SetValueEventArgs> AfterSetValue;

  /// <summary>
  /// 
  /// </summary>
  public event EventHandler AfterResetValue;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="component"></param>
  /// <returns></returns>
  public override bool CanResetValue(object component) => this._canReset;

  /// <summary>
  /// 
  /// </summary>
  public override Type ComponentType => this._component.GetType();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="component"></param>
  /// <returns></returns>
  public override object GetValue(object component) => this._pd.GetValue(component);

  /// <summary>
  /// 
  /// </summary>
  public override bool IsReadOnly
  {
    get
    {
      bool isReadOnly = this._pd.IsReadOnly;
      if (!isReadOnly)
        isReadOnly = this.Attributes[typeof (ReadOnlyAttribute)] is ReadOnlyAttribute attribute && attribute.IsReadOnly;
      return isReadOnly;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public override Type PropertyType => this._pd.PropertyType;

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
    this._pd.SetValue(component, value);
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
    return this._pd.ShouldSerializeValue(component);
  }

  /// <summary>Editor свойства.</summary>
  /// <param name="editorBaseType">Тип editor'а</param>
  /// <returns>Editor</returns>
  public override object GetEditor(Type editorBaseType)
  {
    return this.IsReadOnly ? (object) null : base.GetEditor(editorBaseType);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attribute"></param>
  /// <param name="attributeType"></param>
  private void ReplaceAttribute(Attribute attribute, Type attributeType)
  {
    this.RemoveAttribute(attributeType);
    List<Attribute> list = ((IEnumerable<Attribute>) this.AttributeArray).ToList<Attribute>();
    list.Add(attribute);
    this.AttributeArray = list.ToArray();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attributeType"></param>
  private void RemoveAttribute(Type attributeType)
  {
    Attribute attribute = this.Attributes[attributeType];
    if (attribute == null)
      return;
    List<Attribute> list = ((IEnumerable<Attribute>) this.AttributeArray).ToList<Attribute>();
    list.Remove(attribute);
    this.AttributeArray = list.ToArray();
  }

  /// <summary>Установить converter свойства.</summary>
  /// <param name="converterType">Converter свойства</param>
  public void SetConverter(Type converterType)
  {
    this.FillAttributes((IList) new List<Attribute>());
    if (converterType != (Type) null)
      this.ReplaceAttribute((Attribute) new TypeConverterAttribute(converterType), typeof (TypeConverterAttribute));
    else
      this.RemoveAttribute(typeof (TypeConverterAttribute));
  }

  /// <summary>Установить editor свойства.</summary>
  /// <param name="editorType">Editor</param>
  public void SetEditor(Type editorType)
  {
    this.FillAttributes((IList) new List<Attribute>());
    if (editorType != (Type) null)
      this.ReplaceAttribute((Attribute) new EditorAttribute(editorType, typeof (UITypeEditor)), typeof (EditorAttribute));
    else
      this.RemoveAttribute(typeof (EditorAttribute));
  }

  /// <summary>Только для чтения.</summary>
  /// <param name="isReadOnly"></param>
  public void SetReadOnly(bool isReadOnly)
  {
    this.ReplaceAttribute((Attribute) new ReadOnlyAttribute(isReadOnly), typeof (ReadOnlyAttribute));
  }

  /// <summary>Задать видимость свойства.</summary>
  /// <param name="isVisible">Значение</param>
  public void SetVisible(bool isVisible)
  {
    this.ReplaceAttribute((Attribute) new BrowsableAttribute(isVisible), typeof (BrowsableAttribute));
  }
}
