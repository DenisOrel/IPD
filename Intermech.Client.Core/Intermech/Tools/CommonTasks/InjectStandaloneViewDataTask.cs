
// Type: Intermech.Tools.CommonTasks.InjectStandaloneViewDataTask
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Diagnostics;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.StandaloneView;
using Intermech.Mvp;
using Intermech.Services;
using Intermech.Tools.Integrators;
using System;
using System.Collections.Generic;


namespace Intermech.Tools.CommonTasks;

public class InjectStandaloneViewDataTask : AlterUnchangeableObjectFilesTask
{
  private static readonly StandaloneViewOperationModifiers emptyModifiers = new StandaloneViewOperationModifiers();
  private IStandaloneViewSettingsService settingsService;
  private IOutputView outputViewService;
  private IPrepareForViewDocumentFilesService prepareForViewService;
  private int objectTypeId;
  private IntegratorObject integrator;
  private IStandaloneViewService injectSvc;

  /// <summary>Создает объект.</summary>
  /// <param name="standaloneViewSettingsService">Сервис настроек автономного просмотра</param>
  /// <param name="outputViewService">Сервис окна вывод сообщений</param>
  /// <param name="prepareForViewService">Сервис подготовки локальных файлов документов к просмотру или печати</param>
  public InjectStandaloneViewDataTask(
    IStandaloneViewSettingsService standaloneViewSettingsService,
    IOutputView outputViewService,
    IPrepareForViewDocumentFilesService prepareForViewService)
  {
    if (standaloneViewSettingsService == null)
      throw new ArgumentNullException(nameof (standaloneViewSettingsService));
    if (outputViewService == null)
      throw new ArgumentNullException(nameof (outputViewService));
    if (prepareForViewService == null)
      throw new ArgumentNullException(nameof (prepareForViewService));
    this.settingsService = standaloneViewSettingsService;
    this.outputViewService = outputViewService;
    this.prepareForViewService = prepareForViewService;
  }

  protected override bool DoInitialize()
  {
    this.objectTypeId = DBHelper.GetObjectType(this.ObjectId);
    this.integrator = IntegratorServices.Find(this.objectTypeId);
    if (this.integrator == null)
      return false;
    this.injectSvc = IntegratorServices.GetService<IStandaloneViewService>(this.integrator, false);
    return this.injectSvc != null && base.DoInitialize();
  }

  protected override void DoClear()
  {
    base.DoClear();
    this.objectTypeId = -1;
    this.integrator = (IntegratorObject) null;
    this.injectSvc = (IStandaloneViewService) null;
  }

  protected override void DoAlterFile()
  {
    base.DoAlterFile();
    Tuple<StandaloneViewObjectTypeSettings, StandaloneViewOperationModifiers> viewDataSettings = this.GetInjectViewDataSettings(this.objectTypeId);
    StandaloneViewServiceResult result = this.injectSvc.InjectViewData(new StandaloneViewDataInjectionParameters()
    {
      ObjectId = this.ObjectId,
      FileName = this.FileName,
      FilePath = this.FilePath,
      ObjectTypeSettings = viewDataSettings.Item1,
      InjectSignNamesOnly = viewDataSettings.Item2.InjectSignNamesOnly
    });
    if (result.IsSuccessful)
      this.InvokeExternalPrepareDocumentFileHandlers(result);
    if (result.IsSuccessful)
      AlteredFilesService.Default.ReportAlteredFile(this.FilePath);
    else
      new ErrorReporterAdapter((IMessageReporter) new MultilineMessageReporter((IMessageReporter) new OutputViewMessageReporter(this.outputViewService, "Ошибки")))
      {
        CaptionGenerator = ((Func<ICollection<ErrorInfo>, string>) (errors => $"При записи в файл документа с ид.версии={this.ObjectId} сведений о подписях документа, атрибутов документа, контрольной суммы файла произошла одна или более ошибок."))
      }.ReportErrors(result.Errors);
  }

  private void InvokeExternalPrepareDocumentFileHandlers(StandaloneViewServiceResult result)
  {
    try
    {
      ((PrepareForViewDocumentFilesService) this.prepareForViewService).RaisePrepareDocumentFile(this.ObjectId, this.objectTypeId, this.FileName, this.FilePath);
    }
    catch (Exception ex)
    {
      result.Errors.Add(ErrorInfo.FromException(ex));
    }
  }

  private Tuple<StandaloneViewObjectTypeSettings, StandaloneViewOperationModifiers> GetInjectViewDataSettings(
    int objectTypeId)
  {
    StandaloneViewObjectTypeSettings effectiveSettings = this.settingsService.GetEffectiveSettings(objectTypeId);
    return StandaloneViewVars.AdjustSettingsInDialogMode.Value ? this.AdjustInjectViewDataSettings(effectiveSettings) : Tuple.Create<StandaloneViewObjectTypeSettings, StandaloneViewOperationModifiers>(effectiveSettings, InjectStandaloneViewDataTask.emptyModifiers);
  }

  private Tuple<StandaloneViewObjectTypeSettings, StandaloneViewOperationModifiers> AdjustInjectViewDataSettings(
    StandaloneViewObjectTypeSettings settings)
  {
    StandaloneViewAdjustmentOptions adjustmentOptions = new StandaloneViewAdjustmentOptions();
    adjustmentOptions.SetAll(true);
    StandaloneViewOperationModifiers operationModifiers = new StandaloneViewOperationModifiers();
    MvpContext.ViewService.ShowModal((IPresenter) new StandaloneViewOptionsPresenter()
    {
      AdjustmentOptions = adjustmentOptions,
      OperationModifiers = operationModifiers
    });
    if (!adjustmentOptions.IsFullyEnabled())
    {
      settings = settings.Clone();
      if (!adjustmentOptions.EnableInjectSigns)
        settings.InjectSigns = new bool?(false);
      if (!adjustmentOptions.EnableInjectFileChecksum)
        settings.InjectFileChecksum = new bool?(false);
      if (!adjustmentOptions.EnableInjectAttributes)
        settings.InjectedAttributes.Enabled = false;
    }
    return Tuple.Create<StandaloneViewObjectTypeSettings, StandaloneViewOperationModifiers>(settings, operationModifiers);
  }
}
