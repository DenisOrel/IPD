// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.FontDescriptorFlags
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.Graphics;

internal enum FontDescriptorFlags
{
  FixedPitch = 1,
  Serif = 2,
  Symbolic = 4,
  Script = 8,
  Nonsymbolic = 32, // 0x00000020
  Italic = 64, // 0x00000040
  ForceBold = 262144, // 0x00040000
}
