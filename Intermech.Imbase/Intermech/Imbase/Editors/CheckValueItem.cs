// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.CheckValueItem
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Imbase.Editors;

[Serializable]
internal class CheckValueItem : ICloneable
{
  private bool _checked;
  private object _value;
  private string _name;
  private bool _visible;

  internal CheckValueItem(object value, object descr)
  {
    this._visible = true;
    this._checked = false;
    this._value = value;
    if (descr == null)
      this._name = string.Empty;
    else
      this._name = descr.ToString();
  }

  public bool Checked
  {
    get => this._checked;
    set => this._checked = value;
  }

  public object Value => this._value;

  public string Name => this._name;

  [Browsable(false)]
  public bool Visible
  {
    get => this._visible;
    set => this._visible = value;
  }

  public object Clone()
  {
    return (object) new CheckValueItem(this._value, (object) this._name)
    {
      Checked = this._checked
    };
  }
}
