// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ScheduledTasks.RemoveBlobsTask
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Snapshots;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Services.ScheduledTasks;

internal class RemoveBlobsTask : DBCustomManualScheduledService
{
  private const string SectionName = "RemoveBlobs";
  private const string LastRemoveDateParam = "LastRemoveDate";
  private const string TraceFileName = "RemoveBlobs.log";
  private IBlobStoragesPool _storagesPool;

  public override Guid GUID => new Guid("cadd960a-306c-11d8-b4e9-00304f19f545");

  public override string ServiceName => "Перемещение двоичных данных между файловыми шкафами";

  private IBlobStoragesPool StoragesPool
  {
    get
    {
      if (this._storagesPool == null)
        this._storagesPool = ServerServices.GetService(typeof (IBlobStoragesPool)) as IBlobStoragesPool;
      return this._storagesPool;
    }
  }

  public override bool ProcessEvent(TimedEventProperties properties)
  {
    try
    {
      bool flag = this.StoragesPool.GetActiveStorageID((IUserSession) this.Session) != this.StoragesPool.SnapshotStorageID;
      DataRow[] dataRowArray = this.Session.DBCache.GetTable("IMS_LEVELS").Select("F_STORAGE_ID <> 0");
      int num = 0;
      if (dataRowArray.Length != 0)
      {
        this.Session.EventLogHelper.AddToTrace(sc_14184.ssp_appserver_14185(), Consts.traceAlways, "RemoveBlobs.log");
        Dictionary<int, long> dictionary = new Dictionary<int, long>(dataRowArray.Length);
        for (int index = 0; index < dataRowArray.Length; ++index)
          dictionary.Add(Convert.ToInt32(dataRowArray[index]["F_LEVEL_ID"]), Convert.ToInt64(dataRowArray[index]["F_STORAGE_ID"]));
        FieldTypes[] fldTypes = new FieldTypes[2]
        {
          FieldTypes.ftBlob,
          FieldTypes.ftFile
        };
        DBObjectType dbObjectType = (DBObjectType) null;
        DateTime dateTime = this.Session.Configurations.ReadDateTime("KERNEL", "RemoveBlobs", "LastRemoveDate", DateTime.UtcNow - TimeSpan.FromDays(10950.0), DBConfigMode.GlobalOnly);
        DateTime utcNow = DateTime.UtcNow;
        foreach (KeyValuePair<int, long> keyValuePair in dictionary)
        {
          DataTable dataTable = this.Session.DataManager.ExecuteDataTable("SELECT D.F_OBJECT_ID, O.F_OBJECT_TYPE FROM IMS_LCSTART_DATE D, IMS_LC_STEPS S, IMS_OBJECTS O WHERE D.F_START_DATE > :startDate AND S.F_LC_STEP = D.F_LC_STEP AND S.F_LEVEL_ID = :levID AND O.F_OBJECT_ID = D.F_OBJECT_ID ORDER BY O.F_OBJECT_TYPE", this.Session.DataManager.Parameter("startDate", (object) dateTime), this.Session.DataManager.Parameter("levID", (object) keyValuePair.Key));
          IBlobStorage storage = this.StoragesPool.GetStorage(keyValuePair.Value, (IUserSession) this.Session);
          try
          {
            this.Session.EventLogHelper.AddToTrace("Перемещение двоичных данных в файловый шкаф " + storage.StorageCaption, Consts.traceAlways, "RemoveBlobs.log");
            for (int index1 = 0; index1 < dataTable.Rows.Count; ++index1)
            {
              if (dbObjectType == null || dbObjectType.ObjectType != Convert.ToInt32(dataTable.Rows[index1][1]))
                dbObjectType = this.Session.GetObjectType(Convert.ToInt32(dataTable.Rows[index1][1])) as DBObjectType;
              if (dbObjectType.CanStoreAttributeByFiledType(fldTypes))
              {
                IDBObject dbObject = this.Session.GetObject(Convert.ToInt64(dataTable.Rows[index1][0]), false);
                if (dbObject != null)
                {
                  for (int AttrIndex = 0; AttrIndex < dbObject.Attributes.Count; ++AttrIndex)
                  {
                    IDBAttribute attribute = dbObject.Attributes[AttrIndex];
                    if (attribute.DataType == FieldTypes.ftBlob || attribute.DataType == FieldTypes.ftFile)
                    {
                      for (int index2 = 0; index2 < attribute.ValuesCount; ++index2)
                      {
                        attribute.Index = index2;
                        if (!attribute.IsNull && Convert.ToInt64(attribute.AsDouble) != keyValuePair.Value)
                        {
                          this.RemoveToStorage(attribute, storage);
                          ++num;
                        }
                      }
                      if ((dbObjectType.Options & ObjectTypeOptions.CreateSnapshots) == ObjectTypeOptions.CreateSnapshots && !flag)
                      {
                        DataTable tbl_snap = this.Session.DataManager.ExecuteDataTable("SELECT F_INTEGER_VALUE, F_DOUBLE_VALUE, F_SNAPSHOT_ID FROM IMS_OBJ_SNAPATTRS WHERE F_OBJECT_ID = :objID AND F_ATTRIBUTE_ID = :attrID AND F_DOUBLE_VALUE <> :storID", this.Session.DataManager.Parameter("objID", (object) dbObject.ObjectID), this.Session.DataManager.Parameter("storID", (object) keyValuePair.Value), this.Session.DataManager.Parameter("attrID", (object) attribute.AttributeID));
                        this.RemoveSnapshotsToStorage(tbl_snap, storage);
                        num += tbl_snap.Rows.Count;
                      }
                    }
                  }
                }
              }
            }
          }
          finally
          {
            this.StoragesPool.ReleaseStorage(storage);
          }
        }
        this.Session.Configurations.WriteDateTime("KERNEL", "RemoveBlobs", "LastRemoveDate", utcNow, 0L);
      }
      else
        this.Session.EventLogHelper.AddToTrace("Отсутствуют настройки файловых шкафов для уровней продвижения.", Consts.traceAlways, "RemoveBlobs.log");
      if (flag)
      {
        DataTable tbl_snap = this.Session.DataManager.ExecuteDataTable($"select F_INTEGER_VALUE, F_DOUBLE_VALUE, F_SNAPSHOT_ID from IMS_OBJ_SNAPATTRS WHERE F_ATTRIBUTE_ID IN (SELECT F_ATTRIBUTE_ID FROM IMS_ATTRIBUTES WHERE F_ATTRIBUTE_TYPE IN ({11}, {6})) AND F_DOUBLE_VALUE <> :storID", this.Session.DataManager.Parameter("storID", (object) this.StoragesPool.SnapshotStorageID));
        if (tbl_snap.Rows.Count > 0)
        {
          IBlobStorage storage = this.StoragesPool.GetStorage(this.StoragesPool.SnapshotStorageID, (IUserSession) this.Session);
          try
          {
            this.RemoveSnapshotsToStorage(tbl_snap, storage);
            num += tbl_snap.Rows.Count;
          }
          finally
          {
            this.StoragesPool.ReleaseStorage(storage);
          }
        }
      }
      else
        this.Session.EventLogHelper.AddToTrace("Отсутствуют настройки файловых шкафов для хранения итераций.", Consts.traceAlways, "RemoveBlobs.log");
      this.Session.EventLogHelper.AddToTrace("Задача завершена. Количество перемещенных двоичных данных: " + num.ToString(), Consts.traceAlways, "RemoveBlobs.log");
    }
    catch (Exception ex)
    {
      this.Session.EventLogHelper.AddToTrace($"Ошибка перемещения двоичных данных. Задача прервана с ошибкой {ex.Message}{Environment.NewLine}{ex.StackTrace}", Consts.traceAlways, "RemoveBlobs.log");
    }
    return true;
  }

