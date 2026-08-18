// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.Async
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

#nullable disable
namespace Intermech.Extensions;

public static class Async
{
  private const int CheckCancelInterval = 50;

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Task Delay(int milliseconds, [CanBeNull] CancellationToken? cancellationToken = null)
  {
    return Async.Delay(milliseconds, 50, cancellationToken);
  }

  [NotNull]
  public static Task Delay(
    [PositiveNumber] int milliseconds,
    [PositiveNumber] int millisecondsCheckCancelInterval,
    [CanBeNull] CancellationToken? cancellationToken = null)
  {
    TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
    System.Timers.Timer timer = new System.Timers.Timer();
    DateTime started = DateTime.Now;
    if (cancellationToken.HasValue)
      timer.Elapsed += (ElapsedEventHandler) ((obj, args) =>
      {
        if (cancellationToken.Value.IsCancellationRequested)
        {
          taskCompletionSource.TrySetCanceled(cancellationToken.Value);
          timer.Stop();
          timer.Dispose();
        }
        else
        {
          if ((DateTime.Now - started).Milliseconds < milliseconds)
            return;
          taskCompletionSource.TrySetResult(true);
          timer.Stop();
          timer.Dispose();
        }
      });
    else
      timer.Elapsed += (ElapsedEventHandler) ((obj, args) =>
      {
        taskCompletionSource.TrySetResult(true);
        timer.Stop();
        timer.Dispose();
      });
    timer.Interval = cancellationToken.HasValue ? (double) Math.Min(millisecondsCheckCancelInterval, milliseconds) : (double) milliseconds;
    timer.AutoReset = true;
    timer.Start();
    return (Task) taskCompletionSource.Task;
  }
}
