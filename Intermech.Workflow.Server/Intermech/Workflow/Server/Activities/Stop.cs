// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.Activities.Stop
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel;
using System.Data;

#nullable disable
namespace Intermech.Workflow.Server.Activities;

public class Stop : SystemActivity
{
  public Stop(UserSession uSession, DataTable objectsTable)
    : base(uSession, objectsTable)
  {
    this._rollbackKind = RollbackKind.Disabled;
  }

  public override ActivityKind Kind => ActivityKind.Stop;

  public override bool Collector => true;

  internal override bool ReadyToGo(long senderActivityID)
  {
    this.ReadyToGoCalled = true;
    if (senderActivityID != -1L)
      this.SenderActivityID = senderActivityID;
    return new LinkWalker((WFProcess) this.Process, (IUserSession) this.UserSession).IsAllCompleted(this.ObjectID);
  }

  internal override void AfterExecute()
  {
    base.AfterExecute();
    if (this.ErrorOccured)
      return;
    ((WFProcess) this.Process).StopProcess((IActivity) this);
    this.ContinueExecAtParentProcess(true);
  }
}
