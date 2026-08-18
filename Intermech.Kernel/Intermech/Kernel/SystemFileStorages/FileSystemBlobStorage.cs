// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.SystemFileStorages.FileSystemBlobStorage
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.IO;
using System;
using System.Data;
using System.IO;


namespace Intermech.Kernel.SystemFileStorages;

internal class FileSystemBlobStorage : BlobStorage
{
  private FileSystemTransactionLog transactionLog;
  private bool LogAllMode = true;
  public const string TransLogSubDirName = "TempDir";
  public string TransactionLogDir;
  public static object LogFileLocker = new object();
  private string _LogFileName = string.Empty;

  public string DirName { get; private set; }

  public FileSystemBlobStorage(
    AttributeValues[] StorageObject,
    UserSession session,
    BlobStoragesPool storagesPool)
    : base(StorageObject, session, storagesPool)
  {
    string asString = AttributeValuesHelper.GetAttributeByGuid(StorageObject, new Guid("cad00015-306c-11d8-b4e9-00304f19f545"), this.StorageID, true).AsString;
    if (asString == string.Empty)
      throw new KernelException(string.Format(sc_13024.ssp_appserver_13025(), (object) this.StorageCaption));
    this.DirName = Directory.Exists(asString) ? asString : throw new KernelException(string.Format(sc_13024.ssp_appserver_13026(), (object) asString, (object) this.StorageCaption));
    this.TransactionLogDir = Path.Combine(this.DirName, "TempDir");
    this.InitDataManager(string.Empty, StorageObject, session);
    this.transactionLog = new FileSystemTransactionLog(this);
  }

  public override void ValidateNewStorage(IUserSession session)
  {
    string[] files = Directory.GetFiles(this.DirName);
    string message = string.Format(sc_13024.ssp_appserver_13027(), (object) this.DirName);
    if (files.Length != 0)
      throw new KernelException(message);
    if (Directory.GetDirectories(this.DirName).Length != 0)
      throw new KernelException(message);
    if (!Directory.Exists(this.TransactionLogDir))
      Directory.CreateDirectory(this.TransactionLogDir);
    this.LogOperation($"Создан файловый шкаф {this.StorageName} пользователем {session.UserName} с компьютера {session.ComputerName}", true);
  }

  public override FileInfoStruct GetFileStruct(long fileID, bool readFileBody)
  {
    FileInfoStruct fileInfo = this.InternalGetFileInfo(fileID, out DataTable _);
    if (readFileBody && fileInfo.PacketFileSize > 0L)
    {
      fileInfo.IsolatedFileName = this.GetFileNameInStore(fileID);
      fileInfo.FileBody = (Stream) new FileStream(fileInfo.GetIsolatedFileName(this.iStoreFile), FileMode.Open, FileAccess.Read);
      fileInfo.IsolatedCacheMode = false;
      this.StoragesPool.FilesCache.AddFile(fileInfo.GetIsolatedFileName(this.iStoreFile));
    }
    return fileInfo;
  }

  public override bool SetNewFileStruct(FileInfoStruct fileStruct)
  {
    if (fileStruct.FileBody != null)
    {
      this._dbManager.ExecuteNonQuery($"INSERT INTO {this._StorageName} (F_FILE_ID, F_FILENAME, F_FILEBODY, F_FILESIZE, F_FILEDATE, F_ARC_METHOD, F_ZIPSIZE, F_OBJECTLINK_ID, F_NOTE, F_ATTRIBUTE_ID, F_AUTHOR, F_LINKTYPE) VALUES (:fID, :fname, {this._dbManager.DataProvider.NullBlobStr}, :fsize, :fdate, :arcMethod, :zipSize, :objID, :notes, :attrID, :authr, :linktype)", this._dbManager.Parameter("fID", (object) fileStruct.FileID), this._dbManager.Parameter("fname", (object) fileStruct.FileName), this._dbManager.Parameter("fsize", (object) fileStruct.RealFileSize), this._dbManager.Parameter("objID", (object) fileStruct.ObjectLinkID), this._dbManager.Parameter("notes", (object) fileStruct.Note), this._dbManager.Parameter("fdate", (object) fileStruct.ModifyDate), this._dbManager.Parameter("arcMethod", (object) (int) fileStruct.ArcMethod), this._dbManager.Parameter("zipSize", (object) fileStruct.PacketFileSize), this._dbManager.Parameter("attrID", (object) fileStruct.AttributeID), this._dbManager.Parameter("authr", (object) fileStruct.Author), this._dbManager.Parameter("linktype", (object) Convert.ToInt32((object) fileStruct.FileType)));
      string fileNameInStore = this.GetFileNameInStore(fileStruct.FileID);
      if (fileStruct.FileName != null && fileStruct.FileName.Trim() != string.Empty)
        this.LogOperation($"Файл '{fileStruct.FileName}' регистрируется в шкафу под именем '{fileNameInStore}'", true);
      this.transactionLog.AddOperation(fileStruct.GetIsolatedFileName(this.iStoreFile), fileNameInStore, FileSystemOperationType.NewFile);
    }
    else
      this.InsertFileInfo(fileStruct);
    return true;
  }

  private string GetTempFileName(long fileID)
  {
    return $"{Path.Combine(this.TransactionLogDir, fileID.ToString())}_{Guid.NewGuid()}.tmp";
  }

