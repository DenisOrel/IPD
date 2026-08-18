// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.MechanicalDriver
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Tools.DataExchange;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

public abstract class MechanicalDriver : DocumentCaptureChangesDriver
{
  private bool updateArticles;
  private bool recalculateMass;
  private MechanicalOperations mechanicalOperations;
  private StandardSchedulerStages schedulerStages;
  private List<ISidecarObjectsCaptureChangesExtension> sidecarObjectsExtensions;
  private Lazy<IArticleExternalKeysService> defaultArticleExternalKeysService;
  private Lazy<IArticleKindDetectorService> defaultArticleKindDetectorService;
  private Lazy<IArticleLocatorService> defaultArticleLocatorService;
  private Lazy<IArticleTypesService> defaultArticleTypesService;
  private Lazy<IArticleAttributesProcessingService> defaultArticleAttributesProcessingService;
  private Lazy<IArticleStructureService> defaultArticleStructureService;
  private Lazy<IArticleDocumentationService> defaultArticleDocumentationService;
  private Lazy<IArticleFilesService> defaultArticleFilesService;
  private Lazy<IArticlePhysicalPropertiesService> defaultArticlePhysicalPropertiesService;
  private Lazy<IModelDrawingsImportService> defaultModelDrawingsImportService;

  protected MechanicalDriver()
  {
    this.sidecarObjectsExtensions = new List<ISidecarObjectsCaptureChangesExtension>();
  }

  public SaveChangesMode SaveChangesMode { get; set; }

  public bool UpdateArticles
  {
    get => this.updateArticles;
    set => this.updateArticles = value;
  }

  public bool RecalculateMass
  {
    get => this.recalculateMass;
    set => this.recalculateMass = value;
  }

  /// <summary>
  /// Возвращает коллекцию расширений для создания/обновления ассоциированных объектов IPS.
  /// Ассоциированные объекты - это вспомогательные объекты, связанные с исходными объектами
  /// косвенной связью (например, через содержимое файла исходного объекта).
  /// </summary>
  public ICollection<ISidecarObjectsCaptureChangesExtension> SidecarObjectsExtensions
  {
    get => (ICollection<ISidecarObjectsCaptureChangesExtension>) this.sidecarObjectsExtensions;
  }

  public MechanicalOperations MechanicalOperations
  {
    [DebuggerStepThrough] get => this.mechanicalOperations;
  }

  protected override void ClearDriver()
  {
    base.ClearDriver();
    this.mechanicalOperations = (MechanicalOperations) null;
    this.schedulerStages = (StandardSchedulerStages) null;
    this.defaultArticleExternalKeysService = (Lazy<IArticleExternalKeysService>) null;
    this.defaultArticleKindDetectorService = (Lazy<IArticleKindDetectorService>) null;
    this.defaultArticleLocatorService = (Lazy<IArticleLocatorService>) null;
    this.defaultArticleTypesService = (Lazy<IArticleTypesService>) null;
    this.defaultArticleAttributesProcessingService = (Lazy<IArticleAttributesProcessingService>) null;
    this.defaultArticleStructureService = (Lazy<IArticleStructureService>) null;
    this.defaultArticleDocumentationService = (Lazy<IArticleDocumentationService>) null;
    this.defaultArticleFilesService = (Lazy<IArticleFilesService>) null;
    this.defaultArticlePhysicalPropertiesService = (Lazy<IArticlePhysicalPropertiesService>) null;
    this.defaultModelDrawingsImportService = (Lazy<IModelDrawingsImportService>) null;
    this.CleanupSidecarObjectsExtensions();
  }

  private void CleanupSidecarObjectsExtensions()
  {
    if (this.SidecarObjectsExtensions.Count == 0)
      return;
    foreach (ISidecarObjectsCaptureChangesExtension objectsExtension in (IEnumerable<ISidecarObjectsCaptureChangesExtension>) this.SidecarObjectsExtensions)
      objectsExtension.Cleanup();
  }

