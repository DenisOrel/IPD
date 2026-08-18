
// Type: Intermech.IO.Compression.StreamPacker
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.IO;


namespace Intermech.IO.Compression
{
    /// <summary>
    /// Базовый класс для упаковщиков, позволяющих за одну операцию запаковать или распаковать целый поток (System.IO.Stream).
    /// </summary>
    public abstract class StreamPacker
    {
      /// <summary>Выполняет упаковку потока.</summary>
      /// <param name="source">Исходный неупакованный поток</param>
      /// <param name="target">Результирующий упакованный поток</param>
      /// <param name="progressHandler">Обработчик прогресса упаковки. Может быть не указан</param>
      /// <exception cref="T:System.ArgumentNullException">Не указан исходный или результирующий поток</exception>
      public void Pack(Stream source, Stream target, PercentEventHandler progressHandler = null)
      {
        if (source == null)
          throw new ArgumentNullException(nameof (source));
        if (target == null)
          throw new ArgumentNullException(nameof (target));
        try
        {
          this.DoPack(source, target, progressHandler);
          target.Flush();
        }
        finally
        {
          this.DoCleanup(true);
        }
      }

      /// <summary>Выполняет распаковку потока.</summary>
      /// <param name="source">Исходный упакованный поток</param>
      /// <param name="target">Результирующий распакованный поток</param>
      /// <param name="progressHandler">Обработчик прогресса распаковки. Может быть не указан</param>
      /// <exception cref="T:System.ArgumentNullException">Не указан исходный или результирующий поток</exception>
      public void Unpack(Stream source, Stream target, PercentEventHandler progressHandler = null)
      {
        if (source == null)
          throw new ArgumentNullException(nameof (source));
        if (target == null)
          throw new ArgumentNullException(nameof (target));
        try
        {
          this.DoUnpack(source, target, progressHandler);
          target.Flush();
        }
        finally
        {
          this.DoCleanup(false);
        }
      }

      /// <summary>Реализует упаковку потока.</summary>
      /// <param name="source">Исходный неупакованный поток</param>
      /// <param name="target">Результирующий упакованный поток</param>
      /// <param name="progressHandler">Обработчик прогресса упаковки. Может быть не указан</param>
      protected abstract void DoPack(Stream source, Stream target, PercentEventHandler progressHandler);

      /// <summary>Реализует распаковку потока.</summary>
      /// <param name="source">Исходный упакованный поток</param>
      /// <param name="target">Результирующий распакованный поток</param>
      /// <param name="progressHandler">Обработчик прогресса распаковки. Может быть не указан</param>
      protected abstract void DoUnpack(
        Stream source,
        Stream target,
        PercentEventHandler progressHandler);

      /// <summary>
      /// Реализует очистку внутренних структур упаковщика после завершения операций упаковки или распаковки.
      /// </summary>
      /// <param name="packMode">true - если выполнялась упаковка потока, false - если выполнялась распаковка</param>
      protected virtual void DoCleanup(bool packMode)
      {
      }
    }
}
