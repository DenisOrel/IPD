// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Classes.Tasks.AsyncTaskBase`2
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Contexts;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Intermech.TechCard.Client.Classes.Tasks;

/// <summary>
/// Общий класс для реализации фоновых / асинхронных задач
/// </summary>
/// <typeparam name="TParam"></typeparam>
/// <typeparam name="TResult"></typeparam>
internal abstract class AsyncTaskBase<TParam, TResult>
{
  /// <summary>
  /// Фиксированный контекст редактирования или <see cref="P:Intermech.Interfaces.Contexts.CurrentEditingContext.Dummy" />
  /// </summary>
  private CurrentEditingContext _editingContext;
  /// <summary>
  /// 
  /// </summary>
  private readonly SynchronizationContext _synchronizationContext;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="action"></param>
  /// <param name="state"></param>
  private void OnSendAction(SendOrPostCallback action, object state)
  {
    if (this._synchronizationContext != null)
      this._synchronizationContext.Send(action, state);
    else
      action(state);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="action"></param>
  /// <param name="state"></param>
  private void OnPostAction(SendOrPostCallback action, object state)
  {
    if (this._synchronizationContext != null)
      this._synchronizationContext.Post(action, state);
    else
      action(state);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="taskCompleteEventArgs"></param>
  private void OnTaskCompleted(object taskCompleteEventArgs)
  {
    AsyncTaskBase<TParam, TResult>.TaskCompletedEventHandler taskCompleted = this.TaskCompleted;
    if (taskCompleted == null)
      return;
    taskCompleted((object) this, (AsyncTaskBase<TParam, TResult>.TaskCompleteEventArgs) taskCompleteEventArgs);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="exceptionEventArgs"></param>
  private void OnHandleException(object exceptionEventArgs)
  {
    ExceptionHandler handleException = this.HandleException;
    if (handleException == null)
      return;
    handleException((object) this, (ExceptionEventArgs) exceptionEventArgs);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="progressChangedEventArgs"></param>
  private void OnProgressChanged(object progressChangedEventArgs)
  {
    ProgressChangedEventHandler progressChanged = this.ProgressChanged;
    if (progressChanged == null)
      return;
    progressChanged((object) this, (ProgressChangedEventArgs) progressChangedEventArgs);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="techcardAsyncTask"></param>
  /// <param name="data"></param>
  /// <returns></returns>
  protected abstract TResult DoExecute(
    AsyncTaskBase<TParam, TResult> techcardAsyncTask,
    TParam data);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="synchronizationContext"></param>
  protected AsyncTaskBase(SynchronizationContext synchronizationContext = null)
  {
    this._editingContext = CurrentEditingContext.Dummy;
    this._synchronizationContext = synchronizationContext;
  }

  /// <summary>
  /// Возвращает или задает фиксированный контекст редактирования, в рамках которого выполняется задача.
  /// Значение свойства может содержать объект-пустышку <see cref="P:Intermech.Interfaces.Contexts.CurrentEditingContext.Dummy" />,
  /// который обозначает, что контекст редактирования не фиксирован.
  /// </summary>
  public CurrentEditingContext EditingContext
  {
    [DebuggerStepThrough] get => this._editingContext;
    set
    {
      this._editingContext = value != null ? value : throw new ArgumentNullException(nameof (value));
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public Task Execute(TParam data)
  {
    this.CancellationSource = new CancellationTokenSource();
    return Task.Factory.StartNew(this._editingContext.SendToTask((Action) (() =>
    {
      this.Result = this.DoExecute(this, data);
      this.OnPostAction(new SendOrPostCallback(this.OnTaskCompleted), (object) new AsyncTaskBase<TParam, TResult>.TaskCompleteEventArgs(this.Result));
    })), this.CancellationSource.Token, this.TaskOptions, TaskScheduler.Default).ContinueWith((Action<Task>) (x =>
    {
      if (x.Exception == null)
        return;
      foreach (Exception innerException in x.Exception.Flatten().InnerExceptions)
        this.OnPostAction(new SendOrPostCallback(this.OnHandleException), (object) new ExceptionEventArgs(innerException));
    }), this.CancellationSource.Token);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="progressChangedEventArgs"></param>
  public void OnProgressChanged(ProgressChangedEventArgs progressChangedEventArgs)
  {
    if (progressChangedEventArgs == null)
      throw new ArgumentNullException(nameof (progressChangedEventArgs));
    this.OnPostAction(new SendOrPostCallback(this.OnProgressChanged), (object) progressChangedEventArgs);
  }

  /// <summary>Вызывается при изменении прогресса задачи</summary>
  public event ProgressChangedEventHandler ProgressChanged;

  /// <summary>Вызывается при завершении работы задачи</summary>
  public event AsyncTaskBase<TParam, TResult>.TaskCompletedEventHandler TaskCompleted;

  /// <summary>
  /// Вызывается при возникновении в системе необработанного исключения.
  /// </summary>
  public event ExceptionHandler HandleException;

  /// <summary>
  /// 
  /// </summary>
  public TResult Result { get; private set; }

  /// <summary>
  /// 
  /// </summary>
  public CancellationTokenSource CancellationSource { get; private set; } = new CancellationTokenSource();

  /// <summary>
  /// 
  /// </summary>
  public TaskCreationOptions TaskOptions { get; set; } = TaskCreationOptions.LongRunning | TaskCreationOptions.DenyChildAttach;

  /// <summary>
  /// 
  /// </summary>
  public class TaskCompleteEventArgs : EventArgs
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="result"></param>
    public TaskCompleteEventArgs(TResult result) => this.Result = result;

    /// <summary>
    /// 
    /// </summary>
    public TResult Result { get; }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="args"></param>
  public delegate void TaskCompletedEventHandler(
    object sender,
    AsyncTaskBase<TParam, TResult>.TaskCompleteEventArgs args);
}
