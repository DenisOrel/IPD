// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.OneManyLock
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System;
using System.Globalization;
using System.Threading;

#nullable disable
namespace Intermech.Expert;

/// <summary>
/// Implements a ResourceLock by way of a high-speed reader/writer lock.
/// (C) Джеффри Рихтер, 2013. Разрешает доступ к ресурсу многим читателям или одному писателю.
/// </summary>
public sealed class OneManyLock : IDisposable
{
  private const int c_lsStateStartBit = 0;
  private const int c_lsReadersReadingStartBit = 3;
  private const int c_lsReadersWaitingStartBit = 12;
  private const int c_lsWritersWaitingStartBit = 21;
  private const int c_lsStateMask = 7;
  private const int c_lsReadersReadingMask = 4088;
  private const int c_lsReadersWaitingMask = 2093056;
  private const int c_lsWritersWaitingMask = 1071644672 /*0x3FE00000*/;
  private const int c_lsAnyWaitingMask = 1073737728;
  private const int c_ls1ReaderReading = 8;
  private const int c_ls1ReaderWaiting = 4096 /*0x1000*/;
  private const int c_ls1WriterWaiting = 2097152 /*0x200000*/;
  private int m_LockState;
  private Semaphore m_ReadersLock = new Semaphore(0, int.MaxValue);
  private Semaphore m_WritersLock = new Semaphore(0, int.MaxValue);
  private bool m_exclusive;

  private static OneManyLock.OneManyLockStates State(int ls)
  {
    return (OneManyLock.OneManyLockStates) (ls & 7);
  }

  private static void SetState(ref int ls, OneManyLock.OneManyLockStates newState)
  {
    ls = (int) ((OneManyLock.OneManyLockStates) (ls & -8) | newState);
  }

  private static int NumReadersReading(int ls) => (ls & 4088) >> 3;

  private static void AddReadersReading(ref int ls, int amount) => ls += 8 * amount;

  private static int NumReadersWaiting(int ls) => (ls & 2093056) >> 12;

  private static void AddReadersWaiting(ref int ls, int amount) => ls += 4096 /*0x1000*/ * amount;

  private static int NumWritersWaiting(int ls) => (ls & 1071644672 /*0x3FE00000*/) >> 21;

  private static void AddWritersWaiting(ref int ls, int amount)
  {
    ls += 2097152 /*0x200000*/ * amount;
  }

  private static bool AnyWaiters(int ls) => (ls & 1073737728) != 0;

  private static string DebugState(int ls)
  {
    return string.Format((IFormatProvider) CultureInfo.InvariantCulture, "State={0}, RR={1}, RW={2}, WW={3}", (object) OneManyLock.State(ls), (object) OneManyLock.NumReadersReading(ls), (object) OneManyLock.NumReadersWaiting(ls), (object) OneManyLock.NumWritersWaiting(ls));
  }

  /// <summary>
  /// Returns a string representing the state of the object.
  /// </summary>
  /// <returns>The string representing the state of the object.</returns>
  public override string ToString() => OneManyLock.DebugState(this.m_LockState);

  public void Dispose()
  {
    this.m_WritersLock.Close();
    this.m_WritersLock = (Semaphore) null;
    this.m_ReadersLock.Close();
    this.m_ReadersLock = (Semaphore) null;
  }

  /// <summary>Acquires the lock.</summary>
  public void Enter(bool exclusive)
  {
    if (exclusive)
    {
      while (OneManyLock.WaitToWrite(ref this.m_LockState))
        this.m_WritersLock.WaitOne();
    }
    else
    {
      while (OneManyLock.WaitToRead(ref this.m_LockState))
        this.m_ReadersLock.WaitOne();
    }
    this.m_exclusive = exclusive;
  }

