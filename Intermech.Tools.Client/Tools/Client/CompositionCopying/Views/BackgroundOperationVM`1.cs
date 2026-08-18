// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Views.BackgroundOperationVM`1
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.UI;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Input;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Views;

internal sealed class BackgroundOperationVM<TOperationContext> : 
  ViewModel,
  IBackgroundOperation,
  INotifyPropertyChanged
  where TOperationContext : BackgroundOperationContext
{
  private readonly BackgroundOperationDescriptor<TOperationContext> descriptor;
  private readonly PluggableCommand startCommand;
  private readonly PluggableCommand stopCommand;
  private readonly BackgroundWorker worker;
  private bool isRunning;
  private int progress;
  private TOperationContext currentOperationContext;

  public BackgroundOperationVM(
    BackgroundOperationDescriptor<TOperationContext> descriptor)
  {
    if (descriptor == null)
      throw new ArgumentNullException(nameof (descriptor));
    descriptor.RequireFrozen();
    this.descriptor = descriptor;
    this.startCommand = new PluggableCommand(new Action(this.OnStartCommand));
    this.startCommand.Enabled = false;
    this.stopCommand = new PluggableCommand(new Action(this.OnStopCommand));
    this.stopCommand.Enabled = false;
    this.worker = new BackgroundWorker();
    this.worker.WorkerReportsProgress = true;
    this.worker.WorkerSupportsCancellation = true;
    this.worker.DoWork += new DoWorkEventHandler(this.OnRunOperationInBackground);
    this.worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.OnOperationCompleted);
    this.worker.ProgressChanged += new ProgressChangedEventHandler(this.OnOperationProgressChanged);
  }

  public bool IsRunning
  {
    [DebuggerStepThrough] get => this.isRunning;
    private set
    {
      if (this.isRunning == value)
        return;
      this.isRunning = value;
      this.RaisePropertyChanged(nameof (IsRunning));
    }
  }

  public int Progress
  {
    [DebuggerStepThrough] get => this.progress;
    private set
    {
      if (this.progress == value)
        return;
      this.progress = value;
      this.RaisePropertyChanged(nameof (Progress));
    }
  }

  public ICommand StartCommand
  {
    [DebuggerStepThrough] get => (ICommand) this.startCommand;
  }

  public ICommand StopCommand
  {
    [DebuggerStepThrough] get => (ICommand) this.stopCommand;
  }

  public event EventHandler Starting;

  public event EventHandler Started;

  public event EventHandler Finished;

  public void SwitchCommands(bool canStart)
  {
    if (canStart)
    {
      this.startCommand.Enabled = !this.IsRunning && !this.worker.IsBusy;
      this.stopCommand.Enabled = false;
    }
    else
    {
      this.startCommand.Enabled = false;
      this.stopCommand.Enabled = false;
    }
  }

  public void CancelNoWait()
  {
    if (!this.IsRunning)
      return;
    this.worker.CancelAsync();
    if ((object) this.currentOperationContext != null)
    {
      this.currentOperationContext.DetachFromUIThread();
      this.currentOperationContext = default (TOperationContext);
    }
    this.IsRunning = false;
  }

  private void OnStartCommand()
  {
    TOperationContext operationContext = this.descriptor.OnCreateOperationContext();
    operationContext.Worker = this.worker;
    if (operationContext.UIContext == null && SynchronizationContext.Current != null)
      operationContext.UIContext = SynchronizationContext.Current;
    this.Progress = 0;
    EventHandler starting = this.Starting;
    if (starting != null)
      starting((object) this, EventArgs.Empty);
    this.worker.RunWorkerAsync((object) operationContext);
    this.currentOperationContext = operationContext;
    this.IsRunning = true;
    this.startCommand.Enabled = false;
    this.stopCommand.Enabled = true;
    EventHandler started = this.Started;
    if (started == null)
      return;
    started((object) this, EventArgs.Empty);
  }

  private void OnStopCommand()
  {
    if (!this.worker.IsBusy)
      return;
    this.worker.CancelAsync();
  }

  private void OnOperationCompleted(object sender, RunWorkerCompletedEventArgs e)
  {
    if ((object) this.currentOperationContext != null)
    {
      TOperationContext operationContext = this.currentOperationContext;
      this.currentOperationContext = default (TOperationContext);
      this.IsRunning = false;
      this.Progress = 100;
      this.startCommand.Enabled = false;
      this.stopCommand.Enabled = false;
      if (this.descriptor.OnResult != null)
        this.descriptor.OnResult(operationContext, e.Cancelled, e.Error);
    }
    EventHandler finished = this.Finished;
    if (finished == null)
      return;
    finished((object) this, EventArgs.Empty);
  }

  private void OnOperationProgressChanged(object sender, ProgressChangedEventArgs e)
  {
    this.Progress = e.ProgressPercentage;
  }

  private void OnRunOperationInBackground(object sender, DoWorkEventArgs e)
  {
    this.descriptor.OnRunInBackground((TOperationContext) e.Argument);
    if (!this.worker.CancellationPending)
      return;
    e.Cancel = true;
  }
}
