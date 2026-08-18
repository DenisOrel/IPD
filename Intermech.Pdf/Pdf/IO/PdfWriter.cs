// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.IO.PdfWriter
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;
using System.IO;
using System.Text;


namespace Syncfusion.Pdf.IO
{
    internal class PdfWriter : IPdfWriter, IDisposable
    {
      private bool m_cannotSeek;
      private PdfDocumentBase m_document;
      private long m_length;
      private long m_position;
      private Stream m_stream;

      internal PdfWriter(Stream stream)
      {
        if (stream == null)
          throw new ArgumentNullException(nameof (stream));
        this.m_stream = stream.CanWrite ? stream : throw new ArgumentException("Can't write to the specified stream", nameof (stream));
        if (stream.CanRead && stream.CanSeek)
          return;
        this.m_cannotSeek = true;
      }

      internal void Close()
      {
        if (this.m_stream == null)
          return;
        this.m_stream.Flush();
        this.m_stream = (Stream) null;
      }

      public void Dispose() => this.Close();

      internal Stream GetStream() => this.m_stream;

      public void Write(IPdfPrimitive pdfObject) => pdfObject.Save((IPdfWriter) this);

      public void Write(long number) => new PdfNumber(number).Save((IPdfWriter) this);

      public void Write(float number) => new PdfNumber(number).Save((IPdfWriter) this);

      public void Write(byte[] data)
      {
        Stream stream = this.GetStream();
        int length = data.Length;
        this.m_length += (long) length;
        this.m_position += (long) length;
        byte[] buffer = data;
        int count = length;
        stream.Write(buffer, 0, count);
      }

      public void Write(char[] text) => this.Write(Encoding.UTF8.GetBytes(text));

      public void Write(string text) => this.Write(Encoding.UTF8.GetBytes(text));

      public PdfDocumentBase Document
      {
        get => this.m_document;
        set
        {
          this.m_document = value != null ? value : throw new ArgumentNullException(nameof (Document));
        }
      }

      public long Length => this.m_cannotSeek ? this.m_length : this.m_stream.Length;

      public long Position
      {
        get => this.m_cannotSeek ? this.m_position : this.m_stream.Position;
        set
        {
          this.m_stream.Position = value >= 0L ? value : throw new ArgumentOutOfRangeException(nameof (Position), "The stream position can't be less then zero.");
        }
      }

      private Stream Stream => this.GetStream();
    }
}
