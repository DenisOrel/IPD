// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.FileDependenciesHandler
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.Collections;
using Intermech.Data.SectionEntities;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.IO;
using Intermech.Localization;
using Intermech.Tools.DataExchange;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>Обработчик файловых зависимостей документа.</summary>
public class FileDependenciesHandler : IFileDependenciesHandler
{
  private CaptureChangesDriverContext driverContext;
  private IDocumentBuilder documentBuilder;
  private IFileVault fileVaultService;
  private DraftDocumentOperations draftDocumentsOperations;
  private SectionEntity documentEntity;
  private List<DocumentFileData> documentDependencies;
  private PathCollection unresolvedDependencies;
  private FileDependenciesSharedData sharedData;
  private DocumentFileData documentFile;
  private ObjectSection documentObjectSection;
  private FilesSection documentFilesSection;
  private List<Tuple<long, string>> draftsTableCache;

  /// <summary>Создает объект.</summary>
  /// <param name="driverContext">Рабочий контекст</param>
  /// <param name="documentBuilder">Построитель сущностей для анализируемых документов</param>
  /// <param name="fileVaultService">Сервис файлового хранилища</param>
  /// <param name="draftDocumentsOperations">Сервис операций с черновиками документов</param>
  /// <exception cref="T:ArgumentNullException">Параметры не должны быть равны null</exception>
  public FileDependenciesHandler(
    CaptureChangesDriverContext driverContext,
    IDocumentBuilder documentBuilder,
    IFileVault fileVaultService,
    DraftDocumentOperations draftDocumentsOperations)
  {
    if (driverContext == null)
      throw new ArgumentNullException(nameof (driverContext));
    if (documentBuilder == null)
      throw new ArgumentNullException(nameof (documentBuilder));
    if (fileVaultService == null)
      throw new ArgumentNullException(nameof (fileVaultService));
    if (draftDocumentsOperations == null)
      throw new ArgumentNullException(nameof (draftDocumentsOperations));
    this.driverContext = driverContext;
    this.documentBuilder = documentBuilder;
    this.fileVaultService = fileVaultService;
    this.draftDocumentsOperations = draftDocumentsOperations;
    this.documentDependencies = new List<DocumentFileData>();
    this.unresolvedDependencies = new PathCollection();
  }

  /// <summary>
  /// Получает файловые зависимости документа, выполняет их анализ и обработку.
  /// </summary>
  /// <param name="docItem">Сущность анализируемого документа</param>
  /// <exception cref="T:ArgumentNullException">Параметр <paramref name="documentItem" /> не должен быть равен null</exception>
  public void Run(SectionEntity documentItem)
  {
    if (documentItem == null)
      throw new ArgumentNullException(nameof (documentItem));
    try
    {
      this.DocumentEntity = documentItem;
      this.DoInitialize();
      this.DoRun();
    }
    finally
    {
      this.DoCleanup();
    }
  }

  protected virtual void DoInitialize()
  {
    this.DocumentDependencies.Clear();
    this.UnresolvedDependencies.Clear();
    this.sharedData = CaptureChangesDatabaseGlobals<FileDependenciesSharedData>.GetOrCreate(this.DriverContext.Database, (Func<FileDependenciesSharedData>) (() => new FileDependenciesSharedData()));
    this.documentFile = new DocumentFileData(this.DocumentEntity);
    this.documentObjectSection = this.DocumentEntity.Sections.Get<ObjectSection>();
    this.documentFilesSection = this.DocumentEntity.Sections.Get<FilesSection>();
  }

  protected virtual void DoCleanup()
  {
    this.DocumentEntity = (SectionEntity) null;
    this.DocumentDependencies.Clear();
    this.UnresolvedDependencies.Clear();
    this.sharedData = (FileDependenciesSharedData) null;
    this.documentFile = (DocumentFileData) null;
    this.documentObjectSection = (ObjectSection) null;
    this.documentFilesSection = (FilesSection) null;
    this.draftsTableCache = (List<Tuple<long, string>>) null;
  }

  private void DoRun()
  {
    this.PrepareDocument();
    this.CollectDependencies();
    if (this.UnresolvedDependencies.Count != 0)
      this.ProcessUnresolvedDependencies();
    this.ProcessBadPlacedDependencies();
    if (this.DocumentDependencies.Count == 0)
      return;
    this.ProcessDependencies();
  }

