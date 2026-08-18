// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.BlobStoragesPool
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.CustomServices;
using Intermech.Interfaces.Server;
using Intermech.IO;
using Intermech.Kernel.Cache;
using Intermech.Kernel.Search;
using Intermech.Kernel.SystemFileStorages;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data;


namespace Intermech.Kernel;

public class BlobStoragesPool : LongLifeObject, IBlobStoragesPool, IBlobStoragesService, IDisposable
{
  internal FilesStorage FStorage;
  internal long _ActiveStorageID;
  private long _SnapshotStorageID;
  private ClearBlobsCacheTask _ClearBlobsTask;
  private ConcurrentDictionary<int, long> _Storage4Level;
  private ConcurrentDictionary<long, string> _OtherDBStorages;
  internal static bool DelayedPurge = true;
  private List<BlobStorageInfo> storageList;
  private ConcurrentDictionary<long, AttributeValues[]> _Storages = new ConcurrentDictionary<long, AttributeValues[]>();

  public IAppServerFilesCache FilesCache { get; private set; }

  public BlobStoragesPool()
  {
    this.FStorage = new FilesStorage(ConfigurationManager.AppSettings.Get("FilesCacheFolder"), "ServerFilesCache");
    try
    {
      this.FStorage.LockFolder();
    }
    catch (Exception ex)
    {
      (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AddToTrace(ex.Message);
      throw;
    }
    this.FilesCache = (IAppServerFilesCache) new AppServerFilesCache(this.FStorage);
    ServerServices.AddService(typeof (IAppServerFilesCache), (object) this.FilesCache);
  }

  public void RegisterClearCacheBlobs()
  {
    IDBTimedEvents service = ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    this._ClearBlobsTask = new ClearBlobsCacheTask(this);
    ClearBlobsCacheTask clearBlobsTask = this._ClearBlobsTask;
    service.RegisterService((object) clearBlobsTask);
  }

  public BlobStorageInfo[] GetStorages()
  {
    if (this.storageList == null)
    {
      IUserSession sessionTemporaryClone = (ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone(nameof (GetStorages));
      try
      {
        this.LoadStoragesList(sessionTemporaryClone);
      }
      finally
      {
        sessionTemporaryClone.Logout(nameof (GetStorages));
      }
    }
    return this.storageList.ToArray();
  }

  internal DataTable LoadStoragesList(IUserSession uSession)
  {
    IDBAttributeType attributeType = uSession.GetAttributeType(SystemGUIDs.attributeSnapshotStorage, false);
    DataTable dataTable = (uSession.GetObjectCollection(uSession.IdentHelper.StorageTypeID) as DBObjectCollection).Select(new DBRecordSetParams(-1)
    {
      Columns = new object[6]
      {
        (object) -2,
        (object) attributeType.AttributeID,
        (object) new Guid("cadd98bf-306c-11d8-b4e9-00304f19f545"),
        (object) new Guid("cad00015-306c-11d8-b4e9-00304f19f545"),
        (object) new Guid("cad00000-306c-11d8-b4e9-00304f19f545"),
        (object) -50
      },
      FailIfNotFound = true
    });
    List<BlobStorageInfo> blobStorageInfoList = new List<BlobStorageInfo>(dataTable.Rows.Count);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
      blobStorageInfoList.Add(new BlobStorageInfo(Convert.ToInt64(dataTable.Rows[index][0]), dataTable.Rows[index][5].ToString(), dataTable.Rows[index][4].ToString()));
    this.storageList = blobStorageInfoList;
    return dataTable;
  }

  internal void ValidateStorages(IUserSession uSession)
  {
    if (uSession == null)
      throw new ArgumentNullException(nameof (uSession));
    bool flag = ConfigurationManager.AppSettings.Get("SkipForeignStoragesCheck") != "1";
    try
    {
      foreach (DataRow row in (InternalDataCollectionBase) this.LoadStoragesList(uSession).Rows)
      {
        if (row[1] != DBNull.Value && Convert.ToInt64(row[1]) > 0L)
          this._SnapshotStorageID = Convert.ToInt64(row[0]);
        if (flag && row[3].ToString().Trim() != string.Empty && row[4].ToString() != "Intermech Document Server" && row[4].ToString() != "Файловая система")
        {
          if (row[2].ToString().Trim() == string.Empty)
          {
            uSession.GetObject(Convert.ToInt64(row[0])).Attributes.AddAttribute(uSession.IdentHelper.GetAttributeID("cadd98bf-306c-11d8-b4e9-00304f19f545"), false, new object[1]
            {
              (object) ServerConsts.ShortenedConnectionString
            });
          }
          else
          {
            string upper = row[2].ToString().Trim().ToUpper();
            long int64 = Convert.ToInt64(row[0]);
            if (ServerConsts.ShortenedConnectionString != upper)
            {
              if (this._OtherDBStorages == null)
                this._OtherDBStorages = new ConcurrentDictionary<long, string>();
              this._OtherDBStorages.TryAdd(int64, upper);
              (uSession as UserSession).EventLogHelper.AddToTrace($"Файловый шкаф {uSession.GetObject(int64).Caption} не может быть использован, т.к. с ним работает сервер, подключенный к базе данных {upper}. Данный сервер приложений работает с базой {ServerConsts.ShortenedConnectionString}", Intermech.Consts.traceAlways, string.Empty);
            }
          }
        }
      }
    }
    catch
    {
      DataTable dataTable = (uSession as UserSession).DataManager.ExecuteDataTable("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = " + uSession.IdentHelper.StorageTypeID.ToString());
      if (dataTable.Rows.Count > 0)
        this._SnapshotStorageID = Convert.ToInt64(dataTable.Rows[0][0]);
    }
    if (this._SnapshotStorageID != 0L)
      return;
    this._SnapshotStorageID = this.GetActiveStorageID(uSession);
  }

  public long SnapshotStorageID => this._SnapshotStorageID;

  public IBlobStorage GetStorage(long StorageID, IUserSession UsrSession)
  {
    return this.GetStorageInternal(StorageID, UsrSession as UserSession);
  }

  public void ClearCache() => this._Storages.Clear();

  private IBlobStorage GetStorageInternal(long StorageID, UserSession UsrSession)
  {
    string str;
    if (this._OtherDBStorages != null && this._OtherDBStorages.TryGetValue(StorageID, out str))
      throw new KernelExceptionID(453, (object) StorageID, (object) str, (object) ServerConsts.ShortenedConnectionString);
    IBlobStorage storage = UsrSession.StoragesList.GetStorage(StorageID);
    if (storage == null)
    {
      AttributeValues[] attributesValues;
      if (!this._Storages.TryGetValue(StorageID, out attributesValues))
      {
        attributesValues = UsrSession.GetObject(StorageID).GetAttributesValues(GetAttributeValuesModes.IncludeGuid | GetAttributeValuesModes.IncludeObligatoryAttributes);
        this._Storages.TryAdd(StorageID, attributesValues);
      }
      AttributeValues attributeByGuid = AttributeValuesHelper.GetAttributeByGuid(attributesValues, new Guid("cad00000-306c-11d8-b4e9-00304f19f545"), StorageID, true);
      storage = !(attributeByGuid.Values[0].ToString() == "Intermech Document Server") ? (!(attributeByGuid.Values[0].ToString() == "Файловая система") ? (IBlobStorage) new DBBlobStorage(attributesValues, UsrSession, this) : (IBlobStorage) new FileSystemBlobStorage(attributesValues, UsrSession, this)) : (IBlobStorage) new DiskBlobStorage(attributesValues, UsrSession, this);
      if (UsrSession.DataManager.InTransaction)
        storage.StartTransaction();
      UsrSession.StoragesList.Add(storage);
    }
    return storage;
  }

  internal void InitLevels(IUserSession session)
  {
    if (this._Storage4Level == null)
      this._Storage4Level = new ConcurrentDictionary<int, long>();
    else
      this._Storage4Level.Clear();
    DataTable dataTable = session.GetLifecycleLevelCollection().Select(string.Empty);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      long int64 = Convert.ToInt64(dataTable.Rows[index]["F_STORAGE_ID"]);
      if (int64 > 0L)
        this._Storage4Level[Convert.ToInt32(dataTable.Rows[index]["F_LEVEL_ID"])] = int64;
    }
  }

  public long GetActiveStorage4LevelID(IUserSession session, int levelID)
  {
    if (this._Storage4Level == null)
      this.InitLevels(session);
    long num;
    return this._Storage4Level.TryGetValue(levelID, out num) ? num : 0L;
  }

  public long GetActiveStorageID(IUserSession UsrSession)
  {
    long userStorageId = (UsrSession as UserSession).UserStorageID;
    if (userStorageId > 0L)
      return userStorageId;
    if (this._ActiveStorageID == 0L)
    {
      UserSession userSession = UsrSession as UserSession;
      DataTable dataTable = (userSession.GetObjectCollection(userSession.IdentHelper.StorageTypeID) as DBObjectCollection).Select(new DBRecordSetParams(-1)
      {
        Columns = new object[2]
        {
          (object) -2,
          (object) new Guid("cad00032-306c-11d8-b4e9-00304f19f545")
        },
        FailIfNotFound = true
      });
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (this._ActiveStorageID == 0L || dataTable.Rows.Count > 1 && row[1] != DBNull.Value && Convert.ToInt64(row[1]) > 0L)
          this._ActiveStorageID = Convert.ToInt64(row[0]);
      }
    }
    return this._ActiveStorageID;
  }

  public event GetStorageIDHandler GetStorageIDEvent;

  public long GetStorageID4Object(IUserSession UsrSession, IDBObject obj)
  {
    if (obj == null)
      return this.GetActiveStorageID(UsrSession);
    long activeStorageID = this.GetActiveStorageID(UsrSession);
    long activeStorage4LevelId = this.GetActiveStorage4LevelID(UsrSession, (obj as IDBLifecycleLevel).LevelID);
    if (this.GetStorageIDEvent != null)
    {
      GetStorageIDEventArgs args = new GetStorageIDEventArgs(obj, UsrSession, activeStorageID);
      this.GetStorageIDEvent(args);
      if (args.StorageID > 0L)
        activeStorageID = args.StorageID;
      else if (activeStorage4LevelId > 0L)
        activeStorageID = activeStorage4LevelId;
    }
    else if (activeStorage4LevelId > 0L)
      activeStorageID = activeStorage4LevelId;
    return activeStorageID;
  }

  public void Dispose()
  {
  }

  private void CurrentDomain_ProcessExit(object sender, EventArgs e) => this.Dispose();

  public int ReleaseStorage(IBlobStorage Storage)
  {
    Storage.Release();
    return 0;
  }
}
