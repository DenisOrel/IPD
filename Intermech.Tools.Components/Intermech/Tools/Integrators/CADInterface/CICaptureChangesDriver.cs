// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CICaptureChangesDriver
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.Client.Core;
using Intermech.Collections;
using Intermech.ControlFlow;
using Intermech.Data.EntityDb;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.Mechanical;
using Intermech.Tools.Integrators.Notifications;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>Создает объект.</summary>
/// <param name="integrator">Ссылка на объект интегратора</param>
public class CICaptureChangesDriver(IIntegrator integrator) : AppMechanicalDriver(integrator)
{
  private CADSystemProxy cadSystem;
  private IApplicationFileTypes ftManager;
  private IModelDrawingsService modelDrawingsSvc;
  private ICADInterfaceService cadApiService;
  private ICADSettingsService settingsSvc;
  private CADSettings integratorSettings;
  private CIDocumentApiService commonDocumentApi;
  private CIArticleApiService commonArticleApi;

  public CADSettings IntegratorSettings
  {
    [DebuggerStepThrough] get => this.integratorSettings;
  }

  public CADSystemProxy CADSystem
  {
    [DebuggerStepThrough] get => this.cadSystem;
  }

  public ICADInterfaceService ApiService
  {
    [DebuggerStepThrough] get => this.cadApiService;
  }

  public IModelDrawingsService ModelDrawingsService
  {
    [DebuggerStepThrough] get => this.modelDrawingsSvc;
  }

  protected override void InitializeDriver()
  {
    base.InitializeDriver();
    this.cadSystem = (CADSystemProxy) ServiceUtils.GetService<IApplicationApiService>((object) this.Integrator, true).GetApplicationObject();
    this.ftManager = ServiceUtils.GetService<IApplicationFileTypes>((object) this.Integrator, true);
    this.modelDrawingsSvc = ServiceUtils.GetService<IModelDrawingsService>((object) this.Integrator, true);
    this.cadApiService = ServiceUtils.GetService<ICADInterfaceService>((object) this.Integrator, true);
    this.settingsSvc = ServiceUtils.GetService<ICADSettingsService>((object) this.Integrator, true);
    this.integratorSettings = this.settingsSvc.GetCADSettings();
  }

  protected override void ClearDriver()
  {
    base.ClearDriver();
    this.cadSystem = (CADSystemProxy) null;
    this.ftManager = (IApplicationFileTypes) null;
    this.modelDrawingsSvc = (IModelDrawingsService) null;
    this.cadApiService = (ICADInterfaceService) null;
    this.settingsSvc = (ICADSettingsService) null;
    this.integratorSettings = (CADSettings) null;
    this.commonDocumentApi = (CIDocumentApiService) null;
    this.commonArticleApi = (CIArticleApiService) null;
  }

  /// <summary>
  /// Инициализирует сервисы драйвера, которым требуется контекст текущего вызова драйвера. В момент вызова этого метода свойство <see cref="P:DriverContext" /> уже заполнено.
  /// </summary>
  protected override void InitializeDriverContextServices()
  {
    base.InitializeDriverContextServices();
    this.commonDocumentApi = new CIDocumentApiService(this, this.DriverContext);
    this.commonArticleApi = new CIArticleApiService(this, this.DriverContext);
  }

  /// <summary>
  /// Создает используемый по умолчанию экземпляр сервиса внешний ключей изделий.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// </summary>
  /// <returns>Экземпляр сервиса, используемый по умолчанию для всех изделий, или null</returns>
  protected override IArticleExternalKeysService CreateDefaultArticleExternalKeysService()
  {
    return (IArticleExternalKeysService) new CIModelExternalKeysService(this, this.DriverContext);
  }

  /// <summary>
  /// Создает используемый по умолчанию экземпляр сервиса, обслуживающего задачи импорта чертежей моделей.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// </summary>
  /// <returns>Экземпляр сервиса, используемый по умолчанию для всех изделий, или null</returns>
  protected override IModelDrawingsImportService CreateDefaultModelDrawingsImportService()
  {
    return (IModelDrawingsImportService) new CIModelDrawingsImportService(this, this.DriverContext, this.modelDrawingsSvc);
  }

