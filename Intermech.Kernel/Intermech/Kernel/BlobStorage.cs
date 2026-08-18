// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.BlobStorage
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.IO;
using Intermech.Localization;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;


namespace Intermech.Kernel;

internal abstract class BlobStorage : IBlobStorage, IDisposable
{
  protected BlobStoragesPool StoragesPool;
  private bool _Locked = true;
  private long _StorageID;
  protected IDbManager _dbManager;
  protected ArcMethods _ArcMethod;
  protected int _BufferSize;
  protected string _StorageName = "IMS_STORAGE";
  protected string _StorageCaption;
  public FilesStorage iStoreFile;
  protected UserSession _Session;
  private int _MaxStorageSize;
  protected bool _ExternalDbManager = true;

  public BlobStorage(
    AttributeValues[] StorageObject,
    UserSession session,
    BlobStoragesPool storagesPool)
  {
    this.StoragesPool = storagesPool;
    this._StorageID = AttributeValuesHelper.GetAttributeByID(StorageObject, -2, 0L, true).AsInteger;
    this._Session = session;
    this.iStoreFile = storagesPool.FStorage;
    AttributeValues attributeByGuid1 = AttributeValuesHelper.GetAttributeByGuid(StorageObject, new Guid("cad00028-306c-11d8-b4e9-00304f19f545"), this._StorageID, false);
    if (attributeByGuid1 != null)
      this._StorageName = attributeByGuid1.AsString;
    this._StorageCaption = AttributeValuesHelper.GetAttributeByID(StorageObject, -50, this._StorageID, true).AsString;
    AttributeValues attributeByGuid2 = AttributeValuesHelper.GetAttributeByGuid(StorageObject, new Guid("cad00027-306c-11d8-b4e9-00304f19f545"), this._StorageID, false);
    this._BufferSize = attributeByGuid2 == null ? Intermech.Consts.BlobTransferBufferLength : Convert.ToInt32(attributeByGuid2.AsInteger);
    AttributeValues attributeByGuid3 = AttributeValuesHelper.GetAttributeByGuid(StorageObject, new Guid("cad00026-306c-11d8-b4e9-00304f19f545"), this._StorageID, false);
    if (attributeByGuid3 != null)
    {
      int enumValue = (int) EnumDescConverter.GetEnumValue(typeof (ArcMethods), attributeByGuid3.AsString, (object) this._ArcMethod);
    }
    AttributeValues attributeByGuid4 = AttributeValuesHelper.GetAttributeByGuid(StorageObject, SystemGUIDs.attributeMaxStorageSize, this._StorageID, false);
    if (attributeByGuid4 == null)
      return;
    this._MaxStorageSize = Convert.ToInt32(attributeByGuid4.AsInteger);
  }

