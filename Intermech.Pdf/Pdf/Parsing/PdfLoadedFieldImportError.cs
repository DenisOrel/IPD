// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Parsing.PdfLoadedFieldImportError
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf.Parsing
{
    public class PdfLoadedFieldImportError
    {
      private Exception exceptionDetails;
      private PdfLoadedField loadedFieldName;

      internal PdfLoadedFieldImportError(PdfLoadedField field, Exception exception)
      {
        this.loadedFieldName = field;
        this.exceptionDetails = exception;
      }

      public Exception Exception => this.exceptionDetails;

      public PdfLoadedField Field => this.loadedFieldName;
    }
}
