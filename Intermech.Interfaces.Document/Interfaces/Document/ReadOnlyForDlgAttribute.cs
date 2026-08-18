// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ReadOnlyForDlgAttribute
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Атрибут ReadOnly специально для диалогов</summary>
[Serializable]
public class ReadOnlyForDlgAttribute : Attribute
{
  /// <summary>Только для чтения</summary>
  public bool IsReadOnly = true;

  /// <summary>Конструктор</summary>
  public ReadOnlyForDlgAttribute()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="readOnly">Только для чтения</param>
  public ReadOnlyForDlgAttribute(bool readOnly) => this.IsReadOnly = readOnly;
}
