// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBMemoAttributeType
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
using System.Text;


namespace Intermech.Kernel;

internal class DBMemoAttributeType : DBAttributeType
{
  public DBMemoAttributeType(UserSession uSession, DataRow attributeRow)
    : base(uSession, attributeRow)
  {
    AttributeCacheHelper.GetAttributeTypeValues(FieldTypes.ftMemo, this._AttributeID, ref this._ValueFieldName, ref this._TextFieldName, ref this._ConvertList, ref this._EnabledOperators, ref this._ComputableAttribute, ref this._PossibleValueFieldName);
    this._CanStorePossibleValues = false;
    this.CompatibleTypes = new FieldTypes[3]
    {
      FieldTypes.ftMemo,
      FieldTypes.ftString,
      FieldTypes.ftShortBlob
    };
  }

  internal override string[] IndexFieldNames
  {
    get
    {
      return new string[2]
      {
        "F" + this.AttributeID.ToString(),
        $"F{this.AttributeID.ToString()}ID3"
      };
    }
  }

  internal override string ColumnSQL
  {
    get
    {
      return string.Format("{0} {1}, {0}ID {2}, {0}ID3 {3}", (object) base.ColumnSQL, (object) this.UserSession.DataManager.DataProvider.NVARCHARType(Consts.MaxStringSize), (object) this.UserSession.DataManager.DataProvider.INTEGERType, (object) this.UserSession.DataManager.DataProvider.DATEType);
    }
  }

  public override void ValidateSizeType(long newValue)
  {
    base.ValidateSizeType(newValue);
    this.CheckMaxSize(newValue, (long) Consts.MaxMemoSize);
    if (newValue < (long) Consts.MaxStringSize)
      throw new KernelExceptionID(361, (object) Consts.MaxStringSize);
  }

