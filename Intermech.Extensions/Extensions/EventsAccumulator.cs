// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.EventsAccumulator
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

#nullable disable
namespace Intermech.Extensions;

public sealed class EventsAccumulator : IDisposable
{
  private const int ConstDefaultDelay = 200;
  [NotNull]
  private readonly object _syncObj = new object();
  [CanBeNull]
  private Timer _timer;
  [PositiveNumber]
  private readonly int _delay;
  [NotNull]
  private readonly Action _handler;
  [CanBeNull]
  private readonly SynchronizationContext _capturedContext;
  private readonly bool _firstEventCallHandler;
  private int _counter;
  private bool _disposing;

  public EventsAccumulator([NotNull] Action handler, [PositiveNumber] int delay = 200, bool firstEventCallHandler = true)
  {
    this._handler = handler;
    this._delay = delay;
    this._capturedContext = SynchronizationContext.Current;
    this._firstEventCallHandler = firstEventCallHandler;
  }

  public EventsAccumulator([NotNull] Action handler, bool firstEventCallHandler, [PositiveNumber] int delay = 200)
    : this(handler, delay, firstEventCallHandler)
  {
  }

  public void Dispose()
  {
    this._disposing = true;
    lock (this._syncObj)
      this._timer?.Dispose();
  }

  public void Event()
  {
    if (this._disposing)
      return;
    if (this._capturedContext != null && this._capturedContext != SynchronizationContext.Current)
      this._capturedContext.Post(new Action(this._event));
    else
      this._event();
  }

  private void _event()
  {
    lock (this._syncObj)
    {
      if (this._disposing)
        return;
      if (this._timer == null)
      {
        this._counter = 0;
        if (this._firstEventCallHandler)
          this.CallHandler();
        this._timer = new Timer(new TimerCallback(this.TimerHandler), (object) null, this._delay, 0);
      }
      else
      {
        if (this._counter < int.MaxValue)
          ++this._counter;
        else
          this._counter = 1;
        this._timer.Change(this._delay, 0);
      }
    }
  }

  private void TimerHandler([CanBeNull] object objectState)
  {
    if (this._disposing)
      return;
    int counter = this._counter;
    lock (this._syncObj)
    {
      if (this._disposing || counter != this._counter)
        return;
      this._counter = 0;
      this._timer?.Dispose();
      this._timer = (Timer) null;
      if (this._firstEventCallHandler && counter <= 0)
        return;
      this.CallHandler();
    }
  }

  [Pure]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private void CallHandler()
  {
    if (this._capturedContext != null)
      this._capturedContext.Post(this._handler);
    else
      this._handler();
  }
}