  /// <summary>
  /// Создает используемый по умолчанию экземпляр сервиса для работы с типами изделий.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// </summary>
  /// <returns>Экземпляр сервиса, используемый по умолчанию для всех изделий, или null</returns>
  protected override IArticleTypesService CreateDefaultArticleTypesService()
  {
    return (IArticleTypesService) new CIArticleTypesService((MechanicalDriver) this, this.DriverContext);
  }

  /// <summary>
  /// Создает используемый по умолчанию экземпляр сервиса для определения вида изделия и способа его обработки.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// </summary>
  /// <returns>Экземпляр сервиса, используемый по умолчанию для всех изделий, или null</returns>
  protected override IArticleKindDetectorService CreateDefaultArticleKindDetectorService()
  {
    return (IArticleKindDetectorService) new CIArticleKindDetectorService((MechanicalDriver) this, this.DriverContext, this.ApiService);
  }

  /// <summary>
  /// Создает используемый по умолчанию экземпляр сервиса для поиска изделия в базе IPS по его описанию в документе приложения.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// </summary>
  /// <returns>Экземпляр сервиса, используемый по умолчанию для всех изделий, или null</returns>
  protected override IArticleLocatorService CreateDefaultArticleLocatorService()
  {
    return (IArticleLocatorService) new CIArticleLocatorService((MechanicalDriver) this, this.DriverContext, this.ApiService);
  }

  /// <summary>
  /// Создает используемый по умолчанию экземпляр сервиса для работы с документацией на изделие.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// </summary>
  /// <returns>Экземпляр сервиса, используемый по умолчанию для всех изделий, или null</returns>
  protected override IArticleStructureService CreateDefaultArticleStructureService()
  {
    return (IArticleStructureService) new CIArticleStructureService(this, this.DriverContext, this.ApiService);
  }

  /// <summary>
  /// Создает используемый по умолчанию экземпляр сервиса для работы с документацией на изделие.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// </summary>
  /// <returns>Экземпляр сервиса, используемый по умолчанию для всех изделий, или null</returns>
  protected override IArticleDocumentationService CreateDefaultArticleDocumentationService()
  {
    return (IArticleDocumentationService) new CIArticleDocumentationService(this, this.DriverContext, ClientContext.FileVault, this.modelDrawingsSvc);
  }

  /// <summary>
  /// Создает используемый по умолчанию экземпляр сервиса для работы с дополнительными файлами документа, относящимися к конкретному изделию.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// </summary>
  /// <returns>Экземпляр сервиса, используемый по умолчанию для всех изделий, или null</returns>
  protected override IArticleFilesService CreateDefaultArticleFilesService()
  {
    return (IArticleFilesService) new CIArticleFilesService((MechanicalDriver) this, this.DriverContext);
  }

  /// <summary>
  /// Создает используемый по умолчанию экземпляр сервиса для работы с физическими свойствами изделия.
  /// Если такая возможность не поддерживается интегратором, то метод может вернуть null.
  /// </summary>
  /// <returns>Экземпляр сервиса, используемый по умолчанию для всех изделий, или null</returns>
  protected override IArticlePhysicalPropertiesService CreateDefaultArticlePhysicalPropertiesService()
  {
    return (IArticlePhysicalPropertiesService) new CIArticlePhysicalPropertiesService((MechanicalDriver) this, this.DriverContext);
  }

  /// <summary>
  /// Реализует проверку стартового файла и его документа, которая необходима для защиты от неподдерживаемых типов файлов и документов.
  /// Если файл или его документ не проходит проверку, то метод должен сбросить исключение типа <see cref="T:Intermech.FaultException" />.
  /// </summary>
  /// <param name="rootFilePath">Абсолютный путь к стартовому файлу, находящемуся в рабочей области файлового хранилища</param>
  /// <param name="rootObjectId">Идентификатор версии сохраняемого документа. Может быть не задан, если выполняется импорт нового файла</param>
  /// <exception cref="T:Intermech.FaultException">Неподдерживаемый тип файла или неподдерживаемый документ</exception>
  protected override void ValidateRootFile(string rootFilePath, long rootObjectId)
  {
    base.ValidateRootFile(rootFilePath, rootObjectId);
    this.ValidateSupportedApplicationFile(rootFilePath);
  }

