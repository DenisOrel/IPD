// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.AssemblyDwgDataExtractor
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Data.EntityDb;
using Intermech.Data.SectionEntities;
using Intermech.Tools.DataExchange;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class AssemblyDwgDataExtractor
{
  public void Perform(CaptureChangesDatabase database, StructData outputData)
  {
    if (database == null)
      throw new ArgumentNullException(nameof (database));
    if (outputData == null)
      throw new ArgumentNullException(nameof (outputData));
    this.ReflectObjectIds(database);
    this.ReflectRelationIds(database);
    DwgSpecData dwgSpecData = database.QueryFirst((IQueryCondition) new BinaryCondition(SectionVirtualProperties.SectionTypeRef, BinaryOperator.Equal, (object) typeof (DwgSpecData))).Sections.Get<DwgSpecData>();
    outputData.StructFile = dwgSpecData.StructFile;
    outputData.Spec = dwgSpecData.Spec;
    foreach (SectionEntity sectionEntity in (HashSet<IEntity>) database.Query((IQueryCondition) new BinaryCondition(SectionVirtualProperties.SectionTypeRef, BinaryOperator.Equal, (object) typeof (DwgProjectData))))
    {
      ObjectSection objectSection = sectionEntity.Sections.Get<ObjectSection>();
      outputData.ProjectIds.Add(objectSection.ObjectId);
      if (sectionEntity.Sections.Get<DwgProjectData>().BaseProject)
        outputData.BaseProjectId = objectSection.ObjectId;
    }
  }

  private void ReflectObjectIds(CaptureChangesDatabase database)
  {
    foreach (SectionEntity sectionEntity in (HashSet<IEntity>) database.Query((IQueryCondition) new CompoundSetCondition(CompoundSetOperator.Intersection, new IQueryCondition[2]
    {
      (IQueryCondition) new BinaryCondition(SectionVirtualProperties.SectionTypeRef, BinaryOperator.Equal, (object) typeof (PartData)),
      (IQueryCondition) new BinaryCondition(SectionVirtualProperties.SectionTypeRef, BinaryOperator.Equal, (object) typeof (ObjectSection))
    })))
    {
      ObjectSection objectSection = sectionEntity.Sections.Get<ObjectSection>();
      sectionEntity.Sections.Get<PartData>().ObjectId = objectSection.ObjectId;
    }
  }

  private void ReflectRelationIds(CaptureChangesDatabase database)
  {
    foreach (SectionEntity sectionEntity in (HashSet<IEntity>) database.Query((IQueryCondition) new CompoundSetCondition(CompoundSetOperator.Intersection, new IQueryCondition[2]
    {
      (IQueryCondition) new BinaryCondition(SectionVirtualProperties.SectionTypeRef, BinaryOperator.Equal, (object) typeof (SpecRelation)),
      (IQueryCondition) new BinaryCondition(SectionVirtualProperties.SectionTypeRef, BinaryOperator.Equal, (object) typeof (RelationSection))
    })))
    {
      RelationSection relationSection = sectionEntity.Sections.Get<RelationSection>();
      ObjectSection objectSection = relationSection.ProjectItem.Sections.Get<ObjectSection>();
      SpecRelation specRelation = sectionEntity.Sections.Get<SpecRelation>();
      specRelation.ProjectId = objectSection.ObjectId;
      specRelation.RelationGuid = relationSection.RelationGuid;
    }
  }
}
