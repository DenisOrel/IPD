
// Type: Intermech.Interfaces.IPackedStream
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.IO;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса для упаковки/распаковки данных в формате ZLib.
    /// </summary>
    public interface IPackedStream
    {
      /// <summary>Упаковывает входной поток в выходной.</summary>
      /// <param name="outStream">Выходной поток</param>
      /// <param name="inStream">Входной поток</param>
      /// <param name="compressionLevel">Уровень сжатия от 0 до 9 (0 - без сжатия, 1-быстрое сжатие, 9 - наилучшее сжатие)</param>
      /// <returns>Размер выходного потока</returns>
      /// <exception cref="T:System.ArgumentNullException">Не указан один из потоков</exception>
      /// <exception cref="T:System.ArgumentOutOfRangeException">Неверно указан режим сжатия</exception>
      long PackStream(Stream outStream, Stream inStream, int compressionLevel);

      /// <summary>Упаковывает входной поток в выходной.</summary>
      /// <param name="outStream">Выходной поток</param>
      /// <param name="inStream">Входной поток</param>
      /// <param name="compressionLevel">Уровень сжатия от 0 до 9 (0 - без сжатия, 1-быстрое сжатие, 9 - наилучшее сжатие)</param>
      /// <param name="progressHandler">Обработчик прогресса упаковки. Может быть не указан</param>
      /// <returns>Размер выходного потока</returns>
      /// <exception cref="T:System.ArgumentNullException">Не указан один из потоков</exception>
      /// <exception cref="T:System.ArgumentOutOfRangeException">Неверно указан режим сжатия</exception>
      long PackStream(
        Stream outStream,
        Stream inStream,
        int compressionLevel,
        PercentEventHandler progressHandler);

      /// <summary>Распаковывает входной поток в выходной.</summary>
      /// <param name="outStream">Выходной поток</param>
      /// <param name="inStream">Входной поток</param>
      /// <returns>Размер выходного потока</returns>
      /// <exception cref="T:System.ArgumentNullException">Не указан один из потоков</exception>
      long UnpackStream(Stream outStream, Stream inStream);

      /// <summary>Распаковывает входной поток в выходной.</summary>
      /// <param name="outStream">Выходной поток</param>
      /// <param name="inStream">Входной поток</param>
      /// <param name="progressHandler">Обработчик прогресса распаковки. Может быть не указан</param>
      /// <returns>Размер выходного потока</returns>
      /// <exception cref="T:System.ArgumentNullException">Не указан один из потоков</exception>
      long UnpackStream(Stream outStream, Stream inStream, PercentEventHandler progressHandler);
    }
}
