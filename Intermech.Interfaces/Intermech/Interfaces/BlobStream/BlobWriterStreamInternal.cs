
// Type: Intermech.Interfaces.BlobStream.BlobWriterStreamInternal
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Remoting.Sponsors;
using System;
using System.IO;


namespace Intermech.Interfaces.BlobStream
{
    /// <summary>Внутренний поток для записи в БД</summary>
    internal class BlobWriterStreamInternal : Stream
    {
      internal int dataBlockSize = Consts.DefaultBlobBlockSize;
      internal long fileSize;
      internal BlobInformation blobInformation;
      private IBlobWriterEx writer;
      private bool commit;
      private RemoteLock remoteLock;
      internal IUserSession session;
      internal IDBAttribute lIDBAttribute;
      internal long elementID;
      internal AttributableElements attributableElement;
      internal int attributeID;
      internal int index;
      private long realFileSize;
      private byte[] buf;
      private int curPosition;

      public BlobWriterStreamInternal(
        long aElementID,
        AttributableElements aAttributableElement,
        int aAttributeID,
        int aIndex,
        int dataBlockSize,
        BlobInformation info,
        IUserSession uSession)
      {
        this.lIDBAttribute = (IDBAttribute) null;
        this.session = uSession;
        this.elementID = aElementID;
        this.attributableElement = aAttributableElement;
        this.attributeID = aAttributeID;
        this.index = aIndex;
        if (dataBlockSize > 0)
          this.dataBlockSize = dataBlockSize;
        this.blobInformation = info;
        this.writer = (this.lIDBAttribute == null ? this.GetAttributeInterface(this.elementID, this.attributableElement, this.attributeID, this.index, this.session) : this.lIDBAttribute) as IBlobWriterEx;
        if (this.writer == null)
          throw new Exception("Данный атрибут не поддерживает потоковую запись");
        this.writer.OpenBlob(this.blobInformation, false, false);
        this.buf = new byte[this.DataBlockSize];
        this.remoteLock = new RemoteLock();
        this.remoteLock.Add((object) this.writer);
      }

      /// <summary>Конструктор</summary>
      /// <param name="attr">Аттрибут в который пишем</param>
      /// <param name="dataBlockSize">Размеров блоков для чтения из БД</param>
      /// <param name="info">Информация о файле</param>
      /// <param name="uSession">Сессия</param>
      public BlobWriterStreamInternal(
        IDBAttribute attr,
        int dataBlockSize,
        BlobInformation info,
        IUserSession uSession)
      {
        this.lIDBAttribute = attr;
        this.session = uSession;
        if (dataBlockSize > 0)
          this.dataBlockSize = dataBlockSize;
        this.blobInformation = info;
        this.writer = this.lIDBAttribute as IBlobWriterEx;
        if (this.writer == null)
          throw new Exception("Данный атрибут не поддерживает потоковую запись");
        this.writer.OpenBlob(this.blobInformation, false, false);
        this.buf = new byte[this.DataBlockSize];
        this.remoteLock = new RemoteLock();
        this.remoteLock.Add((object) this.writer);
      }

      /// <summary>Размер читаемых блоков</summary>
      public int DataBlockSize => this.dataBlockSize;

      public BlobInformation BlobInformation
      {
        get => this.blobInformation;
        set => this.blobInformation = value;
      }

      /// <summary>Получение аттрибута</summary>
      /// <param name="aElementID"></param>
      /// <param name="aAttributableElement"></param>
      /// <param name="aAttributeID"></param>
      /// <param name="aIndex"></param>
      /// <param name="iSession"></param>
      /// <returns></returns>
      protected IDBAttribute GetAttributeInterface(
        long aElementID,
        AttributableElements aAttributableElement,
        int aAttributeID,
        int aIndex,
        IUserSession iSession)
      {
        IDBAttribute attributeInterface = (IDBAttribute) null;
        switch (aAttributableElement)
        {
          case AttributableElements.Object:
            IDBObject dbObject = iSession.GetObject(aElementID);
            if (dbObject == null)
              AbortException.Abort("Объект не найден");
            attributeInterface = dbObject.GetAttributeByID(aAttributeID) ?? dbObject.Attributes.AddAttribute(aAttributeID, false);
            break;
          case AttributableElements.Relation:
            IDBRelation relation = iSession.GetRelation(aElementID);
            if (relation == null)
              AbortException.Abort("Связь не найдена");
            attributeInterface = relation.GetAttributeByID(aAttributeID);
            break;
        }
        if (attributeInterface != null)
          attributeInterface.Index = aIndex;
        return attributeInterface;
      }

      public bool Commit
      {
        get => this.commit;
        set => this.commit = value;
      }

      public override void Close()
      {
        if (this.writer == null)
          return;
        try
        {
          if (this.curPosition != 0)
          {
            byte[] numArray = new byte[this.curPosition];
            Buffer.BlockCopy((Array) this.buf, 0, (Array) numArray, 0, this.curPosition);
            this.writer.WriteDataBlock(numArray);
            this.curPosition = 0;
          }
        }
        finally
        {
          if (this.Commit)
            this.writer.CloseBlob(this.RealFileSize);
          else
            this.writer.CancelWrite();
          this.writer = (IBlobWriterEx) null;
          this.lIDBAttribute = (IDBAttribute) null;
        }
        if (this.remoteLock != null)
        {
          this.remoteLock.Dispose();
          this.remoteLock = (RemoteLock) null;
        }
        base.Close();
      }

      public IDBAttribute LIDBAttribute => this.lIDBAttribute;

      public long ElementID => this.elementID;

      public AttributableElements AttributableElement => this.attributableElement;

      public int AttributeID => this.attributeID;

      public int Index => this.index;

      public override bool CanRead => false;

      public override bool CanSeek => false;

      public override bool CanWrite => true;

      public override long Length => this.blobInformation.RealFileSize;

      public long RealFileSize
      {
        get => this.realFileSize;
        set => this.realFileSize = value;
      }

      public override long Position
      {
        get => 0;
        set => throw new Exception("Не поддерживается");
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
        int count1;
        if (count + this.curPosition < this.DataBlockSize)
        {
          Buffer.BlockCopy((Array) buffer, offset, (Array) this.buf, this.curPosition, count);
          this.curPosition += count;
          count = 0;
        }
        else
        {
          for (; count > 0; count -= count1)
          {
            count1 = this.DataBlockSize - this.curPosition;
            if (count1 > count)
              count1 = count;
            Buffer.BlockCopy((Array) buffer, offset, (Array) this.buf, this.curPosition, count1);
            this.curPosition += count1;
            offset += count1;
            if (this.curPosition == this.DataBlockSize)
            {
              this.writer.WriteDataBlock(this.buf);
              this.curPosition = 0;
            }
          }
        }
      }
    }
}
