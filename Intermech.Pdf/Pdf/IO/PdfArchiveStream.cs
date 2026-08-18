// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.IO.PdfArchiveStream
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using Syncfusion.Pdf.Security;
using System;
using System.Collections;
using System.IO;


namespace Syncfusion.Pdf.IO
{
    internal class PdfArchiveStream : PdfStream
    {
      private PdfDocumentBase m_document;
      private SortedListEx m_indices;
      private MemoryStream m_objects;
      private IPdfWriter m_objectWriter;
      private StreamWriter m_writer;

      internal PdfArchiveStream(PdfDocumentBase document)
      {
        this.m_document = document != null ? document : throw new ArgumentNullException(nameof (document));
        this.m_objects = new MemoryStream(1000);
        this.m_objectWriter = (IPdfWriter) new PdfWriter((Stream) this.m_objects);
        this.m_objectWriter.Document = this.m_document;
        this.m_indices = new SortedListEx(16 /*0x10*/);
      }

      internal new void Clear()
      {
        this.m_indices.Clear();
        if (this.m_objects != null)
          this.m_objects.Close();
        if (this.m_writer != null)
          this.m_writer.Close();
        if (this.m_objectWriter != null)
          this.m_objectWriter = (IPdfWriter) null;
        base.Clear();
      }

      public int GetIndex(long objNum) => this.m_indices.IndexOfValue((object) objNum);

      public override void Save(IPdfWriter writer)
      {
        using (MemoryStream memoryStream = new MemoryStream((int) this.m_objects.Length + 100))
        {
          using (this.m_writer = new StreamWriter((Stream) memoryStream))
          {
            this.SaveIndices();
            this.m_writer.Flush();
            this["First"] = (IPdfPrimitive) new PdfNumber(this.m_writer.BaseStream.Position);
            this.SaveObjects();
            this.m_writer.Flush();
            this.Data = memoryStream.ToArray();
          }
        }
        this["N"] = (IPdfPrimitive) new PdfNumber(this.m_indices.Count);
        this["Type"] = (IPdfPrimitive) new PdfName("ObjStm");
        base.Save(writer);
      }

      private void SaveIndices()
      {
        foreach (long key in (IEnumerable) this.m_indices.Keys)
        {
          this.m_writer.Write(this.m_indices[(object) key]);
          this.m_writer.Write(" ");
          this.m_writer.Write(key);
          this.m_writer.Write("\r\n");
        }
      }

      public void SaveObject(IPdfPrimitive obj, PdfReference reference)
      {
        this.m_indices[(object) this.m_objectWriter.Position] = (object) reference.ObjNum;
        PdfSecurity security = this.m_document.Security;
        bool enabled = security.Enabled;
        security.Enabled = false;
        obj.Save(this.m_objectWriter);
        security.Enabled = enabled;
        this.m_objectWriter.Write("\r\n");
      }

      private void SaveObjects()
      {
        byte[] array = this.m_objects.ToArray();
        this.m_writer.BaseStream.Write(array, 0, array.Length);
      }

      internal int ObjCount => this.m_indices.Count;

      private class ObjInfo
      {
        internal int Index;
        internal IPdfPrimitive Obj;

        internal ObjInfo(IPdfPrimitive obj)
        {
          this.Obj = obj;
          this.Index = 0;
        }
      }
    }
}