  private void ValidateSupportedApplicationFile(string rootFilePath)
  {
    if (!this.ftManager.IsApplicationFile(rootFilePath))
      throw new FaultException(string.Format(LocalizationHolder.rm.GetString("Tools.Components_342"), (object) rootFilePath));
  }

  /// <summary>
  /// Открывает стартовый файл. Дополнительно метод может выполнять проверку содержимого открытого стартового файла,
  /// если необходима защита от неподдерживаемого содержимого. Если файл не проходит проверку, то метод должен сбросить исключение типа <see cref="T:Intermech.FaultException" />.
  /// </summary>
  /// <remarks>
  /// В режиме импорта может быть открыт не тот файл, который указан, а более подходящий. Это необходимо для корректного импорта многофайловых документов,
  /// когда пользователь выбирает не основной файл документа, а вспомогательный.
  /// </remarks>
  /// <param name="rootDocumentItem">Сущность документа</param>
  /// <param name="rootFilePath">Абсолютный путь к стартовому файлу, находящемуся в рабочей области файлового хранилища</param>
  /// <param name="rootObjectId">Идентификатор версии сохраняемого документа. Может быть не задан, если выполняется импорт нового файла</param>
  /// <param name="useExactFilePath">true - следует использовать указанный путь к файлу, false - следует использовать для открытия основной файл документа вместо указанного, если он является вспомогательным файлом. Этот режим используется при импорте новый файлов документов</param>
  /// <returns>Открытый документ</returns>
  /// <exception cref="T:Intermech.FaultException">Неподходящее содержимое файла</exception>
  protected override DocumentFileData OpenRootFile(
    SectionEntity rootDocumentItem,
    string rootFilePath,
    long rootObjectId,
    bool useExactFilePath = true)
  {
    DocumentFileData openRootFile = base.OpenRootFile(rootDocumentItem, rootFilePath, rootObjectId, useExactFilePath);
    if (rootObjectId == 0L)
    {
      this.ValidateAllowImportAsDocument(openRootFile);
      this.ValidateAllowImportByPDMFlag(openRootFile);
    }
    if (!useExactFilePath)
    {
      CADDocumentProxy document = openRootFile.CustomSections.Get<CIDocumentData>().Document;
      if (!document.IsMasterDocument && !string.IsNullOrEmpty(document.MasterFile))
        openRootFile = this.OpenDocumentFile(rootDocumentItem, document.MasterFile);
    }
    return openRootFile;
  }

  /// <summary>
  /// Проверяет, можно ли импортировать стартовый файл в виде самостоятельного документа.
  /// </summary>
  /// <param name="openRootFile">Открытый стартовый файл</param>
  /// <exception cref="T:System.Exception">Настройки интегратора запрещают импортировать файл в виде самостоятельного документа</exception>
  protected virtual void ValidateAllowImportAsDocument(DocumentFileData openRootFile)
  {
    this.ValidateAllowImportDrawingAsDocument(openRootFile);
  }

  /// <summary>
  /// Проверяет, можно ли импортировать стартовый файл чертежа в виде самостоятельного документа.
  /// </summary>
  /// <param name="openRootFile">Открытый стартовый файл</param>
  /// <exception cref="T:System.Exception">Настройки интегратора запрещают импортировать файл в виде самостоятельного документа</exception>
  private void ValidateAllowImportDrawingAsDocument(DocumentFileData openRootFile)
  {
    if (ServiceUtils.GetService<IModelDrawingsService>((object) this.Integrator, true).IsDrawingFileName(openRootFile.DocumentFilePath) && this.IntegratorSettings.NewDrawingMode == NewDrawingMode.AdditionalModelFile)
      throw new FaultException($"Нельзя импортировать чертеж '{openRootFile.DocumentFilePath}' как документ IPS. Настройки интегратора позволяют импортировать чертежи только как дополнительные файлы к одноименной 3D-модели.");
  }

  /// <summary>
  /// Проверяет, не запрещен ли импорт файла в качестве документа с помощью PDM-флага.
  /// </summary>
  /// <param name="openRootFile">Открытый стартовый файл</param>
  /// <exception cref="T:System.Exception">PDM-флаг в свойствах файла запрещает импортировать этот файл в виде самостоятельного документа</exception>
  protected virtual void ValidateAllowImportByPDMFlag(DocumentFileData openRootFile)
  {
    CADDocumentProxy document = openRootFile.CustomSections.Get<CIDocumentData>().Document;
    if (CADDocumentHelper.IsDocumentImportDenied((IServiceProvider) this.Integrator, document))
      throw new FaultException(string.Format(LocalizationHolder.rm.GetString("SR_537"), (object) openRootFile.DocumentFilePath, (object) CADDocumentHelper.TryReadGlobalPDMFlag((IServiceProvider) this.Integrator, document)));
  }

