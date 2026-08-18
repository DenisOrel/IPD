// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.K3DNormalArticleHandler
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.Client.Core;
using Intermech.ControlFlow;
using Intermech.Data;
using Intermech.Data.EntityDb;
using Intermech.Data.SectionEntities;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class K3DNormalArticleHandler(
  K3DCaptureChangesDriver driver,
  CaptureChangesDriverContext ctx,
  SectionEntity articleItem) : NormalArticleHandler((MechanicalDriver) driver, ctx, articleItem)
{
  private static readonly StringKey[] ExternalMaterialIDs = new StringKey[3]
  {
    (StringKey) "ID материала",
    (StringKey) "ID материала 1",
    (StringKey) "ID материала 2"
  };

  private K3DCaptureChangesDriver K3DDriver
  {
    [DebuggerStepThrough] get => (K3DCaptureChangesDriver) this.MechanicalDriver;
  }

  public bool EnableUpdatingArticleMaterial { get; set; }

  protected override void OnAfterProcessAttributes()
  {
    base.OnAfterProcessAttributes();
    if (!this.EnableUpdatingArticleMaterial)
      return;
    K3DArticleMaterialParameterReader materialParameterReader = new K3DArticleMaterialParameterReader(this.K3DDriver.Integrator, this.articleItem, (IList<StringKey>) K3DNormalArticleHandler.ExternalMaterialIDs);
    this.UpdateArticleMaterialObject((StringKey) IDCache.Default.Material.Text, (IArticleMaterialParameterReader) materialParameterReader, (ICollection<StringKey>) new StringKey[1]
    {
      K3DNormalArticleHandler.ExternalMaterialIDs[0]
    });
    this.UpdateArticleMaterialObject((StringKey) IDCache.Default.MaterialReplacement1.Text, (IArticleMaterialParameterReader) materialParameterReader, (ICollection<StringKey>) new StringKey[1]
    {
      K3DNormalArticleHandler.ExternalMaterialIDs[1]
    }, (ICollection<Tuple<StringKey, StringKey>>) new Tuple<StringKey, StringKey>[1]
    {
      Tuple.Create<StringKey, StringKey>(K3DNormalArticleHandler.ExternalMaterialIDs[1], K3DNormalArticleHandler.ExternalMaterialIDs[0])
    });
    this.UpdateArticleMaterialObject((StringKey) IDCache.Default.MaterialReplacement2.Text, (IArticleMaterialParameterReader) materialParameterReader, (ICollection<StringKey>) new StringKey[1]
    {
      K3DNormalArticleHandler.ExternalMaterialIDs[2]
    }, (ICollection<Tuple<StringKey, StringKey>>) new Tuple<StringKey, StringKey>[1]
    {
      Tuple.Create<StringKey, StringKey>(K3DNormalArticleHandler.ExternalMaterialIDs[2], K3DNormalArticleHandler.ExternalMaterialIDs[0])
    });
  }

  private void UpdateArticleMaterialObject(
    StringKey materialAttribute,
    IArticleMaterialParameterReader materialParameterReader,
    ICollection<StringKey> synchronizedAttributes,
    ICollection<Tuple<StringKey, StringKey>> renameTable = null)
  {
    ValueRecord valueRecord = this.articleItem.Sections.Get<AttributesSection>().WorkingSet.Find(materialAttribute);
    if (valueRecord == null || valueRecord.DataType != typeof (long))
      return;
    long objectId = valueRecord.Read<long>(0L);
    if (objectId == 0L || this.ctx.Database.QueryFirst((IQueryCondition) new BinaryCondition((object) ObjectSection.ObjectIdRef, BinaryOperator.Equal, (object) objectId)) != null)
      return;
    SectionEntity sectionEntity = this.ctx.Database.AddReferencedDBObject(objectId);
    DisplaySection displaySection = sectionEntity.Sections.Get<DisplaySection>();
    displaySection.DisplayName = $"Материал #{objectId}";
    displaySection.QualifiedName = displaySection.DisplayName;
    if (!DBHelper.IsBasedOnType(ObjectSection.GetObjectType(sectionEntity), IDCache.Default.AllMaterials.Id))
      return;
    sectionEntity.Sections.Set((object) new AttributesSection());
    K3DArticleMaterialHandler task = new K3DArticleMaterialHandler(this.MechanicalDriver, this.ctx, this.articleItem, sectionEntity, materialParameterReader, synchronizedAttributes);
    if (renameTable != null)
      task.AttributeRenameTable = renameTable;
    this.ctx.Scheduler.AddTask((IAction) task);
  }
}
