// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.TaskStatusExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

#nullable disable
namespace Intermech.Extensions;

public static class TaskStatusExtensions
{
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool NotEnded(this TaskStatus taskStatus)
  {
    return taskStatus != TaskStatus.Canceled && taskStatus != TaskStatus.Faulted && taskStatus != TaskStatus.RanToCompletion;
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Ended(this TaskStatus taskStatus)
  {
    return taskStatus == TaskStatus.Canceled || taskStatus == TaskStatus.Faulted || taskStatus == TaskStatus.RanToCompletion;
  }
}
