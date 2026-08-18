// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.DraftDocumentHandler
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Data.SectionEntities;
using Intermech.Files;
using Intermech.Interfaces.Data;
using Intermech.IO;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.UI;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>Обработчик для черновиков документов.</summary>
internal sealed class DraftDocumentHandler : IAction
{
  private DocumentCaptureChangesDriver driver;
  private CaptureChangesDriverContext ctx;
  private SectionEntity docItem;
  private IDCache idCache;
  private IFileVault fileVault;

  /// <summary>Создает объект.</summary>
  /// <param name="driver">Стратегия анализа изменений</param>
  /// <param name="ctx">Рабочий контекст</param>
  /// <param name="docItem">Рабочий элемент для обрабатываемого документа</param>
  /// <param name="idCache">Кэш метаданных</param>
  /// <param name="fileVault">Файловый сервис IPS</param>
  /// <param name="draftDocumentsService">Сервис черновиков документов</param>
  /// <exception cref="T:System.ArgumentNullException">Ошибка в аргументах метода</exception>
  public DraftDocumentHandler(
    DocumentCaptureChangesDriver driver,
    CaptureChangesDriverContext ctx,
    SectionEntity docItem,
    IDCache idCache,
    IFileVault fileVault)
  {
    if (driver == null)
      throw new ArgumentNullException(nameof (driver));
    if (ctx == null)
      throw new ArgumentNullException(nameof (ctx));
    if (docItem == null)
      throw new ArgumentNullException(nameof (docItem));
    if (idCache == null)
      throw new ArgumentNullException(nameof (idCache));
    if (fileVault == null)
      throw new ArgumentNullException(nameof (fileVault));
    this.driver = driver;
    this.ctx = ctx;
    this.docItem = docItem;
    this.idCache = idCache;
    this.fileVault = fileVault;
  }

  private DocumentCaptureChangesDriver Driver
  {
    [DebuggerStepThrough] get => this.driver;
  }

  public void Perform()
  {
    this.StartUIReportOperation();
    try
    {
      this.PerformCore();
    }
    finally
    {
      this.StopUIReportOperation();
    }
  }

  private void PerformCore()
  {
    this.EnsureDBObjectExists();
    this.ProcessAttributes();
    this.Driver.Operations.Db.EmitUIActions(this.ctx, this.docItem);
  }

  private void EnsureDBObjectExists()
  {
    if (!this.docItem.Sections.Get<ObjectSection>().NewObject)
      return;
    this.Driver.Operations.Db.CreateBlankObject(this.ctx, this.docItem);
  }

  private void ProcessAttributes()
  {
    ObjectSection objectSection = this.docItem.Sections.Get<ObjectSection>();
    if (!objectSection.NewObject)
      return;
    string relativePath = PathUtils.GetRelativePath(this.docItem.Sections.Get<DraftDocumentSection>().ExternalFilePath, this.fileVault.WorkArea.AreaPath, RelativePathOptions.ThrowIfNotPossible);
    AttributesSection sectionObject = new AttributesSection();
    this.docItem.Sections.Set((object) sectionObject);
    this.Driver.Operations.Db.FetchObjectAttributes(this.docItem, (IDBAttributableTypeRef) new DirectObjectAttributesRef(objectSection.ObjectType));
    sectionObject.DatabaseSet.TryUpdate((StringKey) this.idCache.Name.Text, (object) $"{relativePath} (заготовка для файла)");
    sectionObject.DatabaseSet.TryUpdate((StringKey) this.Driver.Operations.DraftDocuments.Service.IdCache.ExternalFilePath.Text, (object) relativePath, true);
    this.Driver.Operations.Db.EmitObjectAttributesServerActions(this.docItem);
  }

  private void StartUIReportOperation()
  {
    if (!UIReport.Enabled)
      return;
    UIReport.StartLogicalOperation((object) this.docItem);
  }

  private void StopUIReportOperation()
  {
    if (!UIReport.Enabled)
      return;
    UIReport.StopLogicalOperation((object) this.docItem);
  }
}
