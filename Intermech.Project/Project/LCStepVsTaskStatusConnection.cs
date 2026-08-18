// Decompiled with JetBrains decompiler
// Type: Intermech.Project.LCStepVsTaskStatusConnection
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Project;

/// <summary>Связь между шагом жизненного цикла и статусом задачи</summary>
internal static class LCStepVsTaskStatusConnection
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int TaskStatusToLCStep(TaskStatus status)
  {
    switch (status)
    {
      case TaskStatus.Waiting:
        return LCStep.Waiting.ID;
      case TaskStatus.Sent:
        return LCStep.Sent.ID;
      case TaskStatus.Executed:
        return LCStep.Executing.ID;
      case TaskStatus.Pending:
        return LCStep.Validating.ID;
      case TaskStatus.Completed:
        return LCStep.Completed.ID;
      case TaskStatus.Terminated:
        return LCStep.Terminated.ID;
      default:
        throw new Exception($"Unsupported TaskStatus value: {status}");
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TaskStatus LCStepToTaskStatus(int lcStep)
  {
    if (lcStep == LCStep.Sent.ID)
      return TaskStatus.Sent;
    if (lcStep == LCStep.Executing.ID)
      return TaskStatus.Executed;
    if (lcStep == LCStep.Completed.ID)
      return TaskStatus.Completed;
    if (lcStep == LCStep.Validating.ID)
      return TaskStatus.Pending;
    if (lcStep == LCStep.Terminated.ID)
      return TaskStatus.Terminated;
    return lcStep == LCStep.Waiting.ID ? TaskStatus.Waiting : TaskStatus.NotStarted;
  }
}