  /// <summary>
  /// Возвращает список типов секций, которые драйвер использует для хранения своих временных данных.
  /// Этот метод используется в процессе очистки базы данных контекста для определения секций, которые нужно удалить.
  /// </summary>
  /// <returns>Коллекция типов секций, которые нужно удалить из базы данных контекста</returns>
  protected override ICollection<Type> GetRemovableSectionTypes()
  {
    ICollection<Type> removableSectionTypes = base.GetRemovableSectionTypes();
    removableSectionTypes.Add(typeof (CIDocumentData));
    removableSectionTypes.Add(typeof (CIArticleData));
    removableSectionTypes.Add(typeof (CIArticleStructureCache));
    removableSectionTypes.Add(typeof (CISatelliteModelWithArticles));
    return removableSectionTypes;
  }

  /// <summary>
  /// Вызывается в самом конце после успешного завершения процесса.
  /// Метод может использоваться драйвером для извлечения полезных сведений из рабочего контекста.
  /// </summary>
  protected override void DoPostprocess()
  {
    base.DoPostprocess();
    this.RaiseNotifications();
  }

  private void RaiseNotifications()
  {
    INotificationService service = ServiceUtils.GetService<INotificationService>((object) ServicesManager.ServiceContainer, true);
    if (service.HasSubscribers(CaptureChangesEventArgs.CaptureChangesCompleted))
      this.RaiseCaptureChangesCompletedNotification(service);
    if (!service.HasSubscribers(IMShapeEventArgs.UpdateDB))
      return;
    this.RaiseUpdateIMShapeDBNotification(service);
  }

  private void RaiseCaptureChangesCompletedNotification(INotificationService notificationService)
  {
    EntitySet entitySet = this.DriverContext.Database.Query((IQueryCondition) new CompoundSetCondition(CompoundSetOperator.Intersection, new IQueryCondition[2]
    {
      (IQueryCondition) new BinaryCondition(SectionVirtualProperties.SectionTypeRef, BinaryOperator.Equal, (object) typeof (CIDocumentData)),
      (IQueryCondition) new BinaryCondition(SectionVirtualProperties.SectionTypeRef, BinaryOperator.Equal, (object) typeof (ObjectSection))
    }));
    List<CaptureChangesDocumentInfo> documents = new List<CaptureChangesDocumentInfo>(entitySet.Count);
    foreach (SectionEntity sectionEntity in (HashSet<IEntity>) entitySet)
    {
      long objectId = ObjectSection.GetObjectId(sectionEntity);
      int objectType = ObjectSection.GetObjectType(sectionEntity);
      string masterFile = FilesSection.GetMasterFile(sectionEntity);
      bool isInitialDocument = this.DriverContext.Database.IsEntryPointDocument(sectionEntity);
      bool db = this.IsSavedToDB(sectionEntity);
      documents.Add(new CaptureChangesDocumentInfo(objectId, objectType, masterFile, isInitialDocument, db));
    }
    CaptureChangesEventArgs e = new CaptureChangesEventArgs(CaptureChangesEventArgs.CaptureChangesCompleted, this.SaveChangesMode, this.UpdateArticles, this.Integrator, documents);
    notificationService.FireEvent((object) null, (NotificationEventArgs) e);
  }

