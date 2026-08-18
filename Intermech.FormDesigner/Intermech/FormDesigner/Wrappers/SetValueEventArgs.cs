// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.SetValueEventArgs
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>Параметры назначения</summary>
public class SetValueEventArgs
{
  private PropertyDescriptor _pd;
  private object _value;

  /// <summary>Конструктор</summary>
  /// <param name="pd">исходный PropertyDescriptor</param>
  /// <param name="value">значение</param>
  public SetValueEventArgs(PropertyDescriptor pd, object value)
  {
    this._pd = pd;
    this._value = value;
  }

  /// <summary>Исходный PropertyDescriptor</summary>
  public PropertyDescriptor PropertyDescriptor => this._pd;

  /// <summary>Значение</summary>
  public object Value
  {
    get => this._value;
    set => this._value = value;
  }
}
