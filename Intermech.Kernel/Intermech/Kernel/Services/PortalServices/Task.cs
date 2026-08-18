// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.Task
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;


namespace Intermech.Kernel.Services.PortalServices;

public abstract class Task : IBlobSaved, ITask, IDisposable
{
  private DetailedLog _detailedLog;

  protected DetailedLog DetailTaskLog
  {
    get
    {
      if (this._detailedLog == null && this.GetSettingsService().CreateDetailTaskLog)
        this._detailedLog = new DetailedLog($"{this.Type}_{this.Priority}", this.Name);
      return this._detailedLog;
    }
  }

  protected abstract ITransferSettingsService GetSettingsService();

  public Task()
  {
  }

  public Task(
    long userID,
    Guid userGuid,
    string name,
    TaskType type,
    TaskPriority priority,
    ITransferedObject[] units)
    : this(userID, userGuid, name, type, priority, units, true)
  {
  }

  public Task(
    long userID,
    Guid userGuid,
    string name,
    TaskType type,
    TaskPriority priority,
    ITransferedObject[] units,
    bool enabled)
    : this()
  {
    this.Units = units;
    this.UserID = userID;
    this.UserGuid = userGuid;
    this.TaskID = 0L;
    this.Priority = priority;
    this.Status = TaskStatus.Waiting;
    this.Error = (Exception) null;
    this.Name = name;
    this.Type = type;
    this.Percent = 0.0;
    this.LastStepIDCompleted = -1;
    this.Enabled = enabled;
  }

  protected bool EnableDetailTaskLog => this.DetailTaskLog != null;

  protected void WriteToDetailedLog(string message)
  {
    if (!this.EnableDetailTaskLog)
      return;
    this.DetailTaskLog.Write(message);
  }

  private void CheckPortalVersion(IUserSession session, IPortalConnector connector)
  {
    if (!((ISiteServerService) session.GetCustomService(typeof (ISiteServerService))).Settings.ValidateVersion)
      return;
    string portalVersion = connector.PortalVersion;
    if (string.IsNullOrEmpty(portalVersion))
      return;
    string str = this.GetType().Assembly.GetName().Version.ToString();
    string[] strArray1 = portalVersion.Split('.');
    string[] strArray2 = str.Split('.');
    if (!(strArray1[0] == strArray2[0]) || !(strArray1[1] == strArray2[1]))
      throw new Exception($"Несоответствие версий портала ({portalVersion}) и узла ({str})!");
  }

  public virtual void BeginTask(IUserSession session, IEventLogHelper eventHelper)
  {
    this.FireStart(new TaskStartEventArgs(session));
    IPortalConnector customService = (IPortalConnector) session.GetCustomService(typeof (IPortalConnector));
    try
    {
      Guid guid = customService.Login(session.SessionGUID);
      if (SiteTraceLog.Enabled)
        SiteTraceLog.Write($"Task {this.Name} started with connectionGuid={guid}...");
      this.CheckPortalVersion(session, customService);
      try
      {
        this.OnTaskStarted(session, guid, customService);
        for (int unitIndex = this.LastStepIDCompleted + 1; unitIndex < this.Units.Length; ++unitIndex)
        {
          ITransferedObject unit = this.Units[unitIndex];
          if (unit != null)
          {
            if (SiteTraceLog.Enabled)
              SiteTraceLog.Write($"begining unit={unit.GUID} connectionGuid={guid}");
            this.Begining(session, guid, customService, unit);
            double num = 50.0 * (double) unitIndex / (double) this.Units.Length;
            TaskStepCompletedEventArgs e = new TaskStepCompletedEventArgs(session, num, false, unit, unitIndex);
            if (Math.Round(this.Percent) != Math.Round(num))
            {
              this.Percent = num;
              e.PercentChanged = true;
            }
            this.LastStepIDCompleted = unitIndex;
            this.FireStepCompleted(e);
            if (SiteTraceLog.Enabled)
              SiteTraceLog.Write($"end unit={unit.GUID} connectionGuid={guid}");
          }
        }
      }
      finally
      {
        if (guid != Guid.Empty && customService != null)
          customService.Logout(guid);
      }
      if (SiteTraceLog.Enabled)
        SiteTraceLog.Write($"task completed connectionGuid={guid}");
      this.OnTaskCompleted(session, customService);
      this.FireTaskStatusChanged(new TaskStatusChangedEventArgs(session, TaskStatus.Successfully));
    }
    catch (ThreadAbortException ex)
    {
      if (SiteTraceLog.Enabled)
        SiteTraceLog.Write("catch ThreadAbortException");
      this.OnTaskError(session, customService, (Exception) ex);
      this.FireSaveData(new TaskSaveDataEventArgs(session));
      this.FireTaskStatusChanged(new TaskStatusChangedEventArgs(session, TaskStatus.Aborted));
    }
    catch (Exception ex)
    {
      if (SiteTraceLog.Enabled)
        SiteTraceLog.Write("catch Exception", ex);
      this.Error = ex;
      this.OnTaskError(session, customService, ex);
      this.FireSaveData(new TaskSaveDataEventArgs(session));
      this.FireTaskStatusChanged(new TaskStatusChangedEventArgs(session, TaskStatus.Erroneous));
      throw;
    }
  }

  public event TaskStartEventHandler TaskStartEvent;

  public event TaskStepCompletedEventHandler TaskStepCompletedEvent;

  public event TaskStatusChangedEventHandler TaskStatusChangedEvent;

