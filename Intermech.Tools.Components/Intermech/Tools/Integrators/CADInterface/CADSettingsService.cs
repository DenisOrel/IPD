// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADSettingsService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Tools.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Реализует сервис настроек для CAD-систем на основе CAD-интерфейса.
/// </summary>
public class CADSettingsService : 
  IntegratorSettingsService<CADSettings>,
  IIntegratorSettingsViewModelService,
  ICADSettingsService,
  IIntegratorSettingsService,
  IIntegratorService,
  IDocumentAttributesSettingsService,
  IArticleAttributesSettingsService
{
  private readonly CADSettingsFactory settingsFactory;
  private readonly bool sharedModelAttributes;
  private SynchronizedCADDocumentAttributes docSynchronizedAttributes;
  private SynchronizedCADArticleAttributes artSynchronizedAttributes;

  /// <summary>Создает объект.</summary>
  /// <param name="owner">Владелец сервиса</param>
  /// <param name="settingsFactory">Фабрика составных частей для сервиса инструментов</param>
  /// <param name="sharedModelAttributes">Признак, что атрибуты документа и атрибуты конфигураций хранятся в одном контейнере</param>
  public CADSettingsService(
    IIntegrator owner,
    CADSettingsFactory settingsFactory,
    bool sharedModelAttributes)
    : base(owner)
  {
    this.settingsFactory = settingsFactory != null ? settingsFactory : throw new ArgumentNullException(nameof (settingsFactory), LocalizationHolder.rm.GetString("Tools.Components_388"));
    this.sharedModelAttributes = sharedModelAttributes;
  }

  /// <summary>
  /// Обработчик события, который вызывается сразу после успешной инициализации сервиса.
  /// Может использоваться для выполнения действий, требующих предварительной полной инициализации сервиса.
  /// </summary>
  protected override void DoAfterInitialize()
  {
    base.DoAfterInitialize();
    this.docSynchronizedAttributes = new SynchronizedCADDocumentAttributes((ICADSettingsService) this);
    this.artSynchronizedAttributes = new SynchronizedCADArticleAttributes((ICADSettingsService) this);
    this.docSynchronizedAttributes.LinkWithArticleAttributes(this.sharedModelAttributes, this.artSynchronizedAttributes);
    this.artSynchronizedAttributes.LinkWithDocumentAttributes(this.sharedModelAttributes, this.docSynchronizedAttributes);
  }

  /// <summary>
  /// Возвращает проекцию настроек интегратора, представляющую общую часть настроек всех интеграторов на основе CAD-интерфейса.
  /// При первом вызове этого метода выполняется кэширование настроек интегратора. Кэш автоматически сбрасывается при их изменении в базе.
  /// </summary>
  /// <returns>Общая часть настроек интегратора</returns>
  /// <exception cref="T:System.Exception">Объект с настройками еще не создан в базе IPS, либо настройки интегратора содержат ошибки</exception>
  public CADSettings GetCADSettings() => this.GetSettings();

  /// <summary>
  /// Возвращает список типов файловых документов, которые пользователь может создавать в CAD-системе.
  /// </summary>
  /// <returns>Список типов файловых документов</returns>
  public virtual List<LocalId<int>> GetNewFileDocumentTypes()
  {
    return this.GetCADSettings().GetCommonFileDocumentTypes();
  }

  /// <summary>
  /// Отображает тип документа IPS в тип документа CAD-системы.
  /// </summary>
  /// <param name="documentType">Идентификатор типа документа IPS</param>
  /// <returns>Идентификатор типа документа CAD-системы или null</returns>
  public virtual CADDocumentType? MapDocumentTypeToCADDocumentType(int documentType)
  {
    this.RequireReadyState();
    DocumentGroup byDocumentType = this.GetCADSettings().FileDocumentGroups.FindByDocumentType(documentType, false);
    if (byDocumentType == null)
      return new CADDocumentType?();
    switch (byDocumentType.Name)
    {
      case "Assembly":
        return new CADDocumentType?(CADDocumentType.Assembly);
      case "Part":
        return new CADDocumentType?(CADDocumentType.Part);
      case "AssemblyDrawing":
      case "PartDrawing":
        return new CADDocumentType?(CADDocumentType.Drawing);
      default:
        return new CADDocumentType?(CADDocumentType.Undefined);
    }
  }

  protected override IntegratorSettingsCodec CreateSettingsCodec()
  {
    return (IntegratorSettingsCodec) this.settingsFactory.CreateCodec();
  }

  protected override IntegratorSettingsValidator CreateSettingsValidator()
  {
    return this.settingsFactory.CreateValidator();
  }

  /// <summary>
  /// Возвращает объект, позволяющий получить коллекцию синхронизируемых атрибутов документов.
  /// </summary>
  public ISynchronizedObjectAttributes SynchronizedDocumentAttributes
  {
    [DebuggerStepThrough] get
    {
      this.RequireReadyState();
      return (ISynchronizedObjectAttributes) this.docSynchronizedAttributes;
    }
  }

  /// <summary>
  /// Возвращает объект, позволяющий получить коллекцию синхронизируемых атрибутов изделий.
  /// </summary>
  public ISynchronizedObjectAttributes SynchronizedArticleAttributes
  {
    [DebuggerStepThrough] get
    {
      this.RequireReadyState();
      return (ISynchronizedObjectAttributes) this.artSynchronizedAttributes;
    }
  }

  /// <summary>
  /// Создает модель представления для указанного объекта настроек.
  /// </summary>
  /// <param name="settingsObject">Объект с настройками интегратора</param>
  /// <returns>Модель представления</returns>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="settingsObject" /> не должен быть равен null</exception>
  object IIntegratorSettingsViewModelService.CreateViewModel(ISettingsObject settingsObject)
  {
    if (settingsObject == null)
      throw new ArgumentNullException(nameof (settingsObject));
    this.RequireReadyState();
    CADSettings settings = (CADSettings) settingsObject;
    CADSettingsViewModel settingsViewModel = this.settingsFactory.CreateSettingsViewModel();
    settingsViewModel.LoadContent(settings);
    return (object) settingsViewModel;
  }

  /// <summary>
  /// Восстанавливает объект с настройками из указанной модели представления.
  /// Этот метод используется после завершения редактирования настроек в PropertyGrid.
  /// </summary>
  /// <param name="viewModelObject">Модель представления</param>
  /// <returns>Объект с настройками интегратора</returns>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="viewModelObject" /> не должен быть равен null</exception>
  ISettingsObject IIntegratorSettingsViewModelService.CreateSettingsFromViewModel(
    object viewModelObject)
  {
    if (viewModelObject == null)
      throw new ArgumentNullException(nameof (viewModelObject));
    this.RequireReadyState();
    CADSettingsViewModel settingsViewModel = (CADSettingsViewModel) viewModelObject;
    CADSettings settingsObject = this.settingsFactory.CreateSettingsObject();
    CADSettings settings = settingsObject;
    settingsViewModel.SaveContent(settings);
    return (ISettingsObject) settingsObject;
  }
}
