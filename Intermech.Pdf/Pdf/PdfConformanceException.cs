// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfConformanceException
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf
{
    [Serializable]
    public class PdfConformanceException : PdfDocumentException
    {
      private const string ErrorMessage = "PDF Conformance-level exception.";

      public PdfConformanceException()
        : this("PDF Conformance-level exception.")
      {
      }

      public PdfConformanceException(Exception innerException)
        : this("PDF Conformance-level exception.", innerException)
      {
      }

      public PdfConformanceException(string message)
        : base(message)
      {
      }

      public PdfConformanceException(string message, Exception innerException)
        : base(message, innerException)
      {
      }
    }
}
