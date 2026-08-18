
// Type: Intermech.Search.Data.Repositories.BlobRepository
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;


namespace Intermech.Search.Data.Repositories
{
    public sealed class BlobRepository : IBlobRepository
    {
      public void AddOrUpdate(Blob blob)
      {
        if (ObjectHelper.IsUnknownObjectVersionID(blob.Key.ObjectVersionID))
          return;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBTransactions customService = sessionKeeper.Session.GetCustomService(typeof (IDBTransactions)) as IDBTransactions;
          try
          {
            IDBAttribute byId = sessionKeeper.Session.GetObject(blob.Key.ObjectVersionID).Attributes.FindByID(blob.Key.AttributeTypeID);
            if (byId == null)
              throw new Exception();
            if (blob.Key.Index == int.MaxValue)
            {
              byId.AddValue((object) null);
              byId.Index = byId.ValuesCount - 1;
            }
            else
              byId.Index = blob.Key.Index;
            IBlobWriter blobWriter = byId as IBlobWriter;
            BlobInformation blobInfo = new BlobInformation()
            {
              ArcMethod = blob.ArcMethod,
              FileName = blob.FileName,
              FileType = blob.FileType,
              ModifyDate = DateTime.Now,
              PackedFileSize = blob.PackedFileSize,
              RealFileSize = blob.RealFileSize
            };
            if (blob.ArcMethod == ArcMethods.NotPacked)
            {
              if (blobInfo.RealFileSize == 0L)
                blobInfo.RealFileSize = (long) blob.Value.Length;
              if (blobInfo.PackedFileSize == 0L)
                blobInfo.PackedFileSize = (long) blob.Value.Length;
            }
            else if (blob.ArcMethod == ArcMethods.ZLibPacked && blobInfo.PackedFileSize == 0L)
              blobInfo.PackedFileSize = (long) blob.Value.Length;
            if (blobInfo.Note == null)
              blobInfo.Note = string.Empty;
            blobWriter.OpenBlob(blobInfo, false);
            blobWriter.WriteDataBlock(blob.Value);
            customService.Commit();
          }
          catch
          {
            customService.Rollback();
            throw;
          }
        }
      }

      public Blob Find(BlobKey key)
      {
        if (key == null)
          throw new ArgumentNullException(nameof (key));
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          return this.FindInternal(ObjectHelper.IsUnknownObjectVersionID(key.ObjectVersionID) ? (IDBAttributable) sessionKeeper.Session.GetRelation(key.RelationID) : (IDBAttributable) sessionKeeper.Session.GetObject(key.ObjectVersionID), key);
      }

      public Blob Find(string fileName, bool withValue) => throw new NotImplementedException();

      public List<Blob> FindForObject(long objectVersionID)
      {
        if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
          throw new ArgumentException();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          return this.FindForObjectInternal((IDBAttributable) sessionKeeper.Session.GetObject(objectVersionID), objectVersionID);
      }

      public List<Blob> FindForRelation(long relationID)
      {
        if (RelationHelper.IsUnknownRelationID(relationID))
          throw new ArgumentException();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          return this.FindForRelationInternal((IDBAttributable) sessionKeeper.Session.GetRelation(relationID), relationID);
      }

      public void RemoveForObject(long objectVersionID) => throw new NotImplementedException();

      public void RemoveForRelation(long relationVersionID) => throw new NotImplementedException();

      private Blob FindInternal(IDBAttributable attributable, BlobKey key)
      {
        IDBAttribute attributeById = attributable.GetAttributeByID(key.AttributeTypeID);
        if (attributeById == null || attributeById.ValuesCount <= key.Index)
          return (Blob) null;
        attributeById.Index = key.Index;
        IBlobReader blobReader = attributeById as IBlobReader;
        BlobInformation blobInformation = blobReader.OpenBlob(-1);
        Blob blob = new Blob(key);
        blob.ArcMethod = blobInformation.ArcMethod;
        blob.ID = blobInformation.BlobID;
        blob.FileName = blobInformation.FileName;
        blob.FileType = blobInformation.FileType;
        blob.Note = blobInformation.Note;
        blob.PackedFileSize = blobInformation.PackedFileSize;
        blob.RealFileSize = blobInformation.RealFileSize;
        blobReader.OpenBlob(0);
        blob.Value = blobReader.ReadDataBlock();
        return blob;
      }

      private List<Blob> FindForObjectInternal(IDBAttributable attributable, long objectVersionID)
      {
        List<Blob> forObjectInternal = new List<Blob>();
        IDBAttributeCollection attributes = attributable.Attributes;
        int AttrIndex = 0;
        for (int count = attributes.Count; AttrIndex < count; ++AttrIndex)
        {
          IDBAttribute dbAttribute = attributes[AttrIndex];
          switch (dbAttribute.AttributeType.AttributeType)
          {
            case FieldTypes.ftShortBlob:
            case FieldTypes.ftFile:
            case FieldTypes.ftBlob:
              int num = 0;
              for (int valuesCount = dbAttribute.ValuesCount; num < valuesCount; ++num)
              {
                dbAttribute.Index = num;
                BlobInformation blobInformation = (dbAttribute as IBlobReader).OpenBlob(-1);
                Blob blob = new Blob(new BlobKey(objectVersionID, 0L, dbAttribute.AttributeID, dbAttribute.Index))
                {
                  ArcMethod = blobInformation.ArcMethod,
                  ID = blobInformation.BlobID,
                  FileName = blobInformation.FileName,
                  FileType = blobInformation.FileType,
                  RealFileSize = blobInformation.RealFileSize,
                  PackedFileSize = blobInformation.PackedFileSize
                };
                forObjectInternal.Add(blob);
              }
              break;
          }
        }
        return forObjectInternal;
      }

      private List<Blob> FindForRelationInternal(IDBAttributable attributable, long relationID)
      {
        List<Blob> relationInternal = new List<Blob>();
        IDBAttributeCollection attributes = attributable.Attributes;
        int AttrIndex = 0;
        for (int count = attributes.Count; AttrIndex < count; ++AttrIndex)
        {
          IDBAttribute dbAttribute = attributes[AttrIndex];
          switch (dbAttribute.AttributeType.AttributeType)
          {
            case FieldTypes.ftShortBlob:
            case FieldTypes.ftFile:
            case FieldTypes.ftBlob:
              int num = 0;
              for (int valuesCount = dbAttribute.ValuesCount; num < valuesCount; ++num)
              {
                dbAttribute.Index = num;
                BlobInformation blobInformation = (dbAttribute as IBlobReader).OpenBlob(-1);
                Blob blob = new Blob(new BlobKey(0L, relationID, dbAttribute.AttributeID, dbAttribute.Index))
                {
                  ArcMethod = blobInformation.ArcMethod,
                  ID = blobInformation.BlobID
                };
                relationInternal.Add(blob);
              }
              break;
          }
        }
        return relationInternal;
      }
    }
}
