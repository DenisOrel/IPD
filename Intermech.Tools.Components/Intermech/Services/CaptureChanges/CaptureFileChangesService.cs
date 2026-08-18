// Decompiled with JetBrains decompiler
// Type: Intermech.Services.CaptureChanges.CaptureFileChangesService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.Commands;
using Intermech.Interfaces.Client;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Simple;
using Intermech.UI;
using System;
using System.ComponentModel.Design;

#nullable disable
namespace Intermech.Services.CaptureChanges;

/// <summary>
/// Сервис для захвата изменений в файлах объектов IPS на диске и передачи этих изменений в базу IPS.
/// </summary>
/// <remarks>
/// <para>
/// Сервис используется командой "Сохранить изменения". Он реагирует только на те объекты IPS,
/// которые обрабатываются по общим правилам - через извлечение содержимого атрибута "Файл"
/// в рабочую область файлового хранилища пользователя. Специальные типы вроде "Спецификации",
/// "Извещения" и др. обрабатываются отдельно через перехват соответствующих команд навигатора.</para>
/// <para>
/// Реализация является thread safe.</para>
/// </remarks>
internal sealed class CaptureFileChangesService : ICaptureFileChangesService
{
  private static readonly IServiceProvider emptyContextServices = (IServiceProvider) new ServiceContainer();

  /// <summary>
  /// Захватывает и сохраняет в рабочую копию объекта изменения, сделанные пользователем в
  /// файлах объекта.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="mode">Режим сохранения изменений в объекте</param>
  /// <param name="contextServices">Контекст выполняемой операции. Параметр может быть не задан</param>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор версии объекта</exception>
  /// <exception cref="T:System.Exception">В процессе работы сервиса произошла ошибка</exception>
  public void CaptureChanges(long objectId, SaveChangesMode mode, IServiceProvider contextServices)
  {
    if (objectId == 0L)
      throw new ArgumentException("Не задан идентификатор версии объекта IPS.", nameof (objectId));
    if (contextServices == null)
      contextServices = CaptureFileChangesService.emptyContextServices;
    DBObjectTypeFileHandlingRules fileHandlingRules = IntegratorServices.GetFileHandlingRules(DBHelper.GetObjectType(objectId));
    if (!fileHandlingRules.RequireNormalEditMode)
      return;
    if (fileHandlingRules.IntegratorRef != null)
      this.CaptureChangesWithIntegrator(objectId, fileHandlingRules.IntegratorRef, mode, contextServices);
    else
      this.CaptureModifiedFilesOnly(objectId);
  }

  private void CaptureChangesWithIntegrator(
    long objectId,
    IntegratorObject integratorRef,
    SaveChangesMode mode,
    IServiceProvider contextServices)
  {
    if ((mode != SaveChangesMode.Checkin ? 0 : (IntegratorServices.HasService<IExtendedSaveSupport>(integratorRef) ? 1 : 0)) != 0)
    {
      IExtendedSaveSupport saveService = IntegratorServices.GetService<IExtendedSaveSupport>(integratorRef, true);
      ExtendedSaveOptions saveOptions = (ExtendedSaveOptions) contextServices.GetService(typeof (ExtendedSaveOptions));
      if (saveOptions == null)
        saveOptions = new ExtendedSaveOptions(mode);
      if (this.IsInteractive(contextServices))
        ProgressSinks.DialogService.Invoke($"Расширенное сохранение в {DBHelper.GetObjectCaption(objectId)}", ProgressSinkDialogFlags.Default, (Action<IPercentageProgressSink>) (progressSink => this.InvokeExtendedSaveChanges(saveService, objectId, saveOptions, progressSink)));
      else
        this.InvokeExtendedSaveChanges(saveService, objectId, saveOptions);
    }
    else
    {
      ICaptureChangesService saveService = IntegratorServices.GetService<ICaptureChangesService>(integratorRef, true);
      CaptureChangesOptions saveOptions = new CaptureChangesOptions(mode);
      if (this.IsInteractive(contextServices))
        ProgressSinks.DialogService.Invoke($"Сохранение изменений в {DBHelper.GetObjectCaption(objectId)}", ProgressSinkDialogFlags.Default, (Action<IPercentageProgressSink>) (progressSink => this.InvokeNormalSaveChanges(saveService, objectId, saveOptions, progressSink)));
      else
        this.InvokeNormalSaveChanges(saveService, objectId, saveOptions);
    }
  }

  private void InvokeExtendedSaveChanges(
    IExtendedSaveSupport saveService,
    long objectId,
    ExtendedSaveOptions saveOptions,
    IPercentageProgressSink progressSink = null)
  {
    IPercentageProgressSink progressSink1 = saveOptions.ProgressSink;
    try
    {
      saveOptions.ProgressSink = progressSink;
      saveService.CaptureChanges(objectId, saveOptions);
    }
    finally
    {
      saveOptions.ProgressSink = progressSink1;
    }
  }

  private void InvokeNormalSaveChanges(
    ICaptureChangesService saveService,
    long objectId,
    CaptureChangesOptions saveOptions,
    IPercentageProgressSink progressSink = null)
  {
    IPercentageProgressSink progressSink1 = saveOptions.ProgressSink;
    try
    {
      saveOptions.ProgressSink = progressSink;
      saveService.CaptureChanges(objectId, saveOptions);
    }
    finally
    {
      saveOptions.ProgressSink = progressSink1;
    }
  }

  private bool IsInteractive(IServiceProvider contextServices)
  {
    return !this.IsNonInteractive(contextServices);
  }

  private bool IsNonInteractive(IServiceProvider contextServices)
  {
    ObjectCommandsOptionsHolder service = (ObjectCommandsOptionsHolder) contextServices.GetService(typeof (ObjectCommandsOptionsHolder));
    return service != null && (service.Value & ObjectCommandsOptions.NonInteractive) != 0;
  }

  private void CaptureModifiedFilesOnly(long objectId)
  {
    ProgressSinks.DialogService.Invoke($"Сохранение изменений в {DBHelper.GetObjectCaption(objectId)}", ProgressSinkDialogFlags.Default, (Action<IPercentageProgressSink>) (progressSink => this.CaptureModifiedFilesOnly(objectId, progressSink)));
  }

  private void CaptureModifiedFilesOnly(long objectId, IPercentageProgressSink progressSink)
  {
    using (UIReport.CreateScope())
    {
      ToolServiceReportBuilder serviceReportBuilder = new ToolServiceReportBuilder();
      if (UIReport.Enabled)
        serviceReportBuilder.ReportSaveChangesStart(objectId);
      try
      {
        new CaptureChangesManager()
        {
          Driver = ((ICaptureChangesDriver) new AnyFileCaptureChangesDriver())
        }.CaptureChanges(new CaptureChangesActionParameters()
        {
          ObjectId = objectId,
          ProgressSink = progressSink
        });
        if (!UIReport.Enabled)
          return;
        serviceReportBuilder.ReportSuccess();
      }
      catch (Exception ex)
      {
        if (UIReport.Enabled)
          serviceReportBuilder.ReportFail(ex);
        throw;
      }
    }
  }
}
