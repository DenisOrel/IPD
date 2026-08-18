// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Compression.PdfZlibCompressor
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Compression;
using Syncfusion.Pdf.Primitives;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;

#nullable disable
namespace Syncfusion.Pdf.Compression;

internal class PdfZlibCompressor : IPdfCompressor
{
  private const int DefaultBufferSize = 32 /*0x20*/;
  private static string DefaultName = StreamFilters.FlateDecode.ToString();
  private PdfCompressionLevel m_level;

  public PdfZlibCompressor() => this.m_level = PdfCompressionLevel.Normal;

  public PdfZlibCompressor(PdfCompressionLevel level)
    : this()
  {
    this.m_level = level;
  }

  public byte[] Compress(byte[] data)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    using (MemoryStream inputStream = new MemoryStream(data))
    {
      using (Stream stream = this.Compress((Stream) inputStream))
        return PdfStream.StreamToBytes(stream);
    }
  }

  public Stream Compress(Stream inputStream)
  {
    if (inputStream == null)
      throw new ArgumentNullException(nameof (inputStream));
    MemoryStream outputStream = new MemoryStream();
    CompressedStreamWriter compressedStreamWriter = new CompressedStreamWriter((Stream) outputStream, (Syncfusion.Compression.CompressionLevel) this.Level, false);
    byte[] buffer = new byte[inputStream.Length];
    inputStream.Position = 0L;
    inputStream.Read(buffer, 0, buffer.Length);
    byte[] data = buffer;
    int length = buffer.Length;
    compressedStreamWriter.Write(data, 0, length, true);
    return (Stream) outputStream;
  }

  public byte[] Compress(string data)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    using (MemoryStream inputStream = new MemoryStream(this.Encoding.GetBytes(data)))
    {
      using (Stream stream = this.Compress((Stream) inputStream))
        return PdfStream.StreamToBytes(stream);
    }
  }

  public Stream Decompress(Stream inputStream)
  {
    if (inputStream == null)
      throw new ArgumentNullException(nameof (inputStream));
    MemoryStream memoryStream1 = new MemoryStream();
    byte[] buffer1 = new byte[32 /*0x20*/];
    CompressedStreamReader compressedStreamReader1 = new CompressedStreamReader(inputStream);
    try
    {
      int count;
      while ((count = compressedStreamReader1.Read(buffer1, 0, buffer1.Length)) > 0)
        memoryStream1.Write(buffer1, 0, count);
    }
    catch (Exception ex)
    {
      if (ex.Message == "Wrong block length.")
      {
        inputStream.Position = 0L;
        CompressedStreamReader compressedStreamReader2 = new CompressedStreamReader(inputStream);
        byte[] buffer2 = new byte[1];
        MemoryStream memoryStream2 = new MemoryStream();
        try
        {
          int count;
          while ((count = compressedStreamReader2.Read(buffer2, 0, buffer2.Length)) > 0)
            memoryStream2.Write(buffer2, 0, count);
        }
        catch
        {
        }
        return (Stream) memoryStream2;
      }
      if (ex.Message != "Checksum check failed.")
        throw;
      try
      {
        inputStream.Position = 0L;
        inputStream.ReadByte();
        inputStream.ReadByte();
        using (DeflateStream deflateStream = new DeflateStream(inputStream, CompressionMode.Decompress, true))
        {
          byte[] buffer3 = new byte[4096 /*0x1000*/];
          memoryStream1 = new MemoryStream();
          while (true)
          {
            int count = deflateStream.Read(buffer3, 0, 4096 /*0x1000*/);
            if (count > 0)
              memoryStream1.Write(buffer3, 0, count);
            else
              break;
          }
          return (Stream) memoryStream1;
        }
      }
      catch
      {
      }
      return (Stream) memoryStream1;
    }
    return (Stream) memoryStream1;
  }

  public byte[] Decompress(byte[] data)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    if (data.Length == 0)
      return data;
    using (MemoryStream inputStream = new MemoryStream(data))
    {
      using (Stream stream = this.Decompress((Stream) inputStream))
        return PdfStream.StreamToBytes(stream);
    }
  }

  public byte[] Decompress(string data)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    using (MemoryStream inputStream = new MemoryStream(this.Encoding.GetBytes(data)))
    {
      using (Stream stream = this.Decompress((Stream) inputStream))
        return PdfStream.StreamToBytes(stream);
    }
  }

  public Encoding Encoding => Encoding.UTF8;

  public PdfCompressionLevel Level
  {
    get => this.m_level;
    set
    {
      if (this.m_level == value)
        return;
      this.m_level = value;
    }
  }

  public string Name => PdfZlibCompressor.DefaultName;

  public CompressionType Type => CompressionType.Zlib;
}
