// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.PDMBrowserService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Runtime;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Сервис поддержки PDM-браузера в CAD-системе. Класс является thread-safe.
/// </summary>
public class PDMBrowserService : IntegratorService, IPDMBrowserService, IIntegratorService
{
  private Guid cadSystemGuid;
  private ICADSettingsService settingsService;
  private static readonly ISynchronizeActionReloadStrategy noReloadStrategy = (ISynchronizeActionReloadStrategy) new SynchronizeActionNoReloadStrategy();

  /// <summary>Создает объект.</summary>
  /// <param name="owner">Владелец сервиса</param>
  /// <param name="cadSystemId">Глобальный идентификатор поддерживаемой CAD-системы</param>
  /// <exception cref="T:System.ArgumentException">Идентификатор CAD-системы не может быть равен Guid.Empty</exception>
  public PDMBrowserService(IIntegrator owner, Guid cadSystemId)
    : base(owner)
  {
    this.cadSystemGuid = !(cadSystemId == Guid.Empty) ? cadSystemId : throw new ArgumentException("Не задан идентификатор CAD-системы.", nameof (cadSystemId));
  }

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
  /// Проверяет конфигурацию сервиса и выполняет его окончательную инициализацию.
  /// После успешного выполнения этого метода сервис интегратора можно использовать.
  /// </summary>
  /// <exception cref="T:InvalidOperationException">Конфигурация сервиса некорректна</exception>
  protected override void DoInitialize()
  {
    base.DoInitialize();
    if (this.SettingsService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "SettingsService");
  }

  /// <summary>Возвращает глобальный идентификатор CAD-системы.</summary>
  public Guid CADSystemId
  {
    [DebuggerStepThrough] get
    {
      this.RequireReadyState();
      return this.cadSystemGuid;
    }
  }

  /// <summary>
  /// Определяет, могут ли конструкторские документы указанного типа служить источником информации о зонах для спецификации.
  /// </summary>
  /// <param name="documentType">Идентификатор типа документа</param>
  /// <returns>true - документ может содержать информацию о зонах для спецификации, false - документ не может содержать информацию о зонах</returns>
  /// <exception cref="T:ArgumentException">Параметр <param name="documentType" /> не задан</exception>
  public bool CanProvideSpecificationZones(int documentType)
  {
    if (documentType == -1)
      throw new ArgumentException("Не задан идентификатор типа документа.", nameof (documentType));
    this.RequireReadyState();
    lock (this.Integrator.SyncRoot)
      return this.OnCanProvideSpecificationZones(documentType);
  }

  /// <summary>
  /// Определяет, могут ли конструкторские документы указанного типа служить источником информации о зонах для спецификации.
  /// </summary>
  /// <param name="documentType">Идентификатор типа документа</param>
  /// <returns>true - документ может содержать информацию о зонах для спецификации, false - документ не может содержать информацию о зонах</returns>
  protected virtual bool OnCanProvideSpecificationZones(int documentType)
  {
    DocumentGroup byDocumentType = this.SettingsService.GetCADSettings().FileDocumentGroups.FindByDocumentType(documentType, false);
    return byDocumentType != null && byDocumentType.Name == "AssemblyDrawing";
  }

  /// <summary>
  /// Создает стратегию для переоткрытия в CAD-системе открытых файлов документов, подлежащих обновлению из базы данных IPS.
  /// Используется командной PDM-браузера "Синхронизировать".
  /// </summary>
  /// <returns>Объект стратегии</returns>
  public ISynchronizeActionReloadStrategy CreateSynchronizeActionReloadStrategy()
  {
    this.RequireReadyState();
    lock (this.Integrator.SyncRoot)
      return this.DoCreateSynchronizeActionReloadStrategy();
  }

  /// <summary>
  /// Создает стратегию для переоткрытия в CAD-системе открытых файлов документов, подлежащих обновлению из базы данных IPS.
  /// Используется командной PDM-браузера "Синхронизировать".
  /// </summary>
  /// <returns>Объект стратегии</returns>
  protected virtual ISynchronizeActionReloadStrategy DoCreateSynchronizeActionReloadStrategy()
  {
    return PDMBrowserService.noReloadStrategy;
  }
}
