// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Selection.ImbaseFilterDescriptor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Selection;

public class ImbaseFilterDescriptor(long objID) : Descriptor(objID)
{
  private DataTable _dtFilter;

  public override INode GetChild(INodeID nodeID)
  {
    ImbaseFilterNode child = new ImbaseFilterNode(nodeID.TypeID, this._objID);
    child.SetFilter(this._dtFilter);
    return (INode) child;
  }

  public void SetFilter(DataTable dtFilter) => this._dtFilter = dtFilter;
}
