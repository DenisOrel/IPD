// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBTask
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.IO;
using Intermech.Kernel.Services;
using Intermech.Kernel.Services.PortalServices;
using Intermech.Localization;
using System;
using System.Data;
using System.IO;
using System.Text;


namespace Intermech.Kernel;

public class DBTask(UserSession uSession, DataTable objectParams) : DBObject(uSession, objectParams), IDBTask
{
  private readonly string _errorFileName = "error.xml";
  private readonly object _syncRoot = new object();

  public void LoadTaskFromBase(ITask task)
  {
    IDBAttribute attributeByGuid1 = this.GetAttributeByGuid(PortalConsts.attributeTaskType);
    task.Type = (TaskType) attributeByGuid1.AsInteger;
    this.LoadTaskData(task);
    IDBAttribute attributeByGuid2 = this.GetAttributeByGuid(new Guid("cad00020-306c-11d8-b4e9-00304f19f545"));
    task.Name = attributeByGuid2 != null ? attributeByGuid2.AsString : string.Empty;
    task.Error = (Exception) null;
    IDBAttribute attributeByGuid3 = this.GetAttributeByGuid(PortalConsts.attributePriority);
    task.Priority = (TaskPriority) attributeByGuid3.AsInteger;
    IDBAttribute attributeByGuid4 = this.GetAttributeByGuid(PortalConsts.attributePercent);
    task.Percent = attributeByGuid4.AsDouble;
    IDBAttribute attributeByGuid5 = this.GetAttributeByGuid(PortalConsts.attributeTaskStatus);
    task.Status = (TaskStatus) attributeByGuid5.AsInteger;
    IDBAttribute attributeByGuid6 = this.GetAttributeByGuid(PortalConsts.attributeTaskUser);
    task.UserID = attributeByGuid6 != null ? attributeByGuid6.AsInteger : this.Session.IdentHelper.SysdbaID;
    IDBAttribute attributeByGuid7 = this.GetAttributeByGuid(PortalConsts.attributeLastStepIDCompleted);
    task.LastStepIDCompleted = Convert.ToInt32(attributeByGuid7.AsInteger);
    IDBAttribute attributeByGuid8 = this.GetAttributeByGuid(PortalConsts.attributeTaskTransferEnabled);
    task.Enabled = attributeByGuid8.AsBoolean;
    IDBAttribute attributeByGuid9 = this.GetAttributeByGuid(PortalConsts.attributeTaskFiles);
    if (attributeByGuid9.IsNull)
      return;
    int fileIndex = DBTask.GetFileIndex(attributeByGuid9, BackupConsts.HeaderFileName);
    if (fileIndex < 0)
      return;
    attributeByGuid9.Index = fileIndex;
    IBlobReader blobReader = attributeByGuid9 as IBlobReader;
    BlobInformation blobInformation = blobReader.OpenBlob(0);
    try
    {
      if (blobInformation.RealFileSize <= 0L)
        return;
      using (MemoryStream input = new MemoryStream(blobReader.ReadDataBlock(0)))
      {
        input.Position = 0L;
        BinaryReader reader = new BinaryReader((Stream) input, Encoding.UTF8);
        try
        {
          task.LoadTransferedObjects(reader);
        }
        finally
        {
          reader.Close();
        }
      }
    }
    finally
    {
      blobReader.CloseBlob();
    }
  }

  public string GetIncludesInfo(Guid sessionGuid)
  {
    return TasksHelper.GetTaskByID(this.Session, ServerServices.GetService(typeof (IPortalTasksQueue)) as PortalTasksQueue, (IDBObject) this).GetIncludesInfo(UserSession.GetSessionByID(sessionGuid));
  }

