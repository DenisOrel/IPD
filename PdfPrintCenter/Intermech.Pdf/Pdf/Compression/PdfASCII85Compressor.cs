// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Compression.PdfASCII85Compressor
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.IO;
using System.Text;

#nullable disable
namespace Syncfusion.Pdf.Compression;

internal class PdfASCII85Compressor : IPdfCompressor
{
  private const int m_asciiOffset = 33;
  private uint[] m_codeTable = new uint[5]
  {
    52200625U,
    614125U,
    7225U,
    85U,
    1U
  };
  private byte[] m_decodedBlock = new byte[4];
  private byte[] m_encodedBlock = new byte[5];
  private uint m_tuple;

  public Stream Compress(Stream inputStream) => (Stream) null;

  public byte[] Compress(byte[] data) => (byte[]) null;

  public byte[] Compress(string data) => (byte[]) null;

  private void DecodeBlock() => this.DecodeBlock(this.m_decodedBlock.Length);

  private void DecodeBlock(int bytes)
  {
    for (int index = 0; index < bytes; ++index)
      this.m_decodedBlock[index] = (byte) (this.m_tuple >> 24 - index * 8);
  }

  public byte[] Decompress(byte[] value)
  {
    MemoryStream outputData = new MemoryStream();
    this.Decompress(value, (Stream) outputData);
    byte[] buffer = new byte[outputData.Length];
    outputData.Position = 0L;
    outputData.Read(buffer, 0, (int) outputData.Length - 1);
    return buffer;
  }

  public Stream Decompress(Stream inputStream)
  {
    MemoryStream outputData = new MemoryStream();
    byte[] numArray = new byte[inputStream.Length];
    inputStream.Position = 0L;
    inputStream.Read(numArray, 0, (int) inputStream.Length - 1);
    this.Decompress(numArray, (Stream) outputData);
    return (Stream) outputData;
  }

  public byte[] Decompress(string value)
  {
    byte[] bytes = Encoding.UTF8.GetBytes(value);
    MemoryStream outputData = new MemoryStream();
    this.Decompress(bytes, (Stream) outputData);
    byte[] buffer = new byte[outputData.Length];
    outputData.Position = 0L;
    outputData.Read(buffer, 0, (int) outputData.Length - 1);
    return buffer;
  }

  public void Decompress(byte[] inputData, Stream outputData)
  {
    int index1 = 0;
    foreach (byte num in inputData)
    {
      char ch = Convert.ToChar(num);
      bool flag;
      switch (ch)
      {
        case char.MinValue:
        case '\b':
        case '\t':
        case '\n':
        case '\f':
        case '\r':
          flag = false;
          break;
        case 'z':
          if (index1 != 0)
            throw new PdfException("The character 'z' is invalid inside an ASCII85 block.");
          this.m_decodedBlock[0] = (byte) 0;
          this.m_decodedBlock[1] = (byte) 0;
          this.m_decodedBlock[2] = (byte) 0;
          this.m_decodedBlock[3] = (byte) 0;
          outputData.Write(this.m_decodedBlock, 0, this.m_decodedBlock.Length);
          flag = false;
          break;
        default:
          flag = true;
          break;
      }
      if (flag)
      {
        this.m_tuple += (uint) ((ulong) ((int) ch - 33) * (ulong) this.m_codeTable[index1]);
        ++index1;
        if (index1 == this.m_encodedBlock.Length)
        {
          this.DecodeBlock();
          outputData.Write(this.m_decodedBlock, 0, this.m_decodedBlock.Length);
          this.m_tuple = 0U;
          index1 = 0;
        }
      }
    }
    if (index1 == 0)
      return;
    int bytes = index1 - 1;
    this.m_tuple += this.m_codeTable[bytes];
    this.DecodeBlock(bytes);
    for (int index2 = 0; index2 < bytes; ++index2)
      outputData.WriteByte(this.m_decodedBlock[index2]);
  }

  public string Name => (string) null;

  public CompressionType Type => CompressionType.ASCII85;
}
