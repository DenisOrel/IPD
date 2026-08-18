// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.SyncArticleStructureAction
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Data;
using Intermech.Data.EntityDb;
using Intermech.Data.SectionEntities;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data;
using Intermech.Interfaces.Data.Actions;
using Intermech.IO;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Pools;
using Intermech.Text;
using Intermech.Tools.Data;
using Intermech.Tools.Data.Sync;
using Intermech.Tools.DataExchange;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

public sealed class SyncArticleStructureAction : IAction
{
  private readonly MechanicalDriver driver;
  private readonly CaptureChangesDriverContext ctx;
  private readonly SectionEntity projectArticleItem;
  private readonly IArticleStructureService articleStructureService;
  private readonly IFileVault fileVaultService;
  private int relationType;
  private IDBAttributableTypeRef relationAttributeProvider;
  private ObjectSection projectObject;
  private ObjectActionsSection projectActions;
  private List<ArticleStructureOccurence> articleStructure;
  private Dictionary<string, SectionEntity> componentMap;
  private SyncClusters<ArticleStructureOccurence, SyncArticleStructureAction.DBOccurence> compareClusters;
  private ArticleStructureStats stats;
  private List<SyncArticleStructureAction.MissingDraftArticleComponent> missingDraftComponents;
  private SoftInstantiationHelper softInstantiationHelper;
  private bool softInstantiationEnabled;
  private SectionEntity projectDocumentItem;
  private ObjectSection projectDocumentObject;
  private List<long> projectDocumentFixedComponents;

  public SyncArticleStructureAction(
    MechanicalDriver driver,
    CaptureChangesDriverContext ctx,
    SectionEntity projectArticleItem,
    IArticleStructureService articleStructureService,
    IFileVault fileVaultService)
  {
    if (driver == null)
      throw new ArgumentNullException(nameof (driver));
    if (ctx == null)
      throw new ArgumentNullException(nameof (ctx));
    if (projectArticleItem == null)
      throw new ArgumentNullException("projectArticle");
    if (articleStructureService == null)
      throw new ArgumentNullException(nameof (articleStructureService));
    if (fileVaultService == null)
      throw new ArgumentNullException("fileValueService");
    this.driver = driver;
    this.ctx = ctx;
    this.projectArticleItem = projectArticleItem;
    this.articleStructureService = articleStructureService;
    this.fileVaultService = fileVaultService;
    this.missingDraftComponents = new List<SyncArticleStructureAction.MissingDraftArticleComponent>();
  }