  /// <summary>
  /// Инициализирует сервисы драйвера, которым требуется контекст текущего вызова драйвера. В момент вызова этого метода свойство <see cref="P:DriverContext" /> уже заполнено.
  /// </summary>
  protected override void InitializeDriverContextServices()
  {
    base.InitializeDriverContextServices();
    this.mechanicalOperations = new MechanicalOperations();
    this.schedulerStages = new StandardSchedulerStages(this.DriverContext.Scheduler);
    this.defaultArticleExternalKeysService = new Lazy<IArticleExternalKeysService>(new Func<IArticleExternalKeysService>(this.CreateDefaultArticleExternalKeysService));
    this.defaultArticleKindDetectorService = new Lazy<IArticleKindDetectorService>(new Func<IArticleKindDetectorService>(this.CreateDefaultArticleKindDetectorService));
    this.defaultArticleLocatorService = new Lazy<IArticleLocatorService>(new Func<IArticleLocatorService>(this.CreateDefaultArticleLocatorService));
    this.defaultArticleTypesService = new Lazy<IArticleTypesService>(new Func<IArticleTypesService>(this.CreateDefaultArticleTypesService));
    this.defaultArticleAttributesProcessingService = new Lazy<IArticleAttributesProcessingService>(new Func<IArticleAttributesProcessingService>(this.CreateDefaultArticleAttributesProcessingService));
    this.defaultArticleStructureService = new Lazy<IArticleStructureService>(new Func<IArticleStructureService>(this.CreateDefaultArticleStructureService));
    this.defaultArticleDocumentationService = new Lazy<IArticleDocumentationService>(new Func<IArticleDocumentationService>(this.CreateDefaultArticleDocumentationService));
    this.defaultArticleFilesService = new Lazy<IArticleFilesService>(new Func<IArticleFilesService>(this.CreateDefaultArticleFilesService));
    this.defaultArticlePhysicalPropertiesService = new Lazy<IArticlePhysicalPropertiesService>(new Func<IArticlePhysicalPropertiesService>(this.CreateDefaultArticlePhysicalPropertiesService));
    this.defaultModelDrawingsImportService = new Lazy<IModelDrawingsImportService>(new Func<IModelDrawingsImportService>(this.CreateDefaultModelDrawingsImportService));
    this.InitializeSidecarObjectsExtensions();
  }

  private void InitializeSidecarObjectsExtensions()
  {
    if (this.SidecarObjectsExtensions.Count == 0)
      return;
    foreach (ISidecarObjectsCaptureChangesExtension objectsExtension in (IEnumerable<ISidecarObjectsCaptureChangesExtension>) this.SidecarObjectsExtensions)
      objectsExtension.Initialize();
  }

  /// <summary>
  /// Создает используемый по умолчанию экземпляр сервиса внешний ключей изделий.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// </summary>
  /// <returns>Экземпляр сервиса, используемый по умолчанию для всех изделий, или null</returns>
  protected virtual IArticleExternalKeysService CreateDefaultArticleExternalKeysService()
  {
    return (IArticleExternalKeysService) null;
  }

  /// <summary>
  /// Создает используемый по умолчанию экземпляр сервиса для определения вида изделия и способа его обработки.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// В данном методе создается объект типа <see cref="T:ArticleKindDetectorService" />.
  /// </summary>
  /// <returns>Экземпляр сервиса, используемый по умолчанию для всех изделий, или null</returns>
  protected virtual IArticleKindDetectorService CreateDefaultArticleKindDetectorService()
  {
    return (IArticleKindDetectorService) new ArticleKindDetectorService(this, this.DriverContext);
  }

  /// <summary>
  /// Создает используемый по умолчанию экземпляр сервиса для поиска изделия в базе IPS по его описанию в документе приложения.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// В данном методе создается объект типа <see cref="T:ArticleLocatorService" />.
  /// </summary>
  /// <returns>Экземпляр сервиса, используемый по умолчанию для всех изделий, или null</returns>
  protected virtual IArticleLocatorService CreateDefaultArticleLocatorService()
  {
    return (IArticleLocatorService) new ArticleLocatorService(this, this.DriverContext);
  }

