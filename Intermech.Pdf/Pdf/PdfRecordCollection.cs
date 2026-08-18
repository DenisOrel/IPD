// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfRecordCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Collections;
using System.Collections.Generic;


namespace Syncfusion.Pdf
{
    internal class PdfRecordCollection : IEnumerable
    {
      private List<PdfRecord> m_recordCollection = new List<PdfRecord>();

      internal PdfRecordCollection()
      {
      }

      public void Add(PdfRecord record) => this.m_recordCollection.Add(record);

      public IEnumerator GetEnumerator() => (IEnumerator) this.m_recordCollection.GetEnumerator();

      internal List<PdfRecord> RecordCollection
      {
        get => this.m_recordCollection;
        set => this.m_recordCollection = value;
      }
    }
}
