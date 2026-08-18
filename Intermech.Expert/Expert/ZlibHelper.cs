// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.ZlibHelper
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using Intermech.Interfaces;
using Intermech.IO;
using System.IO;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.Expert;

/// <summary>
/// Упавку распаковку данных вынес в отдельный класс хелпер
/// </summary>
public class ZlibHelper
{
  /// <summary>
  /// Достаточно большой размер буфера, чтобы стоило использовать специальные потоки
  /// </summary>
  private const int LargeBufferSize = 2097152 /*0x200000*/;

  /// <summary>Распаковка XML буфера</summary>
  /// <param name="zipStream"></param>
  /// <returns></returns>
  public static Stream UnpackStream(Stream zipStream)
  {
    if (zipStream == null)
      return (Stream) null;
    Stream outStream = (Stream) new ImChunkedStream();
    ZLibStreamHelper.UnpackStream(zipStream, outStream);
    return outStream;
  }

  /// <summary>Упаковка потока</summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  public static Stream PackStream(Stream stream)
  {
    return ZlibHelper.PackStream(stream, ZLibCompressLevels.Level3);
  }

  /// <summary>Упаковка потока</summary>
  /// <param name="stream"></param>
  /// <param name="packMode"></param>
  /// <returns></returns>
  public static Stream PackStream(Stream stream, ZLibCompressLevels packMode)
  {
    Stream outStream = (Stream) new ImChunkedStream();
    ZLibStreamHelper.PackStream(stream, packMode, outStream);
    return outStream;
  }

  /// <summary>Распаковка XML буфера</summary>
  /// <param name="zipScr"></param>
  /// <returns></returns>
  public static Stream UnpackBuffer(byte[] zipScr)
  {
    using (Stream zipStream = (Stream) new MemoryStream(zipScr))
      return ZlibHelper.UnpackStream(zipStream);
  }

  /// <summary>Упаковка потока</summary>
  /// <param name="ms"></param>
  /// <returns></returns>
  public static byte[] PackBuffer(Stream ms)
  {
    using (Stream stream = ZlibHelper.PackStream(ms))
    {
      switch (stream)
      {
        case ImChunkedStream _:
          return (stream as ImChunkedStream).ToArray();
        case MemoryStream _:
          return (stream as MemoryStream).ToArray();
        default:
          return (byte[]) null;
      }
    }
  }

  /// <summary>Упаковка XML документа</summary>
  /// <param name="xDoc"></param>
  /// <returns></returns>
  internal static Stream PackXml(XmlDocument xDoc)
  {
    using (Stream stream = (Stream) new ImChunkedStream())
    {
      XmlTextWriter w = new XmlTextWriter(stream, Encoding.Unicode);
      xDoc.WriteTo((XmlWriter) w);
      w.Flush();
      return ZlibHelper.PackStream(stream);
    }
  }

  /// <summary>Распаковка XML документа</summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  internal static XmlDocument UnpackXml(Stream stream)
  {
    XmlDocument xmlDocument = new XmlDocument();
    using (Stream inStream = ZlibHelper.UnpackStream(stream))
      xmlDocument.Load(inStream);
    return xmlDocument;
  }

  /// <summary>Упаковка XML документа</summary>
  /// <param name="xDoc"></param>
  /// <returns></returns>
  public static byte[] PackXmlBuffer(XmlDocument xDoc)
  {
    using (ImChunkedStream baseOutputStream = new ImChunkedStream())
    {
      using (DeflaterOutputStream w1 = new DeflaterOutputStream((Stream) baseOutputStream, new Deflater(3)))
      {
        XmlTextWriter w2 = new XmlTextWriter((Stream) w1, Encoding.Unicode);
        xDoc.WriteTo((XmlWriter) w2);
        w2.Flush();
        w1.Flush();
        w1.Finish();
        baseOutputStream.Position = 0L;
        return baseOutputStream.ToArray();
      }
    }
  }

  /// <summary>Распаковка XML документа</summary>
  /// <param name="zipScr"></param>
  /// <returns></returns>
  public static XmlDocument UnpackXmlBuffer(byte[] zipScr)
  {
    using (Stream stream = (Stream) new MemoryStream(zipScr))
    {
      if (zipScr.Length < 2097152 /*0x200000*/)
        return ZlibHelper.UnpackXml(stream);
      using (InflaterInputStream inStream = new InflaterInputStream(stream))
      {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load((Stream) inStream);
        return xmlDocument;
      }
    }
  }

  /// <summary>Распаковать</summary>
  /// <param name="zipScr"></param>
  /// <returns></returns>
  public static Stream UnpackToStream(byte[] zipScr)
  {
    using (Stream stream = (Stream) new MemoryStream(zipScr))
      return zipScr.Length < 2097152 /*0x200000*/ ? ZlibHelper.UnpackStream(stream) : (Stream) new InflaterInputStream(stream);
  }
}