  /// <summary>
  /// Создает используемый по умолчанию экземпляр сервиса для работы с типами изделий.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// В данном методе создается объект типа <see cref="T:ArticleTypesService" />.
  /// </summary>
  /// <returns>Экземпляр сервиса, используемый по умолчанию для всех изделий, или null</returns>
  protected virtual IArticleTypesService CreateDefaultArticleTypesService()
  {
    return (IArticleTypesService) new ArticleTypesService(this, this.DriverContext);
  }

  /// <summary>
  /// Создает используемый по умолчанию экземпляр сервиса для обслуживания задачи синхронизации атрибутов изделий.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// В данном методе создается объект типа <see cref="T:ArticleAttributesProcessingService" />.
  /// </summary>
  /// <returns>Экземпляр сервиса, используемый по умолчанию для всех изделий, или null</returns>
  protected virtual IArticleAttributesProcessingService CreateDefaultArticleAttributesProcessingService()
  {
    return (IArticleAttributesProcessingService) new ArticleAttributesProcessingService(this, this.DriverContext);
  }

  /// <summary>
  /// Создает используемый по умолчанию экземпляр сервиса для работы с документацией на изделие.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// </summary>
  /// <returns>Экземпляр сервиса, используемый по умолчанию для всех изделий, или null</returns>
  protected virtual IArticleStructureService CreateDefaultArticleStructureService()
  {
    return (IArticleStructureService) null;
  }

  /// <summary>
  /// Создает используемый по умолчанию экземпляр сервиса для работы с документацией на изделие.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// В данном методе создается объект типа <see cref="T:ArticleDocumentationService" />.
  /// </summary>
  /// <returns>Экземпляр сервиса, используемый по умолчанию для всех изделий, или null</returns>
  protected virtual IArticleDocumentationService CreateDefaultArticleDocumentationService()
  {
    return (IArticleDocumentationService) new ArticleDocumentationService(this, this.DriverContext, ClientContext.FileVault);
  }

  /// <summary>
  /// Создает используемый по умолчанию экземпляр сервиса для работы с дополнительными файлами документа, относящимися к конкретному изделию.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// </summary>
  /// <returns>Экземпляр сервиса, используемый по умолчанию для всех изделий, или null</returns>
  protected virtual IArticleFilesService CreateDefaultArticleFilesService()
  {
    return (IArticleFilesService) null;
  }

  /// <summary>
  /// Создает используемый по умолчанию экземпляр сервиса для работы с физическими свойствами изделия.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// </summary>
  /// <returns>Экземпляр сервиса, используемый по умолчанию для всех изделий, или null</returns>
  protected virtual IArticlePhysicalPropertiesService CreateDefaultArticlePhysicalPropertiesService()
  {
    return (IArticlePhysicalPropertiesService) null;
  }

  /// <summary>
  /// Создает используемый по умолчанию экземпляр сервиса, обслуживающего задачи импорта чертежей моделей.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// </summary>
  /// <returns>Экземпляр сервиса, используемый по умолчанию для всех изделий, или null</returns>
  protected virtual IModelDrawingsImportService CreateDefaultModelDrawingsImportService()
  {
    return (IModelDrawingsImportService) null;
  }

  public StandardSchedulerStages SchedulerStages => this.schedulerStages;

  /// <summary>
  /// Возвращает сервис для работы с внешними ключами изделий.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// </summary>
  /// <param name="documentItem">Сущность документа</param>
  /// <returns>Сервис для работы с внешними ключами изделий или null</returns>
  /// <exception cref="T:ArgumentNullException">documentItem</exception>
  public IArticleExternalKeysService TryGetArticleExternalKeysService(SectionEntity documentItem)
  {
    return documentItem != null ? this.DoTryGetArticleExternalKeysService(documentItem) : throw new ArgumentNullException(nameof (documentItem));
  }

  /// <summary>
  /// Возвращает сервис для работы с внешними ключами изделий.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// </summary>
  /// <param name="documentItem">Сущность документа</param>
  /// <returns>Сервис для работы с внешними ключами изделий или null</returns>
  protected virtual IArticleExternalKeysService DoTryGetArticleExternalKeysService(
    SectionEntity documentItem)
  {
    return this.defaultArticleExternalKeysService.Value;
  }

