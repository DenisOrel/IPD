
// Type: Intermech.Interfaces.PackedStreamService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.IO.Compression;
using System;
using System.IO;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Реализует сервис для упаковки/распаковки данных в формате ZLib. Класс является thread safe.
    /// </summary>
    public class PackedStreamService : IPackedStream
    {
      private readonly StreamPackerPool[] packerPoolByCompressionLevel;

      /// <summary>Создает объект.</summary>
      public PackedStreamService()
      {
        this.packerPoolByCompressionLevel = new StreamPackerPool[10];
        for (int index = 0; index < this.packerPoolByCompressionLevel.Length; ++index)
        {
          int compressionLevel = index;
          this.packerPoolByCompressionLevel[index] = new StreamPackerPool(0, 8, (Func<StreamPacker>) (() => (StreamPacker) new RawDeflateStreamPacker(compressionLevel, false, 262144 /*0x040000*/)));
        }
      }

      /// <summary>Упаковывает входной поток в выходной.</summary>
      /// <param name="outStream">Выходной поток</param>
      /// <param name="inStream">Входной поток</param>
      /// <param name="compressionLevel">Уроверь сжатия от 0 до 9 (0 - без сжатия, 1-быстрое сжатие, 9 - наилучшее сжатие)</param>
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
      /// <param name="compressionLevel">Уровень сжатия от 0 до 9 (0 - без сжатия, 1-быстрое сжатие, 9 - наилучшее сжатие)</param>
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
        StreamPackerPool streamPackerPool = this.packerPoolByCompressionLevel[compressionLevel];
        StreamPacker packer = streamPackerPool.Allocate();
        try
        {
          packer.Pack(inStream, outStream, progressHandler);
        }
        finally
        {
          streamPackerPool.Release(packer);
        }
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
        StreamPackerPool streamPackerPool = this.packerPoolByCompressionLevel[0];
        StreamPacker packer = streamPackerPool.Allocate();
        try
        {
          packer.Unpack(inStream, outStream, progressHandler);
        }
        finally
        {
          streamPackerPool.Release(packer);
        }
        return outStream.Length;
      }
    }
}
