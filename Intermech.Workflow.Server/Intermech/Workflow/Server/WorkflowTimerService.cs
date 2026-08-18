// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.WorkflowTimerService
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Workflow.Server.Activities;
using System;

#nullable disable
namespace Intermech.Workflow.Server;

public class WorkflowTimerService : DBCustomManualScheduledService
{
  public override bool Visible => false;

  public override bool IsMultiThread => false;

  public static void Register()
  {
    if (!(ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) is DBTimedEvents service))
      return;
    service.RegisterService((object) new WorkflowTimerService());
  }

  public override Guid GUID => wfConsts.WorkflowTimerServiceGuid;

  public override bool ProcessEvent(TimedEventProperties properties)
  {
    switch (properties.IntInfo)
    {
      case 0:
        return this.CreateMessage(properties);
      case 1:
        return this.ExecuteActivity(properties, true);
      case 2:
      case 3:
        return this.ExecuteActivity(properties, false);
      default:
        return true;
    }
  }

  public override string ServiceName
  {
    get => "Служба обработки таймера и периодических событий документооборота";
  }

  private bool CreateMessage(TimedEventProperties p)
  {
    IUserSession userSession = this.Session.Clone("WFS.WorkflowTimerService.CreateMessage");
    if (userSession == null)
      return false;
    try
    {
      switch (userSession.GetObject(p.ObjectID))
      {
        case WFActivity wfActivity:
          wfActivity.SendPeriodMessage();
          break;
        case WFProcess wfProcess:
          wfProcess.SendPeriodMessage();
          break;
      }
    }
    finally
    {
      userSession.Logout("WFS.WorkflowTimerService.CreateMessage");
    }
    return true;
  }

  private bool ExecuteActivity(TimedEventProperties p, bool goForward)
  {
    IUserSession userSession = this.Session.Clone("WFS.WorkflowTimerService.ExecuteActivity");
    if (userSession == null)
      return false;
    try
    {
      userSession.DBObjectsCacheStart();
      try
      {
        if (userSession.GetObject(p.ObjectID) is WFActivity originalSenderActivity)
        {
          ((WFProcess) originalSenderActivity.Process).AcquireBlock();
          try
          {
            if (originalSenderActivity is UserActivity userActivity)
            {
              EventKind intInfo = (EventKind) p.IntInfo;
              userActivity.AddAutoRollbackMessage(intInfo);
              if (intInfo == EventKind.UncompleteTerm || intInfo == EventKind.UnreadTerm)
                goForward = false;
            }
            int result = 0;
            int.TryParse(p.StringInfo, out result);
            new WFActivityProxy(originalSenderActivity.ProcessID, originalSenderActivity, result, (WFActivityProxy.ProcessExecutedHandler) ((processID, userID) =>
            {
              IUserSession sessionTemporaryClone = (ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone("WFS.WorkflowTimerService.ExecuteActivity.ReleaseBlock");
              try
              {
                IDBObject dbObject = sessionTemporaryClone.GetObject(processID, false);
                if (dbObject == null || !(dbObject is WFProcess wfProcess2))
                  return;
                wfProcess2.ReleaseBlock(userID);
              }
              finally
              {
                sessionTemporaryClone.Logout("WFS.WorkflowTimerService.ExecuteActivity.ReleaseBlock");
              }
            })).NextStepAsync(goForward);
          }
          catch (Exception ex)
          {
            ((WFProcess) originalSenderActivity.Process).ReleaseBlock();
            originalSenderActivity.DumpException(ex);
            originalSenderActivity.NextStep(false);
            throw;
          }
        }
      }
      finally
      {
        userSession.DBObjectsCacheStop();
      }
    }
    finally
    {
      userSession.Logout("WFS.WorkflowTimerService.ExecuteActivity");
    }
    return true;
  }
}
