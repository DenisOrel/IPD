// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.DocumentHandlerBase
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.ControlFlow;
using Intermech.ControlFlow.Cooperative;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Data;
using Intermech.Interfaces.Data.Actions;
using Intermech.IO;
using Intermech.Tools.Data.Sync;
using Intermech.Tools.DataExchange;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Реализует основу для создания обработчиков документов.
/// </summary>
public abstract class DocumentHandlerBase : CooperativeAction
{
  private DocumentCaptureChangesDriver driver;
  private CaptureChangesDriverContext driverContext;
  private SectionEntity docEntity;
  private IFileVault fileVaultService;
  private IOpenFilesService openFilesService;
  private DocumentScheduleAdapter scheduleAdapter;
  private ObjectSection docObject;
  private FilesSection docFiles;
  private AttributesSection docAttributes;
  private IDBAttributableTypeRef docAttributesLayout;
  private bool dependenciesComplete;
  private string customFilesBaseDirectory;
  private string docFilesBaseDirectory;

  /// <summary>Создает объект.</summary>
  /// <param name="driver">Стратегия анализа изменений</param>
  /// <param name="driverContext">Рабочий контекст</param>
  /// <param name="documentEntity">Объект обрабатываемого документа</param>
  /// <exception cref="T:System.ArgumentNullException">Ошибка в аргументах метода</exception>
  public DocumentHandlerBase(
    DocumentCaptureChangesDriver driver,
    CaptureChangesDriverContext driverContext,
    SectionEntity documentEntity)
    : base(driverContext.Scheduler)
  {
    if (driver == null)
      throw new ArgumentNullException(nameof (driver));
    if (driverContext == null)
      throw new ArgumentNullException(nameof (driverContext));
    if (documentEntity == null)
      throw new ArgumentNullException(nameof (documentEntity));
    this.driver = driver;
    this.driverContext = driverContext;
    this.docEntity = documentEntity;
    this.fileVaultService = ServiceUtils.GetService<IFileVault>((object) ApplicationServices.Container, true);
    this.openFilesService = ServiceUtils.GetService<IOpenFilesService>((object) ApplicationServices.Container, true);
  }

  protected DocumentCaptureChangesDriver Driver
  {
    [DebuggerStepThrough] get => this.driver;
  }

  protected CaptureChangesDriverContext DriverContext
  {
    [DebuggerStepThrough] get => this.driverContext;
  }

  /// <summary>Возвращает документ, с которым связан текущий объект.</summary>
  protected SectionEntity DocumentEntity
  {
    [DebuggerStepThrough] get => this.docEntity;
  }

  protected IFileVault FileVaultService
  {
    [DebuggerStepThrough] get => this.fileVaultService;
  }

  protected IOpenFilesService OpenFilesService
  {
    [DebuggerStepThrough] get => this.openFilesService;
  }

  public DocumentScheduleAdapter ScheduleAdapter
  {
    [DebuggerStepThrough] get => this.scheduleAdapter;
    [DebuggerStepThrough] set => this.scheduleAdapter = value;
  }

  /// <summary>
  /// Возвращает сведения о документе в базе данных IPS.
  /// Значение свойства доступно только после инициализации текущего объекта.
  /// </summary>
  protected ObjectSection DocumentObject
  {
    [DebuggerStepThrough] get => this.docObject;
  }

  /// <summary>
  /// Возвращает сведения о файлах документе на локальном диске.
  /// Значение свойства доступно только после инициализации текущего объекта.
  /// </summary>
  protected FilesSection DocumentFiles
  {
    [DebuggerStepThrough] get => this.docFiles;
  }

  /// <summary>
  /// Возвращает актуальный действующий путь к каталогу, который является базой для
  /// вычисления относительных путей для файлов документа.
  /// </summary>
  protected string DocumentFilesBaseDirectory
  {
    get
    {
      if (this.docFilesBaseDirectory == null)
        this.docFilesBaseDirectory = string.IsNullOrEmpty(this.CustomFilesBaseDirectory) ? this.fileVaultService.WorkArea.AreaPath : this.CustomFilesBaseDirectory;
      return this.docFilesBaseDirectory;
    }
  }

