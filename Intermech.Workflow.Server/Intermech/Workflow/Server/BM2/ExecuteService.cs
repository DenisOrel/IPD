// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.BM2.ExecuteService
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Workflow.BM2;

#nullable disable
namespace Intermech.Workflow.Server.BM2;

internal class ExecuteService : LongLifeObject, IExecuteService
{
  public static void Register()
  {
    if (!(ApplicationServices.Container.GetService(typeof (ICustomServices)) is ICustomServices service))
      return;
    service.AddService(typeof (IExecuteService), (object) new ExecuteService());
  }

  public void Execute(long processID, long activityID, long userID)
  {
    new WFActivityProxy(processID, activityID, userID).ExecuteCurrentActivity();
  }

  public void ExecuteCustomSender(
    long processID,
    long activityID,
    long senderActivityID,
    long userID)
  {
    new WFActivityProxy(processID, senderActivityID, userID).ExecuteCustomSender(activityID);
  }
}
