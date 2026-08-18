// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.DocumentCaptureChangesDriver
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.ControlFlow;
using Intermech.Data.SectionEntities;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.IO;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Позволяет реализовать драйвер захвата изменений для документов, анализируемых по правилам одной конкретной схемы обработки.
/// </summary>
public abstract class DocumentCaptureChangesDriver : BasicCaptureChangesDriver, IDocumentBuilder
{
  private IDraftDocumentsService draftDocumentsService;
  private AncillaryFilesService ancillaryFilesService;
  private DocumentCaptureChangesOperations driverOperations;

  public DocumentCaptureChangesOperations Operations
  {
    [DebuggerStepThrough] get => this.driverOperations;
  }

  protected override void InitializeDriver()
  {
    base.InitializeDriver();
    this.draftDocumentsService = ServiceUtils.GetService<IDraftDocumentsService>((object) ApplicationServices.Container, true);
    this.ancillaryFilesService = this.CreateAncillaryFilesService();
  }

  protected override void InitializeDriverContextServices()
  {
    base.InitializeDriverContextServices();
    this.driverOperations = this.CreateDriverOperations();
  }

  protected override void ClearDriver()
  {
    base.ClearDriver();
    this.draftDocumentsService = (IDraftDocumentsService) null;
    this.ancillaryFilesService = (AncillaryFilesService) null;
    this.driverOperations = (DocumentCaptureChangesOperations) null;
  }

  private AncillaryFilesService CreateAncillaryFilesService()
  {
    AncillaryFilesService ancillaryFilesService = new AncillaryFilesService();
    ancillaryFilesService.Register((AncillaryFilesProvider) new DefaultAncillaryFilesProvider());
    return ancillaryFilesService;
  }

  private DocumentCaptureChangesOperations CreateDriverOperations()
  {
    return new DocumentCaptureChangesOperations(this.DriverContext, this.draftDocumentsService);
  }

  /// <summary>
  /// Возвращает сервис для работы с дополнительными файлами документов.
  /// </summary>
  /// <returns>Сервис для работы с дополнительными файлами документов</returns>
  public AncillaryFilesService GetAncillaryFilesService() => this.ancillaryFilesService;

  protected override bool PrepareRootDocument(SectionEntity docItem)
  {
    ObjectSection objectSection = docItem.Sections.Get<ObjectSection>();
    FilesSection filesSection = docItem.Sections.Get<FilesSection>();
    DocumentFileData openFileData;
    if (objectSection.NewObject)
    {
      this.ValidateRootFile(filesSection.MasterFile, 0L);
      openFileData = this.OpenRootFile(docItem, filesSection.MasterFile, 0L, false);
      if (!PathUtils.IsSamePath(openFileData.DocumentFilePath, filesSection.MasterFile))
        filesSection.MasterFile = openFileData.DocumentFilePath;
    }
    else
    {
      this.ValidateRootFile(filesSection.MasterFile, objectSection.ObjectId);
      openFileData = this.OpenRootFile(docItem, filesSection.MasterFile, objectSection.ObjectId);
      if (!PathUtils.IsSamePath(openFileData.DocumentFilePath, filesSection.MasterFile))
        throw new InvalidOperationException("Не разрешается заменять открываемый файл в режиме сохранения изменений.");
    }
    this.AttachDocumentFile(docItem, openFileData);
    this.DriverContext.Scheduler.AddTask(this.CreateDocumentHandler(docItem));
    return true;
  }

