// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ImbaseRootNodeDescriptor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase;

public class ImbaseRootNodeDescriptor : HiveDescriptor
{
  private List<long> _catalogIDs;

  public static string RootNodeCaption => LocalizationHolder.rm.GetString("Imbase.Client_90");

  public ImbaseRootNodeDescriptor()
    : base(Consts.RootNodeCategoryID, 0, ImbaseRootNodeDescriptor.RootNodeCaption)
  {
  }

  protected ImbaseRootNodeDescriptor(PersistentState state)
    : base(Consts.RootNodeCategoryID, 0, ImbaseRootNodeDescriptor.RootNodeCaption)
  {
  }

  public ImbaseRootNodeDescriptor(List<long> catalogIDs = null)
    : base(Consts.RootNodeCategoryID, 0, ImbaseRootNodeDescriptor.RootNodeCaption)
  {
    this._catalogIDs = catalogIDs;
  }

  public override void GetObjectData(PersistentState state)
  {
  }

  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    return !(dataFormat == typeof (IDescriptor)) ? (!(dataFormat == typeof (ICanOpenInNewWindow)) ? base.GetData(nodeID, dataFormat) : (object) new CanOpenInNewWindow()) : (object) new ImbaseRootNodeDescriptor(this._catalogIDs);
  }

  public override INode GetChild(INodeID nodeID)
  {
    return this._catalogIDs == null ? base.GetChild(nodeID) : (INode) new ImbaseRootNode(this._catalogIDs);
  }

  public override bool Equals(object obj)
  {
    ImbaseRootNodeDescriptor rootNodeDescriptor = obj as ImbaseRootNodeDescriptor;
    bool flag = false;
    if (rootNodeDescriptor != null)
    {
      if (this._categoryID == rootNodeDescriptor._categoryID && this._typeID == rootNodeDescriptor._typeID)
      {
        if (this._catalogIDs == rootNodeDescriptor._catalogIDs)
          flag = true;
        else if (this._catalogIDs == null || rootNodeDescriptor._catalogIDs == null)
          flag = false;
        else if (this._catalogIDs.Count != rootNodeDescriptor._catalogIDs.Count)
        {
          flag = false;
        }
        else
        {
          flag = true;
          this._catalogIDs.Sort();
          rootNodeDescriptor._catalogIDs.Sort();
          for (int index = 0; index < this._catalogIDs.Count; ++index)
          {
            if (!(flag = this._catalogIDs[index] == rootNodeDescriptor._catalogIDs[index]))
            {
              flag = false;
              break;
            }
          }
        }
      }
    }
    else
      flag = base.Equals(obj);
    return flag;
  }

  public override int GetHashCode() => base.GetHashCode();
}
