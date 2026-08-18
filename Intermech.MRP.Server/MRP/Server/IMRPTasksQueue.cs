// Decompiled with JetBrains decompiler
// Type: Intermech.MRP.Server.IMRPTasksQueue
// Assembly: Intermech.MRP.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 90CF20BA-CEDA-4320-95C8-661A6AE661C2
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MRP.Server.dll

using Intermech.Interfaces.MRP;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.MRP.Server;

internal interface IMRPTasksQueue : IMRPContext, IMRPProgress
{
  MRPTasksQueueState State { get; }

  bool IsDisposed { get; }

  bool AutoComplete { get; set; }

  int InQueue { get; }

  int InProcess { get; }

  int ProcessedTasks { get; }

  int CancelledTasks { get; }

  int SkippedTasks { get; }

  int TotalTasks { get; }

  int NestedTasks { get; }

  bool IsBreaked { get; set; }

  Guid QueueGuid { get; }

  Guid SessionGuid { get; }

  string TaskOperation { get; set; }

  Exception Exception { get; set; }

  void EnqueueTask(IMRPCompositionTask task);

  void Execute();

  bool HasException(Guid actionsID);

  Dictionary<Guid, MRPIntermediateTaskResult> GetResults();
}
