// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CaptureChangesService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces.Client;
using Intermech.Tools.DataExchange;
using Intermech.UI;
using System;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Реализует базовый класс для сервиса интегратора, отвечающего за передачу изменений из файловой копии объекта в базу IPS.
/// Реализация является thread safe.
/// </summary>
public abstract class CaptureChangesService : IntegratorService, ICaptureChangesService
{
  private readonly CaptureChangesManager manager;
  private readonly ToolServiceReportBuilder uiReporter;
  private readonly CaptureChangesOptions emptyCaptureChangesOptions;
  private readonly DataExchangeHelper dataExchangeHelper;

  /// <summary>Создает объект.</summary>
  /// <param name="owner">Владелец компонента</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на владельца компонента не может быть null</exception>
  protected CaptureChangesService(IIntegrator owner)
    : base(owner)
  {
    this.manager = new CaptureChangesManager();
    this.uiReporter = new ToolServiceReportBuilder();
    this.emptyCaptureChangesOptions = new CaptureChangesOptions(SaveChangesMode.Default);
    this.dataExchangeHelper = new DataExchangeHelper();
  }

  /// <summary>
  /// Захватывает и сохраняет в рабочую копию объекта изменения, сделанные пользователем в
  /// файловой копии объекта. Как правило, этот метод вызывается из обработчика команды
  /// "Сохранить изменения".
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <exception cref="T:System.ArgumentException">Параметр <paramref name="objectId" /> не задан</exception>
  public void CaptureChanges(long objectId)
  {
    this.CaptureChanges(objectId, this.emptyCaptureChangesOptions);
  }

  /// <summary>
  /// Захватывает и сохраняет в рабочую копию объекта изменения, сделанные пользователем в
  /// файловой копии объекта. Как правило, этот метод вызывается из обработчика команды
  /// "Сохранить изменения".
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="options">Опции выполнения операции</param>
  /// <exception cref="T:System.ArgumentException">Параметр <paramref name="objectId" /> не задан</exception>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="options" /> не должен быть равен null</exception>
  public void CaptureChanges(long objectId, CaptureChangesOptions options)
  {
    if (objectId == 0L)
      throw new ArgumentException("Не задан идентификатор версии объекта IPS.", nameof (objectId));
    if (options == null)
      throw new ArgumentNullException(nameof (options));
    this.RequireReadyState();
    if (!this.dataExchangeHelper.ShouldCaptureChanges(objectId))
      return;
    lock (this.Integrator.SyncRoot)
    {
      this.CheckDriver();
      if (this.manager.Driver == null)
        this.manager.Driver = this.Driver;
      this.LicenseService.Check();
      IPercentageProgressSink percentageProgressSink = options.ProgressSink ?? ProgressSinks.NullPercentageSink;
      double fileProgressRange = this.dataExchangeHelper.GetMainFileProgressRange(this.Integrator);
      CaptureChangesResult captureChangesResult;
      using (UIReport.CreateScope())
      {
        try
        {
          if (UIReport.Enabled)
            this.uiReporter.ReportSaveChangesStart(objectId);
          this.OnBeforeCaptureChanges(objectId);
          this.ConfigureDriverParameters(options);
          percentageProgressSink.SetState("Сохранение изменений в документе");
          captureChangesResult = this.manager.CaptureChanges(this.CreateActionParameters(objectId, options, percentageProgressSink.CreateNestedSink(fileProgressRange)));
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
          this.ResetDriverParameters();
        }
      }
      percentageProgressSink.SetState("Импорт новых ссылочных зависимостей");
      if (captureChangesResult != null)
        this.dataExchangeHelper.ImportDeferredDraftDocuments(captureChangesResult, percentageProgressSink.CreateNestedSink(100.0 - fileProgressRange));
      percentageProgressSink.SetState(string.Empty);
      percentageProgressSink.SetProgress(100.0);
    }
  }

  private CaptureChangesActionParameters CreateActionParameters(
    long objectId,
    CaptureChangesOptions options,
    IPercentageProgressSink progressSink)
  {
    return new CaptureChangesActionParameters()
    {
      ObjectId = objectId,
      ProgressSink = progressSink
    };
  }

  private void CheckDriver()
  {
    if (this.Driver == null)
      throw new InvalidOperationException("Property 'Driver' must not be null.");
  }

  /// <summary>
  /// Возвращает экземпляр драйвера для захвата изменений в документах интегрируемого приложения.
  /// </summary>
  protected abstract ICaptureChangesDriver Driver { get; }

  /// <summary>
  /// Устанавливает свойства драйвера, управляющие его поведением. Метод вызывается перед каждым использованием драйвера.
  /// </summary>
  /// <param name="options">Опции выполнения операции</param>
  protected virtual void ConfigureDriverParameters(CaptureChangesOptions options)
  {
  }

  /// <summary>
  /// Очищает свойства драйвера, управляющие его поведением. Метод вызывается после каждого использования драйвера.
  /// </summary>
  protected virtual void ResetDriverParameters()
  {
  }

  /// <summary>Вызывается в самом начале процесса.</summary>
  /// <param name="objectId">Идентификатор документа</param>
  protected virtual void OnBeforeCaptureChanges(long objectId)
  {
  }

  /// <summary>
  /// Вызывается в самом конце процесса после успешного захвата изменений.
  /// Этот метод не будет вызван, если при захвате изменений будет сброшено исключение.
  /// </summary>
  /// <param name="result">Результаты захвата изменений</param>
  protected virtual void OnAfterCaptureChanges(CaptureChangesResult result)
  {
    if (result == null)
      throw new ArgumentNullException(nameof (result));
  }
}
