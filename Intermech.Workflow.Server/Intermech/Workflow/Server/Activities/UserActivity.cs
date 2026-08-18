// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.Activities.UserActivity
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Workflow.Server.Activities;

public class UserActivity : WFActivity, IUserActivity
{
  internal bool SkipParticipantsExec;
  internal bool AllowSystemParticipant;
  private ParticipantList _originalParticipants;
  private int _savedNonUserActivitiesCounter;
  private bool _actingUserIDSet;

  public UserActivity(UserSession uSession, DataTable objectsTable)
    : base(uSession, objectsTable)
  {
    this._autoStep = false;
  }

  public override long ParticipantID
  {
    get
    {
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
      this._participantID = value;
      this.Attributes.AddAttribute(wfConsts.AttrRecipID, false, new object[1]
      {
        (object) value
      });
    }
  }

  protected void CheckParticipant(ParticipantList parts)
  {
    if (parts.Count == 0 || parts[0].Kind != ParticipantKind.User)
      throw new WorkflowException(LocalizationHolder.rm.GetString(sc_22160.ssp_workflow_server_22161()) + parts.Count.ToString());
    if (this.AllowSystemParticipant || parts.Count != 1 || parts[0].ID != wfConsts.SystemUserID)
      return;
    parts[0].ID = GlobalMailSettings.Cfg.WorkflowAdminUserID;
  }

  public virtual void ValidateParticipants(ref string s)
  {
    if (this.Participants.Count == 0)
    {
      MiscFunx.AddNewLined(ref s, string.Format(LocalizationHolder.rm.GetString(sc_22160.ssp_workflow_server_22162()), (object) this.Name));
    }
    else
    {
      if (!this.Participants.Invalid)
        return;
      MiscFunx.AddNewLined(ref s, string.Format(LocalizationHolder.rm.GetString(sc_22160.ssp_workflow_server_22163()), (object) this.Name));
    }
  }

  public override WFActivity Clone(VarList senderVariablesList)
  {
    UserActivity userActivity = (UserActivity) base.Clone(senderVariablesList);
    userActivity.GetAttributeByID(wfConsts.AttrParticipantsID)?.Clear();
    userActivity.Participants.Assign(this.OriginalParticipants);
    userActivity.SaveParticipants();
    return (WFActivity) userActivity;
  }

  protected void SaveActingUserID()
  {
    if (this.UserSession.ActingUserID == 0L)
      return;
    this.Attributes.AddAttribute(wfConsts.AttrIOUserID, false, new object[1]
    {
      (object) this.UserSession.ActingUserID
    });
  }

  protected void SaveActingUserID(long actingUserID)
  {
    if (actingUserID == 0L)
      return;
    this.Attributes.AddAttribute(wfConsts.AttrIOUserID, false, new object[1]
    {
      (object) actingUserID
    });
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
    ServerFunx.CopyAttachmentsFlag((IDBObject) this, ServerFunx.CreateWorkOffer((IUserSession) this.UserSession, wfConsts.WorkOfferTypeID, userID, string.Format(LocalizationHolder.rm.GetString(sc_22160.ssp_workflow_server_22164()), (object) this.Caption, (object) this.ProcessID, (object) this.ProcessName), (WFActivity) this, this.Priority, fromUserID));
  }

  internal override bool ReadyToGo(long senderActivityID)
  {
    bool go = base.ReadyToGo(senderActivityID);
    if (go && !(this is Start))
    {
      ParticipantList participantList = new ParticipantList((IUserSession) this.UserSession);
      participantList.Assign(this.Participants);
      MiscFunx.ExpandParticipants((IDBAttributable) this, participantList);
      if (!participantList.EveryOne)
      {
        if (participantList.Count > sc_22160.ssp_workflow_server_22165(2099508014) && this.NeedToSendWorkOffer(participantList))
        {
          for (int index = 0; index < participantList.Count; ++index)
            this.CreateWorkOffer(participantList[index].ID);
          this.Status = ActivityStatus.ParticipantWaiting;
          participantList.XmlSection = "Expanded";
          this.AddParticipantsData = participantList.AsString;
          this.SaveParticipants();
          if (this.Flags.HasFlag((Enum) ActivityFlags.StartTermsWithWorkOffers))
            this.StartTerms();
          return false;
        }
        participantList.EveryOne = true;
      }
      if (participantList.EveryOne && this.Flags.HasFlag((Enum) ActivityFlags.StartTermsWithWorkOffers) && !this.Flags.HasFlag((Enum) ActivityFlags.StartTermsAcceptWorkOffer))
        this.Flags &= ~ActivityFlags.StartTermsWithWorkOffers;
      if (this._originalParticipants == null)
        this._originalParticipants = new ParticipantList((IUserSession) this.UserSession);
      this._originalParticipants.Assign(this.Participants);
      this.Participants.Assign(participantList);
    }
    return go;
  }

