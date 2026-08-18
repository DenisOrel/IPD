// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.SyncDocumentStructureAction
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.ControlFlow;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data;
using Intermech.Interfaces.Data.Actions;
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
using System.Text;

#nullable disable
namespace Intermech.Tools.Integrators;

public sealed class SyncDocumentStructureAction : IAction
{
  private readonly DocumentCaptureChangesDriver driver;
  private readonly CaptureChangesDriverContext ctx;
  private readonly SectionEntity projectItem;
  private int? documentTypeFilter;
  private bool useFixedRelations;
  private FilesSection projectFiles;
  private ObjectActionsSection projectActions;
  private int relationType;
  private string relationName;
  private DirectRelationAttributesRef relationAttributeProvider;
  private SyncClusters<DBObjectEntityRef, SyncDocumentStructureAction.DBPartInfo> syncClusters;

  public SyncDocumentStructureAction(
    DocumentCaptureChangesDriver driver,
    CaptureChangesDriverContext ctx,
    SectionEntity projectItem)
  {
    if (driver == null)
      throw new ArgumentNullException(nameof (driver));
    if (ctx == null)
      throw new ArgumentNullException(nameof (ctx));
    if (projectItem == null)
      throw new ArgumentNullException(nameof (projectItem));
    this.driver = driver;
    this.ctx = ctx;
    this.projectItem = projectItem;
  }

  /// <summary>
  /// Включает и выключает фильтр по типу синхронизируемых документов при запросах в базу данных.
  /// По умолчанию фильтр выключен.
  /// </summary>
  public int? DocumentTypeFilter
  {
    get => this.documentTypeFilter;
    set => this.documentTypeFilter = value;
  }

  /// <summary>
  /// Включает и выключает режим создания между синхронизируемыми документами связей с жесткой конкретизацией.
  /// По умолчанию режим выключен.
  /// </summary>
  public bool UseFixedRelations
  {
    get => this.useFixedRelations;
    set => this.useFixedRelations = value;
  }

