// Decompiled with JetBrains decompiler
// Type: Intermech.MRP.Server.IMRPCompositionTask
// Assembly: Intermech.MRP.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 90CF20BA-CEDA-4320-95C8-661A6AE661C2
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MRP.Server.dll

using Intermech.Interfaces.MRP;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.MRP.Server;

internal interface IMRPCompositionTask : IMRPContext
{
  MRPCompositionTaskState State { get; set; }

  Exception Exception { get; set; }

  Guid TaskID { get; }

  Guid ActionsID { get; set; }

  LinkedList<IMRPAction> Actions { get; }

  IMRPCompositionTask MasterTask { get; set; }

  void Execute(
    Guid sessionGuid,
    IServiceProvider services,
    MRPTaskCompleteEventHandler completeHandler,
    MRPTaskCancelEventHandler cancelHandler);
}
