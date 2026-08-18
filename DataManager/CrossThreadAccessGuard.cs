// Decompiled with JetBrains decompiler
// Type: Intermech.Server.Data.CrossThreadAccessGuard
// Assembly: DataManager, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E7B48B20-48DA-43CF-8D62-6AD3E6FD5CCD
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\DataManager.dll

using Intermech.Runtime;
using System;
using System.Diagnostics;
using System.Threading;

#nullable disable
namespace Intermech.Server.Data;

internal abstract class CrossThreadAccessGuard
{
  private const int SpinTime = 10;
  private const int DefaultStateTag = 0;
  private const int RecoveryStateTag = 1;
  private CrossThreadAccessGuard.TaggedReference<CrossThreadAccessGuard.AccessDescriptor> guardState;
  private static readonly CrossThreadAccessGuard.TaggedReference<CrossThreadAccessGuard.AccessDescriptor> emptyState = new CrossThreadAccessGuard.TaggedReference<CrossThreadAccessGuard.AccessDescriptor>((CrossThreadAccessGuard.AccessDescriptor) null, 0);
  private static readonly CrossThreadAccessGuard.TaggedReference<CrossThreadAccessGuard.AccessDescriptor> recoveryState = new CrossThreadAccessGuard.TaggedReference<CrossThreadAccessGuard.AccessDescriptor>((CrossThreadAccessGuard.AccessDescriptor) null, 1);

  public CrossThreadAccessGuard() => this.guardState = CrossThreadAccessGuard.emptyState;

  public bool IsEntered
  {
    [DebuggerStepThrough] get
    {
      CrossThreadAccessGuard.AccessDescriptor forCurrentThread = this.TryGetAccessDescriptorForCurrentThread(Thread.CurrentThread.ManagedThreadId);
      return forCurrentThread != null && forCurrentThread.Depth != 0;
    }
  }

  private CrossThreadAccessGuard.AccessDescriptor TryGetAccessDescriptorForCurrentThread(
    int currentThreadId)
  {
    CrossThreadAccessGuard.AccessDescriptor target = this.guardState.Target;
    return target != null && target.ThreadId == currentThreadId ? target : (CrossThreadAccessGuard.AccessDescriptor) null;
  }

  public void Enter(int timeout)
  {
    if (timeout <= 0)
      throw new ArgumentOutOfRangeException(nameof (timeout));
    int managedThreadId = Thread.CurrentThread.ManagedThreadId;
    CrossThreadAccessGuard.AccessDescriptor forCurrentThread = this.TryGetAccessDescriptorForCurrentThread(managedThreadId);
    if (forCurrentThread != null)
      forCurrentThread.IncrementDepth();
    else if (!this.TryEnterInternal(managedThreadId))
    {
      int num = timeout / 10;
      while (num != 0)
      {
        --num;
        Thread.Sleep(10);
        if (this.TryEnterInternal(managedThreadId))
          return;
      }
      throw this.CreateTimeoutException(CrossThreadAccessOperation.Enter);
    }
  }

  private bool TryEnterInternal(int currentThreadId)
  {
    CrossThreadAccessGuard.TaggedReference<CrossThreadAccessGuard.AccessDescriptor> taggedReference = Interlocked.CompareExchange<CrossThreadAccessGuard.TaggedReference<CrossThreadAccessGuard.AccessDescriptor>>(ref this.guardState, new CrossThreadAccessGuard.TaggedReference<CrossThreadAccessGuard.AccessDescriptor>(new CrossThreadAccessGuard.AccessDescriptor(currentThreadId), 0), CrossThreadAccessGuard.emptyState);
    if (taggedReference == CrossThreadAccessGuard.emptyState)
      return true;
    if (taggedReference == CrossThreadAccessGuard.recoveryState)
      return false;
    CrossThreadAccessGuard.AccessDescriptor target = taggedReference.Target;
    if (target.TrySetConflictedState())
    {
      CrossThreadConflictInfo conflictInfo = target.GetOrCreateConflictInfo();
      throw this.CreateThreadConflictException(new CrossThreadAccessInfo(currentThreadId, this.GetCurrentThreadStackTrace()), CrossThreadAccessOperation.Enter, conflictInfo);
    }
    return false;
  }

