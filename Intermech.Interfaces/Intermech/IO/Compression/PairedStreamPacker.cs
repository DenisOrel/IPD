
// Type: Intermech.IO.Compression.PairedStreamPacker
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.IO;


namespace Intermech.IO.Compression
{
    /// <summary>
    /// Реализует упаковщик потоков, собранный из двух других упаковщиков. Первый используется для упаковки данных, а второй - для распаковки.
    /// Оба используемых упаковщика должны понимать формат данных друг друга.
    /// </summary>
    public sealed class PairedStreamPacker : StreamPacker
    {
      private readonly StreamPacker packer;
      private readonly StreamPacker unpacker;

      /// <summary>Создает объект.</summary>
      /// <param name="packer">Объект для упаковки данных</param>
      /// <param name="unpacker">Объект для распаковки данных</param>
      public PairedStreamPacker(StreamPacker packer, StreamPacker unpacker)
      {
        if (packer == null)
          throw new ArgumentNullException(nameof (packer));
        if (unpacker == null)
          throw new ArgumentNullException(nameof (unpacker));
        this.packer = packer;
        this.unpacker = unpacker;
      }

      /// <summary>Реализует упаковку потока.</summary>
      /// <param name="source">Исходный неупакованный поток</param>
      /// <param name="target">Результирующий упакованный поток</param>
      /// <param name="progressHandler">Обработчик прогресса упаковки. Может быть не указан</param>
      protected override void DoPack(Stream source, Stream target, PercentEventHandler progressHandler)
      {
        this.packer.Pack(source, target, progressHandler);
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
        this.unpacker.Unpack(source, target, progressHandler);
      }
    }
}
