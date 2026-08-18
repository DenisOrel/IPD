// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.IO.BigEndianWriter
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Text;


namespace Syncfusion.Pdf.IO
{
    internal class BigEndianWriter
    {
      private readonly Encoding c_encoding = Encoding.GetEncoding("windows-1252");
      private const float c_fraction = 16384f;
      internal const int Int16Size = 2;
      internal const int Int32Size = 4;
      internal const int Int64Size = 8;
      private byte[] m_buffer;
      private int m_position;

      public BigEndianWriter(int capacity) => this.m_buffer = new byte[capacity];

      private void Flush(byte[] buff)
      {
        if (buff == null)
          throw new ArgumentNullException(nameof (buff));
        Array.Copy((Array) buff, 0, (Array) this.m_buffer, this.m_position, buff.Length);
        this.m_position += buff.Length;
      }

      public void Write(short value)
      {
        byte[] bytes = BitConverter.GetBytes(value);
        Array.Reverse((Array) bytes);
        this.Flush(bytes);
      }

      public void Write(int value)
      {
        byte[] bytes = BitConverter.GetBytes(value);
        Array.Reverse((Array) bytes);
        this.Flush(bytes);
      }

      public void Write(string value)
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        this.Flush(this.c_encoding.GetBytes(value));
      }

      public void Write(ushort value)
      {
        byte[] bytes = BitConverter.GetBytes(value);
        Array.Reverse((Array) bytes);
        this.Flush(bytes);
      }

      public void Write(byte[] value) => this.Flush(value);

      public void Write(uint value)
      {
        byte[] bytes = BitConverter.GetBytes(value);
        Array.Reverse((Array) bytes);
        this.Flush(bytes);
      }

      public byte[] Data => this.m_buffer;

      public int Position => this.m_position;
    }
}