  /// <summary>
  /// Возвращает сервис, обслуживающий задачи импорта чертежей моделей.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// </summary>
  /// <returns>Сервис, обслуживающий задачи импорта чертежей моделей или null</returns>
  public IModelDrawingsImportService TryGetModelDrawingsService()
  {
    return this.defaultModelDrawingsImportService.Value;
  }

  /// <summary>
  /// Возвращает сервис фасада для API документов, предоставляемого интегрируемым приложением.
  /// </summary>
  /// <param name="documentItem">Сущность документа</param>
  /// <returns>Сервис фасада</returns>
  /// <exception cref="T:ArgumentNullException">documentItem</exception>
  /// <exception cref="T:NotSupportedException">Сервис API не поддерживается интегратором</exception>
  public IDocumentCADApiService GetDocumentApiService(SectionEntity documentItem)
  {
    if (documentItem == null)
      throw new ArgumentNullException(nameof (documentItem));
    if (documentItem.Sections.Contains<FilesSection>())
    {
      IDocumentCADApiService documentApiService = this.DoTryGetDocumentApiService(documentItem);
      if (documentApiService != null)
        return documentApiService;
    }
    throw new NotSupportedException($"Не удалось получить сервис API для документа '{DisplaySection.GetQualifiedName(documentItem)}', так как он не поддерживается интегратором.");
  }

  /// <summary>
  /// Возвращает сервис фасада для API документов, предоставляемого интегрируемым приложением.
  /// </summary>
  /// <param name="documentItem">Сущность документа</param>
  /// <returns>Сервис фасада или null</returns>
  protected virtual IDocumentCADApiService DoTryGetDocumentApiService(SectionEntity documentItem)
  {
    return (IDocumentCADApiService) null;
  }

  /// <summary>
  /// Возвращает сервис фасада для API изделий, предоставляемого интегрируемым приложением.
  /// </summary>
  /// <param name="articleItem">Сущность изделия</param>
  /// <returns>Сервис фасада</returns>
  /// <exception cref="T:ArgumentNullException">articleItem</exception>
  /// <exception cref="T:NotSupportedException">Сервис API не поддерживается интегратором</exception>
  public IArticleCADApiService GetArticleApiService(SectionEntity articleItem)
  {
    if (articleItem == null)
      throw new ArgumentNullException(nameof (articleItem));
    if (articleItem.Sections.Contains<ArticleSection>())
    {
      IArticleCADApiService articleApiService = this.DoTryGetArticleApiService(articleItem);
      if (articleApiService != null)
        return articleApiService;
    }
    throw new NotSupportedException($"Не удалось получить сервис API для изделия '{DisplaySection.GetQualifiedName(articleItem)}', так как он не поддерживается интегратором.");
  }

  /// <summary>
  /// Возвращает сервис фасада для API изделий, предоставляемого интегрируемым приложением.
  /// </summary>
  /// <param name="articleItem">Сущность изделия</param>
  /// <returns>Сервис фасада или null</returns>
  protected virtual IArticleCADApiService DoTryGetArticleApiService(SectionEntity articleItem)
  {
    return (IArticleCADApiService) null;
  }

  /// <summary>
  /// Возвращает сервис для работы с составом изделия.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// </summary>
  /// <param name="articleItem">Сущность изделия</param>
  /// <returns>Сервис для работы с составом изделия или null</returns>
  /// <exception cref="T:ArgumentNullException">articleItem</exception>
  public IArticleStructureService TryGetArticleStructureService(SectionEntity articleItem)
  {
    return articleItem != null ? this.DoTryGetArticleStructureService(articleItem) : throw new ArgumentNullException(nameof (articleItem));
  }

  /// <summary>
  /// Возвращает сервис для работы с составом изделия.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// </summary>
  /// <param name="articleItem">Сущность изделия</param>
  /// <returns>Сервис для работы с составом изделия или null</returns>
  protected virtual IArticleStructureService DoTryGetArticleStructureService(
    SectionEntity articleItem)
  {
    return this.defaultArticleStructureService.Value;
  }

