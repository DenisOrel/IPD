// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.WFProcess
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Project;
using Intermech.Workflow.Server.Activities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Threading;

#nullable disable
namespace Intermech.Workflow.Server;

public class WFProcess(UserSession uSession, DataTable objectsTable) : 
  WFScheme(uSession, objectsTable),
  IProcess,
  IScheme,
  IMailObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData,
  IDBSecurityCollection,
  IDBSecurity,
  ISchemeActivityCreator
{
  private ExtProperties _extProps;
  private int _priority = -1;
  private Notifications _notifications;
  internal bool CheckAdminRights = true;
  private StringList _portalInfo;
  private bool _portalInfoLoaded;
  private long _blockUserID = -1;
  private DateTime _blockDT = DateTime.MinValue;

  public override ActivityKind Kind => ActivityKind.Process;

  internal ExtProperties ExtProps
  {
    get
    {
      return this._extProps ?? (this._extProps = new ExtProperties((IDBObject) this, wfConsts.AttrAddInfoID));
    }
  }

  public bool Executed => wfConsts.ExecStatuses.Contains(this.ProcessStatus);

  public DateTime ProcessStarted
  {
    get
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrStartedID);
      return attributeById == null ? DateTime.MinValue : attributeById.AsDateTime;
    }
    set
    {
      this.Attributes.AddAttribute(wfConsts.AttrStartedID, false, new object[1]
      {
        (object) value
      });
    }
  }

  public ActivityStatus ProcessStatus
  {
    get
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrActivityStatusID);
      return attributeById != null ? (ActivityStatus) attributeById.AsInteger : ActivityStatus.OnApproach;
    }
    set
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrActivityStatusID);
      int num = (int) value;
      int asInteger = attributeById == null ? 0 : (int) attributeById.AsInteger;
      if (asInteger == num)
        return;
      this.SetStatus(attributeById, value, (ActivityStatus) asInteger);
    }
  }

  private void SetStatus(IDBAttribute attr, ActivityStatus value, ActivityStatus oldStatus)
  {
    bool flag = value == ActivityStatus.Terminated;
    if (flag)
    {
      if (this.CheckAdminRights)
        this.CheckAccess(ActionType.wfAdminProcess);
      if (!this.UserSession.InTransaction)
        this.UserSession.StartTransaction();
    }
    try
    {
      this.Attributes.AddAttribute(wfConsts.AttrActivityStatusID, false, new object[1]
      {
        (object) value
      });
      if (flag)
      {
        this.SendNotification(this.Notifications.AbortNotify);
        this.ForceGetActivities = true;
        List<WFActivity> activities = this.Activities;
        this.ForceGetActivities = false;
        WFActivity wfActivity1 = (WFActivity) null;
        for (int index = 0; index < activities.Count; ++index)
        {
          WFActivity wfActivity2 = activities[index];
          if (wfActivity2.Executed)
          {
            wfActivity1 = wfActivity2;
            wfActivity2.Abort();
          }
        }
        wfActivity1?.SendNotification(wfActivity1.Notifications.AbortNotify);
        this.UserSession.Commit();
      }
      if ((value != ActivityStatus.Completed || !GlobalMailSettings.Cfg.ClearMailFoldersOnCompletion) && (value != ActivityStatus.Terminated || !GlobalMailSettings.Cfg.ClearMailFoldersOnTermination))
        return;
      foreach (WFActivity activity in this.Activities)
      {
        if (activity.Status != ActivityStatus.OnApproach)
        {
          IDBAttribute attributeById1 = activity.GetAttributeByID(wfConsts.AttrRecipDeletionID);
          if (attributeById1 != null)
            attributeById1.AsInteger = 2L;
          IDBAttribute attributeById2 = activity.GetAttributeByID(wfConsts.AttrSenderDeletionID);
          if (attributeById2 != null)
            attributeById2.AsInteger = 2L;
        }
      }
    }
    catch
    {
      if (flag && this.UserSession.InTransaction)
        this.UserSession.Rollback();
      throw;
    }
  }

  public bool CreateActivitiesOnDemand
  {
    get
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrCreateActivitiesOnDemandID);
      return attributeById != null && attributeById.AsBoolean;
    }
    set
    {
      this.Attributes.AddAttribute(wfConsts.AttrCreateActivitiesOnDemandID, false, new object[1]
      {
        (object) value
      });
    }
  }

  public long PrototypeSchemeID
  {
    get
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrPrototypeID);
      return attributeById != null ? attributeById.AsInteger : 0L;
    }
  }

  public ProcessPriority Priority
  {
    get
    {
      if (this._priority == -1)
      {
        IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrPriorityID);
        this._priority = attributeById == null ? 0 : (int) attributeById.AsInteger;
      }
      return (ProcessPriority) this._priority;
    }
    set
    {
      this._priority = (int) value;
      this.Attributes.AddAttribute(wfConsts.AttrPriorityID, false, new object[1]
      {
        (object) (int) value
      });
    }
  }

  public Notifications Notifications
  {
    get
    {
      if (this._notifications == null || this._notifications.Invalid || !this._notifications.Loaded)
      {
        this._notifications = new Notifications((IUserSession) this.UserSession);
        IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrNotificationsID);
        if (attributeById != null)
          this._notifications.Load(attributeById);
      }
      return this._notifications;
    }
  }

  internal void SendNotification(Notification n)
  {
    if (!n.Enabled)
      return;
    n.Subject = ServerFunx.ReplaceTextMacros(n.Subject, this.Variables);
    n.Text = ServerFunx.ReplaceTextMacros(n.Text, this.Variables);
    MiscFunx.ExpandParticipants((IDBAttributable) this, n.Recips);
    foreach (Participant recip in n.Recips)
    {
      IDBObject message = ServerFunx.CreateMessage((IUserSession) this.UserSession, recip.ID, n.Subject, n.Text, this.ObjectID, this.ObjectID);
      message.GetAttributeByID(wfConsts.AttrPriorityID).AsInteger = (long) this.Priority;
      ServerFunx.CopyAttachmentsFlag((IDBObject) this, message);
    }
  }

  public void SendPeriodMessage()
  {
    if (this.ProcessStatus != ActivityStatus.Executed)
      return;
    this.SendNotification((Notification) this.Notifications.PeriodNotify);
  }

  private void DeletePeriodNotificationIfNeeded()
  {
    if (!this.Notifications.PeriodNotify.Enabled)
      return;
    IDBTimedEvents service = ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    int eventID = service.FindEvent(wfConsts.WorkflowTimerServiceGuid, 0, this.ObjectID, this.UserSession.DataManager);
    if (eventID <= 0)
      return;
    service.DeleteEventID(eventID, this.UserSession.DataManager);
  }

  public void StartProcess()
  {
    if (this.StartActivity == null)
      throw new KernelException($"Действие старт не найдено. Идентификатор процесса: '{this.ObjectID}'.");
    if (this.UserSession.InTransaction)
    {
      if (!(ApplicationServices.Container.GetService(typeof (IDelayProcessStarter)) is IDelayProcessStarter service))
        throw new KernelException($"Невозможно запустить процесс '{this.Caption}' [{this.ObjectID}]. Не найдена служба отложенного запуска.");
      this.StartActivity.SaveVariables();
      service.AddProcessToQueue(this.UserSession.SessionGUID, this.ObjectID);
    }
    else
    {
      this.SendNotification(this.Notifications.StartNotify);
      if (this.Notifications.PeriodNotify.Enabled)
      {
        IDBTimedEvents service = ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
        service.AddToTrace($"Событие N{service.AddEvent(new TimedEventProperties(0, this.Notifications.PeriodNotify.Period.GetExecTime((IDBObject) this), DateTime.MinValue, wfConsts.WorkflowTimerServiceGuid, this.ObjectID, 0L, "", 0, 0), this.UserSession.DataManager)} для объекта N{this.ObjectID} зарегистрировано.", true);
      }
      if (GlobalMailSettings.Cfg.ValidateProcessOnStart)
      {
        string message = this.Validate(true, (List<long>) null);
        if (!string.IsNullOrEmpty(message))
          throw new InvalidSchemeException(message);
      }
      this.StartActivity.SaveVariables();
      this.ForwardDataFlow((WFActivity) this.StartActivity);
      List<WFLink> nextStepLinks;
      try
      {
        this.StartActivity.FirstLaunch = true;
        nextStepLinks = this.StartActivity.ExecuteStart(-1L);
      }
      catch
      {
        this.StartActivity.FirstLaunch = false;
        throw;
      }
      this.ProcessStatus = ActivityStatus.Executed;
      this.ProcessStarted = DateTime.Now;
      new WFActivityProxy(this.ObjectID, (WFActivity) this.StartActivity).ExecuteNextAsync(this.StartActivity.ParticipantID, this.StartActivity.ObjectID, nextStepLinks, this.StartActivity.VariableList);
    }
  }

  public void StartSubProcess(long subProcessStartOwner)
  {
    if (this.StartActivity == null)
      return;
    if (GlobalMailSettings.Cfg.ValidateProcessOnStart)
    {
      string message = this.Validate(true, (List<long>) null);
      if (!string.IsNullOrEmpty(message))
        throw new InvalidSchemeException(message);
    }
    this.OwnerID = subProcessStartOwner;
    this.StartActivity.StartSubProcessAutoStep = true;
    List<WFLink> nextStepLinks = this.StartActivity.ExecuteStart(-1L);
    this.StartActivity.StartSubProcessAutoStep = false;
    this.ProcessStatus = ActivityStatus.Executed;
    this.ProcessStarted = DateTime.Now;
    new WFActivityProxy(this.ObjectID, (WFActivity) this.StartActivity, this.StartActivity.NonUserActivitiesCounter).ExecuteNextAsync(this.StartActivity.ParticipantID, this.StartActivity.ObjectID, nextStepLinks, this.StartActivity.VariableList);
  }

  private void Abort(WFActivity sender)
  {
    if (this.CheckAdminRights)
      this.CheckAccess(ActionType.wfAdminProcess);
    if (sender == null)
    {
      for (int index = this.Activities.Count - 1; index >= 0; --index)
      {
        WFActivity activity = this.Activities[index];
        if (activity.Status == ActivityStatus.Executed)
        {
          sender = activity;
          break;
        }
      }
      if (sender == null)
      {
        for (int index = this.Activities.Count - 1; index >= 0; --index)
        {
          WFActivity activity = this.Activities[index];
          switch (activity.Status)
          {
            case ActivityStatus.OnApproach:
            case ActivityStatus.Terminated:
              continue;
            default:
              sender = activity;
              goto label_14;
          }
        }
      }
    }
label_14:
    bool flag = false;
    if (this.ProcessStatus == ActivityStatus.Executed)
    {
      if (GlobalMailSettings.Cfg.ClearMailFoldersOnTermination)
      {
        foreach (DataRow row in (InternalDataCollectionBase) this.ListMessages(0, (ConditionStructure[]) null).Rows)
          this.UserSession.GetObject(Convert.ToInt64(row[0])).Delete(0L);
      }
      this.ProcessStatus = ActivityStatus.Terminated;
      DateTime now = DateTime.Now;
      this.Attributes.AddAttribute(wfConsts.AttrCompletedID, false, new object[1]
      {
        (object) now
      });
      if (this.ProcessStarted == DateTime.MinValue)
        this.ProcessStarted = now;
      flag = true;
    }
    if (!flag)
      return;
    if (ApplicationServices.Container.GetService(typeof (IEventLogHelper)) is IEventLogHelper service)
      service.AddEvent(this.ObjectID, 0L, 2, (long) wfConsts.ProcessesTypeID, this.Caption, string.Empty, ActionType.wfAbortProcess, EventlogRecordType.Information, this.UserSession.UserID, this.UserSession.ComputerName, (IUserSession) this.UserSession);
    if (sender != null)
      sender.ContinueExecAtParentProcess(false);
    else
      this.ContinueExecAtParentProcess(false);
  }

  public void StopProcess(IActivity sender, bool isAbort = false)
  {
    if (isAbort)
      this.Abort(sender as WFActivity);
    else if (this.ProcessStatus == ActivityStatus.Executed)
    {
      if (GlobalMailSettings.Cfg.ClearMailFoldersOnCompletion)
      {
        foreach (DataRow row in (InternalDataCollectionBase) this.ListMessages(0, (ConditionStructure[]) null).Rows)
          this.UserSession.GetObject(Convert.ToInt64(row[0])).Delete(0L);
      }
      this.ProcessStatus = ActivityStatus.Completed;
      DateTime now = DateTime.Now;
      this.Attributes.AddAttribute(wfConsts.AttrCompletedID, false, new object[1]
      {
        (object) now
      });
      if (this.ProcessStarted == DateTime.MinValue)
        this.ProcessStarted = now;
      this.DeletePeriodNotificationIfNeeded();
      this.SendNotification(this.Notifications.StopNotify);
    }
    foreach (WFActivity activity in this.Activities)
    {
      if (activity.Kind == ActivityKind.Timer && activity.Executed)
        activity.Abort();
    }
  }

  protected StringList PortalInfo
  {
    get
    {
      if (!this._portalInfoLoaded)
      {
        string str = this.ExtProps.Read(nameof (PortalInfo));
        this._portalInfoLoaded = true;
        if (!string.IsNullOrEmpty(str))
          this._portalInfo = new StringList()
          {
            CommaText = str
          };
      }
      return this._portalInfo;
    }
  }

  internal bool ContinueExecAtParentProcess(bool goNext)
  {
    try
    {
      if (this.PortalInfo != null)
      {
        if (this.PortalInfo.Values["Wait"] == "1")
          WorkflowPortalHandler.ContinueExecutionAtSender(this.PortalInfo, goNext, this);
      }
      else
      {
        bool flag = false;
        IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrExecHistoryID);
        if (attributeById != null && !attributeById.IsNull && this.UserSession.GetObject(Convert.ToInt64(attributeById.Values[attributeById.ValuesCount - sc_22127.ssp_workflow_server_22128(2028694753)]), false) is WFActivity wfActivity && wfActivity.WaitForCompletion)
        {
          this.ForwardDataFlow(wfActivity);
          wfActivity.NextStep(goNext);
          new WFActivityProxy(wfActivity.ProcessID, wfActivity, wfActivity.NonUserActivitiesCounter).ExecuteNextActivity(wfActivity.SenderID, wfActivity.ObjectID, wfActivity.NextStepLinks, wfActivity.VariableList);
          flag = true;
        }
        if (goNext)
        {
          if (!flag)
          {
            if (this.LinkedTaskObjectID != 0L)
            {
              Intermech.Project.Task task = StandaloneTask.Get(this.Session, this.LinkedTaskObjectID);
              if (task != null)
              {
                task.ProjectNeeded();
                if (task.Project != null)
                {
                  if (task.Project._Properties.CompleteTasksOnProcess)
                  {
                    IDBObject dbObject = task.GetObject();
                    try
                    {
                      task.SetRuntimeFlag(dbObject, RuntimeFlags.AutoComplete);
                      try
                      {
                        if (task.Status == TaskStatus.Sent)
                          task.Status = TaskStatus.Executed;
                        task.PercentCompleted = 100.0;
                      }
                      finally
                      {
                        task.SetRuntimeFlag(dbObject, RuntimeFlags.AutoComplete, false);
                      }
                    }
                    finally
                    {
                      task.ReleaseObject();
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
    catch (Exception ex)
    {
      this.ProcessStatus = ActivityStatus.Executed;
      this.GetAttributeByID(wfConsts.AttrActivityResultID)?.Delete(0L);
      this.GetAttributeByID(wfConsts.AttrCompletedID)?.Delete(0L);
      Start startActivity = this.StartActivity;
      if (startActivity != null && startActivity.ParticipantID == this.Session.IdentHelper.SystemID)
      {
        ParticipantList participants = startActivity.Participants;
        participants.Clear();
        participants.AddParticipant(ParticipantKind.User, GlobalMailSettings.Cfg.WorkflowAdminUserID);
        startActivity.SaveParticipants(true);
      }
      return false;
    }
    return true;
  }

  public bool ReplaceParticipant(long userID, long toUserID)
  {
    if (userID == toUserID)
      return false;
    bool flag1 = false;
    this.CheckAccess(ActionType.wfAdminProcess);
    this.AcquireBlock();
    try
    {
      foreach (WFActivity activity in this.Activities)
      {
        if (wfConsts.IsParticipantActivity(activity.Kind) || activity is Script script1 && script1.ExecSide == ScriptExecSide.Client || activity is RemoteProcess remoteProcess1 && remoteProcess1.Participants.Count > 0)
        {
          if (activity.Executed)
          {
            IDBAttribute attributeById = activity.GetAttributeByID(wfConsts.AttrRecipID);
            if (attributeById != null && !attributeById.IsNull && attributeById.AsInteger == userID)
            {
              if (activity.Status == ActivityStatus.Executed)
                activity.HandleTemporaryRights(false);
              attributeById.AsInteger = toUserID;
              switch (activity)
              {
                case Intermech.Workflow.Server.Activities.Task task:
                  task._participantID = toUserID;
                  break;
                case Script script:
                  if (script.ExecSide == ScriptExecSide.Client)
                  {
                    script._participantID = toUserID;
                    break;
                  }
                  break;
                case RemoteProcess remoteProcess:
                  if (remoteProcess.Participants.Count != 0)
                  {
                    remoteProcess._participantID = toUserID;
                    break;
                  }
                  break;
              }
              activity.Attributes.AddAttribute(wfConsts.AttrRecipStatusID, false, new object[1]
              {
                (object) 0
              });
              if (activity.Status == ActivityStatus.Executed)
                activity.HandleTemporaryRights(true);
            }
            bool flag2 = false;
            foreach (Variable variable in activity.VariableList)
            {
              if (!(variable is ISystemVariable) && variable.VarType == VarType.ParticipantList)
              {
                ParticipantList asParticipants = variable.AsParticipants;
                bool flag3 = asParticipants.Replace(ParticipantKind.User, userID, toUserID);
                flag2 |= flag3;
                if (flag3)
                  variable.Value = asParticipants.AsString;
              }
            }
            if (flag2)
              activity.SaveVariables();
            flag1 |= flag2;
          }
          IDBAttribute attributeById1 = activity.GetAttributeByID(wfConsts.AttrParticipantsID);
          if (attributeById1 != null)
          {
            ParticipantList participantList1 = new ParticipantList((IUserSession) this.UserSession);
            string s = attributeById1.Value.ToString();
            string addData = ParticipantList.ExtractAddData(s);
            participantList1.AsString = s;
            bool flag4 = participantList1.Replace(ParticipantKind.User, userID, toUserID);
            if (activity.Status == ActivityStatus.ParticipantWaiting)
            {
              ParticipantList participantList2 = new ParticipantList((IUserSession) this.UserSession)
              {
                XmlSection = "Expanded",
                AsString = addData
              };
              bool flag5 = participantList2.Replace(ParticipantKind.User, userID, toUserID);
              flag4 |= flag5;
              if (flag5)
                addData = participantList2.AsString;
              DataTable dataTable = this.Session.GetObjectCollection(wfConsts.WorkOfferTypeID).Select(new DBRecordSetParams(new ConditionStructure[1]
              {
                new ConditionStructure(wfConsts.AttrActivityID, RelationalOperators.Equal, (object) activity.ObjectID, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID)
              }, new ColumnDescriptor[2]
              {
                new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 0),
                new ColumnDescriptor((object) wfConsts.AttrRecipID, AttributeSourceTypes.Auto, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 0)
              }));
              bool flag6 = false;
              foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
              {
                if (Convert.ToInt64(row[1]) == toUserID)
                {
                  IDBObject dbObject = this.Session.GetObject(Convert.ToInt64(row[0]), false);
                  if (dbObject != null)
                  {
                    IDBAttribute attributeById2 = dbObject.GetAttributeByID(wfConsts.AttrRecipDeletionID);
                    if (attributeById2 != null)
                      attributeById2.AsInteger = 0L;
                    IDBAttribute attributeById3 = dbObject.GetAttributeByID(wfConsts.AttrRecipStatusID);
                    if (attributeById3 != null)
                      attributeById3.AsInteger = 0L;
                    flag6 = true;
                  }
                }
              }
              foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
              {
                if (Convert.ToInt64(row[1]) == userID)
                {
                  IDBObject dbObject = this.Session.GetObject(Convert.ToInt64(row[0]), false);
                  if (dbObject != null)
                  {
                    if (flag6)
                    {
                      dbObject.Delete(0L);
                    }
                    else
                    {
                      IDBAttribute attributeById4 = dbObject.GetAttributeByID(wfConsts.AttrRecipID);
                      if (attributeById4 != null)
                        attributeById4.AsInteger = toUserID;
                      IDBAttribute attributeById5 = dbObject.GetAttributeByID(wfConsts.AttrRecipDeletionID);
                      if (attributeById5 != null)
                        attributeById5.AsInteger = 0L;
                      IDBAttribute attributeById6 = dbObject.GetAttributeByID(wfConsts.AttrRecipStatusID);
                      if (attributeById6 != null)
                        attributeById6.AsInteger = 0L;
                    }
                  }
                }
              }
            }
            if (flag4)
            {
              string asString = participantList1.AsString;
              if (!string.IsNullOrEmpty(addData))
                ParticipantList.InsertAddData(ref asString, addData);
              attributeById1.Value = (object) asString;
            }
            flag1 |= flag4;
          }
        }
      }
    }
    finally
    {
      if (flag1)
      {
        string caption1 = this.Session.GetObjectInfo(userID).Caption;
        string caption2 = this.Session.GetObjectInfo(toUserID).Caption;
        this.AddEvent(this.ObjectID, ActionType.EditProperties, EventlogRecordType.AccessGranted, string.Format(LocalizationHolder.rm.GetString("ReplaceParticipantEvent"), (object) caption1, (object) caption2));
      }
      this.ReleaseBlock();
    }
    return flag1;
  }

  public Dictionary<long, string> Recall()
  {
    Dictionary<long, string> dictionary = new Dictionary<long, string>();
    bool flag = false;
    this.CheckAccess(ActionType.wfAdminProcess);
    this.AcquireBlock();
    try
    {
      List<WFActivity> wfActivityList1 = new List<WFActivity>();
      List<WFActivity> wfActivityList2 = new List<WFActivity>();
      foreach (WFActivity activity in this.Activities)
      {
        if (wfConsts.RollbackActivityKinds.Contains(activity.Kind) && activity.Executed)
        {
          if (activity.RollbackKind == RollbackKind.Disabled)
          {
            wfActivityList2.Add(activity);
            if (!dictionary.ContainsKey(activity.ObjectID))
              dictionary.Add(activity.ObjectID, activity.Caption);
          }
          else
            wfActivityList1.Add(activity);
        }
        if (activity.ObjectType == wfConsts.SubProcessTypeID && activity is SubProcess subProcess && this.UserSession.GetObject(subProcess.SubProcessID, false) is WFProcess wfProcess && wfProcess.Executed && wfProcess.Recall().Count > 0)
        {
          if (!dictionary.ContainsKey(wfProcess.ObjectID))
            dictionary.Add(subProcess.SubProcessID, subProcess.Caption);
          wfActivityList1.Clear();
          break;
        }
      }
      foreach (WFActivity originalSenderActivity in wfActivityList1)
      {
        originalSenderActivity.Flags |= ActivityFlags.Recalling;
        try
        {
          new WFActivityProxy(this.ObjectID, originalSenderActivity, originalSenderActivity.NonUserActivitiesCounter).RecallNextStep(false);
          flag = true;
        }
        finally
        {
          originalSenderActivity.Flags ^= ActivityFlags.Recalling;
        }
      }
    }
    finally
    {
      if (flag)
        this.AddEvent(this.ObjectID, ActionType.EditProperties, EventlogRecordType.AccessGranted, LocalizationHolder.rm.GetString("RecallEvent"));
      this.ReleaseBlock();
    }
    return dictionary;
  }

  public override bool CheckAccess(ActionType action, bool defaultAccess, CheckAccessFlags flags)
  {
    if (this.ExecutionMode && action == ActionType.Edit)
      return true;
    bool flag1 = (flags & CheckAccessFlags.ThrowACException) == CheckAccessFlags.ThrowACException;
    flags &= ~CheckAccessFlags.ThrowACException;
    bool flag2 = base.CheckAccess(action, defaultAccess, flags);
    if (!flag2)
    {
      if (!this.IsAccessTypeDeny && this.OwnerID == this.UserSession.UserID)
        return true;
      if (!this.IsAccessTypeDeny && action == ActionType.wfAdminProcess)
      {
        IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrPrototypeID);
        if (attributeById != null)
        {
          long asInteger = attributeById.AsInteger;
          if (asInteger != 0L && this.Session.GetObject(asInteger, false) is DBObject dbObject)
          {
            dbObject.CheckAccess(action, defaultAccess, true);
            return true;
          }
        }
        return true;
      }
      if (flag1)
        throw new AccessDeniedException((IUserSession) this.UserSession);
    }
    return flag2;
  }

  IActivity IScheme.StartActivity => (IActivity) this.StartActivity;

  protected override void DoDelete()
  {
    foreach (DataRow row in (InternalDataCollectionBase) this.ListMessages(0, (ConditionStructure[]) null).Rows)
      this.UserSession.GetObject(Convert.ToInt64(row[0])).Delete(0L);
    base.DoDelete();
  }

  internal DataTable ListMessages(int typeID, ConditionStructure[] addconds)
  {
    int num = 1;
    HybridDictionary tags = (HybridDictionary) null;
    if (typeID == 0)
    {
      typeID = wfConsts.WorkOfferTypeID;
      tags = new HybridDictionary()
      {
        [(object) "LocalTypesSelector"] = (object) new LocalTypesSelector()
      };
      num = 0;
    }
    ConditionStructure conditionStructure1 = new ConditionStructure(wfConsts.AttrProcessID, RelationalOperators.Equal, (object) this.ObjectID, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID);
    ConditionStructure conditionStructure2 = new ConditionStructure(-7, RelationalOperators.Equal, (object) typeID, LogicalOperators.AND, 0, false);
    ConditionStructure[] conds = new ConditionStructure[1 + num + (addconds != null ? addconds.Length : 0)];
    conds[0] = conditionStructure1;
    if (num == 1)
      conds[1] = conditionStructure2;
    if (addconds != null)
    {
      for (int index = 0; index < addconds.Length; ++index)
        conds[num + index + 1] = addconds[index];
    }
    return MiscFunx.SimpleSelect((IUserSession) this.UserSession, typeID, conds, tags);
  }

  public override void CopyStuffFromPrototype(WFScheme copyPrototype, bool createSchemeVersion = false)
  {
    IDBAttribute byId = copyPrototype.Attributes.FindByID(wfConsts.AttrIsDebugID);
    if (GlobalMailSettings.Cfg.CreateActivitiesOnDemand && byId != null && !byId.AsBoolean)
    {
      if (copyPrototype.CheckoutBy != 0L)
      {
        string objectCaption = MiscFunx.GetObjectCaption(this.Session, copyPrototype.CheckoutBy, false);
        throw new WorkflowException(string.Format(LocalizationHolder.rm.GetString("ErrLaunchCheckedOutScheme"), (object) copyPrototype.Caption, (object) objectCaption));
      }
      this.CreateActivitiesOnDemand = true;
      this.CreateActivity((WFActivity) copyPrototype.StartActivity, true);
      this.AfterCopyStuffFromPrototype();
    }
    else
      base.CopyStuffFromPrototype(copyPrototype);
  }

  protected override void AfterCopyStuffFromPrototype()
  {
    base.AfterCopyStuffFromPrototype();
    for (int i = 0; i < this.Activities.Count; i++)
    {
      this.Activities[i].LCStep = wfConsts.ActivityExecLCStepID;
      List<WFLink> list = this.AllLinks.Where<WFLink>((System.Func<WFLink, bool>) (x => x.FromID == this.Activities[i].ObjectID)).ToList<WFLink>();
      for (int index = 0; index < list.Count; ++index)
        list[index].LCStep = wfConsts.LinkExecLCStepID;
    }
  }

  protected override void DoCommitCreation()
  {
    base.DoCommitCreation();
    if (this.ExtProps.ReadBool("ImportingProcess"))
      return;
    if (this.StartActivity == null)
      throw new Exception("Процесс не может стартовать т.к. не найдено действие старта.");
    this.StartActivity.VariableList.Assign(this.Variables);
    this.StartActivity.SaveVariables();
  }

  public override string Validate(bool checkSubProcessSchemes = true, List<long> checkedSchemesList = null)
  {
    return this.CreateActivitiesOnDemand ? string.Empty : base.Validate(checkSubProcessSchemes, checkedSchemesList);
  }

  public bool ExecutionMode => this._blockUserID == this.UserSession.UserID;

  private long InternalAcquireBlock()
  {
    int num = 0;
    while (num < 10)
    {
      this.ClearAttributesCache();
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrBlockingID);
      this._blockUserID = attributeById != null ? attributeById.AsInteger : throw new Exception(LocalizationHolder.rm.GetString("Workflow.Server_18"));
      if (this._blockUserID != 0L)
      {
        this._blockDT = attributeById.AsDateTime;
        if (this._blockDT != DateTime.MinValue && DateTime.Now.Subtract(this._blockDT).TotalDays > 1.0)
          this._blockUserID = 0L;
      }
      if (this._blockUserID == 0L)
      {
        this._blockUserID = this.UserSession.UserID;
        attributeById.AsInteger = this._blockUserID;
        attributeById.AsDateTime = DateTime.Now;
        return 0;
      }
      ++num;
      Thread.Sleep(1000);
    }
    return this._blockUserID;
  }

  internal void AcquireBlock()
  {
    long objectID = this.InternalAcquireBlock();
    if (objectID == 0L)
      return;
    string str = "??";
    IDBObject dbObject = this.UserSession.GetObject(objectID, false);
    if (dbObject != null)
      str = dbObject.Caption;
    if (objectID != this.UserSession.UserID)
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Workflow.Server_19"), (object) this.Caption, (object) str, (object) this._blockDT));
    throw new Exception($"Процесс \"{this.Caption}\" заблокирован Вами в другой ветке. Дождитесь её окончания и повторите попытку.");
  }

  internal void ReleaseBlock()
  {
    this.ClearAttributesCache();
    IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrBlockingID);
    if (attributeById == null)
      throw new Exception(LocalizationHolder.rm.GetString("Workflow.Server_20"));
    if (attributeById.AsInteger == this.UserSession.UserID)
      attributeById.AsInteger = 0L;
    this._blockUserID = -1L;
  }

  internal void ReleaseBlock(long userID)
  {
    this.ClearAttributesCache();
    IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrBlockingID);
    if (attributeById == null)
      throw new Exception(LocalizationHolder.rm.GetString("Workflow.Server_20"));
    if (attributeById.AsInteger == userID)
      attributeById.AsInteger = 0L;
    this._blockUserID = -1L;
  }

  internal List<long> PreExecuted
  {
    get => this.ExtProps.ReadList<long>("ProcessPreExecuted", new List<long>());
    set
    {
      this.ExtProps.WriteList<long>("ProcessPreExecuted", value, ExtPropertiesFlag.PreExecuted);
      this.ExtProps.Save((IDBObject) this);
    }
  }

  internal void MarkAsPreExecuted(long activityID, bool mark)
  {
    long num = Math.Abs(activityID);
    List<long> preExecuted = this.PreExecuted;
    try
    {
      if (mark)
      {
        if (preExecuted.Contains(num))
          return;
        preExecuted.Add(num);
      }
      else
        preExecuted.Remove(num);
    }
    finally
    {
      this.PreExecuted = preExecuted;
    }
  }
}