  private void PrepareDocument()
  {
    this.documentFilesSection.Dependencies.Clear();
    if (!this.DriverContext.Database.IsEntryPointDocument(this.DocumentEntity))
      return;
    this.sharedData.RegisterAlreadyProcessedFile((FileDependencyProcessingResult) new FileDependencyProcessingResult.Document(this.DocumentFile.DocumentFilePath, this.DocumentEntity));
  }

  /// <summary>
  /// Собирает зависимости анализируемого документа и помещает их в коллекцию DocumentDependencies. Этот метод должен
  /// выполнить всю работу по очистке списка зависимостей от некорректных/неполных/несуществующих путей, а также по преобразованию
  /// путей в пути к мастер-файлам документов.
  /// </summary>
  protected virtual void CollectDependencies()
  {
  }

  private void ProcessUnresolvedDependencies()
  {
    this.AttachUnresolvedDependenciesToDocument((IEnumerable<string>) this.UnresolvedDependencies);
  }

  private void ProcessBadPlacedDependencies()
  {
    List<DocumentFileData> asList = CollectionUtils.ExtractAsList<DocumentFileData>((IList<DocumentFileData>) this.DocumentDependencies, (Predicate<DocumentFileData>) (dependencyFile => !PathUtils.IsPlacedIn(dependencyFile.DocumentFilePath, this.fileVaultService.WorkArea.AreaPath)));
    if (asList.Count == 0)
      return;
    if (!FileVars.SoftMode.Value)
      throw new FaultException(this.GetBadPlacedDependencyErrorMessage(asList[0]));
    foreach (DocumentFileData dependencyFile in asList)
      this.ReportUnresolvedDependency(dependencyFile, this.GetBadPlacedDependencyErrorMessage(dependencyFile));
  }

  private string GetBadPlacedDependencyErrorMessage(DocumentFileData dependencyFile)
  {
    return string.Format(LocalizationHolder.rm.GetString("Tools.Components_411"), (object) dependencyFile.DocumentFilePath);
  }

  private void ProcessDependencies()
  {
    this.documentFilesSection.Dependencies.Capacity = Math.Max(this.documentFilesSection.Dependencies.Capacity, this.DocumentDependencies.Count);
    List<FileOrigin> fileOrigins = this.fileVaultService.WorkArea.GetFileOrigins((IList<string>) this.DocumentDependencies.ConvertAll<string>((Converter<DocumentFileData, string>) (dependencyFile => dependencyFile.DocumentFilePath)), false);
    int index = 0;
    foreach (DocumentFileData documentDependency in this.DocumentDependencies)
    {
      this.ProcessDependency(new FileDependencyProcessingData(documentDependency)
      {
        FileOrigin = fileOrigins[index]
      });
      ++index;
    }
  }

  private void ProcessDependency(FileDependencyProcessingData dependency)
  {
    if (this.CheckDependencyFileConflict(dependency) == FileDependencyProcessingStatus.Skip)
      return;
    FileDependencyProcessingResult alreadyProcessedFile = this.sharedData.FindAlreadyProcessedFile(dependency.File.DocumentFilePath);
    if (alreadyProcessedFile != null)
    {
      if (!(alreadyProcessedFile is FileDependencyProcessingResult.Document))
        return;
      this.documentFilesSection.Dependencies.Add(dependency.File.DocumentFilePath);
    }
    else
    {
      if (dependency.IsNewFile)
        this.TryAttachExistingDraftDocument(dependency);
      this.sharedData.RegisterAlreadyProcessedFile(dependency.IsNewFile ? this.ProcessNewDependency(dependency) : this.ProcessExistingDependency(dependency));
    }
  }

