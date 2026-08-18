
// Type: Intermech.Interfaces.PackedStream
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.IO.Compression;
using System;
using System.IO;


namespace Intermech.Interfaces
{
    /// <summary>
    /// <para>Реализует объект для упаковки/распаковки данных в формате ZLib.</para>
    /// <para>Внимание!!! Этот класс остался для совместимости. Он используется только в тех случаях, когда не ясно, можно ли использовать глобальный сервис IPackedStream.
    /// Во всех остальных случаях на клиенте и на сервере следует использовать сервис IPackedStream.</para>
    /// </summary>
    [Obsolete("Данный класс не рационально использует память - настоятельно рекомендуется его заменить на вызов сервиса IPackedStream")]
    public class PackedStream : IPackedStream
    {
      private const int bufferSize = 32768 /*0x8000*/;

      /// <summary>Упаковывает входной поток в выходной.</summary>
      /// <param name="outStream">Выходной поток</param>
      /// <param name="inStream">Входной поток</param>
      /// <param name="compressionLevel">Уровень сжатия от 0 до 9 (0 - без сжатия, 1-быстрое сжатие, 9 - наилучшее сжатие)</param>
      /// <returns>Размер выходного потока</returns>
      /// <exception cref="T:System.ArgumentNullException">Не указан один из потоков</exception>
      /// <exception cref="T:System.ArgumentOutOfRangeException">Неверно указан режим сжатия</exception>
      public long PackStream(Stream outStream, Stream inStream, int compressionLevel)
      {
        return this.PackStream(outStream, inStream, compressionLevel, (PercentEventHandler) null);
      }

      /// <summary>Упаковывает входной поток в выходной.</summary>
      /// <param name="outStream">Выходной поток</param>
      /// <param name="inStream">Входной поток</param>
      /// <param name="compressionLevel">Режим сжатия от 0 до 9 (0 - без сжатия, 1-быстрое сжатие, 9 - наилучшее сжатие)</param>
      /// <param name="progressHandler">Обработчик прогресса упаковки. Может быть не указан</param>
      /// <returns>Размер выходного потока</returns>
      /// <exception cref="T:System.ArgumentNullException">Не указан один из потоков</exception>
      /// <exception cref="T:System.ArgumentOutOfRangeException">Неверно указан режим сжатия</exception>
      public long PackStream(
        Stream outStream,
        Stream inStream,
        int compressionLevel,
        PercentEventHandler progressHandler)
      {
        if (outStream == null)
          throw new ArgumentNullException(nameof (outStream));
        if (inStream == null)
          throw new ArgumentNullException(nameof (inStream));
        if (compressionLevel < 0 || compressionLevel > 9)
          throw new ArgumentOutOfRangeException(nameof (compressionLevel));
        if (inStream.CanSeek)
          inStream.Seek(0L, SeekOrigin.Begin);
        if (outStream.CanSeek)
          outStream.Seek(0L, SeekOrigin.Begin);
        new RawDeflateStreamPacker(compressionLevel, false, 32768 /*0x8000*/).Pack(inStream, outStream, progressHandler);
        return outStream.Length;
      }

      /// <summary>Распаковывает входной поток в выходной.</summary>
      /// <param name="outStream">Выходной поток</param>
      /// <param name="inStream">Входной поток</param>
      /// <returns>Размер выходного потока</returns>
      /// <exception cref="T:System.ArgumentNullException">Не указан один из потоков</exception>
      public long UnpackStream(Stream outStream, Stream inStream)
      {
        return this.UnpackStream(outStream, inStream, (PercentEventHandler) null);
      }

      /// <summary>Распаковывает входной поток в выходной.</summary>
      /// <param name="outStream">Выходной поток</param>
      /// <param name="inStream">Входной поток</param>
      /// <param name="progressHandler">Обработчик прогресса распаковки. Может быть не указан</param>
      /// <returns>Размер выходного потока</returns>
      /// <exception cref="T:System.ArgumentNullException">Не указан один из потоков</exception>
      public long UnpackStream(Stream outStream, Stream inStream, PercentEventHandler progressHandler)
      {
        if (outStream == null)
          throw new ArgumentNullException(nameof (outStream));
        if (inStream == null)
          throw new ArgumentNullException(nameof (inStream));
        if (outStream.CanSeek)
          outStream.Seek(0L, SeekOrigin.Begin);
        if (inStream.CanSeek)
          inStream.Seek(0L, SeekOrigin.Begin);
        new RawDeflateStreamPacker(0, false, 32768 /*0x8000*/).Unpack(inStream, outStream, progressHandler);
        return outStream.Length;
      }
    }
}