  public event TaskObjectImportedEventHandler TaskObjectImportedEvent;

  public event TaskSaveDataEventHandler TaskSaveDataEvent;

  public ITransferedObject[] Units { get; set; }

  public long UserID { get; set; }

  public TaskPriority Priority { get; set; }

  public Exception Error { get; set; }

  public long TaskID { get; set; }

  public Guid UserGuid { get; }

  public TaskStatus Status { get; set; }

  public string Name { get; set; }

  public TaskType Type { get; set; }

  public double Percent { get; set; }

  public int LastStepIDCompleted { get; set; }

  public bool Enabled { get; set; }

  protected void FireStart(TaskStartEventArgs e)
  {
    if (this.TaskStartEvent == null)
      return;
    this.Status = TaskStatus.Transmitting;
    this.TaskStartEvent((object) this, e);
  }

  protected void FireStepCompleted(TaskStepCompletedEventArgs e)
  {
    TaskStepCompletedEventHandler stepCompletedEvent = this.TaskStepCompletedEvent;
    if (stepCompletedEvent == null)
      return;
    stepCompletedEvent((object) this, e);
  }

  protected void FireTaskStatusChanged(TaskStatusChangedEventArgs e)
  {
    if (this.TaskStatusChangedEvent == null)
      return;
    this.Status = e.NewStatus;
    this.TaskStatusChangedEvent((object) this, e);
  }

  protected void FireImportedObject(TaskObjectImportedEventArgs e)
  {
    TaskObjectImportedEventHandler objectImportedEvent = this.TaskObjectImportedEvent;
    if (objectImportedEvent == null)
      return;
    objectImportedEvent((object) this, e);
  }

  protected void FireSaveData(TaskSaveDataEventArgs e)
  {
    TaskSaveDataEventHandler taskSaveDataEvent = this.TaskSaveDataEvent;
    if (taskSaveDataEvent == null)
      return;
    taskSaveDataEvent((object) this, e);
  }

  protected virtual void OnTaskCompleted(IUserSession session, IPortalConnector connector)
  {
  }

  protected virtual void OnTaskStarted(
    IUserSession session,
    Guid connectionGuid,
    IPortalConnector connector)
  {
  }

  protected virtual void OnTaskError(
    IUserSession session,
    IPortalConnector connector,
    Exception ex)
  {
  }

  protected virtual void Begining(
    IUserSession session,
    Guid connectionGuid,
    IPortalConnector connector,
    ITransferedObject unit)
  {
  }

  public virtual byte[] Save(IUserSession session, IDBObject backupObject) => (byte[]) null;

  public virtual void Load(IUserSession session, IDBObject backupObject, byte[] bytes)
  {
  }

  protected void WriteListGuid(BinaryWriter bw, List<Guid> list)
  {
    if (list != null && list.Count > 0)
    {
      bw.Write(list.Count);
      foreach (Guid guid in list)
        this.WriteGuid(bw, guid);
    }
    else
      bw.Write(0);
  }

  protected List<Guid> ReadListGuid(BinaryReader br)
  {
    int num = br.ReadInt32();
    List<Guid> guidList = new List<Guid>();
    if (num > 0)
    {
      for (int index = 0; index < num; ++index)
        guidList.Add(this.ReadGuid(br));
    }
    return guidList;
  }

  protected void WriteGuid(BinaryWriter bw, Guid guid) => bw.Write(guid.ToString().ToCharArray());

  protected void WriteString(BinaryWriter bw, string str)
  {
    if (!string.IsNullOrEmpty(str))
    {
      bw.Write(str.Length);
      bw.Write(str.ToCharArray());
    }
    else
      bw.Write(0);
  }

  protected string ReadString(BinaryReader br) => Helper.GetString(br.ReadInt32(), br);

  protected Guid ReadGuid(BinaryReader br)
  {
    string g = Helper.GetString(36, br);
    return !string.IsNullOrEmpty(g) ? new Guid(g) : Guid.Empty;
  }

  public override bool Equals(object obj) => obj is Task && ((Task) obj).TaskID == this.TaskID;

  public override int GetHashCode() => this.TaskID.GetHashCode();

  public abstract void LoadTransferedObjects(BinaryReader reader);

  protected string GeneralIncludesInfo()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("\\bСостав задачи:\\b0\\line");
    foreach (TransferedObjectCategory transferedObjectCategory in Enum.GetValues(typeof (TransferedObjectCategory)))
    {
      TransferedObjectCategory category = transferedObjectCategory;
      int num = ((IEnumerable<ITransferedObject>) this.Units).Count<ITransferedObject>((Func<ITransferedObject, bool>) (x => x.Category == category));
      if (num > 0)
      {
        stringBuilder.Append($"{EnumDescConverter.GetEnumDescription((Enum) category)}: {num}");
        stringBuilder.Append("\\line");
      }
    }
    return stringBuilder.ToString();
  }

  public virtual string GetIncludesInfo(IUserSession session)
  {
    if (this.Units == null || this.Units.Length == 0)
      return string.Empty;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("{\\rtf1\\ansi");
    stringBuilder.Append(this.GeneralIncludesInfo());
    stringBuilder.AppendLine("}");
    return stringBuilder.ToString();
  }

  public void CloseLog()
  {
    if (this._detailedLog == null)
      return;
    this._detailedLog.Close();
  }

  public void Dispose() => this.CloseLog();

  public virtual void OnTaskDelete(Guid connectionGuid, IPortalConnector connector)
  {
  }
}
