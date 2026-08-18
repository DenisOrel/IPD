// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.RunOnce
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

#nullable disable
namespace Intermech.Extensions;

public class RunOnce
{
  [NotNull]
  private readonly object _syncObj = new object();
  [NotNull]
  private readonly Action _action;
  private int _alreadyCalledFlag;

  public RunOnce([NotNull] Action action) => this._action = action;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool TryRun()
  {
    if (this.Completed)
      return false;
    lock (this._syncObj)
    {
      if (this.Completed)
        return false;
      this._action();
      Thread.MemoryBarrier();
      Thread.VolatileWrite(ref this._alreadyCalledFlag, int.MaxValue);
      return true;
    }
  }

  public bool Completed
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return Thread.VolatileRead(ref this._alreadyCalledFlag) != 0;
    }
  }
}
