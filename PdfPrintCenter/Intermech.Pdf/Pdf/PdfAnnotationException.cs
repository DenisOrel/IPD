// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfAnnotationException
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;

#nullable disable
namespace Syncfusion.Pdf;

public class PdfAnnotationException : PdfDocumentException
{
  private const string ErrorMessage = "Annotation exception.";

  public PdfAnnotationException()
    : this("Annotation exception.")
  {
  }

  public PdfAnnotationException(Exception innerException)
    : this("Annotation exception.", innerException)
  {
  }

  public PdfAnnotationException(string message)
    : base(message)
  {
  }

  public PdfAnnotationException(string message, Exception innerException)
    : base(message, innerException)
  {
  }
}
