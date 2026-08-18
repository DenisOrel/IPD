// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBShortBlobAttributeType
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;


namespace Intermech.Kernel;

internal class DBShortBlobAttributeType : DBAttributeType
{
  public DBShortBlobAttributeType(UserSession uSession, DataRow attributeRow)
    : base(uSession, attributeRow)
  {
    AttributeCacheHelper.GetAttributeTypeValues(FieldTypes.ftShortBlob, this._AttributeID, ref this._ValueFieldName, ref this._TextFieldName, ref this._ConvertList, ref this._EnabledOperators, ref this._ComputableAttribute, ref this._PossibleValueFieldName);
    this._CanStorePossibleValues = false;
    this.CompatibleTypes = new FieldTypes[1]
    {
      FieldTypes.ftShortBlob
    };
  }

  internal override string[] IndexFieldNames
  {
    get
    {
      return new string[3]
      {
        "F" + this.AttributeID.ToString(),
        $"F{this.AttributeID.ToString()}ID2",
        $"F{this.AttributeID.ToString()}ID3"
      };
    }
  }

  internal override string ColumnSQL
  {
    get
    {
      return string.Format("{0} {1}, {0}ID {2}, {0}ID2 {3}, {0}ID3 {4}", (object) base.ColumnSQL, (object) this.UserSession.DataManager.DataProvider.NVARCHARType(Consts.MaxStringSize), (object) this.UserSession.DataManager.DataProvider.INTEGERType, (object) this.UserSession.DataManager.DataProvider.FLOATType, (object) this.UserSession.DataManager.DataProvider.DATEType);
    }
  }

  public override void ValidateSizeType(long newValue)
  {
    base.ValidateSizeType(newValue);
    this.CheckMaxSize(newValue, (long) Consts.MaxShortBlobSize);
    long sizeType = this.SizeType;
  }

  private void ConvertShortToBlob(IDBAttributable obj1, IBlobStorage storage, long objID)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    if (!(obj1.GetAttributeByID(this.AttributeID) is DBShortBlobAttribute attributeById))
      return;
    for (int index = 0; index < attributeById.ValuesCount; ++index)
    {
      attributeById.Index = index;
      long asInteger = attributeById.AsInteger;
      long intValue = dataManager.DataProvider.NextGeneratorValue("IMS_FILE_ID_GEN", dataManager);
      BlobInformation blobInformation = ((IBlobReader) attributeById).OpenBlob(0);
      FileInfoStruct fileStruct = new FileInfoStruct();
      fileStruct.FileID = intValue;
      fileStruct.ObjectLinkID = objID;
      fileStruct.AttributeID = this.AttributeID;
      if (asInteger > 0L)
      {
        fileStruct.ArcMethod = blobInformation.ArcMethod;
        fileStruct.FileName = string.Empty;
        fileStruct.Note = blobInformation.Note;
        fileStruct.ModifyDate = !(blobInformation.ModifyDate == DateTime.MinValue) ? blobInformation.ModifyDate : DateTime.UtcNow;
        fileStruct.PacketFileSize = blobInformation.PackedFileSize;
        fileStruct.RealFileSize = blobInformation.RealFileSize;
        if (attributeById.BlobState == BlobAttributeStates.OpenedForRead)
        {
          MemoryStream memoryStream = new MemoryStream(attributeById.ReadDataBlock(0));
          fileStruct.FileBody = (Stream) memoryStream;
        }
        else
          fileStruct.FileBody = (Stream) null;
      }
      else
      {
        fileStruct.ArcMethod = ArcMethods.NotPacked;
        fileStruct.FileName = string.Empty;
        fileStruct.ModifyDate = DateTime.UtcNow;
        fileStruct.Note = string.Empty;
        fileStruct.PacketFileSize = 0L;
        fileStruct.RealFileSize = 0L;
      }
      storage.SetNewFileStruct(fileStruct);
      attributeById.DirectSetValues((object) null, (object) intValue, (object) storage.StorageID, (object) null);
      dataManager.ExecuteNonQuery("DELETE FROM IMS_BLOBS WHERE F_KEY = :blobID", dataManager.Parameter("blobID", (object) asInteger));
    }
  }

  protected override void ValidateChangeAttributeType(FieldTypes newType)
  {
    base.ValidateChangeAttributeType(newType);
    if (newType != FieldTypes.ftBlob)
      return;
    IDbManager dataManager = this.UserSession.DataManager;
    BlobStoragesPool service = ServerServices.GetService(typeof (IBlobStoragesPool)) as BlobStoragesPool;
    IBlobStorage storage = service.GetStorage(service.GetActiveStorageID((IUserSession) this.UserSession), (IUserSession) this.UserSession);
    try
    {
      List<string> objectAttrsTables = this.UserSession.DBCache.GetObjectAttrsTables();
      for (int index1 = 0; index1 < objectAttrsTables.Count; ++index1)
      {
        DataTable dataTable = dataManager.ExecuteDataTable($"SELECT DISTINCT F_OBJECT_ID FROM {objectAttrsTables[index1]} WHERE F_ATTRIBUTE_ID = :attrID", dataManager.Parameter("attrID", (object) this.AttributeID));
        for (int index2 = 0; index2 < dataTable.Rows.Count; ++index2)
        {
          IDBAttributable dbAttributable = (IDBAttributable) this.UserSession.GetObject(Convert.ToInt64(dataTable.Rows[index2][0]), false);
          if (dbAttributable != null && objectAttrsTables[index1] == this.UserSession.DBCache.GetAttributesTableName(dbAttributable.TypeID))
            this.ConvertShortToBlob(dbAttributable, storage, (dbAttributable as DBObject).ObjectID);
        }
      }
      DataTable dataTable1 = dataManager.ExecuteDataTable(sc_12712.ssp_appserver_12713(), dataManager.Parameter("attrID", (object) this.AttributeID));
      for (int index = 0; index < dataTable1.Rows.Count; ++index)
      {
        IDBAttributable relation = (IDBAttributable) this.UserSession.GetRelation(Convert.ToInt64(dataTable1.Rows[index][0]), false);
        if (relation != null)
          this.ConvertShortToBlob(relation, storage, (relation as DBRelation).RelationID);
      }
    }
    finally
    {
      service.ReleaseStorage(storage);
    }
  }
}