  public void SaveTaskToBase(ITask newTask, int taskNo)
  {
    this.GetAttributeByGuid(new Guid("cad00020-306c-11d8-b4e9-00304f19f545")).AsString = newTask.Name;
    lock (this._syncRoot)
    {
      if (newTask.Error != null)
        this.WriteErrorAttributes(newTask.Error);
      else
        this.ClearErrorAttributes();
    }
    this.GetAttributeByGuid(PortalConsts.attributeTaskNo).AsInteger = (long) taskNo;
    this.GetAttributeByGuid(PortalConsts.attributePriority).AsInteger = (long) newTask.Priority;
    this.GetAttributeByGuid(PortalConsts.attributePercent).AsDouble = newTask.Percent;
    this.GetAttributeByGuid(PortalConsts.attributeTaskStatus).AsInteger = (long) newTask.Status;
    this.GetAttributeByGuid(PortalConsts.attributeTaskType).AsInteger = (long) newTask.Type;
    this.GetAttributeByGuid(PortalConsts.attributeTaskUser).AsInteger = newTask.UserID;
    this.GetAttributeByGuid(PortalConsts.attributeTaskTransferEnabled).AsBoolean = newTask.Enabled;
    IDBAttribute attributeByGuid = this.GetAttributeByGuid(PortalConsts.attributeTaskFiles);
    this.SetTaskFile(attributeByGuid, newTask.Save((IUserSession) this.UserSession, (IDBObject) this), BackupConsts.TaskDataFileName);
    if (newTask.Units == null)
      return;
    using (ImChunkedStream output = new ImChunkedStream())
    {
      BinaryWriter writer = new BinaryWriter((Stream) output, Encoding.UTF8);
      try
      {
        for (int index = 0; index < newTask.Units.Length; ++index)
          TransferedObjectHelper.WriteTo(writer, newTask.Units[index]);
        this.SetTaskFile(attributeByGuid, output.ToArray(), BackupConsts.HeaderFileName);
      }
      finally
      {
        writer.Close();
      }
    }
  }

  public static int GetFileIndex(IDBAttribute attribute, string fileName)
  {
    int fileIndex = -1;
    if (!attribute.IsNull)
    {
      for (int index = 0; index < attribute.ValuesCount; ++index)
      {
        attribute.Index = index;
        if (!attribute.IsNull)
        {
          IBlobReader blobReader = attribute as IBlobReader;
          BlobInformation blobInformation = blobReader.OpenBlob(-1);
          try
          {
            if (string.IsNullOrEmpty(blobInformation.Note) || !blobInformation.Note.Equals(fileName))
            {
              if (!string.IsNullOrEmpty(blobInformation.FileName))
              {
                if (!blobInformation.FileName.Equals(fileName))
                  continue;
              }
              else
                continue;
            }
            fileIndex = index;
            break;
          }
          finally
          {
            blobReader.CloseBlob();
          }
        }
      }
    }
    else
      fileIndex = 0;
    return fileIndex;
  }

  private void SetTaskFile(IDBAttribute attribute, byte[] data, string fileName)
  {
    int fileIndex = DBTask.GetFileIndex(attribute, fileName);
    if (fileIndex == -1)
      attribute.AddValue((object) null);
    else if (fileIndex > 0)
      attribute.Index = fileIndex;
    this.WriteBlob(attribute as IBlobWriter, data, fileName);
  }

  public void SaveTaskData(ITask task)
  {
    this.SetTaskFile(this.GetAttributeByGuid(PortalConsts.attributeTaskFiles), task.Save((IUserSession) this.UserSession, (IDBObject) this), BackupConsts.TaskDataFileName);
  }

  public void SetError(Exception ex)
  {
    try
    {
      lock (this._syncRoot)
      {
        if (ex == null)
          this.ClearErrorAttributes();
        else
          this.WriteErrorAttributes(ex);
      }
    }
    catch (Exception ex1)
    {
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_999"), (object) this.NameInMessages, (object) Intermech.Kernel.Services.PortalServices.Helper.FormingLogError(ex1)));
    }
  }

  public void SetUnitCompleted(TaskStepCompletedEventArgs e)
  {
    try
    {
      this.SetPercent(e.Percent);
      this.GetAttributeByGuid(PortalConsts.attributeLastStepIDCompleted).AsInteger = (long) e.UnitIndex;
    }
    catch (Exception ex)
    {
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1000"), (object) this.NameInMessages, (object) Intermech.Kernel.Services.PortalServices.Helper.FormingLogError(ex)));
    }
  }

  public void SetPercent(double newPercent)
  {
    try
    {
      lock (this._syncRoot)
      {
        IDBAttribute attributeByGuid = this.GetAttributeByGuid(PortalConsts.attributePercent);
        if (attributeByGuid == null)
          return;
        attributeByGuid.Value = (object) Math.Round(newPercent);
      }
    }
    catch (Exception ex)
    {
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1001"), (object) this.NameInMessages, (object) Intermech.Kernel.Services.PortalServices.Helper.FormingLogError(ex)));
    }
  }