  /// <summary>
  /// Возвращает или задает специальный путь к каталогу, который является базой для
  /// вычисления относительных путей для файлов документа. Это свойство используется,
  /// если файлы документа располагаются не в рабочей области файлового хранилища.
  /// По умолчанию значение свойства равно null.
  /// </summary>
  protected string CustomFilesBaseDirectory
  {
    [DebuggerStepThrough] get => this.customFilesBaseDirectory;
    [DebuggerStepThrough] set
    {
      if (!(this.customFilesBaseDirectory != value))
        return;
      this.customFilesBaseDirectory = value;
      this.docFilesBaseDirectory = (string) null;
    }
  }

  /// <summary>
  /// Возвращает раскладку атрибутов документа в базе IPS.
  /// Значение свойства доступно только после нахождения/создания объекта IPS в базе данных IPS.
  /// </summary>
  protected IDBAttributableTypeRef DocumentAttributesLayout
  {
    [DebuggerStepThrough] get => this.docAttributesLayout;
  }

  /// <summary>
  /// Возвращает атрибуты документа IPS.
  /// Значение свойства доступно только после чтения атрибутов из интегрируемого приложения.
  /// </summary>
  protected AttributesSection DocumentAttributes
  {
    [DebuggerStepThrough] get => this.docAttributes;
  }

  internal void SkipDependencies() => this.dependenciesComplete = true;

  protected sealed override object GetUIReportOperationId() => (object) this.DocumentEntity;

  protected override IEnumerable<CooperativeState> Coroutine()
  {
    this.ValidateProperties();
    this.InitializeHandler();
    this.ProcessDocumentType();
    if (!this.dependenciesComplete)
    {
      this.ProcessDependencies();
      this.dependenciesComplete = true;
    }
    this.EnsureDBObjectExists();
    this.OnAfterDBObjectAttached();
    this.ReadDataFromDBObject();
    this.ReadDataFromFile();
    this.ProcessFiles();
    this.ProcessAttributes();
    this.ProcessDerivedObjects();
    yield return this.Wait((IWaitObject) this.ScheduleAdapter.RelationsStage);
    this.ProcessRelations();
    this.DeleteUnwantedAttributes();
    this.Driver.Operations.Db.EmitObjectAttributesServerActions(this.DocumentEntity);
    yield return this.Wait((IWaitObject) this.ScheduleAdapter.DiskWritesStage);
    if (this.DocumentObject.NewObject && this.IsFilesProcessingEnabled())
      this.SetFilesReadOnlyAttribute(false);
    this.WriteChangesToDocumentFiles();
    if (AnalyzerChangesSection.IsMarked(this.DocumentEntity))
      yield return this.Call(new Func<IEnumerable<CooperativeState>>(this.SaveModifiedDocumentFiles));
    yield return this.Wait((IWaitObject) this.ScheduleAdapter.UploadFilesStage);
    if (this.IsFilesProcessingEnabled())
      this.EmitFilesUploadAction();
    yield return this.Wait((IWaitObject) this.ScheduleAdapter.UIStage);
    this.Driver.Operations.Db.EmitUIActions(this.DriverContext, this.DocumentEntity);
  }

  protected bool IsFilesProcessingEnabled()
  {
    return this.Driver.Operations.Documents.GetFilesProcessingFlag(this.DocumentEntity);
  }

  /// <summary>
  /// Позволяет проверить корректность значений свойств, задающих поведение обработчика. Этот метод вызывается обработчиком перед началом работы.
  /// </summary>
  /// <exception cref="T:Intermech.Tools.DataExchange.DataExchangeConfigurationException">Свойства обработчика заполнены неверно</exception>
  protected virtual void ValidateProperties()
  {
    if (this.ScheduleAdapter == null)
      throw new DataExchangeConfigurationException("ScheduleAdapter");
  }

  /// <summary>Выполняет инициализацию обработчика.</summary>
  protected virtual void InitializeHandler()
  {
    this.docObject = this.DocumentEntity.Sections.Get<ObjectSection>();
    this.docFiles = this.DocumentEntity.Sections.Get<FilesSection>();
    this.docAttributes = new AttributesSection();
    this.DocumentEntity.Sections.Set((object) this.docAttributes);
  }

