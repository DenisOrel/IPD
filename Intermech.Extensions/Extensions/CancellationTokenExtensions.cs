// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.CancellationTokenExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Intermech.Extensions;

public static class CancellationTokenExtensions
{
  public static TaskAwaiter GetAwaiter(this CancellationToken cancellationToken)
  {
    TaskCompletionSource<bool> state = new TaskCompletionSource<bool>();
    Task<bool> task = state.Task;
    if (cancellationToken.IsCancellationRequested)
      state.SetResult(true);
    else
      cancellationToken.Register((Action<object>) (s => ((TaskCompletionSource<bool>) s).SetResult(true)), (object) state);
    return ((Task) task).GetAwaiter();
  }
}
