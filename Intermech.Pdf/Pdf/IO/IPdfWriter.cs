// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.IO.IPdfWriter
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;


namespace Syncfusion.Pdf.IO
{
    internal interface IPdfWriter
    {
      void Write(IPdfPrimitive pdfObject);

      void Write(long number);

      void Write(float number);

      void Write(string text);

      void Write(byte[] data);

      void Write(char[] text);

      PdfDocumentBase Document { get; set; }

      long Length { get; }

      long Position { get; set; }
    }
}