  private void ProcessDocumentType()
  {
    if (this.DocumentObject.ObjectType != -1)
      return;
    SelectedObjectType selectedObjectType = this.DetectNewDocumentType();
    this.DocumentObject.ObjectType = selectedObjectType != null ? selectedObjectType.ObjectType : throw new InvalidOperationException($"Для импортируемого документа '{DisplaySection.GetQualifiedName(this.DocumentEntity)}' интегратор не выбрал тип документа.");
    this.DocumentObject.RequireTypeCheck = selectedObjectType.RequireCheck;
  }

  /// <summary>
  /// Выполняет обработку файловых зависимостей документа. По каждой зависимости в базе данных анализатора создается объект и назначается обработчик.
  /// </summary>
  protected abstract void ProcessDependencies();

  private void ReadDataFromFile()
  {
    this.DocumentAttributes.EmbeddedSet = this.ReadFileProperties();
    this.DocumentAttributes.WorkingSet = this.DecodeDocumentAttributes(this.DocumentAttributes.EmbeddedSet);
  }

  private void EnsureDBObjectExists()
  {
    if (!this.DocumentObject.NewObject || this.Driver.Operations.DraftDocuments.TryCreateBlankDocumentFromDraftDocument(this.DocumentEntity))
      return;
    this.CreateNewDBObject();
  }

  protected virtual void CreateNewDBObject()
  {
    this.Driver.Operations.Db.CreateBlankObject(this.DriverContext, this.DocumentEntity);
  }

  protected virtual void OnAfterDBObjectAttached()
  {
    this.docAttributesLayout = (IDBAttributableTypeRef) new DirectObjectAttributesRef(this.DocumentObject.ObjectType);
  }

  private void ReadDataFromDBObject()
  {
    this.Driver.Operations.Db.FetchObjectAttributes(this.DocumentEntity, this.DocumentAttributesLayout);
  }

  private void ProcessAttributes()
  {
    this.PreserveAttributeScavengery();
    this.CorrectAttributes();
    this.TransferAttributes();
    this.UpdateDBOnlyAttributes();
  }

  private void PreserveAttributeScavengery()
  {
    List<StringKey> attributeKeys = new List<StringKey>(this.DocumentAttributes.WorkingSet.Count);
    foreach (ValueRecord working in this.DocumentAttributes.WorkingSet)
    {
      if (working.DataType == typeof (string))
        attributeKeys.Add(working.Key);
    }
    if (attributeKeys.Count <= 0)
      return;
    this.EncodeDocumentAttributes((ICollection<StringKey>) attributeKeys, this.DocumentAttributes.WorkingSet, this.DocumentAttributes.EmbeddedSet);
  }

  /// <summary>
  /// Корректирует значения атрибутов, прочитанные из файла документа, перед переносом значений атрибутов в объект документа.
  /// </summary>
  protected virtual void CorrectAttributes()
  {
  }

  /// <summary>
  /// Выполняет перенос атрибутов из файла в объект документа.
  /// </summary>
  private void TransferAttributes()
  {
    ICollection<StringKey> transferableAttributes = this.GetTransferableAttributes();
    if (transferableAttributes.Count == 0)
      return;
    this.TransferAttributes(transferableAttributes);
  }

  /// <summary>
  /// Выполняет перенос атрибутов из файла в объект документа.
  /// </summary>
  /// <param name="attributes">Список ключей атрибутов для переноса</param>
  protected virtual void TransferAttributes(ICollection<StringKey> attributes)
  {
    if (attributes == null)
      throw new ArgumentNullException(nameof (attributes));
    AppToDBAttributeSyncTask attributeSyncTask = new AppToDBAttributeSyncTask();
    attributeSyncTask.EntityDisplayName = DisplaySection.GetQualifiedName(this.DocumentEntity);
    attributeSyncTask.EntityId = this.DocumentObject.ObjectId;
    attributeSyncTask.SetApplicationAttributes(this.DocumentAttributes.WorkingSet, this.DocumentAttributes.EmbeddedSet.IsOpenMetadata);
    attributeSyncTask.SetDatabaseAttributes(this.DocumentAttributes.DatabaseSet, this.DocumentAttributesLayout);
    foreach (StringKey attribute in (IEnumerable<StringKey>) attributes)
      attributeSyncTask.Attributes.Add(new AttributeSyncUnit(attribute, this.IsTransferRequired(attribute)));
    attributeSyncTask.RunChecked();
  }

