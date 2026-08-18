// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.PageNumExtensionStyle
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Режимы вида спецификации</summary>
[TypeConverter(typeof (EnumDescConverter))]
[Serializable]
public enum PageNumExtensionStyle
{
  /// <summary>Без расширения</summary>
  [Description("Без расширения")] None,
  /// <summary>Цифры после точки</summary>
  [Description("Цифра после точки - 3.1, 3.2, ...")] DigitsAfterDot,
  /// <summary>Буква после номера</summary>
  [Description("Буква после номера - 3a, 3б, ...")] Letter,
  /// <summary>Произвольное</summary>
  [Description("Произвольное")] Unknown,
}
