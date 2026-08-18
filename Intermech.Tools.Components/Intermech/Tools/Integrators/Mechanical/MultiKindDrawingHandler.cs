// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.MultiKindDrawingHandler
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.ControlFlow;
using Intermech.ControlFlow.Cooperative;
using Intermech.Data.SectionEntities;
using Intermech.Localization;
using Intermech.Tools.DataExchange;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

internal class MultiKindDrawingHandler : CooperativeAction
{
  private readonly MechanicalDriver driver;
  private readonly CaptureChangesDriverContext ctx;
  private readonly SectionEntity docItem;
  private DocumentScheduleAdapter scheduleAdapter;
  private ObjectSection docObj;
  private FilesSection docFiles;
  private IDocumentCADApiService docApiService;
  private bool dependenciesComplete;

  /// <summary>Создает объект.</summary>
  /// <param name="ctx">Рабочий контекст</param>
  /// <param name="docItem">Рабочий элемент для обрабатываемого документа</param>
  /// <exception cref="T:System.ArgumentNullException">Ошибка в аргументах метода</exception>
  public MultiKindDrawingHandler(
    MechanicalDriver driver,
    CaptureChangesDriverContext ctx,
    SectionEntity docItem)
    : base(ctx.Scheduler)
  {
    if (driver == null)
      throw new ArgumentNullException(nameof (driver));
    if (ctx == null)
      throw new ArgumentNullException(nameof (ctx));
    if (docItem == null)
      throw new ArgumentNullException(nameof (docItem));
    this.driver = driver;
    this.ctx = ctx;
    this.docItem = docItem;
  }

  public DocumentScheduleAdapter ScheduleAdapter
  {
    get => this.scheduleAdapter;
    set => this.scheduleAdapter = value;
  }

  /// <summary>
  /// Возвращает анализатор изменений документов CAD-системы.
  /// </summary>
  private MechanicalDriver MechanicalDriver => this.driver;

  protected sealed override object GetUIReportOperationId() => (object) this.docItem;

  protected override IEnumerable<CooperativeState> Coroutine()
  {
    this.ValidateProperties();
    this.InitializeHandler();
    if (this.docObj.ObjectType == -1)
      this.SelectNewDocumentType(true);
    this.ProcessDependencies();
    this.dependenciesComplete = true;
    if (this.docObj.ObjectType == -1)
    {
      yield return this.Wait((IWaitObject) this.ctx.Scheduler.CreateImmediateCheckpoint());
      this.SelectNewDocumentType(false);
    }
    DocumentHandler documentHandler = (DocumentHandler) this.MechanicalDriver.CreateDocumentHandler(this.docItem);
    documentHandler.SkipDependencies();
    this.ctx.Scheduler.AddTask((IAction) documentHandler);
  }

  private void ValidateProperties()
  {
    if (this.scheduleAdapter == null)
      throw new DataExchangeConfigurationException("ScheduleAdapter");
  }

  /// <summary>Выполняет инициализацию обработчика.</summary>
  private void InitializeHandler()
  {
    this.docObj = this.docItem.Sections.Get<ObjectSection>();
    this.docFiles = this.docItem.Sections.Get<FilesSection>();
    this.docApiService = this.MechanicalDriver.GetDocumentApiService(this.docItem);
  }

  /// <summary>
  /// Выполняет обработку файловых зависимостей документа. По каждой зависимости в базе данных анализатора создается объект и назначается обработчик.
  /// </summary>
  private void ProcessDependencies()
  {
    this.docApiService.TryGetFileDependenciesHandler(this.docItem)?.Run(this.docItem);
  }

  private void SelectNewDocumentType(bool allowFail)
  {
    SelectedObjectType selectedObjectType = this.SelectNewDocumentType();
    if (selectedObjectType == null)
    {
      if (!allowFail)
        throw new InvalidOperationException(LocalizationHolder.rm.GetString("Tools.Components_410"));
    }
    else
    {
      this.docObj.ObjectType = selectedObjectType.ObjectType;
      if (!selectedObjectType.RequireCheck)
        return;
      this.docObj.RequireTypeCheck = true;
    }
  }

  /// <summary>Определяет тип документа при импорте в IPS.</summary>
  /// <returns>Тип документа</returns>
  private SelectedObjectType SelectNewDocumentType()
  {
    MechanicalDocumentKind? nullable = this.MechanicalDriver.TryGetMechanicalDocumentKind(this.docItem);
    if (!nullable.HasValue)
    {
      if (!this.dependenciesComplete)
        return (SelectedObjectType) null;
      bool flag = false;
      foreach (string dependency in (OrderedList<string>) this.docFiles.Dependencies)
      {
        MechanicalDocumentKind? mechanicalDocumentKind = this.MechanicalDriver.TryGetMechanicalDocumentKind(FilesSection.FindByMasterFile(this.ctx.Database, dependency) ?? throw new InvalidOperationException());
        if (mechanicalDocumentKind.HasValue && mechanicalDocumentKind.Value == MechanicalDocumentKind.AssemblyModel)
        {
          flag = true;
          break;
        }
      }
      nullable = new MechanicalDocumentKind?(flag ? MechanicalDocumentKind.AssemblyDrawing : MechanicalDocumentKind.PartDrawing);
    }
    return new SelectedObjectType(this.MechanicalDriver.GetTypesByMechanicalDocumentKind(nullable.Value)[0].Id, true);
  }
}
