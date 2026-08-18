// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.ExtendedSaveService`1
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.ControlFlow;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Contexts;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Runtime;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Реализует базовый класс для создания сервиса расширенного сохранения документов.
/// </summary>
/// <typeparam name="TSettingsService">Тип сервиса настроек интегратора</typeparam>
public abstract class ExtendedSaveService<TSettingsService> : IntegratorService, IExtendedSaveSupport
  where TSettingsService : IIntegratorSettingsService
{
  private static readonly DynamicVariable<CurrentEditingContext> EditContextVar = new DynamicVariable<CurrentEditingContext>("ExterndedSaveService.EditContextVar", (CurrentEditingContext) null);
  protected readonly CaptureChangesManager captureManager;
  private readonly ToolServiceReportBuilder uiReporter;
  private readonly DataExchangeHelper dataExchangeHelper;
  private TSettingsService settingsService;
  private IntegratorSettingsCache<ICollection<LocalId<int>>> supportedDocumentTypesCache;

  /// <summary>Создает объект.</summary>
  /// <param name="owner">Владелец компонента</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на владельца компонента не может быть null</exception>
  protected ExtendedSaveService(IIntegrator owner)
    : base(owner)
  {
    this.captureManager = new CaptureChangesManager();
    this.uiReporter = new ToolServiceReportBuilder();
    this.dataExchangeHelper = new DataExchangeHelper();
  }

  /// <summary>
  /// Возвращает или задает ссылку на сервис настроек интегратора. Свойство должно быть заполнено до начала использования текущего сервиса.
  /// </summary>
  public TSettingsService SettingsService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.settingsService;
    }
    set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.settingsService = value;
      }
    }
  }

  /// <summary>
  /// Проверяет конфигурацию сервиса и выполняет его окончательную инициализацию.
  /// После успешного выполнения этого метода сервис интегратора можно использовать.
  /// </summary>
  /// <exception cref="T:InvalidOperationException">Конфигурация сервиса некорректна</exception>
  protected override void DoInitialize()
  {
    base.DoInitialize();
    if ((object) this.SettingsService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "SettingsService");
    this.supportedDocumentTypesCache = new IntegratorSettingsCache<ICollection<LocalId<int>>>((IIntegratorSettingsService) this.settingsService, new Func<ICollection<LocalId<int>>>(this.CollectSupportedDocumentTypes));
  }

  /// <summary>
  /// Возвращает коллекцию типов документов, которые поддерживают расширенное сохранение.
  /// </summary>
  /// <returns>Коллекция идентификаторов типов документов</returns>
  public ICollection<LocalId<int>> GetSupportedDocumentTypes()
  {
    this.RequireReadyState();
    lock (this.Integrator.SyncRoot)
      return this.supportedDocumentTypesCache.Value;
  }

  /// <summary>
  /// Собирает коллекцию типов документов, которые поддерживают расширенное сохранение.
  /// </summary>
  /// <returns>Коллекция идентификаторов типов документов</returns>
  protected virtual IList<LocalId<int>> CollectSupportedDocumentTypes()
  {
    return (IList<LocalId<int>>) new List<LocalId<int>>(32 /*0x20*/);
  }

  /// <summary>
  /// Захватывает и сохраняет в рабочую копию объекта изменения, сделанные пользователем в
  /// файловой копии объекта.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="options">Опции выполнения операции</param>
  /// <exception cref="T:System.ArgumentException">Параметр <paramref name="objectId" /> не задан</exception>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="options" /> не должен быть равен null</exception>
  public ExtendedSaveResult CaptureChanges(long objectId, ExtendedSaveOptions options)
  {
    if (objectId == 0L)
      throw new ArgumentException("Не задан идентификатор версии объекта IPS.", nameof (objectId));
    if (options == null)
      throw new ArgumentNullException(nameof (options));
    this.RequireReadyState();
    if (objectId > 0L)
      throw new FaultException(string.Format(LocalizationHolder.rm.GetString("Tools.Components_485"), (object) DBHelper.GetObjectCaption(objectId)));
    if (!this.dataExchangeHelper.ShouldCaptureChanges(objectId))
      return new ExtendedSaveResult(false, (List<long>) null, (List<string>) null);
    lock (this.Integrator.SyncRoot)
    {
      ICaptureChangesDriver captureChangesDriver = this.GetCaptureChangesDriver();
      if (captureChangesDriver == null)
        throw new InvalidOperationException("No capture driver found.");
      if (this.captureManager.Driver == null)
        this.captureManager.Driver = captureChangesDriver;
      this.LicenseService.Check();
      using (this.TryLockEditContext())
      {
        IPercentageProgressSink percentageProgressSink = options.ProgressSink ?? ProgressSinks.NullPercentageSink;
        double fileProgressRange = this.dataExchangeHelper.GetMainFileProgressRange(this.Integrator);
        CaptureChangesResult captureChangesResult;
        using (UIReport.CreateScope())
        {
          try
          {
            if (UIReport.Enabled)
              this.uiReporter.ReportStart(string.Format(LocalizationHolder.rm.GetString("Tools.Components_391"), (object) DBHelper.GetObjectCaption(objectId)));
            this.OnBeforeCaptureChanges(objectId);
            this.SetCaptureChangesParameters(objectId, options);
            percentageProgressSink.SetState("Расширенное сохранение документа");
            captureChangesResult = this.captureManager.CaptureChanges(this.CreateActionParameters(objectId, options, percentageProgressSink.CreateNestedSink(fileProgressRange)));
            if (captureChangesResult != null)
              this.OnAfterCaptureChanges(captureChangesResult);
            if (UIReport.Enabled)
              this.uiReporter.ReportSuccess();
          }
          catch (Exception ex)
          {
            if (UIReport.Enabled)
              this.uiReporter.ReportFail(ex);
            throw;
          }
          finally
          {
            this.ResetCaptureChangesParameters();
          }
        }
        percentageProgressSink.SetState("Импорт новых ссылочных зависимостей");
        if (captureChangesResult != null)
          this.dataExchangeHelper.ImportDeferredDraftDocuments(captureChangesResult, percentageProgressSink.CreateNestedSink(100.0 - fileProgressRange));
        if (captureChangesResult != null)
          this.OnPostProcessCaptureChanges(captureChangesResult);
        percentageProgressSink.SetState(string.Empty);
        percentageProgressSink.SetProgress(100.0);
        return new ExtendedSaveResult(true, captureChangesResult?.ChangedObjectIds, captureChangesResult?.Errors, true);
      }
    }
  }

  private CaptureChangesActionParameters CreateActionParameters(
    long objectId,
    ExtendedSaveOptions options,
    IPercentageProgressSink progressSink)
  {
    return new CaptureChangesActionParameters()
    {
      ObjectId = objectId,
      ProgressSink = progressSink
    };
  }

  /// <summary>
  /// Возвращает экземпляр драйвера для захвата изменений в документах интегрируемого приложения. Метод обязательно должен вернуть созданный объект.
  /// </summary>
  /// <returns>Объект драйвера</returns>
  protected abstract ICaptureChangesDriver GetCaptureChangesDriver();

  /// <summary>
  /// Устанавливает свойства драйвера, управляющие его поведением.
  /// </summary>
  /// <param name="objectId">Идентификатор документа</param>
  /// <param name="options">Опции выполнения</param>
  protected virtual void SetCaptureChangesParameters(long objectId, ExtendedSaveOptions options)
  {
  }

  /// <summary>
  /// Очищает свойства драйвера, управляющие его поведением.
  /// </summary>
  protected virtual void ResetCaptureChangesParameters()
  {
  }

  /// <summary>
  /// Вычисляет, нужно ли создавать/обновлять производные от документа изделия, в зависимости от выполняемой команды, пожеланий пользователя, а также
  /// наличия в базе IPS изделия у документа.
  /// </summary>
  /// <param name="objectId">Идентификатор сохраняемого документа</param>
  /// <param name="options">Опции выполнения</param>
  /// <returns>Признак, нужно ли создавать/обновлять изделия</returns>
  protected bool CalculateUpdateArticlesParameter(long objectId, ExtendedSaveOptions options)
  {
    if (objectId == 0L)
      throw new ArgumentException();
    if (options == null)
      throw new ArgumentNullException(nameof (options));
    if (options.CreateNewArticlesOnly && options.UpdateExistingArticlesOnly)
      return true;
    if (!options.CreateNewArticlesOnly && !options.UpdateExistingArticlesOnly)
      return false;
    return !this.HasArticlesInDB(objectId) ? options.CreateNewArticlesOnly : options.UpdateExistingArticlesOnly;
  }

  private bool HasArticlesInDB(long objectId)
  {
    if (objectId == 0L)
      throw new ArgumentException();
    VersionsRulePackage editorRule = VersionsRuleSources.GetEditorRule();
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = 1;
    paramSet.Columns = new object[2]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.F_OBJECT_TYPE
    };
    paramSet.Conditions = new ConditionStructure[2]
    {
      new ConditionStructure(IDCache.Default.BasedOnCADModel.Id, RelationalOperators.Equal, (object) true, LogicalOperators.OR, 0, true),
      new ConditionStructure(IDCache.Default.ObjectExternalKey.Id, RelationalOperators.NotEmpty, (object) null, LogicalOperators.NONE, 0, true)
    };
    DataTable dataTable;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(IDCache.Default.ArticleToDocumentTree.Id);
      relationCollection.FiltrationOwnerID = editorRule.OwnerId;
      relationCollection.ObjectTypeID = IDCache.Default.AllArticles.Id;
      dataTable = relationCollection.EntersInVersion(paramSet, objectId);
    }
    return dataTable.Rows.Count > 0;
  }

  private IDisposable TryLockEditContext()
  {
    CurrentEditingContext editContext = this.GetOrCreateEditContext();
    return editContext.ContextID != 0L ? (IDisposable) new CurrentEditingContextScope(editContext) : (IDisposable) null;
  }

  private CurrentEditingContext GetOrCreateEditContext()
  {
    if (!DynamicScope.ScopePresent)
      return this.CreateEditContext();
    if (ExtendedSaveService<TSettingsService>.EditContextVar.Value == null)
      ExtendedSaveService<TSettingsService>.EditContextVar.Value = this.CreateEditContext();
    return ExtendedSaveService<TSettingsService>.EditContextVar.Value;
  }

  private CurrentEditingContext CreateEditContext()
  {
    ICurrentUserAndRole service = ServiceUtils.GetService<ICurrentUserAndRole>((object) ServicesManager.ServiceContainer, true);
    CurrentEditingContext editingContext = new CurrentEditingContext(service.CachedEditingContextID, service.CachedEditingContextModificationID, service.CachedContextMode);
    if (editingContext.ContextID != 0L && editingContext.ContextMode != EditingContextMode.AutoUpdate)
    {
      switch (AutoUpdateContextWindow.Execute(editingContext.ContextID))
      {
        case DialogResult.OK:
          editingContext = editingContext.WithContextMode(EditingContextMode.AutoUpdate);
          break;
        case DialogResult.Abort:
          throw new AbortException();
      }
    }
    return editingContext;
  }

  /// <summary>Вызывается непосредственно перед захватом изменений.</summary>
  /// <param name="objectId">Идентификатор документа</param>
  protected virtual void OnBeforeCaptureChanges(long objectId)
  {
  }

  /// <summary>
  /// Вызывается после успешного захвата изменений. Этот метод не будет вызван, если при захвате изменений будет сброшено исключение.
  /// </summary>
  /// <param name="result">Результаты захвата изменений</param>
  protected virtual void OnAfterCaptureChanges(CaptureChangesResult result)
  {
    if (result == null)
      throw new ArgumentNullException(nameof (result));
  }

  /// <summary>
  /// Вызывается после успешного завершения команды "Расширенное сохранение" и используется для запуска связанных процессов, которые не являются
  /// частью команды.
  /// </summary>
  /// <param name="result">Результаты захвата изменений</param>
  protected virtual void OnPostProcessCaptureChanges(CaptureChangesResult result)
  {
    if (result == null)
      throw new ArgumentNullException(nameof (result));
  }
}
