
// Type: Intermech.Interfaces.BlobStream.BlobReaderStreamInternal
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Remoting.Sponsors;
using System;
using System.IO;


namespace Intermech.Interfaces.BlobStream
{
    /// <summary>Внутренний поток для чтения из базы</summary>
    internal class BlobReaderStreamInternal : Stream
    {
      internal int dataBlockSize = Consts.DefaultBlobBlockSize;
      internal long fileSize;
      internal BlobInformation blobInformation;
      private IBlobReader reader;
      private RemoteLock remoteLock;
      internal IUserSession session;
      internal IDBAttribute lIDBAttribute;
      internal long elementID;
      internal AttributableElements attributableElement;
      internal int attributeID;
      internal int index;
      private byte[] buf;
      private int bufPsition;

      public BlobReaderStreamInternal(
        long aElementID,
        AttributableElements aAttributableElement,
        int aAttributeID,
        int aIndex,
        int dataBlockSize,
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
        this.reader = (this.lIDBAttribute == null ? this.GetAttributeInterface(this.elementID, this.attributableElement, this.attributeID, this.index, this.session) : this.lIDBAttribute) as IBlobReader;
        if (this.reader == null)
          return;
        this.blobInformation = this.reader.OpenBlob(dataBlockSize);
        this.buf = new byte[dataBlockSize];
        this.fileSize = this.blobInformation.ArcMethod == ArcMethods.NotPacked ? this.blobInformation.RealFileSize : this.blobInformation.PackedFileSize;
        this.remoteLock = new RemoteLock();
        this.remoteLock.Add((object) this.reader);
      }

      public BlobReaderStreamInternal(IDBAttribute attr, int dataBlockSize, IUserSession uSession)
      {
        this.lIDBAttribute = attr;
        this.session = uSession;
        if (dataBlockSize > 0)
          this.dataBlockSize = dataBlockSize;
        this.reader = this.lIDBAttribute as IBlobReader;
        this.blobInformation = this.reader.OpenBlob(dataBlockSize);
        this.buf = new byte[dataBlockSize];
        this.fileSize = this.blobInformation.ArcMethod == ArcMethods.NotPacked ? this.blobInformation.RealFileSize : this.blobInformation.PackedFileSize;
        this.remoteLock = new RemoteLock();
        this.remoteLock.Add((object) this.reader);
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
            attributeInterface = dbObject.GetAttributeByID(aAttributeID);
            break;
          case AttributableElements.Relation:
            IDBRelation relation = iSession.GetRelation(aElementID);
            if (relation == null)
              AbortException.Abort("Связь не найдена");
            attributeInterface = relation.GetAttributeByID(aAttributeID);
            break;
        }
        if (attributeInterface != null)
        {
          while (attributeInterface.ValuesCount - 1 < aIndex)
            attributeInterface.AddValue((object) null);
          attributeInterface.Index = aIndex;
        }
        return attributeInterface;
      }

      public override void Close()
      {
        if (this.reader == null)
          return;
        if (this.remoteLock != null)
        {
          this.remoteLock.Dispose();
          this.remoteLock = (RemoteLock) null;
        }
        try
        {
          if (this.reader.BlobState != BlobAttributeStates.Closed)
            this.reader.CloseBlob();
        }
        catch
        {
        }
        this.buf = (byte[]) null;
        this.lIDBAttribute = (IDBAttribute) null;
        this.reader = (IBlobReader) null;
        base.Close();
      }

      public IDBAttribute LIDBAttribute => this.lIDBAttribute;

      public long ElementID => this.elementID;

      public AttributableElements AttributableElement => this.attributableElement;

      public int AttributeID => this.attributeID;

      public int Index => this.index;

      public override bool CanRead => true;

      public override bool CanSeek => false;

      public override bool CanWrite => false;

      public override long Length => this.blobInformation.RealFileSize;

      public override long Position
      {
        get => throw new Exception("Не поддерживается");
        set
        {
          if (value != 0L)
            throw new Exception("Допустима установка только нулевой позиции");
          try
          {
            if (this.reader.BlobState != BlobAttributeStates.Closed)
              this.reader.CloseBlob();
          }
          catch
          {
          }
          this.blobInformation = this.reader.OpenBlob(this.dataBlockSize);
          this.fileSize = this.blobInformation.ArcMethod == ArcMethods.NotPacked ? this.blobInformation.RealFileSize : this.blobInformation.PackedFileSize;
        }
      }

      public override void Flush()
      {
      }

      public override int Read(byte[] buffer, int offset, int count)
      {
        try
        {
          if (this.reader.BlobState == BlobAttributeStates.Closed)
            return 0;
          byte[] numArray = this.reader.ReadDataBlock(count);
          numArray.CopyTo((Array) buffer, offset);
          return numArray.Length;
        }
        catch
        {
          return 0;
        }
      }

      public override long Seek(long offset, SeekOrigin origin) => 0;

      public override void SetLength(long value)
      {
      }

      public override void Write(byte[] buffer, int offset, int count)
      {
      }
    }
}
