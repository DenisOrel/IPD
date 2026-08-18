// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.WorkflowPortalDelayStarter
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System;

#nullable disable
namespace Intermech.Workflow.Server;

internal class WorkflowPortalDelayStarter : DBTimedService
{
  public static void Register()
  {
    if (!(ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents is DBTimedEvents service))
      return;
    service.RegisterService((object) new WorkflowPortalDelayStarter());
  }

  public override Guid GUID => wfConsts.WorkflowPortalDelayStarterGuid;

  public override string ServiceName
  {
    get => "Служба обработки завершения процессов требующих отправку на портал";
  }

  public override bool ProcessEvent(TimedEventProperties properties)
  {
    switch ((EventKind) properties.IntInfo)
    {
      case EventKind.PortalActivity:
        return this.ExecuteActivity(properties);
      case EventKind.PortalProcess:
        return this.ExecuteProcess(properties);
      default:
        return true;
    }
  }

  private bool ExecuteActivity(TimedEventProperties properties)
  {
    IUserSession sessionTemporaryClone = this.TimedEventService.GetSystemSessionTemporaryClone("WFS.WorkflowPortalDelayStarter.ExecuteActivity.TemporaryClone");
    if (sessionTemporaryClone == null)
      return false;
    try
    {
      if (!this.CheckPortalServer(sessionTemporaryClone) || !(sessionTemporaryClone.GetObject(properties.ObjectID, false) is WFActivity sender))
        return false;
      string str = new ExtProperties((IDBObject) sender.Process, wfConsts.AttrAddInfoID).Read("PortalInfo");
      if (!string.IsNullOrEmpty(str))
      {
        StringList sl = new StringList();
        sl.CommaText = str;
        bool result = false;
        bool.TryParse(properties.StringInfo, out result);
        WorkflowPortalHandler.ContinueExecutionAtSender(sl, result, sender);
      }
    }
    finally
    {
      sessionTemporaryClone.Logout("WFS.WorkflowPortalDelayStarter.ExecuteActivity.TemporaryClone");
    }
    return true;
  }

  private bool ExecuteProcess(TimedEventProperties properties)
  {
    IUserSession sessionTemporaryClone = this.TimedEventService.GetSystemSessionTemporaryClone("WFS.WorkflowPortalDelayStarter.ExecuteProcess.TemporaryClone");
    if (sessionTemporaryClone != null)
    {
      try
      {
        if (!this.CheckPortalServer(sessionTemporaryClone))
          return false;
        if (sessionTemporaryClone.GetObject(properties.ObjectID, false) is WFProcess sender)
        {
          string str = new ExtProperties((IDBObject) sender, wfConsts.AttrAddInfoID).Read("PortalInfo");
          if (!string.IsNullOrEmpty(str))
          {
            StringList sl = new StringList();
            sl.CommaText = str;
            bool result = false;
            bool.TryParse(properties.StringInfo, out result);
            WorkflowPortalHandler.ContinueExecutionAtSender(sl, result, sender);
          }
        }
      }
      finally
      {
        sessionTemporaryClone.Logout("WFS.WorkflowPortalDelayStarter.ExecuteProcess.TemporaryClone");
      }
    }
    return true;
  }

  private bool CheckPortalServer(IUserSession systemSession)
  {
    string str = systemSession.Configurations.ReadString("KERNEL", "PortalProps", "PortalServerName", string.Empty, DBConfigMode.GlobalOnly);
    return !(ApplicationServices.Container.GetService(typeof (IAppServers)) is IAppServers service) || !(str != service.ServerName);
  }
}
