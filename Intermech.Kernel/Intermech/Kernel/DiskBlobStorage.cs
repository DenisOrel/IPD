// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DiskBlobStorage
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Vault.Interfaces;
using Intermech.Vault.Interfaces.Client;
using System;
using System.Data;
using System.IO;


namespace Intermech.Kernel;

internal class DiskBlobStorage : BlobStorage
{
  private IDiskFileStorage currentStorage;
  private bool CurrentStorageInTransaction;
  private const string SERVER = "server";
  private const string PORT = "port";

  public DiskBlobStorage(
    AttributeValues[] StorageObject,
    UserSession session,
    BlobStoragesPool storagesPool)
    : base(StorageObject, session, storagesPool)
  {
    string asString = AttributeValuesHelper.GetAttributeByGuid(StorageObject, new Guid("cad00020-306c-11d8-b4e9-00304f19f545"), this.StorageID, true).AsString;
    this._dbManager = session.DataManager;
    this._ExternalDbManager = false;
    DiskBlobStorageConnectionStringBuilder connectionStringBuilder = new DiskBlobStorageConnectionStringBuilder(AttributeValuesHelper.GetAttributeByGuid(StorageObject, new Guid("cad00015-306c-11d8-b4e9-00304f19f545"), this.StorageID, true).AsString);
    connectionStringBuilder.Password = AttributeValuesHelper.GetAttributeByGuid(StorageObject, new Guid("cad01579-306c-11d8-b4e9-00304f19f545"), this.StorageID, true).AsString;
    connectionStringBuilder.Validate();
    ProxyClass proxyClass = new ProxyClass(connectionStringBuilder.Server, connectionStringBuilder.Port.ToString());
    try
    {
      this.currentStorage = proxyClass.CreateStorage(AttributeValuesHelper.GetAttributeByID(StorageObject, -12, this.StorageID, true).AsString, asString, connectionStringBuilder.Password, EnvironmentConsts.MachineName);
      this.currentStorage.UserName = session.UserName;
    }
    catch
    {
      this.currentStorage = (IDiskFileStorage) null;
      throw;
    }
  }

  private FileInfoStruct GetFileStructFromTable(long fileID)
  {
    DataTable dataTable = this._dbManager.ExecuteDataTable($"SELECT {"F_FILE_ID, F_FILENAME, F_FILESIZE, F_FILEDATE, F_ARC_METHOD, F_OBJECTLINK_ID, F_ZIPSIZE, F_NOTE, F_ATTRIBUTE_ID, F_AUTHOR, F_LINKTYPE"} FROM {this._StorageName} WHERE F_FILE_ID = :fID", this._dbManager.Parameter("fID", (object) fileID));
    if (dataTable.Rows.Count == 0)
      throw new KernelException(string.Format(Intermech.Localization.LocalizationHolder.rm.GetString(sc_13015.ssp_appserver_13016()), (object) fileID, (object) this._StorageName));
    FileInfoStruct fileStructFromTable = new FileInfoStruct();
    fileStructFromTable.FileID = fileID;
    fileStructFromTable.ArcMethod = (ArcMethods) Convert.ToInt32(dataTable.Rows[0]["F_ARC_METHOD"]);
    object obj1 = dataTable.Rows[0]["F_FILENAME"];
    fileStructFromTable.FileName = obj1 != DBNull.Value ? Convert.ToString(obj1) : "";
    object obj2 = dataTable.Rows[0]["F_FILEDATE"];
    fileStructFromTable.ModifyDate = obj2 != DBNull.Value ? Convert.ToDateTime(obj2) : DateTime.MinValue;
    fileStructFromTable.PacketFileSize = Convert.ToInt64(dataTable.Rows[0]["F_ZIPSIZE"]);
    fileStructFromTable.RealFileSize = Convert.ToInt64(dataTable.Rows[0]["F_FILESIZE"]);
    fileStructFromTable.ObjectLinkID = Convert.ToInt64(dataTable.Rows[0]["F_OBJECTLINK_ID"]);
    fileStructFromTable.AttributeID = Convert.ToInt32(dataTable.Rows[0]["F_ATTRIBUTE_ID"]);
    fileStructFromTable.FileType = (FileTypes) Convert.ToInt32(dataTable.Rows[0]["F_LINKTYPE"]);
    fileStructFromTable.Author = Convert.ToInt64(dataTable.Rows[0]["F_AUTHOR"]);
    object obj3 = dataTable.Rows[0]["F_NOTE"];
    fileStructFromTable.Note = obj3 != DBNull.Value ? Convert.ToString(obj3) : "";
    fileStructFromTable.FileBody = (Stream) null;
    return fileStructFromTable;
  }

