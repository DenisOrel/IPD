// Decompiled with JetBrains decompiler
// Type: Intermech.Services.IMViewer.IMViewerClientService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.Client.Core;
using Intermech.Data;
using Intermech.Diagnostics;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data.SidecarObjects;
using Intermech.IO;
using Intermech.Kernel.Search;
using Intermech.Runtime;
using Intermech.Tools.Data;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;

#nullable disable
namespace Intermech.Services.IMViewer;

/// <summary>Реализация клиентского сервиса интеграции с IMViewer.</summary>
/// <remarks>Реализация является thread safe.</remarks>
internal sealed class IMViewerClientService : IIMViewerClientService, IIMViewerObjectCreatorService
{
  private readonly IFileVault fileVaultService;
  private readonly IMViewerObjectsIDCache imvIDCache;
  private readonly SidecarObjectsOperations imvOperations;
  private readonly ColumnDescriptor[] publishDataQueryColumns;
  private object syncRoot;
  private IMViewerSystemSettings settings;
  private string converterBaseDirectory;
  private static readonly ErrorInfo[] emptyErrors = new ErrorInfo[0];

  public IMViewerClientService(IFileVault fileVaultService, IMViewerObjectsIDCache imvIDCache)
  {
    if (fileVaultService == null)
      throw new ArgumentNullException(nameof (fileVaultService));
    if (imvIDCache == null)
      throw new ArgumentNullException(nameof (imvIDCache));
    this.fileVaultService = fileVaultService;
    this.imvIDCache = imvIDCache;
    this.imvOperations = new SidecarObjectsOperations((SidecarObjectsIDCache) imvIDCache);
    ColumnDescriptor[] columnDescriptorArray = new ColumnDescriptor[5];
    ColumnDescriptor columnDescriptor = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_ID);
    columnDescriptor.Contents = ColumnContents.Text;
    columnDescriptorArray[0] = columnDescriptor;
    columnDescriptor = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID);
    columnDescriptor.Contents = ColumnContents.Text;
    columnDescriptorArray[1] = columnDescriptor;
    columnDescriptor = new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION);
    columnDescriptor.Contents = ColumnContents.Text;
    columnDescriptorArray[2] = columnDescriptor;
    columnDescriptor = new ColumnDescriptor((object) this.imvIDCache.ContentStatus.Id);
    columnDescriptor.Contents = ColumnContents.Text;
    columnDescriptorArray[3] = columnDescriptor;
    columnDescriptor = new ColumnDescriptor((object) this.imvIDCache.SourceDocumentReference.Id);
    columnDescriptor.Contents = ColumnContents.ID;
    columnDescriptorArray[4] = columnDescriptor;
    this.publishDataQueryColumns = columnDescriptorArray;
    this.syncRoot = new object();
  }

  /// <summary>
  /// Возвращает глобальные настройки интеграции с IMViewer.
  /// Настройки зачитываются при старте приложения и в дальнейшем не изменяются.
  /// </summary>
  public IMViewerSystemSettings Settings
  {
    [DebuggerStepThrough] get
    {
      lock (this.syncRoot)
      {
        if (this.settings == null)
          this.settings = this.LoadServerSettings();
        return this.settings;
      }
    }
  }

  private IMViewerSystemSettings LoadServerSettings()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ServiceUtils.GetService<IIMViewerServerService>((object) sessionKeeper.Session, true).Settings;
  }

  /// <summary>
  /// Проверяет, может ли у документа указанного типа быть связанный с ним объект IMViewer.
  /// </summary>
  /// <param name="documentTypeId">Идентификатор типа документа</param>
  /// <returns>Результат проверки</returns>
  /// <exception cref="T:System.ArgumentException">параметр <paramref name="documentTypeId" /> содержит некорректное значение</exception>
  public bool CanHaveViewerObject(int documentTypeId)
  {
    IntegratorObject integratorObject = documentTypeId != -1 ? IntegratorServices.Find(documentTypeId) : throw new ArgumentException("Не задан идентификатор типа объекта IPS", nameof (documentTypeId));
    if (integratorObject == null)
      return false;
    ICADSettingsService service = IntegratorServices.GetService<ICADSettingsService>(integratorObject, false);
    if (service == null)
      return false;
    try
    {
      return this.IsCADModelType(documentTypeId, integratorObject, service);
    }
    catch (Exception ex)
    {
      string currentMethodName = this.GetCurrentMethodName(nameof (CanHaveViewerObject));
      SuppressedExceptions.TraceException(ex, currentMethodName);
      return false;
    }
  }

  private bool IsCADModelType(
    int documentTypeId,
    IntegratorObject integratorRef,
    ICADSettingsService cadSettingsService)
  {
    CADSettings cadSettings = cadSettingsService.GetCADSettings();
    if (cadSettings.StandardPartType != null && cadSettings.StandardPartType.Id == documentTypeId)
      return true;
    DocumentGroup byDocumentType = cadSettings.FileDocumentGroups.FindByDocumentType(documentTypeId, false);
    return byDocumentType != null && (!(byDocumentType.Name != "Assembly") || !(byDocumentType.Name != "Part"));
  }

  /// <summary>
  /// Проверяет, имеется ли у указанного документа связанный с ним объект IMViewer.
  /// </summary>
  /// <param name="documentId">Идентификатор версии документа</param>
  /// <param name="documentTypeId">Идентификатор типа документа</param>
  /// <returns>Результат проверки</returns>
  /// <exception cref="T:System.ArgumentException">параметр <paramref name="documentId" /> содержит некорректное значение; параметр <paramref name="documentTypeId" /> содержит некорректное значение</exception>
  public bool HasViewerObject(long documentId, int documentTypeId)
  {
    if (Intermech.Consts.IsUndefinedObjectId(documentId))
      throw new ArgumentException("Не задан идентификатор версии объекта IPS.", nameof (documentId));
    if (documentTypeId == -1)
      throw new ArgumentException("Не задан идентификатор типа объекта IPS.", nameof (documentTypeId));
    return this.CanHaveViewerObject(documentTypeId) && !Intermech.Consts.IsUndefinedObjectId(this.imvOperations.Find(documentId));
  }

  /// <summary>
  /// Находит для указанного документа связанный с ним объект IMViewer.
  /// </summary>
  /// <param name="documentId">Идентификатор версии документа</param>
  /// <param name="documentTypeId">Идентификатор типа документа</param>
  /// <returns>Идентификатор версии объекта IMViewer</returns>
  /// <exception cref="T:System.ArgumentException">параметр <paramref name="documentId" /> содержит некорректное значение; параметр <paramref name="documentTypeId" /> содержит некорректное значение</exception>
  public long FindViewerObjectId(long documentId, int documentTypeId)
  {
    if (Intermech.Consts.IsUndefinedObjectId(documentId))
      throw new ArgumentException("Не задан идентификатор версии объекта IPS.", nameof (documentId));
    if (documentTypeId == -1)
      throw new ArgumentException("Не задан идентификатор типа объекта IPS.", nameof (documentTypeId));
    long viewerObjectId = 0;
    if (this.CanHaveViewerObject(documentTypeId))
      viewerObjectId = this.imvOperations.Find(documentId);
    return viewerObjectId;
  }

  /// <summary>
  /// Возвращает данные, необходимые для извлечения на локальный диск файлов IMViewer,
  /// связанных с указанным документом.
  /// </summary>
  /// <param name="documentId">Идентификатор версии документа</param>
  /// <param name="documentTypeId">Идентификатор типа документа</param>
  /// <param name="versionsRule">Правило подбора версий</param>
  /// <returns>Данные для извлечения на локальный диск файлов IMViewer</returns>
  /// <exception cref="T:System.ArgumentException">параметр <paramref name="documentId" /> содержит некорректное значение; параметр <paramref name="documentTypeId" /> содержит некорректное значение</exception>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="versionsRule" /> содержит null</exception>
  public List<IMViewerPublishItem> GetViewerDataForOpenFiles(
    long documentId,
    int documentTypeId,
    VersionsRulePackage versionsRule)
  {
    if (Intermech.Consts.IsUndefinedObjectId(documentId))
      throw new ArgumentException("Не задан идентификатор версии объекта IPS.", nameof (documentId));
    if (documentTypeId == -1)
      throw new ArgumentException("Не задан идентификатор типа объекта IPS.", nameof (documentTypeId));
    if (versionsRule == null)
      throw new ArgumentNullException(nameof (versionsRule));
    return this.GetViewerPublishData((IList<DBObjectState>) this.fileVaultService.DBObjectsInfo.CreateStateListForObjectTree(documentId, versionsRule));
  }

  private List<IMViewerPublishItem> GetViewerPublishData(IList<DBObjectState> sourceDocuments)
  {
    DBObjectState[] dbObjectStateArray = sourceDocuments != null ? new DBObjectState[sourceDocuments.Count] : throw new ArgumentNullException(nameof (sourceDocuments));
    ObjectContentStatus?[] nullableArray = new ObjectContentStatus?[sourceDocuments.Count];
    foreach (Tuple<DBObjectState, DataRow> manyAsRow in this.imvOperations.FindManyAsRows<DBObjectState>(sourceDocuments, (System.Func<DBObjectState, long>) (x => x.ObjectId), this.publishDataQueryColumns))
    {
      DBObjectState dbObjectState1 = manyAsRow.Item1;
      DataRow dataRow = manyAsRow.Item2;
      DBObjectState dbObjectState2 = new DBObjectState(Convert.ToInt64(dataRow[0]), Convert.ToInt64(dataRow[1]), ObjectModifyModes.InBase, Convert.ToString(dataRow[2]));
      ObjectContentStatus int64 = (ObjectContentStatus) Convert.ToInt64(dataRow[3]);
      Convert.ToInt64(dataRow[4]);
      int index = sourceDocuments.IndexOf(dbObjectState1);
      if (index >= 0)
      {
        dbObjectStateArray[index] = dbObjectState2;
        nullableArray[index] = new ObjectContentStatus?(int64);
      }
    }
    List<IMViewerPublishItem> viewerPublishData = new List<IMViewerPublishItem>(sourceDocuments.Count);
    for (int index = 0; index < sourceDocuments.Count; ++index)
    {
      DBObjectState sourceDocument = sourceDocuments[index];
      DBObjectState sidecarObject = dbObjectStateArray[index];
      ObjectContentStatus sidecarContentStatus = nullableArray[index] ?? ObjectContentStatus.NotSet;
      viewerPublishData.Add(new IMViewerPublishItem(sourceDocument, sidecarObject, sidecarContentStatus));
    }
    return viewerPublishData;
  }

  /// <summary>Возвращает имя конфигурации 3D-модели для IMViewer.</summary>
  /// <param name="documentId">Идентификатор версии документа</param>
  /// <param name="documentTypeId">Идентификатор типа документа</param>
  /// <param name="savedConfigurationName">Имя конфигурации, сохраненное в базе данных</param>
  /// <returns>Имя конфигурации 3D-модели для IMViewer</returns>
  /// <exception cref="T:System.ArgumentException">параметр <paramref name="documentId" /> содержит некорректное значение; параметр <paramref name="documentTypeId" /> содержит некорректное значение</exception>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="savedConfigurationName" /> содержит null</exception>
  public string GetViewerModelConfigurationName(
    long documentId,
    int documentTypeId,
    string savedConfigurationName)
  {
    if (Intermech.Consts.IsUndefinedObjectId(documentId))
      throw new ArgumentException("Не задан идентификатор версии объекта IPS.", nameof (documentId));
    if (documentTypeId == -1)
      throw new ArgumentException("Не задан идентификатор типа объекта IPS.", nameof (documentTypeId));
    if (savedConfigurationName == null)
      throw new ArgumentNullException(nameof (savedConfigurationName));
    return IntegratorServices.GetService<ICADInterfaceService>(IntegratorServices.Find(documentTypeId) ?? throw new InvalidOperationException("У документа должен быть настроен интегратор."), true).GetArticleRawConfigurationName(this.fileVaultService.DBFilesInfo.GetMasterFileName(documentId, true), savedConfigurationName);
  }

  /// <summary>
  /// Создает или обновляет объект IMViewer, непосредственно связанный с указанным документом.
  /// </summary>
  /// <param name="documentId">Идентификатор версии документа</param>
  /// <param name="documentTypeId">Идентификатор типа документа</param>
  /// <param name="versionsRule">Правило подбора версий</param>
  /// <param name="cadSystem">Объект CAD-системы</param>
  /// <param name="preOpenDocumentsMode">Режим предварительного открытия документа в CAD-системе</param>
  /// <returns>Список ошибок обновления объектов IMViewer</returns>
  /// <exception cref="T:System.ArgumentException">параметр <paramref name="documentId" /> содержит некорректное значение; параметр <paramref name="documentTypeId" /> содержит некорректное значение</exception>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="cadSystem" /> содержит null; параметр <paramref name="versionsRule" /> содержит null</exception>
  public IList<ErrorInfo> CreateOrUpdateViewerObject(
    long documentId,
    int documentTypeId,
    VersionsRulePackage versionsRule,
    CADSystemProxy cadSystem,
    bool preOpenDocumentsMode)
  {
    if (Intermech.Consts.IsUndefinedObjectId(documentId))
      throw new ArgumentException("Не задан идентификатор версии объекта IPS.", nameof (documentId));
    if (documentTypeId == -1)
      throw new ArgumentException("Не задан идентификатор типа объекта IPS.", nameof (documentTypeId));
    if (versionsRule == null)
      throw new ArgumentNullException(nameof (versionsRule));
    if (cadSystem == null)
      throw new ArgumentNullException(nameof (cadSystem));
    if (!this.CanHaveViewerObject(documentTypeId))
      throw new InvalidOperationException($"Недопустимый тип объекта IPS для создания/обновления объекта IMViewer (ид. типа = {documentTypeId}).");
    IList<ErrorInfo> updateViewerObject = (IList<ErrorInfo>) IMViewerClientService.emptyErrors;
    IMViewerObjectUpdateRecord objectUpdateRecord = this.CreateViewerObjectUpdateRecord(documentId, documentTypeId);
    if (this.CanCreateOrUpdateViewerObject(objectUpdateRecord))
    {
      string areaPath = this.fileVaultService.WorkArea.AreaPath;
      string documentPath = this.fileVaultService.PublishTree(objectUpdateRecord.DocumentState.ObjectId, true, versionsRule, (IFileArea) this.fileVaultService.WorkArea);
      try
      {
        string viewerFile = this.CreateViewerFile(documentPath, areaPath, cadSystem, preOpenDocumentsMode);
        this.CreateOrUpdateViewerObjectCore(objectUpdateRecord, viewerFile);
      }
      catch (Exception ex)
      {
        if (updateViewerObject == IMViewerClientService.emptyErrors)
          updateViewerObject = (IList<ErrorInfo>) new List<ErrorInfo>(1);
        updateViewerObject.Add(this.CreateViewerObjectUpdateErrorInfo(ex, objectUpdateRecord));
      }
    }
    return updateViewerObject;
  }

  /// <summary>
  /// Создает или рекурсивно обновляет все объекты IMViewer, связанные с указанным документом.
  /// Состав документа раскручивается по связям типа "Состав документации".
  /// </summary>
  /// <param name="documentId">Идентификатор версии документа</param>
  /// <param name="documentTypeId">Идентификатор типа документа</param>
  /// <param name="versionsRule">Правило подбора версий</param>
  /// <param name="cadSystem">Объект CAD-системы</param>
  /// <param name="preOpenDocumentsMode">Режим предварительного открытия документа в CAD-системе</param>
  /// <returns>Список ошибок обновления объектов IMViewer</returns>
  /// <exception cref="T:System.ArgumentException">параметр <paramref name="documentId" /> содержит некорректное значение; параметр <paramref name="documentTypeId" /> содержит некорректное значение</exception>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="cadSystem" /> содержит null; параметр <paramref name="versionsRule" /> содержит null</exception>
  public IList<ErrorInfo> CreateOrUpdateViewerObjectsRecursive(
    long documentId,
    int documentTypeId,
    VersionsRulePackage versionsRule,
    CADSystemProxy cadSystem,
    bool preOpenDocumentsMode)
  {
    if (Intermech.Consts.IsUndefinedObjectId(documentId))
      throw new ArgumentException("Не задан идентификатор версии объекта IPS.", nameof (documentId));
    if (documentTypeId == -1)
      throw new ArgumentException("Не задан идентификатор типа объекта IPS.", nameof (documentTypeId));
    if (versionsRule == null)
      throw new ArgumentNullException(nameof (versionsRule));
    if (cadSystem == null)
      throw new ArgumentNullException(nameof (cadSystem));
    if (!this.CanHaveViewerObject(documentTypeId))
      throw new InvalidOperationException($"Недопустимый тип объекта IPS для создания/обновления объекта IMViewer (ид. типа = {documentTypeId}).");
    List<DBObjectState> listForObjectTree = this.fileVaultService.DBObjectsInfo.CreateStateListForObjectTree(documentId, versionsRule);
    List<IMViewerObjectUpdateRecord> all = listForObjectTree.ConvertAll<IMViewerObjectUpdateRecord>(new Converter<DBObjectState, IMViewerObjectUpdateRecord>(this.CreateViewerObjectUpdateRecord)).FindAll(new Predicate<IMViewerObjectUpdateRecord>(this.CanCreateOrUpdateViewerObject));
    if (all.Count == 0)
      return (IList<ErrorInfo>) new List<ErrorInfo>(0);
    this.fileVaultService.WorkArea.Publish((IList<DBObjectState>) listForObjectTree, (IReplaceFilePolicy) new PreserveAnyChanges());
    string areaPath = this.fileVaultService.WorkArea.AreaPath;
    IList<ErrorInfo> objectsRecursive = (IList<ErrorInfo>) IMViewerClientService.emptyErrors;
    for (int index = 0; index < all.Count; ++index)
    {
      IMViewerObjectUpdateRecord updateRecord = all[index];
      string masterFileName = this.fileVaultService.DBFilesInfo.GetMasterFileName(updateRecord.DocumentState.ObjectId, false);
      if (!string.IsNullOrEmpty(masterFileName))
      {
        string documentPath = Path.Combine(areaPath, masterFileName);
        try
        {
          string viewerFile = this.CreateViewerFile(documentPath, areaPath, cadSystem, preOpenDocumentsMode);
          this.CreateOrUpdateViewerObjectCore(updateRecord, viewerFile);
        }
        catch (Exception ex)
        {
          if (objectsRecursive == IMViewerClientService.emptyErrors)
            objectsRecursive = (IList<ErrorInfo>) new List<ErrorInfo>();
          objectsRecursive.Add(this.CreateViewerObjectUpdateErrorInfo(ex, updateRecord));
        }
      }
    }
    return objectsRecursive;
  }

  /// <summary>
  /// Создает пустой объект IMViewer, непосредственно связанный с указанным документом.
  /// </summary>
  /// <param name="documentId">Идентификатор версии документа</param>
  /// <param name="documentTypeId">Идентификатор типа документа</param>
  /// <param name="createBlankObject">Признак создания заготовки объекта</param>
  /// <returns>Идентификатор созданного объекта IMViewer</returns>
  /// <exception cref="T:System.ArgumentException">параметр <paramref name="documentId" /> содержит некорректное значение; параметр <paramref name="documentTypeId" /> содержит некорректное значение</exception>
  /// <exception cref="T:System.Exception">При создании объекта IMViewer произошла ошибка</exception>
  public long CreateEmptyViewerObject(long documentId, int documentTypeId, bool createBlankObject)
  {
    if (Intermech.Consts.IsUndefinedObjectId(documentId))
      throw new ArgumentException("Не задан идентификатор версии объекта IPS.", nameof (documentId));
    if (documentTypeId == -1)
      throw new ArgumentException("Не задан идентификатор типа объекта IPS.", nameof (documentTypeId));
    if (!this.CanHaveViewerObject(documentTypeId))
      throw new InvalidOperationException($"Недопустимый тип объекта IPS для создания/обновления объекта IMViewer (ид. типа = {documentTypeId}).");
    return this.CreateEmptyViewerObjectCore(documentId, documentTypeId, createBlankObject);
  }

  private IMViewerObjectUpdateRecord CreateViewerObjectUpdateRecord(
    long documentId,
    int documentTypeId)
  {
    return new IMViewerObjectUpdateRecord(this.fileVaultService.DBObjectsInfo.GetObjectState(documentId, true), documentTypeId)
    {
      ViewerObjectId = this.FindViewerObjectId(documentId, documentTypeId)
    };
  }

  private IMViewerObjectUpdateRecord CreateViewerObjectUpdateRecord(DBObjectState documentState)
  {
    IMViewerObjectUpdateRecord objectUpdateRecord = new IMViewerObjectUpdateRecord(documentState, DBHelper.GetObjectType(documentState.ObjectId));
    objectUpdateRecord.ViewerObjectId = this.FindViewerObjectId(objectUpdateRecord.DocumentState.ObjectId, objectUpdateRecord.DocumentTypeId);
    return objectUpdateRecord;
  }

  private bool CanCreateOrUpdateViewerObject(IMViewerObjectUpdateRecord updateRecord)
  {
    return Intermech.Consts.IsUndefinedObjectId(updateRecord.ViewerObjectId) || this.ReadViewerObjectContentStatus(updateRecord.ViewerObjectId) != ObjectContentStatus.Actual;
  }

  private void CreateOrUpdateViewerObjectCore(
    IMViewerObjectUpdateRecord updateRecord,
    string imvFilePath)
  {
    if (Intermech.Consts.IsUndefinedObjectId(updateRecord.ViewerObjectId))
      updateRecord.ViewerObjectId = this.CreateEmptyViewerObjectCore(updateRecord.DocumentState.ObjectId, updateRecord.DocumentTypeId);
    this.UpdateViewerObjectFile(updateRecord.ViewerObjectId, imvFilePath);
    this.UpdateViewerObjectContentStatus(updateRecord.ViewerObjectId, ObjectContentStatus.Actual);
  }

  private ErrorInfo CreateViewerObjectUpdateErrorInfo(
    Exception exception,
    IMViewerObjectUpdateRecord updateRecord)
  {
    return ErrorInfo.FromException(exception, $"При обновлении файлов IMViewer для объекта IPS '{updateRecord.DocumentState.Caption}' произошла ошибка.");
  }

  private long CreateEmptyViewerObjectCore(
    long documentId,
    int documentTypeId,
    bool createBlankObject = false)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(this.imvIDCache.SidecarObjectType.Id);
      IDBObject documentObject = sessionKeeper.Session.GetObject(documentId);
      IDBObject viewerObjectRecursive = this.CreateEmptyViewerObjectRecursive(documentId, documentTypeId, documentObject, sessionKeeper.Session, objectCollection);
      if (!createBlankObject)
        viewerObjectRecursive.CommitCreation(true);
      return viewerObjectRecursive.ObjectID;
    }
  }

  private IDBObject CreateEmptyViewerObjectRecursive(
    long documentId,
    int documentTypeId,
    IDBObject documentObject,
    IUserSession userSession,
    IDBObjectCollection imvObjects)
  {
    long parentVersionId = documentObject.ParentVersionID;
    if (Intermech.Consts.IsUndefinedObjectId(parentVersionId))
    {
      IDBObject imvObject = imvObjects.Create();
      imvObject.Caption = this.CreateViewerObjectCaption(documentId, documentTypeId);
      this.UpdateViewerObjectSourceDocumentReference(imvObject, documentId);
      return imvObject;
    }
    IDBObject objectActualCopy = userSession.GetObjectActualCopy(parentVersionId, false);
    long objectId = objectActualCopy.ObjectID;
    int objectType = objectActualCopy.ObjectType;
    long num = this.FindViewerObjectId(objectId, objectType);
    if (Intermech.Consts.IsUndefinedObjectId(num))
    {
      IDBObject viewerObjectRecursive = this.CreateEmptyViewerObjectRecursive(objectId, objectType, objectActualCopy, userSession, imvObjects);
      viewerObjectRecursive.CommitCreation(true);
      num = viewerObjectRecursive.ObjectID;
    }
    IDBObject version = imvObjects.CreateVersion(num);
    version.Caption = this.CreateViewerObjectCaption(documentId, documentTypeId);
    this.UpdateViewerObjectSourceDocumentReference(version, documentId);
    return version;
  }

  private string CreateViewerObjectCaption(long documentId, int documentTypeId)
  {
    StringKey[] identityAttributeNames = new StringKey[2]
    {
      (StringKey) IDCache.Default.Designation.Text,
      (StringKey) IDCache.Default.Name.Text
    };
    ValueBag documentAttributeBag = new ValueBag(identityAttributeNames.Length);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(documentId, true);
      foreach (StringKey stringKey in identityAttributeNames)
      {
        IDBAttribute attributeByName = dbObject.GetAttributeByName((string) stringKey);
        if (attributeByName != null && !attributeByName.IsNull)
        {
          string str = DocumentDesignationHelper.RemoveDocCode(attributeByName.AsString, documentTypeId);
          if (!string.IsNullOrEmpty(str))
            documentAttributeBag.Add(stringKey, (object) str);
        }
      }
    }
    return this.CreateViewerObjectCaption(documentId, documentTypeId, documentAttributeBag, (IEnumerable<StringKey>) identityAttributeNames);
  }

  private void UpdateViewerObjectFile(long imvObjectId, string imvFilePath)
  {
    FileOperations.BatchUpdateFiles(imvObjectId, (IList<IFileAttributeAction>) new IFileAttributeAction[1]
    {
      (IFileAttributeAction) new UploadFileAction(FileState.FromFile(imvFilePath, PathUtils.GetRelativePath(imvFilePath, this.ConverterBaseDirectory, RelativePathOptions.ThrowIfNotPossible)), imvFilePath)
      {
        AllowNewFiles = true
      }
    });
  }

  private ObjectContentStatus ReadViewerObjectContentStatus(long imvObjectId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return this.ReadViewerObjectContentStatus(sessionKeeper.Session.GetObject(imvObjectId, true));
  }

  private ObjectContentStatus ReadViewerObjectContentStatus(IDBObject imvObject)
  {
    return (ObjectContentStatus) imvObject.GetAttributeByID(this.imvIDCache.ContentStatus.Id).AsInteger;
  }

  private void UpdateViewerObjectContentStatus(long imvObjectId, ObjectContentStatus newStatus)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.UpdateViewerObjectContentStatus(sessionKeeper.Session.GetObject(imvObjectId), newStatus);
  }

  private void UpdateViewerObjectContentStatus(IDBObject imvObject, ObjectContentStatus newStatus)
  {
    IDBAttribute attributeById = imvObject.GetAttributeByID(this.imvIDCache.ContentStatus.Id);
    if (attributeById.AsInteger == (long) newStatus)
      return;
    attributeById.AsInteger = (long) newStatus;
  }

  private bool TryUpdateViewerObjectContentStatus(
    long imvObjectId,
    ObjectContentStatus oldStatus,
    ObjectContentStatus newStatus)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject imvObject = sessionKeeper.Session.GetObject(imvObjectId);
      if (this.ReadViewerObjectContentStatus(imvObject) != oldStatus)
        return false;
      this.UpdateViewerObjectContentStatus(imvObject, newStatus);
      return true;
    }
  }

  private void UpdateViewerObjectSourceDocumentReference(IDBObject imvObject, long sourceDocumentId)
  {
    imvObject.GetAttributeByID(this.imvIDCache.SourceDocumentReference.Id).AsInteger = Math.Abs(sourceDocumentId);
  }

  /// <summary>
  /// Изменяет у объекта IMViewer статус с "актуальный" на "устаревший".
  /// </summary>
  /// <param name="documentId">Идентификатор версии документа</param>
  /// <param name="documentTypeId">Идентификатор типа документа</param>
  /// <exception cref="T:System.ArgumentException">параметр <paramref name="documentId" /> содержит некорректное значение; параметр <paramref name="documentTypeId" /> содержит некорректное значение</exception>
  public void MakeViewerObjectOutdated(long documentId, int documentTypeId)
  {
    if (Intermech.Consts.IsUndefinedObjectId(documentId))
      throw new ArgumentException("Не задан идентификатор версии объекта IPS.", nameof (documentId));
    if (documentTypeId == -1)
      throw new ArgumentException("Не задан идентификатор типа объекта IPS.", nameof (documentTypeId));
    if (!this.CanHaveViewerObject(documentTypeId))
      throw new InvalidOperationException($"Недопустимый тип объекта IPS для создания/обновления объекта IMViewer (ид. типа = {documentTypeId}).");
    IMViewerObjectUpdateRecord objectUpdateRecord = new IMViewerObjectUpdateRecord(this.fileVaultService.DBObjectsInfo.GetObjectState(documentId, true), documentTypeId);
    objectUpdateRecord.ViewerObjectId = this.FindViewerObjectId(documentId, documentTypeId);
    if (Intermech.Consts.IsUndefinedObjectId(objectUpdateRecord.ViewerObjectId))
      return;
    this.TryUpdateViewerObjectContentStatus(objectUpdateRecord.ViewerObjectId, ObjectContentStatus.Actual, ObjectContentStatus.Outdated);
  }

  /// <summary>
  /// Возвращает путь к выделенной папке для генерации IMV-файлов.
  /// </summary>
  public string ConverterBaseDirectory
  {
    [DebuggerStepThrough] get
    {
      lock (this.syncRoot)
      {
        if (this.converterBaseDirectory == null)
          this.converterBaseDirectory = Path.Combine(this.fileVaultService.TempArea.AreaPath, "IMViewer");
        return this.converterBaseDirectory;
      }
    }
  }

  /// <summary>
  /// Вычисляет значение заголовка для объекта IMViewer, используя значения атрибутов исходного документа.
  /// </summary>
  /// <param name="documentId">Идентификатор версии документа</param>
  /// <param name="documentTypeId">Идентификатор типа документа</param>
  /// <param name="documentAttributeBag">Контейнер атрибутов исходного документа</param>
  /// <param name="identityAttributeNames">Коллекция имен идентифицирующих атрибутов документа</param>
  /// <returns>Заголовок для объекта IMViewer</returns>
  /// <exception cref="T:System.ArgumentException">параметр <paramref name="documentId" /> содержит некорректное значение; параметр <paramref name="documentTypeId" /> содержит некорректное значение</exception>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="documentAttributeBag" /> содержит null; параметр <paramref name="identityAttributeNames" /> содержит null</exception>
  public string CreateViewerObjectCaption(
    long documentId,
    int documentTypeId,
    ValueBag documentAttributeBag,
    IEnumerable<StringKey> identityAttributeNames)
  {
    if (Intermech.Consts.IsUndefinedObjectId(documentId))
      throw new ArgumentException("Не задан идентификатор версии объекта IPS.", nameof (documentId));
    if (documentTypeId == -1)
      throw new ArgumentException("Не задан идентификатор типа объекта IPS.", nameof (documentTypeId));
    if (documentAttributeBag == null)
      throw new ArgumentNullException(nameof (documentAttributeBag));
    if (identityAttributeNames == null)
      throw new ArgumentNullException(nameof (identityAttributeNames));
    StringBuilder stringBuilder = new StringBuilder();
    int num1 = 0;
    int num2 = 0;
    bool flag = false;
    foreach (StringKey identityAttributeName in identityAttributeNames)
    {
      string str = documentAttributeBag.Read<string>(identityAttributeName, (string) null);
      if (!string.IsNullOrEmpty(str))
      {
        if (num2 == 0)
        {
          stringBuilder.Append(str);
          if (num1 == 0)
            flag = true;
        }
        else if (num2 == 1 & flag)
        {
          stringBuilder.Append(' ').Append('(');
          stringBuilder.Append(str);
        }
        else
        {
          stringBuilder.Append(' ').Append('/').Append(' ');
          stringBuilder.Append(str);
        }
        ++num2;
      }
      ++num1;
    }
    if (num2 > 1 & flag)
      stringBuilder.Append(')');
    if (stringBuilder.Length == 0)
      stringBuilder.AppendFormat("{0} #{1}", (object) this.imvIDCache.SidecarInstanceName, (object) Math.Abs(documentId));
    return stringBuilder.ToString();
  }

  /// <summary>
  /// Создает или обновляет файл объекта IMViewer, используя путь к файлу исходного документа.
  /// </summary>
  /// <param name="documentPath">Путь к файлу исходного документа</param>
  /// <param name="documentBaseDirectory">Путь к базовому каталогу для вычисления относительного пути к файлу документа</param>
  /// <param name="cadSystem">Объект CAD-системы</param>
  /// <param name="preOpenDocumentsMode">Режим предварительного открытия документа в CAD-системе</param>
  /// <returns>Абсолютный путь к файлу IMViewer</returns>
  /// <exception cref="T:System.ArgumentException">параметр <paramref name="documentPath" /> содержит некорректное значение; параметр <paramref name="documentBaseDirectory" /> содержит некорректное значение</exception>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="cadSystem" /> содержит null; параметр <paramref name="documentPath" /> содержит null; параметр <paramref name="documentBaseDirectory" /> содержит null</exception>
  public string CreateViewerFile(
    string documentPath,
    string documentBaseDirectory,
    CADSystemProxy cadSystem,
    bool preOpenDocumentsMode)
  {
    if (string.IsNullOrEmpty(documentPath))
      throw new ArgumentException("Не задан путь к файлу документа.", nameof (documentPath));
    if (string.IsNullOrEmpty(documentBaseDirectory))
      throw new ArgumentException("Не задан путь к базовому каталогу.", nameof (documentBaseDirectory));
    if (cadSystem == null)
      throw new ArgumentNullException(nameof (cadSystem));
    string converterDirectory = this.RebaseToConverterDirectory(Path.GetDirectoryName(documentPath), documentBaseDirectory);
    (CADDocumentProxy cadDocumentProxy, bool flag) = this.PrepareForCreateViewerFile(documentPath, cadSystem, preOpenDocumentsMode);
    try
    {
      return new IMViewerFileConverterProxy(cadSystem).CreateViewerFile(documentPath, converterDirectory);
    }
    finally
    {
      if (cadDocumentProxy != null & flag)
        cadDocumentProxy.Close();
    }
  }

  private string RebaseToConverterDirectory(string path, string baseDirectoryPath)
  {
    return PathUtils.IsSamePath(path, baseDirectoryPath) ? this.ConverterBaseDirectory : Path.Combine(this.ConverterBaseDirectory, PathUtils.GetRelativePath(path, baseDirectoryPath, RelativePathOptions.ThrowIfNotPossible));
  }

  private (CADDocumentProxy, bool) PrepareForCreateViewerFile(
    string documentFilePath,
    CADSystemProxy cadSystem,
    bool preOpenDocumentsMode)
  {
    if (!preOpenDocumentsMode)
      return ((CADDocumentProxy) null, false);
    int documentOpenStatus = (int) cadSystem.GetDocumentOpenStatus(documentFilePath);
    bool openVisible = documentOpenStatus != 2;
    bool flag = documentOpenStatus == 0;
    return (cadSystem.OpenDocument(documentFilePath, openVisible), flag);
  }
}
