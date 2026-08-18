
// Type: Intermech.IO.ImChunkedStream
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.IO;


namespace Intermech.IO
{
    /// <summary>
    /// Поток, который содержит данные в виде списка буферов (фрагментов).
    /// </summary>
    /// <remarks>
    /// Позволяет "более экономично" хранить данные в памяти, уменьшая фрагментацию LargeObjectHeap.
    /// Рекомендуется к использованию вместо MemoryStream.
    /// ВНИМАНИЕ!
    /// Для случаев, когда уже есть целиком буфер с данными и поток используется только на чтение,
    /// следует использовать класс MemoryStream и конструктор вида new MemoryStream(buffer), для избежания
    /// повторного выделения памяти и копирования данных.
    /// </remarks>
    public class ImChunkedStream : Stream
    {
      /// <summary>Stream closed status</summary>
      private bool _isClosed;
      /// <summary>Chunk's size</summary>
      private readonly int _chunkSize;
      /// <summary>Chunk's data</summary>
      private List<DataChunk> _chunks;
      /// <summary>Pool of byte buffers to use</summary>
      private IByteBufferPool _bufferPool;
      /// <summary>offset into chunk to write to</summary>
      private int _lastWriteOffset;
      /// <summary>
      /// 
      /// </summary>
      private DataChunk _currentChunk;
      /// <summary>current chunk to read from, write to</summary>
      private int _currentChunkIdx = -1;
      /// <summary>offset into chunk to read from, write to</summary>
      private int _currentOffset;
      /// <summary>Declare static buffer's pool</summary>
      private static readonly IByteBufferPool SharedBufferPool = (IByteBufferPool) new ImChunkedStreamConcurrentBufferPool(16384 /*0x4000*/);

      /// <summary>Allocate memory chunk</summary>
      /// <returns></returns>
      private DataChunk AllocateMemoryChunk()
      {
        return new DataChunk()
        {
          Buffer = this._bufferPool.GetBuffer(),
          Next = (DataChunk) null
        };
      }

      /// <summary>Release memory chunk</summary>
      /// <param name="chunk"></param>
      private void ReleaseMemoryChunk(DataChunk chunk)
      {
        if (this._bufferPool == null || !this._bufferPool.SupportCache)
          return;
        this._bufferPool.ReturnBuffer(chunk.Buffer);
      }

      /// <summary>Release all memory chunks</summary>
      private void ReleaseMemoryChunks()
      {
        if (this._chunks == null || this._bufferPool == null || !this._bufferPool.SupportCache)
          return;
        foreach (DataChunk chunk in this._chunks)
          this._bufferPool.ReturnBuffer(chunk.Buffer);
      }

      /// <summary>Проверка наличия буфера при записи</summary>
      protected virtual void CheckWriteChunkData()
      {
        if (this._chunks != null)
        {
          if (this._currentChunk != null)
            return;
          this._currentChunk = this._chunks[0];
          this._currentChunkIdx = 0;
          this._currentOffset = 0;
        }
        else
        {
          this._chunks = new List<DataChunk>(128 /*0x80*/);
          this._currentChunk = this.AllocateMemoryChunk();
          this._chunks.Add(this._currentChunk);
          this._currentChunkIdx = 0;
          this._currentOffset = 0;
          this._lastWriteOffset = 0;
        }
      }

      /// <summary>Проверка наличие буфера при чтении</summary>
      /// <returns></returns>
      protected virtual bool CheckReadChunkData()
      {
        if (this._currentChunk == null)
        {
          if (this._chunks == null)
            return false;
          this._currentChunk = this._chunks[0];
          this._currentChunkIdx = 0;
          this._currentOffset = 0;
        }
        return true;
      }

      /// <summary>
      /// ВНИМАНИЕ!
      /// Для случаев, когда уже есть целиком буфер с данными и поток используется только на чтение,
      /// следует использовать new MemoryStream(bufferData) !!
      /// </summary>
      public ImChunkedStream()
        : this(ImChunkedStream.SharedBufferPool)
      {
      }

