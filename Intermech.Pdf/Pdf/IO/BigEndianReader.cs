// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.IO.BigEndianReader
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.IO;
using System.Text;


namespace Syncfusion.Pdf.IO
{
    internal class BigEndianReader
    {
      private readonly Encoding c_encoding = Encoding.GetEncoding(1252);
      private const float c_fraction = 16384f;
      internal const int Int16Size = 2;
      internal const int Int32Size = 4;
      internal const int Int64Size = 8;
      private BinaryReader m_reader;

      public BigEndianReader(BinaryReader reader)
      {
        this.m_reader = reader != null ? reader : throw new ArgumentNullException(nameof (reader));
      }

      public void Close()
      {
        if (this.m_reader == null)
          return;
        if (this.m_reader.BaseStream != null)
          this.m_reader.BaseStream.Close();
        this.m_reader.Close();
        this.m_reader = (BinaryReader) null;
      }

      public int Read(byte[] buffer, int index, int count)
      {
        if (buffer == null)
          throw new ArgumentNullException(nameof (buffer));
        int num1 = 0;
        do
        {
          int num2 = this.m_reader.Read(buffer, index + num1, count - num1);
          num1 += num2;
        }
        while (num1 < count);
        return num1;
      }

      public byte ReadByte() => this.m_reader.ReadByte();

      public byte[] ReadBytes(int count) => this.m_reader.ReadBytes(count);

      public float ReadFixed()
      {
        return (float) BitConverter.ToInt16(this.Reverse(this.m_reader.ReadBytes(2)), 0) + (float) BitConverter.ToInt16(this.Reverse(this.m_reader.ReadBytes(2)), 0) / 16384f;
      }

      public short ReadInt16() => BitConverter.ToInt16(this.Reverse(this.m_reader.ReadBytes(2)), 0);

      public int ReadInt32() => BitConverter.ToInt32(this.Reverse(this.m_reader.ReadBytes(4)), 0);

      public long ReadInt64() => BitConverter.ToInt64(this.Reverse(this.m_reader.ReadBytes(8)), 0);

      public string ReadString(int len) => this.ReadString(len, false);

      public string ReadString(int len, bool unicode)
      {
        if (unicode)
        {
          byte[] bytes = this.ReadBytes(len);
          return Encoding.BigEndianUnicode.GetString(bytes, 0, bytes.Length);
        }
        byte[] bytes1 = this.ReadBytes(len);
        return this.c_encoding.GetString(bytes1, 0, bytes1.Length);
      }

      public ushort ReadUInt16() => BitConverter.ToUInt16(this.Reverse(this.m_reader.ReadBytes(2)), 0);

      public uint ReadUInt32() => BitConverter.ToUInt32(this.Reverse(this.m_reader.ReadBytes(4)), 0);

      public ulong ReadUInt64() => BitConverter.ToUInt64(this.Reverse(this.m_reader.ReadBytes(8)), 0);

      public byte[] Reverse(byte[] buffer)
      {
        if (buffer == null)
          throw new ArgumentNullException(nameof (buffer));
        Array.Reverse((Array) buffer);
        return buffer;
      }

      public void Seek(long position)
      {
        if (!this.m_reader.BaseStream.CanSeek)
          return;
        this.m_reader.BaseStream.Position = position;
      }

      public void Skip(long numBytes) => this.Seek(this.m_reader.BaseStream.Position + numBytes);

      public Stream BaseStream => this.m_reader.BaseStream;

      public BinaryReader Reader
      {
        get => this.m_reader;
        set => this.m_reader = value;
      }
    }
}
