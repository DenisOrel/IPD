
// Type: Intermech.Interfaces.Data.ProjectPartDBRelationRef
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Interfaces.Data;

/// <summary>
/// Реализует ссылку на связь между объектами IPS с помощью пары идентификаторов версий объектов IPS.
/// Тип связи может быть не задан, в этом случае будет использована первая попавшаяся связь.
/// </summary>
/// <remarks>Реализация типа не является thread safe.</remarks>
public sealed class ProjectPartDBRelationRef : IDBRelationRef
{
  private long projectId;
  private long partVersionId;
  private int relationType;
  private long relationId;
  private Guid relationGuid;

  public ProjectPartDBRelationRef(long projectId, long partVersionId, int relationType = -1)
  {
    if (projectId == 0L)
      throw new ArgumentException();
    if (partVersionId == 0L)
      throw new ArgumentException();
    this.projectId = projectId;
    this.partVersionId = partVersionId;
    this.relationType = relationType;
    this.relationId = 0L;
  }

  public long GetProjectId() => this.projectId;

  public Guid GetRelationGuid()
  {
    this.InitializeLazily();
    return this.relationGuid;
  }

  public long GetRelationId()
  {
    this.InitializeLazily();
    return this.relationId;
  }

  public int GetRelationType()
  {
    this.InitializeLazily();
    return this.relationType;
  }

  private void InitializeLazily()
  {
    if (this.relationId != 0L)
      return;
    this.Initialize();
  }

  private void Initialize()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelation relation = sessionKeeper.Session.GetRelation(this.projectId, this.partVersionId, this.relationType, true);
      this.relationGuid = relation != null ? relation.GUID : throw new Exception($"Связь между объектами #{this.projectId} и #{this.partVersionId} не найдена.");
      this.relationId = relation.RelationID;
      if (this.relationType != -1)
        return;
      this.relationType = relation.RelationType;
    }
  }
}