      public ImChunkedStream(IByteBufferPool bufferPool)
      {
        this._bufferPool = bufferPool ?? throw new ArgumentNullException(nameof (bufferPool));
        this._chunkSize = this._bufferPool.BufferLength;
        this._chunks = (List<DataChunk>) null;
      }

      /// <summary>copy entire buffer into an array</summary>
      /// <returns></returns>
      public virtual byte[] ToArray()
      {
        int length = (int) this.Length;
        byte[] buffer = new byte[length];
            DataChunk currentChunk = this._currentChunk;
        int currentChunkIdx = this._currentChunkIdx;
        int currentOffset = this._currentOffset;
        try
        {
          this._currentChunk = (DataChunk) null;
          this._currentChunkIdx = -1;
          this._currentOffset = 0;
          this.Read(buffer, 0, length);
        }
        finally
        {
          this._currentChunk = currentChunk;
          this._currentChunkIdx = currentChunkIdx;
          this._currentOffset = currentOffset;
        }
        return buffer;
      }

      /// <summary>Write remainder of this stream to another stream</summary>
      /// <param name="stream"></param>
      public virtual void WriteTo(Stream stream)
      {
        if (this._isClosed)
          throw new IOException("Stream Is Closed");
        if (stream == null)
          throw new ArgumentNullException(nameof (stream));
        if (this._currentChunk == null)
        {
          if (this._chunks == null)
            return;
          this._currentChunk = this._chunks[0];
          this._currentChunkIdx = 0;
          this._currentOffset = 0;
        }
        byte[] buffer = this._currentChunk.Buffer;
        int num = this._chunkSize;
        if (this._currentChunk.Next == null)
          num = this._lastWriteOffset;
        while (true)
        {
          if (this._currentOffset == num)
          {
            if (this._currentChunk.Next != null)
            {
              this._currentChunk = this._currentChunk.Next;
              ++this._currentChunkIdx;
              this._currentOffset = 0;
              buffer = this._currentChunk.Buffer;
              num = this._chunkSize;
              if (this._currentChunk.Next == null)
                num = this._lastWriteOffset;
            }
            else
              break;
          }
          int count = num - this._currentOffset;
          stream.Write(buffer, this._currentOffset, count);
          this._currentOffset = num;
        }
      }

      /// <summary>Get or set current position</summary>
      public override long Position
      {
        get
        {
          if (this._isClosed)
            throw new IOException("Stream Is Closed");
          return this._currentChunk == null ? 0L : (long) (this._currentChunkIdx * this._chunkSize + this._currentOffset);
        }
        set
        {
          if (this._isClosed)
            throw new IOException("Stream Is Closed");
          if (value < 0L)
            throw new ArgumentOutOfRangeException(nameof (value));
          if (value == 0L)
          {
            this._currentChunk = (DataChunk) null;
            this._currentChunkIdx = -1;
            this._currentOffset = 0;
          }
          else
          {
            if (this._chunks == null || value > (long) (this._chunkSize * this._chunks.Count))
              throw new ArgumentOutOfRangeException(nameof (value));
            this._currentChunkIdx = (int) value / this._chunkSize;
            this._currentOffset = (int) value % this._chunkSize;
            if (this._currentOffset == 0 && this._currentChunkIdx > 0 && this._currentChunkIdx == this._chunks.Count)
            {
              --this._currentChunkIdx;
              this._currentOffset = this._chunkSize;
            }
            this._currentChunk = this._chunks[this._currentChunkIdx];
          }
        }
      }

