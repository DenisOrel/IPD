// Decompiled with JetBrains decompiler
// Type: Intermech.StreamHelper
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces;
using System;
using System.IO;

#nullable disable
namespace Intermech;

public class StreamHelper
{
  public static void SaveToBlobStream(
    IBlobWriter writer,
    ProcessStreamDelegate SaveToStreamFunc,
    string note)
  {
    if (writer == null)
      return;
    MemoryStream memoryStream = new MemoryStream();
    try
    {
      SaveToStreamFunc((Stream) memoryStream);
      BlobInformation blobInfo = new BlobInformation(memoryStream.Length, memoryStream.Length, DateTime.Now, "", ArcMethods.NotPacked, note);
      if (!writer.OpenBlob(blobInfo, false))
        return;
      writer.WriteDataBlock(memoryStream.ToArray());
    }
    finally
    {
      memoryStream.Close();
    }
  }

  public static void LoadFromBlobStream(
    IBlobReader reader,
    ProcessStreamDelegate LoadFromStreamFunc)
  {
    if (reader == null)
      return;
    using (MemoryStream stream = StreamHelper.BlobReaderToStream(reader))
      LoadFromStreamFunc((Stream) stream);
  }

  public static void StreamToFile(MemoryStream stream, string FileName)
  {
    stream.Position = 0L;
    using (FileStream fileStream = new FileStream(FileName, FileMode.Create))
      fileStream.Write(stream.ToArray(), 0, (int) stream.Length);
  }

  /// <summary>
  /// Возвращает MemoryStream, которому после использования нужно сделать Dispose или Close!
  /// </summary>
  public static MemoryStream BlobReaderToStream(IBlobReader reader)
  {
    MemoryStream inStream = new MemoryStream();
    BlobInformation blobInformation = reader.OpenBlob(0);
    try
    {
      if (blobInformation.RealFileSize > 0L)
      {
        int num = blobInformation.ArcMethod == ArcMethods.ZLibPacked ? (int) blobInformation.PackedFileSize : (int) blobInformation.RealFileSize;
        byte[] buffer = reader.ReadDataBlock(num);
        inStream.Write(buffer, 0, num);
        if (blobInformation.ArcMethod == ArcMethods.ZLibPacked)
        {
          MemoryStream outStream = new MemoryStream();
          ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).UnpackStream((Stream) outStream, (Stream) inStream);
          inStream.Close();
          inStream = outStream;
        }
      }
    }
    finally
    {
      reader.CloseBlob();
    }
    inStream.Position = 0L;
    return inStream;
  }

  public static void StreamToBlobWriter(MemoryStream ms, IBlobWriter iw)
  {
    BlobInformation blobInfo = new BlobInformation(ms.Length, ms.Length, DateTime.Now, "", ArcMethods.NotPacked, "");
    if (!iw.OpenBlob(blobInfo, false))
      return;
    iw.WriteDataBlock(ms.ToArray());
  }

  public static string StreamToString(Stream ms)
  {
    ms.Position = 0L;
    StreamReader streamReader = new StreamReader(ms);
    try
    {
      return streamReader.ReadToEnd();
    }
    finally
    {
      streamReader.Close();
    }
  }

  /// <summary>Обязательно использовать внутри using!</summary>
  /// <param name="s"></param>
  /// <returns></returns>
  public static Stream StringToStream(string s)
  {
    MemoryStream stream = new MemoryStream();
    StreamWriter streamWriter = new StreamWriter((Stream) stream);
    streamWriter.Write(s);
    streamWriter.Flush();
    stream.Position = 0L;
    return (Stream) stream;
  }
}
