
// Type: Intermech.Interfaces.Data.DirectDBRelationRef
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Interfaces.Data;

public sealed class DirectDBRelationRef : IDBRelationRef
{
  private readonly Guid relationGuid;
  private readonly long relationId;
  private readonly long projectId;
  private readonly int relationType;

  public DirectDBRelationRef(Guid relationGuid, long relationId, long projectId, int relationType)
  {
    this.relationGuid = !(relationGuid == Guid.Empty) ? relationGuid : throw new ArgumentException();
    this.relationId = relationId;
    this.projectId = projectId;
    this.relationType = relationType;
  }

  public long GetProjectId() => this.projectId;

  public Guid GetRelationGuid() => this.relationGuid;

  public long GetRelationId() => this.relationId;

  public int GetRelationType() => this.relationType;
}