  /// <summary>
  /// Реализует проверку стартового файла и его документа, которая необходима для защиты от неподдерживаемых типов файлов и документов.
  /// Если файл или его документ не проходит проверку, то метод должен сбросить исключение типа <see cref="T:Intermech.FaultException" />.
  /// </summary>
  /// <param name="rootFilePath">Абсолютный путь к стартовому файлу, находящемуся в рабочей области файлового хранилища</param>
  /// <param name="rootObjectId">Идентификатор версии сохраняемого документа. Может быть не задан, если выполняется импорт нового файла</param>
  /// <exception cref="T:Intermech.FaultException">Неподдерживаемый тип файла или неподдерживаемый документ</exception>
  protected virtual void ValidateRootFile(string rootFilePath, long rootObjectId)
  {
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
  protected virtual DocumentFileData OpenRootFile(
    SectionEntity rootDocumentItem,
    string rootFilePath,
    long rootObjectId,
    bool useExactFilePath = true)
  {
    return this.OpenDocumentFile(rootDocumentItem, rootFilePath);
  }

  /// <summary>Позволяет открыть документ.</summary>
  /// <param name="documentItem">Элемент документа в базе данных контекста</param>
  /// <param name="fullPath">Абсолютный путь к файлу документа</param>
  /// <returns>Открытый документ</returns>
  /// <exception cref="T:ArgumentNullException">documentItem || fullPath</exception>
  public abstract DocumentFileData OpenDocumentFile(SectionEntity documentItem, string fullPath);

  /// <summary>Добавляет к документу сведения из открытого файла.</summary>
  /// <param name="docItem">Элемент документа в базе данных контекста</param>
  /// <param name="openFileData">Сведения из открытого файла документа</param>
  public virtual void AttachDocumentFile(SectionEntity docItem, DocumentFileData openFileData)
  {
    if (docItem == null)
      throw new ArgumentNullException(nameof (docItem));
    if (openFileData == null)
      throw new ArgumentNullException(nameof (openFileData));
    docItem.Sections.CopyFrom((IEnumerable<KeyValuePair<Type, object>>) openFileData.CustomSections);
    int objectType = ObjectSection.TryGetObjectType(docItem);
    if (objectType == -1 || objectType == this.draftDocumentsService.IdCache.DraftDocuments.Id)
      return;
    this.SetDocumentKind(docItem, this.MapDocumentTypeToKind(objectType));
  }

  /// <summary>Создает обработчик для документа.</summary>
  /// <param name="docItem">Элемент документа в базе данных контекста</param>
  /// <returns>Обработчик для документа</returns>
  public IAction CreateDocumentHandler(SectionEntity docItem)
  {
    IAction documentHandler = docItem != null ? this.CreateDocumentHandlerInstance(docItem) : throw new ArgumentNullException(nameof (docItem));
    this.SetupDocumentHandler(docItem, documentHandler);
    return documentHandler;
  }

  private IAction CreateDocumentHandlerInstance(SectionEntity docItem)
  {
    if (docItem.Sections.Contains<DraftDocumentSection>())
      return (IAction) new DraftDocumentHandler(this, this.DriverContext, docItem, IDCache.Default, ClientContext.FileVault);
    ObjectSection objectSection = docItem.Sections.Get<ObjectSection>();
    if (objectSection.ObjectType == -1)
      return this.CreateNewDocumentHandler(docItem);
    object documentKind;
    if (!this.TryGetDocumentKind(docItem, out documentKind))
    {
      documentKind = this.MapDocumentTypeToKind(objectSection.ObjectType);
      this.SetDocumentKind(docItem, documentKind);
    }
    return this.CreateTypedDocumentHandler(docItem, documentKind, objectSection.ObjectType);
  }

  protected virtual void SetupDocumentHandler(SectionEntity docItem, IAction documentHandler)
  {
    if (docItem == null)
      throw new ArgumentNullException(nameof (docItem));
    if (documentHandler == null)
      throw new ArgumentNullException(nameof (documentHandler));
  }

  private IAction CreateNewDocumentHandler(SectionEntity docItem)
  {
    ObjectSection objectSection = docItem.Sections.Get<ObjectSection>();
    List<LocalId<int>> localIdList = this.DetectNewDocumentType(docItem);
    if (localIdList == null || localIdList.Count == 0)
      return this.CreateNewDocumentHandler(docItem, this.DetectFallbackDocumentType(docItem));
    if (localIdList.Count == 1)
    {
      objectSection.ObjectType = localIdList[0].Id;
      return this.CreateDocumentHandler(docItem);
    }
    List<object> objectList = new List<object>(localIdList.Count);
    foreach (LocalId<int> localId in localIdList)
    {
      object kind = this.MapDocumentTypeToKind(localId.Id);
      if (!objectList.Contains(kind))
        objectList.Add(kind);
    }
    if (FileVars.SoftMode.Value)
      return this.CreateNewDocumentHandler(docItem, this.SelectNewDocumentTypeSilent(docItem, objectList, localIdList));
    IAction untypedDocumentHandler = this.CreateUntypedDocumentHandler(docItem, objectList, localIdList);
    if (untypedDocumentHandler != null)
      return untypedDocumentHandler;
    return objectList.Count == 1 ? this.CreateNewDocumentHandler(docItem, this.SelectNewDocumentTypeForSingleKindDocument(docItem, objectList[0], localIdList)) : this.CreateNewDocumentHandler(docItem, this.SelectNewDocumentTypeForMultiKindDocument(docItem, objectList, localIdList));
  }

  private IAction CreateNewDocumentHandler(SectionEntity docItem, SelectedObjectType selectedType)
  {
    ObjectSection objectSection = docItem.Sections.Get<ObjectSection>();
    objectSection.ObjectType = selectedType.ObjectType;
    objectSection.RequireTypeCheck = selectedType.RequireCheck;
    return this.CreateDocumentHandler(docItem);
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
  protected abstract List<LocalId<int>> DetectNewDocumentType(SectionEntity docItem);

  protected virtual SelectedObjectType DetectFallbackDocumentType(SectionEntity docItem)
  {
    throw new InvalidOperationException($"Интегратор должен был предложить хотя бы один тип документа для импортируемого документа '{DisplaySection.GetQualifiedName(docItem)}'.");
  }

  protected abstract IAction CreateTypedDocumentHandler(
    SectionEntity docItem,
    object documentKind,
    int documentType);

  protected virtual IAction CreateUntypedDocumentHandler(
    SectionEntity docItem,
    List<object> documentKinds,
    List<LocalId<int>> documentTypes)
  {
    return (IAction) null;
  }

  protected virtual SelectedObjectType SelectNewDocumentTypeForSingleKindDocument(
    SectionEntity docItem,
    object documentKind,
    List<LocalId<int>> possibleDocumentTypes)
  {
    return new SelectedObjectType(this.Operations.Documents.SelectDocumentType(docItem, (ICollection<LocalId<int>>) possibleDocumentTypes).Id, false);
  }

  protected virtual SelectedObjectType SelectNewDocumentTypeForMultiKindDocument(
    SectionEntity docItem,
    List<object> possibleDocumentKinds,
    List<LocalId<int>> possibleDocumentTypes)
  {
    return new SelectedObjectType(this.Operations.Documents.SelectDocumentType(docItem, (ICollection<LocalId<int>>) possibleDocumentTypes).Id, false);
  }

  protected virtual SelectedObjectType SelectNewDocumentTypeSilent(
    SectionEntity docItem,
    List<object> possibleDocumentKinds,
    List<LocalId<int>> possibleDocumentTypes)
  {
    return new SelectedObjectType(possibleDocumentTypes[0].Id, true);
  }

  /// <summary>
  /// Возвращает true, если документы указанного типа могут быть обработаны интегратором.
  /// </summary>
  /// <param name="documentType">Идентификатор типа документа</param>
  /// <returns>Признак возможности обработки</returns>
  public abstract bool IsDocumentTypeSupported(int documentType);

  /// <summary>
  /// Проверяет, могут ли документы указанного типа могут быть обработаны интегратором. Если нет, то метод сбрасывает исключение.
  /// </summary>
  /// <param name="documentType">Идентификатор типа документа</param>
  /// <exception cref="T:Intermech.FaultException">Обработка документов указанного типа не поддерживается</exception>
  public virtual void CheckDocumentTypeSupported(int documentType)
  {
    if (!this.IsDocumentTypeSupported(documentType))
      throw new FaultException($"Обработка документов типа {documentType} не поддерживается интегратором.");
  }

  public List<LocalId<int>> FilterDocumentTypesByExtension(
    SectionEntity docItem,
    List<LocalId<int>> possibleTypes)
  {
    return this.FilterDocumentTypesByExtension(docItem, possibleTypes, true);
  }

  public List<LocalId<int>> FilterDocumentTypesByExtension(
    SectionEntity docItem,
    List<LocalId<int>> possibleTypes,
    bool allowEmptyExtensions)
  {
    if (docItem == null)
      throw new ArgumentNullException(nameof (docItem));
    if (possibleTypes == null)
      throw new ArgumentNullException(nameof (possibleTypes));
    string secondPath = Path.GetExtension(FilesSection.GetMasterFile(docItem));
    LinkedList<LocalId<int>> collection1 = new LinkedList<LocalId<int>>();
    LinkedList<LocalId<int>> collection2 = new LinkedList<LocalId<int>>();
    foreach (LocalId<int> possibleType in possibleTypes)
    {
      DocumentTypeSettings settings = DocumentTypeSettingsCache.GetSettings(possibleType);
      if (PathUtils.IsSamePath(settings.DocumentFileExt, secondPath))
        collection1.AddLast(possibleType);
      else if (allowEmptyExtensions && string.IsNullOrEmpty(settings.DocumentFileExt))
        collection2.AddLast(possibleType);
    }
    List<LocalId<int>> localIdList = new List<LocalId<int>>(collection1.Count + collection2.Count);
    localIdList.AddRange((IEnumerable<LocalId<int>>) collection1);
    localIdList.AddRange((IEnumerable<LocalId<int>>) collection2);
    if (localIdList.Count == 0 & allowEmptyExtensions)
      localIdList.AddRange((IEnumerable<LocalId<int>>) possibleTypes);
    return localIdList;
  }

  public List<LocalId<int>> FilterDocumentTypesByDesignType(
    SectionEntity docItem,
    ICollection<LocalId<int>> possibleTypes,
    string designType)
  {
    if (docItem == null)
      throw new ArgumentNullException(nameof (docItem));
    if (possibleTypes == null)
      throw new ArgumentNullException(nameof (possibleTypes));
    if (string.IsNullOrEmpty(designType))
      throw new ArgumentException();
    List<LocalId<int>> localIdList = new List<LocalId<int>>(possibleTypes.Count);
    foreach (LocalId<int> possibleType in (IEnumerable<LocalId<int>>) possibleTypes)
    {
      if (PathUtils.IsSamePath(DocumentTypeSettingsCache.GetSettings(possibleType).DocumentTypeName, designType))
        localIdList.Add(possibleType);
    }
    return localIdList;
  }

  /// <summary>
  /// Переводит тип документа IPS в вид документа приложения, который используется для выбора обработчика документа. Каждому виду документов соответствует свой обработчик.
  /// </summary>
  /// <param name="documentType">Идентификатор типа документа</param>
  /// <returns>Идентификатор вида документа приложения</returns>
  protected object MapDocumentTypeToKind(int documentType)
  {
    this.CheckDocumentTypeSupported(documentType);
    return this.DoMapDocumentTypeToKind(documentType);
  }

  /// <summary>
  /// Переводит тип документа IPS в вид документа приложения, который используется для выбора обработчика документа. Каждому виду документов соответствует свой обработчик.
  /// </summary>
  /// <param name="documentType">Идентификатор типа документа</param>
  /// <returns>Идентификатор вида документа приложения</returns>
  protected abstract object DoMapDocumentTypeToKind(int documentType);

  public bool HasDocumentKind(SectionEntity docItem)
  {
    DocumentSection documentSection = docItem != null ? docItem.Sections.Get<DocumentSection>((DocumentSection) null) : throw new ArgumentNullException(nameof (docItem));
    return documentSection != null && documentSection.DocumentKind != DocumentSection.UndefinedKind;
  }

  public bool TryGetDocumentKind(SectionEntity docItem, out object documentKind)
  {
    return docItem != null ? this.TryGetDocumentKind(docItem.Sections, out documentKind) : throw new ArgumentNullException(nameof (docItem));
  }

  public bool TryGetDocumentKind(SectionCollection docSections, out object documentKind)
  {
    DocumentSection documentSection = docSections != null ? docSections.Get<DocumentSection>((DocumentSection) null) : throw new ArgumentNullException(nameof (docSections));
    if (documentSection != null && documentSection.DocumentKind != DocumentSection.UndefinedKind)
    {
      documentKind = documentSection.DocumentKind;
      return true;
    }
    documentKind = (object) null;
    return false;
  }

  private void SetDocumentKind(SectionEntity docItem, object documentKind)
  {
    DocumentSection sectionObject = docItem != null ? docItem.Sections.Get<DocumentSection>((DocumentSection) null) : throw new ArgumentNullException(nameof (docItem));
    if (sectionObject == null)
    {
      sectionObject = new DocumentSection();
      docItem.Sections.Set((object) sectionObject);
    }
    sectionObject.DocumentKind = documentKind;
  }
}