  private FileDependencyProcessingStatus CheckDependencyFileConflict(
    FileDependencyProcessingData dependency)
  {
    if (dependency.FileOrigin.OriginType != FileOriginType.DetachedFile)
      return FileDependencyProcessingStatus.Normal;
    long detachedFileObjectId = this.GetDetachedFileObjectId(dependency.FileOrigin);
    if (DBHelper.IsObjectAlive(detachedFileObjectId))
    {
      if (new AttachNewFileToExistingObjectConfirmation(dependency.File.DocumentFilePath, detachedFileObjectId)
      {
        AbortUnconfirmedAction = true
      }.ConfirmAction())
      {
        this.fileVaultService.WorkArea.Attach(detachedFileObjectId);
        Intermech.Files.DBObjectState objectByVersionId = this.fileVaultService.WorkArea.FindPublishedObjectByVersionId(detachedFileObjectId);
        dependency.FileOrigin = new FileOrigin(dependency.FileOrigin.FileName, FileOriginType.WorkFile, dependency.FileOrigin.Id, objectByVersionId);
        return FileDependencyProcessingStatus.Normal;
      }
    }
    string str = $"Файл '{dependency.File.DocumentFilePath}' не может быть импортирован, так как в базе данных уже есть другой объект с таким же именем файла (ид. версии объекта = {detachedFileObjectId}). Этот объект может быть не виден в окнах IPS, так как он был удален и находится в корзине. Переименуйте импортируемый файл и повторите операцию.";
    if (!FileVars.SoftMode.Value)
      throw new FaultException(str);
    this.ReportUnresolvedDependency(dependency.File, str);
    return FileDependencyProcessingStatus.Skip;
  }

  private long GetDetachedFileObjectId(FileOrigin objectFileOrigin)
  {
    VersionsRulePackage editorRule = VersionsRuleSources.GetEditorRule();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObjectByVersionsRule(objectFileOrigin.Id, editorRule.OwnerId, true).ObjectID;
  }

  private void TryAttachExistingDraftDocument(FileDependencyProcessingData dependency)
  {
    dependency.DraftDocumentId = this.TryFindExistingDraftDocument(dependency.File);
  }

  private long? TryFindExistingDraftDocument(DocumentFileData dependencyFile)
  {
    string relativeFileName = PathUtils.GetRelativePath(dependencyFile.DocumentFilePath, this.fileVaultService.WorkArea.AreaPath, RelativePathOptions.ThrowIfNotPossible);
    if (this.draftsTableCache == null)
      this.draftsTableCache = this.draftDocumentsOperations.GetCurrentUserDraftDocumentsCached();
    return CollectionUtils.Find<Tuple<long, string>>((IEnumerable<Tuple<long, string>>) this.draftsTableCache, (Predicate<Tuple<long, string>>) (pair => PathUtils.IsSamePath(relativeFileName, pair.Item2)))?.Item1;
  }

  private FileDependencyProcessingResult ProcessNewDependency(
    FileDependencyProcessingData dependency)
  {
    if (!dependency.File.ForeignFile && this.IsSatelliteDependency(dependency))
    {
      this.ProcessSatelliteDependency(dependency);
      return (FileDependencyProcessingResult) new FileDependencyProcessingResult.SatelliteFile(dependency.File.DocumentFilePath);
    }
    FileDependencyProcessingParameters parametersInternal = this.GetDependencyProcessingParametersInternal(dependency);
    switch (parametersInternal.Mode)
    {
      case FileDependencyProcessingMode.Analyze:
        SectionEntity sectionEntity = this.DriverContext.Database.AddDocument(dependency.File.DocumentFilePath, dependency.ObjectId);
        this.DocumentBuilder.AttachDocumentFile(sectionEntity, dependency.File);
        if (parametersInternal.DocumentAnalysisOptions != null)
          sectionEntity.Sections.Set(parametersInternal.DocumentAnalysisOptions);
        long? draftDocumentId1 = dependency.DraftDocumentId;
        if (draftDocumentId1.HasValue)
        {
          DraftDocumentOperations documentsOperations = this.draftDocumentsOperations;
          SectionEntity documentEntity = sectionEntity;
          draftDocumentId1 = dependency.DraftDocumentId;
          long draftDocumentId2 = draftDocumentId1.Value;
          documentsOperations.AttachDraftDocumentInfo(documentEntity, draftDocumentId2);
        }
        this.DriverContext.Scheduler.AddTask(this.DocumentBuilder.CreateDocumentHandler(sectionEntity));
        this.documentFilesSection.Dependencies.Add(dependency.File.DocumentFilePath);
        return (FileDependencyProcessingResult) new FileDependencyProcessingResult.Document(dependency.File.DocumentFilePath, sectionEntity);
      case FileDependencyProcessingMode.Ignore:
        this.ReportUnresolvedDependency(dependency.File, $"Импорт файла '{dependency.File.DocumentFilePath}' отменен интегратором в соответствии с текущими настройками интегратора.");
        return (FileDependencyProcessingResult) new FileDependencyProcessingResult.IgnoredFile(dependency.File.DocumentFilePath);
      case FileDependencyProcessingMode.DeferImport:
        SectionEntity operationDatabase = this.draftDocumentsOperations.AddDraftDocumentToOperationDatabase(dependency.File.DocumentFilePath, this.draftDocumentsOperations.Service.IdCache.DraftDocuments.Id, dependency.DraftDocumentId);
        this.DocumentBuilder.AttachDocumentFile(operationDatabase, dependency.File);
        if (!dependency.DraftDocumentId.HasValue)
          this.DriverContext.Scheduler.AddTask(this.DocumentBuilder.CreateDocumentHandler(operationDatabase));
        this.documentFilesSection.Dependencies.Add(dependency.File.DocumentFilePath);
        return (FileDependencyProcessingResult) new FileDependencyProcessingResult.Document(dependency.File.DocumentFilePath, operationDatabase);
      default:
        throw new NotSupportedEnumException((Enum) parametersInternal.Mode);
    }
  }