  /// <summary>
  /// Возвращает сервис для работы с документацией на изделие.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// </summary>
  /// <param name="articleItem">Сущность изделия</param>
  /// <returns>Сервис для работы с документаций на изделие или null</returns>
  /// <exception cref="T:ArgumentNullException">articleItem</exception>
  public IArticleDocumentationService TryGetArticleDocumentationService(SectionEntity articleItem)
  {
    return articleItem != null ? this.DoTryGetArticleDocumentationService(articleItem) : throw new ArgumentNullException(nameof (articleItem));
  }

  /// <summary>
  /// Возвращает сервис для работы с документацией на изделие.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// По умолчанию используется реализация <see cref="T:ArticleDocumentationService" />.
  /// </summary>
  /// <param name="articleItem">Сущность изделия</param>
  /// <returns>Сервис для работы с документаций на изделие или null</returns>
  protected virtual IArticleDocumentationService DoTryGetArticleDocumentationService(
    SectionEntity articleItem)
  {
    return this.defaultArticleDocumentationService.Value;
  }

  /// <summary>
  /// Возвращает сервис для определения вида изделия.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// </summary>
  /// <param name="articleItem">Сущность изделия</param>
  /// <returns>Сервис для работы определения вида изделия или null</returns>
  /// <exception cref="T:ArgumentNullException">articleItem</exception>
  public IArticleKindDetectorService TryGetArticleKindDetectorService(SectionEntity articleItem)
  {
    return articleItem != null ? this.DoTryGetArticleKindDetectorService(articleItem) : throw new ArgumentNullException(nameof (articleItem));
  }

  /// <summary>
  /// Возвращает сервис для определения вида изделия.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// По умолчанию используется реализация <see cref="T:ArticleKindDetectorService" />.
  /// </summary>
  /// <param name="articleItem">Сущность изделия</param>
  /// <returns>Сервис для работы определения вида изделия или null</returns>
  protected virtual IArticleKindDetectorService DoTryGetArticleKindDetectorService(
    SectionEntity articleItem)
  {
    return this.defaultArticleKindDetectorService.Value;
  }

  /// <summary>
  /// Возвращает сервис для поиска изделия в базе IPS по его описанию в документе приложения.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// </summary>
  /// <param name="articleItem">Сущность изделия</param>
  /// <returns>Сервис для работы определения вида изделия или null</returns>
  /// <exception cref="T:ArgumentNullException">articleItem</exception>
  public IArticleLocatorService TryGetArticleLocatorService(SectionEntity articleItem)
  {
    return articleItem != null ? this.DoTryGetArticleLocatorService(articleItem) : throw new ArgumentNullException(nameof (articleItem));
  }

  /// <summary>
  /// Возвращает сервис для поиска изделия в базе IPS по его описанию в документе приложения.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// По умолчанию используется реализация <see cref="T:ArticleLocatorService" />.
  /// </summary>
  /// <param name="articleItem">Сущность изделия</param>
  /// <returns>Сервис для работы определения вида изделия или null</returns>
  protected virtual IArticleLocatorService DoTryGetArticleLocatorService(SectionEntity articleItem)
  {
    return this.defaultArticleLocatorService.Value;
  }

  /// <summary>
  /// Возвращает сервис для работы с типами изделий.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// </summary>
  /// <param name="articleItem">Сущность изделия</param>
  /// <returns>Сервис для работы с типами изделий или null</returns>
  /// <exception cref="T:ArgumentNullException">articleItem</exception>
  public IArticleTypesService TryGetArticleTypesService(SectionEntity articleItem)
  {
    return articleItem != null ? this.DoTryGetArticleTypesService(articleItem) : throw new ArgumentNullException(nameof (articleItem));
  }

  /// <summary>
  /// Возвращает сервис для работы с типами изделий.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// </summary>
  /// <param name="articleItem">Сущность изделия</param>
  /// <returns>Сервис для работы с типами изделий или null</returns>
  protected virtual IArticleTypesService DoTryGetArticleTypesService(SectionEntity articleItem)
  {
    return this.defaultArticleTypesService.Value;
  }