  private void RaiseUpdateIMShapeDBNotification(INotificationService notificationService)
  {
    List<int> intList = this.IntegratorSettings.FileDocumentGroups.FindByName("Part", true).AsIdList();
    EntitySet entitySet = this.DriverContext.Database.Query(intList.Count != 1 ? (IQueryCondition) new CompoundSetCondition(CompoundSetOperator.Union, (IEnumerable<IQueryCondition>) intList.ConvertAll<BinaryCondition>((Converter<int, BinaryCondition>) (item => new BinaryCondition((object) ObjectSection.ObjectTypeRef, BinaryOperator.Equal, (object) item)))) : (IQueryCondition) new BinaryCondition((object) ObjectSection.ObjectTypeRef, BinaryOperator.Equal, (object) intList[0]));
    List<IMShapeDocumentInfo> documents = new List<IMShapeDocumentInfo>(entitySet.Count);
    foreach (SectionEntity sectionEntity in (HashSet<IEntity>) entitySet)
    {
      if (this.IsSavedToDB(sectionEntity))
      {
        long objectId = ObjectSection.GetObjectId(sectionEntity);
        int objectType = ObjectSection.GetObjectType(sectionEntity);
        string masterFile = FilesSection.GetMasterFile(sectionEntity);
        documents.Add(new IMShapeDocumentInfo(objectId, objectType, masterFile));
      }
    }
    if (documents.Count == 0)
      return;
    IMShapeEventArgs e = new IMShapeEventArgs(IMShapeEventArgs.UpdateDB, this.Integrator, documents);
    notificationService.FireEvent((object) null, (NotificationEventArgs) e);
  }

  private bool IsSavedToDB(SectionEntity objectEntity)
  {
    ObjectActionsSection objectActionsSection = objectEntity.Sections.Get<ObjectActionsSection>((ObjectActionsSection) null);
    return objectActionsSection != null && (objectActionsSection.ObjectActions.ServerActions.Count != 0 || objectActionsSection.RelationActions.ServerActions.Count != 0);
  }

  /// <summary>Возвращает true, если поддержка допзамен включена.</summary>
  /// <param name="documentItem">Сущность документа</param>
  /// <returns>true - поддержка допзамен включена, false - поддержка допзамен выключена</returns>
  protected internal virtual bool CanSynchronizeSubstitutions(SectionEntity documentItem)
  {
    if (documentItem == null)
      throw new ArgumentNullException(nameof (documentItem));
    return this.IntegratorSettings.SynchronizeSubstitutions;
  }

  /// <summary>
  /// Возвращает сервис фасада для API документов, предоставляемого интегрируемым приложением.
  /// </summary>
  /// <param name="documentItem">Сущность документа</param>
  /// <returns>Сервис фасада или null</returns>
  protected override IDocumentCADApiService DoTryGetDocumentApiService(SectionEntity documentItem)
  {
    return documentItem.Sections.Contains<CIDocumentData>() ? (IDocumentCADApiService) this.commonDocumentApi : base.DoTryGetDocumentApiService(documentItem);
  }

  /// <summary>
  /// Возвращает сервис фасада для API изделий, предоставляемого интегрируемым приложением.
  /// </summary>
  /// <param name="articleItem">Сущность изделия</param>
  /// <returns>Сервис фасада или null</returns>
  protected override IArticleCADApiService DoTryGetArticleApiService(SectionEntity articleItem)
  {
    return articleItem.Sections.Contains<CIArticleData>() ? (IArticleCADApiService) this.commonArticleApi : base.DoTryGetArticleApiService(articleItem);
  }

  protected override void BeginAnalyzeDocuments(IEnumerable<SectionEntity> rootDocuments)
  {
    this.cadSystem.BeginGroupOperation(GroupOperationTypes.General);
    base.BeginAnalyzeDocuments(rootDocuments);
  }

  protected override void EndAnalyzeDocuments(IEnumerable<SectionEntity> rootDocuments)
  {
    base.EndAnalyzeDocuments(rootDocuments);
    this.cadSystem.EndGroupOperation();
  }

  /// <summary>Позволяет открыть документ.</summary>
  /// <param name="documentItem">Элемент документа в базе данных контекста</param>
  /// <param name="fullPath">Абсолютный путь к файлу документа</param>
  /// <returns>Открытый документ</returns>
  /// <exception cref="T:ArgumentNullException">documentItem || fullPath</exception>
  public override DocumentFileData OpenDocumentFile(SectionEntity documentItem, string fullPath)
  {
    if (documentItem == null)
      throw new ArgumentNullException(nameof (documentItem));
    bool openVisible = fullPath != null ? this.GetDocumentOpenVisibleMode(documentItem, fullPath) : throw new ArgumentNullException(nameof (fullPath));
    CADDocumentProxy cadDocument = this.CADSystem.OpenDocument(fullPath, openVisible);
    return CIDocumentHelper.ReadDocumentData(fullPath, cadDocument);
  }