  public void SetEmptyDocumentStructureStatus()
  {
    if (this.driver.Operations.Db.CanHaveIntegrationErrors(this.projectItem))
    {
      DBObjectErrorsBuilder integrationErrorsBuilder = this.driver.Operations.Db.GetIntegrationErrorsBuilder(this.projectItem);
      integrationErrorsBuilder.RemoveByCategory(DBObjectIntegrationStatus.PartialObjectStructureErrorCategory);
      using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(512 /*0x0200*/))
      {
        string uniqueId = this.PartialStructureErrorId(0);
        StringBuilder stringBuilder = objectPoolScope.Object;
        stringBuilder.Append("Состав этого объекта IPS еще не был сформирован. Чтобы устранить ошибку, требуется выполнить сохранение изменений для этого объекта IPS.");
        integrationErrorsBuilder.Add(new DBObjectErrorInfo(uniqueId, DBObjectIntegrationStatus.PartialObjectStructureErrorCategory, stringBuilder.ToString()));
      }
      this.driver.Operations.Db.UpdateIntegrationErrors(this.projectItem, integrationErrorsBuilder);
    }
    if (!this.driver.Operations.Db.CanHaveIntegrationStatus(this.projectItem))
      return;
    this.driver.Operations.Db.UpdatePartialStructureStatus(this.projectItem, true);
  }

  public void Perform()
  {
    using (UIReport.CreateLogicalOperation((object) this.projectItem))
    {
      using (UIReport.CreateLogicalOperation((object) "SyncDocumentStructure"))
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

  private void Cleanup()
  {
    this.projectFiles = (FilesSection) null;
    this.projectActions = (ObjectActionsSection) null;
    this.relationType = -1;
    this.relationName = (string) null;
    this.relationAttributeProvider = (DirectRelationAttributesRef) null;
    this.syncClusters = (SyncClusters<DBObjectEntityRef, SyncDocumentStructureAction.DBPartInfo>) null;
    this.relationAttributeProvider = (DirectRelationAttributesRef) null;
  }

  private void DoPerform()
  {
    this.Initialize();
    this.syncClusters = this.CompareDocumentStructures();
    this.SyncNewRelations();
    this.SyncExistingRelations();
    this.SyncDeletedRelations();
    this.UpdateDocumentStructureStatus();
  }

  private void Initialize()
  {
    this.projectFiles = this.projectItem.Sections.Get<FilesSection>();
    this.projectActions = this.projectItem.Sections.Get<ObjectActionsSection>();
    this.relationType = IDCache.Default.DocumentTree.Id;
    this.relationName = IDCache.Default.DocumentTree.Text;
    this.relationAttributeProvider = new DirectRelationAttributesRef(this.relationType);
  }

  private SyncClusters<DBObjectEntityRef, SyncDocumentStructureAction.DBPartInfo> CompareDocumentStructures()
  {
    List<DBObjectEntityRef> documentStructure1 = this.GetLocalDocumentStructure();
    SyncClusters<DBObjectEntityRef, SyncDocumentStructureAction.DBPartInfo> syncClusters = new SyncClusters<DBObjectEntityRef, SyncDocumentStructureAction.DBPartInfo>(documentStructure1.Count);
    if (ObjectSection.IsNewObject(this.projectItem))
    {
      syncClusters.NewItems.AddRange((IEnumerable<DBObjectEntityRef>) documentStructure1);
    }
    else
    {
      List<SyncDocumentStructureAction.DBPartInfo> documentStructure2 = this.GetDbDocumentStructure();
      foreach (DBObjectEntityRef dbObjectEntityRef in documentStructure1)
      {
        DBObjectEntityRef localPart = dbObjectEntityRef;
        int index = documentStructure2.FindIndex((Predicate<SyncDocumentStructureAction.DBPartInfo>) (dbPart => dbPart.ObjectId == localPart.GetObjectId()));
        if (index >= 0)
        {
          syncClusters.ExistingItems.Add(Tuple.Create<DBObjectEntityRef, SyncDocumentStructureAction.DBPartInfo>(localPart, documentStructure2[index]));
          documentStructure2.RemoveAt(index);
        }
        else
          syncClusters.NewItems.Add(localPart);
      }
      syncClusters.DeletedItems.AddRange((IEnumerable<SyncDocumentStructureAction.DBPartInfo>) documentStructure2);
    }
    return syncClusters;
  }

  private List<DBObjectEntityRef> GetLocalDocumentStructure()
  {
    List<DBObjectEntityRef> documentStructure = new List<DBObjectEntityRef>(this.projectFiles.Dependencies.Count);
    foreach (string dependency in (OrderedList<string>) this.projectFiles.Dependencies)
      documentStructure.Add(new DBObjectEntityRef(FilesSection.FindByMasterFile(this.ctx.Database, dependency) ?? throw new InvalidOperationException(string.Format(LocalizationHolder.rm.GetString("Tools.Components_465"), (object) dependency))));
    return documentStructure;
  }

  private List<SyncDocumentStructureAction.DBPartInfo> GetDbDocumentStructure()
  {
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.Columns = new object[3]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.F_OBJECT_TYPE,
      (object) ObligatoryObjectAttributes.F_PRJ_GUID
    };
    paramSet.RecordCount = -1;
    long objectId = ObjectSection.GetObjectId(this.projectItem);
    VersionsRulePackage editorRule = VersionsRuleSources.GetEditorRule();
    DataTable dataTable;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(this.relationType);
      if (this.documentTypeFilter.HasValue && this.documentTypeFilter.Value != -1)
        relationCollection.ObjectTypeID = this.documentTypeFilter.Value;
      relationCollection.FiltrationOwnerID = editorRule.OwnerId;
      dataTable = relationCollection.ConsistFrom(paramSet, objectId);
    }
    List<SyncDocumentStructureAction.DBPartInfo> documentStructure = new List<SyncDocumentStructureAction.DBPartInfo>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      documentStructure.Add(new SyncDocumentStructureAction.DBPartInfo(new Guid(Convert.ToString(row[2])), Convert.ToInt64(row[0]), Convert.ToInt32(row[1])));
    return documentStructure;
  }

  private void SyncNewRelations()
  {
    foreach (DBObjectEntityRef newItem in this.syncClusters.NewItems)
    {
      if (this.driver.Operations.Checkout.RequireCheckoutOnRelationModification(this.relationType, this.projectItem, (IDBObjectRef) newItem))
      {
        ObjectActionsSection projectActions = this.projectActions;
        projectActions.RequireCheckout = ((projectActions.RequireCheckout ? 1 : 0) | 1) != 0;
      }
      CreateRelationAction createRelationAction = new CreateRelationAction((IDBObjectRef) new DBObjectEntityRef(this.projectItem), (IDBObjectRef) newItem, this.relationType);
      this.projectActions.RelationActions.ServerActions.Add((IAction) createRelationAction);
      if (this.useFixedRelations)
        this.projectActions.RelationActions.ServerActions.Add((IAction) new FixRelationAction((IDBRelationRef) createRelationAction, (IDBObjectRef) newItem, RevisionInstantiationMode.Hard));
      if (this.ReadLocalRelationAttributes != null)
        this.SyncRelationAttributes(newItem.ObjectEntity, (IDBRelationRef) null, (IDBRelationRef) createRelationAction);
      this.projectActions.RelationActions.ClientActions.Add((IAction) new FireRelationCreatedAction((IDBRelationRef) createRelationAction, this.ctx.UINotifications));
    }
  }

  private void SyncExistingRelations()
  {
    if (this.ReadLocalRelationAttributes == null)
      return;
    foreach (Tuple<DBObjectEntityRef, SyncDocumentStructureAction.DBPartInfo> existingItem in this.syncClusters.ExistingItems)
    {
      DBObjectEntityRef dbObjectEntityRef = existingItem.Item1;
      ProjectGuidDBRelationRef guidDbRelationRef = new ProjectGuidDBRelationRef((IDBObjectRef) new DBObjectEntityRef(this.projectItem), existingItem.Item2.RelationGuid);
      if (this.SyncRelationAttributes(dbObjectEntityRef.ObjectEntity, (IDBRelationRef) guidDbRelationRef, (IDBRelationRef) guidDbRelationRef))
        this.projectActions.RelationActions.ClientActions.Add((IAction) new FireRelationModifiedAction((IDBRelationRef) guidDbRelationRef, this.ctx.UINotifications));
    }
  }

  private void SyncDeletedRelations()
  {
    foreach (SyncDocumentStructureAction.DBPartInfo deletedItem in this.syncClusters.DeletedItems)
    {
      if (this.driver.Operations.Checkout.RequireCheckoutOnRelationModification(this.relationType, this.projectItem, (IDBObjectRef) deletedItem))
      {
        ObjectActionsSection projectActions = this.projectActions;
        projectActions.RequireCheckout = ((projectActions.RequireCheckout ? 1 : 0) | 1) != 0;
      }
      DeleteRelationAction relationRef = new DeleteRelationAction((IDBObjectRef) new DBObjectEntityRef(this.projectItem), deletedItem.RelationGuid, this.relationType);
      this.projectActions.RelationActions.ServerActions.Add((IAction) relationRef);
      this.projectActions.RelationActions.ClientActions.Add((IAction) new FireRelationRemovedAction((IDBRelationRef) relationRef, this.ctx.UINotifications));
    }
  }

  private bool SyncRelationAttributes(
    SectionEntity partItem,
    IDBRelationRef relationAttributesRef,
    IDBRelationRef relationActionsRef)
  {
    RelationAttributesEventArgs e = new RelationAttributesEventArgs(this.projectItem, partItem);
    this.ReadLocalRelationAttributes((object) this, e);
    e.RelationAttributes.AcceptChanges();
    if (e.RelationAttributes.Count == 0)
      return false;
    ValueBag databaseSet = relationAttributesRef != null ? this.driver.Operations.Db.ReadRelationAttributes(relationAttributesRef, (IDBAttributableTypeRef) this.relationAttributeProvider) : this.driver.Operations.Db.ReadBlankAttributes((IDBAttributableTypeRef) this.relationAttributeProvider);
    this.TransferRelationAttributes(partItem, e.RelationAttributes, databaseSet);
    List<ValueRecord> changedItems = databaseSet.GetChangedItems();
    if (changedItems.Count == 0)
      return false;
    foreach (ValueRecord valueRecord in changedItems)
    {
      if (this.driver.Operations.Checkout.RequireCheckoutOnRelationAttribute(this.relationType, this.projectItem, partItem, valueRecord.Key))
      {
        ObjectActionsSection projectActions = this.projectActions;
        projectActions.RequireCheckout = ((projectActions.RequireCheckout ? 1 : 0) | 1) != 0;
        break;
      }
    }
    this.projectActions.RelationActions.ServerActions.Add((IAction) new WriteRelationAttributesAction(relationActionsRef, DBAttributeHelper.ToAttributeValues((IList<ValueRecord>) changedItems)));
    return true;
  }

  private void TransferRelationAttributes(
    SectionEntity partItem,
    ValueBag workingSet,
    ValueBag databaseSet)
  {
    AppToDBAttributeSyncTask attributeSyncTask = new AppToDBAttributeSyncTask();
    attributeSyncTask.EntityDisplayName = string.Format(LocalizationHolder.rm.GetString("Attribute.Tools.Components_34"), (object) this.relationName, (object) DisplaySection.GetQualifiedName(partItem));
    attributeSyncTask.SetApplicationAttributes(workingSet, false);
    attributeSyncTask.SetDatabaseAttributes(databaseSet, (IDBAttributableTypeRef) this.relationAttributeProvider);
    attributeSyncTask.AddAllAttributesToSync(false);
    attributeSyncTask.RunChecked();
  }

  public event EventHandler<RelationAttributesEventArgs> ReadLocalRelationAttributes;

  private void UpdateDocumentStructureStatus()
  {
    if (this.driver.Operations.Db.CanHaveIntegrationErrors(this.projectItem))
    {
      DBObjectErrorsBuilder integrationErrorsBuilder = this.driver.Operations.Db.GetIntegrationErrorsBuilder(this.projectItem);
      integrationErrorsBuilder.RemoveByCategory(DBObjectIntegrationStatus.PartialObjectStructureErrorCategory);
      this.driver.Operations.Db.UpdateIntegrationErrors(this.projectItem, integrationErrorsBuilder);
    }
    if (!this.driver.Operations.Db.CanHaveIntegrationStatus(this.projectItem))
      return;
    this.driver.Operations.Db.UpdatePartialStructureStatus(this.projectItem, false);
  }

  private string PartialStructureErrorId(int errorIndex) => $"DS:{errorIndex:D4}";

  private sealed class DBPartInfo : IDBObjectRef, IDBTypedEntityRef
  {
    public readonly long ObjectId;
    public readonly int ObjectType;
    public readonly Guid RelationGuid;

    public DBPartInfo(Guid relationGuid, long partId, int partType)
    {
      this.RelationGuid = relationGuid;
      this.ObjectId = partId;
      this.ObjectType = partType;
    }

    long IDBObjectRef.GetObjectId() => this.ObjectId;

    int IDBTypedEntityRef.GetEntityType() => this.ObjectType;
  }
}