  /// <summary>
  /// Возвращает сервис для обслуживания задачи синхронизации атрибутов изделий.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// По умолчанию используется реализация <see cref="T:ArticleAttributesProcessingService" />.
  /// </summary>
  /// <param name="articleItem">Сущность изделия</param>
  /// <returns>Сервис для обслуживания задачи синхронизации атрибутов изделий или null</returns>
  /// <exception cref="T:ArgumentNullException">articleItem</exception>
  public IArticleAttributesProcessingService TryGetArticleAttributesProcessingService(
    SectionEntity articleItem)
  {
    return articleItem != null ? this.DoTryGetArticleAttributesProcessingService(articleItem) : throw new ArgumentNullException(nameof (articleItem));
  }

  /// <summary>
  /// Возвращает сервис для обслуживания задачи синхронизации атрибутов изделий.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// По умолчанию используется реализация <see cref="T:ArticleAttributesProcessingService" />.
  /// </summary>
  /// <param name="articleItem">Сущность изделия</param>
  /// <returns>Сервис для обслуживания задачи синхронизации атрибутов изделий или null</returns>
  /// <exception cref="T:ArgumentNullException">articleItem</exception>
  protected virtual IArticleAttributesProcessingService DoTryGetArticleAttributesProcessingService(
    SectionEntity articleItem)
  {
    return this.defaultArticleAttributesProcessingService.Value;
  }

  /// <summary>
  /// Возвращает сервис для работы с дополнительными файлами документа, относящимися к конкретному изделию.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// </summary>
  /// <param name="articleItem">Сущность изделия</param>
  /// <returns>Сервис для работы с файлами документа или null</returns>
  /// <exception cref="T:ArgumentNullException">articleItem</exception>
  public IArticleFilesService TryGetArticleFilesService(SectionEntity articleItem)
  {
    return articleItem != null ? this.DoTryGetArticleFilesService(articleItem) : throw new ArgumentNullException(nameof (articleItem));
  }

  /// <summary>
  /// Возвращает сервис для работы с дополнительными файлами документа, относящимися к конкретному изделию.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// </summary>
  /// <param name="articleItem">Сущность изделия</param>
  /// <returns>Сервис для работы с файлами документа или null</returns>
  protected virtual IArticleFilesService DoTryGetArticleFilesService(SectionEntity articleItem)
  {
    return this.defaultArticleFilesService.Value;
  }

  /// <summary>
  /// Возвращает сервис для работы с физическими свойствами изделия.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// </summary>
  /// <param name="articleItem">Сущность изделия</param>
  /// <returns>Сервис для работы с физическими свойствами изделия или null</returns>
  /// <exception cref="T:ArgumentNullException">articleItem</exception>
  public IArticlePhysicalPropertiesService TryGetArticlePhysicalPropertiesService(
    SectionEntity articleItem)
  {
    return articleItem != null ? this.DoTryGetArticlePhysicalPropertiesService(articleItem) : throw new ArgumentNullException(nameof (articleItem));
  }

  /// <summary>
  /// Возвращает сервис для работы с физическими свойствами изделия.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// </summary>
  /// <param name="articleItem">Сущность изделия</param>
  /// <returns>Сервис для работы с физическими свойствами изделия или null</returns>
  protected virtual IArticlePhysicalPropertiesService DoTryGetArticlePhysicalPropertiesService(
    SectionEntity articleItem)
  {
    return this.defaultArticlePhysicalPropertiesService.Value;
  }

