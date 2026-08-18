
// Type: Intermech.Navigator.Controls.ThreadPoolJobInfo
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Содержит сведения о результатах выполнения задания в
/// фоновом потоке, выделенном с помощью класса ThreadPool.
/// </summary>
internal class ThreadPoolJobInfo : JobInfo
{
  /// <summary>
  /// Событие, срабатывающее при завершении выполнения задания.
  /// </summary>
  public event ThreadPoolJobInfo.CompleteCallback Complete;

  public ThreadPoolJobInfo(IJob job, object marker)
    : base(job, marker)
  {
    this.Complete = (ThreadPoolJobInfo.CompleteCallback) null;
  }

  /// <summary>Отменяет выполнение задания.</summary>
  public void Cancel()
  {
    if (this._state != JobState.Waiting && this._state != JobState.Running)
      return;
    this._state = JobState.Cancelled;
  }

  /// <summary>Контролирует выполнение задания в фоновом потоке.</summary>
  /// <param name="state"></param>
  public void WaitCallback(object state)
  {
    if (this._state != JobState.Waiting)
      return;
    try
    {
      this._state = JobState.Running;
      try
      {
        this._job.Execute();
      }
      catch (Exception ex)
      {
        this._exception = ex;
      }
      if (this._state == JobState.Cancelled)
        return;
      this._state = this._exception == null ? JobState.Complete : JobState.Failed;
    }
    finally
    {
      if (this.Complete != null)
        this.Complete(this);
    }
  }

  /// <summary>
  /// Делегат метода, который вызывается при завершении выполнения
  /// задания.
  /// </summary>
  public delegate void CompleteCallback(ThreadPoolJobInfo jobInfo);
}
