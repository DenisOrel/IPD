// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.WFActivity
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using ImSSP;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.LifeCycles;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.Security;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Project;
using Intermech.Scripting;
using Intermech.Workflow.Server.Activities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

#nullable disable
namespace Intermech.Workflow.Server;

public class WFActivity(UserSession uSession, DataTable objectsTable) : 
  DBMailObject(uSession, objectsTable),
  IExecutedActivity,
  IActivity,
  IMailObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData,
  IDBSecurityCollection,
  IDBSecurity
{
  protected bool _autoStep = true;
  private WFScheme _parentScheme;
  internal bool ReadyToGoCalled;
  internal bool ErrorOccured;
  internal int NonUserActivitiesCounter;
  private List<WFLink> _allLinksToThis = new List<WFLink>();
  internal List<WFLink> NextStepLinks = new List<WFLink>();
  private AttachmentList _attachments;
  internal WFScheme _process;
  private long _processID = -1;
  internal long _participantID = -2;
  internal ParticipantList _parts;
  protected string AddParticipantsData = string.Empty;
  private ParticipantList _originalParticipants;
  private bool _allowDeletion;
  protected VarList _variablesList;
  internal bool ModifyAttachmentInCaseActivity;
  private int _flags = -1;
  private int _lasdbflags = -1;
  private bool _rollbackLoaded;
  protected RollbackKind _rollbackKind;
  private int _priority = -1;
  protected long _lastScriptID;
  private LCInfoList _LCList;
  private List<int> _lcDefinedObjectTypes;
  private Notifications _notifications;
  private List<WFActivity> _clones;
  private ExtProperties _extProps;
  private bool _requestedByForm;
  internal bool InAssignAttributes;
  private bool? _isBlockStart;
  private long _threadID = -1;
  private List<WFLink> _parallelBackLinks;
  private StringList _portalInfo;
  private bool _portalInfoLoaded;
  private long _temporaryRights = -1;
  private Terms _terms;

  internal virtual bool GetAutoStep() => this._autoStep;

  public bool AutoStep => this.GetAutoStep();

  public long ParentActivityID
  {
    get
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrParentActivityID);
      return attributeById != null ? attributeById.AsInteger : 0L;
    }
    internal set
    {
      this.Attributes.AddAttribute(wfConsts.AttrParentActivityID, false, new object[1]
      {
        (object) value
      });
    }
  }

  public WFScheme ParentScheme
  {
    get
    {
      if (this._parentScheme == null)
      {
        IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrProcessID);
        if (attributeById != null)
          this._parentScheme = this.UserSession.GetObject(attributeById.AsInteger, false) as WFScheme;
      }
      return this._parentScheme;
    }
  }

  public ProcessLCDirection ProcessLCDirection
  {
    get => (ProcessLCDirection) this.ExtProps.ReadInteger(nameof (ProcessLCDirection), 0L);
  }

  private bool IsAllPreviousCompleted()
  {
    LinkWalker linkWalker = new LinkWalker((WFProcess) this.Process, (IUserSession) this.UserSession);
    long num = this.ParentActivityID;
    if (num == 0L)
      num = this.ObjectID;
    long objectID = num;
    return linkWalker.IsAllPreviousCompleted(objectID);
  }

  internal virtual bool ReadyToGo(long senderActivityID)
  {
    this.ReadyToGoCalled = true;
    if (senderActivityID != -1L)
      this.SenderActivityID = senderActivityID;
    if (!this.Collector || this.IsAllPreviousCompleted())
      return true;
    this.Status = ActivityStatus.CollectorWaiting;
    return false;
  }

  public virtual bool Execute(bool goNext)
  {
    if (!this.ReadyToGoCalled)
      return false;
    bool flag1 = false;
    bool flag2 = false;
    this.ErrorOccured = false;
    try
    {
      this.PrepareActivity();
      flag2 = true;
    }
    catch (Exception ex)
    {
      if (ex is AbortException)
        throw;
      this.ErrorOccured = true;
      this.DumpException(ex);
      if (this is Start start)
      {
        if (!start.FirstLaunch)
          return false;
        throw;
      }
    }
    if (this.ErrorOccured || flag2 && this.AutoStep)
    {
      if (!this.ErrorOccured)
        this.ActivityResult = goNext ? ActivityResult.Next : ActivityResult.Back;
      flag1 = true;
      this.AfterExecute();
      this.PrepareNextStepLinks();
      this.MarkNextStepActivitiesAsPreExecuted();
    }
    else if (flag2)
      this.AfterSent();
    return flag1;
  }

  internal virtual void PrepareActivity()
  {
    this.Status = ActivityStatus.Executed;
    this.StartedTime = DateTime.Now;
    this.Attributes.AddAttribute(wfConsts.AttrRecipStatusID, false, new object[1]
    {
      (object) 0
    });
    SenderStatus senderStatus = SenderStatus.Completed;
    if (this.SenderActivity != null && this.SenderActivity.ActivityResult == ActivityResult.Back)
      senderStatus = SenderStatus.Rejected;
    this.Attributes.AddAttribute(wfConsts.AttrSenderStatusID, false, new object[1]
    {
      (object) (int) senderStatus
    });
    if (this.NonUserActivitiesCounter >= wfConsts.MaxNonUserActivitiesCounter)
    {
      if (this.Process is WFProcess process)
      {
        string str = string.Format(LocalizationHolder.rm.GetString("Workflow.Server_10"), (object) this.NonUserActivitiesCounter);
        process.StopProcess((IActivity) this, true);
        this.CreateMessage(process.OwnerID, LocalizationHolder.rm.GetString("Workflow.Server_9"), str);
        this.MessageText = LocalizationHolder.rm.GetString("Workflow.Server_11");
        throw new AbortException(str);
      }
      this.MessageText = LocalizationHolder.rm.GetString("Workflow.Server_11");
      throw new AbortException(this.MessageText);
    }
    this.BeforeExecute();
    this.SendNotification(this.Notifications.StartNotify);
    if (!this.Notifications.PeriodNotify.Enabled)
      return;
    IDBTimedEvents service = ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    service.AddToTrace($"Событие N{service.AddEvent(new TimedEventProperties(0, this.Notifications.PeriodNotify.Period.GetExecTime((IDBObject) this), DateTime.MinValue, wfConsts.WorkflowTimerServiceGuid, this.ObjectID, 0L, string.Empty, 0, 0), this.UserSession.DataManager)} для объекта N{this.ObjectID} зарегистрировано.", true);
  }

  internal virtual void BeforeExecute()
  {
    this.ExecScript(ScriptKind.BeforeExec);
    this.ProcessLC(LCExec.Before);
  }

  internal virtual void AfterExecute()
  {
    try
    {
      if (this.Status == ActivityStatus.ParticipantWaiting)
        this.DeleteMessages(wfConsts.WorkOfferTypeID);
      this.DeletePeriodNotificationIfNeeded();
      RecipStatus recipStatus = RecipStatus.Completed;
      if (this.ActivityResult == ActivityResult.Back)
        recipStatus = RecipStatus.Rejected;
      this.Attributes.AddAttribute(wfConsts.AttrRecipStatusID, false, new object[1]
      {
        (object) (int) recipStatus
      });
      if (this.StartedTime == DateTime.MinValue)
        this.StartedTime = DateTime.Now;
      if (this.ThreadID == 0L || this.GetParallelBlockLink(LinkDirection.To) == null)
        return;
      this.ThreadID = 0L;
    }
    finally
    {
      try
      {
        this.Status = ActivityStatus.ScriptExecuted;
        this.ExecScript(ScriptKind.AfterExec);
      }
      catch (Exception ex)
      {
        this.DumpException(ex);
      }
      try
      {
        if (this.ProcessLCDirection != ProcessLCDirection.All && (this.ProcessLCDirection != ProcessLCDirection.Next || this.ActivityResult != ActivityResult.Next))
        {
          if (this.ProcessLCDirection == ProcessLCDirection.Back)
          {
            if (this.ActivityResult != ActivityResult.Back)
              goto label_18;
          }
          else
            goto label_18;
        }
        this.Status = ActivityStatus.LCStepExecuted;
        this.ProcessLC(LCExec.After);
      }
      catch (Exception ex)
      {
        this.DumpException(ex);
      }
label_18:
      try
      {
        this.Status = this.Flags.HasFlag((Enum) ActivityFlags.Recalling) ? ActivityStatus.Recalled : ActivityStatus.Completed;
        this.Attributes.AddAttribute(wfConsts.AttrCompletedID, false, new object[1]
        {
          (object) DateTime.Now
        });
        this.SendNotification(this.ActivityResult == ActivityResult.Next ? this.Notifications.StopNotify : this.Notifications.BackNotify);
      }
      catch (Exception ex)
      {
        this.DumpException(ex);
      }
    }
  }

  internal List<WFLink> AllLinksFromThis
  {
    get
    {
      if (this.Process is WFProcess process1)
      {
        List<WFLink> wfLinkList = new List<WFLink>();
        List<WFLink> list;
        if (process1.CreateActivitiesOnDemand || this.ParentActivityID != 0L)
        {
          List<WFLink> allLinks = this.Process.AllLinks;
          list = allLinks != null ? allLinks.Select<WFLink, WFLink>((System.Func<WFLink, WFLink>) (x => x)).Where<WFLink>((System.Func<WFLink, bool>) (x => x.FromID == this.ParentActivityID)).ToList<WFLink>() : (List<WFLink>) null;
        }
        else
        {
          List<WFLink> allLinks = this.Process.AllLinks;
          list = allLinks != null ? allLinks.Select<WFLink, WFLink>((System.Func<WFLink, WFLink>) (x => x)).Where<WFLink>((System.Func<WFLink, bool>) (x => x.FromID == Math.Abs(this.ObjectID))).ToList<WFLink>() : (List<WFLink>) null;
        }
        return list ?? new List<WFLink>();
      }
      List<WFLink> wfLinkList1 = new List<WFLink>();
      WFScheme process2 = this.Process;
      List<WFLink> wfLinkList2;
      if (process2 == null)
      {
        wfLinkList2 = (List<WFLink>) null;
      }
      else
      {
        List<WFLink> allLinks = process2.AllLinks;
        wfLinkList2 = allLinks != null ? allLinks.Select<WFLink, WFLink>((System.Func<WFLink, WFLink>) (x => x)).Where<WFLink>((System.Func<WFLink, bool>) (x => x.FromID == Math.Abs(this.ObjectID))).ToList<WFLink>() : (List<WFLink>) null;
      }
      return wfLinkList2 ?? new List<WFLink>();
    }
  }

  internal List<WFLink> AllLinksToThis
  {
    get
    {
      if (this._allLinksToThis != null && this._allLinksToThis.Count != 0)
        return this._allLinksToThis;
      WFScheme process = this.Process;
      List<WFLink> wfLinkList1;
      if (process == null)
      {
        wfLinkList1 = (List<WFLink>) null;
      }
      else
      {
        List<WFLink> allLinks = process.AllLinks;
        wfLinkList1 = allLinks != null ? allLinks.Select<WFLink, WFLink>((System.Func<WFLink, WFLink>) (x => x)).Where<WFLink>((System.Func<WFLink, bool>) (x => x.ToID == this.ParentActivityID)).ToList<WFLink>() : (List<WFLink>) null;
      }
      List<WFLink> wfLinkList2 = wfLinkList1;
      this._allLinksToThis = wfLinkList2;
      return wfLinkList2 ?? new List<WFLink>();
    }
  }

  protected void AddLinkToNextStep(WFLink link)
  {
    if (this.NextStepLinks.Contains(link))
      return;
    this.NextStepLinks.Add(link);
  }

  internal virtual void PrepareNextStepLinks()
  {
    this.NextStepLinks.Clear();
    if (this.ActivityResult == ActivityResult.Next)
    {
      foreach (WFLink allLinksFromThi in this.AllLinksFromThis)
      {
        if (allLinksFromThi.IsDirect && allLinksFromThi.Kind == LinkKind.Forward && (!this.IsBlockStart || this.ThreadID == 0L || this.ThreadID == allLinksFromThi.ToID))
          this.AddLinkToNextStep(allLinksFromThi);
      }
    }
    else
    {
      long num = 0;
      switch (this.RollbackKind)
      {
        case RollbackKind.Start:
          num = this.GetStartActivityID();
          break;
        case RollbackKind.Previous:
          num = this.SenderActivityID;
          break;
        case RollbackKind.Link:
          using (List<WFLink>.Enumerator enumerator = this.AllLinksFromThis.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              WFLink current = enumerator.Current;
              if (current.Kind == LinkKind.Backward)
                this.AddLinkToNextStep(current);
            }
            break;
          }
        case RollbackKind.StartBlock:
          WFLink parallelBlockLink = this.GetParallelBlockLink(LinkDirection.To);
          if (parallelBlockLink != null)
          {
            num = parallelBlockLink.FromID;
            break;
          }
          break;
        default:
          this.RaiseException(LocalizationHolder.rm.GetString("Workflow.Server_12"));
          break;
      }
      if (num == 0L && this.NextStepLinks.Count == 0)
        num = this.GetStartActivityID();
      if (num <= 0L)
        return;
      this.AddLinkToNextStep(new WFLink(this.UserSession)
      {
        FromID = this.ObjectID,
        ToID = num
      });
    }
  }

  public long SenderActivityID
  {
    get
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrSenderActivityID);
      return attributeById != null ? attributeById.AsInteger : 0L;
    }
    set
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrSenderActivityID);
      if (attributeById != null)
      {
        if (attributeById.AsInteger == value)
          return;
        attributeById.AsInteger = value;
      }
      else
        this.Attributes.AddAttribute(wfConsts.AttrSenderActivityID, false, new object[1]
        {
          (object) value
        });
    }
  }

  internal WFActivity SenderActivity
  {
    get
    {
      long senderActivityId = this.SenderActivityID;
      return senderActivityId != 0L ? this.UserSession.GetObject(senderActivityId, false) as WFActivity : (WFActivity) null;
    }
  }

  public long GetStartActivityID()
  {
    ConditionStructure[] conditions = new ConditionStructure[1]
    {
      new ConditionStructure(wfConsts.AttrProcessID, RelationalOperators.Equal, (object) this.ProcessID, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID)
    };
    IDBObjectCollection objectCollection = this.Session.GetObjectCollection(wfConsts.StartTypeID);
    object[] columns = new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    };
    object[] sortColumns = new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    };
    DBRecordSetParams paramSet = new DBRecordSetParams(conditions, columns, sortColumns, new SortOrders[1]
    {
      SortOrders.ASC
    })
    {
      RecordCount = 1
    };
    DataTable dataTable = objectCollection.Select(paramSet);
    if (dataTable.Rows.Count < 1)
      throw new Exception("Идентификатор действия 'Старт' не найден!");
    return Convert.ToInt64(dataTable.Rows[0][0]);
  }

  public void SendPeriodMessage()
  {
    if (!this.Executed)
      return;
    this.SendNotification((Notification) this.Notifications.PeriodNotify);
  }

  internal void SendNotification(Notification n)
  {
    if (!n.Enabled)
      return;
    this.CreateMessage(n.Recips, n.Subject, n.Text);
  }

  internal virtual IDBObject CreateMessage(long recipID, string Subject, string Text)
  {
    Subject = ServerFunx.ReplaceTextMacros(Subject, this.VariableList);
    Text = ServerFunx.ReplaceTextMacros(Text, this.VariableList);
    long FromUserID = this.RecipID;
    switch (FromUserID)
    {
      case 0:
      case 2:
        if (this is UserActivity && this.Participants.Count > 0)
        {
          ParticipantList pl = new ParticipantList((IUserSession) this.UserSession);
          pl.Assign(this.Participants);
          MiscFunx.ExpandParticipants((IDBAttributable) this, pl);
          FromUserID = pl[0].ID;
          break;
        }
        break;
    }
    IDBObject message = ServerFunx.CreateMessage((IUserSession) this.UserSession, wfConsts.MessageTypeID, recipID, Subject, Text, this.ProcessID, this.ObjectID, FromUserID);
    message.GetAttributeByID(wfConsts.AttrPriorityID).AsInteger = (long) this.Priority;
    ServerFunx.CopyAttachmentsFlag((IDBObject) this, message);
    return message;
  }

  internal void CreateMessage(ParticipantList Recips, string Subject, string Text)
  {
    if (!this.VariableList.SystemAdded)
      this.VariableList.AddSystemVariables((IDBObject) this.Process);
    MiscFunx.ExpandParticipants((IDBAttributable) this, Recips);
    for (int index = 0; index < Recips.Count; ++index)
      this.CreateMessage(Recips[index].ID, Subject, Text);
  }

  internal void DeleteMessages(int typeID)
  {
    this.DeleteMessages(typeID, (ConditionStructure[]) null);
  }

  internal void DeleteMessages(int typeID, ConditionStructure[] addconds)
  {
    foreach (DataRow row in (InternalDataCollectionBase) this.ListMessages(typeID, addconds).Rows)
      this.UserSession.GetObject(Convert.ToInt64(row[0])).Delete(0L);
  }

  internal DataTable ListMessages(int typeID, ConditionStructure[] addconds)
  {
    int num = 1;
    HybridDictionary tags = (HybridDictionary) null;
    if (typeID == 0)
    {
      typeID = wfConsts.WorkOfferTypeID;
      tags = new HybridDictionary();
      tags[(object) "LocalTypesSelector"] = (object) new LocalTypesSelector();
      num = 0;
    }
    ConditionStructure conditionStructure1 = new ConditionStructure();
    conditionStructure1 = this.ObjectID == 0L ? new ConditionStructure(wfConsts.AttrProcessID, RelationalOperators.Equal, (object) this.ProcessID, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID) : new ConditionStructure(wfConsts.AttrActivityID, RelationalOperators.Equal, (object) this.ObjectID, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID);
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

  public DateTime StartedTime
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

  public DateTime CompletedTime
  {
    get
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrCompletedID);
      return attributeById == null ? DateTime.MinValue : attributeById.AsDateTime;
    }
  }

  public ActivityStatus Status
  {
    get
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrActivityStatusID);
      return attributeById == null ? ActivityStatus.OnApproach : (ActivityStatus) attributeById.AsInteger;
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

  internal virtual void SetStatus(
    IDBAttribute attr,
    ActivityStatus value,
    ActivityStatus oldStatus)
  {
    if (attr == null)
    {
      this.Attributes.CheckExistMode = true;
      try
      {
        this.Attributes.AddAttribute(wfConsts.AttrActivityStatusID, false, new object[1]
        {
          (object) value
        });
      }
      finally
      {
        this.Attributes.CheckExistMode = false;
      }
    }
    else
      attr.AsInteger = (long) value;
  }

  public ActivityResult ActivityResult
  {
    get
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrActivityResultID);
      return attributeById == null ? ActivityResult.Next : (ActivityResult) attributeById.AsInteger;
    }
    set
    {
      this.Attributes.AddAttribute(wfConsts.AttrActivityResultID, false, new object[1]
      {
        (object) value
      });
    }
  }

  public AttachmentList Attachments
  {
    get
    {
      if (this._attachments == null)
      {
        this._attachments = new AttachmentList();
        this._attachments.Load((IDBObject) this);
      }
      return this._attachments;
    }
  }

  public WFScheme Process
  {
    get
    {
      if (this._process == null)
      {
        IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrProcessID);
        if (attributeById != null)
          this._process = this.UserSession.GetObject(attributeById.AsInteger, false) as WFScheme;
      }
      return this._process;
    }
  }

  public string ProcessName => this.Process != null ? this.Process.Caption : "??";

  public long ProcessID
  {
    get
    {
      if (this.Kind == ActivityKind.Process)
        return this.ObjectID;
      if (this._process != null)
        return this._process.ObjectID;
      if (this._processID == -1L)
      {
        IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrProcessID);
        this._processID = attributeById != null ? attributeById.AsInteger : 0L;
      }
      return this._processID;
    }
    set
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrProcessID);
      if (attributeById != null)
        attributeById.AsInteger = value;
      this._processID = value;
    }
  }

  IScheme IActivity.Process => (IScheme) this.Process;

  public long SenderID
  {
    get
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrSenderID);
      return attributeById != null ? attributeById.AsInteger : 0L;
    }
    set
    {
      long num = 0;
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrSenderID);
      if (attributeById != null)
      {
        num = attributeById.AsInteger;
        attributeById.AsInteger = value;
      }
      else
        this.Attributes.AddAttribute(wfConsts.AttrSenderID, false, new object[1]
        {
          (object) value
        });
      if (num == value)
        return;
      this.UpdateTempAttributeValue(wfConsts.SysVarSenderID);
    }
  }

  protected virtual long SenderParticipantID => this.SenderID;

  public virtual long ParticipantID
  {
    get => wfConsts.SystemUserID;
    internal set
    {
    }
  }

  public ParticipantList Participants
  {
    get
    {
      if (this._parts == null)
      {
        this._parts = new ParticipantList((IUserSession) this.UserSession);
        IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrParticipantsID);
        if (attributeById != null)
          this._parts.AsString = attributeById.Value.ToString();
      }
      return this._parts;
    }
  }

  internal ParticipantList OriginalParticipants
  {
    get => this._originalParticipants != null ? this._originalParticipants : this.Participants;
  }

  public void SaveParticipants(bool forceSave = false)
  {
    if (!forceSave && this._originalParticipants != null)
      throw new Exception("SaveParticipants not allowed after execute!");
    if (this.Participants == null)
      return;
    IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrParticipantsID);
    if (attributeById == null)
      return;
    string asString = this.Participants.AsString;
    if (!string.IsNullOrEmpty(this.AddParticipantsData))
      ParticipantList.InsertAddData(ref asString, this.AddParticipantsData);
    attributeById.Value = (object) asString;
  }

  public long RecipID
  {
    get
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrRecipID);
      return attributeById != null ? attributeById.AsInteger : 0L;
    }
  }

  internal bool InternalDelete(bool allowDeletion)
  {
    bool flag = false;
    long formId = this.FormID;
    if (this.IsAlien())
      return false;
    if (!(this.Process is WFProcess))
    {
      foreach (long objectID in this.LocalScriptsInCurrentActivity)
        this.UserSession.GetObject(objectID, false)?.Delete(0L);
      if (formId != 0L)
      {
        IDBObjectCollection objectCollection = this.UserSession.GetObjectCollection(wfConsts.ParticipantActivitiesTypeID);
        DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[2]
        {
          new ConditionStructure(wfConsts.AttrFormID, RelationalOperators.Equal, (object) formId, LogicalOperators.AND, 0, false),
          new ConditionStructure(-2, RelationalOperators.NotEqual, (object) this.ObjectID, LogicalOperators.NONE, 0, false)
        });
        paramSet.SetColumnDescriptors(new ColumnDescriptor[1]
        {
          new ColumnDescriptor((object) -2)
        });
        if (objectCollection.SelectWithLocalObjects(paramSet).Rows.Count == 0)
          flag = true;
      }
    }
    this._allowDeletion = allowDeletion;
    this.Delete(0L);
    if (flag)
      this.UserSession.GetObject(formId, false)?.Delete(0L);
    return true;
  }

  protected override void DoDelete()
  {
    if (this.ParentActivityID == 0L)
    {
      for (int index = 0; index < this.AllLinksFromThis.Count; ++index)
        this.AllLinksFromThis[index].InternalDelete(this._allowDeletion);
      for (int index = 0; index < this.AllLinksToThis.Count; ++index)
        this.AllLinksToThis[index].InternalDelete(this._allowDeletion);
    }
    base.DoDelete();
  }

  public bool IsAlien(long processID = 0)
  {
    long num = 0;
    IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrProcessID);
    if (attributeById != null)
      num = attributeById.AsInteger;
    if (processID == 0L && this.Process != null)
      processID = this.Process.ObjectID;
    return Math.Abs(processID) != Math.Abs(num);
  }

  public string Name
  {
    get
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrNameID);
      return attributeById != null ? attributeById.AsString : this.Caption;
    }
    set
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrNameID);
      if (attributeById == null)
        return;
      attributeById.AsString = value;
    }
  }

  public string Description
  {
    get
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrDescriptionID);
      return attributeById != null ? attributeById.AsString : string.Empty;
    }
    set
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrDescriptionID);
      if (attributeById == null)
        return;
      attributeById.Value = (object) value;
    }
  }

  public GlobalVariablesList GlobalVariables => this.Process?.GlobalVariables;

  public IVariables Variables => (IVariables) new RemotingVarList(this.VariableList);

  IVariables IActivity.GlobalVariables
  {
    get => (IVariables) new RemotingVarList((VarList) this.GlobalVariables);
  }

  public VarList VariableList
  {
    get
    {
      string key = $"WFActivity_LoadingVars_{this.ObjectID}";
      if (object.Equals(this.UserSession.GetSessionPluginsData((object) key), (object) true))
        return this._variablesList;
      this.UserSession.SetSessionPluginsData((object) key, (object) true);
      try
      {
        if (this.Flags != (ActivityFlags) -1 && this.Flags.HasFlag((Enum) ActivityFlags.InheritVars))
        {
          if (SimpleFuncs.In((object) this.Status, (object) ActivityStatus.OnApproach, (object) ActivityStatus.Terminated) && this._variablesList == null)
          {
            this._variablesList = new VarList((IUserSession) this.UserSession, true, false);
            if (this.Process != null)
              this._variablesList.Assign(this.Process.Variables);
            this._variablesList.Modified = false;
          }
        }
        if (this._variablesList == null)
        {
          this._variablesList = new VarList((IDBObject) this, true, false);
          if (this.Process is WFProcess && this._variablesList.Count == 0)
            this._variablesList.Assign(this.Process.Variables);
        }
        if (!this._variablesList.SystemAdded)
          this._variablesList.AddSystemVariables((IDBObject) this);
        return this._variablesList;
      }
      finally
      {
        this.UserSession.RemoveSessionPluginsData((object) key);
      }
    }
  }

  IAttachments IActivity.Attachments
  {
    get
    {
      return (IAttachments) new RemotingAttachList(this.Attachments, (IUserSession) this.UserSession, new AttachEventHandler(this.InterfacedAttachmentsChanged));
    }
  }

  private void InterfacedAttachmentsChanged(object sender, Attachment attach)
  {
    if (attach != null && attach.TypeID == 0)
    {
      QuickObjectInfo objectInfo = this.Session.GetObjectInfo(attach.ObjectID);
      attach.TypeID = objectInfo.ObjectTypeID;
    }
    this.SaveAttachments();
  }

  public void SaveAttachments()
  {
    if (this._attachments == null || !this._attachments.Save((IDBObject) this))
      return;
    ServerFunx.WriteAttachmentsFlag((IDBObject) this, this._attachments.Count > 0 ? 1L : 0L);
  }

  public virtual void TransferAttachments(WFActivity toAct)
  {
    toAct.Attachments.AddList(this.Attachments, false);
    toAct.SaveAttachments();
  }

  internal void UpdateMessagesAttachmentFlags(long newValue)
  {
    foreach (DataRow row in (InternalDataCollectionBase) this.ListMessages(0, (ConditionStructure[]) null).Rows)
    {
      IDBObject msg = this.UserSession.GetObject(Convert.ToInt64(row[0]), false);
      if (msg != null)
        ServerFunx.WriteAttachmentsFlag(msg, newValue);
    }
  }

  public void Changed(ActivityChanged flag) => this.Changed(flag, (object) null);

  public virtual void Changed(ActivityChanged flag, object tag)
  {
    if ((flag & ActivityChanged.SaveVariables) != (ActivityChanged) 0)
      this.SaveVariables(false);
    if ((flag & ActivityChanged.SaveGlobalVariables) != (ActivityChanged) 0 && this.GlobalVariables != null && this.GlobalVariables.Modified)
    {
      foreach (Variable globalVariable in (VarList) this.GlobalVariables)
        this.Process.GetAttributeByID(globalVariable.AttrTypeID).Value = globalVariable.TypedValue;
      this.GlobalVariables.Modified = false;
    }
    if ((flag & ActivityChanged.Variables) != (ActivityChanged) 0)
    {
      this._Attributes = (IDBAttributeCollection) null;
      this._variablesList = (VarList) null;
    }
    if ((flag & ActivityChanged.Attachments) != (ActivityChanged) 0)
      this._attachments = (AttachmentList) null;
    if ((flag & ActivityChanged.ExtProps) == (ActivityChanged) 0)
      return;
    this._extProps = (ExtProperties) null;
  }

  public bool Executed => wfConsts.ExecStatuses.Contains(this.Status);

  public string MessageText
  {
    get
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrActivityMessageID);
      return attributeById == null ? string.Empty : attributeById.Value.ToString();
    }
    set
    {
      this.Attributes.AddAttribute(wfConsts.AttrActivityMessageID, false, new object[1]
      {
        (object) value
      });
    }
  }

  public virtual ActivityKind Kind => ActivityKind.None;

  public ActivityFlags Flags
  {
    get
    {
      if (this._flags == -1 || this._flags == 0)
      {
        IDBAttribute attributeById = base.GetAttributeByID(wfConsts.AttrAddIDID);
        this._flags = attributeById == null ? 0 : (int) attributeById.AsInteger;
        this._lasdbflags = this._flags;
      }
      return (ActivityFlags) this._flags;
    }
    set
    {
      this._flags = (int) value;
      int num = this._flags;
      foreach (ActivityFlags activityFlags in Enum.GetValues(typeof (ActivityFlags)))
      {
        if (System.Attribute.IsDefined((MemberInfo) activityFlags.GetType().GetField(activityFlags.ToString()), typeof (RealtimeFlagAttribute)))
          num = (int) ((ActivityFlags) num & ~activityFlags);
      }
      if (this._lasdbflags == num)
        return;
      IDBAttribute attributeById = base.GetAttributeByID(wfConsts.AttrAddIDID);
      if (attributeById == null && num != 0)
        this.Attributes.AddAttribute(wfConsts.AttrAddIDID, false, new object[1]
        {
          (object) num
        });
      else if (attributeById != null && attributeById.AsInteger != (long) num)
        attributeById.AsInteger = (long) num;
      this._lasdbflags = num;
    }
  }

  public RollbackKind RollbackKind => this.GetRollBackKind();

  protected virtual RollbackKind GetRollBackKind()
  {
    if (!this._rollbackLoaded && wfConsts.RollbackActivityKinds.Contains(this.Kind))
    {
      this._rollbackLoaded = true;
      IDBAttribute byId = this.Attributes.FindByID(wfConsts.AttrRollbackKindID);
      if (byId != null)
        this._rollbackKind = (RollbackKind) byId.AsInteger;
    }
    return this._rollbackKind;
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

  public long FormID
  {
    get
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrFormID);
      return attributeById == null ? 0L : attributeById.AsInteger;
    }
    set
    {
      this.Attributes.AddAttribute(wfConsts.AttrFormID, false, new object[1]
      {
        (object) value
      });
    }
  }

  public virtual string Validate(bool checkSubProcessSchemes = true, List<long> checkedSchemesList = null)
  {
    string empty = string.Empty;
    if (this.LCList.Invalid)
      MiscFunx.AddNewLined(ref empty, string.Format(LocalizationHolder.rm.GetString(sc_22118.ssp_workflow_server_22119()), (object) this.Name));
    if (this.Notifications.Invalid)
      MiscFunx.AddNewLined(ref empty, string.Format(LocalizationHolder.rm.GetString(sc_22118.ssp_workflow_server_22120()), (object) this.Name));
    return empty;
  }

  public bool IsValid() => this.Validate(true, (List<long>) null) == string.Empty;

  protected virtual void ExecScript(ScriptKind kind)
  {
    ActivityFlags flags = this.Flags;
    this.Flags |= ActivityFlags.ServerScript;
    if (kind == ScriptKind.BeforeExec)
      this.Flags |= ActivityFlags.BeforeExec;
    else if (kind == ScriptKind.AfterExec)
      this.Flags |= ActivityFlags.AfterExec;
    try
    {
      this.ExecScript(MiscFunx.GetScriptCode((IUserSession) this.UserSession, this.ObjectID, kind, ScriptExecSide.Server, ref this._lastScriptID));
    }
    finally
    {
      this.Flags = flags;
    }
  }

  public void ExecScript(string code)
  {
    if (code == null || !(code.Trim() != string.Empty))
      return;
    bool oldStateOfTransaction = MiscFunx.CheckForActiveTransaction(this.Session);
    try
    {
      try
      {
        if (code.Contains("System.Windows.Forms"))
          throw new WorkflowException(sc_22118.ssp_workflow_server_22121());
        MiscFunx.IsolatedRawExecScript(code, (IActivity) this, CSharpScriptInvocationOptions.WithOptimizations);
      }
      finally
      {
        MiscFunx.CheckForActiveTransaction(this.Session, (IActivity) this, $"[ESS] (Script ID={this._lastScriptID})", oldStateOfTransaction, "Server");
      }
    }
    catch (Exception ex)
    {
      switch (ex)
      {
        case IRollbackException _:
          throw;
        case ScriptInvocationException _:
          if (ex.InnerException == null)
            throw new WorkflowException(string.Format(LocalizationHolder.rm.GetString("ExecScriptError"), (object) ExceptionServices.GetExtendedExceptionText(ex)));
          if (ex.InnerException is IRollbackException)
            throw ex.InnerException;
          throw new WorkflowException(string.Format(LocalizationHolder.rm.GetString("ExecScriptError"), (object) ExceptionServices.GetExtendedExceptionText(ex.InnerException)));
        default:
          throw new WorkflowException(string.Format(LocalizationHolder.rm.GetString("ExecScriptError"), (object) ExceptionServices.GetExtendedExceptionText(ex)));
      }
    }
    finally
    {
      this.SaveVariables();
      if (this.GlobalVariables != null && this.GlobalVariables.Modified)
      {
        foreach (Variable globalVariable in (VarList) this.GlobalVariables)
          this.Process.GetAttributeByID(globalVariable.AttrTypeID).Value = globalVariable.TypedValue;
        this.GlobalVariables.Modified = false;
      }
    }
  }

  public LCInfoList LCList
  {
    get
    {
      if (this._LCList == null)
      {
        this._LCList = new LCInfoList();
        IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrLCConfigAttrID);
        if (attributeById != null && !attributeById.IsNull)
          this._LCList.Load(attributeById);
      }
      return this._LCList;
    }
  }

  protected string UpdateLCLevel(long objID, LCInfo lc, IUserSession systemSession)
  {
    try
    {
      IDBObject dbObject = systemSession.GetObject(objID);
      IDBLifecycleStep lifecycleStep = systemSession.GetLifecycleStep(dbObject.LCStep);
      if (lc.Kind == LCKind.Step)
      {
        if (!this.CheckIfStepIDExists(lc.StepID, dbObject.ObjectType, systemSession))
        {
          string empty = string.Empty;
          IDBObjectType objectType = systemSession.GetObjectType(dbObject.ObjectType, false);
          string str = objectType != null ? objectType.ObjectTypeName : dbObject.ObjectType.ToString();
          return string.Format(LocalizationHolder.rm.GetString("ErrLCStepNotFound"), (object) dbObject.NameInMessages, (object) lc.StepName, (object) lc.StepID, (object) str);
        }
        if (lifecycleStep.LCStep != lc.StepID)
        {
          bool flag = true;
          foreach (int nextStep in lifecycleStep.GetNextSteps())
          {
            if (nextStep == lc.StepID)
            {
              dbObject.LCStep = lc.StepID;
              flag = false;
              break;
            }
          }
          if (flag)
            return $"Невозможно изменить шаг ЖЦ объекта '{dbObject.NameInMessages}': перевод на шаг '{lc.StepName}' с шага '{lifecycleStep.LCName}' невозможен.";
        }
      }
      else if (lifecycleStep.LevelID != lc.LevelID)
      {
        int[] nextSteps = lifecycleStep.GetNextSteps();
        bool flag = true;
        foreach (int aLCStepID in nextSteps)
        {
          if (systemSession.GetLifecycleStep(aLCStepID).LevelID == lc.LevelID)
          {
            dbObject.LCStep = aLCStepID;
            flag = false;
            break;
          }
        }
        if (flag)
          return string.Format(LocalizationHolder.rm.GetString("Workflow.Server_26"), (object) dbObject.NameInMessages);
      }
    }
    catch (Exception ex)
    {
      return ex.Message;
    }
    return string.Empty;
  }

  private bool CheckIfStepIDExists(int stepID, int objType, IUserSession systemSession)
  {
    DataRow[] dataRowArray = systemSession.GetLifecycleStepCollection(objType).GetSchema().Tables["IMS_LC_STEPS"].Select();
    if (dataRowArray != null)
    {
      foreach (DataRow dataRow in dataRowArray)
      {
        int int32 = Convert.ToInt32(dataRow["F_LC_STEP"]);
        if (stepID == int32)
          return true;
      }
    }
    return false;
  }

  protected void ProcessLC(LCExec execTime)
  {
    Dictionary<long, LCInfo> attachesLcInfosDictionary;
    string message1 = this.CanProcessLC(execTime, out attachesLcInfosDictionary);
    if (!string.IsNullOrEmpty(message1))
      throw new WorkflowException(message1);
    string message2 = string.Empty;
    IDBTimedEvents service = ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    IUserSession systemSession = !this.UserSession.IsSystemSession ? (service == null ? (IUserSession) this.UserSession : service.GetSystemSessionTemporaryClone("WFActivity.UpdateLCLevel")) : (IUserSession) this.UserSession;
    try
    {
      foreach (KeyValuePair<long, LCInfo> keyValuePair in attachesLcInfosDictionary)
      {
        string str = this.UpdateLCLevel(keyValuePair.Key, keyValuePair.Value, systemSession);
        if (!string.IsNullOrEmpty(str))
          message2 = $"{message2}\r\n{str}";
      }
    }
    finally
    {
      if (!this.UserSession.IsSystemSession && systemSession != null)
        systemSession.Logout("WFActivity.UpdateLCLevel");
    }
    if (!string.IsNullOrEmpty(message2))
      throw new WorkflowException(message2);
  }

  private string CanProcessLC(
    LCExec execTime,
    out Dictionary<long, LCInfo> attachesLcInfosDictionary)
  {
    ILifecycleService customService = this.UserSession.GetCustomService(typeof (ILifecycleService)) as ILifecycleService;
    attachesLcInfosDictionary = new Dictionary<long, LCInfo>();
    string empty = string.Empty;
    LCInfoList lcList = this.LCList.Filter(execTime);
    if (lcList.Count == 0)
      return empty;
    List<NewLCStepInfo> newLcStepInfoList = new List<NewLCStepInfo>();
    Dictionary<int, int> typeToLCLevel = new Dictionary<int, int>();
    Dictionary<int, int> typeToLCstep = new Dictionary<int, int>();
    this._lcDefinedObjectTypes = new List<int>();
    foreach (LCInfo lcInfo in (List<LCInfo>) lcList)
    {
      this._lcDefinedObjectTypes.Add(lcInfo.ObjectType);
      if (lcInfo.Kind == LCKind.Level)
      {
        if (typeToLCLevel.ContainsKey(lcInfo.ObjectType))
          typeToLCLevel[lcInfo.ObjectType] = lcInfo.LevelID;
        else
          typeToLCLevel.Add(lcInfo.ObjectType, lcInfo.LevelID);
        newLcStepInfoList.Add(new NewLCStepInfo(lcInfo.ObjectType, lcInfo.LevelID, false));
      }
      else
      {
        if (typeToLCstep.ContainsKey(lcInfo.ObjectType))
          typeToLCstep[lcInfo.ObjectType] = lcInfo.StepID;
        else
          typeToLCstep.Add(lcInfo.ObjectType, lcInfo.StepID);
        newLcStepInfoList.Add(new NewLCStepInfo(lcInfo.ObjectType, lcInfo.StepID, true));
      }
    }
    HashSet<long> fromAttachmentsList = this.GetObjectIDFromAttachmentsList(MiscFunx.ExpandAttachments((IUserSession) this.UserSession, this.Attachments, true, typeToLCLevel, typeToLCstep), lcList, ref attachesLcInfosDictionary);
    return customService?.ValidateChangeLCStep(fromAttachmentsList.ToArray<long>(), newLcStepInfoList.ToArray());
  }

  private HashSet<long> GetObjectIDFromAttachmentsList(
    AttachmentList attachmentList,
    LCInfoList lcList,
    ref Dictionary<long, LCInfo> attachesLc)
  {
    HashSet<long> fromAttachmentsList = new HashSet<long>();
    foreach (Attachment attachment in (List<Attachment>) attachmentList)
    {
      int mostAppropriateType = MiscFunx.GetMostAppropriateType(attachment.TypeID, this._lcDefinedObjectTypes);
      if (mostAppropriateType != 0)
      {
        int index = this._lcDefinedObjectTypes.IndexOf(mostAppropriateType);
        LCInfo lc = lcList[index];
        fromAttachmentsList.Add(attachment.ObjectID);
        if (attachesLc.ContainsKey(attachment.ObjectID))
          attachesLc[attachment.ObjectID] = lc;
        else
          attachesLc.Add(attachment.ObjectID, lc);
        if (attachment.InnerList != null && attachment.InnerList.Count > 0)
        {
          if (wfConsts.IsECO(attachment.TypeID))
          {
            if ((lc.Kind != LCKind.Step ? lc.LevelID : this.UserSession.GetLifecycleStep(lc.StepID).LevelID) == wfConsts.ProductionLCLevelID)
              continue;
          }
          foreach (long idFromAttachments in this.GetObjectIDFromAttachmentsList(attachment.InnerList, lcList, ref attachesLc))
            fromAttachmentsList.Add(idFromAttachments);
        }
      }
    }
    return fromAttachmentsList;
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

  public virtual bool Collector
  {
    get
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrCollectorID);
      return attributeById != null && attributeById.AsBoolean;
    }
  }

  public virtual bool Abort()
  {
    if (!this.Executed || this is Intermech.Workflow.Server.Activities.Abort)
      return false;
    this.DeleteMessages(0);
    this.DeletePeriodNotificationIfNeeded();
    this.Status = ActivityStatus.Terminated;
    DateTime now = DateTime.Now;
    this.Attributes.AddAttribute(wfConsts.AttrCompletedID, false, new object[1]
    {
      (object) now
    });
    if (this.StartedTime == DateTime.MinValue)
      this.StartedTime = DateTime.Now;
    return true;
  }

  void IExecutedActivity.NextStep(bool goNext)
  {
    ((WFProcess) this.Process).AcquireBlock();
    try
    {
      new WFActivityProxy(this.ProcessID, this, processExecutedCompetedHandler: (WFActivityProxy.ProcessExecutedHandler) ((processID, userID) =>
      {
        IUserSession sessionTemporaryClone = (ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone("IExecutedActivity.NextStep.ReleaseBlock");
        try
        {
          IDBObject dbObject = sessionTemporaryClone.GetObject(processID, false);
          if (dbObject == null || !(dbObject is WFProcess wfProcess2))
            return;
          wfProcess2.ReleaseBlock(userID);
        }
        finally
        {
          sessionTemporaryClone.Logout("IExecutedActivity.NextStep.ReleaseBlock");
        }
      })).NextStepAsync(goNext);
    }
    catch
    {
      ((WFProcess) this.Process).ReleaseBlock();
      throw;
    }
  }

  internal virtual void NextStep(bool goNext)
  {
    if (!this.Executed)
      return;
    try
    {
      if (!goNext && this.RollbackKind == RollbackKind.Disabled)
        this.RaiseException(LocalizationHolder.rm.GetString("Workflow.Server_12"));
      this.ActivityResult = goNext ? ActivityResult.Next : ActivityResult.Back;
      this.AfterExecute();
      this.PrepareNextStepLinks();
      this.MarkNextStepActivitiesAsPreExecuted();
    }
    catch (Exception ex)
    {
      if (ex is AbortException)
        return;
      throw;
    }
  }

  protected virtual void AfterSent()
  {
  }

  protected internal virtual void Copied()
  {
  }

  public IActivity Clone() => (IActivity) this.Clone(new VarList(this.Session, true, false));

  public virtual WFActivity Clone(VarList senderVariablesList)
  {
    WFActivity act = this.UserSession.GetObjectCollection(this.TypeID).Create((IDBObject) this) as WFActivity;
    act.GetAttributeByID(wfConsts.AttrCompletedID)?.Delete(0L);
    act.GetAttributeByID(wfConsts.AttrStartedID)?.Delete(0L);
    act.GetAttributeByID(wfConsts.AttrActivityResultID)?.Delete(0L);
    act.GetAttributeByID(wfConsts.AttrActivityMessageID)?.Delete(0L);
    act.GetAttributeByID(wfConsts.AttrGraphDataID)?.Delete(0L);
    act.ProjectID = this.ProjectID;
    act.Attachments.Clear();
    act.SaveAttachments();
    long num = this.ParentActivityID;
    if (num == 0L)
      num = this.ObjectID;
    act.ParentActivityID = num;
    this._process?.AddActivity(act);
    if (this.IsAlien())
      act.ProcessID = this.ProcessID;
    act.VariableList.Assign(senderVariablesList);
    act.SaveVariables(false);
    act.CommitCreation(false);
    act.LCStep = wfConsts.ActivityExecLCStepID;
    this._clones = (List<WFActivity>) null;
    return act;
  }

  public List<WFActivity> Clones
  {
    get
    {
      if (this._clones == null)
        this.GetRealClones(this.Process);
      return this._clones;
    }
  }

  internal void GetRealClones(WFScheme process)
  {
    this._clones = new List<WFActivity>();
    long conditionValue = this.ParentActivityID;
    bool flag = conditionValue == 0L;
    if (conditionValue == 0L)
      conditionValue = this.ObjectID;
    foreach (DataRow row in (InternalDataCollectionBase) MiscFunx.SimpleSelect((IUserSession) this.UserSession, this.ObjectType, new ConditionStructure[2]
    {
      new ConditionStructure(wfConsts.AttrProcessID, RelationalOperators.Equal, (object) process.ObjectID, LogicalOperators.AND, 0, false),
      new ConditionStructure(wfConsts.AttrParentActivityID, RelationalOperators.Equal, (object) conditionValue, LogicalOperators.AND, 0, false)
    }).Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      if (!flag)
      {
        if (int64 != this.ObjectID)
          break;
        flag = true;
      }
      this._clones.Add(process.GetDBActivity(int64));
    }
  }

  internal ExtProperties ExtProps
  {
    get
    {
      return this._extProps ?? (this._extProps = new ExtProperties((IDBObject) this, wfConsts.AttrAddInfoID));
    }
  }

  public override IDBObject DoCheckout()
  {
    string key = $"WFActivity_InCheckOut_{this.ObjectID}";
    this.UserSession.SetSessionPluginsData((object) key, (object) true);
    this.UserSession.ClearObjectSmartCache();
    try
    {
      return base.DoCheckout();
    }
    finally
    {
      this.UserSession.RemoveSessionPluginsData((object) key);
    }
  }

  public override void CommitCreation(bool deleteOnException, bool autoCheckout)
  {
    this._variablesList = (VarList) null;
    this._Attributes = (IDBAttributeCollection) null;
    base.CommitCreation(deleteOnException, autoCheckout);
  }

  public override IDBAttributeCollection Attributes
  {
    get
    {
      if (this._Attributes == null)
      {
        this._Attributes = (IDBAttributeCollection) new ActivityAttributeCollection(this.UserSession, this.ObjectID, this.ObjectType, (IDBAttributable) this);
        this.AddVirtualAttributes();
      }
      return this._Attributes;
    }
  }

  private void AddVirtualAttributes()
  {
    string key = $"WFActivity_AddVirtualAttributes_{this.ObjectID}";
    if (object.Equals(this.UserSession.GetSessionPluginsData((object) key), (object) true))
      return;
    this.UserSession.SetSessionPluginsData((object) key, (object) true);
    try
    {
      this.DoAddVirtualAttributes();
    }
    finally
    {
      this.UserSession.RemoveSessionPluginsData((object) key);
    }
  }

  private void DoAddVirtualAttributes() => this.VariableList.AddVirtualAttributes((IDBObject) this);

  public virtual bool CanEditAttributes() => this.IsCreationMode;

  public override IDBAttribute GetAttributeByID(int attributeID)
  {
    IDBAttribute attributeById = base.GetAttributeByID(attributeID);
    string key = $"WFActivity_LoadingVars_{this.ObjectID}";
    if (attributeById == null && !object.Equals(this.UserSession.GetSessionPluginsData((object) key), (object) true) && this.VariableList.GetVariable(attributeID) != null)
    {
      this.AddVirtualAttributes();
      attributeById = this.Attributes.FindByID(attributeID);
    }
    if (attributeById == null && !object.Equals(this.UserSession.GetSessionPluginsData((object) key), (object) true) && this._process != null && this.GlobalVariables.GetVariable(attributeID) != null)
      attributeById = this.Process?.Attributes.FindByID(attributeID);
    return attributeById;
  }

  public override AttributeValues[] GetAttributesValues(GetAttributeValuesModes modes)
  {
    this._requestedByForm = (modes & GetAttributeValuesModes.RequestedByForm) == GetAttributeValuesModes.RequestedByForm;
    this.AddVirtualAttributes();
    AttributeValues[] attributesValues = base.GetAttributesValues(modes);
    List<int> editableVarIDs = new List<int>();
    if (this._process != null)
      attributesValues = this.Process.GetGlobalAttributesValues(modes, attributesValues, editableVarIDs);
    if (this.IsCreationMode)
      return attributesValues;
    editableVarIDs.AddRange((IEnumerable<int>) this.VariableList.EditableVarIDs);
    bool flag1 = this.CanEditAttributes() || this.ObjectType == wfConsts.SchemesTypeID && this._requestedByForm;
    bool flag2 = false;
    foreach (AttributeValues attributeValues in attributesValues)
    {
      if (attributeValues.AttributeType != FieldTypes.ftSystem)
      {
        attributeValues.ReadOnly = !flag1 || !editableVarIDs.Contains(attributeValues.AttributeID);
        if (!flag1 && !flag2 && wfConsts.ProtectedAttributeTypes.Contains(attributeValues.AttributeID))
          flag2 = true;
      }
    }
    if (!flag2)
      return attributesValues;
    List<AttributeValues> attributeValuesList = new List<AttributeValues>();
    foreach (AttributeValues attributeValues in attributesValues)
    {
      if (!wfConsts.ProtectedAttributeTypes.Contains(attributeValues.AttributeID))
        attributeValuesList.Add(attributeValues);
    }
    return attributeValuesList.ToArray();
  }

  public override AttributeValues[] SetAttributesValues(
    AttributeValues[] valuesList,
    bool deleteNotExistingAttributes,
    bool dontDeleteBlobs,
    bool returnDelta,
    GetAttributeValuesModes modes,
    Dictionary<string, Exception> exceptionsList)
  {
    this.AddVirtualAttributes();
    foreach (AttributeValues values in valuesList)
    {
      for (int index = 0; index < values.Values.Length; ++index)
      {
        if (DeleteModesEnum.None.Equals(values.Values[index]))
          values.Values[index] = (object) DBNull.Value;
      }
    }
    string key = $"WFActivity_InSetAttrValues_{this.ObjectID}";
    this.UserSession.SetSessionPluginsData((object) key, (object) true);
    try
    {
      List<AttributeValues> attributeValuesList1 = new List<AttributeValues>();
      attributeValuesList1.AddRange((IEnumerable<AttributeValues>) valuesList);
      if (this._process != null)
      {
        AttributeValues[] attributesValues = this.Process.GetGlobalAttributesValues(modes, new AttributeValues[0], new List<int>());
        if (attributesValues.Length != 0)
        {
          List<int> list = ((IEnumerable<AttributeValues>) attributesValues).Select<AttributeValues, int>((System.Func<AttributeValues, int>) (x => x.AttributeID)).ToList<int>();
          IEnumerable<int> source = ((IEnumerable<AttributeValues>) valuesList).Select<AttributeValues, int>((System.Func<AttributeValues, int>) (x => x.AttributeID)).ToList<int>().Intersect<int>((IEnumerable<int>) list);
          if (source.Any<int>())
          {
            List<AttributeValues> attributeValuesList2 = new List<AttributeValues>();
            foreach (int num in source)
            {
              int values = num;
              AttributeValues attributeValues = ((IEnumerable<AttributeValues>) valuesList).ToList<AttributeValues>().Find((Predicate<AttributeValues>) (x => x.AttributeID == values));
              attributeValues.ReadOnly = false;
              attributeValuesList2.Add(attributeValues);
              attributeValuesList1.Remove(attributeValues);
            }
            this.Process.SetAttributesValues(attributeValuesList2.ToArray(), deleteNotExistingAttributes, dontDeleteBlobs, returnDelta, modes, exceptionsList);
          }
        }
      }
      AttributeValues[] attributeValuesArray = base.SetAttributesValues(attributeValuesList1.ToArray(), deleteNotExistingAttributes, dontDeleteBlobs, returnDelta, modes, exceptionsList);
      if (this.VariableList.FillByVirtualAttributes((IDBObject) this))
        this.SaveVariables(false);
      return attributeValuesArray;
    }
    finally
    {
      this.UserSession.RemoveSessionPluginsData((object) key);
    }
  }

  protected override void DoAfterSetAdditionalAttributeValue(IDBAttribute attribute)
  {
    base.DoAfterSetAdditionalAttributeValue(attribute);
    string key1 = $"WFActivity_InSetAttrValues_{this.ObjectID}";
    string key2 = $"WFActivity_InCheckOut_{this.ObjectID}";
    if (object.Equals(this.UserSession.GetSessionPluginsData((object) $"WFActivity_AddVirtualAttributes_{this.ObjectID}"), (object) true) || object.Equals(this.UserSession.GetSessionPluginsData((object) key1), (object) true) || object.Equals(this.UserSession.GetSessionPluginsData((object) key2), (object) true) || object.Equals(this.UserSession.GetSessionPluginsData((object) $"WFActivity_LoadingVars_{this.ObjectID}"), (object) true) || !attribute.TemporaryAttribute || this._Attributes == null || this._Attributes.FindByID(attribute.AttributeID) == null || !this.VariableList.FillByVirtualAttribute((IDBObject) this, attribute))
      return;
    this.SaveVariables(false);
  }

  protected void UpdateTempAttributeValue(int AttrTypeID)
  {
    Variable variable = this.VariableList?.GetVariable(AttrTypeID);
    if (variable == null)
      return;
    if (variable is CalculatedSystemVariable calculatedSystemVariable)
      calculatedSystemVariable.ClearCache();
    if (!this.VariableList.VirtualAdded)
      return;
    IDBAttribute byId = this.Attributes.FindByID(AttrTypeID);
    if (byId == null)
      return;
    byId.Value = variable.TypedValue;
  }

  public void SaveVariables(bool clearAttributes = true)
  {
    if (this.VariableList == null || !this.VariableList.Modified)
      return;
    this.VariableList.Save((IDBObject) this, false);
    this.VariableList.Modified = false;
    if (!clearAttributes)
      return;
    this._Attributes = (IDBAttributeCollection) null;
  }

  internal void DumpException(Exception e)
  {
    string stack = e is ISimpleMessageException ? string.Empty : $"<hr><small>{e.StackTrace}\r\n</small>";
    this.DumpError(e.Message, stack, !(e is IRollbackException), !(e is NotAllSignedException));
  }

  protected void DumpError(
    string message,
    string stack,
    bool autoRollbackPrefix = true,
    bool sendAdminNotify = true)
  {
    if (this.RollbackKind == RollbackKind.Disabled)
      this._rollbackKind = RollbackKind.Start;
    this.ActivityResult = ActivityResult.Back;
    if (!string.IsNullOrEmpty(stack))
      message = $"{message}\r\n{stack}";
    string messageText = this.MessageText;
    if (!string.IsNullOrEmpty(messageText))
      messageText += "\r\n<br /><br />\r\n";
    string empty = string.Empty;
    string str = autoRollbackPrefix ? string.Format(LocalizationHolder.rm.GetString("AutoRollbackError"), (object) this.Caption, (object) message) : message;
    this.MessageText = messageText + str;
    if (!(GlobalMailSettings.Cfg.NotifyAdminAboutErrors & autoRollbackPrefix & sendAdminNotify))
      return;
    QuickObjectInfo objectInfo1 = this.Session.GetObjectInfo(GlobalMailSettings.Cfg.WorkflowAdminUserID);
    long prototypeSchemeId = this.Process is WFProcess process ? process.PrototypeSchemeID : 0L;
    string Subject = $"{LocalizationHolder.rm.GetString("Workflow.Server_50")}{this.ProcessName}\"";
    string Text = $"{string.Format(LocalizationHolder.rm.GetString("Workflow.Server_51"), (object) this.ProcessID, (object) this.ProcessName, (object) this.ObjectID, (object) this.Name)}{message}<br />{stack}";
    if (prototypeSchemeId == 0L && objectInfo1.ObjectTypeID == wfConsts.UserTypeID)
    {
      ServerFunx.CreateMessage((IUserSession) this.UserSession, GlobalMailSettings.Cfg.WorkflowAdminUserID, Subject, Text, this.ProcessID, this.ObjectID);
    }
    else
    {
      if (prototypeSchemeId == 0L)
        return;
      IDBObject dbObject = this.Session.GetObject(prototypeSchemeId, false);
      if (dbObject == null)
        return;
      IDBAttribute byId = dbObject.Attributes.FindByID(wfConsts.SchemeAdministratorID);
      if (byId != null && !byId.IsNull)
      {
        long asInteger = byId.AsInteger;
        QuickObjectInfo objectInfo2 = this.Session.GetObjectInfo(asInteger);
        if (objectInfo2.ObjectTypeID == wfConsts.UserTypeID)
        {
          ServerFunx.CreateMessage((IUserSession) this.UserSession, asInteger, Subject, Text, this.ProcessID, this.ObjectID);
        }
        else
        {
          if (objectInfo2.ObjectTypeID != wfConsts.GroupTypeID)
            return;
          foreach (long RecipID in MiscFunx.ExpandGroup(this.Session, asInteger))
            ServerFunx.CreateMessage((IUserSession) this.UserSession, RecipID, Subject, Text, this.ProcessID, this.ObjectID);
        }
      }
      else
      {
        if (objectInfo1.ObjectTypeID != wfConsts.UserTypeID)
          return;
        ServerFunx.CreateMessage((IUserSession) this.UserSession, GlobalMailSettings.Cfg.WorkflowAdminUserID, Subject, Text, this.ProcessID, this.ObjectID);
      }
    }
  }

  internal bool IsBlockStart
  {
    get
    {
      if (!this._isBlockStart.HasValue)
        this._isBlockStart = new bool?(this.GetParallelBlockLink(LinkDirection.From) != null);
      return this._isBlockStart.Value;
    }
  }

  protected long ThreadID
  {
    get
    {
      if (this._threadID == -1L)
        this._threadID = this.ExtProps.HasFlag(ExtPropertiesFlag.ThreadID) ? this.ExtProps.ReadInteger(nameof (ThreadID)) : 0L;
      return this._threadID;
    }
    set
    {
      if (this.ThreadID == value)
        return;
      this._threadID = value;
      if (!this.ExtProps.Write(nameof (ThreadID), value, ExtPropertiesFlag.ThreadID, "0"))
        return;
      this.ExtProps.Save((IDBObject) this);
    }
  }

  protected WFLink GetParallelBlockLink(LinkDirection direction)
  {
    List<WFLink> wfLinkList;
    if (direction == LinkDirection.From)
    {
      wfLinkList = this.AllLinksFromThis;
    }
    else
    {
      if (this._parallelBackLinks == null)
      {
        this._parallelBackLinks = new List<WFLink>();
        this.Process.LoadParallelLink(this._parallelBackLinks, LinkDirection.To, new LinkKind[1]
        {
          LinkKind.ParallelBlock
        }, false, this);
      }
      wfLinkList = this._parallelBackLinks;
    }
    if (wfLinkList != null)
    {
      foreach (WFLink parallelBlockLink in wfLinkList)
      {
        if (parallelBlockLink.Kind == LinkKind.ParallelBlock)
          return parallelBlockLink;
      }
    }
    return (WFLink) null;
  }

  internal void RaiseException(string message)
  {
    throw new WorkflowException($"{message} [{this.Caption}]");
  }

  protected long PortalReplicatorUserID => this.Session.IdentHelper.SystemID;

  protected StringList PortalInfo
  {
    get
    {
      if (!this._portalInfoLoaded)
      {
        string str = ((WFProcess) this.Process).ExtProps.Read(nameof (PortalInfo));
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
        IDBAttribute attributeById = this.Process.GetAttributeByID(wfConsts.AttrExecHistoryID);
        if (attributeById != null && !attributeById.IsNull && this.UserSession.GetObject(Convert.ToInt64(attributeById.Values[attributeById.ValuesCount - sc_22118.ssp_workflow_server_22122(793256111)]), false) is WFActivity wfActivity)
        {
          if (wfActivity.WaitForCompletion)
          {
            try
            {
              this.ForwardDataFlow(wfActivity);
              wfActivity.NextStep(goNext);
              new WFActivityProxy(wfActivity.ProcessID, wfActivity, this.NonUserActivitiesCounter).ExecuteNextActivity(wfActivity.SenderID, wfActivity.ObjectID, wfActivity.NextStepLinks, wfActivity.VariableList);
            }
            catch (Exception ex)
            {
              if (!(ex is AbortException))
                throw;
            }
            flag = true;
          }
        }
        if (goNext)
        {
          if (!flag)
          {
            if (this.Process.LinkedTaskObjectID != 0L)
            {
              Intermech.Project.Task task = StandaloneTask.Get(this.Session, this.Process.LinkedTaskObjectID);
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
      this.DumpException(ex);
      this.ActivityResult = ActivityResult.Back;
      if (this.NextStepLinks.Count == 0)
      {
        this._rollbackKind = RollbackKind.Start;
        this.PrepareNextStepLinks();
      }
      if (this.Process != null)
      {
        ((WFProcess) this.Process).ProcessStatus = ActivityStatus.Executed;
        this.Process.GetAttributeByID(wfConsts.AttrActivityResultID)?.Delete(0L);
        this.Process.GetAttributeByID(wfConsts.AttrCompletedID)?.Delete(0L);
        Start startActivity = this.Process.StartActivity;
        if (startActivity != null && startActivity.ParticipantID == this.PortalReplicatorUserID)
        {
          ParticipantList participants = startActivity.Participants;
          participants.Clear();
          participants.AddParticipant(ParticipantKind.User, GlobalMailSettings.Cfg.WorkflowAdminUserID);
          startActivity.SaveParticipants(true);
        }
      }
      return false;
    }
    return true;
  }

  public bool WaitForCompletion
  {
    get
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrWaitForCompletionID);
      return attributeById != null && attributeById.AsBoolean;
    }
    set
    {
      this.Attributes.AddAttribute(wfConsts.AttrWaitForCompletionID, false, new object[1]
      {
        (object) value
      });
    }
  }

  public virtual void ForwardDataFlow(WFActivity toAct, bool passAll = true, int nonUserActivitiesCounter = 0)
  {
    IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrExecHistoryID);
    IDBAttribute dbAttribute = toAct.Attributes.AddAttribute(wfConsts.AttrExecHistoryID, false);
    if (toAct.Collector && dbAttribute != null && !dbAttribute.IsNull && attributeById != null)
    {
      object[] values1 = attributeById.Values;
      object[] values2 = dbAttribute.Values;
      int length1 = values1.Length;
      bool flag = false;
      for (int index = 0; index < length1; ++index)
      {
        object val = values1[index];
        if (!Array.Exists<object>(values2, (Predicate<object>) (obj => obj.Equals(val))))
        {
          int length2 = values2.Length;
          Array.Resize<object>(ref values2, length2 + 1);
          values2[length2] = val;
          flag = true;
        }
      }
      if (flag)
        ((DBAttribute) dbAttribute).DirectSetValues(values2);
    }
    else if (attributeById != null)
      ((DBAttribute) dbAttribute)?.DirectSetValues(attributeById.Values);
    if (dbAttribute != null)
    {
      if (dbAttribute.IsNull)
        dbAttribute.Value = (object) this.ObjectID;
      else if (!Array.Exists<object>(dbAttribute.Values, (Predicate<object>) (obj => obj.Equals((object) this.ObjectID))))
        dbAttribute.AddValue((object) this.ObjectID);
    }
    toAct.SenderID = this.SenderParticipantID;
    toAct.SenderActivityID = this.ObjectID;
    if (this.IsBlockStart && this.ThreadID == 0L)
    {
      if (this.ActivityResult == ActivityResult.Next)
        toAct.ThreadID = toAct.ParentActivityID != 0L ? toAct.ParentActivityID : toAct.ObjectID;
    }
    else
      toAct.ThreadID = this.ThreadID;
    toAct.Priority = this.Priority;
    toAct.NonUserActivitiesCounter = nonUserActivitiesCounter;
    if (!passAll)
      return;
    this.TransferAttachments(toAct);
    if (toAct.Process.Variables.Count > toAct.VariableList.Count)
      toAct.VariableList.Assign(toAct.Process.Variables);
    foreach (Variable variable1 in this.VariableList)
    {
      if (variable1.Kind != VarKind.System && (!GlobalMailSettings.Cfg.CollSkipEmptyVars || toAct.Status != ActivityStatus.CollectorWaiting || !variable1.IsEmpty))
      {
        Variable variable2 = toAct.VariableList.GetVariable(variable1.AttrTypeID);
        if (variable2 != null)
          variable2.Value = variable1.Value;
      }
    }
    toAct.VariableList.AddSystemVariables((IDBObject) this);
    toAct.SaveVariables();
  }

  public void ForwardDataFlow(WFProcess toProcess)
  {
    IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrExecHistoryID);
    IDBAttribute dbAttribute = toProcess.Attributes.AddAttribute(wfConsts.AttrExecHistoryID, false);
    if (attributeById != null)
      ((DBAttribute) dbAttribute)?.DirectSetValues(attributeById.Values);
    if (dbAttribute != null)
    {
      if (dbAttribute.IsNull)
        dbAttribute.Value = (object) this.ObjectID;
      else if (!Array.Exists<object>(dbAttribute.Values, (Predicate<object>) (obj => obj.Equals((object) this.ObjectID))))
        dbAttribute.AddValue((object) this.ObjectID);
    }
    toProcess.Priority = this.Priority;
  }

  public TemporaryRights TempRights
  {
    get
    {
      if (this._temporaryRights == -1L)
      {
        this._temporaryRights = 0L;
        IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrTempRightsID);
        if (attributeById != null)
          this._temporaryRights = attributeById.AsInteger;
      }
      return (TemporaryRights) this._temporaryRights;
    }
  }

  private void HandleTemporaryRights(bool add, Attachment att)
  {
    if (!(ApplicationServices.Container.GetService(typeof (ITemporaryAccessService)) is ITemporaryAccessService service1))
      return;
    if (add)
    {
      if (att.RelationOwnerID <= 0L)
        return;
      bool flag = false;
      IUserSession session = this.Session;
      if (att.RelationOwnerID == this.Session.IdentHelper.SystemID && ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) is IDBTimedEvents service2)
      {
        session = service2.GetSystemSessionTemporaryClone("WFActivity.HandleTemporaryRights.GrantedSession");
        flag = true;
      }
      try
      {
        service1.GrantAccess(session, att.RelationOwnerID, this.ParticipantID, att.ObjectID, (this.TempRights & TemporaryRights.View) != 0, (this.TempRights & TemporaryRights.Edit) != 0, (this.TempRights & TemporaryRights.Admin) != 0);
        if (att.InnerList == null || att.InnerList.Count <= 0 || (this.TempRights & TemporaryRights.HandleGrouped) == TemporaryRights.None)
          return;
        foreach (Attachment inner in (List<Attachment>) att.InnerList)
          service1.GrantAccess(session, att.RelationOwnerID, this.ParticipantID, inner.ObjectID, (this.TempRights & TemporaryRights.View) != 0, (this.TempRights & TemporaryRights.Edit) != 0, (this.TempRights & TemporaryRights.Admin) != 0);
      }
      finally
      {
        if (flag)
          session.Logout("WFActivity.HandleTemporaryRights.GrantedSession");
      }
    }
    else
    {
      service1.ClearAccess(this.Session, this.ParticipantID, att.ObjectID);
      if (att.InnerList == null || att.InnerList.Count <= 0 || (this.TempRights & TemporaryRights.HandleGrouped) == TemporaryRights.None)
        return;
      foreach (Attachment inner in (List<Attachment>) att.InnerList)
        service1.ClearAccess(this.Session, this.ParticipantID, inner.ObjectID);
    }
  }

  internal void HandleTemporaryRights(bool add)
  {
    this.HandleTemporaryRights(this.Attachments, add);
  }

  internal void HandleTemporaryRights(AttachmentList attachments, bool add)
  {
    if (this.TempRights <= TemporaryRights.None || this.ParticipantID == wfConsts.SystemUserID || attachments.Count <= 0)
      return;
    if ((this.TempRights & TemporaryRights.HandleGrouped) != TemporaryRights.None)
      attachments = MiscFunx.ExpandAttachments(this.Session, attachments);
    if (add)
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      Dictionary<long, List<Attachment>> dictionary = new Dictionary<long, List<Attachment>>();
      foreach (Attachment attachment in (List<Attachment>) attachments)
      {
        if (dictionary.ContainsKey(attachment.RelationOwnerID))
        {
          dictionary[attachment.RelationOwnerID].Add(attachment);
        }
        else
        {
          List<Attachment> attachmentList = (List<Attachment>) new AttachmentList();
          attachmentList.Add(attachment);
          dictionary.Add(attachment.RelationOwnerID, attachmentList);
        }
      }
      foreach (KeyValuePair<long, List<Attachment>> keyValuePair in dictionary)
      {
        StringBuilder stringBuilder2 = new StringBuilder();
        foreach (Attachment attachment in keyValuePair.Value)
        {
          if (stringBuilder2.Length != 0)
            stringBuilder2.Append(",\r\n");
          stringBuilder2.AppendFormat("<a href=\"#object={0}\">{1}</a>", (object) attachment.ObjectID, (object) MiscFunx.GetObjectCaption(this.Session, attachment.ObjectID));
        }
        if (this.Session.GetObjectLevel(keyValuePair.Key) == this.Session.IdentHelper.AnnulmentLevelID)
        {
          if (stringBuilder1.Length != 0)
            stringBuilder1.AppendLine();
          stringBuilder1.AppendFormat("Ошибка назначения временных прав на объекты, которые прикрепил пользователь '{0}', так как данный пользователь находится на шаге ЖЦ 'Уволен'. Объекты не прошедшие проверку:", (object) MiscFunx.GetObjectCaption(this.Session, keyValuePair.Key, false));
          stringBuilder1.AppendLine();
          stringBuilder1.Append((object) stringBuilder2);
        }
        else
        {
          IDBRelationCollection relationCollection = this.Session.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad00022-306c-11d8-b4e9-00304f19f545"));
          relationCollection.ObjectTypeID = MetaDataHelper.GetObjectTypeID("cad00007-306c-11d8-b4e9-00304f19f545");
          DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[1]
          {
            (object) ObligatoryObjectAttributes.F_OBJECT_ID
          });
          if (relationCollection.EntersIn(paramSet, this.Session.GetIDByObjectID(keyValuePair.Key)).Rows.Count == 0)
          {
            if (stringBuilder1.Length != 0)
              stringBuilder1.AppendLine();
            stringBuilder1.AppendFormat("Ошибка назначения временных прав на объекты, которые прикрепил пользователь '{0}', так как у данного пользователя отсутствуют назначенные роли. Объекты не прошедшие проверку:", (object) MiscFunx.GetObjectCaption(this.Session, keyValuePair.Key, false));
            stringBuilder1.AppendLine();
            stringBuilder1.Append((object) stringBuilder2);
          }
        }
      }
      if (stringBuilder1.Length > 0)
        throw new WorkflowException(stringBuilder1.ToString());
    }
    Dictionary<long, List<Attachment>> dictionary1 = new Dictionary<long, List<Attachment>>();
    foreach (Attachment attachment in (List<Attachment>) attachments)
    {
      try
      {
        this.HandleTemporaryRights(add, attachment);
      }
      catch (AccessDeniedException ex)
      {
        List<Attachment> attachmentList = (List<Attachment>) null;
        if (!dictionary1.TryGetValue(attachment.RelationOwnerID, out attachmentList))
        {
          attachmentList = new List<Attachment>();
          dictionary1.Add(attachment.RelationOwnerID, attachmentList);
        }
        attachmentList.Add(attachment);
      }
    }
    if (!add || dictionary1.Count <= 0 || !GlobalMailSettings.Cfg.SendTempRightsError)
      return;
    string Subject = LocalizationHolder.rm.GetString("TempRightsErr");
    string str = LocalizationHolder.rm.GetString("TempRightsErrText");
    foreach (KeyValuePair<long, List<Attachment>> keyValuePair in dictionary1)
    {
      string empty = string.Empty;
      foreach (Attachment attachment in keyValuePair.Value)
      {
        if (!string.IsNullOrEmpty(empty))
          empty += ",\r\n";
        empty += $"<a href=\"#object={attachment.ObjectID}\">{MiscFunx.GetObjectCaption(this.Session, attachment.ObjectID)}</a>";
      }
      this.CreateMessage(keyValuePair.Key, Subject, str + empty);
    }
  }

  public Terms Terms
  {
    get
    {
      if (this._terms == null)
      {
        this._terms = new Terms((IUserSession) this.UserSession);
        this._terms.Load((IDBObject) this);
      }
      return this._terms;
    }
  }

  internal void StartTerms()
  {
    if (this.Terms.Term.Period != null)
    {
      DateTime dateTime = this.RegisterTermNotification(this._terms.Term, Intermech.Workflow.EventKind.UncompleteTerm);
      if (dateTime != DateTime.MinValue)
      {
        dateTime = dateTime.ToLocalTime();
        this.Attributes.AddAttribute(wfConsts.AttrCompletedTermID, false, new object[1]
        {
          (object) dateTime
        });
      }
    }
    if (this._terms.ReadTerm.Period == null)
      return;
    this.RegisterTermNotification(this._terms.ReadTerm, Intermech.Workflow.EventKind.UnreadTerm);
  }

  private DateTime RegisterTermNotification(Term term, Intermech.Workflow.EventKind eventKind)
  {
    DateTime startDate = DateTime.MinValue;
    if (term.Period != null)
      startDate = term.Period.GetExecTime((IDBObject) this);
    if (!term.Enabled)
      return startDate;
    if (startDate > DateTime.UtcNow)
    {
      IDBTimedEvents service = ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
      service.AddToTrace($"Событие N{service.AddEvent(new TimedEventProperties(0, startDate, DateTime.MinValue, wfConsts.WorkflowTimerServiceGuid, this.ObjectID, 0L, string.Empty, (int) eventKind, 0), this.UserSession.DataManager)} для объекта N{this.ObjectID} зарегистрировано.", true);
    }
    else
    {
      this.ActivityResult = ActivityResult.Back;
      this._autoStep = true;
      this.AddAutoRollbackMessage(eventKind);
      this.ErrorOccured = true;
    }
    return startDate;
  }

  internal void UnregisterTermNotifications()
  {
    if (this.Terms.Term.Period != null)
      this.UnregisterTermNotification(this.Terms.Term, Intermech.Workflow.EventKind.UncompleteTerm);
    if (this.Terms.ReadTerm.Period == null)
      return;
    this.UnregisterTermNotification(this.Terms.ReadTerm, Intermech.Workflow.EventKind.UnreadTerm);
  }

  internal void UnregisterTermNotification(Term term, Intermech.Workflow.EventKind eventKind)
  {
    if (!term.Enabled)
      return;
    IDBTimedEvents service = ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    int eventID = service.FindEvent(wfConsts.WorkflowTimerServiceGuid, (int) eventKind, this.ObjectID, this.UserSession.DataManager);
    if (eventID <= 0)
      return;
    service.DeleteEventID(eventID, this.UserSession.DataManager);
  }

  internal void AddAutoRollbackMessage(Intermech.Workflow.EventKind eventKind)
  {
    string str = "??";
    switch (eventKind)
    {
      case Intermech.Workflow.EventKind.UncompleteTerm:
        str = LocalizationHolder.rm.GetString("Workflow.Server_32");
        break;
      case Intermech.Workflow.EventKind.UnreadTerm:
        str = LocalizationHolder.rm.GetString("Workflow.Server_33");
        break;
    }
    this.MessageText = string.Format(LocalizationHolder.rm.GetString(sc_22118.ssp_workflow_server_22123()), (object) str);
  }

  private DataTable GetLocalScripts(long objectID)
  {
    IDBRelationCollection relationCollection = this.UserSession.GetRelationCollection(wfConsts.ScriptRelationTypeID);
    relationCollection.LocalTypesMode = true;
    object[] columns = new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    };
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[2]
    {
      new ConditionStructure(-21, RelationalOperators.Equal, (object) objectID, LogicalOperators.AND, 0, false),
      new ConditionStructure(-7, RelationalOperators.Equal, (object) wfConsts.WorkflowLocalScript, LogicalOperators.AND, 0, false)
    }, columns, 0L, (object) null, -1);
    return relationCollection.Select(paramSet);
  }

  public List<long> LocalScriptsInCurrentActivity
  {
    get
    {
      List<long> inCurrentActivity = new List<long>();
      foreach (DataRow row in (InternalDataCollectionBase) this.GetLocalScripts(this.ObjectID).Rows)
        inCurrentActivity.Add(Convert.ToInt64(row.ItemArray[0]));
      return inCurrentActivity;
    }
  }

  public override void SetDeletionStatus(MailFolder folder, DeletionStatus status)
  {
    if (folder == MailFolder.Inbox && this.Flags.HasFlag((Enum) ActivityFlags.DenyDeletionFromMail))
      throw new NotificationException(string.Format(LocalizationHolder.rm.GetString(sc_22118.ssp_workflow_server_22124()), (object) this.Caption));
    base.SetDeletionStatus(folder, status);
  }

  internal void MarkNextStepActivitiesAsPreExecuted()
  {
    foreach (WFLink nextStepLink in this.NextStepLinks)
      ((WFProcess) this.Process).MarkAsPreExecuted(nextStepLink.ToID, true);
  }
}