  /// <summary>
  /// Возвращает список ключей атрибутов, значения которых должны быть перенесены из файла в объект документа IPS.
  /// Как правило, этот список задается в настройках интегратора.
  /// </summary>
  /// <returns>Список ключей атрибутов</returns>
  protected virtual ICollection<StringKey> GetTransferableAttributes()
  {
    return (ICollection<StringKey>) new OrderedList<StringKey>(32 /*0x20*/);
  }

  /// <summary>
  /// Возвращает true, если атрибут обязательно должен быть перенесен из файла в объект документа. Если это не удается сделать,
  /// то будет сброшено исключение и вся операция будет прервана. Ошибки переноса остальных атрибутов игнорируются с занесением информации о
  /// сбое в протокол выполнения.
  /// </summary>
  /// <param name="attributeKey">Ключ атрибута</param>
  /// <returns>Признак, что ошибки в процессе переноса этого атрибута из файла в объект документа недопустимы</returns>
  protected virtual bool IsTransferRequired(StringKey attributeKey) => false;

  /// <summary>
  /// Позволяет обновить значения атрибутов, которые есть только у объекта документа в базе IPS. В файле документа такие атрибуты
  /// не сохраняются.
  /// </summary>
  protected virtual void UpdateDBOnlyAttributes() => this.MarkAsRequireTypeCheck();

  private void MarkAsRequireTypeCheck()
  {
    if (!this.DocumentObject.NewObject)
      return;
    new MarkAsRequireTypeCheckAction(this.DocumentEntity).Perform();
  }

  protected virtual void DeleteUnwantedAttributes()
  {
  }

  /// <summary>Позволяет обработать файлы документа.</summary>
  protected virtual void ProcessFiles()
  {
    if (string.IsNullOrEmpty(this.DocumentFiles.MasterFile))
      return;
    this.ProcessAncillaryFiles();
  }

  /// <summary>Позволяет обработать дополнительные файлы документа.</summary>
  protected virtual void ProcessAncillaryFiles()
  {
    this.AttachAncillaryFilesFromDBObject();
    PathCollection newFiles = this.CollectNewAncillaryFiles();
    this.FilterNewAncillaryFiles(newFiles);
    foreach (string str in (OrderedList<string>) newFiles)
      this.DocumentFiles.Satellites.Add(str);
  }

  private void AttachAncillaryFilesFromDBObject()
  {
    if (this.DocumentObject.NewObject)
      return;
    foreach (string fileName in (IEnumerable<string>) this.fileVaultService.DBFilesInfo.GetFileNames(this.DocumentObject.ObjectId))
    {
      string str = Path.Combine(this.DocumentFilesBaseDirectory, fileName);
      if (!PathUtils.IsSamePath(this.DocumentFiles.MasterFile, str) && File.Exists(str))
        CollectionUtils.AddNew<string>((ICollection<string>) this.DocumentFiles.Satellites, str);
    }
  }

  /// <summary>Собирает новые дополнительные файлы объекта.</summary>
  /// <returns>Список абсолютных путей к файлам</returns>
  protected virtual PathCollection CollectNewAncillaryFiles()
  {
    return this.Driver.GetAncillaryFilesService().GetFiles(this.DocumentEntity);
  }

  internal void FilterNewAncillaryFiles(PathCollection newFiles)
  {
    CollectionUtils.RemoveAll<string>((IList<string>) newFiles, (Predicate<string>) (item => !PathUtils.IsPlacedIn(item, this.DocumentFilesBaseDirectory)));
    if (newFiles.Count == 0)
      return;
    foreach (FileOrigin fileOrigin in this.fileVaultService.WorkArea.GetFileOrigins((IList<string>) newFiles, false))
    {
      if (!this.IsNewAncillaryFile(fileOrigin))
        newFiles.Remove(fileOrigin.FileName);
    }
  }

  private bool IsNewAncillaryFile(FileOrigin fileOrigin)
  {
    return fileOrigin.OriginType == FileOriginType.NewFile && !this.DocumentFiles.Satellites.Contains(fileOrigin.FileName) && FilesSection.FindByMasterOrSatelliteFile(this.DriverContext.Database, fileOrigin.FileName) == null && File.Exists(fileOrigin.FileName);
  }

