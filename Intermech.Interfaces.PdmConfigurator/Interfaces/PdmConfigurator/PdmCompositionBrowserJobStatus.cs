// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.PdmCompositionBrowserJobStatus
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using System;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Состояние задания по раскрутке конфигурируемого состава
/// </summary>
[Serializable]
public sealed class PdmCompositionBrowserJobStatus : ICloneable
{
  /// <summary>
  /// Результат выполнения задания - протокол подбора объектов конфигуратором составов
  /// </summary>
  public TraceLog Trace;
  /// <summary>Индикатор выполнения задания</summary>
  public PdmCompositionBrowserJobProgress Progress;
  /// <summary>
  /// Исключение, которое возникло в процессе изучения опций объектов
  /// </summary>
  public Exception Exception;

  /// <summary>Создать экземпляр класса</summary>
  public PdmCompositionBrowserJobStatus()
  {
    this.Progress = PdmCompositionBrowserJobProgress.NotStarted;
    this.Exception = (Exception) null;
    this.Trace = (TraceLog) null;
  }

  /// <summary>Установить поля класса в "Задание стартовало"</summary>
  public void Start()
  {
    this.Progress = PdmCompositionBrowserJobProgress.Working;
    this.Exception = (Exception) null;
    this.Trace = (TraceLog) null;
  }

  /// <summary>Установить поля класса в "Задание прервано"</summary>
  public void Cancel()
  {
    this.Progress = PdmCompositionBrowserJobProgress.Cancelled;
    this.Exception = (Exception) null;
  }

  /// <summary>
  /// Установить поля класса в "Задание остановлено из-за ошибки"
  /// </summary>
  /// <param name="exception">Возникшее исключение</param>
  /// <param name="trace">Результат работы</param>
  public void Error(Exception exception, TraceLog trace)
  {
    this.Progress = PdmCompositionBrowserJobProgress.Error;
    this.Trace = trace;
    this.Exception = exception;
    if (this.Trace == null)
      return;
    this.Trace.Pack();
  }

  /// <summary>Установить поля класса в "Задание успешно выполнено"</summary>
  /// <param name="trace">Результат работы</param>
  public void Complete(TraceLog trace)
  {
    this.Trace = trace;
    this.Progress = PdmCompositionBrowserJobProgress.Completed;
    this.Exception = (Exception) null;
    if (this.Trace == null)
      return;
    this.Trace.Pack();
  }

  /// <summary>Создать копию экземпляра класса</summary>
  /// <returns>Копия экземпляра класса</returns>
  public object Clone()
  {
    return (object) new PdmCompositionBrowserJobStatus()
    {
      Trace = (this.Trace != null ? this.Trace.Clone() as TraceLog : (TraceLog) null),
      Progress = this.Progress,
      Exception = this.Exception
    };
  }
}
