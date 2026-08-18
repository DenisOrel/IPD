
// Type: Intermech.IO.ImChunkedStreamConcurrentBufferPool
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Pools;
using System;


namespace Intermech.IO
{
    public sealed class ImChunkedStreamConcurrentBufferPool : IByteBufferPool
    {
      private readonly int _bufferLength;
      private readonly ConcurrentBagPool<byte[]> _internalPool;

      public ImChunkedStreamConcurrentBufferPool(int bufferLength)
      {
        this._bufferLength = bufferLength > 0 ? bufferLength : throw new ArgumentOutOfRangeException(nameof (bufferLength));
        this._internalPool = new ConcurrentBagPool<byte[]>(8, new Func<byte[]>(this.CreateBuffer));
      }

      private byte[] CreateBuffer() => new byte[this._bufferLength];

      public bool SupportCache => true;

      public int BufferLength => this._bufferLength;

      public byte[] GetBuffer() => this._internalPool.Allocate();

      public void ReturnBuffer(byte[] buffer) => this._internalPool.Release(buffer);
    }
}
