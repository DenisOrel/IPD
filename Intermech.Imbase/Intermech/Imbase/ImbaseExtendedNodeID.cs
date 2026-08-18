// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ImbaseExtendedNodeID
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;

#nullable disable
namespace Intermech.Imbase;

public class ImbaseExtendedNodeID(CreateObjectNodeParams e) : NodeID(e), INodeIDExtended
{
  public NodeIDPath CorrectPath(NodeIDPath path, INodeID nodeID)
  {
    if (path.Length > 0 && path[path.Length - 1].CategoryID == Consts.RootNodeCategoryID && nodeID is ImbaseExtendedNodeID)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy((nodeID as ImbaseExtendedNodeID).ObjectID, false);
        if (objectActualCopy != null)
        {
          IDBAttribute attributeById = objectActualCopy.GetAttributeByID(Consts.CatalogTypeAttID);
          if (attributeById != null)
            path = new NodeIDPath(path, (INodeID) new CatalogsNodeID(attributeById.AsString));
        }
      }
    }
    return path;
  }
}
