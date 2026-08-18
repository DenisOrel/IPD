// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.ArticleDocumentationService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.Data;
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

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>
/// Реализует сервис для работы с документацией на изделие. Используется при сохранении изменений в конструкторских документах для синхронизации связей типа "Документация на изделие".
/// </summary>
public class ArticleDocumentationService : MechanicalDriverService, IArticleDocumentationService
{
  private IFileVault fileVault;

  /// <summary>Создает объект.</summary>
  /// <param name="driver">Драйвер захвата изменений</param>
  /// <param name="driverContext">Контекст выполняемой операции захвата изменений</param>
  /// <param name="fileVault">Системный сервис файлового хранилища</param>
  /// <exception cref="T:ArgumentNullException">driver or driverContext or fileValue</exception>
  public ArticleDocumentationService(
    MechanicalDriver driver,
    CaptureChangesDriverContext driverContext,
    IFileVault fileVault)
    : base(driver, driverContext)
  {
    this.fileVault = fileVault != null ? fileVault : throw new ArgumentNullException(nameof (fileVault));
  }

  /// <summary>Возвращает системный сервис файлового хранилища.</summary>
  protected IFileVault FileVault
  {
    [DebuggerStepThrough] get => this.fileVault;
  }

  public virtual List<SectionEntity> GetDocuments(SectionEntity articleItem)
  {
    List<SectionEntity> documents = new List<SectionEntity>();
    SectionEntity articleMainDocument = this.Driver.MechanicalOperations.Articles.TryGetArticleMainDocument(articleItem);
    if (articleMainDocument != null)
      documents.Add(articleMainDocument);
    return documents;
  }

  protected SectionEntity TryAddDocument(
    long documentId,
    bool searchInDbOnly,
    List<SectionEntity> documents)
  {
    if (documentId == 0L)
      throw new ArgumentException();
    if (documents == null)
      throw new ArgumentNullException(nameof (documentId));
    if (CollectionUtils.Exists<SectionEntity>((IEnumerable<SectionEntity>) documents, (Predicate<SectionEntity>) (docItem =>
    {
      ObjectSection objectSection = docItem.Sections.Get<ObjectSection>((ObjectSection) null);
      return objectSection != null && objectSection.ObjectId == documentId;
    })))
      return (SectionEntity) null;
    SectionEntity sectionEntity = (SectionEntity) null;
    if (!searchInDbOnly)
      sectionEntity = ObjectSection.FindByObjectId(this.DriverContext.Database, documentId, true);
    if (sectionEntity == null)
      sectionEntity = this.DriverContext.Database.AddReferencedDBObject(documentId);
    documents.Add(sectionEntity);
    return sectionEntity;
  }

  protected SectionEntity TryAddDocument(
    string documentMasterFilePath,
    bool searchInDbOnly,
    List<SectionEntity> documents)
  {
    if (string.IsNullOrEmpty(documentMasterFilePath))
      throw new ArgumentException();
    if (documents == null)
      throw new ArgumentNullException("documentId");
    if (CollectionUtils.Exists<SectionEntity>((IEnumerable<SectionEntity>) documents, (Predicate<SectionEntity>) (docItem =>
    {
      FilesSection filesSection = docItem.Sections.Get<FilesSection>((FilesSection) null);
      return filesSection != null && PathUtils.IsSamePath(filesSection.MasterFile, documentMasterFilePath);
    })))
      return (SectionEntity) null;
    SectionEntity sectionEntity = (SectionEntity) null;
    if (!searchInDbOnly)
      sectionEntity = FilesSection.FindByMasterFile(this.DriverContext.Database, documentMasterFilePath);
    if (sectionEntity == null)
    {
      FileOrigin fileOrigin = this.fileVault.WorkArea.GetFileOrigin(documentMasterFilePath, false);
      if (fileOrigin.OriginType == FileOriginType.WorkFile)
        sectionEntity = this.DriverContext.Database.AddReferencedDBObject(fileOrigin.WorkObject.ObjectId);
    }
    if (sectionEntity == null)
    {
      string relativePath = PathUtils.GetRelativePath(documentMasterFilePath, this.fileVault.WorkArea.AreaPath, RelativePathOptions.ThrowIfNotPossible);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        long idByFileName = ServiceUtils.GetService<IFileNamesService>((object) sessionKeeper.Session, true).GetIDByFileName(relativePath, sessionKeeper.Session.SessionGUID);
        if (idByFileName != -1L)
          sectionEntity = this.DriverContext.Database.AddReferencedDBObject(sessionKeeper.Session.GetObjectByVersionsRule(idByFileName, VersionsRuleSources.GetEditorRule().OwnerId, true).ObjectID);
      }
    }
    if (sectionEntity == null)
      throw new InvalidOperationException($"Не удалось найти документ IPS по его файлу '{documentMasterFilePath}'.");
    documents.Add(sectionEntity);
    return sectionEntity;
  }

  public ValueBag GetRelationAttributes(SectionEntity articleItem, SectionEntity documentItem)
  {
    ValueBag attributes = new ValueBag();
    this.MakeRelationAttributes(articleItem, documentItem, attributes);
    attributes.AcceptChanges();
    return attributes;
  }

  protected virtual void MakeRelationAttributes(
    SectionEntity articleItem,
    SectionEntity documentItem,
    ValueBag attributes)
  {
    if (!this.IsArticleInitialDocument(articleItem, documentItem))
      return;
    this.MakeAttributesForArticleInitialDocument(articleItem, documentItem, attributes);
  }

  protected virtual void MakeAttributesForArticleInitialDocument(
    SectionEntity articleItem,
    SectionEntity documentItem,
    ValueBag attributes)
  {
    IArticleExternalKeysService externalKeysService = this.Driver.TryGetArticleExternalKeysService(documentItem);
    if (externalKeysService != null && externalKeysService.HasExternalKeySupport(articleItem, documentItem))
    {
      string externalKey = externalKeysService.GetExternalKey(articleItem, documentItem);
      if (!string.IsNullOrEmpty(externalKey))
        attributes.AddWithFlag((StringKey) IDCache.Default.ObjectExternalKey.Text, (object) externalKey, NamedFlags.ReadOnly);
    }
    if (this.Driver.TryGetArticleFilesService(articleItem) == null)
      return;
    ArticleFiles articleFiles = articleItem.Sections.Get<ArticleFiles>();
    string str = !string.IsNullOrEmpty(articleFiles.MainArticleFile) ? PathUtils.GetRelativePath(articleFiles.MainArticleFile, this.fileVault.WorkArea.AreaPath, RelativePathOptions.ThrowIfNotPossible) : string.Empty;
    attributes.AddWithFlag((StringKey) IDCache.Default.CADConfigurationFile.Text, (object) str, NamedFlags.ReadOnly);
  }

  /// <summary>
  /// Позволяет проверить, является ли указанный документ исходным документом. Такой документ содержит всю информацию об изделии и используется интегратором
  /// для создания/обновления изделий в базе IPS.
  /// </summary>
  /// <param name="articleItem">Объект изделия</param>
  /// <param name="documentItem">Объект документа</param>
  /// <returns>true, если указанный документ является исходным, иначе - false</returns>
  protected bool IsArticleInitialDocument(SectionEntity articleItem, SectionEntity documentItem)
  {
    if (articleItem == null)
      throw new ArgumentNullException(nameof (articleItem));
    if (documentItem == null)
      throw new ArgumentNullException(nameof (documentItem));
    return this.Driver.MechanicalOperations.Articles.TryGetArticleMainDocument(articleItem) == documentItem;
  }
}
