// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.API.RecordItem
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

#nullable disable
namespace Intermech.Imbase.API;

internal struct RecordItem
{
  private string _data;

  internal RecordItem(string data) => this._data = data;

  internal string cptr => this._data;

  internal int len => this._data.Length;
}
