
// Type: Intermech.Navigator.Controls.JobInfo
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Содержит сведения о результатах выполнения задания в
/// фоновом потоке.
/// </summary>
internal class JobInfo
{
  protected IJob _job;
  protected object _marker;
  protected JobState _state;
  protected Exception _exception;

  public JobInfo(IJob job, object marker)
  {
    this._job = job;
    this._marker = marker;
    this._state = JobState.Waiting;
    this._exception = (Exception) null;
  }

  /// <summary>
  /// Возвращает задание, которое было выполнено в фоновом потоке.
  /// </summary>
  public IJob Job => this._job;

  /// <summary>
  /// Возвращает маркер, который был присвоен заданию при постановке
  /// в очередь на выполнение.
  /// </summary>
  public object Marker => this._marker;

  /// <summary>Возврашает состояние задания.</summary>
  public JobState State => this._state;

  /// <summary>
  /// Возврашает исключение, которое возникло в результате выполнения задания.
  /// Это свойство отлично от null только тогда, когда значение свойства State
  /// равно Failed.
  /// </summary>
  public Exception Exception => this._exception;
}
