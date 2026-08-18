
// Type: Intermech.Interfaces.BlobStream.BlobReaderStream
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.IO;


namespace Intermech.Interfaces.BlobStream
{
    /// <summary>Поток для прямого чтения из базы</summary>
    public class BlobReaderStream : Stream
    {
      private SessionKeeper sk;
      private InflaterInputStream packStream;
      private BlobReaderStreamInternal blobReaderStreamInternal;
      private long position;
      private byte[] readbuffer;
      private int firstBufPos;
      private int lastBufPos;

      /// <summary>Конструктор</summary>
      /// <param name="aElementID">Идентификатор объекта</param>
      /// <param name="aAttributableElement">Вид элемента</param>
      /// <param name="aAttributeID">Идентификатор аттрибута</param>
      /// <param name="aIndex">Индекс файла в аттрибуте</param>
      /// <param name="dataBlockSize">Размеров блоков для чтения из БД (0 по умолчанию)</param>
      /// <param name="uSession">Сессия</param>
      public BlobReaderStream(
        long aElementID,
        AttributableElements aAttributableElement,
        int aAttributeID,
        int aIndex,
        int dataBlockSize,
        IUserSession uSession)
      {
        if (uSession == null)
        {
          this.sk = new SessionKeeper();
          uSession = this.sk.Session;
        }
        this.blobReaderStreamInternal = new BlobReaderStreamInternal(aElementID, aAttributableElement, aAttributeID, aIndex, dataBlockSize, uSession);
        if (this.BlobInformation.ArcMethod == ArcMethods.ZLibPacked)
          this.packStream = new InflaterInputStream((Stream) this.blobReaderStreamInternal, new Inflater(), this.DataBlockSize);
        this.readbuffer = new byte[this.DataBlockSize];
      }

      /// <summary>Конструктор</summary>
      /// <param name="attr">Аттрибут в который пишем</param>
      /// <param name="dataBlockSize">Размеров блоков для чтения из БД (0 по умолчанию) </param>
      /// <param name="uSession">Сессия</param>
      public BlobReaderStream(IDBAttribute attr, int dataBlockSize, IUserSession uSession)
      {
        this.blobReaderStreamInternal = new BlobReaderStreamInternal(attr, dataBlockSize, uSession);
        if (this.BlobInformation.ArcMethod == ArcMethods.ZLibPacked)
          this.packStream = new InflaterInputStream((Stream) this.blobReaderStreamInternal, new Inflater(), this.DataBlockSize);
        this.readbuffer = new byte[this.DataBlockSize];
      }

      public BlobInformation BlobInformation
      {
        get => this.blobReaderStreamInternal.BlobInformation;
        set => this.blobReaderStreamInternal.BlobInformation = value;
      }

      public override void Close()
      {
        if (this.blobReaderStreamInternal == null)
          return;
        this.blobReaderStreamInternal.Close();
        this.blobReaderStreamInternal = (BlobReaderStreamInternal) null;
        this.readbuffer = (byte[]) null;
        if (this.packStream != null)
        {
          this.packStream.Close();
          this.packStream = (InflaterInputStream) null;
        }
        if (this.sk == null)
          return;
        this.sk.Dispose();
        this.sk = (SessionKeeper) null;
      }

      public long ElementID => this.blobReaderStreamInternal.ElementID;

      public AttributableElements AttributableElement
      {
        get => this.blobReaderStreamInternal.AttributableElement;
      }

      public int AttributeID => this.blobReaderStreamInternal.AttributeID;

      public int Index => this.blobReaderStreamInternal.Index;

      public int DataBlockSize => this.blobReaderStreamInternal.DataBlockSize;

      public override bool CanRead => this.blobReaderStreamInternal != null;

      public override bool CanSeek => this.blobReaderStreamInternal != null;

      public override bool CanWrite => false;

      public override long Length => this.BlobInformation.RealFileSize;

      public override long Position
      {
        get => this.position;
        set
        {
          this.blobReaderStreamInternal.Position = value;
          if (value != 0L)
            return;
          this.position = 0L;
          this.packStream = new InflaterInputStream((Stream) this.blobReaderStreamInternal, new Inflater(), this.DataBlockSize);
          this.firstBufPos = 0;
          this.lastBufPos = 0;
        }
      }

      public override void Flush()
      {
      }

      public override int Read(byte[] buffer, int offset, int count)
      {
        Stream stream = (Stream) null;
        if (this.BlobInformation.ArcMethod == ArcMethods.NotPacked)
          stream = (Stream) this.blobReaderStreamInternal;
        if (this.BlobInformation.ArcMethod == ArcMethods.ZLibPacked)
          stream = (Stream) this.packStream;
        if (stream == null)
          return 0;
        int num = 0;
        while (this.lastBufPos > 0 && count > 0)
        {
          int count1 = this.lastBufPos - this.firstBufPos;
          if (count >= count1)
          {
            Buffer.BlockCopy((Array) this.readbuffer, this.firstBufPos, (Array) buffer, offset, count1);
            this.lastBufPos = 0;
            this.firstBufPos = 0;
            offset += count1;
            num += count1;
            count -= count1;
          }
          else if (count < count1)
          {
            Buffer.BlockCopy((Array) this.readbuffer, this.firstBufPos, (Array) buffer, offset, count);
            num += count;
            this.firstBufPos += count;
            count -= count;
            break;
          }
        }
        while (count > 0)
        {
          int count2 = stream.Read(this.readbuffer, 0, this.DataBlockSize);
          if (count2 > 0)
          {
            if (count >= count2)
            {
              Buffer.BlockCopy((Array) this.readbuffer, 0, (Array) buffer, offset, count2);
              offset += count2;
              num += count2;
              count -= count2;
            }
            else
            {
              Buffer.BlockCopy((Array) this.readbuffer, 0, (Array) buffer, offset, count);
              num += count;
              this.firstBufPos = count;
              this.lastBufPos = count2;
              count -= count;
            }
          }
          else
          {
            this.position += (long) num;
            return num;
          }
        }
        this.position += (long) num;
        return num;
      }

      public override long Seek(long offset, SeekOrigin origin)
      {
        if (origin != SeekOrigin.Begin)
          throw new ArgumentException("origin может быть только от старта потока");
        this.Position = offset;
        return 0;
      }

      public override void SetLength(long value)
      {
      }

      public override void Write(byte[] buffer, int offset, int count)
      {
      }

      /// <summary>
      /// Класс который хранит информацию считанную из базы но еще не затребованную
      /// </summary>
      private class DataChunk
      {
        private byte[] buffer;

        public DataChunk(byte[] buffer) => this.buffer = buffer;

        public byte[] Buffer
        {
          get => this.buffer;
          set => this.buffer = value;
        }
      }
    }
}
