// Decompiled with JetBrains decompiler
// Type: Intermech.MRP.Server.Compositions.Tasks.MRPCompositionCreatorTask
// Assembly: Intermech.MRP.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 90CF20BA-CEDA-4320-95C8-661A6AE661C2
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MRP.Server.dll

using System;

#nullable disable
namespace Intermech.MRP.Server.Compositions.Tasks;

internal class MRPCompositionCreatorTask(
  string taskName,
  IServiceProvider services,
  IMRPCompositionTask masterTask) : MRPCompositionBaseTask(taskName, services, masterTask)
{
  public override void Execute(
    Guid sessionGuid,
    IServiceProvider services,
    MRPTaskCompleteEventHandler completeHandler,
    MRPTaskCancelEventHandler cancelHandler)
  {
  }
}
