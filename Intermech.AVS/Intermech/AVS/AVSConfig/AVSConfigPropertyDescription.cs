// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSConfig.AVSConfigPropertyDescription
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.Client;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.AVS.AVSConfig;

public class AVSConfigPropertyDescription : PropertyDescriptor
{
  private string _category;
  private string _displayName;
  private object _oldValue;
  private readonly AvsConfig owner;
  private bool? _readOnly;
  private readonly ArrayList _attributeList = new ArrayList();
  /// <summary>
  /// Исходный PropertyDescriptor полученный TypeDescriptor.GetProperties
  /// </summary>
  private readonly PropertyDescriptor propDesc;

  /// <summary>Сбросить текущее ViewModel значение, заменив на Model</summary>
  /// <param name="component"></param>
  internal void ResetOldValue(object component)
  {
    this._oldValue = this.propDesc.GetValue((object) this.owner ?? component);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="propDesc"></param>
  public AVSConfigPropertyDescription(AvsConfig owner, PropertyDescriptor propDesc)
    : base((MemberDescriptor) propDesc)
  {
    this.propDesc = propDesc;
    this.owner = owner;
    this._oldValue = propDesc?.GetValue((object) owner);
  }

  /// <summary>
  /// 
  /// </summary>
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

  /// <summary>Просто обращается к исходному объекту</summary>
  public override string Category
  {
    get
    {
      if (this._category != null)
        return this._category;
      this._category = this.Attributes[typeof (CategoryAttribute)] is CategoryAttribute attribute ? attribute.Category : this.propDesc.Category;
      return this._category;
    }
  }

  /// <summary>
  /// Получает или устанавливает старое(неизмененное) значение для поля.
  /// При изменении в ProprtyGrid позволяет выделять жирным
  /// шрифтом измененные значения при помощи метода ShouldSerializeValue.
  /// </summary>
  public object OldValue
  {
    get => this._oldValue;
    set => this._oldValue = value;
  }

  /// <summary>
  /// Устанавливает отображаемое имя свойства без использования атрибута.
  /// </summary>
  /// <param name="value">Отображаемое имя</param>
  public void SetDisplayName(string value) => this._displayName = value;

  /// <summary>
  /// Это свойство возвращает название свойства, отображаемое в propertyGrid
  /// </summary>
  public override string DisplayName
  {
    get
    {
      if (this._displayName != null)
        return this._displayName;
      this._displayName = this.Attributes[typeof (DisplayNameAttribute)] is DisplayNameAttribute attribute ? attribute.DisplayName : this.propDesc.Name;
      return this._displayName;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public override Type ComponentType => this.propDesc.ComponentType;

  /// <summary>
  /// 
  /// </summary>
  public override bool IsReadOnly
  {
    get
    {
      if (this._readOnly.HasValue)
        return this._readOnly.Value;
      this._readOnly = new bool?(this.Attributes[typeof (ReadOnlyAttribute)] is ReadOnlyAttribute attribute ? attribute.IsReadOnly && !ClassWrapperForPropertyGrid.IsUserRoleAdmin() : this.propDesc.IsReadOnly);
      return this._readOnly.Value;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public override Type PropertyType => this.propDesc.PropertyType;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="component"></param>
  /// <returns></returns>
  public override bool CanResetValue(object component) => this.propDesc.CanResetValue(component);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="component"></param>
  /// <returns></returns>
  public override object GetValue(object component)
  {
    object obj = this.owner?[this.propDesc.Name];
    if (obj == null && component is AvsConfig avsConfig)
      obj = avsConfig?[this.propDesc.Name];
    return obj;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="component"></param>
  public override void ResetValue(object component) => this.propDesc.ResetValue(component);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="component"></param>
  /// <param name="value"></param>
  public override void SetValue(object component, object value)
  {
    if (value is string && this.Converter.CanConvertFrom(value.GetType()))
    {
      object obj = this.Converter.ConvertFrom(value);
      if (this.owner != null)
      {
        this.owner[this.propDesc.Name] = obj;
      }
      else
      {
        if (!(component is AvsConfig avsConfig))
          return;
        avsConfig[this.propDesc.Name] = obj;
      }
    }
    else if (this.owner != null)
    {
      this.owner[this.propDesc.Name] = value;
    }
    else
    {
      if (!(component is AvsConfig avsConfig))
        return;
      avsConfig[this.propDesc.Name] = value;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="component"></param>
  /// <returns></returns>
  public override bool ShouldSerializeValue(object component)
  {
    return this.propDesc.Attributes[typeof (DefaultValueAttribute)] is DefaultValueAttribute attribute ? !object.Equals(this.GetValue(component), attribute.Value) : !object.Equals(this.GetValue(component), this._oldValue);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attr"></param>
  public void AddAttribute(Attribute attr) => this._attributeList.Add((object) attr);

  /// <summary>
  /// 
  /// </summary>
  public object Owner
  {
    [DebuggerStepThrough] get => (object) this.owner;
  }
}
