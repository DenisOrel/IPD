// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Reports.ImDocumentDataUtils
// Assembly: Intermech.Interfaces.Reports, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3A40A7D8-A018-4590-B8F9-C63911182943
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Reports.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Reports.xml

using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using Intermech.Interfaces.Document;
using Intermech.IO;
using System.IO;

#nullable disable
namespace Intermech.Interfaces.Reports;

/// <summary>
/// 
/// </summary>
public static class ImDocumentDataUtils
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="zipScr"></param>
  /// <param name="updateDoc"></param>
  /// <returns></returns>
  /// <remarks>Вызов метода должен производиться из основного потока приложения</remarks>
  public static ImDocumentData UnpackImDocument(byte[] zipScr)
  {
    if (zipScr == null)
      return (ImDocumentData) null;
    using (Stream baseInputStream = (Stream) new MemoryStream(zipScr))
    {
      using (InflaterInputStream inflaterInputStream = new InflaterInputStream(baseInputStream))
        return ImDocumentData.LoadFromXml((Stream) inflaterInputStream);
    }
  }

  /// <summary>Запаковка документа</summary>
  /// <param name="imDoc"></param>
  /// <returns></returns>
  public static byte[] PackImDocument(ImDocumentData imDoc, ZLibCompressLevels compressLevel = ZLibCompressLevels.Level3)
  {
    if (imDoc == null)
      return (byte[]) null;
    using (ImChunkedStream baseOutputStream = new ImChunkedStream())
    {
      using (DeflaterOutputStream deflaterOutputStream = new DeflaterOutputStream((Stream) baseOutputStream, new Deflater((int) compressLevel)))
      {
        imDoc.SaveToXml((Stream) deflaterOutputStream);
        deflaterOutputStream.Flush();
        deflaterOutputStream.Finish();
        baseOutputStream.Position = 0L;
        return baseOutputStream.ToArray();
      }
    }
  }
}
