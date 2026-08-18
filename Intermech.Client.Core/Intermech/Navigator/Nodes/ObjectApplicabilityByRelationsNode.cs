
// Type: Intermech.Navigator.Nodes.ObjectApplicabilityByRelationsNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Intermech.Navigator.Nodes;

/// <summary>Узел объекта с применяемостью по связям</summary>
/// <summary>Конструктор</summary>
/// <param name="objectVersionID">Идентификатор версии объекта</param>
/// <param name="objectTypeID">Идентификатор типа объекта</param>
public class ObjectApplicabilityByRelationsNode(long objectVersionID, int objectTypeID) : ObjectNode(objectTypeID, objectVersionID)
{
  public static int[] GetRelationTypeIdsFromCompositionsAutosortRule(int objectTypeID)
  {
    List<int> source = new List<int>();
    if (ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service && service.Rule != null)
    {
      List<int> objectTypeIds = MetaDataHelper.GetObjectTypeParentsIDReverse(objectTypeID);
      objectTypeIds.Add(objectTypeID);
      if (service.Rule.ParentObjectTypes != null)
      {
        foreach (ParentObjectType parentObjectType in service.Rule.ParentObjectTypes)
        {
          if (parentObjectType.ChildRelationTypes != null)
          {
            foreach (ChildRelationType childRelationType in parentObjectType.ChildRelationTypes)
            {
              if (childRelationType.Visible && childRelationType.ChildObjectTypes != null && childRelationType.ChildObjectTypes.Any<ChildObjectType>((System.Func<ChildObjectType, bool>) (o => objectTypeIds.Contains(o.ObjectTypeID))))
                source.Add(childRelationType.RelationTypeID);
            }
          }
        }
      }
    }
    return source.Distinct<int>().ToArray<int>();
  }

  protected override List<PartSlot> CreateFolderSlots()
  {
    List<PartSlot> folderSlots = new List<PartSlot>();
    foreach (int relationTypeId in this.GetRelationTypeIds())
      folderSlots.Add(new PartSlot(MetaDataHelper.GetRelationTypeGuid(relationTypeId), (INodePart) new RelatedObjectsPart(this._objTypeID, this._objID, RelatedObjectsRole.Applicability, relationTypeId, this.Services)));
    return folderSlots;
  }

  private int[] GetRelationTypeIds()
  {
    List<int> list = ((IEnumerable<int>) ObjectApplicabilityByRelationsNode.GetRelationTypeIdsFromCompositionsAutosortRule(this._objTypeID)).ToList<int>();
    if (list.Count == 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (DataRow row in (InternalDataCollectionBase) sessionKeeper.Session.GetRelationsApplicabilityCollection().GetApplicabilitiesList(-1, this._objTypeID, -1).Rows)
        {
          IMSObjectType objectType = MetaDataHelper.GetObjectType(Convert.ToInt32(row["F_INOBJECT_TYPE"]));
          int int32 = Convert.ToInt32(row["F_RELATION_TYPE"]);
          if (objectType != null && objectType.DefaultRelation == int32)
            list.Add(int32);
        }
      }
    }
    return list.Distinct<int>().ToArray<int>();
  }
}
