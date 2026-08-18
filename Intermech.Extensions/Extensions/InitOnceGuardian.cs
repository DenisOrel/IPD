// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.InitOnceGuardian
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

#nullable disable
namespace Intermech.Extensions;

public class InitOnceGuardian
{
  [NotNull]
  private readonly object _syncObj = new object();
  private int _alreadyCalledFlag;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Invoke([NotNull] Action action)
  {
    if (this.Completed)
      return;
    lock (this._syncObj)
    {
      if (this.Completed)
        return;
      action();
      Thread.MemoryBarrier();
      Thread.VolatileWrite(ref this._alreadyCalledFlag, int.MaxValue);
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
