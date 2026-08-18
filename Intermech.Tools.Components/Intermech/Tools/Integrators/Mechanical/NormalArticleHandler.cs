// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.NormalArticleHandler
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
using Intermech.Tools.Data;
using Intermech.Tools.Data.Sync;
using Intermech.Tools.DataExchange;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

public class NormalArticleHandler : ArticleHandlerBase
{
  private IFileVault fileVaultService;
  private bool enableGroupIdProcessing;
  private ObjectSection articleObj;
  private AttributesSection articleAttrs;
  private IDBAttributableTypeRef articleAttrsRef;

  public NormalArticleHandler(
    MechanicalDriver driver,
    CaptureChangesDriverContext ctx,
    SectionEntity articleItem)
    : base(driver, ctx, articleItem)
  {
    this.fileVaultService = ServiceUtils.GetService<IFileVault>((object) ApplicationServices.Container, true);
    this.EnableGroupIdProcessing = true;
  }

  /// <summary>
  /// Включает и выключает заполнение атрибута "Идентификатор группового изделия", используемого для хранения в базе данных информации об исполнениях изделия.
  /// По умолчанию, значение свойства установлено в true.
  /// </summary>
  /// <remarks>
  /// Обработка идентификатора группового изделия может быть отключена в случаях, когда конструкторский документ не может служить
  /// источником информации об исполнениях изделия.
  /// </remarks>
  public bool EnableGroupIdProcessing
  {
    [DebuggerStepThrough] get => this.enableGroupIdProcessing;
    [DebuggerStepThrough] set => this.enableGroupIdProcessing = value;
  }

  protected override IEnumerable<CooperativeState> Coroutine()
  {
    this.Initialize();
    yield return this.Call(new Func<IEnumerable<CooperativeState>>(this.CorrectExternalKeys));
    this.BindToDBObject();
    this.EnsureDBObjectExists();
    this.ReadDBObjectData();
    yield return this.Call(new Func<IEnumerable<CooperativeState>>(this.ProcessAttributes));
    this.CollectFiles();
    yield return this.Call(new Func<IEnumerable<CooperativeState>>(this.ProcessRelations));
    this.DeleteUnwantedAttributes();
    this.MechanicalDriver.Operations.Db.EmitObjectAttributesServerActions(this.articleItem);
    yield return this.Call(new Func<IEnumerable<CooperativeState>>(this.WriteChangesToDisk));
    yield return this.Wait((IWaitObject) this.MechanicalDriver.SchedulerStages.UIStage);
    this.MechanicalDriver.Operations.Db.EmitUIActions(this.ctx, this.articleItem);
    EventHandler<ArticleEntityEventArgs> finished = this.Finished;
    if (finished != null)
      finished((object) this, new ArticleEntityEventArgs(this.articleItem));
  }

  protected override object GetUIReportOperationId() => (object) this.articleItem;

  private void Initialize()
  {
    this.articleObj = this.articleItem.Sections.Get<ObjectSection>();
    this.articleAttrs = this.articleItem.Sections.Get<AttributesSection>();
  }

  private IEnumerable<CooperativeState> CorrectExternalKeys()
  {
    if (this.docLinkType == ArticleInitialDocumentType.Normal)
    {
      IArticleExternalKeysService externalKeysService = this.MechanicalDriver.TryGetArticleExternalKeysService(this.docItem);
      if (externalKeysService != null && externalKeysService.HasExternalKeySupport(this.articleItem, this.docItem))
      {
        CorrectExternalKeysAction externalKeysAction = CorrectExternalKeysAction.GetOrCreate(this.ctx.Scheduler, this.docItem, externalKeysService);
        externalKeysAction.RegisterArticle(this.articleItem);
        yield return this.Wait(externalKeysAction.Complete);
      }
    }
  }

  private void BindToDBObject()
  {
    IArticleLocatorService articleLocatorService = this.MechanicalDriver.TryGetArticleLocatorService(this.articleItem);
    if (articleLocatorService == null)
      return;
    ArticleBinder.BindArticle(this.ctx, this.articleItem, articleLocatorService.CreateNormalArticleLocator(this.articleItem), false);
  }

