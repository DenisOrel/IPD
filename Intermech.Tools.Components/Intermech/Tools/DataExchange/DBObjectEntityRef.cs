// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.DBObjectEntityRef
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using Intermech.Interfaces.Data;
using System;

#nullable disable
namespace Intermech.Tools.DataExchange;

public sealed class DBObjectEntityRef : IDBObjectRef, IDBTypedEntityRef, IUpdateableDBObjectRef
{
  private SectionEntity objectEntity;

  public DBObjectEntityRef(SectionEntity objectEntity)
  {
    this.objectEntity = objectEntity != null ? objectEntity : throw new ArgumentNullException(nameof (objectEntity));
  }

  public SectionEntity ObjectEntity => this.objectEntity;

  public long GetObjectId() => ObjectSection.GetObjectId(this.objectEntity);

  public int GetEntityType() => ObjectSection.GetObjectType(this.objectEntity);

  public void UpdateObjectId(long newObjectId)
  {
    this.objectEntity.Sections.Get<ObjectSection>().ObjectId = newObjectId;
  }
}