  private FileDependencyProcessingResult ProcessExistingDependency(
    FileDependencyProcessingData dependency)
  {
    if (dependency.ObjectId == this.documentObjectSection.ObjectId)
    {
      if (!dependency.File.ForeignFile && this.IsSatelliteDependency(dependency))
        this.ProcessSatelliteDependency(dependency);
      return (FileDependencyProcessingResult) new FileDependencyProcessingResult.SatelliteFile(dependency.File.DocumentFilePath);
    }
    FileDependencyProcessingParameters parametersInternal = this.GetDependencyProcessingParametersInternal(dependency);
    switch (parametersInternal.Mode)
    {
      case FileDependencyProcessingMode.Analyze:
        this.ValidateExistingDependencyDocument(dependency);
        SectionEntity sectionEntity = this.DriverContext.Database.AddDocument(dependency.File.DocumentFilePath, dependency.ObjectId);
        this.DocumentBuilder.AttachDocumentFile(sectionEntity, dependency.File);
        if (parametersInternal.DocumentAnalysisOptions != null)
          sectionEntity.Sections.Set(parametersInternal.DocumentAnalysisOptions);
        this.DriverContext.Scheduler.AddTask(this.DocumentBuilder.CreateDocumentHandler(sectionEntity));
        this.documentFilesSection.Dependencies.Add(dependency.File.DocumentFilePath);
        return (FileDependencyProcessingResult) new FileDependencyProcessingResult.Document(dependency.File.DocumentFilePath, sectionEntity);
      case FileDependencyProcessingMode.Ignore:
        SectionEntity documentEntity = this.DriverContext.Database.AddReferencedDBObject(dependency.ObjectId);
        documentEntity.Sections.Set((object) new FilesSection()
        {
          MasterFile = dependency.File.DocumentFilePath
        });
        this.documentFilesSection.Dependencies.Add(dependency.File.DocumentFilePath);
        return (FileDependencyProcessingResult) new FileDependencyProcessingResult.Document(dependency.File.DocumentFilePath, documentEntity);
      default:
        throw new NotSupportedEnumException((Enum) parametersInternal.Mode);
    }
  }

  private void ValidateExistingDependencyDocument(FileDependencyProcessingData dependency)
  {
    this.DocumentBuilder.CheckDocumentTypeSupported(DBHelper.GetObjectType(dependency.ObjectId));
  }

  /// <summary>
  /// Используется для определения зависимостей документа, которые должны быть сохранены как дополнительные файлы документа, а не как самостоятельные документы.
  /// </summary>
  /// <param name="dependency"></param>
  /// <returns></returns>
  protected virtual bool IsSatelliteDependency(FileDependencyProcessingData dependency) => false;

