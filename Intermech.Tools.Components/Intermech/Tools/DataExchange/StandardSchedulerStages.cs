// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.StandardSchedulerStages
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow.Cooperative;
using System;

#nullable disable
namespace Intermech.Tools.DataExchange;

/// <summary>
/// Содержит типовую последовательность этапов выполнения типового обработчика.
/// </summary>
public sealed class StandardSchedulerStages
{
  private readonly CooperativeScheduler scheduler;
  private readonly ManualResetEvent derivedObjectsStage;
  private readonly ManualResetEvent relationsStage;
  private readonly ManualResetEvent diskWritesStage;
  private readonly ManualResetEvent uploadFilesStage;
  private readonly ManualResetEvent uiStage;

  /// <summary>Создает объект.</summary>
  public StandardSchedulerStages(CooperativeScheduler scheduler)
  {
    this.scheduler = scheduler != null ? scheduler : throw new ArgumentNullException(nameof (scheduler));
    this.derivedObjectsStage = new ManualResetEvent(scheduler);
    this.relationsStage = new ManualResetEvent(scheduler);
    this.diskWritesStage = new ManualResetEvent(scheduler);
    this.uploadFilesStage = new ManualResetEvent(scheduler);
    this.uiStage = new ManualResetEvent(scheduler);
    this.scheduler.AppendCheckpoint(this.derivedObjectsStage);
    this.scheduler.AppendCheckpoint(this.relationsStage);
    this.scheduler.AppendCheckpoint(this.diskWritesStage);
    this.scheduler.AppendCheckpoint(this.uploadFilesStage);
    this.scheduler.AppendCheckpoint(this.uiStage);
  }

  /// <summary>Возвращает объект планировщика.</summary>
  public CooperativeScheduler Scheduler => this.scheduler;

  /// <summary>Шаг обработчик производных объектов (изделий и т.п.)</summary>
  public ManualResetEvent DerivedObjectsStage => this.derivedObjectsStage;

  /// <summary>Шаг обработки связей между документами.</summary>
  public ManualResetEvent RelationsStage => this.relationsStage;

  /// <summary>Запись сделанных изменений в файлы документа.</summary>
  public ManualResetEvent DiskWritesStage => this.diskWritesStage;

  /// <summary>
  /// Анализ файлов документа, поиск новых/измененных файлов и планирование загрузки их в базу IPS.
  /// </summary>
  public ManualResetEvent UploadFilesStage => this.uploadFilesStage;

  /// <summary>Планирование обновления интерфейса пользователя.</summary>
  public ManualResetEvent UIStage => this.uiStage;
}
