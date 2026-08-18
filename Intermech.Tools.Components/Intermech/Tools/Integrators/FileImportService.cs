// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.FileImportService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Runtime;
using Intermech.Tools.DataExchange;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Реализует базовый класс для создания сервиса интегратора по импорту файлов в IPS.
/// </summary>
public abstract class FileImportService : IntegratorService, IFileImportSupport
{
  protected readonly CaptureChangesManager captureManager;
  private readonly ToolServiceReportBuilder uiReporter;
  private readonly FileImportOptions emptyFileImportOptions;
  private readonly DataExchangeHelper dataExchangeHelper;
  protected IApplicationFileTypes fileTypeService;
  private TransferFileToWorkspaceMode allowTransferFileToWorkspace;

  /// <summary>Создает объект.</summary>
  /// <param name="owner">Владелец компонента</param>
  protected FileImportService(IIntegrator owner)
    : base(owner)
  {
    this.captureManager = new CaptureChangesManager();
    this.uiReporter = new ToolServiceReportBuilder();
    this.emptyFileImportOptions = new FileImportOptions();
    this.emptyFileImportOptions.NotifyOnDeferredFilesErrors = true;
    this.dataExchangeHelper = new DataExchangeHelper();
  }

  /// <summary>
  /// Возвращает или задает ссылку на сервис типов файлов интегратора. Свойство должно быть заполнено до начала использования текущего сервиса.
  /// </summary>
  public IApplicationFileTypes FileTypeService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.fileTypeService;
    }
    [DebuggerStepThrough] set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.fileTypeService = value;
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
    if (this.FileTypeService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "FileTypeService");
  }

  /// <summary>
  /// Позволяет определить, может ли интегратор импортировать этот файл.
  /// </summary>
  /// <param name="fileInfo">Сведения о файле</param>
  /// <param name="fileContent">Поток с содержимым файла</param>
  /// <returns>true, если интегратор поддерживает этот файл, false - если файл не знаком интегратору</returns>
  public bool CanImportFile(FileInfo fileInfo, Stream fileContent)
  {
    if (fileInfo == null)
      throw new ArgumentNullException(nameof (fileInfo));
    if (fileContent == null)
      throw new ArgumentNullException(nameof (fileContent));
    this.RequireReadyState();
    return this.DoCheckCanImportFile(fileInfo, fileContent);
  }

  /// <summary>
  /// Позволяет определить, может ли интегратор импортировать этот файл.
  /// </summary>
  /// <param name="fileInfo">Сведения о файле</param>
  /// <param name="fileContent">Поток с содержимым файла</param>
  /// <returns>true, если интегратор поддерживает этот файл, false - если файл не знаком интегратору</returns>
  protected virtual bool DoCheckCanImportFile(FileInfo fileInfo, Stream fileContent)
  {
    return this.fileTypeService.IsApplicationFile(fileInfo, fileContent);
  }

  /// <summary>Возвращает флаги особенностей импорта файла.</summary>
  /// <returns>Флаги особенностей импорта файла</returns>
  public ImportFileCapabilities GetImportFileCapabilities()
  {
    this.RequireReadyState();
    return this.DoGetImportFileCapabilities();
  }

  /// <summary>Возвращает флаги особенностей импорта файла.</summary>
  /// <returns>Флаги особенностей импорта файла</returns>
  protected virtual ImportFileCapabilities DoGetImportFileCapabilities()
  {
    return ImportFileCapabilities.None;
  }

  /// <summary>Выполняет импорт файла в систему.</summary>
  /// <param name="fullPath">Абсолютный путь к импортируемому файлу</param>
  /// <returns>Результат импорта файла</returns>
  /// <exception cref="T:System.ArgumentException">Не задан путь к имени импортируемого файла</exception>
  public FileImportResult ImportFile(string fullPath)
  {
    return this.ImportFile(fullPath, this.emptyFileImportOptions);
  }

  /// <summary>Выполняет импорт файла в систему.</summary>
  /// <param name="fullPath">Абсолютный путь к импортируемому файлу</param>
  /// <param name="importOptions">Опции импорта файла</param>
  /// <returns>Результат импорта файла</returns>
  /// <exception cref="T:System.ArgumentException">Не задан путь к имени импортируемого файла</exception>
  public FileImportResult ImportFile(string fullPath, FileImportOptions importOptions)
  {
    if (string.IsNullOrEmpty(fullPath))
      throw new ArgumentException("Не задан путь к импортируемому файлу.", nameof (fullPath));
    if (importOptions == null)
      throw new ArgumentNullException(nameof (importOptions));
    if (!Path.IsPathRooted(fullPath))
      throw new InvalidOperationException($"Путь к импортируемому файлу '{fullPath}' задан не в абсолютной форме.");
    this.RequireReadyState();
    lock (this.Integrator.SyncRoot)
    {
      ICaptureChangesDriver captureChangesDriver = this.GetCaptureChangesDriver();
      if (captureChangesDriver == null)
        throw new InvalidOperationException("No capture driver found.");
      if (this.captureManager.Driver == null)
        this.captureManager.Driver = captureChangesDriver;
      this.LicenseService.Check();
      CaptureChangesResult captureChangesResult;
      using (UIReport.CreateScope())
      {
        try
        {
          if (UIReport.Enabled)
            this.uiReporter.ReportFileImportStart(fullPath);
          if (this.AllowTransferFileToWorkspace != TransferFileToWorkspaceMode.None)
            fullPath = this.TransferFileToWorkspace(fullPath);
          this.OnBeforeImportFile(fullPath);
          this.SetCaptureChangesParameters(FileVars.ExtendedMode.Value);
          captureChangesResult = this.captureManager.ImportFile(this.CreateActionParameters(fullPath, importOptions));
          this.OnAfterImportFile(captureChangesResult);
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
      FileImportResult.Success success = new FileImportResult.Success(fullPath, captureChangesResult.ObjectId);
      success.DeferredFiles.AddRange((IEnumerable<string>) this.dataExchangeHelper.GetDeferredImportFiles(captureChangesResult));
      return (FileImportResult) success;
    }
  }

  private ImportFileActionParameters CreateActionParameters(
    string fullPath,
    FileImportOptions importOptions)
  {
    return new ImportFileActionParameters()
    {
      FullPath = fullPath,
      ProgressSink = importOptions.ProgressSink
    };
  }

  /// <summary>
  /// Возвращает экземпляр драйвера для импорта файла интегрируемого приложения. Метод обязательно должен вернуть созданный объект.
  /// </summary>
  /// <returns>Объект драйвера</returns>
  protected abstract ICaptureChangesDriver GetCaptureChangesDriver();

  /// <summary>
  /// Устанавливает свойства драйвера, управляющие его поведением.
  /// </summary>
  /// <param name="extendedImport">Признак расширенного импорта. Если содержит true, то при импорте должен быть создан не только документ, но и выпускаемые по нему объекты (изделия и др.)</param>
  protected virtual void SetCaptureChangesParameters(bool extendedImport)
  {
  }

  /// <summary>
  /// Очищает свойства драйвера, управляющие его поведением.
  /// </summary>
  protected virtual void ResetCaptureChangesParameters()
  {
  }

  /// <summary>
  /// Включает и выключает режим автоматического перемещения импортируемого файла в рабочую область пользователя.
  /// </summary>
  protected TransferFileToWorkspaceMode AllowTransferFileToWorkspace
  {
    get => this.allowTransferFileToWorkspace;
    set => this.allowTransferFileToWorkspace = value;
  }

  /// <summary>
  /// Реализует проверку, находится ли импортируемый файл в рабочей области, а при необходимости, перемещает файл в рабочую область.
  /// Базовая реализация является пустой.
  /// </summary>
  /// <param name="fullPath">Абсолютный путь к импортируемому файлу</param>
  /// <returns>Абсолютный путь к импортируемому файлу после перемещения в рабочую область</returns>
  private string TransferFileToWorkspace(string fullPath)
  {
    TransferFileToWorkspaceAction toWorkspaceAction = new TransferFileToWorkspaceAction(ServiceUtils.GetService<IOpenFilesService>((object) ApplicationServices.Container, true), ServiceUtils.GetService<IFileVaultSettingsService>((object) ApplicationServices.Container, true));
    toWorkspaceAction.ImportMode = this.AllowTransferFileToWorkspace;
    toWorkspaceAction.SourcePath = fullPath;
    toWorkspaceAction.Perform();
    return toWorkspaceAction.TargetPath;
  }

  /// <summary>Вызывается непосредственно перед импортом файла.</summary>
  /// <param name="fullPath">Абсолютный путь к импортируемому файлу</param>
  protected virtual void OnBeforeImportFile(string fullPath)
  {
  }

  /// <summary>
  /// Вызывается после успешного импорта файла. Этот метод не будет вызван, если при импорте файла будет сброшено исключение.
  /// </summary>
  /// <param name="result">Результаты захвата изменений</param>
  protected virtual void OnAfterImportFile(CaptureChangesResult result)
  {
    if (result == null)
      throw new ArgumentNullException(nameof (result));
  }
}
