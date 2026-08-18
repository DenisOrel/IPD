// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfLoadedAttachmentAnnotation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System;
using System.Drawing;
using System.IO;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfLoadedAttachmentAnnotation : PdfLoadedStyledAnnotation
    {
      private PdfCrossTable m_crossTable;
      private PdfAttachmentIcon m_icon;

      internal PdfLoadedAttachmentAnnotation(
        PdfDictionary dictionary,
        PdfCrossTable crossTable,
        RectangleF rectangle,
        string text)
        : base(dictionary, crossTable)
      {
        if (text == null)
          throw new ArgumentNullException(nameof (text));
        this.Dictionary = dictionary;
        this.m_crossTable = crossTable;
        this.Text = text;
      }

      public byte[] Data
      {
        get
        {
          byte[] data = (byte[]) null;
          if (this.m_crossTable.GetObject(this.Dictionary["FS"]) is PdfDictionary pdfDictionary1 && pdfDictionary1.ContainsKey("EF") && pdfDictionary1["EF"] is PdfDictionary pdfDictionary2 && pdfDictionary2.ContainsKey("F"))
          {
            PdfReferenceHolder pdfReferenceHolder = pdfDictionary2["F"] as PdfReferenceHolder;
            if (pdfReferenceHolder != (PdfReferenceHolder) null && pdfReferenceHolder.Object is PdfStream pdfStream)
            {
              pdfStream.Decompress();
              data = PdfStream.StreamToBytes((Stream) pdfStream.InternalStream);
            }
          }
          return data;
        }
      }

      public string FileName
      {
        get
        {
          PdfDictionary pdfDictionary = this.m_crossTable.GetObject(this.Dictionary["FS"]) as PdfDictionary;
          string fileName = " ";
          if (pdfDictionary.ContainsKey("Desc"))
            return (pdfDictionary["Desc"] as PdfString).Value;
          if (pdfDictionary.ContainsKey("UF"))
            fileName = (pdfDictionary["UF"] as PdfString).Value;
          return fileName;
        }
      }

      public PdfAttachmentIcon Icon
      {
        get => this.m_icon;
        set
        {
          this.m_icon = value;
          this.Dictionary.SetName("Name", this.m_icon.ToString());
        }
      }
    }
}
