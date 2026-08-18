// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Controls.NameValuePair
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

#nullable disable
namespace Intermech.Imbase.Controls;

internal class NameValuePair
{
  private string _name;
  private string _value;

  public NameValuePair(string name, string value)
  {
    this._name = name;
    this._value = value;
  }

  public string Name => this._name;

  public string Value => this._value;
}
