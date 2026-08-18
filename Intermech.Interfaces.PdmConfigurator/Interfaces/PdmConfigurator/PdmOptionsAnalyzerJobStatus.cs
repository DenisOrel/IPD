// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.PdmOptionsAnalyzerJobStatus
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using System;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>Состояние задания по анализу объектов с опциями</summary>
[Serializable]
public sealed class PdmOptionsAnalyzerJobStatus : ICloneable
{
  /// <summary>Количество объектов, добавленных для изучения</summary>
  public long Objects;
  /// <summary>Индикатор выполнения задания</summary>
  public PdmOptionsAnalyzerJobProgress Progress;
  /// <summary>
  /// Обрабатываемый объект и его состав.
  /// Значение будет заполнено только когда задание будет успешно выполнено
  /// </summary>
  public PdmAnalyzedOptionObjects Items;
  /// <summary>
  /// Исключение, которое возникло в процессе изучения опций объектов
  /// </summary>
  public Exception Exception;

  /// <summary>Создать экземпляр класса</summary>
  public PdmOptionsAnalyzerJobStatus()
  {
    this.Objects = 0L;
    this.Progress = PdmOptionsAnalyzerJobProgress.NotStarted;
    this.Items = (PdmAnalyzedOptionObjects) null;
    this.Exception = (Exception) null;
  }

  /// <summary>Установить поля класса в "Задание стартовало"</summary>
  public void Start()
  {
    this.Objects = 0L;
    this.Progress = PdmOptionsAnalyzerJobProgress.NotStarted;
    this.Items = (PdmAnalyzedOptionObjects) null;
    this.Exception = (Exception) null;
  }

  /// <summary>Установить поля класса в "Задание прервано"</summary>
  public void Cancel()
  {
    this.Progress = PdmOptionsAnalyzerJobProgress.Cancelled;
    this.Exception = (Exception) null;
  }

  /// <summary>
  /// Установить поля класса в "Задание остановлено из-за ошибки"
  /// </summary>
  /// <param name="exception">Возникшее исключение</param>
  /// <param name="items">Обрабатываемый объект</param>
  public void Error(Exception exception, PdmAnalyzedOptionObjects items)
  {
    this.Progress = PdmOptionsAnalyzerJobProgress.Error;
    this.Items = items;
    this.Exception = exception;
  }

  /// <summary>Установить поля класса в "Задание успешно выполнено"</summary>
  /// <param name="objects">Количество добавленных описаний объектов</param>
  /// <param name="items">Обработанный объект</param>
  public void Complete(int objects, PdmAnalyzedOptionObjects items)
  {
    this.Objects = (long) objects;
    this.Progress = PdmOptionsAnalyzerJobProgress.Completed;
    this.Items = items;
    this.Exception = (Exception) null;
  }

  /// <summary>Создать копию экземпляра класса</summary>
  /// <returns>Копия экземпляра класса</returns>
  public object Clone()
  {
    return (object) new PdmOptionsAnalyzerJobStatus()
    {
      Objects = this.Objects,
      Progress = this.Progress,
      Items = this.Items,
      Exception = this.Exception
    };
  }
}
