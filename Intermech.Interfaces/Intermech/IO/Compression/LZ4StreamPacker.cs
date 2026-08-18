
// Type: Intermech.IO.Compression.LZ4StreamPacker
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using LZ4n;
using System;
using System.IO;


namespace Intermech.IO.Compression
{
    /// <summary>
    /// Реализует упаковщик потоков на основе real-time алгоритма LZ4 и библиотеки LZ4s.
    /// Это один из самых быстрых и экономичных по памяти упаковщиков, особенно для текстовых данных.
    /// </summary>
    public sealed class LZ4StreamPacker : StreamPacker, IStreamPackerStats
    {
      private readonly byte[] unpackedBlock;
      private readonly byte[] packedBlock;
      private readonly ProgressReporter progressBar;
      private long lastPackInput;
      private long lastPackOutput;

      /// <summary>Создает объект.</summary>
      public LZ4StreamPacker()
      {
        this.unpackedBlock = new byte[64512];
        this.packedBlock = new byte[LZ4Codec.MaximumOutputLength(this.unpackedBlock.Length)];
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
        this.lastPackInput = 0L;
        this.lastPackOutput = 0L;
        for (int index = this.ReadBlockPartially(source, this.unpackedBlock); index > 0; index = this.ReadBlockPartially(source, this.unpackedBlock))
        {
          int num = LZ4Codec.Encode32(this.unpackedBlock, 0, index, this.packedBlock, 0, this.packedBlock.Length);
          this.WriteBlockLength(target, num);
          target.Write(this.packedBlock, 0, num);
          this.lastPackInput += (long) index;
          this.lastPackOutput += (long) num;
          this.progressBar.UpdateProgress((long) index);
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
        for (int index = this.TryReadBlockLength(source); index > 0; index = this.TryReadBlockLength(source))
        {
          this.ReadBlockExactly(source, this.packedBlock, index);
          int count = LZ4Codec.Decode32(this.packedBlock, 0, index, this.unpackedBlock, 0, this.unpackedBlock.Length, false);
          target.Write(this.unpackedBlock, 0, count);
          this.progressBar.UpdateProgress((long) index);
        }
        this.progressBar.Finish();
      }

      private void WriteBlockLength(Stream stream, int blockLength)
      {
        stream.WriteByte((byte) (blockLength & (int) byte.MaxValue));
        stream.WriteByte((byte) (blockLength >> 8 & (int) byte.MaxValue));
      }

      private int TryReadBlockLength(Stream stream)
      {
        int num = stream.ReadByte();
        return num == -1 ? 0 : stream.ReadByte() << 8 | num;
      }

      private int ReadBlockPartially(Stream stream, byte[] outputArray)
      {
        int length = outputArray.Length;
        return stream.Read(outputArray, 0, length);
      }

      private void ReadBlockExactly(Stream stream, byte[] outputArray, int readBytes)
      {
        if (readBytes == 0)
          return;
        int offset = 0;
        int count = readBytes;
        do
        {
          int num = stream.Read(outputArray, offset, count);
          if (num == 0)
            throw new Exception("Unable to read a full packed data block from the source stream. No more data.");
          offset += num;
          count -= num;
        }
        while (count > 0);
      }

      /// <summary>
      /// Реализует очистку внутренних структур упаковщика после завершения операций упаковки или распаковки.
      /// </summary>
      /// <param name="packMode">true - если выполнялась упаковка потока, false - если выполнялась распаковка</param>
      protected override void DoCleanup(bool packMode)
      {
        base.DoCleanup(packMode);
        this.progressBar.Reset();
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
