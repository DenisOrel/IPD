// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBStorageObject
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Objects;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.Misc;
using Intermech.Kernel.Search;
using Intermech.Kernel.Snapshots;
using Intermech.Kernel.SystemFileStorages;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;


namespace Intermech.Kernel;

internal class DBStorageObject(UserSession uSession, DataTable objectParams) : 
  DBObject(uSession, objectParams),
  IBlobStorageObject,
  IDBRecords,
  IDBSessionable,
  IPerformer
{
  internal bool CanSetActiveStorage;

  protected override void DoDelete()
  {
    IDBAttribute attributeByGuid = this.GetAttributeByGuid(new Guid("cad00032-306c-11d8-b4e9-00304f19f545"), false);
    if (attributeByGuid != null && attributeByGuid.AsBoolean)
      throw new KernelExceptionID(sc_13009.ssp_appserver_13010(2069787675), (object) this.Caption);
    List<string> objectAttrsTables = this.UserSession.DBCache.GetObjectAttrsTables();
    objectAttrsTables.Add("IMS_RELATION_ATTRS");
    objectAttrsTables.Add("IMS_OBJ_SNAPATTRS");
    objectAttrsTables.Add("IMS_REL_SNAPATTRS");
    for (int index = 0; index < objectAttrsTables.Count; ++index)
    {
      string str = $"SELECT * FROM {objectAttrsTables[index]} O WHERE O.F_ATTRIBUTE_ID = A.F_ATTRIBUTE_ID AND O.F_DOUBLE_VALUE = {this.ObjectID}";
      if (Convert.ToInt32(this.UserSession.DataManager.ExecuteScalar(string.Format(sc_13009.ssp_appserver_13011(), (object) Convert.ToInt32((object) FieldTypes.ftFile), (object) Convert.ToInt32((object) FieldTypes.ftBlob), (object) str))) > 0)
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_13009.ssp_appserver_13012()), (object) this.ObjectName));
    }
    string caption = this.Caption;
    base.DoDelete();
    try
    {
      BlobStoragesPool service = ServerServices.GetService(typeof (IBlobStoragesPool)) as BlobStoragesPool;
      IBlobStorage storage = service.GetStorage(this.ObjectID, (IUserSession) this.UserSession);
      try
      {
        storage.DeleteStorage();
        service._ActiveStorageID = 0L;
      }
      finally
      {
        service.ReleaseStorage(storage);
      }
    }
    catch (Exception ex)
    {
      this.UserSession.EventLogHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_986"), (object) caption, (object) ex.Message), Consts.traceAlways, string.Empty);
    }
  }

  protected override void DoAfterSetAdditionalAttributeValue(IDBAttribute attribute)
  {
    BlobStoragesPool service = ServerServices.GetService(typeof (IBlobStoragesPool)) as BlobStoragesPool;
    if (this.UserSession.GetAttributeType(new Guid("cad00032-306c-11d8-b4e9-00304f19f545")).AttributeID == attribute.AttributeID)
    {
      if (attribute.AsBoolean)
      {
        this.ClearActiveFlag();
        service._ActiveStorageID = this.ObjectID;
        (UserSession.Sessions as UserSessionCollection).ClearActiveStorageID(0L);
      }
      else if (!this.IsCreationMode && !this.CanSetActiveStorage)
        throw new KernelExceptionID(263, (object) this.Caption);
    }
    service.ClearCache();
    base.DoAfterSetAdditionalAttributeValue(attribute);
  }

  protected override void DoCommitCreation()
  {
    IDBAttribute byGuid = this.Attributes.FindByGUID(new Guid("cad00000-306c-11d8-b4e9-00304f19f545"));
    if (byGuid == null)
      throw new KernelExceptionID(sc_13009.ssp_appserver_13013(2036177692));
    if (byGuid.IsNull || byGuid.AsString == string.Empty)
      byGuid.AsString = this.UserSession.DataManager.DataProvider.GetStorageType();
    BlobStoragesPool service = ServerServices.GetService(typeof (IBlobStoragesPool)) as BlobStoragesPool;
    BlobStorage blobStorage = !(byGuid.AsString == "Intermech Document Server") ? (!(byGuid.AsString == "Файловая система") ? (BlobStorage) new DBBlobStorage(this.GetAttributesValues(GetAttributeValuesModes.IncludeGuid | GetAttributeValuesModes.IncludeObligatoryAttributes), this.UserSession, service) : (BlobStorage) new FileSystemBlobStorage(this.GetAttributesValues(GetAttributeValuesModes.IncludeGuid | GetAttributeValuesModes.IncludeObligatoryAttributes), this.UserSession, service)) : (BlobStorage) new DiskBlobStorage(this.GetAttributesValues(GetAttributeValuesModes.IncludeGuid | GetAttributeValuesModes.IncludeObligatoryAttributes), this.UserSession, service);
    using (blobStorage)
    {
      blobStorage.ValidateNewStorage((IUserSession) this.UserSession);
      IDbManager dataManager = blobStorage.DataManager;
      string storageName = blobStorage.StorageName;
      dataManager.DataProvider.CreateFileStorage(storageName, dataManager);
      dataManager.DataProvider.CreateIndex(storageName, "F_FILENAME", dataManager, SortOrders.ASC);
      dataManager.DataProvider.CreateIndex(storageName, "F_FILEDATE", dataManager, SortOrders.ASC);
      dataManager.DataProvider.CreateIndex(storageName, "F_FILESIZE", dataManager, SortOrders.ASC);
      dataManager.DataProvider.CreateIndex(storageName, "F_OBJECTLINK_ID", dataManager, SortOrders.ASC);
      dataManager.DataProvider.CreateIndex(storageName, "F_NOTE", dataManager, SortOrders.ASC);
      dataManager.DataProvider.CreateIndex(storageName, "F_ATTRIBUTE_ID", dataManager, SortOrders.ASC);
    }
    this.ClearActiveFlag();
    base.DoCommitCreation();
    service._ActiveStorageID = this.ObjectID;
    service.LoadStoragesList((IUserSession) this.UserSession);
  }

  private void ClearActiveFlag()
  {
    foreach (DataRow row in (InternalDataCollectionBase) this.UserSession.GetObjectCollection(this.UserSession.IdentHelper.StorageTypeID).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
    {
      (object) -2,
      (object) new Guid("cad00032-306c-11d8-b4e9-00304f19f545")
    })).Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      if (int64 != this.ObjectID && row[1].ToString() != string.Empty && Convert.ToInt64(row[1]) != 0L && this.UserSession.GetObject(int64) is DBStorageObject dbStorageObject)
      {
        dbStorageObject.CanSetActiveStorage = true;
        dbStorageObject.GetAttributeByGuid(new Guid("cad00032-306c-11d8-b4e9-00304f19f545")).AsBoolean = false;
      }
    }
  }

  public FileStorageInfo GetFileStorageInfo()
  {
    IBlobStoragesPool service = ServerServices.GetService(typeof (IBlobStoragesPool)) as IBlobStoragesPool;
    IBlobStorage storage = service.GetStorage(this.ObjectID, (IUserSession) this.UserSession);
    try
    {
      long int64_1;
      long int64_2;
      long int64_3;
      if (storage is DiskBlobStorage)
      {
        DataTable storageInfo = (storage as DiskBlobStorage).GetStorageInfo();
        int64_1 = Convert.ToInt64(storageInfo.Rows[0][0]);
        int64_2 = Convert.ToInt64(storageInfo.Rows[0][1]);
        int64_3 = Convert.ToInt64(storageInfo.Rows[0][2]);
      }
      else
      {
        DataTable dataTable = storage.DataManager.ExecuteDataTable(sc_13009.ssp_appserver_13014() + storage.StorageName);
        int64_1 = Convert.ToInt64(dataTable.Rows[0][0]);
        int64_3 = dataTable.Rows[0][1] != DBNull.Value ? Convert.ToInt64(dataTable.Rows[0][1]) : 0L;
        int64_2 = dataTable.Rows[0][2] != DBNull.Value ? Convert.ToInt64(dataTable.Rows[0][2]) : 0L;
      }
      return new FileStorageInfo(int64_1, int64_3, int64_2);
    }
    finally
    {
      service.ReleaseStorage(storage);
    }
  }

  private bool InternalRemoveFiles(long[] fileIDs, long toStorageID, IDBAttribute sender)
  {
    IBlobStoragesPool service = ServerServices.GetService(typeof (IBlobStoragesPool)) as IBlobStoragesPool;
    IBlobStorage storage1 = service.GetStorage(toStorageID, (IUserSession) this.UserSession);
    try
    {
      IBlobStorage storage2 = service.GetStorage(this.ObjectID, (IUserSession) this.UserSession);
      try
      {
        for (int index1 = 0; index1 < fileIDs.Length; ++index1)
        {
          FileInfoStruct fileStruct = storage2.GetFileStruct(fileIDs[index1], true);
          this.UserSession.StartTransaction();
          try
          {
            bool flag1 = false;
            if (sender != null)
            {
              (sender as DBAttribute).DirectSetValue("F_DOUBLE_VALUE", (object) toStorageID);
              flag1 = true;
            }
            else
            {
              IDBAttributable dbAttributable = (IDBAttributable) this.UserSession.GetObject(fileStruct.ObjectLinkID, false) ?? (IDBAttributable) this.UserSession.GetObject(-fileStruct.ObjectLinkID, false) ?? (IDBAttributable) this.UserSession.GetRelation(fileStruct.ObjectLinkID, false);
              if (dbAttributable != null)
              {
                for (int AttrIndex = 0; AttrIndex < dbAttributable.Attributes.Count; ++AttrIndex)
                {
                  if (dbAttributable.Attributes[AttrIndex].AttributeType.AttributeType == FieldTypes.ftBlob || dbAttributable.Attributes[AttrIndex].AttributeType.AttributeType == FieldTypes.ftFile)
                  {
                    bool flag2 = false;
                    for (int index2 = 0; index2 < dbAttributable.Attributes[AttrIndex].ValuesCount; ++index2)
                    {
                      dbAttributable.Attributes[AttrIndex].Index = index2;
                      if (dbAttributable.Attributes[AttrIndex].AsInteger == fileIDs[index1])
                      {
                        (dbAttributable.Attributes[AttrIndex] as DBAttribute).DirectSetValue("F_DOUBLE_VALUE", (object) toStorageID);
                        flag1 = true;
                        flag2 = true;
                        break;
                      }
                    }
                    if (flag2)
                      break;
                  }
                }
              }
              else if (this.UserSession.GetSnapshot(fileStruct.ObjectLinkID, false) is DBObjectSnapshot snapshot)
              {
                if (!snapshot.ReplaceSnapStorageID("F_OBJECT_ID", "IMS_OBJ_SNAPATTRS", fileIDs[index1], this.ObjectID, toStorageID))
                  snapshot.ReplaceSnapStorageID("F_PRJLINK_ID", "IMS_REL_SNAPATTRS", fileIDs[index1], this.ObjectID, toStorageID);
                flag1 = true;
              }
            }
            if (flag1)
            {
              storage1.SetFileStruct(fileStruct);
              storage2.DeleteFile(fileIDs[index1]);
            }
            this.UserSession.Commit();
          }
          catch
          {
            this.UserSession.Rollback();
            throw;
          }
        }
      }
      finally
      {
        service.ReleaseStorage(storage2);
      }
    }
    finally
    {
      service.ReleaseStorage(storage1);
    }
    return true;
  }

  public bool RemoveFiles(long[] fileIDs, long toStorageID)
  {
    this.CheckAccess(ActionType.Edit);
    (this.UserSession.GetObject(toStorageID) as IDBSecurity).CheckAccess(ActionType.Edit);
    return this.InternalRemoveFiles(fileIDs, toStorageID, (IDBAttribute) null);
  }

  public void Perform1(long[] fileIDs, long toStor, IDBAttribute sender)
  {
    this.InternalRemoveFiles(fileIDs, toStor, sender);
  }

  public DataTable GetObjectHistory(long id)
  {
    IBlobStoragesPool service = ServerServices.GetService(typeof (IBlobStoragesPool)) as IBlobStoragesPool;
    IBlobStorage storage = service.GetStorage(this.ObjectID, (IUserSession) this.UserSession);
    try
    {
      return (storage as DiskBlobStorage).GetObjectHistory(id);
    }
    finally
    {
      service.ReleaseStorage(storage);
    }
  }

  public DataTable GetVersionHistory(long id)
  {
    IBlobStoragesPool service = ServerServices.GetService(typeof (IBlobStoragesPool)) as IBlobStoragesPool;
    IBlobStorage storage = service.GetStorage(this.ObjectID, (IUserSession) this.UserSession);
    DataTable versionHistory = (DataTable) null;
    try
    {
      if (storage is DiskBlobStorage)
        versionHistory = (storage as DiskBlobStorage).GetVersionFilesList(id);
    }
    finally
    {
      service.ReleaseStorage(storage);
    }
    return versionHistory;
  }

  public DataTable GetFileHistory(string fileName, long objectID)
  {
    IBlobStoragesPool service = ServerServices.GetService(typeof (IBlobStoragesPool)) as IBlobStoragesPool;
    IBlobStorage storage = service.GetStorage(this.ObjectID, (IUserSession) this.UserSession);
    DataTable fileHistory = (DataTable) null;
    try
    {
      if (storage is DiskBlobStorage)
        fileHistory = (storage as DiskBlobStorage).GetHistoryForFile(fileName, objectID);
    }
    finally
    {
      service.ReleaseStorage(storage);
    }
    return fileHistory;
  }

  public DataTable GetFileHistory(long blobID, long objectID)
  {
    IBlobStoragesPool service = ServerServices.GetService(typeof (IBlobStoragesPool)) as IBlobStoragesPool;
    IBlobStorage storage = service.GetStorage(this.ObjectID, (IUserSession) this.UserSession);
    DataTable fileHistory = (DataTable) null;
    try
    {
      if (storage is DiskBlobStorage)
        fileHistory = (storage as DiskBlobStorage).GetHistoryForFile(blobID, objectID);
    }
    finally
    {
      service.ReleaseStorage(storage);
    }
    return fileHistory;
  }

  public DataTable Select(DBRecordSetParams paramSet)
  {
    this.CheckAccess(ActionType.View);
    IBlobStoragesPool service = ServerServices.GetService(typeof (IBlobStoragesPool)) as IBlobStoragesPool;
    IBlobStorage storage = service.GetStorage(this.ObjectID, (IUserSession) this.UserSession);
    try
    {
      return new DBStorageFilesCollection(this.UserSession, -1, storage.StorageName, storage.DataManager).Select(paramSet);
    }
    finally
    {
      service.ReleaseStorage(storage);
    }
  }

  public DataTable SelectWithDescriptions(DBRecordSetParams paramSet)
  {
    throw new OperationNotApplicableException();
  }

  public int Delete(long[] idList, bool throwException, long deleteMode)
  {
    throw new OperationNotApplicableException();
  }

  public bool RecordsExists(ConditionStructure[] conditions, HybridDictionary tags)
  {
    this.CheckAccess(ActionType.View);
    IBlobStoragesPool service = ServerServices.GetService(typeof (IBlobStoragesPool)) as IBlobStoragesPool;
    IBlobStorage storage = service.GetStorage(this.ObjectID, (IUserSession) this.UserSession);
    try
    {
      return new DBStorageFilesCollection(this.UserSession, -1, storage.StorageName, storage.DataManager).RecordsExists(conditions, tags);
    }
    finally
    {
      service.ReleaseStorage(storage);
    }
  }

  public bool RecordsExists(ConditionStructure[] conditions)
  {
    return this.RecordsExists(conditions, (HybridDictionary) null);
  }
}
