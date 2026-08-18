// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.DBRelationEntityRef
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Interfaces.Data;
using System;

#nullable disable
namespace Intermech.Tools.DataExchange;

public sealed class DBRelationEntityRef : IDBRelationRef
{
  private RelationSection relSection;

  public DBRelationEntityRef(SectionEntity relationEntity)
  {
    this.relSection = relationEntity != null ? relationEntity.Sections.Get<RelationSection>() : throw new ArgumentNullException(nameof (relationEntity));
  }

  public long GetProjectId() => ObjectSection.GetObjectId(this.relSection.ProjectItem);

  public Guid GetRelationGuid() => this.relSection.RelationGuid;

  public long GetRelationId()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetRelation(this.GetRelationGuid(), this.GetProjectId(), true).RelationID;
  }

  public int GetRelationType()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetRelation(this.GetRelationGuid(), this.GetProjectId(), true).RelationType;
  }
}
