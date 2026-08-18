// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.SyncArticleDocumentationAction
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Interfaces.Data;
using Intermech.Interfaces.Data.Actions;
using Intermech.Localization;
using Intermech.Tools.Data;
using Intermech.Tools.Data.Sync;
using Intermech.Tools.DataExchange;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

public sealed class SyncArticleDocumentationAction : IAction
{
  private readonly DocumentCaptureChangesDriver driver;
  private readonly CaptureChangesDriverContext ctx;
  private readonly IArticleDocumentationService service;
  private readonly SectionEntity articleItem;
  private int relationType;
  private ObjectSection articleObj;
  private List<SectionEntity> documents;

  public SyncArticleDocumentationAction(
    DocumentCaptureChangesDriver driver,
    CaptureChangesDriverContext ctx,
    IArticleDocumentationService service,
    SectionEntity articleItem)
  {
    if (driver == null)
      throw new ArgumentNullException(nameof (driver));
    if (ctx == null)
      throw new ArgumentNullException(nameof (ctx));
    if (service == null)
      throw new ArgumentNullException(nameof (service));
    if (articleItem == null)
      throw new ArgumentNullException(nameof (articleItem));
    this.driver = driver;
    this.ctx = ctx;
    this.service = service;
    this.articleItem = articleItem;
  }

