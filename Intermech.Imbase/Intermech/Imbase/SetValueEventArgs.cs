// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.SetValueEventArgs
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System.ComponentModel;

#nullable disable
namespace Intermech.Imbase;

internal class SetValueEventArgs
{
  private PropertyDescriptor _pd;
  private object _value;

  internal PropertyDescriptor PropertyDescriptor => this._pd;

  internal object Value
  {
    get => this._value;
    set => this._value = value;
  }

  internal SetValueEventArgs(PropertyDescriptor pd, object value)
  {
    this._pd = pd;
    this._value = value;
  }
}
