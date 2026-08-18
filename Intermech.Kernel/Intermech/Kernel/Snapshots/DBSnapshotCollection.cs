// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Snapshots.DBSnapshotCollection
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Snapshots;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Snapshots;

public class DBSnapshotCollection : DBSessionable, IDBSnapshotCollection
{
  private static Dictionary<ActionType, bool> metadataActions = new Dictionary<ActionType, bool>(2);

  static DBSnapshotCollection()
  {
    DBSnapshotCollection.metadataActions.Add(ActionType.GetAccess, false);
    DBSnapshotCollection.metadataActions.Add(ActionType.SetAccess, false);
  }

  public DBSnapshotCollection(UserSession session)
    : base(session)
  {
    this.InitStaticSecurityOptions(23, 0L, DBSnapshotCollection.metadataActions);
  }

  public override string ObjectName => LocalizationHolder.rm.GetString("SnapshotCollection");

  public long Create(long objectID, string snapshotName, string FiltrationOwnerID)
  {
    this.UserSession.StartTransaction();
    try
    {
      List<long> addedObjects = new List<long>();
      long num = this.CreateInternal(objectID, snapshotName, FiltrationOwnerID, addedObjects);
      this.UserSession.Commit();
      return num;
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
  }

  private long CreateInternal(
    long objectID,
    string snapshotName,
    string FiltrationOwnerID,
    List<long> addedObjects)
  {
    long snapshotID = this.UserSession.DataManager.DataProvider.NextGeneratorValue(sc_14226.ssp_appserver_14227(), this.UserSession.DataManager);
    this.CreateSnapshotInternal(objectID, snapshotID, snapshotName, FiltrationOwnerID, addedObjects, true);
    DBObject dbObject = this.UserSession.GetObject(objectID) as DBObject;
    dbObject.Attributes.AddAttribute(this.UserSession.IdentHelper.ActiveSnapshotID, false, new object[1]
    {
      (object) snapshotID
    });
    dbObject.AfterCreateSnapshot((IDBSnapshotCollection) this, snapshotID, snapshotName, FiltrationOwnerID, addedObjects);
    return snapshotID;
  }

  public long Create(
    long objectID,
    string snapshotName,
    string FiltrationOwnerID,
    long[] addObjectsID)
  {
    this.UserSession.StartTransaction();
    try
    {
      List<long> longList = new List<long>();
      long snapshotID = this.CreateInternal(objectID, snapshotName, FiltrationOwnerID, longList);
      for (int index = 0; index < addObjectsID.Length; ++index)
        this.AddObjectToSnapshot(addObjectsID[index], snapshotID, string.Empty, FiltrationOwnerID, longList);
      this.UserSession.Commit();
      return snapshotID;
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
  }

  public void AddObjectToSnapshot(
    long objectID,
    long snapshotID,
    string snapshotName,
    string FiltrationOwnerID,
    List<long> createdObjects)
  {
    if (createdObjects.Contains(objectID))
      return;
    this.CreateSnapshotInternal(objectID, snapshotID, snapshotName, FiltrationOwnerID, createdObjects, false);
  }

  private void CreateSnapshotInternal(
    long objectID,
    long snapshotID,
    string snapshotName,
    string FiltrationOwnerID,
    List<long> createdObjects,
    bool createSnapshot)
  {
    createdObjects.Add(objectID);
    this.UserSession.StartTransaction();
    try
    {
      IDbManager dataManager = this.UserSession.DataManager;
      IDbDataParameter snapID = dataManager.Parameter("snapID", (object) snapshotID);
      QuickObjectInfo objectInfo = this.UserSession.GetObjectInfo(objectID);
      long EventID;
      long num;
      string str;
      if (createSnapshot)
      {
        SqlHelper.ValidateEmptyValue(snapshotName, LocalizationHolder.rm.GetString("SnapshotFieldName"));
        EventID = this.AddEvent(objectID, ActionType.Create, EventlogRecordType.AccessGranted, string.Format(LocalizationHolder.rm.GetString("CreateSnapshot"), (object) snapshotName));
        num = this.UserSession.UserID;
        str = snapshotName;
        dataManager.ExecuteNonQuery($"INSERT INTO IMS_SNAPSHOTS (F_SNAPSHOT_ID, F_OBJECT_ID, F_ID, F_NAME, F_USER_ID, F_SNAPSHOT_DATE) VALUES (:snapID, :objID, :par_ID, :snapName, :userID, {dataManager.DataProvider.Now})", snapID, dataManager.Parameter("objID", (object) Math.Abs(objectID)), dataManager.Parameter("par_ID", (object) objectInfo.ID), dataManager.Parameter("snapName", (object) str), dataManager.Parameter("userID", (object) num));
      }
      else
      {
        EventID = 0L;
        num = 0L;
        str = string.Empty;
      }
      dataManager.ExecuteNonQuery("INSERT INTO IMS_OBJ_SNAPSHOT (F_SNAPSHOT_ID, F_OBJECT_ID, F_ID, F_LC_STEP, F_VERSION_ID, F_OBJECT_TYPE, F_OWNER_ID, F_ACCESS, F_MODIFY_DATE, F_LEVEL_ID, F_OBJ_CREATE, F_PROJECT_ID, F_MODIFICATION_ID, CAPTION, F_SITE_ID, F_NOTE, F_USER_ID, F_SNAPSHOT_DATE) " + $"SELECT :snapID, ABS(F_OBJECT_ID), F_ID, F_LC_STEP, F_VERSION_ID, F_OBJECT_TYPE, F_OWNER_ID, F_ACCESS, F_MODIFY_DATE, F_LEVEL_ID, F_OBJ_CREATE, F_PROJECT_ID, F_MODIFICATION_ID, :captionPar, F_SITE_ID, :snapName, :userID, {dataManager.DataProvider.Now} FROM IMS_OBJECTS WHERE F_OBJECT_ID = :objID", snapID, dataManager.Parameter("objID", (object) objectID), dataManager.Parameter("captionPar", (object) objectInfo.Caption), dataManager.Parameter("snapName", (object) str), dataManager.Parameter("userID", (object) num));
      dataManager.ExecuteNonQuery("INSERT INTO IMS_OBJ_SNAPATTRS (F_SNAPSHOT_ID, F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_STRING_VALUE, F_INTEGER_VALUE, F_DOUBLE_VALUE, F_DATE_VALUE) " + $"SELECT :snapID, ABS(F_OBJECT_ID), F_ATTRIBUTE_ID, F_INLIST_ID, F_STRING_VALUE, F_INTEGER_VALUE, F_DOUBLE_VALUE, F_DATE_VALUE FROM {this.UserSession.DBCache.GetAttributesTableName(objectInfo.ObjectTypeID)} WHERE F_OBJECT_ID = :objID", snapID, dataManager.Parameter("objID", (object) objectID));
      this.CopyMemoBlobs(snapID, "IMS_OBJ_SNAPATTRS", "F_OBJECT_ID", Math.Abs(objectID));
      dataManager.ExecuteNonQuery("INSERT INTO IMS_REL_SNAPSHOT (F_SNAPSHOT_ID, F_PRJLINK_ID, F_PROJ_ID, F_PART_ID, F_RELATION_TYPE, F_CREATE_DATE, F_PRJ_GUID, F_REL_CREATOR) SELECT :snapID, ABS(F_PRJLINK_ID), ABS(F_PROJ_ID), F_PART_ID, F_RELATION_TYPE, F_CREATE_DATE, F_PRJ_GUID, F_REL_CREATOR FROM IMS_RELATIONS WHERE (F_PROJ_ID = :objID)", snapID, dataManager.Parameter("objID", (object) objectID));
      dataManager.ExecuteNonQuery("INSERT INTO IMS_REL_SNAPATTRS (F_SNAPSHOT_ID, F_PRJLINK_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_STRING_VALUE, F_INTEGER_VALUE, F_DOUBLE_VALUE, F_DATE_VALUE) SELECT :snapID, ABS(A.F_PRJLINK_ID), A.F_ATTRIBUTE_ID, A.F_INLIST_ID, A.F_STRING_VALUE, A.F_INTEGER_VALUE, A.F_DOUBLE_VALUE, A.F_DATE_VALUE FROM IMS_RELATIONS R, IMS_RELATION_ATTRS A WHERE (R.F_PROJ_ID = :objID) AND (A.F_PRJLINK_ID = R.F_PRJLINK_ID)", snapID, dataManager.Parameter("objID", (object) objectID));
      DataTable dataTable1 = dataManager.ExecuteDataTable("SELECT F_PRJLINK_ID FROM IMS_REL_SNAPSHOT WHERE F_SNAPSHOT_ID = :snapID AND F_PROJ_ID = :objID", snapID, dataManager.Parameter("objID", (object) Math.Abs(objectID)));
      for (int index = 0; index < dataTable1.Rows.Count; ++index)
        this.CopyMemoBlobs(snapID, "IMS_REL_SNAPATTRS", "F_PRJLINK_ID", Convert.ToInt64(dataTable1.Rows[index][0]));
      IDBRelationsApplicabilityCollection applicabilityCollection = this.UserSession.GetRelationsApplicabilityCollection();
      List<int> intList = new List<int>();
      DataTable applicabilitiesList = applicabilityCollection.GetApplicabilitiesList(-1, -1, objectInfo.ObjectTypeID);
      for (int index = 0; index < applicabilitiesList.Rows.Count; ++index)
      {
        if ((Convert.ToInt32(applicabilitiesList.Rows[index]["F_OPTIONS"]) & 16 /*0x10*/) == 16 /*0x10*/ && Convert.ToInt32(applicabilitiesList.Rows[index]["F_MIN_LINKS"]) != -1)
        {
          int int32 = Convert.ToInt32(applicabilitiesList.Rows[index]["F_RELATION_TYPE"]);
          if (intList.IndexOf(int32) == -1)
            intList.Add(int32);
        }
      }
      for (int index1 = 0; index1 < intList.Count; ++index1)
      {
        IDBRelationCollection relationCollection = this.UserSession.GetRelationCollection(intList[index1], FiltrationOwnerID);
        relationCollection.LocalTypesMode = true;
        DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[3]
        {
          (object) -2,
          (object) -23,
          (object) -7
        });
        DataTable dataTable2 = relationCollection.ConsistFrom(paramSet, objectID);
        for (int index2 = 0; index2 < dataTable2.Rows.Count; ++index2)
        {
          IDBRelationsApplicability applicability = applicabilityCollection.GetApplicability(Convert.ToInt32(dataTable2.Rows[index2][1]), Convert.ToInt32(dataTable2.Rows[index2][2]), objectInfo.ObjectTypeID);
          if (applicability != null && (applicability.Options & ApplicabilityOptions.CreateSnapshotChild) == ApplicabilityOptions.CreateSnapshotChild)
          {
            long int64 = Convert.ToInt64(dataTable2.Rows[index2][0]);
            if (!createdObjects.Contains(int64))
              this.CreateSnapshotInternal(int64, snapshotID, snapshotName, FiltrationOwnerID, createdObjects, false);
          }
        }
      }
      if (EventID > 0L)
        this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
  }

  private void CopyMemoBlobs(
    IDbDataParameter snapID,
    string tableName,
    string keyFieldName,
    long objLinkID)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    DataTable dataTable = dataManager.ExecuteDataTable(string.Format("SELECT {0}, F_ATTRIBUTE_ID, F_INLIST_ID, F_INTEGER_VALUE, F_DOUBLE_VALUE FROM {1} WHERE F_SNAPSHOT_ID = :snapID AND {0} = :objLinkPar", (object) keyFieldName, (object) tableName), snapID, dataManager.Parameter("objLinkPar", (object) objLinkID));
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(Convert.ToInt32(dataTable.Rows[index][1]));
      long fileID = 0;
      if (dataTable.Rows[index][3] != null && dataTable.Rows[index][3] != DBNull.Value)
        fileID = Convert.ToInt64(dataTable.Rows[index][3]);
      if (attributeType != null && fileID > 0L)
      {
        long num = 0;
        string str = string.Empty;
        switch (attributeType.FieldType)
        {
          case FieldTypes.ftShortBlob:
            if (dataManager.DataProvider.Name != "Sql")
            {
              num = dataManager.DataProvider.NextGeneratorValue("IMS_BLOBS_SNAPSHOT_GEN", dataManager);
              dataManager.ExecuteNonQuery("INSERT INTO IMS_BLOBS_SNAPSHOT (F_KEY, F_SNAPSHOT_ID, F_VALUE, F_FILESIZE, F_FILEDATE, F_ARC_METHOD, F_ZIPSIZE) SELECT :newKey, :snapID, F_VALUE, F_FILESIZE, F_FILEDATE, F_ARC_METHOD, F_ZIPSIZE FROM IMS_BLOBS WHERE F_KEY = :oldKey", dataManager.Parameter("newKey", (object) Convert.ToInt32(num)), snapID, dataManager.Parameter("oldKey", (object) Convert.ToInt32(dataTable.Rows[index][3])));
              break;
            }
            using (dataManager.WithOpenConnection())
            {
              dataManager.ExecuteNonQuery("INSERT INTO IMS_BLOBS_SNAPSHOT (F_SNAPSHOT_ID, F_VALUE, F_FILESIZE, F_FILEDATE, F_ARC_METHOD, F_ZIPSIZE) SELECT :snapID, F_VALUE, F_FILESIZE, F_FILEDATE, F_ARC_METHOD, F_ZIPSIZE FROM IMS_BLOBS WHERE F_KEY = :oldKey", snapID, dataManager.Parameter("oldKey", (object) Convert.ToInt32(dataTable.Rows[index][3])));
              num = Convert.ToInt64(dataManager.ExecuteScalar("SELECT @@IDENTITY AS 'ID'"));
              break;
            }
          case FieldTypes.ftFile:
          case FieldTypes.ftBlob:
            num = dataManager.DataProvider.NextGeneratorValue("IMS_FILE_ID_GEN", dataManager);
            IBlobStoragesPool service = ServerServices.GetService(typeof (IBlobStoragesPool)) as IBlobStoragesPool;
            IBlobStorage storage = service.GetStorage(Convert.ToInt64(dataTable.Rows[index][4]), (IUserSession) this.UserSession);
            try
            {
              FileInfoStruct fileStruct = storage.GetFileStruct(fileID, true);
              fileStruct.FileID = num;
              fileStruct.ObjectLinkID = Convert.ToInt64(snapID.Value);
              storage.CopyToTemporaryFile(fileStruct);
              storage.SetFileStruct(fileStruct);
              str = ", F_DOUBLE_VALUE = " + storage.StorageID.ToString();
              if (fileStruct.FileBody != null)
              {
                fileStruct.FileBody.Close();
                break;
              }
              break;
            }
            finally
            {
              service.ReleaseStorage(storage);
            }
          case FieldTypes.ftMemo:
            if (dataManager.DataProvider.Name != "Sql")
            {
              num = dataManager.DataProvider.NextGeneratorValue("IMS_MEMOS_SNAPSHOT_GEN", dataManager);
              dataManager.ExecuteNonQuery("INSERT INTO IMS_MEMOS_SNAPSHOT (F_KEY, F_SNAPSHOT_ID, F_VALUE) SELECT :newKey, :snapID, F_VALUE FROM IMS_MEMOS WHERE F_KEY = :oldKey", dataManager.Parameter("newKey", (object) Convert.ToInt32(num)), snapID, dataManager.Parameter("oldKey", (object) Convert.ToInt32(dataTable.Rows[index][3])));
              break;
            }
            using (dataManager.WithOpenConnection())
            {
              dataManager.ExecuteNonQuery("INSERT INTO IMS_MEMOS_SNAPSHOT (F_SNAPSHOT_ID, F_VALUE) SELECT :snapID, F_VALUE FROM IMS_MEMOS WHERE F_KEY = :oldKey", snapID, dataManager.Parameter("oldKey", (object) Convert.ToInt32(dataTable.Rows[index][3])));
              num = Convert.ToInt64(dataManager.ExecuteScalar("SELECT @@IDENTITY AS 'ID'"));
              break;
            }
        }
        if (num > 0L)
          dataManager.ExecuteNonQuery($"UPDATE {tableName} SET F_INTEGER_VALUE = :newKey{str} WHERE {keyFieldName} = :objID AND F_ATTRIBUTE_ID = :attrID AND F_INLIST_ID = :inlistID AND F_SNAPSHOT_ID = :snapID", dataManager.Parameter("newKey", (object) num), snapID, dataManager.Parameter("objID", (object) Convert.ToInt64(dataTable.Rows[index][0])), dataManager.Parameter("attrID", (object) Convert.ToInt32(dataTable.Rows[index][1])), dataManager.Parameter("inlistID", (object) Convert.ToInt32(dataTable.Rows[index][2])));
      }
    }
  }

