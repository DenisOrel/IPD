// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ObjectsFromImbaseNode
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Parts;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase;

internal class ObjectsFromImbaseNode : ObjectTypeNode
{
  private long _objID;
  private long[] _objIDs;

  public ObjectsFromImbaseNode(int typeID, long objID, AccessRights accessRights)
    : base(typeID, accessRights)
  {
    this._objID = objID;
  }

  public ObjectsFromImbaseNode(int typeID, List<long> objIDs, AccessRights accessRights)
    : base(typeID, accessRights)
  {
    this._objIDs = objIDs.ToArray();
  }

  protected override List<PartSlot> CreateFolderSlots() => (List<PartSlot>) null;

  protected override List<PartSlot> CreateNonFolderSlots()
  {
    return this.SlotsFromSinglePart((INodePart) new ObjectsPart(this.ObjTypeID, this._objIDs == null ? new ConditionStructure(Consts.ImbaseObjectRefAttID, RelationalOperators.Equal, (object) this._objID, (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID) : new ConditionStructure(Consts.ImbaseObjectRefAttID, RelationalOperators.In, (object) this._objIDs, (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.ID), this.Services));
  }
}