  private void ClearMemos(string tableName)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    dataManager.ExecuteNonQuery(string.Format(sc_12699.ssp_appserver_12700(), (object) tableName), dataManager.Parameter("attrID", (object) this.AttributeID));
    dataManager.ExecuteNonQuery(string.Format(sc_12699.ssp_appserver_12701(), (object) tableName), dataManager.Parameter("attrID", (object) this.AttributeID));
  }

  private void ConvertMemo2ShortBlobFunc(
    DataTable tbl,
    List<DBMemoAttributeType.OldKeyValue> deleteKeys)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    foreach (DataRow row in (InternalDataCollectionBase) tbl.Rows)
    {
      string str = row["F_VALUE"].ToString();
      int int32;
      if (dataManager.DataProvider.Name != "Sql")
      {
        int32 = Convert.ToInt32(dataManager.DataProvider.NextGeneratorValue("IMS_BLOBS_GEN", dataManager));
        dataManager.ExecuteNonQuery("INSERT INTO IMS_BLOBS (F_KEY, F_FILESIZE, F_ARC_METHOD, F_ZIPSIZE, F_VALUE) VALUES (:blobID, 0, 0, 0, NULL)", dataManager.Parameter("blobID", (object) int32));
      }
      else
      {
        using (dataManager.WithOpenConnection())
        {
          dataManager.ExecuteNonQuery("INSERT INTO IMS_BLOBS (F_FILESIZE, F_ARC_METHOD, F_ZIPSIZE, F_VALUE) VALUES (0, 0, 0, NULL)");
          int32 = Convert.ToInt32(dataManager.ExecuteScalar("SELECT @@IDENTITY AS 'ID'"));
        }
      }
      DBMemoAttributeType.OldKeyValue oldKeyValue = new DBMemoAttributeType.OldKeyValue(int32, Convert.ToInt32(row["F_KEY"]), Convert.ToInt64(row[2]), Convert.ToInt32(row["F_INLIST_ID"]));
      deleteKeys.Add(oldKeyValue);
      if (str != string.Empty)
      {
        using (MemoryStream memoryStream = new MemoryStream())
        {
          BinaryWriter binaryWriter = new BinaryWriter((Stream) memoryStream, Encoding.Unicode);
          try
          {
            binaryWriter.Write(str);
            memoryStream.Position = 0L;
            using (MemoryStream outStream = new MemoryStream())
            {
              (ServerServices.GetService(typeof (IPackedStream)) as IPackedStream).PackStream((Stream) outStream, (Stream) memoryStream, 9);
              IDbDataParameter dbDataParameter1 = dataManager.Parameter("val", (object) new DbTypedValue((object) outStream.ToArray(), DbType.Binary));
              DateTime dateTime = row["F_DATE_VALUE"] != DBNull.Value ? Convert.ToDateTime(row["F_DATE_VALUE"]) : DateTime.UtcNow;
              IDbDataParameter dbDataParameter2 = dataManager.Parameter("date1", (object) dateTime);
              dataManager.ExecuteNonQuery("UPDATE IMS_BLOBS SET F_VALUE = :val, F_FILESIZE = :fsize, F_FILEDATE = :date1, F_ARC_METHOD = :arc, F_ZIPSIZE = :zip WHERE F_KEY = :key1", dbDataParameter1, dataManager.Parameter("fsize", (object) memoryStream.Length), dbDataParameter2, dataManager.Parameter("arc", (object) 1), dataManager.Parameter("zip", (object) outStream.Length), dataManager.Parameter("key1", (object) int32));
            }
          }
          finally
          {
            binaryWriter.Close();
          }
        }
      }
    }
  }

  protected override void ValidateChangeAttributeType(FieldTypes newType)
  {
    base.ValidateChangeAttributeType(newType);
    IDbManager dataManager = this.UserSession.DataManager;
    List<string> objectAttrsTables = this.UserSession.DBCache.GetObjectAttrsTables();
    objectAttrsTables.Add("IMS_RELATION_ATTRS");
    for (int index1 = 0; index1 < objectAttrsTables.Count; ++index1)
    {
      switch (newType)
      {
        case FieldTypes.ftString:
          this.ClearMemos(objectAttrsTables[index1]);
          break;
        case FieldTypes.ftShortBlob:
          List<DBMemoAttributeType.OldKeyValue> deleteKeys = new List<DBMemoAttributeType.OldKeyValue>();
          string str = "IMS_RELATION_ATTRS" == objectAttrsTables[index1] || "IMS_REL_SNAPATTRS" == objectAttrsTables[index1] ? "F_PRJLINK_ID" : "F_OBJECT_ID";
          this.ConvertMemo2ShortBlobFunc(dataManager.ExecuteDataTable(string.Format("SELECT F_VALUE, F_KEY, {0}, F_DATE_VALUE, F_INLIST_ID  FROM {1}, IMS_MEMOS WHERE {1}.F_ATTRIBUTE_ID = :attrID AND IMS_MEMOS.F_KEY = {1}.F_INTEGER_VALUE", (object) str, (object) objectAttrsTables[index1]), dataManager.Parameter("attrID", (object) this.AttributeID)), deleteKeys);
          if (deleteKeys.Count > 0)
          {
            IDbDataParameter dbDataParameter1 = dataManager.Parameter("keyID", (object) 0);
            IDbDataParameter dbDataParameter2 = dataManager.Parameter("attrID", (object) this.AttributeID);
            IDbDataParameter dbDataParameter3 = dataManager.Parameter("objID", (object) 0L);
            IDbDataParameter dbDataParameter4 = dataManager.Parameter("lstID", (object) 0);
            for (int index2 = 0; index2 < deleteKeys.Count; ++index2)
            {
              dbDataParameter1.Value = (object) deleteKeys[index2].MemoID;
              dataManager.ExecuteNonQuery("DELETE FROM IMS_MEMOS WHERE F_KEY = :keyID", dbDataParameter1);
              dbDataParameter1.Value = (object) deleteKeys[index2].BlobID;
              dbDataParameter3.Value = (object) deleteKeys[index2].ObjectID;
              dbDataParameter4.Value = (object) deleteKeys[index2].ListID;
              dataManager.ExecuteNonQuery($"UPDATE {objectAttrsTables[index1]} SET F_INTEGER_VALUE = :keyID WHERE {str} = :objID AND F_ATTRIBUTE_ID = :attrID  AND F_INLIST_ID = :lstID", dbDataParameter1, dbDataParameter3, dbDataParameter2, dbDataParameter4);
            }
            break;
          }
          break;
      }
    }
  }

  internal override bool CompareValues(object value1, object value2)
  {
    return CompareValuesHelper.CompareStringValues(value1, value2);
  }

  private struct OldKeyValue(int _blobID, int _memoID, long _objID, int _listID)
  {
    public int BlobID = _blobID;
    public int MemoID = _memoID;
    public long ObjectID = _objID;
    public int ListID = _listID;
  }
}