  public DataTable GetObjectSnapshots(long id, string orderBy)
  {
    return this.GetSnapshots("F_ID", orderBy, id, false);
  }

  public DataTable GetObjectVersionSnapshots(long objectID, string orderBy)
  {
    return this.GetSnapshots("F_OBJECT_ID", orderBy, objectID, false);
  }

  public DataTable GetObjectSnapshotsEx(long id, string orderBy)
  {
    return this.GetSnapshots("F_ID", orderBy, id, true);
  }

  public DataTable GetObjectVersionSnapshotsEx(long objectID, string orderBy)
  {
    return this.GetSnapshots("F_OBJECT_ID", orderBy, objectID, true);
  }

  private DataTable GetSnapshots(string keyField, string orderBy, long keyID, bool exMode)
  {
    if (orderBy != string.Empty)
      orderBy = " ORDER BY " + orderBy;
    IDbManager dataManager = this.UserSession.DataManager;
    string str = (!exMode ? $"F_SNAPSHOT_ID, F_NAME, F_OBJECT_ID, F_ID, F_USER_ID, {dataManager.DataProvider.GetUTCSelect("F_SNAPSHOT_DATE", this.UserSession.TimeZoneOffset)} F_SNAPSHOT_DATE" : $"F_SNAPSHOT_ID, F_NAME, F_OBJECT_ID, F_ID, {dataManager.DataProvider.NVARCHARCast("F_USER_ID", 20, "IMS_OBJ_SNAPSHOT")}, {dataManager.DataProvider.GetUTCSelect("F_SNAPSHOT_DATE", this.UserSession.TimeZoneOffset)} F_SNAPSHOT_DATE") + ", (SELECT F_OBJECT_TYPE FROM IMS_OBJECTS WHERE IMS_OBJECTS.F_OBJECT_ID = IMS_SNAPSHOTS.F_OBJECT_ID) F_OBJECT_TYPE";
    DataTable snapshots = dataManager.ExecuteDataTable(string.Format("SELECT {2} FROM IMS_SNAPSHOTS WHERE {0} = :idPar AND F_USER_ID <> 0 {1}", (object) keyField, (object) orderBy, (object) str), dataManager.Parameter("idPar", (object) keyID));
    if (exMode)
    {
      for (int index = 0; index < snapshots.Rows.Count; ++index)
        snapshots.Rows[index]["F_USER_ID"] = this.GetObjectCaption(snapshots.Rows[index]["F_USER_ID"]);
      snapshots.AcceptChanges();
      foreach (DataColumn column in (InternalDataCollectionBase) snapshots.Columns)
      {
        object obj = DataSetProcessor.ColumnCaptions[(object) column.ColumnName] ?? (object) column.ColumnName;
        column.Caption = obj.ToString();
      }
    }
    return snapshots;
  }

  private object GetObjectCaption(object p)
  {
    if (p == null || p == DBNull.Value || !(p.ToString() != string.Empty))
      return p;
    long int64 = Convert.ToInt64(p);
    return int64 != 0L ? (object) this.UserSession.GetObjectInfo(int64).Caption : (object) string.Empty;
  }
}