  protected void InitDataManager(
    string dbPath,
    AttributeValues[] StorageObject,
    UserSession session)
  {
    string newUserID = ConfigurationManager.AppSettings.Get("User ID");
    string newPassword = Cryptor.Decrypt(ConfigurationManager.AppSettings.Get("Password"), "cad00016-306c-11d8-b4e9-00304f19f545");
    string asString1 = AttributeValuesHelper.GetAttributeByGuid(StorageObject, new Guid("cad00000-306c-11d8-b4e9-00304f19f545"), this.StorageID, true).AsString;
    try
    {
      if (dbPath == string.Empty)
        dbPath = this.GetMainDatabaseConnectionString();
      dbPath = this.ModifyConnectionString(dbPath, newUserID, newPassword);
      string providerName;
      switch (asString1)
      {
        case "MS SQL Server":
          providerName = "Sql";
          break;
        case "Oracle":
          providerName = "Oracle";
          break;
        case "Linter":
          providerName = "Linter";
          break;
        case "PostgreSQL":
          providerName = "PostgreSQL";
          break;
        case "Файловая система":
        case "Intermech Document Server":
          providerName = session.DataManager.DataProvider.Name;
          break;
        default:
          throw new KernelException(string.Format(sc_13000.ssp_appserver_13001(), (object) asString1));
      }
      this._dbManager = (ServerServices.GetService(typeof (IDbManagerService)) as IDbManagerService).CreateDbManager(providerName, dbPath);
      this._ExternalDbManager = true;
    }
    catch (Exception ex)
    {
      string asString2 = AttributeValuesHelper.GetAttributeByID(StorageObject, -50, this.StorageID, true).AsString;
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_13000.ssp_appserver_13002()), (object) asString2, (object) ex.Message));
    }
  }

  private string GetMainDatabaseConnectionString()
  {
    return ((IConnectionStringService) ServerServices.GetService(typeof (IConnectionStringService))).DefaultConnectionString;
  }

  private string ModifyConnectionString(
    string connectionString,
    string newUserID,
    string newPassword)
  {
    DbConnectionStringBuilder builder = new DbConnectionStringBuilder();
    builder.ConnectionString = connectionString;
    return (0 | (this.TryModifyConnectionString(builder, "User ID", newUserID) ? 1 : 0) | (this.TryModifyConnectionString(builder, "Password", newPassword) ? 1 : 0)) == 0 ? connectionString : builder.ToString();
  }

  private bool TryModifyConnectionString(
    DbConnectionStringBuilder builder,
    string key,
    string value)
  {
    if (builder.ContainsKey(key) && object.Equals(builder[key], (object) value))
      return false;
    builder[key] = (object) value;
    return true;
  }

  public string StorageName => this._StorageName;

  public string StorageCaption => this._StorageCaption;

  public IDbManager DataManager => this._dbManager;

  public int MaxStorageSize => this._MaxStorageSize;

  public long StorageID => this._StorageID;

  public abstract FileInfoStruct GetFileStruct(long fileID, bool readFileBody);

  public virtual FileInfoStruct GetFileStruct(long fileID) => this.GetFileStruct(fileID, true);

  protected virtual string GetAdditionalColumns()
  {
    return this._dbManager.DataProvider.Name == "PostgreSQL" ? ", F_OID" : string.Empty;
  }

  protected virtual FileInfoStruct InternalGetFileInfo(long fileID, out DataTable tbl)
  {
    string str = "F_FILE_ID, F_FILENAME, F_FILESIZE, F_FILEDATE, F_ARC_METHOD, F_OBJECTLINK_ID, F_ZIPSIZE, F_NOTE, F_ATTRIBUTE_ID, F_AUTHOR, F_LINKTYPE";
    tbl = this._dbManager.ExecuteDataTable($"SELECT {str}{this.GetAdditionalColumns()} FROM {this._StorageName} WHERE F_FILE_ID = :fID", this._dbManager.Parameter("fID", (object) fileID));
    if (tbl.Rows.Count == 0)
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_13000.ssp_appserver_13003()), (object) fileID, (object) this._StorageName));
    FileInfoStruct fileInfo = new FileInfoStruct();
    fileInfo.FileID = fileID;
    fileInfo.ArcMethod = (ArcMethods) Convert.ToInt32(tbl.Rows[0]["F_ARC_METHOD"]);
    object obj1 = tbl.Rows[0]["F_FILENAME"];
    fileInfo.FileName = obj1 != DBNull.Value ? Convert.ToString(obj1) : "";
    object obj2 = tbl.Rows[0]["F_FILEDATE"];
    fileInfo.ModifyDate = obj2 != DBNull.Value ? Convert.ToDateTime(obj2) : DateTime.MinValue;
    fileInfo.PacketFileSize = Convert.ToInt64(tbl.Rows[0]["F_ZIPSIZE"]);
    fileInfo.RealFileSize = Convert.ToInt64(tbl.Rows[0]["F_FILESIZE"]);
    fileInfo.ObjectLinkID = Convert.ToInt64(tbl.Rows[0]["F_OBJECTLINK_ID"]);
    fileInfo.AttributeID = Convert.ToInt32(tbl.Rows[0]["F_ATTRIBUTE_ID"]);
    fileInfo.Author = Convert.ToInt64(tbl.Rows[0]["F_AUTHOR"]);
    fileInfo.FileType = (FileTypes) Convert.ToInt32(tbl.Rows[0]["F_LINKTYPE"]);
    object obj3 = tbl.Rows[0]["F_NOTE"];
    fileInfo.Note = obj3 != DBNull.Value ? Convert.ToString(obj3) : "";
    return fileInfo;
  }

  public abstract bool SetNewFileStruct(FileInfoStruct fileStruct);

  public abstract void SetFileStruct(FileInfoStruct fileStruct);

  public virtual void ValidateNewStorage(IUserSession session)
  {
  }

  public virtual void CopyToTemporaryFile(FileInfoStruct fs)
  {
  }

  public virtual void PrepareTemporaryFile(FileInfoStruct fileStruct)
  {
    fileStruct.FileBody = (Stream) null;
    string str = fileStruct.GetIsolatedFileName(this.iStoreFile);
    if (!this.iStoreFile.FileExists(str))
    {
      fileStruct.FileBody = (Stream) new FileStream(str, File.Exists(str) ? FileMode.Truncate : FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None);
    }
    else
    {
      int num = 0;
      while (fileStruct.FileBody == null)
      {
        try
        {
          if (num > 0)
          {
            str = fileStruct.GetIsolatedFileName(this.iStoreFile) + num.ToString();
            fileStruct.FileBody = (Stream) new FileStream(str, File.Exists(str) ? FileMode.Truncate : FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None);
            fileStruct.IsolatedFileName = str;
          }
          else
            fileStruct.FileBody = (Stream) new FileStream(str, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
        }
        catch
        {
          if (num++ > 200)
            throw;
        }
      }
    }
    this.StoragesPool.FilesCache.AddFile(str);
  }

  protected virtual void InsertFileInfo(FileInfoStruct fileStruct)
  {
    this._dbManager.ExecuteNonQuery($"INSERT INTO {this._StorageName} (F_FILE_ID, F_FILENAME, F_FILEBODY, F_FILESIZE, F_FILEDATE, F_ARC_METHOD, F_ZIPSIZE, F_OBJECTLINK_ID, F_NOTE, F_ATTRIBUTE_ID, F_AUTHOR, F_LINKTYPE) VALUES (:fID, :fname, {this._dbManager.DataProvider.NullBlobStr}, 0, :fdate, 0, 0, :objID, :notes, :attrID, :authr, :linktype)", this._dbManager.Parameter("fID", (object) fileStruct.FileID), this._dbManager.Parameter("fname", (object) fileStruct.FileName), this._dbManager.Parameter("objID", (object) fileStruct.ObjectLinkID), this._dbManager.Parameter("notes", (object) fileStruct.Note), this._dbManager.Parameter("fdate", (object) fileStruct.ModifyDate), this._dbManager.Parameter("attrID", (object) fileStruct.AttributeID), this._dbManager.Parameter("authr", (object) fileStruct.Author), this._dbManager.Parameter("linktype", (object) Convert.ToInt32((object) fileStruct.FileType)));
  }

  public virtual void DeleteFile(long fileID)
  {
    this._dbManager.ExecuteNonQuery($"DELETE FROM {this._StorageName} WHERE F_FILE_ID = :fileID", this._dbManager.Parameter(nameof (fileID), (object) fileID));
  }

  public virtual void DeleteTemporaryData()
  {
  }

  public virtual DataTable GetObjectFilesList(long objectID)
  {
    return this._dbManager.ExecuteDataTable($"SELECT * FROM {this._StorageName} WHERE F_OBJECTLINK_ID = {objectID}");
  }

  public virtual void StartTransaction()
  {
    if (!this._ExternalDbManager || this._dbManager.InTransaction)
      return;
    this._dbManager.BeginTransaction();
  }

  public virtual void Commit()
  {
    if (!this._ExternalDbManager || !this._dbManager.InTransaction)
      return;
    this._dbManager.Commit();
  }

  public virtual void Rollback()
  {
    if (!this._ExternalDbManager || !this._dbManager.InTransaction)
      return;
    this._dbManager.Rollback();
  }

  public virtual void ChangeAttributeID(int attrID, int toAttrID)
  {
    this._dbManager.ExecuteNonQuery($"UPDATE {this._StorageName} SET F_ATTRIBUTE_ID = :toAttrID WHERE F_ATTRIBUTE_ID = :attrID", this._dbManager.Parameter(nameof (toAttrID), (object) toAttrID), this._dbManager.Parameter(nameof (attrID), (object) attrID));
  }

  public virtual void DeleteStorage()
  {
    object obj;
    try
    {
      obj = this._dbManager.ExecuteScalar("SELECT COUNT(*) FROM " + this._StorageName);
    }
    catch
    {
      obj = (object) 0;
    }
    if (Convert.ToInt32(obj) > 0)
      throw new KernelExceptionID(sc_13000.ssp_appserver_13004(32370988), (object) this._StorageName, obj);
    (UserSession.Sessions as UserSessionCollection).ClearActiveStorageID(this.StorageID);
    try
    {
      this._dbManager.ExecuteNonQuery("DROP TABLE " + this._StorageName);
    }
    catch
    {
    }
  }

  public virtual void ChangeObjectLinkID(long fileID, long toID)
  {
    this._dbManager.ExecuteNonQuery($"UPDATE {this._StorageName} SET F_OBJECTLINK_ID = :toID WHERE F_FILE_ID = :fileID", this._dbManager.Parameter(nameof (toID), (object) toID), this._dbManager.Parameter(nameof (fileID), (object) fileID));
  }

  public abstract void Clear(long blobID);

  protected bool IsBlobNotModified(FileInfoStruct fileStruct, DataRow row)
  {
    object obj = row["F_FILEDATE"];
    DateTime dateTime = obj != DBNull.Value ? Convert.ToDateTime(obj) : DateTime.MinValue;
    return fileStruct.FileBody != null && fileStruct.RealFileSize == Convert.ToInt64(row["F_FILESIZE"]) && fileStruct.ModifyDate == dateTime && fileStruct.FileName == row["F_FILENAME"].ToString() && fileStruct.Note == row["F_NOTE"].ToString() && fileStruct.PacketFileSize == Convert.ToInt64(row["F_ZIPSIZE"]) && fileStruct.Author == Convert.ToInt64(row["F_AUTHOR"]) && Convert.ToInt32((object) fileStruct.FileType) == Convert.ToInt32(row["F_LINKTYPE"]);
  }

  protected void UpdateBlobRecordBeforeSetFile(FileInfoStruct fileStruct)
  {
    this._dbManager.ExecuteNonQuery($"UPDATE {this._StorageName} SET F_FILENAME = :fname, F_OBJECTLINK_ID = :linkID, F_NOTE = :note, F_FILESIZE = :fsize, F_ZIPSIZE = :fzipsize, F_ARC_METHOD = :farc, F_FILEBODY = {this._dbManager.DataProvider.NullBlobStr}, F_FILEDATE = :fdate, F_ATTRIBUTE_ID = :attrID, F_AUTHOR = :authr, F_LINKTYPE = :linktype WHERE F_FILE_ID = :fid", this._dbManager.Parameter("fname", (object) fileStruct.FileName), this._dbManager.Parameter("linkID", (object) fileStruct.ObjectLinkID), this._dbManager.Parameter("note", (object) fileStruct.Note), this._dbManager.Parameter("fsize", (object) fileStruct.RealFileSize), this._dbManager.Parameter("fzipsize", (object) fileStruct.PacketFileSize), this._dbManager.Parameter("farc", (object) Convert.ToInt32((object) fileStruct.ArcMethod)), this._dbManager.Parameter("fid", (object) fileStruct.FileID), this._dbManager.Parameter("fdate", (object) fileStruct.ModifyDate), this._dbManager.Parameter("attrID", (object) fileStruct.AttributeID), this._dbManager.Parameter("authr", (object) fileStruct.Author), this._dbManager.Parameter("linktype", (object) Convert.ToInt32((object) fileStruct.FileType)));
  }

  protected void SetEmptyBlob(FileInfoStruct fileStruct)
  {
    this._dbManager.ExecuteNonQuery($"UPDATE {this._StorageName} SET F_FILENAME = :fname, F_OBJECTLINK_ID = :linkID, F_NOTE = :note, F_FILESIZE = 0, F_ZIPSIZE = 0, F_FILEBODY = {this._dbManager.DataProvider.NullBlobStr}, F_ARC_METHOD = 0, F_FILEDATE = :fdate, F_ATTRIBUTE_ID = :attrID, F_AUTHOR = :authr, F_LINKTYPE = :linktype WHERE F_FILE_ID = :fid", this._dbManager.Parameter("fname", (object) fileStruct.FileName), this._dbManager.Parameter("linkID", (object) fileStruct.ObjectLinkID), this._dbManager.Parameter("note", (object) fileStruct.Note), this._dbManager.Parameter("fid", (object) fileStruct.FileID), this._dbManager.Parameter("fdate", (object) fileStruct.ModifyDate), this._dbManager.Parameter("attrID", (object) fileStruct.AttributeID), this._dbManager.Parameter("authr", (object) fileStruct.Author), this._dbManager.Parameter("linktype", (object) Convert.ToInt32((object) fileStruct.FileType)));
  }

  protected void UpdateBlobRecordInfo(FileInfoStruct fileStruct)
  {
    this._dbManager.ExecuteNonQuery($"UPDATE {this._StorageName} SET F_FILENAME = :fname, F_OBJECTLINK_ID = :linkID, F_NOTE = :note, F_FILEDATE = :fdate, F_ATTRIBUTE_ID = :attrID, F_AUTHOR = :authr, F_LINKTYPE = :linktype WHERE F_FILE_ID = :fid", this._dbManager.Parameter("fname", (object) fileStruct.FileName), this._dbManager.Parameter("linkID", (object) fileStruct.ObjectLinkID), this._dbManager.Parameter("note", (object) fileStruct.Note), this._dbManager.Parameter("fid", (object) fileStruct.FileID), this._dbManager.Parameter("fdate", (object) fileStruct.ModifyDate), this._dbManager.Parameter("attrID", (object) fileStruct.AttributeID), this._dbManager.Parameter("authr", (object) fileStruct.Author), this._dbManager.Parameter("linktype", (object) Convert.ToInt32((object) fileStruct.FileType)));
  }

  protected DataTable GetBlobRecordTable(FileInfoStruct fileStruct)
  {
    return this._dbManager.ExecuteDataTable($"SELECT F_FILE_ID, F_FILENAME, F_FILESIZE, F_FILEDATE, F_ARC_METHOD, F_ZIPSIZE, F_OBJECTLINK_ID, F_NOTE, F_AUTHOR, F_LINKTYPE FROM {this._StorageName} WHERE F_FILE_ID = :id", this._dbManager.Parameter("id", (object) fileStruct.FileID));
  }

  protected virtual void InternalClear(long blobID)
  {
    this._dbManager.ExecuteNonQuery($"UPDATE {this._StorageName} SET F_FILENAME = NULL, F_NOTE = NULL, F_FILESIZE = 0, F_ZIPSIZE = 0, F_FILEBODY = {this._dbManager.DataProvider.NullBlobStr}, F_ARC_METHOD = 0, F_FILEDATE = {this._dbManager.DataProvider.Now} WHERE F_FILE_ID = :fid", this._dbManager.Parameter("fid", (object) blobID));
  }

  public IBlobReader GetBlobReader(long blobID)
  {
    return (IBlobReader) new StorageBlobReader(this, blobID);
  }

  public virtual long FreeSize => -1;

  public virtual void Release()
  {
    this._Locked = false;
    this._Session.StoragesList.RealeseStorage(this.StorageID);
  }

  public virtual void Lock() => this._Locked = true;

  public bool Locked => this._Locked;

  public void Dispose()
  {
    if (!this._ExternalDbManager)
      return;
    this.Rollback();
    this._dbManager.Dispose();
  }
}
