// Decompiled with JetBrains decompiler
// Type: Intermech.Async.SynchronizationContextAwaiter
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

#nullable disable
namespace Intermech.Async;

public readonly struct SynchronizationContextAwaiter([NotNull] SynchronizationContext context) : 
  INotifyCompletion
{
  [NotNull]
  private static readonly SendOrPostCallback _postCallback = (SendOrPostCallback) (state => ((Action) state)());
  [NotNull]
  private readonly SynchronizationContext _context = context;

  public bool IsCompleted => this._context == SynchronizationContext.Current;

  public void OnCompleted([NotNull] Action continuation)
  {
    this._context.Post(SynchronizationContextAwaiter._postCallback, (object) continuation);
  }

  public void GetResult()
  {
  }
}