  public void Exit(Action recoveryAction = null)
  {
    int managedThreadId = Thread.CurrentThread.ManagedThreadId;
    CrossThreadAccessGuard.AccessDescriptor forCurrentThread = this.TryGetAccessDescriptorForCurrentThread(managedThreadId);
    if (forCurrentThread == null)
      throw new InvalidOperationException("The method Exit() was called without Enter().");
    forCurrentThread.DecrementDepth();
    if (forCurrentThread.Depth != 0)
      return;
    Interlocked.Exchange<CrossThreadAccessGuard.TaggedReference<CrossThreadAccessGuard.AccessDescriptor>>(ref this.guardState, CrossThreadAccessGuard.recoveryState);
    if (forCurrentThread.TrySetInactiveState())
    {
      Interlocked.Exchange<CrossThreadAccessGuard.TaggedReference<CrossThreadAccessGuard.AccessDescriptor>>(ref this.guardState, CrossThreadAccessGuard.emptyState);
    }
    else
    {
      if (recoveryAction != null)
        SilentActionInvoker.Default.Invoke(recoveryAction);
      Interlocked.Exchange<CrossThreadAccessGuard.TaggedReference<CrossThreadAccessGuard.AccessDescriptor>>(ref this.guardState, CrossThreadAccessGuard.emptyState);
      CrossThreadConflictInfo conflictInfo = forCurrentThread.GetOrCreateConflictInfo();
      throw this.CreateThreadConflictException(new CrossThreadAccessInfo(managedThreadId, this.GetCurrentThreadStackTrace()), CrossThreadAccessOperation.Exit, conflictInfo);
    }
  }

  protected virtual string GetCurrentThreadStackTrace() => Environment.StackTrace;

  protected virtual Exception CreateTimeoutException(CrossThreadAccessOperation operation)
  {
    return (Exception) new TimeoutException("Unable to get access to a guarded object. Too many concurrent threads.");
  }

  protected abstract Exception CreateThreadConflictException(
    CrossThreadAccessInfo threadInfo,
    CrossThreadAccessOperation operation,
    CrossThreadConflictInfo conflictInfo);

  private sealed class AccessDescriptor
  {
    private const int ActiveState = 0;
    private const int InactiveState = 1;
    private const int ConflictedState = 2;
    private int threadId;
    private int depth;
    private int accessState;
    private CrossThreadConflictInfo conflictInfo;

    public AccessDescriptor(int threadId)
    {
      this.threadId = threadId;
      this.depth = 1;
      this.accessState = 0;
    }

    public int ThreadId
    {
      [DebuggerStepThrough] get => this.threadId;
    }

    public int Depth
    {
      [DebuggerStepThrough] get => this.depth;
    }

    public void IncrementDepth() => Interlocked.Increment(ref this.depth);

    public void DecrementDepth() => Interlocked.Decrement(ref this.depth);

    public bool TrySetConflictedState()
    {
      int num = Interlocked.CompareExchange(ref this.accessState, 2, 0);
      return num == 0 || num == 2;
    }

    public bool TrySetInactiveState()
    {
      return Interlocked.CompareExchange(ref this.accessState, 1, 0) == 0;
    }

    public CrossThreadConflictInfo GetOrCreateConflictInfo()
    {
      if (this.conflictInfo != null)
        return this.conflictInfo;
      CrossThreadConflictInfo threadConflictInfo = new CrossThreadConflictInfo(Guid.NewGuid());
      return Interlocked.CompareExchange<CrossThreadConflictInfo>(ref this.conflictInfo, threadConflictInfo, (CrossThreadConflictInfo) null) ?? threadConflictInfo;
    }
  }

  private sealed class TaggedReference<T> where T : class
  {
    private T target;
    private int tag;

    public TaggedReference(T target, int tag)
    {
      this.target = target;
      this.tag = tag;
    }

    public T Target
    {
      [DebuggerStepThrough] get => this.target;
    }

    public int Tag
    {
      [DebuggerStepThrough] get => this.tag;
    }
  }
}
