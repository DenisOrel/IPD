
// Type: Intermech.Interfaces.Data.Actions.DeleteRelationAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ControlFlow;
using Intermech.Localization;
using System;


namespace Intermech.Interfaces.Data.Actions;

public class DeleteRelationAction : IAction, IDBRelationRef
{
  private readonly IDBObjectRef fromItem;
  private readonly Guid relationGuid;
  private readonly int relationType;
  private long relationId;

  public DeleteRelationAction(IDBObjectRef fromItem, Guid relationGuid, int relationType)
  {
    if (fromItem == null)
      throw new ArgumentNullException(nameof (fromItem));
    if (relationGuid == Guid.Empty)
      throw new ArgumentException();
    if (relationType == -1)
      throw new ArgumentException();
    this.fromItem = fromItem;
    this.relationGuid = relationGuid;
    this.relationType = relationType;
  }

  public void Perform()
  {
    long objectId = this.fromItem.GetObjectId();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelation relation = sessionKeeper.Session.GetRelation(this.relationGuid, objectId, true);
      this.relationId = relation.RelationID;
      relation.Delete(512L /*0x0200*/);
    }
  }

  Guid IDBRelationRef.GetRelationGuid() => this.relationGuid;

  long IDBRelationRef.GetRelationId() => this.relationId;

  long IDBRelationRef.GetProjectId() => this.fromItem.GetObjectId();

  int IDBRelationRef.GetRelationType() => this.relationType;

  public override string ToString() => LocalizationHolder.rm.GetString("SR_1653");
}