  protected override IAction CreateTypedDocumentHandler(
    SectionEntity docItem,
    object documentKind,
    int documentType)
  {
    if (!(documentKind is MechanicalDocumentKind notSupportedValue))
      throw new NotSupportedException($"Не удалось создать обработчик документа, так как не реализована поддержка документов вида '{documentKind}' ('{documentKind.GetType()}').");
    switch (notSupportedValue)
    {
      case MechanicalDocumentKind.AssemblyModel:
        return (IAction) this.CreateModelDocumentHandler(docItem);
      case MechanicalDocumentKind.PartModel:
        return (IAction) this.CreateModelDocumentHandler(docItem);
      case MechanicalDocumentKind.StandardModel:
        return this.CreateStandardPartDocumentHandler(docItem);
      case MechanicalDocumentKind.AssemblyDrawing:
        return (IAction) this.CreateDrawingDocumentHandler(docItem);
      case MechanicalDocumentKind.PartDrawing:
        return (IAction) this.CreateDrawingDocumentHandler(docItem);
      case MechanicalDocumentKind.GenericDocument:
        return (IAction) this.CreateGenericDocumentHandler(docItem);
      default:
        throw new NotSupportedEnumException((Enum) notSupportedValue);
    }
  }

  protected virtual ModelHandler CreateModelDocumentHandler(SectionEntity docItem)
  {
    return new ModelHandler(this, this.DriverContext, docItem);
  }

  private IAction CreateStandardPartDocumentHandler(SectionEntity docItem)
  {
    return (IAction) new StandardPartHandler(this, this.DriverContext, docItem);
  }

  protected virtual DrawingHandler CreateDrawingDocumentHandler(SectionEntity docItem)
  {
    return new DrawingHandler(this, this.DriverContext, docItem);
  }

  protected virtual GenericHandler CreateGenericDocumentHandler(SectionEntity docItem)
  {
    return new GenericHandler(this, this.DriverContext, docItem);
  }

  protected override IAction CreateUntypedDocumentHandler(
    SectionEntity docItem,
    List<object> documentKinds,
    List<LocalId<int>> documentTypes)
  {
    if (documentKinds.Count != 2 || !documentKinds.Contains((object) MechanicalDocumentKind.AssemblyDrawing) || !documentKinds.Contains((object) MechanicalDocumentKind.PartDrawing))
      return base.CreateUntypedDocumentHandler(docItem, documentKinds, documentTypes);
    return (IAction) new MultiKindDrawingHandler(this, this.DriverContext, docItem)
    {
      ScheduleAdapter = DocumentScheduleAdapter.FromStandardScheduler(this.SchedulerStages)
    };
  }

  protected override void SetupDocumentHandler(SectionEntity docItem, IAction documentHandler)
  {
    base.SetupDocumentHandler(docItem, documentHandler);
    if (!(documentHandler is DocumentHandlerBase documentHandlerBase))
      return;
    documentHandlerBase.ScheduleAdapter = DocumentScheduleAdapter.FromStandardScheduler(this.SchedulerStages);
  }

  /// <summary>
  /// <para>
  /// Позволяет определить тип для нового импортируемого документа, прочитав его из файла документа. Если тип документа не может быть
  /// определен однозначно, то метод должен вернуть все возможные типы документов. Если множество возможных типов не является
  /// ограниченным, то этот метод должен вернуть пустой список, а фактический выбор типа для документа должен быть реализован в методе
  /// <see cref="M:DetectFallbackDocumentType" />.</para>
  /// <para>
  /// Этот метод вызывается даже тогда, когда метод <see cref="M:GetDocumentTypeParameterName" /> возвращает null или пустую строку.
  /// Так сделано потому, что иногда тип документа можно определить эвристически без явного хранения имени типа в файле документа.
  /// При реализации метода также нужно учитывать, что он вызывается в самом начале анализа импортируемого документа, и его рабочий элемент практически пуст.</para>
  /// </summary>
  /// <param name="docItem">Рабочий элемент документа</param>
  /// <returns>Список возможных типов для импортируемого документа</returns>
  protected override List<LocalId<int>> DetectNewDocumentType(SectionEntity docItem)
  {
    return this.GetDocumentApiService(docItem).DetectNewDocumentType(docItem);
  }

  public MechanicalDocumentKind? TryGetMechanicalDocumentKind(SectionEntity documentItem)
  {
    return documentItem != null ? this.TryGetMechanicalDocumentKind(documentItem.Sections) : throw new ArgumentNullException(nameof (documentItem));
  }

