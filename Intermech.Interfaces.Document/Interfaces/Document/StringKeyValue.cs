// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.StringKeyValue
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Класс для пары Ключ - Значение</summary>
[Serializable]
public class StringKeyValue : ICloneable
{
  public string Key;
  public string Value;

  public StringKeyValue(string key, string value)
  {
    this.Key = key;
    this.Value = value;
  }

  public StringKeyValue Clone() => new StringKeyValue(this.Key, this.Value);

  object ICloneable.Clone() => (object) new StringKeyValue(this.Key, this.Value);
}