  public void SetStatus(TaskStatus newStatus)
  {
    try
    {
      lock (this._syncRoot)
      {
        IDBAttribute attributeByGuid = this.GetAttributeByGuid(PortalConsts.attributeTaskStatus);
        if (attributeByGuid == null)
          return;
        attributeByGuid.Value = (object) (int) newStatus;
      }
    }
    catch (Exception ex)
    {
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1002"), (object) this.NameInMessages, (object) Intermech.Kernel.Services.PortalServices.Helper.FormingLogError(ex)));
    }
  }

  public void LoadTaskData(ITask task)
  {
    IDBAttribute attributeByGuid = this.GetAttributeByGuid(PortalConsts.attributeTaskFiles);
    int fileIndex = DBTask.GetFileIndex(attributeByGuid, BackupConsts.TaskDataFileName);
    if (fileIndex == -1)
      throw new Exception("Не найден файл с данными задачи!");
    if (fileIndex > 0)
      attributeByGuid.Index = fileIndex;
    IBlobReader blobReader = (IBlobReader) attributeByGuid;
    blobReader.OpenBlob(0);
    try
    {
      task.Load((IUserSession) this.UserSession, (IDBObject) this, blobReader.ReadDataBlock());
    }
    finally
    {
      blobReader.CloseBlob();
    }
  }

  public void WriteErrorAttributes(Exception ex)
  {
    (this.GetAttributeByGuid(PortalConsts.attributeError) ?? this.Attributes.AddAttribute(this.UserSession.GetAttributeType(PortalConsts.attributeError).AttributeID, false)).AsString = ex.Message;
    IDBAttribute dbAttribute = this.GetAttributeByGuid(PortalConsts.attributeFileError) ?? this.Attributes.AddAttribute(this.UserSession.GetAttributeType(PortalConsts.attributeFileError).AttributeID, false);
    using (MemoryStream memoryStream = new MemoryStream())
    {
      if (!ExceptionHelper.ExceptionToXML(ex, (IPluginManager) null).Save((Stream) memoryStream))
        return;
      this.WriteBlob(dbAttribute as IBlobWriter, memoryStream.ToArray(), this._errorFileName);
    }
  }

  private void ClearErrorAttributes()
  {
    this.GetAttributeByGuid(PortalConsts.attributeError)?.Delete(0L);
    this.GetAttributeByGuid(PortalConsts.attributeFileError)?.Delete(0L);
  }

  private void WriteBlob(IBlobWriter blobWriter, byte[] data, string fileName)
  {
    long int64 = Convert.ToInt64(data.Length);
    if (!blobWriter.OpenBlob(new BlobInformation(int64, int64, DateTime.Now, fileName, ArcMethods.NotPacked, fileName), false))
      return;
    blobWriter.WriteDataBlock(data);
  }

  public override int Delete(long deleteMode)
  {
    if (deleteMode != (long) PortalConsts.DeleteWithoutFiles && ServerServices.GetService(typeof (IPortalTasksQueue)) is PortalTasksQueue service)
    {
      ITask taskById = TasksHelper.GetTaskByID(this.Session, service, (IDBObject) this);
      IPortalConnector customService = (IPortalConnector) this.Session.GetCustomService(typeof (IPortalConnector));
      Guid guid = customService.Login(this.Session.SessionGUID);
      try
      {
        taskById.OnTaskDelete(guid, customService);
      }
      finally
      {
        if (guid != Guid.Empty && customService != null)
          customService.Logout(guid);
      }
    }
    return base.Delete(deleteMode != (long) PortalConsts.DeleteWithoutFiles ? deleteMode : 0L);
  }

  public override long AddEvent(
    long objectID,
    long relationID,
    ActionType eventType,
    EventlogRecordType auditType,
    string note)
  {
    if (eventType.Equals((object) ActionType.Create) && auditType.Equals((object) EventlogRecordType.AccessGranted))
    {
      IDBAttribute attributeByGuid = this.GetAttributeByGuid(PortalConsts.attributePublishInformation);
      string str = string.Empty;
      if (attributeByGuid is IMemoReader memoReader && memoReader.OpenMemo(0) > 0)
      {
        str = new string(memoReader.ReadDataBlock());
        memoReader.CloseMemo();
      }
      if (!string.IsNullOrEmpty(note))
      {
        StringBuilder stringBuilder = new StringBuilder(note);
        stringBuilder.AppendLine(str);
        note = stringBuilder.ToString();
      }
      else
        note = str;
    }
    return base.AddEvent(objectID, relationID, eventType, auditType, note);
  }
}