  protected virtual bool GetDocumentOpenVisibleMode(SectionEntity documentItem, string fullPath)
  {
    return false;
  }

  /// <summary>
  /// Возвращает true, если документы указанного типа могут быть обработаны интегратором.
  /// </summary>
  /// <param name="documentType">Идентификатор типа документа</param>
  /// <returns>Признак возможности обработки</returns>
  public override bool IsDocumentTypeSupported(int documentType)
  {
    return this.IntegratorSettings.FileDocumentGroups.FindByDocumentType(documentType, false) != null || this.IntegratorSettings.StandardPartType != null && this.IntegratorSettings.StandardPartType.Id == documentType || this.IntegratorSettings.JTDerivativesEnabled && this.IntegratorSettings.JTDerivedDocumentType.Id == documentType;
  }

  /// <summary>
  /// По указанному типу документа IPS возвращает вид конструкторского документа в рамках унифицированной модели обработки.
  /// Этот метод используется для определения способа обработки документов, уже зарегистрированных в IPS.
  /// </summary>
  /// <param name="documentType">Идентификатор типа документа IPS</param>
  /// <returns>Вид документа в рамках унифицированной модели обработки конструкторских документа</returns>
  public override MechanicalDocumentKind GetMechanicalDocumentKindByType(int documentType)
  {
    DocumentGroup byDocumentType = this.IntegratorSettings.FileDocumentGroups.FindByDocumentType(documentType, false);
    if (byDocumentType != null)
    {
      switch (byDocumentType.Name)
      {
        case "Assembly":
          return MechanicalDocumentKind.AssemblyModel;
        case "Part":
          return MechanicalDocumentKind.PartModel;
        case "AssemblyDrawing":
          return MechanicalDocumentKind.AssemblyDrawing;
        case "PartDrawing":
          return MechanicalDocumentKind.PartDrawing;
        default:
          throw new NotSupportedException($"Группа документов '{byDocumentType.Name}' не поддерживается.");
      }
    }
    else
      return this.IntegratorSettings.StandardPartType != null && this.IntegratorSettings.StandardPartType.Id == documentType ? MechanicalDocumentKind.StandardModel : MechanicalDocumentKind.GenericDocument;
  }

  /// <summary>
  /// По указанному виду конструкторского документа возвращает список соответствующих ему типов документов IPS.
  /// Этот метод используется при определении типов новых документов, импортируемых в IPS. Метод является
  /// зеркальным для метода GetMechanicalDocumentKindByType.
  /// </summary>
  /// <returns>Вид документа в рамках унифицированной модели обработки конструкторских документа</returns>
  /// <returns>Список идентификаторов типов документов IPS</returns>
  public override List<LocalId<int>> GetTypesByMechanicalDocumentKind(
    MechanicalDocumentKind documentKind)
  {
    List<LocalId<int>> mechanicalDocumentKind = new List<LocalId<int>>(32 /*0x20*/);
    switch (documentKind)
    {
      case MechanicalDocumentKind.AssemblyModel:
        mechanicalDocumentKind.AddRange((IEnumerable<LocalId<int>>) this.IntegratorSettings.FileDocumentGroups.FindByName("Assembly", true).DocumentTypes);
        break;
      case MechanicalDocumentKind.PartModel:
        mechanicalDocumentKind.AddRange((IEnumerable<LocalId<int>>) this.IntegratorSettings.FileDocumentGroups.FindByName("Part", true).DocumentTypes);
        break;
      case MechanicalDocumentKind.StandardModel:
        if (this.IntegratorSettings.StandardPartType != null)
        {
          mechanicalDocumentKind.Add((LocalId<int>) this.IntegratorSettings.StandardPartType);
          break;
        }
        break;
      case MechanicalDocumentKind.AssemblyDrawing:
        mechanicalDocumentKind.AddRange((IEnumerable<LocalId<int>>) this.IntegratorSettings.FileDocumentGroups.FindByName("AssemblyDrawing", true).DocumentTypes);
        break;
      case MechanicalDocumentKind.PartDrawing:
        mechanicalDocumentKind.AddRange((IEnumerable<LocalId<int>>) this.IntegratorSettings.FileDocumentGroups.FindByName("PartDrawing", true).DocumentTypes);
        break;
      case MechanicalDocumentKind.GenericDocument:
        if (!this.IntegratorSettings.JTDerivativesEnabled)
          throw new NotSupportedException();
        mechanicalDocumentKind.Add((LocalId<int>) this.IntegratorSettings.JTDerivedDocumentType);
        break;
      default:
        throw new NotSupportedException();
    }
    return mechanicalDocumentKind;
  }

