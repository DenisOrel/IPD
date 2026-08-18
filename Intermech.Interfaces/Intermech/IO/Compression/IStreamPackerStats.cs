
// Type: Intermech.IO.Compression.IStreamPackerStats
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.IO.Compression
{
    /// <summary>
    /// Дополнительный интерфейс упаковщика потоков, предоставляющий статистику эффективности упаковки данных.
    /// </summary>
    public interface IStreamPackerStats
    {
      /// <summary>
      /// Количество байт в исходном потоке в последней операции упаковки.
      /// </summary>
      long LastPackInput { get; }

      /// <summary>
      /// Количество байт в результирующем упакованном потоке в последней операции упаковки.
      /// </summary>
      long LastPackOutput { get; }
    }
}