  private void SetFilesReadOnlyAttribute(bool readOnly)
  {
    this.SetFileReadOnlyAttribute(this.DocumentFiles.MasterFile, readOnly);
    foreach (string satellite in (Collection<string>) this.DocumentFiles.Satellites)
      this.SetFileReadOnlyAttribute(satellite, readOnly);
  }

  private void SetFileReadOnlyAttribute(string filePath, bool readOnly)
  {
    FileAttributes attributes = File.GetAttributes(filePath);
    FileAttributes fileAttributes = readOnly ? attributes | FileAttributes.ReadOnly : attributes & ~FileAttributes.ReadOnly;
    if (fileAttributes == attributes)
      return;
    File.SetAttributes(filePath, fileAttributes);
    this.openFilesService.SetReadOnlyFlag(filePath, readOnly);
  }

  private void EmitFilesUploadAction()
  {
    List<string> stringList = new List<string>(1 + this.DocumentFiles.Satellites.Count);
    stringList.Add(this.DocumentFiles.MasterFile);
    stringList.AddRange((IEnumerable<string>) this.DocumentFiles.Satellites);
    List<UploadFileInfo> items = new List<UploadFileInfo>(stringList.Count);
    if (this.DocumentObject.NewObject)
    {
      foreach (string str in stringList)
      {
        string relativePath = PathUtils.GetRelativePath(str, this.DocumentFilesBaseDirectory, RelativePathOptions.ThrowIfNotPossible);
        items.Add(new UploadFileInfo(relativePath, str));
      }
    }
    else
    {
      List<FileState> areaFiles = new List<FileState>(stringList.Count);
      foreach (string str in stringList)
      {
        string relativePath = PathUtils.GetRelativePath(str, this.DocumentFilesBaseDirectory, RelativePathOptions.ThrowIfNotPossible);
        areaFiles.Add(FileState.FromFile(str, relativePath));
      }
      Intermech.Files.DBObjectState objectState = this.fileVaultService.DBObjectsInfo.GetObjectState(this.DocumentObject.ObjectId, true);
      if (!objectState.IsEditableState)
        objectState = new Intermech.Files.DBObjectState(objectState.Id, objectState.ObjectId, ObjectModifyModes.InBase, objectState.Caption);
      DBObjectFilesDifferenceCalculator differenceCalculator = this.fileVaultService.WorkArea.CreateObjectFilesDifferenceCalculator();
      differenceCalculator.Add(objectState, areaFiles);
      differenceCalculator.Calculate();
      foreach (DBObjectFilesDifferences unsavedObject in this.fileVaultService.DBObjectsInfo.FindUnsavedObjects(differenceCalculator.Results, true))
      {
        foreach (FileDifferencePair differencePair in unsavedObject.DifferencePairs)
          items.Add(new UploadFileInfo(differencePair.LocalState.FileName, Path.Combine(this.DocumentFilesBaseDirectory, differencePair.LocalState.FileName)));
      }
    }
    if (items.Count <= 0)
      return;
    if (UIReport.Enabled)
      CaptureChangesReportHelper.ReportFileUploadData((ICollection<string>) items.ConvertAll<string>((Converter<UploadFileInfo, string>) (item => item.FileName)));
    IDBObjectRef dbObjectRef = (IDBObjectRef) new DBObjectEntityRef(this.DocumentEntity);
    ObjectActionsSection objectActionsSection1 = this.DocumentEntity.Sections.Get<ObjectActionsSection>();
    ObjectActionsSection objectActionsSection2 = objectActionsSection1;
    objectActionsSection2.RequireCheckout = ((objectActionsSection2.RequireCheckout ? 1 : 0) | 1) != 0;
    UploadFilesAction uploadFilesAction = new UploadFilesAction(dbObjectRef, (IList<UploadFileInfo>) items);
    this.SetupFilesUploadAction(uploadFilesAction);
    objectActionsSection1.ObjectActions.ServerActions.Add((IAction) uploadFilesAction);
    if (!string.IsNullOrEmpty(this.CustomFilesBaseDirectory))
      return;
    objectActionsSection1.ObjectActions.ClientActions.Add((IAction) new TrackUploadedFileAction(this.fileVaultService.WorkArea.FileTracker, dbObjectRef, (IObjectFilesUploadResult) uploadFilesAction));
  }

