// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.TtfCompositeGlyphFlags
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;

#nullable disable
namespace Syncfusion.Pdf.Graphics;

[Flags]
internal enum TtfCompositeGlyphFlags : ushort
{
  ARG_1_AND_2_ARE_WORDS = 1,
  ARGS_ARE_XY_VALUES = 2,
  MORE_COMPONENTS = 32, // 0x0020
  RESERVED = 16, // 0x0010
  ROUND_XY_TO_GRID = 4,
  USE_MY_METRICS = 512, // 0x0200
  WE_HAVE_A_SCALE = 8,
  WE_HAVE_A_TWO_BY_TWO = 128, // 0x0080
  WE_HAVE_AN_X_AND_Y_SCALE = 64, // 0x0040
  WE_HAVE_INSTRUCTIONS = 256, // 0x0100
}