      public override void SetLength(long value)
      {
        if (this._isClosed)
          throw new IOException("Stream Is Closed");
        if (value < 0L)
          throw new ArgumentOutOfRangeException(nameof (value));
        if (value == 0L)
        {
          this.ReleaseMemoryChunks();
          if (this._chunks != null)
          {
            this._chunks.Clear();
            this._chunks = (List<DataChunk>) null;
          }
          this._currentChunk = (DataChunk) null;
          this._currentChunkIdx = -1;
          this._currentOffset = 0;
          this._lastWriteOffset = 0;
        }
        else
        {
          this.CheckWriteChunkData();
          int index1 = (int) value / this._chunkSize;
          this._lastWriteOffset = (int) value % this._chunkSize;
          for (int index2 = this._chunks.Count - 1; index2 > index1; --index2)
          {
            this.ReleaseMemoryChunk(this._chunks[index2]);
            this._chunks.RemoveAt(index2);
          }
          for (int count = this._chunks.Count; count <= index1; ++count)
          {
                    DataChunk dataChunk = this.AllocateMemoryChunk();
            this._chunks.Add(dataChunk);
            if (count > 0)
              this._chunks[count - 1].Next = dataChunk;
          }
          this._chunks[index1].Next = (DataChunk) null;
          if (this._currentChunkIdx <= index1 && (this._currentChunkIdx != index1 || this._currentOffset <= this._lastWriteOffset))
            return;
          this._currentChunkIdx = index1;
          this._currentOffset = this._lastWriteOffset;
          this._currentChunk = this._chunks[this._currentChunkIdx];
        }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="disposing"></param>
      protected override void Dispose(bool disposing)
      {
        try
        {
          this._isClosed = true;
          if (disposing)
            this.ReleaseMemoryChunks();
          if (this._chunks != null)
          {
            this._chunks.Clear();
            this._chunks = (List<DataChunk>) null;
          }
          this._currentChunk = (DataChunk) null;
          this._currentChunkIdx = -1;
          this._bufferPool = (IByteBufferPool) null;
        }
        finally
        {
          base.Dispose(disposing);
        }
      }

      /// <summary>Length of stream's data</summary>
      public override long Length
      {
        get
        {
          if (this._isClosed)
            throw new IOException("Stream Is Closed");
          long length = 0;
          if (this._chunks != null)
            length = (long) ((this._chunks.Count - 1) * this._chunkSize + this._lastWriteOffset);
          return length;
        }
      }

      /// <summary>
      /// 
      /// </summary>
      public override bool CanRead => true;

      /// <summary>
      /// 
      /// </summary>
      public override bool CanSeek => true;

      /// <summary>
      /// 
      /// </summary>
      public override bool CanWrite => true;

      /// <summary>
      /// 
      /// </summary>
      public override void Flush()
      {
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="offset"></param>
      /// <param name="origin"></param>
      /// <returns></returns>
      public override long Seek(long offset, SeekOrigin origin)
      {
        if (this._isClosed)
          throw new IOException("Stream Is Closed");
        switch (origin)
        {
          case SeekOrigin.Begin:
            this.Position = offset;
            break;
          case SeekOrigin.Current:
            this.Position += offset;
            break;
          case SeekOrigin.End:
            this.Position = this.Length + offset;
            break;
        }
        return this.Position;
      }

      /// <summary>Read data</summary>
      /// <param name="buffer"></param>
      /// <param name="offset"></param>
      /// <param name="count"></param>
      /// <returns></returns>
      public override int Read(byte[] buffer, int offset, int count)
      {
        if (this._isClosed)
          throw new IOException("Stream Is Closed");
        if (!this.CheckReadChunkData())
          return 0;
        byte[] buffer1 = this._currentChunk.Buffer;
        int num1 = this._chunkSize;
        if (this._currentChunk.Next == null)
          num1 = this._lastWriteOffset;
        int num2 = 0;
        while (count > 0)
        {
          if (this._currentOffset == num1)
          {
            if (this._currentChunk.Next != null)
            {
              this._currentChunk = this._currentChunk.Next;
              ++this._currentChunkIdx;
              this._currentOffset = 0;
              buffer1 = this._currentChunk.Buffer;
              num1 = this._chunkSize;
              if (this._currentChunk.Next == null)
                num1 = this._lastWriteOffset;
            }
            else
              break;
          }
          int count1 = count;
          if (count1 > num1 - this._currentOffset)
            count1 = num1 - this._currentOffset;
          Buffer.BlockCopy((Array) buffer1, this._currentOffset, (Array) buffer, offset, count1);
          offset += count1;
          count -= count1;
          this._currentOffset += count1;
          num2 += count1;
        }
        return num2;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public override int ReadByte()
      {
        if (this._isClosed)
          throw new IOException("Stream Is Closed");
        if (!this.CheckReadChunkData())
          return 0;
        byte[] buffer = this._currentChunk.Buffer;
        int num = this._chunkSize;
        if (this._currentChunk.Next == null)
          num = this._lastWriteOffset;
        if (this._currentOffset == num)
        {
          if (this._currentChunk.Next == null)
            return -1;
          this._currentChunk = this._currentChunk.Next;
          ++this._currentChunkIdx;
          this._currentOffset = 0;
          buffer = this._currentChunk.Buffer;
        }
        return (int) buffer[this._currentOffset++];
      }

      /// <summary>Write data</summary>
      /// <param name="buffer"></param>
      /// <param name="offset"></param>
      /// <param name="count"></param>
      public override void Write(byte[] buffer, int offset, int count)
      {
        if (this._isClosed)
          throw new IOException("Stream Is Closed");
        this.CheckWriteChunkData();
        byte[] buffer1 = this._currentChunk.Buffer;
        int chunkSize = this._chunkSize;
        while (count > 0)
        {
          if (this._currentOffset == chunkSize)
          {
            if (this._currentChunk.Next == null)
            {
              this._currentChunk.Next = this.AllocateMemoryChunk();
              this._chunks.Add(this._currentChunk.Next);
              this._lastWriteOffset = 0;
            }
            this._currentChunk = this._currentChunk.Next;
            buffer1 = this._currentChunk.Buffer;
            chunkSize = this._chunkSize;
            ++this._currentChunkIdx;
            this._currentOffset = 0;
          }
          int count1 = count;
          if (count1 > chunkSize - this._currentOffset)
            count1 = chunkSize - this._currentOffset;
          Buffer.BlockCopy((Array) buffer, offset, (Array) buffer1, this._currentOffset, count1);
          offset += count1;
          count -= count1;
          this._currentOffset += count1;
          if (this._currentChunk.Next == null && this._lastWriteOffset < this._currentOffset)
            this._lastWriteOffset = this._currentOffset;
        }
      }

      /// <summary>Write data</summary>
      /// <param name="value"></param>
      public override void WriteByte(byte value)
      {
        if (this._isClosed)
          throw new IOException("Stream Is Closed");
        this.CheckWriteChunkData();
        byte[] buffer = this._currentChunk.Buffer;
        if (this._currentOffset == this._chunkSize)
        {
          if (this._currentChunk.Next == null)
          {
            this._currentChunk.Next = this.AllocateMemoryChunk();
            this._chunks.Add(this._currentChunk.Next);
            this._lastWriteOffset = 0;
          }
          this._currentChunk = this._currentChunk.Next;
          buffer = this._currentChunk.Buffer;
          ++this._currentChunkIdx;
          this._currentOffset = 0;
        }
        buffer[this._currentOffset++] = value;
        if (this._currentChunk.Next != null || this._currentOffset <= this._lastWriteOffset)
          return;
        this._lastWriteOffset = this._currentOffset;
      }

      /// <summary>Const</summary>
      public class Consts
      {
        /// <summary>Default chunk's size</summary>
        public const int DefChunkSize = 16384 /*0x4000*/;
        /// <summary>Default buffer's size</summary>
        public const int DefMaxBuffers = 10;
        /// <summary>Default chunk list's size</summary>
        public const int DefChunkListSize = 128 /*0x80*/;
      }

      /// <summary>Data chunk item</summary>
      protected internal class DataChunk
      {
        /// <summary>Data buffer</summary>
        public byte[] Buffer;
        /// <summary>Reference to next chunk (for speed up)</summary>
        public DataChunk Next;
      }
    }
}