  /// <summary>
  /// Позволяет выполнить тонкую настройку операции записи измененных файлов документа в базу данных.
  /// </summary>
  /// <param name="action">Действие записи файлов документа</param>
  protected virtual void SetupFilesUploadAction(UploadFilesAction action)
  {
  }

  /// <summary>
  /// Позволяет обработать другие объекты, связанные с документом (например, изделия).
  /// </summary>
  protected virtual void ProcessDerivedObjects()
  {
  }

  /// <summary>Выполняет анализ связей документа.</summary>
  protected virtual void ProcessRelations()
  {
  }

  protected virtual void WriteChangesToDocumentFiles()
  {
    if (this.DocumentAttributes.WorkingSet.HasChanges)
      this.EncodeDocumentAttributes((ICollection<StringKey>) this.DocumentAttributes.WorkingSet.GetChangedItemsKeys(), this.DocumentAttributes.WorkingSet, this.DocumentAttributes.EmbeddedSet);
    if (!this.DocumentAttributes.EmbeddedSet.Bag.HasChanges || !this.WriteFileProperties(this.DocumentAttributes.EmbeddedSet))
      return;
    AnalyzerChangesSection.Mark(this.DocumentEntity);
  }

  /// <summary>
  /// Сохраняет измененные файлы документа на диск. Этот метод используется для сохранения на диск любых изменений в файле документа, а не только
  /// сделанных интегратором. Например, метод будет вызван, если пользователь не сохранил документ в приложении-редакторе перед вызовом команды IPS.
  /// </summary>
  protected abstract IEnumerable<CooperativeState> SaveModifiedDocumentFiles();

  /// <summary>
  /// Определяет тип документа при импорте в IPS, если тип документа не был выбран до создания обработчика документа. Реализация по умолчанию просто возвращает null.
  /// </summary>
  /// <returns>Тип документа или null</returns>
  protected virtual SelectedObjectType DetectNewDocumentType() => (SelectedObjectType) null;

  /// <summary>Читает значения свойств из файла документа.</summary>
  /// <returns>Контейнер со значениями свойств. Если у файла нет свойств, либо нет соответствующего API, то метод должен вернуть пустой контейнер</returns>
  protected abstract ContainerValues ReadFileProperties();

  /// <summary>
  /// Записывает измененные значения свойств в файл документа. Этот метод вызывается только при наличии изменений в свойствах.
  /// Если поддерживается только чтение свойств, то этот метод должен сбросить исключение.
  /// </summary>
  /// <param name="fileProperties">Контейнер со значениями свойств</param>
  /// <returns>true, если запись в файл была произведена</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на контейнер не может быть null</exception>
  /// <exception cref="T:System.NotSupportedException">Запись свойств в файл документа не поддерживается</exception>
  protected abstract bool WriteFileProperties(ContainerValues fileProperties);

  /// <summary>
  /// Выполняет декодирование значений атрибутов документа из свойств файла.
  /// </summary>
  /// <param name="fileProperties">Контейнер со свойствами файла</param>
  /// <returns>Контейнер с значениями атрибутов</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на контейнер со свойствами файла не может быть null</exception>
  protected abstract ValueBag DecodeDocumentAttributes(ContainerValues fileProperties);

  /// <summary>
  /// Выполняет обратное кодирование значений атрибутов документа в значения свойств файла. Если поддерживается
  /// только чтение свойств, но не запись, то этот метод может не выполнять кодирование. Исключение при этом сбрасываться не должно.
  /// </summary>
  /// <param name="attributeKeys">Список имен кодируемых атрибутов</param>
  /// <param name="attributes">Контейнер с значениями атрибутов</param>
  /// <param name="fileProperties">Контейнер со свойствами файла</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылки на контейнеры не могут быть null</exception>
  protected abstract void EncodeDocumentAttributes(
    ICollection<StringKey> attributeKeys,
    ValueBag attributes,
    ContainerValues fileProperties);
}