  public MechanicalDocumentKind? TryGetMechanicalDocumentKind(SectionCollection documentSections)
  {
    if (documentSections == null)
      throw new ArgumentNullException(nameof (documentSections));
    object documentKind;
    if (!this.TryGetDocumentKind(documentSections, out documentKind))
      return new MechanicalDocumentKind?();
    return documentKind is MechanicalDocumentKind mechanicalDocumentKind ? new MechanicalDocumentKind?(mechanicalDocumentKind) : new MechanicalDocumentKind?(MechanicalDocumentKind.GenericDocument);
  }

  /// <summary>
  /// Переводит тип документа IPS в вид документа приложения, который используется для выбора обработчика документа. Каждому виду документов соответствует свой обработчик.
  /// </summary>
  /// <param name="documentType">Идентификатор типа документа</param>
  /// <returns>Идентификатор вида документа приложения</returns>
  protected override object DoMapDocumentTypeToKind(int documentType)
  {
    return (object) this.GetMechanicalDocumentKindByType(documentType);
  }

  /// <summary>
  /// По указанному типу документа IPS возвращает вид конструкторского документа в рамках унифицированной модели обработки.
  /// Этот метод используется для определения способа обработки документов, уже зарегистрированных в IPS.
  /// </summary>
  /// <param name="documentType">Идентификатор типа документа IPS</param>
  /// <returns>Вид документа в рамках унифицированной модели обработки конструкторских документа</returns>
  public abstract MechanicalDocumentKind GetMechanicalDocumentKindByType(int documentType);

  /// <summary>
  /// По указанному виду конструкторского документа возвращает список соответствующих ему типов документов IPS.
  /// Этот метод используется при определении типов новых документов, импортируемых в IPS. Метод является
  /// зеркальным для метода GetMechanicalDocumentKindByType.
  /// </summary>
  /// <returns>Вид документа в рамках унифицированной модели обработки конструкторских документа</returns>
  /// <returns>Список идентификаторов типов документов IPS</returns>
  public abstract List<LocalId<int>> GetTypesByMechanicalDocumentKind(
    MechanicalDocumentKind documentKind);

  internal NormalArticleHandler CreateAndSetupNormalArticleHandler(SectionEntity articleEntity)
  {
    NormalArticleHandler normalArticleHandler = this.CreateNormalArticleHandler(articleEntity);
    this.SetupArticleHandler(articleEntity, (IAction) normalArticleHandler);
    return normalArticleHandler;
  }

  protected virtual NormalArticleHandler CreateNormalArticleHandler(SectionEntity articleEntity)
  {
    return new NormalArticleHandler(this, this.DriverContext, articleEntity);
  }

  internal ImbaseObjectArticleHandler CreateAndSetupImbaseObjectArticleHandler(
    SectionEntity articleEntity)
  {
    ImbaseObjectArticleHandler objectArticleHandler = this.CreateImbaseObjectArticleHandler(articleEntity);
    this.SetupArticleHandler(articleEntity, (IAction) objectArticleHandler);
    return objectArticleHandler;
  }

  protected virtual ImbaseObjectArticleHandler CreateImbaseObjectArticleHandler(
    SectionEntity articleEntity)
  {
    return new ImbaseObjectArticleHandler(this, this.DriverContext, articleEntity);
  }

  internal MinorMaterialArticleHandler CreateAndSetupMinorMaterialArticleHandler(
    SectionEntity articleEntity)
  {
    MinorMaterialArticleHandler materialArticleHandler = this.CreateMinorMaterialArticleHandler(articleEntity);
    this.SetupArticleHandler(articleEntity, (IAction) materialArticleHandler);
    return materialArticleHandler;
  }

  protected virtual MinorMaterialArticleHandler CreateMinorMaterialArticleHandler(
    SectionEntity articleEntity)
  {
    return new MinorMaterialArticleHandler(this, this.DriverContext, articleEntity);
  }

  protected virtual void SetupArticleHandler(SectionEntity articleEntity, IAction articleHandler)
  {
  }
}
