
// Type: Intermech.Interfaces.Data.Actions.CreateRelationActionBase
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ControlFlow;
using System;


namespace Intermech.Interfaces.Data.Actions;

public abstract class CreateRelationActionBase : IAction, IDBRelationRef
{
  private IDBObjectRef fromItem;
  private IDBObjectRef toItem;
  private int relationType;
  private long relationId;
  private Guid relationGuid;

  public CreateRelationActionBase(IDBObjectRef fromItem, IDBObjectRef toItem, int relationType)
  {
    if (fromItem == null)
      throw new ArgumentNullException();
    if (toItem == null)
      throw new ArgumentNullException();
    if (relationType == -1)
      throw new ArgumentException();
    this.fromItem = fromItem;
    this.toItem = toItem;
    this.relationType = relationType;
  }

  public void Perform()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelation relation = this.DoCreateRelation(this.fromItem.GetObjectId(), this.toItem.GetObjectId(), sessionKeeper.Session.GetRelationCollection(this.relationType));
      this.relationId = relation.RelationID;
      this.relationGuid = relation.GUID;
    }
  }

  /// <summary>сессию для результата брать у collection.Session</summary>
  /// <param name="fromId"></param>
  /// <param name="toId"></param>
  /// <param name="collection"></param>
  /// <returns></returns>
  protected abstract IDBRelation DoCreateRelation(
    long fromId,
    long toId,
    IDBRelationCollection collection);

  long IDBRelationRef.GetProjectId() => this.fromItem.GetObjectId();

  Guid IDBRelationRef.GetRelationGuid() => this.relationGuid;

  long IDBRelationRef.GetRelationId() => this.relationId;

  int IDBRelationRef.GetRelationType() => this.relationType;

  public long ProjectId => this.fromItem.GetObjectId();

  public int RelationType => this.relationType;

  public long RelationId => this.relationId;

  public Guid RelationGuid => this.relationGuid;
}
