
// Type: Intermech.IO.Compression.ZipLibDeflateStreamPacker
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System.IO;


namespace Intermech.IO.Compression
{
    /// <summary>
    /// Реализует упаковщик потоков на основе библиотеки ZipLib. Реализован с использованием объектов типа DeflaterOutputStream, InflaterInputStream, а также
    /// обеспечивает повторное использование zip-объектов Inflater и Deflater. Не эффективен по памяти из-за внутренней реализации DeflaterOutputStream и InflaterInputStream.
    /// </summary>
    public sealed class ZipLibDeflateStreamPacker : StreamPacker
    {
      private readonly Deflater deflater;
      private readonly Inflater inflater;
      private readonly byte[] buffer;
      private readonly ProgressReporter progressBar;

      /// <summary>Создает объект.</summary>
      /// <param name="compressionLevel">Уровень сжатия от 0 (без сжатия) до 9 (максимальное сжатие)</param>
      /// <param name="noHeader">Флаг, требуется ли у упакованного потока создавать стандартный заголовок</param>
      /// <param name="bufferSize">Размер буфера в байтах в операциях чтения/записи потоков</param>
      public ZipLibDeflateStreamPacker(int compressionLevel, bool noHeader, int bufferSize)
      {
        StreamPackerContract.CheckCompressionLevel(compressionLevel);
        StreamPackerContract.CheckBufferSize(bufferSize);
        this.deflater = new Deflater(compressionLevel, noHeader);
        this.deflater.SetStrategy(DeflateStrategy.Default);
        this.inflater = new Inflater(noHeader);
        this.buffer = new byte[bufferSize];
        this.progressBar = new ProgressReporter();
      }

      /// <summary>Реализует упаковку потока.</summary>
      /// <param name="source">Исходный неупакованный поток</param>
      /// <param name="target">Результирующий упакованный поток</param>
      /// <param name="progressHandler">Обработчик прогресса упаковки. Может быть не указан</param>
      protected override void DoPack(Stream source, Stream target, PercentEventHandler progressHandler)
      {
        if (progressHandler != null && source.CanSeek)
          this.progressBar.Initialize(source.Length, progressHandler);
        using (DeflaterOutputStream deflaterOutputStream = new DeflaterOutputStream(target, this.deflater, this.buffer.Length))
        {
          deflaterOutputStream.IsStreamOwner = false;
          for (int index = source.Read(this.buffer, 0, this.buffer.Length); index > 0; index = source.Read(this.buffer, 0, this.buffer.Length))
          {
            deflaterOutputStream.Write(this.buffer, 0, index);
            this.progressBar.UpdateProgress((long) index);
          }
          deflaterOutputStream.Flush();
        }
        this.progressBar.Finish();
      }

      /// <summary>Реализует распаковку потока.</summary>
      /// <param name="source">Исходный упакованный поток</param>
      /// <param name="target">Результирующий распакованный поток</param>
      /// <param name="progressHandler">Обработчик прогресса распаковки. Может быть не указан</param>
      protected override void DoUnpack(
        Stream source,
        Stream target,
        PercentEventHandler progressHandler)
      {
        if (progressHandler != null && source.CanSeek)
          this.progressBar.Initialize(source.Length, progressHandler);
        using (InflaterInputStream inflaterInputStream = new InflaterInputStream(source, this.inflater, this.buffer.Length))
        {
          inflaterInputStream.IsStreamOwner = false;
          for (int count = inflaterInputStream.Read(this.buffer, 0, this.buffer.Length); count > 0; count = inflaterInputStream.Read(this.buffer, 0, this.buffer.Length))
          {
            target.Write(this.buffer, 0, count);
            this.progressBar.SetProgress(source.Position);
          }
        }
        this.progressBar.Finish();
      }

      /// <summary>
      /// Реализует очистку внутренних структур упаковщика после завершения операций упаковки или распаковки.
      /// </summary>
      /// <param name="packMode">true - если выполнялась упаковка потока, false - если выполнялась распаковка</param>
      protected override void DoCleanup(bool packMode)
      {
        base.DoCleanup(packMode);
        this.progressBar.Reset();
        if (packMode)
          this.deflater.Reset();
        else
          this.inflater.Reset();
      }
    }
}
