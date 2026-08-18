// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfDocumentException
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;

#nullable disable
namespace Syncfusion.Pdf;

public class PdfDocumentException : PdfException
{
  private const string ErrorMessage = "Critical error on the document level.";

  public PdfDocumentException()
    : this("Critical error on the document level.")
  {
  }

  public PdfDocumentException(Exception innerException)
    : this("Critical error on the document level.", innerException)
  {
  }

  public PdfDocumentException(string message)
    : base(message)
  {
  }

  public PdfDocumentException(string message, Exception innerException)
    : base(message, innerException)
  {
  }
}
