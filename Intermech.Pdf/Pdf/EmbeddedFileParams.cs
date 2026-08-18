// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.EmbeddedFileParams
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;


namespace Syncfusion.Pdf
{
    internal class EmbeddedFileParams : IPdfWrapper
    {
      private DateTime m_creationDate = DateTime.Now;
      private PdfDictionary m_dictionary = new PdfDictionary();
      private DateTime m_modificationDate = DateTime.Now;
      private int m_size;

      public EmbeddedFileParams()
      {
        this.CreationDate = DateTime.Now;
        this.ModificationDate = DateTime.Now;
      }

      public DateTime CreationDate
      {
        get => this.m_creationDate;
        set
        {
          this.m_creationDate = value;
          this.m_dictionary.SetDateTime(nameof (CreationDate), value);
        }
      }

      public DateTime ModificationDate
      {
        get => this.m_modificationDate;
        set
        {
          this.m_modificationDate = value;
          this.m_dictionary.SetDateTime("ModDate", value);
        }
      }

      internal int Size
      {
        get => this.m_size;
        set
        {
          if (this.m_size == value)
            return;
          this.m_size = value;
          this.m_dictionary.SetNumber(nameof (Size), this.m_size);
        }
      }

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;
    }
}
