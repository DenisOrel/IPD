
// Type: Intermech.IO.ByteBufferAllocator
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.IO
{
    /// <summary>Simple buffer's allocator (no cached buffers)</summary>
    [Obsolete("Use the ImChunkedStreamConcurrentBufferPool class instead of this", true)]
    internal sealed class ByteBufferAllocator : IByteBufferPool
    {
      /// <summary>Buffer's size</summary>
      private readonly int _bufferSize;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="bufferSize"></param>
      public ByteBufferAllocator(int bufferSize) => this._bufferSize = bufferSize;

      /// <summary>Support buffer pool's cache</summary>
      public bool SupportCache => false;

      /// <summary>Get buffer length</summary>
      public int BufferLength => this._bufferSize;

      /// <summary>Get buffer from pool</summary>
      /// <returns></returns>
      public byte[] GetBuffer() => new byte[this._bufferSize];

      /// <summary>Return</summary>
      /// <param name="buffer"></param>
      public void ReturnBuffer(byte[] buffer)
      {
      }
    }
}
