// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfExtend
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;

#nullable disable
namespace Syncfusion.Pdf.Graphics;

[Flags]
public enum PdfExtend
{
  None = 0,
  Start = 1,
  End = 2,
  Both = End | Start, // 0x00000003
}
