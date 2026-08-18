// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.DocumentScheduleAdapter
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow.Cooperative;
using Intermech.Tools.DataExchange;
using System;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Используется для адаптации последовательности шагов конкретного планировщика к нуждам
/// обработчика документов на основе класса DocumentHandlerBase.
/// </summary>
/// <remarks>
/// Позволяет инвертировать зависимость обработчика документа от внутреннего устройства
/// рабочего контекста.
/// </remarks>
public class DocumentScheduleAdapter
{
  private ManualResetEvent relationsStage;
  private ManualResetEvent diskWritesStage;
  private ManualResetEvent uploadFilesStage;
  private ManualResetEvent uiStage;

  public static DocumentScheduleAdapter FromStandardScheduler(
    StandardSchedulerStages schedulerStages)
  {
    if (schedulerStages == null)
      throw new ArgumentNullException(nameof (schedulerStages));
    return new DocumentScheduleAdapter()
    {
      RelationsStage = schedulerStages.RelationsStage,
      DiskWritesStage = schedulerStages.DiskWritesStage,
      UploadFilesStage = schedulerStages.UploadFilesStage,
      UIStage = schedulerStages.UIStage
    };
  }

  /// <summary>Шаг обработки связей между документами.</summary>
  public ManualResetEvent RelationsStage
  {
    get => this.relationsStage;
    set => this.relationsStage = value;
  }

  /// <summary>Запись сделанных изменений в файлы документа.</summary>
  public ManualResetEvent DiskWritesStage
  {
    get => this.diskWritesStage;
    set => this.diskWritesStage = value;
  }

  /// <summary>
  /// Анализ файлов документа, поиск новых/измененных файлов и планирование загрузки их в базу IPS.
  /// </summary>
  public ManualResetEvent UploadFilesStage
  {
    get => this.uploadFilesStage;
    set => this.uploadFilesStage = value;
  }

  /// <summary>Планирование обновления интерфейса пользователя.</summary>
  public ManualResetEvent UIStage
  {
    get => this.uiStage;
    set => this.uiStage = value;
  }
}
