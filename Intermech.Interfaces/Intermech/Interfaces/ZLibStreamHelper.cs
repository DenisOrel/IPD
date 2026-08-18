
// Type: Intermech.Interfaces.ZLibStreamHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System.IO;


namespace Intermech.Interfaces
{
    /// <summary>Вспомогательный класс для упаковки/распаковки (ZLib)</summary>
    public static class ZLibStreamHelper
    {
      /// <summary>Стандартный размер буфера при упаковке (4096 байт)</summary>
      internal static int ZlibBufSize = 4096 /*0x1000*/;

      /// <summary>
      /// Выполнить упаковку потока inStream со степенью сжатия packMode, результат вернуть в потоке outStream
      /// </summary>
      /// <param name="inStream">Поток с исходными данными</param>
      /// <param name="packMode">Степень сжатия</param>
      /// <param name="outStream">Поток с упакованными данными</param>
      /// <returns>Длина упакованных данных</returns>
      public static long PackStream(Stream inStream, ZLibCompressLevels packMode, Stream outStream)
      {
        if (inStream == null || inStream.Length <= 0L || outStream == null)
          return 0;
        outStream.SetLength(0L);
        IPackedStream service = ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, false);
        if (service != null)
        {
          long num = service.PackStream(outStream, inStream, (int) packMode);
          outStream.Position = 0L;
          return num;
        }
        byte[] buffer = new byte[ZLibStreamHelper.ZlibBufSize];
        inStream.Seek(0L, SeekOrigin.Begin);
        outStream.Seek(0L, SeekOrigin.Begin);
        DeflaterOutputStream deflaterOutputStream = new DeflaterOutputStream(outStream, new Deflater((int) packMode));
        try
        {
          while (true)
          {
            int count = inStream.Read(buffer, 0, ZLibStreamHelper.ZlibBufSize);
            if (count > 0)
              deflaterOutputStream.Write(buffer, 0, count);
            else
              break;
          }
        }
        catch
        {
        }
        deflaterOutputStream.Flush();
        deflaterOutputStream.Finish();
        outStream.Position = 0L;
        return outStream.Length;
      }

      /// <summary>
      /// Выполнить распаковку потока inStream, результат вернуть в потоку outStream
      /// </summary>
      /// <param name="inStream">Исходный поток с упакованными данными</param>
      /// <param name="outStream">Поток с распакованными данными</param>
      /// <returns>Длина распакованных данных</returns>
      public static long UnpackStream(Stream inStream, Stream outStream)
      {
        if (inStream == null || inStream.Length <= 0L || outStream == null)
          return 0;
        IPackedStream service = ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, false);
        if (service != null)
        {
          long num = service.UnpackStream(outStream, inStream);
          outStream.Position = 0L;
          return num;
        }
        byte[] buffer = new byte[ZLibStreamHelper.ZlibBufSize];
        inStream.Seek(0L, SeekOrigin.Begin);
        outStream.Seek(0L, SeekOrigin.Begin);
        InflaterInputStream inflaterInputStream = new InflaterInputStream(inStream);
        try
        {
          while (true)
          {
            int count = inflaterInputStream.Read(buffer, 0, ZLibStreamHelper.ZlibBufSize);
            if (count > 0)
              outStream.Write(buffer, 0, count);
            else
              break;
          }
        }
        catch
        {
          outStream.Seek(0L, SeekOrigin.Begin);
          outStream.SetLength(0L);
        }
        outStream.Flush();
        outStream.Position = 0L;
        return outStream.Length;
      }
    }
}
