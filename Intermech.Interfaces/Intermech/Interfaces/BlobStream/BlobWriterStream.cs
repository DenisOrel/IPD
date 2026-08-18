
// Type: Intermech.Interfaces.BlobStream.BlobWriterStream
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System.IO;


namespace Intermech.Interfaces.BlobStream
{
    /// <summary>Поток дл я прямой записи в БД</summary>
    public class BlobWriterStream : Stream
    {
      private SessionKeeper sk;
      /// <summary>Пакующий поток</summary>
      private DeflaterOutputStream packStream;
      private BlobWriterStreamInternal blobWriterStreamInternal;
      private int curPosition;
      private long realFileSize;
      private long position;

      /// <summary>Конструктор</summary>
      /// <param name="aElementID">Идентификатор объекта</param>
      /// <param name="aAttributableElement">Вид элемента</param>
      /// <param name="aAttributeID">Идентификатор аттрибута</param>
      /// <param name="aIndex">Индекс файла в аттрибуте</param>
      /// <param name="dataBlockSize">Размеров блоков для записи в БД (0 по утмолчанию)</param>
      /// <param name="info">Информация о файле</param>
      /// <param name="uSession">Сессия</param>
      public BlobWriterStream(
        long aElementID,
        AttributableElements aAttributableElement,
        int aAttributeID,
        int aIndex,
        int dataBlockSize,
        BlobInformation info,
        IUserSession uSession)
      {
        if (uSession == null)
        {
          this.sk = new SessionKeeper();
          uSession = this.sk.Session;
        }
        this.blobWriterStreamInternal = new BlobWriterStreamInternal(aElementID, aAttributableElement, aAttributeID, aIndex, dataBlockSize, info, uSession);
        if (this.BlobInformation.ArcMethod != ArcMethods.ZLibPacked)
          return;
        this.packStream = new DeflaterOutputStream((Stream) this.blobWriterStreamInternal, new Deflater(5));
      }

      /// <summary>Конструктор</summary>
      /// <param name="attr">Аттрибут в который пишем</param>
      /// <param name="dataBlockSize">Размеров блоков для записи в БД (0 по утмолчанию)</param>
      /// <param name="info">Информация о файле</param>
      /// <param name="uSession">Сессия</param>
      public BlobWriterStream(
        IDBAttribute attr,
        int dataBlockSize,
        BlobInformation info,
        IUserSession uSession)
      {
        if (uSession == null)
        {
          this.sk = new SessionKeeper();
          uSession = this.sk.Session;
        }
        this.blobWriterStreamInternal = new BlobWriterStreamInternal(attr, dataBlockSize, info, uSession);
        if (this.BlobInformation.ArcMethod != ArcMethods.ZLibPacked)
          return;
        this.packStream = new DeflaterOutputStream((Stream) this.blobWriterStreamInternal, new Deflater(5));
      }

      /// <summary>Информация о записываемом файле</summary>
      public BlobInformation BlobInformation
      {
        get => this.blobWriterStreamInternal.BlobInformation;
        set => this.blobWriterStreamInternal.BlobInformation = value;
      }

      /// <summary>Записать изменения в БД и закрыть поток</summary>
      public void Commit()
      {
        if (this.blobWriterStreamInternal == null)
          return;
        this.blobWriterStreamInternal.Commit = true;
        this.Close();
      }

      /// <summary>
      /// Закрывает поток с отменой изменений, если ранее не был вызван Commit
      /// </summary>
      public override void Close()
      {
        if (this.blobWriterStreamInternal == null)
          return;
        this.blobWriterStreamInternal.RealFileSize = this.realFileSize;
        if (this.packStream != null)
        {
          this.packStream.Close();
          this.packStream = (DeflaterOutputStream) null;
        }
        this.blobWriterStreamInternal.Close();
        this.blobWriterStreamInternal = (BlobWriterStreamInternal) null;
        if (this.sk == null)
          return;
        this.sk.Dispose();
        this.sk = (SessionKeeper) null;
      }

      /// <summary>Идентификатор объекта</summary>
      public long ElementID => this.blobWriterStreamInternal.ElementID;

      /// <summary>Вид элемента</summary>
      public AttributableElements AttributableElement
      {
        get => this.blobWriterStreamInternal.AttributableElement;
      }

      /// <summary>Идентификатор аттрибута</summary>
      public int AttributeID => this.blobWriterStreamInternal.AttributeID;

      /// <summary>Индекс файла в аттрибуте</summary>
      public int Index => this.blobWriterStreamInternal.Index;

      /// <summary>Размер болоков данных для записи</summary>
      public int DataBlockSize => this.blobWriterStreamInternal.DataBlockSize;

      public override bool CanRead => false;

      public override bool CanSeek => false;

      public override bool CanWrite => true;

      public override long Length => this.realFileSize;

      public override long Position
      {
        get => this.position;
        set
        {
        }
      }

      public override void Flush()
      {
      }

      public override int Read(byte[] buffer, int offset, int count) => 0;

      public override long Seek(long offset, SeekOrigin origin) => 0;

      public override void SetLength(long value)
      {
      }

      public override void Write(byte[] buffer, int offset, int count)
      {
        this.realFileSize += (long) count;
        this.position += (long) count;
        if (this.BlobInformation.ArcMethod == ArcMethods.NotPacked)
          this.blobWriterStreamInternal.Write(buffer, offset, count);
        if (this.BlobInformation.ArcMethod != ArcMethods.ZLibPacked)
          return;
        this.packStream.Write(buffer, offset, count);
      }
    }
}
