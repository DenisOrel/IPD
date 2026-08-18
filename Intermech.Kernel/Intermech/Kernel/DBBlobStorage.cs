// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBBlobStorage
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Data;
using System.IO;


namespace Intermech.Kernel;

internal class DBBlobStorage : BlobStorage
{
  public DBBlobStorage(
    AttributeValues[] StorageObject,
    UserSession session,
    BlobStoragesPool storagesPool)
    : base(StorageObject, session, storagesPool)
  {
    string asString = AttributeValuesHelper.GetAttributeByGuid(StorageObject, new Guid("cad00015-306c-11d8-b4e9-00304f19f545"), this.StorageID, true).AsString;
    Guid guid = new Guid(AttributeValuesHelper.GetAttributeByID(StorageObject, -12, this.StorageID, true).AsString);
    if (asString == string.Empty && guid.Equals(new Guid("cad0000c-306c-11d8-b4e9-00304f19f545")))
    {
      this._dbManager = session.DataManager;
      this._ExternalDbManager = false;
    }
    else
      this.InitDataManager(asString, StorageObject, session);
  }

  public override FileInfoStruct GetFileStruct(long fileID, bool readFileBody)
  {
    DataTable tbl;
    FileInfoStruct fileInfo = this.InternalGetFileInfo(fileID, out tbl);
    if (fileInfo.RealFileSize == 0L && fileInfo.PacketFileSize == 0L || !readFileBody)
    {
      fileInfo.FileBody = (Stream) null;
    }
    else
    {
      string isolatedFileName = fileInfo.GetIsolatedFileName(this.iStoreFile);
      if (this.iStoreFile.FileExists(isolatedFileName))
      {
        try
        {
          fileInfo.FileBody = (Stream) new FileStream(isolatedFileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
        }
        catch
        {
          isolatedFileName = fileInfo.GetIsolatedFileName(this.iStoreFile, true);
          fileInfo.FileBody = (Stream) null;
        }
      }
      if (fileInfo.FileBody == null)
        this._dbManager.DataProvider.ReadFileBody(this._dbManager, fileInfo, this._StorageName, isolatedFileName, this._BufferSize, tbl.Rows[0]);
      this.StoragesPool.FilesCache.AddFile(isolatedFileName);
    }
    return fileInfo;
  }

  public override bool SetNewFileStruct(FileInfoStruct fileStruct)
  {
    using (this._dbManager.WithOpenConnection())
    {
      if (fileStruct.FileBody != null)
        this._dbManager.DataProvider.InsertFileBody(this._dbManager, fileStruct, this._StorageName);
      else
        this.InsertFileInfo(fileStruct);
      return true;
    }
  }

  public override void SetFileStruct(FileInfoStruct fileStruct)
  {
    using (this._dbManager.WithOpenConnection())
    {
      DataTable blobRecordTable = this.GetBlobRecordTable(fileStruct);
      if (blobRecordTable.Rows.Count == 0)
      {
        this.SetNewFileStruct(fileStruct);
      }
      else
      {
        if (this.IsBlobNotModified(fileStruct, blobRecordTable.Rows[0]))
          return;
        if (fileStruct.FileBody == null)
          this.UpdateBlobRecordInfo(fileStruct);
        else if (fileStruct.PacketFileSize == 0L)
        {
          this.SetEmptyBlob(fileStruct);
        }
        else
        {
          this.UpdateBlobRecordBeforeSetFile(fileStruct);
          this._dbManager.DataProvider.WriteBlob(this._StorageName, "F_FILEBODY", "F_FILE_ID", (object) fileStruct.FileID, fileStruct.FileBody, this._dbManager, fileStruct.PacketFileSize);
        }
      }
    }
  }

  public override void Clear(long blobID)
  {
    using (this._dbManager.WithOpenConnection())
      this.InternalClear(blobID);
  }

  public override void DeleteFile(long fileID)
  {
    this._dbManager.DataProvider.DeleteFileBody(this._dbManager, fileID, this._StorageName);
    base.DeleteFile(fileID);
  }

  public void CloneFile(
    long fromFile,
    long toFileID,
    string newFileName,
    long objectID,
    long userID,
    int attributeID)
  {
    bool flag = !this._dbManager.InTransaction;
    if (flag)
      this._dbManager.BeginTransaction();
    try
    {
      this.DeleteFile(toFileID);
      this._dbManager.DataProvider.CloneFile(this._dbManager, this._StorageName, fromFile, toFileID, newFileName, objectID, userID, attributeID);
      if (!flag)
        return;
      this._dbManager.Commit();
    }
    catch
    {
      if (flag)
        this._dbManager.Rollback();
      throw;
    }
  }
}
