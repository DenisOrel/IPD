// Decompiled with JetBrains decompiler
// Type: Intermech.Common.LockOperation
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System;

#nullable disable
namespace Intermech.Common;

public class LockOperation : IDisposable
{
  [NotNull]
  public readonly LocksManager LocksCounter;
  [CanBeNull]
  public readonly string OperationName;

  public LockOperation([NotNull] LocksManager locksCounter, [CanBeNull] string operationName)
  {
    this.LocksCounter = locksCounter;
    this.OperationName = operationName;
    locksCounter.Lock(operationName);
  }

  void IDisposable.Dispose() => this.LocksCounter.Unlock(this.OperationName);
}
