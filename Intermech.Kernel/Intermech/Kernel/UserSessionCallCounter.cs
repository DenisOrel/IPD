// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.UserSessionCallCounter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Threading;
using System;


namespace Intermech.Kernel;

internal sealed class UserSessionCallCounter
{
  private AtomicDateTime _BeginCountTime = new AtomicDateTime(DateTime.UtcNow);
  private AtomicInt32 _CurrentCounter = new AtomicInt32(0);
  private AtomicInt32 _PrevCounter = new AtomicInt32(0);
  private static readonly TimeSpan swapInterval = TimeSpan.FromHours(1.0);

  public UserSessionCallCounter()
  {
    this._BeginCountTime = new AtomicDateTime(DateTime.UtcNow);
    this._CurrentCounter = new AtomicInt32(0);
    this._PrevCounter = new AtomicInt32(0);
  }

  public int Value
  {
    get
    {
      this.SwapCountersPeriodically();
      return this._CurrentCounter.Value * 2 + this._PrevCounter.Value;
    }
  }

  public void Update()
  {
    this.SwapCountersPeriodically();
    this._CurrentCounter.Increment();
  }

  private void SwapCountersPeriodically()
  {
    DateTime utcNow = DateTime.UtcNow;
    DateTime oldValue = this._BeginCountTime.Value;
    if (!(utcNow > oldValue + UserSessionCallCounter.swapInterval) || !this._BeginCountTime.TryModify(oldValue, utcNow))
      return;
    this._PrevCounter.Value = this._CurrentCounter.Value;
    this._CurrentCounter.Value = 0;
  }
}