  private void EnsureDBObjectExists()
  {
    if (!this.articleObj.NewObject)
      return;
    this.CreateNewArticle();
  }

  private void CreateNewArticle()
  {
    if (this.articleObj.ObjectType == -1)
      this.articleObj.ObjectType = (this.MechanicalDriver.TryGetArticleTypesService(this.articleItem) ?? throw new InvalidOperationException($"Требуется сервис типа '{typeof (IArticleTypesService)}' для определения типа нового изделия '{DisplaySection.GetQualifiedName(this.articleItem)}'.")).DetectArticleType(this.articleItem).Id;
    this.MechanicalDriver.Operations.Db.CreateBlankObject(this.ctx, this.articleItem);
  }

  private void ReadDBObjectData()
  {
    this.articleAttrsRef = (IDBAttributableTypeRef) new DirectObjectAttributesRef(this.articleObj.ObjectType);
    this.MechanicalDriver.Operations.Db.FetchObjectAttributes(this.articleItem, this.articleAttrsRef);
  }

  private IEnumerable<CooperativeState> ProcessAttributes()
  {
    this.OnBeforeProcessAttributes();
    this.PreserveAttributeScavengery();
    this.CorrectAttributes();
    if (this.docLinkType != ArticleInitialDocumentType.None)
    {
      RandomizeIdentityAction randomizeIdentityAction = RandomizeIdentityAction.GetOrCreate(this.ctx.Scheduler, this.docItem);
      randomizeIdentityAction.RegisterArticle(this.articleItem);
      yield return this.Wait(randomizeIdentityAction.Complete);
    }
    this.TransferAttributes();
    this.PostCorrectAttributes();
    if (this.docLinkType == ArticleInitialDocumentType.Normal && this.EnableGroupIdProcessing)
    {
      SetGroupIdAction setGroupIdAction = SetGroupIdAction.GetOrCreate(this.ctx.Scheduler, this.docItem);
      setGroupIdAction.RegisterArticle(this.articleItem);
      yield return this.Wait(setGroupIdAction.Complete);
    }
    this.OnAfterProcessAttributes();
  }

  protected virtual void OnBeforeProcessAttributes()
  {
  }

  protected virtual void OnAfterProcessAttributes()
  {
  }

  private void PreserveAttributeScavengery()
  {
    List<StringKey> attributeKeys = new List<StringKey>(this.articleAttrs.WorkingSet.Count);
    foreach (ValueRecord working in this.articleAttrs.WorkingSet)
    {
      if (working.DataType == typeof (string))
        attributeKeys.Add(working.Key);
    }
    if (attributeKeys.Count <= 0)
      return;
    this.articleApiService.EncodeArticleAttributes(this.articleItem, (ICollection<StringKey>) attributeKeys, this.articleAttrs.WorkingSet, this.articleAttrs.EmbeddedSet);
  }

  private void CorrectAttributes()
  {
    foreach (IAction correctAttributeAction in (IEnumerable<IAction>) this.EmitCorrectAttributeActions())
      correctAttributeAction.Perform();
    this.MechanicalDriver.TryGetArticleAttributesProcessingService(this.articleItem)?.PreprocessAttributes(this.articleItem, this.articleAttrs.WorkingSet, this.articleAttrs.DatabaseSet);
  }

  private ICollection<IAction> EmitCorrectAttributeActions()
  {
    ICollection<IAction> actions = (ICollection<IAction>) new LinkedList<IAction>();
    if (this.docLinkType == ArticleInitialDocumentType.Normal)
      actions.Add((IAction) new FillEmptyArticleIdentityHandler(this.MechanicalDriver, this.articleItem, this.docItem));
    IArticleTypesService articleTypesService = this.MechanicalDriver.TryGetArticleTypesService(this.articleItem);
    if (articleTypesService != null)
    {
      string typeAttributeName = articleTypesService.GetArticleTypeAttributeName(this.articleItem);
      if (!string.IsNullOrEmpty(typeAttributeName))
        actions.Add((IAction) new FillObjectTypeAttributeHandler(this.articleItem, typeAttributeName));
    }
    actions.Add((IAction) new FillEmptyMassFromPhysicalPropsHandler(this.MechanicalDriver, this.articleItem, this.MechanicalDriver.RecalculateMass));
    return actions;
  }

