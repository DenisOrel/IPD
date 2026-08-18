// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Nodes.ImbaseCatalogFavoritesDescriptor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;

#nullable disable
namespace Intermech.Imbase.Nodes;

public class ImbaseCatalogFavoritesDescriptor(long objId) : Descriptor(objId)
{
  public override INode GetChild(INodeID nodeID)
  {
    return (INode) new ImbaseCatalogFavoritesNode(nodeID.TypeID, this._objID);
  }
}
