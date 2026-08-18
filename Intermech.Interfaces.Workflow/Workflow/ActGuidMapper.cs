// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.ActGuidMapper
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Workflow;

public class ActGuidMapper
{
  private static Dictionary<Guid, ActivityKind> _guids = new Dictionary<Guid, ActivityKind>();

  public static ActivityKind GuidToKind(Guid g)
  {
    ActivityKind kind = ActivityKind.None;
    ActGuidMapper._guids.TryGetValue(g, out kind);
    return kind;
  }

  static ActGuidMapper()
  {
    ActGuidMapper._guids.Add(wfConsts.StartGuid, ActivityKind.Start);
    ActGuidMapper._guids.Add(wfConsts.TaskGuid, ActivityKind.Task);
    ActGuidMapper._guids.Add(wfConsts.ApproveGuid, ActivityKind.Approve);
    ActGuidMapper._guids.Add(wfConsts.StopGuid, ActivityKind.Stop);
    ActGuidMapper._guids.Add(wfConsts.CondGuid, ActivityKind.Condition);
    ActGuidMapper._guids.Add(wfConsts.CaseGuid, ActivityKind.Case);
    ActGuidMapper._guids.Add(wfConsts.SubProcessGuid, ActivityKind.SubProcess);
    ActGuidMapper._guids.Add(wfConsts.AbortGuid, ActivityKind.Abort);
    ActGuidMapper._guids.Add(wfConsts.TimerGuid, ActivityKind.Timer);
    ActGuidMapper._guids.Add(wfConsts.RegisterGuid, ActivityKind.Register);
    ActGuidMapper._guids.Add(wfConsts.LifeCycleGuid, ActivityKind.LCStep);
    ActGuidMapper._guids.Add(wfConsts.ScriptGuid, ActivityKind.Script);
    ActGuidMapper._guids.Add(wfConsts.RemoteSubProcessGuid, ActivityKind.RemoteSubProcess);
  }
}
