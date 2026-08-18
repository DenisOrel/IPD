// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfException
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf
{
    public class PdfException : Exception
    {
      public PdfException()
      {
      }

      public PdfException(string message)
        : base(message)
      {
      }

      public PdfException(string message, Exception innerException)
        : base(message, innerException)
      {
      }
    }
}
