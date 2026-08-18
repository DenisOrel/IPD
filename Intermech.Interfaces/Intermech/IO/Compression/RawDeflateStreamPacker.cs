
// Type: Intermech.IO.Compression.RawDeflateStreamPacker
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Zip.Compression;
using System.IO;


namespace Intermech.IO.Compression
{
    /// <summary>
    /// Реализует упаковщик потоков на основе библиотеки ZipLib с помощью прямого взаимодействия с zip-объектами Inflater и Deflater.
    /// Это один из самых быстрых и экономичных по памяти упаковщиков. Полностью совместим по формату данных с ZipLibDeflateStreamPacker и взаимозаменяем с ним.
    /// </summary>
    public sealed class RawDeflateStreamPacker : StreamPacker, IStreamPackerStats
    {
      private readonly Deflater deflater;
      private readonly Inflater inflater;
      private readonly byte[] sourceBuffer;
      private readonly byte[] targetBuffer;
      private readonly ProgressReporter progressBar;
      private long lastPackInput;
      private long lastPackOutput;

      /// <summary>Создает объект.</summary>
      /// <param name="compressionLevel">Уровень сжатия от 0 (без сжатия) до 9 (максимальное сжатие)</param>
      /// <param name="noHeader">Флаг, требуется ли у упакованного потока создавать стандартный заголовок</param>
      /// <param name="bufferSize">Размер буфера в байтах в операциях чтения/записи потоков</param>
      public RawDeflateStreamPacker(int compressionLevel, bool noHeader, int bufferSize)
      {
        StreamPackerContract.CheckCompressionLevel(compressionLevel);
        StreamPackerContract.CheckBufferSize(bufferSize);
        this.deflater = new Deflater(compressionLevel, noHeader);
        this.deflater.SetStrategy(DeflateStrategy.Default);
        this.inflater = new Inflater(noHeader);
        this.sourceBuffer = new byte[bufferSize];
        this.targetBuffer = new byte[bufferSize];
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
        int num = source.Read(this.sourceBuffer, 0, this.sourceBuffer.Length);
        if (num != 0)
          this.deflater.SetInput(this.sourceBuffer, 0, num);
        else
          this.deflater.Finish();
        do
        {
          for (int count = this.deflater.Deflate(this.targetBuffer); count != 0; count = this.deflater.Deflate(this.targetBuffer))
            target.Write(this.targetBuffer, 0, count);
          this.progressBar.UpdateProgress((long) num);
          if (!this.deflater.IsFinished && this.deflater.IsNeedingInput)
          {
            num = source.Read(this.sourceBuffer, 0, this.sourceBuffer.Length);
            if (num != 0)
              this.deflater.SetInput(this.sourceBuffer, 0, num);
            else
              this.deflater.Finish();
          }
        }
        while (!this.deflater.IsFinished);
        this.progressBar.Finish();
        this.lastPackInput = this.deflater.TotalIn;
        this.lastPackOutput = this.deflater.TotalOut;
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
        int num = source.Read(this.sourceBuffer, 0, this.sourceBuffer.Length);
        if (num == 0)
          return;
        this.inflater.SetInput(this.sourceBuffer, 0, num);
        do
        {
          for (int count = this.inflater.Inflate(this.targetBuffer); count != 0; count = this.inflater.Inflate(this.targetBuffer))
            target.Write(this.targetBuffer, 0, count);
          this.progressBar.UpdateProgress((long) num);
          if (!this.inflater.IsFinished && this.inflater.IsNeedingInput)
          {
            num = source.Read(this.sourceBuffer, 0, this.sourceBuffer.Length);
            if (num == 0)
              throw new SharpZipBaseException("Невозможно распаковать входной поток данных, так как он неожиданно завершился. Возможно, входной поток не полный.");
            this.inflater.SetInput(this.sourceBuffer, 0, num);
          }
        }
        while (!this.inflater.IsFinished);
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

      /// <summary>
      /// Количество байт в исходном потоке в последней операции упаковки.
      /// </summary>
      long IStreamPackerStats.LastPackInput => this.lastPackInput;

      /// <summary>
      /// Количество байт в результирующем упакованном потоке в последней операции упаковки.
      /// </summary>
      long IStreamPackerStats.LastPackOutput => this.lastPackOutput;
    }
}