  private static bool WaitToWrite(ref int target)
  {
    int num = target;
    int comparand;
    bool write;
    do
    {
      comparand = num;
      int ls = comparand;
      write = false;
      switch (OneManyLock.State(ls))
      {
        case OneManyLock.OneManyLockStates.Free:
        case OneManyLock.OneManyLockStates.ReservedForWriter:
          OneManyLock.SetState(ref ls, OneManyLock.OneManyLockStates.OwnedByWriter);
          break;
        case OneManyLock.OneManyLockStates.OwnedByWriter:
          OneManyLock.AddWritersWaiting(ref ls, 1);
          write = true;
          break;
        case OneManyLock.OneManyLockStates.OwnedByReaders:
        case OneManyLock.OneManyLockStates.OwnedByReadersAndWriterPending:
          OneManyLock.SetState(ref ls, OneManyLock.OneManyLockStates.OwnedByReadersAndWriterPending);
          OneManyLock.AddWritersWaiting(ref ls, 1);
          write = true;
          break;
      }
      num = Interlocked.CompareExchange(ref target, ls, comparand);
    }
    while (comparand != num);
    return write;
  }

  /// <summary>Releases the lock.</summary>
  public void Leave()
  {
    int releaseCount;
    if (this.m_exclusive)
    {
      releaseCount = OneManyLock.DoneWriting(ref this.m_LockState);
    }
    else
    {
      int num = (int) OneManyLock.State(this.m_LockState);
      releaseCount = OneManyLock.DoneReading(ref this.m_LockState);
    }
    if (releaseCount == -1)
    {
      this.m_WritersLock.Release();
    }
    else
    {
      if (releaseCount <= 0)
        return;
      this.m_ReadersLock.Release(releaseCount);
    }
  }

  private static int DoneWriting(ref int target)
  {
    int num1 = target;
    int comparand;
    int num2;
    do
    {
      int ls = comparand = num1;
      if (!OneManyLock.AnyWaiters(ls))
      {
        OneManyLock.SetState(ref ls, OneManyLock.OneManyLockStates.Free);
        num2 = 0;
      }
      else if (OneManyLock.NumWritersWaiting(ls) > 0)
      {
        OneManyLock.SetState(ref ls, OneManyLock.OneManyLockStates.ReservedForWriter);
        OneManyLock.AddWritersWaiting(ref ls, -1);
        num2 = -1;
      }
      else
      {
        num2 = OneManyLock.NumReadersWaiting(ls);
        OneManyLock.SetState(ref ls, OneManyLock.OneManyLockStates.OwnedByReaders);
        OneManyLock.AddReadersWaiting(ref ls, -num2);
      }
      num1 = Interlocked.CompareExchange(ref target, ls, comparand);
    }
    while (comparand != num1);
    return num2;
  }

  private static bool WaitToRead(ref int target)
  {
    int num = target;
    int comparand;
    bool read;
    do
    {
      int ls = comparand = num;
      read = false;
      switch (OneManyLock.State(ls))
      {
        case OneManyLock.OneManyLockStates.Free:
          OneManyLock.SetState(ref ls, OneManyLock.OneManyLockStates.OwnedByReaders);
          OneManyLock.AddReadersReading(ref ls, 1);
          break;
        case OneManyLock.OneManyLockStates.OwnedByWriter:
        case OneManyLock.OneManyLockStates.OwnedByReadersAndWriterPending:
        case OneManyLock.OneManyLockStates.ReservedForWriter:
          OneManyLock.AddReadersWaiting(ref ls, 1);
          read = true;
          break;
        case OneManyLock.OneManyLockStates.OwnedByReaders:
          OneManyLock.AddReadersReading(ref ls, 1);
          break;
      }
      num = Interlocked.CompareExchange(ref target, ls, comparand);
    }
    while (comparand != num);
    return read;
  }

  private static int DoneReading(ref int target)
  {
    int num1 = target;
    int comparand;
    int num2;
    do
    {
      int ls = comparand = num1;
      OneManyLock.AddReadersReading(ref ls, -1);
      if (OneManyLock.NumReadersReading(ls) > 0)
        num2 = 0;
      else if (!OneManyLock.AnyWaiters(ls))
      {
        OneManyLock.SetState(ref ls, OneManyLock.OneManyLockStates.Free);
        num2 = 0;
      }
      else
      {
        OneManyLock.SetState(ref ls, OneManyLock.OneManyLockStates.ReservedForWriter);
        OneManyLock.AddWritersWaiting(ref ls, -1);
        num2 = -1;
      }
      num1 = Interlocked.CompareExchange(ref target, ls, comparand);
    }
    while (comparand != num1);
    return num2;
  }

  private enum OneManyLockStates
  {
    Free,
    OwnedByWriter,
    OwnedByReaders,
    OwnedByReadersAndWriterPending,
    ReservedForWriter,
  }
}
