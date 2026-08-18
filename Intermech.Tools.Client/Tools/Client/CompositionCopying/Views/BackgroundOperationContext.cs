// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Views.BackgroundOperationContext
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Views;

internal abstract class BackgroundOperationContext
{
  private readonly SendOrPostCallback invokeInUIThreadHelper;
  private volatile bool isDetachedFromUIThread;

  protected BackgroundOperationContext()
  {
    this.invokeInUIThreadHelper = new SendOrPostCallback(this.InvokeInUIThread);
  }

  public BackgroundWorker Worker { get; internal set; }

  public SynchronizationContext UIContext { get; set; }

  public bool CancellationPending
  {
    [DebuggerStepThrough] get => this.Worker != null && this.Worker.CancellationPending;
  }

  public void ReportProgress(int percentValue)
  {
    if (this.Worker == null || this.isDetachedFromUIThread)
      return;
    this.Worker.ReportProgress(percentValue);
  }

  public void SendToUIThread(Action action)
  {
    if (action == null)
      throw new ArgumentNullException(nameof (action));
    if (this.UIContext == null)
      return;
    this.UIContext.Send(this.invokeInUIThreadHelper, (object) action);
  }

  public void PostToUIThread(Action action)
  {
    if (action == null)
      throw new ArgumentNullException(nameof (action));
    if (this.UIContext == null)
      return;
    this.UIContext.Post(this.invokeInUIThreadHelper, (object) action);
  }

  private void InvokeInUIThread(object arg)
  {
    if (this.isDetachedFromUIThread)
      return;
    ((Action) arg)();
  }

  internal void DetachFromUIThread()
  {
    if (this.isDetachedFromUIThread)
      return;
    this.isDetachedFromUIThread = true;
  }
}
