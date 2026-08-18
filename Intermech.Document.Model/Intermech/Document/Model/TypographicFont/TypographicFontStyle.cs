// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.TypographicFont.TypographicFontStyle
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;

#nullable disable
namespace Intermech.Document.Model.TypographicFont;

[Flags]
public enum TypographicFontStyle : ushort
{
  Italic = 1,
  Underscore = 2,
  Negative = 4,
  Outlined = 8,
  Strikeout = 16, // 0x0010
  Bold = 32, // 0x0020
  Regular = 64, // 0x0040
  UseTypoMetrics = 128, // 0x0080
  WWS = 256, // 0x0100
  Oblique = 512, // 0x0200
}
