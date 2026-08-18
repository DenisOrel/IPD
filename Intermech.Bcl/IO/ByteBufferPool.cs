
// Type: Intermech.IO.ByteBufferPool
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Threading;


namespace Intermech.IO
{
    /// <summary>Buffer's pool</summary>
    [Obsolete("Use the ImChunkedStreamConcurrentBufferPool class instead of this", true)]
    internal sealed class ByteBufferPool : IByteBufferPool
    {
      /// <summary>Buffer's size</summary>
      protected int _bufferSize;
      /// <summary>Buffer's data pool</summary>
      protected byte[][] _bufferPool;
      /// <summary>
      /// 
      /// </summary>
      protected object _controlCookie = (object) "cookies object";
      /// <summary>Current buffer's index</summary>
      protected int _current;
      /// <summary>Last buffer's index</summary>
      protected int _last;
      /// <summary>Max buffer's index</summary>
      protected int _max;

      /// <summary>Constructor</summary>
      /// <param name="maxBuffers">Max buffer's count</param>
      /// <param name="bufferSize">Buffer's size</param>
      public ByteBufferPool(int maxBuffers, int bufferSize)
      {
        this._max = maxBuffers;
        this._bufferPool = new byte[this._max][];
        this._bufferSize = bufferSize;
        this._current = -1;
        this._last = -1;
      }

      /// <summary>Support buffer pool's cache</summary>
      public bool SupportCache => true;

      /// <summary>Get buffer length</summary>
      public int BufferLength => this._bufferSize;

      /// <summary>Get buffer from pool</summary>
      /// <returns></returns>
      public byte[] GetBuffer()
      {
        object obj = (object) null;
        try
        {
          obj = Interlocked.Exchange(ref this._controlCookie, (object) null);
          if (obj == null)
            return new byte[this._bufferSize];
          if (this._current == -1)
          {
            this._controlCookie = obj;
            return new byte[this._bufferSize];
          }
          byte[] buffer = this._bufferPool[this._current];
          this._bufferPool[this._current] = (byte[]) null;
          this._current = this._current != this._last ? (this._current + 1) % this._max : -1;
          this._controlCookie = obj;
          return buffer;
        }
        catch (ThreadAbortException ex)
        {
          if (obj != null)
          {
            this._current = -1;
            this._last = -1;
            this._controlCookie = obj;
          }
          throw;
        }
      }

      /// <summary>Return buffer to pool</summary>
      /// <param name="buffer"></param>
      public void ReturnBuffer(byte[] buffer)
      {
        if (buffer == null)
          throw new ArgumentNullException(nameof (buffer));
        object obj = (object) null;
        try
        {
          obj = Interlocked.Exchange(ref this._controlCookie, (object) null);
          if (obj == null)
            return;
          if (this._current == -1)
          {
            this._bufferPool[0] = buffer;
            this._current = 0;
            this._last = 0;
          }
          else
          {
            int num = (this._last + 1) % this._max;
            if (num != this._current)
            {
              this._last = num;
              this._bufferPool[this._last] = buffer;
            }
          }
          this._controlCookie = obj;
        }
        catch (ThreadAbortException ex)
        {
          if (obj != null)
          {
            this._current = -1;
            this._last = -1;
            this._controlCookie = obj;
          }
          throw;
        }
      }
    }
}
