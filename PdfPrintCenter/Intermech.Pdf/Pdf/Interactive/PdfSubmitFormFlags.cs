// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfSubmitFormFlags
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

[Flags]
public enum PdfSubmitFormFlags
{
  CanonicalFormat = 512, // 0x00000200
  EmbedForm = 4096, // 0x00001000
  ExclFKey = 2048, // 0x00000800
  ExclNonUserAnnots = 1024, // 0x00000400
  ExportFormat = 4,
  GetMethod = 8,
  IncludeAnnotations = 128, // 0x00000080
  IncludeAppendSaves = 64, // 0x00000040
  IncludeExclude = 1,
  IncludeNoValueFields = 2,
  SubmitCoordinates = 16, // 0x00000010
  SubmitPdf = 256, // 0x00000100
  Xfdf = 32, // 0x00000020
}
