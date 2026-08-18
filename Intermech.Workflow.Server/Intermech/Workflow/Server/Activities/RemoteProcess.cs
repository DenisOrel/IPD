// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.Activities.RemoteProcess
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Workflow.Server.Activities;

public class RemoteProcess(UserSession uSession, DataTable objectsTable) : 
  SystemActivity(uSession, objectsTable),
  IUserActivity
{
  private bool? _giveOwnership;
  private int? _maxCompositionLevel;
  private bool _actingUserIDSet;

  public override ActivityKind Kind => ActivityKind.RemoteSubProcess;

  public override string Validate(bool checkSubProcessSchemes = true, List<long> checkedSchemesList = null)
  {
    string s = base.Validate(checkSubProcessSchemes, checkedSchemesList);
    if (string.IsNullOrEmpty(this.ExtProps.Read("Site")) || string.IsNullOrEmpty(this.ExtProps.Read("TplGuid")))
      MiscFunx.AddNewLined(ref s, MiscFunx.ActivityIncomplete(this.Name));
    if (this.Terms.Invalid)
      MiscFunx.AddNewLined(ref s, string.Format(LocalizationHolder.rm.GetString(sc_22142.ssp_workflow_server_22143()), (object) this.Name));
    return s;
  }

  public bool GiveOwnership
  {
    get
    {
      if (!this._giveOwnership.HasValue)
        this._giveOwnership = new bool?(this.ExtProps.ReadBool(nameof (GiveOwnership)));
      return this._giveOwnership.Value;
    }
  }

  public int MaxCompositionLevel
  {
    get
    {
      if (!this._maxCompositionLevel.HasValue)
        this._maxCompositionLevel = new int?((int) this.ExtProps.ReadInteger(nameof (MaxCompositionLevel), -1L));
      return this._maxCompositionLevel.Value;
    }
  }

  public RemoteProcessStatus RemoteStatus
  {
    get
    {
      long remoteStatus = 0;
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrRemoteProcessStatusID);
      if (attributeById != null)
        remoteStatus = attributeById.AsInteger;
      return (RemoteProcessStatus) remoteStatus;
    }
    set
    {
      this.Attributes.AddAttribute(wfConsts.AttrRemoteProcessStatusID, false, new object[1]
      {
        (object) (long) value
      });
    }
  }

  private List<int> EnableObjectTypes
  {
    get
    {
      List<int> first = (this.Session.GetCustomService(typeof (IPublishTypesConfiguration)) as IPublishTypesConfiguration).PublishObjectTypes;
      if (first == null || first.Count <= 0)
        return (List<int>) null;
      List<int> second = this.ExtProps.ReadList<int>("FTypes");
      if (second != null)
        first = first.Except<int>((IEnumerable<int>) second).ToList<int>();
      return first;
    }
  }

  private List<int> EnableRelationTypes
  {
    get
    {
      List<int> first = (this.Session.GetCustomService(typeof (IPublishTypesConfiguration)) as IPublishTypesConfiguration).PublishRelationTypes;
      if (first == null || first.Count <= 0)
        return (List<int>) null;
      List<int> second = this.ExtProps.ReadList<int>("FRelTypes");
      if (second != null)
        first = first.Except<int>((IEnumerable<int>) second).ToList<int>();
      return first;
    }
  }

  public bool AutoPublishReplication
  {
    get => this.ExtProps.Ini.ReadBoolean("Props", nameof (AutoPublishReplication), true);
  }

  public TaskPriority RemoteTaskPriority
  {
    get => (TaskPriority) this.ExtProps.ReadInteger(nameof (RemoteTaskPriority), 0L);
  }

  protected void LaunchProcess()
  {
    this._autoStep = !this.WaitForCompletion;
    if (!(ApplicationServices.Container.GetService(typeof (ICustomPublisherService)) is ICustomPublisherService service))
      throw new Exception("Сервис публикации на портал не найден.");
    StringList sl = new StringList();
    sl.Values["Launch"] = "1";
    sl.Values["RTGuid"] = this.ExtProps.Read("TplGuid");
    sl.Values["PID"] = this.ProcessID.ToString();
    sl.Values["AID"] = this.ObjectID.ToString();
    sl.Values["SrcProcessName"] = this.ProcessName;
    sl.Values["Wait"] = this._autoStep ? "0" : "1";
    sl.Values["GiveOwnership"] = this.GiveOwnership ? "1" : "0";
    sl.Values["MaxCompositionLevel"] = this.MaxCompositionLevel.ToString();
    ISitesCacheService customService = this.UserSession.GetCustomService(typeof (ISitesCacheService)) as ISitesCacheService;
    sl.Values["SrcSite"] = customService.Info.GUID.ToString();
    sl.Values["SrcSiteName"] = customService.Info.Caption;
    SiteInfo site = customService.GetSite(new Guid(this.ExtProps.Read("Site")));
    bool flag = site.SystemType == SystemTypes.IPS;
    bool createReceipt = flag && this.ExtProps.ReadBool("CreateReceipt");
    sl.Values["CreateReceipt"] = createReceipt ? "1" : "0";
    List<long> attachments = WorkflowPortalHandler.ForwardDataFlow((IUserSession) this.UserSession, (WFActivity) this, sl);
    string str1 = $"{LocalizationHolder.GetString("PortalStartProcessPrefix")} \"{site.Caption} / {this.ExtProps.Read("TplName")}\"";
    this.RemoteStatus = RemoteProcessStatus.WaitingForPublish;
    char? nullable = new char?();
    if (this.GiveOwnership)
      nullable = new char?(site.Code);
    ExtendedPublishOptions options = new ExtendedPublishOptions(PublishCompositionOptions.WithLinkedObjects | PublishCompositionOptions.IncludeFreeChangeAttributes, this.MaxCompositionLevel, this.EnableRelationTypes, this.EnableObjectTypes, (FiltrationSettings) null, site.Code.ToString() + customService.Info.Code.ToString(), this.AutoPublishReplication, nullable, nullable, this.RemoteTaskPriority, 0);
    CustomPublishDataInfo processInfo = new CustomPublishDataInfo(str1, site.Code, attachments, sl.CommaText, options, string.Empty);
    Packet4Publish packet = (Packet4Publish) null;
    if (flag)
    {
      string str2 = "WFPACKET " + DateTime.Now.ToString("ddMMyyyyHHmmffff");
      packet = new Packet4Publish(str2, str2, string.Empty);
    }
    service.CustomPublish(this.UserSession.SessionGUID, (IPublisher) RemoteProcessPublisher.Create((IUserSession) this.UserSession, processInfo, packet, createReceipt, this._process.ObjectID, this.ObjectID), str1, TaskPriority.Normal);
  }

  internal override void PrepareActivity()
  {
    if (this.Participants.Count == 0)
    {
      base.PrepareActivity();
      this.LaunchProcess();
    }
    else
    {
      if (this.NonUserActivitiesCounter > 0)
        --this.NonUserActivitiesCounter;
      ParticipantList participantList = new ParticipantList((IUserSession) this.UserSession);
      participantList.Assign(this.Participants);
      MiscFunx.ExpandParticipants((IDBAttributable) this, participantList);
      CheckParticipant(participantList);
      this.ParticipantID = participantList[0].ID;
      base.PrepareActivity();
      this._autoStep = false;
      if (!this.Flags.HasFlag((Enum) ActivityFlags.StartTermsWithWorkOffers))
        this.StartTerms();
      this.HandleTemporaryRights(true);
      WFActivity wfActivity = (WFActivity) this;
      for (int index = 1; index < participantList.Count; ++index)
      {
        RemoteProcess toAct = (RemoteProcess) wfActivity.Clone(this.VariableList);
        toAct.Participants.Clear();
        toAct.Participants.AddParticipant(participantList[index].Kind, participantList[index].ID);
        if (participantList[index].Kind == ParticipantKind.User)
          toAct.OwnerID = participantList[index].ID;
        this.SenderActivity?.ForwardDataFlow((WFActivity) toAct, nonUserActivitiesCounter: this.NonUserActivitiesCounter);
        if (toAct.ReadyToGo(this.SenderActivityID))
          toAct.Execute(true);
      }
    }

    static void CheckParticipant(ParticipantList parts)
    {
      if (parts.Count == 0 || parts[0].Kind != ParticipantKind.User)
        throw new WorkflowException(LocalizationHolder.rm.GetString(sc_22142.ssp_workflow_server_22144()) + parts.Count.ToString());
      if (parts.Count != 1 || parts[0].ID != wfConsts.SystemUserID)
        return;
      parts[0].ID = GlobalMailSettings.Cfg.WorkflowAdminUserID;
    }
  }

  public override WFActivity Clone(VarList senderVariablesList)
  {
    RemoteProcess remoteProcess = (RemoteProcess) base.Clone(senderVariablesList);
    remoteProcess.GetAttributeByID(wfConsts.AttrParticipantsID)?.Clear();
    remoteProcess.Participants.Assign(this.OriginalParticipants);
    remoteProcess.SaveParticipants();
    return (WFActivity) remoteProcess;
  }

  internal override bool ReadyToGo(long senderActivityID)
  {
    bool go = base.ReadyToGo(senderActivityID);
    if (go && this.Participants.Count > 0)
    {
      ParticipantList pl = new ParticipantList((IUserSession) this.UserSession);
      pl.Assign(this.Participants);
      MiscFunx.ExpandParticipants((IDBAttributable) this, pl);
      if (pl.Count > sc_22142.ssp_workflow_server_22145(2122513907))
      {
        for (int index = 0; index < pl.Count; ++index)
          this.CreateWorkOffer(pl[index].ID);
        this.Status = ActivityStatus.ParticipantWaiting;
        pl.XmlSection = "Expanded";
        this.AddParticipantsData = pl.AsString;
        this.SaveParticipants();
        if (this.Flags.HasFlag((Enum) ActivityFlags.StartTermsWithWorkOffers))
          this.StartTerms();
        return false;
      }
    }
    return go;
  }

  public override long ParticipantID
  {
    get
    {
      if (this.Participants.Count <= 0)
        return base.ParticipantID;
      if (this._participantID == -2L)
      {
        this._participantID = 0L;
        IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrRecipID);
        if (attributeById != null && attributeById.AsInteger != 0L)
          this._participantID = attributeById.AsInteger;
      }
      return this._participantID == 0L ? base.ParticipantID : this._participantID;
    }
    internal set
    {
      if (this.Participants.Count <= 0)
        return;
      this._participantID = value;
      this.Attributes.AddAttribute(wfConsts.AttrRecipID, false, new object[1]
      {
        (object) value
      });
    }
  }

  internal void SetActingUserID(long actingUserID)
  {
    if (actingUserID != 0L)
      this.Attributes.AddAttribute(wfConsts.AttrIOUserID, false, new object[1]
      {
        (object) actingUserID
      });
    this._actingUserIDSet = true;
  }

  internal override void NextStep(bool goNext)
  {
    if (goNext && this.Participants.Count != 0)
      this.LaunchProcess();
    if (!goNext && this.Flags.HasFlag((Enum) ActivityFlags.RequireAnswerText) && !this.Flags.HasFlag((Enum) ActivityFlags.Recalling) && string.IsNullOrEmpty(this.MessageText.Trim()))
      throw new NotificationException(LocalizationHolder.rm.GetString("AnswerRequiredErr"));
    if (!this._actingUserIDSet && this.UserSession.UserID != wfConsts.SystemUserID && this.UserSession.ActingUserID != 0L)
      this.Attributes.AddAttribute(wfConsts.AttrIOUserID, false, new object[1]
      {
        (object) this.UserSession.ActingUserID
      });
    base.NextStep(goNext);
  }

  protected void CreateWorkOffer(long userID)
  {
    IVariable variable = this.Variables.Find("SYS_SENDER");
    long fromUserID = 0;
    if (variable != null)
    {
      ParticipantList participantList = new ParticipantList((IUserSession) this.UserSession)
      {
        AsString = variable.Value
      };
      if (participantList.Count > 0)
        fromUserID = participantList[0].ID;
    }
    ServerFunx.CopyAttachmentsFlag((IDBObject) this, ServerFunx.CreateWorkOffer((IUserSession) this.UserSession, wfConsts.WorkOfferTypeID, userID, string.Format(LocalizationHolder.rm.GetString(sc_22142.ssp_workflow_server_22146()), (object) this.Caption, (object) this.ProcessID, (object) this.ProcessName), (WFActivity) this, this.Priority, fromUserID));
  }

  public void AcceptWorkOffer()
  {
    ((WFProcess) this.Process).AcquireBlock();
    try
    {
      this.DeleteMessages(wfConsts.WorkOfferTypeID);
      this._parts = new ParticipantList((IUserSession) this.UserSession);
      this._parts.AddParticipant(ParticipantKind.User, this.UserSession.UserID);
      if (this.ReadyToGo(this.SenderActivityID))
      {
        if (this.Execute(true))
        {
          if (this.NextStepLinks.Count > 0)
            new WFActivityProxy(this.ProcessID, (WFActivity) this, processExecutedCompetedHandler: (WFActivityProxy.ProcessExecutedHandler) ((processID, userID) =>
            {
              IUserSession sessionTemporaryClone = ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) is IDBTimedEvents service2 ? service2.GetSystemSessionTemporaryClone("RemoteProcess.AcceptWorkOffer.ReleaseBlock") : (IUserSession) null;
              try
              {
                IDBObject dbObject = sessionTemporaryClone?.GetObject(processID, false);
                if (dbObject == null || !(dbObject is WFProcess wfProcess2))
                  return;
                wfProcess2.ReleaseBlock(userID);
              }
              finally
              {
                sessionTemporaryClone?.Logout("RemoteProcess.AcceptWorkOffer.ReleaseBlock");
              }
            })).ExecuteNextAsync(this.UserSession.UserID, this.ObjectID, this.NextStepLinks, this.VariableList);
          else
            new WFActivityProxy(this.ProcessID, (WFActivity) this, processExecutedCompetedHandler: (WFActivityProxy.ProcessExecutedHandler) ((processID, userID) =>
            {
              IUserSession sessionTemporaryClone = ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) is IDBTimedEvents service4 ? service4.GetSystemSessionTemporaryClone("RemoteProcess.AcceptWorkOffer.ReleaseBlock") : (IUserSession) null;
              try
              {
                IDBObject dbObject = sessionTemporaryClone?.GetObject(processID, false);
                if (dbObject == null || !(dbObject is WFProcess wfProcess4))
                  return;
                wfProcess4.ReleaseBlock(userID);
              }
              finally
              {
                sessionTemporaryClone?.Logout("RemoteProcess.AcceptWorkOffer.ReleaseBlock");
              }
            })).NextStepAsync(true);
        }
        else
          ((WFProcess) this.Process).ReleaseBlock();
      }
      else
        ((WFProcess) this.Process).ReleaseBlock();
    }
    catch
    {
      ((WFProcess) this.Process).ReleaseBlock();
      throw;
    }
  }

  public void RejectWorkOffer()
  {
    this.DeleteMessages(wfConsts.WorkOfferTypeID, new ConditionStructure[1]
    {
      new ConditionStructure(wfConsts.AttrRecipID, RelationalOperators.Equal, (object) this.UserSession.UserID, (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID)
    });
    DataTable dataTable = MiscFunx.SimpleSelect((IUserSession) this.UserSession, wfConsts.WorkOfferTypeID, wfConsts.AttrActivityID, RelationalOperators.Equal, (object) this.ObjectID);
    if (dataTable.Rows.Count == 1 && this.ExtProps.Ini.ReadBoolean("Props", "SendWorkOfferLastParticipant", false))
    {
      long int64 = Convert.ToInt64(dataTable.Rows[0].ItemArray[0]);
      IDBObject dbObject = this.UserSession.GetObject(int64, false);
      if (dbObject != null)
      {
        IDBAttribute attributeById = dbObject.GetAttributeByID(wfConsts.AttrRecipID);
        if (attributeById != null)
        {
          this._parts = new ParticipantList((IUserSession) this.UserSession);
          this._parts.AddParticipant(ParticipantKind.User, attributeById.AsInteger);
          if (this.ReadyToGo(this.SenderActivityID) && this.Execute(true))
          {
            if (this.NextStepLinks.Count > 0)
              new WFActivityProxy(this.ProcessID, (WFActivity) this).ExecuteNextAsync(this.UserSession.UserID, this.ObjectID, this.NextStepLinks, this.VariableList);
            else
              new WFActivityProxy(this.ProcessID, (WFActivity) this).NextStepAsync(true);
          }
          dbObject.Delete(0L);
        }
        else
          this.UserSession.EventLog.AddToTrace($"Не найден исполнитель почтового предложения '{dbObject.Caption}' [{int64}]. Выполнение процесса '{this.Process.Caption}' [{this.ProcessID}] будет продолжено, однако последний исполнитель сможет отказаться от выполнения.", 0, "workflow_workOfferException.log");
      }
      else
        this.UserSession.EventLog.AddToTrace($"Не найдено почтовое предложение '{int64}'. Выполнение процесса '{this.Process.Caption}' [{this.ProcessID}] будет продолжено, однако последний исполнитель сможет отказаться от выполнения.", 0, "workflow_workOfferException.log");
    }
    else
    {
      if (dataTable.Rows.Count != 0)
        return;
      this.MessageText = LocalizationHolder.rm.GetString(sc_22142.ssp_workflow_server_22147());
      bool flag = false;
      this.ActivityResult = ActivityResult.Back;
      if (this.RollbackKind == RollbackKind.Disabled)
      {
        this._rollbackKind = RollbackKind.Start;
        flag = true;
      }
      try
      {
        new WFActivityProxy(this.ProcessID, (WFActivity) this).NextStepAsync(false);
      }
      finally
      {
        if (flag)
          this._rollbackKind = RollbackKind.Disabled;
      }
    }
  }

  protected override void AfterSent()
  {
    if (!this.ExtProps.Ini.ReadBoolean("Props", "sendParticipantsEmail", true))
      return;
    EmailSender.Send(this.Session, this.ParticipantID, string.Empty, string.Empty, (IDBObject) this);
  }

  internal override void SetStatus(
    IDBAttribute attr,
    ActivityStatus value,
    ActivityStatus oldStatus)
  {
    base.SetStatus(attr, value, oldStatus);
    if (!wfConsts.CompletedStatuses.Contains(value))
      return;
    this.HandleTemporaryRights(false);
  }

  public override void SetDeletionStatus(MailFolder folder, DeletionStatus status)
  {
    base.SetDeletionStatus(folder, status);
    if (folder != MailFolder.Deleted || status != DeletionStatus.Deleted)
      return;
    this.HandleTemporaryRights(false);
  }
}
