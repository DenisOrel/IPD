// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ImbaseDescriptorsPart
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Persistence;
using System;

#nullable disable
namespace Intermech.Imbase;

public class ImbaseDescriptorsPart(DescriptorCollection descriptors) : DescriptorsPart(descriptors), INodeItems
{
  PersistentState INodeItems.Serialize(INodeID nodeID)
  {
    PersistentState persistentState = new PersistentState();
    if (nodeID is CatalogsNodeID catalogsNodeId && !string.IsNullOrEmpty(catalogsNodeId.CatalogName))
      persistentState.AddValue("Catalog", (object) catalogsNodeId.CatalogName);
    return persistentState;
  }

  INodeID INodeItems.Deserialize(PersistentState persistNodeID)
  {
    string catalogName = Convert.ToString(persistNodeID.GetValue("Catalog"));
    return string.IsNullOrEmpty(catalogName) ? (INodeID) null : (INodeID) new CatalogsNodeID(catalogName);
  }

  INode INodeItems.GetChild(INodeID nodeID)
  {
    INode child = (INode) null;
    if (nodeID is CatalogsNodeID catalogsNodeId)
    {
      child = (ServicesManager.GetService(typeof (IFactory)) as IFactory).GetNode(nodeID.CategoryID, nodeID.TypeID);
      if (child != null && child is ICatalogsNode catalogsNode)
        catalogsNode.Bind(catalogsNodeId.CatalogName);
    }
    return child;
  }
}
