// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Native.ETO
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;

#nullable disable
namespace Syncfusion.Pdf.Native;

[Flags]
internal enum ETO
{
  CLIPPED = 4,
  GLYPH_INDEX = 16, // 0x00000010
  IGNORELANGUAGE = 4096, // 0x00001000
  NUMERICSLATIN = 2048, // 0x00000800
  NUMERICSLOCAL = 1024, // 0x00000400
  OPAQUE = 2,
  PDY = 8192, // 0x00002000
  RTLREADING = 128, // 0x00000080
}
