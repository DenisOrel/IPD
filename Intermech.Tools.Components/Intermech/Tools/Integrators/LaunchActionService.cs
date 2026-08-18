// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.LaunchActionService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Files;
using Intermech.Localization;
using Intermech.Runtime;
using Intermech.Tools.LaunchActions;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Реализует базовый класс для сервиса открытия документов приложения для редактирования или просмтора. Для извлеченая файлов используется рабочая область пользователя.
/// Класс является thread-safe.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="owner">Владелец сервиса</param>
public abstract class LaunchActionService(IIntegrator owner) : 
  IntegratorService(owner),
  ILaunchActionSupport
{
  private IFileVault fileVault;
  private IApplicationFileTypes fileTypeService;

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
  /// Возвращает или задает системный сервис файлового хранилища IPS. Свойство должно быть заполнено до начала использования текущего сервиса.
  /// </summary>
  public IFileVault FileVault
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.fileVault;
    }
    [DebuggerStepThrough] set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.fileVault = value;
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
    if (this.FileVault == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "FileVault");
  }

  /// <summary>
  /// Возвращает true, если интегратор поддерживает тип команды открытия документа в интегрируемом приложении. Реализация по умолчанию предполагает, что
  /// интегратор поддерживает просмотр и редактирование документов.
  /// </summary>
  /// <param name="launchType">Тип команды</param>
  /// <returns>Признак, что интегратор поддерживает указанный тип команды</returns>
  public bool IsSupported(LaunchType launchType)
  {
    this.RequireReadyState();
    return launchType == LaunchType.Edit || launchType == LaunchType.View;
  }

  /// <summary>Открывает документ в приложении</summary>
  /// <param name="launchParams">Параметры команды открытия документа</param>
  /// <param name="afterPublishFile">Событие публикации открываемого файла на диске. Может быть null</param>
  public void OpenDocument(
    LaunchParams launchParams,
    EventHandler<LaunchHandlerEventArgs> afterPublishFile)
  {
    if (launchParams == null)
      throw new ArgumentNullException(nameof (launchParams));
    this.RequireReadyState();
    lock (this.Integrator.SyncRoot)
    {
      this.LicenseService.Check();
      if (!this.IsSupported(launchParams.LaunchType))
        throw new FaultException(string.Format(LocalizationHolder.rm.GetString("Tools.Components_370"), (object) this.Integrator.DisplayName, (object) EnumTypeHelper.GetCaption((Enum) launchParams.LaunchType)));
      if (string.IsNullOrEmpty(launchParams.ObjectFileName))
        launchParams.ObjectFileName = this.fileVault.DBFilesInfo.GetMasterFileName(launchParams.ObjectId, true);
      if (!this.fileTypeService.IsApplicationFile(launchParams.ObjectFileName))
        throw new FaultException(string.Format(LocalizationHolder.rm.GetString("Tools.Components_381"), (object) launchParams.ObjectFileName));
      if (launchParams.FileArea != null && launchParams.FileArea != this.fileVault.WorkArea)
        throw new FaultException($"{this.Integrator.DisplayName} поддерживает извлечение файловы документов только в рабочую область файлового хранилища.");
      if (launchParams.FileArea == null)
        launchParams.FileArea = (IFileArea) this.fileVault.WorkArea;
      launchParams.ResultFilePath = this.fileVault.PublishTree(launchParams.ObjectId, launchParams.ObjectFileName, launchParams.VersionsRule, launchParams.FileArea);
      if (afterPublishFile != null)
        afterPublishFile((object) this, new LaunchHandlerEventArgs(launchParams));
      this.OpenDocumentFileFromDisk(launchParams);
    }
  }

  /// <summary>Открывает файл документа в приложении.</summary>
  /// <param name="launchParams">Параметры команды открытия документа</param>
  protected abstract void OpenDocumentFileFromDisk(LaunchParams launchParams);
}
