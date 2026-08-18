// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBBlobAttributeType
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel;

internal class DBBlobAttributeType : DBAttributeType
{
  public DBBlobAttributeType(UserSession uSession, DataRow attributeRow)
    : base(uSession, attributeRow)
  {
    AttributeCacheHelper.GetAttributeTypeValues(FieldTypes.ftBlob, this._AttributeID, ref this._ValueFieldName, ref this._TextFieldName, ref this._ConvertList, ref this._EnabledOperators, ref this._ComputableAttribute, ref this._PossibleValueFieldName);
    this._CanStorePossibleValues = false;
    this.CompatibleTypes = new FieldTypes[3]
    {
      FieldTypes.ftFile,
      FieldTypes.ftBlob,
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
    this.CheckMaxSize(newValue, (long) Consts.MaxNumericSize);
  }

  private void ConvertBlobToShort(IDBAttributable obj1)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    if (!(obj1.GetAttributeByID(this.AttributeID) is DBStorageAttribute attributeById))
      return;
    for (int index = 0; index < attributeById.ValuesCount; ++index)
    {
      attributeById.Index = index;
      BlobInformation blobInformation = ((IBlobReader) attributeById).OpenBlob(0);
      byte[] numArray = !attributeById.IsNull ? attributeById.ReadDataBlock() : new byte[0];
      long asInteger = attributeById.AsInteger;
      IDbDataParameter dbDataParameter = dataManager.Parameter("val1", (object) new DbTypedValue((object) numArray, DbType.Binary));
      long intValue;
      if (dataManager.DataProvider.Name != "Sql")
      {
        intValue = dataManager.DataProvider.NextGeneratorValue("IMS_BLOBS_GEN", dataManager);
        dataManager.ExecuteNonQuery("INSERT INTO IMS_BLOBS (F_KEY, F_VALUE, F_FILESIZE, F_ZIPSIZE, F_ARC_METHOD, F_FILEDATE) VALUES (:key1, :val1, :fsize, :zip1, :arc1, :m_date1)", dataManager.Parameter("key1", (object) intValue), dbDataParameter, dataManager.Parameter("fsize", (object) blobInformation.RealFileSize), dataManager.Parameter("zip1", (object) blobInformation.PackedFileSize), dataManager.Parameter("arc1", (object) Convert.ToInt32((object) blobInformation.ArcMethod)), dataManager.Parameter("m_date1", (object) blobInformation.ModifyDate));
      }
      else
      {
        dataManager.ExecuteNonQuery("INSERT INTO IMS_BLOBS (F_VALUE, F_FILESIZE, F_ZIPSIZE, F_ARC_METHOD, F_FILEDATE) VALUES (:val1, :fsize, :zip1, :arc1, :m_date1)", dbDataParameter, dataManager.Parameter("fsize", (object) blobInformation.RealFileSize), dataManager.Parameter("zip1", (object) blobInformation.PackedFileSize), dataManager.Parameter("arc1", (object) Convert.ToInt32((object) blobInformation.ArcMethod)), dataManager.Parameter("m_date1", (object) blobInformation.ModifyDate));
        intValue = Convert.ToInt64(dataManager.ExecuteScalar("SELECT @@IDENTITY AS 'ID'"));
      }
      IBlobStorage storage = attributeById.StoragesPool.GetStorage(attributeById.StorageID, (IUserSession) this.UserSession);
      try
      {
        storage.DeleteFile(asInteger);
      }
      finally
      {
        attributeById.StoragesPool.ReleaseStorage(storage);
      }
      attributeById.DirectSetValues((object) null, (object) intValue, (object) 0L, (object) null);
    }
  }

  protected override void ValidateChangeAttributeType(FieldTypes newType)
  {
    base.ValidateChangeAttributeType(newType);
    if (newType != FieldTypes.ftShortBlob)
      return;
    IDbManager dataManager = this.UserSession.DataManager;
    BlobStoragesPool service = ServerServices.GetService(typeof (IBlobStoragesPool)) as BlobStoragesPool;
    DataTable dataTable1 = this.UserSession.GetObjectCollection(new Guid("cad00014-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
    {
      (object) -2,
      (object) -50
    }));
    for (int index = 0; index < dataTable1.Rows.Count; ++index)
    {
      IBlobStorage storage = service.GetStorage(Convert.ToInt64(dataTable1.Rows[index][0]), (IUserSession) this.UserSession);
      try
      {
        object obj = storage.DataManager.ExecuteScalar(string.Format(sc_12679.ssp_appserver_12680(), (object) storage.StorageName, (object) this.AttributeID, (object) Consts.DefaultShortBlobSize));
        if (Convert.ToInt32(obj) > 0)
          throw new KernelExceptionID(sc_12679.ssp_appserver_12681(609539378), (object) this.Name, obj, (object) Consts.DefaultShortBlobSize);
      }
      finally
      {
        service.ReleaseStorage(storage);
      }
    }
    List<string> objectAttrsTables = this.UserSession.DBCache.GetObjectAttrsTables();
    for (int index1 = 0; index1 < objectAttrsTables.Count; ++index1)
    {
      DataTable dataTable2 = dataManager.ExecuteDataTable($"SELECT DISTINCT F_OBJECT_ID FROM {objectAttrsTables[index1]} WHERE F_ATTRIBUTE_ID = :attrID", dataManager.Parameter("attrID", (object) this.AttributeID));
      for (int index2 = 0; index2 < dataTable2.Rows.Count; ++index2)
      {
        IDBAttributable dbAttributable = (IDBAttributable) this.UserSession.GetObject(Convert.ToInt64(dataTable2.Rows[index2][0]), false);
        if (dbAttributable != null)
          this.ConvertBlobToShort(dbAttributable);
      }
    }
    DataTable dataTable3 = dataManager.ExecuteDataTable(sc_12679.ssp_appserver_12682(), dataManager.Parameter("attrID", (object) this.AttributeID));
    for (int index = 0; index < dataTable3.Rows.Count; ++index)
    {
      IDBAttributable relation = (IDBAttributable) this.UserSession.GetRelation(Convert.ToInt64(dataTable3.Rows[index][0]), false);
      if (relation != null)
        this.ConvertBlobToShort(relation);
    }
  }

  public override string SizeTypeDescription => string.Empty;
}
