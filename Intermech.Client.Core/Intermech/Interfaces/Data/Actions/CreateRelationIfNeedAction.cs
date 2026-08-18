
// Type: Intermech.Interfaces.Data.Actions.CreateRelationIfNeedAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;


namespace Intermech.Interfaces.Data.Actions;

public sealed class CreateRelationIfNeedAction(
  IDBObjectRef fromItem,
  IDBObjectRef toItem,
  int relationType) : CreateRelationActionBase(fromItem, toItem, relationType)
{
  protected override IDBRelation DoCreateRelation(
    long fromId,
    long toId,
    IDBRelationCollection collection)
  {
    return collection.Session.GetRelation(fromId, toId, collection.RelationTypeID, true) ?? collection.Create(fromId, toId);
  }

  public override string ToString() => LocalizationHolder.rm.GetString("SR_1648");
}
