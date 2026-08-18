// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADArticleLaunchActionService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces.Data;
using Intermech.Runtime;
using Intermech.Tools.LaunchActions;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Реализует сервис интегратора, отвечающий за открытие документов CAD-системы в контексте изделия.
/// Когда специализированный вариант команды запуска CAD-системы применяется к изделию, то сервис позволяет
/// открыть в CAD-системе файл модели, связанной с изделием, и активизировать в модели конфигурацию, соответствующую изделию.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="owner">Владелец компонента</param>
/// <exception cref="T:System.ArgumentNullException">Ссылка на владельца компонента не может быть null</exception>
public class CADArticleLaunchActionService(IIntegrator owner) : 
  IntegratorService(owner),
  IArticleLaunchActionSupport
{
  private ICADInterfaceService cadApiService;
  private ICADSettingsService settingsService;

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
    [DebuggerStepThrough] set
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
    if (this.SettingsService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "SettingsService");
    if (this.ApiService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "ApiService");
  }

  /// <summary>Проверяет возможность использования сервиса.</summary>
  /// <param name="articleId">Идентификатор версии изделия, к которому изначально была применена команда</param>
  /// <param name="documentLaunchParams">Параметры запуска приложения для документа, связанного с изделием и выбранного пользователем</param>
  /// <param name="documentType">Идентификатор типа документа, связанного с изделием и выбранного пользователем</param>
  /// <returns>true, если это модель CAD-системы, с которой может работать сервис</returns>
  public bool IsSupported(long articleId, LaunchParams documentLaunchParams, int documentType)
  {
    if (documentLaunchParams == null)
      throw new ArgumentNullException(nameof (documentLaunchParams));
    if (articleId == 0L)
      throw new ArgumentException("Не задан идентификатор версии изделия", nameof (articleId));
    if (documentType == -1)
      throw new ArgumentException("Не задан идентификатор типа документа", nameof (documentType));
    this.RequireReadyState();
    if (!string.IsNullOrEmpty(documentLaunchParams.ObjectFileName))
      return false;
    DocumentGroup byDocumentType = this.settingsService.GetCADSettings().FileDocumentGroups.FindByDocumentType(documentType, false);
    if (byDocumentType == null)
      return false;
    return byDocumentType.Name == "Assembly" || byDocumentType.Name == "Part";
  }

  /// <summary>
  /// Заполняет контекст открытия файла, сохраняя в нем информацию об изделии, для которого была вызвана команда запуска приложения.
  /// Позже эта информация будет использована при открытии файла модели в CAD-системе.
  /// </summary>
  /// <param name="articleId">Идентификатор версии изделия, к которому изначально была применена команда</param>
  /// <param name="documentLaunchParams">Параметры запуска приложения для документа, связанного с изделием и выбранного пользователем</param>
  public void MakeLaunchContext(long articleId, LaunchParams documentLaunchParams)
  {
    if (documentLaunchParams == null)
      throw new ArgumentNullException(nameof (documentLaunchParams));
    if (articleId == 0L)
      throw new ArgumentException("Не задан идентификатор версии изделия", nameof (articleId));
    this.RequireReadyState();
    PropertyContainer launchContext = documentLaunchParams.LaunchContext;
    launchContext.Put<bool>("OpenArticleConfiguration", true);
    launchContext.Put<long>("ArticleId", articleId);
  }
}