  public void SetEmptyArticleStructureStatus()
  {
    if (this.driver.Operations.Db.CanHaveIntegrationErrors(this.projectArticleItem))
    {
      SectionEntity articleInitialDocument = this.driver.MechanicalOperations.Articles.TryGetArticleInitialDocument(this.projectArticleItem);
      DBObjectErrorsBuilder integrationErrorsBuilder = this.driver.Operations.Db.GetIntegrationErrorsBuilder(this.projectArticleItem);
      integrationErrorsBuilder.RemoveByCategory(DBObjectIntegrationStatus.PartialObjectStructureErrorCategory);
      using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(512 /*0x0200*/))
      {
        string uniqueId = "AS:0000";
        StringBuilder stringBuilder = objectPoolScope.Object;
        if (articleInitialDocument != null)
          stringBuilder.AppendFormat("Состав этого изделия еще не был сформирован. Чтобы устранить ошибку, требуется выполнить расширенное сохранение для документа #{0}.", (object) ObjectSection.GetObjectId(articleInitialDocument));
        else
          stringBuilder.Append("Состав этого изделия еще не был сформирован. Чтобы устранить ошибку, требуется выполнить расширенное сохранение для соответствующего конструкторского документа.");
        integrationErrorsBuilder.Add(new DBObjectErrorInfo(uniqueId, DBObjectIntegrationStatus.PartialObjectStructureErrorCategory, stringBuilder.ToString()));
      }
      this.driver.Operations.Db.UpdateIntegrationErrors(this.projectArticleItem, integrationErrorsBuilder);
    }
    if (!this.driver.Operations.Db.CanHaveIntegrationStatus(this.projectArticleItem))
      return;
    this.driver.Operations.Db.UpdatePartialStructureStatus(this.projectArticleItem, true);
  }

  public void Perform()
  {
    using (UIReport.CreateLogicalOperation((object) this.projectArticleItem))
    {
      using (UIReport.CreateLogicalOperation((object) "SyncArticleStructure"))
      {
        try
        {
          this.DoPerform();
        }
        finally
        {
          this.Cleanup();
        }
      }
    }
  }

  private void DoPerform()
  {
    this.Initialize();
    this.articleStructure = this.articleStructureService.ReadArticleStructure(this.projectArticleItem);
    this.componentMap = this.CreateComponentMap();
    this.compareClusters = this.CompareArticleStructures();
    this.stats = new ArticleStructureStats();
    this.SyncNewOccurences();
    this.SyncExistingOccurences();
    this.SyncDeletedOccurences();
    this.UpdateArticleStructureStatus();
    this.articleStructureService.FlushArticleStructureChanges(this.projectArticleItem, this.stats);
  }

  private void Cleanup()
  {
    this.relationType = 0;
    this.relationAttributeProvider = (IDBAttributableTypeRef) null;
    this.projectObject = (ObjectSection) null;
    this.projectActions = (ObjectActionsSection) null;
    this.softInstantiationHelper = (SoftInstantiationHelper) null;
    this.softInstantiationEnabled = false;
    this.articleStructure = (List<ArticleStructureOccurence>) null;
    this.componentMap = (Dictionary<string, SectionEntity>) null;
    this.compareClusters = (SyncClusters<ArticleStructureOccurence, SyncArticleStructureAction.DBOccurence>) null;
    this.stats = (ArticleStructureStats) null;
    this.missingDraftComponents.Clear();
    this.projectDocumentItem = (SectionEntity) null;
    this.projectDocumentObject = (ObjectSection) null;
    this.projectDocumentFixedComponents = (List<long>) null;
  }

  private void Initialize()
  {
    this.relationType = IDCache.Default.ArticleTree.Id;
    this.projectObject = this.projectArticleItem.Sections.Get<ObjectSection>();
    this.projectActions = this.projectArticleItem.Sections.Get<ObjectActionsSection>();
    this.softInstantiationHelper = new SoftInstantiationHelper();
  }

  [Conditional("DEBUG")]
  private void ValidateContext()
  {
    if (!this.projectObject.NewObject && this.projectObject.ObjectId == 0L)
      throw new InvalidOperationException(LocalizationHolder.rm.GetString("Tools.Components_440"));
  }

  private Dictionary<string, SectionEntity> CreateComponentMap()
  {
    Dictionary<string, SectionEntity> componentMap = new Dictionary<string, SectionEntity>(this.articleStructure.Count, (IEqualityComparer<string>) new PathComparer());
    foreach (ArticleStructureOccurence componentOccurence in this.articleStructure)
    {
      SectionEntity articleComponent;
      if (!componentMap.TryGetValue(componentOccurence.ComponentKey, out articleComponent))
      {
        articleComponent = this.articleStructureService.FindArticleComponent(this.projectArticleItem, componentOccurence);
        componentMap.Add(componentOccurence.ComponentKey, articleComponent);
      }
    }
    return componentMap;
  }

  private SyncClusters<ArticleStructureOccurence, SyncArticleStructureAction.DBOccurence> CompareArticleStructures()
  {
    SyncClusters<ArticleStructureOccurence, SyncArticleStructureAction.DBOccurence> syncClusters = new SyncClusters<ArticleStructureOccurence, SyncArticleStructureAction.DBOccurence>(this.articleStructure.Count);
    if (this.projectObject.NewObject)
    {
      foreach (ArticleStructureOccurence structureOccurence in this.articleStructure)
        syncClusters.NewItems.Add(structureOccurence);
    }
    else
    {
      List<SyncArticleStructureAction.DBOccurence> dbStructure = SyncArticleStructureAction.GetDbStructure(this.projectObject.ObjectId);
      foreach (ArticleStructureOccurence structureOccurence in this.articleStructure)
      {
        ArticleStructureOccurence componentOccurence = structureOccurence;
        SectionEntity component = this.componentMap[componentOccurence.ComponentKey];
        if (component != null)
        {
          long componentId = ObjectSection.GetObjectId(component);
          int index1 = dbStructure.FindIndex((Predicate<SyncArticleStructureAction.DBOccurence>) (item =>
          {
            if (!(item.OccurenceGuid == componentOccurence.OccurenceGuid))
              return false;
            return item.ComponentId == componentId || item.ComponentVersions.Contains(componentId);
          }));
          if (index1 >= 0)
          {
            syncClusters.ExistingItems.Add(Tuple.Create<ArticleStructureOccurence, SyncArticleStructureAction.DBOccurence>(componentOccurence, dbStructure[index1]));
            dbStructure.RemoveAt(index1);
            continue;
          }
          int index2 = dbStructure.FindIndex((Predicate<SyncArticleStructureAction.DBOccurence>) (item => item.OccurenceGuid == Guid.Empty && item.ComponentId == componentId));
          if (index2 >= 0)
          {
            syncClusters.ExistingItems.Add(Tuple.Create<ArticleStructureOccurence, SyncArticleStructureAction.DBOccurence>(componentOccurence, dbStructure[index2]));
            dbStructure.RemoveAt(index2);
            continue;
          }
        }
        syncClusters.NewItems.Add(componentOccurence);
      }
      syncClusters.DeletedItems.AddRange((IEnumerable<SyncArticleStructureAction.DBOccurence>) dbStructure.FindAll((Predicate<SyncArticleStructureAction.DBOccurence>) (item => item.BasedOnCAD)));
    }
    return syncClusters;
  }

  private static List<SyncArticleStructureAction.DBOccurence> GetDbStructure(long articleId)
  {
    ConditionStructure conditionStructure1 = new ConditionStructure(IDCache.Default.OccurenceKey.Id, RelationalOperators.NotEmpty, (object) null, LogicalOperators.OR, 0, true);
    ConditionStructure conditionStructure2 = new ConditionStructure(IDCache.Default.BasedOnCADModel.Id, RelationalOperators.Equal, (object) true, LogicalOperators.NONE, 0, true);
    return SyncArticleStructureAction.GetDbStructure(articleId, new ConditionStructure[2]
    {
      conditionStructure1,
      conditionStructure2
    });
  }

  private static List<SyncArticleStructureAction.DBOccurence> GetDbStructure(
    long articleId,
    ConditionStructure[] conds)
  {
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = -1;
    paramSet.Columns = new object[6]
    {
      (object) ObligatoryObjectAttributes.F_PRJ_GUID,
      (object) IDCache.Default.OccurenceKey.Text,
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.F_ID,
      (object) ObligatoryObjectAttributes.F_OBJECT_TYPE,
      (object) IDCache.Default.BasedOnCADModel.Text
    };
    paramSet.ColumnsInfo = new ColumnInfo[6]
    {
      new ColumnInfo((object) ObligatoryObjectAttributes.F_PRJ_GUID, AttributeSourceTypes.Relation, (object) null),
      new ColumnInfo((object) IDCache.Default.OccurenceKey.Text, AttributeSourceTypes.Relation, (object) null),
      new ColumnInfo((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, (object) null),
      new ColumnInfo((object) ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Object, (object) null),
      new ColumnInfo((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, (object) null),
      new ColumnInfo((object) IDCache.Default.BasedOnCADModel.Text, AttributeSourceTypes.Relation, (object) null)
    };
    paramSet.Conditions = conds;
    int[] numArray = new int[2]
    {
      IDCache.Default.AllArticles.Id,
      IDCache.Default.AllMaterials.Id
    };
    List<DataTable> tables = new List<DataTable>(numArray.Length);
    VersionsRulePackage editorRule = VersionsRuleSources.GetEditorRule();
    foreach (int num in numArray)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(IDCache.Default.ArticleTree.Id);
        relationCollection.FiltrationOwnerID = editorRule.OwnerId;
        relationCollection.ObjectTypeID = num;
        tables.Add(relationCollection.ConsistFrom(paramSet, articleId));
      }
    }
    DataTable dataTable = DataTableUtils.Merge((IList<DataTable>) tables);
    List<long> idList = new List<long>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      idList.Add(Convert.ToInt64(row[3]));
    Dictionary<long, HashSet<long>> allObjectVersions = SyncArticleStructureAction.GetAllObjectVersions(idList);
    List<SyncArticleStructureAction.DBOccurence> dbStructure = new List<SyncArticleStructureAction.DBOccurence>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long int64 = Convert.ToInt64(row[3]);
      HashSet<long> objectVersions = allObjectVersions[int64];
      dbStructure.Add(new SyncArticleStructureAction.DBOccurence(new Guid(Convert.ToString(row[0])), Convert.IsDBNull(row[1]) ? Guid.Empty : new Guid(Convert.ToString(row[1])), Convert.ToInt64(row[2]), Convert.ToInt32(row[4]), (ICollection<long>) objectVersions, !Convert.IsDBNull(row[5]) && Convert.ToBoolean(row[5])));
    }
    return dbStructure;
  }

  private static Dictionary<long, HashSet<long>> GetAllObjectVersions(List<long> idList)
  {
    if (idList.Count == 0)
      return new Dictionary<long, HashSet<long>>(0);
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = -1;
    paramSet.Columns = new object[2]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.F_ID
    };
    paramSet.Conditions = new ConditionStructure[1]
    {
      new ConditionStructure(-3, RelationalOperators.In, (object) idList.ToArray(), LogicalOperators.NONE, 0, true)
    };
    DataTable dataTable;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(-1);
      objectCollection.ShowAllModifications = true;
      objectCollection.TrashMode = true;
      dataTable = objectCollection.Select(paramSet);
    }
    Dictionary<long, HashSet<long>> allObjectVersions = new Dictionary<long, HashSet<long>>();
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long int64_1 = Convert.ToInt64(row[0]);
      long int64_2 = Convert.ToInt64(row[1]);
      HashSet<long> longSet;
      if (allObjectVersions.TryGetValue(int64_2, out longSet))
      {
        longSet.Add(int64_1);
      }
      else
      {
        longSet = new HashSet<long>();
        longSet.Add(int64_1);
        allObjectVersions.Add(int64_2, longSet);
      }
    }
    return allObjectVersions;
  }

  private void SyncNewOccurences()
  {
    this.InitializeArticleSoftInstantiation();
    foreach (ArticleStructureOccurence newItem in this.compareClusters.NewItems)
    {
      SectionEntity component = this.componentMap[newItem.ComponentKey];
      if (component == null)
      {
        string componentFilePath = this.articleStructureService.TryGetArticleComponentFile(this.projectArticleItem, newItem);
        if (componentFilePath != null && this.IsDraftArticleComponent(newItem, componentFilePath) && !this.missingDraftComponents.Exists((Predicate<SyncArticleStructureAction.MissingDraftArticleComponent>) (item => PathUtils.IsSamePath(item.ComponentDocumentFile, componentFilePath))))
          this.missingDraftComponents.Add(new SyncArticleStructureAction.MissingDraftArticleComponent(componentFilePath));
        if (UIReport.Enabled)
          UIReport.ReportEvent(string.Format(LocalizationHolder.rm.GetString("SR_563"), (object) newItem.ComponentKey), TraceLevel.Warning);
      }
      else
      {
        SectionEntity relationItem = this.EmitRelationItem(Guid.Empty, newItem);
        this.SyncRelationAttributes(relationItem, component);
        if (this.driver.Operations.Checkout.RequireCheckoutOnRelationModification(this.relationType, this.projectArticleItem, component))
        {
          ObjectActionsSection projectActions = this.projectActions;
          projectActions.RequireCheckout = ((projectActions.RequireCheckout ? 1 : 0) | 1) != 0;
        }
        DBObjectEntityRef dbObjectEntityRef = new DBObjectEntityRef(component);
        CreateRelationAction createRelationAction = new CreateRelationAction((IDBObjectRef) new DBObjectEntityRef(this.projectArticleItem), (IDBObjectRef) dbObjectEntityRef, this.relationType);
        this.projectActions.RelationActions.ServerActions.Add((IAction) createRelationAction);
        this.projectActions.RelationActions.ServerActions.Add((IAction) new CaptureRelationGuidAction(relationItem, createRelationAction));
        if (this.IsArticleComponentMustBeSoftInstantiated(component))
          this.projectActions.RelationActions.ServerActions.Add((IAction) new FixRelationAction((IDBRelationRef) createRelationAction, (IDBObjectRef) dbObjectEntityRef));
        this.projectActions.RelationActions.ClientActions.Add((IAction) new FireRelationCreatedAction((IDBRelationRef) createRelationAction, this.ctx.UINotifications));
        List<ValueRecord> changedItems = relationItem.Sections.Get<AttributesSection>().DatabaseSet.GetChangedItems();
        if (changedItems.Count > 0)
          this.projectActions.RelationActions.ServerActions.Add((IAction) new WriteRelationAttributesAction((IDBRelationRef) createRelationAction, DBAttributeHelper.ToAttributeValues((IList<ValueRecord>) changedItems)));
        ++this.stats.CreatedRelations;
      }
    }
  }

  private void SyncExistingOccurences()
  {
    foreach (Tuple<ArticleStructureOccurence, SyncArticleStructureAction.DBOccurence> existingItem in this.compareClusters.ExistingItems)
    {
      ArticleStructureOccurence componentOccurence = existingItem.Item1;
      SyncArticleStructureAction.DBOccurence partItem = existingItem.Item2;
      SectionEntity component = this.componentMap[componentOccurence.ComponentKey];
      if (component == null)
      {
        string componentFilePath = this.articleStructureService.TryGetArticleComponentFile(this.projectArticleItem, componentOccurence);
        if (componentFilePath != null && this.IsDraftArticleComponent(componentOccurence, componentFilePath) && !this.missingDraftComponents.Exists((Predicate<SyncArticleStructureAction.MissingDraftArticleComponent>) (item => PathUtils.IsSamePath(item.ComponentDocumentFile, componentFilePath))))
          this.missingDraftComponents.Add(new SyncArticleStructureAction.MissingDraftArticleComponent(componentFilePath));
        if (UIReport.Enabled)
          UIReport.ReportEvent(string.Format(LocalizationHolder.rm.GetString("SR_564"), (object) componentOccurence.ComponentKey), TraceLevel.Warning);
      }
      else
      {
        SectionEntity sectionEntity = this.EmitRelationItem(partItem.RelationGuid, componentOccurence);
        this.SyncRelationAttributes(sectionEntity, component);
        List<ValueRecord> changedItems = sectionEntity.Sections.Get<AttributesSection>().DatabaseSet.GetChangedItems();
        if (changedItems.Count > 0)
        {
          foreach (ValueRecord valueRecord in changedItems)
          {
            if (this.driver.Operations.Checkout.RequireCheckoutOnRelationAttribute(this.relationType, this.projectArticleItem, (IDBObjectRef) partItem, valueRecord.Key))
            {
              ObjectActionsSection projectActions = this.projectActions;
              projectActions.RequireCheckout = ((projectActions.RequireCheckout ? 1 : 0) | 1) != 0;
              break;
            }
          }
          IDBRelationRef relationRef = (IDBRelationRef) new DBRelationEntityRef(sectionEntity);
          DBObjectEntityRef dbObjectEntityRef = new DBObjectEntityRef(component);
          this.projectActions.RelationActions.ServerActions.Add((IAction) new WriteRelationAttributesAction(relationRef, DBAttributeHelper.ToAttributeValues((IList<ValueRecord>) changedItems)));
          this.projectActions.RelationActions.ClientActions.Add((IAction) new FireRelationModifiedAction(relationRef, this.ctx.UINotifications));
          ++this.stats.ChangedRelations;
        }
      }
    }
  }

  private void SyncDeletedOccurences()
  {
    foreach (SyncArticleStructureAction.DBOccurence deletedItem in this.compareClusters.DeletedItems)
    {
      if (this.driver.Operations.Checkout.RequireCheckoutOnRelationModification(this.relationType, this.projectArticleItem, (IDBObjectRef) deletedItem))
      {
        ObjectActionsSection projectActions = this.projectActions;
        projectActions.RequireCheckout = ((projectActions.RequireCheckout ? 1 : 0) | 1) != 0;
      }
      DeleteRelationAction relationRef = new DeleteRelationAction((IDBObjectRef) new DBObjectEntityRef(this.projectArticleItem), deletedItem.RelationGuid, this.relationType);
      this.projectActions.RelationActions.ServerActions.Add((IAction) relationRef);
      this.projectActions.RelationActions.ClientActions.Add((IAction) new FireRelationRemovedAction((IDBRelationRef) relationRef, this.ctx.UINotifications));
      ++this.stats.DeletedRelations;
    }
  }

  private bool IsDraftArticleComponent(
    ArticleStructureOccurence componentOccurence,
    string componentDocumentPath)
  {
    return DraftDocumentSection.FindByExternalFilePath(this.ctx.Database, componentDocumentPath) != null;
  }

  private void UpdateArticleStructureStatus()
  {
    bool partialStructureStatus = this.missingDraftComponents.Count != 0;
    if (this.driver.Operations.Db.CanHaveIntegrationErrors(this.projectArticleItem))
    {
      SectionEntity articleInitialDocument = this.driver.MechanicalOperations.Articles.TryGetArticleInitialDocument(this.projectArticleItem);
      DBObjectErrorsBuilder integrationErrorsBuilder = this.driver.Operations.Db.GetIntegrationErrorsBuilder(this.projectArticleItem);
      integrationErrorsBuilder.RemoveByCategory(DBObjectIntegrationStatus.PartialObjectStructureErrorCategory);
      int num = 1;
      foreach (SyncArticleStructureAction.MissingDraftArticleComponent missingDraftComponent in this.missingDraftComponents)
      {
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(512 /*0x0200*/))
        {
          string uniqueId = this.PartialStructureErrorId(num++);
          StringBuilder stringBuilder = objectPoolScope.Object;
          stringBuilder.AppendFormat("В состав этого изделия не удалось добавить проектную связь, так как входящее изделие описано в черновике документа '{0}', который еще не импортирован в IPS.", (object) this.TryGetRelativeComponentFilePath(missingDraftComponent.ComponentDocumentFile));
          stringBuilder.Append(' ');
          if (articleInitialDocument != null)
            stringBuilder.AppendFormat("Чтобы исправить ошибку, преобразуйте черновик документа в полноценный документ, а затем выполните расширенное сохранение для документа #{0}.", (object) ObjectSection.GetObjectId(articleInitialDocument));
          else
            stringBuilder.Append("Чтобы исправить ошибку, преобразуйте черновик документа в полноценный документ, а затем выполните расширенное сохранение для соответствующего конструкторского документа.");
          integrationErrorsBuilder.Add(new DBObjectErrorInfo(uniqueId, DBObjectIntegrationStatus.PartialObjectStructureErrorCategory, stringBuilder.ToString()));
        }
      }
      this.driver.Operations.Db.UpdateIntegrationErrors(this.projectArticleItem, integrationErrorsBuilder);
    }
    if (!this.driver.Operations.Db.CanHaveIntegrationStatus(this.projectArticleItem))
      return;
    this.driver.Operations.Db.UpdatePartialStructureStatus(this.projectArticleItem, partialStructureStatus);
  }

  private string TryGetRelativeComponentFilePath(string componentDocumentFile)
  {
    return PathUtils.GetRelativePath(componentDocumentFile, this.fileVaultService.WorkArea.AreaPath, RelativePathOptions.None) ?? componentDocumentFile;
  }

  private SectionEntity EmitRelationItem(
    Guid relationGuid,
    ArticleStructureOccurence componentOccurence)
  {
    RelationSection sectionObject = new RelationSection();
    sectionObject.ProjectItem = this.projectArticleItem;
    sectionObject.RelationGuid = relationGuid;
    sectionObject.NewRelation = relationGuid == Guid.Empty;
    SectionEntity relationItem = new SectionEntity();
    relationItem.Sections.Set((object) sectionObject);
    relationItem.Sections.Set((object) new AttributesSection());
    relationItem.Sections.CopyFrom((IEnumerable<KeyValuePair<Type, object>>) componentOccurence.Sections);
    this.ReadAppRelationAttributes(relationItem, componentOccurence);
    this.ReadDbAttributes(relationItem);
    this.ctx.Database.Add((IEntity) relationItem);
    return relationItem;
  }

  private void ReadAppRelationAttributes(
    SectionEntity relationItem,
    ArticleStructureOccurence componentOccurence)
  {
    ValueBag workingSet = relationItem.Sections.Get<AttributesSection>().WorkingSet;
    workingSet.Add((StringKey) IDCache.Default.BasedOnCADModel.Text, (object) true);
    workingSet.Add((StringKey) IDCache.Default.OccurenceKey.Text, (object) componentOccurence.OccurenceGuid);
    foreach (ValueRecord attribute in componentOccurence.Attributes)
    {
      if (!workingSet.Exists(attribute.Key))
        workingSet.Import(attribute);
    }
    workingSet.SetFlagForAll(NamedFlags.ReadOnly);
    workingSet.AcceptChanges();
  }

  private void ReadDbAttributes(SectionEntity relationItem)
  {
    if (this.relationAttributeProvider == null)
      this.relationAttributeProvider = (IDBAttributableTypeRef) new DirectRelationAttributesRef(this.relationType);
    this.driver.Operations.Db.FetchRelationAttributes(relationItem, this.relationAttributeProvider);
  }

  private void SyncRelationAttributes(SectionEntity relationItem, SectionEntity componentItem)
  {
    AttributesSection attributesSection = relationItem.Sections.Get<AttributesSection>();
    AppToDBAttributeSyncTask attributeSyncTask = new AppToDBAttributeSyncTask();
    attributeSyncTask.EntityDisplayName = string.Format(LocalizationHolder.rm.GetString("Attribute.Tools.Components_34"), (object) IDCache.Default.ArticleTree.Text, (object) DisplaySection.GetQualifiedName(componentItem));
    attributeSyncTask.SetApplicationAttributes(attributesSection.WorkingSet, false);
    attributeSyncTask.SetDatabaseAttributes(attributesSection.DatabaseSet, this.relationAttributeProvider);
    attributeSyncTask.AddAllAttributesToSync(false);
    attributeSyncTask.RunChecked();
  }

  private void InitializeArticleSoftInstantiation()
  {
    this.softInstantiationEnabled = false;
    this.projectDocumentItem = this.driver.MechanicalOperations.Articles.TryGetArticleMainDocument(this.projectArticleItem);
    if (this.projectDocumentItem == null)
      return;
    this.projectDocumentObject = this.projectDocumentItem.Sections.Get<ObjectSection>();
    this.softInstantiationEnabled = !this.softInstantiationHelper.IsAutomaticInstantiationEnabled() && this.softInstantiationHelper.IsAllowedForSomePart(this.projectObject.ObjectType, this.relationType) && this.softInstantiationHelper.IsAllowedForSomePart(this.projectDocumentObject.ObjectType, IDCache.Default.DocumentTree.Id);
    if (!this.softInstantiationEnabled)
      return;
    this.projectDocumentFixedComponents = this.ReadProjectDocumentFixedComponents();
  }

  private List<long> ReadProjectDocumentFixedComponents()
  {
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = -1;
    paramSet.Columns = new object[2]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) IDCache.Default.FixedRelation.Id
    };
    paramSet.Conditions = new ConditionStructure[3]
    {
      new ConditionStructure(IDCache.Default.FixedRelation.Id, RelationalOperators.NotEmpty, (object) null, LogicalOperators.AND, 0, true),
      new ConditionStructure(IDCache.Default.FixedRelationMode.Id, RelationalOperators.NotExistsOrEmpty, (object) null, LogicalOperators.OR, 1, true),
      new ConditionStructure(IDCache.Default.FixedRelationMode.Id, RelationalOperators.Equal, (object) RevisionInstantiationMode.Default, LogicalOperators.NONE, -1, true)
    };
    DataTable dataTable;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(IDCache.Default.DocumentTree.Id);
      relationCollection.ObjectTypeID = IDCache.Default.AllDocuments.Id;
      dataTable = relationCollection.ConsistFrom(paramSet, this.projectDocumentObject.ObjectId);
    }
    List<long> longList = new List<long>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long int64_1 = Convert.ToInt64(row[0]);
      long int64_2 = Convert.ToInt64(row[1]);
      if (Math.Abs(int64_1) == Math.Abs(int64_2))
        longList.Add(int64_1);
    }
    if (longList.Count != 0)
      longList.Sort();
    return longList;
  }

  private bool IsArticleComponentMustBeSoftInstantiated(SectionEntity componentItem)
  {
    if (!this.softInstantiationEnabled || this.projectDocumentFixedComponents.Count == 0)
      return false;
    SectionEntity articleMainDocument = this.driver.MechanicalOperations.Articles.TryGetArticleMainDocument(componentItem);
    return articleMainDocument != null && this.softInstantiationHelper.IsAllowed(this.projectObject.ObjectType, ObjectSection.GetObjectType(componentItem), this.relationType) && this.softInstantiationHelper.IsAllowed(this.projectDocumentObject.ObjectType, ObjectSection.GetObjectType(articleMainDocument), IDCache.Default.DocumentTree.Id) && this.projectDocumentFixedComponents.BinarySearch(ObjectSection.GetObjectId(articleMainDocument)) >= 0;
  }

  private string PartialStructureErrorId(int errorIndex) => $"AS:{errorIndex:D4}";

  private sealed class DBOccurence : IDBObjectRef, IDBTypedEntityRef
  {
    public readonly Guid RelationGuid;
    public readonly Guid OccurenceGuid;
    public readonly long ComponentId;
    public readonly int ComponentType;
    public readonly ICollection<long> ComponentVersions;
    public readonly bool BasedOnCAD;

    public DBOccurence(
      Guid relationGuid,
      Guid occurenceGuid,
      long objectId,
      int objectType,
      ICollection<long> objectVersions,
      bool basedOnCAD)
    {
      this.RelationGuid = relationGuid;
      this.OccurenceGuid = occurenceGuid;
      this.ComponentId = objectId;
      this.ComponentType = objectType;
      this.ComponentVersions = objectVersions;
      this.BasedOnCAD = basedOnCAD;
    }

    long IDBObjectRef.GetObjectId() => this.ComponentId;

    int IDBTypedEntityRef.GetEntityType() => this.ComponentType;
  }

  private sealed class MissingDraftArticleComponent : 
    IEquatable<SyncArticleStructureAction.MissingDraftArticleComponent>
  {
    public MissingDraftArticleComponent(string componentDocumentFile)
    {
      this.ComponentDocumentFile = componentDocumentFile;
    }

    public string ComponentDocumentFile { get; private set; }

    public bool Equals(
      SyncArticleStructureAction.MissingDraftArticleComponent other)
    {
      return other != null && PathUtils.IsSamePath(this.ComponentDocumentFile, other.ComponentDocumentFile);
    }

    public override bool Equals(object obj)
    {
      return !(obj is SyncArticleStructureAction.MissingDraftArticleComponent other) ? base.Equals(obj) : this.Equals(other);
    }

    public override int GetHashCode() => this.ComponentDocumentFile.GetHashCode();
  }
}
