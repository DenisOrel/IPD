// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Native.StringInfoCtype3
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.Native;

internal enum StringInfoCtype3 : ushort
{
  C3_NOTAPPLICABLE = 0,
  C3_DIACRITIC = 2,
  C3_VOWELMARK = 4,
  C3_SYMBOL = 8,
  C3_KATAKANA = 16, // 0x0010
  C3_HIRAGANA = 32, // 0x0020
  C3_HALFWIDTH = 64, // 0x0040
  C3_FULLWIDTH = 128, // 0x0080
  C3_IDEOGRAPH = 256, // 0x0100
  C3_KASHIDA = 512, // 0x0200
  C3_LEXICAL = 1024, // 0x0400
  C3_ALPHA = 32768, // 0x8000
}
