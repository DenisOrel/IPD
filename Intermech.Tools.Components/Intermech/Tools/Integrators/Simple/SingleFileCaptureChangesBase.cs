// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Simple.SingleFileCaptureChangesBase
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Data.SectionEntities;
using Intermech.Tools.DataExchange;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.Simple;

/// <summary>
/// Реализует драйвер захвата изменений для простых документов, состоящих только из мастер-файла и
/// дополнительных файлов и не требующих обмена атрибутами.
/// </summary>
public abstract class SingleFileCaptureChangesBase : DocumentCaptureChangesDriver
{
  protected StandardSchedulerStages schedulerStages;

  protected override void ClearDriver()
  {
    base.ClearDriver();
    this.schedulerStages = (StandardSchedulerStages) null;
  }

  /// <summary>
  /// Инициализирует сервисы драйвера, которым требуется контекст текущего вызова драйвера. В момент вызова этого метода свойство <see cref="P:DriverContext" /> уже заполнено.
  /// </summary>
  protected override void InitializeDriverContextServices()
  {
    base.InitializeDriverContextServices();
    this.schedulerStages = new StandardSchedulerStages(this.DriverContext.Scheduler);
  }

  /// <summary>Позволяет открыть документ.</summary>
  /// <param name="documentItem">Элемент документа в базе данных контекста</param>
  /// <param name="fullPath">Абсолютный путь к файлу документа</param>
  /// <returns>Открытый документ</returns>
  /// <exception cref="T:ArgumentNullException">documentItem || fullPath</exception>
  public sealed override DocumentFileData OpenDocumentFile(
    SectionEntity documentItem,
    string fullPath)
  {
    if (documentItem == null)
      throw new ArgumentNullException(nameof (documentItem));
    DocumentFileData documentFile = fullPath != null ? new DocumentFileData(fullPath) : throw new ArgumentNullException(nameof (fullPath));
    this.OpenDocument(documentFile);
    return documentFile;
  }

  protected virtual void OpenDocument(DocumentFileData documentFile)
  {
  }

  protected sealed override void DoDetachItem(SectionEntity dbItem)
  {
    base.DoDetachItem(dbItem);
    this.CloseDocument(dbItem);
  }

  protected virtual void CloseDocument(SectionEntity docItem)
  {
  }

  protected override void SetupDocumentHandler(SectionEntity docItem, IAction documentHandler)
  {
    base.SetupDocumentHandler(docItem, documentHandler);
    if (!(documentHandler is DocumentHandlerBase documentHandlerBase))
      return;
    documentHandlerBase.ScheduleAdapter = DocumentScheduleAdapter.FromStandardScheduler(this.schedulerStages);
  }
}
