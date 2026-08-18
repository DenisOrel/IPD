// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.MinorMaterialArticleHandler
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.ControlFlow.Cooperative;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces.Data;
using Intermech.Tools.Data;
using Intermech.Tools.Data.Sync;
using Intermech.Tools.DataExchange;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

public class MinorMaterialArticleHandler(
  MechanicalDriver driver,
  CaptureChangesDriverContext ctx,
  SectionEntity articleItem) : ArticleHandlerBase(driver, ctx, articleItem)
{
  private ObjectSection articleObj;
  private AttributesSection articleAttrs;
  private IDBAttributableTypeRef articleAttrsRef;

  protected override IEnumerable<CooperativeState> Coroutine()
  {
    this.Initialize();
    this.BindToDBObject();
    this.EnsureDBObjectExists();
    this.ReadDBObjectData();
    this.ProcessAttributes();
    this.DeleteUnwantedAttributes();
    this.MechanicalDriver.Operations.Db.EmitObjectAttributesServerActions(this.articleItem);
    yield return this.Call(new Func<IEnumerable<CooperativeState>>(this.WriteChangesToDisk));
    yield return this.Wait((IWaitObject) this.MechanicalDriver.SchedulerStages.UIStage);
    this.MechanicalDriver.Operations.Db.EmitUIActions(this.ctx, this.articleItem);
  }

  protected override object GetUIReportOperationId() => (object) this.articleItem;

  private void Initialize()
  {
    this.articleObj = this.articleItem.Sections.Get<ObjectSection>();
    this.articleAttrs = this.articleItem.Sections.Get<AttributesSection>();
  }

  private void BindToDBObject()
  {
    IArticleLocatorService articleLocatorService = this.MechanicalDriver.TryGetArticleLocatorService(this.articleItem);
    if (articleLocatorService == null)
      return;
    ArticleBinder.BindArticle(this.ctx, this.articleItem, articleLocatorService.CreateMinorMaterialLocator(this.articleItem), false);
  }

  private void EnsureDBObjectExists()
  {
    if (!this.articleObj.NewObject)
      return;
    this.CreateNewMaterial();
  }

  private void CreateNewMaterial()
  {
    if (this.articleObj.ObjectType == -1)
      this.articleObj.ObjectType = IDCache.Default.UndefinedMaterial.Id;
    this.MechanicalDriver.Operations.Db.CreateBlankObject(this.ctx, this.articleItem);
  }

  private void ReadDBObjectData()
  {
    this.articleAttrsRef = (IDBAttributableTypeRef) new DirectObjectAttributesRef(this.articleObj.ObjectType);
    this.MechanicalDriver.Operations.Db.FetchObjectAttributes(this.articleItem, this.articleAttrsRef);
  }

  private void ProcessAttributes()
  {
    this.PreserveAttributeScavengery();
    this.CorrectAttributes();
    this.TransferAttributes();
    this.PostCorrectAttributes();
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
    IArticleTypesService articleTypesService = this.MechanicalDriver.TryGetArticleTypesService(this.articleItem);
    if (articleTypesService != null)
    {
      string typeAttributeName = articleTypesService.GetArticleTypeAttributeName(this.articleItem);
      if (!string.IsNullOrEmpty(typeAttributeName))
        actions.Add((IAction) new MinorMaterialsObjectTypeHandler(this.articleItem, typeAttributeName));
    }
    return actions;
  }

  private void TransferAttributes()
  {
    AppToDBAttributeSyncTask attributeSyncTask = new AppToDBAttributeSyncTask();
    attributeSyncTask.EntityDisplayName = DisplaySection.GetQualifiedName(this.articleItem);
    attributeSyncTask.SetApplicationAttributes(this.articleAttrs.WorkingSet, this.articleAttrs.EmbeddedSet.IsOpenMetadata);
    attributeSyncTask.SetDatabaseAttributes(this.articleAttrs.DatabaseSet, this.articleAttrsRef);
    attributeSyncTask.Attributes.Add(new AttributeSyncUnit((StringKey) IDCache.Default.Designation.Text, true));
    attributeSyncTask.Attributes.Add(new AttributeSyncUnit((StringKey) IDCache.Default.OKPCode.Text, true));
    attributeSyncTask.Attributes.Add(new AttributeSyncUnit((StringKey) IDCache.Default.Name.Text, true));
    attributeSyncTask.RunChecked();
  }

  private void PostCorrectAttributes()
  {
    this.MechanicalDriver.TryGetArticleAttributesProcessingService(this.articleItem)?.PostprocessAttributes(this.articleItem, this.articleAttrs.WorkingSet, this.articleAttrs.DatabaseSet);
  }

  protected virtual void DeleteUnwantedAttributes()
  {
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
    this.articleApiService.EncodeArticleAttributes(this.articleItem, (ICollection<StringKey>) this.articleAttrs.WorkingSet.GetChangedItemsKeys(), this.articleAttrs.WorkingSet, this.articleAttrs.EmbeddedSet);
  }
}
