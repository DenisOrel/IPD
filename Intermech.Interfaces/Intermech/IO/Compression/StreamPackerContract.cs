
// Type: Intermech.IO.Compression.StreamPackerContract
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.IO.Compression
{
    internal static class StreamPackerContract
    {
      internal static void CheckCompressionLevel(int compressionLevel)
      {
        if (compressionLevel < 0 || compressionLevel > 9)
          throw new ArgumentOutOfRangeException(nameof (compressionLevel));
      }

      internal static void CheckBufferSize(int bufferSize)
      {
        if (bufferSize < 4096 /*0x1000*/ || bufferSize > 1048576 /*0x100000*/)
          throw new ArgumentOutOfRangeException(nameof (bufferSize));
      }

      internal static void CheckBufferSize(int bufferSize, int maxSize)
      {
        if (bufferSize < 4096 /*0x1000*/ || bufferSize > maxSize)
          throw new ArgumentOutOfRangeException(nameof (bufferSize));
      }
    }
}
