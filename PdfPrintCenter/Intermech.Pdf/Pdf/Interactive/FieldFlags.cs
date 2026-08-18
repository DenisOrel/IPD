// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.FieldFlags
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

[Flags]
internal enum FieldFlags
{
  Comb = 16777216, // 0x01000000
  Combo = 131072, // 0x00020000
  CommitOnSelChange = 67108864, // 0x04000000
  Default = 0,
  DoNotScroll = 8388608, // 0x00800000
  DoNotSpellCheck = 4194304, // 0x00400000
  Edit = 262144, // 0x00040000
  FileSelect = 1048576, // 0x00100000
  Multiline = 4096, // 0x00001000
  MultiSelect = 2097152, // 0x00200000
  NoExport = 4,
  NoToggleToOff = 16384, // 0x00004000
  Password = 8192, // 0x00002000
  PushButton = 65536, // 0x00010000
  Radio = 32768, // 0x00008000
  RadiosInUnison = 33554432, // 0x02000000
  ReadOnly = 1,
  Required = 2,
  RichText = RadiosInUnison, // 0x02000000
  Sort = 524288, // 0x00080000
}
