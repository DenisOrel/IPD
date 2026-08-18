// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBStorageAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;


namespace Intermech.Kernel;

internal class DBStorageAttribute : 
  DBAdditionalAttribute,
  IBlobWriterEx,
  IBlobWriter,
  IBlobReader,
  IDisposable
{
  private const string traceError2FileName = "Error2.log";
  protected BinaryReader _Reader;
  protected BinaryWriter _Writer;
  internal int _DataBlockSize;
  protected long _WrittenBytesCount;
  private IBlobStoragesPool _StoragesPool;
  private BlobAttributeStates _BlobState;
  internal FileInfoStruct _FileStruct;
  private Dictionary<int, BlobContainer> _TempBlobBodies;

  public DBStorageAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, parent)
  {
    this._AutoSaveHistory = false;
  }

  public DBStorageAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    bool temporary,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, temporary, parent)
  {
  }

  protected Dictionary<int, BlobContainer> TempBlobBodies
  {
    get
    {
      if (this._TempBlobBodies == null)
        this._TempBlobBodies = new Dictionary<int, BlobContainer>(1);
      return this._TempBlobBodies;
    }
  }

  private BlobContainer GetCurrentTempBlob()
  {
    return this.TempBlobBodies[this.Index] ?? throw new KernelException("Попытка получить доступ к несуществующему объекту BlobContainer. Возможно, не был вызван метод OpenBlob().");
  }

  private void NotApplicable4TempAttr()
  {
    if (this.TemporaryAttribute)
      throw new KernelException("Операция не применима для временных атрибутов.");
  }

  public override int Index
  {
    set
    {
      if (this.Index == value)
        return;
      this.CheckForClosed();
      this._FileStruct = (FileInfoStruct) null;
      base.Index = value;
    }
  }

  protected virtual void UpdateObjectModifyDate() => this.SetContentDate();

  protected override void DoAddValue(object newValue)
  {
    this.CheckForClosed();
    if (newValue == null)
      this._FileStruct = (FileInfoStruct) null;
    base.DoAddValue((object) null);
  }

  public BlobAttributeStates BlobState
  {
    get => this._BlobState;
    set
    {
      if (this._BlobState == value)
        return;
      if (value != BlobAttributeStates.Closed && this._BlobState != BlobAttributeStates.Closed)
        throw new KernelExceptionID(sc_12563.ssp_appserver_12565(723192427));
      if (value == BlobAttributeStates.Closed)
        this.UserSession.RemoveDisposableObject((object) this);
      else
        this.UserSession.AddDisposableObject((object) this);
      this._BlobState = value;
    }
  }

  internal BlobStoragesPool StoragesPool
  {
    get
    {
      if (this._StoragesPool == null)
        this._StoragesPool = ServerServices.GetService(typeof (IBlobStoragesPool)) as IBlobStoragesPool;
      return this._StoragesPool as BlobStoragesPool;
    }
  }

  public long StorageID => Convert.ToInt64(this.AsDouble);

  public void CloseBlob()
  {
    if (this.BlobState == BlobAttributeStates.Closed)
      return;
    if (this.TemporaryAttribute)
    {
      this._TempBlobBodies = (Dictionary<int, BlobContainer>) null;
    }
    else
    {
      if (this._Reader != null)
      {
        this._Reader.Close();
        this._Reader = (BinaryReader) null;
      }
      if (this._Writer != null)
      {
        this._Writer.Close();
        this._Writer = (BinaryWriter) null;
      }
      if (this._FileStruct.FileBody != null)
        this._FileStruct.FileBody.Close();
    }
    this.BlobState = BlobAttributeStates.Closed;
  }

  protected void CheckForClosed()
  {
    if (this.BlobState != BlobAttributeStates.Closed)
    {
      string str = this.BlobState != BlobAttributeStates.OpenedForRead ? LocalizationHolder.rm.GetString("Kernel_246") : LocalizationHolder.rm.GetString("Kernel_245");
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12563.ssp_appserver_12566()), (object) this.Name, (object) str));
    }
  }

  protected virtual bool SaveStringInfo()
  {
    if (!(this.AsString != this._FileStruct.Note))
      return false;
    if (this._FileStruct.Note.Length > Consts.MaxNoteLength)
      throw new KernelExceptionID(412, (object) this.Name, (object) Consts.MaxNoteLength);
    this.AsString = this._FileStruct.Note;
    return true;
  }

  private void WriteDataToStorage()
  {
    if (this.TemporaryAttribute)
      return;
    this.UserSession.StartTransaction();
    try
    {
      IBlobStorage storage = this.StoragesPool.GetStorage(this.StorageID, (IUserSession) this.UserSession);
      try
      {
        storage.SetFileStruct(this._FileStruct);
        if (!this.SaveStringInfo())
          this.UpdateObjectModifyDate();
        this.ParentObject.AfterSetAdditionalAttributeValue((IDBAttribute) this);
        this.ParentObject.AfterSetComplexAttributeValue((IDBAttribute) this);
      }
      finally
      {
        this._FileStruct.FileBody.Close();
        this.StoragesPool.ReleaseStorage(storage);
        this.BlobState = BlobAttributeStates.Closed;
      }
      this.UserSession.Commit();
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
  }

  public bool WriteDataBlock(byte[] data) => this.WriteDataBlockEx(data, 0, data.Length);

  public bool WriteDataBlockEx(byte[] data, int index, int length)
  {
    if (this.BlobState != BlobAttributeStates.OpenedForWrite)
      throw new KernelExceptionID(sc_12563.ssp_appserver_12567(518907831));
    this._WrittenBytesCount += (long) length;
    if (this._WrittenBytesCount > this._FileStruct.PacketFileSize)
    {
      if (this._DataBlockSize == 0)
        throw new KernelExceptionID(sc_12563.ssp_appserver_12568(1718327054), (object) this._WrittenBytesCount, (object) this._FileStruct.PacketFileSize);
      (this.EventHelper as EventLogHelper).AddToTrace("Коррекция неверного размера файла при записи данных в файловый шкаф.", Consts.traceAlways, "Error2.log");
      string str = string.Empty;
      if (this.ParentObject is DBObject)
        str = (this.ParentObject as DBObject).NameInMessages;
      (this.EventHelper as EventLogHelper).AddToTrace(string.Format($"Реальная длина данных {{0}} байт, заданная в свойствах файла {{1}} байт.{Environment.NewLine}Объект: {{2}}{Environment.NewLine}Имя файла: {{3}}{Environment.NewLine}Идентификатор файла: {{4}}", (object) this._WrittenBytesCount, (object) this._FileStruct.PacketFileSize, (object) str, (object) this._FileStruct.FileName, (object) this._FileStruct.FileID), Consts.traceAlways, "Error2.log");
      this._FileStruct.PacketFileSize = this._WrittenBytesCount;
    }
    if (this.TemporaryAttribute)
    {
      this.GetCurrentTempBlob().WriteDataBlockEx(data, index, length);
    }
    else
    {
      this._Writer.Write(data, index, length);
      this._Writer.Flush();
    }
    if (this._WrittenBytesCount != this._FileStruct.PacketFileSize)
      return true;
    this.WriteDataToStorage();
    return false;
  }

  protected virtual void ValidateBlobInfo(BlobInformation blobInfo)
  {
  }

  private bool IsRedliningFile(string fileName)
  {
    if (this.AttributeID != this.UserSession.IdentHelper.FileAttributeID || this.Index <= 0)
      return false;
    IRedliningService service = ((ICustomServices) ServerServices.GetService(typeof (ICustomServices))).GetService(typeof (IRedliningService)) as IRedliningService;
    object obj = this._ValuesTable[0]["F_STRING_VALUE"];
    string mainFilePath = obj == DBNull.Value || obj == null ? string.Empty : obj.ToString();
    string verifiableFilePath = fileName;
    return service.IsRedliningFile(mainFilePath, verifiableFilePath);
  }

  void IBlobWriterEx.CloseBlob(long realFileSize)
  {
    if (this.BlobState == BlobAttributeStates.OpenedForWrite)
    {
      this._FileStruct.PacketFileSize = this._WrittenBytesCount;
      this._FileStruct.RealFileSize = realFileSize;
      this.WriteDataToStorage();
    }
    this.BlobState = BlobAttributeStates.Closed;
  }

  bool IBlobWriter.OpenBlob(BlobInformation blobInfo, bool onlyInfo)
  {
    return this.OpenBlob(blobInfo, onlyInfo, true);
  }

  public bool OpenBlob(BlobInformation blobInfo, bool onlyInfo, bool fixedSize)
  {
    this._DataBlockSize = 0;
    if (blobInfo.FileName.Trim() == string.Empty)
      blobInfo.FileName = string.Empty;
    this.CheckForClosed();
    this.ValidateBlobInfo(blobInfo);
    if (this.TemporaryAttribute)
    {
      this.TempBlobBodies[this.Index] = new BlobContainer(blobInfo);
    }
    else
    {
      bool flag1 = blobInfo.FileType == FileTypes.ftNormal;
      bool flag2 = blobInfo.FileType == FileTypes.ftAuthentical;
      if (!flag1 && !this.IsNull)
      {
        BlobInformation blobInformation = ((IBlobReader) this).OpenBlob(-1);
        flag1 = blobInformation.FileType == FileTypes.ftNormal;
        if (blobInformation.FileType == FileTypes.ftAuthentical)
          flag2 = true;
      }
      if (flag1)
      {
        this.ValidateDirectWrite((object) null);
      }
      else
      {
        try
        {
          this.CheckAccess(ActionType.Write);
          this.AddEvent(ActionType.Write, EventlogRecordType.AccessGranted);
        }
        catch
        {
          this.AddEvent(ActionType.Write, EventlogRecordType.AccessDenied);
          throw;
        }
        if (flag2)
        {
          try
          {
            this.CheckLCStepAccess(ActionType.EditAuthenticalFiles, true);
          }
          catch
          {
            this.AddEvent(ActionType.EditAuthenticalFiles, EventlogRecordType.AccessDenied);
            throw;
          }
        }
      }
      this.CheckRedliningAccess(blobInfo);
      this._FileStruct = new FileInfoStruct();
      this._FileStruct.ArcMethod = blobInfo.ArcMethod;
      this._FileStruct.FileID = this.AsInteger;
      this._FileStruct.FileName = blobInfo.FileName;
      if (blobInfo.ModifyDate.Kind != DateTimeKind.Utc)
        this._FileStruct.ModifyDate = blobInfo.ModifyDate - this.UserSession.TimeZoneOffset;
      this._FileStruct.Note = blobInfo.Note;
      this._FileStruct.ObjectLinkID = this.DBObjectID;
      this._FileStruct.PacketFileSize = !fixedSize ? long.MaxValue : blobInfo.PackedFileSize;
      this._FileStruct.RealFileSize = blobInfo.RealFileSize;
      this._FileStruct.AttributeID = this.AttributeID;
      this._FileStruct.FileType = blobInfo.FileType == FileTypes.ftRedlining || this.AttributeID != this.UserSession.IdentHelper.AttributeRedlining ? blobInfo.FileType : FileTypes.ftRedlining;
      if (blobInfo.FileType == FileTypes.ftNormal && blobInfo.FileName != null && blobInfo.FileName != string.Empty && this.IsRedliningFile(blobInfo.FileName))
        this._FileStruct.FileType = FileTypes.ftRedlining;
      this._FileStruct.Author = blobInfo.Author != 0L ? blobInfo.Author : this.UserSession.UserID;
      bool flag3 = true;
      IBlobStorage storage = this.StoragesPool.GetStorage(this.StorageID, (IUserSession) this.UserSession);
      try
      {
        if (onlyInfo)
        {
          this._FileStruct.PacketFileSize = blobInfo.PackedFileSize;
          this._FileStruct.FileBody = (Stream) null;
        }
        else
        {
          storage.PrepareTemporaryFile(this._FileStruct);
          this._Writer = new BinaryWriter(this._FileStruct.FileBody);
          flag3 = this._FileStruct.PacketFileSize == 0L;
        }
        if (flag3)
        {
          this.UserSession.StartTransaction();
          try
          {
            storage.SetFileStruct(this._FileStruct);
            if (!this.SaveStringInfo())
              this.UpdateObjectModifyDate();
            this.UserSession.Commit();
          }
          catch
          {
            this.UserSession.Rollback();
            throw;
          }
          finally
          {
            if (this._FileStruct.FileBody != null)
              this._FileStruct.FileBody.Close();
          }
          return false;
        }
      }
      finally
      {
        this.StoragesPool.ReleaseStorage(storage);
      }
    }
    this._WrittenBytesCount = 0L;
    this.BlobState = BlobAttributeStates.OpenedForWrite;
    return true;
  }

  protected virtual void CheckRedliningAccess(BlobInformation blobInfo)
  {
    IRedliningService service = ((ICustomServices) ServerServices.GetService(typeof (ICustomServices))).GetService(typeof (IRedliningService)) as IRedliningService;
    if (this.AttributeID == service.RedliningAttributeID && service.DeleteFiles && this.IsObjectAttribute && (this.ParentObject as IDBLifecycleLevel).LevelID == service.LevelID)
      throw new KernelExceptionID(409, (object) (this.ParentObject as IDBLifecycleLevel).LevelName);
  }

  public void CancelWrite() => this.CloseBlob();

  public byte[] ReadDataBlock(int dataBlockSize)
  {
    if (this.BlobState != BlobAttributeStates.OpenedForRead)
      throw new KernelException(LocalizationHolder.rm.GetString(sc_12563.ssp_appserver_12569()));
    int num = dataBlockSize <= 0 ? this._DataBlockSize : dataBlockSize;
    byte[] numArray;
    if (this.TemporaryAttribute)
    {
      numArray = this.GetCurrentTempBlob().ReadDataBlock(num);
      if (numArray.Length == 0)
        this.CloseBlob();
    }
    else
    {
      numArray = this._FileStruct.FileBody != null ? this._Reader.ReadBytes(num) : new byte[0];
      if (numArray.Length == 0 || this._FileStruct.FileBody.Position == this._FileStruct.FileBody.Length)
        this.CloseBlob();
    }
    return numArray;
  }

  public byte[] ReadDataBlock() => this.ReadDataBlock(0);

  protected virtual void ValidateOpenBlob(bool checkAccess)
  {
  }

  BlobInformation IBlobReader.OpenBlob(int dataBlockSize)
  {
    if (this.TemporaryAttribute)
      throw new KernelException("Для временных атрибутов нельзя вызывать метод IBlobReader.OpenBlob()");
    this.CheckForClosed();
    this.ValidateOpenBlob(dataBlockSize >= 0);
    BlobInformation blobInformation = new BlobInformation();
    IBlobStorage storage = this.StoragesPool.GetStorage(this.StorageID, (IUserSession) this.UserSession);
    try
    {
      this._FileStruct = storage.GetFileStruct(this.AsInteger, dataBlockSize > -1);
      blobInformation.ArcMethod = this._FileStruct.ArcMethod;
      blobInformation.BlobID = this._FileStruct.FileID;
      blobInformation.FileName = this._FileStruct.FileName;
      blobInformation.ModifyDate = this._FileStruct.ModifyDate + this.UserSession.TimeZoneOffset;
      blobInformation.Note = this._FileStruct.Note;
      blobInformation.PackedFileSize = this._FileStruct.PacketFileSize;
      blobInformation.RealFileSize = this._FileStruct.RealFileSize;
      blobInformation.Author = this._FileStruct.Author;
      blobInformation.FileType = this._FileStruct.FileType;
      if (dataBlockSize < 0)
      {
        this.BlobState = BlobAttributeStates.Closed;
      }
      else
      {
        this._DataBlockSize = dataBlockSize != 0 ? dataBlockSize : (blobInformation.PackedFileSize <= (long) int.MaxValue ? Convert.ToInt32(blobInformation.PackedFileSize) : int.MaxValue);
        if (this._FileStruct.FileBody != null)
          this._Reader = new BinaryReader(this._FileStruct.FileBody);
        this.BlobState = BlobAttributeStates.OpenedForRead;
      }
    }
    finally
    {
      this.StoragesPool.ReleaseStorage(storage);
    }
    return blobInformation;
  }

  public void Dispose() => this.CloseBlob();

  public override bool AsBoolean
  {
    set => throw new OperationNotApplicableException();
  }

  public override double AsDouble
  {
    set => throw new OperationNotApplicableException();
  }

  public override long AsInteger
  {
    set => throw new OperationNotApplicableException();
  }

  protected override object GetDefaultValue()
  {
    IDbManager dataManager = this.UserSession.DataManager;
    return (object) dataManager.DataProvider.NextGeneratorValue("IMS_FILE_ID_GEN", dataManager);
  }

  protected virtual bool InitDefaultFileInfoStruct(long fileID)
  {
    if (!this.TemporaryAttribute)
    {
      if (this._FileStruct == null)
        this._FileStruct = new FileInfoStruct();
      this._FileStruct.ArcMethod = ArcMethods.NotPacked;
      this._FileStruct.FileID = fileID;
      this._FileStruct.FileName = "";
      this._FileStruct.ModifyDate = DateTime.UtcNow;
      this._FileStruct.Note = "";
      this._FileStruct.ObjectLinkID = this.DBObjectID;
      this._FileStruct.PacketFileSize = 0L;
      this._FileStruct.RealFileSize = 0L;
      this._FileStruct.AttributeID = this.AttributeID;
    }
    return true;
  }

  protected override void SetDefaultValue(object defValue)
  {
    if (this.TemporaryAttribute)
      return;
    base.SetDefaultValue(defValue);
    this.SetCalculatedValue((object) Convert.ToInt64(defValue), true);
    long storageId4Object = !this.IsObjectAttribute ? 0L : this.StoragesPool.GetStorageID4Object((IUserSession) this.UserSession, this.ParentObject as IDBObject);
    if (storageId4Object == 0L)
      base.AsDouble = Convert.ToDouble(this.UserSession.ActiveStorageID);
    else
      base.AsDouble = (double) storageId4Object;
    IBlobStorage storage = this.StoragesPool.GetStorage(this.StorageID, (IUserSession) this.UserSession);
    try
    {
      if (!this.InitDefaultFileInfoStruct(Convert.ToInt64(defValue)))
        return;
      storage.SetFileStruct(this._FileStruct);
    }
    finally
    {
      this.StoragesPool.ReleaseStorage(storage);
    }
  }

  protected override void DoDeleteValue()
  {
    this.CheckForClosed();
    this.DeleteBlobFromStorage();
    base.DoDeleteValue();
  }

  private void DeleteFromStorage()
  {
    this.CheckForClosed();
    for (int index = 0; index < this.ValuesCount; ++index)
    {
      this.Index = index;
      this.DeleteBlobFromStorage();
    }
  }

  internal override void PurgeValue()
  {
    this.CheckForClosed();
    try
    {
      this.DeleteBlobFromStorage();
    }
    catch
    {
    }
    if (this.ValuesCount > 1)
      base.DoDeleteValue();
    else
      this.DoClear();
  }

  private void DeleteBlobFromStorage()
  {
    if (this.TemporaryAttribute || this.StorageID <= 0L)
      return;
    IDbManager dataManager = this.UserSession.DataManager;
    IBlobStorage storage = this.StoragesPool.GetStorage(this.StorageID, (IUserSession) this.UserSession);
    try
    {
      if (BlobStoragesPool.DelayedPurge)
        storage.DataManager.ExecuteNonQuery($"UPDATE {storage.StorageName} SET F_ATTRIBUTE_ID = {-2000} WHERE F_FILE_ID = :blobID", storage.DataManager.Parameter("blobID", (object) this.AsInteger));
      else
        storage.DeleteFile(this.AsInteger);
    }
    finally
    {
      this.StoragesPool.ReleaseStorage(storage);
    }
    this.DeleteFromFileNamesTable(dataManager);
  }

  private void DeleteFromFileNamesTable(IDbManager db)
  {
    if (this.TemporaryAttribute || this.UserSession.IdentHelper.FileAttributeID != this.AttributeID)
      return;
    db.ExecuteNonQuery("DELETE FROM IMS_FILENAMES WHERE F_KEY = :objID AND F_FILENAME = :fname", db.Parameter("objID", (object) this.DBObjectID), db.Parameter("fname", (object) this.AsString.Trim().ToUpper()));
  }

  protected override int DoDelete()
  {
    this.DeleteFromStorage();
    return base.DoDelete();
  }

  internal override void Purge(bool purgeOwner)
  {
    try
    {
      this.DeleteFromStorage();
    }
    catch (Exception ex)
    {
      this.UserSession.EventLogHelper.AddToTrace($"Ошибка удаления файла '{this.AsString}' из атрибута '{this.Name}' объекта N{this.DBObjectID}: {ex.Message}", Consts.traceAlways, "StorageError.log");
      this.UserSession.EventLogHelper.AddToTrace(ex.StackTrace, Consts.traceAlways, "StorageError.log");
      bool flag = true;
      IBlobStorage storage = this.StoragesPool.GetStorage(this.StorageID, (IUserSession) this.UserSession);
      try
      {
        storage.DataManager.ExecuteScalar($"SELECT F_FILE_ID FROM {storage.StorageName} WHERE F_FILE_ID = -1");
      }
      catch
      {
        flag = false;
      }
      finally
      {
        this.StoragesPool.ReleaseStorage(storage);
      }
      if (flag)
        throw;
    }
    base.Purge(purgeOwner);
  }

  public override object Value
  {
    get
    {
      return this._ValueContentMode == ColumnContents.ID ? (object) this.AsInteger : (object) this.AsString;
    }
    set
    {
      BlobValue blobValue = value is BlobValue ? value as BlobValue : throw new KernelException("Для длинных двоичных данных в поле Value можно записывать только объекты типа BlobValue");
      blobValue.Index = this.Index;
      BlobAttributesHelper.SetBlobValues((IDBAttribute) this, new object[1]
      {
        (object) blobValue
      }, this.UserSession);
    }
  }

  protected override string GetInViewFieldName(AttributeValueField fldType)
  {
    string inViewFieldName;
    switch (fldType)
    {
      case AttributeValueField.Integer:
        inViewFieldName = $"F{this.AttributeID.ToString()}ID";
        break;
      case AttributeValueField.Double:
        inViewFieldName = $"F{this.AttributeID.ToString()}ID2";
        break;
      case AttributeValueField.String:
        inViewFieldName = "F" + this.AttributeID.ToString();
        break;
      case AttributeValueField.Date:
        inViewFieldName = $"F{this.AttributeID.ToString()}ID3";
        break;
      default:
        inViewFieldName = string.Empty;
        break;
    }
    return inViewFieldName;
  }

  protected override string GetDescription() => this.AsString;

  internal override void InternalClear()
  {
    if (!this.TemporaryAttribute)
    {
      this.UserSession.DataManager.ExecuteNonQuery($"UPDATE {this.ValuesTableName} SET F_STRING_VALUE = NULL WHERE {this.ValuesKeyName} = :p0 AND F_ATTRIBUTE_ID = :p1 AND F_INLIST_ID = :p2", this.UserSession.DataManager.Parameter("p0", (object) this.DBObjectID), this.UserSession.DataManager.Parameter("p1", (object) this.AttributeID), this.UserSession.DataManager.Parameter("p2", (object) this.Index));
      this.UpdateViewValue("F" + this.AttributeID.ToString(), (object) DBNull.Value, this.DBObjectID);
      this.DeleteFromFileNamesTable(this.UserSession.DataManager);
      IBlobStorage storage = this.StoragesPool.GetStorage(this.StorageID, (IUserSession) this.UserSession);
      try
      {
        storage.Clear(this.AsInteger);
      }
      finally
      {
        this.StoragesPool.ReleaseStorage(storage);
      }
    }
    this._ValuesTable[this.Index]["F_STRING_VALUE"] = (object) DBNull.Value;
    this.ParentObject.AfterSetAdditionalAttributeValue((IDBAttribute) this);
    this.ParentObject.AfterSetComplexAttributeValue((IDBAttribute) this);
  }

  internal void ChangeObjectLinkID(long toID)
  {
    if (this.TemporaryAttribute)
      return;
    IBlobStorage storage = this.StoragesPool.GetStorage(this.StorageID, (IUserSession) this.UserSession);
    try
    {
      int index1 = this.Index;
      for (int index2 = 0; index2 < this.ValuesCount; ++index2)
      {
        this.Index = index2;
        storage.ChangeObjectLinkID(this.AsInteger, toID);
      }
      this.Index = index1;
    }
    finally
    {
      this.StoragesPool.ReleaseStorage(storage);
    }
  }

  public virtual bool IsContentFile()
  {
    return this._FileStruct == null || this._FileStruct.FileType == FileTypes.ftNormal;
  }

  public bool IsCloneFile(string flName, IDBAttribute sourceAttr)
  {
    bool flag = false;
    if (!this.TemporaryAttribute && this.AsDouble == sourceAttr.AsDouble)
    {
      IBlobStorage storage = this.StoragesPool.GetStorage(this.StorageID, (IUserSession) this.UserSession);
      try
      {
        if (storage is DBBlobStorage)
        {
          (storage as DBBlobStorage).CloneFile(sourceAttr.AsInteger, this.AsInteger, flName, this.DBObjectID, this.UserSession.UserID, this.AttributeID);
          flag = true;
        }
      }
      finally
      {
        this.StoragesPool.ReleaseStorage(storage);
      }
      if (flag)
      {
        if (((IBlobWriter) this).OpenBlob((sourceAttr as IBlobReader).OpenBlob(-1) with
        {
          FileName = flName
        }, true))
          this.CloseBlob();
      }
    }
    return flag;
  }

  public void RemoveToStorage(long toStorageID)
  {
    if (!this.UserSession.IsAdmin)
      throw new KernelExceptionID(126);
    IBlobStorage storage1 = this.StoragesPool.GetStorage(toStorageID, this.Session);
    try
    {
      IBlobStorage storage2 = this.StoragesPool.GetStorage(Convert.ToInt64(this.AsDouble), this.Session);
      try
      {
        FileInfoStruct fileStruct = storage2.GetFileStruct(this.AsInteger, true);
        this.UserSession.StartTransaction();
        try
        {
          storage1.SetFileStruct(fileStruct);
          this.DirectSetValue("F_DOUBLE_VALUE", (object) storage1.StorageID);
          storage2.DeleteFile(this.AsInteger);
          this.UserSession.Commit();
        }
        catch
        {
          this.UserSession.Rollback();
          throw;
        }
      }
      finally
      {
        this.StoragesPool.ReleaseStorage(storage2);
      }
    }
    finally
    {
      this.StoragesPool.ReleaseStorage(storage1);
    }
  }
}
