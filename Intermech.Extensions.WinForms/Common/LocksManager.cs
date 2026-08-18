// Decompiled with JetBrains decompiler
// Type: Intermech.Common.LocksManager
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

#nullable disable
namespace Intermech.Common;

public class LocksManager
{
  [CanBeNull]
  private readonly object _owner;
  [CanBeNull]
  private readonly string _ownerAsString;
  [NotNull]
  private readonly string _lockManagerName;
  private int _locksCounterValue;
  private bool _lastTimeCheckedLockedStatus;
  [CanBeNull]
  private readonly List<LocksManager> _childsLockCounters;
  [NotNull]
  private readonly object _syncObj = new object();
  [NotNull]
  private readonly List<string> _operations = new List<string>();
  private static long _unnamedLockManagerNum;

  public LocksManager(
    [CanBeNull] object owner,
    [CanBeNull] string lockManagerName,
    [NotNull] params LocksManager[] childsLockCounters)
    : this(owner, lockManagerName, (LockStatusChangedHandler) null, (IsExternalLockedHandler) null, (IEnumerable<LocksManager>) childsLockCounters)
  {
  }

  public LocksManager(
    [CanBeNull] object owner,
    [CanBeNull] string lockManagerName,
    [CanBeNull] IEnumerable<LocksManager> childsLockCounters)
    : this(owner, lockManagerName, (LockStatusChangedHandler) null, (IsExternalLockedHandler) null, childsLockCounters)
  {
  }

  public LocksManager(
    [CanBeNull] object owner,
    [CanBeNull] string lockManagerName,
    [CanBeNull] LockStatusChangedHandler lockStatusChangedHandler = null,
    [CanBeNull] IsExternalLockedHandler isExternalLockedHandler = null,
    [CanBeNull] IEnumerable<LocksManager> childsLockCounters = null)
  {
    this._ownerAsString = owner?.GetType().Name;
    this._lockManagerName = !string.IsNullOrEmpty(lockManagerName) ? lockManagerName : $"Unnamed lock manager ({(object) LocksManager._unnamedLockManagerNum++})";
    this._owner = owner;
    if (isExternalLockedHandler != null)
      this._isExternalLocked += isExternalLockedHandler;
    if (lockStatusChangedHandler != null)
      this.OnLockStatusChanged += lockStatusChangedHandler;
    IReadOnlyCollection<LocksManager> locksManagers = childsLockCounters != null ? (IReadOnlyCollection<LocksManager>) childsLockCounters.AsList<LocksManager>() : (IReadOnlyCollection<LocksManager>) null;
    if (locksManagers != null && locksManagers.Any<LocksManager>())
    {
      this._childsLockCounters = new List<LocksManager>((IEnumerable<LocksManager>) locksManagers);
      foreach (LocksManager locksManager in (IEnumerable<LocksManager>) locksManagers)
        locksManager.OnLockStatusChanged += new LockStatusChangedHandler(this.ChildLockManagerChangedHandler);
    }
    this._lastTimeCheckedLockedStatus = this.GetIsLocked(0, false);
  }

  private event LockStatusChangedHandler _onLockStatusChanged;

  private event IsExternalLockedHandler _isExternalLocked;

  public int LocksCount
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._locksCounterValue;
    }
  }

  public event IsExternalLockedHandler IsExternalLocked
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._isExternalLocked += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._isExternalLocked -= value;
    }
  }

  public virtual bool IsLocked
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetIsLocked(this._locksCounterValue);
    }
  }

  public event LockStatusChangedHandler OnLockStatusChanged
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] add
    {
      this._onLockStatusChanged += value;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] remove
    {
      this._onLockStatusChanged -= value;
    }
  }

  [NotNull]
  public string[] LockOperations
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      lock (this._syncObj)
        return this._operations.ToArray<string>(this._operations.Count);
    }
  }

  [CanBeNull]
  public string RootLockOperation
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      lock (this._syncObj)
        return this._operations.FirstOrDefault<string>();
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private void ChildLockManagerChangedHandler(
    [CanBeNull] LocksManager childLocksManager,
    [CanBeNull] object locksCounterOwner,
    bool isLocked)
  {
    this.GetIsLocked(this._locksCounterValue, ignoreChildManager: childLocksManager, ignoreChildManagerLocked: isLocked);
  }

  protected virtual bool GetIsLocked(
    int locksCount,
    bool autoFireChangedEvent = true,
    [CanBeNull] LocksManager ignoreChildManager = null,
    bool ignoreChildManagerLocked = false)
  {
    bool isLocked = locksCount > 0;
    if (!isLocked && this._isExternalLocked != null)
      isLocked = !((IEnumerable<Delegate>) this._isExternalLocked.GetInvocationList()).Any<Delegate>((Func<Delegate, bool>) (externalCheck => ((IsExternalLockedHandler) externalCheck)(this, this._owner)));
    if (!isLocked && this._childsLockCounters != null)
      isLocked = this._childsLockCounters.Any<LocksManager>((Func<LocksManager, bool>) (childsLockCounter => childsLockCounter == ignoreChildManager ? ignoreChildManagerLocked : childsLockCounter.IsLocked));
    if (autoFireChangedEvent && this._lastTimeCheckedLockedStatus != isLocked)
    {
      this._lastTimeCheckedLockedStatus = isLocked;
      this.FireOnLockStatusChanged(isLocked);
    }
    return isLocked;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected virtual void FireOnLockStatusChanged(bool isLocked)
  {
    LockStatusChangedHandler lockStatusChanged = this._onLockStatusChanged;
    if (lockStatusChanged == null)
      return;
    lockStatusChanged(this, this._owner, isLocked);
  }

  public void Lock([CanBeNull] string operationName = null)
  {
    int locksCount;
    lock (this._syncObj)
    {
      locksCount = Interlocked.Increment(ref this._locksCounterValue);
      operationName = !string.IsNullOrEmpty(operationName) ? operationName : "Unnamed operation";
      this._operations.Add(operationName);
    }
    if (locksCount != 1)
      return;
    this.GetIsLocked(locksCount);
  }

  public void Unlock([CanBeNull] string operationName = null)
  {
    int locksCount;
    lock (this._syncObj)
    {
      locksCount = Interlocked.Decrement(ref this._locksCounterValue);
      operationName = !string.IsNullOrEmpty(operationName) ? operationName : "Unnamed operation";
      if (!this._operations.RemoveLast<string>(operationName))
        throw new InvalidOperationException($"{this._ownerAsString}.{this._lockManagerName}.Unlock: Operation '{operationName}' was not started");
    }
    if (locksCount < 0)
      throw new Exception("count < 0");
    if (locksCount != 0)
      return;
    this.GetIsLocked(locksCount);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void CheckStatusChanged() => this.GetIsLocked(this._locksCounterValue);

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public LockOperation LockingOperation([CanBeNull] string operationName = null)
  {
    return new LockOperation(this, operationName);
  }
}
