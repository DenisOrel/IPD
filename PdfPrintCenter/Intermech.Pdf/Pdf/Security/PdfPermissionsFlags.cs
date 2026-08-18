// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Security.PdfPermissionsFlags
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;

#nullable disable
namespace Syncfusion.Pdf.Security;

[Flags]
public enum PdfPermissionsFlags
{
  AccessibilityCopyContent = 512, // 0x00000200
  AssembleDocument = 1024, // 0x00000400
  CopyContent = 16, // 0x00000010
  Default = 0,
  EditAnnotations = 32, // 0x00000020
  EditContent = 8,
  FillFields = 256, // 0x00000100
  FullQualityPrint = 2048, // 0x00000800
  Print = 4,
}