  /// <summary>
  /// Используется для добавления в базу данных контекста анализа специфических сведений, связанных с зависимостью документа, сохраненной как дополнительный файл документа.
  /// </summary>
  /// <param name="dependency"></param>
  protected virtual void ProcessSatelliteDependency(FileDependencyProcessingData dependency)
  {
    this.documentFilesSection.Satellites.Add(dependency.File.DocumentFilePath);
  }

  private FileDependencyProcessingParameters GetDependencyProcessingParametersInternal(
    FileDependencyProcessingData dependency)
  {
    return dependency.File.ForeignFile ? this.GetForeignDependencyProcessingParameters(dependency) : this.GetDependencyProcessingParameters(dependency);
  }

  private FileDependencyProcessingParameters GetForeignDependencyProcessingParameters(
    FileDependencyProcessingData dependency)
  {
    return dependency.IsNewFile ? FileDependencyProcessingParameters.DeferImport : FileDependencyProcessingParameters.Ignore;
  }

  protected virtual FileDependencyProcessingParameters GetDependencyProcessingParameters(
    FileDependencyProcessingData dependency)
  {
    return dependency.IsNewFile ? FileDependencyProcessingParameters.Analyse : FileDependencyProcessingParameters.Ignore;
  }

  private void AttachUnresolvedDependenciesToDocument(IEnumerable<string> dependencyFiles)
  {
    UnresolvedFilesSection sectionObject = this.DocumentFile.CustomSections.Get<UnresolvedFilesSection>((UnresolvedFilesSection) null);
    if (sectionObject == null)
    {
      sectionObject = new UnresolvedFilesSection();
      this.DocumentFile.CustomSections.Set((object) sectionObject);
    }
    sectionObject.Files.AddRange(dependencyFiles);
  }

  private void AttachUnresolvedDependencyToDocument(string dependencyFilePath)
  {
    UnresolvedFilesSection sectionObject = this.DocumentFile.CustomSections.Get<UnresolvedFilesSection>((UnresolvedFilesSection) null);
    if (sectionObject == null)
    {
      sectionObject = new UnresolvedFilesSection();
      this.DocumentFile.CustomSections.Set((object) sectionObject);
    }
    sectionObject.Files.Add(dependencyFilePath);
  }

  private void ReportUnresolvedDependency(DocumentFileData dependencyFile, string errorMessage)
  {
    if (UIReport.Enabled)
    {
      errorMessage = $"{errorMessage} {LocalizationHolder.rm.GetString("SR_541")}";
      UIReport.ReportEvent(errorMessage, TraceLevel.Warning);
    }
    this.AttachUnresolvedDependencyToDocument(dependencyFile.DocumentFilePath);
  }

  /// <summary>Возвращает контекст выполняемой операции.</summary>
  protected CaptureChangesDriverContext DriverContext
  {
    [DebuggerStepThrough] get => this.driverContext;
  }

  /// <summary>
  /// Возвращает построитель сущностей для анализируемых документов.
  /// </summary>
  protected IDocumentBuilder DocumentBuilder
  {
    [DebuggerStepThrough] get => this.documentBuilder;
  }

  /// <summary>
  /// Возвращает или задает анализируемый документ. Значение свойства обязательно должно быть задано до начала выполнения обработчика.
  /// </summary>
  protected SectionEntity DocumentEntity
  {
    [DebuggerStepThrough] get => this.documentEntity;
    [DebuggerStepThrough] set => this.documentEntity = value;
  }

  /// <summary>Возвращает открытый файл анализируемого документа.</summary>
  protected DocumentFileData DocumentFile
  {
    [DebuggerStepThrough] get => this.documentFile;
  }

  /// <summary>Возвращает коллекцию зависимостей документа.</summary>
  protected List<DocumentFileData> DocumentDependencies
  {
    [DebuggerStepThrough] get => this.documentDependencies;
  }

  /// <summary>
  /// Возвращает коллекцию битых зависимостей документа.
  /// Файлы этих зависимостей либо не найдены на диске, либо не могут быть обработаны автоматически.
  /// Все зависимости документа, указанные в этом свойстве, будут сохранены в специальном атрибуте документа 'Требует уточнения ссылок на файлы'.
  /// </summary>
  protected PathCollection UnresolvedDependencies
  {
    [DebuggerStepThrough] get => this.unresolvedDependencies;
  }
}