  protected virtual bool NeedToSendWorkOffer(ParticipantList Participants) => true;

  public override bool CanEditAttributes()
  {
    if (base.CanEditAttributes())
      return true;
    return this.Status == ActivityStatus.Executed && this.ParticipantID == this.UserSession.UserID;
  }

  protected override void AfterSent()
  {
    if (!this.ExtProps.Ini.ReadBoolean("Props", "sendParticipantsEmail", true))
      return;
    EmailSender.Send(this.Session, this.ParticipantID, string.Empty, string.Empty, (IDBObject) this);
  }

  internal override void PrepareActivity()
  {
    this._savedNonUserActivitiesCounter = this.NonUserActivitiesCounter;
    if (this.SkipParticipantsExec)
    {
      this._savedNonUserActivitiesCounter = -1;
    }
    else
    {
      if (this.NonUserActivitiesCounter > 0)
        --this.NonUserActivitiesCounter;
      ParticipantList participantList = new ParticipantList((IUserSession) this.UserSession);
      participantList.Assign(this.Participants);
      MiscFunx.ExpandParticipants((IDBAttributable) this, participantList);
      this.CheckParticipant(participantList);
      this.ParticipantID = participantList[0].ID;
      base.PrepareActivity();
      if (!this.Flags.HasFlag((Enum) ActivityFlags.StartTermsWithWorkOffers))
        this.StartTerms();
      this.HandleTemporaryRights(true);
      WFActivity wfActivity = (WFActivity) this;
      for (int index = 1; index < participantList.Count; ++index)
      {
        UserActivity toAct = (UserActivity) wfActivity.Clone(this.VariableList);
        toAct.Participants.Clear();
        toAct.Participants.AddParticipant(participantList[index].Kind, participantList[index].ID);
        if (participantList[index].Kind == ParticipantKind.User)
          toAct.OwnerID = participantList[index].ID;
        this.SenderActivity?.ForwardDataFlow((WFActivity) toAct, nonUserActivitiesCounter: this.NonUserActivitiesCounter);
        if (toAct.ReadyToGo(this.SenderActivityID))
          toAct.Execute(true);
      }
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
          this.Flags |= ActivityFlags.StartTermsAcceptWorkOffer;
          if (this.ReadyToGo(this.SenderActivityID))
          {
            this.Flags &= ~ActivityFlags.StartTermsAcceptWorkOffer;
            if (this.Execute(true))
            {
              if (this.NextStepLinks.Count > 0)
                new WFActivityProxy(this.ProcessID, (WFActivity) this).ExecuteNextAsync(this.UserSession.UserID, this.ObjectID, this.NextStepLinks, this.VariableList);
              else
                new WFActivityProxy(this.ProcessID, (WFActivity) this).NextStepAsync(true);
            }
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
      this.MessageText = LocalizationHolder.rm.GetString(sc_22160.ssp_workflow_server_22166());
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

  public void AcceptWorkOffer()
  {
    ((WFProcess) this.Process).AcquireBlock();
    try
    {
      this.DeleteMessages(wfConsts.WorkOfferTypeID);
      this._parts = new ParticipantList((IUserSession) this.UserSession);
      this._parts.AddParticipant(ParticipantKind.User, this.UserSession.UserID);
      this.Flags |= ActivityFlags.StartTermsAcceptWorkOffer;
      if (this.ReadyToGo(this.SenderActivityID))
      {
        this.Flags &= ~ActivityFlags.StartTermsAcceptWorkOffer;
        if (this.Execute(true))
        {
          if (this.NextStepLinks.Count > 0)
            new WFActivityProxy(this.ProcessID, (WFActivity) this, processExecutedCompetedHandler: (WFActivityProxy.ProcessExecutedHandler) ((processID, userID) =>
            {
              IUserSession sessionTemporaryClone = ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) is IDBTimedEvents service2 ? service2.GetSystemSessionTemporaryClone("UserActivity.AcceptWorkOffer.ReleaseBlock") : (IUserSession) null;
              try
              {
                IDBObject dbObject = sessionTemporaryClone?.GetObject(processID, false);
                if (dbObject == null || !(dbObject is WFProcess wfProcess2))
                  return;
                wfProcess2.ReleaseBlock(userID);
              }
              finally
              {
                sessionTemporaryClone?.Logout("UserActivity.AcceptWorkOffer.ReleaseBlock");
              }
            })).ExecuteNextAsync(this.UserSession.UserID, this.ObjectID, this.NextStepLinks, this.VariableList);
          else
            new WFActivityProxy(this.ProcessID, (WFActivity) this, processExecutedCompetedHandler: (WFActivityProxy.ProcessExecutedHandler) ((processID, userID) =>
            {
              IUserSession sessionTemporaryClone = ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) is IDBTimedEvents service4 ? service4.GetSystemSessionTemporaryClone("UserActivity.AcceptWorkOffer.ReleaseBlock") : (IUserSession) null;
              try
              {
                IDBObject dbObject = sessionTemporaryClone?.GetObject(processID, false);
                if (dbObject == null || !(dbObject is WFProcess wfProcess4))
                  return;
                wfProcess4.ReleaseBlock(userID);
              }
              finally
              {
                sessionTemporaryClone?.Logout("UserActivity.AcceptWorkOffer.ReleaseBlock");
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

  public override string Validate(bool checkSubProcessSchemes = true, List<long> checkedSchemesList = null)
  {
    string s = base.Validate(checkSubProcessSchemes, checkedSchemesList);
    this.ValidateParticipants(ref s);
    if (this.Terms.Invalid)
      MiscFunx.AddNewLined(ref s, string.Format(LocalizationHolder.rm.GetString(sc_22160.ssp_workflow_server_22167()), (object) this.Name));
    return s;
  }

  protected void CheckParticipant() => this.CheckParticipant(this.Participants);

  protected override void DoDelete() => base.DoDelete();

  public override void Changed(ActivityChanged flag, object tag)
  {
    if ((flag & ActivityChanged.UnreadStatus) != (ActivityChanged) 0 && (RecipStatus) tag == RecipStatus.Read)
    {
      if (this.Terms.ReadTerm.Period != null)
        this.UnregisterTermNotification(this.Terms.ReadTerm, EventKind.UnreadTerm);
      this.SendNotification(this.Notifications.ReadNotify);
    }
    base.Changed(flag, tag);
  }

  internal override void NextStep(bool goNext)
  {
    if (!goNext && this.Flags.HasFlag((Enum) ActivityFlags.RequireAnswerText) && !this.Flags.HasFlag((Enum) ActivityFlags.Recalling) && string.IsNullOrEmpty(this.MessageText.Trim()))
      throw new NotificationException(LocalizationHolder.rm.GetString("AnswerRequiredErr"));
    if (!this._actingUserIDSet && this.UserSession.UserID != wfConsts.SystemUserID)
      this.SaveActingUserID();
    base.NextStep(goNext);
  }

  internal void SetActingUserID(long actingUserID)
  {
    this.SaveActingUserID(actingUserID);
    this._actingUserIDSet = true;
  }

  internal override void AfterExecute()
  {
    if ((this.ErrorOccured || this._autoStep) && this._savedNonUserActivitiesCounter != -1)
      this.NonUserActivitiesCounter = this._savedNonUserActivitiesCounter;
    this.UnregisterTermNotifications();
    base.AfterExecute();
  }

  public override bool Abort()
  {
    if (this.Executed)
      this.UnregisterTermNotifications();
    return base.Abort();
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

  protected override long SenderParticipantID
  {
    get
    {
      long participantId = this.ParticipantID;
      return participantId != wfConsts.SystemUserID ? participantId : base.SenderParticipantID;
    }
  }
}
