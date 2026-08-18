
// Type: Intermech.Navigator.Controls.ThreadPoolJobManager
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections;
using System.Threading;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Реализует менеджер, распределяющий задания по фоновым потокам с
/// помощью класса ThreadPool.
/// </summary>
internal class ThreadPoolJobManager : IJobManager
{
  private IList _jobs;

  public ThreadPoolJobManager() => this._jobs = (IList) new ArrayList();

  /// <summary>Ставит новое задание в очередь на выполнение.</summary>
  /// <param name="job">Задание, которое должно быть выполнено в фоновом потоке.</param>
  /// <param name="marker">Неуникальная метка, присваиваемая заданию.</param>
  public void Queue(IJob job, object marker)
  {
    ThreadPoolJobInfo threadPoolJobInfo = new ThreadPoolJobInfo(job, marker);
    threadPoolJobInfo.Complete += new ThreadPoolJobInfo.CompleteCallback(this.JobComplete);
    lock (this._jobs)
      this._jobs.Add((object) threadPoolJobInfo);
    ThreadPool.QueueUserWorkItem(new WaitCallback(threadPoolJobInfo.WaitCallback));
  }

  /// <summary>
  /// Отменяет выполнение всех заданий, чьи метки совпадают с указанной.
  /// </summary>
  /// <param name="marker">Метка заданий, выполнение которых должно быть отменено.</param>
  public void Cancel(object marker)
  {
    lock (this._jobs)
    {
      for (int index = 0; index < this._jobs.Count; ++index)
      {
        ThreadPoolJobInfo job = (ThreadPoolJobInfo) this._jobs[index];
        if (job.Marker != null)
        {
          if (job.Marker.Equals(marker))
            job.Cancel();
        }
        else if (marker == null)
          job.Cancel();
      }
    }
  }

  /// <summary>Отменяет выполнение всех заданий.</summary>
  public void Cancel()
  {
    lock (this._jobs)
    {
      for (int index = 0; index < this._jobs.Count; ++index)
        ((ThreadPoolJobInfo) this._jobs[index]).Cancel();
    }
  }

  /// <summary>
  /// Событие, наступающее при завершении каждого фонового задания.
  /// Срабатывает в контексте фонового потока, в котором выполнялось
  /// задание.
  /// </summary>
  public event JobCompleteEventHandler Complete;

  /// <summary>
  /// Вызывается из потока задания при завершении его выполнения.
  /// </summary>
  /// <param name="jobInfo"></param>
  private void JobComplete(ThreadPoolJobInfo jobInfo)
  {
    lock (this._jobs)
      this._jobs.Remove((object) jobInfo);
    if (this.Complete != null)
      this.Complete((JobInfo) jobInfo);
    jobInfo.Complete -= new ThreadPoolJobInfo.CompleteCallback(this.JobComplete);
  }
}