  public override void CopyToTemporaryFile(FileInfoStruct fs)
  {
    string isolatedFileName = fs.GetIsolatedFileName(this.iStoreFile);
    if (fs.FileBody != null)
      fs.FileBody.Close();
    string tempFileName = this.GetTempFileName(fs.FileID);
    string destFileName = tempFileName;
    File.Copy(isolatedFileName, destFileName);
    fs.FileBody = (Stream) new FileStream(tempFileName, FileMode.Open, FileAccess.ReadWrite);
    fs.IsolatedFileName = tempFileName;
    fs.IsolatedCacheMode = false;
  }

  public override void PrepareTemporaryFile(FileInfoStruct fileStruct)
  {
    fileStruct.FileBody = (Stream) null;
    string tempFileName = this.GetTempFileName(fileStruct.FileID);
    fileStruct.FileBody = (Stream) new FileStream(tempFileName, FileMode.CreateNew, FileAccess.ReadWrite);
    fileStruct.IsolatedFileName = tempFileName;
    fileStruct.IsolatedCacheMode = false;
  }

  public override void DeleteTemporaryData()
  {
    foreach (string file in Directory.GetFiles(this.TransactionLogDir))
    {
      try
      {
        if (DateTime.Now - File.GetLastWriteTime(file) > TimeSpan.FromDays(2.0))
          File.Delete(file);
      }
      catch (Exception ex)
      {
        this.LogOperation($"Ошибка удаления устаревшего временного файла {file}: {ex.Message}", true);
      }
    }
  }

  private void PrepareFileStruct(FileInfoStruct fileStruct)
  {
    if (fileStruct.FileBody == null || !(fileStruct.FileBody is FileStream) || (fileStruct.FileBody as FileStream).Name.IndexOf(this.TransactionLogDir) >= 0)
      return;
    string tempFileName = this.GetTempFileName(fileStruct.FileID);
    Stream destination = (Stream) new FileStream(tempFileName, FileMode.CreateNew, FileAccess.ReadWrite);
    fileStruct.FileBody.CopyTo(destination);
    fileStruct.FileBody.Close();
    destination.Flush();
    destination.Close();
    fileStruct.FileBody = destination;
    fileStruct.IsolatedFileName = tempFileName;
    fileStruct.IsolatedCacheMode = false;
  }

  public override void SetFileStruct(FileInfoStruct fileStruct)
  {
    this.PrepareFileStruct(fileStruct);
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
        string fileNameInStore = this.GetFileNameInStore(fileStruct.FileID);
        if (fileStruct.FileName != null && fileStruct.FileName.Trim() != string.Empty)
          this.LogOperation($"Файл '{fileStruct.FileName}' обновляется в шкафу под именем '{fileNameInStore}'", false);
        this.transactionLog.AddOperation(fileStruct.GetIsolatedFileName(this.iStoreFile), fileNameInStore, FileSystemOperationType.ReplaceFile);
      }
    }
  }

  protected override string GetAdditionalColumns() => string.Empty;

  public override void Clear(long blobID)
  {
    this.InternalClear(blobID);
    this.transactionLog.AddOperation(string.Empty, this.GetFileNameInStore(blobID), FileSystemOperationType.DeleteFile);
  }

  public override void DeleteFile(long fileID)
  {
    base.DeleteFile(fileID);
    string fileNameInStore = this.GetFileNameInStore(fileID);
    this.transactionLog.AddOperation(string.Empty, fileNameInStore, FileSystemOperationType.DeleteFile);
  }

  private string LogFileName
  {
    get
    {
      if (this._LogFileName == string.Empty)
        this._LogFileName = Path.Combine(this.DirName, FileNameHelper.ReplaceInvalidProtoFileNameChars((ServerServices.GetService(typeof (IAppServers)) as IAppServers).ServerName) + ".log");
      return this._LogFileName;
    }
  }

  public void LogOperation(string message, bool logAlways)
  {
    if (!(this.LogAllMode | logAlways))
      return;
    lock (FileSystemBlobStorage.LogFileLocker)
    {
      using (StreamWriter streamWriter = new StreamWriter(this.LogFileName, true))
      {
        if (message == string.Empty)
          streamWriter.WriteLine();
        else
          streamWriter.WriteLine("{0}> {1}", (object) DateTime.Now, (object) message);
      }
    }
  }

  private string GetFileNameInStore(long fileID)
  {
    string str1 = Convert.ToString(fileID, 16 /*0x10*/);
    string path3 = string.Empty;
    string path2;
    if (str1.Length > 1)
    {
      path2 = str1.Substring(str1.Length - 2);
      if (str1.Length > 3)
        path3 = str1.Substring(str1.Length - 4, 2);
    }
    else
      path2 = string.Empty;
    string str2 = Path.Combine(this.DirName, path2, path3);
    if (!Directory.Exists(str2))
      Directory.CreateDirectory(str2);
    return Path.Combine(str2, fileID.ToString());
  }

  public override long FreeSize => DriveUtils.GetAvailableFreeSpace(this.DirName);

  public override void StartTransaction() => this.transactionLog.StartTransaction();

  public override void Commit() => this.transactionLog.Commit();

  public override void Rollback() => this.transactionLog.Rollback();
}
