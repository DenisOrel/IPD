// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.Activities.Script
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Workflow.Server.Activities;

public class Script(UserSession uSession, DataTable objectsTable) : SystemActivity(uSession, objectsTable)
{
  private string _scriptCode = "@#$";
  private ScriptExecSide _execSide;
  private bool _actingUserIDSet;

  public override ActivityKind Kind => ActivityKind.Script;

  public string ScriptCode
  {
    get
    {
      this.Load();
      return this._scriptCode;
    }
  }

  private void Load()
  {
    if (!(this._scriptCode == sc_22148.ssp_workflow_server_22149()))
      return;
    this._scriptCode = MiscFunx.GetScriptCode((IUserSession) this.UserSession, this.ObjectID, ScriptKind.BeforeExec, ScriptExecSide.Server, ref this._lastScriptID);
    this._execSide = ScriptExecSide.Server;
    if (this._scriptCode != null || MiscFunx.GetScriptID((IUserSession) this.UserSession, this.ObjectID, ScriptKind.BeforeExec, ScriptExecSide.Client) == 0L)
      return;
    this._execSide = ScriptExecSide.Client;
  }

  public ScriptExecSide ExecSide
  {
    get
    {
      this.Load();
      return this._execSide;
    }
  }

  internal override void PrepareActivity()
  {
    if (this.ExecSide == ScriptExecSide.Server)
    {
      this._autoStep = true;
      base.PrepareActivity();
      this.ExecScript(this.ScriptCode);
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
        Script toAct = (Script) wfActivity.Clone(this.VariableList);
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
        throw new WorkflowException(LocalizationHolder.rm.GetString(sc_22148.ssp_workflow_server_22150()) + parts.Count.ToString());
      if (parts.Count != 1 || parts[0].ID != wfConsts.SystemUserID)
        return;
      parts[0].ID = GlobalMailSettings.Cfg.WorkflowAdminUserID;
    }
  }

  public override WFActivity Clone(VarList senderVariablesList)
  {
    Script script = (Script) base.Clone(senderVariablesList);
    script.GetAttributeByID(wfConsts.AttrParticipantsID)?.Clear();
    script.Participants.Assign(this.OriginalParticipants);
    script.SaveParticipants();
    return (WFActivity) script;
  }

  public override long ParticipantID
  {
    get
    {
      if (this.ExecSide != ScriptExecSide.Client)
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
      if (this.ExecSide != ScriptExecSide.Client)
        return;
      this._participantID = value;
      this.Attributes.AddAttribute(wfConsts.AttrRecipID, false, new object[1]
      {
        (object) value
      });
    }
  }

  protected override void ExecScript(ScriptKind kind)
  {
  }

  public override string Validate(bool checkSubProcessSchemes = true, List<long> checkedSchemesList = null)
  {
    string s = base.Validate(checkSubProcessSchemes, checkedSchemesList);
    this.ValidateParticipants(ref s);
    return s;
  }

  public override void ValidateParticipants(ref string s)
  {
    if (this.ExecSide != ScriptExecSide.Client)
      return;
    if (this.Participants.Count == 0)
    {
      MiscFunx.AddNewLined(ref s, string.Format(LocalizationHolder.rm.GetString(sc_22148.ssp_workflow_server_22151()), (object) this.Name));
    }
    else
    {
      if (!this.Participants.Invalid)
        return;
      MiscFunx.AddNewLined(ref s, string.Format(LocalizationHolder.rm.GetString(sc_22148.ssp_workflow_server_22152()), (object) this.Name));
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
    if (!goNext && this.Flags.HasFlag((Enum) ActivityFlags.RequireAnswerText) && !this.Flags.HasFlag((Enum) ActivityFlags.Recalling) && string.IsNullOrEmpty(this.MessageText.Trim()))
      throw new NotificationException(LocalizationHolder.rm.GetString("AnswerRequiredErr"));
    if (!this._actingUserIDSet && this.UserSession.UserID != wfConsts.SystemUserID && this.UserSession.ActingUserID != 0L)
      this.Attributes.AddAttribute(wfConsts.AttrIOUserID, false, new object[1]
      {
        (object) this.UserSession.ActingUserID
      });
    base.NextStep(goNext);
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
