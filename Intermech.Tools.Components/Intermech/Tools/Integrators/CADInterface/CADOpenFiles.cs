// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADOpenFiles
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.Collections;
using Intermech.Files;
using Intermech.IO;
using Intermech.Localization;
using Intermech.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Реализует расширение сервиса открытых файлов для CAD-систем с поддержкой CAD-интерфейса.
/// Класс является thread-safe.
/// </summary>
internal sealed class CADOpenFiles : IntegratorService, IOpenFilesServiceExtension, IOpenFiles
{
  private readonly ReloadService reloadService;
  private readonly CADReloadDriver reloadDriver;
  private IApplicationFileTypes fileTypeService;
  private ICADInterfaceService cadApiService;

  /// <summary>Создает сервис.</summary>
  /// <param name="owner">Владелец сервиса</param>
  public CADOpenFiles(IIntegrator owner)
    : base(owner)
  {
    this.reloadDriver = new CADReloadDriver(owner);
    this.reloadService = new ReloadService(owner);
    this.reloadService.ReloadDriver = (IReloadDriver) this.reloadDriver;
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
  /// Возвращает или задает ссылку на сервис для доступа к API приложения. Свойство должно быть заполнено до начала использования текущего сервиса.
  /// </summary>
  public ICADInterfaceService ApiService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.cadApiService;
    }
    [DebuggerStepThrough] set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.cadApiService = value;
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
    if (this.ApiService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "ApiService");
    this.reloadDriver.OutputService = this.OutputService;
    this.reloadDriver.LicenseService = this.LicenseService;
    this.reloadDriver.ApiService = this.ApiService;
    this.reloadDriver.Initialize();
    this.reloadService.OutputService = this.OutputService;
    this.reloadService.LicenseService = this.LicenseService;
    this.reloadService.Initialize();
  }

  /// <summary>
  /// Возвращает true, если указанный файл открыт в каком-либо приложении.
  /// </summary>
  /// <param name="filePath">Путь к файлу</param>
  /// <returns>Признак того, что файл открыт в приложении</returns>
  /// <exception cref="T:System.ArgumentNullException">Путь к файлу не может быть null</exception>
  public bool IsOpen(string filePath)
  {
    if (filePath == null)
      throw new ArgumentNullException(nameof (filePath));
    this.RequireReadyState();
    if (!Path.IsPathRooted(filePath))
      return false;
    try
    {
      if (!this.fileTypeService.IsApplicationFile(filePath) || !this.cadApiService.IsApplicationRunning)
        return false;
      using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) this.cadApiService))
        return cadApiSession.Application.IsOpenDocument(filePath);
    }
    catch (Exception ex)
    {
      this.ShowError(ex);
      return false;
    }
  }

  /// <summary>
  /// Возвращает true, если указанный файл открыт в каком-либо приложении и имеет несохраненные изменения.
  /// </summary>
  /// <param name="filePath">Путь к файлу</param>
  /// <returns>Признак того, что файл имеет несохраненные изменения</returns>
  /// <exception cref="T:System.ArgumentNullException">Путь к файлу не может быть null</exception>
  public bool IsDirty(string filePath)
  {
    if (filePath == null)
      throw new ArgumentNullException(nameof (filePath));
    this.RequireReadyState();
    if (!Path.IsPathRooted(filePath))
      return false;
    try
    {
      if (!this.fileTypeService.IsApplicationFile(filePath) || !this.cadApiService.IsApplicationRunning)
        return false;
      using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) this.cadApiService))
      {
        CADDocumentProxy openDocument = cadApiSession.Application.FindOpenDocument(filePath);
        return openDocument != null && openDocument.Modified;
      }
    }
    catch (Exception ex)
    {
      this.ShowError(ex);
      return false;
    }
  }

  /// <summary>Сохраняет на диск имеющиеся изменения в файле.</summary>
  /// <param name="filePath">Путь к файлу</param>
  /// <exception cref="T:System.ArgumentNullException">Путь к файлу не может быть null</exception>
  public void Save(string filePath)
  {
    if (filePath == null)
      throw new ArgumentNullException(nameof (filePath));
    this.RequireReadyState();
    if (!Path.IsPathRooted(filePath))
      return;
    try
    {
      if (!this.fileTypeService.IsApplicationFile(filePath) || !this.cadApiService.IsApplicationRunning)
        return;
      using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) this.cadApiService))
      {
        CADDocumentProxy openDocument = cadApiSession.Application.FindOpenDocument(filePath);
        if (openDocument == null || !openDocument.Modified || openDocument.ReadOnly)
          return;
        openDocument.Save();
      }
    }
    catch (Exception ex)
    {
      this.ShowError(ex);
    }
  }

  /// <summary>Управляет возможностью внесения изменений в документ.</summary>
  /// <param name="filePath">Путь к файлу, открытому в приложении</param>
  /// <param name="readOnlyFlag">Значение флага</param>
  public void SetReadOnlyFlag(string filePath, bool readOnlyFlag)
  {
    if (filePath == null)
      throw new ArgumentNullException(nameof (filePath));
    this.RequireReadyState();
    if (!Path.IsPathRooted(filePath))
      return;
    try
    {
      if (!this.fileTypeService.IsApplicationFile(filePath) || !this.cadApiService.IsApplicationRunning)
        return;
      using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) this.cadApiService))
      {
        CADDocumentProxy openDocument = cadApiSession.Application.FindOpenDocument(filePath);
        if (openDocument == null || openDocument.ReadOnly == readOnlyFlag)
          return;
        openDocument.ReadOnly = readOnlyFlag;
      }
    }
    catch (Exception ex)
    {
      this.ShowError(ex);
    }
  }

  /// <summary>
  /// Проверяет, поддерживается ли перезагрузка октрытого файла без его предварительного закрытия. Если файл не открыт, то метод вернет false.
  /// </summary>
  /// <param name="filePath">Путь к файлу</param>
  /// <returns>Признак, что поддерживается перезагрузка октрытого файла без его предварительного закрытия</returns>
  /// <exception cref="T:System.ArgumentNullException">Путь к файлу не может быть null</exception>
  public bool IsReloadable(string filePath)
  {
    if (filePath == null)
      throw new ArgumentNullException(nameof (filePath));
    this.RequireReadyState();
    if (!Path.IsPathRooted(filePath))
      return false;
    try
    {
      if (!this.fileTypeService.IsApplicationFile(filePath) || !this.cadApiService.IsApplicationRunning || File.Exists(filePath) && !FileUtils.CanWriteFile(filePath))
        return false;
      using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) this.cadApiService))
      {
        CADDocumentProxy openDocument = cadApiSession.Application.FindOpenDocument(filePath);
        return openDocument != null && openDocument.Reloadable;
      }
    }
    catch (Exception ex)
    {
      this.ShowError(ex);
      return false;
    }
  }

  /// <summary>
  /// Выполняет перезагрузку открытого файла без его предварительного закрытия.
  /// </summary>
  /// <param name="filePath">Путь к файлу</param>
  /// <exception cref="T:System.ArgumentNullException">Путь к файлу не может быть null</exception>
  public void Reload(string filePath)
  {
    if (filePath == null)
      throw new ArgumentNullException(nameof (filePath));
    this.RequireReadyState();
    if (!Path.IsPathRooted(filePath))
      return;
    try
    {
      if (!this.fileTypeService.IsApplicationFile(filePath) || !this.cadApiService.IsApplicationRunning)
        return;
      using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) this.cadApiService))
      {
        CADDocumentProxy openDocument = cadApiSession.Application.FindOpenDocument(filePath);
        if (openDocument == null || !openDocument.Reloadable)
          return;
        openDocument.Reload();
      }
    }
    catch (Exception ex)
    {
      this.ShowError(ex);
    }
  }

  /// <summary>
  /// Выгружает из приложений все документы, которые используют указанные файлы. Все несохраненные изменения будут потеряны.
  /// </summary>
  /// <param name="fileList">Список путей к файлам, которые должны быть освобождены приложениями</param>
  /// <returns>Объект с информацией для переоткрытия закрытых документов. Может быть null, если ни один документ не был закрыт</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список файлов не может быть null</exception>
  public object Unload(IEnumerable<string> fileList)
  {
    if (fileList == null)
      throw new ArgumentNullException(nameof (fileList), LocalizationHolder.rm.GetString("Tools.Components_382"));
    this.RequireReadyState();
    try
    {
      LinkedList<string> allAsLinkedList = CollectionUtils.FindAllAsLinkedList<string>(fileList, (Predicate<string>) (filePath => Path.IsPathRooted(filePath) && this.fileTypeService.IsApplicationFile(filePath)));
      if (allAsLinkedList.Count == 0 || !this.cadApiService.IsApplicationRunning)
        return (object) null;
      lock (this.Integrator.SyncRoot)
        return this.reloadService.Unload((ICollection<string>) allAsLinkedList);
    }
    catch (Exception ex)
    {
      this.ShowError(ex);
      return (object) null;
    }
  }

  /// <summary>
  /// Выгружает все открытые в приложениях документы. Все несохраненные изменения будут потеряны.
  /// </summary>
  /// <returns>Объект с информацией для переоткрытия закрытых документов. Может быть null, если ни один документ не был закрыт</returns>
  public object UnloadAll()
  {
    this.RequireReadyState();
    try
    {
      if (!this.cadApiService.IsApplicationRunning)
        return (object) null;
      lock (this.Integrator.SyncRoot)
        return this.reloadService.UnloadAll();
    }
    catch (Exception ex)
    {
      this.ShowError(ex);
      return (object) null;
    }
  }

  /// <summary>Переоткрывает закрытые ранее документы.</summary>
  /// <param name="reloadState">Объект состояния с информацией для переоткрытия документов</param>
  public void Reload(object reloadState)
  {
    this.RequireReadyState();
    if (reloadState == null)
      return;
    try
    {
      lock (this.Integrator.SyncRoot)
        this.reloadService.Reload(reloadState);
    }
    catch (Exception ex)
    {
      this.ShowError(ex);
    }
  }

  private void ShowError(Exception x)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(LocalizationHolder.rm.GetString("Tools.Components_384"));
    stringBuilder.Append(' ');
    stringBuilder.Append(x.Message);
    this.OutputService.WriteLine(stringBuilder.ToString());
  }
}
