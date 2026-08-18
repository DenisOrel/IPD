// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.TableReferenceNode
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Intermech.Imbase;

public class TableReferenceNode(int objTypeID, long objID) : ObjectNode(objTypeID, objID)
{
  internal long _tableId;
  internal int _recordsTypeId = Consts.ImbaseTableRecordTypeID;

  protected override List<PartSlot> CreateNonFolderSlots()
  {
    this.AnalizeId();
    return this.SlotsFromSinglePart((INodePart) new TableObjectsPart(this._recordsTypeId, this.GetConditions(), this, this.Services));
  }

  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    if (nodeID is ImbaseTableRecordNodeID tableRecordNodeId && (typeof (IDBObjectID).Equals(dataFormat) || typeof (IImbaseTableRecordID).Equals(dataFormat)))
      return (object) tableRecordNodeId.RecordId;
    return dataFormat == typeof (IDBTypedObjectID) ? (object) new DBTypedObjectID(this._objTypeID, this._objID, 0L, string.Empty, 0L, 0L, 0L, string.Empty, 0L) : (object) null;
  }

  private ConditionStructure[] GetConditions()
  {
    return new ConditionStructure[1]
    {
      new ConditionStructure(Consts.ImbaseTableRefAttID, RelationalOperators.Equal, (object) this._tableId, (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID)
    };
  }

  private void AnalizeId()
  {
    this._tableId = this._objID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (this._objTypeID == Consts.ImbaseTableRefTypeID)
      {
        IDBAttribute attributeById = sessionKeeper.Session.GetObject(this._objID).GetAttributeByID(Consts.ImbaseTableRefAttID);
        if (attributeById != null)
          this._tableId = attributeById.AsInteger;
      }
      IDBAttribute attributeById1 = sessionKeeper.Session.GetObject(this._tableId).GetAttributeByID(Consts.ImbaseTableRowsTypeAttID);
      if (attributeById1 == null)
        return;
      Guid anObjectTypeGuid = new Guid((string) attributeById1.Values[0]);
      IDBObjectType objectType = sessionKeeper.Session.GetObjectType(anObjectTypeGuid);
      if (objectType == null)
        return;
      this._recordsTypeId = objectType.ObjectType;
    }
  }

  internal void OnBeforeSelect(ref DBRecordSetParams setParams)
  {
    if (setParams.Tags == null)
      setParams.Tags = new HybridDictionary();
    setParams.Tags[(object) "$IM_TABLEID"] = (object) this._tableId;
    setParams.Tags[(object) "$IM_PARENTID"] = (object) this._objID;
  }
}
