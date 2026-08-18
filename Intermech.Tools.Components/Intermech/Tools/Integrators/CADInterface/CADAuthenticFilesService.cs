// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADAuthenticFilesService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.ControlFlow;
using Intermech.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Реализует сервис интегратора, отвечающий за создание аутентичных файлов для документов приложения, с которым осуществляется интеграция.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="owner">Владелец компонента</param>
/// <exception cref="T:System.ArgumentNullException">Ссылка на владельца компонента не может быть null</exception>
public class CADAuthenticFilesService(IIntegrator owner) : IntegratorService(owner), IAuthenticFilesService
{
  private ICADSettingsService settingsService;
  private ICADInterfaceService cadApiService;

  /// <summary>
  /// Возвращает или задает ссылку на сервис настроек интегратора. Свойство должно быть заполнено до начала использования текущего сервиса.
  /// </summary>
  public ICADSettingsService SettingsService
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
  /// Возвращает или задает ссылку на сервис для доступа к API приложения. Свойство должно быть заполнено до начала использования текущего сервиса.
  /// </summary>
  public ICADInterfaceService ApiService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.cadApiService;
    }
    set
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
    if (this.SettingsService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "SettingsService");
    if (this.ApiService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "ApiService");
  }

  /// <summary>
  /// Возвращает список типов файлов, которыми могут быть аутентичные файлы.
  /// </summary>
  /// <param name="documentType">Идентификатор типа документа</param>
  /// <returns>Список расширений файлов, начинающихся с точки</returns>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор типа документа</exception>
  public ICollection<string> GetPossibleFileTypes(int documentType)
  {
    if (documentType == -1)
      throw new ArgumentException("Не задан идентификатор типа документа.", nameof (documentType));
    this.RequireReadyState();
    CADDocumentType? cadDocumentType = this.settingsService.MapDocumentTypeToCADDocumentType(documentType);
    if (!cadDocumentType.HasValue)
      return (ICollection<string>) new string[0];
    return cadDocumentType.Value == CADDocumentType.Undefined || cadDocumentType.Value == CADDocumentType.DefinedByTemplate ? (ICollection<string>) new string[0] : this.GetExportFormats(cadDocumentType.Value);
  }

  private ICollection<string> GetExportFormats(CADDocumentType appDocType)
  {
    using (new DynamicScope())
    {
      IntegratorVars.NakedApiSessions.Declare(true);
      using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) this.cadApiService))
        return (ICollection<string>) cadApiSession.Application.GetExportFormats(appDocType);
    }
  }

  /// <summary>
  /// Создает имя и путь для аутентичного файла с учетом особенностей приложения.
  /// </summary>
  /// <param name="documentFilePath">Абсолютный путь к файлу документа</param>
  /// <param name="authenticFileType">Расширение аутентичного файла, начинающееся с точки</param>
  /// <returns>Абсолютный путь к аутентичному файлу</returns>
  public virtual string MakeFilePath(string documentFilePath, string authenticFileType)
  {
    if (documentFilePath == null)
      throw new ArgumentNullException(nameof (documentFilePath));
    if (authenticFileType == null)
      throw new ArgumentNullException(nameof (authenticFileType));
    this.RequireReadyState();
    return documentFilePath + authenticFileType;
  }

  /// <summary>
  /// Создает/обновляет аутентичный файл для указанного документа.
  /// </summary>
  /// <param name="documentFilePath">Абсолютный путь к файлу документа</param>
  /// <param name="authenticFilePath">Абсолютный путь к аутентичному файлу</param>
  /// <exception cref="T:System.ArgumentNullException">documentFilePath или authenticFilePath</exception>
  public void MakeFile(string documentFilePath, string authenticFilePath)
  {
    if (documentFilePath == null)
      throw new ArgumentNullException(nameof (documentFilePath));
    if (authenticFilePath == null)
      throw new ArgumentNullException(nameof (authenticFilePath));
    this.RequireReadyState();
    using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) this.cadApiService))
      cadApiSession.Application.OpenDocument(documentFilePath, this.RequireVisibleDocument()).Export(authenticFilePath);
  }

  /// <summary>
  /// Возвращает признак, что аутентичный файл может быть сформирован только в том случае, если документ был открыт в окне.
  /// Значение по умолчанию - false.
  /// </summary>
  /// <returns>Признак, что документ требуется открыть в окне, чтобы создание аутентичнго файла было возможно</returns>
  protected virtual bool RequireVisibleDocument() => false;
}
