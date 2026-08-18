// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.CatalogsNodeDescriptor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;
using System;

#nullable disable
namespace Intermech.Imbase;

public class CatalogsNodeDescriptor : HiveDescriptor
{
  public CatalogsNodeDescriptor(int catalogType, string catalogTypeName)
    : base(Consts.CatalogsNodeCategoryID, catalogType, catalogTypeName)
  {
  }

  protected CatalogsNodeDescriptor(PersistentState state)
    : base(state)
  {
  }

  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    if (dataFormat == typeof (IDescriptor))
      return (object) new CatalogsNodeDescriptor(this._typeID, this._caption);
    return dataFormat == typeof (ICanOpenInNewWindow) ? (object) new CanOpenInNewWindow() : base.GetData(nodeID, dataFormat);
  }

  public override INode GetChild(INodeID nodeID)
  {
    INode child = base.GetChild(nodeID);
    (child as ICatalogsNode).Bind(this._caption);
    return child;
  }

  public override INodeID GetRecordNodeID() => (INodeID) new CatalogsNodeID(this._caption);
}
