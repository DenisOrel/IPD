
// Type: Intermech.Interfaces.Data.ProjectGuidDBRelationRef
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Interfaces.Data;

/// <summary>
/// Реализует ссылку на связь между объектами IPS с помощью идентификатор версии составного объекта IPS и глобального идентификатора связи.
/// </summary>
/// <remarks>Реализация типа не является thread safe.</remarks>
public sealed class ProjectGuidDBRelationRef : IDBRelationRef
{
  private IDBObjectRef projectRef;
  private Guid relationGuid;
  private long relationId;
  private int relationType;

  public ProjectGuidDBRelationRef(IDBObjectRef projectRef, Guid relationGuid)
  {
    if (projectRef == null)
      throw new ArgumentNullException(nameof (projectRef));
    if (relationGuid == Guid.Empty)
      throw new ArgumentException("Не задан глобальный идентификатор связи.", nameof (relationGuid));
    this.projectRef = projectRef;
    this.relationGuid = relationGuid;
    this.relationId = 0L;
    this.relationType = -1;
  }

  public long GetProjectId() => this.projectRef.GetObjectId();

  public Guid GetRelationGuid() => this.relationGuid;

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
      IDBRelation relation = sessionKeeper.Session.GetRelation(this.relationGuid, this.GetProjectId(), true);
      this.relationId = relation.RelationID;
      this.relationType = relation.RelationType;
    }
  }
}
