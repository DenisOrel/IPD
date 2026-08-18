// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfAnnotationFlags
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

[Flags]
public enum PdfAnnotationFlags
{
  Default = 0,
  Hidden = 2,
  Invisible = 1,
  Locked = 128, // 0x00000080
  NoRotate = 16, // 0x00000010
  NoView = 32, // 0x00000020
  NoZoom = 8,
  Print = 4,
  ReadOnly = 64, // 0x00000040
  ToggleNoView = 256, // 0x00000100
}
