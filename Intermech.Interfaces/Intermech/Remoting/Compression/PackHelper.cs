
// Type: Intermech.Remoting.Compression.PackHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.IO;
using Intermech.IO.Compression;
using Intermech.Pools;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;


namespace Intermech.Remoting.Compression
{
    internal static class PackHelper
    {
      private static readonly ConcurrentBagPool<StreamPacker> packerPool = new ConcurrentBagPool<StreamPacker>(4, new Func<StreamPacker>(PackHelper.CreateFastPacker));
      private static long packInput;
      private static long packOutput;

      public static Stream PackStream(Stream inStream)
      {
        if (inStream == null)
          throw new ArgumentNullException(nameof (inStream));
        StreamPacker streamPacker = (StreamPacker) null;
        Stream target = (Stream) null;
        try
        {
          streamPacker = PackHelper.packerPool.Allocate();
          target = (Stream) new ImChunkedStream();
          streamPacker.Pack(inStream, target);
          target.Seek(0L, SeekOrigin.Begin);
          return target;
        }
        catch
        {
          target?.Dispose();
          throw;
        }
        finally
        {
          if (streamPacker != null)
            PackHelper.packerPool.Release(streamPacker);
        }
      }

      public static Stream UnpackStream(Stream inStream)
      {
        if (inStream == null)
          throw new ArgumentNullException(nameof (inStream));
        StreamPacker streamPacker = (StreamPacker) null;
        Stream target = (Stream) null;
        try
        {
          streamPacker = PackHelper.packerPool.Allocate();
          target = (Stream) new ImChunkedStream();
          streamPacker.Unpack(inStream, target);
          target.Seek(0L, SeekOrigin.Begin);
          return target;
        }
        catch
        {
          target?.Dispose();
          throw;
        }
        finally
        {
          if (streamPacker != null)
            PackHelper.packerPool.Release(streamPacker);
        }
      }

      [Conditional("DEBUG")]
      private static void UpdateStats(StreamPacker packer)
      {
        if (!(packer is IStreamPackerStats streamPackerStats))
          return;
        Interlocked.Add(ref PackHelper.packInput, streamPackerStats.LastPackInput);
        Interlocked.Add(ref PackHelper.packOutput, streamPackerStats.LastPackOutput);
      }

      private static StreamPacker CreateFastPacker() => (StreamPacker) new LZ4StreamPacker();
    }
}
