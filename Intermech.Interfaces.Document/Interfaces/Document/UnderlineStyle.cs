// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.UnderlineStyle
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.ComponentModel;
using Intermech.Localization;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary> Стиль подчёркивания </summary>
[TypeConverter(typeof (EnumCustomConverter))]
[Flags]
public enum UnderlineStyle
{
  /// <summary> Подчёркивание отсутствует </summary>
  [CustomDescription("Attribute.Interfaces.Document_63")] None = 0,
  /// <summary> Подчеркивание </summary>
  [CustomDescription("Attribute.Interfaces.Document_64")] Underline = 1,
  /// <summary> Двойное подчеркивание </summary>
  [CustomDescription("Attribute.Interfaces.Document_65")] DoubleUnderline = 256, // 0x00000100
  /// <summary>Все</summary>
  [CustomDescription("Attribute.Interfaces.Document_66"), Browsable(false)] All = DoubleUnderline | Underline, // 0x00000101
}
