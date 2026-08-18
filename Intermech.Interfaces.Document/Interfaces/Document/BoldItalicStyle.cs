// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.BoldItalicStyle
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

/// <summary> Стиль шрифта </summary>
[Flags]
[TypeConverter(typeof (EnumCustomConverter))]
public enum BoldItalicStyle
{
  /// <summary>Обычный</summary>
  [CustomDescription("Attribute.Interfaces.Document_59")] Regular = 0,
  /// <summary> Жирный </summary>
  [CustomDescription("Attribute.Interfaces.Document_60")] Bold = 2,
  /// <summary> Курсив </summary>
  [CustomDescription("Attribute.Interfaces.Document_61")] Italic = 4,
  /// <summary>Все</summary>
  [CustomDescription("Attribute.Interfaces.Document_62")] BoldItalic = Italic | Bold, // 0x00000006
}
