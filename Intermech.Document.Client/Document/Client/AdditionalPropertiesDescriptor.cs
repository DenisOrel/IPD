// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.AdditionalPropertiesDescriptor
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using System;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.Document.Client;

internal class AdditionalPropertiesDescriptor : PropertyDescriptor
{
  /// <summary>Исходный PropertyDescriptor полученный TypeDescriptor.GetProperties</summary>
  private PropertyDescriptor _PropDesc;
  private AdditionalPropertiesWrapper wrapper;

  /// <summary>Конструктор</summary>
  /// <param name="PropDesc">Исходный PropertyDescriptor полученный TypeDescriptor.GetProperties,
  /// на основе которого работает класс</param>
  public AdditionalPropertiesDescriptor(
    AdditionalPropertiesWrapper wrapper,
    PropertyDescriptor PropDesc)
    : base((MemberDescriptor) PropDesc)
  {
    this._PropDesc = PropDesc;
    this.wrapper = wrapper;
  }

  /// <summary>Просто обращается к исходному объекту</summary>
  public override Type ComponentType
  {
    [DebuggerStepThrough] get => this._PropDesc.ComponentType;
  }

  /// <summary>Только для чтения</summary>
  public override bool IsReadOnly => this._PropDesc.IsReadOnly;

  /// <summary>Просто обращается к исходному объекту</summary>
  public override Type PropertyType
  {
    [DebuggerStepThrough] get => this._PropDesc.PropertyType;
  }

  /// <summary>Можно ли сбросить значение свойства</summary>
  /// <remarks>Просто обращается к исходному объекту</remarks>
  /// <param name="component">Владелец свойства</param>
  /// <returns>Можно ли сбросить значение свойства</returns>
  public override bool CanResetValue(object component)
  {
    return this._PropDesc.CanResetValue((object) this.wrapper);
  }

  /// <summary>Получить значение свойства</summary>
  /// <remarks>Просто обращается к исходному объекту</remarks>
  /// <param name="component">Владелец свойства</param>
  /// <returns>Значение свойства</returns>
  public override object GetValue(object component)
  {
    return this._PropDesc.GetValue((object) this.wrapper);
  }

  /// <summary>Сбросить значение свойства</summary>
  /// <remarks>Просто обращается к исходному объекту</remarks>
  /// <param name="component">Владелец свойства</param>
  public override void ResetValue(object component)
  {
    this._PropDesc.ResetValue((object) this.wrapper);
  }

  /// <summary>Установить значение свойства</summary>
  /// <remarks>Просто обращается к исходному объекту</remarks>
  /// <param name="component">Владелец свойства</param>
  /// <param name="value">Значение свойства</param>
  public override void SetValue(object component, object value)
  {
    if (this.IsReadOnly)
      return;
    this._PropDesc.SetValue((object) this.wrapper, value);
  }

  /// <summary>Нужно ли сохранить данное значение свойства</summary>
  /// <remarks>Просто обращается к исходному объекту</remarks>
  /// <param name="component">Владелец свойства</param>
  /// <returns>Нужно ли сохранить данное значение свойства</returns>
  public override bool ShouldSerializeValue(object component)
  {
    return this._PropDesc.ShouldSerializeValue((object) this.wrapper);
  }
}
