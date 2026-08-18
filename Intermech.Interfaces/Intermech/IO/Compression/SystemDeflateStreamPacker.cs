
// Type: Intermech.IO.Compression.SystemDeflateStreamPacker
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.IO;
using System.IO.Compression;


namespace Intermech.IO.Compression
{
    /// <summary>
    /// Реализует упаковщик потоков на основе системного класса System.IO.Compression.DeflateStream.
    /// Крайне неэффективен по памяти из-за внутреннего устройства DeflateStream.
    /// </summary>
    public sealed class SystemDeflateStreamPacker : StreamPacker
    {
      private readonly byte[] buffer;
      private readonly ProgressReporter progressBar;

      /// <summary>Создает объект.</summary>
      /// <param name="bufferSize">Размер буфера в байтах в операциях чтения/записи потоков</param>
      public SystemDeflateStreamPacker(int bufferSize)
      {
        StreamPackerContract.CheckBufferSize(bufferSize);
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
        using (DeflateStream deflateStream = new DeflateStream(target, CompressionMode.Compress, true))
        {
          for (int index = source.Read(this.buffer, 0, this.buffer.Length); index > 0; index = source.Read(this.buffer, 0, this.buffer.Length))
          {
            deflateStream.Write(this.buffer, 0, index);
            this.progressBar.UpdateProgress((long) index);
          }
          deflateStream.Flush();
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
        using (DeflateStream deflateStream = new DeflateStream(source, CompressionMode.Decompress, true))
        {
          for (int count = deflateStream.Read(this.buffer, 0, this.buffer.Length); count > 0; count = deflateStream.Read(this.buffer, 0, this.buffer.Length))
          {
            target.Write(this.buffer, 0, count);
            this.progressBar.SetProgress(source.Position);
          }
        }
        this.progressBar.Finish();
      }

      protected override void DoCleanup(bool packMode)
      {
        base.DoCleanup(packMode);
        this.progressBar.Reset();
      }
    }
}