  /// <summary>
  /// Выполняет перенос атрибутов из файла в объект изделия.
  /// </summary>
  private void TransferAttributes()
  {
    ICollection<StringKey> transferableAttributes = this.GetTransferableAttributes();
    if (transferableAttributes.Count == 0)
      return;
    this.TransferAttributes(transferableAttributes);
  }

  /// <summary>
  /// Выполняет перенос атрибутов из файла в объект изделия.
  /// </summary>
  /// <param name="attributes">Список ключей атрибутов для переноса</param>
  private void TransferAttributes(ICollection<StringKey> attributes)
  {
    if (attributes == null)
      throw new ArgumentNullException(nameof (attributes));
    AppToDBAttributeSyncTask attributeSyncTask = new AppToDBAttributeSyncTask();
    attributeSyncTask.EntityDisplayName = DisplaySection.GetQualifiedName(this.articleItem);
    attributeSyncTask.EntityId = this.articleObj.ObjectId;
    attributeSyncTask.SetApplicationAttributes(this.articleAttrs.WorkingSet, this.articleAttrs.EmbeddedSet.IsOpenMetadata);
    attributeSyncTask.SetDatabaseAttributes(this.articleAttrs.DatabaseSet, this.articleAttrsRef);
    attributeSyncTask.OnDetectAttributeAction += new EventHandler<DetectAttributeSyncActionArgs>(this.OnDetectTransferAttributeAction);
    foreach (StringKey attribute in (IEnumerable<StringKey>) attributes)
      attributeSyncTask.Attributes.Add(new AttributeSyncUnit(attribute, this.IsTransferRequired(attribute)));
    attributeSyncTask.RunChecked();
  }

  private void OnDetectTransferAttributeAction(object sender, DetectAttributeSyncActionArgs e)
  {
    if (e.Action != null)
      return;
    ValueRecord valueRecord = e.TaskData.SourceTable.Find(e.Attribute.Key);
    if (valueRecord != null && valueRecord.Flags[MechanicalNamedFlags.TableDrivenValue])
    {
      e.Action = (AttributeSyncAction) SkipAttributeSyncAction.Instance;
    }
    else
    {
      if (!(e.Attribute.Key == (StringKey) IDCache.Default.Material.Text) && !(e.Attribute.Key == (StringKey) IDCache.Default.MaterialReplacement1.Text) && !(e.Attribute.Key == (StringKey) IDCache.Default.MaterialReplacement2.Text))
        return;
      e.Action = (AttributeSyncAction) NormalAttributeSyncAction.Instance;
    }
  }

  /// <summary>
  /// Возвращает список ключей атрибутов, значения которых должны быть перенесены из файла в объект изделия IPS.
  /// Как правило, этот список задается в настройках интегратора.
  /// </summary>
  /// <returns>Список ключей атрибутов</returns>
  private ICollection<StringKey> GetTransferableAttributes()
  {
    OrderedList<StringKey> collection = new OrderedList<StringKey>(32 /*0x20*/);
    collection.AddRange<StringKey>((IEnumerable<StringKey>) this.articleApiService.GetArticleSyncAttributes(this.articleItem));
    return (ICollection<StringKey>) collection;
  }

  /// <summary>
  /// Возвращает true, если атрибут обязательно должен быть перенесен из файла в объект изделия. Если это не удается сделать,
  /// то будет сброшено исключение и вся операция будет прервана. Ошибки переноса остальных атрибутов игнорируются с занесением информации о
  /// сбое в протокол выполнения.
  /// </summary>
  /// <param name="attributeKey">Ключ атрибута</param>
  /// <returns>Признак, что ошибки в процессе переноса этого атрибута из файла в объект изделия недопустимы</returns>
  private bool IsTransferRequired(StringKey attributeKey)
  {
    return attributeKey == (StringKey) IDCache.Default.Designation.Text || attributeKey == (StringKey) IDCache.Default.OKPCode.Text || attributeKey == (StringKey) IDCache.Default.Name.Text;
  }

