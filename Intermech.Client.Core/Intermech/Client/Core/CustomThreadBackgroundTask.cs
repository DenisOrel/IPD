
// Type: Intermech.Client.Core.CustomThreadBackgroundTask
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>
/// Класс фоновой задачи для представления в окне фоновых задач
/// (базовый не реалирует thread-safe доступ к контролам)
/// </summary>
public abstract class CustomThreadBackgroundTask : CustomBackgroundTask
{
  /// <summary>
  /// 
  /// </summary>
  protected bool _paused = true;
  /// <summary>Контрол из "основного" потока</summary>
  /// <remarks>Для корректного вызова Invoke only</remarks>
  protected Control _mainThreadControl;
  /// <summary>Поток, в контексте которого будет все крутиться.</summary>
  /// <remarks>В данном классе не используется, нужен для потомков</remarks>
  protected Thread _thread;
  /// <summary>
  /// 
  /// </summary>
  protected EventWaitHandle _event;

  /// <summary>Инициализация параметров класса</summary>
  protected virtual void InitializeData()
  {
    this._event = new EventWaitHandle(true, EventResetMode.ManualReset);
  }

  /// <summary>
  /// 
  /// </summary>
  protected virtual void ThreadProc()
  {
    try
    {
      this.CustomThreadProc();
    }
    catch (Exception ex)
    {
      switch (ex)
      {
        case ThreadAbortException _:
        case ThreadInterruptedException _:
          break;
        default:
          this.SetState(BackgroundTaskState.Error);
          this.SetThrow(ex);
          break;
      }
    }
    Thread.Sleep(1000);
    this.OnChanged(BackgroundTaskChangedType.Dispose);
  }

  /// <summary>Основная процедура фоновой задачи</summary>
  protected abstract void CustomThreadProc();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected virtual void DoThrowException(Exception e)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="text"></param>
  protected virtual void DoWriteOutput(string text)
  {
  }

  /// <summary>Конструктор</summary>
  protected CustomThreadBackgroundTask()
    : this((Control) null)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="mainThreadControl">Контрол из "основного" потока</param>
  protected CustomThreadBackgroundTask(Control mainThreadControl)
  {
    this._mainThreadControl = mainThreadControl;
    this.InitializeData();
  }

  /// <summary>InvokeRequired flag</summary>
  public virtual bool InvokeRequired
  {
    get => this._mainThreadControl != null && this._mainThreadControl.InvokeRequired;
  }

  /// <summary>Invoke method</summary>
  /// <param name="method"></param>
  /// <param name="args"></param>
  /// <returns></returns>
  public virtual object Invoke(Delegate method, params object[] args)
  {
    return this._mainThreadControl == null ? (object) null : this._mainThreadControl.Invoke(method, args);
  }

  /// <summary>Invoke method</summary>
  /// <param name="method"></param>
  /// <param name="args"></param>
  /// <returns></returns>
  public virtual object BeginInvoke(Delegate method, params object[] args)
  {
    return this._mainThreadControl == null ? (object) null : (object) this._mainThreadControl.BeginInvoke(method, args);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="type"></param>
  protected override void OnChanged(BackgroundTaskChangedType type)
  {
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new CustomThreadBackgroundTask.SetChangedCallback(((CustomBackgroundTask) this).OnChanged), (object) type);
    else
      base.OnChanged(type);
  }

  /// <summary>Установка статуса</summary>
  /// <param name="state"></param>
  protected virtual void SetState(BackgroundTaskState state)
  {
    if (this._state == state)
      return;
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new CustomThreadBackgroundTask.SetStateCallback(this.SetState), (object) state);
    else
      this.State = state;
  }

  /// <summary>Установка значения</summary>
  /// <param name="value"></param>
  protected virtual void SetValue(object value)
  {
    if (value is int num && this._value == num)
      return;
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new CustomThreadBackgroundTask.SetValueCallback(this.SetValue), value);
    else
      this.Value = value;
  }

  /// <summary>Установка результата</summary>
  /// <param name="value"></param>
  protected void SetResult(object value)
  {
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new CustomThreadBackgroundTask.SetValueCallback(this.SetValue), value);
    else
      this.Result = value;
  }

  /// <summary>Вдача exception</summary>
  /// <param name="e"></param>
  protected virtual void SetThrow(Exception e)
  {
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new CustomThreadBackgroundTask.SetThrowCallback(this.SetThrow), (object) e);
    else
      this.DoThrowException(e);
  }

  /// <summary>Вывод строки сообщения в IOutputView</summary>
  /// <param name="text"></param>
  protected void WriteOutput(string text)
  {
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new CustomThreadBackgroundTask.WriteOutputCallback(this.WriteOutput), (object) text);
    else
      this.DoWriteOutput(text);
  }

  /// <summary>Запуск процесса</summary>
  public virtual void Start()
  {
    if (this._thread == null || (this._thread.ThreadState & (ThreadState.Unstarted | ThreadState.Stopped)) == ThreadState.Running)
      return;
    this._thread.Start();
    this._paused = false;
  }

  /// <summary>Остановить процесс</summary>
  public override void Stop()
  {
    if (!this.CanStop())
      return;
    base.Stop();
    if (this._thread != null)
      this._thread.Abort();
    this.OnChanged(BackgroundTaskChangedType.Dispose);
  }

  /// <summary>Приостановить процесс</summary>
  public override void Pause()
  {
    if (!this.CanPause())
      return;
    base.Pause();
    this._paused = true;
    this._event.Reset();
  }

  /// <summary>
  /// 
  /// </summary>
  public override void Resume()
  {
    if (!this.CanResume())
      return;
    base.Resume();
    if ((this._thread.ThreadState & (ThreadState.Unstarted | ThreadState.Stopped)) != ThreadState.Running)
    {
      this._thread.Start();
      this._paused = false;
    }
    else
    {
      if (!this._paused)
        return;
      this._event.Set();
      this._paused = false;
    }
  }

  /// <summary>Принудительно завершить процесс</summary>
  public override void Terminate()
  {
    if (!this.CanTerminate())
      return;
    base.Terminate();
    if (this._thread != null)
      this._thread.Abort();
    this.OnChanged(BackgroundTaskChangedType.Dispose);
  }

  /// <summary>Callback для статуса</summary>
  /// <param name="state"></param>
  public delegate void SetStateCallback(BackgroundTaskState state);

  /// <summary>Callback для значения</summary>
  /// <param name="value"></param>
  public delegate void SetValueCallback(object value);

  /// <summary>Callback для exception</summary>
  /// <param name="e"></param>
  public delegate void SetThrowCallback(Exception e);

  /// <summary>Callback для изменений</summary>
  /// <param name="type"></param>
  public delegate void SetChangedCallback(BackgroundTaskChangedType type);

  /// <summary>Callback для IOutputView</summary>
  /// <param name="text"></param>
  public delegate void WriteOutputCallback(string text);
}