  public override FileInfoStruct GetFileStruct(long fileID, bool readFileBody)
  {
    FileInfoStruct fileStructFromTable = this.GetFileStructFromTable(fileID);
    readFileBody = readFileBody && fileStructFromTable.PacketFileSize != 0L;
    if (readFileBody)
    {
      FileInformation fileInformation = this.currentStorage.GetFileInformation(fileID);
      if (fileInformation.PacketFileSize != 0L)
      {
        string isolatedFileName = fileStructFromTable.GetIsolatedFileName(this.iStoreFile);
        IReadWorker readWorker = (this.currentStorage as IFileProcReader2).OpenFileReader(fileInformation);
        fileStructFromTable.FileBody = (Stream) new FileStream(isolatedFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
        try
        {
          byte[] dataBlock = new byte[CommonVariables.BLOCK_SIZE];
          int count;
          do
          {
            count = readWorker.ReadBlock(ref dataBlock, CommonVariables.BLOCK_SIZE);
            if (count > 0)
              fileStructFromTable.FileBody.Write(dataBlock, 0, count);
          }
          while (count > 0);
        }
        finally
        {
          fileStructFromTable.FileBody.Position = 0L;
          readWorker.Close();
        }
      }
    }
    return fileStructFromTable;
  }

  private FileInfoStruct ReadFile(FileInformation fileInfo, bool readFileBody)
  {
    FileInfoStruct fileInfoStruct = this.FillStruct(fileInfo);
    if (fileInfo.PacketFileSize != 0L)
    {
      string isolatedFileName = fileInfoStruct.GetIsolatedFileName(this.iStoreFile);
      IReadWorker readWorker = (this.currentStorage as IFileProcReader2).OpenFileReader(fileInfo);
      fileInfoStruct.FileBody = (Stream) new FileStream(isolatedFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
      try
      {
        byte[] dataBlock = new byte[CommonVariables.BLOCK_SIZE];
        int count;
        do
        {
          count = readWorker.ReadBlock(ref dataBlock, CommonVariables.BLOCK_SIZE);
          if (count > 0)
            fileInfoStruct.FileBody.Write(dataBlock, 0, count);
        }
        while (count > 0);
      }
      finally
      {
        fileInfoStruct.FileBody.Position = 0L;
        readWorker.Close();
      }
    }
    return fileInfoStruct;
  }

  public override bool SetNewFileStruct(FileInfoStruct fileStruct)
  {
    using (this._dbManager.WithOpenConnection())
      this._dbManager.ExecuteNonQuery($"INSERT INTO {this._StorageName} (F_FILE_ID, F_FILENAME, F_FILESIZE, F_FILEDATE, F_ARC_METHOD, F_ZIPSIZE, F_OBJECTLINK_ID, F_NOTE, F_ATTRIBUTE_ID, F_AUTHOR, F_LINKTYPE, F_FILEBODY) VALUES (:fID, :fname, :fsize, :fdate, :arc, :zipsize, :objID, :notes, :attrID, :authr, :linktype, {this._dbManager.DataProvider.NullBlobStr})", this._dbManager.Parameter("fID", (object) fileStruct.FileID), this._dbManager.Parameter("fname", (object) fileStruct.FileName), this._dbManager.Parameter("fsize", (object) fileStruct.RealFileSize), this._dbManager.Parameter("fdate", (object) fileStruct.ModifyDate), this._dbManager.Parameter("arc", (object) Convert.ToInt32((object) fileStruct.ArcMethod)), this._dbManager.Parameter("zipsize", (object) fileStruct.PacketFileSize), this._dbManager.Parameter("objID", (object) fileStruct.ObjectLinkID), this._dbManager.Parameter("notes", (object) fileStruct.Note), this._dbManager.Parameter("attrID", (object) fileStruct.AttributeID), this._dbManager.Parameter("authr", (object) fileStruct.Author), this._dbManager.Parameter("linktype", (object) Convert.ToInt32((object) fileStruct.FileType)));
    if (fileStruct.FileBody != null)
    {
      string tempFileNameToWrite = this.currentStorage.WriteFileInfo(this.FillFileInformation(fileStruct));
      this.WriteFile(fileStruct, tempFileNameToWrite);
    }
    return true;
  }

  public override void SetFileStruct(FileInfoStruct fileStruct)
  {
    using (this._dbManager.WithOpenConnection())
    {
      if (this._dbManager.ExecuteDataTable($"SELECT F_FILE_ID, F_FILENAME, F_FILESIZE, F_FILEDATE, F_ARC_METHOD, F_ZIPSIZE, F_OBJECTLINK_ID, F_NOTE FROM {this._StorageName} WHERE F_FILE_ID = :id", this._dbManager.Parameter("id", (object) fileStruct.FileID)).Rows.Count == 0)
      {
        this.SetNewFileStruct(fileStruct);
        return;
      }
      if (fileStruct.FileBody == null)
        this._dbManager.ExecuteNonQuery($"UPDATE {this._StorageName} SET F_FILENAME = :fname, F_OBJECTLINK_ID = :linkID, F_NOTE = :note, F_FILEDATE = :fdate, F_ATTRIBUTE_ID = :attrID, F_AUTHOR = :authr, F_LINKTYPE = :linktype WHERE F_FILE_ID = :fid", this._dbManager.Parameter("fname", (object) fileStruct.FileName), this._dbManager.Parameter("linkID", (object) fileStruct.ObjectLinkID), this._dbManager.Parameter("note", (object) fileStruct.Note), this._dbManager.Parameter("fid", (object) fileStruct.FileID), this._dbManager.Parameter("fdate", (object) fileStruct.ModifyDate), this._dbManager.Parameter("attrID", (object) fileStruct.AttributeID), this._dbManager.Parameter("authr", (object) fileStruct.Author), this._dbManager.Parameter("linktype", (object) Convert.ToInt32((object) fileStruct.FileType)));
      else
        this._dbManager.ExecuteNonQuery($"UPDATE {this._StorageName} SET F_FILENAME = :fname, F_OBJECTLINK_ID = :linkID, F_NOTE = :note, F_FILESIZE = :fsize, F_ZIPSIZE = :fzipsize, F_ARC_METHOD = :farc, F_FILEBODY = {this._dbManager.DataProvider.NullBlobStr}, F_FILEDATE = :fdate, F_ATTRIBUTE_ID = :attrID, F_AUTHOR = :authr, F_LINKTYPE = :linktype WHERE F_FILE_ID = :fid", this._dbManager.Parameter("fname", (object) fileStruct.FileName), this._dbManager.Parameter("linkID", (object) fileStruct.ObjectLinkID), this._dbManager.Parameter("note", (object) fileStruct.Note), this._dbManager.Parameter("fsize", (object) fileStruct.RealFileSize), this._dbManager.Parameter("fzipsize", (object) fileStruct.PacketFileSize), this._dbManager.Parameter("farc", (object) Convert.ToInt32((object) fileStruct.ArcMethod)), this._dbManager.Parameter("fid", (object) fileStruct.FileID), this._dbManager.Parameter("fdate", (object) fileStruct.ModifyDate), this._dbManager.Parameter("attrID", (object) fileStruct.AttributeID), this._dbManager.Parameter("authr", (object) fileStruct.Author), this._dbManager.Parameter("linktype", (object) Convert.ToInt32((object) fileStruct.FileType)));
    }
    if (fileStruct.PacketFileSize == 0L)
      return;
    string tempFileNameToWrite = this.currentStorage.WriteFileInfo(this.FillFileInformation(fileStruct));
    this.WriteFile(fileStruct, tempFileNameToWrite);
  }

  public override void Clear(long blobID)
  {
    FileInfoStruct fileStructFromTable = this.GetFileStructFromTable(blobID);
    fileStructFromTable.ArcMethod = ArcMethods.NotPacked;
    fileStructFromTable.FileName = string.Empty;
    fileStructFromTable.ModifyDate = DateTime.UtcNow;
    fileStructFromTable.Note = string.Empty;
    fileStructFromTable.PacketFileSize = 0L;
    fileStructFromTable.RealFileSize = 0L;
    this.SetFileStruct(fileStructFromTable);
  }

  private void WriteFile(FileInfoStruct fileStruct, string tempFileNameToWrite)
  {
    IWriteWorker writeWorker = (this.currentStorage as IFileProcWriter2).OpenFileWriter(tempFileNameToWrite);
    try
    {
      if (fileStruct.FileBody == null)
        return;
      int dataLength = CommonVariables.BLOCK_SIZE;
      fileStruct.FileBody.Position = 0L;
      byte[] numArray = new byte[dataLength];
      while (dataLength > 0)
      {
        int blockSize = CommonVariables.BLOCK_SIZE;
        dataLength = fileStruct.FileBody.Read(numArray, 0, blockSize);
        if (dataLength > 0)
          writeWorker.WriteBlock(numArray, dataLength);
      }
    }
    finally
    {
      writeWorker.Close();
    }
  }

  public override void ChangeObjectLinkID(long fileID, long toID)
  {
    FileInfoStruct fileStructFromTable = this.GetFileStructFromTable(fileID);
    this._dbManager.ExecuteNonQuery($"UPDATE {this._StorageName} SET F_OBJECTLINK_ID = :toID WHERE F_FILE_ID = :fileID", this._dbManager.Parameter(nameof (toID), (object) toID), this._dbManager.Parameter(nameof (fileID), (object) fileID));
    FileInformation fileInfo = this.FillFileInformation(fileStructFromTable);
    fileInfo.ObjectID = toID;
    this.currentStorage.ChangeObjectLinkID(fileInfo);
  }

  public DataTable GetObjectHistory(long id) => this.currentStorage.GetObjectHistory(id);

  public DataTable GetVersionHistory(long objectID)
  {
    return this.currentStorage.GetVersionHistory(objectID);
  }

  public override DataTable GetObjectFilesList(long id) => this.currentStorage.GetObjectHistory(id);

  public DataTable GetVersionFilesList(long objectID)
  {
    return this.currentStorage.GetVersionHistory(objectID);
  }

  public DataTable GetHistoryForFile(long blobID, long objectID)
  {
    return this.currentStorage.GetHistoryForFile(blobID, objectID);
  }

  public DataTable GetHistoryForFile(string fileName, long objectID)
  {
    return this.currentStorage.GetHistoryForFile(fileName, objectID);
  }

  public FileInfoStruct GetHistoryFileStructure(int historyID, long objectID, bool readFileBody)
  {
    return this.ReadFile(this.currentStorage.GetFileHistoryInformation(historyID, objectID), readFileBody);
  }

  private FileInfoStruct FillStruct(FileInformation fileInfo)
  {
    return new FileInfoStruct()
    {
      ArcMethod = fileInfo.ArcMethod,
      FileID = fileInfo.BlobID,
      FileName = fileInfo.Name,
      ModifyDate = fileInfo.FileDate,
      Note = fileInfo.Note,
      ObjectLinkID = fileInfo.ObjectID,
      PacketFileSize = fileInfo.PacketFileSize,
      RealFileSize = fileInfo.RealSize
    };
  }

  private FileInformation FillFileInformation(FileInfoStruct fileStruct)
  {
    FileInformation fileInformation = new FileInformation();
    fileInformation.BlobID = fileStruct.FileID;
    fileInformation.ObjectID = fileStruct.ObjectLinkID;
    IDBObject objectActualCopy = this._Session.GetObjectActualCopy(fileStruct.ObjectLinkID, false);
    fileInformation.ID = objectActualCopy == null ? 0L : objectActualCopy.ID;
    fileInformation.Name = fileStruct.FileName;
    fileInformation.FileDate = fileStruct.ModifyDate;
    fileInformation.ArcMethod = fileStruct.ArcMethod;
    fileInformation.RealSize = fileStruct.RealFileSize;
    fileInformation.PacketFileSize = fileStruct.PacketFileSize;
    fileInformation.Note = fileStruct.Note;
    fileInformation.IsStreamEmty = fileStruct.FileBody == null;
    return fileInformation;
  }

  public override void DeleteFile(long fileID)
  {
    try
    {
      base.DeleteFile(fileID);
      this.currentStorage.DeleteFile(fileID);
    }
    catch
    {
    }
  }

  public override void StartTransaction()
  {
    if (this.CurrentStorageInTransaction)
      return;
    this.currentStorage.StartTransaction();
    this.CurrentStorageInTransaction = true;
  }

  public override void Rollback()
  {
    if (!this.CurrentStorageInTransaction)
      return;
    this.currentStorage.Rollback();
    this.CurrentStorageInTransaction = false;
  }

  public override void Commit()
  {
    if (!this.CurrentStorageInTransaction)
      return;
    this.currentStorage.Commit();
    this.CurrentStorageInTransaction = false;
  }

  public DataTable GetStorageInfo() => this.currentStorage.GetStorageInfo();

  public override void DeleteStorage()
  {
    try
    {
      this.currentStorage.DeleteStorage();
    }
    catch
    {
      throw;
    }
    try
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
        throw new KernelExceptionID(sc_13015.ssp_appserver_13017(1765496696), (object) this._StorageName, obj);
      (UserSession.Sessions as UserSessionCollection).ClearActiveStorageID(this.StorageID);
      this._dbManager.ExecuteNonQuery("DROP TABLE " + this._StorageName);
    }
    catch
    {
    }
  }

  public override void Release() => base.Release();
}
