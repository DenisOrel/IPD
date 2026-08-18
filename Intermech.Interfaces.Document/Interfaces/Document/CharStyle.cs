// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.CharStyle
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

/// <summary>Стиль символа для RTF</summary>
[Flags]
[TypeConverter(typeof (EnumCustomConverter))]
public enum CharStyle
{
  /// <summary>Обычный</summary>
  [CustomDescription("Attribute.Interfaces.Document_67")] Regular = 0,
  /// <summary>Полужирный</summary>
  [CustomDescription("Attribute.Interfaces.Document_68")] Bold = 2,
  /// <summary>Подчеркивание</summary>
  [CustomDescription("Attribute.Interfaces.Document_69")] Underline = 1,
  /// <summary>Двойное подчеркивание</summary>
  [CustomDescription("Attribute.Interfaces.Document_70"), Browsable(false)] DoubleUnderline = 256, // 0x00000100
  /// <summary>Курсив</summary>
  [CustomDescription("Attribute.Interfaces.Document_71")] Italic = 4,
  /// <summary>Зачеркнутый</summary>
  [CustomDescription("Attribute.Interfaces.Document_72")] Strikethrough = 8,
  /// <summary>Надстрочный</summary>
  [CustomDescription("Attribute.Interfaces.Document_73"), Browsable(false)] Superscript = 16, // 0x00000010
  /// <summary>Подстрочный</summary>
  [CustomDescription("Attribute.Interfaces.Document_74"), Browsable(false)] Subscript = 32, // 0x00000020
  /// <summary>Скрытый</summary>
  [CustomDescription("Attribute.Interfaces.Document_75"), Browsable(false)] HiddenText = 64, // 0x00000040
  /// <summary>Защищенный</summary>
  [CustomDescription("Attribute.Interfaces.Document_76"), Browsable(false)] ProtectedText = 512, // 0x00000200
  /// <summary>Гиперссылка</summary>
  [CustomDescription("Attribute.Interfaces.Document_77"), Browsable(false)] Hyperlink = 16384, // 0x00004000
  /// <summary>Все прописные</summary>
  [CustomDescription("Attribute.Interfaces.Document_78"), Browsable(false)] AllCaps = 65536, // 0x00010000
  /// <summary>Все малые прописные</summary>
  [CustomDescription("Attribute.Interfaces.Document_79"), Browsable(false)] AllSmallCaps = 131072, // 0x00020000
  /// <summary>Рисунок</summary>
  [CustomDescription("Attribute.Interfaces.Document_80"), Browsable(false)] Picture = 128, // 0x00000080
  /// <summary>Стиль не определен</summary>
  [Browsable(false)] StrikeNull = 1048576, // 0x00100000
  /// <summary>Двойное зачёркивание</summary>
  [CustomDescription("Attribute.Interfaces.Document_591"), Browsable(false)] DoubleStrikethrough = 524288, // 0x00080000
}
