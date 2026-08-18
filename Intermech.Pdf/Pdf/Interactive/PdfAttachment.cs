// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfAttachment
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.IO;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfAttachment : PdfEmbeddedFileSpecification
    {
      public PdfAttachment(string fileName)
        : base(fileName)
      {
      }

      public PdfAttachment(string fileName, byte[] data)
        : base(fileName, data)
      {
      }

      public PdfAttachment(string fileName, Stream stream)
        : base(fileName, stream)
      {
      }
    }
}