  public void Perform()
  {
    using (UIReport.CreateLogicalOperation((object) this.articleItem))
    {
      using (UIReport.CreateLogicalOperation((object) "SyncDocumentationStructure"))
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
    this.documents = this.service.GetDocuments(this.articleItem);
    if (this.documents.Count == 0)
      return;
    if (this.articleObj.NewObject)
      this.CreateNewRelations();
    else
      this.UpdateExistingRelations();
  }

  private void Initialize()
  {
    this.relationType = IDCache.Default.ArticleToDocumentTree.Id;
    this.articleObj = this.articleItem.Sections.Get<ObjectSection>();
  }

  [Conditional("DEBUG")]
  private void ValidateContext()
  {
    if (!this.articleObj.NewObject && this.articleObj.ObjectId == 0L)
      throw new InvalidOperationException(LocalizationHolder.rm.GetString("Tools.Components_440"));
  }

  private void Cleanup()
  {
    this.relationType = -1;
    this.articleObj = (ObjectSection) null;
    this.documents = (List<SectionEntity>) null;
  }

  private void CreateNewRelations()
  {
    foreach (SectionEntity document in this.documents)
      this.CreateNewRelation(document);
  }

  private void CreateNewRelation(SectionEntity documentItem)
  {
    ObjectActionsSection objectActionsSection1 = this.articleItem.Sections.Get<ObjectActionsSection>();
    DBObjectEntityRef dbObjectEntityRef = new DBObjectEntityRef(documentItem);
    if (this.driver.Operations.Checkout.RequireCheckoutOnRelationModification(this.relationType, this.articleItem, (IDBObjectRef) dbObjectEntityRef))
    {
      ObjectActionsSection objectActionsSection2 = objectActionsSection1;
      objectActionsSection2.RequireCheckout = ((objectActionsSection2.RequireCheckout ? 1 : 0) | 1) != 0;
    }
    CreateRelationActionBase relationRef = (CreateRelationActionBase) new CreateRelationIfNeedAction((IDBObjectRef) new DBObjectEntityRef(this.articleItem), (IDBObjectRef) dbObjectEntityRef, this.relationType);
    objectActionsSection1.RelationActions.ServerActions.Add((IAction) relationRef);
    objectActionsSection1.RelationActions.ClientActions.Add((IAction) new FireRelationCreatedAction((IDBRelationRef) relationRef, this.ctx.UINotifications));
    List<ValueRecord> changedItems = this.ProcessRelationAttributes(documentItem, (IDBRelationRef) null).DatabaseSet.GetChangedItems();
    if (changedItems.Count > 0)
      objectActionsSection1.RelationActions.ServerActions.Add((IAction) new WriteRelationAttributesAction((IDBRelationRef) relationRef, DBAttributeHelper.ToAttributeValues((IList<ValueRecord>) changedItems)));
    objectActionsSection1.RelationActions.ServerActions.Add((IAction) new FixRelationAction((IDBRelationRef) relationRef, (IDBObjectRef) new DBObjectEntityRef(documentItem), RevisionInstantiationMode.Hard));
  }

  private void UpdateExistingRelations()
  {
    if (this.articleObj.ObjectId == 0L)
      throw new InvalidOperationException(LocalizationHolder.rm.GetString("Tools.Components_440"));
    foreach (SectionEntity document in this.documents)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBRelation relation = sessionKeeper.Session.GetRelation(this.articleObj.ObjectId, ObjectSection.GetObjectId(document), this.relationType, true);
        if (relation == null)
          this.CreateNewRelation(document);
        else
          this.UpdateExistingRelation(document, relation.GUID);
      }
    }
  }

  private void UpdateExistingRelation(SectionEntity documentItem, Guid relationGuid)
  {
    ProjectGuidDBRelationRef guidDbRelationRef = new ProjectGuidDBRelationRef((IDBObjectRef) new DBObjectEntityRef(this.articleItem), relationGuid);
    List<ValueRecord> changedItems = this.ProcessRelationAttributes(documentItem, (IDBRelationRef) guidDbRelationRef).DatabaseSet.GetChangedItems();
    if (changedItems.Count <= 0)
      return;
    ObjectActionsSection objectActionsSection1 = this.articleItem.Sections.Get<ObjectActionsSection>();
    foreach (ValueRecord valueRecord in changedItems)
    {
      if (this.driver.Operations.Checkout.RequireCheckoutOnRelationAttribute(this.relationType, this.articleItem, documentItem, valueRecord.Key))
      {
        ObjectActionsSection objectActionsSection2 = objectActionsSection1;
        objectActionsSection2.RequireCheckout = ((objectActionsSection2.RequireCheckout ? 1 : 0) | 1) != 0;
      }
    }
    objectActionsSection1.RelationActions.ServerActions.Add((IAction) new WriteRelationAttributesAction((IDBRelationRef) guidDbRelationRef, DBAttributeHelper.ToAttributeValues((IList<ValueRecord>) changedItems)));
    objectActionsSection1.RelationActions.ClientActions.Add((IAction) new FireRelationModifiedAction((IDBRelationRef) guidDbRelationRef, this.ctx.UINotifications));
  }

  private AttributesSection ProcessRelationAttributes(
    SectionEntity documentItem,
    IDBRelationRef relationRefOrNull)
  {
    SectionEntity sectionEntity = new SectionEntity();
    AttributesSection sectionObject = new AttributesSection();
    sectionEntity.Sections.Set((object) sectionObject);
    sectionObject.WorkingSet = this.service.GetRelationAttributes(this.articleItem, documentItem);
    sectionObject.WorkingSet.Update((StringKey) IDCache.Default.BasedOnCADModel.Text, (object) true).Flags.Set(NamedFlags.ReadOnly);
    sectionObject.WorkingSet.AcceptChanges();
    IDBAttributableTypeRef attributableTypeRef = (IDBAttributableTypeRef) new DirectRelationAttributesRef(IDCache.Default.ArticleToDocumentTree.Id);
    sectionObject.DatabaseSet = relationRefOrNull != null ? this.driver.Operations.Db.ReadRelationAttributes(relationRefOrNull, attributableTypeRef) : this.driver.Operations.Db.ReadBlankAttributes(attributableTypeRef);
    AppToDBAttributeSyncTask attributeSyncTask = new AppToDBAttributeSyncTask();
    attributeSyncTask.EntityDisplayName = string.Format(LocalizationHolder.rm.GetString("Attribute.Tools.Components_34"), (object) IDCache.Default.ArticleToDocumentTree.Text, (object) DisplaySection.GetQualifiedName(this.articleItem));
    attributeSyncTask.SetApplicationAttributes(sectionObject.WorkingSet, sectionObject.EmbeddedSet.IsOpenMetadata);
    attributeSyncTask.SetDatabaseAttributes(sectionObject.DatabaseSet, attributableTypeRef);
    attributeSyncTask.AddAllAttributesToSync(false);
    AttributeSyncUnit attribute1 = attributeSyncTask.FindAttribute((StringKey) IDCache.Default.CADConfigurationFile.Text);
    if (attribute1 != null)
      attribute1.CaseInsensitive = true;
    AttributeSyncUnit attribute2 = attributeSyncTask.FindAttribute((StringKey) IDCache.Default.ObjectExternalKey.Text);
    if (attribute2 != null)
      attribute2.CaseInsensitive = true;
    attributeSyncTask.RunChecked();
    return sectionObject;
  }
}
