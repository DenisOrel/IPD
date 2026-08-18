// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Compression.DefaultCompressor
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;
using System.IO;


namespace Syncfusion.Pdf.Compression
{
    internal class DefaultCompressor : IPdfCompressor
    {
      public byte[] Compress(byte[] data)
      {
        return data != null ? data : throw new ArgumentNullException(nameof (data));
      }

      public Stream Compress(Stream inputStream)
      {
        return inputStream != null ? inputStream : throw new ArgumentNullException(nameof (inputStream));
      }

      public byte[] Compress(string data)
      {
        return data != null ? PdfString.StringToByte(data) : throw new ArgumentNullException(nameof (data));
      }

      public Stream Decompress(Stream inputStream)
      {
        return inputStream != null ? inputStream : throw new ArgumentNullException(nameof (inputStream));
      }

      public byte[] Decompress(byte[] value)
      {
        return value != null ? value : throw new ArgumentNullException(nameof (value));
      }

      public byte[] Decompress(string value)
      {
        return value != null ? PdfString.StringToByte(value) : throw new ArgumentNullException(nameof (value));
      }

      public string Name => string.Empty;

      public CompressionType Type => CompressionType.None;
    }
}
