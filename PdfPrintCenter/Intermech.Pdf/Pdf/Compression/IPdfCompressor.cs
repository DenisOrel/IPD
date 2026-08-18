// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Compression.IPdfCompressor
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.IO;

#nullable disable
namespace Syncfusion.Pdf.Compression;

internal interface IPdfCompressor
{
  byte[] Compress(byte[] data);

  Stream Compress(Stream inputStream);

  byte[] Compress(string data);

  Stream Decompress(Stream inputStream);

  byte[] Decompress(byte[] value);

  byte[] Decompress(string value);

  string Name { get; }

  CompressionType Type { get; }
}
