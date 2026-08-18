// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.Activities.Start
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces.Workflow;
using Intermech.Kernel;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Workflow.Server.Activities;

public class Start : UserActivity
{
  internal bool StartSubProcessAutoStep;
  internal bool FirstLaunch;

  public Start(UserSession uSession, DataTable objectsTable)
    : base(uSession, objectsTable)
  {
    this._rollbackKind = RollbackKind.Disabled;
  }

  public override ActivityKind Kind => ActivityKind.Start;

  internal override bool GetAutoStep()
  {
    return this.SenderActivity == null || this.SenderActivity.Kind == ActivityKind.Process || this.GoBackViaPortal || this.StartSubProcessAutoStep;
  }

  protected bool GoBackViaPortal
  {
    get => this.SkipParticipantsExec;
    set
    {
      this.SkipParticipantsExec = value;
      if (value)
        this.ActivityResult = ActivityResult.Back;
      this.AllowSystemParticipant = value;
    }
  }

  internal override void AfterExecute()
  {
    base.AfterExecute();
    if (!this.GoBackViaPortal)
      return;
    this.ContinueExecAtParentProcess(false);
    ((WFProcess) this.Process).StopProcess((IActivity) this);
  }

  public override bool Execute(bool goNext)
  {
    ParticipantList participants = this.Participants;
    participants.Clear();
    long ownerId = this.Process.OwnerID;
    participants.AddParticipant(ParticipantKind.User, ownerId);
    this.SaveParticipants();
    if (this.SenderID == 0L)
      this.SenderID = ownerId;
    this.ParticipantID = ownerId;
    this.SaveActingUserID();
    return base.Execute(goNext);
  }

  public List<WFLink> ExecuteStart(long senderActivityID)
  {
    this._autoStep = true;
    if (this.ReadyToGo(senderActivityID))
      this.Execute(true);
    return this.NextStepLinks;
  }

  public override bool CanEditAttributes()
  {
    return this._process != null && this._process.Kind == ActivityKind.Process && this.Status == ActivityStatus.OnApproach || base.CanEditAttributes();
  }

  internal override void PrepareActivity()
  {
    if (this.SenderActivity != null && this.SenderActivity.ActivityResult == ActivityResult.Back && this.Participants.Count == 1 && this.Participants[0].Kind == ParticipantKind.User && this.Participants[0].ID == this.PortalReplicatorUserID && this.PortalInfo != null && this.PortalInfo.Values["Wait"] == "1")
      this.GoBackViaPortal = true;
    base.PrepareActivity();
  }

  public override void ValidateParticipants(ref string s)
  {
  }

  internal override void PrepareNextStepLinks()
  {
    if (this.GoBackViaPortal)
      return;
    base.PrepareNextStepLinks();
  }

  internal override void NextStep(bool goNext)
  {
    if (!goNext)
      return;
    base.NextStep(goNext);
  }
}