  protected override SelectedObjectType SelectNewDocumentTypeForSingleKindDocument(
    SectionEntity docItem,
    object documentKind,
    List<LocalId<int>> possibleDocumentTypes)
  {
    return new SelectedObjectType(possibleDocumentTypes[0].Id, false);
  }

  protected override List<LocalId<int>> DetectNewDocumentType(SectionEntity docItem)
  {
    if (this.IntegratorSettings.JTDerivativesEnabled)
    {
      JTDerivedFileInfo jtDerivedFileInfo = new JTDerivedFileInfo(FilesSection.GetMasterFile(docItem));
      if (jtDerivedFileInfo.IsDerivedFromJTFile)
        return this.DetectNewJTDerivedDocumentType(docItem, jtDerivedFileInfo);
    }
    return base.DetectNewDocumentType(docItem);
  }

  protected override IAction CreateTypedDocumentHandler(
    SectionEntity docItem,
    object documentKind,
    int documentType)
  {
    return this.IntegratorSettings.JTDerivativesEnabled && DBHelper.IsBasedOnType(documentType, this.IntegratorSettings.JTDerivedDocumentType.Id) ? this.CreateJTDerivedDocumentHandler(docItem, documentKind, documentType) : base.CreateTypedDocumentHandler(docItem, documentKind, documentType);
  }

  private List<LocalId<int>> DetectNewJTDerivedDocumentType(
    SectionEntity jtDerivedDocItem,
    JTDerivedFileInfo jtDerivedFileInfo)
  {
    if (!File.Exists(jtDerivedFileInfo.JTFilePath))
      throw new FaultException($"Невозможно импортировать файл '{FilesSection.GetMasterFile(jtDerivedDocItem)}', так как на диске не найден связанный с ним JT-файл '{jtDerivedFileInfo.JTFilePath}'.");
    jtDerivedDocItem.Sections.Set((object) jtDerivedFileInfo);
    return CollectionUtils.CreateList<LocalId<int>>((LocalId<int>) this.IntegratorSettings.JTDerivedDocumentType);
  }

  private IAction CreateJTDerivedDocumentHandler(
    SectionEntity docItem,
    object documentKind,
    int documentType)
  {
    return (IAction) new JTDerivedDocumentHandler((DocumentCaptureChangesDriver) this, this.DriverContext, docItem);
  }

  protected override void SetupArticleHandler(SectionEntity articleEntity, IAction articleHandler)
  {
    base.SetupArticleHandler(articleEntity, articleHandler);
    switch (articleHandler)
    {
      case NormalArticleHandler normalArticleHandler:
        normalArticleHandler.Finished += new EventHandler<ArticleEntityEventArgs>(this.OnArticleHandlerFinished);
        break;
      case ImbaseObjectArticleHandler objectArticleHandler:
        objectArticleHandler.Finished += new EventHandler<ArticleEntityEventArgs>(this.OnArticleHandlerFinished);
        break;
    }
  }

  private void OnArticleHandlerFinished(object sender, ArticleEntityEventArgs e)
  {
    if (!this.IsCADBuiltInStandardPart(e.ArticleEntity))
      return;
    e.ArticleEntity.Sections.Set((object) new ObjectKeepCheckedOutSection()
    {
      KeepCheckedOut = false
    });
  }

  private bool IsCADBuiltInStandardPart(SectionEntity articleEntity)
  {
    CIArticleData ciArticleData = articleEntity.Sections.Get<CIArticleData>((CIArticleData) null);
    if (ciArticleData != null)
    {
      ObjectSection objectSection = articleEntity.Sections.Get<ObjectSection>();
      ArticleSection articleSection = articleEntity.Sections.Get<ArticleSection>();
      if (objectSection.NewObject && articleSection.InitialDocumentType == ArticleInitialDocumentType.None)
        return CADDocumentHelper.ReadPDMFlag((IServiceProvider) this.Integrator, ciArticleData.Configuration) == 1;
    }
    return false;
  }
}
