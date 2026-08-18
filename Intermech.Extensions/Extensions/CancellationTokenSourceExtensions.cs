// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.CancellationTokenSourceExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Intermech.Extensions;

public static class CancellationTokenSourceExtensions
{
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void ThrowIfCancellationRequested(
    [NotNull] this CancellationTokenSource сancellationTokenSource,
    [CanBeNull] string message = null)
  {
    if (!сancellationTokenSource.IsCancellationRequested)
      return;
    if (!string.IsNullOrWhiteSpace(message))
      throw new OperationCanceledException(message, сancellationTokenSource.Token);
    throw new OperationCanceledException(сancellationTokenSource.Token);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TaskAwaiter GetAwaiter(
    [NotNull] this CancellationTokenSource сancellationTokenSource)
  {
    TaskCompletionSource<bool> state = new TaskCompletionSource<bool>();
    Task<bool> task = state.Task;
    if (сancellationTokenSource.IsCancellationRequested)
      state.SetResult(true);
    else
      сancellationTokenSource.Token.Register((Action<object>) (s => ((TaskCompletionSource<bool>) s).SetResult(true)), (object) state);
    return ((Task) task).GetAwaiter();
  }
}