  private void RemoveSnapshotsToStorage(DataTable tbl_snap, IBlobStorage destStorage)
  {
    for (int index = 0; index < tbl_snap.Rows.Count; ++index)
    {
      IBlobStorage storage = this.StoragesPool.GetStorage(Convert.ToInt64(tbl_snap.Rows[index][1]), (IUserSession) this.Session);
      try
      {
        FileInfoStruct fileStruct = storage.GetFileStruct(Convert.ToInt64(tbl_snap.Rows[index][0]), true);
        if (fileStruct.ObjectLinkID == Convert.ToInt64(tbl_snap.Rows[index][2]))
        {
          DBObjectSnapshot snapshot = this.Session.GetSnapshot(fileStruct.ObjectLinkID, false) as DBObjectSnapshot;
          this.Session.StartTransaction();
          try
          {
            destStorage.SetFileStruct(fileStruct);
            snapshot.ReplaceSnapStorageID("F_OBJECT_ID", "IMS_OBJ_SNAPATTRS", fileStruct.FileID, storage.StorageID, destStorage.StorageID);
            this.Session.Commit();
          }
          catch
          {
            this.Session.Rollback();
            throw;
          }
        }
      }
      finally
      {
        this.StoragesPool.ReleaseStorage(storage);
      }
    }
  }

  private void RemoveToStorage(IDBAttribute attr, IBlobStorage destStorage)
  {
    IBlobStorage storage = this.StoragesPool.GetStorage(Convert.ToInt64(attr.AsDouble), (IUserSession) this.Session);
    try
    {
      FileInfoStruct fileStruct = storage.GetFileStruct(attr.AsInteger, true);
      this.Session.StartTransaction();
      try
      {
        destStorage.SetFileStruct(fileStruct);
        (attr as DBAttribute).DirectSetValue("F_DOUBLE_VALUE", (object) destStorage.StorageID);
        storage.DeleteFile(attr.AsInteger);
        this.Session.Commit();
      }
      catch
      {
        this.Session.Rollback();
        throw;
      }
    }
    finally
    {
      this.StoragesPool.ReleaseStorage(storage);
    }
  }
}