  private void PostCorrectAttributes()
  {
    this.MechanicalDriver.TryGetArticleAttributesProcessingService(this.articleItem)?.PostprocessAttributes(this.articleItem, this.articleAttrs.WorkingSet, this.articleAttrs.DatabaseSet);
  }

  protected virtual void DeleteUnwantedAttributes()
  {
    this.MechanicalDriver.Operations.Db.RemoveIntegrationStatusIfEmpty(this.articleItem);
    this.MechanicalDriver.Operations.Db.RemoveIntegrationErrorsIfEmpty(this.articleItem);
  }

  private void CollectFiles() => this.AttachConfigurationFile();

  private void AttachConfigurationFile()
  {
    if (this.docLinkType != ArticleInitialDocumentType.Normal)
      return;
    IArticleFilesService articleFilesService = this.MechanicalDriver.TryGetArticleFilesService(this.articleItem);
    if (articleFilesService == null)
      return;
    string articleMainFile = articleFilesService.FindArticleMainFile(this.articleItem);
    if (string.IsNullOrEmpty(articleMainFile) || !this.docItem.Sections.Get<FilesSection>().Satellites.Contains(articleMainFile))
      return;
    this.articleItem.Sections.Get<ArticleFiles>().MainArticleFile = articleMainFile;
  }

  private IEnumerable<CooperativeState> ProcessRelations()
  {
    yield return this.Wait((IWaitObject) this.MechanicalDriver.SchedulerStages.RelationsStage);
    IArticleStructureService structureService = this.MechanicalDriver.TryGetArticleStructureService(this.articleItem);
    if (structureService != null && structureService.IsProjectArticle(this.articleItem))
    {
      bool flag = true;
      if (this.docLinkType == ArticleInitialDocumentType.Normal)
        flag = this.MechanicalDriver.Operations.Documents.GetDependenciesProcessingFlag(this.docItem);
      SyncArticleStructureAction articleStructureAction = new SyncArticleStructureAction(this.MechanicalDriver, this.ctx, this.articleItem, structureService, this.fileVaultService);
      if (flag)
        articleStructureAction.Perform();
      else if (this.articleObj.NewObject)
        articleStructureAction.SetEmptyArticleStructureStatus();
    }
    IArticleDocumentationService documentationService = this.MechanicalDriver.TryGetArticleDocumentationService(this.articleItem);
    if (documentationService != null)
      new SyncArticleDocumentationAction((DocumentCaptureChangesDriver) this.MechanicalDriver, this.ctx, documentationService, this.articleItem).Perform();
  }

  private IEnumerable<CooperativeState> WriteChangesToDisk()
  {
    yield return this.Wait((IWaitObject) this.MechanicalDriver.SchedulerStages.DiskWritesStage);
    this.EncodeChangedWorkingAttributes();
    this.WriteChangedFileProperties();
  }

  private void EncodeChangedWorkingAttributes()
  {
    if (!this.articleAttrs.WorkingSet.HasChanges)
      return;
    List<StringKey> changedItemsKeys = this.articleAttrs.WorkingSet.GetChangedItemsKeys();
    CollectionUtils.RemoveAll<StringKey>((IList<StringKey>) changedItemsKeys, (Predicate<StringKey>) (key =>
    {
      ValueRecord valueRecord = this.articleAttrs.WorkingSet.Find(key);
      return valueRecord != null && valueRecord.Flags[MechanicalNamedFlags.TableDrivenValue];
    }));
    if (changedItemsKeys.Count == 0)
      return;
    this.articleApiService.EncodeArticleAttributes(this.articleItem, (ICollection<StringKey>) changedItemsKeys, this.articleAttrs.WorkingSet, this.articleAttrs.EmbeddedSet);
  }

  /// <summary>Событие завершения выполнения обработчика</summary>
  public event EventHandler<ArticleEntityEventArgs> Finished;
}
