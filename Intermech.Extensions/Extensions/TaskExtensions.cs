// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.TaskExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

#nullable disable
namespace Intermech.Extensions;

public static class TaskExtensions
{
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool NotEnded([NotNull] this Task task)
  {
    TaskStatus status = task.Status;
    switch (status)
    {
      case TaskStatus.Canceled:
      case TaskStatus.Faulted:
        return false;
      default:
        return status != TaskStatus.RanToCompletion;
    }
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Ended([NotNull] this Task task)
  {
    TaskStatus status = task.Status;
    switch (status)
    {
      case TaskStatus.Canceled:
      case TaskStatus.Faulted:
        return true;
      default:
        return status == TaskStatus.RanToCompletion;
    }
  }
}
