// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfAutomaticFieldInfoCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;

#nullable disable
namespace Syncfusion.Pdf;

internal class PdfAutomaticFieldInfoCollection : PdfCollection
{
  public int Add(PdfAutomaticFieldInfo fieldInfo)
  {
    return fieldInfo != null ? this.List.Add((object) fieldInfo) : throw new ArgumentNullException(nameof (fieldInfo));
  }
}
